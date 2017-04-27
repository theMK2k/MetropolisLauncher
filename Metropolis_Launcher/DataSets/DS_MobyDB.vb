Imports TC = MKNetLib.cls_MKTypeConverter
Imports DataAccess = MKNetLib.cls_MKSQLiteDataAccess

Partial Class DS_MobyDB
	'Actual IDs on the mobygames database:
	'attributeId = id_Moby_Attributes
	'genreId = id_Moby_Genres

#Region "Select Statements"
	Public Function Select_id_Moby_Genres(ByRef tran As SQLite.SQLiteTransaction, ByVal URLPart As String) As Integer
		Return TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Genres FROM tbl_Moby_Genres WHERE URLPart = " & TC.getSQLFormat(URLPart) & " LIMIT 1", tran), 0)
	End Function

	Public Function Select_id_Moby_Attributes(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Moby_Attributes As Integer) As Integer
		Return TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Attributes FROM tbl_Moby_Attributes WHERE id_Moby_Attributes = " & TC.getSQLFormat(id_Moby_Attributes) & " LIMIT 1", tran), 0)
	End Function

	Public Function Select_id_Moby_Staff(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Moby_Staff As Integer) As Integer
		Return TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Staff FROM tbl_Moby_Staff WHERE id_Moby_Staff = " & TC.getSQLFormat(id_Moby_Staff) & " LIMIT 1", tran), 0)
	End Function

	Public Function Select_id_Moby_Attributes_Categories(ByRef tran As SQLite.SQLiteTransaction, ByVal Name As String) As Integer
		Return TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Attributes_Categories FROM tbl_Moby_Attributes_Categories WHERE Name = " & TC.getSQLFormat(Name) & " LIMIT 1", tran), 0)
	End Function
#End Region

