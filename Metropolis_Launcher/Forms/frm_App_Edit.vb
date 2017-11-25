Public Class frm_App_Edit
	Public Sub New(ByRef BS_Categories_New As BindingSource, ByRef BS_Apps_New As BindingSource)
		InitializeComponent()

		Cursor.Current = Cursors.WaitCursor

		MKNetLib.frm_MKBaseform.UpdateBindingsources(Me.Controls, Me.BS_Categories, BS_Categories_New)
		MKNetLib.frm_MKBaseform.UpdateBindingsources(Me.Controls, Me.BS_Apps, BS_Apps_New)

		Me.BS_Apps = BS_Apps_New
		Me.BS_Categories = BS_Categories_New

		Cursor.Current = Cursors.Default
	End Sub

	Private Sub btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OK.Click
		'TODO: Checks
		If TC.NZ(cmb_Category.EditValue, 0) = 0 Then
			MKDXHelper.MessageBox("Please choose a category.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			cmb_Category.Focus()
			Return
		End If

		Me.DialogResult = Windows.Forms.DialogResult.OK
	End Sub

	Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
		Me.DialogResult = Windows.Forms.DialogResult.Cancel
		Me.Close()
	End Sub

	Private Sub btn_Executable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Executable.Click
		Dim ofd As New OpenFileDialog
		ofd.Filter = "Executables |*.exe;*.bat;*.cmd;*.lnk"
		If ofd.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
			Me.txb_Executable.EditValue = ofd.FileName
		End If
	End Sub
End Class
