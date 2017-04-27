Public Class frm_Backup
	Private _backupDir As String
	Private _backupRetention As Integer

	Public Sub New(backupDir As String, backupRetention As Integer)
		InitializeComponent()

		Me._backupDir = backupDir
	End Sub

	Private Sub frm_Backup_Shown(sender As Object, e As EventArgs) Handles Me.Shown

	End Sub
End Class