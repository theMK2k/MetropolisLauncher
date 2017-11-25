Imports DevExpress.XtraBars.Docking2010.Views
Imports DevExpress.XtraBars.Docking2010.Views.WindowsUI

Public Class frm_Main
	Private WithEvents _ucr_Settings As ucr_Settings
	Private WithEvents _ucr_Apps As ucr_Apps
	Private WithEvents _ucr_Emulation As ucr_Emulation

	Private WithEvents _ucr_Todo As ucr_Todo

	Private _ucr_Settings_Reload As Boolean = False
	Private _ucr_Apps_Reload As Boolean = False
	Private _ucr_Emulation_Reload As Boolean = False
	Private _ucr_Todo_Reload As Boolean = False

	'Private WithEvents btn_About As New DevExpress.XtraBars.Docking2010.WindowsUIButton

	Private _Immediate_Close As Boolean = False

	Private WithEvents btn_About As New DevExpress.XtraBars.Docking2010.WindowsUIButton
	Private WithEvents btn_Help As New DevExpress.XtraBars.Docking2010.WindowsUIButton
	Private WithEvents btn_Logout As New DevExpress.XtraBars.Docking2010.WindowsUIButton

	Public Sub New()
		If Alphaleonis.Win32.Filesystem.File.Exists(System.Windows.Forms.Application.StartupPath & "\log.me") Then cls_Globals.Logging = True
		If Alphaleonis.Win32.Filesystem.File.Exists(System.Windows.Forms.Application.StartupPath & "\debug.me") Then MsgBox("Attach Debugger now...")

		cls_Globals.AddLog("", False)
		cls_Globals.AddLog("Metropolis Launcher started")

		InitializeComponent()

		cls_Globals.AddLog("frm_Main.InitializeComponent run")

		' Initialize global help
		Dim sResfile As String = My.Resources.helpmap
		Dim xmlContent As New Xml.XmlDocument
		xmlContent.LoadXml(sResfile)

#If DEBUG Then
		MKNetLib.cls_MKHelp.InitHelp(xmlContent, System.Windows.Forms.Application.StartupPath & "\helpmap.xml", True)
#Else
		MKNetLib.cls_MKHelp.InitHelp(xmlContent, System.Windows.Forms.Application.StartupPath & "\helpmap.xml", False)
