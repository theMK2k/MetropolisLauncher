Public Class frm_Users_Setup

	Public Sub New()
		InitializeComponent()

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Fill_tbl_Users(tran, Me.DS_ML.tbl_Users, 0, False)
		End Using

		barmng_Users.SetPopupContextMenu(grd_Users, popmnu_Users)
	End Sub

	Private Sub bbi_Add_User_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Add_User.ItemClick
		Dim row As DataRow = Me.DS_ML.tbl_Users.NewRow
		Using frm As New frm_Users_Edit(Me.DS_ML.tbl_Users)
			If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
				row("Username") = frm.txb_Username.EditValue
				row("Password") = IIf(frm.chb_Password.Checked, IIf(frm.txb_Password.Text.Length > 0, cls_Globals.Encode_Password(frm.txb_Password.EditValue), DBNull.Value), DBNull.Value)
				row("Restricted") = frm.chb_Restricted.Checked
				row("Admin") = False

				Me.DS_ML.tbl_Users.Rows.Add(row)
			End If
		End Using
	End Sub

	Private Sub bbi_Edit_User_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Edit_User.ItemClick
		If BS_Users.Current Is Nothing Then Return

		Dim row As DataRow = BS_Users.Current.Row
		Using frm As New frm_Users_Edit(Me.DS_ML.tbl_Users, row("id_Users"))
			frm.txb_Username.EditValue = row("Username")
			frm.lbl_Restricted.Enabled = Not TC.NZ(row("Admin"), False)
			frm.chb_Restricted.Enabled = Not TC.NZ(row("Admin"), False)
			frm.chb_Restricted.Checked = TC.NZ(row("Restricted"), False)

			If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
				row("Username") = frm.txb_Username.EditValue
				row("Password") = IIf(frm.chb_Password.Checked, IIf(frm.txb_Password.Text.Length > 0, cls_Globals.Encode_Password(frm.txb_Password.EditValue), DBNull.Value), row("Password"))
				row("Restricted") = frm.chb_Restricted.Checked
			End If
		End Using
	End Sub

	Private Sub bbi_Delete_User_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Delete_User.ItemClick
		If DevExpress.XtraEditors.XtraMessageBox.Show("Do you really want to delete user '" & BS_Users.Current("Username") & "'?", "Delete User", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
			BS_Users.RemoveCurrent()
		End If
	End Sub

	Private Sub popmnu_Users_BeforePopup(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles popmnu_Users.BeforePopup
		If Not grd_Users.Allow_Popup Then
			e.Cancel = True
			Return
		End If

		If BS_Users.Current Is Nothing Then
			bbi_Delete_User.Enabled = False
			bbi_Edit_User.Enabled = False
		Else
			bbi_Edit_User.Enabled = True
			If TC.NZ(BS_Users.Current("Admin"), False) = True OrElse TC.NZ(BS_Users.Current("id_Users"), 0) = cls_Globals.id_Users Then
				bbi_Delete_User.Enabled = False
			Else
				bbi_Delete_User.Enabled = True
			End If
		End If
	End Sub

	Private Sub btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OK.Click
		Me.BS_Users.EndEdit()

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Upsert_tbl_Users(tran, Me.DS_ML.tbl_Users)
			tran.Commit()
		End Using

		Me.DialogResult = Windows.Forms.DialogResult.OK
		Me.Close()
	End Sub
End Class