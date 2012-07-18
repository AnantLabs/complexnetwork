using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

using RandomGraph.Common.Model;
using CommonLibrary.Model.Attributes;
using RandomGraph.Common.Model.Generation;

namespace StatisticAnalyzerUI
{
    public partial class Graphic : Form
    {
        private Color m_curveColor;
        private bool m_points;
        private PointPairList m_pointPair;
        public StatisticAnalyzer m_parent;

        public Graphic(Color c, bool p, SortedDictionary<double, double> d)
        {
            InitializeComponent();
            Initialization(c, p, d);
        }

        // Event Handlers

        private void Graphic_Load(object sender, EventArgs e)
        {
            if (m_pointPair.Count == 0) // если нет точек для рисования графика
            {
                MessageBox.Show("There are no points.");
                this.Close();
            }
        }

        private void ValueTable_Click(object sender, EventArgs e)
        {
        }

        private void Save_Click(object sender, EventArgs e)
        {
            this.ZedGraph.SaveAs();
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            this.ZedGraph.GraphPane.CurveList.Clear();
            this.ZedGraph.Visible = false;
            this.ZedGraph.Visible = true;
        }

        private void OnClose(object sender, FormClosingEventArgs e)
        {
        }

        // Member Functions //

        public void RefreshGraphic()
        {
            GraphPane graphPane = this.ZedGraph.GraphPane;
            graphPane.Title.Text = "Graphic Name";
            LineItem line = graphPane.AddCurve("A line information", 
                this.m_pointPair, 
                this.m_curveColor, 
                SymbolType.Circle);

            if (m_points)
                line.Line.IsVisible = false;

            this.ZedGraph.AxisChange();
        }

        public void TableClosed()
        {
            this.ValueTable.Enabled = true;
        }

        // Utilities //

        private void Initialization(Color c, bool p, SortedDictionary<double, double> d)
        {
            m_curveColor = c;
            m_points = p;
            SetPointPairList(d);
        }

        private void SetPointPairList(SortedDictionary<double, double> d)
        {
            m_pointPair = new PointPairList();
            SortedDictionary<double, double>.KeyCollection keys = d.Keys;
            foreach (double key in keys)
                m_pointPair.Add(key, d[key]);
        }
    }
}