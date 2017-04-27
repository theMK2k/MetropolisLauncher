<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Rescan_Options
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
		Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Me.btn_No = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.chb_Only_Missing_Files = New MKNetDXLib.ctl_MKDXCheckEdit()
		Me.lbl_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		CType(Me.chb_Only_Missing_Files.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'btn_No
		'
		Me.btn_No.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_No.DialogResult = System.Windows.Forms.DialogResult.No
		Me.btn_No.Location = New System.Drawing.Point(295, 42)
		Me.btn_No.Name = "btn_No"
		Me.btn_No.Size = New System.Drawing.Size(75, 23)
		Me.btn_No.TabIndex = 7
		Me.btn_No.Text = "&No"
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.DialogResult = System.Windows.Forms.DialogResult.Yes
		Me.btn_OK.Location = New System.Drawing.Point(217, 42)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 23)
		Me.btn_OK.TabIndex = 6
		Me.btn_OK.Text = "&Yes"
		'
		'chb_Only_Missing_Files
		'
		Me.chb_Only_Missing_Files.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.chb_Only_Missing_Files.Cursor = System.Windows.Forms.Cursors.Hand
		Me.chb_Only_Missing_Files.Location = New System.Drawing.Point(4, 45)
		Me.chb_Only_Missing_Files.MKBoundLabel = Nothing
		Me.chb_Only_Missing_Files.MKEditValue_Compare = Nothing
		Me.chb_Only_Missing_Files.Name = "chb_Only_Missing_Files"
		Me.chb_Only_Missing_Files.Properties.Caption = "Only rescan for missing files"
		Me.chb_Only_Missing_Files.Size = New System.Drawing.Size(210, 19)
		ToolTipTitleItem1.Text = "Only rescan for missing files"
		ToolTipItem1.LeftIndent = 6
		ToolTipItem1.Text = "If checked, a rescan for new/modified meta-data and MobyGames links will be skipp" &
		"ed."
		SuperToolTip1.Items.Add(ToolTipTitleItem1)
		SuperToolTip1.Items.Add(ToolTipItem1)
		Me.chb_Only_Missing_Files.SuperTip = SuperToolTip1
		Me.chb_Only_Missing_Files.TabIndex = 8
		'
		'lbl_Explanation
		'
		Me.lbl_Explanation.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
		Me.lbl_Explanation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Explanation.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_Explanation.Location = New System.Drawing.Point(0, 0)
		Me.lbl_Explanation.MKBoundControl1 = Nothing
		Me.lbl_Explanation.MKBoundControl2 = Nothing
		Me.lbl_Explanation.MKBoundControl3 = Nothing
		Me.lbl_Explanation.MKBoundControl4 = Nothing
		Me.lbl_Explanation.MKBoundControl5 = Nothing
		Me.lbl_Explanation.Name = "lbl_Explanation"
		Me.lbl_Explanation.Size = New System.Drawing.Size(373, 38)
		Me.lbl_Explanation.TabIndex = 9
		'
		'frm_Rescan_Options
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(373, 68)
		Me.Controls.Add(Me.lbl_Explanation)
		Me.Controls.Add(Me.chb_Only_Missing_Files)
		Me.Controls.Add(Me.btn_No)
		Me.Controls.Add(Me.btn_OK)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.MinimumSize = New System.Drawing.Size(389, 107)
		Me.Name = "frm_Rescan_Options"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Rescan Games"
		CType(Me.chb_Only_Missing_Files.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents btn_No As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents chb_Only_Missing_Files As MKNetDXLib.ctl_MKDXCheckEdit
	Friend WithEvents lbl_Explanation As MKNetDXLib.ctl_MKDXLabel
End Class
