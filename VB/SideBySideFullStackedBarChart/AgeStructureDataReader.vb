Imports System.Collections
Imports System.Data
Imports System.Linq

Namespace SideBySideFullStackedBarChart
	Friend Class AgeStructureDataReader
'INSTANT VB NOTE: The field ageStructureTable was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private Shared ageStructureTable_Renamed As DataTable
		Private Shared ReadOnly Property AgeStructureTable() As DataTable
			Get
				If ageStructureTable_Renamed Is Nothing Then
					ageStructureTable_Renamed = LoadPopulationAgeStructure()
				End If
				Return ageStructureTable_Renamed
			End Get
		End Property
		Private Shared Function LoadPopulationAgeStructure() As DataTable
			Return LoadDataTableFromXml("..\..\Data\Population.xml", "Population")
		End Function
		Friend Shared Function LoadDataTableFromXml(ByVal fileName As String, ByVal tableName As String) As DataTable
			Dim xmlDataSet As New DataSet()
			xmlDataSet.ReadXml(fileName)
			Return xmlDataSet.Tables(tableName)
		End Function
		Friend Shared Function GetDataByAgeAndGender() As IList
			Return AgeStructureTable.AsEnumerable().Select(Function(row) New With {
				Key .GenderAge = New GenderAgeInfo(row.Field(Of String)("Gender"), row.Field(Of String)("Age")),
				Key .Country = row.Field(Of String)("Country"),
				Key .Population = row.Field(Of Long)("Population")
			}).ToList()
		End Function
	End Class
End Namespace
