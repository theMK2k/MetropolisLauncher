Public Class frm_Rom_Manager_ChangeDirectory

	Public ReadOnly Property NewDir As String
		Get
			Return txb_NewDir.Text
		End Get
	End Property

	Public Sub New(ByVal OldDir As String, ByVal NumRoms As Integer)
		InitializeComponent()

		Me.txb_OldDir.Text = OldDir
		lbl_Explanation.Text = lbl_Explanation.Text.Replace("%1%", NumRoms.ToString)
	End Sub

	Private Sub btn_OpenDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OpenDir.Click
		Dim sDir As String = MKNetLib.cls_MKFileSupport.OpenFolderDialog(txb_OldDir.Text)
		If Alphaleonis.Win32.Filesystem.Directory.Exists(sDir) Then
			txb_NewDir.Text = sDir
		End If
	End Sub

	Private Sub btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OK.Click
		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(txb_NewDir.Text) Then
			DevExpress.XtraEditors.XtraMessageBox.Show("Please select a valid new directory first.", "Invalid new directory", MessageBoxButtons.OK, MessageBoxIcon.Information)
			Return
		End If

		Me.DialogResult = Windows.Forms.DialogResult.OK
		Me.Close()
	End Sub

	Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
		Me.DialogResult = Windows.Forms.DialogResult.Cancel
		Me.Close()
	End Sub
End Class
