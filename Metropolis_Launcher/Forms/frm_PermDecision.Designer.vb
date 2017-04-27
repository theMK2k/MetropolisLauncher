<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_PermDecision
	Inherits MKNetDXLib.frm_MKDXBaseForm

	'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso components IsNot Nothing Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	'Wird vom Windows Form-Designer benötigt.
	Private components As System.ComponentModel.IContainer

	'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
	'Sie kann mit dem Windows Form-Designer geändert werden.  
	'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.lbl_Prompt = New MKNetDXLib.ctl_MKDXLabel()
		Me.pnl_Bottom = New MKNetDXLib.ctl_MKDXPanel()
		Me.pnl_Bottom_Right = New MKNetDXLib.ctl_MKDXPanel()
		Me.pnl_Bottom_Left = New MKNetDXLib.ctl_MKDXPanel()
		Me.chb_ApplyAll = New MKNetDXLib.ctl_MKDXCheckEdit()
		CType(Me.pnl_Bottom, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_Bottom.SuspendLayout()
		CType(Me.pnl_Bottom_Right, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.pnl_Bottom_Left, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_Bottom_Left.SuspendLayout()
		CType(Me.chb_ApplyAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'lbl_Prompt
		'
		Me.lbl_Prompt.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_Prompt.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lbl_Prompt.Location = New System.Drawing.Point(0, 0)
		Me.lbl_Prompt.MKBoundControl1 = Nothing
		Me.lbl_Prompt.MKBoundControl2 = Nothing
		Me.lbl_Prompt.MKBoundControl3 = Nothing
		Me.lbl_Prompt.MKBoundControl4 = Nothing
		Me.lbl_Prompt.MKBoundControl5 = Nothing
		Me.lbl_Prompt.Name = "lbl_Prompt"
		Me.lbl_Prompt.Size = New System.Drawing.Size(784, 0)
		Me.lbl_Prompt.TabIndex = 0
		'
		'pnl_Bottom
		'
		Me.pnl_Bottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_Bottom.Controls.Add(Me.pnl_Bottom_Right)
		Me.pnl_Bottom.Controls.Add(Me.pnl_Bottom_Left)
		Me.pnl_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.pnl_Bottom.Location = New System.Drawing.Point(0, 143)
		Me.pnl_Bottom.Margin = New System.Windows.Forms.Padding(0)
		Me.pnl_Bottom.Name = "pnl_Bottom"
		Me.pnl_Bottom.Size = New System.Drawing.Size(784, 23)
		Me.pnl_Bottom.TabIndex = 1
		'
		'pnl_Bottom_Right
		'
		Me.pnl_Bottom_Right.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_Bottom_Right.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_Bottom_Right.Location = New System.Drawing.Point(130, 0)
		Me.pnl_Bottom_Right.Name = "pnl_Bottom_Right"
		Me.pnl_Bottom_Right.Size = New System.Drawing.Size(654, 23)
		Me.pnl_Bottom_Right.TabIndex = 1
		'
		'pnl_Bottom_Left
		'
		Me.pnl_Bottom_Left.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_Bottom_Left.Controls.Add(Me.chb_ApplyAll)
		Me.pnl_Bottom_Left.Dock = System.Windows.Forms.DockStyle.Left
		Me.pnl_Bottom_Left.Location = New System.Drawing.Point(0, 0)
		Me.pnl_Bottom_Left.Name = "pnl_Bottom_Left"
		Me.pnl_Bottom_Left.Size = New System.Drawing.Size(130, 23)
		Me.pnl_Bottom_Left.TabIndex = 0
		'
		'chb_ApplyAll
		'
		Me.chb_ApplyAll.Cursor = System.Windows.Forms.Cursors.Hand
		Me.chb_ApplyAll.Location = New System.Drawing.Point(2, 2)
		Me.chb_ApplyAll.MKBoundLabel = Nothing
		Me.chb_ApplyAll.MKEditValue_Compare = Nothing
		Me.chb_ApplyAll.Name = "chb_ApplyAll"
		Me.chb_ApplyAll.Properties.Caption = "Apply for all"
		Me.chb_ApplyAll.Size = New System.Drawing.Size(125, 19)
		Me.chb_ApplyAll.TabIndex = 0
		'
		'frm_PermDecision
		'
		Me.ClientSize = New System.Drawing.Size(784, 166)
		Me.Controls.Add(Me.pnl_Bottom)
		Me.Controls.Add(Me.lbl_Prompt)
		Me.Name = "frm_PermDecision"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		CType(Me.pnl_Bottom, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_Bottom.ResumeLayout(False)
		CType(Me.pnl_Bottom_Right, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pnl_Bottom_Left, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_Bottom_Left.ResumeLayout(False)
		CType(Me.chb_ApplyAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents lbl_Prompt As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents pnl_Bottom As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents pnl_Bottom_Right As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents pnl_Bottom_Left As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents chb_ApplyAll As MKNetDXLib.ctl_MKDXCheckEdit

End Class
