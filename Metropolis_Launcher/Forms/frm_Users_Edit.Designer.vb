<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Users_Edit
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
		Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem2 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem2 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip3 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem3 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem3 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip4 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem4 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem4 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip5 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem5 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem5 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip6 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem6 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem6 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip7 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem7 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem7 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip8 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem8 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem8 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip9 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem9 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem9 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Users_Edit))
		Me.btn_Cancel = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.txb_Password = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.lbl_Password = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Username = New MKNetDXLib.ctl_MKDXLabel()
		Me.txb_Username = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.lbl_Restricted = New MKNetDXLib.ctl_MKDXLabel()
		Me.chb_Restricted = New MKNetDXLib.ctl_MKDXCheckEdit()
		Me.chb_Password = New MKNetDXLib.ctl_MKDXCheckEdit()
		Me.lbl_Challenges = New MKNetDXLib.ctl_MKDXLabel()
		Me.cmb_Challenges = New MKNetDXLib.ctl_MKDXLookupEdit()
		Me.BTA_Challenges = New MKNetLib.cmp_MKBindableTableAdapter(Me.components)
		Me.DataTable1 = New System.Data.DataTable()
		Me.DataColumn1 = New System.Data.DataColumn()
		Me.DataColumn2 = New System.Data.DataColumn()
		Me.DataColumn3 = New System.Data.DataColumn()
		CType(Me.txb_Password.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_Username.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.chb_Restricted.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.chb_Password.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.cmb_Challenges.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BTA_Challenges, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'btn_Cancel
		'
		Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btn_Cancel.Location = New System.Drawing.Point(218, 100)
		Me.btn_Cancel.Name = "btn_Cancel"
		Me.btn_Cancel.Size = New System.Drawing.Size(75, 23)
		Me.btn_Cancel.TabIndex = 5
		Me.btn_Cancel.Text = "&Cancel"
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.Enabled = False
		Me.btn_OK.Location = New System.Drawing.Point(140, 100)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 23)
		Me.btn_OK.TabIndex = 4
		Me.btn_OK.Text = "&OK"
		'
		'txb_Password
		'
		Me.txb_Password.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_Password.Enabled = False
		Me.txb_Password.Location = New System.Drawing.Point(83, 26)
		Me.txb_Password.MKBoundLabel = Nothing
		Me.txb_Password.MKEditValue_Compare = Nothing
		Me.txb_Password.Name = "txb_Password"
		Me.txb_Password.Size = New System.Drawing.Size(210, 20)
		ToolTipTitleItem1.Text = "Password"
		ToolTipItem1.LeftIndent = 6
		ToolTipItem1.Text = "Enable this option to (re-)define the password for the user. Leave it unchecked a" &
		"nd there will be no alterations on the user's password."
		SuperToolTip1.Items.Add(ToolTipTitleItem1)
		SuperToolTip1.Items.Add(ToolTipItem1)
		Me.txb_Password.SuperTip = SuperToolTip1
		Me.txb_Password.TabIndex = 2
		'
		'lbl_Password
		'
		Me.lbl_Password.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Password.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Password.Enabled = False
		Me.lbl_Password.Location = New System.Drawing.Point(24, 26)
		Me.lbl_Password.MKBoundControl1 = Nothing
		Me.lbl_Password.MKBoundControl2 = Nothing
		Me.lbl_Password.MKBoundControl3 = Nothing
		Me.lbl_Password.MKBoundControl4 = Nothing
		Me.lbl_Password.MKBoundControl5 = Nothing
		Me.lbl_Password.Name = "lbl_Password"
		Me.lbl_Password.Size = New System.Drawing.Size(56, 20)
		ToolTipTitleItem2.Text = "Password"
		ToolTipItem2.LeftIndent = 6
		ToolTipItem2.Text = "Enable this option to (re-)define the password for the user. Leave it unchecked a" &
		"nd there will be no alterations on the user's password."
		SuperToolTip2.Items.Add(ToolTipTitleItem2)
		SuperToolTip2.Items.Add(ToolTipItem2)
		Me.lbl_Password.SuperTip = SuperToolTip2
		Me.lbl_Password.TabIndex = 5
		Me.lbl_Password.Text = "Password:"
		'
		'lbl_Username
		'
		Me.lbl_Username.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Username.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Username.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Username.MKBoundControl1 = Nothing
		Me.lbl_Username.MKBoundControl2 = Nothing
		Me.lbl_Username.MKBoundControl3 = Nothing
		Me.lbl_Username.MKBoundControl4 = Nothing
		Me.lbl_Username.MKBoundControl5 = Nothing
		Me.lbl_Username.Name = "lbl_Username"
		Me.lbl_Username.Size = New System.Drawing.Size(77, 20)
		ToolTipTitleItem3.Text = "Username"
		ToolTipItem3.LeftIndent = 6
		ToolTipItem3.Text = "Define or rename the user's name here."
		SuperToolTip3.Items.Add(ToolTipTitleItem3)
		SuperToolTip3.Items.Add(ToolTipItem3)
		Me.lbl_Username.SuperTip = SuperToolTip3
		Me.lbl_Username.TabIndex = 4
		Me.lbl_Username.Text = "Username:"
		'
		'txb_Username
		'
		Me.txb_Username.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_Username.Location = New System.Drawing.Point(83, 3)
		Me.txb_Username.MKBoundLabel = Nothing
		Me.txb_Username.MKEditValue_Compare = Nothing
		Me.txb_Username.Name = "txb_Username"
		Me.txb_Username.Size = New System.Drawing.Size(210, 20)
		ToolTipTitleItem4.Text = "Username"
		ToolTipItem4.LeftIndent = 6
		ToolTipItem4.Text = "Define or rename the user's name here."
		SuperToolTip4.Items.Add(ToolTipTitleItem4)
		SuperToolTip4.Items.Add(ToolTipItem4)
		Me.txb_Username.SuperTip = SuperToolTip4
		Me.txb_Username.TabIndex = 0
		Me.txb_Username.Tag = ""
		'
		'lbl_Restricted
		'
		Me.lbl_Restricted.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Restricted.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Restricted.Location = New System.Drawing.Point(3, 49)
		Me.lbl_Restricted.MKBoundControl1 = Nothing
		Me.lbl_Restricted.MKBoundControl2 = Nothing
		Me.lbl_Restricted.MKBoundControl3 = Nothing
		Me.lbl_Restricted.MKBoundControl4 = Nothing
		Me.lbl_Restricted.MKBoundControl5 = Nothing
		Me.lbl_Restricted.Name = "lbl_Restricted"
		Me.lbl_Restricted.Size = New System.Drawing.Size(77, 20)
		ToolTipTitleItem5.Text = "Restricted"
		ToolTipItem5.LeftIndent = 6
		ToolTipItem5.Text = "Enable this option and the user will have restricted access to games and media."
		SuperToolTip5.Items.Add(ToolTipTitleItem5)
		SuperToolTip5.Items.Add(ToolTipItem5)
		Me.lbl_Restricted.SuperTip = SuperToolTip5
		Me.lbl_Restricted.TabIndex = 5
		Me.lbl_Restricted.Text = "Restricted:"
		'
		'chb_Restricted
		'
		Me.chb_Restricted.Cursor = System.Windows.Forms.Cursors.Hand
		Me.chb_Restricted.Location = New System.Drawing.Point(83, 49)
		Me.chb_Restricted.MKBoundLabel = Nothing
		Me.chb_Restricted.MKEditValue_Compare = Nothing
		Me.chb_Restricted.Name = "chb_Restricted"
		Me.chb_Restricted.Properties.Caption = ""
		Me.chb_Restricted.Size = New System.Drawing.Size(75, 19)
		ToolTipTitleItem6.Text = "Restricted"
		ToolTipItem6.LeftIndent = 6
		ToolTipItem6.Text = "Enable this option and the user will have restricted access to games and media."
		SuperToolTip6.Items.Add(ToolTipTitleItem6)
		SuperToolTip6.Items.Add(ToolTipItem6)
		Me.chb_Restricted.SuperTip = SuperToolTip6
		Me.chb_Restricted.TabIndex = 3
		'
		'chb_Password
		'
		Me.chb_Password.Cursor = System.Windows.Forms.Cursors.Hand
		Me.chb_Password.Location = New System.Drawing.Point(6, 26)
		Me.chb_Password.MKBoundLabel = Nothing
		Me.chb_Password.MKEditValue_Compare = Nothing
		Me.chb_Password.Name = "chb_Password"
		Me.chb_Password.Properties.Caption = ""
		Me.chb_Password.Size = New System.Drawing.Size(16, 19)
		ToolTipTitleItem7.Text = "Password"
		ToolTipItem7.LeftIndent = 6
		ToolTipItem7.Text = "Enable this option to (re-)define the password for the user. Leave it unchecked a" &
		"nd there will be no alterations on the user's password."
		SuperToolTip7.Items.Add(ToolTipTitleItem7)
		SuperToolTip7.Items.Add(ToolTipItem7)
		Me.chb_Password.SuperTip = SuperToolTip7
		Me.chb_Password.TabIndex = 1
		'
		'lbl_Challenges
		'
		Me.lbl_Challenges.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Challenges.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Challenges.Location = New System.Drawing.Point(24, 72)
		Me.lbl_Challenges.MKBoundControl1 = Nothing
		Me.lbl_Challenges.MKBoundControl2 = Nothing
		Me.lbl_Challenges.MKBoundControl3 = Nothing
		Me.lbl_Challenges.MKBoundControl4 = Nothing
		Me.lbl_Challenges.MKBoundControl5 = Nothing
		Me.lbl_Challenges.Name = "lbl_Challenges"
		Me.lbl_Challenges.Size = New System.Drawing.Size(56, 20)
		ToolTipTitleItem8.Text = "Challenge"
		ToolTipItem8.LeftIndent = 6
		ToolTipItem8.Text = "The user will be bound to a selected challenge. Only the games of that challenge " &
		"will be visible."
		SuperToolTip8.Items.Add(ToolTipTitleItem8)
		SuperToolTip8.Items.Add(ToolTipItem8)
		Me.lbl_Challenges.SuperTip = SuperToolTip8
		Me.lbl_Challenges.TabIndex = 6
		Me.lbl_Challenges.Text = "Challenge:"
		'
		'cmb_Challenges
		'
		Me.cmb_Challenges.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmb_Challenges.EditValue = CType(0, Long)
		Me.cmb_Challenges.Location = New System.Drawing.Point(83, 72)
		Me.cmb_Challenges.MKBoundLabel = Nothing
		Me.cmb_Challenges.MKEditValue_Compare = Nothing
		Me.cmb_Challenges.Name = "cmb_Challenges"
		Me.cmb_Challenges.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.cmb_Challenges.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_Cheevo_Challenges", "id_Cheevo_Challenges", 5, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name", 5, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near)})
		Me.cmb_Challenges.Properties.DataSource = Me.BTA_Challenges
		Me.cmb_Challenges.Properties.DisplayMember = "Name"
		Me.cmb_Challenges.Properties.NullText = ""
		Me.cmb_Challenges.Properties.ShowHeader = False
		Me.cmb_Challenges.Properties.ValueMember = "id_Cheevo_Challenges"
		Me.cmb_Challenges.Size = New System.Drawing.Size(210, 20)
		ToolTipTitleItem9.Text = "Challenge"
		ToolTipItem9.LeftIndent = 6
		ToolTipItem9.Text = "Filter by open RetroAchievements challenges"
		SuperToolTip9.Items.Add(ToolTipTitleItem9)
		SuperToolTip9.Items.Add(ToolTipItem9)
		Me.cmb_Challenges.SuperTip = SuperToolTip9
		Me.cmb_Challenges.TabIndex = 8
		'
		'BTA_Challenges
		'
		Me.BTA_Challenges.AllowDelete = True
		Me.BTA_Challenges.ColumnUpdateBlacklistStream = CType(resources.GetObject("BTA_Challenges.ColumnUpdateBlacklistStream"), System.Collections.Generic.List(Of String))
		Me.BTA_Challenges.Connection = Nothing
		Me.BTA_Challenges.DSStream = CType(resources.GetObject("BTA_Challenges.DSStream"), System.IO.MemoryStream)
		Me.BTA_Challenges.FillString = ""
		Me.BTA_Challenges.Transaction = Nothing
		Me.BTA_Challenges.UpdateTablesStream = CType(resources.GetObject("BTA_Challenges.UpdateTablesStream"), System.Collections.Generic.List(Of String))
		'
		'DataTable1
		'
		Me.DataTable1.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn1, Me.DataColumn2, Me.DataColumn3})
		Me.DataTable1.TableName = "Table1"
		'
		'DataColumn1
		'
		Me.DataColumn1.ColumnName = "id_Cheevo_Challenges"
		Me.DataColumn1.DataType = GetType(Long)
		'
		'DataColumn2
		'
		Me.DataColumn2.ColumnName = "Name"
		'
		'DataColumn3
		'
		Me.DataColumn3.ColumnName = "Sort"
		Me.DataColumn3.DataType = GetType(Long)
		'
		'frm_Users_Edit
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(303, 135)
		Me.Controls.Add(Me.cmb_Challenges)
		Me.Controls.Add(Me.lbl_Challenges)
		Me.Controls.Add(Me.chb_Password)
		Me.Controls.Add(Me.chb_Restricted)
		Me.Controls.Add(Me.btn_Cancel)
		Me.Controls.Add(Me.btn_OK)
		Me.Controls.Add(Me.txb_Username)
		Me.Controls.Add(Me.txb_Password)
		Me.Controls.Add(Me.lbl_Restricted)
		Me.Controls.Add(Me.lbl_Password)
		Me.Controls.Add(Me.lbl_Username)
		Me.Name = "frm_Users_Edit"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Edit User"
		CType(Me.txb_Password.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_Username.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.chb_Restricted.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.chb_Password.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.cmb_Challenges.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BTA_Challenges, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents btn_Cancel As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents txb_Password As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents lbl_Password As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Username As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents txb_Username As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents lbl_Restricted As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents chb_Restricted As MKNetDXLib.ctl_MKDXCheckEdit
	Friend WithEvents chb_Password As MKNetDXLib.ctl_MKDXCheckEdit
	Friend WithEvents lbl_Challenges As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents cmb_Challenges As MKNetDXLib.ctl_MKDXLookupEdit
	Friend WithEvents BTA_Challenges As MKNetLib.cmp_MKBindableTableAdapter
	Friend WithEvents DataTable1 As DataTable
	Friend WithEvents DataColumn1 As DataColumn
	Friend WithEvents DataColumn2 As DataColumn
	Friend WithEvents DataColumn3 As DataColumn
End Class
