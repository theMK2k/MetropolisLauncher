Public Class frm_Moby_Staff_Info

	Private _id_Moby_Staff As Integer = 0

	Public Sub New(ByVal id_Moby_Staff As Integer)
		Me.InitializeComponent()

		Me._id_Moby_Staff = id_Moby_Staff

		Dim sSQL As String = ""
		sSQL &= "	SELECT DISTINCT" & ControlChars.CrLf
		sSQL &= "					MR.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "					, MG.Name" & ControlChars.CrLf
		sSQL &= "					, (	SELECT group_concat(Name, ', ')" & ControlChars.CrLf
		sSQL &= "							FROM" & ControlChars.CrLf
		sSQL &= "							(" & ControlChars.CrLf
		sSQL &= "								SELECT DISTINCT MP2.Name" & ControlChars.CrLf
		sSQL &= "								FROM moby.tbl_Moby_Releases_Staff MRS2" & ControlChars.CrLf
		sSQL &= "								INNER JOIN moby.tbl_Moby_Releases MR2 ON MRS2.id_Moby_Releases = MR2.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "								INNER JOIN moby.tbl_Moby_Platforms MP2 ON MR2.id_Moby_Platforms = MP2.id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "								WHERE MRS2.id_Moby_Staff = MRS.id_Moby_Staff AND MR2.id_Moby_Games = MR.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "								ORDER BY MP2.Name" & ControlChars.CrLf
		sSQL &= "             )" & ControlChars.CrLf
		sSQL &= "						) AS Platforms" & ControlChars.CrLf
		sSQL &= "					, MRS.Position" & ControlChars.CrLf
		sSQL &= "	FROM moby.tbl_Moby_Releases_Staff MRS" & ControlChars.CrLf
		sSQL &= "	INNER JOIN moby.tbl_Moby_Releases MR ON MRS.id_Moby_Releases = MR.id_Moby_Releases" & ControlChars.CrLf
		sSQL &= "	INNER JOIN moby.tbl_Moby_Games MG ON MR.id_Moby_Games = MG.id_Moby_Games" & ControlChars.CrLf
		sSQL &= "	WHERE MRS.id_Moby_Staff = " & TC.getSQLFormat(id_Moby_Staff) & ControlChars.CrLf
		sSQL &= "	ORDER BY MG.Name"
		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, Me.DS_Moby_Games.Tables("src_frm_Moby_Game_Group_Info"))

		'Me.lbl_Bio.Text = MKNetLib.cls_MKRegex.Replace(TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Biography FROM moby.tbl_Moby_Staff WHERE id_Moby_Staff = " & TC.getSQLFormat(_id_Moby_Staff)), "").Replace("<br>", ControlChars.CrLf), "<.*?>", "")
		Me.lbl_Bio.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Biography FROM moby.tbl_Moby_Staff WHERE id_Moby_Staff = " & TC.getSQLFormat(_id_Moby_Staff)), "")
		Me.lbl_Staff_Name.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Name FROM moby.tbl_Moby_Staff WHERE id_Moby_Staff = " & TC.getSQLFormat(_id_Moby_Staff)), "[no name]")
	End Sub
End Class