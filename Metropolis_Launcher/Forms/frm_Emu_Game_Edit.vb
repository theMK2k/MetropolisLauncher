Imports System.ComponentModel

Public Class frm_Emu_Game_Edit

	''' <summary>
	''' If _id_Emu_Games stays 0 we have a multiple Emu Game Edit (Template)
	''' </summary>
	''' <remarks></remarks>
	Private _id_Emu_Games As Integer = 0

	Private _id_Moby_Platforms As Long = 0  'On Multi-Edit, if a platform has been chosen - we can provide a default emulators selection
	Private _MultiVolume As Boolean = False 'On Single-Edit, if the platform supports multiple volumes/disks
	Private _is_DOS_or_Booter As Boolean = False  'On Single-Edit, if the platform is DOS or Booter
	Private _is_ScummVM As Boolean = False  'On Single-Edit, if the platform is ScummVM

	Private _id_Emu_Games_Multi As Integer()
	Private _MultiEdit As Boolean = False
	Private _id_Rombase As Integer = 0
	Private _AutoImport As Boolean = False

	Private _Monitor_TextBoxChanges As Boolean = False

	Private _Name_OldValue As Object
	Private _Prefix_OldValue As Object
	Private _Notes_OldValue As Object
	Private _Description_OldValue As Object
	Private _Special_Info_OldValue As Object
	Private _Publisher_OldValue As Object
	Private _Developer_OldValue As Object
	Private _Year_OldValue As Object
	Private _Version_OldValue As Object
	Private _Alt_OldValue As Object
	Private _id_Emulators As Object = DBNull.Value
	Private _id_Emulators_Default As Object = DBNull.Value
	Private _Mapping_Identifier As String = ""

	''' <summary>
	''' Constructor for Single-Edit mode
	''' </summary>
	''' <param name="id_Emu_Games"></param>
	''' <remarks></remarks>
	Public Sub New(ByVal id_Emu_Games As Integer)
		InitializeComponent()

#If DEBUG Then
		Me.tpg_Regions_New.PageVisible = True
#End If

		_id_Emu_Games = id_Emu_Games

		Cursor.Current = Cursors.WaitCursor

		'Fill the J2K Config DS
		cls_Settings.Fill_J2K_DS(Me.DS_J2K, TC.NZ(cls_Settings.GetSetting("Path_J2K"), ""))

		barmng.SetPopupContextMenu(grd_Genres, popmnu_Genres)
		barmng.SetPopupContextMenu(grd_Perspectives, popmnu_Genres)
		barmng.SetPopupContextMenu(grd_Sports_Themes, popmnu_Genres)
		barmng.SetPopupContextMenu(grd_Educational_Categories, popmnu_Genres)
		barmng.SetPopupContextMenu(grd_Other_Attributes, popmnu_Genres)
		barmng.SetPopupContextMenu(grd_Visual_Presentation, popmnu_Genres)
		barmng.SetPopupContextMenu(grd_Pacing, popmnu_Genres)
		barmng.SetPopupContextMenu(grd_Gameplay, popmnu_Genres)
		barmng.SetPopupContextMenu(grd_Interface_Control, popmnu_Genres)
		barmng.SetPopupContextMenu(grd_Vehicular_Themes, popmnu_Genres)
		barmng.SetPopupContextMenu(grd_Setting, popmnu_Genres)
		barmng.SetPopupContextMenu(grd_Narrative_Theme_Topic, popmnu_Genres)
		barmng.SetPopupContextMenu(grd_DLC_Addon, popmnu_Genres)
		barmng.SetPopupContextMenu(grd_Special_Edition, popmnu_Genres)

		barmng.SetPopupContextMenu(grd_Attributes, popmnu_TechInfo)
		barmng.SetPopupContextMenu(grd_Regions, popmnu_Regions)
		barmng.SetPopupContextMenu(grd_Languages, popmnu_Languages)

	End Sub

	''' <summary>
	''' Constructor for Multi-Edit mode
	''' </summary>
	''' <param name="id_Emu_Games"></param>
	''' <remarks></remarks>
	Public Sub New(ByVal id_Emu_Games() As Integer, Optional ByVal ExtraCaption As String = "", Optional ByVal Mapping_Identifier As String = "", Optional ByVal AutoImport As Boolean = False, Optional ByVal id_Moby_Platforms As Long = 0)
		Me.New(0)
		Me._id_Moby_Platforms = id_Moby_Platforms
		Me.Text = "Edit Multiple Games" & IIf(ExtraCaption.Length > 0, " - " & ExtraCaption, "")
		Me._id_Emu_Games_Multi = id_Emu_Games
		Me._MultiEdit = True

		Me.cmb_J2K_Config.Properties.NullValuePrompt = "<do not change>"

		Me._Mapping_Identifier = Mapping_Identifier
		Me._AutoImport = AutoImport

		For Each ctl As Object In tpg_ReleaseInfo.Controls
			If ctl.GetType Is GetType(MKNetDXLib.ctl_MKDXCheckEdit) Then
				CType(ctl, MKNetDXLib.ctl_MKDXCheckEdit).Properties.AllowGrayed = True
				CType(ctl, MKNetDXLib.ctl_MKDXCheckEdit).Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.InactiveChecked
				CType(ctl, MKNetDXLib.ctl_MKDXCheckEdit).CheckState = CheckState.Indeterminate
			End If
		Next

		Me.rpi_Genres.AllowGrayed = True
		Me.rpi_DLC_Addon.AllowGrayed = True
		Me.rpi_Educational_Categories.AllowGrayed = True
		Me.rpi_Gameplay.AllowGrayed = True
		Me.rpi_Interface_Control.AllowGrayed = True
		Me.rpi_Language.AllowGrayed = True
		Me.rpi_Narrative_Theme_Topic.AllowGrayed = True
		Me.rpi_Other_Attributes.AllowGrayed = True
		Me.rpi_Pacing.AllowGrayed = True
		Me.rpi_Perspectives.AllowGrayed = True
		Me.rpi_Regions.AllowGrayed = True
		Me.rpi_Setting.AllowGrayed = True
		Me.rpi_Special_Edition.AllowGrayed = True
		Me.rpi_Sports_Themes.AllowGrayed = True
		Me.rpi_TechInfo.AllowGrayed = True
		Me.rpi_Vehicular_Themes.AllowGrayed = True
		Me.rpi_Visual_Presentation.AllowGrayed = True
	End Sub

	Private Sub frm_Emu_Game_Edit_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
		Cursor.Current = Cursors.WaitCursor

		Dim bTemplateKnown As Boolean = False

		Dim sSQL As String = ""

		If _MultiEdit Then 'Emu_Game_Edit with multiple Games
			'Get id_Emu_Games that fits the Mapping_Identifier in order to load some pre-set Attributes
			If _Mapping_Identifier.Length > 0 Then
				_id_Emu_Games = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_Emu_Games FROM tbl_Emu_Games WHERE Mapping_Identifier = " & TC.getSQLFormat(_Mapping_Identifier)), 0)
			End If

			'Get id_Rombase that fits the Mapping_Identifier in order to load some pre-set Attributes
			If _Mapping_Identifier.Length > 0 Then
				_id_Rombase = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_Rombase FROM rombase.tbl_Rombase WHERE Mapping_Identifier = " & TC.getSQLFormat(_Mapping_Identifier)), 0)
			End If

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				DS_ML.Fill_src_frm_Emu_Game_Edit_Genres(tran, DS_ML.src_frm_Emu_Game_Edit_Genres, _id_Emu_Games, _id_Rombase, _MultiEdit)
				DS_ML.Fill_src_frm_Emu_Game_Edit_Attributes(tran, DS_ML.src_frm_Emu_Game_Edit_Attributes, _id_Emu_Games, _id_Rombase, _MultiEdit)
				DS_ML.Fill_tbl_Moby_Regions(tran, DS_ML.tbl_Moby_Regions)
				tran.Rollback()
			End Using

			sSQL = ""
			sSQL &= "	SELECT"
			sSQL &= "		id_Languages"
			sSQL &= "		, Language_Short"
			sSQL &= "		, Language"
			sSQL &= "		, CASE	WHEN EXISTS(SELECT 1 FROM rombase.tbl_Rombase_Languages RBL WHERE RBL.id_Languages = L.id_Languages AND RBL.id_Rombase = " & TC.getSQLFormat(_id_Rombase) & ") THEN 1"
			sSQL &= "						WHEN EXISTS(SELECT 1 FROM tbl_Emu_Games_Languages EGL WHERE EGL.id_Languages = L.id_Languages AND EGL.id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games) & ") THEN 1"
			sSQL &= "						ELSE NULL END AS Used" & ControlChars.CrLf
			sSQL &= "	FROM tbl_Languages L"
			DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, DS_ML.tbl_Emu_Games_Edit_Languages)

			sSQL = ""
			sSQL &= "	SELECT"
			sSQL &= "		id_Regions"
			sSQL &= "		, Region"
			sSQL &= "		, CASE	WHEN EXISTS(SELECT 1 FROM rombase.tbl_Rombase_Regions RBR WHERE RBR.id_Regions = R.id_Regions AND RBR.id_Rombase = " & TC.getSQLFormat(_id_Rombase) & ") THEN 1 "
			sSQL &= "						WHEN EXISTS(SELECT 1 FROM tbl_Emu_Games_Regions EGR WHERE EGR.id_Regions = R.id_Regions AND EGR.id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games) & ") THEN 1 "
			sSQL &= "						ELSE NULL END AS Used" & ControlChars.CrLf
			sSQL &= "	FROM tbl_Regions R ORDER BY R.Sort, R.Region"
			DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, DS_ML.tbl_Emu_Games_Edit_Regions)

			'Get Name, Prefix, Notes and Description
			sSQL = "	SELECT"
			sSQL &= "		EMUGAME.Name"
			sSQL &= "		, EMUGAME.Name_Prefix"
			sSQL &= "		, EMUGAME.Note"
			sSQL &= "		, EMUGAME.J2KPreset"
			sSQL &= "		, IFNULL(EMUGAME.Description, RB.Description) AS Description"
			sSQL &= "		, IFNULL(EMUGAME.SpecialInfo, RB.SpecialInfo) AS SpecialInfo"
			sSQL &= "		, IFNULL(EMUGAME.Publisher, RB.Publisher) AS Publisher"
			sSQL &= "		, IFNULL(EMUGAME.Developer, RB.Developer) AS Developer"
			sSQL &= "		, IFNULL(EMUGAME.Year, RB.Year) AS Year"
			sSQL &= "		, NULL AS id_Emulators"
			sSQL &= "		, IFNULL(EMUGAME.Version, RB.Version) AS Version"
			sSQL &= "		, IFNULL(EMUGAME.Alt, RB.Alt) AS Alt"
			sSQL &= "		, IFNULL(EMUGAME.Trainer, RB.Trainer) AS Trainer"
			sSQL &= "		, IFNULL(EMUGAME.Translation, RB.Translation) AS Translation"
			sSQL &= "		, IFNULL(EMUGAME.Hack, RB.Hack) AS Hack"
			sSQL &= "		, IFNULL(EMUGAME.Bios, RB.Bios) AS Bios"
			sSQL &= "		, IFNULL(EMUGAME.Prototype, RB.Prototype) AS Prototype"
			sSQL &= "		, IFNULL(EMUGAME.Alpha, RB.Alpha) AS Alpha"
			sSQL &= "		, IFNULL(EMUGAME.Beta, RB.Beta) AS Beta"
			sSQL &= "		, IFNULL(EMUGAME.Sample, RB.Sample) AS Sample"
			sSQL &= "		, IFNULL(EMUGAME.Kiosk, RB.Kiosk) AS Kiosk"
			sSQL &= "		, IFNULL(EMUGAME.Unlicensed, RB.Unlicensed) AS Unlicensed"
			sSQL &= "		, IFNULL(EMUGAME.Fixed, RB.Fixed) AS Fixed"
			sSQL &= "		, IFNULL(EMUGAME.Pirated, RB.Pirated) AS Pirated"
			sSQL &= "		, IFNULL(EMUGAME.Good, RB.Good) AS Good"
			sSQL &= "		, IFNULL(EMUGAME.Bad, RB.Bad) AS Bad"
			sSQL &= "		, IFNULL(EMUGAME.Overdump, RB.Overdump) AS Overdump"
			sSQL &= "	FROM tbl_Emu_Games EMUGAME"
			sSQL &= "	LEFT JOIN rombase.tbl_Rombase RB ON RB.id_Rombase = " & TC.getSQLFormat(_id_Rombase)
			sSQL &= "	WHERE EMUGAME.id_Emu_Games = " & _id_Emu_Games
			Dim dt As DataTable = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL)

			If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then bTemplateKnown = True

			If dt Is Nothing Or dt.Rows.Count <> 1 Then
				'Get Name, Prefix, Notes and Description
				sSQL = "	SELECT"
				sSQL &= "		NULL AS Name"
				sSQL &= "		, NULL AS Name_Prefix"
				sSQL &= "		, NULL AS Note"
				sSQL &= "		, RB.Description"
				sSQL &= "		, RB.SpecialInfo"
				sSQL &= "		, RB.Publisher"
				sSQL &= "		, RB.Developer"
				sSQL &= "		, RB.Year"
				sSQL &= "		, NULL AS id_Emulators"
				sSQL &= "		, RB.Version"
				sSQL &= "		, RB.Alt"
				sSQL &= "		, RB.Trainer"
				sSQL &= "		, RB.Translation"
				sSQL &= "		, RB.Hack"
				sSQL &= "		, RB.Bios"
				sSQL &= "		, RB.Prototype"
				sSQL &= "		, RB.Alpha"
				sSQL &= "		, RB.Beta"
				sSQL &= "		, RB.Sample"
				sSQL &= "		, RB.Kiosk"
				sSQL &= "		, RB.Unlicensed"
				sSQL &= "		, RB.Fixed"
				sSQL &= "		, RB.Pirated"
				sSQL &= "		, RB.Good"
				sSQL &= "		, RB.Bad"
				sSQL &= "		, RB.Overdump"
				sSQL &= "	FROM rombase.tbl_Rombase RB WHERE RB.id_Rombase = " & TC.getSQLFormat(_id_Rombase)
				dt.Clear()
				dt = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL)
			End If

			If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then bTemplateKnown = True

			If dt Is Nothing Or dt.Rows.Count <> 1 Then
				'Get Name, Prefix, Notes and Description
				sSQL = "	SELECT"
				sSQL &= "		NULL AS Name"
				sSQL &= "		, NULL AS Name_Prefix"
				sSQL &= "		, NULL AS Note"
				sSQL &= "		, NULL AS Description"
				sSQL &= "		, NULL AS SpecialInfo"
				sSQL &= "		, NULL AS Publisher"
				sSQL &= "		, NULL AS Developer"
				sSQL &= "		, NULL AS Year"
				sSQL &= "		, NULL AS id_Emulators"
				sSQL &= "		, NULL AS Version"
				sSQL &= "		, NULL AS Alt"
				sSQL &= "		, NULL AS Trainer"
				sSQL &= "		, NULL AS Translation"
				sSQL &= "		, NULL AS Hack"
				sSQL &= "		, NULL AS Bios"
				sSQL &= "		, NULL AS Prototype"
				sSQL &= "		, NULL AS Alpha"
				sSQL &= "		, NULL AS Beta"
				sSQL &= "		, NULL AS Sample"
				sSQL &= "		, NULL AS Kiosk"
				sSQL &= "		, NULL AS Unlicensed"
				sSQL &= "		, NULL AS Fixed"
				sSQL &= "		, NULL AS Pirated"
				sSQL &= "		, NULL AS Good"
				sSQL &= "		, NULL AS Bad"
				sSQL &= "		, NULL AS Overdump"
				dt.Clear()
				dt = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL)
			End If

			If dt Is Nothing Or dt.Rows.Count <> 1 Then
				MKDXHelper.MessageBox("There was a problem loading the data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Cursor.Current = Cursors.Default
				Me.Close()
			End If

			'OF COURSE On MultiEdit, emulators can be selected
			Me.cmb_Default_Emulator.Properties.NullText = "<do not change>"
			If Me._id_Moby_Platforms > 0 Then
				sSQL = "		SELECT"
				sSQL &= "			-1 AS id_Emulators"
				sSQL &= "			, '<remove all and use global default emulator>' AS Displayname"
				sSQL &= "			, NULL AS J2KPreset"
				sSQL &= "		UNION ALL"
				sSQL &= "		SELECT"
				sSQL &= "			EMP.id_Emulators"
				sSQL &= "			, E.Displayname || CASE WHEN EMP.DefaultEmulator = 1 THEN ' (global default)' ELSE '' END AS Displayname"
				sSQL &= "			, E.J2KPreset"
				sSQL &= "		FROM"
				sSQL &= "		tbl_Emulators_Moby_Platforms EMP"
				sSQL &= "		INNER JOIN tbl_Emulators E ON EMP.id_Emulators = E.id_Emulators"
				sSQL &= "		WHERE id_Moby_Platforms = " & TC.getSQLFormat(_id_Moby_Platforms)
				sSQL &= "		ORDER BY Displayname"
				DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, DS_ML.tbl_Emu_Games_Edit_Default_Emulator)
			End If

			'If Not TC.IsNullNothingOrEmpty(dt.Rows(0)("id_Emulators")) Then
			'	cmb_Default_Emulator.EditValue = dt.Rows(0)("id_Emulators")
			'	_id_Emulators = dt.Rows(0)("id_Emulators")
			'End If

			'_id_Emulators_Default = DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_Emulators FROM tbl_Emulators_Moby_Platforms EMP WHERE EMP.id_Moby_Platforms = (SELECT id_Moby_Platforms FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games) & ") AND EMP.DefaultEmulator = 1")

			Me.txb_Name.EditValue = dt.Rows(0)("Name")
			_Name_OldValue = dt.Rows(0)("Name")

			Me.txb_Prefix.EditValue = dt.Rows(0)("Name_Prefix")
			_Prefix_OldValue = dt.Rows(0)("Name_Prefix")

			Me.txb_Notes.EditValue = dt.Rows(0)("Note")
			_Notes_OldValue = dt.Rows(0)("Note")

			Me.txb_Description.EditValue = dt.Rows(0)("Description") 'MKNetLib.cls_MKISO_8859_1_Replace.ToASCII(System.Text.RegularExpressions.Regex.Replace(TC.NZ(dt.Rows(0)("Description"), "").Replace("<br>", ControlChars.CrLf), "<.*?>", ""), True)
			_Description_OldValue = dt.Rows(0)("Description") 'MKNetLib.cls_MKISO_8859_1_Replace.ToASCII(System.Text.RegularExpressions.Regex.Replace(TC.NZ(dt.Rows(0)("Description"), "").Replace("<br>", ControlChars.CrLf), "<.*?>", ""), True)

			Me.txb_Special_Info.EditValue = TC.NZ(dt.Rows(0)("SpecialInfo"), "")
			_Special_Info_OldValue = TC.NZ(dt.Rows(0)("SpecialInfo"), "")

			Me.txb_Publisher.EditValue = dt.Rows(0)("Publisher")
			_Publisher_OldValue = dt.Rows(0)("Publisher")

			Me.txb_Developer.EditValue = dt.Rows(0)("Developer")
			_Developer_OldValue = dt.Rows(0)("Developer")

			Me.txb_Year.EditValue = dt.Rows(0)("Year")
			_Year_OldValue = dt.Rows(0)("Year")

			Me.txb_Version.EditValue = dt.Rows(0)("Version")
			_Version_OldValue = dt.Rows(0)("Version")

			Me.txb_Alt.EditValue = dt.Rows(0)("Alt")
			_Alt_OldValue = dt.Rows(0)("Alt")

			chb_Alpha.EditValue = IIf(dt.Rows(0)("Alpha") Is DBNull.Value, DBNull.Value, TC.NZ(dt.Rows(0)("Alpha"), False))
			chb_Bad.EditValue = IIf(dt.Rows(0)("Bad") Is DBNull.Value, DBNull.Value, TC.NZ(dt.Rows(0)("Bad"), False))
			chb_Beta.EditValue = IIf(dt.Rows(0)("Beta") Is DBNull.Value, DBNull.Value, TC.NZ(dt.Rows(0)("Beta"), False))
			chb_Bios.EditValue = IIf(dt.Rows(0)("Bios") Is DBNull.Value, DBNull.Value, TC.NZ(dt.Rows(0)("Bios"), False))
			chb_Sample.EditValue = IIf(dt.Rows(0)("Sample") Is DBNull.Value, DBNull.Value, TC.NZ(dt.Rows(0)("Sample"), False))
			chb_Fixed.EditValue = IIf(dt.Rows(0)("Fixed") Is DBNull.Value, DBNull.Value, TC.NZ(dt.Rows(0)("Fixed"), False))
			chb_Good.EditValue = IIf(dt.Rows(0)("Good") Is DBNull.Value, DBNull.Value, TC.NZ(dt.Rows(0)("Good"), False))
			chb_Hack.EditValue = IIf(dt.Rows(0)("Hack") Is DBNull.Value, DBNull.Value, TC.NZ(dt.Rows(0)("Hack"), False))
			chb_Kiosk.EditValue = IIf(dt.Rows(0)("Kiosk") Is DBNull.Value, DBNull.Value, TC.NZ(dt.Rows(0)("Kiosk"), False))
			chb_Overdump.EditValue = IIf(dt.Rows(0)("Overdump") Is DBNull.Value, DBNull.Value, TC.NZ(dt.Rows(0)("Overdump"), False))
			chb_Pirated.EditValue = IIf(dt.Rows(0)("Pirated") Is DBNull.Value, DBNull.Value, TC.NZ(dt.Rows(0)("Pirated"), False))
			chb_Prototype.EditValue = IIf(dt.Rows(0)("Prototype") Is DBNull.Value, DBNull.Value, TC.NZ(dt.Rows(0)("Prototype"), False))
			chb_Trainer.EditValue = IIf(dt.Rows(0)("Trainer") Is DBNull.Value, DBNull.Value, TC.NZ(dt.Rows(0)("Trainer"), False))
			chb_Translation.EditValue = IIf(dt.Rows(0)("Translation") Is DBNull.Value, DBNull.Value, TC.NZ(dt.Rows(0)("Translation"), False))
			chb_Unlicensed.EditValue = IIf(dt.Rows(0)("Unlicensed") Is DBNull.Value, DBNull.Value, TC.NZ(dt.Rows(0)("Unlicensed"), False))

			'On MultiEdit an auto import is possible if the template of the category is known
			If _AutoImport AndAlso bTemplateKnown Then
				Cursor.Current = Cursors.Default
				Me.btn_OK_Click(Me.btn_OK, New System.EventArgs())
			End If
		End If

		If Not _MultiEdit Then 'Emu_Game_Edit with single Game
			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				DS_ML.Fill_src_frm_Emu_Game_Edit_Genres(tran, DS_ML.src_frm_Emu_Game_Edit_Genres, _id_Emu_Games, _id_Rombase, _MultiEdit)
				DS_ML.Fill_src_frm_Emu_Game_Edit_Attributes(tran, DS_ML.src_frm_Emu_Game_Edit_Attributes, _id_Emu_Games, _id_Rombase, _MultiEdit)
				DS_ML.Fill_tbl_Moby_Regions(tran, DS_ML.tbl_Moby_Regions)
				tran.Rollback()
			End Using

			sSQL = ""
			sSQL &= "	SELECT"
			sSQL &= "		id_Languages"
			sSQL &= "		, Language_Short"
			sSQL &= "		, Language"
			sSQL &= "		, CASE	WHEN EXISTS(SELECT 1 FROM tbl_Emu_Games_Languages EGL WHERE EGL.id_Languages = L.id_Languages AND EGL.id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games) & ") THEN 1"
			sSQL &= IIf(_id_Emu_Games = 0, " ELSE NULL END AS Used" & ControlChars.CrLf, " ELSE 0 END AS Used")
			sSQL &= "	FROM tbl_Languages L"
			DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, DS_ML.tbl_Emu_Games_Edit_Languages)

			sSQL = ""
			sSQL &= "	SELECT"
			sSQL &= "		id_Regions"
			sSQL &= "		, Region"
			sSQL &= "		, CASE WHEN EXISTS(SELECT 1 FROM tbl_Emu_Games_Regions EGR WHERE EGR.id_Regions = R.id_Regions AND EGR.id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games) & ") THEN 1 "
			sSQL &= IIf(_id_Emu_Games = 0, " ELSE NULL END AS Used" & ControlChars.CrLf, " ELSE 0 END AS Used")
			sSQL &= "	FROM tbl_Regions R ORDER BY R.Sort, R.Region"
			DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, DS_ML.tbl_Emu_Games_Edit_Regions)

			'Get Name, Prefix, Notes and Description
			sSQL = "	SELECT" &
			"		CASE WHEN EMUGAME.Name IS NULL AND EMUGAME.Name_Prefix IS NULL THEN IFNULL(MOBYGAME.Name, EMUGAME.InnerFile) ELSE EMUGAME.Name END AS Name" &
			"		, CASE WHEN EMUGAME.Name IS NULL AND EMUGAME.Name_Prefix IS NULL THEN MOBYGAME.Name_Prefix ELSE EMUGAME.Name_Prefix END AS Name_Prefix" &
			"		, EMUGAME.Note" &
			"		, EMUGAME.J2KPreset" &
			"		, IFNULL(EMUGAME.Description, MOBYGAME.Description) AS Description" &
			"		, EMUGAME.SpecialInfo" &
			"		, IFNULL(EMUGAME.Publisher, C1.Name) AS Publisher" &
			"		, IFNULL(EMUGAME.Developer, C2.Name) AS Developer" &
			"		, IFNULL(EMUGAME.Year, REL.Year) AS Year" &
			"		, IFNULL(EMUGAME.id_Emulators, (SELECT id_Emulators FROM tbl_Emulators_Moby_Platforms EMP WHERE EMP.id_Moby_Platforms = EMUGAME.id_Moby_Platforms AND EMP.DefaultEmulator = 1)) AS id_Emulators" &
			"		, Version, Alt, Trainer, Translation, Hack, Bios, Prototype, Alpha, Beta, Sample, Kiosk, Unlicensed, Fixed, Pirated, Good, Bad, Overdump" &
			"	FROM tbl_Emu_Games EMUGAME" &
			"	LEFT JOIN moby.tbl_Moby_Games MOBYGAME ON EMUGAME.Moby_Games_URLPart = MOBYGAME.URLPart" &
			"	LEFT JOIN tbl_Moby_Releases REL ON EMUGAME.id_Moby_Platforms = REL.id_Moby_Platforms AND MOBYGAME.id_Moby_Games = REL.id_Moby_Games" &
			"	LEFT JOIN tbl_Moby_Companies C1 ON REL.Publisher_id_Moby_Companies = C1.id_Moby_Companies" &
			"	LEFT JOIN tbl_Moby_Companies C2 ON REL.Developer_id_Moby_Companies = C2.id_Moby_Companies" &
			"	WHERE EMUGAME.id_Emu_Games = " & _id_Emu_Games
			Dim dt As DataTable = DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL)


			If dt Is Nothing Or dt.Rows.Count <> 1 Then
				MKDXHelper.MessageBox("There was a problem loading the data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Cursor.Current = Cursors.Default
				Me.Close()
			End If

			sSQL = ""
			sSQL &= "		SELECT"
			sSQL &= "			EMP.id_Emulators"
			sSQL &= "			, E.Displayname || CASE WHEN EMP.DefaultEmulator = 1 THEN ' (global default)' ELSE '' END AS Displayname"
			sSQL &= "			, E.J2KPreset"
			sSQL &= "		FROM"
			sSQL &= "		tbl_Emulators_Moby_Platforms EMP"
			sSQL &= "		INNER JOIN tbl_Emulators E ON EMP.id_Emulators = E.id_Emulators"
			sSQL &= "		WHERE id_Moby_Platforms = (SELECT id_Moby_Platforms FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games) & " LIMIT 1)"
			DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, sSQL, DS_ML.tbl_Emu_Games_Edit_Default_Emulator)

			If Not TC.IsNullNothingOrEmpty(dt.Rows(0)("id_Emulators")) Then
				cmb_Default_Emulator.EditValue = dt.Rows(0)("id_Emulators")
				_id_Emulators = dt.Rows(0)("id_Emulators")
			End If

			If Not TC.IsNullNothingOrEmpty(dt.Rows(0)("J2KPreset")) Then
				MKNetLib.cls_MKClientSupport.SetBindingSourcePosition(BS_J2K, "ConfigName", dt.Rows(0)("J2KPreset"))
				Try
					cmb_J2K_Config.EditValue = BS_J2K.Current("id_Config")
				Catch ex As Exception

				End Try
			End If

			_id_Emulators_Default = DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_Emulators FROM tbl_Emulators_Moby_Platforms EMP WHERE EMP.id_Moby_Platforms = (SELECT id_Moby_Platforms FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games) & ") AND EMP.DefaultEmulator = 1")

			Me.txb_Name.EditValue = dt.Rows(0)("Name")
			_Name_OldValue = dt.Rows(0)("Name")

			Me.txb_Prefix.EditValue = dt.Rows(0)("Name_Prefix")
			_Prefix_OldValue = dt.Rows(0)("Name_Prefix")

			Me.txb_Notes.EditValue = dt.Rows(0)("Note")
			_Notes_OldValue = dt.Rows(0)("Note")

			Me.txb_Description.EditValue = dt.Rows(0)("Description") 'MKNetLib.cls_MKISO_8859_1_Replace.ToASCII(System.Text.RegularExpressions.Regex.Replace(TC.NZ(dt.Rows(0)("Description"), "").Replace("<br>", ControlChars.CrLf), "<.*?>", ""), True)
			_Description_OldValue = dt.Rows(0)("Description") 'MKNetLib.cls_MKISO_8859_1_Replace.ToASCII(System.Text.RegularExpressions.Regex.Replace(TC.NZ(dt.Rows(0)("Description"), "").Replace("<br>", ControlChars.CrLf), "<.*?>", ""), True)

			Me.txb_Special_Info.EditValue = TC.NZ(dt.Rows(0)("SpecialInfo"), "")
			_Special_Info_OldValue = TC.NZ(dt.Rows(0)("SpecialInfo"), "")

			Me.txb_Publisher.EditValue = dt.Rows(0)("Publisher")
			_Publisher_OldValue = dt.Rows(0)("Publisher")

			Me.txb_Developer.EditValue = dt.Rows(0)("Developer")
			_Developer_OldValue = dt.Rows(0)("Developer")

			Me.txb_Year.EditValue = dt.Rows(0)("Year")
			_Year_OldValue = dt.Rows(0)("Year")

			Me.txb_Version.EditValue = dt.Rows(0)("Version")
			_Version_OldValue = dt.Rows(0)("Version")

			Me.txb_Alt.EditValue = dt.Rows(0)("Alt")
			_Alt_OldValue = dt.Rows(0)("Alt")

			chb_Alpha.Checked = TC.NZ(dt.Rows(0)("Alpha"), False)
			chb_Bad.Checked = TC.NZ(dt.Rows(0)("Bad"), False)
			chb_Beta.Checked = TC.NZ(dt.Rows(0)("Beta"), False)
			chb_Bios.Checked = TC.NZ(dt.Rows(0)("Bios"), False)
			chb_Sample.Checked = TC.NZ(dt.Rows(0)("Sample"), False)
			chb_Fixed.Checked = TC.NZ(dt.Rows(0)("Fixed"), False)
			chb_Good.Checked = TC.NZ(dt.Rows(0)("Good"), False)
			chb_Hack.Checked = TC.NZ(dt.Rows(0)("Hack"), False)
			chb_Kiosk.Checked = TC.NZ(dt.Rows(0)("Kiosk"), False)
			chb_Overdump.Checked = TC.NZ(dt.Rows(0)("Overdump"), False)
			chb_Pirated.Checked = TC.NZ(dt.Rows(0)("Pirated"), False)
			chb_Prototype.Checked = TC.NZ(dt.Rows(0)("Prototype"), False)
			chb_Trainer.Checked = TC.NZ(dt.Rows(0)("Trainer"), False)
			chb_Translation.Checked = TC.NZ(dt.Rows(0)("Translation"), False)
			chb_Unlicensed.Checked = TC.NZ(dt.Rows(0)("Unlicensed"), False)

			'Load DS_ML.tbl_Emu_Games Data and BTAs
			frm_Tag_Parser_Edit.Fill_Tag_Parser_Volumes(Me.DS_ML.ttb_Tag_Parser_Volumes)
			DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT id_Rombase_DOSBox_Filetypes, Displayname, ID FROM rombase.tbl_Rombase_DOSBox_Filetypes", BTA_DOSBox_Filetypes.DS.Tables(0))
			DataAccess.FireProcedureReturnDT(cls_Globals.Conn, 0, False, "SELECT id_Rombase_DOSBox_Exe_Types, Displayname, ID FROM rombase.tbl_Rombase_DOSBox_Exe_Types", BTA_DOSBox_Exe_Types.DS.Tables(0))
			For i As Integer = 0 To 25
				Dim row As DataRow = BTA_DOSBox_Mount_Destination.DS.Tables(0).NewRow
				row("value") = Chr(Asc("A") + i)
				row("Displayname") = Chr(Asc("A") + i)
				BTA_DOSBox_Mount_Destination.DS.Tables(0).Rows.Add(row)
			Next

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				DS_ML.Fill_src_frm_Rom_Manager_Emu_Games(tran, DS_ML.tbl_Emu_Games, Me._id_Moby_Platforms, Me._id_Emu_Games, Me._id_Emu_Games)
			End Using

			Me._MultiVolume = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT PLTFM.MultiVolume FROM tbl_Emu_Games EG LEFT JOIN moby.tbl_Moby_Platforms PLTFM ON EG.id_Moby_Platforms = PLTFM.id_Moby_Platforms WHERE EG.id_Emu_Games = " & TC.getSQLFormat(Me._id_Emu_Games)), False)

			Dim id_Moby_Platforms As Int64 = TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_Moby_Platforms FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(Me._id_Emu_Games)), 0L)
			Me._is_DOS_or_Booter = {cls_Globals.enm_Moby_Platforms.dos, cls_Globals.enm_Moby_Platforms.pcboot}.Contains(id_Moby_Platforms)
			Me._is_ScummVM = {cls_Globals.enm_Moby_Platforms.scummvm}.Contains(id_Moby_Platforms)

			'On DOS and PC Booter Platforms Show and initialize the DOSBox Configuration, also show Dosbox Files/Folder
			If Me._is_DOS_or_Booter Then
				tpg_DOSBox_Config.PageVisible = True
				Me.ucr_DOSBox_Config.Load_Game_Config(Me._id_Emu_Games)

				Me.tpg_DOSBox_Files_and_Directories.PageVisible = True
				BS_DOSBox_Files_and_Folders.Filter = "id_Emu_Games = " & Me._id_Emu_Games & " OR id_Emu_Games_Owner = " & Me._id_Emu_Games & " AND id_Rombase_DOSBox_Filetypes <> " & TC.getSQLFormat(cls_Globals.enm_Rombase_DOSBox_Filetypes.int)
			ElseIf Me._is_ScummVM Then
				tpg_ScummVM_Config.PageVisible = True
				Me.ucr_ScummVM_Config.Load_Game_Config(Me._id_Emu_Games)
			ElseIf _MultiVolume Then
				'In case of Multi-Volume show Disks/Volumes Tabpage
				Me.tpg_Disks_Volumes.PageVisible = True
			End If
		End If

		Me.tv_Regions.ExpandAll()

		Cursor.Current = Cursors.Default
	End Sub

	Private Sub bbi_TechInfo_Description_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_TechInfo_Show_Description.ItemClick
		Using frm As New frm_Description(BS_Attributes.Current("Attribute"), BS_Attributes.Current("Description")) 'Using frm As New frm_Description(BS_Attributes.Current("Attribute"), MKNetLib.cls_MKISO_8859_1_Replace.ToASCII(System.Text.RegularExpressions.Regex.Replace(BS_Attributes.Current("Description").Replace("<br>", ControlChars.CrLf), "<.*?>", ""), True))
			frm.ShowDialog(Me)
		End Using
	End Sub

	''' <summary>
	''' Save Game Properties for a single game. IMPORTANT: also saves a Mapping Template when in MultiEdit mode!
	''' </summary>
	''' <returns></returns>
	''' <remarks></remarks>
	Private Function Save_Single_Game() As Boolean
		Cursor.Current = Cursors.WaitCursor

		BS_Attributes.EndEdit()
		BS_Educational_Categories.EndEdit()
		BS_Genres.EndEdit()
		BS_Other_Attributes.EndEdit()
		BS_Perspectives.EndEdit()
		BS_Sports_Themes.EndEdit()
		BS_Visual_Presentation.EndEdit()
		BS_Pacing.EndEdit()
		BS_Gameplay.EndEdit()
		BS_Interface_Control.EndEdit()
		BS_Vehicular_Themes.EndEdit()
		BS_Setting.EndEdit()
		BS_Narrative_Theme_Topic.EndEdit()
		BS_DLC_Addon.EndEdit()
		BS_Special_Edition.EndEdit()

		BS_Languages.EndEdit()
		BS_Regions.EndEdit()
		BS_DefaultEmu.EndEdit()

		'### Rombase-style save ###
		If Me._is_DOS_or_Booter OrElse Me._MultiVolume Then
			BS_DOSBox_Files_and_Folders.EndEdit()
			BS_MV.EndEdit()
			BS_MV_Volume.EndEdit()
			BTA_DOSBox_Exe_Types.EndEdit()
			BTA_DOSBox_Filetypes.EndEdit()
			BTA_DOSBox_Mount_Destination.EndEdit()

			Dim bHasChanges As Boolean = False

			For Each row As DataRow In DS_ML.tbl_Emu_Games.Rows
				If row.RowState <> DataRowState.Unchanged Then
					bHasChanges = True
					Exit For
				End If
			Next

			'Dim tbl_Changes As DataTable = DS_ML.tbl_Emu_Games.GetChanges
			'If tbl_Changes IsNot Nothing AndAlso tbl_Changes.Rows.Count > 0 Then
			If bHasChanges Then
				Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
					Save_MV(True, tran) 'Save all Main Entries
					Save_MV(False, tran)  'Save all Sub Entries (volumes of the games)

					tran.Commit()
				End Using

				DS_ML.tbl_Emu_Games.AcceptChanges()
			End If
		End If

		'### Emu_Game_Edit save ###
		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction

			If _MultiEdit Then  'Save to Template
				If _id_Emu_Games = 0 AndAlso _Mapping_Identifier.Length = 0 Then
					tran.Rollback()
					Return True
				End If

				If _id_Emu_Games = 0 Then
					_id_Emu_Games = Math.Min(TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT MIN(id_Emu_Games) FROM tbl_Emu_Games", tran), 0), 0)
					_id_Emu_Games = _id_Emu_Games - 1
					DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Emu_Games (id_Emu_Games, Mapping_Identifier) VALUES (" & TC.getSQLParameter(_id_Emu_Games, _Mapping_Identifier) & ")", tran)
				End If
			End If

			Dim dt_Old As New DS_ML.src_ucr_Emulation_GamesDataTable
			DS_ML.Fill_src_ucr_Emulation_Games(tran, dt_Old, Nothing, Nothing, Nothing, _id_Emu_Games)

			Dim sSQL As String = "UPDATE tbl_Emu_Games SET "

			If Not Equals(txb_Name.EditValue, _Name_OldValue) Then sSQL &= "Name = " & IIf(txb_Name.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Name.Text)) & ", " & "Name_USR = " & IIf(txb_Name.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Name.Text)) & ", "
			If Not Equals(txb_Description.EditValue, _Description_OldValue) Then sSQL &= "Description = " & IIf(txb_Description.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Description.Text)) & ", " & "Description_USR = " & IIf(txb_Description.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Description.Text)) & ", "
			If Not Equals(txb_Special_Info.EditValue, _Special_Info_OldValue) Then sSQL &= "SpecialInfo = " & IIf(txb_Special_Info.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Special_Info.Text)) & ", " & "SpecialInfo_USR = " & IIf(txb_Special_Info.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Special_Info.Text)) & ", "
			If Not Equals(txb_Notes.EditValue, _Notes_OldValue) Then sSQL &= "Note = " & IIf(txb_Notes.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Notes.Text)) & ", " & "Note_USR = " & IIf(txb_Notes.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Notes.Text)) & ", "
			If Not Equals(txb_Prefix.EditValue, _Prefix_OldValue) Then sSQL &= "Name_Prefix = " & IIf(txb_Prefix.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Prefix.Text)) & ", " & "Name_Prefix_USR = " & IIf(txb_Prefix.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Prefix.Text)) & ", "
			If Not Equals(txb_Publisher.EditValue, _Publisher_OldValue) Then sSQL &= "Publisher = " & IIf(txb_Publisher.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Publisher.Text)) & ", " & "Publisher_USR = " & IIf(txb_Publisher.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Publisher.Text)) & ", "
			If Not Equals(txb_Developer.EditValue, _Developer_OldValue) Then sSQL &= "Developer = " & IIf(txb_Developer.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Developer.Text)) & ", " & "Developer_USR = " & IIf(txb_Developer.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Developer.Text)) & ", "
			If Not Equals(txb_Year.EditValue, _Year_OldValue) Then sSQL &= "Year = " & IIf(txb_Year.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Year.Text)) & ", " & "Year_USR = " & IIf(txb_Year.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Year.Text)) & ", "
			If Not Equals(txb_Version.EditValue, _Version_OldValue) Then sSQL &= "Version = " & IIf(txb_Version.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Version.Text)) & ", " & "Version_USR = " & IIf(txb_Version.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Version.Text)) & ", "
			If Not Equals(txb_Alt.EditValue, _Alt_OldValue) Then sSQL &= "Alt = " & IIf(txb_Alt.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Alt.Text)) & ", " & "Alt_USR = " & IIf(txb_Alt.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Alt.Text)) & ", "

			If Equals(chb_Alpha.EditValue, True) Then sSQL &= "Alpha = 1, Alpha_USR = 1, " Else If Equals(chb_Alpha.EditValue, False) Then sSQL &= "Alpha = NULL, Alpha_USR = NULL, "
			If Equals(chb_Bad.EditValue, True) Then sSQL &= "Bad = 1, Bad_USR = 1, " Else If Equals(chb_Bad.EditValue, False) Then sSQL &= "Bad = NULL, Bad_USR = NULL, "
			If Equals(chb_Beta.EditValue, True) Then sSQL &= "Beta = 1, Beta_USR = 1, " Else If Equals(chb_Beta.EditValue, False) Then sSQL &= "Beta = NULL, Beta_USR = NULL, "
			If Equals(chb_Bios.EditValue, True) Then sSQL &= "Bios = 1, Bios_USR = 1, " Else If Equals(chb_Bios.EditValue, False) Then sSQL &= "Bios = NULL, Bios_USR = NULL, "
			If Equals(chb_Sample.EditValue, True) Then sSQL &= "Sample = 1, Sample_USR = 1, " Else If Equals(chb_Sample.EditValue, False) Then sSQL &= "Sample = NULL, Sample_USR = NULL, "
			If Equals(chb_Fixed.EditValue, True) Then sSQL &= "Fixed = 1, Fixed_USR = 1, " Else If Equals(chb_Fixed.EditValue, False) Then sSQL &= "Fixed = NULL, Fixed_USR = NULL, "
			If Equals(chb_Good.EditValue, True) Then sSQL &= "Good = 1, Good_USR = 1, " Else If Equals(chb_Good.EditValue, False) Then sSQL &= "Good = NULL, Good_USR = NULL, "
			If Equals(chb_Hack.EditValue, True) Then sSQL &= "Hack = 1, Hack_USR = 1, " Else If Equals(chb_Hack.EditValue, False) Then sSQL &= "Hack = NULL, Hack_USR = NULL, "
			If Equals(chb_Kiosk.EditValue, True) Then sSQL &= "Kiosk = 1, Kiosk_USR = 1, " Else If Equals(chb_Kiosk.EditValue, False) Then sSQL &= "Kiosk = NULL, Kiosk_USR = NULL, "
			If Equals(chb_Overdump.EditValue, True) Then sSQL &= "Overdump = 1, Overdump_USR = 1, " Else If Equals(chb_Overdump.EditValue, False) Then sSQL &= "Overdump = NULL, Overdump_USR = NULL, "
			If Equals(chb_Pirated.EditValue, True) Then sSQL &= "Pirated = 1, Pirated_USR = 1, " Else If Equals(chb_Pirated.EditValue, False) Then sSQL &= "Pirated = NULL, Pirated_USR = NULL, "
			If Equals(chb_Prototype.EditValue, True) Then sSQL &= "Prototype = 1, Prototype_USR = 1, " Else If Equals(chb_Prototype.EditValue, False) Then sSQL &= "Prototype = NULL, Prototype_USR = NULL, "
			If Equals(chb_Trainer.EditValue, True) Then sSQL &= "Trainer = 1, Trainer_USR = 1, " Else If Equals(chb_Trainer.EditValue, False) Then sSQL &= "Trainer = NULL, Trainer_USR = NULL, "
			If Equals(chb_Translation.EditValue, True) Then sSQL &= "Translation = 1, Translation_USR = 1, " Else If Equals(chb_Translation.EditValue, False) Then sSQL &= "Translation = NULL, Translation_USR = NULL, "
			If Equals(chb_Unlicensed.EditValue, True) Then sSQL &= "Unlicensed = 1, Unlicensed_USR = 1, " Else If Equals(chb_Unlicensed.EditValue, False) Then sSQL &= "Unlicensed = NULL, Unlicensed_USR = NULL, "

			'J2K
			Dim j2k_preset As Object = DBNull.Value
			If cmb_J2K_Config.EditValue IsNot DBNull.Value AndAlso cmb_J2K_Config.EditValue IsNot Nothing AndAlso BS_J2K.Current IsNot Nothing Then
				BS_J2K.EndEdit()
				j2k_preset = BS_J2K.Current("ConfigName")
			End If
			If BS_DefaultEmu.Current IsNot Nothing Then
				If Equals(BS_DefaultEmu.Current("J2KPreset"), j2k_preset) Then
					j2k_preset = DBNull.Value
				End If
			End If

			sSQL &= "J2KPreset = " & TC.getSQLFormat(j2k_preset) & ", "

			Dim id_Emulators_Default As Object = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Emulators FROM tbl_Emulators_Moby_Platforms EMP WHERE EMP.id_Moby_Platforms = (SELECT id_Moby_Platforms FROM tbl_Emu_Games WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games) & " LIMIT 1) AND EMP.DefaultEmulator = 1", tran)
			If Equals(id_Emulators_Default, cmb_Default_Emulator.EditValue) Then
				sSQL &= "id_Emulators = NULL, "
			Else
				sSQL &= "id_Emulators = " & TC.getSQLFormat(cmb_Default_Emulator.EditValue) & ", "
			End If

			DataAccess.FireProcedure(tran.Connection, 0, sSQL.Substring(0, sSQL.Length - 2) & " WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games), tran)

			Dim id_Moby_Games As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT GAME.id_Moby_Games FROM tbl_Emu_Games EMUGAME LEFT JOIN moby.tbl_Moby_Games GAME ON EMUGAME.Moby_Games_URLPart = GAME.URLPart WHERE EMUGAME.id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games), tran), 0)
			Dim id_Moby_Releases As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT REL.id_Moby_Releases FROM tbl_Emu_Games EMUGAME LEFT JOIN moby.tbl_Moby_Games GAME ON EMUGAME.Moby_Games_URLPart = GAME.URLPart LEFT JOIN moby.tbl_Moby_Releases REL ON EMUGAME.id_Moby_Platforms = REL.id_Moby_Platforms AND GAME.id_Moby_Games = REL.id_Moby_Games WHERE EMUGAME.id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games), tran), 0)

			Dim dt_Genres As DataTable = DS_ML.src_frm_Emu_Game_Edit_Genres.GetChanges
			If dt_Genres IsNot Nothing AndAlso dt_Genres.Rows.Count > 0 Then

				For Each row As DataRow In dt_Genres.Rows
					Dim id_Moby_Genres = row("id_Moby_Genres")
					Dim b_Used As Object = row("Used")
					Dim id_Moby_Games_Genres As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Games_Genres FROM moby.tbl_Moby_Games_Genres WHERE id_Moby_Games = " & TC.getSQLFormat(id_Moby_Games) & " AND id_Moby_Genres = " & TC.getSQLFormat(row("id_Moby_Genres")), tran), 0)

					'TODO: MultiEdit - properly handling
					If Not TC.NZ(b_Used, False) Then
						If id_Moby_Games_Genres <> 0 Then
							'already defined as moby_game_genre, upsert as Used = 0 to tbl_Emu_Games_Moby_Genres
							DS_ML.Upsert_tbl_Emu_Games_Moby_Genres(tran, _id_Emu_Games, id_Moby_Genres, False)
						Else
							'not defined as moby_game_genre, delete all occurences in tbl_Emu_Games_Moby_Genres
							DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Moby_Genres WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games) & " AND id_Moby_Genres = " & TC.getSQLFormat(id_Moby_Genres), tran)
						End If
					Else
						If id_Moby_Games_Genres <> 0 Then
							'already defined as moby_game_genre, delete all occurences in tbl_Emu_Games_Moby_Genres
							DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Moby_Genres WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games) & " AND id_Moby_Genres = " & TC.getSQLFormat(id_Moby_Genres), tran)
						Else
							'not defined as moby_game_genre, add as Used = 1 to tbl_Emu_Games_Moby_Genres
							DS_ML.Upsert_tbl_Emu_Games_Moby_Genres(tran, _id_Emu_Games, id_Moby_Genres, True)
						End If
					End If
				Next

			End If

			Dim dt_Attributes As DataTable = DS_ML.src_frm_Emu_Game_Edit_Attributes.GetChanges
			If dt_Attributes IsNot Nothing AndAlso dt_Attributes.Rows.Count > 0 Then

				For Each row As DataRow In dt_Attributes.Rows
					Dim id_Moby_Attributes = row("id_Moby_Attributes")
					Dim b_Used As Object = row("Used")
					Dim id_Moby_Releases_Attributes As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Releases_Attributes FROM moby.tbl_Moby_Releases_Attributes WHERE id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases) & " AND id_Moby_Attributes = " & TC.getSQLFormat(row("id_Moby_Attributes")), tran), 0)

					'TODO: MultiEdit - properly handling
					If Not TC.NZ(b_Used, False) Then
						If id_Moby_Releases_Attributes <> 0 Then
							'already defined as moby_releases_attributes, upsert as Used = 0 to tbl_Emu_Games_Moby_Attributes
							DS_ML.Upsert_tbl_Emu_Games_Moby_Attributes(tran, _id_Emu_Games, id_Moby_Attributes, False)
						Else
							'not defined as moby_game_genre, delete all occurences in tbl_Emu_Games_Moby_Attributes
							DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Moby_Attributes WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games) & " AND id_Moby_Attributes = " & TC.getSQLFormat(id_Moby_Attributes), tran)
						End If
					Else
						If id_Moby_Releases_Attributes <> 0 Then
							'already defined as moby_releases_attributes, delete all occurences in tbl_Emu_Games_Moby_Attributes
							DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Moby_Attributes WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games) & " AND id_Moby_Attributes = " & TC.getSQLFormat(id_Moby_Attributes), tran)
						Else
							'not defined as moby_releases_attributes, add as Used = 1 to tbl_Emu_Games_Moby_Attributes
							DS_ML.Upsert_tbl_Emu_Games_Moby_Attributes(tran, _id_Emu_Games, id_Moby_Attributes, True)
						End If
					End If
				Next

			End If

			If DS_ML.tbl_Emu_Games_Edit_Languages.GetChanges IsNot Nothing Then
				DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Languages WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games), tran)

				For Each row As DataRow In DS_ML.tbl_Emu_Games_Edit_Languages.Select("Used = 1")
					DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Emu_Games_Languages (id_Emu_Games, id_Languages, USR) VALUES (" & TC.getSQLParameter(_id_Emu_Games, row("id_Languages"), True) & ")", tran)
				Next
			End If

			If DS_ML.tbl_Emu_Games_Edit_Regions.GetChanges IsNot Nothing Then
				DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Regions WHERE id_Emu_Games = " & TC.getSQLFormat(_id_Emu_Games), tran)

				For Each row As DataRow In DS_ML.tbl_Emu_Games_Edit_Regions.Select("Used = 1")
					DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Emu_Games_Regions (id_Emu_Games, id_Regions, USR) VALUES (" & TC.getSQLParameter(_id_Emu_Games, row("id_Regions"), True) & ")", tran)
				Next
			End If

			Dim dt_New As New DS_ML.src_ucr_Emulation_GamesDataTable
			DS_ML.Fill_src_ucr_Emulation_Games(tran, dt_New, Nothing, Nothing, Nothing, _id_Emu_Games)

			'Extras could need renaming - currently only in the case of Windows Games (id_Moby_Platforms = 3)
			If Not _MultiEdit AndAlso TC.NZ(dt_New.Rows(0)("id_Moby_Platforms"), 0) = cls_Globals.enm_Moby_Platforms.win Then
				If dt_Old.Rows.Count = 1 Then
					Dim s_Old_FileName As String = ""
					If TC.NZ(dt_Old.Rows(0)("InnerFile"), "") = "" Then
						s_Old_FileName = dt_Old.Rows(0)("InnerFile")
					Else
						s_Old_FileName = dt_Old.Rows(0)("File")
					End If

					Dim s_New_FileName As String = ""
					If dt_New.Rows(0)("InnerFile").Length > 0 Then
						s_New_FileName = dt_New.Rows(0)("InnerFile")
					Else
						s_New_FileName = dt_New.Rows(0)("File")
					End If

					Dim al_Old_Extras As ArrayList = cls_Extras.FindAllExtras(dt_Old.Rows(0)("Platform_Short"), dt_Old.Rows(0)("id_Moby_Platforms"), dt_Old.Rows(0)("Game"), s_Old_FileName)
					Dim al_New_Extras As ArrayList = cls_Extras.FindAllExtras(dt_New.Rows(0)("Platform_Short"), dt_New.Rows(0)("id_Moby_Platforms"), dt_New.Rows(0)("Game"), s_New_FileName)

					If al_Old_Extras.Count > 0 AndAlso Not cls_Extras.ExtrasListsEqual(al_Old_Extras, al_New_Extras) Then

						Dim res As DialogResult = MKDXHelper.MessageBox("Your alterations affect the filenames of one or more extras (title, snapshots etc.). Do you want to automatically rename these extras?", "Extras need renaming", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

						If res = DialogResult.Cancel Then
							tran.Rollback()
							Return False
						End If

						If res = DialogResult.Yes Then
							'Rename all extras in al_Old_Extras
							For Each extra As cls_Extras.cls_Extras_Result In al_Old_Extras
								Dim oldpath As String = extra._Path
								Dim newfilename As String = cls_Extras.FindNextFreeExtraFilename(dt_New.Rows(0)("Platform_Short"), extra._ExtraType, cls_Extras.GetExtraFilename(dt_New.Rows(0)("Game"), s_New_FileName))

								If newfilename <> "" Then
									Alphaleonis.Win32.Filesystem.File.Move(oldpath, cls_Globals.Dir_Extras & "\emulation\" & dt_New.Rows(0)("Platform_Short") & "\" & extra._ExtraType & "\" & newfilename & Alphaleonis.Win32.Filesystem.Path.GetExtension(extra._Path))
								End If
							Next
						End If
					End If
				End If
			End If

			DS_ML.Update_tbl_Emu_Games_Caches(tran, _id_Emu_Games)

			If tpg_DOSBox_Config.PageVisible = True Then
				Me.ucr_DOSBox_Config.Save_Game_Config(tran)
			End If

			If tpg_ScummVM_Config.PageVisible = True Then
				Me.ucr_ScummVM_Config.Save_Game_Config(tran)
			End If

			tran.Commit()
		End Using

		Cursor.Current = Cursors.Default

		Return True
	End Function

	Private Function Save_Multiple_Games() As Boolean
		Cursor.Current = Cursors.WaitCursor
		Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Nothing), 400, 100, ProgressBarStyle.Continuous, False, "Saving Game {0} of {1} ...", 0, _id_Emu_Games_Multi.Length, False)

		prg.Start()

		BS_Attributes.EndEdit()
		BS_Educational_Categories.EndEdit()
		BS_Genres.EndEdit()
		BS_Other_Attributes.EndEdit()
		BS_Perspectives.EndEdit()
		BS_Sports_Themes.EndEdit()
		BS_Visual_Presentation.EndEdit()
		BS_Pacing.EndEdit()
		BS_Gameplay.EndEdit()
		BS_Interface_Control.EndEdit()
		BS_Vehicular_Themes.EndEdit()
		BS_Setting.EndEdit()
		BS_Narrative_Theme_Topic.EndEdit()
		BS_DLC_Addon.EndEdit()
		BS_Special_Edition.EndEdit()

		BS_Languages.EndEdit()
		BS_Regions.EndEdit()
		BS_DefaultEmu.EndEdit()

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction

			For Each id_Emu_Games As Integer In _id_Emu_Games_Multi
				prg.IncreaseCurrentValue()

				Dim dt_Old As New DS_ML.src_ucr_Emulation_GamesDataTable
				DS_ML.Fill_src_ucr_Emulation_Games(tran, dt_Old, Nothing, Nothing, Nothing, id_Emu_Games)

				Dim sSQL As String = ""

				If txb_Name.Text.Length > 0 AndAlso Not Equals(txb_Name.EditValue, dt_Old.Rows(0)("Game")) Then sSQL &= "Name = " & IIf(txb_Name.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Name.Text)) & ", " & "Name_USR = " & IIf(txb_Name.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Name.Text)) & ", "
				If txb_Description.Text.Length > 0 AndAlso Not Equals(txb_Description.EditValue, dt_Old.Rows(0)("Description")) Then sSQL &= "Description = " & IIf(txb_Description.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Description.Text)) & ", " & "Description_USR = " & IIf(txb_Description.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Description.Text)) & ", "
				If txb_Special_Info.Text.Length > 0 AndAlso Not Equals(txb_Special_Info.EditValue, dt_Old.Rows(0)("SpecialInfo")) Then sSQL &= "SpecialInfo = " & IIf(txb_Special_Info.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Special_Info.Text)) & ", " & "SpecialInfo_USR = " & IIf(txb_Special_Info.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Special_Info.Text)) & ", "
				If txb_Notes.Text.Length > 0 AndAlso Not Equals(txb_Notes.EditValue, dt_Old.Rows(0)("Note")) Then sSQL &= "Note = " & IIf(txb_Notes.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Notes.Text)) & ", " & "Note_USR = " & IIf(txb_Notes.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Notes.Text)) & ", "
				If txb_Prefix.Text.Length > 0 AndAlso Not Equals(txb_Prefix.EditValue, dt_Old.Rows(0)("Name_Prefix")) Then sSQL &= "Name_Prefix = " & IIf(txb_Prefix.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Prefix.Text)) & ", " & "Name_Prefix_USR = " & IIf(txb_Prefix.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Prefix.Text)) & ", "
				If txb_Publisher.Text.Length > 0 AndAlso Not Equals(txb_Publisher.EditValue, dt_Old.Rows(0)("Publisher")) Then sSQL &= "Publisher = " & IIf(txb_Publisher.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Publisher.Text)) & ", " & "Publisher_USR = " & IIf(txb_Publisher.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Publisher.Text)) & ", "
				If txb_Developer.Text.Length > 0 AndAlso Not Equals(txb_Developer.EditValue, dt_Old.Rows(0)("Developer")) Then sSQL &= "Developer = " & IIf(txb_Developer.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Developer.Text)) & ", " & "Developer_USR = " & IIf(txb_Developer.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Developer.Text)) & ", "
				If txb_Year.Text.Length > 0 AndAlso Not Equals(txb_Year.EditValue, dt_Old.Rows(0)("Year")) Then sSQL &= "Year = " & IIf(txb_Year.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Year.Text)) & ", " & "Year_USR = " & IIf(txb_Year.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Year.Text)) & ", "
				If txb_Version.Text.Length > 0 AndAlso Not Equals(txb_Version.EditValue, dt_Old.Rows(0)("Version")) Then sSQL &= "Version = " & IIf(txb_Version.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Version.Text)) & ", " & "Version_USR = " & IIf(txb_Version.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Version.Text)) & ", "
				If txb_Alt.Text.Length > 0 AndAlso Not Equals(txb_Alt.EditValue, dt_Old.Rows(0)("Alt")) Then sSQL &= "Alt = " & IIf(txb_Alt.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Alt.Text)) & ", " & "Alt_USR = " & IIf(txb_Alt.Text.Length = 0, "NULL", TC.getSQLFormat(txb_Alt.Text)) & ", "

				If chb_Alpha.CheckState <> CheckState.Indeterminate Then If chb_Alpha.Checked Then sSQL &= "Alpha = 1, Alpha_USR = 1, " Else sSQL &= "Alpha = NULL, Alpha_USR = NULL, "
				If chb_Bad.CheckState <> CheckState.Indeterminate Then If chb_Bad.Checked Then sSQL &= "Bad = 1, Bad_USR = 1, " Else sSQL &= "Bad = NULL, Bad_USR = NULL, "
				If chb_Beta.CheckState <> CheckState.Indeterminate Then If chb_Beta.Checked Then sSQL &= "Beta = 1, Beta_USR = 1, " Else sSQL &= "Beta = NULL, Beta_USR = NULL, "
				If chb_Bios.CheckState <> CheckState.Indeterminate Then If chb_Bios.Checked Then sSQL &= "Bios = 1, Bios_USR = 1, " Else sSQL &= "Bios = NULL, Bios_USR = NULL, "
				If chb_Sample.CheckState <> CheckState.Indeterminate Then If chb_Sample.Checked Then sSQL &= "Sample = 1, Sample_USR = 1, " Else sSQL &= "Sample = NULL, Sample_USR = NULL, "
				If chb_Fixed.CheckState <> CheckState.Indeterminate Then If chb_Fixed.Checked Then sSQL &= "Fixed = 1, Fixed_USR = 1, " Else sSQL &= "Fixed = NULL, Fixed_USR = NULL, "
				If chb_Good.CheckState <> CheckState.Indeterminate Then If chb_Good.Checked Then sSQL &= "Good = 1, Good_USR = 1, " Else sSQL &= "Good = NULL, Good_USR = NULL, "
				If chb_Hack.CheckState <> CheckState.Indeterminate Then If chb_Hack.Checked Then sSQL &= "Hack = 1, Hack_USR = 1, " Else sSQL &= "Hack = NULL, Hack_USR = NULL, "
				If chb_Kiosk.CheckState <> CheckState.Indeterminate Then If chb_Kiosk.Checked Then sSQL &= "Kiosk = 1, Kiosk_USR = 1, " Else sSQL &= "Kiosk = NULL, Kiosk_USR = NULL, "
				If chb_Overdump.CheckState <> CheckState.Indeterminate Then If chb_Overdump.Checked Then sSQL &= "Overdump = 1, Overdump_USR = 1, " Else sSQL &= "Overdump = NULL, Overdump_USR = NULL, "
				If chb_Pirated.CheckState <> CheckState.Indeterminate Then If chb_Pirated.Checked Then sSQL &= "Pirated = 1, Pirated_USR = 1, " Else sSQL &= "Pirated = NULL, Pirated_USR = NULL, "
				If chb_Prototype.CheckState <> CheckState.Indeterminate Then If chb_Prototype.Checked Then sSQL &= "Prototype = 1, Prototype_USR = 1, " Else sSQL &= "Prototype = NULL, Prototype_USR = NULL, "
				If chb_Trainer.CheckState <> CheckState.Indeterminate Then If chb_Trainer.Checked Then sSQL &= "Trainer = 1, Trainer_USR = 1, " Else sSQL &= "Trainer = NULL, Trainer_USR = NULL, "
				If chb_Translation.CheckState <> CheckState.Indeterminate Then If chb_Translation.Checked Then sSQL &= "Translation = 1, Translation_USR = 1, " Else sSQL &= "Translation = NULL, Translation_USR = NULL, "
				If chb_Unlicensed.CheckState <> CheckState.Indeterminate Then If chb_Unlicensed.Checked Then sSQL &= "Unlicensed = 1, Unlicensed_USR = 1, " Else sSQL &= "Unlicensed = NULL, Unlicensed_USR = NULL, "

				'J2K
				Dim j2k_preset As Object = DBNull.Value

				If cmb_J2K_Config.EditValue IsNot DBNull.Value AndAlso cmb_J2K_Config.EditValue IsNot Nothing Then
					'In Multiedit you have to select something, else it is <do not change>
					BS_J2K.EndEdit()
					j2k_preset = BS_J2K.Current("ConfigName")

					If cmb_Default_Emulator.EditValue IsNot Nothing AndAlso cmb_Default_Emulator.EditValue IsNot DBNull.Value Then
						If BS_DefaultEmu.Current IsNot Nothing Then
							If Equals(BS_DefaultEmu.Current("J2KPreset"), j2k_preset) Then
								j2k_preset = DBNull.Value
							End If
						End If
					Else
						'Get the J2K Preset from the Emulator
						Dim j2k_preset_emu As Object = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT EMU.J2KPreset FROM tbl_Emu_Games EG INNER JOIN tbl_Emulators EMU ON EG.id_Emulators = EMU.id_Emulators WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), tran)

						If j2k_preset_emu Is Nothing AndAlso TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT EG.id_Emulators FROM tbl_Emu_Games EG WHERE Eg.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), tran), 0) = 0 Then
							'Get the J2K Preset from the global default Emulator
							j2k_preset_emu = DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT E.J2KPreset FROM tbl_Emulators_Moby_Platforms EMP INNER JOIN tbl_Emulators E ON EMP.id_Emulators = E.id_Emulators WHERE id_Moby_Platforms = " & TC.getSQLFormat(_id_Moby_Platforms) & " AND EMP.DefaultEmulator = 1", tran)
						End If

						If Equals(j2k_preset_emu, j2k_preset) Then
							j2k_preset = DBNull.Value
						End If
					End If

					sSQL &= "J2KPreset = " & TC.getSQLFormat(j2k_preset) & ", "
				End If

				If _id_Moby_Platforms > 0L Then
					If TC.NZ(cmb_Default_Emulator.EditValue, 0) = -1 Then 'Remove all default emulators
						sSQL &= "id_Emulators = NULL, "
					ElseIf TC.NZ(cmb_Default_Emulator.EditValue, 0) > 0 Then
						sSQL &= "id_Emulators = " & TC.getSQLFormat(cmb_Default_Emulator.EditValue) & ", "
					End If
				End If

				If sSQL.Length > 0 Then
					sSQL = "UPDATE tbl_Emu_Games SET " & sSQL.Substring(0, sSQL.Length - 2) & " WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games)
					DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
				End If

				Dim id_Moby_Games As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT GAME.id_Moby_Games FROM tbl_Emu_Games EMUGAME LEFT JOIN moby.tbl_Moby_Games GAME ON EMUGAME.Moby_Games_URLPart = GAME.URLPart WHERE EMUGAME.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), tran), 0)
				Dim id_Moby_Releases As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT REL.id_Moby_Releases FROM tbl_Emu_Games EMUGAME LEFT JOIN moby.tbl_Moby_Games GAME ON EMUGAME.Moby_Games_URLPart = GAME.URLPart LEFT JOIN moby.tbl_Moby_Releases REL ON EMUGAME.id_Moby_Platforms = REL.id_Moby_Platforms AND GAME.id_Moby_Games = REL.id_Moby_Games WHERE EMUGAME.id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games), tran), 0)

				'TODO: GetChanges doesn't suffice - everything either "false" or "true" have to be taken care off

				'Dim dt_Genres As DataTable = DS_ML.src_frm_Emu_Game_Edit_Genres.GetChanges
				'If dt_Genres IsNot Nothing AndAlso dt_Genres.Rows.Count > 0 Then

				For Each row As DataRow In DS_ML.src_frm_Emu_Game_Edit_Genres.Select("Used = 1 OR Used = 0")  '0 = Remove; 1 = Add; Null = Do Nothing
					If row("Used") IsNot DBNull.Value AndAlso row("Used") IsNot Nothing Then
						Dim id_Moby_Genres = row("id_Moby_Genres")
						Dim b_Used As Boolean = row("Used")
						Dim id_Moby_Games_Genres As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Games_Genres FROM moby.tbl_Moby_Games_Genres WHERE id_Moby_Games = " & TC.getSQLFormat(id_Moby_Games) & " AND id_Moby_Genres = " & TC.getSQLFormat(row("id_Moby_Genres")), tran), 0)

						'tbl_Emu_Games_Moby_Genres
						'tbl_Moby_Games_Genres
						If Not b_Used Then
							If id_Moby_Games_Genres <> 0 Then
								'already defined as moby_game_genre, upsert as Used = 0 to tbl_Emu_Games_Moby_Genres
								DS_ML.Upsert_tbl_Emu_Games_Moby_Genres(tran, id_Emu_Games, id_Moby_Genres, False)
							Else
								'not defined as moby_game_genre, delete all occurences in tbl_Emu_Games_Moby_Genres
								DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Moby_Genres WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND id_Moby_Genres = " & TC.getSQLFormat(id_Moby_Genres), tran)
							End If
						Else
							If id_Moby_Games_Genres <> 0 Then
								'already defined as moby_game_genre, delete all occurences in tbl_Emu_Games_Moby_Genres
								DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Moby_Genres WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND id_Moby_Genres = " & TC.getSQLFormat(id_Moby_Genres), tran)
							Else
								'not defined as moby_game_genre, add as Used = 1 to tbl_Emu_Games_Moby_Genres
								DS_ML.Upsert_tbl_Emu_Games_Moby_Genres(tran, id_Emu_Games, id_Moby_Genres, True)
							End If
						End If
					End If
				Next

				'End If

				'Dim dt_Attributes As DataTable = DS_ML.src_frm_Emu_Game_Edit_Attributes.GetChanges
				'If dt_Attributes IsNot Nothing AndAlso dt_Attributes.Rows.Count > 0 Then

				For Each row As DataRow In DS_ML.src_frm_Emu_Game_Edit_Attributes.Select("Used = 1 OR Used = 0")
					If row("Used") IsNot DBNull.Value AndAlso row("Used") IsNot Nothing Then
						Dim id_Moby_Attributes = row("id_Moby_Attributes")
						Dim b_Used As Boolean = row("Used")
						Dim id_Moby_Releases_Attributes As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Moby_Releases_Attributes FROM moby.tbl_Moby_Releases_Attributes WHERE id_Moby_Releases = " & TC.getSQLFormat(id_Moby_Releases) & " AND id_Moby_Attributes = " & TC.getSQLFormat(row("id_Moby_Attributes")), tran), 0)

						If Not b_Used Then
							If id_Moby_Releases_Attributes <> 0 Then
								'already defined as moby_releases_attributes, upsert as Used = 0 to tbl_Emu_Games_Moby_Attributes
								DS_ML.Upsert_tbl_Emu_Games_Moby_Attributes(tran, id_Emu_Games, id_Moby_Attributes, False)
							Else
								'not defined as moby_game_genre, delete all occurences in tbl_Emu_Games_Moby_Attributes
								DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Moby_Attributes WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND id_Moby_Attributes = " & TC.getSQLFormat(id_Moby_Attributes), tran)
							End If
						Else
							If id_Moby_Releases_Attributes <> 0 Then
								'already defined as moby_releases_attributes, delete all occurences in tbl_Emu_Games_Moby_Attributes
								DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Moby_Attributes WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND id_Moby_Attributes = " & TC.getSQLFormat(id_Moby_Attributes), tran)
							Else
								'not defined as moby_releases_attributes, add as Used = 1 to tbl_Emu_Games_Moby_Attributes
								DS_ML.Upsert_tbl_Emu_Games_Moby_Attributes(tran, id_Emu_Games, id_Moby_Attributes, True)
							End If
						End If
					End If

				Next

				'End If

				'If DS_ML.tbl_Emu_Games_Edit_Languages.GetChanges IsNot Nothing Then
				For Each row As DataRow In DS_ML.tbl_Emu_Games_Edit_Languages.Select("Used = 0")
					DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Languages WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND id_Languages = " & TC.getSQLFormat(row("id_Languages")), tran)
				Next

				For Each row As DataRow In DS_ML.tbl_Emu_Games_Edit_Languages.Select("Used = 1")
					DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Languages WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND id_Languages = " & TC.getSQLFormat(row("id_Languages")), tran)
					DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Emu_Games_Languages (id_Emu_Games, id_Languages, USR) VALUES (" & TC.getSQLParameter(id_Emu_Games, row("id_Languages"), True) & ")", tran)
				Next
				'End If

				'If DS_ML.tbl_Emu_Games_Edit_Regions.GetChanges IsNot Nothing Then
				For Each row As DataRow In DS_ML.tbl_Emu_Games_Edit_Regions.Select("Used = 0")
					DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Regions WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND id_Regions = " & TC.getSQLFormat(row("id_Regions")), tran)
				Next

				For Each row As DataRow In DS_ML.tbl_Emu_Games_Edit_Regions.Select("Used = 1")
					DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Emu_Games_Regions WHERE id_Emu_Games = " & TC.getSQLFormat(id_Emu_Games) & " AND id_Regions = " & TC.getSQLFormat(row("id_Regions")), tran)
					DataAccess.FireProcedure(tran.Connection, 0, "INSERT INTO tbl_Emu_Games_Regions (id_Emu_Games, id_Regions, USR) VALUES (" & TC.getSQLParameter(id_Emu_Games, row("id_Regions"), True) & ")", tran)
				Next
				'End If

				Dim dt_New As New DS_ML.src_ucr_Emulation_GamesDataTable
				DS_ML.Fill_src_ucr_Emulation_Games(tran, dt_New, Nothing, Nothing, Nothing, id_Emu_Games)

				'Extras could need renaming - currently only in the case of Windows Games (id_Moby_Platforms = 3)
				If TC.NZ(dt_New.Rows(0)("id_Moby_Platforms"), 0) = cls_Globals.enm_Moby_Platforms.win Then
					If dt_Old.Rows.Count = 1 Then
						Dim al_Old_Extras As ArrayList = cls_Extras.FindAllExtras(dt_Old.Rows(0)("Platform_Short"), dt_Old.Rows(0)("id_Moby_Platforms"), dt_Old.Rows(0)("Game"), dt_Old.Rows(0)("InnerFile"))
						Dim al_New_Extras As ArrayList = cls_Extras.FindAllExtras(dt_New.Rows(0)("Platform_Short"), dt_New.Rows(0)("id_Moby_Platforms"), dt_New.Rows(0)("Game"), dt_New.Rows(0)("InnerFile"))

						If al_Old_Extras.Count > 0 AndAlso Not cls_Extras.ExtrasListsEqual(al_Old_Extras, al_New_Extras) Then
							prg.Hide = True

							Dim res As DialogResult = MKDXHelper.MessageBox("Your alterations for " & dt_Old.Rows(0)("Name") & " affect the filenames of one or more extras (title, snapshots etc.). Do you want to automatically rename these extras?", "Extras need renaming", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

							prg.Hide = False

							If res = DialogResult.Cancel Then
								tran.Rollback()
								Return False
							End If

							If res = DialogResult.Yes Then
								'Rename all extras in al_Old_Extras
								For Each extra As cls_Extras.cls_Extras_Result In al_Old_Extras
									Dim oldpath As String = extra._Path
									Dim newfilename As String = cls_Extras.FindNextFreeExtraFilename(dt_New.Rows(0)("Platform_Short"), extra._ExtraType, cls_Extras.GetExtraFilename(dt_New.Rows(0)("Game"), dt_New.Rows(0)("InnerFile")))

									If newfilename <> "" Then
										Alphaleonis.Win32.Filesystem.File.Move(oldpath, cls_Globals.Dir_Extras & "\emulation\" & dt_New.Rows(0)("Platform_Short") & "\" & extra._ExtraType & "\" & newfilename & Alphaleonis.Win32.Filesystem.Path.GetExtension(extra._Path))
									End If
								Next
							End If
						End If
					End If
				End If

				DS_ML.Update_tbl_Emu_Games_Caches(tran, id_Emu_Games)
			Next  'id_Emu_Games

			tran.Commit()

			prg.Close()
			Cursor.Current = Cursors.Default

		End Using

		Return True
	End Function

	Private Function Save() As Boolean
		If Not _MultiEdit Then
			Return Save_Single_Game()
		Else
			Save_Single_Game()  'Saves the Template
			Return Save_Multiple_Games()
		End If
	End Function

	Private Sub btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OK.Click
		If Save() Then
			Me.DialogResult = Windows.Forms.DialogResult.OK
			Me.Close()
		End If
	End Sub

	Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
		Me.DialogResult = Windows.Forms.DialogResult.Cancel
		Me.Close()
	End Sub

	Private Sub cmb_Default_Emulator_Properties_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_Default_Emulator.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
			Me.cmb_Default_Emulator.EditValue = _id_Emulators_Default
		End If
	End Sub

	'TechInfo Shortcuts:
	'Team: 620
	'Co-Op: 619
	'Same/Splitscreen: 156
	'FFA: 618
	'Bots: 621

	Private Sub Apply_TechInfo(ByVal id_Moby_Attributes As Integer, Optional ByVal value As Boolean = True)
		Dim rows() As DataRow = Me.DS_ML.src_frm_Emu_Game_Edit_Attributes.Select("id_Moby_Attributes = " & TC.getSQLFormat(id_Moby_Attributes))
		If rows.Length = 1 Then
			rows(0)("Used") = value
		End If
	End Sub

	Private Sub mni_TechInfo_Shortcut_SportsMP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_TechInfo_Shortcut_Sports_MP.ItemClick
		For Each id_Moby_Attributes As Integer In {618, 620, 621, 156}
			Apply_TechInfo(id_Moby_Attributes)
		Next
	End Sub

	Private Sub mni_TechInfo_Shortcut_CoOp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_TechInfo_Shortcut_CoOp_MP.ItemClick
		For Each id_Moby_Attributes As Integer In {619, 156}
			Apply_TechInfo(id_Moby_Attributes)
		Next
	End Sub

	Private Sub mni_TechInfo_Shortcut_VS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_TechInfo_Shortcut_VS_Fighting.ItemClick
		For Each id_Moby_Attributes As Integer In {618, 156}
			Apply_TechInfo(id_Moby_Attributes)
		Next
	End Sub

	Private Sub bbi_Techinfo_Remove_Value_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_TechInfo_Remove_Value.ItemClick
		If BS_Attributes.Current IsNot Nothing Then
			BS_Attributes.Current("Used") = DBNull.Value
			Me.grd_Attributes.RefreshDataSource()
		End If
	End Sub

	Private Sub bbi_Remove_Value_Languages_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Languages_Remove_Value.ItemClick
		If BS_Languages.Current IsNot Nothing Then
			BS_Languages.Current("Used") = DBNull.Value
			Me.grd_Languages.RefreshDataSource()
		End If
	End Sub

	Private Sub bbi_Remove_Value_Regions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Regions_Remove_Value.ItemClick
		If BS_Regions.Current IsNot Nothing Then
			BS_Regions.Current("Used") = DBNull.Value
			Me.grd_Regions.RefreshDataSource()
		End If
	End Sub

	Private Sub cmb_J2K_Config_ButtonPressed(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_J2K_Config.ButtonPressed
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
			If BS_DefaultEmu.Current Is Nothing Then
				cmb_J2K_Config.EditValue = DBNull.Value
			Else
				'Set the Config from Emulator
				If BS_DefaultEmu.Current("J2KPreset") Is DBNull.Value Then
					cmb_J2K_Config.EditValue = DBNull.Value
				Else
					Dim j2k_config As String = BS_DefaultEmu.Current("J2KPreset")

					MKNetLib.cls_MKClientSupport.SetBindingSourcePosition(BS_J2K, "ConfigName", j2k_config)
					Try
						cmb_J2K_Config.EditValue = BS_J2K.Current("id_Config")
					Catch ex As Exception

					End Try
				End If
			End If
		End If
	End Sub

	Private Sub BS_DefaultEmu_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BS_DefaultEmu.CurrentChanged
		If BS_DefaultEmu.Current IsNot Nothing Then
			'J2K
			If BS_DefaultEmu.Current("J2KPreset") Is DBNull.Value Then
				cmb_J2K_Config.EditValue = DBNull.Value
				'_J2KPreset_Original = DBNull.Value
			Else
				'Set the Config
				Dim j2k_config As String = BS_DefaultEmu.Current("J2KPreset")
				'_J2KPreset_Original = j2k_config

				MKNetLib.cls_MKClientSupport.SetBindingSourcePosition(BS_J2K, "ConfigName", j2k_config)
				Try
					cmb_J2K_Config.EditValue = BS_J2K.Current("id_Config")
				Catch ex As Exception

				End Try
			End If
		End If
	End Sub

	Private Sub gv_DOSBox_Files_and_Folders_CustomColumnDisplayText(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles gv_DOSBox_Files_and_Folders.CustomColumnDisplayText
		If e.Column Is col_DOSBox_Displayname Then
			'Dim row As DataRow = gv_DOSBox_Files_and_Folders.GetRow(e.ListSourceRowIndex).Row
			Dim oInnerFile As Object = gv_DOSBox_Files_and_Folders.GetListSourceRowCellValue(e.ListSourceRowIndex, "InnerFile")
			Dim oFolder As Object = gv_DOSBox_Files_and_Folders.GetListSourceRowCellValue(e.ListSourceRowIndex, "Folder")

			If TC.NZ(oInnerFile, "").Length > 0 Then
				e.DisplayText = oInnerFile
			Else
				e.DisplayText = oFolder
			End If
		End If

		If e.Column Is colid_Rombase_DOSBox_Filetypes Then
			'Dim row As DataRow = gv_DOSBox_Files_and_Folders.GetRow(e.ListSourceRowIndex).Row
			Dim o_id_Rombase_DOSBox_Filetypes As Object = gv_DOSBox_Files_and_Folders.GetListSourceRowCellValue(e.ListSourceRowIndex, "id_Rombase_DOSBox_Filetypes")
			Dim o_id_Rombase_DOSBox_Exe_Types As Object = gv_DOSBox_Files_and_Folders.GetListSourceRowCellValue(e.ListSourceRowIndex, "id_Rombase_DOSBox_Exe_Types")

			If TC.NZ(o_id_Rombase_DOSBox_Filetypes, 0) <> 0 Then
				Dim part1 As String = ""
				Dim part2 As String = ""

				Dim rows_Filetype As DataRow() = BTA_DOSBox_Filetypes.DS.Tables(0).Select("id_Rombase_DOSBox_Filetypes = " & TC.getSQLFormat(o_id_Rombase_DOSBox_Filetypes))
				If rows_Filetype.Length = 1 Then part1 = rows_Filetype(0)("Displayname")

				If TC.NZ(o_id_Rombase_DOSBox_Filetypes, 0) = cls_Globals.enm_Rombase_DOSBox_Filetypes.exe Then  'Executable
					Dim rows_Exe_Type As DataRow() = BTA_DOSBox_Exe_Types.DS.Tables(0).Select("id_Rombase_DOSBox_Exe_Types = " & TC.getSQLFormat(o_id_Rombase_DOSBox_Exe_Types))
					If rows_Exe_Type.Length = 1 Then part2 = " (" & rows_Exe_Type(0)("Displayname") & ")"
				End If

				e.DisplayText = part1 & part2
			End If
		End If
	End Sub

	Private Sub cmb_DOSBox_Volume_Number_ButtonPressed(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_DOSBox_Volume_Number.ButtonPressed
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
			BS_DOSBox_Files_and_Folders.Current("Volume_Number") = DBNull.Value
			cmb_DOSBox_Volume_Number.EditValue = DBNull.Value
		End If
	End Sub

	Private Sub cmb_DOSBox_Mount_Destination_ButtonPressed(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_DOSBox_Mount_Destination.ButtonPressed
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
			BS_DOSBox_Files_and_Folders.Current("DOSBox_Mount_Destination") = DBNull.Value
			cmb_DOSBox_Mount_Destination.EditValue = DBNull.Value
		End If
	End Sub

	Private Sub cmb_DOSBox_Exe_Type_ButtonPressed(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_DOSBox_Exe_Type.ButtonPressed
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
			BS_DOSBox_Files_and_Folders.Current("id_Rombase_DOSBox_Exe_Types") = DBNull.Value
			cmb_DOSBox_Exe_Type.EditValue = DBNull.Value
		End If
	End Sub

	''' <summary>
	''' Clone of frm_Rom_Manager.Save_MV
	''' </summary>
	''' <param name="Save_Main_Entries"></param>
	''' <param name="tran"></param>
	''' <remarks></remarks>
	Private Sub Save_MV(ByVal Save_Main_Entries As Boolean, ByRef tran As SQLite.SQLiteTransaction)
		Dim rows() As DataRow = Nothing

		If Save_Main_Entries Then
			rows = DS_ML.tbl_Emu_Games.Select("id_Emu_Games_Owner IS NULL")
		Else
			rows = DS_ML.tbl_Emu_Games.Select("id_Emu_Games_Owner IS NOT NULL")
		End If

		Dim dict_Emu_Games_Children As New Dictionary(Of Long, ArrayList)
		If Save_Main_Entries Then
			For Each row As DataRow In DS_ML.tbl_Emu_Games.Rows
				If row.RowState <> DataRowState.Deleted AndAlso row.RowState <> DataRowState.Detached Then
					Dim id_Emu_Games_Owner As Long = TC.NZ(row("id_Emu_Games_Owner"), 0L)
					If id_Emu_Games_Owner <> 0L Then
						If dict_Emu_Games_Children.ContainsKey(id_Emu_Games_Owner) Then
							dict_Emu_Games_Children(id_Emu_Games_Owner).Add(row)
						Else
							Dim al As New ArrayList
							al.Add(row)
							dict_Emu_Games_Children.Add(id_Emu_Games_Owner, al)
						End If
					End If
				End If
			Next
		End If

		Dim GC_Counter As Integer = 0

		For Each row As DataRow In rows
			GC_Counter += 1

			If row.RowState = DataRowState.Deleted OrElse row.RowState = DataRowState.Detached OrElse row.RowState = DataRowState.Unchanged Then
				Continue For
			End If

			Try
				Dim id_Emu_Games_Old As Integer = row("id_Emu_Games")
				Dim bWasAdded As Boolean = row.RowState = DataRowState.Added

				'Save changes to database
				DS_ML.Upsert_Rom_Manager_tbl_Emu_Games(tran, row)

				'Update all child rows (volumes of the game)
				If Save_Main_Entries Then
					If id_Emu_Games_Old <> row("id_Emu_Games") AndAlso dict_Emu_Games_Children.ContainsKey(id_Emu_Games_Old) Then
						For Each row_volume As DataRow In dict_Emu_Games_Children(id_Emu_Games_Old)
							row_volume("id_Emu_Games_Owner") = row("id_Emu_Games")
						Next
					End If
				End If

				If id_Emu_Games_Old > 0 Then
					Dim dt_New As New DS_ML.src_ucr_Emulation_GamesDataTable
					DS_ML.Fill_src_ucr_Emulation_Games(tran, dt_New, Nothing, Nothing, Nothing, row("id_Emu_Games"))
				End If

				Dim id_Emu_Games As Integer = row("id_Emu_Games")

				DS_ML.Update_tbl_Emu_Games_Caches(tran, id_Emu_Games)

			Catch ex As Exception
				MKDXHelper.ExceptionMessageBox(ex)
				Return
			End Try
		Next
	End Sub

	Private Sub cmb_DOSBox_Type_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cmb_DOSBox_Type.EditValueChanged
		If TC.IsNullNothingOrEmpty(cmb_DOSBox_Type.EditValue) Then
			lbl_DOSBox_Exe_Type.Visible = False
			cmb_DOSBox_Exe_Type.Visible = False
			lbl_DOSBox_Mount_Destination.Visible = False
			cmb_DOSBox_Mount_Destination.Visible = False
			lbl_DOSBox_Volume_Number.Visible = False
			cmb_DOSBox_Volume_Number.Visible = False
		End If

		If {1, 2, 4, 5, 6}.Contains(TC.NZ(cmb_DOSBox_Type.EditValue, 0)) Then 'Packed Content, CWD, CD Image, Floppy Image, Floppy Booter
			lbl_DOSBox_Exe_Type.Visible = False
			cmb_DOSBox_Exe_Type.Visible = False
			lbl_DOSBox_Mount_Destination.Visible = True
			cmb_DOSBox_Mount_Destination.Visible = True
			lbl_DOSBox_Volume_Number.Visible = True
			cmb_DOSBox_Volume_Number.Visible = True
		End If

		If {3}.Contains(TC.NZ(cmb_DOSBox_Type.EditValue, 0)) Then 'Executable
			lbl_DOSBox_Exe_Type.Visible = True
			cmb_DOSBox_Exe_Type.Visible = True
			lbl_DOSBox_Mount_Destination.Visible = False
			cmb_DOSBox_Mount_Destination.Visible = False
			lbl_DOSBox_Volume_Number.Visible = False
			cmb_DOSBox_Volume_Number.Visible = False
		End If
	End Sub

	Private Sub rpi_Volume_Number_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rpi_Volume_Number.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
			BS_MV.Current("Volume_Number") = DBNull.Value
			sender.EditValue = DBNull.Value
		End If
	End Sub

	Private Sub bbi_Genres_Remove_Value_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Genres_Remove_Value.ItemClick
		Try
			_Genres_PopupControl.DataSource.Current("Used") = DBNull.Value
			_Genres_PopupControl.RefreshDataSource()
		Catch ex As Exception

		End Try
	End Sub

	Private Sub bbi_Genres_Show_Description_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbi_Genres_Show_Description.ItemClick
		Try
			Dim id As Integer = _Genres_PopupControl.DataSource.Current("id_Moby_Genres")

			If id <= 0 Then Return

			Using frm As New frm_Emu_Game_Edit_GenreDescription(id)
				frm.ShowDialog(Me)
			End Using
		Catch ex As Exception

		End Try
	End Sub

	Private Sub popmnu_TechInfo_BeforePopup(sender As Object, e As CancelEventArgs) Handles popmnu_TechInfo.BeforePopup
		If Not grd_Attributes.Allow_Popup Then
			e.Cancel = True
			Return
		End If

		If _MultiEdit Then 'Single Emu Game Edit vs. Multi Emu Game Edit
			bbi_TechInfo_Remove_Value.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
		Else
			bbi_TechInfo_Remove_Value.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		End If
	End Sub

	Private Sub popmnu_Languages_BeforePopup(sender As Object, e As CancelEventArgs) Handles popmnu_Languages.BeforePopup
		If _MultiEdit Then 'Single Emu Game Edit vs. Multi Emu Game Edit
			bbi_Languages_Remove_Value.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
		Else
			bbi_Languages_Remove_Value.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		End If
	End Sub

	Private Sub popmnu_Regions_BeforePopup(sender As Object, e As CancelEventArgs) Handles popmnu_Regions.BeforePopup
		If _MultiEdit Then 'Single Emu Game Edit vs. Multi Emu Game Edit
			bbi_Regions_Remove_Value.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
		Else
			bbi_Regions_Remove_Value.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		End If
	End Sub

	Private _Genres_PopupControl As MKNetDXLib.ctl_MKDXGrid = Nothing

	Private _Genres_Allow_Popup As Boolean = False

	Private Sub Handle_Genres_Allow_Popup(sender As Object, allow_popup As Boolean) Handles grd_Genres.E_Allow_Popup, grd_Perspectives.E_Allow_Popup, grd_Sports_Themes.E_Allow_Popup, grd_Educational_Categories.E_Allow_Popup, grd_Other_Attributes.E_Allow_Popup
		_Genres_Allow_Popup = allow_popup
	End Sub

	Private Sub popmnu_Genres_BeforePopup(sender As Object, e As CancelEventArgs) Handles popmnu_Genres.BeforePopup
		If Not _Genres_Allow_Popup Then
			e.Cancel = True
			Return
		End If

		If _MultiEdit Then 'Single Emu Game Edit vs. Multi Emu Game Edit
			bbi_Genres_Remove_Value.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
		Else
			bbi_Genres_Remove_Value.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		End If
	End Sub

	Private Sub grd_Genres_Enter(sender As Object, e As EventArgs) Handles grd_Genres.Enter, grd_Visual_Presentation.Enter, grd_Vehicular_Themes.Enter, grd_Sports_Themes.Enter, grd_Special_Edition.Enter, grd_Setting.Enter, grd_Perspectives.Enter, grd_Pacing.Enter, grd_Other_Attributes.Enter, grd_Narrative_Theme_Topic.Enter, grd_Interface_Control.Enter, grd_Gameplay.Enter, grd_Educational_Categories.Enter, grd_DLC_Addon.Enter
		_Genres_PopupControl = Nothing
		_Genres_PopupControl = sender
	End Sub

	Private Sub grd_Genres_MouseDown(sender As Object, e As MouseEventArgs) Handles grd_Genres.MouseDown, grd_Visual_Presentation.MouseDown, grd_Vehicular_Themes.MouseDown, grd_Sports_Themes.MouseDown, grd_Special_Edition.MouseDown, grd_Setting.MouseDown, grd_Perspectives.MouseDown, grd_Pacing.MouseDown, grd_Other_Attributes.MouseDown, grd_Narrative_Theme_Topic.MouseDown, grd_Interface_Control.MouseDown, grd_Gameplay.MouseDown, grd_Educational_Categories.MouseDown, grd_DLC_Addon.MouseDown
		_Genres_PopupControl = Nothing
		_Genres_PopupControl = sender
	End Sub

	Private Sub gv_Genres_MouseMove(sender As Object, e As MouseEventArgs) Handles gv_Genres.MouseMove, gv_DLC_Addon.MouseMove, gv_Educational_Categories.MouseMove, gv_Gameplay.MouseMove, gv_Interface_Control.MouseMove, gv_Languages.MouseMove, gv_Narrative_Theme_Topic.MouseMove, gv_Other_Attributes.MouseMove, gv_Pacing.MouseMove, gv_Perspectives.MouseMove, gv_Regions.MouseMove, gv_Setting.MouseMove, gv_Special_Edition.MouseMove, gv_Sports_Themes.MouseMove, gv_Vehicular_Themes.MouseMove, gv_Visual_Presentation.MouseMove, gv_Attributes.MouseMove
		Me.grd_Genres.ShowHandInColumns(sender, {"Used"}, e)
	End Sub
End Class