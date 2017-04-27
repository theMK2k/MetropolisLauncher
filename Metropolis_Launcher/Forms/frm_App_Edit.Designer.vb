<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_App_Edit
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
		Me.BS_Apps = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_MLApps = New Metropolis_Launcher.DS_MLApps()
		Me.BS_Categories = New System.Windows.Forms.BindingSource(Me.components)
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Cancel = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.Ctl_MKDXLabel1 = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Exec = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_ExclusiveExecution = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Title = New MKNetDXLib.ctl_MKDXLabel()
		Me.txb_Application = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.txb_Executable = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.btn_Executable = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.chb_ExclusiveExecution = New MKNetDXLib.ctl_MKDXCheckEdit()
		Me.lbl_Category = New MKNetDXLib.ctl_MKDXLabel()
		Me.cmb_Category = New MKNetDXLib.ctl_MKDXLookupEdit()
		Me.lbl_Description = New MKNetDXLib.ctl_MKDXLabel()
		Me.txb_Description = New MKNetDXLib.ctl_MKDXMemoEdit()
		Me.Ctl_MKDXPanel1 = New MKNetDXLib.ctl_MKDXPanel()
		CType(Me.BS_Apps, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_MLApps, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Categories, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_Application.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_Executable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.chb_ExclusiveExecution.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.cmb_Category.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.txb_Description.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Ctl_MKDXPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Ctl_MKDXPanel1.SuspendLayout()
		Me.SuspendLayout()
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
		'BS_Categories
		'
		Me.BS_Categories.DataMember = "Categories"
		Me.BS_Categories.DataSource = Me.DS_MLApps
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.Location = New System.Drawing.Point(269, 356)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 23)
		Me.btn_OK.TabIndex = 7
		Me.btn_OK.Text = "&OK"
		'
		'btn_Cancel
		'
		Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Cancel.Location = New System.Drawing.Point(347, 356)
		Me.btn_Cancel.Name = "btn_Cancel"
		Me.btn_Cancel.Size = New System.Drawing.Size(75, 23)
		Me.btn_Cancel.TabIndex = 8
		Me.btn_Cancel.Text = "&Cancel"
		'
		'Ctl_MKDXLabel1
		'
		Me.Ctl_MKDXLabel1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.Ctl_MKDXLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.Ctl_MKDXLabel1.Location = New System.Drawing.Point(5, 76)
		Me.Ctl_MKDXLabel1.MKBoundControl1 = Nothing
		Me.Ctl_MKDXLabel1.MKBoundControl2 = Nothing
		Me.Ctl_MKDXLabel1.MKBoundControl3 = Nothing
		Me.Ctl_MKDXLabel1.MKBoundControl4 = Nothing
		Me.Ctl_MKDXLabel1.MKBoundControl5 = Nothing
		Me.Ctl_MKDXLabel1.Name = "Ctl_MKDXLabel1"
		Me.Ctl_MKDXLabel1.Size = New System.Drawing.Size(110, 20)
		Me.Ctl_MKDXLabel1.TabIndex = 1
		Me.Ctl_MKDXLabel1.Text = "Application:"
		'
		'lbl_Exec
		'
		Me.lbl_Exec.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Exec.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Exec.Location = New System.Drawing.Point(5, 99)
		Me.lbl_Exec.MKBoundControl1 = Nothing
		Me.lbl_Exec.MKBoundControl2 = Nothing
		Me.lbl_Exec.MKBoundControl3 = Nothing
		Me.lbl_Exec.MKBoundControl4 = Nothing
		Me.lbl_Exec.MKBoundControl5 = Nothing
		Me.lbl_Exec.Name = "lbl_Exec"
		Me.lbl_Exec.Size = New System.Drawing.Size(110, 20)
		Me.lbl_Exec.TabIndex = 1
		Me.lbl_Exec.Text = "Executable:"
		'
		'lbl_ExclusiveExecution
		'
		Me.lbl_ExclusiveExecution.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_ExclusiveExecution.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_ExclusiveExecution.Location = New System.Drawing.Point(5, 122)
		Me.lbl_ExclusiveExecution.MKBoundControl1 = Nothing
		Me.lbl_ExclusiveExecution.MKBoundControl2 = Nothing
		Me.lbl_ExclusiveExecution.MKBoundControl3 = Nothing
		Me.lbl_ExclusiveExecution.MKBoundControl4 = Nothing
		Me.lbl_ExclusiveExecution.MKBoundControl5 = Nothing
		Me.lbl_ExclusiveExecution.Name = "lbl_ExclusiveExecution"
		Me.lbl_ExclusiveExecution.Size = New System.Drawing.Size(110, 20)
		Me.lbl_ExclusiveExecution.TabIndex = 1
		Me.lbl_ExclusiveExecution.Text = "Exclusive Execution:"
		Me.lbl_ExclusiveExecution.ToolTip = "When ticked, the launcher will be hidden and waits until the application has been" &
		" closed."
		'
		'lbl_Title
		'
		Me.lbl_Title.Appearance.Font = New System.Drawing.Font("Segoe UI", 16.0!)
		Me.lbl_Title.AutoEllipsis = True
		Me.lbl_Title.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Title.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_Title.Location = New System.Drawing.Point(2, 2)
		Me.lbl_Title.MKBoundControl1 = Nothing
		Me.lbl_Title.MKBoundControl2 = Nothing
		Me.lbl_Title.MKBoundControl3 = Nothing
		Me.lbl_Title.MKBoundControl4 = Nothing
		Me.lbl_Title.MKBoundControl5 = Nothing
		Me.lbl_Title.Name = "lbl_Title"
		Me.lbl_Title.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_Title.Size = New System.Drawing.Size(421, 36)
		Me.lbl_Title.TabIndex = 2
		Me.lbl_Title.Text = "Edit Application"
		'
		'txb_Application
		'
		Me.txb_Application.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_Application.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_Apps, "DisplayName", True))
		Me.txb_Application.Location = New System.Drawing.Point(118, 76)
		Me.txb_Application.MKBoundLabel = Nothing
		Me.txb_Application.MKEditValue_Compare = Nothing
		Me.txb_Application.Name = "txb_Application"
		Me.txb_Application.Size = New System.Drawing.Size(304, 20)
		Me.txb_Application.TabIndex = 2
		'
		'txb_Executable
		'
		Me.txb_Executable.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_Executable.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_Apps, "Executable", True))
		Me.txb_Executable.Location = New System.Drawing.Point(118, 99)
		Me.txb_Executable.MKBoundLabel = Nothing
		Me.txb_Executable.MKEditValue_Compare = Nothing
		Me.txb_Executable.Name = "txb_Executable"
		Me.txb_Executable.Size = New System.Drawing.Size(261, 20)
		Me.txb_Executable.TabIndex = 3
		'
		'btn_Executable
		'
		Me.btn_Executable.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Executable.Location = New System.Drawing.Point(382, 99)
		Me.btn_Executable.Name = "btn_Executable"
		Me.btn_Executable.Size = New System.Drawing.Size(40, 20)
		Me.btn_Executable.TabIndex = 4
		Me.btn_Executable.Text = "..."
		'
		'chb_ExclusiveExecution
		'
		Me.chb_ExclusiveExecution.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.chb_ExclusiveExecution.Cursor = System.Windows.Forms.Cursors.Hand
		Me.chb_ExclusiveExecution.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_Apps, "RunExclusive", True))
		Me.chb_ExclusiveExecution.Location = New System.Drawing.Point(115, 122)
		Me.chb_ExclusiveExecution.MKBoundLabel = Nothing
		Me.chb_ExclusiveExecution.MKEditValue_Compare = Nothing
		Me.chb_ExclusiveExecution.Name = "chb_ExclusiveExecution"
		Me.chb_ExclusiveExecution.Properties.Caption = ""
		Me.chb_ExclusiveExecution.Size = New System.Drawing.Size(307, 19)
		Me.chb_ExclusiveExecution.TabIndex = 5
		Me.chb_ExclusiveExecution.ToolTip = "When ticked, the launcher will be hidden and waits until the application has been" &
		" closed."
		'
		'lbl_Category
		'
		Me.lbl_Category.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Category.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Category.Location = New System.Drawing.Point(4, 53)
		Me.lbl_Category.MKBoundControl1 = Nothing
		Me.lbl_Category.MKBoundControl2 = Nothing
		Me.lbl_Category.MKBoundControl3 = Nothing
		Me.lbl_Category.MKBoundControl4 = Nothing
		Me.lbl_Category.MKBoundControl5 = Nothing
		Me.lbl_Category.Name = "lbl_Category"
		Me.lbl_Category.Size = New System.Drawing.Size(110, 20)
		Me.lbl_Category.TabIndex = 5
		Me.lbl_Category.Text = "Category:"
		'
		'cmb_Category
		'
		Me.cmb_Category.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmb_Category.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_Apps, "id_Categories", True))
		Me.cmb_Category.Location = New System.Drawing.Point(118, 53)
		Me.cmb_Category.MKBoundLabel = Nothing
		Me.cmb_Category.MKEditValue_Compare = Nothing
		Me.cmb_Category.Name = "cmb_Category"
		Me.cmb_Category.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.cmb_Category.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_Categories", "id_Categories", 89, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Category", "Category", 55, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near)})
		Me.cmb_Category.Properties.DataSource = Me.BS_Categories
		Me.cmb_Category.Properties.DisplayMember = "Category"
		Me.cmb_Category.Properties.NullText = "[Please choose]"
		Me.cmb_Category.Properties.ShowFooter = False
		Me.cmb_Category.Properties.ShowHeader = False
		Me.cmb_Category.Properties.ValueMember = "id_Categories"
		Me.cmb_Category.Size = New System.Drawing.Size(304, 20)
		Me.cmb_Category.TabIndex = 0
		'
		'lbl_Description
		'
		Me.lbl_Description.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Description.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Description.Location = New System.Drawing.Point(5, 145)
		Me.lbl_Description.MKBoundControl1 = Nothing
		Me.lbl_Description.MKBoundControl2 = Nothing
		Me.lbl_Description.MKBoundControl3 = Nothing
		Me.lbl_Description.MKBoundControl4 = Nothing
		Me.lbl_Description.MKBoundControl5 = Nothing
		Me.lbl_Description.Name = "lbl_Description"
		Me.lbl_Description.Size = New System.Drawing.Size(110, 20)
		Me.lbl_Description.TabIndex = 1
		Me.lbl_Description.Text = "Description:"
		'
		'txb_Description
		'
		Me.txb_Description.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_Description.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.BS_Apps, "Description", True))
		Me.txb_Description.Location = New System.Drawing.Point(117, 145)
		Me.txb_Description.MKBoundLabel = Nothing
		Me.txb_Description.MKEditValue_Compare = Nothing
		Me.txb_Description.Name = "txb_Description"
		Me.txb_Description.Size = New System.Drawing.Size(305, 205)
		Me.txb_Description.TabIndex = 6
		'
		'Ctl_MKDXPanel1
		'
		Me.Ctl_MKDXPanel1.Controls.Add(Me.txb_Description)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.cmb_Category)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.lbl_Category)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.chb_ExclusiveExecution)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.txb_Executable)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.txb_Application)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.lbl_Title)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.lbl_Description)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.lbl_ExclusiveExecution)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.lbl_Exec)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.Ctl_MKDXLabel1)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.btn_Cancel)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.btn_Executable)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.btn_OK)
		Me.Ctl_MKDXPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Ctl_MKDXPanel1.Location = New System.Drawing.Point(0, 0)
		Me.Ctl_MKDXPanel1.Name = "Ctl_MKDXPanel1"
		Me.Ctl_MKDXPanel1.Size = New System.Drawing.Size(425, 383)
		Me.Ctl_MKDXPanel1.TabIndex = 8
		'
		'frm_App_Edit
		'
		Me.ClientSize = New System.Drawing.Size(425, 383)
		Me.Controls.Add(Me.Ctl_MKDXPanel1)
		Me.Name = "frm_App_Edit"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		CType(Me.BS_Apps, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_MLApps, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Categories, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_Application.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_Executable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.chb_ExclusiveExecution.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.cmb_Category.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.txb_Description.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Ctl_MKDXPanel1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Ctl_MKDXPanel1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents BS_Apps As System.Windows.Forms.BindingSource
	Friend WithEvents DS_MLApps As Metropolis_Launcher.DS_MLApps
	Friend WithEvents BS_Categories As System.Windows.Forms.BindingSource
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_Cancel As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents Ctl_MKDXLabel1 As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Exec As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_ExclusiveExecution As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Title As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents txb_Application As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents txb_Executable As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents btn_Executable As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents chb_ExclusiveExecution As MKNetDXLib.ctl_MKDXCheckEdit
	Friend WithEvents lbl_Category As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents cmb_Category As MKNetDXLib.ctl_MKDXLookupEdit
	Friend WithEvents lbl_Description As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents txb_Description As MKNetDXLib.ctl_MKDXMemoEdit
	Friend WithEvents Ctl_MKDXPanel1 As MKNetDXLib.ctl_MKDXPanel

End Class
