Public Class cls_PDSettingHandler
	Implements MKNetDXLib.IDDSettings(Of MKNetDXLib.ctl_MKDXGrid)
	Implements MKNetDXLib.IDDSettings(Of MKNetDXLib.ctl_MKDXSplitter)
	Implements MKNetDXLib.IDDSettings(Of MKNetDXLib.ctl_MKDXSplitPanel)

	Private _cnn As SqlClient.SqlConnection

	Public Shared MainScreen As Screen = Screen.PrimaryScreen	'Aktuell zu verwendender Hauptbildschirm (auf dem neue Fenster zentriert geöffnet werden sollen), wird von frm_Main festgesetzt bei LocationChanged

#Region "MKDXGrid"
	Private Function SaveSettingsDDDataGridView(ByVal grd As MKNetDXLib.ctl_MKDXGrid) As Boolean Implements MKNetDXLib.IDDSettings(Of MKNetDXLib.ctl_MKDXGrid).SaveSettings
		Try
			If cls_Globals.Conn IsNot Nothing Then
				Dim sIdent As String = grd.Ident
				If grd.MainView IsNot Nothing Then
					Dim sSetting As Object = MKNetDXLib.cls_MKDXGrid_Serializer.SaveLayoutBase64(grd.MainView, MKNetDXLib.enm_MKDXGrid_Serialize_Options.Columns)
					If sSetting IsNot Nothing Then
						Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
							DS_ML.Upsert_tbl_ControlSettings(tran, sIdent, "GridLayout", sSetting)
							tran.Commit()
						End Using


						Return True
					End If
				End If
			End If
		Catch ex As Exception

		End Try

		Return False
	End Function

	Private Function LoadSettingsDDDataGridView(ByVal grd As MKNetDXLib.ctl_MKDXGrid) As Boolean Implements MKNetDXLib.IDDSettings(Of MKNetDXLib.ctl_MKDXGrid).LoadSettings
		Try
			If cls_Globals.Conn IsNot Nothing Then
				Dim sIdent As String = grd.Ident
				If grd.MainView IsNot Nothing Then
					Dim sSetting As Object = MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Setting FROM tbl_ControlSettings WHERE HostName = " & TC.getSQLFormat(System.Environment.MachineName) & " AND ControlID = " & TC.getSQLFormat(sIdent) & " AND " & " SettingID = " & TC.getSQLFormat("GridLayout") & IIf(cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0, " AND id_Users = " & TC.getSQLFormat(cls_Globals.id_Users), " AND id_Users IS NULL"))

					If sSetting IsNot Nothing AndAlso sSetting.Length > 0 Then
						grd.MainView.RestoreLayoutFromStream(New System.IO.MemoryStream(Convert.FromBase64String(sSetting)))
						Return True
					End If
				End If
			End If
		Catch ex As Exception

		End Try

		Return False
	End Function

#End Region

