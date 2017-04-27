Public Class frm_Emu_Game_Rating_Weights_Edit
	Public Sub New()
		InitializeComponent()

		Cursor.Current = Cursors.WaitCursor

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Fill_tbl_Emu_Games_Rating_Weights(tran, DS_ML.tbl_Emu_Games_Rating_Weights)
		End Using

		Cursor.Current = Cursors.Default
	End Sub

	Private Sub btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OK.Click
		Cursor.Current = Cursors.WaitCursor

		BS_Rating_Weights.EndEdit()
		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			For Each row As DataRow In DS_ML.tbl_Emu_Games_Rating_Weights.Rows
				DS_ML.Update_tbl_Emu_Games_Rating_Weights(tran, row)
			Next
			tran.Commit()
		End Using

		Cursor.Current = Cursors.Default

		Me.DialogResult = Windows.Forms.DialogResult.OK
		Me.Close()
	End Sub

	Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
		Me.Close()
	End Sub
End Class
