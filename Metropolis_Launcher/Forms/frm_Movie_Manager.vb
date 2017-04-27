Public Class frm_Movie_Manager

	Private Sub btn_IMDBSearchText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_IMDBSearchText.Click
		Dim s_input As String
		s_input = txb_IMDBSearchText.Text
		GetSearchResults(s_input)
	End Sub

	Private Function GetSearchResults(ByVal s_input As String) As DataTable
		Dim s_url As String = "http://www.imdb.com/find?q=" & s_input.Replace(" ", "+") & "&s=all"
		Dim bTryAgain As Boolean = True
		Dim sContent As String = ""

		Dim result_url As String = ""
		Dim bTryAgain2 As Boolean = True
		Dim result_url_content As String = ""

		'call website with search results
		While bTryAgain
			sContent = MKNetLib.cls_MKWebClient.FetchURLToString(s_url)
			bTryAgain = False
			If sContent.Length = 0 AndAlso DevExpress.XtraEditors.XtraMessageBox.Show("Try again?", "", MessageBoxButtons.YesNo) <> MsgBoxResult.Yes Then
				bTryAgain = True
			End If
		End While

		'give html code into "htmlDoc"
		Dim htmlDoc As New HtmlAgilityPack.HtmlDocument
		htmlDoc.LoadHtml(sContent)

		'collect all expressions from a special class in coll_Results
		Dim coll_Results As HtmlAgilityPack.HtmlNodeCollection = htmlDoc.DocumentNode.SelectNodes("//td[@class='result_text']")
		If coll_Results Is Nothing Then
			Return Nothing
		End If

		'Parse: Year, Title, Categories, IMDBid
		For Each node_Result As HtmlAgilityPack.HtmlNode In coll_Results
			If node_Result.InnerHtml.Contains("/title/") Then
				Dim matches As System.Text.RegularExpressions.MatchCollection = MKNetLib.cls_MKRegex.GetMatches(node_Result.InnerHtml, "(tt\d+).+"">(.+)<\/a>( \((.+?)\))+")
				Dim isSeries As Boolean = False

				If matches.Count = 1 AndAlso matches(0).Groups.Count = 5 Then
					Dim row As DS_IMDB.tbl_MoviesRow = DS_IMDB.tbl_Movies.NewRow
					For Each cap As System.Text.RegularExpressions.Capture In matches(0).Groups(4).Captures
						If MKNetLib.cls_MKRegex.IsMatch(cap.Value, "\d\d\d\d") Then
							row.Year = cap.Value
						End If
						If MKNetLib.cls_MKRegex.IsMatch(cap.Value, "\D+") Then
							'row.id_Categories = cap
							'TODO: Setzen von isSeries
							'TODO: Categories abgleichen -> ID auslesen und hier speichern
						End If
					Next

					row.IMDBid = matches(0).Groups(1).Value
					row.Title = matches(0).Groups(2).Value

					'call website of one search result
					result_url = "http://www.imdb.com/title/" & matches(0).Groups(1).Value
					bTryAgain2 = True
					While bTryAgain2
						result_url_content = MKNetLib.cls_MKWebClient.FetchURLToString(result_url)
						bTryAgain2 = False
						If result_url_content.Length = 0 AndAlso DevExpress.XtraEditors.XtraMessageBox.Show("Try again?", "", MessageBoxButtons.YesNo) <> MsgBoxResult.Yes Then
							bTryAgain2 = True
						End If
					End While

					'give html code into "htmlDoc"
					Dim htmlDoc2 As New HtmlAgilityPack.HtmlDocument
					htmlDoc2.LoadHtml(result_url_content)

					Dim notes_Rating As HtmlAgilityPack.HtmlNodeCollection = htmlDoc2.DocumentNode.SelectNodes("//div[@class='titlePageSprite star-box-giga-star']")
					If notes_Rating IsNot Nothing Then
						Dim sRating As String() = notes_Rating.Nodes(0).InnerText.Split(".")
						row.Rating = Convert.ToDouble(sRating(0))
						If sRating.Length > 1 Then
							row.Rating += Convert.ToDouble(sRating(1)) / 10
						End If
					End If


					If isSeries = False Then
						DS_IMDB.tbl_Movies.Rows.Add(row)
					End If

				End If
			End If
		Next

		Return Nothing

	End Function

End Class
