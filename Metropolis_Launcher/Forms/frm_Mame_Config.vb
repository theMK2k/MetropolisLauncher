Imports System.Xml

Public Class frm_Mame_Config

	Public Class cls_Mame_Category_Roms
		Public Category As String
		Public RomNames As ArrayList
	End Class

	Private _DialogResult As DialogResult = Windows.Forms.DialogResult.Cancel

	Public Sub New()
		InitializeComponent()

		Me.txb_MameExe.Text = TC.NZ(cls_Settings.GetSetting("Mame_Executable"), "")
		Me.txb_history_dat.Text = TC.NZ(cls_Settings.GetSetting("Mame_history_dat"), "")
		Me.txb_mameinfo_dat.Text = TC.NZ(cls_Settings.GetSetting("Mame_mameinfo_dat"), "")
		Me.txb_gameinit_dat.Text = TC.NZ(cls_Settings.GetSetting("Mame_gameinit_dat"), "")
		Me.txb_Filter_ini.Text = TC.NZ(cls_Settings.GetSetting("Mame_filter_ini"), "")

		Get_Mame_Version_From_Exe()
	End Sub

	Private Sub frm_Mame_Config_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		cls_Settings.SetSetting("Mame_Executable", txb_MameExe.Text)
		cls_Settings.SetSetting("Mame_history_dat", txb_history_dat.Text)
		cls_Settings.SetSetting("Mame_mameinfo_dat", txb_mameinfo_dat.Text)
		cls_Settings.SetSetting("Mame_gameinit_dat", txb_gameinit_dat.Text)
		cls_Settings.SetSetting("Mame_filter_ini", txb_Filter_ini.Text)

		Me.DialogResult = Me._DialogResult
	End Sub

	Private Sub btn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Close.Click
		Me.Close()
	End Sub

	Private Sub Get_Mame_Version_From_Exe()
		If txb_MameExe.Text = "" Then
			lbl_Mame_Version_Detected.Text = "Please enter the full path to your mame.exe or mame64.exe in the box above."
			Return
		End If

		If Not Alphaleonis.Win32.Filesystem.File.Exists(txb_MameExe.Text) Then
			lbl_Mame_Version_Detected.Text = "[ERROR]: The M.A.M.E. Executable cannot be found!"
			Return
		End If

		Try
			Dim proc As New Process
			With proc.StartInfo
				.FileName = txb_MameExe.Text
				.WorkingDirectory = Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(txb_MameExe.Text)
				.Arguments = "-help"
				.UseShellExecute = False
				.RedirectStandardOutput = True
				.WindowStyle = ProcessWindowStyle.Hidden
				.CreateNoWindow = True
			End With

			proc.Start()
			Dim output As String = proc.StandardOutput.ReadToEnd
			proc.WaitForExit()

			If output.Contains("M.A.M.E. v") OrElse output.Contains("MAME v") Then
				lbl_Mame_Version_Detected.Text = output.Split(ControlChars.CrLf)(0)
			Else
				lbl_Mame_Version_Detected.Text = "[ERROR]: can't get info from the M.A.M.E. Executable you provided."
			End If
		Catch ex As Exception
			lbl_Mame_Version_Detected.Text = "[ERROR]: exception while getting M.A.M.E. version info."
		End Try
	End Sub

	Private Sub btn_Mame_Exe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Mame_Exe.Click
		Dim old_dir As String = txb_MameExe.Text

		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(old_dir) AndAlso Not Alphaleonis.Win32.Filesystem.File.Exists(old_dir) Then
			old_dir = ""
		End If

		Dim sFile As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("Select M.A.M.E. Executable", "M.A.M.E. Executable (mame.exe;mame64.exe)|mame.exe;mame64.exe", 0, "", old_dir, ParentForm:=Me)

		If Alphaleonis.Win32.Filesystem.File.Exists(sFile) Then
			Me.txb_MameExe.Text = sFile
		End If

		Get_Mame_Version_From_Exe()
	End Sub

	Private Sub txb_MameExe_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txb_MameExe.Leave
		Get_Mame_Version_From_Exe()
	End Sub

	Private Sub btn_history_dat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_history_dat.Click
		Dim sFile As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("Select history.dat", "history.dat|history.dat", ParentForm:=Me)

		Me.txb_history_dat.Text = sFile
	End Sub

	Private Sub btn_mameinfo_dat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_mameinfo_dat.Click
		Dim sFile As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("Select mameinfo.dat", "mameinfo.dat|mameinfo.dat", ParentForm:=Me)

		Me.txb_mameinfo_dat.Text = sFile
	End Sub

	Private Sub btn_gameinit_dat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_gameinit_dat.Click
		Dim sFile As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("Select gameinit.dat", "gameinit.dat|gameinit.dat", ParentForm:=Me)

		Me.txb_gameinit_dat.Text = sFile
	End Sub


	Private Sub btn_Filter_ini_Click(sender As Object, e As EventArgs) Handles btn_Filter_ini.Click
		Dim sFile As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("Select an .ini file for Filtering", "*.ini|*.ini", ParentForm:=Me)

		Me.txb_Filter_ini.Text = sFile
	End Sub

	Private Sub btn_Scan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Scan.Click
		If txb_MameExe.Text = "" Then
			MKDXHelper.MessageBox("Please provide the full path to your mame.exe or mame64.exe.", "M.A.M.E. Executable missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			txb_MameExe.Focus()
			Return
		End If

		If Not Alphaleonis.Win32.Filesystem.File.Exists(txb_MameExe.Text) Then
			MKDXHelper.MessageBox("M.A.M.E. Executable is missing!", "M.A.M.E. Executable missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			txb_MameExe.Focus()
			Return
		End If

		If Not Alphaleonis.Win32.Filesystem.File.Exists(txb_MameExe.Text) Then
			MKDXHelper.MessageBox("M.A.M.E. Executable is missing!", "M.A.M.E. Executable not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			txb_MameExe.Focus()
			Return
		End If

		If Not Alphaleonis.Win32.Filesystem.File.Exists(Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(txb_MameExe.Text) & "\mame.ini") Then
			MKDXHelper.MessageBox("The mame.ini file is missing, please create one and configure it. The command is '" & txb_MameExe.Text & " -cc'", "mame.ini not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			txb_MameExe.Focus()
			Return
		End If

		If txb_history_dat.Text = "" Then
			If Not MKDXHelper.MessageBox("Although optional, it is recommended that you provide a history.dat file that fits your M.A.M.E. installation as it provides interesting information for games. Download history.dat from http://www.arcade-history.com/. Do you still want to continue the scan process?", "history.dat is missing", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
				txb_history_dat.Focus()
				Return
			End If
		ElseIf Not Alphaleonis.Win32.Filesystem.File.Exists(txb_history_dat.Text) Then
			If Not MKDXHelper.MessageBox("The history.dat file you provided cannot be found! Do you still want to continue the scan process?", "history.dat not found", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
				txb_history_dat.Focus()
				Return
			End If
		End If

		If txb_mameinfo_dat.Text = "" Then
			If Not MKDXHelper.MessageBox("Although optional, it is recommended that you provide a mameinfo.dat file that fits your M.A.M.E. installation as it provides interesting information for games. Download mameinfo.dat from http://mameinfo.mameworld.info/. Do you still want to continue the scan process?", "mameinfo.dat is missing", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
				txb_mameinfo_dat.Focus()
				Return
			End If
		ElseIf Not Alphaleonis.Win32.Filesystem.File.Exists(txb_mameinfo_dat.Text) Then
			If Not MKDXHelper.MessageBox("The mameinfo.dat file you provided cannot be found! Do you still want to continue the scan process?", "mameinfo.dat not found", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
				txb_mameinfo_dat.Focus()
				Return
			End If
		End If

		If txb_gameinit_dat.Text = "" Then
			If Not MKDXHelper.MessageBox("Although optional, it is recommended that you provide a gameinit.dat file that fits your M.A.M.E. installation as it provides interesting information for games. Download gameinit.dat from http://www.progettosnaps.net/gameinit/. Do you still want to continue the scan process?", "gameinit.dat is missing", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
				txb_gameinit_dat.Focus()
				Return
			End If
		ElseIf Not Alphaleonis.Win32.Filesystem.File.Exists(txb_gameinit_dat.Text) Then
			If Not MKDXHelper.MessageBox("The gameinit.dat file you provided cannot be found! Do you still want to continue the scan process?", "gameinit.dat not found", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
				txb_gameinit_dat.Focus()
				Return
			End If
		End If

		If Not MKDXHelper.MessageBox("The scan process will gather game informations from your provided M.A.M.E. installation and will scan for games in your M.A.M.E. rom directory. This will take a while, do you want to continue?", "Scan", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
			Return
		End If

		Dim bResult As Boolean = True

		Dim dt_Mame_Roms As New DS_ML.tbl_Mame_RomsDataTable

		Me.Cursor = Cursors.WaitCursor

		'Dim res As DialogResult = MKDXHelper.MessageBox("Fetch Rom infos from Mame, history.dat and mameinfo?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

		'If res = Windows.Forms.DialogResult.Cancel Then
		'	Return
		'End If

		'If res = Windows.Forms.DialogResult.Yes Then
		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			bResult = Scan_Mame_Gameinfos(tran, dt_Mame_Roms) 'Fill tbl_Mame_Roms

			If bResult = False Then
				Me.Cursor = Cursors.Default
				tran.Rollback()
				Return
			End If

			Me._DialogResult = Windows.Forms.DialogResult.OK

			tran.Commit()
		End Using
		'End If

		'res = MKDXHelper.MessageBox("Verify roms from M.A.M.E. and add games?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

		'If res = Windows.Forms.DialogResult.Cancel Then
		'	Return
		'End If

		'If res = Windows.Forms.DialogResult.Yes Then
		'Verifyroms here and add games
		'mame64 -verifyroms

		Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Nothing), 400, 60, ProgressBarStyle.Marquee, False, "Scan step 8 of 9: Verifying roms (please be patient, this can take a while!)", 0, 100, True)
		prg.Start()

		Using tran = cls_Globals.Conn.BeginTransaction
			Try

				Dim proc As New Process
				With proc.StartInfo
					.FileName = txb_MameExe.Text
					.WorkingDirectory = Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(txb_MameExe.Text)
					.Arguments = "-verifyroms"
					.UseShellExecute = False
					.RedirectStandardOutput = True
					.WindowStyle = ProcessWindowStyle.Hidden
					.CreateNoWindow = True
				End With

				proc.Start()
				Dim output As String = proc.StandardOutput.ReadToEnd
				proc.WaitForExit()

				Dim dt_Mame_Rom_Names As DataTable = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT name from tbl_Mame_Roms WHERE isbios = 0 AND isdevice = 0 AND ismechanical = 0 AND issoftwarelist = 0 AND hasdisplay = 1 AND runnable = 1", Nothing, tran)
				Dim al_Mame_Rom_Names As New ArrayList
				For Each dr_Mame_Rom_Names As DataRow In dt_Mame_Rom_Names.Rows
					al_Mame_Rom_Names.Add(dr_Mame_Rom_Names("name"))
				Next

				Dim ar_Output As String() = output.Split(ControlChars.CrLf)

				prg.Close()
				prg = New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(tran), 400, 60, ProgressBarStyle.Blocks, False, "Scan step 9 of 9: Processing verified roms", 0, ar_Output.Length, True)
				prg.Start()

				For Each verification_line As String In ar_Output
					prg.IncreaseCurrentValue()

					If verification_line.Contains(" is good") OrElse verification_line.Contains("is best available") Then
						Dim name As String = MKNetLib.cls_MKRegex.GetMatches(verification_line, "romset (.*?) ")(0).Groups(1).Value

						If al_Mame_Rom_Names.Contains(name) Then
							Dim dt_Mame_Rom As DataTable = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT name, description, year, manufacturer, cloneof, romof, mameinfo from tbl_Mame_Roms WHERE name = " & TC.getSQLFormat(name), Nothing, tran)

							If dt_Mame_Rom.Rows.Count = 1 Then
								Dim File As String = name
								Dim InnerFile As String = name
								Dim real_Name As String = dt_Mame_Rom.Rows(0)("description")
								Dim Year As Object = dt_Mame_Rom.Rows(0)("year")

								Dim Name_Prefix As Object = Nothing
								Dim Note As Object = Nothing
								Dim Developer As Object = dt_Mame_Rom.Rows(0)("manufacturer")
								Dim Description As Object = Nothing

								If real_Name.StartsWith("The ") Then
									Name_Prefix = "The"
									real_Name = MKNetLib.cls_MKStringSupport.Clean_Left(real_Name, "The ")
								ElseIf real_Name.StartsWith("A ") Then
									Name_Prefix = "A"
									real_Name = MKNetLib.cls_MKStringSupport.Clean_Left(real_Name, "A ")
								End If

								If TC.NZ(dt_Mame_Rom.Rows(0)("mameinfo"), "").Length > 1 Then
									Description = MKNetLib.cls_MKStringSupport.Ensure_CrLf(dt_Mame_Rom.Rows(0)("mameinfo"))
								End If

								If real_Name.Contains("(") Then
									Dim matches As System.Text.RegularExpressions.MatchCollection = MKNetLib.cls_MKRegex.GetMatches(real_Name, "\((.*?)\)")

									Dim bFirst As Boolean = True

									For Each match As System.Text.RegularExpressions.Match In matches
										If bFirst Then
											Note = match.Groups(1).Value
											bFirst = False
										Else
											Note &= ", " & match.Groups(1).Value
										End If
									Next

									real_Name = MKNetLib.cls_MKRegex.GetMatches(real_Name, "(.*?)\(")(0).Groups(1).Value.Trim
								End If

								DS_ML.Upsert_MAME_tbl_Emu_Games(tran, -2, name, name, real_Name, Name_Prefix, Note, Developer, Description, Year)
							Else
								Dim j As Integer = 0
							End If
						End If
					End If
				Next

				'CLEANUP unwanted Roms
				Dim sSQL As String = ""
				sSQL &= "DELETE FROM tbl_Emu_Games" & ControlChars.CrLf
				sSQL &= "WHERE id_Moby_Platforms = -2" & ControlChars.CrLf
				sSQL &= "			AND" & ControlChars.CrLf
				If Alphaleonis.Win32.Filesystem.File.Exists(txb_Filter_ini.Text) Then
					sSQL &= "			File IN"
					sSQL &= "			("
					sSQL &= "				SELECT name FROM tbl_Mame_Roms WHERE IFNULL(is_allowed_by_filter_ini, 0) = 0"
					sSQL &= "			)"
				Else
					sSQL &= "			File IN" & ControlChars.CrLf
					sSQL &= "			(" & ControlChars.CrLf
					sSQL &= "				SELECT name FROM tbl_Mame_Roms" & ControlChars.CrLf
					sSQL &= "				WHERE " & ControlChars.CrLf
					sSQL &= "				(" & ControlChars.CrLf
					sSQL &= "						IFNULL(isbios, 0) = 1" & ControlChars.CrLf
					sSQL &= "						OR IFNULL(isdevice, 0) = 1" & ControlChars.CrLf
					sSQL &= "						OR IFNULL(ismechanical, 0) = 1" & ControlChars.CrLf
					sSQL &= "						OR IFNULL(runnable, 0) = 0" & ControlChars.CrLf
					sSQL &= "						OR IFNULL(issoftwarelist, 0) = 1" & ControlChars.CrLf
					sSQL &= "						OR IFNULL(hasdisplay, 1) = 0" & ControlChars.CrLf
					sSQL &= "				)" & ControlChars.CrLf
					sSQL &= "				UNION"
					sSQL &= "				SELECT name FROM tbl_Mame_Roms" & ControlChars.CrLf
					sSQL &= "				WHERE cloneof IN" & ControlChars.CrLf
					sSQL &= "				(" & ControlChars.CrLf
					sSQL &= "					SELECT name" & ControlChars.CrLf
					sSQL &= "					FROM tbl_Mame_Roms" & ControlChars.CrLf
					sSQL &= "					WHERE	IFNULL(isbios, 0) = 1" & ControlChars.CrLf
					sSQL &= "								OR IFNULL(isdevice, 0) = 1" & ControlChars.CrLf
					sSQL &= "								OR IFNULL(ismechanical, 0) = 1" & ControlChars.CrLf
					sSQL &= "								OR IFNULL(runnable, 0) = 0" & ControlChars.CrLf
					sSQL &= "								OR IFNULL(issoftwarelist, 0) = 1" & ControlChars.CrLf
					sSQL &= "								OR IFNULL(hasdisplay, 1) = 0" & ControlChars.CrLf
					sSQL &= "				)" & ControlChars.CrLf
					sSQL &= "			)" & ControlChars.CrLf
				End If

				DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)

				DS_ML.Update_Platform_NumGames_Cache(tran, cls_Globals.enm_Moby_Platforms.mame)

				tran.Commit()

				Me.Cursor = Cursors.Default
				prg.Close()
			Catch ex As Exception
				Me.Cursor = Cursors.Default
				If prg IsNot Nothing Then prg.Close()
				tran.Rollback()
				Return
			Finally
				Me.Cursor = Cursors.Default
				If prg IsNot Nothing Then prg.Close()
			End Try
		End Using

		'End If

		MKDXHelper.MessageBox("Import successful!", "Import successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

		Me._DialogResult = Windows.Forms.DialogResult.OK
	End Sub

	''' <summary>
	''' Fill tbl_Mame_Roms
	''' </summary>
	''' <param name="tran"></param>
	''' <param name="dt_Mame_Roms"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Private Function Scan_Mame_Gameinfos(ByRef tran As SQLite.SQLiteTransaction, ByRef dt_Mame_Roms As DS_ML.tbl_Mame_RomsDataTable) As Boolean
		Dim xml_path As String = cls_Globals.TempDir(tran)

		Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(tran), 400, 60, ProgressBarStyle.Marquee, False, "Scan step 1 of 9: Reading game infos from M.A.M.E.", 0, 100, True)
		prg.Start()

		If Alphaleonis.Win32.Filesystem.Directory.Exists(xml_path) Then
			xml_path &= "\" & "mame_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".xml"
		Else
			MKDXHelper.MessageBox("The temporary directory could not be found. Please set one up in the Settings section.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return False
		End If

		'Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname, 400, 60, ProgressBarStyle.Marquee, False, "Scan step 1 of 9: Reading game infos from M.A.M.E.", 0, 100, True)
		'prg.Start()

		'Try
		DS_ML.Delete_tbl_Mame_Roms(tran)

		Dim proc As New Process
		With proc.StartInfo
			.FileName = txb_MameExe.Text
			.WorkingDirectory = Alphaleonis.Win32.Filesystem.Path.GetDirectoryName(txb_MameExe.Text)
			.Arguments = "-listxml"
			.UseShellExecute = False  'Was False; if M.A.M.E. needs to be started as Administrator, ShellExecute is necessary
			.RedirectStandardOutput = True  'We don't need to read the Standard Output as we redirect it to an .xml file
			.WindowStyle = ProcessWindowStyle.Hidden
			.CreateNoWindow = True
		End With

		Using sw As New System.IO.StreamWriter(xml_path, False)
			proc.Start()

			Dim line As String = proc.StandardOutput.ReadLine

			While line IsNot Nothing
				sw.WriteLine(line)
				line = proc.StandardOutput.ReadLine
			End While

			proc.WaitForExit()
			sw.Close()
		End Using

		'TODO: write output to file in order to prevent Out of Memory Exception
		'Dim output As String = proc.StandardOutput.ReadToEnd	'Not needed, we write to file directly
		'proc.StandardOutput.

		If Not Alphaleonis.Win32.Filesystem.File.Exists(xml_path) Then
			prg.Close()
			MKDXHelper.MessageBox("Could not read " & xml_path & "!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return False
		End If

		Dim GameCount As Integer = 0

		Dim xml_reader As New System.Xml.XmlTextReader(xml_path)

		While xml_reader.Read
			If xml_reader.NodeType = System.Xml.XmlNodeType.Element Then
				If {"machine", "game"}.Contains(xml_reader.Name) Then
					GameCount += 1
				End If
			End If
		End While

		xml_reader.Close()

		prg.Close()
		prg = New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(tran), 400, 60, ProgressBarStyle.Blocks, False, "Scan step 2 of 9: Processing game infos from M.A.M.E. ({0} / {1})", 0, GameCount, True)
		prg.Start()

		xml_reader = New System.Xml.XmlTextReader(xml_path)

		While xml_reader.Read
			If xml_reader.NodeType = System.Xml.XmlNodeType.Element Then
				If {"machine", "game"}.Contains(xml_reader.Name) Then
					prg.IncreaseCurrentValue()

					Dim isSoftwareList As Boolean = False
					Dim hasDisplay As Boolean = False

					Dim DR As DS_ML.tbl_Mame_RomsRow = dt_Mame_Roms.NewRow

					DR("issoftwarelist") = False
					DR("hasdisplay") = False

					DR("name") = TC.NZ(xml_reader.GetAttribute("name"), DBNull.Value)
					DR("cloneof") = TC.NZ(xml_reader.GetAttribute("cloneof"), DBNull.Value)
					DR("romof") = TC.NZ(xml_reader.GetAttribute("romof"), DBNull.Value)
					DR("sampleof") = TC.NZ(xml_reader.GetAttribute("sampleof"), DBNull.Value)
					DR("sourcefile") = TC.NZ(xml_reader.GetAttribute("sourcefile"), DBNull.Value)
					DR("isbios") = IIf(TC.NZ(xml_reader.GetAttribute("isbios"), "no") = "no", False, True)
					DR("isdevice") = IIf(TC.NZ(xml_reader.GetAttribute("isdevice"), "no") = "no", False, True)
					DR("ismechanical") = IIf(TC.NZ(xml_reader.GetAttribute("ismechanical"), "no") = "no", False, True)
					DR("runnable") = IIf(TC.NZ(xml_reader.GetAttribute("runnable"), "yes") = "no", False, True)

					While xml_reader.Read AndAlso Not (xml_reader.NodeType = XmlNodeType.EndElement AndAlso {"machine", "game"}.Contains(xml_reader.Name))
						If xml_reader.NodeType = System.Xml.XmlNodeType.Element Then
							Select Case xml_reader.Name
								Case "description"
									DR.description = xml_reader.ReadInnerXml
								Case "year"
									DR.year = xml_reader.ReadInnerXml
								Case "manufacturer"
									DR.manufacturer = xml_reader.ReadInnerXml
								Case "softwarelist"
									DR("issoftwarelist") = True
								Case "display"
									DR("hasdisplay") = True
							End Select
						End If
					End While

					dt_Mame_Roms.Rows.Add(DR)
				End If
			End If
		End While

		xml_reader.Close()

		Alphaleonis.Win32.Filesystem.File.Delete(xml_path)

		'Speedup
		Dim dict_Mame_Roms_Cache As New Dictionary(Of String, ArrayList)
		For Each row_Mame_Roms As DataRow In dt_Mame_Roms
			Dim name As String = TC.NZ(row_Mame_Roms("name"), "")
			If dict_Mame_Roms_Cache.ContainsKey(name) Then
				dict_Mame_Roms_Cache(name).Add(row_Mame_Roms)
			Else
				dict_Mame_Roms_Cache(name) = New ArrayList
				dict_Mame_Roms_Cache(name).Add(row_Mame_Roms)
			End If
		Next

		prg.Close()

		'read is_allowed_by_filter_ini from txb_Filter_ini
		If Alphaleonis.Win32.Filesystem.File.Exists(Me.txb_Filter_ini.Text) Then
			prg = New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(tran), 400, 60, ProgressBarStyle.Blocks, False, "Scan step 3 of 9: Reading filtered games from " & Alphaleonis.Win32.Filesystem.Path.GetFileName(Me.txb_Filter_ini.Text), 0, 100, True)
			prg.Start()

			Dim reader As New System.IO.StreamReader(txb_Filter_ini.Text)

			Dim line As String

			Dim Counter As Integer = 0
			Dim prg_Counter As Integer = 0

			While reader.Peek <> -1
				line = reader.ReadLine.Trim

				If line <> "" AndAlso Not line.StartsWith("[") AndAlso Not line.Contains(" ") Then
					Counter += 1

					If prg_Counter < 100 Then
						If CType(Counter, Double) / CType(GameCount, Double) * 100 > CType(prg_Counter, Double) Then
							prg_Counter += 1
							prg.IncreaseCurrentValue()
						End If
					End If

					'Speedup
					If dict_Mame_Roms_Cache.ContainsKey(line) Then
						Dim al_Rows As ArrayList = dict_Mame_Roms_Cache(line)
						If al_Rows.Count = 1 Then
							al_Rows(0)("is_allowed_by_filter_ini") = True
						End If
					End If
				End If
			End While

			prg.Close()
		End If

		'import gameinit.dat
		If Alphaleonis.Win32.Filesystem.File.Exists(txb_gameinit_dat.Text) Then
			prg = New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(tran), 400, 60, ProgressBarStyle.Blocks, False, "Scan step 4 of 9: Reading game init infos from gameinit.dat", 0, 100, True)
			prg.Start()

			Dim reader As New System.IO.StreamReader(txb_gameinit_dat.Text)

			Dim line As String

			Dim content As String

			Dim Counter As Integer = 0
			Dim prg_Counter As Integer = 0

			While reader.Peek <> -1
				line = reader.ReadLine

				If line.StartsWith("$info=") Then
					Dim mame_roms As String() = line.Replace("$info=", "").Split(",")

					content = ""

					While reader.Peek <> -1
						line = reader.ReadLine

						If line.StartsWith("$end") Then
							For Each mame_rom As String In mame_roms
								If mame_rom.Trim.Length > 0 Then
									Counter += 1

									If prg_Counter < 100 Then
										If CType(Counter, Double) / CType(GameCount, Double) * 100 > CType(prg_Counter, Double) Then
											prg_Counter += 1
											prg.IncreaseCurrentValue()
										End If
									End If

									'Speedup
									If dict_Mame_Roms_Cache.ContainsKey(mame_rom.Trim) Then
										Dim al_Rows As ArrayList = dict_Mame_Roms_Cache(mame_rom.Trim)
										If al_Rows.Count = 1 Then
											al_Rows(0)("mameinfo") = TC.NZ(al_Rows(0)("mameinfo"), "") & content
										End If
									End If
								End If
							Next
							Exit While
						End If

						If Not line.StartsWith("$") AndAlso Not line.StartsWith("#") Then
							content &= line & ControlChars.CrLf
						End If

					End While
				End If
			End While

			prg.Close()
		End If

		'import history.dat
		If Alphaleonis.Win32.Filesystem.File.Exists(txb_history_dat.Text) Then
			prg = New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(tran), 400, 60, ProgressBarStyle.Blocks, False, "Scan step 5 of 9: Reading game infos from history.dat", 0, 100, True)
			prg.Start()

			Dim reader As New System.IO.StreamReader(txb_history_dat.Text)

			Dim line As String

			Dim content As String

			Dim Counter As Integer = 0
			Dim prg_Counter As Integer = 0

			While reader.Peek <> -1
				line = reader.ReadLine

				If line.StartsWith("$info=") Then
					Dim mame_roms As String() = line.Replace("$info=", "").Split(",")

					content = ""

					While reader.Peek <> -1
						line = reader.ReadLine

						If line.StartsWith("$end") Then
							For Each mame_rom As String In mame_roms
								If mame_rom.Trim.Length > 0 Then
									Counter += 1

									If prg_Counter < 100 Then
										If CType(Counter, Double) / CType(GameCount, Double) * 100 > CType(prg_Counter, Double) Then
											prg_Counter += 1
											prg.IncreaseCurrentValue()
										End If
									End If

									'Dim drs_mame_roms As DataRow() = dt_Mame_Roms.Select("name = '" & mame_rom & "'")
									'If drs_mame_roms.Length = 1 Then
									'	drs_mame_roms(0)("mameinfo") = TC.NZ(drs_mame_roms(0)("mameinfo"), "") & content
									'End If

									'Speedup
									If dict_Mame_Roms_Cache.ContainsKey(mame_rom.Trim) Then
										Dim al_Rows As ArrayList = dict_Mame_Roms_Cache(mame_rom.Trim)
										If al_Rows.Count = 1 Then
											al_Rows(0)("mameinfo") = TC.NZ(al_Rows(0)("mameinfo"), "") & content
										End If
									End If
								End If
							Next
							Exit While
						End If

						If Not line.StartsWith("$") AndAlso Not line.StartsWith("#") Then
							content &= line & ControlChars.CrLf
						End If

					End While
				End If
			End While

			prg.Close()
		End If

		'import mameinfo.dat
		If Alphaleonis.Win32.Filesystem.File.Exists(txb_mameinfo_dat.Text) Then
			Dim Counter As Integer = 0
			Dim prg_Counter As Integer = 0

			prg = New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(tran), 400, 60, ProgressBarStyle.Blocks, False, "Scan step 6 of 9: Reading game infos from mameinfo.dat ({0} / {1})", 0, 100, True)
			prg.Start()

			Dim reader As New System.IO.StreamReader(txb_mameinfo_dat.Text)

			Dim line As String

			Dim content As String

			While reader.Peek <> -1
				line = reader.ReadLine

				If line.StartsWith("$info=") Then
					Dim mame_roms As String() = line.Replace("$info=", "").Split(",")

					content = ""

					While reader.Peek <> -1
						line = reader.ReadLine

						If line.StartsWith("$end") Then
							For Each mame_rom As String In mame_roms
								If mame_rom.Trim.Length > 0 Then

									Counter += 1

									If prg_Counter < 100 Then
										If CType(Counter, Double) / CType(GameCount, Double) * 100 > CType(prg_Counter, Double) Then
											prg_Counter += 1
											prg.IncreaseCurrentValue()
										End If
									End If

									'Dim drs_mame_roms As DataRow() = dt_Mame_Roms.Select("name = '" & mame_rom & "'")
									'If drs_mame_roms.Length = 1 Then
									'	drs_mame_roms(0)("mameinfo") = TC.NZ(drs_mame_roms(0)("mameinfo"), "") & content
									'End If

									'Speedup
									If dict_Mame_Roms_Cache.ContainsKey(mame_rom.Trim) Then
										Dim al_Rows As ArrayList = dict_Mame_Roms_Cache(mame_rom.Trim)
										If al_Rows.Count = 1 Then
											al_Rows(0)("mameinfo") = TC.NZ(al_Rows(0)("mameinfo"), "") & content
										End If
									End If
								End If
							Next
							Exit While
						End If

						If Not line.StartsWith("$") AndAlso Not line.StartsWith("#") Then
							content &= line & ControlChars.CrLf
						End If

					End While
				End If
			End While

			prg.Close()
		End If

		prg = New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(tran), 400, 60, ProgressBarStyle.Blocks, False, "Scan step 7 of 9: Save to internal table ({0} / {1})", 0, dt_Mame_Roms.Rows.Count, True)
		prg.Start()

		DataAccess.FireProcedure(tran.Connection, 0, "DELETE FROM tbl_Mame_Roms", tran)

		Dim sSQL As String = ""

		For Each row_mame_rom As DataRow In dt_Mame_Roms
			prg.IncreaseCurrentValue()

			sSQL = "INSERT INTO tbl_Mame_Roms (name, description, year, manufacturer, sourcefile, isbios, isdevice, ismechanical, issoftwarelist, hasdisplay, runnable, cloneof, romof, sampleof, mameinfo, is_allowed_by_filter_ini) VALUES ("
			sSQL &= TC.getSQLParameter(row_mame_rom("name"), row_mame_rom("description"), row_mame_rom("year"), row_mame_rom("manufacturer"), row_mame_rom("sourcefile"), row_mame_rom("isbios"), row_mame_rom("isdevice"), row_mame_rom("ismechanical"), row_mame_rom("issoftwarelist"), row_mame_rom("hasdisplay"), row_mame_rom("runnable"), row_mame_rom("cloneof"), row_mame_rom("romof"), row_mame_rom("sampleof"), row_mame_rom("mameinfo"), row_mame_rom("is_allowed_by_filter_ini")) & ")"
			DataAccess.FireProcedure(tran.Connection, 0, sSQL, tran)
		Next

		prg.Close()
		'Catch ex As Exception
		'	Me.Cursor = Cursors.Default
		'	If prg IsNot Nothing Then prg.Close()
		'	MKDXHelper.MessageBox("Exception: " & ex.Message, "ERROR during scan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'	Return False
		'End Try

		Return True
	End Function

	'''  <summary>
	''' Import Ini File where M.A.M.E. Roms are grouped by folders
	''' </summary>
	''' <param name="aContent">Array of the ini File content</param>
	''' <param name="iniFile">Full path of the ini File</param>
	''' <remarks></remarks>
	Private Sub Import_Ini_File_Folderbased(ByRef aContent As String(), ByVal iniFile As String)
		Dim i As Integer = 0

		Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Nothing), 400, 100, ProgressBarStyle.Marquee, False, "Scanning, please wait...", 0, aContent.Length, False)
		prg.Start()

		While i < aContent.Length

			If aContent(i).Trim.StartsWith("[") AndAlso Not aContent(i).Trim.ToLower = "[folder_settings]" Then
				Dim sCategory As String = aContent(i).Replace("[", "").Replace("]", "").Trim

				If sCategory = "" Then
					i += 1
					Continue While
				End If

				i += 1

				Dim al_RomNames As New ArrayList

				While i < aContent.Length
					Dim sRomName As String = aContent(i)

						If sRomName.Trim.StartsWith("[") Then
							Exit While
						End If

						If sRomName.Trim.Length > 0 Then
							If Not sRomName.StartsWith("#") AndAlso Not sRomName.StartsWith(";") Then
								al_RomNames.Add(sRomName.Trim)
							End If
						End If

						i += 1
					End While

				prg.Close()

				If al_RomNames.Count > 0 Then
					Dim InExpr As String = "'" & String.Join("', '", CType(al_RomNames.ToArray(GetType(String)), String())) & "'"
					Dim sSQL As String = "SELECT group_concat(id_Emu_Games, ',') FROM tbl_Emu_Games WHERE InnerFile IN(" & InExpr & ")"
					Dim id_Emu_Games As Object = DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, sSQL)

					If TC.NZ(id_Emu_Games, "").Length > 0 Then
						Dim id_Emu_Games_Int As Integer() = Array.ConvertAll(id_Emu_Games.Split(","), New Converter(Of String, Integer)(AddressOf ConvertToInteger))

						Using frm As New frm_Emu_Game_Edit(id_Emu_Games_Int, Alphaleonis.Win32.Filesystem.Path.GetFileName(iniFile) & " | " & sCategory & " (" & id_Emu_Games_Int.Length & " Roms)", sCategory, chb_AutoImport.Checked)
							frm.ShowDialog(Me)
						End Using

					End If
				Else
					MKDXHelper.MessageBox(sCategory & " does not contain any roms", "Empty ini Folder", MessageBoxButtons.OK, MessageBoxIcon.Information)
				End If

				prg = New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Nothing), 400, 100, ProgressBarStyle.Marquee, False, "Scanning, please wait...", 0, aContent.Length, False)
				prg.Start()
			Else
				i += 1
			End If
		End While

		prg.Close()
	End Sub

	Public Function ConvertToInteger(ByVal input As String) As Integer
		Dim output As Integer = 0

		Integer.TryParse(input, output)

		Return output
	End Function

	''' <summary>
	''' Import Ini File where the categories are assigned to each M.A.M.E. Rom per line
	''' </summary>
	''' <param name="aContent">Array of the ini File content</param>
	''' <param name="iniFile">Full path of the ini File</param>
	''' <remarks></remarks>
	Private Sub Import_Ini_File_Assignmentbased(ByRef aContent As String(), ByVal iniFile As String)
		Dim i As Integer = 0

		Dim al_Mame_Category_Roms As New ArrayList

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(tran), 400, 100, ProgressBarStyle.Continuous, False, "Scanning line {0} of {1}, please wait...", 0, aContent.Length, False)
			prg.Start()

			While i < aContent.Length
				prg.IncreaseCurrentValue()

				If aContent(i).Contains("=") AndAlso Not aContent(i).Trim.StartsWith("[") AndAlso Not aContent(i).Trim.StartsWith("#") AndAlso Not aContent(i).Trim.StartsWith(";") Then
					Dim matches As System.Text.RegularExpressions.MatchCollection = MKNetLib.cls_MKRegex.GetMatches(aContent(i).Trim, "(.*?)=(.*)")

					If matches.Count = 1 Then
						Dim sCategory As String = matches(0).Groups(2).Captures(0).Value.Trim
						Dim sRom As String = matches(0).Groups(1).Captures(0).Value

						If sCategory.Length > 0 AndAlso sRom.Length > 0 Then
							Dim bFound As Boolean = False

							For Each catrom As cls_Mame_Category_Roms In al_Mame_Category_Roms
								If catrom.Category = sCategory Then
									catrom.RomNames.Add(sRom)
									bFound = True
									Exit For
								End If
							Next

							If Not bFound Then
								Dim catrom As New cls_Mame_Category_Roms
								catrom.Category = sCategory
								catrom.RomNames = New ArrayList()
								catrom.RomNames.Add(sRom)
								al_Mame_Category_Roms.Add(catrom)
							End If
						End If
					End If
				End If

				i += 1
			End While

			tran.Commit()

			prg.Close()
		End Using


		For Each catroms As cls_Mame_Category_Roms In al_Mame_Category_Roms
			If catroms.RomNames.Count > 0 Then
				Dim InExpr As String = "'" & String.Join("', '", CType(catroms.RomNames.ToArray(GetType(String)), String())) & "'"
				Dim sSQL As String = "SELECT group_concat(id_Emu_Games, ',') FROM tbl_Emu_Games WHERE InnerFile IN(" & InExpr & ")"
				Dim id_Emu_Games As Object = DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, sSQL)

				If TC.NZ(id_Emu_Games, "").Length > 0 Then
					Dim id_Emu_Games_Int As Integer() = Array.ConvertAll(id_Emu_Games.Split(","), New Converter(Of String, Integer)(AddressOf ConvertToInteger))

					Using frm As New frm_Emu_Game_Edit(id_Emu_Games_Int, Alphaleonis.Win32.Filesystem.Path.GetFileName(iniFile) & " | " & catroms.Category & " (" & id_Emu_Games_Int.Length & " Roms)", catroms.Category, chb_AutoImport.Checked)
						frm.ShowDialog(Me)
					End Using

				End If
			Else
				MKDXHelper.MessageBox(catroms.Category & " does not contain any roms", "Empty ini Folder", MessageBoxButtons.OK, MessageBoxIcon.Information)
			End If
		Next
	End Sub

	Private Sub btn_ini_Folder_Import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ini_Folder_Import.Click
		Dim iniFile As String = MKNetLib.cls_MKFileSupport.OpenFileDialog("Open ini File", "ini Files (*.ini)|*.ini", ParentForm:=Me)

		'check if ini is folder-based or assignment ("=") based
		If Alphaleonis.Win32.Filesystem.File.Exists(iniFile) Then
			Dim sContent = Alphaleonis.Win32.Filesystem.File.ReadAllText(iniFile)
			Dim aContent As String() = sContent.Split(ControlChars.CrLf)

			Dim iAssignmentCounter As Integer = 0

			'Check in the first 25 rows if "=" can be found in at least 5 of them -> it isn't folder-based ini then
			For i As Integer = 0 To Math.Min(25, aContent.Length - 1)
				If Not aContent(i).StartsWith("[") AndAlso Not aContent(i).StartsWith("#") AndAlso Not aContent(i).StartsWith("#") Then
					If aContent(i).Contains("=") Then iAssignmentCounter += 1
				End If
			Next

			Dim bFolderbased As Boolean = True

			If iAssignmentCounter > 4 Then bFolderbased = False

			If bFolderbased Then
				Import_Ini_File_Folderbased(aContent, iniFile)
			Else
				Import_Ini_File_Assignmentbased(aContent, iniFile)
			End If

			MKDXHelper.MessageBox("The import completed.", "Import completed", MessageBoxButtons.OK, MessageBoxIcon.Information)

			Me._DialogResult = Windows.Forms.DialogResult.OK
		End If
	End Sub



	Private Sub btn_history_dat_www_Click(sender As Object, e As EventArgs) Handles btn_history_dat_www.Click
		cls_Globals.OpenURL("http://www.arcade-history.com/?page=download")
	End Sub

	Private Sub btn_mameinfo_dat_www_Click(sender As Object, e As EventArgs) Handles btn_mameinfo_dat_www.Click
		cls_Globals.OpenURL("http://mameinfo.mameworld.info/")
	End Sub

	Private Sub btn_gameinit_dat_www_Click(sender As Object, e As EventArgs) Handles btn_gameinit_dat_www.Click
		cls_Globals.OpenURL("http://www.progettosnaps.net/gameinit")
	End Sub

	Private Sub btn_Filter_ini_www_Click(sender As Object, e As EventArgs) Handles btn_Filter_ini_www.Click
		cls_Globals.OpenURL("http://www.progettosnaps.net/renameset/")
	End Sub

	Private Sub btn_Category_ini_www_Click(sender As Object, e As EventArgs) Handles btn_Category_ini_www.Click
		cls_Globals.OpenURL("http://nplayers.arcadebelgium.be/")
		cls_Globals.OpenURL("http://www.progettoemma.net/?catlist")
	End Sub
End Class