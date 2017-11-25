Public Class frm_Similarity_Calculation_Details

	Private _id_Similarity_Calculation_Results As Integer = 0
	Private _id_Emu_Games_A As Integer = 0
	Private _id_Emu_Games_B As Integer = 0
	Private _id_Moby_Releases_B As Integer = 0
	Private _row_Emu_Game_Similarity_Result As DataRow = Nothing
	Private _id_Similarity_Calculation_Configuration As Integer = 0

	Public Sub New(ByVal id_Similarity_Calculation_Results As Integer, ByVal row_Emu_Game_Similarity_Result As DataRow, Optional ByVal id_Emu_Games_B As Integer = 0, Optional ByVal id_Moby_Releases_B As Integer = 0, Optional ByVal id_Similarity_Calculation_Configuration As Integer = 0, Optional ByVal id_Emu_Games_A As Integer = 0)
		InitializeComponent()

		If Not Me.DesignMode Then
			tcl_Main.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
		End If

		_id_Similarity_Calculation_Results = id_Similarity_Calculation_Results
		_id_Emu_Games_B = id_Emu_Games_B
		_id_Moby_Releases_B = id_Moby_Releases_B
		_row_Emu_Game_Similarity_Result = row_Emu_Game_Similarity_Result
		_id_Similarity_Calculation_Configuration = id_Similarity_Calculation_Configuration
		_id_Emu_Games_A = id_Emu_Games_A
	End Sub

	Private Sub BTA_Main_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BTA_Main.CurrentChanged
		If BTA_Main.Current IsNot Nothing Then
			For Each tabpg As DevExpress.XtraTab.XtraTabPage In tcl_Main.TabPages
				Dim tabname = tabpg.Name
				Dim tabNum As Integer = CInt(tabname.Substring(4, 3))

				If tabNum = BTA_Main.Current("id") Then
					tcl_Main.SelectedTabPage = tabpg
				End If
			Next
		End If
	End Sub

	Private Sub Get_Details()
		Dim bUseMobyReleaseOnly As Boolean = False
		Dim bTestUseMobyRelease As Boolean = False
		bUseMobyReleaseOnly = (_id_Emu_Games_B = 0)

#If DEBUG Then
		'If Not bUseMobyReleaseOnly Then bTestUseMobyRelease = True
		'MKDXHelper.MessageBox("Debug: Test with id_Moby_Releases", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
