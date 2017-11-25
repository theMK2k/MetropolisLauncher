<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_List_Generator_Edit
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
		Me.components = New System.ComponentModel.Container()
		Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem2 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem2 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_List_Generator_Edit))
		Dim SuperToolTip3 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem3 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem3 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip4 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem4 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem4 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip5 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem5 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem5 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip6 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem6 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem6 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Me.btn_Cancel = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.splt_Main = New MKNetDXLib.ctl_MKDXSplitPanel()
		Me.Ctl_MKDXGroupBox1 = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.txb_Main_Template = New MKNetDXLib.ctl_MKDXMemoEdit()
		Me.gb_File_Entry_Template = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.txb_File_Entry_Template = New MKNetDXLib.ctl_MKDXMemoEdit()
		Me.lbl_Name = New MKNetDXLib.ctl_MKDXLabel()
		Me.txb_Name = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.lbl_Sort = New MKNetDXLib.ctl_MKDXLabel()
		Me.cmb_Sort = New MKNetDXLib.ctl_MKDXLookupEdit()
		Me.BS_Sort_Order = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_ML = New Metropolis_Launcher.DS_ML()
		CType(Me.splt_Main, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splt_Main.SuspendLayout()
		CType(Me.Ctl_MKDXGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Ctl_MKDXGroupBox1.SuspendLayout()
		CType(Me.txb_Main_Template.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gb_File_Entry_Template, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_File_Entry_Template.SuspendLayout()
		CType(Me.txb_File_Entry_Template.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_Name.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.cmb_Sort.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Sort_Order, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'btn_Cancel
		'
		Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Cancel.Location = New System.Drawing.Point(367, 342)
		Me.btn_Cancel.Name = "btn_Cancel"
		Me.btn_Cancel.Size = New System.Drawing.Size(86, 20)
		Me.btn_Cancel.TabIndex = 3
		Me.btn_Cancel.Text = "&Cancel"
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.Location = New System.Drawing.Point(278, 342)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(86, 20)
		Me.btn_OK.TabIndex = 2
		Me.btn_OK.Text = "&OK"
		'
		'splt_Main
		'
		Me.splt_Main.Horizontal = False
		Me.splt_Main.Location = New System.Drawing.Point(3, 49)
		Me.splt_Main.Name = "splt_Main"
		Me.splt_Main.Panel1.Controls.Add(Me.Ctl_MKDXGroupBox1)
		Me.splt_Main.Panel1.Text = "Panel1"
		Me.splt_Main.Panel2.Controls.Add(Me.gb_File_Entry_Template)
		Me.splt_Main.Panel2.Text = "Panel2"
		Me.splt_Main.Size = New System.Drawing.Size(450, 290)
		Me.splt_Main.SplitterPosition = 143
		Me.splt_Main.TabIndex = 8
		Me.splt_Main.Text = "Ctl_MKDXSplitPanel1"
		'
		'Ctl_MKDXGroupBox1
		'
		Me.Ctl_MKDXGroupBox1.Controls.Add(Me.txb_Main_Template)
		Me.Ctl_MKDXGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Ctl_MKDXGroupBox1.Location = New System.Drawing.Point(0, 0)
		Me.Ctl_MKDXGroupBox1.Name = "Ctl_MKDXGroupBox1"
		Me.Ctl_MKDXGroupBox1.Size = New System.Drawing.Size(450, 143)
		Me.Ctl_MKDXGroupBox1.TabIndex = 0
		Me.Ctl_MKDXGroupBox1.Text = "List File Template"
		'
		'txb_Main_Template
		'
		Me.txb_Main_Template.Dock = System.Windows.Forms.DockStyle.Fill
		Me.txb_Main_Template.Location = New System.Drawing.Point(2, 20)
		Me.txb_Main_Template.MKBoundLabel = Nothing
		Me.txb_Main_Template.MKEditValue_Compare = Nothing
		Me.txb_Main_Template.Name = "txb_Main_Template"
		Me.txb_Main_Template.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.txb_Main_Template.Size = New System.Drawing.Size(446, 121)
		ToolTipTitleItem1.Text = "List File Template"
		ToolTipItem1.LeftIndent = 6
		ToolTipItem1.Text = "The List File Template defines the overall template of the list file. Use the var" &
		"iable %entries% to define where the dynamic content of the list should be placed" &
		"."
		SuperToolTip1.Items.Add(ToolTipTitleItem1)
		SuperToolTip1.Items.Add(ToolTipItem1)
		Me.txb_Main_Template.SuperTip = SuperToolTip1
		Me.txb_Main_Template.TabIndex = 0
		'
		'gb_File_Entry_Template
		'
		Me.gb_File_Entry_Template.Controls.Add(Me.txb_File_Entry_Template)
		Me.gb_File_Entry_Template.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gb_File_Entry_Template.Location = New System.Drawing.Point(0, 0)
		Me.gb_File_Entry_Template.Name = "gb_File_Entry_Template"
		Me.gb_File_Entry_Template.Size = New System.Drawing.Size(450, 142)
		Me.gb_File_Entry_Template.TabIndex = 1
		Me.gb_File_Entry_Template.Text = "File Entry Template"
		'
		'txb_File_Entry_Template
		'
		Me.txb_File_Entry_Template.Dock = System.Windows.Forms.DockStyle.Fill
		Me.txb_File_Entry_Template.Location = New System.Drawing.Point(2, 20)
		Me.txb_File_Entry_Template.MKBoundLabel = Nothing
		Me.txb_File_Entry_Template.MKEditValue_Compare = Nothing
		Me.txb_File_Entry_Template.Name = "txb_File_Entry_Template"
		Me.txb_File_Entry_Template.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.txb_File_Entry_Template.Size = New System.Drawing.Size(446, 120)
		ToolTipTitleItem2.Text = "File Entry Template"
		ToolTipItem2.LeftIndent = 6
		ToolTipItem2.Text = resources.GetString("ToolTipItem2.Text")
		SuperToolTip2.Items.Add(ToolTipTitleItem2)
		SuperToolTip2.Items.Add(ToolTipItem2)
		Me.txb_File_Entry_Template.SuperTip = SuperToolTip2
		Me.txb_File_Entry_Template.TabIndex = 0
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
		ToolTipTitleItem3.Text = "Name"
		ToolTipItem3.LeftIndent = 6
		ToolTipItem3.Text = "The Name of the List Generator"
		SuperToolTip3.Items.Add(ToolTipTitleItem3)
		SuperToolTip3.Items.Add(ToolTipItem3)
		Me.lbl_Name.SuperTip = SuperToolTip3
		Me.lbl_Name.TabIndex = 11
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
		Me.txb_Name.Size = New System.Drawing.Size(379, 20)
		ToolTipTitleItem4.Text = "Name"
		ToolTipItem4.LeftIndent = 6
		ToolTipItem4.Text = "The Name of the List Generator"
		SuperToolTip4.Items.Add(ToolTipTitleItem4)
		SuperToolTip4.Items.Add(ToolTipItem4)
		Me.txb_Name.SuperTip = SuperToolTip4
		Me.txb_Name.TabIndex = 0
		'
		'lbl_Sort
		'
		Me.lbl_Sort.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Sort.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Sort.Location = New System.Drawing.Point(3, 26)
		Me.lbl_Sort.MKBoundControl1 = Nothing
		Me.lbl_Sort.MKBoundControl2 = Nothing
		Me.lbl_Sort.MKBoundControl3 = Nothing
		Me.lbl_Sort.MKBoundControl4 = Nothing
		Me.lbl_Sort.MKBoundControl5 = Nothing
		Me.lbl_Sort.Name = "lbl_Sort"
		Me.lbl_Sort.Size = New System.Drawing.Size(68, 20)
		ToolTipTitleItem5.Text = "Sort Order"
		ToolTipItem5.LeftIndent = 6
		ToolTipItem5.Text = "When generating the list file define here in which order the files should be list" &
		"ed."
		SuperToolTip5.Items.Add(ToolTipTitleItem5)
		SuperToolTip5.Items.Add(ToolTipItem5)
		Me.lbl_Sort.SuperTip = SuperToolTip5
		Me.lbl_Sort.TabIndex = 12
		Me.lbl_Sort.Text = "Sort Order:"
		'
		'cmb_Sort
		'
		Me.cmb_Sort.Location = New System.Drawing.Point(74, 26)
		Me.cmb_Sort.MKBoundLabel = Nothing
		Me.cmb_Sort.MKEditValue_Compare = Nothing
		Me.cmb_Sort.Name = "cmb_Sort"
		Me.cmb_Sort.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.cmb_Sort.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_Sort", "id_Sort", 57, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name", 37, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near)})
		Me.cmb_Sort.Properties.DataSource = Me.BS_Sort_Order
		Me.cmb_Sort.Properties.DisplayMember = "Name"
		Me.cmb_Sort.Properties.NullText = ""
		Me.cmb_Sort.Properties.ShowFooter = False
		Me.cmb_Sort.Properties.ShowHeader = False
		Me.cmb_Sort.Properties.ValueMember = "id_Sort"
		Me.cmb_Sort.Size = New System.Drawing.Size(379, 20)
		ToolTipTitleItem6.Text = "Sort Order"
		ToolTipItem6.LeftIndent = 6
		ToolTipItem6.Text = "When generating the list file define here in which order the files should be list" &
		"ed."
		SuperToolTip6.Items.Add(ToolTipTitleItem6)
		SuperToolTip6.Items.Add(ToolTipItem6)
		Me.cmb_Sort.SuperTip = SuperToolTip6
		Me.cmb_Sort.TabIndex = 1
		'
		'BS_Sort_Order
		'
		Me.BS_Sort_Order.DataMember = "static_List_Generator_Sort"
		Me.BS_Sort_Order.DataSource = Me.DS_ML
		'
		'DS_ML
		'
		Me.DS_ML.DataSetName = "DS_ML"
		Me.DS_ML.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'frm_List_Generator_Edit
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(456, 365)
		Me.Controls.Add(Me.cmb_Sort)
		Me.Controls.Add(Me.lbl_Sort)
		Me.Controls.Add(Me.lbl_Name)
		Me.Controls.Add(Me.txb_Name)
		Me.Controls.Add(Me.splt_Main)
		Me.Controls.Add(Me.btn_Cancel)
		Me.Controls.Add(Me.btn_OK)
		Me.Name = "frm_List_Generator_Edit"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Edit List Generator"
		CType(Me.splt_Main, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splt_Main.ResumeLayout(False)
		CType(Me.Ctl_MKDXGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Ctl_MKDXGroupBox1.ResumeLayout(False)
		CType(Me.txb_Main_Template.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gb_File_Entry_Template, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_File_Entry_Template.ResumeLayout(False)
		CType(Me.txb_File_Entry_Template.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_Name.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.cmb_Sort.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Sort_Order, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents btn_Cancel As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents splt_Main As MKNetDXLib.ctl_MKDXSplitPanel
	Friend WithEvents lbl_Name As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents txb_Name As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents lbl_Sort As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents cmb_Sort As MKNetDXLib.ctl_MKDXLookupEdit
	Friend WithEvents Ctl_MKDXGroupBox1 As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents txb_Main_Template As MKNetDXLib.ctl_MKDXMemoEdit
	Friend WithEvents gb_File_Entry_Template As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents txb_File_Entry_Template As MKNetDXLib.ctl_MKDXMemoEdit
	Friend WithEvents DS_ML As DS_ML
	Friend WithEvents BS_Sort_Order As BindingSource
End Class
