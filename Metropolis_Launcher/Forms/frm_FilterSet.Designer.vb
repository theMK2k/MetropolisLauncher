<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_FilterSet
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
		Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem2 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim SuperToolTip3 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem3 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Me.lbl_Name = New MKNetDXLib.ctl_MKDXLabel()
		Me.txb_Name = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.lbl_Quickfilter = New MKNetDXLib.ctl_MKDXLabel()
		Me.chk_QuickFilter = New MKNetDXLib.ctl_MKDXCheckEdit()
		Me.btn_GetQuickfilter = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Cancel = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.lbl_Current_Quickfilter = New MKNetDXLib.ctl_MKDXLabel()
		Me.btn_RemoveQuickFilter = New MKNetDXLib.ctl_MKDXSimpleButton()
		CType(Me.txb_Name.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.chk_QuickFilter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'lbl_Name
		'
		Me.lbl_Name.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Name.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Name.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Name.MKBoundControl1 = Nothing
		Me.lbl_Name.MKBoundControl2 = Nothing
		Me.lbl_Name.MKBoundControl3 = Nothing
		Me.lbl_Name.MKBoundControl4 = Nothing
		Me.lbl_Name.MKBoundControl5 = Nothing
		Me.lbl_Name.Name = "lbl_Name"
		Me.lbl_Name.Size = New System.Drawing.Size(68, 20)
		Me.lbl_Name.TabIndex = 9
		Me.lbl_Name.Text = "Name:"
		'
		'txb_Name
		'
		Me.txb_Name.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_Name.Location = New System.Drawing.Point(74, 3)
		Me.txb_Name.MKBoundLabel = Nothing
		Me.txb_Name.MKEditValue_Compare = Nothing
		Me.txb_Name.Name = "txb_Name"
		Me.txb_Name.Size = New System.Drawing.Size(364, 20)
		Me.txb_Name.TabIndex = 0
		'
		'lbl_Quickfilter
		'
		Me.lbl_Quickfilter.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Quickfilter.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Quickfilter.Location = New System.Drawing.Point(3, 26)
		Me.lbl_Quickfilter.MKBoundControl1 = Nothing
		Me.lbl_Quickfilter.MKBoundControl2 = Nothing
		Me.lbl_Quickfilter.MKBoundControl3 = Nothing
		Me.lbl_Quickfilter.MKBoundControl4 = Nothing
		Me.lbl_Quickfilter.MKBoundControl5 = Nothing
		Me.lbl_Quickfilter.Name = "lbl_Quickfilter"
		Me.lbl_Quickfilter.Size = New System.Drawing.Size(68, 20)
		Me.lbl_Quickfilter.TabIndex = 9
		Me.lbl_Quickfilter.Text = "Quickfilter:"
		Me.lbl_Quickfilter.Visible = False
		'
		'chk_QuickFilter
		'
		Me.chk_QuickFilter.Cursor = System.Windows.Forms.Cursors.Hand
		Me.chk_QuickFilter.Location = New System.Drawing.Point(346, 27)
		Me.chk_QuickFilter.MKBoundLabel = Nothing
		Me.chk_QuickFilter.MKEditValue_Compare = Nothing
		Me.chk_QuickFilter.Name = "chk_QuickFilter"
		Me.chk_QuickFilter.Properties.Caption = "Use Quickfilter"
		Me.chk_QuickFilter.Size = New System.Drawing.Size(92, 19)
		ToolTipTitleItem1.Text = "Use Quickfilter settings in this filterset"
		SuperToolTip1.Items.Add(ToolTipTitleItem1)
		Me.chk_QuickFilter.SuperTip = SuperToolTip1
		Me.chk_QuickFilter.TabIndex = 3
		Me.chk_QuickFilter.Visible = False
		'
		'btn_GetQuickfilter
		'
		Me.btn_GetQuickfilter.Location = New System.Drawing.Point(168, 26)
		Me.btn_GetQuickfilter.Name = "btn_GetQuickfilter"
		Me.btn_GetQuickfilter.Size = New System.Drawing.Size(86, 20)
		ToolTipTitleItem2.Text = "Get the Quickfilter Settings from the current List"
		SuperToolTip2.Items.Add(ToolTipTitleItem2)
		Me.btn_GetQuickfilter.SuperTip = SuperToolTip2
		Me.btn_GetQuickfilter.TabIndex = 1
		Me.btn_GetQuickfilter.Text = "&Get from List"
		Me.btn_GetQuickfilter.Visible = False
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.Location = New System.Drawing.Point(263, 49)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(86, 20)
		Me.btn_OK.TabIndex = 4
		Me.btn_OK.Text = "&OK"
		'
		'btn_Cancel
		'
		Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Cancel.Location = New System.Drawing.Point(352, 49)
		Me.btn_Cancel.Name = "btn_Cancel"
		Me.btn_Cancel.Size = New System.Drawing.Size(86, 20)
		Me.btn_Cancel.TabIndex = 5
		Me.btn_Cancel.Text = "&Cancel"
		'
		'lbl_Current_Quickfilter
		'
		Me.lbl_Current_Quickfilter.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Current_Quickfilter.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Current_Quickfilter.Location = New System.Drawing.Point(74, 26)
		Me.lbl_Current_Quickfilter.MKBoundControl1 = Nothing
		Me.lbl_Current_Quickfilter.MKBoundControl2 = Nothing
		Me.lbl_Current_Quickfilter.MKBoundControl3 = Nothing
		Me.lbl_Current_Quickfilter.MKBoundControl4 = Nothing
		Me.lbl_Current_Quickfilter.MKBoundControl5 = Nothing
		Me.lbl_Current_Quickfilter.Name = "lbl_Current_Quickfilter"
		Me.lbl_Current_Quickfilter.Size = New System.Drawing.Size(91, 20)
		Me.lbl_Current_Quickfilter.TabIndex = 9
		Me.lbl_Current_Quickfilter.Text = "No Quickfilter"
		Me.lbl_Current_Quickfilter.Visible = False
		'
		'btn_RemoveQuickFilter
		'
		Me.btn_RemoveQuickFilter.Location = New System.Drawing.Point(257, 26)
		Me.btn_RemoveQuickFilter.Name = "btn_RemoveQuickFilter"
		Me.btn_RemoveQuickFilter.Size = New System.Drawing.Size(86, 20)
		ToolTipTitleItem3.Text = "Remove the Quickfilter Settings"
		SuperToolTip3.Items.Add(ToolTipTitleItem3)
		Me.btn_RemoveQuickFilter.SuperTip = SuperToolTip3
		Me.btn_RemoveQuickFilter.TabIndex = 2
		Me.btn_RemoveQuickFilter.Text = "&Remove"
		Me.btn_RemoveQuickFilter.Visible = False
		'
		'frm_FilterSet
		'
		Me.ClientSize = New System.Drawing.Size(441, 72)
		Me.Controls.Add(Me.btn_Cancel)
		Me.Controls.Add(Me.btn_OK)
		Me.Controls.Add(Me.btn_RemoveQuickFilter)
		Me.Controls.Add(Me.btn_GetQuickfilter)
		Me.Controls.Add(Me.chk_QuickFilter)
		Me.Controls.Add(Me.lbl_Current_Quickfilter)
		Me.Controls.Add(Me.lbl_Quickfilter)
		Me.Controls.Add(Me.lbl_Name)
		Me.Controls.Add(Me.txb_Name)
		Me.Name = "frm_FilterSet"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Edit Filterset"
		CType(Me.txb_Name.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.chk_QuickFilter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents lbl_Name As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents txb_Name As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents lbl_Quickfilter As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents chk_QuickFilter As MKNetDXLib.ctl_MKDXCheckEdit
	Friend WithEvents btn_GetQuickfilter As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_Cancel As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents lbl_Current_Quickfilter As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents btn_RemoveQuickFilter As MKNetDXLib.ctl_MKDXSimpleButton

End Class
