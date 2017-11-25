Imports System.ComponentModel

Public Class frm_Known_Emulators

	Public Sub New()
		InitializeComponent()

		barmng.SetPopupContextMenu(grd_Emulators, popmnu_Emulators)

		Cursor.Current = Cursors.WaitCursor

		frm_Tag_Parser_Edit.Fill_Tag_Parser_Volumes(Me.DS_ML.ttb_Tag_Parser_Volumes)

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction()
			Me.DS_MobyDB.Fill_tbl_Moby_Platforms(tran, Me.DS_MobyDB.tbl_Moby_Platforms, True)
		End Using

		DS_ML.Fill_tbl_List_Generators(Me.DS_ML.tbl_List_Generators, OnlyRombase:=True)

		Me.DS_Rombase.Fill_tbl_Rombase_Known_Emulators(Me.DS_Rombase.tbl_Rombase_Known_Emulators)

		Cursor.Current = Cursors.Default
	End Sub

	Private Sub BS_Emulators_CurrentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BS_Emulators.CurrentChanged
		Cursor.Current = Cursors.WaitCursor

		If BS_Emulators.Current Is Nothing Then
			pnl_Right.Visible = False

			btn_Delete_Known_Emulator.Enabled = False
		Else
			pnl_Right.Visible = True

			btn_Delete_Known_Emulator.Enabled = True

			Me.DS_Rombase.Fill_src_frm_Known_Emulators_Moby_Platforms(Me.DS_Rombase.src_frm_Known_Emulators_Moby_Platforms, BS_Emulators.Current("id_Rombase_Known_Emulators"))

			Me.DS_Rombase.Fill_src_frm_Known_Emulators_Multivolume_Parameters(Me.DS_Rombase.tbl_Rombase_Known_Emulators_Multivolume_Parameters, BS_Emulators.Current("id_Rombase_Known_Emulators"))

			txb_StartupParameter.Visible = True
			lbl_StartupParameter.Visible = True
			lbl_List_Generator.Visible = True
			cmb_List_Generator.Visible = True
			tpg_MV_Settings.PageVisible = True
			BS_Platforms.Filter = ""
		End If

		Cursor.Current = Cursors.Default
	End Sub

	Private Function CheckSave(Optional ByRef row As DataRow = Nothing) As DialogResult
		Debug.WriteLine("CheckSave START")

		Dim hasChanges As Boolean = False

		If DS_Rombase.src_frm_Known_Emulators_Moby_Platforms.GetChanges IsNot Nothing Then
			Debug.WriteLine("	Change detected in DS_Rombase.src_frm_Known_Emulators_Moby_Platforms.GetChanges")
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

		If DS_Rombase.tbl_Rombase_Known_Emulators_Multivolume_Parameters.GetChanges IsNot Nothing Then
			Debug.WriteLine("	Change detected in DS_Rombase.tbl_Rombase_Known_Emulators_Multivolume_Parameters.GetChanges")
			hasChanges = True
		End If

		If hasChanges Then
			Dim res As DialogResult = MKDXHelper.MessageBox("Save the changes for the current known emulator?", "Save changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
			Return res
		End If

		Return Windows.Forms.DialogResult.None
	End Function

	Private Sub gv_Emulators_BeforeLeaveRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles gv_Emulators.BeforeLeaveRow
		If gv_Emulators.GetRow(e.RowHandle) Is Nothing Then Return
		Dim row As DS_Rombase.tbl_Rombase_Known_EmulatorsRow = gv_Emulators.GetRow(e.RowHandle).Row
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

	Private Sub Handle_Add_Emulator(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Add_Known_Emulator.Click, bbi_Add.ItemClick
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

		Dim row As DS_Rombase.tbl_Rombase_Known_EmulatorsRow = Me.DS_Rombase.tbl_Rombase_Known_Emulators.NewRow
		row("Name") = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(sFullPath)
		row("Exe_Identifier_Regex") = Alphaleonis.Win32.Filesystem.Path.GetFileName(sFullPath)
		row("Identifier") = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(sFullPath)

		Me.DS_Rombase.tbl_Rombase_Known_Emulators.Rows.Add(row)

		BS_Emulators.Position = BS_Emulators.Find("id_Rombase_Known_Emulators", row("id_Rombase_Known_Emulators"))
	End Sub

	Private Sub Handle_Delete_Emulator(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Delete_Known_Emulator.Click, bbi_Delete.ItemClick
		If MKDXHelper.MessageBox("Do you really want to remove the known emulator and its settings?", "Delete Emulator", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
			Dim id_Rombase_Known_Emulators = BS_Emulators.Current("id_Rombase_Known_Emulators")
			Me.BS_Emulators.RemoveCurrent()

			If id_Rombase_Known_Emulators > 0 Then
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM rombase.tbl_Rombase_Known_Emulators WHERE id_Rombase_Known_Emulators = " & TC.getSQLFormat(id_Rombase_Known_Emulators))
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM rombase.tbl_Rombase_Known_Emulators_Moby_Platforms WHERE id_Rombase_Known_Emulators = " & TC.getSQLFormat(id_Rombase_Known_Emulators))
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM rombase.tbl_Rombase_Known_Emulators_Multivolume_Parameters WHERE id_Rombase_Known_Emulators = " & TC.getSQLFormat(id_Rombase_Known_Emulators))
			End If
		End If
	End Sub

	Private Function Save() As Boolean
		If Me.DS_Rombase.tbl_Rombase_Known_Emulators_Multivolume_Parameters.Rows.Count > 0 AndAlso Not Me.txb_StartupParameter.Text.ToLower.Contains("%multivolume%") Then
			If Not MKDXHelper.MessageBox("There are startup parameters defined in the Multiple Volumes tab. These can only be used if you put %multivolume% in the Startup Parameter field in the Settings tab. Do you still want to save?", "Missing Libretro Core", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
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
		Dim id_Rombase_Known_Emulators_Current As Integer = BS_Emulators.Current("id_Rombase_Known_Emulators")
		Dim rowCurrent As DataRow = BS_Emulators.Current.Row

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			Try
				If id_Rombase_Known_Emulators_Current < 0 Then
					Dim sSQL As String = ""
					sSQL &= "INSERT INTO rombase.tbl_Rombase_Known_Emulators (" & ControlChars.CrLf
					sSQL &= "	Identifier" & ControlChars.CrLf
					sSQL &= "	, Name" & ControlChars.CrLf
					sSQL &= "	, Description" & ControlChars.CrLf
					sSQL &= "	, Autoconfig_Note" & ControlChars.CrLf
					sSQL &= "	, Exe_Identifier_Regex" & ControlChars.CrLf
					sSQL &= "	, URL_Website" & ControlChars.CrLf
					sSQL &= "	, URL_Download" & ControlChars.CrLf
					sSQL &= "	, StartupParameter" & ControlChars.CrLf
					sSQL &= "	, ScreenshotDirectory" & ControlChars.CrLf
					sSQL &= "	, id_List_Generators" & ControlChars.CrLf
					sSQL &= " ) VALUES (" & ControlChars.CrLf
					sSQL &= TC.getSQLFormat(rowCurrent("Identifier")) & ControlChars.CrLf
					sSQL &= ", " & TC.getSQLFormat(rowCurrent("Name")) & ControlChars.CrLf
					sSQL &= ", " & TC.getSQLFormat(rowCurrent("Description")) & ControlChars.CrLf
					sSQL &= ", " & TC.getSQLFormat(rowCurrent("Autoconfig_Note")) & ControlChars.CrLf
					sSQL &= ", " & TC.getSQLFormat(rowCurrent("Exe_Identifier_Regex")) & ControlChars.CrLf
					sSQL &= ", " & TC.getSQLFormat(rowCurrent("URL_Website")) & ControlChars.CrLf
					sSQL &= ", " & TC.getSQLFormat(rowCurrent("URL_Download")) & ControlChars.CrLf
					sSQL &= ", " & TC.getSQLFormat(rowCurrent("StartupParameter")) & ControlChars.CrLf
					sSQL &= ", " & TC.getSQLFormat(rowCurrent("ScreenshotDirectory")) & ControlChars.CrLf
					sSQL &= ", " & TC.getSQLFormat(rowCurrent("id_List_Generators")) & ControlChars.CrLf
					sSQL &= "); SELECT last_insert_rowid()" & ControlChars.CrLf

					id_Rombase_Known_Emulators_Current = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, sSQL, tran)
					rowCurrent("id_Rombase_Known_Emulators") = id_Rombase_Known_Emulators_Current
				Else
					DataAccess.FireProcedure(tran.Connection, 0, "UPDATE rombase.tbl_Rombase_Known_Emulators SET Identifier = " & TC.getSQLFormat(rowCurrent("Identifier")) &
																	 ", Name = " & TC.getSQLFormat(rowCurrent("Name")) &
																	 ", Description = " & TC.getSQLFormat(rowCurrent("Description")) &
																	 ", Autoconfig_Note = " & TC.getSQLFormat(rowCurrent("Autoconfig_Note")) &
																	 ", Exe_Identifier_Regex = " & TC.getSQLFormat(rowCurrent("Exe_Identifier_Regex")) &
																	 ", URL_Website = " & TC.getSQLFormat(rowCurrent("URL_Website")) &
																	 ", URL_Download = " & TC.getSQLFormat(rowCurrent("URL_Download")) &
																	 ", StartupParameter = " & TC.getSQLFormat(rowCurrent("StartupParameter")) &
																	 ", ScreenshotDirectory = " & TC.getSQLFormat(rowCurrent("ScreenshotDirectory")) &
																	 ", id_List_Generators = " & TC.getSQLFormat(rowCurrent("id_List_Generators")) & " WHERE id_Rombase_Known_Emulators = " & TC.getSQLFormat(id_Rombase_Known_Emulators_Current), tran)
				End If
				rowCurrent.AcceptChanges()

				DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM rombase.tbl_Rombase_Known_Emulators_Moby_Platforms WHERE id_Rombase_Known_Emulators = " & TC.getSQLFormat(id_Rombase_Known_Emulators_Current), tran)
				For Each row As DataRow In DS_Rombase.src_frm_Known_Emulators_Moby_Platforms.Rows
					If TC.NZ(row("Supported"), False) = True Then
						DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO rombase.tbl_Rombase_Known_Emulators_Moby_Platforms (id_Rombase_Known_Emulators, id_Moby_Platforms) VALUES (" & TC.getSQLParameter(id_Rombase_Known_Emulators_Current, row("id_Moby_Platforms")) & ")", tran)
					End If
					row.AcceptChanges()
				Next
				DS_Rombase.src_frm_Known_Emulators_Moby_Platforms.AcceptChanges()

				DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM rombase.tbl_Rombase_Known_Emulators_Multivolume_Parameters WHERE id_Rombase_Known_Emulators = " & TC.getSQLFormat(id_Rombase_Known_Emulators_Current), tran)
				For Each row As DataRow In DS_Rombase.tbl_Rombase_Known_Emulators_Multivolume_Parameters.Rows
					If row.RowState <> DataRowState.Deleted AndAlso row.RowState <> DataRowState.Detached Then
						DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO rombase.tbl_Rombase_Known_Emulators_Multivolume_Parameters (id_Rombase_Known_Emulators, Volume_Number, Parameter) VALUES (" & TC.getSQLParameter(id_Rombase_Known_Emulators_Current, row("Volume_Number"), row("Parameter")) & ")", tran)
						row.AcceptChanges()
					End If
				Next
				DS_Rombase.tbl_Rombase_Known_Emulators_Multivolume_Parameters.AcceptChanges()

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

	Private Sub Refill_List_Generators()
		Me.DS_ML.tbl_List_Generators.Clear()
		DS_ML.Fill_tbl_List_Generators(Me.DS_ML.tbl_List_Generators, OnlyRombase:=True)
	End Sub

	Private Sub frm_Emulators_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
		If BS_Emulators.Current IsNot Nothing Then
			Dim row As DS_Rombase.tbl_Rombase_Known_EmulatorsRow = BS_Emulators.Current.Row

			'BS_Emulators.EndEdit()

			Select Case CheckSave(row)
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
		For Each row As DataRow In Me.DS_Rombase.tbl_Rombase_Known_Emulators_Multivolume_Parameters.Rows
			If TC.NZ(row("Volume_Number"), 0L) > MaxVol Then MaxVol = row("Volume_Number")
		Next

		Dim newrow As DataRow = Me.DS_Rombase.tbl_Rombase_Known_Emulators_Multivolume_Parameters.NewRow
		newrow("Volume_Number") = MaxVol + 1L
		Me.DS_Rombase.tbl_Rombase_Known_Emulators_Multivolume_Parameters.Rows.Add(newrow)
	End Sub

	Private Sub popmnu_Emulators_BeforePopup(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles popmnu_Emulators.BeforePopup
		If Not grd_Emulators.Allow_Popup Then
			e.Cancel = True
			Return
		End If

		bbi_Add.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
		bbi_Import.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
		bbi_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

		If BS_Emulators.Current IsNot Nothing Then
			bbi_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
		End If
	End Sub

	Private Sub gv_Platforms_MouseMove(sender As Object, e As MouseEventArgs) Handles gv_Platforms.MouseMove
		Me.grd_Platforms.ShowHandInColumns(gv_Platforms, {"Supported"}, e)
	End Sub

	Private Sub cmb_List_Generator_ButtonPressed(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_List_Generator.ButtonPressed
		Select Case e.Button.Kind
			Case DevExpress.XtraEditors.Controls.ButtonPredefines.Delete
				Me.BS_Emulators.Current("id_List_Generators") = DBNull.Value
		End Select
	End Sub

	Private Sub btn_Import_Known_Emulator_Click(sender As Object, e As EventArgs) Handles btn_Import_Known_Emulator.Click, bbi_Import.ItemClick
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

		Using frm As New frm_Select_Emulator
			If frm.ShowDialog <> DialogResult.OK Then
				Return
			End If

			Dim row_Emu As DS_ML.tbl_EmulatorsRow = frm.Selected_Emulator_Row

			If row_Emu Is Nothing Then
				Return
			End If

			Dim row As DS_Rombase.tbl_Rombase_Known_EmulatorsRow = Me.DS_Rombase.tbl_Rombase_Known_Emulators.NewRow
			row("Name") = row_Emu.Displayname
			row("Exe_Identifier_Regex") = Alphaleonis.Win32.Filesystem.Path.GetFileName(row_Emu.Executable)
			row("Identifier") = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(row_Emu.Executable)

			row("StartupParameter") = row_Emu("StartupParameter")
			row("ScreenshotDirectory") = row_Emu("ScreenshotDirectory")
			row("id_List_Generators") = row_Emu("id_List_Generators")

			Me.DS_Rombase.tbl_Rombase_Known_Emulators.Rows.Add(row)

			Dim id_Emulators = row_Emu("id_Emulators")
			Dim id_Rombase_Known_Emulators = row("id_Rombase_Known_Emulators")

			BS_Emulators.Position = BS_Emulators.Find("id_Rombase_Known_Emulators", id_Rombase_Known_Emulators)

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				Me.DS_ML.Fill_src_frm_Emulators_Moby_Platforms(tran, Me.DS_ML.src_frm_Emulators_Moby_Platforms, id_Emulators)
				Me.DS_ML.Fill_src_frm_Emulators_Multivolume_Parameters(tran, Me.DS_ML.tbl_Emulators_Multivolume_Parameters, id_Emulators)

				tran.Commit()
			End Using

			For Each row_Moby_Platforms As DS_ML.src_frm_Emulators_Moby_PlatformsRow In Me.DS_ML.src_frm_Emulators_Moby_Platforms.Rows
				If TC.NZ(row_Moby_Platforms("Supported"), False) = True Then
					For Each row_Rombase_Known_Emulators_Moby_Platforms As DS_Rombase.src_frm_Known_Emulators_Moby_PlatformsRow In Me.DS_Rombase.src_frm_Known_Emulators_Moby_Platforms.Rows
						If row_Rombase_Known_Emulators_Moby_Platforms("id_Moby_Platforms") = row_Moby_Platforms("id_Moby_Platforms") Then
							row_Rombase_Known_Emulators_Moby_Platforms.Supported = True
						End If
					Next
				End If
			Next

			For Each row_Multivolume_Parameters As DS_ML.tbl_Emulators_Multivolume_ParametersRow In Me.DS_ML.tbl_Emulators_Multivolume_Parameters.Rows
				Dim row_Rombase_Known_Emulators_Multivolume_Parameters As DS_Rombase.tbl_Rombase_Known_Emulators_Multivolume_ParametersRow = Me.DS_Rombase.tbl_Rombase_Known_Emulators_Multivolume_Parameters.NewRow

				row_Rombase_Known_Emulators_Multivolume_Parameters.id_Rombase_Known_Emulators = id_Rombase_Known_Emulators
				row_Rombase_Known_Emulators_Multivolume_Parameters("Parameter") = row_Multivolume_Parameters("Parameter")
				row_Rombase_Known_Emulators_Multivolume_Parameters("Volume_Number") = row_Multivolume_Parameters("Volume_Number")

				Me.DS_Rombase.tbl_Rombase_Known_Emulators_Multivolume_Parameters.Rows.Add(row_Rombase_Known_Emulators_Multivolume_Parameters)
			Next

		End Using
	End Sub
End Class
