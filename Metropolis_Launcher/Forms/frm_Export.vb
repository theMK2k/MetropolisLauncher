Public Class frm_Export
	Private _al_id_Emu_Games As ArrayList

	Private pd_Overwrite As New cls_PermDecision(Me, "Export", "File %1% already exists, overwrite?", {New cls_PermDecision.PermDecisionButton("Yes", Windows.Forms.DialogResult.Yes), New cls_PermDecision.PermDecisionButton("No", Windows.Forms.DialogResult.No)})

	Public Sub New(ByRef al_id_Emu_Games As ArrayList, ByVal bHasDOSEntry As Boolean)
		InitializeComponent()

		_al_id_Emu_Games = al_id_Emu_Games

		Dim row As DataRow = Nothing

		'Fill tbl_Mode
		row = tbl_Mode.NewRow
		row("id") = 1
		row("Text") = "Just copy"
		tbl_Mode.Rows.Add(row)

		If Not bHasDOSEntry Then
			row = tbl_Mode.NewRow
			row("id") = 2
			row("Text") = "Pack as individual .zip files"
			tbl_Mode.Rows.Add(row)

			row = tbl_Mode.NewRow
			row("id") = 3
			row("Text") = "Pack as merged .zip files"
			tbl_Mode.Rows.Add(row)
		End If

		'Fill tbl_Compression
		row = tbl_Compression.NewRow
		row("value") = 0
		row("displaytext") = "None"
		tbl_Compression.Rows.Add(row)

		row = tbl_Compression.NewRow
		row("value") = 41
		row("displaytext") = "Deflate - Best Speed"
		tbl_Compression.Rows.Add(row)

		row = tbl_Compression.NewRow
		row("value") = 46
		row("displaytext") = "Deflate - Default"
		tbl_Compression.Rows.Add(row)

		row = tbl_Compression.NewRow
		row("value") = 49
		row("displaytext") = "Deflate - Best Compression"
		tbl_Compression.Rows.Add(row)

		row = tbl_Compression.NewRow
		row("value") = 20
		row("displaytext") = "BZip2"
		tbl_Compression.Rows.Add(row)

		row = tbl_Compression.NewRow
		row("value") = 60
		row("displaytext") = "LZMA"
		tbl_Compression.Rows.Add(row)

		row = tbl_Compression.NewRow
		row("value") = 30
		row("displaytext") = "PPMd"
		tbl_Compression.Rows.Add(row)

		cmb_Mode_EditValueChanged(Nothing, Nothing)
	End Sub

	Private Sub btn_Destination_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Destination.Click
		Dim sDestination As String = MKNetLib.cls_MKFileSupport.OpenFolderDialog()
		If Alphaleonis.Win32.Filesystem.Directory.Exists(sDestination) Then
			Me.txb_Destination.Text = sDestination
		End If
	End Sub

	Private Sub btn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Close.Click
		Me.Close()
	End Sub

	Private Sub btn_Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Export.Click
		If Not Alphaleonis.Win32.Filesystem.Directory.Exists(txb_Destination.Text) Then
			MKDXHelper.MessageBox("The destination directory does not exist.", "Destination not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		Dim prg As New MKNetDXLib.cls_MKDXBaseform_Progress_Helper(cls_Skins.GetCurrentSkinname(Nothing), 400, 100, ProgressBarStyle.Continuous, False, "Exporting Game {0} of {1} ...", 0, _al_id_Emu_Games.Count, True)
		prg.Start()

		Dim bCancelled As Boolean = False

		For Each id_Emu_Games As Int64 In _al_id_Emu_Games
			prg.IncreaseCurrentValue()

			Export_Game(prg, id_Emu_Games, Me.txb_Destination.Text)

			If prg.WaitForCancel Then
				bCancelled = True
				Exit For
			End If

		Next

		prg.Close()

		If Not bCancelled Then
			MKDXHelper.MessageBox("The export completed.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
		Else
			MKDXHelper.MessageBox("The export has been cancelled.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
		End If
	End Sub

	Private Function Export_Game(ByRef prg As MKNetDXLib.cls_MKDXBaseform_Progress_Helper, ByVal id_Emu_Games As Int64, ByVal Destination_Folder As String) As Boolean
		Dim dt_Emu_Games As New DS_ML.src_ucr_Emulation_GamesDataTable

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
			DS_ML.Fill_src_ucr_Emulation_Games(tran, dt_Emu_Games, id_Emu_Games:=id_Emu_Games, ShowVolumes:=True)
			tran.Commit()
		End Using

		Dim rows_Main() As DataRow = dt_Emu_Games.Select("id_Emu_Games = " & id_Emu_Games)

		Dim sOutfilename As String = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(rows_Main(0)("File"))

		Dim TempDir As String = MKNetLib.cls_MKFileSupport.CreateTempDir("mlexport_")

		Dim al_Files As New ArrayList

		For Each row As DataRow In dt_Emu_Games.Rows
			Dim File As String = row("File")
			Dim Folder As String = row("Folder")
			Dim Innerfile As String = TC.NZ(row("InnerFile"), "")

			If IsNumeric(row("id_Rombase_DOSBox_Filetypes")) Then
				Dim i As Integer = 0
				If TC.NZ(row("id_Rombase_DOSBox_Filetypes"), 0) = cls_Globals.enm_Rombase_DOSBox_Filetypes.zip Then
					'Packed Content
				ElseIf TC.NZ(row("id_Rombase_DOSBox_Filetypes"), 0) = cls_Globals.enm_Rombase_DOSBox_Filetypes.cwd Then
					'Workdir
					File = ":WorkDir:"
				ElseIf TC.NZ(row("id_Rombase_DOSBox_Filetypes"), 0) = -1 Then
					'Install Media
					If File <> Innerfile Then Continue For 'The Install Media is contained within packed file
				Else
					Continue For
				End If
			End If

			Dim rfd As New ucr_Emulation.cls_Romfiledata(Folder & "\" & File, Innerfile, TempDir, True, True)

			If Not rfd.IsValid Then
				Continue For
			End If

			al_Files.Add(rfd.Fullpath)
		Next

		Export_Files(prg, sOutfilename, al_Files)

		MKNetLib.cls_MKFileSupport.Delete_Directory(TempDir)

		Return True
	End Function

	''' <summary>
	''' 
	''' </summary>
	''' <param name="OutFileName">Output file name if merged zip is to be exported</param>
	''' <param name="al_Files"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Private Function Export_Files(ByRef prg As MKNetDXLib.cls_MKDXBaseform_Progress_Helper, ByVal OutFileName As String, ByRef al_Files As ArrayList) As String
		Select Case TC.NZ(BS_Mode.Current("id"), 0)
			Case 1	'Just copy
				For Each file As String In al_Files
					'Copy a file
					If Alphaleonis.Win32.Filesystem.File.Exists(file) Then
						Dim filename As String = Alphaleonis.Win32.Filesystem.Path.GetFileName(file)

						Dim bCopy As Boolean = True

						If Alphaleonis.Win32.Filesystem.File.Exists(txb_Destination.Text & "\" & filename) Then
							If Not pd_Overwrite.Show("Export", "File " & filename & " already exists, overwrite?") = Windows.Forms.DialogResult.Yes Then
								bCopy = False
							End If
						End If

						Try
							If bCopy Then
								Alphaleonis.Win32.Filesystem.File.Copy(file, txb_Destination.Text & "\" & filename, True)
							End If
						Catch ex As Exception
							prg.Hide = True
							MKDXHelper.ExceptionMessageBox(ex, "Error while copying " & filename & "." & ControlChars.CrLf & ControlChars.CrLf, "Export")
							prg.Hide = False
						End Try
					ElseIf Alphaleonis.Win32.Filesystem.Directory.Exists(file) Then
						'Copy a directory
						Dim dirname As String = Alphaleonis.Win32.Filesystem.Path.GetFileName(MKNetLib.cls_MKStringSupport.Clean_Right(file, "\"))
						Dim destination As String = txb_Destination.Text & "\" & dirname & "\"

						Dim bCopy As Boolean = True

						If Alphaleonis.Win32.Filesystem.Directory.Exists(txb_Destination.Text & "\" & dirname) Then
							If Not pd_Overwrite.Show("Export", "Directory " & dirname & " already exists, overwrite?") = Windows.Forms.DialogResult.Yes Then
								bCopy = False
							End If
						End If

						Try
							If bCopy Then
								If Not Alphaleonis.Win32.Filesystem.Directory.Exists(destination) Then
									Alphaleonis.Win32.Filesystem.Directory.CreateDirectory(destination)
								End If

								If Alphaleonis.Win32.Filesystem.Directory.Exists(destination) Then
									FileIO.FileSystem.CopyDirectory(file, destination, True)
								Else
									prg.Hide = True
									MKDXHelper.MessageBox("Cannot copy " & dirname & ".", "Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									prg.Hide = False
								End If
							End If
						Catch ex As Exception
							prg.Hide = True
							MKDXHelper.ExceptionMessageBox(ex, "Error while copying " & dirname & "." & ControlChars.CrLf & ControlChars.CrLf, "Export")
							prg.Hide = False
						End Try
					End If
				Next
			Case 2
				'Individual Zips
				For Each file As String In al_Files
					Dim filename As String = Alphaleonis.Win32.Filesystem.Path.GetFileName(file)
					Dim filenamewithoutextention As String = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(file)
					Dim targetfilename = filenamewithoutextention & ".zip"
					Dim targetfullpath = txb_Destination.Text & "\" & targetfilename

					Dim bCopy As Boolean = True

					If Alphaleonis.Win32.Filesystem.File.Exists(targetfullpath) Then
						prg.Hide = True
						If Not pd_Overwrite.Show("Export", "File " & targetfilename & " already exists, overwrite?") = Windows.Forms.DialogResult.Yes Then
							bCopy = False
						End If
						prg.Hide = False
					End If

					Try
						If bCopy Then
							Dim ci As New SharpCompress.Common.CompressionInfo()
							ci.DeflateCompressionLevel = TC.NZ(cmb_Compression.EditValue, 0) Mod 10	'SharpCompress.Compressor.Deflate.CompressionLevel.BestCompression
							ci.Type = TC.NZ(cmb_Compression.EditValue, 0) \ 10 'SharpCompress.Common.CompressionType.Deflate

							Using zip As IO.Stream = IO.File.OpenWrite(targetfullpath)
								Using zipwriter As SharpCompress.Writer.IWriter = SharpCompress.Writer.WriterFactory.Open(zip, SharpCompress.Common.ArchiveType.Zip, ci)
									Using fs As New System.IO.FileStream(file, IO.FileMode.Open)
										zipwriter.Write(filename, fs, Nothing)
									End Using
								End Using
							End Using
						End If
					Catch ex As Exception
						prg.Hide = True
						MKDXHelper.ExceptionMessageBox(ex, "Error while copying " & filename & "." & ControlChars.CrLf & ControlChars.CrLf, "Export")
						prg.Hide = False
					End Try
				Next
			Case 3
				'Merged Zip
				Try
					Dim targetfilename = OutFileName & ".zip"
					Dim targetfullpath = txb_Destination.Text & "\" & targetfilename

					If Alphaleonis.Win32.Filesystem.File.Exists(targetfullpath) Then
						prg.Hide = True
						If Not pd_Overwrite.Show("Export", "File " & targetfilename & " already exists, overwrite?") = Windows.Forms.DialogResult.Yes Then
							Return False
						End If
						prg.Hide = False
					End If

					Dim ci As New SharpCompress.Common.CompressionInfo()
					ci.DeflateCompressionLevel = TC.NZ(cmb_Compression.EditValue, 0) Mod 10	'SharpCompress.Compressor.Deflate.CompressionLevel.BestCompression
					ci.Type = TC.NZ(cmb_Compression.EditValue, 0) \ 10 'SharpCompress.Common.CompressionType.Deflate

					Using zip As IO.Stream = IO.File.OpenWrite(targetfullpath)
						Using zipwriter As SharpCompress.Writer.IWriter = SharpCompress.Writer.WriterFactory.Open(zip, SharpCompress.Common.ArchiveType.Zip, ci)

							For Each yyy As String In al_Files
								Dim xxx As String = Alphaleonis.Win32.Filesystem.Path.GetFileName(yyy)
								Dim filenamewithoutextention As String = Alphaleonis.Win32.Filesystem.Path.GetFileNameWithoutExtension(yyy)

								Using fs As New System.IO.FileStream(yyy, IO.FileMode.Open)
									zipwriter.Write(xxx, fs, Nothing)
								End Using
							Next
						End Using
					End Using
				Catch ex As Exception
					prg.Hide = True
					MKDXHelper.ExceptionMessageBox(ex, "Error while creating " & OutFileName & ".zip." & ControlChars.CrLf & ControlChars.CrLf, "Export")
					prg.Hide = False
				End Try
			Case Else
				prg.Hide = True
				MKDXHelper.MessageBox("Export mode not recognized.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				prg.Hide = False
				Return False
		End Select

		Return True
	End Function

	Private Sub cmb_Mode_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Mode.EditValueChanged
		Select Case TC.NZ(cmb_Mode.EditValue, 0)
			Case 1
				lbl_Compression.Enabled = False
				cmb_Compression.Enabled = False
			Case 2
				lbl_Compression.Enabled = True
				cmb_Compression.Enabled = True
			Case 3
				lbl_Compression.Enabled = True
				cmb_Compression.Enabled = True
			Case Else
				lbl_Compression.Enabled = False
				cmb_Compression.Enabled = False
		End Select
	End Sub
End Class
