Public Class frm_Tag_Parser_Edit
	Private Shared _Regions As String() = {"NTSC", "PAL", "World", "Europe", "USA", "Australia", "Japan", "Korea", "China", "Asia", "Brazil", "Canada", "France", "Germany", "Hong Kong", "Italy", "Netherlands", "Spain", "Sweden", "Taiwan", "Russia"}

	'Private Shared _Languages As String() = {"En", "Ja", "Fr", "De", "Es", "It", "Nl", "Pt", "Sv", "No", "Da", "Fi", "Zh", "Ko", "Pl", "Hu", "Gr"}
	'Private Shared _Languages_Alt As String() = {"Eng", "Jp", "Fre", "Ger", "Spa", "Ita", "Dut", "(Pt)", "(Sv)", "(No)", "(Da)", "(Fi)", "(Zh)", "(Ko)", "(Pl)", "(Hu)", "(Gr)"}

	Private Shared _Languages As String(,) = {
			{"En", "Eng"},
			{"Ja", "Jp"},
			{"Fr", "Fre"},
			{"De", "Ger"},
			{"Es", "Spa"},
			{"It", "Ita"},
			{"Nl", "Dut"},
			{"Pt", "__"},
			{"Sv", "Se"},
			{"No", "__"},
			{"Da", "__"},
			{"Fi", "__"},
			{"Zh", "Cn"},
			{"Ko", "Kr"},
			{"Pl", "__"},
			{"Hu", "__"},
			{"Gr", "__"},
			{"Ar", "__"},
			{"Be", "__"},
			{"Cz", "Cs"},
			{"Sl", "__"},
			{"Sr", "__"},
			{"Ru", "__"}
		}

	Private Shared _Attributes As String() = {"Year", "Version", "Alt", "Trainer", "Translation", "Hack", "Bios", "Prototype", "Alpha", "Beta", "Sample", "Kiosk", "Unlicensed", "Fixed", "Pirated", "Good", "Bad", "Overdump", "PublicDomain"}

	Private _DoContentAnalysis As Boolean = False
	Private _Path As String
	Private _Files As String()
	Private _Contents As New Dictionary(Of String, String)

	Private _Allowed_Extensions As ArrayList = Nothing

	Private _bbi_Set_Additional_Note_Caption As String
	Private _bbi_Remove_Additional_Note_Caption As String

	Private _bbi_Set_Group_Criteria_Caption As String
	Private _bbi_Remove_Group_Criteria_Caption As String
	Private _bbi_Set_Publisher_Caption As String
	Private _bbi_Remove_Publisher_Caption As String

	Private _MultiVolume As Boolean = False

	Private _tran As SQLite.SQLiteTransaction = Nothing

	Public Sub New(ByRef tran As SQLite.SQLiteTransaction)
		InitializeComponent()

		Me._tran = tran

		Fill_Tag_Parser_Volumes(Me.DS_ML.ttb_Tag_Parser_Volumes)

		barmng.SetPopupContextMenu(grd_Tag_Parser, popmnu_TagParser)
	End Sub

	''' <summary>
	''' Contructor
	''' </summary>
	''' <param name="Path">Path to directory or single file</param>
	''' <param name="Allowed_Extensions">Allowed Extensions for filtering</param>
	''' <remarks></remarks>
	Public Sub New(ByRef tran As SQLite.SQLiteTransaction, ByVal Path As String, Optional ByVal Allowed_Extensions As ArrayList = Nothing, Optional ByVal MultiVolume As Boolean = False)
		Me.New(tran)

		_Path = Path

		Init(Allowed_Extensions, MultiVolume)
	End Sub

	''' <summary>
	''' Constructor
	''' </summary>
	''' <param name="Files">File Paths in String Array</param>
	''' <param name="Allowed_Extensions">Allowed Extensions for filtering"</param>
	''' <remarks></remarks>
	Public Sub New(ByRef tran As SQLite.SQLiteTransaction, ByVal Files As String(), Optional ByVal Allowed_Extensions As ArrayList = Nothing, Optional ByVal MultiVolume As Boolean = False)
		Me.New(tran)

		_Files = Files

		Init(Allowed_Extensions, MultiVolume)
	End Sub

	Public Sub Init(ByVal Allowed_Extensions As ArrayList, ByVal MultiVolume As Boolean)
		_DoContentAnalysis = True

		_Allowed_Extensions = Allowed_Extensions

		_bbi_Remove_Additional_Note_Caption = bbi_Remove_Additional_Note.Caption
		_bbi_Set_Additional_Note_Caption = bbi_Set_Additional_Note.Caption

		_bbi_Remove_Group_Criteria_Caption = bbi_Remove_Group_Criteria.Caption
		_bbi_Set_Group_Criteria_Caption = bbi_Set_Group_Criteria.Caption

		_bbi_Remove_Publisher_Caption = bbi_Remove_Publisher.Caption
		_bbi_Set_Publisher_Caption = bbi_Set_Publisher.Caption

		Me._MultiVolume = MultiVolume
		Me.gb_MultiVolume.Visible = Me._MultiVolume

		Me.bbi_Set_Group_Criteria.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
		Me.bbi_Remove_Group_Criteria.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

		If Me._MultiVolume Then
			Me.bbi_Set_Group_Criteria.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
			Me.bbi_Remove_Group_Criteria.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
		End If
	End Sub


	Private Sub ReadAllowedExtensions(ByVal Allowed_Extensions As String)
		For Each ext As String In Allowed_Extensions.Split(";")
			If Not TC.IsNullNothingOrEmpty(ext) Then
				_Allowed_Extensions.Add(ext.ToLower.Replace(".", ""))
			End If
		Next
	End Sub

	Public Shared Sub Fill_Tag_Parser_Volumes(ByRef dt As DS_ML.ttb_Tag_Parser_VolumesDataTable)
		'Dim first_row As DS_ML.ttb_Tag_Parser_VolumesRow = dt.NewRow
		'first_row.id_Tag_Parser_Volumes = Convert.ToInt64(0)
		'first_row.DisplayText = "Not a volume"
		'dt.Rows.Add(first_row)

		For i As Integer = 1 To 99
			Dim row As DS_ML.ttb_Tag_Parser_VolumesRow = dt.NewRow
			row.id_Tag_Parser_Volumes = Convert.ToInt64(i)
			row.DisplayText = "Disc/Volume " & IIf(i < 10, "0", "") & i
			dt.Rows.Add(row)
		Next
	End Sub

	''' <summary>
	''' Analyze all tags in _Contents and either create or fetch data from/for database
	''' </summary>
	''' <remarks></remarks>
	Private Sub Analyze_Contents()
		Dim tran As SQLite.SQLiteTransaction

		If _tran IsNot Nothing Then tran = _tran Else tran = cls_Globals.Conn.BeginTransaction

		Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(tran), 400, 60, ProgressBarStyle.Blocks, False, "Analyzing tag {0} of {1}", 0, _Contents.Count, False)
		prg.Start()

		'SPEEDUP
		Dim dt_Tags_Have As DataTable = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT id_Tag_Parser, Content FROM tbl_Tag_Parser", Nothing, tran)

		Dim dict_Tags_Have As New Dictionary(Of String, Long)

		For Each row_Tag_Have As DataRow In dt_Tags_Have.Rows
			If Not dict_Tags_Have.ContainsKey(row_Tag_Have("Content")) Then
				dict_Tags_Have.Add(row_Tag_Have("Content"), row_Tag_Have("id_Tag_Parser"))
			End If
		Next

		Dim dt_Rombase_Tags_Have As DataTable = DataAccess.FireProcedureReturnDT(tran.Connection, 0, False, "SELECT id_Rombase_Tag_Parser, Content FROM rombase.tbl_Rombase_Tag_Parser", Nothing, tran)

		Dim dict_Rombase_Tags_Have As New Dictionary(Of String, Long)

		For Each row_Tag_Have As DataRow In dt_Rombase_Tags_Have.Rows
			If Not dict_Rombase_Tags_Have.ContainsKey(row_Tag_Have("Content")) Then
				dict_Rombase_Tags_Have.Add(row_Tag_Have("Content"), row_Tag_Have("id_Rombase_Tag_Parser"))
			End If
		Next

		Dim id_Tag_Parser As Long = 0

		For Each sContent As String In _Contents.Keys
			prg.IncreaseCurrentValue()

			id_Tag_Parser = 0
			id_Rombase_Tag_Parser = 0

			'id_Tag_Parser = DS_ML.Select_tbl_Tag_Parser(tran, sContent)

			'SPEEDUP
			If dict_Tags_Have.ContainsKey(sContent) Then
				id_Tag_Parser = dict_Tags_Have(sContent)
			ElseIf dict_Rombase_Tags_Have.ContainsKey(sContent) Then
				id_Rombase_Tag_Parser = dict_Rombase_Tags_Have(sContent)
			End If

			If id_Tag_Parser > 0 Then
				'Entry is already available - load from database
				If _Contents(sContent).Length > 0 Then
					'Fill but replace Found_In with the current one
					DS_ML.Fill_tbl_Tag_Parser(tran, DS_ML.tbl_Tag_Parser, id_Tag_Parser, _Contents(sContent))
				Else
					DS_ML.Fill_tbl_Tag_Parser(tran, DS_ML.tbl_Tag_Parser, id_Tag_Parser)
				End If
			ElseIf id_Rombase_Tag_Parser > 0 Then
				'Found an entry in Rombase Tag Parser - load it
				If _Contents(sContent).Length > 0 Then
					'Fill but replace Found_In with the current one
					DS_ML.Fill_tbl_Rombase_Tag_Parser(tran, DS_ML.tbl_Tag_Parser, id_Rombase_Tag_Parser, _Contents(sContent))
				Else
					DS_ML.Fill_tbl_Rombase_Tag_Parser(tran, DS_ML.tbl_Tag_Parser, id_Rombase_Tag_Parser)
				End If
			Else
				'No Entry found - add new
				Dim row As DS_ML.tbl_Tag_ParserRow = DS_ML.tbl_Tag_Parser.NewRow
				row("Content") = sContent
				row("Apply") = True

				If _Contents(sContent).Length > 0 Then
					row("Found_In") = _Contents(sContent)
				End If

				row("MV_Group_Criteria") = True

				If sContent.ToLower = "[bios]" OrElse sContent.ToLower = "(bios)" Then
					row("Bios") = True
				End If

				'If sContent.ToLower.Contains("hack") Then
				If MKNetLib.cls_MKRegex.IsMatch(sContent, "(\[|\(|\s)(h|H)ack(\s|\)|\])") OrElse MKNetLib.cls_MKRegex.IsMatch(sContent, "(\[|\()h\s") Then
					row("Hack") = True
				End If

				If MKNetLib.cls_MKRegex.IsMatch(sContent, "(\[|\(|\s)19(7|8|9)\d") OrElse MKNetLib.cls_MKRegex.IsMatch(sContent, "(\[|\(|\s)20\d\d") OrElse MKNetLib.cls_MKRegex.IsMatch(sContent, "\d\d\d\d\d\d") Then
					row("Year") = True
				End If

				If MKNetLib.cls_MKRegex.IsMatch(sContent, "v\d+\.\d+") Then
					row("Version") = True
				End If

				If sContent.ToLower = "(proto)" OrElse sContent.ToLower = "(prototype)" Then
					row("Prototype") = True
				End If

				If sContent.ToLower = "(alpha)" Then
					row("Alpha") = True
				End If

				If sContent.ToLower = "(beta)" Then
					row("Beta") = True
				End If

				'If sContent.Contains("[T-") OrElse sContent.Contains("+T-") OrElse sContent.Contains("T+") Then
				If MKNetLib.cls_MKRegex.IsMatch(sContent, "(\[|\()tr\s") OrElse MKNetLib.cls_MKRegex.IsMatch(sContent, "(\[|\()(t|T)\+") OrElse MKNetLib.cls_MKRegex.IsMatch(sContent, "\+(t|T)\-") Then
					row("Translation") = True
				End If

				If MKNetLib.cls_MKRegex.IsMatch(sContent, "(\[|\(|\s)t\s*\+\s*\d") Then
					row("Trainer") = True
				End If

				If Me._MultiVolume Then
					If MKNetLib.cls_MKRegex.IsMatch(sContent, "(\(|\[)(S|s)ide\s*(A|B|a|b|1|2)(\)|\])") Then
						Try
							If MKNetLib.cls_MKRegex.GetMatches(sContent, "(\(|\[)(S|s)ide\s*(A|B|a|b|1|2)(\)|\])")(0).Groups(2).Value = "A" OrElse MKNetLib.cls_MKRegex.GetMatches(sContent, "(\(|\[)(S|s)ide\s*(A|B|a|b|1|2)(\)|\])")(0).Groups(2).Value = "a" OrElse MKNetLib.cls_MKRegex.GetMatches(sContent, "(\(|\[)(S|s)ide\s*(A|B|a|b|1|2)(\)|\])")(0).Groups(2).Value = "1" Then
								row("MV_Volume_Number") = 1L
								row("MV_Group_Criteria") = False
							End If
							If MKNetLib.cls_MKRegex.GetMatches(sContent, "(\(|\[)(S|s)ide\s*(A|B|a|b|1|2)(\)|\])")(0).Groups(2).Value = "B" OrElse MKNetLib.cls_MKRegex.GetMatches(sContent, "(\(|\[)(S|s)ide\s*(A|B|a|b|1|2)(\)|\])")(0).Groups(2).Value = "b" OrElse MKNetLib.cls_MKRegex.GetMatches(sContent, "(\(|\[)(S|s)ide\s*(A|B|a|b|1|2)(\)|\])")(0).Groups(2).Value = "2" Then
								row("MV_Volume_Number") = 2L
								row("MV_Group_Criteria") = False
							End If
						Catch ex As Exception

						End Try
					End If

					If MKNetLib.cls_MKRegex.IsMatch(sContent, "(\(|\[)(disc|Disc|disk|Disk|CD|cd)\s*(\d*)") Then
						Try
							Dim match As String = MKNetLib.cls_MKRegex.GetMatches(sContent, "(\(|\[)(disc|Disc|disk|Disk|CD|cd)\s*(\d*)")(0).Groups(3).Value
							If IsNumeric(match) Then
								Dim iVolume As Int64 = Convert.ToInt64(match)

								If MKNetLib.cls_MKRegex.IsMatch(sContent, "(S|s)ide\s*(A|B|a|b|1|2)") Then
									Try
										Dim rx As String = "(S|s)ide\s*(A|B|a|b|1|2)"
										Dim rx_grp As Integer = 2

										If MKNetLib.cls_MKRegex.GetMatches(sContent, rx)(0).Groups(rx_grp).Value = "A" OrElse MKNetLib.cls_MKRegex.GetMatches(sContent, rx)(0).Groups(rx_grp).Value = "a" OrElse MKNetLib.cls_MKRegex.GetMatches(sContent, rx)(0).Groups(rx_grp).Value = "1" Then
											iVolume = iVolume * 2 - 1
										End If
										If MKNetLib.cls_MKRegex.GetMatches(sContent, rx)(0).Groups(rx_grp).Value = "B" OrElse MKNetLib.cls_MKRegex.GetMatches(sContent, rx)(0).Groups(rx_grp).Value = "b" OrElse MKNetLib.cls_MKRegex.GetMatches(sContent, rx)(0).Groups(rx_grp).Value = "2" Then
											iVolume = iVolume * 2
										End If
									Catch ex As Exception

									End Try
								End If

								If iVolume > 0 Then
									row("MV_Volume_Number") = iVolume
								End If

								row("MV_Group_Criteria") = False
							End If
						Catch ex As Exception

						End Try
					End If
				End If

				If sContent.ToLower.Contains("(alt ") Then
					row("Alt") = True
				End If

				If sContent.ToLower = "(unl)" Then
					row("Unlicensed") = True
				End If

				If sContent = "[!]" Then
					row("Good") = True
				End If

				If sContent = "[b]" Then
					row("Bad") = True
				End If

				If MKNetLib.cls_MKRegex.IsMatch(sContent, "(\(|\[|\s)Fix") OrElse MKNetLib.cls_MKRegex.IsMatch(sContent, "(\(|\[)f\s") Then
					row("Fixed") = True
				End If

				If sContent.ToLower = "(pirate)" Then
					row("Pirated") = True
				End If

				If sContent.ToLower = "(sample)" Then
					row("Sample") = True
				End If

				If sContent.StartsWith("~") Then
					row("Note_HighPriority") = True
				End If

				'Languages
				For iLng As Integer = 0 To (_Languages.Length / 2) - 1
					Dim sID As String = _Languages(iLng, 0)

					For iLngString As Integer = 0 To 1
						Dim sContent_Lower = sContent.ToLower
						Dim sLanguage_Lower = _Languages(iLng, iLngString).ToLower
						If sContent_Lower.Contains("(" & sLanguage_Lower & ")") OrElse sContent_Lower.Contains("(" & sLanguage_Lower & ",") OrElse sContent_Lower.Contains("," & sLanguage_Lower & ",") OrElse sContent_Lower.Contains("," & sLanguage_Lower & ")") OrElse sContent_Lower.Contains("(" & sLanguage_Lower & "+") OrElse sContent_Lower.Contains("," & sLanguage_Lower & "+") OrElse sContent_Lower.Contains("+" & sLanguage_Lower & ",") OrElse sContent_Lower.Contains("+" & sLanguage_Lower & ")") OrElse sContent_Lower.Contains("(" & sLanguage_Lower & "-") OrElse sContent_Lower.Contains("-" & sLanguage_Lower & ")") OrElse sContent_Lower.Contains("-" & sLanguage_Lower & "-") Then
							row(sID) = True
						End If
					Next
				Next

				For Each sRegion As String In _Regions
					If sContent.Contains("(" & sRegion & ")") OrElse sContent.Contains("(" & sRegion & ",") OrElse sContent.Contains(", " & sRegion & ",") OrElse sContent.Contains(", " & sRegion & ")") Then
						row(sRegion.Replace(" ", "")) = True
					End If
				Next

				DS_ML.tbl_Tag_Parser.Rows.Add(row)
			End If
		Next

		prg.Close()

		If _tran Is Nothing Then tran.Commit()
	End Sub

	''' <summary>
	''' Extract Content from filenames and put the tags into al_Contents
	''' </summary>
	''' <param name="FileName"></param>
	''' <param name="dict_Contents">Optional Dictionary which get the tags and filenames appended</param>
	''' <returns>ArrayList of the tags within the filename in their order</returns>
	''' <remarks></remarks>
	Public Shared Function Extract_Content_From_FileName(ByVal FileName As String, Optional ByRef dict_Contents As Dictionary(Of String, String) = Nothing, Optional ByRef al_Allowed_Extensions As ArrayList = Nothing) As Dictionary(Of String, String)
		If al_Allowed_Extensions IsNot Nothing AndAlso al_Allowed_Extensions.Count > 0 AndAlso Not al_Allowed_Extensions.Contains(Alphaleonis.Win32.Filesystem.Path.GetExtension(FileName).ToLower.Replace(".", "")) Then Return New Dictionary(Of String, String)

		If dict_Contents Is Nothing Then dict_Contents = New Dictionary(Of String, String)

		'Match tags in round brackets "()"
		For Each match As System.Text.RegularExpressions.Match In MKNetLib.cls_MKRegex.GetMatches(FileName, "(\(.*?\))")
			Dim sValue As String = match.Value

			'Exclude all tags in round brackets that are within square braces, e.g. [Role Playing (RPG)]
			Dim bExcluded As Boolean = False
			For Each match_exclude As System.Text.RegularExpressions.Match In MKNetLib.cls_MKRegex.GetMatches(FileName, "\[[^\[\]]*(\(.*?\))[^\[\]]*\]")
				If sValue = match_exclude.Groups(1).Value Then
					bExcluded = True
				End If
			Next

			If Not bExcluded AndAlso Not dict_Contents.ContainsKey(sValue) Then
				dict_Contents.Add(sValue, FileName)
			End If
		Next

		'Match tags in square brackets "[]"
		For Each match As System.Text.RegularExpressions.Match In MKNetLib.cls_MKRegex.GetMatches(FileName, "(\[.*?\])")
			Dim sValue As String = match.Value

			'Exclude all tags in square brackets that are within round braces, e.g. (Role Playing [RPG])
			Dim bExcluded As Boolean = False
			For Each match_exclude As System.Text.RegularExpressions.Match In MKNetLib.cls_MKRegex.GetMatches(FileName, "\([^\(\)]*(\[.*?\])[^\(\)]*\)")
				If sValue = match_exclude.Groups(1).Value Then
					bExcluded = True
				End If
			Next

			If Not bExcluded AndAlso Not dict_Contents.ContainsKey(sValue) Then
				dict_Contents.Add(sValue, FileName)
			End If
		Next

		'Match alternate titles
		For Each match As System.Text.RegularExpressions.Match In MKNetLib.cls_MKRegex.GetMatches(FileName, "\~(.*?)[\(\[]")
			Dim sValue As String = match.Groups(1).Value.Trim

			If sValue.Length > 0 AndAlso Not dict_Contents.ContainsKey("~" & sValue) Then
				dict_Contents.Add("~" & sValue, FileName)
			End If
		Next

		'Match Version info that is NOT inside a bracket tag
		For Each match As System.Text.RegularExpressions.Match In MKNetLib.cls_MKRegex.GetMatches(FileName, "[^\(\[]\s+(v\d[\w.]+)")
			Dim sValue As String = match.Groups(1).Value

			If Not dict_Contents.ContainsKey(sValue) Then
				dict_Contents.Add(sValue, FileName)
			End If
		Next

		'Match Version info that is NOT inside a bracket tag and especially starts with a letter
		For Each match As System.Text.RegularExpressions.Match In MKNetLib.cls_MKRegex.GetMatches(FileName, "[^\(\[]\s+(v[a-zA-Z]+[.\d+]+)\s*?[\[\(]")
			Dim sValue As String = match.Groups(1).Value

			If Not dict_Contents.ContainsKey(sValue) Then
				dict_Contents.Add(sValue, FileName)
			End If
		Next

		Return dict_Contents
	End Function

	''' <summary>
	''' Extract content from a filename and put it into the dictionary
	''' </summary>
	''' <param name="fi"></param>
	''' <param name="dict_Contents"></param>
	''' <remarks></remarks>
	Private Sub Extract_Content_From_FileInfo(ByVal fi As Alphaleonis.Win32.Filesystem.FileInfo, ByRef dict_Contents As Dictionary(Of String, String))
		Dim archive As SharpCompress.Archive.IArchive = Nothing

		Try
			If frm_Rom_Manager.Is_Archive(fi.FullName) Then
				archive = SharpCompress.Archive.ArchiveFactory.Open(fi.FullName)
			End If
		Catch ex As Exception

		End Try

		If archive IsNot Nothing Then
			Dim sTmpDir As String = MKNetLib.cls_MKFileSupport.CreateTempDir("ml_")

			'one or more files in archive - extract and call AddGameFromFile with the extracted fileinfo
			For Each entry As SharpCompress.Archive.IArchiveEntry In archive.Entries
				If Not entry.IsDirectory Then
					'entry.FilePath
					Dim sFileName As String = Alphaleonis.Win32.Filesystem.Path.GetFileName(entry.FilePath)
					Extract_Content_From_FileName(sFileName, dict_Contents, _Allowed_Extensions)
				End If
			Next

			MKNetLib.cls_MKFileSupport.Delete_Directory(sTmpDir)
		Else
			'fi.Name
			Dim sFileName As String = Alphaleonis.Win32.Filesystem.Path.GetFileName(fi.FullName)
			Extract_Content_From_FileName(sFileName, dict_Contents, _Allowed_Extensions)
		End If
	End Sub

	Private Sub Extract_Content_From_Path(ByVal Path As String)
		Dim al_Contents As New Dictionary(Of String, String)

		If Alphaleonis.Win32.Filesystem.Directory.Exists(Path) Then
			Dim arrFiles As New ArrayList
			Dim fsrch As New MKNetLib.cls_MKFileSearch(New Alphaleonis.Win32.Filesystem.DirectoryInfo(Path))
			fsrch.Search(New Alphaleonis.Win32.Filesystem.DirectoryInfo(Path), "*.*")
			arrFiles.AddRange(fsrch.Files)

			Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Nothing), 400, 60, ProgressBarStyle.Blocks, False, "Extracting tags from file {0} of {1}", 0, arrFiles.Count, False)
			prg.Start()

			For Each fi As Alphaleonis.Win32.Filesystem.FileInfo In arrFiles
				prg.IncreaseCurrentValue()

				Extract_Content_From_FileInfo(fi, al_Contents)
			Next

			prg.Close()
		End If

		_Contents = al_Contents
	End Sub

	''' <summary>
	''' Result of the tag analysis
	''' </summary>
	''' <returns>False, when no tags have been found and the window will be closed, else True</returns>
	''' <remarks></remarks>
	Private Function Show_Analysis_Result() As Boolean
		iTotalTags = DS_ML.tbl_Tag_Parser.Rows.Count
		iNewTags = TC.NZ(DS_ML.tbl_Tag_Parser.Select("id_Tag_Parser < 0 AND id_Rombase_Tag_Parser IS NULL").Length, 0)

		If iTotalTags = 0 Then
			DevExpress.XtraEditors.XtraMessageBox.Show("No tags have been found, the window will be closed.", "Tag Analysis Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
			Return False
		End If

		If iNewTags = 0 Then
			DevExpress.XtraEditors.XtraMessageBox.Show("Out of " & iTotalTags & " tags, no new tags have been found. Please use the Tag Parser Settings window to review the settings.", "Tag Analysis Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
			Return True
		End If

		DevExpress.XtraEditors.XtraMessageBox.Show("Out of " & iTotalTags & " tags, " & iNewTags & " new " & IIf(iNewTags = 1, "tag has", "tags have") & " been found and will be displayed in bold. Please use the Tag Parser Settings window to adjust the settings.", "Tag Analysis Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
		Return True
	End Function

	Private Sub frm_Tag_Parser_Edit_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		If Me.DialogResult <> Windows.Forms.DialogResult.OK Then
			If DevExpress.XtraEditors.XtraMessageBox.Show("Do you really want to cancel the import process?", "Cancel", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
				e.Cancel = True
				Return
			End If
		End If
	End Sub

	Private Sub frm_Tag_Parser_Edit_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
		Me.lbl_Found_In_Left.Text = ""
		Me.lbl_Found_In_Middle.Text = ""
		Me.lbl_Found_In_Right.Text = ""

		'Content Analysis, fetch already known tags from database, put unknown only in datatable
		If _DoContentAnalysis Then
			If Alphaleonis.Win32.Filesystem.Directory.Exists(_Path) Then
				Extract_Content_From_Path(_Path)
			End If

			If _Files IsNot Nothing Then
				Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Me._tran), 400, 60, ProgressBarStyle.Blocks, False, "Extracting tags from file {0} of {1}", 0, _Files.Length, False)
				prg.Start()

				For Each sFile As String In _Files
					prg.IncreaseCurrentValue()
					Extract_Content_From_FileName(Alphaleonis.Win32.Filesystem.Path.GetFileName(sFile), _Contents, _Allowed_Extensions)
				Next

				prg.Close()
			End If

			Analyze_Contents()

			If Show_Analysis_Result() = False Then
				Me.DialogResult = Windows.Forms.DialogResult.OK
				Me.Close()
				Return
			End If
		End If
	End Sub

	Private Sub txb_Note_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txb_Note.DoubleClick, gv_Tag_Parser.DoubleClick
		'automatically write the content of the tag in the Textbox
		If BS_Tag_Parser.Current IsNot Nothing Then
			txb_Note.EditValue = MKNetLib.cls_MKStringSupport.Clean_Left(MKNetLib.cls_MKStringSupport.Clean_Left(MKNetLib.cls_MKStringSupport.Clean_Left(MKNetLib.cls_MKStringSupport.Clean_Right(MKNetLib.cls_MKStringSupport.Clean_Right(BS_Tag_Parser.Current("Content"), ")"), "]"), "("), "["), "~")
		End If
	End Sub

	Private Sub txb_Note_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txb_Note.EditValueChanged
		If txb_Note IsNot Nothing AndAlso TC.NZ(txb_Note.EditValue, "") = "" Then
			txb_Note.EditValue = DBNull.Value
		End If
	End Sub

	Private Sub gv_Tag_Parser_RowCellStyle(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gv_Tag_Parser.RowCellStyle
		Dim row As DataRow = gv_Tag_Parser.GetRow(e.RowHandle).Row
		If row("id_Tag_Parser") < 0 AndAlso TC.NZ(row("id_Rombase_Tag_Parser"), 0) <= 0 Then
			e.Appearance.Font = New System.Drawing.Font(e.Appearance.Font.Name, e.Appearance.Font.Size, FontStyle.Bold)
		End If
	End Sub

	Private Sub btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OK.Click
		If Me._MultiVolume AndAlso DevExpress.XtraEditors.XtraMessageBox.Show("The current platform has multiple volumes, did you check for each tag the possibility of multi-volume relevance (either if it denotes a specific disc/volume or needs to be considered for grouping) and wish to proceed?", "Multiple Volumes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
			Return
		End If

		BS_Tag_Parser.EndEdit()
		Dim dt_Changes As DataTable = DS_ML.tbl_Tag_Parser.GetChanges
		If dt_Changes IsNot Nothing OrElse DS_ML.tbl_Tag_Parser.Select("id_Tag_Parser < 0").Length > 0 Then
			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				If dt_Changes IsNot Nothing Then
					For Each row As DataRow In dt_Changes.Rows
						DS_ML.Upsert_tbl_Tag_Parser(tran, row)
					Next
				End If

				Dim rows_New_from_Rombase() As DataRow = DS_ML.tbl_Tag_Parser.Select("id_Tag_Parser < 0")
				For Each row As DataRow In rows_New_from_Rombase
					DS_ML.Upsert_tbl_Tag_Parser(tran, row)
				Next

				tran.Commit()
			End Using
		End If

		Me.DialogResult = Windows.Forms.DialogResult.OK
	End Sub

	Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
		Me.Close()
	End Sub

	Public Shared Sub Apply_Filename_Tags(ByRef tran As SQLite.SQLiteTransaction, ByRef row_emu_games As DS_ML.tbl_Emu_GamesRow, ByRef dt_Emu_Games_Languages As DataTable, ByRef dt_Emu_Games_Regions As DataTable, Optional ByRef al_Allowed_Extensions As ArrayList = Nothing, Optional ByVal MultiVolume As Boolean = False)
		'Clean up
		For Each col As String In _Attributes
			row_emu_games(col) = DBNull.Value
		Next

		'Cleanup dt_Emu_Games_Languages
		Dim rows_Emu_Games_Languages As DataRow() = dt_Emu_Games_Languages.Select("id_Emu_Games = " & row_emu_games("id_Emu_Games"))
		For Each row_Emu_Games_Languages In rows_Emu_Games_Languages
			dt_Emu_Games_Languages.Rows.Remove(row_Emu_Games_Languages)
		Next

		'Cleanup dt_Emu_Games_Regions
		Dim rows_Emu_Games_Regions As DataRow() = dt_Emu_Games_Regions.Select("id_Emu_Games = " & row_emu_games("id_Emu_Games"))
		For Each row_Emu_Games_Regions In rows_Emu_Games_Regions
			dt_Emu_Games_Regions.Rows.Remove(row_Emu_Games_Regions)
		Next

		row_emu_games("Note") = DBNull.Value

		Dim sFilteredName As String = ""
		Dim sCleanName As String = ""
		Dim sFileName As String = ""

		If TC.NZ(row_emu_games("InnerFile"), "") <> "" Then
			sFilteredName = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(row_emu_games("InnerFile"))
			sCleanName = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(row_emu_games("InnerFile"))
			sFileName = row_emu_games("InnerFile")
		ElseIf TC.NZ(row_emu_games("File"), "") <> "" Then
			sFilteredName = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(row_emu_games("File"))
			sCleanName = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(row_emu_games("File"))
			sFileName = row_emu_games("File")
		End If

		'Find tags
		Dim dict_Contents As Dictionary(Of String, String) = Extract_Content_From_FileName(sFileName, Nothing, al_Allowed_Extensions)

		'Apply rules
		Dim dt As New DS_ML.tbl_Tag_ParserDataTable

		For Each sContent As String In dict_Contents.Keys
			Try
				Dim id_Tag_Parser As Integer = TC.NZ(DS_ML.Select_tbl_Tag_Parser(tran, sContent), 0)

				If id_Tag_Parser <= 0 Then
					Continue For
				End If

				dt.Clear()

				DS_ML.Fill_tbl_Tag_Parser(tran, dt, id_Tag_Parser)

				If dt.Rows.Count = 0 Then
					Continue For
				End If

				Dim row_tag_parser As DS_ML.tbl_Tag_ParserRow = dt.Rows(0)

				'Always check for Group Criteria
				If TC.NZ(row_tag_parser("MV_Group_Criteria"), True) = False Then
					sFilteredName = sFilteredName.Replace(sContent, "")
				End If

				sCleanName = sCleanName.Replace(sContent, "").Trim  'Remove all tags from CleanName (anything to be displayed in brackets will be chosen by the user as Additional Note)

				If TC.NZ(row_tag_parser("Apply"), False) = False Then
					Continue For
				End If

				'Year
				If TC.NZ(row_tag_parser("Year"), False) = True Then
					Dim year As String = ""

					If MKNetLib.cls_MKRegex.IsMatch(sContent, "\d\d\d\d\d\d\d\d") Then
						year = MKNetLib.cls_MKRegex.GetMatches(sContent, "\d\d\d\d")(0).Value
					ElseIf MKNetLib.cls_MKRegex.IsMatch(sContent, "\d\d\d\d\d\d") Then
						Dim mcoll As System.Text.RegularExpressions.MatchCollection = MKNetLib.cls_MKRegex.GetMatches(sContent, "(\d\d)(\d\d)(\d\d)")

						Try
							If CInt(mcoll(0).Groups(2).Value) > 0 AndAlso CInt(mcoll(0).Groups(2).Value) < 13 AndAlso CInt(mcoll(0).Groups(3).Value) > 0 AndAlso CInt(mcoll(0).Groups(3).Value) < 32 Then
								If CInt(mcoll(0).Groups(1).Value) > 89 Then
									year = "19" & mcoll(0).Groups(1).Value
								Else
									year = "20" & mcoll(0).Groups(1).Value
								End If

							End If
						Catch ex As Exception

						End Try

					ElseIf MKNetLib.cls_MKRegex.IsMatch(sContent, "19\d\d") Then
						year = MKNetLib.cls_MKRegex.GetMatches(sContent, "19\d\d")(0).Value
					ElseIf MKNetLib.cls_MKRegex.IsMatch(sContent, "20\d\d") Then
						year = MKNetLib.cls_MKRegex.GetMatches(sContent, "20\d\d")(0).Value
					End If

					'Cleanup possible false years
					If year.StartsWith("19") OrElse year.StartsWith("20") Then
						row_emu_games("Year") = year
					End If
				End If

				'Version
				If TC.NZ(row_tag_parser("Version"), False) = True Then
					Dim sVersion As String = sContent
					sVersion = MKNetLib.cls_MKStringSupport.Clean_Left(sVersion.Trim, "(").Trim
					sVersion = MKNetLib.cls_MKStringSupport.Clean_Left(sVersion.Trim, "[").Trim
					sVersion = MKNetLib.cls_MKStringSupport.Clean_Right(sVersion.Trim, ")").Trim
					sVersion = MKNetLib.cls_MKStringSupport.Clean_Right(sVersion.Trim, "]").Trim
					sVersion = MKNetLib.cls_MKStringSupport.Clean_Left(sVersion.Trim, "v").Trim
					row_emu_games("Version") = sVersion
				End If

				'Alt
				If TC.NZ(row_tag_parser("Alt"), False) = True Then row_emu_games("Alt") = True

				'Trainer
				If TC.NZ(row_tag_parser("Trainer"), False) = True Then row_emu_games("Trainer") = True

				'Translation
				If TC.NZ(row_tag_parser("Translation"), False) = True Then row_emu_games("Translation") = True

				'Hack
				If TC.NZ(row_tag_parser("Hack"), False) = True Then row_emu_games("Hack") = True

				'Bios
				If TC.NZ(row_tag_parser("Bios"), False) = True Then row_emu_games("Bios") = True

				'Prototype
				If TC.NZ(row_tag_parser("Prototype"), False) = True Then row_emu_games("Prototype") = True

				'Alpha
				If TC.NZ(row_tag_parser("Alpha"), False) = True Then row_emu_games("Alpha") = True

				'Beta
				If TC.NZ(row_tag_parser("Beta"), False) = True Then row_emu_games("Beta") = True

				'Sample
				If TC.NZ(row_tag_parser("Sample"), False) = True Then row_emu_games("Sample") = True

				'Kiosk
				If TC.NZ(row_tag_parser("Kiosk"), False) = True Then row_emu_games("Kiosk") = True

				'Unlicensed
				If TC.NZ(row_tag_parser("Unlicensed"), False) = True Then row_emu_games("Unlicensed") = True

				'Fixed
				If TC.NZ(row_tag_parser("Fixed"), False) = True Then row_emu_games("Fixed") = True

				'Pirated
				If TC.NZ(row_tag_parser("Pirated"), False) = True Then row_emu_games("Pirated") = True

				'Good
				If TC.NZ(row_tag_parser("Good"), False) = True Then row_emu_games("Good") = True

				'Bad
				If TC.NZ(row_tag_parser("Bad"), False) = True Then row_emu_games("Bad") = True

				'Overdump
				If TC.NZ(row_tag_parser("Overdump"), False) = True Then row_emu_games("Overdump") = True

				'PublicDomain
				'If TC.NZ(row_tag_parser("PublicDomain"), False) = True Then row_emu_games("PublicDomain") = True

				'Volume Number
				If MultiVolume Then
					If TC.NZ(row_tag_parser("MV_Volume_Number"), 0) > 0 Then
						row_emu_games("Volume_Number") = row_tag_parser("MV_Volume_Number")
					End If
				End If

				'Languages
				For iLng As Integer = 0 To (_Languages.Length / 2) - 1
					Dim language As String = _Languages(iLng, 0)
					If TC.NZ(row_tag_parser(language), False) = True Then
						Dim id_Languages As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Languages FROM tbl_Languages WHERE Language_Short = " & TC.getSQLFormat(language), tran), 0)
						If id_Languages > 0 Then
							Dim row_Emu_Games_Languages As DataRow = dt_Emu_Games_Languages.NewRow
							row_Emu_Games_Languages("id_Languages") = id_Languages
							row_Emu_Games_Languages("id_Emu_Games") = row_emu_games("id_Emu_Games")
							dt_Emu_Games_Languages.Rows.Add(row_Emu_Games_Languages)
						End If
					End If
				Next

				'Regions
				For Each region As String In _Regions
					If TC.NZ(row_tag_parser(region.Replace(" ", "")), False) = True Then
						Dim id_Regions As Integer = TC.NZ(DataAccess.FireProcedureReturnScalar(tran.Connection, 0, "SELECT id_Regions FROM tbl_Regions WHERE Region = " & TC.getSQLFormat(region), tran), 0)
						If id_Regions > 0 Then
							Dim row_Emu_Games_Regions As DataRow = dt_Emu_Games_Regions.NewRow
							row_Emu_Games_Regions("id_Regions") = id_Regions
							row_Emu_Games_Regions("id_Emu_Games") = row_emu_games("id_Emu_Games")
							dt_Emu_Games_Regions.Rows.Add(row_Emu_Games_Regions)
						End If
					End If
				Next

				'Add additional information
				If TC.NZ(row_tag_parser("Note"), "").Length > 0 Then
					If TC.NZ(row_tag_parser("Note_HighPriority"), False) Then
						'Append left
						If TC.NZ(row_emu_games("Note"), "").Length > 0 Then
							row_emu_games("Note") = row_tag_parser("Note") & ", " & row_emu_games("Note")
						Else
							row_emu_games("Note") = row_tag_parser("Note")
						End If
					Else
						'Append right
						If TC.NZ(row_emu_games("Note"), "").Length > 0 Then
							row_emu_games("Note") &= ", " & row_tag_parser("Note")
						Else
							row_emu_games("Note") = row_tag_parser("Note")
						End If
					End If
				End If

				'Publisher
				If TC.NZ(row_tag_parser("Publisher"), False) = True Then
					Dim sPublisher As String = TC.NZ(row_tag_parser("Content"), "")
					sPublisher = MKNetLib.cls_MKStringSupport.Clean_Left(sPublisher, "(")
					sPublisher = MKNetLib.cls_MKStringSupport.Clean_Left(sPublisher, "[")
					sPublisher = MKNetLib.cls_MKStringSupport.Clean_Right(sPublisher, ")")
					sPublisher = MKNetLib.cls_MKStringSupport.Clean_Right(sPublisher, "]")
					row_emu_games("Publisher") = sPublisher.Trim
				End If
			Catch ex As Exception
				DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message)
			End Try
		Next

		While sFilteredName.Contains("  ")
			sFilteredName = sFilteredName.Replace("  ", " ")
		End While
		sFilteredName = Trim(sFilteredName)

		'Set filtered Name when in MultiVolume mode
		If MultiVolume Then
			row_emu_games("Filtered_Name") = sFilteredName
		End If
		row_emu_games("Name") = sCleanName
		'row_emu_games("Name_USR") = sCleanName	'This collides with Mobygames name
	End Sub

	Private Sub popmnu_TagParser_BeforePopup(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles popmnu_TagParser.BeforePopup
		If Not grd_Tag_Parser.Allow_Popup Then
			e.Cancel = True
			Return
		End If

		Dim iRowHandles As Integer() = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Tag_Parser)
		Dim iNumRows As Integer = iRowHandles.Length

		If iNumRows = 0 Then
			e.Cancel = True
			Return
		End If

		bbi_Remove_Additional_Note.Caption = _bbi_Remove_Additional_Note_Caption.Replace("%0%", iNumRows)
		bbi_Set_Additional_Note.Caption = _bbi_Set_Additional_Note_Caption.Replace("%0%", iNumRows)
		bbi_Remove_Group_Criteria.Caption = _bbi_Remove_Group_Criteria_Caption.Replace("%0%", iNumRows)
		bbi_Set_Group_Criteria.Caption = _bbi_Set_Group_Criteria_Caption.Replace("%0%", iNumRows)
		bbi_Remove_Publisher.Caption = _bbi_Remove_Publisher_Caption.Replace("%0%", iNumRows)
		bbi_Set_Publisher.Caption = _bbi_Set_Publisher_Caption.Replace("%0%", iNumRows)
	End Sub

	Private Sub bbi_Set_Additional_Note_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Set_Additional_Note.ItemClick
		Dim iRowHandles As Integer() = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Tag_Parser)
		Dim iNumRows As Integer = iRowHandles.Length

		If iNumRows <= 0 Then
			Return
		End If

		For Each iRowHandle As Integer In iRowHandles
			Dim row As DataRow = gv_Tag_Parser.GetRow(iRowHandle).Row
			Try
				row("Note") = MKNetLib.cls_MKStringSupport.Clean_Left(MKNetLib.cls_MKStringSupport.Clean_Left(MKNetLib.cls_MKStringSupport.Clean_Left(MKNetLib.cls_MKStringSupport.Clean_Right(MKNetLib.cls_MKStringSupport.Clean_Right(row("Content"), ")"), "]"), "("), "["), "~")
			Catch ex As Exception

			End Try
		Next
	End Sub

	Private Sub bbi_Remove_Additional_Note_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Remove_Additional_Note.ItemClick
		Dim iRowHandles As Integer() = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Tag_Parser)
		Dim iNumRows As Integer = iRowHandles.Length

		If iNumRows <= 0 Then
			Return
		End If

		For Each iRowHandle As Integer In iRowHandles
			Dim row As DataRow = gv_Tag_Parser.GetRow(iRowHandle).Row
			Try
				row("Note") = DBNull.Value
			Catch ex As Exception

			End Try
		Next
	End Sub

	Private Sub cmb_MV_Volume_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmb_MV_Volume.ButtonClick
		If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
			cmb_MV_Volume.EditValue = DBNull.Value
		End If
	End Sub

	Private _sem_chb_MV_Group_Criteria_EditValueChanged As Boolean = False

	Private Sub chb_MV_Group_Criteria_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chb_MV_Group_Criteria.EditValueChanged
		If _sem_chb_MV_Group_Criteria_EditValueChanged Then Return

		If TC.NZ(chb_MV_Group_Criteria.EditValue, False) = True Then
			cmb_MV_Volume.EditValue = DBNull.Value
		End If
	End Sub

	Private Sub cmb_MV_Volume_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_MV_Volume.EditValueChanged
		_sem_chb_MV_Group_Criteria_EditValueChanged = True

		If TC.NZ(cmb_MV_Volume.EditValue, 0) > 0 Then
			chb_MV_Group_Criteria.EditValue = False
		Else
			chb_MV_Group_Criteria.EditValue = True
		End If

		_sem_chb_MV_Group_Criteria_EditValueChanged = False
	End Sub

	Private Sub bbi_Set_Group_Criteria_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Set_Group_Criteria.ItemClick
		Dim iRowHandles As Integer() = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Tag_Parser)
		Dim iNumRows As Integer = iRowHandles.Length

		If iNumRows <= 0 Then
			Return
		End If

		For Each iRowHandle As Integer In iRowHandles
			Dim row As DataRow = gv_Tag_Parser.GetRow(iRowHandle).Row
			Try
				row("MV_Group_Criteria") = True
			Catch ex As Exception

			End Try
		Next
	End Sub

	Private Sub bbi_Remove_Group_Criteria_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Remove_Group_Criteria.ItemClick
		Dim iRowHandles As Integer() = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Tag_Parser)
		Dim iNumRows As Integer = iRowHandles.Length

		If iNumRows <= 0 Then
			Return
		End If

		For Each iRowHandle As Integer In iRowHandles
			Dim row As DataRow = gv_Tag_Parser.GetRow(iRowHandle).Row
			Try
				row("MV_Group_Criteria") = False
			Catch ex As Exception

			End Try
		Next
	End Sub

	Private Sub bbi_Set_Publisher_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Set_Publisher.ItemClick
		Dim iRowHandles As Integer() = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Tag_Parser)
		Dim iNumRows As Integer = iRowHandles.Length

		If iNumRows <= 0 Then
			Return
		End If

		For Each iRowHandle As Integer In iRowHandles
			Dim row As DataRow = gv_Tag_Parser.GetRow(iRowHandle).Row
			Try
				row("Publisher") = True
			Catch ex As Exception

			End Try
		Next
	End Sub

	Private Sub bbi_Remove_Publisher_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Remove_Publisher.ItemClick
		Dim iRowHandles As Integer() = MKNetDXLib.ctl_MKDXGrid.GetGridViewSelectedDataRowHandles(gv_Tag_Parser)
		Dim iNumRows As Integer = iRowHandles.Length

		If iNumRows <= 0 Then
			Return
		End If

		For Each iRowHandle As Integer In iRowHandles
			Dim row As DataRow = gv_Tag_Parser.GetRow(iRowHandle).Row
			Try
				row("Publisher") = False
			Catch ex As Exception

			End Try
		Next
	End Sub

	Private Sub bbi_Export_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Export.ItemClick
		Dim sPath As Object = MKNetLib.cls_MKFileSupport.SaveFile("Export Tag Settings", "Tag Settings Files (*.mlts)|*.mlts", 0, "mlts")
		If TC.NZ(sPath, "") <> "" Then
			Me.DS_ML.tbl_Tag_Parser.WriteXml(sPath)
			DevExpress.XtraEditors.XtraMessageBox.Show("Export done.", "Export Tag Settings", MessageBoxButtons.OK, MessageBoxIcon.Information)
		End If
	End Sub

	Private Sub bbi_Import_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bbi_Import.ItemClick
		Dim sPath As Object = MKNetLib.cls_MKFileSupport.OpenFileDialog("Import Tag Settings", "Tag Settings Files (*.mlts)|*.mlts", ParentForm:=Me)
		If Alphaleonis.Win32.Filesystem.File.Exists(sPath) Then
			Try
				Dim DS_Import As New DS_ML
				DS_Import.tbl_Tag_Parser.ReadXml(sPath)

				For Each row_Import As DS_ML.tbl_Tag_ParserRow In DS_Import.tbl_Tag_Parser.Rows
					If TC.NZ(row_Import("Content"), "").Length > 0 Then
						Dim rows() As DataRow = Me.DS_ML.tbl_Tag_Parser.Select("Content = " & TC.getSQLFormat(row_Import("Content")))
						If rows.Length = 1 Then
							Dim row As DataRow = rows(0)
							For i As Integer = 1 To Me.DS_ML.tbl_Tag_Parser.Columns.Count - 1
								If Me.DS_ML.tbl_Tag_Parser.Columns(i).ColumnName <> "Found_In" Then
									row(i) = row_Import(i)
								End If
							Next
						End If
					End If
				Next

				DevExpress.XtraEditors.XtraMessageBox.Show("Import done.", "Import Tag Settings XML", MessageBoxButtons.OK, MessageBoxIcon.Information)
			Catch ex As Exception
				DevExpress.XtraEditors.XtraMessageBox.Show("Error while importing Tag Settings: " & ControlChars.CrLf & ControlChars.CrLf & ex.Message, "Import Tag Settings XML", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			End Try
		End If
	End Sub

	Private Sub chb_New_Tags_Only_CheckedChanged(sender As Object, e As EventArgs) Handles chb_New_Tags_Only.CheckedChanged
		If chb_New_Tags_Only.Checked Then
			Me.BS_Tag_Parser.Filter = "id_Tag_Parser < 0 AND id_Rombase_Tag_Parser IS NULL"
		Else
			Me.BS_Tag_Parser.Filter = ""
		End If
	End Sub

	Private Sub BS_Tag_Parser_CurrentChanged(sender As Object, e As EventArgs) Handles BS_Tag_Parser.CurrentChanged
		Me.lbl_Found_In_Left.Text = ""
		Me.lbl_Found_In_Middle.Text = ""
		Me.lbl_Found_In_Right.Text = ""

		If BS_Tag_Parser.Current Is Nothing Then
			Return
		End If

		'Dim splitName As String() = System.Text.RegularExpressions.Regex.Split(TC.NZ(BS_Tag_Parser.Current("Found_In"), ""), TC.NZ(BS_Tag_Parser.Current("Content"), "|||"))
		Dim filename As String = TC.NZ(BS_Tag_Parser.Current("Found_In"), "")
		Dim delimiter As String = TC.NZ(BS_Tag_Parser.Current("Content"), "|||")
		Dim findindex As Integer = filename.IndexOf(delimiter)

		Me.lbl_Found_In_Left.Text = filename.Substring(0, findindex)
		Me.lbl_Found_In_Middle.Text = delimiter
		Me.lbl_Found_In_Right.Text = filename.Substring(findindex + delimiter.Length, filename.Length - findindex - delimiter.Length)

	End Sub

	Private Sub gv_Tag_Parser_MouseMove(sender As Object, e As MouseEventArgs) Handles gv_Tag_Parser.MouseMove
		Me.grd_Tag_Parser.ShowHandInColumns(gv_Tag_Parser, {"Apply"}, e)
	End Sub

	Private Sub gv_Tag_Parser_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_Tag_Parser.FocusedRowChanged
		Dim gv As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

		If TC.NZ(gv.GetIncrementalText(), "") <> "" Then
			gv.ClearSelection()
			gv.SelectRow(gv.FocusedRowHandle)
		End If
	End Sub
End Class
