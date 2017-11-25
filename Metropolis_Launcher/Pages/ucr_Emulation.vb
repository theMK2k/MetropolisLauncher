Imports System.ComponentModel
Imports System.Net
Imports DevExpress.Utils.Filtering
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base

Public Class ucr_Emulation
	Public Enum enm_ExtrasMode
		User = 0
		Moby = 1
	End Enum

	Public Class cls_Emu_Game_ProcInfo
		Public id_Emu_Games As Integer
		Public Snapshot_Directory As String
		Public Platform As cls_Globals.enm_Moby_Platforms

		Public Sub New(ByVal id_Emu_Games As Integer, ByVal Snapshot_Directory As String, ByVal Platform As cls_Globals.enm_Moby_Platforms)
			Me.id_Emu_Games = id_Emu_Games
			Me.Snapshot_Directory = Snapshot_Directory
			Me.Platform = Platform
		End Sub
	End Class

	Public Class cls_Moby_Download_Info
		Public URL As String = ""
		Public Filepath As String = ""
		Public Description As String = ""
		Public ApplyExtraDescription As Boolean = False
		Public Payload As Object = Nothing

		Public Sub New(ByVal url As String, filepath As String, Optional ByVal description As String = "", Optional ByVal applyDescription As Boolean = False, Optional ByRef Payload As Object = Nothing)
			Me.URL = url
			Me.Filepath = filepath
			Me.Description = description
			Me.ApplyExtraDescription = applyDescription
			Me.Payload = Payload
		End Sub
	End Class

	Public Event E_Hide()
	Public Event E_Show()

	Private Extras_Mode As enm_ExtrasMode = enm_ExtrasMode.User

	Private Moby_Download_Info As cls_Moby_Download_Info

	'User-provided / Emumovies Extras from extras directory (extras/emulation/$platform)
	Private ExtraType As Object = Nothing 'Name of the extra (Image in upper left corner), e.g. "box", "title", "snap" etc.
	Private ExtraNum As Integer = 0 'Number of image currently used (first image is always "name of game (US) (BLABLA).png", next image is "name of game (US) (BLABLA)_002 [image2].png" ...)
	Private NoExtraFound As Boolean = False

	'Moby Extras
	Private Moby_ExtraNum As Integer = 0

	Private WithEvents Moby_Extras_Downloader As System.Net.WebClient = New System.Net.WebClient

	Private _sem_FilterChange As Boolean = False

	Private _Slideshow As Boolean = False
	Private _Slideshow_Delay As Integer = 1

	Private _Initializing As Boolean = True
	Private _Platform_Changing As Boolean = False

	Private dict_Proc_EmuGames As New Dictionary(Of Integer, cls_Emu_Game_ProcInfo)

	Private _al_StatsChanges As New ArrayList
	Private _al_Screenshots_EmuGames As New ArrayList
	Private _al_Screenshots As New ArrayList

	'Watching the clipboard could be viewed as a security issue - temp. disabled
	'Private WithEvents _ClipboardWatcher As MKNetLib.cls_MKClipboardWatcher = MKNetLib.cls_MKClipboardWatcher.ClipboardWatcher

	Private _bbi_Show_Similarity_Feature_Columns_Caption As String = ""

	Public Sub New()
		InitializeComponent()

#If DEBUG Then
		colid_Emu_Games.OptionsColumn.ShowInCustomizationForm = True
		colid_Moby_Games.OptionsColumn.ShowInCustomizationForm = True
		colid_Moby_Platforms.OptionsColumn.ShowInCustomizationForm = True
		colid_Moby_Releases.OptionsColumn.ShowInCustomizationForm = True
		colid_DOSBox_Configs.OptionsColumn.ShowInCustomizationForm = True
		colid_DOSBox_Configs_Template.OptionsColumn.ShowInCustomizationForm = True
#End If

		Me._bbi_Show_Similarity_Feature_Columns_Caption = bbi_Show_Similarity_Feature_Columns.Caption

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Ensure_Moby_Platform_Caches(tran)
			Me.DS_ML.Fill_src_ucr_Emulators_Platforms(tran, Me.DS_ML.src_ucr_Emulation_Platforms)
			Me.DS_ML.Fill_tbl_FilterSets(tran, Me.DS_ML.tbl_FilterSets, Metropolis_Launcher.DS_ML.enm_FilterSetTypes.Emulation)
			DS_ML.Fill_src_ucr_Emulation_cmb_Groups(tran, Me.DS_ML.src_ucr_Emulation_cmb_Groups)
			DS_ML.Fill_src_ucr_Emulation_cmb_Staff(tran, Me.DS_ML.src_ucr_Emulation_cmb_Staff)
			DS_ML.Fill_src_ucr_Emulation_cmb_Similarity_Calculation_Results(tran, Me.DS_ML.src_ucr_Emulation_cmb_Similarity_Calculation_Results)
			Me.cmb_Filterset.EditValue = CLng(0)
			Me.cmb_Groups.EditValue = CLng(0)
			Me.cmb_Staff.EditValue = CLng(0)
			'Me.DS_ML.Fill_src_ucr_Emulation_Games(tran, Me.DS_ML.src_ucr_Emulation_Games)

			_Slideshow = TC.NZ(cls_Settings.GetSetting("Emu_Slideshow", cls_Settings.enm_Settingmodes.Per_User, tran), "0") = "1"
			_Slideshow_Delay = CInt(TC.NZ(cls_Settings.GetSetting("Emu_Slideshow_Delay", cls_Settings.enm_Settingmodes.Per_User, tran), "1"))

			tmr_ImageUpdate.Interval = _Slideshow_Delay * 1000

			tran.Commit()
		End Using

		barmng.SetPopupContextMenu(grd_Emu_Games, popmnu_Emu)
		barmng.SetPopupContextMenu(grd_Game_Groups, popmnu_GameGroups)
		barmng.SetPopupContextMenu(grd_Staff, popmnu_Staff)
		barmng.SetPopupContextMenu(pic_Game, popmnu_Extras)
		barmng.SetPopupContextMenu(grd_Statistics, popmnu_Statistics)

		cmb_Platform.EditValue = TC.NZ(cls_Settings.GetSetting("ucr_Emulation-Platform", cls_Settings.enm_Settingmodes.Per_User), CLng(-1))
		cmb_Groups.EditValue = TC.NZ(cls_Settings.GetSetting("ucr_Emulation-Group", cls_Settings.enm_Settingmodes.Per_User), CLng(0))
		cmb_Staff.EditValue = TC.NZ(cls_Settings.GetSetting("ucr_Emulation-Developer", cls_Settings.enm_Settingmodes.Per_User), CLng(0))

		Apply_cmb_Similarity_Calculation_Results_Buttons_Enabled()

		_Initializing = False

		Call_J2K() 'Just call J2K with the Default Config

		tmr.Start()
	End Sub

	Public Class cls_Romfiledata
#Region "Member"
		Private _Fullpath As String = ""
		Private _IsValid As String
#End Region

#Region "Properties"
		Public Property Fullpath() As String
			Get
				Return _Fullpath
			End Get
			Set(ByVal value As String)
				_Fullpath = value
			End Set
		End Property

		Public ReadOnly Property DirName() As String
			Get
				Return Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(_Fullpath)
			End Get
		End Property

		Public ReadOnly Property FileName() As String
			Get
				Return Alphaleonis.Win32.Filesystem.Path.GetFileName(_Fullpath)
			End Get
		End Property

		Public ReadOnly Property IsValid() As Boolean
			Get
				Return _IsValid
			End Get
		End Property
