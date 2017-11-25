Imports DataAccess = MKNetLib.cls_MKSQLiteDataAccess
Imports TC = MKNetLib.cls_MKTypeConverter

Public Class ucr_Settings
	Private _sem_Loading As Boolean = True

	Public Event E_Rom_Manager_Changed()

	Public Sub New()
		_sem_Handle_Textboxes_EditValueChanged = True

		InitializeComponent()

		For i As Integer = 0 To cls_Skins.Skins.GetUpperBound(0)
			Dim row As DataRow = BS_Skin.AddNew.Row
			row("id") = i
			row("Skinname") = cls_Skins.Skins(i, 0)
			row("Displayname") = cls_Skins.Skins(i, 1)
			row("Seasonal") = TC.NZ(cls_Skins.Skins(i, 2), False)
		Next

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Ensure_Moby_Platform_Caches(tran)
			tran.Commit()
		End Using

		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT id_Moby_Platforms, Name, URLPart, Display_Name  || ' (' || (SELECT COUNT(1) FROM tbl_Moby_Releases REL WHERE REL.id_Moby_Platforms = PF.id_Moby_Platforms) || ')' AS Display_Name FROM tbl_Moby_Platforms PF WHERE Visible = 1 ORDER BY Display_Name", DS_MobyDB.tbl_Moby_Platforms)

		If cls_Globals.MultiUserMode Then
			btn_Users.Text = "Setup"
		End If

		Me._sem_Loading = False

		txb_Dir_Extras.Text = cls_Settings.Get_Extras_Directory
		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(txb_Dir_Extras.Text) Then
			txb_Dir_Extras.ErrorText = "Directory not found!"
		Else
			cls_Globals.Dir_Extras = txb_Dir_Extras.Text
		End If

		txb_Screenshot_Dir.Text = TC.NZ(cls_Settings.GetSetting("Dir_Screenshot"), "")
		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(txb_Screenshot_Dir.Text) Then
			txb_Screenshot_Dir.ErrorText = "Directory not found!"
		Else
			cls_Globals.Dir_Screenshot = txb_Screenshot_Dir.Text
		End If

		txb_Temp_Dir.Text = cls_Globals.TempDir(Nothing)
		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(txb_Temp_Dir.Text) Then
			txb_Temp_Dir.ErrorText = "Directory not found!"
		Else
			MKNetLib.cls_MKFileSupport.TempDirRoot = cls_Globals.TempDir(Nothing)
		End If

		txb_Backup_Dir.Text = cls_Globals.BackupsDir(Nothing)
		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(txb_Backup_Dir.Text) Then
			txb_Backup_Dir.ErrorText = "Directory not found!"
		End If

		txb_DOSBox_Working_Directory.Text = cls_Settings.Get_DOSBox_CWD
		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(txb_DOSBox_Working_Directory.Text) Then
			txb_DOSBox_Working_Directory.ErrorText = "Directory not found!"
		End If

		txb_J2K.Text = TC.NZ(cls_Settings.GetSetting("Path_J2K"), "")
		If Not Alphaleonis.Win32.Filesystem.File.Exists(txb_J2K.Text) Then
			txb_J2K.ErrorText = "File not found!"
		Else
			'Fill the J2K Config DS
			cls_Settings.Fill_J2K_DS(Me.DS_J2K, TC.NZ(cls_Settings.GetSetting("Path_J2K"), ""))

			'Set the Config
			Dim j2k_config As String = TC.NZ(cls_Settings.GetSetting("Config_J2K"), "")

			If j2k_config = "" Then
				cmb_J2K_Config.EditValue = DBNull.Value
			Else
				MKNetLib.cls_MKClientSupport.SetBindingSourcePosition(BS_J2K, "ConfigName", TC.NZ(cls_Settings.GetSetting("Config_J2K"), "Empty"))
				Try
					cmb_J2K_Config.EditValue = BS_J2K.Current("id_Config")
				Catch ex As Exception

				End Try
			End If
		End If

		Me.chb_StatsEnable.Checked = TC.NZ(cls_Settings.GetSetting("Stats_Enabled", cls_Settings.enm_Settingmodes.Per_User), True)
		Me.spn_StatsMinTime.Value = TC.NZ(cls_Settings.GetSetting("Stats_MinTime", cls_Settings.enm_Settingmodes.Per_User), 0)
		Me.spn_StatsMinTime.Enabled = Me.chb_StatsEnable.Checked

		Me.chb_Downloader.Checked = TC.NZ(cls_Settings.GetSetting("Downloader_Enabled", cls_Settings.enm_Settingmodes.Same_for_All), True)

		Me.cmb_Skin.EditValue = TC.NZ(cls_Settings.GetSetting("Skin", cls_Settings.enm_Settingmodes.Per_User), 4)

		Me.font_Grid.EditValue = TC.NZ(cls_Settings.GetSetting("Font", cls_Settings.enm_Settingmodes.Per_User), "Segoe UI")
		Me.spin_FontSize.EditValue = TC.NZ(cls_Settings.GetSetting("FontSize", cls_Settings.enm_Settingmodes.Per_User), 8)

		If TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT COUNT(1) FROM moby.tbl_Moby_Platforms WHERE id_Moby_Platforms = " & cls_Globals.enm_Moby_Platforms.mame), 0) > 0 Then
			lbl_Mame_Config.Visible = True
			btn_Mame_Config.Visible = True
		End If

		If TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT COUNT(1) FROM moby.tbl_Moby_Platforms WHERE id_Moby_Platforms = " & cls_Globals.enm_Moby_Platforms.dos), 0) > 0 Then
			lbl_DOSBox_Templates.Visible = True
			btn_DOSBox_Templates.Visible = True
		End If

		If TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT COUNT(1) FROM moby.tbl_Moby_Platforms WHERE id_Moby_Platforms = " & cls_Globals.enm_Moby_Platforms.scummvm), 0) > 0 Then
			lbl_ScummVM_Templates.Visible = True
			btn_ScummVM_Templates.Visible = True
		End If

		Me.spn_Backup_Frequency.Value = TC.NZ(cls_Settings.GetSetting("Backup_Frequency", cls_Settings.enm_Settingmodes.Same_for_All), 5)
		Me.spn_Backup_Retention.Value = TC.NZ(cls_Settings.GetSetting("Backup_Retention", cls_Settings.enm_Settingmodes.Same_for_All), 3)

		Me.lbl_Launch_Counter_Value.Text = TC.NZ(cls_Settings.GetSetting("LaunchCounter_BackupRotation"), 0)

