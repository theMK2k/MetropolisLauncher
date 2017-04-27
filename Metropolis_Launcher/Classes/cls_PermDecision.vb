Public Class cls_PermDecision

	Public Class PermDecisionButton
		Private _Text As String
		Private _Result As DialogResult
		Private _Tooltip As String

		Public ReadOnly Property Text As String
			Get
				Return _Text
			End Get
		End Property

		Public ReadOnly Property Result As DialogResult
			Get
				Return _Result
			End Get
		End Property

		Public ReadOnly Property Tooltip As String
			Get
				Return _Tooltip
			End Get
		End Property

		Public Sub New(ByVal Text As String, ByVal Result As DialogResult, Optional ByVal Tooltip As String = "")
			_Text = Text
			_Result = Result
			_Tooltip = Tooltip
		End Sub
	End Class

	Private _Buttons() As PermDecisionButton
	Private _LastDecision As DialogResult
	Private _Caption As String
	Private _Prompt As String
	Private _ApplyAll As Boolean = False
	Private _ParentForm As Windows.Forms.Form = Nothing

	Public Property ApplyAll As Boolean
		Get
			Return _ApplyAll
		End Get
		Set(ByVal value As Boolean)
			Me._ApplyAll = value
		End Set
	End Property

	Public Sub New(ByVal ParentForm As Windows.Forms.Form, ByVal Caption As String, ByVal Prompt As String, ByRef Buttons() As PermDecisionButton)
		_Caption = Caption
		_Prompt = Prompt
		_Buttons = Buttons
		_ParentForm = ParentForm
	End Sub

	Public Function Show(Optional ByVal Caption As String = "", Optional ByVal Prompt As String = "") As DialogResult
		If _ApplyAll Then
			Return _LastDecision
		End If

		If Caption.Length > 0 Then _Caption = Caption
		If Prompt.Length > 0 Then _Prompt = Prompt
		Using frm As New frm_PermDecision(_Caption, _Prompt, _Buttons)
			Dim res = frm.ShowDialog(_ParentForm)
			_LastDecision = res
			_ApplyAll = frm.ApplyAll
			Return res
		End Using
	End Function
End Class
