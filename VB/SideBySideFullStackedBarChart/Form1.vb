Imports System
Imports System.Collections
Imports System.Data
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports System.Xml.Linq
Imports DevExpress.XtraCharts
' ...

Namespace SideBySideFullStackedBarChart
	Partial Public Class Form1
		Inherits Form

		Private stackedBarChart As ChartControl
		Public Sub New()
			InitializeComponent()
		End Sub
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			' Create a new chart.
			stackedBarChart = New ChartControl()
			stackedBarChart.DataSource = AgeStructureDataReader.GetDataByAgeAndGender()
			stackedBarChart.SeriesTemplate.SeriesDataMember = "GenderAge"
			stackedBarChart.SeriesTemplate.ArgumentDataMember = "Country"
			stackedBarChart.SeriesTemplate.ValueDataMembers.AddRange("Population")

			stackedBarChart.SeriesTemplate.View = New SideBySideFullStackedBarSeriesView()

			AddHandler stackedBarChart.BoundDataChanged, AddressOf Me.OnChartBoundDataChanged

			' Access the type-specific options of the diagram.
			Dim diagram As XYDiagram = CType(stackedBarChart.Diagram, XYDiagram)
			diagram.Rotated = True
			diagram.AxisY.Label.TextPattern = "{VP:P}"
			diagram.AxisY.WholeRange.AutoSideMargins = False
			diagram.AxisY.WholeRange.SideMarginsValue = 0

			' Hide the legend (if necessary).
			stackedBarChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True
			stackedBarChart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center
			stackedBarChart.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside
			stackedBarChart.Legend.MaxVerticalPercentage = 20

			' Add a title to the chart (if necessary).
			stackedBarChart.Titles.Add(New ChartTitle())
			stackedBarChart.Titles(0).Text = "Population: Age-Gender Structure"
			stackedBarChart.Titles(0).WordWrap = True

			' Add the chart to the form.
			stackedBarChart.Dock = DockStyle.Fill
			Me.Controls.Add(stackedBarChart)
		End Sub
		Private Sub OnChartBoundDataChanged(ByVal sender As Object, ByVal e As EventArgs)
			For Each series As Series In stackedBarChart.Series
				If Not (TypeOf series.Tag Is GenderAgeInfo) Then
					Return
				End If
				Dim item As GenderAgeInfo = DirectCast(series.Tag, GenderAgeInfo)
				Dim view As SideBySideFullStackedBarSeriesView = TryCast(series.View, SideBySideFullStackedBarSeriesView)
				If view Is Nothing Then
					Return
				End If
				view.StackedGroup = item.Gender
			Next series
		End Sub
	End Class
End Namespace