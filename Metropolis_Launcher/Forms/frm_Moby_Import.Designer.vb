<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Moby_Import
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
		Me.grd_Moby_Games = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_Moby_Web_Games = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_Moby_Web = New System.Data.DataSet()
		Me.tbl_Moby_Games = New System.Data.DataTable()
		Me.DataColumn1 = New System.Data.DataColumn()
		Me.DataColumn2 = New System.Data.DataColumn()
		Me.DataColumn3 = New System.Data.DataColumn()
		Me.DataColumn4 = New System.Data.DataColumn()
		Me.DataColumn5 = New System.Data.DataColumn()
		Me.DataColumn6 = New System.Data.DataColumn()
		Me.DataTable1 = New System.Data.DataTable()
		Me.DataColumn7 = New System.Data.DataColumn()
		Me.DataColumn8 = New System.Data.DataColumn()
		Me.DataColumn9 = New System.Data.DataColumn()
		Me.DataColumn10 = New System.Data.DataColumn()
		Me.gv_Moby_Games = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colGameTitle = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colURLPart = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colYear = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colPlatformName = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.DS_MobyDB = New Metropolis_Launcher.DS_MobyDB()
		Me.btn_Run = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.splt1 = New MKNetDXLib.ctl_MKDXSplitPanel()
		Me.lbl_Moby_Games = New MKNetDXLib.ctl_MKDXLabel()
		Me.grd_Moby_Game_Groups = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_Moby_Web_Game_Groups = New System.Windows.Forms.BindingSource(Me.components)
		Me.gv_Moby_Game_Groups = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colName = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colDescription = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colURLPart1 = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colNumberOfGames = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.lbl_Moby_Game_Groups = New MKNetDXLib.ctl_MKDXLabel()
		Me.barmng = New MKNetDXLib.ctl_MKDXBarManager()
		Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
		Me.bbi_Import_Single_Game = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Import_Single_Game_Group = New DevExpress.XtraBars.BarButtonItem()
		Me.popmnu_Moby_Web_Games = New MKNetDXLib.cmp_MKDXPopupMenu()
		Me.popmnu_Moby_Web_Game_Groups = New MKNetDXLib.cmp_MKDXPopupMenu()
		Me.lbl_GenreImport = New MKNetDXLib.ctl_MKDXLabel()
		Me.txb_GenreImport = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.btn_GenreImport = New MKNetDXLib.ctl_MKDXSimpleButton()
		CType(Me.grd_Moby_Games, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Moby_Web_Games, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_Moby_Web, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.tbl_Moby_Games, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_Moby_Games, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_MobyDB, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.splt1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splt1.SuspendLayout()
		CType(Me.grd_Moby_Game_Groups, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Moby_Web_Game_Groups, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_Moby_Game_Groups, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.barmng, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.popmnu_Moby_Web_Games, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.popmnu_Moby_Web_Game_Groups, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_GenreImport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'grd_Moby_Games
		'
		Me.grd_Moby_Games.DataSource = Me.BS_Moby_Web_Games
		Me.grd_Moby_Games.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_Moby_Games.Location = New System.Drawing.Point(0, 43)
		Me.grd_Moby_Games.MainView = Me.gv_Moby_Games
		Me.grd_Moby_Games.Name = "grd_Moby_Games"
		Me.grd_Moby_Games.Size = New System.Drawing.Size(1004, 136)
		Me.grd_Moby_Games.TabIndex = 0
		Me.grd_Moby_Games.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Moby_Games})
		'
		'BS_Moby_Web_Games
		'
		Me.BS_Moby_Web_Games.DataMember = "tbl_Moby_Games"
		Me.BS_Moby_Web_Games.DataSource = Me.DS_Moby_Web
		'
		'DS_Moby_Web
		'
		Me.DS_Moby_Web.DataSetName = "DS_Moby_Web"
		Me.DS_Moby_Web.Tables.AddRange(New System.Data.DataTable() {Me.tbl_Moby_Games, Me.DataTable1})
		'
		'tbl_Moby_Games
		'
		Me.tbl_Moby_Games.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn1, Me.DataColumn2, Me.DataColumn3, Me.DataColumn4, Me.DataColumn5, Me.DataColumn6})
		Me.tbl_Moby_Games.TableName = "tbl_Moby_Games"
		'
		'DataColumn1
		'
		Me.DataColumn1.ColumnName = "Game_Title"
		'
		'DataColumn2
		'
		Me.DataColumn2.ColumnName = "URLPart"
		'
		'DataColumn3
		'
		Me.DataColumn3.ColumnName = "Year"
		'
		'DataColumn4
		'
		Me.DataColumn4.ColumnName = "Platform_URLPart"
		'
		'DataColumn5
		'
		Me.DataColumn5.ColumnName = "id_Moby_Platforms"
		Me.DataColumn5.DataType = GetType(Integer)
		'
		'DataColumn6
		'
		Me.DataColumn6.ColumnName = "PlatformName"
		'
		'DataTable1
		'
		Me.DataTable1.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn7, Me.DataColumn8, Me.DataColumn9, Me.DataColumn10})
		Me.DataTable1.TableName = "tbl_Moby_Game_Groups"
		'
		'DataColumn7
		'
		Me.DataColumn7.ColumnName = "Name"
		'
		'DataColumn8
		'
		Me.DataColumn8.ColumnName = "Description"
		'
		'DataColumn9
		'
		Me.DataColumn9.ColumnName = "URLPart"
		'
		'DataColumn10
		'
		Me.DataColumn10.ColumnName = "NumberOfGames"
		Me.DataColumn10.DataType = GetType(Integer)
		'
		'gv_Moby_Games
		'
		Me.gv_Moby_Games.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colGameTitle, Me.colURLPart, Me.colYear, Me.colPlatformName})
		Me.gv_Moby_Games.GridControl = Me.grd_Moby_Games
		Me.gv_Moby_Games.Name = "gv_Moby_Games"
		Me.gv_Moby_Games.OptionsView.ShowGroupPanel = False
		'
		'colGameTitle
		'
		Me.colGameTitle.FieldName = "Game_Title"
		Me.colGameTitle.Name = "colGameTitle"
		Me.colGameTitle.OptionsColumn.AllowEdit = False
		Me.colGameTitle.OptionsColumn.AllowFocus = False
		Me.colGameTitle.OptionsColumn.ReadOnly = True
		Me.colGameTitle.Visible = True
		Me.colGameTitle.VisibleIndex = 0
		'
		'colURLPart
		'
		Me.colURLPart.FieldName = "URLPart"
		Me.colURLPart.Name = "colURLPart"
		Me.colURLPart.OptionsColumn.AllowEdit = False
		Me.colURLPart.OptionsColumn.AllowFocus = False
		Me.colURLPart.OptionsColumn.ReadOnly = True
		Me.colURLPart.Visible = True
		Me.colURLPart.VisibleIndex = 1
		'
		'colYear
		'
		Me.colYear.FieldName = "Year"
		Me.colYear.Name = "colYear"
		Me.colYear.OptionsColumn.AllowEdit = False
		Me.colYear.OptionsColumn.AllowFocus = False
		Me.colYear.OptionsColumn.ReadOnly = True
		Me.colYear.Visible = True
		Me.colYear.VisibleIndex = 2
		'
		'colPlatformName
		'
		Me.colPlatformName.Caption = "Platform"
		Me.colPlatformName.FieldName = "PlatformName"
		Me.colPlatformName.Name = "colPlatformName"
		Me.colPlatformName.OptionsColumn.AllowEdit = False
		Me.colPlatformName.OptionsColumn.AllowFocus = False
		Me.colPlatformName.OptionsColumn.ReadOnly = True
		Me.colPlatformName.Visible = True
		Me.colPlatformName.VisibleIndex = 3
		'
		'DS_MobyDB
		'
		Me.DS_MobyDB.DataSetName = "DS_MobyDB"
		Me.DS_MobyDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'btn_Run
		'
		Me.btn_Run.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btn_Run.Location = New System.Drawing.Point(3, 364)
		Me.btn_Run.Name = "btn_Run"
		Me.btn_Run.Size = New System.Drawing.Size(75, 23)
		Me.btn_Run.TabIndex = 0
		Me.btn_Run.Text = "Run it"
		'
		'splt1
		'
		Me.splt1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.splt1.Horizontal = False
		Me.splt1.Location = New System.Drawing.Point(3, 3)
		Me.splt1.Name = "splt1"
		Me.splt1.Panel1.Controls.Add(Me.grd_Moby_Games)
		Me.splt1.Panel1.Controls.Add(Me.lbl_Moby_Games)
		Me.splt1.Panel1.Text = "Panel1"
		Me.splt1.Panel2.Controls.Add(Me.grd_Moby_Game_Groups)
		Me.splt1.Panel2.Controls.Add(Me.lbl_Moby_Game_Groups)
		Me.splt1.Panel2.Text = "Panel2"
		Me.splt1.Size = New System.Drawing.Size(1004, 358)
		Me.splt1.SplitterPosition = 179
		Me.splt1.TabIndex = 2
		Me.splt1.Text = "Ctl_MKDXSplitPanel1"
		'
		'lbl_Moby_Games
		'
		Me.lbl_Moby_Games.Appearance.Font = New System.Drawing.Font("Segoe UI", 16.0!)
		Me.lbl_Moby_Games.AutoEllipsis = True
		Me.lbl_Moby_Games.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Moby_Games.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_Moby_Games.Location = New System.Drawing.Point(0, 0)
		Me.lbl_Moby_Games.MKBoundControl1 = Nothing
		Me.lbl_Moby_Games.MKBoundControl2 = Nothing
		Me.lbl_Moby_Games.MKBoundControl3 = Nothing
		Me.lbl_Moby_Games.MKBoundControl4 = Nothing
		Me.lbl_Moby_Games.MKBoundControl5 = Nothing
		Me.lbl_Moby_Games.Name = "lbl_Moby_Games"
		Me.lbl_Moby_Games.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_Moby_Games.Size = New System.Drawing.Size(1004, 43)
		Me.lbl_Moby_Games.TabIndex = 5
		Me.lbl_Moby_Games.Text = "Moby Games"
		'
		'grd_Moby_Game_Groups
		'
		Me.grd_Moby_Game_Groups.DataSource = Me.BS_Moby_Web_Game_Groups
		Me.grd_Moby_Game_Groups.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_Moby_Game_Groups.Location = New System.Drawing.Point(0, 43)
		Me.grd_Moby_Game_Groups.MainView = Me.gv_Moby_Game_Groups
		Me.grd_Moby_Game_Groups.Name = "grd_Moby_Game_Groups"
		Me.grd_Moby_Game_Groups.Size = New System.Drawing.Size(1004, 131)
		Me.grd_Moby_Game_Groups.TabIndex = 0
		Me.grd_Moby_Game_Groups.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Moby_Game_Groups})
		'
		'BS_Moby_Web_Game_Groups
		'
		Me.BS_Moby_Web_Game_Groups.DataMember = "tbl_Moby_Game_Groups"
		Me.BS_Moby_Web_Game_Groups.DataSource = Me.DS_Moby_Web
		'
		'gv_Moby_Game_Groups
		'
		Me.gv_Moby_Game_Groups.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colName, Me.colDescription, Me.colURLPart1, Me.colNumberOfGames})
		Me.gv_Moby_Game_Groups.GridControl = Me.grd_Moby_Game_Groups
		Me.gv_Moby_Game_Groups.Name = "gv_Moby_Game_Groups"
		Me.gv_Moby_Game_Groups.OptionsView.ShowGroupPanel = False
		'
		'colName
		'
		Me.colName.FieldName = "Name"
		Me.colName.Name = "colName"
		Me.colName.OptionsColumn.AllowEdit = False
		Me.colName.OptionsColumn.AllowFocus = False
		Me.colName.OptionsColumn.ReadOnly = True
		Me.colName.Visible = True
		Me.colName.VisibleIndex = 0
		Me.colName.Width = 456
		'
		'colDescription
		'
		Me.colDescription.FieldName = "Description"
		Me.colDescription.Name = "colDescription"
		Me.colDescription.OptionsColumn.AllowEdit = False
		Me.colDescription.OptionsColumn.AllowFocus = False
		Me.colDescription.OptionsColumn.ReadOnly = True
		Me.colDescription.Visible = True
		Me.colDescription.VisibleIndex = 2
		Me.colDescription.Width = 678
		'
		'colURLPart1
		'
		Me.colURLPart1.FieldName = "URLPart"
		Me.colURLPart1.Name = "colURLPart1"
		Me.colURLPart1.OptionsColumn.AllowEdit = False
		Me.colURLPart1.OptionsColumn.AllowFocus = False
		Me.colURLPart1.OptionsColumn.ReadOnly = True
		Me.colURLPart1.Visible = True
		Me.colURLPart1.VisibleIndex = 1
		Me.colURLPart1.Width = 228
		'
		'colNumberOfGames
		'
		Me.colNumberOfGames.Caption = "N° of Games"
		Me.colNumberOfGames.FieldName = "NumberOfGames"
		Me.colNumberOfGames.Name = "colNumberOfGames"
		Me.colNumberOfGames.OptionsColumn.AllowEdit = False
		Me.colNumberOfGames.OptionsColumn.AllowFocus = False
		Me.colNumberOfGames.OptionsColumn.ReadOnly = True
		Me.colNumberOfGames.Visible = True
		Me.colNumberOfGames.VisibleIndex = 3
		Me.colNumberOfGames.Width = 132
		'
		'lbl_Moby_Game_Groups
		'
		Me.lbl_Moby_Game_Groups.Appearance.Font = New System.Drawing.Font("Segoe UI", 16.0!)
		Me.lbl_Moby_Game_Groups.AutoEllipsis = True
		Me.lbl_Moby_Game_Groups.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Moby_Game_Groups.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_Moby_Game_Groups.Location = New System.Drawing.Point(0, 0)
		Me.lbl_Moby_Game_Groups.MKBoundControl1 = Nothing
		Me.lbl_Moby_Game_Groups.MKBoundControl2 = Nothing
		Me.lbl_Moby_Game_Groups.MKBoundControl3 = Nothing
		Me.lbl_Moby_Game_Groups.MKBoundControl4 = Nothing
		Me.lbl_Moby_Game_Groups.MKBoundControl5 = Nothing
		Me.lbl_Moby_Game_Groups.Name = "lbl_Moby_Game_Groups"
		Me.lbl_Moby_Game_Groups.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_Moby_Game_Groups.Size = New System.Drawing.Size(1004, 43)
		Me.lbl_Moby_Game_Groups.TabIndex = 6
		Me.lbl_Moby_Game_Groups.Text = "Moby Game Groups"
		'
		'barmng
		'
		Me.barmng.DockControls.Add(Me.barDockControlTop)
		Me.barmng.DockControls.Add(Me.barDockControlBottom)
		Me.barmng.DockControls.Add(Me.barDockControlLeft)
		Me.barmng.DockControls.Add(Me.barDockControlRight)
		Me.barmng.Form = Me
		Me.barmng.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.bbi_Import_Single_Game, Me.bbi_Import_Single_Game_Group})
		Me.barmng.MaxItemId = 2
		'
		'barDockControlTop
		'
		Me.barDockControlTop.CausesValidation = False
		Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
		Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
		Me.barDockControlTop.Size = New System.Drawing.Size(1010, 0)
		'
		'barDockControlBottom
		'
		Me.barDockControlBottom.CausesValidation = False
		Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.barDockControlBottom.Location = New System.Drawing.Point(0, 390)
		Me.barDockControlBottom.Size = New System.Drawing.Size(1010, 0)
		'
		'barDockControlLeft
		'
		Me.barDockControlLeft.CausesValidation = False
		Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
		Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
		Me.barDockControlLeft.Size = New System.Drawing.Size(0, 390)
		'
		'barDockControlRight
		'
		Me.barDockControlRight.CausesValidation = False
		Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
		Me.barDockControlRight.Location = New System.Drawing.Point(1010, 0)
		Me.barDockControlRight.Size = New System.Drawing.Size(0, 390)
		'
		'bbi_Import_Single_Game
		'
		Me.bbi_Import_Single_Game.Caption = "Import/Update {0}"
		Me.bbi_Import_Single_Game.Id = 0
		Me.bbi_Import_Single_Game.ImageUri.Uri = "NavigationBar"
		Me.bbi_Import_Single_Game.Name = "bbi_Import_Single_Game"
		'
		'bbi_Import_Single_Game_Group
		'
		Me.bbi_Import_Single_Game_Group.Caption = "Import/Update {0}"
		Me.bbi_Import_Single_Game_Group.Id = 1
		Me.bbi_Import_Single_Game_Group.ImageUri.Uri = "NavigationBar"
		Me.bbi_Import_Single_Game_Group.Name = "bbi_Import_Single_Game_Group"
		'
		'popmnu_Moby_Web_Games
		'
		Me.popmnu_Moby_Web_Games.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Import_Single_Game)})
		Me.popmnu_Moby_Web_Games.Manager = Me.barmng
		Me.popmnu_Moby_Web_Games.Name = "popmnu_Moby_Web_Games"
		'
		'popmnu_Moby_Web_Game_Groups
		'
		Me.popmnu_Moby_Web_Game_Groups.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Import_Single_Game_Group)})
		Me.popmnu_Moby_Web_Game_Groups.Manager = Me.barmng
		Me.popmnu_Moby_Web_Game_Groups.Name = "popmnu_Moby_Web_Game_Groups"
		'
		'lbl_GenreImport
		'
		Me.lbl_GenreImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_GenreImport.Location = New System.Drawing.Point(731, 369)
		Me.lbl_GenreImport.MKBoundControl1 = Nothing
		Me.lbl_GenreImport.MKBoundControl2 = Nothing
		Me.lbl_GenreImport.MKBoundControl3 = Nothing
		Me.lbl_GenreImport.MKBoundControl4 = Nothing
		Me.lbl_GenreImport.MKBoundControl5 = Nothing
		Me.lbl_GenreImport.Name = "lbl_GenreImport"
		Me.lbl_GenreImport.Size = New System.Drawing.Size(92, 13)
		Me.lbl_GenreImport.TabIndex = 7
		Me.lbl_GenreImport.Text = "Test Genre Import:"
		'
		'txb_GenreImport
		'
		Me.txb_GenreImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_GenreImport.Location = New System.Drawing.Point(827, 366)
		Me.txb_GenreImport.MenuManager = Me.barmng
		Me.txb_GenreImport.MKBoundLabel = Nothing
		Me.txb_GenreImport.MKEditValue_Compare = Nothing
		Me.txb_GenreImport.Name = "txb_GenreImport"
		Me.txb_GenreImport.Size = New System.Drawing.Size(100, 20)
		Me.txb_GenreImport.TabIndex = 8
		'
		'btn_GenreImport
		'
		Me.btn_GenreImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_GenreImport.Location = New System.Drawing.Point(930, 365)
		Me.btn_GenreImport.Name = "btn_GenreImport"
		Me.btn_GenreImport.Size = New System.Drawing.Size(77, 23)
		Me.btn_GenreImport.TabIndex = 9
		Me.btn_GenreImport.Text = "Import Genre"
		'
		'frm_Moby_Import
		'
		Me.ClientSize = New System.Drawing.Size(1010, 390)
		Me.Controls.Add(Me.btn_GenreImport)
		Me.Controls.Add(Me.txb_GenreImport)
		Me.Controls.Add(Me.lbl_GenreImport)
		Me.Controls.Add(Me.splt1)
		Me.Controls.Add(Me.btn_Run)
		Me.Controls.Add(Me.barDockControlLeft)
		Me.Controls.Add(Me.barDockControlRight)
		Me.Controls.Add(Me.barDockControlBottom)
		Me.Controls.Add(Me.barDockControlTop)
		Me.Name = "frm_Moby_Import"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		CType(Me.grd_Moby_Games, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Moby_Web_Games, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_Moby_Web, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.tbl_Moby_Games, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Moby_Games, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_MobyDB, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.splt1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splt1.ResumeLayout(False)
		CType(Me.grd_Moby_Game_Groups, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Moby_Web_Game_Groups, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Moby_Game_Groups, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.barmng, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.popmnu_Moby_Web_Games, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.popmnu_Moby_Web_Game_Groups, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_GenreImport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents grd_Moby_Games As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_Moby_Games As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents DS_MobyDB As Metropolis_Launcher.DS_MobyDB
	Friend WithEvents DS_Moby_Web As System.Data.DataSet
	Friend WithEvents tbl_Moby_Games As System.Data.DataTable
	Friend WithEvents DataColumn1 As System.Data.DataColumn
	Friend WithEvents DataColumn2 As System.Data.DataColumn
	Friend WithEvents DataColumn3 As System.Data.DataColumn
	Friend WithEvents BS_Moby_Web_Games As System.Windows.Forms.BindingSource
	Friend WithEvents colGameTitle As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colURLPart As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colYear As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents btn_Run As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents DataColumn4 As System.Data.DataColumn
	Friend WithEvents DataColumn5 As System.Data.DataColumn
	Friend WithEvents DataColumn6 As System.Data.DataColumn
	Friend WithEvents colPlatformName As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents splt1 As MKNetDXLib.ctl_MKDXSplitPanel
	Friend WithEvents lbl_Moby_Games As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents grd_Moby_Game_Groups As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_Moby_Game_Groups As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents lbl_Moby_Game_Groups As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents DataTable1 As System.Data.DataTable
	Friend WithEvents DataColumn7 As System.Data.DataColumn
	Friend WithEvents DataColumn8 As System.Data.DataColumn
	Friend WithEvents DataColumn9 As System.Data.DataColumn
	Friend WithEvents BS_Moby_Web_Game_Groups As System.Windows.Forms.BindingSource
	Friend WithEvents colName As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colDescription As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colURLPart1 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents DataColumn10 As System.Data.DataColumn
	Friend WithEvents colNumberOfGames As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents barmng As MKNetDXLib.ctl_MKDXBarManager
	Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
	Friend WithEvents bbi_Import_Single_Game As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Import_Single_Game_Group As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents popmnu_Moby_Web_Games As MKNetDXLib.cmp_MKDXPopupMenu
	Friend WithEvents popmnu_Moby_Web_Game_Groups As MKNetDXLib.cmp_MKDXPopupMenu
	Friend WithEvents btn_GenreImport As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents txb_GenreImport As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents lbl_GenreImport As MKNetDXLib.ctl_MKDXLabel
End Class
