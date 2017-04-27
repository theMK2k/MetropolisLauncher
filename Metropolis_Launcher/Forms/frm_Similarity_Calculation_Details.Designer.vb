<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Similarity_Calculation_Details
	Inherits MKNetDXLib.frm_MKDXBaseForm

	'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Wird vom Windows Form-Designer benötigt.
	Private components As System.ComponentModel.IContainer

	'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
	'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
	'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Similarity_Calculation_Details))
		Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
		Dim ToolTipTitleItem2 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
		Dim ToolTipItem2 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
		Me.splt_Main = New MKNetDXLib.ctl_MKDXSplitPanel()
		Me.grd_Main = New MKNetDXLib.ctl_MKDXGrid()
		Me.BTA_Main = New MKNetLib.cmp_MKBindableTableAdapter(Me.components)
		Me.gv_Main = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.colFeature = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colScore = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colWeight = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.colWeighted_Score = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.tcl_Main = New MKNetDXLib.ctl_MKDXTabControl()
		Me.tpg_001_Platform = New DevExpress.XtraTab.XtraTabPage()
		Me.pnl_001_Platform = New MKNetDXLib.ctl_MKDXPanel()
		Me.pnl_001_Platform_Details = New MKNetDXLib.ctl_MKDXPanel()
		Me.lbl_Weight_001_Platform = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_001_Platform_A = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_001_Platform_B = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Weight_001_Platform_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_001_Platform_A_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_001_Platform_B_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_001_Platform_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		Me.tpg_002_MobyRank = New DevExpress.XtraTab.XtraTabPage()
		Me.pnl_002_MobyRank = New MKNetDXLib.ctl_MKDXPanel()
		Me.pnl_002_MobyRank_Details = New MKNetDXLib.ctl_MKDXPanel()
		Me.lbl_Weight_002_MobyRank = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_002_MobyRank_A = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_002_MobyRank_B = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Weight_002_MobyRank_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_002_MobyRank_A_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_002_MobyRank_B_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_002_MobyRank_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		Me.tpg_003_MobyScore = New DevExpress.XtraTab.XtraTabPage()
		Me.pnl_003_MobyScore = New MKNetDXLib.ctl_MKDXPanel()
		Me.pnl_003_MobyScore_Details = New MKNetDXLib.ctl_MKDXPanel()
		Me.lbl_Weight_003_MobyScore = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_003_MobyScore_A = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_003_MobyScore_B = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Weight_003_MobyScore_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_003_MobyScore_A_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_003_MobyScore_B_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_003_MobyScore_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		Me.tpg_004_Publisher = New DevExpress.XtraTab.XtraTabPage()
		Me.pnl_004_Publisher = New MKNetDXLib.ctl_MKDXPanel()
		Me.pnl_004_Publisher_Details = New MKNetDXLib.ctl_MKDXPanel()
		Me.lbl_Weight_004_Publisher = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_004_Publisher_A = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_004_Publisher_B = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Weight_004_Publisher_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_004_Publisher_A_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_004_Publisher_B_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_004_Publisher_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		Me.tpg_005_Developer = New DevExpress.XtraTab.XtraTabPage()
		Me.pnl_005_Developer = New MKNetDXLib.ctl_MKDXPanel()
		Me.pnl_005_Developer_Details = New MKNetDXLib.ctl_MKDXPanel()
		Me.lbl_Weight_005_Developer = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_005_Developer_A = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_005_Developer_B = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Weight_005_Developer_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_005_Developer_A_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_005_Developer_B_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_005_Developer_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		Me.tpg_006_Year = New DevExpress.XtraTab.XtraTabPage()
		Me.pnl_006_Year = New MKNetDXLib.ctl_MKDXPanel()
		Me.pnl_006_Year_Details = New MKNetDXLib.ctl_MKDXPanel()
		Me.lbl_Weight_006_Year = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_006_Year_A = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_006_Year_B = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Weight_006_Year_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_006_Year_A_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_006_Year_B_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_006_Year_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		Me.tpg_101_Basic_Genres = New DevExpress.XtraTab.XtraTabPage()
		Me.ucr_101_Basic_Genres = New Metropolis_Launcher.ucr_Similarity_Calculation_Details_Genre()
		Me.tpg_102_Perspectives = New DevExpress.XtraTab.XtraTabPage()
		Me.ucr_102_Perspectives = New Metropolis_Launcher.ucr_Similarity_Calculation_Details_Genre()
		Me.tpg_107_Visual_Presentation = New DevExpress.XtraTab.XtraTabPage()
		Me.ucr_107_Visual_Presentation = New Metropolis_Launcher.ucr_Similarity_Calculation_Details_Genre()
		Me.tpg_108_Gameplay = New DevExpress.XtraTab.XtraTabPage()
		Me.ucr_108_Gameplay = New Metropolis_Launcher.ucr_Similarity_Calculation_Details_Genre()
		Me.tpg_109_Pacing = New DevExpress.XtraTab.XtraTabPage()
		Me.ucr_109_Pacing = New Metropolis_Launcher.ucr_Similarity_Calculation_Details_Genre()
		Me.tpg_110_Narrative_Theme_Topic = New DevExpress.XtraTab.XtraTabPage()
		Me.ucr_110_Narrative_Theme_Topic = New Metropolis_Launcher.ucr_Similarity_Calculation_Details_Genre()
		Me.tpg_111_Setting = New DevExpress.XtraTab.XtraTabPage()
		Me.ucr_111_Setting = New Metropolis_Launcher.ucr_Similarity_Calculation_Details_Genre()
		Me.tpg_103_Sports_Themes = New DevExpress.XtraTab.XtraTabPage()
		Me.ucr_103_Sports_Themes = New Metropolis_Launcher.ucr_Similarity_Calculation_Details_Genre()
		Me.tpg_112_Vehicular_Themes = New DevExpress.XtraTab.XtraTabPage()
		Me.ucr_112_Vehicular_Themes = New Metropolis_Launcher.ucr_Similarity_Calculation_Details_Genre()
		Me.tpg_105_Educational_Categories = New DevExpress.XtraTab.XtraTabPage()
		Me.ucr_105_Educational_Categories = New Metropolis_Launcher.ucr_Similarity_Calculation_Details_Genre()
		Me.tpg_113_Interface_Control = New DevExpress.XtraTab.XtraTabPage()
		Me.ucr_113_Interface_Control = New Metropolis_Launcher.ucr_Similarity_Calculation_Details_Genre()
		Me.tpg_114_DLC_Addon = New DevExpress.XtraTab.XtraTabPage()
		Me.ucr_114_DLC_Addon = New Metropolis_Launcher.ucr_Similarity_Calculation_Details_Genre()
		Me.tpg_115_Special_Edition = New DevExpress.XtraTab.XtraTabPage()
		Me.ucr_115_Special_Edition = New Metropolis_Launcher.ucr_Similarity_Calculation_Details_Genre()
		Me.tpg_106_Other_Attributes = New DevExpress.XtraTab.XtraTabPage()
		Me.ucr_106_Other_Attributes = New Metropolis_Launcher.ucr_Similarity_Calculation_Details_Genre()
		Me.tpg_201_MinPlayers = New DevExpress.XtraTab.XtraTabPage()
		Me.pnl_201_MinPlayers = New MKNetDXLib.ctl_MKDXPanel()
		Me.pnl_201_MinPlayers_Details = New MKNetDXLib.ctl_MKDXPanel()
		Me.lbl_Weight_201_MinPlayers = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_201_MinPlayers_A = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_201_MinPlayers_B = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Weight_201_MinPlayers_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_201_MinPlayers_A_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_201_MinPlayers_B_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_201_MinPlayers_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		Me.tpg_202_MaxPlayers = New DevExpress.XtraTab.XtraTabPage()
		Me.pnl_202_MaxPlayers = New MKNetDXLib.ctl_MKDXPanel()
		Me.pnl_202_MaxPlayers_Details = New MKNetDXLib.ctl_MKDXPanel()
		Me.lbl_Weight_202_MaxPlayers = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_202_MaxPlayers_A = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_202_MaxPlayers_B = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Weight_202_MaxPlayers_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_202_MaxPlayers_A_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_202_MaxPlayers_B_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_202_MaxPlayers_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		Me.tpg_203_AgeO = New DevExpress.XtraTab.XtraTabPage()
		Me.pnl_203_AgeO = New MKNetDXLib.ctl_MKDXPanel()
		Me.pnl_203_AgeO_Details = New MKNetDXLib.ctl_MKDXPanel()
		Me.lbl_Weight_203_AgeO = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_203_AgeO_A = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_203_AgeO_B = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Weight_203_AgeO_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_203_AgeO_A_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_203_AgeO_B_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_203_AgeO_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		Me.tpg_204_AgeP = New DevExpress.XtraTab.XtraTabPage()
		Me.pnl_204_AgeP = New MKNetDXLib.ctl_MKDXPanel()
		Me.pnl_204_AgeP_Details = New MKNetDXLib.ctl_MKDXPanel()
		Me.lbl_Weight_204_AgeP = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_204_AgeP_A = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_204_AgeP_B = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Weight_204_AgeP_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_204_AgeP_A_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_204_AgeP_B_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_204_AgeP = New MKNetDXLib.ctl_MKDXLabel()
		Me.tpg_205_Rating_Descriptors = New DevExpress.XtraTab.XtraTabPage()
		Me.pnl_205_Rating_Descriptors = New MKNetDXLib.ctl_MKDXPanel()
		Me.pnl_205_Rating_Descriptors_Details = New MKNetDXLib.ctl_MKDXPanel()
		Me.tlp_205_Rating_Descriptors = New System.Windows.Forms.TableLayoutPanel()
		Me.gb_205_Rating_Descriptors_AB = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_205_Rating_Descriptors_AB = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_205_Rating_Descriptors_AB = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_205_Rating_Descriptors_AB = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.gb_205_Rating_Descriptors_B = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_205_Rating_Descriptors_B = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_205_Rating_Descriptors_B = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_205_Rating_Descriptors_B = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.gb_205_Rating_Descriptors_A = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_205_Rating_Descriptors_A = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_205_Rating_Descriptors_A = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_205_Rating_Descriptors_A = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.lbl_Weight_205_Rating_Descriptors = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Weight_205_Rating_Descriptors_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_205_Rating_Descriptors_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		Me.tpg_207_Multiplayer_Attributes = New DevExpress.XtraTab.XtraTabPage()
		Me.pnl_207_Multiplayer_Attributes = New MKNetDXLib.ctl_MKDXPanel()
		Me.pnl_207_Multiplayer_Attributes_Details = New MKNetDXLib.ctl_MKDXPanel()
		Me.tlp_207_Multiplayer_Attributes = New System.Windows.Forms.TableLayoutPanel()
		Me.gb_207_Multiplayer_Attributes_AB = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_207_Multiplayer_Attributes_AB = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_207_Multiplayer_Attributes_AB = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_207_Multiplayer_Attributes_AB = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.gb_207_Multiplayer_Attributes_B = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_207_Multiplayer_Attributes_B = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_207_Multiplayer_Attributes_B = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_207_Multiplayer_Attributes_B = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.gb_207_Multiplayer_Attributes_A = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_207_Multiplayer_Attributes_A = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_207_Multiplayer_Attributes_A = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_207_Multiplayer_Attributes_A = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.lbl_Weight_207_Multiplayer_Attributes = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Weight_207_Multiplayer_Attributes_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_207_Multiplayer_Attributes_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		Me.tpg_206_Other_Attributes = New DevExpress.XtraTab.XtraTabPage()
		Me.pnl_206_Other_Attributes = New MKNetDXLib.ctl_MKDXPanel()
		Me.pnl_206_Other_Attributes_Details = New MKNetDXLib.ctl_MKDXPanel()
		Me.tlp_206_Other_Attributes = New System.Windows.Forms.TableLayoutPanel()
		Me.gb_206_Other_Attributes_AB = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_206_Other_Attributes_AB = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_206_Other_Attributes_AB = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_206_Other_Attributes_AB = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.gb_206_Other_Attributes_B = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_206_Other_Attributes_B = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_206_Other_Attributes_B = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_206_Other_Attributes_B = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.gb_206_Other_Attributes_A = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_206_Other_Attributes_A = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_206_Other_Attributes_A = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_206_Other_Attributes_A = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.lbl_Weight_206_Other_Attributes = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Weight_206_Other_Attributes_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_206_Other_Attributes_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		Me.tpg_301_Group_Membership = New DevExpress.XtraTab.XtraTabPage()
		Me.pnl_301_Group_Membership = New MKNetDXLib.ctl_MKDXPanel()
		Me.pnl_301_Group_Membership_Details = New MKNetDXLib.ctl_MKDXPanel()
		Me.tlp_301_Group_Membership = New System.Windows.Forms.TableLayoutPanel()
		Me.gb_301_Group_Membership_AB = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_301_Group_Membership_AB = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_301_Group_Membership_AB = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_301_Group_Membership_AB = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.gb_301_Group_Membership_B = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_301_Group_Membership_B = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_301_Group_Membership_B = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_301_Group_Membership_B = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.gb_301_Group_Membership_A = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_301_Group_Membership_A = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_301_Group_Membership_A = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_301_Group_Membership_A = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.lbl_Weight_301_Group_Membership = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Weight_301_Group_Membership_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_301_Group_Membership_Explanation = New MKNetDXLib.ctl_MKDXLabel()
		Me.tpg_401_Staff = New DevExpress.XtraTab.XtraTabPage()
		Me.pnl_401_Staff = New MKNetDXLib.ctl_MKDXPanel()
		Me.pnl_401_Staff_Details = New MKNetDXLib.ctl_MKDXPanel()
		Me.tlp_401_Staff = New System.Windows.Forms.TableLayoutPanel()
		Me.gb_401_Staff_AB = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_401_Staff_AB = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_401_Staff_AB = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_401_Staff_AB = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.gb_401_Staff_B = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_401_Staff_B = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_401_Staff_B = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_401_Staff_B = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.gb_401_Staff_A = New MKNetDXLib.ctl_MKDXGroupBox()
		Me.grd_401_Staff_A = New MKNetDXLib.ctl_MKDXGrid()
		Me.gv_401_Staff_A = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.col_401_Staff_A = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.lbl_Weight_401_Staff = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Weight_401_Staff_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_401_Staff = New MKNetDXLib.ctl_MKDXLabel()
		Me.DataTable1 = New System.Data.DataTable()
		Me.col_id = New System.Data.DataColumn()
		Me.DataColumn2 = New System.Data.DataColumn()
		Me.DataColumn3 = New System.Data.DataColumn()
		Me.DataColumn4 = New System.Data.DataColumn()
		Me.lbl_Game_A = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Game_A_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Game_B = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Game_B_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Similarity = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Similarity_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.btn_Close = New MKNetDXLib.ctl_MKDXSimpleButton()
		Me.lbl_Similarity_Calculation_Configuration = New MKNetDXLib.ctl_MKDXLabel()
		Me.lbl_Similarity_Calculation_Configuration_Text = New MKNetDXLib.ctl_MKDXLabel()
		Me.DataColumn5 = New System.Data.DataColumn()
		Me.DataColumn6 = New System.Data.DataColumn()
		CType(Me.splt_Main, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.splt_Main.SuspendLayout()
		CType(Me.grd_Main, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.BTA_Main, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_Main, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.tcl_Main, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tcl_Main.SuspendLayout()
		Me.tpg_001_Platform.SuspendLayout()
		CType(Me.pnl_001_Platform, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_001_Platform.SuspendLayout()
		CType(Me.pnl_001_Platform_Details, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_001_Platform_Details.SuspendLayout()
		Me.tpg_002_MobyRank.SuspendLayout()
		CType(Me.pnl_002_MobyRank, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_002_MobyRank.SuspendLayout()
		CType(Me.pnl_002_MobyRank_Details, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_002_MobyRank_Details.SuspendLayout()
		Me.tpg_003_MobyScore.SuspendLayout()
		CType(Me.pnl_003_MobyScore, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_003_MobyScore.SuspendLayout()
		CType(Me.pnl_003_MobyScore_Details, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_003_MobyScore_Details.SuspendLayout()
		Me.tpg_004_Publisher.SuspendLayout()
		CType(Me.pnl_004_Publisher, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_004_Publisher.SuspendLayout()
		CType(Me.pnl_004_Publisher_Details, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_004_Publisher_Details.SuspendLayout()
		Me.tpg_005_Developer.SuspendLayout()
		CType(Me.pnl_005_Developer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_005_Developer.SuspendLayout()
		CType(Me.pnl_005_Developer_Details, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_005_Developer_Details.SuspendLayout()
		Me.tpg_006_Year.SuspendLayout()
		CType(Me.pnl_006_Year, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_006_Year.SuspendLayout()
		CType(Me.pnl_006_Year_Details, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_006_Year_Details.SuspendLayout()
		Me.tpg_101_Basic_Genres.SuspendLayout()
		Me.tpg_102_Perspectives.SuspendLayout()
		Me.tpg_107_Visual_Presentation.SuspendLayout()
		Me.tpg_108_Gameplay.SuspendLayout()
		Me.tpg_109_Pacing.SuspendLayout()
		Me.tpg_110_Narrative_Theme_Topic.SuspendLayout()
		Me.tpg_111_Setting.SuspendLayout()
		Me.tpg_103_Sports_Themes.SuspendLayout()
		Me.tpg_112_Vehicular_Themes.SuspendLayout()
		Me.tpg_105_Educational_Categories.SuspendLayout()
		Me.tpg_113_Interface_Control.SuspendLayout()
		Me.tpg_114_DLC_Addon.SuspendLayout()
		Me.tpg_115_Special_Edition.SuspendLayout()
		Me.tpg_106_Other_Attributes.SuspendLayout()
		Me.tpg_201_MinPlayers.SuspendLayout()
		CType(Me.pnl_201_MinPlayers, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_201_MinPlayers.SuspendLayout()
		CType(Me.pnl_201_MinPlayers_Details, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_201_MinPlayers_Details.SuspendLayout()
		Me.tpg_202_MaxPlayers.SuspendLayout()
		CType(Me.pnl_202_MaxPlayers, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_202_MaxPlayers.SuspendLayout()
		CType(Me.pnl_202_MaxPlayers_Details, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_202_MaxPlayers_Details.SuspendLayout()
		Me.tpg_203_AgeO.SuspendLayout()
		CType(Me.pnl_203_AgeO, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_203_AgeO.SuspendLayout()
		CType(Me.pnl_203_AgeO_Details, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_203_AgeO_Details.SuspendLayout()
		Me.tpg_204_AgeP.SuspendLayout()
		CType(Me.pnl_204_AgeP, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_204_AgeP.SuspendLayout()
		CType(Me.pnl_204_AgeP_Details, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_204_AgeP_Details.SuspendLayout()
		Me.tpg_205_Rating_Descriptors.SuspendLayout()
		CType(Me.pnl_205_Rating_Descriptors, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_205_Rating_Descriptors.SuspendLayout()
		CType(Me.pnl_205_Rating_Descriptors_Details, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_205_Rating_Descriptors_Details.SuspendLayout()
		Me.tlp_205_Rating_Descriptors.SuspendLayout()
		CType(Me.gb_205_Rating_Descriptors_AB, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_205_Rating_Descriptors_AB.SuspendLayout()
		CType(Me.grd_205_Rating_Descriptors_AB, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_205_Rating_Descriptors_AB, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gb_205_Rating_Descriptors_B, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_205_Rating_Descriptors_B.SuspendLayout()
		CType(Me.grd_205_Rating_Descriptors_B, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_205_Rating_Descriptors_B, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gb_205_Rating_Descriptors_A, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_205_Rating_Descriptors_A.SuspendLayout()
		CType(Me.grd_205_Rating_Descriptors_A, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_205_Rating_Descriptors_A, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tpg_207_Multiplayer_Attributes.SuspendLayout()
		CType(Me.pnl_207_Multiplayer_Attributes, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_207_Multiplayer_Attributes.SuspendLayout()
		CType(Me.pnl_207_Multiplayer_Attributes_Details, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_207_Multiplayer_Attributes_Details.SuspendLayout()
		Me.tlp_207_Multiplayer_Attributes.SuspendLayout()
		CType(Me.gb_207_Multiplayer_Attributes_AB, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_207_Multiplayer_Attributes_AB.SuspendLayout()
		CType(Me.grd_207_Multiplayer_Attributes_AB, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_207_Multiplayer_Attributes_AB, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gb_207_Multiplayer_Attributes_B, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_207_Multiplayer_Attributes_B.SuspendLayout()
		CType(Me.grd_207_Multiplayer_Attributes_B, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_207_Multiplayer_Attributes_B, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gb_207_Multiplayer_Attributes_A, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_207_Multiplayer_Attributes_A.SuspendLayout()
		CType(Me.grd_207_Multiplayer_Attributes_A, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_207_Multiplayer_Attributes_A, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tpg_206_Other_Attributes.SuspendLayout()
		CType(Me.pnl_206_Other_Attributes, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_206_Other_Attributes.SuspendLayout()
		CType(Me.pnl_206_Other_Attributes_Details, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_206_Other_Attributes_Details.SuspendLayout()
		Me.tlp_206_Other_Attributes.SuspendLayout()
		CType(Me.gb_206_Other_Attributes_AB, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_206_Other_Attributes_AB.SuspendLayout()
		CType(Me.grd_206_Other_Attributes_AB, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_206_Other_Attributes_AB, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gb_206_Other_Attributes_B, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_206_Other_Attributes_B.SuspendLayout()
		CType(Me.grd_206_Other_Attributes_B, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_206_Other_Attributes_B, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gb_206_Other_Attributes_A, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_206_Other_Attributes_A.SuspendLayout()
		CType(Me.grd_206_Other_Attributes_A, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_206_Other_Attributes_A, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tpg_301_Group_Membership.SuspendLayout()
		CType(Me.pnl_301_Group_Membership, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_301_Group_Membership.SuspendLayout()
		CType(Me.pnl_301_Group_Membership_Details, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_301_Group_Membership_Details.SuspendLayout()
		Me.tlp_301_Group_Membership.SuspendLayout()
		CType(Me.gb_301_Group_Membership_AB, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_301_Group_Membership_AB.SuspendLayout()
		CType(Me.grd_301_Group_Membership_AB, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_301_Group_Membership_AB, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gb_301_Group_Membership_B, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_301_Group_Membership_B.SuspendLayout()
		CType(Me.grd_301_Group_Membership_B, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_301_Group_Membership_B, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gb_301_Group_Membership_A, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_301_Group_Membership_A.SuspendLayout()
		CType(Me.grd_301_Group_Membership_A, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_301_Group_Membership_A, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tpg_401_Staff.SuspendLayout()
		CType(Me.pnl_401_Staff, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_401_Staff.SuspendLayout()
		CType(Me.pnl_401_Staff_Details, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnl_401_Staff_Details.SuspendLayout()
		Me.tlp_401_Staff.SuspendLayout()
		CType(Me.gb_401_Staff_AB, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_401_Staff_AB.SuspendLayout()
		CType(Me.grd_401_Staff_AB, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_401_Staff_AB, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gb_401_Staff_B, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_401_Staff_B.SuspendLayout()
		CType(Me.grd_401_Staff_B, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_401_Staff_B, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gb_401_Staff_A, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gb_401_Staff_A.SuspendLayout()
		CType(Me.grd_401_Staff_A, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.gv_401_Staff_A, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'splt_Main
		'
		Me.splt_Main.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.splt_Main.Location = New System.Drawing.Point(3, 96)
		Me.splt_Main.Name = "splt_Main"
		Me.splt_Main.Panel1.Controls.Add(Me.grd_Main)
		Me.splt_Main.Panel1.Text = "Panel1"
		Me.splt_Main.Panel2.Controls.Add(Me.tcl_Main)
		Me.splt_Main.Panel2.Text = "Panel2"
		Me.splt_Main.Size = New System.Drawing.Size(778, 502)
		Me.splt_Main.SplitterPosition = 384
		Me.splt_Main.TabIndex = 0
		Me.splt_Main.Text = "Ctl_MKDXSplitPanel1"
		'
		'grd_Main
		'
		Me.grd_Main.DataSource = Me.BTA_Main
		Me.grd_Main.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_Main.Location = New System.Drawing.Point(0, 0)
		Me.grd_Main.MainView = Me.gv_Main
		Me.grd_Main.Name = "grd_Main"
		Me.grd_Main.Size = New System.Drawing.Size(384, 502)
		Me.grd_Main.TabIndex = 0
		Me.grd_Main.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_Main})
		'
		'BTA_Main
		'
		Me.BTA_Main.AllowDelete = True
		Me.BTA_Main.ColumnUpdateBlacklistStream = CType(resources.GetObject("BTA_Main.ColumnUpdateBlacklistStream"), System.Collections.Generic.List(Of String))
		Me.BTA_Main.Connection = Nothing
		Me.BTA_Main.DSStream = CType(resources.GetObject("BTA_Main.DSStream"), System.IO.MemoryStream)
		Me.BTA_Main.FillMethod = MKNetLib.FillMethod.ValueList
		Me.BTA_Main.FillString = resources.GetString("BTA_Main.FillString")
		Me.BTA_Main.Position = 0
		Me.BTA_Main.Transaction = Nothing
		Me.BTA_Main.UpdateTablesStream = CType(resources.GetObject("BTA_Main.UpdateTablesStream"), System.Collections.Generic.List(Of String))
		'
		'gv_Main
		'
		Me.gv_Main.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colFeature, Me.colScore, Me.colWeight, Me.colWeighted_Score})
		Me.gv_Main.GridControl = Me.grd_Main
		Me.gv_Main.Name = "gv_Main"
		Me.gv_Main.OptionsBehavior.AllowIncrementalSearch = True
		Me.gv_Main.OptionsView.ColumnAutoWidth = False
		Me.gv_Main.OptionsView.ShowFooter = True
		Me.gv_Main.OptionsView.ShowGroupPanel = False
		Me.gv_Main.OptionsView.ShowIndicator = False
		'
		'colFeature
		'
		Me.colFeature.FieldName = "Feature"
		Me.colFeature.Name = "colFeature"
		Me.colFeature.OptionsColumn.AllowEdit = False
		Me.colFeature.OptionsColumn.ReadOnly = True
		Me.colFeature.Visible = True
		Me.colFeature.VisibleIndex = 0
		Me.colFeature.Width = 141
		'
		'colScore
		'
		Me.colScore.AppearanceCell.Options.UseTextOptions = True
		Me.colScore.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.colScore.FieldName = "Score"
		Me.colScore.Name = "colScore"
		Me.colScore.OptionsColumn.AllowEdit = False
		Me.colScore.OptionsColumn.ReadOnly = True
		Me.colScore.Visible = True
		Me.colScore.VisibleIndex = 1
		Me.colScore.Width = 57
		'
		'colWeight
		'
		Me.colWeight.FieldName = "Weight"
		Me.colWeight.Name = "colWeight"
		Me.colWeight.OptionsColumn.AllowEdit = False
		Me.colWeight.OptionsColumn.ReadOnly = True
		Me.colWeight.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "Weight", "∑={0:#.##}")})
		Me.colWeight.Visible = True
		Me.colWeight.VisibleIndex = 2
		Me.colWeight.Width = 63
		'
		'colWeighted_Score
		'
		Me.colWeighted_Score.Caption = "Weighted Score"
		Me.colWeighted_Score.FieldName = "Weighted_Score"
		Me.colWeighted_Score.Name = "colWeighted_Score"
		Me.colWeighted_Score.OptionsColumn.AllowEdit = False
		Me.colWeighted_Score.OptionsColumn.ReadOnly = True
		Me.colWeighted_Score.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Weighted_Score", "∑={0:#.##}")})
		Me.colWeighted_Score.Visible = True
		Me.colWeighted_Score.VisibleIndex = 3
		Me.colWeighted_Score.Width = 96
		'
		'tcl_Main
		'
		Me.tcl_Main.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tcl_Main.Location = New System.Drawing.Point(0, 0)
		Me.tcl_Main.Name = "tcl_Main"
		Me.tcl_Main.SelectedTabPage = Me.tpg_001_Platform
		Me.tcl_Main.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[True]
		Me.tcl_Main.Size = New System.Drawing.Size(389, 502)
		Me.tcl_Main.TabIndex = 0
		Me.tcl_Main.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tpg_001_Platform, Me.tpg_002_MobyRank, Me.tpg_003_MobyScore, Me.tpg_004_Publisher, Me.tpg_005_Developer, Me.tpg_006_Year, Me.tpg_101_Basic_Genres, Me.tpg_102_Perspectives, Me.tpg_107_Visual_Presentation, Me.tpg_108_Gameplay, Me.tpg_109_Pacing, Me.tpg_110_Narrative_Theme_Topic, Me.tpg_111_Setting, Me.tpg_103_Sports_Themes, Me.tpg_112_Vehicular_Themes, Me.tpg_105_Educational_Categories, Me.tpg_113_Interface_Control, Me.tpg_114_DLC_Addon, Me.tpg_115_Special_Edition, Me.tpg_106_Other_Attributes, Me.tpg_201_MinPlayers, Me.tpg_202_MaxPlayers, Me.tpg_203_AgeO, Me.tpg_204_AgeP, Me.tpg_205_Rating_Descriptors, Me.tpg_207_Multiplayer_Attributes, Me.tpg_206_Other_Attributes, Me.tpg_301_Group_Membership, Me.tpg_401_Staff})
		'
		'tpg_001_Platform
		'
		Me.tpg_001_Platform.Controls.Add(Me.pnl_001_Platform)
		Me.tpg_001_Platform.Name = "tpg_001_Platform"
		Me.tpg_001_Platform.Size = New System.Drawing.Size(383, 474)
		Me.tpg_001_Platform.Text = "001_Platform"
		'
		'pnl_001_Platform
		'
		Me.pnl_001_Platform.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_001_Platform.Controls.Add(Me.pnl_001_Platform_Details)
		Me.pnl_001_Platform.Controls.Add(Me.lbl_001_Platform_Explanation)
		Me.pnl_001_Platform.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_001_Platform.Location = New System.Drawing.Point(0, 0)
		Me.pnl_001_Platform.Name = "pnl_001_Platform"
		Me.pnl_001_Platform.Size = New System.Drawing.Size(383, 474)
		Me.pnl_001_Platform.TabIndex = 8
		'
		'pnl_001_Platform_Details
		'
		Me.pnl_001_Platform_Details.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_001_Platform_Details.Controls.Add(Me.lbl_Weight_001_Platform)
		Me.pnl_001_Platform_Details.Controls.Add(Me.lbl_001_Platform_A)
		Me.pnl_001_Platform_Details.Controls.Add(Me.lbl_001_Platform_B)
		Me.pnl_001_Platform_Details.Controls.Add(Me.lbl_Weight_001_Platform_Text)
		Me.pnl_001_Platform_Details.Controls.Add(Me.lbl_001_Platform_A_Text)
		Me.pnl_001_Platform_Details.Controls.Add(Me.lbl_001_Platform_B_Text)
		Me.pnl_001_Platform_Details.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_001_Platform_Details.Location = New System.Drawing.Point(0, 19)
		Me.pnl_001_Platform_Details.Name = "pnl_001_Platform_Details"
		Me.pnl_001_Platform_Details.Size = New System.Drawing.Size(383, 455)
		Me.pnl_001_Platform_Details.TabIndex = 1
		'
		'lbl_Weight_001_Platform
		'
		Me.lbl_Weight_001_Platform.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Weight_001_Platform.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_001_Platform.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Weight_001_Platform.MKBoundControl1 = Nothing
		Me.lbl_Weight_001_Platform.MKBoundControl2 = Nothing
		Me.lbl_Weight_001_Platform.MKBoundControl3 = Nothing
		Me.lbl_Weight_001_Platform.MKBoundControl4 = Nothing
		Me.lbl_Weight_001_Platform.MKBoundControl5 = Nothing
		Me.lbl_Weight_001_Platform.Name = "lbl_Weight_001_Platform"
		Me.lbl_Weight_001_Platform.Size = New System.Drawing.Size(113, 20)
		Me.lbl_Weight_001_Platform.TabIndex = 7
		Me.lbl_Weight_001_Platform.Text = "Weight:"
		'
		'lbl_001_Platform_A
		'
		Me.lbl_001_Platform_A.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_001_Platform_A.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_001_Platform_A.Location = New System.Drawing.Point(3, 26)
		Me.lbl_001_Platform_A.MKBoundControl1 = Nothing
		Me.lbl_001_Platform_A.MKBoundControl2 = Nothing
		Me.lbl_001_Platform_A.MKBoundControl3 = Nothing
		Me.lbl_001_Platform_A.MKBoundControl4 = Nothing
		Me.lbl_001_Platform_A.MKBoundControl5 = Nothing
		Me.lbl_001_Platform_A.Name = "lbl_001_Platform_A"
		Me.lbl_001_Platform_A.Size = New System.Drawing.Size(113, 20)
		Me.lbl_001_Platform_A.TabIndex = 7
		Me.lbl_001_Platform_A.Text = "Platform of Game A:"
		'
		'lbl_001_Platform_B
		'
		Me.lbl_001_Platform_B.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_001_Platform_B.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_001_Platform_B.Location = New System.Drawing.Point(3, 49)
		Me.lbl_001_Platform_B.MKBoundControl1 = Nothing
		Me.lbl_001_Platform_B.MKBoundControl2 = Nothing
		Me.lbl_001_Platform_B.MKBoundControl3 = Nothing
		Me.lbl_001_Platform_B.MKBoundControl4 = Nothing
		Me.lbl_001_Platform_B.MKBoundControl5 = Nothing
		Me.lbl_001_Platform_B.Name = "lbl_001_Platform_B"
		Me.lbl_001_Platform_B.Size = New System.Drawing.Size(113, 20)
		Me.lbl_001_Platform_B.TabIndex = 7
		Me.lbl_001_Platform_B.Text = "Platform of Game B:"
		'
		'lbl_Weight_001_Platform_Text
		'
		Me.lbl_Weight_001_Platform_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Weight_001_Platform_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Weight_001_Platform_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_001_Platform_Text.Location = New System.Drawing.Point(119, 3)
		Me.lbl_Weight_001_Platform_Text.MKBoundControl1 = Nothing
		Me.lbl_Weight_001_Platform_Text.MKBoundControl2 = Nothing
		Me.lbl_Weight_001_Platform_Text.MKBoundControl3 = Nothing
		Me.lbl_Weight_001_Platform_Text.MKBoundControl4 = Nothing
		Me.lbl_Weight_001_Platform_Text.MKBoundControl5 = Nothing
		Me.lbl_Weight_001_Platform_Text.Name = "lbl_Weight_001_Platform_Text"
		Me.lbl_Weight_001_Platform_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_Weight_001_Platform_Text.TabIndex = 7
		'
		'lbl_001_Platform_A_Text
		'
		Me.lbl_001_Platform_A_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_001_Platform_A_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_001_Platform_A_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_001_Platform_A_Text.Location = New System.Drawing.Point(119, 26)
		Me.lbl_001_Platform_A_Text.MKBoundControl1 = Nothing
		Me.lbl_001_Platform_A_Text.MKBoundControl2 = Nothing
		Me.lbl_001_Platform_A_Text.MKBoundControl3 = Nothing
		Me.lbl_001_Platform_A_Text.MKBoundControl4 = Nothing
		Me.lbl_001_Platform_A_Text.MKBoundControl5 = Nothing
		Me.lbl_001_Platform_A_Text.Name = "lbl_001_Platform_A_Text"
		Me.lbl_001_Platform_A_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_001_Platform_A_Text.TabIndex = 7
		'
		'lbl_001_Platform_B_Text
		'
		Me.lbl_001_Platform_B_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_001_Platform_B_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_001_Platform_B_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_001_Platform_B_Text.Location = New System.Drawing.Point(119, 49)
		Me.lbl_001_Platform_B_Text.MKBoundControl1 = Nothing
		Me.lbl_001_Platform_B_Text.MKBoundControl2 = Nothing
		Me.lbl_001_Platform_B_Text.MKBoundControl3 = Nothing
		Me.lbl_001_Platform_B_Text.MKBoundControl4 = Nothing
		Me.lbl_001_Platform_B_Text.MKBoundControl5 = Nothing
		Me.lbl_001_Platform_B_Text.Name = "lbl_001_Platform_B_Text"
		Me.lbl_001_Platform_B_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_001_Platform_B_Text.TabIndex = 7
		'
		'lbl_001_Platform_Explanation
		'
		Me.lbl_001_Platform_Explanation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_001_Platform_Explanation.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_001_Platform_Explanation.Location = New System.Drawing.Point(0, 0)
		Me.lbl_001_Platform_Explanation.MKBoundControl1 = Nothing
		Me.lbl_001_Platform_Explanation.MKBoundControl2 = Nothing
		Me.lbl_001_Platform_Explanation.MKBoundControl3 = Nothing
		Me.lbl_001_Platform_Explanation.MKBoundControl4 = Nothing
		Me.lbl_001_Platform_Explanation.MKBoundControl5 = Nothing
		Me.lbl_001_Platform_Explanation.Name = "lbl_001_Platform_Explanation"
		Me.lbl_001_Platform_Explanation.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_001_Platform_Explanation.Size = New System.Drawing.Size(383, 19)
		Me.lbl_001_Platform_Explanation.TabIndex = 0
		Me.lbl_001_Platform_Explanation.Text = "If both platforms match, a similarity score of 100 is applied, else 0."
		'
		'tpg_002_MobyRank
		'
		Me.tpg_002_MobyRank.Controls.Add(Me.pnl_002_MobyRank)
		Me.tpg_002_MobyRank.Name = "tpg_002_MobyRank"
		Me.tpg_002_MobyRank.Size = New System.Drawing.Size(383, 474)
		Me.tpg_002_MobyRank.Text = "002_MobyRank"
		'
		'pnl_002_MobyRank
		'
		Me.pnl_002_MobyRank.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_002_MobyRank.Controls.Add(Me.pnl_002_MobyRank_Details)
		Me.pnl_002_MobyRank.Controls.Add(Me.lbl_002_MobyRank_Explanation)
		Me.pnl_002_MobyRank.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_002_MobyRank.Location = New System.Drawing.Point(0, 0)
		Me.pnl_002_MobyRank.Name = "pnl_002_MobyRank"
		Me.pnl_002_MobyRank.Size = New System.Drawing.Size(383, 474)
		Me.pnl_002_MobyRank.TabIndex = 9
		'
		'pnl_002_MobyRank_Details
		'
		Me.pnl_002_MobyRank_Details.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_002_MobyRank_Details.Controls.Add(Me.lbl_Weight_002_MobyRank)
		Me.pnl_002_MobyRank_Details.Controls.Add(Me.lbl_002_MobyRank_A)
		Me.pnl_002_MobyRank_Details.Controls.Add(Me.lbl_002_MobyRank_B)
		Me.pnl_002_MobyRank_Details.Controls.Add(Me.lbl_Weight_002_MobyRank_Text)
		Me.pnl_002_MobyRank_Details.Controls.Add(Me.lbl_002_MobyRank_A_Text)
		Me.pnl_002_MobyRank_Details.Controls.Add(Me.lbl_002_MobyRank_B_Text)
		Me.pnl_002_MobyRank_Details.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_002_MobyRank_Details.Location = New System.Drawing.Point(0, 32)
		Me.pnl_002_MobyRank_Details.Name = "pnl_002_MobyRank_Details"
		Me.pnl_002_MobyRank_Details.Size = New System.Drawing.Size(383, 442)
		Me.pnl_002_MobyRank_Details.TabIndex = 1
		'
		'lbl_Weight_002_MobyRank
		'
		Me.lbl_Weight_002_MobyRank.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Weight_002_MobyRank.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_002_MobyRank.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Weight_002_MobyRank.MKBoundControl1 = Nothing
		Me.lbl_Weight_002_MobyRank.MKBoundControl2 = Nothing
		Me.lbl_Weight_002_MobyRank.MKBoundControl3 = Nothing
		Me.lbl_Weight_002_MobyRank.MKBoundControl4 = Nothing
		Me.lbl_Weight_002_MobyRank.MKBoundControl5 = Nothing
		Me.lbl_Weight_002_MobyRank.Name = "lbl_Weight_002_MobyRank"
		Me.lbl_Weight_002_MobyRank.Size = New System.Drawing.Size(113, 20)
		Me.lbl_Weight_002_MobyRank.TabIndex = 7
		Me.lbl_Weight_002_MobyRank.Text = "Weight:"
		'
		'lbl_002_MobyRank_A
		'
		Me.lbl_002_MobyRank_A.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_002_MobyRank_A.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_002_MobyRank_A.Location = New System.Drawing.Point(3, 26)
		Me.lbl_002_MobyRank_A.MKBoundControl1 = Nothing
		Me.lbl_002_MobyRank_A.MKBoundControl2 = Nothing
		Me.lbl_002_MobyRank_A.MKBoundControl3 = Nothing
		Me.lbl_002_MobyRank_A.MKBoundControl4 = Nothing
		Me.lbl_002_MobyRank_A.MKBoundControl5 = Nothing
		Me.lbl_002_MobyRank_A.Name = "lbl_002_MobyRank_A"
		Me.lbl_002_MobyRank_A.Size = New System.Drawing.Size(113, 20)
		Me.lbl_002_MobyRank_A.TabIndex = 7
		Me.lbl_002_MobyRank_A.Text = "Rank of Game A:"
		'
		'lbl_002_MobyRank_B
		'
		Me.lbl_002_MobyRank_B.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_002_MobyRank_B.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_002_MobyRank_B.Location = New System.Drawing.Point(3, 49)
		Me.lbl_002_MobyRank_B.MKBoundControl1 = Nothing
		Me.lbl_002_MobyRank_B.MKBoundControl2 = Nothing
		Me.lbl_002_MobyRank_B.MKBoundControl3 = Nothing
		Me.lbl_002_MobyRank_B.MKBoundControl4 = Nothing
		Me.lbl_002_MobyRank_B.MKBoundControl5 = Nothing
		Me.lbl_002_MobyRank_B.Name = "lbl_002_MobyRank_B"
		Me.lbl_002_MobyRank_B.Size = New System.Drawing.Size(113, 20)
		Me.lbl_002_MobyRank_B.TabIndex = 7
		Me.lbl_002_MobyRank_B.Text = "Rank of Game B:"
		'
		'lbl_Weight_002_MobyRank_Text
		'
		Me.lbl_Weight_002_MobyRank_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Weight_002_MobyRank_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Weight_002_MobyRank_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_002_MobyRank_Text.Location = New System.Drawing.Point(119, 3)
		Me.lbl_Weight_002_MobyRank_Text.MKBoundControl1 = Nothing
		Me.lbl_Weight_002_MobyRank_Text.MKBoundControl2 = Nothing
		Me.lbl_Weight_002_MobyRank_Text.MKBoundControl3 = Nothing
		Me.lbl_Weight_002_MobyRank_Text.MKBoundControl4 = Nothing
		Me.lbl_Weight_002_MobyRank_Text.MKBoundControl5 = Nothing
		Me.lbl_Weight_002_MobyRank_Text.Name = "lbl_Weight_002_MobyRank_Text"
		Me.lbl_Weight_002_MobyRank_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_Weight_002_MobyRank_Text.TabIndex = 7
		'
		'lbl_002_MobyRank_A_Text
		'
		Me.lbl_002_MobyRank_A_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_002_MobyRank_A_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_002_MobyRank_A_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_002_MobyRank_A_Text.Location = New System.Drawing.Point(119, 26)
		Me.lbl_002_MobyRank_A_Text.MKBoundControl1 = Nothing
		Me.lbl_002_MobyRank_A_Text.MKBoundControl2 = Nothing
		Me.lbl_002_MobyRank_A_Text.MKBoundControl3 = Nothing
		Me.lbl_002_MobyRank_A_Text.MKBoundControl4 = Nothing
		Me.lbl_002_MobyRank_A_Text.MKBoundControl5 = Nothing
		Me.lbl_002_MobyRank_A_Text.Name = "lbl_002_MobyRank_A_Text"
		Me.lbl_002_MobyRank_A_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_002_MobyRank_A_Text.TabIndex = 7
		'
		'lbl_002_MobyRank_B_Text
		'
		Me.lbl_002_MobyRank_B_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_002_MobyRank_B_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_002_MobyRank_B_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_002_MobyRank_B_Text.Location = New System.Drawing.Point(119, 49)
		Me.lbl_002_MobyRank_B_Text.MKBoundControl1 = Nothing
		Me.lbl_002_MobyRank_B_Text.MKBoundControl2 = Nothing
		Me.lbl_002_MobyRank_B_Text.MKBoundControl3 = Nothing
		Me.lbl_002_MobyRank_B_Text.MKBoundControl4 = Nothing
		Me.lbl_002_MobyRank_B_Text.MKBoundControl5 = Nothing
		Me.lbl_002_MobyRank_B_Text.Name = "lbl_002_MobyRank_B_Text"
		Me.lbl_002_MobyRank_B_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_002_MobyRank_B_Text.TabIndex = 7
		'
		'lbl_002_MobyRank_Explanation
		'
		Me.lbl_002_MobyRank_Explanation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_002_MobyRank_Explanation.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_002_MobyRank_Explanation.Location = New System.Drawing.Point(0, 0)
		Me.lbl_002_MobyRank_Explanation.MKBoundControl1 = Nothing
		Me.lbl_002_MobyRank_Explanation.MKBoundControl2 = Nothing
		Me.lbl_002_MobyRank_Explanation.MKBoundControl3 = Nothing
		Me.lbl_002_MobyRank_Explanation.MKBoundControl4 = Nothing
		Me.lbl_002_MobyRank_Explanation.MKBoundControl5 = Nothing
		Me.lbl_002_MobyRank_Explanation.Name = "lbl_002_MobyRank_Explanation"
		Me.lbl_002_MobyRank_Explanation.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_002_MobyRank_Explanation.Size = New System.Drawing.Size(383, 32)
		Me.lbl_002_MobyRank_Explanation.TabIndex = 0
		Me.lbl_002_MobyRank_Explanation.Text = "The similarity score of 100 is reduced by the difference between the ranks of bot" &
		"h games."
		'
		'tpg_003_MobyScore
		'
		Me.tpg_003_MobyScore.Controls.Add(Me.pnl_003_MobyScore)
		Me.tpg_003_MobyScore.Name = "tpg_003_MobyScore"
		Me.tpg_003_MobyScore.Size = New System.Drawing.Size(383, 474)
		Me.tpg_003_MobyScore.Text = "003_MobyScore"
		'
		'pnl_003_MobyScore
		'
		Me.pnl_003_MobyScore.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_003_MobyScore.Controls.Add(Me.pnl_003_MobyScore_Details)
		Me.pnl_003_MobyScore.Controls.Add(Me.lbl_003_MobyScore_Explanation)
		Me.pnl_003_MobyScore.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_003_MobyScore.Location = New System.Drawing.Point(0, 0)
		Me.pnl_003_MobyScore.Name = "pnl_003_MobyScore"
		Me.pnl_003_MobyScore.Size = New System.Drawing.Size(383, 474)
		Me.pnl_003_MobyScore.TabIndex = 10
		'
		'pnl_003_MobyScore_Details
		'
		Me.pnl_003_MobyScore_Details.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_003_MobyScore_Details.Controls.Add(Me.lbl_Weight_003_MobyScore)
		Me.pnl_003_MobyScore_Details.Controls.Add(Me.lbl_003_MobyScore_A)
		Me.pnl_003_MobyScore_Details.Controls.Add(Me.lbl_003_MobyScore_B)
		Me.pnl_003_MobyScore_Details.Controls.Add(Me.lbl_Weight_003_MobyScore_Text)
		Me.pnl_003_MobyScore_Details.Controls.Add(Me.lbl_003_MobyScore_A_Text)
		Me.pnl_003_MobyScore_Details.Controls.Add(Me.lbl_003_MobyScore_B_Text)
		Me.pnl_003_MobyScore_Details.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_003_MobyScore_Details.Location = New System.Drawing.Point(0, 32)
		Me.pnl_003_MobyScore_Details.Name = "pnl_003_MobyScore_Details"
		Me.pnl_003_MobyScore_Details.Size = New System.Drawing.Size(383, 442)
		Me.pnl_003_MobyScore_Details.TabIndex = 1
		'
		'lbl_Weight_003_MobyScore
		'
		Me.lbl_Weight_003_MobyScore.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Weight_003_MobyScore.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_003_MobyScore.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Weight_003_MobyScore.MKBoundControl1 = Nothing
		Me.lbl_Weight_003_MobyScore.MKBoundControl2 = Nothing
		Me.lbl_Weight_003_MobyScore.MKBoundControl3 = Nothing
		Me.lbl_Weight_003_MobyScore.MKBoundControl4 = Nothing
		Me.lbl_Weight_003_MobyScore.MKBoundControl5 = Nothing
		Me.lbl_Weight_003_MobyScore.Name = "lbl_Weight_003_MobyScore"
		Me.lbl_Weight_003_MobyScore.Size = New System.Drawing.Size(113, 20)
		Me.lbl_Weight_003_MobyScore.TabIndex = 7
		Me.lbl_Weight_003_MobyScore.Text = "Weight:"
		'
		'lbl_003_MobyScore_A
		'
		Me.lbl_003_MobyScore_A.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_003_MobyScore_A.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_003_MobyScore_A.Location = New System.Drawing.Point(3, 26)
		Me.lbl_003_MobyScore_A.MKBoundControl1 = Nothing
		Me.lbl_003_MobyScore_A.MKBoundControl2 = Nothing
		Me.lbl_003_MobyScore_A.MKBoundControl3 = Nothing
		Me.lbl_003_MobyScore_A.MKBoundControl4 = Nothing
		Me.lbl_003_MobyScore_A.MKBoundControl5 = Nothing
		Me.lbl_003_MobyScore_A.Name = "lbl_003_MobyScore_A"
		Me.lbl_003_MobyScore_A.Size = New System.Drawing.Size(113, 20)
		Me.lbl_003_MobyScore_A.TabIndex = 7
		Me.lbl_003_MobyScore_A.Text = "Score of Game A:"
		'
		'lbl_003_MobyScore_B
		'
		Me.lbl_003_MobyScore_B.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_003_MobyScore_B.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_003_MobyScore_B.Location = New System.Drawing.Point(3, 49)
		Me.lbl_003_MobyScore_B.MKBoundControl1 = Nothing
		Me.lbl_003_MobyScore_B.MKBoundControl2 = Nothing
		Me.lbl_003_MobyScore_B.MKBoundControl3 = Nothing
		Me.lbl_003_MobyScore_B.MKBoundControl4 = Nothing
		Me.lbl_003_MobyScore_B.MKBoundControl5 = Nothing
		Me.lbl_003_MobyScore_B.Name = "lbl_003_MobyScore_B"
		Me.lbl_003_MobyScore_B.Size = New System.Drawing.Size(113, 20)
		Me.lbl_003_MobyScore_B.TabIndex = 7
		Me.lbl_003_MobyScore_B.Text = "Score of Game B:"
		'
		'lbl_Weight_003_MobyScore_Text
		'
		Me.lbl_Weight_003_MobyScore_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Weight_003_MobyScore_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Weight_003_MobyScore_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_003_MobyScore_Text.Location = New System.Drawing.Point(119, 3)
		Me.lbl_Weight_003_MobyScore_Text.MKBoundControl1 = Nothing
		Me.lbl_Weight_003_MobyScore_Text.MKBoundControl2 = Nothing
		Me.lbl_Weight_003_MobyScore_Text.MKBoundControl3 = Nothing
		Me.lbl_Weight_003_MobyScore_Text.MKBoundControl4 = Nothing
		Me.lbl_Weight_003_MobyScore_Text.MKBoundControl5 = Nothing
		Me.lbl_Weight_003_MobyScore_Text.Name = "lbl_Weight_003_MobyScore_Text"
		Me.lbl_Weight_003_MobyScore_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_Weight_003_MobyScore_Text.TabIndex = 7
		'
		'lbl_003_MobyScore_A_Text
		'
		Me.lbl_003_MobyScore_A_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_003_MobyScore_A_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_003_MobyScore_A_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_003_MobyScore_A_Text.Location = New System.Drawing.Point(119, 26)
		Me.lbl_003_MobyScore_A_Text.MKBoundControl1 = Nothing
		Me.lbl_003_MobyScore_A_Text.MKBoundControl2 = Nothing
		Me.lbl_003_MobyScore_A_Text.MKBoundControl3 = Nothing
		Me.lbl_003_MobyScore_A_Text.MKBoundControl4 = Nothing
		Me.lbl_003_MobyScore_A_Text.MKBoundControl5 = Nothing
		Me.lbl_003_MobyScore_A_Text.Name = "lbl_003_MobyScore_A_Text"
		Me.lbl_003_MobyScore_A_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_003_MobyScore_A_Text.TabIndex = 7
		'
		'lbl_003_MobyScore_B_Text
		'
		Me.lbl_003_MobyScore_B_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_003_MobyScore_B_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_003_MobyScore_B_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_003_MobyScore_B_Text.Location = New System.Drawing.Point(119, 49)
		Me.lbl_003_MobyScore_B_Text.MKBoundControl1 = Nothing
		Me.lbl_003_MobyScore_B_Text.MKBoundControl2 = Nothing
		Me.lbl_003_MobyScore_B_Text.MKBoundControl3 = Nothing
		Me.lbl_003_MobyScore_B_Text.MKBoundControl4 = Nothing
		Me.lbl_003_MobyScore_B_Text.MKBoundControl5 = Nothing
		Me.lbl_003_MobyScore_B_Text.Name = "lbl_003_MobyScore_B_Text"
		Me.lbl_003_MobyScore_B_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_003_MobyScore_B_Text.TabIndex = 7
		'
		'lbl_003_MobyScore_Explanation
		'
		Me.lbl_003_MobyScore_Explanation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_003_MobyScore_Explanation.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_003_MobyScore_Explanation.Location = New System.Drawing.Point(0, 0)
		Me.lbl_003_MobyScore_Explanation.MKBoundControl1 = Nothing
		Me.lbl_003_MobyScore_Explanation.MKBoundControl2 = Nothing
		Me.lbl_003_MobyScore_Explanation.MKBoundControl3 = Nothing
		Me.lbl_003_MobyScore_Explanation.MKBoundControl4 = Nothing
		Me.lbl_003_MobyScore_Explanation.MKBoundControl5 = Nothing
		Me.lbl_003_MobyScore_Explanation.Name = "lbl_003_MobyScore_Explanation"
		Me.lbl_003_MobyScore_Explanation.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_003_MobyScore_Explanation.Size = New System.Drawing.Size(383, 32)
		Me.lbl_003_MobyScore_Explanation.TabIndex = 0
		Me.lbl_003_MobyScore_Explanation.Text = "The similarity score of 100 is reduced by 20 times the difference between the (mo" &
		"bygames users) score of both games."
		'
		'tpg_004_Publisher
		'
		Me.tpg_004_Publisher.Controls.Add(Me.pnl_004_Publisher)
		Me.tpg_004_Publisher.Name = "tpg_004_Publisher"
		Me.tpg_004_Publisher.Size = New System.Drawing.Size(383, 474)
		Me.tpg_004_Publisher.Text = "004_Publisher"
		'
		'pnl_004_Publisher
		'
		Me.pnl_004_Publisher.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_004_Publisher.Controls.Add(Me.pnl_004_Publisher_Details)
		Me.pnl_004_Publisher.Controls.Add(Me.lbl_004_Publisher_Explanation)
		Me.pnl_004_Publisher.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_004_Publisher.Location = New System.Drawing.Point(0, 0)
		Me.pnl_004_Publisher.Name = "pnl_004_Publisher"
		Me.pnl_004_Publisher.Size = New System.Drawing.Size(383, 474)
		Me.pnl_004_Publisher.TabIndex = 9
		'
		'pnl_004_Publisher_Details
		'
		Me.pnl_004_Publisher_Details.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_004_Publisher_Details.Controls.Add(Me.lbl_Weight_004_Publisher)
		Me.pnl_004_Publisher_Details.Controls.Add(Me.lbl_004_Publisher_A)
		Me.pnl_004_Publisher_Details.Controls.Add(Me.lbl_004_Publisher_B)
		Me.pnl_004_Publisher_Details.Controls.Add(Me.lbl_Weight_004_Publisher_Text)
		Me.pnl_004_Publisher_Details.Controls.Add(Me.lbl_004_Publisher_A_Text)
		Me.pnl_004_Publisher_Details.Controls.Add(Me.lbl_004_Publisher_B_Text)
		Me.pnl_004_Publisher_Details.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_004_Publisher_Details.Location = New System.Drawing.Point(0, 19)
		Me.pnl_004_Publisher_Details.Name = "pnl_004_Publisher_Details"
		Me.pnl_004_Publisher_Details.Size = New System.Drawing.Size(383, 455)
		Me.pnl_004_Publisher_Details.TabIndex = 1
		'
		'lbl_Weight_004_Publisher
		'
		Me.lbl_Weight_004_Publisher.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Weight_004_Publisher.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_004_Publisher.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Weight_004_Publisher.MKBoundControl1 = Nothing
		Me.lbl_Weight_004_Publisher.MKBoundControl2 = Nothing
		Me.lbl_Weight_004_Publisher.MKBoundControl3 = Nothing
		Me.lbl_Weight_004_Publisher.MKBoundControl4 = Nothing
		Me.lbl_Weight_004_Publisher.MKBoundControl5 = Nothing
		Me.lbl_Weight_004_Publisher.Name = "lbl_Weight_004_Publisher"
		Me.lbl_Weight_004_Publisher.Size = New System.Drawing.Size(113, 20)
		Me.lbl_Weight_004_Publisher.TabIndex = 7
		Me.lbl_Weight_004_Publisher.Text = "Weight:"
		'
		'lbl_004_Publisher_A
		'
		Me.lbl_004_Publisher_A.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_004_Publisher_A.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_004_Publisher_A.Location = New System.Drawing.Point(3, 26)
		Me.lbl_004_Publisher_A.MKBoundControl1 = Nothing
		Me.lbl_004_Publisher_A.MKBoundControl2 = Nothing
		Me.lbl_004_Publisher_A.MKBoundControl3 = Nothing
		Me.lbl_004_Publisher_A.MKBoundControl4 = Nothing
		Me.lbl_004_Publisher_A.MKBoundControl5 = Nothing
		Me.lbl_004_Publisher_A.Name = "lbl_004_Publisher_A"
		Me.lbl_004_Publisher_A.Size = New System.Drawing.Size(113, 20)
		Me.lbl_004_Publisher_A.TabIndex = 7
		Me.lbl_004_Publisher_A.Text = "Publisher of Game A:"
		'
		'lbl_004_Publisher_B
		'
		Me.lbl_004_Publisher_B.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_004_Publisher_B.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_004_Publisher_B.Location = New System.Drawing.Point(3, 49)
		Me.lbl_004_Publisher_B.MKBoundControl1 = Nothing
		Me.lbl_004_Publisher_B.MKBoundControl2 = Nothing
		Me.lbl_004_Publisher_B.MKBoundControl3 = Nothing
		Me.lbl_004_Publisher_B.MKBoundControl4 = Nothing
		Me.lbl_004_Publisher_B.MKBoundControl5 = Nothing
		Me.lbl_004_Publisher_B.Name = "lbl_004_Publisher_B"
		Me.lbl_004_Publisher_B.Size = New System.Drawing.Size(113, 20)
		Me.lbl_004_Publisher_B.TabIndex = 7
		Me.lbl_004_Publisher_B.Text = "Publisher of Game B:"
		'
		'lbl_Weight_004_Publisher_Text
		'
		Me.lbl_Weight_004_Publisher_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Weight_004_Publisher_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Weight_004_Publisher_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_004_Publisher_Text.Location = New System.Drawing.Point(119, 3)
		Me.lbl_Weight_004_Publisher_Text.MKBoundControl1 = Nothing
		Me.lbl_Weight_004_Publisher_Text.MKBoundControl2 = Nothing
		Me.lbl_Weight_004_Publisher_Text.MKBoundControl3 = Nothing
		Me.lbl_Weight_004_Publisher_Text.MKBoundControl4 = Nothing
		Me.lbl_Weight_004_Publisher_Text.MKBoundControl5 = Nothing
		Me.lbl_Weight_004_Publisher_Text.Name = "lbl_Weight_004_Publisher_Text"
		Me.lbl_Weight_004_Publisher_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_Weight_004_Publisher_Text.TabIndex = 7
		'
		'lbl_004_Publisher_A_Text
		'
		Me.lbl_004_Publisher_A_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_004_Publisher_A_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_004_Publisher_A_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_004_Publisher_A_Text.Location = New System.Drawing.Point(119, 26)
		Me.lbl_004_Publisher_A_Text.MKBoundControl1 = Nothing
		Me.lbl_004_Publisher_A_Text.MKBoundControl2 = Nothing
		Me.lbl_004_Publisher_A_Text.MKBoundControl3 = Nothing
		Me.lbl_004_Publisher_A_Text.MKBoundControl4 = Nothing
		Me.lbl_004_Publisher_A_Text.MKBoundControl5 = Nothing
		Me.lbl_004_Publisher_A_Text.Name = "lbl_004_Publisher_A_Text"
		Me.lbl_004_Publisher_A_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_004_Publisher_A_Text.TabIndex = 7
		'
		'lbl_004_Publisher_B_Text
		'
		Me.lbl_004_Publisher_B_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_004_Publisher_B_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_004_Publisher_B_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_004_Publisher_B_Text.Location = New System.Drawing.Point(119, 49)
		Me.lbl_004_Publisher_B_Text.MKBoundControl1 = Nothing
		Me.lbl_004_Publisher_B_Text.MKBoundControl2 = Nothing
		Me.lbl_004_Publisher_B_Text.MKBoundControl3 = Nothing
		Me.lbl_004_Publisher_B_Text.MKBoundControl4 = Nothing
		Me.lbl_004_Publisher_B_Text.MKBoundControl5 = Nothing
		Me.lbl_004_Publisher_B_Text.Name = "lbl_004_Publisher_B_Text"
		Me.lbl_004_Publisher_B_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_004_Publisher_B_Text.TabIndex = 7
		'
		'lbl_004_Publisher_Explanation
		'
		Me.lbl_004_Publisher_Explanation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_004_Publisher_Explanation.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_004_Publisher_Explanation.Location = New System.Drawing.Point(0, 0)
		Me.lbl_004_Publisher_Explanation.MKBoundControl1 = Nothing
		Me.lbl_004_Publisher_Explanation.MKBoundControl2 = Nothing
		Me.lbl_004_Publisher_Explanation.MKBoundControl3 = Nothing
		Me.lbl_004_Publisher_Explanation.MKBoundControl4 = Nothing
		Me.lbl_004_Publisher_Explanation.MKBoundControl5 = Nothing
		Me.lbl_004_Publisher_Explanation.Name = "lbl_004_Publisher_Explanation"
		Me.lbl_004_Publisher_Explanation.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_004_Publisher_Explanation.Size = New System.Drawing.Size(383, 19)
		Me.lbl_004_Publisher_Explanation.TabIndex = 0
		Me.lbl_004_Publisher_Explanation.Text = "If both publishers match, a similarity score of 100 is applied, else 0."
		'
		'tpg_005_Developer
		'
		Me.tpg_005_Developer.Controls.Add(Me.pnl_005_Developer)
		Me.tpg_005_Developer.Name = "tpg_005_Developer"
		Me.tpg_005_Developer.Size = New System.Drawing.Size(383, 474)
		Me.tpg_005_Developer.Text = "005_Developer"
		'
		'pnl_005_Developer
		'
		Me.pnl_005_Developer.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_005_Developer.Controls.Add(Me.pnl_005_Developer_Details)
		Me.pnl_005_Developer.Controls.Add(Me.lbl_005_Developer_Explanation)
		Me.pnl_005_Developer.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_005_Developer.Location = New System.Drawing.Point(0, 0)
		Me.pnl_005_Developer.Name = "pnl_005_Developer"
		Me.pnl_005_Developer.Size = New System.Drawing.Size(383, 474)
		Me.pnl_005_Developer.TabIndex = 10
		'
		'pnl_005_Developer_Details
		'
		Me.pnl_005_Developer_Details.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_005_Developer_Details.Controls.Add(Me.lbl_Weight_005_Developer)
		Me.pnl_005_Developer_Details.Controls.Add(Me.lbl_005_Developer_A)
		Me.pnl_005_Developer_Details.Controls.Add(Me.lbl_005_Developer_B)
		Me.pnl_005_Developer_Details.Controls.Add(Me.lbl_Weight_005_Developer_Text)
		Me.pnl_005_Developer_Details.Controls.Add(Me.lbl_005_Developer_A_Text)
		Me.pnl_005_Developer_Details.Controls.Add(Me.lbl_005_Developer_B_Text)
		Me.pnl_005_Developer_Details.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_005_Developer_Details.Location = New System.Drawing.Point(0, 19)
		Me.pnl_005_Developer_Details.Name = "pnl_005_Developer_Details"
		Me.pnl_005_Developer_Details.Size = New System.Drawing.Size(383, 455)
		Me.pnl_005_Developer_Details.TabIndex = 1
		'
		'lbl_Weight_005_Developer
		'
		Me.lbl_Weight_005_Developer.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Weight_005_Developer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_005_Developer.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Weight_005_Developer.MKBoundControl1 = Nothing
		Me.lbl_Weight_005_Developer.MKBoundControl2 = Nothing
		Me.lbl_Weight_005_Developer.MKBoundControl3 = Nothing
		Me.lbl_Weight_005_Developer.MKBoundControl4 = Nothing
		Me.lbl_Weight_005_Developer.MKBoundControl5 = Nothing
		Me.lbl_Weight_005_Developer.Name = "lbl_Weight_005_Developer"
		Me.lbl_Weight_005_Developer.Size = New System.Drawing.Size(113, 20)
		Me.lbl_Weight_005_Developer.TabIndex = 7
		Me.lbl_Weight_005_Developer.Text = "Weight:"
		'
		'lbl_005_Developer_A
		'
		Me.lbl_005_Developer_A.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_005_Developer_A.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_005_Developer_A.Location = New System.Drawing.Point(3, 26)
		Me.lbl_005_Developer_A.MKBoundControl1 = Nothing
		Me.lbl_005_Developer_A.MKBoundControl2 = Nothing
		Me.lbl_005_Developer_A.MKBoundControl3 = Nothing
		Me.lbl_005_Developer_A.MKBoundControl4 = Nothing
		Me.lbl_005_Developer_A.MKBoundControl5 = Nothing
		Me.lbl_005_Developer_A.Name = "lbl_005_Developer_A"
		Me.lbl_005_Developer_A.Size = New System.Drawing.Size(113, 20)
		Me.lbl_005_Developer_A.TabIndex = 7
		Me.lbl_005_Developer_A.Text = "Developer of Game A:"
		'
		'lbl_005_Developer_B
		'
		Me.lbl_005_Developer_B.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_005_Developer_B.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_005_Developer_B.Location = New System.Drawing.Point(3, 49)
		Me.lbl_005_Developer_B.MKBoundControl1 = Nothing
		Me.lbl_005_Developer_B.MKBoundControl2 = Nothing
		Me.lbl_005_Developer_B.MKBoundControl3 = Nothing
		Me.lbl_005_Developer_B.MKBoundControl4 = Nothing
		Me.lbl_005_Developer_B.MKBoundControl5 = Nothing
		Me.lbl_005_Developer_B.Name = "lbl_005_Developer_B"
		Me.lbl_005_Developer_B.Size = New System.Drawing.Size(113, 20)
		Me.lbl_005_Developer_B.TabIndex = 7
		Me.lbl_005_Developer_B.Text = "Developer of Game B:"
		'
		'lbl_Weight_005_Developer_Text
		'
		Me.lbl_Weight_005_Developer_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Weight_005_Developer_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Weight_005_Developer_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_005_Developer_Text.Location = New System.Drawing.Point(119, 3)
		Me.lbl_Weight_005_Developer_Text.MKBoundControl1 = Nothing
		Me.lbl_Weight_005_Developer_Text.MKBoundControl2 = Nothing
		Me.lbl_Weight_005_Developer_Text.MKBoundControl3 = Nothing
		Me.lbl_Weight_005_Developer_Text.MKBoundControl4 = Nothing
		Me.lbl_Weight_005_Developer_Text.MKBoundControl5 = Nothing
		Me.lbl_Weight_005_Developer_Text.Name = "lbl_Weight_005_Developer_Text"
		Me.lbl_Weight_005_Developer_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_Weight_005_Developer_Text.TabIndex = 7
		'
		'lbl_005_Developer_A_Text
		'
		Me.lbl_005_Developer_A_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_005_Developer_A_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_005_Developer_A_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_005_Developer_A_Text.Location = New System.Drawing.Point(119, 26)
		Me.lbl_005_Developer_A_Text.MKBoundControl1 = Nothing
		Me.lbl_005_Developer_A_Text.MKBoundControl2 = Nothing
		Me.lbl_005_Developer_A_Text.MKBoundControl3 = Nothing
		Me.lbl_005_Developer_A_Text.MKBoundControl4 = Nothing
		Me.lbl_005_Developer_A_Text.MKBoundControl5 = Nothing
		Me.lbl_005_Developer_A_Text.Name = "lbl_005_Developer_A_Text"
		Me.lbl_005_Developer_A_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_005_Developer_A_Text.TabIndex = 7
		'
		'lbl_005_Developer_B_Text
		'
		Me.lbl_005_Developer_B_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_005_Developer_B_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_005_Developer_B_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_005_Developer_B_Text.Location = New System.Drawing.Point(119, 49)
		Me.lbl_005_Developer_B_Text.MKBoundControl1 = Nothing
		Me.lbl_005_Developer_B_Text.MKBoundControl2 = Nothing
		Me.lbl_005_Developer_B_Text.MKBoundControl3 = Nothing
		Me.lbl_005_Developer_B_Text.MKBoundControl4 = Nothing
		Me.lbl_005_Developer_B_Text.MKBoundControl5 = Nothing
		Me.lbl_005_Developer_B_Text.Name = "lbl_005_Developer_B_Text"
		Me.lbl_005_Developer_B_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_005_Developer_B_Text.TabIndex = 7
		'
		'lbl_005_Developer_Explanation
		'
		Me.lbl_005_Developer_Explanation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_005_Developer_Explanation.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_005_Developer_Explanation.Location = New System.Drawing.Point(0, 0)
		Me.lbl_005_Developer_Explanation.MKBoundControl1 = Nothing
		Me.lbl_005_Developer_Explanation.MKBoundControl2 = Nothing
		Me.lbl_005_Developer_Explanation.MKBoundControl3 = Nothing
		Me.lbl_005_Developer_Explanation.MKBoundControl4 = Nothing
		Me.lbl_005_Developer_Explanation.MKBoundControl5 = Nothing
		Me.lbl_005_Developer_Explanation.Name = "lbl_005_Developer_Explanation"
		Me.lbl_005_Developer_Explanation.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_005_Developer_Explanation.Size = New System.Drawing.Size(383, 19)
		Me.lbl_005_Developer_Explanation.TabIndex = 0
		Me.lbl_005_Developer_Explanation.Text = "If both developers match, a similarity score of 100 is applied, else 0."
		'
		'tpg_006_Year
		'
		Me.tpg_006_Year.Controls.Add(Me.pnl_006_Year)
		Me.tpg_006_Year.Name = "tpg_006_Year"
		Me.tpg_006_Year.Size = New System.Drawing.Size(383, 474)
		Me.tpg_006_Year.Text = "006_Year"
		'
		'pnl_006_Year
		'
		Me.pnl_006_Year.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_006_Year.Controls.Add(Me.pnl_006_Year_Details)
		Me.pnl_006_Year.Controls.Add(Me.lbl_006_Year_Explanation)
		Me.pnl_006_Year.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_006_Year.Location = New System.Drawing.Point(0, 0)
		Me.pnl_006_Year.Name = "pnl_006_Year"
		Me.pnl_006_Year.Size = New System.Drawing.Size(383, 474)
		Me.pnl_006_Year.TabIndex = 11
		'
		'pnl_006_Year_Details
		'
		Me.pnl_006_Year_Details.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_006_Year_Details.Controls.Add(Me.lbl_Weight_006_Year)
		Me.pnl_006_Year_Details.Controls.Add(Me.lbl_006_Year_A)
		Me.pnl_006_Year_Details.Controls.Add(Me.lbl_006_Year_B)
		Me.pnl_006_Year_Details.Controls.Add(Me.lbl_Weight_006_Year_Text)
		Me.pnl_006_Year_Details.Controls.Add(Me.lbl_006_Year_A_Text)
		Me.pnl_006_Year_Details.Controls.Add(Me.lbl_006_Year_B_Text)
		Me.pnl_006_Year_Details.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_006_Year_Details.Location = New System.Drawing.Point(0, 32)
		Me.pnl_006_Year_Details.Name = "pnl_006_Year_Details"
		Me.pnl_006_Year_Details.Size = New System.Drawing.Size(383, 442)
		Me.pnl_006_Year_Details.TabIndex = 1
		'
		'lbl_Weight_006_Year
		'
		Me.lbl_Weight_006_Year.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Weight_006_Year.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_006_Year.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Weight_006_Year.MKBoundControl1 = Nothing
		Me.lbl_Weight_006_Year.MKBoundControl2 = Nothing
		Me.lbl_Weight_006_Year.MKBoundControl3 = Nothing
		Me.lbl_Weight_006_Year.MKBoundControl4 = Nothing
		Me.lbl_Weight_006_Year.MKBoundControl5 = Nothing
		Me.lbl_Weight_006_Year.Name = "lbl_Weight_006_Year"
		Me.lbl_Weight_006_Year.Size = New System.Drawing.Size(113, 20)
		Me.lbl_Weight_006_Year.TabIndex = 7
		Me.lbl_Weight_006_Year.Text = "Weight:"
		'
		'lbl_006_Year_A
		'
		Me.lbl_006_Year_A.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_006_Year_A.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_006_Year_A.Location = New System.Drawing.Point(3, 26)
		Me.lbl_006_Year_A.MKBoundControl1 = Nothing
		Me.lbl_006_Year_A.MKBoundControl2 = Nothing
		Me.lbl_006_Year_A.MKBoundControl3 = Nothing
		Me.lbl_006_Year_A.MKBoundControl4 = Nothing
		Me.lbl_006_Year_A.MKBoundControl5 = Nothing
		Me.lbl_006_Year_A.Name = "lbl_006_Year_A"
		Me.lbl_006_Year_A.Size = New System.Drawing.Size(113, 20)
		Me.lbl_006_Year_A.TabIndex = 7
		Me.lbl_006_Year_A.Text = "Year of Game A:"
		'
		'lbl_006_Year_B
		'
		Me.lbl_006_Year_B.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_006_Year_B.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_006_Year_B.Location = New System.Drawing.Point(3, 49)
		Me.lbl_006_Year_B.MKBoundControl1 = Nothing
		Me.lbl_006_Year_B.MKBoundControl2 = Nothing
		Me.lbl_006_Year_B.MKBoundControl3 = Nothing
		Me.lbl_006_Year_B.MKBoundControl4 = Nothing
		Me.lbl_006_Year_B.MKBoundControl5 = Nothing
		Me.lbl_006_Year_B.Name = "lbl_006_Year_B"
		Me.lbl_006_Year_B.Size = New System.Drawing.Size(113, 20)
		Me.lbl_006_Year_B.TabIndex = 7
		Me.lbl_006_Year_B.Text = "Year of Game B:"
		'
		'lbl_Weight_006_Year_Text
		'
		Me.lbl_Weight_006_Year_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Weight_006_Year_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Weight_006_Year_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_006_Year_Text.Location = New System.Drawing.Point(119, 3)
		Me.lbl_Weight_006_Year_Text.MKBoundControl1 = Nothing
		Me.lbl_Weight_006_Year_Text.MKBoundControl2 = Nothing
		Me.lbl_Weight_006_Year_Text.MKBoundControl3 = Nothing
		Me.lbl_Weight_006_Year_Text.MKBoundControl4 = Nothing
		Me.lbl_Weight_006_Year_Text.MKBoundControl5 = Nothing
		Me.lbl_Weight_006_Year_Text.Name = "lbl_Weight_006_Year_Text"
		Me.lbl_Weight_006_Year_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_Weight_006_Year_Text.TabIndex = 7
		'
		'lbl_006_Year_A_Text
		'
		Me.lbl_006_Year_A_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_006_Year_A_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_006_Year_A_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_006_Year_A_Text.Location = New System.Drawing.Point(119, 26)
		Me.lbl_006_Year_A_Text.MKBoundControl1 = Nothing
		Me.lbl_006_Year_A_Text.MKBoundControl2 = Nothing
		Me.lbl_006_Year_A_Text.MKBoundControl3 = Nothing
		Me.lbl_006_Year_A_Text.MKBoundControl4 = Nothing
		Me.lbl_006_Year_A_Text.MKBoundControl5 = Nothing
		Me.lbl_006_Year_A_Text.Name = "lbl_006_Year_A_Text"
		Me.lbl_006_Year_A_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_006_Year_A_Text.TabIndex = 7
		'
		'lbl_006_Year_B_Text
		'
		Me.lbl_006_Year_B_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_006_Year_B_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_006_Year_B_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_006_Year_B_Text.Location = New System.Drawing.Point(119, 49)
		Me.lbl_006_Year_B_Text.MKBoundControl1 = Nothing
		Me.lbl_006_Year_B_Text.MKBoundControl2 = Nothing
		Me.lbl_006_Year_B_Text.MKBoundControl3 = Nothing
		Me.lbl_006_Year_B_Text.MKBoundControl4 = Nothing
		Me.lbl_006_Year_B_Text.MKBoundControl5 = Nothing
		Me.lbl_006_Year_B_Text.Name = "lbl_006_Year_B_Text"
		Me.lbl_006_Year_B_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_006_Year_B_Text.TabIndex = 7
		'
		'lbl_006_Year_Explanation
		'
		Me.lbl_006_Year_Explanation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_006_Year_Explanation.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_006_Year_Explanation.Location = New System.Drawing.Point(0, 0)
		Me.lbl_006_Year_Explanation.MKBoundControl1 = Nothing
		Me.lbl_006_Year_Explanation.MKBoundControl2 = Nothing
		Me.lbl_006_Year_Explanation.MKBoundControl3 = Nothing
		Me.lbl_006_Year_Explanation.MKBoundControl4 = Nothing
		Me.lbl_006_Year_Explanation.MKBoundControl5 = Nothing
		Me.lbl_006_Year_Explanation.Name = "lbl_006_Year_Explanation"
		Me.lbl_006_Year_Explanation.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_006_Year_Explanation.Size = New System.Drawing.Size(383, 32)
		Me.lbl_006_Year_Explanation.TabIndex = 0
		Me.lbl_006_Year_Explanation.Text = "The similarity score of 100 gets reduced by 10 for each 2-year difference in both" &
		" years."
		'
		'tpg_101_Basic_Genres
		'
		Me.tpg_101_Basic_Genres.Controls.Add(Me.ucr_101_Basic_Genres)
		Me.tpg_101_Basic_Genres.Name = "tpg_101_Basic_Genres"
		Me.tpg_101_Basic_Genres.Size = New System.Drawing.Size(383, 474)
		Me.tpg_101_Basic_Genres.Text = "101_Basic_Genres"
		'
		'ucr_101_Basic_Genres
		'
		Me.ucr_101_Basic_Genres.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ucr_101_Basic_Genres.Location = New System.Drawing.Point(0, 0)
		Me.ucr_101_Basic_Genres.Name = "ucr_101_Basic_Genres"
		Me.ucr_101_Basic_Genres.Size = New System.Drawing.Size(383, 474)
		Me.ucr_101_Basic_Genres.TabIndex = 1
		'
		'tpg_102_Perspectives
		'
		Me.tpg_102_Perspectives.Controls.Add(Me.ucr_102_Perspectives)
		Me.tpg_102_Perspectives.Name = "tpg_102_Perspectives"
		Me.tpg_102_Perspectives.Size = New System.Drawing.Size(383, 474)
		Me.tpg_102_Perspectives.Text = "102_Perspectives"
		'
		'ucr_102_Perspectives
		'
		Me.ucr_102_Perspectives.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ucr_102_Perspectives.Location = New System.Drawing.Point(0, 0)
		Me.ucr_102_Perspectives.Name = "ucr_102_Perspectives"
		Me.ucr_102_Perspectives.Size = New System.Drawing.Size(383, 474)
		Me.ucr_102_Perspectives.TabIndex = 1
		'
		'tpg_107_Visual_Presentation
		'
		Me.tpg_107_Visual_Presentation.Controls.Add(Me.ucr_107_Visual_Presentation)
		Me.tpg_107_Visual_Presentation.Name = "tpg_107_Visual_Presentation"
		Me.tpg_107_Visual_Presentation.Size = New System.Drawing.Size(383, 474)
		Me.tpg_107_Visual_Presentation.Text = "107_Visual_Presentation"
		'
		'ucr_107_Visual_Presentation
		'
		Me.ucr_107_Visual_Presentation.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ucr_107_Visual_Presentation.Location = New System.Drawing.Point(0, 0)
		Me.ucr_107_Visual_Presentation.Name = "ucr_107_Visual_Presentation"
		Me.ucr_107_Visual_Presentation.Size = New System.Drawing.Size(383, 474)
		Me.ucr_107_Visual_Presentation.TabIndex = 0
		'
		'tpg_108_Gameplay
		'
		Me.tpg_108_Gameplay.Controls.Add(Me.ucr_108_Gameplay)
		Me.tpg_108_Gameplay.Name = "tpg_108_Gameplay"
		Me.tpg_108_Gameplay.Size = New System.Drawing.Size(383, 474)
		Me.tpg_108_Gameplay.Text = "108_Gameplay"
		'
		'ucr_108_Gameplay
		'
		Me.ucr_108_Gameplay.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ucr_108_Gameplay.Location = New System.Drawing.Point(0, 0)
		Me.ucr_108_Gameplay.Name = "ucr_108_Gameplay"
		Me.ucr_108_Gameplay.Size = New System.Drawing.Size(383, 474)
		Me.ucr_108_Gameplay.TabIndex = 1
		'
		'tpg_109_Pacing
		'
		Me.tpg_109_Pacing.Controls.Add(Me.ucr_109_Pacing)
		Me.tpg_109_Pacing.Name = "tpg_109_Pacing"
		Me.tpg_109_Pacing.Size = New System.Drawing.Size(383, 474)
		Me.tpg_109_Pacing.Text = "109_Pacing"
		'
		'ucr_109_Pacing
		'
		Me.ucr_109_Pacing.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ucr_109_Pacing.Location = New System.Drawing.Point(0, 0)
		Me.ucr_109_Pacing.Name = "ucr_109_Pacing"
		Me.ucr_109_Pacing.Size = New System.Drawing.Size(383, 474)
		Me.ucr_109_Pacing.TabIndex = 2
		'
		'tpg_110_Narrative_Theme_Topic
		'
		Me.tpg_110_Narrative_Theme_Topic.Controls.Add(Me.ucr_110_Narrative_Theme_Topic)
		Me.tpg_110_Narrative_Theme_Topic.Name = "tpg_110_Narrative_Theme_Topic"
		Me.tpg_110_Narrative_Theme_Topic.Size = New System.Drawing.Size(383, 474)
		Me.tpg_110_Narrative_Theme_Topic.Text = "110_Narrative_Theme_Topic"
		'
		'ucr_110_Narrative_Theme_Topic
		'
		Me.ucr_110_Narrative_Theme_Topic.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ucr_110_Narrative_Theme_Topic.Location = New System.Drawing.Point(0, 0)
		Me.ucr_110_Narrative_Theme_Topic.Name = "ucr_110_Narrative_Theme_Topic"
		Me.ucr_110_Narrative_Theme_Topic.Size = New System.Drawing.Size(383, 474)
		Me.ucr_110_Narrative_Theme_Topic.TabIndex = 2
		'
		'tpg_111_Setting
		'
		Me.tpg_111_Setting.Controls.Add(Me.ucr_111_Setting)
		Me.tpg_111_Setting.Name = "tpg_111_Setting"
		Me.tpg_111_Setting.Size = New System.Drawing.Size(383, 474)
		Me.tpg_111_Setting.Text = "111_Setting"
		'
		'ucr_111_Setting
		'
		Me.ucr_111_Setting.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ucr_111_Setting.Location = New System.Drawing.Point(0, 0)
		Me.ucr_111_Setting.Name = "ucr_111_Setting"
		Me.ucr_111_Setting.Size = New System.Drawing.Size(383, 474)
		Me.ucr_111_Setting.TabIndex = 2
		'
		'tpg_103_Sports_Themes
		'
		Me.tpg_103_Sports_Themes.Controls.Add(Me.ucr_103_Sports_Themes)
		Me.tpg_103_Sports_Themes.Name = "tpg_103_Sports_Themes"
		Me.tpg_103_Sports_Themes.Size = New System.Drawing.Size(383, 474)
		Me.tpg_103_Sports_Themes.Text = "103_Sports_Themes"
		'
		'ucr_103_Sports_Themes
		'
		Me.ucr_103_Sports_Themes.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ucr_103_Sports_Themes.Location = New System.Drawing.Point(0, 0)
		Me.ucr_103_Sports_Themes.Name = "ucr_103_Sports_Themes"
		Me.ucr_103_Sports_Themes.Size = New System.Drawing.Size(383, 474)
		Me.ucr_103_Sports_Themes.TabIndex = 1
		'
		'tpg_112_Vehicular_Themes
		'
		Me.tpg_112_Vehicular_Themes.Controls.Add(Me.ucr_112_Vehicular_Themes)
		Me.tpg_112_Vehicular_Themes.Name = "tpg_112_Vehicular_Themes"
		Me.tpg_112_Vehicular_Themes.Size = New System.Drawing.Size(383, 474)
		Me.tpg_112_Vehicular_Themes.Text = "112_Vehicular_Themes"
		'
		'ucr_112_Vehicular_Themes
		'
		Me.ucr_112_Vehicular_Themes.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ucr_112_Vehicular_Themes.Location = New System.Drawing.Point(0, 0)
		Me.ucr_112_Vehicular_Themes.Name = "ucr_112_Vehicular_Themes"
		Me.ucr_112_Vehicular_Themes.Size = New System.Drawing.Size(383, 474)
		Me.ucr_112_Vehicular_Themes.TabIndex = 2
		'
		'tpg_105_Educational_Categories
		'
		Me.tpg_105_Educational_Categories.Controls.Add(Me.ucr_105_Educational_Categories)
		Me.tpg_105_Educational_Categories.Name = "tpg_105_Educational_Categories"
		Me.tpg_105_Educational_Categories.Size = New System.Drawing.Size(383, 474)
		Me.tpg_105_Educational_Categories.Text = "105_Educational_Categories"
		'
		'ucr_105_Educational_Categories
		'
		Me.ucr_105_Educational_Categories.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ucr_105_Educational_Categories.Location = New System.Drawing.Point(0, 0)
		Me.ucr_105_Educational_Categories.Name = "ucr_105_Educational_Categories"
		Me.ucr_105_Educational_Categories.Size = New System.Drawing.Size(383, 474)
		Me.ucr_105_Educational_Categories.TabIndex = 1
		'
		'tpg_113_Interface_Control
		'
		Me.tpg_113_Interface_Control.Controls.Add(Me.ucr_113_Interface_Control)
		Me.tpg_113_Interface_Control.Name = "tpg_113_Interface_Control"
		Me.tpg_113_Interface_Control.Size = New System.Drawing.Size(383, 474)
		Me.tpg_113_Interface_Control.Text = "113_Interface_Control"
		'
		'ucr_113_Interface_Control
		'
		Me.ucr_113_Interface_Control.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ucr_113_Interface_Control.Location = New System.Drawing.Point(0, 0)
		Me.ucr_113_Interface_Control.Name = "ucr_113_Interface_Control"
		Me.ucr_113_Interface_Control.Size = New System.Drawing.Size(383, 474)
		Me.ucr_113_Interface_Control.TabIndex = 2
		'
		'tpg_114_DLC_Addon
		'
		Me.tpg_114_DLC_Addon.Controls.Add(Me.ucr_114_DLC_Addon)
		Me.tpg_114_DLC_Addon.Name = "tpg_114_DLC_Addon"
		Me.tpg_114_DLC_Addon.Size = New System.Drawing.Size(383, 474)
		Me.tpg_114_DLC_Addon.Text = "114_DLC_Addon"
		'
		'ucr_114_DLC_Addon
		'
		Me.ucr_114_DLC_Addon.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ucr_114_DLC_Addon.Location = New System.Drawing.Point(0, 0)
		Me.ucr_114_DLC_Addon.Name = "ucr_114_DLC_Addon"
		Me.ucr_114_DLC_Addon.Size = New System.Drawing.Size(383, 474)
		Me.ucr_114_DLC_Addon.TabIndex = 2
		'
		'tpg_115_Special_Edition
		'
		Me.tpg_115_Special_Edition.Controls.Add(Me.ucr_115_Special_Edition)
		Me.tpg_115_Special_Edition.Name = "tpg_115_Special_Edition"
		Me.tpg_115_Special_Edition.Size = New System.Drawing.Size(383, 474)
		Me.tpg_115_Special_Edition.Text = "115_Special_Edition"
		'
		'ucr_115_Special_Edition
		'
		Me.ucr_115_Special_Edition.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ucr_115_Special_Edition.Location = New System.Drawing.Point(0, 0)
		Me.ucr_115_Special_Edition.Name = "ucr_115_Special_Edition"
		Me.ucr_115_Special_Edition.Size = New System.Drawing.Size(383, 474)
		Me.ucr_115_Special_Edition.TabIndex = 2
		'
		'tpg_106_Other_Attributes
		'
		Me.tpg_106_Other_Attributes.Controls.Add(Me.ucr_106_Other_Attributes)
		Me.tpg_106_Other_Attributes.Name = "tpg_106_Other_Attributes"
		Me.tpg_106_Other_Attributes.Size = New System.Drawing.Size(383, 474)
		Me.tpg_106_Other_Attributes.Text = "106_Other_Attributes"
		'
		'ucr_106_Other_Attributes
		'
		Me.ucr_106_Other_Attributes.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ucr_106_Other_Attributes.Location = New System.Drawing.Point(0, 0)
		Me.ucr_106_Other_Attributes.Name = "ucr_106_Other_Attributes"
		Me.ucr_106_Other_Attributes.Size = New System.Drawing.Size(383, 474)
		Me.ucr_106_Other_Attributes.TabIndex = 1
		'
		'tpg_201_MinPlayers
		'
		Me.tpg_201_MinPlayers.Controls.Add(Me.pnl_201_MinPlayers)
		Me.tpg_201_MinPlayers.Name = "tpg_201_MinPlayers"
		Me.tpg_201_MinPlayers.Size = New System.Drawing.Size(383, 474)
		Me.tpg_201_MinPlayers.Text = "201_MinPlayers"
		'
		'pnl_201_MinPlayers
		'
		Me.pnl_201_MinPlayers.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_201_MinPlayers.Controls.Add(Me.pnl_201_MinPlayers_Details)
		Me.pnl_201_MinPlayers.Controls.Add(Me.lbl_201_MinPlayers_Explanation)
		Me.pnl_201_MinPlayers.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_201_MinPlayers.Location = New System.Drawing.Point(0, 0)
		Me.pnl_201_MinPlayers.Name = "pnl_201_MinPlayers"
		Me.pnl_201_MinPlayers.Size = New System.Drawing.Size(383, 474)
		Me.pnl_201_MinPlayers.TabIndex = 9
		'
		'pnl_201_MinPlayers_Details
		'
		Me.pnl_201_MinPlayers_Details.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_201_MinPlayers_Details.Controls.Add(Me.lbl_Weight_201_MinPlayers)
		Me.pnl_201_MinPlayers_Details.Controls.Add(Me.lbl_201_MinPlayers_A)
		Me.pnl_201_MinPlayers_Details.Controls.Add(Me.lbl_201_MinPlayers_B)
		Me.pnl_201_MinPlayers_Details.Controls.Add(Me.lbl_Weight_201_MinPlayers_Text)
		Me.pnl_201_MinPlayers_Details.Controls.Add(Me.lbl_201_MinPlayers_A_Text)
		Me.pnl_201_MinPlayers_Details.Controls.Add(Me.lbl_201_MinPlayers_B_Text)
		Me.pnl_201_MinPlayers_Details.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_201_MinPlayers_Details.Location = New System.Drawing.Point(0, 32)
		Me.pnl_201_MinPlayers_Details.Name = "pnl_201_MinPlayers_Details"
		Me.pnl_201_MinPlayers_Details.Size = New System.Drawing.Size(383, 442)
		Me.pnl_201_MinPlayers_Details.TabIndex = 1
		'
		'lbl_Weight_201_MinPlayers
		'
		Me.lbl_Weight_201_MinPlayers.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Weight_201_MinPlayers.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_201_MinPlayers.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Weight_201_MinPlayers.MKBoundControl1 = Nothing
		Me.lbl_Weight_201_MinPlayers.MKBoundControl2 = Nothing
		Me.lbl_Weight_201_MinPlayers.MKBoundControl3 = Nothing
		Me.lbl_Weight_201_MinPlayers.MKBoundControl4 = Nothing
		Me.lbl_Weight_201_MinPlayers.MKBoundControl5 = Nothing
		Me.lbl_Weight_201_MinPlayers.Name = "lbl_Weight_201_MinPlayers"
		Me.lbl_Weight_201_MinPlayers.Size = New System.Drawing.Size(113, 20)
		Me.lbl_Weight_201_MinPlayers.TabIndex = 7
		Me.lbl_Weight_201_MinPlayers.Text = "Weight:"
		'
		'lbl_201_MinPlayers_A
		'
		Me.lbl_201_MinPlayers_A.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_201_MinPlayers_A.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_201_MinPlayers_A.Location = New System.Drawing.Point(3, 26)
		Me.lbl_201_MinPlayers_A.MKBoundControl1 = Nothing
		Me.lbl_201_MinPlayers_A.MKBoundControl2 = Nothing
		Me.lbl_201_MinPlayers_A.MKBoundControl3 = Nothing
		Me.lbl_201_MinPlayers_A.MKBoundControl4 = Nothing
		Me.lbl_201_MinPlayers_A.MKBoundControl5 = Nothing
		Me.lbl_201_MinPlayers_A.Name = "lbl_201_MinPlayers_A"
		Me.lbl_201_MinPlayers_A.Size = New System.Drawing.Size(113, 20)
		Me.lbl_201_MinPlayers_A.TabIndex = 7
		Me.lbl_201_MinPlayers_A.Text = "Min. Players of Game A:"
		'
		'lbl_201_MinPlayers_B
		'
		Me.lbl_201_MinPlayers_B.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_201_MinPlayers_B.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_201_MinPlayers_B.Location = New System.Drawing.Point(3, 49)
		Me.lbl_201_MinPlayers_B.MKBoundControl1 = Nothing
		Me.lbl_201_MinPlayers_B.MKBoundControl2 = Nothing
		Me.lbl_201_MinPlayers_B.MKBoundControl3 = Nothing
		Me.lbl_201_MinPlayers_B.MKBoundControl4 = Nothing
		Me.lbl_201_MinPlayers_B.MKBoundControl5 = Nothing
		Me.lbl_201_MinPlayers_B.Name = "lbl_201_MinPlayers_B"
		Me.lbl_201_MinPlayers_B.Size = New System.Drawing.Size(113, 20)
		Me.lbl_201_MinPlayers_B.TabIndex = 7
		Me.lbl_201_MinPlayers_B.Text = "Min. Players of Game B:"
		'
		'lbl_Weight_201_MinPlayers_Text
		'
		Me.lbl_Weight_201_MinPlayers_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Weight_201_MinPlayers_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Weight_201_MinPlayers_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_201_MinPlayers_Text.Location = New System.Drawing.Point(119, 3)
		Me.lbl_Weight_201_MinPlayers_Text.MKBoundControl1 = Nothing
		Me.lbl_Weight_201_MinPlayers_Text.MKBoundControl2 = Nothing
		Me.lbl_Weight_201_MinPlayers_Text.MKBoundControl3 = Nothing
		Me.lbl_Weight_201_MinPlayers_Text.MKBoundControl4 = Nothing
		Me.lbl_Weight_201_MinPlayers_Text.MKBoundControl5 = Nothing
		Me.lbl_Weight_201_MinPlayers_Text.Name = "lbl_Weight_201_MinPlayers_Text"
		Me.lbl_Weight_201_MinPlayers_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_Weight_201_MinPlayers_Text.TabIndex = 7
		'
		'lbl_201_MinPlayers_A_Text
		'
		Me.lbl_201_MinPlayers_A_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_201_MinPlayers_A_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_201_MinPlayers_A_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_201_MinPlayers_A_Text.Location = New System.Drawing.Point(119, 26)
		Me.lbl_201_MinPlayers_A_Text.MKBoundControl1 = Nothing
		Me.lbl_201_MinPlayers_A_Text.MKBoundControl2 = Nothing
		Me.lbl_201_MinPlayers_A_Text.MKBoundControl3 = Nothing
		Me.lbl_201_MinPlayers_A_Text.MKBoundControl4 = Nothing
		Me.lbl_201_MinPlayers_A_Text.MKBoundControl5 = Nothing
		Me.lbl_201_MinPlayers_A_Text.Name = "lbl_201_MinPlayers_A_Text"
		Me.lbl_201_MinPlayers_A_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_201_MinPlayers_A_Text.TabIndex = 7
		'
		'lbl_201_MinPlayers_B_Text
		'
		Me.lbl_201_MinPlayers_B_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_201_MinPlayers_B_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_201_MinPlayers_B_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_201_MinPlayers_B_Text.Location = New System.Drawing.Point(119, 49)
		Me.lbl_201_MinPlayers_B_Text.MKBoundControl1 = Nothing
		Me.lbl_201_MinPlayers_B_Text.MKBoundControl2 = Nothing
		Me.lbl_201_MinPlayers_B_Text.MKBoundControl3 = Nothing
		Me.lbl_201_MinPlayers_B_Text.MKBoundControl4 = Nothing
		Me.lbl_201_MinPlayers_B_Text.MKBoundControl5 = Nothing
		Me.lbl_201_MinPlayers_B_Text.Name = "lbl_201_MinPlayers_B_Text"
		Me.lbl_201_MinPlayers_B_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_201_MinPlayers_B_Text.TabIndex = 7
		'
		'lbl_201_MinPlayers_Explanation
		'
		Me.lbl_201_MinPlayers_Explanation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_201_MinPlayers_Explanation.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_201_MinPlayers_Explanation.Location = New System.Drawing.Point(0, 0)
		Me.lbl_201_MinPlayers_Explanation.MKBoundControl1 = Nothing
		Me.lbl_201_MinPlayers_Explanation.MKBoundControl2 = Nothing
		Me.lbl_201_MinPlayers_Explanation.MKBoundControl3 = Nothing
		Me.lbl_201_MinPlayers_Explanation.MKBoundControl4 = Nothing
		Me.lbl_201_MinPlayers_Explanation.MKBoundControl5 = Nothing
		Me.lbl_201_MinPlayers_Explanation.Name = "lbl_201_MinPlayers_Explanation"
		Me.lbl_201_MinPlayers_Explanation.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_201_MinPlayers_Explanation.Size = New System.Drawing.Size(383, 32)
		Me.lbl_201_MinPlayers_Explanation.TabIndex = 0
		Me.lbl_201_MinPlayers_Explanation.Text = "The similarity score of 100 is reduced by 10 times the difference between the min" &
		"umum number of players supported by both games."
		'
		'tpg_202_MaxPlayers
		'
		Me.tpg_202_MaxPlayers.Controls.Add(Me.pnl_202_MaxPlayers)
		Me.tpg_202_MaxPlayers.Name = "tpg_202_MaxPlayers"
		Me.tpg_202_MaxPlayers.Size = New System.Drawing.Size(383, 474)
		Me.tpg_202_MaxPlayers.Text = "202_MaxPlayers"
		'
		'pnl_202_MaxPlayers
		'
		Me.pnl_202_MaxPlayers.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_202_MaxPlayers.Controls.Add(Me.pnl_202_MaxPlayers_Details)
		Me.pnl_202_MaxPlayers.Controls.Add(Me.lbl_202_MaxPlayers_Explanation)
		Me.pnl_202_MaxPlayers.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_202_MaxPlayers.Location = New System.Drawing.Point(0, 0)
		Me.pnl_202_MaxPlayers.Name = "pnl_202_MaxPlayers"
		Me.pnl_202_MaxPlayers.Size = New System.Drawing.Size(383, 474)
		Me.pnl_202_MaxPlayers.TabIndex = 10
		'
		'pnl_202_MaxPlayers_Details
		'
		Me.pnl_202_MaxPlayers_Details.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_202_MaxPlayers_Details.Controls.Add(Me.lbl_Weight_202_MaxPlayers)
		Me.pnl_202_MaxPlayers_Details.Controls.Add(Me.lbl_202_MaxPlayers_A)
		Me.pnl_202_MaxPlayers_Details.Controls.Add(Me.lbl_202_MaxPlayers_B)
		Me.pnl_202_MaxPlayers_Details.Controls.Add(Me.lbl_Weight_202_MaxPlayers_Text)
		Me.pnl_202_MaxPlayers_Details.Controls.Add(Me.lbl_202_MaxPlayers_A_Text)
		Me.pnl_202_MaxPlayers_Details.Controls.Add(Me.lbl_202_MaxPlayers_B_Text)
		Me.pnl_202_MaxPlayers_Details.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_202_MaxPlayers_Details.Location = New System.Drawing.Point(0, 32)
		Me.pnl_202_MaxPlayers_Details.Name = "pnl_202_MaxPlayers_Details"
		Me.pnl_202_MaxPlayers_Details.Size = New System.Drawing.Size(383, 442)
		Me.pnl_202_MaxPlayers_Details.TabIndex = 1
		'
		'lbl_Weight_202_MaxPlayers
		'
		Me.lbl_Weight_202_MaxPlayers.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Weight_202_MaxPlayers.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_202_MaxPlayers.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Weight_202_MaxPlayers.MKBoundControl1 = Nothing
		Me.lbl_Weight_202_MaxPlayers.MKBoundControl2 = Nothing
		Me.lbl_Weight_202_MaxPlayers.MKBoundControl3 = Nothing
		Me.lbl_Weight_202_MaxPlayers.MKBoundControl4 = Nothing
		Me.lbl_Weight_202_MaxPlayers.MKBoundControl5 = Nothing
		Me.lbl_Weight_202_MaxPlayers.Name = "lbl_Weight_202_MaxPlayers"
		Me.lbl_Weight_202_MaxPlayers.Size = New System.Drawing.Size(113, 20)
		Me.lbl_Weight_202_MaxPlayers.TabIndex = 7
		Me.lbl_Weight_202_MaxPlayers.Text = "Weight:"
		'
		'lbl_202_MaxPlayers_A
		'
		Me.lbl_202_MaxPlayers_A.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_202_MaxPlayers_A.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_202_MaxPlayers_A.Location = New System.Drawing.Point(3, 26)
		Me.lbl_202_MaxPlayers_A.MKBoundControl1 = Nothing
		Me.lbl_202_MaxPlayers_A.MKBoundControl2 = Nothing
		Me.lbl_202_MaxPlayers_A.MKBoundControl3 = Nothing
		Me.lbl_202_MaxPlayers_A.MKBoundControl4 = Nothing
		Me.lbl_202_MaxPlayers_A.MKBoundControl5 = Nothing
		Me.lbl_202_MaxPlayers_A.Name = "lbl_202_MaxPlayers_A"
		Me.lbl_202_MaxPlayers_A.Size = New System.Drawing.Size(113, 20)
		Me.lbl_202_MaxPlayers_A.TabIndex = 7
		Me.lbl_202_MaxPlayers_A.Text = "Max. Players of Game A:"
		'
		'lbl_202_MaxPlayers_B
		'
		Me.lbl_202_MaxPlayers_B.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_202_MaxPlayers_B.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_202_MaxPlayers_B.Location = New System.Drawing.Point(3, 49)
		Me.lbl_202_MaxPlayers_B.MKBoundControl1 = Nothing
		Me.lbl_202_MaxPlayers_B.MKBoundControl2 = Nothing
		Me.lbl_202_MaxPlayers_B.MKBoundControl3 = Nothing
		Me.lbl_202_MaxPlayers_B.MKBoundControl4 = Nothing
		Me.lbl_202_MaxPlayers_B.MKBoundControl5 = Nothing
		Me.lbl_202_MaxPlayers_B.Name = "lbl_202_MaxPlayers_B"
		Me.lbl_202_MaxPlayers_B.Size = New System.Drawing.Size(113, 20)
		Me.lbl_202_MaxPlayers_B.TabIndex = 7
		Me.lbl_202_MaxPlayers_B.Text = "Max. Players of Game B:"
		'
		'lbl_Weight_202_MaxPlayers_Text
		'
		Me.lbl_Weight_202_MaxPlayers_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Weight_202_MaxPlayers_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Weight_202_MaxPlayers_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_202_MaxPlayers_Text.Location = New System.Drawing.Point(119, 3)
		Me.lbl_Weight_202_MaxPlayers_Text.MKBoundControl1 = Nothing
		Me.lbl_Weight_202_MaxPlayers_Text.MKBoundControl2 = Nothing
		Me.lbl_Weight_202_MaxPlayers_Text.MKBoundControl3 = Nothing
		Me.lbl_Weight_202_MaxPlayers_Text.MKBoundControl4 = Nothing
		Me.lbl_Weight_202_MaxPlayers_Text.MKBoundControl5 = Nothing
		Me.lbl_Weight_202_MaxPlayers_Text.Name = "lbl_Weight_202_MaxPlayers_Text"
		Me.lbl_Weight_202_MaxPlayers_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_Weight_202_MaxPlayers_Text.TabIndex = 7
		'
		'lbl_202_MaxPlayers_A_Text
		'
		Me.lbl_202_MaxPlayers_A_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_202_MaxPlayers_A_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_202_MaxPlayers_A_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_202_MaxPlayers_A_Text.Location = New System.Drawing.Point(119, 26)
		Me.lbl_202_MaxPlayers_A_Text.MKBoundControl1 = Nothing
		Me.lbl_202_MaxPlayers_A_Text.MKBoundControl2 = Nothing
		Me.lbl_202_MaxPlayers_A_Text.MKBoundControl3 = Nothing
		Me.lbl_202_MaxPlayers_A_Text.MKBoundControl4 = Nothing
		Me.lbl_202_MaxPlayers_A_Text.MKBoundControl5 = Nothing
		Me.lbl_202_MaxPlayers_A_Text.Name = "lbl_202_MaxPlayers_A_Text"
		Me.lbl_202_MaxPlayers_A_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_202_MaxPlayers_A_Text.TabIndex = 7
		'
		'lbl_202_MaxPlayers_B_Text
		'
		Me.lbl_202_MaxPlayers_B_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_202_MaxPlayers_B_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_202_MaxPlayers_B_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_202_MaxPlayers_B_Text.Location = New System.Drawing.Point(119, 49)
		Me.lbl_202_MaxPlayers_B_Text.MKBoundControl1 = Nothing
		Me.lbl_202_MaxPlayers_B_Text.MKBoundControl2 = Nothing
		Me.lbl_202_MaxPlayers_B_Text.MKBoundControl3 = Nothing
		Me.lbl_202_MaxPlayers_B_Text.MKBoundControl4 = Nothing
		Me.lbl_202_MaxPlayers_B_Text.MKBoundControl5 = Nothing
		Me.lbl_202_MaxPlayers_B_Text.Name = "lbl_202_MaxPlayers_B_Text"
		Me.lbl_202_MaxPlayers_B_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_202_MaxPlayers_B_Text.TabIndex = 7
		'
		'lbl_202_MaxPlayers_Explanation
		'
		Me.lbl_202_MaxPlayers_Explanation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_202_MaxPlayers_Explanation.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_202_MaxPlayers_Explanation.Location = New System.Drawing.Point(0, 0)
		Me.lbl_202_MaxPlayers_Explanation.MKBoundControl1 = Nothing
		Me.lbl_202_MaxPlayers_Explanation.MKBoundControl2 = Nothing
		Me.lbl_202_MaxPlayers_Explanation.MKBoundControl3 = Nothing
		Me.lbl_202_MaxPlayers_Explanation.MKBoundControl4 = Nothing
		Me.lbl_202_MaxPlayers_Explanation.MKBoundControl5 = Nothing
		Me.lbl_202_MaxPlayers_Explanation.Name = "lbl_202_MaxPlayers_Explanation"
		Me.lbl_202_MaxPlayers_Explanation.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_202_MaxPlayers_Explanation.Size = New System.Drawing.Size(383, 32)
		Me.lbl_202_MaxPlayers_Explanation.TabIndex = 0
		Me.lbl_202_MaxPlayers_Explanation.Text = "The similarity score of 100 is reduced by 10 times the difference between the max" &
		"imum number of players supported by both games."
		'
		'tpg_203_AgeO
		'
		Me.tpg_203_AgeO.Controls.Add(Me.pnl_203_AgeO)
		Me.tpg_203_AgeO.Name = "tpg_203_AgeO"
		Me.tpg_203_AgeO.Size = New System.Drawing.Size(383, 474)
		Me.tpg_203_AgeO.Text = "203_AgeO"
		'
		'pnl_203_AgeO
		'
		Me.pnl_203_AgeO.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_203_AgeO.Controls.Add(Me.pnl_203_AgeO_Details)
		Me.pnl_203_AgeO.Controls.Add(Me.lbl_203_AgeO_Explanation)
		Me.pnl_203_AgeO.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_203_AgeO.Location = New System.Drawing.Point(0, 0)
		Me.pnl_203_AgeO.Name = "pnl_203_AgeO"
		Me.pnl_203_AgeO.Size = New System.Drawing.Size(383, 474)
		Me.pnl_203_AgeO.TabIndex = 11
		'
		'pnl_203_AgeO_Details
		'
		Me.pnl_203_AgeO_Details.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_203_AgeO_Details.Controls.Add(Me.lbl_Weight_203_AgeO)
		Me.pnl_203_AgeO_Details.Controls.Add(Me.lbl_203_AgeO_A)
		Me.pnl_203_AgeO_Details.Controls.Add(Me.lbl_203_AgeO_B)
		Me.pnl_203_AgeO_Details.Controls.Add(Me.lbl_Weight_203_AgeO_Text)
		Me.pnl_203_AgeO_Details.Controls.Add(Me.lbl_203_AgeO_A_Text)
		Me.pnl_203_AgeO_Details.Controls.Add(Me.lbl_203_AgeO_B_Text)
		Me.pnl_203_AgeO_Details.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_203_AgeO_Details.Location = New System.Drawing.Point(0, 32)
		Me.pnl_203_AgeO_Details.Name = "pnl_203_AgeO_Details"
		Me.pnl_203_AgeO_Details.Size = New System.Drawing.Size(383, 442)
		Me.pnl_203_AgeO_Details.TabIndex = 1
		'
		'lbl_Weight_203_AgeO
		'
		Me.lbl_Weight_203_AgeO.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Weight_203_AgeO.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_203_AgeO.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Weight_203_AgeO.MKBoundControl1 = Nothing
		Me.lbl_Weight_203_AgeO.MKBoundControl2 = Nothing
		Me.lbl_Weight_203_AgeO.MKBoundControl3 = Nothing
		Me.lbl_Weight_203_AgeO.MKBoundControl4 = Nothing
		Me.lbl_Weight_203_AgeO.MKBoundControl5 = Nothing
		Me.lbl_Weight_203_AgeO.Name = "lbl_Weight_203_AgeO"
		Me.lbl_Weight_203_AgeO.Size = New System.Drawing.Size(113, 20)
		Me.lbl_Weight_203_AgeO.TabIndex = 7
		Me.lbl_Weight_203_AgeO.Text = "Weight:"
		'
		'lbl_203_AgeO_A
		'
		Me.lbl_203_AgeO_A.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_203_AgeO_A.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_203_AgeO_A.Location = New System.Drawing.Point(3, 26)
		Me.lbl_203_AgeO_A.MKBoundControl1 = Nothing
		Me.lbl_203_AgeO_A.MKBoundControl2 = Nothing
		Me.lbl_203_AgeO_A.MKBoundControl3 = Nothing
		Me.lbl_203_AgeO_A.MKBoundControl4 = Nothing
		Me.lbl_203_AgeO_A.MKBoundControl5 = Nothing
		Me.lbl_203_AgeO_A.Name = "lbl_203_AgeO_A"
		Me.lbl_203_AgeO_A.Size = New System.Drawing.Size(113, 20)
		Me.lbl_203_AgeO_A.TabIndex = 7
		Me.lbl_203_AgeO_A.Text = "AgeO of Game A:"
		'
		'lbl_203_AgeO_B
		'
		Me.lbl_203_AgeO_B.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_203_AgeO_B.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_203_AgeO_B.Location = New System.Drawing.Point(3, 49)
		Me.lbl_203_AgeO_B.MKBoundControl1 = Nothing
		Me.lbl_203_AgeO_B.MKBoundControl2 = Nothing
		Me.lbl_203_AgeO_B.MKBoundControl3 = Nothing
		Me.lbl_203_AgeO_B.MKBoundControl4 = Nothing
		Me.lbl_203_AgeO_B.MKBoundControl5 = Nothing
		Me.lbl_203_AgeO_B.Name = "lbl_203_AgeO_B"
		Me.lbl_203_AgeO_B.Size = New System.Drawing.Size(113, 20)
		Me.lbl_203_AgeO_B.TabIndex = 7
		Me.lbl_203_AgeO_B.Text = "AgeO of Game B:"
		'
		'lbl_Weight_203_AgeO_Text
		'
		Me.lbl_Weight_203_AgeO_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Weight_203_AgeO_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Weight_203_AgeO_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_203_AgeO_Text.Location = New System.Drawing.Point(119, 3)
		Me.lbl_Weight_203_AgeO_Text.MKBoundControl1 = Nothing
		Me.lbl_Weight_203_AgeO_Text.MKBoundControl2 = Nothing
		Me.lbl_Weight_203_AgeO_Text.MKBoundControl3 = Nothing
		Me.lbl_Weight_203_AgeO_Text.MKBoundControl4 = Nothing
		Me.lbl_Weight_203_AgeO_Text.MKBoundControl5 = Nothing
		Me.lbl_Weight_203_AgeO_Text.Name = "lbl_Weight_203_AgeO_Text"
		Me.lbl_Weight_203_AgeO_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_Weight_203_AgeO_Text.TabIndex = 7
		'
		'lbl_203_AgeO_A_Text
		'
		Me.lbl_203_AgeO_A_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_203_AgeO_A_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_203_AgeO_A_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_203_AgeO_A_Text.Location = New System.Drawing.Point(119, 26)
		Me.lbl_203_AgeO_A_Text.MKBoundControl1 = Nothing
		Me.lbl_203_AgeO_A_Text.MKBoundControl2 = Nothing
		Me.lbl_203_AgeO_A_Text.MKBoundControl3 = Nothing
		Me.lbl_203_AgeO_A_Text.MKBoundControl4 = Nothing
		Me.lbl_203_AgeO_A_Text.MKBoundControl5 = Nothing
		Me.lbl_203_AgeO_A_Text.Name = "lbl_203_AgeO_A_Text"
		Me.lbl_203_AgeO_A_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_203_AgeO_A_Text.TabIndex = 7
		'
		'lbl_203_AgeO_B_Text
		'
		Me.lbl_203_AgeO_B_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_203_AgeO_B_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_203_AgeO_B_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_203_AgeO_B_Text.Location = New System.Drawing.Point(119, 49)
		Me.lbl_203_AgeO_B_Text.MKBoundControl1 = Nothing
		Me.lbl_203_AgeO_B_Text.MKBoundControl2 = Nothing
		Me.lbl_203_AgeO_B_Text.MKBoundControl3 = Nothing
		Me.lbl_203_AgeO_B_Text.MKBoundControl4 = Nothing
		Me.lbl_203_AgeO_B_Text.MKBoundControl5 = Nothing
		Me.lbl_203_AgeO_B_Text.Name = "lbl_203_AgeO_B_Text"
		Me.lbl_203_AgeO_B_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_203_AgeO_B_Text.TabIndex = 7
		'
		'lbl_203_AgeO_Explanation
		'
		Me.lbl_203_AgeO_Explanation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_203_AgeO_Explanation.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_203_AgeO_Explanation.Location = New System.Drawing.Point(0, 0)
		Me.lbl_203_AgeO_Explanation.MKBoundControl1 = Nothing
		Me.lbl_203_AgeO_Explanation.MKBoundControl2 = Nothing
		Me.lbl_203_AgeO_Explanation.MKBoundControl3 = Nothing
		Me.lbl_203_AgeO_Explanation.MKBoundControl4 = Nothing
		Me.lbl_203_AgeO_Explanation.MKBoundControl5 = Nothing
		Me.lbl_203_AgeO_Explanation.Name = "lbl_203_AgeO_Explanation"
		Me.lbl_203_AgeO_Explanation.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_203_AgeO_Explanation.Size = New System.Drawing.Size(383, 32)
		Me.lbl_203_AgeO_Explanation.TabIndex = 0
		Me.lbl_203_AgeO_Explanation.Text = "The similarity score of 100 is reduced by 10 times the difference between the opt" &
		"imistic minumum age for both games."
		'
		'tpg_204_AgeP
		'
		Me.tpg_204_AgeP.Controls.Add(Me.pnl_204_AgeP)
		Me.tpg_204_AgeP.Name = "tpg_204_AgeP"
		Me.tpg_204_AgeP.Size = New System.Drawing.Size(383, 474)
		Me.tpg_204_AgeP.Text = "204_AgeP"
		'
		'pnl_204_AgeP
		'
		Me.pnl_204_AgeP.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_204_AgeP.Controls.Add(Me.pnl_204_AgeP_Details)
		Me.pnl_204_AgeP.Controls.Add(Me.lbl_204_AgeP)
		Me.pnl_204_AgeP.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_204_AgeP.Location = New System.Drawing.Point(0, 0)
		Me.pnl_204_AgeP.Name = "pnl_204_AgeP"
		Me.pnl_204_AgeP.Size = New System.Drawing.Size(383, 474)
		Me.pnl_204_AgeP.TabIndex = 12
		'
		'pnl_204_AgeP_Details
		'
		Me.pnl_204_AgeP_Details.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_204_AgeP_Details.Controls.Add(Me.lbl_Weight_204_AgeP)
		Me.pnl_204_AgeP_Details.Controls.Add(Me.lbl_204_AgeP_A)
		Me.pnl_204_AgeP_Details.Controls.Add(Me.lbl_204_AgeP_B)
		Me.pnl_204_AgeP_Details.Controls.Add(Me.lbl_Weight_204_AgeP_Text)
		Me.pnl_204_AgeP_Details.Controls.Add(Me.lbl_204_AgeP_A_Text)
		Me.pnl_204_AgeP_Details.Controls.Add(Me.lbl_204_AgeP_B_Text)
		Me.pnl_204_AgeP_Details.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_204_AgeP_Details.Location = New System.Drawing.Point(0, 32)
		Me.pnl_204_AgeP_Details.Name = "pnl_204_AgeP_Details"
		Me.pnl_204_AgeP_Details.Size = New System.Drawing.Size(383, 442)
		Me.pnl_204_AgeP_Details.TabIndex = 1
		'
		'lbl_Weight_204_AgeP
		'
		Me.lbl_Weight_204_AgeP.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Weight_204_AgeP.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_204_AgeP.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Weight_204_AgeP.MKBoundControl1 = Nothing
		Me.lbl_Weight_204_AgeP.MKBoundControl2 = Nothing
		Me.lbl_Weight_204_AgeP.MKBoundControl3 = Nothing
		Me.lbl_Weight_204_AgeP.MKBoundControl4 = Nothing
		Me.lbl_Weight_204_AgeP.MKBoundControl5 = Nothing
		Me.lbl_Weight_204_AgeP.Name = "lbl_Weight_204_AgeP"
		Me.lbl_Weight_204_AgeP.Size = New System.Drawing.Size(113, 20)
		Me.lbl_Weight_204_AgeP.TabIndex = 7
		Me.lbl_Weight_204_AgeP.Text = "Weight:"
		'
		'lbl_204_AgeP_A
		'
		Me.lbl_204_AgeP_A.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_204_AgeP_A.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_204_AgeP_A.Location = New System.Drawing.Point(3, 26)
		Me.lbl_204_AgeP_A.MKBoundControl1 = Nothing
		Me.lbl_204_AgeP_A.MKBoundControl2 = Nothing
		Me.lbl_204_AgeP_A.MKBoundControl3 = Nothing
		Me.lbl_204_AgeP_A.MKBoundControl4 = Nothing
		Me.lbl_204_AgeP_A.MKBoundControl5 = Nothing
		Me.lbl_204_AgeP_A.Name = "lbl_204_AgeP_A"
		Me.lbl_204_AgeP_A.Size = New System.Drawing.Size(113, 20)
		Me.lbl_204_AgeP_A.TabIndex = 7
		Me.lbl_204_AgeP_A.Text = "AgeP of Game A:"
		'
		'lbl_204_AgeP_B
		'
		Me.lbl_204_AgeP_B.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_204_AgeP_B.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_204_AgeP_B.Location = New System.Drawing.Point(3, 49)
		Me.lbl_204_AgeP_B.MKBoundControl1 = Nothing
		Me.lbl_204_AgeP_B.MKBoundControl2 = Nothing
		Me.lbl_204_AgeP_B.MKBoundControl3 = Nothing
		Me.lbl_204_AgeP_B.MKBoundControl4 = Nothing
		Me.lbl_204_AgeP_B.MKBoundControl5 = Nothing
		Me.lbl_204_AgeP_B.Name = "lbl_204_AgeP_B"
		Me.lbl_204_AgeP_B.Size = New System.Drawing.Size(113, 20)
		Me.lbl_204_AgeP_B.TabIndex = 7
		Me.lbl_204_AgeP_B.Text = "AgeP of Game B:"
		'
		'lbl_Weight_204_AgeP_Text
		'
		Me.lbl_Weight_204_AgeP_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Weight_204_AgeP_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Weight_204_AgeP_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_204_AgeP_Text.Location = New System.Drawing.Point(119, 3)
		Me.lbl_Weight_204_AgeP_Text.MKBoundControl1 = Nothing
		Me.lbl_Weight_204_AgeP_Text.MKBoundControl2 = Nothing
		Me.lbl_Weight_204_AgeP_Text.MKBoundControl3 = Nothing
		Me.lbl_Weight_204_AgeP_Text.MKBoundControl4 = Nothing
		Me.lbl_Weight_204_AgeP_Text.MKBoundControl5 = Nothing
		Me.lbl_Weight_204_AgeP_Text.Name = "lbl_Weight_204_AgeP_Text"
		Me.lbl_Weight_204_AgeP_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_Weight_204_AgeP_Text.TabIndex = 7
		'
		'lbl_204_AgeP_A_Text
		'
		Me.lbl_204_AgeP_A_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_204_AgeP_A_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_204_AgeP_A_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_204_AgeP_A_Text.Location = New System.Drawing.Point(119, 26)
		Me.lbl_204_AgeP_A_Text.MKBoundControl1 = Nothing
		Me.lbl_204_AgeP_A_Text.MKBoundControl2 = Nothing
		Me.lbl_204_AgeP_A_Text.MKBoundControl3 = Nothing
		Me.lbl_204_AgeP_A_Text.MKBoundControl4 = Nothing
		Me.lbl_204_AgeP_A_Text.MKBoundControl5 = Nothing
		Me.lbl_204_AgeP_A_Text.Name = "lbl_204_AgeP_A_Text"
		Me.lbl_204_AgeP_A_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_204_AgeP_A_Text.TabIndex = 7
		'
		'lbl_204_AgeP_B_Text
		'
		Me.lbl_204_AgeP_B_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_204_AgeP_B_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_204_AgeP_B_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_204_AgeP_B_Text.Location = New System.Drawing.Point(119, 49)
		Me.lbl_204_AgeP_B_Text.MKBoundControl1 = Nothing
		Me.lbl_204_AgeP_B_Text.MKBoundControl2 = Nothing
		Me.lbl_204_AgeP_B_Text.MKBoundControl3 = Nothing
		Me.lbl_204_AgeP_B_Text.MKBoundControl4 = Nothing
		Me.lbl_204_AgeP_B_Text.MKBoundControl5 = Nothing
		Me.lbl_204_AgeP_B_Text.Name = "lbl_204_AgeP_B_Text"
		Me.lbl_204_AgeP_B_Text.Size = New System.Drawing.Size(260, 20)
		Me.lbl_204_AgeP_B_Text.TabIndex = 7
		'
		'lbl_204_AgeP
		'
		Me.lbl_204_AgeP.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_204_AgeP.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_204_AgeP.Location = New System.Drawing.Point(0, 0)
		Me.lbl_204_AgeP.MKBoundControl1 = Nothing
		Me.lbl_204_AgeP.MKBoundControl2 = Nothing
		Me.lbl_204_AgeP.MKBoundControl3 = Nothing
		Me.lbl_204_AgeP.MKBoundControl4 = Nothing
		Me.lbl_204_AgeP.MKBoundControl5 = Nothing
		Me.lbl_204_AgeP.Name = "lbl_204_AgeP"
		Me.lbl_204_AgeP.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_204_AgeP.Size = New System.Drawing.Size(383, 32)
		Me.lbl_204_AgeP.TabIndex = 0
		Me.lbl_204_AgeP.Text = "The similarity score of 100 is reduced by 10 times the difference between the pes" &
		"simistic minumum age for both games."
		'
		'tpg_205_Rating_Descriptors
		'
		Me.tpg_205_Rating_Descriptors.Controls.Add(Me.pnl_205_Rating_Descriptors)
		Me.tpg_205_Rating_Descriptors.Name = "tpg_205_Rating_Descriptors"
		Me.tpg_205_Rating_Descriptors.Size = New System.Drawing.Size(383, 474)
		Me.tpg_205_Rating_Descriptors.Text = "205_Rating_Descriptors"
		'
		'pnl_205_Rating_Descriptors
		'
		Me.pnl_205_Rating_Descriptors.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_205_Rating_Descriptors.Controls.Add(Me.pnl_205_Rating_Descriptors_Details)
		Me.pnl_205_Rating_Descriptors.Controls.Add(Me.lbl_205_Rating_Descriptors_Explanation)
		Me.pnl_205_Rating_Descriptors.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_205_Rating_Descriptors.Location = New System.Drawing.Point(0, 0)
		Me.pnl_205_Rating_Descriptors.Name = "pnl_205_Rating_Descriptors"
		Me.pnl_205_Rating_Descriptors.Size = New System.Drawing.Size(383, 474)
		Me.pnl_205_Rating_Descriptors.TabIndex = 16
		'
		'pnl_205_Rating_Descriptors_Details
		'
		Me.pnl_205_Rating_Descriptors_Details.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_205_Rating_Descriptors_Details.Controls.Add(Me.tlp_205_Rating_Descriptors)
		Me.pnl_205_Rating_Descriptors_Details.Controls.Add(Me.lbl_Weight_205_Rating_Descriptors)
		Me.pnl_205_Rating_Descriptors_Details.Controls.Add(Me.lbl_Weight_205_Rating_Descriptors_Text)
		Me.pnl_205_Rating_Descriptors_Details.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_205_Rating_Descriptors_Details.Location = New System.Drawing.Point(0, 97)
		Me.pnl_205_Rating_Descriptors_Details.Name = "pnl_205_Rating_Descriptors_Details"
		Me.pnl_205_Rating_Descriptors_Details.Size = New System.Drawing.Size(383, 377)
		Me.pnl_205_Rating_Descriptors_Details.TabIndex = 1
		'
		'tlp_205_Rating_Descriptors
		'
		Me.tlp_205_Rating_Descriptors.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tlp_205_Rating_Descriptors.ColumnCount = 1
		Me.tlp_205_Rating_Descriptors.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tlp_205_Rating_Descriptors.Controls.Add(Me.gb_205_Rating_Descriptors_AB, 0, 2)
		Me.tlp_205_Rating_Descriptors.Controls.Add(Me.gb_205_Rating_Descriptors_B, 0, 1)
		Me.tlp_205_Rating_Descriptors.Controls.Add(Me.gb_205_Rating_Descriptors_A, 0, 0)
		Me.tlp_205_Rating_Descriptors.Location = New System.Drawing.Point(0, 27)
		Me.tlp_205_Rating_Descriptors.Name = "tlp_205_Rating_Descriptors"
		Me.tlp_205_Rating_Descriptors.RowCount = 3
		Me.tlp_205_Rating_Descriptors.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
		Me.tlp_205_Rating_Descriptors.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
		Me.tlp_205_Rating_Descriptors.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
		Me.tlp_205_Rating_Descriptors.Size = New System.Drawing.Size(383, 351)
		Me.tlp_205_Rating_Descriptors.TabIndex = 8
		'
		'gb_205_Rating_Descriptors_AB
		'
		Me.gb_205_Rating_Descriptors_AB.Controls.Add(Me.grd_205_Rating_Descriptors_AB)
		Me.gb_205_Rating_Descriptors_AB.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gb_205_Rating_Descriptors_AB.Location = New System.Drawing.Point(3, 237)
		Me.gb_205_Rating_Descriptors_AB.Name = "gb_205_Rating_Descriptors_AB"
		Me.gb_205_Rating_Descriptors_AB.Size = New System.Drawing.Size(377, 111)
		Me.gb_205_Rating_Descriptors_AB.TabIndex = 2
		Me.gb_205_Rating_Descriptors_AB.Text = "Rating Descriptors shared by Games A and B"
		'
		'grd_205_Rating_Descriptors_AB
		'
		Me.grd_205_Rating_Descriptors_AB.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_205_Rating_Descriptors_AB.Location = New System.Drawing.Point(2, 20)
		Me.grd_205_Rating_Descriptors_AB.MainView = Me.gv_205_Rating_Descriptors_AB
		Me.grd_205_Rating_Descriptors_AB.Name = "grd_205_Rating_Descriptors_AB"
		Me.grd_205_Rating_Descriptors_AB.Size = New System.Drawing.Size(373, 89)
		Me.grd_205_Rating_Descriptors_AB.TabIndex = 3
		Me.grd_205_Rating_Descriptors_AB.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_205_Rating_Descriptors_AB})
		'
		'gv_205_Rating_Descriptors_AB
		'
		Me.gv_205_Rating_Descriptors_AB.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_205_Rating_Descriptors_AB})
		Me.gv_205_Rating_Descriptors_AB.GridControl = Me.grd_205_Rating_Descriptors_AB
		Me.gv_205_Rating_Descriptors_AB.Name = "gv_205_Rating_Descriptors_AB"
		Me.gv_205_Rating_Descriptors_AB.OptionsSelection.InvertSelection = True
		Me.gv_205_Rating_Descriptors_AB.OptionsView.ShowColumnHeaders = False
		Me.gv_205_Rating_Descriptors_AB.OptionsView.ShowGroupPanel = False
		Me.gv_205_Rating_Descriptors_AB.OptionsView.ShowIndicator = False
		'
		'col_205_Rating_Descriptors_AB
		'
		Me.col_205_Rating_Descriptors_AB.FieldName = "Name"
		Me.col_205_Rating_Descriptors_AB.Name = "col_205_Rating_Descriptors_AB"
		Me.col_205_Rating_Descriptors_AB.OptionsColumn.AllowEdit = False
		Me.col_205_Rating_Descriptors_AB.OptionsColumn.ReadOnly = True
		Me.col_205_Rating_Descriptors_AB.Visible = True
		Me.col_205_Rating_Descriptors_AB.VisibleIndex = 0
		'
		'gb_205_Rating_Descriptors_B
		'
		Me.gb_205_Rating_Descriptors_B.Controls.Add(Me.grd_205_Rating_Descriptors_B)
		Me.gb_205_Rating_Descriptors_B.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gb_205_Rating_Descriptors_B.Location = New System.Drawing.Point(3, 120)
		Me.gb_205_Rating_Descriptors_B.Name = "gb_205_Rating_Descriptors_B"
		Me.gb_205_Rating_Descriptors_B.Size = New System.Drawing.Size(377, 111)
		Me.gb_205_Rating_Descriptors_B.TabIndex = 1
		Me.gb_205_Rating_Descriptors_B.Text = "Rating Descriptors of Game B"
		'
		'grd_205_Rating_Descriptors_B
		'
		Me.grd_205_Rating_Descriptors_B.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_205_Rating_Descriptors_B.Location = New System.Drawing.Point(2, 20)
		Me.grd_205_Rating_Descriptors_B.MainView = Me.gv_205_Rating_Descriptors_B
		Me.grd_205_Rating_Descriptors_B.Name = "grd_205_Rating_Descriptors_B"
		Me.grd_205_Rating_Descriptors_B.Size = New System.Drawing.Size(373, 89)
		Me.grd_205_Rating_Descriptors_B.TabIndex = 2
		Me.grd_205_Rating_Descriptors_B.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_205_Rating_Descriptors_B})
		'
		'gv_205_Rating_Descriptors_B
		'
		Me.gv_205_Rating_Descriptors_B.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_205_Rating_Descriptors_B})
		Me.gv_205_Rating_Descriptors_B.GridControl = Me.grd_205_Rating_Descriptors_B
		Me.gv_205_Rating_Descriptors_B.Name = "gv_205_Rating_Descriptors_B"
		Me.gv_205_Rating_Descriptors_B.OptionsSelection.InvertSelection = True
		Me.gv_205_Rating_Descriptors_B.OptionsView.ShowColumnHeaders = False
		Me.gv_205_Rating_Descriptors_B.OptionsView.ShowGroupPanel = False
		Me.gv_205_Rating_Descriptors_B.OptionsView.ShowIndicator = False
		'
		'col_205_Rating_Descriptors_B
		'
		Me.col_205_Rating_Descriptors_B.FieldName = "Name"
		Me.col_205_Rating_Descriptors_B.Name = "col_205_Rating_Descriptors_B"
		Me.col_205_Rating_Descriptors_B.OptionsColumn.AllowEdit = False
		Me.col_205_Rating_Descriptors_B.OptionsColumn.ReadOnly = True
		Me.col_205_Rating_Descriptors_B.Visible = True
		Me.col_205_Rating_Descriptors_B.VisibleIndex = 0
		'
		'gb_205_Rating_Descriptors_A
		'
		Me.gb_205_Rating_Descriptors_A.Controls.Add(Me.grd_205_Rating_Descriptors_A)
		Me.gb_205_Rating_Descriptors_A.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gb_205_Rating_Descriptors_A.Location = New System.Drawing.Point(3, 3)
		Me.gb_205_Rating_Descriptors_A.Name = "gb_205_Rating_Descriptors_A"
		Me.gb_205_Rating_Descriptors_A.Size = New System.Drawing.Size(377, 111)
		Me.gb_205_Rating_Descriptors_A.TabIndex = 0
		Me.gb_205_Rating_Descriptors_A.Text = "Rating Descriptors of Game A"
		'
		'grd_205_Rating_Descriptors_A
		'
		Me.grd_205_Rating_Descriptors_A.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_205_Rating_Descriptors_A.Location = New System.Drawing.Point(2, 20)
		Me.grd_205_Rating_Descriptors_A.MainView = Me.gv_205_Rating_Descriptors_A
		Me.grd_205_Rating_Descriptors_A.Name = "grd_205_Rating_Descriptors_A"
		Me.grd_205_Rating_Descriptors_A.Size = New System.Drawing.Size(373, 89)
		Me.grd_205_Rating_Descriptors_A.TabIndex = 1
		Me.grd_205_Rating_Descriptors_A.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_205_Rating_Descriptors_A})
		'
		'gv_205_Rating_Descriptors_A
		'
		Me.gv_205_Rating_Descriptors_A.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_205_Rating_Descriptors_A})
		Me.gv_205_Rating_Descriptors_A.GridControl = Me.grd_205_Rating_Descriptors_A
		Me.gv_205_Rating_Descriptors_A.Name = "gv_205_Rating_Descriptors_A"
		Me.gv_205_Rating_Descriptors_A.OptionsSelection.InvertSelection = True
		Me.gv_205_Rating_Descriptors_A.OptionsView.ShowColumnHeaders = False
		Me.gv_205_Rating_Descriptors_A.OptionsView.ShowGroupPanel = False
		Me.gv_205_Rating_Descriptors_A.OptionsView.ShowIndicator = False
		'
		'col_205_Rating_Descriptors_A
		'
		Me.col_205_Rating_Descriptors_A.FieldName = "Name"
		Me.col_205_Rating_Descriptors_A.Name = "col_205_Rating_Descriptors_A"
		Me.col_205_Rating_Descriptors_A.OptionsColumn.AllowEdit = False
		Me.col_205_Rating_Descriptors_A.OptionsColumn.ReadOnly = True
		Me.col_205_Rating_Descriptors_A.Visible = True
		Me.col_205_Rating_Descriptors_A.VisibleIndex = 0
		'
		'lbl_Weight_205_Rating_Descriptors
		'
		Me.lbl_Weight_205_Rating_Descriptors.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Weight_205_Rating_Descriptors.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_205_Rating_Descriptors.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Weight_205_Rating_Descriptors.MKBoundControl1 = Nothing
		Me.lbl_Weight_205_Rating_Descriptors.MKBoundControl2 = Nothing
		Me.lbl_Weight_205_Rating_Descriptors.MKBoundControl3 = Nothing
		Me.lbl_Weight_205_Rating_Descriptors.MKBoundControl4 = Nothing
		Me.lbl_Weight_205_Rating_Descriptors.MKBoundControl5 = Nothing
		Me.lbl_Weight_205_Rating_Descriptors.Name = "lbl_Weight_205_Rating_Descriptors"
		Me.lbl_Weight_205_Rating_Descriptors.Size = New System.Drawing.Size(45, 20)
		Me.lbl_Weight_205_Rating_Descriptors.TabIndex = 7
		Me.lbl_Weight_205_Rating_Descriptors.Text = "Weight:"
		'
		'lbl_Weight_205_Rating_Descriptors_Text
		'
		Me.lbl_Weight_205_Rating_Descriptors_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Weight_205_Rating_Descriptors_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Weight_205_Rating_Descriptors_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_205_Rating_Descriptors_Text.Location = New System.Drawing.Point(51, 3)
		Me.lbl_Weight_205_Rating_Descriptors_Text.MKBoundControl1 = Nothing
		Me.lbl_Weight_205_Rating_Descriptors_Text.MKBoundControl2 = Nothing
		Me.lbl_Weight_205_Rating_Descriptors_Text.MKBoundControl3 = Nothing
		Me.lbl_Weight_205_Rating_Descriptors_Text.MKBoundControl4 = Nothing
		Me.lbl_Weight_205_Rating_Descriptors_Text.MKBoundControl5 = Nothing
		Me.lbl_Weight_205_Rating_Descriptors_Text.Name = "lbl_Weight_205_Rating_Descriptors_Text"
		Me.lbl_Weight_205_Rating_Descriptors_Text.Size = New System.Drawing.Size(328, 20)
		Me.lbl_Weight_205_Rating_Descriptors_Text.TabIndex = 7
		'
		'lbl_205_Rating_Descriptors_Explanation
		'
		Me.lbl_205_Rating_Descriptors_Explanation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_205_Rating_Descriptors_Explanation.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_205_Rating_Descriptors_Explanation.Location = New System.Drawing.Point(0, 0)
		Me.lbl_205_Rating_Descriptors_Explanation.MKBoundControl1 = Nothing
		Me.lbl_205_Rating_Descriptors_Explanation.MKBoundControl2 = Nothing
		Me.lbl_205_Rating_Descriptors_Explanation.MKBoundControl3 = Nothing
		Me.lbl_205_Rating_Descriptors_Explanation.MKBoundControl4 = Nothing
		Me.lbl_205_Rating_Descriptors_Explanation.MKBoundControl5 = Nothing
		Me.lbl_205_Rating_Descriptors_Explanation.Name = "lbl_205_Rating_Descriptors_Explanation"
		Me.lbl_205_Rating_Descriptors_Explanation.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_205_Rating_Descriptors_Explanation.Size = New System.Drawing.Size(383, 97)
		Me.lbl_205_Rating_Descriptors_Explanation.TabIndex = 0
		Me.lbl_205_Rating_Descriptors_Explanation.Text = resources.GetString("lbl_205_Rating_Descriptors_Explanation.Text")
		'
		'tpg_207_Multiplayer_Attributes
		'
		Me.tpg_207_Multiplayer_Attributes.Controls.Add(Me.pnl_207_Multiplayer_Attributes)
		Me.tpg_207_Multiplayer_Attributes.Name = "tpg_207_Multiplayer_Attributes"
		Me.tpg_207_Multiplayer_Attributes.Size = New System.Drawing.Size(383, 474)
		Me.tpg_207_Multiplayer_Attributes.Text = "207_Multiplayer_Attributes"
		'
		'pnl_207_Multiplayer_Attributes
		'
		Me.pnl_207_Multiplayer_Attributes.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_207_Multiplayer_Attributes.Controls.Add(Me.pnl_207_Multiplayer_Attributes_Details)
		Me.pnl_207_Multiplayer_Attributes.Controls.Add(Me.lbl_207_Multiplayer_Attributes_Explanation)
		Me.pnl_207_Multiplayer_Attributes.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_207_Multiplayer_Attributes.Location = New System.Drawing.Point(0, 0)
		Me.pnl_207_Multiplayer_Attributes.Name = "pnl_207_Multiplayer_Attributes"
		Me.pnl_207_Multiplayer_Attributes.Size = New System.Drawing.Size(383, 474)
		Me.pnl_207_Multiplayer_Attributes.TabIndex = 17
		'
		'pnl_207_Multiplayer_Attributes_Details
		'
		Me.pnl_207_Multiplayer_Attributes_Details.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_207_Multiplayer_Attributes_Details.Controls.Add(Me.tlp_207_Multiplayer_Attributes)
		Me.pnl_207_Multiplayer_Attributes_Details.Controls.Add(Me.lbl_Weight_207_Multiplayer_Attributes)
		Me.pnl_207_Multiplayer_Attributes_Details.Controls.Add(Me.lbl_Weight_207_Multiplayer_Attributes_Text)
		Me.pnl_207_Multiplayer_Attributes_Details.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_207_Multiplayer_Attributes_Details.Location = New System.Drawing.Point(0, 97)
		Me.pnl_207_Multiplayer_Attributes_Details.Name = "pnl_207_Multiplayer_Attributes_Details"
		Me.pnl_207_Multiplayer_Attributes_Details.Size = New System.Drawing.Size(383, 377)
		Me.pnl_207_Multiplayer_Attributes_Details.TabIndex = 1
		'
		'tlp_207_Multiplayer_Attributes
		'
		Me.tlp_207_Multiplayer_Attributes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tlp_207_Multiplayer_Attributes.ColumnCount = 1
		Me.tlp_207_Multiplayer_Attributes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tlp_207_Multiplayer_Attributes.Controls.Add(Me.gb_207_Multiplayer_Attributes_AB, 0, 2)
		Me.tlp_207_Multiplayer_Attributes.Controls.Add(Me.gb_207_Multiplayer_Attributes_B, 0, 1)
		Me.tlp_207_Multiplayer_Attributes.Controls.Add(Me.gb_207_Multiplayer_Attributes_A, 0, 0)
		Me.tlp_207_Multiplayer_Attributes.Location = New System.Drawing.Point(0, 27)
		Me.tlp_207_Multiplayer_Attributes.Name = "tlp_207_Multiplayer_Attributes"
		Me.tlp_207_Multiplayer_Attributes.RowCount = 3
		Me.tlp_207_Multiplayer_Attributes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
		Me.tlp_207_Multiplayer_Attributes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
		Me.tlp_207_Multiplayer_Attributes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
		Me.tlp_207_Multiplayer_Attributes.Size = New System.Drawing.Size(383, 351)
		Me.tlp_207_Multiplayer_Attributes.TabIndex = 8
		'
		'gb_207_Multiplayer_Attributes_AB
		'
		Me.gb_207_Multiplayer_Attributes_AB.Controls.Add(Me.grd_207_Multiplayer_Attributes_AB)
		Me.gb_207_Multiplayer_Attributes_AB.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gb_207_Multiplayer_Attributes_AB.Location = New System.Drawing.Point(3, 237)
		Me.gb_207_Multiplayer_Attributes_AB.Name = "gb_207_Multiplayer_Attributes_AB"
		Me.gb_207_Multiplayer_Attributes_AB.Size = New System.Drawing.Size(377, 111)
		Me.gb_207_Multiplayer_Attributes_AB.TabIndex = 2
		Me.gb_207_Multiplayer_Attributes_AB.Text = "Multiplayer Attributes shared by Games A and B"
		'
		'grd_207_Multiplayer_Attributes_AB
		'
		Me.grd_207_Multiplayer_Attributes_AB.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_207_Multiplayer_Attributes_AB.Location = New System.Drawing.Point(2, 20)
		Me.grd_207_Multiplayer_Attributes_AB.MainView = Me.gv_207_Multiplayer_Attributes_AB
		Me.grd_207_Multiplayer_Attributes_AB.Name = "grd_207_Multiplayer_Attributes_AB"
		Me.grd_207_Multiplayer_Attributes_AB.Size = New System.Drawing.Size(373, 89)
		Me.grd_207_Multiplayer_Attributes_AB.TabIndex = 3
		Me.grd_207_Multiplayer_Attributes_AB.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_207_Multiplayer_Attributes_AB})
		'
		'gv_207_Multiplayer_Attributes_AB
		'
		Me.gv_207_Multiplayer_Attributes_AB.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_207_Multiplayer_Attributes_AB})
		Me.gv_207_Multiplayer_Attributes_AB.GridControl = Me.grd_207_Multiplayer_Attributes_AB
		Me.gv_207_Multiplayer_Attributes_AB.Name = "gv_207_Multiplayer_Attributes_AB"
		Me.gv_207_Multiplayer_Attributes_AB.OptionsSelection.InvertSelection = True
		Me.gv_207_Multiplayer_Attributes_AB.OptionsView.ShowColumnHeaders = False
		Me.gv_207_Multiplayer_Attributes_AB.OptionsView.ShowGroupPanel = False
		Me.gv_207_Multiplayer_Attributes_AB.OptionsView.ShowIndicator = False
		'
		'col_207_Multiplayer_Attributes_AB
		'
		Me.col_207_Multiplayer_Attributes_AB.FieldName = "Name"
		Me.col_207_Multiplayer_Attributes_AB.Name = "col_207_Multiplayer_Attributes_AB"
		Me.col_207_Multiplayer_Attributes_AB.OptionsColumn.AllowEdit = False
		Me.col_207_Multiplayer_Attributes_AB.OptionsColumn.ReadOnly = True
		Me.col_207_Multiplayer_Attributes_AB.Visible = True
		Me.col_207_Multiplayer_Attributes_AB.VisibleIndex = 0
		'
		'gb_207_Multiplayer_Attributes_B
		'
		Me.gb_207_Multiplayer_Attributes_B.Controls.Add(Me.grd_207_Multiplayer_Attributes_B)
		Me.gb_207_Multiplayer_Attributes_B.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gb_207_Multiplayer_Attributes_B.Location = New System.Drawing.Point(3, 120)
		Me.gb_207_Multiplayer_Attributes_B.Name = "gb_207_Multiplayer_Attributes_B"
		Me.gb_207_Multiplayer_Attributes_B.Size = New System.Drawing.Size(377, 111)
		Me.gb_207_Multiplayer_Attributes_B.TabIndex = 1
		Me.gb_207_Multiplayer_Attributes_B.Text = "Multiplayer Attributes of Game B"
		'
		'grd_207_Multiplayer_Attributes_B
		'
		Me.grd_207_Multiplayer_Attributes_B.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_207_Multiplayer_Attributes_B.Location = New System.Drawing.Point(2, 20)
		Me.grd_207_Multiplayer_Attributes_B.MainView = Me.gv_207_Multiplayer_Attributes_B
		Me.grd_207_Multiplayer_Attributes_B.Name = "grd_207_Multiplayer_Attributes_B"
		Me.grd_207_Multiplayer_Attributes_B.Size = New System.Drawing.Size(373, 89)
		Me.grd_207_Multiplayer_Attributes_B.TabIndex = 2
		Me.grd_207_Multiplayer_Attributes_B.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_207_Multiplayer_Attributes_B})
		'
		'gv_207_Multiplayer_Attributes_B
		'
		Me.gv_207_Multiplayer_Attributes_B.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_207_Multiplayer_Attributes_B})
		Me.gv_207_Multiplayer_Attributes_B.GridControl = Me.grd_207_Multiplayer_Attributes_B
		Me.gv_207_Multiplayer_Attributes_B.Name = "gv_207_Multiplayer_Attributes_B"
		Me.gv_207_Multiplayer_Attributes_B.OptionsSelection.InvertSelection = True
		Me.gv_207_Multiplayer_Attributes_B.OptionsView.ShowColumnHeaders = False
		Me.gv_207_Multiplayer_Attributes_B.OptionsView.ShowGroupPanel = False
		Me.gv_207_Multiplayer_Attributes_B.OptionsView.ShowIndicator = False
		'
		'col_207_Multiplayer_Attributes_B
		'
		Me.col_207_Multiplayer_Attributes_B.FieldName = "Name"
		Me.col_207_Multiplayer_Attributes_B.Name = "col_207_Multiplayer_Attributes_B"
		Me.col_207_Multiplayer_Attributes_B.OptionsColumn.AllowEdit = False
		Me.col_207_Multiplayer_Attributes_B.OptionsColumn.ReadOnly = True
		Me.col_207_Multiplayer_Attributes_B.Visible = True
		Me.col_207_Multiplayer_Attributes_B.VisibleIndex = 0
		'
		'gb_207_Multiplayer_Attributes_A
		'
		Me.gb_207_Multiplayer_Attributes_A.Controls.Add(Me.grd_207_Multiplayer_Attributes_A)
		Me.gb_207_Multiplayer_Attributes_A.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gb_207_Multiplayer_Attributes_A.Location = New System.Drawing.Point(3, 3)
		Me.gb_207_Multiplayer_Attributes_A.Name = "gb_207_Multiplayer_Attributes_A"
		Me.gb_207_Multiplayer_Attributes_A.Size = New System.Drawing.Size(377, 111)
		Me.gb_207_Multiplayer_Attributes_A.TabIndex = 0
		Me.gb_207_Multiplayer_Attributes_A.Text = "Multiplayer Attributes of Game A"
		'
		'grd_207_Multiplayer_Attributes_A
		'
		Me.grd_207_Multiplayer_Attributes_A.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_207_Multiplayer_Attributes_A.Location = New System.Drawing.Point(2, 20)
		Me.grd_207_Multiplayer_Attributes_A.MainView = Me.gv_207_Multiplayer_Attributes_A
		Me.grd_207_Multiplayer_Attributes_A.Name = "grd_207_Multiplayer_Attributes_A"
		Me.grd_207_Multiplayer_Attributes_A.Size = New System.Drawing.Size(373, 89)
		Me.grd_207_Multiplayer_Attributes_A.TabIndex = 1
		Me.grd_207_Multiplayer_Attributes_A.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_207_Multiplayer_Attributes_A})
		'
		'gv_207_Multiplayer_Attributes_A
		'
		Me.gv_207_Multiplayer_Attributes_A.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_207_Multiplayer_Attributes_A})
		Me.gv_207_Multiplayer_Attributes_A.GridControl = Me.grd_207_Multiplayer_Attributes_A
		Me.gv_207_Multiplayer_Attributes_A.Name = "gv_207_Multiplayer_Attributes_A"
		Me.gv_207_Multiplayer_Attributes_A.OptionsSelection.InvertSelection = True
		Me.gv_207_Multiplayer_Attributes_A.OptionsView.ShowColumnHeaders = False
		Me.gv_207_Multiplayer_Attributes_A.OptionsView.ShowGroupPanel = False
		Me.gv_207_Multiplayer_Attributes_A.OptionsView.ShowIndicator = False
		'
		'col_207_Multiplayer_Attributes_A
		'
		Me.col_207_Multiplayer_Attributes_A.FieldName = "Name"
		Me.col_207_Multiplayer_Attributes_A.Name = "col_207_Multiplayer_Attributes_A"
		Me.col_207_Multiplayer_Attributes_A.OptionsColumn.AllowEdit = False
		Me.col_207_Multiplayer_Attributes_A.OptionsColumn.ReadOnly = True
		Me.col_207_Multiplayer_Attributes_A.Visible = True
		Me.col_207_Multiplayer_Attributes_A.VisibleIndex = 0
		'
		'lbl_Weight_207_Multiplayer_Attributes
		'
		Me.lbl_Weight_207_Multiplayer_Attributes.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Weight_207_Multiplayer_Attributes.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_207_Multiplayer_Attributes.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Weight_207_Multiplayer_Attributes.MKBoundControl1 = Nothing
		Me.lbl_Weight_207_Multiplayer_Attributes.MKBoundControl2 = Nothing
		Me.lbl_Weight_207_Multiplayer_Attributes.MKBoundControl3 = Nothing
		Me.lbl_Weight_207_Multiplayer_Attributes.MKBoundControl4 = Nothing
		Me.lbl_Weight_207_Multiplayer_Attributes.MKBoundControl5 = Nothing
		Me.lbl_Weight_207_Multiplayer_Attributes.Name = "lbl_Weight_207_Multiplayer_Attributes"
		Me.lbl_Weight_207_Multiplayer_Attributes.Size = New System.Drawing.Size(45, 20)
		Me.lbl_Weight_207_Multiplayer_Attributes.TabIndex = 7
		Me.lbl_Weight_207_Multiplayer_Attributes.Text = "Weight:"
		'
		'lbl_Weight_207_Multiplayer_Attributes_Text
		'
		Me.lbl_Weight_207_Multiplayer_Attributes_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Weight_207_Multiplayer_Attributes_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Weight_207_Multiplayer_Attributes_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_207_Multiplayer_Attributes_Text.Location = New System.Drawing.Point(51, 3)
		Me.lbl_Weight_207_Multiplayer_Attributes_Text.MKBoundControl1 = Nothing
		Me.lbl_Weight_207_Multiplayer_Attributes_Text.MKBoundControl2 = Nothing
		Me.lbl_Weight_207_Multiplayer_Attributes_Text.MKBoundControl3 = Nothing
		Me.lbl_Weight_207_Multiplayer_Attributes_Text.MKBoundControl4 = Nothing
		Me.lbl_Weight_207_Multiplayer_Attributes_Text.MKBoundControl5 = Nothing
		Me.lbl_Weight_207_Multiplayer_Attributes_Text.Name = "lbl_Weight_207_Multiplayer_Attributes_Text"
		Me.lbl_Weight_207_Multiplayer_Attributes_Text.Size = New System.Drawing.Size(328, 20)
		Me.lbl_Weight_207_Multiplayer_Attributes_Text.TabIndex = 7
		'
		'lbl_207_Multiplayer_Attributes_Explanation
		'
		Me.lbl_207_Multiplayer_Attributes_Explanation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_207_Multiplayer_Attributes_Explanation.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_207_Multiplayer_Attributes_Explanation.Location = New System.Drawing.Point(0, 0)
		Me.lbl_207_Multiplayer_Attributes_Explanation.MKBoundControl1 = Nothing
		Me.lbl_207_Multiplayer_Attributes_Explanation.MKBoundControl2 = Nothing
		Me.lbl_207_Multiplayer_Attributes_Explanation.MKBoundControl3 = Nothing
		Me.lbl_207_Multiplayer_Attributes_Explanation.MKBoundControl4 = Nothing
		Me.lbl_207_Multiplayer_Attributes_Explanation.MKBoundControl5 = Nothing
		Me.lbl_207_Multiplayer_Attributes_Explanation.Name = "lbl_207_Multiplayer_Attributes_Explanation"
		Me.lbl_207_Multiplayer_Attributes_Explanation.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_207_Multiplayer_Attributes_Explanation.Size = New System.Drawing.Size(383, 97)
		Me.lbl_207_Multiplayer_Attributes_Explanation.TabIndex = 0
		Me.lbl_207_Multiplayer_Attributes_Explanation.Text = resources.GetString("lbl_207_Multiplayer_Attributes_Explanation.Text")
		'
		'tpg_206_Other_Attributes
		'
		Me.tpg_206_Other_Attributes.Controls.Add(Me.pnl_206_Other_Attributes)
		Me.tpg_206_Other_Attributes.Name = "tpg_206_Other_Attributes"
		Me.tpg_206_Other_Attributes.Size = New System.Drawing.Size(383, 474)
		Me.tpg_206_Other_Attributes.Text = "206_Other_Attributes"
		'
		'pnl_206_Other_Attributes
		'
		Me.pnl_206_Other_Attributes.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_206_Other_Attributes.Controls.Add(Me.pnl_206_Other_Attributes_Details)
		Me.pnl_206_Other_Attributes.Controls.Add(Me.lbl_206_Other_Attributes_Explanation)
		Me.pnl_206_Other_Attributes.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_206_Other_Attributes.Location = New System.Drawing.Point(0, 0)
		Me.pnl_206_Other_Attributes.Name = "pnl_206_Other_Attributes"
		Me.pnl_206_Other_Attributes.Size = New System.Drawing.Size(383, 474)
		Me.pnl_206_Other_Attributes.TabIndex = 17
		'
		'pnl_206_Other_Attributes_Details
		'
		Me.pnl_206_Other_Attributes_Details.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_206_Other_Attributes_Details.Controls.Add(Me.tlp_206_Other_Attributes)
		Me.pnl_206_Other_Attributes_Details.Controls.Add(Me.lbl_Weight_206_Other_Attributes)
		Me.pnl_206_Other_Attributes_Details.Controls.Add(Me.lbl_Weight_206_Other_Attributes_Text)
		Me.pnl_206_Other_Attributes_Details.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_206_Other_Attributes_Details.Location = New System.Drawing.Point(0, 84)
		Me.pnl_206_Other_Attributes_Details.Name = "pnl_206_Other_Attributes_Details"
		Me.pnl_206_Other_Attributes_Details.Size = New System.Drawing.Size(383, 390)
		Me.pnl_206_Other_Attributes_Details.TabIndex = 1
		'
		'tlp_206_Other_Attributes
		'
		Me.tlp_206_Other_Attributes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tlp_206_Other_Attributes.ColumnCount = 1
		Me.tlp_206_Other_Attributes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tlp_206_Other_Attributes.Controls.Add(Me.gb_206_Other_Attributes_AB, 0, 2)
		Me.tlp_206_Other_Attributes.Controls.Add(Me.gb_206_Other_Attributes_B, 0, 1)
		Me.tlp_206_Other_Attributes.Controls.Add(Me.gb_206_Other_Attributes_A, 0, 0)
		Me.tlp_206_Other_Attributes.Location = New System.Drawing.Point(0, 27)
		Me.tlp_206_Other_Attributes.Name = "tlp_206_Other_Attributes"
		Me.tlp_206_Other_Attributes.RowCount = 3
		Me.tlp_206_Other_Attributes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
		Me.tlp_206_Other_Attributes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
		Me.tlp_206_Other_Attributes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
		Me.tlp_206_Other_Attributes.Size = New System.Drawing.Size(383, 364)
		Me.tlp_206_Other_Attributes.TabIndex = 8
		'
		'gb_206_Other_Attributes_AB
		'
		Me.gb_206_Other_Attributes_AB.Controls.Add(Me.grd_206_Other_Attributes_AB)
		Me.gb_206_Other_Attributes_AB.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gb_206_Other_Attributes_AB.Location = New System.Drawing.Point(3, 245)
		Me.gb_206_Other_Attributes_AB.Name = "gb_206_Other_Attributes_AB"
		Me.gb_206_Other_Attributes_AB.Size = New System.Drawing.Size(377, 116)
		Me.gb_206_Other_Attributes_AB.TabIndex = 2
		Me.gb_206_Other_Attributes_AB.Text = "Tech Info shared by Games A and B"
		'
		'grd_206_Other_Attributes_AB
		'
		Me.grd_206_Other_Attributes_AB.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_206_Other_Attributes_AB.Location = New System.Drawing.Point(2, 20)
		Me.grd_206_Other_Attributes_AB.MainView = Me.gv_206_Other_Attributes_AB
		Me.grd_206_Other_Attributes_AB.Name = "grd_206_Other_Attributes_AB"
		Me.grd_206_Other_Attributes_AB.Size = New System.Drawing.Size(373, 94)
		Me.grd_206_Other_Attributes_AB.TabIndex = 3
		Me.grd_206_Other_Attributes_AB.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_206_Other_Attributes_AB})
		'
		'gv_206_Other_Attributes_AB
		'
		Me.gv_206_Other_Attributes_AB.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_206_Other_Attributes_AB})
		Me.gv_206_Other_Attributes_AB.GridControl = Me.grd_206_Other_Attributes_AB
		Me.gv_206_Other_Attributes_AB.Name = "gv_206_Other_Attributes_AB"
		Me.gv_206_Other_Attributes_AB.OptionsSelection.InvertSelection = True
		Me.gv_206_Other_Attributes_AB.OptionsView.ShowColumnHeaders = False
		Me.gv_206_Other_Attributes_AB.OptionsView.ShowGroupPanel = False
		Me.gv_206_Other_Attributes_AB.OptionsView.ShowIndicator = False
		'
		'col_206_Other_Attributes_AB
		'
		Me.col_206_Other_Attributes_AB.FieldName = "Name"
		Me.col_206_Other_Attributes_AB.Name = "col_206_Other_Attributes_AB"
		Me.col_206_Other_Attributes_AB.OptionsColumn.AllowEdit = False
		Me.col_206_Other_Attributes_AB.OptionsColumn.ReadOnly = True
		Me.col_206_Other_Attributes_AB.Visible = True
		Me.col_206_Other_Attributes_AB.VisibleIndex = 0
		'
		'gb_206_Other_Attributes_B
		'
		Me.gb_206_Other_Attributes_B.Controls.Add(Me.grd_206_Other_Attributes_B)
		Me.gb_206_Other_Attributes_B.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gb_206_Other_Attributes_B.Location = New System.Drawing.Point(3, 124)
		Me.gb_206_Other_Attributes_B.Name = "gb_206_Other_Attributes_B"
		Me.gb_206_Other_Attributes_B.Size = New System.Drawing.Size(377, 115)
		Me.gb_206_Other_Attributes_B.TabIndex = 1
		Me.gb_206_Other_Attributes_B.Text = "Tech Info of Game B"
		'
		'grd_206_Other_Attributes_B
		'
		Me.grd_206_Other_Attributes_B.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_206_Other_Attributes_B.Location = New System.Drawing.Point(2, 20)
		Me.grd_206_Other_Attributes_B.MainView = Me.gv_206_Other_Attributes_B
		Me.grd_206_Other_Attributes_B.Name = "grd_206_Other_Attributes_B"
		Me.grd_206_Other_Attributes_B.Size = New System.Drawing.Size(373, 93)
		Me.grd_206_Other_Attributes_B.TabIndex = 2
		Me.grd_206_Other_Attributes_B.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_206_Other_Attributes_B})
		'
		'gv_206_Other_Attributes_B
		'
		Me.gv_206_Other_Attributes_B.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_206_Other_Attributes_B})
		Me.gv_206_Other_Attributes_B.GridControl = Me.grd_206_Other_Attributes_B
		Me.gv_206_Other_Attributes_B.Name = "gv_206_Other_Attributes_B"
		Me.gv_206_Other_Attributes_B.OptionsSelection.InvertSelection = True
		Me.gv_206_Other_Attributes_B.OptionsView.ShowColumnHeaders = False
		Me.gv_206_Other_Attributes_B.OptionsView.ShowGroupPanel = False
		Me.gv_206_Other_Attributes_B.OptionsView.ShowIndicator = False
		'
		'col_206_Other_Attributes_B
		'
		Me.col_206_Other_Attributes_B.FieldName = "Name"
		Me.col_206_Other_Attributes_B.Name = "col_206_Other_Attributes_B"
		Me.col_206_Other_Attributes_B.OptionsColumn.AllowEdit = False
		Me.col_206_Other_Attributes_B.OptionsColumn.ReadOnly = True
		Me.col_206_Other_Attributes_B.Visible = True
		Me.col_206_Other_Attributes_B.VisibleIndex = 0
		'
		'gb_206_Other_Attributes_A
		'
		Me.gb_206_Other_Attributes_A.Controls.Add(Me.grd_206_Other_Attributes_A)
		Me.gb_206_Other_Attributes_A.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gb_206_Other_Attributes_A.Location = New System.Drawing.Point(3, 3)
		Me.gb_206_Other_Attributes_A.Name = "gb_206_Other_Attributes_A"
		Me.gb_206_Other_Attributes_A.Size = New System.Drawing.Size(377, 115)
		Me.gb_206_Other_Attributes_A.TabIndex = 0
		Me.gb_206_Other_Attributes_A.Text = "Tech Info of Game A"
		'
		'grd_206_Other_Attributes_A
		'
		Me.grd_206_Other_Attributes_A.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_206_Other_Attributes_A.Location = New System.Drawing.Point(2, 20)
		Me.grd_206_Other_Attributes_A.MainView = Me.gv_206_Other_Attributes_A
		Me.grd_206_Other_Attributes_A.Name = "grd_206_Other_Attributes_A"
		Me.grd_206_Other_Attributes_A.Size = New System.Drawing.Size(373, 93)
		Me.grd_206_Other_Attributes_A.TabIndex = 1
		Me.grd_206_Other_Attributes_A.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_206_Other_Attributes_A})
		'
		'gv_206_Other_Attributes_A
		'
		Me.gv_206_Other_Attributes_A.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_206_Other_Attributes_A})
		Me.gv_206_Other_Attributes_A.GridControl = Me.grd_206_Other_Attributes_A
		Me.gv_206_Other_Attributes_A.Name = "gv_206_Other_Attributes_A"
		Me.gv_206_Other_Attributes_A.OptionsSelection.InvertSelection = True
		Me.gv_206_Other_Attributes_A.OptionsView.ShowColumnHeaders = False
		Me.gv_206_Other_Attributes_A.OptionsView.ShowGroupPanel = False
		Me.gv_206_Other_Attributes_A.OptionsView.ShowIndicator = False
		'
		'col_206_Other_Attributes_A
		'
		Me.col_206_Other_Attributes_A.FieldName = "Name"
		Me.col_206_Other_Attributes_A.Name = "col_206_Other_Attributes_A"
		Me.col_206_Other_Attributes_A.OptionsColumn.AllowEdit = False
		Me.col_206_Other_Attributes_A.OptionsColumn.ReadOnly = True
		Me.col_206_Other_Attributes_A.Visible = True
		Me.col_206_Other_Attributes_A.VisibleIndex = 0
		'
		'lbl_Weight_206_Other_Attributes
		'
		Me.lbl_Weight_206_Other_Attributes.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Weight_206_Other_Attributes.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_206_Other_Attributes.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Weight_206_Other_Attributes.MKBoundControl1 = Nothing
		Me.lbl_Weight_206_Other_Attributes.MKBoundControl2 = Nothing
		Me.lbl_Weight_206_Other_Attributes.MKBoundControl3 = Nothing
		Me.lbl_Weight_206_Other_Attributes.MKBoundControl4 = Nothing
		Me.lbl_Weight_206_Other_Attributes.MKBoundControl5 = Nothing
		Me.lbl_Weight_206_Other_Attributes.Name = "lbl_Weight_206_Other_Attributes"
		Me.lbl_Weight_206_Other_Attributes.Size = New System.Drawing.Size(45, 20)
		Me.lbl_Weight_206_Other_Attributes.TabIndex = 7
		Me.lbl_Weight_206_Other_Attributes.Text = "Weight:"
		'
		'lbl_Weight_206_Other_Attributes_Text
		'
		Me.lbl_Weight_206_Other_Attributes_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Weight_206_Other_Attributes_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Weight_206_Other_Attributes_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_206_Other_Attributes_Text.Location = New System.Drawing.Point(51, 3)
		Me.lbl_Weight_206_Other_Attributes_Text.MKBoundControl1 = Nothing
		Me.lbl_Weight_206_Other_Attributes_Text.MKBoundControl2 = Nothing
		Me.lbl_Weight_206_Other_Attributes_Text.MKBoundControl3 = Nothing
		Me.lbl_Weight_206_Other_Attributes_Text.MKBoundControl4 = Nothing
		Me.lbl_Weight_206_Other_Attributes_Text.MKBoundControl5 = Nothing
		Me.lbl_Weight_206_Other_Attributes_Text.Name = "lbl_Weight_206_Other_Attributes_Text"
		Me.lbl_Weight_206_Other_Attributes_Text.Size = New System.Drawing.Size(328, 20)
		Me.lbl_Weight_206_Other_Attributes_Text.TabIndex = 7
		'
		'lbl_206_Other_Attributes_Explanation
		'
		Me.lbl_206_Other_Attributes_Explanation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_206_Other_Attributes_Explanation.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_206_Other_Attributes_Explanation.Location = New System.Drawing.Point(0, 0)
		Me.lbl_206_Other_Attributes_Explanation.MKBoundControl1 = Nothing
		Me.lbl_206_Other_Attributes_Explanation.MKBoundControl2 = Nothing
		Me.lbl_206_Other_Attributes_Explanation.MKBoundControl3 = Nothing
		Me.lbl_206_Other_Attributes_Explanation.MKBoundControl4 = Nothing
		Me.lbl_206_Other_Attributes_Explanation.MKBoundControl5 = Nothing
		Me.lbl_206_Other_Attributes_Explanation.Name = "lbl_206_Other_Attributes_Explanation"
		Me.lbl_206_Other_Attributes_Explanation.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_206_Other_Attributes_Explanation.Size = New System.Drawing.Size(383, 84)
		Me.lbl_206_Other_Attributes_Explanation.TabIndex = 0
		Me.lbl_206_Other_Attributes_Explanation.Text = resources.GetString("lbl_206_Other_Attributes_Explanation.Text")
		'
		'tpg_301_Group_Membership
		'
		Me.tpg_301_Group_Membership.Controls.Add(Me.pnl_301_Group_Membership)
		Me.tpg_301_Group_Membership.Name = "tpg_301_Group_Membership"
		Me.tpg_301_Group_Membership.Size = New System.Drawing.Size(383, 474)
		Me.tpg_301_Group_Membership.Text = "301_Group_Membership"
		'
		'pnl_301_Group_Membership
		'
		Me.pnl_301_Group_Membership.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_301_Group_Membership.Controls.Add(Me.pnl_301_Group_Membership_Details)
		Me.pnl_301_Group_Membership.Controls.Add(Me.lbl_301_Group_Membership_Explanation)
		Me.pnl_301_Group_Membership.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_301_Group_Membership.Location = New System.Drawing.Point(0, 0)
		Me.pnl_301_Group_Membership.Name = "pnl_301_Group_Membership"
		Me.pnl_301_Group_Membership.Size = New System.Drawing.Size(383, 474)
		Me.pnl_301_Group_Membership.TabIndex = 18
		'
		'pnl_301_Group_Membership_Details
		'
		Me.pnl_301_Group_Membership_Details.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_301_Group_Membership_Details.Controls.Add(Me.tlp_301_Group_Membership)
		Me.pnl_301_Group_Membership_Details.Controls.Add(Me.lbl_Weight_301_Group_Membership)
		Me.pnl_301_Group_Membership_Details.Controls.Add(Me.lbl_Weight_301_Group_Membership_Text)
		Me.pnl_301_Group_Membership_Details.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_301_Group_Membership_Details.Location = New System.Drawing.Point(0, 97)
		Me.pnl_301_Group_Membership_Details.Name = "pnl_301_Group_Membership_Details"
		Me.pnl_301_Group_Membership_Details.Size = New System.Drawing.Size(383, 377)
		Me.pnl_301_Group_Membership_Details.TabIndex = 1
		'
		'tlp_301_Group_Membership
		'
		Me.tlp_301_Group_Membership.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tlp_301_Group_Membership.ColumnCount = 1
		Me.tlp_301_Group_Membership.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tlp_301_Group_Membership.Controls.Add(Me.gb_301_Group_Membership_AB, 0, 2)
		Me.tlp_301_Group_Membership.Controls.Add(Me.gb_301_Group_Membership_B, 0, 1)
		Me.tlp_301_Group_Membership.Controls.Add(Me.gb_301_Group_Membership_A, 0, 0)
		Me.tlp_301_Group_Membership.Location = New System.Drawing.Point(0, 27)
		Me.tlp_301_Group_Membership.Name = "tlp_301_Group_Membership"
		Me.tlp_301_Group_Membership.RowCount = 3
		Me.tlp_301_Group_Membership.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
		Me.tlp_301_Group_Membership.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
		Me.tlp_301_Group_Membership.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
		Me.tlp_301_Group_Membership.Size = New System.Drawing.Size(383, 351)
		Me.tlp_301_Group_Membership.TabIndex = 8
		'
		'gb_301_Group_Membership_AB
		'
		Me.gb_301_Group_Membership_AB.Controls.Add(Me.grd_301_Group_Membership_AB)
		Me.gb_301_Group_Membership_AB.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gb_301_Group_Membership_AB.Location = New System.Drawing.Point(3, 237)
		Me.gb_301_Group_Membership_AB.Name = "gb_301_Group_Membership_AB"
		Me.gb_301_Group_Membership_AB.Size = New System.Drawing.Size(377, 111)
		Me.gb_301_Group_Membership_AB.TabIndex = 2
		Me.gb_301_Group_Membership_AB.Text = "Group Membership shared by Games A and B"
		'
		'grd_301_Group_Membership_AB
		'
		Me.grd_301_Group_Membership_AB.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_301_Group_Membership_AB.Location = New System.Drawing.Point(2, 20)
		Me.grd_301_Group_Membership_AB.MainView = Me.gv_301_Group_Membership_AB
		Me.grd_301_Group_Membership_AB.Name = "grd_301_Group_Membership_AB"
		Me.grd_301_Group_Membership_AB.Size = New System.Drawing.Size(373, 89)
		Me.grd_301_Group_Membership_AB.TabIndex = 3
		Me.grd_301_Group_Membership_AB.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_301_Group_Membership_AB})
		'
		'gv_301_Group_Membership_AB
		'
		Me.gv_301_Group_Membership_AB.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_301_Group_Membership_AB})
		Me.gv_301_Group_Membership_AB.GridControl = Me.grd_301_Group_Membership_AB
		Me.gv_301_Group_Membership_AB.Name = "gv_301_Group_Membership_AB"
		Me.gv_301_Group_Membership_AB.OptionsSelection.InvertSelection = True
		Me.gv_301_Group_Membership_AB.OptionsView.ShowColumnHeaders = False
		Me.gv_301_Group_Membership_AB.OptionsView.ShowGroupPanel = False
		Me.gv_301_Group_Membership_AB.OptionsView.ShowIndicator = False
		'
		'col_301_Group_Membership_AB
		'
		Me.col_301_Group_Membership_AB.FieldName = "Name"
		Me.col_301_Group_Membership_AB.Name = "col_301_Group_Membership_AB"
		Me.col_301_Group_Membership_AB.OptionsColumn.AllowEdit = False
		Me.col_301_Group_Membership_AB.OptionsColumn.ReadOnly = True
		Me.col_301_Group_Membership_AB.Visible = True
		Me.col_301_Group_Membership_AB.VisibleIndex = 0
		'
		'gb_301_Group_Membership_B
		'
		Me.gb_301_Group_Membership_B.Controls.Add(Me.grd_301_Group_Membership_B)
		Me.gb_301_Group_Membership_B.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gb_301_Group_Membership_B.Location = New System.Drawing.Point(3, 120)
		Me.gb_301_Group_Membership_B.Name = "gb_301_Group_Membership_B"
		Me.gb_301_Group_Membership_B.Size = New System.Drawing.Size(377, 111)
		Me.gb_301_Group_Membership_B.TabIndex = 1
		Me.gb_301_Group_Membership_B.Text = "Group Membership of Game B"
		'
		'grd_301_Group_Membership_B
		'
		Me.grd_301_Group_Membership_B.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_301_Group_Membership_B.Location = New System.Drawing.Point(2, 20)
		Me.grd_301_Group_Membership_B.MainView = Me.gv_301_Group_Membership_B
		Me.grd_301_Group_Membership_B.Name = "grd_301_Group_Membership_B"
		Me.grd_301_Group_Membership_B.Size = New System.Drawing.Size(373, 89)
		Me.grd_301_Group_Membership_B.TabIndex = 2
		Me.grd_301_Group_Membership_B.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_301_Group_Membership_B})
		'
		'gv_301_Group_Membership_B
		'
		Me.gv_301_Group_Membership_B.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_301_Group_Membership_B})
		Me.gv_301_Group_Membership_B.GridControl = Me.grd_301_Group_Membership_B
		Me.gv_301_Group_Membership_B.Name = "gv_301_Group_Membership_B"
		Me.gv_301_Group_Membership_B.OptionsSelection.InvertSelection = True
		Me.gv_301_Group_Membership_B.OptionsView.ShowColumnHeaders = False
		Me.gv_301_Group_Membership_B.OptionsView.ShowGroupPanel = False
		Me.gv_301_Group_Membership_B.OptionsView.ShowIndicator = False
		'
		'col_301_Group_Membership_B
		'
		Me.col_301_Group_Membership_B.FieldName = "Name"
		Me.col_301_Group_Membership_B.Name = "col_301_Group_Membership_B"
		Me.col_301_Group_Membership_B.OptionsColumn.AllowEdit = False
		Me.col_301_Group_Membership_B.OptionsColumn.ReadOnly = True
		Me.col_301_Group_Membership_B.Visible = True
		Me.col_301_Group_Membership_B.VisibleIndex = 0
		'
		'gb_301_Group_Membership_A
		'
		Me.gb_301_Group_Membership_A.Controls.Add(Me.grd_301_Group_Membership_A)
		Me.gb_301_Group_Membership_A.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gb_301_Group_Membership_A.Location = New System.Drawing.Point(3, 3)
		Me.gb_301_Group_Membership_A.Name = "gb_301_Group_Membership_A"
		Me.gb_301_Group_Membership_A.Size = New System.Drawing.Size(377, 111)
		Me.gb_301_Group_Membership_A.TabIndex = 0
		Me.gb_301_Group_Membership_A.Text = "Group Membership of Game A"
		'
		'grd_301_Group_Membership_A
		'
		Me.grd_301_Group_Membership_A.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_301_Group_Membership_A.Location = New System.Drawing.Point(2, 20)
		Me.grd_301_Group_Membership_A.MainView = Me.gv_301_Group_Membership_A
		Me.grd_301_Group_Membership_A.Name = "grd_301_Group_Membership_A"
		Me.grd_301_Group_Membership_A.Size = New System.Drawing.Size(373, 89)
		Me.grd_301_Group_Membership_A.TabIndex = 1
		Me.grd_301_Group_Membership_A.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_301_Group_Membership_A})
		'
		'gv_301_Group_Membership_A
		'
		Me.gv_301_Group_Membership_A.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_301_Group_Membership_A})
		Me.gv_301_Group_Membership_A.GridControl = Me.grd_301_Group_Membership_A
		Me.gv_301_Group_Membership_A.Name = "gv_301_Group_Membership_A"
		Me.gv_301_Group_Membership_A.OptionsSelection.InvertSelection = True
		Me.gv_301_Group_Membership_A.OptionsView.ShowColumnHeaders = False
		Me.gv_301_Group_Membership_A.OptionsView.ShowGroupPanel = False
		Me.gv_301_Group_Membership_A.OptionsView.ShowIndicator = False
		'
		'col_301_Group_Membership_A
		'
		Me.col_301_Group_Membership_A.FieldName = "Name"
		Me.col_301_Group_Membership_A.Name = "col_301_Group_Membership_A"
		Me.col_301_Group_Membership_A.OptionsColumn.AllowEdit = False
		Me.col_301_Group_Membership_A.OptionsColumn.ReadOnly = True
		Me.col_301_Group_Membership_A.Visible = True
		Me.col_301_Group_Membership_A.VisibleIndex = 0
		'
		'lbl_Weight_301_Group_Membership
		'
		Me.lbl_Weight_301_Group_Membership.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Weight_301_Group_Membership.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_301_Group_Membership.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Weight_301_Group_Membership.MKBoundControl1 = Nothing
		Me.lbl_Weight_301_Group_Membership.MKBoundControl2 = Nothing
		Me.lbl_Weight_301_Group_Membership.MKBoundControl3 = Nothing
		Me.lbl_Weight_301_Group_Membership.MKBoundControl4 = Nothing
		Me.lbl_Weight_301_Group_Membership.MKBoundControl5 = Nothing
		Me.lbl_Weight_301_Group_Membership.Name = "lbl_Weight_301_Group_Membership"
		Me.lbl_Weight_301_Group_Membership.Size = New System.Drawing.Size(45, 20)
		Me.lbl_Weight_301_Group_Membership.TabIndex = 7
		Me.lbl_Weight_301_Group_Membership.Text = "Weight:"
		'
		'lbl_Weight_301_Group_Membership_Text
		'
		Me.lbl_Weight_301_Group_Membership_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Weight_301_Group_Membership_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Weight_301_Group_Membership_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_301_Group_Membership_Text.Location = New System.Drawing.Point(51, 3)
		Me.lbl_Weight_301_Group_Membership_Text.MKBoundControl1 = Nothing
		Me.lbl_Weight_301_Group_Membership_Text.MKBoundControl2 = Nothing
		Me.lbl_Weight_301_Group_Membership_Text.MKBoundControl3 = Nothing
		Me.lbl_Weight_301_Group_Membership_Text.MKBoundControl4 = Nothing
		Me.lbl_Weight_301_Group_Membership_Text.MKBoundControl5 = Nothing
		Me.lbl_Weight_301_Group_Membership_Text.Name = "lbl_Weight_301_Group_Membership_Text"
		Me.lbl_Weight_301_Group_Membership_Text.Size = New System.Drawing.Size(328, 20)
		Me.lbl_Weight_301_Group_Membership_Text.TabIndex = 7
		'
		'lbl_301_Group_Membership_Explanation
		'
		Me.lbl_301_Group_Membership_Explanation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_301_Group_Membership_Explanation.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_301_Group_Membership_Explanation.Location = New System.Drawing.Point(0, 0)
		Me.lbl_301_Group_Membership_Explanation.MKBoundControl1 = Nothing
		Me.lbl_301_Group_Membership_Explanation.MKBoundControl2 = Nothing
		Me.lbl_301_Group_Membership_Explanation.MKBoundControl3 = Nothing
		Me.lbl_301_Group_Membership_Explanation.MKBoundControl4 = Nothing
		Me.lbl_301_Group_Membership_Explanation.MKBoundControl5 = Nothing
		Me.lbl_301_Group_Membership_Explanation.Name = "lbl_301_Group_Membership_Explanation"
		Me.lbl_301_Group_Membership_Explanation.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_301_Group_Membership_Explanation.Size = New System.Drawing.Size(383, 97)
		Me.lbl_301_Group_Membership_Explanation.TabIndex = 0
		Me.lbl_301_Group_Membership_Explanation.Text = resources.GetString("lbl_301_Group_Membership_Explanation.Text")
		'
		'tpg_401_Staff
		'
		Me.tpg_401_Staff.Controls.Add(Me.pnl_401_Staff)
		Me.tpg_401_Staff.Name = "tpg_401_Staff"
		Me.tpg_401_Staff.Size = New System.Drawing.Size(383, 474)
		Me.tpg_401_Staff.Text = "401_Staff"
		'
		'pnl_401_Staff
		'
		Me.pnl_401_Staff.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_401_Staff.Controls.Add(Me.pnl_401_Staff_Details)
		Me.pnl_401_Staff.Controls.Add(Me.lbl_401_Staff)
		Me.pnl_401_Staff.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_401_Staff.Location = New System.Drawing.Point(0, 0)
		Me.pnl_401_Staff.Name = "pnl_401_Staff"
		Me.pnl_401_Staff.Size = New System.Drawing.Size(383, 474)
		Me.pnl_401_Staff.TabIndex = 19
		'
		'pnl_401_Staff_Details
		'
		Me.pnl_401_Staff_Details.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		Me.pnl_401_Staff_Details.Controls.Add(Me.tlp_401_Staff)
		Me.pnl_401_Staff_Details.Controls.Add(Me.lbl_Weight_401_Staff)
		Me.pnl_401_Staff_Details.Controls.Add(Me.lbl_Weight_401_Staff_Text)
		Me.pnl_401_Staff_Details.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pnl_401_Staff_Details.Location = New System.Drawing.Point(0, 84)
		Me.pnl_401_Staff_Details.Name = "pnl_401_Staff_Details"
		Me.pnl_401_Staff_Details.Size = New System.Drawing.Size(383, 390)
		Me.pnl_401_Staff_Details.TabIndex = 1
		'
		'tlp_401_Staff
		'
		Me.tlp_401_Staff.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tlp_401_Staff.ColumnCount = 1
		Me.tlp_401_Staff.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tlp_401_Staff.Controls.Add(Me.gb_401_Staff_AB, 0, 2)
		Me.tlp_401_Staff.Controls.Add(Me.gb_401_Staff_B, 0, 1)
		Me.tlp_401_Staff.Controls.Add(Me.gb_401_Staff_A, 0, 0)
		Me.tlp_401_Staff.Location = New System.Drawing.Point(0, 27)
		Me.tlp_401_Staff.Name = "tlp_401_Staff"
		Me.tlp_401_Staff.RowCount = 3
		Me.tlp_401_Staff.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
		Me.tlp_401_Staff.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
		Me.tlp_401_Staff.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
		Me.tlp_401_Staff.Size = New System.Drawing.Size(383, 364)
		Me.tlp_401_Staff.TabIndex = 8
		'
		'gb_401_Staff_AB
		'
		Me.gb_401_Staff_AB.Controls.Add(Me.grd_401_Staff_AB)
		Me.gb_401_Staff_AB.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gb_401_Staff_AB.Location = New System.Drawing.Point(3, 245)
		Me.gb_401_Staff_AB.Name = "gb_401_Staff_AB"
		Me.gb_401_Staff_AB.Size = New System.Drawing.Size(377, 116)
		Me.gb_401_Staff_AB.TabIndex = 2
		Me.gb_401_Staff_AB.Text = "Staff involved in both Games A and B"
		'
		'grd_401_Staff_AB
		'
		Me.grd_401_Staff_AB.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_401_Staff_AB.Location = New System.Drawing.Point(2, 20)
		Me.grd_401_Staff_AB.MainView = Me.gv_401_Staff_AB
		Me.grd_401_Staff_AB.Name = "grd_401_Staff_AB"
		Me.grd_401_Staff_AB.Size = New System.Drawing.Size(373, 94)
		Me.grd_401_Staff_AB.TabIndex = 3
		Me.grd_401_Staff_AB.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_401_Staff_AB})
		'
		'gv_401_Staff_AB
		'
		Me.gv_401_Staff_AB.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_401_Staff_AB})
		Me.gv_401_Staff_AB.GridControl = Me.grd_401_Staff_AB
		Me.gv_401_Staff_AB.Name = "gv_401_Staff_AB"
		Me.gv_401_Staff_AB.OptionsSelection.InvertSelection = True
		Me.gv_401_Staff_AB.OptionsView.ShowColumnHeaders = False
		Me.gv_401_Staff_AB.OptionsView.ShowGroupPanel = False
		Me.gv_401_Staff_AB.OptionsView.ShowIndicator = False
		'
		'col_401_Staff_AB
		'
		Me.col_401_Staff_AB.FieldName = "Name"
		Me.col_401_Staff_AB.Name = "col_401_Staff_AB"
		Me.col_401_Staff_AB.OptionsColumn.AllowEdit = False
		Me.col_401_Staff_AB.OptionsColumn.ReadOnly = True
		Me.col_401_Staff_AB.Visible = True
		Me.col_401_Staff_AB.VisibleIndex = 0
		'
		'gb_401_Staff_B
		'
		Me.gb_401_Staff_B.Controls.Add(Me.grd_401_Staff_B)
		Me.gb_401_Staff_B.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gb_401_Staff_B.Location = New System.Drawing.Point(3, 124)
		Me.gb_401_Staff_B.Name = "gb_401_Staff_B"
		Me.gb_401_Staff_B.Size = New System.Drawing.Size(377, 115)
		Me.gb_401_Staff_B.TabIndex = 1
		Me.gb_401_Staff_B.Text = "Staff involved in Game B"
		'
		'grd_401_Staff_B
		'
		Me.grd_401_Staff_B.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_401_Staff_B.Location = New System.Drawing.Point(2, 20)
		Me.grd_401_Staff_B.MainView = Me.gv_401_Staff_B
		Me.grd_401_Staff_B.Name = "grd_401_Staff_B"
		Me.grd_401_Staff_B.Size = New System.Drawing.Size(373, 93)
		Me.grd_401_Staff_B.TabIndex = 2
		Me.grd_401_Staff_B.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_401_Staff_B})
		'
		'gv_401_Staff_B
		'
		Me.gv_401_Staff_B.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_401_Staff_B})
		Me.gv_401_Staff_B.GridControl = Me.grd_401_Staff_B
		Me.gv_401_Staff_B.Name = "gv_401_Staff_B"
		Me.gv_401_Staff_B.OptionsSelection.InvertSelection = True
		Me.gv_401_Staff_B.OptionsView.ShowColumnHeaders = False
		Me.gv_401_Staff_B.OptionsView.ShowGroupPanel = False
		Me.gv_401_Staff_B.OptionsView.ShowIndicator = False
		'
		'col_401_Staff_B
		'
		Me.col_401_Staff_B.FieldName = "Name"
		Me.col_401_Staff_B.Name = "col_401_Staff_B"
		Me.col_401_Staff_B.OptionsColumn.AllowEdit = False
		Me.col_401_Staff_B.OptionsColumn.ReadOnly = True
		Me.col_401_Staff_B.Visible = True
		Me.col_401_Staff_B.VisibleIndex = 0
		'
		'gb_401_Staff_A
		'
		Me.gb_401_Staff_A.Controls.Add(Me.grd_401_Staff_A)
		Me.gb_401_Staff_A.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gb_401_Staff_A.Location = New System.Drawing.Point(3, 3)
		Me.gb_401_Staff_A.Name = "gb_401_Staff_A"
		Me.gb_401_Staff_A.Size = New System.Drawing.Size(377, 115)
		Me.gb_401_Staff_A.TabIndex = 0
		Me.gb_401_Staff_A.Text = "Staff involved in Game A"
		'
		'grd_401_Staff_A
		'
		Me.grd_401_Staff_A.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grd_401_Staff_A.Location = New System.Drawing.Point(2, 20)
		Me.grd_401_Staff_A.MainView = Me.gv_401_Staff_A
		Me.grd_401_Staff_A.Name = "grd_401_Staff_A"
		Me.grd_401_Staff_A.Size = New System.Drawing.Size(373, 93)
		Me.grd_401_Staff_A.TabIndex = 1
		Me.grd_401_Staff_A.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_401_Staff_A})
		'
		'gv_401_Staff_A
		'
		Me.gv_401_Staff_A.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_401_Staff_A})
		Me.gv_401_Staff_A.GridControl = Me.grd_401_Staff_A
		Me.gv_401_Staff_A.Name = "gv_401_Staff_A"
		Me.gv_401_Staff_A.OptionsSelection.InvertSelection = True
		Me.gv_401_Staff_A.OptionsView.ShowColumnHeaders = False
		Me.gv_401_Staff_A.OptionsView.ShowGroupPanel = False
		Me.gv_401_Staff_A.OptionsView.ShowIndicator = False
		'
		'col_401_Staff_A
		'
		Me.col_401_Staff_A.FieldName = "Name"
		Me.col_401_Staff_A.Name = "col_401_Staff_A"
		Me.col_401_Staff_A.OptionsColumn.AllowEdit = False
		Me.col_401_Staff_A.OptionsColumn.ReadOnly = True
		Me.col_401_Staff_A.Visible = True
		Me.col_401_Staff_A.VisibleIndex = 0
		'
		'lbl_Weight_401_Staff
		'
		Me.lbl_Weight_401_Staff.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Weight_401_Staff.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_401_Staff.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Weight_401_Staff.MKBoundControl1 = Nothing
		Me.lbl_Weight_401_Staff.MKBoundControl2 = Nothing
		Me.lbl_Weight_401_Staff.MKBoundControl3 = Nothing
		Me.lbl_Weight_401_Staff.MKBoundControl4 = Nothing
		Me.lbl_Weight_401_Staff.MKBoundControl5 = Nothing
		Me.lbl_Weight_401_Staff.Name = "lbl_Weight_401_Staff"
		Me.lbl_Weight_401_Staff.Size = New System.Drawing.Size(45, 20)
		Me.lbl_Weight_401_Staff.TabIndex = 7
		Me.lbl_Weight_401_Staff.Text = "Weight:"
		'
		'lbl_Weight_401_Staff_Text
		'
		Me.lbl_Weight_401_Staff_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Weight_401_Staff_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Weight_401_Staff_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Weight_401_Staff_Text.Location = New System.Drawing.Point(51, 3)
		Me.lbl_Weight_401_Staff_Text.MKBoundControl1 = Nothing
		Me.lbl_Weight_401_Staff_Text.MKBoundControl2 = Nothing
		Me.lbl_Weight_401_Staff_Text.MKBoundControl3 = Nothing
		Me.lbl_Weight_401_Staff_Text.MKBoundControl4 = Nothing
		Me.lbl_Weight_401_Staff_Text.MKBoundControl5 = Nothing
		Me.lbl_Weight_401_Staff_Text.Name = "lbl_Weight_401_Staff_Text"
		Me.lbl_Weight_401_Staff_Text.Size = New System.Drawing.Size(328, 20)
		Me.lbl_Weight_401_Staff_Text.TabIndex = 7
		'
		'lbl_401_Staff
		'
		Me.lbl_401_Staff.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
		Me.lbl_401_Staff.Dock = System.Windows.Forms.DockStyle.Top
		Me.lbl_401_Staff.Location = New System.Drawing.Point(0, 0)
		Me.lbl_401_Staff.MKBoundControl1 = Nothing
		Me.lbl_401_Staff.MKBoundControl2 = Nothing
		Me.lbl_401_Staff.MKBoundControl3 = Nothing
		Me.lbl_401_Staff.MKBoundControl4 = Nothing
		Me.lbl_401_Staff.MKBoundControl5 = Nothing
		Me.lbl_401_Staff.Name = "lbl_401_Staff"
		Me.lbl_401_Staff.Padding = New System.Windows.Forms.Padding(3)
		Me.lbl_401_Staff.Size = New System.Drawing.Size(383, 84)
		Me.lbl_401_Staff.TabIndex = 0
		Me.lbl_401_Staff.Text = resources.GetString("lbl_401_Staff.Text")
		'
		'DataTable1
		'
		Me.DataTable1.Columns.AddRange(New System.Data.DataColumn() {Me.col_id, Me.DataColumn2, Me.DataColumn3, Me.DataColumn4})
		Me.DataTable1.TableName = "Table1"
		'
		'col_id
		'
		Me.col_id.ColumnName = "id"
		Me.col_id.DataType = GetType(Integer)
		'
		'DataColumn2
		'
		Me.DataColumn2.ColumnName = "Feature"
		'
		'DataColumn3
		'
		Me.DataColumn3.ColumnName = "Calculation"
		'
		'DataColumn4
		'
		Me.DataColumn4.ColumnName = "Score"
		'
		'lbl_Game_A
		'
		Me.lbl_Game_A.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Game_A.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Game_A.Location = New System.Drawing.Point(3, 3)
		Me.lbl_Game_A.MKBoundControl1 = Nothing
		Me.lbl_Game_A.MKBoundControl2 = Nothing
		Me.lbl_Game_A.MKBoundControl3 = Nothing
		Me.lbl_Game_A.MKBoundControl4 = Nothing
		Me.lbl_Game_A.MKBoundControl5 = Nothing
		Me.lbl_Game_A.Name = "lbl_Game_A"
		Me.lbl_Game_A.Size = New System.Drawing.Size(121, 20)
		Me.lbl_Game_A.TabIndex = 7
		Me.lbl_Game_A.Text = "Game A:"
		'
		'lbl_Game_A_Text
		'
		Me.lbl_Game_A_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Game_A_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Game_A_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Game_A_Text.Location = New System.Drawing.Point(127, 3)
		Me.lbl_Game_A_Text.MKBoundControl1 = Nothing
		Me.lbl_Game_A_Text.MKBoundControl2 = Nothing
		Me.lbl_Game_A_Text.MKBoundControl3 = Nothing
		Me.lbl_Game_A_Text.MKBoundControl4 = Nothing
		Me.lbl_Game_A_Text.MKBoundControl5 = Nothing
		Me.lbl_Game_A_Text.Name = "lbl_Game_A_Text"
		Me.lbl_Game_A_Text.Size = New System.Drawing.Size(654, 20)
		Me.lbl_Game_A_Text.TabIndex = 7
		'
		'lbl_Game_B
		'
		Me.lbl_Game_B.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Game_B.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Game_B.Location = New System.Drawing.Point(3, 26)
		Me.lbl_Game_B.MKBoundControl1 = Nothing
		Me.lbl_Game_B.MKBoundControl2 = Nothing
		Me.lbl_Game_B.MKBoundControl3 = Nothing
		Me.lbl_Game_B.MKBoundControl4 = Nothing
		Me.lbl_Game_B.MKBoundControl5 = Nothing
		Me.lbl_Game_B.Name = "lbl_Game_B"
		Me.lbl_Game_B.Size = New System.Drawing.Size(121, 20)
		Me.lbl_Game_B.TabIndex = 7
		Me.lbl_Game_B.Text = "Game B:"
		'
		'lbl_Game_B_Text
		'
		Me.lbl_Game_B_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Game_B_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Game_B_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Game_B_Text.Location = New System.Drawing.Point(127, 26)
		Me.lbl_Game_B_Text.MKBoundControl1 = Nothing
		Me.lbl_Game_B_Text.MKBoundControl2 = Nothing
		Me.lbl_Game_B_Text.MKBoundControl3 = Nothing
		Me.lbl_Game_B_Text.MKBoundControl4 = Nothing
		Me.lbl_Game_B_Text.MKBoundControl5 = Nothing
		Me.lbl_Game_B_Text.Name = "lbl_Game_B_Text"
		Me.lbl_Game_B_Text.Size = New System.Drawing.Size(654, 20)
		Me.lbl_Game_B_Text.TabIndex = 7
		'
		'lbl_Similarity
		'
		Me.lbl_Similarity.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Similarity.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Similarity.Location = New System.Drawing.Point(3, 72)
		Me.lbl_Similarity.MKBoundControl1 = Nothing
		Me.lbl_Similarity.MKBoundControl2 = Nothing
		Me.lbl_Similarity.MKBoundControl3 = Nothing
		Me.lbl_Similarity.MKBoundControl4 = Nothing
		Me.lbl_Similarity.MKBoundControl5 = Nothing
		Me.lbl_Similarity.Name = "lbl_Similarity"
		Me.lbl_Similarity.Size = New System.Drawing.Size(121, 20)
		ToolTipTitleItem1.Text = "Overall Similarity"
		ToolTipItem1.LeftIndent = 6
		ToolTipItem1.Text = resources.GetString("ToolTipItem1.Text")
		SuperToolTip1.Items.Add(ToolTipTitleItem1)
		SuperToolTip1.Items.Add(ToolTipItem1)
		Me.lbl_Similarity.SuperTip = SuperToolTip1
		Me.lbl_Similarity.TabIndex = 7
		Me.lbl_Similarity.Text = "Overall Similarity:"
		'
		'lbl_Similarity_Text
		'
		Me.lbl_Similarity_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Similarity_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Similarity_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Similarity_Text.Location = New System.Drawing.Point(127, 72)
		Me.lbl_Similarity_Text.MKBoundControl1 = Nothing
		Me.lbl_Similarity_Text.MKBoundControl2 = Nothing
		Me.lbl_Similarity_Text.MKBoundControl3 = Nothing
		Me.lbl_Similarity_Text.MKBoundControl4 = Nothing
		Me.lbl_Similarity_Text.MKBoundControl5 = Nothing
		Me.lbl_Similarity_Text.Name = "lbl_Similarity_Text"
		Me.lbl_Similarity_Text.Size = New System.Drawing.Size(654, 20)
		ToolTipTitleItem2.Text = "Overall Similarity"
		ToolTipItem2.LeftIndent = 6
		ToolTipItem2.Text = resources.GetString("ToolTipItem2.Text")
		SuperToolTip2.Items.Add(ToolTipTitleItem2)
		SuperToolTip2.Items.Add(ToolTipItem2)
		Me.lbl_Similarity_Text.SuperTip = SuperToolTip2
		Me.lbl_Similarity_Text.TabIndex = 7
		'
		'btn_Close
		'
		Me.btn_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btn_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btn_Close.Location = New System.Drawing.Point(706, 602)
		Me.btn_Close.Name = "btn_Close"
		Me.btn_Close.Size = New System.Drawing.Size(75, 19)
		Me.btn_Close.TabIndex = 0
		Me.btn_Close.Text = "&Close"
		'
		'lbl_Similarity_Calculation_Configuration
		'
		Me.lbl_Similarity_Calculation_Configuration.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
		Me.lbl_Similarity_Calculation_Configuration.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Similarity_Calculation_Configuration.Location = New System.Drawing.Point(3, 49)
		Me.lbl_Similarity_Calculation_Configuration.MKBoundControl1 = Nothing
		Me.lbl_Similarity_Calculation_Configuration.MKBoundControl2 = Nothing
		Me.lbl_Similarity_Calculation_Configuration.MKBoundControl3 = Nothing
		Me.lbl_Similarity_Calculation_Configuration.MKBoundControl4 = Nothing
		Me.lbl_Similarity_Calculation_Configuration.MKBoundControl5 = Nothing
		Me.lbl_Similarity_Calculation_Configuration.Name = "lbl_Similarity_Calculation_Configuration"
		Me.lbl_Similarity_Calculation_Configuration.Size = New System.Drawing.Size(121, 20)
		Me.lbl_Similarity_Calculation_Configuration.TabIndex = 7
		Me.lbl_Similarity_Calculation_Configuration.Text = "Similarity Configuration:"
		'
		'lbl_Similarity_Calculation_Configuration_Text
		'
		Me.lbl_Similarity_Calculation_Configuration_Text.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbl_Similarity_Calculation_Configuration_Text.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
		Me.lbl_Similarity_Calculation_Configuration_Text.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
		Me.lbl_Similarity_Calculation_Configuration_Text.Location = New System.Drawing.Point(127, 49)
		Me.lbl_Similarity_Calculation_Configuration_Text.MKBoundControl1 = Nothing
		Me.lbl_Similarity_Calculation_Configuration_Text.MKBoundControl2 = Nothing
		Me.lbl_Similarity_Calculation_Configuration_Text.MKBoundControl3 = Nothing
		Me.lbl_Similarity_Calculation_Configuration_Text.MKBoundControl4 = Nothing
		Me.lbl_Similarity_Calculation_Configuration_Text.MKBoundControl5 = Nothing
		Me.lbl_Similarity_Calculation_Configuration_Text.Name = "lbl_Similarity_Calculation_Configuration_Text"
		Me.lbl_Similarity_Calculation_Configuration_Text.Size = New System.Drawing.Size(654, 20)
		Me.lbl_Similarity_Calculation_Configuration_Text.TabIndex = 7
		'
		'DataColumn5
		'
		Me.DataColumn5.ColumnName = "Weight"
		'
		'DataColumn6
		'
		Me.DataColumn6.ColumnName = "Weighted_Score"
		'
		'frm_Similarity_Calculation_Details
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(784, 624)
		Me.Controls.Add(Me.btn_Close)
		Me.Controls.Add(Me.lbl_Similarity_Calculation_Configuration_Text)
		Me.Controls.Add(Me.lbl_Similarity_Text)
		Me.Controls.Add(Me.lbl_Game_B_Text)
		Me.Controls.Add(Me.lbl_Game_A_Text)
		Me.Controls.Add(Me.lbl_Similarity_Calculation_Configuration)
		Me.Controls.Add(Me.lbl_Similarity)
		Me.Controls.Add(Me.lbl_Game_B)
		Me.Controls.Add(Me.lbl_Game_A)
		Me.Controls.Add(Me.splt_Main)
		Me.Name = "frm_Similarity_Calculation_Details"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Similarity Calculation Details"
		CType(Me.splt_Main, System.ComponentModel.ISupportInitialize).EndInit()
		Me.splt_Main.ResumeLayout(False)
		CType(Me.grd_Main, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.BTA_Main, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_Main, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.tcl_Main, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tcl_Main.ResumeLayout(False)
		Me.tpg_001_Platform.ResumeLayout(False)
		CType(Me.pnl_001_Platform, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_001_Platform.ResumeLayout(False)
		CType(Me.pnl_001_Platform_Details, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_001_Platform_Details.ResumeLayout(False)
		Me.tpg_002_MobyRank.ResumeLayout(False)
		CType(Me.pnl_002_MobyRank, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_002_MobyRank.ResumeLayout(False)
		CType(Me.pnl_002_MobyRank_Details, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_002_MobyRank_Details.ResumeLayout(False)
		Me.tpg_003_MobyScore.ResumeLayout(False)
		CType(Me.pnl_003_MobyScore, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_003_MobyScore.ResumeLayout(False)
		CType(Me.pnl_003_MobyScore_Details, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_003_MobyScore_Details.ResumeLayout(False)
		Me.tpg_004_Publisher.ResumeLayout(False)
		CType(Me.pnl_004_Publisher, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_004_Publisher.ResumeLayout(False)
		CType(Me.pnl_004_Publisher_Details, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_004_Publisher_Details.ResumeLayout(False)
		Me.tpg_005_Developer.ResumeLayout(False)
		CType(Me.pnl_005_Developer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_005_Developer.ResumeLayout(False)
		CType(Me.pnl_005_Developer_Details, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_005_Developer_Details.ResumeLayout(False)
		Me.tpg_006_Year.ResumeLayout(False)
		CType(Me.pnl_006_Year, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_006_Year.ResumeLayout(False)
		CType(Me.pnl_006_Year_Details, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_006_Year_Details.ResumeLayout(False)
		Me.tpg_101_Basic_Genres.ResumeLayout(False)
		Me.tpg_102_Perspectives.ResumeLayout(False)
		Me.tpg_107_Visual_Presentation.ResumeLayout(False)
		Me.tpg_108_Gameplay.ResumeLayout(False)
		Me.tpg_109_Pacing.ResumeLayout(False)
		Me.tpg_110_Narrative_Theme_Topic.ResumeLayout(False)
		Me.tpg_111_Setting.ResumeLayout(False)
		Me.tpg_103_Sports_Themes.ResumeLayout(False)
		Me.tpg_112_Vehicular_Themes.ResumeLayout(False)
		Me.tpg_105_Educational_Categories.ResumeLayout(False)
		Me.tpg_113_Interface_Control.ResumeLayout(False)
		Me.tpg_114_DLC_Addon.ResumeLayout(False)
		Me.tpg_115_Special_Edition.ResumeLayout(False)
		Me.tpg_106_Other_Attributes.ResumeLayout(False)
		Me.tpg_201_MinPlayers.ResumeLayout(False)
		CType(Me.pnl_201_MinPlayers, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_201_MinPlayers.ResumeLayout(False)
		CType(Me.pnl_201_MinPlayers_Details, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_201_MinPlayers_Details.ResumeLayout(False)
		Me.tpg_202_MaxPlayers.ResumeLayout(False)
		CType(Me.pnl_202_MaxPlayers, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_202_MaxPlayers.ResumeLayout(False)
		CType(Me.pnl_202_MaxPlayers_Details, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_202_MaxPlayers_Details.ResumeLayout(False)
		Me.tpg_203_AgeO.ResumeLayout(False)
		CType(Me.pnl_203_AgeO, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_203_AgeO.ResumeLayout(False)
		CType(Me.pnl_203_AgeO_Details, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_203_AgeO_Details.ResumeLayout(False)
		Me.tpg_204_AgeP.ResumeLayout(False)
		CType(Me.pnl_204_AgeP, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_204_AgeP.ResumeLayout(False)
		CType(Me.pnl_204_AgeP_Details, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_204_AgeP_Details.ResumeLayout(False)
		Me.tpg_205_Rating_Descriptors.ResumeLayout(False)
		CType(Me.pnl_205_Rating_Descriptors, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_205_Rating_Descriptors.ResumeLayout(False)
		CType(Me.pnl_205_Rating_Descriptors_Details, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_205_Rating_Descriptors_Details.ResumeLayout(False)
		Me.tlp_205_Rating_Descriptors.ResumeLayout(False)
		CType(Me.gb_205_Rating_Descriptors_AB, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_205_Rating_Descriptors_AB.ResumeLayout(False)
		CType(Me.grd_205_Rating_Descriptors_AB, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_205_Rating_Descriptors_AB, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gb_205_Rating_Descriptors_B, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_205_Rating_Descriptors_B.ResumeLayout(False)
		CType(Me.grd_205_Rating_Descriptors_B, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_205_Rating_Descriptors_B, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gb_205_Rating_Descriptors_A, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_205_Rating_Descriptors_A.ResumeLayout(False)
		CType(Me.grd_205_Rating_Descriptors_A, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_205_Rating_Descriptors_A, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tpg_207_Multiplayer_Attributes.ResumeLayout(False)
		CType(Me.pnl_207_Multiplayer_Attributes, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_207_Multiplayer_Attributes.ResumeLayout(False)
		CType(Me.pnl_207_Multiplayer_Attributes_Details, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_207_Multiplayer_Attributes_Details.ResumeLayout(False)
		Me.tlp_207_Multiplayer_Attributes.ResumeLayout(False)
		CType(Me.gb_207_Multiplayer_Attributes_AB, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_207_Multiplayer_Attributes_AB.ResumeLayout(False)
		CType(Me.grd_207_Multiplayer_Attributes_AB, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_207_Multiplayer_Attributes_AB, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gb_207_Multiplayer_Attributes_B, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_207_Multiplayer_Attributes_B.ResumeLayout(False)
		CType(Me.grd_207_Multiplayer_Attributes_B, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_207_Multiplayer_Attributes_B, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gb_207_Multiplayer_Attributes_A, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_207_Multiplayer_Attributes_A.ResumeLayout(False)
		CType(Me.grd_207_Multiplayer_Attributes_A, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_207_Multiplayer_Attributes_A, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tpg_206_Other_Attributes.ResumeLayout(False)
		CType(Me.pnl_206_Other_Attributes, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_206_Other_Attributes.ResumeLayout(False)
		CType(Me.pnl_206_Other_Attributes_Details, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_206_Other_Attributes_Details.ResumeLayout(False)
		Me.tlp_206_Other_Attributes.ResumeLayout(False)
		CType(Me.gb_206_Other_Attributes_AB, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_206_Other_Attributes_AB.ResumeLayout(False)
		CType(Me.grd_206_Other_Attributes_AB, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_206_Other_Attributes_AB, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gb_206_Other_Attributes_B, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_206_Other_Attributes_B.ResumeLayout(False)
		CType(Me.grd_206_Other_Attributes_B, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_206_Other_Attributes_B, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gb_206_Other_Attributes_A, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_206_Other_Attributes_A.ResumeLayout(False)
		CType(Me.grd_206_Other_Attributes_A, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_206_Other_Attributes_A, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tpg_301_Group_Membership.ResumeLayout(False)
		CType(Me.pnl_301_Group_Membership, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_301_Group_Membership.ResumeLayout(False)
		CType(Me.pnl_301_Group_Membership_Details, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_301_Group_Membership_Details.ResumeLayout(False)
		Me.tlp_301_Group_Membership.ResumeLayout(False)
		CType(Me.gb_301_Group_Membership_AB, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_301_Group_Membership_AB.ResumeLayout(False)
		CType(Me.grd_301_Group_Membership_AB, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_301_Group_Membership_AB, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gb_301_Group_Membership_B, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_301_Group_Membership_B.ResumeLayout(False)
		CType(Me.grd_301_Group_Membership_B, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_301_Group_Membership_B, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gb_301_Group_Membership_A, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_301_Group_Membership_A.ResumeLayout(False)
		CType(Me.grd_301_Group_Membership_A, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_301_Group_Membership_A, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tpg_401_Staff.ResumeLayout(False)
		CType(Me.pnl_401_Staff, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_401_Staff.ResumeLayout(False)
		CType(Me.pnl_401_Staff_Details, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnl_401_Staff_Details.ResumeLayout(False)
		Me.tlp_401_Staff.ResumeLayout(False)
		CType(Me.gb_401_Staff_AB, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_401_Staff_AB.ResumeLayout(False)
		CType(Me.grd_401_Staff_AB, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_401_Staff_AB, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gb_401_Staff_B, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_401_Staff_B.ResumeLayout(False)
		CType(Me.grd_401_Staff_B, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_401_Staff_B, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gb_401_Staff_A, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gb_401_Staff_A.ResumeLayout(False)
		CType(Me.grd_401_Staff_A, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.gv_401_Staff_A, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DataTable1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents splt_Main As MKNetDXLib.ctl_MKDXSplitPanel
	Friend WithEvents tcl_Main As MKNetDXLib.ctl_MKDXTabControl
	Friend WithEvents tpg_001_Platform As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_002_MobyRank As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_003_MobyScore As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_004_Publisher As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_005_Developer As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_006_Year As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_101_Basic_Genres As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_102_Perspectives As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_103_Sports_Themes As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_105_Educational_Categories As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_106_Other_Attributes As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_201_MinPlayers As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_202_MaxPlayers As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_203_AgeO As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_204_AgeP As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_205_Rating_Descriptors As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_206_Other_Attributes As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_301_Group_Membership As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_401_Staff As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents grd_Main As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents BTA_Main As MKNetLib.cmp_MKBindableTableAdapter
	Friend WithEvents gv_Main As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents colFeature As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colScore As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents DataTable1 As System.Data.DataTable
	Friend WithEvents col_id As System.Data.DataColumn
	Friend WithEvents DataColumn2 As System.Data.DataColumn
	Friend WithEvents DataColumn3 As System.Data.DataColumn
	Friend WithEvents DataColumn4 As System.Data.DataColumn
	Friend WithEvents lbl_Game_A As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Game_A_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Game_B As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Game_B_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Similarity As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Similarity_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents btn_Close As MKNetDXLib.ctl_MKDXSimpleButton
	Friend WithEvents lbl_Similarity_Calculation_Configuration As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Similarity_Calculation_Configuration_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_001_Platform_B As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_001_Platform_A As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_001_Platform_B_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_001_Platform_A_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents pnl_001_Platform As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents pnl_001_Platform_Details As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents lbl_Weight_001_Platform As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Weight_001_Platform_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_001_Platform_Explanation As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents pnl_002_MobyRank As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents pnl_002_MobyRank_Details As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents lbl_Weight_002_MobyRank As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_002_MobyRank_A As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_002_MobyRank_B As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Weight_002_MobyRank_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_002_MobyRank_A_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_002_MobyRank_B_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_002_MobyRank_Explanation As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents pnl_003_MobyScore As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents pnl_003_MobyScore_Details As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents lbl_Weight_003_MobyScore As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_003_MobyScore_A As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_003_MobyScore_B As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Weight_003_MobyScore_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_003_MobyScore_A_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_003_MobyScore_B_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_003_MobyScore_Explanation As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents pnl_004_Publisher As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents pnl_004_Publisher_Details As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents lbl_Weight_004_Publisher As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_004_Publisher_A As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_004_Publisher_B As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Weight_004_Publisher_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_004_Publisher_A_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_004_Publisher_B_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_004_Publisher_Explanation As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents pnl_005_Developer As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents pnl_005_Developer_Details As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents lbl_Weight_005_Developer As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_005_Developer_A As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_005_Developer_B As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Weight_005_Developer_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_005_Developer_A_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_005_Developer_B_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_005_Developer_Explanation As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents pnl_006_Year As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents pnl_006_Year_Details As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents lbl_Weight_006_Year As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_006_Year_A As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_006_Year_B As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Weight_006_Year_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_006_Year_A_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_006_Year_B_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_006_Year_Explanation As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents pnl_201_MinPlayers As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents pnl_201_MinPlayers_Details As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents lbl_Weight_201_MinPlayers As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_201_MinPlayers_A As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_201_MinPlayers_B As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Weight_201_MinPlayers_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_201_MinPlayers_A_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_201_MinPlayers_B_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_201_MinPlayers_Explanation As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents pnl_202_MaxPlayers As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents pnl_202_MaxPlayers_Details As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents lbl_Weight_202_MaxPlayers As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_202_MaxPlayers_A As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_202_MaxPlayers_B As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Weight_202_MaxPlayers_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_202_MaxPlayers_A_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_202_MaxPlayers_B_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_202_MaxPlayers_Explanation As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents pnl_203_AgeO As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents pnl_203_AgeO_Details As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents lbl_Weight_203_AgeO As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_203_AgeO_A As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_203_AgeO_B As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Weight_203_AgeO_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_203_AgeO_A_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_203_AgeO_B_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_203_AgeO_Explanation As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents pnl_204_AgeP As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents pnl_204_AgeP_Details As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents lbl_Weight_204_AgeP As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_204_AgeP_A As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_204_AgeP_B As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Weight_204_AgeP_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_204_AgeP_A_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_204_AgeP_B_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_204_AgeP As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents pnl_205_Rating_Descriptors As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents pnl_205_Rating_Descriptors_Details As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents tlp_205_Rating_Descriptors As System.Windows.Forms.TableLayoutPanel
	Friend WithEvents gb_205_Rating_Descriptors_AB As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents grd_205_Rating_Descriptors_AB As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_205_Rating_Descriptors_AB As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_205_Rating_Descriptors_AB As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents gb_205_Rating_Descriptors_B As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents grd_205_Rating_Descriptors_B As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_205_Rating_Descriptors_B As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_205_Rating_Descriptors_B As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents gb_205_Rating_Descriptors_A As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents grd_205_Rating_Descriptors_A As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_205_Rating_Descriptors_A As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_205_Rating_Descriptors_A As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents lbl_Weight_205_Rating_Descriptors As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Weight_205_Rating_Descriptors_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_205_Rating_Descriptors_Explanation As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents pnl_206_Other_Attributes As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents pnl_206_Other_Attributes_Details As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents tlp_206_Other_Attributes As System.Windows.Forms.TableLayoutPanel
	Friend WithEvents gb_206_Other_Attributes_AB As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents grd_206_Other_Attributes_AB As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_206_Other_Attributes_AB As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_206_Other_Attributes_AB As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents gb_206_Other_Attributes_B As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents grd_206_Other_Attributes_B As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_206_Other_Attributes_B As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_206_Other_Attributes_B As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents gb_206_Other_Attributes_A As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents grd_206_Other_Attributes_A As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_206_Other_Attributes_A As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_206_Other_Attributes_A As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents lbl_Weight_206_Other_Attributes As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Weight_206_Other_Attributes_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_206_Other_Attributes_Explanation As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents pnl_301_Group_Membership As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents pnl_301_Group_Membership_Details As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents tlp_301_Group_Membership As System.Windows.Forms.TableLayoutPanel
	Friend WithEvents gb_301_Group_Membership_AB As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents grd_301_Group_Membership_AB As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_301_Group_Membership_AB As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_301_Group_Membership_AB As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents gb_301_Group_Membership_B As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents grd_301_Group_Membership_B As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_301_Group_Membership_B As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_301_Group_Membership_B As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents gb_301_Group_Membership_A As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents grd_301_Group_Membership_A As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_301_Group_Membership_A As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_301_Group_Membership_A As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents lbl_Weight_301_Group_Membership As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Weight_301_Group_Membership_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_301_Group_Membership_Explanation As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents pnl_401_Staff As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents pnl_401_Staff_Details As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents tlp_401_Staff As System.Windows.Forms.TableLayoutPanel
	Friend WithEvents gb_401_Staff_AB As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents grd_401_Staff_AB As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_401_Staff_AB As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_401_Staff_AB As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents gb_401_Staff_B As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents grd_401_Staff_B As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_401_Staff_B As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_401_Staff_B As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents gb_401_Staff_A As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents grd_401_Staff_A As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_401_Staff_A As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_401_Staff_A As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents lbl_Weight_401_Staff As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Weight_401_Staff_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_401_Staff As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents DataColumn5 As System.Data.DataColumn
	Friend WithEvents DataColumn6 As System.Data.DataColumn
	Friend WithEvents colWeight As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents colWeighted_Score As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents tpg_207_Multiplayer_Attributes As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents pnl_207_Multiplayer_Attributes As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents pnl_207_Multiplayer_Attributes_Details As MKNetDXLib.ctl_MKDXPanel
	Friend WithEvents tlp_207_Multiplayer_Attributes As System.Windows.Forms.TableLayoutPanel
	Friend WithEvents gb_207_Multiplayer_Attributes_AB As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents grd_207_Multiplayer_Attributes_AB As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_207_Multiplayer_Attributes_AB As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_207_Multiplayer_Attributes_AB As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents gb_207_Multiplayer_Attributes_B As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents grd_207_Multiplayer_Attributes_B As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_207_Multiplayer_Attributes_B As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_207_Multiplayer_Attributes_B As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents gb_207_Multiplayer_Attributes_A As MKNetDXLib.ctl_MKDXGroupBox
	Friend WithEvents grd_207_Multiplayer_Attributes_A As MKNetDXLib.ctl_MKDXGrid
	Friend WithEvents gv_207_Multiplayer_Attributes_A As DevExpress.XtraGrid.Views.Grid.GridView
	Friend WithEvents col_207_Multiplayer_Attributes_A As DevExpress.XtraGrid.Columns.GridColumn
	Friend WithEvents lbl_Weight_207_Multiplayer_Attributes As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_Weight_207_Multiplayer_Attributes_Text As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents lbl_207_Multiplayer_Attributes_Explanation As MKNetDXLib.ctl_MKDXLabel
	Friend WithEvents tpg_107_Visual_Presentation As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_108_Gameplay As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_109_Pacing As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_110_Narrative_Theme_Topic As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_111_Setting As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_112_Vehicular_Themes As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_113_Interface_Control As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_114_DLC_Addon As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents tpg_115_Special_Edition As DevExpress.XtraTab.XtraTabPage
	Friend WithEvents ucr_107_Visual_Presentation As ucr_Similarity_Calculation_Details_Genre
	Friend WithEvents ucr_108_Gameplay As ucr_Similarity_Calculation_Details_Genre
	Friend WithEvents ucr_109_Pacing As ucr_Similarity_Calculation_Details_Genre
	Friend WithEvents ucr_110_Narrative_Theme_Topic As ucr_Similarity_Calculation_Details_Genre
	Friend WithEvents ucr_111_Setting As ucr_Similarity_Calculation_Details_Genre
	Friend WithEvents ucr_112_Vehicular_Themes As ucr_Similarity_Calculation_Details_Genre
	Friend WithEvents ucr_113_Interface_Control As ucr_Similarity_Calculation_Details_Genre
	Friend WithEvents ucr_114_DLC_Addon As ucr_Similarity_Calculation_Details_Genre
	Friend WithEvents ucr_115_Special_Edition As ucr_Similarity_Calculation_Details_Genre
	Friend WithEvents ucr_101_Basic_Genres As ucr_Similarity_Calculation_Details_Genre
	Friend WithEvents ucr_102_Perspectives As ucr_Similarity_Calculation_Details_Genre
	Friend WithEvents ucr_103_Sports_Themes As ucr_Similarity_Calculation_Details_Genre
	Friend WithEvents ucr_105_Educational_Categories As ucr_Similarity_Calculation_Details_Genre
	Friend WithEvents ucr_106_Other_Attributes As ucr_Similarity_Calculation_Details_Genre
End Class
