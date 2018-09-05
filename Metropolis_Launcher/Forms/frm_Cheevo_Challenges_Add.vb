Public Class frm_Cheevo_Challenges_Add
	Private _CheevoType As cls_Globals.enm_CheevoTypes
	Private _row_Cheevo As DataRow = Nothing
	Private _id_Emu_Games As Int64 = 0
	Private _GameName As String = ""
	Private _JustChooseDestination As Boolean = False

	Public Sub New(CheevoType As cls_Globals.enm_CheevoTypes, id_Emu_Games As Int64, GameName As String, Optional ByRef row_Cheevo As DataRow = Nothing, Optional ByVal JustChooseDestination As Boolean = False)
		InitializeComponent()

		_CheevoType = CheevoType

		If _CheevoType = cls_Globals.enm_CheevoTypes.RetroAchievements Then
			Me.Name = "frm_Cheevo_Challenges_Add_RetroAchievements"

			If MKNetLib.cls_MKSQLDataAccess.HasColumn(row_Cheevo, "Title") Then
				Me.lbl_AchievementDisplayText.Text = GameName.Replace("&", "&&") & ": " & row_Cheevo("Title").Replace("&", "&&")
			Else
				Me.lbl_AchievementDisplayText.Text = GameName.Replace("&", "&&") & ": " & row_Cheevo("Cheevo_Title").Replace("&", "&&")
			End If

			Me.lbl_Hardcore.Visible = True
			Me.chb_Hardcore.Visible = True
			Me.lbl_Total_Runtime.Visible = False
			Me.spn_Hours.Visible = False
			Me.lbl_Hours.Visible = False
			Me.spn_Minutes.Visible = False
			Me.lbl_Minutes.Visible = False
		ElseIf _CheevoType = cls_Globals.enm_CheevoTypes.TotalRuntime Then
			Me.Name = "frm_Cheevo_Challenges_Add_Runtime"

			Me.lbl_AchievementDisplayText.Text = GameName.Replace("&", "&&") & " (runtime based achievement)"

			Me.lbl_Hardcore.Visible = False
			Me.chb_Hardcore.Visible = False
			Me.lbl_Total_Runtime.Visible = True
			Me.spn_Hours.Visible = True
			Me.lbl_Hours.Visible = True
			Me.spn_Minutes.Visible = True
			Me.lbl_Minutes.Visible = True

			If row_Cheevo IsNot Nothing Then
				Dim Runtime As Integer = TC.NZ(row_Cheevo("Runtime"), 0)

				Dim RuntimeMinutes As Integer = Runtime / 60

				spn_Minutes.Value = RuntimeMinutes Mod 60
				spn_Hours.Value = (RuntimeMinutes - (RuntimeMinutes Mod 60)) / 60
			End If
		End If

		Me._JustChooseDestination = JustChooseDestination

		If JustChooseDestination Then
			Me.Name = "frm_Cheevo_Challenges_Add_Move"

			Me.lbl_AchievementDisplayText.Text = "Move " & Me.lbl_AchievementDisplayText.Text & " to another challenge"

			Me.Text = "Move Achievement to Challenge"
			Me.lbl_Hardcore.Visible = False
			Me.chb_Hardcore.Visible = False
			Me.lbl_Total_Runtime.Visible = False
			'Me.spn_Hours.Visible = False
			'Me.lbl_Hours.Visible = False
			'Me.spn_Minutes.Visible = False
			'Me.lbl_Minutes.Visible = False
			Me.spn_Minutes.ReadOnly = True
			Me.spn_Hours.ReadOnly = True

			'For Each button As DevExpress.XtraEditors.Controls.EditorButton In Me.cmb_Challenges.Properties.Buttons
			'	If button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
			'		button.Visible = False
			'	End If
			'Next

		Else
			Me.lbl_AchievementDisplayText.Text = "Add " & Me.lbl_AchievementDisplayText.Text & " to a challenge"
		End If

		Me._row_Cheevo = row_Cheevo
		Me._id_Emu_Games = id_Emu_Games
		Me._GameName = GameName

		DS_ML.Fill_tbl_Cheevo_Challenges(Me.DS_ML.tbl_Cheevo_Challenges)

		If BS_Cheevo_Challenges.Current IsNot Nothing Then
			cmb_Challenges.EditValue = BS_Cheevo_Challenges.Current("id_Cheevo_Challenges")
		End If

		If BS_Tiers.Current IsNot Nothing Then
			cmb_Tier.EditValue = BS_Tiers.Current("Tier")
		End If

		If BS_Cheevo_Challenges.Current IsNot Nothing AndAlso Me.DS_ML.ttb_Cheevo_Challenges_Tiers.Rows.Count = 0 Then
			'Perform an "add" click on the Tiers-Combobox
			Me.cmb_Tier_ButtonPressed(Nothing, New DevExpress.XtraEditors.Controls.ButtonPressedEventArgs(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)))
		End If

		Update_Buttons_Enabled()
	End Sub

	Private Sub BS_Cheevo_Challenges_CurrentChanged(sender As Object, e As EventArgs) Handles BS_Cheevo_Challenges.CurrentChanged
		If BS_Cheevo_Challenges.Current Is Nothing Then
			Me.DS_ML.ttb_Cheevo_Challenges_Tiers.Clear()
			Return
		End If

		DS_ML.Fill_ttb_Cheevo_Challenges_Tiers(Me.DS_ML.ttb_Cheevo_Challenges_Tiers, BS_Cheevo_Challenges.Current("id_Cheevo_Challenges"))

		DS_ML.Fill_tbl_Cheevo_Challenges_Cheevos(Me.DS_ML.tbl_Cheevo_Challenges_Cheevos, BS_Cheevo_Challenges.Current("id_Cheevo_Challenges"))

		Update_Buttons_Enabled()
	End Sub

	Private Sub BS_Tiers_CurrentChanged(sender As Object, e As EventArgs) Handles BS_Tiers.CurrentChanged
		If BS_Tiers.Current Is Nothing Then
			Me.BS_Cheevos_in_Tier.Filter = "Tier = 0"
			Return
		End If

		Me.BS_Cheevos_in_Tier.Filter = "Tier = " & BS_Tiers.Current("Tier")

		Update_Buttons_Enabled()
	End Sub

	Private Sub Update_Buttons_Enabled()
		'### OK Button ###
		If BS_Cheevo_Challenges.Current IsNot Nothing Then
			'### Can't add Label ###
			Dim canAdd As Boolean = True

			If BS_Tiers.Current Is Nothing Then
				canAdd = False
				lbl_CantAdd.Text = "Note: Please select or add a tier."
			End If

			'Check if to-be-added achievement is already part of the selected challenge's achievements

			If canAdd AndAlso _CheevoType = cls_Globals.enm_CheevoTypes.RetroAchievements Then
				For Each row_CCC As DS_ML.tbl_Cheevo_Challenges_CheevosRow In Me.DS_ML.tbl_Cheevo_Challenges_Cheevos.Rows
					If _JustChooseDestination Then
						If TC.NZ(row_CCC("Cheevo_ID"), 0) = Me._row_Cheevo("Cheevo_ID") Then
							canAdd = False
							lbl_CantAdd.Text = "Note: The achievement is already part of the selected challenge!"
						End If
					Else
						If TC.NZ(row_CCC("Cheevo_ID"), 0) = Me._row_Cheevo("ID") Then
							canAdd = False
							lbl_CantAdd.Text = "Note: The achievement is already part of the selected challenge!"
						End If
					End If
				Next
			End If

			If canAdd AndAlso _CheevoType = cls_Globals.enm_CheevoTypes.TotalRuntime Then
				'check if game is already there
				For Each row_CCC As DS_ML.tbl_Cheevo_Challenges_CheevosRow In Me.DS_ML.tbl_Cheevo_Challenges_Cheevos.Rows
					If row_CCC.id_Emu_Games = Me._id_Emu_Games Then
						canAdd = False
						lbl_CantAdd.Text = "Note: The game is already part of the selected challenge!"
					End If
				Next

				If spn_Hours.Value = 0 AndAlso spn_Minutes.Value = 0 Then
					canAdd = False
					lbl_CantAdd.Text = "Note: Please set a total runtime of at least 1 minute!"
				End If
			End If

			If Not canAdd Then
				Me.lbl_CantAdd.Visible = True
			Else
				Me.lbl_CantAdd.Visible = False
			End If

			Me.btn_OK.Enabled = canAdd
		Else
			Me.btn_OK.Enabled = False
		End If

		'### Add Tier Button ###
		Dim allowAddTierButtonEnabled = False

		If BS_Cheevo_Challenges.Current IsNot Nothing Then
			Dim maxTier As Int64 = 0
			For Each rowTier As DS_ML.ttb_Cheevo_Challenges_TiersRow In Me.DS_ML.ttb_Cheevo_Challenges_Tiers.Rows
				If maxTier < rowTier.Tier Then
					maxTier = rowTier.Tier
				End If
			Next

			If maxTier = 0 Then
				allowAddTierButtonEnabled = True
			Else
				If Me.DS_ML.tbl_Cheevo_Challenges_Cheevos.Select("Tier = " & maxTier).Length > 0 Then
					allowAddTierButtonEnabled = True
				End If
			End If
		End If

		For Each button As DevExpress.XtraEditors.Controls.EditorButton In Me.cmb_Tier.Properties.Buttons
			If button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
				button.Enabled = allowAddTierButtonEnabled
			End If
		Next
	End Sub

	Private Sub cmb_Challenges_ButtonPressed(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_Challenges.ButtonPressed
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
			Using frm As New MKNetDXLib.frm_TextBoxEdit("Name:", "Please provide a name for the new challenge", "")
				If frm.ShowDialog <> DialogResult.Cancel Then
					Dim row_Challenge As DS_ML.tbl_Cheevo_ChallengesRow = Me.DS_ML.tbl_Cheevo_Challenges.NewRow
					row_Challenge.Name = frm.Input
					Me.DS_ML.tbl_Cheevo_Challenges.Rows.Add(row_Challenge)
					Me.cmb_Challenges.EditValue = row_Challenge("id_Cheevo_Challenges")

					'Also Perform an "add" click on the Tiers-Combobox
					Me.cmb_Tier_ButtonPressed(Nothing, New DevExpress.XtraEditors.Controls.ButtonPressedEventArgs(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)))
				End If
			End Using
		End If
	End Sub

	Private Sub cmb_Tier_ButtonPressed(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_Tier.ButtonPressed
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
			Dim minTier = 0
			For Each row_Tier As DS_ML.ttb_Cheevo_Challenges_TiersRow In Me.DS_ML.ttb_Cheevo_Challenges_Tiers.Rows
				If minTier < row_Tier.Tier Then
					minTier = row_Tier.Tier
				End If
			Next

			Dim row_newTier As DS_ML.ttb_Cheevo_Challenges_TiersRow = Me.DS_ML.ttb_Cheevo_Challenges_Tiers.NewRow
			row_newTier.Tier = minTier + 1
			Me.DS_ML.ttb_Cheevo_Challenges_Tiers.Rows.Add(row_newTier)
			Me.cmb_Tier.EditValue = row_newTier("Tier")

			Update_Buttons_Enabled()
		End If
	End Sub

	Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
		If BS_Cheevo_Challenges.Current Is Nothing OrElse BS_Tiers.Current Is Nothing Then
			Return
		End If

		Dim id_Cheevo_Challenges As Int64 = TC.NZ(BS_Cheevo_Challenges.Current("id_Cheevo_Challenges"), -1L)

		If id_Cheevo_Challenges < 0 Then
			Dim sSQLInserChallenge = "INSERT INTO tbl_Cheevo_Challenges (Name) VALUES (" & TC.getSQLFormat(BS_Cheevo_Challenges.Current("Name")) & "); SELECT last_insert_rowid()"
			id_Cheevo_Challenges = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, sSQLInserChallenge), 0)
		End If

		If id_Cheevo_Challenges < 0 Then
			MKDXHelper.MessageBox("There has been an error while creating the challenge.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Return
		End If

		If Me._JustChooseDestination Then
			BS_Cheevo_Challenges.Current("id_Cheevo_Challenges") = id_Cheevo_Challenges
			Me.cmb_Challenges.EditValue = id_Cheevo_Challenges
			Me.DialogResult = DialogResult.OK
			Me.Close()
			Return
		End If

		Dim sSQL = ""

		If Me._CheevoType = cls_Globals.enm_CheevoTypes.RetroAchievements Then
			sSQL &= "INSERT INTO tbl_Cheevo_Challenges_Cheevos (" & ControlChars.CrLf
			sSQL &= "	id_Cheevo_Challenges" & ControlChars.CrLf
			sSQL &= "	, CheevoType" & ControlChars.CrLf
			sSQL &= "	, Tier" & ControlChars.CrLf
			sSQL &= "	, id_Emu_Games" & ControlChars.CrLf
			sSQL &= "	, Cheevo_GameName" & ControlChars.CrLf
			sSQL &= "	, Cheevo_ID" & ControlChars.CrLf
			sSQL &= "	, Cheevo_Title" & ControlChars.CrLf
			sSQL &= "	, Cheevo_Description" & ControlChars.CrLf
			sSQL &= "	, Cheevo_Points" & ControlChars.CrLf
			sSQL &= "	, Cheevo_BadgeName" & ControlChars.CrLf
			sSQL &= "	, Cheevo_Flags" & ControlChars.CrLf
			sSQL &= "	, Hardcore" & ControlChars.CrLf
			sSQL &= ") VALUES (" & ControlChars.CrLf
			sSQL &= TC.getSQLParameter(id_Cheevo_Challenges, Me._CheevoType, BS_Tiers.Current("Tier"), Me._id_Emu_Games, Me._GameName, Me._row_Cheevo("ID"), TC.NZ(Me._row_Cheevo("Title"), "<no name>"), Me._row_Cheevo("Description"), Me._row_Cheevo("Points"), Me._row_Cheevo("BadgeName"), Me._row_Cheevo("Flags"), Me.chb_Hardcore.Checked)
			sSQL &= ")"
		ElseIf Me._CheevoType = cls_Globals.enm_CheevoTypes.TotalRuntime Then
			sSQL &= "INSERT INTO tbl_Cheevo_Challenges_Cheevos (" & ControlChars.CrLf
			sSQL &= "	id_Cheevo_Challenges" & ControlChars.CrLf
			sSQL &= "	, CheevoType" & ControlChars.CrLf
			sSQL &= "	, Tier" & ControlChars.CrLf
			sSQL &= "	, id_Emu_Games" & ControlChars.CrLf
			sSQL &= "	, Cheevo_GameName" & ControlChars.CrLf
			sSQL &= "	, Cheevo_Title" & ControlChars.CrLf
			sSQL &= "	, Cheevo_Description" & ControlChars.CrLf
			sSQL &= "	, Runtime" & ControlChars.CrLf
			sSQL &= ") VALUES (" & ControlChars.CrLf

			Dim sTitle As String = "Total Runtime: " & (IIf(spn_Hours.Value > 0, spn_Hours.Value & " " & IIf(spn_Hours.Value > 1, "hours", "hour"), "") & " " & IIf(spn_Minutes.Value > 0, spn_Minutes.Value & " " & IIf(spn_Minutes.Value > 1, "minutes", "minute"), "")).Trim
			Dim sDescription As String = "Play the game until you reach a total runtime of " & (IIf(spn_Hours.Value > 0, spn_Hours.Value & " " & IIf(spn_Hours.Value > 1, "hours", "hour"), "") & " " & IIf(spn_Minutes.Value > 0, spn_Minutes.Value & " " & IIf(spn_Minutes.Value > 1, "minutes", "minute"), "")).Trim

			sSQL &= TC.getSQLParameter(id_Cheevo_Challenges, Me._CheevoType, BS_Tiers.Current("Tier"), Me._id_Emu_Games, Me._GameName, sTitle, sDescription, (spn_Hours.Value * 60 + spn_Minutes.Value) * 60)
			sSQL &= ")"
		End If


		DataAccess.FireProcedure(cls_Globals.Conn, 0, sSQL)

		Me.DialogResult = DialogResult.OK
		Me.Close()
	End Sub

	Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
		Me.DialogResult = DialogResult.Cancel
		Me.Close()
	End Sub

	Private Sub spn_Hours_EditValueChanged(sender As Object, e As EventArgs) Handles spn_Minutes.EditValueChanged, spn_Hours.EditValueChanged
		Update_Buttons_Enabled()
	End Sub
End Class