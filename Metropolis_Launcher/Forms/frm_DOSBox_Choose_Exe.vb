Public Class frm_DOSBox_Choose_Exe
	Public _row_Emu_Game As DS_ML.src_ucr_Emulation_GamesRow = Nothing

	Public Sub New(ByVal Exe_Type As String, ByVal Filter As String, ByVal dt As DS_ML.tbl_Emu_GamesDataTable, ByRef row_Emu_Game As DS_ML.src_ucr_Emulation_GamesRow)
		InitializeComponent()

		Me._row_Emu_Game = row_Emu_Game

		Me.Text = Exe_Type.ToUpper & " executable"
		Me.lbl_Explanation.Text = "Please select a file for autostart as the " & Exe_Type & " executable in the list below and press OK. If you choose 'Just mount', DOSBox will start but won't autostart an executable."
		Me.BS_DOSBox_Files_and_Folders.DataSource = dt
		Me.BS_DOSBox_Files_and_Folders.Filter = Filter
	End Sub

	Private Sub btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OK.Click
		Me.DialogResult = Windows.Forms.DialogResult.OK
		Me.Close()
	End Sub

	Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Just_Mount.Click
		Me.DialogResult = Windows.Forms.DialogResult.Ignore
		Me.Close()
	End Sub

	Private Sub gv_DOSBox_Files_and_Folders_CustomColumnDisplayText(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles gv_DOSBox_Files_and_Folders.CustomColumnDisplayText
		If e.Column Is col_DOSBox_Displayname Then
			'Dim row As DataRow = gv_DOSBox_Files_and_Folders.GetRow(e.ListSourceRowIndex).Row

			'Dim oInnerFile As Object = gv_DOSBox_Files_and_Folders.GetListSourceRowCellValue(e.ListSourceRowIndex, "InnerFile")
			'Dim oFolder As Object = gv_DOSBox_Files_and_Folders.GetListSourceRowCellValue(e.ListSourceRowIndex, "Folder")

			'If TC.NZ(oInnerFile, "").Length > 0 Then
			'	e.DisplayText = oInnerFile
			'Else
			'	e.DisplayText = oFolder
			'End If

			e.DisplayText = TC.NZ(gv_DOSBox_Files_and_Folders.GetListSourceRowCellValue(e.ListSourceRowIndex, "tmp_DOSBox_DisplayText"), "")
		End If
	End Sub

	Private Sub gv_DOSBox_Files_and_Folders_DoubleClick(sender As Object, e As System.EventArgs) Handles gv_DOSBox_Files_and_Folders.DoubleClick
		Me.DialogResult = Windows.Forms.DialogResult.OK
		Me.Close()
	End Sub

	Private Sub gv_DOSBox_Files_and_Folders_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_DOSBox_Files_and_Folders.KeyDown
		If e.KeyCode = Keys.Enter Then
			Me.DialogResult = Windows.Forms.DialogResult.OK
			Me.Close()
		End If
		If e.KeyCode = Keys.Escape Then
			Me.DialogResult = Windows.Forms.DialogResult.Cancel
			Me.Close()
		End If
	End Sub

	Private Sub btn_Create_TDL_Menu_Click(sender As Object, e As EventArgs) Handles btn_Create_TDL_Menu.Click
		Using frm_TDL_Create_Menu As New frm_TDL_Create_Menu(Me._row_Emu_Game)
			If frm_TDL_Create_Menu.ShowDialog() = DialogResult.OK Then
				'reload from DB
				Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
					dt_Files = New DS_ML.tbl_Emu_GamesDataTable
					DS_ML.Fill_src_frm_Rom_Manager_Emu_Games(tran, dt_Files, _row_Emu_Game("id_Moby_Platforms"), _row_Emu_Game("id_Emu_Games"), _row_Emu_Game("id_Emu_Games"))

					'Format the DOSBox_DisplayText of the file entries
					DS_ML.Prepare_tmp_DOSBox_DisplayText(dt_Files)

					Me.BS_DOSBox_Files_and_Folders.DataSource = dt_Files

				End Using
			End If
		End Using
	End Sub
End Class
