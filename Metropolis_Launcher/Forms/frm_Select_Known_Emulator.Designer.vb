<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_Select_Known_Emulator
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
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Close = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.grd_Emulators = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_Known_Emulators = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_Rombase = New Metropolis_Launcher.DS_Rombase()
		Me.gv_Emulators = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colName = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.DS_ML = New Metropolis_Launcher.DS_ML()
		Me.lbl_Static = New MKNetDXLib.ctl_MKDXLabel()
		Me.Ctl_MKDXPanel1 = New MKNetDXLib.ctl_MKDXPanel()
		Me.lbl_Description = New MKNetDXLib.ctl_MKDXLabel()
		CType(Me.grd_Emulators, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Known_Emulators, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_Rombase, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_Emulators, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Ctl_MKDXPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Ctl_MKDXPanel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.Location = New System.Drawing.Point(201, 4)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 20)
		Me.btn_OK.TabIndex = 2
		Me.btn_OK.Text = "&OK"
		'
		'btn_Close
		'
		Me.btn_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btn_Close.Location = New System.Drawing.Point(279, 4)
		Me.btn_Close.Name = "btn_Close"
		Me.btn_Close.Size = New System.Drawing.Size(75, 20)
		Me.btn_Close.TabIndex = 3
		Me.btn_Close.Text = "&Cancel"
		'
		'grd_Emulators
		'
		Me.grd_Emulators.DataSource = Me.BS_Known_Emulators
		Me.grd_Emulators.Dock = System.Windows.Forms.DockStyle.Top
		Me.grd_Emulators.Location = New System.Drawing.Point(0, 45)
		Me.grd_Emulators.MainView = Me.gv_Emulators
		Me.grd_Emulators.Name = "grd_Emulators"
		Me.grd_Emulators.Size = New System.Drawing.Size(358, 77)
		Me.grd_Emulators.TabIndex = 4
		Me.grd_Emulators.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Emulators})
		'
		'BS_Known_Emulators
		'
		Me.BS_Known_Emulators.DataMember = "tbl_Rombase_Known_Emulators"
		Me.BS_Known_Emulators.DataSource = Me.DS_Rombase
		'
		'DS_Rombase
		'
		Me.DS_Rombase.DataSetName = "DS_Rombase"
		Me.DS_Rombase.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'gv_Emulators
		'
		Me.gv_Emulators.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colName})
		Me.gv_Emulators.GridControl = Me.grd_Emulators
		Me.gv_Emulators.Name = "gv_Emulators"
		Me.gv_Emulators.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_Emulators.OptionsSelection.InvertSelection = True
		Me.gv_Emulators.OptionsView.ShowColumnHeaders = False
		Me.gv_Emulators.OptionsView.ShowGroupPanel = False
		Me.gv_Emulators.OptionsView.ShowIndicator = False
		'
		'colName
		'
		Me.colName.Caption = "Emulator"
		Me.colName.FieldName = "Name"
		Me.colName.Name = "colName"
		Me.colName.OptionsColumn.AllowEdit = False
		Me.colName.OptionsColumn.ReadOnly = True
		Me.colName.Visible = True
		Me.colName.VisibleIndex = 0
		'
		'DS_ML
		'
		Me.DS_ML.DataSetName = "DS_ML"
		Me.DS_ML.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'lbl_Static
		'
		Me.lbl_Static.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_Static.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_Static.Location = New System.Drawing.Point(0, 0)
		Me.lbl_Static.MKBoundControl1 = Nothing
		Me.lbl_Static.MKBoundControl2 = Nothing
		Me.lbl_Static.MKBoundControl3 = Nothing
		Me.lbl_Static.MKBoundControl4 = Nothing
		Me.lbl_Static.MKBoundControl5 = Nothing
		Me.lbl_Static.Name = "lbl_Static"
		Me.lbl_Static.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_Static.Size = New System.Drawing.Size(358, 45)
		Me.lbl_Static.TabIndex = 5
		Me.lbl_Static.Text = "Metropolis Launcher was able to detect the emulator for auto config. However, the" &
		"re are multiple options for it. Please choose the appropriate option from the li" &
		"st below."
		'
		'Ctl_MKDXPanel1
		'
		Me.Ctl_MKDXPanel1.Controls.Add(Me.btn_Close)
		Me.Ctl_MKDXPanel1.Controls.Add(Me.btn_OK)
		Me.Ctl_MKDXPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.Ctl_MKDXPanel1.Location = New System.Drawing.Point(0, 313)
		Me.Ctl_MKDXPanel1.Name = "Ctl_MKDXPanel1"
		Me.Ctl_MKDXPanel1.Size = New System.Drawing.Size(358, 28)
		Me.Ctl_MKDXPanel1.TabIndex = 6
		'
		'lbl_Description
		'
		Me.lbl_Description.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_Description.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BS_Known_Emulators, "Description", True))
		Me.lbl_Description.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lbl_Description.Location = New System.Drawing.Point(0, 122)
		Me.lbl_Description.MKBoundControl1 = Nothing
		Me.lbl_Description.MKBoundControl2 = Nothing
		Me.lbl_Description.MKBoundControl3 = Nothing
		Me.lbl_Description.MKBoundControl4 = Nothing
		Me.lbl_Description.MKBoundControl5 = Nothing
		Me.lbl_Description.Name = "lbl_Description"
		Me.lbl_Description.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_Description.Size = New System.Drawing.Size(358, 6)
		Me.lbl_Description.TabIndex = 7
		'
		'frm_Select_Known_Emulator
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(358, 341)
		Me.Controls.Add(Me.Ctl_MKDXPanel1)
		Me.Controls.Add(Me.lbl_Description)
		Me.Controls.Add(Me.grd_Emulators)
		Me.Controls.Add(Me.lbl_Static)
		Me.MinimumSize = New System.Drawing.Size(374, 380)
		Me.Name = "frm_Select_Known_Emulator"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Select Auto Config Option"
		CType(Me.grd_Emulators, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Known_Emulators, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_Rombase, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Emulators, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Ctl_MKDXPanel1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Ctl_MKDXPanel1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_Close As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents grd_Emulators As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_Emulators As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents BS_Known_Emulators As BindingSource
	Friend WithEvents DS_ML As DS_ML
	Friend WithEvents DS_Rombase As DS_Rombase
	Friend WithEvents colName As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents lbl_Static As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents Ctl_MKDXPanel1 As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents lbl_Description As MKNetDXLib.ctl_MKDXLabel
End Class
