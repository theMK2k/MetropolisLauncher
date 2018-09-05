Public Class frm_Emulators_Pre_Post_Launch_Command_Edit
	Public Sub New(Optional ByVal WindowCaption As String = "Edit Pre/Post Launch Command", Optional ByVal Directory As String = "", Optional ByVal Executable As String = "", Optional ByVal StartupParameter As String = "", Optional ByVal WaitForExit As Boolean = False, Optional ByVal Minimized As Boolean = False)
		InitializeComponent()

		Me.Text = WindowCaption

		Me.txb_Directory.EditValue = Directory
		Me.txb_Executable.EditValue = Executable
		Me.txb_StartupParameter.EditValue = StartupParameter
		Me.chb_WaitForExit.Checked = WaitForExit
		Me.chb_Minimized.Checked = Minimized
	End Sub

	Private Sub btn_EmulatorFileOpen_Click(sender As Object, e As EventArgs) Handles btn_EmulatorFileOpen.Click
		Dim sFullPath As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("Open", "Executables (*.exe;*.bat;*.cmd;*.lnk)|*.exe;*.bat;*.cmd;*.lnk", ParentForm:=Me)

		If Not Alphaleonis.Win32.Filesystem.File.Exists(sFullPath) Then
			Return
		End If

		Me.txb_Directory.EditValue = Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(sFullPath)
		Me.txb_Executable.EditValue = Alphaleonis.Win32.Filesystem.Path.GetFileName(sFullPath)
	End Sub

	Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
		If TC.NZ(Me.txb_Directory.EditValue, "") = "" Then
			MKDXHelper.MessageBox("Directory is missing", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.txb_Directory.Focus()
			Return
		End If

		If TC.NZ(Me.txb_Executable.EditValue, "") = "" Then
			MKDXHelper.MessageBox("Executable is missing", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.txb_Executable.Focus()
			Return
		End If

		Me.DialogResult = DialogResult.OK
		Me.Close()
		Return
	End Sub

	Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
		Me.DialogResult = DialogResult.Cancel
		Me.Close()
		Return
	End Sub
End Class