<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ScummVM_Scan
	Inherits MKNetDXLib.frm_MKDXBaseForm

	'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Wird vom Windows Form-Designer benötigt.
	Private components As System.ComponentModel.IContainer

	'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
	'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
	'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.lbl_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		Me.Ctl_MKDXPanel1 = New MKNetDXLib.ctl_MKDXPanel()
		Me.btn_No = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Browse_Dir = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.txb_Dir = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.lbl_Dir = New MKNetDXLib.ctl_MKDXLabel()
		Me.btn_Browse_ScummVM = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.txb_ScummVM = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.lbl_ScummVM = New MKNetDXLib.ctl_MKDXLabel()
		CType(Me.Ctl_MKDXPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Ctl_MKDXPanel1.SuspendLayout()
		CType(Me.txb_Dir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_ScummVM.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'lbl_Explanation
		'
		Me.lbl_Explanation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_Explanation.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_Explanation.Location = New System.Drawing.Point(0, 0)
		Me.lbl_Explanation.MKBoundControl1 = Nothing
		Me.lbl_Explanation.MKBoundControl2 = Nothing
		Me.lbl_Explanation.MKBoundControl3 = Nothing
		Me.lbl_Explanation.MKBoundControl4 = Nothing
		Me.lbl_Explanation.MKBoundControl5 = Nothing
		Me.lbl_Explanation.Name = "lbl_Explanation"
		Me.lbl_Explanation.Size = New System.Drawing.Size(519, 26)
		Me.lbl_Explanation.TabIndex = 0
		Me.lbl_Explanation.Text = "A ScummVM version 2.0.0 or later is necessary to scan and add games. You can use earlier versions for lanching, though."
		'
		'Ctl_MKDXPanel1
		'
		Me.Ctl_MKDXPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.Ctl_MKDXPanel1.Controls.Add(Me.btn_No)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.btn_OK)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.btn_Browse_Dir)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.txb_Dir)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.lbl_Dir)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.btn_Browse_ScummVM)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.txb_ScummVM)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.lbl_ScummVM)
		Me.Ctl_MKDXPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Ctl_MKDXPanel1.Location = New System.Drawing.Point(0, 26)
		Me.Ctl_MKDXPanel1.Name = "Ctl_MKDXPanel1"
		Me.Ctl_MKDXPanel1.Size = New System.Drawing.Size(519, 79)
		Me.Ctl_MKDXPanel1.TabIndex = 1
		'
		'btn_No
		'
		Me.btn_No.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_No.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btn_No.Location = New System.Drawing.Point(440, 52)
		Me.btn_No.Name = "btn_No"
		Me.btn_No.Size = New System.Drawing.Size(75, 23)
		Me.btn_No.TabIndex = 9
		Me.btn_No.Text = "&Cancel"
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.Location = New System.Drawing.Point(362, 52)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 23)
		Me.btn_OK.TabIndex = 8
		Me.btn_OK.Text = "&OK"
		'
		'btn_Browse_Dir
		'
		Me.btn_Browse_Dir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Browse_Dir.Location = New System.Drawing.Point(479, 29)
		Me.btn_Browse_Dir.Name = "btn_Browse_Dir"
		Me.btn_Browse_Dir.Size = New System.Drawing.Size(37, 20)
		Me.btn_Browse_Dir.TabIndex = 5
		Me.btn_Browse_Dir.Text = "..."
		'
		'txb_Dir
		'
		Me.txb_Dir.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_Dir.Location = New System.Drawing.Point(113, 29)
		Me.txb_Dir.MKBoundLabel = Nothing
		Me.txb_Dir.MKEditValue_Compare = Nothing
		Me.txb_Dir.Name = "txb_Dir"
		Me.txb_Dir.Size = New System.Drawing.Size(363, 20)
		Me.txb_Dir.TabIndex = 4
		'
		'lbl_Dir
		'
		Me.lbl_Dir.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Dir.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Dir.Location = New System.Drawing.Point(3, 29)
		Me.lbl_Dir.MKBoundControl1 = Nothing
		Me.lbl_Dir.MKBoundControl2 = Nothing
		Me.lbl_Dir.MKBoundControl3 = Nothing
		Me.lbl_Dir.MKBoundControl4 = Nothing
		Me.lbl_Dir.MKBoundControl5 = Nothing
		Me.lbl_Dir.Name = "lbl_Dir"
		Me.lbl_Dir.Size = New System.Drawing.Size(107, 20)
		Me.lbl_Dir.TabIndex = 3
		Me.lbl_Dir.Text = "Directory to scan:"
		'
		'btn_Browse_ScummVM
		'
		Me.btn_Browse_ScummVM.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Browse_ScummVM.Location = New System.Drawing.Point(479, 6)
		Me.btn_Browse_ScummVM.Name = "btn_Browse_ScummVM"
		Me.btn_Browse_ScummVM.Size = New System.Drawing.Size(37, 20)
		Me.btn_Browse_ScummVM.TabIndex = 2
		Me.btn_Browse_ScummVM.Text = "..."
		'
		'txb_ScummVM
		'
		Me.txb_ScummVM.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_ScummVM.Location = New System.Drawing.Point(113, 6)
		Me.txb_ScummVM.MKBoundLabel = Nothing
		Me.txb_ScummVM.MKEditValue_Compare = Nothing
		Me.txb_ScummVM.Name = "txb_ScummVM"
		Me.txb_ScummVM.Size = New System.Drawing.Size(363, 20)
		Me.txb_ScummVM.TabIndex = 1
		'
		'lbl_ScummVM
		'
		Me.lbl_ScummVM.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_ScummVM.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_ScummVM.Location = New System.Drawing.Point(3, 6)
		Me.lbl_ScummVM.MKBoundControl1 = Nothing
		Me.lbl_ScummVM.MKBoundControl2 = Nothing
		Me.lbl_ScummVM.MKBoundControl3 = Nothing
		Me.lbl_ScummVM.MKBoundControl4 = Nothing
		Me.lbl_ScummVM.MKBoundControl5 = Nothing
		Me.lbl_ScummVM.Name = "lbl_ScummVM"
		Me.lbl_ScummVM.Size = New System.Drawing.Size(107, 20)
		Me.lbl_ScummVM.TabIndex = 0
		Me.lbl_ScummVM.Text = "ScummVM for scan:"
		'
		'frm_ScummVM_Scan
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(519, 105)
		Me.Controls.Add(Me.Ctl_MKDXPanel1)
		Me.Controls.Add(Me.lbl_Explanation)
		Me.MinimumSize = New System.Drawing.Size(314, 144)
		Me.Name = "frm_ScummVM_Scan"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Add ScummVM Games"
		CType(Me.Ctl_MKDXPanel1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Ctl_MKDXPanel1.ResumeLayout(False)
		CType(Me.txb_Dir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_ScummVM.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents lbl_Explanation As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents Ctl_MKDXPanel1 As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents btn_Browse_Dir As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents txb_Dir As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents lbl_Dir As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents btn_Browse_ScummVM As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents txb_ScummVM As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents lbl_ScummVM As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents btn_No As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
End Class
