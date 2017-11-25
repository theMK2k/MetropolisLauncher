Public Class cls_Extras
	Public Class cls_Extras_Result
		Public _Path As String = ""
		Public _ExtraNum As Integer = 0
		Public _NoExtraFound As Boolean = True
		Public _ExtraType As Object = Nothing
		Public _WrappedToFirst As Boolean = False 'True when the current extra is the very first after wrapping from the last

		Public Sub New(ByVal Path As String, ByVal ExtraNum As Integer, ByVal NoExtraFound As Boolean, ByVal ExtraType As Object, ByVal WrappedToFirst As Boolean)
			_Path = Path
			_ExtraNum = ExtraNum
			_NoExtraFound = NoExtraFound
			_ExtraType = ExtraType
			_WrappedToFirst = WrappedToFirst
		End Sub
	End Class

	Public Shared _SupportedExtensions As String() = {".png", ".jpg", ".bmp"}
	Public Shared _SupportedExtensions_Masks As String() = {"*.png", "*.jpg", "*.bmp"}

	Public Shared Function FindNextExtraFromSupportedExtensions(ByVal PathWithoutExtension) As String
		For Each ext As String In _SupportedExtensions
			If Alphaleonis.Win32.Filesystem.File.Exists(PathWithoutExtension & ext) Then
				Return PathWithoutExtension & ext
			End If
		Next

		Return ""
	End Function

	Public Shared Function getExtraSuffix(ExtraNum) As String
		If ExtraNum = 0 Then
			Return ""
		Else
			Return "_" & ExtraNum.ToString.PadLeft(3, "0")
		End If
	End Function

	Public Shared Function FindNextExtra(ByVal id_Emu_Games As Integer, ByVal ExtraNum As Integer, ByVal SkipToNextImmediately As Boolean, ByVal ExtraType As Object, Optional ByVal IgnoreHiddenExtraCategories As Boolean = False, Optional ByVal LimitToExtraType As Boolean = False) As cls_Extras_Result
		Dim dt_Emu_Games As New DS_ML.src_ucr_Emulation_GamesDataTable

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Fill_src_ucr_Emulation_Games(tran, dt_Emu_Games, Nothing, Nothing, Nothing, id_Emu_Games)
		End Using

		If dt_Emu_Games.Rows.Count <> 1 Then Return New cls_Extras_Result("", 0, True, Nothing, False)

		Dim row_Emu_Games As DataRow = dt_Emu_Games.Rows(0)

		Dim Platform_Short As String = TC.NZ(row_Emu_Games("Platform_Short"), "")
		Dim id_Moby_Platforms As Integer = row_Emu_Games("id_Moby_Platforms")
		Dim Game As String = TC.NZ(row_Emu_Games("Game"), "")

		Dim FileName As String = ""
		If TC.NZ(row_Emu_Games("InnerFile"), "") <> "" Then
			FileName = row_Emu_Games("InnerFile")
		Else
			FileName = row_Emu_Games("File")
		End If

		Return FindNextExtra(Platform_Short, id_Moby_Platforms, Game, FileName, ExtraNum, SkipToNextImmediately, ExtraType, IgnoreHiddenExtraCategories, LimitToExtraType)
	End Function

	Public Shared Function FindNextExtra(ByVal Platform_Short As String, ByVal id_Moby_Platforms As Integer, ByVal Game As String, ByVal FileName As String, ByVal ExtraNum As Integer, ByVal SkipToNextImmediately As Boolean, ByVal ExtraType As Object, Optional ByVal IgnoreHiddenExtraCategories As Boolean = False, Optional ByVal LimitToExtraType As Boolean = False) As cls_Extras_Result
		If TC.NZ(ExtraType, "").Length = 0 Then Return New cls_Extras_Result("", 0, True, Nothing, False)

		If SkipToNextImmediately Then
			ExtraNum = ExtraNum + 1
		End If

		Dim bDone As Boolean = False
		Dim iNext As Integer = 0

		Dim sPlatform As String = Platform_Short
		Dim sRom As String = ""

		Dim NoExtraFound As Boolean = False

		Select Case TC.NZ(id_Moby_Platforms, 0)
			Case cls_Globals.enm_Moby_Platforms.win
				sRom = GetExtraFilename(Game, FileName)
			Case Else
				sRom = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(TC.NZ(MKNetLib.cls_MKStringSupport.GetCleanFileName(FileName), ""))
		End Select

		Dim sNextExtra As String = ""

		Dim bWrappedToFirst = False

		While Not bDone AndAlso iNext < 2
			Dim sFileName As String = FindNextExtraFromSupportedExtensions(cls_Globals.Dir_Extras & "\emulation\" & sPlatform & "\" & ExtraType & "\" & sRom & getExtraSuffix(ExtraNum))  'IIf(ExtraNum = 0, "", " [image" & (ExtraNum + 1) & "]")

			If Alphaleonis.Win32.Filesystem.File.Exists(sFileName) Then
				sNextExtra = sFileName
				bDone = True
			Else
				If Not LimitToExtraType Then
					ExtraType = DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Name FROM tbl_Emu_Extras WHERE Sort = (SELECT MIN(Sort) FROM tbl_Emu_Extras WHERE" & IIf(IgnoreHiddenExtraCategories, "", " IFNULL(Hide, 0) = 0 AND ") & " Sort > (SELECT Sort FROM tbl_Emu_Extras WHERE Name = '" & ExtraType & "')) LIMIT 1")
				Else
					iNext = iNext + 1
				End If

				ExtraNum = 0

				If TC.NZ(ExtraType, "").Length = 0 Then
					bWrappedToFirst = True
					ExtraType = DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Name FROM tbl_Emu_Extras WHERE Sort = (SELECT MIN(Sort) from tbl_Emu_Extras " & IIf(IgnoreHiddenExtraCategories, "", "WHERE IFNULL(Hide, 0) = 0") & ") LIMIT 1")
					iNext = iNext + 1
				End If
			End If
		End While

		If iNext = 2 Then
			NoExtraFound = True
		End If

		Return New cls_Extras_Result(sNextExtra, ExtraNum, NoExtraFound, ExtraType, bWrappedToFirst)
	End Function

	''' <summary>
	''' Find all Extras according to FileName
	''' </summary>
	''' <param name="Platform_Short"></param>
	''' <param name="id_Moby_Platforms"></param>
	''' <param name="Game"></param>
	''' <param name="FileName"></param>
	''' <returns>ArrayList of cls_Extras_Result</returns>
	Public Shared Function FindAllExtras(ByVal Platform_Short As String, ByVal id_Moby_Platforms As Integer, ByVal Game As String, ByVal FileName As String) As ArrayList
		Dim Result As New ArrayList

		Dim bDone As Boolean = False

		Dim ExtraType As Object = DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Name FROM tbl_Emu_Extras WHERE Sort = (SELECT MIN(Sort) from tbl_Emu_Extras LIMIT 1)")

		Dim ExtraNum As Integer = 0

		Dim bFirst As Boolean = True

		While Not bDone
			Dim res As cls_Extras_Result = FindNextExtra(Platform_Short, id_Moby_Platforms, Game, FileName, ExtraNum, Not bFirst, ExtraType, True)

			If bFirst Then bFirst = False

			If res._NoExtraFound Then
				bDone = True
				Exit While
			Else
				If Not ExtrasListContains(Result, res) Then
					Result.Add(res)
				Else
					bDone = True
					Exit While
				End If
			End If

			ExtraNum = res._ExtraNum
			ExtraType = res._ExtraType
		End While

		Return Result
	End Function

	Public Shared Function ExtrasListContains(ByRef ExtrasList As ArrayList, ByRef Extra As cls_Extras_Result) As Boolean
		For Each item As cls_Extras_Result In ExtrasList
			If item._Path.ToLower = Extra._Path.ToLower Then
				Return True
			End If
		Next

		Return False
	End Function

	Public Shared Function ExtrasListsEqual(ByRef ExtrasList1 As ArrayList, ByRef ExtrasList2 As ArrayList) As Boolean
		If ExtrasList1.Count <> ExtrasList2.Count Then Return False

		For Each item As cls_Extras_Result In ExtrasList1
			If Not ExtrasListContains(ExtrasList2, item) Then Return False
		Next

		Return True
	End Function

	''' <summary>
	''' Get the next free extra filename without extension (for later save)
	''' </summary>
	''' <param name="Platform_Short">The short name for the platform, e.g. snes, gen, win etc.</param>
	''' <param name="ExtraCategory">The extra category</param>
	''' <param name="FileName">The ROM filename without its extension</param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Shared Function FindNextFreeExtraFilename(ByVal Platform_Short As String, ByVal ExtraCategory As String, ByVal FileName As String) As String
		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(cls_Globals.Dir_Extras & "\emulation\" & Platform_Short & "\" & ExtraCategory) Then
			Try
				Alphaleonis.Win32.Filesystem.Directory.CreateDirectory(cls_Globals.Dir_Extras & "\emulation\" & Platform_Short & "\" & ExtraCategory)
			Catch ex As Exception
				Return ""
			End Try
		End If

		Dim ExtraNum As Integer = 0

		Dim bDone As Boolean = False

		While Not bDone
			If Alphaleonis.Win32.Filesystem.File.Exists(FindNextExtraFromSupportedExtensions(cls_Globals.Dir_Extras & "\emulation\" & Platform_Short & "\" & ExtraCategory & "\" & FileName & getExtraSuffix(ExtraNum))) Then 'IIf(ExtraNum = 0, "", " [image" & (ExtraNum + 1) & "]")
				ExtraNum += 1
			Else
				bDone = True
			End If
		End While

		Return FileName & getExtraSuffix(ExtraNum) ' IIf(ExtraNum = 0, "", " [image" & (ExtraNum + 1) & "]")
	End Function

	Public Shared Function GetExtraFilename(ByVal Game As String, ByVal FileName As String) As String
		If Game.ToLower = FileName.ToLower Then
			Return Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(MKNetLib.cls_MKStringSupport.GetCleanFileName(TC.NZ(Game, "")))
		Else
			Return MKNetLib.cls_MKStringSupport.GetCleanFileName(TC.NZ(Game, ""))
		End If
	End Function

	''' <summary>
	''' Ensure the file is present and not 0 size
	''' If it is 0 size, delete it
	''' </summary>
	''' <param name="filepath"></param>
	''' <returns>True, if file exists and is of size != 0</returns>
	Public Shared Function EnsureExtrasFile(filepath)
		If Not Alphaleonis.Win32.Filesystem.File.Exists(filepath) Then
			Return False
		End If

		Dim fi As New Alphaleonis.Win32.Filesystem.FileInfo(filepath)
		If fi.Length = 0 Then
			Try
				Alphaleonis.Win32.Filesystem.File.Delete(filepath)
			Catch ex As Exception
			End Try

			Return False
		End If

		Return True
	End Function
End Class
