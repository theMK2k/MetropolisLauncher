Imports System.ComponentModel

Public Class frm_Emulators

	Public Sub New()
		InitializeComponent()

		DS_Rombase.Fill_tbl_Rombase_Known_Emulators(Me.DS_Rombase.tbl_Rombase_Known_Emulators)

		barmng.SetPopupContextMenu(grd_Emulators, popmnu_Emulators)

		Cursor.Current = Cursors.WaitCursor

		frm_Tag_Parser_Edit.Fill_Tag_Parser_Volumes(Me.DS_ML.ttb_Tag_Parser_Volumes)

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction()
			Me.DS_MobyDB.Fill_tbl_Moby_Platforms(tran, Me.DS_MobyDB.tbl_Moby_Platforms, True)
			Me.DS_ML.Fill_src_frm_Emulators(tran, Me.DS_ML.tbl_Emulators)
		End Using

		DS_ML.Fill_tbl_List_Generators(Me.DS_ML.tbl_List_Generators)

		'Fill the J2K Config DS
		cls_Settings.Fill_J2K_DS(Me.DS_J2K, TC.NZ(cls_Settings.GetSetting("Path_J2K"), ""))

		Cursor.Current = Cursors.Default

		BS_Emulators_CurrentChanged(Nothing, Nothing)
	End Sub

	Private _J2KPreset_Original As Object

	Private Function isRetroArch() As Boolean
		If BS_Emulators.Current Is Nothing Then Return False
		Return TC.NZ(BS_Emulators.Current("Executable"), "").ToLower.Contains("retroarch")
	End Function

	Private Function isDOSBox() As Boolean
		If BS_Emulators.Current Is Nothing Then Return False
		Return TC.NZ(BS_Emulators.Current("Executable"), "").ToLower.Contains("dosbox")
	End Function

	Private Function isScummVM() As Boolean
		If BS_Emulators.Current Is Nothing Then Return False
		Return TC.NZ(BS_Emulators.Current("Executable"), "").ToLower.Contains("scummvm")
	End Function

	Private Function hasNewDOSBoxPatches() As Boolean
		'New DOSBox Patches and of course a newly added emulator contains at least one Patch row with Activated = NULL
		For Each row As DataRow In DS_ML.src_frm_Emulators_DOSBox_Patches.Rows
			If TC.IsNullNothingOrEmpty(row("Activated")) Then
				Return True
			End If
		Next

		Return False
	End Function

	Public Enum enm_DOSBox_Builds
		Unknown = 0
		DAUM = 1
		MB = 2
		ECE = 3
	End Enum

	Private Sub DetectNewDOSBoxPatches()
		Dim dosbox_build As enm_DOSBox_Builds = enm_DOSBox_Builds.Unknown

		Dim row As DataRow = BS_Emulators.Current.Row
		Dim sFullPath As String = row("InstallDirectory") & "\" & row("Executable")
		If Alphaleonis.Win32.Filesystem.File.Exists(sFullPath) Then
			Try
				Dim sContent As String = MKNetLib.cls_MKFileSupport.GetFileContents(sFullPath)
				If sContent.Contains("SVN_MB") Then
					dosbox_build = enm_DOSBox_Builds.MB
				ElseIf sContent.Contains("SVN-Daum") Then
					dosbox_build = enm_DOSBox_Builds.DAUM
				ElseIf sContent.Contains("ECE") Then
					dosbox_build = enm_DOSBox_Builds.ECE
				End If

				For Each row_patch As DataRow In DS_ML.src_frm_Emulators_DOSBox_Patches.Rows
					If TC.IsNullNothingOrEmpty(row_patch("Activated")) Then
						Select Case dosbox_build
							Case enm_DOSBox_Builds.DAUM
								row_patch("Activated") = TC.NZ(row_patch("DAUM_Supported"), False)
							Case enm_DOSBox_Builds.MB
								row_patch("Activated") = TC.NZ(row_patch("MB_Supported"), False)
							Case enm_DOSBox_Builds.ECE
								row_patch("Activated") = TC.NZ(row_patch("ECE_Supported"), False)
						End Select
						row_patch.AcceptChanges()
					End If
				Next
			Catch ex As Exception

			End Try
		End If

	End Sub

	Private Sub BS_Emulators_CurrentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BS_Emulators.CurrentChanged
		Cursor.Current = Cursors.WaitCursor

		If BS_Emulators.Current Is Nothing Then
			pnl_Right.Visible = False

			btn_Duplicate_Emulator.Enabled = False
			btn_Delete_Emulator.Enabled = False
			cmb_J2K_Config.EditValue = DBNull.Value
		Else
			pnl_Right.Visible = True

			btn_Duplicate_Emulator.Enabled = True
			btn_Delete_Emulator.Enabled = True

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				Me.DS_ML.Fill_src_frm_Emulators_Moby_Platforms(tran, Me.DS_ML.src_frm_Emulators_Moby_Platforms, BS_Emulators.Current("id_Emulators"))
				Me.DS_ML.Fill_src_frm_Emulators_Multivolume_Parameters(tran, Me.DS_ML.tbl_Emulators_Multivolume_Parameters, BS_Emulators.Current("id_Emulators"))

				tran.Commit()
			End Using

			If isDOSBox() Then
				txb_StartupParameter.Visible = False
				lbl_StartupParameter.Visible = False
				lbl_List_Generator.Visible = False
				cmb_List_Generator.Visible = False
				tpg_DOSBox_Patches.PageVisible = True
				tpg_MV_Settings.PageVisible = False
				'lbl_AutoItScript.Visible = False
				'memo_AutItScript.Visible = False
				BS_Platforms.Filter = "id_Moby_Platforms IN (2, 4)"
				Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
					DS_ML.Fill_src_frm_Emulators_DOSBox_Patches(tran, DS_ML.src_frm_Emulators_DOSBox_Patches, BS_Emulators.Current("id_Emulators"))
					DS_ML.Fill_src_frm_Emulators_DOSBox_Patches_Categories(tran, DS_ML.src_frm_Emulators_DOSBox_Patches_Categories)
				End Using

				'TODO: DOSBox detection (external Function for existing DOSBoxes w/o entries in tbl_Emulators_DOSBox_Patches)
				If hasNewDOSBoxPatches() Then
					DetectNewDOSBoxPatches()
				End If

				'Automatically check Supported for the DOS Platform
				For Each row As DS_ML.src_frm_Emulators_Moby_PlatformsRow In Me.DS_ML.src_frm_Emulators_Moby_Platforms.Rows
					If row.id_Moby_Platforms = cls_Globals.enm_Moby_Platforms.dos Then
						If TC.NZ(row("Supported"), False) = False Then
							row("Supported") = True
						End If
					End If
				Next
			ElseIf isScummVM() Then
				txb_StartupParameter.Visible = False
				lbl_StartupParameter.Visible = False
				lbl_List_Generator.Visible = False
				cmb_List_Generator.Visible = False
				tpg_DOSBox_Patches.PageVisible = False
				tpg_MV_Settings.PageVisible = False
				'lbl_AutoItScript.Visible = False
				'memo_AutItScript.Visible = False
				BS_Platforms.Filter = "id_Moby_Platforms IN (-3)"

				'Automatically check Supported for the DOS Platform
				For Each row As DS_ML.src_frm_Emulators_Moby_PlatformsRow In Me.DS_ML.src_frm_Emulators_Moby_Platforms.Rows
					If row.id_Moby_Platforms = cls_Globals.enm_Moby_Platforms.scummvm Then
						If TC.NZ(row("Supported"), False) = False Then
							row("Supported") = True
						End If
					End If
				Next
			Else
				txb_StartupParameter.Visible = True
				lbl_StartupParameter.Visible = True
				lbl_List_Generator.Visible = True
				cmb_List_Generator.Visible = True
				tpg_DOSBox_Patches.PageVisible = False
				tpg_MV_Settings.PageVisible = True
				'lbl_AutoItScript.Visible = True
				'memo_AutItScript.Visible = True
				BS_Platforms.Filter = ""
				DS_ML.src_frm_Emulators_DOSBox_Patches_Categories.Clear()
				DS_ML.src_frm_Emulators_DOSBox_Patches.Clear()
			End If

			'J2K
			If BS_Emulators.Current("J2KPreset") Is DBNull.Value Then
				cmb_J2K_Config.EditValue = DBNull.Value
				_J2KPreset_Original = DBNull.Value
			Else
				'Set the Config
				Dim j2k_config As String = BS_Emulators.Current("J2KPreset")
				_J2KPreset_Original = j2k_config

				MKNetLib.cls_MKClientSupport.SetBindingSourcePosition(BS_J2K, "ConfigName", j2k_config)
				Try
					cmb_J2K_Config.EditValue = BS_J2K.Current("id_Config")
				Catch ex As Exception

				End Try
			End If
		End If

		Refill_LibretroCore()

		If DetectEmu().Rows.Count > 0 Then
			Me.btn_AutoConfig.Enabled = True
		Else
			Me.btn_AutoConfig.Enabled = False
		End If

		Cursor.Current = Cursors.Default
	End Sub

	Private Function CheckSave(Optional ByRef row As DataRow = Nothing) As DialogResult
		Debug.WriteLine("CheckSave START")

		Me.BS_Emulators.EndEdit()

		If row Is Nothing AndAlso BS_Emulators IsNot Nothing Then
			row = BS_Emulators.Current.row
		End If

		Dim hasChanges As Boolean = False

		If DS_ML.src_frm_Emulators_Moby_Platforms.GetChanges IsNot Nothing Then
			Debug.WriteLine("	Change detected in DS_ML.src_frm_Emulators_Moby_Platforms.GetChanges")
			hasChanges = True
		End If

		If row IsNot Nothing AndAlso row.RowState = DataRowState.Added Then
			Debug.WriteLine("	Change detected in row (added)")
			hasChanges = True
		End If

		If row IsNot Nothing AndAlso row.RowState = DataRowState.Modified Then
			Debug.WriteLine("	Change detected in row (modified)")
			hasChanges = True
		End If

		If _J2KPreset_Original IsNot Nothing AndAlso _J2KPreset_Original IsNot DBNull.Value AndAlso Not Equals(_J2KPreset_Original, cmb_J2K_Config.Text) Then
			Debug.WriteLine("	Change detected in J2K (1)")
			hasChanges = True
		End If

		If (_J2KPreset_Original Is Nothing OrElse _J2KPreset_Original Is DBNull.Value) AndAlso cmb_J2K_Config.EditValue IsNot Nothing AndAlso cmb_J2K_Config.EditValue IsNot DBNull.Value Then
			Debug.WriteLine("	Change detected in J2K (2)")
			hasChanges = True
		End If

		If DS_ML.src_frm_Emulators_DOSBox_Patches.GetChanges IsNot Nothing Then
			Debug.WriteLine("	Change detected in DS_ML.src_frm_Emulators_DOSBox_Patches.GetChanges")
			hasChanges = True
		End If

		If DS_ML.tbl_Emulators_Multivolume_Parameters.GetChanges IsNot Nothing Then
			Debug.WriteLine("	Change detected in DS_ML.tbl_Emulators_Multivolume_Parameters.GetChanges")
			hasChanges = True
		End If

		If hasChanges Then
			Dim res As DialogResult = MKDXHelper.MessageBox("Save the changes for the current emulator?", "Save changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
			Return res
		End If

		Return Windows.Forms.DialogResult.None
	End Function

	Private Sub gv_Emulators_BeforeLeaveRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles gv_Emulators.BeforeLeaveRow
		If gv_Emulators.GetRow(e.RowHandle) Is Nothing Then Return
		Dim row As DS_ML.tbl_EmulatorsRow = gv_Emulators.GetRow(e.RowHandle).Row
		Select Case CheckSave(row)
			Case Windows.Forms.DialogResult.Yes
				If Not Save() Then
					e.Allow = False
				End If
			Case Windows.Forms.DialogResult.No
				row.RejectChanges()
			Case Windows.Forms.DialogResult.Cancel
				e.Allow = False
		End Select
	End Sub

	Private Sub btn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Close.Click
		Me.Close()
	End Sub

	Private Sub Handle_Add_Emulator(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Add_Emulator.Click, bbi_Add.ItemClick
		If BS_Emulators.Current IsNot Nothing Then
			Select Case CheckSave(BS_Emulators.Current.Row)
				Case Windows.Forms.DialogResult.Yes
					If Not Save() Then
						Return
					End If
					BS_Emulators.Current.Row.AcceptChanges()
				Case Windows.Forms.DialogResult.No
					BS_Emulators.Current.Row.RejectChanges()
				Case Windows.Forms.DialogResult.Cancel
					Return
			End Select
		End If

		Dim sFullPath As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("New Emulator", "Executables (*.exe;*.bat;*.cmd;*.lnk)|*.exe;*.bat;*.cmd;*.lnk", ParentForm:=Me)

		If Not Alphaleonis.Win32.Filesystem.File.Exists(sFullPath) Then
			Return
		End If

		Dim row As DS_ML.tbl_EmulatorsRow = Me.DS_ML.tbl_Emulators.NewRow
		row("InstallDirectory") = Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(sFullPath)
		row("Executable") = Alphaleonis.Win32.Filesystem.Path.GetFileName(sFullPath)
		row("Displayname") = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(sFullPath)

		Me.DS_ML.tbl_Emulators.Rows.Add(row)

		BS_Emulators.Position = BS_Emulators.Find("id_Emulators", row("id_Emulators"))

		AutoConfigure(False)
	End Sub

	Private Sub Handle_Delete_Emulator(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Delete_Emulator.Click, bbi_Delete.ItemClick
		If MKDXHelper.MessageBox("Do you really want to remove the emulator and its settings?", "Delete Emulator", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
			Dim id_Emulators = BS_Emulators.Current("id_Emulators")
			Me.BS_Emulators.RemoveCurrent()

			If id_Emulators > 0 Then
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Emulators WHERE id_Emulators = " & TC.getSQLFormat(id_Emulators))
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Emulators_Moby_Platforms WHERE id_Emulators = " & TC.getSQLFormat(id_Emulators))
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Emulators_Multivolume_Parameters WHERE id_Emulators = " & TC.getSQLFormat(id_Emulators))
			End If
		End If
	End Sub

	Private Function Save() As Boolean
		If Me.DS_ML.src_frm_Emulators_Moby_Platforms.Select("Supported = 1").Length = 0 Then
			If Not MKDXHelper.MessageBox("It seems you didn't choose one or more supported platform/s in the list. Do you still want to save?", "Missing Supported Platform/s", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
				Return False
			End If
		End If

		If isRetroArch() AndAlso TC.IsNullNothingOrEmpty(cmb_Libretro_Core.EditValue) Then
			If Not MKDXHelper.MessageBox("The emulator appears to be RetroArch. Please consider to choose a Libretro Core. Do you still want to save?", "Missing Libretro Core", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
				Return False
			End If
		End If

		If Me.DS_ML.tbl_Emulators_Multivolume_Parameters.Rows.Count > 0 AndAlso Not Me.txb_StartupParameter.Text.ToLower.Contains("%multivolume%") Then
			If Not MKDXHelper.MessageBox("There are startup parameters defined in the Multiple Volumes tab. These can only be used if you put %multivolume% in the Startup Parameter field in the Settings tab. Do you still want to save?", "Missing Startup Parameter", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
				Return False
			End If
		End If

		If Me.txb_StartupParameter.Text.ToLower.Contains("%listfile") AndAlso TC.NZ(Me.cmb_List_Generator.EditValue, 0) = 0 Then
			If Not MKDXHelper.MessageBox("You apparently want to generate and use a list file as a Startup Parameter, but you didn't chose a List Generator. Do you still want to save?", "Missing Libretro Core", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
				Return False
			End If
		End If

		If TC.NZ(Me.cmb_List_Generator.EditValue, 0) > 0 AndAlso Not Me.txb_StartupParameter.Text.ToLower.Contains("%listfile") Then
			If Not MKDXHelper.MessageBox("You chose a List Generator but you didn't provide the %listfile.ext% variable in the Startup Parameter. Do you still want to save?", "Missing Libretro Core", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
				Return False
			End If
		End If

		Cursor.Current = Cursors.WaitCursor

		'Save Platform Settings for the current Emulator
		Dim id_Emulators_Current As Integer = BS_Emulators.Current("id_Emulators")
		Dim rowCurrent As DataRow = BS_Emulators.Current.Row

		If cmb_J2K_Config.EditValue IsNot DBNull.Value AndAlso cmb_J2K_Config.EditValue IsNot Nothing Then
			rowCurrent("J2KPreset") = cmb_J2K_Config.Text
		Else
			rowCurrent("J2KPreset") = DBNull.Value
		End If

		rowCurrent("Libretro_Core") = cmb_Libretro_Core.EditValue

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			Try
				If id_Emulators_Current < 0 Then
					id_Emulators_Current = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "INSERT INTO tbl_Emulators(Displayname, InstallDirectory, Executable, StartupParameter, AutoItScript, J2KPreset, ScreenshotDirectory, Libretro_Core, id_List_Generators) VALUES (" & TC.getSQLParameter(rowCurrent("Displayname"), rowCurrent("InstallDirectory"), rowCurrent("Executable"), rowCurrent("StartupParameter"), rowCurrent("AutoItScript"), rowCurrent("J2KPreset"), rowCurrent("ScreenshotDirectory"), rowCurrent("Libretro_Core"), rowCurrent("id_List_Generators")) & "); SELECT last_insert_rowid()", tran)
					rowCurrent("id_Emulators") = id_Emulators_Current
				Else
					DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Emulators SET Displayname = " & TC.getSQLFormat(rowCurrent("Displayname")) & ", InstallDirectory = " & TC.getSQLFormat(rowCurrent("InstallDirectory")) & ", Executable = " & TC.getSQLFormat(rowCurrent("Executable")) & ", StartupParameter = " & TC.getSQLFormat(rowCurrent("StartupParameter")) & ", AutoItScript = " & TC.getSQLFormat(rowCurrent("AutoItScript")) & ", J2KPreset = " & TC.getSQLFormat(rowCurrent("J2KPreset")) & ", ScreenshotDirectory = " & TC.getSQLFormat(rowCurrent("ScreenshotDirectory")) & ", Libretro_Core = " & TC.getSQLFormat(rowCurrent("Libretro_Core")) & ", id_List_Generators = " & TC.getSQLFormat(rowCurrent("id_List_Generators")) & " WHERE id_Emulators = " & TC.getSQLFormat(id_Emulators_Current), tran)
				End If
				rowCurrent.AcceptChanges()

				DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emulators_Moby_Platforms WHERE id_Emulators = " & TC.getSQLFormat(id_Emulators_Current), tran)
				For Each row As DataRow In DS_ML.src_frm_Emulators_Moby_Platforms.Rows
					If TC.NZ(row("Supported"), False) = True Then
						If TC.NZ(row("DefaultEmulator"), False) = True Then
							DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Emulators_Moby_Platforms SET DefaultEmulator = 0 WHERE id_Moby_Platforms = " & row("id_Moby_Platforms"), tran)
						End If
						DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Emulators_Moby_Platforms (id_Emulators, id_Moby_Platforms, DefaultEmulator) VALUES (" & TC.getSQLParameter(id_Emulators_Current, row("id_Moby_Platforms"), row("DefaultEmulator")) & ")", tran)
					End If
					row.AcceptChanges()
				Next
				DS_ML.src_frm_Emulators_Moby_Platforms.AcceptChanges()

				DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emulators_Multivolume_Parameters WHERE id_Emulators = " & TC.getSQLFormat(id_Emulators_Current), tran)
				For Each row As DataRow In DS_ML.tbl_Emulators_Multivolume_Parameters.Rows
					If row.RowState <> DataRowState.Deleted AndAlso row.RowState <> DataRowState.Detached Then
						DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Emulators_Multivolume_Parameters (id_Emulators, Volume_Number, Parameter) VALUES (" & TC.getSQLParameter(id_Emulators_Current, row("Volume_Number"), row("Parameter")) & ")", tran)
						row.AcceptChanges()
					End If
				Next
				DS_ML.tbl_Emulators_Multivolume_Parameters.AcceptChanges()

				DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emulators_DOSBox_Patches WHERE id_Emulators = " & TC.getSQLFormat(id_Emulators_Current), tran)
				For Each row As DataRow In DS_ML.src_frm_Emulators_DOSBox_Patches.Rows
					If row.RowState <> DataRowState.Deleted AndAlso row.RowState <> DataRowState.Detached Then
						Dim sSQL As String = "INSERT INTO tbl_Emulators_DOSBox_Patches (id_Emulators, id_DOSBox_Patches, Activated) VALUES ("
						sSQL &= TC.getSQLFormat(id_Emulators_Current)
						sSQL &= ", " & TC.getSQLFormat(row("id_DOSBox_Patches"))
						sSQL &= ", " & TC.getSQLFormat(TC.NZ(row("Activated"), False))
						sSQL &= ")"
						DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
						row.AcceptChanges()
					End If
				Next
				DS_ML.src_frm_Emulators_DOSBox_Patches.AcceptChanges()

				tran.Commit()
			Catch ex As Exception
				tran.Rollback()
			End Try
		End Using

		Cursor.Current = Cursors.Default

		Return True
	End Function

	Private Sub btn_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save.Click
		Save()
	End Sub

	Private Sub btn_EmulatorFileOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_EmulatorFileOpen.Click
		Dim sFullPath As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("Open Emulator", "Executables (*.exe;*.bat;*.cmd;*.lnk)|*.exe;*.bat;*.cmd;*.lnk", InitialDirectory:=Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(txb_Directory.Text), ParentForm:=Me)

		If Not Alphaleonis.Win32.Filesystem.File.Exists(sFullPath) Then
			Return
		End If

		Dim row As DataRow = BS_Emulators.Current.Row
		row("InstallDirectory") = Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(sFullPath)
		row("Executable") = Alphaleonis.Win32.Filesystem.Path.GetFileName(sFullPath)

		Refill_LibretroCore()
	End Sub

	Private Sub Refill_List_Generators()
		Me.DS_ML.tbl_List_Generators.Clear()
		DS_ML.Fill_tbl_List_Generators(Me.DS_ML.tbl_List_Generators)
	End Sub

	Private Sub Refill_LibretroCore()
		If Not isRetroArch() Then
			lbl_Libretro_Core.Visible = False
			cmb_Libretro_Core.Visible = False
			cmb_Libretro_Core.EditValue = Nothing
			Return
		End If

		Dim oLastCore As Object = BS_Emulators.Current("Libretro_Core") 'cmb_Libretro_Core.EditValue
		BTA_Libretro_Core.Table.Clear()

		Dim basedir As String = MKNetLib.cls_MKStringSupport.Clean_Right(BS_Emulators.Current("InstallDirectory"), "\") ' txb_Directory.Text
		Dim coredir As String = basedir & "\cores"
		Dim infodir As String = basedir & "\info"

		lbl_Libretro_Core.Visible = True
		cmb_Libretro_Core.Visible = True

		If Alphaleonis.Win32.Filesystem.Directory.Exists(coredir) Then
			Dim files As String() = Alphaleonis.Win32.Filesystem.Directory.GetFiles(coredir, "*", IO.SearchOption.TopDirectoryOnly)
			For Each file As String In files
				If Alphaleonis.Win32.Filesystem.Path.GetExtension(file).ToLower = ".dll" Then
					Dim row_file As DataRow = BTA_Libretro_Core.Table.NewRow
					row_file("DLL") = Alphaleonis.Win32.Filesystem.Path.GetFileName(file)
					row_file("Displayname") = Alphaleonis.Win32.Filesystem.Path.GetFileName(file)

					'TODO: find Displayname in RetroArch DB if possible
					Dim infofile As String = infodir & "\" & Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(file) & ".info"
					If Alphaleonis.Win32.Filesystem.File.Exists(infofile) Then
						Dim sContent As String = MKNetLib.cls_MKFileSupport.GetFileContents(infofile)
						If Not TC.IsNullNothingOrEmpty(sContent) Then
							For Each line As String In sContent.Split(vbLf)
								If line.Contains("display_name") Then
									If MKNetLib.cls_MKRegex.IsMatch(line, """(.*?)""") Then
										row_file("Displayname") = MKNetLib.cls_MKRegex.GetMatches(line, """(.*?)""")(0).Groups(1).Captures(0).Value
									End If
								End If
							Next
						End If
					End If

					BTA_Libretro_Core.Table.Rows.Add(row_file)
				End If
			Next
		End If

		If BTA_Libretro_Core.Find("DLL", oLastCore) >= 0 Then
			cmb_Libretro_Core.EditValue = oLastCore
			BTA_Libretro_Core.SetBindingSourcePosition("DLL", oLastCore)
		End If
	End Sub

	Private Sub btn_ScreenshotDirectoryOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ScreenshotDirectoryOpen.Click
		Dim sDir As String = MKNetLib.cls_MKFileSupport.OpenFolderDialog("", True)

		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(sDir) Then
			Return
		End If

		BS_Emulators.Current.Row("ScreenshotDirectory") = sDir
	End Sub

	Private Sub rpi_DefaultEmulator_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rpi_DefaultEmulator.CheckedChanged
		Dim row As DataRow = BS_Platforms.Current.Row

		Dim rv As DataRowView = gv_Platforms.GetRow(gv_Platforms.FocusedRowHandle)

		If sender.Checked = True Then
			rv.Item("Supported") = True
			gv_Platforms.RefreshData()
		End If
	End Sub

	Private Sub rpi_Supported_CheckedChanged(sender As Object, e As System.EventArgs) Handles rpi_Supported.CheckedChanged
		Dim row As DataRow = BS_Platforms.Current.Row

		Dim rv As DataRowView = gv_Platforms.GetRow(gv_Platforms.FocusedRowHandle)

		If sender.Checked = False Then
			rv.Item("DefaultEmulator") = False
			gv_Platforms.RefreshData()
		End If
	End Sub

	Private Sub frm_Emulators_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
		If BS_Emulators.Current IsNot Nothing Then
			Dim row As DS_ML.tbl_EmulatorsRow = BS_Emulators.Current.Row

			BS_Emulators.EndEdit()

			Select Case CheckSave()
				Case Windows.Forms.DialogResult.Yes
					If Not Save() Then
						e.Cancel = True
					End If
					row.AcceptChanges()
				Case Windows.Forms.DialogResult.No
					row.RejectChanges()
				Case Windows.Forms.DialogResult.Cancel
					e.Cancel = True
			End Select
		End If
	End Sub

	Private Sub btn_MV_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_MV_Delete.Click
		If BS_MV.Current IsNot Nothing Then
			BS_MV.RemoveCurrent()
		End If
	End Sub

	Private Sub btn_MV_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_MV_Add.Click
		If BS_Emulators.Current Is Nothing Then Return

		Dim MaxVol As Int64 = 0L
		For Each row As DataRow In Me.DS_ML.tbl_Emulators_Multivolume_Parameters.Rows
			If TC.NZ(row("Volume_Number"), 0L) > MaxVol Then MaxVol = row("Volume_Number")
		Next

		Dim newrow As DataRow = Me.DS_ML.tbl_Emulators_Multivolume_Parameters.NewRow
		newrow("Volume_Number") = MaxVol + 1L
		Me.DS_ML.tbl_Emulators_Multivolume_Parameters.Rows.Add(newrow)
	End Sub

	Private Sub popmnu_Emulators_BeforePopup(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles popmnu_Emulators.BeforePopup
		If Not grd_Emulators.Allow_Popup Then
			e.Cancel = True
			Return
		End If

		bbi_Add.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
		bbi_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		bbi_Duplicate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

		If BS_Emulators.Current IsNot Nothing Then
			bbi_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
			bbi_Duplicate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
		End If
	End Sub

	Private Sub Handle_Duplicate_Emulator(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Duplicate_Emulator.Click, bbi_Duplicate.ItemClick
		If BS_Emulators.Current Is Nothing Then Return

		Dim row As DS_ML.tbl_EmulatorsRow = BS_Emulators.Current.Row

		BS_Emulators.EndEdit()

		If DS_ML.src_frm_Emulators_Moby_Platforms.GetChanges IsNot Nothing OrElse row.RowState = DataRowState.Added OrElse row.RowState = DataRowState.Modified Then
			MKDXHelper.MessageBox("Please save your changes before duplicating an emulator setting.", "Duplicate Emulator", MessageBoxButtons.OK, MessageBoxIcon.Information)
			Return
		End If

		Dim id_Emulators = BS_Emulators.Current("id_Emulators")

		If id_Emulators > 0 Then
			Cursor.Current = Cursors.WaitCursor

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction()
				Try

					Dim id_Emulators_New As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "INSERT INTO tbl_Emulators (Displayname, InstallDirectory, Executable, StartupParameter, AutoItScript, J2KPreset, ScreenshotDirectory, Libretro_Core) SELECT Displayname || ' Copy', InstallDirectory, Executable, StartupParameter, AutoItScript, J2KPreset, ScreenshotDirectory, Libretro_Core FROM tbl_Emulators WHERE id_Emulators = " & TC.getSQLFormat(id_Emulators) & "; SELECT last_insert_rowid()", tran), 0)
					If id_Emulators_New > 0 Then
						DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Emulators_Moby_Platforms (id_Emulators, id_Moby_Platforms, DefaultEmulator) SELECT " & TC.getSQLFormat(id_Emulators_New) & ", id_Moby_Platforms, NULL FROM tbl_Emulators_Moby_Platforms WHERE id_Emulators = " & TC.getSQLFormat(id_Emulators), tran)
						DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Emulators_Multivolume_Parameters (id_Emulators, Volume_Number, Parameter) SELECT " & TC.getSQLFormat(id_Emulators_New) & ", Volume_Number, Parameter FROM tbl_Emulators_Multivolume_Parameters WHERE id_Emulators = " & TC.getSQLFormat(id_Emulators), tran)

						frm_Tag_Parser_Edit.Fill_Tag_Parser_Volumes(Me.DS_ML.ttb_Tag_Parser_Volumes)

						Me.DS_ML.Fill_src_frm_Emulators(tran, Me.DS_ML.tbl_Emulators)

						Me.BS_Emulators.Position = Me.BS_Emulators.Find("id_Emulators", id_Emulators_New)
					End If

					tran.Commit()
				Catch ex As Exception
					tran.Rollback()
					MKDXHelper.ExceptionMessageBox(ex, Caption:="Duplicate Emulator")
				End Try
			End Using

			Cursor.Current = Cursors.Default
		End If
	End Sub

	Private Sub cmb_J2K_Config_ButtonPressed(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_J2K_Config.ButtonPressed
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
			cmb_J2K_Config.EditValue = DBNull.Value
		End If
	End Sub

	Private Sub btn_Run_Click(sender As System.Object, e As System.EventArgs) Handles btn_Run.Click
		Dim fullpath As String = txb_Directory.Text & "\" & txb_Executable.Text
		If Not Alphaleonis.Win32.Filesystem.File.Exists(fullpath) Then
			MKDXHelper.MessageBox("Error while launching the emulator, file '" & fullpath & "' not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		Else
			Try
				Dim proc = New System.Diagnostics.Process
				proc.StartInfo.FileName = fullpath
				proc.StartInfo.WorkingDirectory = txb_Directory.Text
				proc.StartInfo.UseShellExecute = True
				proc.Start()
			Catch ex As Exception
				MKDXHelper.ExceptionMessageBox(ex)
			End Try
		End If
	End Sub

	Private Sub BS_DOSBox_Patches_Categories_CurrentChanged(sender As Object, e As System.EventArgs) Handles BS_DOSBox_Patches_Categories.CurrentChanged
		If BS_DOSBox_Patches_Categories.Current Is Nothing Then
			BS_DOSBox_Patches.Filter = "id_DOSBox_Patches_Categories = 0"
		Else
			BS_DOSBox_Patches.Filter = "id_DOSBox_Patches_Categories = " & BS_DOSBox_Patches_Categories.Current("id_DOSBox_Patches_Categories")
		End If
	End Sub

	Private Sub txb_Executable_Leave(sender As Object, e As EventArgs) Handles txb_Executable.Leave
		Refill_LibretroCore()
	End Sub

	Private Sub txb_Directory_Leave(sender As Object, e As EventArgs) Handles txb_Directory.Leave
		Refill_LibretroCore()
	End Sub

	Private Sub cmb_Libretro_Core_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_Libretro_Core.ButtonPressed
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
			cmb_Libretro_Core.EditValue = DBNull.Value
		End If
	End Sub

	Private Sub gv_DOSBox_Patches_MouseMove(sender As Object, e As MouseEventArgs) Handles gv_DOSBox_Patches.MouseMove
		Me.grd_DOSBox_Patches.ShowHandInColumns(gv_DOSBox_Patches, {"Activated"}, e)
	End Sub

	Private Sub gv_Platforms_MouseMove(sender As Object, e As MouseEventArgs) Handles gv_Platforms.MouseMove
		Me.grd_Platforms.ShowHandInColumns(gv_Platforms, {"Supported", "DefaultEmulator"}, e)
	End Sub

	Private Sub cmb_List_Generator_ButtonPressed(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_List_Generator.ButtonPressed
		BS_Emulators.EndEdit()

		Select Case e.Button.Kind
			Case DevExpress.XtraEditors.Controls.ButtonPredefines.Plus
				'Add List Generator
				Using frm As New frm_List_Generator_Edit
					If frm.ShowDialog = DialogResult.OK Then
						'TODO: Save to tbl_List_Generators, set id_List_Generators within this Emulator and reload the list of List_Generators
						Dim id_List_Generators As Int64 = 0L
						Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
							id_List_Generators = DS_ML.Upsert_tbl_List_Generators(tran, frm.txb_Name.EditValue.Trim, frm.cmb_Sort.EditValue, frm.txb_Main_Template.EditValue, frm.txb_File_Entry_Template.EditValue)
							tran.Commit()
						End Using
						Refill_List_Generators()
						If id_List_Generators > 0L Then
							Me.BS_Emulators.Current("id_List_Generators") = id_List_Generators
						End If
					End If
				End Using
			Case DevExpress.XtraEditors.Controls.ButtonPredefines.Minus
				'Delete List Generator
				If TC.NZ(Me.BS_Emulators.Current("id_List_Generators"), 0) = 0 Then
					Return
				End If

				If TC.NZ(Me.BS_Emulators.Current("id_List_Generators"), 0) < 0 Then
					MKDXHelper.MessageBox("The selected list generator cannot be deleted because it is shipped with Metropolis Launcher.", "Delete List Generator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Return
				End If

				Dim id_List_Generators As Int64 = Me.BS_Emulators.Current("id_List_Generators")

				If MKDXHelper.MessageBox("Do you really want to delete this list generator?", "Delete List Generator", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> DialogResult.Yes Then
					Return
				End If


				Dim sSQL As String = ""
				sSQL &= "DELETE FROM tbl_List_Generators WHERE id_List_Generators = " & TC.getSQLFormat(id_List_Generators)
				sSQL &= "; UPDATE tbl_Emulators SET id_List_Generators = NULL WHERE id_List_Generators NOT IN (SELECT id_List_Generators FROM tbl_List_Generators)"

				DataAccess.FireProcedure(cls_Globals.Conn, 0, sSQL)

				For Each row As DataRow In Me.DS_ML.tbl_Emulators.Rows
					If TC.NZ(row("id_List_Generators"), 0) = id_List_Generators Then
						row("id_List_Generators") = DBNull.Value
					End If
				Next

				Refill_List_Generators()
			Case DevExpress.XtraEditors.Controls.ButtonPredefines.Delete
				'Set to NULL
				Me.BS_Emulators.Current("id_List_Generators") = DBNull.Value
			Case DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis
				'Edit List Generator
				If BS_List_Generators.Current Is Nothing Then
					Return
				End If
				Using frm As New frm_List_Generator_Edit(BS_List_Generators.Current("id_List_Generators"), TC.NZ(BS_List_Generators.Current("Name"), ""), TC.NZ(BS_List_Generators.Current("Sort"), 1L), TC.NZ(BS_List_Generators.Current("Main_Template"), ""), TC.NZ(BS_List_Generators.Current("File_Entry_Template"), ""))
					If frm.ShowDialog = DialogResult.OK Then
						If BS_List_Generators.Current("id_List_Generators") > 0 Then
							Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
								DS_ML.Upsert_tbl_List_Generators(tran, frm.txb_Name.EditValue.Trim, frm.cmb_Sort.EditValue, frm.txb_Main_Template.EditValue, frm.txb_File_Entry_Template.EditValue, Me.BS_List_Generators.Current("id_List_Generators"))
								tran.Commit()
							End Using
							Refill_List_Generators()
						End If

						If BS_List_Generators.Current("id_List_Generators") < 0 Then
							'Add (a shipped List Generator was edited)
							Dim id_List_Generators As Int64 = 0L
							Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
								id_List_Generators = DS_ML.Upsert_tbl_List_Generators(tran, frm.txb_Name.EditValue.Trim, frm.cmb_Sort.EditValue, frm.txb_Main_Template.EditValue, frm.txb_File_Entry_Template.EditValue)
								tran.Commit()
							End Using
							Refill_List_Generators()
							If id_List_Generators > 0L Then
								Me.BS_Emulators.Current("id_List_Generators") = id_List_Generators
							End If
						End If
					End If
				End Using
		End Select
	End Sub

#Region "Common Emulators"
	Private Function DetectEmu() As DS_Rombase.tbl_Rombase_Known_EmulatorsDataTable
		Dim dtResult As New DS_Rombase.tbl_Rombase_Known_EmulatorsDataTable

		If BS_Emulators.Current Is Nothing Then
			Return dtResult
		End If

		Dim exename = TC.NZ(BS_Emulators.Current("Executable"), "").ToLower

		If exename = "" Then
			Return dtResult
		End If

		Dim dt As New DS_Rombase.tbl_Rombase_Known_EmulatorsDataTable

		For Each row_Known_Emulator As DS_Rombase.tbl_Rombase_Known_EmulatorsRow In Me.DS_Rombase.tbl_Rombase_Known_Emulators.Rows
			Dim known_Regex As String = TC.NZ(row_Known_Emulator("Exe_Identifier_Regex"), "").ToLower

			If MKNetLib.cls_MKRegex.IsMatch(exename, known_Regex) Then
				dtResult.ImportRow(row_Known_Emulator)
			End If
		Next

		Return dtResult
	End Function

	Private Sub btn_AutoConfig_Click(sender As Object, e As EventArgs) Handles btn_AutoConfig.Click
		AutoConfigure(True)
	End Sub

	Private Sub AutoConfigure(ByVal Ask As Boolean)
		Dim dtKnownEmulators = DetectEmu()

		If dtKnownEmulators.Rows.Count = 0 Then
			If Ask Then
				MKDXHelper.MessageBox("The emulator couldn't be identified. An automatic configuration is not possible.", "Autoconfigure this Emulator", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			End If
			Return
		End If

		If Ask Then
			If MKDXHelper.MessageBox("By performing the automatic configuration some settings will be overwritten. Do you want to continue?", "Autoconfig", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> DialogResult.Yes Then
				Return
			End If
		End If

		Dim id_Rombase_Known_Emulators As Int64 = 0
		Dim row_Rombase_Known_Emulators As DS_Rombase.tbl_Rombase_Known_EmulatorsRow = Nothing

		If dtKnownEmulators.Rows.Count = 1 Then
			row_Rombase_Known_Emulators = dtKnownEmulators.Rows(0)
		End If

		If dtKnownEmulators.Rows.Count > 1 Then
			Using frm As New frm_Select_Known_Emulator(dtKnownEmulators)
				If frm.ShowDialog = DialogResult.OK Then
					row_Rombase_Known_Emulators = frm.Selected_Known_Emulator_Row
				End If
			End Using
		End If

		If row_Rombase_Known_Emulators Is Nothing Then
			Return
		End If

		id_Rombase_Known_Emulators = row_Rombase_Known_Emulators("id_Rombase_Known_Emulators")

		'In any case: we reset the platforms
		Reset_Platforms()
		BS_Emulators.Current("ScreenshotDirectory") = DBNull.Value
		BS_Emulators.Current("StartupParameter") = """%romfullpath%"""
		BS_Emulators.Current("id_List_Generators") = DBNull.Value
		BS_Emulators.Current("Libretro_Core") = DBNull.Value
		Me.DS_ML.tbl_Emulators_Multivolume_Parameters.Clear()

		If Not TC.IsNullNothingOrEmpty(row_Rombase_Known_Emulators("Name")) Then
			BS_Emulators.Current("Displayname") = row_Rombase_Known_Emulators("Name")
		End If

		If Not TC.IsNullNothingOrEmpty(row_Rombase_Known_Emulators("StartupParameter")) Then
			BS_Emulators.Current("StartupParameter") = row_Rombase_Known_Emulators("StartupParameter")
		End If

		If Not TC.IsNullNothingOrEmpty(row_Rombase_Known_Emulators("ScreenshotDirectory")) Then
			Dim screenshotdir As String = row_Rombase_Known_Emulators("ScreenshotDirectory")
			screenshotdir = MKNetLib.cls_MKStringSupport.Clean_Right(TC.NZ(BS_Emulators.Current("InstallDirectory"), ""), "\") & "\" & MKNetLib.cls_MKStringSupport.Clean_Right(MKNetLib.cls_MKStringSupport.Clean_Left(screenshotdir, "\"), "\")
			If Alphaleonis.Win32.Filesystem.Directory.Exists(screenshotdir) Then
				BS_Emulators.Current("ScreenshotDirectory") = screenshotdir
			End If
		End If

		If Not TC.IsNullNothingOrEmpty(row_Rombase_Known_Emulators("id_List_Generators")) Then
			BS_Emulators.Current("id_List_Generators") = row_Rombase_Known_Emulators("id_List_Generators")
		End If

		Me.DS_Rombase.Fill_src_frm_Known_Emulators_Moby_Platforms(Me.DS_Rombase.src_frm_Known_Emulators_Moby_Platforms, id_Rombase_Known_Emulators)
		Me.DS_Rombase.Fill_src_frm_Known_Emulators_Multivolume_Parameters(Me.DS_Rombase.tbl_Rombase_Known_Emulators_Multivolume_Parameters, id_Rombase_Known_Emulators)

		'Autoconfigure Platforms
		Dim bSetDefault As Boolean = False

		If Me.DS_Rombase.src_frm_Known_Emulators_Moby_Platforms.Select("Supported = 1").Length > 0 Then
			bSetDefault = (MKDXHelper.MessageBox("Do you want this emulator to be the default emulator for the platforms it supports?", "Autoconfig", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes)
		End If

		For Each rowKnownEmuPlatforms As DS_Rombase.src_frm_Known_Emulators_Moby_PlatformsRow In Me.DS_Rombase.src_frm_Known_Emulators_Moby_Platforms.Rows
			If TC.NZ(rowKnownEmuPlatforms("Supported"), False) = True Then
				For Each rowPlatform As DS_ML.src_frm_Emulators_Moby_PlatformsRow In Me.DS_ML.src_frm_Emulators_Moby_Platforms.Rows
					If rowPlatform("id_Moby_Platforms") = rowKnownEmuPlatforms("id_Moby_Platforms") Then
						rowPlatform("Supported") = True
						If bSetDefault Then
							rowPlatform("DefaultEmulator") = True
						End If
					End If
				Next
			End If
		Next

		'Multivolume Parameters
		For Each rowKnownEmuMultivolume As DS_Rombase.tbl_Rombase_Known_Emulators_Multivolume_ParametersRow In Me.DS_Rombase.tbl_Rombase_Known_Emulators_Multivolume_Parameters.Rows
			Dim row_MV As DS_ML.tbl_Emulators_Multivolume_ParametersRow = Me.DS_ML.tbl_Emulators_Multivolume_Parameters.NewRow

			row_MV.id_Emulators = BS_Emulators.Current("id_Emulators")
			row_MV("Parameter") = rowKnownEmuMultivolume("Parameter")
			row_MV("Volume_Number") = rowKnownEmuMultivolume("Volume_Number")

			Me.DS_ML.tbl_Emulators_Multivolume_Parameters.Rows.Add(row_MV)
		Next

		Me.BS_Emulators.EndEdit()

		Dim sMessage As String = ""
		sMessage &= "The auto configuration of " & row_Rombase_Known_Emulators("Name") & " was successful."

		If Not TC.IsNullNothingOrEmpty(row_Rombase_Known_Emulators("Autoconfig_Note")) Then
			sMessage &= ControlChars.CrLf
			sMessage &= ControlChars.CrLf
			sMessage &= "IMPORTANT: " & row_Rombase_Known_Emulators("Autoconfig_Note")
		End If

		MKDXHelper.MessageBox(sMessage, "Autoconfig", MessageBoxButtons.OK, MessageBoxIcon.Information)
	End Sub

	Private Sub Reset_Platforms()
		For Each row As DS_ML.src_frm_Emulators_Moby_PlatformsRow In Me.DS_ML.src_frm_Emulators_Moby_Platforms.Rows
			If TC.NZ(row("Supported"), False) = True Then
				row("Supported") = False
			End If

			If TC.NZ(row("DefaultEmulator"), False) = True Then
				row("DefaultEmulator") = False
			End If
		Next
	End Sub

	Private Sub cmb_List_Generator_EditValueChanged(sender As Object, e As EventArgs) Handles cmb_List_Generator.EditValueChanged
		Dim edutButtonsEnabled = TC.NZ(Me.cmb_List_Generator.EditValue, 0L) <> 0

		For Each btn As DevExpress.XtraEditors.Controls.EditorButton In Me.cmb_List_Generator.Properties.Buttons()
			If {DevExpress.XtraEditors.Controls.ButtonPredefines.Minus, DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, DevExpress.XtraEditors.Controls.ButtonPredefines.Delete}.Contains(btn.Kind) Then
				btn.Enabled = edutButtonsEnabled
			End If
		Next
	End Sub
#End Region
End Class
