Public Class frm_FilterSet

	Public Enum enm_QuickfilterSource
		None = 0
		FromGrid = 1
		FromDatabase = 2
	End Enum

	Public Function QuickFilterSource2String(ByVal qfs As enm_QuickfilterSource) As String
		Select Case qfs
			Case enm_QuickfilterSource.None
				Return "No Quickfilter"
			Case enm_QuickfilterSource.FromGrid
				Return "From List"
			Case enm_QuickfilterSource.FromDatabase
				Return "From Database"
			Case Else
				Return "???"
		End Select
	End Function

	Public Function QuickFilterSourceSuperTip(ByVal qfs As enm_QuickfilterSource) As String
		Select Case qfs
			Case enm_QuickfilterSource.None
				Return "There is no quickfilter for this filterset"
			Case enm_QuickfilterSource.FromGrid
				Return "Quickfilter is derived from the current list"
			Case enm_QuickfilterSource.FromDatabase
				Return "Quickfilter is derived from the database"
			Case Else
				Return "???"
		End Select
	End Function

	Public Property FilterSet_Name As Object
		Get
			Return txb_Name.EditValue
		End Get
		Set(ByVal value As Object)
			txb_Name.EditValue = value
		End Set
	End Property

	Private _FilterSet_Quickfilter As Object = Nothing
	Public Property FilterSet_Quickfilter As Object
		Get
			Return _FilterSet_Quickfilter
		End Get
		Set(ByVal value As Object)
			_FilterSet_Quickfilter = value
		End Set
	End Property

	Private _CurrentGrid_Quickfilter As Object = Nothing
	Public Property CurrentGrid_Quickfilter As Object
		Get
			Return _CurrentGrid_Quickfilter
		End Get
		Set(ByVal value As Object)
			_CurrentGrid_Quickfilter = value
		End Set
	End Property

	Public Property UseQuickFilter As Boolean
		Get
			Return chk_QuickFilter.Checked
		End Get
		Set(ByVal value As Boolean)
			chk_QuickFilter.Checked = value
		End Set
	End Property

	Private _QuickFilter As Object = Nothing
	Public Property QuickFilter As Object
		Get
			Return _QuickFilter
		End Get
		Set(ByVal value As Object)
			Me._QuickFilter = value
			If TC.IsNullNothingOrEmpty(_QuickFilter) Then
				Me.QuickFilterSource = enm_QuickfilterSource.None
			End If
		End Set
	End Property

	Private _QuickFilterSource As enm_QuickfilterSource = enm_QuickfilterSource.None
	Public Property QuickFilterSource As enm_QuickfilterSource
		Get
			Return _QuickFilterSource
		End Get
		Set(ByVal value As enm_QuickfilterSource)
			_QuickFilterSource = value
			Me.lbl_Current_Quickfilter.Text = QuickFilterSource2String(value)
			Dim st As New DevExpress.Utils.SuperToolTip
			Dim tti As New DevExpress.Utils.ToolTipTitleItem()
			tti.Text = QuickFilterSourceSuperTip(value)
			st.Items.Add(tti)
			Me.lbl_Current_Quickfilter.SuperTip = st
		End Set
	End Property

	Private _id_FilterSet As Object = Nothing
	Public Property id_FilterSet
		Get
			Return _id_FilterSet
		End Get
		Set(ByVal value)
			_id_FilterSet = value
		End Set
	End Property

	Public Sub New(ByVal id_FilterSet As Object, ByVal CurrentGrid_QuickFilter As Object, ByVal FilterSet_QuickFilter As Object, ByVal UseQuickFilter As Boolean, ByVal FilterSet_Name As Object)
		InitializeComponent()

		Me.id_FilterSet = id_FilterSet

		Me.QuickFilterSource = enm_QuickfilterSource.None

		Me._CurrentGrid_Quickfilter = CurrentGrid_QuickFilter
		If TC.IsNullNothingOrEmpty(Me._CurrentGrid_Quickfilter) Then
			btn_GetQuickfilter.Enabled = False
		End If

		Me._FilterSet_Quickfilter = FilterSet_QuickFilter

		Me.UseQuickFilter = UseQuickFilter

		'ALWAYS use the current Quickfilter from the Grid
		'If TC.NZ(id_FilterSet, 0) > 0 Then
		'	Me.QuickFilter = Me.FilterSet_Quickfilter
		'	If Not TC.IsNullNothingOrEmpty(Me.QuickFilter) Then
		'		Me.QuickFilterSource = enm_QuickfilterSource.FromDatabase
		'	End If
		'Else
		Me.QuickFilter = Me.CurrentGrid_Quickfilter
		If Not TC.IsNullNothingOrEmpty(Me.QuickFilter) Then
			Me.QuickFilterSource = enm_QuickfilterSource.FromGrid
		End If
		'End If

		Me.FilterSet_Name = FilterSet_Name
	End Sub

	Private Sub btn_GetQuickfilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_GetQuickfilter.Click
		Me.QuickFilter = _CurrentGrid_Quickfilter
		If Not TC.IsNullNothingOrEmpty(Me.QuickFilter) Then
			Me.QuickFilterSource = enm_QuickfilterSource.FromGrid
		End If
	End Sub

	Private Sub btn_RemoveQuickFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_RemoveQuickFilter.Click
		Me.QuickFilter = Nothing
	End Sub

	Private Sub btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OK.Click
		If TC.IsNullNothingOrEmpty(Me.txb_Name.EditValue) Then
			MKDXHelper.MessageBox("Please provide a name of the filterset.", "Name is missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		If 0 < DataAccess.FireProcedureReturnScalar(cls_Globals.Conn, 0, "SELECT COUNT(1) FROM tbl_FilterSets WHERE [Name] = " & TC.getSQLFormat(txb_Name.EditValue) & IIf(TC.NZ(Me.id_FilterSet, 0) > 0, " AND id_FilterSets <> " & TC.getSQLFormat(Me.id_FilterSet), "")) Then
			MKDXHelper.MessageBox("The name is already used.", "Name is used", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		Me.DialogResult = Windows.Forms.DialogResult.OK
	End Sub

	Private Sub btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cancel.Click
		Me.DialogResult = Windows.Forms.DialogResult.Cancel
	End Sub
End Class
