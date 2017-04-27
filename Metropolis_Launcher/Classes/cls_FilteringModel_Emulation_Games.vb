Imports DevExpress.Utils.Filtering
Imports System.ComponentModel.DataAnnotations

Public Class cls_FilteringModel_Emulation_Games
	<Display(GroupName:="Rank, Score, Fav"), FilterRange(0, 100)>
	Public Property Rank As Int32

	<Display(GroupName:="Rank, Score, Fav"), FilterRange(0.0, 5.0)>
	Public Property Score As Decimal

	<Display(GroupName:="Release"), FilterLookup(11)>
	Public Property Year As String

	<Display(GroupName:="Rank, Score, Fav")>
	Public Property Favourite As Boolean

	<Display(GroupName:="Release"), FilterLookup(10)>
	Public Property Platform As String

	<FilterRange(0, 18), Display(GroupName:="Age", Name:="Age (Pessimistic)")>
	Public Property Age_Pessimistic As Int32

	<FilterRange(0, 18), Display(GroupName:="Age", Name:="Age (Optimistic)")>
	Public Property Age_Optimistic As Int32

	<FilterLookup(10), Display(GroupName:="Genres, Technical", Name:="Genres")>
	Public Property Basic_Genres As String

	<Display(GroupName:="Genres, Technical"), FilterLookup(10)>
	Public Property Perspectives As String

	<FilterLookup(10), Display(GroupName:="Genres, Technical", Name:="Sports Themes")>
	Public Property Sports_Themes As String

	<FilterLookup(10), Display(Name:="Educational Categories", GroupName:="Genres, Technical")>
	Public Property Educational_Categories As String

	<FilterLookup(10), Display(GroupName:="Genres, Technical", Name:="Visual Presentation")>
	Public Property Visual_Presentation As String

	<FilterLookup(10), Display(GroupName:="Genres, Technical", Name:="Pacing")>
	Public Property Pacing As String

	<FilterLookup(10), Display(GroupName:="Genres, Technical", Name:="Gameplay")>
	Public Property Gameplay As String

	<FilterLookup(10), Display(GroupName:="Genres, Technical", Name:="Interface / Control")>
	Public Property Interface_Control As String

	<FilterLookup(10), Display(GroupName:="Genres, Technical", Name:="Vehicular Themes")>
	Public Property Vehicular_Themes As String

	<FilterLookup(10), Display(GroupName:="Genres, Technical", Name:="Setting")>
	Public Property Setting As String

	<FilterLookup(10), Display(GroupName:="Genres, Technical", Name:="Narrative Theme / Topic")>
	Public Property Narrative_Theme_Topic As String

	<FilterLookup(10), Display(GroupName:="Genres, Technical", Name:="DLC / Add-On")>
	Public Property DLC_Addon As String

	<FilterLookup(10), Display(GroupName:="Genres, Technical", Name:="Special Edition")>
	Public Property Special_Edition As String

	<FilterLookup(10), Display(GroupName:="Genres, Technical", Name:="Other Attributes")>
	Public Property Other_Attributes As String

	<FilterRange(0, 8), Display(GroupName:="Multiplayer", Name:="Min. Players")>
	Public Property MinPlayers As Int32

	<FilterRange(0, 8), Display(GroupName:="Multiplayer", Name:="Max. Players")>
	Public Property MaxPlayers As Int32

	<Display(GroupName:="Rank, Score, Fav")>
	Public Property Rating As Int32

	<Display(GroupName:="Release"), FilterLookup(10)>
	Public Property Regions As String

	<Display(GroupName:="Release"), FilterLookup(10)>
	Public Property Languages As String

	<FilterLookup(10), Display(GroupName:="Multiplayer", Name:="Multiplayer Game Modes")>
	Public Property MP_GameModes As String

	<FilterLookup(10), Display(GroupName:="Multiplayer", Name:="Multiplayer Options")>
	Public Property MP_Options As String

End Class
