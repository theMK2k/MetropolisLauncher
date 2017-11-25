Imports DataAccess = MKNetLib.cls_MKSQLiteDataAccess
Imports DevExpress.XtraBars
Imports System.ComponentModel
Imports DevExpress.XtraGrid.Views.Base

Public Class frm_Rom_Manager
	Private Provide_Merge As Boolean = False  'Provide Merging Functionality - currently not

	Private dict_Have As New Dictionary(Of String, ArrayList) 'Dictionary for duplicate search while adding files
	Private pd_Add As New cls_PermDecision(Me, "Rom match found", "", {New cls_PermDecision.PermDecisionButton("&Replace", Windows.Forms.DialogResult.Yes), New cls_PermDecision.PermDecisionButton("&Add new", Windows.Forms.DialogResult.No), New cls_PermDecision.PermDecisionButton("&Skip", Windows.Forms.DialogResult.Ignore), New cls_PermDecision.PermDecisionButton("&Cancel", Windows.Forms.DialogResult.Cancel)})
	Private pd_ExtrasRename As New cls_PermDecision(Me, "Extras need renaming", "Your alterations affect the filenames of one or more extras (title, snapshots etc.). Do you want to automatically rename these extras?", {New cls_PermDecision.PermDecisionButton("&Yes", Windows.Forms.DialogResult.Yes), New cls_PermDecision.PermDecisionButton("&No", Windows.Forms.DialogResult.No), New cls_PermDecision.PermDecisionButton("&Cancel", Windows.Forms.DialogResult.Cancel)})
	Private pd_Remove_Name As New cls_PermDecision(Me, "Replace Game Name", "When linking a game to a moby games entry usually the moby game's name will be used. Do you want to replace the current game name %0% with %1%?", {New cls_PermDecision.PermDecisionButton("Yes", Windows.Forms.DialogResult.Yes), New cls_PermDecision.PermDecisionButton("No", Windows.Forms.DialogResult.No), New cls_PermDecision.PermDecisionButton("Cancel", Windows.Forms.DialogResult.Cancel)})

	Private merge_row As DataRow = Nothing

	Private bbi_Merge_Select_Caption As String
	Private bbi_Merge_Start_Caption As String

	Private _id_Emu_Games As Object = Nothing
	Private _id_Moby_Platforms As Object = Nothing

	Private _DialogResult As DialogResult = Windows.Forms.DialogResult.Cancel

	''' <summary>
	''' We are in Rescan mode (remove local duplicates and check for DB dupes, also check for missing files and ask for removal if not found
	''' </summary>
	''' <remarks></remarks>
	Private _Rescan As Boolean = False

	Private _dict_DOSBox_Ignore As New Dictionary(Of String, String)

	Private _id_DOSBox_Templates_Default As Integer = 0
	Private _id_ScummVM_Templates_Default As Integer = 0

	Private Function Get_id_DOSBox_Templates_Default() As Integer
		If _id_DOSBox_Templates_Default > 0 Then Return _id_DOSBox_Templates_Default

		_id_DOSBox_Templates_Default = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_DOSBox_Configs FROM main.tbl_DOSBox_Configs CFG INNER JOIN rombase.tbl_Rombase_DOSBox_Configs RBCFG ON CFG.id_Rombase_DOSBox_Configs = RBCFG.id_Rombase_DOSBox_Configs AND RBCFG.isDefault = 1"), 0)
		Return _id_DOSBox_Templates_Default
	End Function

	Private Function Get_id_ScummVM_Templates_Default() As Integer
		If _id_ScummVM_Templates_Default > 0 Then Return _id_ScummVM_Templates_Default

		_id_ScummVM_Templates_Default = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_ScummVM_Configs FROM main.tbl_ScummVM_Configs CFG INNER JOIN rombase.tbl_Rombase_ScummVM_Configs RBCFG ON CFG.id_Rombase_ScummVM_Configs = RBCFG.id_Rombase_ScummVM_Configs AND RBCFG.isDefault = 1"), 0)
		Return _id_ScummVM_Templates_Default
	End Function

	Public Sub New(Optional ByVal id_Emu_Games As Object = Nothing, Optional ByVal id_Moby_Platforms As Object = Nothing)
		InitializeComponent()

		Me.tcl_MV.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False

		barmng.SetPopupContextMenu(grd_Emu_Games, popmnu_Rom_Manager)
		barmng.SetPopupContextMenu(grd_DOSBox_Files_and_Folders, popmnu_DOSBox_Files_and_Folders)
		barmng.SetPopupContextMenu(grd_Moby_Releases, popmnu_Moby_Games)

		spltpnl_Right.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2

		frm_Tag_Parser_Edit.Fill_Tag_Parser_Volumes(Me.DS_ML.ttb_Tag_Parser_Volumes)

		bbi_Merge_Select_Caption = bbi_Merge_Select.Caption
		bbi_Merge_Start_Caption = bbi_Merge_Start.Caption

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Ensure_Moby_Platform_Caches(tran)
			tran.Commit()
		End Using

		Refill_cmb_Platforms()
		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT id_Rombase_DOSBox_Filetypes, Displayname, ID FROM rombase.tbl_Rombase_DOSBox_Filetypes", BTA_DOSBox_Filetypes.DS.Tables(0))
		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT id_Rombase_DOSBox_Exe_Types, Displayname, ID FROM rombase.tbl_Rombase_DOSBox_Exe_Types", BTA_DOSBox_Exe_Types.DS.Tables(0))
		For i As Integer = 0 To 25
			Dim row As DataRow = BTA_DOSBox_Mount_Destination.DS.Tables(0).NewRow
			row("value") = Chr(Asc("A") + i)
			row("Displayname") = Chr(Asc("A") + i)
			BTA_DOSBox_Mount_Destination.DS.Tables(0).Rows.Add(row)
		Next

		Load_dict_DOSBox_Ignore()

		_id_Emu_Games = id_Emu_Games
		_id_Moby_Platforms = id_Moby_Platforms
	End Sub

	Private Sub Refill_cmb_Platforms(Optional ByRef tran As SQLite.SQLiteTransaction = Nothing)
		Dim bTran As Boolean = False

		If tran IsNot Nothing Then
			bTran = True
		End If

		If Not bTran Then
			tran = cls_Globals.Conn.BeginTransaction
		End If

		Me.DS_ML.tbl_Tag_Parser.Clear()

		Dim sSQL As String = ""
		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= "	PLTFM.id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "	, PLTFM.Display_Name" & ControlChars.CrLf
		sSQL &= "		||" & ControlChars.CrLf
		sSQL &= "		' ('" & ControlChars.CrLf
		sSQL &= "		|| (" & ControlChars.CrLf
		sSQL &= "			SELECT COUNT(1)" & ControlChars.CrLf
		sSQL &= "			FROM tbl_Emu_Games EG" & ControlChars.CrLf
		sSQL &= "			WHERE EG.id_Moby_Platforms = PLTFM.id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "			AND EG.id_Emu_Games_Owner IS NULL" & ControlChars.CrLf
		sSQL &= "		)" & ControlChars.CrLf
		sSQL &= "		|| ')' AS Display_Name" & ControlChars.CrLf
		sSQL &= "	, PLTFM.URLPart" & ControlChars.CrLf
		sSQL &= "	, PLTFM.MultiVolume" & ControlChars.CrLf
		sSQL &= "FROM moby.tbl_Moby_Platforms PLTFM" & ControlChars.CrLf
		sSQL &= "LEFT JOIN main.tbl_Moby_Platforms_Settings PLTFMS ON PLTFM.id_Moby_Platforms = PLTFMS.id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "WHERE (PLTFM.id_Moby_Platforms > 0 OR PLTFM.id_Moby_Platforms = -3)" & ControlChars.CrLf
		sSQL &= "			AND PLTFM.Visible = 1" & ControlChars.CrLf
		sSQL &= "			AND id_Moby_Platforms_Owner IS NULL" & ControlChars.CrLf
		sSQL &= "			AND (PLTFMS.Visible IS NULL OR PLTFMS.Visible = 1)" & ControlChars.CrLf
		sSQL &= "ORDER BY PLTFM.Display_Name" & ControlChars.CrLf

		DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, DS_MobyDB.tbl_Moby_Platforms, tran)

		If Not bTran Then
			Try
				tran.Rollback()
			Catch ex As Exception

			End Try
		End If
	End Sub

	Private Sub Load_dict_DOSBox_Ignore()
		Dim dt As DataTable = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT id_Rombase_DOSBox_Ignore, Name, Extension, CRC32 FROM rombase.tbl_Rombase_DOSBox_Ignore")
		For Each row As DataRow In dt.Rows
			_dict_DOSBox_Ignore.Add(row("Name") & "+" & IIf(TC.NZ(row("Extension"), "").Length > 0, row("Extension"), "*") & "+" & IIf(TC.NZ(row("CRC32"), "").Length > 0, row("CRC32"), "*"), "")
		Next

	End Sub

	Private Function DOSBox_Ignore(ByVal Filename As String, ByVal CRC32 As String, ByVal Size As Long) As Boolean
		Try
			If Size = 0 AndAlso CRC32 = "00000000" Then Return True

			Dim Name As String = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(Filename).ToLower
			Dim Extension As String = Alphaleonis.Win32.Filesystem.Path.GetExtension(Filename).Replace(".", "").ToLower
			CRC32 = CRC32.ToLower

			If _dict_DOSBox_Ignore.ContainsKey(Name & "+" & Extension & "+" & CRC32) OrElse _dict_DOSBox_Ignore.ContainsKey(Name & "+" & Extension & "+*") OrElse _dict_DOSBox_Ignore.ContainsKey(Name & "+" & "+*" & "+*") Then
				Return True
			End If

			Return False
		Catch ex As Exception
			Return False
		End Try
	End Function

	Private Sub frm_Rom_Manager_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
		If TC.NZ(_id_Emu_Games, 0) > 0 Then
			Dim id_Moby_Platforms As Int64 = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_Moby_Platforms FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games)), 0)
			cmb_Platform.EditValue = id_Moby_Platforms
			grd_Emu_Games.ForceInitialize()
			Me.BS_Emu_Games.Position = Me.BS_Emu_Games.Find("id_Emu_Games", _id_Emu_Games)
			Me.gv_Emu_Games.ClearSelection()
			Me.gv_Emu_Games.SelectRow(Me.gv_Emu_Games.FocusedRowHandle)

		ElseIf TC.NZ(_id_Moby_Platforms, 0) > 0 Then
			cmb_Platform.EditValue = CLng(_id_Moby_Platforms)
			grd_Emu_Games.ForceInitialize()
		End If

		Me.gv_MV.ExpandAllGroups()
	End Sub

	Private Function Set_Moby_Link(ByRef row_Emu_Games As DataRow, ByRef row_Moby_Releases As DataRow) As DialogResult
		If row_Emu_Games("Name") IsNot DBNull.Value Then

			Dim res As DialogResult = DialogResult.OK

			If cmb_Platform.EditValue <> cls_Globals.enm_Moby_Platforms.scummvm Then
				res = pd_Remove_Name.Show("Replace Game Name", "When linking a game to a Mobygames entry usually the Mobygames' name and publisher info (and much more meta data) will be used. Do you want to replace the game name '" & row_Emu_Games("Name") & "' with '" & row_Moby_Releases("Gamename") & "' as well as the publisher info?")
				If res = Windows.Forms.DialogResult.Yes Then
					row_Emu_Games("Name") = DBNull.Value
					row_Emu_Games("Publisher") = DBNull.Value
				End If
			End If

			If res <> Windows.Forms.DialogResult.Cancel Then
				row_Emu_Games("Moby_Games_URLPart") = row_Moby_Releases("Moby_Games_URLPart").ToString.Replace("\", "")
				row_Emu_Games("deprecated") = row_Moby_Releases("deprecated")

				If Me.cmb_Platform.EditValue = cls_Globals.enm_Moby_Platforms.scummvm Then
					row_Emu_Games("id_Moby_Platforms_Alternative") = row_Moby_Releases("id_Moby_Platforms")
				End If

				Update_Children(row_Emu_Games)
			End If

			Return res
		Else
			row_Emu_Games("Moby_Games_URLPart") = row_Moby_Releases("Moby_Games_URLPart").ToString.Replace("\", "")
			row_Emu_Games("deprecated") = row_Moby_Releases("deprecated")

			If Me.cmb_Platform.EditValue = cls_Globals.enm_Moby_Platforms.scummvm Then
				row_Emu_Games("id_Moby_Platforms_Alternative") = row_Moby_Releases("id_Moby_Platforms")
			End If

			Update_Children(row_Emu_Games)

			Return Windows.Forms.DialogResult.Yes
		End If
	End Function

	Private Sub grd_Moby_Releases_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd_Moby_Releases.DoubleClick
		bbi_SetLink_Click(bbi_SetLink, New System.EventArgs)
	End Sub

	Private Sub grd_Emu_Games_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd_Emu_Games.DoubleClick
		Dim e_mouse As DevExpress.Utils.DXMouseEventArgs = e
		Dim hitinfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = gv_Emu_Games.CalcHitInfo(e_mouse.Location)
		If Not hitinfo.InRow Then
			Return
		End If

		If BS_Emu_Games.Current IsNot Nothing AndAlso BS_Moby_Releases.Current IsNot Nothing Then
			Set_Moby_Link(BS_Emu_Games.Current.Row, BS_Moby_Releases.Current.Row)
		End If

		gv_Emu_Games.RefreshData()
	End Sub

	Private Sub Refill(ByVal Platform As Object)
		Cursor.Current = Cursors.WaitCursor

		merge_row = Nothing

		Me.DS_MobyDB.src_Moby_Releases.Clear()
		DS_ML.tbl_Emu_Games.Clear()
		DS_ML.tbl_Emu_Games_Languages.Clear()
		DS_ML.tbl_Emu_Games_Regions.Clear()

		If Not TC.IsNullNothingOrEmpty(Platform) Then

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				DS_ML.Fill_src_frm_Rom_Manager_Moby_Releases(tran, Me.DS_MobyDB.src_Moby_Releases, Platform)
				DS_ML.Fill_src_frm_Rom_Manager_Emu_Games(tran, DS_ML.tbl_Emu_Games, Platform)

				DS_ML.tbl_Emu_Games_Languages.Clear()
				DS_ML.Fill_tbl_Emu_Games_Languages(tran, DS_ML.tbl_Emu_Games_Languages, Platform)
				DS_ML.tbl_Emu_Games_Regions.Clear()
				DS_ML.Fill_tbl_Emu_Games_Regions(tran, DS_ML.tbl_Emu_Games_Regions, Platform)

				bbi_Add_DOSBox_Game_Directory.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
				bbi_Add_DOSBox_Game_Media.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

				bbi_AddGames.Visibility = BarItemVisibility.Always

				If TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT MultiVolume FROM moby.tbl_Moby_Platforms WHERE id_Moby_Platforms = " & TC.getSQLFormat(Platform), tran), False) Then
					spltpnl_Right.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both

					'pnl_Discs_Volumes.Visible = True
					'pnl_DOSBox_Files_and_Folders.Visible = False

					tpg_Discs_Volumes.PageVisible = True
					tpg_DOSBox_Files_Directories.PageVisible = False
					tcl_MV.SelectedTabPage = tpg_Discs_Volumes
				Else
					If {cls_Globals.enm_Moby_Platforms.dos, cls_Globals.enm_Moby_Platforms.pcboot}.Contains(Platform) Then
						'DOS and PC Booter
						spltpnl_Right.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both

						'pnl_Discs_Volumes.Visible = False
						'pnl_DOSBox_Files_and_Folders.Visible = True

						tpg_Discs_Volumes.PageVisible = False
						tpg_DOSBox_Files_Directories.PageVisible = True
						tcl_MV.SelectedTabPage = tpg_Discs_Volumes

						bbi_Add_DOSBox_Game_Directory.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
						bbi_Add_DOSBox_Game_Media.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
					ElseIf {cls_Globals.enm_Moby_Platforms.scummvm}.Contains(Platform) Then
						'ScummVM
						bbi_AddGames.Visibility = BarItemVisibility.Never

						spltpnl_Right.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
						BS_MV.Filter = "id_Emu_Games = 0"
						BS_DOSBox_Files_and_Folders.Filter = "id_Emu_Games = 0"
					Else
						'Other Platforms
						spltpnl_Right.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
						BS_MV.Filter = "id_Emu_Games = 0"
						BS_DOSBox_Files_and_Folders.Filter = "id_Emu_Games = 0"
					End If
				End If

				tran.Commit()
			End Using

			gv_Emu_Games.ExpandAllGroups()
			gv_MV.ExpandAllGroups()
			gv_Moby_Releases.ExpandAllGroups()
		Else
			spltpnl_Right.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
			BS_MV.Filter = "id_Emu_Games = 0"
			BS_DOSBox_Files_and_Folders.Filter = "id_Emu_Games = 0"
		End If

		Cursor.Current = Cursors.Default
	End Sub

	Private Sub cmb_Platform_EditValueChanging(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles cmb_Platform.EditValueChanging
		'Check if something has to be saved
		If Save(True) = DialogResult.Cancel Then
			e.Cancel = True
			Return
		End If

		Refill(e.NewValue)
	End Sub

	Private Sub BS_Rombase_CurrentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BS_Rombase.CurrentChanged
		If BS_Rombase.Current Is Nothing Then Return

		If Not TC.IsNullNothingOrEmpty(BS_Rombase.Current("id_Moby_Releases")) Then
			Dim iNewPos As Integer = BS_Moby_Releases.Find("id_Moby_Releases", BS_Rombase.Current("id_Moby_Releases"))
			If iNewPos > 0 Then
				BS_Moby_Releases.Position = iNewPos
			End If
		Else
			Dim iNewPos As Integer = BS_Moby_Releases.Find("Soundex", BS_Rombase.Current("Soundex"))
			If iNewPos > 0 Then
				BS_Moby_Releases.Position = iNewPos
			End If
		End If

	End Sub

	Private Function Get_Allowed_Extensions(ByVal id_Moby_Platforms As Integer) As ArrayList
		Dim al_Allowed_Extensions As New ArrayList

		Select Case id_Moby_Platforms
			Case cls_Globals.enm_Moby_Platforms.dos
				For Each ext As String In "zip;7z;rar".Split(";")
					If Not TC.IsNullNothingOrEmpty(ext) Then
						al_Allowed_Extensions.Add(ext)
					End If
				Next
			Case cls_Globals.enm_Moby_Platforms.win
				For Each ext As String In "exe;bat;cmd;lnk".Split(";")
					If Not TC.IsNullNothingOrEmpty(ext) Then
						al_Allowed_Extensions.Add(ext)
					End If
				Next
			Case cls_Globals.enm_Moby_Platforms.ps1, cls_Globals.enm_Moby_Platforms.ps2, cls_Globals.enm_Moby_Platforms.psp, cls_Globals.enm_Moby_Platforms.scd, cls_Globals.enm_Moby_Platforms.cd32, cls_Globals.enm_Moby_Platforms.sat
				For Each ext As String In "cue;iso".Split(";")
					If Not TC.IsNullNothingOrEmpty(ext) Then
						al_Allowed_Extensions.Add(ext)
					End If
				Next
			Case cls_Globals.enm_Moby_Platforms.gc, cls_Globals.enm_Moby_Platforms.wii
				For Each ext As String In "iso;wbfs;ciso".Split(";")
					If Not TC.IsNullNothingOrEmpty(ext) Then
						al_Allowed_Extensions.Add(ext)
					End If
				Next
			Case cls_Globals.enm_Moby_Platforms.dc
				For Each ext As String In "cdi;gdi".Split(";")
					If Not TC.IsNullNothingOrEmpty(ext) Then
						al_Allowed_Extensions.Add(ext)
					End If
				Next
		End Select

		Return al_Allowed_Extensions
	End Function

	Private dict_Rombase_crc As Dictionary(Of String, ArrayList)
	Private dict_Rombase_md5 As Dictionary(Of String, ArrayList)
	Private dict_Rombase_sha1 As Dictionary(Of String, ArrayList)
	Private dict_Rombase_CustomIdentifier As Dictionary(Of String, ArrayList)

	Private Sub Prepare_dict_Rombase()
		Dim dt As DataTable = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT id_rombase, id_rombase_owner, filename, size, crc, md5, sha1, CustomIdentifier, id_Moby_Platforms FROM tbl_Rombase")

		dict_Rombase_crc = New Dictionary(Of String, ArrayList)
		dict_Rombase_md5 = New Dictionary(Of String, ArrayList)
		dict_Rombase_sha1 = New Dictionary(Of String, ArrayList)
		dict_Rombase_CustomIdentifier = New Dictionary(Of String, ArrayList)

		For Each row_rombase As DataRow In dt.Rows
			Dim size As String = TC.NZ(row_rombase("size"), "")
			Dim crc As String = TC.NZ(row_rombase("crc"), "")
			Dim md5 As String = TC.NZ(row_rombase("md5"), "")
			Dim sha1 As String = TC.NZ(row_rombase("sha1"), "")
			Dim CustomIdentifier As String = TC.NZ(row_rombase("CustomIdentifier"), "")
			Dim id_Moby_Platforms As Integer = TC.NZ(row_rombase("id_Moby_Platforms"), 0)

			If crc.Length > 0 Then
				If dict_Rombase_crc.ContainsKey(size & ";" & crc) Then
					dict_Rombase_crc(size & ";" & crc).Add(row_rombase)
				Else
					Dim al As New ArrayList
					al.Add(row_rombase)
					dict_Rombase_crc.Add(size & ";" & crc, al)
				End If
			End If

			If md5.Length > 0 Then
				If dict_Rombase_md5.ContainsKey(size & ";" & md5) Then
					dict_Rombase_md5(size & ";" & md5).Add(row_rombase)
				Else
					Dim al As New ArrayList
					al.Add(row_rombase)
					dict_Rombase_md5.Add(size & ";" & md5, al)
				End If
			End If

			If sha1.Length > 0 Then
				If dict_Rombase_sha1.ContainsKey(size & ";" & sha1) Then
					dict_Rombase_sha1(size & ";" & sha1).Add(row_rombase)
				Else
					Dim al As New ArrayList
					al.Add(row_rombase)
					dict_Rombase_sha1.Add(size & ";" & sha1, al)
				End If
			End If

			If CustomIdentifier.Length > 0 AndAlso (id_Moby_Platforms > 0 OrElse id_Moby_Platforms = cls_Globals.enm_Moby_Platforms.scummvm) Then
				If dict_Rombase_CustomIdentifier.ContainsKey(id_Moby_Platforms & ";" & CustomIdentifier) Then
					dict_Rombase_CustomIdentifier(id_Moby_Platforms & ";" & CustomIdentifier).Add(row_rombase)
				Else
					Dim al As New ArrayList
					al.Add(row_rombase)
					dict_Rombase_CustomIdentifier.Add(id_Moby_Platforms & ";" & CustomIdentifier, al)
				End If
			End If
		Next
	End Sub

	Private Sub Clear_dict_Rombase()
		If dict_Rombase_crc IsNot Nothing Then
			dict_Rombase_crc.Clear()
			dict_Rombase_crc = Nothing
		End If

		If dict_Rombase_md5 IsNot Nothing Then
			dict_Rombase_md5.Clear()
			dict_Rombase_md5 = Nothing
		End If

		If dict_Rombase_sha1 IsNot Nothing Then
			dict_Rombase_sha1.Clear()
			dict_Rombase_sha1 = Nothing
		End If

		If dict_Rombase_CustomIdentifier IsNot Nothing Then
			dict_Rombase_CustomIdentifier.Clear()
			dict_Rombase_CustomIdentifier = Nothing
		End If
	End Sub

	Private Function Get_id_Rombase(ByRef tran As SQLite.SQLiteTransaction, ByVal file As String, ByVal size As Long, ByVal crc As String, ByVal md5 As String, ByVal sha1 As String, ByVal id_Moby_Platforms As Long, Optional ByVal CustomIdentifier As Object = Nothing) As Long
		'in all occasions check against size too!
		If dict_Rombase_crc IsNot Nothing Then
			Dim al_Row_Rombase As New ArrayList

			If (id_Moby_Platforms > 0 OrElse id_Moby_Platforms = cls_Globals.enm_Moby_Platforms.scummvm) AndAlso TC.NZ(CustomIdentifier, "").Length > 0 Then
				If dict_Rombase_CustomIdentifier.ContainsKey(id_Moby_Platforms & ";" & CustomIdentifier) Then
					al_Row_Rombase = dict_Rombase_CustomIdentifier(id_Moby_Platforms & ";" & CustomIdentifier)
					If al_Row_Rombase.Count = 1 Then
						Return al_Row_Rombase(0)("id_rombase")
					End If
				End If
			End If

			If md5.Length > 0 Then
				If dict_Rombase_md5.ContainsKey(size & ";" & md5) Then
					al_Row_Rombase = dict_Rombase_md5(size & ";" & md5)
					If al_Row_Rombase.Count = 1 Then
						'Return al_Row_Rombase(0)
						Return al_Row_Rombase(0)("id_rombase")
					End If
				End If
			End If

			If sha1.Length > 0 Then
				If dict_Rombase_sha1.ContainsKey(size & ";" & sha1) Then
					Dim al_sha1 As ArrayList = dict_Rombase_sha1(size & ";" & sha1)
					If al_sha1.Count = 1 Then
						'Return al_sha1(0)
						Return al_sha1(0)("id_rombase")
					End If

					If al_sha1.Count > 1 AndAlso al_Row_Rombase.Count > 1 Then
						Dim al_Remove As New ArrayList
						For Each row_Rombase As DataRow In al_Row_Rombase
							If Not al_sha1.Contains(row_Rombase) Then
								al_Remove.Add(row_Rombase)
							End If
						Next

						For Each row_Rombase As Long In al_Remove
							al_Row_Rombase.Remove(row_Rombase)
						Next

						If al_Row_Rombase.Count = 1 Then
							Return al_Row_Rombase(0)
						End If
					End If
				End If
			End If

			'Here we only have crc
			If crc.Length > 0 Then
				If dict_Rombase_crc.ContainsKey(size & ";" & crc) Then
					If dict_Rombase_crc(size & ";" & crc).Count = 1 Then
						Return dict_Rombase_crc(size & ";" & crc)(0)("id_rombase")
					End If
				End If
			End If

			Return 0L
		Else
			Return DS_Rombase.Select_id_Rombase(tran, DBNull.Value, file, size, crc, md5, sha1, id_Moby_Platforms, Nothing, CustomIdentifier)
		End If
	End Function

	''' <summary>
	''' Check if a file is an archive, but revoke archives that are accepted by emulators e.g. WHDLoad packs for FS-UAE
	''' </summary>
	''' <param name="Filename"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Shared Function Is_Archive(ByVal Filename As String) As Boolean
		'Only accept certain extensions
		If Not {"rar", "zip", "tar", "gz", "7z"}.Contains(Alphaleonis.Win32.Filesystem.Path.GetExtension(Filename).ToLower.Replace(".", "")) Then
			Return False
		End If

		Dim fl As String = Filename.ToLower

		'Don't handle WHDLoad packs as archives
		If fl.Contains("[whd") OrElse fl.Contains("whd]") OrElse fl.Contains("whd#") _
			 OrElse fl.Contains("[otr") OrElse fl.Contains("otr]") OrElse fl.Contains("otr#") _
			 OrElse fl.Contains("[jst") OrElse fl.Contains("jst]") OrElse fl.Contains("jst#") Then
			Return False
		End If

		Try
			'Try to open with our Compressor, if it works, it is indeed an archive
			Dim archive As SharpCompress.Archive.IArchive = Nothing
			'Dim fi As New Alphaleonis.Win32.Filesystem.FileInfo(Filename)
			archive = SharpCompress.Archive.ArchiveFactory.Open(Filename)

			Return True
		Catch ex As Exception
			Return False
		End Try
	End Function

	Private Sub bbi_AddGamesFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_AddGamesFolder.ItemClick
		If BS_Moby_Platforms.Current Is Nothing Then Return

		If BS_Moby_Platforms.Current("id_Moby_Platforms") = cls_Globals.enm_Moby_Platforms.dos Then
			Add_DOSBox_Games(enm_DOSBoxAdd_Mode.Packed_Files_in_Directory)
			Return
		End If

		If BS_Moby_Platforms.Current("id_Moby_Platforms") = cls_Globals.enm_Moby_Platforms.scummvm Then
			Add_ScummVM_Games()
			Return
		End If

		pd_Add.ApplyAll = False

		merge_row = Nothing

		Dim al_Allowed_Extensions As ArrayList = Get_Allowed_Extensions(TC.NZ(cmb_Platform.EditValue, 0))

		Dim sFolder As String = MKNetLib.cls_MKFileSupport.OpenFolderDialog(TC.NZ(cls_Settings.GetSetting("Browse_Romfolder" & "_" & BS_Moby_Platforms.Current("URLPart")), ""))

		If Alphaleonis.Win32.Filesystem.Directory.Exists(sFolder) Then
			cls_Settings.SetSetting("Browse_Romfolder" & "_" & BS_Moby_Platforms.Current("URLPart"), sFolder)

			Using frm As New frm_Tag_Parser_Edit(Nothing, sFolder, al_Allowed_Extensions, TC.NZ(Me.BS_Moby_Platforms.Current("MultiVolume"), False))
				If frm.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then
					Return
				End If
			End Using

			Dim result As New cls_AddGameStats()

			Dim arrFiles As New ArrayList
			Dim fsrch As New MKNetLib.cls_MKFileSearch(New Alphaleonis.Win32.Filesystem.DirectoryInfo(sFolder))
			fsrch.Search(New Alphaleonis.Win32.Filesystem.DirectoryInfo(sFolder), "*.*")
			arrFiles.AddRange(fsrch.Files)

			If arrFiles.Count > 10 Then
				Prepare_dict_Rombase()
			End If

			Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Nothing), 400, 60, ProgressBarStyle.Blocks, False, "Importing file {0} of {1}", 0, arrFiles.Count, False)
			prg.Start()

			PrepareDictHave()

			Dim Aborted As Boolean = False

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				For Each fi As Alphaleonis.Win32.Filesystem.FileInfo In arrFiles
					prg.IncreaseCurrentValue()

					If {".sfv", ".txt", ".nfo"}.Contains(fi.Extension.ToLower) Then Continue For 'Skip certain files

					Dim archive As SharpCompress.Archive.IArchive = Nothing

					Try
						If Is_Archive(fi.FullName) Then
							archive = SharpCompress.Archive.ArchiveFactory.Open(fi.FullName)
						End If

					Catch ex As Exception

					End Try

					If archive IsNot Nothing Then
						Dim sTmpDir As String = MKNetLib.cls_MKFileSupport.CreateTempDir("ml_")

						'one or more files in archive - extract and call AddGameFromFile with the extracted fileinfo
						For Each entry As SharpCompress.Archive.IArchiveEntry In archive.Entries
							If Not entry.IsDirectory Then
								Dim sOutFile As String = sTmpDir & "\" & Alphaleonis.Win32.Filesystem.Path.GetFileName(entry.FilePath)
								'If Not Alphaleonis.Win32.Filesystem.File.Exists(sOutFile) Then
								'Using sw As New IO.StreamWriter(sOutFile)
								'	GC.SuppressFinalize(sw.BaseStream)
								'	entry.WriteTo(sw.BaseStream)
								'	sw.Close()

								'	Dim res As cls_3IntVec = AddGameFromFile(tran, New Alphaleonis.Win32.Filesystem.FileInfo(sOutFile), fi, prg, al_Allowed_Extensions)

								'	If res Is Nothing Then
								'		'User cancelled
								'		Aborted = True
								'		Exit For
								'	End If

								'	result.Add(res)
								'End Using
								'End If

								'AddGameFromFile without extracting
								Dim res As cls_AddGameStats = AddGameFromFile(tran, Nothing, fi, prg, al_Allowed_Extensions, entry)
								If res Is Nothing Then
									'User cancelled
									Aborted = True
									Exit For
								End If

								result.Add(res)
							End If
						Next

						MKNetLib.cls_MKFileSupport.Delete_Directory(sTmpDir)

						If Aborted Then
							Exit For
						End If
					Else
						'Not an archive
						Dim res As cls_AddGameStats = AddGameFromFile(tran, fi, fi, prg, al_Allowed_Extensions)

						If res Is Nothing Then
							'User cancelled
							Aborted = True
							Exit For
						End If

						result.Add(res)
					End If
				Next

				tran.Commit()
				prg.Close()
			End Using

			Dim cntMismatch As Integer = Me.DS_ML.tbl_Emu_Games.Select("ROMBASE_id_Moby_Platforms IS NOT NULL AND id_Moby_Platforms <> ROMBASE_id_Moby_Platforms").Length

			If TC.NZ(Me.BS_Moby_Platforms.Current("MultiVolume"), False) Then
				Group_Volumes()
			End If

			Dim sResult As String = "Result" & IIf(Aborted, "after cancellation", "") & ": " & ControlChars.CrLf & ControlChars.CrLf & result._new & " new games added" & ControlChars.CrLf & result._links & " links to MobyGames meta data applied" & ControlChars.CrLf & result._duplicates_added & " added duplicates" & ControlChars.CrLf & result._duplicates_replaced & " replaced duplicates" & ControlChars.CrLf & result._duplicates_ignored & " ignored duplicates"

			If cntMismatch > 0 Then
				sResult &= ControlChars.CrLf & ControlChars.CrLf & "WARNING: There have been " & cntMismatch & " platform mismatch/es detected! All affected entries are in red color. Did you import Roms for the correct Platform?"
			End If

			Clear_dict_Rombase()

			MKDXHelper.MessageBox(sResult, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
		End If
	End Sub

	Private Sub PrepareDictHave()
		dict_Have.Clear()

		For Each row As DataRow In DS_ML.tbl_Emu_Games.Rows
			Dim pathID As String = (row("Folder") & "\" & row("File") & TC.NZ(row("InnerFile"), "<null>")).ToString.ToLower
			If row.RowState <> DataRowState.Deleted AndAlso row.RowState <> DataRowState.Detached Then
				If Not dict_Have.ContainsKey(pathID) Then
					dict_Have.Add(pathID, New ArrayList({row}))
				End If
			End If
		Next
	End Sub

	Private dict_Group_Volumes_Filtered_Names As Dictionary(Of String, ArrayList) = Nothing

	Private Sub Group_Volumes(ByVal Filtered_Name As String)
		Dim rows_emugames As ArrayList = dict_Group_Volumes_Filtered_Names(Filtered_Name)

		Dim id_MainEntry As Long = 0

		For Each row_emugames As DataRow In rows_emugames
			If TC.NZ(row_emugames("id_Emu_Games_Owner"), 0) <> 0 Then
				id_MainEntry = row_emugames("id_Emu_Games_Owner")
				Exit For
			End If
		Next

		'No former Main Entry found - get the first with Volume_Number = 1
		If id_MainEntry = 0 Then
			For Each row_emugames As DataRow In rows_emugames
				If TC.NZ(row_emugames("Volume_Number"), 0) = 1 Then
					id_MainEntry = row_emugames("id_Emu_Games")
					Exit For
				End If
			Next
		End If

		'No Main Entry found with Volume_Number = 1 - get the first with Volume_Number = NULL and give it Volume_Number = 1
		If id_MainEntry = 0 Then
			For Each row_emugames As DataRow In rows_emugames
				If TC.NZ(row_emugames("Volume_Number"), 0) = 0 Then
					id_MainEntry = row_emugames("id_Emu_Games")
					row_emugames("Volume_Number") = 1L
					Exit For
				End If
			Next
		End If

		'Still no Main Entry found - just use the first and give it Volume_Number = 1
		If id_MainEntry = 0 Then
			For Each row_emugames As DataRow In rows_emugames
				id_MainEntry = row_emugames("id_Emu_Games")
				row_emugames("Volume_Number") = 1L
				Exit For
			Next
		End If

		For Each row_emugames As DataRow In rows_emugames
			If row_emugames("id_Emu_Games") <> id_MainEntry Then
				row_emugames("id_Emu_Games_Owner") = id_MainEntry
			Else
				row_emugames("id_Emu_Games_Owner") = DBNull.Value
			End If
		Next

		'Maintain unique Volume Numbers
		For Each row_emugames1 As DataRow In rows_emugames
			For Each row_emugames2 As DataRow In rows_emugames
				If row_emugames2("id_Emu_Games") <> id_MainEntry Then
					If TC.NZ(row_emugames1("Volume_Number"), -1) = TC.NZ(row_emugames2("Volume_Number"), -2) Then
						If row_emugames1("id_Emu_Games") <> row_emugames2("id_Emu_Games") Then
							row_emugames2("Volume_Number") = DBNull.Value
						End If
					End If
				End If
			Next
		Next
	End Sub

	Private Sub Update_Children(ByRef row_Emu_Games As DataRow)
		For Each row_Child As DataRow In Me.DS_ML.tbl_Emu_Games.Select("id_Emu_Games_Owner = " & row_Emu_Games("id_Emu_Games"))
			row_Child("Moby_Games_URLPart") = row_Emu_Games("Moby_Games_URLPart")
			If Me.cmb_Platform.EditValue = cls_Globals.enm_Moby_Platforms.scummvm Then
				row_Child("id_Moby_Platforms_Alternative") = row_Emu_Games("id_Moby_Platforms_Alternative")
			End If
		Next
	End Sub

	Private Sub Group_Volumes()
		Dim dict_FilteredNames As New Dictionary(Of String, String) 'List of all processed filtered names

		'TODO: Don't just group only games with id_Emu_Games < 0
		'Group New games to existing games
		'Do existing games bring their filtered name from database? -> YES!
		'Use file extension with the filtered name -> implemented in Tag_Parser_Edit
		Dim rows_emugames_added() As DataRow = Me.DS_ML.tbl_Emu_Games.Select("id_Emu_Games < 0", "Filtered_Name")

		dict_Group_Volumes_Filtered_Names = New Dictionary(Of String, ArrayList)

		For Each row_emugames_added As DataRow In rows_emugames_added
			Dim al_row_emugames As ArrayList = Nothing

			Dim sFiltered_Name As String = row_emugames_added("Filtered_Name")

			If dict_Group_Volumes_Filtered_Names.ContainsKey(sFiltered_Name) Then
				al_row_emugames = dict_Group_Volumes_Filtered_Names(sFiltered_Name)
			Else
				al_row_emugames = New ArrayList
				dict_Group_Volumes_Filtered_Names.Add(sFiltered_Name, al_row_emugames)
			End If

			al_row_emugames.Add(row_emugames_added)
		Next

		'Add old emugames entries to the arraylists
		Dim rows_emugames_old() As DataRow = Me.DS_ML.tbl_Emu_Games.Select("id_Emu_Games > 0", "Filtered_Name")

		For Each item As KeyValuePair(Of String, ArrayList) In dict_Group_Volumes_Filtered_Names
			For Each row_emugames_old As DataRow In rows_emugames_old
				If row_emugames_old("Filtered_Name") = item.Key Then
					item.Value.Add(row_emugames_old)
				End If
			Next
		Next

		Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Nothing), 400, 60, ProgressBarStyle.Blocks, False, "Grouping volumes {0} of {1}", 0, rows_emugames_added.Length, False)
		prg.Start()

		For Each row_emugames As DataRow In rows_emugames_added
			prg.IncreaseCurrentValue()

			Dim sFiltered_Name = row_emugames("Filtered_Name")

			If dict_FilteredNames.ContainsKey(sFiltered_Name) Then Continue For

			Group_Volumes(sFiltered_Name)
			dict_FilteredNames.Add(sFiltered_Name, sFiltered_Name)
		Next

		prg.Close()
	End Sub

	Private Sub bbi_AddGames_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_AddGames.ItemClick
		If BS_Moby_Platforms.Current Is Nothing Then Return
		If BS_Moby_Platforms.Current("id_Moby_Platforms") = cls_Globals.enm_Moby_Platforms.dos Then
			Add_DOSBox_Games()
			Return
		End If

		pd_Add.ApplyAll = False

		merge_row = Nothing

		Dim sFilter As String = "All Files (*.*)|*.*"
		Dim al_Allowed_Extensions As ArrayList = Get_Allowed_Extensions(TC.NZ(cmb_Platform.EditValue, 0))

		Select Case TC.NZ(cmb_Platform.EditValue, 0)
			Case cls_Globals.enm_Moby_Platforms.win
				sFilter = "Executables |*.exe;*.bat;*.cmd;*.lnk"
			Case cls_Globals.enm_Moby_Platforms.ps1, cls_Globals.enm_Moby_Platforms.ps2, cls_Globals.enm_Moby_Platforms.psp, cls_Globals.enm_Moby_Platforms.scd, cls_Globals.enm_Moby_Platforms.cd32, cls_Globals.enm_Moby_Platforms.sat
				sFilter = "CD Images (*.cue;*.iso)|*.cue;*.iso"
			Case cls_Globals.enm_Moby_Platforms.gc, cls_Globals.enm_Moby_Platforms.wii
				sFilter = "Wii/GC Images (*.iso;*.ciso;*.wbfs)|*.iso;*.ciso;*.wbfs"
			Case cls_Globals.enm_Moby_Platforms.dc
				sFilter = "DreamCast Images (*.gdi;*.cdi)|*.gdi;*.cdi"
		End Select

		Dim sFiles As String() = MKNetLib.cls_MKFileSupport.OpenFileDialog("Select Files", sFilter, 0, "", TC.NZ(cls_Settings.GetSetting("Browse_Romfolder" & "_" & BS_Moby_Platforms.Current("URLPart")), ""), True, ParentForm:=Me)

		If sFiles(0).Length = 0 Then Return

		If Alphaleonis.Win32.Filesystem.Directory.Exists(Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(sFiles(0))) Then
			cls_Settings.SetSetting("Browse_Romfolder" & "_" & BS_Moby_Platforms.Current("URLPart"), Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(sFiles(0)))
		End If

		Using frm As New frm_Tag_Parser_Edit(Nothing, sFiles, al_Allowed_Extensions, TC.NZ(Me.BS_Moby_Platforms.Current("MultiVolume"), False))
			If frm.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then
				Return
			End If
		End Using

		If sFiles.Length > 10 Then
			Prepare_dict_Rombase()
		End If

		Dim Aborted As Boolean = False
		Dim result As New cls_AddGameStats()

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(tran), 400, 60, ProgressBarStyle.Blocks, False, "Reading {0} of {1}", 0, sFiles.Length, False)
			prg.Start()

			PrepareDictHave()

			For Each sFile As String In sFiles
				prg.IncreaseCurrentValue()

				If Alphaleonis.Win32.Filesystem.File.Exists(sFile) Then

					If {".sfv", ".txt", ".nfo"}.Contains(Alphaleonis.Win32.Filesystem.Path.GetExtension(sFile).ToLower) Then Continue For 'Skip certain files

					Dim archive As SharpCompress.Archive.IArchive = Nothing

					Try
						If Is_Archive(sFile) Then
							archive = SharpCompress.Archive.ArchiveFactory.Open(sFile)
						End If
					Catch ex As Exception

					End Try

					If archive IsNot Nothing Then
						Dim sTmpDir As String = MKNetLib.cls_MKFileSupport.CreateTempDir("ml_")

						'one or more files in archive - extract and call AddGameFromFile with the extracted fileinfo
						For Each entry As SharpCompress.Archive.IArchiveEntry In archive.Entries
							If Not entry.IsDirectory Then
								'Dim sOutFile As String = sTmpDir & "\" & Alphaleonis.Win32.Filesystem.Path.GetFileName(entry.FilePath)
								'If Not Alphaleonis.Win32.Filesystem.File.Exists(sOutFile) Then
								'	Using sw As New IO.StreamWriter(sOutFile)
								'		GC.SuppressFinalize(sw.BaseStream)
								'		entry.WriteTo(sw.BaseStream)
								'		sw.Close()

								'		Dim res As cls_3IntVec = AddGameFromFile(tran, New Alphaleonis.Win32.Filesystem.FileInfo(sOutFile), New Alphaleonis.Win32.Filesystem.FileInfo(sFile), prg, al_Allowed_Extensions)

								'		If res Is Nothing Then
								'			'User cancelled
								'			Aborted = True
								'			Exit For
								'		End If

								'		result.Add(res)
								'	End Using
								'End If

								'AddGameFromFile without extracting
								Dim res As cls_AddGameStats = AddGameFromFile(tran, Nothing, New Alphaleonis.Win32.Filesystem.FileInfo(sFile), prg, al_Allowed_Extensions, entry)

								If res Is Nothing Then
									'User cancelled
									Aborted = True
									Exit For
								End If

								result.Add(res)

							End If

						Next

						MKNetLib.cls_MKFileSupport.Delete_Directory(sTmpDir)

						If Aborted Then
							Exit For
						End If
					Else
						'Not an archive
						Dim res As cls_AddGameStats = AddGameFromFile(tran, New Alphaleonis.Win32.Filesystem.FileInfo(sFile), New Alphaleonis.Win32.Filesystem.FileInfo(sFile), prg, al_Allowed_Extensions)

						If res Is Nothing Then
							'User cancelled
							Aborted = True
							Exit For
						End If

						result.Add(res)
					End If
				End If
			Next

			tran.Commit()
			prg.Close()
		End Using

		Dim cntMismatch As Integer = Me.DS_ML.tbl_Emu_Games.Select("ROMBASE_id_Moby_Platforms IS NOT NULL AND id_Moby_Platforms <> ROMBASE_id_Moby_Platforms").Length

		If TC.NZ(Me.BS_Moby_Platforms.Current("MultiVolume"), False) Then
			Group_Volumes()
		End If

		Clear_dict_Rombase()

		'Dim sResult As String = "Result" & IIf(Aborted, " after cancellation", "") & ": " & result._new & " games added, " & result._links & " of them were linked to MobyGames meta data, " & result._duplicates_added & " duplicates were newly added and " & result._duplicates_replaced & " duplicates have been replaced."
		Dim sResult As String = "Result" & IIf(Aborted, "after cancellation", "") & ": " & ControlChars.CrLf & ControlChars.CrLf & result._new & " new games added" & ControlChars.CrLf & result._links & " links to MobyGames meta data applied" & ControlChars.CrLf & result._duplicates_added & " added duplicates" & ControlChars.CrLf & result._duplicates_replaced & " replaced duplicates" & ControlChars.CrLf & result._duplicates_ignored & " ignored duplicates"

		If cntMismatch > 0 Then
			sResult &= ControlChars.CrLf & ControlChars.CrLf & "WARNING: There have been " & cntMismatch & " platform mismatch/es detected! All affected entries are in red color. Did you import Roms for the correct Platform?"
		End If

		MKDXHelper.MessageBox(sResult, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
	End Sub

	Public Enum enm_Identification_Methods
		None = 0
		File_Hashes = 1
		PS1_Identifier = 2
		PS2_Identifier = 3
		PSP_Identifier = 4
		SCD_Identifier = 5
		GC_Wii_Identifier = 6
		CD32_Identifier = 7
		DC_Identifier = 8
		SAT_Identifier = 9
	End Enum

	Private Function Get_ISO_CustomIdentifier(ByVal inner_fi As Alphaleonis.Win32.Filesystem.FileInfo, ByVal identification_method As enm_Identification_Methods) As String
		Dim custom_identifier As String = ""

		Try
			Dim iso_file As String = Alphaleonis.Win32.Filesystem.Path.GetFileName(inner_fi.FullName).ToLower
			Dim directory As String = inner_fi.DirectoryName & "\"

			Select Case Alphaleonis.Win32.Filesystem.Path.GetExtension(iso_file)
				Case ".cue"
					Dim sContent As String = MKNetLib.cls_MKFileSupport.GetFileContents(directory & iso_file)
					Dim rx_matches As System.Text.RegularExpressions.MatchCollection = MKNetLib.cls_MKRegex.GetMatches(sContent, "FILE\s""(.*?)""\sBINARY")
					If rx_matches.Count > 0 Then
						iso_file = rx_matches(0).Groups(1).Value
					End If
				Case ".gdi"
					Dim sContent As String = MKNetLib.cls_MKFileSupport.GetFileContents(directory & iso_file)
					For Each sLine As String In sContent.Split(ControlChars.NewLine)
						Dim rx_matches As System.Text.RegularExpressions.MatchCollection = MKNetLib.cls_MKRegex.GetMatches(sLine, "\d+ \d+ \d+ \d+ (.*) \d+")
						If rx_matches.Count > 0 Then
							Dim temp_filename As String = rx_matches(0).Groups(1).Value.Trim
							If Alphaleonis.Win32.Filesystem.File.Exists(directory & temp_filename) Then
								iso_file = rx_matches(0).Groups(1).Value
								Exit For
							End If
						End If
					Next
			End Select

			Using isostream As New IO.FileStream(directory & iso_file, IO.FileMode.Open, IO.FileAccess.Read)
				Dim format As DiscUtils.enm_Format = DiscUtils.Utils.Detect(isostream)

				Dim dirs As String() = {}
				Dim files As String() = {}

				Select Case format
					Case DiscUtils.enm_Format.Iso9660, DiscUtils.enm_Format.Iso9660_Joliet, DiscUtils.enm_Format.Iso9660_Mode2_Form1, DiscUtils.enm_Format.Iso9660_Mode2_Form2, DiscUtils.enm_Format.Iso9660_RockRidge
						Using cdreader As New DiscUtils.Iso9660.CDReader(isostream, False, True, format)
							dirs = cdreader.GetDirectories(cdreader.Root.Name)
							files = cdreader.GetFiles(cdreader.Root.Name)

						End Using
					Case DiscUtils.enm_Format.Udf
						Using udfreader As New DiscUtils.Udf.UdfReader(isostream)
							dirs = udfreader.GetDirectories(udfreader.Root.Name)
							files = udfreader.GetFiles(udfreader.Root.Name)

						End Using
				End Select

				If identification_method = enm_Identification_Methods.SCD_Identifier Then
					isostream.Position = &H190
					Dim myBuf(16 - 1) As Byte
					isostream.Read(myBuf, 0, 16)
					Dim idString As String = System.Text.Encoding.ASCII.GetString(myBuf)

					If MKNetLib.cls_MKRegex.IsMatch(idString, "\d+") Then
						Return MKNetLib.cls_MKRegex.GetMatches(idString, "\d+")(0).Value
					Else
						Return ""
					End If
				End If

				If identification_method = enm_Identification_Methods.GC_Wii_Identifier Then
					If Alphaleonis.Win32.Filesystem.Path.GetExtension(inner_fi.FullName).ToLower = ".iso" Then
						isostream.Position = 0
					ElseIf Alphaleonis.Win32.Filesystem.Path.GetExtension(inner_fi.FullName).ToLower = ".wbfs" Then
						isostream.Position = 512
					ElseIf Alphaleonis.Win32.Filesystem.Path.GetExtension(inner_fi.FullName).ToLower = ".ciso" Then
						isostream.Position = 32768
					End If
					Dim myBuf(6 - 1) As Byte
					isostream.Read(myBuf, 0, 6)
					Dim idString As String = System.Text.Encoding.ASCII.GetString(myBuf).ToUpper

					If MKNetLib.cls_MKRegex.IsMatch(idString, "[A-Z0-9]{6}") Then
						Return MKNetLib.cls_MKRegex.GetMatches(idString, "[A-Z0-9]{6}")(0).Value
					Else
						Return ""
					End If
				End If

				If identification_method = enm_Identification_Methods.CD32_Identifier Then
					'TODO: CD32 Identifier
				End If

				If identification_method = enm_Identification_Methods.DC_Identifier Then
					isostream.Position = &H50
					Dim myBuf(10 - 1) As Byte
					isostream.Read(myBuf, 0, 10)
					Dim idString As String = System.Text.Encoding.ASCII.GetString(myBuf).ToUpper

					idString = idString.Trim.Replace("-", "").Replace(" ", "").Replace("_", "").ToUpper

					If idString.Length > 0 Then
						Return idString
					Else
						Return ""
					End If
				End If

				If identification_method = enm_Identification_Methods.SAT_Identifier Then
					isostream.Position = &H20
					Dim myBuf(10 - 1) As Byte
					isostream.Read(myBuf, 0, 10)
					Dim idString As String = System.Text.Encoding.ASCII.GetString(myBuf).ToUpper

					If idString.Contains("SEGA") Then
						isostream.Position = &H30
						isostream.Read(myBuf, 0, 10)
						idString = System.Text.Encoding.ASCII.GetString(myBuf).ToUpper
					End If

					idString = idString.Trim.Replace("-", "").Replace(" ", "").Replace("_", "").ToUpper

					If idString.Length > 0 Then
						Return idString
					Else
						Return ""
					End If
				End If

				If {enm_Identification_Methods.PS1_Identifier, enm_Identification_Methods.PS2_Identifier}.Contains(identification_method) Then
					For Each file As String In files
						If file.Contains("SYSTEM.CNF") Then
							Dim sContent As String = ""
							Select Case format
								Case DiscUtils.enm_Format.Iso9660, DiscUtils.enm_Format.Iso9660_Joliet, DiscUtils.enm_Format.Iso9660_Mode2_Form1, DiscUtils.enm_Format.Iso9660_Mode2_Form2, DiscUtils.enm_Format.Iso9660_RockRidge
									Using cdreader As New DiscUtils.Iso9660.CDReader(isostream, False, True, format)
										Dim in_stream As DiscUtils.SparseStream = cdreader.OpenFile(file, IO.FileMode.Open, IO.FileAccess.Read, True, DiscUtils.Utils.SectorSize(format), DiscUtils.Utils.SectorOffset(format), DiscUtils.Utils.DataLength(format))
										Dim sr As New System.IO.StreamReader(in_stream)
										sContent = sr.ReadToEnd
									End Using
								Case DiscUtils.enm_Format.Udf
									Using udfreader As New DiscUtils.Udf.UdfReader(isostream)
										Dim in_stream As DiscUtils.SparseStream = udfreader.OpenFile(file, IO.FileMode.Open, IO.FileAccess.Read, True, DiscUtils.Utils.SectorSize(format), DiscUtils.Utils.SectorOffset(format), DiscUtils.Utils.DataLength(format))
										Dim sr As New System.IO.StreamReader(in_stream)
										sContent = sr.ReadToEnd
									End Using
							End Select
							If TC.NZ(sContent, "").Length > 0 Then
								Dim sBootLine As String = ""
								For Each line As String In sContent.Split(ControlChars.Lf)
									'BOOT = cdrom:\SCES_019.22;1
									'       cdrom:SCES_006.99;1
									'       cdrom:\SLES_027.40;1 <- ???
									'       BOOT=cdrom:\SLES_022.38;1
									line = line.ToUpper.Trim
									If line.Contains("BOOT") Then
										sBootLine = MKNetLib.cls_MKRegex.GetMatches(line, "\:(.*?);")(0).Groups(1).Value.Replace(".", "").Replace("_", "").Replace("\", "")
										custom_identifier = sBootLine
										Exit For
									End If
								Next
							End If
						End If

						If custom_identifier.Length > 0 Then Exit For
					Next
				End If

				If identification_method = enm_Identification_Methods.PSP_Identifier Then
					For Each file As String In files
						If file.Contains("UMD_DATA.BIN") Then
							Dim sContent As String = ""
							Select Case format
								Case DiscUtils.enm_Format.Iso9660, DiscUtils.enm_Format.Iso9660_Joliet, DiscUtils.enm_Format.Iso9660_Mode2_Form1, DiscUtils.enm_Format.Iso9660_Mode2_Form2, DiscUtils.enm_Format.Iso9660_RockRidge
									Using cdreader As New DiscUtils.Iso9660.CDReader(isostream, False, True, format)
										Dim in_stream As DiscUtils.SparseStream = cdreader.OpenFile(file, IO.FileMode.Open, IO.FileAccess.Read, True, DiscUtils.Utils.SectorSize(format), DiscUtils.Utils.SectorOffset(format), DiscUtils.Utils.DataLength(format))
										Dim sr As New System.IO.StreamReader(in_stream)
										sContent = sr.ReadToEnd
									End Using
								Case DiscUtils.enm_Format.Udf
									Using udfreader As New DiscUtils.Udf.UdfReader(isostream)
										Dim in_stream As DiscUtils.SparseStream = udfreader.OpenFile(file, IO.FileMode.Open, IO.FileAccess.Read, True, DiscUtils.Utils.SectorSize(format), DiscUtils.Utils.SectorOffset(format), DiscUtils.Utils.DataLength(format))
										Dim sr As New System.IO.StreamReader(in_stream)
										sContent = sr.ReadToEnd
									End Using
							End Select
							If TC.NZ(sContent, "").Length > 0 Then
								Dim sBootLine As String = ""
								For Each line As String In sContent.Split(ControlChars.Lf)
									'ULES-00640|1169702CE9CF507E|0001|G             |
									line = line.ToUpper.Trim
									custom_identifier = line.Split("|")(0).Replace("-", "")
								Next
							End If
						End If

						If custom_identifier.Length > 0 Then Exit For
					Next
				End If
			End Using
		Catch ex As Exception

		End Try

		Return custom_identifier
	End Function

	''' <summary>
	''' Add a Game from file to DS_ML.tbl_Emu_Games
	''' Find a mapping to tbl_Moby_Releases
	''' Don't add duplicates
	''' </summary>
	''' <param name="tran"></param>
	''' <param name="inner_fi">FileInfo of the inner File (if it exists), else it's just the FileInfo of the main File</param>
	''' <param name="main_fi">FileInfo of the main File (this can be an archive but also a single rom)</param>
	''' <returns>cls_3IntVec with a statistical result (x: Added, y: Mapping found, z: Duplicate)</returns>
	''' <remarks></remarks>
	Private Function AddGameFromFile(ByRef tran As SQLite.SQLiteTransaction, ByRef inner_fi As Alphaleonis.Win32.Filesystem.FileInfo, ByRef main_fi As Alphaleonis.Win32.Filesystem.FileInfo, Optional ByVal prg As MKNetDXLib.cls_MKDXBaseform_Progress_Helper = Nothing, Optional ByVal al_Allowed_Extensions As ArrayList = Nothing, Optional ByRef ArchiveEntry As SharpCompress.Archive.IArchiveEntry = Nothing) As cls_AddGameStats
		If al_Allowed_Extensions IsNot Nothing AndAlso al_Allowed_Extensions.Count > 0 AndAlso Not al_Allowed_Extensions.Contains(Alphaleonis.Win32.Filesystem.Path.GetExtension(inner_fi.FullName).ToLower.Replace(".", "")) Then Return New cls_AddGameStats()
		Dim MappingFound As Integer = 0

		Dim id_Moby_Platforms As Integer = cmb_Platform.EditValue

		'Dim filename As String = inner_fi.Name
		Dim folder As String = Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(main_fi.FullName)
		Dim file As String = Alphaleonis.Win32.Filesystem.Path.GetFileName(main_fi.FullName)
		Dim innerfile As Object = Nothing
		Dim size As Long = 0

		If ArchiveEntry IsNot Nothing Then
			size = ArchiveEntry.Size
		Else
			size = inner_fi.Length
		End If

		Dim crc As String = ""
		Dim md5 As String = ""
		Dim sha1 As String = ""

		Dim CustomIdentifier As Object = Nothing

		Dim identification_method As enm_Identification_Methods = enm_Identification_Methods.File_Hashes

		If id_Moby_Platforms = cls_Globals.enm_Moby_Platforms.ps1 Then
			'Sony Playstation identification
			identification_method = enm_Identification_Methods.PS1_Identifier
		ElseIf id_Moby_Platforms = cls_Globals.enm_Moby_Platforms.ps2 Then
			'Sony Playstation 2 identification
			identification_method = enm_Identification_Methods.PS2_Identifier
		ElseIf id_Moby_Platforms = cls_Globals.enm_Moby_Platforms.psp Then
			'Sony Playstation Portable identification
			identification_method = enm_Identification_Methods.PSP_Identifier
		ElseIf id_Moby_Platforms = cls_Globals.enm_Moby_Platforms.scd Then
			'Sega CD / Mega CD identification
			identification_method = enm_Identification_Methods.SCD_Identifier
		ElseIf {cls_Globals.enm_Moby_Platforms.gc, cls_Globals.enm_Moby_Platforms.wii}.Contains(id_Moby_Platforms) Then
			'Nintendo GameCube identification
			identification_method = enm_Identification_Methods.GC_Wii_Identifier
		ElseIf id_Moby_Platforms = cls_Globals.enm_Moby_Platforms.cd32 Then
			'CD32 Identifier
			identification_method = enm_Identification_Methods.CD32_Identifier
		ElseIf id_Moby_Platforms = cls_Globals.enm_Moby_Platforms.dc Then
			'DreamCast Identifier
			identification_method = enm_Identification_Methods.DC_Identifier
		ElseIf id_Moby_Platforms = cls_Globals.enm_Moby_Platforms.sat Then
			'Saturn Identifier
			identification_method = enm_Identification_Methods.SAT_Identifier
		End If

		If identification_method <> enm_Identification_Methods.File_Hashes Then
			CustomIdentifier = Get_ISO_CustomIdentifier(inner_fi, identification_method)
		End If

		If identification_method = enm_Identification_Methods.File_Hashes Then
			If ArchiveEntry IsNot Nothing Then
				crc = Hex(ArchiveEntry.Crc).ToLower.PadLeft(8, "0")
			Else
				If inner_fi IsNot Nothing Then
					crc = MKNetLib.cls_MKFileSupport.CRC32Hash(inner_fi.FullName)
				Else
					crc = MKNetLib.cls_MKFileSupport.CRC32Hash(main_fi.FullName)
				End If
			End If

			'Unpack and calculate md5 and sha1 only if crc+size is not successful
			'md5 = MKNetLib.cls_MKFileSupport.MD5Hash(inner_fi.FullName)
			'sha1 = MKNetLib.cls_MKFileSupport.SHA1Hash(inner_fi.FullName)
		End If


		Dim bIsInnerFile As Boolean = False
		If ArchiveEntry IsNot Nothing Then bIsInnerFile = True
		If inner_fi IsNot Nothing AndAlso main_fi IsNot Nothing AndAlso inner_fi.FullName <> main_fi.FullName Then bIsInnerFile = True

		If ArchiveEntry IsNot Nothing Then
			innerfile = Alphaleonis.Win32.Filesystem.Path.GetFileName(ArchiveEntry.FilePath)
		End If

		If inner_fi IsNot Nothing Then
			innerfile = Alphaleonis.Win32.Filesystem.Path.GetFileName(inner_fi.FullName)
		End If


		Dim rowemugames As DS_ML.tbl_Emu_GamesRow = Nothing
		Dim bAddNew As Boolean = True
		Dim bDuplicate_Replaced As Boolean = False
		Dim bDuplicate_Added As Boolean = False

		Dim filepath As String = (folder & "\" & file & TC.NZ(innerfile, "<null>")).ToLower

		'Find local duplicate and return if there is one found
		If Not _Rescan AndAlso dict_Have.ContainsKey(filepath) Then
			'Return New cls_3IntVec(0, 0, 1)
			Dim rows As ArrayList = dict_Have(filepath) 'DS_ML.tbl_Emu_Games.Select("folder = " & TC.getSQLFormat(folder) & " AND innerfile = " & TC.getSQLFormat(innerfile))
			If rows.Count = 1 Then
				rowemugames = rows(0)
				bAddNew = False
			Else
				Return New cls_AddGameStats(0, 0, 1)
			End If
		End If

		If bAddNew Then
			rowemugames = Me.DS_ML.tbl_Emu_Games.NewRow
			rowemugames.created = DateTime.Now

			'Find double entry
			Dim id_Emu_Games As Object = Nothing

			id_Emu_Games = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Emu_Games FROM tbl_Emu_Games WHERE Folder = " & TC.getSQLFormat(folder) & " AND File = " & TC.getSQLFormat(file) & " AND InnerFile " & IIf(TC.NZ(innerfile, "").Length > 0, "= ", "IS ") & TC.getSQLFormat(innerfile), tran)

			If TC.NZ(id_Emu_Games, 0) > 0 Then

				If _Rescan Then
					Dim rows_local_dupe() As DataRow = Me.DS_ML.tbl_Emu_Games.Select("id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games))
					For Each row_local_dupe As DataRow In rows_local_dupe
						Me.DS_ML.tbl_Emu_Games.Removetbl_Emu_GamesRow(row_local_dupe)
					Next
				End If

				Dim dt_Emu_Games As New DS_ML.tbl_Emu_GamesDataTable
				DS_ML.Fill_src_frm_Rom_Manager_Emu_Games(tran, dt_Emu_Games, id_Moby_Platforms, id_Emu_Games)

				Dim rowdupe As DataRow = dt_Emu_Games.Rows(0)

				'Copy over all data from the DB's duplicate row
				For Each col As DataColumn In dt_Emu_Games.Columns
					rowemugames(col.ColumnName) = rowdupe(col.ColumnName)
				Next
			Else
				'Try to find a similar entry by crc, sha1 and md5 and ask the user if these should be replaced
				If identification_method = enm_Identification_Methods.File_Hashes Then
					id_Emu_Games = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Emu_Games FROM tbl_Emu_Games WHERE size = " & TC.getSQLFormat(size) & IIf(crc.Length > 0, " AND CRC32 = " & TC.getSQLFormat(crc), "") & IIf(sha1.Length > 0, " AND SHA1 = " & TC.getSQLFormat(sha1), "") & IIf(md5.Length > 0, " AND MD5 = " & TC.getSQLFormat(md5), ""), tran)
				End If

				If TC.NZ(id_Emu_Games, 0) > 0 Then
					Dim original As String = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT InnerFile || ' [' || Folder || '\' || File || ']' FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), tran), "<not found>")

					If prg IsNot Nothing AndAlso Not pd_Add.ApplyAll Then prg.Hide = True
					Dim bWaitCursor As Boolean = Cursor.Current = Cursors.WaitCursor
					Cursor.Current = Cursors.Default

					Dim res As DialogResult = pd_Add.Show("", "The following match has been found:" & ControlChars.CrLf & "Original: " & original & ControlChars.CrLf & "New: " & innerfile & " [" & folder & "\" & file & "]" & ControlChars.CrLf & ControlChars.CrLf & "please choose the appropriate action.")

					If bWaitCursor Then Cursor.Current = Cursors.WaitCursor
					If prg IsNot Nothing Then prg.Hide = False

					If res = Windows.Forms.DialogResult.Yes Then
						'Replace
						bDuplicate_Replaced = True

						Dim rows As DataRow() = DS_ML.tbl_Emu_Games.Select("id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games))

						For Each row As DataRow In rows
							DS_ML.tbl_Emu_Games.Rows.Remove(row)
						Next
					End If

					If res = Windows.Forms.DialogResult.No Then
						bDuplicate_Added = True

						id_Emu_Games = 0
					End If

					If res = DialogResult.Ignore Then
						Return New cls_AddGameStats(0, 0, 0, 0, 1)
					End If

					If res = Windows.Forms.DialogResult.Cancel Then
						Return Nothing
					End If
				End If

				If TC.NZ(id_Emu_Games, 0) > 0 Then
					'Dim dt_Emu_Games As DataTable = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT id_Emu_Games, Folder, File, InnerFile, Moby_Games_URLPart, Hidden FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), Nothing, tran)
					Dim dt_Emu_Games As New DS_ML.tbl_Emu_GamesDataTable
					DS_ML.Fill_src_frm_Rom_Manager_Emu_Games(tran, dt_Emu_Games, id_Moby_Platforms, id_Emu_Games)

					Dim rowdupe As DataRow = dt_Emu_Games.Rows(0)

					'Copy over all data from the DB's duplicate row
					For Each col As DataColumn In dt_Emu_Games.Columns
						rowemugames(col.ColumnName) = rowdupe(col.ColumnName)
					Next
					rowemugames("Folder") = folder
					rowemugames("File") = file
					rowemugames("InnerFile") = innerfile
				Else
					'No double or similar entries
					rowemugames("Folder") = folder
					rowemugames("File") = file
					rowemugames("InnerFile") = innerfile
				End If
			End If
		End If

		'Add other attributes
		rowemugames("id_Moby_Platforms") = id_Moby_Platforms
		rowemugames("Size") = size
		If crc.Length > 0 Then rowemugames("CRC32") = crc
		If sha1.Length > 0 Then rowemugames("SHA1") = sha1
		If md5.Length > 0 Then rowemugames("MD5") = md5
		If TC.NZ(CustomIdentifier, "").Length > 0 Then rowemugames("CustomIdentifier") = CustomIdentifier

		frm_Tag_Parser_Edit.Apply_Filename_Tags(tran, rowemugames, DS_ML.tbl_Emu_Games_Languages, DS_ML.tbl_Emu_Games_Regions, Nothing, TC.NZ(Me.BS_Moby_Platforms.Current("MultiVolume"), False))

		'### Check in Database and save id_rombase ###
		'Speedup
		Dim id_rombase As Long = Get_id_Rombase(tran, file, size, crc, md5, sha1, id_Moby_Platforms, CustomIdentifier)

		If ArchiveEntry IsNot Nothing AndAlso TC.NZ(id_rombase, 0) = 0 Then
			'TODO: deflate ArchiveEntry, calculate crc, md5 and sha and run Get_id_Rombase again

		End If

		If id_rombase > 0 Then
			rowemugames("id_Rombase") = id_rombase
			rowemugames("Name") = DBNull.Value
			rowemugames("Name_USR") = DBNull.Value
			rowemugames("Publisher") = DBNull.Value
			rowemugames("Publisher_USR") = DBNull.Value

			If TC.NZ(rowemugames("Moby_Games_URLPart"), "").Length = 0 OrElse _Rescan Then
				'Moby Game isn't identified yet, so maybe we find an entry in the rombase database
				'Dim rowrombase As DS_Rombase.tbl_RombaseRow = Nothing

				Dim oMoby_Games_URLPart As Object = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT Moby_Games_URLPart FROM tbl_Rombase WHERE id_Rombase = " & TC.getSQLFormat(id_rombase), tran)
				Dim oROMBASE_id_Moby_Platforms As Object = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Platforms FROM tbl_Rombase WHERE id_Rombase = " & TC.getSQLFormat(id_rombase), tran)

				If TC.NZ(oMoby_Games_URLPart, "").Length > 0 Then
					rowemugames("Moby_Games_URLPart") = oMoby_Games_URLPart
					MappingFound = 1
				End If

				If TC.NZ(oROMBASE_id_Moby_Platforms, 0) > 0 Then
					rowemugames("ROMBASE_id_Moby_Platforms") = oROMBASE_id_Moby_Platforms
				End If
			End If
		End If

		If bAddNew Then
			Me.DS_ML.tbl_Emu_Games.Rows.Add(rowemugames)
			If Not dict_Have.ContainsKey(filepath) Then
				dict_Have.Add(filepath, New ArrayList({rowemugames}))
			End If

			If bDuplicate_Added Then
				Return New cls_AddGameStats(0, MappingFound, 1, 0)
			ElseIf bDuplicate_Replaced Then
				Return New cls_AddGameStats(0, MappingFound, 0, 1)
			Else
				Return New cls_AddGameStats(1, MappingFound, 0, 0)
			End If
		Else
			Return New cls_AddGameStats(0, 0, 0, 0, 1)
		End If
	End Function


	''' <summary>
	''' Add a DOSBox Game from file to DS_ML.tbl_Emu_Games (more sub-entries will be created)
	''' Find a mapping to tbl_Moby_Releases
	''' Don't add duplicates
	''' </summary>
	''' <param name="tran"></param>
	''' <param name="inner_fi">FileInfo of the inner File (if it exists), else it's just the FileInfo of the main File</param>
	''' <param name="main_fi">FileInfo of the main File (this can be an archive but also a single rom)</param>
	''' <returns>cls_3IntVec with a statistical result (x: Added, y: Mapping found, z: Duplicate)</returns>
	''' <remarks></remarks>
	Private Function Add_DOSBox_Game_From_File(ByRef tran As SQLite.SQLiteTransaction, ByRef inner_fi As Alphaleonis.Win32.Filesystem.FileInfo, ByRef main_fi As Alphaleonis.Win32.Filesystem.FileInfo, Optional ByVal prg As MKNetDXLib.cls_MKDXBaseform_Progress_Helper = Nothing, Optional ByVal al_Allowed_Extensions As ArrayList = Nothing, Optional ByVal id_Emu_Games_Owner As Integer = 0, Optional ByVal UseRombaseCache As Boolean = False) As cls_3IntVec
		If al_Allowed_Extensions IsNot Nothing AndAlso al_Allowed_Extensions.Count > 0 AndAlso Not al_Allowed_Extensions.Contains(Alphaleonis.Win32.Filesystem.Path.GetExtension(inner_fi.FullName).ToLower.Replace(".", "")) Then Return New cls_3IntVec(0, 0, 0)
		Dim MappingFound As Integer = 0

		Dim id_Moby_Platforms As Integer = cmb_Platform.EditValue

		'Dim filename As String = inner_fi.Name
		Dim folder As String = Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(main_fi.FullName)
		Dim file As String = Alphaleonis.Win32.Filesystem.Path.GetFileName(main_fi.FullName)
		Dim innerfile As Object = Nothing
		Dim size As Long = inner_fi.Length

		Dim crc As String = ""
		Dim md5 As String = ""
		Dim sha1 As String = ""

		If Not {".zip", ".7z", ".rar"}.Contains(main_fi.Extension) Then
			crc = MKNetLib.cls_MKFileSupport.CRC32Hash(inner_fi.FullName)
			md5 = MKNetLib.cls_MKFileSupport.MD5Hash(inner_fi.FullName)
			sha1 = MKNetLib.cls_MKFileSupport.SHA1Hash(inner_fi.FullName)
		End If

		Dim bIsInnerFile As Boolean = inner_fi.FullName <> main_fi.FullName

		innerfile = Alphaleonis.Win32.Filesystem.Path.GetFileName(inner_fi.FullName)

		Dim rowemugames As DS_ML.tbl_Emu_GamesRow = Nothing
		Dim bAddNew As Boolean = True

		Dim filepath As String = (folder & "\" & file & TC.NZ(innerfile, "<null>")).ToLower

		'Find local duplicate and return if there is one found
		If Not _Rescan AndAlso dict_Have.ContainsKey(filepath) Then
			'Return New cls_3IntVec(0, 0, 1)
			Dim rows As ArrayList = dict_Have(filepath) 'DS_ML.tbl_Emu_Games.Select("folder = " & TC.getSQLFormat(folder) & " AND innerfile = " & TC.getSQLFormat(innerfile))
			If rows.Count = 1 Then
				rowemugames = rows(0)
				bAddNew = False
			Else
				Return New cls_3IntVec(0, 0, 1)
			End If
		End If

		If bAddNew Then
			rowemugames = Me.DS_ML.tbl_Emu_Games.NewRow
			rowemugames.created = DateTime.Now

			'Find double entry
			Dim id_Emu_Games As Object = Nothing

			id_Emu_Games = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Emu_Games FROM tbl_Emu_Games WHERE Folder = " & TC.getSQLFormat(folder) & " AND File = " & TC.getSQLFormat(file) & " AND InnerFile " & IIf(TC.NZ(innerfile, "").Length > 0, "= ", "IS ") & TC.getSQLFormat(innerfile), tran)

			If TC.NZ(id_Emu_Games, 0) > 0 Then

				If _Rescan Then
					Dim rows_local_dupe() As DataRow = Me.DS_ML.tbl_Emu_Games.Select("id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games))
					For Each row_local_dupe As DataRow In rows_local_dupe
						Me.DS_ML.tbl_Emu_Games.Removetbl_Emu_GamesRow(row_local_dupe)
					Next
				End If

				Dim dt_Emu_Games As New DS_ML.tbl_Emu_GamesDataTable
				DS_ML.Fill_src_frm_Rom_Manager_Emu_Games(tran, dt_Emu_Games, id_Moby_Platforms, id_Emu_Games)

				Dim rowdupe As DataRow = dt_Emu_Games.Rows(0)

				'Copy over all data from the DB's duplicate row
				For Each col As DataColumn In dt_Emu_Games.Columns
					rowemugames(col.ColumnName) = rowdupe(col.ColumnName)
				Next
			Else
				'Try to find a similar entry by crc, sha1 and md5 and ask the user if these should be replaced
				If crc.Length > 0 OrElse sha1.Length > 0 OrElse md5.Length > 0 Then
					id_Emu_Games = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Emu_Games FROM tbl_Emu_Games WHERE 1=1 AND size = " & TC.getSQLFormat(inner_fi.Length) & " AND " & IIf(crc.Length > 0, " AND CRC32 = " & TC.getSQLFormat(crc), "") & IIf(sha1.Length > 0, " AND SHA1 = " & TC.getSQLFormat(sha1), "") & IIf(md5.Length > 0, " AND MD5 = " & TC.getSQLFormat(md5), ""), tran)
				End If

				If TC.NZ(id_Emu_Games, 0) > 0 Then
					Dim original As String = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT InnerFile || ' [' || Folder || '\' || File || ']' FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), tran), "<not found>")

					If prg IsNot Nothing AndAlso Not pd_Add.ApplyAll Then prg.Hide = True
					Dim res As DialogResult = pd_Add.Show("", "The following match has been found:" & ControlChars.CrLf & "Original: " & original & ControlChars.CrLf & "New: " & innerfile & " [" & folder & "\" & file & "]" & ControlChars.CrLf & ControlChars.CrLf & "please choose the appropriate action.")
					If prg IsNot Nothing Then prg.Hide = False

					If res = Windows.Forms.DialogResult.Yes Then
						'Replace - Find local dupe and delete row
						Dim rows As DataRow() = DS_ML.tbl_Emu_Games.Select("id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games))
						For Each row As DataRow In rows
							DS_ML.tbl_Emu_Games.Rows.Remove(row)
						Next
					End If

					If res = Windows.Forms.DialogResult.No Then
						id_Emu_Games = 0
					End If

					If res = Windows.Forms.DialogResult.Cancel Then
						Return Nothing
					End If

					'Dim dt_Emu_Games As DataTable = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT id_Emu_Games, Folder, File, InnerFile, Moby_Games_URLPart, Hidden FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), Nothing, tran)
					Dim dt_Emu_Games As New DS_ML.tbl_Emu_GamesDataTable
					DS_ML.Fill_src_frm_Rom_Manager_Emu_Games(tran, dt_Emu_Games, id_Moby_Platforms, id_Emu_Games)

					Dim rowdupe As DataRow = dt_Emu_Games.Rows(0)

					'Copy over all data from the DB's duplicate row
					For Each col As DataColumn In dt_Emu_Games.Columns
						rowemugames(col.ColumnName) = rowdupe(col.ColumnName)
					Next
					rowemugames("Folder") = folder
					rowemugames("File") = file
					rowemugames("InnerFile") = innerfile
				Else
					'No double or similar entries
					rowemugames("Folder") = folder
					rowemugames("File") = file
					rowemugames("InnerFile") = innerfile
				End If
			End If
		End If

		'Add other attributes
		rowemugames("id_Moby_Platforms") = id_Moby_Platforms
		rowemugames("Size") = size
		If crc.Length > 0 Then rowemugames("CRC32") = crc
		If sha1.Length > 0 Then rowemugames("SHA1") = sha1
		If md5.Length > 0 Then rowemugames("MD5") = md5

		If id_Emu_Games_Owner > 0 Then rowemugames("id_Emu_Games_Owner") = id_Emu_Games_Owner

		'Check in Database and save id_rombase
		'Speedup
		'TODO: find id_rombase with sub-entries (fuzzy search)
		'-> id_Rombase gets found at the end of the procedure
		'Dim id_rombase As Long

		'If Not {".zip", ".7z"}.Contains(main_fi.Extension) Then
		'	id_rombase = Get_id_Rombase(tran, file, size, crc, md5, sha1, id_Moby_Platforms)
		'End If
		'If id_rombase > 0 Then
		'	rowemugames("id_Rombase") = id_rombase
		'End If

		'If TC.NZ(rowemugames("Moby_Games_URLPart"), "").Length = 0 Then
		'	'Moby Game isn't identified yet, so maybe we find an entry in the rombase database
		'	Dim rowrombase As DS_Rombase.tbl_RombaseRow = Nothing

		'	Dim oMoby_Games_URLPart As Object = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT Moby_Games_URLPart FROM tbl_Rombase WHERE id_Rombase = " & TC.getSQLFormat(id_rombase), tran)
		'	Dim oROMBASE_id_Moby_Platforms As Object = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Platforms FROM tbl_Rombase WHERE id_Rombase = " & TC.getSQLFormat(id_rombase), tran)

		'	If TC.NZ(oMoby_Games_URLPart, "").Length > 0 Then
		'		rowemugames("Moby_Games_URLPart") = oMoby_Games_URLPart
		'		MappingFound = 1
		'	End If

		'	If TC.NZ(oROMBASE_id_Moby_Platforms, 0) > 0 Then
		'		rowemugames("ROMBASE_id_Moby_Platforms") = oROMBASE_id_Moby_Platforms
		'	End If
		'End If

		frm_Tag_Parser_Edit.Apply_Filename_Tags(tran, rowemugames, DS_ML.tbl_Emu_Games_Languages, DS_ML.tbl_Emu_Games_Regions, Nothing, TC.NZ(Me.BS_Moby_Platforms.Current("MultiVolume"), False))

		If bAddNew Then
			'TODO: find the correct Config Template
			rowemugames("id_DOSBox_Configs_Template") = Get_id_DOSBox_Templates_Default()
			rowemugames("id_Rombase_DOSBox_Filetypes") = cls_Globals.enm_Rombase_DOSBox_Filetypes.zip 'Packed Content
			rowemugames("id_Rombase_DOSBox_Exe_Types") = DBNull.Value 'This is not an executable
			rowemugames("DOSBox_Mount_Destination") = "C"   'This is to be mounted as the C drive

			Dim cache_item As New cls_Get_and_Apply_id_Rombase_Emu_Games_Cache_Item(rowemugames, True)

			'TODO: if id_Emu_Games_Owner then find another Mount Destination

			Me.DS_ML.tbl_Emu_Games.Rows.Add(rowemugames)
			If Not dict_Have.ContainsKey(filepath) Then
				dict_Have.Add(filepath, New ArrayList({rowemugames}))
			End If

			'Add DOSBox_Sub_Entries here
			Add_DOSBox_Packed_Game_SubEntries(tran, main_fi.FullName, rowemugames, id_Emu_Games_Owner, cache_item)

			'Find and apply Rombase stuff
			'If Get_and_Apply_id_Rombase(tran, DS_ML.tbl_Emu_Games, rowemugames("id_Emu_Games"), 3) > 0 Then
			If Get_and_Apply_id_Rombase(tran, DS_ML.tbl_Emu_Games, rowemugames("id_Emu_Games"), UseCache:=UseRombaseCache, EmuGames_Cache:=cache_item) > 0 Then
				MappingFound = 1
			End If

			Return New cls_3IntVec(1, MappingFound, 0)
		Else
			Return New cls_3IntVec(0, 0, 1)
		End If
	End Function

	Private Sub Add_DOSBox_Packed_Game_SubEntries(ByRef tran As SQLite.SQLiteTransaction, ByVal PackFilePath As String, ByVal row_Owner As DataRow, Optional ByVal id_Emu_Games_Owner As Integer = 0, Optional ByVal cache_item As cls_Get_and_Apply_id_Rombase_Emu_Games_Cache_Item = Nothing)
		Dim rows_game As New ArrayList
		rows_game.Add(row_Owner)

		If id_Emu_Games_Owner = 0 Then
			'### Add WorkingDirectory for the game (create if possible)
			Dim cwd As String = cls_Settings.Get_DOSBox_CWD(tran) & "\" & Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(row_Owner("File"))
			If Not Alphaleonis.Win32.Filesystem.Directory.Exists(cwd) Then
				Try
					Alphaleonis.Win32.Filesystem.Directory.CreateDirectory(cwd)

				Catch ex As Exception
					MKDXHelper.ExceptionMessageBox(ex, "There has been an error while creating the working directory '" & cwd & "'. The error was: ", "Error while creating working directory")
					Return
				End Try
			End If
			Dim rowcwd As DataRow
			rowcwd = Me.DS_ML.tbl_Emu_Games.NewRow
			rowcwd("created") = DateTime.Now

			rowcwd("id_Emu_Games_Owner") = row_Owner("id_Emu_Games")
			rowcwd("Folder") = cwd
			rowcwd("File") = cwd.Replace(Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(cwd) & "\", "")
			rowcwd("id_Moby_Platforms") = cmb_Platform.EditValue

			rowcwd("id_Rombase_DOSBox_Filetypes") = cls_Globals.enm_Rombase_DOSBox_Filetypes.cwd  'Working Directory
			rowcwd("DOSBox_Mount_Destination") = "C"    'This is to be mounted as the C drive

			rows_game.Add(rowcwd)

			cache_item._rows_Emu_Games_Children.Add(rowcwd)

			Me.DS_ML.tbl_Emu_Games.Rows.Add(rowcwd)
		End If

		'### Add SubEntries for each file in the pack (excluding ignored files)
		Dim archive As SharpCompress.Archive.IArchive = Nothing

		Try
			If Is_Archive(PackFilePath) Then
				archive = SharpCompress.Archive.ArchiveFactory.Open(PackFilePath)
			End If
		Catch ex As Exception

		End Try

		If archive IsNot Nothing Then
			Try
				For Each entry As SharpCompress.Archive.IArchiveEntry In archive.Entries
					If Not entry.IsDirectory Then
						If Not DOSBox_Ignore(entry.FilePath, Hex(entry.Crc).ToLower.PadLeft(8, "0"), entry.Size) Then
							Dim bAddNew As Boolean = True 'TODO: dict_have etc.

							Dim rowsub As DataRow = Nothing

							If bAddNew Then
								rowsub = Me.DS_ML.tbl_Emu_Games.NewRow
								rowsub("created") = DateTime.Now
							End If

							rowsub("id_Emu_Games_Owner") = IIf(id_Emu_Games_Owner = 0, row_Owner("id_Emu_Games"), id_Emu_Games_Owner)
							rowsub("Folder") = row_Owner("Folder")
							rowsub("File") = row_Owner("File")
							rowsub("InnerFile") = entry.FilePath
							rowsub("id_Moby_Platforms") = cmb_Platform.EditValue

							rowsub("size") = entry.Size
							rowsub("crc32") = Hex(entry.Crc).ToLower.PadLeft(8, "0")

							Dim sub_file As String = entry.FilePath.ToLower

							If sub_file.EndsWith(".exe") OrElse sub_file.EndsWith(".bat") OrElse sub_file.EndsWith(".com") Then 'Executables
								rowsub("id_Rombase_DOSBox_Filetypes") = cls_Globals.enm_Rombase_DOSBox_Filetypes.exe  'Executable

								rowsub("id_Rombase_DOSBox_Exe_Types") = DBNull.Value  'Check this against rombase
							ElseIf sub_file.EndsWith(".iso") OrElse sub_file.EndsWith(".cue") Then  'CD images
								rowsub("id_Rombase_DOSBox_Filetypes") = cls_Globals.enm_Rombase_DOSBox_Filetypes.iso

								rowsub("DOSBox_Mount_Destination") = "D"  'CDs are mounted as D drive
								rowsub("Volume_Number") = 1

								For Each row_game As DataRow In rows_game
									If TC.NZ(row_game("id_Rombase_DOSBox_Filetypes"), 0) = cls_Globals.enm_Rombase_DOSBox_Filetypes.iso Then
										If TC.NZ(rowsub("Volume_Number"), 0) < TC.NZ(row_game("Volume_Number"), 0) + 1 Then
											rowsub("Volume_Number") = TC.NZ(row_game("Volume_Number"), 0) + 1
										End If
									End If
								Next
							Else
								rowsub("id_Rombase_DOSBox_Filetypes") = cls_Globals.enm_Rombase_DOSBox_Filetypes.int  'Internal file
							End If

							rows_game.Add(rowsub)

							cache_item._rows_Emu_Games_Children.Add(rowsub)


							If bAddNew Then
								Me.DS_ML.tbl_Emu_Games.Rows.Add(rowsub)

								'TODO: dict_have
								'If Not dict_Have.ContainsKey(filepath) Then
								'	dict_Have.Add(filepath, 0)
								'End If
							End If
						End If
					End If
				Next
			Catch ex As Exception

			End Try
		End If
	End Sub

	''' <summary>
	''' Add a ScummVM Game to DS_ML.tbl_Emu_Games
	''' Find a mapping to tbl_Moby_Releases
	''' Don't add duplicates
	''' </summary>
	''' <param name="tran"></param>
	''' <returns>cls_AddGameStats</returns>
	''' <remarks></remarks>
	Private Function Add_ScummVM_Game(ByRef tran As SQLite.SQLiteTransaction, ByRef dict_ScummVMEntry As Dictionary(Of String, String), Optional ByVal prg As MKNetDXLib.cls_MKDXBaseform_Progress_Helper = Nothing) As cls_AddGameStats
		Dim MappingFound As Integer = 0

		Dim id_Moby_Platforms As Integer = cmb_Platform.EditValue

		'Dim filename As String = inner_fi.Name
		Dim folder As String = dict_ScummVMEntry("path")
		Dim file As Object = Nothing 'TODO: Add ScummVM game from .zip/.7z/.rar
		Dim innerfile As Object = dict_ScummVMEntry("description")
		Dim CustomIdentifier As Object = dict_ScummVMEntry("CustomIdentifier") & ":" & dict_ScummVMEntry("gameid")

		Dim rowemugames As DS_ML.tbl_Emu_GamesRow = Nothing
		Dim bAddNew As Boolean = True
		Dim bDuplicate_Replaced As Boolean = False
		Dim bDuplicate_Added As Boolean = False

		'Find local duplicate and return if there is one found
		Dim folderID As String = (folder & "\" & TC.NZ(file, "<null>") & TC.NZ(innerfile, "<null>")).ToLower

		If Not _Rescan AndAlso dict_Have.ContainsKey(folderID) Then
			Dim rows As ArrayList = dict_Have(folderID)

			If rows.Count = 1 Then
				rowemugames = rows(0)
				bAddNew = False
			Else
				Return New cls_AddGameStats(0, 0, 1)
			End If
		End If

		If bAddNew Then
			rowemugames = Me.DS_ML.tbl_Emu_Games.NewRow
			rowemugames.created = DateTime.Now

			rowemugames("id_ScummVM_Configs_Template") = Get_id_ScummVM_Templates_Default() 'Get_id_DOSBox_Templates_Default()

			'Find double entry
			Dim id_Emu_Games As Object = Nothing

			id_Emu_Games = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Emu_Games FROM tbl_Emu_Games WHERE Folder LIKE " & TC.getSQLFormat(folder) & " AND File " & IIf(TC.NZ(innerfile, "").Length > 0, "= ", "IS ") & TC.getSQLFormat(file) & " AND InnerFile " & IIf(TC.NZ(innerfile, "").Length > 0, "= ", "IS ") & TC.getSQLFormat(innerfile), tran)

			If TC.NZ(id_Emu_Games, 0) > 0 Then

				If _Rescan Then
					Dim rows_local_dupe() As DataRow = Me.DS_ML.tbl_Emu_Games.Select("id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games))
					For Each row_local_dupe As DataRow In rows_local_dupe
						Me.DS_ML.tbl_Emu_Games.Removetbl_Emu_GamesRow(row_local_dupe)
					Next
				End If

				Dim dt_Emu_Games As New DS_ML.tbl_Emu_GamesDataTable
				DS_ML.Fill_src_frm_Rom_Manager_Emu_Games(tran, dt_Emu_Games, id_Moby_Platforms, id_Emu_Games)

				Dim rowdupe As DataRow = dt_Emu_Games.Rows(0)

				'Copy over all data from the DB's duplicate row
				For Each col As DataColumn In dt_Emu_Games.Columns
					rowemugames(col.ColumnName) = rowdupe(col.ColumnName)
					rowemugames("CustomIdentifier") = CustomIdentifier
				Next
			Else
				If TC.NZ(id_Emu_Games, 0) > 0 Then
					Dim original As String = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT InnerFile || ' [' || Folder || '\' || File || ']' FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), tran), "<not found>")

					If prg IsNot Nothing AndAlso Not pd_Add.ApplyAll Then prg.Hide = True
					Dim bWaitCursor As Boolean = Cursor.Current = Cursors.WaitCursor
					Cursor.Current = Cursors.Default

					Dim res As DialogResult = pd_Add.Show("", "The following match has been found:" & ControlChars.CrLf & "Original: " & original & ControlChars.CrLf & "New: " & innerfile & " [" & folder & "\" & file & "]" & ControlChars.CrLf & ControlChars.CrLf & "please choose the appropriate action.")

					If bWaitCursor Then Cursor.Current = Cursors.WaitCursor
					If prg IsNot Nothing Then prg.Hide = False

					If res = Windows.Forms.DialogResult.Yes Then
						'Replace
						bDuplicate_Replaced = True

						Dim rows As DataRow() = DS_ML.tbl_Emu_Games.Select("id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games))

						For Each row As DataRow In rows
							DS_ML.tbl_Emu_Games.Rows.Remove(row)
						Next
					End If

					If res = Windows.Forms.DialogResult.No Then
						bDuplicate_Added = True

						id_Emu_Games = 0
					End If

					If res = Windows.Forms.DialogResult.Cancel Then
						Return Nothing
					End If
				End If

				If TC.NZ(id_Emu_Games, 0) > 0 Then
					Dim dt_Emu_Games As New DS_ML.tbl_Emu_GamesDataTable
					DS_ML.Fill_src_frm_Rom_Manager_Emu_Games(tran, dt_Emu_Games, id_Moby_Platforms, id_Emu_Games)

					Dim rowdupe As DataRow = dt_Emu_Games.Rows(0)

					'Copy over all data from the DB's duplicate row
					For Each col As DataColumn In dt_Emu_Games.Columns
						rowemugames(col.ColumnName) = rowdupe(col.ColumnName)
					Next
					rowemugames("Folder") = folder
					rowemugames("File") = DBNull.Value ' file ' We don't save the scanned gameid anymore, we use --auto-detect when launching
					rowemugames("InnerFile") = innerfile
				Else
					'No double or similar entries
					rowemugames("Folder") = folder
					rowemugames("File") = DBNull.Value ' file ' We don't save the scanned gameid anymore, we use --auto-detect when launching
					rowemugames("InnerFile") = innerfile
				End If
			End If
		End If

		'Add other attributes
		rowemugames("id_Moby_Platforms") = id_Moby_Platforms
		rowemugames("Size") = 0
		rowemugames("CustomIdentifier") = CustomIdentifier

		frm_Tag_Parser_Edit.Apply_Filename_Tags(tran, rowemugames, DS_ML.tbl_Emu_Games_Languages, DS_ML.tbl_Emu_Games_Regions, Nothing, TC.NZ(Me.BS_Moby_Platforms.Current("MultiVolume"), False), False)

		'### Check in Database and save id_rombase ###
		'Speedup
		Dim id_rombase As Long = Get_id_Rombase(tran, file, 0, "", "", "", id_Moby_Platforms, CustomIdentifier)

		If id_rombase > 0 Then
			rowemugames("id_Rombase") = id_rombase
			rowemugames("Name") = DBNull.Value
			rowemugames("Name_USR") = DBNull.Value
			rowemugames("Publisher") = DBNull.Value
			rowemugames("Publisher_USR") = DBNull.Value

			If TC.NZ(rowemugames("Moby_Games_URLPart"), "").Length = 0 OrElse _Rescan Then
				'Moby Game isn't identified yet, so maybe we find an entry in the rombase database
				'Dim rowrombase As DS_Rombase.tbl_RombaseRow = Nothing

				Dim oMoby_Games_URLPart As Object = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT Moby_Games_URLPart FROM tbl_Rombase WHERE id_Rombase = " & TC.getSQLFormat(id_rombase), tran)
				Dim oROMBASE_id_Moby_Platforms As Object = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Platforms FROM tbl_Rombase WHERE id_Rombase = " & TC.getSQLFormat(id_rombase), tran)
				Dim oROMBASE_id_Moby_Platforms_Alternative As Object = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Platforms_Alternative FROM tbl_Rombase WHERE id_Rombase = " & TC.getSQLFormat(id_rombase), tran)

				If TC.NZ(oMoby_Games_URLPart, "").Length > 0 Then
					rowemugames("Moby_Games_URLPart") = oMoby_Games_URLPart
					MappingFound = 1
				End If

				If TC.NZ(oROMBASE_id_Moby_Platforms, 0) > 0 Then
					rowemugames("ROMBASE_id_Moby_Platforms") = oROMBASE_id_Moby_Platforms
				End If

				If TC.NZ(oROMBASE_id_Moby_Platforms_Alternative, 0) > 0 Then
					rowemugames("id_Moby_Platforms_Alternative") = oROMBASE_id_Moby_Platforms_Alternative
				End If
			End If
		End If

		Dim filepath As String = (folder & "\" & file & TC.NZ(innerfile, "<null>")).toLower

		If bAddNew Then
			Me.DS_ML.tbl_Emu_Games.Rows.Add(rowemugames)
			If Not dict_Have.ContainsKey(filepath) Then
				dict_Have.Add(filepath, New ArrayList({rowemugames}))
			End If

			If bDuplicate_Added Then
				Return New cls_AddGameStats(0, MappingFound, 1, 0)
			ElseIf bDuplicate_Replaced Then
				Return New cls_AddGameStats(0, MappingFound, 0, 1)
			Else
				Return New cls_AddGameStats(1, MappingFound, 0, 0)
			End If
		Else
			Return New cls_AddGameStats(0, 0, 0, 0, 1)
		End If
	End Function


	Private Sub popmnu_Rom_Manager_BeforePopup(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles popmnu_Rom_Manager.BeforePopup
		If Not grd_Emu_Games.Allow_Popup Then
			e.Cancel = True
			Return
		End If

		If TC.NZ(cmb_Platform.EditValue, 0) = 0 Then
			e.Cancel = True
			Return
		End If

		Dim iRowHandles As Integer() = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Emu_Games)
		Dim iNumRows As Integer = iRowHandles.Length

		Me.bbi_Debug_Export_XML.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		Me.bbi_Debug_Import_XML.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		Me.bbi_Debug_Group_Volumes.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		Me.bbi_Debug_SetModified.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		Me.bbi_Debug_Apply_TDC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

#If DEBUG Then
		Me.bbi_Debug_Export_XML.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
		Me.bbi_Debug_Import_XML.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
		Me.bbi_Debug_Group_Volumes.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
		Me.bbi_Debug_SetModified.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
		Me.bbi_Debug_Apply_TDC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
#End If

		If iNumRows <= 0 Then
			Me.bbi_RemoveLink.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
			Me.bbi_Delete_Games.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
			Me.bbi_Change_Directory.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
			Me.bbi_Edit_Game.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
			Me.bbi_Edit_Multiple_Games.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
			Me.bbi_Rescan.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
			Me.bbi_SetHidden.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
			Me.bbi_UnsetHidden.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
			Me.bbi_SetLink.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
			Me.bbi_Export.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
			Me.bbi_Auto_Link.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
			Me.bbi_MobyLink_QA.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		Else
			Me.bbi_RemoveLink.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
			Me.bbi_Delete_Games.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
			Me.bbi_Change_Directory.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
			Me.bbi_Edit_Game.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
			Me.bbi_Edit_Multiple_Games.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
			Me.bbi_Rescan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
			Me.bbi_SetHidden.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
			Me.bbi_UnsetHidden.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
			Me.bbi_SetLink.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
			Me.bbi_Export.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
			Me.bbi_Auto_Link.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
			Me.bbi_MobyLink_QA.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

			Me.bbi_Delete_Games.Caption = "Remove " & iNumRows & IIf(iNumRows > 1, " games", " game") & " from your collection"
			Me.bbi_RemoveLink.Caption = "Rem&ove link on " & iNumRows & IIf(iNumRows > 1, " games", " game")
			Me.bbi_Rescan.Caption = "&Rescan " & iNumRows & IIf(iNumRows > 1, " games", " game")
			Me.bbi_SetHidden.Caption = "Set &hidden on " & iNumRows & IIf(iNumRows > 1, " games", " game")
			Me.bbi_UnsetHidden.Caption = "&Unset hidden on " & iNumRows & IIf(iNumRows > 1, " games", " game")
			Me.bbi_Export.Caption = "E&xport " & iNumRows & IIf(iNumRows > 1, " games", " game")
			Me.bbi_MobyLink_QA.Caption = "MobyLink &QA on " & iNumRows & IIf(iNumRows > 1, " games", " game")

			If BS_Moby_Releases.Current Is Nothing Then
				Me.bbi_SetLink.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
			Else
				Me.bbi_SetLink.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
				Me.bbi_SetLink.Caption = "Se&t link to '" & BS_Moby_Releases.Current("Gamename") & "' on " & iNumRows & IIf(iNumRows > 1, " games", " game")
			End If
		End If

		If Provide_Merge Then
			If BS_Emu_Games.Current Is Nothing Then
				bbi_Merge_Select.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
				bbi_Merge_Start.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
			Else
				bbi_Merge_Select.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
				bbi_Merge_Start.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

				If merge_row Is Nothing Then
					bbi_Merge_Select.Caption = bbi_Merge_Select_Caption.Replace("%0%", TC.NZ(BS_Emu_Games.Current("InnerFile"), "<unknown>"))
					bbi_Merge_Start.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
				Else
					bbi_Merge_Select.Caption = "Remove selection of " & TC.NZ(merge_row("InnerFile"), "<unknown>") & " for merging"

					If BS_Emu_Games.Current("id_Emu_Games") <> merge_row("id_Emu_Games") Then
						bbi_Merge_Start.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
						bbi_Merge_Start.Caption = bbi_Merge_Start_Caption.Replace("%0%", TC.NZ(merge_row("InnerFile"), "<unknown>")).Replace("%1%", TC.NZ(BS_Emu_Games.Current("InnerFile"), "<unknown>"))
					Else
						bbi_Merge_Start.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
					End If
				End If
			End If
		End If
	End Sub

	Private Sub Remove_Moby_Link(ByRef row_Emu_Games As DataRow)
		If row_Emu_Games("Name_USR") IsNot DBNull.Value Then
			row_Emu_Games("Name") = row_Emu_Games("Name_USR")
		End If

		If row_Emu_Games("Publisher_USR") IsNot DBNull.Value Then
			row_Emu_Games("Publisher") = row_Emu_Games("Publisher_USR")
		End If

		row_Emu_Games("Moby_Games_URLPart") = DBNull.Value
		row_Emu_Games("deprecated") = DBNull.Value

		If Me.cmb_Platform.EditValue = cls_Globals.enm_Moby_Platforms.scummvm Then
			row_Emu_Games("id_Moby_Platforms_Alternative") = DBNull.Value
		End If

		Update_Children(row_Emu_Games)
	End Sub

	Private Sub bbi_RemoveLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_RemoveLink.ItemClick
		Dim iRowHandles As Integer() = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Emu_Games)
		Dim iNumRows As Integer = iRowHandles.Length

		If iNumRows <= 0 Then
			Return
		End If

		For Each iRowHandle As Integer In iRowHandles
			Dim row As DataRow = gv_Emu_Games.GetRow(iRowHandle).Row
			'row("Moby_Games_URLPart") = DBNull.Value
			Remove_Moby_Link(row)
		Next

		gv_Emu_Games.RefreshData()
	End Sub

	Private Sub btn_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save.Click
		Save()
	End Sub

	Private Sub frm_Rom_Manager_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		If Save(True) = DialogResult.Cancel Then
			e.Cancel = True
			Return
		End If

		GC.Collect()
		GC.Collect()

		Me.DialogResult = _DialogResult
	End Sub

	Private Function Save_MV(ByVal Save_Main_Entries As Boolean, ByRef tran As SQLite.SQLiteTransaction) As DialogResult
		'SPEEDUP: Create INSERT Command (doesn't speed up)
		'DS_ML.tbl_Emu_Games

		'Dim Column_Blacklist As String() = {"ROMBASE_id_Moby_Platforms", "id_Emu_Games", "Rating_Gameplay", "Rating_Graphics", "Rating_Sound", "Rating_Story", "Rating_Personal", "Num_Played", "Num_Runtime"}

		'Dim sSQL_INSERT1 = "INSERT INTO tbl_Emu_Games ("
		'Dim sSQL_INSERT2 As String = " VALUES ("
		'Dim bFirst As Boolean = True

		'Dim cmd_Insert As SQLite.SQLiteCommand = tran.Connection.CreateCommand
		'cmd_Insert.Transaction = tran

		'Dim ar_ParamNums As New ArrayList

		'Dim iParamNum As Integer = 0

		'For Each col As DataColumn In DS_ML.tbl_Emu_Games.Columns
		'	If Not Column_Blacklist.Contains(col.ColumnName) Then
		'		If bFirst Then
		'			sSQL_INSERT1 &= "	"
		'			sSQL_INSERT2 &= "	"
		'			bFirst = False
		'		Else
		'			sSQL_INSERT1 &= "	, "
		'			sSQL_INSERT2 &= "	, "
		'		End If
		'		sSQL_INSERT1 &= col.ColumnName
		'		sSQL_INSERT2 &= "@" & col.ColumnName
		'		'ar_Insert_Params.Add(col.ColumnName)

		'		Dim param As SQLite.SQLiteParameter = cmd_Insert.CreateParameter
		'		param.ParameterName = "@" & col.ColumnName
		'		cmd_Insert.Parameters.Add(param)
		'		ar_ParamNums.Add(iParamNum)
		'	End If

		'	iParamNum += 1
		'Next


		'sSQL_INSERT1 &= ")"
		'sSQL_INSERT2 &= "); SELECT last_insert_rowid()"

		'cmd_Insert.CommandText = sSQL_INSERT1 & sSQL_INSERT2

		'Create UPDATE query for _USR fields (to be needed later)
		Dim s_USR_Update_Statement As String = Create_USR_Update_Statement(tran)

		Dim rows() As DataRow = Nothing

		If Save_Main_Entries Then
			rows = DS_ML.tbl_Emu_Games.Select("id_Emu_Games_Owner IS NULL", "id_Emu_Games DESC")
		Else
			rows = DS_ML.tbl_Emu_Games.Select("id_Emu_Games_Owner IS NOT NULL", "id_Emu_Games DESC")
		End If

		Dim dict_Emu_Games_Children As New Dictionary(Of Long, ArrayList)
		If Save_Main_Entries Then
			For Each row As DataRow In DS_ML.tbl_Emu_Games.Rows
				If row.RowState <> DataRowState.Deleted AndAlso row.RowState <> DataRowState.Detached Then
					Dim id_Emu_Games_Owner As Long = TC.NZ(row("id_Emu_Games_Owner"), 0L)
					If id_Emu_Games_Owner <> 0L Then
						If dict_Emu_Games_Children.ContainsKey(id_Emu_Games_Owner) Then
							dict_Emu_Games_Children(id_Emu_Games_Owner).Add(row)
						Else
							Dim al As New ArrayList
							al.Add(row)
							dict_Emu_Games_Children.Add(id_Emu_Games_Owner, al)
						End If
					End If
				End If
			Next
		End If

		Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(tran), 400, 60, ProgressBarStyle.Blocks, False, "Saving " & IIf(Save_Main_Entries, "Main", "Sub") & " Entries {0} of {1}", 0, rows.Length, False)
		prg.Start()

		Dim GC_Counter As Integer = 0

		For Each row As DataRow In rows
			prg.IncreaseCurrentValue()

			GC_Counter += 1

			If row.RowState = DataRowState.Deleted OrElse row.RowState = DataRowState.Detached OrElse row.RowState = DataRowState.Unchanged Then
				Continue For
			End If

			Try
				Dim id_Emu_Games_Old As Integer = row("id_Emu_Games")
				Dim bWasAdded As Boolean = row.RowState = DataRowState.Added

				'Get old state (in order for possible extras renaming)
				Dim dt_Old As New DS_ML.src_ucr_Emulation_GamesDataTable
				If id_Emu_Games_Old > 0 Then
					DS_ML.Fill_src_ucr_Emulation_Games(tran, dt_Old, Nothing, Nothing, Nothing, id_Emu_Games_Old)
				End If

				'Save changes to database
				'If row("id_Emu_Games") > 0 Then
				DS_ML.Upsert_Rom_Manager_tbl_Emu_Games(tran, row)
				'Else
				'	Dim iParam As Integer = 0
				'	For Each param As SQLite.SQLiteParameter In cmd_Insert.Parameters
				'		param.Value = row(ar_ParamNums(iParam))
				'		iParam += 1
				'	Next
				'	row("id_Emu_Games") = cmd_Insert.ExecuteScalar()
				'	row.AcceptChanges()
				'End If

				'Update all child rows (volumes of the game)
				If Save_Main_Entries Then
					'If id_Emu_Games_Old <> row("id_Emu_Games") Then
					'	For Each row_volume As DataRow In Me.DS_ML.tbl_Emu_Games.Select("id_Emu_Games_Owner = " & TC.getSQLFormat(id_Emu_Games_Old))
					'		row_volume("id_Emu_Games_Owner") = row("id_Emu_Games")
					'	Next
					'End If

					'SPEEDUP
					If id_Emu_Games_Old <> row("id_Emu_Games") AndAlso dict_Emu_Games_Children.ContainsKey(id_Emu_Games_Old) Then
						For Each row_volume As DataRow In dict_Emu_Games_Children(id_Emu_Games_Old)
							row_volume("id_Emu_Games_Owner") = row("id_Emu_Games")
						Next
					End If

				End If

				If id_Emu_Games_Old > 0 Then
					Dim dt_New As New DS_ML.src_ucr_Emulation_GamesDataTable
					DS_ML.Fill_src_ucr_Emulation_Games(tran, dt_New, Nothing, Nothing, Nothing, row("id_Emu_Games"))

					'Extras could need renaming - currently only in the case of Windows Games (id_Moby_Platforms = 3)
					If TC.NZ(dt_New.Rows(0)("id_Moby_Platforms"), 0) = cls_Globals.enm_Moby_Platforms.win Then
						If dt_Old.Rows.Count = 1 Then
							Dim al_Old_Extras As ArrayList = cls_Extras.FindAllExtras(dt_Old.Rows(0)("Platform_Short"), dt_Old.Rows(0)("id_Moby_Platforms"), dt_Old.Rows(0)("Game"), dt_Old.Rows(0)("InnerFile"))
							Dim al_New_Extras As ArrayList = cls_Extras.FindAllExtras(dt_New.Rows(0)("Platform_Short"), dt_New.Rows(0)("id_Moby_Platforms"), dt_New.Rows(0)("Game"), dt_New.Rows(0)("InnerFile"))

							If al_Old_Extras.Count > 0 AndAlso Not cls_Extras.ExtrasListsEqual(al_Old_Extras, al_New_Extras) Then
								prg.Hide = True
								Dim res_extras As DialogResult = pd_ExtrasRename.Show("Extras need renaming (" & row("File") & ")")
								prg.Hide = False

								If res_extras = DialogResult.Yes Then
									'Rename all extras in al_Old_Extras
									For Each extra As cls_Extras.cls_Extras_Result In al_Old_Extras
										Dim oldpath As String = extra._Path
										Dim newfilename As String = cls_Extras.FindNextFreeExtraFilename(dt_New.Rows(0)("Platform_Short"), extra._ExtraType, Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(MKNetLib.cls_MKStringSupport.GetCleanFileName(dt_New.Rows(0)("Game"))))

										If newfilename <> "" Then
											Alphaleonis.Win32.Filesystem.File.Move(oldpath, cls_Globals.Dir_Extras & "\emulation\" & dt_New.Rows(0)("Platform_Short") & "\" & extra._ExtraType & "\" & newfilename & Alphaleonis.Win32.Filesystem.Path.GetExtension(extra._Path))
										End If
									Next
								End If
							End If
						End If
					End If
				End If

				Dim id_Emu_Games As Integer = row("id_Emu_Games")

				If Save_Main_Entries Then
					If bWasAdded Then
						'TODO: should these save operations really only be performed if the game has been added (and not rescanned)?

						If TC.NZ(row("id_Rombase"), 0) > 0 Then
							'Get Data from Rombase (RowState = Added on newly added games and rescanned games)
							Dim id_Rombase As Integer = row("id_Rombase")

							Dim dt_Rombase As DataTable = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT * FROM rombase.tbl_Rombase WHERE id_Rombase = " & TC.getSQLFormat(id_Rombase), Nothing, tran)

							For Each row_Rombase As DataRow In dt_Rombase.Rows
								Dim sSQL As String = "UPDATE main.tbl_Emu_Games SET "

								bFirst = True

								For Each col As DataColumn In row_Rombase.Table.Columns
									Select Case col.ColumnName.ToLower
										Case "created", "updated", "id_rombase", "filename", "size", "crc", "md5", "sha1", "id_moby_platforms", "id_moby_releases", "moby_platforms_urlpart", "mapping_identifier", "customidentifier", "specialinfo", "id_rombase_dosbox_configs", "id_rombase_owner"
											'Do nothing with these columns
										Case Else
											If MKNetLib.cls_MKSQLDataAccess.HasColumn(DS_ML.tbl_Emu_Games, col.ColumnName) Then
												If Not TC.IsNullNothingOrEmpty(row_Rombase(col)) Then
													If bFirst Then
														bFirst = False
													Else
														sSQL &= "	, "
													End If


													sSQL &= col.ColumnName & " = " & TC.getSQLFormat(row_Rombase(col.ColumnName))
												End If
											Else
												Dim s As String = col.ColumnName
											End If
									End Select
								Next

								sSQL &= " WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games)

								If Not bFirst Then DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
							Next

							'Copy over other rombase stuff
							'1. Alternate Titles
							DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM main.tbl_Emu_Games_Alternate_Titles WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND IFNULL(USR, 0) = 0", tran)
							DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO main.tbl_Emu_Games_Alternate_Titles (id_Emu_Games, Alternate_Title, Description) SELECT " & TC.getSQLFormat(id_Emu_Games) & ", Alternate_Title, Description FROM rombase.tbl_Rombase_Alternate_Titles RBAT WHERE id_Rombase = " & TC.getSQLFormat(id_Rombase) & " AND NOT EXISTS(SELECT * FROM main.tbl_Emu_Games_Alternate_Titles EGAT WHERE EGAT.Alternate_Title = RBAT.Alternate_Title AND EGAT.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & ")", tran)

							'2. Languages
							DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM main.tbl_Emu_Games_Languages WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND IFNULL(USR, 0) = 0", tran)
							DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO main.tbl_Emu_Games_Languages (id_Emu_Games, id_Languages) SELECT " & TC.getSQLFormat(id_Emu_Games) & ", id_Languages FROM rombase.tbl_Rombase_Languages RBL WHERE id_Rombase = " & TC.getSQLFormat(id_Rombase) & " AND NOT EXISTS(SELECT * FROM main.tbl_Emu_Games_Languages WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND id_Languages = RBL.id_Languages)", tran)

							'3. Regions
							DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM main.tbl_Emu_Games_Regions WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND IFNULL(USR, 0) = 0", tran)
							DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO main.tbl_Emu_Games_Regions (id_Emu_Games, id_Regions) SELECT " & TC.getSQLFormat(id_Emu_Games) & ", id_Regions FROM rombase.tbl_Rombase_Regions RBR WHERE id_Rombase = " & TC.getSQLFormat(id_Rombase) & " AND NOT EXISTS(SELECT * FROM main.tbl_Emu_Games_Regions WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND id_Regions = RBR.id_Regions)", tran)

							'4. Moby_Attributes
							DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM main.tbl_Emu_Games_Moby_Attributes WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND IFNULL(USR, 0) = 0", tran)
							DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO main.tbl_Emu_Games_Moby_Attributes (id_Emu_Games, id_Moby_Attributes, Used) SELECT " & TC.getSQLFormat(id_Emu_Games) & ", id_Moby_Attributes, Used FROM rombase.tbl_Rombase_Moby_Attributes RBMA WHERE id_Rombase = " & TC.getSQLFormat(id_Rombase) & " AND NOT EXISTS(SELECT * FROM main.tbl_Emu_Games_Moby_Attributes WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND id_Moby_Attributes = RBMA.id_Moby_Attributes)", tran)

							'5. Moby_Genres
							DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM main.tbl_Emu_Games_Moby_Genres WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND IFNULL(USR, 0) = 0", tran)
							DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO main.tbl_Emu_Games_Moby_Genres (id_Emu_Games, id_Moby_Genres, Used) SELECT " & TC.getSQLFormat(id_Emu_Games) & ", id_Moby_Genres, Used FROM rombase.tbl_Rombase_Moby_Genres RBMG WHERE id_Rombase = " & TC.getSQLFormat(id_Rombase) & " AND NOT EXISTS(SELECT * FROM main.tbl_Emu_Games_Moby_Genres WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND id_Moby_Genres = RBMG.id_Moby_Genres)", tran)
						End If
					End If

					'Insert Languages/Regions from Tag Parser - Only on Main Entries
					Dim rows_Languages() As DataRow = DS_ML.tbl_Emu_Games_Languages.Select("id_Emu_Games = " & id_Emu_Games_Old)
					For Each row_Languages As DataRow In rows_Languages
						row_Languages("id_Emu_Games") = row("id_Emu_Games")
					Next
					DS_ML.Upsert_tbl_Emu_Games_Languages(tran, DS_ML.tbl_Emu_Games_Languages, row("id_Emu_Games"))

					Dim rows_Regions() As DataRow = DS_ML.tbl_Emu_Games_Regions.Select("id_Emu_Games = " & id_Emu_Games_Old)
					For Each row_Regions As DataRow In rows_Regions
						row_Regions("id_Emu_Games") = row("id_Emu_Games")
					Next
					DS_ML.Upsert_tbl_Emu_Games_Regions(tran, DS_ML.tbl_Emu_Games_Regions, row("id_Emu_Games"))

					'Copy over _USR Field content (as this content is already defined by the user and he doesn't want to lose that)
					DataAccess.FireProcedure(tran.Connection, 0, s_USR_Update_Statement & TC.getSQLFormat(id_Emu_Games), tran)

				End If

				DS_ML.Update_tbl_Emu_Games_Caches(tran, id_Emu_Games)

				If GC_Counter = 100 Then
					GC.Collect()
					GC_Counter = 0
				End If
			Catch ex As Exception
				prg.Hide = True
				MKDXHelper.ExceptionMessageBox(ex, "Save")
				prg.Hide = False
				Return DialogResult.Cancel
			End Try
		Next

		prg.Close()

		Return DialogResult.Yes
	End Function

	Private Function Save(Optional ByVal AskForSave As Boolean = False) As DialogResult
		BS_DOSBox_Files_and_Folders.EndEdit()
		BS_Emu_Games.EndEdit()
		BS_Moby_Platforms.EndEdit()
		BS_MV.EndEdit()
		BS_MV_Volume.EndEdit()
		BTA_DOSBox_Exe_Types.EndEdit()
		BTA_DOSBox_Filetypes.EndEdit()
		BTA_DOSBox_Mount_Destination.EndEdit()

		Dim bHasChanges As Boolean = False

		For Each row As DataRow In DS_ML.tbl_Emu_Games.Rows
			If row.RowState <> DataRowState.Unchanged Then
				bHasChanges = True
				Exit For
			End If
		Next

		'Dim tbl_Changes As DataTable = DS_ML.tbl_Emu_Games.GetChanges
		'If tbl_Changes IsNot Nothing AndAlso tbl_Changes.Rows.Count > 0 Then
		If bHasChanges Then
			Dim res As DialogResult = Windows.Forms.DialogResult.Yes

			If AskForSave Then
				Dim cntMismatch As Integer = Me.DS_ML.tbl_Emu_Games.Select("ROMBASE_id_Moby_Platforms IS NOT NULL AND id_Moby_Platforms <> ROMBASE_id_Moby_Platforms").Length

				Dim sResult As String = "Save changes?"

				If cntMismatch > 0 Then
					sResult &= ControlChars.CrLf & ControlChars.CrLf & "WARNING: There are " & cntMismatch & " platform mismatch/es! You should not save!"
				End If

				res = MKDXHelper.MessageBox(sResult, "Save changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
			End If

			Select Case res
				Case Windows.Forms.DialogResult.Yes
					Cursor.Current = Cursors.WaitCursor

					MKNetLib.cls_MKSQLiteDataAccess.EnableHighPerformance(cls_Globals.Conn)

					Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
						Save_MV(True, tran) 'Save all Main Entries
						Save_MV(False, tran)  'Save all Sub Entries (volumes of the games)
						DS_ML.Update_Platform_NumGames_Cache_AllUsers(tran, Me.cmb_Platform.EditValue)
						Me.Refill_cmb_Platforms(tran)
						tran.Commit()
					End Using

					DS_ML.tbl_Emu_Games.AcceptChanges()

					Cursor.Current = Cursors.Default

					Me._DialogResult = DialogResult.OK
					Return Windows.Forms.DialogResult.Yes
				Case Windows.Forms.DialogResult.No
					Return Windows.Forms.DialogResult.No
				Case Else
					Return DialogResult.Cancel
			End Select
		End If

		Return DialogResult.Yes
	End Function

	Private Sub BS_Emu_Games_CurrentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BS_Emu_Games.CurrentChanged
		gv_Moby_Releases.RefreshData()

		If BS_Emu_Games.Current Is Nothing Then
			BS_MV.Filter = "id_Emu_Games = 0"
			BS_DOSBox_Files_and_Folders.Filter = "id_Emu_Games = 0"

			Return
		End If

		If Not TC.IsNullNothingOrEmpty(BS_Emu_Games.Current("Moby_Games_URLPart")) Then
			Dim iNewPos As Integer = 0

			If Not Me.cmb_Platform.EditValue = cls_Globals.enm_Moby_Platforms.scummvm OrElse TC.IsNullNothingOrEmpty(BS_Emu_Games.Current("id_Moby_Platforms_Alternative")) Then
				iNewPos = BS_Moby_Releases.Find("Moby_Games_URLPart", BS_Emu_Games.Current("Moby_Games_URLPart"))
			Else
				Dim rows_Moby_Releases() As DataRow = Me.DS_MobyDB.src_Moby_Releases.Select("Moby_Games_URLPart = " & TC.getSQLFormat(BS_Emu_Games.Current("Moby_Games_URLPart")) & " AND id_Moby_Platforms = " & TC.getSQLFormat(BS_Emu_Games.Current("id_Moby_Platforms_Alternative")))
				If rows_Moby_Releases.Count > 0 Then
					iNewPos = BS_Moby_Releases.Find("id_Moby_Releases", rows_Moby_Releases(0)("id_Moby_Releases"))
				End If
			End If

			If iNewPos >= 0 Then
				BS_Moby_Releases.Position = iNewPos
				Me.gv_Moby_Releases.ClearSelection()
				Me.gv_Moby_Releases.SelectRow(Me.gv_Moby_Releases.FocusedRowHandle)
			End If
		End If

		If TC.NZ(Me.BS_Moby_Platforms.Current("MultiVolume"), False) Then
			BS_MV.Filter = "id_Emu_Games = " & BS_Emu_Games.Current("id_Emu_Games") & " OR id_Emu_Games_Owner = " & BS_Emu_Games.Current("id_Emu_Games")
		End If

		'DOS Platform
		If BS_Moby_Platforms.Current IsNot Nothing AndAlso {cls_Globals.enm_Moby_Platforms.dos, cls_Globals.enm_Moby_Platforms.pcboot}.Contains(BS_Moby_Platforms.Current("id_Moby_Platforms")) Then
			BS_DOSBox_Files_and_Folders.Filter = "id_Emu_Games = " & BS_Emu_Games.Current("id_Emu_Games") & " OR id_Emu_Games_Owner = " & BS_Emu_Games.Current("id_Emu_Games") & " AND id_Rombase_DOSBox_Filetypes <> 7"
		End If

		gv_MV.ExpandAllGroups()
	End Sub

	Private Sub bbi_Delete_Games_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Delete_Games.ItemClick
		If DS_ML.tbl_Emu_Games.GetChanges IsNot Nothing AndAlso DS_ML.tbl_Emu_Games.GetChanges.Rows.Count > 0 Then
			MKDXHelper.MessageBox("Please save before deleting any games.", "Delete games", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		Dim iRowHandles As Integer() = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Emu_Games)
		Dim iNumRows As Integer = iRowHandles.Length

		If iNumRows <= 0 Then
			Return
		End If

		If MKDXHelper.MessageBox("Really remove " & iNumRows & IIf(iNumRows > 1, " games", " game") & " from your collection?" & IIf(TC.NZ(Me.BS_Moby_Platforms.Current("MultiVolume"), False) = True, ControlChars.CrLf & ControlChars.CrLf & "Note: this will also remove multiple volumes associated to these games.", ""), "Remove games", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
			Return
		End If

		Cursor.Current = Cursors.WaitCursor

		Try
			Dim ar_Rows As New ArrayList

			For Each iRowHandle As Integer In iRowHandles
				If iRowHandle >= 0 Then
					ar_Rows.Add(gv_Emu_Games.GetRow(iRowHandle).Row)
				End If
			Next

			Dim al_Games As New ArrayList

			For Each row As DataRow In ar_Rows
				If merge_row IsNot Nothing AndAlso Equals(row, merge_row) Then merge_row = Nothing

				Dim id_Emu_Games As Integer = row("id_Emu_Games")

				If id_Emu_Games > 0 Then
					al_Games.Add(id_Emu_Games)
				End If
			Next

			If al_Games.Count > 0 Then
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Emu_Games WHERE id_Emu_Games IN (" & TC.getSQLParameter_FromArrayList(al_Games) & ")")
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Emu_Games WHERE id_Emu_Games_Owner > 0 AND id_Emu_Games_Owner NOT IN (SELECT id_Emu_Games FROM tbl_Emu_Games)")
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Emu_Games_Languages WHERE id_Emu_Games > 0 AND id_Emu_Games NOT IN (SELECT id_Emu_Games FROM tbl_Emu_Games)")
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Emu_Games_Regions WHERE id_Emu_Games > 0 AND id_Emu_Games NOT IN (SELECT id_Emu_Games FROM tbl_Emu_Games)")
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Emu_Games_Moby_Attributes WHERE id_Emu_Games > 0 AND id_Emu_Games NOT IN (SELECT id_Emu_Games FROM tbl_Emu_Games)")
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Emu_Games_Moby_Genres WHERE id_Emu_Games > 0 AND id_Emu_Games NOT IN (SELECT id_Emu_Games FROM tbl_Emu_Games)")
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Emu_Games_Alternate_Titles WHERE id_Emu_Games > 0 AND id_Emu_Games NOT IN (SELECT id_Emu_Games FROM tbl_Emu_Games)")
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Users_Emu_Games WHERE id_Emu_Games > 0 AND id_Emu_Games NOT IN (SELECT id_Emu_Games FROM tbl_Emu_Games)")
			End If

			For Each row As DataRow In ar_Rows
				Dim rows_Children() As DataRow = DS_ML.tbl_Emu_Games.Select("id_Emu_Games_Owner = " & TC.getSQLFormat(row("id_Emu_Games")))

				For Each row_Child As DataRow In rows_Children
					Me.DS_ML.tbl_Emu_Games.Removetbl_Emu_GamesRow(row_Child)
				Next

				Me.DS_ML.tbl_Emu_Games.Removetbl_Emu_GamesRow(row)
			Next

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				DS_ML.Update_Platform_NumGames_Cache_AllUsers(tran, Me.cmb_Platform.EditValue)
				Me.Refill_cmb_Platforms(tran)
				tran.Commit()

				Me.DS_ML.tbl_Emu_Games.AcceptChanges()
			End Using
		Catch ex As Exception

		End Try

		Cursor.Current = Cursors.Default

		'Refill(cmb_Platform.EditValue)
		gv_Emu_Games.RefreshData()
	End Sub

	Private Sub bbi_SetHidden_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bbi_SetHidden.ItemClick
		Dim iRowHandles As Integer() = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Emu_Games)
		Dim iNumRows As Integer = iRowHandles.Length

		If iNumRows <= 0 Then
			Return
		End If

		For Each iRowHandle As Integer In iRowHandles
			Dim row As DataRow = gv_Emu_Games.GetRow(iRowHandle).Row
			row("Hidden") = True
		Next

		gv_Emu_Games.RefreshData()
	End Sub

	Private Sub bbi_UnsetHidden_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bbi_UnsetHidden.ItemClick
		Dim iRowHandles As Integer() = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Emu_Games)
		Dim iNumRows As Integer = iRowHandles.Length

		If iNumRows <= 0 Then
			Return
		End If

		For Each iRowHandle As Integer In iRowHandles
			Dim row As DataRow = gv_Emu_Games.GetRow(iRowHandle).Row
			row("Hidden") = False
		Next

		gv_Emu_Games.RefreshData()
	End Sub

	Private Sub bbi_SetLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_SetLink.ItemClick
		Dim iRowHandles As Integer() = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Emu_Games)
		Dim iNumRows As Integer = iRowHandles.Length

		If BS_Emu_Games.Current IsNot Nothing AndAlso BS_Moby_Releases.Current IsNot Nothing Then
			Dim mobyRow As DataRow = BS_Moby_Releases.Current.Row

			For Each iRowHandle As Integer In iRowHandles
				Dim row As DataRow = gv_Emu_Games.GetRow(iRowHandle).Row
				'row("Moby_Games_URLPart") = BS_Moby_Releases.Current("Moby_Games_URLPart").ToString.Replace("\", "")
				Dim res As DialogResult = Set_Moby_Link(row, mobyRow)
				If res = Windows.Forms.DialogResult.Cancel Then
					Exit For
				End If
			Next
		End If

		gv_Emu_Games.RefreshData()
	End Sub

	Private Sub gv_Moby_Releases_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gv_Moby_Releases.RowCellStyle
		If BS_Emu_Games.Current IsNot Nothing Then
			If TC.NZ(BS_Emu_Games.Current("Moby_Games_URLPart"), "") <> "" Then
				Dim row = gv_Moby_Releases.GetRow(e.RowHandle)
				If TC.NZ(row("Moby_Games_URLPart"), "") = BS_Emu_Games.Current("Moby_Games_URLPart") Then
					If Not cmb_Platform.EditValue = cls_Globals.enm_Moby_Platforms.scummvm OrElse TC.IsNullNothingOrEmpty(BS_Emu_Games.Current("id_Moby_Platforms_Alternative")) OrElse TC.NZ(BS_Emu_Games.Current("id_Moby_Platforms_Alternative"), 0) = TC.NZ(row("id_Moby_Platforms"), 0) Then
						e.Appearance.Font = New Font(e.Appearance.Font.FontFamily.Name, e.Appearance.Font.Size, FontStyle.Bold)
					End If
				End If
			End If
		End If

		If e.RowHandle >= 0 Then
			Dim row As DataRow = gv_Moby_Releases.GetRow(e.RowHandle).Row
			If TC.NZ(row("Highlighted"), False) = True Then
				e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
			End If
		End If
	End Sub

	Private Sub bbi_Change_Directory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Change_Directory.ItemClick
		Dim rows_Game() As DataRow = DS_ML.tbl_Emu_Games.Select("id_Emu_Games_Owner IS NULL AND Folder = " & TC.getSQLFormat(BS_Emu_Games.Current("Folder")))
		Dim count As Integer = rows_Game.Length
		Using frm As New frm_Rom_Manager_ChangeDirectory(BS_Emu_Games.Current("Folder"), count)
			If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
				If frm.NewDir <> BS_Emu_Games.Current("Folder") Then
					'Select all Main- and Sub-Entries
					rows_Game = DS_ML.tbl_Emu_Games.Select("Folder = " & TC.getSQLFormat(BS_Emu_Games.Current("Folder")))
					For Each row_Game As DataRow In rows_Game
						row_Game("Folder") = frm.NewDir
					Next
				End If
			End If
		End Using
	End Sub

	Private Sub gv_Emu_Games_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gv_Emu_Games.RowCellStyle
		Dim row As DataRow = gv_Emu_Games.GetRow(e.RowHandle).Row
		If row IsNot Nothing Then
			If TC.NZ(row("ROMBASE_id_Moby_Platforms"), 0) <> 0 AndAlso TC.NZ(row("id_Moby_Platforms"), -1) <> TC.NZ(row("ROMBASE_id_Moby_Platforms"), -1) Then
				e.Appearance.ForeColor = Color.Red
			End If
		End If

		If TC.NZ(row("tmp_Highlighted"), False) = True AndAlso TC.NZ(row("Unavailable"), False) = True Then
			e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold Or FontStyle.Strikeout)
		ElseIf TC.NZ(row("tmp_Highlighted"), False) = True Then
			e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
		ElseIf TC.NZ(row("Unavailable"), False) = True Then
			e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Strikeout)
		End If
	End Sub

	Private Sub bbi_Merge_Select_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bbi_Merge_Select.ItemClick
		If merge_row Is Nothing Then
			If DS_ML.tbl_Emu_Games.GetChanges IsNot Nothing Then
				MKDXHelper.MessageBox("Please save first and merge later.", "Select for merging", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Return
			End If
			merge_row = BS_Emu_Games.Current.Row
		Else
			merge_row = Nothing
		End If
	End Sub

	Private Sub bbi_Merge_Start_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bbi_Merge_Start.ItemClick
		If DS_ML.tbl_Emu_Games.Select("id_Emu_Games < 0").Length > 0 Then
			MKDXHelper.MessageBox("Please save first and merge later.", "Select for merging", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		If MKDXHelper.MessageBox("The following operation will copy all attributes of " & merge_row("InnerFile") & " to " & BS_Emu_Games.Current("InnerFile") & "'s data including Regions, Languages, Mobygame entry, Genre, Theme, Perspective etc." & ControlChars.CrLf & ControlChars.CrLf & "The file-specific data (Filename, Inner File, CRC32, SHA-1 and MD5 hashes) will be left untouched for " & BS_Emu_Games.Current("InnerFile") & "." & ControlChars.CrLf & ControlChars.CrLf & "The game " & merge_row("InnerFile") & " will be removed afterwards. Do you want to proceed?", "Merge", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
			Cursor.Current = Cursors.WaitCursor

			Dim old_id_Emu_Games As Integer = merge_row("id_Emu_Games")
			Dim new_id_Emu_Games As Integer = BS_Emu_Games.Current("id_Emu_Games")
			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				Try
					DS_ML.Merge_tbl_Emu_Games(tran, old_id_Emu_Games, new_id_Emu_Games)
					DS_ML.Merge_tbl_Emu_Games_Alternate_Titles(tran, old_id_Emu_Games, new_id_Emu_Games)
					DS_ML.Merge_tbl_Emu_Games_Languages(tran, old_id_Emu_Games, new_id_Emu_Games)
					DS_ML.Merge_tbl_Emu_Games_Moby_Attributes(tran, old_id_Emu_Games, new_id_Emu_Games)
					DS_ML.Merge_tbl_Emu_Games_Moby_Genres(tran, old_id_Emu_Games, new_id_Emu_Games)
					DS_ML.Merge_tbl_Emu_Games_Regions(tran, old_id_Emu_Games, new_id_Emu_Games)
					tran.Commit()
					Refill(cmb_Platform.EditValue)

					Cursor.Current = Cursors.Default

					MKDXHelper.MessageBox("Merge Operation successful.", "Merge", MessageBoxButtons.OK, MessageBoxIcon.Information)
				Catch ex As Exception
					tran.Rollback()

					Cursor.Current = Cursors.Default

					MKDXHelper.ExceptionMessageBox(ex, "An unexpected Error occured, any changes have been reverted. The Error was:" & ControlChars.CrLf, "Error")
				End Try
			End Using
		End If
	End Sub

	Private Sub bbi_Rescan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Rescan.ItemClick
		Dim pd_NotFound As New cls_PermDecision(Me, "File not found", "", {New cls_PermDecision.PermDecisionButton("&Remove", Windows.Forms.DialogResult.Yes), New cls_PermDecision.PermDecisionButton("&Keep", Windows.Forms.DialogResult.No), New cls_PermDecision.PermDecisionButton("&Cancel", Windows.Forms.DialogResult.Cancel)})

		Dim iRowHandles As Integer() = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Emu_Games)
		Dim iNumRows As Integer = iRowHandles.Length

		If iNumRows <= 0 Then
			Return
		End If

		Dim bUseRombaseCache As Boolean = (iNumRows > 10)

		Dim bOnlyMissingFiles As Boolean = False

		If TC.NZ(cmb_Platform.EditValue, 0L) = cls_Globals.enm_Moby_Platforms.scummvm Then
			'ScummVM rescan only supports scan for missing file/directories
			bOnlyMissingFiles = True
		End If

		If Not bOnlyMissingFiles Then
			Using frm As New frm_Rescan_Options("Perform a rescan on " & iNumRows & IIf(iNumRows > 1, " games", " game") & "?")
				If frm.ShowDialog(Me) <> DialogResult.Yes Then
					Return
				End If

				bOnlyMissingFiles = frm.chb_Only_Missing_Files.Checked
			End Using
		End If

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction()
			Dim ar_Main_Rows As New ArrayList
			Dim sFiles As New List(Of String)

			For Each iRowHandle As Integer In iRowHandles
				Dim row As DataRow = gv_Emu_Games.GetRow(iRowHandle).Row
				'files.Add(New cls_3ObjVec(row("Folder"), row("File"), row("InnerFile")))
				ar_Main_Rows.Add(row)
				sFiles.Add(TC.NZ(row("Folder"), "") & "\" & TC.NZ(row("File"), ""))
			Next

			If Not bOnlyMissingFiles Then
				Dim bAllowArchiveContentAnalysis As Boolean = True

				If {cls_Globals.enm_Moby_Platforms.dos, cls_Globals.enm_Moby_Platforms.scummvm}.Contains(Me.cmb_Platform.EditValue) Then
					bAllowArchiveContentAnalysis = False
				End If

				Using frm As New frm_Tag_Parser_Edit(tran, sFiles.ToArray, Nothing, TC.NZ(Me.BS_Moby_Platforms.Current("MultiVolume"), False), True, bAllowArchiveContentAnalysis)
					If frm.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then
						Return
					End If
				End Using
			End If

			Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(tran), 400, 60, ProgressBarStyle.Blocks, False, "Rescanning {0} of {1}", 0, iNumRows, False)
			prg.Start()

			Try
				Me._Rescan = True

				Cursor.Current = Cursors.WaitCursor

				Dim al_Delete As New ArrayList
				Dim ar_Delete_Rows As New ArrayList

				For Each main_Row As DataRow In ar_Main_Rows
					prg.IncreaseCurrentValue()

					Dim mainfile As String = main_Row("Folder") & "\" & main_Row("File")
					Dim innerfile As String = ""
					Dim sTmpDir As String = ""

					If TC.NZ(main_Row("File"), "") <> "" Then
						'Check file existence
						If Not Alphaleonis.Win32.Filesystem.File.Exists(mainfile) Then

							Cursor.Current = Cursors.Default
							prg.Hide = True
							Dim res As DialogResult = pd_NotFound.Show("File not found", "The file " & mainfile & " has not been found. Do you want to remove it from the collection, keep it or cancel the rescan process?")
							Cursor.Current = Cursors.WaitCursor
							prg.Hide = False

							Select Case res
								Case Windows.Forms.DialogResult.Yes
									'Remove the Game
									Dim id_Emu_Games As Integer = main_Row("id_Emu_Games")

									If id_Emu_Games > 0 Then
										al_Delete.Add(id_Emu_Games)
									End If

									ar_Delete_Rows.Add(main_Row)

									Continue For
								Case Windows.Forms.DialogResult.No
									'Keep (do nothing eh?)
									If TC.NZ(main_Row("Unavailable"), False) = False Then
										main_Row("Unavailable") = True
									End If
									Continue For
								Case Windows.Forms.DialogResult.Cancel
									'Cancel the rescan operation
									Exit For
							End Select
						Else
							'File exists, check .lnk target
							If Alphaleonis.Win32.Filesystem.Path.GetExtension(mainfile).ToLower.Replace(".", "") = "lnk" Then
								'get executable and things from lnk info
								Dim lnk As String = mainfile
								Dim fullpath As String = MKNetLib.cls_MKFileSupport.LNK_GetPath(lnk)

								If Not Alphaleonis.Win32.Filesystem.File.Exists(fullpath) Then

									prg.Hide = True
									Cursor.Current = Cursors.Default
									Dim res As DialogResult = pd_NotFound.Show("File not found", "The file referenced by the link file cannot be found. Do you want to remove it from the collection, keep it or cancel the rescan process?" & ControlChars.CrLf & ControlChars.CrLf & "Link file: " & lnk & ControlChars.CrLf & "Referenced file:" & fullpath)
									prg.Hide = False
									Cursor.Current = Cursors.WaitCursor

									Select Case res
										Case Windows.Forms.DialogResult.Yes
											'Remove the Game
											Dim id_Emu_Games As Integer = main_Row("id_Emu_Games")

											If id_Emu_Games > 0 Then
												al_Delete.Add(id_Emu_Games)
											End If

											ar_Delete_Rows.Add(main_Row)

											Continue For
										Case Windows.Forms.DialogResult.No
											'Keep (do nothing eh?)
											If TC.NZ(main_Row("Unavailable"), False) = False Then
												main_Row("Unavailable") = True
											End If

											Continue For
										Case Windows.Forms.DialogResult.Cancel
											'Cancel the rescan operation
											Exit For
									End Select
								End If
							End If

						End If
					Else
						If Not Alphaleonis.Win32.Filesystem.Directory.Exists(main_Row("Folder")) Then

							prg.Hide = True
							Cursor.Current = Cursors.Default
							Dim res As DialogResult = pd_NotFound.Show("Directory not found", "The directory '" & main_Row("Folder") & "' has not been found. Do you want to remove the game from the collection, keep it or cancel the rescan process?")
							prg.Hide = False
							Cursor.Current = Cursors.WaitCursor

							Select Case res
								Case Windows.Forms.DialogResult.Yes
									Dim id_Emu_Games As Integer = main_Row("id_Emu_Games")

									If id_Emu_Games > 0 Then
										al_Delete.Add(id_Emu_Games)
									End If

									ar_Delete_Rows.Add(main_Row)

									Continue For
								Case Windows.Forms.DialogResult.No
									'Keep (do nothing eh?)
									If TC.NZ(main_Row("Unavailable"), False) = False Then
										main_Row("Unavailable") = True
									End If

									Continue For
								Case Windows.Forms.DialogResult.Cancel
									'Cancel the rescan operation
									Exit For
							End Select
						End If
					End If

					If TC.NZ(main_Row("Unavailable"), False) = True Then
						main_Row("Unavailable") = False
					End If

					If TC.NZ(cmb_Platform.EditValue, 0L) <> cls_Globals.enm_Moby_Platforms.scummvm AndAlso TC.NZ(main_Row("File"), "").ToLower <> TC.NZ(main_Row("InnerFile"), "").ToLower Then
						Try
							'We could have an archive with a specific inner file
							Dim archive As SharpCompress.Archive.IArchive = SharpCompress.Archive.ArchiveFactory.Open(mainfile)

							If archive IsNot Nothing Then
								sTmpDir = MKNetLib.cls_MKFileSupport.CreateTempDir("ml_")

								For Each entry As SharpCompress.Archive.IArchiveEntry In archive.Entries
									If Not entry.IsDirectory Then
										If Alphaleonis.Win32.Filesystem.Path.GetFileName(entry.FilePath).ToLower = TC.NZ(main_Row("InnerFile"), "").ToLower Then
											'We found the file we want to extract
											Dim sOutFile As String = sTmpDir & "\" & Alphaleonis.Win32.Filesystem.Path.GetFileName(entry.FilePath)
											If Not Alphaleonis.Win32.Filesystem.File.Exists(sOutFile) Then
												Using sw As New IO.StreamWriter(sOutFile)
													GC.SuppressFinalize(sw.BaseStream)
													entry.WriteTo(sw.BaseStream)
													'sw.BaseStream.Close()
													sw.Close()

													innerfile = sOutFile
													Exit For
												End Using
											End If
										End If
									End If
								Next
							End If

							If Not Alphaleonis.Win32.Filesystem.File.Exists(innerfile) Then

								prg.Hide = True
								Cursor.Current = Cursors.Default
								Dim res As DialogResult = pd_NotFound.Show("Inner file not found", "The expected inner file '" & TC.NZ(main_Row("InnerFile"), "") & "' could not be found in " & mainfile & ". Do you want to remove it from the collection, keep it or cancel the rescan process?")
								prg.Hide = False
								Cursor.Current = Cursors.WaitCursor

								Select Case res
									Case Windows.Forms.DialogResult.Yes
										Dim id_Emu_Games As Integer = main_Row("id_Emu_Games")

										If id_Emu_Games > 0 Then
											al_Delete.Add(id_Emu_Games)
										End If

										ar_Delete_Rows.Add(main_Row)

										Continue For
									Case Windows.Forms.DialogResult.No
										'Keep (do nothing eh?)
										If TC.NZ(main_Row("Unavailable"), False) = False Then
											main_Row("Unavailable") = True
										End If

										Continue For
									Case Windows.Forms.DialogResult.Cancel
										'Cancel the rescan operation
										Exit For
								End Select
							End If
						Catch ex As Exception
							prg.Hide = True
							Cursor.Current = Cursors.Default
							Dim res As DialogResult = pd_NotFound.Show("Error on decompression", "There has been an error on decompressing " & mainfile & " with the expected inner file " & main_Row("InnerFile") & ". Do you want to remove it from the collection, keep it or cancel the rescan process?" & ControlChars.CrLf & ControlChars.CrLf & "The error was: " & ex.Message)
							prg.Hide = False
							Cursor.Current = Cursors.WaitCursor

							Select Case res
								Case Windows.Forms.DialogResult.Yes
									Dim id_Emu_Games As Integer = main_Row("id_Emu_Games")

									If id_Emu_Games > 0 Then
										al_Delete.Add(id_Emu_Games)
									End If

									ar_Delete_Rows.Add(main_Row)

									Continue For
								Case Windows.Forms.DialogResult.No
									'Keep (do nothing eh?)
									If TC.NZ(main_Row("Unavailable"), False) = False Then
										main_Row("Unavailable") = True
									End If

									Continue For
								Case Windows.Forms.DialogResult.Cancel
									'Cancel the rescan operation
									Exit For
							End Select
						End Try
					Else
						innerfile = mainfile
					End If

					If TC.NZ(main_Row("Unavailable"), False) = True Then
						main_Row("Unavailable") = False
					End If

					If Not bOnlyMissingFiles Then
						If cmb_Platform.EditValue = cls_Globals.enm_Moby_Platforms.dos Then
							Rescan_DOSBox_Game(main_Row("id_Emu_Games"), tran, Me.DS_ML.tbl_Emu_Games, bUseRombaseCache, Me.DS_ML.tbl_Emu_Games_Languages, Me.DS_ML.tbl_Emu_Games_Regions, prg)
						ElseIf cmb_Platform.EditValue = cls_Globals.enm_Moby_Platforms.mame Then

						Else
							AddGameFromFile(tran, New Alphaleonis.Win32.Filesystem.FileInfo(innerfile), New Alphaleonis.Win32.Filesystem.FileInfo(mainfile), prg)
						End If
					End If

					If sTmpDir <> "" Then MKNetLib.cls_MKFileSupport.Delete_Directory(sTmpDir)
				Next

				If ar_Delete_Rows.Count > 0 Then
					For Each row_Delete As DataRow In ar_Delete_Rows
						row_Delete.Delete()
					Next
				End If

				If al_Delete.Count > 0 Then
					DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games WHERE id_Emu_Games IN (" & TC.getSQLParameter_FromArrayList(al_Delete) & ")", tran)
					DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games WHERE id_Emu_Games_Owner > 0 AND id_Emu_Games_Owner NOT IN (SELECT id_Emu_Games FROM tbl_Emu_Games)", tran)
					DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Languages WHERE id_Emu_Games > 0 AND id_Emu_Games NOT IN (SELECT id_Emu_Games FROM tbl_Emu_Games)", tran)
					DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Regions WHERE id_Emu_Games > 0 AND id_Emu_Games NOT IN (SELECT id_Emu_Games FROM tbl_Emu_Games)", tran)
					DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Moby_Attributes WHERE id_Emu_Games > 0 AND id_Emu_Games NOT IN (SELECT id_Emu_Games FROM tbl_Emu_Games)", tran)
					DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Moby_Genres WHERE id_Emu_Games > 0 AND id_Emu_Games NOT IN (SELECT id_Emu_Games FROM tbl_Emu_Games)", tran)
					DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Alternate_Titles WHERE id_Emu_Games > 0 AND id_Emu_Games NOT IN (SELECT id_Emu_Games FROM tbl_Emu_Games)", tran)
				End If

				Try
					tran.Commit()
				Catch ex As Exception

				End Try
			Catch ex As Exception
				Cursor.Current = Cursors.Default
				prg.Close()
				MKDXHelper.ExceptionMessageBox(ex, "Rescan")
				tran.Rollback()
				Refill(cmb_Platform.EditValue)
			Finally
				Cursor.Current = Cursors.Default
				prg.Close()
				Me._Rescan = False
			End Try

			gv_Emu_Games.RefreshData()
		End Using
	End Sub

	Private Function Create_USR_Update_Statement(ByVal tran As SQLite.SQLiteTransaction) As String
		Dim sSQL As String = "UPDATE tbl_Emu_Games SET "
		Dim bFirst As Boolean = True
		Dim dt_Cols As DataTable = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT * FROM tbl_Emu_Games LIMIT 0", Nothing, tran)
		If dt_Cols IsNot Nothing Then
			For Each col As DataColumn In dt_Cols.Columns
				If col.ColumnName.Contains("_USR") Then
					If bFirst Then
						bFirst = False
					Else
						sSQL &= ", "
					End If

					sSQL &= col.ColumnName.Replace("_USR", "") & " = CASE WHEN LENGTH(" & col.ColumnName & ") > 0 THEN " & col.ColumnName & " ELSE " & col.ColumnName.Replace("_USR", "") & " END"
				End If
			Next
		End If

		If bFirst Then
			Return "SELECT 0"
		End If

		sSQL &= " WHERE id_Emu_Games = "
		Return sSQL
	End Function

	Private Sub bbi_Edit_Game_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Edit_Game.ItemClick
		If BS_Emu_Games.Current Is Nothing Then Return
		If DS_ML.tbl_Emu_Games.GetChanges IsNot Nothing Then
			MKDXHelper.MessageBox("Please save before editing any games.", "Edit Game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		Using frm As New frm_Emu_Game_Edit(CInt(BS_Emu_Games.Current("id_Emu_Games")))
			frm.ShowDialog(Me)
		End Using

	End Sub

	Public Function ConvertToInteger(ByVal input As String) As Integer
		Dim output As Integer = 0

		Integer.TryParse(input, output)

		Return output
	End Function

	Private Sub bbi_Edit_Multiple_Games_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bbi_Edit_Multiple_Games.ItemClick
		If BS_Emu_Games.Current Is Nothing Then Return
		If DS_ML.tbl_Emu_Games.GetChanges IsNot Nothing Then
			MKDXHelper.MessageBox("Please save before editing any games.", "Edit Game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		Dim iRowHandles As Integer() = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Emu_Games)
		Dim iNumRows As Integer = iRowHandles.Length

		If iNumRows <= 0 Then
			Return
		End If

		Dim al_id_Emu_Games As New ArrayList

		For Each iRowHandle As Integer In iRowHandles
			Dim row As DataRow = gv_Emu_Games.GetRow(iRowHandle).Row
			al_id_Emu_Games.Add(CInt(row("id_Emu_Games")))
		Next

		Dim id_Emu_Games_Int As Integer() = CType(al_id_Emu_Games.ToArray(GetType(Integer)), Integer())

		Using frm As New frm_Emu_Game_Edit(id_Emu_Games_Int, "Edit " & iRowHandles.Length & " Games", "", False, BS_Moby_Platforms.Current("id_Moby_Platforms"))
			frm.ShowDialog(Me)
		End Using
	End Sub

	Private Sub bbi_Moby_Games_Open_Moby_Page_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Moby_Games_Open_Moby_Page.ItemClick
		If BS_Moby_Releases.Current Is Nothing Then Return

		Try
			Dim sURL As String = "http://www.mobygames.com/game/" & BS_Moby_Platforms.Current("URLPart") & "/" & TC.NZ(BS_Moby_Releases.Current("Moby_Games_URLPart"), "").Replace("\", "")
			Dim procinfo As New ProcessStartInfo(sURL)
			procinfo.UseShellExecute = True
			Process.Start(procinfo)
		Catch ex As Exception

		End Try
	End Sub

	Private Sub rpi_Volume_Number_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rpi_Volume_Number.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
			BS_MV.Current("Volume_Number") = DBNull.Value
			sender.EditValue = DBNull.Value
		End If
	End Sub

	Private Sub rpi_DOSBox_Volume_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rpi_DOSBox_Volume.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
			BS_DOSBox_Files_and_Folders.Current("Volume_Number") = DBNull.Value
			sender.EditValue = DBNull.Value
		End If
	End Sub

	Private Sub bbi_Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Export.ItemClick
		Dim iRowHandles As Integer() = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Emu_Games)
		Dim iNumRows As Integer = iRowHandles.Length

		If iNumRows <= 0 Then
			Return
		End If

		If DS_ML.tbl_Emu_Games.GetChanges IsNot Nothing Then
			MKDXHelper.MessageBox("Please save before exporting any games.", "Export games", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		Dim al_id_Emu_Games As New ArrayList

		Dim bHasDOSEntry As Boolean = False

		For Each iRowHandle As Integer In iRowHandles
			Dim row As DataRow = gv_Emu_Games.GetRow(iRowHandle).Row
			al_id_Emu_Games.Add(row("id_Emu_Games"))
			If TC.NZ(row("id_Moby_Platforms"), 0) = 2 Then
				bHasDOSEntry = True
			End If
		Next

		Using frm As New frm_Export(al_id_Emu_Games, bHasDOSEntry)
			frm.ShowDialog(Me)
		End Using
	End Sub

	Private Sub bbi_Debug_Import_XML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Debug_Import_XML.ItemClick
		Dim sPath As Object = MKNetLib.cls_MKFileSupport.OpenFileDialog("Import XML", "XML Files (*.xml)|*.xml", ParentForm:=Me)
		If TC.NZ(sPath, "") <> "" Then
			Me.DS_ML.Clear()
			Me.DS_ML.ReadXml(sPath)
			MKDXHelper.MessageBox("Import done.", "Export XML", MessageBoxButtons.OK, MessageBoxIcon.Information)
		End If
	End Sub

	Private Sub bbi_Debug_Export_XML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Debug_Export_XML.ItemClick
		Dim sPath As Object = MKNetLib.cls_MKFileSupport.SaveFileDialog("Export XML", "XML Files (*.xml)|*.xml")
		If TC.NZ(sPath, "") <> "" Then
			Me.DS_ML.WriteXml(sPath)
			MKDXHelper.MessageBox("Export done.", "Export XML", MessageBoxButtons.OK, MessageBoxIcon.Information)
		End If
	End Sub

	Private Sub bbi_Debug_Group_Volumes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Debug_Group_Volumes.ItemClick
		Me.Group_Volumes()
	End Sub

	Private Sub bbi_Debug_SetModified_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Debug_SetModified.ItemClick
		For Each row As DataRow In DS_ML.tbl_Emu_Games.Rows
			row.SetModified()
		Next
	End Sub

	Private Sub gv_DOSBox_Files_and_Folders_CustomColumnDisplayText(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles gv_DOSBox_Files_and_Folders.CustomColumnDisplayText
		If e.Column Is col_DOSBox_Displayname Then
			'Dim row As DataRow = gv_DOSBox_Files_and_Folders.GetRow(e.ListSourceRowIndex).Row
			Dim oInnerFile As Object = gv_DOSBox_Files_and_Folders.GetListSourceRowCellValue(e.ListSourceRowIndex, "InnerFile")
			Dim oFolder As Object = gv_DOSBox_Files_and_Folders.GetListSourceRowCellValue(e.ListSourceRowIndex, "Folder")

			If TC.NZ(oInnerFile, "").Length > 0 Then
				e.DisplayText = oInnerFile
			Else
				e.DisplayText = oFolder
			End If
		End If

		If e.Column Is colid_Rombase_DOSBox_Filetypes Then
			'Dim row As DataRow = gv_DOSBox_Files_and_Folders.GetRow(e.ListSourceRowIndex).Row
			Dim o_id_Rombase_DOSBox_Filetypes As Object = gv_DOSBox_Files_and_Folders.GetListSourceRowCellValue(e.ListSourceRowIndex, "id_Rombase_DOSBox_Filetypes")
			Dim o_id_Rombase_DOSBox_Exe_Types As Object = gv_DOSBox_Files_and_Folders.GetListSourceRowCellValue(e.ListSourceRowIndex, "id_Rombase_DOSBox_Exe_Types")

			If TC.NZ(o_id_Rombase_DOSBox_Filetypes, 0) <> 0 Then
				Dim part1 As String = ""
				Dim part2 As String = ""

				Dim rows_Filetype As DataRow() = BTA_DOSBox_Filetypes.DS.Tables(0).Select("id_Rombase_DOSBox_Filetypes = " & TC.getSQLFormat(o_id_Rombase_DOSBox_Filetypes))
				If rows_Filetype.Length = 1 Then part1 = rows_Filetype(0)("Displayname")

				If TC.NZ(o_id_Rombase_DOSBox_Filetypes, 0) = cls_Globals.enm_Rombase_DOSBox_Filetypes.exe Then  'Executable
					Dim rows_Exe_Type As DataRow() = BTA_DOSBox_Exe_Types.DS.Tables(0).Select("id_Rombase_DOSBox_Exe_Types = " & TC.getSQLFormat(o_id_Rombase_DOSBox_Exe_Types))
					If rows_Exe_Type.Length = 1 Then part2 = " (" & rows_Exe_Type(0)("Displayname") & ")"
				End If

				e.DisplayText = part1 & part2
			End If
		End If
	End Sub

	Private Sub BS_Moby_Platforms_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BS_Moby_Platforms.CurrentChanged
		BS_Emu_Games_CurrentChanged(BS_Emu_Games, New System.EventArgs)
	End Sub

	Public Enum enm_DOSBoxAdd_Mode
		Packed_Files = 0              'User adds packed files (a CWD per file is to be created)
		Packed_Files_in_Directory = 1 'User adds packed games within a directory and it's sub-directories (a CWD per file is to be created)
		Installed_in_Directory = 2    'User adds a game that is already installed within a directory (no CWD required)
		Install_Media_Files = 3       'User adds install media, a CWD has to be created (maybe prompt the user?)
	End Enum

	Private Sub Add_DOSBox_Games(Optional ByVal mode As enm_DOSBoxAdd_Mode = enm_DOSBoxAdd_Mode.Packed_Files, Optional ByVal file_locations As Object = Nothing, Optional ByVal id_Emu_Games_Owner As Integer = 0)
		'TODO: handle file locations (no extra prompt for the user)

		'### Adding Packed Files (from files or directory) ###"
		If {enm_DOSBoxAdd_Mode.Packed_Files, enm_DOSBoxAdd_Mode.Packed_Files_in_Directory}.Contains(mode) Then
			'Packed files (either from file list or directory)

			If Not cls_Settings.Check_DOSBox_CWD Then
				Return
			End If

			Dim sFiles As String() = Nothing

			Dim al_Allowed_Extensions As ArrayList = Get_Allowed_Extensions(TC.NZ(cmb_Platform.EditValue, 0))

			If mode = enm_DOSBoxAdd_Mode.Packed_Files_in_Directory Then
				If MKDXHelper.MessageBox("For DOS Games, please choose a directory that contains packed releases (i.e. multiple games in zip files, sub-directories will be scanned too) here. If you want to add an installed DOS game instance, choose 'Add Game (Installed, Directory)'. Do you want to continue?", "Add Games", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
					Return
				End If

				Dim sFolder As String = MKNetLib.cls_MKFileSupport.OpenFolderDialog(TC.NZ(cls_Settings.GetSetting("Browse_Romfolder" & "_" & BS_Moby_Platforms.Current("URLPart")), ""))

				If Alphaleonis.Win32.Filesystem.Directory.Exists(sFolder) Then
					cls_Settings.SetSetting("Browse_Romfolder" & "_" & BS_Moby_Platforms.Current("URLPart"), sFolder)

					Dim arrFiles As New ArrayList
					Dim fsrch As New MKNetLib.cls_MKFileSearch(New Alphaleonis.Win32.Filesystem.DirectoryInfo(sFolder), "*.*", "*", {".zip", ".rar", ".7z"})
					fsrch.Search()
					arrFiles.AddRange(fsrch.Files)

					Dim al_filelocations As New ArrayList

					For Each fi As Alphaleonis.Win32.Filesystem.FileInfo In arrFiles
						al_filelocations.Add(fi.FullName)
					Next

					sFiles = CType(al_filelocations.ToArray(GetType(String)), String())

					If sFiles Is Nothing OrElse sFiles.Length = 0 Then
						Return
					End If
				End If
			End If

			If mode = enm_DOSBoxAdd_Mode.Packed_Files Then
				If MKDXHelper.MessageBox("For DOS Games, please choose packed releases (i.e. one or more zip files) here. If you want to add an installed DOS game instance, choose 'Add Game (Installed, Directory)'. Do you want to continue?", "Add Games", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
					Return
				End If

				pd_Add.ApplyAll = False

				merge_row = Nothing

				sFilter = "Packed DOS Releases |*.zip;*.7z;*.rar"

				sFiles = MKNetLib.cls_MKFileSupport.OpenFileDialog("Select Files", sFilter, 0, "", TC.NZ(cls_Settings.GetSetting("Browse_Romfolder" & "_" & BS_Moby_Platforms.Current("URLPart")), ""), True, ParentForm:=Me)

				If sFiles(0).Length = 0 Then Return

				If Alphaleonis.Win32.Filesystem.Directory.Exists(Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(sFiles(0))) Then
					cls_Settings.SetSetting("Browse_Romfolder" & "_" & BS_Moby_Platforms.Current("URLPart"), Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(sFiles(0)))
				End If
			End If

			If sFiles Is Nothing OrElse sFiles(0).Length = 0 Then Return

			Using frm As New frm_Tag_Parser_Edit(Nothing, sFiles, al_Allowed_Extensions, TC.NZ(Me.BS_Moby_Platforms.Current("MultiVolume"), False), True, AllowArchiveContentAnalysis:=False)
				If frm.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then
					Return
				End If
			End Using

			If id_Emu_Games_Owner > 0 Then
				If sFiles.Length > 10 Then
					Prepare_dict_Rombase()
				End If
			End If

			Dim Aborted As Boolean = False
			Dim result As New cls_3IntVec()

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(tran), 400, 60, ProgressBarStyle.Blocks, False, "Reading {0} of {1}", 0, sFiles.Length, False)
				prg.Start()

				PrepareDictHave()

				Dim bUseRombaseCache As Boolean = (sFiles.Count > 10)

				For Each sFile As String In sFiles
					prg.IncreaseCurrentValue()

					If Alphaleonis.Win32.Filesystem.File.Exists(sFile) Then
						Dim res As cls_3IntVec = Add_DOSBox_Game_From_File(tran, New Alphaleonis.Win32.Filesystem.FileInfo(sFile), New Alphaleonis.Win32.Filesystem.FileInfo(sFile), prg, al_Allowed_Extensions, id_Emu_Games_Owner, bUseRombaseCache)

						If res Is Nothing Then
							'User cancelled
							Aborted = True
							Exit For
						End If

						result.Add(res)
					End If
				Next

				tran.Commit()
				prg.Close()
			End Using

			Dim cntMismatch As Integer = Me.DS_ML.tbl_Emu_Games.Select("ROMBASE_id_Moby_Platforms IS NOT NULL AND id_Moby_Platforms <> ROMBASE_id_Moby_Platforms").Length

			Clear_dict_Rombase()

			Dim sResult As String = "Result" & IIf(Aborted, "after cancellation", "") & ": " & result._x & " games added, " & result._y & " of them were mapped to game info, " & result._z & " duplicates have been ignored."

			If cntMismatch > 0 Then
				sResult &= ControlChars.CrLf & ControlChars.CrLf & "WARNING: There have been " & cntMismatch & " platform mismatch/es detected! All affected entries are in red color. Did you import Roms for the correct Platform?"
			End If

			MKDXHelper.MessageBox(sResult, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
		End If 'Mode = Packed files (from either directory or file list)

		'### Adding Directory containing installed instance ###
		If mode = enm_DOSBoxAdd_Mode.Installed_in_Directory Then
			If MKDXHelper.MessageBox("Select a Directory that contains a single installed game in the next dialog. Do you want to continue?", "Add Games", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
				Return
			End If

			Dim sFolder As String = MKNetLib.cls_MKFileSupport.OpenFolderDialog(TC.NZ(cls_Settings.GetSetting("Browse_Romfolder" & "_" & BS_Moby_Platforms.Current("URLPart")), ""))

			If Alphaleonis.Win32.Filesystem.Directory.Exists(sFolder) Then
				cls_Settings.SetSetting("Browse_Romfolder" & "_" & BS_Moby_Platforms.Current("URLPart"), sFolder)

				Dim sDirectoryName As String = sFolder.Replace(Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(sFolder) & "\", "")

				Using frm_tag As New frm_Tag_Parser_Edit(Nothing, {sDirectoryName}, Nothing, TC.NZ(Me.BS_Moby_Platforms.Current("MultiVolume"), False), True, False)
					If frm_tag.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then
						Return
					End If
				End Using

				Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction

					'Dim arrFiles As New ArrayList
					'Dim fsrch As New MKNetLib.cls_MKFileSearch(New Alphaleonis.Win32.Filesystem.DirectoryInfo(sFolder), "*.*", "*")
					'fsrch.Search()
					'arrFiles.AddRange(fsrch.Files)

					'TODO: check if the current working directory is already in use

					Dim MappingFound As Integer = 0

					Dim id_Moby_Platforms As Integer = cmb_Platform.EditValue

					'Dim filename As String = inner_fi.Name
					Dim folder As String = sFolder
					Dim file As String = folder.Replace(Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(folder) & "\", "")
					'Dim innerfile As Object = ""
					'Dim size As Long = inner_fi.Length

					'Dim crc As String = ""
					'Dim md5 As String = ""
					'Dim sha1 As String = ""

					'If Not {".zip", ".7z"}.Contains(main_fi.Extension) Then
					'	crc = MKNetLib.cls_MKFileSupport.CRC32Hash(inner_fi.FullName)
					'	md5 = MKNetLib.cls_MKFileSupport.MD5Hash(inner_fi.FullName)
					'	sha1 = MKNetLib.cls_MKFileSupport.SHA1Hash(inner_fi.FullName)
					'End If

					'Dim bIsInnerFile As Boolean = inner_fi.FullName <> main_fi.FullName

					'innerfile = Alphaleonis.Win32.Filesystem.Path.GetFileName(inner_fi.FullName)

					Dim rowemugames As DS_ML.tbl_Emu_GamesRow = Nothing
					Dim bAddNew As Boolean = True

					'Find local duplicate and return if there is one found
					'If Not _Rescan AndAlso dict_Have.ContainsKey(filepath) Then
					'	'Return New cls_3IntVec(0, 0, 1)
					'	Dim rows As DataRow() = DS_ML.tbl_Emu_Games.Select("folder = " & TC.getSQLFormat(folder) & " AND innerfile = " & TC.getSQLFormat(innerfile))
					'	If rows.Length = 1 Then
					'		rowemugames = rows(0)
					'		bAddNew = False
					'	Else
					'		Return New cls_3IntVec(0, 0, 1)
					'	End If
					'End If

					If bAddNew Then
						rowemugames = Me.DS_ML.tbl_Emu_Games.NewRow
						rowemugames.created = DateTime.Now

						'Find double entry
						Dim id_Emu_Games As Object = Nothing

						If TC.NZ(id_Emu_Games, 0) > 0 Then

							If _Rescan Then
								Dim rows_local_dupe() As DataRow = Me.DS_ML.tbl_Emu_Games.Select("id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games))
								For Each row_local_dupe As DataRow In rows_local_dupe
									Me.DS_ML.tbl_Emu_Games.Removetbl_Emu_GamesRow(row_local_dupe)
								Next
							End If

							Dim dt_Emu_Games As New DS_ML.tbl_Emu_GamesDataTable
							DS_ML.Fill_src_frm_Rom_Manager_Emu_Games(tran, dt_Emu_Games, id_Moby_Platforms, id_Emu_Games)

							Dim rowdupe As DataRow = dt_Emu_Games.Rows(0)

							'Copy over all data from the DB's duplicate row
							For Each col As DataColumn In dt_Emu_Games.Columns
								rowemugames(col.ColumnName) = rowdupe(col.ColumnName)
							Next
						Else
							'Try to find a similar entry by crc, sha1 and md5 and ask the user if these should be replaced

							'id_Emu_Games = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Emu_Games FROM tbl_Emu_Games WHERE 1=1 " & IIf(crc.Length > 0, " AND CRC32 = " & TC.getSQLFormat(crc), "") & IIf(sha1.Length > 0, " AND SHA1 = " & TC.getSQLFormat(sha1), "") & IIf(md5.Length > 0, " AND MD5 = " & TC.getSQLFormat(md5), ""), tran)

							'If TC.NZ(id_Emu_Games, 0) > 0 Then
							'	Dim original As String = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT InnerFile || ' [' || Folder || '\' || File || ']' FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), tran), "<not found>")

							'	If prg IsNot Nothing AndAlso Not pd_Add.ApplyAll Then prg.Hide = True
							'	Dim res As DialogResult = pd_Add.Show("", "The following match has been found:" & ControlChars.CrLf & "Original: " & original & ControlChars.CrLf & "New: " & innerfile & " [" & folder & "\" & file & "]" & ControlChars.CrLf & ControlChars.CrLf & "please choose the appropriate action.")
							'	If prg IsNot Nothing Then prg.Hide = False

							'	If res = Windows.Forms.DialogResult.Yes Then
							'		'Replace - Find local dupe and delete row
							'		Dim rows As DataRow() = DS_ML.tbl_Emu_Games.Select("id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games))
							'		For Each row As DataRow In rows
							'			DS_ML.tbl_Emu_Games.Rows.Remove(row)
							'		Next
							'	End If

							'	If res = Windows.Forms.DialogResult.No Then
							'		id_Emu_Games = 0
							'	End If

							'	If res = Windows.Forms.DialogResult.Cancel Then
							'		Return Nothing
							'	End If
							'End If

							If TC.NZ(id_Emu_Games, 0) > 0 Then
								'Dim dt_Emu_Games As DataTable = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT id_Emu_Games, Folder, File, InnerFile, Moby_Games_URLPart, Hidden FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), Nothing, tran)
								Dim dt_Emu_Games As New DS_ML.tbl_Emu_GamesDataTable
								DS_ML.Fill_src_frm_Rom_Manager_Emu_Games(tran, dt_Emu_Games, id_Moby_Platforms, id_Emu_Games)

								Dim rowdupe As DataRow = dt_Emu_Games.Rows(0)

								'Copy over all data from the DB's duplicate row
								For Each col As DataColumn In dt_Emu_Games.Columns
									rowemugames(col.ColumnName) = rowdupe(col.ColumnName)
								Next
								rowemugames("Folder") = folder
								rowemugames("File") = file
							Else
								'No double or similar entries
								rowemugames("Folder") = folder
								rowemugames("File") = file
							End If
						End If
					End If

					'Add other attributes
					rowemugames("id_Moby_Platforms") = id_Moby_Platforms
					'rowemugames("Size") = Size
					'If crc.Length > 0 Then rowemugames("CRC32") = crc
					'If sha1.Length > 0 Then rowemugames("SHA1") = sha1
					'If md5.Length > 0 Then rowemugames("MD5") = md5

					'Check in Database and save id_rombase
					'Speedup
					'TODO: find id_rombase with sub-entries (fuzzy search)
					'Dim id_rombase As Long

					'If Not {".zip", ".7z"}.Contains(main_fi.Extension) Then
					'	id_rombase = Get_id_Rombase(tran, file, Size, crc, md5, sha1, id_Moby_Platforms)
					'End If
					'If id_rombase > 0 Then
					'	rowemugames("id_Rombase") = id_rombase
					'End If

					'If TC.NZ(rowemugames("Moby_Games_URLPart"), "").Length = 0 Then
					'	'Moby Game isn't identified yet, so maybe we find an entry in the rombase database
					'	Dim rowrombase As DS_Rombase.tbl_RombaseRow = Nothing

					'	Dim oMoby_Games_URLPart As Object = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT Moby_Games_URLPart FROM tbl_Rombase WHERE id_Rombase = " & TC.getSQLFormat(id_rombase), tran)
					'	Dim oROMBASE_id_Moby_Platforms As Object = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Platforms FROM tbl_Rombase WHERE id_Rombase = " & TC.getSQLFormat(id_rombase), tran)

					'	If TC.NZ(oMoby_Games_URLPart, "").Length > 0 Then
					'		rowemugames("Moby_Games_URLPart") = oMoby_Games_URLPart
					'		MappingFound = 1
					'	End If

					'	If TC.NZ(oROMBASE_id_Moby_Platforms, 0) > 0 Then
					'		rowemugames("ROMBASE_id_Moby_Platforms") = oROMBASE_id_Moby_Platforms
					'	End If
					'End If

					frm_Tag_Parser_Edit.Apply_Filename_Tags(tran, rowemugames, DS_ML.tbl_Emu_Games_Languages, DS_ML.tbl_Emu_Games_Regions, Nothing, TC.NZ(Me.BS_Moby_Platforms.Current("MultiVolume"), False))

					If bAddNew Then
						'TODO: find the correct Config Template
						rowemugames("id_DOSBox_Configs_Template") = Get_id_DOSBox_Templates_Default()
						rowemugames("id_Rombase_DOSBox_Filetypes") = cls_Globals.enm_Rombase_DOSBox_Filetypes.cwd 'Working Directory
						rowemugames("id_Rombase_DOSBox_Exe_Types") = DBNull.Value 'This is not an executable
						rowemugames("DOSBox_Mount_Destination") = "C"   'This is to be mounted as the C drive

						If id_Emu_Games_Owner > 0 Then rowemugames("id_Emu_Games_Owner") = id_Emu_Games_Owner

						Me.DS_ML.tbl_Emu_Games.Rows.Add(rowemugames)

						'If Not dict_Have.ContainsKey(filepath) Then
						'	dict_Have.Add(filepath, 0)
						'End If

						'TODO: Add DOSBox_Sub_Entries here - use arrFiles
						'Add_DOSBox_Packed_Game_SubEntries(main_fi.FullName, rowemugames)

						Rescan_DOSBox_Game(IIf(id_Emu_Games_Owner = 0, rowemugames("id_Emu_Games"), id_Emu_Games_Owner), tran, Me.DS_ML.tbl_Emu_Games)

						Return
					Else
						Return
					End If
				End Using
			End If
		End If

		'### Adding Install Media ###
		If mode = enm_DOSBoxAdd_Mode.Install_Media_Files Then
			If MKDXHelper.MessageBox("Select one or more Install Media files (i.e. CD Images, Floppy Images) for a single game in the next dialog. Do you want to continue?", "Add Games", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
				Return
			End If

			Dim sFiles As String() = MKNetLib.cls_MKFileSupport.OpenFileDialog("Open Install Media Files", "Images (*.iso;*.cue;*.img)|*.iso;*.cue;*.img", 0, "", TC.NZ(cls_Settings.GetSetting("Browse_Romfolder" & "_" & BS_Moby_Platforms.Current("URLPart")), ""), True, ParentForm:=Me)

			If sFiles.Length = 0 OrElse Not Alphaleonis.Win32.Filesystem.File.Exists(sFiles(0)) Then
				Return
			End If

			Dim rows_game As New ArrayList

			'Dim id_Emu_Games_Owner As Integer = 0

			Using frm_tag As New frm_Tag_Parser_Edit(Nothing, sFiles, Nothing, True, True, False) 'Adding DOS Game Media is always Multi Volume
				If frm_tag.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then
					Return
				End If
			End Using

			cls_Settings.SetSetting("Browse_Romfolder" & "_" & BS_Moby_Platforms.Current("URLPart"), Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(sFiles(0)))

			Dim bNeedCWD As Boolean = (id_Emu_Games_Owner = 0)

			'TODO: Do it the MultiVolume way
			'      Add all files into DS_ML.tbl_Emu_Games
			'      Call Group_Volumes
			'      Call Ensure_DOSBox_Working_Directory

			Dim row_head As DataRow = Nothing

			For Each sFile As String In sFiles
				If Alphaleonis.Win32.Filesystem.File.Exists(sFile) Then

					Dim rowsub As DataRow = Nothing
					rowsub = Me.DS_ML.tbl_Emu_Games.NewRow
					rowsub("created") = DateTime.Now

					If id_Emu_Games_Owner <> 0 Then
						rowsub("id_Emu_Games_Owner") = id_Emu_Games_Owner
					End If

					rowsub("Folder") = Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(sFile)
					rowsub("File") = Alphaleonis.Win32.Filesystem.Path.GetFileName(sFile)
					rowsub("InnerFile") = Alphaleonis.Win32.Filesystem.Path.GetFileName(sFile)
					rowsub("id_Moby_Platforms") = cmb_Platform.EditValue

					rowsub("id_DOSBox_Configs_Template") = Get_id_DOSBox_Templates_Default()
					rowsub("id_Rombase_DOSBox_Filetypes") = cls_Globals.enm_Rombase_DOSBox_Filetypes.iso  'CD Image
					rowsub("DOSBox_Mount_Destination") = "D"    'CD Images are to be mounted as the D drive

					'rowsub("Volume_Number") = 1
					Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
						frm_Tag_Parser_Edit.Apply_Filename_Tags(tran, rowsub, DS_ML.tbl_Emu_Games_Languages, DS_ML.tbl_Emu_Games_Regions, Nothing, True)
					End Using

					For Each row_game As DataRow In rows_game
						If TC.NZ(row_game("id_Rombase_DOSBox_Filetypes"), 0) = cls_Globals.enm_Rombase_DOSBox_Filetypes.iso Then
							If TC.NZ(rowsub("Volume_Number"), 0) < TC.NZ(row_game("Volume_Number"), 0) + 1 Then
								rowsub("Volume_Number") = TC.NZ(row_game("Volume_Number"), 0) + 1
							End If
						End If
					Next

					rows_game.Add(rowsub)

					If id_Emu_Games_Owner = 0 Then
						id_Emu_Games_Owner = rowsub("id_Emu_Games")
						row_head = rowsub
					End If

					Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
						frm_Tag_Parser_Edit.Apply_Filename_Tags(tran, rowsub, DS_ML.tbl_Emu_Games_Languages, DS_ML.tbl_Emu_Games_Regions, Nothing, TC.NZ(Me.BS_Moby_Platforms.Current("MultiVolume"), False))
					End Using

					Me.DS_ML.tbl_Emu_Games.Rows.Add(rowsub)

				End If
			Next

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				If row_head IsNot Nothing Then
					frm_Tag_Parser_Edit.Apply_Filename_Tags(tran, row_head, DS_ML.tbl_Emu_Games_Languages, DS_ML.tbl_Emu_Games_Regions, Nothing, TC.NZ(Me.BS_Moby_Platforms.Current("MultiVolume"), False))
				End If
			End Using

			If bNeedCWD AndAlso id_Emu_Games_Owner <> 0 Then
				'Add WorkingDirectory for the game (create if possible)
				Dim cwd As String = cls_Settings.Get_DOSBox_CWD & "\" & Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(sFiles(0))
				If Not Alphaleonis.Win32.Filesystem.Directory.Exists(cwd) Then
					Try
						Alphaleonis.Win32.Filesystem.Directory.CreateDirectory(cwd)

					Catch ex As Exception
						MKDXHelper.ExceptionMessageBox(ex, "There has been an error while creating the working directory '" & cwd & "'. The error was: ", "Creating Working Directory")
						Return
					End Try
				End If

				Dim rowcwd As DataRow
				rowcwd = Me.DS_ML.tbl_Emu_Games.NewRow
				rowcwd("created") = DateTime.Now

				rowcwd("id_Emu_Games_Owner") = id_Emu_Games_Owner
				rowcwd("Folder") = cwd
				rowcwd("File") = cwd.Replace(Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(cwd) & "\", "")
				rowcwd("id_Moby_Platforms") = cmb_Platform.EditValue

				rowcwd("id_DOSBox_Configs_Template") = Get_id_DOSBox_Templates_Default()
				rowcwd("id_Rombase_DOSBox_Filetypes") = cls_Globals.enm_Rombase_DOSBox_Filetypes.cwd  'Working Directory
				rowcwd("DOSBox_Mount_Destination") = "C"    'This is to be mounted as the C drive

				rows_game.Add(rowcwd)


				Me.DS_ML.tbl_Emu_Games.Rows.Add(rowcwd)

				id_Emu_Games_Owner = rowcwd("id_Emu_Games")
			End If
		End If
	End Sub

	Private Sub cmb_DOSBox_Volume_Number_ButtonPressed(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_DOSBox_Volume_Number.ButtonPressed
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
			BS_DOSBox_Files_and_Folders.Current("Volume_Number") = DBNull.Value
			cmb_DOSBox_Volume_Number.EditValue = DBNull.Value
		End If
	End Sub

	Private Sub cmb_DOSBox_Mount_Destination_ButtonPressed(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_DOSBox_Mount_Destination.ButtonPressed
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
			BS_DOSBox_Files_and_Folders.Current("DOSBox_Mount_Destination") = DBNull.Value
			cmb_DOSBox_Mount_Destination.EditValue = DBNull.Value
		End If
	End Sub

	Private Sub cmb_DOSBox_Exe_Type_ButtonPressed(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_DOSBox_Exe_Type.ButtonPressed
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
			BS_DOSBox_Files_and_Folders.Current("id_Rombase_DOSBox_Exe_Types") = DBNull.Value
			cmb_DOSBox_Exe_Type.EditValue = DBNull.Value
		End If
	End Sub

	''' <summary>
	''' Rescan DOSBox Game files within Working Directories in case they have been changed through unpacking or installing OR the Working Directory just has been added within the Rom Manager
	''' </summary>
	''' <remarks></remarks>
	Public Shared Sub Rescan_DOSBox_Game(ByVal id_Emu_Games As Integer, Optional ByVal tran As SQLite.SQLiteTransaction = Nothing, Optional ByVal tbl_Emu_Games As DS_ML.tbl_Emu_GamesDataTable = Nothing, Optional ByVal UseRombaseCache As Boolean = False, Optional ByRef tbl_Emu_Games_Languages As DS_ML.tbl_Emu_Games_LanguagesDataTable = Nothing, Optional ByRef tbl_Emu_Games_Regions As DS_ML.tbl_Emu_Games_RegionsDataTable = Nothing, Optional ByVal prg As MKNetDXLib.cls_MKDXBaseform_Progress_Helper = Nothing)
		Dim bTran As Boolean = False
		If tran Is Nothing Then
			bTran = True
			tran = cls_Globals.Conn.BeginTransaction
		End If

		Dim bUseDB As Boolean = True
		If tbl_Emu_Games IsNot Nothing Then
			bUseDB = False
		End If

		Try
			'Reload the file list
			Dim dt_Files As New DS_ML.tbl_Emu_GamesDataTable

			If bUseDB Then
				DS_ML.Fill_src_frm_Rom_Manager_Emu_Games(tran, dt_Files, 0, id_Emu_Games, id_Emu_Games)
			Else
				dt_Files = tbl_Emu_Games
			End If

			'Get the "main" Emu Games entry
			Dim row_Head As DataRow = Nothing
			Dim rows_Head As DS_ML.tbl_Emu_GamesRow() = dt_Files.Select("id_Emu_Games_Owner IS NULL AND id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games))

			If rows_Head.Length > 1 Then
				If prg IsNot Nothing Then prg.Hide = True
				Dim bWaitCursor As Boolean = Cursor.Current = Cursors.WaitCursor
				Cursor.Current = Cursors.Default
				MKDXHelper.MessageBox("Error while rescanning a DOSBox game: cannot determine main game entry.", "Error while rescanning DOSBox game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				If bWaitCursor Then Cursor.Current = Cursors.WaitCursor
				If prg IsNot Nothing Then prg.Hide = False

				If bTran Then
					tran.Rollback()
					Return
				End If
			End If

			If rows_Head.Length = 0 Then
				If prg IsNot Nothing Then prg.Hide = True
				Dim bWaitCursor As Boolean = Cursor.Current = Cursors.WaitCursor
				Cursor.Current = Cursors.Default
				MKDXHelper.MessageBox("Error while rescanning a DOSBox game: cannot find main game entry.", "Error while rescanning DOSBox game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				If prg IsNot Nothing Then prg.Hide = False
				If bWaitCursor Then Cursor.Current = Cursors.WaitCursor

				If bTran Then
					tran.Rollback()
					Return
				End If
			End If

			If tbl_Emu_Games_Languages IsNot Nothing AndAlso tbl_Emu_Games_Regions IsNot Nothing Then
				frm_Tag_Parser_Edit.Apply_Filename_Tags(tran, rows_Head(0), tbl_Emu_Games_Languages, tbl_Emu_Games_Regions, Nothing, True)
			End If

			row_Head = rows_Head(0)

			For Each row_CWD As DataRow In dt_Files.Select("id_Rombase_DOSBox_Filetypes = " & TC.getSQLFormat(cls_Globals.enm_Rombase_DOSBox_Filetypes.cwd) & " AND DOSBox_Mount_Destination IS NOT NULL AND (id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " OR id_Emu_Games_Owner = " & TC.getSQLFormat(id_Emu_Games) & ")")
				'Seach all files within the CWD
				If Not Alphaleonis.Win32.Filesystem.Directory.Exists(row_CWD("Folder")) Then Continue For 'CWD should exist

				Dim fs As New MKNetLib.cls_MKFileSearch(row_CWD("Folder").ToString, "*.*", "*", {".exe", ".bat", ".com", ".cue", ".iso"}) 'Only executable files and cd images are interesting
				fs.Search()

				Dim found_files As ArrayList = fs.Files

				For Each fi As Alphaleonis.Win32.Filesystem.FileInfo In fs.Files
					'Skip if file is already known
					If dt_Files.Select("InnerFile = " & TC.getSQLFormat(fi.Name) & " AND Folder = " & TC.getSQLFormat(fi.Directory.FullName) & " AND (id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " OR id_Emu_Games_Owner = " & TC.getSQLFormat(id_Emu_Games) & ")").Length > 0 Then Continue For

					Dim row_file As DataRow = Nothing
					Dim rows_file As DataRow() = dt_Files.Select("InnerFile LIKE '%" & fi.Name.Replace("'", "''") & "' AND Size = " & fi.Length & " AND InnerFile <> File" & " AND (id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " OR id_Emu_Games_Owner = " & TC.getSQLFormat(id_Emu_Games) & ")")

					'Skip if more equivalent files are found (actually don't skip, just use the first equivalent file)
					'If rows_file.Length > 1 Then Continue For

					Dim bEquivalentFileHandled As Boolean = False

					If rows_file.Length > 0 Then '= 1 Then
						'Equivalent file is found (check if it is inside a packed file and update if necessary)
						row_file = rows_file(0)

						Dim rows_file_owner As DataRow() = dt_Files.Select("File = " & TC.getSQLFormat(row_file("File")) & " AND InnerFile = " & TC.getSQLFormat(row_file("File")) & " AND (id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " OR id_Emu_Games_Owner = " & TC.getSQLFormat(id_Emu_Games) & ")") 'If it is within a packed file, then there is also a DataRow for that packed file

						If rows_file_owner.Length = 1 Then
							If TC.NZ(rows_file_owner(0)("id_Rombase_DOSBox_Filetypes"), 0) = cls_Globals.enm_Rombase_DOSBox_Filetypes.zip Then  'FileType = Packed Content
								If bUseDB Then  'This should only be a constellation in the bUseDB case but just in case we'll check for it anyways
									'The equivalent file is a file within a packed file -> UPDATE to the file within the working directory
									Dim crc As String = MKNetLib.cls_MKFileSupport.CRC32Hash(fi.FullName)
									Dim md5 As String = MKNetLib.cls_MKFileSupport.MD5Hash(fi.FullName)
									Dim sha1 As String = MKNetLib.cls_MKFileSupport.SHA1Hash(fi.FullName)

									Dim sSQL As String = ""
									sSQL = "UPDATE tbl_Emu_Games" & ControlChars.CrLf
									sSQL &= "SET" & ControlChars.CrLf
									sSQL &= "	Folder = " & TC.getSQLFormat(fi.DirectoryName) & ControlChars.CrLf
									sSQL &= "	, File = " & TC.getSQLFormat(fi.Name) & ControlChars.CrLf
									sSQL &= "	, InnerFile = " & TC.getSQLFormat(fi.Name) & ControlChars.CrLf
									sSQL &= "	, CRC32 = " & TC.getSQLFormat(crc) & ControlChars.CrLf
									sSQL &= "	, SHA1 = " & TC.getSQLFormat(sha1) & ControlChars.CrLf
									sSQL &= "	, MD5 = " & TC.getSQLFormat(md5) & ControlChars.CrLf
									sSQL &= "WHERE id_Emu_Games = " & TC.getSQLFormat(row_file("id_Emu_Games"))

									bEquivalentFileHandled = DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
								End If
							End If
						End If
					End If

					If Not bEquivalentFileHandled Then
						'No equivalent file is found -> add new
						Dim crc As String = MKNetLib.cls_MKFileSupport.CRC32Hash(fi.FullName)
						Dim md5 As String = MKNetLib.cls_MKFileSupport.MD5Hash(fi.FullName)
						Dim sha1 As String = MKNetLib.cls_MKFileSupport.SHA1Hash(fi.FullName)

						Dim id_Rombase_DOSBox_Exe_Types As Object = DBNull.Value  'TODO: check Rombase if an exe type is available

						If bUseDB Then
							Dim sSQL As String = ""
							sSQL = "INSERT INTO tbl_Emu_Games" & ControlChars.CrLf
							sSQL &= "(" & ControlChars.CrLf
							sSQL &= "	id_Emu_Games_Owner" & ControlChars.CrLf
							sSQL &= "	, id_Rombase_DOSBox_Filetypes" & ControlChars.CrLf
							sSQL &= "	, id_Rombase_DOSBox_Exe_Types" & ControlChars.CrLf
							sSQL &= "	, Moby_Games_URLPart" & ControlChars.CrLf
							sSQL &= "	, id_Moby_Platforms" & ControlChars.CrLf
							sSQL &= "	, Folder" & ControlChars.CrLf
							sSQL &= "	, File" & ControlChars.CrLf
							sSQL &= "	, InnerFile" & ControlChars.CrLf
							sSQL &= "	, Size" & ControlChars.CrLf
							sSQL &= "	, CRC32" & ControlChars.CrLf
							sSQL &= "	, SHA1" & ControlChars.CrLf
							sSQL &= "	, MD5" & ControlChars.CrLf
							sSQL &= ")" & ControlChars.CrLf
							sSQL &= "VALUES" & ControlChars.CrLf
							sSQL &= "(" & ControlChars.CrLf
							sSQL &= TC.getSQLFormat(id_Emu_Games) & ControlChars.CrLf 'id_Emu_Games_Owner

							If {".exe", ".bat", ".com"}.Contains(fi.Extension.ToLower) Then
								sSQL &= "	, " & TC.getSQLFormat(3) & ControlChars.CrLf  'File_Type = 3 (Executable)
							Else
								sSQL &= "	, " & TC.getSQLFormat(4) & ControlChars.CrLf  'File_Type = 4 (CD Image)
							End If

							sSQL &= "	, " & TC.getSQLFormat(id_Rombase_DOSBox_Exe_Types) & ControlChars.CrLf  'TODO: id_Rombase_DOSBox_Exe_Types (check Rombase if an exe type is available)
							sSQL &= "	, " & TC.getSQLFormat(row_Head("Moby_Games_URLPart")) & ControlChars.CrLf
							sSQL &= "	, " & TC.getSQLFormat(row_Head("id_Moby_Platforms")) & ControlChars.CrLf
							sSQL &= "	, " & TC.getSQLFormat(fi.DirectoryName) & ControlChars.CrLf
							sSQL &= "	, " & TC.getSQLFormat(fi.Name) & ControlChars.CrLf  'File
							sSQL &= "	, " & TC.getSQLFormat(fi.Name) & ControlChars.CrLf  'InnerFile
							sSQL &= "	, " & TC.getSQLFormat(fi.Length) & ControlChars.CrLf
							sSQL &= "	, " & TC.getSQLFormat(crc) & ControlChars.CrLf
							sSQL &= "	, " & TC.getSQLFormat(sha1) & ControlChars.CrLf
							sSQL &= "	, " & TC.getSQLFormat(md5) & ControlChars.CrLf
							sSQL &= "); SELECT last_insert_rowid()" & ControlChars.CrLf

							Dim id_Emu_Games_New As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, sSQL, tran), 0)

							If id_Emu_Games_New <> 0 Then
								If TC.NZ(id_Rombase_DOSBox_Exe_Types, 0) <> 0 Then
									Dim sSQLUpdate As String = ""
									sSQLUpdate &= "UPDATE tbl_Emu_Games" & ControlChars.CrLf
									sSQLUpdate &= "SET id_Rombase_DOSBox_Exe_Types = NULL"
									sSQLUpdate &= "WHERE (id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " OR id_Emu_Games_Owner = " & TC.getSQLFormat(id_Emu_Games) & ")" & ControlChars.CrLf
									sSQLUpdate &= "	AND id_Rombase_DOSBox_Exe_Types = " & TC.getSQLFormat(id_Rombase_DOSBox_Exe_Types) & ControlChars.CrLf
									sSQLUpdate &= "	AND id_Emu_Games <> " & TC.getSQLFormat(id_Emu_Games_New)

									DataAccess.FireProcedure(tran.Connection, 0, sSQLUpdate, tran)
								End If
							End If
						Else
							'Not bUseDB
							Dim row As DataRow = tbl_Emu_Games.NewRow
							row("created") = DateTime.Now

							row("id_Emu_Games_Owner") = id_Emu_Games

							If {".exe", ".bat", ".com"}.Contains(fi.Extension.ToLower) Then
								row("id_Rombase_DOSBox_Filetypes") = cls_Globals.enm_Rombase_DOSBox_Filetypes.exe
							ElseIf {".cue", ".iso"}.Contains(fi.Extension.ToLower) Then
								row("id_Rombase_DOSBox_Filetypes") = cls_Globals.enm_Rombase_DOSBox_Filetypes.iso
								'TODO: Volume Number, Mount type

								row("DOSBox_Mount_Destination") = "D" 'CDs are mounted as D drive
								row("Volume_Number") = 1

								For Each row_game As DataRow In dt_Files.Select("id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " OR id_Emu_Games_Owner = " & TC.getSQLFormat(id_Emu_Games))
									If TC.NZ(row_game("id_Rombase_DOSBox_Filetypes"), 0) = cls_Globals.enm_Rombase_DOSBox_Filetypes.iso Then
										If TC.NZ(row("Volume_Number"), 0) < TC.NZ(row("Volume_Number"), 0) + 1 Then
											row("Volume_Number") = TC.NZ(row_game("Volume_Number"), 0) + 1
										End If
									End If
								Next
							End If

							row("id_Rombase_DOSBox_Exe_Types") = id_Rombase_DOSBox_Exe_Types
							row("Moby_Games_URLPart") = row_Head("Moby_Games_URLPart")
							row("id_Moby_Platforms") = row_Head("id_Moby_Platforms")
							row("Folder") = fi.DirectoryName
							row("File") = fi.Name
							row("InnerFile") = fi.Name
							row("Size") = fi.Length
							row("CRC32") = crc
							row("SHA1") = sha1
							row("MD5") = md5


							tbl_Emu_Games.Rows.Add(row)
						End If
					End If
				Next
			Next

			'Find and apply Rombase stuff
			'Get_and_Apply_id_Rombase(tran, tbl_Emu_Games, id_Emu_Games, 3)
			Get_and_Apply_id_Rombase(tran, tbl_Emu_Games, id_Emu_Games, UseCache:=UseRombaseCache)
			If bUseDB Then
				'Write to DB because Get_and_Apply_id_Rombase will only modifiy tbl_Emu_Games
				For Each row_Emu_Games As DataRow In tbl_Emu_Games.Select("(id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " OR id_Emu_Games_Owner = " & TC.getSQLFormat(id_Emu_Games) & ") AND (id_Rombase IS NOT NULL OR Moby_Games_URLPart IS NOT NULL)")
					DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Emu_Games SET id_Rombase = " & TC.getSQLFormat(row_Emu_Games("id_Rombase")) & ", Moby_Games_URLPart = " & TC.getSQLFormat(row_Emu_Games("Moby_Games_URLPart")) & " WHERE id_Emu_Games = " & TC.getSQLFormat(row_Emu_Games("id_Emu_Games")), tran)
				Next
			End If

			If bUseDB AndAlso bTran Then tran.Commit()
		Catch ex As Exception
			If prg IsNot Nothing Then prg.Hide = True
			Dim bWaitCursor As Boolean = Cursor.Current = Cursors.WaitCursor
			Cursor.Current = Cursors.Default
			MKDXHelper.ExceptionMessageBox(ex, "There has been an error while rescanning a DOSBox game. The error was: ", "Error while rescanning DOSBox game")
			If prg IsNot Nothing Then prg.Hide = False
			If bWaitCursor Then Cursor.Current = Cursors.WaitCursor

			If bTran Then
				If ex.GetType IsNot GetType(System.Data.SQLite.SQLiteException) Then
					tran.Rollback()
				End If
			Else
				Throw ex
			End If
		End Try
	End Sub

	Public Class cls_Get_and_Apply_id_Rombase_Emu_Games_Cache_Item
		Public _row_Emu_Games As DataRow
		Public _rows_Emu_Games_Children As New ArrayList

		Public Sub New(ByRef row_Emu_Games As DataRow, ByVal is_Owner As Boolean)
			If is_Owner Then
				_row_Emu_Games = row_Emu_Games
			Else
				_rows_Emu_Games_Children.Add(row_Emu_Games)
			End If
		End Sub
	End Class

	Public Shared Get_and_Apply_id_Rombase_Emu_Games_Cache As Dictionary(Of Int64, cls_Get_and_Apply_id_Rombase_Emu_Games_Cache_Item)

	''' <summary>
	''' Get id_Rombase by EmuGame's files (given by id_Emu_Games_Owner)
	''' If dt_Emu_Games is Nothing, all Data gets loaded from DB and applied to DB
	''' If dt_Emu_Games isnot Nothing, the Data in dt_Emu_Games gets updated if possible
	''' </summary>
	''' <param name="tran"></param>
	''' <param name="dt_Emu_Games"></param>
	''' <param name="id_Emu_Games_Owner"></param>
	''' <returns>id_Rombase (Owner)</returns>
	''' <remarks></remarks>
	Public Shared Function Get_and_Apply_id_Rombase(ByRef tran As SQLite.SQLiteTransaction, ByRef dt_Emu_Games As DataTable, ByVal id_Emu_Games_Owner As Int64, Optional ByVal id_Rombase_DOSBox_Filetypes As Integer = -1, Optional ByVal UseCache As Boolean = False, Optional ByVal EmuGames_Cache As cls_Get_and_Apply_id_Rombase_Emu_Games_Cache_Item = Nothing) As Int64
		Dim bUseDB As Boolean = dt_Emu_Games Is Nothing AndAlso tran IsNot Nothing

		Dim id_Rombase_Owner As Int64 = 0

		If id_Emu_Games_Owner = 0 Then Return 0

		If bUseDB Then
			dt_Emu_Games = New DS_ML.tbl_Emu_GamesDataTable
			DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT id_Emu_Games, id_Emu_Games_Owner, id_Rombase_DOSBox_Filetypes, size, crc32, NULL AS ROMBASE_id_Moby_Platforms FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_Owner) & " OR (id_Emu_Games_Owner = " & TC.getSQLFormat(id_Emu_Games_Owner) & IIf(id_Rombase_DOSBox_Filetypes = -1, ")", " AND id_Rombase_DOSBox_Filetypes = " & id_Rombase_DOSBox_Filetypes & ")"), dt_Emu_Games, tran)
		End If

		'If UseCache AndAlso Get_and_Apply_id_Rombase_Emu_Games_Cache Is Nothing Then
		'	Get_and_Apply_id_Rombase_Emu_Games_Cache = New Dictionary(Of Int64, cls_Get_and_Apply_id_Rombase_Emu_Games_Cache_Item)
		'	For Each row As DataRow In dt_Emu_Games.Rows
		'		Dim id As Int64 = TC.NZ(row("id_Emu_Games_Owner"), 0)
		'		Dim is_Owner As Boolean = False
		'		If id = 0 Then
		'			id = TC.NZ(row("id_Emu_Games"), 0)
		'			is_Owner = True
		'		End If

		'		If Not Get_and_Apply_id_Rombase_Emu_Games_Cache.ContainsKey(id) Then
		'			Get_and_Apply_id_Rombase_Emu_Games_Cache(id) = New cls_Get_and_Apply_id_Rombase_Emu_Games_Cache_Item(row, is_Owner)
		'		Else
		'			If is_Owner Then
		'				Get_and_Apply_id_Rombase_Emu_Games_Cache(id)._row_Emu_Games = row
		'			Else
		'				Get_and_Apply_id_Rombase_Emu_Games_Cache(id)._rows_Emu_Games_Children.Add(row)
		'			End If
		'		End If
		'	Next
		'End If

		Dim row_Emu_Games_Owner As DataRow = Nothing

		Try
			If EmuGames_Cache IsNot Nothing Then
				row_Emu_Games_Owner = EmuGames_Cache._row_Emu_Games
			Else
				row_Emu_Games_Owner = dt_Emu_Games.Select("id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_Owner))(0)
			End If

			Dim id_Rombase_Owner_Chosen As Int64 = 0

			If TC.NZ(row_Emu_Games_Owner("id_Rombase"), 0) > 0 Then
				'Rombase is already found
				'Return row_Emu_Games_Owner("id_Rombase")
				id_Rombase_Owner_Chosen = row_Emu_Games_Owner("id_Rombase")
			End If

			Dim ar_Emu_Games_Children As ArrayList
			If EmuGames_Cache IsNot Nothing Then
				ar_Emu_Games_Children = EmuGames_Cache._rows_Emu_Games_Children
			Else
				ar_Emu_Games_Children = New ArrayList
				Dim rows_Emu_Games_Children() As DataRow = dt_Emu_Games.Select("id_Emu_Games_Owner = " & TC.getSQLFormat(id_Emu_Games_Owner) & IIf(id_Rombase_DOSBox_Filetypes = -1, "", " AND id_Rombase_DOSBox_Filetypes = " & id_Rombase_DOSBox_Filetypes) & " AND size IS NOT NULL AND crc32 IS NOT NULL")
				For Each row_Emu_Games_Children As DataRow In rows_Emu_Games_Children
					ar_Emu_Games_Children.Add(row_Emu_Games_Children)
				Next
			End If

			Dim iNumValidChildren As Integer = 0
			For Each row_Emu_Games_Children As DataRow In ar_Emu_Games_Children
				If Not TC.IsNullNothingOrEmpty(row_Emu_Games_Children("size")) AndAlso Not TC.IsNullNothingOrEmpty(row_Emu_Games_Children("crc32")) Then
					iNumValidChildren += 1
				End If
			Next

			Dim dt_Rombase_Hits As New DataTable
			dt_Rombase_Hits.Columns.Add("id_Rombase", GetType(Int64))
			dt_Rombase_Hits.Columns.Add("id_Rombase_Owner", GetType(Int64))
			dt_Rombase_Hits.Columns.Add("id_Emu_Games", GetType(Int64))
			dt_Rombase_Hits.Columns.Add("Hits", GetType(Integer))
			dt_Rombase_Hits.Columns.Add("Ratio", GetType(Integer))

			If id_Rombase_Owner_Chosen = 0 Then
				'id_Rombase not found for the row_Emu_Games -> search for a fitting rombase entry

				Dim count_Emu_Games_Files = iNumValidChildren + IIf(TC.NZ(row_Emu_Games_Owner("size"), 0) > 0 And TC.NZ(row_Emu_Games_Owner("crc32"), "") <> "", 1, 0)

				Dim dt_Owner_Rombase_Entries As DataTable = get_All_id_Rombase(tran, row_Emu_Games_Owner("size"), row_Emu_Games_Owner("crc32"), id_Rombase_DOSBox_Filetypes, UseCache)
				If dt_Owner_Rombase_Entries IsNot Nothing AndAlso dt_Owner_Rombase_Entries.Rows.Count > 0 Then
					'add to dt_Rombase_Hits
					For Each row_Owner_Rombase_Entry As DataRow In dt_Owner_Rombase_Entries.Rows
						'Rombase Owner
						If TC.NZ(row_Owner_Rombase_Entry("id_Rombase"), 0) > 0 AndAlso TC.NZ(row_Owner_Rombase_Entry("id_Rombase_Owner"), 0) = 0 Then
							If dt_Rombase_Hits.Select("id_Rombase = " & TC.getSQLFormat(row_Owner_Rombase_Entry("id_Rombase"))).Length = 0 Then
								'add
								Dim row_Rombase_Hits As DataRow = dt_Rombase_Hits.Rows.Add
								row_Rombase_Hits("id_Rombase") = row_Owner_Rombase_Entry("id_Rombase")
								row_Rombase_Hits("id_Emu_Games") = row_Emu_Games_Owner("id_Emu_Games")
								row_Rombase_Hits("Hits") = 1
								'dt_Rombase_Hits.Rows.Add(row_Rombase_Hits)
							Else
								'increase
								Dim row_Rombase_Hits As DataRow = dt_Rombase_Hits.Select("id_Rombase = " & TC.getSQLFormat(row_Owner_Rombase_Entry("id_Rombase")))(0)
								row_Rombase_Hits("Hits") += 1
							End If
						End If

						'Rombase Child
						If TC.NZ(row_Owner_Rombase_Entry("id_Rombase"), 0) > 0 AndAlso TC.NZ(row_Owner_Rombase_Entry("id_Rombase_Owner"), 0) > 0 Then
							If dt_Rombase_Hits.Select("id_Rombase = " & TC.getSQLFormat(row_Owner_Rombase_Entry("id_Rombase"))).Length = 0 Then
								'add
								Dim row_Rombase_Hits As DataRow = dt_Rombase_Hits.Rows.Add()
								row_Rombase_Hits("id_Rombase") = row_Owner_Rombase_Entry("id_Rombase")
								row_Rombase_Hits("id_Rombase_Owner") = row_Owner_Rombase_Entry("id_Rombase_Owner")
								row_Rombase_Hits("Hits") = 1
								'dt_Rombase_Hits.Rows.Add(row_Rombase_Hits)
							Else
								'increase
								Dim row_Rombase_Hits As DataRow = dt_Rombase_Hits.Select("id_Rombase = " & TC.getSQLFormat(row_Owner_Rombase_Entry("id_Rombase")))(0)
								row_Rombase_Hits("Hits") += 1
							End If
						End If
					Next

					Dim i2 As Integer = 0
				End If

				For Each row_Emu_Games_Children As DataRow In ar_Emu_Games_Children
					Dim dt_Child_Rombase_Entries As DataTable = get_All_id_Rombase(tran, row_Emu_Games_Children("size"), row_Emu_Games_Children("crc32"), id_Rombase_DOSBox_Filetypes, UseCache)

					If dt_Child_Rombase_Entries IsNot Nothing AndAlso dt_Child_Rombase_Entries.Rows.Count > 0 Then
						For Each row_Child_Rombase_Entry As DataRow In dt_Child_Rombase_Entries.Rows
							'Rombase Owner
							If TC.NZ(row_Child_Rombase_Entry("id_Rombase_Owner"), 0) > 0 Then
								If dt_Rombase_Hits.Select("id_Rombase = " & TC.getSQLFormat(row_Child_Rombase_Entry("id_Rombase_Owner"))).Length = 0 Then
									'add
									Dim row_Rombase_Hits As DataRow = dt_Rombase_Hits.Rows.Add
									row_Rombase_Hits("id_Rombase") = row_Child_Rombase_Entry("id_Rombase_Owner")
									row_Rombase_Hits("id_Emu_Games") = row_Emu_Games_Owner("id_Emu_Games")
									row_Rombase_Hits("Hits") = 1
									'dt_Rombase_Hits.Rows.Add(row_Rombase_Hits)
								Else
									'increase
									Dim row_Rombase_Hits As DataRow = dt_Rombase_Hits.Select("id_Rombase = " & TC.getSQLFormat(row_Child_Rombase_Entry("id_Rombase_Owner")))(0)
									row_Rombase_Hits("Hits") += 1
								End If
							End If

							'Rombase Child
							If TC.NZ(row_Child_Rombase_Entry("id_Rombase"), 0) > 0 Then
								If dt_Rombase_Hits.Select("id_Rombase = " & TC.getSQLFormat(row_Child_Rombase_Entry("id_Rombase"))).Length = 0 Then
									'add
									Dim row_Rombase_Hits As DataRow = dt_Rombase_Hits.Rows.Add()
									row_Rombase_Hits("id_Rombase") = row_Child_Rombase_Entry("id_Rombase")
									row_Rombase_Hits("id_Rombase_Owner") = row_Child_Rombase_Entry("id_Rombase_Owner")
									row_Rombase_Hits("id_Emu_Games") = row_Emu_Games_Children("id_Emu_Games")
									row_Rombase_Hits("Hits") = 1
									'dt_Rombase_Hits.Rows.Add(row_Rombase_Hits)
								Else
									'increase
									Dim row_Rombase_Hits As DataRow = dt_Rombase_Hits.Select("id_Rombase = " & TC.getSQLFormat(row_Child_Rombase_Entry("id_Rombase")))(0)
									row_Rombase_Hits("Hits") += 1
								End If
							End If
						Next

					End If
				Next

				Dim rows_Rombase_Owner_Chosen() As DataRow = dt_Rombase_Hits.Select("id_Rombase_Owner IS NULL AND id_Rombase IS NOT NULL", "Hits DESC")

				Dim max_Hits As Integer = 0
				Dim count_Rombase_Files = 0

				If rows_Rombase_Owner_Chosen.Length > 0 Then
					For Each row_Rombase_Owner_Chosen In rows_Rombase_Owner_Chosen
						max_Hits = rows_Rombase_Owner_Chosen(0)("Hits")
						Dim ratio_Emu_Games = max_Hits / count_Emu_Games_Files

						If ratio_Emu_Games >= 0.8 Then
							id_Rombase_Owner_Chosen = rows_Rombase_Owner_Chosen(0)("id_Rombase")

							If UseCache Then
								count_Rombase_Files = get_All_id_Rombase_Owner_Count(id_Rombase_Owner_Chosen)
							Else
								count_Rombase_Files = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT COUNT(1) FROM rombase.tbl_Rombase WHERE (id_Rombase = " & TC.getSQLFormat(id_Rombase_Owner_Chosen) & " OR id_Rombase_Owner = " & TC.getSQLFormat(id_Rombase_Owner_Chosen) & ") AND size IS NOT NULL AND crc IS NOT NULL " & IIf(id_Rombase_DOSBox_Filetypes = -1, "", " AND id_Rombase_DOSBox_FileTypes = " & id_Rombase_DOSBox_Filetypes), tran), 0.0)
							End If

							Dim ratio_Rombase = max_Hits / count_Rombase_Files

							If ratio_Rombase < 0.8 Then
								'Hit/CountFiles ratio must be >= 0.8
								id_Rombase_Owner_Chosen = 0
							Else
								Exit For 'id_Rombase_Owner_Chosen is OK
							End If
						End If
					Next
				End If
			End If

			'id_Rombase_Owner_Chosen was either already applied to our row_Emu_Games or it has been found by searching
			If id_Rombase_Owner_Chosen > 0 Then
				Dim oMoby_Games_URLPart As Object = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT Moby_Games_URLPart FROM tbl_Rombase WHERE id_Rombase = " & TC.getSQLFormat(id_Rombase_Owner_Chosen), tran)
				Dim oROMBASE_id_Moby_Platforms As Object = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Platforms FROM tbl_Rombase WHERE id_Rombase = " & TC.getSQLFormat(id_Rombase_Owner_Chosen), tran)

				row_Emu_Games_Owner("id_Rombase") = id_Rombase_Owner_Chosen

				If TC.NZ(oMoby_Games_URLPart, "").Length > 0 Then
					row_Emu_Games_Owner("Moby_Games_URLPart") = oMoby_Games_URLPart
					row_Emu_Games_Owner("Name") = DBNull.Value
					row_Emu_Games_Owner("Name_USR") = DBNull.Value
					row_Emu_Games_Owner("Publisher_USR") = DBNull.Value
				End If

				If TC.NZ(oROMBASE_id_Moby_Platforms, 0) > 0 Then
					row_Emu_Games_Owner("ROMBASE_id_Moby_Platforms") = oROMBASE_id_Moby_Platforms
				End If

				For Each row_Emu_Games_Children As DataRow In ar_Emu_Games_Children
					If TC.NZ(oMoby_Games_URLPart, "").Length > 0 Then
						row_Emu_Games_Children("Moby_Games_URLPart") = oMoby_Games_URLPart
						row_Emu_Games_Owner("Name") = DBNull.Value
						row_Emu_Games_Owner("Publisher") = DBNull.Value
						row_Emu_Games_Owner("Name_USR") = DBNull.Value
						row_Emu_Games_Owner("Publisher_USR") = DBNull.Value
					End If

					If TC.NZ(oROMBASE_id_Moby_Platforms, 0) > 0 Then
						row_Emu_Games_Children("ROMBASE_id_Moby_Platforms") = oROMBASE_id_Moby_Platforms
					End If

					Dim rows_Rombase_Children As DataRow() = dt_Rombase_Hits.Select("id_Rombase_Owner = " & TC.getSQLFormat(id_Rombase_Owner_Chosen) & " AND id_Emu_Games = " & TC.getSQLFormat(row_Emu_Games_Children("id_Emu_Games")))
					If rows_Rombase_Children.Length = 1 Then
						row_Emu_Games_Children("id_Rombase") = rows_Rombase_Children(0)("id_Rombase")
					End If
				Next

				If bUseDB Then
					For Each row_Emu_Games As DataRow In dt_Emu_Games.Select("id_Rombase IS NOT NULL OR Moby_Games_URLPart IS NOT NULL")
						DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Emu_Games SET id_Rombase = " & TC.getSQLFormat(row_Emu_Games("id_Rombase")) & ", Moby_Games_URLPart = " & TC.getSQLFormat(row_Emu_Games("Moby_Games_URLPart")) & ", Name = " & TC.getSQLFormat(row_Emu_Games("Name")) & ", Name_USR = " & TC.getSQLFormat(row_Emu_Games("Name_USR")) & ", Publisher = " & TC.getSQLFormat(row_Emu_Games("Publisher")) & ", Publisher_USR = " & TC.getSQLFormat(row_Emu_Games("Publisher_USR")) & " WHERE id_Emu_Games = " & TC.getSQLFormat(row_Emu_Games("id_Emu_Games")), tran)
					Next
				End If

				id_Rombase_Owner = id_Rombase_Owner_Chosen
			End If

		Catch ex As Exception
			MKDXHelper.ExceptionMessageBox(ex, "Error while identifying Game: ", "")
		End Try

		Return id_Rombase_Owner
	End Function

	Private Class get_All_id_Rombase_Entry
		Public _id_Rombase_DOSBox_Filetypes As Object
		Public _id_Rombase As Object
		Public _id_Rombase_Owner As Object

		Public Sub New(id_Rombase_DOSBox_Filetypes As Object, id_Rombase As Object, id_Rombase_Owner As Object)
			_id_Rombase_DOSBox_Filetypes = id_Rombase_DOSBox_Filetypes
			_id_Rombase = id_Rombase
			_id_Rombase_Owner = id_Rombase_Owner
		End Sub
	End Class

	Public Shared get_All_id_Rombase_Cache As Dictionary(Of String, ArrayList)

	Public Shared get_All_id_Rombase_Owner_Count As Dictionary(Of Int64, Int32)

	Public Shared Function get_All_id_Rombase(ByRef tran As SQLite.SQLiteTransaction, ByVal Size As Object, ByVal CRC32 As Object, Optional ByVal id_Rombase_DOSBox_Filetypes As Integer = -1, Optional ByVal UseCache As Boolean = False) As DataTable
		If TC.IsNullNothingOrEmpty(Size) OrElse TC.IsNullNothingOrEmpty(CRC32) Then
			Return Nothing
		End If

		If UseCache Then
			If get_All_id_Rombase_Cache Is Nothing Then
				get_All_id_Rombase_Cache = New Dictionary(Of String, ArrayList)
				get_All_id_Rombase_Owner_Count = New Dictionary(Of Int64, Integer)

				Dim dt_Cache As DataTable = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT id_Rombase, id_Rombase_Owner, size, crc, id_Rombase_DOSBox_Filetypes FROM rombase.tbl_Rombase WHERE size IS NOT NULL AND crc IS NOT NULL " & IIf(id_Rombase_DOSBox_Filetypes = -1, "", " AND id_Rombase_DOSBox_FileTypes = " & id_Rombase_DOSBox_Filetypes), Nothing, tran)

				For Each row_Cache As DataRow In dt_Cache.Rows
					Dim entry As New get_All_id_Rombase_Entry(row_Cache("id_Rombase_DOSBox_Filetypes"), row_Cache("id_Rombase"), row_Cache("id_Rombase_Owner"))
					Dim key As String = row_Cache("size") & "|" & row_Cache("crc")
					If Not get_All_id_Rombase_Cache.ContainsKey(key) Then
						get_All_id_Rombase_Cache(key) = New ArrayList
					End If
					get_All_id_Rombase_Cache(key).Add(entry)

					Dim id_Rombase As Int64 = row_Cache("id_Rombase")
					If Not TC.IsNullNothingOrEmpty(row_Cache("id_Rombase_Owner")) Then id_Rombase = row_Cache("id_Rombase_Owner")
					If get_All_id_Rombase_Owner_Count.ContainsKey(id_Rombase) Then
						get_All_id_Rombase_Owner_Count(id_Rombase) += 1
					Else
						get_All_id_Rombase_Owner_Count(id_Rombase) = 1
					End If
				Next
			End If

			Dim current_key As String = Size.ToString & "|" & CRC32.ToString

			If get_All_id_Rombase_Cache.ContainsKey(current_key) Then
				Dim dt_Result As New DataTable()
				dt_Result.Columns.Add("id_Rombase", GetType(Int64))
				dt_Result.Columns.Add("id_Rombase_Owner", GetType(Int64))

				Dim ar_Result As ArrayList = get_All_id_Rombase_Cache(current_key)
				For Each result As get_All_id_Rombase_Entry In ar_Result
					Dim row_result As DataRow = dt_Result.NewRow
					row_result("id_Rombase") = result._id_Rombase
					row_result("id_Rombase_Owner") = result._id_Rombase_Owner
					dt_Result.Rows.Add(row_result)
				Next

				Return dt_Result
			Else
				Return Nothing
			End If
		Else
			Return DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT id_Rombase, id_Rombase_Owner FROM rombase.tbl_Rombase WHERE size = " & TC.getSQLFormat(Size) & " AND crc = " & TC.getSQLFormat(CRC32) & IIf(id_Rombase_DOSBox_Filetypes = -1, "", " AND id_Rombase_DOSBox_FileTypes = " & id_Rombase_DOSBox_Filetypes), Nothing, tran)
		End If
	End Function

	Private Sub bbi_Add_DOSBox_Game_Directory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Add_DOSBox_Game_Directory.ItemClick
		Add_DOSBox_Games(enm_DOSBoxAdd_Mode.Installed_in_Directory)
	End Sub

	Private Sub bbi_Add_DOSBox_Game_Media_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Add_DOSBox_Game_Media.ItemClick
		Add_DOSBox_Games(enm_DOSBoxAdd_Mode.Install_Media_Files)
	End Sub

	Private Sub popmnu_DOSBox_Files_and_Folders_BeforePopup(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles popmnu_DOSBox_Files_and_Folders.BeforePopup
		If Not grd_DOSBox_Files_and_Folders.Allow_Popup Then
			e.Cancel = True
			Return
		End If

		If BS_Emu_Games.Current Is Nothing Then
			e.Cancel = True
			Return
		End If


		If BS_DOSBox_Files_and_Folders.Current Is Nothing Then
			bbi_DOSBox_Files_and_Folders_Rename.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		Else
			bbi_DOSBox_Files_and_Folders_Rename.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

			bbi_DOSBox_Files_and_Folders_Rename.Enabled = False

			'allow zip, iso, img, img_mount if File = InnerFile
			If {cls_Globals.enm_Rombase_DOSBox_Filetypes.zip, cls_Globals.enm_Rombase_DOSBox_Filetypes.iso, cls_Globals.enm_Rombase_DOSBox_Filetypes.img, cls_Globals.enm_Rombase_DOSBox_Filetypes.img_boot}.Contains(BS_DOSBox_Files_and_Folders.Current("id_Rombase_DOSBox_Filetypes")) Then
				If Equals(BS_DOSBox_Files_and_Folders.Current("File"), BS_DOSBox_Files_and_Folders.Current("InnerFile")) Then
					bbi_DOSBox_Files_and_Folders_Rename.Enabled = True
				End If
			End If

			'allow cwd
			If {cls_Globals.enm_Rombase_DOSBox_Filetypes.cwd}.Contains(BS_DOSBox_Files_and_Folders.Current("id_Rombase_DOSBox_Filetypes")) Then
				bbi_DOSBox_Files_and_Folders_Rename.Enabled = True
			End If

			'do not allow delete on head data
			If TC.NZ(BS_DOSBox_Files_and_Folders.Current("id_Emu_Games_Owner"), 0) = 0 Then
				'TODO: currently, there is no delete menu item
			End If
		End If
	End Sub

	Private Sub bbi_DOSBox_Files_and_Folders_Add_Archive_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_DOSBox_Files_and_Folders_Add_Archive.ItemClick
		If BS_Emu_Games.Current Is Nothing OrElse TC.NZ(BS_Emu_Games.Current("id_Emu_Games"), 0) = 0 Then Return

		Add_DOSBox_Games(enm_DOSBoxAdd_Mode.Packed_Files, Nothing, BS_Emu_Games.Current("id_Emu_Games"))
	End Sub

	Private Sub bbi_DOSBox_Files_and_Folders_Add_Directory_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_DOSBox_Files_and_Folders_Add_Directory.ItemClick
		If BS_Emu_Games.Current Is Nothing OrElse TC.NZ(BS_Emu_Games.Current("id_Emu_Games"), 0) = 0 Then Return

		Add_DOSBox_Games(enm_DOSBoxAdd_Mode.Installed_in_Directory, Nothing, BS_Emu_Games.Current("id_Emu_Games"))
	End Sub

	Private Sub bbi_DOSBox_Files_and_Folders_Add_Media_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_DOSBox_Files_and_Folders_Add_Media.ItemClick
		If BS_Emu_Games.Current Is Nothing OrElse TC.NZ(BS_Emu_Games.Current("id_Emu_Games"), 0) = 0 Then Return

		Add_DOSBox_Games(enm_DOSBoxAdd_Mode.Install_Media_Files, Nothing, BS_Emu_Games.Current("id_Emu_Games"))
	End Sub

	Private Sub bbi_Debug_Apply_TDC_Click(sender As System.Object, e As System.EventArgs) Handles bbi_Debug_Apply_TDC.ItemClick
		If Not TC.NZ(cmb_Platform.EditValue, 0) = cls_Globals.enm_Moby_Platforms.dos Then
			MKDXHelper.MessageBox("Please select the PC - DOS Platform first.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		Dim sFile As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("Open TDC.mdb", "Access Databases (*.mdb)|*.mdb", ParentForm:=Me)
		If Not Alphaleonis.Win32.Filesystem.File.Exists(sFile) Then Return

		Dim sResultType As String = ""
		Dim sResultDetails As String = ""

		Dim prg As MKNetDXLib.cls_MKDXBaseform_Progress_Helper = Nothing

		Try
			Dim acc_conn As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & sFile & ";Persist Security Info=False;")
			Dim acc_adapter As New OleDb.OleDbDataAdapter("SELECT * FROM TDC", acc_conn)
			Dim dt_TDC As New DataTable("TDC")
			acc_adapter.Fill(dt_TDC)

			Dim al_Remove As New ArrayList

			For Each row As DataRow In dt_TDC.Rows
				If Not TC.NZ(row("MobyURL"), "").Contains("/") AndAlso Not TC.NZ(row("MobyURL"), "").Contains("mobygames") Then
					al_Remove.Add(row)
				Else
					Dim ar_MobyURL As String() = TC.NZ(row("MobyURL"), "").Split("/")
					row("MobyURL") = ar_MobyURL(ar_MobyURL.Length - 1)
					row("long filename") = TC.NZ(row("long filename"), "").ToLower.Trim().Replace("'", "''")
				End If
			Next

			For Each row As DataRow In al_Remove
				dt_TDC.Rows.Remove(row)
			Next

			Dim rows_Owner As DataRow() = DS_ML.tbl_Emu_Games.Select("id_Emu_Games_Owner IS NULL")

			Dim dt_Results As New DataTable("Results")
			dt_Results.Columns.Add("Type", GetType(System.String))
			dt_Results.Columns.Add("Details", GetType(System.String))

			prg = New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Nothing), 400, 60, ProgressBarStyle.Blocks, False, "Processing file {0} of {1}", 0, rows_Owner.Length, False)
			prg.Start()

			Dim GC_Counter As Integer = 0

			For Each row_Owner As DataRow In rows_Owner
				prg.IncreaseCurrentValue()

				GC_Counter += 1

				Dim sOwnerFilename = TC.NZ(row_Owner("File"), "").ToLower.Trim.Replace("'", "''")

				sResultType = "OK"
				sResultDetails = sOwnerFilename

				Dim rows_TDC As DataRow() = dt_TDC.Select("[long filename] = '" & sOwnerFilename & "'")

				If rows_TDC.Length = 0 Then
					sResultType = "NOTASSIGNED"
				Else
					If rows_TDC.Length = 1 Then
						Dim sOwner_MobyURL As String = TC.NZ(row_Owner("Moby_Games_URLPart"), "")
						Dim sTDC_MobyURL As String = TC.NZ(rows_TDC(0)("MobyURL"), "")

						If sOwner_MobyURL = "" Then
							Dim rows_Moby_Releases() As DataRow = Me.DS_MobyDB.src_Moby_Releases.Select("Moby_Games_URLPart = '" & sTDC_MobyURL & "'")

							If rows_Moby_Releases.Length = 0 Then
								sResultType = "NOTFOUND"
								sResultDetails &= ": not found '" & sTDC_MobyURL & "'"
							End If

							If rows_Moby_Releases.Length > 1 Then
								sResultType = "MULTIPLEFOUND"
								sResultDetails &= ": multiple found '" & sTDC_MobyURL & "'"
							End If

							If rows_Moby_Releases.Length = 1 Then
								'Set_Moby_Link(BS_Emu_Games.Current.Row, BS_Moby_Releases.Current.row)
								Set_Moby_Link(row_Owner, rows_Moby_Releases(0))

								sResultType = "ASSIGNED"
								sResultDetails &= ": assigned to '" & sTDC_MobyURL & "'"
							End If
						Else
							If sOwner_MobyURL = sTDC_MobyURL Then
								sResultType = "SAME"
								sResultDetails &= ": '" & sOwner_MobyURL & "'"
							Else
								sResultType = "DIFFERENT"
								sResultDetails &= ": '" & sOwner_MobyURL & "' <> '" & sTDC_MobyURL & "'"
							End If
						End If
					Else
						sResultType = "MULTIPLEFOUND"
					End If
				End If

				'Write Results
				Dim row_Result As DataRow = dt_Results.NewRow
				row_Result("Type") = sResultType
				row_Result("Details") = sResultDetails
				dt_Results.Rows.Add(row_Result)

				'Prevent Out-of-Memory Exceptions
				If GC_Counter = 10 Then
					GC.Collect()
					GC_Counter = 0
				End If
			Next

			Dim sb_Result As New System.Text.StringBuilder

			Dim rows_NOTASSIGNED As DataRow() = dt_Results.Select("Type = 'NOTASSIGNED'")
			sb_Result.AppendLine("### Not assigned: " & rows_NOTASSIGNED.Length & " ###")
			For Each row_NOTASSIGNED In rows_NOTASSIGNED
				sb_Result.AppendLine(row_NOTASSIGNED("Details"))
			Next
			sb_Result.AppendLine("")

			Dim rows_ASSIGNED As DataRow() = dt_Results.Select("Type = 'ASSIGNED'")
			sb_Result.AppendLine("### Assigned: " & rows_ASSIGNED.Length & " ###")
			For Each row_ASSIGNED In rows_ASSIGNED
				sb_Result.AppendLine(row_ASSIGNED("Details"))
			Next
			sb_Result.AppendLine("")

			Dim rows_SAME As DataRow() = dt_Results.Select("Type = 'SAME'")
			sb_Result.AppendLine("### Same: " & rows_SAME.Length & " ###")
			For Each row_SAME In rows_SAME
				sb_Result.AppendLine(row_SAME("Details"))
			Next
			sb_Result.AppendLine("")

			Dim rows_DIFFERENT As DataRow() = dt_Results.Select("Type = 'DIFFERENT'")
			sb_Result.AppendLine("### Different: " & rows_DIFFERENT.Length & " ###")
			For Each row_DIFFERENT In rows_DIFFERENT
				sb_Result.AppendLine(row_DIFFERENT("Details"))
			Next
			sb_Result.AppendLine("")

			Dim rows_MULTIPLEFOUND As DataRow() = dt_Results.Select("Type = 'MULTIPLEFOUND'")
			sb_Result.AppendLine("### Multiple found: " & rows_MULTIPLEFOUND.Length & " ###")
			For Each row_MULTIPLEFOUND In rows_MULTIPLEFOUND
				sb_Result.AppendLine(row_MULTIPLEFOUND("Details"))
			Next
			sb_Result.AppendLine("")

			Dim rows_NOTFOUND As DataRow() = dt_Results.Select("Type = 'NOTFOUND'")
			sb_Result.AppendLine("### URL not found: " & rows_NOTFOUND.Length & " ###")
			For Each row_NOTFOUND In rows_NOTFOUND
				sb_Result.AppendLine(row_NOTFOUND("Details"))
			Next
			sb_Result.AppendLine("")

			If prg IsNot Nothing Then prg.Close()

			Dim frm As New MKNetDXLib.frm_MKDXMemoEdit(sb_Result.ToString)
			frm.Text = "Results"
			frm.ShowDialog(Me)

		Catch ex As Exception
			If prg IsNot Nothing Then prg.Close()
			MKDXHelper.ExceptionMessageBox(ex, sResultDetails & ControlChars.CrLf & ControlChars.CrLf, "Error")
		End Try
	End Sub

	Private Sub cmb_DOSBox_Type_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cmb_DOSBox_Type.EditValueChanged
		If TC.IsNullNothingOrEmpty(cmb_DOSBox_Type.EditValue) Then
			lbl_DOSBox_Exe_Type.Visible = False
			cmb_DOSBox_Exe_Type.Visible = False
			lbl_DOSBox_Mount_Destination.Visible = False
			cmb_DOSBox_Mount_Destination.Visible = False
			lbl_DOSBox_Volume_Number.Visible = False
			cmb_DOSBox_Volume_Number.Visible = False
		End If

		If {cls_Globals.enm_Rombase_DOSBox_Filetypes.zip, cls_Globals.enm_Rombase_DOSBox_Filetypes.cwd, cls_Globals.enm_Rombase_DOSBox_Filetypes.iso, cls_Globals.enm_Rombase_DOSBox_Filetypes.img, cls_Globals.enm_Rombase_DOSBox_Filetypes.img_boot}.Contains(TC.NZ(cmb_DOSBox_Type.EditValue, 0)) Then 'Packed Content, CWD, CD Image, Floppy Image, Floppy Booter
			lbl_DOSBox_Exe_Type.Visible = False
			cmb_DOSBox_Exe_Type.Visible = False
			lbl_DOSBox_Mount_Destination.Visible = True
			cmb_DOSBox_Mount_Destination.Visible = True
			lbl_DOSBox_Volume_Number.Visible = True
			cmb_DOSBox_Volume_Number.Visible = True
		End If

		If {cls_Globals.enm_Rombase_DOSBox_Filetypes.exe}.Contains(TC.NZ(cmb_DOSBox_Type.EditValue, 0)) Then  'Executable
			lbl_DOSBox_Exe_Type.Visible = True
			cmb_DOSBox_Exe_Type.Visible = True
			lbl_DOSBox_Mount_Destination.Visible = False
			cmb_DOSBox_Mount_Destination.Visible = False
			lbl_DOSBox_Volume_Number.Visible = False
			cmb_DOSBox_Volume_Number.Visible = False
		End If
	End Sub

	Private Sub bbi_DOSBox_Files_and_Folders_Rename_Click(sender As System.Object, e As System.EventArgs) Handles bbi_DOSBox_Files_and_Folders_Rename.ItemClick
		If BS_DOSBox_Files_and_Folders.Current Is Nothing Then Return

		Dim sOldFile As String = TC.NZ(BS_DOSBox_Files_and_Folders.Current("File"), "")
		If sOldFile = "" Then
			Dim folder As String = TC.NZ(BS_DOSBox_Files_and_Folders.Current("Folder"), "")
			Dim folder_parts As String() = folder.Split("\")
			sOldFile = folder_parts(folder_parts.Length - 1)
		End If
		Dim sNewFile As String = ""

		Using frm As New MKNetDXLib.frm_TextBoxEdit("New Name:", "Please enter the new name in the box below.", sOldFile, False)
			If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
				sNewFile = frm.Input
			End If
		End Using

		If sNewFile = sOldFile Then
			Return
		End If

		Cursor = Cursors.WaitCursor

		Try
			If sNewFile <> "" Then
				If Not TC.IsNullNothingOrEmpty(BS_DOSBox_Files_and_Folders.Current("File")) AndAlso Not TC.IsNullNothingOrEmpty(BS_DOSBox_Files_and_Folders.Current("InnerFile")) AndAlso Equals(BS_DOSBox_Files_and_Folders.Current("File"), BS_DOSBox_Files_and_Folders.Current("InnerFile")) Then
					'Renaming a file (can be zipfile with dependencies!)
					Dim sOldPath As String = BS_DOSBox_Files_and_Folders.Current("Folder") & "\" & sOldFile
					Dim sNewPath As String = BS_DOSBox_Files_and_Folders.Current("Folder") & "\" & sNewFile

					If Alphaleonis.Win32.Filesystem.File.Exists(sOldPath) AndAlso Not Alphaleonis.Win32.Filesystem.File.Exists(sNewPath) Then
						Cursor = Cursors.Default
						If MKDXHelper.MessageBox("You are about to rename '" & sOldPath & "' to '" & sNewPath & "' also on the file system. Do you want to continue?", "Rename", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
							Return
						End If
						Cursor = Cursors.WaitCursor
					End If
					If Not Alphaleonis.Win32.Filesystem.File.Exists(sOldPath) AndAlso Not Alphaleonis.Win32.Filesystem.File.Exists(sNewPath) Then
						Cursor = Cursors.Default
						If MKDXHelper.MessageBox("It appears that neither '" & sOldPath & "' nor '" & sNewPath & "' exist on the file system. Do you want to continue renaming the locations in the database?", "Rename", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
							Return
						End If
						Cursor = Cursors.WaitCursor
					End If

					If Alphaleonis.Win32.Filesystem.File.Exists(sOldPath) AndAlso Not Alphaleonis.Win32.Filesystem.File.Exists(sNewPath) Then
						Alphaleonis.Win32.Filesystem.File.Move(sOldPath, sNewPath)
					End If

					Dim rows_affected As DataRow() = Me.DS_ML.tbl_Emu_Games.Select("Folder = " & TC.getSQLFormat(BS_DOSBox_Files_and_Folders.Current("Folder")) & " AND File = " & TC.getSQLFormat(sOldFile))

					For Each row_affected As DataRow In rows_affected
						row_affected("File") = sNewFile
						If row_affected("InnerFile") = sOldFile Then
							row_affected("InnerFile") = sNewFile
						End If
					Next

					If Alphaleonis.Win32.Filesystem.Path.GetExtension(sNewPath).ToLower = ".cue" Then
						If Alphaleonis.Win32.Filesystem.File.Exists(sNewPath) Then
							Cursor = Cursors.Default
							If MKDXHelper.MessageBox("You have renamed a .cue file, do you also want to rename a bin file referenced by it accordingly?", "Rename", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
								Return
							End If
							Cursor = Cursors.WaitCursor

							Dim sContent As String = MKNetLib.cls_MKFileSupport.GetFileContents(sNewPath)
							Dim arContent As String() = sContent.Split(ControlChars.CrLf)
							Dim sbNewContent As New System.Text.StringBuilder
							For Each sLine As String In arContent
								sLine = sLine.Replace(ControlChars.Cr, "").Replace(ControlChars.Lf, "")
								Dim matches As System.Text.RegularExpressions.MatchCollection = MKNetLib.cls_MKRegex.GetMatches(sLine, "file(.*?)binary", 521)  'with ignorecase
								If matches.Count > 0 Then
									Dim sOldBinFile As String = matches(0).Groups(1).Value.Replace("""", "").Trim
									Dim sNewBinFile As String = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(sNewFile) & ".bin"
									Dim sOldBinPath As String = Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(sOldPath) & "\" & sOldBinFile
									Dim sNewBinPath As String = Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(sNewPath) & "\" & sNewBinFile

									If Not Alphaleonis.Win32.Filesystem.File.Exists(sOldPath) AndAlso Not Alphaleonis.Win32.Filesystem.File.Exists(sNewPath) Then
										Cursor = Cursors.Default
										If MKDXHelper.MessageBox("It appears that neither '" & sOldBinPath & "' nor '" & sNewBinPath & "' exist on the file system. Do you want to continue renaming the location in the .cue file?", "Rename", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
											sbNewContent.AppendLine(sLine)
											Cursor = Cursors.WaitCursor
											Continue For
										End If
									End If

									If Alphaleonis.Win32.Filesystem.File.Exists(sOldBinPath) AndAlso Not Alphaleonis.Win32.Filesystem.File.Exists(sNewBinPath) Then
										Alphaleonis.Win32.Filesystem.File.Move(sOldBinPath, sNewBinPath)
									End If

									sbNewContent.AppendLine("FILE """ & sNewBinFile & """ BINARY")
								Else
									sbNewContent.AppendLine(sLine)
								End If
							Next
							MKNetLib.cls_MKFileSupport.SaveTextToFile(sbNewContent.ToString, sNewPath)
						End If
					End If
				Else
					If TC.NZ(BS_DOSBox_Files_and_Folders.Current("InnerFile"), "") = "" Then
						'TODO: Renaming Directory (dont forget dependencies)
						Dim sOldPath As String = BS_DOSBox_Files_and_Folders.Current("Folder")
						Dim sNewPath As String = MKNetLib.cls_MKStringSupport.Clean_Right(BS_DOSBox_Files_and_Folders.Current("Folder"), sOldFile) & sNewFile

						If Alphaleonis.Win32.Filesystem.Directory.Exists(sOldPath) AndAlso Not Alphaleonis.Win32.Filesystem.Directory.Exists(sNewPath) Then
							Cursor = Cursors.Default
							If MKDXHelper.MessageBox("You are about to rename '" & sOldPath & "' to '" & sNewPath & "' also on the file system. Do you want to continue?", "Rename", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
								Return
							End If
							Cursor = Cursors.WaitCursor
						End If
						If Not Alphaleonis.Win32.Filesystem.Directory.Exists(sOldPath) AndAlso Not Alphaleonis.Win32.Filesystem.Directory.Exists(sNewPath) Then
							Cursor = Cursors.Default
							If MKDXHelper.MessageBox("It appears that neither '" & sOldPath & "' nor '" & sNewPath & "' exist on the file system. Do you want to continue renaming the locations in the database?", "Rename", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
								Return
							End If
							Cursor = Cursors.WaitCursor
						End If

						If Alphaleonis.Win32.Filesystem.Directory.Exists(sOldPath) AndAlso Not Alphaleonis.Win32.Filesystem.Directory.Exists(sNewPath) Then
							Alphaleonis.Win32.Filesystem.Directory.Move(sOldPath, sNewPath)
						End If

						Dim rows_affected As DataRow() = Me.DS_ML.tbl_Emu_Games.Select("Folder LIKE " & TC.getSQLFormat(sOldPath & "%"))

						For Each row_affected As DataRow In rows_affected
							row_affected("Folder") = sNewPath & MKNetLib.cls_MKStringSupport.Clean_Left(row_affected("Folder"), sOldPath)
							If row_affected("File") = sOldFile Then
								row_affected("File") = sNewFile
							End If
						Next
					End If

				End If

			End If
		Catch ex As Exception
			Cursor = Cursors.Default
			MKDXHelper.ExceptionMessageBox(ex, Caption:="Rename")
		End Try

		Cursor = Cursors.Default
	End Sub

	Private Sub bbi_Moby_Games_Evaluate_Links_Click(sender As Object, e As EventArgs) Handles bbi_Moby_Games_Evaluate_Links.ItemClick
		Cursor = Cursors.WaitCursor

		Dim ar_Have As New ArrayList
		For Each row_Emu_Games As DataRow In DS_ML.tbl_Emu_Games.Rows
			If Not {DataRowState.Deleted, DataRowState.Detached}.Contains(row_Emu_Games.RowState) Then
				Dim Moby_Games_URLPart As String = TC.NZ(row_Emu_Games("Moby_Games_URLPart"), "")
				Moby_Games_URLPart = Replace(Moby_Games_URLPart, "\", "")

				If Moby_Games_URLPart <> "" Then
					If Not ar_Have.Contains(Moby_Games_URLPart) Then
						ar_Have.Add(Moby_Games_URLPart)
					End If
				End If
			End If
		Next

		Dim ar_Missing As New ArrayList
		Dim ar_Moby_Total As New ArrayList
		For Each row_Moby_Releases As DataRow In Me.DS_MobyDB.src_Moby_Releases.Rows
			Dim sURLPart As String = row_Moby_Releases("Moby_Games_URLPart").ToString.Replace("\", "")

			If Not ar_Moby_Total.Contains(sURLPart) Then
				If (TC.NZ(row_Moby_Releases("deprecated"), False) = False AndAlso TC.NZ(row_Moby_Releases("compilation"), False) = False) OrElse ar_Have.Contains(sURLPart) Then
					ar_Moby_Total.Add(sURLPart)
				End If
			End If

			If Not ar_Have.Contains(sURLPart) Then
				If TC.NZ(row_Moby_Releases("deprecated"), False) = False AndAlso TC.NZ(row_Moby_Releases("compilation"), False) = False Then
					row_Moby_Releases("Highlighted") = True

					If Not ar_Missing.Contains(sURLPart) Then
						ar_Missing.Add(sURLPart)
					End If
				End If
			Else
				row_Moby_Releases("Highlighted") = False
			End If
		Next

		Dim sMessage As String = ""
		sMessage &= "Out of " & ar_Moby_Total.Count & " distinct MobyGames Releases, " & ar_Have.Count & " are linked to a Game, " & ar_Missing.Count & " are missing." & ControlChars.CrLf
		sMessage &= "The link ratio is " & CInt(CDbl(ar_Have.Count) * 100 / CDbl(ar_Moby_Total.Count)) & "%. Any missing MobyGame Release is highlighted."
		sMessage &= ControlChars.CrLf & ControlChars.CrLf & "Deprecated and Compilation releases have been ignored."
		sMessage &= ControlChars.CrLf & ControlChars.CrLf & CInt(CDbl(ar_Have.Count) * 100 / CDbl(ar_Moby_Total.Count)) & "% (" & ar_Have.Count & " / " & ar_Moby_Total.Count & ")"

		MKDXHelper.MessageBox(sMessage, "Evaluate MobyGames Links", MessageBoxButtons.OK, MessageBoxIcon.Information)

		Cursor = Cursors.Default
	End Sub

	Private Sub bbi_Auto_Link_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbi_Auto_Link.ItemClick
		If BS_Moby_Platforms.Current Is Nothing Then
			Return
		End If

		Dim autolinkOptions As frm_Moby_Auto_Link_Options.cls_Moby_Auto_Link_Options

		Dim Strip_File_Extensions As Boolean = False

		Dim dotcounter As Integer = 0
		For Each row_Rombase As DS_Rombase.tbl_RombaseRow In Me.DS_Rombase.tbl_Rombase.Rows
			If TC.NZ(row_Rombase.filename, "").Contains(".") Then
				dotcounter += 1
			End If
		Next

		If CDbl(dotcounter) / CDbl(Me.DS_Rombase.tbl_Rombase.Rows.Count) > 0.9 Then
			Strip_File_Extensions = True
		End If

		Using frm As New frm_Moby_Auto_Link_Options(Strip_File_Extensions)
			If frm.ShowDialog <> DialogResult.OK Then
				Return
			End If

			autolinkOptions = frm.Result
		End Using

		Dim tbl_Moby_Auto_Link As New DS_ML.tbl_Moby_Auto_LinkDataTable

		Dim sSelect As String = "id_Emu_Games_Owner IS NULL AND Moby_Games_URLPart IS NULL"

		If autolinkOptions.Redetect_Deprecated Then
			sSelect = "(" & sSelect & ")" & " OR (id_Emu_Games_Owner IS NULL AND Moby_Games_URLPart IS NOT NULL AND deprecated = 1)"
		End If

		Dim rows_Emu_Games As DS_ML.tbl_Emu_GamesRow() = Me.DS_ML.tbl_Emu_Games.Select(sSelect)

		Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Nothing), 400, 60, ProgressBarStyle.Blocks, False, "Preparing Game data ...", 0, rows_Emu_Games.Length, False)
		prg.Start()

		For Each row_Emu_Games As DS_ML.tbl_Emu_GamesRow In rows_Emu_Games
			prg.IncreaseCurrentValue()

			Dim row_Auto_Link As DS_ML.tbl_Moby_Auto_LinkRow = tbl_Moby_Auto_Link.NewRow
			row_Auto_Link.id = row_Emu_Games.id_Emu_Games

			If TC.NZ(row_Emu_Games("CustomIdentifier"), "") <> "" Then
				row_Auto_Link.Identifier = row_Emu_Games("CustomIdentifier")
			Else
				row_Auto_Link.Identifier = TC.NZ(row_Emu_Games("crc32"), "")
			End If

			Dim GameName As String = ""

			If TC.NZ(row_Emu_Games.InnerFile, "") <> "" Then
				GameName = row_Emu_Games.InnerFile
			End If

			If GameName = "" Then
				GameName = Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(row_Emu_Games.Folder)
			End If

			row_Auto_Link.GameName = row_Emu_Games.InnerFile

			tbl_Moby_Auto_Link.Rows.Add(row_Auto_Link)
		Next

		prg.Close()

		If tbl_Moby_Auto_Link.Rows.Count = 0 Then
			MKDXHelper.MessageBox("All entries are already linked, no need for an auto link.", "Auto Link", MessageBoxButtons.OK, MessageBoxIcon.Information)
			Return
		End If

		Dim src_Moby_Releases As New DS_MobyDB.src_Moby_ReleasesDataTable

		prg = New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Nothing), 400, 60, ProgressBarStyle.Blocks, False, "Preparing Moby data ...", 0, Me.DS_MobyDB.src_Moby_Releases.Rows.Count, False)
		prg.Start()

		For Each row_Moby_Releases As DS_MobyDB.src_Moby_ReleasesRow In Me.DS_MobyDB.src_Moby_Releases.Rows
			If Not autolinkOptions.Ignore_Deprecated OrElse Not TC.NZ(row_Moby_Releases.deprecated, False) Then
				src_Moby_Releases.ImportRow(row_Moby_Releases)
			End If
		Next

		For Each row_Moby_Releases As DS_MobyDB.src_Moby_ReleasesRow In src_Moby_Releases.Rows
			prg.IncreaseCurrentValue()
		Next

		prg.Close()

		sExplanation = "The left list shows all your games that had previously missing MobyGames links. If a match with a MobyGames release has been found, the corresponding fields (Moby Gamename, Match Accuracy etc.) have values. If the match accuracy is exactly 100%, the link is automatically set to be applied (see Apply column). Please thoroughly review these results and check/uncheck the Apply checkbox (by click or by pressing Enter). You can also re-link with another MobyGames release by doubleclicking the release on the right list."

		Using frm As New frm_Moby_Auto_Link(tbl_Moby_Auto_Link, src_Moby_Releases, sExplanation, autolinkOptions)
			If frm.ShowDialog() = DialogResult.OK Then
				Dim iLinkCount As Integer = 0

				For Each rowAutoLink As DS_ML.tbl_Moby_Auto_LinkRow In frm.DS_ML.tbl_Moby_Auto_Link.Select("Apply = 1")
					Dim rowsEmuGames() As DS_ML.tbl_Emu_GamesRow = Me.DS_ML.tbl_Emu_Games.Select("id_Emu_Games = " & TC.getSQLFormat(rowAutoLink.id))

					If rowsEmuGames.Length = 1 Then
						Dim rowsMobyReleases() As DS_MobyDB.src_Moby_ReleasesRow = Me.DS_MobyDB.src_Moby_Releases.Select("id_Moby_Releases = " & TC.getSQLFormat(rowAutoLink.Match_id_Moby_Releases))

						If rowsMobyReleases.Count = 1 Then
							iLinkCount += 1
							Set_Moby_Link(rowsEmuGames(0), rowsMobyReleases(0))
						End If
					End If
				Next

				MKDXHelper.MessageBox(iLinkCount & " links have been applied.", "Auto-Link", MessageBoxButtons.OK, MessageBoxIcon.Information)
			End If
		End Using
	End Sub

	Private Sub popmnu_Moby_Games_BeforePopup(sender As Object, e As CancelEventArgs) Handles popmnu_Moby_Games.BeforePopup
		If Not grd_Moby_Releases.Allow_Popup Then
			e.Cancel = True
			Return
		End If
	End Sub

	Private Sub gv_Emu_Games_MouseMove(sender As Object, e As MouseEventArgs) Handles gv_Emu_Games.MouseMove
		Me.grd_Emu_Games.ShowHandInColumns(gv_Emu_Games, {"Hidden"}, e)
	End Sub

	Private Sub gv_Emu_Games_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_Emu_Games.FocusedRowChanged, gv_MV.FocusedRowChanged, gv_DOSBox_Files_and_Folders.FocusedRowChanged
		Dim gv As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

		If TC.NZ(gv.GetIncrementalText(), "") <> "" Then
			gv.ClearSelection()
			gv.SelectRow(gv.FocusedRowHandle)
		End If
	End Sub

	Private Sub bbi_MobyLink_QA_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbi_MobyLink_QA.ItemClick
		Dim iRowHandles As Integer() = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Emu_Games)
		Dim iNumRows As Integer = iRowHandles.Length

		If iNumRows <= 0 Then
			Return
		End If

		For Each iRowHandle As Integer In iRowHandles
			Dim row As DataRow = gv_Emu_Games.GetRow(iRowHandle).Row

			Dim rowStateUnchanged As Boolean = True
			If (row.RowState <> DataRowState.Unchanged) Then
				rowStateUnchanged = False
			End If

			row("tmp_Highlighted") = False
			If rowStateUnchanged Then row.AcceptChanges()

			Dim sInnerFile As String = TC.NZ(row("InnerFile"), "")

			If sInnerFile = "" Then
				Continue For
			End If

			Dim sMoby_Games_URLPart As String = TC.NZ(row("Moby_Games_URLPart"), "")

			If sMoby_Games_URLPart = "" Then
				Continue For
			End If

			Dim rowsMobyReleases() As DataRow = Me.DS_MobyDB.src_Moby_Releases.Select("Moby_Games_URLPart = " & TC.getSQLFormat(sMoby_Games_URLPart))

			row("tmp_Highlighted") = True
			If rowStateUnchanged Then row.AcceptChanges()

			If rowsMobyReleases.Length <> 1 Then
				Continue For
			End If

			Dim sDeveloper = TC.NZ(rowsMobyReleases(0)("Developer"), "").ToLower
			Dim sPublisher = TC.NZ(rowsMobyReleases(0)("Publisher"), "").ToLower

			If sDeveloper = "" AndAlso sPublisher = "" Then
				Continue For
			End If

			Dim rxMatches As System.Text.RegularExpressions.MatchCollection = MKNetLib.cls_MKRegex.GetMatches(sInnerFile, "\(.*?\)")

			If rxMatches.Count = 0 Then
				Continue For
			End If

			For Each match As System.Text.RegularExpressions.Match In rxMatches
				Dim sMatchValue = match.Value.Replace("(", "").Replace(")", "").ToLower

				If sMatchValue = "" Then
					Continue For
				End If

				Dim iDistanceDeveloper As Integer = MKNetLib.cls_MKStringSupport.FuzzySearch.LevenshteinDistance(sMatchValue, sDeveloper)
				Dim lengthDeveloper As Integer = Math.Max(sMatchValue.Length, sDeveloper.Length)
				Dim scoreDeveloper As Double = 1.0 - CDbl(iDistanceDeveloper) / lengthDeveloper

				Dim iDistancePublisher As Integer = MKNetLib.cls_MKStringSupport.FuzzySearch.LevenshteinDistance(sMatchValue, sPublisher)
				Dim lengthPublisher As Integer = Math.Max(sMatchValue.Length, sPublisher.Length)
				Dim scorePublisher As Double = 1.0 - CDbl(iDistancePublisher) / lengthPublisher

				If scoreDeveloper > 0.5 Then
					row("tmp_Highlighted") = False
				End If

				If scorePublisher > 0.5 Then
					row("tmp_Highlighted") = False
				End If

				If rowStateUnchanged Then row.AcceptChanges()

			Next

			Dim i As Integer = 0
		Next

		gv_Emu_Games.RefreshData()
	End Sub

	Private Sub Add_ScummVM_Games()
		Dim sPathScummVM = ""
		Dim sPathToScan = ""

		Using frm As New frm_ScummVM_Scan()
			If frm.ShowDialog = DialogResult.OK Then
				sPathScummVM = TC.NZ(frm.txb_ScummVM.EditValue, "")
				sPathToScan = TC.NZ(frm.txb_Dir.EditValue, "")
			Else
				Return
			End If
		End Using

		Dim sIniPath As String = MKNetLib.cls_MKFileSupport.CreateTempDir("ml_") & "\scummvm.ini"

		If Not Alphaleonis.Win32.Filesystem.File.Exists(sPathScummVM) Then
			Return
		End If

		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(sPathToScan) Then
			Return
		End If

		Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Nothing), 400, 100, ProgressBarStyle.Marquee, False, "Scanning, please wait...", 0, 0, False)
		prg.Start()

		Try
			Dim proc As New Process
			With proc.StartInfo
				.FileName = sPathScummVM
				.WorkingDirectory = Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(sPathScummVM)
				.Arguments = "--config=""" & sIniPath & """" & " --path=""" & sPathToScan & """ --recursive --add"
				.UseShellExecute = False
				.RedirectStandardOutput = True
				.WindowStyle = ProcessWindowStyle.Hidden
				.CreateNoWindow = True
			End With

			proc.Start()
			Dim output As String = proc.StandardOutput.ReadToEnd
			proc.WaitForExit()
		Catch ex As Exception
			prg.Close()
			MKDXHelper.ExceptionMessageBox(ex, "An Error occured while running scummvm.exe: ", "Add ScummVM Games")
			Return
		End Try

		If Not Alphaleonis.Win32.Filesystem.File.Exists(sIniPath) Then
			prg.Close()
			MKDXHelper.MessageBox("Probably scummvm crashed during the scan process and didn't create a scummvm.ini which would be necessary to add games. Please check if a scan via the scummvm UI works and try again.", "Add ScummVM Games", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		Dim sContent = Alphaleonis.Win32.Filesystem.File.ReadAllText(sIniPath)
		Dim aContent As String() = sContent.Split(ControlChars.CrLf, ControlChars.Lf)

		Dim ar_Descriptions As New ArrayList
		Dim ar_Entries As New ArrayList

		Dim i As Integer = 0

		While i < aContent.Length
			If aContent(i).Trim.StartsWith("[") AndAlso Not aContent(i).Trim.ToLower = "[scummvm]" Then
				Dim sTarget As String = aContent(i).Replace("[", "").Replace("]", "").Trim

				Dim sCustomIdentifier As String = sTarget

				If MKNetLib.cls_MKRegex.IsMatch(sTarget, "(^.*)\-\d+$") Then
					sCustomIdentifier = MKNetLib.cls_MKRegex.GetMatches(sTarget, "(^.*)\-\d+$")(0).Groups(1).Value
				End If

				Dim sDescription As String = ""
				Dim sGameID As String = ""
				Dim sPath As String = ""
				Dim sLanguage As String = ""

				i += 1

				While i < aContent.Length
					Dim sLine As String = aContent(i).Trim

					If sLine.StartsWith("[") Then
						Exit While
					End If

					If sLine.StartsWith("gameid") Then
						sGameID = sLine.Split("=")(1).Trim
					End If

					If sLine.StartsWith("description") Then
						sDescription = sLine.Split("=")(1).Trim
					End If

					If sLine.StartsWith("language") Then
						sLanguage = sLine.Split("=")(1).Trim
					End If

					If sLine.StartsWith("path") Then
						sPath = sLine.Split("=")(1).Trim
						sPath = MKNetLib.cls_MKStringSupport.Clean_Right(sPath, "\")
					End If

					i += 1
				End While

				If sTarget <> "" AndAlso sCustomIdentifier <> "" AndAlso sGameID <> "" AndAlso sPath <> "" AndAlso sDescription <> "" Then
					'Valid Entry - add to tbl_Emu_Games
					If sLanguage <> "" Then
						sDescription &= " (" & sLanguage & ")"
					End If

					Dim dict_Entry As New Dictionary(Of String, String)
					dict_Entry("target") = sTarget
					dict_Entry("CustomIdentifier") = sCustomIdentifier
					dict_Entry("gameid") = sGameID
					dict_Entry("path") = sPath
					dict_Entry("description") = sDescription

					ar_Entries.Add(dict_Entry)
					ar_Descriptions.Add(sDescription)
				End If
			Else
				i += 1
			End If
		End While

		If ar_Entries.Count = 0 Then
			prg.Close()
			MKDXHelper.MessageBox("The scan revealed no results.", "Add ScummVM Games", MessageBoxButtons.OK, MessageBoxIcon.Information)
			Return
		End If

		prg.Close()

		Using frmTagParser As New frm_Tag_Parser_Edit(Nothing, ar_Descriptions.ToArray(GetType(String)), Nothing, False, False, False)
			If frmTagParser.ShowDialog(Me) = DialogResult.Cancel Then
				Return
			End If
		End Using

		Dim result As New cls_AddGameStats()

		If ar_Entries.Count > 10 Then
			Prepare_dict_Rombase()
		End If

		prg = New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Nothing), 400, 60, ProgressBarStyle.Blocks, False, "Importing game {0} of {1}", 0, ar_Entries.Count, False)
		prg.Start()

		PrepareDictHave()

		Dim Aborted As Boolean = False

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			For Each ScummVMEntry As Dictionary(Of String, String) In ar_Entries
				prg.IncreaseCurrentValue()

				Dim res As cls_AddGameStats = Add_ScummVM_Game(tran, ScummVMEntry, prg)

				If res Is Nothing Then
					'User cancelled
					Aborted = True
					Exit For
				End If

				result.Add(res)
			Next

			tran.Commit()
			prg.Close()
		End Using

		Dim cntMismatch As Integer = Me.DS_ML.tbl_Emu_Games.Select("ROMBASE_id_Moby_Platforms IS NOT NULL AND id_Moby_Platforms <> ROMBASE_id_Moby_Platforms").Length

		Dim sResult As String = "Result" & IIf(Aborted, "after cancellation", "") & ": " & ControlChars.CrLf & ControlChars.CrLf & result._new & " new games added" & ControlChars.CrLf & result._links & " links to MobyGames meta data applied" & ControlChars.CrLf & result._duplicates_added & " added duplicates" & ControlChars.CrLf & result._duplicates_replaced & " replaced duplicates" & ControlChars.CrLf & result._duplicates_ignored & " ignored duplicates"

		If cntMismatch > 0 Then
			sResult &= ControlChars.CrLf & ControlChars.CrLf & "WARNING: There have been " & cntMismatch & " platform mismatch/es detected! All affected entries are in red color. Did you import Roms for the correct Platform?"
		End If

		Clear_dict_Rombase()

		MKDXHelper.MessageBox(sResult, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
	End Sub
End Class