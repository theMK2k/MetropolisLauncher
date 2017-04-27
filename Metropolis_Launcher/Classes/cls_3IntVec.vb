Public Class cls_3IntVec
	Public _x As Integer
	Public _y As Integer
	Public _z As Integer

	Public Sub New(Optional ByVal x As Integer = 0, Optional ByVal y As Integer = 0, Optional ByVal z As Integer = 0)
		Me._x = x
		Me._y = y
		Me._z = z
	End Sub

	Public Sub Add(ByVal vec As cls_3IntVec)
		Me._x += vec._x
		Me._y += vec._y
		Me._z += vec._z
	End Sub
End Class
