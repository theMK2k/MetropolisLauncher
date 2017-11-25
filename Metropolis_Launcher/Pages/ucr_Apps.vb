Public Class ucr_Apps
	Public Event E_Hide()
	Public Event E_Show()

	Public Sub New()
		InitializeComponent()

		DS_MLApps.Fill_Categories(cls_Globals.Conn, Me.DS_MLApps.Categories)
		DS_MLApps.Fill_Apps(cls_Globals.Conn, Me.DS_MLApps.Apps)

		barmng_Apps.SetPopupContextMenu(grd_Apps, popmnu_Apps)
	End Sub

	Private Sub grd_Apps_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grd_Apps.MouseDown
		If e.Button = Windows.Forms.MouseButtons.Right Then
			cls_Globals.Suppress_MetroUINavigationBarsShowing = True
		End If

		'Show Popupmenu here
		If e.Button = Windows.Forms.MouseButtons.Right Then
			popmnu_Apps.ShowPopup(Control.MousePosition)
		End If
	End Sub

	Private Sub Run()
		If BS_Apps.Current Is Nothing Then
			Return
		End If

		If TC.NZ(BS_Apps.Current("Executable"), "") = "" Then
			Return
		End If

		Dim procinfo As New ProcessStartInfo(TC.NZ(BS_Apps.Current("Executable"), ""), TC.NZ(BS_Apps.Current("Arguments"), ""))
		procinfo.UseShellExecute = Not TC.NZ(BS_Apps.Current("RunExclusive"), False)

		If Not procinfo.UseShellExecute Then
			'Me.Visible = False
			RaiseEvent E_Hide()
		End If

		Dim proc As System.Diagnostics.Process = System.Diagnostics.Process.Start(procinfo)

		If Not procinfo.UseShellExecute Then
			proc.WaitForExit()
			'Me.Visible = True
			RaiseEvent E_Show()
		End If
	End Sub

#Region "Popupmenu"
	Private Sub popmnu_Apps_BeforePopup(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles popmnu_Apps.BeforePopup
		If Not grd_Apps.Allow_Popup Then
			e.Cancel = True
			Return
		End If

		If BS_Apps.Current Is Nothing Then
			bbi_Run.Enabled = False
			bbi_Edit.Enabled = False
			bbi_Delete.Enabled = False
			bbi_Reset.Enabled = False
			bbi_ResetAll.Enabled = False
		Else
			bbi_Run.Enabled = True
			bbi_Edit.Enabled = True
			bbi_Delete.Enabled = True
			bbi_Reset.Enabled = True
			bbi_ResetAll.Enabled = True
		End If
	End Sub

	Private Sub bbi_Run_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Run.ItemClick
		Run()
	End Sub

	Private Sub bbi_Add_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Add.ItemClick
		BS_Apps.AddNew()
		BS_Apps.MoveLast()
		Using frm As New frm_App_Edit(BS_Categories, BS_Apps)
			If frm.ShowDialog(Me.ParentForm) = DialogResult.OK Then
				BS_Apps.EndEdit()
				DS_MLApps.Update_Apps(cls_Globals.Conn, Me.DS_MLApps.Apps)
			Else
				BS_Apps.RemoveCurrent()
				Me.DS_MLApps.Apps.RejectChanges()
			End If
		End Using
	End Sub

	Private Sub bbi_Edit_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Edit.ItemClick
		Using frm As New frm_App_Edit(BS_Categories, BS_Apps)
			If frm.ShowDialog(Me.ParentForm) = DialogResult.OK Then
				BS_Apps.EndEdit()
				DS_MLApps.Update_Apps(cls_Globals.Conn, Me.DS_MLApps.Apps)
			Else
				DS_MLApps.Apps.RejectChanges()
			End If
		End Using
	End Sub

	Private Sub bbi_Delete_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Delete.ItemClick
		If MKDXHelper.MessageBox("Do you really want to delete the current application entry?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
			BS_Apps.RemoveCurrent()
			DS_MLApps.Update_Apps(cls_Globals.Conn, Me.DS_MLApps.Apps)
		End If
	End Sub

	Private Sub bbi_Reset_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Reset.ItemClick

	End Sub

	Private Sub bbi_ResetAll_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_ResetAll.ItemClick

	End Sub

	Private Sub grd_Apps_DoubleClick(sender As Object, e As EventArgs) Handles grd_Apps.DoubleClick
		Run()
	End Sub
#End Region

End Class
