Public Class frm_Emu_Game_Screenshotviewer

	Public _al_Screenshots As ArrayList

	Public Platform_Short As String
	Public FileName As String

	''' <summary>
	''' Constructor
	''' </summary>
	''' <param name="id_Emu_Games"></param>
	''' <param name="al_Screenshots">ArrayList of Bitmap Objects or cls_Extras_Results</param>
	Public Sub New(ByVal id_Emu_Games As Integer, ByVal al_Screenshots As ArrayList)
		InitializeComponent()

		Dim dt As New DS_ML.src_ucr_Emulation_GamesDataTable

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Fill_tbl_Emu_Extras(tran, DS_ML.tbl_Emu_Extras)
			DS_ML.Fill_src_ucr_Emulation_Games(tran, dt, Nothing, Nothing, Nothing, id_Emu_Games)
		End Using

		Dim row_Emu_Game As DataRow = dt.Rows(0)

		Dim Game As String = TC.NZ(row_Emu_Game("Game"), "<Unknown Game>")

		If al_Screenshots Is Nothing OrElse al_Screenshots.Count = 0 OrElse al_Screenshots.Item(0).GetType Is GetType(Bitmap) Then
			_al_Screenshots = al_Screenshots

			For i = 0 To _al_Screenshots.Count - 1
				Dim row As DataRow = DS_Screenshots.Tables("tbl_Screenshots").NewRow
				row("Sort") = row("id")
				DS_Screenshots.Tables("tbl_Screenshots").Rows.Add(row)
			Next

			Me.Text = "New Screenshots for " & Game
		Else
			'ArrayList al_Screenshots contains cls_Extras_Result Objects
			_al_Screenshots = New ArrayList
			For Each old_extra As cls_Extras.cls_Extras_Result In al_Screenshots
				Try
					Dim img As Bitmap = Image.FromStream(New IO.MemoryStream(Alphaleonis.Win32.Filesystem.File.ReadAllBytes(old_extra._Path)))

					If img.PhysicalDimension.Width > 1 And img.PhysicalDimension.Height > 1 Then
						_al_Screenshots.Add(img)
						Dim row As DataRow = DS_Screenshots.Tables("tbl_Screenshots").NewRow

						row("Sort") = row("id")

						row("Category") = 4

						Try
							row("Category") = Me.DS_ML.tbl_Emu_Extras.Select("Name = " & TC.getSQLFormat(old_extra._ExtraType))(0)("id_Emu_Extras")
						Catch ex As Exception

						End Try

						DS_Screenshots.Tables("tbl_Screenshots").Rows.Add(row)
					End If
				Catch ex As Exception

				End Try
			Next

			Me.Text = "Extras for " & Game
		End If

		Dim FileNameX As String = ""

		If TC.NZ(row_Emu_Game("InnerFile"), "") <> "" Then
			FileNameX = row_Emu_Game("InnerFile")
		Else
			FileNameX = TC.NZ(row_Emu_Game("File"), "")
		End If

		Platform_Short = TC.NZ(row_Emu_Game("Platform_Short"), "")

		Select Case TC.NZ(row_Emu_Game("id_Moby_Platforms"), 0)
			Case cls_Globals.enm_Moby_Platforms.win
				'Windows Games
				FileName = cls_Extras.GetExtraFilename(Game, FileNameX)
			Case Else
				'Standard Emulation Games
				FileName = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(FileNameX)
		End Select

		Set_Buttons_Enabled()
	End Sub

	Private Sub Set_Buttons_Enabled()
		If BS_Screenshots.Current IsNot Nothing Then
			Dim iSortMin As Integer = Integer.MaxValue
			Dim iSortMax As Integer = 0

			For Each row As DataRow In Me.DS_Screenshots.Tables("tbl_Screenshots").Rows
				If iSortMin > TC.NZ(row("Sort"), Integer.MaxValue) Then iSortMin = row("Sort")
				If iSortMax < TC.NZ(row("Sort"), 0) Then iSortMax = row("Sort")
			Next

			If BS_Screenshots.Current("Sort") <> iSortMin Then btn_Move_Up.Enabled = True Else btn_Move_Up.Enabled = False
			If BS_Screenshots.Current("Sort") <> iSortMax Then btn_Move_Down.Enabled = True Else btn_Move_Down.Enabled = False
		Else
			btn_Move_Up.Enabled = False
			btn_Move_Down.Enabled = False
		End If
	End Sub

	Private Sub BS_Screenshots_CurrentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BS_Screenshots.CurrentChanged
		If BS_Screenshots.Current IsNot Nothing Then
			Me.pic_Game.Image = _al_Screenshots.Item(BS_Screenshots.Current("id"))
		Else
			Me.pic_Game.Image = Nothing
		End If

		Set_Buttons_Enabled()
	End Sub

	Private Sub pic_Game_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pic_Game.Click
		Using frm As New frm_Screenshot_Edit(_al_Screenshots.Item(BS_Screenshots.Current("id")))
			If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
				_al_Screenshots.Item(BS_Screenshots.Current("id")) = frm.CropImage
				Me.pic_Game.Image = frm.CropImage
			End If
		End Using
	End Sub

	Private Sub Handle_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd_Screenshots.KeyDown, pic_Game.KeyDown
		If e.KeyCode = Keys.Escape Then
			Me.DialogResult = Windows.Forms.DialogResult.Cancel
			Me.Close()
		End If
	End Sub

	Private Sub btn_Move_Up_Click(sender As Object, e As EventArgs) Handles btn_Move_Up.Click
		If BS_Screenshots.Current Is Nothing Then Return

		For Each row As DataRow In Me.DS_Screenshots.Tables("tbl_Screenshots").Rows
			If TC.NZ(row("Sort"), -9999) = (BS_Screenshots.Current("Sort") - 1) Then
				row("Sort") = BS_Screenshots.Current("Sort")
				BS_Screenshots.Current("Sort") -= 1
				Exit For
			End If
		Next

		Set_Buttons_Enabled()

		'Workaround for Grid Update on Datasource change
		grd_Screenshots.Focus()
		btn_Move_Up.Focus()
	End Sub

	Private Sub btn_Move_Down_Click(sender As Object, e As EventArgs) Handles btn_Move_Down.Click
		If BS_Screenshots.Current Is Nothing Then Return

		For Each row As DataRow In Me.DS_Screenshots.Tables("tbl_Screenshots").Rows
			If TC.NZ(row("Sort"), -9999) = (BS_Screenshots.Current("Sort") + 1) Then
				row("Sort") = BS_Screenshots.Current("Sort")
				BS_Screenshots.Current("Sort") += 1
				Exit For
			End If
		Next

		Set_Buttons_Enabled()

		'Workaround for Grid Update on Datasource change
		grd_Screenshots.Focus()
		btn_Move_Down.Focus()
	End Sub

	Public Sub AddImage(ByVal img As Bitmap)
		If img.PhysicalDimension.Width > 1 And img.PhysicalDimension.Height > 1 Then
			Dim row As DataRow = DS_Screenshots.Tables("tbl_Screenshots").NewRow
			row("Sort") = row("id")
			DS_Screenshots.Tables("tbl_Screenshots").Rows.Add(row)
			_al_Screenshots.Add(img)
		End If
	End Sub

	Public Sub AddImages()
		Dim sFiles() As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("Add Extras", "All image files (*.jpg;*.png)|*.jpg;*.png", MultiSelect:=True, ParentForm:=Me)
		If sFiles IsNot Nothing AndAlso sFiles.Length > 0 Then
			For Each sFile As String In sFiles
				Try
					Dim img As Bitmap = Image.FromStream(New IO.MemoryStream(Alphaleonis.Win32.Filesystem.File.ReadAllBytes(sFile)))
					AddImage(img)
				Catch ex As Exception

				End Try
			Next
		End If
	End Sub

	Private Sub bbi_Add_from_Files_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Add_from_Files.ItemClick
		AddImages()
	End Sub

	Private Sub bbi_Add_from_Clipboard_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Add_from_Clipboard.ItemClick
		If Clipboard.ContainsImage Then
			Dim img As Bitmap = Clipboard.GetImage
			AddImage(img)
		End If

	End Sub

	Private Sub popmnu_Add_BeforePopup(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles popmnu_Add.BeforePopup
		If Clipboard.ContainsImage Then
			bbi_Add_from_Clipboard.Enabled = True
		Else
			bbi_Add_from_Clipboard.Enabled = False
		End If

	End Sub

	Private Sub gv_Screenshots_MouseMove(sender As Object, e As MouseEventArgs) Handles gv_Screenshots.MouseMove
		grd_Screenshots.ShowHandInColumns(gv_Screenshots, {"Use"}, e)
	End Sub

	Private Sub gv_Screenshots_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_Screenshots.FocusedRowChanged
		Dim gv As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

		If TC.NZ(gv.GetIncrementalText(), "") <> "" Then
			gv.ClearSelection()
			gv.SelectRow(gv.FocusedRowHandle)
		End If
	End Sub
End Class
