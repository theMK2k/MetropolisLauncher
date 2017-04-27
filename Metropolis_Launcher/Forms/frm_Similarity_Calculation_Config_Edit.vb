Public Class frm_Similarity_Calculation_Config_Edit
	Private _id_Similarity_Calculation_Config As Integer = 0

	Public Sub New(Optional ByVal id_Similarity_Calculation_Config As Integer = 0)
		InitializeComponent()

		_id_Similarity_Calculation_Config = id_Similarity_Calculation_Config
	End Sub

	Private Sub frm_Similarity_Calculation_Config_Edit_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
		If _id_Similarity_Calculation_Config > 0 Then
			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				DS_ML.Fill_tbl_Similarity_Calculation_Config(tran, Me.DS_ML.tbl_Similarity_Calculation_Config, _id_Similarity_Calculation_Config)

				If Me.DS_ML.tbl_Similarity_Calculation_Config.Rows.Count <> 1 Then
					DevExpress.XtraEditors.XtraMessageBox.Show("There has been an error while fetching data from the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Me.Close()
				End If
			End Using
		Else
			Dim row As DataRow = Me.DS_ML.tbl_Similarity_Calculation_Config.NewRow
			Me.DS_ML.tbl_Similarity_Calculation_Config.Rows.Add(row)
		End If
	End Sub

	Private Sub Handle_Slider_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sld_401_Staff.EditValueChanged, sld_301_Group_Membership.EditValueChanged, sld_206_Other_Attributes.EditValueChanged, sld_205_Rating_Descriptors.EditValueChanged, sld_204_AgeP.EditValueChanged, sld_203_AgeO.EditValueChanged, sld_202_MaxPlayers.EditValueChanged, sld_201_MinPlayers.EditValueChanged, sld_106_Other_Attributes.EditValueChanged, sld_105_Educational_Categories.EditValueChanged, sld_103_Sports_Themes.EditValueChanged, sld_102_Perspectives.EditValueChanged, sld_101_Basic_Genres.EditValueChanged, sld_006_Year.EditValueChanged, sld_005_Developer.EditValueChanged, sld_004_Publisher.EditValueChanged, sld_003_MobyScore.EditValueChanged, sld_002_MobyRank.EditValueChanged, sld_001_Platform.EditValueChanged, sld_207_Multiplayer_Attributes.EditValueChanged, sld_115_Special_Edition.EditValueChanged, sld_114_DLC_Addon.EditValueChanged, sld_113_Interface_Control.EditValueChanged, sld_112_Vehicular_Themes.EditValueChanged, sld_111_Setting.EditValueChanged, sld_110_Narrative_Theme_Topic.EditValueChanged, sld_109_Pacing.EditValueChanged, sld_108_Gameplay.EditValueChanged, sld_107_Visual_Presentation.EditValueChanged
		BS_Similarity_Calculation_Config.EndEdit()
	End Sub

	Private Sub btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OK.Click
		BS_Similarity_Calculation_Config.EndEdit()

		Dim bOK As Boolean = False
		For Each col As DataColumn In DS_ML.tbl_Similarity_Calculation_Config.Columns
			If col.ColumnName.Contains("Weight") Then
				If TC.NZ(DS_ML.tbl_Similarity_Calculation_Config.Rows(0)(col.ColumnName), 0) <> 0 Then
					bOK = True
					Exit For
				End If
			End If
		Next

		If Not bOK Then
			DevExpress.XtraEditors.XtraMessageBox.Show("Not all weights should be set to 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		If _id_Similarity_Calculation_Config = 0 Then
			Dim sSQL As String = "INSERT INTO tbl_Similarity_Calculation_Config ("
			Dim sSQL1 As String = ""
			Dim sSQL2 As String = ""
			For Each col As DataColumn In DS_ML.tbl_Similarity_Calculation_Config.Columns
				If Not col.ColumnName.StartsWith("id_") Then
					sSQL1 &= IIf(sSQL1 = "", "", ", ") & "[" & col.ColumnName & "]"
					sSQL2 &= IIf(sSQL2 = "", "", ", ") & TC.getSQLFormat(DS_ML.tbl_Similarity_Calculation_Config.Rows(0)(col.ColumnName))
				End If
			Next

			sSQL &= sSQL1 & ") VALUES (" & sSQL2 & ")"

			DataAccess.FireProcedure(cls_Globals.Conn, 0, sSQL)

			Me.DialogResult = Windows.Forms.DialogResult.OK
			Me.Close()
		Else
			Dim sSQL As String = "UPDATE tbl_Similarity_Calculation_Config SET "
			Dim sSQL1 As String = ""

			For Each col As DataColumn In DS_ML.tbl_Similarity_Calculation_Config.Columns
				If Not col.ColumnName.StartsWith("id_") AndAlso Not col.ColumnName.ToLower = "sort" Then
					sSQL1 &= IIf(sSQL1 = "", "", ", ") & "[" & col.ColumnName & "] = " & TC.getSQLFormat(DS_ML.tbl_Similarity_Calculation_Config.Rows(0)(col.ColumnName))
				End If
			Next

			sSQL &= sSQL1 & " WHERE id_Similarity_Calculation_Config = " & _id_Similarity_Calculation_Config

			DataAccess.FireProcedure(cls_Globals.Conn, 0, sSQL)

			Me.DialogResult = Windows.Forms.DialogResult.OK
			Me.Close()
		End If
	End Sub
End Class
