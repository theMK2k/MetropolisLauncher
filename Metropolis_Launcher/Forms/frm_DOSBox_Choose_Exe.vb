Public Class frm_DOSBox_Choose_Exe
	Public Sub New(ByVal Exe_Type As String, ByVal Filter As String, ByVal dt As DS_ML.tbl_Emu_GamesDataTable)
		InitializeComponent()

		Me.Text = Exe_Type.ToUpper & " executable"
		Me.lbl_Explanation.Text = "Please select a file for autostart as the " & Exe_Type & " executable in the list below and press OK. If you choose 'Just mount', DOSBox will start but won't autostart an executable."
		Me.BS_DOSBox_Files_and_Folders.DataSource = dt
		Me.BS_DOSBox_Files_and_Folders.Filter = Filter
	End Sub

	Private Sub btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OK.Click
		Me.DialogResult = Windows.Forms.DialogResult.OK
		Me.Close()
	End Sub

	Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Just_Mount.Click
		Me.DialogResult = Windows.Forms.DialogResult.Cancel
		Me.Close()
	End Sub

	Private Sub gv_DOSBox_Files_and_Folders_CustomColumnDisplayText(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles gv_DOSBox_Files_and_Folders.CustomColumnDisplayText
		If e.Column Is col_DOSBox_Displayname Then
			'Dim row As DataRow = gv_DOSBox_Files_and_Folders.GetRow(e.ListSourceRowIndex).Row
			Dim oInnerFile As Object = gv_DOSBox_Files_and_Folders.GetListSourceRowCellValue(e.ListSourceRowIndex, "InnerFile")
			Dim oFolder As Object = gv_DOSBox_Files_and_Folders.GetListSourceRowCellValue(e.ListSourceRowIndex, "Folder")

			If TC.NZ(oInnerFile, "").Length > 0 Then
				e.DisplayText = oInnerFile
			Else
				e.DisplayText = oFolder
			End If
		End If
	End Sub

	Private Sub gv_DOSBox_Files_and_Folders_DoubleClick(sender As Object, e As System.EventArgs) Handles gv_DOSBox_Files_and_Folders.DoubleClick
		Me.DialogResult = Windows.Forms.DialogResult.OK
		Me.Close()
	End Sub

	Private Sub gv_DOSBox_Files_and_Folders_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_DOSBox_Files_and_Folders.KeyDown
		If e.KeyCode = Keys.Enter Then
			Me.DialogResult = Windows.Forms.DialogResult.OK
			Me.Close()
		End If
		If e.KeyCode = Keys.Escape Then
			Me.DialogResult = Windows.Forms.DialogResult.Cancel
			Me.Close()
		End If
	End Sub
End Class