#End If

		Dim id_Emu_Games_A As Integer = _id_Emu_Games_A
		If id_Emu_Games_A = 0 Then id_Emu_Games_A = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_Emu_Games FROM tbl_Similarity_Calculation_Results WHERE id_Similarity_Calculation_Results = " & TC.getSQLFormat(_id_Similarity_Calculation_Results)), 0)
		Dim id_Moby_Releases_A As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_Moby_Releases FROM tbl_Moby_Releases WHERE id_Moby_Platforms = (SELECT IFNULL(id_Moby_Platforms_Alternative, id_Moby_Platforms) FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ") AND id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = (SELECT Moby_Games_URLPart FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & "))"), 0)

		If _id_Moby_Releases_B = 0 Then
			_id_Moby_Releases_B = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_Moby_Releases FROM tbl_Moby_Releases WHERE id_Moby_Platforms = (SELECT IFNULL(id_Moby_Platforms_Alternative, id_Moby_Platforms) FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ") AND id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = (SELECT Moby_Games_URLPart FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B) & "))"), 0)
		End If

		Dim dt_Similarity_Calculation_Configuration As DataTable = Nothing
		If _id_Similarity_Calculation_Configuration <> 0 Then
			dt_Similarity_Calculation_Configuration = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT CONF.Name, [Weight_001_Platform], [Weight_002_MobyRank], [Weight_003_MobyScore], [Weight_004_Publisher], [Weight_005_Developer], [Weight_006_Year], [Weight_101_Basic_Genres], [Weight_102_Perspectives], [Weight_103_Sports_Themes], [Weight_105_Educational_Categories], [Weight_106_Other_Attributes], [Weight_107_Visual_Presentation], [Weight_108_Gameplay], [Weight_109_Pacing], [Weight_110_Narrative_Theme_Topic], [Weight_111_Setting], [Weight_112_Vehicular_Themes], [Weight_113_Interface_Control], [Weight_114_DLC_Addon], [Weight_115_Special_Edition], [Weight_201_MinPlayers], [Weight_202_MaxPlayers], [Weight_203_AgeO], [Weight_204_AgeP], [Weight_205_Rating_Descriptors], [Weight_206_Other_Attributes], [Weight_207_Multiplayer_Attributes], [Weight_301_Group_Membership], [Weight_401_Staff] FROM tbl_Similarity_Calculation_Config CONF WHERE id_Similarity_Calculation_Config = " & TC.getSQLFormat(_id_Similarity_Calculation_Configuration))
		Else
			dt_Similarity_Calculation_Configuration = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT CONF.Name, [Weight_001_Platform], [Weight_002_MobyRank], [Weight_003_MobyScore], [Weight_004_Publisher], [Weight_005_Developer], [Weight_006_Year], [Weight_101_Basic_Genres], [Weight_102_Perspectives], [Weight_103_Sports_Themes], [Weight_105_Educational_Categories], [Weight_106_Other_Attributes], [Weight_107_Visual_Presentation], [Weight_108_Gameplay], [Weight_109_Pacing], [Weight_110_Narrative_Theme_Topic], [Weight_111_Setting], [Weight_112_Vehicular_Themes], [Weight_113_Interface_Control], [Weight_114_DLC_Addon], [Weight_115_Special_Edition], [Weight_201_MinPlayers], [Weight_202_MaxPlayers], [Weight_203_AgeO], [Weight_204_AgeP], [Weight_205_Rating_Descriptors], [Weight_206_Other_Attributes], [Weight_207_Multiplayer_Attributes], [Weight_301_Group_Membership], [Weight_401_Staff] FROM tbl_Similarity_Calculation_Results RES INNER JOIN tbl_Similarity_Calculation_Config CONF ON RES.id_Similarity_Calculation_Config = CONF.id_Similarity_Calculation_Config WHERE RES.id_Similarity_Calculation_Results = " & TC.getSQLFormat(_id_Similarity_Calculation_Results))
		End If

		If dt_Similarity_Calculation_Configuration.Rows.Count <> 1 Then
			MKDXHelper.MessageBox("There has been an error while retrieving the configuration.", "Similarity Details", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		Dim row_Similarity_Calculation_Configuration As DataRow = dt_Similarity_Calculation_Configuration.Rows(0)

		lbl_Similarity_Calculation_Configuration_Text.Text = TC.NZ(row_Similarity_Calculation_Configuration("Name"), "<none>")

		lbl_Game_A_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT	CASE	WHEN EG.Name IS NULL THEN	CASE WHEN MG.Name IS NULL THEN IFNULL(EG.InnerFile, EG.File) ELSE IFNULL(EG.Name_Prefix || ' ', IFNULL(MG.Name_Prefix || ' ', '')) || IFNULL(MG.Name, '') || IFNULL(' (' || EG.Note || ')', '') END	ELSE	IFNULL(EG.Name_Prefix || ' ', '') || IFNULL(EG.Name, '') || IFNULL(' (' || EG.Note || ')', '') END || ' (' || PLTFM.Name || ')' FROM tbl_Emu_Games EG LEFT JOIN tbl_Moby_Games MG ON EG.Moby_Games_URLPart = MG.URLPart LEFT JOIN tbl_Moby_Releases MR ON IFNULL(EG.id_Moby_Platforms_Alternative, EG.id_Moby_Platforms) = MR.id_Moby_Platforms AND MG.id_Moby_Games = MR.id_Moby_Games LEFT JOIN tbl_Moby_Platforms PLTFM ON IFNULL(EG.id_Moby_Platforms_Alternative, EG.id_Moby_Platforms) = PLTFM.id_Moby_Platforms WHERE EG.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A)), "<none>")

		If bUseMobyReleaseOnly OrElse bTestUseMobyRelease Then
			lbl_Game_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT IFNULL(MG.Name_Prefix || ' ', '') || IFNULL(MG.Name, '')  || ' (' || PLTFM.Name || ')' FROM tbl_Moby_Releases MR LEFT JOIN tbl_Moby_Games MG ON MR.id_Moby_Games = MG.id_Moby_Games LEFT JOIN tbl_Moby_Platforms PLTFM ON MR.id_Moby_Platforms = PLTFM.id_Moby_Platforms WHERE id_Moby_Releases = " & TC.getSQLFormat(_id_Moby_Releases_B)), "<none>")
		Else
			lbl_Game_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT	CASE	WHEN EG.Name IS NULL THEN	CASE WHEN MG.Name IS NULL THEN IFNULL(EG.InnerFile, EG.File) ELSE IFNULL(EG.Name_Prefix || ' ', IFNULL(MG.Name_Prefix || ' ', '')) || IFNULL(MG.Name, '') || IFNULL(' (' || EG.Note || ')', '') END	ELSE	IFNULL(EG.Name_Prefix || ' ', '') || IFNULL(EG.Name, '') || IFNULL(' (' || EG.Note || ')', '') END || ' (' || PLTFM.Name || ')' FROM tbl_Emu_Games EG LEFT JOIN tbl_Moby_Games MG ON EG.Moby_Games_URLPart = MG.URLPart LEFT JOIN tbl_Moby_Releases MR ON IFNULL(EG.id_Moby_Platforms_Alternative, EG.id_Moby_Platforms) = MR.id_Moby_Platforms AND MG.id_Moby_Games = MR.id_Moby_Games LEFT JOIN tbl_Moby_Platforms PLTFM ON IFNULL(EG.id_Moby_Platforms_Alternative, EG.id_Moby_Platforms) = PLTFM.id_Moby_Platforms WHERE EG.id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B)), "<none>")
		End If

		lbl_Similarity_Text.Text = _row_Emu_Game_Similarity_Result("Similarity")

		Dim row_BTA As DataRow = Nothing

		Dim iSumWeightedScore As Integer = 0
		Dim iSumWeights As Integer = 0

		'001_Platform
		lbl_Weight_001_Platform_Text.Text = row_Similarity_Calculation_Configuration("Weight_001_Platform")
		row_BTA = BTA_Main.Table.Select("id = 001")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_001_Platform")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("001_Platform")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		lbl_001_Platform_A_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT PLTFM.Name FROM tbl_Emu_Games EG LEFT JOIN tbl_Moby_Platforms PLTFM ON IFNULL(EG.id_Moby_Platforms_Alternative, EG.id_Moby_Platforms) = PLTFM.id_Moby_Platforms WHERE EG.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A)), "<none>")
		If bUseMobyReleaseOnly OrElse bTestUseMobyRelease Then
			lbl_001_Platform_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT PLTFM.Name FROM tbl_Moby_Releases MR LEFT JOIN tbl_Moby_Games MG ON MR.id_Moby_Games = MG.id_Moby_Games LEFT JOIN tbl_Moby_Platforms PLTFM ON MR.id_Moby_Platforms = PLTFM.id_Moby_Platforms WHERE MR.id_Moby_Releases = " & TC.getSQLFormat(_id_Moby_Releases_B)), "<none>")
		Else
			lbl_001_Platform_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT PLTFM.Name FROM tbl_Emu_Games EG LEFT JOIN tbl_Moby_Platforms PLTFM ON IFNULL(EG.id_Moby_Platforms_Alternative, EG.id_Moby_Platforms) = PLTFM.id_Moby_Platforms WHERE EG.id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B)), "<none>")
		End If

		'002_MobyRank
		lbl_Weight_002_MobyRank_Text.Text = row_Similarity_Calculation_Configuration("Weight_002_MobyRank")
		row_BTA = BTA_Main.Table.Select("id = 002")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_002_MobyRank")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("002_MobyRank")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If
		lbl_002_MobyRank_A_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT	REL.MobyRank FROM	tbl_Emu_Games EMUGAME LEFT JOIN tbl_Moby_Games GAME ON EMUGAME.Moby_Games_URLPart = GAME.URLPart LEFT JOIN tbl_Moby_Releases REL ON IFNULL(EMUGAME.id_Moby_Platforms_Alternative, EMUGAME.id_Moby_Platforms) = REL.id_Moby_Platforms AND GAME.id_Moby_Games = REL.id_Moby_Games WHERE EMUGAME.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A)), "<none>")
		If bUseMobyReleaseOnly OrElse bTestUseMobyRelease Then
			lbl_002_MobyRank_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT	REL.MobyRank FROM	tbl_Moby_Releases REL WHERE id_Moby_Releases = " & TC.getSQLFormat(_id_Moby_Releases_B)), "<none>")
		Else
			lbl_002_MobyRank_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT	REL.MobyRank FROM	tbl_Emu_Games EMUGAME LEFT JOIN tbl_Moby_Games GAME ON EMUGAME.Moby_Games_URLPart = GAME.URLPart LEFT JOIN tbl_Moby_Releases REL ON IFNULL(EMUGAME.id_Moby_Platforms_Alternative, EMUGAME.id_Moby_Platforms) = REL.id_Moby_Platforms AND GAME.id_Moby_Games = REL.id_Moby_Games WHERE EMUGAME.id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B)), "<none>")
		End If

		'003_MobyScore
		lbl_Weight_003_MobyScore_Text.Text = row_Similarity_Calculation_Configuration("Weight_003_MobyScore")
		row_BTA = BTA_Main.Table.Select("id = 003")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_003_MobyScore")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("003_MobyScore")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If
		lbl_003_MobyScore_A_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT	REL.MobyScore FROM	tbl_Emu_Games EMUGAME LEFT JOIN tbl_Moby_Games GAME ON EMUGAME.Moby_Games_URLPart = GAME.URLPart LEFT JOIN tbl_Moby_Releases REL ON IFNULL(EMUGAME.id_Moby_Platforms_Alternative, EMUGAME.id_Moby_Platforms) = REL.id_Moby_Platforms AND GAME.id_Moby_Games = REL.id_Moby_Games WHERE EMUGAME.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A)), "<none>")
		If bUseMobyReleaseOnly OrElse bTestUseMobyRelease Then
			lbl_003_MobyScore_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT	REL.MobyScore FROM	tbl_Moby_Releases REL WHERE id_Moby_Releases = " & TC.getSQLFormat(_id_Moby_Releases_B)), "<none>")
		Else
			lbl_003_MobyScore_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT	REL.MobyScore FROM	tbl_Emu_Games EMUGAME LEFT JOIN tbl_Moby_Games GAME ON EMUGAME.Moby_Games_URLPart = GAME.URLPart LEFT JOIN tbl_Moby_Releases REL ON IFNULL(EMUGAME.id_Moby_Platforms_Alternative, EMUGAME.id_Moby_Platforms) = REL.id_Moby_Platforms AND GAME.id_Moby_Games = REL.id_Moby_Games WHERE EMUGAME.id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B)), "<none>")
		End If

		'004_Publisher
		lbl_Weight_004_Publisher_Text.Text = row_Similarity_Calculation_Configuration("Weight_004_Publisher")
		row_BTA = BTA_Main.Table.Select("id = 004")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_004_Publisher")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("004_Publisher")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If
		lbl_004_Publisher_A_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT	IFNULL(EMUGAME.Publisher, C1.Name) FROM	tbl_Emu_Games EMUGAME LEFT JOIN tbl_Moby_Games GAME ON EMUGAME.Moby_Games_URLPart = GAME.URLPart LEFT JOIN tbl_Moby_Releases REL ON IFNULL(EMUGAME.id_Moby_Platforms_Alternative, EMUGAME.id_Moby_Platforms) = REL.id_Moby_Platforms AND GAME.id_Moby_Games = REL.id_Moby_Games LEFT JOIN tbl_Moby_Companies C1 ON REL.Publisher_id_Moby_Companies = C1.id_Moby_Companies WHERE EMUGAME.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A)), "<none>")
		If bUseMobyReleaseOnly OrElse bTestUseMobyRelease Then
			lbl_004_Publisher_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT	C1.Name FROM	tbl_Moby_Releases REL LEFT JOIN tbl_Moby_Companies C1 ON REL.Publisher_id_Moby_Companies = C1.id_Moby_Companies WHERE id_Moby_Releases = " & TC.getSQLFormat(_id_Moby_Releases_B)), "<none>")
		Else
			lbl_004_Publisher_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT	IFNULL(EMUGAME.Publisher, C1.Name) FROM	tbl_Emu_Games EMUGAME LEFT JOIN tbl_Moby_Games GAME ON EMUGAME.Moby_Games_URLPart = GAME.URLPart LEFT JOIN tbl_Moby_Releases REL ON IFNULL(EMUGAME.id_Moby_Platforms_Alternative, EMUGAME.id_Moby_Platforms) = REL.id_Moby_Platforms AND GAME.id_Moby_Games = REL.id_Moby_Games LEFT JOIN tbl_Moby_Companies C1 ON REL.Publisher_id_Moby_Companies = C1.id_Moby_Companies WHERE EMUGAME.id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B)), "<none>")
		End If

		'005_Developer
		lbl_Weight_005_Developer_Text.Text = row_Similarity_Calculation_Configuration("Weight_005_Developer")
		row_BTA = BTA_Main.Table.Select("id = 005")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_005_Developer")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("005_Developer")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If
		lbl_005_Developer_A_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT	IFNULL(EMUGAME.Developer, C2.Name) FROM	tbl_Emu_Games EMUGAME LEFT JOIN tbl_Moby_Games GAME ON EMUGAME.Moby_Games_URLPart = GAME.URLPart LEFT JOIN tbl_Moby_Releases REL ON IFNULL(EMUGAME.id_Moby_Platforms_Alternative, EMUGAME.id_Moby_Platforms) = REL.id_Moby_Platforms AND GAME.id_Moby_Games = REL.id_Moby_Games LEFT JOIN tbl_Moby_Companies C2 ON REL.Developer_id_Moby_Companies = C2.id_Moby_Companies WHERE EMUGAME.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A)), "<none>")
		If bUseMobyReleaseOnly OrElse bTestUseMobyRelease Then
			lbl_005_Developer_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT	C2.Name FROM	tbl_Moby_Releases REL LEFT JOIN tbl_Moby_Companies C2 ON REL.Developer_id_Moby_Companies = C2.id_Moby_Companies WHERE id_Moby_Releases = " & TC.getSQLFormat(_id_Moby_Releases_B)), "<none>")
		Else
			lbl_005_Developer_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT	IFNULL(EMUGAME.Developer, C2.Name) FROM	tbl_Emu_Games EMUGAME LEFT JOIN tbl_Moby_Games GAME ON EMUGAME.Moby_Games_URLPart = GAME.URLPart LEFT JOIN tbl_Moby_Releases REL ON IFNULL(EMUGAME.id_Moby_Platforms_Alternative, EMUGAME.id_Moby_Platforms) = REL.id_Moby_Platforms AND GAME.id_Moby_Games = REL.id_Moby_Games LEFT JOIN tbl_Moby_Companies C2 ON REL.Developer_id_Moby_Companies = C2.id_Moby_Companies WHERE EMUGAME.id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B)), "<none>")
		End If

		'006_Year
		lbl_Weight_006_Year_Text.Text = row_Similarity_Calculation_Configuration("Weight_006_Year")
		row_BTA = BTA_Main.Table.Select("id = 006")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_006_Year")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("006_Year")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If
		lbl_006_Year_A_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT	IFNULL(EMUGAME.Year, REL.Year) FROM	tbl_Emu_Games EMUGAME LEFT JOIN tbl_Moby_Games GAME ON EMUGAME.Moby_Games_URLPart = GAME.URLPart LEFT JOIN tbl_Moby_Releases REL ON IFNULL(EMUGAME.id_Moby_Platforms_Alternative, EMUGAME.id_Moby_Platforms) = REL.id_Moby_Platforms AND GAME.id_Moby_Games = REL.id_Moby_Games WHERE EMUGAME.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A)), "<none>")
		If bUseMobyReleaseOnly OrElse bTestUseMobyRelease Then
			lbl_006_Year_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT	REL.Year FROM	tbl_Moby_Releases REL WHERE id_Moby_Releases = " & TC.getSQLFormat(_id_Moby_Releases_B)), "<none>")
		Else
			lbl_006_Year_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT	IFNULL(EMUGAME.Year, REL.Year) FROM	tbl_Emu_Games EMUGAME LEFT JOIN tbl_Moby_Games GAME ON EMUGAME.Moby_Games_URLPart = GAME.URLPart LEFT JOIN tbl_Moby_Releases REL ON IFNULL(EMUGAME.id_Moby_Platforms_Alternative, EMUGAME.id_Moby_Platforms) = REL.id_Moby_Platforms AND GAME.id_Moby_Games = REL.id_Moby_Games WHERE EMUGAME.id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B)), "<none>")
		End If

		'101_Basic_Genres
		row_BTA = BTA_Main.Table.Select("id = 101")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_101_Basic_Genres")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("101_Basic_Genres")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		Me.ucr_101_Basic_Genres.Init(cls_Globals.enm_Moby_Genres_Categories.Basic_Genres, "Basic Genres", row_Similarity_Calculation_Configuration("Weight_101_Basic_Genres"), bUseMobyReleaseOnly, bTestUseMobyRelease, id_Emu_Games_A, _id_Emu_Games_B, _id_Moby_Releases_B)

		'102_Perspectives
		row_BTA = BTA_Main.Table.Select("id = 102")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_102_Perspectives")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("102_Perspectives")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		Me.ucr_102_Perspectives.Init(cls_Globals.enm_Moby_Genres_Categories.Perspective, "Perspectives", row_Similarity_Calculation_Configuration("Weight_102_Perspectives"), bUseMobyReleaseOnly, bTestUseMobyRelease, id_Emu_Games_A, _id_Emu_Games_B, _id_Moby_Releases_B)

		'103_Sports_Themes
		row_BTA = BTA_Main.Table.Select("id = 103")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_103_Sports_Themes")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("103_Sports_Themes")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		Me.ucr_103_Sports_Themes.Init(cls_Globals.enm_Moby_Genres_Categories.Sports_Themes, "Sports Themes", row_Similarity_Calculation_Configuration("Weight_103_Sports_Themes"), bUseMobyReleaseOnly, bTestUseMobyRelease, id_Emu_Games_A, _id_Emu_Games_B, _id_Moby_Releases_B)

		'105_Educational_Categories
		row_BTA = BTA_Main.Table.Select("id = 105")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_105_Educational_Categories")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("105_Educational_Categories")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		Me.ucr_105_Educational_Categories.Init(cls_Globals.enm_Moby_Genres_Categories.Educational_Categories, "Educational Categories", row_Similarity_Calculation_Configuration("Weight_105_Educational_Categories"), bUseMobyReleaseOnly, bTestUseMobyRelease, id_Emu_Games_A, _id_Emu_Games_B, _id_Moby_Releases_B)

		'106_Other_Attributes
		row_BTA = BTA_Main.Table.Select("id = 106")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_106_Other_Attributes")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("106_Other_Attributes")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		Me.ucr_106_Other_Attributes.Init(cls_Globals.enm_Moby_Genres_Categories.Other_Attributes, "Other Attributes", row_Similarity_Calculation_Configuration("Weight_106_Other_Attributes"), bUseMobyReleaseOnly, bTestUseMobyRelease, id_Emu_Games_A, _id_Emu_Games_B, _id_Moby_Releases_B)

		'107_Visual_Presentation
		row_BTA = BTA_Main.Table.Select("id = 107")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_107_Visual_Presentation")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("107_Visual_Presentation")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		Me.ucr_107_Visual_Presentation.Init(cls_Globals.enm_Moby_Genres_Categories.Visual_Presentation, "Visual Presentations", row_Similarity_Calculation_Configuration("Weight_107_Visual_Presentation"), bUseMobyReleaseOnly, bTestUseMobyRelease, id_Emu_Games_A, _id_Emu_Games_B, _id_Moby_Releases_B)

		'108_Gameplay
		row_BTA = BTA_Main.Table.Select("id = 108")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_108_Gameplay")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("108_Gameplay")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		Me.ucr_108_Gameplay.Init(cls_Globals.enm_Moby_Genres_Categories.Gameplay, "Gameplay Attributes", row_Similarity_Calculation_Configuration("Weight_108_Gameplay"), bUseMobyReleaseOnly, bTestUseMobyRelease, id_Emu_Games_A, _id_Emu_Games_B, _id_Moby_Releases_B)

		'109_Pacing
		row_BTA = BTA_Main.Table.Select("id = 109")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_109_Pacing")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("109_Pacing")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		Me.ucr_109_Pacing.Init(cls_Globals.enm_Moby_Genres_Categories.Pacing, "Pacing Attributes", row_Similarity_Calculation_Configuration("Weight_109_Pacing"), bUseMobyReleaseOnly, bTestUseMobyRelease, id_Emu_Games_A, _id_Emu_Games_B, _id_Moby_Releases_B)

		'110_Narrative_Theme_Topic
		row_BTA = BTA_Main.Table.Select("id = 110")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_110_Narrative_Theme_Topic")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("110_Narrative_Theme_Topic")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		Me.ucr_110_Narrative_Theme_Topic.Init(cls_Globals.enm_Moby_Genres_Categories.Narrative_Theme_Topic, "Narrative Themes / Topics", row_Similarity_Calculation_Configuration("Weight_110_Narrative_Theme_Topic"), bUseMobyReleaseOnly, bTestUseMobyRelease, id_Emu_Games_A, _id_Emu_Games_B, _id_Moby_Releases_B)

		'111_Setting
		row_BTA = BTA_Main.Table.Select("id = 111")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_111_Setting")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("111_Setting")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		Me.ucr_111_Setting.Init(cls_Globals.enm_Moby_Genres_Categories.Setting, "Setting Attributes", row_Similarity_Calculation_Configuration("Weight_111_Setting"), bUseMobyReleaseOnly, bTestUseMobyRelease, id_Emu_Games_A, _id_Emu_Games_B, _id_Moby_Releases_B)

		'112_Vehicular_Themes
		row_BTA = BTA_Main.Table.Select("id = 112")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_112_Vehicular_Themes")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("112_Vehicular_Themes")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		Me.ucr_112_Vehicular_Themes.Init(cls_Globals.enm_Moby_Genres_Categories.Vehicular_Themes, "Vehicular Themes", row_Similarity_Calculation_Configuration("Weight_112_Vehicular_Themes"), bUseMobyReleaseOnly, bTestUseMobyRelease, id_Emu_Games_A, _id_Emu_Games_B, _id_Moby_Releases_B)

		'113_Interface_Control
		row_BTA = BTA_Main.Table.Select("id = 113")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_113_Interface_Control")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("113_Interface_Control")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		Me.ucr_113_Interface_Control.Init(cls_Globals.enm_Moby_Genres_Categories.Interface_Control, "Interface / Control Attributes", row_Similarity_Calculation_Configuration("Weight_113_Interface_Control"), bUseMobyReleaseOnly, bTestUseMobyRelease, id_Emu_Games_A, _id_Emu_Games_B, _id_Moby_Releases_B)

		'114 DLC / Add-on
		row_BTA = BTA_Main.Table.Select("id = 114")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_114_DLC_Addon")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("114_DLC_Addon")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		Me.ucr_114_DLC_Addon.Init(cls_Globals.enm_Moby_Genres_Categories.DLC_Addon, "DLC / Add-On Attributes", row_Similarity_Calculation_Configuration("Weight_114_DLC_Addon"), bUseMobyReleaseOnly, bTestUseMobyRelease, id_Emu_Games_A, _id_Emu_Games_B, _id_Moby_Releases_B)

		'115 Special Edition
		row_BTA = BTA_Main.Table.Select("id = 115")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_115_Special_Edition")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("115_Special_Edition")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		Me.ucr_115_Special_Edition.Init(cls_Globals.enm_Moby_Genres_Categories.Special_Edition, "Special Edition Attributes", row_Similarity_Calculation_Configuration("Weight_115_Special_Edition"), bUseMobyReleaseOnly, bTestUseMobyRelease, id_Emu_Games_A, _id_Emu_Games_B, _id_Moby_Releases_B)

		'201_MinPlayers
		lbl_Weight_201_MinPlayers_Text.Text = row_Similarity_Calculation_Configuration("Weight_201_MinPlayers")
		row_BTA = BTA_Main.Table.Select("id = 201")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_201_MinPlayers")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("201_MinPlayers")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		lbl_201_MinPlayers_A_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT MIN(A.MinPlayers) FROM (SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA WHERE RA.id_Moby_Releases = (SELECT id_Moby_Releases FROM tbl_Moby_Releases WHERE id_Moby_Platforms = (SELECT IFNULL(id_Moby_Platforms_Alternative, id_Moby_Platforms) FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ") AND id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = (SELECT Moby_Games_URLPart FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & "))) UNION SELECT id_Moby_Attributes FROM tbl_Emu_Games_Moby_Attributes EGMA WHERE EGMA.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ") AS temp_Attributes LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes WHERE A.id_Moby_Attributes NOT IN(SELECT id_Moby_Attributes FROM tbl_Emu_Games_Moby_Attributes WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & " AND Used = 0)"), "<none>")
		If bUseMobyReleaseOnly OrElse bTestUseMobyRelease Then
			lbl_201_MinPlayers_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT MIN(A.MinPlayers) FROM	(SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA WHERE RA.id_Moby_Releases = " & TC.getSQLFormat(_id_Moby_Releases_B) & ") AS temp_Attributes LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes"), "<none>")
		Else
			lbl_201_MinPlayers_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT MIN(A.MinPlayers) FROM (SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA WHERE RA.id_Moby_Releases = (SELECT id_Moby_Releases FROM tbl_Moby_Releases WHERE id_Moby_Platforms = (SELECT IFNULL(id_Moby_Platforms_Alternative, id_Moby_Platforms) FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B) & ") AND id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = (SELECT Moby_Games_URLPart FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B) & "))) UNION SELECT id_Moby_Attributes FROM tbl_Emu_Games_Moby_Attributes EGMA WHERE EGMA.id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B) & ") AS temp_Attributes LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes WHERE A.id_Moby_Attributes NOT IN(SELECT id_Moby_Attributes FROM tbl_Emu_Games_Moby_Attributes WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B) & " AND Used = 0)"), "<none>")
		End If

		'202_MaxPlayers
		lbl_Weight_202_MaxPlayers_Text.Text = row_Similarity_Calculation_Configuration("Weight_202_MaxPlayers")
		row_BTA = BTA_Main.Table.Select("id = 202")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_202_MaxPlayers")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("202_MaxPlayers")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		lbl_202_MaxPlayers_A_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT MAX(A.MaxPlayers) FROM (SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA WHERE RA.id_Moby_Releases = (SELECT id_Moby_Releases FROM tbl_Moby_Releases WHERE id_Moby_Platforms = (SELECT IFNULL(id_Moby_Platforms_Alternative, id_Moby_Platforms) FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ") AND id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = (SELECT Moby_Games_URLPart FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & "))) UNION SELECT id_Moby_Attributes FROM tbl_Emu_Games_Moby_Attributes EGMA WHERE EGMA.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ") AS temp_Attributes LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes WHERE A.id_Moby_Attributes NOT IN(SELECT id_Moby_Attributes FROM tbl_Emu_Games_Moby_Attributes WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & " AND Used = 0)"), "<none>")
		If bUseMobyReleaseOnly OrElse bTestUseMobyRelease Then
			lbl_202_MaxPlayers_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT MAX(A.MaxPlayers) FROM	(SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA WHERE RA.id_Moby_Releases = " & TC.getSQLFormat(_id_Moby_Releases_B) & ") AS temp_Attributes LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes"), "<none>")
		Else
			lbl_202_MaxPlayers_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT MAX(A.MaxPlayers) FROM (SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA WHERE RA.id_Moby_Releases = (SELECT id_Moby_Releases FROM tbl_Moby_Releases WHERE id_Moby_Platforms = (SELECT IFNULL(id_Moby_Platforms_Alternative, id_Moby_Platforms) FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B) & ") AND id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = (SELECT Moby_Games_URLPart FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B) & "))) UNION SELECT id_Moby_Attributes FROM tbl_Emu_Games_Moby_Attributes EGMA WHERE EGMA.id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B) & ") AS temp_Attributes LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes WHERE A.id_Moby_Attributes NOT IN(SELECT id_Moby_Attributes FROM tbl_Emu_Games_Moby_Attributes WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B) & " AND Used = 0)"), "<none>")
		End If

		'203_AgeO
		lbl_Weight_203_AgeO_Text.Text = row_Similarity_Calculation_Configuration("Weight_203_AgeO")
		row_BTA = BTA_Main.Table.Select("id = 203")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_203_AgeO")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("203_AgeO")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		lbl_203_AgeO_A_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT MIN(A.Rating_Age_From) FROM (SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA WHERE RA.id_Moby_Releases = (SELECT id_Moby_Releases FROM tbl_Moby_Releases WHERE id_Moby_Platforms = (SELECT IFNULL(id_Moby_Platforms_Alternative, id_Moby_Platforms) FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ") AND id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = (SELECT Moby_Games_URLPart FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & "))) UNION SELECT id_Moby_Attributes FROM tbl_Emu_Games_Moby_Attributes EGMA WHERE EGMA.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ") AS temp_Attributes LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes WHERE A.id_Moby_Attributes NOT IN(SELECT id_Moby_Attributes FROM tbl_Emu_Games_Moby_Attributes WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & " AND Used = 0)"), "<none>")
		If bUseMobyReleaseOnly OrElse bTestUseMobyRelease Then
			lbl_203_AgeO_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT MIN(A.Rating_Age_From) FROM	(SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA WHERE RA.id_Moby_Releases = " & TC.getSQLFormat(_id_Moby_Releases_B) & ") AS temp_Attributes LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes"), "<none>")
		Else
			lbl_203_AgeO_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT MIN(A.Rating_Age_From) FROM (SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA WHERE RA.id_Moby_Releases = (SELECT id_Moby_Releases FROM tbl_Moby_Releases WHERE id_Moby_Platforms = (SELECT IFNULL(id_Moby_Platforms_Alternative, id_Moby_Platforms) FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B) & ") AND id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = (SELECT Moby_Games_URLPart FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B) & "))) UNION SELECT id_Moby_Attributes FROM tbl_Emu_Games_Moby_Attributes EGMA WHERE EGMA.id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B) & ") AS temp_Attributes LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes WHERE A.id_Moby_Attributes NOT IN(SELECT id_Moby_Attributes FROM tbl_Emu_Games_Moby_Attributes WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B) & " AND Used = 0)"), "<none>")
		End If

		'204_AgeP
		lbl_Weight_204_AgeP_Text.Text = row_Similarity_Calculation_Configuration("Weight_204_AgeP")
		row_BTA = BTA_Main.Table.Select("id = 204")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_204_AgeP")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("204_AgeP")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		lbl_204_AgeP_A_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT MAX(A.Rating_Age_From) FROM (SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA WHERE RA.id_Moby_Releases = (SELECT id_Moby_Releases FROM tbl_Moby_Releases WHERE id_Moby_Platforms = (SELECT IFNULL(id_Moby_Platforms_Alternative, id_Moby_Platforms) FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ") AND id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = (SELECT Moby_Games_URLPart FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & "))) UNION SELECT id_Moby_Attributes FROM tbl_Emu_Games_Moby_Attributes EGMA WHERE EGMA.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & ") AS temp_Attributes LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes WHERE A.id_Moby_Attributes NOT IN(SELECT id_Moby_Attributes FROM tbl_Emu_Games_Moby_Attributes WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games_A) & " AND Used = 0)"), "<none>")
		If bUseMobyReleaseOnly OrElse bTestUseMobyRelease Then
			lbl_204_AgeP_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT MAX(A.Rating_Age_From) FROM	(SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA WHERE RA.id_Moby_Releases = " & TC.getSQLFormat(_id_Moby_Releases_B) & ") AS temp_Attributes LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes"), "<none>")
		Else
			lbl_204_AgeP_B_Text.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT MAX(A.Rating_Age_From) FROM (SELECT id_Moby_Attributes FROM tbl_Moby_Releases_Attributes RA WHERE RA.id_Moby_Releases = (SELECT id_Moby_Releases FROM tbl_Moby_Releases WHERE id_Moby_Platforms = (SELECT IFNULL(id_Moby_Platforms_Alternative, id_Moby_Platforms) FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B) & ") AND id_Moby_Games = (SELECT id_Moby_Games FROM moby.tbl_Moby_Games MG WHERE MG.URLPart = (SELECT Moby_Games_URLPart FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B) & "))) UNION SELECT id_Moby_Attributes FROM tbl_Emu_Games_Moby_Attributes EGMA WHERE EGMA.id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B) & ") AS temp_Attributes LEFT JOIN tbl_Moby_Attributes A ON temp_Attributes.id_Moby_Attributes = A.id_Moby_Attributes WHERE A.id_Moby_Attributes NOT IN(SELECT id_Moby_Attributes FROM tbl_Emu_Games_Moby_Attributes WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games_B) & " AND Used = 0)"), "<none>")
		End If

		'205_Rating_Descriptors
		lbl_Weight_205_Rating_Descriptors_Text.Text = row_Similarity_Calculation_Configuration("Weight_205_Rating_Descriptors")
		row_BTA = BTA_Main.Table.Select("id = 205")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_205_Rating_Descriptors")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("205_Rating_Descriptors")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		grd_205_Rating_Descriptors_A.DataSource = DS_ML.Select_Attributes_By_id_Emu_Games(id_Emu_Games_A, DS_ML.enm_Attributes_Types.Rating_Descriptors)

		If bUseMobyReleaseOnly OrElse bTestUseMobyRelease Then
			grd_205_Rating_Descriptors_B.DataSource = DS_ML.Select_Attributes_By_id_Moby_Releases(_id_Moby_Releases_B, DS_ML.enm_Attributes_Types.Rating_Descriptors)
			grd_205_Rating_Descriptors_AB.DataSource = DS_ML.Select_Attributes_AB_By_Moby_Releases(id_Emu_Games_A, _id_Moby_Releases_B, DS_ML.enm_Attributes_Types.Rating_Descriptors)
		Else
			grd_205_Rating_Descriptors_B.DataSource = DS_ML.Select_Attributes_By_id_Emu_Games(_id_Emu_Games_B, DS_ML.enm_Attributes_Types.Rating_Descriptors)
			grd_205_Rating_Descriptors_AB.DataSource = DS_ML.Select_Attributes_AB_By_id_Emu_Games(id_Emu_Games_A, _id_Emu_Games_B, DS_ML.enm_Attributes_Types.Rating_Descriptors)
		End If

		gb_205_Rating_Descriptors_A.Text &= " (" & grd_205_Rating_Descriptors_A.DataSource.Rows.Count & ")"
		gb_205_Rating_Descriptors_B.Text &= " (" & grd_205_Rating_Descriptors_B.DataSource.Rows.Count & ")"
		gb_205_Rating_Descriptors_AB.Text &= " (" & grd_205_Rating_Descriptors_AB.DataSource.Rows.Count & ")"

		'206_Other_Attributes (TechInfo)
		lbl_Weight_206_Other_Attributes_Text.Text = row_Similarity_Calculation_Configuration("Weight_206_Other_Attributes")
		row_BTA = BTA_Main.Table.Select("id = 206")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_206_Other_Attributes")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("206_Other_Attributes")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		grd_206_Other_Attributes_A.DataSource = DS_ML.Select_Attributes_By_id_Emu_Games(id_Emu_Games_A, DS_ML.enm_Attributes_Types.Other_Attributes)

		If bUseMobyReleaseOnly OrElse bTestUseMobyRelease Then
			grd_206_Other_Attributes_B.DataSource = DS_ML.Select_Attributes_By_id_Moby_Releases(_id_Moby_Releases_B, DS_ML.enm_Attributes_Types.Other_Attributes)
			grd_206_Other_Attributes_AB.DataSource = DS_ML.Select_Attributes_AB_By_Moby_Releases(id_Emu_Games_A, _id_Moby_Releases_B, DS_ML.enm_Attributes_Types.Other_Attributes)
		Else
			grd_206_Other_Attributes_B.DataSource = DS_ML.Select_Attributes_By_id_Emu_Games(_id_Emu_Games_B, DS_ML.enm_Attributes_Types.Other_Attributes)
			grd_206_Other_Attributes_AB.DataSource = DS_ML.Select_Attributes_AB_By_id_Emu_Games(id_Emu_Games_A, _id_Emu_Games_B, DS_ML.enm_Attributes_Types.Other_Attributes)
		End If

		gb_206_Other_Attributes_A.Text &= " (" & grd_206_Other_Attributes_A.DataSource.Rows.Count & ")"
		gb_206_Other_Attributes_B.Text &= " (" & grd_206_Other_Attributes_B.DataSource.Rows.Count & ")"
		gb_206_Other_Attributes_AB.Text &= " (" & grd_206_Other_Attributes_AB.DataSource.Rows.Count & ")"

		'207_Multiplayer_Attributes
		lbl_Weight_207_Multiplayer_Attributes_Text.Text = row_Similarity_Calculation_Configuration("Weight_207_Multiplayer_Attributes")
		row_BTA = BTA_Main.Table.Select("id = 207")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_207_Multiplayer_Attributes")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("207_Multiplayer_Attributes")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		grd_207_Multiplayer_Attributes_A.DataSource = DS_ML.Select_Attributes_By_id_Emu_Games(id_Emu_Games_A, DS_ML.enm_Attributes_Types.Multiplayer_Attributes)

		If bUseMobyReleaseOnly OrElse bTestUseMobyRelease Then
			grd_207_Multiplayer_Attributes_B.DataSource = DS_ML.Select_Attributes_By_id_Moby_Releases(_id_Moby_Releases_B, DS_ML.enm_Attributes_Types.Multiplayer_Attributes)
			grd_207_Multiplayer_Attributes_AB.DataSource = DS_ML.Select_Attributes_AB_By_Moby_Releases(id_Emu_Games_A, _id_Moby_Releases_B, DS_ML.enm_Attributes_Types.Multiplayer_Attributes)
		Else
			grd_207_Multiplayer_Attributes_B.DataSource = DS_ML.Select_Attributes_By_id_Emu_Games(_id_Emu_Games_B, DS_ML.enm_Attributes_Types.Multiplayer_Attributes)
			grd_207_Multiplayer_Attributes_AB.DataSource = DS_ML.Select_Attributes_AB_By_id_Emu_Games(id_Emu_Games_A, _id_Emu_Games_B, DS_ML.enm_Attributes_Types.Multiplayer_Attributes)
		End If

		gb_207_Multiplayer_Attributes_A.Text &= " (" & grd_207_Multiplayer_Attributes_A.DataSource.Rows.Count & ")"
		gb_207_Multiplayer_Attributes_B.Text &= " (" & grd_207_Multiplayer_Attributes_B.DataSource.Rows.Count & ")"
		gb_207_Multiplayer_Attributes_AB.Text &= " (" & grd_207_Multiplayer_Attributes_AB.DataSource.Rows.Count & ")"

		'301_Group_Membership
		lbl_Weight_301_Group_Membership_Text.Text = row_Similarity_Calculation_Configuration("Weight_301_Group_Membership")
		row_BTA = BTA_Main.Table.Select("id = 301")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_301_Group_Membership")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("301_Group_Membership")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		grd_301_Group_Membership_A.DataSource = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT	MGG.Name AS Name FROM moby.tbl_Moby_Game_Groups_Moby_Releases MGGMR INNER JOIN moby.tbl_Moby_Game_Groups MGG ON MGGMR.id_Moby_Game_Groups = MGG.id_Moby_Game_Groups WHERE MGGMR.id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases_A) & " ORDER BY MGG.Name")

		grd_301_Group_Membership_B.DataSource = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT	MGG.Name AS Name FROM moby.tbl_Moby_Game_Groups_Moby_Releases MGGMR INNER JOIN moby.tbl_Moby_Game_Groups MGG ON MGGMR.id_Moby_Game_Groups = MGG.id_Moby_Game_Groups WHERE MGGMR.id_Moby_Releases = " & TC.getSQLFormat(_id_Moby_Releases_B) & " ORDER BY MGG.Name")
		grd_301_Group_Membership_AB.DataSource = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT	MGG.Name AS Name FROM moby.tbl_Moby_Game_Groups_Moby_Releases MGGMR INNER JOIN moby.tbl_Moby_Game_Groups MGG ON MGGMR.id_Moby_Game_Groups = MGG.id_Moby_Game_Groups WHERE MGGMR.id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases_A) & " AND MGGMR.id_Moby_Game_Groups IN (SELECT id_Moby_Game_Groups FROM moby.tbl_Moby_Game_Groups_Moby_Releases WHERE id_Moby_Releases = " & TC.getSQLFormat(_id_Moby_Releases_B) & ") ORDER BY MGG.Name")

		gb_301_Group_Membership_A.Text &= " (" & grd_301_Group_Membership_A.DataSource.Rows.Count & ")"
		gb_301_Group_Membership_B.Text &= " (" & grd_301_Group_Membership_B.DataSource.Rows.Count & ")"
		gb_301_Group_Membership_AB.Text &= " (" & grd_301_Group_Membership_AB.DataSource.Rows.Count & ")"

		'401_Staff
		lbl_Weight_401_Staff_Text.Text = row_Similarity_Calculation_Configuration("Weight_401_Staff")
		row_BTA = BTA_Main.Table.Select("id = 401")(0)
		row_BTA("Weight") = row_Similarity_Calculation_Configuration("Weight_401_Staff")
		row_BTA("Score") = _row_Emu_Game_Similarity_Result("401_Staff")
		If IsNumeric(row_BTA("Weight")) AndAlso IsNumeric(row_BTA("Score")) Then
			row_BTA("Weighted_Score") = row_BTA("Weight") * row_BTA("Score")
			iSumWeights += 100 * row_BTA("Weight")
			iSumWeightedScore += row_BTA("Weighted_Score")
		End If

		grd_401_Staff_A.DataSource = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT DISTINCT	MS.Name AS Name FROM moby.tbl_Moby_Releases_Staff MRS INNER JOIN moby.tbl_Moby_Staff MS ON MRS.id_Moby_Staff = MS.id_Moby_Staff WHERE MRS.id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases_A) & " ORDER BY MS.Name")

		grd_401_Staff_B.DataSource = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT DISTINCT	MS.Name AS Name FROM moby.tbl_Moby_Releases_Staff MRS INNER JOIN moby.tbl_Moby_Staff MS ON MRS.id_Moby_Staff = MS.id_Moby_Staff WHERE MRS.id_Moby_Releases = " & TC.getSQLFormat(_id_Moby_Releases_B) & " ORDER BY MS.Name")
		grd_401_Staff_AB.DataSource = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT DISTINCT	MS.Name AS Name FROM moby.tbl_Moby_Releases_Staff MRS INNER JOIN moby.tbl_Moby_Staff MS ON MRS.id_Moby_Staff = MS.id_Moby_Staff WHERE MRS.id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases_A) & " AND MRS.id_Moby_Staff IN (SELECT id_Moby_Staff FROM moby.tbl_Moby_Releases_Staff WHERE id_Moby_Releases = " & TC.getSQLFormat(_id_Moby_Releases_B) & ") ORDER BY MS.Name")

		gb_401_Staff_A.Text &= " (" & grd_401_Staff_A.DataSource.Rows.Count & ")"
		gb_401_Staff_B.Text &= " (" & grd_401_Staff_B.DataSource.Rows.Count & ")"
		gb_401_Staff_AB.Text &= " (" & grd_401_Staff_AB.DataSource.Rows.Count & ")"
	End Sub

	Private Sub frm_Similarity_Calculation_Details_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		Get_Details()
	End Sub

	Private _Sum_Weights As Integer = 0

	Private Sub gv_Main_CustomSummaryCalculate(ByVal sender As System.Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles gv_Main.CustomSummaryCalculate
		If e.IsTotalSummary = True And e.Item Is Me.colWeight.SummaryItem Then
			If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Start Then
				_Sum_Weights = 0
			End If

			If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
				If IsNumeric(e.FieldValue) AndAlso IsNumeric(e.Row("Score")) Then
					_Sum_Weights += 100 * e.FieldValue
				End If
			End If

			If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Finalize Then
				e.TotalValue = _Sum_Weights
			End If
		End If
	End Sub
End Class