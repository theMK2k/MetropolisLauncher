Partial Class DS_ML
	Partial Public Class ttb_Emulators_PreLaunch_CommandsDataTable
	End Class

	Public Enum enm_FilterSetTypes
		All = 0
		Emulation = 1
	End Enum

#Region "Select Statements"
	Public Function Select_src_ucr_Emulation_Games_Rating(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Emu_Games As Integer) As Object
		Dim sSQL As String =
		"	SELECT" & ControlChars.CrLf &
		"					CAST" & ControlChars.CrLf &
		"						(" & ControlChars.CrLf &
		"							CAST(" & ControlChars.CrLf &
		"								CAST(IFNULL(Rating_Gameplay * RW1.Weight, 0) AS REAL) / 5 + CAST(IFNULL(Rating_Graphics * RW2.Weight, 0) AS REAL) / 5 + CAST(IFNULL(Rating_Personal * RW5.Weight, 0) AS REAL) / 5 + CAST(IFNULL(Rating_Sound * RW3.Weight, 0) AS REAL) / 5 + CAST(IFNULL(Rating_Story * RW4.Weight, 0) AS REAL) / 5 AS REAL" & ControlChars.CrLf &
		"							)" & ControlChars.CrLf &
		"							/" & ControlChars.CrLf &
		"							(" & ControlChars.CrLf &
		"								CAST(" & ControlChars.CrLf &
		"									CASE WHEN Rating_Gameplay IS NOT NULL THEN RW1.Weight ELSE 0 END" & ControlChars.CrLf &
		"									+ CASE WHEN Rating_Graphics IS NOT NULL THEN RW2.Weight ELSE 0 END" & ControlChars.CrLf &
		"									+ CASE WHEN Rating_Personal IS NOT NULL THEN RW5.Weight ELSE 0 END" & ControlChars.CrLf &
		"									+ CASE WHEN Rating_Sound IS NOT NULL THEN RW3.Weight ELSE 0 END" & ControlChars.CrLf &
		"									+ CASE WHEN Rating_Story IS NOT NULL THEN RW4.Weight ELSE 0 END" & ControlChars.CrLf &
		"								AS REAL)" & ControlChars.CrLf &
		"							)" & ControlChars.CrLf &
		"							* 100" & ControlChars.CrLf &
		"						AS INTEGER" & ControlChars.CrLf &
		"						)" & ControlChars.CrLf &
		"						AS Rating" & ControlChars.CrLf &
		"	FROM			tbl_Emu_Games EMUGAME" & ControlChars.CrLf &
		" LEFT JOIN tbl_Emu_Games_Rating_Weights RW1 ON RW1.id_Emu_Games_Rating_Weights = 1" & ControlChars.CrLf &
		" LEFT JOIN tbl_Emu_Games_Rating_Weights RW2 ON RW2.id_Emu_Games_Rating_Weights = 2" & ControlChars.CrLf &
		" LEFT JOIN tbl_Emu_Games_Rating_Weights RW3 ON RW3.id_Emu_Games_Rating_Weights = 3" & ControlChars.CrLf &
		" LEFT JOIN tbl_Emu_Games_Rating_Weights RW4 ON RW4.id_Emu_Games_Rating_Weights = 4" & ControlChars.CrLf &
		" LEFT JOIN tbl_Emu_Games_Rating_Weights RW5 ON RW5.id_Emu_Games_Rating_Weights = 5" & ControlChars.CrLf &
		"	WHERE			EMUGAME.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games)

		Return MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnScalar(tran.Connection, 0, sSQL, tran)
	End Function

	Public Shared Function Select_tbl_Tag_Parser(ByRef tran As SQLite.SQLiteTransaction, ByVal Content As String)
		Return TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Tag_Parser FROM tbl_Tag_Parser WHERE Content = " & TC.getSQLFormat(Content), tran), 0)
	End Function

	Public Shared Function Select_Genres_By_id_Emu_Games(ByVal id_Emu_Games As Integer, ByVal Genre_Type As cls_Globals.enm_Moby_Genres_Categories) As DataTable
		Dim sSQL As String = ""
		sSQL &= "	SELECT G.Name AS Name" & ControlChars.CrLf
		sSQL &= "	FROM" & ControlChars.CrLf
		sSQL &= "	(" & ControlChars.CrLf
		sSQL &= "		SELECT id_Moby_Genres FROM tbl_Moby_Games_Genres GG" & ControlChars.CrLf
		sSQL &= "		WHERE GG.id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = (SELECT Moby_Games_URLPart FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & "))" & ControlChars.CrLf
		sSQL &= "		UNION SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "		FROM tbl_Emu_Games_Moby_Genres EGMG" & ControlChars.CrLf
		sSQL &= "		WHERE EGMG.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & ControlChars.CrLf
		sSQL &= "	) AS temp_Genres" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Genres G ON temp_Genres.id_Moby_Genres = G.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "	WHERE G.id_Moby_Genres_Categories = " & TC.getSQLFormat(Genre_Type)

		sSQL &= "	AND G.id_Moby_Genres NOT IN" & ControlChars.CrLf
		sSQL &= "	(" & ControlChars.CrLf
		sSQL &= "		SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "		FROM tbl_Emu_Games_Moby_Genres" & ControlChars.CrLf
		sSQL &= "		WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & ControlChars.CrLf
		sSQL &= "					AND Used = 0" & ControlChars.CrLf
		sSQL &= "	) ORDER BY G.Name" & ControlChars.CrLf

		Return DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL)
	End Function

	Public Shared Function Select_Genres_By_id_Moby_Releases(ByVal id_Moby_Releases As Integer, ByVal Genre_Type As cls_Globals.enm_Moby_Genres_Categories) As DataTable
		Dim sSQL As String = ""
		sSQL &= "	SELECT DISTINCT" & ControlChars.CrLf
		sSQL &= "		MGEN.Name AS Name" & ControlChars.CrLf
		sSQL &= "	FROM moby.tbl_Moby_Games_Genres MGG" & ControlChars.CrLf
		sSQL &= "	INNER JOIN moby.tbl_Moby_Games MG ON MGG.id_Moby_Games = MG.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "	INNER JOIN moby.tbl_Moby_Releases MR ON MG.id_Moby_Games = MR.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "	INNER JOIN moby.tbl_Moby_Genres MGEN ON MGG.id_Moby_Genres = MGEN.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "	WHERE MGEN.id_Moby_Genres_Categories = " & TC.getSQLFormat(Genre_Type)

		sSQL &= " AND MR.id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases) & ControlChars.CrLf
		sSQL &= "	ORDER BY MGEN.Name" & ControlChars.CrLf

		Return DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL)
	End Function

	Public Shared Function Select_Genres_AB_By_id_Emu_Games(ByVal id_Emu_Games_A As Integer, ByVal id_Emu_Games_B As Integer, ByVal Genre_Type As cls_Globals.enm_Moby_Genres_Categories) As DataTable
		Dim sSQL As String = ""
		sSQL &= "	SELECT G.Name AS Name" & ControlChars.CrLf
		sSQL &= "	FROM" & ControlChars.CrLf
		sSQL &= "	(" & ControlChars.CrLf
		sSQL &= "		SELECT id_Moby_Genres FROM tbl_Moby_Games_Genres GG" & ControlChars.CrLf
		sSQL &= "		WHERE GG.id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = (SELECT Moby_Games_URLPart FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & "))" & ControlChars.CrLf
		sSQL &= "		UNION SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "		FROM tbl_Emu_Games_Moby_Genres EGMG" & ControlChars.CrLf
		sSQL &= "		WHERE EGMG.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ControlChars.CrLf
		sSQL &= "	) AS temp_Genres" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Genres G ON temp_Genres.id_Moby_Genres = G.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "	WHERE G.id_Moby_Genres_Categories = " & TC.getSQLFormat(Genre_Type)

		sSQL &= "	AND G.id_Moby_Genres NOT IN" & ControlChars.CrLf
		sSQL &= "	(" & ControlChars.CrLf
		sSQL &= "		SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "		FROM tbl_Emu_Games_Moby_Genres" & ControlChars.CrLf
		sSQL &= "		WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ControlChars.CrLf
		sSQL &= "					AND Used = 0" & ControlChars.CrLf
		sSQL &= "	)" & ControlChars.CrLf
		sSQL &= "AND G.id_Moby_Genres IN" & ControlChars.CrLf
		sSQL &= "(" & ControlChars.CrLf
		sSQL &= "	SELECT temp_GenresC.id_Moby_Genres FROM" & ControlChars.CrLf
		sSQL &= "	(	SELECT id_Moby_Genres FROM tbl_Moby_Games_Genres GGC" & ControlChars.CrLf
		sSQL &= "		WHERE GGC.id_Moby_Games = (SELECT id_Moby_Games FROM tbl_Moby_Games WHERE URLPart = (SELECT Moby_Games_URLPart FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_B) & "))" & ControlChars.CrLf
		sSQL &= "		UNION SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "		FROM tbl_Emu_Games_Moby_Genres EGMGC" & ControlChars.CrLf
		sSQL &= "		WHERE EGMGC.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_B) & ControlChars.CrLf
		sSQL &= "	) AS temp_GenresC" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Genres GC ON temp_GenresC.id_Moby_Genres = GC.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "	WHERE GC.id_Moby_Genres_Categories = " & TC.getSQLFormat(Genre_Type)

		sSQL &= "	AND GC.id_Moby_Genres NOT IN" & ControlChars.CrLf
		sSQL &= "	(" & ControlChars.CrLf
		sSQL &= "		SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "		FROM tbl_Emu_Games_Moby_Genres" & ControlChars.CrLf
		sSQL &= "		WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_B) & ControlChars.CrLf
		sSQL &= "		AND Used = 0" & ControlChars.CrLf
		sSQL &= "	)" & ControlChars.CrLf
		sSQL &= ")" & ControlChars.CrLf
		sSQL &= " ORDER BY G.Name" & ControlChars.CrLf

		Return DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL)
	End Function

	Public Shared Function Select_Genres_AB_By_id_Moby_Releases(ByVal id_Emu_Games_A As Integer, ByVal id_Moby_Releases_B As Integer, ByVal Genre_Type As cls_Globals.enm_Moby_Genres_Categories) As DataTable
		Dim sSQL As String = ""
		sSQL &= "	SELECT G.Name AS Name" & ControlChars.CrLf
		sSQL &= "	FROM" & ControlChars.CrLf
		sSQL &= "	(" & ControlChars.CrLf
		sSQL &= "		SELECT id_Moby_Genres FROM tbl_Moby_Games_Genres GG" & ControlChars.CrLf
		sSQL &= "		WHERE GG.id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = (SELECT Moby_Games_URLPart FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & "))" & ControlChars.CrLf
		sSQL &= "		UNION SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "		FROM tbl_Emu_Games_Moby_Genres EGMG" & ControlChars.CrLf
		sSQL &= "		WHERE EGMG.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ControlChars.CrLf
		sSQL &= "	) AS temp_Genres" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Genres G ON temp_Genres.id_Moby_Genres = G.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "	WHERE G.id_Moby_Genres_Categories = " & TC.getSQLFormat(Genre_Type)

		sSQL &= "	AND G.id_Moby_Genres NOT IN" & ControlChars.CrLf
		sSQL &= "	(" & ControlChars.CrLf
		sSQL &= "		SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "		FROM tbl_Emu_Games_Moby_Genres" & ControlChars.CrLf
		sSQL &= "		WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ControlChars.CrLf
		sSQL &= "					AND Used = 0" & ControlChars.CrLf
		sSQL &= "	)" & ControlChars.CrLf
		sSQL &= "AND G.id_Moby_Genres IN" & ControlChars.CrLf
		sSQL &= "(" & ControlChars.CrLf
		sSQL &= "	SELECT temp_GenresC.id_Moby_Genres FROM" & ControlChars.CrLf
		sSQL &= "	(	SELECT id_Moby_Genres FROM tbl_Moby_Games_Genres GGC" & ControlChars.CrLf
		sSQL &= "		WHERE GGC.id_Moby_Games = (SELECT id_Moby_Games FROM tbl_Moby_Releases WHERE id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases_B) & ")" & ControlChars.CrLf
		sSQL &= "	) AS temp_GenresC" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Genres GC ON temp_GenresC.id_Moby_Genres = GC.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "	WHERE GC.id_Moby_Genres_Categories = " & TC.getSQLFormat(Genre_Type)

		sSQL &= ")" & ControlChars.CrLf
		sSQL &= " ORDER BY G.Name" & ControlChars.CrLf

		Return DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL)
	End Function

	Public Enum enm_Attributes_Types
		Rating_Descriptors = 0
		Other_Attributes = 1
		Multiplayer_Attributes = 2
	End Enum

	Public Shared Function Select_Attributes_By_id_Emu_Games(ByVal id_Emu_Games As Integer, ByVal Attributes_Type As enm_Attributes_Types) As DataTable
		Dim sSQL As String = ""
		sSQL &= "	SELECT A.Name AS Name FROM" & ControlChars.CrLf
		sSQL &= "	(" & ControlChars.CrLf
		sSQL &= "		SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "		WHERE RA.id_Moby_Releases = (SELECT id_Moby_Releases FROM tbl_Moby_Releases WHERE id_Moby_Platforms = (SELECT IFNULL(id_Moby_Platforms_Alternative, id_Moby_Platforms) FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & ") AND id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = (SELECT Moby_Games_URLPart FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & ")))" & ControlChars.CrLf
		sSQL &= "		UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "		FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "		WHERE EGMA.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & ControlChars.CrLf
		sSQL &= "	) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf

		Select Case Attributes_Type
			Case enm_Attributes_Types.Rating_Descriptors
				sSQL &= "	WHERE	AC.RatingDescriptor = 1" & ControlChars.CrLf
			Case enm_Attributes_Types.Other_Attributes
				sSQL &= "	WHERE	IFNULL(AC.RatingDescriptor, 0) = 0 AND IFNULL(AC.RatingSystem, 0) = 0 AND IFNULL(AC.NumPlayersCategory, 0) = 0 AND AC.id_Moby_Attributes_Categories NOT IN (12, 16)" & ControlChars.CrLf
			Case enm_Attributes_Types.Multiplayer_Attributes
				sSQL &= "	WHERE	AC.id_Moby_Attributes_Categories IN (12, 16)" & ControlChars.CrLf
		End Select

		sSQL &= "	AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "	(" & ControlChars.CrLf
		sSQL &= "		SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "		FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "		WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & ControlChars.CrLf
		sSQL &= "		AND Used = 0" & ControlChars.CrLf
		sSQL &= "	)" & ControlChars.CrLf
		sSQL &= "	ORDER BY A.Name" & ControlChars.CrLf

		Return DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL)
	End Function

	Public Shared Function Select_Attributes_By_id_Moby_Releases(ByVal id_Moby_Releases As Integer, ByVal Attributes_Type As enm_Attributes_Types) As DataTable
		Dim sSQL As String = ""

		sSQL &= "	SELECT A.Name AS Name FROM" & ControlChars.CrLf
		sSQL &= "	(" & ControlChars.CrLf
		sSQL &= "		SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "		WHERE RA.id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases) & ControlChars.CrLf
		sSQL &= "	) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf

		Select Case Attributes_Type
			Case enm_Attributes_Types.Rating_Descriptors
				sSQL &= "	WHERE	AC.RatingDescriptor = 1" & ControlChars.CrLf
			Case enm_Attributes_Types.Other_Attributes
				sSQL &= "	WHERE	IFNULL(AC.RatingDescriptor, 0) = 0 AND IFNULL(AC.RatingSystem, 0) = 0 AND IFNULL(AC.NumPlayersCategory, 0) = 0 AND AC.id_Moby_Attributes_Categories NOT IN (12, 16)" & ControlChars.CrLf
			Case enm_Attributes_Types.Multiplayer_Attributes
				sSQL &= "	WHERE	AC.id_Moby_Attributes_Categories IN (12, 16)" & ControlChars.CrLf
		End Select

		sSQL &= "	ORDER BY A.Name" & ControlChars.CrLf

		Return DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL)
	End Function

	Public Shared Function Select_Attributes_AB_By_id_Emu_Games(ByVal id_Emu_Games_A As Integer, ByVal id_Emu_Games_B As Integer, ByVal Attributes_Type As enm_Attributes_Types) As DataTable
		Dim sSQL As String = ""

		sSQL &= "	SELECT A.Name AS Name FROM" & ControlChars.CrLf
		sSQL &= "	(" & ControlChars.CrLf
		sSQL &= "		SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "		WHERE RA.id_Moby_Releases = (SELECT id_Moby_Releases FROM tbl_Moby_Releases WHERE id_Moby_Platforms = (SELECT IFNULL(id_Moby_Platforms_Alternative, id_Moby_Platforms) FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ") AND id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = (SELECT Moby_Games_URLPart FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ")))" & ControlChars.CrLf
		sSQL &= "		UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "		FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "		WHERE EGMA.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ControlChars.CrLf
		sSQL &= "	) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf

		Select Case Attributes_Type
			Case enm_Attributes_Types.Rating_Descriptors
				sSQL &= "	WHERE	AC.RatingDescriptor = 1" & ControlChars.CrLf
			Case enm_Attributes_Types.Other_Attributes
				sSQL &= "	WHERE	IFNULL(AC.RatingDescriptor, 0) = 0 AND IFNULL(AC.RatingSystem, 0) = 0 AND IFNULL(AC.NumPlayersCategory, 0) = 0 AND AC.id_Moby_Attributes_Categories NOT IN (12, 16)" & ControlChars.CrLf
			Case enm_Attributes_Types.Multiplayer_Attributes
				sSQL &= "	WHERE	AC.id_Moby_Attributes_Categories IN (12, 16)" & ControlChars.CrLf
		End Select

		sSQL &= "	AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "	(" & ControlChars.CrLf
		sSQL &= "		SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "		FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "		WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_B) & ControlChars.CrLf
		sSQL &= "		AND Used = 0" & ControlChars.CrLf
		sSQL &= "	)" & ControlChars.CrLf
		sSQL &= "	AND A.id_Moby_Attributes IN" & ControlChars.CrLf
		sSQL &= "	(" & ControlChars.CrLf
		sSQL &= "		SELECT A.id_Moby_Attributes FROM" & ControlChars.CrLf
		sSQL &= "		(" & ControlChars.CrLf
		sSQL &= "			SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "			WHERE RA.id_Moby_Releases = (SELECT id_Moby_Releases FROM tbl_Moby_Releases WHERE id_Moby_Platforms = (SELECT IFNULL(id_Moby_Platforms_Alternative, id_Moby_Platforms) FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_B) & ") AND id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = (SELECT Moby_Games_URLPart FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_B) & ")))" & ControlChars.CrLf
		sSQL &= "			UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "			FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "			WHERE EGMA.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_B) & ControlChars.CrLf
		sSQL &= "		) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "		LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "		LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf

		Select Case Attributes_Type
			Case enm_Attributes_Types.Rating_Descriptors
				sSQL &= "	WHERE	AC.RatingDescriptor = 1" & ControlChars.CrLf
			Case enm_Attributes_Types.Other_Attributes
				sSQL &= "	WHERE	IFNULL(AC.RatingDescriptor, 0) = 0 AND IFNULL(AC.RatingSystem, 0) = 0 AND IFNULL(AC.NumPlayersCategory, 0) = 0 AND AC.id_Moby_Attributes_Categories NOT IN (12, 16)" & ControlChars.CrLf
			Case enm_Attributes_Types.Multiplayer_Attributes
				sSQL &= "	WHERE	AC.id_Moby_Attributes_Categories IN (12, 16)" & ControlChars.CrLf
		End Select

		sSQL &= "		AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "		(" & ControlChars.CrLf
		sSQL &= "			SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "			FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "			WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_B) & ControlChars.CrLf
		sSQL &= "			AND Used = 0" & ControlChars.CrLf
		sSQL &= "		)" & ControlChars.CrLf
		sSQL &= "	)" & ControlChars.CrLf
		sSQL &= "	ORDER BY A.Name" & ControlChars.CrLf

		Return DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL)
	End Function

	Public Shared Function Select_Attributes_AB_By_Moby_Releases(ByVal id_Emu_Games_A As Integer, ByVal id_Moby_Releases_B As Integer, ByVal Attributes_Type As enm_Attributes_Types) As DataTable
		Dim sSQL As String = ""

		sSQL &= "	SELECT A.Name AS Name FROM" & ControlChars.CrLf
		sSQL &= "	(" & ControlChars.CrLf
		sSQL &= "		SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "		WHERE RA.id_Moby_Releases = (SELECT id_Moby_Releases FROM tbl_Moby_Releases WHERE id_Moby_Platforms = (SELECT IFNULL(id_Moby_Platforms_Alternative, id_Moby_Platforms) FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ") AND id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = (SELECT Moby_Games_URLPart FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ")))" & ControlChars.CrLf
		sSQL &= "		UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "		FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "		WHERE EGMA.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ControlChars.CrLf
		sSQL &= "	) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf

		Select Case Attributes_Type
			Case enm_Attributes_Types.Rating_Descriptors
				sSQL &= "	WHERE	AC.RatingDescriptor = 1" & ControlChars.CrLf
			Case enm_Attributes_Types.Other_Attributes
				sSQL &= "	WHERE	IFNULL(AC.RatingDescriptor, 0) = 0 AND IFNULL(AC.RatingSystem, 0) = 0 AND IFNULL(AC.NumPlayersCategory, 0) = 0 AND AC.id_Moby_Attributes_Categories NOT IN (12, 16)" & ControlChars.CrLf
			Case enm_Attributes_Types.Multiplayer_Attributes
				sSQL &= "	WHERE	AC.id_Moby_Attributes_Categories IN (12, 16)" & ControlChars.CrLf
		End Select

		sSQL &= "	AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "	(" & ControlChars.CrLf
		sSQL &= "		SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "		FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "		WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ControlChars.CrLf
		sSQL &= "		AND Used = 0" & ControlChars.CrLf
		sSQL &= "	)" & ControlChars.CrLf
		sSQL &= "	AND A.id_Moby_Attributes IN" & ControlChars.CrLf
		sSQL &= "	(" & ControlChars.CrLf
		sSQL &= "		SELECT A.id_Moby_Attributes FROM" & ControlChars.CrLf
		sSQL &= "		(" & ControlChars.CrLf
		sSQL &= "			SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "			WHERE RA.id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases_B) & ControlChars.CrLf
		sSQL &= "		) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "		LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "		LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf

		Select Case Attributes_Type
			Case enm_Attributes_Types.Rating_Descriptors
				sSQL &= "	WHERE	AC.RatingDescriptor = 1" & ControlChars.CrLf
			Case enm_Attributes_Types.Other_Attributes
				sSQL &= "	WHERE	IFNULL(AC.RatingDescriptor, 0) = 0 AND IFNULL(AC.RatingSystem, 0) = 0 AND IFNULL(AC.NumPlayersCategory, 0) = 0 AND AC.id_Moby_Attributes_Categories NOT IN (12, 16)" & ControlChars.CrLf
			Case enm_Attributes_Types.Multiplayer_Attributes
				sSQL &= "	WHERE	AC.id_Moby_Attributes_Categories IN (12, 16)" & ControlChars.CrLf
		End Select

		sSQL &= "	)" & ControlChars.CrLf
		sSQL &= "	ORDER BY A.Name" & ControlChars.CrLf

		Return DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL)
	End Function

	''' <summary>
	''' 
	''' </summary>
	''' <param name="tran"></param>
	''' <param name="id_Moby_Platforms"></param>
	''' <param name="id_Users">Use only if you want to impersonate a restricted! user</param>
	''' <returns></returns>
	Public Shared Function Select_id_Moby_Platforms_Caches(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Moby_Platforms As Int64, Optional ByVal id_Users As Int64 = 0) As Int64
		If id_Users = 0 AndAlso cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0 AndAlso cls_Globals.Restricted Then
			id_Users = cls_Globals.id_Users
		End If

		Dim sSQL As String = ""
		sSQL &= "SELECT id_Moby_Platforms_Caches FROM tbl_Moby_Platforms_Caches WHERE id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms) & " AND id_Users " & IIf(id_Users > 0, " = " & TC.getSQLFormat(id_Users), " IS NULL")
		Return TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, sSQL, tran), 0L)
	End Function
#End Region

#Region "Fill Statements"
	Public Sub Fill_src_frm_Emulators(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As tbl_EmulatorsDataTable)
		dt.Clear()
		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT id_Emulators, Displayname, InstallDirectory, Executable, StartupParameter, AutoItScript, J2KPreset, ScreenshotDirectory, Libretro_Core, id_List_Generators, ScriptType, ScriptPath FROM tbl_Emulators ORDER BY Displayname", dt, tran)
	End Sub

	Public Sub Fill_src_frm_Emulators_Moby_Platforms(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As src_frm_Emulators_Moby_PlatformsDataTable, ByVal id_Emulators As Integer, Optional ByVal id_Users As Object = Nothing)
		dt.Clear()
		Dim sSQL As String = ""
		sSQL &= "	SELECT" & ControlChars.CrLf
		sSQL &= "		PLTFM.id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "		, PLTFM.Display_Name" & ControlChars.CrLf
		sSQL &= "		, CASE WHEN EMUPLTFM.id_Moby_Platforms IS NOT NULL THEN 1 ELSE 0 END AS Supported" & ControlChars.CrLf
		sSQL &= "		, IFNULL(" & ControlChars.CrLf
		sSQL &= "				UEMUPLTFM.DefaultEmulator, " & ControlChars.CrLf
		sSQL &= "				IFNULL(EMUPLTFM.DefaultEmulator, 0)" & ControlChars.CrLf
		sSQL &= "		) AS DefaultEmulator" & ControlChars.CrLf
		sSQL &= "	FROM moby.tbl_Moby_Platforms PLTFM" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN main.tbl_Emulators_Moby_Platforms EMUPLTFM ON PLTFM.id_Moby_Platforms = EMUPLTFM.id_Moby_Platforms AND EMUPLTFM.id_Emulators = " & TC.getSQLFormat(id_Emulators) & ControlChars.CrLf
		sSQL &= "	LEFT JOIN main.tbl_Moby_Platforms_Settings PLTFMS ON PLTFM.id_Moby_Platforms = PLTFMS.id_Moby_Platforms " & ControlChars.CrLf
		sSQL &= "	LEFT JOIN main.tbl_Users_Emulators_Moby_Platforms UEMUPLTFM ON UEMUPLTFM.id_Users = " & TC.getSQLFormat(TC.NZ(id_Users, 0)) & " AND PLTFM.id_Moby_Platforms = UEMUPLTFM.id_Moby_Platforms AND UEMUPLTFM.id_Emulators = " & TC.getSQLFormat(id_Emulators) & ControlChars.CrLf
		sSQL &= "	WHERE PLTFM.Visible = 1" & ControlChars.CrLf
		sSQL &= "				AND PLTFM.GenericEmulated = 1" & ControlChars.CrLf
		sSQL &= "				AND PLTFM.id_Moby_Platforms_Owner IS NULL" & ControlChars.CrLf
		sSQL &= "				AND (PLTFMS.Visible IS NULL OR PLTFMS.Visible = 1)" & ControlChars.CrLf
		sSQL &= "	ORDER BY Display_Name" & ControlChars.CrLf
		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)
	End Sub

	Public Sub Fill_src_frm_Emulators_Multivolume_Parameters(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As tbl_Emulators_Multivolume_ParametersDataTable, ByVal id_Emulators As Integer)
		dt.Clear()
		Dim sSQL As String =
		"	SELECT" &
		"		id_Emulators_Multivolume_Parameters" &
		"		, id_Emulators" &
		"		, Volume_Number" &
		"		, Parameter" &
		"	FROM tbl_Emulators_Multivolume_Parameters" &
		"	WHERE id_Emulators = " & TC.getSQLFormat(id_Emulators) &
		"	ORDER BY Volume_Number"
		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)
	End Sub

	Public Shared Sub Ensure_Moby_Platform_Caches(ByRef tran As SQLite.SQLiteTransaction)
		Dim id_Users As Int64 = 0
		If cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0 AndAlso cls_Globals.Restricted Then
			id_Users = cls_Globals.id_Users
		End If

		Dim sSQL As String = ""
		sSQL &= "SELECT PLTFM.id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "	FROM	moby.tbl_Moby_Platforms PLTFM" & ControlChars.CrLf
		sSQL &= "	WHERE	PLTFM.id_Moby_Platforms NOT IN (" & ControlChars.CrLf
		sSQL &= "		SELECT id_Moby_Platforms FROM tbl_Moby_Platforms_Caches WHERE id_Users " & IIf(id_Users > 0, " = " & TC.getSQLFormat(id_Users), " IS NULL")
		sSQL &= "	)" & ControlChars.CrLf
		sSQL &= "	AND	(" & ControlChars.CrLf
		sSQL &= "				PLTFM.id_Moby_Platforms IN (-1, -2)" & ControlChars.CrLf
		sSQL &= "				OR" & ControlChars.CrLf
		sSQL &= "				(" & ControlChars.CrLf
		sSQL &= "					PLTFM.Visible = 1" & ControlChars.CrLf
		sSQL &= "					AND" & ControlChars.CrLf
		sSQL &= "					PLTFM.id_Moby_Platforms_Owner IS NULL" & ControlChars.CrLf
		sSQL &= "				)" & ControlChars.CrLf
		sSQL &= "			)" & ControlChars.CrLf

		Dim dt_Platforms As DataTable = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, Nothing, tran)

		For Each row As DataRow In dt_Platforms.Rows
			Update_Platform_NumGames_Cache(tran, row("id_Moby_Platforms"))
		Next
	End Sub

	Public Sub Fill_src_ucr_Emulators_Platforms(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As src_ucr_Emulation_PlatformsDataTable)
		Dim id_Users As Int64 = 0
		If cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0 AndAlso cls_Globals.Restricted Then
			id_Users = cls_Globals.id_Users
		End If

		dt.Clear()
		Dim sSQL As String = ""
		sSQL &= "	SELECT" & ControlChars.CrLf
		sSQL &= "		PLTFM.id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "		, PLTFM.Display_Name  || ' (' || IFNULL(MPC.NumGames, 0) || ')' AS Name" & ControlChars.CrLf
		sSQL &= "		, CASE WHEN PLTFM.id_Moby_Platforms = -1 THEN 0 ELSE 1 END AS Sort" & ControlChars.CrLf
		sSQL &= "	FROM	moby.tbl_Moby_Platforms PLTFM" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Platforms_Settings PLTFMS ON PLTFM.id_Moby_Platforms = PLTFMS.id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= " LEFT JOIN tbl_Moby_Platforms_Caches MPC ON MPC.id_Moby_Platforms = PLTFM.id_Moby_Platforms AND MPC.id_Users " & IIf(id_Users > 0, " = " & TC.getSQLFormat(id_Users), " IS NULL") & ControlChars.CrLf
		sSQL &= "	WHERE	("
		sSQL &= "					(PLTFM.id_Moby_Platforms IN (-1, -2) AND (PLTFMS.Visible IS NULL OR PLTFMS.Visible = 1))" & ControlChars.CrLf
		sSQL &= "					OR" & ControlChars.CrLf
		sSQL &= "					(" & ControlChars.CrLf
		sSQL &= "						PLTFM.Visible = 1" & ControlChars.CrLf
		sSQL &= "						AND" & ControlChars.CrLf
		sSQL &= "						PLTFM.id_Moby_Platforms_Owner Is NULL" & ControlChars.CrLf
		sSQL &= "						AND" & ControlChars.CrLf
		sSQL &= "						(" & ControlChars.CrLf
		sSQL &= "							PLTFMS.Visible IS NULL" & ControlChars.CrLf
		sSQL &= "							OR" & ControlChars.CrLf
		sSQL &= "							PLTFMS.Visible = 1" & ControlChars.CrLf
		sSQL &= "						)" & ControlChars.CrLf
		sSQL &= "					)" & ControlChars.CrLf
		sSQL &= "				)" & ControlChars.CrLf
		If id_Users > 0 Then
			sSQL &= "			AND IFNULL(MPC.NumGames, 0) > 0"
		End If
		sSQL &= "	ORDER BY Sort, Name"

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)
	End Sub

	Public Shared Sub Fill_src_ucr_Emulation_Games(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As src_ucr_Emulation_GamesDataTable, Optional ByVal id_Moby_Platforms As Object = Nothing, Optional ByVal SearchText As Object = Nothing, Optional ByVal id_FilterSets As Object = Nothing, Optional ByVal id_Emu_Games As Object = Nothing, Optional ByVal ShowHidden As Boolean = True, Optional ByVal id_Moby_Game_Groups As Object = CLng(0), Optional ByVal ShowVolumes As Boolean = False, Optional ByVal id_Moby_Staff As Object = CLng(0), Optional ByVal id_Similarity_Calculation_Results As Object = CLng(0), Optional ByVal id_Cheevo_Challenges As Object = CLng(0), Optional ByVal Cheevo_Challenges_Tier As Object = CLng(0), Optional ByVal Cheevo_Challenges_Show_Completed As Boolean = False)
		If dt Is Nothing Then dt = New src_ucr_Emulation_GamesDataTable

		dt.Clear()

		If id_Emu_Games Is Nothing AndAlso TC.NZ(id_Similarity_Calculation_Results, 0) <> 0 Then
			DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM ttb_Emu_Games_Similarity_Calculation; INSERT INTO ttb_Emu_Games_Similarity_Calculation (id_Emu_Games, Similarity, [001_Platform], [002_MobyRank], [003_MobyScore], [004_Publisher], [005_Developer], [006_Year], [101_Basic_Genres], [102_Perspectives], [103_Sports_Themes], [105_Educational_Categories], [106_Other_Attributes], [107_Visual_Presentation], [108_Gameplay], [109_Pacing], [110_Narrative_Theme_Topic], [111_Setting], [112_Vehicular_Themes], [113_Interface_Control], [114_DLC_Addon], [115_Special_Edition], [201_MinPlayers], [202_MaxPlayers], [203_AgeO], [204_AgeP], [205_Rating_Descriptors], [206_Other_Attributes], [207_Multiplayer_Attributes], [301_Group_Membership], [401_Staff]) SELECT id_Emu_Games, Similarity, [001_Platform], [002_MobyRank], [003_MobyScore], [004_Publisher], [005_Developer], [006_Year], [101_Basic_Genres], [102_Perspectives], [103_Sports_Themes], [105_Educational_Categories], [106_Other_Attributes], [107_Visual_Presentation], [108_Gameplay], [109_Pacing], [110_Narrative_Theme_Topic], [111_Setting], [112_Vehicular_Themes], [113_Interface_Control], [114_DLC_Addon], [115_Special_Edition], [201_MinPlayers], [202_MaxPlayers], [203_AgeO], [204_AgeP], [205_Rating_Descriptors], [206_Other_Attributes], [207_Multiplayer_Attributes], [301_Group_Membership], [401_Staff] FROM tbl_Similarity_Calculation_Results_Entries WHERE id_Emu_Games Is Not NULL And id_Similarity_Calculation_Results = " & TC.getSQLFormat(id_Similarity_Calculation_Results), tran)
			DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM ttb_Moby_Releases_Similarity_Calculation; INSERT INTO ttb_Moby_Releases_Similarity_Calculation (id_Moby_Releases, Similarity, [001_Platform], [002_MobyRank], [003_MobyScore], [004_Publisher], [005_Developer], [006_Year], [101_Basic_Genres], [102_Perspectives], [103_Sports_Themes], [105_Educational_Categories], [106_Other_Attributes], [107_Visual_Presentation], [108_Gameplay], [109_Pacing], [110_Narrative_Theme_Topic], [111_Setting], [112_Vehicular_Themes], [113_Interface_Control], [114_DLC_Addon], [115_Special_Edition], [201_MinPlayers], [202_MaxPlayers], [203_AgeO], [204_AgeP], [205_Rating_Descriptors], [206_Other_Attributes], [207_Multiplayer_Attributes], [301_Group_Membership], [401_Staff]) SELECT id_Moby_Releases, Similarity, [001_Platform], [002_MobyRank], [003_MobyScore], [004_Publisher], [005_Developer], [006_Year], [101_Basic_Genres], [102_Perspectives], [103_Sports_Themes], [105_Educational_Categories], [106_Other_Attributes], [107_Visual_Presentation], [108_Gameplay], [109_Pacing], [110_Narrative_Theme_Topic], [111_Setting], [112_Vehicular_Themes], [113_Interface_Control], [114_DLC_Addon], [115_Special_Edition], [201_MinPlayers], [202_MaxPlayers], [203_AgeO], [204_AgeP], [205_Rating_Descriptors], [206_Other_Attributes], [207_Multiplayer_Attributes], [301_Group_Membership], [401_Staff] FROM tbl_Similarity_Calculation_Results_Entries WHERE id_Emu_Games Is NULL And id_Similarity_Calculation_Results = " & TC.getSQLFormat(id_Similarity_Calculation_Results), tran)
		End If

		Dim sSQL As String = ""

		sSQL &= "	SELECT" & ControlChars.CrLf
		sSQL &= "					EMUGAME.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "					, EMUGAME.id_Emu_Games_Owner" & ControlChars.CrLf
		sSQL &= "					, EMUGAME.id_DOSBox_Configs_Template" & ControlChars.CrLf
		sSQL &= "					, EMUGAME.id_DOSBox_Configs" & ControlChars.CrLf
		sSQL &= "					, EMUGAME.id_Rombase_DOSBox_Filetypes" & ControlChars.CrLf
		sSQL &= "					, EMUGAME.id_Rombase_DOSBox_Exe_Types" & ControlChars.CrLf
		sSQL &= "					, IFNULL(EMUGAME.Unavailable, 0) AS Unavailable" & ControlChars.CrLf
		sSQL &= "					, EMUGAME.DOSBox_Mount_Destination" & ControlChars.CrLf
		'sSQL &= "					, REPLACE(" & ControlChars.CrLf
		sSQL &= "					,		CASE	WHEN EMUGAME.Name Is NULL" & ControlChars.CrLf
		sSQL &= "									THEN	CASE WHEN GAME.Name Is NULL " & ControlChars.CrLf
		sSQL &= "												THEN IFNULL(EMUGAME.InnerFile, EMUGAME.File)" & ControlChars.CrLf
		sSQL &= "												ELSE IFNULL(EMUGAME.Name_Prefix || ' ', IFNULL(GAME.Name_Prefix || ' ', '')) || IFNULL(GAME.Name, '') || IFNULL(' (' || EMUGAME.Note || ')', '')" & ControlChars.CrLf
		sSQL &= "												END" & ControlChars.CrLf
		sSQL &= "									ELSE	IFNULL(EMUGAME.Name_Prefix || ' ', '') || IFNULL(EMUGAME.Name, '') || IFNULL(' (' || EMUGAME.Note || ')', '')" & ControlChars.CrLf
		sSQL &= "						END" & ControlChars.CrLf
		'		sSQL &= "						, ':', '>')" & ControlChars.CrLf
		sSQL &= "						AS Game" & ControlChars.CrLf
		sSQL &= "					, EMUGAME.Folder" & ControlChars.CrLf
		sSQL &= "					, EMUGAME.File" & ControlChars.CrLf
		sSQL &= "					, EMUGAME.InnerFile" & ControlChars.CrLf
		sSQL &= "					, EMUGAME.J2KPreset" & ControlChars.CrLf
		sSQL &= "					" & ControlChars.CrLf
		sSQL &= "					, EMUGAME.Cache_Regions AS Regions" & ControlChars.CrLf
		sSQL &= "					, EMUGAME.Cache_Languages AS Languages" & ControlChars.CrLf
		sSQL &= "					" & ControlChars.CrLf
		sSQL &= "					, REL.MobyRank AS Rank" & ControlChars.CrLf
		sSQL &= "					, REL.MobyScore AS Score" & ControlChars.CrLf
		sSQL &= "					" & ControlChars.CrLf
		sSQL &= "					, IFNULL(EMUGAME.Year, REL.Year) AS Year" & ControlChars.CrLf

		If cls_Globals.Admin Then
			sSQL &= "					, IFNULL(CASE WHEN USREG.id_Users_Emu_Games IS NULL THEN EMUGAME.Num_Played ELSE USREG.Num_Played END, 0) AS Num_Played" & ControlChars.CrLf
			sSQL &= "					, IFNULL(CASE WHEN USREG.id_Users_Emu_Games IS NULL THEN EMUGAME.Num_Runtime ELSE USREG.Num_Runtime END, 0) AS Num_Runtime" & ControlChars.CrLf
			sSQL &= "					, IFNULL(CASE WHEN USREG.id_Users_Emu_Games IS NULL THEN EMUGAME.Favourite ELSE USREG.Favourite END, 0) AS Favourite" & ControlChars.CrLf
			sSQL &= "					, IFNULL(CASE WHEN USREG.id_Users_Emu_Games IS NULL THEN EMUGAME.Want ELSE USREG.Want END, 0) AS Want" & ControlChars.CrLf
			sSQL &= "					, IFNULL(CASE WHEN USREG.id_Users_Emu_Games IS NULL THEN EMUGAME.Have ELSE USREG.Have END, 0) AS Have" & ControlChars.CrLf
			sSQL &= "					, IFNULL(CASE WHEN USREG.id_Users_Emu_Games IS NULL THEN EMUGAME.Trade ELSE USREG.Trade END, 0) AS Trade" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN USREG.id_Users_Emu_Games IS NULL THEN EMUGAME.Last_Played ELSE USREG.Last_Played END AS Last_Played" & ControlChars.CrLf
		Else
			sSQL &= "					, IFNULL(USREG.Num_Played, 0) AS Num_Played" & ControlChars.CrLf
			sSQL &= "					, IFNULL(USREG.Num_Runtime, 0) AS Num_Runtime" & ControlChars.CrLf
			sSQL &= "					, IFNULL(USREG.Favourite, 0) AS Favourite" & ControlChars.CrLf
			sSQL &= "					, IFNULL(USREG.Want, 0) AS Want" & ControlChars.CrLf
			sSQL &= "					, IFNULL(USREG.Have, 0) AS Have" & ControlChars.CrLf
			sSQL &= "					, IFNULL(USREG.Trade, 0) AS Trade" & ControlChars.CrLf
			sSQL &= "					, USREG.Last_Played AS Last_Played" & ControlChars.CrLf
		End If

		sSQL &= "					, IFNULL(EMUGAME.Rating_Gameplay, 0) AS Rating_Gameplay" & ControlChars.CrLf
		sSQL &= "					, IFNULL(EMUGAME.Rating_Graphics, 0) AS Rating_Graphics" & ControlChars.CrLf
		sSQL &= "					, IFNULL(EMUGAME.Rating_Personal, 0) AS Rating_Personal" & ControlChars.CrLf
		sSQL &= "					, IFNULL(EMUGAME.Rating_Sound, 0) AS Rating_Sound" & ControlChars.CrLf
		sSQL &= "					, IFNULL(EMUGAME.Rating_Story, 0) AS Rating_Story" & ControlChars.CrLf
		sSQL &= "					" & ControlChars.CrLf
		sSQL &= "					, PF.Name AS Platform" & ControlChars.CrLf
		sSQL &= "					, PF.ShortName AS Platform_Short" & ControlChars.CrLf
		sSQL &= "					, PF.RetroAchievements AS Platform_RetroAchievements" & ControlChars.CrLf
		sSQL &= "					, GAME.Name AS Game_NoPrefix" & ControlChars.CrLf
		sSQL &= "					, EMUGAME.created" & ControlChars.CrLf
		sSQL &= "					" & ControlChars.CrLf
		sSQL &= "					-- Optional Main Table" & ControlChars.CrLf
		sSQL &= "					, Cache_Age_Pessimistic AS Age_Pessimistic" & ControlChars.CrLf
		sSQL &= "					, Cache_Age_Optimistic AS Age_Optimistic" & ControlChars.CrLf
		sSQL &= "					, IFNULL(EMUGAME.Publisher, C1.Name) AS Publisher" & ControlChars.CrLf
		sSQL &= "					, IFNULL(EMUGAME.Developer, C2.Name) AS Developer" & ControlChars.CrLf
		sSQL &= "					" & ControlChars.CrLf
		sSQL &= "					-- Details" & ControlChars.CrLf
		sSQL &= "					, IFNULL(EMUGAME.Description, GAME.Description) AS Description" & ControlChars.CrLf
		sSQL &= "					, EMUGAME.SpecialInfo" & ControlChars.CrLf
		sSQL &= "					, REL.Technical_Notes" & ControlChars.CrLf
		sSQL &= "					" & ControlChars.CrLf
		sSQL &= "					, Cache_Alternate_Titles AS Alternate_Titles" & ControlChars.CrLf
		sSQL &= "					, Cache_Basic_Genres AS Basic_Genres" & ControlChars.CrLf
		sSQL &= "					, Cache_Perspectives AS Perspectives" & ControlChars.CrLf
		sSQL &= "					, Cache_Sports_Themes AS Sports_Themes" & ControlChars.CrLf
		sSQL &= "					, Cache_Educational_Categories AS Educational_Categories" & ControlChars.CrLf
		sSQL &= "					, Cache_Other_Attributes AS Other_Attributes" & ControlChars.CrLf
		sSQL &= "					, Cache_Visual_Presentation AS Visual_Presentation" & ControlChars.CrLf
		sSQL &= "					, Cache_Pacing AS Pacing" & ControlChars.CrLf
		sSQL &= "					, Cache_Gameplay AS Gameplay" & ControlChars.CrLf
		sSQL &= "					, Cache_Interface_Control AS Interface_Control" & ControlChars.CrLf
		sSQL &= "					, Cache_Vehicular_Themes AS Vehicular_Themes" & ControlChars.CrLf
		sSQL &= "					, Cache_Setting AS Setting" & ControlChars.CrLf
		sSQL &= "					, Cache_Narrative_Theme_Topic AS Narrative_Theme_Topic" & ControlChars.CrLf
		sSQL &= "					, Cache_DLC_Addon AS DLC_Addon" & ControlChars.CrLf
		sSQL &= "					, Cache_Special_Edition AS Special_Edition" & ControlChars.CrLf
		sSQL &= "					" & ControlChars.CrLf
		sSQL &= "					, REL.URL AS Moby_URL" & ControlChars.CrLf
		sSQL &= "					, PF.URLPart AS Moby_Platforms_URLPart" & ControlChars.CrLf
		sSQL &= "					, EMUGAME.Moby_Games_URLPart" & ControlChars.CrLf
		sSQL &= "					, IFNULL(GAME.Platform_Exclusive, 0) AS Platform_Exclusive" & ControlChars.CrLf
		sSQL &= "					" & ControlChars.CrLf
		sSQL &= "					, Cache_MinPlayers AS MinPlayers" & ControlChars.CrLf
		sSQL &= "					, Cache_MaxPlayers AS MaxPlayers" & ControlChars.CrLf
		sSQL &= "					, IFNULL(PF.MultiVolume, 0) AS MultiVolume" & ControlChars.CrLf
		sSQL &= "					" & ControlChars.CrLf
		sSQL &= "				-- Attributes" & ControlChars.CrLf
		sSQL &= "				, EMUGAME.Version" & ControlChars.CrLf
		sSQL &= "				, IFNULL(EMUGAME.Alt, 0) AS Alt" & ControlChars.CrLf
		sSQL &= "				, IFNULL(EMUGAME.Trainer, 0) AS Trainer" & ControlChars.CrLf
		sSQL &= "				, IFNULL(EMUGAME.Translation, 0) AS Translation" & ControlChars.CrLf
		sSQL &= "				, IFNULL(EMUGAME.Hack, 0) AS Hack" & ControlChars.CrLf
		sSQL &= "				, IFNULL(EMUGAME.Bios, 0) AS Bios" & ControlChars.CrLf
		sSQL &= "				, IFNULL(EMUGAME.Prototype, 0) AS Prototype" & ControlChars.CrLf
		sSQL &= "				, IFNULL(EMUGAME.Alpha, 0) AS Alpha" & ControlChars.CrLf
		sSQL &= "				, IFNULL(EMUGAME.Beta, 0) AS Beta" & ControlChars.CrLf
		sSQL &= "				, IFNULL(EMUGAME.Sample, 0) AS Sample" & ControlChars.CrLf
		sSQL &= "				, IFNULL(EMUGAME.Kiosk, 0) AS Kiosk" & ControlChars.CrLf
		sSQL &= "				, IFNULL(EMUGAME.Unlicensed, 0) AS Unlicensed" & ControlChars.CrLf
		sSQL &= "				, IFNULL(EMUGAME.Fixed, 0) AS Fixed" & ControlChars.CrLf
		sSQL &= "				, IFNULL(EMUGAME.Pirated, 0) AS Pirated" & ControlChars.CrLf
		sSQL &= "				, IFNULL(EMUGAME.Good, 0) AS Good" & ControlChars.CrLf
		sSQL &= "				, IFNULL(EMUGAME.Bad, 0) AS Bad" & ControlChars.CrLf
		sSQL &= "				, IFNULL(EMUGAME.Overdump, 0) AS Overdump" & ControlChars.CrLf
		sSQL &= "				, IFNULL(EMUGAME.PublicDomain, 0) AS PublicDomain" & ControlChars.CrLf
		sSQL &= "				" & ControlChars.CrLf
		sSQL &= "				-- Derived Attributes FROM Moby/Emu_Games_Attributes" & ControlChars.CrLf
		sSQL &= "				, Cache_MP_GameModes AS MP_GameModes" & ControlChars.CrLf
		sSQL &= "				, Cache_MP_Options AS MP_Options" & ControlChars.CrLf
		sSQL &= "				" & ControlChars.CrLf
		sSQL &= "				-- ids" & ControlChars.CrLf
		sSQL &= "				, EMUGAME.id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "				, EMUGAME.id_Moby_Platforms_Alternative" & ControlChars.CrLf
		sSQL &= "				, GAME.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "				, EMUGAME.id_Rombase" & ControlChars.CrLf
		sSQL &= "				, EMUGAME.Volume_Number" & ControlChars.CrLf
		sSQL &= "				, EMUGAME.CustomIdentifier" & ControlChars.CrLf
		sSQL &= "				" & ControlChars.CrLf
		sSQL &= "				,	CAST" & ControlChars.CrLf
		sSQL &= "					(" & ControlChars.CrLf
		sSQL &= "						CAST(" & ControlChars.CrLf
		sSQL &= "							CAST(IFNULL(Rating_Gameplay * RW1.Weight, 0) AS REAL) / 5 + CAST(IFNULL(Rating_Graphics * RW2.Weight, 0) AS REAL) / 5 + CAST(IFNULL(Rating_Personal * RW5.Weight, 0) AS REAL) / 5 + CAST(IFNULL(Rating_Sound * RW3.Weight, 0) AS REAL) / 5 + CAST(IFNULL(Rating_Story * RW4.Weight, 0) AS REAL) / 5 AS REAL" & ControlChars.CrLf
		sSQL &= "						)" & ControlChars.CrLf
		sSQL &= "						/" & ControlChars.CrLf
		sSQL &= "						(" & ControlChars.CrLf
		sSQL &= "							CAST(" & ControlChars.CrLf
		sSQL &= "								CASE WHEN Rating_Gameplay IS NOT NULL THEN RW1.Weight ELSE 0 END" & ControlChars.CrLf
		sSQL &= "								+ CASE WHEN Rating_Graphics IS NOT NULL THEN RW2.Weight ELSE 0 END" & ControlChars.CrLf
		sSQL &= "								+ CASE WHEN Rating_Personal IS NOT NULL THEN RW5.Weight ELSE 0 END" & ControlChars.CrLf
		sSQL &= "								+ CASE WHEN Rating_Sound IS NOT NULL THEN RW3.Weight ELSE 0 END" & ControlChars.CrLf
		sSQL &= "								+ CASE WHEN Rating_Story IS NOT NULL THEN RW4.Weight ELSE 0 END" & ControlChars.CrLf
		sSQL &= "							AS REAL)" & ControlChars.CrLf
		sSQL &= "						)" & ControlChars.CrLf
		sSQL &= "						* 100" & ControlChars.CrLf
		sSQL &= "					AS INTEGER" & ControlChars.CrLf
		sSQL &= "					)" & ControlChars.CrLf
		sSQL &= "					AS Rating" & ControlChars.CrLf
		sSQL &= "				, REL.id_Moby_Releases"

		If id_Similarity_Calculation_Results <> 0 Then
			sSQL &= "					-- Similarity Calculation" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.Similarity ELSE EGS.Similarity END AS Similarity" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[001_Platform] ELSE EGS.[001_Platform] END  AS [001_Platform]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[002_MobyRank] ELSE EGS.[002_MobyRank] END  AS [002_MobyRank]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[003_MobyScore] ELSE EGS.[003_MobyScore] END  AS [003_MobyScore]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[004_Publisher] ELSE EGS.[004_Publisher] END  AS [004_Publisher]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[005_Developer] ELSE EGS.[005_Developer] END  AS [005_Developer]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[006_Year] ELSE EGS.[006_Year] END  AS [006_Year]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[101_Basic_Genres] ELSE EGS.[101_Basic_Genres] END  AS [101_Basic_Genres]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[102_Perspectives] ELSE EGS.[102_Perspectives] END  AS [102_Perspectives]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[103_Sports_Themes] ELSE EGS.[103_Sports_Themes] END  AS [103_Sports_Themes]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[105_Educational_Categories] ELSE EGS.[105_Educational_Categories] END  AS [105_Educational_Categories]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[106_Other_Attributes] ELSE EGS.[106_Other_Attributes] END  AS [106_Other_Attributes]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[107_Visual_Presentation] ELSE EGS.[107_Visual_Presentation] END  AS [107_Visual_Presentation]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[108_Gameplay] ELSE EGS.[108_Gameplay] END  AS [108_Gameplay]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[109_Pacing] ELSE EGS.[109_Pacing] END  AS [109_Pacing]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[110_Narrative_Theme_Topic] ELSE EGS.[110_Narrative_Theme_Topic] END  AS [110_Narrative_Theme_Topic]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[111_Setting] ELSE EGS.[111_Setting] END  AS [111_Setting]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[112_Vehicular_Themes] ELSE EGS.[112_Vehicular_Themes] END  AS [112_Vehicular_Themes]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[113_Interface_Control] ELSE EGS.[113_Interface_Control] END  AS [113_Interface_Control]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[114_DLC_Addon] ELSE EGS.[114_DLC_Addon] END  AS [114_DLC_Addon]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[115_Special_Edition] ELSE EGS.[115_Special_Edition] END  AS [115_Special_Edition]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[201_MinPlayers] ELSE EGS.[201_MinPlayers] END  AS [201_MinPlayers]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[202_MaxPlayers] ELSE EGS.[202_MaxPlayers] END  AS [202_MaxPlayers]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[203_AgeO] ELSE EGS.[203_AgeO] END  AS [203_AgeO]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[204_AgeP] ELSE EGS.[204_AgeP] END  AS [204_AgeP]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[205_Rating_Descriptors] ELSE EGS.[205_Rating_Descriptors] END  AS [205_Rating_Descriptors]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[206_Other_Attributes] ELSE EGS.[206_Other_Attributes] END  AS [206_Other_Attributes]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[207_Multiplayer_Attributes] ELSE EGS.[207_Multiplayer_Attributes] END  AS [207_Multiplayer_Attributes]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[301_Group_Membership] ELSE EGS.[301_Group_Membership] END  AS [301_Group_Membership]" & ControlChars.CrLf
			sSQL &= "					, CASE WHEN EGS.id_Emu_Games IS NULL THEN MRS.[401_Staff] ELSE EGS.[401_Staff] END  AS [401_Staff]" & ControlChars.CrLf
		End If

		sSQL &= "	, IFNULL(EMUGAME.CRC32, RB.CRC) AS CRC32" & ControlChars.CrLf
		sSQL &= "	, IFNULL(EMUGAME.SHA1, RB.SHA1) AS SHA1" & ControlChars.CrLf
		sSQL &= "	, IFNULL(EMUGAME.MD5, RB.MD5) AS MD5" & ControlChars.CrLf

		sSQL &= "	FROM			tbl_Emu_Games EMUGAME" & ControlChars.CrLf
		sSQL &= " " & IIf(cls_Globals.id_Cheevo_Challenges = 0L AndAlso cls_Globals.Restricted AndAlso Not ShowVolumes, "INNER", "LEFT") & " JOIN tbl_Users_Emu_Games USREG ON id_Users = " & cls_Globals.id_Users & " AND EMUGAME.id_Emu_Games = USREG.id_Emu_Games"
		sSQL &= "	LEFT JOIN tbl_Moby_Games GAME ON EMUGAME.Moby_Games_URLPart = GAME.URLPart" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Platforms PF ON EMUGAME.id_Moby_Platforms = PF.id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Platforms PFALT ON EMUGAME.id_Moby_Platforms_Alternative = PFALT.id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Platforms_Settings PFS ON EMUGAME.id_Moby_Platforms = PFS.id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Releases REL ON IFNULL(EMUGAME.id_Moby_Platforms_Alternative, EMUGAME.id_Moby_Platforms) = REL.id_Moby_Platforms AND GAME.id_Moby_Games = REL.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Companies C1 ON REL.Publisher_id_Moby_Companies = C1.id_Moby_Companies" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Companies C2 ON REL.Developer_id_Moby_Companies = C2.id_Moby_Companies" & ControlChars.CrLf
		sSQL &= " LEFT JOIN tbl_Emu_Games_Rating_Weights RW1 ON RW1.id_Emu_Games_Rating_Weights = 1" & ControlChars.CrLf
		sSQL &= " LEFT JOIN tbl_Emu_Games_Rating_Weights RW2 ON RW2.id_Emu_Games_Rating_Weights = 2" & ControlChars.CrLf
		sSQL &= " LEFT JOIN tbl_Emu_Games_Rating_Weights RW3 ON RW3.id_Emu_Games_Rating_Weights = 3" & ControlChars.CrLf
		sSQL &= " LEFT JOIN tbl_Emu_Games_Rating_Weights RW4 ON RW4.id_Emu_Games_Rating_Weights = 4" & ControlChars.CrLf
		sSQL &= " LEFT JOIN tbl_Emu_Games_Rating_Weights RW5 ON RW5.id_Emu_Games_Rating_Weights = 5" & ControlChars.CrLf
		If id_Similarity_Calculation_Results <> 0 Then
			sSQL &= " LEFT JOIN ttb_Emu_Games_Similarity_Calculation EGS ON EMUGAME.id_Emu_Games = EGS.id_Emu_Games" & ControlChars.CrLf
			sSQL &= " LEFT JOIN ttb_Moby_Releases_Similarity_Calculation MRS ON REL.id_Moby_Releases = MRS.id_Moby_Releases" & ControlChars.CrLf
		End If
		sSQL &= "	LEFT JOIN rombase.tbl_Rombase RB ON EMUGAME.id_Rombase = RB.id_Rombase" & ControlChars.CrLf

		If id_Similarity_Calculation_Results <> 0 Then
			'sSQL &= "	LEFT JOIN tbl_Similarity_Calculation_Results SCR ON SCR.id_Similarity_Calculation_Results = " & TC.getSQLFormat(id_Similarity_Calculation_Results) & ControlChars.CrLf
			'sSQL &= "	LEFT JOIN tbl_Similarity_Calculation_Results_Entries SCRE ON SCRE.id_Similarity_Calculation_Results = SCR.id_Similarity_Calculation_Results AND (SCRE.id_Emu_Games = EMUGAME.id_Emu_Games OR (SCRE.id_Emu_Games IS NULL AND SCRE.id_Moby_Releases = REL.id_Moby_Releases))" & ControlChars.CrLf

			'sSQL &= "	LEFT JOIN tbl_Similarity_Calculation_Results_Entries SCRE ON SCRE.id_Similarity_Calculation_Results = " & TC.getSQLFormat(id_Similarity_Calculation_Results) & " AND SCRE.id_Emu_Games = EMUGAME.id_Emu_Games" & ControlChars.CrLf
		End If

		sSQL &= "	WHERE " & IIf(ShowHidden, "			1=1", "			(EMUGAME.Hidden IS NULL OR EMUGAME.Hidden = 0)") & ControlChars.CrLf

		sSQL &= "	AND (PFS.Visible IS NULL OR PFS.Visible = 1)" & ControlChars.CrLf

		If Not ShowVolumes AndAlso id_Emu_Games Is Nothing Then
			sSQL &= "	AND EMUGAME.id_Emu_Games_Owner IS NULL" & ControlChars.CrLf
		End If

		If TC.NZ(id_Emu_Games, 0) <> 0 Then
			If ShowVolumes Then
				sSQL &= " AND (EMUGAME.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " OR EMUGAME.id_Emu_Games_Owner = " & TC.getSQLFormat(id_Emu_Games) & ")" & ControlChars.CrLf
			Else
				sSQL &= " AND EMUGAME.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & ControlChars.CrLf
			End If

		Else
			sSQL &= " AND EMUGAME.id_Emu_Games > 0" & ControlChars.CrLf 'Prevent Emu_Mapping entries from showing up
		End If

		'If TC.NZ(id_Moby_Platforms, 0) > 0 OrElse TC.NZ(id_Moby_Platforms, 0) = cls_Globals.enm_Moby_Platforms.mame Then
		If TC.NZ(id_Moby_Platforms, 0) <> 0 AndAlso TC.NZ(id_Moby_Platforms, 0) <> cls_Globals.enm_Moby_Platforms.ALL Then
			sSQL &= " AND EMUGAME.id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms) & ControlChars.CrLf
		End If

		If TC.NZ(SearchText, "").Length > 0 Then
			SearchText = TC.getSQLParameter("%" & SearchText & "%")

			sSQL &= " AND" & ControlChars.CrLf &
			"( " & ControlChars.CrLf &
			" EMUGAME.Name LIKE " & SearchText & ControlChars.CrLf &
			" OR EMUGAME.File LIKE " & SearchText & ControlChars.CrLf &
			" OR EMUGAME.InnerFile LIKE " & SearchText & ControlChars.CrLf &
			" OR EMUGAME.Description LIKE " & SearchText & ControlChars.CrLf &
			" OR EMUGAME.Note LIKE " & SearchText & ControlChars.CrLf &
			" OR GAME.Name LIKE " & SearchText & ControlChars.CrLf &
			" OR EMUGAME.Name LIKE " & SearchText & ControlChars.CrLf &
			" OR GAME.Description LIKE " & SearchText & ControlChars.CrLf &
			" OR EMUGAME.Description LIKE " & SearchText & ControlChars.CrLf &
			" OR EMUGAME.SpecialInfo LIKE " & SearchText & ControlChars.CrLf &
			" OR Alternate_Titles LIKE " & SearchText & ControlChars.CrLf &
			") " & ControlChars.CrLf
		End If

		If TC.NZ(id_Moby_Game_Groups, 0) > 0 Then
			sSQL &= "	AND GAME.id_Moby_Games IN (" & ControlChars.CrLf
			sSQL &= "		SELECT SQGroup_MR.id_Moby_Games" & ControlChars.CrLf
			sSQL &= "		FROM moby.tbl_Moby_Game_Groups_Moby_Releases SQGroup_MGGMR" & ControlChars.CrLf
			sSQL &= "		INNER JOIN moby.tbl_Moby_Releases SQGroup_MR ON SQGroup_MGGMR.id_Moby_Releases = SQGroup_MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "		WHERE SQGroup_MGGMR.id_Moby_Game_Groups = " & TC.getSQLFormat(id_Moby_Game_Groups) & ControlChars.CrLf
			sSQL &= "	)" & ControlChars.CrLf
		End If

		If TC.NZ(id_Moby_Staff, 0) > 0 Then
			sSQL &= "	AND GAME.id_Moby_Games IN (" & ControlChars.CrLf
			sSQL &= "		SELECT SQStaff_MR.id_Moby_Games" & ControlChars.CrLf
			sSQL &= "		FROM moby.tbl_Moby_Releases_Staff SQStaff_MRS" & ControlChars.CrLf
			sSQL &= "		INNER JOIN moby.tbl_Moby_Releases SQStaff_MR ON SQStaff_MRS.id_Moby_Releases = SQStaff_MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "		WHERE SQStaff_MRS.id_Moby_Staff = " & TC.getSQLFormat(id_Moby_Staff) & ControlChars.CrLf
			sSQL &= "	)"
		End If

		If TC.NZ(id_Cheevo_Challenges, 0) > 0 Then
			'TODO: id_Cheevo_Challenges
			sSQL &= "	AND EMUGAME.id_Emu_Games IN (" & ControlChars.CrLf
			sSQL &= " SELECT CCC.id_Emu_Games FROM tbl_Cheevo_Challenges_Cheevos CCC" & ControlChars.CrLf
			sSQL &= "	LEFT JOIN tbl_Users_Cheevo_Challenges_Cheevos UCCC ON CCC.id_Cheevo_Challenges_Cheevos = UCCC.id_Cheevo_Challenges_Cheevos" & ControlChars.CrLf
			sSQL &= IIf(cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0, " AND id_Users = " & TC.getSQLFormat(cls_Globals.id_Users), " AND id_Users IS NULL") & ControlChars.CrLf
			sSQL &= "	WHERE CCC.id_Cheevo_Challenges = " & TC.getSQLFormat(id_Cheevo_Challenges) & ControlChars.CrLf
			If TC.NZ(Cheevo_Challenges_Tier, 0) > 0 Then
				If Cheevo_Challenges_Show_Completed Then
					sSQL &= "				AND CCC.Tier <= " & TC.getSQLFormat(Cheevo_Challenges_Tier) & ControlChars.CrLf
				Else
					sSQL &= "				AND CCC.Tier = " & TC.getSQLFormat(Cheevo_Challenges_Tier) & ControlChars.CrLf
				End If
			End If

			If Not Cheevo_Challenges_Show_Completed Then
				sSQL &= "		AND" & ControlChars.CrLf
				sSQL &= "		(	" & ControlChars.CrLf
				sSQL &= "			UCCC.id_Users_Cheevo_Challenges_Cheevos IS NULL" & ControlChars.CrLf
				sSQL &= "			OR (IFNULL(CCC.Hardcore, 0) = 0 AND IFNULL(UCCC.Unlocked_Casual, 0) = 0)" & ControlChars.CrLf
				sSQL &= "			OR (IFNULL(CCC.Hardcore, 0) = 1 AND IFNULL(UCCC.Unlocked_Hardcore, 0) = 0)" & ControlChars.CrLf
				sSQL &= "		)	" & ControlChars.CrLf
			End If

			sSQL &= "	)" & ControlChars.CrLf
		End If

		If TC.NZ(id_FilterSets, 0) > 0 Then
			sSQL &= " AND 1 = 1" & ControlChars.CrLf
		End If

		If ShowVolumes Then
			sSQL &= "	ORDER BY EMUGAME.Volume_Number" & ControlChars.CrLf
		End If

		'sSQL &= "	ORDER BY GAME.Name"

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)
	End Sub

	Public Sub Fill_tbl_FilterSets(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As tbl_FilterSetsDataTable, Optional ByVal FilterSetType As enm_FilterSetTypes = enm_FilterSetTypes.All)
		dt.Clear()
		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT id_FilterSets, Type, Name, ApplyGridFilter, GridFilter, 1 AS Sort FROM tbl_FilterSets WHERE 1=1 " & IIf(FilterSetType = 0, "", " AND Type = " & TC.getSQLFormat(FilterSetType)) & IIf(cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0, " AND id_Users = " & TC.getSQLFormat(cls_Globals.id_Users), " AND id_Users IS NULL") & " UNION SELECT 0 AS id_FilterSets, 0 AS Type, 'None' AS Name, 0 AS ApplyGridFilter, NULL AS GridFilter, 0 AS Sort ORDER BY Sort, Name", dt, tran)
	End Sub

	Public Sub Fill_tbl_Emu_Extras(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As tbl_Emu_ExtrasDataTable, Optional ByVal bShowAny As Boolean = False)
		dt.Clear()
		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, IIf(bShowAny, "SELECT 0 AS id_Emu_Extras, 'any' as Name, 0 AS Sort, 'Search for any missing extra' AS Description, 0 AS Hide UNION ", "") & "SELECT id_Emu_Extras, Name, Sort, Description, Hide FROM tbl_Emu_Extras", dt, tran)
	End Sub

	Public Sub Fill_tbl_Emu_Games_Rating_Weights(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As tbl_Emu_Games_Rating_WeightsDataTable)
		dt.Clear()
		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT id_Emu_Games_Rating_Weights, Rating_Category, Weight FROM tbl_Emu_Games_Rating_Weights", dt, tran)
	End Sub

	Public Sub Fill_src_frm_Emu_Game_Edit_Genres(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As src_frm_Emu_Game_Edit_GenresDataTable, ByVal id_Emu_Games As Integer, ByVal id_Rombase As Integer, ByVal MultiEdit As Boolean)
		dt.Clear()

		Dim sSQL As String = ""
		sSQL &= "	SELECT" & ControlChars.CrLf
		sSQL &= "		MG.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "		, MG.id_Moby_Genres_Categories" & ControlChars.CrLf
		sSQL &= "		, MG.Name" & ControlChars.CrLf
		sSQL &= "		, MG.URLPart" & ControlChars.CrLf
		sSQL &= "		, CASE	WHEN EXISTS(SELECT 1 FROM tbl_Emu_Games_Moby_Genres EGMG WHERE EGMG.Used = 1 AND EGMG.id_Moby_Genres = MG.id_Moby_Genres AND EGMG.id_Emu_Games = EMUGAME.id_Emu_Games) THEN 1" & ControlChars.CrLf
		sSQL &= "						WHEN NOT EXISTS(SELECT 1 FROM tbl_Emu_Games_Moby_Genres EGMG WHERE EGMG.Used = 0 AND EGMG.id_Moby_Genres = MG.id_Moby_Genres AND EGMG.id_Emu_Games = EMUGAME.id_Emu_Games) AND EXISTS(SELECT 1 FROM moby.tbl_Moby_Games_Genres MGG WHERE MGG.id_Moby_Genres = MG.id_Moby_Genres AND MGG.id_Moby_Games = GAME.id_Moby_Games) THEN 1" & ControlChars.CrLf
		sSQL &= IIf(MultiEdit, "						WHEN EXISTS(SELECT 1 FROM tbl_Rombase_Moby_Genres RBMG WHERE RBMG.Used = 1 AND RBMG.id_Moby_Genres = MG.id_Moby_Genres AND RBMG.id_Rombase = RB.id_Rombase) THEN 1", "") & ControlChars.CrLf
		sSQL &= IIf(MultiEdit, "						WHEN NOT EXISTS(SELECT 1 FROM tbl_Rombase_Moby_Genres RBMG WHERE RBMG.Used = 0 AND RBMG.id_Moby_Genres = MG.id_Moby_Genres AND RBMG.id_Rombase = RB.id_Rombase) AND EXISTS(SELECT 1 FROM moby.tbl_Moby_Games_Genres MGG WHERE MGG.id_Moby_Genres = MG.id_Moby_Genres AND MGG.id_Moby_Games = GAME.id_Moby_Games) THEN 1", "") & ControlChars.CrLf
		sSQL &= IIf(MultiEdit, "						ELSE NULL" & ControlChars.CrLf, "						ELSE 0" & ControlChars.CrLf)
		sSQL &= "			END" & ControlChars.CrLf
		sSQL &= "			AS Used" & ControlChars.CrLf
		sSQL &= "		, EMUGAME.Moby_Games_URLPart" & ControlChars.CrLf
		sSQL &= "	FROM moby.tbl_Moby_Genres MG" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Emu_Games EMUGAME ON EMUGAME.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & ControlChars.CrLf
		sSQL &= "	LEFT JOIN moby.tbl_Moby_Games GAME ON EMUGAME.Moby_Games_URLPart = GAME.URLPart" & ControlChars.CrLf
		sSQL &= " LEFT JOIN rombase.tbl_Rombase RB ON RB.id_Rombase = " & TC.getSQLFormat(id_Rombase) & ControlChars.CrLf
		sSQL &= " ORDER BY MG.Name"

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)
	End Sub

	Public Sub Fill_src_frm_Emu_Game_Edit_Attributes(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As src_frm_Emu_Game_Edit_AttributesDataTable, ByVal id_Emu_Games As Integer, ByVal id_Rombase As Integer, ByVal MultiEdit As Boolean)
		dt.Clear()

		Dim sSQL As String = ""
		sSQL &= "	SELECT" & ControlChars.CrLf
		sSQL &= "		id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "		, CASE	WHEN ATTC.RatingSystem = 1 OR ATTC.RatingDescriptor = 1 THEN 'Rating Systems'" & ControlChars.CrLf
		sSQL &= "						WHEN ATTC.Name = 'Multiplayer Game Modes' OR ATTC.Name = 'Multiplayer Options' OR ATTC.Name LIKE 'Number of Players%' THEN 'Multiplayer Attributes'" & ControlChars.CrLf
		sSQL &= "						ELSE 'Tech Info'" & ControlChars.CrLf
		sSQL &= "			END" & ControlChars.CrLf
		sSQL &= "			AS CategoryGroup" & ControlChars.CrLf
		sSQL &= "		, ATTC.Name As Category" & ControlChars.CrLf
		sSQL &= "		, ATT.Name AS Attribute" & ControlChars.CrLf
		sSQL &= "		, ATT.Description" & ControlChars.CrLf
		sSQL &= "		, ATT.Rating_Age_From" & ControlChars.CrLf
		sSQL &= "		,	ATTC.RatingSystem" & ControlChars.CrLf
		sSQL &= "		, ATTC.RatingDescriptor" & ControlChars.CrLf
		sSQL &= "		, CASE	WHEN EXISTS(SELECT 1 FROM tbl_Emu_Games_Moby_Attributes EGMA WHERE EGMA.Used = 1 AND EGMA.id_Moby_Attributes = ATT.id_Moby_Attributes AND EGMA.id_Emu_Games = EMUGAME.id_Emu_Games) THEN 1" & ControlChars.CrLf
		sSQL &= "						WHEN NOT EXISTS(SELECT 1 FROM tbl_Emu_Games_Moby_Attributes EGMA WHERE EGMA.Used = 0 AND EGMA.id_Moby_Attributes = ATT.id_Moby_Attributes AND EGMA.id_Emu_Games = EMUGAME.id_Emu_Games) AND EXISTS(SELECT 1 FROM moby.tbl_Moby_Releases_Attributes MRA WHERE REL.id_Moby_Releases = MRA.id_Moby_Releases AND ATT.id_Moby_Attributes = MRA.id_Moby_Attributes) THEN 1" & ControlChars.CrLf
		sSQL &= IIf(MultiEdit, "						WHEN EXISTS(SELECT 1 FROM tbl_Rombase_Moby_Attributes RBMA WHERE RBMA.Used = 1 AND RBMA.id_Moby_Attributes = ATT.id_Moby_Attributes AND RBMA.id_Rombase = RB.id_Rombase) THEN 1", "") & ControlChars.CrLf
		sSQL &= IIf(MultiEdit, "						WHEN NOT EXISTS(SELECT 1 FROM tbl_Rombase_Moby_Attributes RBMA WHERE RBMA.Used = 0 AND RBMA.id_Moby_Attributes = ATT.id_Moby_Attributes AND RBMA.id_Rombase = RB.id_Rombase) AND EXISTS(SELECT 1 FROM moby.tbl_Moby_Releases_Attributes MRA WHERE REL.id_Moby_Releases = MRA.id_Moby_Releases AND ATT.id_Moby_Attributes = MRA.id_Moby_Attributes) THEN 1", "") & ControlChars.CrLf
		sSQL &= IIf(MultiEdit, "						ELSE NULL" & ControlChars.CrLf, "						ELSE 0" & ControlChars.CrLf)
		sSQL &= "			END" & ControlChars.CrLf
		sSQL &= "			AS Used" & ControlChars.CrLf
		sSQL &= "	FROM moby.tbl_Moby_Attributes ATT" & ControlChars.CrLf
		sSQL &= "	INNER JOIN moby.tbl_Moby_Attributes_Categories ATTC ON ATT.id_Moby_Attributes_Categories = ATTC.id_Moby_Attributes_Categories" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Emu_Games EMUGAME ON EMUGAME.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & ControlChars.CrLf
		sSQL &= "	LEFT JOIN moby.tbl_Moby_Games GAME ON EMUGAME.Moby_Games_URLPart = GAME.URLPart" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN moby.tbl_Moby_Releases REL ON REL.id_Moby_Platforms = IFNULL(EMUGAME.id_Moby_Platforms_Alternative, EMUGAME.id_Moby_Platforms) AND GAME.id_Moby_Games = REL.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN rombase.tbl_Rombase RB ON RB.id_Rombase = " & TC.getSQLFormat(id_Rombase) & ControlChars.CrLf
		sSQL &= "	" & ControlChars.CrLf
		sSQL &= "	ORDER BY CategoryGroup, ATTC.Name, ATT.Name" & ControlChars.CrLf

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)
	End Sub

	Public Shared Sub Fill_tbl_Tag_Parser(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As tbl_Tag_ParserDataTable, Optional ByVal id_Tag_Parser As Integer = 0, Optional ByVal New_Found_In_Value As Object = Nothing)
		Dim sSQL As String = "SELECT" & ControlChars.CrLf
		sSQL &= "	id_Tag_Parser" & ControlChars.CrLf
		sSQL &= "	, Found_In" & ControlChars.CrLf
		sSQL &= "	, Apply" & ControlChars.CrLf
		sSQL &= "	, Content" & ControlChars.CrLf
		sSQL &= "	, IFNULL(MV_Group_Criteria, 1) AS MV_Group_Criteria" & ControlChars.CrLf
		sSQL &= "	, MV_Volume_Number" & ControlChars.CrLf
		sSQL &= "	, Note" & ControlChars.CrLf
		sSQL &= "	, Note_HighPriority" & ControlChars.CrLf
		sSQL &= "	, Publisher" & ControlChars.CrLf
		sSQL &= "	, Year" & ControlChars.CrLf
		sSQL &= "	, Bios" & ControlChars.CrLf
		sSQL &= "	, Hack" & ControlChars.CrLf
		sSQL &= "	, Trainer" & ControlChars.CrLf
		sSQL &= "	, Version" & ControlChars.CrLf
		sSQL &= "	, Prototype" & ControlChars.CrLf
		sSQL &= "	, Beta" & ControlChars.CrLf
		sSQL &= "	, Translation" & ControlChars.CrLf
		sSQL &= "	, Alt" & ControlChars.CrLf
		sSQL &= "	, Unlicensed" & ControlChars.CrLf
		sSQL &= "	, Good" & ControlChars.CrLf
		sSQL &= "	, Bad" & ControlChars.CrLf
		sSQL &= "	, Fixed" & ControlChars.CrLf
		sSQL &= "	, Overdump" & ControlChars.CrLf
		sSQL &= "	, Pirated" & ControlChars.CrLf
		sSQL &= "	, Alpha" & ControlChars.CrLf
		sSQL &= "	, Kiosk" & ControlChars.CrLf
		sSQL &= "	, Sample" & ControlChars.CrLf
		sSQL &= "	, En" & ControlChars.CrLf
		sSQL &= "	, Ja" & ControlChars.CrLf
		sSQL &= "	, Fr" & ControlChars.CrLf
		sSQL &= "	, De" & ControlChars.CrLf
		sSQL &= "	, Es" & ControlChars.CrLf
		sSQL &= "	, It" & ControlChars.CrLf
		sSQL &= "	, Nl" & ControlChars.CrLf
		sSQL &= "	, Pt" & ControlChars.CrLf
		sSQL &= "	, Sv" & ControlChars.CrLf
		sSQL &= "	, No" & ControlChars.CrLf
		sSQL &= "	, Da" & ControlChars.CrLf
		sSQL &= "	, Fi" & ControlChars.CrLf
		sSQL &= "	, Zh" & ControlChars.CrLf
		sSQL &= "	, Ko" & ControlChars.CrLf
		sSQL &= "	, Pl" & ControlChars.CrLf
		sSQL &= "	, Hu" & ControlChars.CrLf
		sSQL &= "	, Gr" & ControlChars.CrLf
		sSQL &= "	, Ar" & ControlChars.CrLf
		sSQL &= "	, Be" & ControlChars.CrLf
		sSQL &= "	, Cz" & ControlChars.CrLf
		sSQL &= "	, Ru" & ControlChars.CrLf
		sSQL &= "	, Sl" & ControlChars.CrLf
		sSQL &= "	, Sr" & ControlChars.CrLf
		sSQL &= "	, NTSC" & ControlChars.CrLf
		sSQL &= "	, PAL" & ControlChars.CrLf
		sSQL &= "	, World" & ControlChars.CrLf
		sSQL &= "	, Europe" & ControlChars.CrLf
		sSQL &= "	, USA" & ControlChars.CrLf
		sSQL &= "	, Asia" & ControlChars.CrLf
		sSQL &= "	, Australia" & ControlChars.CrLf
		sSQL &= "	, Japan" & ControlChars.CrLf
		sSQL &= "	, Korea" & ControlChars.CrLf
		sSQL &= "	, China" & ControlChars.CrLf
		sSQL &= "	, Brazil" & ControlChars.CrLf
		sSQL &= "	, Canada" & ControlChars.CrLf
		sSQL &= "	, France" & ControlChars.CrLf
		sSQL &= "	, Germany" & ControlChars.CrLf
		sSQL &= "	, HongKong" & ControlChars.CrLf
		sSQL &= "	, Italy" & ControlChars.CrLf
		sSQL &= "	, Netherlands" & ControlChars.CrLf
		sSQL &= "	, Russia" & ControlChars.CrLf
		sSQL &= "	, Spain" & ControlChars.CrLf
		sSQL &= "	, Sweden" & ControlChars.CrLf
		sSQL &= "	, Taiwan" & ControlChars.CrLf
		sSQL &= "	FROM tbl_Tag_Parser" & ControlChars.CrLf
		sSQL &= "	WHERE 1 = 1"

		If id_Tag_Parser <> 0 Then
			sSQL &= "	AND id_Tag_Parser = " & TC.getSQLFormat(id_Tag_Parser) & ControlChars.CrLf
		End If

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)

		If New_Found_In_Value IsNot Nothing Then
			Dim rows As DataRow() = dt.Select("id_Tag_Parser = " & TC.getSQLFormat(id_Tag_Parser))
			If rows.Length = 1 Then
				rows(0)("Found_In") = New_Found_In_Value
			End If
		End If
	End Sub

	Public Shared Sub Fill_tbl_Rombase_Tag_Parser(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As tbl_Tag_ParserDataTable, Optional ByVal id_Rombase_Tag_Parser As Integer = 0, Optional ByVal New_Found_In_Value As Object = Nothing)
		Dim sSQL As String = "SELECT" & ControlChars.CrLf
		sSQL &= "	id_Rombase_Tag_Parser" & ControlChars.CrLf
		sSQL &= "	, Found_In" & ControlChars.CrLf
		sSQL &= "	, Apply" & ControlChars.CrLf
		sSQL &= "	, Content" & ControlChars.CrLf
		sSQL &= "	, IFNULL(MV_Group_Criteria, 1) AS MV_Group_Criteria" & ControlChars.CrLf
		sSQL &= "	, MV_Volume_Number" & ControlChars.CrLf
		sSQL &= "	, Note" & ControlChars.CrLf
		sSQL &= "	, Note_HighPriority" & ControlChars.CrLf
		sSQL &= "	, Publisher" & ControlChars.CrLf
		sSQL &= "	, Year" & ControlChars.CrLf
		sSQL &= "	, Bios" & ControlChars.CrLf
		sSQL &= "	, Hack" & ControlChars.CrLf
		sSQL &= "	, Trainer" & ControlChars.CrLf
		sSQL &= "	, Version" & ControlChars.CrLf
		sSQL &= "	, Prototype" & ControlChars.CrLf
		sSQL &= "	, Beta" & ControlChars.CrLf
		sSQL &= "	, Translation" & ControlChars.CrLf
		sSQL &= "	, Alt" & ControlChars.CrLf
		sSQL &= "	, Unlicensed" & ControlChars.CrLf
		sSQL &= "	, Good" & ControlChars.CrLf
		sSQL &= "	, Bad" & ControlChars.CrLf
		sSQL &= "	, Fixed" & ControlChars.CrLf
		sSQL &= "	, Overdump" & ControlChars.CrLf
		sSQL &= "	, Pirated" & ControlChars.CrLf
		sSQL &= "	, Alpha" & ControlChars.CrLf
		sSQL &= "	, Kiosk" & ControlChars.CrLf
		sSQL &= "	, Sample" & ControlChars.CrLf
		sSQL &= "	, En" & ControlChars.CrLf
		sSQL &= "	, Ja" & ControlChars.CrLf
		sSQL &= "	, Fr" & ControlChars.CrLf
		sSQL &= "	, De" & ControlChars.CrLf
		sSQL &= "	, Es" & ControlChars.CrLf
		sSQL &= "	, It" & ControlChars.CrLf
		sSQL &= "	, Nl" & ControlChars.CrLf
		sSQL &= "	, Pt" & ControlChars.CrLf
		sSQL &= "	, Sv" & ControlChars.CrLf
		sSQL &= "	, No" & ControlChars.CrLf
		sSQL &= "	, Da" & ControlChars.CrLf
		sSQL &= "	, Fi" & ControlChars.CrLf
		sSQL &= "	, Zh" & ControlChars.CrLf
		sSQL &= "	, Ko" & ControlChars.CrLf
		sSQL &= "	, Pl" & ControlChars.CrLf
		sSQL &= "	, Hu" & ControlChars.CrLf
		sSQL &= "	, Gr" & ControlChars.CrLf
		sSQL &= "	, Ar" & ControlChars.CrLf
		sSQL &= "	, Be" & ControlChars.CrLf
		sSQL &= "	, Cz" & ControlChars.CrLf
		sSQL &= "	, Ru" & ControlChars.CrLf
		sSQL &= "	, Sl" & ControlChars.CrLf
		sSQL &= "	, Sr" & ControlChars.CrLf
		sSQL &= "	, NTSC" & ControlChars.CrLf
		sSQL &= "	, PAL" & ControlChars.CrLf
		sSQL &= "	, World" & ControlChars.CrLf
		sSQL &= "	, Europe" & ControlChars.CrLf
		sSQL &= "	, USA" & ControlChars.CrLf
		sSQL &= "	, Asia" & ControlChars.CrLf
		sSQL &= "	, Australia" & ControlChars.CrLf
		sSQL &= "	, Japan" & ControlChars.CrLf
		sSQL &= "	, Korea" & ControlChars.CrLf
		sSQL &= "	, China" & ControlChars.CrLf
		sSQL &= "	, Brazil" & ControlChars.CrLf
		sSQL &= "	, Canada" & ControlChars.CrLf
		sSQL &= "	, France" & ControlChars.CrLf
		sSQL &= "	, Germany" & ControlChars.CrLf
		sSQL &= "	, HongKong" & ControlChars.CrLf
		sSQL &= "	, Italy" & ControlChars.CrLf
		sSQL &= "	, Netherlands" & ControlChars.CrLf
		sSQL &= "	, Russia" & ControlChars.CrLf
		sSQL &= "	, Spain" & ControlChars.CrLf
		sSQL &= "	, Sweden" & ControlChars.CrLf
		sSQL &= "	, Taiwan" & ControlChars.CrLf
		sSQL &= "	FROM tbl_Rombase_Tag_Parser" & ControlChars.CrLf
		sSQL &= "	WHERE 1 = 1"

		If id_Rombase_Tag_Parser <> 0 Then
			sSQL &= "	AND id_Rombase_Tag_Parser = " & TC.getSQLFormat(id_Rombase_Tag_Parser) & ControlChars.CrLf
		End If

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)

		If New_Found_In_Value IsNot Nothing Then
			Dim rows As DataRow() = dt.Select("id_Rombase_Tag_Parser = " & TC.getSQLFormat(id_Rombase_Tag_Parser))
			If rows.Length = 1 Then
				rows(0)("Found_In") = New_Found_In_Value
			End If
		End If
	End Sub

	Public Shared Sub Fill_tbl_Emu_Games_Languages(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As tbl_Emu_Games_LanguagesDataTable, Optional ByVal id_Moby_Platforms As Integer = 0, Optional ByVal id_Emu_Games As Integer = 0)
		Dim sSQL As String = ""
		sSQL &= "	SELECT id_Emu_Games_Languages"
		sSQL &= ", id_Emu_Games"
		sSQL &= ", id_Languages"
		sSQL &= " FROM tbl_Emu_Games_Languages"

		If id_Emu_Games = 0 And id_Moby_Platforms > 0 Then
			sSQL &= " WHERE id_Emu_Games IN (SELECT id_Emu_Games FROM tbl_Emu_Games WHERE id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms) & ")"
		End If

		If id_Emu_Games > 0 Then
			sSQL &= " WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games)
		End If

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)
	End Sub

	Public Shared Sub Fill_tbl_Emu_Games_Regions(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As tbl_Emu_Games_RegionsDataTable, Optional ByVal id_Moby_Platforms As Integer = 0, Optional ByVal id_Emu_Games As Integer = 0)
		Dim sSQL As String = ""
		sSQL &= "SELECT"
		sSQL &= " id_Emu_Games_Regions"
		sSQL &= ", id_Emu_Games"
		sSQL &= ", id_Regions"
		sSQL &= " FROM tbl_Emu_Games_Regions"

		If id_Emu_Games = 0 And id_Moby_Platforms > 0 Then
			sSQL &= " WHERE id_Emu_Games IN (SELECT id_Emu_Games FROM tbl_Emu_Games WHERE id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms) & ")"
		End If

		If id_Emu_Games > 0 Then
			sSQL &= " WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games)
		End If

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)
	End Sub

	Public Shared Function Fill_src_frm_Rom_Manager_Emu_Games(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As tbl_Emu_GamesDataTable, ByVal id_Moby_Platforms As Integer, Optional ByVal id_Emu_Games As Integer = 0, Optional ByVal id_Emu_Games_Owner As Integer = 0, Optional ByVal Only_DOSBox_Filetype As cls_Globals.enm_Rombase_DOSBox_Filetypes = 0) As DS_ML.tbl_Emu_GamesDataTable
		If dt Is Nothing Then
			dt = New DS_ML.tbl_Emu_GamesDataTable
		End If

		sSQL = ""
		sSQL &= "SELECT"
		sSQL &= "	EMUGAMES.id_Emu_Games"
		sSQL &= "	, EMUGAMES.id_Emu_Games_Owner"
		sSQL &= "	, EMUGAMES.id_DOSBox_Configs_Template"
		sSQL &= "	, EMUGAMES.id_DOSBox_Configs"
		sSQL &= "	, EMUGAMES.id_ScummVM_Configs_Template"
		sSQL &= "	, EMUGAMES.id_ScummVM_Configs"
		sSQL &= "	, EMUGAMES.id_Rombase_DOSBox_Filetypes"
		sSQL &= "	, EMUGAMES.id_Rombase_DOSBox_Exe_Types"
		sSQL &= "	, EMUGAMES.CustomIdentifier"
		sSQL &= "	, IFNULL(EMUGAMES.Unavailable, 0) AS Unavailable"
		sSQL &= "	, EMUGAMES.DOSBox_Mount_Destination"
		sSQL &= "	, EMUGAMES.Volume_Number"
		sSQL &= "	, EMUGAMES.Filtered_Name"
		sSQL &= "	, EMUGAMES.Hidden"
		sSQL &= "	, EMUGAMES.Moby_Games_URLPart"
		sSQL &= "	, EMUGAMES.id_Moby_Platforms"
		sSQL &= "	, EMUGAMES.id_Moby_Platforms_Alternative"
		sSQL &= "	, EMUGAMES.id_Rombase"
		sSQL &= "	, EMUGAMES.id_Emulators"
		sSQL &= "	, EMUGAMES.Folder"
		sSQL &= "	, EMUGAMES.File"
		sSQL &= "	, EMUGAMES.InnerFile"
		sSQL &= "	, EMUGAMES.Size"
		sSQL &= "	, EMUGAMES.CRC32"
		sSQL &= "	, EMUGAMES.SHA1"
		sSQL &= "	, EMUGAMES.MD5"
		sSQL &= "	, EMUGAMES.Name"
		sSQL &= "	, EMUGAMES.Name_USR"
		sSQL &= "	, EMUGAMES.Name_Prefix"
		sSQL &= "	, EMUGAMES.Name_Prefix_USR"
		sSQL &= "	, EMUGAMES.Note"
		sSQL &= "	, EMUGAMES.Note_USR"
		sSQL &= "	, EMUGAMES.Publisher"
		sSQL &= "	, EMUGAMES.Publisher_USR"
		sSQL &= "	, EMUGAMES.Publisher_id_Moby_Companies"
		sSQL &= "	, EMUGAMES.Publisher_id_Moby_Companies_USR"
		sSQL &= "	, EMUGAMES.Developer"
		sSQL &= "	, EMUGAMES.Developer_USR"
		sSQL &= "	, EMUGAMES.Developer_id_Moby_Companies"
		sSQL &= "	, EMUGAMES.Developer_id_Moby_Companies_USR"
		sSQL &= "	, EMUGAMES.Description"
		sSQL &= "	, EMUGAMES.Description_USR"
		sSQL &= "	, EMUGAMES.Favourite"
		sSQL &= "	, EMUGAMES.Rating_Gameplay"
		sSQL &= "	, EMUGAMES.Rating_Graphics"
		sSQL &= "	, EMUGAMES.Rating_Sound"
		sSQL &= "	, EMUGAMES.Rating_Story"
		sSQL &= "	, EMUGAMES.Rating_Personal"
		sSQL &= "	, EMUGAMES.Num_Played"
		sSQL &= "	, EMUGAMES.Num_Runtime"
		sSQL &= "	, EMUGAMES.Year"
		sSQL &= "	, EMUGAMES.Year_USR"
		sSQL &= "	, EMUGAMES.Version"
		sSQL &= "	, EMUGAMES.Version_USR"
		sSQL &= "	, EMUGAMES.Alt"
		sSQL &= "	, EMUGAMES.Alt_USR"
		sSQL &= "	, EMUGAMES.Trainer"
		sSQL &= "	, EMUGAMES.Trainer_USR"
		sSQL &= "	, EMUGAMES.Translation"
		sSQL &= "	, EMUGAMES.Translation_USR"
		sSQL &= "	, EMUGAMES.Hack"
		sSQL &= "	, EMUGAMES.Hack_USR"
		sSQL &= "	, EMUGAMES.Bios"
		sSQL &= "	, EMUGAMES.Bios_USR"
		sSQL &= "	, EMUGAMES.Prototype"
		sSQL &= "	, EMUGAMES.Prototype_USR"
		sSQL &= "	, EMUGAMES.Alpha"
		sSQL &= "	, EMUGAMES.Alpha_USR"
		sSQL &= "	, EMUGAMES.Beta"
		sSQL &= "	, EMUGAMES.Beta_USR"
		sSQL &= "	, EMUGAMES.Sample"
		sSQL &= "	, EMUGAMES.Sample_USR"
		sSQL &= "	, EMUGAMES.Kiosk"
		sSQL &= "	, EMUGAMES.Kiosk_USR"
		sSQL &= "	, EMUGAMES.Unlicensed"
		sSQL &= "	, EMUGAMES.Unlicensed_USR"
		sSQL &= "	, EMUGAMES.Fixed"
		sSQL &= "	, EMUGAMES.Fixed_USR"
		sSQL &= "	, EMUGAMES.Pirated"
		sSQL &= "	, EMUGAMES.Pirated_USR"
		sSQL &= "	, EMUGAMES.Good"
		sSQL &= "	, EMUGAMES.Good_USR"
		sSQL &= "	, EMUGAMES.Bad"
		sSQL &= "	, EMUGAMES.Bad_USR"
		sSQL &= "	, EMUGAMES.Overdump"
		sSQL &= "	, EMUGAMES.Overdump_USR"
		sSQL &= "	, EMUGAMES.PublicDomain"
		sSQL &= "	, EMUGAMES.PublicDomain_USR"
		sSQL &= "	, ROMBASE.id_Moby_Platforms AS ROMBASE_id_Moby_Platforms"
		sSQL &= "	, EMUGAMES.created"
		sSQL &= " , CASE WHEN REL.deprecated = 1 THEN 1 ELSE MG.deprecated END AS deprecated"
		sSQL &= " , EMUGAMES.TDL_Show_in_Menu"
		sSQL &= " , EMUGAMES.TDL_DisplayText"
		sSQL &= " , EMUGAMES.TDL_Sort"
		sSQL &= "	FROM tbl_Emu_Games EMUGAMES"
		sSQL &= "	LEFT JOIN tbl_Rombase ROMBASE ON EMUGAMES.id_Rombase = ROMBASE.id_Rombase"
		sSQL &= "	LEFT JOIN tbl_Moby_Games MG ON EMUGAMES.Moby_Games_URLPart = MG.URLPart"
		sSQL &= " LEFT JOIN tbl_Moby_Releases REL ON REL.id_Moby_Games = MG.id_Moby_Games AND REL.id_Moby_Platforms = EMUGAMES.id_Moby_Platforms"

		sSQL &= "	WHERE 1=1"

		If id_Emu_Games = 0 Then
			sSQL &= "	AND EMUGAMES.id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms)
		ElseIf id_Emu_Games > 0 Then
			sSQL &= "	AND ( EMUGAMES.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games)

			If id_Emu_Games_Owner > 0 Then
				sSQL &= " OR EMUGAMES.id_Emu_Games_Owner = " & TC.getSQLFormat(id_Emu_Games)
			End If

			sSQL &= "	)"
		End If

		If Only_DOSBox_Filetype <> 0 Then
			sSQL &= "	AND EMUGAMES.id_Rombase_DOSBox_Filetypes = " & Only_DOSBox_Filetype
		End If

		sSQL &= "	ORDER BY File, InnerFile"

		DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)

		Return dt
	End Function

	Public Shared Sub Fill_src_frm_Rom_Manager_Moby_Releases(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DS_MobyDB.src_Moby_ReleasesDataTable, ByVal id_Moby_Platforms As Integer)
		Dim sSQL As String = ""
		sSQL &= "	Select" & ControlChars.CrLf
		sSQL &= "		REL.id_Moby_Releases As id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "		, IFNULL(GAME.Name_Prefix || ' ', '') || GAME.Name AS GameName" & ControlChars.CrLf
		sSQL &= "		, REL.id_Moby_Platforms AS id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "		, soundex(GAME.Name) AS Soundex" & ControlChars.CrLf
		sSQL &= "		, GAME.URLPart AS Moby_Games_URLPart" & ControlChars.CrLf
		sSQL &= "		, REL.Year AS Year" & ControlChars.CrLf
		sSQL &= "		, REL.created AS created" & ControlChars.CrLf
		sSQL &= "		, REL.Developer_id_Moby_Companies AS Developer_id_Moby_Companies" & ControlChars.CrLf
		sSQL &= "		, DEV.Name AS Developer" & ControlChars.CrLf
		sSQL &= "		, REL.Publisher_id_Moby_Companies AS Publisher_id_Moby_Companies" & ControlChars.CrLf
		sSQL &= "		, PUB.Name AS Publisher" & ControlChars.CrLf
		sSQL &= "		, CASE WHEN REL.deprecated = 1 THEN 1 ELSE GAME.deprecated END AS deprecated" & ControlChars.CrLf
		sSQL &= "		, CASE WHEN EXISTS(" & ControlChars.CrLf
		sSQL &= "				SELECT 1" & ControlChars.CrLf
		sSQL &= "				FROM moby.tbl_Moby_Games_Genres MGG" & ControlChars.CrLf
		sSQL &= "				INNER JOIN moby.tbl_Moby_Genres MG ON MGG.id_Moby_Genres = MG.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "				WHERE MGG.id_Moby_Games = REL.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "							AND MG.URLPart = 'compilation-shovelware'" & ControlChars.CrLf
		sSQL &= "			) THEN 1 ELSE 0 END AS compilation" & ControlChars.CrLf
		sSQL &= "		, PLTFM.ShortName AS Platform" & ControlChars.CrLf
		sSQL &= "		, PLTFM.URLPart AS Moby_Platforms_URLPart" & ControlChars.CrLf
		sSQL &= "	FROM moby.tbl_Moby_Releases REL" & ControlChars.CrLf
		sSQL &= "	INNER JOIN moby.tbl_Moby_Games GAME ON REL.id_Moby_Games = GAME.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "	INNER JOIN moby.tbl_Moby_Platforms PLTFM ON REL.id_Moby_Platforms = PLTFM.id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN moby.tbl_Moby_Companies DEV ON REL.Developer_id_Moby_Companies = DEV.id_Moby_Companies" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN moby.tbl_Moby_Companies PUB ON REL.Publisher_id_Moby_Companies = PUB.id_Moby_Companies" & ControlChars.CrLf

		If TC.NZ(id_Moby_Platforms, 0) <> 0 AndAlso id_Moby_Platforms <> cls_Globals.enm_Moby_Platforms.ALL AndAlso id_Moby_Platforms <> cls_Globals.enm_Moby_Platforms.scummvm Then
			sSQL &= "	WHERE REL.id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms) & ControlChars.CrLf
		End If

		sSQL &= "	UNION" & ControlChars.CrLf
		sSQL &= "	SELECT" & ControlChars.CrLf
		sSQL &= "		-REL.id_Moby_Releases AS id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "		, MGAT.Alternate_Title || ' [' || IFNULL(MGAT.Description, 'NODESCRIPTION') || '; ' || IFNULL(GAME.Name_Prefix || ' ', '') || GAME.Name || ']' AS GameName" & ControlChars.CrLf
		sSQL &= "		, REL.id_Moby_Platforms AS id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "		, NULL AS Soundex" & ControlChars.CrLf
		sSQL &= "		, '\' || GAME.URLPart AS Moby_Games_URLPart" & ControlChars.CrLf
		sSQL &= "		, REL.Year AS Year" & ControlChars.CrLf
		sSQL &= "		, REL.created AS created" & ControlChars.CrLf
		sSQL &= "		, REL.Developer_id_Moby_Companies AS Developer_id_Moby_Companies" & ControlChars.CrLf
		sSQL &= "		, DEV.Name AS Developer" & ControlChars.CrLf
		sSQL &= "		, REL.Publisher_id_Moby_Companies AS Publisher_id_Moby_Companies" & ControlChars.CrLf
		sSQL &= "		, PUB.Name AS Publisher" & ControlChars.CrLf
		sSQL &= "		, CASE WHEN REL.deprecated = 1 THEN 1 ELSE GAME.deprecated END AS deprecated" & ControlChars.CrLf
		sSQL &= "		, CASE WHEN EXISTS(" & ControlChars.CrLf
		sSQL &= "				SELECT 1" & ControlChars.CrLf
		sSQL &= "				FROM moby.tbl_Moby_Games_Genres MGG" & ControlChars.CrLf
		sSQL &= "				INNER JOIN moby.tbl_Moby_Genres MG ON MGG.id_Moby_Genres = MG.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "				WHERE MGG.id_Moby_Games = REL.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "							AND MG.URLPart = 'compilation-shovelware'" & ControlChars.CrLf
		sSQL &= "			) THEN 1 ELSE 0 END AS compilation" & ControlChars.CrLf
		sSQL &= "		, PLTFM.ShortName AS Platform" & ControlChars.CrLf
		sSQL &= "		, PLTFM.URLPart AS Moby_Platforms_URLPart" & ControlChars.CrLf
		sSQL &= "	FROM moby.tbl_Moby_Games_Alternate_Titles MGAT" & ControlChars.CrLf
		sSQL &= "	INNER JOIN moby.tbl_Moby_Games GAME ON MGAT.id_Moby_Games = GAME.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "	INNER JOIN moby.tbl_Moby_Releases REL ON REL.id_Moby_Games = GAME.id_Moby_Games"
		sSQL &= "	INNER JOIN moby.tbl_Moby_Platforms PLTFM ON REL.id_Moby_Platforms = PLTFM.id_Moby_Platforms" & ControlChars.CrLf

		If TC.NZ(id_Moby_Platforms, 0) <> 0 AndAlso id_Moby_Platforms <> cls_Globals.enm_Moby_Platforms.ALL AndAlso id_Moby_Platforms <> cls_Globals.enm_Moby_Platforms.scummvm Then
			sSQL &= " AND REL.id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms) & ControlChars.CrLf
		End If

		sSQL &= "	LEFT JOIN moby.tbl_Moby_Companies DEV ON REL.Developer_id_Moby_Companies = DEV.id_Moby_Companies" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN moby.tbl_Moby_Companies PUB ON REL.Publisher_id_Moby_Companies = PUB.id_Moby_Companies" & ControlChars.CrLf

		sSQL &= "	ORDER BY GameName"
		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)

	End Sub

	Public Shared Sub Fill_tbl_History(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DS_ML.tbl_HistoryDataTable, Optional ByVal id_Emu_Games As Object = Nothing)
		dt.Clear()

		Dim conn As SQLite.SQLiteConnection

		If tran IsNot Nothing Then
			conn = tran.Connection
		Else
			conn = cls_Globals.Conn
		End If

		Dim sSQL As String = ""
		sSQL &= "	SELECT"
		sSQL &= "		id_History"
		sSQL &= "		, id_Emu_Games"
		sSQL &= "		, Start"
		sSQL &= "		, End"
		sSQL &= "		, strftime('%s', End) - strftime('%s', Start) AS Runtime"
		sSQL &= "	FROM tbl_History"
		sSQL &= "	WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games)

		If Not cls_Globals.Admin Then
			sSQL &= " AND id_Users = " & TC.getSQLFormat(cls_Globals.id_Users)
		Else
			sSQL &= " AND id_Users IS NULL"
		End If

		DataAccess.FireProcedureReturnDT(conn, 0, False, sSQL, dt, tran)
	End Sub

	Public Shared Sub Fill_tbl_ImageEditorSettings(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DS_ML.tbl_ImageEditorTemplatesDataTable)
		dt.Clear()

		Dim sSQL As String = ""
		sSQL &= "	SELECT"
		sSQL &= "		0 AS Sort, 0 AS id_ImageEditorTemplates, 'Default' AS Title, 0 AS Top, 0 AS Bottom, 0 AS Left, 0 AS Right"
		sSQL &= " UNION"
		sSQL &= " SELECT"
		sSQL &= "		1 AS Sort"
		sSQL &= "		, id_ImageEditorTemplates"
		sSQL &= "		, IFNULL(Title, '<No Title>') || ' (' || Top || '; ' || Bottom || '; ' || Left || '; ' || Right || ')' As Title"
		sSQL &= "		, Top"
		sSQL &= "		, Bottom"
		sSQL &= "		, Left"
		sSQL &= "		, Right"
		sSQL &= "	FROM tbl_ImageEditorTemplates ORDER BY Sort, Title"

		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

	Public Shared Sub Fill_src_ucr_Emulation_Game_Groups(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DS_ML.src_ucr_Emulation_GameGroupsDataTable, ByVal id_Moby_Games As Object, ByVal id_Moby_Platforms As Object)
		dt.Clear()

		Dim sSQL As String = ""
		sSQL &= "	SELECT"
		sSQL &= "					MGGMR.id_Moby_Game_Groups"
		sSQL &= "					, MGG.Name"
		sSQL &= "					, (SELECT COUNT(1) FROM tbl_Moby_Game_Groups_Moby_Releases WHERE id_Moby_Game_Groups = MGGMR.id_Moby_Game_Groups) AS GameCount"
		sSQL &= "	FROM tbl_Moby_Game_Groups_Moby_Releases MGGMR"
		sSQL &= "	INNER JOIN tbl_Moby_Game_Groups MGG ON MGGMR.id_Moby_Game_Groups = MGG.id_Moby_Game_Groups"
		sSQL &= "	INNER JOIN tbl_Moby_Releases MR ON MGGMR.id_Moby_Releases = MR.id_Moby_Releases"
		sSQL &= "	INNER JOIN tbl_Moby_Games MG ON MR.id_Moby_Games = MG.id_Moby_Games"
		sSQL &= "	WHERE MR.id_Moby_Games = " & TC.getSQLFormat(id_Moby_Games) & " AND MR.id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms)
		sSQL &= "				AND MGG.Name <> """""
		sSQL &= "	ORDER BY MGG.Name"

		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

	Public Shared Sub Fill_src_ucr_Emulation_Moby_Releases_Staff(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DS_ML.src_ucr_Emulation_Moby_Releases_StaffDataTable, ByVal id_Moby_Games As Object, ByVal id_Moby_Platforms As Object)
		dt.Clear()

		Dim sSQL As String = ""
		sSQL &= "	SELECT"
		sSQL &= "					MRS.id_Moby_Releases_Staff"
		sSQL &= "					, MS.id_Moby_Staff"
		sSQL &= "					, MRS.Position"
		sSQL &= "					, MS.Name"
		sSQL &= "					, MRS.Sort"
		sSQL &= "	FROM tbl_Moby_Releases_Staff MRS"
		sSQL &= "	INNER JOIN tbl_Moby_Staff MS ON MRS.id_Moby_Staff = MS.id_Moby_Staff"
		sSQL &= "	INNER JOIN tbl_Moby_Releases MR ON MRS.id_Moby_Releases = MR.id_Moby_Releases"
		sSQL &= "	INNER JOIN tbl_Moby_Games MG ON MR.id_Moby_Games = MG.id_Moby_Games"
		sSQL &= "	WHERE	MR.id_Moby_Games = " & TC.getSQLFormat(id_Moby_Games)
		sSQL &= "				AND MR.id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms)
		sSQL &= "				AND MS.Name <> """""
		sSQL &= "	ORDER BY MRS.Sort"

		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

	Public Shared Sub Fill_src_ucr_Emulation_cmb_Groups(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DS_ML.src_ucr_Emulation_cmb_GroupsDataTable)
		dt.Clear()

		Dim sSQL As String = ""
		sSQL &= "	SELECT"
		sSQL &= "					id_Moby_Game_Groups"
		sSQL &= "					, Name"
		sSQL &= "					, 1 AS Sort"
		sSQL &= "	FROM tbl_Moby_Game_Groups MGG"
		sSQL &= "	WHERE Name <> """""
		sSQL &= "	UNION"
		sSQL &= "	SELECT"
		sSQL &= "					0 AS id_Moby_Game_Groups"
		sSQL &= "					, 'None' AS Name"
		sSQL &= "					, 0 AS Sort"
		sSQL &= "	ORDER BY Sort, MGG.Name"

		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

	Public Shared Sub Fill_src_ucr_Emulation_cmb_Staff(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DS_ML.src_ucr_Emulation_cmb_StaffDataTable)
		dt.Clear()

		Dim sSQL As String = ""
		sSQL &= "	SELECT"
		sSQL &= "					id_Moby_Staff"
		sSQL &= "					, Name"
		sSQL &= "					, 1 AS Sort"
		sSQL &= "	FROM tbl_Moby_Staff MS"
		sSQL &= "	WHERE Name <> """""
		sSQL &= "	UNION"
		sSQL &= "	SELECT"
		sSQL &= "					0 AS id_Moby_Staff"
		sSQL &= "					, 'None' AS Name"
		sSQL &= "					, 0 AS Sort"
		sSQL &= "	ORDER BY Sort, MS.Name"

		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

	Public Shared Sub Fill_src_ucr_Emulation_cmb_Similarity_Calculation_Results(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DS_ML.src_ucr_Emulation_cmb_Similarity_Calculation_ResultsDataTable)
		dt.Clear()

		Dim sSQL As String = ""
		sSQL &= "	SELECT"
		sSQL &= "					id_Similarity_Calculation_Results"
		sSQL &= "					, Name"
		sSQL &= "					, 1 AS Sort"
		sSQL &= "	FROM tbl_Similarity_Calculation_Results SCR"
		sSQL &= "	UNION"
		sSQL &= "	SELECT"
		sSQL &= "					0 AS id_Similarity_Calculation_Results"
		sSQL &= "					, 'None' AS Name"
		sSQL &= "					, 0 AS Sort"
		sSQL &= "	ORDER BY Sort, SCR.Name"

		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

	Public Shared Sub Fill_tbl_Rombase_DOSBox_Template_Configs(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DS_ML.tbl_DOSBox_ConfigsDataTable, Optional ByVal id_Rombase_DOSBox_Configs As Long = 0)
		If dt Is Nothing Then
			dt = New DS_ML.tbl_DOSBox_ConfigsDataTable
		End If

		dt.Clear()

		Dim sSQL As String = ""
		sSQL &= "	SELECT"
		sSQL &= "		RBDBC.[id_Rombase_DOSBox_Configs] AS [id_DOSBox_Configs]"
		sSQL &= "		, 1 AS [isTemplate]"
		sSQL &= "		, RBDBC.[id_Rombase_DOSBox_Configs] AS [id_Rombase_DOSBox_Configs]"
		sSQL &= "		, RBDBC.[Displayname] AS [Displayname]"
		sSQL &= "		, RBDBC.[sdl-fullscreen] AS [sdl-fullscreen]"
		sSQL &= "		, RBDBC.[sdl-fulldouble] AS [sdl-fulldouble]"
		sSQL &= "		, RBDBC.[sdl-fullresolution] AS [sdl-fullresolution]"
		sSQL &= "		, RBDBC.[sdl-windowresolution] AS [sdl-windowresolution]"
		sSQL &= "		, RBDBC.[sdl-output] AS [sdl-output]"
		sSQL &= "		, RBDBC.[sdl-autolock] AS [sdl-autolock]"
		sSQL &= "		, RBDBC.[sdl-sensitivity] AS [sdl-sensitivity]"
		sSQL &= "		, RBDBC.[sdl-waitonerror] AS [sdl-waitonerror]"
		sSQL &= "		, RBDBC.[sdl-priority_1] AS [sdl-priority_1]"
		sSQL &= "		, RBDBC.[sdl-priority_2] AS [sdl-priority_2]"
		sSQL &= "		, RBDBC.[sdl-mapperfile] AS [sdl-mapperfile]"
		sSQL &= "		, RBDBC.[sdl-usescancodes] AS [sdl-usescancodes]"
		sSQL &= "		, RBDBC.[dosbox-language] AS [dosbox-language]"
		sSQL &= "		, RBDBC.[dosbox-machine] AS [dosbox-machine]"
		sSQL &= "		, RBDBC.[dosbox-memsize] AS [dosbox-memsize]"
		sSQL &= "		, RBDBC.[render-frameskip] AS [render-frameskip]"
		sSQL &= "		, RBDBC.[render-aspect] AS [render-aspect]"
		sSQL &= "		, RBDBC.[render-scaler] AS [render-scaler]"
		sSQL &= "		, RBDBC.[render-scaler_forced] AS [render-scaler_forced]"
		sSQL &= "		, RBDBC.[cpu-core] AS [cpu-core]"
		sSQL &= "		, RBDBC.[cpu-cputype] AS [cpu-cputype]"
		sSQL &= "		, RBDBC.[cpu-cycles] AS [cpu-cycles]"
		sSQL &= "		, RBDBC.[cpu-cycleup] AS [cpu-cycleup]"
		sSQL &= "		, RBDBC.[cpu-cycledown] AS [cpu-cycledown]"
		sSQL &= "		, RBDBC.[mixer-nosound] AS [mixer-nosound]"
		sSQL &= "		, RBDBC.[mixer-rate] AS [mixer-rate]"
		sSQL &= "		, RBDBC.[mixer-blocksize] AS [mixer-blocksize]"
		sSQL &= "		, RBDBC.[mixer-prebuffer] AS [mixer-prebuffer]"
		sSQL &= "		, RBDBC.[midi-mpu401] AS [midi-mpu401]"
		sSQL &= "		, RBDBC.[midi-mididevice] AS [midi-mididevice]"
		sSQL &= "		, RBDBC.[midi-midiconfig] AS [midi-midiconfig]"
		sSQL &= "		, RBDBC.[sblaster-sbtype] AS [sblaster-sbtype]"
		sSQL &= "		, RBDBC.[sblaster-sbbase] AS [sblaster-sbbase]"
		sSQL &= "		, RBDBC.[sblaster-irq] AS [sblaster-irq]"
		sSQL &= "		, RBDBC.[sblaster-dma] AS [sblaster-dma]"
		sSQL &= "		, RBDBC.[sblaster-hdma] AS [sblaster-hdma]"
		sSQL &= "		, RBDBC.[sblaster-sbmixer] AS [sblaster-sbmixer]"
		sSQL &= "		, RBDBC.[sblaster-oplmode] AS [sblaster-oplmode]"
		sSQL &= "		, RBDBC.[sblaster-oplemu] AS [sblaster-oplemu]"
		sSQL &= "		, RBDBC.[sblaster-oplrate] AS [sblaster-oplrate]"
		sSQL &= "		, RBDBC.[gus-gus] AS [gus-gus]"
		sSQL &= "		, RBDBC.[gus-gusrate] AS [gus-gusrate]"
		sSQL &= "		, RBDBC.[gus-gusbase] AS [gus-gusbase]"
		sSQL &= "		, RBDBC.[gus-gusirq] AS [gus-gusirq]"
		sSQL &= "		, RBDBC.[gus-gusdma] AS [gus-gusdma]"
		sSQL &= "		, RBDBC.[gus-ultradir] AS [gus-ultradir]"
		sSQL &= "		, RBDBC.[speaker-pcspeaker] AS [speaker-pcspeaker]"
		sSQL &= "		, RBDBC.[speaker-pcrate] AS [speaker-pcrate]"
		sSQL &= "		, RBDBC.[speaker-tandy] AS [speaker-tandy]"
		sSQL &= "		, RBDBC.[speaker-tandyrate] AS [speaker-tandyrate]"
		sSQL &= "		, RBDBC.[speaker-disney] AS [speaker-disney]"
		sSQL &= "		, RBDBC.[joystick-joysticktype] AS [joystick-joysticktype]"
		sSQL &= "		, RBDBC.[joystick-timed] AS [joystick-timed]"
		sSQL &= "		, RBDBC.[joystick-autofire] AS [joystick-autofire]"
		sSQL &= "		, RBDBC.[joystick-swap34] AS [joystick-swap34]"
		sSQL &= "		, RBDBC.[joystick-buttonwrap] AS [joystick-buttonwrap]"
		sSQL &= "		, RBDBC.[serial-serial1] AS [serial-serial1]"
		sSQL &= "		, RBDBC.[serial-serial2] AS [serial-serial2]"
		sSQL &= "		, RBDBC.[serial-serial3] AS [serial-serial3]"
		sSQL &= "		, RBDBC.[serial-serial4] AS [serial-serial4]"
		sSQL &= "		, RBDBC.[dos-xms] AS [dos-xms]"
		sSQL &= "		, RBDBC.[dos-ems] AS [dos-ems]"
		sSQL &= "		, RBDBC.[dos-umb] AS [dos-umb]"
		sSQL &= "		, RBDBC.[dos-keyboardlayout] AS [dos-keyboardlayout]"
		sSQL &= "		, RBDBC.[ipx-ipx] AS [ipx-ipx]"
		sSQL &= "		, RBDBC.[autoexec-before] AS [autoexec-before]"
		sSQL &= "		, RBDBC.[autoexec-after] AS [autoexec-after]"
		sSQL &= "		, RBDBC.[ml-autoclose] AS [ml-autoclose]"
		sSQL &= "		, RBDBC.[ml-showconsole] AS [ml-showconsole]"
		sSQL &= "		, RBDBC.[ml-customsettings] AS [ml-customsettings]"
		sSQL &= "		, RBDBC.[ml-useloadfix] AS [ml-useloadfix]"
		sSQL &= "		, RBDBC.[ml-loadfix] AS [ml-loadfix]"
		sSQL &= "		, RBDBC.[ml-volume_master_left] AS [ml-volume_master_left]"
		sSQL &= "		, RBDBC.[ml-volume_master_right] AS [ml-volume_master_right]"
		sSQL &= "		, RBDBC.[ml-volume_spkr_left] AS [ml-volume_spkr_left]"
		sSQL &= "		, RBDBC.[ml-volume_spkr_right] AS [ml-volume_spkr_right]"
		sSQL &= "		, RBDBC.[ml-volume_sb_left] AS [ml-volume_sb_left]"
		sSQL &= "		, RBDBC.[ml-volume_sb_right] AS [ml-volume_sb_right]"
		sSQL &= "		, RBDBC.[ml-volume_disney_left] AS [ml-volume_disney_left]"
		sSQL &= "		, RBDBC.[ml-volume_disney_right] AS [ml-volume_disney_right]"
		sSQL &= "		, RBDBC.[ml-volume_gus_left] AS [ml-volume_gus_left]"
		sSQL &= "		, RBDBC.[ml-volume_gus_right] AS [ml-volume_gus_right]"
		sSQL &= "		, RBDBC.[ml-volume_fm_left] AS [ml-volume_fm_left]"
		sSQL &= "		, RBDBC.[ml-volume_fm_right] AS [ml-volume_fm_right]"
		sSQL &= "		, RBDBC.[ml-volume_cdaudio_left] AS [ml-volume_cdaudio_left]"
		sSQL &= "		, RBDBC.[ml-volume_cdaudio_right] AS [ml-volume_cdaudio_right]"
		sSQL &= "		, RBDBC.[p_sdl_pixelshader] AS [p_sdl_pixelshader]"
		sSQL &= "		, RBDBC.[p_sdl_pixelshader_forced] AS [p_sdl_pixelshader_forced]"
		sSQL &= "		, RBDBC.[p_sdl_output] AS [p_sdl_output]"
		sSQL &= "		, RBDBC.[p_sdl_surfacenp-sharpness] AS [p_sdl_surfacenp-sharpness]"
		sSQL &= "		, RBDBC.[p_sdl_surface-collapse-dbl] AS [p_sdl_surface-collapse-dbl]"
		sSQL &= "		, RBDBC.[p_dosbox_vmemsize] AS [p_dosbox_vmemsize]"
		sSQL &= "		, RBDBC.[p_dosbox_memsizekb] AS [p_dosbox_memsizekb]"
		sSQL &= "		, RBDBC.[p_dosbox_forcerate] AS [p_dosbox_forcerate]"
		sSQL &= "		, RBDBC.[p_dosbox_pit_hack] AS [p_dosbox_pit_hack]"
		sSQL &= "		, RBDBC.[p_render_scaler] AS [p_render_scaler]"
		sSQL &= "		, RBDBC.[p_render_autofit] AS [p_render_autofit]"
		sSQL &= "		, RBDBC.[p_vsync_vsyncmode] AS [p_vsync_vsyncmode]"
		sSQL &= "		, RBDBC.[p_vsync_vsyncrate] AS [p_vsync_vsyncrate]"
		sSQL &= "		, RBDBC.[p_cpu_cputype] AS [p_cpu_cputype]"
		sSQL &= "		, RBDBC.[p_keyboard_aux] AS [p_keyboard_aux]"
		sSQL &= "		, RBDBC.[p_keyboard_auxdevice] AS [p_keyboard_auxdevice]"
		sSQL &= "		, RBDBC.[p_voodoo] AS [p_voodoo]"
		sSQL &= "		, RBDBC.[p_voodoo_voodoomem] AS [p_voodoo_voodoomem]"
		sSQL &= "		, RBDBC.[p_mixer_swapstereo] AS [p_mixer_swapstereo]"
		sSQL &= "		, RBDBC.[p_midi_mididevice] AS [p_midi_mididevice]"
		sSQL &= "		, RBDBC.[p_midi_mt32_reverse_stereo] AS [p_midi_mt32_reverse_stereo]"
		sSQL &= "		, RBDBC.[p_midi_mt32_verbose] AS [p_midi_mt32_verbose]"
		sSQL &= "		, RBDBC.[p_midi_mt32_thread] AS [p_midi_mt32_thread]"
		sSQL &= "		, RBDBC.[p_midi_mt32_dac] AS [p_midi_mt32_dac]"
		sSQL &= "		, RBDBC.[p_midi_mt32_reverb_mode] AS [p_midi_mt32_reverb_mode]"
		sSQL &= "		, RBDBC.[p_midi_mt32_reverb_time] AS [p_midi_mt32_reverb_time]"
		sSQL &= "		, RBDBC.[p_midi_mt32_reverb_level] AS [p_midi_mt32_reverb_level]"
		sSQL &= "		, RBDBC.[p_midi_mt32_partials] AS [p_midi_mt32_partials]"
		sSQL &= "		, RBDBC.[p_midi_mt32_romdir] AS [p_midi_mt32_romdir]"
		sSQL &= "		, RBDBC.[p_midi_mt32_chunk] AS [p_midi_mt32_chunk]"
		sSQL &= "		, RBDBC.[p_midi_mt32_prebuffer] AS [p_midi_mt32_prebuffer]"
		sSQL &= "		, RBDBC.[p_midi_mt32_analog] AS [p_midi_mt32_analog]"
		sSQL &= "		, RBDBC.[p_midi_mt32_rate] AS [p_midi_mt32_rate]"
		sSQL &= "		, RBDBC.[p_midi_mt32_src_quality] AS [p_midi_mt32_src_quality]"
		sSQL &= "		, RBDBC.[p_midi_mt32_niceampramp] AS [p_midi_mt32_niceampramp]"
		sSQL &= "		, RBDBC.[p_midi_fluid_soundfont] AS [p_midi_fluid_soundfont]"
		sSQL &= "		, RBDBC.[p_midi_fluid_samplerate] AS [p_midi_fluid_samplerate]"
		sSQL &= "		, RBDBC.[p_midi_fluid_gain] AS [p_midi_fluid_gain]"
		sSQL &= "		, RBDBC.[p_midi_fluid_polyphony] AS [p_midi_fluid_polyphony]"
		sSQL &= "		, RBDBC.[p_midi_fluid_cores] AS [p_midi_fluid_cores]"
		sSQL &= "		, RBDBC.[p_midi_fluid_periods] AS [p_midi_fluid_periods]"
		sSQL &= "		, RBDBC.[p_midi_fluid_periodsize] AS [p_midi_fluid_periodsize]"
		sSQL &= "		, RBDBC.[p_midi_fluid_reverb] AS [p_midi_fluid_reverb]"
		sSQL &= "		, RBDBC.[p_midi_fluid_chorus] AS [p_midi_fluid_chorus]"
		sSQL &= "		, RBDBC.[p_midi_fluid_reverb_roomsize] AS [p_midi_fluid_reverb_roomsize]"
		sSQL &= "		, RBDBC.[p_midi_fluid_reverb_damping] AS [p_midi_fluid_reverb_damping]"
		sSQL &= "		, RBDBC.[p_midi_fluid_reverb_width] AS [p_midi_fluid_reverb_width]"
		sSQL &= "		, RBDBC.[p_midi_fluid_reverb_level] AS [p_midi_fluid_reverb_level]"
		sSQL &= "		, RBDBC.[p_midi_fluid_chorus_number] AS [p_midi_fluid_chorus_number]"
		sSQL &= "		, RBDBC.[p_midi_fluid_chorus_level] AS [p_midi_fluid_chorus_level]"
		sSQL &= "		, RBDBC.[p_midi_fluid_chorus_speed] AS [p_midi_fluid_chorus_speed]"
		sSQL &= "		, RBDBC.[p_midi_fluid_chorus_depth] AS [p_midi_fluid_chorus_depth]"
		sSQL &= "		, RBDBC.[p_midi_fluid_chorus_type] AS [p_midi_fluid_chorus_type]"
		sSQL &= "		, RBDBC.[p_sblaster_oplmode] AS [p_sblaster_oplmode]"
		sSQL &= "		, RBDBC.[p_sblaster_oplemu] AS [p_sblaster_oplemu]"
		sSQL &= "		, RBDBC.[p_sblaster_hardwarebase] AS [p_sblaster_hardwarebase]"
		sSQL &= "		, RBDBC.[p_sblaster_goldplay] AS [p_sblaster_goldplay]"
		sSQL &= "		, RBDBC.[p_innova_innova] AS [p_innova_innova]"
		sSQL &= "		, RBDBC.[p_innova_samplerate] AS [p_innova_samplerate]"
		sSQL &= "		, RBDBC.[p_innova_sidbase] AS [p_innova_sidbase]"
		sSQL &= "		, RBDBC.[p_innova_quality] AS [p_innova_quality]"
		sSQL &= "		, RBDBC.[p_speaker_ps1audio] AS [p_speaker_ps1audio]"
		sSQL &= "		, RBDBC.[p_speaker_ps1audiorate] AS [p_speaker_ps1audiorate]"
		sSQL &= "		, RBDBC.[p_printer_printer] AS [p_printer_printer]"
		sSQL &= "		, RBDBC.[p_printer_dpi] AS [p_printer_dpi]"
		sSQL &= "		, RBDBC.[p_printer_width] AS [p_printer_width]"
		sSQL &= "		, RBDBC.[p_printer_height] AS [p_printer_height]"
		sSQL &= "		, RBDBC.[p_printer_printoutput] AS [p_printer_printoutput]"
		sSQL &= "		, RBDBC.[p_printer_multipage] AS [p_printer_multipage]"
		sSQL &= "		, RBDBC.[p_printer_docpath] AS [p_printer_docpath]"
		sSQL &= "		, RBDBC.[p_printer_timeout] AS [p_printer_timeout]"
		sSQL &= "		, RBDBC.[p_parallel_parallel1] AS [p_parallel_parallel1]"
		sSQL &= "		, RBDBC.[p_parallel_parallel2] AS [p_parallel_parallel2]"
		sSQL &= "		, RBDBC.[p_parallel_parallel3] AS [p_parallel_parallel3]"
		sSQL &= "		, RBDBC.[p_parallel_dongle] AS [p_parallel_dongle]"
		sSQL &= "		, RBDBC.[p_glide_glide] AS [p_glide_glide]"
		sSQL &= "		, RBDBC.[p_glide_lfb] AS [p_glide_lfb]"
		sSQL &= "		, RBDBC.[p_glide_splash] AS [p_glide_splash]"
		sSQL &= "		, RBDBC.[p_ne2000_ne2000] AS [p_ne2000_ne2000]"
		sSQL &= "		, RBDBC.[p_ne2000_nicbase] AS [p_ne2000_nicbase]"
		sSQL &= "		, RBDBC.[p_ne2000_nicirq] AS [p_ne2000_nicirq]"
		sSQL &= "		, RBDBC.[p_ne2000_macaddr] AS [p_ne2000_macaddr]"
		sSQL &= "		, RBDBC.[p_ne2000_realnic] AS [p_ne2000_realnic]"
		sSQL &= "		, RBDBC.[p_ide1_enable] AS [p_ide1_enable]"
		sSQL &= "		, RBDBC.[p_ide1_int13fakeio] AS [p_ide1_int13fakeio]"
		sSQL &= "		, RBDBC.[p_ide1_int13fakev86io] AS [p_ide1_int13fakev86io]"
		sSQL &= "		, RBDBC.[p_ide2_enable] AS [p_ide2_enable]"
		sSQL &= "		, RBDBC.[p_ide2_int13fakeio] AS [p_ide2_int13fakeio]"
		sSQL &= "		, RBDBC.[p_ide2_int13fakev86io] AS [p_ide2_int13fakev86io]"
		sSQL &= "		, RBDBC.[p_ide3_enable] AS [p_ide3_enable]"
		sSQL &= "		, RBDBC.[p_ide3_int13fakeio] AS [p_ide3_int13fakeio]"
		sSQL &= "		, RBDBC.[p_ide3_int13fakev86io] AS [p_ide3_int13fakev86io]"
		sSQL &= "		, RBDBC.[p_ide4_enable] AS [p_ide4_enable]"
		sSQL &= "		, RBDBC.[p_ide4_int13fakeio] AS [p_ide4_int13fakeio]"
		sSQL &= "		, RBDBC.[p_ide4_int13fakev86io] AS [p_ide4_int13fakev86io]"
		sSQL &= "	FROM rombase.tbl_Rombase_DOSBox_Configs RBDBC"
		If id_Rombase_DOSBox_Configs <> 0 Then sSQL &= "	WHERE RBDBC.id_Rombase_DOSBox_Configs = " & TC.getSQLFormat(id_Rombase_DOSBox_Configs)
		sSQL &= "	ORDER BY [Displayname]"

		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

	Public Shared Sub Fill_tbl_Rombase_ScummVM_Template_Configs(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DS_ML.tbl_ScummVM_ConfigsDataTable, Optional ByVal id_Rombase_ScummVM_Configs As Long = 0)
		If dt Is Nothing Then
			dt = New DS_ML.tbl_ScummVM_ConfigsDataTable
		End If

		dt.Clear()

		Dim sSQL As String = ""
		sSQL &= "	SELECT"
		sSQL &= "		RBSVMC.[id_Rombase_ScummVM_Configs] AS [id_ScummVM_Configs]"
		sSQL &= "		, 1 AS [isTemplate]"
		sSQL &= "		, RBSVMC.[id_Rombase_ScummVM_Configs] AS [id_Rombase_ScummVM_Configs]"
		sSQL &= "		, RBSVMC.[Displayname] AS [Displayname]"
		sSQL &= "		, RBSVMC.[user_defined_config] AS [user_defined_config]"
		sSQL &= "		, RBSVMC.[user_defined_commandline] AS [user_defined_commandline]"
		sSQL &= "		, RBSVMC.[console] AS [console]"
		sSQL &= "		, RBSVMC.[confirm_exit] AS [confirm_exit]"
		sSQL &= "		, RBSVMC.[savepath] AS [savepath]"
		sSQL &= "		, RBSVMC.[extrapath] AS [extrapath]"
		sSQL &= "		, RBSVMC.[language] AS [language]"
		sSQL &= "		, RBSVMC.[joystick_num] AS [joystick_num]"
		sSQL &= "		, RBSVMC.[copy_protection] AS [copy_protection]"
		sSQL &= "		, RBSVMC.[boot_param] AS [boot_param]"
		sSQL &= "		, RBSVMC.[fullscreen] AS [fullscreen]"
		sSQL &= "		, RBSVMC.[aspect_ratio] AS [aspect_ratio]"
		sSQL &= "		, RBSVMC.[gfx_mode] AS [gfx_mode]"
		sSQL &= "		, RBSVMC.[subtitles] AS [subtitles]"
		sSQL &= "		, RBSVMC.[music_driver] AS [music_driver]"
		sSQL &= "		, RBSVMC.[multi_midi] AS [multi_midi]"
		sSQL &= "		, RBSVMC.[soundfont] AS [soundfont]"
		sSQL &= "		, RBSVMC.[native_mt32] AS [native_mt32]"
		sSQL &= "		, RBSVMC.[enable_gs] AS [enable_gs]"
		sSQL &= "		, RBSVMC.[output_rate] AS [output_rate]"
		sSQL &= "		, RBSVMC.[music_volume] AS [music_volume]"
		sSQL &= "		, RBSVMC.[speech_volume] AS [speech_volume]"
		sSQL &= "		, RBSVMC.[sfx_volume] AS [sfx_volume]"
		sSQL &= "		, RBSVMC.[speech_mute] AS [speech_mute]"
		sSQL &= "		, RBSVMC.[talkspeed] AS [talkspeed]"
		sSQL &= "		, RBSVMC.[cdrom] AS [cdrom]"
		sSQL &= "		, RBSVMC.[tempo] AS [tempo]"
		sSQL &= "		, RBSVMC.[midi_gain] AS [midi_gain]"
		sSQL &= "		, RBSVMC.[autosave_period] AS [autosave_period]"
		sSQL &= "		, RBSVMC.[save_slot] AS [save_slot]"
		sSQL &= "		, RBSVMC.[demo_mode] AS [demo_mode]"
		sSQL &= "		, RBSVMC.[alt_intro] AS [alt_intro]"
		sSQL &= "		, RBSVMC.[music_mute] AS [music_mute]"
		sSQL &= "		, RBSVMC.[sfx_mute] AS [sfx_mute]"
		sSQL &= "		, RBSVMC.[gfx_details] AS [gfx_details]"
		sSQL &= "		, RBSVMC.[object_labels] AS [object_labels]"
		sSQL &= "		, RBSVMC.[reverse_stereo] AS [reverse_stereo]"
		sSQL &= "		, RBSVMC.[walkspeed] AS [walkspeed]"
		sSQL &= "		, RBSVMC.[filtering] AS [filtering]"
		sSQL &= "		, RBSVMC.[opl_driver] AS [opl_driver]"
		sSQL &= "		, RBSVMC.[originalsaveload] AS [originalsaveload]"
		sSQL &= "		, RBSVMC.[altamigapalette] AS [altamigapalette]"
		sSQL &= "		, RBSVMC.[mousesupport] AS [mousesupport]"
		sSQL &= "		, RBSVMC.[disable_dithering] AS [disable_dithering]"
		sSQL &= "		, RBSVMC.[prefer_digitalsfx] AS [prefer_digitalsfx]"
		sSQL &= "		, RBSVMC.[native_fb01] AS [native_fb01]"
		sSQL &= "		, RBSVMC.[use_cdaudio] AS [use_cdaudio]"
		sSQL &= "		, RBSVMC.[windows_cursors] AS [windows_cursors]"
		sSQL &= "		, RBSVMC.[silver_cursors] AS [silver_cursors]"
		sSQL &= "		, RBSVMC.[enable_gore] AS [enable_gore]"
		sSQL &= "		, RBSVMC.[smooth_scrolling] AS [smooth_scrolling]"
		sSQL &= "		, RBSVMC.[floating_cursors] AS [floating_cursors]"
		sSQL &= "		, RBSVMC.[enable_color_blind] AS [enable_color_blind]"
		sSQL &= "		, RBSVMC.[studio_audience] AS [studio_audience]"
		sSQL &= "		, RBSVMC.[skip_support] AS [skip_support]"
		sSQL &= "		, RBSVMC.[helium_mode] AS [helium_mode]"
		sSQL &= "		, RBSVMC.[skiphallofrecordsscenes] AS [skiphallofrecordsscenes]"
		sSQL &= "		, RBSVMC.[scalemakingofvideos] AS [scalemakingofvideos]"
		sSQL &= "		, RBSVMC.[fast_movie_speed] AS [fast_movie_speed]"
		sSQL &= "		, RBSVMC.[doublefps] AS [doublefps]"
		sSQL &= "		, RBSVMC.[venusenabled] AS [venusenabled]"
		sSQL &= "		, RBSVMC.[noanimwhileturning] AS [noanimwhileturning]"
		sSQL &= "		, RBSVMC.[mpegmovies] AS [mpegmovies]"
		sSQL &= "	FROM rombase.tbl_Rombase_ScummVM_Configs RBSVMC"
		If id_Rombase_ScummVM_Configs <> 0 Then sSQL &= "	WHERE RBSVMC.id_Rombase_ScummVM_Configs = " & TC.getSQLFormat(id_Rombase_ScummVM_Configs)
		sSQL &= "	ORDER BY [Displayname]"

		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub


	Public Shared Sub Fill_tbl_DOSBox_Template_Configs(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DS_ML.tbl_DOSBox_ConfigsDataTable, Optional ByVal id_DOSBox_Configs As Long = 0)
		If dt Is Nothing Then
			dt = New DS_ML.tbl_DOSBox_ConfigsDataTable
		End If

		dt.Clear()

		Dim sSQL As String = ""
		sSQL &= "	SELECT"
		sSQL &= "		DBC.[id_DOSBox_Configs] AS [id_DOSBox_Configs]"
		sSQL &= "		, DBC.[isTemplate] AS [isTemplate]"
		sSQL &= "		, IFNULL(DBC.[id_Rombase_DOSBox_Configs], RBDBC.[id_Rombase_DOSBox_Configs]) AS [id_Rombase_DOSBox_Configs]"
		sSQL &= "		, IFNULL(DBC.[Displayname], RBDBC.[Displayname]) AS [Displayname]"
		sSQL &= "		, IFNULL(DBC.[sdl-fullscreen], RBDBC.[sdl-fullscreen]) AS [sdl-fullscreen]"
		sSQL &= "		, IFNULL(DBC.[sdl-fulldouble], RBDBC.[sdl-fulldouble]) AS [sdl-fulldouble]"
		sSQL &= "		, IFNULL(DBC.[sdl-fullresolution], RBDBC.[sdl-fullresolution]) AS [sdl-fullresolution]"
		sSQL &= "		, IFNULL(DBC.[sdl-windowresolution], RBDBC.[sdl-windowresolution]) AS [sdl-windowresolution]"
		sSQL &= "		, IFNULL(DBC.[sdl-output], RBDBC.[sdl-output]) AS [sdl-output]"
		sSQL &= "		, IFNULL(DBC.[sdl-autolock], RBDBC.[sdl-autolock]) AS [sdl-autolock]"
		sSQL &= "		, IFNULL(DBC.[sdl-sensitivity], RBDBC.[sdl-sensitivity]) AS [sdl-sensitivity]"
		sSQL &= "		, IFNULL(DBC.[sdl-waitonerror], RBDBC.[sdl-waitonerror]) AS [sdl-waitonerror]"
		sSQL &= "		, IFNULL(DBC.[sdl-priority_1], RBDBC.[sdl-priority_1]) AS [sdl-priority_1]"
		sSQL &= "		, IFNULL(DBC.[sdl-priority_2], RBDBC.[sdl-priority_2]) AS [sdl-priority_2]"
		sSQL &= "		, IFNULL(DBC.[sdl-mapperfile], RBDBC.[sdl-mapperfile]) AS [sdl-mapperfile]"
		sSQL &= "		, IFNULL(DBC.[sdl-usescancodes], RBDBC.[sdl-usescancodes]) AS [sdl-usescancodes]"
		sSQL &= "		, IFNULL(DBC.[dosbox-language], RBDBC.[dosbox-language]) AS [dosbox-language]"
		sSQL &= "		, IFNULL(DBC.[dosbox-machine], RBDBC.[dosbox-machine]) AS [dosbox-machine]"
		sSQL &= "		, IFNULL(DBC.[dosbox-memsize], RBDBC.[dosbox-memsize]) AS [dosbox-memsize]"
		sSQL &= "		, IFNULL(DBC.[render-frameskip], RBDBC.[render-frameskip]) AS [render-frameskip]"
		sSQL &= "		, IFNULL(DBC.[render-aspect], RBDBC.[render-aspect]) AS [render-aspect]"
		sSQL &= "		, IFNULL(DBC.[render-scaler], RBDBC.[render-scaler]) AS [render-scaler]"
		sSQL &= "		, IFNULL(DBC.[render-scaler_forced], RBDBC.[render-scaler_forced]) AS [render-scaler_forced]"
		sSQL &= "		, IFNULL(DBC.[cpu-core], RBDBC.[cpu-core]) AS [cpu-core]"
		sSQL &= "		, IFNULL(DBC.[cpu-cputype], RBDBC.[cpu-cputype]) AS [cpu-cputype]"
		sSQL &= "		, IFNULL(DBC.[cpu-cycles], RBDBC.[cpu-cycles]) AS [cpu-cycles]"
		sSQL &= "		, IFNULL(DBC.[cpu-cycleup], RBDBC.[cpu-cycleup]) AS [cpu-cycleup]"
		sSQL &= "		, IFNULL(DBC.[cpu-cycledown], RBDBC.[cpu-cycledown]) AS [cpu-cycledown]"
		sSQL &= "		, IFNULL(DBC.[mixer-nosound], RBDBC.[mixer-nosound]) AS [mixer-nosound]"
		sSQL &= "		, IFNULL(DBC.[mixer-rate], RBDBC.[mixer-rate]) AS [mixer-rate]"
		sSQL &= "		, IFNULL(DBC.[mixer-blocksize], RBDBC.[mixer-blocksize]) AS [mixer-blocksize]"
		sSQL &= "		, IFNULL(DBC.[mixer-prebuffer], RBDBC.[mixer-prebuffer]) AS [mixer-prebuffer]"
		sSQL &= "		, IFNULL(DBC.[midi-mpu401], RBDBC.[midi-mpu401]) AS [midi-mpu401]"
		sSQL &= "		, IFNULL(DBC.[midi-mididevice], RBDBC.[midi-mididevice]) AS [midi-mididevice]"
		sSQL &= "		, IFNULL(DBC.[midi-midiconfig], RBDBC.[midi-midiconfig]) AS [midi-midiconfig]"
		sSQL &= "		, IFNULL(DBC.[sblaster-sbtype], RBDBC.[sblaster-sbtype]) AS [sblaster-sbtype]"
		sSQL &= "		, IFNULL(DBC.[sblaster-sbbase], RBDBC.[sblaster-sbbase]) AS [sblaster-sbbase]"
		sSQL &= "		, IFNULL(DBC.[sblaster-irq], RBDBC.[sblaster-irq]) AS [sblaster-irq]"
		sSQL &= "		, IFNULL(DBC.[sblaster-dma], RBDBC.[sblaster-dma]) AS [sblaster-dma]"
		sSQL &= "		, IFNULL(DBC.[sblaster-hdma], RBDBC.[sblaster-hdma]) AS [sblaster-hdma]"
		sSQL &= "		, IFNULL(DBC.[sblaster-sbmixer], RBDBC.[sblaster-sbmixer]) AS [sblaster-sbmixer]"
		sSQL &= "		, IFNULL(DBC.[sblaster-oplmode], RBDBC.[sblaster-oplmode]) AS [sblaster-oplmode]"
		sSQL &= "		, IFNULL(DBC.[sblaster-oplemu], RBDBC.[sblaster-oplemu]) AS [sblaster-oplemu]"
		sSQL &= "		, IFNULL(DBC.[sblaster-oplrate], RBDBC.[sblaster-oplrate]) AS [sblaster-oplrate]"
		sSQL &= "		, IFNULL(DBC.[gus-gus], RBDBC.[gus-gus]) AS [gus-gus]"
		sSQL &= "		, IFNULL(DBC.[gus-gusrate], RBDBC.[gus-gusrate]) AS [gus-gusrate]"
		sSQL &= "		, IFNULL(DBC.[gus-gusbase], RBDBC.[gus-gusbase]) AS [gus-gusbase]"
		sSQL &= "		, IFNULL(DBC.[gus-gusirq], RBDBC.[gus-gusirq]) AS [gus-gusirq]"
		sSQL &= "		, IFNULL(DBC.[gus-gusdma], RBDBC.[gus-gusdma]) AS [gus-gusdma]"
		sSQL &= "		, IFNULL(DBC.[gus-ultradir], RBDBC.[gus-ultradir]) AS [gus-ultradir]"
		sSQL &= "		, IFNULL(DBC.[speaker-pcspeaker], RBDBC.[speaker-pcspeaker]) AS [speaker-pcspeaker]"
		sSQL &= "		, IFNULL(DBC.[speaker-pcrate], RBDBC.[speaker-pcrate]) AS [speaker-pcrate]"
		sSQL &= "		, IFNULL(DBC.[speaker-tandy], RBDBC.[speaker-tandy]) AS [speaker-tandy]"
		sSQL &= "		, IFNULL(DBC.[speaker-tandyrate], RBDBC.[speaker-tandyrate]) AS [speaker-tandyrate]"
		sSQL &= "		, IFNULL(DBC.[speaker-disney], RBDBC.[speaker-disney]) AS [speaker-disney]"
		sSQL &= "		, IFNULL(DBC.[joystick-joysticktype], RBDBC.[joystick-joysticktype]) AS [joystick-joysticktype]"
		sSQL &= "		, IFNULL(DBC.[joystick-timed], RBDBC.[joystick-timed]) AS [joystick-timed]"
		sSQL &= "		, IFNULL(DBC.[joystick-autofire], RBDBC.[joystick-autofire]) AS [joystick-autofire]"
		sSQL &= "		, IFNULL(DBC.[joystick-swap34], RBDBC.[joystick-swap34]) AS [joystick-swap34]"
		sSQL &= "		, IFNULL(DBC.[joystick-buttonwrap], RBDBC.[joystick-buttonwrap]) AS [joystick-buttonwrap]"
		sSQL &= "		, IFNULL(DBC.[serial-serial1], RBDBC.[serial-serial1]) AS [serial-serial1]"
		sSQL &= "		, IFNULL(DBC.[serial-serial2], RBDBC.[serial-serial2]) AS [serial-serial2]"
		sSQL &= "		, IFNULL(DBC.[serial-serial3], RBDBC.[serial-serial3]) AS [serial-serial3]"
		sSQL &= "		, IFNULL(DBC.[serial-serial4], RBDBC.[serial-serial4]) AS [serial-serial4]"
		sSQL &= "		, IFNULL(DBC.[dos-xms], RBDBC.[dos-xms]) AS [dos-xms]"
		sSQL &= "		, IFNULL(DBC.[dos-ems], RBDBC.[dos-ems]) AS [dos-ems]"
		sSQL &= "		, IFNULL(DBC.[dos-umb], RBDBC.[dos-umb]) AS [dos-umb]"
		sSQL &= "		, IFNULL(DBC.[dos-keyboardlayout], RBDBC.[dos-keyboardlayout]) AS [dos-keyboardlayout]"
		sSQL &= "		, IFNULL(DBC.[ipx-ipx], RBDBC.[ipx-ipx]) AS [ipx-ipx]"
		sSQL &= "		, IFNULL(DBC.[autoexec-before], RBDBC.[autoexec-before]) AS [autoexec-before]"
		sSQL &= "		, IFNULL(DBC.[autoexec-after], RBDBC.[autoexec-after]) AS [autoexec-after]"
		sSQL &= "		, IFNULL(DBC.[ml-autoclose], RBDBC.[ml-autoclose]) AS [ml-autoclose]"
		sSQL &= "		, IFNULL(DBC.[ml-showconsole], RBDBC.[ml-showconsole]) AS [ml-showconsole]"
		sSQL &= "		, IFNULL(DBC.[ml-customsettings], RBDBC.[ml-customsettings]) AS [ml-customsettings]"
		sSQL &= "		, IFNULL(DBC.[ml-useloadfix], RBDBC.[ml-useloadfix]) AS [ml-useloadfix]"
		sSQL &= "		, IFNULL(DBC.[ml-loadfix], RBDBC.[ml-loadfix]) AS [ml-loadfix]"
		sSQL &= "		, IFNULL(DBC.[ml-volume_master_left], RBDBC.[ml-volume_master_left]) AS [ml-volume_master_left]"
		sSQL &= "		, IFNULL(DBC.[ml-volume_master_right], RBDBC.[ml-volume_master_right]) AS [ml-volume_master_right]"
		sSQL &= "		, IFNULL(DBC.[ml-volume_spkr_left], RBDBC.[ml-volume_spkr_left]) AS [ml-volume_spkr_left]"
		sSQL &= "		, IFNULL(DBC.[ml-volume_spkr_right], RBDBC.[ml-volume_spkr_right]) AS [ml-volume_spkr_right]"
		sSQL &= "		, IFNULL(DBC.[ml-volume_sb_left], RBDBC.[ml-volume_sb_left]) AS [ml-volume_sb_left]"
		sSQL &= "		, IFNULL(DBC.[ml-volume_sb_right], RBDBC.[ml-volume_sb_right]) AS [ml-volume_sb_right]"
		sSQL &= "		, IFNULL(DBC.[ml-volume_disney_left], RBDBC.[ml-volume_disney_left]) AS [ml-volume_disney_left]"
		sSQL &= "		, IFNULL(DBC.[ml-volume_disney_right], RBDBC.[ml-volume_disney_right]) AS [ml-volume_disney_right]"
		sSQL &= "		, IFNULL(DBC.[ml-volume_gus_left], RBDBC.[ml-volume_gus_left]) AS [ml-volume_gus_left]"
		sSQL &= "		, IFNULL(DBC.[ml-volume_gus_right], RBDBC.[ml-volume_gus_right]) AS [ml-volume_gus_right]"
		sSQL &= "		, IFNULL(DBC.[ml-volume_fm_left], RBDBC.[ml-volume_fm_left]) AS [ml-volume_fm_left]"
		sSQL &= "		, IFNULL(DBC.[ml-volume_fm_right], RBDBC.[ml-volume_fm_right]) AS [ml-volume_fm_right]"
		sSQL &= "		, IFNULL(DBC.[ml-volume_cdaudio_left], RBDBC.[ml-volume_cdaudio_left]) AS [ml-volume_cdaudio_left]"
		sSQL &= "		, IFNULL(DBC.[ml-volume_cdaudio_right], RBDBC.[ml-volume_cdaudio_right]) AS [ml-volume_cdaudio_right]"
		sSQL &= "		, IFNULL(DBC.[p_sdl_pixelshader], RBDBC.[p_sdl_pixelshader]) AS [p_sdl_pixelshader]"
		sSQL &= "		, IFNULL(DBC.[p_sdl_pixelshader_forced], RBDBC.[p_sdl_pixelshader_forced]) AS [p_sdl_pixelshader_forced]"
		sSQL &= "		, IFNULL(DBC.[p_sdl_output], RBDBC.[p_sdl_output]) AS [p_sdl_output]"
		sSQL &= "		, IFNULL(DBC.[p_sdl_surfacenp-sharpness], RBDBC.[p_sdl_surfacenp-sharpness]) AS [p_sdl_surfacenp-sharpness]"
		sSQL &= "		, IFNULL(DBC.[p_sdl_surface-collapse-dbl], RBDBC.[p_sdl_surface-collapse-dbl]) AS [p_sdl_surface-collapse-dbl]"
		sSQL &= "		, IFNULL(DBC.[p_dosbox_vmemsize], RBDBC.[p_dosbox_vmemsize]) AS [p_dosbox_vmemsize]"
		sSQL &= "		, IFNULL(DBC.[p_dosbox_memsizekb], RBDBC.[p_dosbox_memsizekb]) AS [p_dosbox_memsizekb]"
		sSQL &= "		, IFNULL(DBC.[p_dosbox_forcerate], RBDBC.[p_dosbox_forcerate]) AS [p_dosbox_forcerate]"
		sSQL &= "		, IFNULL(DBC.[p_dosbox_pit_hack], RBDBC.[p_dosbox_pit_hack]) AS [p_dosbox_pit_hack]"
		sSQL &= "		, IFNULL(DBC.[p_render_scaler], RBDBC.[p_render_scaler]) AS [p_render_scaler]"
		sSQL &= "		, IFNULL(DBC.[p_render_autofit], RBDBC.[p_render_autofit]) AS [p_render_autofit]"
		sSQL &= "		, IFNULL(DBC.[p_vsync_vsyncmode], RBDBC.[p_vsync_vsyncmode]) AS [p_vsync_vsyncmode]"
		sSQL &= "		, IFNULL(DBC.[p_vsync_vsyncrate], RBDBC.[p_vsync_vsyncrate]) AS [p_vsync_vsyncrate]"
		sSQL &= "		, IFNULL(DBC.[p_cpu_cputype], RBDBC.[p_cpu_cputype]) AS [p_cpu_cputype]"
		sSQL &= "		, IFNULL(DBC.[p_keyboard_aux], RBDBC.[p_keyboard_aux]) AS [p_keyboard_aux]"
		sSQL &= "		, IFNULL(DBC.[p_keyboard_auxdevice], RBDBC.[p_keyboard_auxdevice]) AS [p_keyboard_auxdevice]"
		sSQL &= "		, IFNULL(DBC.[p_voodoo], RBDBC.[p_voodoo]) AS [p_voodoo]"
		sSQL &= "		, IFNULL(DBC.[p_voodoo_voodoomem], RBDBC.[p_voodoo_voodoomem]) AS [p_voodoo_voodoomem]"
		sSQL &= "		, IFNULL(DBC.[p_mixer_swapstereo], RBDBC.[p_mixer_swapstereo]) AS [p_mixer_swapstereo]"
		sSQL &= "		, IFNULL(DBC.[p_midi_mididevice], RBDBC.[p_midi_mididevice]) AS [p_midi_mididevice]"
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_reverse_stereo], RBDBC.[p_midi_mt32_reverse_stereo]) AS [p_midi_mt32_reverse_stereo]"
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_verbose], RBDBC.[p_midi_mt32_verbose]) AS [p_midi_mt32_verbose]"
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_thread], RBDBC.[p_midi_mt32_thread]) AS [p_midi_mt32_thread]"
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_dac], RBDBC.[p_midi_mt32_dac]) AS [p_midi_mt32_dac]"
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_reverb_mode], RBDBC.[p_midi_mt32_reverb_mode]) AS [p_midi_mt32_reverb_mode]"
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_reverb_time], RBDBC.[p_midi_mt32_reverb_time]) AS [p_midi_mt32_reverb_time]"
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_reverb_level], RBDBC.[p_midi_mt32_reverb_level]) AS [p_midi_mt32_reverb_level]"
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_partials], RBDBC.[p_midi_mt32_partials]) AS [p_midi_mt32_partials]"
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_romdir], RBDBC.[p_midi_mt32_romdir]) AS [p_midi_mt32_romdir]"
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_chunk], RBDBC.[p_midi_mt32_chunk]) AS [p_midi_mt32_chunk]"
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_prebuffer], RBDBC.[p_midi_mt32_prebuffer]) AS [p_midi_mt32_prebuffer]"
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_analog], RBDBC.[p_midi_mt32_analog]) AS [p_midi_mt32_analog]"
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_rate], RBDBC.[p_midi_mt32_rate]) AS [p_midi_mt32_rate]"
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_src_quality], RBDBC.[p_midi_mt32_src_quality]) AS [p_midi_mt32_src_quality]"
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_niceampramp], RBDBC.[p_midi_mt32_niceampramp]) AS [p_midi_mt32_niceampramp]"
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_soundfont], RBDBC.[p_midi_fluid_soundfont]) AS [p_midi_fluid_soundfont]"
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_samplerate], RBDBC.[p_midi_fluid_samplerate]) AS [p_midi_fluid_samplerate]"
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_gain], RBDBC.[p_midi_fluid_gain]) AS [p_midi_fluid_gain]"
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_polyphony], RBDBC.[p_midi_fluid_polyphony]) AS [p_midi_fluid_polyphony]"
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_cores], RBDBC.[p_midi_fluid_cores]) AS [p_midi_fluid_cores]"
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_periods], RBDBC.[p_midi_fluid_periods]) AS [p_midi_fluid_periods]"
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_periodsize], RBDBC.[p_midi_fluid_periodsize]) AS [p_midi_fluid_periodsize]"
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_reverb], RBDBC.[p_midi_fluid_reverb]) AS [p_midi_fluid_reverb]"
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_chorus], RBDBC.[p_midi_fluid_chorus]) AS [p_midi_fluid_chorus]"
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_reverb_roomsize], RBDBC.[p_midi_fluid_reverb_roomsize]) AS [p_midi_fluid_reverb_roomsize]"
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_reverb_damping], RBDBC.[p_midi_fluid_reverb_damping]) AS [p_midi_fluid_reverb_damping]"
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_reverb_width], RBDBC.[p_midi_fluid_reverb_width]) AS [p_midi_fluid_reverb_width]"
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_reverb_level], RBDBC.[p_midi_fluid_reverb_level]) AS [p_midi_fluid_reverb_level]"
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_chorus_number], RBDBC.[p_midi_fluid_chorus_number]) AS [p_midi_fluid_chorus_number]"
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_chorus_level], RBDBC.[p_midi_fluid_chorus_level]) AS [p_midi_fluid_chorus_level]"
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_chorus_speed], RBDBC.[p_midi_fluid_chorus_speed]) AS [p_midi_fluid_chorus_speed]"
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_chorus_depth], RBDBC.[p_midi_fluid_chorus_depth]) AS [p_midi_fluid_chorus_depth]"
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_chorus_type], RBDBC.[p_midi_fluid_chorus_type]) AS [p_midi_fluid_chorus_type]"
		sSQL &= "		, IFNULL(DBC.[p_sblaster_oplmode], RBDBC.[p_sblaster_oplmode]) AS [p_sblaster_oplmode]"
		sSQL &= "		, IFNULL(DBC.[p_sblaster_oplemu], RBDBC.[p_sblaster_oplemu]) AS [p_sblaster_oplemu]"
		sSQL &= "		, IFNULL(DBC.[p_sblaster_hardwarebase], RBDBC.[p_sblaster_hardwarebase]) AS [p_sblaster_hardwarebase]"
		sSQL &= "		, IFNULL(DBC.[p_sblaster_goldplay], RBDBC.[p_sblaster_goldplay]) AS [p_sblaster_goldplay]"
		sSQL &= "		, IFNULL(DBC.[p_innova_innova], RBDBC.[p_innova_innova]) AS [p_innova_innova]"
		sSQL &= "		, IFNULL(DBC.[p_innova_samplerate], RBDBC.[p_innova_samplerate]) AS [p_innova_samplerate]"
		sSQL &= "		, IFNULL(DBC.[p_innova_sidbase], RBDBC.[p_innova_sidbase]) AS [p_innova_sidbase]"
		sSQL &= "		, IFNULL(DBC.[p_innova_quality], RBDBC.[p_innova_quality]) AS [p_innova_quality]"
		sSQL &= "		, IFNULL(DBC.[p_speaker_ps1audio], RBDBC.[p_speaker_ps1audio]) AS [p_speaker_ps1audio]"
		sSQL &= "		, IFNULL(DBC.[p_speaker_ps1audiorate], RBDBC.[p_speaker_ps1audiorate]) AS [p_speaker_ps1audiorate]"
		sSQL &= "		, IFNULL(DBC.[p_printer_printer], RBDBC.[p_printer_printer]) AS [p_printer_printer]"
		sSQL &= "		, IFNULL(DBC.[p_printer_dpi], RBDBC.[p_printer_dpi]) AS [p_printer_dpi]"
		sSQL &= "		, IFNULL(DBC.[p_printer_width], RBDBC.[p_printer_width]) AS [p_printer_width]"
		sSQL &= "		, IFNULL(DBC.[p_printer_height], RBDBC.[p_printer_height]) AS [p_printer_height]"
		sSQL &= "		, IFNULL(DBC.[p_printer_printoutput], RBDBC.[p_printer_printoutput]) AS [p_printer_printoutput]"
		sSQL &= "		, IFNULL(DBC.[p_printer_multipage], RBDBC.[p_printer_multipage]) AS [p_printer_multipage]"
		sSQL &= "		, IFNULL(DBC.[p_printer_docpath], RBDBC.[p_printer_docpath]) AS [p_printer_docpath]"
		sSQL &= "		, IFNULL(DBC.[p_printer_timeout], RBDBC.[p_printer_timeout]) AS [p_printer_timeout]"
		sSQL &= "		, IFNULL(DBC.[p_parallel_parallel1], RBDBC.[p_parallel_parallel1]) AS [p_parallel_parallel1]"
		sSQL &= "		, IFNULL(DBC.[p_parallel_parallel2], RBDBC.[p_parallel_parallel2]) AS [p_parallel_parallel2]"
		sSQL &= "		, IFNULL(DBC.[p_parallel_parallel3], RBDBC.[p_parallel_parallel3]) AS [p_parallel_parallel3]"
		sSQL &= "		, IFNULL(DBC.[p_parallel_dongle], RBDBC.[p_parallel_dongle]) AS [p_parallel_dongle]"
		sSQL &= "		, IFNULL(DBC.[p_glide_glide], RBDBC.[p_glide_glide]) AS [p_glide_glide]"
		sSQL &= "		, IFNULL(DBC.[p_glide_lfb], RBDBC.[p_glide_lfb]) AS [p_glide_lfb]"
		sSQL &= "		, IFNULL(DBC.[p_glide_splash], RBDBC.[p_glide_splash]) AS [p_glide_splash]"
		sSQL &= "		, IFNULL(DBC.[p_ne2000_ne2000], RBDBC.[p_ne2000_ne2000]) AS [p_ne2000_ne2000]"
		sSQL &= "		, IFNULL(DBC.[p_ne2000_nicbase], RBDBC.[p_ne2000_nicbase]) AS [p_ne2000_nicbase]"
		sSQL &= "		, IFNULL(DBC.[p_ne2000_nicirq], RBDBC.[p_ne2000_nicirq]) AS [p_ne2000_nicirq]"
		sSQL &= "		, IFNULL(DBC.[p_ne2000_macaddr], RBDBC.[p_ne2000_macaddr]) AS [p_ne2000_macaddr]"
		sSQL &= "		, IFNULL(DBC.[p_ne2000_realnic], RBDBC.[p_ne2000_realnic]) AS [p_ne2000_realnic]"
		sSQL &= "		, IFNULL(DBC.[p_ide1_enable], RBDBC.[p_ide1_enable]) AS [p_ide1_enable]"
		sSQL &= "		, IFNULL(DBC.[p_ide1_int13fakeio], RBDBC.[p_ide1_int13fakeio]) AS [p_ide1_int13fakeio]"
		sSQL &= "		, IFNULL(DBC.[p_ide1_int13fakev86io], RBDBC.[p_ide1_int13fakev86io]) AS [p_ide1_int13fakev86io]"
		sSQL &= "		, IFNULL(DBC.[p_ide2_enable], RBDBC.[p_ide2_enable]) AS [p_ide2_enable]"
		sSQL &= "		, IFNULL(DBC.[p_ide2_int13fakeio], RBDBC.[p_ide2_int13fakeio]) AS [p_ide2_int13fakeio]"
		sSQL &= "		, IFNULL(DBC.[p_ide2_int13fakev86io], RBDBC.[p_ide2_int13fakev86io]) AS [p_ide2_int13fakev86io]"
		sSQL &= "		, IFNULL(DBC.[p_ide3_enable], RBDBC.[p_ide3_enable]) AS [p_ide3_enable]"
		sSQL &= "		, IFNULL(DBC.[p_ide3_int13fakeio], RBDBC.[p_ide3_int13fakeio]) AS [p_ide3_int13fakeio]"
		sSQL &= "		, IFNULL(DBC.[p_ide3_int13fakev86io], RBDBC.[p_ide3_int13fakev86io]) AS [p_ide3_int13fakev86io]"
		sSQL &= "		, IFNULL(DBC.[p_ide4_enable], RBDBC.[p_ide4_enable]) AS [p_ide4_enable]"
		sSQL &= "		, IFNULL(DBC.[p_ide4_int13fakeio], RBDBC.[p_ide4_int13fakeio]) AS [p_ide4_int13fakeio]"
		sSQL &= "		, IFNULL(DBC.[p_ide4_int13fakev86io], RBDBC.[p_ide4_int13fakev86io]) AS [p_ide4_int13fakev86io]"
		sSQL &= "	FROM main.tbl_DOSBox_Configs DBC"
		sSQL &= "	LEFT JOIN rombase.tbl_Rombase_DOSBox_Configs RBDBC ON DBC.id_Rombase_DOSBox_Configs = RBDBC.id_Rombase_DOSBox_Configs"
		sSQL &= "	WHERE DBC.isTemplate = 1"
		If id_DOSBox_Configs <> 0 Then sSQL &= "	AND DBC.id_DOSBox_Configs = " & TC.getSQLFormat(id_DOSBox_Configs)
		sSQL &= "	ORDER BY [Displayname]"

		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

	Public Shared Sub Fill_tbl_ScummVM_Template_Configs(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DS_ML.tbl_ScummVM_ConfigsDataTable, Optional ByVal id_ScummVM_Configs As Long = 0)
		If dt Is Nothing Then
			dt = New DS_ML.tbl_ScummVM_ConfigsDataTable
		End If

		dt.Clear()

		Dim sSQL As String = ""
		sSQL &= "	SELECT"
		sSQL &= "		SVMC.[id_ScummVM_Configs] AS [id_ScummVM_Configs]"
		sSQL &= "		, SVMC.[isTemplate] AS [isTemplate]"
		sSQL &= "		, IFNULL(SVMC.[id_Rombase_ScummVM_Configs], RBSVMC.[id_Rombase_ScummVM_Configs]) AS [id_Rombase_ScummVM_Configs]"
		sSQL &= "		, IFNULL(SVMC.[Displayname], RBSVMC.[Displayname]) AS [Displayname]"
		sSQL &= "		, IFNULL(SVMC.[user_defined_config], RBSVMC.[user_defined_config]) AS [user_defined_config]"
		sSQL &= "		, IFNULL(SVMC.[user_defined_commandline], RBSVMC.[user_defined_commandline]) AS [user_defined_commandline]"
		sSQL &= "		, IFNULL(SVMC.[console], RBSVMC.[console]) AS [console]"
		sSQL &= "		, IFNULL(SVMC.[confirm_exit], RBSVMC.[confirm_exit]) AS [confirm_exit]"
		sSQL &= "		, IFNULL(SVMC.[savepath], RBSVMC.[savepath]) AS [savepath]"
		sSQL &= "		, IFNULL(SVMC.[extrapath], RBSVMC.[extrapath]) AS [extrapath]"
		sSQL &= "		, IFNULL(SVMC.[language], RBSVMC.[language]) AS [language]"
		sSQL &= "		, IFNULL(SVMC.[joystick_num], RBSVMC.[joystick_num]) AS [joystick_num]"
		sSQL &= "		, IFNULL(SVMC.[copy_protection], RBSVMC.[copy_protection]) AS [copy_protection]"
		sSQL &= "		, IFNULL(SVMC.[boot_param], RBSVMC.[boot_param]) AS [boot_param]"
		sSQL &= "		, IFNULL(SVMC.[fullscreen], RBSVMC.[fullscreen]) AS [fullscreen]"
		sSQL &= "		, IFNULL(SVMC.[aspect_ratio], RBSVMC.[aspect_ratio]) AS [aspect_ratio]"
		sSQL &= "		, IFNULL(SVMC.[gfx_mode], RBSVMC.[gfx_mode]) AS [gfx_mode]"
		sSQL &= "		, IFNULL(SVMC.[subtitles], RBSVMC.[subtitles]) AS [subtitles]"
		sSQL &= "		, IFNULL(SVMC.[music_driver], RBSVMC.[music_driver]) AS [music_driver]"
		sSQL &= "		, IFNULL(SVMC.[multi_midi], RBSVMC.[multi_midi]) AS [multi_midi]"
		sSQL &= "		, IFNULL(SVMC.[soundfont], RBSVMC.[soundfont]) AS [soundfont]"
		sSQL &= "		, IFNULL(SVMC.[native_mt32], RBSVMC.[native_mt32]) AS [native_mt32]"
		sSQL &= "		, IFNULL(SVMC.[enable_gs], RBSVMC.[enable_gs]) AS [enable_gs]"
		sSQL &= "		, IFNULL(SVMC.[output_rate], RBSVMC.[output_rate]) AS [output_rate]"
		sSQL &= "		, IFNULL(SVMC.[music_volume], RBSVMC.[music_volume]) AS [music_volume]"
		sSQL &= "		, IFNULL(SVMC.[speech_volume], RBSVMC.[speech_volume]) AS [speech_volume]"
		sSQL &= "		, IFNULL(SVMC.[sfx_volume], RBSVMC.[sfx_volume]) AS [sfx_volume]"
		sSQL &= "		, IFNULL(SVMC.[speech_mute], RBSVMC.[speech_mute]) AS [speech_mute]"
		sSQL &= "		, IFNULL(SVMC.[talkspeed], RBSVMC.[talkspeed]) AS [talkspeed]"
		sSQL &= "		, IFNULL(SVMC.[cdrom], RBSVMC.[cdrom]) AS [cdrom]"
		sSQL &= "		, IFNULL(SVMC.[tempo], RBSVMC.[tempo]) AS [tempo]"
		sSQL &= "		, IFNULL(SVMC.[midi_gain], RBSVMC.[midi_gain]) AS [midi_gain]"
		sSQL &= "		, IFNULL(SVMC.[autosave_period], RBSVMC.[autosave_period]) AS [autosave_period]"
		sSQL &= "		, IFNULL(SVMC.[save_slot], RBSVMC.[save_slot]) AS [save_slot]"
		sSQL &= "		, IFNULL(SVMC.[demo_mode], RBSVMC.[demo_mode]) AS [demo_mode]"
		sSQL &= "		, IFNULL(SVMC.[alt_intro], RBSVMC.[alt_intro]) AS [alt_intro]"
		sSQL &= "		, IFNULL(SVMC.[music_mute], RBSVMC.[music_mute]) AS [music_mute]"
		sSQL &= "		, IFNULL(SVMC.[sfx_mute], RBSVMC.[sfx_mute]) AS [sfx_mute]"
		sSQL &= "		, IFNULL(SVMC.[gfx_details], RBSVMC.[gfx_details]) AS [gfx_details]"
		sSQL &= "		, IFNULL(SVMC.[object_labels], RBSVMC.[object_labels]) AS [object_labels]"
		sSQL &= "		, IFNULL(SVMC.[reverse_stereo], RBSVMC.[reverse_stereo]) AS [reverse_stereo]"
		sSQL &= "		, IFNULL(SVMC.[walkspeed], RBSVMC.[walkspeed]) AS [walkspeed]"
		sSQL &= "		, IFNULL(SVMC.[filtering], RBSVMC.[filtering]) AS [filtering]"
		sSQL &= "		, IFNULL(SVMC.[opl_driver], RBSVMC.[opl_driver]) AS [opl_driver]"
		sSQL &= "		, IFNULL(SVMC.[originalsaveload], RBSVMC.[originalsaveload]) AS [originalsaveload]"
		sSQL &= "		, IFNULL(SVMC.[altamigapalette], RBSVMC.[altamigapalette]) AS [altamigapalette]"
		sSQL &= "		, IFNULL(SVMC.[mousesupport], RBSVMC.[mousesupport]) AS [mousesupport]"
		sSQL &= "		, IFNULL(SVMC.[disable_dithering], RBSVMC.[disable_dithering]) AS [disable_dithering]"
		sSQL &= "		, IFNULL(SVMC.[prefer_digitalsfx], RBSVMC.[prefer_digitalsfx]) AS [prefer_digitalsfx]"
		sSQL &= "		, IFNULL(SVMC.[native_fb01], RBSVMC.[native_fb01]) AS [native_fb01]"
		sSQL &= "		, IFNULL(SVMC.[use_cdaudio], RBSVMC.[use_cdaudio]) AS [use_cdaudio]"
		sSQL &= "		, IFNULL(SVMC.[windows_cursors], RBSVMC.[windows_cursors]) AS [windows_cursors]"
		sSQL &= "		, IFNULL(SVMC.[silver_cursors], RBSVMC.[silver_cursors]) AS [silver_cursors]"
		sSQL &= "		, IFNULL(SVMC.[enable_gore], RBSVMC.[enable_gore]) AS [enable_gore]"
		sSQL &= "		, IFNULL(SVMC.[smooth_scrolling], RBSVMC.[smooth_scrolling]) AS [smooth_scrolling]"
		sSQL &= "		, IFNULL(SVMC.[floating_cursors], RBSVMC.[floating_cursors]) AS [floating_cursors]"
		sSQL &= "		, IFNULL(SVMC.[enable_color_blind], RBSVMC.[enable_color_blind]) AS [enable_color_blind]"
		sSQL &= "		, IFNULL(SVMC.[studio_audience], RBSVMC.[studio_audience]) AS [studio_audience]"
		sSQL &= "		, IFNULL(SVMC.[skip_support], RBSVMC.[skip_support]) AS [skip_support]"
		sSQL &= "		, IFNULL(SVMC.[helium_mode], RBSVMC.[helium_mode]) AS [helium_mode]"
		sSQL &= "		, IFNULL(SVMC.[skiphallofrecordsscenes], RBSVMC.[skiphallofrecordsscenes]) AS [skiphallofrecordsscenes]"
		sSQL &= "		, IFNULL(SVMC.[scalemakingofvideos], RBSVMC.[scalemakingofvideos]) AS [scalemakingofvideos]"
		sSQL &= "		, IFNULL(SVMC.[fast_movie_speed], RBSVMC.[fast_movie_speed]) AS [fast_movie_speed]"
		sSQL &= "		, IFNULL(SVMC.[doublefps], RBSVMC.[doublefps]) AS [doublefps]"
		sSQL &= "		, IFNULL(SVMC.[venusenabled], RBSVMC.[venusenabled]) AS [venusenabled]"
		sSQL &= "		, IFNULL(SVMC.[noanimwhileturning], RBSVMC.[noanimwhileturning]) AS [noanimwhileturning]"
		sSQL &= "		, IFNULL(SVMC.[mpegmovies], RBSVMC.[mpegmovies]) AS [mpegmovies]"
		sSQL &= "	FROM main.tbl_ScummVM_Configs SVMC"
		sSQL &= "	LEFT JOIN rombase.tbl_Rombase_ScummVM_Configs RBSVMC ON SVMC.id_Rombase_ScummVM_Configs = RBSVMC.id_Rombase_ScummVM_Configs"
		sSQL &= "	WHERE SVMC.isTemplate = 1"
		If id_ScummVM_Configs <> 0 Then sSQL &= "	AND SVMC.id_ScummVM_Configs = " & TC.getSQLFormat(id_ScummVM_Configs)
		sSQL &= "	ORDER BY [Displayname]"

		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

	Public Shared Sub Fill_tbl_DOSBox_Configs(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DS_ML.tbl_DOSBox_ConfigsDataTable, Optional ByVal id_Emu_Games As Long = 0)
		If dt Is Nothing Then dt = New DS_ML.tbl_DOSBox_ConfigsDataTable
		dt.Clear()

		Dim sSQL As String = ""
		sSQL &= "	SELECT" & ControlChars.CrLf
		sSQL &= "		IFNULL(DBC.[id_DOSBox_Configs], DBCT.[id_DOSBox_Configs]) AS [id_DOSBox_Configs]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[id_Rombase_DOSBox_Configs], IFNULL(DBCT.[id_Rombase_DOSBox_Configs], RBDBC.[id_Rombase_DOSBox_Configs])) AS [id_Rombase_DOSBox_Configs]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[Displayname], IFNULL(DBCT.[Displayname], RBDBC.[Displayname])) AS [Displayname]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sdl-fullscreen], IFNULL(DBCT.[sdl-fullscreen], RBDBC.[sdl-fullscreen])) AS [sdl-fullscreen]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sdl-fulldouble], IFNULL(DBCT.[sdl-fulldouble], RBDBC.[sdl-fulldouble])) AS [sdl-fulldouble]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sdl-fullresolution], IFNULL(DBCT.[sdl-fullresolution], RBDBC.[sdl-fullresolution])) AS [sdl-fullresolution]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sdl-windowresolution], IFNULL(DBCT.[sdl-windowresolution], RBDBC.[sdl-windowresolution])) AS [sdl-windowresolution]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sdl-output], IFNULL(DBCT.[sdl-output], RBDBC.[sdl-output])) AS [sdl-output]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sdl-autolock], IFNULL(DBCT.[sdl-autolock], RBDBC.[sdl-autolock])) AS [sdl-autolock]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sdl-sensitivity], IFNULL(DBCT.[sdl-sensitivity], RBDBC.[sdl-sensitivity])) AS [sdl-sensitivity]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sdl-waitonerror], IFNULL(DBCT.[sdl-waitonerror], RBDBC.[sdl-waitonerror])) AS [sdl-waitonerror]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sdl-priority_1], IFNULL(DBCT.[sdl-priority_1], RBDBC.[sdl-priority_1])) AS [sdl-priority_1]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sdl-priority_2], IFNULL(DBCT.[sdl-priority_2], RBDBC.[sdl-priority_2])) AS [sdl-priority_2]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sdl-mapperfile], IFNULL(DBCT.[sdl-mapperfile], RBDBC.[sdl-mapperfile])) AS [sdl-mapperfile]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sdl-usescancodes], IFNULL(DBCT.[sdl-usescancodes], RBDBC.[sdl-usescancodes])) AS [sdl-usescancodes]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[dosbox-language], IFNULL(DBCT.[dosbox-language], RBDBC.[dosbox-language])) AS [dosbox-language]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[dosbox-machine], IFNULL(DBCT.[dosbox-machine], RBDBC.[dosbox-machine])) AS [dosbox-machine]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[dosbox-memsize], IFNULL(DBCT.[dosbox-memsize], RBDBC.[dosbox-memsize])) AS [dosbox-memsize]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[render-frameskip], IFNULL(DBCT.[render-frameskip], RBDBC.[render-frameskip])) AS [render-frameskip]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[render-aspect], IFNULL(DBCT.[render-aspect], RBDBC.[render-aspect])) AS [render-aspect]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[render-scaler], IFNULL(DBCT.[render-scaler], RBDBC.[render-scaler])) AS [render-scaler]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[render-scaler_forced], IFNULL(DBCT.[render-scaler_forced], RBDBC.[render-scaler_forced])) AS [render-scaler_forced]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[cpu-core], IFNULL(DBCT.[cpu-core], RBDBC.[cpu-core])) AS [cpu-core]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[cpu-cputype], IFNULL(DBCT.[cpu-cputype], RBDBC.[cpu-cputype])) AS [cpu-cputype]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[cpu-cycles], IFNULL(DBCT.[cpu-cycles], RBDBC.[cpu-cycles])) AS [cpu-cycles]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[cpu-cycleup], IFNULL(DBCT.[cpu-cycleup], RBDBC.[cpu-cycleup])) AS [cpu-cycleup]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[cpu-cycledown], IFNULL(DBCT.[cpu-cycledown], RBDBC.[cpu-cycledown])) AS [cpu-cycledown]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[mixer-nosound], IFNULL(DBCT.[mixer-nosound], RBDBC.[mixer-nosound])) AS [mixer-nosound]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[mixer-rate], IFNULL(DBCT.[mixer-rate], RBDBC.[mixer-rate])) AS [mixer-rate]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[mixer-blocksize], IFNULL(DBCT.[mixer-blocksize], RBDBC.[mixer-blocksize])) AS [mixer-blocksize]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[mixer-prebuffer], IFNULL(DBCT.[mixer-prebuffer], RBDBC.[mixer-prebuffer])) AS [mixer-prebuffer]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[midi-mpu401], IFNULL(DBCT.[midi-mpu401], RBDBC.[midi-mpu401])) AS [midi-mpu401]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[midi-mididevice], IFNULL(DBCT.[midi-mididevice], RBDBC.[midi-mididevice])) AS [midi-mididevice]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[midi-midiconfig], IFNULL(DBCT.[midi-midiconfig], RBDBC.[midi-midiconfig])) AS [midi-midiconfig]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sblaster-sbtype], IFNULL(DBCT.[sblaster-sbtype], RBDBC.[sblaster-sbtype])) AS [sblaster-sbtype]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sblaster-sbbase], IFNULL(DBCT.[sblaster-sbbase], RBDBC.[sblaster-sbbase])) AS [sblaster-sbbase]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sblaster-irq], IFNULL(DBCT.[sblaster-irq], RBDBC.[sblaster-irq])) AS [sblaster-irq]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sblaster-dma], IFNULL(DBCT.[sblaster-dma], RBDBC.[sblaster-dma])) AS [sblaster-dma]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sblaster-hdma], IFNULL(DBCT.[sblaster-hdma], RBDBC.[sblaster-hdma])) AS [sblaster-hdma]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sblaster-sbmixer], IFNULL(DBCT.[sblaster-sbmixer], RBDBC.[sblaster-sbmixer])) AS [sblaster-sbmixer]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sblaster-oplmode], IFNULL(DBCT.[sblaster-oplmode], RBDBC.[sblaster-oplmode])) AS [sblaster-oplmode]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sblaster-oplemu], IFNULL(DBCT.[sblaster-oplemu], RBDBC.[sblaster-oplemu])) AS [sblaster-oplemu]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sblaster-oplrate], IFNULL(DBCT.[sblaster-oplrate], RBDBC.[sblaster-oplrate])) AS [sblaster-oplrate]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[gus-gus], IFNULL(DBCT.[gus-gus], RBDBC.[gus-gus])) AS [gus-gus]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[gus-gusrate], IFNULL(DBCT.[gus-gusrate], RBDBC.[gus-gusrate])) AS [gus-gusrate]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[gus-gusbase], IFNULL(DBCT.[gus-gusbase], RBDBC.[gus-gusbase])) AS [gus-gusbase]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[gus-gusirq], IFNULL(DBCT.[gus-gusirq], RBDBC.[gus-gusirq])) AS [gus-gusirq]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[gus-gusdma], IFNULL(DBCT.[gus-gusdma], RBDBC.[gus-gusdma])) AS [gus-gusdma]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[gus-ultradir], IFNULL(DBCT.[gus-ultradir], RBDBC.[gus-ultradir])) AS [gus-ultradir]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[speaker-pcspeaker], IFNULL(DBCT.[speaker-pcspeaker], RBDBC.[speaker-pcspeaker])) AS [speaker-pcspeaker]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[speaker-pcrate], IFNULL(DBCT.[speaker-pcrate], RBDBC.[speaker-pcrate])) AS [speaker-pcrate]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[speaker-tandy], IFNULL(DBCT.[speaker-tandy], RBDBC.[speaker-tandy])) AS [speaker-tandy]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[speaker-tandyrate], IFNULL(DBCT.[speaker-tandyrate], RBDBC.[speaker-tandyrate])) AS [speaker-tandyrate]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[speaker-disney], IFNULL(DBCT.[speaker-disney], RBDBC.[speaker-disney])) AS [speaker-disney]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[joystick-joysticktype], IFNULL(DBCT.[joystick-joysticktype], RBDBC.[joystick-joysticktype])) AS [joystick-joysticktype]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[joystick-timed], IFNULL(DBCT.[joystick-timed], RBDBC.[joystick-timed])) AS [joystick-timed]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[joystick-autofire], IFNULL(DBCT.[joystick-autofire], RBDBC.[joystick-autofire])) AS [joystick-autofire]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[joystick-swap34], IFNULL(DBCT.[joystick-swap34], RBDBC.[joystick-swap34])) AS [joystick-swap34]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[joystick-buttonwrap], IFNULL(DBCT.[joystick-buttonwrap], RBDBC.[joystick-buttonwrap])) AS [joystick-buttonwrap]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[serial-serial1], IFNULL(DBCT.[serial-serial1], RBDBC.[serial-serial1])) AS [serial-serial1]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[serial-serial2], IFNULL(DBCT.[serial-serial2], RBDBC.[serial-serial2])) AS [serial-serial2]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[serial-serial3], IFNULL(DBCT.[serial-serial3], RBDBC.[serial-serial3])) AS [serial-serial3]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[serial-serial4], IFNULL(DBCT.[serial-serial4], RBDBC.[serial-serial4])) AS [serial-serial4]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[dos-xms], IFNULL(DBCT.[dos-xms], RBDBC.[dos-xms])) AS [dos-xms]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[dos-ems], IFNULL(DBCT.[dos-ems], RBDBC.[dos-ems])) AS [dos-ems]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[dos-umb], IFNULL(DBCT.[dos-umb], RBDBC.[dos-umb])) AS [dos-umb]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[dos-keyboardlayout], IFNULL(DBCT.[dos-keyboardlayout], RBDBC.[dos-keyboardlayout])) AS [dos-keyboardlayout]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ipx-ipx], IFNULL(DBCT.[ipx-ipx], RBDBC.[ipx-ipx])) AS [ipx-ipx]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[autoexec-before], IFNULL(DBCT.[autoexec-before], RBDBC.[autoexec-before])) AS [autoexec-before]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[autoexec-after], IFNULL(DBCT.[autoexec-after], RBDBC.[autoexec-after])) AS [autoexec-after]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ml-autoclose], IFNULL(DBCT.[ml-autoclose], RBDBC.[ml-autoclose])) AS [ml-autoclose]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ml-showconsole], IFNULL(DBCT.[ml-showconsole], RBDBC.[ml-showconsole])) AS [ml-showconsole]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ml-customsettings], IFNULL(DBCT.[ml-customsettings], RBDBC.[ml-customsettings])) AS [ml-customsettings]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ml-useloadfix], IFNULL(DBCT.[ml-useloadfix], RBDBC.[ml-useloadfix])) AS [ml-useloadfix]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ml-loadfix], IFNULL(DBCT.[ml-loadfix], RBDBC.[ml-loadfix])) AS [ml-loadfix]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ml-volume_master_left], IFNULL(DBCT.[ml-volume_master_left], RBDBC.[ml-volume_master_left])) AS [ml-volume_master_left]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ml-volume_master_right], IFNULL(DBCT.[ml-volume_master_right], RBDBC.[ml-volume_master_right])) AS [ml-volume_master_right]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ml-volume_spkr_left], IFNULL(DBCT.[ml-volume_spkr_left], RBDBC.[ml-volume_spkr_left])) AS [ml-volume_spkr_left]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ml-volume_spkr_right], IFNULL(DBCT.[ml-volume_spkr_right], RBDBC.[ml-volume_spkr_right])) AS [ml-volume_spkr_right]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ml-volume_sb_left], IFNULL(DBCT.[ml-volume_sb_left], RBDBC.[ml-volume_sb_left])) AS [ml-volume_sb_left]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ml-volume_sb_right], IFNULL(DBCT.[ml-volume_sb_right], RBDBC.[ml-volume_sb_right])) AS [ml-volume_sb_right]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ml-volume_disney_left], IFNULL(DBCT.[ml-volume_disney_left], RBDBC.[ml-volume_disney_left])) AS [ml-volume_disney_left]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ml-volume_disney_right], IFNULL(DBCT.[ml-volume_disney_right], RBDBC.[ml-volume_disney_right])) AS [ml-volume_disney_right]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ml-volume_gus_left], IFNULL(DBCT.[ml-volume_gus_left], RBDBC.[ml-volume_gus_left])) AS [ml-volume_gus_left]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ml-volume_gus_right], IFNULL(DBCT.[ml-volume_gus_right], RBDBC.[ml-volume_gus_right])) AS [ml-volume_gus_right]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ml-volume_fm_left], IFNULL(DBCT.[ml-volume_fm_left], RBDBC.[ml-volume_fm_left])) AS [ml-volume_fm_left]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ml-volume_fm_right], IFNULL(DBCT.[ml-volume_fm_right], RBDBC.[ml-volume_fm_right])) AS [ml-volume_fm_right]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ml-volume_cdaudio_left], IFNULL(DBCT.[ml-volume_cdaudio_left], RBDBC.[ml-volume_cdaudio_left])) AS [ml-volume_cdaudio_left]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[ml-volume_cdaudio_right], IFNULL(DBCT.[ml-volume_cdaudio_right], RBDBC.[ml-volume_cdaudio_right])) AS [ml-volume_cdaudio_right]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_sdl_pixelshader], IFNULL(DBCT.[p_sdl_pixelshader], RBDBC.[p_sdl_pixelshader])) AS [p_sdl_pixelshader]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_sdl_pixelshader_forced], IFNULL(DBCT.[p_sdl_pixelshader_forced], RBDBC.[p_sdl_pixelshader_forced])) AS [p_sdl_pixelshader_forced]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_sdl_output], IFNULL(DBCT.[p_sdl_output], RBDBC.[p_sdl_output])) AS [p_sdl_output]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_sdl_surfacenp-sharpness], IFNULL(DBCT.[p_sdl_surfacenp-sharpness], RBDBC.[p_sdl_surfacenp-sharpness])) AS [p_sdl_surfacenp-sharpness]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_sdl_surface-collapse-dbl], IFNULL(DBCT.[p_sdl_surface-collapse-dbl], RBDBC.[p_sdl_surface-collapse-dbl])) AS [p_sdl_surface-collapse-dbl]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_dosbox_vmemsize], IFNULL(DBCT.[p_dosbox_vmemsize], RBDBC.[p_dosbox_vmemsize])) AS [p_dosbox_vmemsize]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_dosbox_memsizekb], IFNULL(DBCT.[p_dosbox_memsizekb], RBDBC.[p_dosbox_memsizekb])) AS [p_dosbox_memsizekb]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_dosbox_forcerate], IFNULL(DBCT.[p_dosbox_forcerate], RBDBC.[p_dosbox_forcerate])) AS [p_dosbox_forcerate]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_dosbox_pit_hack], IFNULL(DBCT.[p_dosbox_pit_hack], RBDBC.[p_dosbox_pit_hack])) AS [p_dosbox_pit_hack]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_render_scaler], IFNULL(DBCT.[p_render_scaler], RBDBC.[p_render_scaler])) AS [p_render_scaler]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_render_autofit], IFNULL(DBCT.[p_render_autofit], RBDBC.[p_render_autofit])) AS [p_render_autofit]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_vsync_vsyncmode], IFNULL(DBCT.[p_vsync_vsyncmode], RBDBC.[p_vsync_vsyncmode])) AS [p_vsync_vsyncmode]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_vsync_vsyncrate], IFNULL(DBCT.[p_vsync_vsyncrate], RBDBC.[p_vsync_vsyncrate])) AS [p_vsync_vsyncrate]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_cpu_cputype], IFNULL(DBCT.[p_cpu_cputype], RBDBC.[p_cpu_cputype])) AS [p_cpu_cputype]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_keyboard_aux], IFNULL(DBCT.[p_keyboard_aux], RBDBC.[p_keyboard_aux])) AS [p_keyboard_aux]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_keyboard_auxdevice], IFNULL(DBCT.[p_keyboard_auxdevice], RBDBC.[p_keyboard_auxdevice])) AS [p_keyboard_auxdevice]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_voodoo], IFNULL(DBCT.[p_voodoo], RBDBC.[p_voodoo])) AS [p_voodoo]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_voodoo_voodoomem], IFNULL(DBCT.[p_voodoo_voodoomem], RBDBC.[p_voodoo_voodoomem])) AS [p_voodoo_voodoomem]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_mixer_swapstereo], IFNULL(DBCT.[p_mixer_swapstereo], RBDBC.[p_mixer_swapstereo])) AS [p_mixer_swapstereo]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_mididevice], IFNULL(DBCT.[p_midi_mididevice], RBDBC.[p_midi_mididevice])) AS [p_midi_mididevice]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_reverse_stereo], IFNULL(DBCT.[p_midi_mt32_reverse_stereo], RBDBC.[p_midi_mt32_reverse_stereo])) AS [p_midi_mt32_reverse_stereo]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_verbose], IFNULL(DBCT.[p_midi_mt32_verbose], RBDBC.[p_midi_mt32_verbose])) AS [p_midi_mt32_verbose]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_thread], IFNULL(DBCT.[p_midi_mt32_thread], RBDBC.[p_midi_mt32_thread])) AS [p_midi_mt32_thread]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_dac], IFNULL(DBCT.[p_midi_mt32_dac], RBDBC.[p_midi_mt32_dac])) AS [p_midi_mt32_dac]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_reverb_mode], IFNULL(DBCT.[p_midi_mt32_reverb_mode], RBDBC.[p_midi_mt32_reverb_mode])) AS [p_midi_mt32_reverb_mode]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_reverb_time], IFNULL(DBCT.[p_midi_mt32_reverb_time], RBDBC.[p_midi_mt32_reverb_time])) AS [p_midi_mt32_reverb_time]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_reverb_level], IFNULL(DBCT.[p_midi_mt32_reverb_level], RBDBC.[p_midi_mt32_reverb_level])) AS [p_midi_mt32_reverb_level]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_partials], IFNULL(DBCT.[p_midi_mt32_partials], RBDBC.[p_midi_mt32_partials])) AS [p_midi_mt32_partials]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_romdir], IFNULL(DBCT.[p_midi_mt32_romdir], RBDBC.[p_midi_mt32_romdir])) AS [p_midi_mt32_romdir]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_chunk], IFNULL(DBCT.[p_midi_mt32_chunk], RBDBC.[p_midi_mt32_chunk])) AS [p_midi_mt32_chunk]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_prebuffer], IFNULL(DBCT.[p_midi_mt32_prebuffer], RBDBC.[p_midi_mt32_prebuffer])) AS [p_midi_mt32_prebuffer]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_analog], IFNULL(DBCT.[p_midi_mt32_analog], RBDBC.[p_midi_mt32_analog])) AS [p_midi_mt32_analog]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_rate], IFNULL(DBCT.[p_midi_mt32_rate], RBDBC.[p_midi_mt32_rate])) AS [p_midi_mt32_rate]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_src_quality], IFNULL(DBCT.[p_midi_mt32_src_quality], RBDBC.[p_midi_mt32_src_quality])) AS [p_midi_mt32_src_quality]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_mt32_niceampramp], IFNULL(DBCT.[p_midi_mt32_niceampramp], RBDBC.[p_midi_mt32_niceampramp])) AS [p_midi_mt32_niceampramp]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_soundfont], IFNULL(DBCT.[p_midi_fluid_soundfont], RBDBC.[p_midi_fluid_soundfont])) AS [p_midi_fluid_soundfont]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_samplerate], IFNULL(DBCT.[p_midi_fluid_samplerate], RBDBC.[p_midi_fluid_samplerate])) AS [p_midi_fluid_samplerate]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_gain], IFNULL(DBCT.[p_midi_fluid_gain], RBDBC.[p_midi_fluid_gain])) AS [p_midi_fluid_gain]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_polyphony], IFNULL(DBCT.[p_midi_fluid_polyphony], RBDBC.[p_midi_fluid_polyphony])) AS [p_midi_fluid_polyphony]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_cores], IFNULL(DBCT.[p_midi_fluid_cores], RBDBC.[p_midi_fluid_cores])) AS [p_midi_fluid_cores]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_periods], IFNULL(DBCT.[p_midi_fluid_periods], RBDBC.[p_midi_fluid_periods])) AS [p_midi_fluid_periods]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_periodsize], IFNULL(DBCT.[p_midi_fluid_periodsize], RBDBC.[p_midi_fluid_periodsize])) AS [p_midi_fluid_periodsize]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_reverb], IFNULL(DBCT.[p_midi_fluid_reverb], RBDBC.[p_midi_fluid_reverb])) AS [p_midi_fluid_reverb]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_chorus], IFNULL(DBCT.[p_midi_fluid_chorus], RBDBC.[p_midi_fluid_chorus])) AS [p_midi_fluid_chorus]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_reverb_roomsize], IFNULL(DBCT.[p_midi_fluid_reverb_roomsize], RBDBC.[p_midi_fluid_reverb_roomsize])) AS [p_midi_fluid_reverb_roomsize]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_reverb_damping], IFNULL(DBCT.[p_midi_fluid_reverb_damping], RBDBC.[p_midi_fluid_reverb_damping])) AS [p_midi_fluid_reverb_damping]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_reverb_width], IFNULL(DBCT.[p_midi_fluid_reverb_width], RBDBC.[p_midi_fluid_reverb_width])) AS [p_midi_fluid_reverb_width]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_reverb_level], IFNULL(DBCT.[p_midi_fluid_reverb_level], RBDBC.[p_midi_fluid_reverb_level])) AS [p_midi_fluid_reverb_level]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_chorus_number], IFNULL(DBCT.[p_midi_fluid_chorus_number], RBDBC.[p_midi_fluid_chorus_number])) AS [p_midi_fluid_chorus_number]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_chorus_level], IFNULL(DBCT.[p_midi_fluid_chorus_level], RBDBC.[p_midi_fluid_chorus_level])) AS [p_midi_fluid_chorus_level]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_chorus_speed], IFNULL(DBCT.[p_midi_fluid_chorus_speed], RBDBC.[p_midi_fluid_chorus_speed])) AS [p_midi_fluid_chorus_speed]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_chorus_depth], IFNULL(DBCT.[p_midi_fluid_chorus_depth], RBDBC.[p_midi_fluid_chorus_depth])) AS [p_midi_fluid_chorus_depth]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_midi_fluid_chorus_type], IFNULL(DBCT.[p_midi_fluid_chorus_type], RBDBC.[p_midi_fluid_chorus_type])) AS [p_midi_fluid_chorus_type]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_sblaster_oplmode], IFNULL(DBCT.[p_sblaster_oplmode], RBDBC.[p_sblaster_oplmode])) AS [p_sblaster_oplmode]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_sblaster_oplemu], IFNULL(DBCT.[p_sblaster_oplemu], RBDBC.[p_sblaster_oplemu])) AS [p_sblaster_oplemu]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_sblaster_hardwarebase], IFNULL(DBCT.[p_sblaster_hardwarebase], RBDBC.[p_sblaster_hardwarebase])) AS [p_sblaster_hardwarebase]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_sblaster_goldplay], IFNULL(DBCT.[p_sblaster_goldplay], RBDBC.[p_sblaster_goldplay])) AS [p_sblaster_goldplay]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_innova_innova], IFNULL(DBCT.[p_innova_innova], RBDBC.[p_innova_innova])) AS [p_innova_innova]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_innova_samplerate], IFNULL(DBCT.[p_innova_samplerate], RBDBC.[p_innova_samplerate])) AS [p_innova_samplerate]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_innova_sidbase], IFNULL(DBCT.[p_innova_sidbase], RBDBC.[p_innova_sidbase])) AS [p_innova_sidbase]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_innova_quality], IFNULL(DBCT.[p_innova_quality], RBDBC.[p_innova_quality])) AS [p_innova_quality]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_speaker_ps1audio], IFNULL(DBCT.[p_speaker_ps1audio], RBDBC.[p_speaker_ps1audio])) AS [p_speaker_ps1audio]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_speaker_ps1audiorate], IFNULL(DBCT.[p_speaker_ps1audiorate], RBDBC.[p_speaker_ps1audiorate])) AS [p_speaker_ps1audiorate]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_printer_printer], IFNULL(DBCT.[p_printer_printer], RBDBC.[p_printer_printer])) AS [p_printer_printer]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_printer_dpi], IFNULL(DBCT.[p_printer_dpi], RBDBC.[p_printer_dpi])) AS [p_printer_dpi]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_printer_width], IFNULL(DBCT.[p_printer_width], RBDBC.[p_printer_width])) AS [p_printer_width]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_printer_height], IFNULL(DBCT.[p_printer_height], RBDBC.[p_printer_height])) AS [p_printer_height]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_printer_printoutput], IFNULL(DBCT.[p_printer_printoutput], RBDBC.[p_printer_printoutput])) AS [p_printer_printoutput]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_printer_multipage], IFNULL(DBCT.[p_printer_multipage], RBDBC.[p_printer_multipage])) AS [p_printer_multipage]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_printer_docpath], IFNULL(DBCT.[p_printer_docpath], RBDBC.[p_printer_docpath])) AS [p_printer_docpath]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_printer_timeout], IFNULL(DBCT.[p_printer_timeout], RBDBC.[p_printer_timeout])) AS [p_printer_timeout]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_parallel_parallel1], IFNULL(DBCT.[p_parallel_parallel1], RBDBC.[p_parallel_parallel1])) AS [p_parallel_parallel1]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_parallel_parallel2], IFNULL(DBCT.[p_parallel_parallel2], RBDBC.[p_parallel_parallel2])) AS [p_parallel_parallel2]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_parallel_parallel3], IFNULL(DBCT.[p_parallel_parallel3], RBDBC.[p_parallel_parallel3])) AS [p_parallel_parallel3]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_parallel_dongle], IFNULL(DBCT.[p_parallel_dongle], RBDBC.[p_parallel_dongle])) AS [p_parallel_dongle]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_glide_glide], IFNULL(DBCT.[p_glide_glide], RBDBC.[p_glide_glide])) AS [p_glide_glide]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_glide_lfb], IFNULL(DBCT.[p_glide_lfb], RBDBC.[p_glide_lfb])) AS [p_glide_lfb]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_glide_splash], IFNULL(DBCT.[p_glide_splash], RBDBC.[p_glide_splash])) AS [p_glide_splash]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_ne2000_ne2000], IFNULL(DBCT.[p_ne2000_ne2000], RBDBC.[p_ne2000_ne2000])) AS [p_ne2000_ne2000]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_ne2000_nicbase], IFNULL(DBCT.[p_ne2000_nicbase], RBDBC.[p_ne2000_nicbase])) AS [p_ne2000_nicbase]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_ne2000_nicirq], IFNULL(DBCT.[p_ne2000_nicirq], RBDBC.[p_ne2000_nicirq])) AS [p_ne2000_nicirq]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_ne2000_macaddr], IFNULL(DBCT.[p_ne2000_macaddr], RBDBC.[p_ne2000_macaddr])) AS [p_ne2000_macaddr]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_ne2000_realnic], IFNULL(DBCT.[p_ne2000_realnic], RBDBC.[p_ne2000_realnic])) AS [p_ne2000_realnic]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_ide1_enable], IFNULL(DBCT.[p_ide1_enable], RBDBC.[p_ide1_enable])) AS [p_ide1_enable]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_ide1_int13fakeio], IFNULL(DBCT.[p_ide1_int13fakeio], RBDBC.[p_ide1_int13fakeio])) AS [p_ide1_int13fakeio]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_ide1_int13fakev86io], IFNULL(DBCT.[p_ide1_int13fakev86io], RBDBC.[p_ide1_int13fakev86io])) AS [p_ide1_int13fakev86io]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_ide2_enable], IFNULL(DBCT.[p_ide2_enable], RBDBC.[p_ide2_enable])) AS [p_ide2_enable]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_ide2_int13fakeio], IFNULL(DBCT.[p_ide2_int13fakeio], RBDBC.[p_ide2_int13fakeio])) AS [p_ide2_int13fakeio]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_ide2_int13fakev86io], IFNULL(DBCT.[p_ide2_int13fakev86io], RBDBC.[p_ide2_int13fakev86io])) AS [p_ide2_int13fakev86io]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_ide3_enable], IFNULL(DBCT.[p_ide3_enable], RBDBC.[p_ide3_enable])) AS [p_ide3_enable]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_ide3_int13fakeio], IFNULL(DBCT.[p_ide3_int13fakeio], RBDBC.[p_ide3_int13fakeio])) AS [p_ide3_int13fakeio]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_ide3_int13fakev86io], IFNULL(DBCT.[p_ide3_int13fakev86io], RBDBC.[p_ide3_int13fakev86io])) AS [p_ide3_int13fakev86io]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_ide4_enable], IFNULL(DBCT.[p_ide4_enable], RBDBC.[p_ide4_enable])) AS [p_ide4_enable]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_ide4_int13fakeio], IFNULL(DBCT.[p_ide4_int13fakeio], RBDBC.[p_ide4_int13fakeio])) AS [p_ide4_int13fakeio]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[p_ide4_int13fakev86io], IFNULL(DBCT.[p_ide4_int13fakev86io], RBDBC.[p_ide4_int13fakev86io])) AS [p_ide4_int13fakev86io]" & ControlChars.CrLf
		sSQL &= "	FROM main.tbl_Emu_Games EG " & ControlChars.CrLf
		sSQL &= " LEFT JOIN main.tbl_DOSBox_Configs DBC ON DBC.id_DOSBox_Configs = EG.id_DOSBox_Configs" & ControlChars.CrLf
		sSQL &= " LEFT JOIN main.tbl_DOSBox_Configs DBCT ON DBCT.id_DOSBox_Configs = EG.id_DOSBox_Configs_Template" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN rombase.tbl_Rombase_DOSBox_Configs RBDBC ON DBCT.id_Rombase_DOSBox_Configs = RBDBC.id_Rombase_DOSBox_Configs" & ControlChars.CrLf
		sSQL &= "	WHERE EG.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games)

		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

	Public Shared Sub Fill_tbl_ScummVM_Configs(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DS_ML.tbl_ScummVM_ConfigsDataTable, Optional ByVal id_Emu_Games As Long = 0)
		If dt Is Nothing Then dt = New DS_ML.tbl_ScummVM_ConfigsDataTable
		dt.Clear()

		Dim sSQL As String = ""
		sSQL &= "	SELECT" & ControlChars.CrLf
		sSQL &= "		IFNULL(DBC.[id_ScummVM_Configs], DBCT.[id_ScummVM_Configs]) AS [id_ScummVM_Configs]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[id_Rombase_ScummVM_Configs], IFNULL(DBCT.[id_Rombase_ScummVM_Configs], RBDBC.[id_Rombase_ScummVM_Configs])) AS [id_Rombase_ScummVM_Configs]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[Displayname], IFNULL(DBCT.[Displayname], RBDBC.[Displayname])) AS [Displayname]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[user_defined_config], IFNULL(DBCT.[user_defined_config], RBDBC.[user_defined_config])) AS [user_defined_config]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[user_defined_commandline], IFNULL(DBCT.[user_defined_commandline], RBDBC.[user_defined_commandline])) AS [user_defined_commandline]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[console], IFNULL(DBCT.[console], RBDBC.[console])) AS [console]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[confirm_exit], IFNULL(DBCT.[confirm_exit], RBDBC.[confirm_exit])) AS [confirm_exit]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[savepath], IFNULL(DBCT.[savepath], RBDBC.[savepath])) AS [savepath]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[extrapath], IFNULL(DBCT.[extrapath], RBDBC.[extrapath])) AS [extrapath]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[language], IFNULL(DBCT.[language], RBDBC.[language])) AS [language]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[joystick_num], IFNULL(DBCT.[joystick_num], RBDBC.[joystick_num])) AS [joystick_num]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[copy_protection], IFNULL(DBCT.[copy_protection], RBDBC.[copy_protection])) AS [copy_protection]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[boot_param], IFNULL(DBCT.[boot_param], RBDBC.[boot_param])) AS [boot_param]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[fullscreen], IFNULL(DBCT.[fullscreen], RBDBC.[fullscreen])) AS [fullscreen]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[aspect_ratio], IFNULL(DBCT.[aspect_ratio], RBDBC.[aspect_ratio])) AS [aspect_ratio]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[gfx_mode], IFNULL(DBCT.[gfx_mode], RBDBC.[gfx_mode])) AS [gfx_mode]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[subtitles], IFNULL(DBCT.[subtitles], RBDBC.[subtitles])) AS [subtitles]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[music_driver], IFNULL(DBCT.[music_driver], RBDBC.[music_driver])) AS [music_driver]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[multi_midi], IFNULL(DBCT.[multi_midi], RBDBC.[multi_midi])) AS [multi_midi]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[soundfont], IFNULL(DBCT.[soundfont], RBDBC.[soundfont])) AS [soundfont]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[native_mt32], IFNULL(DBCT.[native_mt32], RBDBC.[native_mt32])) AS [native_mt32]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[enable_gs], IFNULL(DBCT.[enable_gs], RBDBC.[enable_gs])) AS [enable_gs]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[output_rate], IFNULL(DBCT.[output_rate], RBDBC.[output_rate])) AS [output_rate]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[music_volume], IFNULL(DBCT.[music_volume], RBDBC.[music_volume])) AS [music_volume]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[speech_volume], IFNULL(DBCT.[speech_volume], RBDBC.[speech_volume])) AS [speech_volume]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sfx_volume], IFNULL(DBCT.[sfx_volume], RBDBC.[sfx_volume])) AS [sfx_volume]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[speech_mute], IFNULL(DBCT.[speech_mute], RBDBC.[speech_mute])) AS [speech_mute]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[talkspeed], IFNULL(DBCT.[talkspeed], RBDBC.[talkspeed])) AS [talkspeed]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[cdrom], IFNULL(DBCT.[cdrom], RBDBC.[cdrom])) AS [cdrom]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[tempo], IFNULL(DBCT.[tempo], RBDBC.[tempo])) AS [tempo]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[midi_gain], IFNULL(DBCT.[midi_gain], RBDBC.[midi_gain])) AS [midi_gain]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[autosave_period], IFNULL(DBCT.[autosave_period], RBDBC.[autosave_period])) AS [autosave_period]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[save_slot], IFNULL(DBCT.[save_slot], RBDBC.[save_slot])) AS [save_slot]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[demo_mode], IFNULL(DBCT.[demo_mode], RBDBC.[demo_mode])) AS [demo_mode]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[alt_intro], IFNULL(DBCT.[alt_intro], RBDBC.[alt_intro])) AS [alt_intro]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[music_mute], IFNULL(DBCT.[music_mute], RBDBC.[music_mute])) AS [music_mute]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[sfx_mute], IFNULL(DBCT.[sfx_mute], RBDBC.[sfx_mute])) AS [sfx_mute]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[gfx_details], IFNULL(DBCT.[gfx_details], RBDBC.[gfx_details])) AS [gfx_details]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[object_labels], IFNULL(DBCT.[object_labels], RBDBC.[object_labels])) AS [object_labels]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[reverse_stereo], IFNULL(DBCT.[reverse_stereo], RBDBC.[reverse_stereo])) AS [reverse_stereo]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[walkspeed], IFNULL(DBCT.[walkspeed], RBDBC.[walkspeed])) AS [walkspeed]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[filtering], IFNULL(DBCT.[filtering], RBDBC.[filtering])) AS [filtering]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[opl_driver], IFNULL(DBCT.[opl_driver], RBDBC.[opl_driver])) AS [opl_driver]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[originalsaveload], IFNULL(DBCT.[originalsaveload], RBDBC.[originalsaveload])) AS [originalsaveload]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[altamigapalette], IFNULL(DBCT.[altamigapalette], RBDBC.[altamigapalette])) AS [altamigapalette]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[mousesupport], IFNULL(DBCT.[mousesupport], RBDBC.[mousesupport])) AS [mousesupport]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[disable_dithering], IFNULL(DBCT.[disable_dithering], RBDBC.[disable_dithering])) AS [disable_dithering]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[prefer_digitalsfx], IFNULL(DBCT.[prefer_digitalsfx], RBDBC.[prefer_digitalsfx])) AS [prefer_digitalsfx]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[native_fb01], IFNULL(DBCT.[native_fb01], RBDBC.[native_fb01])) AS [native_fb01]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[use_cdaudio], IFNULL(DBCT.[use_cdaudio], RBDBC.[use_cdaudio])) AS [use_cdaudio]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[windows_cursors], IFNULL(DBCT.[windows_cursors], RBDBC.[windows_cursors])) AS [windows_cursors]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[silver_cursors], IFNULL(DBCT.[silver_cursors], RBDBC.[silver_cursors])) AS [silver_cursors]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[enable_gore], IFNULL(DBCT.[enable_gore], RBDBC.[enable_gore])) AS [enable_gore]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[smooth_scrolling], IFNULL(DBCT.[smooth_scrolling], RBDBC.[smooth_scrolling])) AS [smooth_scrolling]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[floating_cursors], IFNULL(DBCT.[floating_cursors], RBDBC.[floating_cursors])) AS [floating_cursors]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[enable_color_blind], IFNULL(DBCT.[enable_color_blind], RBDBC.[enable_color_blind])) AS [enable_color_blind]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[studio_audience], IFNULL(DBCT.[studio_audience], RBDBC.[studio_audience])) AS [studio_audience]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[skip_support], IFNULL(DBCT.[skip_support], RBDBC.[skip_support])) AS [skip_support]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[helium_mode], IFNULL(DBCT.[helium_mode], RBDBC.[helium_mode])) AS [helium_mode]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[skiphallofrecordsscenes], IFNULL(DBCT.[skiphallofrecordsscenes], RBDBC.[skiphallofrecordsscenes])) AS [skiphallofrecordsscenes]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[scalemakingofvideos], IFNULL(DBCT.[scalemakingofvideos], RBDBC.[scalemakingofvideos])) AS [scalemakingofvideos]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[fast_movie_speed], IFNULL(DBCT.[fast_movie_speed], RBDBC.[fast_movie_speed])) AS [fast_movie_speed]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[doublefps], IFNULL(DBCT.[doublefps], RBDBC.[doublefps])) AS [doublefps]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[venusenabled], IFNULL(DBCT.[venusenabled], RBDBC.[venusenabled])) AS [venusenabled]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[noanimwhileturning], IFNULL(DBCT.[noanimwhileturning], RBDBC.[noanimwhileturning])) AS [noanimwhileturning]" & ControlChars.CrLf
		sSQL &= "		, IFNULL(DBC.[mpegmovies], IFNULL(DBCT.[mpegmovies], RBDBC.[mpegmovies])) AS [mpegmovies]" & ControlChars.CrLf
		sSQL &= "	FROM main.tbl_Emu_Games EG " & ControlChars.CrLf
		sSQL &= " LEFT JOIN main.tbl_ScummVM_Configs DBC ON DBC.id_ScummVM_Configs = EG.id_ScummVM_Configs" & ControlChars.CrLf
		sSQL &= " LEFT JOIN main.tbl_ScummVM_Configs DBCT ON DBCT.id_ScummVM_Configs = EG.id_ScummVM_Configs_Template" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN rombase.tbl_Rombase_ScummVM_Configs RBDBC ON DBCT.id_Rombase_ScummVM_Configs = RBDBC.id_Rombase_ScummVM_Configs" & ControlChars.CrLf
		sSQL &= "	WHERE EG.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games)

		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

	Public Shared Sub Fill_src_frm_Emulators_DOSBox_Patches(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DS_ML.src_frm_Emulators_DOSBox_PatchesDataTable, Optional ByVal id_Emulators As Integer = 0)
		Dim sSQL As String = ""
		sSQL &= "	SELECT" & ControlChars.CrLf
		sSQL &= "		P.id_Rombase_DOSBox_Patches AS id_DOSBox_Patches" & ControlChars.CrLf
		sSQL &= "		, PC.id_Rombase_DOSBox_Patches_Categories AS id_DOSBox_Patches_Categories" & ControlChars.CrLf
		sSQL &= "		, PC.Categoryname" & ControlChars.CrLf
		sSQL &= "		, P.Identifier" & ControlChars.CrLf
		sSQL &= "		, P.Patchname" & ControlChars.CrLf
		sSQL &= "		, P.Description" & ControlChars.CrLf
		sSQL &= "		, EP.Activated" & ControlChars.CrLf
		sSQL &= "		, P.DAUM_Supported" & ControlChars.CrLf
		sSQL &= "		, P.MB_Supported" & ControlChars.CrLf
		sSQL &= "		, P.ECE_Supported" & ControlChars.CrLf
		sSQL &= "	FROM rombase.tbl_Rombase_DOSBox_Patches P" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN rombase.tbl_Rombase_DOSBox_Patches_Categories PC ON P.id_Rombase_DOSBox_Patches_Categories = PC.id_Rombase_DOSBox_Patches_Categories" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Emulators_DOSBox_Patches EP ON P.id_Rombase_DOSBox_Patches = EP.id_DOSBox_Patches AND EP.id_Emulators = " & TC.getSQLFormat(id_Emulators) & ControlChars.CrLf
		sSQL &= "	ORDER BY PC.Sort, P.Sort" & ControlChars.CrLf

		dt.Clear()
		DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)
	End Sub

	Public Shared Sub Fill_src_frm_Emulators_DOSBox_Patches_Categories(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DS_ML.src_frm_Emulators_DOSBox_Patches_CategoriesDataTable)
		Dim sSQL As String = ""
		sSQL &= "	SELECT DISTINCT" & ControlChars.CrLf
		sSQL &= "		PC.id_Rombase_DOSBox_Patches_Categories AS id_DOSBox_Patches_Categories" & ControlChars.CrLf
		sSQL &= "		, PC.Categoryname" & ControlChars.CrLf
		sSQL &= "	FROM tbl_Rombase_DOSBox_Patches P" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Rombase_DOSBox_Patches_Categories PC ON P.id_Rombase_DOSBox_Patches_Categories = PC.id_Rombase_DOSBox_Patches_Categories" & ControlChars.CrLf
		sSQL &= "	ORDER BY PC.Sort, P.Sort" & ControlChars.CrLf
		dt.Clear()

		DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)
	End Sub

	Public Shared Sub Fill_tbl_Users(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As tbl_UsersDataTable, ByVal id_Users As Integer, ByVal ShowOnlyRestricted As Boolean)
		dt.Clear()
		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT id_Users, Admin, Username, Password, Restricted, IFNULL(id_Cheevo_Challenges, 0) AS id_Cheevo_Challenges FROM tbl_Users WHERE 1=1 " & IIf(id_Users > 0, " AND id_Users = " & TC.getSQLFormat(id_Users), "") & IIf(ShowOnlyRestricted, " AND Restricted = 1", "") & " ORDER BY Username", dt, tran)
	End Sub

	Public Shared Sub Fill_tbl_Similarity_Calculation_Config(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As tbl_Similarity_Calculation_ConfigDataTable, Optional ByVal id_Similarity_Calculation_Config As Object = Nothing)
		dt.Clear()

		Dim sSQL As String = ""

		If Not IsNumeric(id_Similarity_Calculation_Config) Then
			sSQL &= "SELECT" & ControlChars.CrLf
			sSQL &= "	0 AS id_Similarity_Calculation_Config" & ControlChars.CrLf
			sSQL &= "	, 'Default' AS Name" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_001_Platform" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_002_MobyRank" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_003_MobyScore" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_004_Publisher" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_005_Developer" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_006_Year" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_101_Basic_Genres" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_102_Perspectives" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_103_Sports_Themes" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_105_Educational_Categories" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_106_Other_Attributes" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_107_Visual_Presentation" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_108_Gameplay" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_109_Pacing" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_110_Narrative_Theme_Topic" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_111_Setting" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_112_Vehicular_Themes" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_113_Interface_Control" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_114_DLC_Addon" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_115_Special_Edition" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_201_MinPlayers" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_202_MaxPlayers" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_203_AgeO" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_204_AgeP" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_205_Rating_Descriptors" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_206_Other_Attributes" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_207_Multiplayer_Attributes" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_301_Group_Membership" & ControlChars.CrLf
			sSQL &= "	, 1 AS Weight_401_Staff" & ControlChars.CrLf
			sSQL &= "	, 0 AS Sort" & ControlChars.CrLf
			sSQL &= "UNION" & ControlChars.CrLf
		End If

		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= "	id_Similarity_Calculation_Config" & ControlChars.CrLf
		sSQL &= "	, Name" & ControlChars.CrLf
		sSQL &= "	, Weight_001_Platform" & ControlChars.CrLf
		sSQL &= "	, Weight_002_MobyRank" & ControlChars.CrLf
		sSQL &= "	, Weight_003_MobyScore" & ControlChars.CrLf
		sSQL &= "	, Weight_004_Publisher" & ControlChars.CrLf
		sSQL &= "	, Weight_005_Developer" & ControlChars.CrLf
		sSQL &= "	, Weight_006_Year" & ControlChars.CrLf
		sSQL &= "	, Weight_101_Basic_Genres" & ControlChars.CrLf
		sSQL &= "	, Weight_102_Perspectives" & ControlChars.CrLf
		sSQL &= "	, Weight_103_Sports_Themes" & ControlChars.CrLf
		sSQL &= "	, Weight_105_Educational_Categories" & ControlChars.CrLf
		sSQL &= "	, Weight_106_Other_Attributes" & ControlChars.CrLf
		sSQL &= "	, Weight_107_Visual_Presentation" & ControlChars.CrLf
		sSQL &= "	, Weight_108_Gameplay" & ControlChars.CrLf
		sSQL &= "	, Weight_109_Pacing" & ControlChars.CrLf
		sSQL &= "	, Weight_110_Narrative_Theme_Topic" & ControlChars.CrLf
		sSQL &= "	, Weight_111_Setting" & ControlChars.CrLf
		sSQL &= "	, Weight_112_Vehicular_Themes" & ControlChars.CrLf
		sSQL &= "	, Weight_113_Interface_Control" & ControlChars.CrLf
		sSQL &= "	, Weight_114_DLC_Addon" & ControlChars.CrLf
		sSQL &= "	, Weight_115_Special_Edition" & ControlChars.CrLf
		sSQL &= "	, Weight_201_MinPlayers" & ControlChars.CrLf
		sSQL &= "	, Weight_202_MaxPlayers" & ControlChars.CrLf
		sSQL &= "	, Weight_203_AgeO" & ControlChars.CrLf
		sSQL &= "	, Weight_204_AgeP" & ControlChars.CrLf
		sSQL &= "	, Weight_205_Rating_Descriptors" & ControlChars.CrLf
		sSQL &= "	, Weight_206_Other_Attributes" & ControlChars.CrLf
		sSQL &= "	, Weight_207_Multiplayer_Attributes" & ControlChars.CrLf
		sSQL &= "	, Weight_301_Group_Membership" & ControlChars.CrLf
		sSQL &= "	, Weight_401_Staff" & ControlChars.CrLf
		sSQL &= "	, 1 AS Sort" & ControlChars.CrLf
		sSQL &= "FROM tbl_Similarity_Calculation_Config" & ControlChars.CrLf

		If IsNumeric(id_Similarity_Calculation_Config) Then
			sSQL &= "WHERE id_Similarity_Calculation_Config = " & TC.getSQLFormat(id_Similarity_Calculation_Config) & ControlChars.CrLf
		End If

		sSQL &= "ORDER BY Sort, Name" & ControlChars.CrLf

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)
	End Sub

	Public Shared Function GenSQL_Fill_tbl_Similarity_Calculation_Genre(ByVal id_Moby_Genres_Categories, ByVal ColumnName)
		Dim sSQL As String = ""
		sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Genres FROM tbl_Moby_Games_Genres GG" & ControlChars.CrLf
		sSQL &= "									WHERE GG.id_Moby_Games = MG.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "									UNION SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Genres EGMG" & ControlChars.CrLf
		sSQL &= "									WHERE EGMG.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "								) AS temp_Genres" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Genres G ON temp_Genres.id_Moby_Genres = G.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "								WHERE G.id_Moby_Genres_Categories = " & TC.getSQLFormat(id_Moby_Genres_Categories) & ControlChars.CrLf
		sSQL &= "								AND G.id_Moby_Genres NOT IN" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Genres" & ControlChars.CrLf
		sSQL &= "									WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) = 0 " & ControlChars.CrLf
		sSQL &= "							AND" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Genres FROM tbl_Moby_Games_Genres GGC" & ControlChars.CrLf
		sSQL &= "									WHERE GGC.id_Moby_Games = MGC.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "									UNION SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Genres EGMGC" & ControlChars.CrLf
		sSQL &= "									WHERE EGMGC.id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "								) AS temp_Genres" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Genres GC ON temp_Genres.id_Moby_Genres = GC.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "								WHERE GC.id_Moby_Genres_Categories = " & TC.getSQLFormat(id_Moby_Genres_Categories) & ControlChars.CrLf
		sSQL &= "								AND GC.id_Moby_Genres NOT IN" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Genres" & ControlChars.CrLf
		sSQL &= "									WHERE id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) = 0" & ControlChars.CrLf
		sSQL &= "							THEN NULL" & ControlChars.CrLf
		sSQL &= "							ELSE" & ControlChars.CrLf
		sSQL &= "								CAST" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									CAST" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Genres FROM tbl_Moby_Games_Genres GG" & ControlChars.CrLf
		sSQL &= "												WHERE GG.id_Moby_Games = MG.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "												UNION SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Genres EGMG" & ControlChars.CrLf
		sSQL &= "												WHERE EGMG.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "											) AS temp_Genres" & ControlChars.CrLf
		sSQL &= "											LEFT JOIN tbl_Moby_Genres G ON temp_Genres.id_Moby_Genres = G.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "											WHERE G.id_Moby_Genres_Categories = " & TC.getSQLFormat(id_Moby_Genres_Categories) & ControlChars.CrLf
		sSQL &= "											AND G.id_Moby_Genres NOT IN" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Genres" & ControlChars.CrLf
		sSQL &= "												WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												AND Used = 0" & ControlChars.CrLf
		sSQL &= "											)" & ControlChars.CrLf
		sSQL &= "											AND G.id_Moby_Genres IN" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT temp_GenresC.id_Moby_Genres FROM" & ControlChars.CrLf
		sSQL &= "												(	SELECT id_Moby_Genres FROM tbl_Moby_Games_Genres GGC" & ControlChars.CrLf
		sSQL &= "													WHERE GGC.id_Moby_Games = MGC.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "													UNION SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "													FROM tbl_Emu_Games_Moby_Genres EGMGC" & ControlChars.CrLf
		sSQL &= "													WHERE EGMGC.id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												) AS temp_GenresC" & ControlChars.CrLf
		sSQL &= "												LEFT JOIN tbl_Moby_Genres GC ON temp_GenresC.id_Moby_Genres = GC.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "												WHERE GC.id_Moby_Genres_Categories = " & TC.getSQLFormat(id_Moby_Genres_Categories) & ControlChars.CrLf
		sSQL &= "												AND GC.id_Moby_Genres NOT IN" & ControlChars.CrLf
		sSQL &= "												(" & ControlChars.CrLf
		sSQL &= "													SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "													FROM tbl_Emu_Games_Moby_Genres" & ControlChars.CrLf
		sSQL &= "													WHERE id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "													AND Used = 0" & ControlChars.CrLf
		sSQL &= "												)" & ControlChars.CrLf
		sSQL &= "											)" & ControlChars.CrLf
		sSQL &= "										) " & ControlChars.CrLf
		sSQL &= "										AS FLOAT" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "" & ControlChars.CrLf
		sSQL &= "									/" & ControlChars.CrLf
		sSQL &= "" & ControlChars.CrLf
		sSQL &= "									CAST" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												(" & ControlChars.CrLf
		sSQL &= "													SELECT id_Moby_Genres FROM tbl_Moby_Games_Genres GG" & ControlChars.CrLf
		sSQL &= "													WHERE GG.id_Moby_Games = MG.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "													UNION SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "													FROM tbl_Emu_Games_Moby_Genres EGMG" & ControlChars.CrLf
		sSQL &= "													WHERE EGMG.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "													UNION SELECT id_Moby_Genres FROM tbl_Moby_Games_Genres GGC" & ControlChars.CrLf
		sSQL &= "													WHERE GGC.id_Moby_Games = MGC.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "													UNION SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "													FROM tbl_Emu_Games_Moby_Genres EGMGC" & ControlChars.CrLf
		sSQL &= "													WHERE EGMGC.id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												) AS temp_Genres" & ControlChars.CrLf
		sSQL &= "												INNER JOIN tbl_Moby_Genres G" & ControlChars.CrLf
		sSQL &= "												ON	temp_Genres.id_Moby_Genres = G.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "														AND G.id_Moby_Genres_Categories = " & TC.getSQLFormat(id_Moby_Genres_Categories) & ControlChars.CrLf
		sSQL &= "														AND G.id_Moby_Genres NOT IN" & ControlChars.CrLf
		sSQL &= "														(" & ControlChars.CrLf
		sSQL &= "															SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "															FROM tbl_Emu_Games_Moby_Genres" & ControlChars.CrLf
		sSQL &= "															WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "															AND Used = 0" & ControlChars.CrLf
		sSQL &= "															INTERSECT" & ControlChars.CrLf
		sSQL &= "															SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "															FROM tbl_Emu_Games_Moby_Genres" & ControlChars.CrLf
		sSQL &= "															WHERE id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "															AND Used = 0" & ControlChars.CrLf
		sSQL &= "														)" & ControlChars.CrLf
		sSQL &= "											)" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "" & ControlChars.CrLf
		sSQL &= "										AS FLOAT" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "									* 100" & ControlChars.CrLf
		sSQL &= "									AS INTEGER" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							END" & ControlChars.CrLf
		sSQL &= "							AS " & ColumnName & ControlChars.CrLf
		Return sSQL
	End Function

	Public Shared Function GenSQL_Fill_tbl_Similarity_Calculation_MobyReleases_Genre(ByVal id_Moby_Genres_Categories, ByVal ColumnName)
		Dim sSQL As String = ""
		sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Genres FROM tbl_Moby_Games_Genres GG" & ControlChars.CrLf
		sSQL &= "									WHERE GG.id_Moby_Games = MG.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "									UNION SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Genres EGMG" & ControlChars.CrLf
		sSQL &= "									WHERE EGMG.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "								) AS temp_Genres" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Genres G ON temp_Genres.id_Moby_Genres = G.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "								WHERE G.id_Moby_Genres_Categories = " & TC.getSQLFormat(id_Moby_Genres_Categories) & ControlChars.CrLf
		sSQL &= "								AND G.id_Moby_Genres NOT IN" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Genres" & ControlChars.CrLf
		sSQL &= "									WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) = 0 " & ControlChars.CrLf
		sSQL &= "							AND" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Genres FROM tbl_Moby_Games_Genres GGC" & ControlChars.CrLf
		sSQL &= "									WHERE GGC.id_Moby_Games = MGC.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "								) AS temp_Genres" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Genres GC ON temp_Genres.id_Moby_Genres = GC.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "								WHERE GC.id_Moby_Genres_Categories = " & TC.getSQLFormat(id_Moby_Genres_Categories) & ControlChars.CrLf
		sSQL &= "							) = 0" & ControlChars.CrLf
		sSQL &= "							THEN NULL" & ControlChars.CrLf
		sSQL &= "							ELSE" & ControlChars.CrLf
		sSQL &= "								CAST" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									CAST" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Genres FROM tbl_Moby_Games_Genres GG" & ControlChars.CrLf
		sSQL &= "												WHERE GG.id_Moby_Games = MG.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "												UNION SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Genres EGMG" & ControlChars.CrLf
		sSQL &= "												WHERE EGMG.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "											) AS temp_Genres" & ControlChars.CrLf
		sSQL &= "											LEFT JOIN tbl_Moby_Genres G ON temp_Genres.id_Moby_Genres = G.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "											WHERE G.id_Moby_Genres_Categories = " & TC.getSQLFormat(id_Moby_Genres_Categories) & ControlChars.CrLf
		sSQL &= "											AND G.id_Moby_Genres NOT IN" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Genres" & ControlChars.CrLf
		sSQL &= "												WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												AND Used = 0" & ControlChars.CrLf
		sSQL &= "											)" & ControlChars.CrLf
		sSQL &= "											AND G.id_Moby_Genres IN" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT temp_GenresC.id_Moby_Genres FROM" & ControlChars.CrLf
		sSQL &= "												(	SELECT id_Moby_Genres FROM tbl_Moby_Games_Genres GGC" & ControlChars.CrLf
		sSQL &= "													WHERE GGC.id_Moby_Games = MGC.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "												) AS temp_GenresC" & ControlChars.CrLf
		sSQL &= "												LEFT JOIN tbl_Moby_Genres GC ON temp_GenresC.id_Moby_Genres = GC.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "												WHERE GC.id_Moby_Genres_Categories = " & TC.getSQLFormat(id_Moby_Genres_Categories) & ControlChars.CrLf
		sSQL &= "											)" & ControlChars.CrLf
		sSQL &= "										) " & ControlChars.CrLf
		sSQL &= "										AS FLOAT" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "" & ControlChars.CrLf
		sSQL &= "									/" & ControlChars.CrLf
		sSQL &= "" & ControlChars.CrLf
		sSQL &= "									CAST" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												(" & ControlChars.CrLf
		sSQL &= "													SELECT id_Moby_Genres FROM tbl_Moby_Games_Genres GG" & ControlChars.CrLf
		sSQL &= "													WHERE GG.id_Moby_Games = MG.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "													UNION SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "													FROM tbl_Emu_Games_Moby_Genres EGMG" & ControlChars.CrLf
		sSQL &= "													WHERE EGMG.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "													UNION SELECT id_Moby_Genres FROM tbl_Moby_Games_Genres GGC" & ControlChars.CrLf
		sSQL &= "													WHERE GGC.id_Moby_Games = MGC.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "												) AS temp_Genres" & ControlChars.CrLf
		sSQL &= "												INNER JOIN tbl_Moby_Genres G" & ControlChars.CrLf
		sSQL &= "												ON	temp_Genres.id_Moby_Genres = G.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "														AND G.id_Moby_Genres_Categories = " & TC.getSQLFormat(id_Moby_Genres_Categories) & ControlChars.CrLf
		sSQL &= "											)" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "" & ControlChars.CrLf
		sSQL &= "										AS FLOAT" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "									* 100" & ControlChars.CrLf
		sSQL &= "									AS INTEGER" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							END" & ControlChars.CrLf
		sSQL &= "							AS " & ColumnName & ControlChars.CrLf
		Return sSQL
	End Function

	Public Shared Sub Fill_tbl_Similarity_Calculation(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As tbl_Similarity_CalculationDataTable, ByVal id_Emu_Games As Integer, ByVal id_Similarity_Calculation_Config As Integer, ByVal Only_Show_Haves As Boolean)
		dt.Clear()

		Dim sSQL As String = ""
		sSQL &= "-- Cleanup Temp. Tables" & ControlChars.CrLf
		sSQL &= "DELETE FROM ttb_Emu_Games_Similarity_Calculation;" & ControlChars.CrLf
		sSQL &= "DELETE FROM ttb_Moby_Releases_Similarity_Calculation;" & ControlChars.CrLf
		sSQL &= "" & ControlChars.CrLf
		sSQL &= "-- Calculate Similarity for Emu_Games" & ControlChars.CrLf
		sSQL &= "INSERT INTO ttb_Emu_Games_Similarity_Calculation" & ControlChars.CrLf
		sSQL &= "(" & ControlChars.CrLf
		sSQL &= "				id_Emu_Games" & ControlChars.CrLf
		sSQL &= "				, id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "				, [001_Platform]" & ControlChars.CrLf
		sSQL &= "				, [002_MobyRank]" & ControlChars.CrLf
		sSQL &= "				, [003_MobyScore]" & ControlChars.CrLf
		sSQL &= "				, [004_Publisher]" & ControlChars.CrLf
		sSQL &= "				, [005_Developer]" & ControlChars.CrLf
		sSQL &= "				, [006_Year]" & ControlChars.CrLf
		sSQL &= "				, [101_Basic_Genres]" & ControlChars.CrLf
		sSQL &= "				, [102_Perspectives]" & ControlChars.CrLf
		sSQL &= "				, [103_Sports_Themes]" & ControlChars.CrLf
		sSQL &= "				, [105_Educational_Categories]" & ControlChars.CrLf
		sSQL &= "				, [106_Other_Attributes]" & ControlChars.CrLf
		sSQL &= "				, [107_Visual_Presentation]" & ControlChars.CrLf
		sSQL &= "				, [108_Gameplay]" & ControlChars.CrLf
		sSQL &= "				, [109_Pacing]" & ControlChars.CrLf
		sSQL &= "				, [110_Narrative_Theme_Topic]" & ControlChars.CrLf
		sSQL &= "				, [111_Setting]" & ControlChars.CrLf
		sSQL &= "				, [112_Vehicular_Themes]" & ControlChars.CrLf
		sSQL &= "				, [113_Interface_Control]" & ControlChars.CrLf
		sSQL &= "				, [114_DLC_Addon]" & ControlChars.CrLf
		sSQL &= "				, [115_Special_Edition]" & ControlChars.CrLf
		sSQL &= "				, [201_MinPlayers]" & ControlChars.CrLf
		sSQL &= "				, [202_MaxPlayers]" & ControlChars.CrLf
		sSQL &= "				, [203_AgeO]" & ControlChars.CrLf
		sSQL &= "				, [204_AgeP]" & ControlChars.CrLf
		sSQL &= "				, [205_Rating_Descriptors]" & ControlChars.CrLf
		sSQL &= "				, [206_Other_Attributes]" & ControlChars.CrLf
		sSQL &= "				, [207_Multiplayer_Attributes]" & ControlChars.CrLf
		sSQL &= "				, [301_Group_Membership]" & ControlChars.CrLf
		sSQL &= "				, [401_Staff]" & ControlChars.CrLf
		sSQL &= "				, Similarity" & ControlChars.CrLf
		sSQL &= ")" & ControlChars.CrLf
		sSQL &= "SELECT		" & ControlChars.CrLf
		sSQL &= "				SubSim.id_Emu_Games AS id_Emu_Games" & ControlChars.CrLf
		sSQL &= "				, SubSim.id_Moby_Releases AS id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "				, SubSim.[001_Platform] AS [001_Platform]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[002_MobyRank] AS [002_MobyRank]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[003_MobyScore] AS [003_MobyScore]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[004_Publisher] AS [004_Publisher]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[005_Developer] AS [005_Developer]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[006_Year] AS [006_Year]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[101_Basic_Genres] AS [101_Basic_Genres]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[102_Perspectives] AS [102_Perspectives]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[103_Sports_Themes] AS [103_Sports_Themes]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[105_Educational_Categories] AS [105_Educational_Categories]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[106_Other_Attributes] AS [106_Other_Attributes]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[107_Visual_Presentation] AS [107_Visual_Presentation]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[108_Gameplay] AS [108_Gameplay]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[109_Pacing] AS [109_Pacing]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[110_Narrative_Theme_Topic] AS [110_Narrative_Theme_Topic]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[111_Setting] AS [111_Setting]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[112_Vehicular_Themes] AS [112_Vehicular_Themes]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[113_Interface_Control] AS [113_Interface_Control]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[114_DLC_Addon] AS [114_DLC_Addon]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[115_Special_Edition] AS [115_Special_Edition]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[201_MinPlayers] AS [201_MinPlayers]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[202_MaxPlayers] AS [202_MaxPlayers]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[203_AgeO] AS [203_AgeO]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[204_AgeP] AS [204_AgeP]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[205_Rating_Descriptors] AS [205_Rating_Descriptors]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[206_Other_Attributes] AS [206_Other_Attributes]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[207_Multiplayer_Attributes] AS [207_Multiplayer_Attributes]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[301_Group_Membership] AS [301_Group_Membership]" & ControlChars.CrLf
		sSQL &= "				, SubSim.[401_Staff] AS [401_Staff]" & ControlChars.CrLf
		sSQL &= "				, CAST" & ControlChars.CrLf
		sSQL &= "					(" & ControlChars.CrLf
		sSQL &= "						CAST" & ControlChars.CrLf
		sSQL &= "						(" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[001_Platform], 0) * IFNULL(SimConf.[Weight_001_Platform], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[002_MobyRank], 0) * IFNULL(SimConf.[Weight_002_MobyRank], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[003_MobyScore], 0) * IFNULL(SimConf.[Weight_003_MobyScore], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[004_Publisher], 0) * IFNULL(SimConf.[Weight_004_Publisher], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[005_Developer], 0) * IFNULL(SimConf.[Weight_005_Developer], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[006_Year], 0) * IFNULL(SimConf.[Weight_006_Year], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[101_Basic_Genres], 0) * IFNULL(SimConf.[Weight_101_Basic_Genres], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[102_Perspectives], 0) * IFNULL(SimConf.[Weight_102_Perspectives], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[103_Sports_Themes], 0) * IFNULL(SimConf.[Weight_103_Sports_Themes], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[105_Educational_Categories], 0) * IFNULL(SimConf.[Weight_105_Educational_Categories], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[106_Other_Attributes], 0) * IFNULL(SimConf.[Weight_106_Other_Attributes], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[107_Visual_Presentation], 0) * IFNULL(SimConf.[Weight_107_Visual_Presentation], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[108_Gameplay], 0) * IFNULL(SimConf.[Weight_108_Gameplay], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[109_Pacing], 0) * IFNULL(SimConf.[Weight_109_Pacing], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[110_Narrative_Theme_Topic], 0) * IFNULL(SimConf.[Weight_110_Narrative_Theme_Topic], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[111_Setting], 0) * IFNULL(SimConf.[Weight_111_Setting], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[112_Vehicular_Themes], 0) * IFNULL(SimConf.[Weight_112_Vehicular_Themes], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[113_Interface_Control], 0) * IFNULL(SimConf.[Weight_113_Interface_Control], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[114_DLC_Addon], 0) * IFNULL(SimConf.[Weight_114_DLC_Addon], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[115_Special_Edition], 0) * IFNULL(SimConf.[Weight_115_Special_Edition], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[201_MinPlayers], 0) * IFNULL(SimConf.[Weight_201_MinPlayers], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[202_MaxPlayers], 0) * IFNULL(SimConf.[Weight_202_MaxPlayers], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[203_AgeO], 0) * IFNULL(SimConf.[Weight_203_AgeO], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[204_AgeP], 0) * IFNULL(SimConf.[Weight_204_AgeP], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[205_Rating_Descriptors], 0) * IFNULL(SimConf.[Weight_205_Rating_Descriptors], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[206_Other_Attributes], 0) * IFNULL(SimConf.[Weight_206_Other_Attributes], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[207_Multiplayer_Attributes], 0) * IFNULL(SimConf.[Weight_207_Multiplayer_Attributes], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[301_Group_Membership], 0) * IFNULL(SimConf.[Weight_301_Group_Membership], 1)" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								IFNULL(SubSim.[401_Staff], 0) * IFNULL(SimConf.[Weight_401_Staff], 1)" & ControlChars.CrLf
		sSQL &= "							)" & ControlChars.CrLf
		sSQL &= "							AS FLOAT" & ControlChars.CrLf
		sSQL &= "						)" & ControlChars.CrLf
		sSQL &= "" & ControlChars.CrLf
		sSQL &= "						/" & ControlChars.CrLf
		sSQL &= "" & ControlChars.CrLf
		sSQL &= "						CAST" & ControlChars.CrLf
		sSQL &= "						(	MAX" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[001_Platform] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_001_Platform], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[002_MobyRank] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_002_MobyRank], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[003_MobyScore] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_003_MobyScore], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[004_Publisher] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_004_Publisher], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[005_Developer] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_005_Developer], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[006_Year] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_006_Year], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[101_Basic_Genres] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_101_Basic_Genres], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[102_Perspectives] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_102_Perspectives], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[103_Sports_Themes] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_103_Sports_Themes], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[105_Educational_Categories] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_105_Educational_Categories], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[106_Other_Attributes] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_106_Other_Attributes], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[107_Visual_Presentation] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_107_Visual_Presentation], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[108_Gameplay] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_108_Gameplay], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[109_Pacing] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_109_Pacing], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[110_Narrative_Theme_Topic] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_110_Narrative_Theme_Topic], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[111_Setting] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_111_Setting], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[112_Vehicular_Themes] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_112_Vehicular_Themes], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[113_Interface_Control] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_113_Interface_Control], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[114_DLC_Addon] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_114_DLC_Addon], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[115_Special_Edition] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_115_Special_Edition], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[201_MinPlayers] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_201_MinPlayers], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[202_MaxPlayers] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_202_MaxPlayers], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[203_AgeO] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_203_AgeO], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[204_AgeP] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_204_AgeP], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[205_Rating_Descriptors] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_205_Rating_Descriptors], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[206_Other_Attributes] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_206_Other_Attributes], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[207_Multiplayer_Attributes] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_207_Multiplayer_Attributes], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[301_Group_Membership] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_301_Group_Membership], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								+" & ControlChars.CrLf
		sSQL &= "								CASE WHEN SubSim.[401_Staff] IS NULL THEN 0 ELSE IFNULL(SimConf.[Weight_401_Staff], 1) END * 100" & ControlChars.CrLf
		sSQL &= "								, 1" & ControlChars.CrLf
		sSQL &= "							)" & ControlChars.CrLf
		sSQL &= "							AS FLOAT" & ControlChars.CrLf
		sSQL &= "						)" & ControlChars.CrLf
		sSQL &= "" & ControlChars.CrLf
		sSQL &= "						* 100" & ControlChars.CrLf
		sSQL &= "" & ControlChars.CrLf
		sSQL &= "						AS INTEGER" & ControlChars.CrLf
		sSQL &= "					)" & ControlChars.CrLf
		sSQL &= "					AS [Similarity]" & ControlChars.CrLf
		sSQL &= "FROM" & ControlChars.CrLf
		sSQL &= "(" & ControlChars.CrLf
		sSQL &= "	SELECT		EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "						, MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "						, CASE	WHEN IFNULL(EG.id_Moby_Platforms_Alternative, EG.id_Moby_Platforms) = IFNULL(EGC.id_Moby_Platforms_Alternative, EGC.id_Moby_Platforms) THEN 100 ELSE 0 END AS [001_Platform]" & ControlChars.CrLf
		sSQL &= "						, CASE	WHEN MR.MobyRank IS NOT NULL OR MRC.MobyRank IS NOT NULL" & ControlChars.CrLf
		sSQL &= "										THEN 100 - ABS(IFNULL(MR.MobyRank, 0) - IFNULL(MRC.MobyRank, 0))" & ControlChars.CrLf
		sSQL &= "										ELSE NULL" & ControlChars.CrLf
		sSQL &= "							END" & ControlChars.CrLf
		sSQL &= "							AS [002_MobyRank]" & ControlChars.CrLf
		sSQL &= "						, CASE	WHEN MR.MobyScore IS NOT NULL OR MRC.MobyScore IS NOT NULL" & ControlChars.CrLf
		sSQL &= "										THEN CAST(100 - ABS(20.0*IFNULL(MR.MobyScore, 0) - 20.0*IFNULL(MRC.MobyScore, 0)) AS INTEGER)" & ControlChars.CrLf
		sSQL &= "										ELSE NULL" & ControlChars.CrLf
		sSQL &= "							END AS [003_MobyScore]" & ControlChars.CrLf
		sSQL &= "						, CASE	WHEN IFNULL(EG.Publisher, C1.Name) IS NULL AND IFNULL(EGC.Publisher, C1C.Name) IS NULL THEN NULL" & ControlChars.CrLf
		sSQL &= "										WHEN IFNULL(EG.Publisher, C1.Name) = IFNULL(EGC.Publisher, C1C.Name) THEN 100" & ControlChars.CrLf
		sSQL &= "										ELSE 0" & ControlChars.CrLf
		sSQL &= "							END AS [004_Publisher]" & ControlChars.CrLf
		sSQL &= "						, CASE	WHEN IFNULL(EG.Developer, C2.Name) IS NULL AND IFNULL(EGC.Developer, C2C.Name) IS NULL THEN NULL" & ControlChars.CrLf
		sSQL &= "										WHEN IFNULL(EG.Developer, C2.Name) = IFNULL(EGC.Developer, C2C.Name) THEN 100" & ControlChars.CrLf
		sSQL &= "										ELSE 0 " & ControlChars.CrLf
		sSQL &= "							END AS [005_Developer]" & ControlChars.CrLf
		sSQL &= "						, CASE	WHEN (IFNULL(EG.Year, MR.Year) IS NULL OR IFNULL(EG.Year, MR.Year) < 1950) AND (IFNULL(EGC.Year, MRC.Year) IS NULL OR IFNULL(EGC.Year, MRC.Year) < 1950) THEN NULL" & ControlChars.CrLf
		sSQL &= "										ELSE MAX(100 - (10 * CAST(ABS(IFNULL(EG.Year, MR.Year) - IFNULL(EGC.Year, MRC.Year)) / 2 AS INTEGER)), 0)" & ControlChars.CrLf
		sSQL &= "							END AS [006_Year]" & ControlChars.CrLf

		sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_Genre(cls_Globals.enm_Moby_Genres_Categories.Basic_Genres, "[101_Basic_Genres]")
		sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_Genre(cls_Globals.enm_Moby_Genres_Categories.Perspective, "[102_Perspectives]")
		sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_Genre(cls_Globals.enm_Moby_Genres_Categories.Sports_Themes, "[103_Sports_Themes]")
		sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_Genre(cls_Globals.enm_Moby_Genres_Categories.Educational_Categories, "[105_Educational_Categories]")
		sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_Genre(cls_Globals.enm_Moby_Genres_Categories.Other_Attributes, "[106_Other_Attributes]")
		sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_Genre(cls_Globals.enm_Moby_Genres_Categories.Visual_Presentation, "[107_Visual_Presentation]")
		sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_Genre(cls_Globals.enm_Moby_Genres_Categories.Gameplay, "[108_Gameplay]")
		sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_Genre(cls_Globals.enm_Moby_Genres_Categories.Pacing, "[109_Pacing]")
		sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_Genre(cls_Globals.enm_Moby_Genres_Categories.Narrative_Theme_Topic, "[110_Narrative_Theme_Topic]")
		sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_Genre(cls_Globals.enm_Moby_Genres_Categories.Setting, "[111_Setting]")
		sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_Genre(cls_Globals.enm_Moby_Genres_Categories.Vehicular_Themes, "[112_Vehicular_Themes]")
		sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_Genre(cls_Globals.enm_Moby_Genres_Categories.Interface_Control, "[113_Interface_Control]")
		sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_Genre(cls_Globals.enm_Moby_Genres_Categories.DLC_Addon, "[114_DLC_Addon]")
		sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_Genre(cls_Globals.enm_Moby_Genres_Categories.Special_Edition, "[115_Special_Edition]")

		sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT MIN(A.MinPlayers) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "									WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "									WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "								) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "								WHERE A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) IS NULL" & ControlChars.CrLf
		sSQL &= "							OR" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT MIN(AC.MinPlayers) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
		sSQL &= "									WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMAC" & ControlChars.CrLf
		sSQL &= "									WHERE EGMAC.id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "								) AS temp_AttributesC" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "								WHERE AC.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									WHERE id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) IS NULL" & ControlChars.CrLf
		sSQL &= "							THEN NULL" & ControlChars.CrLf
		sSQL &= "							ELSE" & ControlChars.CrLf
		sSQL &= "							MAX(0," & ControlChars.CrLf
		sSQL &= "								100 - " & ControlChars.CrLf
		sSQL &= "								10 * ABS" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										SELECT MIN(A.MinPlayers) FROM" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "											WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "											UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "											WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "										) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "										LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "										WHERE A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "											AND Used = 0" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "									-" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										SELECT MIN(AC.MinPlayers) FROM" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
		sSQL &= "											WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "											UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes EGMAC" & ControlChars.CrLf
		sSQL &= "											WHERE EGMAC.id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "										) AS temp_AttributesC" & ControlChars.CrLf
		sSQL &= "										LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "										WHERE AC.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											WHERE id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "											AND Used = 0" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							)" & ControlChars.CrLf
		sSQL &= "							END" & ControlChars.CrLf
		sSQL &= "							AS [201_MinPlayers]" & ControlChars.CrLf
		sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT MAX(A.MaxPlayers) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "									WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "									WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "								) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "								WHERE A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) IS NULL" & ControlChars.CrLf
		sSQL &= "							AND" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT MAX(AC.MaxPlayers) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
		sSQL &= "									WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMAC" & ControlChars.CrLf
		sSQL &= "									WHERE EGMAC.id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "								) AS temp_AttributesC" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "								WHERE AC.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									WHERE id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) IS NULL" & ControlChars.CrLf
		sSQL &= "							THEN NULL" & ControlChars.CrLf
		sSQL &= "							ELSE" & ControlChars.CrLf
		sSQL &= "							MAX(0," & ControlChars.CrLf
		sSQL &= "								100 - " & ControlChars.CrLf
		sSQL &= "								10 * ABS" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										SELECT MAX(A.MaxPlayers) FROM" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "											WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "											UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "											WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "										) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "										LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "										WHERE A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "											AND Used = 0" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "									-" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										SELECT MAX(AC.MaxPlayers) FROM" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
		sSQL &= "											WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "											UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes EGMAC" & ControlChars.CrLf
		sSQL &= "											WHERE EGMAC.id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "										) AS temp_AttributesC" & ControlChars.CrLf
		sSQL &= "										LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "										WHERE AC.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											WHERE id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "											AND Used = 0" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							)" & ControlChars.CrLf
		sSQL &= "							END" & ControlChars.CrLf
		sSQL &= "							AS [202_MaxPlayers]" & ControlChars.CrLf
		sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT MIN(A.Rating_Age_From) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "									WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "									WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "								) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "								WHERE A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) IS NULL" & ControlChars.CrLf
		sSQL &= "							OR" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT MIN(AC.Rating_Age_From) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
		sSQL &= "									WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMAC" & ControlChars.CrLf
		sSQL &= "									WHERE EGMAC.id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "								) AS temp_AttributesC" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "								WHERE AC.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									WHERE id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) IS NULL" & ControlChars.CrLf
		sSQL &= "							THEN NULL" & ControlChars.CrLf
		sSQL &= "							ELSE" & ControlChars.CrLf
		sSQL &= "							MAX(0," & ControlChars.CrLf
		sSQL &= "								100 - " & ControlChars.CrLf
		sSQL &= "								10 * ABS" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										SELECT MIN(A.Rating_Age_From) FROM" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "											WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "											UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "											WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "										) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "										LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "										WHERE A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "											AND Used = 0" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "									-" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										SELECT MIN(AC.Rating_Age_From) FROM" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
		sSQL &= "											WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "											UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes EGMAC" & ControlChars.CrLf
		sSQL &= "											WHERE EGMAC.id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "										) AS temp_AttributesC" & ControlChars.CrLf
		sSQL &= "										LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "										WHERE AC.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											WHERE id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "											AND Used = 0" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							)" & ControlChars.CrLf
		sSQL &= "							END" & ControlChars.CrLf
		sSQL &= "							AS [203_AgeO]" & ControlChars.CrLf
		sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT MAX(A.Rating_Age_From) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "									WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "									WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "								) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "								WHERE A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) IS NULL" & ControlChars.CrLf
		sSQL &= "							OR" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT MAX(AC.Rating_Age_From) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
		sSQL &= "									WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMAC" & ControlChars.CrLf
		sSQL &= "									WHERE EGMAC.id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "								) AS temp_AttributesC" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "								WHERE AC.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									WHERE id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) IS NULL" & ControlChars.CrLf
		sSQL &= "							THEN NULL" & ControlChars.CrLf
		sSQL &= "							ELSE" & ControlChars.CrLf
		sSQL &= "							MAX(0," & ControlChars.CrLf
		sSQL &= "								100 - " & ControlChars.CrLf
		sSQL &= "								10 * ABS" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										SELECT MAX(A.Rating_Age_From) FROM" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "											WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "											UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "											WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "										) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "										LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "										WHERE A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "											AND Used = 0" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "									-" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										SELECT MAX(AC.Rating_Age_From) FROM" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
		sSQL &= "											WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "											UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes EGMAC" & ControlChars.CrLf
		sSQL &= "											WHERE EGMAC.id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "										) AS temp_AttributesC" & ControlChars.CrLf
		sSQL &= "										LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "										WHERE AC.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											WHERE id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "											AND Used = 0" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							)" & ControlChars.CrLf
		sSQL &= "							END" & ControlChars.CrLf
		sSQL &= "							AS [204_AgeP]" & ControlChars.CrLf
		sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "									WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "									WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "								) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf
		sSQL &= "								WHERE	AC.RatingDescriptor = 1" & ControlChars.CrLf
		sSQL &= "								AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) = 0" & ControlChars.CrLf
		sSQL &= "							AND" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
		sSQL &= "									WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMAC" & ControlChars.CrLf
		sSQL &= "									WHERE EGMAC.id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "								) AS temp_AttributesC" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes_Categories ACC ON AC.id_Moby_Attributes_Categories = ACC.id_Moby_Attributes_Categories" & ControlChars.CrLf
		sSQL &= "								WHERE	ACC.RatingDescriptor = 1" & ControlChars.CrLf
		sSQL &= "								AND AC.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									WHERE id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) = 0" & ControlChars.CrLf
		sSQL &= "							THEN NULL" & ControlChars.CrLf
		sSQL &= "							ELSE" & ControlChars.CrLf
		sSQL &= "								CAST" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									CAST" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "												WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "												UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "												WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "											) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "											LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf
		sSQL &= "											WHERE	AC.RatingDescriptor = 1" & ControlChars.CrLf
		sSQL &= "											AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												AND Used = 0" & ControlChars.CrLf
		sSQL &= "											)" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "										AS FLOAT" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "									/" & ControlChars.CrLf
		sSQL &= "									CAST" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "												WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "												UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "												WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												UNION SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
		sSQL &= "												WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "												UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes EGMAC" & ControlChars.CrLf
		sSQL &= "												WHERE EGMAC.id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "											) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "											LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf
		sSQL &= "											WHERE	AC.RatingDescriptor = 1" & ControlChars.CrLf
		sSQL &= "											AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												AND Used = 0" & ControlChars.CrLf
		sSQL &= "												INTERSECT" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												WHERE id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												AND Used = 0" & ControlChars.CrLf
		sSQL &= "											)" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "										AS FLOAT" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "									* 100" & ControlChars.CrLf
		sSQL &= "									AS INTEGER" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							END" & ControlChars.CrLf
		sSQL &= "							AS [205_Rating_Descriptors]" & ControlChars.CrLf
		sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "									WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "									WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "								) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf
		sSQL &= "								WHERE	IFNULL(AC.RatingDescriptor, 0) = 0 AND IFNULL(AC.RatingSystem, 0) = 0 AND IFNULL(AC.NumPlayersCategory, 0) = 0 AND AC.id_Moby_Attributes_Categories NOT IN (12, 16)" & ControlChars.CrLf
		sSQL &= "								AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) = 0" & ControlChars.CrLf
		sSQL &= "							AND" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
		sSQL &= "									WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMAC" & ControlChars.CrLf
		sSQL &= "									WHERE EGMAC.id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "								) AS temp_AttributesC" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes_Categories ACC ON AC.id_Moby_Attributes_Categories = ACC.id_Moby_Attributes_Categories" & ControlChars.CrLf
		sSQL &= "								WHERE	IFNULL(ACC.RatingDescriptor, 0) = 0 AND IFNULL(ACC.RatingSystem, 0) = 0 AND IFNULL(ACC.NumPlayersCategory, 0) = 0 AND AC.id_Moby_Attributes_Categories NOT IN (12, 16)" & ControlChars.CrLf
		sSQL &= "								AND AC.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									WHERE id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) = 0" & ControlChars.CrLf
		sSQL &= "							THEN NULL" & ControlChars.CrLf
		sSQL &= "							ELSE" & ControlChars.CrLf
		sSQL &= "								CAST" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									CAST" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "												WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "												UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "												WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "											) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "											LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf
		sSQL &= "											WHERE	IFNULL(AC.RatingDescriptor, 0) = 0 AND IFNULL(AC.RatingSystem, 0) = 0 AND IFNULL(AC.NumPlayersCategory, 0) = 0 AND AC.id_Moby_Attributes_Categories NOT IN (12, 16)" & ControlChars.CrLf
		sSQL &= "											AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												AND Used = 0" & ControlChars.CrLf
		sSQL &= "											)" & ControlChars.CrLf
		sSQL &= "											AND A.id_Moby_Attributes IN" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM" & ControlChars.CrLf
		sSQL &= "												(" & ControlChars.CrLf
		sSQL &= "													SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
		sSQL &= "													WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "													UNION" & ControlChars.CrLf
		sSQL &= "													SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "													FROM tbl_Emu_Games_Moby_Attributes EGMAC" & ControlChars.CrLf
		sSQL &= "													WHERE EGMAC.id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												) AS temp_AttributesC" & ControlChars.CrLf
		sSQL &= "												WHERE temp_AttributesC.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "												(" & ControlChars.CrLf
		sSQL &= "													SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "													FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "													WHERE id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "													AND Used = 0" & ControlChars.CrLf
		sSQL &= "												)" & ControlChars.CrLf
		sSQL &= "											)" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "										AS FLOAT" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "									/" & ControlChars.CrLf
		sSQL &= "									CAST" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "												WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "												UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "												WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												UNION SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
		sSQL &= "												WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "												UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes EGMAC" & ControlChars.CrLf
		sSQL &= "												WHERE EGMAC.id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "											) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "											LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf
		sSQL &= "											WHERE	IFNULL(AC.RatingDescriptor, 0) = 0 AND IFNULL(AC.RatingSystem, 0) = 0 AND IFNULL(AC.NumPlayersCategory, 0) = 0 AND AC.id_Moby_Attributes_Categories NOT IN (12, 16)" & ControlChars.CrLf
		sSQL &= "											AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												AND Used = 0" & ControlChars.CrLf
		sSQL &= "												INTERSECT" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												WHERE id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												AND Used = 0" & ControlChars.CrLf
		sSQL &= "											)" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "										AS FLOAT" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "									* 100" & ControlChars.CrLf
		sSQL &= "									AS INTEGER" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							END" & ControlChars.CrLf
		sSQL &= "							AS [206_Other_Attributes]" & ControlChars.CrLf
		sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "									WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "									WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "								) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf
		sSQL &= "								WHERE	AC.id_Moby_Attributes_Categories IN (12, 16)" & ControlChars.CrLf
		sSQL &= "								AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) = 0" & ControlChars.CrLf
		sSQL &= "							AND" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
		sSQL &= "									WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMAC" & ControlChars.CrLf
		sSQL &= "									WHERE EGMAC.id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "								) AS temp_AttributesC" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "								LEFT JOIN tbl_Moby_Attributes_Categories ACC ON AC.id_Moby_Attributes_Categories = ACC.id_Moby_Attributes_Categories" & ControlChars.CrLf
		sSQL &= "								WHERE	AC.id_Moby_Attributes_Categories IN (12, 16)" & ControlChars.CrLf
		sSQL &= "								AND AC.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									WHERE id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) = 0" & ControlChars.CrLf
		sSQL &= "							THEN NULL" & ControlChars.CrLf
		sSQL &= "							ELSE" & ControlChars.CrLf
		sSQL &= "								CAST" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									CAST" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "												WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "												UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "												WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "											) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "											LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf
		sSQL &= "											WHERE	AC.id_Moby_Attributes_Categories IN (12, 16)" & ControlChars.CrLf
		sSQL &= "											AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												AND Used = 0" & ControlChars.CrLf
		sSQL &= "											)" & ControlChars.CrLf
		sSQL &= "											AND A.id_Moby_Attributes IN" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM" & ControlChars.CrLf
		sSQL &= "												(" & ControlChars.CrLf
		sSQL &= "													SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
		sSQL &= "													WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "													UNION" & ControlChars.CrLf
		sSQL &= "													SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "													FROM tbl_Emu_Games_Moby_Attributes EGMAC" & ControlChars.CrLf
		sSQL &= "													WHERE EGMAC.id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												) AS temp_AttributesC" & ControlChars.CrLf
		sSQL &= "												WHERE temp_AttributesC.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "												(" & ControlChars.CrLf
		sSQL &= "													SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "													FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "													WHERE id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "													AND Used = 0" & ControlChars.CrLf
		sSQL &= "												)" & ControlChars.CrLf
		sSQL &= "											)" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "										AS FLOAT" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "									/" & ControlChars.CrLf
		sSQL &= "									CAST" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "												WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "												UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "												WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												UNION SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
		sSQL &= "												WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "												UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes EGMAC" & ControlChars.CrLf
		sSQL &= "												WHERE EGMAC.id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "											) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "											LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "											LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf
		sSQL &= "											WHERE	AC.id_Moby_Attributes_Categories IN (12, 16)" & ControlChars.CrLf
		sSQL &= "											AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												AND Used = 0" & ControlChars.CrLf
		sSQL &= "												INTERSECT" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "												WHERE id_Emu_Games = EGC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												AND Used = 0" & ControlChars.CrLf
		sSQL &= "											)" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "										AS FLOAT" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "									* 100" & ControlChars.CrLf
		sSQL &= "									AS INTEGER" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							END" & ControlChars.CrLf
		sSQL &= "							AS [207_Multiplayer_Attributes]" & ControlChars.CrLf
		sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT DISTINCT id_Moby_Game_Groups FROM tbl_Moby_Game_Groups_Moby_Releases MGGMR" & ControlChars.CrLf
		sSQL &= "									WHERE MGGMR.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) = 0" & ControlChars.CrLf
		sSQL &= "							AND" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT DISTINCT id_Moby_Game_Groups FROM tbl_Moby_Game_Groups_Moby_Releases MGGMRC" & ControlChars.CrLf
		sSQL &= "									WHERE MGGMRC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) = 0" & ControlChars.CrLf
		sSQL &= "							THEN NULL" & ControlChars.CrLf
		sSQL &= "							ELSE" & ControlChars.CrLf
		sSQL &= "								CAST" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									CAST" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT DISTINCT id_Moby_Game_Groups FROM tbl_Moby_Game_Groups_Moby_Releases MGGMR" & ControlChars.CrLf
		sSQL &= "												WHERE MGGMR.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "												INTERSECT SELECT DISTINCT id_Moby_Game_Groups FROM tbl_Moby_Game_Groups_Moby_Releases MGGMRC" & ControlChars.CrLf
		sSQL &= "												WHERE MGGMRC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "											)" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "										AS FLOAT" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "									/" & ControlChars.CrLf
		sSQL &= "									CAST" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT id_Moby_Game_Groups FROM tbl_Moby_Game_Groups_Moby_Releases MGGMR" & ControlChars.CrLf
		sSQL &= "												WHERE MGGMR.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "												UNION SELECT id_Moby_Game_Groups FROM tbl_Moby_Game_Groups_Moby_Releases MGGMRC" & ControlChars.CrLf
		sSQL &= "												WHERE MGGMRC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "											)" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "										AS FLOAT" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "									* 100" & ControlChars.CrLf
		sSQL &= "									AS INTEGER" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							END" & ControlChars.CrLf
		sSQL &= "							AS [301_Group_Membership]" & ControlChars.CrLf
		sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT DISTINCT id_Moby_Staff FROM tbl_Moby_Releases_Staff MRS" & ControlChars.CrLf
		sSQL &= "									WHERE MRS.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) = 0" & ControlChars.CrLf
		sSQL &= "							AND" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT DISTINCT id_Moby_Staff FROM tbl_Moby_Releases_Staff MRSC" & ControlChars.CrLf
		sSQL &= "									WHERE MRSC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							) = 0" & ControlChars.CrLf
		sSQL &= "							THEN NULL" & ControlChars.CrLf
		sSQL &= "							ELSE" & ControlChars.CrLf
		sSQL &= "								CAST" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									CAST" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT DISTINCT id_Moby_Staff FROM tbl_Moby_Releases_Staff MRS" & ControlChars.CrLf
		sSQL &= "												WHERE MRS.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "												INTERSECT SELECT DISTINCT id_Moby_Staff FROM tbl_Moby_Releases_Staff MRSC" & ControlChars.CrLf
		sSQL &= "												WHERE MRSC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "" & ControlChars.CrLf
		sSQL &= "											)" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "										AS FLOAT" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "									/" & ControlChars.CrLf
		sSQL &= "									CAST" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										(" & ControlChars.CrLf
		sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
		sSQL &= "											(" & ControlChars.CrLf
		sSQL &= "												SELECT DISTINCT id_Moby_Staff FROM tbl_Moby_Releases_Staff MRS" & ControlChars.CrLf
		sSQL &= "												WHERE MRS.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "												UNION SELECT DISTINCT id_Moby_Staff FROM tbl_Moby_Releases_Staff MRSC" & ControlChars.CrLf
		sSQL &= "												WHERE MRSC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "											)" & ControlChars.CrLf
		sSQL &= "										)" & ControlChars.CrLf
		sSQL &= "										AS FLOAT" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "									* 100" & ControlChars.CrLf
		sSQL &= "									AS INTEGER" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "							END" & ControlChars.CrLf
		sSQL &= "							AS [401_Staff]" & ControlChars.CrLf
		sSQL &= "	FROM			tbl_Emu_Games EG" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Games MG ON EG.Moby_Games_URLPart = MG.URLPart" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Releases MR ON IFNULL(EG.id_Moby_Platforms_Alternative, EG.id_Moby_Platforms) = MR.id_Moby_Platforms AND MG.id_Moby_Games = MR.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Companies C1 ON MR.Publisher_id_Moby_Companies = C1.id_Moby_Companies" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Companies C2 ON MR.Developer_id_Moby_Companies = C2.id_Moby_Companies" & ControlChars.CrLf
		sSQL &= "" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Emu_Games EGC ON EGC.id_Emu_Games_Owner IS NULL AND EGC.id_Emu_Games > 0" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Games MGC ON EGC.Moby_Games_URLPart = MGC.URLPart" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Releases MRC ON IFNULL(EGC.id_Moby_Platforms_Alternative, EGC.id_Moby_Platforms) = MRC.id_Moby_Platforms AND MGC.id_Moby_Games = MRC.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Companies C1C ON MRC.Publisher_id_Moby_Companies = C1C.id_Moby_Companies" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Companies C2C ON MRC.Developer_id_Moby_Companies = C2C.id_Moby_Companies" & ControlChars.CrLf
		sSQL &= "" & ControlChars.CrLf
		sSQL &= "	WHERE		EG.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " -- variable" & ControlChars.CrLf
		sSQL &= ") SubSim" & ControlChars.CrLf
		sSQL &= "LEFT JOIN tbl_Similarity_Calculation_Config SimConf ON SimConf.id_Similarity_Calculation_Config = " & TC.getSQLFormat(id_Similarity_Calculation_Config) & " -- variable" & ControlChars.CrLf
		sSQL &= ";" & ControlChars.CrLf

		If Not Only_Show_Haves Then
			sSQL &= "" & ControlChars.CrLf
			sSQL &= "-- Calculate Similarity for Moby Releases (that are not already calculated through Emu Games)" & ControlChars.CrLf
			sSQL &= "	INSERT INTO ttb_Moby_Releases_Similarity_Calculation" & ControlChars.CrLf
			sSQL &= "	(" & ControlChars.CrLf
			sSQL &= "					id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "					, [001_Platform]" & ControlChars.CrLf
			sSQL &= "					, [002_MobyRank]" & ControlChars.CrLf
			sSQL &= "					, [003_MobyScore]" & ControlChars.CrLf
			sSQL &= "					, [004_Publisher]" & ControlChars.CrLf
			sSQL &= "					, [005_Developer]" & ControlChars.CrLf
			sSQL &= "					, [006_Year]" & ControlChars.CrLf
			sSQL &= "					, [101_Basic_Genres]" & ControlChars.CrLf
			sSQL &= "					, [102_Perspectives]" & ControlChars.CrLf
			sSQL &= "					, [103_Sports_Themes]" & ControlChars.CrLf
			sSQL &= "					, [105_Educational_Categories]" & ControlChars.CrLf
			sSQL &= "					, [106_Other_Attributes]" & ControlChars.CrLf
			sSQL &= "					, [107_Visual_Presentation]" & ControlChars.CrLf
			sSQL &= "					, [108_Gameplay]" & ControlChars.CrLf
			sSQL &= "					, [109_Pacing]" & ControlChars.CrLf
			sSQL &= "					, [110_Narrative_Theme_Topic]" & ControlChars.CrLf
			sSQL &= "					, [111_Setting]" & ControlChars.CrLf
			sSQL &= "					, [112_Vehicular_Themes]" & ControlChars.CrLf
			sSQL &= "					, [113_Interface_Control]" & ControlChars.CrLf
			sSQL &= "					, [114_DLC_Addon]" & ControlChars.CrLf
			sSQL &= "					, [115_Special_Edition]" & ControlChars.CrLf
			sSQL &= "					, [201_MinPlayers]" & ControlChars.CrLf
			sSQL &= "					, [202_MaxPlayers]" & ControlChars.CrLf
			sSQL &= "					, [203_AgeO]" & ControlChars.CrLf
			sSQL &= "					, [204_AgeP]" & ControlChars.CrLf
			sSQL &= "					, [205_Rating_Descriptors]" & ControlChars.CrLf
			sSQL &= "					, [206_Other_Attributes]" & ControlChars.CrLf
			sSQL &= "					, [207_Multiplayer_Attributes]" & ControlChars.CrLf
			sSQL &= "					, [301_Group_Membership]" & ControlChars.CrLf
			sSQL &= "					, [401_Staff]" & ControlChars.CrLf
			sSQL &= "					, Weight_001_Platform" & ControlChars.CrLf
			sSQL &= "					, Weight_002_MobyRank" & ControlChars.CrLf
			sSQL &= "					, Weight_003_MobyScore" & ControlChars.CrLf
			sSQL &= "					, Weight_004_Publisher" & ControlChars.CrLf
			sSQL &= "					, Weight_005_Developer" & ControlChars.CrLf
			sSQL &= "					, Weight_006_Year" & ControlChars.CrLf
			sSQL &= "					, Weight_101_Basic_Genres" & ControlChars.CrLf
			sSQL &= "					, Weight_102_Perspectives" & ControlChars.CrLf
			sSQL &= "					, Weight_103_Sports_Themes" & ControlChars.CrLf
			sSQL &= "					, Weight_105_Educational_Categories" & ControlChars.CrLf
			sSQL &= "					, Weight_106_Other_Attributes" & ControlChars.CrLf
			sSQL &= "					, Weight_107_Visual_Presentation" & ControlChars.CrLf
			sSQL &= "					, Weight_108_Gameplay" & ControlChars.CrLf
			sSQL &= "					, Weight_109_Pacing" & ControlChars.CrLf
			sSQL &= "					, Weight_110_Narrative_Theme_Topic" & ControlChars.CrLf
			sSQL &= "					, Weight_111_Setting" & ControlChars.CrLf
			sSQL &= "					, Weight_112_Vehicular_Themes" & ControlChars.CrLf
			sSQL &= "					, Weight_113_Interface_Control" & ControlChars.CrLf
			sSQL &= "					, Weight_114_DLC_Addon" & ControlChars.CrLf
			sSQL &= "					, Weight_115_Special_Edition" & ControlChars.CrLf
			sSQL &= "					, Weight_201_MinPlayers" & ControlChars.CrLf
			sSQL &= "					, Weight_202_MaxPlayers" & ControlChars.CrLf
			sSQL &= "					, Weight_203_AgeO" & ControlChars.CrLf
			sSQL &= "					, Weight_204_AgeP" & ControlChars.CrLf
			sSQL &= "					, Weight_205_Rating_Descriptors" & ControlChars.CrLf
			sSQL &= "					, Weight_206_Other_Attributes" & ControlChars.CrLf
			sSQL &= "					, Weight_207_Multiplayer_Attributes" & ControlChars.CrLf
			sSQL &= "					, Weight_301_Group_Membership" & ControlChars.CrLf
			sSQL &= "					, Weight_401_Staff" & ControlChars.CrLf
			sSQL &= "	)" & ControlChars.CrLf
			sSQL &= "" & ControlChars.CrLf
			sSQL &= "	SELECT		MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "						, CASE	WHEN IFNULL(EG.id_Moby_Platforms_Alternative, EG.id_Moby_Platforms) = MRC.id_Moby_Platforms THEN 100 ELSE 0 END AS [001_Platform]" & ControlChars.CrLf
			sSQL &= "						, CASE	WHEN MR.MobyRank IS NOT NULL OR MRC.MobyRank IS NOT NULL" & ControlChars.CrLf
			sSQL &= "										THEN 100 - ABS(IFNULL(MR.MobyRank, 0) - IFNULL(MRC.MobyRank, 0))" & ControlChars.CrLf
			sSQL &= "										ELSE NULL" & ControlChars.CrLf
			sSQL &= "							END" & ControlChars.CrLf
			sSQL &= "							AS [002_MobyRank]" & ControlChars.CrLf
			sSQL &= "						, CASE	WHEN MR.MobyScore IS NOT NULL OR MRC.MobyScore IS NOT NULL" & ControlChars.CrLf
			sSQL &= "										THEN CAST(100 - ABS(20.0*IFNULL(MR.MobyScore, 0) - 20.0*IFNULL(MRC.MobyScore, 0)) AS INTEGER)" & ControlChars.CrLf
			sSQL &= "										ELSE NULL" & ControlChars.CrLf
			sSQL &= "							END AS [003_MobyScore]" & ControlChars.CrLf
			sSQL &= "						, CASE	WHEN IFNULL(EG.Publisher, C1.Name) IS NULL AND C1C.Name IS NULL THEN NULL" & ControlChars.CrLf
			sSQL &= "										WHEN IFNULL(EG.Publisher, C1.Name) = C1C.Name THEN 100" & ControlChars.CrLf
			sSQL &= "										ELSE 0" & ControlChars.CrLf
			sSQL &= "							END AS [004_Publisher]" & ControlChars.CrLf
			sSQL &= "						, CASE	WHEN IFNULL(EG.Developer, C2.Name) IS NULL AND C2C.Name IS NULL THEN NULL" & ControlChars.CrLf
			sSQL &= "										WHEN IFNULL(EG.Developer, C2.Name) = C2C.Name THEN 100" & ControlChars.CrLf
			sSQL &= "										ELSE 0 " & ControlChars.CrLf
			sSQL &= "							END AS [005_Developer]" & ControlChars.CrLf
			sSQL &= "						, CASE	WHEN (IFNULL(EG.Year, MR.Year) IS NULL OR IFNULL(EG.Year, MR.Year) < 1950) AND (MRC.Year IS NULL OR MRC.Year < 1950) THEN NULL" & ControlChars.CrLf
			sSQL &= "										ELSE MAX(100 - (10 * CAST(ABS(IFNULL(EG.Year, MR.Year) - MRC.Year) / 2 AS INTEGER)), 0)" & ControlChars.CrLf
			sSQL &= "							END AS [006_Year]" & ControlChars.CrLf

			sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_MobyReleases_Genre(cls_Globals.enm_Moby_Genres_Categories.Basic_Genres, "[101_Basic_Genres]")
			sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_MobyReleases_Genre(cls_Globals.enm_Moby_Genres_Categories.Perspective, "[102_Perspectives]")
			sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_MobyReleases_Genre(cls_Globals.enm_Moby_Genres_Categories.Sports_Themes, "[103_Sports_Themes]")
			sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_MobyReleases_Genre(cls_Globals.enm_Moby_Genres_Categories.Educational_Categories, "[105_Educational_Categories]")
			sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_MobyReleases_Genre(cls_Globals.enm_Moby_Genres_Categories.Other_Attributes, "[106_Other_Attributes]")
			sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_MobyReleases_Genre(cls_Globals.enm_Moby_Genres_Categories.Visual_Presentation, "[107_Visual_Presentation]")
			sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_MobyReleases_Genre(cls_Globals.enm_Moby_Genres_Categories.Gameplay, "[108_Gameplay]")
			sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_MobyReleases_Genre(cls_Globals.enm_Moby_Genres_Categories.Pacing, "[109_Pacing]")
			sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_MobyReleases_Genre(cls_Globals.enm_Moby_Genres_Categories.Narrative_Theme_Topic, "[110_Narrative_Theme_Topic]")
			sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_MobyReleases_Genre(cls_Globals.enm_Moby_Genres_Categories.Setting, "[111_Setting]")
			sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_MobyReleases_Genre(cls_Globals.enm_Moby_Genres_Categories.Vehicular_Themes, "[112_Vehicular_Themes]")
			sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_MobyReleases_Genre(cls_Globals.enm_Moby_Genres_Categories.Interface_Control, "[113_Interface_Control]")
			sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_MobyReleases_Genre(cls_Globals.enm_Moby_Genres_Categories.DLC_Addon, "[114_DLC_Addon]")
			sSQL &= GenSQL_Fill_tbl_Similarity_Calculation_MobyReleases_Genre(cls_Globals.enm_Moby_Genres_Categories.Special_Edition, "[115_Special_Edition]")

			sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
			sSQL &= "							(" & ControlChars.CrLf
			sSQL &= "								SELECT MIN(A.MinPlayers) FROM" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
			sSQL &= "									WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
			sSQL &= "									WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "								) AS temp_Attributes" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "								WHERE A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "									AND Used = 0" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							) IS NULL" & ControlChars.CrLf
			sSQL &= "							OR" & ControlChars.CrLf
			sSQL &= "							(" & ControlChars.CrLf
			sSQL &= "								SELECT MIN(AC.MinPlayers) FROM" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
			sSQL &= "									WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "								) AS temp_AttributesC" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "							) IS NULL" & ControlChars.CrLf
			sSQL &= "							THEN NULL" & ControlChars.CrLf
			sSQL &= "							ELSE" & ControlChars.CrLf
			sSQL &= "							MAX(0," & ControlChars.CrLf
			sSQL &= "								100 - " & ControlChars.CrLf
			sSQL &= "								10 * ABS" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									(" & ControlChars.CrLf
			sSQL &= "										SELECT MIN(A.MinPlayers) FROM" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
			sSQL &= "											WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "											UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
			sSQL &= "											WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "										) AS temp_Attributes" & ControlChars.CrLf
			sSQL &= "										LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "										WHERE A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "											WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "											AND Used = 0" & ControlChars.CrLf
			sSQL &= "										)" & ControlChars.CrLf
			sSQL &= "									)" & ControlChars.CrLf
			sSQL &= "									-" & ControlChars.CrLf
			sSQL &= "									(" & ControlChars.CrLf
			sSQL &= "										SELECT MIN(AC.MinPlayers) FROM" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
			sSQL &= "											WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "										) AS temp_AttributesC" & ControlChars.CrLf
			sSQL &= "										LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									)" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							)" & ControlChars.CrLf
			sSQL &= "							END" & ControlChars.CrLf
			sSQL &= "							AS [201_MinPlayers]" & ControlChars.CrLf
			sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
			sSQL &= "							(" & ControlChars.CrLf
			sSQL &= "								SELECT MAX(A.MaxPlayers) FROM" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
			sSQL &= "									WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
			sSQL &= "									WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "								) AS temp_Attributes" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "								WHERE A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "									AND Used = 0" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							) IS NULL" & ControlChars.CrLf
			sSQL &= "							AND" & ControlChars.CrLf
			sSQL &= "							(" & ControlChars.CrLf
			sSQL &= "								SELECT MAX(AC.MaxPlayers) FROM" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
			sSQL &= "									WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "								) AS temp_AttributesC" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "							) IS NULL" & ControlChars.CrLf
			sSQL &= "							THEN NULL" & ControlChars.CrLf
			sSQL &= "							ELSE" & ControlChars.CrLf
			sSQL &= "							MAX(0," & ControlChars.CrLf
			sSQL &= "								100 - " & ControlChars.CrLf
			sSQL &= "								10 * ABS" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									(" & ControlChars.CrLf
			sSQL &= "										SELECT MAX(A.MaxPlayers) FROM" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
			sSQL &= "											WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "											UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
			sSQL &= "											WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "										) AS temp_Attributes" & ControlChars.CrLf
			sSQL &= "										LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "										WHERE A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "											WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "											AND Used = 0" & ControlChars.CrLf
			sSQL &= "										)" & ControlChars.CrLf
			sSQL &= "									)" & ControlChars.CrLf
			sSQL &= "									-" & ControlChars.CrLf
			sSQL &= "									(" & ControlChars.CrLf
			sSQL &= "										SELECT MAX(AC.MaxPlayers) FROM" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
			sSQL &= "											WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "										) AS temp_AttributesC" & ControlChars.CrLf
			sSQL &= "										LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									)" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							)" & ControlChars.CrLf
			sSQL &= "							END" & ControlChars.CrLf
			sSQL &= "							AS [202_MaxPlayers]" & ControlChars.CrLf
			sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
			sSQL &= "							(" & ControlChars.CrLf
			sSQL &= "								SELECT MIN(A.Rating_Age_From) FROM" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
			sSQL &= "									WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
			sSQL &= "									WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "								) AS temp_Attributes" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "								WHERE A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "									AND Used = 0" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							) IS NULL" & ControlChars.CrLf
			sSQL &= "							OR" & ControlChars.CrLf
			sSQL &= "							(" & ControlChars.CrLf
			sSQL &= "								SELECT MIN(AC.Rating_Age_From) FROM" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
			sSQL &= "									WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "								) AS temp_AttributesC" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "							) IS NULL" & ControlChars.CrLf
			sSQL &= "							THEN NULL" & ControlChars.CrLf
			sSQL &= "							ELSE" & ControlChars.CrLf
			sSQL &= "							MAX(0," & ControlChars.CrLf
			sSQL &= "								100 - " & ControlChars.CrLf
			sSQL &= "								10 * ABS" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									(" & ControlChars.CrLf
			sSQL &= "										SELECT MIN(A.Rating_Age_From) FROM" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
			sSQL &= "											WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "											UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
			sSQL &= "											WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "										) AS temp_Attributes" & ControlChars.CrLf
			sSQL &= "										LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "										WHERE A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "											WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "											AND Used = 0" & ControlChars.CrLf
			sSQL &= "										)" & ControlChars.CrLf
			sSQL &= "									)" & ControlChars.CrLf
			sSQL &= "									-" & ControlChars.CrLf
			sSQL &= "									(" & ControlChars.CrLf
			sSQL &= "										SELECT MIN(AC.Rating_Age_From) FROM" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
			sSQL &= "											WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "										) AS temp_AttributesC" & ControlChars.CrLf
			sSQL &= "										LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									)" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							)" & ControlChars.CrLf
			sSQL &= "							END" & ControlChars.CrLf
			sSQL &= "							AS [203_AgeO]" & ControlChars.CrLf
			sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
			sSQL &= "							(" & ControlChars.CrLf
			sSQL &= "								SELECT MAX(A.Rating_Age_From) FROM" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
			sSQL &= "									WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
			sSQL &= "									WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "								) AS temp_Attributes" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "								WHERE A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "									AND Used = 0" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							) IS NULL" & ControlChars.CrLf
			sSQL &= "							OR" & ControlChars.CrLf
			sSQL &= "							(" & ControlChars.CrLf
			sSQL &= "								SELECT MAX(AC.Rating_Age_From) FROM" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
			sSQL &= "									WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "								) AS temp_AttributesC" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "							) IS NULL" & ControlChars.CrLf
			sSQL &= "							THEN NULL" & ControlChars.CrLf
			sSQL &= "							ELSE" & ControlChars.CrLf
			sSQL &= "							MAX(0," & ControlChars.CrLf
			sSQL &= "								100 - " & ControlChars.CrLf
			sSQL &= "								10 * ABS" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									(" & ControlChars.CrLf
			sSQL &= "										SELECT MAX(A.Rating_Age_From) FROM" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
			sSQL &= "											WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "											UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
			sSQL &= "											WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "										) AS temp_Attributes" & ControlChars.CrLf
			sSQL &= "										LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "										WHERE A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "											FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "											WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "											AND Used = 0" & ControlChars.CrLf
			sSQL &= "										)" & ControlChars.CrLf
			sSQL &= "									)" & ControlChars.CrLf
			sSQL &= "									-" & ControlChars.CrLf
			sSQL &= "									(" & ControlChars.CrLf
			sSQL &= "										SELECT MAX(AC.Rating_Age_From) FROM" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
			sSQL &= "											WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "										) AS temp_AttributesC" & ControlChars.CrLf
			sSQL &= "										LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									)" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							)" & ControlChars.CrLf
			sSQL &= "							END" & ControlChars.CrLf
			sSQL &= "							AS [204_AgeP]" & ControlChars.CrLf
			sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
			sSQL &= "							(" & ControlChars.CrLf
			sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
			sSQL &= "									WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
			sSQL &= "									WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "								) AS temp_Attributes" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf
			sSQL &= "								WHERE	AC.RatingDescriptor = 1" & ControlChars.CrLf
			sSQL &= "								AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "									AND Used = 0" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							) = 0" & ControlChars.CrLf
			sSQL &= "							AND" & ControlChars.CrLf
			sSQL &= "							(" & ControlChars.CrLf
			sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
			sSQL &= "									WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "								) AS temp_AttributesC" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes_Categories ACC ON AC.id_Moby_Attributes_Categories = ACC.id_Moby_Attributes_Categories" & ControlChars.CrLf
			sSQL &= "								WHERE	ACC.RatingDescriptor = 1" & ControlChars.CrLf
			sSQL &= "							) = 0" & ControlChars.CrLf
			sSQL &= "							THEN NULL" & ControlChars.CrLf
			sSQL &= "							ELSE" & ControlChars.CrLf
			sSQL &= "								CAST" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									CAST" & ControlChars.CrLf
			sSQL &= "									(" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "											(" & ControlChars.CrLf
			sSQL &= "												SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
			sSQL &= "												WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "												UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
			sSQL &= "												WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "											) AS temp_Attributes" & ControlChars.CrLf
			sSQL &= "											LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "											LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf
			sSQL &= "											WHERE	AC.RatingDescriptor = 1" & ControlChars.CrLf
			sSQL &= "											AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
			sSQL &= "											(" & ControlChars.CrLf
			sSQL &= "												SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "												WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "												AND Used = 0" & ControlChars.CrLf
			sSQL &= "											)" & ControlChars.CrLf
			sSQL &= "											AND A.id_Moby_Attributes IN (SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes WHERE id_Moby_Releases = MRC.id_Moby_Releases) -- HERE!" & ControlChars.CrLf
			sSQL &= "										)" & ControlChars.CrLf
			sSQL &= "										AS FLOAT" & ControlChars.CrLf
			sSQL &= "									)" & ControlChars.CrLf
			sSQL &= "									/" & ControlChars.CrLf
			sSQL &= "									CAST" & ControlChars.CrLf
			sSQL &= "									(" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "											(" & ControlChars.CrLf
			sSQL &= "												SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
			sSQL &= "												WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "												UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
			sSQL &= "												WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "												UNION SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
			sSQL &= "												WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "											) AS temp_Attributes" & ControlChars.CrLf
			sSQL &= "											LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "											LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf
			sSQL &= "											WHERE	AC.RatingDescriptor = 1" & ControlChars.CrLf
			sSQL &= "										)" & ControlChars.CrLf
			sSQL &= "										AS FLOAT" & ControlChars.CrLf
			sSQL &= "									)" & ControlChars.CrLf
			sSQL &= "									* 100" & ControlChars.CrLf
			sSQL &= "									AS INTEGER" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							END" & ControlChars.CrLf
			sSQL &= "							AS [205_Rating_Descriptors]" & ControlChars.CrLf
			sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
			sSQL &= "							(" & ControlChars.CrLf
			sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
			sSQL &= "									WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
			sSQL &= "									WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "								) AS temp_Attributes" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf
			sSQL &= "								WHERE	IFNULL(AC.RatingDescriptor, 0) = 0 AND IFNULL(AC.RatingSystem, 0) = 0 AND IFNULL(AC.NumPlayersCategory, 0) = 0 AND AC.id_Moby_Attributes_Categories NOT IN (12, 16)" & ControlChars.CrLf
			sSQL &= "								AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "									AND Used = 0" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							) = 0" & ControlChars.CrLf
			sSQL &= "							AND" & ControlChars.CrLf
			sSQL &= "							(" & ControlChars.CrLf
			sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
			sSQL &= "									WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "								) AS temp_AttributesC" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes_Categories ACC ON AC.id_Moby_Attributes_Categories = ACC.id_Moby_Attributes_Categories" & ControlChars.CrLf
			sSQL &= "								WHERE	IFNULL(ACC.RatingDescriptor, 0) = 0 AND IFNULL(ACC.RatingSystem, 0) = 0 AND IFNULL(ACC.NumPlayersCategory, 0) = 0 AND AC.id_Moby_Attributes_Categories NOT IN (12, 16)" & ControlChars.CrLf
			sSQL &= "							) = 0" & ControlChars.CrLf
			sSQL &= "							THEN NULL" & ControlChars.CrLf
			sSQL &= "							ELSE" & ControlChars.CrLf
			sSQL &= "								CAST" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									CAST" & ControlChars.CrLf
			sSQL &= "									(" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "											(" & ControlChars.CrLf
			sSQL &= "												SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
			sSQL &= "												WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "												UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
			sSQL &= "												WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "											) AS temp_Attributes" & ControlChars.CrLf
			sSQL &= "											LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "											LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf
			sSQL &= "											WHERE	IFNULL(AC.RatingDescriptor, 0) = 0 AND IFNULL(AC.RatingSystem, 0) = 0 AND IFNULL(AC.NumPlayersCategory, 0) = 0 AND AC.id_Moby_Attributes_Categories NOT IN (12, 16)" & ControlChars.CrLf
			sSQL &= "											AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
			sSQL &= "											(" & ControlChars.CrLf
			sSQL &= "												SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "												WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "												AND Used = 0" & ControlChars.CrLf
			sSQL &= "											)" & ControlChars.CrLf
			sSQL &= "											AND A.id_Moby_Attributes IN (SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes WHERE id_Moby_Releases = MRC.id_Moby_Releases) -- HERE!" & ControlChars.CrLf
			sSQL &= "										)" & ControlChars.CrLf
			sSQL &= "										AS FLOAT" & ControlChars.CrLf
			sSQL &= "									)" & ControlChars.CrLf
			sSQL &= "									/" & ControlChars.CrLf
			sSQL &= "									CAST" & ControlChars.CrLf
			sSQL &= "									(" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "											(" & ControlChars.CrLf
			sSQL &= "												SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
			sSQL &= "												WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "												UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
			sSQL &= "												WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "												UNION SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
			sSQL &= "												WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "											) AS temp_Attributes" & ControlChars.CrLf
			sSQL &= "											LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "											LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf
			sSQL &= "											WHERE	IFNULL(AC.RatingDescriptor, 0) = 0 AND IFNULL(AC.RatingSystem, 0) = 0 AND IFNULL(AC.NumPlayersCategory, 0) = 0 AND AC.id_Moby_Attributes_Categories NOT IN (12, 16)" & ControlChars.CrLf
			sSQL &= "										)" & ControlChars.CrLf
			sSQL &= "										AS FLOAT" & ControlChars.CrLf
			sSQL &= "									)" & ControlChars.CrLf
			sSQL &= "									* 100" & ControlChars.CrLf
			sSQL &= "									AS INTEGER" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							END" & ControlChars.CrLf
			sSQL &= "							AS [206_Other_Attributes]" & ControlChars.CrLf
			sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
			sSQL &= "							(" & ControlChars.CrLf
			sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
			sSQL &= "									WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "									UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
			sSQL &= "									WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "								) AS temp_Attributes" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf
			sSQL &= "								WHERE	AC.id_Moby_Attributes_Categories IN (12, 16)" & ControlChars.CrLf
			sSQL &= "								AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "									WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "									AND Used = 0" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							) = 0" & ControlChars.CrLf
			sSQL &= "							AND" & ControlChars.CrLf
			sSQL &= "							(" & ControlChars.CrLf
			sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
			sSQL &= "									WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "								) AS temp_AttributesC" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes AC ON temp_AttributesC.id_Moby_Attributes = AC.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "								LEFT JOIN tbl_Moby_Attributes_Categories ACC ON AC.id_Moby_Attributes_Categories = ACC.id_Moby_Attributes_Categories" & ControlChars.CrLf
			sSQL &= "								WHERE	AC.id_Moby_Attributes_Categories IN (12, 16)" & ControlChars.CrLf
			sSQL &= "							) = 0" & ControlChars.CrLf
			sSQL &= "							THEN NULL" & ControlChars.CrLf
			sSQL &= "							ELSE" & ControlChars.CrLf
			sSQL &= "								CAST" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									CAST" & ControlChars.CrLf
			sSQL &= "									(" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "											(" & ControlChars.CrLf
			sSQL &= "												SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
			sSQL &= "												WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "												UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
			sSQL &= "												WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "											) AS temp_Attributes" & ControlChars.CrLf
			sSQL &= "											LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "											LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf
			sSQL &= "											WHERE	AC.id_Moby_Attributes_Categories IN (12, 16)" & ControlChars.CrLf
			sSQL &= "											AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
			sSQL &= "											(" & ControlChars.CrLf
			sSQL &= "												SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "												WHERE id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "												AND Used = 0" & ControlChars.CrLf
			sSQL &= "											)" & ControlChars.CrLf
			sSQL &= "											AND A.id_Moby_Attributes IN (SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes WHERE id_Moby_Releases = MRC.id_Moby_Releases) -- HERE!" & ControlChars.CrLf
			sSQL &= "										)" & ControlChars.CrLf
			sSQL &= "										AS FLOAT" & ControlChars.CrLf
			sSQL &= "									)" & ControlChars.CrLf
			sSQL &= "									/" & ControlChars.CrLf
			sSQL &= "									CAST" & ControlChars.CrLf
			sSQL &= "									(" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "											(" & ControlChars.CrLf
			sSQL &= "												SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
			sSQL &= "												WHERE RA.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "												UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "												FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
			sSQL &= "												WHERE EGMA.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
			sSQL &= "												UNION SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RAC" & ControlChars.CrLf
			sSQL &= "												WHERE RAC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "											) AS temp_Attributes" & ControlChars.CrLf
			sSQL &= "											LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
			sSQL &= "											LEFT JOIN tbl_Moby_Attributes_Categories AC ON A.id_Moby_Attributes_Categories = AC.id_Moby_Attributes_Categories" & ControlChars.CrLf
			sSQL &= "											WHERE	AC.id_Moby_Attributes_Categories IN (12, 16)" & ControlChars.CrLf
			sSQL &= "										)" & ControlChars.CrLf
			sSQL &= "										AS FLOAT" & ControlChars.CrLf
			sSQL &= "									)" & ControlChars.CrLf
			sSQL &= "									* 100" & ControlChars.CrLf
			sSQL &= "									AS INTEGER" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							END" & ControlChars.CrLf
			sSQL &= "							AS [207_Multiplayer_Attributes]" & ControlChars.CrLf
			sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
			sSQL &= "							(" & ControlChars.CrLf
			sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT DISTINCT id_Moby_Game_Groups FROM tbl_Moby_Game_Groups_Moby_Releases MGGMR" & ControlChars.CrLf
			sSQL &= "									WHERE MGGMR.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							) = 0" & ControlChars.CrLf
			sSQL &= "							AND" & ControlChars.CrLf
			sSQL &= "							(" & ControlChars.CrLf
			sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT DISTINCT id_Moby_Game_Groups FROM tbl_Moby_Game_Groups_Moby_Releases MGGMRC" & ControlChars.CrLf
			sSQL &= "									WHERE MGGMRC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							) = 0" & ControlChars.CrLf
			sSQL &= "							THEN NULL" & ControlChars.CrLf
			sSQL &= "							ELSE" & ControlChars.CrLf
			sSQL &= "								CAST" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									CAST" & ControlChars.CrLf
			sSQL &= "									(" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "											(" & ControlChars.CrLf
			sSQL &= "												SELECT DISTINCT id_Moby_Game_Groups FROM tbl_Moby_Game_Groups_Moby_Releases MGGMR" & ControlChars.CrLf
			sSQL &= "												WHERE MGGMR.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "												INTERSECT SELECT DISTINCT id_Moby_Game_Groups FROM tbl_Moby_Game_Groups_Moby_Releases MGGMRC" & ControlChars.CrLf
			sSQL &= "												WHERE MGGMRC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "											)" & ControlChars.CrLf
			sSQL &= "										)" & ControlChars.CrLf
			sSQL &= "										AS FLOAT" & ControlChars.CrLf
			sSQL &= "									)" & ControlChars.CrLf
			sSQL &= "									/" & ControlChars.CrLf
			sSQL &= "									CAST" & ControlChars.CrLf
			sSQL &= "									(" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "											(" & ControlChars.CrLf
			sSQL &= "												SELECT id_Moby_Game_Groups FROM tbl_Moby_Game_Groups_Moby_Releases MGGMR" & ControlChars.CrLf
			sSQL &= "												WHERE MGGMR.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "												UNION SELECT id_Moby_Game_Groups FROM tbl_Moby_Game_Groups_Moby_Releases MGGMRC" & ControlChars.CrLf
			sSQL &= "												WHERE MGGMRC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "											)" & ControlChars.CrLf
			sSQL &= "										)" & ControlChars.CrLf
			sSQL &= "										AS FLOAT" & ControlChars.CrLf
			sSQL &= "									)" & ControlChars.CrLf
			sSQL &= "									* 100" & ControlChars.CrLf
			sSQL &= "									AS INTEGER" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							END" & ControlChars.CrLf
			sSQL &= "							AS [301_Group_Membership]" & ControlChars.CrLf
			sSQL &= "						,	CASE WHEN" & ControlChars.CrLf
			sSQL &= "							(" & ControlChars.CrLf
			sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT DISTINCT id_Moby_Staff FROM tbl_Moby_Releases_Staff MRS" & ControlChars.CrLf
			sSQL &= "									WHERE MRS.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							) = 0" & ControlChars.CrLf
			sSQL &= "							AND" & ControlChars.CrLf
			sSQL &= "							(" & ControlChars.CrLf
			sSQL &= "								SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									SELECT DISTINCT id_Moby_Staff FROM tbl_Moby_Releases_Staff MRSC" & ControlChars.CrLf
			sSQL &= "									WHERE MRSC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							) = 0" & ControlChars.CrLf
			sSQL &= "							THEN NULL" & ControlChars.CrLf
			sSQL &= "							ELSE" & ControlChars.CrLf
			sSQL &= "								CAST" & ControlChars.CrLf
			sSQL &= "								(" & ControlChars.CrLf
			sSQL &= "									CAST" & ControlChars.CrLf
			sSQL &= "									(" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "											(" & ControlChars.CrLf
			sSQL &= "												SELECT DISTINCT id_Moby_Staff FROM tbl_Moby_Releases_Staff MRS" & ControlChars.CrLf
			sSQL &= "												WHERE MRS.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "												INTERSECT SELECT DISTINCT id_Moby_Staff FROM tbl_Moby_Releases_Staff MRSC" & ControlChars.CrLf
			sSQL &= "												WHERE MRSC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "" & ControlChars.CrLf
			sSQL &= "											)" & ControlChars.CrLf
			sSQL &= "										)" & ControlChars.CrLf
			sSQL &= "										AS FLOAT" & ControlChars.CrLf
			sSQL &= "									)" & ControlChars.CrLf
			sSQL &= "									/" & ControlChars.CrLf
			sSQL &= "									CAST" & ControlChars.CrLf
			sSQL &= "									(" & ControlChars.CrLf
			sSQL &= "										(" & ControlChars.CrLf
			sSQL &= "											SELECT COUNT(1) FROM" & ControlChars.CrLf
			sSQL &= "											(" & ControlChars.CrLf
			sSQL &= "												SELECT DISTINCT id_Moby_Staff FROM tbl_Moby_Releases_Staff MRS" & ControlChars.CrLf
			sSQL &= "												WHERE MRS.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "												UNION SELECT DISTINCT id_Moby_Staff FROM tbl_Moby_Releases_Staff MRSC" & ControlChars.CrLf
			sSQL &= "												WHERE MRSC.id_Moby_Releases = MRC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "											)" & ControlChars.CrLf
			sSQL &= "										)" & ControlChars.CrLf
			sSQL &= "										AS FLOAT" & ControlChars.CrLf
			sSQL &= "									)" & ControlChars.CrLf
			sSQL &= "									* 100" & ControlChars.CrLf
			sSQL &= "									AS INTEGER" & ControlChars.CrLf
			sSQL &= "								)" & ControlChars.CrLf
			sSQL &= "							END" & ControlChars.CrLf
			sSQL &= "							AS [401_Staff]" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_001_Platform, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_002_MobyRank, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_003_MobyScore, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_004_Publisher, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_005_Developer, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_006_Year, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_101_Basic_Genres, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_102_Perspectives, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_103_Sports_Themes, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_105_Educational_Categories, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_106_Other_Attributes, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_107_Visual_Presentation, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_108_Gameplay, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_109_Pacing, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_110_Narrative_Theme_Topic, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_111_Setting, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_112_Vehicular_Themes, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_113_Interface_Control, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_114_DLC_Addon, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_115_Special_Edition, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_201_MinPlayers, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_202_MaxPlayers, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_203_AgeO, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_204_AgeP, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_205_Rating_Descriptors, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_206_Other_Attributes, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_207_Multiplayer_Attributes, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_301_Group_Membership, 1)" & ControlChars.CrLf
			sSQL &= "						, IFNULL(SimConf.Weight_401_Staff, 1)" & ControlChars.CrLf
			sSQL &= "	FROM			tbl_Emu_Games EG" & ControlChars.CrLf
			sSQL &= "	LEFT JOIN tbl_Moby_Games MG ON EG.Moby_Games_URLPart = MG.URLPart" & ControlChars.CrLf
			sSQL &= "	LEFT JOIN tbl_Moby_Releases MR ON IFNULL(EG.id_Moby_Platforms_Alternative, EG.id_Moby_Platforms) = MR.id_Moby_Platforms AND MG.id_Moby_Games = MR.id_Moby_Games" & ControlChars.CrLf
			sSQL &= "	LEFT JOIN tbl_Moby_Companies C1 ON MR.Publisher_id_Moby_Companies = C1.id_Moby_Companies" & ControlChars.CrLf
			sSQL &= "	LEFT JOIN tbl_Moby_Companies C2 ON MR.Developer_id_Moby_Companies = C2.id_Moby_Companies" & ControlChars.CrLf
			sSQL &= "" & ControlChars.CrLf
			sSQL &= "	LEFT JOIN tbl_Moby_Releases MRC" & ControlChars.CrLf
			sSQL &= "	LEFT JOIN tbl_Moby_Games MGC ON MRC.id_Moby_Games = MGC.id_Moby_Games" & ControlChars.CrLf
			sSQL &= "	LEFT JOIN tbl_Moby_Companies C1C ON MRC.Publisher_id_Moby_Companies = C1C.id_Moby_Companies" & ControlChars.CrLf
			sSQL &= "	LEFT JOIN tbl_Moby_Companies C2C ON MRC.Developer_id_Moby_Companies = C2C.id_Moby_Companies" & ControlChars.CrLf
			sSQL &= "	LEFT JOIN tbl_Similarity_Calculation_Config SimConf ON SimConf.id_Similarity_Calculation_Config = " & TC.getSQLFormat(id_Similarity_Calculation_Config) & " -- variable" & ControlChars.CrLf
			sSQL &= "	WHERE		EG.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " -- variable" & ControlChars.CrLf
			sSQL &= "					AND MRC.id_Moby_Releases NOT IN (SELECT id_Moby_Releases FROM ttb_Emu_Games_Similarity_Calculation WHERE id_Moby_Releases IS NOT NULL)" & ControlChars.CrLf
			sSQL &= ";" & ControlChars.CrLf
			sSQL &= "UPDATE	ttb_Moby_Releases_Similarity_Calculation" & ControlChars.CrLf
			sSQL &= "SET			Similarity =" & ControlChars.CrLf
			sSQL &= "				CAST" & ControlChars.CrLf
			sSQL &= "				(" & ControlChars.CrLf
			sSQL &= "					CAST" & ControlChars.CrLf
			sSQL &= "					(" & ControlChars.CrLf
			sSQL &= "						(" & ControlChars.CrLf
			sSQL &= "							IFNULL([001_Platform], 0) * [Weight_001_Platform]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([002_MobyRank], 0) * [Weight_002_MobyRank]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([003_MobyScore], 0) * [Weight_003_MobyScore]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([004_Publisher], 0) * [Weight_004_Publisher]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([005_Developer], 0) * [Weight_005_Developer]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([006_Year], 0) * [Weight_006_Year]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([101_Basic_Genres], 0) * [Weight_101_Basic_Genres]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([102_Perspectives], 0) * [Weight_102_Perspectives]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([103_Sports_Themes], 0) * [Weight_103_Sports_Themes]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([105_Educational_Categories], 0) * [Weight_105_Educational_Categories]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([106_Other_Attributes], 0) * [Weight_106_Other_Attributes]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([107_Visual_Presentation], 0) * [Weight_107_Visual_Presentation]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([108_Gameplay], 0) * [Weight_108_Gameplay]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([109_Pacing], 0) * [Weight_109_Pacing]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([110_Narrative_Theme_Topic], 0) * [Weight_110_Narrative_Theme_Topic]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([111_Setting], 0) * [Weight_111_Setting]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([112_Vehicular_Themes], 0) * [Weight_112_Vehicular_Themes]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([113_Interface_Control], 0) * [Weight_113_Interface_Control]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([114_DLC_Addon], 0) * [Weight_114_DLC_Addon]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([115_Special_Edition], 0) * [Weight_115_Special_Edition]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([201_MinPlayers], 0) * [Weight_201_MinPlayers]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([202_MaxPlayers], 0) * [Weight_202_MaxPlayers]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([203_AgeO], 0) * [Weight_203_AgeO]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([204_AgeP], 0) * [Weight_204_AgeP]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([205_Rating_Descriptors], 0) * [Weight_205_Rating_Descriptors]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([206_Other_Attributes], 0) * [Weight_206_Other_Attributes]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([207_Multiplayer_Attributes], 0) * [Weight_207_Multiplayer_Attributes]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([301_Group_Membership], 0) * [Weight_301_Group_Membership]" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							IFNULL([401_Staff], 0) * [Weight_401_Staff]" & ControlChars.CrLf
			sSQL &= "						)" & ControlChars.CrLf
			sSQL &= "						AS FLOAT" & ControlChars.CrLf
			sSQL &= "					)" & ControlChars.CrLf
			sSQL &= "" & ControlChars.CrLf
			sSQL &= "					/" & ControlChars.CrLf
			sSQL &= "" & ControlChars.CrLf
			sSQL &= "					CAST" & ControlChars.CrLf
			sSQL &= "					(	MAX" & ControlChars.CrLf
			sSQL &= "						(" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [001_Platform] IS NULL THEN 0 ELSE [Weight_001_Platform] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [002_MobyRank] IS NULL THEN 0 ELSE [Weight_002_MobyRank] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [003_MobyScore] IS NULL THEN 0 ELSE [Weight_003_MobyScore] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [004_Publisher] IS NULL THEN 0 ELSE [Weight_004_Publisher] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [005_Developer] IS NULL THEN 0 ELSE [Weight_005_Developer] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [006_Year] IS NULL THEN 0 ELSE [Weight_006_Year] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [101_Basic_Genres] IS NULL THEN 0 ELSE [Weight_101_Basic_Genres] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [102_Perspectives] IS NULL THEN 0 ELSE [Weight_102_Perspectives] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [103_Sports_Themes] IS NULL THEN 0 ELSE [Weight_103_Sports_Themes] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [105_Educational_Categories] IS NULL THEN 0 ELSE [Weight_105_Educational_Categories] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [106_Other_Attributes] IS NULL THEN 0 ELSE [Weight_106_Other_Attributes] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [107_Visual_Presentation] IS NULL THEN 0 ELSE [Weight_107_Visual_Presentation] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [108_Gameplay] IS NULL THEN 0 ELSE [Weight_108_Gameplay] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [109_Pacing] IS NULL THEN 0 ELSE [Weight_109_Pacing] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [110_Narrative_Theme_Topic] IS NULL THEN 0 ELSE [Weight_110_Narrative_Theme_Topic] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [111_Setting] IS NULL THEN 0 ELSE [Weight_111_Setting] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [112_Vehicular_Themes] IS NULL THEN 0 ELSE [Weight_112_Vehicular_Themes] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [113_Interface_Control] IS NULL THEN 0 ELSE [Weight_113_Interface_Control] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [114_DLC_Addon] IS NULL THEN 0 ELSE [Weight_114_DLC_Addon] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [115_Special_Edition] IS NULL THEN 0 ELSE [Weight_115_Special_Edition] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [201_MinPlayers] IS NULL THEN 0 ELSE [Weight_201_MinPlayers] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [202_MaxPlayers] IS NULL THEN 0 ELSE [Weight_202_MaxPlayers] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [203_AgeO] IS NULL THEN 0 ELSE [Weight_203_AgeO] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [204_AgeP] IS NULL THEN 0 ELSE [Weight_204_AgeP] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [205_Rating_Descriptors] IS NULL THEN 0 ELSE [Weight_205_Rating_Descriptors] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [206_Other_Attributes] IS NULL THEN 0 ELSE [Weight_206_Other_Attributes] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [207_Multiplayer_Attributes] IS NULL THEN 0 ELSE [Weight_207_Multiplayer_Attributes] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [301_Group_Membership] IS NULL THEN 0 ELSE [Weight_301_Group_Membership] END * 100" & ControlChars.CrLf
			sSQL &= "							+" & ControlChars.CrLf
			sSQL &= "							CASE WHEN [401_Staff] IS NULL THEN 0 ELSE [Weight_401_Staff] END * 100" & ControlChars.CrLf
			sSQL &= "							, 1" & ControlChars.CrLf
			sSQL &= "						)" & ControlChars.CrLf
			sSQL &= "						AS FLOAT" & ControlChars.CrLf
			sSQL &= "					)" & ControlChars.CrLf
			sSQL &= "" & ControlChars.CrLf
			sSQL &= "					* 100" & ControlChars.CrLf
			sSQL &= "" & ControlChars.CrLf
			sSQL &= "					AS INTEGER" & ControlChars.CrLf
			sSQL &= "				)" & ControlChars.CrLf
			sSQL &= ";		" & ControlChars.CrLf
			sSQL &= "" & ControlChars.CrLf
		End If

		sSQL &= "-- Output" & ControlChars.CrLf
		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= "				EGSC.id_Emu_Games AS id_Emu_Games" & ControlChars.CrLf
		sSQL &= "				, EGSC.id_Moby_Releases AS id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "				,		CASE	WHEN EG.Name IS NULL" & ControlChars.CrLf
		sSQL &= "						THEN	CASE WHEN MG.Name IS NULL " & ControlChars.CrLf
		sSQL &= "									THEN IFNULL(EG.InnerFile, EG.File)" & ControlChars.CrLf
		sSQL &= "										ELSE IFNULL(EG.Name_Prefix || ' ', IFNULL(MG.Name_Prefix || ' ', '')) || IFNULL(MG.Name, '') || IFNULL(' (' || EG.Note || ')', '')" & ControlChars.CrLf
		sSQL &= "										END" & ControlChars.CrLf
		sSQL &= "							ELSE	IFNULL(EG.Name_Prefix || ' ', '') || IFNULL(EG.Name, '') || IFNULL(' (' || EG.Note || ')', '')" & ControlChars.CrLf
		sSQL &= "				END" & ControlChars.CrLf
		sSQL &= "				AS Game" & ControlChars.CrLf
		sSQL &= "				, PLTFM.Name AS Platform" & ControlChars.CrLf
		sSQL &= "				, EG.Folder AS Folder" & ControlChars.CrLf
		sSQL &= "				, EG.File AS File" & ControlChars.CrLf
		sSQL &= "				, EG.InnerFile AS InnerFile" & ControlChars.CrLf
		sSQL &= "				, 1 AS Have" & ControlChars.CrLf
		sSQL &= "				, Similarity" & ControlChars.CrLf
		sSQL &= "				, EGSC.[001_Platform] AS [001_Platform]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[002_MobyRank] AS [002_MobyRank]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[003_MobyScore] AS [003_MobyScore]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[004_Publisher] AS [004_Publisher]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[005_Developer] AS [005_Developer]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[006_Year] AS [006_Year]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[101_Basic_Genres] AS [101_Basic_Genres]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[102_Perspectives] AS [102_Perspectives]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[103_Sports_Themes] AS [103_Sports_Themes]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[105_Educational_Categories] AS [105_Educational_Categories]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[106_Other_Attributes] AS [106_Other_Attributes]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[107_Visual_Presentation] AS [107_Visual_Presentation]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[108_Gameplay] AS [108_Gameplay]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[109_Pacing] AS [109_Pacing]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[110_Narrative_Theme_Topic] AS [110_Narrative_Theme_Topic]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[111_Setting] AS [111_Setting]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[112_Vehicular_Themes] AS [112_Vehicular_Themes]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[113_Interface_Control] AS [113_Interface_Control]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[114_DLC_Addon] AS [114_DLC_Addon]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[115_Special_Edition] AS [115_Special_Edition]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[201_MinPlayers] AS [201_MinPlayers]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[202_MaxPlayers] AS [202_MaxPlayers]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[203_AgeO] AS [203_AgeO]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[204_AgeP] AS [204_AgeP]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[205_Rating_Descriptors] AS [205_Rating_Descriptors]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[206_Other_Attributes] AS [206_Other_Attributes]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[207_Multiplayer_Attributes] AS [207_Multiplayer_Attributes]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[301_Group_Membership] AS [301_Group_Membership]" & ControlChars.CrLf
		sSQL &= "				, EGSC.[401_Staff] AS [401_Staff]" & ControlChars.CrLf
		sSQL &= "FROM		ttb_Emu_Games_Similarity_Calculation EGSC" & ControlChars.CrLf
		sSQL &= "INNER JOIN tbl_Emu_Games EG ON EGSC.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "LEFT JOIN tbl_Moby_Games MG ON EG.Moby_Games_URLPart = MG.URLPart" & ControlChars.CrLf
		sSQL &= "LEFT JOIN tbl_Moby_Releases MR ON IFNULL(EG.id_Moby_Platforms_Alternative, EG.id_Moby_Platforms) = MR.id_Moby_Platforms AND MG.id_Moby_Games = MR.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "LEFT JOIN tbl_Moby_Platforms PLTFM ON IFNULL(EG.id_Moby_Platforms_Alternative, EG.id_Moby_Platforms) = PLTFM.id_Moby_Platforms" & ControlChars.CrLf

		If Not Only_Show_Haves Then
			sSQL &= "UNION" & ControlChars.CrLf
			sSQL &= "SELECT" & ControlChars.CrLf
			sSQL &= "				NULL AS id_Emu_Games" & ControlChars.CrLf
			sSQL &= "				, MRSC.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "				,	IFNULL(MG.Name_Prefix || ' ', '') || IFNULL(MG.Name, '') AS Game" & ControlChars.CrLf
			sSQL &= "				, PLTFM.Name AS Platform" & ControlChars.CrLf
			sSQL &= "				, NULL AS Folder" & ControlChars.CrLf
			sSQL &= "				, NULL AS File" & ControlChars.CrLf
			sSQL &= "				, NULL AS InnerFile" & ControlChars.CrLf
			sSQL &= "				, 0 AS Have" & ControlChars.CrLf
			sSQL &= "				, Similarity" & ControlChars.CrLf
			sSQL &= "				, MRSC.[001_Platform] AS [001_Platform]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[002_MobyRank] AS [002_MobyRank]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[003_MobyScore] AS [003_MobyScore]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[004_Publisher] AS [004_Publisher]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[005_Developer] AS [005_Developer]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[006_Year] AS [006_Year]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[101_Basic_Genres] AS [101_Basic_Genres]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[102_Perspectives] AS [102_Perspectives]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[103_Sports_Themes] AS [103_Sports_Themes]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[105_Educational_Categories] AS [105_Educational_Categories]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[106_Other_Attributes] AS [106_Other_Attributes]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[107_Visual_Presentation] AS [107_Visual_Presentation]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[108_Gameplay] AS [108_Gameplay]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[109_Pacing] AS [109_Pacing]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[110_Narrative_Theme_Topic] AS [110_Narrative_Theme_Topic]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[111_Setting] AS [111_Setting]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[112_Vehicular_Themes] AS [112_Vehicular_Themes]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[113_Interface_Control] AS [113_Interface_Control]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[114_DLC_Addon] AS [114_DLC_Addon]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[115_Special_Edition] AS [115_Special_Edition]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[201_MinPlayers] AS [201_MinPlayers]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[202_MaxPlayers] AS [202_MaxPlayers]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[203_AgeO] AS [203_AgeO]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[204_AgeP] AS [204_AgeP]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[205_Rating_Descriptors] AS [205_Rating_Descriptors]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[206_Other_Attributes] AS [206_Other_Attributes]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[207_Multiplayer_Attributes] AS [207_Multiplayer_Attributes]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[301_Group_Membership] AS [301_Group_Membership]" & ControlChars.CrLf
			sSQL &= "				, MRSC.[401_Staff] AS [401_Staff]" & ControlChars.CrLf
			sSQL &= "FROM		ttb_Moby_Releases_Similarity_Calculation MRSC" & ControlChars.CrLf
			sSQL &= "LEFT JOIN tbl_Moby_Releases MR ON MRSC.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
			sSQL &= "LEFT JOIN tbl_Moby_Games MG ON MR.id_Moby_Games = MG.id_Moby_Games" & ControlChars.CrLf
			sSQL &= "LEFT JOIN tbl_Moby_Platforms PLTFM ON MR.id_Moby_Platforms = PLTFM.id_Moby_Platforms" & ControlChars.CrLf
		End If

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)
	End Sub

	Public Shared Sub Fill_tbl_Similarity_Calculation_From_Results(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As tbl_Similarity_CalculationDataTable, ByVal id_Similarity_Calculation_Results As Integer)
		Dim sSQL As String = ""
		sSQL &= "	SELECT" & ControlChars.CrLf
		sSQL &= "					SCRE.id_Emu_Games AS id_Emu_Games" & ControlChars.CrLf
		sSQL &= "					, SCRE.id_Moby_Releases AS id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "					,	CASE	WHEN SCRE.id_Emu_Games IS NULL THEN IFNULL(MG.Name_Prefix || ' ', '') || IFNULL(MG.Name, '')" & ControlChars.CrLf
		sSQL &= "									WHEN EG.Name IS NULL" & ControlChars.CrLf
		sSQL &= "									THEN	CASE	WHEN MG.Name IS NULL " & ControlChars.CrLf
		sSQL &= "															THEN IFNULL(EG.InnerFile, EG.File)" & ControlChars.CrLf
		sSQL &= "															ELSE IFNULL(EG.Name_Prefix || ' ', IFNULL(MG.Name_Prefix || ' ', '')) || IFNULL(MG.Name, '') || IFNULL(' (' || EG.Note || ')', '')" & ControlChars.CrLf
		sSQL &= "												END" & ControlChars.CrLf
		sSQL &= "									ELSE	IFNULL(EG.Name_Prefix || ' ', '') || IFNULL(EG.Name, '') || IFNULL(' (' || EG.Note || ')', '')" & ControlChars.CrLf
		sSQL &= "					END" & ControlChars.CrLf
		sSQL &= "					AS Game" & ControlChars.CrLf
		sSQL &= "					, PLTFM.Name AS Platform" & ControlChars.CrLf
		sSQL &= "					, EG.Folder AS Folder" & ControlChars.CrLf
		sSQL &= "					, EG.File AS File" & ControlChars.CrLf
		sSQL &= "					, EG.InnerFile AS InnerFile" & ControlChars.CrLf
		sSQL &= "					, CASE WHEN SCRE.id_Emu_Games IS NULL THEN 0 ELSE 1 END AS Have" & ControlChars.CrLf
		sSQL &= "					, SCRE.Similarity" & ControlChars.CrLf
		sSQL &= "					, SCRE.[001_Platform] AS [001_Platform]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[002_MobyRank] AS [002_MobyRank]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[003_MobyScore] AS [003_MobyScore]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[004_Publisher] AS [004_Publisher]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[005_Developer] AS [005_Developer]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[006_Year] AS [006_Year]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[101_Basic_Genres] AS [101_Basic_Genres]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[102_Perspectives] AS [102_Perspectives]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[103_Sports_Themes] AS [103_Sports_Themes]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[105_Educational_Categories] AS [105_Educational_Categories]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[106_Other_Attributes] AS [106_Other_Attributes]" & ControlChars.CrLf
		sSQL &= "					,	SCRE.[107_Visual_Presentation] AS [107_Visual_Presentation]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[108_Gameplay] AS [108_Gameplay]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[109_Pacing] AS [109_Pacing]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[110_Narrative_Theme_Topic] AS [110_Narrative_Theme_Topic]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[111_Setting] AS [111_Setting]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[112_Vehicular_Themes] AS [112_Vehicular_Themes]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[113_Interface_Control] AS [113_Interface_Control]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[114_DLC_Addon] AS [114_DLC_Addon]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[115_Special_Edition] AS [115_Special_Edition]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[201_MinPlayers] AS [201_MinPlayers]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[202_MaxPlayers] AS [202_MaxPlayers]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[203_AgeO] AS [203_AgeO]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[204_AgeP] AS [204_AgeP]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[205_Rating_Descriptors] AS [205_Rating_DeSCREiptors]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[206_Other_Attributes] AS [206_Other_Attributes]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[207_Multiplayer_Attributes] AS [207_Multiplayer_Attributes]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[301_Group_Membership] AS [301_Group_Membership]" & ControlChars.CrLf
		sSQL &= "					, SCRE.[401_Staff] AS [401_Staff]" & ControlChars.CrLf
		sSQL &= "	FROM		tbl_Similarity_Calculation_Results_Entries SCRE" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Emu_Games EG ON SCRE.id_Emu_Games = EG.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Releases MR ON SCRE.id_Moby_Releases = MR.id_Moby_Releases --IFNULL(EG.id_Moby_Platforms_Alternative, EG.id_Moby_Platforms) = MR.id_Moby_Platforms AND MG.id_Moby_Games = MR.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Games MG ON MR.id_Moby_Games = MG.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Platforms PLTFM ON MR.id_Moby_Platforms = PLTFM.id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "	WHERE SCRE.id_Similarity_Calculation_Results = " & TC.getSQLFormat(id_Similarity_Calculation_Results) & ControlChars.CrLf

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)
	End Sub

	Public Shared Sub Fill_tbl_List_Generators(ByRef dt As tbl_List_GeneratorsDataTable, Optional ByVal id_List_Generators As Integer = 0, Optional ByVal OnlyRombase As Boolean = False)
		dt.Clear()
		Dim sSQL As String = ""

		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= "		-id_Rombase_List_Generators AS id_List_Generators" & ControlChars.CrLf
		sSQL &= "		, Name" & ControlChars.CrLf
		sSQL &= "		, Main_Template" & ControlChars.CrLf
		sSQL &= "		, File_Entry_Template" & ControlChars.CrLf
		sSQL &= "		, Sort" & ControlChars.CrLf
		sSQL &= "FROM	rombase.tbl_Rombase_List_Generators" & ControlChars.CrLf
		sSQL &= "WHERE	1=1" & ControlChars.CrLf
		sSQL &= IIf(id_List_Generators < 0, " AND id_Rombase_List_Generators = " & TC.getSQLFormat(-1 * id_List_Generators), "") & ControlChars.CrLf

		sSQL &= "UNION" & ControlChars.CrLf

		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= "		id_List_Generators" & ControlChars.CrLf
		sSQL &= "		, Name" & ControlChars.CrLf
		sSQL &= "		, Main_Template" & ControlChars.CrLf
		sSQL &= "		, File_Entry_Template" & ControlChars.CrLf
		sSQL &= "		, Sort" & ControlChars.CrLf
		sSQL &= "FROM	tbl_List_Generators" & ControlChars.CrLf
		sSQL &= "WHERE	" & IIf(OnlyRombase, "0=1", "1=1") & ControlChars.CrLf
		sSQL &= IIf(id_List_Generators > 0, " AND id_List_Generators = " & TC.getSQLFormat(id_List_Generators), "") & ControlChars.CrLf

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

	Public Shared Sub Fill_static_List_Generators_Sort(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As static_List_Generator_SortDataTable)
		dt.Clear()

		Dim sSQL As String = ""
		sSQL &= "SELECT	1 AS id_Sort, 'Ascending' AS Name" & ControlChars.CrLf
		sSQL &= "UNION" & ControlChars.CrLf
		sSQL &= "SELECT 2 As id_Sort, 'Descending' AS Name" & ControlChars.CrLf

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)
	End Sub

	Public Shared Sub Fill_src_ucr_Emulation_Moby_Releases_Screenshots(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As src_ucr_Emulation_Moby_Releases_ScreenshotsDataTable, ByVal id_Moby_Releases As Object)
		dt.Clear()

		If TC.IsNullNothingOrEmpty(id_Moby_Releases) Then
			Return
		End If

		Dim sSQL As String = ""
		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= "	MRS.id_Moby_Releases_Screenshots" & ControlChars.CrLf
		sSQL &= "	, MRS.URL" & ControlChars.CrLf
		sSQL &= "	, MRS.Description" & ControlChars.CrLf
		sSQL &= "	, MRS.Description AS tmp_Description" & ControlChars.CrLf
		sSQL &= "FROM moby.tbl_Moby_Releases_Screenshots AS MRS" & ControlChars.CrLf
		sSQL &= "LEFT JOIN tbl_Moby_Extras_Properties PROP ON PROP.id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases) & " AND PROP.id_Moby_Releases_Screenshots = MRS.id_Moby_Releases_Screenshots" & ControlChars.CrLf
		sSQL &= "WHERE	MRS.deprecated = 0" & ControlChars.CrLf
		sSQL &= "				AND MRS.id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases) & ControlChars.CrLf
		sSQL &= "				AND IFNULL(PROP.Show, 1) = 1" & ControlChars.CrLf
		sSQL &= "ORDER BY PROP.Sort" & ControlChars.CrLf

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)
	End Sub

	Public Shared Sub Fill_src_ucr_Emulation_Moby_Releases_Cover_Art(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As src_ucr_Emulation_Moby_Releases_Cover_ArtDataTable, ByVal id_Moby_Releases As Object)
		dt.Clear()

		If TC.IsNullNothingOrEmpty(id_Moby_Releases) Then
			Return
		End If

		Dim sSQL As String = ""
		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= "	MRCA.id_Moby_Releases_Cover_Art" & ControlChars.CrLf
		sSQL &= "	, MRCA.id_Moby_Cover_Art_Types" & ControlChars.CrLf
		sSQL &= "	, MRCA.URL" & ControlChars.CrLf
		sSQL &= "	, MRCA.Packaging" & ControlChars.CrLf
		sSQL &= "	, MRCA.Comment" & ControlChars.CrLf
		sSQL &= "	, MRCA.PackageComment" & ControlChars.CrLf
		sSQL &= "	, ''" & ControlChars.CrLf
		sSQL &= "		|| IFNULL(' ' || MRCA.Packaging, '')" & ControlChars.CrLf
		sSQL &= "		|| IFNULL(' (' || MCAT.Name || ')', '')" & ControlChars.CrLf
		sSQL &= "		|| IFNULL('\n' ||" & ControlChars.CrLf
		sSQL &= "			(" & ControlChars.CrLf
		sSQL &= "				SELECT GROUP_CONCAT(MR.Region, ', ')" & ControlChars.CrLf
		sSQL &= "				FROM moby.tbl_Moby_Releases_Cover_Art_Regions MRCAR" & ControlChars.CrLf
		sSQL &= "				INNER JOIN moby.tbl_Moby_Regions MR ON MRCAR.id_Moby_Regions = MR.id_Moby_Regions AND MRCAR.id_Moby_Releases_Cover_Art = MRCA.id_Moby_Releases_Cover_Art" & ControlChars.CrLf
		sSQL &= "				ORDER BY MR.sort" & ControlChars.CrLf
		sSQL &= "			)" & ControlChars.CrLf
		sSQL &= "			, '')" & ControlChars.CrLf
		sSQL &= "		AS tmp_Description" & ControlChars.CrLf
		sSQL &= "FROM moby.tbl_Moby_Releases_Cover_Art MRCA" & ControlChars.CrLf
		sSQL &= "LEFT JOIN moby.tbl_Moby_Cover_Art_Types MCAT ON MRCA.id_Moby_Cover_Art_Types = MCAT.id_Moby_Cover_Art_Types" & ControlChars.CrLf
		sSQL &= "LEFT JOIN tbl_Moby_Extras_Properties PROP ON PROP.id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases) & " AND PROP.id_Moby_Releases_Cover_Art = MRCA.id_Moby_Releases_Cover_Art" & ControlChars.CrLf
		sSQL &= "WHERE	deprecated = 0" & ControlChars.CrLf
		sSQL &= "				AND MRCA.id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases) & ControlChars.CrLf
		sSQL &= "				AND IFNULL(PROP.Show, 1) = 1" & ControlChars.CrLf
		sSQL &= "				ORDER BY PROP.Sort"

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)
	End Sub

	Public Shared Sub Fill_src_frm_MOBY_Extras_Manager(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As src_frm_MOBY_Extras_ManagerDataTable, ByVal id_Moby_Releases As Object)
		dt.Clear()

		If TC.IsNullNothingOrEmpty(id_Moby_Releases) Then
			Return
		End If

		Dim sSQL As String = ""
		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= " 'Cover Art' AS ExtraType" & ControlChars.CrLf
		sSQL &= "	, MRCA.id_Moby_Releases_Cover_Art AS id_Moby_Releases_Cover_Art" & ControlChars.CrLf
		sSQL &= "	, NULL AS id_Moby_Releases_Screenshots" & ControlChars.CrLf
		sSQL &= "	, MRCA.URL AS URL" & ControlChars.CrLf
		sSQL &= "	, ''" & ControlChars.CrLf
		sSQL &= "		|| IFNULL(' ' || MRCA.Packaging, '')" & ControlChars.CrLf
		sSQL &= "		|| IFNULL(' (' || MCAT.Name || ')', '')" & ControlChars.CrLf
		sSQL &= "		|| IFNULL(' ' ||" & ControlChars.CrLf
		sSQL &= "			(" & ControlChars.CrLf
		sSQL &= "				SELECT GROUP_CONCAT(MR.Region, ', ')" & ControlChars.CrLf
		sSQL &= "				FROM moby.tbl_Moby_Releases_Cover_Art_Regions MRCAR" & ControlChars.CrLf
		sSQL &= "				INNER JOIN moby.tbl_Moby_Regions MR ON MRCAR.id_Moby_Regions = MR.id_Moby_Regions AND MRCAR.id_Moby_Releases_Cover_Art = MRCA.id_Moby_Releases_Cover_Art" & ControlChars.CrLf
		sSQL &= "				ORDER BY MR.sort" & ControlChars.CrLf
		sSQL &= "			)" & ControlChars.CrLf
		sSQL &= "			, '')" & ControlChars.CrLf
		sSQL &= "		AS tmp_Description" & ControlChars.CrLf
		sSQL &= "	, PROP.Sort AS Sort" & ControlChars.CrLf
		sSQL &= "	, IFNULL(PROP.Show, 1) AS Show" & ControlChars.CrLf
		sSQL &= "	, PROP.id_Moby_Extras_Properties AS id_Moby_Extras_Properties" & ControlChars.CrLf
		sSQL &= "FROM moby.tbl_Moby_Releases_Cover_Art MRCA" & ControlChars.CrLf
		sSQL &= "LEFT JOIN moby.tbl_Moby_Cover_Art_Types MCAT ON MRCA.id_Moby_Cover_Art_Types = MCAT.id_Moby_Cover_Art_Types" & ControlChars.CrLf
		sSQL &= "LEFT JOIN tbl_Moby_Extras_Properties PROP ON PROP.id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases) & " AND PROP.id_Moby_Releases_Cover_Art = MRCA.id_Moby_Releases_Cover_Art" & ControlChars.CrLf
		sSQL &= "WHERE	MRCA.deprecated = 0" & ControlChars.CrLf
		sSQL &= "AND MRCA.id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases) & ControlChars.CrLf

		sSQL &= "UNION" & ControlChars.CrLf

		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= " 'Screenshots' AS ExtraType" & ControlChars.CrLf
		sSQL &= "	, NULL AS id_Moby_Releases_Cover_Art" & ControlChars.CrLf
		sSQL &= "	, MRS.id_Moby_Releases_Screenshots" & ControlChars.CrLf
		sSQL &= "	, MRS.URL" & ControlChars.CrLf
		sSQL &= "	, MRS.Description AS tmp_Description" & ControlChars.CrLf
		sSQL &= "	, PROP.Sort AS Sort" & ControlChars.CrLf
		sSQL &= "	, IFNULL(PROP.Show, 1) AS Show" & ControlChars.CrLf
		sSQL &= "	, PROP.id_Moby_Extras_Properties AS id_Moby_Extras_Properties" & ControlChars.CrLf
		sSQL &= "FROM moby.tbl_Moby_Releases_Screenshots MRS" & ControlChars.CrLf
		sSQL &= "LEFT JOIN tbl_Moby_Extras_Properties PROP ON PROP.id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases) & " AND PROP.id_Moby_Releases_Screenshots = MRS.id_Moby_Releases_Screenshots" & ControlChars.CrLf
		sSQL &= "WHERE	MRS.deprecated = 0" & ControlChars.CrLf
		sSQL &= "				AND MRS.id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases) & ControlChars.CrLf
		sSQL &= "ORDER BY ExtraType, Sort" & ControlChars.CrLf

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)
	End Sub

	Public Shared Sub Fill_tbl_Moby_Regions(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As tbl_Moby_RegionsDataTable)
		Dim sSQL As String = ""
		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= "	id_Moby_Regions" & ControlChars.CrLf
		sSQL &= "	, id_Moby_Regions_Owner" & ControlChars.CrLf
		sSQL &= "	, Region" & ControlChars.CrLf
		sSQL &= "	, Sort" & ControlChars.CrLf
		sSQL &= "	, 0 AS tmp_Apply" & ControlChars.CrLf
		sSQL &= "FROM moby.tbl_Moby_Regions" & ControlChars.CrLf
		sSQL &= "ORDER BY SORT, Region"

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)
	End Sub

	Public Shared Sub Fill_tbl_Total_DOS_Launcher_Configs(ByRef dt As tbl_Total_DOS_Launcher_ConfigsDataTable, Optional ByVal id_Total_DOS_Launcher_Configs As Int64 = 0)
		dt.Clear()
		Dim sSQL As String = ""

		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= "		-id_Rombase_Total_DOS_Launcher_Configs AS id_Total_DOS_Launcher_Configs" & ControlChars.CrLf
		sSQL &= "		, DisplayName" & ControlChars.CrLf
		sSQL &= "		, proglocations" & ControlChars.CrLf
		sSQL &= "		, cachelocation" & ControlChars.CrLf
		sSQL &= "		, userlevel" & ControlChars.CrLf
		sSQL &= "		, forcelogging" & ControlChars.CrLf
		sSQL &= "		, swapping" & ControlChars.CrLf
		sSQL &= "		, preloading" & ControlChars.CrLf
		sSQL &= "		, pauseafterrun" & ControlChars.CrLf
		sSQL &= "		, VESA" & ControlChars.CrLf
		sSQL &= "FROM	rombase.tbl_Rombase_Total_DOS_Launcher_Configs" & ControlChars.CrLf
		sSQL &= "WHERE	" & IIf(id_Total_DOS_Launcher_Configs > 0, "0=1", "1=1") & ControlChars.CrLf
		sSQL &= IIf(id_Total_DOS_Launcher_Configs < 0, " AND id_Rombase_Total_DOS_Launcher_Configs = " & TC.getSQLFormat(-1 * id_Total_DOS_Launcher_Configs), "") & ControlChars.CrLf

		sSQL &= "UNION" & ControlChars.CrLf

		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= "		id_Total_DOS_Launcher_Configs AS id_Total_DOS_Launcher_Configs" & ControlChars.CrLf
		sSQL &= "		, DisplayName" & ControlChars.CrLf
		sSQL &= "		, proglocations" & ControlChars.CrLf
		sSQL &= "		, cachelocation" & ControlChars.CrLf
		sSQL &= "		, userlevel" & ControlChars.CrLf
		sSQL &= "		, forcelogging" & ControlChars.CrLf
		sSQL &= "		, swapping" & ControlChars.CrLf
		sSQL &= "		, preloading" & ControlChars.CrLf
		sSQL &= "		, pauseafterrun" & ControlChars.CrLf
		sSQL &= "		, VESA" & ControlChars.CrLf
		sSQL &= "FROM	tbl_Total_DOS_Launcher_Configs" & ControlChars.CrLf
		sSQL &= "WHERE	" & IIf(id_Total_DOS_Launcher_Configs < 0, "0=1", "1=1") & ControlChars.CrLf
		sSQL &= IIf(id_Total_DOS_Launcher_Configs > 0, " AND id_Total_DOS_Launcher_Configs = " & TC.getSQLFormat(id_Total_DOS_Launcher_Configs), "") & ControlChars.CrLf

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

	Public Shared Sub Fill_tbl_Cheevo_Challenges(ByRef dt As tbl_Cheevo_ChallengesDataTable)
		dt.Clear()
		Dim sSQL As String = ""

		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= "		id_Cheevo_Challenges" & ControlChars.CrLf
		sSQL &= "		, Name" & ControlChars.CrLf
		sSQL &= "		, created" & ControlChars.CrLf
		sSQL &= "		, updated" & ControlChars.CrLf
		sSQL &= "FROM	tbl_Cheevo_Challenges" & ControlChars.CrLf
		sSQL &= "ORDER BY Name"

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

	Public Shared Sub Fill_tbl_Cheevo_Challenges_Cheevos(ByRef dt As tbl_Cheevo_Challenges_CheevosDataTable, Optional ByVal id_Cheevo_Challenges As Int64 = 0, Optional ByVal Tier As Int64 = 0)
		dt.Clear()
		Dim sSQL As String = ""

		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= "		id_Cheevo_Challenges_Cheevos" & ControlChars.CrLf
		sSQL &= "		, IFNULL(CheevoType, 0) AS CheevoType" & ControlChars.CrLf
		sSQL &= "		, id_Cheevo_Challenges" & ControlChars.CrLf
		sSQL &= "		, Tier" & ControlChars.CrLf
		sSQL &= "		, id_Emu_Games" & ControlChars.CrLf
		sSQL &= "		, Cheevo_GameName" & ControlChars.CrLf
		sSQL &= "		, Cheevo_ID" & ControlChars.CrLf
		sSQL &= "		, Cheevo_Title" & ControlChars.CrLf
		sSQL &= "		, Cheevo_Description" & ControlChars.CrLf
		sSQL &= "		, Cheevo_Points" & ControlChars.CrLf
		sSQL &= "		, Cheevo_BadgeName" & ControlChars.CrLf
		sSQL &= "		, Cheevo_Flags" & ControlChars.CrLf
		sSQL &= "		, Hardcore" & ControlChars.CrLf
		sSQL &= "		, Runtime" & ControlChars.CrLf
		sSQL &= "		, created" & ControlChars.CrLf
		sSQL &= "		, updated" & ControlChars.CrLf
		'sSQL &= "		, Cheevo_Title || CASE WHEN IFNULL(Hardcore, 0) = 1 THEN ' [Hardcore]' ELSE ' [Casual]' END AS tmp_Cheevo_Title_with_Mode" & ControlChars.CrLf		sSQL &= "		, Cheevo_Title || CASE WHEN IFNULL(Hardcore, 0) = 1 THEN ' [Hardcore]' ELSE ' [Casual]' END AS tmp_Cheevo_Title_with_Mode" & ControlChars.CrLf
		sSQL &= "		, Cheevo_Title || CASE WHEN IFNULL(CheevoType, 0) = 0 THEN CASE WHEN IFNULL(Hardcore, 0) = 1 THEN ' [Hardcore]' ELSE ' [Casual]' END ELSE '' END AS tmp_Cheevo_Title_with_Mode" & ControlChars.CrLf

		sSQL &= "FROM	tbl_Cheevo_Challenges_Cheevos" & ControlChars.CrLf
		sSQL &= "WHERE  1=1" & ControlChars.CrLf
		If id_Cheevo_Challenges <> 0 Then
			sSQL &= "   AND id_Cheevo_Challenges = " & TC.getSQLFormat(id_Cheevo_Challenges) & ControlChars.CrLf
		End If
		If Tier > 0 Then
			sSQL &= "   AND Tier = " & TC.getSQLFormat(Tier) & ControlChars.CrLf
		End If
		sSQL &= "ORDER BY created" & ControlChars.CrLf

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

	Public Shared Sub Fill_ttb_Cheevo_Challenges_Tiers(ByRef dt As ttb_Cheevo_Challenges_TiersDataTable, ByVal id_Cheevo_Challenges As Int64)
		dt.Clear()
		Dim sSQL As String = ""

		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= "   " & TC.getSQLFormat(id_Cheevo_Challenges) & " AS id_Cheevo_Challenges" & ControlChars.CrLf
		sSQL &= "   , Tier" & ControlChars.CrLf
		sSQL &= "   , 'Tier ' + Tier AS Displayname" & ControlChars.CrLf
		sSQL &= "FROM" & ControlChars.CrLf
		sSQL &= "(SELECT DISTINCT Tier FROM tbl_Cheevo_Challenges_Cheevos WHERE id_Cheevo_Challenges = " & TC.getSQLFormat(id_Cheevo_Challenges) & ") SQ" & ControlChars.CrLf
		sSQL &= "ORDER BY Tier ASC" & ControlChars.CrLf

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

	Public Shared Sub Fill_ttb_Open_Challenges(ByRef dt As ttb_Open_ChallengesDataTable)
		dt.Clear()
		Dim sSQL As String = ""

		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= "	CC.id_Cheevo_Challenges AS id_Cheevo_Challenges" & ControlChars.CrLf
		sSQL &= "	, CC.Name AS Name" & ControlChars.CrLf
		sSQL &= "	, (" & ControlChars.CrLf
		sSQL &= "		SELECT" & ControlChars.CrLf
		sSQL &= "			MIN(Tier)" & ControlChars.CrLf
		sSQL &= "		FROM tbl_Cheevo_Challenges_Cheevos CCC" & ControlChars.CrLf
		sSQL &= "		LEFT JOIN tbl_Users_Cheevo_Challenges_Cheevos UCCC ON CCC.id_Cheevo_Challenges_Cheevos = UCCC.id_Cheevo_Challenges_Cheevos" & ControlChars.CrLf
		sSQL &= IIf(cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0, " AND id_Users = " & TC.getSQLFormat(cls_Globals.id_Users), " AND id_Users IS NULL") & ControlChars.CrLf
		sSQL &= "		WHERE	CCC.id_Cheevo_Challenges = CC.id_Cheevo_Challenges" & ControlChars.CrLf
		sSQL &= "				AND UCCC.id_Users_Cheevo_Challenges_Cheevos IS NULL" & ControlChars.CrLf
		sSQL &= "				AND (" & ControlChars.CrLf
		sSQL &= "							(IFNULL(CCC.Hardcore, 0) = 0 AND IFNULL(UCCC.Unlocked_Casual, 0) = 0)" & ControlChars.CrLf
		sSQL &= "							OR (IFNULL(CCC.Hardcore, 0) = 1 AND IFNULL(UCCC.Unlocked_Hardcore, 0) = 0)" & ControlChars.CrLf
		sSQL &= "				)" & ControlChars.CrLf
		sSQL &= "	)" & ControlChars.CrLf
		sSQL &= "	AS Tier" & ControlChars.CrLf
		sSQL &= "	, (SELECT MAX(Tier) FROM tbl_Cheevo_Challenges_Cheevos CCC WHERE CCC.id_Cheevo_Challenges = CC.id_Cheevo_Challenges) AS MaxTier" & ControlChars.CrLf
		sSQL &= "	, 1 AS Sort" & ControlChars.CrLf
		sSQL &= "FROM tbl_Cheevo_Challenges CC" & ControlChars.CrLf
		sSQL &= "WHERE CC.id_Cheevo_Challenges IN (" & ControlChars.CrLf
		sSQL &= "	SELECT" & ControlChars.CrLf
		sSQL &= "		id_Cheevo_Challenges" & ControlChars.CrLf
		sSQL &= "	FROM tbl_Cheevo_Challenges_Cheevos CCC" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Users_Cheevo_Challenges_Cheevos UCCC ON CCC.id_Cheevo_Challenges_Cheevos = UCCC.id_Cheevo_Challenges_Cheevos" & ControlChars.CrLf
		sSQL &= IIf(cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0, " AND id_Users = " & TC.getSQLFormat(cls_Globals.id_Users), " AND id_Users IS NULL") & ControlChars.CrLf
		sSQL &= "	WHERE	UCCC.id_Users_Cheevo_Challenges_Cheevos IS NULL" & ControlChars.CrLf
		sSQL &= "			OR (IFNULL(CCC.Hardcore, 0) = 0 AND IFNULL(UCCC.Unlocked_Casual, 0) = 0)" & ControlChars.CrLf
		sSQL &= "			OR (IFNULL(CCC.Hardcore, 0) = 1 AND IFNULL(UCCC.Unlocked_Hardcore, 0) = 0)" & ControlChars.CrLf
		sSQL &= ")" & ControlChars.CrLf

		sSQL &= "	UNION" & ControlChars.CrLf
		sSQL &= "	SELECT" & ControlChars.CrLf
		sSQL &= "	0 AS id_Cheevo_Challenges" & ControlChars.CrLf
		sSQL &= "	, 'None' AS Name" & ControlChars.CrLf
		sSQL &= "	, 0 AS Tier" & ControlChars.CrLf
		sSQL &= "	, 0 AS MaxTier" & ControlChars.CrLf
		sSQL &= "	, 0 AS Sort" & ControlChars.CrLf

		'Always UNION Completed Challenges
		sSQL &= "UNION" & ControlChars.CrLf
		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= "	CC.id_Cheevo_Challenges AS id_Cheevo_Challenges" & ControlChars.CrLf
		sSQL &= "	, CC.Name AS Name" & ControlChars.CrLf
		sSQL &= "	, -1 AS Tier" & ControlChars.CrLf
		sSQL &= "	, (SELECT MAX(Tier) FROM tbl_Cheevo_Challenges_Cheevos CCC WHERE CCC.id_Cheevo_Challenges = CC.id_Cheevo_Challenges) AS MaxTier" & ControlChars.CrLf
		sSQL &= "	, 2 AS Sort" & ControlChars.CrLf
		sSQL &= "FROM tbl_Cheevo_Challenges CC" & ControlChars.CrLf
		sSQL &= "WHERE CC.id_Cheevo_Challenges NOT IN (" & ControlChars.CrLf
		sSQL &= "	SELECT" & ControlChars.CrLf
		sSQL &= "		id_Cheevo_Challenges" & ControlChars.CrLf
		sSQL &= "	FROM tbl_Cheevo_Challenges_Cheevos CCC" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Users_Cheevo_Challenges_Cheevos UCCC ON CCC.id_Cheevo_Challenges_Cheevos = UCCC.id_Cheevo_Challenges_Cheevos" & ControlChars.CrLf
		sSQL &= IIf(cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0, " AND id_Users = " & TC.getSQLFormat(cls_Globals.id_Users), " AND id_Users IS NULL") & ControlChars.CrLf
		sSQL &= "	WHERE	UCCC.id_Users_Cheevo_Challenges_Cheevos IS NULL" & ControlChars.CrLf
		sSQL &= "			OR (IFNULL(CCC.Hardcore, 0) = 0 AND IFNULL(UCCC.Unlocked_Casual, 0) = 0)" & ControlChars.CrLf
		sSQL &= "			OR (IFNULL(CCC.Hardcore, 0) = 1 AND IFNULL(UCCC.Unlocked_Hardcore, 0) = 0)" & ControlChars.CrLf
		sSQL &= ")" & ControlChars.CrLf

		sSQL &= "ORDER BY Sort, Name"

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)

		If cls_Globals.id_Cheevo_Challenges > 0 Then
			'User is bound to a challenge and this challenge may not be included (because it is already completed)
			Dim isChallengeInTable As Boolean = False
			For Each row As DataRow In dt.Rows
				If row("id_Cheevo_Challenges") = cls_Globals.id_Cheevo_Challenges Then
					isChallengeInTable = True
				End If
			Next

			If Not isChallengeInTable Then
				sSQL = ""

				sSQL &= "SELECT" & ControlChars.CrLf
				sSQL &= "	CC.id_Cheevo_Challenges AS id_Cheevo_Challenges" & ControlChars.CrLf
				sSQL &= "	, CC.Name AS Name" & ControlChars.CrLf
				sSQL &= "	, -1 AS Tier" & ControlChars.CrLf
				sSQL &= "	, (SELECT MAX(Tier) FROM tbl_Cheevo_Challenges_Cheevos CCC WHERE CCC.id_Cheevo_Challenges = CC.id_Cheevo_Challenges) AS MaxTier" & ControlChars.CrLf
				sSQL &= "	, 1 AS Sort" & ControlChars.CrLf
				sSQL &= "FROM tbl_Cheevo_Challenges CC" & ControlChars.CrLf
				sSQL &= "WHERE CC.id_Cheevo_Challenges = " & TC.getSQLFormat(cls_Globals.id_Cheevo_Challenges)

				MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
			End If
		End If

		For Each row As DataRow In dt.Rows
			row("DisplayText") = row("Name") & IIf(row("Tier") > 0, " Tier " & row("Tier") & " of " & row("MaxTier"), "") & IIf(row("Tier") = -1, " (completed)", "")
		Next
	End Sub

	Public Shared Sub Fill_ttb_Open_Challenges_Cheevos(ByRef dt As ttb_Open_Challenges_CheevosDataTable)
		dt.Clear()
		Dim sSQL As String = ""

		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= "	CCC.id_Cheevo_Challenges_Cheevos" & ControlChars.CrLf
		sSQL &= "	, CCC.id_Cheevo_Challenges" & ControlChars.CrLf
		sSQL &= "	, IFNULL(CCC.CheevoType, 0) AS CheevoType" & ControlChars.CrLf
		sSQL &= "	, UCCC.id_Users_Cheevo_Challenges_Cheevos" & ControlChars.CrLf
		sSQL &= "	, CC.Name AS Challenge_Name" & ControlChars.CrLf
		sSQL &= "	, CCC.Tier" & ControlChars.CrLf
		sSQL &= "	, CCC.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "	, CCC.Cheevo_GameName" & ControlChars.CrLf
		sSQL &= "	, CCC.Cheevo_ID" & ControlChars.CrLf
		sSQL &= "	, CCC.Cheevo_Title || CASE WHEN IFNULL(CCC.CheevoType, 0) = 0 THEN CASE WHEN IFNULL(CCC.Hardcore, 0) = 1 THEN ' [Hardcore]' ELSE ' [Casual]' END ELSE '' END AS Cheevo_Title" & ControlChars.CrLf
		sSQL &= "	, CCC.Cheevo_Description" & ControlChars.CrLf
		sSQL &= "	, CCC.Hardcore" & ControlChars.CrLf
		sSQL &= "	, CCC.Runtime" & ControlChars.CrLf
		sSQL &= "	, UCCC.Unlocked_Casual" & ControlChars.CrLf
		sSQL &= "	, UCCC.Unlocked_Hardcore" & ControlChars.CrLf
		sSQL &= "FROM tbl_Cheevo_Challenges_Cheevos CCC" & ControlChars.CrLf
		sSQL &= "LEFT JOIN tbl_Users_Cheevo_Challenges_Cheevos UCCC ON CCC.id_Cheevo_Challenges_Cheevos = UCCC.id_Cheevo_Challenges_Cheevos" & ControlChars.CrLf
		sSQL &= IIf(cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0, " AND id_Users = " & TC.getSQLFormat(cls_Globals.id_Users), " AND id_Users IS NULL") & ControlChars.CrLf
		sSQL &= "INNER JOIN tbl_Cheevo_Challenges CC ON CCC.id_Cheevo_Challenges = CC.id_Cheevo_Challenges" & ControlChars.CrLf
		sSQL &= "WHERE	UCCC.id_Users_Cheevo_Challenges_Cheevos IS NULL" & ControlChars.CrLf
		sSQL &= "		OR (IFNULL(CCC.Hardcore, 0) = 0 AND IFNULL(UCCC.Unlocked_Casual, 0) = 0)" & ControlChars.CrLf
		sSQL &= "		OR (IFNULL(CCC.Hardcore, 0) = 1 AND IFNULL(UCCC.Unlocked_Hardcore, 0) = 0)" & ControlChars.CrLf

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, dt)
	End Sub

	Public Sub Fill_ttb_Emulators_Pre_Post_Launch_Commands(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DataTable, ByVal id_Emulators As Int64, ByVal isPreLaunch As Boolean)
		dt.Clear()

		Dim sSQL As String = ""
		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= "	id_Emulators_Pre_Post_Launch_Commands" & ControlChars.CrLf
		sSQL &= "	, id_Emulators" & ControlChars.CrLf
		sSQL &= "	, Sort" & ControlChars.CrLf
		sSQL &= "	, Directory" & ControlChars.CrLf
		sSQL &= "	, Executable" & ControlChars.CrLf
		sSQL &= "	, Parameter" & ControlChars.CrLf
		sSQL &= "	, Minimized" & ControlChars.CrLf
		sSQL &= "	, WaitForExit" & ControlChars.CrLf
		sSQL &= "FROM		tbl_Emulators_Pre_Post_Launch_Commands EPPLC" & ControlChars.CrLf
		sSQL &= "WHERE	" & IIf(isPreLaunch, "PreLaunch", "PostLaunch") & " = 1" & ControlChars.CrLf
		sSQL &= "				AND id_Emulators = " & TC.getSQLFormat(id_Emulators) & ControlChars.CrLf
		sSQL &= "ORDER BY Sort" & ControlChars.CrLf

		DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL, dt, tran)
	End Sub

#End Region

#Region "Upsert Statements"
	''' <summary>
	''' INSERT or UPDATE tbl_Emu_Games, on INSERT the id_Emu_Games will be overwritten by the correct value from DB
	''' </summary>
	''' <param name="tran"></param>
	''' <param name="row"></param>
	''' <remarks></remarks>
	Public Sub Upsert_Rom_Manager_tbl_Emu_Games(ByRef tran As SQLite.SQLiteTransaction, ByRef row As DataRow)
		Dim Column_Blacklist As String() = {"ROMBASE_id_Moby_Platforms", "id_Emu_Games", "Rating_Gameplay", "Rating_Graphics", "Rating_Sound", "Rating_Story", "Rating_Personal", "Num_Played", "Num_Runtime", "deprecated"}

		Dim id_Emu_Games As Object = Nothing
		If TC.NZ(row("id_Emu_Games"), 0) > 0 Then
			id_Emu_Games = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Emu_Games FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(row("id_Emu_Games")), tran)
		End If

		Dim bFirst As Boolean = True

		If TC.NZ(id_Emu_Games, 0) > 0 Then
			'Update
			sSQL = "UPDATE tbl_Emu_Games SET "

			For Each col As DataColumn In row.Table.Columns
				If Not Column_Blacklist.Contains(col.ColumnName) AndAlso Not col.ColumnName.StartsWith("tmp") Then
					If bFirst Then
						sSQL &= "	"
						bFirst = False
					Else
						sSQL &= "	, "
					End If
					sSQL &= col.ColumnName & " = " & TC.getSQLFormat(row(col.ColumnName))
				End If
			Next

			sSQL &= " WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games)

			DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)

			Return
		End If

		'Insert
		sSQL = "INSERT INTO tbl_Emu_Games ("
		Dim sSQL2 As String = " VALUES ("

		For Each col As DataColumn In row.Table.Columns
			If Not Column_Blacklist.Contains(col.ColumnName) AndAlso Not TC.IsNullNothingOrEmpty(row(col.ColumnName)) Then
				If bFirst Then
					sSQL &= "	"
					sSQL2 &= "	"
					bFirst = False
				Else
					sSQL &= "	, "
					sSQL2 &= "	, "
				End If
				sSQL &= col.ColumnName
				sSQL2 &= TC.getSQLFormat(row(col.ColumnName))
			End If
		Next


		sSQL &= ")"
		sSQL2 &= "); SELECT last_insert_rowid()"

		'Insert
		id_Emu_Games = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, sSQL & sSQL2, tran)
		row("id_Emu_Games") = id_Emu_Games
		row.AcceptChanges()
	End Sub

	Public Shared Sub Upsert_MAME_tbl_Emu_Games(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Moby_Platforms As Integer, ByVal File As Object, ByVal InnerFile As Object, ByVal Name As Object, ByVal Name_Prefix As Object, ByVal Note As Object, ByVal Developer As Object, ByVal Description As Object, ByVal Year As Object)
		Dim id_Emu_Games As Object = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Emu_Games FROM tbl_Emu_Games WHERE InnerFile = " & TC.getSQLFormat(InnerFile) & " AND " & " id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms), tran)

		Dim sSQL As String = ""

		If TC.NZ(id_Emu_Games, 0) <> 0 Then
			sSQL &= "	UPDATE tbl_Emu_Games"
			sSQL &= "	SET"
			sSQL &= "		id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms)
			sSQL &= "		, File = " & TC.getSQLFormat(File)
			sSQL &= "		, InnerFile = " & TC.getSQLFormat(InnerFile)
			sSQL &= "		, Name = " & TC.getSQLFormat(Name)
			sSQL &= "		, Name_Prefix = " & TC.getSQLFormat(Name_Prefix)
			sSQL &= "		, Note = " & TC.getSQLFormat(Note)
			sSQL &= "		, Developer = " & TC.getSQLFormat(Developer)
			sSQL &= "		, Description = " & TC.getSQLFormat(Description)
			sSQL &= "		, Year = " & TC.getSQLFormat(Year)
			sSQL &= " WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games)

			DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)

			Return
		End If

		'INSERT
		sSQL &= "	INSERT INTO tbl_Emu_Games"
		sSQL &= "	("
		sSQL &= "		id_Moby_Platforms"
		sSQL &= "		, File"
		sSQL &= "		, InnerFile"
		sSQL &= "		, Name"
		sSQL &= "		, Name_Prefix"
		sSQL &= "		, Note"
		sSQL &= "		, Developer"
		sSQL &= "		, Description"
		sSQL &= "		, Year"
		sSQL &= "	)"
		sSQL &= "	VALUES"
		sSQL &= "	("
		sSQL &= TC.getSQLParameter(id_Moby_Platforms, File, InnerFile, Name, Name_Prefix, Note, Developer, Description, Year)
		sSQL &= "	)"

		DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
	End Sub

	Public Shared Sub Upsert_tbl_ControlSettings(ByRef tran As SQLite.SQLiteTransaction, ByVal ControlID As String, ByVal SettingID As String, ByVal Setting As String)
		If Setting.Length > 0 Then
			DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_ControlSettings WHERE HostName = " & TC.getSQLFormat(System.Environment.MachineName) & " AND ControlID = " & TC.getSQLFormat(ControlID) & " AND SettingID = " & TC.getSQLFormat(SettingID) & IIf(cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0, " AND id_Users = " & TC.getSQLFormat(cls_Globals.id_Users), " AND id_Users IS NULL"), tran)

			DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_ControlSettings (ControlID, SettingID, Setting, HostName, id_Users) VALUES (" & TC.getSQLParameter(ControlID, SettingID, Setting, System.Environment.MachineName, IIf(cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0, cls_Globals.id_Users, DBNull.Value)) & ")", tran)
		End If
	End Sub

	Public Shared Function Upsert_tbl_Filtersets(ByRef tran As SQLite.SQLiteTransaction, ByVal id_FilterSets As Object, ByVal Type As enm_FilterSetTypes, ByVal Name As String, ByVal ApplyGridFilter As Boolean, ByVal GridFilter As Object) As Integer
		If TC.NZ(id_FilterSets, 0) > 0 Then
			'Update
			DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_FilterSets SET Type = " & TC.getSQLFormat(Type) & ", Name = " & TC.getSQLFormat(Name) & ", ApplyGridFilter = " & TC.getSQLFormat(ApplyGridFilter) & ", GridFilter = " & TC.getSQLFormat(GridFilter) & " WHERE id_FilterSets = " & TC.getSQLFormat(id_FilterSets), tran)
			Return id_FilterSets
		Else
			'Insert
			Return DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "INSERT INTO tbl_FilterSets (Type, Name, ApplyGridFilter, GridFilter, id_Users) VALUES (" & TC.getSQLParameter(Type, Name, ApplyGridFilter, GridFilter, IIf(cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0, cls_Globals.id_Users, DBNull.Value)) & "); SELECT last_insert_rowid()", tran)
		End If
	End Function

	Public Shared Function Upsert_tbl_Emu_Extras(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Emu_Extras As Object, ByVal Name As String, ByVal Sort As Object, ByVal Description As Object, ByVal Hide As Object) As Integer
		If TC.NZ(id_Emu_Extras, 0) > 0 Then
			'Update
			DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Emu_Extras SET Name = " & TC.getSQLFormat(Name) & ", Sort = " & TC.getSQLFormat(Sort) & ", Description = " & TC.getSQLFormat(Description) & ", Hide = " & TC.getSQLFormat(Hide) & " WHERE id_Emu_Extras = " & id_Emu_Extras, tran)
			Return id_Emu_Extras
		Else
			'Insert
			Return DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "INSERT INTO tbl_Emu_Extras (Name, Sort, Description, Hide) VALUES (" & TC.getSQLParameter(Name, Sort, Description, Hide) & "); SELECT last_insert_rowid()", tran)
		End If
	End Function

	Public Shared Function Update_tbl_Emu_Games_Ratings(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Emu_Games As Object, ByVal Rating_Gameplay As Object, ByVal Rating_Graphics As Object, ByVal Rating_Personal As Object, ByVal Rating_Sound As Object, ByVal Rating_Story As Object) As Boolean
		If TC.NZ(id_Emu_Games, 0) <= 0 Then Return False

		If TC.NZ(Rating_Gameplay, 0) = 0 Then Rating_Gameplay = DBNull.Value
		If TC.NZ(Rating_Graphics, 0) = 0 Then Rating_Graphics = DBNull.Value
		If TC.NZ(Rating_Personal, 0) = 0 Then Rating_Personal = DBNull.Value
		If TC.NZ(Rating_Sound, 0) = 0 Then Rating_Sound = DBNull.Value
		If TC.NZ(Rating_Story, 0) = 0 Then Rating_Story = DBNull.Value

		Return DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Emu_Games SET Rating_Gameplay = " & TC.getSQLFormat(Rating_Gameplay) & ", Rating_Graphics = " & TC.getSQLFormat(Rating_Graphics) & ", Rating_Personal = " & TC.getSQLFormat(Rating_Personal) & ", Rating_Sound = " & TC.getSQLFormat(Rating_Sound) & ", Rating_Story = " & TC.getSQLFormat(Rating_Story) & " WHERE id_Emu_Games = " & id_Emu_Games, tran)
	End Function

	Public Sub Update_tbl_Emu_Games_Rating_Weights(ByRef tran As SQLite.SQLiteTransaction, ByRef row As DataRow)
		If TC.NZ(row("id_Emu_Games_Rating_Weights"), 0) <= 0 Then Return

		MKNetLib.cls_MKSQLiteDataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Emu_Games_Rating_Weights SET Weight = " & TC.getSQLFormat(row("Weight")) & " WHERE id_Emu_Games_Rating_Weights = " & TC.getSQLFormat(row("id_Emu_Games_Rating_Weights")))
	End Sub

	Public Function Upsert_tbl_Emu_Games_Moby_Genres(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Emu_Games As Integer, ByVal id_Moby_Genres As Integer, ByVal Used As Boolean) As Integer
		Dim id_Emu_Games_Moby_Genres As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Emu_Games_Moby_Genres FROM tbl_Emu_Games_Moby_Genres WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND id_Moby_Genres = " & TC.getSQLFormat(id_Moby_Genres), tran), 0)
		If id_Emu_Games_Moby_Genres <> 0 Then
			id_Emu_Games_Moby_Genres = DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Emu_Games_Moby_Genres SET USR = " & TC.getSQLFormat(True) & ", Used = " & TC.getSQLFormat(Used) & " WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND id_Moby_Genres = " & TC.getSQLFormat(id_Moby_Genres), tran)
			Return id_Emu_Games_Moby_Genres
		End If

		Return DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Emu_Games_Moby_Genres (id_Emu_Games, id_Moby_Genres, Used, USR) VALUES (" & TC.getSQLParameter(id_Emu_Games, id_Moby_Genres, Used, True) & "); SELECT last_insert_rowid()", tran)
	End Function

	Public Function Upsert_tbl_Emu_Games_Moby_Attributes(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Emu_Games As Integer, ByVal id_Moby_Attributes As Integer, ByVal Used As Boolean) As Integer
		Dim id_Emu_Games_Moby_Attributes As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Emu_Games_Moby_Attributes FROM tbl_Emu_Games_Moby_Attributes WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND id_Moby_Attributes = " & TC.getSQLFormat(id_Moby_Attributes), tran), 0)
		If id_Emu_Games_Moby_Attributes <> 0 Then
			id_Emu_Games_Moby_Attributes = DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Emu_Games_Moby_Attributes SET USR = " & TC.getSQLFormat(True) & ", Used = " & TC.getSQLFormat(Used) & " WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND id_Moby_Attributes = " & TC.getSQLFormat(id_Moby_Attributes), tran)
			Return id_Emu_Games_Moby_Attributes
		End If

		Return DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Emu_Games_Moby_Attributes (id_Emu_Games, id_Moby_Attributes, Used, USR) VALUES (" & TC.getSQLParameter(id_Emu_Games, id_Moby_Attributes, Used, True) & "); SELECT last_insert_rowid()", tran)
	End Function

	Public Function Upsert_tbl_Tag_Parser(ByRef tran As SQLite.SQLiteTransaction, ByRef row As DS_ML.tbl_Tag_ParserRow) As Integer
		Dim bFirst As Boolean = True

		If row("id_Tag_Parser") < 0 Then
			Dim sSQL As String = "INSERT INTO tbl_Tag_Parser ("
			Dim sSQL2 As String = " VALUES ("


			For Each col As DataColumn In row.Table.Columns
				If Not {"id_tag_parser", "id_rombase_tag_parser"}.Contains(col.ColumnName.ToLower) Then
					If bFirst Then
						bFirst = False
					Else
						sSQL &= ", "
						sSQL2 &= ", "
					End If

					sSQL &= col.ColumnName
					sSQL2 &= TC.getSQLFormat(row(col.ColumnName))
				End If
			Next

			sSQL &= ") "
			sSQL2 &= "); SELECT last_insert_rowid()"

			Return DataAccess.FireProcedure(tran.Connection, 0, sSQL & sSQL2, tran)
		End If


		Dim sSQLUpdate As String = "UPDATE tbl_Tag_Parser SET "

		For Each col As DataColumn In row.Table.Columns
			If Not {"id_tag_parser", "id_rombase_tag_parser"}.Contains(col.ColumnName.ToLower) Then
				If bFirst Then
					bFirst = False
				Else
					sSQLUpdate &= ", "
				End If

				sSQLUpdate &= col.ColumnName & " = " & TC.getSQLFormat(row(col.ColumnName))
			End If
		Next

		sSQLUpdate &= " WHERE id_Tag_Parser = " & TC.getSQLFormat(row("id_Tag_Parser"))

		Return DataAccess.FireProcedure(tran.Connection, 0, sSQLUpdate, tran)
	End Function

	Public Sub Upsert_tbl_Emu_Games_Languages(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DS_ML.tbl_Emu_Games_LanguagesDataTable, ByVal id_Emu_Games As Integer)
		'Clean up
		DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Languages WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games))

		'Insert
		Dim rows_languages() As DataRow = dt.Select("id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games))
		For Each row As DataRow In rows_languages
			DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Emu_Games_Languages (id_Emu_Games, id_Languages) VALUES (" & TC.getSQLParameter(row("id_Emu_Games"), row("id_Languages")) & ")", tran)
		Next
	End Sub

	Public Sub Upsert_tbl_Emu_Games_Regions(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DS_ML.tbl_Emu_Games_RegionsDataTable, ByVal id_Emu_Games As Integer)
		'Clean up
		DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Regions WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games))

		'Insert
		Dim rows_Regions() As DataRow = dt.Select("id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games))
		For Each row As DataRow In rows_Regions
			DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Emu_Games_Regions (id_Emu_Games, id_Regions) VALUES (" & TC.getSQLParameter(row("id_Emu_Games"), row("id_Regions")) & ")", tran)
		Next
	End Sub

	Public Shared Function Insert_tbl_History(ByRef tran As SQLite.SQLiteTransaction, ByVal Start As DateTime, ByVal [End] As DateTime, Optional ByVal id_Emu_Games As Object = Nothing) As Integer
		Return DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "INSERT INTO tbl_History (Start, End, id_Emu_Games" & IIf(Not cls_Globals.Admin, ", id_Users", "") & ") VALUES (" & TC.getSQLParameter(Start, [End], id_Emu_Games) & IIf(Not cls_Globals.Admin, ", " & TC.getSQLFormat(cls_Globals.id_Users), "") & "); SELECT last_insert_rowid()", tran)
	End Function

	Public Shared Function Upsert_tbl_DOSBox_Configs_Templates(ByRef tran As SQLite.SQLiteTransaction, ByRef row As DS_ML.tbl_DOSBox_ConfigsRow, Optional ByVal Create_Duplicate As Boolean = False, Optional ByVal New_Template_Name As String = "") As Int64
		Dim tbl_RB As New DS_ML.tbl_DOSBox_ConfigsDataTable

		If Not Create_Duplicate Then
			If TC.NZ(row("id_Rombase_DOSBox_Configs"), 0) > 0 Then
				DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT * FROM rombase.tbl_Rombase_DOSBox_Configs WHERE id_Rombase_DOSBOX_Configs = " & TC.getSQLFormat(row("id_Rombase_DOSBox_Configs")), tbl_RB, tran)
			End If
		End If

		Dim row_RB As DataRow = Nothing
		If tbl_RB.Rows.Count = 1 Then
			row_RB = tbl_RB.Rows(0)
		End If

		Dim id_DOSBox_Configs As Int64 = 0

		'Insert a new default DOSBox Config or Update an existing one
		'Only add values if they differ from row_RB
		If Not Create_Duplicate AndAlso TC.NZ(row("id_DOSBox_Configs"), 0) > 0 Then
			'Update
			Dim sSQL As String = ""
			sSQL &= "	UPDATE tbl_DOSBox_Configs SET" & ControlChars.CrLf

			sSQL &= "	isTemplate = " & TC.getSQLFormat(1) & ControlChars.CrLf
			sSQL &= "	, id_Rombase_DOSBox_Configs = " & TC.getSQLFormat(row("id_Rombase_DOSBox_Configs")) & ControlChars.CrLf

			For Each col As DataColumn In row.Table.Columns
				If Not {"id_DOSBox_Configs", "id_Rombase_DOSBox_Configs"}.Contains(col.ColumnName) Then
					sSQL &= "	, [" & col.ColumnName & "] = " & IIf(row_RB IsNot Nothing AndAlso MKNetLib.cls_MKSQLDataAccess.HasColumn(row_RB, col.ColumnName) AndAlso Equals(row(col.ColumnName), row_RB(col.ColumnName)), TC.getSQLFormat(DBNull.Value), TC.getSQLFormat(row(col.ColumnName))) & ControlChars.CrLf
				End If
			Next

			sSQL &= "	WHERE id_DOSBox_Configs = " & TC.getSQLFormat(row("id_DOSBox_Configs"))

			DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
			tran.Commit()
			id_DOSBox_Configs = row("id_DOSBox_Configs")
		Else
			'Insert
			Dim sSQL As String = ""
			sSQL &= "	INSERT INTO tbl_DOSBox_Configs" & ControlChars.CrLf

			Dim sSQL_Columns As String = "	(" & ControlChars.CrLf
			Dim sSQL_Values As String = "	(" & ControlChars.CrLf

			sSQL_Columns &= "	isTemplate" & ControlChars.CrLf
			sSQL_Columns &= "	, id_Rombase_DOSBox_Configs" & ControlChars.CrLf


			sSQL_Values &= "	" & TC.getSQLFormat(1) & ControlChars.CrLf
			sSQL_Values &= "	, " & IIf(Create_Duplicate, "NULL", TC.getSQLFormat(row("id_Rombase_DOSBox_Configs"))) & ControlChars.CrLf

			For Each col As DataColumn In row.Table.Columns
				If Not {"id_DOSBox_Configs", "id_Rombase_DOSBox_Configs"}.Contains(col.ColumnName) Then
					sSQL_Columns &= "	, [" & col.ColumnName & "] " & ControlChars.CrLf
					If col.ColumnName = "Displayname" AndAlso TC.NZ(New_Template_Name, "") <> "" Then
						sSQL_Values &= "	, " & TC.getSQLFormat(New_Template_Name)
					Else
						sSQL_Values &= "	, " & IIf(row_RB IsNot Nothing AndAlso MKNetLib.cls_MKSQLDataAccess.HasColumn(row_RB, col.ColumnName) AndAlso Equals(row(col.ColumnName), row_RB(col.ColumnName)), TC.getSQLFormat(DBNull.Value), TC.getSQLFormat(row(col.ColumnName))) & ControlChars.CrLf
					End If
				End If
			Next

			sSQL_Columns &= "	)" & ControlChars.CrLf & " VALUES " & ControlChars.CrLf
			sSQL_Values &= "	)" & ControlChars.CrLf

			sSQL &= sSQL_Columns & sSQL_Values & "; SELECT last_insert_rowid()"

			id_DOSBox_Configs = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, sSQL, tran), 0)
			tran.Commit()
		End If

		Return id_DOSBox_Configs
	End Function

	Public Shared Function Upsert_tbl_ScummVM_Configs_Templates(ByRef tran As SQLite.SQLiteTransaction, ByRef row As DS_ML.tbl_ScummVM_ConfigsRow, Optional ByVal Create_Duplicate As Boolean = False, Optional ByVal New_Template_Name As String = "") As Int64
		Dim tbl_RB As New DS_ML.tbl_ScummVM_ConfigsDataTable

		If Not Create_Duplicate Then
			If TC.NZ(row("id_Rombase_ScummVM_Configs"), 0) > 0 Then
				DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT * FROM rombase.tbl_Rombase_ScummVM_Configs WHERE id_Rombase_ScummVM_Configs = " & TC.getSQLFormat(row("id_Rombase_ScummVM_Configs")), tbl_RB, tran)
			End If
		End If

		Dim row_RB As DataRow = Nothing
		If tbl_RB.Rows.Count = 1 Then
			row_RB = tbl_RB.Rows(0)
		End If

		Dim id_ScummVM_Configs As Int64 = 0

		'Insert a new default ScummVM Config or Update an existing one
		'Only add values if they differ from row_RB
		If Not Create_Duplicate AndAlso TC.NZ(row("id_ScummVM_Configs"), 0) > 0 Then
			'Update
			Dim sSQL As String = ""
			sSQL &= "	UPDATE tbl_ScummVM_Configs SET" & ControlChars.CrLf

			sSQL &= "	isTemplate = " & TC.getSQLFormat(1) & ControlChars.CrLf
			sSQL &= "	, id_Rombase_ScummVM_Configs = " & TC.getSQLFormat(row("id_Rombase_ScummVM_Configs")) & ControlChars.CrLf

			For Each col As DataColumn In row.Table.Columns
				If Not {"id_ScummVM_Configs", "id_Rombase_ScummVM_Configs"}.Contains(col.ColumnName) Then
					sSQL &= "	, [" & col.ColumnName & "] = " & IIf(row_RB IsNot Nothing AndAlso MKNetLib.cls_MKSQLDataAccess.HasColumn(row_RB, col.ColumnName) AndAlso Equals(row(col.ColumnName), row_RB(col.ColumnName)), TC.getSQLFormat(DBNull.Value), TC.getSQLFormat(row(col.ColumnName))) & ControlChars.CrLf
				End If
			Next

			sSQL &= "	WHERE id_ScummVM_Configs = " & TC.getSQLFormat(row("id_ScummVM_Configs"))

			DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
			tran.Commit()
			id_ScummVM_Configs = row("id_ScummVM_Configs")
		Else
			'Insert
			Dim sSQL As String = ""
			sSQL &= "	INSERT INTO tbl_ScummVM_Configs" & ControlChars.CrLf

			Dim sSQL_Columns As String = "	(" & ControlChars.CrLf
			Dim sSQL_Values As String = "	(" & ControlChars.CrLf

			sSQL_Columns &= "	isTemplate" & ControlChars.CrLf
			sSQL_Columns &= "	, id_Rombase_ScummVM_Configs" & ControlChars.CrLf


			sSQL_Values &= "	" & TC.getSQLFormat(1) & ControlChars.CrLf
			sSQL_Values &= "	, " & IIf(Create_Duplicate, "NULL", TC.getSQLFormat(row("id_Rombase_ScummVM_Configs"))) & ControlChars.CrLf

			For Each col As DataColumn In row.Table.Columns
				If Not {"id_ScummVM_Configs", "id_Rombase_ScummVM_Configs"}.Contains(col.ColumnName) Then
					sSQL_Columns &= "	, [" & col.ColumnName & "] " & ControlChars.CrLf
					If col.ColumnName = "Displayname" AndAlso TC.NZ(New_Template_Name, "") <> "" Then
						sSQL_Values &= "	, " & TC.getSQLFormat(New_Template_Name)
					Else
						sSQL_Values &= "	, " & IIf(row_RB IsNot Nothing AndAlso MKNetLib.cls_MKSQLDataAccess.HasColumn(row_RB, col.ColumnName) AndAlso Equals(row(col.ColumnName), row_RB(col.ColumnName)), TC.getSQLFormat(DBNull.Value), TC.getSQLFormat(row(col.ColumnName))) & ControlChars.CrLf
					End If
				End If
			Next

			sSQL_Columns &= "	)" & ControlChars.CrLf & " VALUES " & ControlChars.CrLf
			sSQL_Values &= "	)" & ControlChars.CrLf

			sSQL &= sSQL_Columns & sSQL_Values & "; SELECT last_insert_rowid()"

			id_ScummVM_Configs = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, sSQL, tran), 0)
			tran.Commit()
		End If

		Return id_ScummVM_Configs
	End Function

	Public Shared Function Upsert_tbl_DOSBox_Config(ByRef tran As SQLite.SQLiteTransaction, ByRef row As DS_ML.tbl_DOSBox_ConfigsRow, ByVal id_Emu_Games As Long) As Int64
		Dim id_DOSBox_Config_Template As Long = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_DOSBox_Configs_Template FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), tran), 0)
		Dim id_DOSBox_Config As Long = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_DOSBox_Configs FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), tran), 0)

		Dim row_Template As DataRow = Nothing

		Dim tbl_Template As DS_ML.tbl_DOSBox_ConfigsDataTable = Nothing
		If id_DOSBox_Config_Template <> 0 Then
			DS_ML.Fill_tbl_DOSBox_Template_Configs(tran, tbl_Template, id_DOSBox_Config_Template)
		End If

		If tbl_Template.Rows.Count = 1 Then
			row_Template = tbl_Template.Rows(0)
		End If

		'Insert a new DOSBox Config for the game or Update an existing one
		'Only add values if they differ from row_Template
		If id_DOSBox_Config <> 0 Then
			'Update
			Dim sSQL As String = ""
			sSQL &= "	UPDATE tbl_DOSBox_Configs SET" & ControlChars.CrLf

			sSQL &= "	isTemplate = " & TC.getSQLFormat(DBNull.Value) & ControlChars.CrLf
			sSQL &= "	, id_Rombase_DOSBox_Configs = " & TC.getSQLFormat(DBNull.Value) & ControlChars.CrLf

			For Each col As DataColumn In row.Table.Columns
				If Not {"id_DOSBox_Configs", "id_Rombase_DOSBox_Configs"}.Contains(col.ColumnName) Then
					sSQL &= "	, [" & col.ColumnName & "] = " & IIf(row_Template IsNot Nothing AndAlso MKNetLib.cls_MKSQLDataAccess.HasColumn(row_Template, col.ColumnName) AndAlso Equals(row(col.ColumnName), row_Template(col.ColumnName)), TC.getSQLFormat(DBNull.Value), TC.getSQLFormat(row(col.ColumnName))) & ControlChars.CrLf
				End If
			Next

			sSQL &= "	WHERE id_DOSBox_Configs = " & TC.getSQLFormat(row("id_DOSBox_Configs"))

			DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
		Else
			'Insert
			Dim sSQL As String = ""
			sSQL &= "	INSERT INTO tbl_DOSBox_Configs" & ControlChars.CrLf

			Dim sSQL_Columns As String = "	(" & ControlChars.CrLf
			Dim sSQL_Values As String = "	(" & ControlChars.CrLf

			sSQL_Columns &= "	isTemplate" & ControlChars.CrLf
			sSQL_Columns &= "	, id_Rombase_DOSBox_Configs" & ControlChars.CrLf


			sSQL_Values &= TC.getSQLFormat(DBNull.Value)
			sSQL_Values &= ", " & TC.getSQLFormat(DBNull.Value)

			For Each col As DataColumn In row.Table.Columns
				If Not {"id_DOSBox_Configs", "id_Rombase_DOSBox_Configs"}.Contains(col.ColumnName) Then
					sSQL_Columns &= "	, [" & col.ColumnName & "] " & ControlChars.CrLf
					sSQL_Values &= "	, " & IIf(row_Template IsNot Nothing AndAlso MKNetLib.cls_MKSQLDataAccess.HasColumn(row_Template, col.ColumnName) AndAlso Equals(row(col.ColumnName), row_Template(col.ColumnName)), TC.getSQLFormat(DBNull.Value), TC.getSQLFormat(row(col.ColumnName))) & ControlChars.CrLf
				End If
			Next

			sSQL_Columns &= "	)" & ControlChars.CrLf & " VALUES " & ControlChars.CrLf
			sSQL_Values &= "	)" & ControlChars.CrLf

			sSQL &= sSQL_Columns & sSQL_Values & "; SELECT last_insert_rowid()"

			id_DOSBox_Config = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, sSQL, tran), 0)

			DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Emu_Games SET id_DOSBox_Configs = " & TC.getSQLFormat(id_DOSBox_Config) & " WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), tran)
		End If

		Return id_DOSBox_Config
	End Function

	Public Shared Function Upsert_tbl_ScummVM_Config(ByRef tran As SQLite.SQLiteTransaction, ByRef row As DS_ML.tbl_ScummVM_ConfigsRow, ByVal id_Emu_Games As Long) As Int64
		Dim id_ScummVM_Config_Template As Long = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_ScummVM_Configs_Template FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), tran), 0)
		Dim id_ScummVM_Config As Long = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_ScummVM_Configs FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), tran), 0)

		Dim row_Template As DataRow = Nothing

		Dim tbl_Template As DS_ML.tbl_ScummVM_ConfigsDataTable = Nothing
		If id_ScummVM_Config_Template <> 0 Then
			DS_ML.Fill_tbl_ScummVM_Template_Configs(tran, tbl_Template, id_ScummVM_Config_Template)
		End If

		If tbl_Template.Rows.Count = 1 Then
			row_Template = tbl_Template.Rows(0)
		End If

		'Insert a new ScummVM Config for the game or Update an existing one
		'Only add values if they differ from row_Template
		If id_ScummVM_Config <> 0 Then
			'Update
			Dim sSQL As String = ""
			sSQL &= "	UPDATE tbl_ScummVM_Configs SET" & ControlChars.CrLf

			sSQL &= "	isTemplate = " & TC.getSQLFormat(DBNull.Value) & ControlChars.CrLf
			sSQL &= "	, id_Rombase_ScummVM_Configs = " & TC.getSQLFormat(DBNull.Value) & ControlChars.CrLf

			For Each col As DataColumn In row.Table.Columns
				If Not {"id_ScummVM_Configs", "id_Rombase_ScummVM_Configs"}.Contains(col.ColumnName) Then
					sSQL &= "	, [" & col.ColumnName & "] = " & IIf(row_Template IsNot Nothing AndAlso MKNetLib.cls_MKSQLDataAccess.HasColumn(row_Template, col.ColumnName) AndAlso Equals(row(col.ColumnName), row_Template(col.ColumnName)), TC.getSQLFormat(DBNull.Value), TC.getSQLFormat(row(col.ColumnName))) & ControlChars.CrLf
				End If
			Next

			sSQL &= "	WHERE id_ScummVM_Configs = " & TC.getSQLFormat(row("id_ScummVM_Configs"))

			DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
		Else
			'Insert
			Dim sSQL As String = ""
			sSQL &= "	INSERT INTO tbl_ScummVM_Configs" & ControlChars.CrLf

			Dim sSQL_Columns As String = "	(" & ControlChars.CrLf
			Dim sSQL_Values As String = "	(" & ControlChars.CrLf

			sSQL_Columns &= "	isTemplate" & ControlChars.CrLf
			sSQL_Columns &= "	, id_Rombase_ScummVM_Configs" & ControlChars.CrLf


			sSQL_Values &= TC.getSQLFormat(DBNull.Value)
			sSQL_Values &= ", " & TC.getSQLFormat(DBNull.Value)

			For Each col As DataColumn In row.Table.Columns
				If Not {"id_ScummVM_Configs", "id_Rombase_ScummVM_Configs"}.Contains(col.ColumnName) Then
					sSQL_Columns &= "	, [" & col.ColumnName & "] " & ControlChars.CrLf
					sSQL_Values &= "	, " & IIf(row_Template IsNot Nothing AndAlso MKNetLib.cls_MKSQLDataAccess.HasColumn(row_Template, col.ColumnName) AndAlso Equals(row(col.ColumnName), row_Template(col.ColumnName)), TC.getSQLFormat(DBNull.Value), TC.getSQLFormat(row(col.ColumnName))) & ControlChars.CrLf
				End If
			Next

			sSQL_Columns &= "	)" & ControlChars.CrLf & " VALUES " & ControlChars.CrLf
			sSQL_Values &= "	)" & ControlChars.CrLf

			sSQL &= sSQL_Columns & sSQL_Values & "; SELECT last_insert_rowid()"

			id_ScummVM_Config = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, sSQL, tran), 0)

			DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Emu_Games SET id_ScummVM_Configs = " & TC.getSQLFormat(id_ScummVM_Config) & " WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), tran)
		End If

		Return id_ScummVM_Config
	End Function

	Public Shared Sub Upsert_tbl_Users(ByRef tran As SQLite.SQLiteTransaction, ByRef tbl_Users As DS_ML.tbl_UsersDataTable)
		For Each row As DS_ML.tbl_UsersRow In tbl_Users
			If row.RowState = DataRowState.Deleted Then
				If row("id_Users") > 0 Then
					DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Users WHERE id_Users = " & TC.getSQLFormat(row("id_Users")), tran)
				End If
			End If

			If row.RowState = DataRowState.Added Then
				DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Users (Username, Password, Admin, Restricted, id_Cheevo_Challenges) VALUES (" & TC.getSQLParameter(row("Username"), row("Password"), row("Admin"), row("Restricted"), TC.NZ("id_Cheevo_Challenges", 0)) & ")", tran)
			End If

			If row.RowState = DataRowState.Modified Then
				DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Users SET Username = " & TC.getSQLFormat(row("Username")) & ", Password = " & TC.getSQLFormat(row("Password")) & ", Admin = " & TC.getSQLFormat(row("Admin")) & ", Restricted = " & TC.getSQLFormat(row("Restricted")) & ", id_Cheevo_Challenges = " & TC.getSQLFormat(TC.NZ(row("id_Cheevo_Challenges"), 0)) & " WHERE id_Users = " & TC.getSQLFormat(row("id_Users")), tran)
			End If
		Next
	End Sub

	Public Shared Function Upsert_tbl_Users_Emu_Games(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Users As Integer, ByVal id_Emu_Games As Integer, ByVal Num_Played As Object, ByVal Num_Runtime As Object, Optional ByVal Last_Played As Object = Nothing, Optional ByVal Favourite As Object = Nothing, Optional ByVal Have As Object = Nothing, Optional ByVal Want As Object = Nothing, Optional ByVal Trade As Object = Nothing) As Boolean
		Dim id_Users_Emu_Games As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Users_Emu_Games FROM tbl_Users_Emu_Games WHERE id_Users = " & TC.getSQLFormat(id_Users) & " AND id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), tran), 0)

		If id_Users_Emu_Games <> 0 Then
			'Update
			Dim sSQL As String = ""
			sSQL &= "UPDATE tbl_Users_Emu_Games" & ControlChars.CrLf
			sSQL &= "SET" & ControlChars.CrLf

			Dim bHasChange As Boolean = False

			If Not IsNothing(Favourite) Then
				sSQL &= "	Favourite = " & TC.getSQLFormat(Favourite) & ControlChars.CrLf
				bHasChange = True
			End If

			If Not IsNothing(Have) Then
				If bHasChange Then sSQL &= ", "
				sSQL &= "	Have = " & TC.getSQLFormat(Have) & ControlChars.CrLf
				bHasChange = True
			End If

			If Not IsNothing(Want) Then
				If bHasChange Then sSQL &= ", "
				sSQL &= "	Want = " & TC.getSQLFormat(Want) & ControlChars.CrLf
				bHasChange = True
			End If

			If Not IsNothing(Trade) Then
				If bHasChange Then sSQL &= ", "
				sSQL &= "	Trade = " & TC.getSQLFormat(Trade) & ControlChars.CrLf
				bHasChange = True
			End If

			If Not IsNothing(Num_Played) Then
				If bHasChange Then sSQL &= ", "
				sSQL &= "	Num_Played = " & TC.getSQLFormat(Num_Played) & ControlChars.CrLf
				bHasChange = True
			End If

			If Not IsNothing(Num_Runtime) Then
				If bHasChange Then sSQL &= ", "
				sSQL &= "	Num_Runtime = " & TC.getSQLFormat(Num_Runtime) & ControlChars.CrLf
				bHasChange = True
			End If

			If Not IsNothing(Last_Played) Then
				If bHasChange Then sSQL &= ", "
				sSQL &= "	Last_Played = " & TC.getSQLFormat(Last_Played) & ControlChars.CrLf
				bHasChange = True
			End If

			sSQL &= "WHERE id_Users_Emu_Games = " & TC.getSQLFormat(id_Users_Emu_Games)

			If bHasChange Then
				Return DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
			Else
				Return True
			End If

		Else
			'Insert
			If cls_Globals.Admin Then
				Dim sSQL_tbl_Emu_Games As String = "SELECT Num_Played, Num_Runtime, Last_Played, Favourite, Want, Have, Trade FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games)
				Dim dt_Emu_Games As DataTable = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, sSQL_tbl_Emu_Games, Nothing, tran)
				If dt_Emu_Games.Rows.Count = 1 Then
					If Num_Played Is Nothing Then Num_Played = dt_Emu_Games.Rows(0)("Num_Played")
					If Num_Runtime Is Nothing Then Num_Runtime = dt_Emu_Games.Rows(0)("Num_Runtime")
					If Last_Played Is Nothing Then Last_Played = dt_Emu_Games.Rows(0)("Last_Played")
					If Favourite Is Nothing Then Favourite = dt_Emu_Games.Rows(0)("Favourite")
					If Have Is Nothing Then Have = dt_Emu_Games.Rows(0)("Have")
					If Want Is Nothing Then Want = dt_Emu_Games.Rows(0)("Want")
					If Trade Is Nothing Then Trade = dt_Emu_Games.Rows(0)("Trade")
				End If
			End If

			Dim sSQL As String = ""
			sSQL &= "INSERT INTO tbl_Users_Emu_Games (id_Users, id_Emu_Games, Num_Played, Num_Runtime, Last_Played, Favourite, Have, Want, Trade) VALUES (" & TC.getSQLParameter(id_Users, id_Emu_Games, Num_Played, Num_Runtime, Last_Played, Favourite, Have, Want, Trade) & ")"
			Return DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
		End If
	End Function

	Public Shared Function Upsert_tbl_Similarity_Calculation_Results(ByRef tran As SQLite.SQLiteTransaction, ByRef dt As DS_ML.tbl_Similarity_CalculationDataTable, ByVal Name As String, ByVal id_Similarity_Calculation_Config As Integer, ByVal id_Emu_Games As Integer, ByVal id_Similarity_Calculation_Results As Integer) As Integer
		If id_Similarity_Calculation_Results = 0 Then

			id_Similarity_Calculation_Results = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "INSERT INTO tbl_Similarity_Calculation_Results (Name, id_Similarity_Calculation_Config, id_Emu_Games) VALUES (" & TC.getSQLParameter(Name, id_Similarity_Calculation_Config, id_Emu_Games) & "); SELECT last_insert_rowid()", tran), 0)

			If id_Similarity_Calculation_Results <= 0 Then
				Return False
			End If
		Else
			DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Similarity_Calculation_Results_Entries WHERE id_Similarity_Calculation_Results = " & id_Similarity_Calculation_Results, tran)
		End If

		Dim sSQL1 As String = ""
		sSQL1 &= "INSERT INTO tbl_Similarity_Calculation_Results_Entries" & ControlChars.CrLf
		sSQL1 &= "(	"
		sSQL1 &= "id_Similarity_Calculation_Results" & ControlChars.CrLf
		sSQL1 &= ", id_Emu_Games" & ControlChars.CrLf
		sSQL1 &= ", id_Moby_Releases" & ControlChars.CrLf
		sSQL1 &= ", Similarity" & ControlChars.CrLf
		sSQL1 &= ", [001_Platform]" & ControlChars.CrLf
		sSQL1 &= ", [002_MobyRank]" & ControlChars.CrLf
		sSQL1 &= ", [003_MobyScore]" & ControlChars.CrLf
		sSQL1 &= ", [004_Publisher]" & ControlChars.CrLf
		sSQL1 &= ", [005_Developer]" & ControlChars.CrLf
		sSQL1 &= ", [006_Year]" & ControlChars.CrLf
		sSQL1 &= ", [101_Basic_Genres]" & ControlChars.CrLf
		sSQL1 &= ", [102_Perspectives]" & ControlChars.CrLf
		sSQL1 &= ", [103_Sports_Themes]" & ControlChars.CrLf
		sSQL1 &= ", [105_Educational_Categories]" & ControlChars.CrLf
		sSQL1 &= ", [106_Other_Attributes]" & ControlChars.CrLf
		sSQL1 &= ", [107_Visual_Presentation]" & ControlChars.CrLf
		sSQL1 &= ", [108_Gameplay]" & ControlChars.CrLf
		sSQL1 &= ", [109_Pacing]" & ControlChars.CrLf
		sSQL1 &= ", [110_Narrative_Theme_Topic]" & ControlChars.CrLf
		sSQL1 &= ", [111_Setting]" & ControlChars.CrLf
		sSQL1 &= ", [112_Vehicular_Themes]" & ControlChars.CrLf
		sSQL1 &= ", [113_Interface_Control]" & ControlChars.CrLf
		sSQL1 &= ", [114_DLC_Addon]" & ControlChars.CrLf
		sSQL1 &= ", [115_Special_Edition]" & ControlChars.CrLf
		sSQL1 &= ", [201_MinPlayers]" & ControlChars.CrLf
		sSQL1 &= ", [202_MaxPlayers]" & ControlChars.CrLf
		sSQL1 &= ", [203_AgeO]" & ControlChars.CrLf
		sSQL1 &= ", [204_AgeP]" & ControlChars.CrLf
		sSQL1 &= ", [205_Rating_Descriptors]" & ControlChars.CrLf
		sSQL1 &= ", [206_Other_Attributes]" & ControlChars.CrLf
		sSQL1 &= ", [207_Multiplayer_Attributes]" & ControlChars.CrLf
		sSQL1 &= ", [301_Group_Membership]" & ControlChars.CrLf
		sSQL1 &= ", [401_Staff]" & ControlChars.CrLf
		sSQL1 &= ") VALUES ("

		For Each row As DataRow In dt.Rows
			Dim sSQL2 As String = ""
			sSQL2 &= TC.getSQLFormat(id_Similarity_Calculation_Results)
			sSQL2 &= ", " & TC.getSQLFormat(row("id_Emu_Games"))
			sSQL2 &= ", " & TC.getSQLFormat(row("id_Moby_Releases"))
			sSQL2 &= ", " & TC.getSQLFormat(row("Similarity"))
			sSQL2 &= ", " & TC.getSQLFormat(row("001_Platform"))
			sSQL2 &= ", " & TC.getSQLFormat(row("002_MobyRank"))
			sSQL2 &= ", " & TC.getSQLFormat(row("003_MobyScore"))
			sSQL2 &= ", " & TC.getSQLFormat(row("004_Publisher"))
			sSQL2 &= ", " & TC.getSQLFormat(row("005_Developer"))
			sSQL2 &= ", " & TC.getSQLFormat(row("006_Year"))
			sSQL2 &= ", " & TC.getSQLFormat(row("101_Basic_Genres"))
			sSQL2 &= ", " & TC.getSQLFormat(row("102_Perspectives"))
			sSQL2 &= ", " & TC.getSQLFormat(row("103_Sports_Themes"))
			sSQL2 &= ", " & TC.getSQLFormat(row("105_Educational_Categories"))
			sSQL2 &= ", " & TC.getSQLFormat(row("106_Other_Attributes"))
			sSQL2 &= ", " & TC.getSQLFormat(row("107_Visual_Presentation"))
			sSQL2 &= ", " & TC.getSQLFormat(row("108_Gameplay"))
			sSQL2 &= ", " & TC.getSQLFormat(row("109_Pacing"))
			sSQL2 &= ", " & TC.getSQLFormat(row("110_Narrative_Theme_Topic"))
			sSQL2 &= ", " & TC.getSQLFormat(row("111_Setting"))
			sSQL2 &= ", " & TC.getSQLFormat(row("112_Vehicular_Themes"))
			sSQL2 &= ", " & TC.getSQLFormat(row("113_Interface_Control"))
			sSQL2 &= ", " & TC.getSQLFormat(row("114_DLC_Addon"))
			sSQL2 &= ", " & TC.getSQLFormat(row("115_Special_Edition"))
			sSQL2 &= ", " & TC.getSQLFormat(row("201_MinPlayers"))
			sSQL2 &= ", " & TC.getSQLFormat(row("202_MaxPlayers"))
			sSQL2 &= ", " & TC.getSQLFormat(row("203_AgeO"))
			sSQL2 &= ", " & TC.getSQLFormat(row("204_AgeP"))
			sSQL2 &= ", " & TC.getSQLFormat(row("205_Rating_Descriptors"))
			sSQL2 &= ", " & TC.getSQLFormat(row("206_Other_Attributes"))
			sSQL2 &= ", " & TC.getSQLFormat(row("207_Multiplayer_Attributes"))
			sSQL2 &= ", " & TC.getSQLFormat(row("301_Group_Membership"))
			sSQL2 &= ", " & TC.getSQLFormat(row("401_Staff"))
			sSQL2 &= ")"

			DataAccess.FireProcedureReturnScalar(tran.Connection, 0, sSQL1 & sSQL2, tran)
		Next

		Return id_Similarity_Calculation_Results
	End Function

	Public Shared Function Upsert_tbl_List_Generators(ByRef tran As SQLite.SQLiteTransaction, ByVal Name As String, ByVal Sort As Int64, ByVal Main_Template As String, ByVal File_Entry_Template As String, Optional ByVal id_List_Generators As Int64 = 0L) As Int64
		If id_List_Generators = 0L Then
			Return DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "INSERT INTO tbl_List_Generators (Name, Sort, Main_Template, File_Entry_Template) VALUES (" & TC.getSQLParameter(Name, Sort, Main_Template, File_Entry_Template) & "); SELECT last_insert_rowid()", tran)
		Else
			DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_List_Generators SET Name = " & TC.getSQLFormat(Name) & ", Sort = " & TC.getSQLFormat(Sort) & ", Main_Template = " & TC.getSQLFormat(Main_Template) & ", File_Entry_Template = " & TC.getSQLFormat(File_Entry_Template) & " WHERE id_List_Generators = " & TC.getSQLFormat(id_List_Generators), tran)
			Return id_List_Generators
		End If
	End Function

	Public Shared Function Upsert_tbl_Total_DOS_Launcher_Configs(ByRef tran As SQLite.SQLiteTransaction, ByVal DisplayName As String, ByVal proglocations As String, ByVal cachelocation As String, ByVal userlevel As String, ByVal forcelogging As Boolean, ByVal swapping As Boolean, ByVal preloading As Boolean, ByVal pauseafterrun As Boolean, ByVal VESA As Object, Optional ByVal id_Total_DOS_Launcher_Configs As Int64 = 0L) As Int64
		If id_Total_DOS_Launcher_Configs = 0L Then
			Return DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "INSERT INTO tbl_Total_DOS_Launcher_Configs (DisplayName, proglocations, cachelocation, userlevel, forcelogging, swapping, preloading, pauseafterrun, VESA) VALUES (" & TC.getSQLParameter(DisplayName, proglocations, cachelocation, userlevel, forcelogging, swapping, preloading, pauseafterrun, VESA) & "); SELECT last_insert_rowid()", tran)
		Else
			DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Total_DOS_Launcher_Configs SET DisplayName = " & TC.getSQLFormat(DisplayName) & ", proglocations = " & TC.getSQLFormat(proglocations) & ", cachelocation = " & TC.getSQLFormat(cachelocation) & ", userlevel = " & TC.getSQLFormat(userlevel) & ", forcelogging = " & TC.getSQLFormat(forcelogging) & ", swapping = " & TC.getSQLFormat(swapping) & ", preloading = " & TC.getSQLFormat(preloading) & ", pauseafterrun = " & TC.getSQLFormat(pauseafterrun) & ", VESA = " & TC.getSQLFormat(VESA) & " WHERE id_Total_DOS_Launcher_Configs = " & TC.getSQLFormat(id_Total_DOS_Launcher_Configs), tran)
			Return id_Total_DOS_Launcher_Configs
		End If
	End Function

	Public Shared Sub Upsert_tbl_Users_Emulators_Moby_Platforms_Enforce_Not_Default(ByRef tran As SQLite.SQLiteTransaction, Optional ByVal id_Users As Int64 = 0, Optional ByVal id_Moby_Platforms As Int64 = 0, Optional ByVal id_Emulators As Int64 = 0)
		Dim dt_Users As DataTable = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT id_Users, Username FROM tbl_Users WHERE IFNULL(Admin, 0) = 0" & IIf(id_Users > 0, " AND id_Users = " & TC.getSQLFormat(id_Users), ""))
		Dim dt_Emulators As DataTable = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT id_Emulators, Displayname FROM tbl_Emulators" & IIf(id_Emulators > 0, " WHERE id_Emulators = " & TC.getSQLFormat(id_Emulators), ""))

		For Each row_User As DataRow In dt_Users.Rows
			Dim current_id_Users As Int64 = row_User("id_Users")

			Dim sSQLCheckDefaults As String = ""
			sSQLCheckDefaults &= "SELECT COUNT(1)" & ControlChars.CrLf
			sSQLCheckDefaults &= "FROM tbl_Users_Emulators_Moby_Platforms" & ControlChars.CrLf
			sSQLCheckDefaults &= "WHERE id_Users = " & TC.getSQLFormat(current_id_Users) & ControlChars.CrLf
			sSQLCheckDefaults &= "AND id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms) & ControlChars.CrLf
			sSQLCheckDefaults &= "AND DefaultEmulator = 1" & ControlChars.CrLf

			If TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, sSQLCheckDefaults, tran), 0) > 0 Then
				For Each row_Emulators As DataRow In dt_Emulators.Rows
					Dim sSQL As String = ""
					sSQL &= "SELECT id_Users_Emulators_Moby_Platforms" & ControlChars.CrLf
					sSQL &= "FROM tbl_Users_Emulators_Moby_Platforms" & ControlChars.CrLf
					sSQL &= "WHERE id_Users = " & TC.getSQLFormat(current_id_Users) & ControlChars.CrLf
					sSQL &= "AND id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms) & ControlChars.CrLf
					sSQL &= "AND id_Emulators = " & TC.getSQLFormat(row_Emulators("id_Emulators")) & ControlChars.CrLf

					Dim id_Users_Emulators_Moby_Platforms As Int64 = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, sSQL, tran), 0L)

					If id_Users_Emulators_Moby_Platforms = 0L Then
						'INSERT (we dont need to update)
						DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Users_Emulators_Moby_Platforms (id_Users, id_Emulators, id_Moby_Platforms, DefaultEmulator) VALUES (" & TC.getSQLParameter(current_id_Users, row_Emulators("id_Emulators"), id_Moby_Platforms, 0) & ")", tran)
					End If
				Next
			End If

		Next
	End Sub

	Public Shared Sub Upsert_tbl_Users_Emulators_Moby_Platforms_Enforce_Not_Default_All_Emulators_For_Platform(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Users As Int64, ByVal id_Moby_Platforms As Int64, ByVal excluded_id_Emulators As Int64)
		Dim sSQL As String = ""
		sSQL &= "INSERT INTO tbl_Users_Emulators_Moby_Platforms (" & ControlChars.CrLf
		sSQL &= "	id_Users" & ControlChars.CrLf
		sSQL &= "	, id_Emulators" & ControlChars.CrLf
		sSQL &= "	, id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "	, DefaultEmulator" & ControlChars.CrLf
		sSQL &= ")" & ControlChars.CrLf
		sSQL &= "SELECT" & ControlChars.CrLf
		sSQL &= "	" & TC.getSQLFormat(id_Users) & " AS id_Users" & ControlChars.CrLf
		sSQL &= "	, EMP.id_Emulators AS id_Emulators" & ControlChars.CrLf
		sSQL &= "	, EMP.id_Moby_Platforms AS id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "	, 0 AS DefaultEmulator" & ControlChars.CrLf
		sSQL &= "FROM tbl_Emulators_Moby_Platforms EMP" & ControlChars.CrLf
		sSQL &= "WHERE EMP.id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms) & ControlChars.CrLf
		sSQL &= "AND (EMP.id_Emulators <> " & TC.getSQLFormat(excluded_id_Emulators) & ")" & ControlChars.CrLf
		sSQL &= "AND EMP.id_Emulators || '-' || EMP.id_Moby_Platforms NOT IN (" & ControlChars.CrLf
		sSQL &= "	SELECT UEMP.id_Emulators || '-' || UEMP.id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "	FROM tbl_Users_Emulators_Moby_Platforms UEMP" & ControlChars.CrLf
		sSQL &= "	WHERE UEMP.id_Users = " & TC.getSQLFormat(id_Users) & ControlChars.CrLf
		sSQL &= "	AND UEMP.id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms) & ControlChars.CrLf
		sSQL &= ")" & ControlChars.CrLf

		DataAccess.FireProcedure(cls_Globals.Conn, 0, sSQL, tran)
	End Sub


#End Region

#Region "Delete Statements"
	Public Shared Sub Delete_tbl_Mame_Roms(ByRef tran As SQLite.SQLiteTransaction, Optional ByVal id_Mame_Roms As Integer = 0)
		DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Mame_Roms" & IIf(id_Mame_Roms <> 0, " WHERE id_Mame_Roms = " & TC.getSQLFormat(id_Mame_Roms), ""), tran)
	End Sub

#End Region

#Region "Merge Scripts"
	Public Sub Merge_tbl_Emu_Games_Alternate_Titles(ByRef tran As SQLite.SQLiteTransaction, ByVal old_id_Emu_Games As Integer, ByVal new_id_Emu_Games As Integer)
		Dim sSQL As String = ""
		sSQL &= "	UPDATE tbl_Emu_Games_Alternate_Titles"
		sSQL &= "	Set id_Emu_Games = " & TC.getSQLFormat(new_id_Emu_Games)
		sSQL &= "	WHERE	id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games)
		sSQL &= "				And	Not EXISTS"
		sSQL &= "				("
		sSQL &= "					Select * FROM"
		sSQL &= "					tbl_Emu_Games_Alternate_Titles AT2 "
		sSQL &= "					WHERE AT2.id_Emu_Games = " & TC.getSQLFormat(new_id_Emu_Games)
		sSQL &= "								And AT2.Alternate_Title = tbl_Emu_Games_Alternate_Titles.Alternate_Title"
		sSQL &= "								And AT2.Description = tbl_Emu_Games_Alternate_Titles.Description"
		sSQL &= "				) Then"
		sSQL &= "	;"
		sSQL &= "	DELETE FROM tbl_Emu_Games_Alternate_Titles WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games)
		DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
	End Sub

	Public Sub Merge_tbl_Emu_Games_Languages(ByRef tran As SQLite.SQLiteTransaction, ByVal old_id_Emu_Games As Integer, ByVal new_id_Emu_Games As Integer)
		Dim sSQL As String = ""
		sSQL &= "	UPDATE tbl_Emu_Games_Languages"
		sSQL &= "	Set id_Emu_Games = " & TC.getSQLFormat(new_id_Emu_Games)
		sSQL &= "	WHERE	id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games)
		sSQL &= "				And	Not EXISTS"
		sSQL &= "				("
		sSQL &= "					Select * FROM"
		sSQL &= "					tbl_Emu_Games_Languages EGL2 "
		sSQL &= "					WHERE EGL2.id_Emu_Games = " & TC.getSQLFormat(new_id_Emu_Games)
		sSQL &= "								And EGL2.id_Languages = tbl_Emu_Games_Languages.id_Languages"
		sSQL &= "				)"
		sSQL &= "	;"
		sSQL &= "	DELETE FROM tbl_Emu_Games_Languages WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games)
		DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
	End Sub

	Public Sub Merge_tbl_Emu_Games_Moby_Attributes(ByRef tran As SQLite.SQLiteTransaction, ByVal old_id_Emu_Games As Integer, ByVal new_id_Emu_Games As Integer)
		Dim sSQL As String = ""
		sSQL &= "	UPDATE tbl_Emu_Games_Moby_Attributes"
		sSQL &= "	Set id_Emu_Games = " & TC.getSQLFormat(new_id_Emu_Games)
		sSQL &= "	WHERE	id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games)
		sSQL &= "				And	Not EXISTS"
		sSQL &= "				("
		sSQL &= "					Select * FROM"
		sSQL &= "					tbl_Emu_Games_Moby_Attributes EGMA2"
		sSQL &= "					WHERE EGMA2.id_Emu_Games = " & TC.getSQLFormat(new_id_Emu_Games)
		sSQL &= "								And EGMA2.id_Moby_Attributes = tbl_Emu_Games_Moby_Attributes.id_Moby_Attributes"
		sSQL &= "				)"
		sSQL &= "	;"
		sSQL &= "	DELETE FROM tbl_Emu_Games_Moby_Attributes WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games)
		DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
	End Sub

	Public Sub Merge_tbl_Emu_Games_Moby_Genres(ByRef tran As SQLite.SQLiteTransaction, ByVal old_id_Emu_Games As Integer, ByVal new_id_Emu_Games As Integer)
		Dim sSQL As String = ""
		sSQL &= "	UPDATE tbl_Emu_Games_Moby_Genres"
		sSQL &= "	Set id_Emu_Games = " & TC.getSQLFormat(new_id_Emu_Games)
		sSQL &= "	WHERE	id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games)
		sSQL &= "				And	Not EXISTS"
		sSQL &= "				("
		sSQL &= "					Select * FROM"
		sSQL &= "					tbl_Emu_Games_Moby_Genres EGMG2"
		sSQL &= "					WHERE EGMG2.id_Emu_Games = " & TC.getSQLFormat(new_id_Emu_Games)
		sSQL &= "								And EGMG2.id_Moby_Genres = tbl_Emu_Games_Moby_Genres.id_Moby_Genres"
		sSQL &= "				)"
		sSQL &= "	;"
		sSQL &= "	DELETE FROM tbl_Emu_Games_Moby_Genres WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games)
		DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
	End Sub

	Public Sub Merge_tbl_Emu_Games_Regions(ByRef tran As SQLite.SQLiteTransaction, ByVal old_id_Emu_Games As Integer, ByVal new_id_Emu_Games As Integer)
		Dim sSQL As String = ""
		sSQL &= "	UPDATE tbl_Emu_Games_Regions"
		sSQL &= "	Set id_Emu_Games = " & TC.getSQLFormat(new_id_Emu_Games)
		sSQL &= "	WHERE	id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games)
		sSQL &= "				And	Not EXISTS"
		sSQL &= "				("
		sSQL &= "					Select * FROM"
		sSQL &= "					tbl_Emu_Games_Regions EGR2"
		sSQL &= "					WHERE EGR2.id_Emu_Games = " & TC.getSQLFormat(new_id_Emu_Games)
		sSQL &= "								And EGR2.id_Regions = tbl_Emu_Games_Regions.id_Regions"
		sSQL &= "				)"
		sSQL &= "	;"
		sSQL &= "	DELETE FROM tbl_Emu_Games_Regions WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games)
		DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
	End Sub

	Public Sub Merge_tbl_Emu_Games(ByRef tran As SQLite.SQLiteTransaction, ByVal old_id_Emu_Games As Integer, ByVal new_id_Emu_Games As Integer)
		Dim sSQL As String = ""
		sSQL &= "	UPDATE tbl_Emu_Games"
		sSQL &= "	Set		Hidden = IFNULL((Select Hidden FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Hidden)"
		sSQL &= "				, Moby_Games_URLPart = IFNULL((Select Moby_Games_URLPart FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Moby_Games_URLPart)"
		sSQL &= "				, id_Moby_Platforms = IFNULL((Select id_Moby_Platforms FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), id_Moby_Platforms)"
		sSQL &= "				, id_Rombase = IFNULL((Select id_Rombase FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), id_Rombase)"
		sSQL &= "				, id_Emulators = IFNULL((Select id_Emulators FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), id_Emulators)"
		sSQL &= "				, Name = IFNULL((Select Name FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Name)"
		sSQL &= "				, Name_Prefix = IFNULL((Select Name_Prefix FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Name_Prefix)"
		sSQL &= "				, Note = IFNULL((Select Note FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Note)"
		sSQL &= "				, Publisher = IFNULL((Select Publisher FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Publisher)"
		sSQL &= "				, Publisher_id_Moby_Companies = IFNULL((Select Publisher_id_Moby_Companies FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Publisher_id_Moby_Companies)"
		sSQL &= "				, Developer = IFNULL((Select Developer FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Developer)"
		sSQL &= "				, Developer_id_Moby_Companies = IFNULL((Select Developer_id_Moby_Companies FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Developer_id_Moby_Companies)"
		sSQL &= "				, Description = IFNULL((Select Description FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Description)"
		sSQL &= "				, Favourite = IFNULL((Select Favourite FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Favourite)"
		sSQL &= "				, Rating_Gameplay = IFNULL((Select Rating_Gameplay FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Rating_Gameplay)"
		sSQL &= "				, Rating_Graphics = IFNULL((Select Rating_Graphics FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Rating_Graphics)"
		sSQL &= "				, Rating_Sound = IFNULL((Select Rating_Sound FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Rating_Sound)"
		sSQL &= "				, Rating_Story = IFNULL((Select Rating_Story FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Rating_Story)"
		sSQL &= "				, Rating_Personal = IFNULL((Select Rating_Personal FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Rating_Personal)"
		sSQL &= "				, Num_Played = IFNULL((Select Num_Played FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Num_Played)"
		sSQL &= "				, Num_Runtime = IFNULL((Select Num_Runtime FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Num_Runtime)"
		sSQL &= "				, Year = IFNULL((Select Year FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Year)"
		sSQL &= "				, Version = IFNULL((Select Version FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Version)"
		sSQL &= "				, Alt = IFNULL((Select Alt FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Alt)"
		sSQL &= "				, Trainer = IFNULL((Select Trainer FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Trainer)"
		sSQL &= "				, Translation = IFNULL((Select Translation FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Translation)"
		sSQL &= "				, Hack = IFNULL((Select Hack FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Hack)"
		sSQL &= "				, Bios = IFNULL((Select Bios FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Bios)"
		sSQL &= "				, Prototype = IFNULL((Select Prototype FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Prototype)"
		sSQL &= "				, Alpha = IFNULL((Select Alpha FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Alpha)"
		sSQL &= "				, Beta = IFNULL((Select Beta FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Beta)"
		sSQL &= "				, Sample = IFNULL((Select Sample FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Sample)"
		sSQL &= "				, Kiosk = IFNULL((Select Kiosk FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Kiosk)"
		sSQL &= "				, Unlicensed = IFNULL((Select Unlicensed FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Unlicensed)"
		sSQL &= "				, Fixed = IFNULL((Select Fixed FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Fixed)"
		sSQL &= "				, Pirated = IFNULL((Select Pirated FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Pirated)"
		sSQL &= "				, Good = IFNULL((Select Good FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Good)"
		sSQL &= "				, Bad = IFNULL((Select Bad FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Bad)"
		sSQL &= "				, Overdump = IFNULL((Select Overdump FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), Overdump)"
		sSQL &= "				, PublicDomain = IFNULL((Select PublicDomain FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & "), PublicDomain)"
		sSQL &= "	WHERE	id_Emu_Games = " & TC.getSQLFormat(new_id_Emu_Games) & " Or id_Emu_Games_Owner = " & TC.getSQLFormat(new_id_Emu_Games)
		sSQL &= "	;"
		sSQL &= "	DELETE FROM tbl_Emu_Games WHERE	id_Emu_Games = " & TC.getSQLFormat(old_id_Emu_Games) & " Or id_Emu_Games_Owner = " & TC.getSQLFormat(old_id_Emu_Games)
		DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
	End Sub
#End Region

#Region "Update Scripts"
	Public Shared Function GenSQL_Update_tbl_Emu_Games_Caches_Genre(ByVal id_Moby_Genres_Categories, ByVal CacheName)
		Dim sSQL As String = ""
		sSQL &= "	" & ControlChars.CrLf
		sSQL &= "			, " & CacheName & " =" & ControlChars.CrLf
		sSQL &= "				(" & ControlChars.CrLf
		sSQL &= "					Select group_concat(Name, ', ')" & ControlChars.CrLf
		sSQL &= "					FROM" & ControlChars.CrLf
		sSQL &= "					(	SELECT Name FROM"
		sSQL &= "						(" & ControlChars.CrLf
		sSQL &= "							SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "							FROM tbl_Moby_Games_Genres GG" & ControlChars.CrLf
		sSQL &= "							WHERE GG.id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = tbl_Emu_Games.Moby_Games_URLPart)" & ControlChars.CrLf
		sSQL &= "							UNION SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "							FROM tbl_Emu_Games_Moby_Genres EGMG" & ControlChars.CrLf
		sSQL &= "							WHERE EGMG.id_Emu_Games = tbl_Emu_Games.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "						) AS temp_Genres" & ControlChars.CrLf
		sSQL &= "						LEFT JOIN tbl_Moby_Genres G ON temp_Genres.id_Moby_Genres = G.id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "						WHERE G.id_Moby_Genres_Categories = " & id_Moby_Genres_Categories & ControlChars.CrLf
		sSQL &= "									AND G.id_Moby_Genres NOT IN" & ControlChars.CrLf
		sSQL &= "									(" & ControlChars.CrLf
		sSQL &= "										SELECT id_Moby_Genres" & ControlChars.CrLf
		sSQL &= "										FROM tbl_Emu_Games_Moby_Genres" & ControlChars.CrLf
		sSQL &= "										WHERE id_Emu_Games = tbl_Emu_Games.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "													AND Used = 0" & ControlChars.CrLf
		sSQL &= "									)" & ControlChars.CrLf
		sSQL &= "					)" & ControlChars.CrLf
		sSQL &= "					ORDER BY Name" & ControlChars.CrLf
		sSQL &= "				)" & ControlChars.CrLf

		Return sSQL
	End Function

	Public Shared Sub Update_tbl_Emu_Games_Caches(ByRef tran As SQLite.SQLiteTransaction, Optional ByVal id_Emu_Games As Integer = 0)
		Dim sSQL As String = ""
		sSQL &= "	UPDATE tbl_Emu_Games" & ControlChars.CrLf
		sSQL &= "	SET	 	Cache_Regions = " & ControlChars.CrLf
		sSQL &= "				(" & ControlChars.CrLf
		sSQL &= "					SELECT group_concat(R.Region, ',') FROM tbl_Emu_Games_Regions EGR INNER JOIN moby.tbl_Moby_Regions R ON EGR.id_Regions = R.id_Moby_Regions WHERE EGR.id_Emu_Games = tbl_Emu_Games.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "				)" & ControlChars.CrLf
		sSQL &= "	" & ControlChars.CrLf
		sSQL &= "			,	Cache_Languages = " & ControlChars.CrLf
		sSQL &= "				(" & ControlChars.CrLf
		sSQL &= "					IFNULL((SELECT group_concat(L.Language_Short, ',') FROM tbl_Emu_Games_Languages EGL INNER JOIN tbl_Languages L ON EGL.id_Languages = L.id_Languages	WHERE EGL.id_Emu_Games = tbl_Emu_Games.id_Emu_Games), '(En)')" & ControlChars.CrLf
		sSQL &= "				)" & ControlChars.CrLf
		sSQL &= "	" & ControlChars.CrLf
		sSQL &= "			, Cache_Age_Pessimistic =" & ControlChars.CrLf
		sSQL &= "				(" & ControlChars.CrLf
		sSQL &= "					SELECT MAX(A.Rating_Age_From)" & ControlChars.CrLf
		sSQL &= "					FROM" & ControlChars.CrLf
		sSQL &= "					(" & ControlChars.CrLf
		sSQL &= "						SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "						FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "						WHERE RA.id_Moby_Releases = (SELECT id_Moby_Releases FROM moby.tbl_Moby_Releases REL WHERE REL.id_Moby_Platforms = IFNULL(tbl_Emu_Games.id_Moby_Platforms_Alternative, tbl_Emu_Games.id_Moby_Platforms) AND REL.id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = tbl_Emu_Games.Moby_Games_URLPart))" & ControlChars.CrLf
		sSQL &= "						UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "						FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "						WHERE EGMA.id_Emu_Games = tbl_Emu_Games.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "					) " & ControlChars.CrLf
		sSQL &= "					AS temp_Attributes " & ControlChars.CrLf
		sSQL &= "					LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes " & ControlChars.CrLf
		sSQL &= "					WHERE A.id_Moby_Attributes NOT IN " & ControlChars.CrLf
		sSQL &= "					(" & ControlChars.CrLf
		sSQL &= "						SELECT id_Moby_Attributes FROM tbl_Emu_Games_Moby_Attributes MA" & ControlChars.CrLf
		sSQL &= "						WHERE MA.id_Emu_Games = tbl_Emu_Games.id_Emu_Games " & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "					)" & ControlChars.CrLf
		sSQL &= "				)" & ControlChars.CrLf
		sSQL &= "	" & ControlChars.CrLf
		sSQL &= "			, Cache_Age_Optimistic =" & ControlChars.CrLf
		sSQL &= "				(" & ControlChars.CrLf
		sSQL &= "					SELECT MIN(A.Rating_Age_From)" & ControlChars.CrLf
		sSQL &= "					FROM" & ControlChars.CrLf
		sSQL &= "					(" & ControlChars.CrLf
		sSQL &= "						SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "						FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "						WHERE RA.id_Moby_Releases = (SELECT id_Moby_Releases FROM moby.tbl_Moby_Releases REL WHERE REL.id_Moby_Platforms = IFNULL(tbl_Emu_Games.id_Moby_Platforms_Alternative, tbl_Emu_Games.id_Moby_Platforms) AND REL.id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = tbl_Emu_Games.Moby_Games_URLPart))" & ControlChars.CrLf
		sSQL &= "						UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "						FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "						WHERE EGMA.id_Emu_Games = tbl_Emu_Games.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "					)" & ControlChars.CrLf
		sSQL &= "					AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "					LEFT JOIN tbl_Moby_Attributes A" & ControlChars.CrLf
		sSQL &= "					ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "					WHERE A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "					(" & ControlChars.CrLf
		sSQL &= "						SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "						FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "						WHERE id_Emu_Games = tbl_Emu_Games.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "					)" & ControlChars.CrLf
		sSQL &= "				)" & ControlChars.CrLf
		sSQL &= "	" & ControlChars.CrLf
		sSQL &= "			, Cache_Alternate_Titles =" & ControlChars.CrLf
		sSQL &= "				(" & ControlChars.CrLf
		sSQL &= "					SELECT group_concat(IFNULL('""' || Alternate_Title || '""', '') || IFNULL(' - ' || Description, ''), CAST(X'0D' AS TEXT) || CAST(X'0A' AS TEXT))" & ControlChars.CrLf
		sSQL &= "					FROM tbl_Moby_Games_Alternate_Titles ALT " & ControlChars.CrLf
		sSQL &= "					WHERE ALT.id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = tbl_Emu_Games.Moby_Games_URLPart)" & ControlChars.CrLf
		sSQL &= "				)" & ControlChars.CrLf
		sSQL &= "	" & ControlChars.CrLf

		sSQL &= GenSQL_Update_tbl_Emu_Games_Caches_Genre(cls_Globals.enm_Moby_Genres_Categories.Basic_Genres, "Cache_Basic_Genres")
		sSQL &= GenSQL_Update_tbl_Emu_Games_Caches_Genre(cls_Globals.enm_Moby_Genres_Categories.Perspective, "Cache_Perspectives")
		sSQL &= GenSQL_Update_tbl_Emu_Games_Caches_Genre(cls_Globals.enm_Moby_Genres_Categories.Sports_Themes, "Cache_Sports_Themes")
		sSQL &= GenSQL_Update_tbl_Emu_Games_Caches_Genre(cls_Globals.enm_Moby_Genres_Categories.Educational_Categories, "Cache_Educational_Categories")
		sSQL &= GenSQL_Update_tbl_Emu_Games_Caches_Genre(cls_Globals.enm_Moby_Genres_Categories.Other_Attributes, "Cache_Other_Attributes")
		sSQL &= GenSQL_Update_tbl_Emu_Games_Caches_Genre(cls_Globals.enm_Moby_Genres_Categories.Visual_Presentation, "Cache_Visual_Presentation")
		sSQL &= GenSQL_Update_tbl_Emu_Games_Caches_Genre(cls_Globals.enm_Moby_Genres_Categories.Pacing, "Cache_Pacing")
		sSQL &= GenSQL_Update_tbl_Emu_Games_Caches_Genre(cls_Globals.enm_Moby_Genres_Categories.Gameplay, "Cache_Gameplay")
		sSQL &= GenSQL_Update_tbl_Emu_Games_Caches_Genre(cls_Globals.enm_Moby_Genres_Categories.Interface_Control, "Cache_Interface_Control")
		sSQL &= GenSQL_Update_tbl_Emu_Games_Caches_Genre(cls_Globals.enm_Moby_Genres_Categories.Vehicular_Themes, "Cache_Vehicular_Themes")
		sSQL &= GenSQL_Update_tbl_Emu_Games_Caches_Genre(cls_Globals.enm_Moby_Genres_Categories.Setting, "Cache_Setting")
		sSQL &= GenSQL_Update_tbl_Emu_Games_Caches_Genre(cls_Globals.enm_Moby_Genres_Categories.Narrative_Theme_Topic, "Cache_Narrative_Theme_Topic")
		sSQL &= GenSQL_Update_tbl_Emu_Games_Caches_Genre(cls_Globals.enm_Moby_Genres_Categories.DLC_Addon, "Cache_DLC_Addon")
		sSQL &= GenSQL_Update_tbl_Emu_Games_Caches_Genre(cls_Globals.enm_Moby_Genres_Categories.Special_Edition, "Cache_Special_Edition")

		sSQL &= "	" & ControlChars.CrLf
		sSQL &= "			, Cache_MinPlayers =" & ControlChars.CrLf
		sSQL &= "				(" & ControlChars.CrLf
		sSQL &= "					SELECT MIN(A.MinPlayers)" & ControlChars.CrLf
		sSQL &= "					FROM" & ControlChars.CrLf
		sSQL &= "					(" & ControlChars.CrLf
		sSQL &= "						SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "						FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "						WHERE RA.id_Moby_Releases = (SELECT id_Moby_Releases FROM moby.tbl_Moby_Releases REL WHERE REL.id_Moby_Platforms = IFNULL(tbl_Emu_Games.id_Moby_Platforms_Alternative, tbl_Emu_Games.id_Moby_Platforms) AND REL.id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = tbl_Emu_Games.Moby_Games_URLPart))" & ControlChars.CrLf
		sSQL &= "						UNION" & ControlChars.CrLf
		sSQL &= "						SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "						FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "						WHERE EGMA.id_Emu_Games = tbl_Emu_Games.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "					) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "					LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "					WHERE A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "					(" & ControlChars.CrLf
		sSQL &= "						SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "						FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "						WHERE id_Emu_Games = tbl_Emu_Games.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "									AND Used = 0" & ControlChars.CrLf
		sSQL &= "					)" & ControlChars.CrLf
		sSQL &= "				)" & ControlChars.CrLf
		sSQL &= "	" & ControlChars.CrLf
		sSQL &= "			, Cache_MaxPlayers =" & ControlChars.CrLf
		sSQL &= "				(" & ControlChars.CrLf
		sSQL &= "					SELECT MAX(A.MaxPlayers)" & ControlChars.CrLf
		sSQL &= "					FROM" & ControlChars.CrLf
		sSQL &= "					(" & ControlChars.CrLf
		sSQL &= "						SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "						FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "						WHERE RA.id_Moby_Releases = (SELECT id_Moby_Releases FROM moby.tbl_Moby_Releases REL WHERE REL.id_Moby_Platforms = IFNULL(tbl_Emu_Games.id_Moby_Platforms_Alternative, tbl_Emu_Games.id_Moby_Platforms) AND REL.id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = tbl_Emu_Games.Moby_Games_URLPart))" & ControlChars.CrLf
		sSQL &= "						UNION SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "						FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "						WHERE EGMA.id_Emu_Games = tbl_Emu_Games.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "					) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "					LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "					WHERE A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "					(" & ControlChars.CrLf
		sSQL &= "						SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "						FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "						WHERE id_Emu_Games = tbl_Emu_Games.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "						AND Used = 0" & ControlChars.CrLf
		sSQL &= "					)" & ControlChars.CrLf
		sSQL &= "				)" & ControlChars.CrLf
		sSQL &= "	" & ControlChars.CrLf
		sSQL &= "			, Cache_MP_GameModes =" & ControlChars.CrLf
		sSQL &= "				(" & ControlChars.CrLf
		sSQL &= "					SELECT group_concat(A.Name, ', ')" & ControlChars.CrLf
		sSQL &= "					FROM" & ControlChars.CrLf
		sSQL &= "					(" & ControlChars.CrLf
		sSQL &= "						SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "						FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "						WHERE RA.id_Moby_Releases = (SELECT id_Moby_Releases FROM moby.tbl_Moby_Releases REL WHERE REL.id_Moby_Platforms = IFNULL(tbl_Emu_Games.id_Moby_Platforms_Alternative, tbl_Emu_Games.id_Moby_Platforms) AND REL.id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = tbl_Emu_Games.Moby_Games_URLPart))" & ControlChars.CrLf
		sSQL &= "						UNION" & ControlChars.CrLf
		sSQL &= "						SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "						FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "						WHERE EGMA.id_Emu_Games = tbl_Emu_Games.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "					) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "					LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "					WHERE A.id_Moby_Attributes_Categories = 16" & ControlChars.CrLf
		sSQL &= "								AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									WHERE id_Emu_Games = tbl_Emu_Games.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												AND Used = 0" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "				)" & ControlChars.CrLf
		sSQL &= "	" & ControlChars.CrLf
		sSQL &= "			, Cache_MP_Options =" & ControlChars.CrLf
		sSQL &= "				(" & ControlChars.CrLf
		sSQL &= "					SELECT group_concat(A.Name, ', ')" & ControlChars.CrLf
		sSQL &= "					FROM" & ControlChars.CrLf
		sSQL &= "					(" & ControlChars.CrLf
		sSQL &= "						SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "						FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "						WHERE RA.id_Moby_Releases = (SELECT id_Moby_Releases FROM moby.tbl_Moby_Releases REL WHERE REL.id_Moby_Platforms = IFNULL(tbl_Emu_Games.id_Moby_Platforms_Alternative, tbl_Emu_Games.id_Moby_Platforms) AND REL.id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = tbl_Emu_Games.Moby_Games_URLPart))" & ControlChars.CrLf
		sSQL &= "						UNION" & ControlChars.CrLf
		sSQL &= "						SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "						FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "						WHERE EGMA.id_Emu_Games = tbl_Emu_Games.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "					) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "					LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "					WHERE A.id_Moby_Attributes_Categories = 12" & ControlChars.CrLf
		sSQL &= "								AND A.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "								(" & ControlChars.CrLf
		sSQL &= "									SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "									WHERE id_Emu_Games = tbl_Emu_Games.id_Emu_Games" & ControlChars.CrLf
		sSQL &= "												AND Used = 0" & ControlChars.CrLf
		sSQL &= "								)" & ControlChars.CrLf
		sSQL &= "				)" & ControlChars.CrLf

		If id_Emu_Games <> 0 Then
			sSQL &= " WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games)
		End If

		DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
	End Sub

	Public Sub Update_Platform_NumGames_Cache_AllUsers(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Moby_Platforms As Int64)
		Dim ar_Users As New ArrayList
		ar_Users.Add(0L) 'all unrestricted users

		Dim tbl_Users As DataTable = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT id_Users FROM tbl_Users WHERE restricted = 1", Nothing, tran)
		For Each row_Users As DataRow In tbl_Users.Rows
			ar_Users.Add(row_Users("id_Users"))
		Next

		For Each id_Users As Int64 In ar_Users
			Update_Platform_NumGames_Cache(tran, id_Moby_Platforms, id_Users)
			If id_Moby_Platforms <> -1 Then
				Update_Platform_NumGames_Cache(tran, -1, id_Users)
			End If
		Next
	End Sub

	''' <summary>
	''' 
	''' </summary>
	''' <param name="tran"></param>
	''' <param name="id_Moby_Platforms"></param>
	''' <param name="id_Users">Use only if you want to impersonate a resticted! user</param>
	''' <returns></returns>
	Public Shared Function Update_Platform_NumGames_Cache(ByRef tran As SQLite.SQLiteTransaction, ByVal id_Moby_Platforms As Int64, Optional ByVal id_Users As Int64 = 0) As Boolean
		If id_Users = 0 AndAlso cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0 AndAlso cls_Globals.Restricted Then
			id_Users = cls_Globals.id_Users
		End If

		Dim sSQL_numGames As String = ""
		sSQL_numGames &= "	SELECT	COUNT(1)" & ControlChars.CrLf
		sSQL_numGames &= "	FROM tbl_Emu_Games EG" & ControlChars.CrLf
		If (id_Users > 0) Then
			sSQL_numGames &= " INNER JOIN tbl_Users_Emu_Games USREG ON id_Users = " & TC.getSQLFormat(id_Users) & " AND EG.id_Emu_Games = USREG.id_Emu_Games"
		End If
		sSQL_numGames &= "	WHERE	EG.id_Emu_Games_Owner IS NULL AND (EG.Hidden IS NULL OR EG.Hidden = 0)" & ControlChars.CrLf
		If (id_Moby_Platforms <> -1) Then
			sSQL_numGames &= "	AND id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms)
		End If

		Dim numGames As Int64 = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, sSQL_numGames, tran), 0L)

		Dim sSQL_id_Moby_Platforms_Caches As String = ""
		sSQL_id_Moby_Platforms_Caches &= "	SELECT id_Moby_Platforms_Caches" & ControlChars.CrLf
		sSQL_id_Moby_Platforms_Caches &= "	FROM tbl_Moby_Platforms_Caches" & ControlChars.CrLf
		sSQL_id_Moby_Platforms_Caches &= "	WHERE id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms) & ControlChars.CrLf
		If id_Users > 0 Then
			sSQL_id_Moby_Platforms_Caches &= "	AND id_Users = " & TC.getSQLFormat(id_Users) & ControlChars.CrLf
		Else
			sSQL_id_Moby_Platforms_Caches &= "	AND id_Users IS NULL" & ControlChars.CrLf
		End If

		Dim id_Moby_Platform_Caches As Int64 = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, sSQL_id_Moby_Platforms_Caches, tran), 0L)

		If id_Moby_Platform_Caches > 0 Then
			'Update
			Dim sSQL_Update As String = ""
			sSQL_Update &= "UPDATE tbl_Moby_Platforms_Caches SET" & ControlChars.CrLf
			sSQL_Update &= "	NumGames = " & TC.getSQLFormat(numGames) & ControlChars.CrLf
			sSQL_Update &= "WHERE id_Moby_Platforms_Caches = " & TC.getSQLFormat(id_Moby_Platform_Caches)

			Return DataAccess.FireProcedure(tran.Connection, 0, sSQL_Update, tran)
		Else
			'Insert
			Dim sSQL_Insert As String = ""
			sSQL_Insert &= "INSERT INTO tbl_Moby_Platforms_Caches (id_Moby_Platforms, id_Users, NumGames) VALUES (" & TC.getSQLParameter(id_Moby_Platforms, IIf(id_Users > 0, id_Users, DBNull.Value), numGames) & ")"
			Return DataAccess.FireProcedure(tran.Connection, 0, sSQL_Insert, tran)
		End If

		Return False
	End Function

	Public Shared Sub Update_Emu_Games_Unavailable(ByVal id_Emu_Games As Int64, ByVal Unavailable As Boolean, Optional ByRef tran As SQLite.SQLiteTransaction = Nothing)
		Dim conn As SQLite.SQLiteConnection
		If tran IsNot Nothing Then
			conn = tran.Connection
		Else
			conn = cls_Globals.Conn
		End If
		DataAccess.FireProcedure(conn, 0, "UPDATE tbl_Emu_Games SET Unavailable = " & TC.getSQLFormat(Unavailable) & " WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), tran)
	End Sub
#End Region

#Region "Transformations"
	''' <summary>
	''' 
	''' </summary>
	''' <param name="tbl_Emu_Games">Table filled with Files and optional with CWDs</param>
	''' <param name="tbl_Emu_Games_CWDs">Optional Table filled with CWDs</param>
	Public Shared Sub Prepare_tmp_DOSBox_DisplayText(ByRef tbl_Emu_Games As DS_ML.tbl_Emu_GamesDataTable, Optional ByRef tbl_Emu_Games_CWDs As DS_ML.tbl_Emu_GamesDataTable = Nothing)
		If tbl_Emu_Games_CWDs Is Nothing Then
			tbl_Emu_Games_CWDs = tbl_Emu_Games
		End If

		For Each row As DS_ML.tbl_Emu_GamesRow In tbl_Emu_Games.Rows
			'If TC.NZ(row("id_Rombase_DOSBox_Filetypes"), 0) <> cls_Globals.enm_Rombase_DOSBox_Filetypes.cwd Then
			Dim oInnerFile As Object = row("InnerFile")
			Dim oFolder As Object = row("Folder")

			If TC.NZ(oInnerFile, "").Length = 0 Then
				row("tmp_DOSBox_DisplayText") = oFolder
				Continue For
			End If

			For Each row_cwd As DS_ML.tbl_Emu_GamesRow In tbl_Emu_Games_CWDs.Rows
				If TC.NZ(row_cwd("id_Rombase_DOSBox_Filetypes"), 0) = cls_Globals.enm_Rombase_DOSBox_Filetypes.cwd Then
					If TC.NZ(row_cwd("Folder"), "") <> "" AndAlso row("Folder").ToString.Contains(row_cwd("Folder")) Then
						If TC.NZ(row("InnerFile"), "") <> "" Then
							row("tmp_DOSBox_DisplayText") = row("Folder").Replace(row_cwd("Folder"), "") & "\" & row("InnerFile")
						End If
					End If
				End If
			Next

			If TC.NZ(row("tmp_DOSBox_DisplayText"), "") = "" Then
				row("tmp_DOSBox_DisplayText") = TC.NZ(oInnerFile, "")
			End If

			'Packed content contains forward slashes, we want to display backward slashes, also we don't want a prepended backward slash
			row("tmp_DOSBox_DisplayText") = MKNetLib.cls_MKStringSupport.Clean_Left(TC.NZ(row("tmp_DOSBox_DisplayText"), "").Replace("/", "\"), "\")
			'End If
		Next

	End Sub

#End Region

#Region "Rombase -> Main Migration Scripts"
	Public Shared Sub Migrate_Rombase_DOSBox_Configs(ByRef conn As SQLite.SQLiteConnection)
		Dim iMissingDOSBoxConfigs As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(conn, 0, "SELECT COUNT(1) FROM rombase.tbl_Rombase_DOSBox_Configs WHERE id_Rombase_DOSBox_Configs NOT IN (SELECT id_Rombase_DOSBox_Configs FROM main.tbl_DOSBox_Configs WHERE id_Rombase_DOSBox_Configs IS NOT NULL)"), 0)

		If iMissingDOSBoxConfigs > 0 Then
			Dim dt_Colinfo As DataTable = DataAccess.FireProcedureReturnDT(conn, 0, False, "SELECT * FROM main.tbl_DOSBox_Configs LIMIT 0")

			Dim sSQL As String = ""
			sSQL &= "	INSERT INTO tbl_DOSBox_Configs" & ControlChars.CrLf

			Dim sSQL_Columns As String = "	(isTemplate, id_Rombase_DOSBox_Configs" & ControlChars.CrLf
			Dim sSQL_Rombase_Select As String = "	SELECT 1, id_Rombase_DOSBox_Configs, " & ControlChars.CrLf

			Dim Blacklist As String() = {"id_DOSBox_Configs", "isTemplate", "id_Rombase_DOSBox_Configs"}
			sSQL_Columns &= "	, " & MKNetLib.cls_MKClientSupport.ListColumnNames(dt_Colinfo, Blacklist)


			Dim bFirst As Boolean = True

			For i As Integer = 0 To dt_Colinfo.Columns.Count - Blacklist.Length - 1
				If bFirst Then bFirst = False Else sSQL_Rombase_Select &= "	, "
				sSQL_Rombase_Select &= "NULL"
			Next

			sSQL_Columns &= "	)" & ControlChars.CrLf
			sSQL_Rombase_Select &= "	FROM rombase.tbl_Rombase_DOSBox_Configs WHERE id_Rombase_DOSBox_Configs NOT IN (SELECT id_Rombase_DOSBox_Configs FROM main.tbl_DOSBox_Configs WHERE id_Rombase_DOSBox_Configs IS NOT NULL)" & ControlChars.CrLf

			sSQL &= sSQL_Columns & sSQL_Rombase_Select

			DataAccess.FireProcedureReturnScalar(conn, 0, sSQL)
		End If

		'TODO: UPDATE from Rombase
	End Sub

	Public Shared Sub Migrate_Rombase_ScummVM_Configs(ByRef conn As SQLite.SQLiteConnection)
		Dim iMissingScummVMConfigs As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(conn, 0, "SELECT COUNT(1) FROM rombase.tbl_Rombase_ScummVM_Configs WHERE id_Rombase_ScummVM_Configs NOT IN (SELECT id_Rombase_ScummVM_Configs FROM main.tbl_ScummVM_Configs WHERE id_Rombase_ScummVM_Configs IS NOT NULL)"), 0)

		If iMissingScummVMConfigs > 0 Then
			Dim dt_Colinfo As DataTable = DataAccess.FireProcedureReturnDT(conn, 0, False, "SELECT * FROM main.tbl_ScummVM_Configs LIMIT 0")

			Dim sSQL As String = ""
			sSQL &= "	INSERT INTO tbl_ScummVM_Configs" & ControlChars.CrLf

			Dim sSQL_Columns As String = "	(isTemplate, id_Rombase_ScummVM_Configs" & ControlChars.CrLf
			Dim sSQL_Rombase_Select As String = "	SELECT 1, id_Rombase_ScummVM_Configs, " & ControlChars.CrLf

			Dim Blacklist As String() = {"id_ScummVM_Configs", "isTemplate", "id_Rombase_ScummVM_Configs"}
			sSQL_Columns &= "	, " & MKNetLib.cls_MKClientSupport.ListColumnNames(dt_Colinfo, Blacklist)


			Dim bFirst As Boolean = True

			For i As Integer = 0 To dt_Colinfo.Columns.Count - Blacklist.Length - 1
				If bFirst Then bFirst = False Else sSQL_Rombase_Select &= "	, "
				sSQL_Rombase_Select &= "NULL"
			Next

			sSQL_Columns &= "	)" & ControlChars.CrLf
			sSQL_Rombase_Select &= "	FROM rombase.tbl_Rombase_ScummVM_Configs WHERE id_Rombase_ScummVM_Configs NOT IN (SELECT id_Rombase_ScummVM_Configs FROM main.tbl_ScummVM_Configs WHERE id_Rombase_ScummVM_Configs IS NOT NULL)" & ControlChars.CrLf

			sSQL &= sSQL_Columns & sSQL_Rombase_Select

			DataAccess.FireProcedureReturnScalar(conn, 0, sSQL)
		End If

		'TODO: UPDATE from Rombase
	End Sub
#End Region
End Class
