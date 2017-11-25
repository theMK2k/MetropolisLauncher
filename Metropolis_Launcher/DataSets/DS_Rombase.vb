Imports TC = MKNetLib.cls_MKTypeConverter
Imports DataAccess = MKNetLib.cls_MKSQLiteDataAccess

Partial Class DS_Rombase
	Partial Public Class tbl_Rombase_Known_EmulatorsDataTable
	End Class

#Region "Select Statements"
	Public Shared Function Select_id_Rombase(ByRef tran As SQLite.SQLiteTransaction, ByVal Mapping_Identifier As Object, ByVal filename As Object, ByVal size As Object, ByVal crc As Object, ByVal md5 As Object, ByVal sha1 As Object, ByVal id_Moby_Platforms As Object, ByVal id_Moby_Releases As Object, ByVal CustomIdentifier As Object, Optional ByVal id_Rombase_Owner As Object = Nothing) As Integer
		Dim dt As DataTable

		If TC.NZ(Mapping_Identifier, "").Length > 0 Then
			dt = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT id_rombase, filename, size, crc, md5, sha1, id_Moby_Platforms, id_Moby_Releases FROM tbl_Rombase WHERE Mapping_Identifier = " & TC.getSQLFormat(Mapping_Identifier) & IIf(TC.NZ(id_Rombase_Owner, 0) > 0, " AND id_Rombase_Owner = " & TC.getSQLFormat(id_Rombase_Owner), ""), Nothing, tran)
			If dt.Rows.Count = 1 Then
				Return dt.Rows(0)("id_rombase")
			Else
				Return 0
			End If
		End If

		If TC.NZ(CustomIdentifier, "").Length > 0 AndAlso TC.NZ(id_Moby_Platforms, 0) <> 0 Then
			dt = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT id_rombase, filename, size, crc, md5, sha1, id_Moby_Platforms, id_Moby_Releases FROM tbl_Rombase WHERE CustomIdentifier = " & TC.getSQLFormat(CustomIdentifier) & " AND id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms) & IIf(TC.NZ(id_Rombase_Owner, 0) > 0, " AND id_Rombase_Owner = " & TC.getSQLFormat(id_Rombase_Owner), ""), Nothing, tran)
			If dt.Rows.Count = 1 Then
				Return dt.Rows(0)("id_rombase")
			Else
				Return 0
			End If
		End If

		dt = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT id_rombase, filename, size, crc, md5, sha1, id_Moby_Platforms, id_Moby_Releases FROM tbl_Rombase WHERE size = " & TC.getSQLFormat(size) & " AND ( 0=1 " & IIf(TC.NZ(crc, "").Length > 0, " OR crc = " & TC.getSQLFormat(crc), "") & IIf(TC.NZ(sha1, "").Length > 0, " OR sha1 = " & TC.getSQLFormat(sha1), "") & IIf(TC.NZ(md5, "").Length > 0, " OR md5 = " & TC.getSQLFormat(md5), "") & ") " & IIf(False, " AND id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms), "") & IIf(TC.NZ(id_Rombase_Owner, 0) > 0, " AND id_Rombase_Owner = " & TC.getSQLFormat(id_Rombase_Owner), ""), Nothing, tran)

		Dim row As DataRow = Nothing

		If dt.Rows.Count = 1 Then
			Return TC.NZ(dt.Rows(0)("id_rombase"), 0)
		End If

		For Each rowtemp As DataRow In dt.Rows
			If Not TC.IsNullNothingOrEmpty(md5) AndAlso Not TC.IsNullNothingOrEmpty(rowtemp("md5")) Then
				If md5 = rowtemp("md5") Then
					row = rowtemp
					Exit For
				Else
					'MD5 Checksum error
					Continue For
				End If
			End If

			If Not TC.IsNullNothingOrEmpty(sha1) AndAlso Not TC.IsNullNothingOrEmpty(rowtemp("sha1")) Then
				If sha1 = rowtemp("sha1") Then
					row = rowtemp
					Exit For
				Else
					'SHA1 Checksum error
					Continue For
				End If
			End If

			'Last resort: crc
			If TC.NZ(crc, "no1") = TC.NZ(rowtemp("crc"), "no2") Then
				row = rowtemp
				Exit For
			End If
		Next

		If row IsNot Nothing Then
			Return TC.NZ(row("id_rombase"), 0)
		Else
			Return 0
		End If
	End Function

	Public Function Select_Rombase_Records(ByRef tran As SQLite.SQLiteTransaction, ByVal id_rombase As Integer) As DataTable
		Dim dt As DataTable = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT id_rombase, filename, size, crc, md5, sha1, id_Moby_Platforms, id_Moby_Releases, Moby_Games_URLPart FROM tbl_Rombase WHERE id_rombase = " & TC.getSQLFormat(id_rombase), Nothing, tran)
		Return dt
	End Function
