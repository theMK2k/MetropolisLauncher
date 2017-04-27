<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Search_Missing_Extras
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
		Me.lbl_Platform = New MKNetDXLib.ctl_MKDXLabel()
		Me.cmb_Extra_Type = New MKNetDXLib.ctl_MKDXLookupEdit()
		Me.BS_Extra_Type = New System.Windows.Forms.BindingSource()
		Me.DS_ML = New Metropolis_Launcher.DS_ML()
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Cancel = New MKNetDXLib.ctl_MKDXSimpleButton()
		CType(Me.cmb_Extra_Type.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Extra_Type, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'lbl_Platform
		'
		Me.lbl_Platform.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Platform.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Platform.Location = New System.Drawing.Point(4, 3)
		Me.lbl_Platform.MKBoundControl1 = Nothing
		Me.lbl_Platform.MKBoundControl2 = Nothing
		Me.lbl_Platform.MKBoundControl3 = Nothing
		Me.lbl_Platform.MKBoundControl4 = Nothing
		Me.lbl_Platform.MKBoundControl5 = Nothing
		Me.lbl_Platform.Name = "lbl_Platform"
		Me.lbl_Platform.Size = New System.Drawing.Size(256, 20)
		Me.lbl_Platform.TabIndex = 5
		Me.lbl_Platform.Text = "Search for missing Extras in the following category:"
		'
		'cmb_Extra_Type
		'
		Me.cmb_Extra_Type.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmb_Extra_Type.Location = New System.Drawing.Point(263, 3)
		Me.cmb_Extra_Type.MKBoundLabel = Nothing
		Me.cmb_Extra_Type.MKEditValue_Compare = Nothing
		Me.cmb_Extra_Type.Name = "cmb_Extra_Type"
		Me.cmb_Extra_Type.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.cmb_Extra_Type.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_Emu_Extras", "id_Emu_Extras", 94, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name", 37, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Sort", "Sort", 30, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description", "Description", 63, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Hide", "Hide", 31, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.Near)})
		Me.cmb_Extra_Type.Properties.DataSource = Me.BS_Extra_Type
		Me.cmb_Extra_Type.Properties.DisplayMember = "Name"
		Me.cmb_Extra_Type.Properties.ValueMember = "id_Emu_Extras"
		Me.cmb_Extra_Type.Size = New System.Drawing.Size(250, 20)
		Me.cmb_Extra_Type.TabIndex = 0
		'
		'BS_Extra_Type
		'
		Me.BS_Extra_Type.DataMember = "tbl_Emu_Extras"
		Me.BS_Extra_Type.DataSource = Me.DS_ML
		'
		'DS_ML
		'
		Me.DS_ML.DataSetName = "DS_ML"
		Me.DS_ML.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'btn_OK
		'
		Me.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btn_OK.Location = New System.Drawing.Point(360, 51)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 23)
		Me.btn_OK.TabIndex = 1
		Me.btn_OK.Text = "&OK"
		'
		'btn_Cancel
		'
		Me.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btn_Cancel.Location = New System.Drawing.Point(438, 51)
		Me.btn_Cancel.Name = "btn_Cancel"
		Me.btn_Cancel.Size = New System.Drawing.Size(75, 23)
		Me.btn_Cancel.TabIndex = 2
		Me.btn_Cancel.Text = "&Cancel"
		'
		'frm_Search_Missing_Extras
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(516, 77)
		Me.Controls.Add(Me.btn_Cancel)
		Me.Controls.Add(Me.btn_OK)
		Me.Controls.Add(Me.cmb_Extra_Type)
		Me.Controls.Add(Me.lbl_Platform)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frm_Search_Missing_Extras"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Search for missing Extras"
		CType(Me.cmb_Extra_Type.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Extra_Type, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents lbl_Platform As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents cmb_Extra_Type As MKNetDXLib.ctl_MKDXLookupEdit
	Friend WithEvents BS_Extra_Type As System.Windows.Forms.BindingSource
	Friend WithEvents DS_ML As Metropolis_Launcher.DS_ML
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_Cancel As MKNetDXLib.ctl_MKDXSimpleButton
End Class
