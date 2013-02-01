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
        string yAxis;
        ZedGraphControl graphic;
        PointPairList values;

        public ExtendedGraphic(SortedDictionary<BigInteger, double> dict, string text)
        {
            yAxis = text;

            graphic = new ZedGraphControl();
            graphic.Dock = DockStyle.Fill;
            this.Controls.Add(graphic);

            values = new PointPairList();

            SortedDictionary<BigInteger, double>.KeyCollection keys = dict.Keys;
            foreach (BigInteger i in keys)
            {
                values.Add(Convert.ToDouble(i.ToString()), dict[i]);
            }

            InitializeComponent();
        }

        private void ExtendedGraphic_Load(object sender, EventArgs e)
        {
            this.Text += " " + yAxis;

            graphic.GraphPane.Title.Text = yAxis;
            graphic.GraphPane.XAxis.Title.Text = "Mu";
            graphic.GraphPane.YAxis.Title.Text = yAxis;

            LineItem l = graphic.GraphPane.AddCurve(yAxis, values, Color.Black, SymbolType.Circle);

            graphic.AxisChange();
            graphic.Invalidate();
            graphic.Refresh();
        }
    }
}
