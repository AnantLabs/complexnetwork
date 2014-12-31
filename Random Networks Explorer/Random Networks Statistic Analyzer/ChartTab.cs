using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Random_Networks_Statistic_Analyzer
{
    public partial class ChartTab : UserControl
    {
        public ChartTab()
        {
            InitializeComponent();
        }

        private void ChartTab_Load(object sender, EventArgs e)
        {
            // TODO clean
            analyzeOptionChart.Titles.Add("my title");

            ChartArea chArea = new ChartArea("my chart area");
            chArea.AxisX.Title = "X axis";
            chArea.AxisY.Title = "Y axis";
            analyzeOptionChart.ChartAreas.Add(chArea);

            Series s = new Series("my serie");
            s.ChartType = SeriesChartType.Line;
            s.Color = Color.Red;
            //foreach (KeyValuePair<double, SubGraphsInfo> v in this.research.Result[k])
            //{
            s.Points.Add(new DataPoint(3, 5));
            s.Points.Add(new DataPoint(4, 6));
            s.Points.Add(new DataPoint(5, 7));
            //}
            analyzeOptionChart.Series.Add(s);
        }
    }
}
