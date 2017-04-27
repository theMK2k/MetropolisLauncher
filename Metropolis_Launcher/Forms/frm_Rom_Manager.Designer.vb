<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Rom_Manager
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Rom_Manager))
		Dim SuperToolTip3 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem3 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem3 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Me.rpi_Moby_Release = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		Me.BS_Moby_Releases = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_MobyDB = New Metropolis_Launcher.DS_MobyDB()
		Me.rpi_chb_Hidden = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
		Me.pnl_Right = New MKNetDXLib.ctl_MKDXPanel()
		Me.lbl_Moby_Releases = New MKNetDXLib.ctl_MKDXLabel()
		Me.grd_Moby_Releases = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_Moby_Releases = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colGamename = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colYear = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colAdded1 = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colHighlighted = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colDeveloper = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colPublisher = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.Ctl_MKDXSplitter1 = New MKNetDXLib.ctl_MKDXSplitter()
		Me.pnl_Left = New MKNetDXLib.ctl_MKDXPanel()
		Me.btn_Save = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.lbl_Platform = New MKNetDXLib.ctl_MKDXLabel()
		Me.cmb_Platform = New MKNetDXLib.ctl_MKDXLookupEdit()
		Me.BS_Moby_Platforms = New System.Windows.Forms.BindingSource(Me.components)
		Me.grd_Emu_Games = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_Emu_Games = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_ML = New Metropolis_Launcher.DS_ML()
		Me.gv_Emu_Games = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colFolder = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colfile = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colInnerFile = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colMoby_Games_URLPart = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colHidden = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colAdded = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.BS_Moby_Platforms_gv1 = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_Rombase = New Metropolis_Launcher.DS_Rombase()
		Me.BS_Rombase = New System.Windows.Forms.BindingSource(Me.components)
		Me.BS_Moby_Platforms_gv2 = New System.Windows.Forms.BindingSource(Me.components)
		Me.spltpnl_Right = New MKNetDXLib.ctl_MKDXSplitPanel()
		Me.tcl_MV = New MKNetDXLib.ctl_MKDXTabControl()
		Me.tpg_Discs_Volumes = New DevExpress.XtraTab.XtraTabPage()
		Me.pnl_Discs_Volumes = New MKNetDXLib.ctl_MKDXPanel()
		Me.grd_MV = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_MV = New System.Windows.Forms.BindingSource(Me.components)
		Me.gv_MV = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.RepositoryItemLookUpEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
		Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colVolume_Number = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.rpi_Volume_Number = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		Me.BS_MV_Volume = New System.Windows.Forms.BindingSource(Me.components)
		Me.lbl_Volumes = New MKNetDXLib.ctl_MKDXLabel()
		Me.tpg_DOSBox_Files_Directories = New DevExpress.XtraTab.XtraTabPage()
		Me.pnl_DOSBox_Files_and_Folders = New MKNetDXLib.ctl_MKDXPanel()
		Me.Ctl_MKDXSplitPanel1 = New MKNetDXLib.ctl_MKDXSplitPanel()
		Me.grd_DOSBox_Files_and_Folders = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_DOSBox_Files_and_Folders = New System.Windows.Forms.BindingSource(Me.components)
		Me.gv_DOSBox_Files_and_Folders = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_DOSBox_Displayname = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colid_Rombase_DOSBox_Filetypes = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colDOSBox_Mount_Destination = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colVolume_Number1 = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.rpi_DOSBox_Volume = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colInnerFile1 = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.lbl_DOSBox_Files_and_Folders = New MKNetDXLib.ctl_MKDXLabel()
		Me.txb_DOSBox_Inner_File = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.txb_DOSBox_File = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.txb_DOSBox_Folder = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.cmb_DOSBox_Volume_Number = New MKNetDXLib.ctl_MKDXLookupEdit()
		Me.cmb_DOSBox_Mount_Destination = New MKNetDXLib.ctl_MKDXLookupEdit()
		Me.BTA_DOSBox_Mount_Destination = New MKNetLib.cmp_MKBindableTableAdapter(Me.components)
		Me.cmb_DOSBox_Exe_Type = New MKNetDXLib.ctl_MKDXLookupEdit()
		Me.BTA_DOSBox_Exe_Types = New MKNetLib.cmp_MKBindableTableAdapter(Me.components)
		Me.cmb_DOSBox_Type = New MKNetDXLib.ctl_MKDXLookupEdit()
		Me.BTA_DOSBox_Filetypes = New MKNetLib.cmp_MKBindableTableAdapter(Me.components)
		Me.lbl_DOSBox_Volume_Number = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_DOSBox_Mount_Destination = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_DOSBox_InnerFile = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_DOSBox_File = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_DOSBox_Exe_Type = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_DOSBox_Folder = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_DOSBox_Type = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_DOSBox_Folder_and_Files_Settings = New MKNetDXLib.ctl_MKDXLabel()
		Me.DataTable1 = New System.Data.DataTable()
		Me.DataColumn12 = New System.Data.DataColumn()
		Me.DataColumn13 = New System.Data.DataColumn()
		Me.DataColumn14 = New System.Data.DataColumn()
		Me.DataTable2 = New System.Data.DataTable()
		Me.DataColumn15 = New System.Data.DataColumn()
		Me.DataColumn16 = New System.Data.DataColumn()
		Me.DataColumn17 = New System.Data.DataColumn()
		Me.DataTable3 = New System.Data.DataTable()
		Me.DataColumn18 = New System.Data.DataColumn()
		Me.DataColumn19 = New System.Data.DataColumn()
		Me.barmng = New MKNetDXLib.ctl_MKDXBarManager()
		Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
		Me.bbi_AddGames = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_AddGamesFolder = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Add_DOSBox_Game_Directory = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Add_DOSBox_Game_Media = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Edit_Game = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Edit_Multiple_Games = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Change_Directory = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Rescan = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_SetHidden = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_UnsetHidden = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_SetLink = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_RemoveLink = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Delete_Games = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Merge_Select = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Merge_Start = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Export = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Debug_Import_XML = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Debug_Export_XML = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Debug_Group_Volumes = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Debug_SetModified = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Debug_Apply_TDC = New DevExpress.XtraBars.BarButtonItem()
		Me.SkinBarSubItem1 = New DevExpress.XtraBars.SkinBarSubItem()
		Me.bbi_DOSBox_Files_and_Folders_Rename = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_DOSBox_Files_and_Folders_Add_Archive = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_DOSBox_Files_and_Folders_Add_Directory = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_DOSBox_Files_and_Folders_Add_Media = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Moby_Games_Open_Moby_Page = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Moby_Games_Evaluate_Links = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Auto_Link = New DevExpress.XtraBars.BarButtonItem()
		Me.popmnu_Rom_Manager = New MKNetDXLib.cmp_MKDXPopupMenu()
		Me.popmnu_DOSBox_Files_and_Folders = New MKNetDXLib.cmp_MKDXPopupMenu()
		Me.popmnu_Moby_Games = New MKNetDXLib.cmp_MKDXPopupMenu()
		Me.coldeprecated = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.coldeprecated1 = New DevExpress.XtraGrid.Columns.GridColumn()
		CType(Me.rpi_Moby_Release, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Moby_Releases, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_MobyDB, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_chb_Hidden, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.pnl_Right, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_Right.SuspendLayout()
		CType(Me.grd_Moby_Releases, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_Moby_Releases, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.pnl_Left, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_Left.SuspendLayout()
		CType(Me.cmb_Platform.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Moby_Platforms, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.grd_Emu_Games, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Emu_Games, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_Emu_Games, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Moby_Platforms_gv1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_Rombase, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Rombase, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Moby_Platforms_gv2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spltpnl_Right, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.spltpnl_Right.SuspendLayout()
		CType(Me.tcl_MV, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tcl_MV.SuspendLayout()
		Me.tpg_Discs_Volumes.SuspendLayout()
		CType(Me.pnl_Discs_Volumes, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_Discs_Volumes.SuspendLayout()
		CType(Me.grd_MV, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_MV, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_MV, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.RepositoryItemLookUpEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_Volume_Number, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_MV_Volume, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tpg_DOSBox_Files_Directories.SuspendLayout()
		CType(Me.pnl_DOSBox_Files_and_Folders, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_DOSBox_Files_and_Folders.SuspendLayout()
		CType(Me.Ctl_MKDXSplitPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Ctl_MKDXSplitPanel1.SuspendLayout()
		CType(Me.grd_DOSBox_Files_and_Folders, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_DOSBox_Files_and_Folders, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_DOSBox_Files_and_Folders, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_DOSBox_Volume, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_DOSBox_Inner_File.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_DOSBox_File.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_DOSBox_Folder.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.cmb_DOSBox_Volume_Number.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.cmb_DOSBox_Mount_Destination.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BTA_DOSBox_Mount_Destination, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.cmb_DOSBox_Exe_Type.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BTA_DOSBox_Exe_Types, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.cmb_DOSBox_Type.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BTA_DOSBox_Filetypes, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DataTable2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DataTable3, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.barmng, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.popmnu_Rom_Manager, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.popmnu_DOSBox_Files_and_Folders, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.popmnu_Moby_Games, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'rpi_Moby_Release
		'
		Me.rpi_Moby_Release.AutoHeight = False
		Me.rpi_Moby_Release.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.rpi_Moby_Release.DataSource = Me.BS_Moby_Releases
		Me.rpi_Moby_Release.DisplayMember = "Gamename"
		Me.rpi_Moby_Release.Name = "rpi_Moby_Release"
		Me.rpi_Moby_Release.NullText = ""
		Me.rpi_Moby_Release.ValueMember = "Moby_Games_URLPart"
		'
		'BS_Moby_Releases
		'
		Me.BS_Moby_Releases.DataMember = "src_Moby_Releases"
		Me.BS_Moby_Releases.DataSource = Me.DS_MobyDB
		'
		'DS_MobyDB
		'
		Me.DS_MobyDB.DataSetName = "DS_MobyDB"
		Me.DS_MobyDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'rpi_chb_Hidden
		'
		Me.rpi_chb_Hidden.AutoHeight = False
		Me.rpi_chb_Hidden.Name = "rpi_chb_Hidden"
		Me.rpi_chb_Hidden.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
		'
		'pnl_Right
		'
		Me.pnl_Right.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_Right.Controls.Add(Me.lbl_Moby_Releases)
		Me.pnl_Right.Controls.Add(Me.grd_Moby_Releases)
		Me.pnl_Right.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_Right.Location = New System.Drawing.Point(0, 0)
		Me.pnl_Right.Name = "pnl_Right"
		Me.pnl_Right.Size = New System.Drawing.Size(589, 313)
		Me.pnl_Right.TabIndex = 5
		'
		'lbl_Moby_Releases
		'
		Me.lbl_Moby_Releases.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Moby_Releases.Appearance.Font = New System.Drawing.Font("Segoe UI", 16.0!)
		Me.lbl_Moby_Releases.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Moby_Releases.Location = New System.Drawing.Point(3, 4)
		Me.lbl_Moby_Releases.MKBoundControl1 = Nothing
		Me.lbl_Moby_Releases.MKBoundControl2 = Nothing
		Me.lbl_Moby_Releases.MKBoundControl3 = Nothing
		Me.lbl_Moby_Releases.MKBoundControl4 = Nothing
		Me.lbl_Moby_Releases.MKBoundControl5 = Nothing
		Me.lbl_Moby_Releases.Name = "lbl_Moby_Releases"
		Me.lbl_Moby_Releases.Size = New System.Drawing.Size(582, 42)
		Me.lbl_Moby_Releases.TabIndex = 2
		Me.lbl_Moby_Releases.Text = "Moby Releases"
		'
		'grd_Moby_Releases
		'
		Me.grd_Moby_Releases.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.grd_Moby_Releases.DataSource = Me.BS_Moby_Releases
		Me.grd_Moby_Releases.EmbeddedNavigator.Buttons.Append.Visible = False
		Me.grd_Moby_Releases.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
		Me.grd_Moby_Releases.EmbeddedNavigator.Buttons.Edit.Visible = False
		Me.grd_Moby_Releases.EmbeddedNavigator.Buttons.EndEdit.Visible = False
		Me.grd_Moby_Releases.EmbeddedNavigator.Buttons.Remove.Visible = False
		Me.grd_Moby_Releases.EmbeddedNavigator.TextStringFormat = "{0} of {1}"
		Me.grd_Moby_Releases.Location = New System.Drawing.Point(3, 49)
		Me.grd_Moby_Releases.MainView = Me.gv_Moby_Releases
		Me.grd_Moby_Releases.Name = "grd_Moby_Releases"
		Me.grd_Moby_Releases.Size = New System.Drawing.Size(582, 262)
		Me.grd_Moby_Releases.TabIndex = 0
		Me.grd_Moby_Releases.UseEmbeddedNavigator = True
		Me.grd_Moby_Releases.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Moby_Releases})
		'
		'gv_Moby_Releases
		'
		Me.gv_Moby_Releases.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colGamename, Me.colYear, Me.colAdded1, Me.colHighlighted, Me.colDeveloper, Me.colPublisher, Me.coldeprecated1})
		Me.gv_Moby_Releases.GridControl = Me.grd_Moby_Releases
		Me.gv_Moby_Releases.Name = "gv_Moby_Releases"
		Me.gv_Moby_Releases.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_Moby_Releases.OptionsView.ColumnAutoWidth = False
		Me.gv_Moby_Releases.OptionsView.ShowGroupPanel = False
		'
		'colGamename
		'
		Me.colGamename.FieldName = "Gamename"
		Me.colGamename.Name = "colGamename"
		Me.colGamename.OptionsColumn.AllowEdit = False
		Me.colGamename.OptionsColumn.ReadOnly = True
		Me.colGamename.Visible = True
		Me.colGamename.VisibleIndex = 0
		Me.colGamename.Width = 1234
		'
		'colYear
		'
		Me.colYear.FieldName = "Year"
		Me.colYear.Name = "colYear"
		Me.colYear.OptionsColumn.AllowEdit = False
		Me.colYear.OptionsColumn.ReadOnly = True
		Me.colYear.Visible = True
		Me.colYear.VisibleIndex = 1
		Me.colYear.Width = 260
		'
		'colAdded1
		'
		Me.colAdded1.Caption = "Added"
		Me.colAdded1.DisplayFormat.FormatString = "g"
		Me.colAdded1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
		Me.colAdded1.FieldName = "created"
		Me.colAdded1.Name = "colAdded1"
		Me.colAdded1.OptionsColumn.AllowEdit = False
		Me.colAdded1.OptionsColumn.ReadOnly = True
		Me.colAdded1.Visible = True
		Me.colAdded1.VisibleIndex = 2
		Me.colAdded1.Width = 171
		'
		'colHighlighted
		'
		Me.colHighlighted.FieldName = "Highlighted"
		Me.colHighlighted.Name = "colHighlighted"
		'
		'colDeveloper
		'
		Me.colDeveloper.FieldName = "Developer"
		Me.colDeveloper.Name = "colDeveloper"
		Me.colDeveloper.OptionsColumn.AllowEdit = False
		Me.colDeveloper.OptionsColumn.ReadOnly = True
		Me.colDeveloper.Visible = True
		Me.colDeveloper.VisibleIndex = 3
		'
		'colPublisher
		'
		Me.colPublisher.FieldName = "Publisher"
		Me.colPublisher.Name = "colPublisher"
		Me.colPublisher.OptionsColumn.AllowEdit = False
		Me.colPublisher.OptionsColumn.ReadOnly = True
		Me.colPublisher.Visible = True
		Me.colPublisher.VisibleIndex = 4
		'
		'Ctl_MKDXSplitter1
		'
		Me.Ctl_MKDXSplitter1.Location = New System.Drawing.Point(414, 0)
		Me.Ctl_MKDXSplitter1.Name = "Ctl_MKDXSplitter1"
		Me.Ctl_MKDXSplitter1.Size = New System.Drawing.Size(5, 730)
		Me.Ctl_MKDXSplitter1.TabIndex = 4
		Me.Ctl_MKDXSplitter1.TabStop = False
		'
		'pnl_Left
		'
		Me.pnl_Left.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_Left.Controls.Add(Me.btn_Save)
		Me.pnl_Left.Controls.Add(Me.lbl_Platform)
		Me.pnl_Left.Controls.Add(Me.cmb_Platform)
		Me.pnl_Left.Controls.Add(Me.grd_Emu_Games)
		Me.pnl_Left.Dock = System.Windows.Forms.DockStyle.Left
		Me.pnl_Left.Location = New System.Drawing.Point(0, 0)
		Me.pnl_Left.Name = "pnl_Left"
		Me.pnl_Left.Size = New System.Drawing.Size(414, 730)
		Me.pnl_Left.TabIndex = 3
		'
		'btn_Save
		'
		Me.btn_Save.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Save.Location = New System.Drawing.Point(319, 3)
		Me.btn_Save.Name = "btn_Save"
		Me.btn_Save.Size = New System.Drawing.Size(91, 20)
		Me.btn_Save.TabIndex = 2
		Me.btn_Save.Text = "&Save"
		'
		'lbl_Platform
		'
		Me.lbl_Platform.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Platform.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Platform.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Platform.MKBoundControl1 = Nothing
		Me.lbl_Platform.MKBoundControl2 = Nothing
		Me.lbl_Platform.MKBoundControl3 = Nothing
		Me.lbl_Platform.MKBoundControl4 = Nothing
		Me.lbl_Platform.MKBoundControl5 = Nothing
		Me.lbl_Platform.Name = "lbl_Platform"
		Me.lbl_Platform.Size = New System.Drawing.Size(90, 20)
		Me.lbl_Platform.TabIndex = 2
		Me.lbl_Platform.Text = "Platform:"
		'
		'cmb_Platform
		'
		Me.cmb_Platform.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmb_Platform.Location = New System.Drawing.Point(96, 3)
		Me.cmb_Platform.MKBoundLabel = Nothing
		Me.cmb_Platform.MKEditValue_Compare = Nothing
		Me.cmb_Platform.Name = "cmb_Platform"
		Me.cmb_Platform.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.cmb_Platform.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_Moby_Platforms", "id_Moby_Platforms", 114, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Display_Name", "Display_Name", 77, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near)})
		Me.cmb_Platform.Properties.DataSource = Me.BS_Moby_Platforms
		Me.cmb_Platform.Properties.DisplayMember = "Display_Name"
		Me.cmb_Platform.Properties.NullText = "please choose..."
		Me.cmb_Platform.Properties.ShowHeader = False
		Me.cmb_Platform.Properties.ValueMember = "id_Moby_Platforms"
		Me.cmb_Platform.Size = New System.Drawing.Size(220, 20)
		Me.cmb_Platform.TabIndex = 1
		'
		'BS_Moby_Platforms
		'
		Me.BS_Moby_Platforms.DataMember = "tbl_Moby_Platforms"
		Me.BS_Moby_Platforms.DataSource = Me.DS_MobyDB
		'
		'grd_Emu_Games
		'
		Me.grd_Emu_Games.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.grd_Emu_Games.DataSource = Me.BS_Emu_Games
		Me.grd_Emu_Games.EmbeddedNavigator.Buttons.Append.Visible = False
		Me.grd_Emu_Games.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
		Me.grd_Emu_Games.EmbeddedNavigator.Buttons.Edit.Visible = False
		Me.grd_Emu_Games.EmbeddedNavigator.Buttons.EndEdit.Visible = False
		Me.grd_Emu_Games.EmbeddedNavigator.Buttons.Remove.Visible = False
		Me.grd_Emu_Games.EmbeddedNavigator.TextStringFormat = "{0} of {1}"
		Me.grd_Emu_Games.Location = New System.Drawing.Point(3, 42)
		Me.grd_Emu_Games.MainView = Me.gv_Emu_Games
		Me.grd_Emu_Games.Name = "grd_Emu_Games"
		Me.grd_Emu_Games.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rpi_Moby_Release})
		Me.grd_Emu_Games.Size = New System.Drawing.Size(408, 686)
		Me.grd_Emu_Games.TabIndex = 0
		Me.grd_Emu_Games.UseEmbeddedNavigator = True
		Me.grd_Emu_Games.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Emu_Games})
		'
		'BS_Emu_Games
		'
		Me.BS_Emu_Games.DataMember = "tbl_Emu_Games"
		Me.BS_Emu_Games.DataSource = Me.DS_ML
		Me.BS_Emu_Games.Filter = "id_Emu_Games_Owner IS NULL"
		'
		'DS_ML
		'
		Me.DS_ML.DataSetName = "DS_ML"
		Me.DS_ML.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'gv_Emu_Games
		'
		Me.gv_Emu_Games.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colFolder, Me.colfile, Me.colInnerFile, Me.colMoby_Games_URLPart, Me.colHidden, Me.colAdded, Me.coldeprecated})
		Me.gv_Emu_Games.GridControl = Me.grd_Emu_Games
		Me.gv_Emu_Games.GroupCount = 1
		Me.gv_Emu_Games.Name = "gv_Emu_Games"
		Me.gv_Emu_Games.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_Emu_Games.OptionsSelection.MultiSelect = True
		Me.gv_Emu_Games.OptionsView.ColumnAutoWidth = False
		Me.gv_Emu_Games.OptionsView.ShowGroupPanel = False
		Me.gv_Emu_Games.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.colFolder, DevExpress.Data.ColumnSortOrder.Ascending)})
		'
		'colFolder
		'
		Me.colFolder.Caption = "Directory"
		Me.colFolder.FieldName = "Folder"
		Me.colFolder.Name = "colFolder"
		Me.colFolder.OptionsColumn.AllowEdit = False
		Me.colFolder.OptionsColumn.ReadOnly = True
		Me.colFolder.Visible = True
		Me.colFolder.VisibleIndex = 2
		'
		'colfile
		'
		Me.colfile.Caption = "Filename"
		Me.colfile.FieldName = "File"
		Me.colfile.Name = "colfile"
		Me.colfile.OptionsColumn.AllowEdit = False
		Me.colfile.OptionsColumn.ReadOnly = True
		Me.colfile.Visible = True
		Me.colfile.VisibleIndex = 0
		Me.colfile.Width = 140
		'
		'colInnerFile
		'
		Me.colInnerFile.Caption = "Inner File"
		Me.colInnerFile.FieldName = "InnerFile"
		Me.colInnerFile.Name = "colInnerFile"
		Me.colInnerFile.OptionsColumn.AllowEdit = False
		Me.colInnerFile.OptionsColumn.ReadOnly = True
		Me.colInnerFile.Visible = True
		Me.colInnerFile.VisibleIndex = 1
		Me.colInnerFile.Width = 175
		'
		'colMoby_Games_URLPart
		'
		Me.colMoby_Games_URLPart.Caption = "Moby Release"
		Me.colMoby_Games_URLPart.ColumnEdit = Me.rpi_Moby_Release
		Me.colMoby_Games_URLPart.FieldName = "Moby_Games_URLPart"
		Me.colMoby_Games_URLPart.Name = "colMoby_Games_URLPart"
		Me.colMoby_Games_URLPart.OptionsColumn.AllowEdit = False
		Me.colMoby_Games_URLPart.OptionsColumn.ReadOnly = True
		Me.colMoby_Games_URLPart.Visible = True
		Me.colMoby_Games_URLPart.VisibleIndex = 2
		Me.colMoby_Games_URLPart.Width = 102
		'
		'colHidden
		'
		Me.colHidden.ColumnEdit = Me.rpi_chb_Hidden
		Me.colHidden.FieldName = "Hidden"
		Me.colHidden.Name = "colHidden"
		Me.colHidden.Visible = True
		Me.colHidden.VisibleIndex = 3
		Me.colHidden.Width = 50
		'
		'colAdded
		'
		Me.colAdded.Caption = "Added"
		Me.colAdded.DisplayFormat.FormatString = "g"
		Me.colAdded.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
		Me.colAdded.FieldName = "created"
		Me.colAdded.Name = "colAdded"
		Me.colAdded.OptionsColumn.AllowEdit = False
		Me.colAdded.OptionsColumn.ReadOnly = True
		Me.colAdded.Visible = True
		Me.colAdded.VisibleIndex = 4
		Me.colAdded.Width = 94
		'
		'BS_Moby_Platforms_gv1
		'
		Me.BS_Moby_Platforms_gv1.DataMember = "tbl_Moby_Platforms"
		Me.BS_Moby_Platforms_gv1.DataSource = Me.DS_MobyDB
		'
		'DS_Rombase
		'
		Me.DS_Rombase.DataSetName = "DS_Rombase"
		Me.DS_Rombase.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'BS_Rombase
		'
		Me.BS_Rombase.DataMember = "tbl_Rombase"
		Me.BS_Rombase.DataSource = Me.DS_Rombase
		'
		'BS_Moby_Platforms_gv2
		'
		Me.BS_Moby_Platforms_gv2.DataMember = "tbl_Moby_Platforms"
		Me.BS_Moby_Platforms_gv2.DataSource = Me.DS_MobyDB
		'
		'spltpnl_Right
		'
		Me.spltpnl_Right.Dock = System.Windows.Forms.DockStyle.Fill
		Me.spltpnl_Right.Horizontal = False
		Me.spltpnl_Right.Location = New System.Drawing.Point(419, 0)
		Me.spltpnl_Right.Name = "spltpnl_Right"
		Me.spltpnl_Right.Panel1.Controls.Add(Me.tcl_MV)
		Me.spltpnl_Right.Panel1.Text = "pnl_MV"
		Me.spltpnl_Right.Panel2.Controls.Add(Me.pnl_Right)
		Me.spltpnl_Right.Panel2.Text = "pnl_Moby"
		Me.spltpnl_Right.Size = New System.Drawing.Size(589, 730)
		Me.spltpnl_Right.SplitterPosition = 412
		Me.spltpnl_Right.TabIndex = 3
		Me.spltpnl_Right.Text = "Ctl_MKDXSplitPanel1"
		'
		'tcl_MV
		'
		Me.tcl_MV.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tcl_MV.Location = New System.Drawing.Point(0, 0)
		Me.tcl_MV.Name = "tcl_MV"
		Me.tcl_MV.SelectedTabPage = Me.tpg_Discs_Volumes
		Me.tcl_MV.Size = New System.Drawing.Size(589, 412)
		Me.tcl_MV.TabIndex = 0
		Me.tcl_MV.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tpg_Discs_Volumes, Me.tpg_DOSBox_Files_Directories})
		'
		'tpg_Discs_Volumes
		'
		Me.tpg_Discs_Volumes.Controls.Add(Me.pnl_Discs_Volumes)
		Me.tpg_Discs_Volumes.Name = "tpg_Discs_Volumes"
		Me.tpg_Discs_Volumes.Size = New System.Drawing.Size(583, 384)
		Me.tpg_Discs_Volumes.Text = "Discs/Volumes"
		'
		'pnl_Discs_Volumes
		'
		Me.pnl_Discs_Volumes.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_Discs_Volumes.Controls.Add(Me.grd_MV)
		Me.pnl_Discs_Volumes.Controls.Add(Me.lbl_Volumes)
		Me.pnl_Discs_Volumes.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_Discs_Volumes.Location = New System.Drawing.Point(0, 0)
		Me.pnl_Discs_Volumes.Name = "pnl_Discs_Volumes"
		Me.pnl_Discs_Volumes.Size = New System.Drawing.Size(583, 384)
		Me.pnl_Discs_Volumes.TabIndex = 5
		'
		'grd_MV
		'
		Me.grd_MV.DataSource = Me.BS_MV
		Me.grd_MV.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_MV.EmbeddedNavigator.Buttons.Append.Visible = False
		Me.grd_MV.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
		Me.grd_MV.EmbeddedNavigator.Buttons.Edit.Visible = False
		Me.grd_MV.EmbeddedNavigator.Buttons.EndEdit.Visible = False
		Me.grd_MV.EmbeddedNavigator.Buttons.Remove.Visible = False
		Me.grd_MV.EmbeddedNavigator.TextStringFormat = "{0} of {1}"
		Me.grd_MV.Location = New System.Drawing.Point(0, 42)
		Me.grd_MV.MainView = Me.gv_MV
		Me.grd_MV.Name = "grd_MV"
		Me.grd_MV.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemLookUpEdit1, Me.RepositoryItemCheckEdit1, Me.rpi_Volume_Number})
		Me.grd_MV.Size = New System.Drawing.Size(583, 342)
		Me.grd_MV.TabIndex = 0
		Me.grd_MV.UseEmbeddedNavigator = True
		Me.grd_MV.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_MV})
		'
		'BS_MV
		'
		Me.BS_MV.DataMember = "tbl_Emu_Games"
		Me.BS_MV.DataSource = Me.DS_ML
		Me.BS_MV.Filter = "id_Emu_Games = 0"
		'
		'gv_MV
		'
		Me.gv_MV.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.colVolume_Number})
		Me.gv_MV.GridControl = Me.grd_MV
		Me.gv_MV.GroupCount = 1
		Me.gv_MV.Name = "gv_MV"
		Me.gv_MV.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_MV.OptionsSelection.MultiSelect = True
		Me.gv_MV.OptionsView.ColumnAutoWidth = False
		Me.gv_MV.OptionsView.ShowGroupPanel = False
		Me.gv_MV.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)})
		'
		'GridColumn1
		'
		Me.GridColumn1.Caption = "Directory"
		Me.GridColumn1.FieldName = "Folder"
		Me.GridColumn1.Name = "GridColumn1"
		Me.GridColumn1.OptionsColumn.AllowEdit = False
		Me.GridColumn1.OptionsColumn.ReadOnly = True
		Me.GridColumn1.Visible = True
		Me.GridColumn1.VisibleIndex = 0
		'
		'GridColumn2
		'
		Me.GridColumn2.Caption = "Filename"
		Me.GridColumn2.FieldName = "File"
		Me.GridColumn2.Name = "GridColumn2"
		Me.GridColumn2.OptionsColumn.AllowEdit = False
		Me.GridColumn2.OptionsColumn.ReadOnly = True
		Me.GridColumn2.Visible = True
		Me.GridColumn2.VisibleIndex = 0
		Me.GridColumn2.Width = 140
		'
		'GridColumn3
		'
		Me.GridColumn3.Caption = "Inner File"
		Me.GridColumn3.FieldName = "InnerFile"
		Me.GridColumn3.Name = "GridColumn3"
		Me.GridColumn3.OptionsColumn.AllowEdit = False
		Me.GridColumn3.OptionsColumn.ReadOnly = True
		Me.GridColumn3.Visible = True
		Me.GridColumn3.VisibleIndex = 1
		Me.GridColumn3.Width = 175
		'
		'GridColumn4
		'
		Me.GridColumn4.Caption = "Game"
		Me.GridColumn4.ColumnEdit = Me.RepositoryItemLookUpEdit1
		Me.GridColumn4.FieldName = "Moby_Games_URLPart"
		Me.GridColumn4.Name = "GridColumn4"
		Me.GridColumn4.OptionsColumn.AllowEdit = False
		Me.GridColumn4.OptionsColumn.ReadOnly = True
		Me.GridColumn4.Width = 102
		'
		'RepositoryItemLookUpEdit1
		'
		Me.RepositoryItemLookUpEdit1.AutoHeight = False
		Me.RepositoryItemLookUpEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.RepositoryItemLookUpEdit1.DataSource = Me.BS_Moby_Releases
		Me.RepositoryItemLookUpEdit1.DisplayMember = "Gamename"
		Me.RepositoryItemLookUpEdit1.Name = "RepositoryItemLookUpEdit1"
		Me.RepositoryItemLookUpEdit1.NullText = ""
		Me.RepositoryItemLookUpEdit1.ValueMember = "Moby_Games_URLPart"
		'
		'GridColumn5
		'
		Me.GridColumn5.ColumnEdit = Me.RepositoryItemCheckEdit1
		Me.GridColumn5.FieldName = "Hidden"
		Me.GridColumn5.Name = "GridColumn5"
		Me.GridColumn5.Width = 50
		'
		'RepositoryItemCheckEdit1
		'
		Me.RepositoryItemCheckEdit1.AutoHeight = False
		Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
		Me.RepositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
		'
		'GridColumn6
		'
		Me.GridColumn6.Caption = "Added"
		Me.GridColumn6.DisplayFormat.FormatString = "g"
		Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
		Me.GridColumn6.FieldName = "created"
		Me.GridColumn6.Name = "GridColumn6"
		Me.GridColumn6.OptionsColumn.AllowEdit = False
		Me.GridColumn6.OptionsColumn.ReadOnly = True
		Me.GridColumn6.Visible = True
		Me.GridColumn6.VisibleIndex = 3
		Me.GridColumn6.Width = 94
		'
		'colVolume_Number
		'
		Me.colVolume_Number.Caption = "Volume Number"
		Me.colVolume_Number.ColumnEdit = Me.rpi_Volume_Number
		Me.colVolume_Number.FieldName = "Volume_Number"
		Me.colVolume_Number.Name = "colVolume_Number"
		Me.colVolume_Number.Visible = True
		Me.colVolume_Number.VisibleIndex = 2
		Me.colVolume_Number.Width = 118
		'
		'rpi_Volume_Number
		'
		Me.rpi_Volume_Number.AutoHeight = False
		Me.rpi_Volume_Number.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
		Me.rpi_Volume_Number.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_Tag_Parser_Volumes", "id_Tag_Parser_Volumes", 137, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayText", "Display Text", 69, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near)})
		Me.rpi_Volume_Number.DataSource = Me.BS_MV_Volume
		Me.rpi_Volume_Number.DisplayMember = "DisplayText"
		Me.rpi_Volume_Number.Name = "rpi_Volume_Number"
		Me.rpi_Volume_Number.NullText = "Not a volume"
		Me.rpi_Volume_Number.ShowHeader = False
		Me.rpi_Volume_Number.ValueMember = "id_Tag_Parser_Volumes"
		'
		'BS_MV_Volume
		'
		Me.BS_MV_Volume.DataMember = "ttb_Tag_Parser_Volumes"
		Me.BS_MV_Volume.DataSource = Me.DS_ML
		'
		'lbl_Volumes
		'
		Me.lbl_Volumes.Appearance.Font = New System.Drawing.Font("Segoe UI", 16.0!)
		Me.lbl_Volumes.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Volumes.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_Volumes.Location = New System.Drawing.Point(0, 0)
		Me.lbl_Volumes.MKBoundControl1 = Nothing
		Me.lbl_Volumes.MKBoundControl2 = Nothing
		Me.lbl_Volumes.MKBoundControl3 = Nothing
		Me.lbl_Volumes.MKBoundControl4 = Nothing
		Me.lbl_Volumes.MKBoundControl5 = Nothing
		Me.lbl_Volumes.Name = "lbl_Volumes"
		Me.lbl_Volumes.Size = New System.Drawing.Size(583, 42)
		Me.lbl_Volumes.TabIndex = 3
		Me.lbl_Volumes.Text = "Discs/Volumes"
		'
		'tpg_DOSBox_Files_Directories
		'
		Me.tpg_DOSBox_Files_Directories.Controls.Add(Me.pnl_DOSBox_Files_and_Folders)
		Me.tpg_DOSBox_Files_Directories.Name = "tpg_DOSBox_Files_Directories"
		Me.tpg_DOSBox_Files_Directories.Size = New System.Drawing.Size(583, 384)
		Me.tpg_DOSBox_Files_Directories.Text = "DOSBox Files and Directories"
		'
		'pnl_DOSBox_Files_and_Folders
		'
		Me.pnl_DOSBox_Files_and_Folders.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_DOSBox_Files_and_Folders.Controls.Add(Me.Ctl_MKDXSplitPanel1)
		Me.pnl_DOSBox_Files_and_Folders.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_DOSBox_Files_and_Folders.Location = New System.Drawing.Point(0, 0)
		Me.pnl_DOSBox_Files_and_Folders.Name = "pnl_DOSBox_Files_and_Folders"
		Me.pnl_DOSBox_Files_and_Folders.Size = New System.Drawing.Size(583, 384)
		Me.pnl_DOSBox_Files_and_Folders.TabIndex = 6
		'
		'Ctl_MKDXSplitPanel1
		'
		Me.Ctl_MKDXSplitPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Ctl_MKDXSplitPanel1.Location = New System.Drawing.Point(0, 0)
		Me.Ctl_MKDXSplitPanel1.Name = "Ctl_MKDXSplitPanel1"
		Me.Ctl_MKDXSplitPanel1.Panel1.Controls.Add(Me.grd_DOSBox_Files_and_Folders)
		Me.Ctl_MKDXSplitPanel1.Panel1.Controls.Add(Me.lbl_DOSBox_Files_and_Folders)
		Me.Ctl_MKDXSplitPanel1.Panel1.Text = "Panel1"
		Me.Ctl_MKDXSplitPanel1.Panel2.Controls.Add(Me.txb_DOSBox_Inner_File)
		Me.Ctl_MKDXSplitPanel1.Panel2.Controls.Add(Me.txb_DOSBox_File)
		Me.Ctl_MKDXSplitPanel1.Panel2.Controls.Add(Me.txb_DOSBox_Folder)
		Me.Ctl_MKDXSplitPanel1.Panel2.Controls.Add(Me.cmb_DOSBox_Volume_Number)
		Me.Ctl_MKDXSplitPanel1.Panel2.Controls.Add(Me.cmb_DOSBox_Mount_Destination)
		Me.Ctl_MKDXSplitPanel1.Panel2.Controls.Add(Me.cmb_DOSBox_Exe_Type)
		Me.Ctl_MKDXSplitPanel1.Panel2.Controls.Add(Me.cmb_DOSBox_Type)
		Me.Ctl_MKDXSplitPanel1.Panel2.Controls.Add(Me.lbl_DOSBox_Volume_Number)
		Me.Ctl_MKDXSplitPanel1.Panel2.Controls.Add(Me.lbl_DOSBox_Mount_Destination)
		Me.Ctl_MKDXSplitPanel1.Panel2.Controls.Add(Me.lbl_DOSBox_InnerFile)
		Me.Ctl_MKDXSplitPanel1.Panel2.Controls.Add(Me.lbl_DOSBox_File)
		Me.Ctl_MKDXSplitPanel1.Panel2.Controls.Add(Me.lbl_DOSBox_Exe_Type)
		Me.Ctl_MKDXSplitPanel1.Panel2.Controls.Add(Me.lbl_DOSBox_Folder)
		Me.Ctl_MKDXSplitPanel1.Panel2.Controls.Add(Me.lbl_DOSBox_Type)
		Me.Ctl_MKDXSplitPanel1.Panel2.Controls.Add(Me.lbl_DOSBox_Folder_and_Files_Settings)
		Me.Ctl_MKDXSplitPanel1.Panel2.Text = "Panel2"
		Me.Ctl_MKDXSplitPanel1.Size = New System.Drawing.Size(583, 384)
		Me.Ctl_MKDXSplitPanel1.SplitterPosition = 281
		Me.Ctl_MKDXSplitPanel1.TabIndex = 6
		Me.Ctl_MKDXSplitPanel1.Text = "Ctl_MKDXSplitPanel1"
		'
		'grd_DOSBox_Files_and_Folders
		'
		Me.grd_DOSBox_Files_and_Folders.DataSource = Me.BS_DOSBox_Files_and_Folders
		Me.grd_DOSBox_Files_and_Folders.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_DOSBox_Files_and_Folders.EmbeddedNavigator.Buttons.Append.Visible = False
		Me.grd_DOSBox_Files_and_Folders.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
		Me.grd_DOSBox_Files_and_Folders.EmbeddedNavigator.Buttons.Edit.Visible = False
		Me.grd_DOSBox_Files_and_Folders.EmbeddedNavigator.Buttons.EndEdit.Visible = False
		Me.grd_DOSBox_Files_and_Folders.EmbeddedNavigator.Buttons.Remove.Visible = False
		Me.grd_DOSBox_Files_and_Folders.EmbeddedNavigator.TextStringFormat = "{0} of {1}"
		Me.grd_DOSBox_Files_and_Folders.Location = New System.Drawing.Point(0, 42)
		Me.grd_DOSBox_Files_and_Folders.MainView = Me.gv_DOSBox_Files_and_Folders
		Me.grd_DOSBox_Files_and_Folders.Name = "grd_DOSBox_Files_and_Folders"
		Me.grd_DOSBox_Files_and_Folders.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rpi_DOSBox_Volume})
		Me.grd_DOSBox_Files_and_Folders.Size = New System.Drawing.Size(281, 342)
		Me.grd_DOSBox_Files_and_Folders.TabIndex = 0
		Me.grd_DOSBox_Files_and_Folders.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_DOSBox_Files_and_Folders})
		'
		'BS_DOSBox_Files_and_Folders
		'
		Me.BS_DOSBox_Files_and_Folders.DataMember = "tbl_Emu_Games"
		Me.BS_DOSBox_Files_and_Folders.DataSource = Me.DS_ML
		Me.BS_DOSBox_Files_and_Folders.Filter = "id_Emu_Games = 0"
		'
		'gv_DOSBox_Files_and_Folders
		'
		Me.gv_DOSBox_Files_and_Folders.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_DOSBox_Displayname, Me.colid_Rombase_DOSBox_Filetypes, Me.colDOSBox_Mount_Destination, Me.colVolume_Number1, Me.GridColumn7, Me.GridColumn8, Me.colInnerFile1})
		Me.gv_DOSBox_Files_and_Folders.GridControl = Me.grd_DOSBox_Files_and_Folders
		Me.gv_DOSBox_Files_and_Folders.Name = "gv_DOSBox_Files_and_Folders"
		Me.gv_DOSBox_Files_and_Folders.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_DOSBox_Files_and_Folders.OptionsSelection.MultiSelect = True
		Me.gv_DOSBox_Files_and_Folders.OptionsView.ColumnAutoWidth = False
		Me.gv_DOSBox_Files_and_Folders.OptionsView.ShowGroupPanel = False
		Me.gv_DOSBox_Files_and_Folders.OptionsView.ShowIndicator = False
		'
		'col_DOSBox_Displayname
		'
		Me.col_DOSBox_Displayname.Caption = "File/Directory"
		Me.col_DOSBox_Displayname.Name = "col_DOSBox_Displayname"
		Me.col_DOSBox_Displayname.OptionsColumn.AllowEdit = False
		Me.col_DOSBox_Displayname.Visible = True
		Me.col_DOSBox_Displayname.VisibleIndex = 0
		Me.col_DOSBox_Displayname.Width = 163
		'
		'colid_Rombase_DOSBox_Filetypes
		'
		Me.colid_Rombase_DOSBox_Filetypes.Caption = "Type"
		Me.colid_Rombase_DOSBox_Filetypes.FieldName = "id_Rombase_DOSBox_Filetypes"
		Me.colid_Rombase_DOSBox_Filetypes.Name = "colid_Rombase_DOSBox_Filetypes"
		Me.colid_Rombase_DOSBox_Filetypes.OptionsColumn.AllowEdit = False
		Me.colid_Rombase_DOSBox_Filetypes.Visible = True
		Me.colid_Rombase_DOSBox_Filetypes.VisibleIndex = 1
		Me.colid_Rombase_DOSBox_Filetypes.Width = 85
		'
		'colDOSBox_Mount_Destination
		'
		Me.colDOSBox_Mount_Destination.Caption = "Mount"
		Me.colDOSBox_Mount_Destination.FieldName = "DOSBox_Mount_Destination"
		Me.colDOSBox_Mount_Destination.Name = "colDOSBox_Mount_Destination"
		Me.colDOSBox_Mount_Destination.OptionsColumn.AllowEdit = False
		Me.colDOSBox_Mount_Destination.Visible = True
		Me.colDOSBox_Mount_Destination.VisibleIndex = 2
		Me.colDOSBox_Mount_Destination.Width = 58
		'
		'colVolume_Number1
		'
		Me.colVolume_Number1.Caption = "Volume"
		Me.colVolume_Number1.ColumnEdit = Me.rpi_DOSBox_Volume
		Me.colVolume_Number1.FieldName = "Volume_Number"
		Me.colVolume_Number1.Name = "colVolume_Number1"
		Me.colVolume_Number1.OptionsColumn.AllowEdit = False
		Me.colVolume_Number1.Visible = True
		Me.colVolume_Number1.VisibleIndex = 3
		Me.colVolume_Number1.Width = 78
		'
		'rpi_DOSBox_Volume
		'
		Me.rpi_DOSBox_Volume.AutoHeight = False
		Me.rpi_DOSBox_Volume.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
		Me.rpi_DOSBox_Volume.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_Tag_Parser_Volumes", "id_Tag_Parser_Volumes", 137, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayText", "Display Text", 69, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near)})
		Me.rpi_DOSBox_Volume.DataSource = Me.BS_MV_Volume
		Me.rpi_DOSBox_Volume.DisplayMember = "DisplayText"
		Me.rpi_DOSBox_Volume.Name = "rpi_DOSBox_Volume"
		Me.rpi_DOSBox_Volume.NullText = "Not a volume"
		Me.rpi_DOSBox_Volume.ShowHeader = False
		Me.rpi_DOSBox_Volume.ValueMember = "id_Tag_Parser_Volumes"
		'
		'GridColumn7
		'
		Me.GridColumn7.Caption = "Directory"
		Me.GridColumn7.FieldName = "Folder"
		Me.GridColumn7.Name = "GridColumn7"
		Me.GridColumn7.OptionsColumn.AllowEdit = False
		Me.GridColumn7.Width = 144
		'
		'GridColumn8
		'
		Me.GridColumn8.Caption = "Filename"
		Me.GridColumn8.FieldName = "File"
		Me.GridColumn8.Name = "GridColumn8"
		Me.GridColumn8.OptionsColumn.AllowEdit = False
		Me.GridColumn8.Width = 152
		'
		'colInnerFile1
		'
		Me.colInnerFile1.Caption = "Inner File"
		Me.colInnerFile1.FieldName = "InnerFile"
		Me.colInnerFile1.Name = "colInnerFile1"
		Me.colInnerFile1.OptionsColumn.AllowEdit = False
		Me.colInnerFile1.Width = 172
		'
		'lbl_DOSBox_Files_and_Folders
		'
		Me.lbl_DOSBox_Files_and_Folders.Appearance.Font = New System.Drawing.Font("Segoe UI", 16.0!)
		Me.lbl_DOSBox_Files_and_Folders.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_DOSBox_Files_and_Folders.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_DOSBox_Files_and_Folders.Location = New System.Drawing.Point(0, 0)
		Me.lbl_DOSBox_Files_and_Folders.MKBoundControl1 = Nothing
		Me.lbl_DOSBox_Files_and_Folders.MKBoundControl2 = Nothing
		Me.lbl_DOSBox_Files_and_Folders.MKBoundControl3 = Nothing
		Me.lbl_DOSBox_Files_and_Folders.MKBoundControl4 = Nothing
		Me.lbl_DOSBox_Files_and_Folders.MKBoundControl5 = Nothing
		Me.lbl_DOSBox_Files_and_Folders.Name = "lbl_DOSBox_Files_and_Folders"
		Me.lbl_DOSBox_Files_and_Folders.Size = New System.Drawing.Size(281, 42)
		Me.lbl_DOSBox_Files_and_Folders.TabIndex = 7
		Me.lbl_DOSBox_Files_and_Folders.Text = "Files and Directories"
		'
		'txb_DOSBox_Inner_File
		'
		Me.txb_DOSBox_Inner_File.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_DOSBox_Inner_File.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_DOSBox_Files_and_Folders, "InnerFile", True))
		Me.txb_DOSBox_Inner_File.Location = New System.Drawing.Point(94, 88)
		Me.txb_DOSBox_Inner_File.MKBoundLabel = Nothing
		Me.txb_DOSBox_Inner_File.MKEditValue_Compare = Nothing
		Me.txb_DOSBox_Inner_File.Name = "txb_DOSBox_Inner_File"
		Me.txb_DOSBox_Inner_File.Properties.ReadOnly = True
		Me.txb_DOSBox_Inner_File.Size = New System.Drawing.Size(200, 20)
		ToolTipTitleItem1.Text = "Inner File"
		ToolTipItem1.LeftIndent = 6
		ToolTipItem1.Text = "If the File is packed (e.g. zip or rar file), the inner file specifies the file w" &
		"ithing the packed file."
		SuperToolTip1.Items.Add(ToolTipTitleItem1)
		SuperToolTip1.Items.Add(ToolTipItem1)
		Me.txb_DOSBox_Inner_File.SuperTip = SuperToolTip1
		Me.txb_DOSBox_Inner_File.TabIndex = 2
		'
		'txb_DOSBox_File
		'
		Me.txb_DOSBox_File.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_DOSBox_File.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_DOSBox_Files_and_Folders, "File", True))
		Me.txb_DOSBox_File.Location = New System.Drawing.Point(94, 65)
		Me.txb_DOSBox_File.MKBoundLabel = Nothing
		Me.txb_DOSBox_File.MKEditValue_Compare = Nothing
		Me.txb_DOSBox_File.Name = "txb_DOSBox_File"
		Me.txb_DOSBox_File.Properties.ReadOnly = True
		Me.txb_DOSBox_File.Size = New System.Drawing.Size(200, 20)
		Me.txb_DOSBox_File.TabIndex = 1
		'
		'txb_DOSBox_Folder
		'
		Me.txb_DOSBox_Folder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_DOSBox_Folder.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BS_DOSBox_Files_and_Folders, "Folder", True))
		Me.txb_DOSBox_Folder.Location = New System.Drawing.Point(94, 42)
		Me.txb_DOSBox_Folder.MKBoundLabel = Nothing
		Me.txb_DOSBox_Folder.MKEditValue_Compare = Nothing
		Me.txb_DOSBox_Folder.Name = "txb_DOSBox_Folder"
		Me.txb_DOSBox_Folder.Properties.ReadOnly = True
		Me.txb_DOSBox_Folder.Size = New System.Drawing.Size(200, 20)
		Me.txb_DOSBox_Folder.TabIndex = 0
		'
		'cmb_DOSBox_Volume_Number
		'
		Me.cmb_DOSBox_Volume_Number.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_DOSBox_Files_and_Folders, "Volume_Number", True))
		Me.cmb_DOSBox_Volume_Number.Location = New System.Drawing.Point(94, 157)
		Me.cmb_DOSBox_Volume_Number.MKBoundLabel = Nothing
		Me.cmb_DOSBox_Volume_Number.MKEditValue_Compare = Nothing
		Me.cmb_DOSBox_Volume_Number.Name = "cmb_DOSBox_Volume_Number"
		Me.cmb_DOSBox_Volume_Number.Properties.AllowFocused = False
		Me.cmb_DOSBox_Volume_Number.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
		Me.cmb_DOSBox_Volume_Number.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_Tag_Parser_Volumes", "id_Tag_Parser_Volumes", 5, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayText", "Display Text", 5, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near)})
		Me.cmb_DOSBox_Volume_Number.Properties.DataSource = Me.BS_MV_Volume
		Me.cmb_DOSBox_Volume_Number.Properties.DisplayMember = "DisplayText"
		Me.cmb_DOSBox_Volume_Number.Properties.NullText = ""
		Me.cmb_DOSBox_Volume_Number.Properties.ShowFooter = False
		Me.cmb_DOSBox_Volume_Number.Properties.ShowHeader = False
		Me.cmb_DOSBox_Volume_Number.Properties.ValueMember = "id_Tag_Parser_Volumes"
		Me.cmb_DOSBox_Volume_Number.Size = New System.Drawing.Size(143, 20)
		Me.cmb_DOSBox_Volume_Number.TabIndex = 5
		'
		'cmb_DOSBox_Mount_Destination
		'
		Me.cmb_DOSBox_Mount_Destination.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_DOSBox_Files_and_Folders, "DOSBox_Mount_Destination", True))
		Me.cmb_DOSBox_Mount_Destination.Location = New System.Drawing.Point(94, 134)
		Me.cmb_DOSBox_Mount_Destination.MKBoundLabel = Nothing
		Me.cmb_DOSBox_Mount_Destination.MKEditValue_Compare = Nothing
		Me.cmb_DOSBox_Mount_Destination.Name = "cmb_DOSBox_Mount_Destination"
		Me.cmb_DOSBox_Mount_Destination.Properties.AllowFocused = False
		Me.cmb_DOSBox_Mount_Destination.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
		Me.cmb_DOSBox_Mount_Destination.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Value", "Value", 5, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Displayname", "Displayname", 5, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near)})
		Me.cmb_DOSBox_Mount_Destination.Properties.DataSource = Me.BTA_DOSBox_Mount_Destination
		Me.cmb_DOSBox_Mount_Destination.Properties.DisplayMember = "Displayname"
		Me.cmb_DOSBox_Mount_Destination.Properties.NullText = ""
		Me.cmb_DOSBox_Mount_Destination.Properties.ShowFooter = False
		Me.cmb_DOSBox_Mount_Destination.Properties.ShowHeader = False
		Me.cmb_DOSBox_Mount_Destination.Properties.ValueMember = "Value"
		Me.cmb_DOSBox_Mount_Destination.Size = New System.Drawing.Size(143, 20)
		ToolTipTitleItem2.Text = "Mount as"
		ToolTipItem2.LeftIndent = 6
		ToolTipItem2.Text = "The drive letter within DOSBox to which the file's content gets mounted"
		SuperToolTip2.Items.Add(ToolTipTitleItem2)
		SuperToolTip2.Items.Add(ToolTipItem2)
		Me.cmb_DOSBox_Mount_Destination.SuperTip = SuperToolTip2
		Me.cmb_DOSBox_Mount_Destination.TabIndex = 4
		'
		'BTA_DOSBox_Mount_Destination
		'
		Me.BTA_DOSBox_Mount_Destination.AllowDelete = True
		Me.BTA_DOSBox_Mount_Destination.ColumnUpdateBlacklistStream = CType(resources.GetObject("BTA_DOSBox_Mount_Destination.ColumnUpdateBlacklistStream"), System.Collections.Generic.List(Of String))
		Me.BTA_DOSBox_Mount_Destination.Connection = Nothing
		Me.BTA_DOSBox_Mount_Destination.DSStream = CType(resources.GetObject("BTA_DOSBox_Mount_Destination.DSStream"), System.IO.MemoryStream)
		Me.BTA_DOSBox_Mount_Destination.FillString = ""
		Me.BTA_DOSBox_Mount_Destination.Transaction = Nothing
		Me.BTA_DOSBox_Mount_Destination.UpdateTablesStream = CType(resources.GetObject("BTA_DOSBox_Mount_Destination.UpdateTablesStream"), System.Collections.Generic.List(Of String))
		'
		'cmb_DOSBox_Exe_Type
		'
		Me.cmb_DOSBox_Exe_Type.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_DOSBox_Files_and_Folders, "id_Rombase_DOSBox_Exe_Types", True))
		Me.cmb_DOSBox_Exe_Type.Location = New System.Drawing.Point(94, 134)
		Me.cmb_DOSBox_Exe_Type.MKBoundLabel = Nothing
		Me.cmb_DOSBox_Exe_Type.MKEditValue_Compare = Nothing
		Me.cmb_DOSBox_Exe_Type.Name = "cmb_DOSBox_Exe_Type"
		Me.cmb_DOSBox_Exe_Type.Properties.AllowFocused = False
		Me.cmb_DOSBox_Exe_Type.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
		Me.cmb_DOSBox_Exe_Type.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_Rombase_DOSBox_Exe_Types", "id_Rombase_DOS Box_Exe_Types", 5, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Displayname", "Displayname", 5, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 5, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Near)})
		Me.cmb_DOSBox_Exe_Type.Properties.DataSource = Me.BTA_DOSBox_Exe_Types
		Me.cmb_DOSBox_Exe_Type.Properties.DisplayMember = "Displayname"
		Me.cmb_DOSBox_Exe_Type.Properties.NullText = ""
		Me.cmb_DOSBox_Exe_Type.Properties.ShowFooter = False
		Me.cmb_DOSBox_Exe_Type.Properties.ShowHeader = False
		Me.cmb_DOSBox_Exe_Type.Properties.ValueMember = "id_Rombase_DOSBox_Exe_Types"
		Me.cmb_DOSBox_Exe_Type.Size = New System.Drawing.Size(143, 20)
		Me.cmb_DOSBox_Exe_Type.TabIndex = 22
		'
		'BTA_DOSBox_Exe_Types
		'
		Me.BTA_DOSBox_Exe_Types.AllowDelete = True
		Me.BTA_DOSBox_Exe_Types.ColumnUpdateBlacklistStream = CType(resources.GetObject("BTA_DOSBox_Exe_Types.ColumnUpdateBlacklistStream"), System.Collections.Generic.List(Of String))
		Me.BTA_DOSBox_Exe_Types.Connection = Nothing
		Me.BTA_DOSBox_Exe_Types.DSStream = CType(resources.GetObject("BTA_DOSBox_Exe_Types.DSStream"), System.IO.MemoryStream)
		Me.BTA_DOSBox_Exe_Types.FillString = ""
		Me.BTA_DOSBox_Exe_Types.Transaction = Nothing
		Me.BTA_DOSBox_Exe_Types.UpdateTablesStream = CType(resources.GetObject("BTA_DOSBox_Exe_Types.UpdateTablesStream"), System.Collections.Generic.List(Of String))
		'
		'cmb_DOSBox_Type
		'
		Me.cmb_DOSBox_Type.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_DOSBox_Files_and_Folders, "id_Rombase_DOSBox_Filetypes", True))
		Me.cmb_DOSBox_Type.Location = New System.Drawing.Point(94, 111)
		Me.cmb_DOSBox_Type.MKBoundLabel = Nothing
		Me.cmb_DOSBox_Type.MKEditValue_Compare = Nothing
		Me.cmb_DOSBox_Type.Name = "cmb_DOSBox_Type"
		Me.cmb_DOSBox_Type.Properties.AllowFocused = False
		Me.cmb_DOSBox_Type.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.cmb_DOSBox_Type.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_Rombase_DOSBox_Filetypes", "id_Rombase_DOS Box_Filetypes", 5, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Displayname", "Displayname", 5, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 5, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Near)})
		Me.cmb_DOSBox_Type.Properties.DataSource = Me.BTA_DOSBox_Filetypes
		Me.cmb_DOSBox_Type.Properties.DisplayMember = "Displayname"
		Me.cmb_DOSBox_Type.Properties.NullText = ""
		Me.cmb_DOSBox_Type.Properties.ShowFooter = False
		Me.cmb_DOSBox_Type.Properties.ShowHeader = False
		Me.cmb_DOSBox_Type.Properties.ValueMember = "id_Rombase_DOSBox_Filetypes"
		Me.cmb_DOSBox_Type.Size = New System.Drawing.Size(143, 20)
		Me.cmb_DOSBox_Type.TabIndex = 3
		'
		'BTA_DOSBox_Filetypes
		'
		Me.BTA_DOSBox_Filetypes.AllowDelete = True
		Me.BTA_DOSBox_Filetypes.ColumnUpdateBlacklistStream = CType(resources.GetObject("BTA_DOSBox_Filetypes.ColumnUpdateBlacklistStream"), System.Collections.Generic.List(Of String))
		Me.BTA_DOSBox_Filetypes.Connection = Nothing
		Me.BTA_DOSBox_Filetypes.DSStream = CType(resources.GetObject("BTA_DOSBox_Filetypes.DSStream"), System.IO.MemoryStream)
		Me.BTA_DOSBox_Filetypes.FillString = ""
		Me.BTA_DOSBox_Filetypes.Filter = "ID <> 'int'"
		Me.BTA_DOSBox_Filetypes.Transaction = Nothing
		Me.BTA_DOSBox_Filetypes.UpdateTablesStream = CType(resources.GetObject("BTA_DOSBox_Filetypes.UpdateTablesStream"), System.Collections.Generic.List(Of String))
		'
		'lbl_DOSBox_Volume_Number
		'
		Me.lbl_DOSBox_Volume_Number.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_DOSBox_Volume_Number.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_DOSBox_Volume_Number.Location = New System.Drawing.Point(1, 157)
		Me.lbl_DOSBox_Volume_Number.MKBoundControl1 = Nothing
		Me.lbl_DOSBox_Volume_Number.MKBoundControl2 = Nothing
		Me.lbl_DOSBox_Volume_Number.MKBoundControl3 = Nothing
		Me.lbl_DOSBox_Volume_Number.MKBoundControl4 = Nothing
		Me.lbl_DOSBox_Volume_Number.MKBoundControl5 = Nothing
		Me.lbl_DOSBox_Volume_Number.Name = "lbl_DOSBox_Volume_Number"
		Me.lbl_DOSBox_Volume_Number.Size = New System.Drawing.Size(90, 20)
		Me.lbl_DOSBox_Volume_Number.TabIndex = 9
		Me.lbl_DOSBox_Volume_Number.Text = "Volume N°:"
		'
		'lbl_DOSBox_Mount_Destination
		'
		Me.lbl_DOSBox_Mount_Destination.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_DOSBox_Mount_Destination.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_DOSBox_Mount_Destination.Location = New System.Drawing.Point(1, 134)
		Me.lbl_DOSBox_Mount_Destination.MKBoundControl1 = Nothing
		Me.lbl_DOSBox_Mount_Destination.MKBoundControl2 = Nothing
		Me.lbl_DOSBox_Mount_Destination.MKBoundControl3 = Nothing
		Me.lbl_DOSBox_Mount_Destination.MKBoundControl4 = Nothing
		Me.lbl_DOSBox_Mount_Destination.MKBoundControl5 = Nothing
		Me.lbl_DOSBox_Mount_Destination.Name = "lbl_DOSBox_Mount_Destination"
		Me.lbl_DOSBox_Mount_Destination.Size = New System.Drawing.Size(90, 20)
		Me.lbl_DOSBox_Mount_Destination.TabIndex = 9
		Me.lbl_DOSBox_Mount_Destination.Text = "Mount as:"
		'
		'lbl_DOSBox_InnerFile
		'
		Me.lbl_DOSBox_InnerFile.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_DOSBox_InnerFile.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_DOSBox_InnerFile.Location = New System.Drawing.Point(1, 88)
		Me.lbl_DOSBox_InnerFile.MKBoundControl1 = Nothing
		Me.lbl_DOSBox_InnerFile.MKBoundControl2 = Nothing
		Me.lbl_DOSBox_InnerFile.MKBoundControl3 = Nothing
		Me.lbl_DOSBox_InnerFile.MKBoundControl4 = Nothing
		Me.lbl_DOSBox_InnerFile.MKBoundControl5 = Nothing
		Me.lbl_DOSBox_InnerFile.Name = "lbl_DOSBox_InnerFile"
		Me.lbl_DOSBox_InnerFile.Size = New System.Drawing.Size(90, 20)
		Me.lbl_DOSBox_InnerFile.TabIndex = 9
		Me.lbl_DOSBox_InnerFile.Text = "Inner File:"
		'
		'lbl_DOSBox_File
		'
		Me.lbl_DOSBox_File.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_DOSBox_File.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_DOSBox_File.Location = New System.Drawing.Point(1, 65)
		Me.lbl_DOSBox_File.MKBoundControl1 = Nothing
		Me.lbl_DOSBox_File.MKBoundControl2 = Nothing
		Me.lbl_DOSBox_File.MKBoundControl3 = Nothing
		Me.lbl_DOSBox_File.MKBoundControl4 = Nothing
		Me.lbl_DOSBox_File.MKBoundControl5 = Nothing
		Me.lbl_DOSBox_File.Name = "lbl_DOSBox_File"
		Me.lbl_DOSBox_File.Size = New System.Drawing.Size(90, 20)
		Me.lbl_DOSBox_File.TabIndex = 9
		Me.lbl_DOSBox_File.Text = "File:"
		'
		'lbl_DOSBox_Exe_Type
		'
		Me.lbl_DOSBox_Exe_Type.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_DOSBox_Exe_Type.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_DOSBox_Exe_Type.Location = New System.Drawing.Point(1, 134)
		Me.lbl_DOSBox_Exe_Type.MKBoundControl1 = Nothing
		Me.lbl_DOSBox_Exe_Type.MKBoundControl2 = Nothing
		Me.lbl_DOSBox_Exe_Type.MKBoundControl3 = Nothing
		Me.lbl_DOSBox_Exe_Type.MKBoundControl4 = Nothing
		Me.lbl_DOSBox_Exe_Type.MKBoundControl5 = Nothing
		Me.lbl_DOSBox_Exe_Type.Name = "lbl_DOSBox_Exe_Type"
		Me.lbl_DOSBox_Exe_Type.Size = New System.Drawing.Size(90, 20)
		Me.lbl_DOSBox_Exe_Type.TabIndex = 9
		Me.lbl_DOSBox_Exe_Type.Text = "Executable Type:"
		'
		'lbl_DOSBox_Folder
		'
		Me.lbl_DOSBox_Folder.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_DOSBox_Folder.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_DOSBox_Folder.Location = New System.Drawing.Point(1, 42)
		Me.lbl_DOSBox_Folder.MKBoundControl1 = Nothing
		Me.lbl_DOSBox_Folder.MKBoundControl2 = Nothing
		Me.lbl_DOSBox_Folder.MKBoundControl3 = Nothing
		Me.lbl_DOSBox_Folder.MKBoundControl4 = Nothing
		Me.lbl_DOSBox_Folder.MKBoundControl5 = Nothing
		Me.lbl_DOSBox_Folder.Name = "lbl_DOSBox_Folder"
		Me.lbl_DOSBox_Folder.Size = New System.Drawing.Size(90, 20)
		Me.lbl_DOSBox_Folder.TabIndex = 9
		Me.lbl_DOSBox_Folder.Text = "Directory:"
		'
		'lbl_DOSBox_Type
		'
		Me.lbl_DOSBox_Type.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_DOSBox_Type.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_DOSBox_Type.Location = New System.Drawing.Point(1, 111)
		Me.lbl_DOSBox_Type.MKBoundControl1 = Nothing
		Me.lbl_DOSBox_Type.MKBoundControl2 = Nothing
		Me.lbl_DOSBox_Type.MKBoundControl3 = Nothing
		Me.lbl_DOSBox_Type.MKBoundControl4 = Nothing
		Me.lbl_DOSBox_Type.MKBoundControl5 = Nothing
		Me.lbl_DOSBox_Type.Name = "lbl_DOSBox_Type"
		Me.lbl_DOSBox_Type.Size = New System.Drawing.Size(90, 20)
		Me.lbl_DOSBox_Type.TabIndex = 9
		Me.lbl_DOSBox_Type.Text = "Type:"
		'
		'lbl_DOSBox_Folder_and_Files_Settings
		'
		Me.lbl_DOSBox_Folder_and_Files_Settings.Appearance.Font = New System.Drawing.Font("Segoe UI", 16.0!)
		Me.lbl_DOSBox_Folder_and_Files_Settings.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_DOSBox_Folder_and_Files_Settings.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_DOSBox_Folder_and_Files_Settings.Location = New System.Drawing.Point(0, 0)
		Me.lbl_DOSBox_Folder_and_Files_Settings.MKBoundControl1 = Nothing
		Me.lbl_DOSBox_Folder_and_Files_Settings.MKBoundControl2 = Nothing
		Me.lbl_DOSBox_Folder_and_Files_Settings.MKBoundControl3 = Nothing
		Me.lbl_DOSBox_Folder_and_Files_Settings.MKBoundControl4 = Nothing
		Me.lbl_DOSBox_Folder_and_Files_Settings.MKBoundControl5 = Nothing
		Me.lbl_DOSBox_Folder_and_Files_Settings.Name = "lbl_DOSBox_Folder_and_Files_Settings"
		Me.lbl_DOSBox_Folder_and_Files_Settings.Size = New System.Drawing.Size(297, 42)
		Me.lbl_DOSBox_Folder_and_Files_Settings.TabIndex = 8
		Me.lbl_DOSBox_Folder_and_Files_Settings.Text = "Configuration"
		'
		'DataTable1
		'
		Me.DataTable1.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn12, Me.DataColumn13, Me.DataColumn14})
		Me.DataTable1.TableName = "Table1"
		'
		'DataColumn12
		'
		Me.DataColumn12.ColumnName = "id_Rombase_DOSBox_Filetypes"
		Me.DataColumn12.DataType = GetType(Long)
		'
		'DataColumn13
		'
		Me.DataColumn13.ColumnName = "Displayname"
		'
		'DataColumn14
		'
		Me.DataColumn14.ColumnName = "ID"
		'
		'DataTable2
		'
		Me.DataTable2.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn15, Me.DataColumn16, Me.DataColumn17})
		Me.DataTable2.TableName = "Table1"
		'
		'DataColumn15
		'
		Me.DataColumn15.ColumnName = "id_Rombase_DOSBox_Exe_Types"
		Me.DataColumn15.DataType = GetType(Long)
		'
		'DataColumn16
		'
		Me.DataColumn16.ColumnName = "Displayname"
		'
		'DataColumn17
		'
		Me.DataColumn17.ColumnName = "ID"
		'
		'DataTable3
		'
		Me.DataTable3.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn18, Me.DataColumn19})
		Me.DataTable3.TableName = "Table1"
		'
		'DataColumn18
		'
		Me.DataColumn18.ColumnName = "Value"
		'
		'DataColumn19
		'
		Me.DataColumn19.ColumnName = "Displayname"
		'
		'barmng
		'
		Me.barmng.DockControls.Add(Me.barDockControlTop)
		Me.barmng.DockControls.Add(Me.barDockControlBottom)
		Me.barmng.DockControls.Add(Me.barDockControlLeft)
		Me.barmng.DockControls.Add(Me.barDockControlRight)
		Me.barmng.Form = Me
		Me.barmng.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.bbi_AddGames, Me.bbi_AddGamesFolder, Me.bbi_Add_DOSBox_Game_Directory, Me.bbi_Add_DOSBox_Game_Media, Me.bbi_Edit_Game, Me.bbi_Edit_Multiple_Games, Me.bbi_Change_Directory, Me.bbi_Rescan, Me.bbi_SetHidden, Me.bbi_UnsetHidden, Me.bbi_SetLink, Me.bbi_RemoveLink, Me.bbi_Delete_Games, Me.bbi_Merge_Select, Me.bbi_Merge_Start, Me.bbi_Export, Me.bbi_Debug_Import_XML, Me.bbi_Debug_Export_XML, Me.bbi_Debug_Group_Volumes, Me.bbi_Debug_SetModified, Me.bbi_Debug_Apply_TDC, Me.SkinBarSubItem1, Me.bbi_DOSBox_Files_and_Folders_Rename, Me.bbi_DOSBox_Files_and_Folders_Add_Archive, Me.bbi_DOSBox_Files_and_Folders_Add_Directory, Me.bbi_DOSBox_Files_and_Folders_Add_Media, Me.bbi_Moby_Games_Open_Moby_Page, Me.bbi_Moby_Games_Evaluate_Links, Me.bbi_Auto_Link})
		Me.barmng.MaxItemId = 29
		'
		'barDockControlTop
		'
		Me.barDockControlTop.CausesValidation = False
		Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
		Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
		Me.barDockControlTop.Size = New System.Drawing.Size(1008, 0)
		'
		'barDockControlBottom
		'
		Me.barDockControlBottom.CausesValidation = False
		Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.barDockControlBottom.Location = New System.Drawing.Point(0, 730)
		Me.barDockControlBottom.Size = New System.Drawing.Size(1008, 0)
		'
		'barDockControlLeft
		'
		Me.barDockControlLeft.CausesValidation = False
		Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
		Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
		Me.barDockControlLeft.Size = New System.Drawing.Size(0, 730)
		'
		'barDockControlRight
		'
		Me.barDockControlRight.CausesValidation = False
		Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
		Me.barDockControlRight.Location = New System.Drawing.Point(1008, 0)
		Me.barDockControlRight.Size = New System.Drawing.Size(0, 730)
		'
		'bbi_AddGames
		'
		Me.bbi_AddGames.Caption = "&Add Games (Files) ..."
		Me.bbi_AddGames.Id = 0
		Me.bbi_AddGames.ImageUri.Uri = "Add"
		Me.bbi_AddGames.Name = "bbi_AddGames"
		'
		'bbi_AddGamesFolder
		'
		Me.bbi_AddGamesFolder.Caption = "Add Games (Directory) ..."
		Me.bbi_AddGamesFolder.Id = 1
		Me.bbi_AddGamesFolder.ImageUri.Uri = "Add"
		Me.bbi_AddGamesFolder.Name = "bbi_AddGamesFolder"
		'
		'bbi_Add_DOSBox_Game_Directory
		'
		Me.bbi_Add_DOSBox_Game_Directory.Caption = "Add Game (Installed, Directory) ..."
		Me.bbi_Add_DOSBox_Game_Directory.Id = 2
		Me.bbi_Add_DOSBox_Game_Directory.ImageUri.Uri = "Add"
		Me.bbi_Add_DOSBox_Game_Directory.Name = "bbi_Add_DOSBox_Game_Directory"
		'
		'bbi_Add_DOSBox_Game_Media
		'
		Me.bbi_Add_DOSBox_Game_Media.Caption = "Add Game (Install Media) ..."
		Me.bbi_Add_DOSBox_Game_Media.Id = 3
		Me.bbi_Add_DOSBox_Game_Media.ImageUri.Uri = "Add"
		Me.bbi_Add_DOSBox_Game_Media.Name = "bbi_Add_DOSBox_Game_Media"
		'
		'bbi_Edit_Game
		'
		Me.bbi_Edit_Game.Caption = "&Edit Game"
		Me.bbi_Edit_Game.Id = 4
		Me.bbi_Edit_Game.ImageUri.Uri = "Edit"
		Me.bbi_Edit_Game.Name = "bbi_Edit_Game"
		'
		'bbi_Edit_Multiple_Games
		'
		Me.bbi_Edit_Multiple_Games.Caption = "E&dit Multiple Games"
		Me.bbi_Edit_Multiple_Games.Id = 5
		Me.bbi_Edit_Multiple_Games.ImageUri.Uri = "CustomizeGrid"
		Me.bbi_Edit_Multiple_Games.Name = "bbi_Edit_Multiple_Games"
		'
		'bbi_Change_Directory
		'
		Me.bbi_Change_Directory.Caption = "&Change Directory"
		Me.bbi_Change_Directory.Id = 6
		Me.bbi_Change_Directory.ImageUri.Uri = "Open"
		Me.bbi_Change_Directory.Name = "bbi_Change_Directory"
		'
		'bbi_Rescan
		'
		Me.bbi_Rescan.Caption = "bbi_Rescan"
		Me.bbi_Rescan.Id = 7
		Me.bbi_Rescan.ImageUri.Uri = "Refresh"
		Me.bbi_Rescan.Name = "bbi_Rescan"
		'
		'bbi_SetHidden
		'
		Me.bbi_SetHidden.Caption = "bbi_SetHidden"
		Me.bbi_SetHidden.Id = 8
		Me.bbi_SetHidden.ImageUri.Uri = "InFrontOfText"
		Me.bbi_SetHidden.Name = "bbi_SetHidden"
		'
		'bbi_UnsetHidden
		'
		Me.bbi_UnsetHidden.Caption = "bbi_UnsetHidden"
		Me.bbi_UnsetHidden.Id = 9
		Me.bbi_UnsetHidden.ImageUri.Uri = "Sqare"
		Me.bbi_UnsetHidden.Name = "bbi_UnsetHidden"
		'
		'bbi_SetLink
		'
		Me.bbi_SetLink.Caption = "bbi_SetLink"
		Me.bbi_SetLink.Id = 10
		Me.bbi_SetLink.ImageUri.Uri = "Replace"
		Me.bbi_SetLink.Name = "bbi_SetLink"
		'
		'bbi_RemoveLink
		'
		Me.bbi_RemoveLink.Caption = "bbi_RemoveLink"
		Me.bbi_RemoveLink.Id = 11
		Me.bbi_RemoveLink.ImageUri.Uri = "Undo"
		Me.bbi_RemoveLink.Name = "bbi_RemoveLink"
		'
		'bbi_Delete_Games
		'
		Me.bbi_Delete_Games.Caption = "bbi_Delete_Games"
		Me.bbi_Delete_Games.Id = 12
		Me.bbi_Delete_Games.ImageUri.Uri = "Delete"
		Me.bbi_Delete_Games.Name = "bbi_Delete_Games"
		'
		'bbi_Merge_Select
		'
		Me.bbi_Merge_Select.Caption = "Select %0% for merging"
		Me.bbi_Merge_Select.Id = 13
		Me.bbi_Merge_Select.ImageUri.Uri = "Apply"
		Me.bbi_Merge_Select.Name = "bbi_Merge_Select"
		Me.bbi_Merge_Select.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		'
		'bbi_Merge_Start
		'
		Me.bbi_Merge_Start.Caption = "Merge %0% into %1%"
		Me.bbi_Merge_Start.Id = 14
		Me.bbi_Merge_Start.ImageUri.Uri = "Forward"
		Me.bbi_Merge_Start.Name = "bbi_Merge_Start"
		Me.bbi_Merge_Start.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		'
		'bbi_Export
		'
		Me.bbi_Export.Caption = "&Export %0% Games"
		Me.bbi_Export.Id = 15
		Me.bbi_Export.ImageUri.Uri = "SaveAndNew"
		Me.bbi_Export.Name = "bbi_Export"
		'
		'bbi_Debug_Import_XML
		'
		Me.bbi_Debug_Import_XML.Caption = "Debug: Import XML"
		Me.bbi_Debug_Import_XML.Id = 16
		Me.bbi_Debug_Import_XML.Name = "bbi_Debug_Import_XML"
		'
		'bbi_Debug_Export_XML
		'
		Me.bbi_Debug_Export_XML.Caption = "Debug: Export XML"
		Me.bbi_Debug_Export_XML.Id = 17
		Me.bbi_Debug_Export_XML.Name = "bbi_Debug_Export_XML"
		'
		'bbi_Debug_Group_Volumes
		'
		Me.bbi_Debug_Group_Volumes.Caption = "Debug: Group Volumes"
		Me.bbi_Debug_Group_Volumes.Id = 18
		Me.bbi_Debug_Group_Volumes.Name = "bbi_Debug_Group_Volumes"
		'
		'bbi_Debug_SetModified
		'
		Me.bbi_Debug_SetModified.Caption = "Debug: Set modified flag on all rows"
		Me.bbi_Debug_SetModified.Id = 19
		Me.bbi_Debug_SetModified.Name = "bbi_Debug_SetModified"
		'
		'bbi_Debug_Apply_TDC
		'
		Me.bbi_Debug_Apply_TDC.Caption = "Debug: Apply TDC.mdb ..."
		Me.bbi_Debug_Apply_TDC.Id = 20
		Me.bbi_Debug_Apply_TDC.Name = "bbi_Debug_Apply_TDC"
		'
		'SkinBarSubItem1
		'
		Me.SkinBarSubItem1.Caption = "SkinBarSubItem1"
		Me.SkinBarSubItem1.Id = 21
		Me.SkinBarSubItem1.Name = "SkinBarSubItem1"
		'
		'bbi_DOSBox_Files_and_Folders_Rename
		'
		Me.bbi_DOSBox_Files_and_Folders_Rename.Caption = "&Rename"
		Me.bbi_DOSBox_Files_and_Folders_Rename.Id = 22
		Me.bbi_DOSBox_Files_and_Folders_Rename.ImageUri.Uri = "SpellCheckAsYouType"
		Me.bbi_DOSBox_Files_and_Folders_Rename.Name = "bbi_DOSBox_Files_and_Folders_Rename"
		'
		'bbi_DOSBox_Files_and_Folders_Add_Archive
		'
		Me.bbi_DOSBox_Files_and_Folders_Add_Archive.Caption = "Add &Archive File"
		Me.bbi_DOSBox_Files_and_Folders_Add_Archive.Id = 23
		Me.bbi_DOSBox_Files_and_Folders_Add_Archive.ImageUri.Uri = "Add"
		Me.bbi_DOSBox_Files_and_Folders_Add_Archive.Name = "bbi_DOSBox_Files_and_Folders_Add_Archive"
		'
		'bbi_DOSBox_Files_and_Folders_Add_Directory
		'
		Me.bbi_DOSBox_Files_and_Folders_Add_Directory.Caption = "Add &Directory"
		Me.bbi_DOSBox_Files_and_Folders_Add_Directory.Id = 24
		Me.bbi_DOSBox_Files_and_Folders_Add_Directory.ImageUri.Uri = "Add"
		Me.bbi_DOSBox_Files_and_Folders_Add_Directory.Name = "bbi_DOSBox_Files_and_Folders_Add_Directory"
		'
		'bbi_DOSBox_Files_and_Folders_Add_Media
		'
		Me.bbi_DOSBox_Files_and_Folders_Add_Media.Caption = "Add &Media"
		Me.bbi_DOSBox_Files_and_Folders_Add_Media.Id = 25
		Me.bbi_DOSBox_Files_and_Folders_Add_Media.ImageUri.Uri = "Add"
		Me.bbi_DOSBox_Files_and_Folders_Add_Media.Name = "bbi_DOSBox_Files_and_Folders_Add_Media"
		'
		'bbi_Moby_Games_Open_Moby_Page
		'
		Me.bbi_Moby_Games_Open_Moby_Page.Caption = "&Open Moby Page"
		Me.bbi_Moby_Games_Open_Moby_Page.Id = 26
		Me.bbi_Moby_Games_Open_Moby_Page.ImageUri.Uri = "NavigationBar"
		Me.bbi_Moby_Games_Open_Moby_Page.Name = "bbi_Moby_Games_Open_Moby_Page"
		'
		'bbi_Moby_Games_Evaluate_Links
		'
		Me.bbi_Moby_Games_Evaluate_Links.Caption = "&Evaluate MobyGames Links"
		Me.bbi_Moby_Games_Evaluate_Links.Id = 27
		Me.bbi_Moby_Games_Evaluate_Links.ImageUri.Uri = "Zoom100"
		Me.bbi_Moby_Games_Evaluate_Links.Name = "bbi_Moby_Games_Evaluate_Links"
		'
		'bbi_Auto_Link
		'
		Me.bbi_Auto_Link.Caption = "Detect MobyGames Links..."
		Me.bbi_Auto_Link.Id = 28
		Me.bbi_Auto_Link.ImageUri.Uri = "Find"
		Me.bbi_Auto_Link.Name = "bbi_Auto_Link"
		ToolTipTitleItem3.Text = "Detect MobyGames Links"
		ToolTipItem3.LeftIndent = 6
		ToolTipItem3.Text = resources.GetString("ToolTipItem3.Text")
		SuperToolTip3.Items.Add(ToolTipTitleItem3)
		SuperToolTip3.Items.Add(ToolTipItem3)
		Me.bbi_Auto_Link.SuperTip = SuperToolTip3
		'
		'popmnu_Rom_Manager
		'
		Me.popmnu_Rom_Manager.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_AddGames, True), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_AddGamesFolder), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Add_DOSBox_Game_Directory), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Add_DOSBox_Game_Media), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Edit_Game), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Edit_Multiple_Games), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Change_Directory), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Rescan, True), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_SetHidden), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_UnsetHidden), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_SetLink), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_RemoveLink), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Auto_Link), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Delete_Games, True), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Merge_Select, True), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Merge_Start), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Export, True), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Debug_Import_XML, True), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Debug_Export_XML), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Debug_Group_Volumes), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Debug_SetModified), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Debug_Apply_TDC)})
		Me.popmnu_Rom_Manager.Manager = Me.barmng
		Me.popmnu_Rom_Manager.Name = "popmnu_Rom_Manager"
		'
		'popmnu_DOSBox_Files_and_Folders
		'
		Me.popmnu_DOSBox_Files_and_Folders.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_DOSBox_Files_and_Folders_Rename), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_DOSBox_Files_and_Folders_Add_Archive, True), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_DOSBox_Files_and_Folders_Add_Directory), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_DOSBox_Files_and_Folders_Add_Media)})
		Me.popmnu_DOSBox_Files_and_Folders.Manager = Me.barmng
		Me.popmnu_DOSBox_Files_and_Folders.Name = "popmnu_DOSBox_Files_and_Folders"
		'
		'popmnu_Moby_Games
		'
		Me.popmnu_Moby_Games.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Moby_Games_Open_Moby_Page), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Moby_Games_Evaluate_Links)})
		Me.popmnu_Moby_Games.Manager = Me.barmng
		Me.popmnu_Moby_Games.Name = "popmnu_Moby_Games"
		'
		'coldeprecated
		'
		Me.coldeprecated.FieldName = "deprecated"
		Me.coldeprecated.Name = "coldeprecated"
		Me.coldeprecated.OptionsColumn.AllowEdit = False
		Me.coldeprecated.OptionsColumn.ReadOnly = True
		Me.coldeprecated.ToolTip = "indicates that the MobyGame link may be deprecated (nothing really to worry here," &
		" the meta data is still there)"
		'
		'coldeprecated1
		'
		Me.coldeprecated1.FieldName = "deprecated"
		Me.coldeprecated1.Name = "coldeprecated1"
		Me.coldeprecated1.OptionsColumn.AllowEdit = False
		Me.coldeprecated1.OptionsColumn.ReadOnly = True
		Me.coldeprecated1.ToolTip = "indicates that the MobyGame link may be deprecated (nothing really to worry here," &
		" the meta data is still there)"
		'
		'frm_Rom_Manager
		'
		Me.ClientSize = New System.Drawing.Size(1008, 730)
		Me.Controls.Add(Me.spltpnl_Right)
		Me.Controls.Add(Me.Ctl_MKDXSplitter1)
		Me.Controls.Add(Me.pnl_Left)
		Me.Controls.Add(Me.barDockControlLeft)
		Me.Controls.Add(Me.barDockControlRight)
		Me.Controls.Add(Me.barDockControlBottom)
		Me.Controls.Add(Me.barDockControlTop)
		Me.Name = "frm_Rom_Manager"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Rom Manager"
		Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
		CType(Me.rpi_Moby_Release, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Moby_Releases, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_MobyDB, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_chb_Hidden, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pnl_Right, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_Right.ResumeLayout(False)
		CType(Me.grd_Moby_Releases, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Moby_Releases, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pnl_Left, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_Left.ResumeLayout(False)
		CType(Me.cmb_Platform.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Moby_Platforms, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.grd_Emu_Games, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Emu_Games, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Emu_Games, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Moby_Platforms_gv1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_Rombase, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Rombase, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Moby_Platforms_gv2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spltpnl_Right, System.ComponentModel.ISupportInitialize).EndInit()
		Me.spltpnl_Right.ResumeLayout(False)
		CType(Me.tcl_MV, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tcl_MV.ResumeLayout(False)
		Me.tpg_Discs_Volumes.ResumeLayout(False)
		CType(Me.pnl_Discs_Volumes, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_Discs_Volumes.ResumeLayout(False)
		CType(Me.grd_MV, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_MV, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_MV, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.RepositoryItemLookUpEdit1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_Volume_Number, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_MV_Volume, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tpg_DOSBox_Files_Directories.ResumeLayout(False)
		CType(Me.pnl_DOSBox_Files_and_Folders, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_DOSBox_Files_and_Folders.ResumeLayout(False)
		CType(Me.Ctl_MKDXSplitPanel1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Ctl_MKDXSplitPanel1.ResumeLayout(False)
		CType(Me.grd_DOSBox_Files_and_Folders, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_DOSBox_Files_and_Folders, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_DOSBox_Files_and_Folders, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_DOSBox_Volume, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_DOSBox_Inner_File.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_DOSBox_File.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_DOSBox_Folder.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.cmb_DOSBox_Volume_Number.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.cmb_DOSBox_Mount_Destination.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BTA_DOSBox_Mount_Destination, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.cmb_DOSBox_Exe_Type.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BTA_DOSBox_Exe_Types, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.cmb_DOSBox_Type.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BTA_DOSBox_Filetypes, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DataTable2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DataTable3, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.barmng, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.popmnu_Rom_Manager, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.popmnu_DOSBox_Files_and_Folders, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.popmnu_Moby_Games, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents pnl_Right As MKNetDXLib.ctl_MKDXPanel
	Private WithEvents grd_Moby_Releases As MKNetDXLib.ctl_MKDXGrid
	Private WithEvents gv_Moby_Releases As DevExpress.XtraGrid.Views.Grid.GridView
	Private WithEvents colGamename As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents Ctl_MKDXSplitter1 As MKNetDXLib.ctl_MKDXSplitter
	Friend WithEvents pnl_Left As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents btn_Save As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents lbl_Platform As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents cmb_Platform As MKNetDXLib.ctl_MKDXLookupEdit
	Friend WithEvents BS_Moby_Platforms_gv1 As System.Windows.Forms.BindingSource
	Friend WithEvents BS_Moby_Platforms As System.Windows.Forms.BindingSource
	Friend WithEvents DS_Rombase As Metropolis_Launcher.DS_Rombase
	Friend WithEvents BS_Moby_Releases As System.Windows.Forms.BindingSource
	Friend WithEvents BS_Rombase As System.Windows.Forms.BindingSource
	Friend WithEvents BS_Moby_Platforms_gv2 As System.Windows.Forms.BindingSource
	Private WithEvents grd_Emu_Games As MKNetDXLib.ctl_MKDXGrid
	Private WithEvents gv_Emu_Games As DevExpress.XtraGrid.Views.Grid.GridView
	Private WithEvents colfile As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents DS_ML As Metropolis_Launcher.DS_ML
	Friend WithEvents BS_Emu_Games As System.Windows.Forms.BindingSource
	Friend WithEvents lbl_Moby_Releases As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents colFolder As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colInnerFile As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colMoby_Games_URLPart As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colHidden As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colYear As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colAdded1 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colAdded As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents spltpnl_Right As MKNetDXLib.ctl_MKDXSplitPanel
	Private WithEvents grd_MV As MKNetDXLib.ctl_MKDXGrid
	Private WithEvents gv_MV As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
	Private WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents RepositoryItemLookUpEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
	Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
	Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colVolume_Number As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents rpi_Volume_Number As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
	Friend WithEvents lbl_Volumes As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents BS_MV_Volume As System.Windows.Forms.BindingSource
	Friend WithEvents BS_MV As System.Windows.Forms.BindingSource
	Friend WithEvents pnl_DOSBox_Files_and_Folders As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents Ctl_MKDXSplitPanel1 As MKNetDXLib.ctl_MKDXSplitPanel
	Private WithEvents grd_DOSBox_Files_and_Folders As MKNetDXLib.ctl_MKDXGrid
	Private WithEvents gv_DOSBox_Files_and_Folders As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
	Private WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents rpi_DOSBox_Volume As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
	Friend WithEvents lbl_DOSBox_Files_and_Folders As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents pnl_Discs_Volumes As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents BS_DOSBox_Files_and_Folders As System.Windows.Forms.BindingSource
	Friend WithEvents lbl_DOSBox_Folder_and_Files_Settings As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents colid_Rombase_DOSBox_Filetypes As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents lbl_DOSBox_Volume_Number As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_DOSBox_Mount_Destination As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_DOSBox_File As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_DOSBox_Exe_Type As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_DOSBox_Folder As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_DOSBox_Type As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents cmb_DOSBox_Exe_Type As MKNetDXLib.ctl_MKDXLookupEdit
	Friend WithEvents cmb_DOSBox_Type As MKNetDXLib.ctl_MKDXLookupEdit
	Friend WithEvents txb_DOSBox_File As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents txb_DOSBox_Folder As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents cmb_DOSBox_Volume_Number As MKNetDXLib.ctl_MKDXLookupEdit
	Friend WithEvents cmb_DOSBox_Mount_Destination As MKNetDXLib.ctl_MKDXLookupEdit
	Friend WithEvents BTA_DOSBox_Mount_Destination As MKNetLib.cmp_MKBindableTableAdapter
	Friend WithEvents BTA_DOSBox_Exe_Types As MKNetLib.cmp_MKBindableTableAdapter
	Friend WithEvents BTA_DOSBox_Filetypes As MKNetLib.cmp_MKBindableTableAdapter
	Friend WithEvents DataTable1 As System.Data.DataTable
	Friend WithEvents DataColumn12 As System.Data.DataColumn
	Friend WithEvents DataColumn13 As System.Data.DataColumn
	Friend WithEvents DataColumn14 As System.Data.DataColumn
	Friend WithEvents DataTable2 As System.Data.DataTable
	Friend WithEvents DataColumn15 As System.Data.DataColumn
	Friend WithEvents DataColumn16 As System.Data.DataColumn
	Friend WithEvents DataColumn17 As System.Data.DataColumn
	Friend WithEvents DataTable3 As System.Data.DataTable
	Friend WithEvents DataColumn18 As System.Data.DataColumn
	Friend WithEvents DataColumn19 As System.Data.DataColumn
	Friend WithEvents col_DOSBox_Displayname As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colDOSBox_Mount_Destination As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colVolume_Number1 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colInnerFile1 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents rpi_Moby_Release As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
	Friend WithEvents rpi_chb_Hidden As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
	Friend WithEvents txb_DOSBox_Inner_File As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents lbl_DOSBox_InnerFile As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents colHighlighted As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents barmng As MKNetDXLib.ctl_MKDXBarManager
	Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
	Friend WithEvents bbi_AddGames As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_AddGamesFolder As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Add_DOSBox_Game_Directory As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Add_DOSBox_Game_Media As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Edit_Game As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Edit_Multiple_Games As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Change_Directory As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Rescan As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_SetHidden As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_UnsetHidden As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_SetLink As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_RemoveLink As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Delete_Games As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Merge_Select As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Merge_Start As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Export As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Debug_Import_XML As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Debug_Export_XML As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Debug_Group_Volumes As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Debug_SetModified As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Debug_Apply_TDC As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents SkinBarSubItem1 As DevExpress.XtraBars.SkinBarSubItem
	Friend WithEvents popmnu_Rom_Manager As MKNetDXLib.cmp_MKDXPopupMenu
	Friend WithEvents bbi_DOSBox_Files_and_Folders_Rename As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_DOSBox_Files_and_Folders_Add_Archive As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_DOSBox_Files_and_Folders_Add_Directory As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_DOSBox_Files_and_Folders_Add_Media As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents popmnu_DOSBox_Files_and_Folders As MKNetDXLib.cmp_MKDXPopupMenu
	Friend WithEvents bbi_Moby_Games_Open_Moby_Page As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Moby_Games_Evaluate_Links As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents popmnu_Moby_Games As MKNetDXLib.cmp_MKDXPopupMenu
	Friend WithEvents tcl_MV As MKNetDXLib.ctl_MKDXTabControl
	Friend WithEvents tpg_Discs_Volumes As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_DOSBox_Files_Directories As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents DS_MobyDB As DS_MobyDB
	Friend WithEvents bbi_Auto_Link As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents colDeveloper As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colPublisher As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents coldeprecated1 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents coldeprecated As DevExpress.XtraGrid.Columns.GridColumn
End Class
