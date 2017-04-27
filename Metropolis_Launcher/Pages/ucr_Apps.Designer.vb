<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucr_Apps
	Inherits MKNetDXLib.ctl_MKDXUserControl

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
		Me.grd_Apps = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_Apps = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_MLApps = New Metropolis_Launcher.DS_MLApps()
		Me.gv_Apps = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colid_Categories = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.rpi_Category = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		Me.BS_Categories = New System.Windows.Forms.BindingSource(Me.components)
		Me.colDisplayName = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.Ctl_MKDXSplitter1 = New MKNetDXLib.ctl_MKDXSplitter()
		Me.txb_Description = New MKNetDXLib.ctl_MKDXMemoEdit()
		Me.barmng_Apps = New MKNetDXLib.ctl_MKDXBarManager()
		Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
		Me.bbi_Run = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Add = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Edit = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Delete = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Reset = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_ResetAll = New DevExpress.XtraBars.BarButtonItem()
		Me.lbl_Displayname = New MKNetDXLib.ctl_MKDXLabel()
		Me.popmnu_Apps = New MKNetDXLib.cmp_MKDXPopupMenu()
		CType(Me.grd_Apps, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Apps, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_MLApps, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_Apps, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_Category, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Categories, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_Description.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.barmng_Apps, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.popmnu_Apps, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'grd_Apps
		'
		Me.grd_Apps.DataSource = Me.BS_Apps
		Me.grd_Apps.Dock = System.Windows.Forms.DockStyle.Left
		Me.grd_Apps.Location = New System.Drawing.Point(0, 0)
		Me.grd_Apps.MainView = Me.gv_Apps
		Me.grd_Apps.Name = "grd_Apps"
		Me.grd_Apps.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rpi_Category})
		Me.grd_Apps.Size = New System.Drawing.Size(411, 600)
		Me.grd_Apps.TabIndex = 0
		Me.grd_Apps.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Apps})
		'
		'BS_Apps
		'
		Me.BS_Apps.DataMember = "Apps"
		Me.BS_Apps.DataSource = Me.DS_MLApps
		'
		'DS_MLApps
		'
		Me.DS_MLApps.DataSetName = "DS_MLApps"
		Me.DS_MLApps.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'gv_Apps
		'
		Me.gv_Apps.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colid_Categories, Me.colDisplayName})
		Me.gv_Apps.GridControl = Me.grd_Apps
		Me.gv_Apps.Name = "gv_Apps"
		Me.gv_Apps.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
		Me.gv_Apps.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_Apps.OptionsView.ColumnAutoWidth = False
		Me.gv_Apps.OptionsView.ShowGroupPanel = False
		Me.gv_Apps.OptionsView.ShowIndicator = False
		'
		'colid_Categories
		'
		Me.colid_Categories.Caption = "Category"
		Me.colid_Categories.ColumnEdit = Me.rpi_Category
		Me.colid_Categories.FieldName = "id_Categories"
		Me.colid_Categories.Name = "colid_Categories"
		Me.colid_Categories.OptionsColumn.AllowEdit = False
		Me.colid_Categories.OptionsColumn.ReadOnly = True
		Me.colid_Categories.Visible = True
		Me.colid_Categories.VisibleIndex = 0
		Me.colid_Categories.Width = 105
		'
		'rpi_Category
		'
		Me.rpi_Category.AutoHeight = False
		Me.rpi_Category.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.rpi_Category.DataSource = Me.BS_Categories
		Me.rpi_Category.DisplayMember = "Category"
		Me.rpi_Category.Name = "rpi_Category"
		Me.rpi_Category.ValueMember = "id_Categories"
		'
		'BS_Categories
		'
		Me.BS_Categories.DataMember = "Categories"
		Me.BS_Categories.DataSource = Me.DS_MLApps
		'
		'colDisplayName
		'
		Me.colDisplayName.Caption = "Application"
		Me.colDisplayName.FieldName = "DisplayName"
		Me.colDisplayName.Name = "colDisplayName"
		Me.colDisplayName.OptionsColumn.AllowEdit = False
		Me.colDisplayName.OptionsColumn.ReadOnly = True
		Me.colDisplayName.Visible = True
		Me.colDisplayName.VisibleIndex = 1
		Me.colDisplayName.Width = 292
		'
		'Ctl_MKDXSplitter1
		'
		Me.Ctl_MKDXSplitter1.Location = New System.Drawing.Point(411, 0)
		Me.Ctl_MKDXSplitter1.Name = "Ctl_MKDXSplitter1"
		Me.Ctl_MKDXSplitter1.Size = New System.Drawing.Size(5, 600)
		Me.Ctl_MKDXSplitter1.TabIndex = 1
		Me.Ctl_MKDXSplitter1.TabStop = False
		'
		'txb_Description
		'
		Me.txb_Description.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BS_Apps, "Description", True))
		Me.txb_Description.Dock = System.Windows.Forms.DockStyle.Fill
		Me.txb_Description.Location = New System.Drawing.Point(416, 36)
		Me.txb_Description.MenuManager = Me.barmng_Apps
		Me.txb_Description.MKBoundLabel = Nothing
		Me.txb_Description.MKEditValue_Compare = Nothing
		Me.txb_Description.Name = "txb_Description"
		Me.txb_Description.Properties.ReadOnly = True
		Me.txb_Description.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txb_Description.Size = New System.Drawing.Size(384, 564)
		Me.txb_Description.TabIndex = 0
		'
		'barmng_Apps
		'
		Me.barmng_Apps.DockControls.Add(Me.barDockControlTop)
		Me.barmng_Apps.DockControls.Add(Me.barDockControlBottom)
		Me.barmng_Apps.DockControls.Add(Me.barDockControlLeft)
		Me.barmng_Apps.DockControls.Add(Me.barDockControlRight)
		Me.barmng_Apps.Form = Me
		Me.barmng_Apps.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.bbi_Run, Me.bbi_Add, Me.bbi_Edit, Me.bbi_Delete, Me.bbi_Reset, Me.bbi_ResetAll})
		Me.barmng_Apps.MaxItemId = 14
		'
		'barDockControlTop
		'
		Me.barDockControlTop.CausesValidation = False
		Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
		Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
		Me.barDockControlTop.Size = New System.Drawing.Size(800, 0)
		'
		'barDockControlBottom
		'
		Me.barDockControlBottom.CausesValidation = False
		Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.barDockControlBottom.Location = New System.Drawing.Point(0, 600)
		Me.barDockControlBottom.Size = New System.Drawing.Size(800, 0)
		'
		'barDockControlLeft
		'
		Me.barDockControlLeft.CausesValidation = False
		Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
		Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
		Me.barDockControlLeft.Size = New System.Drawing.Size(0, 600)
		'
		'barDockControlRight
		'
		Me.barDockControlRight.CausesValidation = False
		Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
		Me.barDockControlRight.Location = New System.Drawing.Point(800, 0)
		Me.barDockControlRight.Size = New System.Drawing.Size(0, 600)
		'
		'bbi_Run
		'
		Me.bbi_Run.Caption = "&Run"
		Me.bbi_Run.Id = 8
		Me.bbi_Run.ImageUri.Uri = "DoubleNext"
		Me.bbi_Run.Name = "bbi_Run"
		'
		'bbi_Add
		'
		Me.bbi_Add.Caption = "&Add"
		Me.bbi_Add.Id = 9
		Me.bbi_Add.ImageUri.Uri = "Add"
		Me.bbi_Add.Name = "bbi_Add"
		'
		'bbi_Edit
		'
		Me.bbi_Edit.Caption = "&Edit"
		Me.bbi_Edit.Id = 10
		Me.bbi_Edit.ImageUri.Uri = "Edit"
		Me.bbi_Edit.Name = "bbi_Edit"
		'
		'bbi_Delete
		'
		Me.bbi_Delete.Caption = "&Delete"
		Me.bbi_Delete.Id = 11
		Me.bbi_Delete.ImageUri.Uri = "Delete"
		Me.bbi_Delete.Name = "bbi_Delete"
		'
		'bbi_Reset
		'
		Me.bbi_Reset.Caption = "Reset &Statistics"
		Me.bbi_Reset.Id = 12
		Me.bbi_Reset.ImageUri.Uri = "Clear"
		Me.bbi_Reset.Name = "bbi_Reset"
		Me.bbi_Reset.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		'
		'bbi_ResetAll
		'
		Me.bbi_ResetAll.Caption = "Reset &all Statistics"
		Me.bbi_ResetAll.Id = 13
		Me.bbi_ResetAll.ImageUri.Uri = "Clear"
		Me.bbi_ResetAll.Name = "bbi_ResetAll"
		Me.bbi_ResetAll.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		'
		'lbl_Displayname
		'
		Me.lbl_Displayname.Appearance.Font = New System.Drawing.Font("Segoe UI", 16.0!)
		Me.lbl_Displayname.AutoEllipsis = True
		Me.lbl_Displayname.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Displayname.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BS_Apps, "DisplayName", True))
		Me.lbl_Displayname.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_Displayname.Location = New System.Drawing.Point(416, 0)
		Me.lbl_Displayname.MKBoundControl1 = Nothing
		Me.lbl_Displayname.MKBoundControl2 = Nothing
		Me.lbl_Displayname.MKBoundControl3 = Nothing
		Me.lbl_Displayname.MKBoundControl4 = Nothing
		Me.lbl_Displayname.MKBoundControl5 = Nothing
		Me.lbl_Displayname.Name = "lbl_Displayname"
		Me.lbl_Displayname.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_Displayname.Size = New System.Drawing.Size(384, 36)
		Me.lbl_Displayname.TabIndex = 0
		'
		'popmnu_Apps
		'
		Me.popmnu_Apps.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Run), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Add, True), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Edit), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Delete), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Reset, True), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_ResetAll)})
		Me.popmnu_Apps.Manager = Me.barmng_Apps
		Me.popmnu_Apps.Name = "popmnu_Apps"
		'
		'ucr_Apps
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.Controls.Add(Me.txb_Description)
		Me.Controls.Add(Me.lbl_Displayname)
		Me.Controls.Add(Me.Ctl_MKDXSplitter1)
		Me.Controls.Add(Me.grd_Apps)
		Me.Controls.Add(Me.barDockControlLeft)
		Me.Controls.Add(Me.barDockControlRight)
		Me.Controls.Add(Me.barDockControlBottom)
		Me.Controls.Add(Me.barDockControlTop)
		Me.Name = "ucr_Apps"
		Me.Size = New System.Drawing.Size(800, 600)
		CType(Me.grd_Apps, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Apps, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_MLApps, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Apps, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_Category, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Categories, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_Description.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.barmng_Apps, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.popmnu_Apps, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents grd_Apps As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_Apps As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents DS_MLApps As Metropolis_Launcher.DS_MLApps
	Friend WithEvents BS_Apps As System.Windows.Forms.BindingSource
	Friend WithEvents BS_Categories As System.Windows.Forms.BindingSource
	Friend WithEvents colid_Categories As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colDisplayName As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents Ctl_MKDXSplitter1 As MKNetDXLib.ctl_MKDXSplitter
	Friend WithEvents lbl_Displayname As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents barmng_Apps As MKNetDXLib.ctl_MKDXBarManager
	Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
	Friend WithEvents popmnu_Apps As MKNetDXLib.cmp_MKDXPopupMenu
	Friend WithEvents bbi_Run As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Add As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Edit As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Delete As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Reset As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_ResetAll As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents txb_Description As MKNetDXLib.ctl_MKDXMemoEdit
	Friend WithEvents rpi_Category As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit

End Class
