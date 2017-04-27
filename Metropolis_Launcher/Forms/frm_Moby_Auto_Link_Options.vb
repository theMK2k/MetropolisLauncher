Public Class frm_Moby_Auto_Link_Options

	Public Class cls_Moby_Auto_Link_Options
		Public Strip_File_Extension As Boolean
		Public Strip_Tags As Boolean
		Public Sort_Words As Boolean
		Public Remove_Characters As Boolean
		Public Remove_Characters_String As String
		Public Minimum_Match_Score As Integer

		Public Sub New(ByVal Strip_File_Extension As Boolean, ByVal Strip_Tags As Boolean, ByVal Sort_Words As Boolean, ByVal Remove_Characters As Boolean, ByVal Remove_Characters_String As String, ByVal Minimum_Match_Score As Integer)
			Me.Strip_File_Extension = Strip_File_Extension
			Me.Strip_Tags = Strip_Tags
			Me.Sort_Words = Sort_Words
			Me.Remove_Characters = Remove_Characters
			Me.Remove_Characters_String = Remove_Characters_String
			Me.Minimum_Match_Score = Minimum_Match_Score
		End Sub
	End Class

	Public Sub New(Optional ByVal Strip_File_Extension As Boolean = True)
		InitializeComponent()

		' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
		Me.chb_Strip_Extensions.Checked = Strip_File_Extension
	End Sub

	Public ReadOnly Property Result As cls_Moby_Auto_Link_Options
		Get
			Return New cls_Moby_Auto_Link_Options(Me.chb_Strip_Extensions.Checked, Me.chb_Strip_Tags.Checked, Me.chb_Sort_Words.Checked, Me.chb_Remove_Characters.Checked, Me.txb_Remove_Characters.Text, CInt(Me.spn_Minimum_Match_Score.Value))
		End Get
	End Property
End Class