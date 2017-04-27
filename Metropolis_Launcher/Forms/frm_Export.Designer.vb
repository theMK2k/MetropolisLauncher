<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Export
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
		Me.btn_Export = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Close = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.lbl_Destination = New MKNetDXLib.ctl_MKDXLabel()
		Me.txb_Destination = New MKNetDXLib.ctl_MKDXTextEdit()
		Me.btn_Destination = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.lbl_Option = New MKNetDXLib.ctl_MKDXLabel()
		Me.cmb_Mode = New MKNetDXLib.ctl_MKDXLookupEdit()
		Me.BS_Mode = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_Export = New System.Data.DataSet()
		Me.tbl_Mode = New System.Data.DataTable()
		Me.DataColumn1 = New System.Data.DataColumn()
		Me.DataColumn2 = New System.Data.DataColumn()
		Me.lbl_Compression = New MKNetDXLib.ctl_MKDXLabel()
		Me.cmb_Compression = New MKNetDXLib.ctl_MKDXLookupEdit()
		Me.BS_Compression = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_Compression = New System.Data.DataSet()
		Me.tbl_Compression = New System.Data.DataTable()
		Me.DataColumn3 = New System.Data.DataColumn()
		Me.DataColumn4 = New System.Data.DataColumn()
		CType(Me.txb_Destination.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.cmb_Mode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Mode, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_Export, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.tbl_Mode, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.cmb_Compression.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Compression, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_Compression, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.tbl_Compression, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'btn_Export
		'
		Me.btn_Export.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Export.Location = New System.Drawing.Point(228, 76)
		Me.btn_Export.Name = "btn_Export"
		Me.btn_Export.Size = New System.Drawing.Size(75, 23)
		Me.btn_Export.TabIndex = 4
		Me.btn_Export.Text = "&Export"
		'
		'btn_Close
		'
		Me.btn_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Close.Location = New System.Drawing.Point(306, 76)
		Me.btn_Close.Name = "btn_Close"
		Me.btn_Close.Size = New System.Drawing.Size(75, 23)
		Me.btn_Close.TabIndex = 5
		Me.btn_Close.Text = "&Close"
		'
		'lbl_Destination
		'
		Me.lbl_Destination.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Destination.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Destination.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Destination.MKBoundControl1 = Nothing
		Me.lbl_Destination.MKBoundControl2 = Nothing
		Me.lbl_Destination.MKBoundControl3 = Nothing
		Me.lbl_Destination.MKBoundControl4 = Nothing
		Me.lbl_Destination.MKBoundControl5 = Nothing
		Me.lbl_Destination.Name = "lbl_Destination"
		Me.lbl_Destination.Size = New System.Drawing.Size(93, 20)
		Me.lbl_Destination.TabIndex = 1
		Me.lbl_Destination.Text = "Destination:"
		'
		'txb_Destination
		'
		Me.txb_Destination.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txb_Destination.Location = New System.Drawing.Point(99, 3)
		Me.txb_Destination.MKBoundLabel = Nothing
		Me.txb_Destination.MKEditValue_Compare = Nothing
		Me.txb_Destination.Name = "txb_Destination"
		Me.txb_Destination.Size = New System.Drawing.Size(250, 20)
		Me.txb_Destination.TabIndex = 0
		'
		'btn_Destination
		'
		Me.btn_Destination.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Destination.Location = New System.Drawing.Point(352, 3)
		Me.btn_Destination.Name = "btn_Destination"
		Me.btn_Destination.Size = New System.Drawing.Size(29, 20)
		Me.btn_Destination.TabIndex = 1
		Me.btn_Destination.Text = "..."
		'
		'lbl_Option
		'
		Me.lbl_Option.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Option.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Option.Location = New System.Drawing.Point(3, 26)
		Me.lbl_Option.MKBoundControl1 = Nothing
		Me.lbl_Option.MKBoundControl2 = Nothing
		Me.lbl_Option.MKBoundControl3 = Nothing
		Me.lbl_Option.MKBoundControl4 = Nothing
		Me.lbl_Option.MKBoundControl5 = Nothing
		Me.lbl_Option.Name = "lbl_Option"
		Me.lbl_Option.Size = New System.Drawing.Size(93, 20)
		Me.lbl_Option.TabIndex = 1
		Me.lbl_Option.Text = "Export Mode:"
		'
		'cmb_Mode
		'
		Me.cmb_Mode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmb_Mode.EditValue = "1"
		Me.cmb_Mode.Location = New System.Drawing.Point(99, 26)
		Me.cmb_Mode.MKBoundLabel = Nothing
		Me.cmb_Mode.MKEditValue_Compare = Nothing
		Me.cmb_Mode.Name = "cmb_Mode"
		Me.cmb_Mode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.cmb_Mode.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "id", 31, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Text", "Text", 32, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near)})
		Me.cmb_Mode.Properties.DataSource = Me.BS_Mode
		Me.cmb_Mode.Properties.DisplayMember = "Text"
		Me.cmb_Mode.Properties.ShowHeader = False
		Me.cmb_Mode.Properties.ValueMember = "id"
		Me.cmb_Mode.Size = New System.Drawing.Size(250, 20)
		Me.cmb_Mode.TabIndex = 2
		'
		'BS_Mode
		'
		Me.BS_Mode.DataMember = "tbl_Mode"
		Me.BS_Mode.DataSource = Me.DS_Export
		'
		'DS_Export
		'
		Me.DS_Export.DataSetName = "NewDataSet"
		Me.DS_Export.Tables.AddRange(New System.Data.DataTable() {Me.tbl_Mode})
		'
		'tbl_Mode
		'
		Me.tbl_Mode.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn1, Me.DataColumn2})
		Me.tbl_Mode.TableName = "tbl_Mode"
		'
		'DataColumn1
		'
		Me.DataColumn1.ColumnName = "id"
		Me.DataColumn1.DataType = GetType(Integer)
		'
		'DataColumn2
		'
		Me.DataColumn2.ColumnName = "Text"
		'
		'lbl_Compression
		'
		Me.lbl_Compression.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Compression.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Compression.Location = New System.Drawing.Point(3, 49)
		Me.lbl_Compression.MKBoundControl1 = Nothing
		Me.lbl_Compression.MKBoundControl2 = Nothing
		Me.lbl_Compression.MKBoundControl3 = Nothing
		Me.lbl_Compression.MKBoundControl4 = Nothing
		Me.lbl_Compression.MKBoundControl5 = Nothing
		Me.lbl_Compression.Name = "lbl_Compression"
		Me.lbl_Compression.Size = New System.Drawing.Size(93, 20)
		Me.lbl_Compression.TabIndex = 1
		Me.lbl_Compression.Text = "Compression:"
		'
		'cmb_Compression
		'
		Me.cmb_Compression.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmb_Compression.EditValue = 46
		Me.cmb_Compression.Location = New System.Drawing.Point(99, 49)
		Me.cmb_Compression.MKBoundLabel = Nothing
		Me.cmb_Compression.MKEditValue_Compare = Nothing
		Me.cmb_Compression.Name = "cmb_Compression"
		Me.cmb_Compression.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.cmb_Compression.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("value", "value", 5, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("displaytext", "displaytext", 5, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near)})
		Me.cmb_Compression.Properties.DataSource = Me.BS_Compression
		Me.cmb_Compression.Properties.DisplayMember = "displaytext"
		Me.cmb_Compression.Properties.ShowHeader = False
		Me.cmb_Compression.Properties.ValueMember = "value"
		Me.cmb_Compression.Size = New System.Drawing.Size(250, 20)
		Me.cmb_Compression.TabIndex = 3
		'
		'BS_Compression
		'
		Me.BS_Compression.DataMember = "tbl_Compression"
		Me.BS_Compression.DataSource = Me.DS_Compression
		'
		'DS_Compression
		'
		Me.DS_Compression.DataSetName = "NewDataSet"
		Me.DS_Compression.Tables.AddRange(New System.Data.DataTable() {Me.tbl_Compression})
		'
		'tbl_Compression
		'
		Me.tbl_Compression.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn3, Me.DataColumn4})
		Me.tbl_Compression.TableName = "tbl_Compression"
		'
		'DataColumn3
		'
		Me.DataColumn3.ColumnName = "value"
		Me.DataColumn3.DataType = GetType(Integer)
		'
		'DataColumn4
		'
		Me.DataColumn4.ColumnName = "displaytext"
		'
		'frm_Export
		'
		Me.ClientSize = New System.Drawing.Size(384, 102)
		Me.Controls.Add(Me.cmb_Compression)
		Me.Controls.Add(Me.cmb_Mode)
		Me.Controls.Add(Me.txb_Destination)
		Me.Controls.Add(Me.lbl_Compression)
		Me.Controls.Add(Me.lbl_Option)
		Me.Controls.Add(Me.lbl_Destination)
		Me.Controls.Add(Me.btn_Close)
		Me.Controls.Add(Me.btn_Destination)
		Me.Controls.Add(Me.btn_Export)
		Me.MinimumSize = New System.Drawing.Size(400, 140)
		Me.Name = "frm_Export"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Export games"
		CType(Me.txb_Destination.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.cmb_Mode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Mode, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_Export, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.tbl_Mode, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.cmb_Compression.Properties, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Compression, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_Compression, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.tbl_Compression, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents btn_Export As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_Close As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents lbl_Destination As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents txb_Destination As MKNetDXLib.ctl_MKDXTextEdit
	Friend WithEvents btn_Destination As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents lbl_Option As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents cmb_Mode As MKNetDXLib.ctl_MKDXLookupEdit
	Friend WithEvents BS_Mode As System.Windows.Forms.BindingSource
	Friend WithEvents DS_Export As System.Data.DataSet
	Friend WithEvents tbl_Mode As System.Data.DataTable
	Friend WithEvents DataColumn1 As System.Data.DataColumn
	Friend WithEvents DataColumn2 As System.Data.DataColumn
	Friend WithEvents lbl_Compression As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents cmb_Compression As MKNetDXLib.ctl_MKDXLookupEdit
	Friend WithEvents BS_Compression As System.Windows.Forms.BindingSource
	Friend WithEvents DS_Compression As System.Data.DataSet
	Friend WithEvents tbl_Compression As System.Data.DataTable
	Friend WithEvents DataColumn3 As System.Data.DataColumn
	Friend WithEvents DataColumn4 As System.Data.DataColumn

End Class
