<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Moby_Game_Group_Info
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
		Me.lbl_GoupName = New MKNetDXLib.ctl_MKDXLabel()
		Me.btn_Close = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.lbl_Description = New MKNetDXLib.ctl_MKDXLabel()
		Me.grd_Moby_Games = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_Moby_Games = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_Moby_Games = New System.Data.DataSet()
		Me.DataTable1 = New System.Data.DataTable()
		Me.DataColumn1 = New System.Data.DataColumn()
		Me.DataColumn2 = New System.Data.DataColumn()
		Me.DataColumn3 = New System.Data.DataColumn()
		Me.gv_Moby_Games = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colName = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colPlatforms = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.pnl_Main = New MKNetDXLib.ctl_MKDXPanel()
		CType(Me.grd_Moby_Games, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Moby_Games, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_Moby_Games, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_Moby_Games, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.pnl_Main, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_Main.SuspendLayout()
		Me.SuspendLayout()
		'
		'lbl_GoupName
		'
		Me.lbl_GoupName.Appearance.Font = New System.Drawing.Font("Segoe UI", 16.0!)
		Me.lbl_GoupName.AutoEllipsis = True
		Me.lbl_GoupName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_GoupName.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_GoupName.Location = New System.Drawing.Point(0, 0)
		Me.lbl_GoupName.MKBoundControl1 = Nothing
		Me.lbl_GoupName.MKBoundControl2 = Nothing
		Me.lbl_GoupName.MKBoundControl3 = Nothing
		Me.lbl_GoupName.MKBoundControl4 = Nothing
		Me.lbl_GoupName.MKBoundControl5 = Nothing
		Me.lbl_GoupName.Name = "lbl_GoupName"
		Me.lbl_GoupName.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_GoupName.Size = New System.Drawing.Size(626, 36)
		Me.lbl_GoupName.TabIndex = 3
		Me.lbl_GoupName.Text = "[Groupname]"
		'
		'btn_Close
		'
		Me.btn_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btn_Close.Location = New System.Drawing.Point(554, 341)
		Me.btn_Close.Name = "btn_Close"
		Me.btn_Close.Size = New System.Drawing.Size(75, 23)
		Me.btn_Close.TabIndex = 0
		Me.btn_Close.Text = "&Close"
		'
		'lbl_Description
		'
		Me.lbl_Description.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_Description.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_Description.Location = New System.Drawing.Point(0, 36)
		Me.lbl_Description.MKBoundControl1 = Nothing
		Me.lbl_Description.MKBoundControl2 = Nothing
		Me.lbl_Description.MKBoundControl3 = Nothing
		Me.lbl_Description.MKBoundControl4 = Nothing
		Me.lbl_Description.MKBoundControl5 = Nothing
		Me.lbl_Description.Name = "lbl_Description"
		Me.lbl_Description.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.lbl_Description.Size = New System.Drawing.Size(626, 16)
		Me.lbl_Description.TabIndex = 0
		Me.lbl_Description.Text = "[Description]"
		'
		'grd_Moby_Games
		'
		Me.grd_Moby_Games.DataSource = Me.BS_Moby_Games
		Me.grd_Moby_Games.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_Moby_Games.Location = New System.Drawing.Point(0, 52)
		Me.grd_Moby_Games.MainView = Me.gv_Moby_Games
		Me.grd_Moby_Games.Name = "grd_Moby_Games"
		Me.grd_Moby_Games.Size = New System.Drawing.Size(626, 283)
		Me.grd_Moby_Games.TabIndex = 0
		Me.grd_Moby_Games.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Moby_Games})
		'
		'BS_Moby_Games
		'
		Me.BS_Moby_Games.DataMember = "src_frm_Moby_Game_Group_Info"
		Me.BS_Moby_Games.DataSource = Me.DS_Moby_Games
		'
		'DS_Moby_Games
		'
		Me.DS_Moby_Games.DataSetName = "DS_Moby_Games"
		Me.DS_Moby_Games.Tables.AddRange(New System.Data.DataTable() {Me.DataTable1})
		'
		'DataTable1
		'
		Me.DataTable1.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn1, Me.DataColumn2, Me.DataColumn3})
		Me.DataTable1.TableName = "src_frm_Moby_Game_Group_Info"
		'
		'DataColumn1
		'
		Me.DataColumn1.ColumnName = "id_Moby_Games"
		Me.DataColumn1.DataType = GetType(Long)
		'
		'DataColumn2
		'
		Me.DataColumn2.ColumnName = "Name"
		'
		'DataColumn3
		'
		Me.DataColumn3.ColumnName = "Platforms"
		'
		'gv_Moby_Games
		'
		Me.gv_Moby_Games.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colName, Me.colPlatforms})
		Me.gv_Moby_Games.GridControl = Me.grd_Moby_Games
		Me.gv_Moby_Games.Name = "gv_Moby_Games"
		Me.gv_Moby_Games.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_Moby_Games.OptionsView.ShowGroupPanel = False
		Me.gv_Moby_Games.OptionsView.ShowIndicator = False
		'
		'colName
		'
		Me.colName.Caption = "Game"
		Me.colName.FieldName = "Name"
		Me.colName.Name = "colName"
		Me.colName.OptionsColumn.AllowEdit = False
		Me.colName.OptionsColumn.ReadOnly = True
		Me.colName.Visible = True
		Me.colName.VisibleIndex = 0
		'
		'colPlatforms
		'
		Me.colPlatforms.FieldName = "Platforms"
		Me.colPlatforms.Name = "colPlatforms"
		Me.colPlatforms.OptionsColumn.AllowEdit = False
		Me.colPlatforms.OptionsColumn.ReadOnly = True
		Me.colPlatforms.Visible = True
		Me.colPlatforms.VisibleIndex = 1
		'
		'pnl_Main
		'
		Me.pnl_Main.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.pnl_Main.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_Main.Controls.Add(Me.grd_Moby_Games)
		Me.pnl_Main.Controls.Add(Me.lbl_Description)
		Me.pnl_Main.Controls.Add(Me.lbl_GoupName)
		Me.pnl_Main.Location = New System.Drawing.Point(3, 3)
		Me.pnl_Main.Name = "pnl_Main"
		Me.pnl_Main.Size = New System.Drawing.Size(626, 335)
		Me.pnl_Main.TabIndex = 9
		'
		'frm_Moby_Game_Group_Info
		'
		Me.ClientSize = New System.Drawing.Size(632, 367)
		Me.Controls.Add(Me.pnl_Main)
		Me.Controls.Add(Me.btn_Close)
		Me.Name = "frm_Moby_Game_Group_Info"
		Me.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Game Group Info"
		CType(Me.grd_Moby_Games, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Moby_Games, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_Moby_Games, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Moby_Games, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pnl_Main, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_Main.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents lbl_GoupName As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents btn_Close As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents lbl_Description As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents grd_Moby_Games As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_Moby_Games As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents pnl_Main As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents BS_Moby_Games As System.Windows.Forms.BindingSource
	Friend WithEvents DS_Moby_Games As System.Data.DataSet
	Friend WithEvents DataTable1 As System.Data.DataTable
	Friend WithEvents DataColumn1 As System.Data.DataColumn
	Friend WithEvents DataColumn2 As System.Data.DataColumn
	Friend WithEvents DataColumn3 As System.Data.DataColumn
	Friend WithEvents colName As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colPlatforms As DevExpress.XtraGrid.Columns.GridColumn

End Class
