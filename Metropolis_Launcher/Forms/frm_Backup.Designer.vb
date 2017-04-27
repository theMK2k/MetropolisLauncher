<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Backup
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
		Me.mpb_Progress = New MKNetDXLib.ctl_MKDXMarqueeProgressBar()
		Me.lbl_Progress = New MKNetDXLib.ctl_MKDXLabel()
		CType(Me.mpb_Progress.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'mpb_Progress
		'
		Me.mpb_Progress.EditValue = 0
		Me.mpb_Progress.Location = New System.Drawing.Point(2, 19)
		Me.mpb_Progress.Name = "mpb_Progress"
		Me.mpb_Progress.Properties.ProgressAnimationMode = DevExpress.Utils.Drawing.ProgressAnimationMode.Cycle
		Me.mpb_Progress.Size = New System.Drawing.Size(376, 18)
		Me.mpb_Progress.TabIndex = 5
		'
		'lbl_Progress
		'
		Me.lbl_Progress.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Progress.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
		Me.lbl_Progress.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_Progress.Location = New System.Drawing.Point(0, 0)
		Me.lbl_Progress.MKBoundControl1 = Nothing
		Me.lbl_Progress.MKBoundControl2 = Nothing
		Me.lbl_Progress.MKBoundControl3 = Nothing
		Me.lbl_Progress.MKBoundControl4 = Nothing
		Me.lbl_Progress.MKBoundControl5 = Nothing
		Me.lbl_Progress.Name = "lbl_Progress"
		Me.lbl_Progress.Size = New System.Drawing.Size(166, 13)
		Me.lbl_Progress.TabIndex = 4
		Me.lbl_Progress.Text = "Please wait - Backup in progress..."
		'
		'frm_Backup
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(381, 53)
		Me.ControlBox = False
		Me.Controls.Add(Me.mpb_Progress)
		Me.Controls.Add(Me.lbl_Progress)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Name = "frm_Backup"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Metropolis Launcher"
		Me.TopMost = True
		CType(Me.mpb_Progress.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents mpb_Progress As MKNetDXLib.ctl_MKDXMarqueeProgressBar
	Friend WithEvents lbl_Progress As MKNetDXLib.ctl_MKDXLabel
End Class
