<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Screenshot_Edit
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
		Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
		Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem2 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem2 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
		Dim SuperToolTip3 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem3 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem3 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
		Dim SuperToolTip4 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem4 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem4 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Me.pic_Game = New MKNetDXLib.ctl_MKDXPictureEdit()
		Me.gb_Crop = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.spn_Right = New MKNetDXLib.ctl_MKDXSpinEdit()
		Me.spn_Left = New MKNetDXLib.ctl_MKDXSpinEdit()
		Me.spn_Bottom = New MKNetDXLib.ctl_MKDXSpinEdit()
		Me.spn_Top = New MKNetDXLib.ctl_MKDXSpinEdit()
		Me.Ctl_MKDXLabel4 = New MKNetDXLib.ctl_MKDXLabel()
		Me.Ctl_MKDXLabel3 = New MKNetDXLib.ctl_MKDXLabel()
		Me.Ctl_MKDXLabel2 = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Top = New MKNetDXLib.ctl_MKDXLabel()
		Me.btn_Cancel = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.lbl_Templates = New MKNetDXLib.ctl_MKDXLabel()
		Me.cmb_Templates = New MKNetDXLib.ctl_MKDXLookupEdit()
		Me.BS_ImageEditorTemplates = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_ML = New Metropolis_Launcher.DS_ML()
		CType(Me.pic_Game.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gb_Crop, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_Crop.SuspendLayout()
		CType(Me.spn_Right.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spn_Left.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spn_Bottom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.spn_Top.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.cmb_Templates.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_ImageEditorTemplates, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'pic_Game
		'
		Me.pic_Game.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.pic_Game.Cursor = System.Windows.Forms.Cursors.Hand
		Me.pic_Game.Location = New System.Drawing.Point(121, 26)
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
		Me.pic_Game.Size = New System.Drawing.Size(500, 387)
		ToolTipTitleItem1.Text = "Change Background"
		ToolTipItem1.LeftIndent = 6
		ToolTipItem1.Text = "Click here to change the background."
		SuperToolTip1.Items.Add(ToolTipTitleItem1)
		SuperToolTip1.Items.Add(ToolTipItem1)
		Me.pic_Game.SuperTip = SuperToolTip1
		Me.pic_Game.TabIndex = 2
		'
		'gb_Crop
		'
		Me.gb_Crop.Controls.Add(Me.spn_Right)
		Me.gb_Crop.Controls.Add(Me.spn_Left)
		Me.gb_Crop.Controls.Add(Me.spn_Bottom)
		Me.gb_Crop.Controls.Add(Me.spn_Top)
		Me.gb_Crop.Controls.Add(Me.Ctl_MKDXLabel4)
		Me.gb_Crop.Controls.Add(Me.Ctl_MKDXLabel3)
		Me.gb_Crop.Controls.Add(Me.Ctl_MKDXLabel2)
		Me.gb_Crop.Controls.Add(Me.lbl_Top)
		Me.gb_Crop.Location = New System.Drawing.Point(2, 26)
		Me.gb_Crop.Name = "gb_Crop"
		Me.gb_Crop.Size = New System.Drawing.Size(115, 122)
		Me.gb_Crop.TabIndex = 1
		Me.gb_Crop.Text = "Crop"
		'
		'spn_Right
		'
		Me.spn_Right.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
		Me.spn_Right.Location = New System.Drawing.Point(53, 95)
		Me.spn_Right.MKBoundLabel = Nothing
		Me.spn_Right.MKEditValue_Compare = Nothing
		Me.spn_Right.Name = "spn_Right"
		Me.spn_Right.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
		Me.spn_Right.Size = New System.Drawing.Size(58, 20)
		Me.spn_Right.TabIndex = 3
		'
		'spn_Left
		'
		Me.spn_Left.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
		Me.spn_Left.Location = New System.Drawing.Point(53, 72)
		Me.spn_Left.MKBoundLabel = Nothing
		Me.spn_Left.MKEditValue_Compare = Nothing
		Me.spn_Left.Name = "spn_Left"
		Me.spn_Left.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
		Me.spn_Left.Size = New System.Drawing.Size(58, 20)
		Me.spn_Left.TabIndex = 2
		'
		'spn_Bottom
		'
		Me.spn_Bottom.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
		Me.spn_Bottom.Location = New System.Drawing.Point(53, 49)
		Me.spn_Bottom.MKBoundLabel = Nothing
		Me.spn_Bottom.MKEditValue_Compare = Nothing
		Me.spn_Bottom.Name = "spn_Bottom"
		Me.spn_Bottom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
		Me.spn_Bottom.Size = New System.Drawing.Size(58, 20)
		Me.spn_Bottom.TabIndex = 1
		'
		'spn_Top
		'
		Me.spn_Top.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
		Me.spn_Top.Location = New System.Drawing.Point(53, 26)
		Me.spn_Top.MKBoundLabel = Nothing
		Me.spn_Top.MKEditValue_Compare = Nothing
		Me.spn_Top.Name = "spn_Top"
		Me.spn_Top.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
		Me.spn_Top.Size = New System.Drawing.Size(58, 20)
		Me.spn_Top.TabIndex = 0
		'
		'Ctl_MKDXLabel4
		'
		Me.Ctl_MKDXLabel4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.Ctl_MKDXLabel4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.Ctl_MKDXLabel4.Location = New System.Drawing.Point(6, 95)
		Me.Ctl_MKDXLabel4.MKBoundControl1 = Nothing
		Me.Ctl_MKDXLabel4.MKBoundControl2 = Nothing
		Me.Ctl_MKDXLabel4.MKBoundControl3 = Nothing
		Me.Ctl_MKDXLabel4.MKBoundControl4 = Nothing
		Me.Ctl_MKDXLabel4.MKBoundControl5 = Nothing
		Me.Ctl_MKDXLabel4.Name = "Ctl_MKDXLabel4"
		Me.Ctl_MKDXLabel4.Size = New System.Drawing.Size(44, 20)
		Me.Ctl_MKDXLabel4.TabIndex = 0
		Me.Ctl_MKDXLabel4.Text = "Right:"
		'
		'Ctl_MKDXLabel3
		'
		Me.Ctl_MKDXLabel3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.Ctl_MKDXLabel3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.Ctl_MKDXLabel3.Location = New System.Drawing.Point(6, 72)
		Me.Ctl_MKDXLabel3.MKBoundControl1 = Nothing
		Me.Ctl_MKDXLabel3.MKBoundControl2 = Nothing
		Me.Ctl_MKDXLabel3.MKBoundControl3 = Nothing
		Me.Ctl_MKDXLabel3.MKBoundControl4 = Nothing
		Me.Ctl_MKDXLabel3.MKBoundControl5 = Nothing
		Me.Ctl_MKDXLabel3.Name = "Ctl_MKDXLabel3"
		Me.Ctl_MKDXLabel3.Size = New System.Drawing.Size(44, 20)
		Me.Ctl_MKDXLabel3.TabIndex = 0
		Me.Ctl_MKDXLabel3.Text = "Left:"
		'
		'Ctl_MKDXLabel2
		'
		Me.Ctl_MKDXLabel2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.Ctl_MKDXLabel2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.Ctl_MKDXLabel2.Location = New System.Drawing.Point(6, 49)
		Me.Ctl_MKDXLabel2.MKBoundControl1 = Nothing
		Me.Ctl_MKDXLabel2.MKBoundControl2 = Nothing
		Me.Ctl_MKDXLabel2.MKBoundControl3 = Nothing
		Me.Ctl_MKDXLabel2.MKBoundControl4 = Nothing
		Me.Ctl_MKDXLabel2.MKBoundControl5 = Nothing
		Me.Ctl_MKDXLabel2.Name = "Ctl_MKDXLabel2"
		Me.Ctl_MKDXLabel2.Size = New System.Drawing.Size(44, 20)
		Me.Ctl_MKDXLabel2.TabIndex = 0
		Me.Ctl_MKDXLabel2.Text = "Bottom:"
		'
		'lbl_Top
		'
		Me.lbl_Top.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Top.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Top.Location = New System.Drawing.Point(6, 26)
		Me.lbl_Top.MKBoundControl1 = Nothing
		Me.lbl_Top.MKBoundControl2 = Nothing
		Me.lbl_Top.MKBoundControl3 = Nothing
		Me.lbl_Top.MKBoundControl4 = Nothing
		Me.lbl_Top.MKBoundControl5 = Nothing
		Me.lbl_Top.Name = "lbl_Top"
		Me.lbl_Top.Size = New System.Drawing.Size(44, 20)
		Me.lbl_Top.TabIndex = 0
		Me.lbl_Top.Text = "Top:"
		'
		'btn_Cancel
		'
		Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btn_Cancel.Location = New System.Drawing.Point(546, 416)
		Me.btn_Cancel.Name = "btn_Cancel"
		Me.btn_Cancel.Size = New System.Drawing.Size(75, 23)
		Me.btn_Cancel.TabIndex = 4
		Me.btn_Cancel.Text = "&Cancel"
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.Location = New System.Drawing.Point(468, 416)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 23)
		Me.btn_OK.TabIndex = 3
		Me.btn_OK.Text = "&OK"
		'
		'lbl_Templates
		'
		Me.lbl_Templates.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Templates.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Templates.Location = New System.Drawing.Point(2, 3)
		Me.lbl_Templates.MKBoundControl1 = Nothing
		Me.lbl_Templates.MKBoundControl2 = Nothing
		Me.lbl_Templates.MKBoundControl3 = Nothing
		Me.lbl_Templates.MKBoundControl4 = Nothing
		Me.lbl_Templates.MKBoundControl5 = Nothing
		Me.lbl_Templates.Name = "lbl_Templates"
		Me.lbl_Templates.Size = New System.Drawing.Size(115, 20)
		Me.lbl_Templates.TabIndex = 0
		Me.lbl_Templates.Text = "Templates:"
		'
		'cmb_Templates
		'
		Me.cmb_Templates.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmb_Templates.EditValue = CType(0, Long)
		Me.cmb_Templates.Location = New System.Drawing.Point(121, 3)
		Me.cmb_Templates.MKBoundLabel = Nothing
		Me.cmb_Templates.MKEditValue_Compare = Nothing
		Me.cmb_Templates.Name = "cmb_Templates"
		ToolTipTitleItem2.Text = "Add Template"
		ToolTipItem2.LeftIndent = 6
		ToolTipItem2.Text = "Add current settings as Template."
		SuperToolTip2.Items.Add(ToolTipTitleItem2)
		SuperToolTip2.Items.Add(ToolTipItem2)
		ToolTipTitleItem3.Text = "Delete Template"
		ToolTipItem3.LeftIndent = 6
		ToolTipItem3.Text = "Delete current template"
		SuperToolTip3.Items.Add(ToolTipTitleItem3)
		SuperToolTip3.Items.Add(ToolTipItem3)
		ToolTipTitleItem4.Text = "Edit Template"
		ToolTipItem4.LeftIndent = 6
		ToolTipItem4.Text = "Edit the template's name."
		SuperToolTip4.Items.Add(ToolTipTitleItem4)
		SuperToolTip4.Items.Add(ToolTipItem4)
		Me.cmb_Templates.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "", -1, False, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, SuperToolTip2, True), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Minus, "", -1, False, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, SuperToolTip3, True), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, False, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", Nothing, SuperToolTip4, True)})
		Me.cmb_Templates.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_ImageEditorTemplates", "id_Image Editor Settings", 140, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Top", "Top", 28, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Bottom", "Bottom", 44, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Left", "Left", 29, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Right", "Right", 35, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Title", "Title", 30, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near)})
		Me.cmb_Templates.Properties.DataSource = Me.BS_ImageEditorTemplates
		Me.cmb_Templates.Properties.DisplayMember = "Title"
		Me.cmb_Templates.Properties.ValueMember = "id_ImageEditorTemplates"
		Me.cmb_Templates.Size = New System.Drawing.Size(500, 20)
		Me.cmb_Templates.TabIndex = 0
		'
		'BS_ImageEditorTemplates
		'
		Me.BS_ImageEditorTemplates.DataMember = "tbl_ImageEditorTemplates"
		Me.BS_ImageEditorTemplates.DataSource = Me.DS_ML
		'
		'DS_ML
		'
		Me.DS_ML.DataSetName = "DS_ML"
		Me.DS_ML.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'frm_Screenshot_Edit
		'
		Me.ClientSize = New System.Drawing.Size(624, 442)
		Me.Controls.Add(Me.cmb_Templates)
		Me.Controls.Add(Me.btn_Cancel)
		Me.Controls.Add(Me.btn_OK)
		Me.Controls.Add(Me.gb_Crop)
		Me.Controls.Add(Me.pic_Game)
		Me.Controls.Add(Me.lbl_Templates)
		Me.Name = "frm_Screenshot_Edit"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Edit Image"
		CType(Me.pic_Game.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gb_Crop, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_Crop.ResumeLayout(False)
		CType(Me.spn_Right.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spn_Left.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spn_Bottom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.spn_Top.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.cmb_Templates.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_ImageEditorTemplates, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents pic_Game As MKNetDXLib.ctl_MKDXPictureEdit
	Friend WithEvents gb_Crop As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents spn_Top As MKNetDXLib.ctl_MKDXSpinEdit
	Friend WithEvents Ctl_MKDXLabel4 As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents Ctl_MKDXLabel3 As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents Ctl_MKDXLabel2 As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Top As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents btn_Cancel As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents spn_Right As MKNetDXLib.ctl_MKDXSpinEdit
	Friend WithEvents spn_Left As MKNetDXLib.ctl_MKDXSpinEdit
	Friend WithEvents spn_Bottom As MKNetDXLib.ctl_MKDXSpinEdit
	Friend WithEvents lbl_Templates As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents cmb_Templates As MKNetDXLib.ctl_MKDXLookupEdit
	Friend WithEvents BS_ImageEditorTemplates As System.Windows.Forms.BindingSource
	Friend WithEvents DS_ML As Metropolis_Launcher.DS_ML

End Class
