<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Description
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
		Me.txb_Description = New MKNetDXLib.ctl_MKDXMemoEdit()
		Me.lbl_Title = New MKNetDXLib.ctl_MKDXLabel()
		CType(Me.txb_Description.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'txb_Description
		'
		Me.txb_Description.Dock = System.Windows.Forms.DockStyle.Fill
		Me.txb_Description.Location = New System.Drawing.Point(0, 36)
		Me.txb_Description.MKBoundLabel = Nothing
		Me.txb_Description.MKEditValue_Compare = Nothing
		Me.txb_Description.Name = "txb_Description"
		Me.txb_Description.Properties.ReadOnly = True
		Me.txb_Description.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txb_Description.Size = New System.Drawing.Size(423, 284)
		Me.txb_Description.TabIndex = 1
		'
		'lbl_Title
		'
		Me.lbl_Title.Appearance.BackColor = System.Drawing.Color.Transparent
		Me.lbl_Title.Appearance.Font = New System.Drawing.Font("Segoe UI", 16.0!)
		Me.lbl_Title.AutoEllipsis = True
		Me.lbl_Title.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Title.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_Title.Location = New System.Drawing.Point(0, 0)
		Me.lbl_Title.MKBoundControl1 = Nothing
		Me.lbl_Title.MKBoundControl2 = Nothing
		Me.lbl_Title.MKBoundControl3 = Nothing
		Me.lbl_Title.MKBoundControl4 = Nothing
		Me.lbl_Title.MKBoundControl5 = Nothing
		Me.lbl_Title.Name = "lbl_Title"
		Me.lbl_Title.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_Title.Size = New System.Drawing.Size(423, 36)
		Me.lbl_Title.TabIndex = 0
		'
		'frm_Description
		'
		Me.ClientSize = New System.Drawing.Size(423, 320)
		Me.Controls.Add(Me.txb_Description)
		Me.Controls.Add(Me.lbl_Title)
		Me.Name = "frm_Description"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		CType(Me.txb_Description.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents txb_Description As MKNetDXLib.ctl_MKDXMemoEdit
	Friend WithEvents lbl_Title As MKNetDXLib.ctl_MKDXLabel

End Class
