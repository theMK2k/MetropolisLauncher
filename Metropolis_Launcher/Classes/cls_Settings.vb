Public Class cls_Settings
	Public Enum enm_Settingmodes
		Same_for_All = 0
		Per_User = 1
	End Enum

	Public Shared Function GetSetting(ByVal SettingName As String, Optional ByVal Settingmode As enm_Settingmodes = enm_Settingmodes.Same_for_All, Optional ByRef tran As SQLite.SQLiteTransaction = Nothing) As Object
		If Settingmode = enm_Settingmodes.Per_User AndAlso cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0 Then
			SettingName = SettingName & "_User_" & cls_Globals.id_Users.ToString
		End If

		Dim bCloseTran As Boolean = False

		If tran Is Nothing Then
			tran = cls_Globals.Conn.BeginTransaction
			bCloseTran = True
		End If

		Dim oRes As Object = DataAccess.GetSetting(tran.Connection, 0, SettingName, tran)

		If bCloseTran Then
			Try
				tran.Dispose()
			Catch ex As Exception

			End Try
		End If
		Return oRes
	End Function

	Public Shared Sub SetSetting(ByVal SettingName As String, ByVal SettingValue As Object, Optional ByVal Settingmode As enm_Settingmodes = enm_Settingmodes.Same_for_All, Optional ByRef tran As SQLite.SQLiteTransaction = Nothing)
		If Settingmode = enm_Settingmodes.Per_User AndAlso cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0 Then
			SettingName = SettingName & "_User_" & cls_Globals.id_Users.ToString
		End If

		Dim bCloseTran As Boolean = False

		If tran Is Nothing Then
			tran = cls_Globals.Conn.BeginTransaction
			bCloseTran = True
		End If

		DataAccess.SetSetting(tran.Connection, 0, SettingName, SettingValue, tran)

		If bCloseTran Then
			tran.Commit()
			tran.Dispose()
		End If
	End Sub

	''' <summary>
	''' Apply the Settings to the current runtime instance
	''' </summary>
	''' <remarks></remarks>
	Public Shared Sub Apply_Settings()
		cls_Skins.LoadSkin(TC.NZ(cls_Settings.GetSetting("Skin", enm_Settingmodes.Per_User), 4))
		cls_Fonts.ApplyFont(TC.NZ(cls_Settings.GetSetting("Font", enm_Settingmodes.Per_User), "Segoe UI"), TC.NZ(cls_Settings.GetSetting("FontSize", enm_Settingmodes.Per_User), 8))
		cls_Globals.Dir_Extras = Get_Extras_Directory()

		If Alphaleonis.Win32.Filesystem.Directory.Exists(TC.NZ(cls_Settings.GetSetting("Dir_Screenshot"), "")) Then
			cls_Globals.Dir_Screenshot = TC.NZ(cls_Settings.GetSetting("Dir_Screenshot"), "")
		End If

		cls_Globals.RetroAchievements_User = TC.NZ(cls_Settings.GetSetting("RetroAchievements_User", cls_Settings.enm_Settingmodes.Per_User), "")
		cls_Globals.RetroAchievements_Pass = TC.NZ(cls_Settings.GetSetting("RetroAchievements_Pass", cls_Settings.enm_Settingmodes.Per_User), "")

		MKNetLib.cls_MKFileSupport.TempDirRoot = cls_Globals.TempDir(Nothing)
	End Sub

	Public Shared Function Get_Extras_Directory()
		Dim dir_extras As String = TC.NZ(cls_Settings.GetSetting("Dir_Extras"), "")
		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dir_extras) Then
			dir_extras = Application.StartupPath & "\extras"

			If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dir_extras) Then
				dir_extras = ""
			End If
		End If

		Return dir_extras
	End Function

	Public Shared Function Get_DOSBox_CWD(Optional ByRef tran As SQLite.SQLiteTransaction = Nothing) As String
		Dim dosbox_cwd As String = TC.NZ(cls_Settings.GetSetting("Dir_DOSBox_CWD", enm_Settingmodes.Same_for_All, tran), "")

		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dosbox_cwd) Then
			dosbox_cwd = Application.StartupPath & "\dosbox"

			If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dosbox_cwd) Then
				dosbox_cwd = ""
			End If
		End If

		Return dosbox_cwd
	End Function

	Public Shared Function Check_DOSBox_CWD(Optional ByRef tran As SQLite.SQLiteTransaction = Nothing) As Boolean
		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(Get_DOSBox_CWD(tran)) Then
			MKDXHelper.MessageBox("The DOSBox Working Directory cannot be found, please set one up in the Settings section!", "DOSBox Working Directory not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return False
		End If

		Return True
	End Function

	Public Shared Sub Fill_J2K_DS(ByVal DS As DataSet, ByVal j2k_path As String)
		If Alphaleonis.Win32.Filesystem.File.Exists(j2k_path) Then
			If Alphaleonis.Win32.Filesystem.File.Exists(Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(j2k_path) & "\default.j2k") Then
				'DS.Tables.Clear()
				'DS.ReadXmlSchema(Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(j2k_path) & "\default.j2k")
				DS.ReadXml(Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(j2k_path) & "\default.j2k", XmlReadMode.InferSchema)
			ElseIf Alphaleonis.Win32.Filesystem.File.Exists(MKNetLib.cls_MKFileSupport.Get_SpecialFolder(Environment.SpecialFolder.MyDocuments) & "\J2K\" & "default.j2k") Then
				DS.ReadXml(MKNetLib.cls_MKFileSupport.Get_SpecialFolder(Environment.SpecialFolder.MyDocuments) & "\J2K\" & "default.j2k")
			End If
		End If
	End Sub
End Class

Public Class cls_Skins
	''' <summary>
	''' Internal Skin Array, Fields are: DevExpress Skin Name, Displayname, Seasonal Flag
	''' </summary>
	''' <remarks>When adding new skins, always add at the bottom!</remarks>
	Public Shared Skins(,) As String = {
	 {"Blueprint", "Draft - Blue", "0"},
	 {"Metropolis", "Metropolis", "0"},
	 {"Metropolis Dark", "Metropolis Dark", "0"},
	 {"Pumpkin", "Halloween", "1"},
	 {"Sharp Plus", "Sharp Plus", "0"},
	 {"Springtime", "Spring", "1"},
	 {"Summer 2008", "Summer", "1"},
	 {"Valentine", "Valentine's Day", "1"},
	 {"Whiteprint", "Draft - White", "0"},
	 {"Xmas 2008 Blue", "X-Mas", "1"},
	 {"DevExpress Style", "DX", "0"},
	 {"DevExpress Dark Style", "DX Dark", "0"},
	 {"Office 2013", "Flat", "0"},
	 {"Office 2013 Dark Gray", "Flat Gray", "0"},
	 {"Visual Studio 2013 Blue", "Metropolis Blue", "0"},
	 {"Visual Studio 2013 Light", "Metropolis Light Gray", "0"},
	 {"High Contrast", "High Contrast", "0"},
	 {"Sharp", "Sharp", "0"}
	}

	Public Shared Sub LoadSkin(ByVal index As Integer)
		Try
			MKNetDXLib.cls_MKDXSkin.LoadSkin(Skins(index, 0))
		Catch ex As Exception

		End Try
	End Sub

	Public Shared Function GetCurrentSkinname(ByVal tran As SQLite.SQLiteTransaction) As String
		Return Skins(TC.NZ(cls_Settings.GetSetting("Skin", cls_Settings.enm_Settingmodes.Per_User, tran), 4), 0)
	End Function
End Class

Public Class cls_Fonts
	''' <summary>
	''' Apply a Standard Font for all Controls
	''' </summary>
	''' <param name="FontName"></param>
	''' <param name="FontSize"></param>
	''' <remarks></remarks>
	Public Shared Sub ApplyFont(ByVal FontName As String, ByVal FontSize As Integer)
		'DevExpress.Utils.AppearanceObject.DefaultFont = New Font(FontName, FontSize)
		MKNetDXLib.ctl_MKDXGrid.Default_GridFont = New Font(FontName, FontSize)
	End Sub
End Class