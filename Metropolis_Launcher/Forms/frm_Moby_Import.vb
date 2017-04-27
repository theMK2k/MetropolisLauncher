Public Class frm_Moby_Import

#Region "Members"
	Private _id_Moby_Platforms As Integer = 0
	'Private _Moby_Platforms_URLPart As String
	'Private _Moby_Platforms_Name As String

	Private _bbi_Import_Single_Game_Caption As String
	Private _bbi_Import_Single_Game_Group_Caption As String

	Private _tran As SQLite.SQLiteTransaction

	Private _Import_Log As New System.Text.StringBuilder
	Private _Import_Total As Integer
	Private _Import_New As Integer
	Private _Import_Error As Integer

	Private _Import_Groups As Boolean = False

	Private UserAgentHeader As String = "Mozilla/5.0 (Windows NT 6.1; rv:15.0) Gecko/20120716 Firefox/15.0a2"

	Private iTraffic As Long = 0
#End Region

	''' <summary>
	''' Contructor for Import of a whole platform
	''' </summary>
	''' <param name="id_Moby_Platforms"></param>
	''' <param name="Moby_Platforms_URLPart"></param>
	''' <param name="Moby_Platforms_Name"></param>
	''' <remarks></remarks>
	Public Sub New(ByVal id_Moby_Platforms As Integer, ByVal Moby_Platforms_URLPart As String, ByVal Moby_Platforms_Name As String, Optional ByVal ImportGameGroups As Boolean = False)
		Init()

		Me._id_Moby_Platforms = id_Moby_Platforms

		Get_GameList(id_Moby_Platforms, Moby_Platforms_URLPart, Moby_Platforms_Name)

		If ImportGameGroups Then
			Try
				Get_GameGroups()
			Catch ex As Exception
				DevExpress.XtraEditors.XtraMessageBox.Show("EXCEPTION: " & vbCrLf & ex.Message & ex.StackTrace.ToString)
			End Try

			_Import_Groups = True
		End If
	End Sub

	''' <summary>
	''' Constructor for multiple Platforms
	''' </summary>
	''' <remarks></remarks>
	Public Sub New(ByVal dt_Moby_Platforms As DS_MobyDB.tbl_Moby_PlatformsDataTable)
		Init()

		For Each dr As DS_MobyDB.tbl_Moby_PlatformsRow In dt_Moby_Platforms.Rows
			Try
				Get_GameList(dr.id_Moby_Platforms, dr.URLPart, dr.Display_Name)
			Catch ex As Exception
				DevExpress.XtraEditors.XtraMessageBox.Show("EXCEPTION: " & vbCrLf & "id_Moby_Platforms: " & TC.NZ(dr("id_Moby_Platforms"), "<null>") & vbCrLf & "URLPart: " & TC.NZ(dr("URLPart"), "<null>") & vbCrLf & "Platform Name: " & TC.NZ(dr("Display_Name"), "<null>") & vbCrLf & ex.Message & ex.StackTrace.ToString)
			End Try
		Next

		Try
			Get_GameGroups()
		Catch ex As Exception
			DevExpress.XtraEditors.XtraMessageBox.Show("EXCEPTION: " & vbCrLf & ex.Message & ex.StackTrace.ToString)
		End Try

		_Import_Groups = True
	End Sub

	Private Sub Init()
		InitializeComponent()

		_bbi_Import_Single_Game_Caption = bbi_Import_Single_Game.Caption
		_bbi_Import_Single_Game_Group_Caption = bbi_Import_Single_Game_Group.Caption


		barmng.SetPopupContextMenu(grd_Moby_Games, popmnu_Moby_Web_Games)
		barmng.SetPopupContextMenu(grd_Moby_Game_Groups, popmnu_Moby_Web_Game_Groups)
	End Sub

	Public Function Get_GameList(ByVal id_Moby_Platforms As Integer, ByVal Moby_Platforms_URLPart As String, ByVal Moby_Platforms_Name As String) As DataTable
		Dim tbl As DataTable = DS_Moby_Web.Tables("tbl_Moby_Games")
		Dim sURL As String = "http://www.mobygames.com/browse/games/" & Moby_Platforms_URLPart & "/list-games/"

		Dim bContinue As Boolean = True

		Dim prg As MKNetDXLib.cls_MKDXBaseform_Progress_Helper = Nothing

		While bContinue

			Dim bTryAgain As Boolean = True

			Dim sContent As String = ""

			While bTryAgain
				sContent = MKNetLib.cls_MKWebClient.FetchURLToStringSafe(sURL)
				Debug.WriteLine(sURL)
				bTryAgain = False
				If sContent.Length = 0 AndAlso DevExpress.XtraEditors.XtraMessageBox.Show("Try again?", "", MessageBoxButtons.YesNo) <> MsgBoxResult.Yes Then
					bTryAgain = True
				End If
			End While

			iTraffic += sContent.Length

			Dim htmlDoc As New HtmlAgilityPack.HtmlDocument
			htmlDoc.LoadHtml(sContent)

			'/html/body/div/div[2]/table/tbody/tr/td[2]/div/div/div[2]/div/div[3]/div/table/tbody

			Dim coll_tr_Moby_Release As HtmlAgilityPack.HtmlNodeCollection = htmlDoc.DocumentNode.SelectNodes("//table[@class='molist']//tr[@valign='top']")
			If coll_tr_Moby_Release Is Nothing OrElse coll_tr_Moby_Release.Count = 0 Then
				Exit While
			End If

			If prg Is Nothing Then
				Try
					prg = New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(_tran), 400, 60, ProgressBarStyle.Blocks, False, "Fetching game names for " & Moby_Platforms_Name & " from web ({0} of {1})", 0, CInt(MKNetLib.cls_MKRegex.GetMatches(htmlDoc.DocumentNode.SelectSingleNode("//td[@class='mobHeaderItems']").InnerText, "of (\d+)")(0).Groups(1).Value), True)
					prg.Start()
				Catch ex As Exception

				End Try
			End If

			For Each tr_Release As HtmlAgilityPack.HtmlNode In coll_tr_Moby_Release
				Try
					If prg IsNot Nothing Then
						prg.IncreaseCurrentValue()
					End If

					Dim s_Moby_URLPart As String = MKNetLib.cls_MKStringSupport.Clean_Right(htmlDoc.DocumentNode.SelectSingleNode(tr_Release.XPath & "//td[1]/a").Attributes(0).Value, "/").Split("/").Last
					Dim s_Moby_GameName As String = htmlDoc.DocumentNode.SelectSingleNode(tr_Release.XPath & "//td[1]/a").ChildNodes(0).InnerText
					Dim s_Moby_Year As String = htmlDoc.DocumentNode.SelectSingleNode(tr_Release.XPath & "//td[2]/a").ChildNodes(0).InnerText

					s_Moby_GameName = MKNetLib.cls_MKHTMLtoText.ConvertHTMLtoText(s_Moby_GameName)

					Dim dr As DataRow = tbl.NewRow
					dr("Game_Title") = s_Moby_GameName
					dr("Year") = s_Moby_Year
					dr("URLPart") = s_Moby_URLPart
					dr("Platform_URLPart") = Moby_Platforms_URLPart
					dr("id_Moby_Platforms") = id_Moby_Platforms
					dr("PlatformName") = Moby_Platforms_Name
					tbl.Rows.Add(dr)
				Catch ex As Exception
					'Devexpress.XtraEditors.XtraMessageBox.Show("ERROR: Row: " & ex.Message)
				End Try

			Next

			'Find next page
			bContinue = False
			If htmlDoc.DocumentNode.SelectSingleNode("//td[@class='mobHeaderNav']") IsNot Nothing Then
				For Each html_node As HtmlAgilityPack.HtmlNode In htmlDoc.DocumentNode.SelectSingleNode("//td[@class='mobHeaderNav']").ChildNodes
					If html_node.InnerText = "Next" Then
						bContinue = True
						sURL = "http://www.mobygames.com" & html_node.Attributes(0).Value
						Exit For
					End If
				Next
			End If

			If prg IsNot Nothing Then
				If prg.WaitForCancel Then
					prg.Close()
					Return tbl
				End If
			End If
		End While

		If prg IsNot Nothing Then
			prg.Close()
		End If

		Return tbl
	End Function

	Public Function Get_GameGroups() As DataTable
		Dim tbl As DataTable = DS_Moby_Web.Tables("tbl_Moby_Game_Groups")
		Dim sURL As String = "http://www.mobygames.com/browse/game-groups/"

		Dim bContinue As Boolean = True

		Dim prg As MKNetDXLib.cls_MKDXBaseform_Progress_Helper = Nothing

		While bContinue

			Dim bTryAgain As Boolean = True

			Dim sContent As String = ""

			While bTryAgain
				sContent = MKNetLib.cls_MKWebClient.FetchURLToStringSafe(sURL)
				bTryAgain = False
				If sContent.Length = 0 AndAlso DevExpress.XtraEditors.XtraMessageBox.Show("Try again?", "", MessageBoxButtons.YesNo) <> MsgBoxResult.Yes Then
					bTryAgain = True
				End If
			End While

			iTraffic += sContent.Length

			Dim htmlDoc As New HtmlAgilityPack.HtmlDocument
			htmlDoc.LoadHtml(sContent)

			Dim coll_tr_Moby_Game_Group As HtmlAgilityPack.HtmlNodeCollection = htmlDoc.DocumentNode.SelectNodes("//table[@class='molist']//tr[@valign='top']")
			If coll_tr_Moby_Game_Group Is Nothing OrElse coll_tr_Moby_Game_Group.Count = 0 Then
				Exit While
			End If

			If prg Is Nothing Then
				Try
					prg = New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(_tran), 400, 60, ProgressBarStyle.Blocks, False, "Fetching game groups list ({0} of {1})", 0, CInt(MKNetLib.cls_MKRegex.GetMatches(htmlDoc.DocumentNode.SelectSingleNode("//td[@class='mobHeaderItems']").InnerText, "of (\d+)")(0).Groups(1).Value), True)
					prg.Start()
				Catch ex As Exception

				End Try
			End If

			For Each tr_Game_Group As HtmlAgilityPack.HtmlNode In coll_tr_Moby_Game_Group
				Try
					If prg IsNot Nothing Then
						prg.IncreaseCurrentValue()
					End If

					Dim s_Moby_URLPart As String = MKNetLib.cls_MKStringSupport.Clean_Right(htmlDoc.DocumentNode.SelectSingleNode(tr_Game_Group.XPath & "//td[1]/a").Attributes(0).Value, "/").Split("/").Last
					Dim s_Moby_Group_Name As String = htmlDoc.DocumentNode.SelectSingleNode(tr_Game_Group.XPath & "//td[1]/a").ChildNodes(0).InnerText
					Dim s_Moby_Description As String = htmlDoc.DocumentNode.SelectSingleNode(tr_Game_Group.XPath & "//td[2]").InnerText
					Dim s_GameCount As String = htmlDoc.DocumentNode.SelectSingleNode(tr_Game_Group.XPath & "//td[3]").InnerText

					s_Moby_Group_Name = MKNetLib.cls_MKHTMLtoText.ConvertHTMLtoText(s_Moby_Group_Name)
					s_Moby_Description = MKNetLib.cls_MKHTMLtoText.ConvertHTMLtoText(s_Moby_Description)
					s_GameCount = MKNetLib.cls_MKHTMLtoText.ConvertHTMLtoText(s_GameCount)

					Dim iGameCount As Integer = 0

					If IsNumeric(s_GameCount) Then iGameCount = CInt(s_GameCount)


					Dim dr As DataRow = tbl.NewRow
					dr("Name") = s_Moby_Group_Name 'cls_Globals.ISO_8859_1_Replace.ToASCII_Int(s_Moby_Group_Name)
					dr("Description") = s_Moby_Description
					dr("URLPart") = s_Moby_URLPart
					dr("NumberOfGames") = iGameCount
					tbl.Rows.Add(dr)
				Catch ex As Exception
					'Devexpress.XtraEditors.XtraMessageBox.Show("ERROR: Row: " & ex.Message)
				End Try
			Next

			'Find next page
			bContinue = False
			If htmlDoc.DocumentNode.SelectSingleNode("//td[@class='mobHeaderNav']") IsNot Nothing Then
				For Each html_node As HtmlAgilityPack.HtmlNode In htmlDoc.DocumentNode.SelectSingleNode("//td[@class='mobHeaderNav']").ChildNodes
					If html_node.InnerText = "Next" Then
						bContinue = True
						sURL = "http://www.mobygames.com" & html_node.Attributes(0).Value
						Exit For
					End If
				Next
			End If

			If prg.WaitForCancel Then
				prg.Close()
				Return tbl
			End If
		End While

		If prg IsNot Nothing Then
			prg.Close()
		End If

		Return tbl
	End Function

	''' <summary>
	''' Add text to the log
	''' </summary>
	''' <param name="Text"></param>
	''' <param name="NewLine"></param>
	''' <remarks></remarks>
	Private Sub AddLog(ByVal Text As String, Optional ByVal NewLine As Boolean = True)
		If NewLine Then
			Me._Import_Log.AppendLine(Text)
		Else
			Me._Import_Log.Append(Text)
		End If
	End Sub

	''' <summary>
	''' Import ALL the games
	''' </summary>
	''' <remarks></remarks>
	Private Sub Import_Games()
		Me._Import_Log = New System.Text.StringBuilder

		_tran = cls_Globals.Conn.BeginTransaction

		Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(_tran), 400, 60, ProgressBarStyle.Blocks, False, "Importing game {0} of {1}: {2}", 0, DS_Moby_Web.Tables("tbl_Moby_Games").Rows.Count, False)
		prg.Start()

		_Import_Total = DS_Moby_Web.Tables("tbl_Moby_Games").Rows.Count
		_Import_New = 0
		_Import_Error = False

		For Each row_Game As DataRow In DS_Moby_Web.Tables("tbl_Moby_Games").Rows
			prg.IncreaseCurrentValue()
			prg.DetailText = row_Game("Game_Title")

			Try
				Import_Game(TC.NZ(row_Game("Game_Title"), ""), TC.NZ(row_Game("URLPart"), ""), TC.NZ(row_Game("Year"), ""), TC.NZ(row_Game("Platform_URLPart"), ""), row_Game("id_Moby_Platforms"))
			Catch ex As Exception
				AddLog("  EXCEPTION: " & ex.Message & ex.StackTrace)
			End Try
		Next

		Update_Moby_Releases_Platform_Owner(_tran)

		_tran.Commit()

		prg.Close()
	End Sub

	Private Sub Import_Game_Groups()
		Me._Import_Log = New System.Text.StringBuilder

		_tran = cls_Globals.Conn.BeginTransaction

		Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(_tran), 400, 60, ProgressBarStyle.Blocks, False, "Importing game group {0} of {1}: {2}", 0, DS_Moby_Web.Tables("tbl_Moby_Game_Groups").Rows.Count, False)
		prg.Start()

		_Import_Total = DS_Moby_Web.Tables("tbl_Moby_Game_Groups").Rows.Count
		_Import_New = 0
		_Import_Error = False

		For Each row_Game_Group As DataRow In DS_Moby_Web.Tables("tbl_Moby_Game_Groups").Rows
			prg.IncreaseCurrentValue()
			prg.DetailText = row_Game_Group("Name")

			Try
				Import_Game_Group(TC.NZ(row_Game_Group("Name"), ""), TC.NZ(row_Game_Group("URLPart"), ""), TC.NZ(row_Game_Group("NumberOfGames"), 0))
			Catch ex As Exception
				AddLog("  EXCEPTION: " & ex.Message & ex.StackTrace)
			End Try
		Next

		_tran.Commit()

		prg.Close()
	End Sub

	Private Function SplitPrefix(ByVal Name As String, ByVal Prefix As String) As cls_3ObjVec
		Return New cls_3ObjVec(Name, Name.Substring(Prefix.Length).Trim, Name.Substring(0, Prefix.Length).Trim)
	End Function

	''' <summary>
	''' Split a game name by it's Prefix if any can be found
	''' </summary>
	''' <param name="Name"></param>
	''' <returns>3-Object Vector, x = old name, y = new name, z = prefix</returns>
	''' <remarks></remarks>
	Private Function SplitPrefix(ByVal Name As String) As cls_3ObjVec
		If Name.ToLower.StartsWith("the ") Then Return SplitPrefix(Name, "the ")
		If Name.ToLower.StartsWith("a ") Then Return SplitPrefix(Name, "a ")
		If Name.ToLower.StartsWith("disney's ") Then Return SplitPrefix(Name, "disney's ")
		If Name.ToLower.StartsWith("disney ") Then Return SplitPrefix(Name, "disney ")

		Return New cls_3ObjVec(Name.Trim, Name.Trim, "")
	End Function

	Private Sub Import_Game(ByVal Game_Title As String, ByVal URLPart As String, ByVal Year As String, ByVal Platform_URLPart As String, ByVal id_Moby_Platforms As Integer)
		AddLog("Importing " & Game_Title)

		If URLPart.Length = 0 Then
			AddLog("  ERROR: No URL part found!")
			Me._Import_Error = Me._Import_Error + 1
			Return
		End If

		'All the attributes of the Release
		Dim Prefix As Object = Nothing
		Dim id_Moby_Games As Object = Nothing
		Dim id_Moby_Releases As Object = Nothing
		Dim Publisher_id_Moby_Companies As Object = Nothing
		Dim Developer_id_Moby_Companies As Object = Nothing
		Dim id_Genre_Main As Object = Nothing
		Dim Rank As Object = Nothing
		Dim Score As Object = Nothing
		Dim TechInfo As Object = Nothing

		Dim sURL As String = "http://www.mobygames.com/game/" & Platform_URLPart & "/" & URLPart
		AddLog("  Fetching Main Summary HTML from " & sURL)

		Dim sContent_Summary As String = ""
		Dim ErrorText As String = ""
		sContent_Summary = MKNetLib.cls_MKWebClient.FetchURLToStringSafe(sURL, UserAgentHeader, ErrorText)
		iTraffic += sContent_Summary.Length
		If ErrorText <> "" Then AddLog("    " & ErrorText)

		Dim htmlDoc_Summary As New HtmlAgilityPack.HtmlDocument
		htmlDoc_Summary.LoadHtml(sContent_Summary)

		Dim splitName As cls_3ObjVec = SplitPrefix(Game_Title)

		If splitName._x <> splitName._y Then
			Game_Title = splitName._y
			Prefix = splitName._z
		End If

		'Add Game to DB
		Dim Game_Description As Object = MKNetLib.cls_MKRegex.GetMatches(htmlDoc_Summary.DocumentNode.SelectSingleNode("//div[@class='col-md-8 col-lg-8']").InnerHtml, "Description</h2>(.*?)<div", System.Text.RegularExpressions.RegexOptions.Singleline Or System.Text.RegularExpressions.RegexOptions.CultureInvariant Or System.Text.RegularExpressions.RegexOptions.Compiled)(0).Groups(1).Value
		Game_Description = MKNetLib.cls_MKHTMLtoText.ConvertHTMLtoText(Game_Description)

		AddLog("  Adding Game to tbl_Moby_Games")
		id_Moby_Games = DS_MobyDB.Upsert_Moby_Games(_tran, URLPart, Game_Title, Game_Description, Prefix)

		'Developer, Publisher (for Release)
		Dim node_ReleaseInfo As HtmlAgilityPack.HtmlNode = htmlDoc_Summary.DocumentNode.SelectSingleNode("//div[@id='coreGameRelease']")
		If node_ReleaseInfo Is Nothing Then
			AddLog("  ERROR: Parser can't find ReleaseInfo!")
			_Import_Error = True
		Else
			For Each subnode_ReleaseInfo As HtmlAgilityPack.HtmlNode In node_ReleaseInfo.ChildNodes
				If subnode_ReleaseInfo.PreviousSibling IsNot Nothing AndAlso (subnode_ReleaseInfo.PreviousSibling.InnerText = "Published by" OrElse subnode_ReleaseInfo.PreviousSibling.InnerText = "Developed by") Then
					Dim Company_URLPart As String = MKNetLib.cls_MKStringSupport.Clean_Right(subnode_ReleaseInfo.SelectSingleNode(subnode_ReleaseInfo.XPath & "//a").Attributes(0).Value, "/").Split("/").Last
					Dim Company_Name As String = MKNetLib.cls_MKISO_8859_1_Replace.ToASCII(subnode_ReleaseInfo.SelectSingleNode(subnode_ReleaseInfo.XPath & "//a").InnerText)

					Dim id_Moby_Companies As Integer = DS_MobyDB.Upsert_Moby_Companies(_tran, Company_URLPart, Company_Name)

					Select Case subnode_ReleaseInfo.PreviousSibling.InnerText
						Case "Published by"
							Publisher_id_Moby_Companies = id_Moby_Companies
						Case "Developed by"
							Developer_id_Moby_Companies = id_Moby_Companies
						Case ""
					End Select
				End If
			Next
		End If

		'Genres
		Dim node_Genres As HtmlAgilityPack.HtmlNode = htmlDoc_Summary.DocumentNode.SelectSingleNode("//div[@id='coreGameGenre']")
		If node_Genres Is Nothing Then
			AddLog("  ERROR: Parser can't find GenreInfo!")
			_Import_Error = True
		Else
			DS_MobyDB.Delete_Moby_Games_Genres(_tran, id_Moby_Games)
			For Each subnode_Genres As HtmlAgilityPack.HtmlNode In node_Genres.ChildNodes
				For Each subsubnode_Genres As HtmlAgilityPack.HtmlNode In subnode_Genres.ChildNodes
					If subsubnode_Genres.SelectNodes(subsubnode_Genres.XPath & "//a") IsNot Nothing Then
						For Each subsubnode_link_Genres As HtmlAgilityPack.HtmlNode In subsubnode_Genres.SelectNodes(subsubnode_Genres.XPath & "//a")
							Dim Genre_URLPart As String = MKNetLib.cls_MKStringSupport.Clean_Right(subsubnode_link_Genres.Attributes(0).Value, "/").Split("/").Last
							Dim id_Moby_Genres As Integer = Upsert_Genre(_tran, Genre_URLPart)
							If id_Moby_Genres <> -1 Then
								If id_Moby_Genres = 0 Then
									AddLog("  ERROR: Can't find Genre in DB with URLPart = " & TC.getSQLFormat(Genre_URLPart))
									_Import_Error = True
								Else
									DS_MobyDB.Upsert_Moby_Games_Genres(_tran, id_Moby_Games, id_Moby_Genres)
								End If
							End If
						Next
					End If
				Next
			Next
		End If

		'Rank and Score
		Dim node_Rank As HtmlAgilityPack.HtmlNode = htmlDoc_Summary.DocumentNode.SelectSingleNode("//div[@id='coreGameRank']")
		If node_Rank Is Nothing Then
			AddLog("  ERROR: Parser can't find RankInfo!")
			_Import_Error = True
		Else
			Dim sRank As String = node_Rank.SelectSingleNode(node_Rank.XPath & "/div/div/div/div/div/div").InnerText
			Dim sScore As String = node_Rank.SelectSingleNode(node_Rank.XPath & "/div/div/div/div[3]/div/div").InnerText

			If IsNumeric(sRank) Then
				Rank = CInt(sRank)
			End If

			If IsNumeric(sScore) Then
				Score = Double.Parse(sScore, Globalization.CultureInfo.InvariantCulture)
			End If
		End If

		'TechInfo Note (field on tbl_Moby_Releases)
		'Test with http://www.mobygames.com/game/genesis/batman-forever_/techinfo
		Dim sContent_TechInfo As String = MKNetLib.cls_MKWebClient.FetchURLToStringSafe(sURL & "/techinfo", UserAgentHeader, ErrorText)
		iTraffic += sContent_TechInfo.Length
		If ErrorText <> "" Then AddLog("    " & ErrorText)

		Dim htmlDoc_TechInfo As New HtmlAgilityPack.HtmlDocument
		htmlDoc_TechInfo.LoadHtml(sContent_TechInfo)
		For Each node_TechInfo As HtmlAgilityPack.HtmlNode In htmlDoc_TechInfo.DocumentNode.SelectNodes("//table[@class='techInfo']//tr")
			For Each subnode_TechInfo As HtmlAgilityPack.HtmlNode In node_TechInfo.ChildNodes
				If subnode_TechInfo.InnerText = "Notes" Then
					If subnode_TechInfo.NextSibling IsNot Nothing AndAlso subnode_TechInfo.NextSibling.NextSibling IsNot Nothing Then
						TechInfo = MKNetLib.cls_MKHTMLtoText.ConvertHTMLtoText(subnode_TechInfo.NextSibling.NextSibling.InnerText)
					End If
				End If
			Next
		Next

		'Add Moby Release
		id_Moby_Releases = DS_MobyDB.Upsert_Moby_Releases(_tran, id_Moby_Games, id_Moby_Platforms, Rank, Score, Publisher_id_Moby_Companies, Developer_id_Moby_Companies, sURL, TechInfo, Year)

		'Up next: id_Moby_Releases dependent Data

		'Get all the TechInfo (Attributes)
		For Each node_TechInfo As HtmlAgilityPack.HtmlNode In htmlDoc_TechInfo.DocumentNode.SelectNodes("//table[@class='techInfo']")
			If node_TechInfo.SelectNodes(node_TechInfo.XPath & "//a") IsNot Nothing Then
				For Each subnode_TechInfo As HtmlAgilityPack.HtmlNode In node_TechInfo.SelectNodes(node_TechInfo.XPath & "//a")
					If MKNetLib.cls_MKRegex.IsMatch(subnode_TechInfo.Attributes(0).Value, "attributeId,(\d*)") Then
						Dim id_Moby_Attributes As Object = MKNetLib.cls_MKRegex.GetMatches(subnode_TechInfo.Attributes(0).Value, "attributeId,(\d*)")(0).Groups(1).Value
						If IsNumeric(id_Moby_Attributes) Then
							If TC.NZ(DS_MobyDB.Select_id_Moby_Attributes(_tran, id_Moby_Attributes), 0) = 0 Then
								Dim sContent_Attributes As String = MKNetLib.cls_MKWebClient.FetchURLToStringSafe("http://www.mobygames.com/attribute/sheet/attributeId," & id_Moby_Attributes, UserAgentHeader, ErrorText)
								iTraffic += sContent_Attributes.Length
								If ErrorText <> "" Then AddLog("    " & ErrorText)

								Dim htmlDoc_Attributes As New HtmlAgilityPack.HtmlDocument
								htmlDoc_Attributes.LoadHtml(sContent_Attributes)

								Dim sAttributeCategory As String = htmlDoc_Attributes.DocumentNode.SelectNodes("//em")(1).InnerText
								Dim sAttributeName As String = htmlDoc_Attributes.DocumentNode.SelectNodes("//em")(0).InnerText
								Dim sAttributeDescription As String = htmlDoc_Attributes.DocumentNode.SelectSingleNode("//p[2]").PreviousSibling.InnerText

								sAttributeName = MKNetLib.cls_MKHTMLtoText.ConvertHTMLtoText(sAttributeName)
								sAttributeDescription = MKNetLib.cls_MKHTMLtoText.ConvertHTMLtoText(sAttributeDescription)

								Dim id_Moby_Attributes_Categories As Integer = DS_MobyDB.Upsert_Moby_Attributes_Categories(_tran, sAttributeCategory)
								DS_MobyDB.Upsert_Moby_Attributes(_tran, id_Moby_Attributes, id_Moby_Attributes_Categories, sAttributeName, sAttributeDescription)
							End If

							DS_MobyDB.Upsert_Moby_Releases_Attributes(_tran, id_Moby_Releases, id_Moby_Attributes)
						End If
					End If
				Next
			End If
		Next

		'Alternate Titles (if they exist)
		'Test with http://www.mobygames.com/game/ultimate-doom
		If MKNetLib.cls_MKRegex.IsMatch(htmlDoc_Summary.DocumentNode.SelectSingleNode("//div[@class='col-md-8 col-lg-8']").InnerHtml, "Alternate Titles</h2><ul>(.*?)</ul>", System.Text.RegularExpressions.RegexOptions.Singleline Or System.Text.RegularExpressions.RegexOptions.CultureInvariant Or System.Text.RegularExpressions.RegexOptions.Compiled) Then
			Dim Alternate_Titles As String = MKNetLib.cls_MKRegex.GetMatches(htmlDoc_Summary.DocumentNode.SelectSingleNode("//div[@class='col-md-8 col-lg-8']").InnerHtml, "Alternate Titles</h2><ul>(.*?)</ul>", System.Text.RegularExpressions.RegexOptions.Singleline Or System.Text.RegularExpressions.RegexOptions.CultureInvariant Or System.Text.RegularExpressions.RegexOptions.Compiled)(0).Groups(1).Value

			If MKNetLib.cls_MKRegex.IsMatch(Alternate_Titles, "<li>""(.+?)"".+?<em>(.+?)</em>", System.Text.RegularExpressions.RegexOptions.Singleline Or System.Text.RegularExpressions.RegexOptions.CultureInvariant Or System.Text.RegularExpressions.RegexOptions.Compiled) Then
				For Each match_Title As System.Text.RegularExpressions.Match In MKNetLib.cls_MKRegex.GetMatches(Alternate_Titles, "<li>""(.+?)"".+?<em>(.+?)</em>", System.Text.RegularExpressions.RegexOptions.Singleline Or System.Text.RegularExpressions.RegexOptions.CultureInvariant Or System.Text.RegularExpressions.RegexOptions.Compiled)
					Dim sAlternateTitle As String = match_Title.Groups(1).Value
					Dim sDescription As String = match_Title.Groups(2).Value
					DS_MobyDB.Upsert_Moby_Games_Alternate_Titles(_tran, id_Moby_Games, sAlternateTitle, sDescription)
				Next
			End If
		End If

		'Rating Systems	"rating-systems"
		'Test with http://www.mobygames.com/game/dos/ultimate-doom/rating-systems
		Dim sContent_RatingSystems As String = MKNetLib.cls_MKWebClient.FetchURLToStringSafe(sURL & "/rating-systems", UserAgentHeader, ErrorText)
		iTraffic += sContent_RatingSystems.Length
		If ErrorText <> "" Then AddLog("    " & ErrorText)

		Dim htmlDoc_RatingSystems As New HtmlAgilityPack.HtmlDocument
		htmlDoc_RatingSystems.LoadHtml(sContent_RatingSystems)

		For Each node_RatingSystem As HtmlAgilityPack.HtmlNode In htmlDoc_RatingSystems.DocumentNode.SelectNodes("//table[@summary='Rating Categories and Descriptors']")
			If node_RatingSystem.SelectNodes(node_RatingSystem.XPath & "//a") IsNot Nothing Then
				For Each subnode_RatingSystem As HtmlAgilityPack.HtmlNode In node_RatingSystem.SelectNodes(node_RatingSystem.XPath & "//a")
					If MKNetLib.cls_MKRegex.IsMatch(subnode_RatingSystem.Attributes(0).Value, "attributeId,(\d*)") Then
						Dim id_Moby_Attributes As Object = MKNetLib.cls_MKRegex.GetMatches(subnode_RatingSystem.Attributes(0).Value, "attributeId,(\d*)")(0).Groups(1).Value
						If IsNumeric(id_Moby_Attributes) Then
							If TC.NZ(DS_MobyDB.Select_id_Moby_Attributes(_tran, id_Moby_Attributes), 0) = 0 Then
								Dim sContent_Attributes As String = MKNetLib.cls_MKWebClient.FetchURLToStringSafe("http://www.mobygames.com/attribute/sheet/attributeId," & id_Moby_Attributes, UserAgentHeader, ErrorText)
								iTraffic += sContent_Attributes.Length
								If ErrorText <> "" Then AddLog("    " & ErrorText)

								Dim htmlDoc_Attributes As New HtmlAgilityPack.HtmlDocument
								htmlDoc_Attributes.LoadHtml(sContent_Attributes)

								Dim sAttributeCategory As String = htmlDoc_Attributes.DocumentNode.SelectNodes("//em")(1).InnerText
								Dim sAttributeName As String = htmlDoc_Attributes.DocumentNode.SelectNodes("//em")(0).InnerText
								Dim sAttributeDescription As String = htmlDoc_Attributes.DocumentNode.SelectSingleNode("//p[2]").PreviousSibling.InnerText

								sAttributeName = MKNetLib.cls_MKHTMLtoText.ConvertHTMLtoText(sAttributeName)
								sAttributeDescription = MKNetLib.cls_MKHTMLtoText.ConvertHTMLtoText(sAttributeDescription)

								Dim id_Moby_Attributes_Categories As Integer = DS_MobyDB.Upsert_Moby_Attributes_Categories(_tran, sAttributeCategory, True)
								DS_MobyDB.Upsert_Moby_Attributes(_tran, id_Moby_Attributes, id_Moby_Attributes_Categories, sAttributeName, sAttributeDescription)
							End If

							DS_MobyDB.Upsert_Moby_Releases_Attributes(_tran, id_Moby_Releases, id_Moby_Attributes)
						End If
					End If
				Next
			End If
		Next

		'Credits (Staff)
		Dim sContent_Credits As String = MKNetLib.cls_MKWebClient.FetchURLToStringSafe(sURL & "/credits", UserAgentHeader, ErrorText)
		iTraffic += sContent_Credits.Length
		If ErrorText <> "" Then AddLog("    " & ErrorText)

		Dim htmlDoc_Credits As New HtmlAgilityPack.HtmlDocument
		htmlDoc_Credits.LoadHtml(sContent_Credits)

		Dim iSort As Integer = 0

		If Not sContent_Credits.Contains("There are no credits for ") Then
			For Each node_Credits As HtmlAgilityPack.HtmlNode In htmlDoc_Credits.DocumentNode.SelectNodes("//table[@summary='List of Credits']")
				If node_Credits.SelectNodes(node_Credits.XPath & "//tr[@class='crln']") IsNot Nothing Then
					For Each subnode_Credits As HtmlAgilityPack.HtmlNode In node_Credits.SelectNodes(node_Credits.XPath & "//tr[@class='crln']")
						Dim sCreditPosition As String = MKNetLib.cls_MKISO_8859_1_Replace.ToASCII(subnode_Credits.SelectNodes(subnode_Credits.XPath & "//td")(0).InnerText)

						If subnode_Credits.SelectNodes(subnode_Credits.XPath & "//a") IsNot Nothing Then
							For Each subsubnode_Credits As HtmlAgilityPack.HtmlNode In subnode_Credits.SelectNodes(subnode_Credits.XPath & "//a")
								If MKNetLib.cls_MKRegex.IsMatch(subsubnode_Credits.Attributes(0).Value, "developerId,(\d*)") Then
									Dim id_Moby_Staff As Object = MKNetLib.cls_MKRegex.GetMatches(subsubnode_Credits.Attributes(0).Value, "developerId,(\d*)")(0).Groups(1).Value
									If IsNumeric(id_Moby_Staff) Then
										If TC.NZ(DS_MobyDB.Select_id_Moby_Staff(_tran, id_Moby_Staff), 0) = 0 Then
											Dim sContent_Staff As String = MKNetLib.cls_MKWebClient.FetchURLToStringSafe("http://www.mobygames.com/developer/sheet/bio/developerId," & id_Moby_Staff, UserAgentHeader, ErrorText)
											iTraffic += sContent_Staff.Length
											If ErrorText <> "" Then AddLog("    " & ErrorText)

											Dim htmlDoc_Developer As New HtmlAgilityPack.HtmlDocument
											htmlDoc_Developer.LoadHtml(sContent_Staff)

											Dim sDeveloperName As String = MKNetLib.cls_MKISO_8859_1_Replace.ToASCII(subsubnode_Credits.InnerText).Trim
											Dim sDeveloperBio_Html As String = htmlDoc_Developer.DocumentNode.SelectSingleNode("//div[@class='col-md-8 col-lg-8']").InnerHtml

											Dim sDeveloperBio As String = ""
											If MKNetLib.cls_MKRegex.IsMatch(sDeveloperBio_Html, "<\/h2>(.*?)<p>", 520 Or System.Text.RegularExpressions.RegexOptions.Singleline) Then
												sDeveloperBio = MKNetLib.cls_MKRegex.GetMatches(sDeveloperBio_Html, "<\/h2>(.*?)<p>", 520 Or System.Text.RegularExpressions.RegexOptions.Singleline)(0).Groups(1).Value
												sDeveloperBio = MKNetLib.cls_MKHTMLtoText.ConvertHTMLtoText(sDeveloperBio)
											Else
												sDeveloperBio = "There is no biography on file for " & sDeveloperName
											End If

											DS_MobyDB.Upsert_Moby_Staff(_tran, id_Moby_Staff, sDeveloperName, sDeveloperBio)
										End If

										iSort += 1
										DS_MobyDB.Upsert_Moby_Releases_Staff(_tran, id_Moby_Releases, id_Moby_Staff, sCreditPosition, iSort)
									End If
								End If
							Next
						End If

					Next
				End If
			Next
		End If

	End Sub

	Private _dict_Genre_URLParts As New Dictionary(Of String, Integer)

	''' <summary>
	''' Fetch Genre Info from MobyGames and Insert or Update to moby.tbl_Moby_Genres
	''' </summary>
	''' <param name="URLPart"></param>
	''' <returns>0 on Error, -1 on expected Error, positive Integer as id_Moby_Genres</returns>
	Private Function Upsert_Genre(ByRef tran As SQLite.SQLiteTransaction, ByVal URLPart As String) As Integer
		If URLPart.Contains("attributeId") Then
			Return -1
		End If

		If _dict_Genre_URLParts.ContainsKey(URLPart) Then
			Return _dict_Genre_URLParts(URLPart)
		End If

		Dim sURL As String = "http://www.mobygames.com/genre/sheet/" & URLPart & "/"
		AddLog("  Fetching Genre from " & sURL)

		Dim sContent_Genre As String = ""
		Dim ErrorText As String = ""
		sContent_Genre = MKNetLib.cls_MKWebClient.FetchURLToStringSafe(sURL, UserAgentHeader, ErrorText)
		iTraffic += sContent_Genre.Length

		If ErrorText <> "" Then
			AddLog("    " & ErrorText)
			Return 0
		End If

		Dim htmlDoc_Genre As New HtmlAgilityPack.HtmlDocument
		htmlDoc_Genre.LoadHtml(sContent_Genre)

		Dim Genre_Title = MKNetLib.cls_MKRegex.GetMatches(htmlDoc_Genre.DocumentNode.SelectSingleNode("//div[@class='col-md-8 col-lg-8']").InnerHtml, "<h1.*?>(.*?)<", System.Text.RegularExpressions.RegexOptions.Singleline Or System.Text.RegularExpressions.RegexOptions.CultureInvariant Or System.Text.RegularExpressions.RegexOptions.Compiled)(0).Groups(1).Value.Trim
		Dim Genre_Description = MKNetLib.cls_MKRegex.GetMatches(htmlDoc_Genre.DocumentNode.SelectSingleNode("//div[@class='col-md-8 col-lg-8']").InnerHtml, "Description</h2>(.*?)<div", System.Text.RegularExpressions.RegexOptions.Singleline Or System.Text.RegularExpressions.RegexOptions.CultureInvariant Or System.Text.RegularExpressions.RegexOptions.Compiled)(0).Groups(1).Value.Trim
		'Dim Title = htmlDoc_Genre.DocumentNode.SelectSingleNode("//title").InnerHtml

		Genre_Description = MKNetLib.cls_MKHTMLtoText.ConvertHTMLtoText(Genre_Description)

		Dim id_Moby_Genres = Me.DS_MobyDB.Upsert_Moby_Genres(tran, URLPart, Genre_Title, Genre_Description)

		If TC.NZ(id_Moby_Genres, 0) > 0 Then
			Me._dict_Genre_URLParts(URLPart) = id_Moby_Genres

			Return id_Moby_Genres
		Else
			AddLog("    ERROR while upserting to moby.tbl_Moby_Genres")
			Return 0
		End If
	End Function

	Private Sub Import_Game_Group(ByVal Name As String, ByVal URLPart As String, ByVal NumberOfGames As Integer)
		AddLog("Importing Group " & Name & " (" & NumberOfGames & " Games)")

		If URLPart.Length = 0 Then
			AddLog("  ERROR: No URL part found!")
			Me._Import_Error = Me._Import_Error + 1
			Return
		End If

		'All the attributes of the Game Group
		Dim Description As Object = Nothing
		Dim id_Moby_Game_Groups As Integer = 0

		Dim sURL As String = "http://www.mobygames.com/game-group/" & URLPart

		AddLog("  Fetching First HTML from " & sURL)

		Dim bContinue As Boolean = True

		While bContinue

			Dim bTryAgain As Boolean = True

			Dim sContent As String = ""

			'Fetch Content
			While bTryAgain
				sContent = MKNetLib.cls_MKWebClient.FetchURLToString(sURL)
				bTryAgain = False
				If sContent.Length = 0 AndAlso DevExpress.XtraEditors.XtraMessageBox.Show("Try again?", "", MessageBoxButtons.YesNo) <> MsgBoxResult.Yes Then
					bTryAgain = True
				End If
			End While


			Dim htmlDoc As New HtmlAgilityPack.HtmlDocument
			htmlDoc.LoadHtml(sContent)

			'/html/body/div/div[2]/table/tbody/tr/td[2]/div/div/div[2]/div/div[3]/div/table/tbody

			If Description Is Nothing Then
				Description = MKNetLib.cls_MKRegex.GetMatches(htmlDoc.DocumentNode.SelectSingleNode("//div[@class='col-md-12 col-lg-12']").InnerHtml, "Description</h2>(.*?)<div", System.Text.RegularExpressions.RegexOptions.Singleline Or System.Text.RegularExpressions.RegexOptions.CultureInvariant Or System.Text.RegularExpressions.RegexOptions.Compiled)(0).Groups(1).Value
				'Description = MKNetLib.cls_MKISO_8859_1_Replace.ToASCII(Description)
				'Description = MKNetLib.cls_MKRegex.Replace(Description, "<a href.*?>", "")
				'Description = MKNetLib.cls_MKRegex.Replace(Description, "</a>", "")
				Description = MKNetLib.cls_MKHTMLtoText.ConvertHTMLtoText(Description)
				id_Moby_Game_Groups = DS_MobyDB.Upsert_Moby_Game_Groups(_tran, URLPart, Name, Description)
			End If

			Dim coll_tr_Moby_Release As HtmlAgilityPack.HtmlNodeCollection = htmlDoc.DocumentNode.SelectNodes("//table[@class='molist']//tr[@valign='top']")
			If coll_tr_Moby_Release Is Nothing OrElse coll_tr_Moby_Release.Count = 0 Then
				Exit While
			End If

			For Each tr_Release As HtmlAgilityPack.HtmlNode In coll_tr_Moby_Release
				Try
					Dim s_Moby_URLPart As String = MKNetLib.cls_MKStringSupport.Clean_Right(htmlDoc.DocumentNode.SelectSingleNode(tr_Release.XPath & "//td[1]/a").Attributes(0).Value, "/").Split("/").Last

					Dim coll_tr_Moby_Platforms As HtmlAgilityPack.HtmlNodeCollection = htmlDoc.DocumentNode.SelectNodes(tr_Release.XPath & "//td[3]/a")

					If coll_tr_Moby_Platforms Is Nothing OrElse coll_tr_Moby_Platforms.Count = 0 Then
						Continue For
					End If

					For Each tr_Platform As HtmlAgilityPack.HtmlNode In coll_tr_Moby_Platforms
						Dim Platform_URLPart As String = MKNetLib.cls_MKStringSupport.Clean_Right(tr_Platform.Attributes(0).Value, "/").Split("/").Last

						Dim id_Moby_Game_Groups_Moby_Releases As Integer = DS_MobyDB.Upsert_Moby_Game_Groups_Moby_Releases(_tran, id_Moby_Game_Groups, s_Moby_URLPart, Platform_URLPart)
					Next

				Catch ex As Exception
					DevExpress.XtraEditors.XtraMessageBox.Show("ERROR: Row: " & ex.Message)
				End Try

			Next

			'Find next page
			bContinue = False
			If htmlDoc.DocumentNode.SelectSingleNode("//td[@class='mobHeaderNav']") IsNot Nothing Then
				For Each html_node As HtmlAgilityPack.HtmlNode In htmlDoc.DocumentNode.SelectSingleNode("//td[@class='mobHeaderNav']").ChildNodes
					If html_node.InnerText = "Next" Then
						bContinue = True
						sURL = "http://www.mobygames.com" & html_node.Attributes(0).Value
						Exit For
					End If
				Next
			End If
		End While
	End Sub

