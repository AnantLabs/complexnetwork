using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using Core.Enumerations;

namespace Random_Networks_Statistic_Analyzer
{
    public struct ChartProperties
    {
        public ChartProperties(ThickeningType t, double v, ApproximationType at)
        {
            thickeningType = t;
            thickeningValue = v;
            approximationType = at;
        }

        ThickeningType thickeningType;
        double thickeningValue;
        ApproximationType approximationType;
    }

    public partial class AnalyzeCharts : Form
    {
        private int groupId;
        private Dictionary<AnalyzeOption, ChartProperties> chartsProperties;

        public AnalyzeCharts(int gId, Dictionary<AnalyzeOption, ChartProperties> p)
        {
            InitializeComponent();
            groupId = gId;
            chartsProperties = p;
        }

        private void AnalyzeCharts_Load(object sender, EventArgs e)
        {
            foreach (AnalyzeOption opt in chartsProperties.Keys)
            {
                TabPage optTab = new TabPage(opt.ToString());
                ChartTab t = new ChartTab();
                t.Dock = DockStyle.Fill;
                optTab.Controls.Add(t);
                chartTabs.TabPages.Add(optTab);
            }

            /*Chart graphic = new Chart();
            graphic.Titles.Add("Network Size = " + this.research.Size.ToString());

            ChartArea chArea = new ChartArea("Current Level = " + k.ToString());
            chArea.AxisX.Title = "Mu";
            chArea.AxisY.Title = "Order";
            graphic.ChartAreas.Add(chArea);

            Series s = new Series("Current Level = " + k.ToString());
            s.ChartType = SeriesChartType.Line;
            s.Color = Color.Red;
            foreach (KeyValuePair<double, SubGraphsInfo> v in this.research.Result[k])
            {
                s.Points.Add(new DataPoint(v.Key, v.Value.avgOrder));
            }
            graphic.Series.Add(s);

            graphic.Dock = DockStyle.Fill;
            TabPage page = new TabPage("Current Level = " + k.ToString());
            page.Controls.Add(graphic);
            this.graphicsTab.TabPages.Add(page);

            this.graphics.Add(graphic);*/
        }
    }
}
