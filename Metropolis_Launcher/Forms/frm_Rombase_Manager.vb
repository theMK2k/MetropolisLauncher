Imports DataAccess = MKNetLib.cls_MKSQLiteDataAccess

Public Class frm_ROMBase_Manager
	Private _bbi_Remove_Link_Caption As String
	Private _bbi_Delete_Caption As String
	Private _ChangesWritten As Boolean = False

	Private _Moby_Platforms_URLPart As String = ""

	Private _id_Rombase As Object = Nothing

	Public Sub New(Optional ByVal id_Rombase As Object = Nothing)
		InitializeComponent()

		barmng.SetPopupContextMenu(grd_DAT, popmnu_Rombase)
		barmng.SetPopupContextMenu(grd_Moby_Releases, popmnu_Moby_Games)

		Dim sSQL As String = ""
		sSQL &= "	SELECT	id_Moby_Platforms"
		sSQL &= "					, Display_Name || ' (' || "
		sSQL &= "						("
		sSQL &= "							SELECT COUNT(1)"
		sSQL &= "							FROM rombase.tbl_Rombase RB"
		sSQL &= "							WHERE RB.id_Moby_Platforms = PLTFM.id_Moby_Platforms"
		sSQL &= "										AND RB.id_Moby_Releases IS NOT NULL"
		sSQL &= "										AND RB.id_Rombase_Owner IS NULL"
		sSQL &= "						)"
		sSQL &= "						|| '/' || "
		sSQL &= "						("
		sSQL &= "							SELECT COUNT(1)"
		sSQL &= "							FROM rombase.tbl_Rombase RB"
		sSQL &= "							WHERE RB.id_Moby_Platforms = PLTFM.id_Moby_Platforms"
		sSQL &= "							AND RB.id_Rombase_Owner IS NULL"
		sSQL &= "						) || ')' AS Display_Name, URLPart FROM moby.tbl_Moby_Platforms PLTFM WHERE Visible = 1 AND id_Moby_Platforms_Owner IS NULL ORDER BY Display_Name"

		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, DS_MobyDB.tbl_Moby_Platforms)


		gv_DAT.ShowFindPanel()
		gv_Moby_Releases.ShowFindPanel()

		_bbi_Remove_Link_Caption = bbi_Remove_Link.Caption
		_bbi_Delete_Caption = bbi_Delete.Caption

		_id_Rombase = id_Rombase
	End Sub

	Private Sub frm_Rombase_Manager_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
		If TC.NZ(_id_Rombase, 0) > 0 Then
			cmb_Platform.EditValue = CLng(TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_Moby_Platforms FROM tbl_Rombase WHERE id_Rombase = " & TC.getSQLFormat(_id_Rombase)), 0))
			grd_DAT.ForceInitialize()
			Me.BS_Rombase.Position = Me.BS_Rombase.Find("id_Rombase", _id_Rombase)
		End If

#If Not DEBUG Then
		If Not MKDXHelper.MessageBox("The RomBase Manager is an internal tool for managing file metadata. This data is not supposed to be managed by individual users. Click >yes< if you really know what you're doing.", "RomBase Manager", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
			Me.Close()
			Return
		End If
#End If

	End Sub

	Private Sub grd_Moby_Releases_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd_Moby_Releases.DoubleClick
		Dim e_mouse As DevExpress.Utils.DXMouseEventArgs = e
		Dim hitinfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = gv_Moby_Releases.CalcHitInfo(e_mouse.Location)
		If Not hitinfo.InRow Then
			Return
		End If

		If BS_Rombase.Current IsNot Nothing AndAlso BS_Moby_Releases IsNot Nothing Then
			Dim row_Current As DataRow = BS_Moby_Releases.Current.Row

			Dim ar_Selected_Rows As New ArrayList



			For Each iRowHandle As Integer In gv_DAT.GetSelectedRows
				If iRowHandle >= 0 AndAlso gv_DAT.GetRow(iRowHandle) IsNot Nothing Then
					ar_Selected_Rows.Add(gv_DAT.GetRow(iRowHandle).Row)
				End If
			Next

			For Each selected_Row As DataRow In ar_Selected_Rows
				selected_Row("id_Moby_Releases") = Math.Abs(row_Current("id_Moby_Releases"))
				selected_Row("Moby_Games_URLPart") = row_Current("Moby_Games_URLPart").Replace("\", "")
				selected_Row("deprecated") = row_Current("deprecated")
			Next

		End If
	End Sub

	Private Sub grd_DAT_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd_DAT.DoubleClick
		Dim e_mouse As DevExpress.Utils.DXMouseEventArgs = e
		Dim hitinfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = gv_DAT.CalcHitInfo(e_mouse.Location)
		If Not hitinfo.InRow Then
			Return
		End If

		If BS_Rombase.Current IsNot Nothing AndAlso BS_Moby_Releases IsNot Nothing Then
			BS_Rombase.Current("id_Moby_Releases") = Math.Abs(BS_Moby_Releases.Current("id_Moby_Releases"))
			BS_Rombase.Current("Moby_Games_URLPart") = BS_Moby_Releases.Current("Moby_Games_URLPart").Replace("\", "")
		End If

		gv_DAT.RefreshData()
	End Sub

	Private Sub cmb_Platform_EditValueChanging(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles cmb_Platform.EditValueChanging
		'ask for save
		If Save(True) = Windows.Forms.DialogResult.Cancel Then
			e.Cancel = True
			Return
		End If

		Cursor.Current = Cursors.WaitCursor



		Me.DS_MobyDB.src_Moby_Releases.Clear()
		Me.DS_Rombase.tbl_Rombase.Clear()

		If Not TC.IsNullNothingOrEmpty(e.NewValue) Then
			'Dim sSQL As String = ""
			'sSQL &= "	SELECT" & ControlChars.CrLf
			'sSQL &= "	REL.id_Moby_Releases AS id_Moby_Releases" & ControlChars.CrLf
			'sSQL &= "	, IFNULL(GAME.Name_Prefix, '') || GAME.Name AS GameName" & ControlChars.CrLf
			'sSQL &= "	, REL.id_Moby_Platforms AS id_Moby_Platforms" & ControlChars.CrLf
			''sSQL &= "	, soundex(GAME.Name) AS Soundex" & ControlChars.CrLf
			'sSQL &= "	, GAME.URLPart AS Moby_Games_URLPart" & ControlChars.CrLf
			'sSQL &= "	FROM moby.tbl_Moby_Releases REL" & ControlChars.CrLf
			'sSQL &= "	INNER JOIN moby.tbl_Moby_Games GAME ON REL.id_Moby_Games = GAME.id_Moby_Games" & ControlChars.CrLf
			'sSQL &= "	WHERE REL.id_Moby_Platforms = " & TC.getSQLFormat(e.NewValue) & ControlChars.CrLf

			'sSQL &= "	UNION" & ControlChars.CrLf
			'sSQL &= "	SELECT" & ControlChars.CrLf
			'sSQL &= "		-REL.id_Moby_Releases AS id_Moby_Releases" & ControlChars.CrLf
			'sSQL &= "		, MGAT.Alternate_Title || ' [' || IFNULL(MGAT.Description, 'NODESCRIPTION') || '; ' || IFNULL(GAME.Name_Prefix, '') || GAME.Name || ']' AS GameName" & ControlChars.CrLf
			'sSQL &= "		, REL.id_Moby_Platforms AS id_Moby_Platforms" & ControlChars.CrLf
			''sSQL &= "		, NULL AS Soundex" & ControlChars.CrLf
			'sSQL &= "		, GAME.URLPart AS Moby_Games_URLPart" & ControlChars.CrLf
			'sSQL &= "	FROM moby.tbl_Moby_Games_Alternate_Titles MGAT" & ControlChars.CrLf
			'sSQL &= "	INNER JOIN moby.tbl_Moby_Games GAME ON MGAT.id_Moby_Games = GAME.id_Moby_Games" & ControlChars.CrLf
			'sSQL &= "	INNER JOIN moby.tbl_Moby_Releases REL ON REL.id_Moby_Games = GAME.id_Moby_Games AND REL.id_Moby_Platforms = " & TC.getSQLFormat(e.NewValue) & ControlChars.CrLf

			'sSQL &= "	ORDER BY GameName" & ControlChars.CrLf
			'DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, Me.DS_MobyDB.src_Moby_Releases)

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				DS_ML.Fill_src_frm_Rom_Manager_Moby_Releases(tran, Me.DS_MobyDB.src_Moby_Releases, e.NewValue)
			End Using

			sSQL = ""
			sSQL &= "	SELECT"
			sSQL &= "	RB.id_rombase"
			sSQL &= "	, RB.filename"
			sSQL &= "	, RB.size"
			sSQL &= "	, RB.crc"
			sSQL &= "	, RB.md5"
			sSQL &= "	, RB.sha1"
			sSQL &= "	, RB.id_Moby_Platforms"
			sSQL &= "	, RB.id_Moby_Releases"
			'sSQL &= "	, soundex(filename) AS Soundex"
			sSQL &= " , RB.Moby_Platforms_URLPart"
			sSQL &= " , RB.Moby_Games_URLPart"
			sSQL &= "	, RB.CustomIdentifier"
			sSQL &= " , CASE WHEN REL.deprecated = 1 THEN 1 ELSE MG.deprecated END AS deprecated"
			sSQL &= "	FROM rombase.tbl_Rombase RB"
			sSQL &= " LEFT JOIN moby.tbl_Moby_Games MG ON RB.Moby_Games_URLPart = MG.URLPart"
			sSQL &= " LEFT JOIN tbl_Moby_Releases REL ON REL.id_Moby_Games = MG.id_Moby_Games AND REL.id_Moby_Platforms = RB.id_Moby_Platforms"
			sSQL &= "	WHERE RB.id_Rombase_Owner IS NULL AND RB.id_Moby_Platforms = " & TC.getSQLFormat(e.NewValue)
			sSQL &= "	ORDER BY filename"
			DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, DS_Rombase.tbl_Rombase)

			If e.NewValue <> 0 Then
				_Moby_Platforms_URLPart = TC.NZ(DS_MobyDB.tbl_Moby_Platforms.Select("id_Moby_Platforms = " & e.NewValue)(0)("URLPart"), "")
			End If

			Cursor.Current = Cursors.Default
		End If
	End Sub

	Private Sub BS_Rombase_CurrentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BS_Rombase.CurrentChanged
		gv_Moby_Releases.RefreshData()

		If BS_Rombase.Current Is Nothing Then Return

		If Not TC.IsNullNothingOrEmpty(BS_Rombase.Current("id_Moby_Releases")) Then
			Dim iNewPos As Integer = BS_Moby_Releases.Find("id_Moby_Releases", BS_Rombase.Current("id_Moby_Releases"))
			If iNewPos > 0 Then
				BS_Moby_Releases.Position = iNewPos
				Me.gv_Moby_Releases.ClearSelection()
				Me.gv_Moby_Releases.SelectRow(Me.gv_Moby_Releases.FocusedRowHandle)
			End If
		Else
			'MK2k: don't autoselect by soundex, it's too inaccurate
			'Dim iNewPos As Integer = BS_Moby_Releases.Find("Soundex", BS_Rombase.Current("Soundex"))
			'If iNewPos > 0 Then
			'	BS_Moby_Releases.Position = iNewPos
			'End If
		End If

	End Sub

	Private Sub popmnu_Rombase_BeforePopup(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles popmnu_Rombase.BeforePopup
		If Not grd_DAT.Allow_Popup Then
			e.Cancel = True
			Return
		End If

		If TC.NZ(cmb_Platform.EditValue, 0) = 0 Then
			e.Cancel = True
		End If

		If BS_Rombase.Current IsNot Nothing Then
			Me.bbi_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
			Me.bbi_Delete.Caption = _bbi_Delete_Caption.Replace("{0}", gv_DAT.SelectedRowsCount)

			Me.bbi_Remove_Link.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
			Me.bbi_Remove_Link.Caption = _bbi_Remove_Link_Caption.Replace("{0}", gv_DAT.SelectedRowsCount)
		Else
			Me.bbi_Remove_Link.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		End If
	End Sub

	Private Sub popmnu_Moby_Games_BeforePopup(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles popmnu_Moby_Games.BeforePopup
		If Not grd_Moby_Releases.Allow_Popup Then
			e.Cancel = True
			Return
		End If
	End Sub

	Private Sub bbi_Add_Games_from_DAT_XML_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Add_Games_from_DAT_XML.ItemClick
		Dim ds As New DataSet
		Dim sFile As String = MKNetLib.cls_MKFileSupport.OpenFileDialog(ParentForm:=Me)

		If Alphaleonis.Win32.Filesystem.File.Exists(sFile) Then
			Me.DS_Rombase.tbl_Rombase.Clear()
			ds.ReadXml(sFile)
		End If

		Dim iTotal As Integer = 0
		Dim iNew As Integer = 0
		Dim iError As Integer = 0

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			Try
				iTotal = ds.Tables("rom").Rows.Count

				For Each rowrom As DataRow In ds.Tables("rom").Rows
					Dim rowrombase As DS_Rombase.tbl_RombaseRow = Nothing

					'Check in Database and save id_rombase
					Dim filename As String = rowrom("name")
					Dim size As Integer = rowrom("size")
					Dim crc As String = rowrom("crc").ToString.ToLower
					Dim md5 As Object = DBNull.Value
					Dim sha1 As Object = DBNull.Value
					If MKNetLib.cls_MKSQLDataAccess.HasColumn(rowrom, "md5") AndAlso Not TC.IsNullNothingOrEmpty(rowrom("md5")) Then md5 = rowrom("md5").ToString.ToLower
					If MKNetLib.cls_MKSQLDataAccess.HasColumn(rowrom, "sha1") AndAlso Not TC.IsNullNothingOrEmpty(rowrom("sha1")) Then sha1 = rowrom("sha1").ToString.ToLower
					Dim id_Moby_Platforms As Integer = cmb_Platform.EditValue

					Dim id_rombase As Integer = DS_Rombase.Select_id_Rombase(tran, DBNull.Value, filename, size, crc, md5, sha1, id_Moby_Platforms, Nothing, Nothing)

					'Check in current records (duplicate search)
					If Not TC.IsNullNothingOrEmpty(rowrom("size")) Then
						Dim rowsrombase() As DataRow = DS_Rombase.tbl_Rombase.Select("size = " & rowrom("size"))

						If rowsrombase.Length > 0 Then
							For Each rowrombase_check As DataRow In rowsrombase
								If MKNetLib.cls_MKSQLDataAccess.HasColumn(rowrom, "md5") AndAlso Not TC.IsNullNothingOrEmpty(rowrom("md5")) AndAlso Not TC.IsNullNothingOrEmpty(rowrombase_check("md5")) AndAlso rowrom("md5").ToString.ToLower = rowrombase_check("md5").ToString.ToLower Then
									rowrombase = rowrombase_check
									Exit For
								End If

								If MKNetLib.cls_MKSQLDataAccess.HasColumn(rowrom, "sha1") AndAlso Not TC.IsNullNothingOrEmpty(rowrom("sha1")) AndAlso Not TC.IsNullNothingOrEmpty(rowrombase_check("sha1")) AndAlso rowrom("sha1").ToString.ToLower = rowrombase_check("sha1").ToString.ToLower Then
									rowrombase = rowrombase_check
									Exit For
								End If
							Next
						End If
					End If

					Try
						If rowrombase Is Nothing Then
							If id_rombase <> 0 Then
								'rombase entry found in db -> add it to the current records
								Dim dt As DataTable = DS_Rombase.Select_Rombase_Records(tran, id_rombase)
								If dt IsNot Nothing AndAlso dt.Rows.Count = 1 Then
									rowrombase = DS_Rombase.tbl_Rombase.NewRow
									rowrombase("id_rombase") = id_rombase
									rowrombase("filename") = dt.Rows(0)("filename")
									rowrombase("size") = dt.Rows(0)("size")
									rowrombase("crc") = dt.Rows(0)("crc")

									rowrombase("md5") = IIf(Not TC.IsNullNothingOrEmpty(md5), md5, dt.Rows(0)("md5"))
									rowrombase("sha1") = IIf(Not TC.IsNullNothingOrEmpty(sha1), sha1, dt.Rows(0)("sha1"))

									'rowrombase("Soundex") = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT soundex('" & dt.Rows(0)("filename").Replace("'", "''") & "')", tran)
									rowrombase("id_Moby_Platforms") = cmb_Platform.EditValue
									rowrombase("id_Moby_Releases") = dt.Rows(0)("id_Moby_Releases")
									rowrombase("Moby_Platforms_URLPart") = _Moby_Platforms_URLPart
									rowrombase("Moby_Games_URLPart") = dt.Rows(0)("Moby_Games_URLPart")
									DS_Rombase.tbl_Rombase.Rows.Add(rowrombase)
								Else
									id_rombase = 0
								End If

							End If

							'No corresponding rombase entry found -> add new
							If id_rombase = 0 Then
								rowrombase = DS_Rombase.tbl_Rombase.NewRow
								rowrombase("filename") = rowrom("name")
								rowrombase("size") = rowrom("size")
								rowrombase("crc") = rowrom("crc").ToString.ToLower
								If MKNetLib.cls_MKSQLDataAccess.HasColumn(rowrom, "md5") AndAlso Not TC.IsNullNothingOrEmpty(rowrom("md5")) Then rowrombase("md5") = rowrom("md5").ToString.ToLower
								If MKNetLib.cls_MKSQLDataAccess.HasColumn(rowrom, "sha1") AndAlso Not TC.IsNullNothingOrEmpty(rowrom("sha1")) Then rowrombase("sha1") = rowrom("sha1").ToString.ToLower
								'rowrombase("Soundex") = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT soundex('" & rowrom("name").Replace("'", "''") & "')", tran)
								rowrombase("id_Moby_Platforms") = cmb_Platform.EditValue
								rowrombase("Moby_Platforms_URLPart") = _Moby_Platforms_URLPart
								DS_Rombase.tbl_Rombase.Rows.Add(rowrombase)
								iNew += 1
							End If
						Else
							'duplicate found in datatable - get additional infos
							If MKNetLib.cls_MKSQLDataAccess.HasColumn(rowrom, "md5") AndAlso Not TC.IsNullNothingOrEmpty(rowrom("md5")) Then rowrombase("md5") = rowrom("md5")
							If MKNetLib.cls_MKSQLDataAccess.HasColumn(rowrom, "sha1") AndAlso Not TC.IsNullNothingOrEmpty(rowrom("sha1")) Then rowrombase("sha1") = rowrom("sha1")
						End If
					Catch ex As Exception
						iError += 1
					End Try

				Next
			Catch ex As Exception
				MKDXHelper.ExceptionMessageBox(ex)
			Finally
				tran.Commit()
			End Try
		End Using

		MKDXHelper.MessageBox("Import done, " & iNew & " new entries out of " & iTotal & " and " & iError & " errors!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
	End Sub

	Private Sub grd_Moby_Releases_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd_Moby_Releases.Click

	End Sub

	Private Sub bbi_Remove_Link_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Remove_Link.ItemClick
		Dim ar_Rows_Delete As New ArrayList
		For Each iRowHandle As Integer In gv_DAT.GetSelectedRows
			If iRowHandle >= 0 Then
				Dim row As DataRow = gv_DAT.GetRow(iRowHandle).Row
				If Not {DataRowState.Deleted, DataRowState.Detached}.Contains(row.RowState) Then
					ar_Rows_Delete.Add(row)
				End If
			End If
		Next

		For Each row As DataRow In ar_Rows_Delete
			row("id_Moby_Releases") = DBNull.Value
			row("Moby_Games_URLPart") = DBNull.Value
			row("deprecated") = DBNull.Value
		Next
		gv_Moby_Releases.RefreshData()
	End Sub

	Private Sub bbi_Load_XML_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Load_XML.ItemClick
		Dim sFile As String = MKNetLib.cls_MKFileSupport.OpenFileDialog(ParentForm:=Me)
		If Alphaleonis.Win32.Filesystem.File.Exists(sFile) Then
			DS_Rombase.ReadXml(sFile)
		End If

		For Each row As DataRow In DS_Rombase.tbl_Rombase.Rows
			If Not TC.IsNullNothingOrEmpty(row("crc")) Then row("crc") = row("crc").ToString.ToLower
			If Not TC.IsNullNothingOrEmpty(row("sha1")) Then row("sha1") = row("sha1").ToString.ToLower
			If Not TC.IsNullNothingOrEmpty(row("md5")) Then row("md5") = row("md5").ToString.ToLower
			row("id_Moby_Platforms") = cmb_Platform.EditValue
		Next
	End Sub

	Private Sub bbi_Write_XML_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Write_XML.ItemClick
		Dim sFile As String = MKNetLib.cls_MKFileSupport.SaveFileDialog()
		If Alphaleonis.Win32.Filesystem.File.Exists(sFile) Then
			DS_Rombase.WriteXml(sFile)
		End If
	End Sub

	Private Sub bbi_Copy_Name_to_Clipboard_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Copy_Name_to_Clipboard.ItemClick
		If BS_Rombase.Current IsNot Nothing Then
			My.Computer.Clipboard.SetText(BS_Rombase.Current("filename"))
		End If
	End Sub

	Private Function Save(Optional ByVal AskForSave As Boolean = False) As DialogResult
		If AskForSave Then
			If BS_Rombase.DataSource.Tables(BS_Rombase.DataMember).GetChanges IsNot Nothing Then
				Dim res As DialogResult = MKDXHelper.MessageBox("Do you want to save your changes?", "Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
				If res = Windows.Forms.DialogResult.Cancel Then
					Return Windows.Forms.DialogResult.Cancel
				End If
				If res = Windows.Forms.DialogResult.No Then
					Return Windows.Forms.DialogResult.No
				End If
			End If
		End If

		Cursor.Current = Cursors.WaitCursor

		Dim dt_Changes As DataTable = DS_Rombase.tbl_Rombase.GetChanges
		If dt_Changes IsNot Nothing Then
			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				For Each row As DataRow In dt_Changes.Rows
					Try
						DS_Rombase.Upsert_Rombase(tran, DBNull.Value, row("filename"), row("size"), row("crc"), row("md5"), row("sha1"), row("id_Moby_Platforms"), row("id_Moby_Releases"), row("Moby_Platforms_URLPart"), row("Moby_Games_URLPart"), CustomIdentifier:=row("CustomIdentifier"), id_rombase:=row("id_rombase"))
					Catch ex As Exception
						MKDXHelper.ExceptionMessageBox(ex)
					End Try
				Next

				tran.Commit()
			End Using

			DS_Rombase.tbl_Rombase.AcceptChanges()

			_ChangesWritten = True
		End If

		Cursor.Current = Cursors.Default

		Return Windows.Forms.DialogResult.Yes
	End Function

	Private Sub btn_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save.Click
		Save()
	End Sub

	Private Sub gv_Moby_Releases_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gv_Moby_Releases.RowCellStyle
		If BS_Rombase.Current IsNot Nothing Then
			If TC.NZ(BS_Rombase.Current("id_Moby_Releases"), 0) <> 0 Then
				If e.RowHandle >= 0 Then
					If TC.NZ(gv_Moby_Releases.GetRow(e.RowHandle)("id_Moby_Releases"), -1) = BS_Rombase.Current("id_Moby_Releases") Then
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

	Private Sub frm_Rombase_Manager_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		If BS_Rombase.DataSource.Tables(BS_Rombase.DataMember).GetChanges IsNot Nothing Then
			If Save(True) = Windows.Forms.DialogResult.Cancel Then
				e.Cancel = True
				Return
			End If
		End If

		If _ChangesWritten = True Then
			Me.DialogResult = Windows.Forms.DialogResult.OK
		End If
	End Sub

	Private Sub bbi_Delete_ItemClick(sender As Object, e As EventArgs) Handles bbi_Delete.ItemClick
		If MKDXHelper.MessageBox("Do you really want to delete the selected entries?", "Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> DialogResult.Yes Then Return

		Dim ar_Rows_Delete As New ArrayList
		For Each iRowHandle As Integer In gv_DAT.GetSelectedRows
			If iRowHandle >= 0 Then
				Dim row As DataRow = gv_DAT.GetRow(iRowHandle).Row
				If Not {DataRowState.Deleted, DataRowState.Detached}.Contains(row.RowState) Then
					ar_Rows_Delete.Add(row)
				End If
			End If
		Next

		For Each row As DataRow In ar_Rows_Delete
			If TC.NZ(row("id_Rombase"), 0) > 0 Then
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM rombase.tbl_Rombase WHERE id_Rombase = " & TC.getSQLFormat(row("id_Rombase")))
			End If
			DS_Rombase.tbl_Rombase.Rows.Remove(row)
		Next
	End Sub

	Private Sub bbi_Add_Games_from_CSV_Customidentifier_Name_ItemClick(sender As Object, e As EventArgs) Handles bbi_Add_Games_from_CSV_Customidentifier_Name.ItemClick
		Dim sFile As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("Open CSV File", "CSV Files (*.csv)|*.csv", ParentForm:=Me)

		Dim iTotal As Integer = 0
		Dim iNew As Integer = 0
		Dim iError As Integer = 0

		If Alphaleonis.Win32.Filesystem.File.Exists(sFile) Then
			Dim sContent As String = MKNetLib.cls_MKFileSupport.GetFileContents(sFile)
			Dim sLines As String() = sContent.Split(ControlChars.CrLf)
			iTotal = sLines.Length

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				Try
					For Each sLine As String In sLines
						sLine = sLine.Trim
						Dim CustomIdentifier = sLine.Split(";")(0).ToUpper.Replace("-", "").Replace(" ", "").Trim

						Dim id_Moby_Platforms As Integer = cmb_Platform.EditValue

						If {cls_Globals.enm_Moby_Platforms.gc, cls_Globals.enm_Moby_Platforms.wii}.Contains(id_Moby_Platforms) Then
							If id_Moby_Platforms = cls_Globals.enm_Moby_Platforms.gc AndAlso Not {"G", "D"}.Contains(CustomIdentifier(0)) Then
								Continue For 'Skip entry, because GameCube games always have "G" or "D" as first letter
							Else
								If id_Moby_Platforms = cls_Globals.enm_Moby_Platforms.wii AndAlso {"G", "D"}.Contains(CustomIdentifier(0)) Then
									Continue For 'Skip entry, because Wii games don't start with "G" or "D"
								End If
							End If
						End If

						Dim filename = sLine.Split(";")(1)

						If filename.Trim.Length = 0 Then
							Continue For
						End If

						Dim id_rombase As Integer = DS_Rombase.Select_id_Rombase(tran, DBNull.Value, filename, Nothing, Nothing, Nothing, Nothing, id_Moby_Platforms, Nothing, CustomIdentifier)

						'Check in current records (duplicate search)
						Dim rowsrombase() As DataRow = DS_Rombase.tbl_Rombase.Select("CustomIdentifier = " & TC.getSQLFormat(CustomIdentifier))

						If rowsrombase.Length = 0 Then
							Dim row As DataRow = DS_Rombase.tbl_Rombase.NewRow
							row("CustomIdentifier") = CustomIdentifier
							row("filename") = filename
							row("id_Moby_Platforms") = id_Moby_Platforms
							row("Moby_Platforms_URLPart") = _Moby_Platforms_URLPart
							DS_Rombase.tbl_Rombase.Rows.Add(row)

							iNew += 1
						End If
					Next
				Catch ex As Exception
					iError += 1
				Finally
					tran.Commit()
				End Try
			End Using

			MKDXHelper.MessageBox("Import done, " & iNew & " new entries out of " & iTotal & " and " & iError & " errors!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
		End If
	End Sub

	Private Sub bbi_Open_Moby_Page_ItemClick(sender As Object, e As EventArgs) Handles bbi_Open_Moby_Page.ItemClick
		If BS_Moby_Releases.Current Is Nothing Then Return

		Try
			Dim sURL As String = "http://www.mobygames.com/game/" & BS_Moby_Platforms.Current("URLPart") & "/" & TC.NZ(BS_Moby_Releases.Current("Moby_Games_URLPart"), "").Replace("\", "")
			Dim procinfo As New ProcessStartInfo(sURL)
			procinfo.UseShellExecute = True
			Process.Start(procinfo)
		Catch ex As Exception

		End Try
	End Sub

	Private Sub Add_from_SegaRetro(sender As Object, e As EventArgs) Handles bbi_Add_Games_from_CSV_SegaCD.ItemClick, bbi_Add_Games_from_CSV_Saturn.ItemClick, bbi_Add_Games_from_CSV_DreamCast.ItemClick
		Dim sFile As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("Open CSV File", "CSV Files (*.csv)|*.csv", ParentForm:=Me)

		Dim sPlatform = "SCD"

		If sender Is bbi_Add_Games_from_CSV_Saturn Then
			sPlatform = "SAT"
		End If

		If sender Is bbi_Add_Games_from_CSV_DreamCast Then
			sPlatform = "DC"
		End If

		Dim iTotal As Integer = 0
		Dim iNew As Integer = 0
		Dim iError As Integer = 0

		Dim sCollisions As String = ""

		If Alphaleonis.Win32.Filesystem.File.Exists(sFile) Then
			Dim sContent As String = MKNetLib.cls_MKFileSupport.GetFileContents(sFile)
			Dim sLines As String() = sContent.Split(ControlChars.CrLf)
			iTotal = sLines.Length

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				Try
					Dim filename As String = ""

					For Each sLine As String In sLines
						sLine = sLine.Trim
						Dim sFields() As String = sLine.Split(";")

						If sFields(0).Trim.Length > 0 Then
							filename = sFields(0).Trim 'Follow-up line could have empty filename
						End If

						If filename = "" Then Continue For

						Dim arCustomIdentifier As New ArrayList
						For i = 1 To sFields.Count - 1
							Dim sCustomIdentifier = sFields(i)

							If sPlatform = "SCD" Then
								If MKNetLib.cls_MKRegex.IsMatch(sCustomIdentifier, "\d+") Then
									arCustomIdentifier.Add(MKNetLib.cls_MKRegex.GetMatches(sCustomIdentifier, "\d+")(0).Value)
								End If
							End If

							If sPlatform = "SAT" Then
								sCustomIdentifier = sCustomIdentifier.Trim
								If sCustomIdentifier.Length > 0 Then
									If sCustomIdentifier.Contains("(") Then
										sCustomIdentifier = sCustomIdentifier.Substring(0, sCustomIdentifier.IndexOf("("))
									End If

									arCustomIdentifier.Add(sCustomIdentifier.Trim.Replace("-", "").Replace(" ", "").ToUpper)

									'Check if the last 3 characters are "-nn" where nn is a number (european regions are not part of the identifier on the cd)
									If sCustomIdentifier.Length > 3 Then
										If MKNetLib.cls_MKRegex.IsMatch(sCustomIdentifier.Substring(sCustomIdentifier.Length - 3, 3), "\-\d\d") Then
											sCustomIdentifier = sCustomIdentifier.Substring(0, sCustomIdentifier.Length - 3)
											arCustomIdentifier.Add(sCustomIdentifier.Trim.Replace("-", "").Replace(" ", "").ToUpper)
										End If
									End If
								End If
							End If

							If sPlatform = "DC" Then
								Dim sField As String = sFields(i)

								For Each sIdentifier As String In sField.Split(",")
									sIdentifier = sIdentifier.Trim
									If sIdentifier.Length > 0 Then
										arCustomIdentifier.Add(sCustomIdentifier.Trim.Replace("-", "").Replace(" ", "").ToUpper)

										'Check if the last 3 characters are "-??" where ?? are numbers or letters
										If sCustomIdentifier.Length > 3 Then
											If MKNetLib.cls_MKRegex.IsMatch(sCustomIdentifier.Substring(sCustomIdentifier.Length - 3, 3), "\-..") Then
												sCustomIdentifier = sCustomIdentifier.Substring(0, sCustomIdentifier.Length - 3)
												arCustomIdentifier.Add(sCustomIdentifier.Trim.Replace("-", "").Replace(" ", "").ToUpper)
											End If
										End If
									End If
								Next


							End If
						Next

						Dim id_Moby_Platforms As Integer = cmb_Platform.EditValue

						For Each CustomIdentifier In arCustomIdentifier
							'Check in current records (duplicate search)
							Dim rowsrombase() As DataRow = DS_Rombase.tbl_Rombase.Select("CustomIdentifier = " & TC.getSQLFormat(CustomIdentifier))

							If rowsrombase.Length = 0 Then
								Dim row As DataRow = DS_Rombase.tbl_Rombase.NewRow
								row("CustomIdentifier") = CustomIdentifier
								row("filename") = filename
								row("id_Moby_Platforms") = id_Moby_Platforms
								row("Moby_Platforms_URLPart") = _Moby_Platforms_URLPart
								DS_Rombase.tbl_Rombase.Rows.Add(row)

								iNew += 1
							Else
								Dim sMessage As String = ""
								sMessage &= "Found " & rowsrombase.Count & " matches for Identifier '" & CustomIdentifier & " of " & filename & ControlChars.CrLf
								sMessage &= "- " & filename & ControlChars.CrLf
								For Each rowrombase As DataRow In rowsrombase
									sMessage &= "- " & rowrombase("filename") & ControlChars.CrLf
								Next

								sCollisions &= sMessage & ControlChars.CrLf
							End If
						Next
					Next
				Catch ex As Exception
					iError += 1
				Finally
					tran.Commit()
				End Try
			End Using

			MKDXHelper.MessageBox("Import done, " & iNew & " new entries out of " & iTotal & " and " & iError & " errors!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		End If
	End Sub

	Private Sub bbi_Evaluate_Moby_Links_ItemClick(sender As Object, e As EventArgs) Handles bbi_Evaluate_Moby_Links.ItemClick
		Cursor = Cursors.WaitCursor

		Dim ar_Have As New ArrayList
		For Each row_Rombase As DataRow In Me.DS_Rombase.Tables("tbl_Rombase").Rows
			If Not {DataRowState.Deleted, DataRowState.Detached}.Contains(row_Rombase.RowState) Then
				Dim Moby_Games_URLPart As String = TC.NZ(row_Rombase("Moby_Games_URLPart"), "")
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

		For Each have As String In ar_Have
			If Not ar_Moby_Total.Contains(have) Then
				Debug.WriteLine("Have not in Total: " & have)
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

	Private Sub bbi_Auto_Link_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Auto_Link.ItemClick
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

		Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Nothing), 400, 60, ProgressBarStyle.Blocks, False, "Preparing Rombase data ...", 0, Me.DS_Rombase.tbl_Rombase.Rows.Count, False)
		prg.Start()

		For Each row_Rombase As DS_Rombase.tbl_RombaseRow In Me.DS_Rombase.tbl_Rombase.Rows
			prg.IncreaseCurrentValue()

			Dim doCheck As Boolean = False

			If TC.NZ(row_Rombase("Moby_Games_URLPart"), "") = "" Then
				doCheck = True
			ElseIf autolinkOptions.Redetect_Deprecated AndAlso TC.NZ(row_Rombase("deprecated"), False) = True Then
				doCheck = True
			End If

			If doCheck Then
				Dim row_Auto_Link As DS_ML.tbl_Moby_Auto_LinkRow = tbl_Moby_Auto_Link.NewRow
				row_Auto_Link.id = row_Rombase.id_rombase

				If TC.NZ(row_Rombase("CustomIdentifier"), "") <> "" Then
					row_Auto_Link.Identifier = row_Rombase("CustomIdentifier")
				Else
					row_Auto_Link.Identifier = TC.NZ(row_Rombase("crc"), "")
				End If

				row_Auto_Link.GameName = row_Rombase.filename

				tbl_Moby_Auto_Link.Rows.Add(row_Auto_Link)
			End If
		Next

		prg.Close()

		If tbl_Moby_Auto_Link.Rows.Count = 0 Then
			MKDXHelper.MessageBox("All entries are already linked, no need for an auto link.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

		sExplanation = "The left list shows all the RomBase entries that had previously missing MobyGames links. If a match with a MobyGames release has been found, the corresponding fields (Moby Gamename, Match Accuracy etc.) have values. If the match accuracy is exactly 100%, the link is automatically set to be applied (see Apply column). Please thoroughly review these results and check/uncheck the Apply checkbox (by click or by pressing Enter). You can also re-link with another MobyGames release by doubleclicking the release on the right list."

		Using frm As New frm_Moby_Auto_Link(tbl_Moby_Auto_Link, src_Moby_Releases, sExplanation, autolinkOptions)
			If frm.ShowDialog() = DialogResult.OK Then
				Dim iLinkCount As Integer = 0

				For Each rowAutoLink As DS_ML.tbl_Moby_Auto_LinkRow In frm.DS_ML.tbl_Moby_Auto_Link.Select("Apply = 1")
					Dim rowsRombase() As DS_Rombase.tbl_RombaseRow = Me.DS_Rombase.tbl_Rombase.Select("id_Rombase = " & TC.getSQLFormat(rowAutoLink.id))

					If rowsRombase.Length = 1 Then
						iLinkCount += 1
						rowsRombase(0)("Moby_Games_URLPart") = rowAutoLink("Match_Moby_Games_URLPart").Replace("\", "")
						rowsRombase(0)("id_Moby_Releases") = rowAutoLink("Match_id_Moby_Releases")

					End If
				Next

				MKDXHelper.MessageBox(iLinkCount & " links have been applied.", "Auto-Link", MessageBoxButtons.OK, MessageBoxIcon.Information)
			End If
		End Using
	End Sub

	Private Function add_Game(ByRef tran As SQLite.SQLiteTransaction, ByVal GameName As String, ByVal Serial As String) As Integer
		If GameName = "" OrElse Serial = "" Then
			Return -1
		End If

		Dim id_Moby_Platforms As Integer = cmb_Platform.EditValue

		'Check in current records (duplicate search)
		Dim rowsrombase() As DataRow = DS_Rombase.tbl_Rombase.Select("CustomIdentifier = " & TC.getSQLFormat(Serial))

		If rowsrombase.Length = 0 Then
			Dim row As DS_Rombase.tbl_RombaseRow = DS_Rombase.tbl_Rombase.NewRow
			row("CustomIdentifier") = Serial
			row("filename") = GameName
			row("id_Moby_Platforms") = id_Moby_Platforms
			row("Moby_Platforms_URLPart") = _Moby_Platforms_URLPart
			DS_Rombase.tbl_Rombase.Rows.Add(row)

			Return 1
		Else
			For Each rowrombase As DS_Rombase.tbl_RombaseRow In rowsrombase
				rowrombase.filename = GameName
			Next

			Return 0
		End If

		Return 0
	End Function

	Private Sub bbi_Add_Games_from_DAT_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Add_Games_from_DAT.ItemClick
		Dim ds As New DataSet
		Dim sFile As String = MKNetLib.cls_MKFileSupport.OpenFileDialog(ParentForm:=Me)

		If Not Alphaleonis.Win32.Filesystem.File.Exists(sFile) Then
			Return
		End If

		Dim iTotal As Integer = 0
		Dim iNew As Integer = 0
		Dim iError As Integer = 0

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			Try
				Dim objReader As New System.IO.StreamReader(sFile)

				Dim attSerial As String = ""
				Dim attGameName As String = ""

				Dim result As Integer

				Do While objReader.Peek() <> -1
					Dim line As String = objReader.ReadLine().Trim
					Dim line_lower = line.ToLower

					If line_lower.StartsWith("game (") Then
						result = add_Game(tran, attGameName, attSerial)
						iTotal += 1
						Select Case result
							Case -1
								iError += 1
							Case 1
								iNew += 1
							Case 0
						End Select

						attSerial = ""
						attGameName = ""
					End If

					If line_lower.StartsWith("name") Then
						If MKNetLib.cls_MKRegex.IsMatch(line, "\""(.*?)\""") Then
							attGameName = MKNetLib.cls_MKRegex.GetMatches(line, "\""(.*?)\""")(0).Groups(1).Value.Trim
						End If
					End If

					If line_lower.StartsWith("serial") Then
						If MKNetLib.cls_MKRegex.IsMatch(line, "\""(.*?)\""") Then
							attSerial = MKNetLib.cls_MKRegex.GetMatches(line, "\""(.*?)\""")(0).Groups(1).Value.Replace("-", "").Replace(" ", "").Trim.ToUpper
						End If
					End If
				Loop

				result = add_Game(tran, attGameName, attSerial)
				iTotal += 1
				Select Case result
					Case -1
						iError += 1
					Case 1
						iNew += 1
					Case 0
				End Select

				iTotal -= 1
				iError -= 1
			Catch ex As Exception
				MKDXHelper.ExceptionMessageBox(ex)
			Finally
				tran.Commit()
			End Try
		End Using

		MKDXHelper.MessageBox("Import done, " & iNew & " new entries out of " & iTotal & " and " & iError & " errors!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
	End Sub

	Private Sub gv_DAT_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_DAT.FocusedRowChanged
		Dim gv As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

		If TC.NZ(gv.GetIncrementalText(), "") <> "" Then
			gv.ClearSelection()
			gv.SelectRow(gv.FocusedRowHandle)
		End If
	End Sub
End Class