#End Region

		Public Sub New(ByVal File As String, ByVal InnerFile As String, ByVal TempDir As String, Optional ByVal DecompressIfPossible As Boolean = True, Optional ByVal ShowError As Boolean = True)
			Dim bIsDirectory As Boolean = False
			If File.Contains(":WorkDir:") Then
				bIsDirectory = True

				Dim fullpath = File.Replace(":WorkDir:", "")


				If Not Alphaleonis.Win32.Filesystem.Directory.Exists(fullpath) Then
					If ShowError Then
						MKDXHelper.MessageBox("The directory has not been found: " & fullpath, "Directory not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					End If

					Me._IsValid = False
					Return
				End If

				Me._Fullpath = fullpath
				Me._IsValid = True
			Else
				If Not Alphaleonis.Win32.Filesystem.File.Exists(File) Then
					If ShowError Then
						MKDXHelper.MessageBox("The game has not been found: " & File, "Game not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					End If

					Me._IsValid = False
					Return
				End If

				Me._Fullpath = File

				If DecompressIfPossible AndAlso InnerFile <> "" Then
					If Me.FileName.ToLower <> InnerFile.ToLower Then
						Try
							'We have an archive with a specific inner file
							Dim archive As SharpCompress.Archive.IArchive = SharpCompress.Archive.ArchiveFactory.Open(File)

							If archive IsNot Nothing Then
								For Each entry As SharpCompress.Archive.IArchiveEntry In archive.Entries
									If Not entry.IsDirectory Then
										If Alphaleonis.Win32.Filesystem.Path.GetFileName(entry.FilePath).ToLower = InnerFile.ToLower Then
											'We found the file we want to extract
											Dim sOutFile As String = TempDir & "\" & Alphaleonis.Win32.Filesystem.Path.GetFileName(entry.FilePath)
											If Not Alphaleonis.Win32.Filesystem.File.Exists(sOutFile) Then
												Using sw As New IO.StreamWriter(sOutFile)
													GC.SuppressFinalize(sw.BaseStream)
													entry.WriteTo(sw.BaseStream)
													'sw.BaseStream.Close()
													sw.Close()
												End Using
											End If

											Me._Fullpath = sOutFile
											Exit For
										End If
									End If
								Next
							End If

							If Not Alphaleonis.Win32.Filesystem.File.Exists(Me.Fullpath) Then
								Dim res As DialogResult = MKDXHelper.MessageBox("The expected inner file " & InnerFile & " could not be found in " & File & ". Please have a look in the Rom Manager and check the archive.", "Inner file not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								Me._IsValid = False
								Return
							End If
						Catch ex As Exception
							MKDXHelper.ExceptionMessageBox(ex, "There has been an error on decompressing " & File & " with the expected inner file " & InnerFile & ". Please have a look in the Rom Manager and check the archive." & ControlChars.CrLf & ControlChars.CrLf & "The error was: ", "Error on decompression")
							Me._IsValid = False
							Return
						End Try
					End If
				End If

				Me._IsValid = True
			End If
		End Sub
	End Class

	Private Function isDOSBoxPatchActivated(ByRef tbl_patches As DS_ML.src_frm_Emulators_DOSBox_PatchesDataTable, ByVal Patchname As String)
		For Each row As DataRow In tbl_patches
			If row("Identifier") = Patchname Then
				Return TC.NZ(row("Activated"), False)
			End If
		Next

		Throw New NotImplementedException("The DOSBox patch '" & Patchname & "' cannot be found")
		Return False
	End Function

	''' <summary>
	''' Prepare the DOSBox config for launching the game
	''' </summary>
	''' <returns>DOSBox startup parameters (incl. temp. DOSBox config), else empty String</returns>
	''' <remarks></remarks>
	Private Function Prepare_DOSBox(ByVal row_Emulators As DataRow, ByVal row_Emu_Game As DataRow, Optional ByVal id_Rombase_DOSBox_Exe_Types As Integer = 0) As String
		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			Try
				Dim tbl_patches As New DS_ML.src_frm_Emulators_DOSBox_PatchesDataTable
				DS_ML.Fill_src_frm_Emulators_DOSBox_Patches(tran, tbl_patches, row_Emulators("id_Emulators"))

				'Get the config for the game
				Dim dt_DOSBox_Config As DS_ML.tbl_DOSBox_ConfigsDataTable = Nothing
				DS_ML.Fill_tbl_DOSBox_Configs(tran, dt_DOSBox_Config, row_Emu_Game("id_Emu_Games"))

				If dt_DOSBox_Config Is Nothing OrElse dt_DOSBox_Config.Rows.Count <> 1 Then
					MKDXHelper.MessageBox("There has been an error while creating the DOSBox config (Errorcode 1).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					tran.Commit()
					Return ""
				End If

				Dim row_DOSBox_Config As DataRow = dt_DOSBox_Config.Rows(0)

				Dim sb_DOSBox_Config As New System.Text.StringBuilder()

				Dim sb_sdl As New System.Text.StringBuilder("[sdl]" & ControlChars.CrLf)
				Dim sb_dosbox As New System.Text.StringBuilder("[dosbox]" & ControlChars.CrLf)
				Dim sb_render As New System.Text.StringBuilder("[render]" & ControlChars.CrLf)
				Dim sb_cpu As New System.Text.StringBuilder("[cpu]" & ControlChars.CrLf)
				Dim sb_mixer As New System.Text.StringBuilder("[mixer]" & ControlChars.CrLf)
				Dim sb_midi As New System.Text.StringBuilder("[midi]" & ControlChars.CrLf)
				Dim sb_sblaster As New System.Text.StringBuilder("[sblaster]" & ControlChars.CrLf)
				Dim sb_gus As New System.Text.StringBuilder("[gus]" & ControlChars.CrLf)
				Dim sb_speaker As New System.Text.StringBuilder("[speaker]" & ControlChars.CrLf)
				Dim sb_joystick As New System.Text.StringBuilder("[joystick]" & ControlChars.CrLf)
				Dim sb_serial As New System.Text.StringBuilder("[serial]" & ControlChars.CrLf)
				Dim sb_dos As New System.Text.StringBuilder("[dos]" & ControlChars.CrLf)
				Dim sb_ipx As New System.Text.StringBuilder("[ipx]" & ControlChars.CrLf)
				Dim sb_vsync As New System.Text.StringBuilder()
				Dim sb_pci As New System.Text.StringBuilder()
				Dim sb_glide As New System.Text.StringBuilder()
				Dim sb_keyboard As New System.Text.StringBuilder()
				Dim sb_ne2000 As New System.Text.StringBuilder()
				Dim sb_autoexec As New System.Text.StringBuilder("[autoexec]" & ControlChars.CrLf)

				'[sdl]
				sb_sdl.AppendLine(IIf(TC.NZ(row_DOSBox_Config("sdl-fullscreen"), False), "fullscreen=true", "fullscreen=false"))
				sb_sdl.AppendLine(IIf(TC.NZ(row_DOSBox_Config("sdl-fulldouble"), False), "fulldouble=true", "fulldouble=false"))
				sb_sdl.AppendLine("fullresolution=" & TC.NZ(row_DOSBox_Config("sdl-fullresolution"), "original"))
				sb_sdl.AppendLine("windowresolution=" & TC.NZ(row_DOSBox_Config("sdl-windowresolution"), "original"))

				Dim sdl_output As String = TC.NZ(row_DOSBox_Config("sdl-output"), "surface")
				Dim p_sdl_output As String = TC.NZ(row_DOSBox_Config("p_sdl_output"), "")
				If p_sdl_output.Length > 0 Then
					If (p_sdl_output = "direct3d" AndAlso isDOSBoxPatchActivated(tbl_patches, "direct3d_with_pixelshader")) _
					 OrElse (p_sdl_output = "openglhq" AndAlso isDOSBoxPatchActivated(tbl_patches, "hq2x_openglhq")) _
					 OrElse ({"surfacepp", "surfacenp", "surfacepb"}.Contains(p_sdl_output) AndAlso isDOSBoxPatchActivated(tbl_patches, "pixelperfect")) _
					 Then
						sdl_output = p_sdl_output
					End If
				End If
				sb_sdl.AppendLine("output=" & sdl_output)

				sb_sdl.AppendLine(IIf(TC.NZ(row_DOSBox_Config("sdl-autolock"), True), "autolock=true", "autolock=false"))
				sb_sdl.AppendLine("sensitivity=" & TC.NZ(row_DOSBox_Config("sdl-sensitivity"), 100).ToString)
				sb_sdl.AppendLine(IIf(TC.NZ(row_DOSBox_Config("sdl-waitonerror"), True), "waitonerror=true", "waitonerror=false"))
				sb_sdl.AppendLine("priority=" & TC.NZ(row_DOSBox_Config("sdl-priority_1"), "higher") & "," & TC.NZ(row_DOSBox_Config("sdl-priority_2"), "normal"))
				sb_sdl.AppendLine("mapperfile=" & TC.NZ(row_DOSBox_Config("sdl-mapperfile"), "mapper.map"))
				sb_sdl.AppendLine(IIf(TC.NZ(row_DOSBox_Config("sdl-usescancodes"), True), "usescancodes=true", "usescancodes=false"))

				If isDOSBoxPatchActivated(tbl_patches, "direct3d_with_pixelshader") Then
					sb_sdl.AppendLine("pixelshader=" & TC.NZ(row_DOSBox_Config("p_sdl_pixelshader"), "none") & IIf(TC.NZ(row_DOSBox_Config("p_sdl_pixelshader_forced"), False), " forced", ""))
				End If

				If isDOSBoxPatchActivated(tbl_patches, "pixelperfect") Then
					sb_sdl.AppendLine("surfacenp-sharpness=" & TC.NZ(row_DOSBox_Config("p_sdl_surfacenp-sharpness"), 50).ToString) 'integer
					sb_sdl.AppendLine("surface-collapse-dbl=" & IIf(TC.NZ(row_DOSBox_Config("p_sdl_surface-collapse-dbl"), False), "true", "false"))
				End If

				'[dosbox]
				sb_dosbox.AppendLine("language=" & TC.NZ(row_DOSBox_Config("dosbox-language"), ""))
				sb_dosbox.AppendLine("machine=" & TC.NZ(row_DOSBox_Config("dosbox-machine"), "svga_s3"))

				sb_dosbox.AppendLine("memsize=" & TC.NZ(row_DOSBox_Config("dosbox-memsize"), 16).ToString)
				If isDOSBoxPatchActivated(tbl_patches, "memsizekb") Then
					sb_dosbox.AppendLine("memsizekb=" & TC.NZ(row_DOSBox_Config("p_dosbox_memsizekb"), 0).ToString)
				End If

				If isDOSBoxPatchActivated(tbl_patches, "pit_timer") Then sb_dosbox.AppendLine("pit hack=" & TC.NZ(row_DOSBox_Config("p_dosbox_pit_hack"), ""))

				If isDOSBoxPatchActivated(tbl_patches, "vmemsize") Then sb_dosbox.AppendLine("vmemsize=" & TC.NZ(row_DOSBox_Config("p_dosbox_vmemsize"), 4).ToString)
				If isDOSBoxPatchActivated(tbl_patches, "forcerate") Then sb_dosbox.AppendLine("forcerate=" & TC.NZ(row_DOSBox_Config("p_dosbox_forcerate"), ""))


				'[render]
				sb_render.AppendLine("frameskip=" & TC.NZ(row_DOSBox_Config("render-frameskip"), 0).ToString)
				sb_render.AppendLine(IIf(TC.NZ(row_DOSBox_Config("render-aspect"), True), "aspect=true", "aspect=false"))

				Dim render_scaler As String = TC.NZ(row_DOSBox_Config("render-scaler"), "normal2x")
				Dim p_render_scaler As String = TC.NZ(row_DOSBox_Config("p_render_scaler"), "")
				If p_render_scaler.Length > 0 AndAlso isDOSBoxPatchActivated(tbl_patches, "scaler_" & p_render_scaler) Then
					render_scaler = p_render_scaler
				End If
				sb_render.AppendLine("scaler=" & render_scaler & IIf(TC.NZ(row_DOSBox_Config("render-scaler_forced"), False), " forced", ""))

				If isDOSBoxPatchActivated(tbl_patches, "autofit") Then
					sb_render.AppendLine("autofit=" & IIf(TC.NZ(row_DOSBox_Config("p_render_autofit"), True), "true", "false"))
				End If

				'[cpu]
				sb_cpu.AppendLine("core=" & TC.NZ(row_DOSBox_Config("cpu-core"), "auto"))

				Dim cpu_cputype As String = TC.NZ(row_DOSBox_Config("cpu-cputype"), "auto")
				Dim p_cpu_cputype As String = TC.NZ(row_DOSBox_Config("p_cpu_cputype"), "")
				If p_cpu_cputype.Length > 0 AndAlso isDOSBoxPatchActivated(tbl_patches, "cputype_" & p_cpu_cputype) Then
					cpu_cputype = p_cpu_cputype
				End If
				sb_cpu.AppendLine("cputype=" & cpu_cputype)

				sb_cpu.AppendLine("cycles=" & TC.NZ(row_DOSBox_Config("cpu-cycles"), "auto"))
				sb_cpu.AppendLine("cycleup=" & TC.NZ(row_DOSBox_Config("cpu-cycleup"), 200).ToString)
				sb_cpu.AppendLine("cycledown=" & TC.NZ(row_DOSBox_Config("cpu-cycledown"), 200).ToString)

				'[mixer]
				sb_mixer.AppendLine(IIf(TC.NZ(row_DOSBox_Config("mixer-nosound"), False), "nosound=true", "nosound=false"))
				sb_mixer.AppendLine("rate=" & TC.NZ(row_DOSBox_Config("mixer-rate"), "44100"))
				sb_mixer.AppendLine("blocksize=" & TC.NZ(row_DOSBox_Config("mixer-blocksize"), "1024"))
				sb_mixer.AppendLine("prebuffer=" & TC.NZ(row_DOSBox_Config("mixer-prebuffer"), 20).ToString)
				If isDOSBoxPatchActivated(tbl_patches, "swapstereo") Then
					sb_mixer.AppendLine("swapstereo=" & IIf(TC.NZ(row_DOSBox_Config("p_mixer_swapstereo"), False), "true", "false"))
				End If

				'[midi]
				sb_midi.AppendLine("mpu401=" & TC.NZ(row_DOSBox_Config("midi-mpu401"), "intelligent"))

				Dim midi_mididevice As String = TC.NZ(row_DOSBox_Config("midi-mididevice"), "default")
				Dim p_midi_mididevice As String = TC.NZ(row_DOSBox_Config("p_midi_mididevice"), "default")
				If p_midi_mididevice.Length > 0 Then
					If (p_midi_mididevice = "mt32" AndAlso isDOSBoxPatchActivated(tbl_patches, "mt32")) _
					 OrElse (p_midi_mididevice = "synth" AndAlso isDOSBoxPatchActivated(tbl_patches, "mididevice_synth")) _
					 OrElse (p_midi_mididevice = "timidity" AndAlso isDOSBoxPatchActivated(tbl_patches, "mididevice_timidity")) _
					 OrElse (p_midi_mididevice = "fluidsynth" AndAlso isDOSBoxPatchActivated(tbl_patches, "mididevice_fluidsynth")) _
					 Then
						midi_mididevice = p_midi_mididevice
					End If
				End If
				sb_midi.AppendLine("mididevice=" & midi_mididevice)

				sb_midi.AppendLine("midiconfig=" & TC.NZ(row_DOSBox_Config("midi-midiconfig"), ""))

				If isDOSBoxPatchActivated(tbl_patches, "mt32") Then
					sb_midi.AppendLine("mt32.reverse.stereo=" & IIf(TC.NZ(row_DOSBox_Config("p_midi_mt32_reverse_stereo"), False), "true", "false"))  'on/off
					sb_midi.AppendLine("mt32.verbose=" & IIf(TC.NZ(row_DOSBox_Config("p_midi_mt32_verbose"), False), "true", "false"))                'on/off
					sb_midi.AppendLine("mt32.thread=" & IIf(TC.NZ(row_DOSBox_Config("p_midi_mt32_thread"), False), "true", "false"))                  'on/off
					sb_midi.AppendLine("mt32.dac=" & TC.NZ(row_DOSBox_Config("p_midi_mt32_dac"), "auto"))
					sb_midi.AppendLine("mt32.reverb.mode=" & TC.NZ(row_DOSBox_Config("p_midi_mt32_reverb_mode"), "auto").ToString)
					sb_midi.AppendLine("mt32.reverb.time=" & TC.NZ(row_DOSBox_Config("p_midi_mt32_reverb_time"), 5).ToString)
					sb_midi.AppendLine("mt32.reverb.level=" & TC.NZ(row_DOSBox_Config("p_midi_mt32_reverb_level"), 3).ToString)
					sb_midi.AppendLine("mt32.partials=" & TC.NZ(row_DOSBox_Config("p_midi_mt32_partials"), 32).ToString)

					sb_midi.AppendLine("mt32.romdir=" & TC.NZ(row_DOSBox_Config("p_midi_mt32_romdir"), "").ToString)
					sb_midi.AppendLine("mt32.chunk=" & TC.NZ(row_DOSBox_Config("p_midi_mt32_chunk"), 16).ToString)
					sb_midi.AppendLine("mt32.prebuffer=" & TC.NZ(row_DOSBox_Config("p_midi_mt32_prebuffer"), 32).ToString)
					sb_midi.AppendLine("mt32.analog=" & TC.NZ(row_DOSBox_Config("p_midi_mt32_analog"), 2).ToString)
					sb_midi.AppendLine("mt32.rate=" & TC.NZ(row_DOSBox_Config("p_midi_mt32_rate"), 44100).ToString)
					sb_midi.AppendLine("mt32.src.quality=" & TC.NZ(row_DOSBox_Config("p_midi_mt32_src_quality"), 2).ToString)
					sb_midi.AppendLine("mt32.niceampramp=" & IIf(TC.NZ(row_DOSBox_Config("p_midi_mt32_niceampramp"), True), "true", "false"))
				End If

				If isDOSBoxPatchActivated(tbl_patches, "mididevice_fluidsynth") Then
					sb_midi.AppendLine("fluid.soundfont=" & TC.NZ(row_DOSBox_Config("p_midi_fluid_soundfont"), ""))
					sb_midi.AppendLine("fluid.samplerate=" & TC.NZ(row_DOSBox_Config("p_midi_fluid_samplerate"), 48000).ToString)
					sb_midi.AppendLine("fluid.gain=" & TC.NZ(MKNetLib.cls_MKStringSupport.Clean_Left(row_DOSBox_Config("p_midi_fluid_gain"), 0.6).ToString.Replace(",", "."), "0"))
					sb_midi.AppendLine("fluid.polyphony=" & TC.NZ(row_DOSBox_Config("p_midi_fluid_polyphony"), 256).ToString)
					sb_midi.AppendLine("fluid.cores=" & TC.NZ(row_DOSBox_Config("p_midi_fluid_cores"), "default"))
					sb_midi.AppendLine("fluid.periods=" & TC.NZ(row_DOSBox_Config("p_midi_fluid_periods"), 8).ToString)
					sb_midi.AppendLine("fluid.periodsize=" & TC.NZ(row_DOSBox_Config("p_midi_fluid_periodsize"), 512).ToString)

					sb_midi.AppendLine("fluid.reverb=" & IIf(TC.NZ(row_DOSBox_Config("p_midi_fluid_reverb"), True), "yes", "no"))
					sb_midi.AppendLine("fluid.reverb,roomsize=" & TC.NZ(MKNetLib.cls_MKStringSupport.Clean_Left(row_DOSBox_Config("p_midi_fluid_reverb_roomsize"), 0.61).ToString.Replace(",", "."), "0"))
					sb_midi.AppendLine("fluid.reverb.damping=" & TC.NZ(MKNetLib.cls_MKStringSupport.Clean_Left(row_DOSBox_Config("p_midi_fluid_reverb_damping"), 0.23).ToString.Replace(",", "."), "0"))
					sb_midi.AppendLine("fluid.reverb.width=" & TC.NZ(MKNetLib.cls_MKStringSupport.Clean_Left(row_DOSBox_Config("p_midi_fluid_reverb_width"), 0.76).ToString.Replace(",", "."), "0"))
					sb_midi.AppendLine("fluid.reverb.level=" & TC.NZ(MKNetLib.cls_MKStringSupport.Clean_Left(row_DOSBox_Config("p_midi_fluid_reverb_level"), 0.57).ToString.Replace(",", "."), "0"))

					sb_midi.AppendLine("fluid.chorus=" & IIf(TC.NZ(row_DOSBox_Config("p_midi_fluid_chorus"), True), "yes", "no"))
					sb_midi.AppendLine("fluid.chorus.number=" & TC.NZ(row_DOSBox_Config("p_midi_fluid_chorus_number"), 3).ToString)
					sb_midi.AppendLine("fluid.chorus.level=" & TC.NZ(MKNetLib.cls_MKStringSupport.Clean_Left(row_DOSBox_Config("p_midi_fluid_chorus_level"), 1.2).ToString.Replace(",", "."), "0"))
					sb_midi.AppendLine("fluid.chorus.speed=" & TC.NZ(MKNetLib.cls_MKStringSupport.Clean_Left(row_DOSBox_Config("p_midi_fluid_chorus_speed"), 0.3).ToString.Replace(",", "."), "0"))
					sb_midi.AppendLine("fluid.chorus.depth=" & TC.NZ(MKNetLib.cls_MKStringSupport.Clean_Left(row_DOSBox_Config("p_midi_fluid_chorus_depth"), 8.0).ToString.Replace(",", "."), "0"))
					sb_midi.AppendLine("fluid.chorus.type=" & TC.NZ(row_DOSBox_Config("p_midi_fluid_chorus_type"), 0).ToString)
				End If

				'[sblaster]
				sb_sblaster.AppendLine("sbtype=" & TC.NZ(row_DOSBox_Config("sblaster-sbtype"), "sb16"))
				sb_sblaster.AppendLine("sbbase=" & TC.NZ(row_DOSBox_Config("sblaster-sbbase"), "220"))
				sb_sblaster.AppendLine("irq=" & TC.NZ(row_DOSBox_Config("sblaster-irq"), "7"))
				sb_sblaster.AppendLine("dma=" & TC.NZ(row_DOSBox_Config("sblaster-dma"), "1"))
				sb_sblaster.AppendLine("hdma=" & TC.NZ(row_DOSBox_Config("sblaster-hdma"), "5"))
				sb_sblaster.AppendLine(IIf(TC.NZ(row_DOSBox_Config("sblaster-sbmixer"), True), "sbmixer=true", "sbmixer=false"))

				Dim sblaster_oplmode As String = TC.NZ(row_DOSBox_Config("sblaster-oplmode"), "auto")
				Dim p_sblaster_oplmode As String = TC.NZ(row_DOSBox_Config("p_sblaster_oplmode"), "")
				If p_sblaster_oplmode.Length > 0 AndAlso isDOSBoxPatchActivated(tbl_patches, "opl_cms_passthrough") Then
					sblaster_oplmode = p_sblaster_oplmode
				End If
				sb_sblaster.AppendLine("oplmode=" & sblaster_oplmode)

				Dim oplemu As String = TC.NZ(row_DOSBox_Config("sblaster-oplemu"), "default")
				If isDOSBoxPatchActivated(tbl_patches, "sblaster_oplemu_nuked") Then
					If Not TC.IsNullNothingOrEmpty(row_DOSBox_Config("p_sblaster_oplemu")) Then
						oplemu = row_DOSBox_Config("p_sblaster_oplemu")
					End If
				End If
				sb_sblaster.AppendLine("oplemu=" & oplemu)

				sb_sblaster.AppendLine("oplrate=" & TC.NZ(row_DOSBox_Config("sblaster-oplrate"), "44100"))

				If isDOSBoxPatchActivated(tbl_patches, "opl_cms_passthrough") Then
					sb_sblaster.AppendLine("hardwarebase=" & TC.NZ(row_DOSBox_Config("p_sblaster_hardwarebase"), "220"))
				End If

				If isDOSBoxPatchActivated(tbl_patches, "goldplay") Then
					sb_sblaster.AppendLine("goldplay=" & IIf(TC.NZ(row_DOSBox_Config("p_sblaster_goldplay"), False), "true", "false"))
				End If

				'[gus]
				sb_gus.AppendLine("gus=" & IIf(TC.NZ(row_DOSBox_Config("gus-gus"), False), "true", "false"))
				sb_gus.AppendLine("gusrate=" & TC.NZ(row_DOSBox_Config("gus-gusrate"), "44100"))
				sb_gus.AppendLine("gusbase=" & TC.NZ(row_DOSBox_Config("gus-gusrate"), "240"))
				sb_gus.AppendLine("gusirq=" & TC.NZ(row_DOSBox_Config("gus-gusirq"), "5"))
				sb_gus.AppendLine("gusdma=" & TC.NZ(row_DOSBox_Config("gus-gusdma"), "3"))
				sb_gus.AppendLine("ultradir=" & TC.NZ(row_DOSBox_Config("gus-ultradir"), ""))

				'[speaker]
				sb_speaker.AppendLine(IIf(TC.NZ(row_DOSBox_Config("speaker-pcspeaker"), True), "pcspeaker=true", "pcspeaker=false"))
				sb_speaker.AppendLine("pcrate=" & TC.NZ(row_DOSBox_Config("speaker-pcrate"), "44100"))
				sb_speaker.AppendLine("tandy=" & TC.NZ(row_DOSBox_Config("speaker-tandy"), "auto"))
				sb_speaker.AppendLine("tandyrate=" & TC.NZ(row_DOSBox_Config("speaker-tandyrate"), "44100"))
				sb_speaker.AppendLine(IIf(TC.NZ(row_DOSBox_Config("speaker-disney"), True), "disney=true", "disney=false"))

				'[joystick]
				sb_joystick.AppendLine("joysticktype=" & TC.NZ(row_DOSBox_Config("joystick-joysticktype"), "auto"))
				sb_joystick.AppendLine(IIf(TC.NZ(row_DOSBox_Config("joystick-timed"), True), "timed=true", "timed=false"))
				sb_joystick.AppendLine(IIf(TC.NZ(row_DOSBox_Config("joystick-autofire"), True), "autofire=true", "autofire=false"))
				sb_joystick.AppendLine(IIf(TC.NZ(row_DOSBox_Config("joystick-swap34"), True), "swap34=true", "swap34=false"))
				sb_joystick.AppendLine(IIf(TC.NZ(row_DOSBox_Config("joystick-buttonwrap"), True), "buttonwrap=true", "buttonwrap=false"))

				'TODO: [serial]
				sb_serial.AppendLine("serial1=" & TC.NZ(row_DOSBox_Config("serial-serial1"), "dummy"))
				sb_serial.AppendLine("serial2=" & TC.NZ(row_DOSBox_Config("serial-serial2"), "dummy"))
				sb_serial.AppendLine("serial3=" & TC.NZ(row_DOSBox_Config("serial-serial3"), "disabled"))
				sb_serial.AppendLine("serial4=" & TC.NZ(row_DOSBox_Config("serial-serial4"), "disabled"))

				'[dos]
				sb_dos.AppendLine(IIf(TC.NZ(row_DOSBox_Config("dos-xms"), True), "xms=true", "xms=false"))
				sb_dos.AppendLine(IIf(TC.NZ(row_DOSBox_Config("dos-ems"), True), "ems=true", "ems=false"))
				sb_dos.AppendLine(IIf(TC.NZ(row_DOSBox_Config("dos-umb"), True), "umb=true", "umb=false"))
				sb_dos.AppendLine("keyboardlayout=" & TC.NZ(row_DOSBox_Config("dos-keyboardlayout"), "auto"))

				'[ipx]
				sb_ipx.AppendLine(IIf(TC.NZ(row_DOSBox_Config("ipx-ipx"), True), "ipx=true", "ipx=false"))

				'[vsync] - if patch is active
				If isDOSBoxPatchActivated(tbl_patches, "vsync") Then
					sb_vsync.AppendLine("[vsync]")
					sb_vsync.AppendLine("vsyncmode=" & TC.NZ(row_DOSBox_Config("p_vsync_vsyncmode"), "off"))
					sb_vsync.AppendLine("vsyncrate=" & TC.NZ(row_DOSBox_Config("p_vsync_vsyncrate"), 75).ToString)
				End If

				'[pci] - if patches are active
				If isDOSBoxPatchActivated(tbl_patches, "voodoo") Then
					sb_pci.AppendLine("[pci]")
					sb_pci.AppendLine("voodoo=" & TC.NZ(row_DOSBox_Config("p_voodoo"), "auto"))
					sb_pci.AppendLine("voodoomem=" & TC.NZ(row_DOSBox_Config("p_voodoo_voodoomem"), "standard"))
				End If

				'[glide] - if patch is active
				If isDOSBoxPatchActivated(tbl_patches, "glide") Then
					sb_glide.AppendLine("[glide]")
					sb_glide.AppendLine("glide=" & TC.NZ(row_DOSBox_Config("p_glide_glide"), "true"))
					sb_glide.AppendLine("lfb=" & TC.NZ(row_DOSBox_Config("p_glide_lfb"), "full"))
					sb_glide.AppendLine("splash=" & IIf(TC.NZ(row_DOSBox_Config("p_glide_splash"), True), "true", "false"))
				End If

				'[keyboard] - if patches are active
				If isDOSBoxPatchActivated(tbl_patches, "aux") Then
					sb_keyboard.AppendLine("[keyboard]")
					sb_keyboard.AppendLine("aux=" & IIf(TC.NZ(row_DOSBox_Config("p_keyboard_aux"), False), "true", "false"))
					sb_keyboard.AppendLine("auxdevice=" & TC.NZ(row_DOSBox_Config("p_keyboard_auxdevice"), "intellimouse"))
				End If

				'[ne2000] - if patch is active
				If isDOSBoxPatchActivated(tbl_patches, "ne2000") Then
					sb_ne2000.AppendLine("[ne2000]")
					sb_ne2000.AppendLine("ne2000=" & IIf(TC.NZ(row_DOSBox_Config("p_ne2000_ne2000"), True), "true", "false"))
					sb_ne2000.AppendLine("nicbase=" & TC.NZ(row_DOSBox_Config("p_ne2000_nicbase"), "300"))
					sb_ne2000.AppendLine("nicirq=" & TC.NZ(row_DOSBox_Config("p_ne2000_nicirq"), 3).ToString)
					sb_ne2000.AppendLine("realnic=" & TC.NZ(row_DOSBox_Config("p_ne2000_realnic"), "list"))

					Dim ne2000_macaddr As String = TC.NZ(row_DOSBox_Config("p_ne2000_macaddr"), "AC: DE:48:??:??:??")
					If ne2000_macaddr.Contains("?") Then
						Dim sb_macaddr As New System.Text.StringBuilder
						Dim guid As String = System.Guid.NewGuid.ToString.ToUpper
						Dim counter As Integer = guid.Length - 1
						For i = 0 To ne2000_macaddr.Length - 1
							If ne2000_macaddr(i) = "?" Then
								sb_macaddr.Append(guid(counter))
								counter -= 1
							Else
								sb_macaddr.Append(ne2000_macaddr(i))
							End If
						Next
						ne2000_macaddr = sb_macaddr.ToString
					End If
					sb_ne2000.AppendLine("macaddr=" & ne2000_macaddr)
				End If

				'[autoexec]
				'autoexec-before
				If TC.NZ(row_DOSBox_Config("autoexec-before"), "").Length > 0 Then
					sb_autoexec.AppendLine(row_DOSBox_Config("autoexec-before"))
				End If

				'ml-volume
				Dim volumeChannels As String() = {"MASTER", "DISNEY", "SPKR", "GUS", "SB", "FM", "CDAUDIO"}
				For Each volumeChannel As String In volumeChannels
					If TC.NZ(row_DOSBox_Config("ml-volume_" & volumeChannel & "_left"), 100) <> 100 OrElse TC.NZ(row_DOSBox_Config("ml-volume_" & volumeChannel & "_right"), 100) <> 100 Then
						sb_autoexec.AppendLine("mixer " & volumeChannel & " " & TC.NZ(row_DOSBox_Config("ml-volume_" & volumeChannel & "_left"), 100).ToString & ":" & TC.NZ(row_DOSBox_Config("ml-volume_" & volumeChannel & "_left"), 100).ToString & " /NOSHOW")
					End If
				Next

				'TODO: ml-ipx

				'MOUNT Preparation
				Dim dt_Files As New DS_ML.tbl_Emu_GamesDataTable
				DS_ML.Fill_src_frm_Rom_Manager_Emu_Games(tran, dt_Files, row_Emu_Game("id_Moby_Platforms"), row_Emu_Game("id_Emu_Games"), row_Emu_Game("id_Emu_Games"))

				Dim dview_Mount As New DataView(dt_Files)
				dview_Mount.Sort = "DOSBox_Mount_Destination ASC, Volume_Number ASC"
				Dim dt_Mount As DataTable = dview_Mount.ToTable

				Dim Working_Directory As String = ""

				Dim rows_Mount As DataRow() = dt_Mount.Select("DOSBox_Mount_Destination IS NOT NULL AND DOSBox_Mount_Destination <> ''")

				Dim al_Mount_Destination As New ArrayList()
				For Each row_Mount As DataRow In rows_Mount
					If Not al_Mount_Destination.Contains(row_Mount("DOSBox_Mount_Destination")) Then
						al_Mount_Destination.Add(row_Mount("DOSBox_Mount_Destination"))
					End If

					If TC.NZ(row_Mount("id_Rombase_DOSBox_Filetypes"), 0) = cls_Globals.enm_Rombase_DOSBox_Filetypes.cwd And Working_Directory = "" Then   'is Working Directory
						Working_Directory = row_Mount("Folder")
					End If
				Next

				If al_Mount_Destination.Count = 0 Then
					MKDXHelper.MessageBox("There is nothing to mount, please check the Rom Manager.", "Nothing to mount", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					tran.Commit()
					Return ""
				End If

				'TODO: Booters don't need working directories!
				If Working_Directory = "" Then
					MKDXHelper.MessageBox("There is no working directory to be mounted, please check the Rom Manager.", "Working directory not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					tran.Commit()
					Return ""
				End If

				Dim al_Remove_Mount_Destination As New ArrayList

				Dim bUnpacked As Boolean = False

				For Each Mount_Destination As String In al_Mount_Destination
					Dim rows_Mount_Destination() As DataRow = dt_Mount.Select("DOSBox_Mount_Destination = " & TC.getSQLFormat(Mount_Destination))

					For Each row_Mount_Destination In rows_Mount_Destination
						'Packed files
						If Not isDOSBoxPatchActivated(tbl_patches, "zipmount") Then
							If TC.NZ(row_Mount_Destination("id_Rombase_DOSBox_Filetypes"), 0) = cls_Globals.enm_Rombase_DOSBox_Filetypes.zip Then
								'Unpack, add to al_Remove_Mount_Destination, relocate file entries
								Dim file As String = row_Mount_Destination("Folder") & "\" & row_Mount_Destination("File")
								If Not Alphaleonis.Win32.Filesystem.File.Exists(file) Then
									'File not found
									MKDXHelper.MessageBox("The file " & file & "could not be found, please check the Rom Manager.", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									tran.Commit()
									Return ""
								Else
									'Unpack
									Dim prg As MKNetDXLib.cls_MKDXBaseform_Progress_Helper = Nothing

									Try
										Dim archive As SharpCompress.Archive.IArchive = SharpCompress.Archive.ArchiveFactory.Open(file)

										prg = New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(tran), 400, 100, ProgressBarStyle.Marquee, False, "Unpacking " & file, 0, archive.Entries.Count, False)
										prg.Start()

										For Each archive_entry As SharpCompress.Archive.IArchiveEntry In archive.Entries
											prg.IncreaseCurrentValue()
											If Not archive_entry.IsDirectory Then
												SharpCompress.Archive.IArchiveEntryExtensions.WriteToDirectory(archive_entry, Working_Directory, SharpCompress.Common.ExtractOptions.ExtractFullPath Or SharpCompress.Common.ExtractOptions.Overwrite)
											End If
										Next

										al_Remove_Mount_Destination.Add(row_Mount_Destination("id_Emu_Games"))
									Catch ex As Exception
										If prg IsNot Nothing Then prg.Close()
										MKDXHelper.ExceptionMessageBox(ex, "There has been an exception while unpacking " & file & "." & ControlChars.CrLf & "The error was: ", "Exception")
									Finally
										If prg IsNot Nothing Then prg.Close()
									End Try

									bUnpacked = True
								End If
							End If
						End If

						'Packed CD images
						If TC.NZ(row_Mount_Destination("id_Rombase_DOSBox_Filetypes"), 0) = cls_Globals.enm_Rombase_DOSBox_Filetypes.iso AndAlso Not Equals(row_Mount_Destination("File"), row_Mount_Destination("InnerFile")) Then
							'we have to unpack CD images (also unpack the .bin files alongside .cue files)
							Dim file As String = row_Mount_Destination("Folder") & "\" & row_Mount_Destination("File")

							If Not Alphaleonis.Win32.Filesystem.File.Exists(file) Then
								'File not found
								MKDXHelper.MessageBox("The file " & file & "could not be found, please check the Rom Manager.", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								tran.Commit()
								Return ""
							Else
								'Unpack
								Dim prg As MKNetDXLib.cls_MKDXBaseform_Progress_Helper = Nothing

								Try
									Dim archive As SharpCompress.Archive.IArchive = SharpCompress.Archive.ArchiveFactory.Open(file)

									prg = New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(tran), 400, 100, ProgressBarStyle.Marquee, False, "Unpacking CD image " & row_Mount_Destination("InnerFile"), 0, 1, False)
									prg.Start()

									Dim sCueSheet As String = ""

									For Each archive_entry As SharpCompress.Archive.IArchiveEntry In archive.Entries
										If Not archive_entry.IsDirectory Then
											If Equals(archive_entry.FilePath, row_Mount_Destination("InnerFile")) Then
												'Only unpack if not already exists
												If Not Alphaleonis.Win32.Filesystem.File.Exists(Working_Directory & "\" & archive_entry.FilePath) Then
													SharpCompress.Archive.IArchiveEntryExtensions.WriteToDirectory(archive_entry, Working_Directory, SharpCompress.Common.ExtractOptions.ExtractFullPath Or SharpCompress.Common.ExtractOptions.Overwrite)
													If archive_entry.FilePath.ToLower.EndsWith(".cue") Then
														sCueSheet = archive_entry.FilePath
													End If
												End If
											End If
										End If
									Next

									If sCueSheet <> "" Then
										'extract all the stuff within the cuesheet
										Dim sCueSheetContent As String = MKNetLib.cls_MKFileSupport.GetFileContents(Working_Directory & "\" & sCueSheet)
										Dim matches As Object = MKNetLib.cls_MKRegex.GetMatches(sCueSheetContent, "FILE\s*\""(.*?)""")
										For Each match As System.Text.RegularExpressions.Match In matches
											Dim targetfile As String = match.Groups(1).Value.ToLower

											For Each archive_entry As SharpCompress.Archive.IArchiveEntry In archive.Entries
												If Not archive_entry.IsDirectory Then
													If archive_entry.FilePath.ToLower.EndsWith(targetfile) Then
														'Only unpack if not already exists
														If Not Alphaleonis.Win32.Filesystem.File.Exists(Working_Directory & "\" & archive_entry.FilePath) Then
															SharpCompress.Archive.IArchiveEntryExtensions.WriteToDirectory(archive_entry, Working_Directory, SharpCompress.Common.ExtractOptions.ExtractFullPath Or SharpCompress.Common.ExtractOptions.Overwrite)
														End If
													End If
												End If
											Next
										Next
									End If

								Catch ex As Exception
									If prg IsNot Nothing Then prg.Close()
									MKDXHelper.ExceptionMessageBox(ex, "There has been an exception while unpacking " & row_Mount_Destination("InnerFile") & "." & ControlChars.CrLf & "The error was: ")
								Finally
									If prg IsNot Nothing Then prg.Close()
								End Try

								bUnpacked = True
							End If
						End If
					Next

					'Remove mount destinations of packed files after unpacking
					For Each remove_id As Integer In al_Remove_Mount_Destination
						DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Emu_Games SET DOSBox_Mount_Destination = NULL WHERE id_Emu_Games = " & TC.getSQLFormat(remove_id), tran)
					Next
				Next

				If bUnpacked Then
					'Rescan DOSBox Working Directory
					Try
						frm_Rom_Manager.Rescan_DOSBox_Game(row_Emu_Game("id_Emu_Games"), tran)
					Catch ex As Exception
						MKDXHelper.ExceptionMessageBox(ex, "There has been an error while rescanning a DOSBox game after unpacking. The error was: ", "Error while rescanning DOSBox game")
					End Try

					'Reload the file list
					dt_Files = New DS_ML.tbl_Emu_GamesDataTable
					DS_ML.Fill_src_frm_Rom_Manager_Emu_Games(tran, dt_Files, row_Emu_Game("id_Moby_Platforms"), row_Emu_Game("id_Emu_Games"), row_Emu_Game("id_Emu_Games"))

					dview_Mount = New DataView(dt_Files)
					dview_Mount.Sort = "DOSBox_Mount_Destination ASC, Volume_Number ASC"
					dt_Mount = dview_Mount.ToTable
				End If

				'MOUNT
				Dim sFirstMountLetter As String = ""

				For Each Mount_Destination As String In al_Mount_Destination
					Dim Mount_Command As String = ""

					Dim rows_Mount_Destination() As DataRow = dt_Mount.Select("DOSBox_Mount_Destination = " & TC.getSQLFormat(Mount_Destination))

					Dim is_CD_Images As Boolean = False
					Dim is_Working_Directory As Boolean = False
					Dim is_Packed_Content As Boolean = False
					Dim is_Floppy_Images As Boolean = False
					Dim is_Floppy_Booter As Boolean = False

					For Each row_Mount_Destination In rows_Mount_Destination
						Select Case TC.NZ(row_Mount_Destination("id_Rombase_DOSBox_Filetypes"), 0)
							Case cls_Globals.enm_Rombase_DOSBox_Filetypes.zip
								is_Packed_Content = True
							Case cls_Globals.enm_Rombase_DOSBox_Filetypes.cwd
								is_Working_Directory = True
							Case cls_Globals.enm_Rombase_DOSBox_Filetypes.iso
								is_CD_Images = True
							Case cls_Globals.enm_Rombase_DOSBox_Filetypes.img
								is_Floppy_Images = True
							Case cls_Globals.enm_Rombase_DOSBox_Filetypes.img_boot
								is_Floppy_Booter = True
						End Select
					Next

					'TODO: use short paths for all files

					If is_CD_Images Then
						If is_Working_Directory OrElse is_Packed_Content OrElse is_Floppy_Images OrElse is_Floppy_Booter Then
							MKDXHelper.MessageBox("You cannot mount CD image/s but also other media on drive " & Mount_Destination & ". Please check the Rom Manager.", "Cannot mix CD with other media", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							tran.Commit()
							Return ""
						End If

						Mount_Command = "imgmount " & Mount_Destination

						For Each row_Mount_Destination In rows_Mount_Destination
							If TC.NZ(row_Mount_Destination("id_Rombase_DOSBox_Filetypes"), 0) = cls_Globals.enm_Rombase_DOSBox_Filetypes.iso Then 'Only CD Images
								Mount_Command &= " """ & row_Mount_Destination("Folder") & "\" & row_Mount_Destination("File") & """"
							End If
						Next

						Mount_Command &= " -t iso"
					ElseIf is_Floppy_Images OrElse is_Floppy_Booter Then
						If is_Working_Directory OrElse is_Packed_Content OrElse is_CD_Images Then
							MKDXHelper.MessageBox("You cannot mount floppy image/s but also other media on drive " & Mount_Destination & ". Please check the Rom Manager.", "Cannot mix CD with other media", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							tran.Commit()
							Return ""
						End If

						Mount_Command = "imgmount " & Mount_Destination

						For Each row_Mount_Destination In rows_Mount_Destination
							If {cls_Globals.enm_Rombase_DOSBox_Filetypes.img, cls_Globals.enm_Rombase_DOSBox_Filetypes.img_boot}.Contains(TC.NZ(row_Mount_Destination("id_Rombase_DOSBox_Filetypes"), 0)) Then  'Only Floppy or Booter Images
								Mount_Command &= " """ & row_Mount_Destination("Folder") & "\" & row_Mount_Destination("File") & """"
							End If
						Next

						Mount_Command &= " -t floppy"
					ElseIf is_Packed_Content Then
						If isDOSBoxPatchActivated(tbl_patches, "zipmount") Then
							Mount_Command = "mount " & Mount_Destination & " """

							If is_Working_Directory Then
								For Each row_Mount_Destination In rows_Mount_Destination
									If TC.NZ(row_Mount_Destination("id_Rombase_DOSBox_Filetypes"), 0) = cls_Globals.enm_Rombase_DOSBox_Filetypes.cwd Then 'Only Working Directories
										Dim sb_shortpath As New System.Text.StringBuilder(256)
										MKNetLib.cls_MKFileSupport.GetShortPathName(row_Mount_Destination("Folder"), sb_shortpath, sb_shortpath.Capacity)

										Mount_Command &= sb_shortpath.ToString & ":"
									End If
								Next
							End If

							For Each row_Mount_Destination In rows_Mount_Destination
								If TC.NZ(row_Mount_Destination("id_Rombase_DOSBox_Filetypes"), 0) = cls_Globals.enm_Rombase_DOSBox_Filetypes.zip Then 'Only Packed Content
									Dim sb_shortpath As New System.Text.StringBuilder(256)
									MKNetLib.cls_MKFileSupport.GetShortPathName(row_Mount_Destination("Folder") & "\" & row_Mount_Destination("File"), sb_shortpath, sb_shortpath.Capacity)

									Mount_Command &= sb_shortpath.ToString & ":"
								End If
							Next

							Mount_Command &= "/" & """"
						End If
					ElseIf is_Working_Directory Then
						If rows_Mount_Destination.Length > 1 Then
							MKDXHelper.MessageBox("You cannot mount more than one working directory to drive " & Mount_Destination & ". Please check the Rom Manager.", "Cannot mix CD with other media", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							tran.Commit()
							Return ""
						End If

						Dim sb_shortpath As New System.Text.StringBuilder(256)
						MKNetLib.cls_MKFileSupport.GetShortPathName(rows_Mount_Destination(0)("Folder"), sb_shortpath, sb_shortpath.Capacity)

						Mount_Command = "mount " & Mount_Destination & " """ & sb_shortpath.ToString & """"
					End If

					sb_autoexec.AppendLine(Mount_Command)

					If sFirstMountLetter = "" Then sFirstMountLetter = Mount_Destination
				Next

				'autoexec-after
				If TC.NZ(row_DOSBox_Config("autoexec-after"), "").Length > 0 Then
					sb_autoexec.AppendLine(row_DOSBox_Config("autoexec-after"))
				End If

				'Autostart an .exe file
				Dim Autostart_Exe As Boolean = False
				If id_Rombase_DOSBox_Exe_Types <> 0 Then
					Dim row_Exe As DataRow = Nothing
					Dim rows_Exe As DataRow() = dt_Files.Select("id_Rombase_DOSBox_Exe_Types = " & id_Rombase_DOSBox_Exe_Types)

					Dim Exe_Type As String = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT Displayname FROM rombase.tbl_Rombase_DOSBox_Exe_Types WHERE id_Rombase_DOSBox_Exe_Types = " & TC.getSQLFormat(id_Rombase_DOSBox_Exe_Types), tran), "")

					If rows_Exe.Length = 0 Then
						'No exe of the wanted type could be found
						'Using frm As New frm_DOSBox_Choose_Exe(Exe_Type.ToUpper & " executable", "id_Rombase_DOSBox_Filetypes = " & TC.getSQLFormat(cls_Globals.enm_Rombase_DOSBox_Filetypes.exe), dt_Files, "Please select a file for autostart as the " & Exe_Type & " executable in the list below and press OK. If you choose 'Just mount', DOSBox will start but won't autostart an executable.")
						Using frm As New frm_DOSBox_Choose_Exe(Exe_Type.ToUpper, "id_Rombase_DOSBox_Filetypes = " & TC.getSQLFormat(cls_Globals.enm_Rombase_DOSBox_Filetypes.exe), dt_Files)
							If frm.ShowDialog(Me.ParentForm) = DialogResult.OK Then
								If frm.BS_DOSBox_Files_and_Folders.Current IsNot Nothing Then
									row_Exe = frm.BS_DOSBox_Files_and_Folders.Current.Row
									DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Emu_Games SET id_Rombase_DOSBox_Exe_Types = " & TC.getSQLFormat(id_Rombase_DOSBox_Exe_Types) & " WHERE id_Emu_Games = " & TC.getSQLFormat(row_Exe("id_Emu_Games")), tran)
								End If
							End If
						End Using
					Else
						If rows_Exe.Length > 1 Then
							'More than one exe of that type found
							'Using frm As New frm_DOSBox_Choose_Exe(Exe_Type.ToUpper & " executable", "id_Rombase_DOSBox_Exe_Types = " & id_Rombase_DOSBox_Exe_Types, dt_Files, "Please select a file for autostart as the " & Exe_Type & " executable in the list below and press OK. If you choose 'Just mount', DOSBox will start but won't autostart an executable.")
							Using frm As New frm_DOSBox_Choose_Exe(Exe_Type.ToUpper, "id_Rombase_DOSBox_Exe_Types = " & id_Rombase_DOSBox_Exe_Types, dt_Files)
								If frm.ShowDialog(Me.ParentForm) = DialogResult.OK Then
									If frm.BS_DOSBox_Files_and_Folders.Current IsNot Nothing Then
										For Each row_Exe_Chosen As DataRow In rows_Exe
											If row_Exe_Chosen Is frm.BS_DOSBox_Files_and_Folders.Current.Row Then
												row_Exe = row_Exe_Chosen
												Exit For
											End If
										Next
									End If
								End If
							End Using
						Else
							row_Exe = rows_Exe(0)
						End If
					End If

					If row_Exe IsNot Nothing Then
						Autostart_Exe = False
						Dim Exe_Path As String = row_Exe("InnerFile").Replace("/", "\")

						If Not Equals(row_Exe("File"), row_Exe("InnerFile")) AndAlso TC.NZ(row_Exe("InnerFile"), "").Length > 0 Then
							'Inside packed content
							For Each row_mount As DataRow In dt_Files.Select("DOSBox_Mount_Destination IS NOT NULL")
								If row_mount("File") = row_Exe("File") Then
									sb_autoexec.AppendLine(row_mount("DOSBox_Mount_Destination") & ":") 'change drive
									sb_autoexec.AppendLine("cd \")  'change to root
									If Exe_Path.Contains("\") Then
										sb_autoexec.AppendLine("cd " & Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(Exe_Path))
									End If

									If Exe_Path.ToLower.Contains(".bat") Then
										sb_autoexec.AppendLine("call " & Alphaleonis.Win32.Filesystem.Path.GetFileName(Exe_Path))
									Else
										sb_autoexec.AppendLine(Alphaleonis.Win32.Filesystem.Path.GetFileName(Exe_Path))
									End If
									Autostart_Exe = True
									Exit For
								End If
							Next
						Else
							'Inside directory
							For Each row_mount As DataRow In dt_Files.Select("DOSBox_Mount_Destination IS NOT NULL")
								If row_Exe("Folder").ToLower.Contains(row_mount("Folder").ToLower) Then
									Exe_Path = Replace(row_Exe("Folder").ToLower, row_mount("Folder").tolower, "")
									Exe_Path = MKNetLib.cls_MKStringSupport.Clean_Left(Exe_Path, "\")
									Exe_Path = MKNetLib.cls_MKStringSupport.Clean_Right(Exe_Path, "\")
									Exe_Path &= "\" & row_Exe("InnerFile")
									sb_autoexec.AppendLine(row_mount("DOSBox_Mount_Destination") & ":") 'change drive
									sb_autoexec.AppendLine("cd \")  'change to root
									If Exe_Path.Contains("\") Then
										sb_autoexec.AppendLine("cd " & Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(Exe_Path))
									End If

									If Exe_Path.ToLower.Contains(".bat") Then
										sb_autoexec.AppendLine("call " & Alphaleonis.Win32.Filesystem.Path.GetFileName(Exe_Path))
									Else
										sb_autoexec.AppendLine(Alphaleonis.Win32.Filesystem.Path.GetFileName(Exe_Path))
									End If
									Autostart_Exe = True
									Exit For
								End If
							Next
						End If

						If Not Autostart_Exe Then
							MKDXHelper.MessageBox("The mounted media or directory for the executable " & Exe_Path & " could not be found, please check the Rom Manager.", "No mounted media for executable found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							tran.Commit()
							Return ""
						End If
					End If
				End If

				If Not Autostart_Exe Then
					'Don't autostart .exe file but cd into the first mounted drive (else it would reside in Z:\)
					If sFirstMountLetter <> "" Then
						sb_autoexec.AppendLine(sFirstMountLetter & ":")
					End If
				End If

				'ml-autoclose
				If Autostart_Exe Then
					If TC.NZ(row_DOSBox_Config("ml-autoclose"), True) Then
						sb_autoexec.AppendLine("exit")
					End If
				End If

				'Compile the complete DOSBox Config
				sb_DOSBox_Config.AppendLine(sb_sdl.ToString)
				sb_DOSBox_Config.AppendLine(sb_dosbox.ToString)
				sb_DOSBox_Config.AppendLine(sb_render.ToString)
				sb_DOSBox_Config.AppendLine(sb_cpu.ToString)
				sb_DOSBox_Config.AppendLine(sb_mixer.ToString)
				sb_DOSBox_Config.AppendLine(sb_midi.ToString)
				sb_DOSBox_Config.AppendLine(sb_sblaster.ToString)
				sb_DOSBox_Config.AppendLine(sb_gus.ToString)
				sb_DOSBox_Config.AppendLine(sb_speaker.ToString)
				sb_DOSBox_Config.AppendLine(sb_joystick.ToString)
				sb_DOSBox_Config.AppendLine(sb_serial.ToString)
				sb_DOSBox_Config.AppendLine(sb_dos.ToString)
				sb_DOSBox_Config.AppendLine(sb_ipx.ToString)
				sb_DOSBox_Config.AppendLine(sb_vsync.ToString)
				sb_DOSBox_Config.AppendLine(sb_pci.ToString)
				sb_DOSBox_Config.AppendLine(sb_glide.ToString)
				sb_DOSBox_Config.AppendLine(sb_keyboard.ToString)
				sb_DOSBox_Config.AppendLine(sb_ne2000.ToString)

				'Append User Custom Settings
				If TC.NZ(row_DOSBox_Config("ml-customsettings"), "").Trim <> "" Then
					sb_DOSBox_Config.AppendLine("")
					sb_DOSBox_Config.AppendLine(row_DOSBox_Config("ml-customsettings"))
				End If

				sb_DOSBox_Config.AppendLine(sb_autoexec.ToString)

				Try
					tran.Commit()
				Catch ex As Exception

				End Try

				Dim sb_Startup As New System.Text.StringBuilder

				'Save Config and create startup parameters
				Dim TempDir As String = MKNetLib.cls_MKFileSupport.CreateTempDir("ml_") 'One temp dir for all extracted roms
				Dim ConfFile As String = TempDir & "\" & Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(row_Emu_Game("File")) & ".conf"
				If MKNetLib.cls_MKFileSupport.SaveTextToFile(sb_DOSBox_Config.ToString, ConfFile) Then
					'Startup params
					If TC.NZ(row_DOSBox_Config("ml-showconsole"), False) = False Then
						sb_Startup.Append("-noconsole ")
					End If

					sb_Startup.Append(" -conf " & """" & ConfFile & """")

					Return sb_Startup.ToString
				Else
					MKDXHelper.MessageBox("An error occured while saving the DOSBox config '" & ConfFile & "'.", "Error while saving DOSBox config", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					tran.Commit()
					Return ""
				End If
			Catch ex As Exception
				MKDXHelper.ExceptionMessageBox(ex, "There has been an exception while creating the DOSBox config." & ControlChars.CrLf & "The error was: ")
				If tran IsNot Nothing AndAlso tran.Connection IsNot Nothing Then
					Try
						tran.Commit()
					Catch ex2 As Exception

					End Try
				End If
				Return ""
			End Try
		End Using
	End Function

	''' <summary>
	''' Prepare the ScummVM config for launching the game
	''' </summary>
	''' <returns>ScummVM startup parameters (incl. temp. ScummVM config), else empty String</returns>
	''' <remarks></remarks>
	Private Function Prepare_ScummVM(ByVal row_Emulators As DataRow, ByVal row_Emu_Game As DataRow) As String
		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			Try
				'Get the config for the game
				Dim dt_ScummVM_Config As DS_ML.tbl_ScummVM_ConfigsDataTable = Nothing
				DS_ML.Fill_tbl_ScummVM_Configs(tran, dt_ScummVM_Config, row_Emu_Game("id_Emu_Games"))

				If dt_ScummVM_Config Is Nothing OrElse dt_ScummVM_Config.Rows.Count <> 1 Then
					MKDXHelper.MessageBox("There has been an error while creating the ScummVM config (Errorcode 1).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					tran.Commit()
					Return ""
				End If

				Dim row_ScummVM_Config As DataRow = dt_ScummVM_Config.Rows(0)

				Dim sb_ScummVM_Config As New System.Text.StringBuilder()

				sb_ScummVM_Config.AppendLine("# This is an auto-generated scummvm.ini by Metropolis Launcher #" & ControlChars.CrLf)

				sb_ScummVM_Config.AppendLine("[scummvm]")

				'We won't bother with updates here
				sb_ScummVM_Config.AppendLine("updates_check=0")

				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("console")) Then sb_ScummVM_Config.AppendLine("console=" & IIf(TC.NZ(row_ScummVM_Config("console"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("confirm_exit")) Then sb_ScummVM_Config.AppendLine("confirm_exit=" & IIf(TC.NZ(row_ScummVM_Config("confirm_exit"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("savepath")) Then
					If Alphaleonis.Win32.Filesystem.Directory.Exists(row_ScummVM_Config("savepath")) Then
						sb_ScummVM_Config.AppendLine("savepath=" & row_ScummVM_Config("savepath"))
					End If
				End If
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("extrapath")) Then
					If Alphaleonis.Win32.Filesystem.Directory.Exists(row_ScummVM_Config("extrapath")) Then
						sb_ScummVM_Config.AppendLine("extrapath=" & row_ScummVM_Config("extrapath"))
					End If
				End If
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("language")) AndAlso Not row_ScummVM_Config("language").ToString.StartsWith("<") Then sb_ScummVM_Config.AppendLine("language=" & row_ScummVM_Config("language"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("joystick_num")) AndAlso Not row_ScummVM_Config("joystick_num").ToString.StartsWith("<") Then sb_ScummVM_Config.AppendLine("joystick_num=" & row_ScummVM_Config("joystick_num"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("copy_protection")) Then sb_ScummVM_Config.AppendLine("copy_protection=" & IIf(TC.NZ(row_ScummVM_Config("copy_protection"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("boot_param")) AndAlso Not row_ScummVM_Config("boot_param").ToString.StartsWith("<") Then
					If MKNetLib.cls_MKRegex.IsMatch(row_ScummVM_Config("boot_param"), "^\-{0,1}\d*") Then
						sb_ScummVM_Config.AppendLine("boot_param=" & MKNetLib.cls_MKRegex.GetMatches(row_ScummVM_Config("boot_param"), "^\-{0,1}\d*")(0).Value)
					End If
				End If
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("fullscreen")) Then sb_ScummVM_Config.AppendLine("fullscreen=" & IIf(TC.NZ(row_ScummVM_Config("fullscreen"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("aspect_ratio")) Then sb_ScummVM_Config.AppendLine("aspect_ratio=" & IIf(TC.NZ(row_ScummVM_Config("aspect_ratio"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("gfx_mode")) AndAlso Not row_ScummVM_Config("gfx_mode").ToString.StartsWith("<") Then sb_ScummVM_Config.AppendLine("gfx_mode=" & row_ScummVM_Config("gfx_mode"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("subtitles")) Then sb_ScummVM_Config.AppendLine("subtitles=" & IIf(TC.NZ(row_ScummVM_Config("subtitles"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("music_driver")) AndAlso Not row_ScummVM_Config("music_driver").ToString.StartsWith("<") Then sb_ScummVM_Config.AppendLine("music_driver=" & row_ScummVM_Config("music_driver"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("opl_driver")) AndAlso Not row_ScummVM_Config("opl_driver").ToString.StartsWith("<") Then sb_ScummVM_Config.AppendLine("opl_driver=" & row_ScummVM_Config("opl_driver"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("multi_midi")) Then sb_ScummVM_Config.AppendLine("multi_midi=" & IIf(TC.NZ(row_ScummVM_Config("multi_midi"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("soundfont")) Then
					If Alphaleonis.Win32.Filesystem.File.Exists(row_ScummVM_Config("soundfont")) Then
						sb_ScummVM_Config.AppendLine("soundfont=" & row_ScummVM_Config("soundfont"))
					End If
				End If
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("native_mt32")) Then sb_ScummVM_Config.AppendLine("native_mt32=" & IIf(TC.NZ(row_ScummVM_Config("native_mt32"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("enable_gs")) Then sb_ScummVM_Config.AppendLine("enable_gs=" & IIf(TC.NZ(row_ScummVM_Config("enable_gs"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("output_rate")) AndAlso Not row_ScummVM_Config("output_rate").ToString.StartsWith("<") Then sb_ScummVM_Config.AppendLine("output_rate=" & row_ScummVM_Config("output_rate"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("music_volume")) Then sb_ScummVM_Config.AppendLine("music_volume=" & row_ScummVM_Config("music_volume").ToString)
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("speech_volume")) Then sb_ScummVM_Config.AppendLine("speech_volume=" & row_ScummVM_Config("speech_volume").ToString)
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("sfx_volume")) Then sb_ScummVM_Config.AppendLine("sfx_volume=" & row_ScummVM_Config("sfx_volume").ToString)
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("speech_mute")) Then sb_ScummVM_Config.AppendLine("speech_mute=" & IIf(TC.NZ(row_ScummVM_Config("speech_mute"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("talkspeed")) Then sb_ScummVM_Config.AppendLine("talkspeed=" & row_ScummVM_Config("talkspeed").ToString)
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("cdrom")) Then sb_ScummVM_Config.AppendLine("cdrom=" & row_ScummVM_Config("cdrom").ToString)
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("tempo")) Then sb_ScummVM_Config.AppendLine("tempo=" & row_ScummVM_Config("tempo").ToString)
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("midi_gain")) Then sb_ScummVM_Config.AppendLine("midi_gain=" & row_ScummVM_Config("midi_gain").ToString)
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("autosave_period")) Then sb_ScummVM_Config.AppendLine("autosave_period=" & row_ScummVM_Config("autosave_period").ToString)
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("save_slot")) AndAlso Not row_ScummVM_Config("save_slot").ToString.StartsWith("<") Then sb_ScummVM_Config.AppendLine("save_slot=" & row_ScummVM_Config("save_slot").ToString)
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("filtering")) Then sb_ScummVM_Config.AppendLine("filtering=" & IIf(TC.NZ(row_ScummVM_Config("filtering"), False), "true", "false"))

				'Game-specific options
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("demo_mode")) Then sb_ScummVM_Config.AppendLine("demo_mode=" & IIf(TC.NZ(row_ScummVM_Config("demo_mode"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("alt_intro")) Then sb_ScummVM_Config.AppendLine("alt_intro=" & IIf(TC.NZ(row_ScummVM_Config("alt_intro"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("music_mute")) Then sb_ScummVM_Config.AppendLine("music_mute=" & IIf(TC.NZ(row_ScummVM_Config("music_mute"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("sfx_mute")) Then sb_ScummVM_Config.AppendLine("sfx_mute=" & IIf(TC.NZ(row_ScummVM_Config("sfx_mute"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("gfx_details")) AndAlso Not row_ScummVM_Config("gfx_details").ToString.StartsWith("<") Then sb_ScummVM_Config.AppendLine("gfx_details=" & row_ScummVM_Config("gfx_details").ToString)
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("object_labels")) Then sb_ScummVM_Config.AppendLine("object_labels=" & IIf(TC.NZ(row_ScummVM_Config("object_labels"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("reverse_stereo")) Then sb_ScummVM_Config.AppendLine("reverse_stereo=" & IIf(TC.NZ(row_ScummVM_Config("reverse_stereo"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("walkspeed")) AndAlso Not row_ScummVM_Config("walkspeed").ToString.StartsWith("<") Then sb_ScummVM_Config.AppendLine("walkspeed=" & row_ScummVM_Config("walkspeed").ToString)
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("originalsaveload")) Then sb_ScummVM_Config.AppendLine("originalsaveload=" & IIf(TC.NZ(row_ScummVM_Config("originalsaveload"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("altamigapalette")) Then sb_ScummVM_Config.AppendLine("altamigapalette=" & IIf(TC.NZ(row_ScummVM_Config("altamigapalette"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("mousesupport")) Then sb_ScummVM_Config.AppendLine("mousesupport=" & IIf(TC.NZ(row_ScummVM_Config("mousesupport"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("disable_dithering")) Then sb_ScummVM_Config.AppendLine("disable_dithering=" & IIf(TC.NZ(row_ScummVM_Config("disable_dithering"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("prefer_digitalsfx")) Then sb_ScummVM_Config.AppendLine("prefer_digitalsfx=" & IIf(TC.NZ(row_ScummVM_Config("prefer_digitalsfx"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("native_fb01")) Then sb_ScummVM_Config.AppendLine("native_fb01=" & IIf(TC.NZ(row_ScummVM_Config("native_fb01"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("use_cdaudio")) Then sb_ScummVM_Config.AppendLine("use_cdaudio=" & IIf(TC.NZ(row_ScummVM_Config("use_cdaudio"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("windows_cursors")) Then sb_ScummVM_Config.AppendLine("windows_cursors=" & IIf(TC.NZ(row_ScummVM_Config("windows_cursors"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("silver_cursors")) Then sb_ScummVM_Config.AppendLine("silver_cursors=" & IIf(TC.NZ(row_ScummVM_Config("silver_cursors"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("enable_gore")) Then sb_ScummVM_Config.AppendLine("enable_gore=" & IIf(TC.NZ(row_ScummVM_Config("enable_gore"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("smooth_scrolling")) Then sb_ScummVM_Config.AppendLine("smooth_scrolling=" & IIf(TC.NZ(row_ScummVM_Config("smooth_scrolling"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("floating_cursors")) Then sb_ScummVM_Config.AppendLine("floating_cursors=" & IIf(TC.NZ(row_ScummVM_Config("floating_cursors"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("enable_color_blind")) Then sb_ScummVM_Config.AppendLine("enable_color_blind=" & IIf(TC.NZ(row_ScummVM_Config("enable_color_blind"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("studio_audience")) Then sb_ScummVM_Config.AppendLine("studio_audience=" & IIf(TC.NZ(row_ScummVM_Config("studio_audience"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("skip_support")) Then sb_ScummVM_Config.AppendLine("skip_support=" & IIf(TC.NZ(row_ScummVM_Config("skip_support"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("helium_mode")) Then sb_ScummVM_Config.AppendLine("helium_mode=" & IIf(TC.NZ(row_ScummVM_Config("helium_mode"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("skiphallofrecordsscenes")) Then sb_ScummVM_Config.AppendLine("skiphallofrecordsscenes=" & IIf(TC.NZ(row_ScummVM_Config("skiphallofrecordsscenes"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("scalemakingofvideos")) Then sb_ScummVM_Config.AppendLine("scalemakingofvideos=" & IIf(TC.NZ(row_ScummVM_Config("scalemakingofvideos"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("fast_movie_speed")) Then sb_ScummVM_Config.AppendLine("fast_movie_speed=" & IIf(TC.NZ(row_ScummVM_Config("fast_movie_speed"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("doublefps")) Then sb_ScummVM_Config.AppendLine("doublefps=" & IIf(TC.NZ(row_ScummVM_Config("doublefps"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("venusenabled")) Then sb_ScummVM_Config.AppendLine("venusenabled=" & IIf(TC.NZ(row_ScummVM_Config("venusenabled"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("noanimwhileturning")) Then sb_ScummVM_Config.AppendLine("noanimwhileturning=" & IIf(TC.NZ(row_ScummVM_Config("noanimwhileturning"), False), "true", "false"))
				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("mpegmovies")) Then sb_ScummVM_Config.AppendLine("mpegmovies=" & IIf(TC.NZ(row_ScummVM_Config("mpegmovies"), False), "true", "false"))

				If Not TC.IsNullNothingOrEmpty(row_ScummVM_Config("user_defined_config")) Then sb_ScummVM_Config.AppendLine(row_ScummVM_Config("user_defined_config"))


				'MOUNT Preparation
				'For now we'll just use the directory and scanned gameid
				Dim gamepath As String = row_Emu_Game("Folder")
				Dim CustomIdentifier As String = TC.NZ(row_Emu_Game("CustomIdentifier"), "")
				Dim gameid As String = ""

				If CustomIdentifier.Split(":").Length > 1 Then
					gameid = CustomIdentifier.Split(":")(1)
				End If

				Dim sb_Startup As New System.Text.StringBuilder

				'Save Config and create startup parameters
				Dim TempDir As String = MKNetLib.cls_MKFileSupport.CreateTempDir("ml_") 'One temp dir for all extracted roms
				Dim ConfFile As String = TempDir & "\scummvm.ini"
				If MKNetLib.cls_MKFileSupport.SaveTextToFile(sb_ScummVM_Config.ToString, ConfFile) Then
					'Startup params
					sb_Startup.Append("--config=""" & ConfFile & """ --path=""" & gamepath & """")

					If Not TC.IsNullNothingOrEmpty(" " & row_ScummVM_Config("user_defined_commandline")) Then sb_Startup.Append(row_ScummVM_Config("user_defined_commandline"))

					sb_Startup.Append(" " & gameid)

					tran.Commit()

					Return sb_Startup.ToString
				Else
					MKDXHelper.MessageBox("An error occured while saving the ScummVM config '" & ConfFile & "'.", "Error while saving ScummVM config", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					tran.Commit()
					Return ""
				End If
			Catch ex As Exception
				MKDXHelper.ExceptionMessageBox(ex, "There has been an exception while creating the ScummVM config." & ControlChars.CrLf & "The error was: " & ex.Message)
				If tran IsNot Nothing AndAlso tran.Connection IsNot Nothing Then
					Try
						tran.Commit()
					Catch ex2 As Exception

					End Try
				End If
				Return ""
			End Try
		End Using
	End Function

	Private Sub Launch_Game(Optional ByVal id_Emulators As Object = Nothing, Optional ByVal id_Rombase_DOSBox_Exe_Types As Integer = cls_Globals.enm_Rombase_DOSBox_Exe_Types.main)
		'Clean up Temp Dir of files older than 2 hours
		MKNetLib.cls_MKFileSupport.Delete_Directorycontent(cls_Globals.TempDir(Nothing), 7200)

		MKNetLib.cls_MKFileSupport.DeleteContainedFiles(cls_Globals.Dir_Screenshot, cls_Extras._SupportedExtensions_Masks, IO.SearchOption.TopDirectoryOnly, FileIO.UIOption.OnlyErrorDialogs)
		_al_Screenshots.Clear()
		_al_Screenshots_EmuGames.Clear()

		If BS_Emu_Games.Current Is Nothing Then
			Return
		End If

		Dim proc = New System.Diagnostics.Process

		Select Case BS_Emu_Games.Current("id_Moby_Platforms")
			Case cls_Globals.enm_Moby_Platforms.mame
				Launch_Game_MAME(proc)
			Case cls_Globals.enm_Moby_Platforms.win
				Launch_Game_WIN(proc)
			Case cls_Globals.enm_Moby_Platforms.dos
				Launch_Game_DOS(proc, id_Emulators, id_Rombase_DOSBox_Exe_Types)
			Case cls_Globals.enm_Moby_Platforms.scummvm
				Launch_Game_ScummVM(proc, id_Emulators)
			Case Else       'Standard Emulation
				Launch_Game_EMU(proc, id_Emulators)
		End Select
	End Sub

	Private Sub Launch_Game_ScummVM(ByRef proc As System.Diagnostics.Process, ByVal id_Emulators As Object)
		Dim bShiftKeyPressed As Boolean = My.Computer.Keyboard.ShiftKeyDown

		Dim sSQL_Emulator As String = "	SELECT" & ControlChars.CrLf
		sSQL_Emulator &= "		EMU.id_Emulators" & ControlChars.CrLf
		sSQL_Emulator &= "		, EMU.Displayname" & ControlChars.CrLf
		sSQL_Emulator &= "		, EMU.InstallDirectory" & ControlChars.CrLf
		sSQL_Emulator &= "		, EMU.Executable" & ControlChars.CrLf
		sSQL_Emulator &= "		, EMU.StartupParameter" & ControlChars.CrLf
		sSQL_Emulator &= "		, EMU.AutoItScript" & ControlChars.CrLf
		sSQL_Emulator &= "		, EMU.J2KPreset" & ControlChars.CrLf
		sSQL_Emulator &= "		, EMU.ScreenshotDirectory" & ControlChars.CrLf
		sSQL_Emulator &= "		, EMU.Libretro_Core" & ControlChars.CrLf
		sSQL_Emulator &= "		, EMU.id_List_Generators" & ControlChars.CrLf
		sSQL_Emulator &= "		, LGEN.Name AS LGEN_Name" & ControlChars.CrLf
		sSQL_Emulator &= "		, LGEN.Main_Template AS LGEN_Main_Template" & ControlChars.CrLf
		sSQL_Emulator &= "		, LGEN.File_Entry_Template AS LGEN_File_Entry_Template" & ControlChars.CrLf
		sSQL_Emulator &= "		, LGEN.Sort AS LGEN_Sort" & ControlChars.CrLf
		sSQL_Emulator &= "	FROM tbl_Emulators_Moby_Platforms EMUPLTFM" & ControlChars.CrLf
		sSQL_Emulator &= "	LEFT JOIN tbl_Emulators EMU ON EMUPLTFM.id_Emulators = EMU.id_Emulators" & ControlChars.CrLf
		sSQL_Emulator &= "	LEFT JOIN tbl_List_Generators LGEN ON EMU.id_List_Generators = LGEN.id_List_Generators" & ControlChars.CrLf

		If id_Emulators Is Nothing Then
			sSQL_Emulator &= "	WHERE EMUPLTFM.id_Emulators = (" & ControlChars.CrLf
			sSQL_Emulator &= "			SELECT id_Emulators" & ControlChars.CrLf
			sSQL_Emulator &= "			FROM tbl_Emu_Games" & ControlChars.CrLf
			sSQL_Emulator &= "			WHERE id_Emu_Games = " & BS_Emu_Games.Current("id_Emu_Games") & ControlChars.CrLf
			sSQL_Emulator &= "		) OR (" & ControlChars.CrLf
			sSQL_Emulator &= "			EMUPLTFM.DefaultEmulator = 1" & ControlChars.CrLf
			sSQL_Emulator &= "			AND id_Moby_Platforms = " & TC.getSQLFormat(BS_Emu_Games.Current("id_Moby_Platforms")) & ControlChars.CrLf
			sSQL_Emulator &= "		)" & ControlChars.CrLf
			sSQL_Emulator &= "	ORDER BY EMUPLTFM.DefaultEmulator" & ControlChars.CrLf
		Else
			sSQL_Emulator &= "	WHERE EMU.id_Emulators = " & TC.getSQLFormat(id_Emulators) & ControlChars.CrLf
		End If

		sSQL_Emulator &= "	LIMIT 1"

		Dim dt_Emulators As DataTable = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL_Emulator)

		If TC.NZ(id_Emulators, 0) <= 0 Then
			If dt_Emulators Is Nothing OrElse dt_Emulators.Rows.Count < 1 Then
				If MKDXHelper.MessageBox("There is no default ScummVM emulator found, do you want to set one up?", "No default emulator found", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
					Using frm As New frm_Emulators
						frm.ShowDialog(Me.ParentForm)
					End Using
				End If
				Return
			End If
		Else
			If dt_Emulators Is Nothing OrElse dt_Emulators.Rows.Count < 1 Then
				MKDXHelper.MessageBox("ERROR: Emulator not found.", "Emulator not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Return
			End If
		End If

		id_Emulators = TC.NZ(dt_Emulators.Rows(0)("id_Emulators"), 0)

		Dim emufullpath As String = TC.NZ(dt_Emulators.Rows(0)("InstallDirectory"), "") & "\" & TC.NZ(dt_Emulators.Rows(0)("Executable"), "")
		If Not Alphaleonis.Win32.Filesystem.File.Exists(emufullpath) Then
			MKDXHelper.MessageBox("The emulator's executable has not been found: " & emufullpath, "Emulator not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		Dim snapdir As String = TC.NZ(dt_Emulators.Rows(0)("ScreenshotDirectory"), "")
		MKNetLib.cls_MKFileSupport.DeleteContainedFiles(snapdir, cls_Extras._SupportedExtensions_Masks, IO.SearchOption.TopDirectoryOnly, FileIO.UIOption.OnlyErrorDialogs)

		Dim emuexe As String = Alphaleonis.Win32.Filesystem.Path.GetFileName(emufullpath)
		Dim emudir As String = Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(emufullpath)

		'Generate Config
		Dim Args As String = Prepare_ScummVM(dt_Emulators.Rows(0), BS_Emu_Games.Current.Row)

		If TC.IsNullNothingOrEmpty(Args) Then
			Return
		End If

		proc.StartInfo.FileName = emufullpath
		proc.StartInfo.WorkingDirectory = emudir

		proc.StartInfo.Arguments = Args

		proc.StartInfo.UseShellExecute = True

		proc.EnableRaisingEvents = True

		AddHandler proc.Exited, AddressOf Handle_Proc_Exited

		'Shell call would be: emufullpath & " " & Args

		If bShiftKeyPressed Then
			Using frm As New MKNetDXLib.frm_TextBoxEdit("Command:", "This is the command for launching the emulator.", """" & proc.StartInfo.FileName & """ " & proc.StartInfo.Arguments, True)
				If frm.ShowDialog(Me.ParentForm) <> DialogResult.OK Then
					Return
				End If
			End Using
		End If

		Dim J2K_Preset = TC.NZ(dt_Emulators.Rows(0)("J2KPreset"), "")

		If TC.NZ(BS_Emu_Games.Current("J2KPreset"), "").Length > 0 Then
			J2K_Preset = TC.NZ(BS_Emu_Games.Current("J2KPreset"), "")
		End If

		Call_J2K(J2K_Preset)  'Call J2K

		proc.Start()

		Try
			dict_Proc_EmuGames.Add(proc.Id, New cls_Emu_Game_ProcInfo(BS_Emu_Games.Current("id_Emu_Games"), snapdir, cls_Globals.enm_Moby_Platforms.scummvm))
		Catch ex As Exception

		End Try
	End Sub


	Private Sub Launch_Game_EMU(ByRef proc As System.Diagnostics.Process, ByVal id_Emulators As Object)
		Dim bShiftKeyPressed As Boolean = My.Computer.Keyboard.ShiftKeyDown

		Dim id_Moby_Platforms As Integer = BS_Emu_Games.Current("id_Moby_Platforms")

		Dim sSQL_Emulator As String = "	SELECT" & ControlChars.CrLf
		sSQL_Emulator &= "		EMU.id_Emulators" & ControlChars.CrLf
		sSQL_Emulator &= "		, EMU.Displayname" & ControlChars.CrLf
		sSQL_Emulator &= "		, EMU.InstallDirectory" & ControlChars.CrLf
		sSQL_Emulator &= "		, EMU.Executable" & ControlChars.CrLf
		sSQL_Emulator &= "		, EMU.StartupParameter" & ControlChars.CrLf
		sSQL_Emulator &= "		, EMU.AutoItScript" & ControlChars.CrLf
		sSQL_Emulator &= "		, EMU.J2KPreset" & ControlChars.CrLf
		sSQL_Emulator &= "		, EMU.ScreenshotDirectory" & ControlChars.CrLf
		sSQL_Emulator &= "		, EMU.Libretro_Core" & ControlChars.CrLf
		sSQL_Emulator &= "		, EMU.id_List_Generators" & ControlChars.CrLf
		sSQL_Emulator &= "		, IFNULL(RBLGEN.Name, LGEN.Name) AS LGEN_Name" & ControlChars.CrLf
		sSQL_Emulator &= "		, IFNULL(RBLGEN.Main_Template, LGEN.Main_Template) AS LGEN_Main_Template" & ControlChars.CrLf
		sSQL_Emulator &= "		, IFNULL(RBLGEN.File_Entry_Template, LGEN.File_Entry_Template) AS LGEN_File_Entry_Template" & ControlChars.CrLf
		sSQL_Emulator &= "		, IFNULL(RBLGEN.Sort, LGEN.Sort) AS LGEN_Sort" & ControlChars.CrLf
		sSQL_Emulator &= "	FROM tbl_Emulators_Moby_Platforms EMUPLTFM" & ControlChars.CrLf
		sSQL_Emulator &= "	LEFT JOIN tbl_Emulators EMU ON EMUPLTFM.id_Emulators = EMU.id_Emulators" & ControlChars.CrLf
		sSQL_Emulator &= "	LEFT JOIN tbl_List_Generators LGEN ON EMU.id_List_Generators = LGEN.id_List_Generators" & ControlChars.CrLf
		sSQL_Emulator &= "	LEFT JOIN rombase.tbl_Rombase_List_Generators RBLGEN ON -EMU.id_List_Generators = RBLGEN.id_Rombase_List_Generators" & ControlChars.CrLf

		If id_Emulators Is Nothing Then
			sSQL_Emulator &= "	WHERE EMUPLTFM.id_Emulators = (" & ControlChars.CrLf
			sSQL_Emulator &= "			SELECT id_Emulators" & ControlChars.CrLf
			sSQL_Emulator &= "			FROM tbl_Emu_Games" & ControlChars.CrLf
			sSQL_Emulator &= "			WHERE id_Emu_Games = " & BS_Emu_Games.Current("id_Emu_Games") & ControlChars.CrLf
			sSQL_Emulator &= "		) OR (" & ControlChars.CrLf
			sSQL_Emulator &= "			EMUPLTFM.DefaultEmulator = 1" & ControlChars.CrLf
			sSQL_Emulator &= "			AND id_Moby_Platforms = " & TC.getSQLFormat(id_Moby_Platforms) & ControlChars.CrLf
			sSQL_Emulator &= "		)" & ControlChars.CrLf
			sSQL_Emulator &= "	ORDER BY EMUPLTFM.DefaultEmulator" & ControlChars.CrLf
		Else
			sSQL_Emulator &= "	WHERE EMU.id_Emulators = " & TC.getSQLFormat(id_Emulators) & ControlChars.CrLf
		End If

		sSQL_Emulator &= "	LIMIT 1"

		Dim dt_Emulators As DataTable = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL_Emulator)

		If TC.NZ(id_Emulators, 0) <= 0 Then
			If dt_Emulators Is Nothing OrElse dt_Emulators.Rows.Count < 1 Then
				If MKDXHelper.MessageBox("There is no default emulator found for this platform, do you want to set one up?", "No default emulator found", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
					Using frm As New frm_Emulators
						frm.ShowDialog(Me.ParentForm)
					End Using
				End If
				Return
			End If
		Else
			If dt_Emulators Is Nothing OrElse dt_Emulators.Rows.Count < 1 Then
				MKDXHelper.MessageBox("ERROR: Emulator not found.", "Emulator not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Return
			End If
		End If

		id_Emulators = TC.NZ(dt_Emulators.Rows(0)("id_Emulators"), 0)

		Dim emufullpath As String = TC.NZ(dt_Emulators.Rows(0)("InstallDirectory"), "") & "\" & TC.NZ(dt_Emulators.Rows(0)("Executable"), "")
		If Not Alphaleonis.Win32.Filesystem.File.Exists(emufullpath) Then
			MKDXHelper.MessageBox("The emulator's executable has not been found: " & emufullpath, "Emulator not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		Dim snapdir As String = TC.NZ(dt_Emulators.Rows(0)("ScreenshotDirectory"), "")
		MKNetLib.cls_MKFileSupport.DeleteContainedFiles(snapdir, cls_Extras._SupportedExtensions_Masks, IO.SearchOption.TopDirectoryOnly, FileIO.UIOption.OnlyErrorDialogs)

		Dim emuexe As String = Alphaleonis.Win32.Filesystem.Path.GetFileName(emufullpath)
		Dim emudir As String = Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(emufullpath)

		Dim Args As String = dt_Emulators.Rows(0)("StartupParameter")
		Args = Args.Replace("%emudir%", emudir)
		Args = Args.Replace("%emuexe%", emuexe)
		Args = Args.Replace("%emufullpath%", emufullpath)

		Dim TempDir As String = ""

		'Main Romfile Args
		If Args.Contains("%rom") Then
			TempDir = MKNetLib.cls_MKFileSupport.CreateTempDir("ml_") 'One temp dir for all extracted roms

			Dim rfd As New cls_Romfiledata(TC.NZ(BS_Emu_Games.Current("Folder"), "") & "\" & TC.NZ(BS_Emu_Games.Current("File"), ""), TC.NZ(BS_Emu_Games.Current("Innerfile"), ""), TempDir)

			If Not rfd.IsValid Then
				Me.Set_Current_Emu_Game_Unavailable(True)
				Return
			End If

			Me.Set_Current_Emu_Game_Unavailable(False)

			Args = Args.Replace("%romdir%", rfd.DirName)
			Args = Args.Replace("%romfile%", rfd.FileName)
			Args = Args.Replace("%romfullpath%", rfd.Fullpath)
		End If

		If Args.Contains("%multivolume%") Then
			If BS_Emu_Games.Current("MultiVolume") = True Then
				Dim sMultiVolume As String = ""

				Dim iMaxVol As Integer = 0

				Dim dt_MV_Params As New DS_ML.tbl_Emulators_Multivolume_ParametersDataTable
				Dim dt_Emu_Games_Volumes As New DS_ML.src_ucr_Emulation_GamesDataTable

				Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
					DS_ML.Fill_src_frm_Emulators_Multivolume_Parameters(tran, dt_MV_Params, id_Emulators)

					If dt_MV_Params.Select("Volume_Number = 1").Length = 0 Then
						MKDXHelper.MessageBox("A parameter setup for volume 1 could not be found, please check your emulator settings for " & dt_Emulators.Rows(0)("Displayname") & ".", "No entry set up for volume 1", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						tran.Commit()
						Return
					End If

					iMaxVol = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "Select MAX(Volume_Number) FROM tbl_Emulators_Multivolume_Parameters WHERE id_Emulators = " & TC.getSQLFormat(id_Emulators), tran), 0)

					DS_ML.Fill_src_ucr_Emulation_Games(tran, dt_Emu_Games_Volumes, Nothing, Nothing, Nothing, BS_Emu_Games.Current("id_Emu_Games"), True, 0, True)

					tran.Commit()
				End Using

				For iVol As Integer = 1 To iMaxVol
					Dim rowsVolParam() As DataRow = dt_MV_Params.Select("Volume_Number = " & TC.getSQLFormat(iVol))

					If rowsVolParam.Length > 0 Then
						Dim rowsVolGames() As DataRow = dt_Emu_Games_Volumes.Select("Volume_Number = " & TC.getSQLFormat(iVol))

						If iVol = 1 AndAlso rowsVolGames.Length = 0 Then
							rowsVolGames = dt_Emu_Games_Volumes.Select("Volume_Number IS NULL")
						End If

						If iVol = 1 AndAlso rowsVolGames.Length = 0 Then
							MKDXHelper.MessageBox("The first disc/volume of the game could not be found, please check the Rom Manager.", "First disc/volume missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Me.Set_Current_Emu_Game_Unavailable(True)
							Return
						End If

						Me.Set_Current_Emu_Game_Unavailable(False)

						If rowsVolGames.Length > 0 Then
							Dim sParam As String = rowsVolParam(0)("Parameter")

							sParam = sParam.Replace("%emudir%", emudir)
							sParam = sParam.Replace("%emuexe%", emuexe)
							sParam = sParam.Replace("%emufullpath%", emufullpath)

							If sParam.Contains("%rom") Then
								If Not Alphaleonis.Win32.Filesystem.Directory.Exists(TempDir) Then TempDir = MKNetLib.cls_MKFileSupport.CreateTempDir("ml_") 'One temp dir for all extracted roms

								Dim rfdVol As New cls_Romfiledata(TC.NZ(rowsVolGames(0)("Folder"), "") & "\" & TC.NZ(rowsVolGames(0)("File"), ""), TC.NZ(rowsVolGames(0)("Innerfile"), ""), TempDir)

								If Not rfdVol.IsValid Then Return

								sParam = sParam.Replace("%romdir%", rfdVol.DirName)
								sParam = sParam.Replace("%romfile%", rfdVol.FileName)
								sParam = sParam.Replace("%romfullpath%", rfdVol.Fullpath)
							End If

							sMultiVolume &= sParam
						End If
					End If
				Next

				Args = Args.Replace("%multivolume%", sMultiVolume)
			Else
				Args = Args.Replace("%multivolume%", "")
			End If
		End If

		'Generate a file list text file and provide it as a startup parameter
		If Args.Contains("%listfile") Then
			Dim iMaxVol As Integer = 0
			Dim iStartVol As Integer = 1
			Dim iEndVol As Integer = 1
			Dim iStep As Integer = 1

			'TODO: Checks (Main_Template, File_Entry_Template)

			Dim dt_Emu_Games_Volumes As New DS_ML.src_ucr_Emulation_GamesDataTable

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				iMaxVol = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "Select MAX(Volume_Number) FROM tbl_Emu_Games WHERE id_Emu_Games = " & BS_Emu_Games.Current("id_Emu_Games") & " OR id_Emu_Games_Owner = " & BS_Emu_Games.Current("id_Emu_Games"), tran), 0)

				DS_ML.Fill_src_ucr_Emulation_Games(tran, dt_Emu_Games_Volumes, Nothing, Nothing, Nothing, BS_Emu_Games.Current("id_Emu_Games"), True, 0, True)

				'tran.Commit()
			End Using

			If TC.NZ(dt_Emulators.Rows(0)("LGEN_Sort"), 1) = 2 Then
				'Descending Sort
				iStartVol = iMaxVol
				iEndVol = 1
				iStep = -1
			Else
				'Ascending Sort
				iStartVol = 1
				iEndVol = iMaxVol
				iStep = 1
			End If

			Dim sEntries As String = ""

			For iVol As Integer = iStartVol To iEndVol Step iStep
				Dim rowsVolGames() As DataRow = dt_Emu_Games_Volumes.Select("Volume_Number = " & TC.getSQLFormat(iVol))

				If iVol = 1 AndAlso rowsVolGames.Length = 0 Then
					rowsVolGames = dt_Emu_Games_Volumes.Select("Volume_Number IS NULL")
				End If

				If iVol = 1 AndAlso rowsVolGames.Length = 0 Then
					MKDXHelper.MessageBox("The first disc/volume of the game could not be found, please check the Rom Manager.", "First disc/volume missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Me.Set_Current_Emu_Game_Unavailable(True)
					Return
				End If

				Me.Set_Current_Emu_Game_Unavailable(False)

				If rowsVolGames.Length > 0 Then
					Dim sEntry As String = TC.NZ(dt_Emulators.Rows(0)("LGEN_File_Entry_Template"), "")

					sEntry = sEntry.Replace("%emudir%", emudir)
					sEntry = sEntry.Replace("%emuexe%", emuexe)
					sEntry = sEntry.Replace("%emufullpath%", emufullpath)

					If sEntry.Contains("%rom") Then
						If Not Alphaleonis.Win32.Filesystem.Directory.Exists(TempDir) Then TempDir = MKNetLib.cls_MKFileSupport.CreateTempDir("ml_") 'One temp dir for all extracted roms

						Dim rfdVol As New cls_Romfiledata(TC.NZ(rowsVolGames(0)("Folder"), "") & "\" & TC.NZ(rowsVolGames(0)("File"), ""), TC.NZ(rowsVolGames(0)("Innerfile"), ""), TempDir)

						If Not rfdVol.IsValid Then Return

						sEntry = sEntry.Replace("%romdir%", rfdVol.DirName)
						sEntry = sEntry.Replace("%romfile%", rfdVol.FileName)
						sEntry = sEntry.Replace("%romfullpath%", rfdVol.Fullpath)
					End If

					sEntries &= sEntry
				End If
			Next

			Dim sFileListContent As String = TC.NZ(dt_Emulators.Rows(0)("LGEN_Main_Template"), "").Replace("%entries%", sEntries)

			'TODO: Args = Args.Replace("%listfile%", sMultiVolume)
			Dim sVariable As String = MKNetLib.cls_MKRegex.GetMatches(Args, "%listfile.*?%")(0).Value

			Dim sFileExtension = ".txt"

			If sVariable.Contains(".") Then
				sFileExtension = MKNetLib.cls_MKRegex.GetMatches(sVariable, "(\..*?)%")(0).Value.Replace("%", "")
			End If

			'Create the listfile
			If Not Alphaleonis.Win32.Filesystem.Directory.Exists(TempDir) Then TempDir = MKNetLib.cls_MKFileSupport.CreateTempDir("ml_") 'One temp dir for all extracted roms and the listfile

			Dim sFileListPath As String = TempDir & "\" & "filelist" & sFileExtension

			MKNetLib.cls_MKFileSupport.SaveTextToFile(sFileListContent, sFileListPath)

			Args = Args.Replace(sVariable, sFileListPath)
		End If

		If Not TC.IsNullNothingOrEmpty(dt_Emulators.Rows(0)("Libretro_Core")) Then
			Args = "-L cores\" & dt_Emulators.Rows(0)("Libretro_Core").Trim & (IIf(Args <> "", " ", "")) & Args
		End If

		proc.StartInfo.FileName = emufullpath
		proc.StartInfo.WorkingDirectory = emudir

		proc.StartInfo.Arguments = Args

		proc.StartInfo.UseShellExecute = True

		proc.EnableRaisingEvents = True

		AddHandler proc.Exited, AddressOf Handle_Proc_Exited

		'Shell call would be: emufullpath & " " & Args

		If bShiftKeyPressed Then
			Using frm As New MKNetDXLib.frm_TextBoxEdit("Command:", "This is the command for launching the emulator.", """" & proc.StartInfo.FileName & """ " & proc.StartInfo.Arguments, True)
				If frm.ShowDialog(Me.ParentForm) <> DialogResult.OK Then
					Return
				End If
			End Using
		End If

		Dim J2K_Preset = TC.NZ(dt_Emulators.Rows(0)("J2KPreset"), "")

		If TC.NZ(BS_Emu_Games.Current("J2KPreset"), "").Length > 0 Then
			J2K_Preset = TC.NZ(BS_Emu_Games.Current("J2KPreset"), "")
		End If

		Call_J2K(J2K_Preset)  'Call J2K

		proc.Start()

		Try
			dict_Proc_EmuGames.Add(proc.Id, New cls_Emu_Game_ProcInfo(BS_Emu_Games.Current("id_Emu_Games"), snapdir, id_Moby_Platforms))
		Catch ex As Exception

		End Try
	End Sub

	Private Sub Launch_Game_DOS(ByRef proc As System.Diagnostics.Process, ByVal id_Emulators As Object, ByVal id_Rombase_DOSBox_Exe_Types As Integer)
		Dim sSQL_DefaultEmu As String = "	SELECT" & ControlChars.CrLf &
				"		EMU.id_Emulators" & ControlChars.CrLf &
				"		, EMU.Displayname" & ControlChars.CrLf &
				"		, EMU.InstallDirectory" & ControlChars.CrLf &
				"		, EMU.Executable" & ControlChars.CrLf &
				"		, EMU.StartupParameter" & ControlChars.CrLf &
				"		, EMU.AutoItScript" & ControlChars.CrLf &
				"		, EMU.J2KPreset" & ControlChars.CrLf &
				"		, EMU.ScreenshotDirectory" & ControlChars.CrLf &
				"		, EMU.DOSBox_Patch_NE2000_Ethernet" & ControlChars.CrLf &
				"		, EMU.DOSBox_Patch_ZIP_Mount" & ControlChars.CrLf &
				"	FROM tbl_Emulators_Moby_Platforms EMUPLTFM" & ControlChars.CrLf &
				"	LEFT JOIN tbl_Emulators EMU ON EMUPLTFM.id_Emulators = EMU.id_Emulators" & ControlChars.CrLf &
				IIf(id_Emulators Is Nothing, "	WHERE EMUPLTFM.id_Emulators = (SELECT id_Emulators FROM tbl_Emu_Games WHERE id_Emu_Games = " & BS_Emu_Games.Current("id_Emu_Games") & ") OR (EMUPLTFM.DefaultEmulator = 1 AND id_Moby_Platforms = " & TC.getSQLFormat(BS_Emu_Games.Current("id_Moby_Platforms")) & ") ORDER BY EMUPLTFM.DefaultEmulator ", "	WHERE EMU.id_Emulators = " & TC.getSQLFormat(id_Emulators)) & ControlChars.CrLf &
				"	LIMIT 1"

		Dim dt_Emulators As DataTable = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL_DefaultEmu)

		If TC.NZ(id_Emulators, 0) <= 0 Then
			If dt_Emulators Is Nothing OrElse dt_Emulators.Rows.Count < 1 Then
				If MKDXHelper.MessageBox("There is no default DOSBox found for this platform, do you want to set one up?", "No default DOSBox found", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
					Using frm As New frm_Emulators
						frm.ShowDialog(Me.ParentForm)
					End Using
				End If
				Return
			End If
		Else
			If dt_Emulators Is Nothing OrElse dt_Emulators.Rows.Count < 1 Then
				MKDXHelper.MessageBox("ERROR: DOSBox not found.", "DOSBox not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Return
			End If
		End If

		id_Emulators = TC.NZ(dt_Emulators.Rows(0)("id_Emulators"), 0)

		Dim emufullpath As String = TC.NZ(dt_Emulators.Rows(0)("InstallDirectory"), "") & "\" & TC.NZ(dt_Emulators.Rows(0)("Executable"), "")
		If Not Alphaleonis.Win32.Filesystem.File.Exists(emufullpath) Then
			MKDXHelper.MessageBox("The DOSBox executable has not been found: " & emufullpath, "DOSBox exe not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		Dim snapdir As String = TC.NZ(dt_Emulators.Rows(0)("ScreenshotDirectory"), "")
		MKNetLib.cls_MKFileSupport.DeleteContainedFiles(snapdir, cls_Extras._SupportedExtensions_Masks, IO.SearchOption.TopDirectoryOnly, FileIO.UIOption.OnlyErrorDialogs)

		Dim emuexe As String = Alphaleonis.Win32.Filesystem.Path.GetFileName(emufullpath)
		Dim emudir As String = Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(emufullpath)

		Dim Args As String = Prepare_DOSBox(dt_Emulators.Rows(0), BS_Emu_Games.Current.Row, id_Rombase_DOSBox_Exe_Types) 'Autolaunch Main exe

		If Args = "" Then
			Return  'DOSBox Preparation had an error or has been cancelled
		End If

		Dim TempDir As String = ""

		proc.StartInfo.FileName = emufullpath
		proc.StartInfo.WorkingDirectory = emudir

		proc.StartInfo.Arguments = Args

		proc.StartInfo.UseShellExecute = True

		proc.EnableRaisingEvents = True

		AddHandler proc.Exited, AddressOf Handle_Proc_Exited

		'Shell call would be: emufullpath & " " & Args

		Dim J2K_Preset = TC.NZ(dt_Emulators.Rows(0)("J2KPreset"), "")

		If TC.NZ(BS_Emu_Games.Current("J2KPreset"), "").Length > 0 Then
			J2K_Preset = TC.NZ(BS_Emu_Games.Current("J2KPreset"), "")
		End If

		Call_J2K(J2K_Preset)  'Call J2K

		proc.Start()

		Try
			dict_Proc_EmuGames.Add(proc.Id, New cls_Emu_Game_ProcInfo(BS_Emu_Games.Current("id_Emu_Games"), snapdir, cls_Globals.enm_Moby_Platforms.dos))
		Catch ex As Exception

		End Try
	End Sub

	Private Sub Set_Current_Emu_Game_Unavailable(ByVal Unavailable As Boolean, Optional ByRef tran As SQLite.SQLiteTransaction = Nothing)
		If TC.NZ(BS_Emu_Games.Current("Unavailable"), False) <> Unavailable Then
			BS_Emu_Games.Current("Unavailable") = Unavailable
			DS_ML.Update_Emu_Games_Unavailable(BS_Emu_Games.Current("id_Emu_Games"), Unavailable, tran)
			Me.gv_Emu_Games.RefreshData()
		End If
	End Sub

	Private Sub Launch_Game_WIN(ByRef proc As System.Diagnostics.Process)
		Dim fullpath As String = BS_Emu_Games.Current("Folder") & "\" & BS_Emu_Games.Current("File") 'TODO: Inner File? (maybe extract .exe from .lnk - also target directory)

		If Not Alphaleonis.Win32.Filesystem.File.Exists(fullpath) Then
			MKDXHelper.MessageBox("ERROR: The file cannot be found: " & fullpath, "File not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.Set_Current_Emu_Game_Unavailable(True)
			Return
		End If

		Me.Set_Current_Emu_Game_Unavailable(False)

		proc.StartInfo.WorkingDirectory = BS_Emu_Games.Current("Folder")

		If Alphaleonis.Win32.Filesystem.Path.GetExtension(fullpath).ToLower.Replace(".", "") = "lnk" Then
			'get executable and things from lnk info
			Dim lnk As String = fullpath
			fullpath = MKNetLib.cls_MKFileSupport.LNK_GetPath(lnk)

			If Not Alphaleonis.Win32.Filesystem.File.Exists(fullpath) Then
				MKDXHelper.MessageBox("ERROR: The file referenced by the link file cannot be found!" & ControlChars.CrLf & ControlChars.CrLf & "Link file: " & lnk & ControlChars.CrLf & "Referenced file:" & fullpath, "File not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Me.Set_Current_Emu_Game_Unavailable(True)
				Return
			End If

			Me.Set_Current_Emu_Game_Unavailable(False)

			proc.StartInfo.WorkingDirectory = MKNetLib.cls_MKFileSupport.LNK_GetWorkingDirectory(lnk)
			proc.StartInfo.Arguments = MKNetLib.cls_MKFileSupport.LNK_GetArguments(lnk)
		End If


		proc.StartInfo.FileName = fullpath

		'TODO: game-based args - proc.StartInfo.Arguments = Args

		proc.StartInfo.UseShellExecute = True
		proc.EnableRaisingEvents = True

		AddHandler proc.Exited, AddressOf Handle_Proc_Exited

		Call_J2K(TC.NZ(BS_Emu_Games.Current("J2KPreset"), ""))  'Call J2K

		Try
			proc.Start()
		Catch ex As Exception
			MKDXHelper.ExceptionMessageBox(ex)
		End Try

		Try
			dict_Proc_EmuGames.Add(proc.Id, New cls_Emu_Game_ProcInfo(BS_Emu_Games.Current("id_Emu_Games"), "", cls_Globals.enm_Moby_Platforms.win))
		Catch ex As Exception

		End Try
	End Sub

	Private Sub Launch_Game_MAME(ByRef proc As System.Diagnostics.Process)
		Dim bShiftKeyPressed As Boolean = My.Computer.Keyboard.ShiftKeyDown

		Dim romname As String = BS_Emu_Games.Current("File")  'Unique MAME Rom Name

		proc.StartInfo.FileName = TC.NZ(cls_Settings.GetSetting("Mame_Executable"), "") 'MAME Fullpath to mame.exe
		proc.StartInfo.WorkingDirectory = Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(TC.NZ(cls_Settings.GetSetting("Mame_Executable"), ""))  'MAME Folder
		proc.StartInfo.Arguments = romname

		proc.StartInfo.UseShellExecute = True

		proc.EnableRaisingEvents = True

		If bShiftKeyPressed Then
			Using frm As New MKNetDXLib.frm_TextBoxEdit("Command:", "This is the command for launching the emulator.", """" & proc.StartInfo.FileName & """ " & proc.StartInfo.Arguments, True)
				If frm.ShowDialog(Me.ParentForm) <> DialogResult.OK Then
					Return
				End If
			End Using
		End If

		AddHandler proc.Exited, AddressOf Handle_Proc_Exited

		proc.Start()

		Try
			dict_Proc_EmuGames.Add(proc.Id, New cls_Emu_Game_ProcInfo(BS_Emu_Games.Current("id_Emu_Games"), "", cls_Globals.enm_Moby_Platforms.mame))
		Catch ex As Exception

		End Try
	End Sub

	Private Sub Handle_Proc_Exited(ByVal sender As Object, ByVal e As System.EventArgs)
		'Check if user wants to log history, check if enough time went by
		Dim proc As System.Diagnostics.Process = CType(sender, System.Diagnostics.Process)

		Call_J2K()  'Reset J2K

		RemoveHandler proc.Exited, AddressOf Handle_Proc_Exited

		Dim pi As cls_Emu_Game_ProcInfo = Nothing
		Try
			pi = dict_Proc_EmuGames(proc.Id)
		Catch ex As Exception

		End Try

		If pi Is Nothing Then Return

		Dim id_Emu_Games As Integer = pi.id_Emu_Games
		Dim snapdir As String = pi.Snapshot_Directory

		dict_Proc_EmuGames.Remove(proc.Id)

		If TC.NZ(cls_Settings.GetSetting("Stats_Enabled", cls_Settings.enm_Settingmodes.Per_User), True) Then
			Dim MinTime As Integer = TC.NZ(cls_Settings.GetSetting("Stats_MinTime", cls_Settings.enm_Settingmodes.Per_User), 0)

			If MinTime = 0 OrElse DateDiff(DateInterval.Minute, proc.StartTime, proc.ExitTime) >= MinTime Then
				_al_StatsChanges.Add(New cls_3ObjVec(id_Emu_Games, proc.StartTime, proc.ExitTime))
			End If
		End If

		_al_Screenshots_EmuGames.Add(New cls_Emu_Game_ProcInfo(id_Emu_Games, snapdir, pi.Platform))

		'Rescan DOSBox Working Directory (an installer could have been used!)
		If pi.Platform = cls_Globals.enm_Moby_Platforms.dos Then
			frm_Rom_Manager.Rescan_DOSBox_Game(id_Emu_Games)
		End If
	End Sub

	Private Function Get_DOSBox_Exe_Type() As Integer
		Dim id_Rombase_DOSBox_Exe_Types As Integer = cls_Globals.enm_Rombase_DOSBox_Exe_Types.main  'Main .exe

		If My.Computer.Keyboard.ShiftKeyDown Then
			id_Rombase_DOSBox_Exe_Types = cls_Globals.enm_Rombase_DOSBox_Exe_Types.setup  'Setup .exe
		End If

		If My.Computer.Keyboard.CtrlKeyDown Then
			id_Rombase_DOSBox_Exe_Types = cls_Globals.enm_Rombase_DOSBox_Exe_Types.inst 'Installer .exe
		End If

		If My.Computer.Keyboard.ShiftKeyDown AndAlso My.Computer.Keyboard.CtrlKeyDown Then
			id_Rombase_DOSBox_Exe_Types = 0 'No autolaunch
		End If

		Return id_Rombase_DOSBox_Exe_Types
	End Function

	Private Sub grd_Emu_Games_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd_Emu_Games.DoubleClick
		Dim pt As System.Drawing.Point = gv_Emu_Games.GridControl.PointToClient(Control.MousePosition)

		Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = gv_Emu_Games.CalcHitInfo(pt)
		If info.InRowCell Then
			Launch_Game(Nothing, Get_DOSBox_Exe_Type)
		End If
	End Sub

	Private Sub grd_Emu_Games_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd_Emu_Games.KeyDown
		If e.KeyCode = Keys.Enter Then
			Launch_Game(Nothing, Get_DOSBox_Exe_Type)
		End If

		If e.KeyCode = Keys.F5 Then
			Me.Refill_Emu_Games()
		End If
	End Sub

	Public Sub Select_BindingSource_Row_on_gv_Emu_Games()
		Me.gv_Emu_Games.ClearSelection()
		Me.gv_Emu_Games.SelectRow(Me.gv_Emu_Games.FocusedRowHandle)
	End Sub

	Public Sub Refill_Emu_Games()
		Cursor.Current = Cursors.WaitCursor

		If TC.IsNullNothingOrEmpty(cmb_Platform.EditValue) Then
			DS_ML.src_ucr_Emulation_Games.Clear()
			Return
		End If

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction

			Dim id_Emu_Games As Object = Nothing
			If BS_Emu_Games.Current IsNot Nothing Then
				id_Emu_Games = BS_Emu_Games.Current("id_Emu_Games")
			End If

			_Platform_Changing = True
			DS_ML.Fill_src_ucr_Emulation_Games(tran, Me.DS_ML.src_ucr_Emulation_Games, cmb_Platform.EditValue, txb_Search.EditValue, cmb_Filterset.EditValue, Nothing, False, cmb_Groups.EditValue, False, cmb_Staff.EditValue, cmb_Similarity_Calculation_Results.EditValue)

			If id_Emu_Games IsNot Nothing AndAlso BS_Emu_Games.Current IsNot Nothing Then
				BS_Emu_Games.Position = BS_Emu_Games.Find("id_Emu_Games", id_Emu_Games)
				Select_BindingSource_Row_on_gv_Emu_Games()
			End If

			_Platform_Changing = False

			BS_Emu_Games_CurrentChanged(BS_Emu_Games, New System.EventArgs)

			'tran.Commit()
		End Using

		If TC.NZ(cmb_Similarity_Calculation_Results.EditValue, 0) <> 0 Then
			colSimilarity.Visible = True
		Else
			_bbi_Show_Similarity_Feature_Columns_Shown = False
			colSimilarity.Visible = False
			col001_Platform.Visible = False
			col002_MobyRank.Visible = False
			col003_MobyScore.Visible = False
			col004_Publisher.Visible = False
			col005_Developer.Visible = False
			col006_Year.Visible = False
			col101_Basic_Genres.Visible = False
			col102_Perspectives.Visible = False
			col103_Sports_Themes.Visible = False
			col105_Educational_Categories.Visible = False
			col106_Other_Attributes.Visible = False
			col107_Visual_Presentation.Visible = False
			col108_Gameplay.Visible = False
			col109_Pacing.Visible = False
			col110_Narrative_Theme_Topic.Visible = False
			col111_Setting.Visible = False
			col112_Vehicular_Themes.Visible = False
			col113_Interface_Control.Visible = False
			col114_DLC_Addon.Visible = False
			col115_Special_Edition.Visible = False
			col201_MinPlayers.Visible = False
			col202_MaxPlayers.Visible = False
			col203_AgeO.Visible = False
			col204_AgeP.Visible = False
			col205_Rating_Descriptors.Visible = False
			col207_Multiplayer_Attributes.Visible = False
			col206_Other_Attributes.Visible = False
			col301_Group_Membership.Visible = False
			col401_Staff.Visible = False
		End If

		Prepare_filteringUIContext_QueryRangeData()
		filteringUIContext.UpdateMemberBindings()

		Cursor.Current = Cursors.Default
	End Sub

	Public Sub Refill_cmb_Similarity_Calculation_Results()
		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			_Initializing = True
			Dim obj_id As Object = cmb_Similarity_Calculation_Results.EditValue
			DS_ML.Fill_src_ucr_Emulation_cmb_Similarity_Calculation_Results(tran, Me.DS_ML.src_ucr_Emulation_cmb_Similarity_Calculation_Results)
			MKNetLib.cls_MKClientSupport.SetBindingSourcePosition(BS_Similarity_Calculation_Results, "id_Similarity_Calculation_Results", obj_id)
			cmb_Similarity_Calculation_Results.EditValue = obj_id
			_Initializing = False
		End Using
	End Sub


	Private Sub Handle_LabelsMouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
		'We need to focus here, else we can't assure scrolling by mouse wheel
		Me.spnl_Summary.Focus()
	End Sub

	Private Sub InsertDescriptionBlock(ByVal title As String, ByVal text As Object)
		If Not TC.IsNullNothingOrEmpty(text) Then
			Dim lbl As New MKNetDXLib.ctl_MKDXLabel
			'AddHandler lbl.MouseWheel, AddressOf Handle_LabelsMouseWheelEvent
			AddHandler lbl.MouseEnter, AddressOf Handle_LabelsMouseEnter
			lbl.Font = New Font("SegoeUI", 8.25)
			lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
			lbl.Padding = New Padding(0, 0, 0, 3)
			lbl.Text = text 'MKNetLib.cls_MKISO_8859_1_Replace.ToASCII(System.Text.RegularExpressions.Regex.Replace(text.Replace("<br>", ControlChars.CrLf), "<.*?>", ""), True).Replace("&", "&&")
			Me.spnl_Summary.Controls.Add(lbl)
			lbl.Dock = DockStyle.Top

			lbl = New MKNetDXLib.ctl_MKDXLabel
			AddHandler lbl.MouseEnter, AddressOf Handle_LabelsMouseEnter
			lbl.Font = New Font("SegoeUI", 10, FontStyle.Bold)
			lbl.Padding = New Padding(0, 0, 0, 3)
			lbl.Text = title.Replace("&", "&&")
			Me.spnl_Summary.Controls.Add(lbl)
			lbl.Dock = DockStyle.Top
		End If
	End Sub

	Private Sub InsertTechnicalSpecs(ByVal id_Emu_Games As Integer)
		Dim sSQL As String = ""
		sSQL &= "	SELECT" & ControlChars.CrLf
		sSQL &= "		MAC.Name as Category" & ControlChars.CrLf
		sSQL &= "		, MA.Name as Attribute" & ControlChars.CrLf
		sSQL &= "		, MA.Description" & ControlChars.CrLf
		sSQL &= "	FROM" & ControlChars.CrLf
		sSQL &= "	(" & ControlChars.CrLf
		sSQL &= "		SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "		FROM tbl_Moby_Releases_Attributes RA" & ControlChars.CrLf
		sSQL &= "		WHERE RA.id_Moby_Releases = (SELECT id_Moby_Releases FROM tbl_Emu_Games EG LEFT JOIN tbl_Moby_Releases REL ON REL.id_Moby_Platforms = IFNULL(EG.id_Moby_Platforms_Alternative, EG.id_Moby_Platforms) AND REL.id_Moby_Games = (SELECT id_Moby_Games FROM tbl_Moby_Games MG WHERE URLPart = EG.Moby_Games_URLPart) WHERE EG.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & ")" & ControlChars.CrLf
		sSQL &= "		UNION SELECT id_Moby_Attributes FROM tbl_Emu_Games_Moby_Attributes EGMA" & ControlChars.CrLf
		sSQL &= "		WHERE EGMA.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & ControlChars.CrLf
		sSQL &= "	) AS temp_Attributes" & ControlChars.CrLf
		sSQL &= "	LEFT JOIN tbl_Moby_Attributes MA ON temp_Attributes.id_Moby_Attributes = MA.id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "	INNER JOIN tbl_Moby_Attributes_Categories MAC ON MAC.id_Moby_Attributes_Categories = MA.id_Moby_Attributes_Categories" & ControlChars.CrLf
		sSQL &= "	WHERE MA.id_Moby_Attributes NOT IN" & ControlChars.CrLf
		sSQL &= "	(" & ControlChars.CrLf
		sSQL &= "		SELECT id_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "		FROM tbl_Emu_Games_Moby_Attributes" & ControlChars.CrLf
		sSQL &= "		WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND Used = 0" & ControlChars.CrLf
		sSQL &= "	)" & ControlChars.CrLf
		sSQL &= "	ORDER BY Category, Attribute"

		DS_ML.tbl_Technical_Specs.Clear()
		DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, DS_ML.tbl_Technical_Specs)
		If DS_ML.tbl_Technical_Specs.Rows.Count = 0 Then
			lbl_NoSpecs.Visible = True
			Me.grd_TechnicalSpecs.Visible = False
		Else
			lbl_NoSpecs.Visible = False
			Me.grd_TechnicalSpecs.Visible = True
		End If
	End Sub

	Private Sub Update_Description()
		spnl_Summary.Controls.Clear()

		spnl_Summary.SuspendLayout()

		If BS_Emu_Games.Current Is Nothing Then
			Return
		End If

		InsertTechnicalSpecs(BS_Emu_Games.Current("id_Emu_Games"))

		InsertDescriptionBlock("Description", BS_Emu_Games.Current("Description"))
		InsertDescriptionBlock("Alternate Titles", BS_Emu_Games.Current("Alternate_Titles"))
		InsertDescriptionBlock("Special Info", BS_Emu_Games.Current("SpecialInfo"))

		spnl_Summary.ResumeLayout()
	End Sub

	Private Sub BS_Emu_Games_CurrentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BS_Emu_Games.CurrentChanged
		If _Initializing Then Return
		If _Platform_Changing Then Return

		tmr_ImageUpdate.Stop()

		If BS_Emu_Games.Current Is Nothing Then
			pic_Game.Image = Nothing
			lbl_Displayname.Text = ""
			DS_ML.tbl_History.Clear()
			DS_ML.src_ucr_Emulation_GameGroups.Clear()
			lbl_Emu_Games_Playcount.Text = ""
			lbl_Emu_Games_Runtime_Value.Text = ""
			Return
		End If

		lbl_Displayname.Text = TC.NZ(BS_Emu_Games.Current("Game"), "").Replace("&", "&&")

		If TC.NZ(BS_Emu_Games.Current("id_Moby_Platforms"), 0) = cls_Globals.enm_Moby_Platforms.win AndAlso TC.NZ(BS_Emu_Games.Current("Game"), "nope1").ToLower = TC.NZ(BS_Emu_Games.Current("InnerFile"), "nope2").ToLower Then
			lbl_Displayname.Text = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(BS_Emu_Games.Current("Game"))
		End If

		Update_Description()

		tmr_ImageUpdate.Start()

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Fill_tbl_History(tran, DS_ML.tbl_History, BS_Emu_Games.Current("id_Emu_Games"))

			lbl_Emu_Games_Playcount.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT COUNT(1) FROM tbl_History WHERE id_Users " & IIf(cls_Globals.Admin, "IS NULL", "= " & TC.getSQLFormat(cls_Globals.id_Users)) & " AND id_Emu_Games = " & TC.getSQLFormat(BS_Emu_Games.Current("id_Emu_Games")), tran), 0)
			lbl_Emu_Games_Runtime_Value.Text = MKNetLib.cls_MKStringSupport.GetTimeString(TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT SUM(strftime('%s', End) - strftime('%s', Start)) FROM tbl_History WHERE id_Users " & IIf(cls_Globals.Admin, "IS NULL", "= " & TC.getSQLFormat(cls_Globals.id_Users)) & " AND id_Emu_Games = " & TC.getSQLFormat(BS_Emu_Games.Current("id_Emu_Games")), tran), 0))

			DS_ML.Fill_src_ucr_Emulation_Game_Groups(tran, DS_ML.src_ucr_Emulation_GameGroups, BS_Emu_Games.Current("id_Moby_Games"), TC.NZ(BS_Emu_Games.Current("id_Moby_Platforms_Alternative"), BS_Emu_Games.Current("id_Moby_Platforms")))
			If DS_ML.src_ucr_Emulation_GameGroups.Rows.Count = 0 Then
				Me.lbl_Game_Groups.Text = "This game is not part of any group."
				Me.grd_Game_Groups.Visible = False
			Else
				Me.lbl_Game_Groups.Text = "This game is part of the following groups:"
				Me.grd_Game_Groups.Visible = True
			End If

			DS_ML.Fill_src_ucr_Emulation_Moby_Releases_Staff(tran, DS_ML.src_ucr_Emulation_Moby_Releases_Staff, BS_Emu_Games.Current("id_Moby_Games"), TC.NZ(BS_Emu_Games.Current("id_Moby_Platforms_Alternative"), BS_Emu_Games.Current("id_Moby_Platforms")))
			If DS_ML.src_ucr_Emulation_Moby_Releases_Staff.Rows.Count = 0 Then
				Me.lbl_Staff_Grid.Text = "There is no staff listed for this game."
				Me.grd_Staff.Visible = False
			Else
				Me.lbl_Staff_Grid.Text = "This game has been created by the following staff:"
				Me.grd_Staff.Visible = True
			End If

			DS_ML.Fill_src_ucr_Emulation_Moby_Releases_Screenshots(tran, Me.DS_ML.src_ucr_Emulation_Moby_Releases_Screenshots, BS_Emu_Games.Current("id_Moby_Releases"))
			DS_ML.Fill_src_ucr_Emulation_Moby_Releases_Cover_Art(tran, Me.DS_ML.src_ucr_Emulation_Moby_Releases_Cover_Art, BS_Emu_Games.Current("id_Moby_Releases"))

			Debug.WriteLine(BS_Emu_Games.Current("Game") & " EXTRAS: Moby Screenshots: " & Me.DS_ML.src_ucr_Emulation_Moby_Releases_Screenshots.Rows.Count)
			Debug.WriteLine(BS_Emu_Games.Current("Game") & " EXTRAS: Moby Cover Art: " & Me.DS_ML.src_ucr_Emulation_Moby_Releases_Cover_Art.Rows.Count)

			'tran.Commit()
		End Using

		ApplyFirstExtra()
	End Sub

	Private Sub pic_Game_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pic_Game.Click
		ApplyNextExtra(True, True, True)
		tmr_ImageUpdate.Stop()
		tmr_ImageUpdate.Start()
	End Sub

	''' <summary>
	''' Get the first extra
	''' </summary>
	''' <remarks></remarks>
	Private Sub ApplyFirstExtra()
		Extras_Mode = enm_ExtrasMode.User

		pic_Game.Image = Nothing

		ExtraType = DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Name FROM tbl_Emu_Extras WHERE Sort = (SELECT MIN(Sort) from tbl_Emu_Extras WHERE IFNULL(Hide, 0) = 0 LIMIT 1)")
		ExtraNum = 0
		NoExtraFound = False

		Moby_ExtraNum = 0
		Moby_Extra_isDownloading = False

		Me.Moby_Download_Info = Nothing

		ApplyNextExtra(False, True, True)

		tmr_ImageUpdate.Start()
	End Sub

	Private Sub Analyze_Missing_Extras()
		Dim SearchMode As Integer = 0

		Using frm As New frm_Search_Missing_Extras
			If frm.ShowDialog(Me.ParentForm) <> DialogResult.OK Then
				Return
			End If

			SearchMode = TC.NZ(frm.cmb_Extra_Type.EditValue, 0)
		End Using

		Me.Cursor = Cursors.WaitCursor

		Dim extraType As String = DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Name FROM tbl_Emu_Extras " & IIf(SearchMode = 0, "", "WHERE id_Emu_Extras = " & TC.getSQLFormat(SearchMode)) & " LIMIT 1")

		For Each row As DataRow In Me.DS_ML.src_ucr_Emulation_Games.Rows
			If TC.IsNullNothingOrEmpty(row("id_Emu_Games_Owner")) Then
				Dim res As cls_Extras.cls_Extras_Result = cls_Extras.FindNextExtra(row("id_Emu_Games"), 0, False, extraType, True, IIf(SearchMode = 0, False, True))
				row("tmp_Highlighted") = res._NoExtraFound
			End If
		Next
		Me.Cursor = Cursors.Default
	End Sub

	''' <summary>
	''' Get the next extra
	''' </summary>
	''' <remarks></remarks>
	Private Sub ApplyNextExtra(ByVal SkipToNextImmediately As Boolean, Optional ByVal DoRecurse As Boolean = True, Optional ByVal ApplyExtraDescription As Boolean = False)
		If ApplyExtraDescription Then
			Me.lbl_Extras_Description.Text = ""
		End If

		If Extras_Mode = enm_ExtrasMode.User Then
			Debug.WriteLine("EXTRAS: User Mode, ExtraNum = " & Me.ExtraNum & ", ExtraType = " & Me.ExtraType & ", SkipToNextImmediately = " & SkipToNextImmediately)

			If BS_Emu_Games.Current Is Nothing Then Return
			If TC.NZ(ExtraType, "").Length = 0 Then Return

			Dim res As cls_Extras.cls_Extras_Result = cls_Extras.FindNextExtra(BS_Emu_Games.Current("id_Emu_Games"), Me.ExtraNum, SkipToNextImmediately, Me.ExtraType)

			If res._WrappedToFirst Then
				Debug.WriteLine("EXTRAS: Wrapped to first, changing to Moby mode")
				Me.ExtraType = res._ExtraType
				Extras_Mode = enm_ExtrasMode.Moby
				If DoRecurse Then ApplyNextExtra(False, True)
				Return
			ElseIf res._NoExtraFound Then
				Debug.WriteLine("EXTRAS: No Extra found, changing to Moby mode")
				Extras_Mode = enm_ExtrasMode.Moby
				If DoRecurse Then ApplyNextExtra(False, False)
				Return
			Else
				Debug.WriteLine("EXTRAS: Image found, loading for display")
				pic_Game.Image = Image.FromStream(New IO.MemoryStream(Alphaleonis.Win32.Filesystem.File.ReadAllBytes(res._Path))) 'Image.FromFile(res._Path)
				Me.ExtraType = res._ExtraType
				Me.ExtraNum = res._ExtraNum
			End If
		End If

		If Extras_Mode = enm_ExtrasMode.Moby Then
			Debug.WriteLine("EXTRAS: Moby Mode")

			If Moby_Extras_Downloader.IsBusy Then
				Debug.WriteLine("EXTRAS: Downloader is busy, aborting")
				Return
			End If

			If Me.DS_ML.src_ucr_Emulation_Moby_Releases_Cover_Art.Rows.Count = 0 AndAlso Me.DS_ML.src_ucr_Emulation_Moby_Releases_Screenshots.Rows.Count = 0 Then
				Debug.WriteLine("EXTRAS: no Cover Art and no Screenshots found, aborting")
				Me.Extras_Mode = enm_ExtrasMode.User
				If DoRecurse Then ApplyNextExtra(False, False)
				Return
			Else
				Debug.WriteLine("EXTRAS: Cover Art: " & Me.DS_ML.src_ucr_Emulation_Moby_Releases_Cover_Art.Rows.Count & ", Screenshots: " & Me.DS_ML.src_ucr_Emulation_Moby_Releases_Screenshots.Rows.Count)
			End If

			If SkipToNextImmediately Then
				Debug.WriteLine("EXTRAS: increasing Moby_ExtraNum")
				Me.Moby_ExtraNum += 1
			End If

			Dim dirpath As Object = cls_Settings.Get_Extras_Directory()
			If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
				Debug.WriteLine("EXTRAS: Extras_Directory not found, aborting")
				Return
			End If

			Dim extranum As Integer = Me.Moby_ExtraNum

			If Me.DS_ML.src_ucr_Emulation_Moby_Releases_Cover_Art.Rows.Count > extranum Then
				'>> Cover Art <<
				Debug.WriteLine("EXTRAS: using Cover Art")

				Dim row_Cover_Art As DS_ML.src_ucr_Emulation_Moby_Releases_Cover_ArtRow = Me.DS_ML.src_ucr_Emulation_Moby_Releases_Cover_Art.Rows(extranum)

				If TC.IsNullNothingOrEmpty(row_Cover_Art("URL")) OrElse TC.NZ(row_Cover_Art("URL"), "").Split("/").Length < 2 Then
					Debug.WriteLine("EXTRAS: Cover Art URL missing or malformed: " & TC.NZ(row_Cover_Art("URL"), "<missing>"))
					Return
				End If

				Dim description As String = TC.NZ(row_Cover_Art("tmp_Description"), "").Replace("\n", ControlChars.CrLf).Trim

				If ApplyExtraDescription Then
					Me.lbl_Extras_Description.Text = description
				End If

				Dim url As String = row_Cover_Art("URL")
				Dim filename As String = url.Split("/")(url.Split("/").Length - 1)

				dirpath &= "\moby"
				If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
					Try
						Alphaleonis.Win32.Filesystem.Directory.CreateDirectory(dirpath)
					Catch ex As Exception

					End Try
				End If
				If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
					Debug.WriteLine("EXTRAS: Cannot create directory " & dirpath & ", aborting")
					Return
				End If

				dirpath &= "\cover-art"
				If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
					Try
						Alphaleonis.Win32.Filesystem.Directory.CreateDirectory(dirpath)
					Catch ex As Exception

					End Try
				End If
				If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
					Debug.WriteLine("EXTRAS: Cannot create directory " & dirpath & ", aborting")
					Return
				End If

				dirpath &= "\" & BS_Emu_Games.Current("Platform_Short")
				If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
					Try
						Alphaleonis.Win32.Filesystem.Directory.CreateDirectory(dirpath)
					Catch ex As Exception

					End Try
				End If
				If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
					Debug.WriteLine("EXTRAS: Cannot create directory " & dirpath & ", aborting")
					Return
				End If

				Dim filepath As String = dirpath & "\" & filename

				If cls_Extras.EnsureExtrasFile(filepath) Then
					'Apply here
					Debug.WriteLine("EXTRAS: Extra found, loading for display")
					pic_Game.Image = Image.FromStream(New IO.MemoryStream(Alphaleonis.Win32.Filesystem.File.ReadAllBytes(filepath)))
				Else
					'Run the Downloader
					Debug.WriteLine("EXTRAS: Extra not found, starting the download")
					Download_Moby_Extra(url, filepath, description, ApplyExtraDescription)
				End If
			Else
				Debug.WriteLine("EXTRAS: using Screenshots")

				'>> Screenshots <<
				extranum = extranum - DS_ML.src_ucr_Emulation_Moby_Releases_Cover_Art.Rows.Count

				If Not DS_ML.src_ucr_Emulation_Moby_Releases_Screenshots.Rows.Count > extranum Then
					Debug.WriteLine("EXTRAS: we hit the ceiling, switching to User now")
					Me.Moby_ExtraNum = 0
					Me.ExtraNum = 0
					Me.Extras_Mode = enm_ExtrasMode.User
					If DoRecurse Then Me.ApplyNextExtra(False, True)
					Return
				End If

				Dim row_Screenshots As DS_ML.src_ucr_Emulation_Moby_Releases_ScreenshotsRow = Me.DS_ML.src_ucr_Emulation_Moby_Releases_Screenshots.Rows(extranum)

				If TC.IsNullNothingOrEmpty(row_Screenshots("URL")) OrElse TC.NZ(row_Screenshots("URL"), "").Split("/").Length < 2 Then
					Debug.WriteLine("EXTRAS: Screenshots URL missing or malformed: " & TC.NZ(row_Screenshots("URL"), "<missing>"))
					Return
				End If

				Dim description As String = TC.NZ(row_Screenshots("tmp_Description"), "").Replace("\n", ControlChars.CrLf).Trim

				If ApplyExtraDescription Then
					Me.lbl_Extras_Description.Text = description
				End If

				Dim url As String = row_Screenshots("URL")
				Dim filename As String = url.Split("/")(url.Split("/").Length - 1)

				dirpath &= "\moby"
				If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
					Try
						Alphaleonis.Win32.Filesystem.Directory.CreateDirectory(dirpath)
					Catch ex As Exception

					End Try
				End If
				If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
					Debug.WriteLine("EXTRAS: Cannot create directory " & dirpath & ", aborting")
					Return
				End If

				dirpath &= "\screenshots"
				If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
					Try
						Alphaleonis.Win32.Filesystem.Directory.CreateDirectory(dirpath)
					Catch ex As Exception

					End Try
				End If
				If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
					Debug.WriteLine("EXTRAS: Cannot create directory " & dirpath & ", aborting")
					Return
				End If

				dirpath &= "\" & BS_Emu_Games.Current("Platform_Short")
				If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
					Try
						Alphaleonis.Win32.Filesystem.Directory.CreateDirectory(dirpath)
					Catch ex As Exception

					End Try
				End If
				If Not Alphaleonis.Win32.Filesystem.Directory.Exists(dirpath) Then
					Debug.WriteLine("EXTRAS: Cannot create directory " & dirpath & ", aborting")
					Return
				End If

				Dim filepath As String = dirpath & "\" & filename

				If cls_Extras.EnsureExtrasFile(filepath) Then
					'Apply here
					Debug.WriteLine("EXTRAS: Extra found, loading for display")
					pic_Game.Image = Image.FromStream(New IO.MemoryStream(Alphaleonis.Win32.Filesystem.File.ReadAllBytes(filepath)))
				Else
					'Run the Downloader
					Debug.WriteLine("EXTRAS: Extra not found, starting the download")
					Download_Moby_Extra(url, filepath, description, ApplyExtraDescription)
				End If
			End If
		End If
	End Sub

	Private Sub Download_Moby_Extra(url, filepath, description, applyDescription)
		Try
			If Not TC.NZ(cls_Settings.GetSetting("Downloader_Enabled", cls_Settings.enm_Settingmodes.Same_for_All), True) Then
				Return
			End If
		Catch ex As Exception

		End Try

		Me.pic_Game.Cursor = Cursors.WaitCursor
		Me.prg_Extras_Download.EditValue = 0
		Me.prg_Extras_Download.Visible = True

		Me.Moby_Download_Info = New cls_Moby_Download_Info(url, filepath, description, applyDescription)

		Me.Moby_Extras_Downloader.DownloadFileAsync(New Uri("http://www.mobygames.com" & url), filepath)
	End Sub


	Private Sub Moby_Extras_Downloader_DownloadFileCompleted(sender As Object, e As AsyncCompletedEventArgs) Handles Moby_Extras_Downloader.DownloadFileCompleted
		Debug.WriteLine("EXTRAS: Download completed event")

		Me.prg_Extras_Download.Visible = False
		Me.pic_Game.Cursor = Cursors.Hand

		If e.Cancelled Then
			Debug.WriteLine("EXTRAS: Download was cancelled, aborting")
			Return
		End If

		If e.Error IsNot Nothing Then
			Debug.WriteLine("EXTRAS: Download has errored: " & e.Error.Message & e.Error.StackTrace)
			Return
		End If


		If Me.Moby_Download_Info Is Nothing Then
			Debug.WriteLine("EXTRAS: After Download: Game has changed, aborting")
			Me.ApplyFirstExtra()
			Return
		End If

		If Not Alphaleonis.Win32.Filesystem.File.Exists(Me.Moby_Download_Info.Filepath) Then
			Debug.WriteLine("EXTRAS: After Download: File not found, aborting")
			Return
		End If

		Debug.WriteLine("EXTRAS: After Download: Loading for display")
		If cls_Extras.EnsureExtrasFile(Me.Moby_Download_Info.Filepath) Then
			pic_Game.Image = Image.FromStream(New IO.MemoryStream(Alphaleonis.Win32.Filesystem.File.ReadAllBytes(Me.Moby_Download_Info.Filepath)))
		End If

		If Me.Moby_Download_Info.ApplyExtraDescription Then
			Me.lbl_Extras_Description.Text = Me.Moby_Download_Info.Description
		End If
	End Sub

	Private Sub cmb_Platform_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_Platform.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis Then
			Using frm As New frm_Moby_Platforms_Configuration
				If frm.ShowDialog(Me.ParentForm) = DialogResult.OK Then
					Dim objPltfm As Object = cmb_Platform.EditValue

					Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
						Me.DS_ML.Fill_src_ucr_Emulators_Platforms(tran, Me.DS_ML.src_ucr_Emulation_Platforms)
						tran.Commit()
					End Using

					If Not TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Visible FROM main.tbl_Moby_Platforms_Settings WHERE id_Moby_Platforms = " & TC.getSQLFormat(objPltfm)), True) Then
						cmb_Platform.EditValue = -1
					End If
				End If
			End Using
		End If
	End Sub

	Private Sub cmb_Platform_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles cmb_Platform.EditValueChanging
		Save_EmuGame_Position()
	End Sub

	Private Sub cmb_Platform_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_Platform.EditValueChanged
		If _Initializing Then Return
		Refill_Emu_Games()
		Load_EmuGame_Position()
	End Sub

	Private Sub cmb_Group_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_Groups.EditValueChanged
		For Each btn As DevExpress.XtraEditors.Controls.EditorButton In cmb_Groups.Properties.Buttons
			If btn.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis Then
				If TC.NZ(cmb_Groups.EditValue, 0) = 0 Then
					btn.Enabled = False
				Else
					btn.Enabled = True
				End If
			End If
		Next

		If _Initializing Then Return
		Refill_Emu_Games()
	End Sub

	Private Sub cmb_Staff_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_Staff.EditValueChanged
		For Each btn As DevExpress.XtraEditors.Controls.EditorButton In cmb_Staff.Properties.Buttons
			If btn.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis Then
				If TC.NZ(cmb_Staff.EditValue, 0) = 0 Then
					btn.Enabled = False
				Else
					btn.Enabled = True
				End If
			End If
		Next

		If _Initializing Then Return
		Refill_Emu_Games()
	End Sub

	Private Sub txb_Search_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txb_Search.ButtonClick
		Refill_Emu_Games()
	End Sub

	Private Sub txb_Search_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txb_Search.KeyDown
		If e.KeyCode = Keys.Enter Then
			Refill_Emu_Games()
		End If
	End Sub

	Private Sub Apply_cmb_Similarity_Calculation_Results_Buttons_Enabled()
		If TC.NZ(cmb_Similarity_Calculation_Results.EditValue, 0) = 0 Then
			For Each btn As DevExpress.XtraEditors.Controls.EditorButton In cmb_Similarity_Calculation_Results.Properties.Buttons
				If {DevExpress.XtraEditors.Controls.ButtonPredefines.Minus, DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis}.Contains(btn.Kind) Then
					btn.Enabled = False
				End If
			Next
		Else
			For Each btn As DevExpress.XtraEditors.Controls.EditorButton In cmb_Similarity_Calculation_Results.Properties.Buttons
				If {DevExpress.XtraEditors.Controls.ButtonPredefines.Minus, DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis}.Contains(btn.Kind) Then
					btn.Enabled = True
				End If
			Next
		End If
	End Sub

	Private Sub cmb_Similarity_Calculation_Results_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_Similarity_Calculation_Results.EditValueChanged
		Apply_cmb_Similarity_Calculation_Results_Buttons_Enabled()

		If _Initializing Then Return
		Refill_Emu_Games()
	End Sub

	Private Sub tmr_ImageUpdate_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmr_ImageUpdate.Tick
		If _Slideshow Then
			ApplyNextExtra(True, True, True)
		End If
	End Sub

	Private Sub ttctl_TecSpec_DefaultController_GetActiveObjectInfo(ByVal sender As System.Object, ByVal e As DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs) Handles ttctl_TecSpec.DefaultController.GetActiveObjectInfo
		Try
			If e.SelectedControl Is Me.grd_TechnicalSpecs Then
				Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = gv_TechnicalSpecs.CalcHitInfo(e.ControlMousePosition)
				If info.InRowCell Then
					Dim sCategory = gv_TechnicalSpecs.GetRowCellDisplayText(info.RowHandle, "Category")
					Dim sAttribute As String = gv_TechnicalSpecs.GetRowCellDisplayText(info.RowHandle, "Attribute")
					Dim sDescription As String = gv_TechnicalSpecs.GetRow(info.RowHandle).Row("Description")
					Dim cellKey As String = info.RowHandle.ToString() & " - " & info.Column.ToString()
					Dim result As New DevExpress.Utils.ToolTipControlInfo(cellKey, "asd")
					result.SuperTip = New DevExpress.Utils.SuperToolTip
					Dim args As New DevExpress.Utils.SuperToolTipSetupArgs
					args.Title.Text = sCategory & ": " & sAttribute
					args.Contents.Text = sDescription
					result.SuperTip.Setup(args)
					result.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip
					result.Interval = 0
					e.Info = result
				End If
			End If
		Catch ex As Exception

		End Try
	End Sub

	Dim stream As IO.MemoryStream
	Dim _Filters As String = ""

	Private Sub cmb_Filterset_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_Filterset.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
			Using frm As New frm_FilterSet(Nothing, MKNetDXLib.cls_MKDXGrid_Serializer.SaveLayoutBase64(Me.gv_Emu_Games, MKNetDXLib.enm_MKDXGrid_Serialize_Options.Filters), Nothing, True, "New Filterset")
				If frm.ShowDialog(Me.ParentForm) = DialogResult.OK Then
					Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
						Dim id_FilterSets As Object = DS_ML.Upsert_tbl_Filtersets(tran, frm.id_FilterSet, Metropolis_Launcher.DS_ML.enm_FilterSetTypes.Emulation, frm.FilterSet_Name, frm.UseQuickFilter, frm.QuickFilter)
						Me.DS_ML.Fill_tbl_FilterSets(tran, Me.DS_ML.tbl_FilterSets, Metropolis_Launcher.DS_ML.enm_FilterSetTypes.Emulation)
						tran.Commit()

						Me.cmb_Filterset.EditValue = CLng(id_FilterSets)
					End Using
				End If
			End Using
		End If

		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Minus Then
			If MKDXHelper.MessageBox("Do you really want to delete this Filterset?", "Delete Filterset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_FilterSets WHERE id_FilterSets = " & TC.getSQLFormat(cmb_Filterset.EditValue))

				Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
					Me.DS_ML.Fill_tbl_FilterSets(tran, Me.DS_ML.tbl_FilterSets, Metropolis_Launcher.DS_ML.enm_FilterSetTypes.Emulation)
					tran.Commit()

					Me.cmb_Filterset.EditValue = CLng(0)
				End Using
			End If
		End If

		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis Then
			Using frm As New frm_FilterSet(BS_FilterSets.Current("id_FilterSets"), MKNetDXLib.cls_MKDXGrid_Serializer.SaveLayoutBase64(Me.gv_Emu_Games, MKNetDXLib.enm_MKDXGrid_Serialize_Options.Filters), BS_FilterSets.Current("GridFilter"), BS_FilterSets.Current("ApplyGridFilter"), BS_FilterSets.Current("Name"))
				If frm.ShowDialog(Me.ParentForm) = DialogResult.OK Then
					Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
						Dim id_FilterSets As Object = DS_ML.Upsert_tbl_Filtersets(tran, frm.id_FilterSet, Metropolis_Launcher.DS_ML.enm_FilterSetTypes.Emulation, frm.FilterSet_Name, frm.UseQuickFilter, frm.QuickFilter)
						Me.DS_ML.Fill_tbl_FilterSets(tran, Me.DS_ML.tbl_FilterSets, Metropolis_Launcher.DS_ML.enm_FilterSetTypes.Emulation)
						tran.Commit()

						Me.cmb_Filterset.EditValue = CLng(id_FilterSets)
					End Using
				End If
			End Using
		End If
	End Sub

	Private Sub grd_Emu_Games_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd_Emu_Games.MouseEnter
		grd_Emu_Games.Focus()
	End Sub

	Private Sub Handle_LaunchBarItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
		Launch_Game(e.Item.Tag, Get_DOSBox_Exe_Type)
	End Sub

#Region "Popupmenu"

	Private _bbi_Show_Similarity_Feature_Columns_Shown As Boolean = False

	Private Sub popmnu_Emulation_BeforePopup(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles popmnu_Emu.BeforePopup
		If Not grd_Emu_Games.Allow_Popup Then
			e.Cancel = True
			Return
		End If

		If cls_Globals.Restricted OrElse Not cls_Globals.MultiUserMode Then
			bsi_MultiUser.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		Else
			bsi_MultiUser.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
		End If

		bbi_Open_Moby_Page.Enabled = False
		bbi_Contribute_TechInfo.Enabled = False
		bsi_Launch.Enabled = False
		bbi_Launch_Random.Enabled = False
		bbi_DOSBox_Templates.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		bbi_DOSBox_Clear_Exe_Config.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		bbi_ScummVM_Templates.Visibility = DevExpress.XtraBars.BarItemVisibility.Never


		bbi_Edit_Game.Enabled = False
		bbi_Edit_Multiple_Games.Enabled = False
		bsi_Export.Enabled = False

		bbi_Analyze_Missing_Extras.Enabled = False

		bbi_MultiUser_Add_Games.Enabled = False
		bbi_MultiUser_Show_Games.Enabled = False
		bbi_MultiUser_Remove_Games.Enabled = False

		'		bbi_Rombase_Manager.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

		'#If DEBUG Then
		'		bbi_Rombase_Manager.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
		'#End If

		bbi_Similarity_Calculation.Enabled = False
		bbi_Show_Similarity_Feature_Columns.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		bbi_Open_Similarity_Details.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

		If MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Emu_Games).Length > 0 Then
			If BS_Emu_Games.Current IsNot Nothing Then
				bbi_Edit_Game.Enabled = True
				bbi_Edit_Multiple_Games.Enabled = True
				bsi_Export.Enabled = True

				bbi_Analyze_Missing_Extras.Enabled = True

				bbi_MultiUser_Add_Games.Enabled = True
				bbi_MultiUser_Show_Games.Enabled = True

				bbi_Similarity_Calculation.Enabled = True

				If TC.NZ(cmb_Similarity_Calculation_Results.EditValue, 0) Then
					bbi_Show_Similarity_Feature_Columns.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
					bbi_Open_Similarity_Details.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

					If _bbi_Show_Similarity_Feature_Columns_Shown Then
						bbi_Show_Similarity_Feature_Columns.Caption = _bbi_Show_Similarity_Feature_Columns_Caption.Replace("{0}", "Hide")
					Else
						bbi_Show_Similarity_Feature_Columns.Caption = _bbi_Show_Similarity_Feature_Columns_Caption.Replace("{0}", "Show")
					End If
				End If

				If {cls_Globals.enm_Moby_Platforms.dos, cls_Globals.enm_Moby_Platforms.pcboot}.Contains(TC.NZ(BS_Emu_Games.Current("id_Moby_Platforms"), 0)) Then 'DOS or PC Booter
					bbi_DOSBox_Templates.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
					bbi_DOSBox_Clear_Exe_Config.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
				End If

				If {cls_Globals.enm_Moby_Platforms.scummvm}.Contains(TC.NZ(BS_Emu_Games.Current("id_Moby_Platforms"), 0)) Then
					bbi_ScummVM_Templates.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
				End If

				If Not TC.IsNullNothingOrEmpty(BS_Emu_Games.Current("File")) Then

					Dim sSQL_Emus As String = "	SELECT" & ControlChars.CrLf &
					"		EMU.id_Emulators" & ControlChars.CrLf &
					"		, EMU.Displayname" & ControlChars.CrLf &
					"		, EMU.InstallDirectory" & ControlChars.CrLf &
					"		, EMU.Executable" & ControlChars.CrLf &
					"		, EMU.StartupParameter" & ControlChars.CrLf &
					"		, EMU.AutoItScript" & ControlChars.CrLf &
					"		, EMU.J2KPreset" & ControlChars.CrLf &
					"		, EMU.ScreenshotDirectory" & ControlChars.CrLf &
					"		, EMUPLTFM.DefaultEmulator AS DefaultEmulator_Global" & ControlChars.CrLf &
					"		, CASE WHEN EMU.id_Emulators = EMUGAME.id_Emulators THEN 1 ELSE 0 END AS DefaultEmulator" & ControlChars.CrLf &
					"	FROM tbl_Emulators_Moby_Platforms EMUPLTFM" & ControlChars.CrLf &
					"	LEFT JOIN tbl_Emulators EMU ON EMUPLTFM.id_Emulators = EMU.id_Emulators" & ControlChars.CrLf &
					"	LEFT JOIN tbl_Emu_Games EMUGAME ON EMUGAME.id_Emu_Games = " & TC.getSQLFormat(BS_Emu_Games.Current("id_Emu_Games")) & ControlChars.CrLf &
					"	WHERE EMUPLTFM.id_Moby_Platforms = " & TC.getSQLFormat(BS_Emu_Games.Current("id_Moby_Platforms")) & ControlChars.CrLf &
					" ORDER BY EMU.DisplayName"

					Dim dt As DataTable = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL_Emus)

					'Dynamic Launch Menu Bar Items
					For Each bbi As DevExpress.XtraBars.BarItemLink In bsi_Launch.ItemLinks
						RemoveHandler bbi.Item.ItemClick, AddressOf Handle_LaunchBarItemClick
					Next

					bsi_Launch.ClearLinks()

					For Each row As DataRow In dt.Rows
						Dim bbi As New DevExpress.XtraBars.BarButtonItem(Me.barmng, row("DisplayName") & IIf(TC.NZ(row("DefaultEmulator_Global"), False), " (Global Default)", IIf(TC.NZ(row("DefaultEmulator"), False), " (Default)", "")))
						bbi.Tag = row("id_Emulators")
						AddHandler bbi.ItemClick, AddressOf Handle_LaunchBarItemClick

						bsi_Launch.AddItem(bbi)
					Next

					''PC - Windows Platform
					'If TC.NZ(BS_Emu_Games.Current("id_Moby_Platforms"), 0) = cls_Globals.enm_Moby_Platforms.win Then

					'End If

					''M.A.M.E. Platform
					'If TC.NZ(BS_Emu_Games.Current("id_Moby_Platforms"), 0) = cls_Globals.enm_Moby_Platforms.mame Then
					'	Dim bbi As New DevExpress.XtraBars.BarButtonItem(Me.barmng_Emu, "M.A.M.E. (Global Default)")
					'	bbi.Tag = -2
					'	AddHandler bbi.ItemClick, AddressOf Handle_LaunchBarItemClick

					'	bsi_Launch.AddItem(bbi)
					'End If

					bsi_Launch.Enabled = True

				End If

				bbi_MultiUser_Remove_Games.Enabled = True
				bbi_Launch_Random.Enabled = True

				If Not TC.IsNullNothingOrEmpty(BS_Emu_Games.Current("Moby_URL")) Then
					bbi_Open_Moby_Page.Enabled = True
				End If

				If Not TC.IsNullNothingOrEmpty(BS_Emu_Games.Current("Moby_Platforms_URLPart")) AndAlso Not TC.IsNullNothingOrEmpty(BS_Emu_Games.Current("Moby_Games_URLPart")) Then
					bbi_Contribute_TechInfo.Enabled = True
				End If
			End If
		Else
			'No Game selected
		End If

		If cls_Globals.MultiUserMode = True AndAlso cls_Globals.Admin = False Then
			bsi_MultiUser.Enabled = False
			bbi_DOSBox_Clear_Exe_Config.Enabled = False
			bbi_DOSBox_Templates.Enabled = False
			bbi_ScummVM_Templates.Enabled = False
			bbi_Edit_Game.Enabled = False
			bbi_Edit_Multiple_Games.Enabled = False
			bbi_Emu_Settings.Enabled = False
			bbi_Rom_Manager.Enabled = False
			bbi_Rombase_Manager.Enabled = False
			bsi_Export.Enabled = False
		End If
	End Sub

	Private Sub popmnu_Extras_BeforePopup(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles popmnu_Extras.BeforePopup
		If BS_Emu_Games.Current Is Nothing Then
			bbi_USER_Extras_Manager.Enabled = False
		Else
			bbi_USER_Extras_Manager.Enabled = True
		End If
	End Sub

	Private Sub gv_Emu_Games_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gv_Emu_Games.MouseDown
		Dim hitinfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = gv_Emu_Games.CalcHitInfo(e.Location)

		'Dim sResult As String = ""
		'sResult &= "InColumn: " & hitinfo.InColumn & ControlChars.CrLf
		'sResult &= "InColumnPanel: " & hitinfo.InColumnPanel & ControlChars.CrLf
		'sResult &= "InFilterPanel: " & hitinfo.InFilterPanel & ControlChars.CrLf
		'sResult &= "InGroupColumn: " & hitinfo.InGroupColumn & ControlChars.CrLf
		'sResult &= "InGroupPanel: " & hitinfo.InGroupPanel & ControlChars.CrLf
		'sResult &= "InRow: " & hitinfo.InRow & ControlChars.CrLf
		'sResult &= "InRowCell: " & hitinfo.InRowCell

		'Debug.Writeline(sResult)

		If Not hitinfo.InColumn AndAlso Not hitinfo.InColumnPanel AndAlso Not hitinfo.InFilterPanel AndAlso Not hitinfo.InGroupColumn AndAlso Not hitinfo.InGroupPanel Then

			'Boolean switches
			If hitinfo.Column IsNot Nothing AndAlso {"Favourite", "Have", "Want", "Trade"}.Contains(hitinfo.Column.FieldName) AndAlso hitinfo.RowHandle > -1 Then
				Dim colName As String = hitinfo.Column.FieldName

				Dim row As DataRow = gv_Emu_Games.GetRow(hitinfo.RowHandle).Row

				If Not cls_Globals.Admin Then
					Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
						If DS_ML.Upsert_tbl_Users_Emu_Games(tran, cls_Globals.id_Users, TC.getSQLFormat(row("id_Emu_Games")), Nothing, Nothing, Nothing, IIf(colName = "Favourite", Not TC.NZ(row("Favourite"), False), TC.NZ(row("Favourite"), False)), IIf(colName = "Have", Not TC.NZ(row("Have"), False), TC.NZ(row("Have"), False)), IIf(colName = "Want", Not TC.NZ(row("Want"), False), TC.NZ(row("Want"), False)), IIf(colName = "Trade", Not TC.NZ(row("Trade"), False), TC.NZ(row("Trade"), False))) Then
							tran.Commit()
							row(colName) = Not TC.NZ(row(colName), False)
							grd_Emu_Games.Refresh()
						End If
					End Using
				Else
					If DataAccess.FireProcedure(cls_Globals.Conn, 0, "UPDATE tbl_Emu_Games SET " & colName & " = CASE WHEN " & colName & " = 1 THEN 0 ELSE 1 END WHERE id_Emu_Games = " & TC.getSQLFormat(row("id_Emu_Games"))) Then
						row(colName) = Not TC.NZ(row(colName), False)
						grd_Emu_Games.Refresh()
					End If
				End If
			End If

			If hitinfo.Column IsNot Nothing AndAlso hitinfo.Column.FieldName = "tmp_Highlighted" AndAlso hitinfo.RowHandle > -1 Then
				Dim row As DataRow = gv_Emu_Games.GetRow(hitinfo.RowHandle).Row

				row("tmp_Highlighted") = Not TC.NZ(row("tmp_Highlighted"), False)
				grd_Emu_Games.Refresh()
			End If
		End If
	End Sub

	Private Sub grd_Emu_Games_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grd_Emu_Games.MouseDown
		Application.DoEvents()

		If e.Button = Windows.Forms.MouseButtons.Right Then
			cls_Globals.Suppress_MetroUINavigationBarsShowing = True
		End If
	End Sub

	Private Sub bbi_Open_Moby_Page_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Open_Moby_Page.ItemClick
		Dim sURL As String = BS_Emu_Games.Current("Moby_URL")
		Dim procinfo As New ProcessStartInfo(sURL)
		procinfo.UseShellExecute = True
		Process.Start(procinfo)
	End Sub

	Private Sub bbi_Contribute_TechInfo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Contribute_TechInfo.ItemClick
		Dim sURL As String = "http://www.mobygames.com/game/sheet/contribute/tech_info/acg,1/" & BS_Emu_Games.Current("Moby_Games_URLPart") & "/" & BS_Emu_Games.Current("Moby_Platforms_URLPart") & "/"
		Dim procinfo As New ProcessStartInfo(sURL)
		procinfo.UseShellExecute = True
		Process.Start(procinfo)
	End Sub

	Private Sub grd_Game_Groups_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grd_Game_Groups.MouseDown
		Application.DoEvents()

		If e.Button = Windows.Forms.MouseButtons.Right Then
			cls_Globals.Suppress_MetroUINavigationBarsShowing = True
		End If
	End Sub

	Private Sub popmnu_GameGroups_BeforePopup(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles popmnu_GameGroups.BeforePopup
		If Not grd_Game_Groups.Allow_Popup Then
			e.Cancel = True
			Return
		End If

		bbi_Contribute_TechInfo.Enabled = False
		bsi_Launch.Enabled = False

		If MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Game_Groups).Length = 0 Then
			e.Cancel = True
		End If
	End Sub

	Private Sub popmnu_Staff_BeforePopup(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles popmnu_Staff.BeforePopup
		If Not grd_Staff.Allow_Popup Then
			e.Cancel = True
			Return
		End If

		If MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Staff).Length = 0 Then
			e.Cancel = True
		End If
	End Sub
#End Region

	Private Sub cmb_Filterset_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Filterset.Validated
		If _Initializing Then Return
	End Sub

	Private Sub cmb_Group_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Groups.Validated
		If _Initializing Then Return
	End Sub

	Private Sub cmb_Staff_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Staff.Validated
		If _Initializing Then Return
	End Sub

	Private Sub BS_FilterSets_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BS_FilterSets.CurrentChanged
		If BS_FilterSets.Current Is Nothing OrElse BS_FilterSets.Current("id_FilterSets") = 0 Then
			cmb_Filterset.Properties.Buttons(2).Enabled = False
			cmb_Filterset.Properties.Buttons(3).Enabled = False

			Me.gv_Emu_Games.ActiveFilter.Clear()

			Return
		Else
			cmb_Filterset.Properties.Buttons(2).Enabled = True
			cmb_Filterset.Properties.Buttons(3).Enabled = True
		End If

		If TC.NZ(BS_FilterSets.Current("ApplyGridFilter"), False) = True AndAlso Not TC.IsNullNothingOrEmpty(BS_FilterSets.Current("GridFilter")) Then
			Me.gv_Emu_Games.RestoreLayoutFromStream(New System.IO.MemoryStream(Convert.FromBase64String(BS_FilterSets.Current("GridFilter"))))
		End If
	End Sub

	'Removed: should already be handled in the MouseMove and MouseClick events of the rating gauges
	'Private Sub gv_Emu_Games_BeforeLeaveRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles gv_Emu_Games.BeforeLeaveRow
	'	BS_Emu_Games.EndEdit()

	'	If gv_Emu_Games.GetRow(e.RowHandle) Is Nothing Then Return

	'	Dim row As DataRow = gv_Emu_Games.GetRow(e.RowHandle).Row

	' If row.RowState = DataRowState.Modified Then
	' 	Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
	' 		DS_ML.Update_tbl_Emu_Games_Ratings(tran, row("id_Emu_Games"), row("Rating_Gameplay"), row("Rating_Graphics"), row("Rating_Personal"), row("Rating_Sound"), row("Rating_Story"))
	' 		row("Rating") = DS_ML.Select_src_ucr_Emulation_Games_Rating(tran, row("id_Emu_Games"))
	' 		tran.Commit()
	' 		row.AcceptChanges()
	' 	End Using
	' End If

	'End Sub

	Private Sub gv_Emu_Games_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gv_Emu_Games.MouseMove
		'Dim hitinfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = gv_Emu_Games.CalcHitInfo(e.Location)

		'If hitinfo.InRowCell AndAlso Not hitinfo.InGroupRow AndAlso Not hitinfo.InColumnPanel AndAlso Not hitinfo.InFilterPanel AndAlso Not hitinfo.InGroupColumn AndAlso Not hitinfo.InGroupPanel AndAlso Not hitinfo.InGroupRow Then
		'	If {"Favourite", "Have", "Want", "Trade", "tmp_Highlighted"}.Contains(hitinfo.Column.FieldName) Then
		'		grd_Emu_Games.Cursor = Cursors.Hand
		'	End If
		'Else
		'	If grd_Emu_Games.Cursor = Cursors.Hand Then
		'		grd_Emu_Games.Cursor = Cursors.Default
		'	End If
		'End If
		grd_Emu_Games.ShowHandInColumns(gv_Emu_Games, {"Favourite", "Have", "Want", "Trade", "tmp_Highlighted"}, e)
	End Sub

	Private Sub bbi_Edit_Game_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Edit_Game.ItemClick
		If BS_Emu_Games.Current Is Nothing Then Return

		Dim id_Emu_Games As Integer = TC.getSQLFormat(BS_Emu_Games.Current("id_Emu_Games"))

		Using frm As New frm_Emu_Game_Edit(id_Emu_Games)
			If frm.ShowDialog(Me.ParentForm) = DialogResult.OK Then
				Refill_Emu_Games()
			End If
		End Using
	End Sub

	Private Sub bbi_Edit_Multiple_Games_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Edit_Multiple_Games.ItemClick
		If BS_Emu_Games.Current Is Nothing Then Return

		Dim iRowHandles As Integer() = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Emu_Games)
		Dim iNumRows As Integer = iRowHandles.Length

		If iNumRows <= 0 Then
			Return
		End If

		Dim al_id_Emu_Games As New ArrayList

		For Each iRowHandle As Integer In iRowHandles
			Dim row As DataRow = gv_Emu_Games.GetRow(iRowHandle).Row
			al_id_Emu_Games.Add(CInt(row("id_Emu_Games")))
		Next

		Dim id_Emu_Games_Int As Integer() = CType(al_id_Emu_Games.ToArray(GetType(Integer)), Integer())

		Using frm As New frm_Emu_Game_Edit(id_Emu_Games_Int, "Edit " & iRowHandles.Length & " Games", "", False, IIf(TC.NZ(cmb_Platform.EditValue, 0) > 0, TC.NZ(cmb_Platform.EditValue, 0), Nothing))
			If frm.ShowDialog(Me.ParentForm) = DialogResult.OK Then
				Refill_Emu_Games()
			End If
		End Using
	End Sub

	Private Sub Gauge_Rating_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gc_Rating_Gameplay.MouseMove, gc_Rating_Personal.MouseMove, gc_Rating_Story.MouseMove, gc_Rating_Sound.MouseMove, gc_Rating_Graphics.MouseMove
		If e.Button = Windows.Forms.MouseButtons.Left Then
			CType(CType(sender, MKNetDXLib.ctl_MKDXGaugeControl).Gauges(0), DevExpress.XtraGauges.Win.Gauges.Linear.LinearGauge).Scales(0).Value = Math.Max(0, Math.Min(5, CType(CType(e.Location.X, Double) / CType(sender.width, Double) * 5 + 0.5, Integer)))

			If BS_Emu_Games.Current Is Nothing Then Return

			BS_Emu_Games.EndEdit()
			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				Dim row As DataRow = BS_Emu_Games.Current.Row
				DS_ML.Update_tbl_Emu_Games_Ratings(tran, row("id_Emu_Games"), row("Rating_Gameplay"), row("Rating_Graphics"), row("Rating_Personal"), row("Rating_Sound"), row("Rating_Story"))
				row("Rating") = DS_ML.Select_src_ucr_Emulation_Games_Rating(tran, row("id_Emu_Games"))
				tran.Commit()
				row.AcceptChanges()
			End Using
		End If
	End Sub

	Private Sub Gauge_Rating_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gc_Rating_Gameplay.MouseClick, gc_Rating_Graphics.MouseClick, gc_Rating_Personal.MouseClick, gc_Rating_Sound.MouseClick, gc_Rating_Story.MouseClick
		If e.Button = Windows.Forms.MouseButtons.Left Then
			CType(CType(sender, MKNetDXLib.ctl_MKDXGaugeControl).Gauges(0), DevExpress.XtraGauges.Win.Gauges.Linear.LinearGauge).Scales(0).Value = Math.Max(0, Math.Min(5, CType(CType(e.Location.X, Double) / CType(sender.width, Double) * 5 + 0.5, Integer)))

			If BS_Emu_Games.Current Is Nothing Then Return

			BS_Emu_Games.EndEdit()
			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				Dim row As DataRow = BS_Emu_Games.Current.Row
				DS_ML.Update_tbl_Emu_Games_Ratings(tran, row("id_Emu_Games"), row("Rating_Gameplay"), row("Rating_Graphics"), row("Rating_Personal"), row("Rating_Sound"), row("Rating_Story"))
				row("Rating") = DS_ML.Select_src_ucr_Emulation_Games_Rating(tran, row("id_Emu_Games"))
				tran.Commit()
				row.AcceptChanges()
			End Using
		End If
	End Sub

	Private Sub lbl_Rating_Edit_Weights_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbl_Rating_Edit_Weights.Click
		Using frm As New frm_Emu_Game_Rating_Weights_Edit
			If frm.ShowDialog(Me.ParentForm) = DialogResult.OK Then
				Refill_Emu_Games()
			End If
		End Using
	End Sub

	Private Sub Save_EmuGame_Position()
		Dim pltfm As String = ""
		If TC.NZ(cmb_Platform.EditValue, 0) <> 0 Then
			pltfm = "_pltfm_" & cmb_Platform.EditValue.ToString
		End If

		If BS_Emu_Games.Current IsNot Nothing Then
			cls_Settings.SetSetting("ucr_Emulation-Game" & pltfm, BS_Emu_Games.Current("id_Emu_Games"), cls_Settings.enm_Settingmodes.Per_User)
		Else
			cls_Settings.SetSetting("ucr_Emulation-Game" & pltfm, DBNull.Value, cls_Settings.enm_Settingmodes.Per_User)
		End If
	End Sub

	Private Sub Load_EmuGame_Position()
		Dim pltfm As String = ""
		If TC.NZ(cmb_Platform.EditValue, 0) <> 0 Then
			pltfm = "_pltfm_" & cmb_Platform.EditValue.ToString
		End If

		BS_Emu_Games.Position = BS_Emu_Games.Find("id_Emu_Games", TC.NZ(cls_Settings.GetSetting("ucr_Emulation-Game" & pltfm, cls_Settings.enm_Settingmodes.Per_User), 0L))
		Select_BindingSource_Row_on_gv_Emu_Games()
	End Sub

	Private Sub ucr_Emulation_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Leave
		'Save Position, Platform and Filterset here
		cls_Settings.SetSetting("ucr_Emulation-Platform", cmb_Platform.EditValue, cls_Settings.enm_Settingmodes.Per_User)
		cls_Settings.SetSetting("ucr_Emulation-Group", cmb_Groups.EditValue, cls_Settings.enm_Settingmodes.Per_User)
		cls_Settings.SetSetting("ucr_Emulation-Developer", cmb_Staff.EditValue, cls_Settings.enm_Settingmodes.Per_User)
		cls_Settings.SetSetting("ucr_Emulation-Filterset", cmb_Filterset.EditValue, cls_Settings.enm_Settingmodes.Per_User)

		Save_EmuGame_Position()
	End Sub

	Private Sub grd_Emu_Games_DDAfterLoadSettings(ByVal Sender As Object, ByVal e As MKNetDXLib.cls_DDSettingHandlerDelegates(Of MKNetDXLib.ctl_MKDXGrid).SettingEventArg_WithResult) Handles grd_Emu_Games.DDAfterLoadSettings
		Refill_Emu_Games()
	End Sub

	Private Sub gv_Game_Stats_CustomColumnDisplayText(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles gv_Statistics.CustomColumnDisplayText
		Try
			If e.Column.FieldName = "Runtime" Then
				e.DisplayText = MKNetLib.cls_MKStringSupport.GetTimeString(TC.NZ(e.Value, 0))
			End If
		Catch ex As Exception

		End Try
	End Sub

	Private Sub gv_Emu_Games_CustomColumnDisplayText(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles gv_Emu_Games.CustomColumnDisplayText
		Try
			If e.Column.FieldName = "Num_Played" Then
				If TC.NZ(e.Value, 0) = 0 Then
					e.DisplayText = ""
				End If
			End If

			If e.Column.FieldName = "Num_Runtime" Then
				If TC.NZ(e.Value, 0) = 0 Then
					e.DisplayText = ""
				Else
					e.DisplayText = MKNetLib.cls_MKStringSupport.GetTimeString(TC.NZ(e.Value, 0))
				End If
			End If

			If TC.NZ(gv_Emu_Games.GetListSourceRowCellValue(e.ListSourceRowIndex, "id_Moby_Platforms"), 0) <> cls_Globals.enm_Moby_Platforms.scummvm Then
				If e.Column.FieldName = "Game" AndAlso TC.NZ(gv_Emu_Games.GetListSourceRowCellValue(e.ListSourceRowIndex, "Game"), "nope1").ToLower = TC.NZ(gv_Emu_Games.GetListSourceRowCellValue(e.ListSourceRowIndex, "InnerFile"), "nope2").ToLower Then
					e.DisplayText = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(e.Value)
				End If
			End If
		Catch ex As Exception

		End Try
	End Sub

	Private Sub AddNewScreenshots(ByVal id_Emu_Games As Integer, ByRef al_Screenshots As ArrayList, ByVal Snapshot_Directories As String())
		'Get Screenshots from Snapshot Directories
		For Each Snapshot_Directory As String In Snapshot_Directories
			If Alphaleonis.Win32.Filesystem.Directory.Exists(Snapshot_Directory) Then
				For Each file As String In Alphaleonis.Win32.Filesystem.Directory.GetFiles(Snapshot_Directory, "*.*")
					Dim ext As String = Alphaleonis.Win32.Filesystem.Path.GetExtension(file).ToLower.Replace(".", "")
					If ext = "bmp" OrElse ext = "png" OrElse ext = "jpg" OrElse ext = "tif" Then
						Try
							'Dim img As New Bitmap(file)
							Dim img As Bitmap = Image.FromStream(New IO.MemoryStream(Alphaleonis.Win32.Filesystem.File.ReadAllBytes(file)))

							If img.PhysicalDimension.Width > 1 And img.PhysicalDimension.Height > 1 Then
								al_Screenshots.Add(img)
							End If
						Catch ex As Exception

						End Try
					End If
				Next
			End If
		Next

		If al_Screenshots.Count = 0 Then Return

		Using frm As New frm_USER_Extras_Manager(id_Emu_Games, al_Screenshots)
			frm.Name = frm.Name & "_ADD"
			If frm.ShowDialog(Me.ParentForm) = DialogResult.OK Then
				For Each row As DataRow In frm.DS_Screenshots.Tables("tbl_Screenshots").Select("", "Sort")
					If TC.NZ(row("Use"), False) = False Then Continue For

					Dim ExtraCategory As String = DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Name FROM tbl_Emu_Extras WHERE id_Emu_Extras = " & TC.getSQLFormat(row("Category")))
					Dim FileName As String = cls_Extras.FindNextFreeExtraFilename(frm.Platform_Short, ExtraCategory, frm.FileName)

					If FileName = "" Then Continue For

					Try
						Dim mediadir As String = cls_Globals.Dir_Extras & "\emulation\" & frm.Platform_Short & "\" & ExtraCategory
						If Not Alphaleonis.Win32.Filesystem.Directory.Exists(mediadir) Then
							Alphaleonis.Win32.Filesystem.Directory.CreateDirectory(mediadir)
						End If
						CType(frm._al_Screenshots.Item(row("id")), Image).Save(cls_Globals.Dir_Extras & "\emulation\" & frm.Platform_Short & "\" & ExtraCategory & "\" & FileName & ".png", System.Drawing.Imaging.ImageFormat.Png)
					Catch ex As Exception

					End Try
				Next

				ApplyFirstExtra()
			End If
		End Using
	End Sub

	Private Sub Refresh_Emu_Game_History(ByVal id_Emu_Games As Integer, ByVal tran As SQLite.SQLiteTransaction)
		Dim TotalPlaycount As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT COUNT(1) FROM tbl_History WHERE id_Users " & IIf(cls_Globals.Admin, "IS NULL", "= " & TC.getSQLFormat(cls_Globals.id_Users)) & " AND id_Emu_Games = " & TC.getSQLFormat(BS_Emu_Games.Current("id_Emu_Games")), tran), 0)
		Dim TotalRuntime As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT SUM(strftime('%s', End) - strftime('%s', Start)) FROM tbl_History WHERE id_Users " & IIf(cls_Globals.Admin, "IS NULL", "= " & TC.getSQLFormat(cls_Globals.id_Users)) & " AND id_Emu_Games = " & TC.getSQLFormat(BS_Emu_Games.Current("id_Emu_Games")), tran), 0)

		If cls_Globals.Admin Then
			DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Emu_Games SET Num_Played = " & TC.getSQLFormat(TotalPlaycount) & ", Num_Runtime = " & TC.getSQLFormat(TotalRuntime) & " WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), tran)
		Else
			DS_ML.Upsert_tbl_Users_Emu_Games(tran, cls_Globals.id_Users, BS_Emu_Games.Current("id_Emu_Games"), TotalPlaycount, TotalRuntime)
		End If

		Dim rows() As DataRow = DS_ML.src_ucr_Emulation_Games.Select("id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games))
		If rows.Length = 1 Then
			Try
				rows(0)("Num_Played") = TotalPlaycount
			Catch ex As Exception

			End Try
			Try
				rows(0)("Num_Runtime") = TotalRuntime
			Catch ex As Exception

			End Try
			Try
				rows(0)("Last_Played") = DateTime.Now
			Catch ex As Exception

			End Try
		End If

		DS_ML.Fill_tbl_History(tran, DS_ML.tbl_History, BS_Emu_Games.Current("id_Emu_Games"))
		lbl_Emu_Games_Playcount.Text = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT COUNT(1) FROM tbl_History WHERE id_Users " & IIf(cls_Globals.Admin, "IS NULL", "= " & TC.getSQLFormat(cls_Globals.id_Users)) & " AND id_Emu_Games = " & TC.getSQLFormat(BS_Emu_Games.Current("id_Emu_Games")), tran), 0)
		lbl_Emu_Games_Runtime_Value.Text = MKNetLib.cls_MKStringSupport.GetTimeString(TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT SUM(strftime('%s', End) - strftime('%s', Start)) FROM tbl_History WHERE id_Users " & IIf(cls_Globals.Admin, "IS NULL", "= " & TC.getSQLFormat(cls_Globals.id_Users)) & " AND id_Emu_Games = " & TC.getSQLFormat(BS_Emu_Games.Current("id_Emu_Games")), tran), 0))

	End Sub

	Private Sub tmr_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmr.Tick
		If _al_StatsChanges.Count > 0 Then
			For Each objvec As cls_3ObjVec In _al_StatsChanges
				Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
					Dim id_Emu_Games As Integer = objvec._x
					Dim StartTime As DateTime = objvec._y
					Dim ExitTime As DateTime = objvec._z

					DS_ML.Insert_tbl_History(tran, StartTime, ExitTime, id_Emu_Games)

					If cls_Globals.Admin Then
						DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Emu_Games SET Last_Played = " & TC.getSQLFormat(ExitTime) & " WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), tran)
					Else
						DataAccess.FireProcedure(tran.Connection, 0, "UPDATE tbl_Users_Emu_Games SET Last_Played = " & TC.getSQLFormat(ExitTime) & " WHERE id_Users = " & TC.getSQLFormat(cls_Globals.id_Users) & " AND id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), tran)
					End If

					Refresh_Emu_Game_History(id_Emu_Games, tran)

					Try
						tran.Commit()
					Catch ex As Exception

					End Try
				End Using
			Next

			_al_StatsChanges.Clear()
		End If

		If _al_Screenshots_EmuGames.Count > 0 Then
			Dim al_Shots As New ArrayList

			If _al_Screenshots.Count > 0 Then
				If _al_Screenshots.Count > 0 Then
					For Each item As Object In _al_Screenshots
						al_Shots.Add(item)
					Next
				End If
			End If

			Dim id_Emu_Games As Integer = CType(_al_Screenshots_EmuGames.Item(0), cls_Emu_Game_ProcInfo).id_Emu_Games
			Dim snapdir As String = CType(_al_Screenshots_EmuGames.Item(0), cls_Emu_Game_ProcInfo).Snapshot_Directory

			_al_Screenshots.Clear()
			_al_Screenshots_EmuGames.Clear()

			'AddNewScreenshots(id_Emu_Games, al_Shots, snapdir)
			AddNewScreenshots(id_Emu_Games, al_Shots, New String() {snapdir, cls_Globals.Dir_Screenshot})
		End If
	End Sub

	Private Sub grd_Emu_Games_DDAfterSaveSettings(ByVal Sender As Object, ByVal e As MKNetDXLib.cls_DDSettingHandlerDelegates(Of MKNetDXLib.ctl_MKDXGrid).SettingEventArg_WithResult) Handles grd_Emu_Games.DDAfterSaveSettings
		'cls_Settings.SetSetting("Emu_Games_Splt1", Me.spltpnl1.SplitterPosition, per_user)
	End Sub

	Dim _First_Paint_Handled As Boolean = False

	Private Sub ucr_Emulation_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
		If _First_Paint_Handled = True Then Return

		_First_Paint_Handled = True

		grd_Emu_Games.ForceInitialize()
		cmb_Filterset.EditValue = CLng(TC.NZ(cls_Settings.GetSetting("ucr_Emulation-Filterset", cls_Settings.enm_Settingmodes.Per_User), 0))
		Load_EmuGame_Position()
		'gv_Emu_Games.MakeRowVisible(gv_Emu_Games.FocusedRowHandle)

		filteringUIContext.RetrieveFields()
		'accordion_FilterUI.ExpandAll()

		_Initializing = False
	End Sub

	Private Sub bbi_Rom_Manager_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Rom_Manager.ItemClick
		Dim id_Emu_Games As Object = Nothing
		Dim id_Moby_Platforms As Object = Nothing

		If BS_Emu_Games.Current IsNot Nothing Then
			id_Emu_Games = BS_Emu_Games.Current("id_Emu_Games")
		Else
			If TC.NZ(cmb_Platform.EditValue, 0) > 0 Then
				id_Moby_Platforms = cmb_Platform.EditValue
			End If
		End If

#If DEBUG Then
		MKDXHelper.MessageBox("TODO: fix Rom Manager call from main list", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
		Dim frm As New frm_Rom_Manager(id_Emu_Games, id_Moby_Platforms)
		frm.Show(Me)
#Else
		Using frm As New frm_Rom_Manager(id_Emu_Games, id_Moby_Platforms)
			If frm.ShowDialog(Me.ParentForm) Then
				Me.Refill_Emu_Games()
				Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
					Me.DS_ML.Fill_src_ucr_Emulators_Platforms(tran, Me.DS_ML.src_ucr_Emulation_Platforms)
				End Using
			End If
		End Using
#End If

	End Sub

	Private Sub bbi_Rombase_Manager_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Rombase_Manager.ItemClick
		Dim id_Rombase As Object = Nothing

		If BS_Emu_Games.Current IsNot Nothing Then
			id_Rombase = BS_Emu_Games.Current("id_Rombase")
		End If

		Using frm As New frm_ROMBase_Manager(id_Rombase)
			frm.ShowDialog(Me.ParentForm)
		End Using
	End Sub

	'Watching the clipboard can be seen as a security issue - temp. disabled
	'Private Sub _ClipboardWatcher_ClipboardContentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _ClipboardWatcher.ClipboardContentChanged
	'	If Clipboard.ContainsImage Then
	'		_al_Screenshots.Add(Clipboard.GetImage)
	'	End If
	'End Sub

	Private Sub bbi_GameGroup_Info_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_GameGroup_Info.ItemClick
		If BS_Game_Groups.Current Is Nothing Then Return

		Dim id_Moby_Game_Groups As Integer = TC.NZ(BS_Game_Groups.Current("id_Moby_Game_Groups"), 0)

		If id_Moby_Game_Groups = 0 Then Return

		Using frm As New frm_Moby_Game_Group_Info(id_Moby_Game_Groups)
			frm.ShowDialog(Me.ParentForm)
		End Using
	End Sub

	Private Sub bbi_Staff_Info_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Staff_Info.ItemClick
		If BS_Moby_Releases_Staff.Current Is Nothing Then Return

		Dim id_Moby_Staff As Integer = TC.NZ(BS_Moby_Releases_Staff.Current("id_Moby_Staff"), 0)

		If id_Moby_Staff = 0 Then Return

		Using frm As New frm_Moby_Staff_Info(id_Moby_Staff)
			frm.ShowDialog(Me.ParentForm)
		End Using
	End Sub

	Private Sub bbi_GameGroup_Filter_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_GameGroup_Filter.ItemClick
		If BS_Game_Groups.Current Is Nothing Then Return

		Dim id_Moby_Game_Groups As Long = TC.NZ(BS_Game_Groups.Current("id_Moby_Game_Groups"), 0)

		If id_Moby_Game_Groups = 0 Then Return

		Me.cmb_Groups.EditValue = id_Moby_Game_Groups
	End Sub

	Private Sub bbi_Developer_Filter_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Staff_Filter.ItemClick
		If BS_Moby_Releases_Staff.Current Is Nothing Then Return

		Dim id_Moby_Staff As Long = TC.NZ(BS_Moby_Releases_Staff.Current("id_Moby_Staff"), 0)

		If id_Moby_Staff = 0 Then Return

		Me.cmb_Staff.EditValue = id_Moby_Staff
	End Sub

	Private Sub bbi_Export_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Export.ItemClick
		Dim iRowHandles As Integer() = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Emu_Games)
		Dim iNumRows As Integer = iRowHandles.Length

		If iNumRows <= 0 Then
			Return
		End If

		Dim al_id_Emu_Games As New ArrayList

		Dim bHasDOSEntry As Boolean = False

		For Each iRowHandle As Integer In iRowHandles
			Dim row As DataRow = gv_Emu_Games.GetRow(iRowHandle).Row
			al_id_Emu_Games.Add(row("id_Emu_Games"))
			If TC.NZ(row("id_Moby_Platforms"), 0) = 2 Then
				bHasDOSEntry = True
			End If
		Next

		Using frm As New frm_Export(al_id_Emu_Games, bHasDOSEntry)
			frm.ShowDialog(Me.ParentForm)
		End Using
	End Sub

	Private Sub bbi_Emu_Settings_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Emu_Settings.ItemClick
		Using frm As New frm_Emulators
			frm.ShowDialog(Me.ParentForm)
		End Using
	End Sub

	Private Sub bbi_DOSBox_Templates_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_DOSBox_Templates.ItemClick
		Using frm As New frm_DOSBox_Templates
			frm.ShowDialog(Me.ParentForm)
		End Using
	End Sub

	Private Sub bbi_ScummVM_Templates_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_ScummVM_Templates.ItemClick
		Using frm As New frm_ScummVM_Templates
			frm.ShowDialog(Me.ParentForm)
		End Using
	End Sub

	Private Sub bbi_DOSBox_Clear_Exe_Config_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_DOSBox_Clear_Exe_Config.ItemClick
		If MKDXHelper.MessageBox("Do you want to clear the executables configuration for the selected game?" & ControlChars.CrLf & "After clearing the configuration you will be prompted with a selection when launching the game.", "Clear Executables Configuration", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
			DataAccess.FireProcedure(cls_Globals.Conn, 0, "UPDATE tbl_Emu_Games SET id_Rombase_DOSBox_Exe_Types = NULL WHERE id_Emu_Games = " & TC.getSQLFormat(BS_Emu_Games.Current("id_Emu_Games")) & " OR id_Emu_Games_Owner = " & TC.getSQLFormat(BS_Emu_Games.Current("id_Emu_Games")))
			MKDXHelper.MessageBox("The executables configuration for the selected game has been cleared.", "Clear Executables Configuration", MessageBoxButtons.OK, MessageBoxIcon.Information)
		End If
	End Sub

	Private Sub Call_J2K(Optional ByVal J2K_Preset As String = "")
		Dim file_J2K As String = TC.NZ(cls_Settings.GetSetting("Path_J2K"), "")
		If Not Alphaleonis.Win32.Filesystem.File.Exists(file_J2K) Then Return

		If J2K_Preset = "" Then J2K_Preset = TC.NZ(cls_Settings.GetSetting("Config_J2K"), "")
		If J2K_Preset = "" Then J2K_Preset = "Empty"

		Dim TempDir As String = MKNetLib.cls_MKFileSupport.CreateTempDir("ml_") 'One temp dir for all extracted roms
		Dim batch_J2K As String = TempDir & "\" & "j2k.bat"

		Dim batch_Content As String = """" & file_J2K & """" & " " & """" & J2K_Preset & """"

		If MKNetLib.cls_MKFileSupport.SaveTextToFile(batch_Content, batch_J2K) Then
			Dim proc As New System.Diagnostics.Process
			proc.StartInfo.FileName = batch_J2K
			proc.StartInfo.UseShellExecute = True
			proc.StartInfo.CreateNoWindow = True
			proc.Start()
		End If
	End Sub

	Private Sub bsi_Launch_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bsi_Launch.ItemPress
		If BS_Emu_Games.Current IsNot Nothing Then
			Launch_Game(Nothing, Get_DOSBox_Exe_Type)
		End If
	End Sub

	Private Sub gv_Emu_Games_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gv_Emu_Games.RowCellStyle
		If e.RowHandle >= 0 Then
			Dim row As DataRow = gv_Emu_Games.GetRow(e.RowHandle).Row

			If TC.NZ(row("tmp_Highlighted"), False) = True AndAlso TC.NZ(row("Unavailable"), False) = True Then
				e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold Or FontStyle.Strikeout)
			ElseIf TC.NZ(row("tmp_Highlighted"), False) = True Then
				e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)
			ElseIf TC.NZ(row("Unavailable"), False) = True Then
				e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Strikeout)
			End If
		End If
	End Sub

	Private Sub bbi_Analyze_Missing_Extras_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Analyze_Missing_Extras.ItemClick
		Analyze_Missing_Extras()
	End Sub

	Private Sub Handle_bbi_MultiUser_Add_Remove_Games_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_MultiUser_Add_Games.ItemClick, bbi_MultiUser_Remove_Games.ItemClick
		Dim bAdd As Boolean = e.Item Is bbi_MultiUser_Add_Games

		Dim id_Users As Integer = 0

		If TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT COUNT(1) FROM tbl_Users WHERE Restricted = 1"), 0) = 0 Then
			MKDXHelper.MessageBox("There is no restricted user to choose from, please set one up in Settings -> Multi User Mode.", "Add Games to restricted user", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		Using frm As New frm_Login(True, True, "Select the target user.")
			frm.Text = IIf(bAdd, "Add Games to restricted user", "Remove Games from restricted user")
			If frm.ShowDialog(Me.ParentForm) <> DialogResult.OK Then
				Return
			End If

			id_Users = frm.cmb_Users.EditValue

			If id_Users > 0 Then
				Dim iRowHandles() As Integer = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Emu_Games)
				Dim iAdded As Integer = 0
				Dim iSkipped As Integer = 0
				Dim iDeleted As Integer = 0
				Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
					For Each iRowHandle As Integer In iRowHandles
						Dim row_Emu_Games As DS_ML.src_ucr_Emulation_GamesRow = gv_Emu_Games.GetRow(iRowHandle).Row
						Dim id_Users_Emu_Games As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Users_Emu_Games FROM tbl_Users_Emu_Games WHERE id_Users = " & TC.getSQLFormat(id_Users) & " AND id_Emu_Games = " & TC.getSQLFormat(row_Emu_Games("id_Emu_Games")), tran), 0)
						If id_Users_Emu_Games > 0 Then
							If Not bAdd Then
								DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Users_Emu_Games WHERE id_Users_Emu_Games = " & TC.getSQLFormat(id_Users_Emu_Games), tran)
								iDeleted += 1
							Else
								iSkipped += 1
							End If
						Else
							If bAdd Then
								DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Users_Emu_Games (id_Users, id_Emu_Games) VALUES (" & TC.getSQLParameter(id_Users, row_Emu_Games("id_Emu_Games")) & ")", tran)
								iAdded += 1
							Else
								iSkipped += 1
							End If
						End If
					Next

					DS_ML.Update_Platform_NumGames_Cache(tran, Me.cmb_Platform.EditValue, id_Users)

					tran.Commit()

					If bAdd Then
						MKDXHelper.MessageBox("For the restricted user ' " & frm.cmb_Users.Text & "' out of " & iRowHandles.Length & " selected games " & iAdded & " have been added and " & iSkipped & " have been skipped (because they were already added to this user).", "Add selected games to restricted user", MessageBoxButtons.OK, MessageBoxIcon.Information)
					Else
						MKDXHelper.MessageBox("For the restricted user ' " & frm.cmb_Users.Text & "' out of " & iRowHandles.Length & " selected games " & iDeleted & " have been removed and " & iSkipped & " have been skipped (because they were not added to this user in the first place).", "Remove selected games from restricted user", MessageBoxButtons.OK, MessageBoxIcon.Information)
					End If
				End Using
			End If

		End Using

	End Sub

	Private Sub bbi_MultiUser_Show_Games_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_MultiUser_Show_Games.ItemClick
		Dim id_User As Integer = 0

		If TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT COUNT(1) FROM tbl_Users WHERE Restricted = 1"), 0) = 0 Then
			MKDXHelper.MessageBox("There is no restricted user to choose from, please set one up in Settings -> Multi User Mode.", "No restricted user found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		Using frm As New frm_Login(True, True, "Select the restricted user, whose games you want to view in bold font.")
			frm.Text = "Show Games of restricted user"
			If frm.ShowDialog(Me.ParentForm) <> DialogResult.OK Then
				Return
			End If

			id_User = frm.cmb_Users.EditValue

			Me.Cursor = Cursors.WaitCursor

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				For Each row As DataRow In Me.DS_ML.src_ucr_Emulation_Games.Rows
					If TC.IsNullNothingOrEmpty(row("id_Emu_Games_Owner")) Then
						If TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Users_Emu_Games FROM tbl_Users_Emu_Games WHERE id_Users = " & TC.getSQLFormat(id_User) & " AND id_Emu_Games = " & row("id_Emu_Games"), tran), 0) > 0 Then
							row("tmp_Highlighted") = True
						Else
							row("tmp_Highlighted") = False
						End If
					End If
				Next
			End Using

			Me.Cursor = Cursors.Default
		End Using
	End Sub

	Private Sub bbi_Similarity_Calculation_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Similarity_Calculation.ItemClick
		Using frm As New frm_Similarity_Calculation(BS_Emu_Games.Current("id_Emu_Games"), BS_Emu_Games.Current("Game"), BS_Emu_Games.Current("Platform"))
			frm.ShowDialog(Me.ParentForm)

			Refill_cmb_Similarity_Calculation_Results()

			If frm.Updated_Results.Contains(cmb_Similarity_Calculation_Results.EditValue) Then
				Refill_Emu_Games()
			End If
		End Using
	End Sub

	Private Sub cmb_Similarity_Calculation_Results_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_Similarity_Calculation_Results.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Minus Then
			If MKDXHelper.MessageBox("Do you really want to delete the similarity calculation results '" & cmb_Similarity_Calculation_Results.Text & "'?", "Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Similarity_Calculation_Results_Entries WHERE id_Similarity_Calculation_Results = " & TC.getSQLFormat(cmb_Similarity_Calculation_Results.EditValue))
				DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_Similarity_Calculation_Results WHERE id_Similarity_Calculation_Results = " & TC.getSQLFormat(cmb_Similarity_Calculation_Results.EditValue))

				Refill_cmb_Similarity_Calculation_Results()
				cmb_Similarity_Calculation_Results.EditValue = 0
				Refill_Emu_Games()
			End If
		ElseIf e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis Then
			Using frm As New frm_Similarity_Calculation(BS_Similarity_Calculation_Results.Current("id_Similarity_Calculation_Results"))
				frm.ShowDialog(Me.ParentForm)
			End Using
		End If
	End Sub

	Private Sub bbi_Show_Similarity_Features_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Show_Similarity_Feature_Columns.ItemClick
		If Not _bbi_Show_Similarity_Feature_Columns_Shown Then
			_bbi_Show_Similarity_Feature_Columns_Shown = True
			col001_Platform.Visible = True
			col001_Platform.VisibleIndex = 0
			col002_MobyRank.Visible = True
			col002_MobyRank.VisibleIndex = 0
			col003_MobyScore.Visible = True
			col003_MobyScore.VisibleIndex = 0
			col004_Publisher.Visible = True
			col004_Publisher.VisibleIndex = 0
			col005_Developer.Visible = True
			col005_Developer.VisibleIndex = 0
			col006_Year.Visible = True
			col006_Year.VisibleIndex = 0
			col101_Basic_Genres.Visible = True
			col101_Basic_Genres.VisibleIndex = 0
			col102_Perspectives.Visible = True
			col102_Perspectives.VisibleIndex = 0
			col103_Sports_Themes.Visible = True
			col103_Sports_Themes.VisibleIndex = 0
			col105_Educational_Categories.Visible = True
			col105_Educational_Categories.VisibleIndex = 0
			col106_Other_Attributes.Visible = True
			col106_Other_Attributes.VisibleIndex = 0
			col107_Visual_Presentation.Visible = True
			col107_Visual_Presentation.VisibleIndex = 0
			col108_Gameplay.Visible = True
			col108_Gameplay.VisibleIndex = 0
			col109_Pacing.Visible = True
			col109_Pacing.VisibleIndex = 0
			col110_Narrative_Theme_Topic.Visible = True
			col110_Narrative_Theme_Topic.VisibleIndex = 0
			col111_Setting.Visible = True
			col111_Setting.VisibleIndex = 0
			col112_Vehicular_Themes.Visible = True
			col112_Vehicular_Themes.VisibleIndex = 0
			col113_Interface_Control.Visible = True
			col113_Interface_Control.VisibleIndex = 0
			col114_DLC_Addon.Visible = True
			col114_DLC_Addon.VisibleIndex = 0
			col115_Special_Edition.Visible = True
			col115_Special_Edition.VisibleIndex = 0
			col201_MinPlayers.Visible = True
			col201_MinPlayers.VisibleIndex = 0
			col202_MaxPlayers.Visible = True
			col202_MaxPlayers.VisibleIndex = 0
			col203_AgeO.Visible = True
			col203_AgeO.VisibleIndex = 0
			col204_AgeP.Visible = True
			col204_AgeP.VisibleIndex = 0
			col205_Rating_Descriptors.Visible = True
			col205_Rating_Descriptors.VisibleIndex = 0
			col207_Multiplayer_Attributes.Visible = True
			col207_Multiplayer_Attributes.VisibleIndex = 0
			col206_Other_Attributes.Visible = True
			col206_Other_Attributes.VisibleIndex = 0
			col301_Group_Membership.Visible = True
			col301_Group_Membership.VisibleIndex = 0
			col401_Staff.Visible = True
			col401_Staff.VisibleIndex = 0
		Else
			_bbi_Show_Similarity_Feature_Columns_Shown = False
			col001_Platform.Visible = False
			col002_MobyRank.Visible = False
			col003_MobyScore.Visible = False
			col004_Publisher.Visible = False
			col005_Developer.Visible = False
			col006_Year.Visible = False
			col101_Basic_Genres.Visible = False
			col102_Perspectives.Visible = False
			col103_Sports_Themes.Visible = False
			col105_Educational_Categories.Visible = False
			col106_Other_Attributes.Visible = False
			col107_Visual_Presentation.Visible = False
			col108_Gameplay.Visible = False
			col109_Pacing.Visible = False
			col110_Narrative_Theme_Topic.Visible = False
			col111_Setting.Visible = False
			col112_Vehicular_Themes.Visible = False
			col113_Interface_Control.Visible = False
			col114_DLC_Addon.Visible = False
			col115_Special_Edition.Visible = False
			col201_MinPlayers.Visible = False
			col202_MaxPlayers.Visible = False
			col203_AgeO.Visible = False
			col204_AgeP.Visible = False
			col205_Rating_Descriptors.Visible = False
			col207_Multiplayer_Attributes.Visible = False
			col206_Other_Attributes.Visible = False
			col301_Group_Membership.Visible = False
			col401_Staff.Visible = False
		End If
	End Sub

	Private Sub bbi_Open_Similarity_Details_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Open_Similarity_Details.ItemClick
		If BS_Emu_Games.Current Is Nothing Then Return

		Using frm As New frm_Similarity_Calculation_Details(TC.NZ(cmb_Similarity_Calculation_Results.EditValue, 0), BS_Emu_Games.Current.Row, TC.NZ(BS_Emu_Games.Current("id_Emu_Games"), 0), TC.NZ(BS_Emu_Games.Current("id_Moby_Releases"), 0))
			frm.ShowDialog(Me.ParentForm)
		End Using
	End Sub

	Private Sub cmb_Staff_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_Staff.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis Then
			Dim id_Moby_Staff As Integer = TC.NZ(cmb_Staff.EditValue, 0)

			If id_Moby_Staff = 0 Then Return

			Using frm As New frm_Moby_Staff_Info(id_Moby_Staff)
				frm.ShowDialog(Me.ParentForm)
			End Using
		End If
	End Sub

	Private Sub cmb_Groups_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_Groups.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis Then
			Dim id_Moby_Groups As Integer = TC.NZ(cmb_Groups.EditValue, 0)

			If id_Moby_Groups = 0 Then Return

			Using frm As New frm_Moby_Game_Group_Info(id_Moby_Groups)
				frm.ShowDialog(Me.ParentForm)
			End Using
		End If
	End Sub

	Private Sub bbi_Launch_Random_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Launch_Random.ItemClick
		Dim iMaxRow = Me.gv_Emu_Games.DataRowCount

		If iMaxRow < 1 Then
			Return
		End If

		Dim rnd As New Random(Environment.TickCount)
		Dim iChosen As Integer = rnd.Next() Mod iMaxRow

		Dim rowChosen As DataRow = gv_Emu_Games.GetRow(iChosen).Row

		MKNetLib.cls_MKClientSupport.SetBindingSourcePosition(BS_Emu_Games, "id_Emu_Games", rowChosen("id_Emu_Games"))

		Launch_Game()
	End Sub

	Private Sub bbi_USER_Extras_Manager_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbi_USER_Extras_Manager.ItemClick
		If BS_Emu_Games.Current Is Nothing Then Return

		Dim ar_Extras As ArrayList = cls_Extras.FindAllExtras(BS_Emu_Games.Current("Platform_Short"), BS_Emu_Games.Current("id_Moby_Platforms"), BS_Emu_Games.Current("Game"), TC.NZ(BS_Emu_Games.Current("InnerFile"), BS_Emu_Games.Current("File")))

		Using frm As New frm_USER_Extras_Manager(BS_Emu_Games.Current("id_Emu_Games"), ar_Extras)
			If frm.ShowDialog(Me.ParentForm) = DialogResult.OK Then
				For Each old_extra As cls_Extras.cls_Extras_Result In ar_Extras
					Try
						If Not old_extra._NoExtraFound Then
							Alphaleonis.Win32.Filesystem.File.Delete(old_extra._Path)
						End If
					Catch ex As Exception

					End Try
				Next

				For Each row As DataRow In frm.DS_Screenshots.Tables("tbl_Screenshots").Select("", "Sort")
					If TC.NZ(row("Use"), False) = False Then Continue For

					Dim ExtraCategory As String = DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Name FROM tbl_Emu_Extras WHERE id_Emu_Extras = " & TC.getSQLFormat(row("Category")))
					Dim FileName As String = cls_Extras.FindNextFreeExtraFilename(frm.Platform_Short, ExtraCategory, frm.FileName)

					If FileName = "" Then Continue For

					Try
						Dim mediadir As String = cls_Globals.Dir_Extras & "\emulation\" & frm.Platform_Short & "\" & ExtraCategory
						If Not Alphaleonis.Win32.Filesystem.Directory.Exists(mediadir) Then
							Alphaleonis.Win32.Filesystem.Directory.CreateDirectory(mediadir)
						End If
						CType(frm._al_Screenshots.Item(row("id")), Image).Save(cls_Globals.Dir_Extras & "\emulation\" & frm.Platform_Short & "\" & ExtraCategory & "\" & FileName & ".png", System.Drawing.Imaging.ImageFormat.Png)
					Catch ex As Exception

					End Try
				Next

				ApplyFirstExtra()
			End If
		End Using
	End Sub

	Private Sub bbi_Extras_Viewer_Settings_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbi_Extras_Viewer_Settings.ItemClick
		Using frm As New frm_Emu_ImageSettings
			If frm.ShowDialog(Me.ParentForm) = DialogResult.OK Then
				ApplyFirstExtra()
				_Slideshow = TC.NZ(cls_Settings.GetSetting("Emu_Slideshow", cls_Settings.enm_Settingmodes.Per_User), "0") = "1"
				_Slideshow_Delay = CInt(TC.NZ(cls_Settings.GetSetting("Emu_Slideshow_Delay", cls_Settings.enm_Settingmodes.Per_User), "1"))
				tmr_ImageUpdate.Interval = _Slideshow_Delay * 1000
			End If
		End Using
	End Sub

	Private Sub bbi_Statistics_Remove_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbi_Statistics_Remove.ItemClick
		If BS_Emu_Games_History.Current Is Nothing Then Return

		If MKDXHelper.MessageBox("Do you really want to remove the current statistics entry?", "Remove statistics entry", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
			DataAccess.FireProcedure(cls_Globals.Conn, 0, "DELETE FROM tbl_History WHERE id_History = " & TC.getSQLFormat(BS_Emu_Games_History.Current("id_History")))
			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				Refresh_Emu_Game_History(BS_Emu_Games.Current("id_Emu_Games"), tran)
				tran.Commit()
			End Using
		End If
	End Sub

	Private Sub popmnu_Statistics_BeforePopup(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles popmnu_Statistics.BeforePopup
		If Not grd_Statistics.Allow_Popup OrElse BS_Emu_Games_History.Current Is Nothing Then
			e.Cancel = True
		End If
	End Sub

	Private Sub filteringUIContext_QueryRangeData(sender As Object, e As DevExpress.Utils.Filtering.QueryRangeDataEventArgs) Handles filteringUIContext.QueryRangeData
		If e.PropertyPath = "Rank" Then
			e.Result.Minimum = 0
			e.Result.Maximum = 100
		End If

		If e.PropertyPath = "Score" Then
			e.Result.Minimum = 0.0
			e.Result.Maximum = 5.0
		End If

		If e.PropertyPath = "MinPlayers" Then
			e.Result.Minimum = 1
			e.Result.Maximum = Me._FilterUI_MinPlayers_Max
		End If

		If e.PropertyPath = "MaxPlayers" Then
			e.Result.Minimum = 1
			e.Result.Maximum = Me._FilterUI_MaxPlayers_Max
		End If
	End Sub

	Private _FilterUI_ar_Year As ArrayList
	Private _FilterUI_ar_Platform As ArrayList
	Private _FilterUI_ar_Basic_Genres As ArrayList
	Private _FilterUI_ar_Perspectives As ArrayList
	Private _FilterUI_ar_Sports_Themes As ArrayList
	Private _FilterUI_ar_Educational_Categories As ArrayList
	Private _FilterUI_ar_Other_Attributes As ArrayList
	Private _FilterUI_ar_Visual_Presentation As ArrayList
	Private _FilterUI_ar_Pacing As ArrayList
	Private _FilterUI_ar_Gameplay As ArrayList
	Private _FilterUI_ar_Interface_Control As ArrayList
	Private _FilterUI_ar_Vehicular_Themes As ArrayList
	Private _FilterUI_ar_Setting As ArrayList
	Private _FilterUI_ar_Narrative_Theme_Topic As ArrayList
	Private _FilterUI_ar_DLC_Addon As ArrayList
	Private _FilterUI_ar_Special_Edition As ArrayList
	Private _FilterUI_ar_Regions As ArrayList
	Private _FilterUI_ar_Languages As ArrayList
	Private _FilterUI_ar_MP_GameModes As ArrayList
	Private _FilterUI_ar_MP_Options As ArrayList

	Private _FilterUI_MinPlayers_Max As Integer
	Private _FilterUI_MaxPlayers_Max As Integer

	Private Sub Prepare_filteringUIContext_QueryRangeData()
		Dim bWaitCursor = False
		If Cursor <> Cursors.WaitCursor Then
			bWaitCursor = True
			Cursor = Cursors.WaitCursor
		End If

		Me._FilterUI_ar_Year = New ArrayList
		Me._FilterUI_ar_Platform = New ArrayList
		Me._FilterUI_ar_Basic_Genres = New ArrayList
		Me._FilterUI_ar_Perspectives = New ArrayList
		Me._FilterUI_ar_Visual_Presentation = New ArrayList
		Me._FilterUI_ar_Pacing = New ArrayList
		Me._FilterUI_ar_Gameplay = New ArrayList
		Me._FilterUI_ar_Interface_Control = New ArrayList
		Me._FilterUI_ar_Vehicular_Themes = New ArrayList
		Me._FilterUI_ar_Setting = New ArrayList
		Me._FilterUI_ar_Narrative_Theme_Topic = New ArrayList
		Me._FilterUI_ar_DLC_Addon = New ArrayList
		Me._FilterUI_ar_Special_Edition = New ArrayList
		Me._FilterUI_ar_Sports_Themes = New ArrayList
		Me._FilterUI_ar_Educational_Categories = New ArrayList
		Me._FilterUI_ar_Other_Attributes = New ArrayList
		Me._FilterUI_ar_Regions = New ArrayList
		Me._FilterUI_ar_Languages = New ArrayList
		Me._FilterUI_ar_MP_GameModes = New ArrayList
		Me._FilterUI_ar_MP_Options = New ArrayList

		Me._FilterUI_MinPlayers_Max = 1
		Me._FilterUI_MaxPlayers_Max = 1

		For Each row As DataRow In Me.DS_ML.src_ucr_Emulation_Games.Rows
			AddToFilterUIArray(row("Year"), Me._FilterUI_ar_Year)
			AddToFilterUIArray(row("Platform"), Me._FilterUI_ar_Platform)
			AddToFilterUIArray(row("Basic_Genres"), Me._FilterUI_ar_Basic_Genres)
			AddToFilterUIArray(row("Perspectives"), Me._FilterUI_ar_Perspectives)
			AddToFilterUIArray(row("Sports_Themes"), Me._FilterUI_ar_Sports_Themes)
			AddToFilterUIArray(row("Educational_Categories"), Me._FilterUI_ar_Educational_Categories)
			AddToFilterUIArray(row("Other_Attributes"), Me._FilterUI_ar_Other_Attributes)
			AddToFilterUIArray(row("Visual_Presentation"), Me._FilterUI_ar_Visual_Presentation)
			AddToFilterUIArray(row("Pacing"), Me._FilterUI_ar_Pacing)
			AddToFilterUIArray(row("Gameplay"), Me._FilterUI_ar_Gameplay)
			AddToFilterUIArray(row("Interface_Control"), Me._FilterUI_ar_Interface_Control)
			AddToFilterUIArray(row("Vehicular_Themes"), Me._FilterUI_ar_Vehicular_Themes)
			AddToFilterUIArray(row("Setting"), Me._FilterUI_ar_Setting)
			AddToFilterUIArray(row("Narrative_Theme_Topic"), Me._FilterUI_ar_Narrative_Theme_Topic)
			AddToFilterUIArray(row("DLC_Addon"), Me._FilterUI_ar_DLC_Addon)
			AddToFilterUIArray(row("Special_Edition"), Me._FilterUI_ar_Special_Edition)
			AddToFilterUIArray(row("Regions"), Me._FilterUI_ar_Regions)
			AddToFilterUIArray(row("Languages"), Me._FilterUI_ar_Languages)
			AddToFilterUIArray(row("MP_GameModes"), Me._FilterUI_ar_MP_GameModes)
			AddToFilterUIArray(row("MP_Options"), Me._FilterUI_ar_MP_Options)

			If TC.NZ(row("MinPlayers"), 0) > Me._FilterUI_MinPlayers_Max Then Me._FilterUI_MinPlayers_Max = TC.NZ(row("MinPlayers"), 0)
			If TC.NZ(row("MaxPlayers"), 0) > Me._FilterUI_MaxPlayers_Max Then Me._FilterUI_MaxPlayers_Max = TC.NZ(row("MaxPlayers"), 0)
		Next

		Me._FilterUI_ar_Year.Sort()
		Me._FilterUI_ar_Platform.Sort()
		Me._FilterUI_ar_Basic_Genres.Sort()
		Me._FilterUI_ar_Perspectives.Sort()
		Me._FilterUI_ar_Sports_Themes.Sort()
		Me._FilterUI_ar_Educational_Categories.Sort()
		Me._FilterUI_ar_Visual_Presentation.Sort()
		Me._FilterUI_ar_Pacing.Sort()
		Me._FilterUI_ar_Gameplay.Sort()
		Me._FilterUI_ar_Interface_Control.Sort()
		Me._FilterUI_ar_Vehicular_Themes.Sort()
		Me._FilterUI_ar_Setting.Sort()
		Me._FilterUI_ar_Narrative_Theme_Topic.Sort()
		Me._FilterUI_ar_DLC_Addon.Sort()
		Me._FilterUI_ar_Special_Edition.Sort()
		Me._FilterUI_ar_Sports_Themes.Sort()
		Me._FilterUI_ar_Educational_Categories.Sort()
		Me._FilterUI_ar_Other_Attributes.Sort()
		Me._FilterUI_ar_Regions.Sort()
		Me._FilterUI_ar_Languages.Sort()
		Me._FilterUI_ar_MP_GameModes.Sort()
		Me._FilterUI_ar_MP_Options.Sort()

		If bWaitCursor Then
			Cursor = Cursors.Default
		End If
	End Sub

	Private Sub AddToFilterUIArray(ByVal oItem As Object, ByVal arList As ArrayList)
		If Not TC.IsNullNothingOrEmpty(oItem) Then
			Dim sItem As String = oItem.ToString

			For Each sItemSplit As String In sItem.Split(",")
				sItemSplit = sItemSplit.Trim

				If sItemSplit <> "" Then
					If Not arList.Contains(sItemSplit) Then
						arList.Add(sItemSplit)
					End If
				End If
			Next
		End If
	End Sub

	Private Sub filteringUIContext_QueryLookupData(sender As Object, e As QueryLookupDataEventArgs) Handles filteringUIContext.QueryLookupData
		If e.PropertyPath = "Year" AndAlso Me._FilterUI_ar_Year IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_Year.ToArray(GetType(String))
		If e.PropertyPath = "Platform" AndAlso Me._FilterUI_ar_Platform IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_Platform.ToArray(GetType(String))
		If e.PropertyPath = "Basic_Genres" AndAlso Me._FilterUI_ar_Basic_Genres IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_Basic_Genres.ToArray(GetType(String))
		If e.PropertyPath = "Perspectives" AndAlso Me._FilterUI_ar_Perspectives IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_Perspectives.ToArray(GetType(String))
		If e.PropertyPath = "Sports_Themes" AndAlso Me._FilterUI_ar_Sports_Themes IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_Sports_Themes.ToArray(GetType(String))
		If e.PropertyPath = "Educational_Categories" AndAlso Me._FilterUI_ar_Educational_Categories IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_Educational_Categories.ToArray(GetType(String))
		If e.PropertyPath = "Other_Attributes" AndAlso Me._FilterUI_ar_Other_Attributes IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_Other_Attributes.ToArray(GetType(String))

		If e.PropertyPath = "Visual_Presentation" AndAlso Me._FilterUI_ar_Visual_Presentation IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_Visual_Presentation.ToArray(GetType(String))
		If e.PropertyPath = "Pacing" AndAlso Me._FilterUI_ar_Pacing IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_Pacing.ToArray(GetType(String))
		If e.PropertyPath = "Gameplay" AndAlso Me._FilterUI_ar_Gameplay IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_Gameplay.ToArray(GetType(String))
		If e.PropertyPath = "Interface_Control" AndAlso Me._FilterUI_ar_Interface_Control IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_Interface_Control.ToArray(GetType(String))
		If e.PropertyPath = "Vehicular_Themes" AndAlso Me._FilterUI_ar_Vehicular_Themes IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_Vehicular_Themes.ToArray(GetType(String))
		If e.PropertyPath = "Setting" AndAlso Me._FilterUI_ar_Setting IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_Setting.ToArray(GetType(String))
		If e.PropertyPath = "Narrative_Theme_Topic" AndAlso Me._FilterUI_ar_Narrative_Theme_Topic IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_Narrative_Theme_Topic.ToArray(GetType(String))
		If e.PropertyPath = "DLC_Addon" AndAlso Me._FilterUI_ar_DLC_Addon IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_DLC_Addon.ToArray(GetType(String))
		If e.PropertyPath = "Special_Edition" AndAlso Me._FilterUI_ar_Special_Edition IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_Special_Edition.ToArray(GetType(String))

		If e.PropertyPath = "Regions" AndAlso Me._FilterUI_ar_Regions IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_Regions.ToArray(GetType(String))
		If e.PropertyPath = "Languages" AndAlso Me._FilterUI_ar_Languages IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_Languages.ToArray(GetType(String))
		If e.PropertyPath = "MP_GameModes" AndAlso Me._FilterUI_ar_MP_GameModes IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_MP_GameModes.ToArray(GetType(String))
		If e.PropertyPath = "MP_Options" AndAlso Me._FilterUI_ar_MP_Options IsNot Nothing Then e.Result.DataSource = Me._FilterUI_ar_MP_Options.ToArray(GetType(String))
	End Sub

	Private _FilterUI_CriteriaChanged As Integer = 0

	Private Function Generate_Filter_String(filterCriteria As DevExpress.Data.Filtering.CriteriaOperator) As String
		Dim filterString As String = ""

		If filterCriteria.GetType Is GetType(DevExpress.Data.Filtering.GroupOperator) Then
			' TODO: Concat all subsequent Operators with the Operatortype ("AND", "OR" etc.)
			Dim op As DevExpress.Data.Filtering.GroupOperator = CType(filterCriteria, DevExpress.Data.Filtering.GroupOperator)

			For Each subop As DevExpress.Data.Filtering.CriteriaOperator In op.Operands
				Dim opType As String = "AND"
				If op.OperatorType = DevExpress.Data.Filtering.GroupOperatorType.Or Then
					opType = "OR"
				End If
				filterString &= IIf(filterString = "", "(", " " & opType & " ") & Generate_Filter_String(subop)
			Next
			filterString &= ")"
		ElseIf filterCriteria.GetType Is GetType(DevExpress.Data.Filtering.BinaryOperator) AndAlso CType(filterCriteria, DevExpress.Data.Filtering.BinaryOperator).OperatorType = DevExpress.Data.Filtering.BinaryOperatorType.Equal Then
			' TODO: transform to "Contains([col], 'str')"
			Dim op As DevExpress.Data.Filtering.BinaryOperator = CType(filterCriteria, DevExpress.Data.Filtering.BinaryOperator)
			filterString = "Contains(" & op.LeftOperand.ToString & ", " & op.RightOperand.ToString & ")"
		ElseIf filterCriteria.GetType Is GetType(DevExpress.Data.Filtering.InOperator) Then
			' TODO: transform to "(Contains([col], 'str1') Or Contains([col], 'str2')"
			Dim op As DevExpress.Data.Filtering.InOperator = CType(filterCriteria, DevExpress.Data.Filtering.InOperator)
			Dim col As String = op.LeftOperand.ToString
			For Each val As DevExpress.Data.Filtering.OperandValue In op.Operands
				filterString &= IIf(filterString = "", "(", " OR ") & "Contains(" & col & ", " & val.ToString & ")"
			Next
			filterString &= ")"
		Else
			Return filterCriteria.ToString
		End If

		Return filterString
	End Function

	Private Sub filteringUIContext_FilterCriteriaChanged(ByVal sender As Object, ByVal e As FilterCiteriaChangedEventArgs) Handles filteringUIContext.FilterCriteriaChanged
		_FilterUI_CriteriaChanged += 1

		Dim newFilter = ""

		If e.FilterCriteria IsNot Nothing Then
			newFilter = Generate_Filter_String(e.FilterCriteria)
		End If

		Me.gv_Emu_Games.ActiveFilterString = newFilter

		_FilterUI_CriteriaChanged -= 1
	End Sub

	Private Sub gv_Emu_Games_ColumnFilterChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_Emu_Games.ColumnFilterChanged
		If _FilterUI_CriteriaChanged > 0 Then
			Return
		End If
		If gv_Emu_Games.ActiveFilter.IsEmpty Then
			filteringUIContext.ClearFilterCriteria()
		End If
	End Sub

	Private Sub gv_Emu_Games_FilterEditorCreated(sender As Object, e As FilterControlEventArgs) Handles gv_Emu_Games.FilterEditorCreated
		Dim al_RemoveColumns As New ArrayList

		For Each col As DevExpress.XtraGrid.FilterEditor.GridFilterColumn In e.FilterControl.FilterColumns
			If col.ColumnCaption.Contains("«") Then
				al_RemoveColumns.Add(col)
			End If
		Next

		For Each col As DevExpress.XtraGrid.FilterEditor.GridFilterColumn In al_RemoveColumns
			e.FilterControl.FilterColumns.Remove(col)
		Next
	End Sub

	Private Sub bbi_Export_XLSX_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbi_Export_XLSX.ItemClick
		Dim filename As String = MKNetLib.cls_MKFileSupport.SaveFileDialog("Export to Excel", "Excel Files (*.xlsx)|*.xlsx", 0, "xlsx")
		If filename <> "" Then
			Me.gv_Emu_Games.ExportToXlsx(filename)
		End If
	End Sub

	Private Sub bbi_Export_CSV_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbi_Export_CSV.ItemClick
		Dim filename As String = MKNetLib.cls_MKFileSupport.SaveFileDialog("Export to CSV", "CSV Files (*.csv)|*.csv", 0, "csv")
		If filename <> "" Then
			Me.gv_Emu_Games.ExportToCsv(filename)
		End If
	End Sub

	Private Sub gv_Emu_Games_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles gv_Emu_Games.FocusedRowChanged
		If TC.NZ(gv_Emu_Games.GetIncrementalText(), "") <> "" Then
			Me.Select_BindingSource_Row_on_gv_Emu_Games()
		End If
	End Sub

	Private Sub Moby_Extras_Downloader_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles Moby_Extras_Downloader.DownloadProgressChanged
		Me.prg_Extras_Download.EditValue = e.ProgressPercentage
	End Sub

	Private Sub bbi_MOBY_Extras_Manager_ItemClick(sender As Object, e As ItemClickEventArgs) Handles bbi_MOBY_Extras_Manager.ItemClick
		If BS_Emu_Games.Current Is Nothing Then
			Return
		End If

		Using frm As New frm_MOBY_Extras_Manager(TC.NZ(BS_Emu_Games.Current("id_Moby_Releases"), 0L), BS_Emu_Games.Current("Platform_Short"))
			If frm.ShowDialog = DialogResult.OK Then
				'TODO: Reload extras, reset extras counter
			End If
		End Using
	End Sub
End Class