Imports DevExpress.XtraGrid.Views.Grid

Public Class frm_MOBY_Extras_Manager

	Private _id_Moby_Releases As Int64
	Private _Platform_Short As String

	Private Moby_Download_Info As ucr_Emulation.cls_Moby_Download_Info


	Private WithEvents Moby_Extras_Downloader As System.Net.WebClient = New System.Net.WebClient

	Public Sub New(ByVal id_Moby_Releases As Int64, ByVal Platform_Short As String)
		InitializeComponent()

		Me._id_Moby_Releases = id_Moby_Releases
		Me._Platform_Short = Platform_Short

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Fill_src_frm_MOBY_Extras_Manager(tran, Me.DS_ML.src_frm_MOBY_Extras_Manager, Me._id_Moby_Releases)
		End Using

		Dim dict_ExtraType_Sorts As New Dictionary(Of String, Int64)

		For Each row As DS_ML.src_frm_MOBY_Extras_ManagerRow In Me.DS_ML.src_frm_MOBY_Extras_Manager.Rows
			If dict_ExtraType_Sorts.ContainsKey(row("ExtraType")) Then
				row("Sort") = dict_ExtraType_Sorts(row("ExtraType")) + 1
				dict_ExtraType_Sorts(row("ExtraType")) += 1
			Else
				row("Sort") = 1
				dict_ExtraType_Sorts(row("ExtraType")) = 1
			End If

			Dim url As String = row("URL")
			Dim filename As String = url.Split("/")(url.Split("/").Length - 1)
			Dim dirpath As Object = cls_Settings.Get_Extras_Directory() & "\moby" & IIf(row("ExtraType") = "Screenshots", "\screenshots", "\cover-art") & "\" & Me._Platform_Short
			Dim filepath As String = dirpath & "\" & filename

			If Alphaleonis.Win32.Filesystem.File.Exists(filepath) Then
				row("tmp_Available") = True
			Else
				row("tmp_Available") = False
			End If
		Next

		Me.chb_Download.Checked = TC.NZ(cls_Settings.GetSetting("MOBY_Extras_Manager_Download_on_Select"), True)

		Me.gv_Extras.ExpandAllGroups()
	End Sub

	Private Sub BS_Extras_CurrentChanged(sender As Object, e As EventArgs) 'Handles BS_Extras.CurrentChanged
		Me.lbl_MobyDownload_Error.Text = ""

		If BS_Extras.Current Is Nothing Then
			Me.pic_Game.Image = Nothing
		End If

		Dim url As String = BS_Extras.Current("URL")
		Dim filename As String = url.Split("/")(url.Split("/").Length - 1)
		Dim dirpath As Object = cls_Settings.Get_Extras_Directory()

		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
			Debug.WriteLine("EXTRAS: Extras_Directory not found, aborting")
			Return
		End If

		dirpath &= "\moby"
		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
			Try
				Alphaleonis.Win32.Filesystem.Directory.CreateDirectory(dirpath)
			Catch ex As Exception

			End Try
		End If
		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
			Debug.WriteLine("EXTRAS: Cannot create directory " & dirpath & ", aborting")
			Return
		End If

		Select Case BS_Extras.Current("ExtraType")
			Case "Screenshots"
				dirpath &= "\screenshots"
			Case "Cover Art"
				dirpath &= "\cover-art"
		End Select

		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
			Try
				Alphaleonis.Win32.Filesystem.Directory.CreateDirectory(dirpath)
			Catch ex As Exception

			End Try
		End If
		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
			Debug.WriteLine("EXTRAS: Cannot create directory " & dirpath & ", aborting")
			Return
		End If

		dirpath &= "\" & Me._Platform_Short

		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
			Try
				Alphaleonis.Win32.Filesystem.Directory.CreateDirectory(dirpath)
			Catch ex As Exception

			End Try
		End If
		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
			Debug.WriteLine("EXTRAS: Cannot create directory " & dirpath & ", aborting")
			Return
		End If

		Dim filepath As String = dirpath & "\" & filename

		If cls_Extras.EnsureExtrasFile(filepath) Then
			pic_Game.Image = cls_Extras.LoadImageFromStreamSafe(filepath)
			Me.BS_Extras.Current("tmp_Available") = True
			Me.gv_Extras.RefreshData()
			Return
		Else
			Me.pic_Game.Image = Nothing
		End If

		EnableMoveUpDownButtons()

		If Not Me.chb_Download.Checked Then
			Return
		End If

		'## Preparing Download ##
		Download_Moby_Extra(url, filepath, BS_Extras.Current)

	End Sub

	Private Sub Download_Moby_Extra(url, filepath, row)
		If Me.Moby_Extras_Downloader.IsBusy Then
			Return
		End If

		Me.pic_Game.Cursor = Cursors.WaitCursor
		Me.prg_Extras_Download.EditValue = 0
		Me.prg_Extras_Download.Visible = True

		Me.Moby_Download_Info = New ucr_Emulation.cls_Moby_Download_Info(url, filepath, Payload:=row, isArchiveOrg:=False)
		Debug.WriteLine("Fetching " & "http://www.mobygames.com" & url)
		Me.Moby_Extras_Downloader.DownloadFileAsync(New Uri("http://www.mobygames.com" & url), filepath)
	End Sub


	Private Sub EnableMoveUpDownButtons()
		Me.btn_Move_Down.Enabled = False
		Me.btn_Move_Up.Enabled = False

		Dim ExtraType = TC.NZ(BS_Extras.Current("ExtraType"), "")
		Dim Sort As Int64 = TC.NZ(BS_Extras.Current("Sort"), 0L)

		If Not TC.IsNullNothingOrEmpty(ExtraType) AndAlso Sort <> 0L Then
			For Each row As DS_ML.src_frm_MOBY_Extras_ManagerRow In Me.DS_ML.src_frm_MOBY_Extras_Manager.Rows
				If TC.NZ(row("ExtraType"), "") = ExtraType Then
					If TC.NZ(row("Sort"), 0L) <> 0 AndAlso TC.NZ(row("Sort"), 0L) > Sort Then
						Me.btn_Move_Down.Enabled = True
					End If
					If TC.NZ(row("Sort"), 0L) <> 0 AndAlso TC.NZ(row("Sort"), 0L) < Sort Then
						Me.btn_Move_Up.Enabled = True
					End If
				End If
			Next
		End If
	End Sub

	Private Sub gv_Extras_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles gv_Extras.RowCellStyle
		If e.RowHandle >= 0 Then
			Dim row As DataRow = gv_Extras.GetRow(e.RowHandle).Row
			If TC.NZ(row("tmp_Available"), False) = True Then
				e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Regular)
			Else
				e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Italic)
			End If
		End If
	End Sub

	Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
		cls_Settings.SetSetting("MOBY_Extras_Manager_Download_on_Select", Me.chb_Download.Checked)

		For Each row As DS_ML.src_frm_MOBY_Extras_ManagerRow In Me.DS_ML.src_frm_MOBY_Extras_Manager.Rows
			If Not TC.IsNullNothingOrEmpty(row("id_Moby_Extras_Properties")) Then
				'UPDATE
				Dim sSQL As String = ""
				sSQL &= "UPDATE tbl_Moby_Extras_Properties" & ControlChars.CrLf
				sSQL &= "	SET" & ControlChars.CrLf
				sSQL &= "	id_Moby_Releases = " & TC.getSQLFormat(Me._id_Moby_Releases) & ControlChars.CrLf
				sSQL &= "	, id_Moby_Releases_Cover_Art = " & TC.getSQLFormat(row("id_Moby_Releases_Cover_Art")) & ControlChars.CrLf
				sSQL &= "	, id_Moby_Releases_Screenshots = " & TC.getSQLFormat(row("id_Moby_Releases_Screenshots")) & ControlChars.CrLf
				sSQL &= "	, Show = " & TC.getSQLFormat(row("Show")) & ControlChars.CrLf
				sSQL &= "	, Sort = " & TC.getSQLFormat(row("Sort")) & ControlChars.CrLf
				sSQL &= "	WHERE id_Moby_Extras_Properties = " & TC.getSQLFormat(row("id_Moby_Extras_Properties")) & ControlChars.CrLf

				DataAccess.FireProcedure(cls_Globals.Conn, 0, sSQL)
			Else
				'INSERT
				Dim sSQL As String = ""
				sSQL &= "INSERT INTO tbl_Moby_Extras_Properties" & ControlChars.CrLf
				sSQL &= "	(" & ControlChars.CrLf
				sSQL &= "		id_Moby_Releases" & ControlChars.CrLf
				sSQL &= "		, id_Moby_Releases_Cover_Art" & ControlChars.CrLf
				sSQL &= "		, id_Moby_Releases_Screenshots" & ControlChars.CrLf
				sSQL &= "		, Show" & ControlChars.CrLf
				sSQL &= "		, Sort" & ControlChars.CrLf
				sSQL &= "	)" & ControlChars.CrLf
				sSQL &= "	VALUES" & ControlChars.CrLf
				sSQL &= "	(" & ControlChars.CrLf
				sSQL &= "		" & TC.getSQLFormat(Me._id_Moby_Releases) & ControlChars.CrLf
				sSQL &= "		, " & TC.getSQLFormat(row("id_Moby_Releases_Cover_Art")) & ControlChars.CrLf
				sSQL &= "		, " & TC.getSQLFormat(row("id_Moby_Releases_Screenshots")) & ControlChars.CrLf
				sSQL &= "		, " & TC.getSQLFormat(row("Show")) & ControlChars.CrLf
				sSQL &= "		, " & TC.getSQLFormat(row("Sort")) & ControlChars.CrLf
				sSQL &= "	)" & ControlChars.CrLf

				DataAccess.FireProcedure(cls_Globals.Conn, 0, sSQL)
			End If
		Next

		Me.DialogResult = DialogResult.OK
		Me.Close()
	End Sub

	Private Sub btn_Move_Up_Click(sender As Object, e As EventArgs) Handles btn_Move_Up.Click
		If BS_Extras.Current Is Nothing Then
			Return
		End If

		Dim ExtraType = TC.NZ(BS_Extras.Current("ExtraType"), "")
		Dim Sort As Int64 = TC.NZ(BS_Extras.Current("Sort"), 0L)
		Dim MaxLower As Int64 = -1
		Dim rowMaxLower As DS_ML.src_frm_MOBY_Extras_ManagerRow = Nothing

		If Not TC.IsNullNothingOrEmpty(ExtraType) AndAlso Sort <> 0L Then
			For Each row As DS_ML.src_frm_MOBY_Extras_ManagerRow In Me.DS_ML.src_frm_MOBY_Extras_Manager.Rows
				If TC.NZ(row("ExtraType"), "") = ExtraType Then
					If TC.NZ(row("Sort"), 0L) <> 0 AndAlso TC.NZ(row("Sort"), 0L) < Sort AndAlso (MaxLower = -1 OrElse TC.NZ(row("Sort"), 0L) > MaxLower) Then
						rowMaxLower = row
						MaxLower = TC.NZ(row("Sort"), 0L)
					End If
				End If
			Next
		End If

		If rowMaxLower IsNot Nothing Then
			BS_Extras.Current("Sort") = MaxLower
			rowMaxLower("Sort") = Sort
		End If

		EnableMoveUpDownButtons()
	End Sub

	Private Sub btn_Move_Down_Click(sender As Object, e As EventArgs) Handles btn_Move_Down.Click
		If BS_Extras.Current Is Nothing Then
			Return
		End If

		Dim ExtraType = TC.NZ(BS_Extras.Current("ExtraType"), "")
		Dim Sort As Int64 = TC.NZ(BS_Extras.Current("Sort"), 0L)
		Dim MinHigher As Int64 = -1
		Dim rowMinHigher As DS_ML.src_frm_MOBY_Extras_ManagerRow = Nothing

		If Not TC.IsNullNothingOrEmpty(ExtraType) AndAlso Sort <> 0L Then
			For Each row As DS_ML.src_frm_MOBY_Extras_ManagerRow In Me.DS_ML.src_frm_MOBY_Extras_Manager.Rows
				If TC.NZ(row("ExtraType"), "") = ExtraType Then
					If TC.NZ(row("Sort"), 0L) <> 0 AndAlso TC.NZ(row("Sort"), 0L) > Sort AndAlso (MinHigher = -1 OrElse TC.NZ(row("Sort"), 0L) < MinHigher) Then
						rowMinHigher = row
						MinHigher = TC.NZ(row("Sort"), 0L)
					End If
				End If
			Next
		End If

		If rowMinHigher IsNot Nothing Then
			BS_Extras.Current("Sort") = MinHigher
			rowMinHigher("Sort") = Sort
		End If

		EnableMoveUpDownButtons()
	End Sub


	Private Sub Moby_Extras_Downloader_DownloadProgressChanged(sender As Object, e As Net.DownloadProgressChangedEventArgs) Handles Moby_Extras_Downloader.DownloadProgressChanged
		Me.prg_Extras_Download.EditValue = e.ProgressPercentage
	End Sub

	Private Sub Moby_Extras_Downloader_DownloadFileCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles Moby_Extras_Downloader.DownloadFileCompleted
		Debug.WriteLine("EXTRAS: Download completed event")

		Me.pic_Game.Cursor = Cursors.Default

		Me.prg_Extras_Download.Visible = False

		If e.Cancelled Then
			Debug.WriteLine("EXTRAS: Download was cancelled, aborting")
			Return
		End If

		If Me.Moby_Download_Info Is Nothing Then
			Debug.WriteLine("EXTRAS: After Download: Game has changed, aborting")
			Return
		End If

		If e.Error IsNot Nothing Then
			Debug.WriteLine("EXTRAS: Download has errored: " & e.Error.Message & e.Error.StackTrace)
			If Not Me.Moby_Download_Info.isArchiveOrg Then
				'Retry from archive.org directly
				Me.Moby_Download_Info.isArchiveOrg = True

				Dim archiveOrgUrl As String = "http://web.archive.org/web/http://www.mobygames.com" & Me.Moby_Download_Info.URL
				'Dim archiveOrgUrl As String = "http://web.archive.org/web/http%3A%2F%2Fwww.mobygames.com" & url.ToString.Replace("/", "%2F")
				'does not work: Dim archiveOrgUrl As String = "https://archive.org/download/www.mobygames.com" & url

				Me.Moby_Extras_Downloader.DownloadFileAsync(New Uri(archiveOrgUrl), Me.Moby_Download_Info.Filepath)
			Else
				Me.lbl_MobyDownload_Error.Text = "Error while downloading: " & e.Error.Message
				Me.pic_Game.Image = Nothing
			End If

			Return
		End If

		If Not Alphaleonis.Win32.Filesystem.File.Exists(Me.Moby_Download_Info.Filepath) Then
			Debug.WriteLine("EXTRAS: After Download: File not found, aborting")
			Return
		End If

		Debug.WriteLine("EXTRAS: After Download: try to load as Image")
		Dim img As System.Drawing.Image = cls_Extras.LoadImageFromStreamSafe(Me.Moby_Download_Info.Filepath, False)

		If img Is Nothing Then
			Debug.WriteLine("EXTRAS: After Download: it is NOT an image")

			If Me.Moby_Download_Info.isArchiveOrg Then
				Debug.WriteLine("EXTRAS: After Download: archive.org may have sent just html with the image embedded")
				'Load the file contents as HTML and check for "real" download URL
				Dim sContent As String = MKNetLib.cls_MKFileSupport.GetFileContents(Me.Moby_Download_Info.Filepath)

				Try
					Dim newURL As String = MKNetLib.cls_MKRegex.GetMatches(sContent, "<iframe.*src=""(.*?)"".*?>")(0).Groups(1).Value

					Debug.WriteLine("EXTRAS: After Download: fetching from " & newURL)
					Me.Moby_Extras_Downloader.DownloadFileAsync(New Uri(newURL), Me.Moby_Download_Info.Filepath)
				Catch ex As Exception
					Debug.WriteLine("EXTRAS: After Download: Failed reading the HTML")
				End Try
			Else
				Debug.WriteLine("EXTRAS: Removing file")
				Try
					Alphaleonis.Win32.Filesystem.File.Delete(Me.Moby_Download_Info.Filepath)
				Catch ex As Exception

				End Try
			End If
		End If

		Debug.WriteLine("EXTRAS: After Download: Loading for display")
		If cls_Extras.EnsureExtrasFile(Me.Moby_Download_Info.Filepath) Then
			pic_Game.Image = img

		End If

		If Me.Moby_Download_Info.Payload IsNot Nothing Then
			Me.Moby_Download_Info.Payload("tmp_Available") = True
			Me.gv_Extras.RefreshData()
		End If
	End Sub

	Private Sub gv_Extras_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_Extras.FocusedRowChanged
		BS_Extras_CurrentChanged(Nothing, Nothing)
	End Sub
End Class