#Region "MKDXColorSplitter"
	Private Function LoadSettingsMKDXSplitter(ByVal obj As MKNetDXLib.ctl_MKDXSplitter) As Boolean Implements MKNetDXLib.IDDSettings(Of MKNetDXLib.ctl_MKDXSplitter).LoadSettings
		Dim i As Integer
		Dim sIdent = obj.Ident

		Try
			i = TC.NZ(MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Setting FROM tbl_ControlSettings WHERE HostName = " & TC.getSQLFormat(System.Environment.MachineName) & " AND ControlID = " & TC.getSQLFormat(sIdent) & " AND " & " SettingID = " & TC.getSQLFormat("SplitPosition") & IIf(cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0, " AND id_Users = " & TC.getSQLFormat(cls_Globals.id_Users), "") & IIf(cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0, " AND id_Users = " & TC.getSQLFormat(cls_Globals.id_Users), " AND id_Users IS NULL")), 0)
			If i > 0 Then
				obj.SplitPosition = i
			End If
			Return True
		Catch
			Return False
		End Try
	End Function

	Private Function SaveSettingsMKDXSplitter(ByVal obj As MKNetDXLib.ctl_MKDXSplitter) As Boolean Implements MKNetDXLib.IDDSettings(Of MKNetDXLib.ctl_MKDXSplitter).SaveSettings
		Try
			'Return DataAccess.FireProcedure(cls_Globals.Conn, 0, "EXEC dbo.gp_SetControlProperties " & TC.getSQLParameter(obj.Ident, obj.SplitPosition))
			Dim sIdent = obj.Ident
			Dim sSetting As String = obj.SplitPosition

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				DS_ML.Upsert_tbl_ControlSettings(tran, sIdent, "SplitPosition", sSetting)
				tran.Commit()
			End Using


			Return True
		Catch
			Return False
		End Try
	End Function
#End Region

#Region "MKDXSplitPanel"
	Private Function LoadSettingsMKDXSplitPanel(ByVal obj As MKNetDXLib.ctl_MKDXSplitPanel) As Boolean Implements MKNetDXLib.IDDSettings(Of MKNetDXLib.ctl_MKDXSplitPanel).LoadSettings
		Dim i As Integer
		Dim sIdent = obj.Ident

		Try
			i = TC.NZ(MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Setting FROM tbl_ControlSettings WHERE HostName = " & TC.getSQLFormat(System.Environment.MachineName) & " AND ControlID = " & TC.getSQLFormat(sIdent) & " AND " & " SettingID = " & TC.getSQLFormat("SplitterPosition") & IIf(cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0, " AND id_Users = " & TC.getSQLFormat(cls_Globals.id_Users), " AND id_Users IS NULL")), 0)
			If i > 0 Then
				obj.SplitterPosition = i
			End If

			obj.Collapsed = TC.NZ(MKNetLib.cls_MKSQLiteDataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT Setting FROM tbl_ControlSettings WHERE HostName = " & TC.getSQLFormat(System.Environment.MachineName) & " AND ControlID = " & TC.getSQLFormat(sIdent) & " AND " & " SettingID = " & TC.getSQLFormat("Collapsed") & IIf(cls_Globals.MultiUserMode AndAlso Not cls_Globals.Admin AndAlso cls_Globals.id_Users > 0, " AND id_Users = " & TC.getSQLFormat(cls_Globals.id_Users), " AND id_Users IS NULL")), obj.Collapsed)

			Return True
		Catch
			Return False
		End Try
	End Function

	Private Function SaveSettingsMKDXSplitPanel(ByVal obj As MKNetDXLib.ctl_MKDXSplitPanel) As Boolean Implements MKNetDXLib.IDDSettings(Of MKNetDXLib.ctl_MKDXSplitPanel).SaveSettings
		Try
			Dim sIdent = obj.Ident
			Dim sSetting As String = obj.SplitterPosition

			Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction
				DS_ML.Upsert_tbl_ControlSettings(tran, sIdent, "SplitterPosition", sSetting)

				sSetting = TC.getSQLFormat(obj.Collapsed)
				DS_ML.Upsert_tbl_ControlSettings(tran, sIdent, "Collapsed", sSetting)

				tran.Commit()
			End Using

			Return True
		Catch
			Return False
		End Try
	End Function
#End Region

	Public Sub New(ByVal cnn As SqlClient.SqlConnection)
		_cnn = cnn
		If _cnn IsNot Nothing AndAlso _cnn.State = ConnectionState.Closed Then
			_cnn.Open()
		End If
		MKNetDXLib.cls_DDSettingHandlerDelegates(Of MKNetDXLib.ctl_MKDXGrid).SaveSettingsFunction = AddressOf SaveSettingsDDDataGridView
		MKNetDXLib.cls_DDSettingHandlerDelegates(Of MKNetDXLib.ctl_MKDXGrid).LoadSettingsFunction = AddressOf LoadSettingsDDDataGridView
		MKNetDXLib.cls_DDSettingHandlerDelegates(Of MKNetDXLib.ctl_MKDXSplitter).SaveSettingsFunction = AddressOf SaveSettingsMKDXSplitter
		MKNetDXLib.cls_DDSettingHandlerDelegates(Of MKNetDXLib.ctl_MKDXSplitter).LoadSettingsFunction = AddressOf LoadSettingsMKDXSplitter
		MKNetDXLib.cls_DDSettingHandlerDelegates(Of MKNetDXLib.ctl_MKDXSplitPanel).SaveSettingsFunction = AddressOf SaveSettingsMKDXSplitPanel
		MKNetDXLib.cls_DDSettingHandlerDelegates(Of MKNetDXLib.ctl_MKDXSplitPanel).LoadSettingsFunction = AddressOf LoadSettingsMKDXSplitPanel
	End Sub

	Protected Overrides Sub Finalize()
		MyBase.Finalize()
		Try
			If _cnn IsNot Nothing Then
				_cnn.Close()
			End If
		Catch
			'
		Finally
			_cnn = Nothing
		End Try
	End Sub
End Class