#Region "Upsert Statements"
	Public Function Upsert_Moby_Games(ByRef tran As SQLite.SQLiteTransaction, ByVal URLPart As String, ByVal Name As String, ByVal Description As String, Optional ByVal Prefix As Object = Nothing) As Integer
		Dim id_Moby_Games As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Games FROM tbl_Moby_Games WHERE URLPart = " & TC.getSQLFormat(URLPart) & " LIMIT 1", tran), 0)

		If id_Moby_Games > 0 Then
			DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Moby_Games SET Name = " & TC.getSQLFormat(Name) & ", Description = " & TC.getSQLFormat(Description) & ", Name_Prefix = " & TC.getSQLFormat(Prefix) & " WHERE id_Moby_Games = " & TC.getSQLFormat(id_Moby_Games), tran)
			Return id_Moby_Games
		End If

		DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Moby_Games (URLPart, Name, Description, Name_Prefix) VALUES (" & TC.getSQLParameter(URLPart, Name, Description, Prefix) & ")", tran)

		Return DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Games FROM tbl_Moby_Games WHERE URLPart = " & TC.getSQLFormat(URLPart) & " LIMIT 1", tran)
	End Function

	Public Function Upsert_Moby_Companies(ByRef tran As SQLite.SQLiteTransaction, ByVal URLPart As String, ByVal Name As String) As Integer
		Dim id_Moby_Companies As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Companies FROM tbl_Moby_Companies WHERE URLPart = " & TC.getSQLFormat(URLPart) & " AND Name = " & TC.getSQLParameter(Name) & " LIMIT 1", tran), 0)

		If id_Moby_Companies > 0 Then
			DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Moby_Companies SET Name = " & TC.getSQLFormat(Name) & " WHERE id_Moby_Companies = " & TC.getSQLFormat(id_Moby_Companies), tran)
			Return id_Moby_Companies
		End If

		DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Moby_Companies (URLPart, Name) VALUES (" & TC.getSQLParameter(URLPart, Name) & ")", tran)

		Return DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Companies FROM tbl_Moby_Companies WHERE URLPart = " & TC.getSQLFormat(URLPart) & " AND Name = " & TC.getSQLParameter(Name) & " LIMIT 1", tran)
	End Function

	Public Function Upsert_Moby_Attributes_Categories(ByRef tran As SQLite.SQLiteTransaction, ByVal Name As String, Optional ByVal RatingSystem As Boolean = False) As Integer
		Dim id_Moby_Attributes_Categories As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Attributes_Categories FROM tbl_Moby_Attributes_Categories WHERE Name = " & TC.getSQLFormat(Name) & " LIMIT 1", tran), 0)

		If id_Moby_Attributes_Categories > 0 Then
			DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Moby_Attributes_Categories SET Name = " & TC.getSQLFormat(Name) & ", RatingSystem = " & TC.getSQLFormat(RatingSystem) & " WHERE id_Moby_Attributes_Categories = " & TC.getSQLFormat(id_Moby_Attributes_Categories), tran)
			Return id_Moby_Attributes_Categories
		End If

		DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Moby_Attributes_Categories (Name, RatingSystem) VALUES (" & TC.getSQLFormat(Name) & ", " & TC.getSQLFormat(RatingSystem) & ")", tran)

		Return DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Attributes_Categories FROM tbl_Moby_Attributes_Categories WHERE Name = " & TC.getSQLFormat(Name) & " LIMIT 1", tran)
	End Function

	Public Function Upsert_Moby_Attributes(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Moby_Attributes As Integer, ByVal id_Moby_Attributes_Categories As Integer, ByVal Name As String, ByVal Description As String, Optional ByVal Rating_Age_From As Object = Nothing) As Integer
		Dim id_Moby_Attributes_DB As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Attributes FROM tbl_Moby_Attributes WHERE id_Moby_Attributes = " & TC.getSQLFormat(id_Moby_Attributes) & " LIMIT 1", tran), 0)

		If id_Moby_Attributes_DB > 0 Then
			DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Moby_Attributes SET Name = " & TC.getSQLFormat(Name) & ", Description = " & TC.getSQLFormat(Description) & IIf(Rating_Age_From IsNot Nothing, ", Rating_Age_From = " & TC.getSQLFormat(Rating_Age_From), "") & " WHERE id_Moby_Attributes = " & TC.getSQLFormat(id_Moby_Attributes), tran)
			Return id_Moby_Attributes_DB
		End If

		DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Moby_Attributes (id_Moby_Attributes, id_Moby_Attributes_Categories, Name, Description, Rating_Age_From) VALUES (" & TC.getSQLParameter(id_Moby_Attributes, id_Moby_Attributes_Categories, Name, Description, Rating_Age_From) & ")", tran)

		Return DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Attributes FROM tbl_Moby_Attributes WHERE id_Moby_Attributes = " & TC.getSQLFormat(id_Moby_Attributes) & " LIMIT 1", tran)
	End Function

	Public Function Upsert_Moby_Releases(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Moby_Games As Integer, ByVal id_Moby_Platforms As Integer, ByVal MobyRank As Object, ByVal MobyScore As Object, ByVal Publisher_id_Moby_Companies As Object, ByVal Developer_id_Moby_Companies As Object, ByVal URL As String, ByVal Technical_Notes As Object, ByVal Year As Object) As Integer
		Dim id_Moby_Releases As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Releases FROM tbl_Moby_Releases WHERE id_Moby_Games = " & TC.getSQLFormat(id_Moby_Games) & " AND id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms) & " LIMIT 1", tran), 0)

		If id_Moby_Releases > 0 Then
			DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Moby_Releases SET MobyRank = " & TC.getSQLFormat(MobyRank) & ", MobyScore = " & TC.getSQLFormat(MobyScore) & ", Publisher_id_Moby_Companies = " & TC.getSQLFormat(Publisher_id_Moby_Companies) & ", Developer_id_Moby_Companies = " & TC.getSQLFormat(Developer_id_Moby_Companies) & ", URL = " & TC.getSQLFormat(URL) & ", Technical_Notes = " & TC.getSQLFormat(Technical_Notes) & ", Year = " & TC.getSQLFormat(Year) & " WHERE id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases), tran)
			Return id_Moby_Releases
		End If

		DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Moby_Releases (id_Moby_Games, id_Moby_Platforms, MobyRank, MobyScore, Publisher_id_Moby_Companies, Developer_id_Moby_Companies, URL, Technical_Notes, Year) VALUES (" & TC.getSQLParameter(id_Moby_Games, id_Moby_Platforms, MobyRank, MobyScore, Publisher_id_Moby_Companies, Developer_id_Moby_Companies, URL, Technical_Notes, Year) & ")", tran)

		Return DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Releases FROM tbl_Moby_Releases WHERE id_Moby_Games = " & TC.getSQLFormat(id_Moby_Games) & " AND id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms) & " LIMIT 1", tran)
	End Function

	Public Function Upsert_Moby_Releases_Attributes(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Moby_Releases As Integer, ByVal id_Moby_Attributes As String) As Integer
		Dim id_Moby_Releases_Attributes As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Releases_Attributes FROM tbl_Moby_Releases_Attributes WHERE id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases) & " AND id_Moby_Attributes = " & TC.getSQLFormat(id_Moby_Attributes) & " LIMIT 1", tran), 0)

		If id_Moby_Releases_Attributes > 0 Then
			Return id_Moby_Releases_Attributes
		End If

		DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Moby_Releases_Attributes (id_Moby_Releases, id_Moby_Attributes) VALUES (" & TC.getSQLParameter(id_Moby_Releases, id_Moby_Attributes) & ")", tran)

		Return DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Releases_Attributes FROM tbl_Moby_Releases_Attributes WHERE id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases) & " AND id_Moby_Attributes = " & TC.getSQLFormat(id_Moby_Attributes) & " LIMIT 1", tran)
	End Function

	Public Function Upsert_Moby_Games_Alternate_Titles(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Moby_Games As Integer, ByVal Alternate_Title As String, ByVal Description As String) As Integer
		Dim id_Moby_Alternate_Titles As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Games_Alternate_Titles FROM tbl_Moby_Games_Alternate_Titles WHERE id_Moby_Games = " & TC.getSQLFormat(id_Moby_Games) & " AND Alternate_Title = " & TC.getSQLFormat(Alternate_Title) & " LIMIT 1", tran), 0)

		If id_Moby_Alternate_Titles > 0 Then
			Return id_Moby_Alternate_Titles
		End If

		DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Moby_Games_Alternate_Titles (id_Moby_Games, Alternate_Title, Description) VALUES (" & TC.getSQLParameter(id_Moby_Games, Alternate_Title, Description) & ")", tran)

		Return DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Games_Alternate_Titles FROM tbl_Moby_Games_Alternate_Titles WHERE id_Moby_Games = " & TC.getSQLFormat(id_Moby_Games) & " AND Alternate_Title = " & TC.getSQLFormat(Alternate_Title) & " LIMIT 1", tran)
	End Function

	Public Function Upsert_Moby_Games_Genres(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Moby_Games As Integer, ByVal id_Moby_Genres As Integer) As Integer
		Dim id_Moby_Games_Genres As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Games_Genres FROM tbl_Moby_Games_Genres WHERE id_Moby_Games = " & TC.getSQLFormat(id_Moby_Games) & " AND id_Moby_Genres = " & TC.getSQLFormat(id_Moby_Genres) & " LIMIT 1", tran), 0)

		If id_Moby_Games_Genres > 0 Then
			Return id_Moby_Games_Genres
		End If

		DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Moby_Games_Genres (id_Moby_Games, id_Moby_Genres) VALUES (" & TC.getSQLParameter(id_Moby_Games, id_Moby_Genres) & ")", tran)

		Return DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Games_Genres FROM tbl_Moby_Games_Genres WHERE id_Moby_Games = " & TC.getSQLFormat(id_Moby_Games) & " AND id_Moby_Genres = " & TC.getSQLFormat(id_Moby_Genres) & " LIMIT 1", tran)
	End Function

	Public Function Upsert_Moby_Genres(ByRef tran As SQLite.SQLiteTransaction, ByVal URLPart As String, ByVal Name As String, ByVal Description As String) As Integer
		Dim id_Moby_Genres As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Genres FROM moby.tbl_Moby_Genres WHERE URLPart = " & TC.getSQLFormat(URLPart), tran), 0)

		If id_Moby_Genres > 0 Then
			DataAccess.FireProcedure(tran.Connection, 0, "UPDATE moby.tbl_Moby_Genres SET Name = " & TC.getSQLFormat(Name) & ", Description = " & TC.getSQLFormat(Description) & " WHERE id_Moby_Genres = " & TC.getSQLFormat(id_Moby_Genres), tran)
			Return id_Moby_Genres
		End If

		DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Moby_Genres (URLPart, Name, Description, No_Category) VALUES (" & TC.getSQLParameter(URLPart, Name, Description, 1) & ")", tran)

		Return TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Genres FROM moby.tbl_Moby_Genres WHERE URLPart = " & TC.getSQLFormat(URLPart), tran), 0)
	End Function

	Public Function Upsert_Moby_Game_Groups(ByRef tran As SQLite.SQLiteTransaction, ByVal URLPart As String, ByVal Name As String, ByVal Description As String) As Integer
		Dim id_Moby_Game_Groups As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Game_Groups FROM tbl_Moby_Game_Groups WHERE URLPart = " & TC.getSQLFormat(URLPart) & " LIMIT 1", tran), 0)

		If id_Moby_Game_Groups > 0 Then
			DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Moby_Game_Groups SET Name = " & TC.getSQLFormat(Name) & ", Description = " & TC.getSQLFormat(Description) & " WHERE id_Moby_Game_Groups = " & TC.getSQLFormat(id_Moby_Game_Groups), tran)
			Return id_Moby_Game_Groups
		End If

		DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Moby_Game_Groups (URLPart, Name, Description) VALUES (" & TC.getSQLParameter(URLPart, Name, Description) & ")", tran)

		Return DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Game_Groups FROM tbl_Moby_Game_Groups WHERE URLPart = " & TC.getSQLFormat(URLPart) & " LIMIT 1", tran)
	End Function

	Public Function Upsert_Moby_Game_Groups_Moby_Releases(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Moby_Game_Groups As Integer, ByVal Moby_Games_URLPart As String, ByVal Moby_Platforms_URLPart As String) As Integer
		If id_Moby_Game_Groups <= 0 Then Return 0

		Dim id_Moby_Games As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Games FROM tbl_Moby_Games WHERE URLPart = " & TC.getSQLFormat(Moby_Games_URLPart) & " LIMIT 1", tran), 0)

		If id_Moby_Games <= 0 Then Return 0

		Dim id_Moby_Platforms As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Platforms FROM tbl_Moby_Platforms WHERE URLPart = " & TC.getSQLFormat(Moby_Platforms_URLPart) & " LIMIT 1", tran), 0)

		If id_Moby_Platforms <= 0 Then Return 0

		Dim id_Moby_Releases As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Releases FROM tbl_Moby_Releases WHERE id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms) & " AND id_Moby_Games = " & TC.getSQLFormat(id_Moby_Games) & " LIMIT 1"), 0)

		If id_Moby_Releases <= 0 Then Return 0

		Dim id_Moby_Game_Groups_Moby_Releases As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Game_Groups_Moby_Releases FROM tbl_Moby_Game_Groups_Moby_Releases WHERE id_Moby_Game_Groups = " & TC.getSQLFormat(id_Moby_Game_Groups) & " AND id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases) & " LIMIT 1", tran), 0)

		If id_Moby_Game_Groups_Moby_Releases <> 0 Then Return id_Moby_Game_Groups_Moby_Releases

		Return TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "INSERT INTO tbl_Moby_Game_Groups_Moby_Releases (id_Moby_Game_Groups, id_Moby_Releases) VALUES (" & TC.getSQLParameter(id_Moby_Game_Groups, id_Moby_Releases) & "); SELECT last_insert_rowid()", tran), 0)
	End Function

	Public Function Upsert_Moby_Staff(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Moby_Staff As Integer, ByVal Name As String, ByVal Biography As String) As Integer
		Dim id_Moby_Staff_DB As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Staff FROM tbl_Moby_Staff WHERE id_Moby_Staff = " & TC.getSQLFormat(id_Moby_Staff) & " LIMIT 1", tran), 0)

		If id_Moby_Staff_DB > 0 Then
			DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Moby_Staff SET Name = " & TC.getSQLFormat(Name) & ", Biography = " & TC.getSQLFormat(Biography) & " WHERE id_Moby_Staff = " & TC.getSQLFormat(id_Moby_Staff_DB), tran)
			Return id_Moby_Staff_DB
		End If

		DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Moby_Staff (id_Moby_Staff, Name, Biography) VALUES (" & TC.getSQLParameter(id_Moby_Staff, Name, Biography) & ")", tran)

		Return DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Staff FROM tbl_Moby_Staff WHERE id_Moby_Staff = " & TC.getSQLFormat(id_Moby_Staff) & " LIMIT 1", tran)
	End Function

	Public Function Upsert_Moby_Releases_Staff(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Moby_Releases As Integer, ByVal id_Moby_Staff As String, ByVal Position As String, ByVal Sort As Integer) As Integer
		Dim id_Moby_Releases_Staff As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Releases_Staff FROM tbl_Moby_Releases_Staff WHERE id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases) & " AND id_Moby_Staff = " & TC.getSQLFormat(id_Moby_Staff) & " AND Position = " & TC.getSQLFormat(Position) & " LIMIT 1", tran), 0)

		If id_Moby_Releases_Staff > 0 Then
			Return id_Moby_Releases_Staff
		End If

		DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Moby_Releases_Staff (id_Moby_Releases, id_Moby_Staff, Position, Sort) VALUES (" & TC.getSQLParameter(id_Moby_Releases, id_Moby_Staff, Position, Sort) & ")", tran)

		Return DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Releases_Staff FROM tbl_Moby_Releases_Staff WHERE id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases) & " AND id_Moby_Staff = " & TC.getSQLFormat(id_Moby_Staff) & " LIMIT 1", tran)
	End Function
