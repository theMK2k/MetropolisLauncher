Public Class frm_PermDecision
	Public ReadOnly Property ApplyAll As Boolean
		Get
			Return chb_ApplyAll.Checked
		End Get
	End Property

	Public Sub New(ByVal Caption As String, ByVal Prompt As String, ByRef Buttons() As cls_PermDecision.PermDecisionButton)
		InitializeComponent()

		Me.Text = Caption
		Me.lbl_Prompt.Text = Prompt

		For Each Button As cls_PermDecision.PermDecisionButton In Buttons
			Dim btn As New MKNetDXLib.ctl_MKDXSimpleButton
			btn.Text = Button.Text
			btn.DialogResult = Button.Result
			If Not TC.IsNullNothingOrEmpty(Button.Tooltip) Then btn.ToolTip = Button.Tooltip
			btn.Dock = DockStyle.Right
			Me.pnl_Bottom_Right.Controls.Add(btn)

			Dim lbl As New MKNetDXLib.ctl_MKDXLabel
			lbl.MinimumSize = New Size(New Point(3, 0))
			lbl.Dock = DockStyle.Right
			Me.pnl_Bottom_Right.Controls.Add(lbl)
		Next
	End Sub
End Class
