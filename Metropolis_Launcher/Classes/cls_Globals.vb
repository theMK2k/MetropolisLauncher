Public Class cls_Globals
	Public Shared Suppress_MetroUINavigationBarsShowing As Boolean = False
	Public Shared Conn As SQLite.SQLiteConnection
	Public Shared ISO_8859_1_Replace As New MKNetLib.cls_MKISO_8859_1_Replace
	'Public Shared DataDir As String = "g:\Frontends\MetropolisLauncher\"
	Public Shared Dir_Extras As String = Application.StartupPath & "\extras"
	Public Shared Dir_Screenshot As String = ""

	Public Shared MLSettingHandler As New cls_PDSettingHandler(Nothing)

	Public Shared StartupTime As DateTime = DateTime.Now

	Public Shared MultiUserMode As Boolean = False
	Public Shared Admin As Boolean = True
	Public Shared Restricted As Boolean = False
	Public Shared id_Users As Integer = 0

	Public Shared Logging As Boolean = False
	Public Shared Logfile As String = ""

	Public Shared Function GetLogfile() As String
		If Alphaleonis.Win32.Filesystem.File.Exists(Logfile) Then
			Return Logfile
		Else
			Logfile = System.Windows.Forms.Application.StartupPath & "\" & "ml.log"
			Return Logfile
		End If
	End Function

	Public Shared Sub AddLog(ByVal text As String, Optional ByVal Timestamp As Boolean = True, Optional ByVal Endline As Boolean = True)
		If Not Logging Then Return

		MKNetLib.cls_MKFileSupport.SaveTextToFile(IIf(Timestamp, DateTime.Now.ToString("yyyyMMdd HHmmss") & "	", "") & text & IIf(Endline, ControlChars.CrLf, ""), GetLogfile)
	End Sub

	Public Shared Function Encode_Password(ByVal Password As String) As String
		If Password IsNot Nothing Then
			Return MKNetLib.cls_MKCryptography.Get_MD5("ML4EVAR" & Password & "RAVE4LM")
		Else
			Return Nothing
		End If
	End Function

	'id_Moby_Platforms of tbl_Moby_Platforms
	Public Enum enm_Moby_Platforms
		mame = -2
		ALL = -1
		linux = 1
		dos = 2
		win = 3
		pcboot = 4
		win3x = 5
		ps1 = 6
		ps2 = 7
		dc = 8
		n64 = 9
		gb = 10
		gbc = 11
		gba = 12
		xbox = 13
		gc = 14
		snes = 15
		gen = 16
		jag = 17
		lynx = 18
		amiga = 19
		scd = 20
		_32x = 21
		nes = 22
		sat = 23
		st = 24
		gg = 25
		sms = 26
		c64 = 27
		a26 = 28
		cv = 29
		iv = 30
		aii = 31
		ngage = 32
		a52 = 33
		a78 = 34
		_3do = 35
		ng = 36
		vec = 37
		vb = 38
		a8bit = 39
		tgfx = 40
		zxspec = 41
		vsmile = 42
		vic20 = 43
		nds = 44
		tgfxcd = 45
		psp = 46
		ti994a = 47
		ws = 48
		wsc = 49
		gamecom = 50
		a2gs = 51
		ngp = 52
		ngpc = 53
		ngcd = 54
		giz = 55
		cd32 = 56
		msx = 57
		trs80 = 58
		pcfx = 59
		cpc = 60
		c128 = 61
		coco = 62
		brew = 63
		j2me = 64
		palm = 65
		winmob = 66
		symbian = 67
		zod = 68
		x360 = 69
		exen = 70
		mophun = 71
		doja = 72
		cdi = 73
		mac = 74
		ody = 75
		chanf = 76
		cbmpet = 77
		ody2 = 78
		dragon = 79
		ipod = 80
		ps3 = 81
		wii = 82
		cdtv = 83
		browser = 84
		spectravid = 85
		iphone = 86
		dsi = 87
		zeebo = 88
		ngage2 = 89
		bb = 90
		android = 91
		bbc = 92
		electron = 93
		pc88 = 94
		pc98 = 95
		ipad = 96
		mv = 97
		winphone = 98
		bada = 99
		webos = 100
		_3ds = 101
		fmtowns = 102
		pico = 103
		gw = 104
		vita = 105
		x68k = 106
		playdia = 107
		gp32 = 108
		sv = 109
		superacan = 110
		oric = 111
		pippin = 112
		rca = 113
		sg1000 = 114
		c16 = 115
		nuon = 116
		acorn = 117
		zx80 = 118
		zx81 = 119
		samcoupe = 120
		sharpx1 = 121
		gp2x = 122
		wiz = 123
		loopy = 124
		pv1000 = 125
		fm7 = 126
		sgfx = 127
		g7400 = 128
		atom = 129
		_to = 130
		ql = 131
		wiiu = 132
	End Enum

	Public Enum enm_Rombase_DOSBox_Filetypes
		zip = 1
		cwd = 2
		exe = 3
		iso = 4
		img = 5
		img_boot = 6
		int = 7
	End Enum

	Public Enum enm_Rombase_DOSBox_Exe_Types
		inst = 1
		main = 2
		setup = 3
		ignore = 4
	End Enum

	'id_Moby_Genres_Categories of tbl_Moby_Genres_Categories
	Public Enum enm_Moby_Genres_Categories
		Basic_Genres = 1
		Perspective = 2
		Visual_Presentation = 3
		Pacing = 4
		Gameplay = 5
		Interface_Control = 6
		Sports_Themes = 7
		Educational_Categories = 8
		Vehicular_Themes = 9
		Setting = 10
		Narrative_Theme_Topic = 11
		DLC_Addon = 12
		Special_Edition = 13
		Other_Attributes = 14
	End Enum

	Public Shared Function TempDir(tran As SQLite.SQLiteTransaction) As String
		Dim sTempDir As String = TC.NZ(cls_Settings.GetSetting("Dir_Temp", tran:=tran), "")

		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(sTempDir) Then
			sTempDir = Application.StartupPath & "\temp"
		End If

		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(sTempDir) Then
			sTempDir = Alphaleonis.Win32.Filesystem.Path.GetTempPath
		End If

		MKNetLib.cls_MKFileSupport.TempDirRoot = sTempDir
		Return sTempDir
	End Function

	Public Shared Function BackupsDir(tran As SQLite.SQLiteTransaction) As String
		Dim sBackupDir As String = TC.NZ(cls_Settings.GetSetting("Dir_Backup", tran:=tran), "")

		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(sBackupDir) Then
			sBackupDir = Application.StartupPath & "\backups"
		End If

		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(sBackupDir) Then
			sBackupDir = ""
		End If

		Return sBackupDir
	End Function
End Class
