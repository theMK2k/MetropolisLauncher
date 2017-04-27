<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Emu_Game_Rating_Weights_Edit
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
		Me.btn_Cancel = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.grd_Rating_Weights = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_Rating_Weights = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_ML = New Metropolis_Launcher.DS_ML()
		Me.gv_Rating_Weights = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colRating_Category = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colWeight = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.RepositoryItemSpinEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
		CType(Me.grd_Rating_Weights, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Rating_Weights, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_Rating_Weights, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.Location = New System.Drawing.Point(206, 149)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 23)
		Me.btn_OK.TabIndex = 1
		Me.btn_OK.Text = "&OK"
		'
		'btn_Cancel
		'
		Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Cancel.Location = New System.Drawing.Point(284, 149)
		Me.btn_Cancel.Name = "btn_Cancel"
		Me.btn_Cancel.Size = New System.Drawing.Size(75, 23)
		Me.btn_Cancel.TabIndex = 2
		Me.btn_Cancel.Text = "&Cancel"
		'
		'grd_Rating_Weights
		'
		Me.grd_Rating_Weights.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.grd_Rating_Weights.DataSource = Me.BS_Rating_Weights
		Me.grd_Rating_Weights.Location = New System.Drawing.Point(3, 3)
		Me.grd_Rating_Weights.MainView = Me.gv_Rating_Weights
		Me.grd_Rating_Weights.Name = "grd_Rating_Weights"
		Me.grd_Rating_Weights.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemSpinEdit1})
		Me.grd_Rating_Weights.Size = New System.Drawing.Size(356, 143)
		Me.grd_Rating_Weights.TabIndex = 0
		Me.grd_Rating_Weights.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Rating_Weights})
		'
		'BS_Rating_Weights
		'
		Me.BS_Rating_Weights.DataMember = "tbl_Emu_Games_Rating_Weights"
		Me.BS_Rating_Weights.DataSource = Me.DS_ML
		'
		'DS_ML
		'
		Me.DS_ML.DataSetName = "DS_ML"
		Me.DS_ML.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'gv_Rating_Weights
		'
		Me.gv_Rating_Weights.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colRating_Category, Me.colWeight})
		Me.gv_Rating_Weights.GridControl = Me.grd_Rating_Weights
		Me.gv_Rating_Weights.Name = "gv_Rating_Weights"
		Me.gv_Rating_Weights.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_Rating_Weights.OptionsView.ShowGroupPanel = False
		'
		'colRating_Category
		'
		Me.colRating_Category.Caption = "Rating Category"
		Me.colRating_Category.FieldName = "Rating_Category"
		Me.colRating_Category.Name = "colRating_Category"
		Me.colRating_Category.OptionsColumn.AllowEdit = False
		Me.colRating_Category.OptionsColumn.ReadOnly = True
		Me.colRating_Category.Visible = True
		Me.colRating_Category.VisibleIndex = 0
		Me.colRating_Category.Width = 258
		'
		'colWeight
		'
		Me.colWeight.ColumnEdit = Me.RepositoryItemSpinEdit1
		Me.colWeight.FieldName = "Weight"
		Me.colWeight.Name = "colWeight"
		Me.colWeight.Visible = True
		Me.colWeight.VisibleIndex = 1
		Me.colWeight.Width = 80
		'
		'RepositoryItemSpinEdit1
		'
		Me.RepositoryItemSpinEdit1.AutoHeight = False
		Me.RepositoryItemSpinEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
		Me.RepositoryItemSpinEdit1.IsFloatValue = False
		Me.RepositoryItemSpinEdit1.Mask.EditMask = "N00"
		Me.RepositoryItemSpinEdit1.MaxValue = New Decimal(New Integer() {99, 0, 0, 0})
		Me.RepositoryItemSpinEdit1.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
		Me.RepositoryItemSpinEdit1.Name = "RepositoryItemSpinEdit1"
		'
		'frm_Emu_Game_Rating_Weights_Edit
		'
		Me.ClientSize = New System.Drawing.Size(362, 175)
		Me.Controls.Add(Me.grd_Rating_Weights)
		Me.Controls.Add(Me.btn_Cancel)
		Me.Controls.Add(Me.btn_OK)
		Me.Name = "frm_Emu_Game_Rating_Weights_Edit"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Edit Rating Weights"
		CType(Me.grd_Rating_Weights, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Rating_Weights, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Rating_Weights, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_Cancel As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents grd_Rating_Weights As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_Rating_Weights As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents BS_Rating_Weights As System.Windows.Forms.BindingSource
	Friend WithEvents DS_ML As Metropolis_Launcher.DS_ML
	Friend WithEvents colRating_Category As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colWeight As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents RepositoryItemSpinEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit

End Class
