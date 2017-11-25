Public Class ucr_ScummVM_Config
	Private _First_Paint_Handled As Boolean = False

	Private _id_Emu_Games As Long = 0       ' <> 0 when Game specific config
	Private _id_ScummVM_Configs As Long = 0  ' <> 0 when Template config

	Public Event E_Close(ByVal sender As Object, ByVal e As System.EventArgs)

	Public Event E_Template_Changing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)

	Public Sub New()
		InitializeComponent()

		If cls_Globals.Conn IsNot Nothing Then
			DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT -1 AS id_Rombase_ScummVM_BootParams, '<none>' AS Gamename, '<none>' AS Bootparam, '<none>' AS Description UNION SELECT id_Rombase_ScummVM_BootParams, Gamename, Bootparam, Bootparam || ' - ' || Description AS Description FROM rombase.tbl_Rombase_ScummVM_BootParams", Me.BTA_boot_param.Table)
		End If

		SetControlValues()
	End Sub

	Public Sub Show_Panels()
		Me.SuspendLayout()

		Me.pnl_Top.Visible = False
		Me.pnl_Bottom.Visible = False
		Me.pnl_Top_Templates.Visible = False
		Me.spltpnl_Main.Visible = False

		If Me._id_Emu_Games <> 0 Then
			'Game specific config
			Me.pnl_Top.Visible = True
			Me.spltpnl_Main.Visible = True
		End If

		If Me._id_ScummVM_Configs <> 0 Then
			'Template config
			Me.pnl_Top_Templates.Visible = True
			Me.spltpnl_Main.Visible = True
			Me.pnl_Bottom.Visible = True
		End If

		Me.ResumeLayout()
	End Sub

	Public Sub Clear()
		Me._id_ScummVM_Configs = 0
		Me._id_Emu_Games = 0

		Me.DS_ML.tbl_ScummVM_Configs.Clear()
		SetControlValues()
		Show_Panels()
	End Sub

	Public Sub SetEnableCompare(ByVal EnableCompare As Boolean)
		For Each ctrl As Windows.Forms.Control In MKNetDXLib.frm_MKDXBaseForm.GetAllControls(Me.Controls)
			If ctrl.GetType.GetInterface("IMKEditValueComparer") IsNot Nothing Then
				Dim compare_ctrl As MKNetDXLib.IMKEditValueComparer = ctrl
				If compare_ctrl IsNot Nothing Then
					compare_ctrl.MKEnable_EditValue_Compare = EnableCompare
				End If
			End If

			If Not EnableCompare AndAlso ctrl.GetType Is GetType(MKNetDXLib.ctl_MKDXLabel) Then
				Dim lbl As MKNetDXLib.ctl_MKDXLabel = CType(ctrl, MKNetDXLib.ctl_MKDXLabel)
				lbl.Font = New Font(lbl.Font.FontFamily, lbl.Font.Size)
			End If
		Next
	End Sub

	Public Sub Load_Template(ByVal id_ScummVM_Configs As Long)
		Me._id_ScummVM_Configs = id_ScummVM_Configs
		Me._id_Emu_Games = 0

		Me.DS_ML.tbl_ScummVM_Configs.Clear()
		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Fill_tbl_ScummVM_Template_Configs(tran, Me.DS_ML.tbl_ScummVM_Configs, id_ScummVM_Configs)

			Dim id_Rombase_ScummVM_Configs As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Rombase_ScummVM_Configs FROM main.tbl_ScummVM_Configs WHERE id_ScummVM_Configs = " & TC.getSQLFormat(id_ScummVM_Configs), tran), 0)
			If id_Rombase_ScummVM_Configs > 0 Then
				'compare
				DS_ML.Fill_tbl_Rombase_ScummVM_Template_Configs(tran, Me.DS_ML_Templates.tbl_ScummVM_Configs, id_Rombase_ScummVM_Configs)
				SetEnableCompare(True)
			Else
				'Don't compare
				SetEnableCompare(False)
			End If

			SetControlValues()
			Show_Panels()
		End Using
	End Sub

	Public Sub Load_Game_Config(ByVal id_Emu_Games As Long)
		Me._id_ScummVM_Configs = 0
		Me._id_Emu_Games = id_Emu_Games

		Me._sem_cmb_Template_EditValueChanging = True

		Me.DS_ML.tbl_ScummVM_Configs.Clear()
		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Fill_tbl_ScummVM_Configs(tran, Me.DS_ML.tbl_ScummVM_Configs, id_Emu_Games)

			Dim id_Templates As Object = DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_ScummVM_Configs_Template FROM main.tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games))

			DS_ML.Fill_tbl_ScummVM_Template_Configs(tran, Me.DS_ML_Templates.tbl_ScummVM_Configs)

			SetEnableCompare(True)

			cmb_Template.EditValue = id_Templates
			BS_ScummVM_Templates.Position = BS_ScummVM_Templates.Find("id_ScummVM_Configs", id_Templates)
			SetControlValues()
			Show_Panels()
		End Using

		Me._sem_cmb_Template_EditValueChanging = False
	End Sub

	Private Sub SetControlValues()
		If BS_ScummVM_Configs.Current Is Nothing Then Return

		Dim row As DataRow = BS_ScummVM_Configs.Current.Row

		'TODO!
		''CPU Cycles
		'Dim sCPUCycles As String = TC.NZ(row("cpu-cycles"), "")
		'If sCPUCycles.Contains("auto") Then
		'	rb_cpu_cycles_auto.Checked = True
		'ElseIf sCPUCycles.Contains("max") Then
		'	rb_cpu_cycles_max.Checked = True
		'ElseIf sCPUCycles.Contains("fixed") Then
		'	rb_cpu_cycles_fixed.Checked = True
		'End If

		'spn_p_dosbox_forcerate_specific.Value = 0
		'Dim s_p_dosbox_forcerate As String = TC.NZ(row("p_dosbox_forcerate"), "")
		'If s_p_dosbox_forcerate = "ntsc" Then
		'	rb_p_dosbox_forcerate_ntsc.Checked = True
		'ElseIf s_p_dosbox_forcerate = "pal" Then
		'	rb_p_dosbox_forcerate_pal.Checked = True
		'ElseIf IsNumeric(s_p_dosbox_forcerate) Then
		'	rb_p_dosbox_forcerate_specific.Checked = True
		'	spn_p_dosbox_forcerate_specific.Value = CInt(s_p_dosbox_forcerate)
		'Else
		'	rb_p_dosbox_forcerate_disabled.Checked = True
		'End If

		'Dim matches As System.Text.RegularExpressions.MatchCollection = MKNetLib.cls_MKRegex.GetMatches(sCPUCycles, "\d+")
		'If matches.Count > 0 Then
		'	spn_cpu_cycles_fixed.Value = Convert.ToInt64(matches(0).Value)
		'End If
	End Sub

	Private Sub SetDataRowValues()
		If BS_ScummVM_Configs.Current Is Nothing Then Return

		BS_ScummVM_Configs.EndEdit()

		Dim row As DataRow = BS_ScummVM_Configs.Current.Row

		'TODO!
		''CPU Cycles
		'If rb_cpu_cycles_auto.Checked Then
		'	row("cpu-cycles") = "auto"
		'ElseIf rb_cpu_cycles_max.Checked Then
		'	row("cpu-cycles") = "max"
		'Else
		'	row("cpu-cycles") = "fixed " & spn_cpu_cycles_fixed.Value.ToString
		'End If

		''Force Framerate
		'If rb_p_dosbox_forcerate_disabled.Checked Then
		'	row("p_dosbox_forcerate") = DBNull.Value
		'ElseIf rb_p_dosbox_forcerate_ntsc.Checked Then
		'	row("p_dosbox_forcerate") = "ntsc"
		'ElseIf rb_p_dosbox_forcerate_pal.Checked Then
		'	row("p_dosbox_forcerate") = "pal"
		'Else
		'	row("p_dosbox_forcerate") = CInt(spn_p_dosbox_forcerate_specific.Value.ToString)
		'End If
	End Sub

	Private Sub ucr_ScummVM_Config_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
		tree_Pages.ForceInitialize()
		tree_Pages.ExpandAll()

		If Not Me.DesignMode Then
			Me.tcl_ScummVM_Config.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
		End If
	End Sub

	Private Sub BTA_Tabs_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BTA_Tabs.CurrentChanged
		If BTA_Tabs.Current IsNot Nothing Then
			For Each tabpg As DevExpress.XtraTab.XtraTabPage In tcl_ScummVM_Config.TabPages
				If tabpg.Text = BTA_Tabs.Current("TabName") Then
					tcl_ScummVM_Config.SelectedTabPage = tabpg
				End If
			Next
		End If
	End Sub

	Private Sub btn_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save_Template.Click
		Save_Template()
	End Sub

	Public Function Save_Template(Optional ByVal Create_Duplicate As Boolean = False, Optional ByVal New_Template_Name As String = "") As Int64
		Dim result As Int64 = 0

		If BS_ScummVM_Configs.Current Is Nothing Then Return result

		'Set Non-bound Data, also does BS.EndEdit
		SetDataRowValues()

		If Not BS_ScummVM_Configs.Current.Row.RowState = System.Data.DataRowState.Unchanged Then
			If TC.NZ(BS_ScummVM_Configs.Current("Displayname"), "").Length = 0 Then
				MKDXHelper.MessageBox("Please provide a template name.", "Save Template", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				txb_Template_Name.Focus()
				Return result
			End If

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				result = DS_ML.Upsert_tbl_ScummVM_Configs_Templates(tran, BS_ScummVM_Configs.Current.Row)
				BS_ScummVM_Configs.Current.Row.AcceptChanges()
			End Using
		End If

		If Create_Duplicate Then
			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				result = DS_ML.Upsert_tbl_ScummVM_Configs_Templates(tran, BS_ScummVM_Configs.Current.Row, True, New_Template_Name)
			End Using
		End If

		Return result
	End Function

	''' <summary>
	''' Save the configuration for a specific game
	''' </summary>
	''' <param name="tran"></param>
	''' <remarks></remarks>
	Public Sub Save_Game_Config(ByVal tran As SQLite.SQLiteTransaction)
		If BS_ScummVM_Configs.Current Is Nothing Then Return

		'Set Non-bound Data, also does BS.EndEdit
		SetDataRowValues()

		If BS_ScummVM_Configs.Current.Row.RowState = System.Data.DataRowState.Unchanged Then
			Return
		End If

		DS_ML.Upsert_tbl_ScummVM_Config(tran, BS_ScummVM_Configs.Current.Row, Me._id_Emu_Games)
		BS_ScummVM_Configs.Current.Row.AcceptChanges()
	End Sub

	Public Sub Reject_Configuration()
		BS_ScummVM_Configs.Current.Row.RejectChanges()
	End Sub

	Private Sub btn_Cancel_Template_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel_Template.Click
		RaiseEvent E_Close(Me, New System.EventArgs)
	End Sub

	Public Function HasChanges() As Boolean
		BS_ScummVM_Configs.EndEdit()
		If BS_ScummVM_Configs.Current Is Nothing Then
			Return False
		Else
			Return Not BS_ScummVM_Configs.Current.Row.RowState = System.Data.DataRowState.Unchanged
		End If
	End Function

	Private _sem_cmb_Template_EditValueChanging As Boolean = False

	Private Sub cmb_Template_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Template.EditValueChanged
		If _sem_cmb_Template_EditValueChanging Then Return
		If _id_Emu_Games = 0 Then Return 'Only check, if it is a game specific config

		DataAccess.FireProcedure(cls_Globals.Conn, 0, "UPDATE tbl_Emu_Games SET id_ScummVM_Configs_Template = " & TC.getSQLFormat(cmb_Template.EditValue) & " WHERE id_Emu_Games = " & Me._id_Emu_Games)

		Load_Game_Config(Me._id_Emu_Games)
	End Sub

	Private Sub cmb_Template_EditValueChanging(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles cmb_Template.EditValueChanging
		If _sem_cmb_Template_EditValueChanging Then Return
		If _id_Emu_Games = 0 Then Return 'Only check, if it is a game specific config

		If Me.HasChanges Then
			Dim res As DialogResult = MKDXHelper.MessageBox("Do you want to save your current changes before changing the underlying template?", "Change Template", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

			If res = Windows.Forms.DialogResult.Yes Then
				Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
					Save_Game_Config(tran)
					tran.Commit()
				End Using
			End If

			If res = Windows.Forms.DialogResult.No Then
				Reject_Configuration()
			End If

			If res = Windows.Forms.DialogResult.Cancel Then
				e.Cancel = True
			End If
		End If
	End Sub

	Private Sub btn_savepath_Click(sender As Object, e As EventArgs) Handles btn_savepath.Click
		Dim dirpath As String = MKNetLib.cls_MKFileSupport.OpenFolderDialog("", True, Me.ParentForm)
		If Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
			Me.txb_savepath.EditValue = MKNetLib.cls_MKStringSupport.Clean_Right(dirpath, "\")
		End If
	End Sub

	Private Sub btn_extrapath_Click(sender As Object, e As EventArgs) Handles btn_extrapath.Click
		Dim dirpath As String = MKNetLib.cls_MKFileSupport.OpenFolderDialog("", True, Me.ParentForm)
		If Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
			Me.txb_extrapath.EditValue = MKNetLib.cls_MKStringSupport.Clean_Right(dirpath, "\")
		End If
	End Sub


	Private Sub btn_soundfont_Click(sender As Object, e As EventArgs) Handles btn_soundfont.Click
		Dim filepath As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("Open SoundFont", "All Files (*.*)|*.*", ParentForm:=Me.ParentForm)
		If Alphaleonis.Win32.Filesystem.File.Exists(filepath) Then
			Me.txb_soundfont.EditValue = filepath
		End If
	End Sub

	Private Sub Combobox_Close_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_language.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Close Then
			sender.EditValue = DBNull.Value
		End If
	End Sub

	Private Sub Handle_volume_ValueChanged(sender As Object, e As EventArgs) Handles tb_music_volume.ValueChanged, tb_speech_volume.ValueChanged, tb_sfx_volume.ValueChanged
		If sender Is tb_music_volume Then lbl_music_volume_value.Text = sender.Value.ToString
		If sender Is tb_speech_volume Then lbl_speech_volume_value.Text = sender.Value.ToString
		If sender Is tb_sfx_volume Then lbl_sfx_volume_value.Text = sender.Value.ToString
	End Sub
End Class