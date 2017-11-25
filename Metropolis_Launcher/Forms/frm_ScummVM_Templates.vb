Imports System.ComponentModel

Public Class frm_ScummVM_Templates
	Public Sub New()
		InitializeComponent()
		Reload_Templates()

		barmng.SetPopupContextMenu(grd_ScummVM_Configs, popmnu_ScummVM_Templates)
	End Sub

	Public Sub Reload_Templates(Optional ByVal id_ScummVM_Configs As Long = 0)
		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Fill_tbl_ScummVM_Template_Configs(tran, DS_ML.tbl_ScummVM_Configs)
		End Using

		If id_ScummVM_Configs <> 0 Then
			BS_ScummVM_Configs.Position = BS_ScummVM_Configs.Find("id_ScummVM_Configs", id_ScummVM_Configs)
		End If
	End Sub

	Private Sub BS_ScummVM_Configs_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BS_ScummVM_Configs.CurrentChanged
		If BS_ScummVM_Configs.Current Is Nothing Then
			Me.ucr_ScummVM_Config.Clear()
			Return
		End If

		Me.ucr_ScummVM_Config.Load_Template(BS_ScummVM_Configs.Current("id_ScummVM_Configs"))
	End Sub

	''' <summary>
	''' Save (or better: ask for Save with Yes/No/Cancel)
	''' </summary>
	''' <param name="ShowMessage"></param>
	''' <param name="Message_Title"></param>
	''' <param name="Message_Text"></param>
	''' <returns>True if either Yes or No is chosen, False if Cancelled</returns>
	''' <remarks></remarks>
	Private Function Save(ByVal ShowMessage As Boolean, ByVal Message_Text As String, ByVal Message_Title As String) As Boolean
		If ucr_ScummVM_Config.HasChanges Then
			Dim res As DialogResult = Windows.Forms.DialogResult.Yes
			If ShowMessage Then
				res = DevExpress.XtraEditors.XtraMessageBox.Show(Message_Text, Message_Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
			End If

			If res = Windows.Forms.DialogResult.Yes Then
				Me.ucr_ScummVM_Config.Save_Template()
				Return True
			End If

			If res = Windows.Forms.DialogResult.No Then
				Me.ucr_ScummVM_Config.Reject_Configuration()
				Return True
			End If

			If res = Windows.Forms.DialogResult.Cancel Then
				Return False
			End If
		End If

		Return True
	End Function

	Private Sub btn_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Add.Click, bbi_Add.ItemClick
		If Not Save(True, "Do you want to save your changes before adding a new ScummVM Template?", "Add Template") Then
			Return
		End If

		Dim sNewTemplateName As String = ""

		Dim bDone As Boolean = False

		While Not bDone
			Using frm As New MKNetDXLib.frm_TextBoxEdit("Template Name:", "Add Template", "<New ScummVM Template>")
				If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
					sNewTemplateName = frm.Input
				Else
					Return
				End If
			End Using

			If TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT COUNT(1) FROM main.tbl_ScummVM_Configs WHERE Displayname = " & TC.getSQLFormat(sNewTemplateName)), 0) > 0 Then
				Dim res As DialogResult = DevExpress.XtraEditors.XtraMessageBox.Show("There already exists a Template with the name '" & sNewTemplateName & "'. Do you really want to create a template with the same name?", "Add Template", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
				If res = Windows.Forms.DialogResult.Cancel Then
					Return
				End If
				If res = Windows.Forms.DialogResult.Yes Then
					bDone = True
				End If
				If res = Windows.Forms.DialogResult.No Then
					bDone = False
				End If
			Else
				bDone = True
			End If
		End While

		Dim id_ScummVM_Configs As Long = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "INSERT INTO main.tbl_ScummVM_Configs (Displayname, isTemplate) VALUES (" & TC.getSQLFormat(sNewTemplateName) & ", 1); SELECT last_insert_rowid()"), 0L)

		Me.Reload_Templates(id_ScummVM_Configs)
	End Sub

	Private Sub btn_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Delete.Click, bbi_Delete.ItemClick
		If BS_ScummVM_Configs.Current Is Nothing Then Return

		Cursor = Cursors.WaitCursor

		If TC.NZ(BS_ScummVM_Configs.Current("id_Rombase_ScummVM_Configs"), 0) <> 0 Then
			Cursor = Cursors.Default
			DevExpress.XtraEditors.XtraMessageBox.Show("This template cannot be deleted because it is part of the Metropolis Launcher installation. You can however modify it as you like.", "Delete Template", MessageBoxButtons.OK, MessageBoxIcon.Information)
			Return
		End If

		Dim iNumChildren As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT COUNT(1) FROM main.tbl_Emu_Games WHERE id_ScummVM_Configs_Template = " & BS_ScummVM_Configs.Current("id_ScummVM_Configs")), 0)

		Cursor = Cursors.Default

		If DevExpress.XtraEditors.XtraMessageBox.Show("Do you really want to delete template '" & BS_ScummVM_Configs.Current("Displayname") & "'?" & IIf(iNumChildren > 0, ControlChars.CrLf & ControlChars.CrLf & "WARNING: " & iNumChildren & " ScummVM configurations rely on this template and will be assigned to the Default Template!", ""), "Delete Template", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
			Cursor = Cursors.WaitCursor

			Dim id_Rombase_ScummVM_Configs As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_Rombase_ScummVM_Configs FROM rombase.tbl_Rombase_ScummVM_Configs WHERE isDefault = 1 LIMIT 1"), 0)
			Dim id_ScummVM_Configs As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_ScummVM_Configs FROM tbl_ScummVM_Configs WHERE id_Rombase_ScummVM_Configs = " & TC.getSQLFormat(id_Rombase_ScummVM_Configs) & " LIMIT 1"), 0)


			If id_ScummVM_Configs = 0 Then
				Cursor = Cursors.Default
				DevExpress.XtraEditors.XtraMessageBox.Show("Error: unable to find the Default Template, aborting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Return
			Else
				If Not DataAccess.FireProcedure(cls_Globals.Conn, 0, "UPDATE tbl_Emu_Games SET id_ScummVM_Configs_Template = " & TC.getSQLFormat(id_ScummVM_Configs) & " WHERE id_ScummVM_Configs_Template = " & TC.getSQLFormat(BS_ScummVM_Configs.Current("id_ScummVM_Configs"))) Then
					Cursor = Cursors.Default
					DevExpress.XtraEditors.XtraMessageBox.Show("Error: can't assign the Default Template, aborting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Return
				End If
			End If

			DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM main.tbl_ScummVM_Configs WHERE id_ScummVM_Configs = " & TC.getSQLFormat(BS_ScummVM_Configs.Current("id_ScummVM_Configs")))
			Me.Reload_Templates()
			Cursor = Cursors.Default
		End If
	End Sub

	Private Sub btn_Duplicate_Template_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Duplicate_Template.Click, bbi_Duplicate.ItemClick
		If BS_ScummVM_Configs.Current Is Nothing Then Return

		Dim sNewTemplateName As String = TC.NZ(BS_ScummVM_Configs.Current("Displayname"), "<Unnamed>") & " Copy"

		Dim bDone As Boolean = False

		While Not bDone
			Using frm As New MKNetDXLib.frm_TextBoxEdit("Template Name:", "Duplicate Template", sNewTemplateName)
				If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
					sNewTemplateName = frm.Input
				Else
					Return
				End If
			End Using

			If TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT COUNT(1) FROM main.tbl_ScummVM_Configs WHERE Displayname = " & TC.getSQLFormat(sNewTemplateName)), 0) > 0 Then
				Dim res As DialogResult = DevExpress.XtraEditors.XtraMessageBox.Show("There already exists a Template with the name '" & sNewTemplateName & "'. Do you really want to create a template with the same name?", "Add Template", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
				If res = Windows.Forms.DialogResult.Cancel Then
					Return
				End If
				If res = Windows.Forms.DialogResult.Yes Then
					bDone = True
				End If
				If res = Windows.Forms.DialogResult.No Then
					bDone = False
				End If
			Else
				bDone = True
			End If
		End While

		Dim id_ScummVM_Configs As Long = ucr_ScummVM_Config.Save_Template(True, sNewTemplateName)

		Me.Reload_Templates(id_ScummVM_Configs)
	End Sub

	Private Sub gv_ScummVM_Config_BeforeLeaveRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles gv_ScummVM_Configs.BeforeLeaveRow
		If Not Save(True, "Do you want to save your changes before switching to another template?", "Switching Templates") Then
			e.Allow = False
			Return
		End If
	End Sub

	Private Sub ucr_ScummVM_Config_E_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ucr_ScummVM_Config.E_Close
		Me.Close()
	End Sub

	Private Sub frm_ScummVM_Templates_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		If Not Save(True, "Do you want to save your changes before closing the ScummVM Templates editor?", "Close") Then
			e.Cancel = True
			Return
		End If
	End Sub

	Private Sub popmnu_ScummVM_Templates_BeforePopup(sender As Object, e As CancelEventArgs) Handles popmnu_ScummVM_Templates.BeforePopup
		If Not grd_ScummVM_Configs.Allow_Popup Then
			e.Cancel = True
			Return
		End If

		If BS_ScummVM_Configs.Current Is Nothing Then
			bbi_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		Else
			bbi_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
		End If
	End Sub
End Class
