Public Class frm_ScummVM_Scan

	Public Sub New()
		InitializeComponent()

		Me.btn_Browse_Dir.Focus()

		Dim sPath As String = TC.NZ(cls_Settings.GetSetting("ScummVM_Path", cls_Settings.enm_Settingmodes.Same_for_All), "")
		If Alphaleonis.Win32.Filesystem.File.Exists(sPath) Then
			Me.txb_ScummVM.EditValue = sPath
		End If
	End Sub

	Private Sub btn_Browse_ScummVM_Click(sender As Object, e As EventArgs) Handles btn_Browse_ScummVM.Click
		Dim sPath As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("Open scummvm.exe", "scummvm.exe|scummvm.exe", 0, "", "", False, Me)
		If Alphaleonis.Win32.Filesystem.File.Exists(sPath) Then
			Me.txb_ScummVM.EditValue = sPath
		End If
	End Sub

	Private Sub btn_Browse_Dir_Click(sender As Object, e As EventArgs) Handles btn_Browse_Dir.Click
		Dim sPath = MKNetLib.cls_MKFileSupport.OpenFolderDialog("", True, Me)
		If Alphaleonis.Win32.Filesystem.Directory.Exists(sPath) Then
			Me.txb_Dir.EditValue = sPath
		End If
	End Sub

	Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
		If Not Alphaleonis.Win32.Filesystem.File.Exists(Me.txb_ScummVM.EditValue) Then
			MKDXHelper.MessageBox("Please provide a ScummVM path.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.txb_ScummVM.Focus()
			Return
		End If

		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(Me.txb_Dir.EditValue) Then
			MKDXHelper.MessageBox("Please provide a path to scan.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.txb_Dir.Focus()
			Return
		End If

		'Check if ScummVM supports necessary command line switches
		Dim isValid As Boolean = False

		Dim sPathScummVM As String = TC.NZ(Me.txb_ScummVM.EditValue, "")
		Try
			Dim proc As New Process
			With proc.StartInfo
				.FileName = sPathScummVM
				.WorkingDirectory = Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(sPathScummVM)
				.Arguments = "--help"
				.UseShellExecute = False
				.RedirectStandardOutput = True
				.WindowStyle = ProcessWindowStyle.Hidden
				.CreateNoWindow = True
			End With

			proc.Start()
			Dim output As String = proc.StandardOutput.ReadToEnd
			proc.WaitForExit()

			If output.Contains("--add") AndAlso output.Contains("--recursive") Then
				isValid = True
			End If
		Catch ex As Exception
			MKDXHelper.ExceptionMessageBox(ex, "An Error occured while checking ScummVM: ")
			Return
		End Try

		If Not isValid Then
			MKDXHelper.MessageBox("The chosen ScummVM doesn't seem to support the necessary functionality to scan games (command line parameters --add and --recursive). Did you provide a ScummVM version 2.0.0 or later?", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		cls_Settings.SetSetting("ScummVM_Path", TC.NZ(Me.txb_ScummVM.EditValue, ""), cls_Settings.enm_Settingmodes.Same_for_All)

		Me.DialogResult = DialogResult.OK
		Me.Close()
	End Sub
End Class