Imports System.ComponentModel

Public Class frm_Emulators
	Public Sub New()
		InitializeComponent()

		barmng.SetPopupContextMenu(grd_Emulators, popmnu_Emulators)

		Cursor.Current = Cursors.WaitCursor

		frm_Tag_Parser_Edit.Fill_Tag_Parser_Volumes(Me.DS_ML.ttb_Tag_Parser_Volumes)

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction()
			Me.DS_MobyDB.Fill_tbl_Moby_Platforms(tran, Me.DS_MobyDB.tbl_Moby_Platforms, True)
			Me.DS_ML.Fill_src_frm_Emulators(tran, Me.DS_ML.tbl_Emulators)
		End Using

		'Fill the J2K Config DS
		cls_Settings.Fill_J2K_DS(Me.DS_J2K, TC.NZ(cls_Settings.GetSetting("Path_J2K"), ""))

		Cursor.Current = Cursors.Default
	End Sub

	Private _J2KPreset_Original As Object

	Private Function isRetroArch() As Boolean
		If BS_Emulators.Current Is Nothing Then Return False
		Return txb_Executable.Text.ToLower.Contains("retroarch")
	End Function

	Private Function isDOSBox() As Boolean
		If BS_Emulators.Current Is Nothing Then Return False
		Return BS_Emulators.Current("Executable").ToLower.Contains("dosbox")
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
				End If

				For Each row_patch As DataRow In DS_ML.src_frm_Emulators_DOSBox_Patches.Rows
					If TC.IsNullNothingOrEmpty(row_patch("Activated")) Then
						Select Case dosbox_build
							Case enm_DOSBox_Builds.DAUM
								row_patch("Activated") = TC.NZ(row_patch("DAUM_Supported"), False)
							Case enm_DOSBox_Builds.MB
								row_patch("Activated") = TC.NZ(row_patch("MB_Supported"), False)
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

		Me.DS_ML.tbl_Emulators_Moby_Platforms.Clear()

		If BS_Emulators.Current Is Nothing Then
			'TODO: gb_Emulator_Settings.Enabled = False
			btn_Duplicate_Emulator.Enabled = False
			btn_Delete_Emulator.Enabled = False
			cmb_J2K_Config.EditValue = DBNull.Value
		Else
			'TODO: gb_Emulator_Settings.Enabled = True
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
			Else
				txb_StartupParameter.Visible = True
				lbl_StartupParameter.Visible = True
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

		Cursor.Current = Cursors.Default
	End Sub

	Private Function CheckSave(Optional ByRef row As DataRow = Nothing) As DialogResult
		If DS_ML.src_frm_Emulators_Moby_Platforms.GetChanges IsNot Nothing _
		OrElse (row IsNot Nothing AndAlso row.RowState = DataRowState.Added) _
		OrElse (row IsNot Nothing AndAlso row.RowState = DataRowState.Modified) _
		OrElse (_J2KPreset_Original IsNot Nothing AndAlso _J2KPreset_Original IsNot DBNull.Value AndAlso Not Equals(_J2KPreset_Original, cmb_J2K_Config.Text)) _
		OrElse ((_J2KPreset_Original Is Nothing OrElse _J2KPreset_Original Is DBNull.Value) AndAlso (cmb_J2K_Config.EditValue IsNot Nothing AndAlso cmb_J2K_Config.EditValue IsNot DBNull.Value)) _
		OrElse DS_ML.src_frm_Emulators_DOSBox_Patches.GetChanges IsNot Nothing _
		OrElse DS_ML.src_frm_Emulators_Moby_Platforms.GetChanges IsNot Nothing _
		OrElse DS_ML.tbl_Emulators_Multivolume_Parameters.GetChanges IsNot Nothing Then
			Dim res As DialogResult = DevExpress.XtraEditors.XtraMessageBox.Show("Save the changes for the current emulator?", "Save changes?", MessageBoxButtons.YesNoCancel)
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
	End Sub

	Private Sub Handle_Delete_Emulator(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Delete_Emulator.Click, bbi_Delete.ItemClick
		If DevExpress.XtraEditors.XtraMessageBox.Show("Do you really want to remove the emulator and its settings?", "Delete Emulator", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
			Dim id_Emulators = BS_Emulators.Current("id_Emulators")
			Me.BS_Emulators.RemoveCurrent()

			Dim rows_Platforms() As DataRow = Me.DS_ML.tbl_Emulators_Moby_Platforms.Select("id_Emulators = " & id_Emulators)
			For Each row_Platforms In rows_Platforms
				Me.DS_ML.tbl_Emulators_Moby_Platforms.Rows.Remove(row_Platforms)
			Next

			If id_Emulators > 0 Then
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Emulators WHERE id_Emulators = " & TC.getSQLFormat(id_Emulators))
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Emulators_Moby_Platforms WHERE id_Emulators = " & TC.getSQLFormat(id_Emulators))
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Emulators_Multivolume_Parameters WHERE id_Emulators = " & TC.getSQLFormat(id_Emulators))
			End If
		End If
	End Sub

	Private Function Save() As Boolean
		If isRetroArch() AndAlso TC.IsNullNothingOrEmpty(cmb_Libretro_Core.EditValue) Then
			If Not DevExpress.XtraEditors.XtraMessageBox.Show("The emulator appears to be RetroArch. Please consider to choose a Libretro Core. Do you still want to save?", "Missing Libretro Core", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
				Return False
			End If
		End If

		If Me.DS_ML.tbl_Emulators_Multivolume_Parameters.Rows.Count > 0 AndAlso Not Me.txb_StartupParameter.Text.ToLower.Contains("%multivolume%") Then
			If Not DevExpress.XtraEditors.XtraMessageBox.Show("There are startup parameters defined in the Multiple Volumes tab. These can only be used if you put %multivolume% in the Startup Parameter field in the Settings tab. Do you still want to save?", "Missing Libretro Core", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
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
					id_Emulators_Current = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "INSERT INTO tbl_Emulators(Displayname, InstallDirectory, Executable, StartupParameter, AutoItScript, J2KPreset, ScreenshotDirectory, Libretro_Core) VALUES(" & TC.getSQLParameter(rowCurrent("Displayname"), rowCurrent("InstallDirectory"), rowCurrent("Executable"), rowCurrent("StartupParameter"), rowCurrent("AutoItScript"), rowCurrent("J2KPreset"), rowCurrent("ScreenshotDirectory"), rowCurrent("Libretro_Core")) & "); SELECT last_insert_rowid()", tran)
					rowCurrent("id_Emulators") = id_Emulators_Current
				Else
					DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Emulators SET Displayname = " & TC.getSQLFormat(rowCurrent("Displayname")) & ", InstallDirectory = " & TC.getSQLFormat(rowCurrent("InstallDirectory")) & ", Executable = " & TC.getSQLFormat(rowCurrent("Executable")) & ", StartupParameter = " & TC.getSQLFormat(rowCurrent("StartupParameter")) & ", AutoItScript = " & TC.getSQLFormat(rowCurrent("AutoItScript")) & ", J2KPreset = " & TC.getSQLFormat(rowCurrent("J2KPreset")) & ", ScreenshotDirectory = " & TC.getSQLFormat(rowCurrent("ScreenshotDirectory")) & ", Libretro_Core = " & TC.getSQLFormat(rowCurrent("Libretro_Core")) & " WHERE id_Emulators = " & TC.getSQLFormat(id_Emulators_Current), tran)
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

	Private Sub Refill_LibretroCore()
		Dim oLastCore As Object = cmb_Libretro_Core.EditValue
		BTA_Libretro_Core.Table.Clear()

		If Not isRetroArch() Then
			lbl_Libretro_Core.Visible = False
			cmb_Libretro_Core.Visible = False
			cmb_Libretro_Core.EditValue = Nothing
			Return
		End If

		Dim basedir As String = MKNetLib.cls_MKStringSupport.Clean_Right(txb_Directory.Text, "\")
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
			DevExpress.XtraEditors.XtraMessageBox.Show("Please save your changes before duplicating an emulator setting.", "Duplicate Emulator", MessageBoxButtons.OK)
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
					DevExpress.XtraEditors.XtraMessageBox.Show("Error while duplicating emulator settings: " & ex.Message, "Duplicate Emulator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
			DevExpress.XtraEditors.XtraMessageBox.Show("Error while launching the emulator, file '" & fullpath & "' not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		Else
			Try
				Dim proc = New System.Diagnostics.Process
				proc.StartInfo.FileName = fullpath
				proc.StartInfo.WorkingDirectory = txb_Directory.Text
				proc.StartInfo.UseShellExecute = True
				proc.Start()
			Catch ex As Exception
				DevExpress.XtraEditors.XtraMessageBox.Show("Error while launching the emulator: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
		Me.grd_Platforms.ShowHandInColumns(gv_Platforms, {"Suported", "DefaultEmulator"}, e)
	End Sub
End Class
