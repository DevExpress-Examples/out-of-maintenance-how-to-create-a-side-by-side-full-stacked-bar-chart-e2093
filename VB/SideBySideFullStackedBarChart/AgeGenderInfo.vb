Namespace SideBySideFullStackedBarChart
	Public Structure GenderAgeInfo
'INSTANT VB NOTE: The field gender was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private ReadOnly gender_Renamed As String
'INSTANT VB NOTE: The field age was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private ReadOnly age_Renamed As String

		Public ReadOnly Property Gender() As String
			Get
				Return gender_Renamed
			End Get
		End Property
		Public ReadOnly Property Age() As String
			Get
				Return age_Renamed
			End Get
		End Property

		Public Sub New(ByVal gender As String, ByVal age As String)
			Me.gender_Renamed = gender
			Me.age_Renamed = age
		End Sub
		Public Overrides Function ToString() As String
			Return Gender & ": " & Age
		End Function
	End Structure
End Namespace
