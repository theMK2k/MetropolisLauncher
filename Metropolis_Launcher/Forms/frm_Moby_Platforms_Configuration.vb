Public Class frm_Moby_Platforms_Configuration
	Public Sub New()
		InitializeComponent()

		Dim sSQL As String = ""
		sSQL &= "	SELECT" & ControlChars.CrLf
		sSQL &= "					PLTFM.id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "					, PLTFM.Display_Name AS Name" & ControlChars.CrLf
		sSQL &= "					, PLTFM.ShortName" & ControlChars.CrLf
		sSQL &= "					, IFNULL(PLTFMS.Visible, 1) AS Visible" & ControlChars.CrLf
		sSQL &= "	FROM moby.tbl_Moby_Platforms PLTFM" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN main.tbl_Moby_Platforms_Settings PLTFMS ON PLTFM.id_Moby_Platforms = PLTFMS.id_Moby_Platforms" & ControlChars.CrLf
		sSQL &= "	WHERE PLTFM.Visible = 1" & ControlChars.CrLf
		sSQL &= "				AND PLTFM.id_Moby_Platforms_Owner IS NULL"
		sSQL &= "	ORDER BY Display_Name"

		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, DS_ML.tbl_Moby_Platforms_Settings)

		If cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin Then
			Me.rpi_Checkedit.ReadOnly = True
			Me.colVisible.OptionsColumn.AllowEdit = False
			Me.btn_OK.Visible = False
			Me.btn_Cancel.Text = "&Close"
		End If
	End Sub

	Private Sub btn_OK_Click(sender As System.Object, e As System.EventArgs) Handles btn_OK.Click
		BS_Platform_Settings.EndEdit()
		For Each row As DataRow In Me.DS_ML.tbl_Moby_Platforms_Settings.Rows
			DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM main.tbl_Moby_Platforms_Settings WHERE id_Moby_Platforms = " & TC.getSQLFormat(row("id_Moby_Platforms")))
			DataAccess.FireProcedure(cls_Globals.Conn, 0, "INSERT INTO main.tbl_Moby_Platforms_Settings (id_Moby_Platforms, Visible) VALUES (" & TC.getSQLParameter(row("id_Moby_Platforms"), row("Visible")) & ")")
		Next

		Me.DialogResult = Windows.Forms.DialogResult.OK
		Me.Close()
	End Sub

	Private Sub btn_Cancel_Click(sender As System.Object, e As System.EventArgs) Handles btn_Cancel.Click
		Me.Close()
	End Sub

	Private Sub gv_Platforms_MouseMove(sender As Object, e As MouseEventArgs) Handles gv_Platforms.MouseMove
		If Not cls_Globals.MultiUserMode OrElse cls_Globals.Admin Then
			Me.grd_Platforms.ShowHandInColumns(gv_Platforms, {"Visible"}, e)
		End If
	End Sub
End Class