<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_Moby_Staff_Info
	Inherits MKNetDXLib.frm_MKDXBaseForm

	'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
	<System.Diagnostics.DebuggerNonUserCode()>
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
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Me.grd_Moby_Games = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_Moby_Games = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_Moby_Games = New System.Data.DataSet()
		Me.DataTable1 = New System.Data.DataTable()
		Me.DataColumn1 = New System.Data.DataColumn()
		Me.DataColumn2 = New System.Data.DataColumn()
		Me.DataColumn3 = New System.Data.DataColumn()
		Me.DataColumn4 = New System.Data.DataColumn()
		Me.gv_Moby_Games = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colName = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colPosition = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colPlatforms = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.btn_Close = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.lbl_Bio = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Staff_Name = New MKNetDXLib.ctl_MKDXLabel()
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
		Me.DataTable1.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn1, Me.DataColumn2, Me.DataColumn3, Me.DataColumn4})
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
		'DataColumn4
		'
		Me.DataColumn4.ColumnName = "Position"
		'
		'gv_Moby_Games
		'
		Me.gv_Moby_Games.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colName, Me.colPosition, Me.colPlatforms})
		Me.gv_Moby_Games.GridControl = Me.grd_Moby_Games
		Me.gv_Moby_Games.Name = "gv_Moby_Games"
		Me.gv_Moby_Games.OptionsView.ColumnAutoWidth = False
		Me.gv_Moby_Games.OptionsView.ShowGroupPanel = False
		Me.gv_Moby_Games.OptionsView.ShowIndicator = False
		'
		'colName
		'
		Me.colName.Caption = "Game"
		Me.colName.FieldName = "Name"
		Me.colName.Name = "colName"
		Me.colName.OptionsColumn.AllowEdit = False
		Me.colName.Visible = True
		Me.colName.VisibleIndex = 0
		Me.colName.Width = 152
		'
		'colPosition
		'
		Me.colPosition.FieldName = "Position"
		Me.colPosition.Name = "colPosition"
		Me.colPosition.OptionsColumn.AllowEdit = False
		Me.colPosition.Visible = True
		Me.colPosition.VisibleIndex = 1
		Me.colPosition.Width = 144
		'
		'colPlatforms
		'
		Me.colPlatforms.FieldName = "Platforms"
		Me.colPlatforms.Name = "colPlatforms"
		Me.colPlatforms.OptionsColumn.AllowEdit = False
		Me.colPlatforms.Visible = True
		Me.colPlatforms.VisibleIndex = 2
		Me.colPlatforms.Width = 323
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
		'lbl_Bio
		'
		Me.lbl_Bio.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_Bio.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_Bio.Location = New System.Drawing.Point(0, 36)
		Me.lbl_Bio.MKBoundControl1 = Nothing
		Me.lbl_Bio.MKBoundControl2 = Nothing
		Me.lbl_Bio.MKBoundControl3 = Nothing
		Me.lbl_Bio.MKBoundControl4 = Nothing
		Me.lbl_Bio.MKBoundControl5 = Nothing
		Me.lbl_Bio.Name = "lbl_Bio"
		Me.lbl_Bio.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.lbl_Bio.Size = New System.Drawing.Size(626, 16)
		Me.lbl_Bio.TabIndex = 0
		Me.lbl_Bio.Text = "[Biography]"
		'
		'lbl_Staff_Name
		'
		Me.lbl_Staff_Name.Appearance.Font = New System.Drawing.Font("Segoe UI", 16.0!)
		Me.lbl_Staff_Name.AutoEllipsis = True
		Me.lbl_Staff_Name.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Staff_Name.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_Staff_Name.Location = New System.Drawing.Point(0, 0)
		Me.lbl_Staff_Name.MKBoundControl1 = Nothing
		Me.lbl_Staff_Name.MKBoundControl2 = Nothing
		Me.lbl_Staff_Name.MKBoundControl3 = Nothing
		Me.lbl_Staff_Name.MKBoundControl4 = Nothing
		Me.lbl_Staff_Name.MKBoundControl5 = Nothing
		Me.lbl_Staff_Name.Name = "lbl_Staff_Name"
		Me.lbl_Staff_Name.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_Staff_Name.Size = New System.Drawing.Size(626, 36)
		Me.lbl_Staff_Name.TabIndex = 3
		Me.lbl_Staff_Name.Text = "[Staff Name]"
		'
		'pnl_Main
		'
		Me.pnl_Main.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.pnl_Main.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_Main.Controls.Add(Me.grd_Moby_Games)
		Me.pnl_Main.Controls.Add(Me.lbl_Bio)
		Me.pnl_Main.Controls.Add(Me.lbl_Staff_Name)
		Me.pnl_Main.Location = New System.Drawing.Point(3, 3)
		Me.pnl_Main.Name = "pnl_Main"
		Me.pnl_Main.Size = New System.Drawing.Size(626, 335)
		Me.pnl_Main.TabIndex = 11
		'
		'frm_Moby_Staff_Info
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(632, 367)
		Me.Controls.Add(Me.btn_Close)
		Me.Controls.Add(Me.pnl_Main)
		Me.Name = "frm_Moby_Staff_Info"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Staff Info"
		CType(Me.grd_Moby_Games, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Moby_Games, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_Moby_Games, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Moby_Games, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pnl_Main, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_Main.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents grd_Moby_Games As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents BS_Moby_Games As System.Windows.Forms.BindingSource
	Friend WithEvents DS_Moby_Games As System.Data.DataSet
	Friend WithEvents DataTable1 As System.Data.DataTable
	Friend WithEvents DataColumn1 As System.Data.DataColumn
	Friend WithEvents DataColumn2 As System.Data.DataColumn
	Friend WithEvents DataColumn3 As System.Data.DataColumn
	Friend WithEvents gv_Moby_Games As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents colName As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colPlatforms As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents btn_Close As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents lbl_Bio As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Staff_Name As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents pnl_Main As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents DataColumn4 As DataColumn
	Friend WithEvents colPosition As DevExpress.XtraGrid.Columns.GridColumn
End Class
