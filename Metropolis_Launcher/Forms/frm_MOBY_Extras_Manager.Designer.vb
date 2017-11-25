<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_MOBY_Extras_Manager
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
		Me.splt_Main = New MKNetDXLib.ctl_MKDXSplitPanel()
		Me.grd_Extras = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_Extras = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_ML = New Metropolis_Launcher.DS_ML()
		Me.gv_Extras = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colExtraType = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colShow = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.coltmp_Description = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colSort = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.rpi_Categories = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		Me.prg_Extras_Download = New MKNetDXLib.ctl_MKDXProgressBarControl()
		Me.pic_Game = New MKNetDXLib.ctl_MKDXPictureEdit()
		Me.btn_Cancel = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Move_Down = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Move_Up = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.chb_Download = New MKNetDXLib.ctl_MKDXCheckEdit()
		CType(Me.splt_Main, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splt_Main.SuspendLayout()
		CType(Me.grd_Extras, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Extras, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_Extras, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_Categories, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.prg_Extras_Download.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.pic_Game.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.chb_Download.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'splt_Main
		'
		Me.splt_Main.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.splt_Main.Location = New System.Drawing.Point(3, 3)
		Me.splt_Main.Name = "splt_Main"
		Me.splt_Main.Panel1.Controls.Add(Me.grd_Extras)
		Me.splt_Main.Panel1.Text = "Panel1"
		Me.splt_Main.Panel2.Controls.Add(Me.prg_Extras_Download)
		Me.splt_Main.Panel2.Controls.Add(Me.pic_Game)
		Me.splt_Main.Panel2.Text = "Panel2"
		Me.splt_Main.Size = New System.Drawing.Size(712, 375)
		Me.splt_Main.SplitterPosition = 327
		Me.splt_Main.TabIndex = 1
		Me.splt_Main.Text = "Ctl_MKDXSplitPanel1"
		'
		'grd_Extras
		'
		Me.grd_Extras.DataSource = Me.BS_Extras
		Me.grd_Extras.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_Extras.Location = New System.Drawing.Point(0, 0)
		Me.grd_Extras.MainView = Me.gv_Extras
		Me.grd_Extras.Name = "grd_Extras"
		Me.grd_Extras.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rpi_Categories})
		Me.grd_Extras.Size = New System.Drawing.Size(327, 375)
		Me.grd_Extras.TabIndex = 0
		Me.grd_Extras.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Extras})
		'
		'BS_Extras
		'
		Me.BS_Extras.DataMember = "src_frm_MOBY_Extras_Manager"
		Me.BS_Extras.DataSource = Me.DS_ML
		'
		'DS_ML
		'
		Me.DS_ML.DataSetName = "DS_ML"
		Me.DS_ML.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'gv_Extras
		'
		Me.gv_Extras.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colExtraType, Me.colShow, Me.coltmp_Description, Me.colSort})
		Me.gv_Extras.GridControl = Me.grd_Extras
		Me.gv_Extras.GroupCount = 1
		Me.gv_Extras.Name = "gv_Extras"
		Me.gv_Extras.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_Extras.OptionsView.ShowGroupPanel = False
		Me.gv_Extras.OptionsView.ShowIndicator = False
		Me.gv_Extras.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.colExtraType, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.colSort, DevExpress.Data.ColumnSortOrder.Ascending)})
		'
		'colExtraType
		'
		Me.colExtraType.FieldName = "ExtraType"
		Me.colExtraType.GroupFormat.FormatString = "{1}"
		Me.colExtraType.GroupFormat.FormatType = DevExpress.Utils.FormatType.Custom
		Me.colExtraType.Name = "colExtraType"
		Me.colExtraType.OptionsColumn.AllowEdit = False
		Me.colExtraType.OptionsColumn.ReadOnly = True
		Me.colExtraType.Visible = True
		Me.colExtraType.VisibleIndex = 0
		'
		'colShow
		'
		Me.colShow.FieldName = "Show"
		Me.colShow.MaxWidth = 20
		Me.colShow.Name = "colShow"
		Me.colShow.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
		Me.colShow.ToolTip = "Indicates if the extra will be shown in the Games & Emulation main screen"
		Me.colShow.Visible = True
		Me.colShow.VisibleIndex = 0
		Me.colShow.Width = 41
		'
		'coltmp_Description
		'
		Me.coltmp_Description.Caption = "Description"
		Me.coltmp_Description.FieldName = "tmp_Description"
		Me.coltmp_Description.Name = "coltmp_Description"
		Me.coltmp_Description.OptionsColumn.AllowEdit = False
		Me.coltmp_Description.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
		Me.coltmp_Description.OptionsColumn.ReadOnly = True
		Me.coltmp_Description.Visible = True
		Me.coltmp_Description.VisibleIndex = 1
		Me.coltmp_Description.Width = 1261
		'
		'colSort
		'
		Me.colSort.FieldName = "Sort"
		Me.colSort.Name = "colSort"
		Me.colSort.OptionsColumn.AllowEdit = False
		Me.colSort.OptionsColumn.AllowShowHide = False
		Me.colSort.OptionsColumn.ReadOnly = True
		Me.colSort.OptionsColumn.ShowInCustomizationForm = False
		Me.colSort.Width = 45
		'
		'rpi_Categories
		'
		Me.rpi_Categories.AutoHeight = False
		Me.rpi_Categories.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.rpi_Categories.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_Emu_Extras", "id_Emu_Extras", 94, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name", 37, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Sort", "Sort", 30, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description", "Description", 63, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Hide", "Hide", 31, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Near)})
		Me.rpi_Categories.DisplayMember = "Name"
		Me.rpi_Categories.Name = "rpi_Categories"
		Me.rpi_Categories.ShowHeader = False
		Me.rpi_Categories.ValueMember = "id_Emu_Extras"
		'
		'prg_Extras_Download
		'
		Me.prg_Extras_Download.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.prg_Extras_Download.Location = New System.Drawing.Point(0, 362)
		Me.prg_Extras_Download.Name = "prg_Extras_Download"
		Me.prg_Extras_Download.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid
		Me.prg_Extras_Download.Properties.ShowTitle = True
		Me.prg_Extras_Download.Size = New System.Drawing.Size(380, 13)
		Me.prg_Extras_Download.TabIndex = 7
		Me.prg_Extras_Download.Visible = False
		'
		'pic_Game
		'
		Me.pic_Game.Cursor = System.Windows.Forms.Cursors.Default
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
		Me.pic_Game.Size = New System.Drawing.Size(380, 375)
		ToolTipTitleItem1.Text = "Click to edit"
		ToolTipItem1.LeftIndent = 6
		ToolTipItem1.Text = "Click here to edit the image."
		SuperToolTip1.Items.Add(ToolTipTitleItem1)
		SuperToolTip1.Items.Add(ToolTipItem1)
		Me.pic_Game.SuperTip = SuperToolTip1
		Me.pic_Game.TabIndex = 0
		'
		'btn_Cancel
		'
		Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btn_Cancel.Location = New System.Drawing.Point(640, 381)
		Me.btn_Cancel.Name = "btn_Cancel"
		Me.btn_Cancel.Size = New System.Drawing.Size(75, 23)
		Me.btn_Cancel.TabIndex = 6
		Me.btn_Cancel.Text = "&Cancel"
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.Location = New System.Drawing.Point(562, 381)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 23)
		Me.btn_OK.TabIndex = 5
		Me.btn_OK.Text = "&OK"
		'
		'btn_Move_Down
		'
		Me.btn_Move_Down.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btn_Move_Down.Enabled = False
		Me.btn_Move_Down.Location = New System.Drawing.Point(81, 381)
		Me.btn_Move_Down.Name = "btn_Move_Down"
		Me.btn_Move_Down.Size = New System.Drawing.Size(75, 23)
		Me.btn_Move_Down.TabIndex = 8
		Me.btn_Move_Down.Text = "Move Down"
		'
		'btn_Move_Up
		'
		Me.btn_Move_Up.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btn_Move_Up.Enabled = False
		Me.btn_Move_Up.Location = New System.Drawing.Point(3, 381)
		Me.btn_Move_Up.Name = "btn_Move_Up"
		Me.btn_Move_Up.Size = New System.Drawing.Size(75, 23)
		Me.btn_Move_Up.TabIndex = 7
		Me.btn_Move_Up.Text = "Move Up"
		'
		'chb_Download
		'
		Me.chb_Download.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.chb_Download.Location = New System.Drawing.Point(159, 383)
		Me.chb_Download.MKBoundLabel = Nothing
		Me.chb_Download.MKEditValue_Compare = Nothing
		Me.chb_Download.Name = "chb_Download"
		Me.chb_Download.Properties.Caption = "Download extras when selected"
		Me.chb_Download.Size = New System.Drawing.Size(184, 19)
		Me.chb_Download.TabIndex = 9
		'
		'frm_MOBY_Extras_Manager
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(718, 407)
		Me.Controls.Add(Me.chb_Download)
		Me.Controls.Add(Me.btn_Move_Down)
		Me.Controls.Add(Me.btn_Move_Up)
		Me.Controls.Add(Me.btn_Cancel)
		Me.Controls.Add(Me.btn_OK)
		Me.Controls.Add(Me.splt_Main)
		Me.Name = "frm_MOBY_Extras_Manager"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "MOBY Extras Manager"
		CType(Me.splt_Main, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splt_Main.ResumeLayout(False)
		CType(Me.grd_Extras, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Extras, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Extras, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_Categories, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.prg_Extras_Download.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pic_Game.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.chb_Download.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents splt_Main As MKNetDXLib.ctl_MKDXSplitPanel
	Friend WithEvents grd_Extras As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_Extras As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents rpi_Categories As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
	Friend WithEvents pic_Game As MKNetDXLib.ctl_MKDXPictureEdit
	Friend WithEvents btn_Cancel As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents DS_ML As DS_ML
	Friend WithEvents BS_Extras As BindingSource
	Friend WithEvents colExtraType As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colShow As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents coltmp_Description As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colSort As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents btn_Move_Down As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_Move_Up As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents chb_Download As MKNetDXLib.ctl_MKDXCheckEdit
	Friend WithEvents prg_Extras_Download As MKNetDXLib.ctl_MKDXProgressBarControl
End Class
