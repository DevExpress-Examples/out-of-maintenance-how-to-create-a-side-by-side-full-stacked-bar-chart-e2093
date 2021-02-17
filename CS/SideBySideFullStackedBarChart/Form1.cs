using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DevExpress.XtraCharts;
// ...

namespace SideBySideFullStackedBarChart {
    public partial class Form1 : Form {
        ChartControl stackedBarChart;
        public Form1() {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e) {
            // Create a new chart.
            stackedBarChart = new ChartControl();
            stackedBarChart.DataSource = AgeStructureDataReader.GetDataByAgeAndGender();
            stackedBarChart.SeriesTemplate.SeriesDataMember = "GenderAge";
            stackedBarChart.SeriesTemplate.ArgumentDataMember = "Country";
            stackedBarChart.SeriesTemplate.ValueDataMembers.AddRange("Population");        
            
            stackedBarChart.SeriesTemplate.View = new SideBySideFullStackedBarSeriesView();
            
            stackedBarChart.BoundDataChanged += this.OnChartBoundDataChanged;

            // Access the type-specific options of the diagram.
            XYDiagram diagram = (XYDiagram)stackedBarChart.Diagram;
            diagram.Rotated = true;
            diagram.AxisY.Label.TextPattern = "{VP:P}";
            diagram.AxisY.WholeRange.AutoSideMargins = false;
            diagram.AxisY.WholeRange.SideMarginsValue = 0;

            // Hide the legend (if necessary).
            stackedBarChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            stackedBarChart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            stackedBarChart.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            stackedBarChart.Legend.MaxVerticalPercentage = 20;

            // Add a title to the chart (if necessary).
            stackedBarChart.Titles.Add(new ChartTitle());
            stackedBarChart.Titles[0].Text = "Population: Age-Gender Structure";
            stackedBarChart.Titles[0].WordWrap = true;            

            // Add the chart to the form.
            stackedBarChart.Dock = DockStyle.Fill;
            this.Controls.Add(stackedBarChart);
        }
        private void OnChartBoundDataChanged(object sender, EventArgs e) {
            foreach (Series series in stackedBarChart.Series) {
                if (!(series.Tag is GenderAgeInfo)) return;
                GenderAgeInfo item = (GenderAgeInfo)series.Tag;
                SideBySideFullStackedBarSeriesView view = series.View as SideBySideFullStackedBarSeriesView;
                if (view == null) return;
                view.StackedGroup = item.Gender;
            }
        }
    }
}