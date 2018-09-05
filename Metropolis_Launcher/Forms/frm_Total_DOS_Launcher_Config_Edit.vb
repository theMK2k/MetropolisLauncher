Public Class frm_Total_DOS_Launcher_Config_Edit
	Public _id_Total_DOS_Launcher_Configs = 0

	Public Sub New(Optional ByVal id_Total_DOS_Launcher_Configs = 0)
		InitializeComponent()

		Me._id_Total_DOS_Launcher_Configs = id_Total_DOS_Launcher_Configs

		If id_Total_DOS_Launcher_Configs <> 0 Then
			DS_ML.Fill_tbl_Total_DOS_Launcher_Configs(Me.DS_ML.tbl_Total_DOS_Launcher_Configs, id_Total_DOS_Launcher_Configs)
			Me.Text = "Edit Total DOS Launcher Configuration"
		Else
			Me.BS_Total_DOS_Launcher_Configs.AddNew()
			Me.Text = "Add Total DOS Launcher Configuration"
		End If
	End Sub

	Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
		Me.DialogResult = DialogResult.Cancel
		Me.Close()
	End Sub

	Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
		If TC.NZ(Me.txb_DisplayName.EditValue, "").Trim = "" Then
			MKDXHelper.MessageBox("Please provide a name for the Total DOS Launcher Configuration", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.txb_DisplayName.Focus()
			Return
		End If

		If TC.NZ(Me.txb_proglocations.EditValue, "").Trim = "" Then
			MKDXHelper.MessageBox("Please provide Program Locations", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.txb_proglocations.Focus()
			Return
		End If

		If TC.NZ(Me.txb_cachelocation.EditValue, "").Trim = "" Then
			MKDXHelper.MessageBox("Please provide a Cache Location", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.txb_cachelocation.Focus()
			Return
		End If

		If TC.NZ(Me.cmb_userlevel.EditValue, "").Trim = "" Then
			MKDXHelper.MessageBox("Please provide a Cache Location", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.cmb_userlevel.Focus()
			Return
		End If

		If Me._id_Total_DOS_Launcher_Configs < 0 Then
			Dim res As DialogResult = MKDXHelper.MessageBox("You are editing a shipped Total DOS Launcher Configuration, do you want to create a new one?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

			If res = DialogResult.Cancel Then
				Return
			End If

			If res = DialogResult.No Then
				Me.DialogResult = DialogResult.Cancel
				Me.Close()
				Return
			End If
		End If

		If TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_Total_DOS_Launcher_Configs FROM tbl_Total_DOS_Launcher_Configs WHERE DisplayName = " & TC.getSQLFormat(Me.txb_DisplayName.EditValue) & IIf(Me._id_Total_DOS_Launcher_Configs > 0, " AND id_Total_DOS_Launcher_Configs <> " & TC.getSQLFormat(Me._id_Total_DOS_Launcher_Configs), "")), 0) <> 0 Then
			MKDXHelper.MessageBox("Another Total DOS Launcher Configuration with the same name already exists. Please use a unique name.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.txb_DisplayName.Focus()
			Return
		End If

		If TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_Rombase_Total_DOS_Launcher_Configs FROM rombase.tbl_Rombase_Total_DOS_Launcher_Configs WHERE DisplayName = " & TC.getSQLFormat(Me.txb_DisplayName.EditValue)), 0) <> 0 Then
			MKDXHelper.MessageBox("Another shipped Total DOS Launcher Configuration with the same name already exists. Please use a unique name.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.txb_DisplayName.Focus()
			Return
		End If

		Me.DialogResult = DialogResult.OK
		Me.Close()
	End Sub

	Private Sub cmb_VESA_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_VESA.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
			Me.cmb_VESA.EditValue = Nothing
		End If
	End Sub
End Class