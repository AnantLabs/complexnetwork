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
        string yAxis;
        ZedGraphControl graphic;
        PointPairList values;

        public ExtendedGraphic(SortedDictionary<double, double> dict, string paramLine, string text)
        {
            InitializeComponent();

            parameterLine = paramLine;
            yAxis = text;

            graphic = new ZedGraphControl();
            graphic.Dock = DockStyle.Fill;
            this.graphicPanel.Controls.Add(graphic);

            values = new PointPairList();

            SortedDictionary<double, double>.KeyCollection keys = dict.Keys;
            foreach (double i in keys)
            {
                values.Add(Convert.ToDouble(i.ToString()), dict[i]);
            }
        }

        private void ExtendedGraphic_Load(object sender, EventArgs e)
        {
            this.Text += " " + yAxis;

            graphic.GraphPane.Title.Text = yAxis;
            graphic.GraphPane.XAxis.Title.Text = "Mu";
            graphic.GraphPane.YAxis.Title.Text = yAxis;

            LineItem l = graphic.GraphPane.AddCurve(parameterLine, values, Color.Black, SymbolType.Circle);

            graphic.AxisChange();
            graphic.Invalidate();
            graphic.Refresh();
        }

        private void save_Click(object sender, EventArgs e)
        {
            graphic.SaveAs();
        }
    }
}
