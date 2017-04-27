<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Rom_Manager_ChangeDirectory
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
		Me.lbl_New = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Old = New MKNetDXLib.ctl_MKDXLabel()
		Me.btn_Cancel = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.txb_NewDir = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.txb_OldDir = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.btn_OpenDir = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.lbl_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		CType(Me.txb_NewDir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_OldDir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'lbl_New
		'
		Me.lbl_New.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_New.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_New.Location = New System.Drawing.Point(3, 49)
		Me.lbl_New.MKBoundControl1 = Nothing
		Me.lbl_New.MKBoundControl2 = Nothing
		Me.lbl_New.MKBoundControl3 = Nothing
		Me.lbl_New.MKBoundControl4 = Nothing
		Me.lbl_New.MKBoundControl5 = Nothing
		Me.lbl_New.Name = "lbl_New"
		Me.lbl_New.Size = New System.Drawing.Size(110, 20)
		Me.lbl_New.TabIndex = 6
		Me.lbl_New.Text = "New Directory:"
		'
		'lbl_Old
		'
		Me.lbl_Old.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Old.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Old.Location = New System.Drawing.Point(3, 26)
		Me.lbl_Old.MKBoundControl1 = Nothing
		Me.lbl_Old.MKBoundControl2 = Nothing
		Me.lbl_Old.MKBoundControl3 = Nothing
		Me.lbl_Old.MKBoundControl4 = Nothing
		Me.lbl_Old.MKBoundControl5 = Nothing
		Me.lbl_Old.Name = "lbl_Old"
		Me.lbl_Old.Size = New System.Drawing.Size(110, 20)
		Me.lbl_Old.TabIndex = 5
		Me.lbl_Old.Text = "Old Directory:"
		'
		'btn_Cancel
		'
		Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Cancel.Location = New System.Drawing.Point(350, 87)
		Me.btn_Cancel.Name = "btn_Cancel"
		Me.btn_Cancel.Size = New System.Drawing.Size(75, 23)
		Me.btn_Cancel.TabIndex = 4
		Me.btn_Cancel.Text = "&Cancel"
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.Location = New System.Drawing.Point(272, 87)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 23)
		Me.btn_OK.TabIndex = 3
		Me.btn_OK.Text = "&OK"
		'
		'txb_NewDir
		'
		Me.txb_NewDir.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_NewDir.Location = New System.Drawing.Point(116, 49)
		Me.txb_NewDir.MKBoundLabel = Nothing
		Me.txb_NewDir.MKEditValue_Compare = Nothing
		Me.txb_NewDir.Name = "txb_NewDir"
		Me.txb_NewDir.Size = New System.Drawing.Size(266, 20)
		Me.txb_NewDir.TabIndex = 1
		'
		'txb_OldDir
		'
		Me.txb_OldDir.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_OldDir.Location = New System.Drawing.Point(116, 26)
		Me.txb_OldDir.MKBoundLabel = Nothing
		Me.txb_OldDir.MKEditValue_Compare = Nothing
		Me.txb_OldDir.Name = "txb_OldDir"
		Me.txb_OldDir.Properties.ReadOnly = True
		Me.txb_OldDir.Size = New System.Drawing.Size(309, 20)
		Me.txb_OldDir.TabIndex = 0
		'
		'btn_OpenDir
		'
		Me.btn_OpenDir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OpenDir.Location = New System.Drawing.Point(385, 49)
		Me.btn_OpenDir.Name = "btn_OpenDir"
		Me.btn_OpenDir.Size = New System.Drawing.Size(40, 20)
		Me.btn_OpenDir.TabIndex = 2
		Me.btn_OpenDir.Text = "..."
		'
		'lbl_Explanation
		'
		Me.lbl_Explanation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Explanation.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
		Me.lbl_Explanation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Explanation.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Explanation.MKBoundControl1 = Nothing
		Me.lbl_Explanation.MKBoundControl2 = Nothing
		Me.lbl_Explanation.MKBoundControl3 = Nothing
		Me.lbl_Explanation.MKBoundControl4 = Nothing
		Me.lbl_Explanation.MKBoundControl5 = Nothing
		Me.lbl_Explanation.Name = "lbl_Explanation"
		Me.lbl_Explanation.Size = New System.Drawing.Size(422, 20)
		Me.lbl_Explanation.TabIndex = 14
		Me.lbl_Explanation.Text = "Changing this directory will affect %1% games."
		'
		'frm_Rom_Manager_ChangeDirectory
		'
		Me.ClientSize = New System.Drawing.Size(428, 113)
		Me.Controls.Add(Me.lbl_Explanation)
		Me.Controls.Add(Me.txb_NewDir)
		Me.Controls.Add(Me.txb_OldDir)
		Me.Controls.Add(Me.btn_OpenDir)
		Me.Controls.Add(Me.btn_Cancel)
		Me.Controls.Add(Me.btn_OK)
		Me.Controls.Add(Me.lbl_New)
		Me.Controls.Add(Me.lbl_Old)
		Me.Name = "frm_Rom_Manager_ChangeDirectory"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Change Directory"
		CType(Me.txb_NewDir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_OldDir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents lbl_New As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Old As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents btn_Cancel As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents txb_NewDir As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents txb_OldDir As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents btn_OpenDir As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents lbl_Explanation As MKNetDXLib.ctl_MKDXLabel

End Class
