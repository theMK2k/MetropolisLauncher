<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Emu_ImageSettings
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
		Me.lbl_Slideshow = New MKNetDXLib.ctl_MKDXLabel()
		Me.chb_Slideshow = New MKNetDXLib.ctl_MKDXCheckEdit()
		Me.lbl_Slideshow_Delay = New MKNetDXLib.ctl_MKDXLabel()
		Me.spn_Slideshow_Delay = New MKNetDXLib.ctl_MKDXSpinEdit()
		Me.grd_ImageOrdering = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_ImageOrdering = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_ML = New Metropolis_Launcher.DS_ML()
		Me.gv_ImageOrdering = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colid_Emu_Extras = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colName = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colSort = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colDescription = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colHide = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.Ctl_MKDXSimpleButton2 = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.gb_Image_Order = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.btn_Down = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Up = New MKNetDXLib.ctl_MKDXSimpleButton()
		CType(Me.chb_Slideshow.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spn_Slideshow_Delay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.grd_ImageOrdering, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_ImageOrdering, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_ImageOrdering, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gb_Image_Order, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_Image_Order.SuspendLayout()
		Me.SuspendLayout()
		'
		'lbl_Slideshow
		'
		Me.lbl_Slideshow.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Slideshow.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Slideshow.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Slideshow.MKBoundControl1 = Nothing
		Me.lbl_Slideshow.MKBoundControl2 = Nothing
		Me.lbl_Slideshow.MKBoundControl3 = Nothing
		Me.lbl_Slideshow.MKBoundControl4 = Nothing
		Me.lbl_Slideshow.MKBoundControl5 = Nothing
		Me.lbl_Slideshow.Name = "lbl_Slideshow"
		Me.lbl_Slideshow.Size = New System.Drawing.Size(84, 20)
		Me.lbl_Slideshow.TabIndex = 0
		Me.lbl_Slideshow.Text = "Slideshow:"
		'
		'chb_Slideshow
		'
		Me.chb_Slideshow.Cursor = System.Windows.Forms.Cursors.Hand
		Me.chb_Slideshow.Location = New System.Drawing.Point(90, 4)
		Me.chb_Slideshow.MKBoundLabel = Nothing
		Me.chb_Slideshow.MKEditValue_Compare = Nothing
		Me.chb_Slideshow.Name = "chb_Slideshow"
		Me.chb_Slideshow.Properties.Caption = ""
		Me.chb_Slideshow.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
		Me.chb_Slideshow.Size = New System.Drawing.Size(20, 19)
		Me.chb_Slideshow.TabIndex = 0
		'
		'lbl_Slideshow_Delay
		'
		Me.lbl_Slideshow_Delay.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Slideshow_Delay.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Slideshow_Delay.Location = New System.Drawing.Point(113, 3)
		Me.lbl_Slideshow_Delay.MKBoundControl1 = Nothing
		Me.lbl_Slideshow_Delay.MKBoundControl2 = Nothing
		Me.lbl_Slideshow_Delay.MKBoundControl3 = Nothing
		Me.lbl_Slideshow_Delay.MKBoundControl4 = Nothing
		Me.lbl_Slideshow_Delay.MKBoundControl5 = Nothing
		Me.lbl_Slideshow_Delay.Name = "lbl_Slideshow_Delay"
		Me.lbl_Slideshow_Delay.Size = New System.Drawing.Size(61, 20)
		Me.lbl_Slideshow_Delay.TabIndex = 0
		Me.lbl_Slideshow_Delay.Text = "Delay:"
		'
		'spn_Slideshow_Delay
		'
		Me.spn_Slideshow_Delay.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
		Me.spn_Slideshow_Delay.Location = New System.Drawing.Point(177, 3)
		Me.spn_Slideshow_Delay.MKBoundLabel = Nothing
		Me.spn_Slideshow_Delay.MKEditValue_Compare = Nothing
		Me.spn_Slideshow_Delay.Name = "spn_Slideshow_Delay"
		Me.spn_Slideshow_Delay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
		Me.spn_Slideshow_Delay.Properties.MaxValue = New Decimal(New Integer() {9999, 0, 0, 0})
		Me.spn_Slideshow_Delay.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
		Me.spn_Slideshow_Delay.Size = New System.Drawing.Size(75, 20)
		Me.spn_Slideshow_Delay.TabIndex = 1
		'
		'grd_ImageOrdering
		'
		Me.grd_ImageOrdering.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.grd_ImageOrdering.DataSource = Me.BS_ImageOrdering
		Me.grd_ImageOrdering.Location = New System.Drawing.Point(4, 23)
		Me.grd_ImageOrdering.MainView = Me.gv_ImageOrdering
		Me.grd_ImageOrdering.Name = "grd_ImageOrdering"
		Me.grd_ImageOrdering.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
		Me.grd_ImageOrdering.Size = New System.Drawing.Size(370, 206)
		Me.grd_ImageOrdering.TabIndex = 0
		Me.grd_ImageOrdering.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_ImageOrdering})
		'
		'BS_ImageOrdering
		'
		Me.BS_ImageOrdering.DataMember = "tbl_Emu_Extras"
		Me.BS_ImageOrdering.DataSource = Me.DS_ML
		Me.BS_ImageOrdering.Sort = "Sort ASC"
		'
		'DS_ML
		'
		Me.DS_ML.DataSetName = "DS_ML"
		Me.DS_ML.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'gv_ImageOrdering
		'
		Me.gv_ImageOrdering.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colid_Emu_Extras, Me.colName, Me.colSort, Me.colDescription, Me.colHide})
		Me.gv_ImageOrdering.GridControl = Me.grd_ImageOrdering
		Me.gv_ImageOrdering.Name = "gv_ImageOrdering"
		Me.gv_ImageOrdering.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_ImageOrdering.OptionsView.ShowGroupPanel = False
		'
		'colid_Emu_Extras
		'
		Me.colid_Emu_Extras.FieldName = "id_Emu_Extras"
		Me.colid_Emu_Extras.Name = "colid_Emu_Extras"
		'
		'colName
		'
		Me.colName.Caption = "Directory"
		Me.colName.FieldName = "Name"
		Me.colName.Name = "colName"
		Me.colName.OptionsColumn.AllowEdit = False
		Me.colName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
		Me.colName.OptionsColumn.ReadOnly = True
		Me.colName.Visible = True
		Me.colName.VisibleIndex = 0
		Me.colName.Width = 134
		'
		'colSort
		'
		Me.colSort.FieldName = "Sort"
		Me.colSort.Name = "colSort"
		'
		'colDescription
		'
		Me.colDescription.FieldName = "Description"
		Me.colDescription.Name = "colDescription"
		Me.colDescription.OptionsColumn.AllowEdit = False
		Me.colDescription.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
		Me.colDescription.OptionsColumn.ReadOnly = True
		Me.colDescription.Visible = True
		Me.colDescription.VisibleIndex = 1
		Me.colDescription.Width = 162
		'
		'colHide
		'
		Me.colHide.ColumnEdit = Me.RepositoryItemCheckEdit1
		Me.colHide.FieldName = "Hide"
		Me.colHide.MaxWidth = 40
		Me.colHide.MinWidth = 40
		Me.colHide.Name = "colHide"
		Me.colHide.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
		Me.colHide.Visible = True
		Me.colHide.VisibleIndex = 2
		Me.colHide.Width = 40
		'
		'RepositoryItemCheckEdit1
		'
		Me.RepositoryItemCheckEdit1.AutoHeight = False
		Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
		Me.RepositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.Location = New System.Drawing.Point(228, 286)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 23)
		Me.btn_OK.TabIndex = 3
		Me.btn_OK.Text = "&OK"
		'
		'Ctl_MKDXSimpleButton2
		'
		Me.Ctl_MKDXSimpleButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Ctl_MKDXSimpleButton2.Location = New System.Drawing.Point(306, 286)
		Me.Ctl_MKDXSimpleButton2.Name = "Ctl_MKDXSimpleButton2"
		Me.Ctl_MKDXSimpleButton2.Size = New System.Drawing.Size(75, 23)
		Me.Ctl_MKDXSimpleButton2.TabIndex = 4
		Me.Ctl_MKDXSimpleButton2.Text = "&Cancel"
		'
		'gb_Image_Order
		'
		Me.gb_Image_Order.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.gb_Image_Order.Controls.Add(Me.grd_ImageOrdering)
		Me.gb_Image_Order.Controls.Add(Me.btn_Down)
		Me.gb_Image_Order.Controls.Add(Me.btn_Up)
		Me.gb_Image_Order.Location = New System.Drawing.Point(3, 26)
		Me.gb_Image_Order.Name = "gb_Image_Order"
		Me.gb_Image_Order.Size = New System.Drawing.Size(378, 257)
		Me.gb_Image_Order.TabIndex = 2
		Me.gb_Image_Order.Text = "Image Order and Visibility"
		'
		'btn_Down
		'
		Me.btn_Down.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btn_Down.Location = New System.Drawing.Point(80, 232)
		Me.btn_Down.Name = "btn_Down"
		Me.btn_Down.Size = New System.Drawing.Size(75, 23)
		Me.btn_Down.TabIndex = 2
		Me.btn_Down.Text = "Move &Down"
		'
		'btn_Up
		'
		Me.btn_Up.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btn_Up.Location = New System.Drawing.Point(2, 232)
		Me.btn_Up.Name = "btn_Up"
		Me.btn_Up.Size = New System.Drawing.Size(75, 23)
		Me.btn_Up.TabIndex = 1
		Me.btn_Up.Text = "Move &Up"
		'
		'frm_Emu_ImageSettings
		'
		Me.ClientSize = New System.Drawing.Size(384, 312)
		Me.Controls.Add(Me.gb_Image_Order)
		Me.Controls.Add(Me.Ctl_MKDXSimpleButton2)
		Me.Controls.Add(Me.btn_OK)
		Me.Controls.Add(Me.spn_Slideshow_Delay)
		Me.Controls.Add(Me.chb_Slideshow)
		Me.Controls.Add(Me.lbl_Slideshow_Delay)
		Me.Controls.Add(Me.lbl_Slideshow)
		Me.MinimumSize = New System.Drawing.Size(400, 350)
		Me.Name = "frm_Emu_ImageSettings"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Viewer Settings"
		CType(Me.chb_Slideshow.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spn_Slideshow_Delay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.grd_ImageOrdering, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_ImageOrdering, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_ImageOrdering, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gb_Image_Order, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_Image_Order.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents lbl_Slideshow As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents chb_Slideshow As MKNetDXLib.ctl_MKDXCheckEdit
	Friend WithEvents lbl_Slideshow_Delay As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents spn_Slideshow_Delay As MKNetDXLib.ctl_MKDXSpinEdit
	Friend WithEvents grd_ImageOrdering As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_ImageOrdering As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents Ctl_MKDXSimpleButton2 As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents gb_Image_Order As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents btn_Down As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_Up As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents DS_ML As Metropolis_Launcher.DS_ML
	Friend WithEvents BS_ImageOrdering As System.Windows.Forms.BindingSource
	Friend WithEvents colid_Emu_Extras As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colName As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colSort As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colDescription As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colHide As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit

End Class
