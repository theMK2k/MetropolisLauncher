Imports System.ComponentModel

Public Class frm_Similarity_Calculation

	Private _id_Emu_Games As Integer = 0
	Private _Game As String
	Private _Platform As String

	Private _id_Similarity_Calculation_Results As Integer = 0

	Private _last_used_Similarity_Calculation_Configuration = ""
	Private _last_used_id_Similarity_Calculation_Configuration As Integer = 0

	Public Updated_Results As New ArrayList

	Public Results_Saved As Boolean = True

	Public Sub New(ByVal id_Emu_Games As Integer, ByVal Game As String, ByVal Platform As String)
		InitializeComponent()

		barmng.SetPopupContextMenu(grd_Similarity_Calculation, popmnu_Similarity_Calculation)

		_id_Emu_Games = id_Emu_Games
		_Game = Game
		_Platform = Platform

		Me.Text = Me.Text.Replace("%1%", Game & " (" & Platform & ")")
	End Sub

	Public Sub New(ByVal id_Similarity_Calculation_Results)
		InitializeComponent()

		barmng.SetPopupContextMenu(grd_Similarity_Calculation, popmnu_Similarity_Calculation)

		_id_Similarity_Calculation_Results = id_Similarity_Calculation_Results

		btn_Save.Visible = False
		pnl_Top.Visible = False
	End Sub

	Private Sub Refill_cmb_Similarity_Calculation_Configuration()
		Dim obj_id As Object = cmb_Similarity_Calculation_Configuration.EditValue
		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Fill_tbl_Similarity_Calculation_Config(tran, DS_ML.tbl_Similarity_Calculation_Config)
		End Using

		If TC.NZ(obj_id, 0) <> 0 AndAlso DS_ML.tbl_Similarity_Calculation_Config.Select("id_Similarity_Calculation_Config = " & TC.NZ(obj_id, 0)).Length = 1 Then
			cmb_Similarity_Calculation_Configuration.EditValue = obj_id
		Else
			cmb_Similarity_Calculation_Configuration.EditValue = 0
		End If
	End Sub

	Private Sub frm_Similarity_Calculation_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		cls_Settings.SetSetting("frm_Similarity_Calculation-cmb_Similarity_Calculation_Configuration", cmb_Similarity_Calculation_Configuration.EditValue, cls_Settings.enm_Settingmodes.Per_User)
	End Sub

	Private Sub frm_Similarity_Calculation_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
		Cursor = Cursors.WaitCursor
		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			Refill_cmb_Similarity_Calculation_Configuration()

			Dim idConf As Integer = TC.NZ(cls_Settings.GetSetting("frm_Similarity_Calculation-cmb_Similarity_Calculation_Configuration", cls_Settings.enm_Settingmodes.Per_User, tran), 0)
			If idConf <> 0 AndAlso DS_ML.tbl_Similarity_Calculation_Config.Select("id_Similarity_Calculation_Config = " & idConf).Length = 1 Then
				cmb_Similarity_Calculation_Configuration.EditValue = idConf
			End If

			If _id_Similarity_Calculation_Results > 0 Then
				Me.Text = "Results for " & TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT Name FROM tbl_Similarity_Calculation_Results WHERE id_Similarity_Calculation_Results = " & TC.getSQLFormat(_id_Similarity_Calculation_Results), tran), "")
				MKNetLib.cls_MKClientSupport.SetBindingSourcePosition(BS_Similarity_Calculation_Config, "id_Similarity_Calculation_Config", TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Similarity_Calculation_Config FROM tbl_Similarity_Calculation_Results WHERE id_Similarity_Calculation_Results = " & TC.getSQLFormat(_id_Similarity_Calculation_Results), tran), 0))
				DS_ML.Fill_tbl_Similarity_Calculation_From_Results(tran, DS_ML.tbl_Similarity_Calculation, _id_Similarity_Calculation_Results)
			End If
		End Using
		Cursor = Cursors.Default
	End Sub

	Private Sub cmb_Similarity_Calculation_Configuration_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_Similarity_Calculation_Configuration.ButtonClick
		Select Case e.Button.Kind
			Case DevExpress.XtraEditors.Controls.ButtonPredefines.Plus
				Using frm As New frm_Similarity_Calculation_Config_Edit
					If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
						Refill_cmb_Similarity_Calculation_Configuration()
					End If
				End Using
			Case DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis
				Using frm As New frm_Similarity_Calculation_Config_Edit(Me.cmb_Similarity_Calculation_Configuration.EditValue)
					If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
						Refill_cmb_Similarity_Calculation_Configuration()
					End If
				End Using
			Case DevExpress.XtraEditors.Controls.ButtonPredefines.Minus
				If TC.NZ(cmb_Similarity_Calculation_Configuration.EditValue, 0) > 0 Then
					If TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT COUNT(1) FROM tbl_Similarity_Calculation_Results WHERE id_Similarity_Calculation_Config = " & TC.getSQLFormat(cmb_Similarity_Calculation_Configuration.EditValue)), 0) <> 0 Then
						MKDXHelper.MessageBox("The currently selected configuration is still in use by one or more saved results. You have to remove these results before deleting the configuration.", "Delete Configuration", MessageBoxButtons.OK, MessageBoxIcon.Information)
						Return
					End If

					DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Similarity_Calculation_Config WHERE id_Similarity_Calculation_Config = " & TC.getSQLFormat(cmb_Similarity_Calculation_Configuration.EditValue))
					Refill_cmb_Similarity_Calculation_Configuration()
				End If
		End Select
	End Sub

	Private Sub btn_Go_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Go.Click
		If TC.NZ(cmb_Similarity_Calculation_Configuration.EditValue, 0) = 0 Then
			If MKDXHelper.MessageBox("Warning: the default configuration just sits there being quite useless. It weights each feature set with the same amount and thus gives a rather unfortunate result in the similarity calculation. Please consider creating a configuration to your needs." & ControlChars.CrLf & ControlChars.CrLf & "Do you want to continue calculating similarities based on the default configuration?", "Similarity Calculation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
				Return
			End If
		End If

		Me.btn_Save.Enabled = False
		Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Nothing), 400, 100, ProgressBarStyle.Marquee, False, "Calculating similarities, this will take a while - please be patient...", 0, 1, False)
		Try
			prg.Start()

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				DS_ML.Fill_tbl_Similarity_Calculation(tran, DS_ML.tbl_Similarity_Calculation, Me._id_Emu_Games, cmb_Similarity_Calculation_Configuration.EditValue, chb_Haves_Only.Checked)
			End Using

			prg.Close()

			_last_used_Similarity_Calculation_Configuration = cmb_Similarity_Calculation_Configuration.Text
			_last_used_id_Similarity_Calculation_Configuration = cmb_Similarity_Calculation_Configuration.EditValue

			Me.btn_Save.Enabled = True

			Me.Results_Saved = False
		Catch ex As Exception
			prg.Close()

		End Try

	End Sub

	Private Sub btn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Close.Click
		Me.Close()
	End Sub

	Private Sub btn_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save.Click
		Dim ResultsName As String = _Game & " (" & _Platform & ") [" & _last_used_Similarity_Calculation_Configuration & "]"

		Dim bLoop As Boolean = True

		While bLoop
			bLoop = False

			Using frm As New MKNetDXLib.frm_TextBoxEdit("Name:", "Please provide a name for the results", ResultsName, False)
				If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
					ResultsName = frm.Input

					Dim id_Similarity_Calculation_Results = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_Similarity_Calculation_Results FROM tbl_Similarity_Calculation_Results WHERE Name = " & TC.getSQLFormat(ResultsName)), 0)

					If id_Similarity_Calculation_Results <> 0 Then
						Dim rel As DialogResult = MKDXHelper.MessageBox("The result set '" & ResultsName & "' already exists, do you want to overwrite? Choose 'No' to select a different name.", "Result Set Name akready exists", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)

						If rel = Windows.Forms.DialogResult.No Then
							bLoop = True
							Continue While
						End If

						If rel = Windows.Forms.DialogResult.Cancel Then
							Return
						End If

						Me.Updated_Results.Add(id_Similarity_Calculation_Results)
					End If

					Cursor = Cursors.WaitCursor

					Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
						id_Similarity_Calculation_Results = DS_ML.Upsert_tbl_Similarity_Calculation_Results(tran, DS_ML.tbl_Similarity_Calculation, ResultsName, _last_used_id_Similarity_Calculation_Configuration, _id_Emu_Games, id_Similarity_Calculation_Results)
						Updated_Results.Add(id_Similarity_Calculation_Results)
						tran.Commit()
					End Using

					Cursor = Cursors.Default

					MKDXHelper.MessageBox("Results are saved as '" & ResultsName & "'.", "Save Results", MessageBoxButtons.OK, MessageBoxIcon.Information)

					Me.Results_Saved = True
				End If
			End Using
		End While
	End Sub

	Private Sub BS_Similarity_Calculation_Config_CurrentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BS_Similarity_Calculation_Config.CurrentChanged
		If BS_Similarity_Calculation_Config.Current Is Nothing OrElse TC.NZ(BS_Similarity_Calculation_Config.Current("id_Similarity_Calculation_Config"), 0) = 0 Then
			For Each btn As DevExpress.XtraEditors.Controls.EditorButton In cmb_Similarity_Calculation_Configuration.Properties.Buttons
				If {DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, DevExpress.XtraEditors.Controls.ButtonPredefines.Minus}.Contains(btn.Kind) Then
					btn.Enabled = False
				End If
			Next
		Else
			For Each btn As DevExpress.XtraEditors.Controls.EditorButton In cmb_Similarity_Calculation_Configuration.Properties.Buttons
				btn.Enabled = True
			Next
		End If
	End Sub

	Private Sub bbi_Details_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Details.ItemClick
		If BS_Similarity_Calculation.Current Is Nothing Then Return

		Dim id_Similarity_Calculation_Results = _id_Similarity_Calculation_Results

		If id_Similarity_Calculation_Results = 0 AndAlso Updated_Results.Count > 0 Then
			id_Similarity_Calculation_Results = Updated_Results(Updated_Results.Count - 1)
		End If

		Using frm As New frm_Similarity_Calculation_Details(id_Similarity_Calculation_Results, BS_Similarity_Calculation.Current.Row, TC.NZ(BS_Similarity_Calculation.Current("id_Emu_Games"), 0), TC.NZ(BS_Similarity_Calculation.Current("id_Moby_Releases"), 0), _last_used_id_Similarity_Calculation_Configuration, _id_Emu_Games)
			frm.ShowDialog(Me)
		End Using
	End Sub

	Private Sub frm_Similarity_Calculation_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
		If Me.Results_Saved = False Then
			If Not MKDXHelper.MessageBox("Please consider saving your results before closing. Do you want to close anyway?", "Results not saved", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
				e.Cancel = True
			End If
		End If
	End Sub
End Class