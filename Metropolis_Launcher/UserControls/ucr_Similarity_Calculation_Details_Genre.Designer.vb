<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucr_Similarity_Calculation_Details_Genre
	Inherits MKNetDXLib.ctl_MKDXUserControl

	'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucr_Similarity_Calculation_Details_Genre))
		Me.lbl_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		Me.pnl_Details = New MKNetDXLib.ctl_MKDXPanel()
		Me.tlp_Main = New System.Windows.Forms.TableLayoutPanel()
		Me.gb_AB = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_AB = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_AB = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_AB_Name = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.gb_B = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_B = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_B = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_B_Name = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.gb_A = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_A = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_A = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_A_Name = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.lbl_Weight = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Weight_Text = New MKNetDXLib.ctl_MKDXLabel()
		CType(Me.pnl_Details, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_Details.SuspendLayout()
		Me.tlp_Main.SuspendLayout()
		CType(Me.gb_AB, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_AB.SuspendLayout()
		CType(Me.grd_AB, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_AB, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gb_B, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_B.SuspendLayout()
		CType(Me.grd_B, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_B, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gb_A, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_A.SuspendLayout()
		CType(Me.grd_A, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_A, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'lbl_Explanation
		'
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
		Me.lbl_Explanation.Size = New System.Drawing.Size(383, 97)
		Me.lbl_Explanation.TabIndex = 1
		Me.lbl_Explanation.Text = resources.GetString("lbl_Explanation.Text")
		'
		'pnl_Details
		'
		Me.pnl_Details.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_Details.Controls.Add(Me.tlp_Main)
		Me.pnl_Details.Controls.Add(Me.lbl_Weight)
		Me.pnl_Details.Controls.Add(Me.lbl_Weight_Text)
		Me.pnl_Details.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_Details.Location = New System.Drawing.Point(0, 97)
		Me.pnl_Details.Name = "pnl_Details"
		Me.pnl_Details.Size = New System.Drawing.Size(383, 377)
		Me.pnl_Details.TabIndex = 2
		'
		'tlp_Main
		'
		Me.tlp_Main.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tlp_Main.ColumnCount = 1
		Me.tlp_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tlp_Main.Controls.Add(Me.gb_AB, 0, 2)
		Me.tlp_Main.Controls.Add(Me.gb_B, 0, 1)
		Me.tlp_Main.Controls.Add(Me.gb_A, 0, 0)
		Me.tlp_Main.Location = New System.Drawing.Point(0, 27)
		Me.tlp_Main.Name = "tlp_Main"
		Me.tlp_Main.RowCount = 3
		Me.tlp_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
		Me.tlp_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
		Me.tlp_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
		Me.tlp_Main.Size = New System.Drawing.Size(383, 351)
		Me.tlp_Main.TabIndex = 8
		'
		'gb_AB
		'
		Me.gb_AB.Controls.Add(Me.grd_AB)
		Me.gb_AB.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gb_AB.Location = New System.Drawing.Point(3, 237)
		Me.gb_AB.Name = "gb_AB"
		Me.gb_AB.Size = New System.Drawing.Size(377, 111)
		Me.gb_AB.TabIndex = 2
		Me.gb_AB.Text = "%%Category_Name%% shared by Games A and B"
		'
		'grd_AB
		'
		Me.grd_AB.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_AB.Location = New System.Drawing.Point(2, 20)
		Me.grd_AB.MainView = Me.gv_AB
		Me.grd_AB.Name = "grd_AB"
		Me.grd_AB.Size = New System.Drawing.Size(373, 89)
		Me.grd_AB.TabIndex = 3
		Me.grd_AB.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_AB})
		'
		'gv_AB
		'
		Me.gv_AB.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_AB_Name})
		Me.gv_AB.GridControl = Me.grd_AB
		Me.gv_AB.Name = "gv_AB"
		Me.gv_AB.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_AB.OptionsSelection.InvertSelection = True
		Me.gv_AB.OptionsView.ShowColumnHeaders = False
		Me.gv_AB.OptionsView.ShowGroupPanel = False
		Me.gv_AB.OptionsView.ShowIndicator = False
		'
		'col_AB_Name
		'
		Me.col_AB_Name.FieldName = "Name"
		Me.col_AB_Name.Name = "col_AB_Name"
		Me.col_AB_Name.OptionsColumn.AllowEdit = False
		Me.col_AB_Name.OptionsColumn.ReadOnly = True
		Me.col_AB_Name.Visible = True
		Me.col_AB_Name.VisibleIndex = 0
		'
		'gb_B
		'
		Me.gb_B.Controls.Add(Me.grd_B)
		Me.gb_B.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gb_B.Location = New System.Drawing.Point(3, 120)
		Me.gb_B.Name = "gb_B"
		Me.gb_B.Size = New System.Drawing.Size(377, 111)
		Me.gb_B.TabIndex = 1
		Me.gb_B.Text = "%%Category_Name%% of Game B"
		'
		'grd_B
		'
		Me.grd_B.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_B.Location = New System.Drawing.Point(2, 20)
		Me.grd_B.MainView = Me.gv_B
		Me.grd_B.Name = "grd_B"
		Me.grd_B.Size = New System.Drawing.Size(373, 89)
		Me.grd_B.TabIndex = 2
		Me.grd_B.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_B})
		'
		'gv_B
		'
		Me.gv_B.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_B_Name})
		Me.gv_B.GridControl = Me.grd_B
		Me.gv_B.Name = "gv_B"
		Me.gv_B.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_B.OptionsSelection.InvertSelection = True
		Me.gv_B.OptionsView.ShowColumnHeaders = False
		Me.gv_B.OptionsView.ShowGroupPanel = False
		Me.gv_B.OptionsView.ShowIndicator = False
		'
		'col_B_Name
		'
		Me.col_B_Name.FieldName = "Name"
		Me.col_B_Name.Name = "col_B_Name"
		Me.col_B_Name.OptionsColumn.AllowEdit = False
		Me.col_B_Name.OptionsColumn.ReadOnly = True
		Me.col_B_Name.Visible = True
		Me.col_B_Name.VisibleIndex = 0
		'
		'gb_A
		'
		Me.gb_A.Controls.Add(Me.grd_A)
		Me.gb_A.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gb_A.Location = New System.Drawing.Point(3, 3)
		Me.gb_A.Name = "gb_A"
		Me.gb_A.Size = New System.Drawing.Size(377, 111)
		Me.gb_A.TabIndex = 0
		Me.gb_A.Text = "%%Category_Name%% of Game A"
		'
		'grd_A
		'
		Me.grd_A.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_A.Location = New System.Drawing.Point(2, 20)
		Me.grd_A.MainView = Me.gv_A
		Me.grd_A.Name = "grd_A"
		Me.grd_A.Size = New System.Drawing.Size(373, 89)
		Me.grd_A.TabIndex = 1
		Me.grd_A.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_A})
		'
		'gv_A
		'
		Me.gv_A.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_A_Name})
		Me.gv_A.GridControl = Me.grd_A
		Me.gv_A.Name = "gv_A"
		Me.gv_A.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_A.OptionsSelection.InvertSelection = True
		Me.gv_A.OptionsView.ShowColumnHeaders = False
		Me.gv_A.OptionsView.ShowGroupPanel = False
		Me.gv_A.OptionsView.ShowIndicator = False
		'
		'col_A_Name
		'
		Me.col_A_Name.FieldName = "Name"
		Me.col_A_Name.Name = "col_A_Name"
		Me.col_A_Name.OptionsColumn.AllowEdit = False
		Me.col_A_Name.OptionsColumn.ReadOnly = True
		Me.col_A_Name.Visible = True
		Me.col_A_Name.VisibleIndex = 0
		'
		'lbl_Weight
		'
		Me.lbl_Weight.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Weight.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Weight.MKBoundControl1 = Nothing
		Me.lbl_Weight.MKBoundControl2 = Nothing
		Me.lbl_Weight.MKBoundControl3 = Nothing
		Me.lbl_Weight.MKBoundControl4 = Nothing
		Me.lbl_Weight.MKBoundControl5 = Nothing
		Me.lbl_Weight.Name = "lbl_Weight"
		Me.lbl_Weight.Size = New System.Drawing.Size(45, 20)
		Me.lbl_Weight.TabIndex = 7
		Me.lbl_Weight.Text = "Weight:"
		'
		'lbl_Weight_Text
		'
		Me.lbl_Weight_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Weight_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Weight_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_Text.Location = New System.Drawing.Point(51, 3)
		Me.lbl_Weight_Text.MKBoundControl1 = Nothing
		Me.lbl_Weight_Text.MKBoundControl2 = Nothing
		Me.lbl_Weight_Text.MKBoundControl3 = Nothing
		Me.lbl_Weight_Text.MKBoundControl4 = Nothing
		Me.lbl_Weight_Text.MKBoundControl5 = Nothing
		Me.lbl_Weight_Text.Name = "lbl_Weight_Text"
		Me.lbl_Weight_Text.Size = New System.Drawing.Size(328, 20)
		Me.lbl_Weight_Text.TabIndex = 7
		'
		'ucr_Similarity_Calculation_Details_Genre
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.Controls.Add(Me.pnl_Details)
		Me.Controls.Add(Me.lbl_Explanation)
		Me.Name = "ucr_Similarity_Calculation_Details_Genre"
		Me.Size = New System.Drawing.Size(383, 474)
		CType(Me.pnl_Details, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_Details.ResumeLayout(False)
		Me.tlp_Main.ResumeLayout(False)
		CType(Me.gb_AB, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_AB.ResumeLayout(False)
		CType(Me.grd_AB, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_AB, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gb_B, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_B.ResumeLayout(False)
		CType(Me.grd_B, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_B, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gb_A, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_A.ResumeLayout(False)
		CType(Me.grd_A, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_A, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents lbl_Explanation As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents pnl_Details As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents tlp_Main As TableLayoutPanel
	Friend WithEvents gb_AB As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents grd_AB As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_AB As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_AB_Name As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents gb_B As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents grd_B As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_B As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_B_Name As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents gb_A As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents grd_A As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_A As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_A_Name As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents lbl_Weight As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Weight_Text As MKNetDXLib.ctl_MKDXLabel
End Class
