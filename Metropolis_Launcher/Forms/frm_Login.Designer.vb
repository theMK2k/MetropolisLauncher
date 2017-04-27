<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Login
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
		Me.lbl_Users = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Password = New MKNetDXLib.ctl_MKDXLabel()
		Me.txb_Password = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Cancel = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.DS_ML = New Metropolis_Launcher.DS_ML()
		Me.BS_Users = New System.Windows.Forms.BindingSource(Me.components)
		Me.cmb_Users = New MKNetDXLib.ctl_MKDXLookupEdit()
		Me.Ctl_MKDXPanel1 = New MKNetDXLib.ctl_MKDXPanel()
		Me.lbl_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		CType(Me.txb_Password.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Users, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.cmb_Users.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Ctl_MKDXPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Ctl_MKDXPanel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'lbl_Users
		'
		Me.lbl_Users.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Users.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Users.Location = New System.Drawing.Point(5, 3)
		Me.lbl_Users.MKBoundControl1 = Nothing
		Me.lbl_Users.MKBoundControl2 = Nothing
		Me.lbl_Users.MKBoundControl3 = Nothing
		Me.lbl_Users.MKBoundControl4 = Nothing
		Me.lbl_Users.MKBoundControl5 = Nothing
		Me.lbl_Users.Name = "lbl_Users"
		Me.lbl_Users.Size = New System.Drawing.Size(77, 20)
		Me.lbl_Users.TabIndex = 1
		Me.lbl_Users.Text = "User:"
		'
		'lbl_Password
		'
		Me.lbl_Password.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Password.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Password.Location = New System.Drawing.Point(5, 26)
		Me.lbl_Password.MKBoundControl1 = Nothing
		Me.lbl_Password.MKBoundControl2 = Nothing
		Me.lbl_Password.MKBoundControl3 = Nothing
		Me.lbl_Password.MKBoundControl4 = Nothing
		Me.lbl_Password.MKBoundControl5 = Nothing
		Me.lbl_Password.Name = "lbl_Password"
		Me.lbl_Password.Size = New System.Drawing.Size(77, 20)
		Me.lbl_Password.TabIndex = 1
		Me.lbl_Password.Text = "Password:"
		'
		'txb_Password
		'
		Me.txb_Password.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_Password.Location = New System.Drawing.Point(83, 26)
		Me.txb_Password.MKBoundLabel = Nothing
		Me.txb_Password.MKEditValue_Compare = Nothing
		Me.txb_Password.Name = "txb_Password"
		Me.txb_Password.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
		Me.txb_Password.Size = New System.Drawing.Size(214, 20)
		Me.txb_Password.TabIndex = 1
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.Location = New System.Drawing.Point(144, 78)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 23)
		Me.btn_OK.TabIndex = 2
		Me.btn_OK.Text = "&OK"
		'
		'btn_Cancel
		'
		Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btn_Cancel.Location = New System.Drawing.Point(222, 78)
		Me.btn_Cancel.Name = "btn_Cancel"
		Me.btn_Cancel.Size = New System.Drawing.Size(75, 23)
		Me.btn_Cancel.TabIndex = 3
		Me.btn_Cancel.Text = "&Cancel"
		'
		'DS_ML
		'
		Me.DS_ML.DataSetName = "DS_ML"
		Me.DS_ML.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'BS_Users
		'
		Me.BS_Users.DataMember = "tbl_Users"
		Me.BS_Users.DataSource = Me.DS_ML
		'
		'cmb_Users
		'
		Me.cmb_Users.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmb_Users.Location = New System.Drawing.Point(83, 3)
		Me.cmb_Users.MKBoundLabel = Nothing
		Me.cmb_Users.MKEditValue_Compare = Nothing
		Me.cmb_Users.Name = "cmb_Users"
		Me.cmb_Users.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.cmb_Users.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_Users", "id_Users", 64, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Admin", "Admin", 39, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Username", "Username", 58, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Password", "Password", 56, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Restricted", "Restricted", 59, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far)})
		Me.cmb_Users.Properties.DataSource = Me.BS_Users
		Me.cmb_Users.Properties.DisplayMember = "Username"
		Me.cmb_Users.Properties.ShowFooter = False
		Me.cmb_Users.Properties.ShowHeader = False
		Me.cmb_Users.Properties.ValueMember = "id_Users"
		Me.cmb_Users.Size = New System.Drawing.Size(214, 20)
		Me.cmb_Users.TabIndex = 0
		'
		'Ctl_MKDXPanel1
		'
		Me.Ctl_MKDXPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.Ctl_MKDXPanel1.Controls.Add(Me.lbl_Users)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.lbl_Password)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.cmb_Users)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.txb_Password)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.btn_Cancel)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.btn_OK)
		Me.Ctl_MKDXPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Ctl_MKDXPanel1.Location = New System.Drawing.Point(0, 6)
		Me.Ctl_MKDXPanel1.Name = "Ctl_MKDXPanel1"
		Me.Ctl_MKDXPanel1.Size = New System.Drawing.Size(314, 111)
		Me.Ctl_MKDXPanel1.TabIndex = 4
		'
		'lbl_Explanation
		'
		Me.lbl_Explanation.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
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
		Me.lbl_Explanation.Size = New System.Drawing.Size(314, 6)
		Me.lbl_Explanation.TabIndex = 5
		'
		'frm_Login
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(314, 117)
		Me.Controls.Add(Me.Ctl_MKDXPanel1)
		Me.Controls.Add(Me.lbl_Explanation)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
		Me.Name = "frm_Login"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Welcome! Please log in..."
		CType(Me.txb_Password.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Users, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.cmb_Users.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Ctl_MKDXPanel1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Ctl_MKDXPanel1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents lbl_Users As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Password As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents txb_Password As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_Cancel As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents DS_ML As Metropolis_Launcher.DS_ML
	Friend WithEvents BS_Users As System.Windows.Forms.BindingSource
	Friend WithEvents cmb_Users As MKNetDXLib.ctl_MKDXLookupEdit
	Friend WithEvents Ctl_MKDXPanel1 As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents lbl_Explanation As MKNetDXLib.ctl_MKDXLabel
End Class
