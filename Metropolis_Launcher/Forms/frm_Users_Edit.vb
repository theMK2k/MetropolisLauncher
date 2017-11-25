Public Class frm_Users_Edit
	Private _id_Users As Integer = 0
	Private _tbl_Users As DS_ML.tbl_UsersDataTable = Nothing

	Public Enum enm_EditMode
		All = 0
		Password = 1
		UserRestriction = 2
		Username = 4
	End Enum

	Public Sub New(ByVal tbl_Users As DS_ML.tbl_UsersDataTable, Optional ByVal id_Users As Integer = 0, Optional ByVal editMode As enm_EditMode = enm_EditMode.All)
		InitializeComponent()

		Me._tbl_Users = tbl_Users
		Me._id_Users = id_Users

		If Not editMode = enm_EditMode.All Then
			Me.lbl_Username.Enabled = False
			Me.txb_Username.Enabled = False

			Me.lbl_Password.Enabled = False
			Me.chb_Password.Enabled = False

			Me.lbl_Restricted.Visible = False
			Me.chb_Restricted.Visible = False

			If (editMode And enm_EditMode.Password) = enm_EditMode.Password Then
				Me.Name &= "_P"
				Me.Text = "Change Password"
				Me.lbl_Password.Enabled = True
				Me.chb_Password.Enabled = True
			End If

			If (editMode And enm_EditMode.Username) = enm_EditMode.Username Then
				Me.Name &= "_U"
				Me.lbl_Username.Enabled = False
				Me.txb_Username.Enabled = False
			End If

			If (editMode And enm_EditMode.UserRestriction) = enm_EditMode.UserRestriction Then
				Me.Name &= "_R"
				Me.lbl_Restricted.Visible = False
				Me.chb_Restricted.Visible = False
			End If
		End If
	End Sub

	Private Sub chb_Password_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_Password.CheckedChanged
		Me.lbl_Password.Enabled = chb_Password.Checked
		Me.txb_Password.Enabled = chb_Password.Checked
	End Sub

	Private Sub btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OK.Click
		If Me._tbl_Users.Select("Username = " & TC.getSQLFormat(Me.txb_Username.EditValue) & " AND id_Users <> " & Me._id_Users).Length > 0 Then
			MKDXHelper.MessageBox("This Username is already used, please choose another one.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		Me.DialogResult = DialogResult.OK
		Me.Close()
	End Sub

	Private Sub txb_Username_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txb_Username.EditValueChanged
		If TC.NZ(txb_Username.EditValue, "") <> "" Then
			btn_OK.Enabled = True
		Else
			btn_OK.Enabled = False
		End If
	End Sub
End Class