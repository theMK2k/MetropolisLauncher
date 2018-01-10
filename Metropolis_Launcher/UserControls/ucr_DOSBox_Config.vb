Public Class ucr_DOSBox_Config
	Private _First_Paint_Handled As Boolean = False

	Private _id_Emu_Games As Long = 0       ' <> 0 when Game specific config
	Private _id_DOSBox_Configs As Long = 0  ' <> 0 when Template config

	Public Event E_Close(ByVal sender As Object, ByVal e As System.EventArgs)

	Public Event E_Template_Changing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)

	Public Sub New()
		InitializeComponent()

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

		If Me._id_DOSBox_Configs <> 0 Then
			'Template config
			Me.pnl_Top_Templates.Visible = True
			Me.spltpnl_Main.Visible = True
			Me.pnl_Bottom.Visible = True
		End If

		Me.ResumeLayout()
	End Sub

	Public Sub Clear()
		Me._id_DOSBox_Configs = 0
		Me._id_Emu_Games = 0

		Me.DS_ML.tbl_DOSBox_Configs.Clear()
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

	Public Sub Load_Template(ByVal id_DOSBox_Configs As Long)
		Me._id_DOSBox_Configs = id_DOSBox_Configs
		Me._id_Emu_Games = 0

		Me.DS_ML.tbl_DOSBox_Configs.Clear()
		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Fill_tbl_DOSBox_Template_Configs(tran, Me.DS_ML.tbl_DOSBox_Configs, id_DOSBox_Configs)

			Dim id_Rombase_DOSBox_Configs As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Rombase_DOSBox_Configs FROM main.tbl_DOSBox_Configs WHERE id_DOSBox_Configs = " & TC.getSQLFormat(id_DOSBox_Configs), tran), 0)
			If id_Rombase_DOSBox_Configs > 0 Then
				'compare
				DS_ML.Fill_tbl_Rombase_DOSBox_Template_Configs(tran, Me.DS_ML_Templates.tbl_DOSBox_Configs, id_Rombase_DOSBox_Configs)
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
		Me._id_DOSBox_Configs = 0
		Me._id_Emu_Games = id_Emu_Games

		Me._sem_cmb_Template_EditValueChanging = True

		Me.DS_ML.tbl_DOSBox_Configs.Clear()
		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Fill_tbl_DOSBox_Configs(tran, Me.DS_ML.tbl_DOSBox_Configs, id_Emu_Games)

			Dim id_Templates As Object = DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_DOSBox_Configs_Template FROM main.tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games))

			DS_ML.Fill_tbl_DOSBox_Template_Configs(tran, Me.DS_ML_Templates.tbl_DOSBox_Configs)

			SetEnableCompare(True)

			cmb_Template.EditValue = id_Templates
			BS_Templates.Position = BS_Templates.Find("id_DOSBox_Configs", id_Templates)
			SetControlValues()
			Show_Panels()
		End Using

		Me._sem_cmb_Template_EditValueChanging = False
	End Sub

	Private Sub SetControlValues()
		If BS_DOSBox_Configs.Current Is Nothing Then Return

		Dim row As DataRow = BS_DOSBox_Configs.Current.Row

		'CPU Cycles
		Dim sCPUCycles As String = TC.NZ(row("cpu-cycles"), "")
		If sCPUCycles.Contains("auto") Then
			rb_cpu_cycles_auto.Checked = True
		ElseIf sCPUCycles.Contains("max") Then
			rb_cpu_cycles_max.Checked = True
		ElseIf sCPUCycles.Contains("fixed") Then
			rb_cpu_cycles_fixed.Checked = True
		End If

		spn_p_dosbox_forcerate_specific.Value = 0
		Dim s_p_dosbox_forcerate As String = TC.NZ(row("p_dosbox_forcerate"), "")
		If s_p_dosbox_forcerate = "ntsc" Then
			rb_p_dosbox_forcerate_ntsc.Checked = True
		ElseIf s_p_dosbox_forcerate = "pal" Then
			rb_p_dosbox_forcerate_pal.Checked = True
		ElseIf IsNumeric(s_p_dosbox_forcerate) Then
			rb_p_dosbox_forcerate_specific.Checked = True
			spn_p_dosbox_forcerate_specific.Value = CInt(s_p_dosbox_forcerate)
		Else
			rb_p_dosbox_forcerate_disabled.Checked = True
		End If

		Dim matches As System.Text.RegularExpressions.MatchCollection = MKNetLib.cls_MKRegex.GetMatches(sCPUCycles, "\d+")
		If matches.Count > 0 Then
			spn_cpu_cycles_fixed.Value = Convert.ToInt64(matches(0).Value)
		End If
	End Sub

	Private Sub SetDataRowValues()
		If BS_DOSBox_Configs.Current Is Nothing Then Return

		BS_DOSBox_Configs.EndEdit()

		Dim row As DataRow = BS_DOSBox_Configs.Current.Row

		'CPU Cycles
		If rb_cpu_cycles_auto.Checked Then
			row("cpu-cycles") = "auto"
		ElseIf rb_cpu_cycles_max.Checked Then
			row("cpu-cycles") = "max"
		Else
			row("cpu-cycles") = "fixed " & spn_cpu_cycles_fixed.Value.ToString
		End If

		'Force Framerate
		If rb_p_dosbox_forcerate_disabled.Checked Then
			row("p_dosbox_forcerate") = DBNull.Value
		ElseIf rb_p_dosbox_forcerate_ntsc.Checked Then
			row("p_dosbox_forcerate") = "ntsc"
		ElseIf rb_p_dosbox_forcerate_pal.Checked Then
			row("p_dosbox_forcerate") = "pal"
		Else
			row("p_dosbox_forcerate") = CInt(spn_p_dosbox_forcerate_specific.Value.ToString)
		End If
	End Sub

	Private Sub ucr_DOSBox_Config_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
		tree_Pages.ForceInitialize()
		tree_Pages.ExpandAll()

		If Not Me.DesignMode Then
			Me.tcl_Dosbox_Config.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
		End If
	End Sub

	Private Sub BTA_Tabs_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BTA_Tabs.CurrentChanged
		If BTA_Tabs.Current IsNot Nothing Then
			For Each tabpg As DevExpress.XtraTab.XtraTabPage In tcl_Dosbox_Config.TabPages
				If tabpg.Text = BTA_Tabs.Current("TabName") Then
					tcl_Dosbox_Config.SelectedTabPage = tabpg

					'Workaround for "MIDI (Patches)" Tab visibility
					If tabpg.Text = "MIDI (Patches)" Then
						Me.p_midi_EditValueChanged(Nothing, Nothing)

						'	My.Application.DoEvents()
						'	Dim chosen_mididevice As Object = Me.cmb_p_midi_mididevice.EditValue
						'	Me.cmb_p_midi_mididevice.EditValue = "mt32"
						'	My.Application.DoEvents()
						'	Me.cmb_p_midi_mididevice.EditValue = "fluidsynth"
						'	My.Application.DoEvents()
						'	Me.cmb_p_midi_mididevice.EditValue = chosen_mididevice
						'	My.Application.DoEvents()
					End If
				End If
      Next
		End If
	End Sub

	Private Sub rb_cpu_cycles_fixed_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rb_cpu_cycles_fixed.CheckedChanged
		Me.spn_cpu_cycles_fixed.Enabled = Me.rb_cpu_cycles_fixed.Checked
	End Sub

	Private Sub spn_cpu_cycleup_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles spn_cpu_cycleup.EditValueChanged, spn_cpu_cycledown.EditValueChanged
		lbl_cpu_cycleup_unit.Text = IIf(spn_cpu_cycleup.Value < 100, "%", "frames")
		lbl_cpu_cycledown_unit.Text = IIf(spn_cpu_cycledown.Value < 100, "%", "frames")
	End Sub

	Private Sub chb_ml_useloadfix_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_ml_useloadfix.CheckedChanged
		lbl_ml_loadfix.Enabled = chb_ml_useloadfix.Checked
		spn_ml_loadfix.Enabled = chb_ml_useloadfix.Checked
	End Sub

	Private Sub Volume_Trackbars_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb_ml_volume_master_left.ValueChanged, tb_ml_volume_master_right.ValueChanged, tb_ml_volume_cdaudio_left.ValueChanged, tb_ml_volume_cdaudio_right.ValueChanged, tb_ml_volume_disney_left.ValueChanged, tb_ml_volume_disney_right.ValueChanged, tb_ml_volume_fm_left.ValueChanged, tb_ml_volume_fm_right.ValueChanged, tb_ml_volume_gus_left.ValueChanged, tb_ml_volume_gus_right.ValueChanged, tb_ml_volume_sb_left.ValueChanged, tb_ml_volume_sb_right.ValueChanged, tb_ml_volume_spkr_left.ValueChanged, tb_ml_volume_spkr_right.ValueChanged
		If sender Is tb_ml_volume_master_left Then lbl_ml_volume_master_left.Text = sender.Value.ToString
		If sender Is tb_ml_volume_master_right Then lbl_ml_volume_master_right.Text = sender.Value.ToString
		If sender Is tb_ml_volume_cdaudio_left Then lbl_ml_volume_cdaudio_left.Text = sender.Value.ToString
		If sender Is tb_ml_volume_cdaudio_right Then lbl_ml_volume_cdaudio_right.Text = sender.Value.ToString
		If sender Is tb_ml_volume_disney_left Then lbl_ml_volume_disney_left.Text = sender.Value.ToString
		If sender Is tb_ml_volume_disney_right Then lbl_ml_volume_disney_right.Text = sender.Value.ToString
		If sender Is tb_ml_volume_fm_left Then lbl_ml_volume_fm_left.Text = sender.Value.ToString
		If sender Is tb_ml_volume_fm_right Then lbl_ml_volume_fm_right.Text = sender.Value.ToString
		If sender Is tb_ml_volume_gus_left Then lbl_ml_volume_gus_left.Text = sender.Value.ToString
		If sender Is tb_ml_volume_gus_right Then lbl_ml_volume_gus_right.Text = sender.Value.ToString
		If sender Is tb_ml_volume_sb_left Then lbl_ml_volume_sb_left.Text = sender.Value.ToString
		If sender Is tb_ml_volume_sb_right Then lbl_ml_volume_sb_right.Text = sender.Value.ToString
		If sender Is tb_ml_volume_spkr_left Then lbl_ml_volume_spkr_left.Text = sender.Value.ToString
		If sender Is tb_ml_volume_spkr_right Then lbl_ml_volume_spkr_right.Text = sender.Value.ToString
	End Sub

	Private Sub btn_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save_Template.Click
		Save_Template()
	End Sub

	Public Function Save_Template(Optional ByVal Create_Duplicate As Boolean = False, Optional ByVal New_Template_Name As String = "") As Int64
		Dim result As Int64 = 0

		If BS_DOSBox_Configs.Current Is Nothing Then Return result

		BS_DOSBox_Configs.EndEdit()

		'Set Non-bound Data, also does BS.EndEdit
		SetDataRowValues()

		If Not BS_DOSBox_Configs.Current.Row.RowState = System.Data.DataRowState.Unchanged Then
			If TC.NZ(BS_DOSBox_Configs.Current("Displayname"), "").Length = 0 Then
				MKDXHelper.MessageBox("Please provide a template name.", "Save Template", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				txb_Template_Name.Focus()
				Return result
			End If

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				result = DS_ML.Upsert_tbl_DOSBox_Configs_Templates(tran, BS_DOSBox_Configs.Current.Row)
				BS_DOSBox_Configs.Current.Row.AcceptChanges()
			End Using
		End If

		If Create_Duplicate Then
			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				result = DS_ML.Upsert_tbl_DOSBox_Configs_Templates(tran, BS_DOSBox_Configs.Current.Row, True, New_Template_Name)
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
		If BS_DOSBox_Configs.Current Is Nothing Then Return

		'Set Non-bound Data, also does BS.EndEdit
		SetDataRowValues()

		If BS_DOSBox_Configs.Current.Row.RowState = System.Data.DataRowState.Unchanged Then
			Return
		End If

		DS_ML.Upsert_tbl_DOSBox_Config(tran, BS_DOSBox_Configs.Current.Row, Me._id_Emu_Games)
		BS_DOSBox_Configs.Current.Row.AcceptChanges()
	End Sub

	Public Sub Reject_Configuration()
		BS_DOSBox_Configs.Current.Row.RejectChanges()
	End Sub

	Private Sub btn_Cancel_Template_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel_Template.Click
		RaiseEvent E_Close(Me, New System.EventArgs)
	End Sub

	Public Function HasChanges() As Boolean
		BS_DOSBox_Configs.EndEdit()
		If BS_DOSBox_Configs.Current Is Nothing Then
			Return False
		Else
			Return Not BS_DOSBox_Configs.Current.Row.RowState = System.Data.DataRowState.Unchanged
		End If
	End Function

	Private _sem_cmb_Template_EditValueChanging As Boolean = False

	Private Sub cmb_Template_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Template.EditValueChanged
		If _sem_cmb_Template_EditValueChanging Then Return
		If _id_Emu_Games = 0 Then Return 'Only check, if it is a game specific config

		DataAccess.FireProcedure(cls_Globals.Conn, 0, "UPDATE tbl_Emu_Games SET id_DOSBox_Configs_Template = " & TC.getSQLFormat(cmb_Template.EditValue) & " WHERE id_Emu_Games = " & Me._id_Emu_Games)

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

	Private Sub Handle_LookupEdit_Delete_Button_Press(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_p_cputype.ButtonPressed, cmb_p_dosbox_pit_hack.ButtonPressed, cmb_p_sdl_output.ButtonPressed, cmb_p_render_scaler.ButtonPressed, cmb_p_sblaster_sbtype.ButtonPressed, cmb_p_sblaster_oplmode.ButtonPressed, cmb_p_sblaster_oplmode.ButtonPressed, cmb_p_sblaster_hardwarebase.ButtonPressed, cmb_p_midi_mididevice.ButtonPressed, cmb_p_midi_mt32_reverb_mode.ButtonPressed, cmb_p_midi_mididevice.ButtonPressed, cmb_p_midi_mt32_dac.ButtonPressed, cmb_p_sblaster_oplemu.ButtonPressed
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
			CType(sender, MKNetDXLib.ctl_MKDXLookupEdit).EditValue = DBNull.Value
		End If
	End Sub

	Private Sub btn_p_sdl_pixelshader_Click(sender As System.Object, e As System.EventArgs) Handles btn_p_sdl_pixelshader.Click
		Dim filepath As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("Browse Pixelshader program file", "Pixelshader programs *.fx|*.fx", ParentForm:=Me.ParentForm)
		If Alphaleonis.Win32.Filesystem.File.Exists(filepath) Then
			Me.txb_p_sdl_pixelshader.EditValue = Alphaleonis.Win32.Filesystem.Path.GetFileName(filepath)
		End If
	End Sub

	Private Sub btn_p_sdl_pixelshader_Clear_Click(sender As System.Object, e As System.EventArgs) Handles btn_p_sdl_pixelshader_Clear.Click
		Me.txb_p_sdl_pixelshader.EditValue = "none"
	End Sub

	Private Sub cmb_p_dosbox_vmemsize_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cmb_p_dosbox_vmemsize.EditValueChanged
		lbl_p_dosbox_vmemsize_details.Text = ""
		If TC.NZ(cmb_p_dosbox_vmemsize.EditValue, -1) < 0 Then Return
		Select Case TC.NZ(cmb_p_dosbox_vmemsize.EditValue, 0)
			Case 0
				lbl_p_dosbox_vmemsize_details.Text = "800x600  at 256 colors"
			Case 1
				lbl_p_dosbox_vmemsize_details.Text = "1024x768  at 256 colors or 800x600  at 64k colors"
			Case 2
				lbl_p_dosbox_vmemsize_details.Text = "1600x1200 at 256 colors or 1024x768 at 64k colors or 640x480 at 16M colors"
			Case 4
				lbl_p_dosbox_vmemsize_details.Text = "1600x1200 at 64k colors or 1024x768 at 16M colors"
			Case 8
				lbl_p_dosbox_vmemsize_details.Text = "up to 1600x1200 at 16M colors"
			Case 16
				lbl_p_dosbox_vmemsize_details.Text = "up to 1600x1200 at 16M colors + double/triple buffer extra ram"
			Case 24
				lbl_p_dosbox_vmemsize_details.Text = "up to 1600x1200 at 16M colors + double/triple buffer extra ram"
			Case 32
				lbl_p_dosbox_vmemsize_details.Text = "up to 1600x1200 at 16M colors + double/triple buffer extra ram"
		End Select
	End Sub

	Private Sub chb_p_dosbox_forcerate_specific_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rb_p_dosbox_forcerate_specific.CheckedChanged
		lbl_p_dosbox_forcerate_specific.Enabled = rb_p_dosbox_forcerate_specific.Checked
		spn_p_dosbox_forcerate_specific.Enabled = rb_p_dosbox_forcerate_specific.Checked
	End Sub

	Private Sub cmb_p_vsync_vsyncmode_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cmb_p_vsync_vsyncmode.EditValueChanged, cmb_p_pci_voodoo.EditValueChanged, cmb_p_glide_lfb.EditValueChanged, cmb_p_glide_glide.EditValueChanged
		Dim bEnable As Boolean = {"on", "force"}.Contains(TC.NZ(cmb_p_vsync_vsyncmode.EditValue, ""))
		spn_p_vsync_vsyncrate.Enabled = bEnable
		lbl_p_vsync_vsyncrate.Enabled = bEnable
	End Sub

	Private Sub chb_p_keyboard_aux_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chb_p_keyboard_aux.CheckedChanged
		lbl_p_keyboard_auxdevice.Enabled = chb_p_keyboard_aux.Checked
		cmb_p_keyboard_auxdevice.Enabled = chb_p_keyboard_aux.Checked
	End Sub

	Private Sub p_midi_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cmb_p_midi_mididevice.EditValueChanged, chb_p_midi_fluid_chorus.EditValueChanged, chb_p_midi_fluid_reverb.EditValueChanged, chb_p_midi_mt32_thread.EditValueChanged
		If Not {"mt32", "fluidsynth"}.Contains(TC.NZ(cmb_p_midi_mididevice.EditValue, "")) Then
			Me.tcl_p_midi.Visible = False
		Else
			Me.tcl_p_midi.Visible = True

			Me.tpg_p_midi_empty.PageVisible = False
			Me.tpg_p_midi_fluidsynth.PageVisible = False
			Me.tpg_p_midi_mt32.PageVisible = False

			'Workaround: garbaged visibility of tab page content
			Me.tpg_p_midi_empty.PageVisible = True
			Me.tpg_p_midi_empty.PageVisible = False

			If {"mt32"}.Contains(TC.NZ(cmb_p_midi_mididevice.EditValue, "")) Then
				Me.tpg_p_midi_mt32.PageVisible = True
				Me.tcl_p_midi.SelectedTabPage = Me.tpg_p_midi_mt32

				Me.lbl_p_midi_mt32_chunk.Enabled = TC.NZ(Me.chb_p_midi_mt32_thread.EditValue, False)
				Me.lbl_p_midi_mt32_chunk_ms.Enabled = TC.NZ(Me.chb_p_midi_mt32_thread.EditValue, False)
				Me.lbl_p_midi_mt32_prebuffer.Enabled = TC.NZ(Me.chb_p_midi_mt32_thread.EditValue, False)
				Me.lbl_p_midi_mt32_prebuffer_ms.Enabled = TC.NZ(Me.chb_p_midi_mt32_thread.EditValue, False)

				Me.spn_p_midi_mt32_chunk.Enabled = TC.NZ(Me.chb_p_midi_mt32_thread.EditValue, False)
				Me.spn_p_midi_mt32_prebuffer.Enabled = TC.NZ(Me.chb_p_midi_mt32_thread.EditValue, False)
			End If

			If {"fluidsynth"}.Contains(TC.NZ(cmb_p_midi_mididevice.EditValue, "")) Then
				Me.tpg_p_midi_fluidsynth.PageVisible = True
				Me.tcl_p_midi.SelectedTabPage = Me.tpg_p_midi_fluidsynth

				Me.lbl_p_midi_fluid_chorus_depth.Enabled = TC.NZ(Me.chb_p_midi_fluid_chorus.EditValue, False)
				Me.lbl_p_midi_fluid_chorus_depth_ms.Enabled = TC.NZ(Me.chb_p_midi_fluid_chorus.EditValue, False)
				Me.lbl_p_midi_fluid_chorus_level.Enabled = TC.NZ(Me.chb_p_midi_fluid_chorus.EditValue, False)
				Me.lbl_p_midi_fluid_chorus_number.Enabled = TC.NZ(Me.chb_p_midi_fluid_chorus.EditValue, False)
				Me.lbl_p_midi_fluid_chorus_speed.Enabled = TC.NZ(Me.chb_p_midi_fluid_chorus.EditValue, False)
				Me.lbl_p_midi_fluid_chorus_speed_hz.Enabled = TC.NZ(Me.chb_p_midi_fluid_chorus.EditValue, False)
				Me.lbl_p_midi_fluid_chorus_type.Enabled = TC.NZ(Me.chb_p_midi_fluid_chorus.EditValue, False)

				Me.spn_p_midi_fluid_chorus_depth.Enabled = TC.NZ(Me.chb_p_midi_fluid_chorus.EditValue, False)
				Me.spn_p_midi_fluid_chorus_level.Enabled = TC.NZ(Me.chb_p_midi_fluid_chorus.EditValue, False)
				Me.spn_p_midi_fluid_chorus_number.Enabled = TC.NZ(Me.chb_p_midi_fluid_chorus.EditValue, False)
				Me.spn_p_midi_fluid_chorus_speed.Enabled = TC.NZ(Me.chb_p_midi_fluid_chorus.EditValue, False)
				Me.cmb_p_midi_fluid_chorus_type.Enabled = TC.NZ(Me.chb_p_midi_fluid_chorus.EditValue, False)

				Me.lbl_p_midi_fluid_reverb_damping.Enabled = TC.NZ(Me.chb_p_midi_fluid_reverb.EditValue, False)
				Me.lbl_p_midi_fluid_reverb_level.Enabled = TC.NZ(Me.chb_p_midi_fluid_reverb.EditValue, False)
				Me.lbl_p_midi_fluid_reverb_roomsize.Enabled = TC.NZ(Me.chb_p_midi_fluid_reverb.EditValue, False)
				Me.lbl_p_midi_fluid_reverb_width.Enabled = TC.NZ(Me.chb_p_midi_fluid_reverb.EditValue, False)

				Me.spn_p_midi_fluid_reverb_damping.Enabled = TC.NZ(Me.chb_p_midi_fluid_reverb.EditValue, False)
				Me.spn_p_midi_fluid_reverb_level.Enabled = TC.NZ(Me.chb_p_midi_fluid_reverb.EditValue, False)
				Me.spn_p_midi_fluid_reverb_roomsize.Enabled = TC.NZ(Me.chb_p_midi_fluid_reverb.EditValue, False)
				Me.spn_p_midi_fluid_reverb_width.Enabled = TC.NZ(Me.chb_p_midi_fluid_reverb.EditValue, False)
			End If
		End If
	End Sub

	Private Sub cmb_p_sdl_output_EditValueChanged(sender As Object, e As EventArgs) Handles cmb_p_sdl_output.EditValueChanged
		Me.lbl_p_sdl_surfacenp_sharpness.Enabled = (TC.NZ(cmb_p_sdl_output.EditValue, "") = "surfacenp")
		Me.lbl_p_sdl_surfacenp_sharpness_percent.Enabled = (TC.NZ(cmb_p_sdl_output.EditValue, "") = "surfacenp")
		Me.spn_p_sdl_surfacenp_sharpness.Enabled = (TC.NZ(cmb_p_sdl_output.EditValue, "") = "surfacenp")
		Me.lbl_p_sdl_surface_collapse_dbl.Enabled = ({"surfacepp", "surfacenp", "surfacenb"}.Contains(TC.NZ(cmb_p_sdl_output.EditValue, "")))
		Me.chb_p_sdl_surface_collapse_dbl.Enabled = ({"surfacepp", "surfacenp", "surfacenb"}.Contains(TC.NZ(cmb_p_sdl_output.EditValue, "")))
	End Sub

	Private Sub chb_p_ne2000_ne2000_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chb_p_ne2000_ne2000.CheckedChanged
		Me.lbl_p_ne2000_macaddr.Enabled = Me.chb_p_ne2000_ne2000.Checked
		Me.lbl_p_ne2000_nicbase.Enabled = Me.chb_p_ne2000_ne2000.Checked
		Me.lbl_p_ne2000_nicirq.Enabled = Me.chb_p_ne2000_ne2000.Checked
		Me.lbl_p_ne2000_realnic.Enabled = Me.chb_p_ne2000_ne2000.Checked

		Me.txb_p_ne2000_macaddr.Enabled = Me.chb_p_ne2000_ne2000.Checked
		Me.txb_p_ne2000_nicbase.Enabled = Me.chb_p_ne2000_ne2000.Checked
		Me.txb_p_ne2000_realnic.Enabled = Me.chb_p_ne2000_ne2000.Checked
		Me.spn_p_ne2000_nicirq.Enabled = Me.chb_p_ne2000_ne2000.Checked
		Me.btn_p_ne2000_realnic.Enabled = Me.chb_p_ne2000_ne2000.Checked
	End Sub

	Private Sub btn_p_ne2000_realnic_Click(sender As System.Object, e As System.EventArgs) Handles btn_p_ne2000_realnic.Click
		Using frm As New frm_DOSBox_Choose_NIC
			If frm.ShowDialog(Me.ParentForm) = DialogResult.OK Then
				Me.txb_p_ne2000_realnic.EditValue = frm.Selected_NIC
			End If
		End Using
	End Sub

	Private Sub btn_p_ne2000_realnic_clear_Click(sender As System.Object, e As System.EventArgs) Handles btn_p_ne2000_realnic_clear.Click
		Me.txb_p_ne2000_realnic.EditValue = "list"
	End Sub

	Private Sub cmb_p_sblaster_oplemu_EditValueChanged(sender As Object, e As EventArgs) Handles cmb_p_sblaster_oplemu.EditValueChanged
		If TC.NZ(cmb_p_sblaster_oplemu.EditValue, "") = "nuked" Then
			Dim bChanged As Boolean = False
			If TC.NZ(cmb_mixer_rate.EditValue, "") <> "49716" Then
				Me.BS_DOSBox_Configs.Current("mixer-rate") = "49716"
				cmb_mixer_rate.EditValue = "49716"
				bChanged = True
			End If
			If TC.NZ(cmb_sblaster_oplrate.EditValue, "") <> "49716" Then
				Me.BS_DOSBox_Configs.Current("sblaster-oplrate") = "49716"
				cmb_sblaster_oplrate.EditValue = "49716"
				bChanged = True
			End If

			If bChanged Then
				MKDXHelper.MessageBox("The Nuked OPL3 Emulator has been chosen. It is absolutely recommended that the Mixer Rate as well as the OPL Rate are set to 49716. These changes have been applied now.", "Nuked OPL3 Emulator", MessageBoxButtons.OK, MessageBoxIcon.Information)
			End If
		End If
	End Sub

	Private Sub btn_p_midi_fluid_soundfont_Click(sender As Object, e As EventArgs) Handles btn_p_midi_fluid_soundfont.Click
		Dim filepath As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("Browse SoundFont file", "Soundfont File *.sf2|*.sf2", ParentForm:=Me.ParentForm)
		If Alphaleonis.Win32.Filesystem.File.Exists(filepath) Then
			Me.txb_p_midi_fluid_soundfont.EditValue = filepath
		End If
	End Sub

	Private Sub btn_p_midi_mt32_romdir_Click(sender As Object, e As EventArgs) Handles btn_p_midi_mt32_romdir.Click
		Dim dirpath As String = MKNetLib.cls_MKFileSupport.OpenFolderDialog("", True, Me.ParentForm)
		If Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
			Me.txb_p_midi_mt32_romdir.EditValue = dirpath
		End If
	End Sub

	Private Sub spn_p_midi_mt32_chunk_EditValueChanged(sender As Object, e As EventArgs) Handles spn_p_midi_mt32_chunk.EditValueChanged
		Me.spn_p_midi_mt32_prebuffer.Properties.MinValue = TC.NZ(Me.spn_p_midi_mt32_chunk.EditValue, 0L) + 1
	End Sub

	Private Sub btn_sdl_mapperfile_Click(sender As Object, e As EventArgs) Handles btn_sdl_mapperfile.Click
		Dim filepath As String = MKNetLib.cls_MKFileSupport.SaveFileDialog("Browse Mapper File", "DOSBox Mapper File *.map|*.map", DefaultExt:=".map", ParentForm:=Me.ParentForm, PromptOverwrite:=False)
		If filepath <> "" Then
			Me.txb_sdl_mapperfile.EditValue = filepath
		End If
	End Sub

	Private Sub btn_dosbox_language_Click(sender As Object, e As EventArgs) Handles btn_dosbox_language.Click
		Dim filepath As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("Browse Language File", ParentForm:=Me.ParentForm)
		If Alphaleonis.Win32.Filesystem.File.Exists(filepath) Then
			Me.txb_dosbox_language.EditValue = filepath
		End If
	End Sub
End Class