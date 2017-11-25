Public Class frm_Select_Known_Emulator

	Public ReadOnly Property Selected_Known_Emulator_Row As DS_Rombase.tbl_Rombase_Known_EmulatorsRow
		Get
			If BS_Known_Emulators.Current Is Nothing Then
				Return Nothing
			End If

			Return BS_Known_Emulators.Current.Row
		End Get
	End Property


	Public Sub New(ByRef dtKnownEmulators As DS_Rombase.tbl_Rombase_Known_EmulatorsDataTable)
		InitializeComponent()

		Me.BS_Known_Emulators.DataMember = ""
		Me.BS_Known_Emulators.DataSource = dtKnownEmulators
	End Sub

	Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
		If BS_Known_Emulators.Current Is Nothing Then
			Return
		End If

		Me.DialogResult = DialogResult.OK
	End Sub
End Class