#Region "Button and Menu Events"
	Private Sub btn_Run_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Run.Click
		Dim dt_Start As DateTime = DateTime.Now

		Import_Games()

		Dim noCatMessage As String = ""
		Dim iNumNoCatGenres As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT COUNT(1) FROM moby.tbl_Moby_Genres WHERE No_Category = 1"), 0)
		If iNumNoCatGenres > 0 Then
			noCatMessage = ControlChars.CrLf & ControlChars.CrLf & "ATTENTiON: A total of " & iNumNoCatGenres & " genres are now under No_Category!"
		End If

		Dim GameImportLog As String = _Import_Log.ToString

		Dim GameGroupImportLog As String = ""
		If Me._Import_Groups Then
			Import_Game_Groups() 'Only import Game Groups if all Platforms get imported
			GameGroupImportLog = _Import_Log.ToString
		End If

		'### Calculated fields
		'Platform_Exclusive in tbl_Moby_Games
		DataAccess.FireProcedure(cls_Globals.Conn, 0, "UPDATE moby.tbl_Moby_Games SET Platform_Exclusive = CASE WHEN (SELECT COUNT(1) FROM tbl_Moby_Releases REL WHERE REL.id_Moby_Games = tbl_Moby_Games.id_Moby_Games) = 1 THEN 1 ELSE 0 END")

		'Show some logs
		MKNetDXLib.frm_MKDXMemoEdit.CreateAndShowDialog("Games" & IIf(Me._id_Moby_Platforms = 0, " and Groups", "") & " import done, time: " & DateDiff(DateInterval.Hour, dt_Start, DateTime.Now) & " hours" & ControlChars.CrLf & "Traffic: " & iTraffic & " bytes " & ControlChars.CrLf & "Following up the logs." & noCatMessage, Me)

		MKNetDXLib.frm_MKDXMemoEdit.CreateAndShowDialog(GameImportLog, Me)

		If Me._id_Moby_Platforms = 0 Then
			MKNetDXLib.frm_MKDXMemoEdit.CreateAndShowDialog(GameGroupImportLog, Me)
		End If
	End Sub

	Private Sub popmnu_Moby_Web_Games_BeforePopup(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles popmnu_Moby_Web_Games.BeforePopup
		If Not grd_Moby_Games.Allow_Popup Then
			e.Cancel = True
			Return
		End If

		If BS_Moby_Web_Games.Current Is Nothing Then
			e.Cancel = True
		Else
			bbi_Import_Single_Game.Caption = _bbi_Import_Single_Game_Caption.Replace("{0}", BS_Moby_Web_Games.Current("Game_Title"))
		End If
	End Sub

	Private Sub popmnu_Moby_Web_Game_Groups_BeforePopup(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles popmnu_Moby_Web_Game_Groups.BeforePopup
		If Not grd_Moby_Game_Groups.Allow_Popup Then
			e.Cancel = True
			Return
		End If

		If BS_Moby_Web_Game_Groups.Current Is Nothing Then
			e.Cancel = True
		Else
			bbi_Import_Single_Game_Group.Caption = _bbi_Import_Single_Game_Group_Caption.Replace("{0}", BS_Moby_Web_Game_Groups.Current("Name"))
		End If
	End Sub

	Private Sub mni_Import_Single_Game_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		_tran = cls_Globals.Conn.BeginTransaction

		If BS_Moby_Web_Games.Current Is Nothing Then
			Return
		End If

		Try
			Import_Game(BS_Moby_Web_Games.Current("Game_Title"), BS_Moby_Web_Games.Current("URLPart"), BS_Moby_Web_Games.Current("Year"), BS_Moby_Web_Games.Current("Platform_URLPart"), BS_Moby_Web_Games.Current("id_Moby_Platforms"))
		Catch ex As Exception
			DevExpress.XtraEditors.XtraMessageBox.Show("  EXCEPTION: " & ex.Message & ControlChars.CrLf & ex.StackTrace.ToString)
		End Try

		Update_Moby_Releases_Platform_Owner(_tran)

		_tran.Commit()
	End Sub
#End Region

	Private Sub mni_Import_Single_Game_Group_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		_tran = cls_Globals.Conn.BeginTransaction

		If BS_Moby_Web_Game_Groups.Current Is Nothing Then
			Return
		End If

		Try
			Import_Game_Group(BS_Moby_Web_Game_Groups.Current("Name"), BS_Moby_Web_Game_Groups.Current("URLPart"), BS_Moby_Web_Game_Groups.Current("NumberOfGames"))
		Catch ex As Exception
			DevExpress.XtraEditors.XtraMessageBox.Show("  EXCEPTION: " & ex.Message & ControlChars.CrLf & ex.StackTrace.ToString)
		End Try

		_tran.Commit()
	End Sub

	''' <summary>
	''' For all Moby Platforms having an owner: Update all Moby Releases of that Platform with the Owner's ID, keep site's ID in id_Moby_Platforms_Site
	''' </summary>
	''' <param name="tran"></param>
	''' <remarks></remarks>
	Public Sub Update_Moby_Releases_Platform_Owner(tran As SQLite.SQLiteTransaction)
		Dim dt_Moby_Platforms_Children As DataTable = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT id_Moby_Platforms, id_Moby_Platforms_Owner FROM moby.tbl_Moby_Platforms WHERE id_Moby_Platforms_Owner IS NOT NULL", Nothing, tran)

		For Each row As DataRow In dt_Moby_Platforms_Children.Rows
			Dim sSQL As String = ""
			sSQL &= "	UPDATE	moby.tbl_Moby_Releases"
			sSQL &= "	SET			id_Moby_Platforms_Site = " & TC.getSQLFormat(row("id_Moby_Platforms"))
			sSQL &= "					, id_Moby_Platforms = " & TC.getSQLFormat(row("id_Moby_Platforms_Owner"))
			sSQL &= "	WHERE id_Moby_Platforms = " & TC.getSQLFormat(row("id_Moby_Platforms"))
			sSQL &= "	AND 	id_Moby_Games NOT IN (SELECT id_Moby_Games FROM moby.tbl_Moby_Releases WHERE id_Moby_Platforms = " & TC.getSQLFormat(row("id_Moby_Platforms_Owner")) & ")"

			DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
		Next
	End Sub

	Private Sub btn_GenreImport_Click(sender As Object, e As EventArgs) Handles btn_GenreImport.Click
		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			Upsert_Genre(tran, txb_GenreImport.Text)
		End Using
	End Sub
End Class
