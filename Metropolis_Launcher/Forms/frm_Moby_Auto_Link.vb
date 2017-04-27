Imports System.ComponentModel

Public Class frm_Moby_Auto_Link

	Private _src_Moby_Releases As DS_MobyDB.src_Moby_ReleasesDataTable

	Private _dict_Moby_Releases As New Dictionary(Of String, DS_MobyDB.src_Moby_ReleasesRow)

	Private _Auto_Link_Options As frm_Moby_Auto_Link_Options.cls_Moby_Auto_Link_Options

	Private _tbl_Moby_Auto_Link As DS_ML.tbl_Moby_Auto_LinkDataTable

	Private _Moby_Platform_URLPart As String

	Public Sub New(ByRef tbl_Moby_Auto_Link As DS_ML.tbl_Moby_Auto_LinkDataTable, ByRef src_Moby_Releases As DS_MobyDB.src_Moby_ReleasesDataTable, ByVal sExplanation As String, ByRef Auto_Link_Options As frm_Moby_Auto_Link_Options.cls_Moby_Auto_Link_Options, ByVal Moby_Platform_URLPart As String)
		InitializeComponent()

		barmng.SetPopupContextMenu(grd_Moby_Releases, popmnu_Moby_Games)
		barmng.SetPopupContextMenu(grd_Moby_Auto_Link, popmnu_Link)

		Me.lbl_Explanation.Text = sExplanation
		Me._Auto_Link_Options = Auto_Link_Options

		Me._tbl_Moby_Auto_Link = tbl_Moby_Auto_Link
		Me._src_Moby_Releases = src_Moby_Releases

		Me._Moby_Platform_URLPart = Moby_Platform_URLPart

		'### Import and Filter Moby Auto Link Rows ###
		Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Nothing), 400, 60, ProgressBarStyle.Blocks, False, "Filtering Game names...", 0, _tbl_Moby_Auto_Link.Rows.Count, False)
		prg.Start()

		For Each row As DS_ML.tbl_Moby_Auto_LinkRow In _tbl_Moby_Auto_Link.Rows
			Me.DS_ML.tbl_Moby_Auto_Link.ImportRow(row)
		Next

		For Each row As DS_ML.tbl_Moby_Auto_LinkRow In Me.DS_ML.tbl_Moby_Auto_Link.Rows
			prg.IncreaseCurrentValue()

			row.GameName_Filtered = Apply_Filter(TC.NZ(row("GameName"), ""), False)
		Next

		prg.Close()

		'### Import and Filter Moby Releases Rows ###
		prg = New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Nothing), 400, 60, ProgressBarStyle.Blocks, False, "Filtering Moby Release names...", 0, _src_Moby_Releases.Rows.Count, False)
		prg.Start()

		For Each row As DS_MobyDB.src_Moby_ReleasesRow In _src_Moby_Releases.Rows
			prg.IncreaseCurrentValue()

			Dim tmp_Gamename_Filtered As String = Apply_Filter(TC.NZ(row("Gamename"), ""), True)
			row("tmp_Gamename_Filtered") = tmp_Gamename_Filtered

			If tmp_Gamename_Filtered <> "" AndAlso Not Me._dict_Moby_Releases.ContainsKey(tmp_Gamename_Filtered) Then
				Me._dict_Moby_Releases(tmp_Gamename_Filtered) = row
			End If
		Next

		Me.BS_Moby_Releases.DataSource = Me._src_Moby_Releases

		prg.Close()

		'### Do the matching ###
		Dim arrMobyReleaseNameList As String() = (From row As DataRow In _src_Moby_Releases.Rows Select TC.NZ(row("tmp_Gamename_Filtered"), "")).ToArray
		Dim glistMobyReleaseNameList As New Generic.List(Of String)
		glistMobyReleaseNameList.AddRange(arrMobyReleaseNameList)

		prg = New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Nothing), 400, 60, ProgressBarStyle.Blocks, False, "Analyzing entry {0} of {1}", 0, Me.DS_ML.tbl_Moby_Auto_Link.Rows.Count, False)
		prg.Start()

		For Each row_Game As DS_ML.tbl_Moby_Auto_LinkRow In Me.DS_ML.tbl_Moby_Auto_Link.Rows
			prg.IncreaseCurrentValue()

			Dim Gamename_Filtered As String = TC.NZ(row_Game("GameName_Filtered"), "")

			If Gamename_Filtered = "" Then
				Continue For
			End If

			Dim minimumScore As Double = CDbl(Me._Auto_Link_Options.Minimum_Match_Score) / CDbl(100)

			Dim foundWords As List(Of String) = MKNetLib.cls_MKStringSupport.FuzzySearch.Search(Gamename_Filtered, glistMobyReleaseNameList, minimumScore)

			Dim row_BestMatch As DS_MobyDB.src_Moby_ReleasesRow = Nothing
			Dim score_BestMatch As Double = 0.0

			For Each foundWord As String In foundWords
				Dim levenshteinDistance As Integer = MKNetLib.cls_MKStringSupport.FuzzySearch.LevenshteinDistance(Gamename_Filtered, foundWord)

				Dim length As Integer = Math.Max(Gamename_Filtered.Length, foundWord.Length)
				Dim score As Double = 1.0 - CDbl(levenshteinDistance) / length

				If score > score_BestMatch Then
					row_BestMatch = Me._dict_Moby_Releases(foundWord)
					score_BestMatch = score
				End If
			Next

			If score_BestMatch > 0.0 AndAlso row_BestMatch IsNot Nothing Then
				row_Game.Match_Accuracy = CType(score_BestMatch * 100, Integer)
				row_Game.Match_Moby_created = row_BestMatch("created")
				row_Game.Match_Moby_Gamename = row_BestMatch("Gamename")
				row_Game.Match_Moby_Gamename_Filtered = row_BestMatch("tmp_Gamename_Filtered")
				row_Game.Match_Moby_Games_URLPart = row_BestMatch("Moby_Games_URLPart")
				row_Game.Match_id_Moby_Releases = Math.Abs(row_BestMatch("id_Moby_Releases"))
				row_Game.Match_Moby_Year = row_BestMatch("Year")
				row_Game("Developer") = row_BestMatch("Developer")
				row_Game("Publisher") = row_BestMatch("Publisher")
				row_Game("deprecated") = row_BestMatch("deprecated")

				If score_BestMatch = 1 Then
					row_Game.Apply = True
				End If
			End If
		Next

		prg.Close()
	End Sub

	Private Function Apply_Filter(ByVal text As String, ByVal is_Moby_Release_Name As Boolean) As String
		Dim text_filtered As String

		If is_Moby_Release_Name OrElse Not Me._Auto_Link_Options.Strip_File_Extension Then
			text_filtered = text.ToLower()
		Else
			Try
				text_filtered = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(text).ToLower()
			Catch ex As Exception
				text_filtered = text.ToLower()
			End Try
		End If

		'Remove [] tags from moby release names
		If is_Moby_Release_Name Then
			Dim matches As System.Text.RegularExpressions.MatchCollection = MKNetLib.cls_MKRegex.GetMatches(text_filtered, "\[.*?\]")
			If matches.Count > 0 Then
				For Each match As System.Text.RegularExpressions.Match In matches
					text_filtered = text_filtered.Replace(match.Value, "").Trim
				Next
			End If
		End If

		'(Optional) Remove Tags
		If Not is_Moby_Release_Name AndAlso Me._Auto_Link_Options.Strip_Tags Then
			'Remove all tags
			Dim dict_contents As Dictionary(Of String, String) = frm_Tag_Parser_Edit.Extract_Content_From_FileName(text_filtered)

			For Each scontent As String In dict_contents.Keys
				text_filtered = text_filtered.Replace(scontent, "")
			Next
		End If

		'Get rid of double/triple etc. spaces
		While text_filtered.Contains("  ")
			text_filtered = text_filtered.Replace("  ", " ")
		End While
		text_filtered = Trim(text_filtered)

		'Split into whole words and sort them
		If Me._Auto_Link_Options.Sort_Words Then
			Dim words As String() = text_filtered.Split(" ")

			Dim new_text_filtered As String = ""

			Array.Sort(words)

			For Each word As String In words
				new_text_filtered &= IIf(new_text_filtered <> "", " ", "") & word
			Next

			text_filtered = new_text_filtered
		End If

		'Remove unwanted Characters
		If Me._Auto_Link_Options.Remove_Characters AndAlso Me._Auto_Link_Options.Remove_Characters_String <> "" Then
			For Each character As Char In Me._Auto_Link_Options.Remove_Characters_String
				text_filtered = text_filtered.Replace(character, "")
			Next
		End If

		Return text_filtered.Trim()
	End Function

	Private Sub frm_Moby_Auto_Link_Shown(sender As Object, e As EventArgs) Handles Me.Shown


	End Sub

	Private Sub gv_Moby_Auto_Link_MouseMove(sender As Object, e As MouseEventArgs) Handles abgvMoby_Auto_Link.MouseMove
		Dim hitinfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = abgvMoby_Auto_Link.CalcHitInfo(e.Location)
		If hitinfo.InRowCell Then
			grd_Moby_Auto_Link.Cursor = Cursors.Default

			If {"Apply"}.Contains(hitinfo.Column.FieldName) Then
				If hitinfo.RowHandle >= 0 Then
					If TC.NZ(abgvMoby_Auto_Link.GetRow(hitinfo.RowHandle).Row("Match_Moby_Games_URLPart"), "") <> "" Then
						grd_Moby_Auto_Link.Cursor = Cursors.Hand
					Else
						grd_Moby_Auto_Link.Cursor = Cursors.No
					End If
				End If
			End If
		End If
	End Sub

	Private Sub gv_Moby_Auto_Link_MouseDown(sender As Object, e As MouseEventArgs) Handles abgvMoby_Auto_Link.MouseDown
		If e.Button = MouseButtons.Left Then
			Dim hitinfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = abgvMoby_Auto_Link.CalcHitInfo(e.Location)

			If Not hitinfo.InColumn AndAlso Not hitinfo.InColumnPanel AndAlso Not hitinfo.InFilterPanel AndAlso Not hitinfo.InGroupColumn AndAlso Not hitinfo.InGroupPanel Then
				If hitinfo.Column IsNot Nothing AndAlso {"Apply"}.Contains(hitinfo.Column.FieldName) AndAlso hitinfo.RowHandle > -1 Then

					If hitinfo.RowHandle >= 0 Then
						Dim row As DataRow = abgvMoby_Auto_Link.GetRow(hitinfo.RowHandle).Row
						If TC.NZ(row("Match_Moby_Games_URLPart"), "") <> "" Then
							row("Apply") = Not TC.NZ(row("Apply"), False)
							grd_Moby_Auto_Link.Refresh()
						End If
					End If
				End If
			End If
		End If

		'If e.Button = MouseButtons.Right Then
		'	Me.popmnu_Link.ShowPopup(New Point(e.X, e.Y))
		'End If
	End Sub

	Private Sub BS_Moby_Auto_Link_CurrentChanged(sender As Object, e As EventArgs) Handles BS_Moby_Auto_Link.CurrentChanged
		gv_Moby_Releases.RefreshData()

		If BS_Moby_Auto_Link.Current Is Nothing Then Return

		If Not TC.IsNullNothingOrEmpty(BS_Moby_Auto_Link.Current("Match_id_Moby_Releases")) Then
			Dim iNewPos As Integer = BS_Moby_Releases.Find("id_Moby_Releases", BS_Moby_Auto_Link.Current("Match_id_Moby_Releases"))
			If iNewPos > 0 Then
				BS_Moby_Releases.Position = iNewPos
				Me.gv_Moby_Releases.ClearSelection()
				Me.gv_Moby_Releases.SelectRow(Me.gv_Moby_Releases.FocusedRowHandle)

			End If
		End If
	End Sub

	Private Sub grd_Moby_Releases_DoubleClick(sender As Object, e As EventArgs) Handles grd_Moby_Releases.DoubleClick
		Dim e_mouse As DevExpress.Utils.DXMouseEventArgs = e
		Dim hitinfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = gv_Moby_Releases.CalcHitInfo(e_mouse.Location)
		If Not hitinfo.InRow Then
			Return
		End If

		If BS_Moby_Auto_Link.Current IsNot Nothing AndAlso BS_Moby_Releases.Current IsNot Nothing Then
			For Each iRowHandle As Integer In abgvMoby_Auto_Link.GetSelectedRows
				Dim row As DataRowView = abgvMoby_Auto_Link.GetRow(iRowHandle)
				If iRowHandle >= 0 AndAlso abgvMoby_Auto_Link.GetRow(iRowHandle) IsNot Nothing Then
					row.Row("Match_id_Moby_Releases") = Math.Abs(BS_Moby_Releases.Current("id_Moby_Releases"))
					row.Row("Match_Moby_Games_URLPart") = BS_Moby_Releases.Current("Moby_Games_URLPart").Replace("\", "")
					'row.Row("Match_Accuracy") = DBNull.Value
					row.Row("Match_Moby_created") = BS_Moby_Releases.Current("created")
					row.Row("Match_Moby_Gamename") = BS_Moby_Releases.Current("Gamename")
					row.Row("Match_Moby_Gamename_Filtered") = BS_Moby_Releases.Current("tmp_Gamename_Filtered")
					row.Row("Match_Moby_Year") = BS_Moby_Releases.Current("Year")
					row.Row("Developer") = BS_Moby_Releases.Current("Developer")
					row.Row("Publisher") = BS_Moby_Releases.Current("Publisher")
					row.Row("deprecated") = BS_Moby_Releases.Current("deprecated")
					row.Row("Apply") = True
				End If
			Next
		End If
	End Sub

	Private Sub abgvMoby_Auto_Link_KeyDown(sender As Object, e As KeyEventArgs) Handles abgvMoby_Auto_Link.KeyDown
		If BS_Moby_Auto_Link.Current Is Nothing Then
			Return
		End If

		If e.KeyCode = Keys.Enter Then
			If TC.NZ(BS_Moby_Auto_Link.Current("Match_Moby_Games_URLPart"), "") <> "" Then
				BS_Moby_Auto_Link.Current("Apply") = Not TC.NZ(BS_Moby_Auto_Link.Current("Apply"), False)
				BS_Moby_Auto_Link.EndEdit()
				grd_Moby_Auto_Link.Refresh()
			End If

		End If
	End Sub

	Private Sub bbi_Open_Moby_Page_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Open_Moby_Page.ItemClick
		If BS_Moby_Releases.Current Is Nothing Then Return

		Try
			Dim sURL As String = "http://www.mobygames.com/game/" & Me._Moby_Platform_URLPart & "/" & TC.NZ(BS_Moby_Releases.Current("Moby_Games_URLPart"), "").Replace("\", "")
			Dim procinfo As New ProcessStartInfo(sURL)
			procinfo.UseShellExecute = True
			Process.Start(procinfo)
		Catch ex As Exception

		End Try
	End Sub

	Private Sub popmnu_Moby_Games_BeforePopup(sender As Object, e As CancelEventArgs) Handles popmnu_Moby_Games.BeforePopup
		If Not grd_Moby_Releases.Allow_Popup Then
			e.Cancel = True
			Return
		End If
	End Sub

	Private Sub popmnu_Link_BeforePopup(sender As Object, e As CancelEventArgs) Handles popmnu_Link.BeforePopup
		If Not grd_Moby_Auto_Link.Allow_Popup Then
			e.Cancel = True
			Return
		End If
	End Sub

	Private Sub SetApply(ByVal apply As Boolean)
		For Each rowHandle As Integer In Me.abgvMoby_Auto_Link.GetSelectedRows
			If rowHandle >= 0 Then
				Dim row As DataRow = Me.abgvMoby_Auto_Link.GetRow(rowHandle)
				row("Apply") = apply
			End If
		Next
	End Sub

	Private Sub bbi_Apply_True_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Apply_True.ItemClick
		SetApply(True)
	End Sub

	Private Sub bbi_Apply_False_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Apply_False.ItemClick
		SetApply(False)
	End Sub

	Private Sub abgvMoby_Auto_Link_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles abgvMoby_Auto_Link.FocusedRowChanged
		Dim gv As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView = CType(sender, DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView)

		If TC.NZ(gv.GetIncrementalText(), "") <> "" Then
			gv.ClearSelection()
			gv.SelectRow(gv.FocusedRowHandle)
		End If
	End Sub
End Class