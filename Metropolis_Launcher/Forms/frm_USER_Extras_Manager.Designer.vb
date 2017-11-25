<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_USER_Extras_Manager
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
		Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Me.splt_Main = New MKNetDXLib.ctl_MKDXSplitPanel()
		Me.grd_Screenshots = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_Screenshots = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_Screenshots = New System.Data.DataSet()
		Me.tbl_Screenshots = New System.Data.DataTable()
		Me.DataColumn1 = New System.Data.DataColumn()
		Me.DataColumn2 = New System.Data.DataColumn()
		Me.DataColumn3 = New System.Data.DataColumn()
		Me.DataColumn4 = New System.Data.DataColumn()
		Me.DataColumn5 = New System.Data.DataColumn()
		Me.gv_Screenshots = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colUse = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colDisplaytext = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colCategory = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.rpi_Categories = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		Me.BS_Emu_Extras = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_ML = New Metropolis_Launcher.DS_ML()
		Me.pic_Game = New MKNetDXLib.ctl_MKDXPictureEdit()
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Cancel = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Move_Up = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Move_Down = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.ddb_Add = New MKNetDXLib.ctl_MKDXDropDownButton()
		Me.popmnu_Add = New MKNetDXLib.cmp_MKDXPopupMenu()
		Me.bbi_Add_from_Files = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Add_from_Clipboard = New DevExpress.XtraBars.BarButtonItem()
		Me.barmng_Add = New MKNetDXLib.ctl_MKDXBarManager()
		Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
		Me.BarDockControl2 = New DevExpress.XtraBars.BarDockControl()
		Me.BarDockControl3 = New DevExpress.XtraBars.BarDockControl()
		Me.BarDockControl4 = New DevExpress.XtraBars.BarDockControl()
		Me.bbi_GameGroup_Info = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_GameGroup_Filter = New DevExpress.XtraBars.BarButtonItem()
		CType(Me.splt_Main, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splt_Main.SuspendLayout()
		CType(Me.grd_Screenshots, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Screenshots, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_Screenshots, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.tbl_Screenshots, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_Screenshots, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_Categories, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Emu_Extras, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.pic_Game.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.popmnu_Add, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.barmng_Add, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'splt_Main
		'
		Me.splt_Main.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.splt_Main.Location = New System.Drawing.Point(3, 3)
		Me.splt_Main.Name = "splt_Main"
		Me.splt_Main.Panel1.Controls.Add(Me.grd_Screenshots)
		Me.splt_Main.Panel1.Text = "Panel1"
		Me.splt_Main.Panel2.Controls.Add(Me.pic_Game)
		Me.splt_Main.Panel2.Text = "Panel2"
		Me.splt_Main.Size = New System.Drawing.Size(651, 337)
		Me.splt_Main.SplitterPosition = 280
		Me.splt_Main.TabIndex = 0
		Me.splt_Main.Text = "Ctl_MKDXSplitPanel1"
		'
		'grd_Screenshots
		'
		Me.grd_Screenshots.DataSource = Me.BS_Screenshots
		Me.grd_Screenshots.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_Screenshots.Location = New System.Drawing.Point(0, 0)
		Me.grd_Screenshots.MainView = Me.gv_Screenshots
		Me.grd_Screenshots.Name = "grd_Screenshots"
		Me.grd_Screenshots.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rpi_Categories})
		Me.grd_Screenshots.Size = New System.Drawing.Size(280, 337)
		Me.grd_Screenshots.TabIndex = 0
		Me.grd_Screenshots.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Screenshots})
		'
		'BS_Screenshots
		'
		Me.BS_Screenshots.DataMember = "tbl_Screenshots"
		Me.BS_Screenshots.DataSource = Me.DS_Screenshots
		Me.BS_Screenshots.Sort = "Sort"
		'
		'DS_Screenshots
		'
		Me.DS_Screenshots.DataSetName = "NewDataSet"
		Me.DS_Screenshots.Tables.AddRange(New System.Data.DataTable() {Me.tbl_Screenshots})
		'
		'tbl_Screenshots
		'
		Me.tbl_Screenshots.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn1, Me.DataColumn2, Me.DataColumn3, Me.DataColumn4, Me.DataColumn5})
		Me.tbl_Screenshots.TableName = "tbl_Screenshots"
		'
		'DataColumn1
		'
		Me.DataColumn1.AllowDBNull = False
		Me.DataColumn1.AutoIncrement = True
		Me.DataColumn1.ColumnName = "id"
		Me.DataColumn1.DataType = GetType(Integer)
		'
		'DataColumn2
		'
		Me.DataColumn2.ColumnName = "Use"
		Me.DataColumn2.DataType = GetType(Boolean)
		Me.DataColumn2.DefaultValue = True
		'
		'DataColumn3
		'
		Me.DataColumn3.ColumnName = "Category"
		Me.DataColumn3.DataType = GetType(Long)
		Me.DataColumn3.DefaultValue = CType(4, Long)
		'
		'DataColumn4
		'
		Me.DataColumn4.ColumnName = "Displaytext"
		Me.DataColumn4.Expression = "'Image ' + id"
		Me.DataColumn4.ReadOnly = True
		'
		'DataColumn5
		'
		Me.DataColumn5.AllowDBNull = False
		Me.DataColumn5.AutoIncrement = True
		Me.DataColumn5.AutoIncrementSeed = CType(1, Long)
		Me.DataColumn5.ColumnName = "Sort"
		Me.DataColumn5.DataType = GetType(Integer)
		'
		'gv_Screenshots
		'
		Me.gv_Screenshots.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colUse, Me.colDisplaytext, Me.colCategory})
		Me.gv_Screenshots.GridControl = Me.grd_Screenshots
		Me.gv_Screenshots.Name = "gv_Screenshots"
		Me.gv_Screenshots.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_Screenshots.OptionsView.ShowColumnHeaders = False
		Me.gv_Screenshots.OptionsView.ShowGroupPanel = False
		Me.gv_Screenshots.OptionsView.ShowIndicator = False
		'
		'colUse
		'
		Me.colUse.FieldName = "Use"
		Me.colUse.Name = "colUse"
		Me.colUse.Visible = True
		Me.colUse.VisibleIndex = 0
		'
		'colDisplaytext
		'
		Me.colDisplaytext.FieldName = "Displaytext"
		Me.colDisplaytext.Name = "colDisplaytext"
		Me.colDisplaytext.OptionsColumn.AllowEdit = False
		Me.colDisplaytext.OptionsColumn.ReadOnly = True
		Me.colDisplaytext.Visible = True
		Me.colDisplaytext.VisibleIndex = 1
		Me.colDisplaytext.Width = 710
		'
		'colCategory
		'
		Me.colCategory.ColumnEdit = Me.rpi_Categories
		Me.colCategory.FieldName = "Category"
		Me.colCategory.Name = "colCategory"
		Me.colCategory.Visible = True
		Me.colCategory.VisibleIndex = 2
		Me.colCategory.Width = 709
		'
		'rpi_Categories
		'
		Me.rpi_Categories.AutoHeight = False
		Me.rpi_Categories.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.rpi_Categories.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_Emu_Extras", "id_Emu_Extras", 94, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name", 37, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Sort", "Sort", 30, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description", "Description", 63, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Hide", "Hide", 31, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Near)})
		Me.rpi_Categories.DataSource = Me.BS_Emu_Extras
		Me.rpi_Categories.DisplayMember = "Name"
		Me.rpi_Categories.Name = "rpi_Categories"
		Me.rpi_Categories.ShowHeader = False
		Me.rpi_Categories.ValueMember = "id_Emu_Extras"
		'
		'BS_Emu_Extras
		'
		Me.BS_Emu_Extras.DataMember = "tbl_Emu_Extras"
		Me.BS_Emu_Extras.DataSource = Me.DS_ML
		'
		'DS_ML
		'
		Me.DS_ML.DataSetName = "DS_ML"
		Me.DS_ML.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'pic_Game
		'
		Me.pic_Game.Cursor = System.Windows.Forms.Cursors.Hand
		Me.pic_Game.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pic_Game.Location = New System.Drawing.Point(0, 0)
		Me.pic_Game.Name = "pic_Game"
		Me.pic_Game.Properties.AllowFocused = False
		Me.pic_Game.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Transparent
		Me.pic_Game.Properties.AppearanceFocused.Options.UseBorderColor = True
		Me.pic_Game.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pic_Game.Properties.ErrorImage = Nothing
		Me.pic_Game.Properties.InitialImage = Nothing
		Me.pic_Game.Properties.NullText = " "
		Me.pic_Game.Properties.PictureInterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
		Me.pic_Game.Properties.ReadOnly = True
		Me.pic_Game.Properties.ShowMenu = False
		Me.pic_Game.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.[True]
		Me.pic_Game.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
		Me.pic_Game.Size = New System.Drawing.Size(366, 337)
		ToolTipTitleItem1.Text = "Click to edit"
		ToolTipItem1.LeftIndent = 6
		ToolTipItem1.Text = "Click here to edit the image."
		SuperToolTip1.Items.Add(ToolTipTitleItem1)
		SuperToolTip1.Items.Add(ToolTipItem1)
		Me.pic_Game.SuperTip = SuperToolTip1
		Me.pic_Game.TabIndex = 0
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btn_OK.Location = New System.Drawing.Point(501, 343)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 23)
		Me.btn_OK.TabIndex = 3
		Me.btn_OK.Text = "&OK"
		'
		'btn_Cancel
		'
		Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btn_Cancel.Location = New System.Drawing.Point(579, 343)
		Me.btn_Cancel.Name = "btn_Cancel"
		Me.btn_Cancel.Size = New System.Drawing.Size(75, 23)
		Me.btn_Cancel.TabIndex = 4
		Me.btn_Cancel.Text = "&Cancel"
		'
		'btn_Move_Up
		'
		Me.btn_Move_Up.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btn_Move_Up.Enabled = False
		Me.btn_Move_Up.Location = New System.Drawing.Point(3, 343)
		Me.btn_Move_Up.Name = "btn_Move_Up"
		Me.btn_Move_Up.Size = New System.Drawing.Size(75, 23)
		Me.btn_Move_Up.TabIndex = 0
		Me.btn_Move_Up.Text = "Move Up"
		'
		'btn_Move_Down
		'
		Me.btn_Move_Down.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btn_Move_Down.Enabled = False
		Me.btn_Move_Down.Location = New System.Drawing.Point(81, 343)
		Me.btn_Move_Down.Name = "btn_Move_Down"
		Me.btn_Move_Down.Size = New System.Drawing.Size(75, 23)
		Me.btn_Move_Down.TabIndex = 1
		Me.btn_Move_Down.Text = "Move Down"
		'
		'ddb_Add
		'
		Me.ddb_Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.ddb_Add.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Show
		Me.ddb_Add.DropDownControl = Me.popmnu_Add
		Me.ddb_Add.Location = New System.Drawing.Point(159, 344)
		Me.ddb_Add.Name = "ddb_Add"
		Me.ddb_Add.Size = New System.Drawing.Size(86, 22)
		Me.ddb_Add.TabIndex = 2
		Me.ddb_Add.Text = "Add Image"
		'
		'popmnu_Add
		'
		Me.popmnu_Add.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Add_from_Files), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Add_from_Clipboard)})
		Me.popmnu_Add.Manager = Me.barmng_Add
		Me.popmnu_Add.Name = "popmnu_Add"
		'
		'bbi_Add_from_Files
		'
		Me.bbi_Add_from_Files.Caption = "from File/s"
		Me.bbi_Add_from_Files.Id = 2
		Me.bbi_Add_from_Files.ImageUri.Uri = "Open"
		Me.bbi_Add_from_Files.Name = "bbi_Add_from_Files"
		'
		'bbi_Add_from_Clipboard
		'
		Me.bbi_Add_from_Clipboard.Caption = "from Clipboard"
		Me.bbi_Add_from_Clipboard.Id = 3
		Me.bbi_Add_from_Clipboard.ImageUri.Uri = "Paste"
		Me.bbi_Add_from_Clipboard.Name = "bbi_Add_from_Clipboard"
		'
		'barmng_Add
		'
		Me.barmng_Add.DockControls.Add(Me.BarDockControl1)
		Me.barmng_Add.DockControls.Add(Me.BarDockControl2)
		Me.barmng_Add.DockControls.Add(Me.BarDockControl3)
		Me.barmng_Add.DockControls.Add(Me.BarDockControl4)
		Me.barmng_Add.Form = Me
		Me.barmng_Add.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.bbi_GameGroup_Info, Me.bbi_GameGroup_Filter, Me.bbi_Add_from_Files, Me.bbi_Add_from_Clipboard})
		Me.barmng_Add.MaxItemId = 4
		'
		'BarDockControl1
		'
		Me.BarDockControl1.CausesValidation = False
		Me.BarDockControl1.Dock = System.Windows.Forms.DockStyle.Top
		Me.BarDockControl1.Location = New System.Drawing.Point(0, 0)
		Me.BarDockControl1.Size = New System.Drawing.Size(657, 0)
		'
		'BarDockControl2
		'
		Me.BarDockControl2.CausesValidation = False
		Me.BarDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.BarDockControl2.Location = New System.Drawing.Point(0, 369)
		Me.BarDockControl2.Size = New System.Drawing.Size(657, 0)
		'
		'BarDockControl3
		'
		Me.BarDockControl3.CausesValidation = False
		Me.BarDockControl3.Dock = System.Windows.Forms.DockStyle.Left
		Me.BarDockControl3.Location = New System.Drawing.Point(0, 0)
		Me.BarDockControl3.Size = New System.Drawing.Size(0, 369)
		'
		'BarDockControl4
		'
		Me.BarDockControl4.CausesValidation = False
		Me.BarDockControl4.Dock = System.Windows.Forms.DockStyle.Right
		Me.BarDockControl4.Location = New System.Drawing.Point(657, 0)
		Me.BarDockControl4.Size = New System.Drawing.Size(0, 369)
		'
		'bbi_GameGroup_Info
		'
		Me.bbi_GameGroup_Info.AllowRightClickInMenu = False
		Me.bbi_GameGroup_Info.Caption = "&Group Info"
		Me.bbi_GameGroup_Info.Id = 0
		Me.bbi_GameGroup_Info.Name = "bbi_GameGroup_Info"
		'
		'bbi_GameGroup_Filter
		'
		Me.bbi_GameGroup_Filter.AllowRightClickInMenu = False
		Me.bbi_GameGroup_Filter.Caption = "&Filter by this group"
		Me.bbi_GameGroup_Filter.Id = 1
		Me.bbi_GameGroup_Filter.Name = "bbi_GameGroup_Filter"
		'
		'frm_Emu_Game_Screenshotviewer
		'
		Me.ClientSize = New System.Drawing.Size(657, 369)
		Me.Controls.Add(Me.ddb_Add)
		Me.Controls.Add(Me.btn_Move_Down)
		Me.Controls.Add(Me.btn_Move_Up)
		Me.Controls.Add(Me.btn_Cancel)
		Me.Controls.Add(Me.btn_OK)
		Me.Controls.Add(Me.splt_Main)
		Me.Controls.Add(Me.BarDockControl3)
		Me.Controls.Add(Me.BarDockControl4)
		Me.Controls.Add(Me.BarDockControl2)
		Me.Controls.Add(Me.BarDockControl1)
		Me.Name = "frm_Emu_Game_Screenshotviewer"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		CType(Me.splt_Main, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splt_Main.ResumeLayout(False)
		CType(Me.grd_Screenshots, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Screenshots, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_Screenshots, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.tbl_Screenshots, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Screenshots, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_Categories, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Emu_Extras, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pic_Game.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.popmnu_Add, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.barmng_Add, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents splt_Main As MKNetDXLib.ctl_MKDXSplitPanel
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_Cancel As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents grd_Screenshots As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_Screenshots As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents pic_Game As MKNetDXLib.ctl_MKDXPictureEdit
	Friend WithEvents BS_Screenshots As System.Windows.Forms.BindingSource
	Friend WithEvents tbl_Screenshots As System.Data.DataTable
	Friend WithEvents DataColumn1 As System.Data.DataColumn
	Friend WithEvents DataColumn2 As System.Data.DataColumn
	Friend WithEvents DataColumn3 As System.Data.DataColumn
	Friend WithEvents DataColumn4 As System.Data.DataColumn
	Friend WithEvents colUse As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colDisplaytext As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colCategory As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents DS_ML As Metropolis_Launcher.DS_ML
	Friend WithEvents rpi_Categories As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
	Friend WithEvents BS_Emu_Extras As System.Windows.Forms.BindingSource
	Public WithEvents DS_Screenshots As System.Data.DataSet
	Friend WithEvents DataColumn5 As DataColumn
	Friend WithEvents btn_Move_Up As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_Move_Down As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents ddb_Add As MKNetDXLib.ctl_MKDXDropDownButton
	Private WithEvents barmng_Add As MKNetDXLib.ctl_MKDXBarManager
	Private WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
	Private WithEvents BarDockControl2 As DevExpress.XtraBars.BarDockControl
	Private WithEvents BarDockControl3 As DevExpress.XtraBars.BarDockControl
	Private WithEvents BarDockControl4 As DevExpress.XtraBars.BarDockControl
	Private WithEvents bbi_GameGroup_Info As DevExpress.XtraBars.BarButtonItem
	Private WithEvents bbi_GameGroup_Filter As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Add_from_Files As DevExpress.XtraBars.BarButtonItem
	Private WithEvents popmnu_Add As MKNetDXLib.cmp_MKDXPopupMenu
	Friend WithEvents bbi_Add_from_Clipboard As DevExpress.XtraBars.BarButtonItem
End Class
