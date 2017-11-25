<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Select_Emulator
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
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_Close = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.grd_Emulators = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_Emulators = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colDisplayname = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.DS_ML = New Metropolis_Launcher.DS_ML()
		Me.BS_Emulators = New System.Windows.Forms.BindingSource(Me.components)
		Me.colInstallDirectory = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colExecutable = New DevExpress.XtraGrid.Columns.GridColumn()
		CType(Me.grd_Emulators, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_Emulators, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Emulators, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.Location = New System.Drawing.Point(203, 380)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 20)
		Me.btn_OK.TabIndex = 2
		Me.btn_OK.Text = "&OK"
		'
		'btn_Close
		'
		Me.btn_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btn_Close.Location = New System.Drawing.Point(281, 380)
		Me.btn_Close.Name = "btn_Close"
		Me.btn_Close.Size = New System.Drawing.Size(75, 20)
		Me.btn_Close.TabIndex = 3
		Me.btn_Close.Text = "&Cancel"
		'
		'grd_Emulators
		'
		Me.grd_Emulators.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.grd_Emulators.DataSource = Me.BS_Emulators
		Me.grd_Emulators.Location = New System.Drawing.Point(3, 3)
		Me.grd_Emulators.MainView = Me.gv_Emulators
		Me.grd_Emulators.Name = "grd_Emulators"
		Me.grd_Emulators.Size = New System.Drawing.Size(352, 374)
		Me.grd_Emulators.TabIndex = 4
		Me.grd_Emulators.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Emulators})
		'
		'gv_Emulators
		'
		Me.gv_Emulators.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colDisplayname, Me.colInstallDirectory, Me.colExecutable})
		Me.gv_Emulators.GridControl = Me.grd_Emulators
		Me.gv_Emulators.Name = "gv_Emulators"
		Me.gv_Emulators.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_Emulators.OptionsSelection.InvertSelection = True
		Me.gv_Emulators.OptionsView.ShowGroupPanel = False
		Me.gv_Emulators.OptionsView.ShowIndicator = False
		'
		'colDisplayname
		'
		Me.colDisplayname.FieldName = "Displayname"
		Me.colDisplayname.Name = "colDisplayname"
		Me.colDisplayname.OptionsColumn.AllowEdit = False
		Me.colDisplayname.OptionsColumn.ReadOnly = True
		Me.colDisplayname.Visible = True
		Me.colDisplayname.VisibleIndex = 0
		'
		'DS_ML
		'
		Me.DS_ML.DataSetName = "DS_ML"
		Me.DS_ML.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'BS_Emulators
		'
		Me.BS_Emulators.DataMember = "tbl_Emulators"
		Me.BS_Emulators.DataSource = Me.DS_ML
		'
		'colInstallDirectory
		'
		Me.colInstallDirectory.FieldName = "InstallDirectory"
		Me.colInstallDirectory.Name = "colInstallDirectory"
		Me.colInstallDirectory.OptionsColumn.AllowEdit = False
		Me.colInstallDirectory.OptionsColumn.ReadOnly = True
		Me.colInstallDirectory.Visible = True
		Me.colInstallDirectory.VisibleIndex = 1
		'
		'colExecutable
		'
		Me.colExecutable.FieldName = "Executable"
		Me.colExecutable.Name = "colExecutable"
		Me.colExecutable.OptionsColumn.AllowEdit = False
		Me.colExecutable.OptionsColumn.ReadOnly = True
		Me.colExecutable.Visible = True
		Me.colExecutable.VisibleIndex = 2
		'
		'frm_Select_Emulator
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(358, 403)
		Me.Controls.Add(Me.grd_Emulators)
		Me.Controls.Add(Me.btn_OK)
		Me.Controls.Add(Me.btn_Close)
		Me.Name = "frm_Select_Emulator"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Select Emulator"
		CType(Me.grd_Emulators, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Emulators, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Emulators, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_Close As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents grd_Emulators As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_Emulators As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents colDisplayname As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents BS_Emulators As BindingSource
	Friend WithEvents DS_ML As DS_ML
	Friend WithEvents colInstallDirectory As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colExecutable As DevExpress.XtraGrid.Columns.GridColumn
End Class
