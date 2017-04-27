Public Class frm_Moby_Game_Group_Info

	Private _id_Moby_Game_Groups As Integer = 0

	Public Sub New(ByVal id_Moby_Game_Groups As Integer)
		Me.InitializeComponent()

		Me._id_Moby_Game_Groups = id_Moby_Game_Groups

		Dim sSQL As String = ""
		sSQL &= "	SELECT DISTINCT"
		sSQL &= "					MR.id_Moby_Games"
		sSQL &= "					, MG.Name"
		sSQL &= "					, (	SELECT group_concat(MP2.Name, ', ')"
		sSQL &= "							FROM moby.tbl_Moby_Game_Groups_Moby_Releases MGGMR2"
		sSQL &= "							INNER JOIN moby.tbl_Moby_Releases MR2 ON MGGMR2.id_Moby_Releases = MR2.id_Moby_Releases"
		sSQL &= "							INNER JOIN moby.tbl_Moby_Platforms MP2 ON MR2.id_Moby_Platforms = MP2.id_Moby_Platforms"
		sSQL &= "							WHERE MGGMR2.id_Moby_Game_Groups = MGGMR.id_Moby_Game_Groups AND MR2.id_Moby_Games = MR.id_Moby_Games"
		sSQL &= "							ORDER BY MP2.Name"
		sSQL &= "						) AS Platforms"
		sSQL &= "	FROM moby.tbl_Moby_Game_Groups_Moby_Releases MGGMR"
		sSQL &= "	INNER JOIN moby.tbl_Moby_Releases MR ON MGGMR.id_Moby_Releases = MR.id_Moby_Releases"
		sSQL &= "	INNER JOIN moby.tbl_Moby_Games MG ON MR.id_Moby_Games = MG.id_Moby_Games"
		sSQL &= "	WHERE MGGMR.id_Moby_Game_Groups = " & TC.getSQLFormat(id_Moby_Game_Groups)
		sSQL &= "	ORDER BY MG.Name"
		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, Me.DS_Moby_Games.Tables("src_frm_Moby_Game_Group_Info"))

		'Me.lbl_Description.Text = MKNetLib.cls_MKRegex.Replace(TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Description FROM moby.tbl_Moby_Game_Groups WHERE id_Moby_Game_Groups = " & TC.getSQLFormat(_id_Moby_Game_Groups)), "[no description]").Replace("<br>", ControlChars.CrLf), "<.*?>", "")
		Me.lbl_Description.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Description FROM moby.tbl_Moby_Game_Groups WHERE id_Moby_Game_Groups = " & TC.getSQLFormat(_id_Moby_Game_Groups)), "[no description]")
		Me.lbl_GoupName.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Name FROM moby.tbl_Moby_Game_Groups WHERE id_Moby_Game_Groups = " & TC.getSQLFormat(_id_Moby_Game_Groups)), "[no group name]")
	End Sub
End Class