#End Region

#Region "DELETE Statements"
	Public Sub Delete_Moby_Games_Genres(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Moby_Games As Integer)
		DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM moby.tbl_Moby_Games_Genres WHERE id_Moby_Games = " & TC.getSQLFormat(id_Moby_Games))
	End Sub
#End Region

#Region "FillTable_Statements"
	Public Function Fill_tbl_Moby_Games(ByRef tran As SQLite.SQLiteTransaction, Optional ByRef dt As DataTable = Nothing) As DataTable
		If dt Is Nothing Then
			dt = New DataTable("Table1")
		End If
		DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT * FROM tbl_Moby_Games ORDER BY Name", dt)

		Return dt
	End Function

	Public Function Fill_tbl_Moby_Platforms(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As tbl_Moby_PlatformsDataTable, Optional ByVal bShowOnlyGenericEmulated As Boolean = False) As tbl_Moby_PlatformsDataTable
		If dt Is Nothing Then
			dt = New tbl_Moby_PlatformsDataTable()
		End If

		DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT PLTFM.id_Moby_Platforms, PLTFM.Name, PLTFM.URLPart, PLTFM.Display_Name FROM moby.tbl_Moby_Platforms PLTFM LEFT JOIN main.tbl_Moby_Platforms_Settings PLTFMS ON PLTFM.id_Moby_Platforms = PLTFMS.id_Moby_Platforms WHERE PLTFM.Visible = 1 AND PLTFM.id_Moby_Platforms_Owner IS NULL AND (PLTFMS.Visible IS NULL OR PLTFMS.Visible = 1) " & IIf(bShowOnlyGenericEmulated, " AND PLTFM.GenericEmulated = 1 ", "") & " ORDER BY PLTFM.Display_Name", dt, tran)

		Return dt
	End Function

#End Region

End Class