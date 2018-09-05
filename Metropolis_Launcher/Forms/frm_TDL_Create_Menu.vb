Public Class frm_TDL_Create_Menu

	Public _row_Emu_Games As DS_ML.src_ucr_Emulation_GamesRow = Nothing

	Public _Destination_Path As String = ""

	Private _SuccessfullyCreated As Boolean = False

	Private _lbl_Custom_Subtitle_Display_Text As String = ""

	Public Sub New(ByRef row_Emu_Games As DS_ML.src_ucr_Emulation_GamesRow)

		InitializeComponent()

		Me._lbl_Custom_Subtitle_Display_Text = lbl_Custom_Subtitle_Display.Text

		Me._row_Emu_Games = row_Emu_Games

		Dim dt_cwd As New DataTable

		Dim sTDL_Distro_File = TC.NZ(cls_Settings.GetSetting("TDL_distro"), System.Windows.Forms.Application.StartupPath & "\Tools\total-dos-launcher.zip")

		If Alphaleonis.Win32.Filesystem.File.Exists(sTDL_Distro_File) Then
			Me.txb_TDL_distro.EditValue = sTDL_Distro_File
		End If

		DS_ML.Fill_tbl_Total_DOS_Launcher_Configs(Me.DS_ML.tbl_Total_DOS_Launcher_Configs)

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Fill_src_frm_Rom_Manager_Emu_Games(tran, Me.DS_ML.tbl_Emu_Games, Me._row_Emu_Games("id_Moby_Platforms"), Me._row_Emu_Games("id_Emu_Games"), Me._row_Emu_Games("id_Emu_Games"), cls_Globals.enm_Rombase_DOSBox_Filetypes.exe)

			dt_cwd = DS_ML.Fill_src_frm_Rom_Manager_Emu_Games(tran, Nothing, Me._row_Emu_Games("id_Moby_Platforms"), Me._row_Emu_Games("id_Emu_Games"), Me._row_Emu_Games("id_Emu_Games"), cls_Globals.enm_Rombase_DOSBox_Filetypes.cwd)

			If dt_cwd.Rows.Count > 0 Then
				_Destination_Path = TC.NZ(dt_cwd.Rows(0)("Folder"), "")
			End If
		End Using

		Dim iMaxSort As Integer = -1

		Dim arRowsRemove As New ArrayList

		DS_ML.Prepare_tmp_DOSBox_DisplayText(Me.DS_ML.tbl_Emu_Games, dt_cwd)

		For Each row As DS_ML.tbl_Emu_GamesRow In Me.DS_ML.tbl_Emu_Games.Rows
			'If the file is in a sub-folder, we want to display it that way: subfolder\file.ext
			'For Each row_cwd As DataRow In dt_cwd.Rows
			'	If TC.NZ(row_cwd("Folder"), "") <> "" AndAlso row("Folder").ToString.Contains(row_cwd("Folder")) Then
			'		row("InnerFile") = row("Folder").Replace(row_cwd("Folder"), "") & "\" & row("InnerFile")
			'	End If
			'Next

			''Packed content contains forward slashes, we want to display backward slashes, also we don't want a prepended backward slash
			'row("InnerFile") = MKNetLib.cls_MKStringSupport.Clean_Left(row("InnerFile").Replace("/", "\"), "\")

			If TC.NZ(row("TDL_DisplayText"), "") = "" Then

				row("TDL_DisplayText") = row("InnerFile")

				Try
					row("TDL_DisplayText") = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(row("TDL_DisplayText"))
				Catch ex As Exception

				End Try
			End If

			If TC.IsNullNothingOrEmpty(row("TDL_Show_in_Menu")) Then
				row("TDL_Show_in_Menu") = True
			End If

			If iMaxSort < TC.NZ(row("TDL_Sort"), 0) Then
				iMaxSort = TC.NZ(row("TDL_Sort"), 0)
			End If

			If row("InnerFile").tolower = "tdl.exe" Then
				arRowsRemove.Add(row)
			End If
		Next

		'We don't want to see TDL.exe in here
		For Each row As DataRow In arRowsRemove
			Me.DS_ML.tbl_Emu_Games.Rows.Remove(row)
		Next

		iMaxSort += 1

		For Each row As DS_ML.tbl_Emu_GamesRow In Me.DS_ML.tbl_Emu_Games.Rows
			If TC.isNULL(row("TDL_Sort")) Then
				row("TDL_Sort") = iMaxSort
				iMaxSort += 1
			End If
		Next

		Dim subtitle As Object = DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT TDL_Subtitle FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(row_Emu_Games("id_Emu_Games")))

		If TC.isNULL(subtitle) Then
			If TC.NZ(Me._row_Emu_Games("Game"), "") <> "" Then
				Me.txb_Custom_Subtitle.EditValue = ": " & TC.NZ(Me._row_Emu_Games("Game"), "")
			End If
		Else
			If TC.NZ(subtitle, "") <> "" Then
				Me.txb_Custom_Subtitle.EditValue = TC.NZ(subtitle, "")
			End If
		End If

		Me.txb_Custom_Subtitle_EditValueChanged(Nothing, Nothing)
	End Sub

	Private Sub frm_TDL_Create_Menu_Shown(sender As Object, e As EventArgs) Handles Me.Shown
		If _Destination_Path = "" Then
			MKNetDXLib.cls_MKDXHelper.MessageBox("The game doesn't seem to have a working directory, aborting.", "Directory not defined", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.Close()
			Return
		End If

		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(_Destination_Path) Then
			MKNetDXLib.cls_MKDXHelper.MessageBox("The working directory '" & _Destination_Path & "' could not be found, aborting.", "Directory not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.Close()
			Return
		End If
	End Sub

	Private Sub btn_Close_Click(sender As Object, e As EventArgs) Handles btn_Close.Click
		If Not _SuccessfullyCreated Then
			Me.DialogResult = DialogResult.Cancel
		Else
			Me.DialogResult = DialogResult.OK
		End If
		Me.Close()
	End Sub

	Private Sub btn_TDL_distro_www_Click(sender As Object, e As EventArgs) Handles btn_TDL_distro_www.Click
		cls_Globals.OpenURL("https://github.com/MobyGamer/total-dos-launcher/releases")
	End Sub

	Private Sub btn_TDL_distro_Click(sender As Object, e As EventArgs) Handles btn_TDL_distro.Click
		Dim sFile As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("Browse Total DOS Launcher .zip File", "*.zip|*.zip", ParentForm:=Me)

		If Not Alphaleonis.Win32.Filesystem.File.Exists(sFile) Then
			Return
		End If

		cls_Settings.SetSetting("TDL_distro", sFile)

		Me.txb_TDL_distro.EditValue = sFile
	End Sub

	Private Sub Refill_Total_DOS_Launcher_Configs()
		Me.DS_ML.tbl_Total_DOS_Launcher_Configs.Clear()
		DS_ML.Fill_tbl_Total_DOS_Launcher_Configs(Me.DS_ML.tbl_Total_DOS_Launcher_Configs)
	End Sub

	Private Sub cmb_Config_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_Config.ButtonClick
		Select Case e.Button.Kind
			Case DevExpress.XtraEditors.Controls.ButtonPredefines.Plus
				'Add TDL Config
				Using frm As New frm_Total_DOS_Launcher_Config_Edit
					If frm.ShowDialog = DialogResult.OK Then
						Dim id_Total_DOS_Launcher_Configs As Int64 = 0L
						Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
							id_Total_DOS_Launcher_Configs = DS_ML.Upsert_tbl_Total_DOS_Launcher_Configs(tran, frm.txb_DisplayName.Text.Trim, frm.txb_proglocations.Text.Trim, frm.txb_cachelocation.Text.Trim, TC.NZ(frm.cmb_userlevel.EditValue, ""), frm.chb_forcelogging.Checked, frm.chb_swapping.Checked, frm.chb_preloading.Checked, frm.chb_pauseafterrun.Checked, frm.cmb_VESA.EditValue)
							tran.Commit()
						End Using
						Refill_Total_DOS_Launcher_Configs()
						If id_Total_DOS_Launcher_Configs > 0L Then
							MKNetLib.cls_MKClientSupport.SetBindingSourcePosition(Me.BS_Config, "id_Total_DOS_Launcher_Configs", id_Total_DOS_Launcher_Configs)
						End If
					End If
				End Using
			Case DevExpress.XtraEditors.Controls.ButtonPredefines.Minus
				'Delete TDL Config

				If TC.NZ(Me.BS_Config.Current("id_Total_DOS_Launcher_Configs"), 0) < 0 Then
					MKDXHelper.MessageBox("The selected Total DOS Launcher Configuration cannot be deleted because it is shipped with Metropolis Launcher.", "Delete Total DOS Launcher Configuration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Return
				End If

				Dim id_Total_DOS_Launcher_Configs As Int64 = Me.BS_Config.Current("id_Total_DOS_Launcher_Configs")

				If MKDXHelper.MessageBox("Do you really want to delete this Total DOS Launcher Configuration?", "Delete Total DOS Launcher Configuration", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> DialogResult.Yes Then
					Return
				End If


				Dim sSQL As String = ""
				sSQL &= "DELETE FROM tbl_Total_DOS_Launcher_Configs WHERE id_Total_DOS_Launcher_Configs = " & TC.getSQLFormat(id_Total_DOS_Launcher_Configs)

				DataAccess.FireProcedure(cls_Globals.Conn, 0, sSQL)

				Me.cmb_Config.EditValue = Nothing
				Refill_Total_DOS_Launcher_Configs()
				If BS_Config.Current IsNot Nothing Then
					Me.cmb_Config.EditValue = BS_Config.Current("id_Total_DOS_Launcher_Configs")
				End If
			Case DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis
				'Edit TDL Config
				If BS_Config.Current Is Nothing Then
					Return
				End If
				Using frm As New frm_Total_DOS_Launcher_Config_Edit(Me.BS_Config.Current("id_Total_DOS_Launcher_Configs"))
					If frm.ShowDialog = DialogResult.OK Then
						If BS_Config.Current("id_Total_DOS_Launcher_Configs") > 0 Then
							'Edit (a user's TDL config was edited)
							Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
								DS_ML.Upsert_tbl_Total_DOS_Launcher_Configs(tran, frm.txb_DisplayName.Text.Trim, frm.txb_proglocations.Text.Trim, frm.txb_cachelocation.Text.Trim, TC.NZ(frm.cmb_userlevel.EditValue, ""), frm.chb_forcelogging.Checked, frm.chb_swapping.Checked, frm.chb_preloading.Checked, frm.chb_pauseafterrun.Checked, frm.cmb_VESA.EditValue, Me.BS_Config.Current("id_Total_DOS_Launcher_Configs"))
								tran.Commit()
							End Using
							Refill_Total_DOS_Launcher_Configs()
						ElseIf BS_Config.Current("id_Total_DOS_Launcher_Configs") < 0 Then
							'Add (a shipped TDL Config was edited)
							Dim id_Total_DOS_Launcher_Configs As Int64 = 0L
							Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
								id_Total_DOS_Launcher_Configs = DS_ML.Upsert_tbl_Total_DOS_Launcher_Configs(tran, frm.txb_DisplayName.Text.Trim, frm.txb_proglocations.Text.Trim, frm.txb_cachelocation.Text.Trim, TC.NZ(frm.cmb_userlevel.EditValue, ""), frm.chb_forcelogging.Checked, frm.chb_swapping.Checked, frm.chb_preloading.Checked, frm.chb_pauseafterrun.Checked, frm.cmb_VESA.EditValue)
								tran.Commit()
							End Using
							Refill_Total_DOS_Launcher_Configs()
							If id_Total_DOS_Launcher_Configs > 0L Then
								MKNetLib.cls_MKClientSupport.SetBindingSourcePosition(Me.BS_Config, "id_Total_DOS_Launcher_Configs", id_Total_DOS_Launcher_Configs)
							End If
						End If
					End If
				End Using

		End Select

	End Sub

	Private Function Validate_TDL_distro(ByVal path As String) As String
		Return ""
	End Function

	Private Sub btn_Create_Click(sender As Object, e As EventArgs) Handles btn_Create.Click
		If Not Alphaleonis.Win32.Filesystem.File.Exists(txb_TDL_distro.Text) Then
			MKNetDXLib.cls_MKDXHelper.MessageBox("Total DOS Launcher .zip file does not exist.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Return
		End If

		Dim sValidationResult As String = Validate_TDL_distro(txb_TDL_distro.Text)

		If sValidationResult <> "" Then
			MKNetDXLib.cls_MKDXHelper.MessageBox(sValidationResult, "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Return
		End If

		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(_Destination_Path) Then
			MKNetDXLib.cls_MKDXHelper.MessageBox("Destination directory does not exist.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Return
		End If

		Me.Export()
	End Sub

	Public Class cls_TDL_Entry
		Public Shared BatchCounter As Integer = 0

		Public _foundfile As String 'Source filenames with full paths and extensions 
		Public _baseFile As String  'Source filenames with extensions (no paths)
		Public _title As Byte()     'Title within TDL's menu (as defined by the user)
		Public _sort As Integer
		Public _DOSname As String   'DOS (8.3) filename

		Public _batchName As String 'a (unique) name of the batch file especially created for this game (will be placed under _TDL_\BATCHES)

		Public _row_Emu_Games As DataRow

		Public Sub New(ByRef row_Emu_Games As DataRow)
			Me._row_Emu_Games = row_Emu_Games
			Me._foundfile = row_Emu_Games("tmp_DOSBox_DisplayText")
			Me._baseFile = Alphaleonis.Win32.Filesystem.Path.GetFileName(_foundfile)
			Me._sort = TC.NZ(row_Emu_Games("TDL_Sort"), 0)

			Me._title = System.Text.Encoding.ASCII.GetBytes(row_Emu_Games("TDL_DisplayText"))

			BatchCounter += 1
			Me._batchName = BatchCounter.ToString & ".BAT"
		End Sub
	End Class

	Public Class cls_TDL_Entry_Comparer
		Implements IComparer

		Private Function IComparer_Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
			Return x._sort < y._sort
		End Function
	End Class

	Private Function Create_TDL_INI(ByVal path As String, ByVal proglocations As String) As Boolean
		Dim sb_TDL_INI As New System.Text.StringBuilder

		sb_TDL_INI.AppendLine("///")
		sb_TDL_INI.AppendLine("/// TDL.INI created by Metropolis Launcher")
		sb_TDL_INI.AppendLine("///")
		sb_TDL_INI.AppendLine("/// This is the TDL configuration file, which is read on startup and used")
		sb_TDL_INI.AppendLine("/// to configure how TDL operates, where archive files are found, etc.")
		sb_TDL_INI.AppendLine("/// Please read the descriptions for each setting before making changes.")
		sb_TDL_INI.AppendLine("///")
		sb_TDL_INI.AppendLine("")
		sb_TDL_INI.AppendLine("[prefs]")
		sb_TDL_INI.AppendLine("")
		sb_TDL_INI.AppendLine("; PATH-like variable that specifies where TDL can find the archive files")
		sb_TDL_INI.AppendLine("; prepared by the indexer and copied over to the vintage system.")
		sb_TDL_INI.AppendLine("; Multiple locations are allowed to get past the FAT16 2-gigabyte limit.")
		sb_TDL_INI.AppendLine("; (DOS limitations cap this path string at 80 characters -- do not exceed!)")

		sb_TDL_INI.AppendLine("proglocations=" & proglocations)

		sb_TDL_INI.AppendLine("")
		sb_TDL_INI.AppendLine(";Cache directory location.  This is where archives (.zip files, etc.) are")
		sb_TDL_INI.AppendLine(";decompressed into.  If you want to save your game, hi scores, etc., then")
		sb_TDL_INI.AppendLine(";this should be a permanent directory on a hard disk.  If you don't care")
		sb_TDL_INI.AppendLine(";about retaining files, you can put this on a RAM disk.")

		sb_TDL_INI.AppendLine("cachelocation=" & Me.BS_Config.Current("cachelocation"))

		sb_TDL_INI.AppendLine("")
		sb_TDL_INI.AppendLine("; TDL can operate in three user modes:")
		sb_TDL_INI.AppendLine("; - REGULAR is the default and recommended mode, where the TDL will try")
		sb_TDL_INI.AppendLine(";   to make educated decisions where applicable.")
		sb_TDL_INI.AppendLine("; - POWER displays more messages, copies the debug log to a text logfile,")
		sb_TDL_INI.AppendLine(";   and generally gives more control.")
		sb_TDL_INI.AppendLine("; - KIOSK is meant for shows, conventions, and museums; it disables features")
		sb_TDL_INI.AppendLine(";   that could confuse novice users, and makes as many decisions on behalf")
		sb_TDL_INI.AppendLine(";   of the user as possible.  ctrl-alt-delete is trapped and disabled.")
		sb_TDL_INI.AppendLine(";   Finally, the TDL cannot be exited in this mode.")
		sb_TDL_INI.AppendLine("; It is highly recommended you run in REGULAR mode unless instructed otherwise.")

		sb_TDL_INI.AppendLine("userlevel=" & Me.BS_Config.Current("userlevel"))

		sb_TDL_INI.AppendLine("")
		sb_TDL_INI.AppendLine("; Being a power user copies the debug log into a text file.  You can force")
		sb_TDL_INI.AppendLine("; this behavior ON for all user levels for troubleshooting purposes.")

		sb_TDL_INI.AppendLine("forcelogging=" & IIf(Me.BS_Config.Current("forcelogging"), "enabled", "disabled"))

		sb_TDL_INI.AppendLine("")
		sb_TDL_INI.AppendLine("; TDL swaps itself into EMS, XMS, extended memory, or disk when launching a")
		sb_TDL_INI.AppendLine("; program.  If you suspect this is causing problems, you can turn it off, but")
		sb_TDL_INI.AppendLine("; you will have less DOS RAM available for running your program.")

		sb_TDL_INI.AppendLine("swapping=" & IIf(Me.BS_Config.Current("swapping"), "enabled", "disabled"))

		sb_TDL_INI.AppendLine("")
		sb_TDL_INI.AppendLine("; TDL normally preloads data into system RAM to increase operating speed.")
		sb_TDL_INI.AppendLine("; If you have a very large number of titles loaded (over 10,000) and do")
		sb_TDL_INI.AppendLine("; not have any EMS or XMS memory configured, you might run out of low DOS")
		sb_TDL_INI.AppendLine("; RAM. You can disable preloading to force the TDL to work, but if your")
		sb_TDL_INI.AppendLine("; hard disk is a slow device, it could operate very slowly.  (Generally,")
		sb_TDL_INI.AppendLine("; don't disable preloading unless you experience memory errors.)")

		sb_TDL_INI.AppendLine("preloading=" & IIf(Me.BS_Config.Current("preloading"), "enabled", "disabled"))

		sb_TDL_INI.AppendLine("")
		sb_TDL_INI.AppendLine("; TDL normally returns to the menu after execution.  You can change this")
		sb_TDL_INI.AppendLine("; behavior to waiting for user input, so that the user can read the screen")
		sb_TDL_INI.AppendLine("; before returning.")

		sb_TDL_INI.AppendLine("pauseafterrun=" & IIf(Me.BS_Config.Current("pauseafterrun"), "enabled", "disabled"))

		sb_TDL_INI.AppendLine("")
		sb_TDL_INI.AppendLine("; TDL uses the text mode already set up in DOS.  If you'd like the TDL to")
		sb_TDL_INI.AppendLine("; initialize an extended VESA text mode, you can enter which mode here.")
		sb_TDL_INI.AppendLine("; Tested VESA text modes include:")
		sb_TDL_INI.AppendLine(";   108h   80x60")
		sb_TDL_INI.AppendLine(";   109h  132x25")
		sb_TDL_INI.AppendLine(";   10Ah  132x43")
		sb_TDL_INI.AppendLine(";   10Bh  132x50")
		sb_TDL_INI.AppendLine(";   10Ch  132x60")
		sb_TDL_INI.AppendLine("; You must enter the mode number in hex, ie. 108h")

		If TC.NZ(Me.BS_Config.Current("VESA"), "") = "" Then
			sb_TDL_INI.AppendLine(";VESA=10Ch")
		Else
			sb_TDL_INI.AppendLine("VESA=" & Me.BS_Config.Current("VESA"))
		End If

		sb_TDL_INI.AppendLine("")
		sb_TDL_INI.AppendLine("; TDL can display an optional subtitle after the main title.")
		sb_TDL_INI.AppendLine("; For example, instead of the default ""The Total DOS Launcher"", you can")
		sb_TDL_INI.AppendLine("; add a subheader "" - Action Games"", so that the title now becomes")
		sb_TDL_INI.AppendLine("; ""The Total DOS Launcher - Action Games"".  (Note how the "" - "" separator")
		sb_TDL_INI.AppendLine("; was part of the defined string; you can use whatever separator you want,")
		sb_TDL_INI.AppendLine("; such as "":  "".)  This is useful for building several sub-collections or")
		sb_TDL_INI.AppendLine("; anthologies you want grouped together.  Some examples below; uncomment")
		sb_TDL_INI.AppendLine("; and modify to use:")
		sb_TDL_INI.AppendLine(";subheader= - Action Games")
		sb_TDL_INI.AppendLine(";subheader=: Sierra AGI Anthology")
		sb_TDL_INI.AppendLine(";subheader= -=- iD Anthology: Vintage")

		If TC.NZ(Me.txb_Custom_Subtitle.EditValue, "") <> "" Then
			Dim sSubtitle As String = txb_Custom_Subtitle.Text.Trim()
			If Not sSubtitle.StartsWith(":") Then
				sSubtitle = " " & sSubtitle
			End If

			sSubtitle = System.Text.Encoding.Default.GetString(System.Text.Encoding.ASCII.GetBytes(sSubtitle))
			sb_TDL_INI.AppendLine("subheader=" & sSubtitle)
		End If

		Dim sError As String = ""
		If Not MKNetLib.cls_MKFileSupport.SaveTextToFile(sb_TDL_INI.ToString, path, sError) Then
			MKNetDXLib.cls_MKDXHelper.MessageBox("ERROR while writing " & path & ": " & sError, "Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return False
		End If

		Return True
	End Function

	Public Function Unpack_TDL_Distro(ByVal path As String) As Boolean
		Dim file As String = Me.txb_TDL_distro.Text
		If Not Alphaleonis.Win32.Filesystem.File.Exists(file) Then
			'File not found
			MKDXHelper.MessageBox("The Total DOS Launcher binary release file '" & file & "' could not be found.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return False
		Else
			'Unpack
			Try
				Dim archive As SharpCompress.Archive.IArchive = SharpCompress.Archive.ArchiveFactory.Open(file)

				For Each archive_entry As SharpCompress.Archive.IArchiveEntry In archive.Entries
					If Not archive_entry.IsDirectory AndAlso archive_entry.FilePath.ToLower.Contains("/distro") AndAlso Not archive_entry.FilePath.ToLower.Contains("tdl.ini") Then
						Dim innerPath As String = MKNetLib.cls_MKRegex.GetMatches(archive_entry.FilePath, ".*?distro/(.*)")(0).Groups(1).Value
						Dim arInnerPath As String() = innerPath.Split("/")

						Dim destinationPath As String = path

						If arInnerPath.Length > 1 Then
							'We have some "inner" directories to take care of
							For i As Integer = 0 To arInnerPath.Length - 2
								destinationPath &= "\" & arInnerPath(i)
								If Not Alphaleonis.Win32.Filesystem.Directory.Exists(destinationPath) Then
									Alphaleonis.Win32.Filesystem.Directory.CreateDirectory(destinationPath)
								End If
							Next
						End If

						SharpCompress.Archive.IArchiveEntryExtensions.WriteToDirectory(archive_entry, destinationPath, SharpCompress.Common.ExtractOptions.Overwrite)
					End If
				Next
			Catch ex As Exception
				MKDXHelper.ExceptionMessageBox(ex, "There has been an exception while unpacking " & file & "." & ControlChars.CrLf & "The error was: ", "Error")
				Return False
			End Try

			Return True
		End If
	End Function

	Public Function Rebuild_HANDLERS_INI(path) As Boolean
		If Not Alphaleonis.Win32.Filesystem.File.Exists(path) Then
			MKDXHelper.MessageBox("Error: The Total DOS Launcher distro does not contain a HANDLERS.INI file.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return False
		End If

		Dim sError As String = ""
		Dim sContents As String = MKNetLib.cls_MKFileSupport.GetFileContents(path, sError)
		If sError <> "" Then
			MKNetDXLib.cls_MKDXHelper.MessageBox("ERROR while reading " & path & ": " & sError, "Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return False
		End If

		Dim arContents = sContents.Split(ControlChars.Lf)

		Dim sbNewContents As New System.Text.StringBuilder

		For Each sLine In arContents
			sbNewContents.AppendLine(sLine.Trim)

			If sLine.Trim = "[END]" Then
				Exit For
			End If
		Next

		If Not MKNetLib.cls_MKFileSupport.SaveTextToFile(sbNewContents.ToString, path, sError) Then
			MKNetDXLib.cls_MKDXHelper.MessageBox("ERROR while writing " & path & ": " & sError, "Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return False
		End If

		Return True
	End Function

	Private Function Create_proglocations(arTDLEntries As ArrayList) As String
		Dim arProglocations As New ArrayList
		arProglocations.Add("..")

		Dim sProglocations As String = ".."

		For Each tdlEntry As cls_TDL_Entry In arTDLEntries
			Dim parts = tdlEntry._foundfile.Split("\")

			If parts.Count > 1 Then
				Dim path As String = ".."

				For i As Integer = 0 To parts.Count - 2
					path &= "\" & parts(i)
				Next

				If Not arProglocations.Contains(path) Then
					arProglocations.Add(path)
					sProglocations &= ";" & path
				End If
			End If
		Next

		Return sProglocations
	End Function

	Private Sub Export()
		Dim arDOSNames As New ArrayList
		Dim arTDLEntries As New ArrayList

		cls_TDL_Entry.BatchCounter = 0

		For Each row_game As DataRow In Me.DS_ML.tbl_Emu_Games.Rows
			If TC.NZ(row_game("TDL_Show_in_Menu"), False) = True Then
				arTDLEntries.Add(New cls_TDL_Entry(row_game))
			End If
		Next

		arTDLEntries.Sort(New cls_TDL_Entry_Comparer())

		'BUILD UP proglocations
		Dim useBatches As Boolean = False

		Dim proglocations = Create_proglocations(arTDLEntries)

		If proglocations.Length > 80 Then
			useBatches = True
		End If

		'Find unique DOS names for each entry, if not possible set it to ""
		If Not useBatches Then
			For Each tdl_entry As cls_TDL_Entry In arTDLEntries
				Dim filenameWithExtension As String = tdl_entry._baseFile

				If arDOSNames.Contains(filenameWithExtension) Then
					'MKNetDXLib.cls_MKDXHelper.MessageBox("Unfortunately, " & tdl_entry._baseFile & " can't be added, because it is a duplicate.", "Create Total DOS Launcher Menu", MessageBoxButtons.OK, MessageBoxIcon.Warning)
					'tdl_entry._DOSname = ""
					useBatches = True 'we revert to using batches so we can have unique DOS names
					Exit For
				End If

				arDOSNames.Add(filenameWithExtension)
				tdl_entry._DOSname = filenameWithExtension.ToUpper
			Next
		End If

		'Use Batches instead of the actual executables (which themselves however could also be batches)
		If useBatches Then
			proglocations = "BATCHES"
			arDOSNames.Clear()

			For Each tdl_entry As cls_TDL_Entry In arTDLEntries
				Dim filenameWithExtension As String = tdl_entry._batchName
				arDOSNames.Add(filenameWithExtension)
				tdl_entry._DOSname = filenameWithExtension.ToUpper
			Next
		End If

		Dim tdlPath As String = Me._Destination_Path & "\_TDL_"

		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(tdlPath) Then
			Try
				Alphaleonis.Win32.Filesystem.Directory.CreateDirectory(tdlPath)
			Catch ex As Exception
				MKNetDXLib.cls_MKDXHelper.ExceptionMessageBox(ex)
				Return
			End Try
		End If

		'Create BATCHES subdir and the individual Batch files
		If useBatches Then
			Try
				Alphaleonis.Win32.Filesystem.Directory.CreateDirectory(tdlPath & "\BATCHES")

				For Each tdl_entry As cls_TDL_Entry In arTDLEntries
					Dim sbContent As String = ""

					sbContent &= "@ECHO OFF"
					sbContent &= ControlChars.CrLf & "REM -------------------------------------------------------------------"
					sbContent &= ControlChars.CrLf & "REM --- Batch created by Metropolis Launcher for Total DOS Launcher ---"
					sbContent &= ControlChars.CrLf & "REM -------------------------------------------------------------------"
					sbContent &= ControlChars.CrLf
					sbContent &= ControlChars.CrLf & "ECHO Launching " & System.Text.Encoding.ASCII.GetString(tdl_entry._title)
					sbContent &= ControlChars.CrLf

					sbContent &= ControlChars.CrLf & "CD .."  'CD to _TDL_
					sbContent &= ControlChars.CrLf & "CD .."  'CD to "root"

					Dim splitPath As String() = tdl_entry._foundfile.Split("\")

					For i As Integer = 0 To splitPath.Length - 1
						If i = splitPath.Length - 1 Then
							sbContent &= ControlChars.CrLf & splitPath(i)
						Else
							sbContent &= ControlChars.CrLf & "CD " & splitPath(i)
						End If
					Next

					Dim errinfo As String = ""
					MKNetLib.cls_MKFileSupport.SaveTextToFile(sbContent, tdlPath & "\BATCHES\" & tdl_entry._batchName)
					If errinfo <> "" Then
						Throw New Exception(errinfo)
					End If
				Next
			Catch ex As Exception
				MKNetDXLib.cls_MKDXHelper.ExceptionMessageBox(ex)
				Return
			End Try
		End If

		'extract TDL distro from TDL .zip file
		If Not Me.Unpack_TDL_Distro(tdlPath) Then
			Return
		End If

		'rebuild HANDLERS.INI (CrLf line endings)
		If Not Me.Rebuild_HANDLERS_INI(tdlPath & "\HANDLERS.INI") Then
			Return
		End If

		'Dim proglocations = Create_proglocations(arTDLEntries)

		'If proglocations.Length > 80 Then
		'	MKNetDXLib.cls_MKDXHelper.MessageBox("Unfortunately, Total DOS Launcher only accepts proglocations to be max. 80 characters. The current menu definition demands the following proglocations: '" & proglocations & "' this is too long (" & proglocations.Length & " characters).", "Create Total DOS Launcher Menu", MessageBoxButtons.OK, MessageBoxIcon.Warning)
		'	Return
		'End If

		'build TDL.INI from tbl_Total_DOS_Launcher_Configs entry
		If Not Me.Create_TDL_INI(tdlPath & "\TDL.INI", proglocations) Then
			Return
		End If

		'Create FILES.IDX
		Using bw As System.IO.BinaryWriter = New System.IO.BinaryWriter(System.IO.File.Open(tdlPath & "\" & "FILES.IDX", System.IO.FileMode.Create))
			bw.Write(CType(arDOSNames.Count, UShort))

			Dim idx As UShort = 0
			For Each tdl_entry As cls_TDL_Entry In arTDLEntries
				If tdl_entry._DOSname = "" Then
					Continue For
				End If

				bw.Write(idx)
				bw.Write(System.Text.Encoding.ASCII.GetBytes(tdl_entry._DOSname.PadRight(12, vbNullChar)))

				idx = idx + 1
			Next
		End Using

		'Create TITLES.IDX
		Using bw As System.IO.BinaryWriter = New System.IO.BinaryWriter(System.IO.File.Open(tdlPath & "\" & "TITLES.IDX", System.IO.FileMode.Create))
			bw.Write(CType(arDOSNames.Count, UShort))

			'curofs=2+(len(titles)*4) #real starting offset is past the offset structure itself
			Dim arOffsets As New ArrayList
			Dim curofs As Integer = 2 + arDOSNames.Count * 4
			For Each tdl_entry As cls_TDL_Entry In arTDLEntries
				If tdl_entry._DOSname = "" Then
					Continue For
				End If

				arOffsets.Add(curofs)
				curofs = curofs + (2 + 16 + 1 + tdl_entry._title.Length)
			Next

			For Each offset In arOffsets
				bw.Write(CType(offset, UInt32))
			Next

			Dim idx As UShort = 0
			For Each tdl_entry As cls_TDL_Entry In arTDLEntries
				If tdl_entry._DOSname = "" Then
					Continue For
				End If

				bw.Write(idx)

				'TODO: write MD5 hash of title
				bw.Write(System.Text.Encoding.ASCII.GetBytes("0123456789ABCDEF"))

				bw.Write(CType(tdl_entry._title.Length, Byte))

				bw.Write(tdl_entry._title)

				idx = idx + 1
			Next
		End Using

		'rescan must NOT scan more than the TDL.EXE under _TDL_
		Try
			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				frm_Rom_Manager.Rescan_DOSBox_Game(Me._row_Emu_Games("id_Emu_Games"), tran)
				tran.Commit()
			End Using
		Catch ex As Exception
			'
		End Try

		'save to tbl_Emu_Games
		For Each row_Emu_Game As DS_ML.tbl_Emu_GamesRow In DS_ML.tbl_Emu_Games
			Dim sSQL As String = "UPDATE tbl_Emu_Games SET" & controlchars.CrLf

			sSQL &= "TDL_DisplayText = " & TC.getSQLFormat(row_Emu_Game.TDL_DisplayText) & ControlChars.CrLf
			sSQL &= ", TDL_Show_in_Menu = " & TC.getSQLFormat(row_Emu_Game.TDL_Show_in_Menu) & ControlChars.CrLf
			sSQL &= ", TDL_Sort = " & TC.getSQLFormat(row_Emu_Game.TDL_Sort) & ControlChars.CrLf

			sSQL &= "WHERE id_Emu_Games = " & row_Emu_Game("id_Emu_Games")

			DataAccess.FireProcedure(cls_Globals.Conn, 0, sSQL)
		Next

		If Not TC.isNULL(Me.txb_Custom_Subtitle.EditValue) AndAlso Not Me.txb_Custom_Subtitle.EditValue Is Nothing Then
			DataAccess.FireProcedure(cls_Globals.Conn, 0, "UPDATE tbl_Emu_Games SET TDL_Subtitle = " & TC.getSQLFormat(Me.txb_Custom_Subtitle.EditValue) & " WHERE id_Emu_Games = " & TC.getSQLFormat(Me._row_Emu_Games("id_Emu_Games")))
		Else
			DataAccess.FireProcedure(cls_Globals.Conn, 0, "UPDATE tbl_Emu_Games SET TDL_Subtitle = NULL WHERE id_Emu_Games = " & TC.getSQLFormat(Me._row_Emu_Games("id_Emu_Games")))
		End If

		_SuccessfullyCreated = True

		MKNetDXLib.cls_MKDXHelper.MessageBox("Total DOS Launcher menu created.", "Create Total DOS Launcher Menu", MessageBoxButtons.OK, MessageBoxIcon.Information)
	End Sub

	Private Sub btn_Move_Up_Click(sender As Object, e As EventArgs) Handles btn_Move_Up.Click
		If BS_Emu_Games.Current Is Nothing Then
			Return
		End If

		Dim currentSort As Integer = BS_Emu_Games.Current("TDL_Sort")
		For Each row_Emu_Games As DataRow In DS_ML.tbl_Emu_Games.Rows
			If row_Emu_Games("TDL_Sort") = currentSort - 1 Then
				row_Emu_Games("TDL_Sort") = currentSort
				BS_Emu_Games.Current("TDL_Sort") = currentSort - 1
			End If
		Next
	End Sub

	Private Sub btn_Move_Down_Click(sender As Object, e As EventArgs) Handles btn_Move_Down.Click
		If BS_Emu_Games.Current Is Nothing Then
			Return
		End If

		Dim currentSort As Integer = BS_Emu_Games.Current("TDL_Sort")
		For Each row_Emu_Games As DataRow In DS_ML.tbl_Emu_Games.Rows
			If row_Emu_Games("TDL_Sort") = currentSort + 1 Then
				row_Emu_Games("TDL_Sort") = currentSort
				BS_Emu_Games.Current("TDL_Sort") = currentSort + 1
			End If
		Next
	End Sub

	Private Sub txb_Custom_Subtitle_EditValueChanged(sender As Object, e As EventArgs) Handles txb_Custom_Subtitle.EditValueChanged
		Dim sSubtitle As String = txb_Custom_Subtitle.Text.Trim()
		If Not sSubtitle.StartsWith(":") Then
			sSubtitle = " " & sSubtitle
		End If

		sSubtitle = System.Text.Encoding.Default.GetString(System.Text.Encoding.ASCII.GetBytes(sSubtitle))

		Me.lbl_Custom_Subtitle_Display.Text = _lbl_Custom_Subtitle_Display_Text.Replace("%SUBTITLE%", sSubtitle)
	End Sub

	Private Sub Update_Custom_Subtitle_Warning()
		Me.lbl_Custom_Subtitle_Warning.Visible = False

		If Me.BS_Config.Current Is Nothing Then
			Return
		End If

		If lbl_Custom_Subtitle_Display.Text.Length > 78 Then
			Dim vesaMode As String = TC.NZ(Me.BS_Config.Current("VESA"), "")

			If {"", "108h"}.Contains(vesaMode) Then
				Me.lbl_Custom_Subtitle_Warning.Visible = True
			End If
		End If
	End Sub

	Private Sub lbl_Custom_Subtitle_Display_TextChanged(sender As Object, e As EventArgs) Handles lbl_Custom_Subtitle_Display.TextChanged
		Update_Custom_Subtitle_Warning()
	End Sub

	Private Sub BS_Config_CurrentChanged(sender As Object, e As EventArgs) Handles BS_Config.CurrentChanged
		Update_Custom_Subtitle_Warning()
	End Sub
End Class