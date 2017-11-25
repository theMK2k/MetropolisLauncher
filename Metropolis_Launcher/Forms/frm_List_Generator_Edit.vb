Public Class frm_List_Generator_Edit

	Public _id_List_Generators As Int64 = 0L

	Public Sub New()
		InitializeComponent()

		Using tran As SQLite.SQLiteTransaction = cls_Globals.Conn.BeginTransaction()
			DS_ML.Fill_static_List_Generators_Sort(tran, Me.DS_ML.static_List_Generator_Sort)
		End Using

		'Apply Default Values
		Me.txb_Name.EditValue = "<New List Generator>"
		Me.cmb_Sort.EditValue = 1L
		Me.Text = "Add List Generator"
	End Sub

	Public Sub New(ByVal id_List_Generators As Int64, ByVal Name As String, ByVal Sort As Int64, ByVal MainTemplate As String, ByVal FileEntryTemplate As String)
		Me.New

		Me._id_List_Generators = id_List_Generators
		Me.txb_Name.EditValue = Name
		Me.cmb_Sort.EditValue = Sort
		Me.txb_Main_Template.EditValue = MainTemplate
		Me.txb_File_Entry_Template.EditValue = FileEntryTemplate
		Me.Text = "Edit List Generator"
	End Sub

	Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
		'Checks
		If TC.NZ(Me.txb_Name.EditValue, "").Trim = "" Then
			MKDXHelper.MessageBox("Please provide a name for the List Generator", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.txb_Name.Focus()
			Return
		End If

		If TC.NZ(Me.cmb_Sort.EditValue, 0) = 0 Then
			MKDXHelper.MessageBox("Please provide a sort order for the List Generator", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.cmb_Sort.Focus()
			Return
		End If

		If TC.NZ(Me.txb_Main_Template.EditValue, "").Trim = "" Then
			MKDXHelper.MessageBox("Please provide a List File Template for the List Generator", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.txb_Main_Template.Focus()
			Return
		End If

		If Not TC.NZ(Me.txb_Main_Template.EditValue, "").Contains("%entries%") Then
			If Not MKDXHelper.MessageBox("Your List File Template does not contain the variable %entries%, do you really want to save?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
				Me.txb_Main_Template.Focus()
				Return
			End If
		End If

		If Not TC.NZ(Me.txb_File_Entry_Template.EditValue, "").Contains("%") Then
			If Not MKDXHelper.MessageBox("Your File Entry Template does not contain any variable, do you really want to save?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then
				Me.txb_File_Entry_Template.Focus()
				Return
			End If
		End If

		If Me._id_List_Generators < 0 Then
			Dim res As DialogResult = MKDXHelper.MessageBox("You are editing a shipped list generator, do you want to create a new one?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

			If res = DialogResult.Cancel Then
				Return
			End If

			If res = DialogResult.No Then
				Me.DialogResult = DialogResult.Cancel
				Me.Close()
				Return
			End If
		End If

		If TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_List_Generators FROM tbl_List_Generators WHERE Name = " & TC.getSQLFormat(Me.txb_Name.EditValue) & IIf(Me._id_List_Generators > 0, " AND id_List_Generators <> " & TC.getSQLFormat(Me._id_List_Generators), "")), 0) <> 0 Then
			MKDXHelper.MessageBox("Another List Generator with the same name already exists. Please use a unique name.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.txb_Name.Focus()
			Return
		End If

		If TC.NZ(DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT id_Rombase_List_Generators FROM rombase.tbl_Rombase_List_Generators WHERE Name = " & TC.getSQLFormat(Me.txb_Name.EditValue)), 0) <> 0 Then
			MKDXHelper.MessageBox("Another shipped List Generator with the same name already exists. Please use a unique name.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.txb_Name.Focus()
			Return
		End If

		Me.DialogResult = DialogResult.OK
		Me.Close()
	End Sub

	Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
		Me.DialogResult = DialogResult.Cancel
		Me.Close()
	End Sub
End Class