Public Class frm_Login

	Private _IgnorePassword As Boolean = False

	Public Sub New(Optional ByVal ShowOnlyRestricted As Boolean = False, Optional ByVal IgnorePassword As Boolean = False, Optional ByVal Explanation As String = "")
		InitializeComponent()

		Me.lbl_Explanation.Text = Explanation

		Me._IgnorePassword = IgnorePassword AndAlso ShowOnlyRestricted

		If IgnorePassword Then
			lbl_Password.Visible = False
			txb_Password.Visible = False
		End If

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Fill_tbl_Users(tran, Me.DS_ML.tbl_Users, 0, ShowOnlyRestricted)
		End Using

		If Not ShowOnlyRestricted AndAlso TC.NZ(cls_Settings.GetSetting("Last_Login_id_User"), 0) > 0 Then
			cmb_Users.EditValue = cls_Settings.GetSetting("Last_Login_id_User")
			BS_Users.Position = BS_Users.Find("id_Users", cmb_Users.EditValue)
		Else
			If BS_Users.Current IsNot Nothing Then
				cmb_Users.EditValue = BS_Users.Current("id_Users")
			End If
		End If
	End Sub

	Private Sub BS_Users_CurrentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BS_Users.CurrentChanged
		Me.txb_Password.EditValue = DBNull.Value

		If BS_Users.Current Is Nothing Then Return

		If TC.NZ(BS_Users.Current("Password"), "") = "" Then
			Me.lbl_Password.Enabled = False
			Me.txb_Password.Enabled = False
		Else
			Me.lbl_Password.Enabled = True
			Me.txb_Password.Enabled = True
		End If
	End Sub

	Private Sub btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OK.Click
		If Not _IgnorePassword Then
			If TC.NZ(BS_Users.Current("Password"), "") <> "" Then
				If cls_Globals.Encode_Password(TC.NZ(txb_Password.EditValue, "")) <> BS_Users.Current("Password") Then
					MKDXHelper.MessageBox("The password is not correct.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Return
				End If
			End If

			cls_Globals.Restricted = TC.NZ(BS_Users.Current("Restricted"), False)
			cls_Globals.Admin = TC.NZ(BS_Users.Current("Admin"), False)
			cls_Globals.id_Users = BS_Users.Current("id_Users")
			cls_Globals.id_Cheevo_Challenges = BS_Users.Current("id_Cheevo_Challenges")

			cls_Settings.SetSetting("Last_Login_id_User", cmb_Users.EditValue)
		End If

		Me.DialogResult = DialogResult.OK
		Me.Close()
	End Sub

	Private Sub txb_Password_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txb_Password.KeyDown
		If e.KeyCode = Keys.Enter Then
			btn_OK.PerformClick()
		End If
	End Sub

	Private Sub frm_Login_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
		If txb_Password.Enabled Then
			txb_Password.Focus()
		Else
			btn_OK.Focus()
		End If
	End Sub
End Class