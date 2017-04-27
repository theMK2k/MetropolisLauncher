Imports System.ComponentModel

Public Class frm_DOSBox_Templates
	Public Sub New()
		InitializeComponent()
		Reload_Templates()

		barmng.SetPopupContextMenu(grd_DOSBox_Configs, popmnu_DOSBox_Templates)
	End Sub

	Public Sub Reload_Templates(Optional ByVal id_DOSBox_Configs As Long = 0)
		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Fill_tbl_DOSBox_Template_Configs(tran, DS_ML.tbl_DOSBox_Configs)
		End Using

		If id_DOSBox_Configs <> 0 Then
			BS_DOSBox_Configs.Position = BS_DOSBox_Configs.Find("id_DOSBox_Configs", id_DOSBox_Configs)
		End If
	End Sub

	Private Sub BS_DOSBox_Configs_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BS_DOSBox_Configs.CurrentChanged
		If BS_DOSBox_Configs.Current Is Nothing Then
			Me.ucr_DOSBox_Config.Clear()
			Return
		End If

		Me.ucr_DOSBox_Config.Load_Template(BS_DOSBox_Configs.Current("id_DOSBox_Configs"))
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
		If ucr_DOSBox_Config.HasChanges Then
			Dim res As DialogResult = Windows.Forms.DialogResult.Yes
			If ShowMessage Then
				res = DevExpress.XtraEditors.XtraMessageBox.Show(Message_Text, Message_Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
			End If

			If res = Windows.Forms.DialogResult.Yes Then
				Me.ucr_DOSBox_Config.Save_Template()
				Return True
			End If

			If res = Windows.Forms.DialogResult.No Then
				Me.ucr_DOSBox_Config.Reject_Configuration()
				Return True
			End If

			If res = Windows.Forms.DialogResult.Cancel Then
				Return False
			End If
		End If

		Return True
	End Function

	Private Sub btn_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Add.Click, bbi_Add.ItemClick
		If Not Save(True, "Do you want to save your changes before adding a new DOSBox Template?", "Add Template") Then
			Return
		End If

		Dim sNewTemplateName As String = ""

		Dim bDone As Boolean = False

		While Not bDone
			Using frm As New MKNetDXLib.frm_TextBoxEdit("Template Name:", "Add Template", "<New DOSBox Template>")
				If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
					sNewTemplateName = frm.Input
				Else
					Return
				End If
			End Using

			If TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT COUNT(1) FROM main.tbl_DOSBox_Configs WHERE Displayname = " & TC.getSQLFormat(sNewTemplateName)), 0) > 0 Then
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

		Dim id_DOSBox_Configs As Long = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "INSERT INTO main.tbl_DOSBox_Configs (Displayname, isTemplate) VALUES (" & TC.getSQLFormat(sNewTemplateName) & ", 1); SELECT last_insert_rowid()"), 0L)

		Me.Reload_Templates(id_DOSBox_Configs)
	End Sub

	Private Sub btn_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Delete.Click, bbi_Delete.ItemClick
		If BS_DOSBox_Configs.Current Is Nothing Then Return

		Cursor = Cursors.WaitCursor

		If TC.NZ(BS_DOSBox_Configs.Current("id_Rombase_DOSBox_Configs"), 0) <> 0 Then
			Cursor = Cursors.Default
			DevExpress.XtraEditors.XtraMessageBox.Show("This template cannot be deleted because it is part of the Metropolis Launcher installation. You can however modify it as you like.", "Delete Template", MessageBoxButtons.OK, MessageBoxIcon.Information)
			Return
		End If

		Dim iNumChildren As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT COUNT(1) FROM main.tbl_Emu_Games WHERE id_DOSBox_Configs_Template = " & BS_DOSBox_Configs.Current("id_DOSBox_Configs")), 0)

		Cursor = Cursors.Default

		If DevExpress.XtraEditors.XtraMessageBox.Show("Do you really want to delete template '" & BS_DOSBox_Configs.Current("Displayname") & "'?" & IIf(iNumChildren > 0, ControlChars.CrLf & ControlChars.CrLf & "WARNING: " & iNumChildren & " DOSBox configurations rely on this template and will be assigned to the Default Template!", ""), "Delete Template", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
			Cursor = Cursors.WaitCursor

			Dim id_Rombase_DOSBox_Configs As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_Rombase_DOSBox_Configs FROM rombase.tbl_Rombase_DOSBox_Configs WHERE isDefault = 1 LIMIT 1"), 0)
			Dim id_DOSBox_Configs As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_DOSBox_Configs FROM tbl_DOSBox_Configs WHERE id_Rombase_DOSBox_Configs = " & TC.getSQLFormat(id_Rombase_DOSBox_Configs) & " LIMIT 1"), 0)


			If id_DOSBox_Configs = 0 Then
				Cursor = Cursors.Default
				DevExpress.XtraEditors.XtraMessageBox.Show("Error: unable to find the Default Template, aborting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Return
			Else
				If Not DataAccess.FireProcedure(cls_Globals.Conn, 0, "UPDATE tbl_Emu_Games SET id_DOSBox_Configs_Template = " & TC.getSQLFormat(id_DOSBox_Configs) & " WHERE id_DOSBox_Configs_Template = " & TC.getSQLFormat(BS_DOSBox_Configs.Current("id_DOSBox_Configs"))) Then
					Cursor = Cursors.Default
					DevExpress.XtraEditors.XtraMessageBox.Show("Error: can't assign the Default Template, aborting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Return
				End If
			End If

			DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM main.tbl_DOSBox_Configs WHERE id_DOSBox_Configs = " & TC.getSQLFormat(BS_DOSBox_Configs.Current("id_DOSBox_Configs")))
			Me.Reload_Templates()
			Cursor = Cursors.Default
		End If
	End Sub

	Private Sub btn_Duplicate_Template_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Duplicate_Template.Click, bbi_Duplicate.ItemClick
		If BS_DOSBox_Configs.Current Is Nothing Then Return

		Dim sNewTemplateName As String = TC.NZ(BS_DOSBox_Configs.Current("Displayname"), "<Unnamed>") & " Copy"

		Dim bDone As Boolean = False

		While Not bDone
			Using frm As New MKNetDXLib.frm_TextBoxEdit("Template Name:", "Duplicate Template", sNewTemplateName)
				If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
					sNewTemplateName = frm.Input
				Else
					Return
				End If
			End Using

			If TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT COUNT(1) FROM main.tbl_DOSBox_Configs WHERE Displayname = " & TC.getSQLFormat(sNewTemplateName)), 0) > 0 Then
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

		Dim id_DOSBox_Configs As Long = ucr_DOSBox_Config.Save_Template(True, sNewTemplateName)

		Me.Reload_Templates(id_DOSBox_Configs)
	End Sub

	Private Sub gv_DOSBox_Config_BeforeLeaveRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles gv_DOSBox_Config.BeforeLeaveRow
		If Not Save(True, "Do you want to save your changes before switching to another template?", "Switching Templates") Then
			e.Allow = False
			Return
		End If
	End Sub

	Private Sub ucr_DOSBox_Config_E_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ucr_DOSBox_Config.E_Close
		Me.Close()
	End Sub

	Private Sub frm_DOSBox_Templates_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		If Not Save(True, "Do you want to save your changes before closing the DOSBox Templates editor?", "Close") Then
			e.Cancel = True
			Return
		End If
	End Sub

	Private Sub popmnu_DOSBox_Templates_BeforePopup(sender As Object, e As CancelEventArgs) Handles popmnu_DOSBox_Templates.BeforePopup
		If Not grd_DOSBox_Configs.Allow_Popup Then
			e.Cancel = True
			Return
		End If

		If BS_DOSBox_Configs.Current Is Nothing Then
			bbi_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		Else
			bbi_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
		End If
	End Sub
End Class
