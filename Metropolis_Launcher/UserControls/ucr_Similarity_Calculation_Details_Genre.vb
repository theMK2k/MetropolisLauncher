Public Class ucr_Similarity_Calculation_Details_Genre
	Private _lbl_Explanation_Text As String
	Private _gb_A_Text As String
	Private _gb_B_Text As String
	Private _gb_AB_Text As String

	Public Sub New()
		InitializeComponent()

		Me._lbl_Explanation_Text = Me.lbl_Explanation.Text
		Me._gb_A_Text = Me.gb_A.Text
		Me._gb_B_Text = Me.gb_B.Text
		Me._gb_AB_Text = Me.gb_AB.Text
	End Sub

	Public Sub Init(ByVal Moby_Game_Genres_Category As cls_Globals.enm_Moby_Genres_Categories, ByVal Category_Name As String, ByVal Weight As Object, ByVal bUseMobyReleaseOnly As Boolean, ByVal bTestUseMobyRelease As Boolean, ByVal id_Emu_Games_A As Integer, ByVal id_Emu_Games_B As Integer, ByVal id_Moby_Releases_B As Integer)
		Try
			Weight = TC.NZ(Weight, 1)

			lbl_Weight_Text.Text = Weight

			grd_A.DataSource = DS_ML.Select_Genres_By_id_Emu_Games(id_Emu_Games_A, Moby_Game_Genres_Category)

			If bUseMobyReleaseOnly OrElse bTestUseMobyRelease Then
				grd_B.DataSource = DS_ML.Select_Genres_By_id_Moby_Releases(id_Moby_Releases_B, Moby_Game_Genres_Category)
				grd_AB.DataSource = DS_ML.Select_Genres_AB_By_id_Moby_Releases(id_Emu_Games_A, id_Moby_Releases_B, Moby_Game_Genres_Category)
			Else
				grd_B.DataSource = DS_ML.Select_Genres_By_id_Emu_Games(id_Emu_Games_B, Moby_Game_Genres_Category)
				grd_AB.DataSource = DS_ML.Select_Genres_AB_By_id_Emu_Games(id_Emu_Games_A, id_Emu_Games_B, Moby_Game_Genres_Category)
			End If

			_gb_A_Text &= " (" & grd_A.DataSource.Rows.Count & ")"
			_gb_B_Text &= " (" & grd_B.DataSource.Rows.Count & ")"
			_gb_AB_Text &= " (" & grd_AB.DataSource.Rows.Count & ")"

			Me.lbl_Explanation.Text = Me._lbl_Explanation_Text.Replace("%%Category_Name%%", Category_Name)
			Me.gb_A.Text = Me._gb_A_Text.Replace("%%Category_Name%%", Category_Name)
			Me.gb_B.Text = Me._gb_B_Text.Replace("%%Category_Name%%", Category_Name)
			Me.gb_AB.Text = Me._gb_AB_Text.Replace("%%Category_Name%%", Category_Name)
		Catch ex As Exception

		End Try
	End Sub
End Class