#End Region

#Region "Fill Statements"
	Public Sub Fill_tbl_Rombase_Known_Emulators(ByRef dt As DS_Rombase.tbl_Rombase_Known_EmulatorsDataTable)
		Dim sSQL As String = ""
		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= "	id_Rombase_Known_Emulators" & ControlChars.CrLf
		sSQL &= "	, Identifier" & ControlChars.CrLf
		sSQL &= "	, Name" & ControlChars.CrLf
		sSQL &= "	, Description" & ControlChars.CrLf
		sSQL &= "	, Autoconfig_Note" & ControlChars.CrLf
		sSQL &= "	, Exe_Identifier_Regex" & ControlChars.CrLf
		sSQL &= "	, URL_Website" & ControlChars.CrLf
		sSQL &= "	, URL_Download" & ControlChars.CrLf
		sSQL &= "	, StartupParameter" & ControlChars.CrLf
		sSQL &= "	, ScreenshotDirectory" & ControlChars.CrLf
		sSQL &= "	, id_List_Generators" & ControlChars.CrLf
		sSQL &= "FROM rombase.tbl_Rombase_Known_Emulators" & ControlChars.CrLf
		sSQL &= "ORDER BY Name" & ControlChars.CrLf

		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

	Public Sub Fill_src_frm_Known_Emulators_Moby_Platforms(ByRef dt As DS_Rombase.src_frm_Known_Emulators_Moby_PlatformsDataTable, ByVal id_Rombase_Known_Emulators As Integer)
		dt.Clear()
		Dim sSQL As String =
		"	SELECT" &
		"		PLTFM.id_Moby_Platforms" &
		"		, PLTFM.Display_Name" &
		"		, CASE WHEN EMUPLTFM.id_Moby_Platforms IS NOT NULL THEN 1 ELSE 0 END AS Supported" &
		"	FROM moby.tbl_Moby_Platforms PLTFM" &
		"	LEFT JOIN rombase.tbl_Rombase_Known_Emulators_Moby_Platforms EMUPLTFM ON PLTFM.id_Moby_Platforms = EMUPLTFM.id_Moby_Platforms AND EMUPLTFM.id_Rombase_Known_Emulators = " & TC.getSQLFormat(id_Rombase_Known_Emulators) &
		" LEFT JOIN main.tbl_Moby_Platforms_Settings PLTFMS ON PLTFM.id_Moby_Platforms = PLTFMS.id_Moby_Platforms " &
		"	WHERE PLTFM.Visible = 1" &
		"				AND PLTFM.GenericEmulated = 1" &
		"				AND PLTFM.id_Moby_Platforms_Owner IS NULL" &
		"	ORDER BY Display_Name"
		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

	Public Sub Fill_src_frm_Known_Emulators_Multivolume_Parameters(ByRef dt As tbl_Rombase_Known_Emulators_Multivolume_ParametersDataTable, ByVal id_Rombase_Known_Emulators As Integer) 'tbl_Emulators_Multivolume_ParametersDataTable
		dt.Clear()
		Dim sSQL As String =
		"	SELECT" &
		"		id_Rombase_Known_Emulators_Multivolume_Parameters" &
		"		, id_Rombase_Known_Emulators" &
		"		, Volume_Number" &
		"		, Parameter" &
		"	FROM rombase.tbl_Rombase_Known_Emulators_Multivolume_Parameters" &
		"	WHERE id_Rombase_Known_Emulators = " & TC.getSQLFormat(id_Rombase_Known_Emulators) &
		"	ORDER BY Volume_Number"
		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

#End Region

