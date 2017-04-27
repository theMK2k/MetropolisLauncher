<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DOSBox_Choose_NIC
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_DOSBox_Choose_NIC))
		Me.grd_NIC = New MKNetDXLib.ctl_MKDXGrid()
		Me.BTA_NIC = New MKNetLib.cmp_MKBindableTableAdapter(Me.components)
		Me.gv_NIC = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_Displaytext = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.rpi_Moby_Releases = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		Me.RepositoryItemLookUpEdit5 = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
		Me.rpi_MV_Volume = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		Me.pnl_Bottom = New MKNetDXLib.ctl_MKDXPanel()
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Cancel = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.lbl_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		Me.DataTable1 = New System.Data.DataTable()
		Me.DataColumn1 = New System.Data.DataColumn()
		CType(Me.grd_NIC, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BTA_NIC, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_NIC, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_Moby_Releases, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.RepositoryItemLookUpEdit5, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_MV_Volume, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.pnl_Bottom, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_Bottom.SuspendLayout()
		CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'grd_NIC
		'
		Me.grd_NIC.DataSource = Me.BTA_NIC
		Me.grd_NIC.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_NIC.EmbeddedNavigator.Buttons.Append.Visible = False
		Me.grd_NIC.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
		Me.grd_NIC.EmbeddedNavigator.Buttons.Edit.Visible = False
		Me.grd_NIC.EmbeddedNavigator.Buttons.EndEdit.Visible = False
		Me.grd_NIC.EmbeddedNavigator.Buttons.Remove.Visible = False
		Me.grd_NIC.EmbeddedNavigator.TextStringFormat = "{0} of {1}"
		Me.grd_NIC.Location = New System.Drawing.Point(0, 19)
		Me.grd_NIC.MainView = Me.gv_NIC
		Me.grd_NIC.Name = "grd_NIC"
		Me.grd_NIC.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rpi_Moby_Releases, Me.RepositoryItemLookUpEdit5, Me.RepositoryItemCheckEdit2, Me.rpi_MV_Volume})
		Me.grd_NIC.Size = New System.Drawing.Size(384, 352)
		Me.grd_NIC.TabIndex = 0
		Me.grd_NIC.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_NIC})
		'
		'BTA_NIC
		'
		Me.BTA_NIC.AllowDelete = True
		Me.BTA_NIC.ColumnUpdateBlacklistStream = CType(resources.GetObject("BTA_NIC.ColumnUpdateBlacklistStream"), System.Collections.Generic.List(Of String))
		Me.BTA_NIC.Connection = Nothing
		Me.BTA_NIC.DSStream = CType(resources.GetObject("BTA_NIC.DSStream"), System.IO.MemoryStream)
		Me.BTA_NIC.FillString = ""
		Me.BTA_NIC.Transaction = Nothing
		Me.BTA_NIC.UpdateTablesStream = CType(resources.GetObject("BTA_NIC.UpdateTablesStream"), System.Collections.Generic.List(Of String))
		'
		'gv_NIC
		'
		Me.gv_NIC.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_Displaytext})
		Me.gv_NIC.GridControl = Me.grd_NIC
		Me.gv_NIC.Name = "gv_NIC"
		Me.gv_NIC.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_NIC.OptionsSelection.InvertSelection = True
		Me.gv_NIC.OptionsView.ShowColumnHeaders = False
		Me.gv_NIC.OptionsView.ShowGroupPanel = False
		Me.gv_NIC.OptionsView.ShowIndicator = False
		'
		'col_Displaytext
		'
		Me.col_Displaytext.Caption = "Network Interface Cards"
		Me.col_Displaytext.FieldName = "Displaytext"
		Me.col_Displaytext.Name = "col_Displaytext"
		Me.col_Displaytext.OptionsColumn.AllowEdit = False
		Me.col_Displaytext.Visible = True
		Me.col_Displaytext.VisibleIndex = 0
		Me.col_Displaytext.Width = 163
		'
		'rpi_Moby_Releases
		'
		Me.rpi_Moby_Releases.AutoHeight = False
		Me.rpi_Moby_Releases.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.rpi_Moby_Releases.DisplayMember = "Gamename"
		Me.rpi_Moby_Releases.Name = "rpi_Moby_Releases"
		Me.rpi_Moby_Releases.NullText = ""
		Me.rpi_Moby_Releases.ValueMember = "Moby_Games_URLPart"
		'
		'RepositoryItemLookUpEdit5
		'
		Me.RepositoryItemLookUpEdit5.AutoHeight = False
		Me.RepositoryItemLookUpEdit5.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.RepositoryItemLookUpEdit5.DisplayMember = "Display_Name"
		Me.RepositoryItemLookUpEdit5.Name = "RepositoryItemLookUpEdit5"
		Me.RepositoryItemLookUpEdit5.ValueMember = "id_Moby_Platforms"
		'
		'RepositoryItemCheckEdit2
		'
		Me.RepositoryItemCheckEdit2.AutoHeight = False
		Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
		Me.RepositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
		'
		'rpi_MV_Volume
		'
		Me.rpi_MV_Volume.AutoHeight = False
		Me.rpi_MV_Volume.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
		Me.rpi_MV_Volume.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_Tag_Parser_Volumes", "id_Tag_Parser_Volumes", 137, DevExpress.Utils.FormatType.Numeric, "", False, DevExpress.Utils.HorzAlignment.Far), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayText", "Display Text", 69, DevExpress.Utils.FormatType.None, "", True, DevExpress.Utils.HorzAlignment.Near)})
		Me.rpi_MV_Volume.DisplayMember = "DisplayText"
		Me.rpi_MV_Volume.Name = "rpi_MV_Volume"
		Me.rpi_MV_Volume.NullText = "Not a volume"
		Me.rpi_MV_Volume.ShowHeader = False
		Me.rpi_MV_Volume.ValueMember = "id_Tag_Parser_Volumes"
		'
		'pnl_Bottom
		'
		Me.pnl_Bottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_Bottom.Controls.Add(Me.btn_OK)
		Me.pnl_Bottom.Controls.Add(Me.btn_Cancel)
		Me.pnl_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.pnl_Bottom.Location = New System.Drawing.Point(0, 371)
		Me.pnl_Bottom.Name = "pnl_Bottom"
		Me.pnl_Bottom.Size = New System.Drawing.Size(384, 29)
		Me.pnl_Bottom.TabIndex = 11
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.Location = New System.Drawing.Point(229, 3)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 23)
		Me.btn_OK.TabIndex = 0
		Me.btn_OK.Text = "&OK"
		'
		'btn_Cancel
		'
		Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Cancel.Location = New System.Drawing.Point(307, 3)
		Me.btn_Cancel.Name = "btn_Cancel"
		Me.btn_Cancel.Size = New System.Drawing.Size(75, 23)
		Me.btn_Cancel.TabIndex = 1
		Me.btn_Cancel.Text = "&Cancel"
		'
		'lbl_Explanation
		'
		Me.lbl_Explanation.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Explanation.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
		Me.lbl_Explanation.AutoEllipsis = True
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
		Me.lbl_Explanation.Size = New System.Drawing.Size(384, 19)
		Me.lbl_Explanation.TabIndex = 10
		Me.lbl_Explanation.Text = "Please select a network interface card from the list below."
		'
		'DataTable1
		'
		Me.DataTable1.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn1})
		Me.DataTable1.TableName = "Table1"
		'
		'DataColumn1
		'
		Me.DataColumn1.ColumnName = "Displaytext"
		'
		'frm_DOSBox_Choose_NIC
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(384, 400)
		Me.Controls.Add(Me.grd_NIC)
		Me.Controls.Add(Me.pnl_Bottom)
		Me.Controls.Add(Me.lbl_Explanation)
		Me.Name = "frm_DOSBox_Choose_NIC"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Choose NIC"
		CType(Me.grd_NIC, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BTA_NIC, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_NIC, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_Moby_Releases, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.RepositoryItemLookUpEdit5, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_MV_Volume, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pnl_Bottom, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_Bottom.ResumeLayout(False)
		CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Private WithEvents grd_NIC As MKNetDXLib.ctl_MKDXGrid
	Private WithEvents gv_NIC As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_Displaytext As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents rpi_Moby_Releases As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
	Friend WithEvents RepositoryItemLookUpEdit5 As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
	Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
	Friend WithEvents rpi_MV_Volume As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
	Friend WithEvents pnl_Bottom As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_Cancel As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents lbl_Explanation As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents BTA_NIC As MKNetLib.cmp_MKBindableTableAdapter
	Friend WithEvents DataTable1 As System.Data.DataTable
	Friend WithEvents DataColumn1 As System.Data.DataColumn
End Class
