<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Cheevo_Challenges_Edit
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
		Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
		Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
		Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
		Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
		Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
		Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
		Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
		Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
		Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem2 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Me.lbl_Challenge = New MKNetDXLib.ctl_MKDXLabel()
		Me.cmb_Challenges = New MKNetDXLib.ctl_MKDXLookupEdit()
		Me.BS_Cheevo_Challenges = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_ML = New Metropolis_Launcher.DS_ML()
		Me.BS_Cheevo_Challenges_Cheevos = New System.Windows.Forms.BindingSource(Me.components)
		Me.grd_Cheevos = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_Cheevos = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colTier = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.rpiTier = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
		Me.colHardcore = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.rpiHardcore = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
		Me.colCheevo_GameName = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colCheevo_Title = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colCheevo_Description = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.btn_Close = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Save = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.barmng = New MKNetDXLib.ctl_MKDXBarManager()
		Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
		Me.bbi_Cheevo_Remove = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Cheevo_Move = New DevExpress.XtraBars.BarButtonItem()
		Me.popmnu_Cheevos = New MKNetDXLib.cmp_MKDXPopupMenu()
		CType(Me.cmb_Challenges.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Cheevo_Challenges, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Cheevo_Challenges_Cheevos, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.grd_Cheevos, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_Cheevos, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpiTier, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpiHardcore, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.barmng, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.popmnu_Cheevos, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'lbl_Challenge
		'
		Me.lbl_Challenge.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Challenge.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Challenge.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Challenge.MKBoundControl1 = Nothing
		Me.lbl_Challenge.MKBoundControl2 = Nothing
		Me.lbl_Challenge.MKBoundControl3 = Nothing
		Me.lbl_Challenge.MKBoundControl4 = Nothing
		Me.lbl_Challenge.MKBoundControl5 = Nothing
		Me.lbl_Challenge.Name = "lbl_Challenge"
		Me.lbl_Challenge.Size = New System.Drawing.Size(77, 20)
		Me.lbl_Challenge.TabIndex = 5
		Me.lbl_Challenge.Text = "Challenge:"
		'
		'cmb_Challenges
		'
		Me.cmb_Challenges.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmb_Challenges.Location = New System.Drawing.Point(81, 3)
		Me.cmb_Challenges.MKBoundLabel = Nothing
		Me.cmb_Challenges.MKEditValue_Compare = Nothing
		Me.cmb_Challenges.Name = "cmb_Challenges"
		ToolTipTitleItem1.Text = "Add new Challenge"
		SuperToolTip1.Items.Add(ToolTipTitleItem1)
		ToolTipTitleItem2.Text = "Delete this Challenge"
		SuperToolTip2.Items.Add(ToolTipTitleItem2)
		Me.cmb_Challenges.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, SuperToolTip1, True), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Minus, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", Nothing, SuperToolTip2, True)})
		Me.cmb_Challenges.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_Cheevo_Challenges", "id_Cheevo_Challenges", 5, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name", 5, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("created", "created", 5, DevExpress.Utils.FormatType.DateTime, "dd.MM.yyyy", False, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("updated", "updated", 5, DevExpress.Utils.FormatType.DateTime, "dd.MM.yyyy", False, DevExpress.Utils.HorzAlignment.Near)})
		Me.cmb_Challenges.Properties.DataSource = Me.BS_Cheevo_Challenges
		Me.cmb_Challenges.Properties.DisplayMember = "Name"
		Me.cmb_Challenges.Properties.NullText = "<please choose>"
		Me.cmb_Challenges.Properties.ShowFooter = False
		Me.cmb_Challenges.Properties.ShowHeader = False
		Me.cmb_Challenges.Properties.ValueMember = "id_Cheevo_Challenges"
		Me.cmb_Challenges.Size = New System.Drawing.Size(762, 20)
		Me.cmb_Challenges.TabIndex = 4
		'
		'BS_Cheevo_Challenges
		'
		Me.BS_Cheevo_Challenges.DataMember = "tbl_Cheevo_Challenges"
		Me.BS_Cheevo_Challenges.DataSource = Me.DS_ML
		'
		'DS_ML
		'
		Me.DS_ML.DataSetName = "DS_ML"
		Me.DS_ML.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'BS_Cheevo_Challenges_Cheevos
		'
		Me.BS_Cheevo_Challenges_Cheevos.DataMember = "tbl_Cheevo_Challenges_Cheevos"
		Me.BS_Cheevo_Challenges_Cheevos.DataSource = Me.DS_ML
		Me.BS_Cheevo_Challenges_Cheevos.Filter = ""
		'
		'grd_Cheevos
		'
		Me.grd_Cheevos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.grd_Cheevos.DataSource = Me.BS_Cheevo_Challenges_Cheevos
		Me.grd_Cheevos.Location = New System.Drawing.Point(3, 26)
		Me.grd_Cheevos.MainView = Me.gv_Cheevos
		Me.grd_Cheevos.Name = "grd_Cheevos"
		Me.grd_Cheevos.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rpiTier, Me.rpiHardcore})
		Me.grd_Cheevos.Size = New System.Drawing.Size(840, 333)
		Me.grd_Cheevos.TabIndex = 6
		Me.grd_Cheevos.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Cheevos})
		'
		'gv_Cheevos
		'
		Me.gv_Cheevos.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colTier, Me.colHardcore, Me.colCheevo_GameName, Me.colCheevo_Title, Me.colCheevo_Description})
		Me.gv_Cheevos.GridControl = Me.grd_Cheevos
		Me.gv_Cheevos.Name = "gv_Cheevos"
		Me.gv_Cheevos.OptionsView.ColumnAutoWidth = False
		Me.gv_Cheevos.OptionsView.ShowGroupPanel = False
		Me.gv_Cheevos.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.colTier, DevExpress.Data.ColumnSortOrder.Ascending)})
		'
		'colTier
		'
		Me.colTier.ColumnEdit = Me.rpiTier
		Me.colTier.FieldName = "Tier"
		Me.colTier.Name = "colTier"
		Me.colTier.Visible = True
		Me.colTier.VisibleIndex = 0
		Me.colTier.Width = 49
		'
		'rpiTier
		'
		Me.rpiTier.AutoHeight = False
		Me.rpiTier.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.rpiTier.IsFloatValue = False
		Me.rpiTier.Mask.EditMask = "N00"
		Me.rpiTier.MaxValue = New Decimal(New Integer() {99, 0, 0, 0})
		Me.rpiTier.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
		Me.rpiTier.Name = "rpiTier"
		'
		'colHardcore
		'
		Me.colHardcore.ColumnEdit = Me.rpiHardcore
		Me.colHardcore.FieldName = "Hardcore"
		Me.colHardcore.Name = "colHardcore"
		Me.colHardcore.Visible = True
		Me.colHardcore.VisibleIndex = 1
		'
		'rpiHardcore
		'
		Me.rpiHardcore.AutoHeight = False
		Me.rpiHardcore.Name = "rpiHardcore"
		'
		'colCheevo_GameName
		'
		Me.colCheevo_GameName.Caption = "Game"
		Me.colCheevo_GameName.FieldName = "Cheevo_GameName"
		Me.colCheevo_GameName.Name = "colCheevo_GameName"
		Me.colCheevo_GameName.OptionsColumn.AllowEdit = False
		Me.colCheevo_GameName.OptionsColumn.ReadOnly = True
		Me.colCheevo_GameName.Visible = True
		Me.colCheevo_GameName.VisibleIndex = 2
		Me.colCheevo_GameName.Width = 179
		'
		'colCheevo_Title
		'
		Me.colCheevo_Title.Caption = "Achievement Title"
		Me.colCheevo_Title.FieldName = "Cheevo_Title"
		Me.colCheevo_Title.Name = "colCheevo_Title"
		Me.colCheevo_Title.OptionsColumn.AllowEdit = False
		Me.colCheevo_Title.OptionsColumn.ReadOnly = True
		Me.colCheevo_Title.Visible = True
		Me.colCheevo_Title.VisibleIndex = 3
		Me.colCheevo_Title.Width = 152
		'
		'colCheevo_Description
		'
		Me.colCheevo_Description.Caption = "Achievement Description"
		Me.colCheevo_Description.FieldName = "Cheevo_Description"
		Me.colCheevo_Description.Name = "colCheevo_Description"
		Me.colCheevo_Description.OptionsColumn.AllowEdit = False
		Me.colCheevo_Description.OptionsColumn.ReadOnly = True
		Me.colCheevo_Description.Visible = True
		Me.colCheevo_Description.VisibleIndex = 4
		Me.colCheevo_Description.Width = 365
		'
		'btn_Close
		'
		Me.btn_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Close.Location = New System.Drawing.Point(768, 362)
		Me.btn_Close.Name = "btn_Close"
		Me.btn_Close.Size = New System.Drawing.Size(75, 23)
		Me.btn_Close.TabIndex = 12
		Me.btn_Close.Text = "&Close"
		'
		'btn_Save
		'
		Me.btn_Save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btn_Save.Location = New System.Drawing.Point(3, 362)
		Me.btn_Save.Name = "btn_Save"
		Me.btn_Save.Size = New System.Drawing.Size(75, 23)
		Me.btn_Save.TabIndex = 11
		Me.btn_Save.Text = "&Save"
		'
		'barmng
		'
		Me.barmng.DockControls.Add(Me.barDockControlTop)
		Me.barmng.DockControls.Add(Me.barDockControlBottom)
		Me.barmng.DockControls.Add(Me.barDockControlLeft)
		Me.barmng.DockControls.Add(Me.barDockControlRight)
		Me.barmng.Form = Me
		Me.barmng.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.bbi_Cheevo_Remove, Me.bbi_Cheevo_Move})
		Me.barmng.MaxItemId = 2
		'
		'barDockControlTop
		'
		Me.barDockControlTop.CausesValidation = False
		Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
		Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
		Me.barDockControlTop.Size = New System.Drawing.Size(846, 0)
		'
		'barDockControlBottom
		'
		Me.barDockControlBottom.CausesValidation = False
		Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.barDockControlBottom.Location = New System.Drawing.Point(0, 388)
		Me.barDockControlBottom.Size = New System.Drawing.Size(846, 0)
		'
		'barDockControlLeft
		'
		Me.barDockControlLeft.CausesValidation = False
		Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
		Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
		Me.barDockControlLeft.Size = New System.Drawing.Size(0, 388)
		'
		'barDockControlRight
		'
		Me.barDockControlRight.CausesValidation = False
		Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
		Me.barDockControlRight.Location = New System.Drawing.Point(846, 0)
		Me.barDockControlRight.Size = New System.Drawing.Size(0, 388)
		'
		'bbi_Cheevo_Remove
		'
		Me.bbi_Cheevo_Remove.Caption = "Remove Achievement from Challenge"
		Me.bbi_Cheevo_Remove.Id = 0
		Me.bbi_Cheevo_Remove.ImageUri.Uri = "Delete"
		Me.bbi_Cheevo_Remove.Name = "bbi_Cheevo_Remove"
		'
		'bbi_Cheevo_Move
		'
		Me.bbi_Cheevo_Move.Caption = "Move Achievement to another Challenge"
		Me.bbi_Cheevo_Move.Id = 1
		Me.bbi_Cheevo_Move.ImageUri.Uri = "Replace"
		Me.bbi_Cheevo_Move.Name = "bbi_Cheevo_Move"
		'
		'popmnu_Cheevos
		'
		Me.popmnu_Cheevos.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Cheevo_Remove), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Cheevo_Move)})
		Me.popmnu_Cheevos.Manager = Me.barmng
		Me.popmnu_Cheevos.Name = "popmnu_Cheevos"
		'
		'frm_Cheevo_Challenges_Edit
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(846, 388)
		Me.Controls.Add(Me.btn_Close)
		Me.Controls.Add(Me.btn_Save)
		Me.Controls.Add(Me.grd_Cheevos)
		Me.Controls.Add(Me.lbl_Challenge)
		Me.Controls.Add(Me.cmb_Challenges)
		Me.Controls.Add(Me.barDockControlLeft)
		Me.Controls.Add(Me.barDockControlRight)
		Me.Controls.Add(Me.barDockControlBottom)
		Me.Controls.Add(Me.barDockControlTop)
		Me.Name = "frm_Cheevo_Challenges_Edit"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Edit Challenges"
		CType(Me.cmb_Challenges.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Cheevo_Challenges, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Cheevo_Challenges_Cheevos, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.grd_Cheevos, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Cheevos, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpiTier, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpiHardcore, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.barmng, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.popmnu_Cheevos, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents lbl_Challenge As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents cmb_Challenges As MKNetDXLib.ctl_MKDXLookupEdit
	Friend WithEvents DS_ML As DS_ML
	Friend WithEvents BS_Cheevo_Challenges As BindingSource
	Friend WithEvents BS_Cheevo_Challenges_Cheevos As BindingSource
	Friend WithEvents grd_Cheevos As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_Cheevos As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents btn_Close As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_Save As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents colTier As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents rpiTier As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
	Friend WithEvents colHardcore As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents rpiHardcore As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
	Friend WithEvents colCheevo_GameName As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colCheevo_Description As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colCheevo_Title As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents barmng As MKNetDXLib.ctl_MKDXBarManager
	Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
	Friend WithEvents bbi_Cheevo_Remove As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Cheevo_Move As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents popmnu_Cheevos As MKNetDXLib.cmp_MKDXPopupMenu
End Class
