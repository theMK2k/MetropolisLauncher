<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Moby_Auto_Link
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
		Me.lbl_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		Me.pnl_Main = New MKNetDXLib.ctl_MKDXPanel()
		Me.spltpnl_Main = New MKNetDXLib.ctl_MKDXSplitPanel()
		Me.grd_Moby_Auto_Link = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_Moby_Auto_Link = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_ML = New Metropolis_Launcher.DS_ML()
		Me.abgvMoby_Auto_Link = New DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView()
		Me.GridBand1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
		Me.colApply = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
		Me.rpi_Apply = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
		Me.colMatch_Accuracy = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
		Me.colGameName = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
		Me.colMatch_Moby_Year = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
		Me.colDeveloper1 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
		Me.colGameName_Filtered = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
		Me.colSpacer2 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
		Me.colIdentifier = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
		Me.colSpacer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
		Me.colMatch_Moby_Gamename = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
		Me.colMatch_Moby_created = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
		Me.colPublisher1 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
		Me.colMatch_Moby_Gamename_Filtered = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
		Me.coldeprecated = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
		Me.rpi_Moby_Release = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		Me.rpi_Moby_Platforms_gv1 = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		Me.grd_Moby_Releases = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_Moby_Releases = New System.Windows.Forms.BindingSource(Me.components)
		Me.gv_Moby_Releases = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colPublisher = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colDeveloper = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colHighlighted = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colYear = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colcreated = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colDeprecated1 = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colPlatform = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.lbl_Moby_Releases = New MKNetDXLib.ctl_MKDXLabel()
		Me.btn_Cancel = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.barmng = New MKNetDXLib.ctl_MKDXBarManager()
		Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
		Me.bbi_Open_Moby_Page = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Apply_True = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Apply_False = New DevExpress.XtraBars.BarButtonItem()
		Me.popmnu_Moby_Games = New MKNetDXLib.cmp_MKDXPopupMenu()
		Me.popmnu_Link = New MKNetDXLib.cmp_MKDXPopupMenu()
		CType(Me.pnl_Main, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_Main.SuspendLayout()
		CType(Me.spltpnl_Main, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.spltpnl_Main.SuspendLayout()
		CType(Me.grd_Moby_Auto_Link, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Moby_Auto_Link, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.abgvMoby_Auto_Link, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_Apply, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_Moby_Release, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_Moby_Platforms_gv1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.grd_Moby_Releases, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Moby_Releases, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_Moby_Releases, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.barmng, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.popmnu_Moby_Games, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.popmnu_Link, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'lbl_Explanation
		'
		Me.lbl_Explanation.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Explanation.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
		Me.lbl_Explanation.AutoEllipsis = True
		Me.lbl_Explanation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_Explanation.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_Explanation.Location = New System.Drawing.Point(0, 0)
		Me.lbl_Explanation.MKBoundControl1 = Nothing
		Me.lbl_Explanation.MKBoundControl2 = Nothing
		Me.lbl_Explanation.MKBoundControl3 = Nothing
		Me.lbl_Explanation.MKBoundControl4 = Nothing
		Me.lbl_Explanation.MKBoundControl5 = Nothing
		Me.lbl_Explanation.Name = "lbl_Explanation"
		Me.lbl_Explanation.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_Explanation.Size = New System.Drawing.Size(984, 6)
		Me.lbl_Explanation.TabIndex = 8
		'
		'pnl_Main
		'
		Me.pnl_Main.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_Main.Controls.Add(Me.spltpnl_Main)
		Me.pnl_Main.Controls.Add(Me.btn_Cancel)
		Me.pnl_Main.Controls.Add(Me.btn_OK)
		Me.pnl_Main.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_Main.Location = New System.Drawing.Point(0, 6)
		Me.pnl_Main.Name = "pnl_Main"
		Me.pnl_Main.Size = New System.Drawing.Size(984, 655)
		Me.pnl_Main.TabIndex = 1
		'
		'spltpnl_Main
		'
		Me.spltpnl_Main.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.spltpnl_Main.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2
		Me.spltpnl_Main.Location = New System.Drawing.Point(4, 3)
		Me.spltpnl_Main.Name = "spltpnl_Main"
		Me.spltpnl_Main.Panel1.Controls.Add(Me.grd_Moby_Auto_Link)
		Me.spltpnl_Main.Panel1.Text = "Panel1"
		Me.spltpnl_Main.Panel2.Controls.Add(Me.grd_Moby_Releases)
		Me.spltpnl_Main.Panel2.Controls.Add(Me.lbl_Moby_Releases)
		Me.spltpnl_Main.Panel2.Text = "Panel2"
		Me.spltpnl_Main.Size = New System.Drawing.Size(977, 623)
		Me.spltpnl_Main.SplitterPosition = 324
		Me.spltpnl_Main.TabIndex = 11
		Me.spltpnl_Main.Text = "Ctl_MKDXSplitPanel1"
		'
		'grd_Moby_Auto_Link
		'
		Me.grd_Moby_Auto_Link.DataSource = Me.BS_Moby_Auto_Link
		Me.grd_Moby_Auto_Link.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_Moby_Auto_Link.EmbeddedNavigator.Buttons.Append.Visible = False
		Me.grd_Moby_Auto_Link.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
		Me.grd_Moby_Auto_Link.EmbeddedNavigator.Buttons.Edit.Visible = False
		Me.grd_Moby_Auto_Link.EmbeddedNavigator.Buttons.EndEdit.Visible = False
		Me.grd_Moby_Auto_Link.EmbeddedNavigator.Buttons.Remove.Visible = False
		Me.grd_Moby_Auto_Link.EmbeddedNavigator.TextStringFormat = "{0} of {1}"
		Me.grd_Moby_Auto_Link.Location = New System.Drawing.Point(0, 0)
		Me.grd_Moby_Auto_Link.MainView = Me.abgvMoby_Auto_Link
		Me.grd_Moby_Auto_Link.Name = "grd_Moby_Auto_Link"
		Me.grd_Moby_Auto_Link.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rpi_Moby_Release, Me.rpi_Moby_Platforms_gv1, Me.rpi_Apply})
		Me.grd_Moby_Auto_Link.Size = New System.Drawing.Size(648, 623)
		Me.grd_Moby_Auto_Link.TabIndex = 3
		Me.grd_Moby_Auto_Link.UseEmbeddedNavigator = True
		Me.grd_Moby_Auto_Link.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.abgvMoby_Auto_Link})
		'
		'BS_Moby_Auto_Link
		'
		Me.BS_Moby_Auto_Link.DataMember = "tbl_Moby_Auto_Link"
		Me.BS_Moby_Auto_Link.DataSource = Me.DS_ML
		'
		'DS_ML
		'
		Me.DS_ML.DataSetName = "DS_ML"
		Me.DS_ML.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'abgvMoby_Auto_Link
		'
		Me.abgvMoby_Auto_Link.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.GridBand1})
		Me.abgvMoby_Auto_Link.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {Me.colSpacer2, Me.colApply, Me.colGameName, Me.colGameName_Filtered, Me.colMatch_Accuracy, Me.colMatch_Moby_created, Me.colMatch_Moby_Gamename, Me.colMatch_Moby_Gamename_Filtered, Me.colMatch_Moby_Year, Me.colSpacer, Me.colIdentifier, Me.colDeveloper1, Me.colPublisher1, Me.coldeprecated})
		Me.abgvMoby_Auto_Link.CustomizationFormBounds = New System.Drawing.Rectangle(483, 386, 222, 219)
		Me.abgvMoby_Auto_Link.GridControl = Me.grd_Moby_Auto_Link
		Me.abgvMoby_Auto_Link.Name = "abgvMoby_Auto_Link"
		Me.abgvMoby_Auto_Link.OptionsBehavior.AllowIncrementalSearch = True
		Me.abgvMoby_Auto_Link.OptionsSelection.MultiSelect = True
		Me.abgvMoby_Auto_Link.OptionsView.EnableAppearanceEvenRow = True
		Me.abgvMoby_Auto_Link.OptionsView.EnableAppearanceOddRow = True
		Me.abgvMoby_Auto_Link.OptionsView.ShowGroupPanel = False
		Me.abgvMoby_Auto_Link.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.colMatch_Accuracy, DevExpress.Data.ColumnSortOrder.Ascending)})
		'
		'GridBand1
		'
		Me.GridBand1.Columns.Add(Me.colApply)
		Me.GridBand1.Columns.Add(Me.colMatch_Accuracy)
		Me.GridBand1.Columns.Add(Me.colGameName)
		Me.GridBand1.Columns.Add(Me.colMatch_Moby_Year)
		Me.GridBand1.Columns.Add(Me.colDeveloper1)
		Me.GridBand1.Columns.Add(Me.colGameName_Filtered)
		Me.GridBand1.Columns.Add(Me.colSpacer2)
		Me.GridBand1.Columns.Add(Me.colIdentifier)
		Me.GridBand1.Columns.Add(Me.colSpacer)
		Me.GridBand1.Columns.Add(Me.colMatch_Moby_Gamename)
		Me.GridBand1.Columns.Add(Me.colMatch_Moby_created)
		Me.GridBand1.Columns.Add(Me.colPublisher1)
		Me.GridBand1.Columns.Add(Me.colMatch_Moby_Gamename_Filtered)
		Me.GridBand1.Name = "GridBand1"
		Me.GridBand1.VisibleIndex = 0
		Me.GridBand1.Width = 1404
		'
		'colApply
		'
		Me.colApply.ColumnEdit = Me.rpi_Apply
		Me.colApply.FieldName = "Apply"
		Me.colApply.Name = "colApply"
		Me.colApply.OptionsColumn.AllowEdit = False
		Me.colApply.OptionsColumn.ReadOnly = True
		Me.colApply.ToolTip = "Apply the auto link for this game"
		Me.colApply.Visible = True
		Me.colApply.Width = 41
		'
		'rpi_Apply
		'
		Me.rpi_Apply.AutoHeight = False
		Me.rpi_Apply.Name = "rpi_Apply"
		'
		'colMatch_Accuracy
		'
		Me.colMatch_Accuracy.Caption = "Match Accuracy"
		Me.colMatch_Accuracy.FieldName = "Match_Accuracy"
		Me.colMatch_Accuracy.Name = "colMatch_Accuracy"
		Me.colMatch_Accuracy.OptionsColumn.AllowEdit = False
		Me.colMatch_Accuracy.OptionsColumn.ReadOnly = True
		Me.colMatch_Accuracy.ToolTip = "The accuracy of the game's filtered name compared to the filtered name of the Mob" &
		"y Release"
		Me.colMatch_Accuracy.Visible = True
		Me.colMatch_Accuracy.Width = 103
		'
		'colGameName
		'
		Me.colGameName.Caption = "Gamename"
		Me.colGameName.FieldName = "GameName"
		Me.colGameName.Name = "colGameName"
		Me.colGameName.OptionsColumn.AllowEdit = False
		Me.colGameName.OptionsColumn.ReadOnly = True
		Me.colGameName.ToolTip = "The game's name (derived from filename or directory name)"
		Me.colGameName.Visible = True
		Me.colGameName.Width = 464
		'
		'colMatch_Moby_Year
		'
		Me.colMatch_Moby_Year.Caption = "Moby Year"
		Me.colMatch_Moby_Year.FieldName = "Match_Moby_Year"
		Me.colMatch_Moby_Year.Name = "colMatch_Moby_Year"
		Me.colMatch_Moby_Year.OptionsColumn.AllowEdit = False
		Me.colMatch_Moby_Year.OptionsColumn.ReadOnly = True
		Me.colMatch_Moby_Year.ToolTip = "The year of the game's release according to MobyGames"
		Me.colMatch_Moby_Year.Visible = True
		Me.colMatch_Moby_Year.Width = 159
		'
		'colDeveloper1
		'
		Me.colDeveloper1.FieldName = "Developer"
		Me.colDeveloper1.Name = "colDeveloper1"
		Me.colDeveloper1.OptionsColumn.AllowEdit = False
		Me.colDeveloper1.OptionsColumn.ReadOnly = True
		Me.colDeveloper1.ToolTip = "The Moby Game's development company"
		Me.colDeveloper1.Visible = True
		Me.colDeveloper1.Width = 429
		'
		'colGameName_Filtered
		'
		Me.colGameName_Filtered.Caption = "Gamename (filtered)"
		Me.colGameName_Filtered.FieldName = "GameName_Filtered"
		Me.colGameName_Filtered.Name = "colGameName_Filtered"
		Me.colGameName_Filtered.OptionsColumn.AllowEdit = False
		Me.colGameName_Filtered.OptionsColumn.ReadOnly = True
		Me.colGameName_Filtered.ToolTip = "The game's name after filtering"
		Me.colGameName_Filtered.Visible = True
		Me.colGameName_Filtered.Width = 208
		'
		'colSpacer2
		'
		Me.colSpacer2.Name = "colSpacer2"
		Me.colSpacer2.OptionsColumn.AllowEdit = False
		Me.colSpacer2.OptionsColumn.ReadOnly = True
		Me.colSpacer2.RowIndex = 1
		Me.colSpacer2.Visible = True
		Me.colSpacer2.Width = 41
		'
		'colIdentifier
		'
		Me.colIdentifier.FieldName = "Identifier"
		Me.colIdentifier.Name = "colIdentifier"
		Me.colIdentifier.OptionsColumn.AllowEdit = False
		Me.colIdentifier.OptionsColumn.ReadOnly = True
		Me.colIdentifier.RowIndex = 1
		Me.colIdentifier.ToolTip = "The game's identifier"
		Me.colIdentifier.Visible = True
		Me.colIdentifier.Width = 103
		'
		'colSpacer
		'
		Me.colSpacer.Name = "colSpacer"
		Me.colSpacer.OptionsColumn.AllowEdit = False
		Me.colSpacer.OptionsColumn.ReadOnly = True
		Me.colSpacer.RowIndex = 1
		Me.colSpacer.Width = 103
		'
		'colMatch_Moby_Gamename
		'
		Me.colMatch_Moby_Gamename.Caption = "Moby Release"
		Me.colMatch_Moby_Gamename.FieldName = "Match_Moby_Gamename"
		Me.colMatch_Moby_Gamename.Name = "colMatch_Moby_Gamename"
		Me.colMatch_Moby_Gamename.OptionsColumn.AllowEdit = False
		Me.colMatch_Moby_Gamename.OptionsColumn.ReadOnly = True
		Me.colMatch_Moby_Gamename.RowIndex = 1
		Me.colMatch_Moby_Gamename.ToolTip = "The game's name of the Moby Release"
		Me.colMatch_Moby_Gamename.Visible = True
		Me.colMatch_Moby_Gamename.Width = 464
		'
		'colMatch_Moby_created
		'
		Me.colMatch_Moby_created.Caption = "Moby Created"
		Me.colMatch_Moby_created.FieldName = "Match_Moby_created"
		Me.colMatch_Moby_created.Name = "colMatch_Moby_created"
		Me.colMatch_Moby_created.OptionsColumn.AllowEdit = False
		Me.colMatch_Moby_created.OptionsColumn.ReadOnly = True
		Me.colMatch_Moby_created.RowIndex = 1
		Me.colMatch_Moby_created.ToolTip = "Creation date of the Moby Release"
		Me.colMatch_Moby_created.Visible = True
		Me.colMatch_Moby_created.Width = 159
		'
		'colPublisher1
		'
		Me.colPublisher1.FieldName = "Publisher"
		Me.colPublisher1.Name = "colPublisher1"
		Me.colPublisher1.OptionsColumn.AllowEdit = False
		Me.colPublisher1.OptionsColumn.ReadOnly = True
		Me.colPublisher1.RowIndex = 1
		Me.colPublisher1.ToolTip = "The Moby Game's publishing company"
		Me.colPublisher1.Visible = True
		Me.colPublisher1.Width = 429
		'
		'colMatch_Moby_Gamename_Filtered
		'
		Me.colMatch_Moby_Gamename_Filtered.Caption = "Moby Release (filtered)"
		Me.colMatch_Moby_Gamename_Filtered.FieldName = "Match_Moby_Gamename_Filtered"
		Me.colMatch_Moby_Gamename_Filtered.Name = "colMatch_Moby_Gamename_Filtered"
		Me.colMatch_Moby_Gamename_Filtered.OptionsColumn.AllowEdit = False
		Me.colMatch_Moby_Gamename_Filtered.OptionsColumn.ReadOnly = True
		Me.colMatch_Moby_Gamename_Filtered.RowIndex = 1
		Me.colMatch_Moby_Gamename_Filtered.ToolTip = "The game's name of the Moby Release after filtering"
		Me.colMatch_Moby_Gamename_Filtered.Visible = True
		Me.colMatch_Moby_Gamename_Filtered.Width = 208
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
		'rpi_Moby_Release
		'
		Me.rpi_Moby_Release.AutoHeight = False
		Me.rpi_Moby_Release.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.rpi_Moby_Release.DisplayMember = "Gamename"
		Me.rpi_Moby_Release.Name = "rpi_Moby_Release"
		Me.rpi_Moby_Release.NullText = ""
		Me.rpi_Moby_Release.ValueMember = "id_Moby_Releases"
		'
		'rpi_Moby_Platforms_gv1
		'
		Me.rpi_Moby_Platforms_gv1.AutoHeight = False
		Me.rpi_Moby_Platforms_gv1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.rpi_Moby_Platforms_gv1.DisplayMember = "Display_Name"
		Me.rpi_Moby_Platforms_gv1.Name = "rpi_Moby_Platforms_gv1"
		Me.rpi_Moby_Platforms_gv1.ValueMember = "id_Moby_Platforms"
		'
		'grd_Moby_Releases
		'
		Me.grd_Moby_Releases.DataSource = Me.BS_Moby_Releases
		Me.grd_Moby_Releases.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_Moby_Releases.EmbeddedNavigator.Buttons.Append.Visible = False
		Me.grd_Moby_Releases.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
		Me.grd_Moby_Releases.EmbeddedNavigator.Buttons.Edit.Visible = False
		Me.grd_Moby_Releases.EmbeddedNavigator.Buttons.EndEdit.Visible = False
		Me.grd_Moby_Releases.EmbeddedNavigator.Buttons.Remove.Visible = False
		Me.grd_Moby_Releases.EmbeddedNavigator.TextStringFormat = "{0} of {1}"
		Me.grd_Moby_Releases.Location = New System.Drawing.Point(0, 42)
		Me.grd_Moby_Releases.MainView = Me.gv_Moby_Releases
		Me.grd_Moby_Releases.Name = "grd_Moby_Releases"
		Me.grd_Moby_Releases.Size = New System.Drawing.Size(324, 581)
		Me.grd_Moby_Releases.TabIndex = 4
		Me.grd_Moby_Releases.UseEmbeddedNavigator = True
		Me.grd_Moby_Releases.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Moby_Releases})
		'
		'gv_Moby_Releases
		'
		Me.gv_Moby_Releases.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colPublisher, Me.colDeveloper, Me.GridColumn1, Me.colHighlighted, Me.colYear, Me.colcreated, Me.colDeprecated1, Me.colPlatform})
		Me.gv_Moby_Releases.GridControl = Me.grd_Moby_Releases
		Me.gv_Moby_Releases.Name = "gv_Moby_Releases"
		Me.gv_Moby_Releases.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_Moby_Releases.OptionsView.ColumnAutoWidth = False
		Me.gv_Moby_Releases.OptionsView.ShowGroupPanel = False
		'
		'colPublisher
		'
		Me.colPublisher.Caption = "Publisher"
		Me.colPublisher.FieldName = "Publisher"
		Me.colPublisher.Name = "colPublisher"
		Me.colPublisher.OptionsColumn.AllowEdit = False
		Me.colPublisher.OptionsColumn.ReadOnly = True
		Me.colPublisher.Visible = True
		Me.colPublisher.VisibleIndex = 3
		'
		'colDeveloper
		'
		Me.colDeveloper.Caption = "Developer"
		Me.colDeveloper.FieldName = "Developer"
		Me.colDeveloper.Name = "colDeveloper"
		Me.colDeveloper.OptionsColumn.AllowEdit = False
		Me.colDeveloper.OptionsColumn.ReadOnly = True
		Me.colDeveloper.Visible = True
		Me.colDeveloper.VisibleIndex = 4
		'
		'GridColumn1
		'
		Me.GridColumn1.FieldName = "Gamename"
		Me.GridColumn1.Name = "GridColumn1"
		Me.GridColumn1.OptionsColumn.AllowEdit = False
		Me.GridColumn1.OptionsColumn.ReadOnly = True
		Me.GridColumn1.Visible = True
		Me.GridColumn1.VisibleIndex = 1
		Me.GridColumn1.Width = 151
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
		Me.colYear.VisibleIndex = 2
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
		Me.colcreated.VisibleIndex = 5
		Me.colcreated.Width = 99
		'
		'colDeprecated1
		'
		Me.colDeprecated1.Caption = "deprecated"
		Me.colDeprecated1.FieldName = "deprecated"
		Me.colDeprecated1.Name = "colDeprecated1"
		Me.colDeprecated1.OptionsColumn.AllowEdit = False
		Me.colDeprecated1.OptionsColumn.ReadOnly = True
		Me.colDeprecated1.ToolTip = "indicates that the MobyGame link may be deprecated (nothing really to worry here," &
		" the meta data is still there)"
		Me.colDeprecated1.Visible = True
		Me.colDeprecated1.VisibleIndex = 0
		'
		'colPlatform
		'
		Me.colPlatform.Caption = "Platform"
		Me.colPlatform.FieldName = "Platform"
		Me.colPlatform.Name = "colPlatform"
		Me.colPlatform.OptionsColumn.AllowEdit = False
		Me.colPlatform.OptionsColumn.ReadOnly = True
		'
		'lbl_Moby_Releases
		'
		Me.lbl_Moby_Releases.Appearance.Font = New System.Drawing.Font("Segoe UI", 16.0!)
		Me.lbl_Moby_Releases.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Moby_Releases.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_Moby_Releases.Location = New System.Drawing.Point(0, 0)
		Me.lbl_Moby_Releases.MKBoundControl1 = Nothing
		Me.lbl_Moby_Releases.MKBoundControl2 = Nothing
		Me.lbl_Moby_Releases.MKBoundControl3 = Nothing
		Me.lbl_Moby_Releases.MKBoundControl4 = Nothing
		Me.lbl_Moby_Releases.MKBoundControl5 = Nothing
		Me.lbl_Moby_Releases.Name = "lbl_Moby_Releases"
		Me.lbl_Moby_Releases.Size = New System.Drawing.Size(324, 42)
		Me.lbl_Moby_Releases.TabIndex = 3
		Me.lbl_Moby_Releases.Text = "Moby Releases"
		'
		'btn_Cancel
		'
		Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btn_Cancel.Location = New System.Drawing.Point(906, 629)
		Me.btn_Cancel.Name = "btn_Cancel"
		Me.btn_Cancel.Size = New System.Drawing.Size(75, 23)
		Me.btn_Cancel.TabIndex = 10
		Me.btn_Cancel.Text = "&Cancel"
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btn_OK.Location = New System.Drawing.Point(828, 629)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 23)
		Me.btn_OK.TabIndex = 9
		Me.btn_OK.Text = "&OK"
		'
		'barmng
		'
		Me.barmng.DockControls.Add(Me.barDockControlTop)
		Me.barmng.DockControls.Add(Me.barDockControlBottom)
		Me.barmng.DockControls.Add(Me.barDockControlLeft)
		Me.barmng.DockControls.Add(Me.barDockControlRight)
		Me.barmng.Form = Me
		Me.barmng.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.bbi_Open_Moby_Page, Me.bbi_Apply_True, Me.bbi_Apply_False})
		Me.barmng.MaxItemId = 16
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
		'bbi_Open_Moby_Page
		'
		Me.bbi_Open_Moby_Page.Caption = "&Open Moby Page"
		Me.bbi_Open_Moby_Page.Id = 10
		Me.bbi_Open_Moby_Page.ImageUri.Uri = "NavigationBar"
		Me.bbi_Open_Moby_Page.Name = "bbi_Open_Moby_Page"
		'
		'bbi_Apply_True
		'
		Me.bbi_Apply_True.Caption = "Apply for selected Games"
		Me.bbi_Apply_True.Id = 14
		Me.bbi_Apply_True.ImageUri.Uri = "Apply"
		Me.bbi_Apply_True.Name = "bbi_Apply_True"
		'
		'bbi_Apply_False
		'
		Me.bbi_Apply_False.Caption = "Don't Apply for selected games"
		Me.bbi_Apply_False.Id = 15
		Me.bbi_Apply_False.ImageUri.Uri = "Cancel"
		Me.bbi_Apply_False.Name = "bbi_Apply_False"
		'
		'popmnu_Moby_Games
		'
		Me.popmnu_Moby_Games.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Open_Moby_Page)})
		Me.popmnu_Moby_Games.Manager = Me.barmng
		Me.popmnu_Moby_Games.Name = "popmnu_Moby_Games"
		'
		'popmnu_Link
		'
		Me.popmnu_Link.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Apply_True), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Apply_False)})
		Me.popmnu_Link.Manager = Me.barmng
		Me.popmnu_Link.Name = "popmnu_Link"
		'
		'frm_Moby_Auto_Link
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(984, 661)
		Me.Controls.Add(Me.pnl_Main)
		Me.Controls.Add(Me.lbl_Explanation)
		Me.Controls.Add(Me.barDockControlLeft)
		Me.Controls.Add(Me.barDockControlRight)
		Me.Controls.Add(Me.barDockControlBottom)
		Me.Controls.Add(Me.barDockControlTop)
		Me.Name = "frm_Moby_Auto_Link"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Detect MobyGames Links Preview"
		Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
		CType(Me.pnl_Main, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_Main.ResumeLayout(False)
		CType(Me.spltpnl_Main, System.ComponentModel.ISupportInitialize).EndInit()
		Me.spltpnl_Main.ResumeLayout(False)
		CType(Me.grd_Moby_Auto_Link, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Moby_Auto_Link, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.abgvMoby_Auto_Link, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_Apply, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_Moby_Release, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_Moby_Platforms_gv1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.grd_Moby_Releases, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Moby_Releases, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Moby_Releases, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.barmng, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.popmnu_Moby_Games, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.popmnu_Link, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents pnl_Main As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents DS_ML As DS_ML
	Private WithEvents grd_Moby_Auto_Link As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents rpi_Moby_Platforms_gv1 As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
	Friend WithEvents rpi_Moby_Release As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
	Friend WithEvents BS_Moby_Auto_Link As BindingSource
	Friend WithEvents rpi_Apply As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
	Friend WithEvents btn_Cancel As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents spltpnl_Main As MKNetDXLib.ctl_MKDXSplitPanel
	Friend WithEvents lbl_Moby_Releases As MKNetDXLib.ctl_MKDXLabel
	Private WithEvents grd_Moby_Releases As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents BS_Moby_Releases As BindingSource
	Private WithEvents gv_Moby_Releases As DevExpress.XtraGrid.Views.Grid.GridView
	Private WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colHighlighted As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colYear As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colcreated As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents lbl_Explanation As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents abgvMoby_Auto_Link As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
	Friend WithEvents colApply As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
	Friend WithEvents colGameName As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
	Friend WithEvents colGameName_Filtered As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
	Friend WithEvents colMatch_Accuracy As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
	Friend WithEvents colMatch_Moby_created As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
	Friend WithEvents colMatch_Moby_Gamename As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
	Friend WithEvents colMatch_Moby_Gamename_Filtered As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
	Friend WithEvents colMatch_Moby_Year As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
	Friend WithEvents colSpacer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
	Friend WithEvents colSpacer2 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
	Friend WithEvents barmng As MKNetDXLib.ctl_MKDXBarManager
	Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
	Friend WithEvents bbi_Open_Moby_Page As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents popmnu_Moby_Games As MKNetDXLib.cmp_MKDXPopupMenu
	Friend WithEvents GridBand1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
	Friend WithEvents colIdentifier As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
	Friend WithEvents colPublisher As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colDeveloper As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents bbi_Apply_True As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Apply_False As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents colDeveloper1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
	Friend WithEvents colPublisher1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
	Friend WithEvents popmnu_Link As MKNetDXLib.cmp_MKDXPopupMenu
	Friend WithEvents coldeprecated As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
	Friend WithEvents colDeprecated1 As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colPlatform As DevExpress.XtraGrid.Columns.GridColumn
End Class
