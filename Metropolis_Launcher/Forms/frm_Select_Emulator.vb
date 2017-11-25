Public Class frm_Select_Emulator

	Public ReadOnly Property Selected_Emulator_Row As DS_ML.tbl_EmulatorsRow
		Get
			If BS_Emulators.Current Is Nothing Then
				Return Nothing
			End If

			Return BS_Emulators.Current.Row
		End Get
	End Property


	Public Sub New()
		InitializeComponent()

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction()
			Me.DS_ML.Fill_src_frm_Emulators(tran, Me.DS_ML.tbl_Emulators)
		End Using
	End Sub

	Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
		If BS_Emulators.Current Is Nothing Then
			Return
		End If

		Me.DialogResult = DialogResult.OK
	End Sub
End Class