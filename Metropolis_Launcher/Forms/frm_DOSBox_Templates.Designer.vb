<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DOSBox_Templates
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
		Me.components = New System.ComponentModel.Container()
		Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem2 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem2 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip3 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem3 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem3 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Me.splt_Main = New MKNetDXLib.ctl_MKDXSplitPanel()
		Me.btn_Duplicate_Template = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Delete = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Add = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.grd_DOSBox_Configs = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_DOSBox_Configs = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_ML = New Metropolis_Launcher.DS_ML()
		Me.gv_DOSBox_Config = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colDisplayname = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.ucr_DOSBox_Config = New Metropolis_Launcher.ucr_DOSBox_Config()
		Me.barmng = New MKNetDXLib.ctl_MKDXBarManager()
		Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
		Me.bbi_Add = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Duplicate = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Delete = New DevExpress.XtraBars.BarButtonItem()
		Me.popmnu_DOSBox_Templates = New MKNetDXLib.cmp_MKDXPopupMenu()
		CType(Me.splt_Main, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splt_Main.SuspendLayout()
		CType(Me.grd_DOSBox_Configs, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_DOSBox_Configs, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_DOSBox_Config, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.barmng, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.popmnu_DOSBox_Templates, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'splt_Main
		'
		Me.splt_Main.Dock = System.Windows.Forms.DockStyle.Fill
		Me.splt_Main.Location = New System.Drawing.Point(0, 0)
		Me.splt_Main.Name = "splt_Main"
		Me.splt_Main.Panel1.Controls.Add(Me.btn_Duplicate_Template)
		Me.splt_Main.Panel1.Controls.Add(Me.btn_Delete)
		Me.splt_Main.Panel1.Controls.Add(Me.btn_Add)
		Me.splt_Main.Panel1.Controls.Add(Me.grd_DOSBox_Configs)
		Me.splt_Main.Panel1.MinSize = 237
		Me.splt_Main.Panel1.Text = "Panel1"
		Me.splt_Main.Panel2.Controls.Add(Me.ucr_DOSBox_Config)
		Me.splt_Main.Panel2.Text = "Panel2"
		Me.splt_Main.Size = New System.Drawing.Size(784, 447)
		Me.splt_Main.SplitterPosition = 237
		Me.splt_Main.TabIndex = 0
		Me.splt_Main.Text = "Ctl_MKDXSplitPanel1"
		'
		'btn_Duplicate_Template
		'
		Me.btn_Duplicate_Template.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btn_Duplicate_Template.Location = New System.Drawing.Point(81, 421)
		Me.btn_Duplicate_Template.Name = "btn_Duplicate_Template"
		Me.btn_Duplicate_Template.Size = New System.Drawing.Size(75, 23)
		ToolTipTitleItem1.Text = "Duplicate"
		ToolTipItem1.LeftIndent = 6
		ToolTipItem1.Text = "Duplicate the selected DOSBox Template"
		SuperToolTip1.Items.Add(ToolTipTitleItem1)
		SuperToolTip1.Items.Add(ToolTipItem1)
		Me.btn_Duplicate_Template.SuperTip = SuperToolTip1
		Me.btn_Duplicate_Template.TabIndex = 2
		Me.btn_Duplicate_Template.Text = "D&uplicate"
		'
		'btn_Delete
		'
		Me.btn_Delete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btn_Delete.Location = New System.Drawing.Point(159, 421)
		Me.btn_Delete.Name = "btn_Delete"
		Me.btn_Delete.Size = New System.Drawing.Size(75, 23)
		ToolTipTitleItem2.Text = "Delete"
		ToolTipItem2.LeftIndent = 6
		ToolTipItem2.Text = "Delete the current DOSBox Template."
		SuperToolTip2.Items.Add(ToolTipTitleItem2)
		SuperToolTip2.Items.Add(ToolTipItem2)
		Me.btn_Delete.SuperTip = SuperToolTip2
		Me.btn_Delete.TabIndex = 3
		Me.btn_Delete.Text = "&Delete"
		'
		'btn_Add
		'
		Me.btn_Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btn_Add.Location = New System.Drawing.Point(3, 421)
		Me.btn_Add.Name = "btn_Add"
		Me.btn_Add.Size = New System.Drawing.Size(75, 23)
		ToolTipTitleItem3.Text = "Add"
		ToolTipItem3.LeftIndent = 6
		ToolTipItem3.Text = "Add a new DOSBox Template to the list."
		SuperToolTip3.Items.Add(ToolTipTitleItem3)
		SuperToolTip3.Items.Add(ToolTipItem3)
		Me.btn_Add.SuperTip = SuperToolTip3
		Me.btn_Add.TabIndex = 1
		Me.btn_Add.Text = "&Add"
		'
		'grd_DOSBox_Configs
		'
		Me.grd_DOSBox_Configs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.grd_DOSBox_Configs.DataSource = Me.BS_DOSBox_Configs
		Me.grd_DOSBox_Configs.Location = New System.Drawing.Point(3, 3)
		Me.grd_DOSBox_Configs.MainView = Me.gv_DOSBox_Config
		Me.grd_DOSBox_Configs.Name = "grd_DOSBox_Configs"
		Me.grd_DOSBox_Configs.Size = New System.Drawing.Size(232, 415)
		Me.grd_DOSBox_Configs.TabIndex = 0
		Me.grd_DOSBox_Configs.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_DOSBox_Config})
		'
		'BS_DOSBox_Configs
		'
		Me.BS_DOSBox_Configs.DataMember = "tbl_DOSBox_Configs"
		Me.BS_DOSBox_Configs.DataSource = Me.DS_ML
		'
		'DS_ML
		'
		Me.DS_ML.DataSetName = "DS_ML"
		Me.DS_ML.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'gv_DOSBox_Config
		'
		Me.gv_DOSBox_Config.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colDisplayname})
		Me.gv_DOSBox_Config.GridControl = Me.grd_DOSBox_Configs
		Me.gv_DOSBox_Config.Name = "gv_DOSBox_Config"
		Me.gv_DOSBox_Config.OptionsSelection.InvertSelection = True
		Me.gv_DOSBox_Config.OptionsView.ShowGroupPanel = False
		Me.gv_DOSBox_Config.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.[False]
		Me.gv_DOSBox_Config.OptionsView.ShowIndicator = False
		'
		'colDisplayname
		'
		Me.colDisplayname.Caption = "DOSBox Template"
		Me.colDisplayname.FieldName = "Displayname"
		Me.colDisplayname.Name = "colDisplayname"
		Me.colDisplayname.OptionsColumn.AllowEdit = False
		Me.colDisplayname.OptionsColumn.ReadOnly = True
		Me.colDisplayname.Visible = True
		Me.colDisplayname.VisibleIndex = 0
		'
		'ucr_DOSBox_Config
		'
		Me.ucr_DOSBox_Config.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ucr_DOSBox_Config.Location = New System.Drawing.Point(0, 3)
		Me.ucr_DOSBox_Config.Name = "ucr_DOSBox_Config"
		Me.ucr_DOSBox_Config.Size = New System.Drawing.Size(539, 441)
		Me.ucr_DOSBox_Config.TabIndex = 0
		'
		'barmng
		'
		Me.barmng.DockControls.Add(Me.barDockControlTop)
		Me.barmng.DockControls.Add(Me.barDockControlBottom)
		Me.barmng.DockControls.Add(Me.barDockControlLeft)
		Me.barmng.DockControls.Add(Me.barDockControlRight)
		Me.barmng.Form = Me
		Me.barmng.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.bbi_Add, Me.bbi_Duplicate, Me.bbi_Delete})
		Me.barmng.MaxItemId = 3
		'
		'barDockControlTop
		'
		Me.barDockControlTop.CausesValidation = False
		Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
		Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
		Me.barDockControlTop.Size = New System.Drawing.Size(784, 0)
		'
		'barDockControlBottom
		'
		Me.barDockControlBottom.CausesValidation = False
		Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.barDockControlBottom.Location = New System.Drawing.Point(0, 447)
		Me.barDockControlBottom.Size = New System.Drawing.Size(784, 0)
		'
		'barDockControlLeft
		'
		Me.barDockControlLeft.CausesValidation = False
		Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
		Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
		Me.barDockControlLeft.Size = New System.Drawing.Size(0, 447)
		'
		'barDockControlRight
		'
		Me.barDockControlRight.CausesValidation = False
		Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
		Me.barDockControlRight.Location = New System.Drawing.Point(784, 0)
		Me.barDockControlRight.Size = New System.Drawing.Size(0, 447)
		'
		'bbi_Add
		'
		Me.bbi_Add.Caption = "&Add"
		Me.bbi_Add.Id = 0
		Me.bbi_Add.ImageUri.Uri = "Add"
		Me.bbi_Add.Name = "bbi_Add"
		'
		'bbi_Duplicate
		'
		Me.bbi_Duplicate.Caption = "&Duplicate"
		Me.bbi_Duplicate.Id = 1
		Me.bbi_Duplicate.ImageUri.Uri = "Copy"
		Me.bbi_Duplicate.Name = "bbi_Duplicate"
		'
		'bbi_Delete
		'
		Me.bbi_Delete.Caption = "De&lete"
		Me.bbi_Delete.Id = 2
		Me.bbi_Delete.ImageUri.Uri = "Delete"
		Me.bbi_Delete.Name = "bbi_Delete"
		'
		'popmnu_DOSBox_Templates
		'
		Me.popmnu_DOSBox_Templates.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Add), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Duplicate), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Delete)})
		Me.popmnu_DOSBox_Templates.Manager = Me.barmng
		Me.popmnu_DOSBox_Templates.Name = "popmnu_DOSBox_Templates"
		'
		'frm_DOSBox_Templates
		'
		Me.ClientSize = New System.Drawing.Size(784, 447)
		Me.Controls.Add(Me.splt_Main)
		Me.Controls.Add(Me.barDockControlLeft)
		Me.Controls.Add(Me.barDockControlRight)
		Me.Controls.Add(Me.barDockControlBottom)
		Me.Controls.Add(Me.barDockControlTop)
		Me.Name = "frm_DOSBox_Templates"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "DOSBox Templates"
		CType(Me.splt_Main, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splt_Main.ResumeLayout(False)
		CType(Me.grd_DOSBox_Configs, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_DOSBox_Configs, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_DOSBox_Config, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.barmng, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.popmnu_DOSBox_Templates, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents splt_Main As MKNetDXLib.ctl_MKDXSplitPanel
	Friend WithEvents grd_DOSBox_Configs As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents BS_DOSBox_Configs As System.Windows.Forms.BindingSource
	Friend WithEvents DS_ML As Metropolis_Launcher.DS_ML
	Friend WithEvents gv_DOSBox_Config As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents colDisplayname As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents btn_Delete As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_Add As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents ucr_DOSBox_Config As Metropolis_Launcher.ucr_DOSBox_Config
	Friend WithEvents btn_Duplicate_Template As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents barmng As MKNetDXLib.ctl_MKDXBarManager
	Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
	Friend WithEvents bbi_Add As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Duplicate As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Delete As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents popmnu_DOSBox_Templates As MKNetDXLib.cmp_MKDXPopupMenu
End Class
