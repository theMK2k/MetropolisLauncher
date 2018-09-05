Imports DevExpress.XtraEditors.Controls

Public Class frm_Cheevo_Challenges_Edit
	Public Sub New(Optional ByVal id_Cheevo_Challenges As Int64 = 0L)
		InitializeComponent()

		If cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin Then
			rpiHardcore.ReadOnly = True
			rpiTier.ReadOnly = True
			bbi_Cheevo_Move.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
			bbi_Cheevo_Remove.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
			btn_Save.Visible = False

			colHardcore.OptionsColumn.AllowEdit = False
			colHardcore.OptionsColumn.ReadOnly = True

			colTier.OptionsColumn.AllowEdit = False
			colTier.OptionsColumn.ReadOnly = True

			For Each btn As EditorButton In Me.cmb_Challenges.Properties.Buttons
				If btn.Kind <> ButtonPredefines.Combo Then
					btn.Visible = False
				End If
			Next
		End If

		barmng.SetPopupContextMenu(grd_Cheevos, popmnu_Cheevos)

		DS_ML.Fill_tbl_Cheevo_Challenges(Me.DS_ML.tbl_Cheevo_Challenges)

		If BS_Cheevo_Challenges.Current IsNot Nothing Then
			cmb_Challenges.EditValue = BS_Cheevo_Challenges.Current("id_Cheevo_Challenges")
		End If

		If id_Cheevo_Challenges > 0L Then
			Me.cmb_Challenges.EditValue = id_Cheevo_Challenges
			MKNetLib.cls_MKClientSupport.SetBindingSourcePosition(BS_Cheevo_Challenges, "id_Cheevo_Challenges", id_Cheevo_Challenges)
		End If
	End Sub

	Private Sub BS_Cheevo_Challenges_CurrentChanged(sender As Object, e As EventArgs) Handles BS_Cheevo_Challenges.CurrentChanged
		If BS_Cheevo_Challenges.Current Is Nothing Then
			Me.DS_ML.tbl_Cheevo_Challenges_Cheevos.Clear()
			Me.BS_Cheevo_Challenges_Cheevos.Filter = ""
			Return
		End If

		DS_ML.Fill_tbl_Cheevo_Challenges_Cheevos(Me.DS_ML.tbl_Cheevo_Challenges_Cheevos, BS_Cheevo_Challenges.Current("id_Cheevo_Challenges"))
		Me.BS_Cheevo_Challenges_Cheevos.Filter = "id_Cheevo_Challenges = " & TC.getSQLFormat(BS_Cheevo_Challenges.Current("id_Cheevo_Challenges"))
	End Sub

	Private Sub btn_Close_Click(sender As Object, e As EventArgs) Handles btn_Close.Click
		Me.Close()
	End Sub

	Private Function hasChanges() As Boolean
		BS_Cheevo_Challenges_Cheevos.EndEdit()

		Dim dt_Changes As DataTable = DS_ML.tbl_Cheevo_Challenges_Cheevos.GetChanges
		If dt_Changes IsNot Nothing AndAlso dt_Changes.Rows.Count > 0 Then
			Return True
		End If

		Return False
	End Function

	Private Function Save() As Boolean
		Try
			Dim ar_rows_Delete As New ArrayList

			For Each row_Change As DS_ML.tbl_Cheevo_Challenges_CheevosRow In Me.DS_ML.tbl_Cheevo_Challenges_Cheevos.Rows
				If row_Change.RowState = DataRowState.Deleted Then
					ar_rows_Delete.Add(row_Change)
				Else
					'UPDATE
					Dim sSQL As String = ""
					sSQL &= "UPDATE tbl_Cheevo_Challenges_Cheevos SET" & ControlChars.CrLf
					sSQL &= "	Hardcore = " & TC.getSQLFormat(row_Change("Hardcore")) & ControlChars.CrLf
					sSQL &= "	, Tier = " & TC.getSQLFormat(row_Change("Tier")) & ControlChars.CrLf
					sSQL &= "	, id_Cheevo_Challenges = " & TC.getSQLFormat(row_Change("id_Cheevo_Challenges")) & ControlChars.CrLf
					sSQL &= "WHERE id_Cheevo_Challenges_Cheevos = " & TC.getSQLFormat(row_Change("id_Cheevo_Challenges_Cheevos", DataRowVersion.Original))

					DataAccess.FireProcedure(cls_Globals.Conn, 0, sSQL)
					row_Change.AcceptChanges()
				End If
			Next

			For Each row_Delete As DS_ML.tbl_Cheevo_Challenges_CheevosRow In ar_rows_Delete
				'DELETE
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Cheevo_Challenges_Cheevos WHERE id_Cheevo_Challenges_Cheevos = " & TC.getSQLFormat(row_Delete("id_Cheevo_Challenges_Cheevos", DataRowVersion.Original)))
				row_Delete.AcceptChanges()
			Next
		Catch ex As Exception
			MKDXHelper.ExceptionMessageBox(ex, "Error while saving")
			Return False
		End Try

		Return True
	End Function

	Private Function AskSave(Optional ByVal message As String = "Do you want to save your changes?", Optional ByVal buttons As MessageBoxButtons = MessageBoxButtons.YesNoCancel) As Boolean
		If hasChanges() Then
			Dim res As DialogResult = MKNetDXLib.cls_MKDXHelper.MessageBox(message, "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

			If res = DialogResult.Cancel Then
				Return False
			End If

			If res = DialogResult.Yes Then
				Return Me.Save()
			End If

			If res = DialogResult.No Then
				Return True
			End If
		End If

		Return True
	End Function

	Private Sub frm_Cheevo_Challenges_Edit_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
		If Not AskSave() Then
			e.Cancel = True
			Return
		End If
	End Sub

	Private Sub bbi_Cheevo_Remove_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Cheevo_Remove.ItemClick
		If BS_Cheevo_Challenges_Cheevos.Current Is Nothing Then
			Return
		End If

		Me.BS_Cheevo_Challenges_Cheevos.RemoveCurrent()
	End Sub

	Private Sub bbi_Cheevo_Move_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Cheevo_Move.ItemClick
		Using frm As New frm_Cheevo_Challenges_Add(BS_Cheevo_Challenges_Cheevos.Current("CheevoType"), BS_Cheevo_Challenges_Cheevos.Current("id_Emu_Games"), BS_Cheevo_Challenges_Cheevos.Current("Cheevo_GameName"), BS_Cheevo_Challenges_Cheevos.Current.Row, True)
			If frm.ShowDialog() = DialogResult.OK Then
				BS_Cheevo_Challenges_Cheevos.Current("id_Cheevo_Challenges") = frm.cmb_Challenges.EditValue
				BS_Cheevo_Challenges_Cheevos.Current("Tier") = frm.cmb_Tier.EditValue

				Me.Save()

				Dim selectedChallenge As Long = Me.BS_Cheevo_Challenges.Current("id_Cheevo_Challenges")

				DS_ML.Fill_tbl_Cheevo_Challenges(Me.DS_ML.tbl_Cheevo_Challenges)

				MKNetLib.cls_MKClientSupport.SetBindingSourcePosition(Me.BS_Cheevo_Challenges, "id_Cheevo_Challenges", selectedChallenge)

				MKNetDXLib.cls_MKDXHelper.MessageBox("The achievement has been moved.", "Move Achievement to Challenge", MessageBoxButtons.OK, MessageBoxIcon.Information)

				Me.grd_Cheevos.DataSource = Nothing
				Me.grd_Cheevos.DataSource = BS_Cheevo_Challenges_Cheevos

			End If
		End Using
	End Sub

	Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_Save.Click
		Me.Save()
	End Sub

	Private Sub cmb_Challenges_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles cmb_Challenges.EditValueChanging
		If Not Me.AskSave Then
			e.Cancel = True
			Return
		End If
	End Sub

	Private Sub cmb_Challenges_ButtonPressed(sender As Object, e As ButtonPressedEventArgs) Handles cmb_Challenges.ButtonPressed
		If e.Button.Kind = ButtonPredefines.Plus Then
			'Add new Challenge
			If Not Me.AskSave Then
				Return
			End If

			Using frm As New MKNetDXLib.frm_TextBoxEdit("Name:", "Please provide a name for the new challenge", "")
				If frm.ShowDialog <> DialogResult.Cancel Then
					DataAccess.FireProcedure(cls_Globals.Conn, 0, "INSERT INTO tbl_Cheevo_Challenges (Name) VALUES (" & TC.getSQLFormat(frm.Input) & ")")
					DS_ML.Fill_tbl_Cheevo_Challenges(Me.DS_ML.tbl_Cheevo_Challenges)

					MKNetDXLib.cls_MKDXHelper.MessageBox("The challenge has been added.", "Add Challenge", MessageBoxButtons.OK, MessageBoxIcon.Information)
				End If
			End Using
		End If

		If e.Button.Kind = ButtonPredefines.Minus Then
			'Delete Challenge
			'-> check if this challenge is bound to a user, display question dialog
			Dim sUsers As String = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT GROUP_CONCAT(Username, ', ') AS Users FROM tbl_Users WHERE id_Cheevo_Challenges = " & TC.getSQLFormat(cmb_Challenges.EditValue) & " GROUP BY id_Cheevo_Challenges"), "")

			If sUsers <> "" Then
				If MKNetDXLib.cls_MKDXHelper.MessageBox("The selected challenge is currently bound to the following user/s:" & ControlChars.CrLf & ControlChars.CrLf & sUsers & ControlChars.CrLf & ControlChars.CrLf & "Do you want to delete this challenge and remove the challenge binding from the users?", "Delete Challenge", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> DialogResult.Yes Then
					Return
				End If
			Else
				If MKNetDXLib.cls_MKDXHelper.MessageBox("Do you really want to delete the selected challenge?", "Delete Challenge", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> DialogResult.Yes Then
					Return
				End If
			End If

			DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Cheevo_Challenges WHERE id_Cheevo_Challenges = " & TC.getSQLFormat(cmb_Challenges.EditValue))
			DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Cheevo_Challenges_Cheevos WHERE id_Cheevo_Challenges NOT IN (SELECT id_Cheevo_Challenges FROM tbl_Cheevo_Challenges)")
			DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Users_Cheevo_Challenges_Cheevos WHERE id_Cheevo_Challenges_Cheevos NOT IN (SELECT id_Cheevo_Challenges_Cheevos FROM tbl_Cheevo_Challenges_Cheevos)")
			DataAccess.FireProcedure(cls_Globals.Conn, 0, "UPDATE tbl_Users SET id_Cheevo_Challenges = NULL WHERE id_Cheevo_Challenges NOT IN (SELECT id_Cheevo_Challenges FROM tbl_Cheevo_Challenges)")

			DS_ML.Fill_tbl_Cheevo_Challenges(Me.DS_ML.tbl_Cheevo_Challenges)
			If BS_Cheevo_Challenges.Current IsNot Nothing Then
				Me.cmb_Challenges.EditValue = Me.BS_Cheevo_Challenges.Current("id_Cheevo_Challenges")
			Else
				Me.cmb_Challenges.EditValue = Nothing
			End If

			MKNetDXLib.cls_MKDXHelper.MessageBox("The challenge has been deleted.", "Delete Challenge", MessageBoxButtons.OK, MessageBoxIcon.Information)
		End If
	End Sub

	Private Sub rpiHardcore_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles rpiHardcore.EditValueChanging
		If BS_Cheevo_Challenges_Cheevos.Current("CheevoType") = cls_Globals.enm_CheevoTypes.TotalRuntime Then
			MKDXHelper.MessageBox("Runtime based achievements have no distinction between hardcore and casual.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
			e.Cancel = True
			Return
		End If
	End Sub
End Class