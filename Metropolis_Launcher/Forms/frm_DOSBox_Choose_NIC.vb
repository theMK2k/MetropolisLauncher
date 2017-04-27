Public Class frm_DOSBox_Choose_NIC
	Public ReadOnly Property Selected_NIC As String
		Get
			If BTA_NIC.Current IsNot Nothing Then
				Return BTA_NIC.Current("Displaytext")
			Else
				Return ""
			End If
		End Get
	End Property


	Public Sub New()
		InitializeComponent()
	End Sub

	Private Sub frm_DOSBox_Choose_NIC_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
		Dim bFound As Boolean = False

		For Each nic As System.Net.NetworkInformation.NetworkInterface In System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces
			Dim row As DataRowView = BTA_NIC.AddNew()
			row("Displaytext") = nic.Description
			bFound = True
		Next

		If Not bFound Then
			DevExpress.XtraEditors.XtraMessageBox.Show("It seems that there could no network interface card be found. The window will be closed.", "Could not find any NICs", MessageBoxButtons.OK, MessageBoxIcon.Information)
			Me.DialogResult = Windows.Forms.DialogResult.Cancel
			Me.Close()
		End If
	End Sub

	Private Sub btn_OK_Click(sender As System.Object, e As System.EventArgs) Handles btn_OK.Click
		Me.DialogResult = Windows.Forms.DialogResult.OK
		Me.Close()
	End Sub

	Private Sub btn_Cancel_Click(sender As System.Object, e As System.EventArgs) Handles btn_Cancel.Click
		Me.DialogResult = Windows.Forms.DialogResult.Cancel
		Me.Close()
	End Sub

	Private Sub gv_NIC_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gv_NIC.DoubleClick
		btn_OK.PerformClick()
	End Sub
End Class