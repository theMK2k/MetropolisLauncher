Public Class frm_Search_Missing_Extras
	Public Sub New()
		InitializeComponent()

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Fill_tbl_Emu_Extras(tran, Me.DS_ML.tbl_Emu_Extras, True)
		End Using

		cmb_Extra_Type.EditValue = 0
	End Sub
End Class