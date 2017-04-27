<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Moby_Platforms_Configuration
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
		Me.grd_Platforms = New MKNetDXLib.ctl_MKDXGrid()
		Me.BS_Platform_Settings = New System.Windows.Forms.BindingSource(Me.components)
		Me.DS_ML = New Metropolis_Launcher.DS_ML()
		Me.gv_Platforms = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colName = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colShortname = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colVisible = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.rpi_Checkedit = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
		Me.rpi_DefaultEmulator = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
		Me.btn_Cancel = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		CType(Me.grd_Platforms, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Platform_Settings, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_Platforms, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_Checkedit, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpi_DefaultEmulator, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'grd_Platforms
		'
		Me.grd_Platforms.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.grd_Platforms.DataSource = Me.BS_Platform_Settings
		Me.grd_Platforms.Location = New System.Drawing.Point(3, 3)
		Me.grd_Platforms.MainView = Me.gv_Platforms
		Me.grd_Platforms.Name = "grd_Platforms"
		Me.grd_Platforms.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rpi_Checkedit, Me.rpi_DefaultEmulator})
		Me.grd_Platforms.Size = New System.Drawing.Size(519, 364)
		Me.grd_Platforms.TabIndex = 0
		Me.grd_Platforms.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Platforms})
		'
		'BS_Platform_Settings
		'
		Me.BS_Platform_Settings.DataMember = "tbl_Moby_Platforms_Settings"
		Me.BS_Platform_Settings.DataSource = Me.DS_ML
		'
		'DS_ML
		'
		Me.DS_ML.DataSetName = "DS_ML"
		Me.DS_ML.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'gv_Platforms
		'
		Me.gv_Platforms.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colName, Me.colShortname, Me.colVisible})
		Me.gv_Platforms.GridControl = Me.grd_Platforms
		Me.gv_Platforms.Name = "gv_Platforms"
		Me.gv_Platforms.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_Platforms.OptionsView.ShowGroupPanel = False
		Me.gv_Platforms.OptionsView.ShowIndicator = False
		'
		'colName
		'
		Me.colName.Caption = "Platform"
		Me.colName.FieldName = "Name"
		Me.colName.Name = "colName"
		Me.colName.OptionsColumn.AllowEdit = False
		Me.colName.OptionsColumn.ReadOnly = True
		Me.colName.Visible = True
		Me.colName.VisibleIndex = 1
		Me.colName.Width = 729
		'
		'colShortname
		'
		Me.colShortname.Caption = "Short Name"
		Me.colShortname.FieldName = "Shortname"
		Me.colShortname.Name = "colShortname"
		Me.colShortname.OptionsColumn.AllowEdit = False
		Me.colShortname.OptionsColumn.ReadOnly = True
		Me.colShortname.Visible = True
		Me.colShortname.VisibleIndex = 2
		Me.colShortname.Width = 250
		'
		'colVisible
		'
		Me.colVisible.Caption = "Visible"
		Me.colVisible.FieldName = "Visible"
		Me.colVisible.Name = "colVisible"
		Me.colVisible.Visible = True
		Me.colVisible.VisibleIndex = 0
		Me.colVisible.Width = 211
		'
		'rpi_Checkedit
		'
		Me.rpi_Checkedit.AutoHeight = False
		Me.rpi_Checkedit.Name = "rpi_Checkedit"
		Me.rpi_Checkedit.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
		'
		'rpi_DefaultEmulator
		'
		Me.rpi_DefaultEmulator.AutoHeight = False
		Me.rpi_DefaultEmulator.Name = "rpi_DefaultEmulator"
		Me.rpi_DefaultEmulator.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
		'
		'btn_Cancel
		'
		Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Cancel.Location = New System.Drawing.Point(447, 370)
		Me.btn_Cancel.Name = "btn_Cancel"
		Me.btn_Cancel.Size = New System.Drawing.Size(75, 23)
		Me.btn_Cancel.TabIndex = 2
		Me.btn_Cancel.Text = "&Cancel"
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.Location = New System.Drawing.Point(369, 370)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 23)
		Me.btn_OK.TabIndex = 1
		Me.btn_OK.Text = "&OK"
		'
		'frm_Moby_Platforms_Configuration
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(525, 396)
		Me.Controls.Add(Me.btn_Cancel)
		Me.Controls.Add(Me.btn_OK)
		Me.Controls.Add(Me.grd_Platforms)
		Me.Name = "frm_Moby_Platforms_Configuration"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Platform Settings"
		CType(Me.grd_Platforms, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Platform_Settings, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Platforms, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_Checkedit, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpi_DefaultEmulator, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents grd_Platforms As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_Platforms As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents rpi_Checkedit As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
	Friend WithEvents rpi_DefaultEmulator As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
	Friend WithEvents btn_Cancel As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents DS_ML As Metropolis_Launcher.DS_ML
	Friend WithEvents BS_Platform_Settings As System.Windows.Forms.BindingSource
	Friend WithEvents colName As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colShortname As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colVisible As DevExpress.XtraGrid.Columns.GridColumn
End Class
