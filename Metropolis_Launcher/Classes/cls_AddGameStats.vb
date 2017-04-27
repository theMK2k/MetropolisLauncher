Public Class cls_AddGameStats
	Public _new As Integer
	Public _links As Integer
	Public _duplicates_added As Integer
	Public _duplicates_replaced As Integer
	Public _duplicates_ignored As Integer

	Public Sub New(Optional ByVal [new] As Integer = 0, Optional ByVal links As Integer = 0, Optional ByVal duplicates_added As Integer = 0, Optional ByVal duplicates_replaced As Integer = 0, Optional ByVal duplicates_ignored As Integer = 0)
		Me._new = [new]
		Me._links = [links]
		Me._duplicates_added = duplicates_added
		Me._duplicates_replaced = duplicates_replaced
		Me._duplicates_ignored = duplicates_ignored
	End Sub

	Public Sub Add(ByVal addGameStats As cls_AddGameStats)
		Me._new += addGameStats._new
		Me._links += addGameStats._links
		Me._duplicates_added += addGameStats._duplicates_added
		Me._duplicates_replaced += addGameStats._duplicates_replaced
		Me._duplicates_ignored += addGameStats._duplicates_ignored
	End Sub
End Class
