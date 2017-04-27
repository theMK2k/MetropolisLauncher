Public Class frm_Description
	Public Sub New(ByVal Title As String, ByVal Description As String)
		InitializeComponent()

		Me.lbl_Title.Text = Title
		Me.txb_Description.Text = Description
		Me.txb_Description.DeselectAll()
	End Sub
End Class
