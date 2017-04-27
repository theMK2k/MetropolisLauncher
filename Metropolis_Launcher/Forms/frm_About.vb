Public Class frm_About

	Public Sub New()
		InitializeComponent()

		Dim sVersion As String = Application.ProductVersion.Split(".")(0) & "." & Application.ProductVersion.Split(".")(1) & "." & Application.ProductVersion.Split(".")(2)
		lbl_BuildInfo.Text = sVersion & " " & "build " & Alphaleonis.Win32.Filesystem.File.GetLastWriteTime(System.Reflection.Assembly.GetEntryAssembly().Location).ToString("yyyyMMdd-HHmmss")
	End Sub

	Private Sub OpenLink(ByVal URL As String)
		Process.Start(URL)
	End Sub

	Private Sub btn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Close.Click
		Me.Close()
	End Sub

	Private Sub Handle_Link_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbl_MobyGames_Link.Click, lbl_EmulationEvolved_Link.Click, lbl_TOSEC_Link.Click, lbl_TDC_Link.Click, lbl_PDRoms_Link.Click, lbl_NoIntro_Link.Click, lbl_MetropolisLauncher_Link.Click, lbl_Archive_Link.Click, lbl_RetroArch_Link.Click, lbl_RetroAchievements_Link.Click, lbl_Bottom_Fugue_Link.Click, lbl_Bottom_SpaceInvaders_Link.Click
		Select Case CType(sender, Control).Name
			Case lbl_MobyGames_Link.Name
				OpenLink("http://www.mobygames.com")
			Case lbl_Archive_Link.Name
				OpenLink("http://www.archive.org")
			Case lbl_MetropolisLauncher_Link.Name
				OpenLink("http://www.metropolis-launcher.net")
			Case lbl_EmulationEvolved_Link.Name
				OpenLink("http://www.emulation-evolved.net")
			Case lbl_NoIntro_Link.Name
				OpenLink("http://www.no-intro.org")
			Case lbl_PDRoms_Link.Name
				OpenLink("http://www.pdroms.de")
			Case lbl_TDC_Link.Name
				OpenLink("http://www.totaldoscollection.org")
			Case lbl_TOSEC_Link.Name
				OpenLink("http://www.tosec.org")
			Case lbl_RetroAchievements_Link.Name
				OpenLink("http://www.retroachievements.org")
			Case lbl_RetroArch_Link.Name
				OpenLink("http://www.libretro.com")
			Case lbl_Bottom_Fugue_Link.Name
				OpenLink("http://p.yusukekamiyamane.com")
			Case lbl_Bottom_SpaceInvaders_Link.Name
				OpenLink("http://moglenstar.deviantart.com/")
		End Select
	End Sub
End Class