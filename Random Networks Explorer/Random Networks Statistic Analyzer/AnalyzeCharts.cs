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
        public ChartProperties(ThickeningType t, double v, ApproximationType at, Color c)
        {
            thickeningType = t;
            thickeningValue = v;
            approximationType = at;
            color = c;
        }

        ThickeningType thickeningType;
        double thickeningValue;
        ApproximationType approximationType;
        Color color;
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
        }
    }
}