#Region "Upsert Statements"
	Public Shared Function Upsert_Rombase(ByRef tran As SQLite.SQLiteTransaction, ByVal Mapping_Identifier As Object, ByVal filename As Object, ByVal size As Object, ByVal crc As Object, ByVal md5 As Object, ByVal sha1 As Object, ByVal id_Moby_Platforms As Object, ByVal id_Moby_Releases As Object, ByVal Moby_Platforms_URLPart As Object, ByVal Moby_Games_URLPart As Object, Optional ByVal id_Rombase_Owner As Object = Nothing, Optional ByVal CustomIdentifier As Object = Nothing, Optional ByVal id_rombase As Object = Nothing, Optional ByVal id_Moby_Platforms_Alternative As Object = Nothing) As Integer
		Dim sSQL = ""

		If TC.NZ(Mapping_Identifier, "") = "" Then
			If TC.NZ(id_rombase, 0) < 0 Then
				id_rombase = 0
			End If
		End If

		id_rombase = TC.NZ(id_rombase, 0)
		If id_rombase = 0 Then
			Dim obj_id_rombase = Select_id_Rombase(tran, Mapping_Identifier, filename, size, crc, md5, sha1, id_Moby_Platforms, id_Moby_Releases, CustomIdentifier, id_Rombase_Owner)
			If TC.NZ(obj_id_rombase, 0) <> 0 Then
				id_rombase = obj_id_rombase
			End If
		End If

		If id_rombase <> 0 Then
			'Update
			sSQL = "UPDATE tbl_Rombase SET "
			sSQL &= "  Mapping_Identifier = " & TC.getSQLFormat(Mapping_Identifier)
			sSQL &= ", filename = " & TC.getSQLFormat(filename)
			sSQL &= ", size = " & TC.getSQLFormat(size)
			sSQL &= ", crc = " & TC.getSQLFormat(crc)
			sSQL &= ", md5 = " & TC.getSQLFormat(md5)
			sSQL &= ", sha1 = " & TC.getSQLFormat(sha1)
			sSQL &= ", id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms)
			sSQL &= ", id_Moby_Platforms_Alternative = " & TC.getSQLFormat(id_Moby_Platforms_Alternative)
			sSQL &= ", id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases)
			sSQL &= ", Moby_Platforms_URLPart = " & TC.getSQLFormat(Moby_Platforms_URLPart)
			sSQL &= ", Moby_Games_URLPart = " & TC.getSQLFormat(Moby_Games_URLPart)
			sSQL &= ", id_Rombase_Owner = " & TC.getSQLFormat(id_Rombase_Owner)
			sSQL &= ", CustomIdentifier = " & TC.getSQLFormat(CustomIdentifier)
			sSQL &= " WHERE id_Rombase = " & TC.getSQLFormat(id_rombase)

			DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)

			Return id_rombase
		End If

		Dim new_id_rombase As Integer = 0
		If TC.NZ(Mapping_Identifier, "").Length > 0 Then
			new_id_rombase = Math.Min(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT MIN(id_rombase) FROM tbl_rombase", tran), 0) - 1
		End If

		sSQL = "INSERT INTO tbl_Rombase (" & IIf(TC.NZ(Mapping_Identifier, "").Length > 0, "id_rombase, ", "") & "Mapping_Identifier, filename, size, crc, md5, sha1, id_Moby_Platforms, id_Moby_Releases, Moby_Platforms_URLPart, Moby_Games_URLPart, id_Rombase_Owner, CustomIdentifier, id_Moby_Platforms_Alternative) VALUES (" _
		& IIf(TC.NZ(Mapping_Identifier, "").Length > 0, TC.getSQLFormat(new_id_rombase) & ", ", "") _
		& TC.getSQLFormat(Mapping_Identifier) _
		& ", " & TC.getSQLFormat(filename) _
		& ", " & TC.getSQLFormat(size) _
		& ", " & TC.getSQLFormat(crc) _
		& ", " & TC.getSQLFormat(md5) _
		& ", " & TC.getSQLFormat(sha1) _
		& ", " & TC.getSQLFormat(id_Moby_Platforms) _
		& ", " & TC.getSQLFormat(id_Moby_Releases) _
		& ", " & TC.getSQLFormat(Moby_Platforms_URLPart) _
		& ", " & TC.getSQLFormat(Moby_Games_URLPart) _
		& ", " & TC.getSQLFormat(id_Rombase_Owner) _
		& ", " & TC.getSQLFormat(CustomIdentifier) _
		& ", " & TC.getSQLFormat(id_Moby_Platforms_Alternative) _
		& "); SELECT last_insert_rowid()"

		'Insert
		Return TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, sSQL, tran), 0)
		'dt = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT id_rombase, filename, size, crc, md5, sha1, id_Moby_Platforms, id_Moby_Releases FROM tbl_Rombase WHERE size = " & TC.getSQLFormat(size) & " AND crc = " & TC.getSQLFormat(crc) & " AND id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms), Nothing, tran)

		'Return Select_id_Rombase(tran, Mapping_Identifier, filename, size, crc, md5, sha1, id_Moby_Platforms, id_Moby_Releases)
	End Function
#End Region

End Class
