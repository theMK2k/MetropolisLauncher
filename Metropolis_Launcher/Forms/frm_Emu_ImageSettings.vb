Public Class frm_Emu_ImageSettings

	Private Sub chb_Slideshow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_Slideshow.CheckedChanged
		Me.lbl_Slideshow_Delay.Enabled = Me.chb_Slideshow.Checked
		Me.spn_Slideshow_Delay.Enabled = Me.chb_Slideshow.Checked
	End Sub

	Private Sub spn_Slideshow_Delay_CustomDisplayText(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs) Handles spn_Slideshow_Delay.CustomDisplayText
		e.DisplayText = e.Value & " sec."
	End Sub

	Public Sub New()
		InitializeComponent()

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction()
			Me.DS_ML.Fill_tbl_Emu_Extras(tran, Me.DS_ML.tbl_Emu_Extras)
			Me.chb_Slideshow.Checked = TC.NZ(cls_Settings.GetSetting("Emu_Slideshow", cls_Settings.enm_Settingmodes.Per_User, tran), "0") = "1"
			Me.spn_Slideshow_Delay.Value = CInt(TC.NZ(cls_Settings.GetSetting("Emu_Slideshow_Delay", cls_Settings.enm_Settingmodes.Per_User, tran), "1"))
			tran.Commit()
		End Using

		chb_Slideshow_CheckedChanged(chb_Slideshow, Nothing)
	End Sub

	Private Sub btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OK.Click
		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			For Each row As DataRow In DS_ML.tbl_Emu_Extras.Rows
				If row.RowState <> DataRowState.Unchanged AndAlso row.RowState <> DataRowState.Deleted Then
					DS_ML.Upsert_tbl_Emu_Extras(tran, row("id_Emu_Extras"), row("Name"), row("Sort"), row("Description"), row("Hide"))
				End If
			Next

			cls_Settings.SetSetting("Emu_Slideshow", IIf(Me.chb_Slideshow.Checked, "1", "0"), cls_Settings.enm_Settingmodes.Per_User, tran)
			cls_Settings.SetSetting("Emu_Slideshow_Delay", spn_Slideshow_Delay.Value, cls_Settings.enm_Settingmodes.Per_User, tran)

			tran.Commit()
		End Using

		Me.DialogResult = Windows.Forms.DialogResult.OK
		Me.Close()
	End Sub

	Private Sub Ctl_MKDXSimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ctl_MKDXSimpleButton2.Click
		Me.Close()
	End Sub

	Private Function Get_Minimum_Sort() As Integer
		Dim iMinimum As Integer = -1
		For Each row As DataRow In Me.DS_ML.tbl_Emu_Extras
			If iMinimum = -1 OrElse row("Sort") < iMinimum Then
				iMinimum = row("Sort")
			End If
		Next

		Return iMinimum
	End Function

	Private Function Get_Maximum_Sort() As Integer
		Dim iMaximum As Integer = -1
		For Each row As DataRow In Me.DS_ML.tbl_Emu_Extras
			If iMaximum = -1 OrElse row("Sort") > iMaximum Then
				iMaximum = row("Sort")
			End If
		Next

		Return iMaximum
	End Function

	Private Sub btn_Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Up.Click
		Dim row_src As DataRow = BS_ImageOrdering.Current.row
		Dim row_dest As DataRow = Nothing
		For Each row As DataRow In DS_ML.tbl_Emu_Extras.Rows
			If row("Sort") = row_src("Sort") - 1 Then
				row_dest = row
				Exit For
			End If
		Next

		If row_dest IsNot Nothing Then
			Dim iSort As Integer = row_src("Sort")
			row_src("Sort") = row_dest("Sort")
			row_dest("Sort") = iSort
		End If
	End Sub

	Private Sub btn_Down_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Down.Click
		Dim row_src As DataRow = BS_ImageOrdering.Current.row
		Dim row_dest As DataRow = Nothing
		For Each row As DataRow In DS_ML.tbl_Emu_Extras.Rows
			If row("Sort") = row_src("Sort") + 1 Then
				row_dest = row
				Exit For
			End If
		Next

		If row_dest IsNot Nothing Then
			Dim iSort As Integer = row_src("Sort")
			row_src("Sort") = row_dest("Sort")
			row_dest("Sort") = iSort
		End If
	End Sub

	Private Sub BS_ImageOrdering_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BS_ImageOrdering.CurrentChanged
		If BS_ImageOrdering.Current("Sort") = Get_Minimum_Sort() Then btn_Up.Enabled = False Else btn_Up.Enabled = True
		If BS_ImageOrdering.Current("Sort") = Get_Maximum_Sort() Then btn_Down.Enabled = False Else btn_Down.Enabled = True
	End Sub

	Private Sub gv_ImageOrdering_MouseMove(sender As Object, e As MouseEventArgs) Handles gv_ImageOrdering.MouseMove
		Me.grd_ImageOrdering.ShowHandInColumns(gv_ImageOrdering, {"Hide"}, e)
	End Sub
End Class