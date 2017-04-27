Public Class frm_Emu_Game_Edit_GenreDescription
	Public Sub New(ByVal id_Moby_Genres As Integer)
		InitializeComponent()

		Cursor.Current = Cursors.WaitCursor

		lbl_Genre.Text = DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Name FROM moby.tbl_Moby_Genres WHERE id_Moby_Genres = " & TC.getSQLFormat(id_Moby_Genres))
		txb_Description.Text = DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Description FROM moby.tbl_Moby_Genres WHERE id_Moby_Genres = " & TC.getSQLFormat(id_Moby_Genres))
		txb_Description.DeselectAll()

		Cursor.Current = Cursors.Default
	End Sub
End Class
