Partial Class DS_MLApps
	Public Shared Function Fill_Categories(ByRef conn As SQLite.SQLiteConnection, ByRef dt As CategoriesDataTable) As Boolean
		dt.Clear()
		DataAccess.FireProcedureReturnDT(conn, 0, False, "SELECT id_Apps_Categories AS id_Categories, Category FROM tbl_Apps_Categories", dt)
		Return True
	End Function

	Public Shared Function Fill_Apps(ByRef conn As SQLite.SQLiteConnection, ByRef dt As AppsDataTable) As Boolean
		dt.Clear()
		DataAccess.FireProcedureReturnDT(conn, 0, False, "SELECT id_Apps, id_Categories, DisplayName, Executable, Arguments, RunExclusive, Stat_Executions, Stat_RunLength, Description FROM tbl_Apps", dt)
		Return True
	End Function

	Public Shared Function Update_Apps(ByRef conn As SQLite.SQLiteConnection, ByRef dt As AppsDataTable) As Boolean
		For Each row As DataRow In dt.Rows
			If row.RowState = DataRowState.Deleted Then
				DataAccess.FireProcedure(conn, 0, "DELETE FROM tbl_Apps WHERE id_Apps = " & TC.getSQLFormat(row("id_Apps", DataRowVersion.Original)))
			End If

			If row.RowState = DataRowState.Added Then
				Dim id_Apps As Integer = DataAccess.FireProcedureReturnScalar(conn, 0, "INSERT INTO tbl_Apps (id_Categories, DisplayName, Executable, Arguments, RunExclusive, Stat_Executions, Stat_RunLength, Description) VALUES (" & TC.getSQLParameter(row("id_Categories"), row("DisplayName"), row("Executable"), row("Arguments"), row("RunExclusive"), row("Stat_Executions"), row("Stat_RunLength"), row("Description")) & "); SELECT last_insert_rowid()")
				row("id_Apps") = id_Apps
			End If

			If row.RowState = DataRowState.Modified Then
				Dim sSQL As String = "UPDATE tbl_Apps SET "
				sSQL &= "	id_Categories = " & TC.getSQLFormat(row("id_Categories"))
				sSQL &= "	, DisplayName = " & TC.getSQLFormat(row("DisplayName"))
				sSQL &= "	, Executable = " & TC.getSQLFormat(row("Executable"))
				sSQL &= "	, Arguments = " & TC.getSQLFormat(row("Arguments"))
				sSQL &= "	, RunExclusive = " & TC.getSQLFormat(row("RunExclusive"))
				sSQL &= "	, Stat_Executions = " & TC.getSQLFormat(row("Stat_Executions"))
				sSQL &= "	, Stat_RunLength = " & TC.getSQLFormat(row("Stat_RunLength"))
				sSQL &= "	, Description = " & TC.getSQLFormat(row("Description"))
				sSQL &= " WHERE id_Apps = " & TC.getSQLFormat(row("id_Apps"))

				DataAccess.FireProcedure(conn, 0, sSQL)
			End If
		Next

		dt.AcceptChanges()

		Return True
	End Function
End Class
