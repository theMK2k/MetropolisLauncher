<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Users_Setup
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Users_Setup))
		Me.DS_ML = New Metropolis_Launcher.DS_ML()
		Me.BS_Users = New System.Windows.Forms.BindingSource(Me.components)
		Me.grd_Users = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_Users = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colUsername = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colPassword = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colAdmin = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colRestricted = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.btn_Cancel = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.btn_OK = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.barmng_Users = New MKNetDXLib.ctl_MKDXBarManager()
		Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
		Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
		Me.bbi_Add_User = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Delete_User = New DevExpress.XtraBars.BarButtonItem()
		Me.bbi_Edit_User = New DevExpress.XtraBars.BarButtonItem()
		Me.popmnu_Users = New MKNetDXLib.cmp_MKDXPopupMenu()
		Me.colid_Cheevo_Challenges = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.BTA_Challenges = New MKNetLib.cmp_MKBindableTableAdapter(Me.components)
		Me.rpiChallenges = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BS_Users, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.grd_Users, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_Users, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.barmng_Users, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.popmnu_Users, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BTA_Challenges, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.rpiChallenges, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'DS_ML
		'
		Me.DS_ML.DataSetName = "DS_ML"
		Me.DS_ML.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'BS_Users
		'
		Me.BS_Users.DataMember = "tbl_Users"
		Me.BS_Users.DataSource = Me.DS_ML
		'
		'grd_Users
		'
		Me.grd_Users.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.grd_Users.DataSource = Me.BS_Users
		Me.grd_Users.Location = New System.Drawing.Point(3, 3)
		Me.grd_Users.MainView = Me.gv_Users
		Me.grd_Users.Name = "grd_Users"
		Me.grd_Users.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rpiChallenges})
		Me.grd_Users.Size = New System.Drawing.Size(596, 342)
		Me.grd_Users.TabIndex = 0
		Me.grd_Users.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Users})
		'
		'gv_Users
		'
		Me.gv_Users.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colUsername, Me.colPassword, Me.colAdmin, Me.colRestricted, Me.colid_Cheevo_Challenges})
		Me.gv_Users.GridControl = Me.grd_Users
		Me.gv_Users.Name = "gv_Users"
		Me.gv_Users.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_Users.OptionsView.ColumnAutoWidth = False
		Me.gv_Users.OptionsView.ShowGroupPanel = False
		'
		'colUsername
		'
		Me.colUsername.FieldName = "Username"
		Me.colUsername.Name = "colUsername"
		Me.colUsername.OptionsColumn.AllowEdit = False
		Me.colUsername.OptionsColumn.ReadOnly = True
		Me.colUsername.Visible = True
		Me.colUsername.VisibleIndex = 0
		Me.colUsername.Width = 150
		'
		'colPassword
		'
		Me.colPassword.FieldName = "Password"
		Me.colPassword.Name = "colPassword"
		Me.colPassword.OptionsColumn.AllowEdit = False
		Me.colPassword.OptionsColumn.ReadOnly = True
		Me.colPassword.Visible = True
		Me.colPassword.VisibleIndex = 1
		Me.colPassword.Width = 79
		'
		'colAdmin
		'
		Me.colAdmin.FieldName = "Admin"
		Me.colAdmin.Name = "colAdmin"
		Me.colAdmin.OptionsColumn.AllowEdit = False
		Me.colAdmin.OptionsColumn.ReadOnly = True
		Me.colAdmin.ToolTip = "The user is the administrator"
		Me.colAdmin.Visible = True
		Me.colAdmin.VisibleIndex = 2
		'
		'colRestricted
		'
		Me.colRestricted.FieldName = "Restricted"
		Me.colRestricted.Name = "colRestricted"
		Me.colRestricted.OptionsColumn.AllowEdit = False
		Me.colRestricted.OptionsColumn.ReadOnly = True
		Me.colRestricted.ToolTip = "User is restricted to certain games"
		Me.colRestricted.Visible = True
		Me.colRestricted.VisibleIndex = 3
		'
		'btn_Cancel
		'
		Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btn_Cancel.Location = New System.Drawing.Point(524, 348)
		Me.btn_Cancel.Name = "btn_Cancel"
		Me.btn_Cancel.Size = New System.Drawing.Size(75, 23)
		Me.btn_Cancel.TabIndex = 2
		Me.btn_Cancel.Text = "&Cancel"
		'
		'btn_OK
		'
		Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_OK.Location = New System.Drawing.Point(446, 348)
		Me.btn_OK.Name = "btn_OK"
		Me.btn_OK.Size = New System.Drawing.Size(75, 23)
		Me.btn_OK.TabIndex = 1
		Me.btn_OK.Text = "&OK"
		'
		'barmng_Users
		'
		Me.barmng_Users.DockControls.Add(Me.barDockControlTop)
		Me.barmng_Users.DockControls.Add(Me.barDockControlBottom)
		Me.barmng_Users.DockControls.Add(Me.barDockControlLeft)
		Me.barmng_Users.DockControls.Add(Me.barDockControlRight)
		Me.barmng_Users.Form = Me
		Me.barmng_Users.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.bbi_Add_User, Me.bbi_Delete_User, Me.bbi_Edit_User})
		Me.barmng_Users.MaxItemId = 3
		'
		'barDockControlTop
		'
		Me.barDockControlTop.CausesValidation = False
		Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
		Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
		Me.barDockControlTop.Size = New System.Drawing.Size(611, 0)
		'
		'barDockControlBottom
		'
		Me.barDockControlBottom.CausesValidation = False
		Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.barDockControlBottom.Location = New System.Drawing.Point(0, 383)
		Me.barDockControlBottom.Size = New System.Drawing.Size(611, 0)
		'
		'barDockControlLeft
		'
		Me.barDockControlLeft.CausesValidation = False
		Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
		Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
		Me.barDockControlLeft.Size = New System.Drawing.Size(0, 383)
		'
		'barDockControlRight
		'
		Me.barDockControlRight.CausesValidation = False
		Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
		Me.barDockControlRight.Location = New System.Drawing.Point(611, 0)
		Me.barDockControlRight.Size = New System.Drawing.Size(0, 383)
		'
		'bbi_Add_User
		'
		Me.bbi_Add_User.Caption = "&Add user"
		Me.bbi_Add_User.Id = 0
		Me.bbi_Add_User.ImageUri.Uri = "Add"
		Me.bbi_Add_User.Name = "bbi_Add_User"
		'
		'bbi_Delete_User
		'
		Me.bbi_Delete_User.Caption = "&Delete user"
		Me.bbi_Delete_User.Id = 1
		Me.bbi_Delete_User.ImageUri.Uri = "Delete"
		Me.bbi_Delete_User.Name = "bbi_Delete_User"
		'
		'bbi_Edit_User
		'
		Me.bbi_Edit_User.Caption = "&Edit user"
		Me.bbi_Edit_User.Id = 2
		Me.bbi_Edit_User.ImageUri.Uri = "Edit"
		Me.bbi_Edit_User.Name = "bbi_Edit_User"
		'
		'popmnu_Users
		'
		Me.popmnu_Users.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Add_User), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Edit_User), New DevExpress.XtraBars.LinkPersistInfo(Me.bbi_Delete_User)})
		Me.popmnu_Users.Manager = Me.barmng_Users
		Me.popmnu_Users.Name = "popmnu_Users"
		'
		'colid_Cheevo_Challenges
		'
		Me.colid_Cheevo_Challenges.Caption = "Challenge"
		Me.colid_Cheevo_Challenges.ColumnEdit = Me.rpiChallenges
		Me.colid_Cheevo_Challenges.FieldName = "id_Cheevo_Challenges"
		Me.colid_Cheevo_Challenges.Name = "colid_Cheevo_Challenges"
		Me.colid_Cheevo_Challenges.OptionsColumn.AllowEdit = False
		Me.colid_Cheevo_Challenges.OptionsColumn.ReadOnly = True
		Me.colid_Cheevo_Challenges.ToolTip = "User is bound to a challenge"
		Me.colid_Cheevo_Challenges.Visible = True
		Me.colid_Cheevo_Challenges.VisibleIndex = 4
		Me.colid_Cheevo_Challenges.Width = 197
		'
		'BTA_Challenges
		'
		Me.BTA_Challenges.AllowDelete = True
		Me.BTA_Challenges.ColumnUpdateBlacklistStream = CType(resources.GetObject("BTA_Challenges.ColumnUpdateBlacklistStream"), System.Collections.Generic.List(Of String))
		Me.BTA_Challenges.Connection = Nothing
		Me.BTA_Challenges.DSStream = CType(resources.GetObject("BTA_Challenges.DSStream"), System.IO.MemoryStream)
		Me.BTA_Challenges.FillString = ""
		Me.BTA_Challenges.Transaction = Nothing
		Me.BTA_Challenges.UpdateTablesStream = CType(resources.GetObject("BTA_Challenges.UpdateTablesStream"), System.Collections.Generic.List(Of String))
		'
		'rpiChallenges
		'
		Me.rpiChallenges.AutoHeight = False
		Me.rpiChallenges.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
		Me.rpiChallenges.DataSource = Me.BTA_Challenges
		Me.rpiChallenges.DisplayMember = "Name"
		Me.rpiChallenges.Name = "rpiChallenges"
		Me.rpiChallenges.ValueMember = "id_Cheevo_Challenges"
		'
		'frm_Users_Setup
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(611, 383)
		Me.Controls.Add(Me.btn_Cancel)
		Me.Controls.Add(Me.btn_OK)
		Me.Controls.Add(Me.grd_Users)
		Me.Controls.Add(Me.barDockControlLeft)
		Me.Controls.Add(Me.barDockControlRight)
		Me.Controls.Add(Me.barDockControlBottom)
		Me.Controls.Add(Me.barDockControlTop)
		Me.Name = "frm_Users_Setup"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Setup Users (Multi User Mode)"
		CType(Me.DS_ML, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BS_Users, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.grd_Users, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Users, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.barmng_Users, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.popmnu_Users, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BTA_Challenges, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.rpiChallenges, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents DS_ML As Metropolis_Launcher.DS_ML
	Friend WithEvents BS_Users As System.Windows.Forms.BindingSource
	Friend WithEvents grd_Users As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_Users As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents colUsername As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colPassword As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colAdmin As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colRestricted As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents btn_Cancel As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents btn_OK As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents barmng_Users As MKNetDXLib.ctl_MKDXBarManager
	Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
	Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
	Friend WithEvents bbi_Add_User As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Delete_User As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents bbi_Edit_User As DevExpress.XtraBars.BarButtonItem
	Friend WithEvents popmnu_Users As MKNetDXLib.cmp_MKDXPopupMenu
	Friend WithEvents colid_Cheevo_Challenges As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents BTA_Challenges As MKNetLib.cmp_MKBindableTableAdapter
	Friend WithEvents rpiChallenges As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
End Class