#If DEBUG Then
		gb_Internal.Visible = True
#End If

		_sem_Handle_Textboxes_EditValueChanged = False
	End Sub

	Public Sub Set_Controls_Enable()
		'Set any control Enabled = False
		For Each ctrl As Control In pnl_Left.Controls
			If cls_Globals.MultiUserMode = True AndAlso cls_Globals.Admin = False Then
				ctrl.Enabled = False
			Else
				ctrl.Enabled = True
			End If
		Next

		If cls_Globals.MultiUserMode = True AndAlso cls_Globals.Admin = False Then
			lbl_Skin.Enabled = True
			cmb_Skin.Enabled = True
			lbl_Stats.Enabled = True
			lbl_StatsMinTime.Enabled = True
			lbl_StatsMinutes.Enabled = True
			chb_StatsEnable.Enabled = True
			spn_StatsMinTime.Enabled = True
		End If

		If cls_Globals.MultiUserMode = True AndAlso cls_Globals.Restricted = False Then

		End If

		If Not cls_Globals.MultiUserMode Then
			lbl_Password.Visible = False
			btn_Password.Visible = False
		Else
			lbl_Password.Enabled = True
			btn_Password.Enabled = True
		End If

	End Sub

	Private Sub cmb_Skin_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_Skin.EditValueChanged
		If _sem_Loading Then Return
		cls_Skins.LoadSkin(cmb_Skin.EditValue)
	End Sub

	Public Sub Save_Settings()
		If cls_Globals.Conn.State = ConnectionState.Closed Then
			Try
				cls_Globals.Conn.Open()
			Catch ex As Exception
				Return
			End Try
		End If

		cls_Settings.SetSetting("Skin", TC.NZ(Me.cmb_Skin.EditValue, 0), cls_Settings.enm_Settingmodes.Per_User)
		cls_Settings.SetSetting("Dir_Extras", TC.NZ(Me.txb_Dir_Extras.EditValue, ""))
		cls_Settings.SetSetting("Dir_Screenshot", TC.NZ(Me.txb_Screenshot_Dir.EditValue, ""))
		cls_Settings.SetSetting("Dir_Temp", TC.NZ(Me.txb_Temp_Dir.EditValue, ""))
		cls_Settings.SetSetting("Dir_DOSBox_CWD", TC.NZ(Me.txb_DOSBox_Working_Directory.EditValue, ""))
		cls_Settings.SetSetting("Path_J2K", TC.NZ(Me.txb_J2K.EditValue, ""))

		If TC.IsNullNothingOrEmpty(Me.cmb_J2K_Config.EditValue) Then
			cls_Settings.SetSetting("Config_J2K", DBNull.Value)
		Else
			cls_Settings.SetSetting("Config_J2K", IIf(TC.NZ(Me.cmb_J2K_Config.Text, "") = "", "Empty", TC.NZ(Me.cmb_J2K_Config.Text, "")))
		End If

		cls_Settings.SetSetting("Font", TC.NZ(Me.font_Grid.EditValue, "Segoe UI"), cls_Settings.enm_Settingmodes.Per_User)
		cls_Settings.SetSetting("FontSize", TC.NZ(Me.spin_FontSize.EditValue, 8), cls_Settings.enm_Settingmodes.Per_User)

		cls_Settings.SetSetting("Stats_Enabled", Me.chb_StatsEnable.Checked, cls_Settings.enm_Settingmodes.Per_User)
		cls_Settings.SetSetting("Stats_MinTime", Me.spn_StatsMinTime.Value, cls_Settings.enm_Settingmodes.Per_User)

		cls_Settings.SetSetting("Downloader_Enabled", Me.chb_Downloader.Checked, cls_Settings.enm_Settingmodes.Same_for_All)

		cls_Settings.SetSetting("Backup_Frequency", spn_Backup_Frequency.Value)
		cls_Settings.SetSetting("Backup_Retention", spn_Backup_Retention.Value)
		cls_Settings.SetSetting("Dir_Backup", txb_Backup_Dir.Text)
	End Sub

	Private Sub Handle_Font_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles font_Grid.EditValueChanged, spin_FontSize.EditValueChanged, spn_StatsMinTime.EditValueChanged
		If Me._sem_Loading Then Return
		If Not TC.IsNullNothingOrEmpty(font_Grid.EditValue) AndAlso spin_FontSize.Value > 0 Then
			cls_Fonts.ApplyFont(font_Grid.EditValue, spin_FontSize.Value)
		End If
	End Sub

	Private Sub btn_Rombase_Manager_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_RomBase_Manager.Click
		Using frm As New frm_ROMBase_Manager
			frm.ShowDialog(Me.ParentForm)
		End Using
	End Sub

	Private Sub tbl_EmulatorSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_EmulatorSettings.Click
		Using frm As New frm_Emulators
			frm.ShowDialog(Me.ParentForm)
		End Using
	End Sub

	Private Sub btn_Rom_Manager_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Rom_Manager.Click
		Using frm As New frm_Rom_Manager
			If frm.ShowDialog(Me.ParentForm) <> DialogResult.Cancel Then
				RaiseEvent E_Rom_Manager_Changed()
			End If
		End Using
	End Sub

	Private Sub Btn_MovieManager_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Movie_Manager.Click
		Using frm As New frm_Movie_Manager
			frm.ShowDialog(Me.ParentForm)
		End Using
	End Sub

	Private Sub Handle_Directory_Buttons_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Dir_Extras.Click, btn_Screenshot_Dir.Click, btn_Temp_Dir.Click, btn_DOSBox_Working_Directory.Click, btn_Backup_Dir.Click
		Dim sFolder As String = MKNetLib.cls_MKFileSupport.OpenFolderDialog
		If Alphaleonis.Win32.Filesystem.Directory.Exists(sFolder) Then
			If sender Is btn_Dir_Extras Then
				txb_Dir_Extras.Focus()
				Me.txb_Dir_Extras.EditValue = sFolder
				cls_Globals.Dir_Extras = sFolder
				txb_Dir_Extras.DoValidate()
				btn_Dir_Extras.Focus()
			End If
			If sender Is btn_Screenshot_Dir Then
				txb_Screenshot_Dir.Focus()
				Me.txb_Screenshot_Dir.EditValue = sFolder
				cls_Globals.Dir_Screenshot = sFolder
				txb_Screenshot_Dir.DoValidate()
				btn_Screenshot_Dir.Focus()
			End If
			If sender Is btn_Temp_Dir Then
				txb_Temp_Dir.Focus()
				Me.txb_Temp_Dir.EditValue = sFolder
				txb_Temp_Dir.DoValidate()
				btn_Temp_Dir.Focus()
			End If
			If sender Is btn_DOSBox_Working_Directory Then
				txb_DOSBox_Working_Directory.Focus()
				Me.txb_DOSBox_Working_Directory.EditValue = sFolder
				txb_DOSBox_Working_Directory.DoValidate()
				btn_DOSBox_Working_Directory.Focus()
			End If
			If sender Is btn_Backup_Dir Then
				txb_Backup_Dir.Focus()
				Me.txb_Backup_Dir.EditValue = sFolder
				txb_Backup_Dir.DoValidate()
				txb_Backup_Dir.Focus()
			End If
		End If
	End Sub

	Private Sub Handle_Directory_Textboxes_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs, Optional ByVal NewValue As Object = Nothing) Handles txb_Dir_Extras.Validating, txb_Screenshot_Dir.Validating, txb_Temp_Dir.Validating, txb_DOSBox_Working_Directory.Validating
		If NewValue Is Nothing Then
			If Not Alphaleonis.Win32.Filesystem.Directory.Exists(TC.NZ(CType(sender, MKNetDXLib.ctl_MKDXTextEdit).EditValue, "")) Then
				CType(sender, MKNetDXLib.ctl_MKDXTextEdit).ErrorText = "Directory not found!"
			Else
				If sender Is txb_Dir_Extras Then cls_Globals.Dir_Extras = CType(sender, MKNetDXLib.ctl_MKDXTextEdit).Text
				If sender Is txb_Screenshot_Dir Then cls_Globals.Dir_Screenshot = CType(sender, MKNetDXLib.ctl_MKDXTextEdit).Text
			End If
		Else
			If Not Alphaleonis.Win32.Filesystem.Directory.Exists(TC.NZ(NewValue, "")) Then
				CType(sender, MKNetDXLib.ctl_MKDXTextEdit).ErrorText = "Directory not found!"
			Else
				If sender Is txb_Dir_Extras Then cls_Globals.Dir_Extras = TC.NZ(NewValue, "")
				If sender Is txb_Screenshot_Dir Then cls_Globals.Dir_Screenshot = TC.NZ(NewValue, "")
			End If
		End If
	End Sub

	Private Sub Handle_File_Textboxes_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txb_J2K.Validating
		If TC.NZ(CType(sender, MKNetDXLib.ctl_MKDXButtonEdit).EditValue, "") <> "" AndAlso Not Alphaleonis.Win32.Filesystem.File.Exists(TC.NZ(CType(sender, MKNetDXLib.ctl_MKDXButtonEdit).EditValue, "")) Then
			CType(sender, MKNetDXLib.ctl_MKDXButtonEdit).ErrorText = "File not found!"
		End If
	End Sub

	Private Sub chb_StatsEnable_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chb_StatsEnable.CheckStateChanged
		Me.spn_StatsMinTime.Enabled = Me.chb_StatsEnable.Checked
	End Sub

	Private Sub btn_ReSync_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ReSync.Click
		If MKDXHelper.MessageBox("Do you really want to re-sync every data to the rombase?", "Re-Sync", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> DialogResult.Yes Then
			Return
		End If

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			Try
				'### 0. Find Data in tbl_Emu_Games Main Entries (id_Emu_Games_Owner IS NULL) not associated to tbl_Rombase but containing interesting data
				'filename, size, crc, md5, sha1, id_Moby_Platforms, id_Moby_Releases, Moby_Platforms_URLPart, Moby_Games_URLPart
				'or all the Mapping entries
				Dim sSQL As String = ""
				sSQL &= "	SELECT" & ControlChars.CrLf
				sSQL &= "		GAME.id_Emu_Games" & ControlChars.CrLf
				sSQL &= "		, GAME.InnerFile" & ControlChars.CrLf
				sSQL &= "		, GAME.Size" & ControlChars.CrLf
				sSQL &= "		, GAME.CRC32" & ControlChars.CrLf
				sSQL &= "		, GAME.MD5" & ControlChars.CrLf
				sSQL &= "		, GAME.SHA1" & ControlChars.CrLf
				sSQL &= "		, GAME.id_Moby_Platforms" & ControlChars.CrLf
				sSQL &= "		, GAME.id_Moby_Platforms_Alternative" & ControlChars.CrLf
				sSQL &= "		, REL.id_Moby_Releases" & ControlChars.CrLf
				sSQL &= "		, PLTFM.URLPart AS Moby_Platforms_URLPart" & ControlChars.CrLf
				sSQL &= "		, MG.URLPart AS Moby_Games_URLPart" & ControlChars.CrLf
				sSQL &= "		, GAME.CustomIdentifier" & ControlChars.CrLf
				sSQL &= "	FROM tbl_Emu_Games GAME" & ControlChars.CrLf
				sSQL &= "	LEFT JOIN moby.tbl_Moby_Platforms PLTFM ON GAME.id_Moby_Platforms = PLTFM.id_Moby_Platforms" & ControlChars.CrLf
				sSQL &= "	LEFT JOIN moby.tbl_Moby_Games MG ON GAME.Moby_Games_URLPart = MG.URLPart" & ControlChars.CrLf
				sSQL &= "	LEFT JOIN moby.tbl_Moby_Releases REL ON REL.id_Moby_Platforms = IFNULL(GAME.id_Moby_Platforms_Alternative, GAME.id_Moby_Platforms) AND REL.id_Moby_Games = MG.id_Moby_Games" & ControlChars.CrLf
				sSQL &= "	WHERE GAME.id_Moby_Platforms <> 3 AND GAME.id_Moby_Platforms <> -2 " & ControlChars.CrLf  'Don't export Windows games (3) and M.A.M.E. games (-2)
				sSQL &= " AND id_Emu_Games_Owner IS NULL" & ControlChars.CrLf 'Only Main Entries
				sSQL &= "	AND REL.id_Moby_Platforms IS NOT NULL" & ControlChars.CrLf
				sSQL &= "	AND GAME.id_Rombase IS NULL" & ControlChars.CrLf
				sSQL &= "	AND" & ControlChars.CrLf
				sSQL &= "	(" & ControlChars.CrLf
				sSQL &= "		GAME.id_Moby_Platforms <> 2" & ControlChars.CrLf  'If Platform is not DOS, just import every entry, even without interesting info
				sSQL &= "		OR" & ControlChars.CrLf
				sSQL &= "		GAME.Moby_Games_URLPart IS NOT NULL" & ControlChars.CrLf
				sSQL &= "		OR" & ControlChars.CrLf
				sSQL &= "		GAME.Name IS NOT NULL OR GAME.Note IS NOT NULL OR GAME.Publisher IS NOT NULL OR GAME.Developer IS NOT NULL OR GAME.Description IS NOT NULL OR GAME.SpecialInfo IS NOT NULL OR GAME.Year IS NOT NULL OR GAME.Version IS NOT NULL OR GAME.Alt IS NOT NULL OR GAME.Trainer IS NOT NULL OR GAME.Translation IS NOT NULL OR GAME.Hack IS NOT NULL OR GAME.Bios IS NOT NULL OR GAME.Prototype IS NOT NULL OR GAME.Alpha IS NOT NULL OR GAME.Beta IS NOT NULL OR GAME.Sample IS NOT NULL OR GAME.Kiosk IS NOT NULL OR GAME.Unlicensed IS NOT NULL OR GAME.Fixed IS NOT NULL OR GAME.Pirated IS NOT NULL OR GAME.Good IS NOT NULL OR GAME.Bad IS NOT NULL OR GAME.Overdump IS NOT NULL OR GAME.PublicDomain IS NOT NULL" & ControlChars.CrLf
				sSQL &= "	)" & ControlChars.CrLf
				sSQL &= "	ORDER BY GAME.InnerFile" & ControlChars.CrLf

				Dim dt_EmuGames As DataTable = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, Nothing, tran)

				'Write Main Entry to tbl_Rombase
				'Get id_Rombase
				'Write id_Rombase to tbl_Emu_Games

				'Get Sub-Entries, write those into tbl_Rombase, update id_Rombase for each Sub-Entry
				For Each row_Emu_Games In dt_EmuGames.Rows
					Dim id_Rombase As Integer = DS_Rombase.Upsert_Rombase(tran, DBNull.Value, row_Emu_Games("InnerFile"), row_Emu_Games("Size"), row_Emu_Games("CRC32"), row_Emu_Games("MD5"), row_Emu_Games("SHA1"), row_Emu_Games("id_Moby_Platforms"), row_Emu_Games("id_Moby_Releases"), row_Emu_Games("Moby_Platforms_URLPart"), row_Emu_Games("Moby_Games_URLPart"), CustomIdentifier:=row_Emu_Games("CustomIdentifier"), id_Moby_Platforms_Alternative:=row_Emu_Games("id_Moby_Platforms_Alternative"))
					If id_Rombase <> 0 Then
						DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Emu_Games SET id_Rombase = " & TC.getSQLFormat(id_Rombase) & " WHERE id_Emu_Games = " & TC.getSQLFormat(row_Emu_Games("id_Emu_Games")), tran)
					End If

					'Sub-Entries
					sSQL = ""
					sSQL &= "	SELECT" & ControlChars.CrLf
					sSQL &= "		GAME.id_Emu_Games" & ControlChars.CrLf
					sSQL &= "		, GAME.InnerFile" & ControlChars.CrLf
					sSQL &= "		, GAME.Size" & ControlChars.CrLf
					sSQL &= "		, GAME.CRC32" & ControlChars.CrLf
					sSQL &= "		, GAME.MD5" & ControlChars.CrLf
					sSQL &= "		, GAME.SHA1" & ControlChars.CrLf
					sSQL &= "		, GAME.id_Moby_Platforms" & ControlChars.CrLf
					sSQL &= "		, GAME.id_Moby_Platforms_Alternative" & ControlChars.CrLf
					sSQL &= "		, REL.id_Moby_Releases" & ControlChars.CrLf
					sSQL &= "		, PLTFM.URLPart AS Moby_Platforms_URLPart" & ControlChars.CrLf
					sSQL &= "		, MG.URLPart AS Moby_Games_URLPart" & ControlChars.CrLf
					sSQL &= "		, GAME.CustomIdentifier" & ControlChars.CrLf
					sSQL &= "	FROM tbl_Emu_Games GAME" & ControlChars.CrLf
					sSQL &= "	LEFT JOIN moby.tbl_Moby_Platforms PLTFM ON GAME.id_Moby_Platforms = PLTFM.id_Moby_Platforms" & ControlChars.CrLf
					sSQL &= "	LEFT JOIN moby.tbl_Moby_Games MG ON GAME.Moby_Games_URLPart = MG.URLPart" & ControlChars.CrLf
					sSQL &= "	LEFT JOIN moby.tbl_Moby_Releases REL ON REL.id_Moby_Platforms = GAME.id_Moby_Platforms AND REL.id_Moby_Games = MG.id_Moby_Games" & ControlChars.CrLf
					sSQL &= "	WHERE id_Emu_Games_Owner = " & TC.getSQLFormat(row_Emu_Games("id_Emu_Games")) & ControlChars.CrLf 'All the Sub-Entries of the current Emu_Game
					sSQL &= "	ORDER BY GAME.InnerFile" & ControlChars.CrLf

					Dim dt_EmuGames_Sub As DataTable = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, Nothing, tran)

					For Each row_Emu_Games_Sub In dt_EmuGames_Sub.Rows
						'Note: this takes ages!
						Dim id_Rombase_Sub As Integer = DS_Rombase.Upsert_Rombase(tran, DBNull.Value, row_Emu_Games_Sub("InnerFile"), row_Emu_Games_Sub("Size"), row_Emu_Games_Sub("CRC32"), row_Emu_Games_Sub("MD5"), row_Emu_Games_Sub("SHA1"), row_Emu_Games_Sub("id_Moby_Platforms"), row_Emu_Games_Sub("id_Moby_Releases"), row_Emu_Games_Sub("Moby_Platforms_URLPart"), row_Emu_Games_Sub("Moby_Games_URLPart"), id_Rombase, row_Emu_Games_Sub("CustomIdentifier"), id_Moby_Platforms_Alternative:=row_Emu_Games_Sub("id_Moby_Platforms_Alternative"))
						If id_Rombase_Sub <> 0 Then
							DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Emu_Games SET id_Rombase = " & TC.getSQLFormat(id_Rombase_Sub) & " WHERE id_Emu_Games = " & TC.getSQLFormat(row_Emu_Games_Sub("id_Emu_Games")), tran)
						End If
					Next
				Next

				'Mapping Templates
				Dim dt_Templates As DataTable = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT * FROM tbl_Emu_Games WHERE id_Rombase IS NULL AND id_Emu_Games < 0 AND Mapping_Identifier IS NOT NULL")
				For Each row_Emu_Games In dt_Templates.Rows
					Dim id_Rombase As Integer = DS_Rombase.Upsert_Rombase(tran, row_Emu_Games("Mapping_Identifier"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value)
					If id_Rombase <> 0 Then
						DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Emu_Games SET id_Rombase = " & TC.getSQLFormat(id_Rombase) & " WHERE id_Emu_Games = " & TC.getSQLFormat(row_Emu_Games("id_Emu_Games")), tran)
					End If
				Next

				'### 1. Pump Data from main.tbl_Emu_Games into rombase.tbl_Rombase
				'Get all necessary columns
				'TODO: CAREFUL with id_DOSBox_Configs_Templates!
				Dim dt_cols As DataTable = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT * FROM rombase.tbl_Rombase LIMIT 0", Nothing, tran)

				sSQL = ""
				sSQL &= "	REPLACE INTO rombase.tbl_Rombase"
				sSQL &= "	("

				Dim bSkipComma As Boolean = True
				For Each col As DataColumn In dt_cols.Columns
					If bSkipComma Then
						bSkipComma = False
					Else
						sSQL &= "	, "
					End If

					sSQL &= "	[" & col.ColumnName & "]" & ControlChars.CrLf
				Next
				sSQL &= "	)"
				sSQL &= "	SELECT"

				bSkipComma = True

				For Each col As DataColumn In dt_cols.Columns
					If bSkipComma Then
						bSkipComma = False
					Else
						sSQL &= "	, "
					End If

					Select Case col.ColumnName.ToLower
						Case "filename", "size", "crc", "md5", "sha1", "id_moby_platforms", "id_rombase_owner"
							sSQL &= "	RB.[" & col.ColumnName & "]" & ControlChars.CrLf
						Case "id_moby_releases"
							sSQL &= "	("
							sSQL &= "		SELECT id_Moby_Releases"
							sSQL &= "		FROM	moby.tbl_Moby_Releases MR"
							sSQL &= "		INNER JOIN moby.tbl_Moby_Games MG ON MR.id_Moby_Games = MG.id_Moby_Games AND MG.URLPart = EG.Moby_Games_URLPart"
							sSQL &= "		INNER JOIN moby.tbl_Moby_Platforms MP ON MR.id_Moby_Platforms = MP.id_Moby_Platforms"
							sSQL &= "		WHERE MR.id_Moby_Platforms = EG.id_Moby_Platforms"
							sSQL &= "					AND MG.URLPart = EG.Moby_Games_URLPart"
							sSQL &= "		LIMIT 1"
							sSQL &= "	) AS id_Moby_Releases" & ControlChars.CrLf
						Case "moby_platforms_urlpart"
							sSQL &= "	(SELECT URLPart FROM moby.tbl_Moby_Platforms WHERE id_Moby_Platforms = EG.id_Moby_Platforms) AS Moby_Platforms_URLPart" & ControlChars.CrLf
						Case "id_rombase_dosbox_configs"
							sSQL &= "	("
							sSQL &= "		SELECT DBC.id_Rombase_DOSBox_Configs"
							sSQL &= "		FROM tbl_DOSBox_Configs DBC"
							sSQL &= "		WHERE DBC.id_DOSBox_Configs = EG.id_DOSBox_Configs_Template"
							sSQL &= "	) AS id_Rombase_DOSBox_Configs" & ControlChars.CrLf
						Case Else
							sSQL &= "	EG.[" & col.ColumnName & "]" & ControlChars.CrLf
					End Select
				Next

				sSQL &= "	FROM main.tbl_Emu_Games EG"
				sSQL &= "	INNER JOIN rombase.tbl_Rombase RB ON EG.id_Rombase = RB.id_Rombase"
				sSQL &= "	WHERE EG.id_Rombase IS NOT NULL"

				DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)

				'### 2. tbl_Emu_Games_Alternate_Titles -> tbl_Rombase_Alternate_Titles
				DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM rombase.tbl_Rombase_Alternate_Titles WHERE id_Rombase IN (SELECT EG.id_Rombase FROM main.tbl_Emu_Games_Alternate_Titles EGAT INNER JOIN main.tbl_Emu_Games EG ON EGAT.id_Emu_Games = EG.id_Emu_Games); INSERT INTO rombase.tbl_Rombase_Alternate_Titles (id_Rombase, Alternate_Title, Description) SELECT	EG.id_Rombase, EGAT.Alternate_Title, EGAT.Description FROM main.tbl_Emu_Games_Alternate_Titles EGAT INNER JOIN main.tbl_Emu_Games EG ON EGAT.id_Emu_Games = EG.id_Emu_Games WHERE EG.id_Rombase IS NOT NULL", tran)

				'### 3. tbl_Emu_Games_Moby_Attributes -> tbl_Rombase_Moby_Attributes 
				DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM rombase.tbl_Rombase_Moby_Attributes WHERE id_Rombase IN (SELECT EG.id_Rombase FROM main.tbl_Emu_Games_Moby_Attributes EGMA INNER JOIN main.tbl_Emu_Games EG ON EGMA.id_Emu_Games = EG.id_Emu_Games); INSERT INTO rombase.tbl_Rombase_Moby_Attributes (id_Rombase, id_Moby_Attributes, Used) SELECT	EG.id_Rombase, EGMA.id_Moby_Attributes, EGMA.Used FROM main.tbl_Emu_Games_Moby_Attributes EGMA INNER JOIN main.tbl_Emu_Games EG ON EGMA.id_Emu_Games = EG.id_Emu_Games WHERE EG.id_Rombase IS NOT NULL", tran)

				'### 4. Moby Genres
				DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM rombase.tbl_Rombase_Moby_Genres WHERE id_Rombase IN (SELECT EG.id_Rombase FROM main.tbl_Emu_Games_Moby_Genres EGMG INNER JOIN main.tbl_Emu_Games EG ON EGMG.id_Emu_Games = EG.id_Emu_Games); INSERT INTO rombase.tbl_Rombase_Moby_Genres (id_Rombase, id_Moby_Genres, Used) SELECT	EG.id_Rombase, EGMG.id_Moby_Genres, EGMG.Used FROM main.tbl_Emu_Games_Moby_Genres EGMG INNER JOIN main.tbl_Emu_Games EG ON EGMG.id_Emu_Games = EG.id_Emu_Games WHERE EG.id_Rombase IS NOT NULL", tran)

				'### 5. Languages
				DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM rombase.tbl_Rombase_Languages WHERE id_Rombase IN (SELECT EG.id_Rombase FROM main.tbl_Emu_Games_Languages EGL INNER JOIN main.tbl_Emu_Games EG ON EGL.id_Emu_Games = EG.id_Emu_Games); INSERT INTO rombase.tbl_Rombase_Languages (id_Rombase, id_Languages) SELECT	EG.id_Rombase, EGL.id_Languages FROM main.tbl_Emu_Games_Languages EGL INNER JOIN main.tbl_Emu_Games EG ON EGL.id_Emu_Games = EG.id_Emu_Games WHERE EG.id_Rombase IS NOT NULL", tran)

				'### 6. Regions
				DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM rombase.tbl_Rombase_Regions WHERE id_Rombase IN (SELECT EG.id_Rombase FROM main.tbl_Emu_Games_Regions EGR INNER JOIN main.tbl_Emu_Games EG ON EGR.id_Emu_Games = EG.id_Emu_Games); INSERT INTO rombase.tbl_Rombase_Regions (id_Rombase, id_Regions) SELECT	EG.id_Rombase, EGR.id_Regions FROM main.tbl_Emu_Games_Regions EGR INNER JOIN main.tbl_Emu_Games EG ON EGR.id_Emu_Games = EG.id_Emu_Games WHERE EG.id_Rombase IS NOT NULL", tran)

				'### 7. tbl_Tag_Parser
				DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM rombase.tbl_Rombase_Tag_Parser; INSERT INTO rombase.tbl_Rombase_Tag_Parser (Apply, Content, Note, Note_HighPriority, Year, Bios, Hack, Trainer, Version, Prototype, Beta, Translation, Alt, Unlicensed, Good, Bad, Fixed, Overdump, Pirated, Alpha, Kiosk, Sample, En, Ja, Fr, De, Es, It, Nl, Pt, Sv, No, Da, Fi, Zh, Ko, Pl, NTSC, PAL, World, Europe, USA, Australia, Japan, Korea, China, Asia, Brazil, Canada, France, Germany, HongKong, Italy, Netherlands, Russia, Spain, Sweden, Taiwan, created, updated, MV_Group_Criteria, MV_Volume_Number, Publisher, Hu, Gr, Found_In, Ar, Be, Cz, Ru, Sl, Sr) SELECT Apply, Content, Note, Note_HighPriority, Year, Bios, Hack, Trainer, Version, Prototype, Beta, Translation, Alt, Unlicensed, Good, Bad, Fixed, Overdump, Pirated, Alpha, Kiosk, Sample, En, Ja, Fr, De, Es, It, Nl, Pt, Sv, No, Da, Fi, Zh, Ko, Pl, NTSC, PAL, World, Europe, USA, Australia, Japan, Korea, China, Asia, Brazil, Canada, France, Germany, HongKong, Italy, Netherlands, Russia, Spain, Sweden, Taiwan, created, updated, MV_Group_Criteria, MV_Volume_Number, Publisher, Hu, Gr, Found_In, Ar, Be, Cz, Ru, Sl, Sr FROM main.tbl_Tag_Parser", tran)

				tran.Commit()

				MKDXHelper.MessageBox("Re-Sync to Rombase successful.", "Re-Sync successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
			Catch ex As Exception
				MKDXHelper.ExceptionMessageBox(ex)
				tran.Rollback()
			End Try
		End Using
	End Sub

	Private Sub btn_Mame_Config_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Mame_Config.Click
		Using frm As New frm_Mame_Config
			If frm.ShowDialog(Me.ParentForm) <> DialogResult.Cancel Then
				RaiseEvent E_Rom_Manager_Changed()
			End If
		End Using
	End Sub

	Private Sub btn_DOSBox_Templates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_DOSBox_Templates.Click
		Using frm As New frm_DOSBox_Templates
			frm.ShowDialog(Me.ParentForm)
		End Using
	End Sub

	Private Sub btn_ScummVM_Templates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ScummVM_Templates.Click
		Using frm As New frm_ScummVM_Templates
			frm.ShowDialog(Me.ParentForm)
		End Using
	End Sub


	Private Sub btn_J2K_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_J2K.Click
		Dim j2k_path As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("Browse to your J2K Installation", "(J2K.exe)|J2K.exe", ParentForm:=Me.ParentForm)
		If Alphaleonis.Win32.Filesystem.File.Exists(j2k_path) Then
			Me.DS_J2K.Clear()
			Try
				Dim fv As FileVersionInfo = FileVersionInfo.GetVersionInfo(j2k_path)
				If fv.ProductName.ToLower = "j2k" Then
					'All Good
					txb_J2K.Focus()
					Me.txb_J2K.EditValue = j2k_path
					txb_J2K.DoValidate()
					btn_J2K.Focus()

					'Fill the J2K Config DS
					Dim Config_Old As Object = Nothing
					If BS_J2K.Current IsNot Nothing AndAlso Not TC.IsNullNothingOrEmpty(cmb_J2K_Config.EditValue) Then
						Config_Old = BS_J2K.Current("ConfigName")
					End If

					If cmb_J2K_Config.EditValue Is DBNull.Value OrElse cmb_J2K_Config.EditValue Is Nothing Then
						Config_Old = DBNull.Value
					End If

					cls_Settings.Fill_J2K_DS(Me.DS_J2K, j2k_path)

					If TC.NZ(Config_Old, "") <> "" Then
						MKNetLib.cls_MKClientSupport.SetBindingSourcePosition(BS_J2K, "ConfigName", Config_Old)
						Try
							cmb_J2K_Config.EditValue = BS_J2K.Current("id_Config")
						Catch ex As Exception

						End Try
					Else
						cmb_J2K_Config.EditValue = DBNull.Value
					End If

					If Config_Old Is DBNull.Value OrElse Config_Old Is Nothing Then
						cmb_J2K_Config.EditValue = DBNull.Value
					End If
				Else
					MKDXHelper.MessageBox("The file you selected cannot be recognized as J2K.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Return
				End If
			Catch ex As Exception
				MKDXHelper.ExceptionMessageBox(ex, "Error while reading " & j2k_path & ". The error was:" & ControlChars.CrLf)
			End Try
		End If
	End Sub

	Private Sub cmb_J2K_Config_ButtonPressed(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_J2K_Config.ButtonPressed
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
			cmb_J2K_Config.EditValue = DBNull.Value
		End If
	End Sub

	Private Sub btn_RombaseCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_RombaseCheck.Click
		'frm_Rom_Manager.Get_and_Apply_id_Rombase(cls_Globals.Conn.BeginTransaction, Nothing, 653677)
	End Sub

	Private Sub btn_Platform_Settings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Platform_Settings.Click
		Using frm As New frm_Moby_Platforms_Configuration
			frm.ShowDialog(Me.ParentForm)
		End Using
	End Sub

	Private Sub btn_Refresh_Caches_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Refresh_Caches.Click
		Me.Cursor = Cursors.WaitCursor

		Try
			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				DS_ML.Update_tbl_Emu_Games_Caches(tran)
				tran.Commit()
			End Using

			MKDXHelper.MessageBox("Caches have been successfully refreshed.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception

		End Try

		Me.Cursor = Cursors.Default
	End Sub

	Private Sub btn_Users_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Users.Click
		If Not cls_Globals.MultiUserMode Then
			'Initial Setup
			If MKDXHelper.MessageBox("Multi User Mode allows you to define more than one user for Metropolis Launcher. Each user can be given a password. It is also possible to restict any user to certain pre-selected games (parental control *hint*hint*)." & ControlChars.CrLf & ControlChars.CrLf & "If you click 'yes', you will enable Multi User mode and an unrestricted user 'Admin' will be created. It is strongly advised to define a password for this user.", "Enable Multi User Mode", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
				If Not DataAccess.FireProcedure(cls_Globals.Conn, 0, "INSERT INTO tbl_Users (Admin, Username, Restricted) VALUES (1, 'Admin', 0)") Then
					MKDXHelper.MessageBox("Enabling Multi User Mode was not possible, please try again after restarting Metropolis Launcher. If the problem still persists, please contact the developer.", "Enable Multi User Mode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Return
				End If

				cls_Globals.MultiUserMode = True
				cls_Globals.id_Users = DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_Users FROM tbl_Users WHERE Admin = 1")
				cls_Globals.Restricted = False
				btn_Users.Text = "Setup"
			Else
				Return
			End If
		End If

		Using frm As New frm_Users_Setup
			frm.ShowDialog(Me.ParentForm)
		End Using
	End Sub

	Private Sub btn_Password_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Password.Click
		Dim DS_ML As New DS_ML

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Fill_tbl_Users(tran, DS_ML.tbl_Users, cls_Globals.id_Users, False)
		End Using

		Dim row As DataRow = DS_ML.tbl_Users.Select("id_Users = " & cls_Globals.id_Users)(0)

		Using frm As New frm_Users_Edit(New DS_ML.tbl_UsersDataTable, cls_Globals.id_Users, frm_Users_Edit.enm_EditMode.Password)
			frm.txb_Username.EditValue = row("Username")
			frm.lbl_Restricted.Enabled = Not TC.NZ(row("Admin"), False)
			frm.chb_Restricted.Enabled = Not TC.NZ(row("Admin"), False)
			frm.chb_Restricted.Checked = TC.NZ(row("Restricted"), False)
			frm.chb_Password.Checked = True
			frm.chb_Password.Visible = False

			frm.lbl_Password.SuperTip = Nothing
			frm.txb_Password.SuperTip = Nothing

			frm.lbl_Username.SuperTip = Nothing
			frm.txb_Username.SuperTip = Nothing

			If frm.ShowDialog(Me.ParentForm) = DialogResult.OK Then
				'row("Username") = frm.txb_Username.EditValue
				row("Password") = IIf(frm.chb_Password.Checked, IIf(frm.txb_Password.Text.Length > 0, cls_Globals.Encode_Password(frm.txb_Password.EditValue), DBNull.Value), row("Password"))
				'row("Restricted") = frm.chb_Restricted.Checked

				Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
					DS_ML.Upsert_tbl_Users(tran, DS_ML.tbl_Users)
					tran.Commit()
				End Using
			End If
		End Using
	End Sub

	Private _sem_Handle_Textboxes_EditValueChanged As Boolean = False

	Private Sub Handle_Textboxes_EditValueChanged(sender As Object, e As EventArgs) Handles txb_Temp_Dir.EditValueChanged, txb_Screenshot_Dir.EditValueChanged, txb_J2K.EditValueChanged, txb_DOSBox_Working_Directory.EditValueChanged, txb_Dir_Extras.EditValueChanged, txb_Backup_Dir.EditValueChanged
		If _sem_Handle_Textboxes_EditValueChanged Then Return

		If sender Is txb_Dir_Extras Then
			txb_Dir_Extras.DoValidate()
		End If
		If sender Is txb_Screenshot_Dir Then
			txb_Screenshot_Dir.DoValidate()
		End If
		If sender Is txb_Temp_Dir Then
			txb_Temp_Dir.DoValidate()
		End If
		If sender Is txb_DOSBox_Working_Directory Then
			txb_DOSBox_Working_Directory.DoValidate()
		End If

		Save_Settings()
	End Sub

	Private Sub Handle_Textboxes_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles txb_Temp_Dir.EditValueChanging, txb_Screenshot_Dir.EditValueChanging, txb_J2K.EditValueChanging, txb_DOSBox_Working_Directory.EditValueChanging, txb_Dir_Extras.EditValueChanging, txb_Backup_Dir.EditValueChanging
		If _sem_Handle_Textboxes_EditValueChanged Then Return

		If sender Is txb_Dir_Extras Then
			If Alphaleonis.Win32.Filesystem.Directory.Exists(e.NewValue) Then
				cls_Globals.Dir_Extras = e.NewValue
			End If
			Handle_Directory_Textboxes_Validating(txb_Dir_Extras, Nothing, e.NewValue)
		End If
		If sender Is txb_Screenshot_Dir Then
			If Alphaleonis.Win32.Filesystem.Directory.Exists(e.NewValue) Then
				cls_Globals.Dir_Screenshot = e.NewValue
			End If
			Handle_Directory_Textboxes_Validating(txb_Screenshot_Dir, Nothing, e.NewValue)
		End If
		If sender Is txb_Temp_Dir Then
			Handle_Directory_Textboxes_Validating(txb_Temp_Dir, Nothing, e.NewValue)
		End If
		If sender Is txb_DOSBox_Working_Directory Then
			Handle_Directory_Textboxes_Validating(txb_DOSBox_Working_Directory, Nothing, e.NewValue)
		End If
	End Sub

	Private Sub txb_J2K_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txb_J2K.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
			txb_J2K.Text = ""
		End If
	End Sub

	Private Sub btn_Known_Emulators_Click(sender As Object, e As EventArgs) Handles btn_Known_Emulators.Click
		Using frm As New frm_Known_Emulators
			frm.ShowDialog()
		End Using
	End Sub
End Class
