<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DOSBox_Choose_Exe
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
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Just_Mount = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.grd_DOSBox_Files_and_Folders = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_DOSBox_Files_and_Folders = New System.Windows.Forms.BindingSource(Me.components)
		Me.gv_DOSBox_Files_and_Folders = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_DOSBox_Displayname = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.rpi_Moby_Releases = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		Me.RepositoryItemLookUpEdit5 = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
		Me.rpi_MV_Volume = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		Me.lbl_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		Me.Ctl_MKDXPanel1 = New MKNetDXLib.ctl_MKDXPanel()
		CType(Me.grd_DOSBox_Files_and_Folders, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_DOSBox_Files_and_Folders, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_DOSBox_Files_and_Folders, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_Moby_Releases, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.RepositoryItemLookUpEdit5, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_MV_Volume, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Ctl_MKDXPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Ctl_MKDXPanel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.Location = New System.Drawing.Point(217, 3)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 23)
		Me.btn_OK.TabIndex = 0
		Me.btn_OK.Text = "&OK"
		'
		'btn_Just_Mount
		'
		Me.btn_Just_Mount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Just_Mount.Location = New System.Drawing.Point(295, 3)
		Me.btn_Just_Mount.Name = "btn_Just_Mount"
		Me.btn_Just_Mount.Size = New System.Drawing.Size(75, 23)
		Me.btn_Just_Mount.TabIndex = 1
		Me.btn_Just_Mount.Text = "&Just mount"
		'
		'grd_DOSBox_Files_and_Folders
		'
		Me.grd_DOSBox_Files_and_Folders.DataSource = Me.BS_DOSBox_Files_and_Folders
		Me.grd_DOSBox_Files_and_Folders.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_DOSBox_Files_and_Folders.EmbeddedNavigator.Buttons.Append.Visible = False
		Me.grd_DOSBox_Files_and_Folders.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
		Me.grd_DOSBox_Files_and_Folders.EmbeddedNavigator.Buttons.Edit.Visible = False
		Me.grd_DOSBox_Files_and_Folders.EmbeddedNavigator.Buttons.EndEdit.Visible = False
		Me.grd_DOSBox_Files_and_Folders.EmbeddedNavigator.Buttons.Remove.Visible = False
		Me.grd_DOSBox_Files_and_Folders.EmbeddedNavigator.TextStringFormat = "{0} of {1}"
		Me.grd_DOSBox_Files_and_Folders.Location = New System.Drawing.Point(0, 45)
		Me.grd_DOSBox_Files_and_Folders.MainView = Me.gv_DOSBox_Files_and_Folders
		Me.grd_DOSBox_Files_and_Folders.Name = "grd_DOSBox_Files_and_Folders"
		Me.grd_DOSBox_Files_and_Folders.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rpi_Moby_Releases, Me.RepositoryItemLookUpEdit5, Me.RepositoryItemCheckEdit2, Me.rpi_MV_Volume})
		Me.grd_DOSBox_Files_and_Folders.Size = New System.Drawing.Size(372, 310)
		Me.grd_DOSBox_Files_and_Folders.TabIndex = 0
		Me.grd_DOSBox_Files_and_Folders.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_DOSBox_Files_and_Folders})
		'
		'BS_DOSBox_Files_and_Folders
		'
		Me.BS_DOSBox_Files_and_Folders.Filter = ""
		'
		'gv_DOSBox_Files_and_Folders
		'
		Me.gv_DOSBox_Files_and_Folders.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_DOSBox_Displayname})
		Me.gv_DOSBox_Files_and_Folders.GridControl = Me.grd_DOSBox_Files_and_Folders
		Me.gv_DOSBox_Files_and_Folders.Name = "gv_DOSBox_Files_and_Folders"
		Me.gv_DOSBox_Files_and_Folders.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_DOSBox_Files_and_Folders.OptionsSelection.InvertSelection = True
		Me.gv_DOSBox_Files_and_Folders.OptionsView.ShowColumnHeaders = False
		Me.gv_DOSBox_Files_and_Folders.OptionsView.ShowGroupPanel = False
		Me.gv_DOSBox_Files_and_Folders.OptionsView.ShowIndicator = False
		'
		'col_DOSBox_Displayname
		'
		Me.col_DOSBox_Displayname.Caption = "File/Directory"
		Me.col_DOSBox_Displayname.Name = "col_DOSBox_Displayname"
		Me.col_DOSBox_Displayname.OptionsColumn.AllowEdit = False
		Me.col_DOSBox_Displayname.Visible = True
		Me.col_DOSBox_Displayname.VisibleIndex = 0
		Me.col_DOSBox_Displayname.Width = 163
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
		Me.lbl_Explanation.Size = New System.Drawing.Size(372, 45)
		Me.lbl_Explanation.TabIndex = 7
		Me.lbl_Explanation.Text = "Please select a file for autostart in the list below and press OK. If you choose " &
		"'Just mount', DOSBox will start and mount but won't autostart an executable."
		'
		'Ctl_MKDXPanel1
		'
		Me.Ctl_MKDXPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.Ctl_MKDXPanel1.Controls.Add(Me.btn_OK)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.btn_Just_Mount)
		Me.Ctl_MKDXPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.Ctl_MKDXPanel1.Location = New System.Drawing.Point(0, 355)
		Me.Ctl_MKDXPanel1.Name = "Ctl_MKDXPanel1"
		Me.Ctl_MKDXPanel1.Size = New System.Drawing.Size(372, 29)
		Me.Ctl_MKDXPanel1.TabIndex = 8
		'
		'frm_DOSBox_Choose_Exe
		'
		Me.ClientSize = New System.Drawing.Size(372, 384)
		Me.Controls.Add(Me.grd_DOSBox_Files_and_Folders)
		Me.Controls.Add(Me.Ctl_MKDXPanel1)
		Me.Controls.Add(Me.lbl_Explanation)
		Me.Name = "frm_DOSBox_Choose_Exe"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Choose an executable"
		CType(Me.grd_DOSBox_Files_and_Folders, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_DOSBox_Files_and_Folders, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_DOSBox_Files_and_Folders, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_Moby_Releases, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.RepositoryItemLookUpEdit5, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_MV_Volume, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Ctl_MKDXPanel1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Ctl_MKDXPanel1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_Just_Mount As MKNetDXLib.ctl_MKDXSimpleButton
	Private WithEvents grd_DOSBox_Files_and_Folders As MKNetDXLib.ctl_MKDXGrid
	Private WithEvents gv_DOSBox_Files_and_Folders As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_DOSBox_Displayname As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents rpi_Moby_Releases As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
	Friend WithEvents RepositoryItemLookUpEdit5 As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
	Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
	Friend WithEvents rpi_MV_Volume As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
	Friend WithEvents lbl_Explanation As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents Ctl_MKDXPanel1 As MKNetDXLib.ctl_MKDXPanel
	Public WithEvents BS_DOSBox_Files_and_Folders As System.Windows.Forms.BindingSource

End Class
