<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_Known_Emulators
	Inherits MKNetDXLib.frm_MKDXBaseForm

	'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
	<System.Diagnostics.DebuggerNonUserCode()>
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
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem2 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim SuperToolTip3 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem3 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
		Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
		Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
		Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
		Dim SuperToolTip4 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip5 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem4 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem2 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Known_Emulators))
		Dim SuperToolTip6 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem5 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem3 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip7 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem6 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem4 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip8 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem7 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem5 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip9 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem8 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem6 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip10 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem9 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem7 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip11 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem10 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim SuperToolTip12 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem11 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Me.gb_Emulators = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_Emulators = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_Emulators = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_Rombase = New Metropolis_Launcher.DS_Rombase()
		Me.gv_Emulators = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colDisplayname = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colIdentifier = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.pnl_Emulators_Buttons = New MKNetDXLib.ctl_MKDXPanel()
		Me.btn_Delete_Known_Emulator = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Import_Known_Emulator = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Add_Known_Emulator = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.DS_ML = New Metropolis_Launcher.DS_ML()
		Me.Ctl_MKDXSplitter1 = New MKNetDXLib.ctl_MKDXSplitter()
		Me.pnl_Platforms = New MKNetDXLib.ctl_MKDXPanel()
		Me.grd_Platforms = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_Platforms = New System.Windows.Forms.BindingSource(Me.components)
		Me.gv_Platforms = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colDisplay_Name = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colSupported = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.rpi_Supported = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
		Me.rpi_DefaultEmulator = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
		Me.lbl_Platforms = New MKNetDXLib.ctl_MKDXLabel()
		Me.btn_Save = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.Ctl_MKDXSplitter2 = New MKNetDXLib.ctl_MKDXSplitter()
		Me.pnl_Settings = New MKNetDXLib.ctl_MKDXPanel()
		Me.tcl_Settings = New MKNetDXLib.ctl_MKDXTabControl()
		Me.tpg_Settings = New DevExpress.XtraTab.XtraTabPage()
		Me.Ctl_MKDXSplitPanel1 = New MKNetDXLib.ctl_MKDXSplitPanel()
		Me.lbl_Description = New MKNetDXLib.ctl_MKDXLabel()
		Me.txb_Description = New MKNetDXLib.ctl_MKDXMemoEdit()
		Me.barmng = New MKNetDXLib.ctl_MKDXBarManager()
		Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
		Me.bbi_Add = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Import = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Delete = New DevExpress.XtraBars.BarButtonItem()
		Me.Ctl_MKDXMemoEdit1 = New MKNetDXLib.ctl_MKDXMemoEdit()
		Me.lbl_Autoconfig_Note = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Exe_Identifier_Regex = New MKNetDXLib.ctl_MKDXLabel()
		Me.txb_Exe_Identifier_Regex = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.lbl_URL_Download = New MKNetDXLib.ctl_MKDXLabel()
		Me.txb_URL_Download = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.lbl_URL_Website = New MKNetDXLib.ctl_MKDXLabel()
		Me.txb_URL_Website = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.lbl_Identifier = New MKNetDXLib.ctl_MKDXLabel()
		Me.txb_Identifier = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.cmb_List_Generator = New MKNetDXLib.ctl_MKDXLookupEdit()
		Me.BS_List_Generators = New System.Windows.Forms.BindingSource(Me.components)
		Me.lbl_List_Generator = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Name = New MKNetDXLib.ctl_MKDXLabel()
		Me.txb_ScreenshotDirectory = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.txb_StartupParameter = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.txb_Name = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.lbl_StartupParameter = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Screenshot_Directory = New MKNetDXLib.ctl_MKDXLabel()
		Me.tpg_MV_Settings = New DevExpress.XtraTab.XtraTabPage()
		Me.grd_MV = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_MV = New System.Windows.Forms.BindingSource(Me.components)
		Me.gv_MV = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colVolume_Number = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.rpi_MV = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		Me.BS_Volumes = New System.Windows.Forms.BindingSource(Me.components)
		Me.colParameter = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.pnl_MV_Buttons = New MKNetDXLib.ctl_MKDXPanel()
		Me.btn_MV_Delete = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_MV_Add = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.BTA_Libretro_Core = New MKNetLib.cmp_MKBindableTableAdapter(Me.components)
		Me.BS_J2K = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_J2K = New System.Data.DataSet()
		Me.tbl_Config = New System.Data.DataTable()
		Me.DataColumn5 = New System.Data.DataColumn()
		Me.DataColumn6 = New System.Data.DataColumn()
		Me.BS_DOSBox_Patches_Categories = New System.Windows.Forms.BindingSource(Me.components)
		Me.BS_DOSBox_Patches = New System.Windows.Forms.BindingSource(Me.components)
		Me.btn_Close = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.BS_rpi_Platforms = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_MobyDB = New Metropolis_Launcher.DS_MobyDB()
		Me.pnl_Right = New MKNetDXLib.ctl_MKDXPanel()
		Me.pnl_Buttons = New MKNetDXLib.ctl_MKDXPanel()
		Me.popmnu_Emulators = New MKNetDXLib.cmp_MKDXPopupMenu()
		Me.DataTable1 = New System.Data.DataTable()
		Me.DataColumn1 = New System.Data.DataColumn()
		Me.DataColumn2 = New System.Data.DataColumn()
		CType(Me.gb_Emulators, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_Emulators.SuspendLayout()
		CType(Me.grd_Emulators, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Emulators, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_Rombase, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_Emulators, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.pnl_Emulators_Buttons, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_Emulators_Buttons.SuspendLayout()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.pnl_Platforms, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_Platforms.SuspendLayout()
		CType(Me.grd_Platforms, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Platforms, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_Platforms, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_Supported, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_DefaultEmulator, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.pnl_Settings, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_Settings.SuspendLayout()
		CType(Me.tcl_Settings, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tcl_Settings.SuspendLayout()
		Me.tpg_Settings.SuspendLayout()
		CType(Me.Ctl_MKDXSplitPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Ctl_MKDXSplitPanel1.SuspendLayout()
		CType(Me.txb_Description.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.barmng, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Ctl_MKDXMemoEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_Exe_Identifier_Regex.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_URL_Download.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_URL_Website.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_Identifier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.cmb_List_Generator.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_List_Generators, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_ScreenshotDirectory.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_StartupParameter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_Name.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tpg_MV_Settings.SuspendLayout()
		CType(Me.grd_MV, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_MV, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_MV, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_MV, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Volumes, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.pnl_MV_Buttons, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_MV_Buttons.SuspendLayout()
		CType(Me.BTA_Libretro_Core, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_J2K, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_J2K, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.tbl_Config, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_DOSBox_Patches_Categories, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_DOSBox_Patches, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_rpi_Platforms, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_MobyDB, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.pnl_Right, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_Right.SuspendLayout()
		CType(Me.pnl_Buttons, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_Buttons.SuspendLayout()
		CType(Me.popmnu_Emulators, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'gb_Emulators
		'
		Me.gb_Emulators.Controls.Add(Me.grd_Emulators)
		Me.gb_Emulators.Controls.Add(Me.pnl_Emulators_Buttons)
		Me.gb_Emulators.Dock = System.Windows.Forms.DockStyle.Left
		Me.gb_Emulators.Location = New System.Drawing.Point(0, 0)
		Me.gb_Emulators.Name = "gb_Emulators"
		Me.gb_Emulators.Size = New System.Drawing.Size(260, 585)
		Me.gb_Emulators.TabIndex = 4
		Me.gb_Emulators.Text = "Known Emulators"
		'
		'grd_Emulators
		'
		Me.grd_Emulators.DataSource = Me.BS_Emulators
		Me.grd_Emulators.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_Emulators.Location = New System.Drawing.Point(2, 20)
		Me.grd_Emulators.MainView = Me.gv_Emulators
		Me.grd_Emulators.Name = "grd_Emulators"
		Me.grd_Emulators.Size = New System.Drawing.Size(256, 537)
		Me.grd_Emulators.TabIndex = 0
		Me.grd_Emulators.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Emulators})
		'
		'BS_Emulators
		'
		Me.BS_Emulators.DataMember = "tbl_Rombase_Known_Emulators"
		Me.BS_Emulators.DataSource = Me.DS_Rombase
		'
		'DS_Rombase
		'
		Me.DS_Rombase.DataSetName = "DS_Rombase"
		Me.DS_Rombase.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'gv_Emulators
		'
		Me.gv_Emulators.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colDisplayname, Me.colIdentifier})
		Me.gv_Emulators.GridControl = Me.grd_Emulators
		Me.gv_Emulators.Name = "gv_Emulators"
		Me.gv_Emulators.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_Emulators.OptionsSelection.InvertSelection = True
		Me.gv_Emulators.OptionsView.ShowGroupPanel = False
		Me.gv_Emulators.OptionsView.ShowIndicator = False
		'
		'colDisplayname
		'
		Me.colDisplayname.FieldName = "Name"
		Me.colDisplayname.Name = "colDisplayname"
		Me.colDisplayname.OptionsColumn.AllowEdit = False
		Me.colDisplayname.OptionsColumn.ReadOnly = True
		Me.colDisplayname.Visible = True
		Me.colDisplayname.VisibleIndex = 0
		Me.colDisplayname.Width = 167
		'
		'colIdentifier
		'
		Me.colIdentifier.FieldName = "Identifier"
		Me.colIdentifier.Name = "colIdentifier"
		Me.colIdentifier.OptionsColumn.AllowEdit = False
		Me.colIdentifier.OptionsColumn.ReadOnly = True
		Me.colIdentifier.Visible = True
		Me.colIdentifier.VisibleIndex = 1
		Me.colIdentifier.Width = 87
		'
		'pnl_Emulators_Buttons
		'
		Me.pnl_Emulators_Buttons.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_Emulators_Buttons.Controls.Add(Me.btn_Delete_Known_Emulator)
		Me.pnl_Emulators_Buttons.Controls.Add(Me.btn_Import_Known_Emulator)
		Me.pnl_Emulators_Buttons.Controls.Add(Me.btn_Add_Known_Emulator)
		Me.pnl_Emulators_Buttons.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.pnl_Emulators_Buttons.Location = New System.Drawing.Point(2, 557)
		Me.pnl_Emulators_Buttons.Name = "pnl_Emulators_Buttons"
		Me.pnl_Emulators_Buttons.Size = New System.Drawing.Size(256, 26)
		Me.pnl_Emulators_Buttons.TabIndex = 5
		'
		'btn_Delete_Known_Emulator
		'
		Me.btn_Delete_Known_Emulator.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Delete_Known_Emulator.Location = New System.Drawing.Point(179, 4)
		Me.btn_Delete_Known_Emulator.Name = "btn_Delete_Known_Emulator"
		Me.btn_Delete_Known_Emulator.Size = New System.Drawing.Size(75, 20)
		ToolTipTitleItem1.Text = "Delete Emulator"
		SuperToolTip1.Items.Add(ToolTipTitleItem1)
		Me.btn_Delete_Known_Emulator.SuperTip = SuperToolTip1
		Me.btn_Delete_Known_Emulator.TabIndex = 2
		Me.btn_Delete_Known_Emulator.Text = "&Delete"
		'
		'btn_Import_Known_Emulator
		'
		Me.btn_Import_Known_Emulator.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Import_Known_Emulator.Location = New System.Drawing.Point(101, 4)
		Me.btn_Import_Known_Emulator.Name = "btn_Import_Known_Emulator"
		Me.btn_Import_Known_Emulator.Size = New System.Drawing.Size(75, 20)
		ToolTipTitleItem2.Text = "Duplicate Emulator"
		SuperToolTip2.Items.Add(ToolTipTitleItem2)
		Me.btn_Import_Known_Emulator.SuperTip = SuperToolTip2
		Me.btn_Import_Known_Emulator.TabIndex = 1
		Me.btn_Import_Known_Emulator.Text = "&Import"
		'
		'btn_Add_Known_Emulator
		'
		Me.btn_Add_Known_Emulator.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Add_Known_Emulator.Location = New System.Drawing.Point(23, 4)
		Me.btn_Add_Known_Emulator.Name = "btn_Add_Known_Emulator"
		Me.btn_Add_Known_Emulator.Size = New System.Drawing.Size(75, 20)
		ToolTipTitleItem3.Text = "Add Emulator"
		SuperToolTip3.Items.Add(ToolTipTitleItem3)
		Me.btn_Add_Known_Emulator.SuperTip = SuperToolTip3
		Me.btn_Add_Known_Emulator.TabIndex = 0
		Me.btn_Add_Known_Emulator.Text = "&Add"
		'
		'DS_ML
		'
		Me.DS_ML.DataSetName = "DS_ML"
		Me.DS_ML.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'Ctl_MKDXSplitter1
		'
		Me.Ctl_MKDXSplitter1.Location = New System.Drawing.Point(260, 0)
		Me.Ctl_MKDXSplitter1.Name = "Ctl_MKDXSplitter1"
		Me.Ctl_MKDXSplitter1.Size = New System.Drawing.Size(5, 585)
		Me.Ctl_MKDXSplitter1.TabIndex = 5
		Me.Ctl_MKDXSplitter1.TabStop = False
		'
		'pnl_Platforms
		'
		Me.pnl_Platforms.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_Platforms.Controls.Add(Me.grd_Platforms)
		Me.pnl_Platforms.Controls.Add(Me.lbl_Platforms)
		Me.pnl_Platforms.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_Platforms.Location = New System.Drawing.Point(2, 308)
		Me.pnl_Platforms.Name = "pnl_Platforms"
		Me.pnl_Platforms.Size = New System.Drawing.Size(411, 249)
		Me.pnl_Platforms.TabIndex = 10
		'
		'grd_Platforms
		'
		Me.grd_Platforms.DataSource = Me.BS_Platforms
		Me.grd_Platforms.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_Platforms.Location = New System.Drawing.Point(0, 31)
		Me.grd_Platforms.MainView = Me.gv_Platforms
		Me.grd_Platforms.Name = "grd_Platforms"
		Me.grd_Platforms.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rpi_Supported, Me.rpi_DefaultEmulator})
		Me.grd_Platforms.Size = New System.Drawing.Size(411, 218)
		Me.grd_Platforms.TabIndex = 0
		Me.grd_Platforms.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Platforms})
		'
		'BS_Platforms
		'
		Me.BS_Platforms.DataMember = "src_frm_Known_Emulators_Moby_Platforms"
		Me.BS_Platforms.DataSource = Me.DS_Rombase
		'
		'gv_Platforms
		'
		Me.gv_Platforms.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colDisplay_Name, Me.colSupported})
		Me.gv_Platforms.GridControl = Me.grd_Platforms
		Me.gv_Platforms.Name = "gv_Platforms"
		Me.gv_Platforms.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_Platforms.OptionsView.ShowGroupPanel = False
		Me.gv_Platforms.OptionsView.ShowIndicator = False
		'
		'colDisplay_Name
		'
		Me.colDisplay_Name.Caption = "Platform"
		Me.colDisplay_Name.FieldName = "Display_Name"
		Me.colDisplay_Name.Name = "colDisplay_Name"
		Me.colDisplay_Name.OptionsColumn.AllowEdit = False
		Me.colDisplay_Name.OptionsColumn.ReadOnly = True
		Me.colDisplay_Name.Visible = True
		Me.colDisplay_Name.VisibleIndex = 0
		Me.colDisplay_Name.Width = 232
		'
		'colSupported
		'
		Me.colSupported.Caption = "Supported"
		Me.colSupported.ColumnEdit = Me.rpi_Supported
		Me.colSupported.FieldName = "Supported"
		Me.colSupported.Name = "colSupported"
		Me.colSupported.Visible = True
		Me.colSupported.VisibleIndex = 1
		Me.colSupported.Width = 72
		'
		'rpi_Supported
		'
		Me.rpi_Supported.AutoHeight = False
		Me.rpi_Supported.Name = "rpi_Supported"
		Me.rpi_Supported.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
		'
		'rpi_DefaultEmulator
		'
		Me.rpi_DefaultEmulator.AutoHeight = False
		Me.rpi_DefaultEmulator.Name = "rpi_DefaultEmulator"
		Me.rpi_DefaultEmulator.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
		'
		'lbl_Platforms
		'
		Me.lbl_Platforms.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!)
		Me.lbl_Platforms.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
		Me.lbl_Platforms.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Platforms.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_Platforms.Location = New System.Drawing.Point(0, 0)
		Me.lbl_Platforms.MKBoundControl1 = Nothing
		Me.lbl_Platforms.MKBoundControl2 = Nothing
		Me.lbl_Platforms.MKBoundControl3 = Nothing
		Me.lbl_Platforms.MKBoundControl4 = Nothing
		Me.lbl_Platforms.MKBoundControl5 = Nothing
		Me.lbl_Platforms.Name = "lbl_Platforms"
		Me.lbl_Platforms.Size = New System.Drawing.Size(411, 31)
		Me.lbl_Platforms.TabIndex = 0
		Me.lbl_Platforms.Text = "Platforms supported by this Emulator:"
		'
		'btn_Save
		'
		Me.btn_Save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Save.Location = New System.Drawing.Point(254, 4)
		Me.btn_Save.Name = "btn_Save"
		Me.btn_Save.Size = New System.Drawing.Size(75, 20)
		Me.btn_Save.TabIndex = 0
		Me.btn_Save.Text = "&Save"
		'
		'Ctl_MKDXSplitter2
		'
		Me.Ctl_MKDXSplitter2.Dock = System.Windows.Forms.DockStyle.Top
		Me.Ctl_MKDXSplitter2.Location = New System.Drawing.Point(2, 303)
		Me.Ctl_MKDXSplitter2.Name = "Ctl_MKDXSplitter2"
		Me.Ctl_MKDXSplitter2.Size = New System.Drawing.Size(411, 5)
		Me.Ctl_MKDXSplitter2.TabIndex = 11
		Me.Ctl_MKDXSplitter2.TabStop = False
		'
		'pnl_Settings
		'
		Me.pnl_Settings.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_Settings.Controls.Add(Me.tcl_Settings)
		Me.pnl_Settings.Dock = System.Windows.Forms.DockStyle.Top
		Me.pnl_Settings.Location = New System.Drawing.Point(2, 2)
		Me.pnl_Settings.MinimumSize = New System.Drawing.Size(411, 146)
		Me.pnl_Settings.Name = "pnl_Settings"
		Me.pnl_Settings.Size = New System.Drawing.Size(411, 301)
		Me.pnl_Settings.TabIndex = 9
		'
		'tcl_Settings
		'
		Me.tcl_Settings.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tcl_Settings.Location = New System.Drawing.Point(0, 0)
		Me.tcl_Settings.Name = "tcl_Settings"
		Me.tcl_Settings.SelectedTabPage = Me.tpg_Settings
		Me.tcl_Settings.Size = New System.Drawing.Size(411, 301)
		Me.tcl_Settings.TabIndex = 0
		Me.tcl_Settings.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tpg_Settings, Me.tpg_MV_Settings})
		'
		'tpg_Settings
		'
		Me.tpg_Settings.Controls.Add(Me.Ctl_MKDXSplitPanel1)
		Me.tpg_Settings.Controls.Add(Me.lbl_Exe_Identifier_Regex)
		Me.tpg_Settings.Controls.Add(Me.txb_Exe_Identifier_Regex)
		Me.tpg_Settings.Controls.Add(Me.lbl_URL_Download)
		Me.tpg_Settings.Controls.Add(Me.txb_URL_Download)
		Me.tpg_Settings.Controls.Add(Me.lbl_URL_Website)
		Me.tpg_Settings.Controls.Add(Me.txb_URL_Website)
		Me.tpg_Settings.Controls.Add(Me.lbl_Identifier)
		Me.tpg_Settings.Controls.Add(Me.txb_Identifier)
		Me.tpg_Settings.Controls.Add(Me.cmb_List_Generator)
		Me.tpg_Settings.Controls.Add(Me.lbl_List_Generator)
		Me.tpg_Settings.Controls.Add(Me.lbl_Name)
		Me.tpg_Settings.Controls.Add(Me.txb_ScreenshotDirectory)
		Me.tpg_Settings.Controls.Add(Me.txb_StartupParameter)
		Me.tpg_Settings.Controls.Add(Me.txb_Name)
		Me.tpg_Settings.Controls.Add(Me.lbl_StartupParameter)
		Me.tpg_Settings.Controls.Add(Me.lbl_Screenshot_Directory)
		Me.tpg_Settings.Name = "tpg_Settings"
		Me.tpg_Settings.Size = New System.Drawing.Size(405, 273)
		Me.tpg_Settings.Text = "Settings"
		'
		'Ctl_MKDXSplitPanel1
		'
		Me.Ctl_MKDXSplitPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Ctl_MKDXSplitPanel1.Horizontal = False
		Me.Ctl_MKDXSplitPanel1.Location = New System.Drawing.Point(2, 187)
		Me.Ctl_MKDXSplitPanel1.Name = "Ctl_MKDXSplitPanel1"
		Me.Ctl_MKDXSplitPanel1.Panel1.Controls.Add(Me.lbl_Description)
		Me.Ctl_MKDXSplitPanel1.Panel1.Controls.Add(Me.txb_Description)
		Me.Ctl_MKDXSplitPanel1.Panel1.Text = "Panel1"
		Me.Ctl_MKDXSplitPanel1.Panel2.Controls.Add(Me.Ctl_MKDXMemoEdit1)
		Me.Ctl_MKDXSplitPanel1.Panel2.Controls.Add(Me.lbl_Autoconfig_Note)
		Me.Ctl_MKDXSplitPanel1.Panel2.Text = "Panel2"
		Me.Ctl_MKDXSplitPanel1.Size = New System.Drawing.Size(400, 83)
		Me.Ctl_MKDXSplitPanel1.SplitterPosition = 41
		Me.Ctl_MKDXSplitPanel1.TabIndex = 26
		Me.Ctl_MKDXSplitPanel1.Text = "Ctl_MKDXSplitPanel1"
		'
		'lbl_Description
		'
		Me.lbl_Description.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Description.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Description.Location = New System.Drawing.Point(1, 1)
		Me.lbl_Description.MKBoundControl1 = Nothing
		Me.lbl_Description.MKBoundControl2 = Nothing
		Me.lbl_Description.MKBoundControl3 = Nothing
		Me.lbl_Description.MKBoundControl4 = Nothing
		Me.lbl_Description.MKBoundControl5 = Nothing
		Me.lbl_Description.Name = "lbl_Description"
		Me.lbl_Description.Size = New System.Drawing.Size(112, 20)
		Me.lbl_Description.TabIndex = 22
		Me.lbl_Description.Text = "Description:"
		'
		'txb_Description
		'
		Me.txb_Description.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_Description.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_Emulators, "Description", True))
		Me.txb_Description.Location = New System.Drawing.Point(116, 1)
		Me.txb_Description.MenuManager = Me.barmng
		Me.txb_Description.MKBoundLabel = Nothing
		Me.txb_Description.MKEditValue_Compare = Nothing
		Me.txb_Description.Name = "txb_Description"
		Me.txb_Description.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txb_Description.Size = New System.Drawing.Size(284, 37)
		Me.txb_Description.TabIndex = 8
		'
		'barmng
		'
		Me.barmng.DockControls.Add(Me.barDockControlTop)
		Me.barmng.DockControls.Add(Me.barDockControlBottom)
		Me.barmng.DockControls.Add(Me.barDockControlLeft)
		Me.barmng.DockControls.Add(Me.barDockControlRight)
		Me.barmng.Form = Me
		Me.barmng.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.bbi_Add, Me.bbi_Import, Me.bbi_Delete})
		Me.barmng.MaxItemId = 3
		'
		'barDockControlTop
		'
		Me.barDockControlTop.CausesValidation = False
		Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
		Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
		Me.barDockControlTop.Size = New System.Drawing.Size(680, 0)
		'
		'barDockControlBottom
		'
		Me.barDockControlBottom.CausesValidation = False
		Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.barDockControlBottom.Location = New System.Drawing.Point(0, 585)
		Me.barDockControlBottom.Size = New System.Drawing.Size(680, 0)
		'
		'barDockControlLeft
		'
		Me.barDockControlLeft.CausesValidation = False
		Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
		Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
		Me.barDockControlLeft.Size = New System.Drawing.Size(0, 585)
		'
		'barDockControlRight
		'
		Me.barDockControlRight.CausesValidation = False
		Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
		Me.barDockControlRight.Location = New System.Drawing.Point(680, 0)
		Me.barDockControlRight.Size = New System.Drawing.Size(0, 585)
		'
		'bbi_Add
		'
		Me.bbi_Add.Caption = "&Add"
		Me.bbi_Add.Id = 0
		Me.bbi_Add.ImageUri.Uri = "Add"
		Me.bbi_Add.Name = "bbi_Add"
		'
		'bbi_Import
		'
		Me.bbi_Import.Caption = "&Import"
		Me.bbi_Import.Id = 1
		Me.bbi_Import.ImageUri.Uri = "Copy"
		Me.bbi_Import.Name = "bbi_Import"
		'
		'bbi_Delete
		'
		Me.bbi_Delete.Caption = "De&lete"
		Me.bbi_Delete.Id = 2
		Me.bbi_Delete.ImageUri.Uri = "Delete"
		Me.bbi_Delete.Name = "bbi_Delete"
		'
		'Ctl_MKDXMemoEdit1
		'
		Me.Ctl_MKDXMemoEdit1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Ctl_MKDXMemoEdit1.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_Emulators, "Autoconfig_Note", True))
		Me.Ctl_MKDXMemoEdit1.Location = New System.Drawing.Point(116, 0)
		Me.Ctl_MKDXMemoEdit1.MenuManager = Me.barmng
		Me.Ctl_MKDXMemoEdit1.MKBoundLabel = Nothing
		Me.Ctl_MKDXMemoEdit1.MKEditValue_Compare = Nothing
		Me.Ctl_MKDXMemoEdit1.Name = "Ctl_MKDXMemoEdit1"
		Me.Ctl_MKDXMemoEdit1.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.Ctl_MKDXMemoEdit1.Size = New System.Drawing.Size(284, 37)
		Me.Ctl_MKDXMemoEdit1.TabIndex = 24
		'
		'lbl_Autoconfig_Note
		'
		Me.lbl_Autoconfig_Note.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Autoconfig_Note.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Autoconfig_Note.Location = New System.Drawing.Point(1, -5)
		Me.lbl_Autoconfig_Note.MKBoundControl1 = Nothing
		Me.lbl_Autoconfig_Note.MKBoundControl2 = Nothing
		Me.lbl_Autoconfig_Note.MKBoundControl3 = Nothing
		Me.lbl_Autoconfig_Note.MKBoundControl4 = Nothing
		Me.lbl_Autoconfig_Note.MKBoundControl5 = Nothing
		Me.lbl_Autoconfig_Note.Name = "lbl_Autoconfig_Note"
		Me.lbl_Autoconfig_Note.Size = New System.Drawing.Size(112, 20)
		Me.lbl_Autoconfig_Note.TabIndex = 23
		Me.lbl_Autoconfig_Note.Text = "Autoconfig Note:"
		'
		'lbl_Exe_Identifier_Regex
		'
		Me.lbl_Exe_Identifier_Regex.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Exe_Identifier_Regex.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Exe_Identifier_Regex.Location = New System.Drawing.Point(3, 49)
		Me.lbl_Exe_Identifier_Regex.MKBoundControl1 = Nothing
		Me.lbl_Exe_Identifier_Regex.MKBoundControl2 = Nothing
		Me.lbl_Exe_Identifier_Regex.MKBoundControl3 = Nothing
		Me.lbl_Exe_Identifier_Regex.MKBoundControl4 = Nothing
		Me.lbl_Exe_Identifier_Regex.MKBoundControl5 = Nothing
		Me.lbl_Exe_Identifier_Regex.Name = "lbl_Exe_Identifier_Regex"
		Me.lbl_Exe_Identifier_Regex.Size = New System.Drawing.Size(112, 20)
		Me.lbl_Exe_Identifier_Regex.TabIndex = 25
		Me.lbl_Exe_Identifier_Regex.Text = "Exe Identifier Regex:"
		'
		'txb_Exe_Identifier_Regex
		'
		Me.txb_Exe_Identifier_Regex.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_Exe_Identifier_Regex.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_Emulators, "Exe_Identifier_Regex", True))
		Me.txb_Exe_Identifier_Regex.Location = New System.Drawing.Point(118, 49)
		Me.txb_Exe_Identifier_Regex.MKBoundLabel = Nothing
		Me.txb_Exe_Identifier_Regex.MKEditValue_Compare = Nothing
		Me.txb_Exe_Identifier_Regex.Name = "txb_Exe_Identifier_Regex"
		Me.txb_Exe_Identifier_Regex.Size = New System.Drawing.Size(284, 20)
		Me.txb_Exe_Identifier_Regex.TabIndex = 2
		'
		'lbl_URL_Download
		'
		Me.lbl_URL_Download.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_URL_Download.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_URL_Download.Location = New System.Drawing.Point(3, 164)
		Me.lbl_URL_Download.MKBoundControl1 = Nothing
		Me.lbl_URL_Download.MKBoundControl2 = Nothing
		Me.lbl_URL_Download.MKBoundControl3 = Nothing
		Me.lbl_URL_Download.MKBoundControl4 = Nothing
		Me.lbl_URL_Download.MKBoundControl5 = Nothing
		Me.lbl_URL_Download.Name = "lbl_URL_Download"
		Me.lbl_URL_Download.Size = New System.Drawing.Size(112, 20)
		Me.lbl_URL_Download.TabIndex = 21
		Me.lbl_URL_Download.Text = "Download URL:"
		'
		'txb_URL_Download
		'
		Me.txb_URL_Download.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_URL_Download.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_Emulators, "URL_Download", True))
		Me.txb_URL_Download.Location = New System.Drawing.Point(118, 164)
		Me.txb_URL_Download.MKBoundLabel = Nothing
		Me.txb_URL_Download.MKEditValue_Compare = Nothing
		Me.txb_URL_Download.Name = "txb_URL_Download"
		Me.txb_URL_Download.Size = New System.Drawing.Size(284, 20)
		Me.txb_URL_Download.TabIndex = 7
		'
		'lbl_URL_Website
		'
		Me.lbl_URL_Website.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_URL_Website.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_URL_Website.Location = New System.Drawing.Point(3, 141)
		Me.lbl_URL_Website.MKBoundControl1 = Nothing
		Me.lbl_URL_Website.MKBoundControl2 = Nothing
		Me.lbl_URL_Website.MKBoundControl3 = Nothing
		Me.lbl_URL_Website.MKBoundControl4 = Nothing
		Me.lbl_URL_Website.MKBoundControl5 = Nothing
		Me.lbl_URL_Website.Name = "lbl_URL_Website"
		Me.lbl_URL_Website.Size = New System.Drawing.Size(112, 20)
		Me.lbl_URL_Website.TabIndex = 19
		Me.lbl_URL_Website.Text = "Website URL:"
		'
		'txb_URL_Website
		'
		Me.txb_URL_Website.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_URL_Website.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_Emulators, "URL_Website", True))
		Me.txb_URL_Website.Location = New System.Drawing.Point(118, 141)
		Me.txb_URL_Website.MKBoundLabel = Nothing
		Me.txb_URL_Website.MKEditValue_Compare = Nothing
		Me.txb_URL_Website.Name = "txb_URL_Website"
		Me.txb_URL_Website.Size = New System.Drawing.Size(284, 20)
		Me.txb_URL_Website.TabIndex = 6
		'
		'lbl_Identifier
		'
		Me.lbl_Identifier.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Identifier.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Identifier.Location = New System.Drawing.Point(3, 26)
		Me.lbl_Identifier.MKBoundControl1 = Nothing
		Me.lbl_Identifier.MKBoundControl2 = Nothing
		Me.lbl_Identifier.MKBoundControl3 = Nothing
		Me.lbl_Identifier.MKBoundControl4 = Nothing
		Me.lbl_Identifier.MKBoundControl5 = Nothing
		Me.lbl_Identifier.Name = "lbl_Identifier"
		Me.lbl_Identifier.Size = New System.Drawing.Size(112, 20)
		Me.lbl_Identifier.TabIndex = 17
		Me.lbl_Identifier.Text = "Unique Identifier:"
		'
		'txb_Identifier
		'
		Me.txb_Identifier.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_Identifier.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_Emulators, "Identifier", True))
		Me.txb_Identifier.Location = New System.Drawing.Point(118, 26)
		Me.txb_Identifier.MKBoundLabel = Nothing
		Me.txb_Identifier.MKEditValue_Compare = Nothing
		Me.txb_Identifier.Name = "txb_Identifier"
		Me.txb_Identifier.Size = New System.Drawing.Size(284, 20)
		Me.txb_Identifier.TabIndex = 1
		'
		'cmb_List_Generator
		'
		Me.cmb_List_Generator.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmb_List_Generator.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_Emulators, "id_List_Generators", True))
		Me.cmb_List_Generator.Location = New System.Drawing.Point(118, 118)
		Me.cmb_List_Generator.MKBoundLabel = Nothing
		Me.cmb_List_Generator.MKEditValue_Compare = Nothing
		Me.cmb_List_Generator.Name = "cmb_List_Generator"
		Me.cmb_List_Generator.Properties.AllowFocused = False
		ToolTipItem1.Text = "Don't use a List Generator with this Emulator"
		SuperToolTip4.Items.Add(ToolTipItem1)
		Me.cmb_List_Generator.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, SuperToolTip4, True)})
		Me.cmb_List_Generator.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_List_Generators", "id_List_Generators", 5, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name", 5, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Main_Template", "Main_Template", 5, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("File_Entry_Template", "File_Entry_Template", 5, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Sort", "Sort", 5, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far)})
		Me.cmb_List_Generator.Properties.DataSource = Me.BS_List_Generators
		Me.cmb_List_Generator.Properties.DisplayMember = "Name"
		Me.cmb_List_Generator.Properties.NullText = "<Don't use a List Generator>"
		Me.cmb_List_Generator.Properties.ShowFooter = False
		Me.cmb_List_Generator.Properties.ShowHeader = False
		Me.cmb_List_Generator.Properties.ValueMember = "id_List_Generators"
		Me.cmb_List_Generator.Size = New System.Drawing.Size(284, 20)
		ToolTipTitleItem4.Text = "List Generator"
		ToolTipItem2.LeftIndent = 6
		ToolTipItem2.Text = resources.GetString("ToolTipItem2.Text")
		SuperToolTip5.Items.Add(ToolTipTitleItem4)
		SuperToolTip5.Items.Add(ToolTipItem2)
		Me.cmb_List_Generator.SuperTip = SuperToolTip5
		Me.cmb_List_Generator.TabIndex = 5
		'
		'BS_List_Generators
		'
		Me.BS_List_Generators.DataMember = "tbl_List_Generators"
		Me.BS_List_Generators.DataSource = Me.DS_ML
		'
		'lbl_List_Generator
		'
		Me.lbl_List_Generator.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_List_Generator.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_List_Generator.Location = New System.Drawing.Point(3, 118)
		Me.lbl_List_Generator.MKBoundControl1 = Nothing
		Me.lbl_List_Generator.MKBoundControl2 = Nothing
		Me.lbl_List_Generator.MKBoundControl3 = Nothing
		Me.lbl_List_Generator.MKBoundControl4 = Nothing
		Me.lbl_List_Generator.MKBoundControl5 = Nothing
		Me.lbl_List_Generator.Name = "lbl_List_Generator"
		Me.lbl_List_Generator.Size = New System.Drawing.Size(112, 20)
		ToolTipTitleItem5.Text = "List Generator"
		ToolTipItem3.LeftIndent = 6
		ToolTipItem3.Text = resources.GetString("ToolTipItem3.Text")
		SuperToolTip6.Items.Add(ToolTipTitleItem5)
		SuperToolTip6.Items.Add(ToolTipItem3)
		Me.lbl_List_Generator.SuperTip = SuperToolTip6
		Me.lbl_List_Generator.TabIndex = 13
		Me.lbl_List_Generator.Text = "List Generator:"
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
		Me.lbl_Name.Size = New System.Drawing.Size(112, 20)
		Me.lbl_Name.TabIndex = 7
		Me.lbl_Name.Text = "Name:"
		'
		'txb_ScreenshotDirectory
		'
		Me.txb_ScreenshotDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_ScreenshotDirectory.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_Emulators, "ScreenshotDirectory", True))
		Me.txb_ScreenshotDirectory.Location = New System.Drawing.Point(118, 72)
		Me.txb_ScreenshotDirectory.MKBoundLabel = Nothing
		Me.txb_ScreenshotDirectory.MKEditValue_Compare = Nothing
		Me.txb_ScreenshotDirectory.Name = "txb_ScreenshotDirectory"
		Me.txb_ScreenshotDirectory.Size = New System.Drawing.Size(284, 20)
		ToolTipTitleItem6.Text = "Screenshot Directory"
		ToolTipItem4.LeftIndent = 6
		ToolTipItem4.Text = "Define the screenshot directory of the emulator here. This will be used for autom" &
		"atic screenshot collection of your roms."
		SuperToolTip7.Items.Add(ToolTipTitleItem6)
		SuperToolTip7.Items.Add(ToolTipItem4)
		Me.txb_ScreenshotDirectory.SuperTip = SuperToolTip7
		Me.txb_ScreenshotDirectory.TabIndex = 3
		'
		'txb_StartupParameter
		'
		Me.txb_StartupParameter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_StartupParameter.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_Emulators, "StartupParameter", True))
		Me.txb_StartupParameter.Location = New System.Drawing.Point(118, 95)
		Me.txb_StartupParameter.MKBoundLabel = Nothing
		Me.txb_StartupParameter.MKEditValue_Compare = Nothing
		Me.txb_StartupParameter.Name = "txb_StartupParameter"
		Me.txb_StartupParameter.Size = New System.Drawing.Size(284, 20)
		ToolTipTitleItem7.Text = "Startup Parameter"
		ToolTipItem5.LeftIndent = 6
		ToolTipItem5.Text = resources.GetString("ToolTipItem5.Text")
		SuperToolTip8.Items.Add(ToolTipTitleItem7)
		SuperToolTip8.Items.Add(ToolTipItem5)
		Me.txb_StartupParameter.SuperTip = SuperToolTip8
		Me.txb_StartupParameter.TabIndex = 4
		'
		'txb_Name
		'
		Me.txb_Name.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_Name.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_Emulators, "Name", True))
		Me.txb_Name.Location = New System.Drawing.Point(118, 3)
		Me.txb_Name.MKBoundLabel = Nothing
		Me.txb_Name.MKEditValue_Compare = Nothing
		Me.txb_Name.Name = "txb_Name"
		Me.txb_Name.Size = New System.Drawing.Size(284, 20)
		Me.txb_Name.TabIndex = 0
		'
		'lbl_StartupParameter
		'
		Me.lbl_StartupParameter.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_StartupParameter.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_StartupParameter.Location = New System.Drawing.Point(3, 95)
		Me.lbl_StartupParameter.MKBoundControl1 = Nothing
		Me.lbl_StartupParameter.MKBoundControl2 = Nothing
		Me.lbl_StartupParameter.MKBoundControl3 = Nothing
		Me.lbl_StartupParameter.MKBoundControl4 = Nothing
		Me.lbl_StartupParameter.MKBoundControl5 = Nothing
		Me.lbl_StartupParameter.Name = "lbl_StartupParameter"
		Me.lbl_StartupParameter.Size = New System.Drawing.Size(112, 20)
		ToolTipTitleItem8.Text = "Startup Parameter"
		ToolTipItem6.LeftIndent = 6
		ToolTipItem6.Text = resources.GetString("ToolTipItem6.Text")
		SuperToolTip9.Items.Add(ToolTipTitleItem8)
		SuperToolTip9.Items.Add(ToolTipItem6)
		Me.lbl_StartupParameter.SuperTip = SuperToolTip9
		Me.lbl_StartupParameter.TabIndex = 7
		Me.lbl_StartupParameter.Text = "Startup Parameter:"
		'
		'lbl_Screenshot_Directory
		'
		Me.lbl_Screenshot_Directory.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Screenshot_Directory.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Screenshot_Directory.Location = New System.Drawing.Point(3, 72)
		Me.lbl_Screenshot_Directory.MKBoundControl1 = Nothing
		Me.lbl_Screenshot_Directory.MKBoundControl2 = Nothing
		Me.lbl_Screenshot_Directory.MKBoundControl3 = Nothing
		Me.lbl_Screenshot_Directory.MKBoundControl4 = Nothing
		Me.lbl_Screenshot_Directory.MKBoundControl5 = Nothing
		Me.lbl_Screenshot_Directory.Name = "lbl_Screenshot_Directory"
		Me.lbl_Screenshot_Directory.Size = New System.Drawing.Size(112, 20)
		ToolTipTitleItem9.Text = "Screenshot Directory"
		ToolTipItem7.LeftIndent = 6
		ToolTipItem7.Text = "Define the screenshot directory of the emulator here. This will be used for autom" &
		"atic screenshot collection of your roms."
		SuperToolTip10.Items.Add(ToolTipTitleItem9)
		SuperToolTip10.Items.Add(ToolTipItem7)
		Me.lbl_Screenshot_Directory.SuperTip = SuperToolTip10
		Me.lbl_Screenshot_Directory.TabIndex = 7
		Me.lbl_Screenshot_Directory.Text = "Screenshot Directory:"
		'
		'tpg_MV_Settings
		'
		Me.tpg_MV_Settings.Controls.Add(Me.grd_MV)
		Me.tpg_MV_Settings.Controls.Add(Me.pnl_MV_Buttons)
		Me.tpg_MV_Settings.Name = "tpg_MV_Settings"
		Me.tpg_MV_Settings.Size = New System.Drawing.Size(405, 273)
		Me.tpg_MV_Settings.Text = "Multiple Volumes"
		'
		'grd_MV
		'
		Me.grd_MV.DataSource = Me.BS_MV
		Me.grd_MV.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_MV.Location = New System.Drawing.Point(0, 0)
		Me.grd_MV.MainView = Me.gv_MV
		Me.grd_MV.Name = "grd_MV"
		Me.grd_MV.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rpi_MV})
		Me.grd_MV.Size = New System.Drawing.Size(405, 247)
		Me.grd_MV.TabIndex = 0
		Me.grd_MV.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_MV})
		'
		'BS_MV
		'
		Me.BS_MV.DataMember = "tbl_Rombase_Known_Emulators_Multivolume_Parameters"
		Me.BS_MV.DataSource = Me.DS_Rombase
		'
		'gv_MV
		'
		Me.gv_MV.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colVolume_Number, Me.colParameter})
		Me.gv_MV.GridControl = Me.grd_MV
		Me.gv_MV.Name = "gv_MV"
		Me.gv_MV.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_MV.OptionsView.ShowGroupPanel = False
		Me.gv_MV.OptionsView.ShowIndicator = False
		'
		'colVolume_Number
		'
		Me.colVolume_Number.Caption = "Disc/Volume"
		Me.colVolume_Number.ColumnEdit = Me.rpi_MV
		Me.colVolume_Number.FieldName = "Volume_Number"
		Me.colVolume_Number.Name = "colVolume_Number"
		Me.colVolume_Number.Visible = True
		Me.colVolume_Number.VisibleIndex = 0
		Me.colVolume_Number.Width = 104
		'
		'rpi_MV
		'
		Me.rpi_MV.AutoHeight = False
		Me.rpi_MV.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.rpi_MV.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_Tag_Parser_Volumes", "id_Tag_Parser_Volumes", 137, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayText", "Display Text", 69, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near)})
		Me.rpi_MV.DataSource = Me.BS_Volumes
		Me.rpi_MV.DisplayMember = "DisplayText"
		Me.rpi_MV.Name = "rpi_MV"
		Me.rpi_MV.ShowHeader = False
		Me.rpi_MV.ValueMember = "id_Tag_Parser_Volumes"
		'
		'BS_Volumes
		'
		Me.BS_Volumes.DataMember = "ttb_Tag_Parser_Volumes"
		Me.BS_Volumes.DataSource = Me.DS_ML
		'
		'colParameter
		'
		Me.colParameter.Caption = "Parameter"
		Me.colParameter.FieldName = "Parameter"
		Me.colParameter.Name = "colParameter"
		Me.colParameter.Visible = True
		Me.colParameter.VisibleIndex = 1
		Me.colParameter.Width = 323
		'
		'pnl_MV_Buttons
		'
		Me.pnl_MV_Buttons.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_MV_Buttons.Controls.Add(Me.btn_MV_Delete)
		Me.pnl_MV_Buttons.Controls.Add(Me.btn_MV_Add)
		Me.pnl_MV_Buttons.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.pnl_MV_Buttons.Location = New System.Drawing.Point(0, 247)
		Me.pnl_MV_Buttons.Name = "pnl_MV_Buttons"
		Me.pnl_MV_Buttons.Size = New System.Drawing.Size(405, 26)
		Me.pnl_MV_Buttons.TabIndex = 8
		'
		'btn_MV_Delete
		'
		Me.btn_MV_Delete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_MV_Delete.Location = New System.Drawing.Point(326, 3)
		Me.btn_MV_Delete.Name = "btn_MV_Delete"
		Me.btn_MV_Delete.Size = New System.Drawing.Size(75, 20)
		ToolTipTitleItem10.Text = "Delete selected Volume from list"
		SuperToolTip11.Items.Add(ToolTipTitleItem10)
		Me.btn_MV_Delete.SuperTip = SuperToolTip11
		Me.btn_MV_Delete.TabIndex = 1
		Me.btn_MV_Delete.Text = "&Delete"
		'
		'btn_MV_Add
		'
		Me.btn_MV_Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_MV_Add.Location = New System.Drawing.Point(248, 3)
		Me.btn_MV_Add.Name = "btn_MV_Add"
		Me.btn_MV_Add.Size = New System.Drawing.Size(75, 20)
		ToolTipTitleItem11.Text = "Add a Volume to the list"
		SuperToolTip12.Items.Add(ToolTipTitleItem11)
		Me.btn_MV_Add.SuperTip = SuperToolTip12
		Me.btn_MV_Add.TabIndex = 0
		Me.btn_MV_Add.Text = "&Add"
		'
		'BTA_Libretro_Core
		'
		Me.BTA_Libretro_Core.AllowDelete = True
		Me.BTA_Libretro_Core.ColumnUpdateBlacklistStream = CType(resources.GetObject("BTA_Libretro_Core.ColumnUpdateBlacklistStream"), System.Collections.Generic.List(Of String))
		Me.BTA_Libretro_Core.Connection = Nothing
		Me.BTA_Libretro_Core.DSStream = CType(resources.GetObject("BTA_Libretro_Core.DSStream"), System.IO.MemoryStream)
		Me.BTA_Libretro_Core.FillString = ""
		Me.BTA_Libretro_Core.Sort = "Displayname"
		Me.BTA_Libretro_Core.Transaction = Nothing
		Me.BTA_Libretro_Core.UpdateTablesStream = CType(resources.GetObject("BTA_Libretro_Core.UpdateTablesStream"), System.Collections.Generic.List(Of String))
		'
		'BS_J2K
		'
		Me.BS_J2K.DataMember = "tbl_Config"
		Me.BS_J2K.DataSource = Me.DS_J2K
		'
		'DS_J2K
		'
		Me.DS_J2K.DataSetName = "DS"
		Me.DS_J2K.Tables.AddRange(New System.Data.DataTable() {Me.tbl_Config})
		'
		'tbl_Config
		'
		Me.tbl_Config.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn5, Me.DataColumn6})
		Me.tbl_Config.TableName = "tbl_Config"
		'
		'DataColumn5
		'
		Me.DataColumn5.ColumnName = "id_Config"
		Me.DataColumn5.DataType = GetType(Integer)
		'
		'DataColumn6
		'
		Me.DataColumn6.ColumnName = "ConfigName"
		'
		'BS_DOSBox_Patches_Categories
		'
		Me.BS_DOSBox_Patches_Categories.DataMember = "src_frm_Emulators_DOSBox_Patches_Categories"
		Me.BS_DOSBox_Patches_Categories.DataSource = Me.DS_ML
		'
		'BS_DOSBox_Patches
		'
		Me.BS_DOSBox_Patches.DataMember = "src_frm_Emulators_DOSBox_Patches"
		Me.BS_DOSBox_Patches.DataSource = Me.DS_ML
		Me.BS_DOSBox_Patches.Filter = "id_DOSBox_Patches_Categories = 0"
		'
		'btn_Close
		'
		Me.btn_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Close.Location = New System.Drawing.Point(332, 4)
		Me.btn_Close.Name = "btn_Close"
		Me.btn_Close.Size = New System.Drawing.Size(75, 20)
		Me.btn_Close.TabIndex = 1
		Me.btn_Close.Text = "&Close"
		'
		'BS_rpi_Platforms
		'
		Me.BS_rpi_Platforms.DataSource = Me.DS_ML
		Me.BS_rpi_Platforms.Position = 0
		'
		'DS_MobyDB
		'
		Me.DS_MobyDB.DataSetName = "DS_MobyDB"
		Me.DS_MobyDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'pnl_Right
		'
		Me.pnl_Right.Controls.Add(Me.pnl_Platforms)
		Me.pnl_Right.Controls.Add(Me.pnl_Buttons)
		Me.pnl_Right.Controls.Add(Me.Ctl_MKDXSplitter2)
		Me.pnl_Right.Controls.Add(Me.pnl_Settings)
		Me.pnl_Right.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_Right.Location = New System.Drawing.Point(265, 0)
		Me.pnl_Right.Name = "pnl_Right"
		Me.pnl_Right.Size = New System.Drawing.Size(415, 585)
		Me.pnl_Right.TabIndex = 11
		Me.pnl_Right.Visible = False
		'
		'pnl_Buttons
		'
		Me.pnl_Buttons.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_Buttons.Controls.Add(Me.btn_Save)
		Me.pnl_Buttons.Controls.Add(Me.btn_Close)
		Me.pnl_Buttons.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.pnl_Buttons.Location = New System.Drawing.Point(2, 557)
		Me.pnl_Buttons.Name = "pnl_Buttons"
		Me.pnl_Buttons.Size = New System.Drawing.Size(411, 26)
		Me.pnl_Buttons.TabIndex = 7
		'
		'popmnu_Emulators
		'
		Me.popmnu_Emulators.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Add), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Import), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Delete)})
		Me.popmnu_Emulators.Manager = Me.barmng
		Me.popmnu_Emulators.Name = "popmnu_Emulators"
		'
		'DataTable1
		'
		Me.DataTable1.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn1, Me.DataColumn2})
		Me.DataTable1.TableName = "Table1"
		'
		'DataColumn1
		'
		Me.DataColumn1.ColumnName = "DLL"
		'
		'DataColumn2
		'
		Me.DataColumn2.ColumnName = "Displayname"
		'
		'frm_Known_Emulators
		'
		Me.ClientSize = New System.Drawing.Size(680, 585)
		Me.Controls.Add(Me.pnl_Right)
		Me.Controls.Add(Me.Ctl_MKDXSplitter1)
		Me.Controls.Add(Me.gb_Emulators)
		Me.Controls.Add(Me.barDockControlLeft)
		Me.Controls.Add(Me.barDockControlRight)
		Me.Controls.Add(Me.barDockControlBottom)
		Me.Controls.Add(Me.barDockControlTop)
		Me.Name = "frm_Known_Emulators"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "KNOWN Emulators"
		Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
		CType(Me.gb_Emulators, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_Emulators.ResumeLayout(False)
		CType(Me.grd_Emulators, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Emulators, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_Rombase, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Emulators, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pnl_Emulators_Buttons, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_Emulators_Buttons.ResumeLayout(False)
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pnl_Platforms, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_Platforms.ResumeLayout(False)
		CType(Me.grd_Platforms, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Platforms, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Platforms, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_Supported, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_DefaultEmulator, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pnl_Settings, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_Settings.ResumeLayout(False)
		CType(Me.tcl_Settings, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tcl_Settings.ResumeLayout(False)
		Me.tpg_Settings.ResumeLayout(False)
		CType(Me.Ctl_MKDXSplitPanel1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Ctl_MKDXSplitPanel1.ResumeLayout(False)
		CType(Me.txb_Description.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.barmng, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Ctl_MKDXMemoEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_Exe_Identifier_Regex.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_URL_Download.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_URL_Website.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_Identifier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.cmb_List_Generator.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_List_Generators, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_ScreenshotDirectory.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_StartupParameter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_Name.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tpg_MV_Settings.ResumeLayout(False)
		CType(Me.grd_MV, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_MV, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_MV, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_MV, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Volumes, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pnl_MV_Buttons, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_MV_Buttons.ResumeLayout(False)
		CType(Me.BTA_Libretro_Core, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_J2K, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_J2K, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.tbl_Config, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_DOSBox_Patches_Categories, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_DOSBox_Patches, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_rpi_Platforms, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_MobyDB, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pnl_Right, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_Right.ResumeLayout(False)
		CType(Me.pnl_Buttons, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_Buttons.ResumeLayout(False)
		CType(Me.popmnu_Emulators, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents gb_Emulators As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents grd_Emulators As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_Emulators As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents pnl_Emulators_Buttons As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents btn_Delete_Known_Emulator As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_Add_Known_Emulator As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents Ctl_MKDXSplitter1 As MKNetDXLib.ctl_MKDXSplitter
	Friend WithEvents DS_ML As Metropolis_Launcher.DS_ML
	Friend WithEvents BS_Emulators As System.Windows.Forms.BindingSource
	Friend WithEvents colDisplayname As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents grd_Platforms As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_Platforms As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents lbl_Platforms As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents BS_Platforms As System.Windows.Forms.BindingSource
	Friend WithEvents colSupported As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents rpi_Supported As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
	Friend WithEvents lbl_Screenshot_Directory As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_StartupParameter As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Name As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents btn_Close As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents pnl_Platforms As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents Ctl_MKDXSplitter2 As MKNetDXLib.ctl_MKDXSplitter
	Friend WithEvents pnl_Settings As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents txb_ScreenshotDirectory As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents txb_StartupParameter As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents txb_Name As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents btn_Save As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents BS_rpi_Platforms As System.Windows.Forms.BindingSource
	Friend WithEvents DS_MobyDB As Metropolis_Launcher.DS_MobyDB
	Friend WithEvents colDisplay_Name As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents rpi_DefaultEmulator As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
	Friend WithEvents tcl_Settings As MKNetDXLib.ctl_MKDXTabControl
	Friend WithEvents tpg_Settings As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_MV_Settings As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents pnl_MV_Buttons As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents btn_MV_Delete As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_MV_Add As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents grd_MV As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_MV As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents pnl_Right As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents pnl_Buttons As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents BS_MV As System.Windows.Forms.BindingSource
	Friend WithEvents colVolume_Number As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colParameter As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents BS_Volumes As System.Windows.Forms.BindingSource
	Friend WithEvents rpi_MV As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
	Friend WithEvents DS_J2K As System.Data.DataSet
	Friend WithEvents tbl_Config As System.Data.DataTable
	Friend WithEvents DataColumn5 As System.Data.DataColumn
	Friend WithEvents DataColumn6 As System.Data.DataColumn
	Friend WithEvents BS_J2K As System.Windows.Forms.BindingSource
	Friend WithEvents BS_DOSBox_Patches_Categories As System.Windows.Forms.BindingSource
	Friend WithEvents BS_DOSBox_Patches As System.Windows.Forms.BindingSource
	Friend WithEvents popmnu_Emulators As MKNetDXLib.cmp_MKDXPopupMenu
	Friend WithEvents bbi_Add As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Import As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Delete As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents barmng As MKNetDXLib.ctl_MKDXBarManager
	Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
	Friend WithEvents BTA_Libretro_Core As MKNetLib.cmp_MKBindableTableAdapter
	Friend WithEvents DataTable1 As DataTable
	Friend WithEvents DataColumn1 As DataColumn
	Friend WithEvents DataColumn2 As DataColumn
	Friend WithEvents cmb_List_Generator As MKNetDXLib.ctl_MKDXLookupEdit
	Friend WithEvents lbl_List_Generator As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents BS_List_Generators As BindingSource
	Friend WithEvents txb_Description As MKNetDXLib.ctl_MKDXMemoEdit
	Friend WithEvents lbl_Description As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_URL_Download As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents txb_URL_Download As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents lbl_URL_Website As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents txb_URL_Website As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents lbl_Identifier As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents txb_Identifier As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents lbl_Exe_Identifier_Regex As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents txb_Exe_Identifier_Regex As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents DS_Rombase As DS_Rombase
	Friend WithEvents colIdentifier As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents btn_Import_Known_Emulator As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents Ctl_MKDXSplitPanel1 As MKNetDXLib.ctl_MKDXSplitPanel
	Friend WithEvents Ctl_MKDXMemoEdit1 As MKNetDXLib.ctl_MKDXMemoEdit
	Friend WithEvents lbl_Autoconfig_Note As MKNetDXLib.ctl_MKDXLabel
End Class