#End If

		DevExpress.Skins.SkinManager.EnableFormSkins()
		DevExpress.UserSkins.BonusSkins.Register()

		MKNetDXLib.frm_MKDXBaseForm.Default_Form_Icon = Me.Icon
		DevExpress.Utils.ToolTipController.DefaultController.AutoPopDelay = 20000

		Dim aboutAction As DelegateAction = New DelegateAction(AddressOf CanShowAbout, AddressOf ShowAbout)
		aboutAction.Caption = "About"
		aboutAction.Type = ActionType.Navigation
		aboutAction.Edge = ActionEdge.Right
		aboutAction.Behavior = ActionBehavior.HideBarOnClick
		'MetroUIView.ContentContainerActions.Add(aboutAction)

		Dim helpAction As DelegateAction = New DelegateAction(AddressOf CanShowHelp, AddressOf ShowHelp)
		helpAction.Caption = "Help"
		helpAction.Type = ActionType.Navigation
		helpAction.Edge = ActionEdge.Right
		helpAction.Behavior = ActionBehavior.HideBarOnClick
		'MetroUIView.ContentContainerActions.Add(helpAction)

		Dim logoutAction As DelegateAction = New DelegateAction(AddressOf CanShowLogOut, AddressOf LogOut)
		logoutAction.Caption = "Log out"
		logoutAction.Type = ActionType.Navigation
		logoutAction.Edge = ActionEdge.Right
		logoutAction.Behavior = ActionBehavior.HideBarOnClick
		'MetroUIView.ContentContainerActions.Add(logoutAction)

		btn_About = New DevExpress.XtraBars.Docking2010.WindowsUIButton
		btn_About.ImageUri = "Show"
		btn_About.Caption = "About"

		btn_Help = New DevExpress.XtraBars.Docking2010.WindowsUIButton
		btn_Help.ImageUri = "Zoom"
		btn_Help.Caption = "Help"

		btn_Logout = New DevExpress.XtraBars.Docking2010.WindowsUIButton
		btn_Logout.ImageUri = "Reset"
		btn_Logout.Caption = "Log out"

		tilecontainer_Main.Buttons.AddRange({btn_Logout, btn_Help, btn_About})
	End Sub

	Private Function CanShowAbout() As Boolean
		Return True
	End Function

	Private Function CanShowHelp() As Boolean
		Return True
	End Function

	Private Function CanShowLogOut() As Boolean
		Return cls_Globals.MultiUserMode
	End Function

	Private Sub ShowAbout()
		Using frm As New frm_About
			frm.ShowDialog(Me)
		End Using
	End Sub

	Private Sub ShowHelp()
		MKNetLib.cls_MKHelp.ShowHelp()
	End Sub

	Private Sub LogOut()
		Me.MetroUIView.Controller.Activate(tilecontainer_Main)

		'If Me._ucr_Apps IsNot Nothing Then
		'	Me._ucr_Apps.Dispose()
		'	Me._ucr_Apps = Nothing
		'End If

		'If Me._ucr_Emulation IsNot Nothing Then
		'	Me._ucr_Emulation.Dispose()
		'	Me._ucr_Emulation = Nothing
		'End If

		'If Me._ucr_Settings IsNot Nothing Then
		'	Me._ucr_Settings.Dispose()
		'	Me._ucr_Settings = Nothing
		'End If

		'If Me._ucr_Todo IsNot Nothing Then
		'	Me._ucr_Todo.Dispose()
		'	Me._ucr_Todo = Nothing
		'End If

		_ucr_Apps_Reload = True
		_ucr_Emulation_Reload = True
		_ucr_Settings_Reload = True
		_ucr_Todo_Reload = True

		Me.frm_Main_Load(Me, New System.EventArgs)
		Me.frm_Main_Shown(Me, New System.EventArgs)

		If Me.IsDisposed Then Return

		MetroUIView.ReleaseDeferredLoadControls(False)
	End Sub

	Private Sub btn_About_Click(sender As Object, e As EventArgs) Handles btn_About.Click
		ShowAbout()
	End Sub

	Private Sub btn_Help_Click(sender As Object, e As EventArgs) Handles btn_Help.Click
		ShowHelp()
	End Sub

	Private Sub btn_Logout_Click(sender As Object, e As EventArgs) Handles btn_Logout.Click
		LogOut()
	End Sub



	Private Sub frm_Main_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		If _Immediate_Close Then
			Return
		End If

		Try
			If TC.NZ(cls_Settings.GetSetting("Stats_Enabled", cls_Settings.enm_Settingmodes.Per_User), True) Then
				Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
					DS_ML.Insert_tbl_History(tran, cls_Globals.StartupTime, DateTime.Now())
					tran.Commit()
				End Using

			End If
		Catch ex As Exception

		End Try

		Try
			Dim numLaunches As Integer = TC.NZ(cls_Settings.GetSetting("LaunchCounter_BackupRotation"), 0)
			Dim backupDir As String = cls_Globals.BackupsDir(Nothing)
			Dim backupFrequency As Integer = TC.NZ(cls_Settings.GetSetting("Backup_Frequency", cls_Settings.enm_Settingmodes.Same_for_All), 5)
			Dim backupRetention As Integer = TC.NZ(cls_Settings.GetSetting("Backup_Retention", cls_Settings.enm_Settingmodes.Same_for_All), 3)
			Dim currentSkin As String = cls_Skins.GetCurrentSkinname(Nothing)

			Dim doBackup As Boolean = False

			If Alphaleonis.Win32.Filesystem.Directory.Exists(backupDir) Then
				doBackup = True
			End If

			If doBackup AndAlso backupFrequency = 0 Then
				cls_Settings.SetSetting("LaunchCounter_BackupRotation", 0)
				doBackup = False
			End If

			If doBackup AndAlso numLaunches >= backupFrequency Then
				cls_Settings.SetSetting("LaunchCounter_BackupRotation", 0)
			Else
				doBackup = False
			End If

			If Not Alphaleonis.Win32.Filesystem.File.Exists(System.Windows.Forms.Application.StartupPath & "\ml.db") Then
				doBackup = False
			End If

			If doBackup Then
				cls_Globals.Conn.Close()

				Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(currentSkin, 400, 100, ProgressBarStyle.Marquee, False, "Please wait - backup in progress ...", 0, 0, False)

				prg.Start()

				Dim targetfilename = "ml_backup_" & DateTime.Now.ToString("ddMMyyyy_HHmmss") & ".zip"
				Dim targetfullpath = backupDir & "\" & targetfilename

				Try
					Dim al_Files As New ArrayList
					al_Files.Add(System.Windows.Forms.Application.StartupPath & "\ml.db")

					Dim ci As New SharpCompress.Common.CompressionInfo()
					ci.DeflateCompressionLevel = SharpCompress.Compressor.Deflate.CompressionLevel.BestSpeed
					ci.Type = SharpCompress.Common.CompressionType.Deflate

					Using zip As IO.Stream = IO.File.OpenWrite(targetfullpath)
						Using zipwriter As SharpCompress.Writer.IWriter = SharpCompress.Writer.WriterFactory.Open(zip, SharpCompress.Common.ArchiveType.Zip, ci)

							For Each yyy As String In al_Files
								Dim xxx As String = Alphaleonis.Win32.Filesystem.Path.GetFileName(yyy)
								Dim filenamewithoutextention As String = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(yyy)

								Using fs As New System.IO.FileStream(yyy, IO.FileMode.Open)
									zipwriter.Write(xxx, fs, Nothing)
								End Using
							Next
						End Using
					End Using

					prg.Close()
				Catch ex As Exception
					prg.Close()
					MKDXHelper.ExceptionMessageBox(ex, "Error while creating " & targetfullpath & "." & ControlChars.CrLf & ControlChars.CrLf, "Creating Backup")
				End Try

				Try
					prg = New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(currentSkin, 400, 100, ProgressBarStyle.Marquee, False, "Please wait - cleanup in progress ...", 0, 0, False)

					prg.Start()

					Dim files As IOrderedEnumerable(Of String) = Alphaleonis.Win32.Filesystem.Directory.GetFiles(backupDir, "*.zip").OrderBy(Function(f) f)
					If files.Count > backupRetention Then
						For i As Integer = 0 To files.Count - backupRetention - 1
							Alphaleonis.Win32.Filesystem.File.Delete(files(i))
						Next
					End If

					prg.Close()
				Catch ex As Exception
					prg.Close()

					MKDXHelper.ExceptionMessageBox(ex, "Error while cleaning up:" & ControlChars.CrLf & ControlChars.CrLf, "Cleanup Backupdirectory")
				End Try

				Try
					cls_Globals.Conn.Open()
				Catch ex As Exception

				End Try
			End If
		Catch ex As Exception

		End Try
	End Sub

	Private Function Open_DB() As Boolean
		Dim bInitialStartup As Boolean = False

		Dim mldb_file As String = System.Windows.Forms.Application.StartupPath & "\ml.db"
		Dim mldb_initial_file As String = System.Windows.Forms.Application.StartupPath & "\ml.db_initial"
		Dim mobydb_file As String = System.Windows.Forms.Application.StartupPath & "\moby.db"
		Dim rombasedb_file As String = System.Windows.Forms.Application.StartupPath & "\rombase.db"

		If Not Alphaleonis.Win32.Filesystem.File.Exists(mldb_file) Then
			bInitialStartup = True

			If Alphaleonis.Win32.Filesystem.File.Exists(mldb_initial_file) Then
				MKDXHelper.MessageBox("Hi! This is your first time starting Metropolis Launcher. The main database (ml.db) will be initialized now.", "Metropolis Launcher", MessageBoxButtons.OK, MessageBoxIcon.Information)

				Try
					Alphaleonis.Win32.Filesystem.File.Copy(mldb_initial_file, mldb_file)
				Catch ex As Exception
					MKDXHelper.ExceptionMessageBox(ex, "Something went wrong while copying ml.db_initial to ml.db, if you installed Metropolis Launcher under C:\Program Files please choose a different location." & ControlChars.CrLf & ControlChars.CrLf & "The error was: ", "Metropolis Launcher")
					Return False
				End Try
			Else
				MKDXHelper.MessageBox("Hi! This is your first time starting Metropolis Launcher, but the initial database cannot be found (ml.db_initial). Please get it together with all the other necessary database files from https://metropolis-launcher.net", "Metropolis Launcher", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Return False
			End If
		End If

		If Not Alphaleonis.Win32.Filesystem.File.Exists(mldb_file) Then
			MKDXHelper.MessageBox("The main database file cannot be found (ml.db), please download and extract Metropolis Launcher from https://metropolis-launcher.net", "Metropolis Launcher", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return False
		End If

		If Not Alphaleonis.Win32.Filesystem.File.Exists(mobydb_file) Then
			MKDXHelper.MessageBox("The Moby Games database file cannot be found (moby.db), please download and extract Metropolis Launcher from https://metropolis-launcher.net", "Metropolis Launcher", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return False
		End If

		If Not Alphaleonis.Win32.Filesystem.File.Exists(rombasedb_file) Then
			MKDXHelper.MessageBox("The Rombase database file cannot be found (rombase.db), please download and extract Metropolis Launcher from https://metropolis-launcher.net", "Metropolis Launcher", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return False
		End If

		Try
			Dim str As New SQLite.SQLiteConnectionStringBuilder()
			str.DataSource = mldb_file

			cls_Globals.Conn = New SQLite.SQLiteConnection(str.ConnectionString)

			Try
				Dim oRes As Object = MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT name FROM SQLITE_MASTER where type='table'")
				cls_Globals.Conn.Open()
			Catch ex As Exception
				Debug.WriteLine(ex.Message)
			End Try

			MKNetLib.cls_MKSQLiteDataAccess.FireProcedure(cls_Globals.Conn, 0, "ATTACH 'moby.db' AS 'moby'", Nothing)
			MKNetLib.cls_MKSQLiteDataAccess.FireProcedure(cls_Globals.Conn, 0, "ATTACH 'rombase.db' AS 'rombase'", Nothing)
		Catch ex As Exception
			MKDXHelper.ExceptionMessageBox(ex, "An error occured while opening the database connection, Metropolis Launcher will be closed now." & ControlChars.CrLf & ControlChars.CrLf & "The error was: ", "Metropolis Launcher")
			Return False
		End Try

		If bInitialStartup Then
			MKDXHelper.MessageBox("Initialization of the databases was successful.", "Metropolis Launcher", MessageBoxButtons.OK, MessageBoxIcon.Information)
		End If

		cls_Settings.SetSetting("LaunchCounter", TC.NZ(cls_Settings.GetSetting("LaunchCounter"), 0) + 1)
		cls_Settings.SetSetting("LaunchCounter_BackupRotation", TC.NZ(cls_Settings.GetSetting("LaunchCounter_BackupRotation"), 0) + 1)

		Return True
	End Function

	Private Sub frm_Main_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		'Show Error Messages from SQLite Module
		MKNetLib.cls_MKSQLiteDataAccess.ShowErrorMessages = True

		'Load Bonus Skins Assembly
		MKNetDXLib.cls_MKDXSkin.Load_Bonus_Skin()

		If Not Me.Open_DB() Then
			_Immediate_Close = True
			Me.Close()
			Return
		End If

		'Multi User Mode
		DataAccess.ShowErrorMessages = False
		cls_Globals.MultiUserMode = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT COUNT(1) FROM tbl_Users"), 0) <> 0
		DataAccess.ShowErrorMessages = True

		'Apply Settings to the current runtime instance
		cls_Settings.Apply_Settings()

		If cls_Globals.MultiUserMode Then
			Using frm As New frm_Login
				If frm.ShowDialog(Me) = DialogResult.Cancel Then
					Me.Close()
				Else
					Me.Text = "Metropolis Launcher - " & frm.BS_Users.Current("Username") & IIf(cls_Globals.Restricted, " (Restricted)", "")

					'Apply Settings for the logged in user
					cls_Settings.Apply_Settings()
				End If
			End Using
		End If

		btn_Logout.Visible = cls_Globals.MultiUserMode
	End Sub

	Private Sub MetroUIView_DocumentDeactivated(ByVal sender As Object, ByVal e As DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs) Handles MetroUIView.DocumentDeactivated
		If e.Document Is doc_Settings Then
			'Settings speichern
			Me._ucr_Settings.Save_Settings()
		End If

		btn_Logout.Visible = cls_Globals.MultiUserMode
	End Sub

	Private Sub MetroUIView_QueryControl(ByVal sender As Object, ByVal e As DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs) Handles MetroUIView.QueryControl
		If e.Document Is doc_Settings Then
			_ucr_Settings = New ucr_Settings()
			_ucr_Settings.Set_Controls_Enable()
			e.Control = _ucr_Settings
			_ucr_Settings.Focus()
		End If

		If e.Document Is doc_Apps Then
			_ucr_Apps = New ucr_Apps()
			e.Control = _ucr_Apps
			_ucr_Apps.Focus()
		End If

		If e.Document Is doc_Cinema Then
			_ucr_Todo_Reload = False
			_ucr_Todo = New ucr_Todo
			e.Control = _ucr_Todo
			_ucr_Todo.Focus()
		End If

		'If e.Document Is doc_TV Then
		'	_ucr_Todo = New ucr_Todo

		'	e.Control = _ucr_Todo
		'	_ucr_Todo.Focus()
		'End If

		If e.Document Is doc_Emulation Then
			_ucr_Emulation_Reload = False
			_ucr_Emulation = New ucr_Emulation
			e.Control = _ucr_Emulation
			_ucr_Emulation.Focus()
		End If
	End Sub

	Private Sub MetroUIView_NavigationBarsShowing(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.Docking2010.Views.WindowsUI.NavigationBarsCancelEventArgs) Handles MetroUIView.NavigationBarsShowing
		If cls_Globals.Suppress_MetroUINavigationBarsShowing Then
			e.Cancel = True
			cls_Globals.Suppress_MetroUINavigationBarsShowing = False
		End If
	End Sub

	Private Sub Handle_Hide() Handles _ucr_Apps.E_Hide, _ucr_Emulation.E_Hide
		Me.Hide()
	End Sub

	Private Sub Handle_Show() Handles _ucr_Apps.E_Show, _ucr_Emulation.E_Show
		Me.Show()
	End Sub

	Private Sub frm_Main_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
		If Me.IsDisposed Then Return

		'Run DB Sync
		Dim blacklist As MKNetLib.cls_MKSQLiteDBSync.cls_MKContentSync_BlacklistItem() = {New MKNetLib.cls_MKSQLiteDBSync.cls_MKContentSync_BlacklistItem("created", "*"),
																						 New MKNetLib.cls_MKSQLiteDBSync.cls_MKContentSync_BlacklistItem("updated", "*"),
																							New MKNetLib.cls_MKSQLiteDBSync.cls_MKContentSync_BlacklistItem("Sort", "tbl_Emu_Extras"),
																							New MKNetLib.cls_MKSQLiteDBSync.cls_MKContentSync_BlacklistItem("Hide", "tbl_Emu_Extras"),
																							New MKNetLib.cls_MKSQLiteDBSync.cls_MKContentSync_BlacklistItem("Weight", "tbl_Emu_Games_Rating_Weights")}
		Dim dbsync As New MKNetLib.cls_MKSQLiteDBSync(System.Windows.Forms.Application.StartupPath & "\ml.db_initial", System.Windows.Forms.Application.StartupPath & "\ml.db", 7, blacklist)

		Try
			dbsync.DoSync()
		Catch ex As Exception
			MKDXHelper.MessageBox("Error while synchronizing databases:" & ControlChars.CrLf & dbsync._SyncLog, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			_Immediate_Close = True
			Me.Close()
			Return
		End Try

		'Database Updates
		Dim sLastRunHost As String = TC.NZ(cls_Settings.GetSetting("LastRunHost"), "")

		Dim DS As New MKNetDXLib.DS_SQLite_DBUpdater

		If sLastRunHost <> System.Environment.MachineName Then
			'ML has lately been run on a different host -> run update scripts for all hosts and for the current host
			DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT Sort AS Version, Script FROM tbl_Startup_Scripts WHERE HostName IS NULL OR HostName = '" & System.Environment.MachineName & "' ORDER BY Sort", DS.tbl_DBUpdates)
		Else
			'ML has lately been run on the same host -> run update scripts only for all hosts
			DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT Sort AS Version, Script FROM tbl_Startup_Scripts WHERE HostName IS NULL ORDER BY Sort", DS.tbl_DBUpdates)
		End If

		Try
			Dim sSQL As String = ""
			sSQL &= "-- ML DB" & ControlChars.CrLf
			sSQL &= "CREATE UNIQUE INDEX IF NOT EXISTS main.IDX_tbl_Settings_Key ON tbl_Settings (Key)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Emu_Games_Hidden ON tbl_Emu_Games (Hidden)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Emu_Games_id_Emu_Games_Owner ON tbl_Emu_Games (id_Emu_Games_Owner)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Emu_Games_id_Moby_Platforms ON tbl_Emu_Games (id_Moby_Platforms)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Emu_Games_Volume_Number ON tbl_Emu_Games (Volume_Number)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Emu_Games_Alternate_Titles_id_Emu_Games ON tbl_Emu_Games_Alternate_Titles (id_Emu_Games)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Emu_Games_Moby_Genres_id_Emu_Games ON tbl_Emu_Games_Moby_Genres (id_Emu_Games)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Emu_Games_Moby_Genres_Used ON tbl_Emu_Games_Moby_Genres (Used)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Emu_Games_Moby_Attributes_id_Emu_Games ON tbl_Emu_Games_Moby_Attributes (id_Emu_Games)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Emu_Games_Moby_Attributes_Used ON tbl_Emu_Games_Moby_Attributes (Used)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Emu_Games_Languages_id_Emu_Games ON tbl_Emu_Games_Languages (id_Emu_Games)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Emu_Games_Regions_id_Emu_Games ON tbl_Emu_Games_Regions (id_Emu_Games)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_History_id_Emu_Games ON tbl_History (id_Emu_Games)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_History_Start ON tbl_History (Start)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Tag_Parser_Content ON tbl_Tag_Parser (Content)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Emulators_Moby_Platforms_id_Moby_Platforms ON tbl_Emulators_Moby_Platforms (id_Moby_Platforms)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_ControlSettings_ControlID ON tbl_ControlSettings (ControlID)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_ControlSettings_SettingID ON tbl_ControlSettings (SettingID)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_ControlSettings_id_Users ON tbl_ControlSettings (id_Users)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Moby_Platforms_Settings_id_Moby_Platforms ON tbl_Moby_Platforms_Settings (id_Moby_Platforms)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Moby_Platforms_Settings_Visible ON tbl_Moby_Platforms_Settings (Visible)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Users_Emu_Games_id_Users ON tbl_Users_Emu_Games (id_Users)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Users_Emu_Games_id_Emu_Games ON tbl_Users_Emu_Games (id_Emu_Games)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Similarity_Calculation_Results_Entries_id_Emu_Games ON tbl_Similarity_Calculation_Results_Entries (id_Emu_Games)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Similarity_Calculation_Results_Entries_id_Moby_Releases ON tbl_Similarity_Calculation_Results_Entries (id_Moby_Releases)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Similarity_Calculation_Results_Entries_id_Similarity_Calculation_Results ON tbl_Similarity_Calculation_Results_Entries (id_Similarity_Calculation_Results)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS main.IDX_tbl_Mame_Roms_name ON tbl_Mame_Roms (name)" & ControlChars.CrLf
			sSQL &= "" & ControlChars.CrLf
			sSQL &= "-- Moby DB" & ControlChars.CrLf
			sSQL &= ";	CREATE UNIQUE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Attribtues_id_Moby_Attributes ON tbl_Moby_Attributes (id_Moby_Attributes)" & ControlChars.CrLf
			sSQL &= ";	CREATE UNIQUE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Attributes_Categories_id_Moby_Attributes_Categories ON tbl_Moby_Attributes_Categories (id_Moby_Attributes_Categories)" & ControlChars.CrLf
			sSQL &= ";	CREATE UNIQUE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Companies_id_Moby_Companies ON tbl_Moby_Companies (id_Moby_Companies)" & ControlChars.CrLf
			sSQL &= ";	CREATE UNIQUE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Games_id_Moby_Games ON tbl_Moby_Games (id_Moby_Games)" & ControlChars.CrLf
			sSQL &= ";	CREATE UNIQUE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Games_Alternate_Titles_id_Moby_Games_Alternate_Titles ON tbl_Moby_Games_Alternate_Titles (id_Moby_Games_Alternate_Titles)" & ControlChars.CrLf
			sSQL &= ";	CREATE UNIQUE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Games_Genres_id_Moby_Games_Genres ON tbl_Moby_Games_Genres (id_Moby_Games_Genres)" & ControlChars.CrLf
			sSQL &= ";	CREATE UNIQUE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Genres_id_Moby_Genres ON tbl_Moby_Genres (id_Moby_Genres)" & ControlChars.CrLf
			sSQL &= ";	CREATE UNIQUE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Platforms_id_Moby_Platforms ON tbl_Moby_Platforms (id_Moby_Platforms)" & ControlChars.CrLf
			sSQL &= ";	CREATE UNIQUE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Releases_id_Moby_Releases ON tbl_Moby_Releases (id_Moby_Releases)" & ControlChars.CrLf
			sSQL &= ";	CREATE UNIQUE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Releases_Attributes_id_Moby_Releases_Attributes ON tbl_Moby_Releases_Attributes (id_Moby_Releases_Attributes)" & ControlChars.CrLf
			sSQL &= "" & ControlChars.CrLf
			sSQL &= "-- Moby DB - Foreign Keys" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Game_Groups_Moby_Releases_id_Moby_Game_Groups ON tbl_Moby_Game_Groups_Moby_Releases (id_Moby_Game_Groups)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Game_Groups_Moby_Releases_id_Moby_Releases ON tbl_Moby_Game_Groups_Moby_Releases (id_Moby_Releases)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Releases_Attributes_id_Moby_Attributes ON tbl_Moby_Releases_Attributes (id_Moby_Attributes)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Releases_Staff_id_Moby_Releases ON tbl_Moby_Releases_Staff (id_Moby_Releases)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Releases_Staff_id_Moby_Staff ON tbl_Moby_Releases_Staff (id_Moby_Staff)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Releases_Cover_Art_id_Moby_Releases ON tbl_Moby_Releases_Cover_Art (id_Moby_Releases)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Releases_Cover_Art_id_Moby_Cover_Art_Types ON tbl_Moby_Releases_Cover_Art (id_Moby_Cover_Art_Types)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Releases_Cover_Art_Packaging ON tbl_Moby_Releases_Cover_Art (Packaging)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Releases_Cover_Art_Regions_id_Moby_Releases_Cover_Art ON tbl_Moby_Releases_Cover_Art_Regions (id_Moby_Releases_Cover_Art)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Releases_Cover_Art_Regions_id_Regions ON tbl_Moby_Releases_Cover_Art_Regions (id_Moby_Regions)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Releases_Screenshots_id_Moby_Releases ON tbl_Moby_Releases_Screenshots (id_Moby_Releases)" & ControlChars.CrLf
			sSQL &= "" & ControlChars.CrLf
			sSQL &= "-- Rombase DB" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS rombase.IDX_tbl_Rombase_size ON tbl_Rombase (size)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS rombase.IDX_tbl_Rombase_crc ON tbl_Rombase (crc)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS rombase.IDX_tbl_Rombase_md5 ON tbl_Rombase (md5)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS rombase.IDX_tbl_Rombase_sha1 ON tbl_Rombase (sha1)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS rombase.IDX_tbl_Rombase_id_Moby_Platforms ON tbl_Rombase (id_Moby_Platforms)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS rombase.IDX_tbl_Rombase_id_Rombase_Owner ON tbl_Rombase (id_Rombase_Owner)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS rombase.IDX_tbl_Rombase_CustomIdentifier ON tbl_Rombase (CustomIdentifier)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS rombase.IDX_tbl_Rombase_Mapping_Identifier ON tbl_Rombase (Mapping_Identifier)" & ControlChars.CrLf
			sSQL &= "" & ControlChars.CrLf
			sSQL &= "-- Main Query Optimization" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Releases_Attributes_id_Moby_Releases ON tbl_Moby_Releases_Attributes (id_Moby_Releases)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Attributes_Rating_Age_From ON tbl_Moby_Attributes (Rating_Age_From)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Games_Alternate_Titles_id_Moby_Games ON tbl_Moby_Games_Alternate_Titles (id_Moby_Games)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Games_Genres_id_Moby_Games ON tbl_Moby_Games_Genres (id_Moby_Games)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Games_Genres_id_Moby_Genres ON tbl_Moby_Games_Genres (id_Moby_Genres)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Genres_id_Moby_Genres ON tbl_Moby_Genres (id_Moby_Genres)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Genres_id_Moby_Basic_Genres ON tbl_Moby_Genres (Basic_Genres)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Genres_id_Moby_Perspectives ON tbl_Moby_Genres (Perspectives)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Genres_id_Moby_Non_Sports_Themes ON tbl_Moby_Genres (Non_Sports_Themes)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Genres_id_Moby_Sports_Themes ON tbl_Moby_Genres (Sports_Themes)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Genres_id_Moby_Educational_Categories ON tbl_Moby_Genres (Educational_Categories)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Genres_id_Moby_Other_Attributes ON tbl_Moby_Genres (Other_Attributes)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Games_URLPart ON tbl_Moby_Games (URLPart)" & ControlChars.CrLf
			sSQL &= ";	CREATE INDEX IF NOT EXISTS moby.IDX_tbl_Moby_Releases_id_Moby_Games_id_Moby_Platforms ON tbl_Moby_Releases (id_Moby_Games, id_Moby_Platforms)" & ControlChars.CrLf
			sSQL &= "" & ControlChars.CrLf
			sSQL &= "-- Rom Manager Optimization" & ControlChars.CrLf
			sSQL &= "; CREATE INDEX IF NOT EXISTS main.IDX_tbl_Emu_Games_File ON tbl_Emu_Games (File)" & ControlChars.CrLf
			sSQL &= "; CREATE INDEX IF NOT EXISTS main.IDX_tbl_Emu_Games_Folder ON tbl_Emu_Games (Folder)" & ControlChars.CrLf
			sSQL &= "; CREATE INDEX IF NOT EXISTS main.IDX_tbl_Emu_Games_InnerFile ON tbl_Emu_Games (InnerFile)" & ControlChars.CrLf
			sSQL &= "; CREATE INDEX IF NOT EXISTS main.IDX_tbl_Emu_Games_CRC32 ON tbl_Emu_Games (CRC32)" & ControlChars.CrLf
			sSQL &= "; CREATE INDEX IF NOT EXISTS main.IDX_tbl_Emu_Games_SHA1 ON tbl_Emu_Games (SHA1)" & ControlChars.CrLf
			sSQL &= "; CREATE INDEX IF NOT EXISTS main.IDX_tbl_Emu_Games_MD5 ON tbl_Emu_Games (MD5)" & ControlChars.CrLf

			DataAccess.FireProcedure(cls_Globals.Conn, 0, sSQL)
		Catch ex As Exception
			MKDXHelper.ExceptionMessageBox(ex, "An error occured while updating indexes on the database." & ControlChars.CrLf & ControlChars.CrLf & "The error was: ", "Metropolis Launcher")
		End Try

		Using frm As New MKNetDXLib.frm_SQLite_DBUpdater(cls_Globals.Conn, DS, AddressOf Application.DoEvents)
			frm.ShowDialog(Me)
		End Using

		cls_Settings.SetSetting("LastRunHost", System.Environment.MachineName)

		'Migrate new DOSBox/ScummVM Profiles from Rombase
		DS_ML.Migrate_Rombase_DOSBox_Configs(cls_Globals.Conn)
		DS_ML.Migrate_Rombase_ScummVM_Configs(cls_Globals.Conn)

#If PreRelease Then
		MKDXHelper.MessageBox("Hi, you are using this PreRelease of Metropolis Launcher, because you agreed in helping the project or have otherwise been deemed worthy." & ControlChars.CrLf & ControlChars.CrLf & "This is build " & Alphaleonis.Win32.Filesystem.File.GetLastWriteTime(System.Reflection.Assembly.GetEntryAssembly().Location).ToString("yyyyMMdd-HHmmss") & ControlChars.CrLf & ControlChars.CrLf & "PLEASE DO NOT REDISTRIBUTE - The final version will be released as freeware when it's done.", "Metropolis Launcher PreRelease", MessageBoxButtons.OK, MessageBoxIcon.Information)
#End If

		Me.tile_Apps.Enabled = True
		Me.tile_Cinema.Enabled = True
		Me.tile_Emulation.Enabled = True
		Me.tile_Settings.Enabled = True

		Me.tilecontainer_Main.Subtitle = ""
	End Sub

	Private Sub _ucr_Settings_E_Rom_Manager_Changed() Handles _ucr_Settings.E_Rom_Manager_Changed
		If _ucr_Emulation IsNot Nothing Then
			_ucr_Emulation.Refill_Emu_Games()
		End If
	End Sub

	Private Sub MetroUIView_ControlReleasing(sender As Object, e As ControlReleasingEventArgs) Handles MetroUIView.ControlReleasing

	End Sub

	Private Sub frm_Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

	End Sub
End Class
