<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Cheevo_Challenges_Add
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
		Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem2 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Cheevo_Challenges_Add))
		Dim SuperToolTip3 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem3 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem2 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip4 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem4 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem3 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip5 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem5 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem4 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip6 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem6 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem5 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Me.lbl_Challenge = New MKNetDXLib.ctl_MKDXLabel()
		Me.cmb_Challenges = New MKNetDXLib.ctl_MKDXLookupEdit()
		Me.BS_Cheevo_Challenges = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_ML = New Metropolis_Launcher.DS_ML()
		Me.lbl_Tier = New MKNetDXLib.ctl_MKDXLabel()
		Me.cmb_Tier = New MKNetDXLib.ctl_MKDXLookupEdit()
		Me.BS_Tiers = New System.Windows.Forms.BindingSource(Me.components)
		Me.gb_Cheevos_in_Tier = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_RetroAchievements = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_Cheevos_in_Tier = New System.Windows.Forms.BindingSource(Me.components)
		Me.agv_RetroAchievements = New DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView()
		Me.GridBand1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
		Me.colCheevo_GameName = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
		Me.colTitle = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
		Me.colDescription1 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
		Me.rpi_RetroAchievements_Unlock = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		Me.lbl_Cheevos_in_Tier_Message = New MKNetDXLib.ctl_MKDXLabel()
		Me.btn_Cancel = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.lbl_Hardcore = New MKNetDXLib.ctl_MKDXLabel()
		Me.chb_Hardcore = New MKNetDXLib.ctl_MKDXCheckEdit()
		Me.lbl_CantAdd = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Total_Runtime = New MKNetDXLib.ctl_MKDXLabel()
		Me.spn_Hours = New MKNetDXLib.ctl_MKDXSpinEdit()
		Me.lbl_Hours = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Minutes = New MKNetDXLib.ctl_MKDXLabel()
		Me.spn_Minutes = New MKNetDXLib.ctl_MKDXSpinEdit()
		Me.lbl_AchievementDisplayText = New MKNetDXLib.ctl_MKDXLabel()
		CType(Me.cmb_Challenges.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Cheevo_Challenges, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.cmb_Tier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Tiers, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gb_Cheevos_in_Tier, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_Cheevos_in_Tier.SuspendLayout()
		CType(Me.grd_RetroAchievements, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Cheevos_in_Tier, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.agv_RetroAchievements, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_RetroAchievements_Unlock, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.chb_Hardcore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spn_Hours.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spn_Minutes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'lbl_Challenge
		'
		Me.lbl_Challenge.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Challenge.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Challenge.Location = New System.Drawing.Point(3, 26)
		Me.lbl_Challenge.MKBoundControl1 = Nothing
		Me.lbl_Challenge.MKBoundControl2 = Nothing
		Me.lbl_Challenge.MKBoundControl3 = Nothing
		Me.lbl_Challenge.MKBoundControl4 = Nothing
		Me.lbl_Challenge.MKBoundControl5 = Nothing
		Me.lbl_Challenge.Name = "lbl_Challenge"
		Me.lbl_Challenge.Size = New System.Drawing.Size(77, 20)
		Me.lbl_Challenge.TabIndex = 3
		Me.lbl_Challenge.Text = "Challenge:"
		'
		'cmb_Challenges
		'
		Me.cmb_Challenges.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmb_Challenges.Location = New System.Drawing.Point(81, 26)
		Me.cmb_Challenges.MKBoundLabel = Nothing
		Me.cmb_Challenges.MKEditValue_Compare = Nothing
		Me.cmb_Challenges.Name = "cmb_Challenges"
		ToolTipTitleItem1.Text = "Add new Challenge"
		SuperToolTip1.Items.Add(ToolTipTitleItem1)
		Me.cmb_Challenges.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, SuperToolTip1, True)})
		Me.cmb_Challenges.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_Cheevo_Challenges", "id_Cheevo_Challenges", 5, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name", 5, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("created", "created", 5, DevExpress.Utils.FormatType.DateTime, "dd.MM.yyyy", False, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("updated", "updated", 5, DevExpress.Utils.FormatType.DateTime, "dd.MM.yyyy", False, DevExpress.Utils.HorzAlignment.Near)})
		Me.cmb_Challenges.Properties.DataSource = Me.BS_Cheevo_Challenges
		Me.cmb_Challenges.Properties.DisplayMember = "Name"
		Me.cmb_Challenges.Properties.NullText = "<please choose>"
		Me.cmb_Challenges.Properties.ShowFooter = False
		Me.cmb_Challenges.Properties.ShowHeader = False
		Me.cmb_Challenges.Properties.ValueMember = "id_Cheevo_Challenges"
		Me.cmb_Challenges.Size = New System.Drawing.Size(404, 20)
		Me.cmb_Challenges.TabIndex = 2
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
		'lbl_Tier
		'
		Me.lbl_Tier.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Tier.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Tier.Location = New System.Drawing.Point(3, 49)
		Me.lbl_Tier.MKBoundControl1 = Nothing
		Me.lbl_Tier.MKBoundControl2 = Nothing
		Me.lbl_Tier.MKBoundControl3 = Nothing
		Me.lbl_Tier.MKBoundControl4 = Nothing
		Me.lbl_Tier.MKBoundControl5 = Nothing
		Me.lbl_Tier.Name = "lbl_Tier"
		Me.lbl_Tier.Size = New System.Drawing.Size(77, 20)
		ToolTipTitleItem2.Text = "Tier"
		ToolTipItem1.LeftIndent = 6
		ToolTipItem1.Text = resources.GetString("ToolTipItem1.Text")
		SuperToolTip2.Items.Add(ToolTipTitleItem2)
		SuperToolTip2.Items.Add(ToolTipItem1)
		Me.lbl_Tier.SuperTip = SuperToolTip2
		Me.lbl_Tier.TabIndex = 4
		Me.lbl_Tier.Text = "Tier:"
		'
		'cmb_Tier
		'
		Me.cmb_Tier.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmb_Tier.Location = New System.Drawing.Point(81, 49)
		Me.cmb_Tier.MKBoundLabel = Nothing
		Me.cmb_Tier.MKEditValue_Compare = Nothing
		Me.cmb_Tier.Name = "cmb_Tier"
		Me.cmb_Tier.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)})
		Me.cmb_Tier.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_Cheevo_Challenges", "id_Cheevo_Challenges", 5, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Tier", "Tier", 5, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayName", "Display Name", 5, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Near)})
		Me.cmb_Tier.Properties.DataSource = Me.BS_Tiers
		Me.cmb_Tier.Properties.DisplayMember = "Tier"
		Me.cmb_Tier.Properties.NullText = "<please choose>"
		Me.cmb_Tier.Properties.ShowFooter = False
		Me.cmb_Tier.Properties.ShowHeader = False
		Me.cmb_Tier.Properties.ValueMember = "Tier"
		Me.cmb_Tier.Size = New System.Drawing.Size(404, 20)
		ToolTipTitleItem3.Text = "Tier"
		ToolTipItem2.LeftIndent = 6
		ToolTipItem2.Text = resources.GetString("ToolTipItem2.Text")
		SuperToolTip3.Items.Add(ToolTipTitleItem3)
		SuperToolTip3.Items.Add(ToolTipItem2)
		Me.cmb_Tier.SuperTip = SuperToolTip3
		Me.cmb_Tier.TabIndex = 5
		'
		'BS_Tiers
		'
		Me.BS_Tiers.DataMember = "ttb_Cheevo_Challenges_Tiers"
		Me.BS_Tiers.DataSource = Me.DS_ML
		'
		'gb_Cheevos_in_Tier
		'
		Me.gb_Cheevos_in_Tier.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.gb_Cheevos_in_Tier.Controls.Add(Me.grd_RetroAchievements)
		Me.gb_Cheevos_in_Tier.Controls.Add(Me.lbl_Cheevos_in_Tier_Message)
		Me.gb_Cheevos_in_Tier.Location = New System.Drawing.Point(3, 118)
		Me.gb_Cheevos_in_Tier.Name = "gb_Cheevos_in_Tier"
		Me.gb_Cheevos_in_Tier.Size = New System.Drawing.Size(482, 209)
		Me.gb_Cheevos_in_Tier.TabIndex = 6
		Me.gb_Cheevos_in_Tier.Text = "Achievements in selected Tier"
		'
		'grd_RetroAchievements
		'
		Me.grd_RetroAchievements.DataSource = Me.BS_Cheevos_in_Tier
		Me.grd_RetroAchievements.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_RetroAchievements.Location = New System.Drawing.Point(2, 39)
		Me.grd_RetroAchievements.MainView = Me.agv_RetroAchievements
		Me.grd_RetroAchievements.Name = "grd_RetroAchievements"
		Me.grd_RetroAchievements.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rpi_RetroAchievements_Unlock})
		Me.grd_RetroAchievements.Size = New System.Drawing.Size(478, 168)
		Me.grd_RetroAchievements.TabIndex = 10
		Me.grd_RetroAchievements.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.agv_RetroAchievements})
		'
		'BS_Cheevos_in_Tier
		'
		Me.BS_Cheevos_in_Tier.DataMember = "tbl_Cheevo_Challenges_Cheevos"
		Me.BS_Cheevos_in_Tier.DataSource = Me.DS_ML
		Me.BS_Cheevos_in_Tier.Filter = "Tier = 0"
		'
		'agv_RetroAchievements
		'
		Me.agv_RetroAchievements.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.GridBand1})
		Me.agv_RetroAchievements.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {Me.colCheevo_GameName, Me.colTitle, Me.colDescription1})
		Me.agv_RetroAchievements.GridControl = Me.grd_RetroAchievements
		Me.agv_RetroAchievements.GroupFormat = "{1}"
		Me.agv_RetroAchievements.Name = "agv_RetroAchievements"
		Me.agv_RetroAchievements.OptionsView.ColumnAutoWidth = True
		Me.agv_RetroAchievements.OptionsView.ShowBands = False
		Me.agv_RetroAchievements.OptionsView.ShowColumnHeaders = False
		Me.agv_RetroAchievements.OptionsView.ShowGroupPanel = False
		Me.agv_RetroAchievements.OptionsView.ShowIndicator = False
		'
		'GridBand1
		'
		Me.GridBand1.Caption = "GridBand1"
		Me.GridBand1.Columns.Add(Me.colCheevo_GameName)
		Me.GridBand1.Columns.Add(Me.colTitle)
		Me.GridBand1.Columns.Add(Me.colDescription1)
		Me.GridBand1.Name = "GridBand1"
		Me.GridBand1.VisibleIndex = 0
		Me.GridBand1.Width = 351
		'
		'colCheevo_GameName
		'
		Me.colCheevo_GameName.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
		Me.colCheevo_GameName.AppearanceCell.Options.UseFont = True
		Me.colCheevo_GameName.FieldName = "Cheevo_GameName"
		Me.colCheevo_GameName.Name = "colCheevo_GameName"
		Me.colCheevo_GameName.OptionsColumn.AllowEdit = False
		Me.colCheevo_GameName.OptionsColumn.AllowFocus = False
		Me.colCheevo_GameName.OptionsColumn.ReadOnly = True
		Me.colCheevo_GameName.Visible = True
		Me.colCheevo_GameName.Width = 178
		'
		'colTitle
		'
		Me.colTitle.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
		Me.colTitle.AppearanceCell.Options.UseFont = True
		Me.colTitle.FieldName = "Cheevo_Title"
		Me.colTitle.Name = "colTitle"
		Me.colTitle.OptionsColumn.AllowEdit = False
		Me.colTitle.OptionsColumn.AllowFocus = False
		Me.colTitle.OptionsColumn.ReadOnly = True
		Me.colTitle.Visible = True
		Me.colTitle.Width = 173
		'
		'colDescription1
		'
		Me.colDescription1.AutoFillDown = True
		Me.colDescription1.FieldName = "Cheevo_Description"
		Me.colDescription1.Name = "colDescription1"
		Me.colDescription1.OptionsColumn.AllowEdit = False
		Me.colDescription1.OptionsColumn.AllowFocus = False
		Me.colDescription1.OptionsColumn.ReadOnly = True
		Me.colDescription1.RowIndex = 1
		Me.colDescription1.Visible = True
		Me.colDescription1.Width = 351
		'
		'rpi_RetroAchievements_Unlock
		'
		Me.rpi_RetroAchievements_Unlock.AutoHeight = False
		Me.rpi_RetroAchievements_Unlock.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.rpi_RetroAchievements_Unlock.DisplayMember = "DisplayText"
		Me.rpi_RetroAchievements_Unlock.Name = "rpi_RetroAchievements_Unlock"
		Me.rpi_RetroAchievements_Unlock.ValueMember = "id"
		'
		'lbl_Cheevos_in_Tier_Message
		'
		Me.lbl_Cheevos_in_Tier_Message.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_Cheevos_in_Tier_Message.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_Cheevos_in_Tier_Message.Location = New System.Drawing.Point(2, 20)
		Me.lbl_Cheevos_in_Tier_Message.MKBoundControl1 = Nothing
		Me.lbl_Cheevos_in_Tier_Message.MKBoundControl2 = Nothing
		Me.lbl_Cheevos_in_Tier_Message.MKBoundControl3 = Nothing
		Me.lbl_Cheevos_in_Tier_Message.MKBoundControl4 = Nothing
		Me.lbl_Cheevos_in_Tier_Message.MKBoundControl5 = Nothing
		Me.lbl_Cheevos_in_Tier_Message.Name = "lbl_Cheevos_in_Tier_Message"
		Me.lbl_Cheevos_in_Tier_Message.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_Cheevos_in_Tier_Message.Size = New System.Drawing.Size(478, 19)
		Me.lbl_Cheevos_in_Tier_Message.TabIndex = 9
		Me.lbl_Cheevos_in_Tier_Message.Text = "No achievements are defined in the selected tier"
		Me.lbl_Cheevos_in_Tier_Message.Visible = False
		'
		'btn_Cancel
		'
		Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Cancel.Location = New System.Drawing.Point(410, 330)
		Me.btn_Cancel.Name = "btn_Cancel"
		Me.btn_Cancel.Size = New System.Drawing.Size(75, 23)
		Me.btn_Cancel.TabIndex = 10
		Me.btn_Cancel.Text = "&Cancel"
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.Enabled = False
		Me.btn_OK.Location = New System.Drawing.Point(332, 330)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 23)
		Me.btn_OK.TabIndex = 9
		Me.btn_OK.Text = "&OK"
		'
		'lbl_Hardcore
		'
		Me.lbl_Hardcore.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Hardcore.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Hardcore.Location = New System.Drawing.Point(3, 72)
		Me.lbl_Hardcore.MKBoundControl1 = Nothing
		Me.lbl_Hardcore.MKBoundControl2 = Nothing
		Me.lbl_Hardcore.MKBoundControl3 = Nothing
		Me.lbl_Hardcore.MKBoundControl4 = Nothing
		Me.lbl_Hardcore.MKBoundControl5 = Nothing
		Me.lbl_Hardcore.Name = "lbl_Hardcore"
		Me.lbl_Hardcore.Size = New System.Drawing.Size(77, 20)
		ToolTipTitleItem4.Text = "Harcore"
		ToolTipItem3.LeftIndent = 6
		ToolTipItem3.Text = "The achievement must be accomplished in hardcore mode."
		SuperToolTip4.Items.Add(ToolTipTitleItem4)
		SuperToolTip4.Items.Add(ToolTipItem3)
		Me.lbl_Hardcore.SuperTip = SuperToolTip4
		Me.lbl_Hardcore.TabIndex = 11
		Me.lbl_Hardcore.Text = "Hardcore:"
		'
		'chb_Hardcore
		'
		Me.chb_Hardcore.Location = New System.Drawing.Point(81, 72)
		Me.chb_Hardcore.MKBoundLabel = Nothing
		Me.chb_Hardcore.MKEditValue_Compare = Nothing
		Me.chb_Hardcore.Name = "chb_Hardcore"
		Me.chb_Hardcore.Properties.Caption = ""
		Me.chb_Hardcore.Size = New System.Drawing.Size(75, 19)
		ToolTipTitleItem5.Text = "Hardcore"
		ToolTipItem4.LeftIndent = 6
		ToolTipItem4.Text = "The achievement must be accomplished in hardcore mode."
		SuperToolTip5.Items.Add(ToolTipTitleItem5)
		SuperToolTip5.Items.Add(ToolTipItem4)
		Me.chb_Hardcore.SuperTip = SuperToolTip5
		Me.chb_Hardcore.TabIndex = 12
		'
		'lbl_CantAdd
		'
		Me.lbl_CantAdd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_CantAdd.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_CantAdd.Location = New System.Drawing.Point(2, 332)
		Me.lbl_CantAdd.MKBoundControl1 = Nothing
		Me.lbl_CantAdd.MKBoundControl2 = Nothing
		Me.lbl_CantAdd.MKBoundControl3 = Nothing
		Me.lbl_CantAdd.MKBoundControl4 = Nothing
		Me.lbl_CantAdd.MKBoundControl5 = Nothing
		Me.lbl_CantAdd.Name = "lbl_CantAdd"
		Me.lbl_CantAdd.Size = New System.Drawing.Size(326, 20)
		Me.lbl_CantAdd.TabIndex = 13
		Me.lbl_CantAdd.Text = "Note: The achievement is already part of the selected challenge!"
		Me.lbl_CantAdd.Visible = False
		'
		'lbl_Total_Runtime
		'
		Me.lbl_Total_Runtime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Total_Runtime.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Total_Runtime.Location = New System.Drawing.Point(3, 95)
		Me.lbl_Total_Runtime.MKBoundControl1 = Nothing
		Me.lbl_Total_Runtime.MKBoundControl2 = Nothing
		Me.lbl_Total_Runtime.MKBoundControl3 = Nothing
		Me.lbl_Total_Runtime.MKBoundControl4 = Nothing
		Me.lbl_Total_Runtime.MKBoundControl5 = Nothing
		Me.lbl_Total_Runtime.Name = "lbl_Total_Runtime"
		Me.lbl_Total_Runtime.Size = New System.Drawing.Size(77, 20)
		ToolTipTitleItem6.Text = "Total Runtime"
		ToolTipItem5.LeftIndent = 6
		ToolTipItem5.Text = "Define here, how long the game has to be played in order to unlock it within the " &
		"challenge."
		SuperToolTip6.Items.Add(ToolTipTitleItem6)
		SuperToolTip6.Items.Add(ToolTipItem5)
		Me.lbl_Total_Runtime.SuperTip = SuperToolTip6
		Me.lbl_Total_Runtime.TabIndex = 14
		Me.lbl_Total_Runtime.Text = "Total Runtime:"
		'
		'spn_Hours
		'
		Me.spn_Hours.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
		Me.spn_Hours.Location = New System.Drawing.Point(83, 95)
		Me.spn_Hours.MKBoundLabel = Nothing
		Me.spn_Hours.MKEditValue_Compare = Nothing
		Me.spn_Hours.Name = "spn_Hours"
		Me.spn_Hours.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.spn_Hours.Properties.IsFloatValue = False
		Me.spn_Hours.Properties.Mask.EditMask = "N00"
		Me.spn_Hours.Properties.MaxValue = New Decimal(New Integer() {99, 0, 0, 0})
		Me.spn_Hours.Size = New System.Drawing.Size(36, 20)
		Me.spn_Hours.TabIndex = 15
		'
		'lbl_Hours
		'
		Me.lbl_Hours.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Hours.Location = New System.Drawing.Point(122, 95)
		Me.lbl_Hours.MKBoundControl1 = Nothing
		Me.lbl_Hours.MKBoundControl2 = Nothing
		Me.lbl_Hours.MKBoundControl3 = Nothing
		Me.lbl_Hours.MKBoundControl4 = Nothing
		Me.lbl_Hours.MKBoundControl5 = Nothing
		Me.lbl_Hours.Name = "lbl_Hours"
		Me.lbl_Hours.Size = New System.Drawing.Size(27, 20)
		Me.lbl_Hours.TabIndex = 16
		Me.lbl_Hours.Text = "hours"
		'
		'lbl_Minutes
		'
		Me.lbl_Minutes.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Minutes.Location = New System.Drawing.Point(191, 95)
		Me.lbl_Minutes.MKBoundControl1 = Nothing
		Me.lbl_Minutes.MKBoundControl2 = Nothing
		Me.lbl_Minutes.MKBoundControl3 = Nothing
		Me.lbl_Minutes.MKBoundControl4 = Nothing
		Me.lbl_Minutes.MKBoundControl5 = Nothing
		Me.lbl_Minutes.Name = "lbl_Minutes"
		Me.lbl_Minutes.Size = New System.Drawing.Size(37, 20)
		Me.lbl_Minutes.TabIndex = 18
		Me.lbl_Minutes.Text = "minutes"
		'
		'spn_Minutes
		'
		Me.spn_Minutes.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
		Me.spn_Minutes.Location = New System.Drawing.Point(152, 95)
		Me.spn_Minutes.MKBoundLabel = Nothing
		Me.spn_Minutes.MKEditValue_Compare = Nothing
		Me.spn_Minutes.Name = "spn_Minutes"
		Me.spn_Minutes.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.spn_Minutes.Properties.IsFloatValue = False
		Me.spn_Minutes.Properties.Mask.EditMask = "N00"
		Me.spn_Minutes.Properties.MaxValue = New Decimal(New Integer() {59, 0, 0, 0})
		Me.spn_Minutes.Size = New System.Drawing.Size(36, 20)
		Me.spn_Minutes.TabIndex = 17
		'
		'lbl_AchievementDisplayText
		'
		Me.lbl_AchievementDisplayText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_AchievementDisplayText.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_AchievementDisplayText.Location = New System.Drawing.Point(3, 3)
		Me.lbl_AchievementDisplayText.MKBoundControl1 = Nothing
		Me.lbl_AchievementDisplayText.MKBoundControl2 = Nothing
		Me.lbl_AchievementDisplayText.MKBoundControl3 = Nothing
		Me.lbl_AchievementDisplayText.MKBoundControl4 = Nothing
		Me.lbl_AchievementDisplayText.MKBoundControl5 = Nothing
		Me.lbl_AchievementDisplayText.Name = "lbl_AchievementDisplayText"
		Me.lbl_AchievementDisplayText.Size = New System.Drawing.Size(482, 20)
		Me.lbl_AchievementDisplayText.TabIndex = 19
		'
		'frm_Cheevo_Challenges_Add
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(488, 356)
		Me.Controls.Add(Me.lbl_AchievementDisplayText)
		Me.Controls.Add(Me.lbl_Minutes)
		Me.Controls.Add(Me.spn_Minutes)
		Me.Controls.Add(Me.lbl_Hours)
		Me.Controls.Add(Me.spn_Hours)
		Me.Controls.Add(Me.lbl_Total_Runtime)
		Me.Controls.Add(Me.lbl_CantAdd)
		Me.Controls.Add(Me.chb_Hardcore)
		Me.Controls.Add(Me.lbl_Hardcore)
		Me.Controls.Add(Me.btn_Cancel)
		Me.Controls.Add(Me.btn_OK)
		Me.Controls.Add(Me.gb_Cheevos_in_Tier)
		Me.Controls.Add(Me.cmb_Tier)
		Me.Controls.Add(Me.lbl_Tier)
		Me.Controls.Add(Me.lbl_Challenge)
		Me.Controls.Add(Me.cmb_Challenges)
		Me.Name = "frm_Cheevo_Challenges_Add"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Add Achievement to Challenge"
		CType(Me.cmb_Challenges.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Cheevo_Challenges, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.cmb_Tier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Tiers, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gb_Cheevos_in_Tier, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_Cheevos_in_Tier.ResumeLayout(False)
		CType(Me.grd_RetroAchievements, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Cheevos_in_Tier, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.agv_RetroAchievements, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_RetroAchievements_Unlock, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.chb_Hardcore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spn_Hours.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spn_Minutes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents lbl_Challenge As MKNetDXLib.ctl_MKDXLabel
    Friend WithEvents cmb_Challenges As MKNetDXLib.ctl_MKDXLookupEdit
    Friend WithEvents lbl_Tier As MKNetDXLib.ctl_MKDXLabel
    Friend WithEvents cmb_Tier As MKNetDXLib.ctl_MKDXLookupEdit
    Friend WithEvents gb_Cheevos_in_Tier As MKNetDXLib.ctl_MKDXGroupBox
    Friend WithEvents grd_RetroAchievements As MKNetDXLib.ctl_MKDXGrid
    Friend WithEvents agv_RetroAchievements As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
	Friend WithEvents colTitle As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
	Friend WithEvents colDescription1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
	Friend WithEvents rpi_RetroAchievements_Unlock As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
	Friend WithEvents lbl_Cheevos_in_Tier_Message As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents btn_Cancel As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents DS_ML As DS_ML
	Friend WithEvents BS_Cheevo_Challenges As BindingSource
	Friend WithEvents BS_Tiers As BindingSource
	Friend WithEvents BS_Cheevos_in_Tier As BindingSource
	Friend WithEvents lbl_Hardcore As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents chb_Hardcore As MKNetDXLib.ctl_MKDXCheckEdit
	Friend WithEvents lbl_CantAdd As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents colCheevo_GameName As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
	Friend WithEvents GridBand1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
	Friend WithEvents lbl_Total_Runtime As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents spn_Hours As MKNetDXLib.ctl_MKDXSpinEdit
	Friend WithEvents lbl_Hours As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Minutes As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents spn_Minutes As MKNetDXLib.ctl_MKDXSpinEdit
	Friend WithEvents lbl_AchievementDisplayText As MKNetDXLib.ctl_MKDXLabel
End Class
