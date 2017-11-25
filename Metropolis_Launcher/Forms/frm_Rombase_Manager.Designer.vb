<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ROMBase_Manager
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_ROMBase_Manager))
		Me.pnl_Left = New MKNetDXLib.ctl_MKDXPanel()
		Me.btn_Save = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.lbl_Platform = New MKNetDXLib.ctl_MKDXLabel()
		Me.cmb_Platform = New MKNetDXLib.ctl_MKDXLookupEdit()
		Me.BS_Moby_Platforms = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_MobyDB = New Metropolis_Launcher.DS_MobyDB()
		Me.grd_DAT = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_Rombase = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_Rombase = New Metropolis_Launcher.DS_Rombase()
		Me.gv_DAT = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colid_rombase = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colfilename = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colid_Moby_Platforms = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.rpi_Moby_Platforms_gv1 = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		Me.BS_Moby_Platforms_gv1 = New System.Windows.Forms.BindingSource(Me.components)
		Me.colid_Moby_Releases = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.rpi_Moby_Release = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		Me.BS_Moby_Releases = New System.Windows.Forms.BindingSource(Me.components)
		Me.colmd5 = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colsha1 = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colCustomIdentifier = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.coldeprecated = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.Ctl_MKDXSplitter1 = New MKNetDXLib.ctl_MKDXSplitter()
		Me.pnl_Right = New MKNetDXLib.ctl_MKDXPanel()
		Me.grd_Moby_Releases = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_Moby_Releases = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colGamename = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colHighlighted = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colYear = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colcreated = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colDeveloper = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colPublisher = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.coldeprecated1 = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colcompilation = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.BS_Moby_Platforms_gv2 = New System.Windows.Forms.BindingSource(Me.components)
		Me.barmng = New MKNetDXLib.ctl_MKDXBarManager()
		Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
		Me.bbi_Copy_Name_to_Clipboard = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Add_Games_from_DAT_XML = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Add_Games_from_CSV_Customidentifier_Name = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Add_Games_from_CSV_SegaCD = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Add_Games_from_CSV_Saturn = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Add_Games_from_CSV_DreamCast = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Delete = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Remove_Link = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Load_XML = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Write_XML = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Open_Moby_Page = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Evaluate_Moby_Links = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Auto_Link = New DevExpress.XtraBars.BarButtonItem()
		Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Add_Games_from_DAT = New DevExpress.XtraBars.BarButtonItem()
		Me.popmnu_Rombase = New MKNetDXLib.cmp_MKDXPopupMenu()
		Me.popmnu_Moby_Games = New MKNetDXLib.cmp_MKDXPopupMenu()
		CType(Me.pnl_Left, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_Left.SuspendLayout()
		CType(Me.cmb_Platform.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Moby_Platforms, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_MobyDB, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.grd_DAT, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Rombase, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_Rombase, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_DAT, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_Moby_Platforms_gv1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Moby_Platforms_gv1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_Moby_Release, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Moby_Releases, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.pnl_Right, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_Right.SuspendLayout()
		CType(Me.grd_Moby_Releases, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_Moby_Releases, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Moby_Platforms_gv2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.barmng, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.popmnu_Rombase, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.popmnu_Moby_Games, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'pnl_Left
		'
		Me.pnl_Left.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_Left.Controls.Add(Me.btn_Save)
		Me.pnl_Left.Controls.Add(Me.lbl_Platform)
		Me.pnl_Left.Controls.Add(Me.cmb_Platform)
		Me.pnl_Left.Controls.Add(Me.grd_DAT)
		Me.pnl_Left.Dock = System.Windows.Forms.DockStyle.Left
		Me.pnl_Left.Location = New System.Drawing.Point(0, 0)
		Me.pnl_Left.Name = "pnl_Left"
		Me.pnl_Left.Size = New System.Drawing.Size(497, 661)
		Me.pnl_Left.TabIndex = 0
		'
		'btn_Save
		'
		Me.btn_Save.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Save.Location = New System.Drawing.Point(402, 3)
		Me.btn_Save.Name = "btn_Save"
		Me.btn_Save.Size = New System.Drawing.Size(91, 20)
		Me.btn_Save.TabIndex = 1
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
		Me.cmb_Platform.Size = New System.Drawing.Size(303, 20)
		Me.cmb_Platform.TabIndex = 0
		'
		'BS_Moby_Platforms
		'
		Me.BS_Moby_Platforms.DataMember = "tbl_Moby_Platforms"
		Me.BS_Moby_Platforms.DataSource = Me.DS_MobyDB
		'
		'DS_MobyDB
		'
		Me.DS_MobyDB.DataSetName = "DS_MobyDB"
		Me.DS_MobyDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'grd_DAT
		'
		Me.grd_DAT.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.grd_DAT.DataSource = Me.BS_Rombase
		Me.grd_DAT.EmbeddedNavigator.Buttons.Append.Visible = False
		Me.grd_DAT.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
		Me.grd_DAT.EmbeddedNavigator.Buttons.Edit.Visible = False
		Me.grd_DAT.EmbeddedNavigator.Buttons.EndEdit.Visible = False
		Me.grd_DAT.EmbeddedNavigator.Buttons.Remove.Visible = False
		Me.grd_DAT.EmbeddedNavigator.TextStringFormat = "{0} of {1}"
		Me.grd_DAT.Location = New System.Drawing.Point(3, 26)
		Me.grd_DAT.MainView = Me.gv_DAT
		Me.grd_DAT.Name = "grd_DAT"
		Me.grd_DAT.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rpi_Moby_Release, Me.rpi_Moby_Platforms_gv1})
		Me.grd_DAT.Size = New System.Drawing.Size(491, 632)
		Me.grd_DAT.TabIndex = 2
		Me.grd_DAT.UseEmbeddedNavigator = True
		Me.grd_DAT.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_DAT})
		'
		'BS_Rombase
		'
		Me.BS_Rombase.DataMember = "tbl_Rombase"
		Me.BS_Rombase.DataSource = Me.DS_Rombase
		'
		'DS_Rombase
		'
		Me.DS_Rombase.DataSetName = "DS_Rombase"
		Me.DS_Rombase.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'gv_DAT
		'
		Me.gv_DAT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colid_rombase, Me.colfilename, Me.colid_Moby_Platforms, Me.colid_Moby_Releases, Me.colmd5, Me.colsha1, Me.colCustomIdentifier, Me.coldeprecated})
		Me.gv_DAT.GridControl = Me.grd_DAT
		Me.gv_DAT.Name = "gv_DAT"
		Me.gv_DAT.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_DAT.OptionsSelection.MultiSelect = True
		Me.gv_DAT.OptionsView.ColumnAutoWidth = False
		Me.gv_DAT.OptionsView.ShowGroupPanel = False
		'
		'colid_rombase
		'
		Me.colid_rombase.FieldName = "id_rombase"
		Me.colid_rombase.Name = "colid_rombase"
		Me.colid_rombase.OptionsColumn.AllowEdit = False
		Me.colid_rombase.OptionsColumn.ReadOnly = True
		'
		'colfilename
		'
		Me.colfilename.Caption = "Filename"
		Me.colfilename.FieldName = "filename"
		Me.colfilename.Name = "colfilename"
		Me.colfilename.OptionsColumn.AllowEdit = False
		Me.colfilename.OptionsColumn.ReadOnly = True
		Me.colfilename.Visible = True
		Me.colfilename.VisibleIndex = 0
		Me.colfilename.Width = 277
		'
		'colid_Moby_Platforms
		'
		Me.colid_Moby_Platforms.Caption = "Platform"
		Me.colid_Moby_Platforms.ColumnEdit = Me.rpi_Moby_Platforms_gv1
		Me.colid_Moby_Platforms.FieldName = "id_Moby_Platforms"
		Me.colid_Moby_Platforms.Name = "colid_Moby_Platforms"
		Me.colid_Moby_Platforms.OptionsColumn.AllowEdit = False
		Me.colid_Moby_Platforms.OptionsColumn.ReadOnly = True
		Me.colid_Moby_Platforms.Width = 112
		'
		'rpi_Moby_Platforms_gv1
		'
		Me.rpi_Moby_Platforms_gv1.AutoHeight = False
		Me.rpi_Moby_Platforms_gv1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.rpi_Moby_Platforms_gv1.DataSource = Me.BS_Moby_Platforms_gv1
		Me.rpi_Moby_Platforms_gv1.DisplayMember = "Display_Name"
		Me.rpi_Moby_Platforms_gv1.Name = "rpi_Moby_Platforms_gv1"
		Me.rpi_Moby_Platforms_gv1.ValueMember = "id_Moby_Platforms"
		'
		'BS_Moby_Platforms_gv1
		'
		Me.BS_Moby_Platforms_gv1.DataMember = "tbl_Moby_Platforms"
		Me.BS_Moby_Platforms_gv1.DataSource = Me.DS_MobyDB
		'
		'colid_Moby_Releases
		'
		Me.colid_Moby_Releases.Caption = "Moby Release"
		Me.colid_Moby_Releases.ColumnEdit = Me.rpi_Moby_Release
		Me.colid_Moby_Releases.FieldName = "id_Moby_Releases"
		Me.colid_Moby_Releases.Name = "colid_Moby_Releases"
		Me.colid_Moby_Releases.OptionsColumn.AllowEdit = False
		Me.colid_Moby_Releases.OptionsColumn.ReadOnly = True
		Me.colid_Moby_Releases.Visible = True
		Me.colid_Moby_Releases.VisibleIndex = 1
		Me.colid_Moby_Releases.Width = 175
		'
		'rpi_Moby_Release
		'
		Me.rpi_Moby_Release.AutoHeight = False
		Me.rpi_Moby_Release.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.rpi_Moby_Release.DataSource = Me.BS_Moby_Releases
		Me.rpi_Moby_Release.DisplayMember = "Gamename"
		Me.rpi_Moby_Release.Name = "rpi_Moby_Release"
		Me.rpi_Moby_Release.NullText = ""
		Me.rpi_Moby_Release.ValueMember = "id_Moby_Releases"
		'
		'BS_Moby_Releases
		'
		Me.BS_Moby_Releases.DataMember = "src_Moby_Releases"
		Me.BS_Moby_Releases.DataSource = Me.DS_MobyDB
		'
		'colmd5
		'
		Me.colmd5.FieldName = "md5"
		Me.colmd5.Name = "colmd5"
		Me.colmd5.OptionsColumn.AllowEdit = False
		Me.colmd5.OptionsColumn.ReadOnly = True
		'
		'colsha1
		'
		Me.colsha1.FieldName = "sha1"
		Me.colsha1.Name = "colsha1"
		Me.colsha1.OptionsColumn.AllowEdit = False
		Me.colsha1.OptionsColumn.ReadOnly = True
		'
		'colCustomIdentifier
		'
		Me.colCustomIdentifier.FieldName = "CustomIdentifier"
		Me.colCustomIdentifier.Name = "colCustomIdentifier"
		Me.colCustomIdentifier.OptionsColumn.AllowEdit = False
		Me.colCustomIdentifier.OptionsColumn.ReadOnly = True
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
		'Ctl_MKDXSplitter1
		'
		Me.Ctl_MKDXSplitter1.Location = New System.Drawing.Point(497, 0)
		Me.Ctl_MKDXSplitter1.Name = "Ctl_MKDXSplitter1"
		Me.Ctl_MKDXSplitter1.Size = New System.Drawing.Size(5, 661)
		Me.Ctl_MKDXSplitter1.TabIndex = 1
		Me.Ctl_MKDXSplitter1.TabStop = False
		'
		'pnl_Right
		'
		Me.pnl_Right.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_Right.Controls.Add(Me.grd_Moby_Releases)
		Me.pnl_Right.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_Right.Location = New System.Drawing.Point(502, 0)
		Me.pnl_Right.Name = "pnl_Right"
		Me.pnl_Right.Size = New System.Drawing.Size(482, 661)
		Me.pnl_Right.TabIndex = 2
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
		Me.grd_Moby_Releases.Location = New System.Drawing.Point(3, 26)
		Me.grd_Moby_Releases.MainView = Me.gv_Moby_Releases
		Me.grd_Moby_Releases.Name = "grd_Moby_Releases"
		Me.grd_Moby_Releases.Size = New System.Drawing.Size(475, 632)
		Me.grd_Moby_Releases.TabIndex = 0
		Me.grd_Moby_Releases.UseEmbeddedNavigator = True
		Me.grd_Moby_Releases.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Moby_Releases})
		'
		'gv_Moby_Releases
		'
		Me.gv_Moby_Releases.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colGamename, Me.colHighlighted, Me.colYear, Me.colcreated, Me.colDeveloper, Me.colPublisher, Me.coldeprecated1, Me.colcompilation})
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
		Me.colGamename.Width = 279
		'
		'colHighlighted
		'
		Me.colHighlighted.FieldName = "Highlighted"
		Me.colHighlighted.Name = "colHighlighted"
		'
		'colYear
		'
		Me.colYear.FieldName = "Year"
		Me.colYear.Name = "colYear"
		Me.colYear.OptionsColumn.AllowEdit = False
		Me.colYear.OptionsColumn.ReadOnly = True
		Me.colYear.Visible = True
		Me.colYear.VisibleIndex = 1
		Me.colYear.Width = 62
		'
		'colcreated
		'
		Me.colcreated.Caption = "Added"
		Me.colcreated.DisplayFormat.FormatString = "g"
		Me.colcreated.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
		Me.colcreated.FieldName = "created"
		Me.colcreated.Name = "colcreated"
		Me.colcreated.OptionsColumn.AllowEdit = False
		Me.colcreated.OptionsColumn.ReadOnly = True
		Me.colcreated.Visible = True
		Me.colcreated.VisibleIndex = 2
		Me.colcreated.Width = 133
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
		'coldeprecated1
		'
		Me.coldeprecated1.FieldName = "deprecated"
		Me.coldeprecated1.Name = "coldeprecated1"
		Me.coldeprecated1.OptionsColumn.AllowEdit = False
		Me.coldeprecated1.OptionsColumn.ReadOnly = True
		Me.coldeprecated1.ToolTip = "indicates that the MobyGame may be deprecated (nothing really to worry here, the " &
		"meta data is still there)"
		'
		'colcompilation
		'
		Me.colcompilation.Caption = "Compilation"
		Me.colcompilation.FieldName = "compilation"
		Me.colcompilation.Name = "colcompilation"
		Me.colcompilation.OptionsColumn.AllowEdit = False
		Me.colcompilation.OptionsColumn.ReadOnly = True
		Me.colcompilation.ToolTip = "indicates that this is a compilation of multiple games"
		Me.colcompilation.Visible = True
		Me.colcompilation.VisibleIndex = 5
		'
		'BS_Moby_Platforms_gv2
		'
		Me.BS_Moby_Platforms_gv2.DataMember = "tbl_Moby_Platforms"
		Me.BS_Moby_Platforms_gv2.DataSource = Me.DS_MobyDB
		'
		'barmng
		'
		Me.barmng.DockControls.Add(Me.barDockControlTop)
		Me.barmng.DockControls.Add(Me.barDockControlBottom)
		Me.barmng.DockControls.Add(Me.barDockControlLeft)
		Me.barmng.DockControls.Add(Me.barDockControlRight)
		Me.barmng.Form = Me
		Me.barmng.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.bbi_Copy_Name_to_Clipboard, Me.bbi_Add_Games_from_DAT_XML, Me.bbi_Add_Games_from_CSV_Customidentifier_Name, Me.bbi_Add_Games_from_CSV_SegaCD, Me.bbi_Add_Games_from_CSV_Saturn, Me.bbi_Add_Games_from_CSV_DreamCast, Me.bbi_Delete, Me.bbi_Remove_Link, Me.bbi_Load_XML, Me.bbi_Write_XML, Me.bbi_Open_Moby_Page, Me.bbi_Evaluate_Moby_Links, Me.bbi_Auto_Link, Me.BarButtonItem1, Me.bbi_Add_Games_from_DAT})
		Me.barmng.MaxItemId = 15
		'
		'barDockControlTop
		'
		Me.barDockControlTop.CausesValidation = False
		Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
		Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
		Me.barDockControlTop.Size = New System.Drawing.Size(984, 0)
		'
		'barDockControlBottom
		'
		Me.barDockControlBottom.CausesValidation = False
		Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.barDockControlBottom.Location = New System.Drawing.Point(0, 661)
		Me.barDockControlBottom.Size = New System.Drawing.Size(984, 0)
		'
		'barDockControlLeft
		'
		Me.barDockControlLeft.CausesValidation = False
		Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
		Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
		Me.barDockControlLeft.Size = New System.Drawing.Size(0, 661)
		'
		'barDockControlRight
		'
		Me.barDockControlRight.CausesValidation = False
		Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
		Me.barDockControlRight.Location = New System.Drawing.Point(984, 0)
		Me.barDockControlRight.Size = New System.Drawing.Size(0, 661)
		'
		'bbi_Copy_Name_to_Clipboard
		'
		Me.bbi_Copy_Name_to_Clipboard.Caption = "&Copy Name to Clipboard"
		Me.bbi_Copy_Name_to_Clipboard.Id = 0
		Me.bbi_Copy_Name_to_Clipboard.ImageUri.Uri = "Paste"
		Me.bbi_Copy_Name_to_Clipboard.Name = "bbi_Copy_Name_to_Clipboard"
		'
		'bbi_Add_Games_from_DAT_XML
		'
		Me.bbi_Add_Games_from_DAT_XML.Caption = "Add Games from DAT (XML) file"
		Me.bbi_Add_Games_from_DAT_XML.Id = 1
		Me.bbi_Add_Games_from_DAT_XML.ImageUri.Uri = "Add"
		Me.bbi_Add_Games_from_DAT_XML.Name = "bbi_Add_Games_from_DAT_XML"
		'
		'bbi_Add_Games_from_CSV_Customidentifier_Name
		'
		Me.bbi_Add_Games_from_CSV_Customidentifier_Name.Caption = "Add Games from CSV (CustomIdentifier;Name)"
		Me.bbi_Add_Games_from_CSV_Customidentifier_Name.Id = 2
		Me.bbi_Add_Games_from_CSV_Customidentifier_Name.ImageUri.Uri = "Add"
		Me.bbi_Add_Games_from_CSV_Customidentifier_Name.Name = "bbi_Add_Games_from_CSV_Customidentifier_Name"
		'
		'bbi_Add_Games_from_CSV_SegaCD
		'
		Me.bbi_Add_Games_from_CSV_SegaCD.Caption = "Add Games from CSV (SegaCD: Name;id1;id2;id3;id4)"
		Me.bbi_Add_Games_from_CSV_SegaCD.Id = 3
		Me.bbi_Add_Games_from_CSV_SegaCD.ImageUri.Uri = "Add"
		Me.bbi_Add_Games_from_CSV_SegaCD.Name = "bbi_Add_Games_from_CSV_SegaCD"
		'
		'bbi_Add_Games_from_CSV_Saturn
		'
		Me.bbi_Add_Games_from_CSV_Saturn.Caption = "Add Games from CSV (Saturn: Name;id1;...;id5)"
		Me.bbi_Add_Games_from_CSV_Saturn.Id = 4
		Me.bbi_Add_Games_from_CSV_Saturn.ImageUri.Uri = "Add"
		Me.bbi_Add_Games_from_CSV_Saturn.Name = "bbi_Add_Games_from_CSV_Saturn"
		'
		'bbi_Add_Games_from_CSV_DreamCast
		'
		Me.bbi_Add_Games_from_CSV_DreamCast.Caption = "Add Games from CSV (DreamCast: Name;id1;id2;...)"
		Me.bbi_Add_Games_from_CSV_DreamCast.Id = 5
		Me.bbi_Add_Games_from_CSV_DreamCast.ImageUri.Uri = "Add"
		Me.bbi_Add_Games_from_CSV_DreamCast.Name = "bbi_Add_Games_from_CSV_DreamCast"
		'
		'bbi_Delete
		'
		Me.bbi_Delete.Caption = "Delete {0} entries"
		Me.bbi_Delete.Id = 6
		Me.bbi_Delete.ImageUri.Uri = "Delete"
		Me.bbi_Delete.Name = "bbi_Delete"
		'
		'bbi_Remove_Link
		'
		Me.bbi_Remove_Link.Caption = "Remove Link on {0} games"
		Me.bbi_Remove_Link.Id = 7
		Me.bbi_Remove_Link.ImageUri.Uri = "Undo"
		Me.bbi_Remove_Link.Name = "bbi_Remove_Link"
		'
		'bbi_Load_XML
		'
		Me.bbi_Load_XML.Caption = "Load XML"
		Me.bbi_Load_XML.Id = 8
		Me.bbi_Load_XML.ImageUri.Uri = "Open"
		Me.bbi_Load_XML.Name = "bbi_Load_XML"
		'
		'bbi_Write_XML
		'
		Me.bbi_Write_XML.Caption = "Write XML"
		Me.bbi_Write_XML.Id = 9
		Me.bbi_Write_XML.ImageUri.Uri = "Save"
		Me.bbi_Write_XML.Name = "bbi_Write_XML"
		'
		'bbi_Open_Moby_Page
		'
		Me.bbi_Open_Moby_Page.Caption = "&Open Moby Page"
		Me.bbi_Open_Moby_Page.Id = 10
		Me.bbi_Open_Moby_Page.ImageUri.Uri = "NavigationBar"
		Me.bbi_Open_Moby_Page.Name = "bbi_Open_Moby_Page"
		'
		'bbi_Evaluate_Moby_Links
		'
		Me.bbi_Evaluate_Moby_Links.Caption = "&Evaluate Moby Links" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
		Me.bbi_Evaluate_Moby_Links.Id = 11
		Me.bbi_Evaluate_Moby_Links.ImageUri.Uri = "Zoom100"
		Me.bbi_Evaluate_Moby_Links.Name = "bbi_Evaluate_Moby_Links"
		'
		'bbi_Auto_Link
		'
		Me.bbi_Auto_Link.Caption = "Detect MobyGames Links..."
		Me.bbi_Auto_Link.Id = 12
		Me.bbi_Auto_Link.ImageUri.Uri = "Find"
		Me.bbi_Auto_Link.Name = "bbi_Auto_Link"
		ToolTipTitleItem1.Text = "Detect MobyGames Links"
		ToolTipItem1.LeftIndent = 6
		ToolTipItem1.Text = resources.GetString("ToolTipItem1.Text")
		SuperToolTip1.Items.Add(ToolTipTitleItem1)
		SuperToolTip1.Items.Add(ToolTipItem1)
		Me.bbi_Auto_Link.SuperTip = SuperToolTip1
		'
		'BarButtonItem1
		'
		Me.BarButtonItem1.Caption = "BarButtonItem1"
		Me.BarButtonItem1.Id = 13
		Me.BarButtonItem1.Name = "BarButtonItem1"
		'
		'bbi_Add_Games_from_DAT
		'
		Me.bbi_Add_Games_from_DAT.Caption = "Add Games from DAT with serial tag (No-Intro Sony PSP)"
		Me.bbi_Add_Games_from_DAT.Id = 14
		Me.bbi_Add_Games_from_DAT.ImageUri.Uri = "Add"
		Me.bbi_Add_Games_from_DAT.Name = "bbi_Add_Games_from_DAT"
		'
		'popmnu_Rombase
		'
		Me.popmnu_Rombase.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Copy_Name_to_Clipboard), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Add_Games_from_DAT_XML), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Add_Games_from_DAT), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Add_Games_from_CSV_Customidentifier_Name), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Add_Games_from_CSV_SegaCD), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Add_Games_from_CSV_Saturn), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Add_Games_from_CSV_DreamCast), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Delete), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Auto_Link, True), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Remove_Link), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Load_XML, True), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Write_XML)})
		Me.popmnu_Rombase.Manager = Me.barmng
		Me.popmnu_Rombase.Name = "popmnu_Rombase"
		'
		'popmnu_Moby_Games
		'
		Me.popmnu_Moby_Games.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Open_Moby_Page), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Evaluate_Moby_Links)})
		Me.popmnu_Moby_Games.Manager = Me.barmng
		Me.popmnu_Moby_Games.Name = "popmnu_Moby_Games"
		'
		'frm_ROMBase_Manager
		'
		Me.ClientSize = New System.Drawing.Size(984, 661)
		Me.Controls.Add(Me.pnl_Right)
		Me.Controls.Add(Me.Ctl_MKDXSplitter1)
		Me.Controls.Add(Me.pnl_Left)
		Me.Controls.Add(Me.barDockControlLeft)
		Me.Controls.Add(Me.barDockControlRight)
		Me.Controls.Add(Me.barDockControlBottom)
		Me.Controls.Add(Me.barDockControlTop)
		Me.Name = "frm_ROMBase_Manager"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "RomBase Manager"
		Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
		CType(Me.pnl_Left, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_Left.ResumeLayout(False)
		CType(Me.cmb_Platform.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Moby_Platforms, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_MobyDB, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.grd_DAT, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Rombase, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_Rombase, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_DAT, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_Moby_Platforms_gv1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Moby_Platforms_gv1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_Moby_Release, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Moby_Releases, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pnl_Right, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_Right.ResumeLayout(False)
		CType(Me.grd_Moby_Releases, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Moby_Releases, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Moby_Platforms_gv2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.barmng, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.popmnu_Rombase, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.popmnu_Moby_Games, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents pnl_Left As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents Ctl_MKDXSplitter1 As MKNetDXLib.ctl_MKDXSplitter
	Friend WithEvents pnl_Right As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents BS_Rombase As System.Windows.Forms.BindingSource
	Friend WithEvents DS_Rombase As Metropolis_Launcher.DS_Rombase
	Friend WithEvents BS_Moby_Platforms As System.Windows.Forms.BindingSource
	Friend WithEvents BS_Moby_Releases As System.Windows.Forms.BindingSource
	Friend WithEvents rpi_Moby_Release As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
	Friend WithEvents lbl_Platform As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents cmb_Platform As MKNetDXLib.ctl_MKDXLookupEdit
	Private WithEvents grd_DAT As MKNetDXLib.ctl_MKDXGrid
	Private WithEvents gv_DAT As DevExpress.XtraGrid.Views.Grid.GridView
	Private WithEvents grd_Moby_Releases As MKNetDXLib.ctl_MKDXGrid
	Private WithEvents gv_Moby_Releases As DevExpress.XtraGrid.Views.Grid.GridView
	Private WithEvents colfilename As DevExpress.XtraGrid.Columns.GridColumn
	Private WithEvents colid_Moby_Platforms As DevExpress.XtraGrid.Columns.GridColumn
	Private WithEvents colid_Moby_Releases As DevExpress.XtraGrid.Columns.GridColumn
	Private WithEvents colGamename As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents BS_Moby_Platforms_gv1 As System.Windows.Forms.BindingSource
	Friend WithEvents BS_Moby_Platforms_gv2 As System.Windows.Forms.BindingSource
	Friend WithEvents rpi_Moby_Platforms_gv1 As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
	Friend WithEvents btn_Save As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents colid_rombase As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colmd5 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colsha1 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colCustomIdentifier As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colHighlighted As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colYear As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colcreated As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents barmng As MKNetDXLib.ctl_MKDXBarManager
	Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
	Friend WithEvents bbi_Copy_Name_to_Clipboard As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Add_Games_from_DAT_XML As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Add_Games_from_CSV_Customidentifier_Name As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Add_Games_from_CSV_SegaCD As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Add_Games_from_CSV_Saturn As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Add_Games_from_CSV_DreamCast As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Delete As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Remove_Link As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Load_XML As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Write_XML As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents popmnu_Rombase As MKNetDXLib.cmp_MKDXPopupMenu
	Friend WithEvents bbi_Open_Moby_Page As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Evaluate_Moby_Links As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents popmnu_Moby_Games As MKNetDXLib.cmp_MKDXPopupMenu
	Friend WithEvents DS_MobyDB As DS_MobyDB
	Friend WithEvents bbi_Auto_Link As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Add_Games_from_DAT As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents colDeveloper As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colPublisher As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents coldeprecated As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents coldeprecated1 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colcompilation As DevExpress.XtraGrid.Columns.GridColumn
End Class
