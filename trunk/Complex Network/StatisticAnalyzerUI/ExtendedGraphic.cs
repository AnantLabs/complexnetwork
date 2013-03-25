using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Numerics;

using ZedGraph;

namespace StatisticAnalyzerUI
{
    public partial class ExtendedGraphic : Form
    {
        string parameterLine;
        ZedGraphControl avgsGraphic;
        ZedGraphControl sigmasGraphic;
        PointPairList avgsValues;
        PointPairList sigmasValues;

        public ExtendedGraphic(SortedDictionary<double, double> avgsDict, 
            SortedDictionary<double, double> sigmasDict, string paramLine)
        {
            InitializeComponent();

            parameterLine = paramLine;

            avgsGraphic = new ZedGraphControl();
            avgsGraphic.Dock = DockStyle.Fill;
            this.resultsTab.TabPages[0].Controls.Add(avgsGraphic);
            avgsValues = new PointPairList();

            SortedDictionary<double, double>.KeyCollection keys = avgsDict.Keys;
            foreach (double i in keys)
            {
                avgsValues.Add(Convert.ToDouble(i.ToString()), avgsDict[i]);
            }

            sigmasGraphic = new ZedGraphControl();
            sigmasGraphic.Dock = DockStyle.Fill;
            this.resultsTab.TabPages[1].Controls.Add(sigmasGraphic);
            sigmasValues = new PointPairList();

            keys = sigmasDict.Keys;
            foreach (double i in keys)
            {
                sigmasValues.Add(Convert.ToDouble(i.ToString()), sigmasDict[i]);
            }
        }

        private void ExtendedGraphic_Load(object sender, EventArgs e)
        {
            avgsGraphic.GraphPane.Title.Text = "Avgs";
            avgsGraphic.GraphPane.XAxis.Title.Text = "Mu";
            avgsGraphic.GraphPane.YAxis.Title.Text = "Avgs";

            avgsGraphic.GraphPane.Legend.FontSpec.Size = 8;
            LineItem avgsL = avgsGraphic.GraphPane.AddCurve(parameterLine, 
                avgsValues, Color.Black, SymbolType.Circle);

            avgsGraphic.AxisChange();
            avgsGraphic.Invalidate();
            avgsGraphic.Refresh();

            sigmasGraphic.GraphPane.Title.Text = "Sigmas";
            sigmasGraphic.GraphPane.XAxis.Title.Text = "Mu";
            sigmasGraphic.GraphPane.YAxis.Title.Text = "Sigmas";

            sigmasGraphic.GraphPane.Legend.FontSpec.Size = 8;
            LineItem sigmasL = sigmasGraphic.GraphPane.AddCurve(parameterLine,
                sigmasValues, Color.Black, SymbolType.Circle);

            sigmasGraphic.AxisChange();
            sigmasGraphic.Invalidate();
            sigmasGraphic.Refresh();
        }

        private void ExtendedGraphic_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        /*private void save_Click(object sender, EventArgs e)
        {
            avgsGraphic.SaveAs();
            sigmasGraphic.SaveAs();
        }*/
    }
}
