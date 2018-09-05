Public Class frm_Export_TDL

	Private _arGames As ArrayList

	Private _lbl_Custom_Subtitle_Display_Text As String = ""

	Public Sub New(ByRef arGames As ArrayList, ByVal iTotalSelected As Integer)
		InitializeComponent()

		Me._lbl_Custom_Subtitle_Display_Text = lbl_Custom_Subtitle_Display.Text
		Me.txb_Custom_Subtitle_EditValueChanged(Nothing, Nothing)

		_arGames = arGames

		Me.lbl_Explanation.Text = Me.lbl_Explanation.Text.Replace("%1%", arGames.Count).Replace("%2%", iTotalSelected)

		Dim sTDL_Distro_File = TC.NZ(cls_Settings.GetSetting("TDL_distro"), System.Windows.Forms.Application.StartupPath & "\Tools\total-dos-launcher.zip")

		If Alphaleonis.Win32.Filesystem.File.Exists(sTDL_Distro_File) Then
			Me.txb_TDL_distro.EditValue = sTDL_Distro_File
		End If

		DS_ML.Fill_tbl_Total_DOS_Launcher_Configs(Me.DS_ML.tbl_Total_DOS_Launcher_Configs)
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

	Private Function Validate_TDL_distro(ByVal sPath As Object) As String
		'DEPRECATED, check .zip file contents!

		'If Not Alphaleonis.Win32.Filesystem.Directory.Exists(sPath) Then
		'	Return "The directory is not available"
		'End If

		'Dim files As String() = Alphaleonis.Win32.Filesystem.Directory.GetFiles(sPath)

		'Dim bHasTDLExe As Boolean = False

		'For Each file In files
		'	file = Alphaleonis.Win32.Filesystem.Path.GetFileName(file.ToLower)
		'	If file = "tdl.exe" Then
		'		bHasTDLExe = True
		'		Exit For
		'	End If
		'Next

		'If Not bHasTDLExe Then
		'	Return "The directory must at least contain TDL.EXE. Did you select the distro directory of Total DOS Launcher?"
		'End If

		'Return ""

		Return ""
	End Function

	Private Sub btn_Close_Click(sender As Object, e As EventArgs) Handles btn_Close.Click
		Me.Close()
	End Sub

	Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
		If Not Alphaleonis.Win32.Filesystem.File.Exists(txb_TDL_distro.Text) Then
			MKNetDXLib.cls_MKDXHelper.MessageBox("Total DOS Launcher .zip file does not exist.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Return
		End If

		Dim sValidationResult = Validate_TDL_distro(txb_TDL_distro.Text)

		If sValidationResult <> "" Then
			MKNetDXLib.cls_MKDXHelper.MessageBox(sValidationResult, "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Return
		End If

		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(txb_Destination.Text) Then
			MKNetDXLib.cls_MKDXHelper.MessageBox("Destination directory does not exist.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Return
		End If

		If Not MKNetLib.cls_MKFileSupport.isDirectoryEmpty(txb_Destination.Text) Then
			If Not MKNetDXLib.cls_MKDXHelper.MessageBox("Destination directory is not empty. Do you want to continue?", "Export", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) = DialogResult.Yes Then
				Return
			End If
		End If

		Me.Export()
	End Sub

	Public Class cls_TDL_Entry
		Public _foundfile As String 'Source filenames with full paths and extensions 
		Public _baseFile As String  'Source filenames with extensions (no paths)
		Public _title As Byte()     'Title within TDL's menu (currently just the long filename without extension)
		Public _DOSname As String   'DOS (8.3) filename

		Public _row_Emu_Games As DataRow

		Public Sub New(ByRef row_Emu_Games As DataRow)
			Me._row_Emu_Games = row_Emu_Games
			Me._foundfile = row_Emu_Games("Folder") & "\" & row_Emu_Games("File")
			Me._baseFile = Alphaleonis.Win32.Filesystem.Path.GetFileName(_foundfile)

			'TODO: generate title out of Meta Data instead of just the filename
			Me._title = System.Text.Encoding.ASCII.GetBytes(Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(_foundfile))
		End Sub
	End Class

	Public Class cls_TDL_Entry_Comparer
		Implements IComparer

		Private Function IComparer_Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
			Return String.Compare(x._baseFile, y._baseFile)
		End Function
	End Class

	Private Function Create_TDL_INI(ByVal path As String) As Boolean
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

		sb_TDL_INI.AppendLine("proglocations=" & Me.BS_Config.Current("proglocations"))

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
					If Not archive_entry.IsDirectory AndAlso archive_entry.FilePath.ToLower.Contains("/distro") AndAlso not archive_entry.FilePath.ToLower.Contains("tdl.ini") Then
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

	Private Sub Export()
		Dim arDOSNames As New ArrayList
		Dim arTDLEntries As New ArrayList

		For Each row_game As DataRow In Me._arGames
			arTDLEntries.Add(New cls_TDL_Entry(row_game))
		Next

		arTDLEntries.Sort(New cls_TDL_Entry_Comparer())

		'Find unique DOS names for each entry, if not possible set it to ""
		For Each tdl_entry As cls_TDL_Entry In arTDLEntries
			Dim filenameWithExtension As String = tdl_entry._baseFile
			Dim sDOSNameWithoutExtension As String = System.Text.Encoding.Default.GetString(System.Text.Encoding.ASCII.GetBytes(Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(filenameWithExtension)))
			sDOSNameWithoutExtension = MKNetLib.cls_MKStringSupport.MultiReplace(sDOSNameWithoutExtension, " +-'[](),.~!@#$%^&*{}:".ToCharArray(), "").ToUpper
			sDOSNameWithoutExtension = sDOSNameWithoutExtension.Replace(vbNullChar, "").Replace(CChar("?"), CChar("_"))
			If sDOSNameWithoutExtension.Length > 8 Then
				sDOSNameWithoutExtension = sDOSNameWithoutExtension.Substring(0, 8)
			End If

			'TODO: create a FindUniqueDOSName function
			If arDOSNames.Contains(sDOSNameWithoutExtension) Then
				'Debug.WriteLine("Find Unique DOS Name: " & sDOSNameWithoutExtension & " is already taken")

				For pos As Integer = sDOSNameWithoutExtension.Length - 1 To 0 Step -1
					For Each charPlacement As Char In "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
						'sDOSNameWithoutExtension = sDOSNameWithoutExtension.Remove(pos, 1).Insert(pos, charPlacement)
						Mid(sDOSNameWithoutExtension, pos + 1, 1) = charPlacement

						'Debug.Write("  trying " & sDOSNameWithoutExtension & " ... ")

						If Not arDOSNames.Contains(sDOSNameWithoutExtension) Then
							'Debug.WriteLine(" SUCCESS")
							Exit For
						End If

						'Debug.WriteLine(" FAIL")
					Next

					If Not arDOSNames.Contains(sDOSNameWithoutExtension) Then
						Exit For
					End If
				Next
			End If

			If arDOSNames.Contains(sDOSNameWithoutExtension) Then
				MKNetDXLib.cls_MKDXHelper.MessageBox("Unfortunately, " & tdl_entry._baseFile & " can't be added, because a unique DOS name couldn't be created. Please raise an issue on github and provide a file list.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning)
				tdl_entry._DOSname = ""
			End If

			arDOSNames.Add(sDOSNameWithoutExtension)
			tdl_entry._DOSname = sDOSNameWithoutExtension & Alphaleonis.Win32.Filesystem.Path.GetExtension(filenameWithExtension).ToUpper

		Next

		' DEPRECATED: Copy over the TDL distro
		'FileIO.FileSystem.CopyDirectory(txb_TDL_distro.Text, txb_Destination.Text)

		'extract TDL distro from TDL .zip file
		If Not Me.Unpack_TDL_Distro(Me.txb_Destination.Text) Then
			Return
		End If

		'rebuild HANDLERS.INI (CrLf line endings)
		If Not Me.Rebuild_HANDLERS_INI(Me.txb_Destination.Text & "\HANDLERS.INI") Then
			Return
		End If

		'build TDL.INI from tbl_Total_DOS_Launcher_Configs entry
		If Not Me.Create_TDL_INI(Me.txb_Destination.Text & "\TDL.INI") Then
			Return
		End If

		'Create FILES.IDX
		Using bw As System.IO.BinaryWriter = New System.IO.BinaryWriter(System.IO.File.Open(txb_Destination.Text & "\" & "FILES.IDX", System.IO.FileMode.Create))
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
		Using bw As System.IO.BinaryWriter = New System.IO.BinaryWriter(System.IO.File.Open(txb_Destination.Text & "\" & "TITLES.IDX", System.IO.FileMode.Create))
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

		'Create TITLES.DAT (Favourites)
		Using bw As System.IO.BinaryWriter = New System.IO.BinaryWriter(System.IO.File.Open(txb_Destination.Text & "\" & "TITLES.DAT", System.IO.FileMode.Create))
			For Each tdl_entry As cls_TDL_Entry In arTDLEntries
				If tdl_entry._DOSname = "" Then
					Continue For
				End If

				If TC.NZ(tdl_entry._row_Emu_Games("Favourite"), False) Then
					bw.Write(CByte(1))
				Else
					bw.Write(CByte(0))
				End If
			Next
		End Using

		Dim dirFiles As String = "\files"
		Dim dirFilesCounter As Integer = 0
		Dim bytesCounter As Integer = 0

		Dim fullPathDirFiles As String = txb_Destination.Text & dirFiles & IIf(Me.chb_Split_2GB_Chunks.Enabled AndAlso dirFilesCounter > 0, dirFilesCounter, "")

		'Fill the /files directory
		Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Nothing), 400, 100, ProgressBarStyle.Continuous, False, "Copy game {0} of {1} ...", 0, arDOSNames.Count, False)

		prg.Start()

		'TODO: Split 2GB (2,147,483,647 Byte)

		For Each tdl_entry As cls_TDL_Entry In arTDLEntries
			If tdl_entry._DOSname = "" Then
				Continue For
			End If

			prg.IncreaseCurrentValue()

			If Me.chb_Split_2GB_Chunks.Enabled Then
				Dim fi As New Alphaleonis.Win32.Filesystem.FileInfo(tdl_entry._foundfile)

				If fi.Length > 2147000000 Then
					prg.Hide = True

					Dim res As DialogResult = MKNetDXLib.cls_MKDXHelper.MessageBox("The file '" & tdl_entry._foundfile & "' exceeds the 2GB size limit. Copy anyways?", "Export", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)

					Select Case res
						Case DialogResult.Yes
							'Just going on
						Case DialogResult.No
							'skipping
							Continue For
						Case DialogResult.Cancel
							Return
					End Select

					prg.Hide = False
				End If

				If bytesCounter + fi.Length > 2147000000 Then
					bytesCounter = 0
					dirFilesCounter += 1
					fullPathDirFiles = txb_Destination.Text & dirFiles & IIf(Me.chb_Split_2GB_Chunks.Enabled AndAlso dirFilesCounter > 0, dirFilesCounter, "")
					bytesCounter += MKNetLib.cls_MKFileSupport.GetDirectorySize(fullPathDirFiles)
				End If

				bytesCounter += fi.Length
			End If

			'Create directory if it doesn't exist
			If Not Alphaleonis.Win32.Filesystem.Directory.Exists(fullPathDirFiles) Then
				Alphaleonis.Win32.Filesystem.Directory.CreateDirectory(fullPathDirFiles)
			End If

			If Not Alphaleonis.Win32.Filesystem.Directory.Exists(fullPathDirFiles) Then
				prg.Close()
				MKNetDXLib.cls_MKDXHelper.MessageBox("Failed to create the directory '" & fullPathDirFiles & "', abort.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Return
			End If

			If Alphaleonis.Win32.Filesystem.File.Exists(fullPathDirFiles & "\" & tdl_entry._DOSname) Then
				Try
					Alphaleonis.Win32.Filesystem.File.Delete(fullPathDirFiles & "\" & tdl_entry._DOSname)
				Catch ex As Exception

				End Try
			End If

			Try
				Alphaleonis.Win32.Filesystem.File.Copy(tdl_entry._foundfile, fullPathDirFiles & "\" & tdl_entry._DOSname)
			Catch ex As Exception

			End Try
		Next

		prg.Close()

		MKNetDXLib.cls_MKDXHelper.MessageBox("The export as a Total DOS Launcher Collection finished.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
	End Sub

	Private Sub btn_Destination_Click(sender As Object, e As EventArgs) Handles btn_Destination.Click
		Dim sDirectory As String = MKNetLib.cls_MKFileSupport.OpenFolderDialog("", True, Me)

		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(sDirectory) Then
			Return
		End If

		Me.txb_Destination.EditValue = sDirectory
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