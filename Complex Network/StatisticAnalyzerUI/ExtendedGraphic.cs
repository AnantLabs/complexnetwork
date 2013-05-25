using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Numerics;

using StatisticAnalyzer.Analyzer;
using ZedGraph;

namespace StatisticAnalyzerUI
{
    public partial class ExtendedGraphic : Form
    {
        public List<StAnalyzeResult> resultsList; // Contains all StAnalyzeResults

        private Color currentColor; // Color of last graph line
        private bool currentPointView; // When true, points of the line are linked
        private ExtendedGraphicCondition parent;

        ZedGraphControl avgsGraphic;
        ZedGraphControl sigmasGraphic;

        public ExtendedGraphic(StAnalyzeResult stAnalyzeResult,
            Color color,
            bool pointView,
            ExtendedGraphicCondition graphicCondition)
        {
            InitializeComponent();

            this.resultsList = new List<StAnalyzeResult>();
            this.resultsList.Add(stAnalyzeResult);
            this.currentColor = color;
            this.currentPointView = pointView;
            this.parent = graphicCondition;

            avgsGraphic = new ZedGraphControl();
            avgsGraphic.Dock = DockStyle.Fill;
            this.resultsTab.TabPages[0].Controls.Add(avgsGraphic);
            avgsGraphic.GraphPane.Title.Text = "Avgs";
            avgsGraphic.GraphPane.XAxis.Title.Text = "Mu";
            avgsGraphic.GraphPane.YAxis.Title.Text = "Avgs";

            sigmasGraphic = new ZedGraphControl();
            sigmasGraphic.Dock = DockStyle.Fill;
            this.resultsTab.TabPages[1].Controls.Add(sigmasGraphic);
            sigmasGraphic.GraphPane.Title.Text = "Sigmas";
            sigmasGraphic.GraphPane.XAxis.Title.Text = "Mu";
            sigmasGraphic.GraphPane.YAxis.Title.Text = "Sigmas";
        }

        public void Add(StAnalyzeResult stAnalyzeResult, Color color, bool pointView)
        {
            this.resultsList.Add(stAnalyzeResult);
            this.currentColor = color;
            this.currentPointView = pointView;

            this.MaximizeBox = true;
            this.WindowState = FormWindowState.Maximized;
            this.MaximizeBox = false;

            DrawGraphics();
        }

        private void ExtendedGraphic_Load(object sender, EventArgs e)
        {
            DrawGraphics();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (0 == this.resultsTab.SelectedIndex)
            {
                avgsGraphic.SaveAs();
            }
            else
            {
                sigmasGraphic.SaveAs();
            }
        }

        private void valuesBtn_Click(object sender, EventArgs e)
        {
            ExtendedValueTable extendedValueTable = new ExtendedValueTable();
            if (0 == this.resultsTab.SelectedIndex)
            {
                
            }
            else
            {
                
            }
            extendedValueTable.Show();
        }

        private void DrawGraphics()
        {
            int length = resultsList.Count();
            PointPairList avgsValues = new PointPairList();
            PointPairList sigmasValues = new PointPairList();

            foreach (double d in resultsList[length - 1].trajectoryAvgs.Keys)
            {
                avgsValues.Add(d, resultsList[length - 1].trajectoryAvgs[d]);
            }

            foreach (double d in resultsList[length - 1].trajectorySigmas.Keys)
            {
                sigmasValues.Add(d, resultsList[length - 1].trajectorySigmas[d]);
            }

            avgsGraphic.GraphPane.Legend.FontSpec.Size = 8;
            LineItem avgsL = avgsGraphic.GraphPane.AddCurve(resultsList[length - 1].parameterLine,
                avgsValues, currentColor, SymbolType.Circle);
            avgsL.IsVisible = this.currentPointView;

            avgsGraphic.AxisChange();
            avgsGraphic.Invalidate();
            avgsGraphic.Refresh();            

            sigmasGraphic.GraphPane.Legend.FontSpec.Size = 8;
            LineItem sigmasL = sigmasGraphic.GraphPane.AddCurve(resultsList[length - 1].parameterLine,
                sigmasValues, currentColor, SymbolType.Circle);
            sigmasL.IsVisible = this.currentPointView;

            sigmasGraphic.AxisChange();
            sigmasGraphic.Invalidate();
            sigmasGraphic.Refresh();
        }

        private void ExtendedGraphic_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.parent != null)
            {
                this.parent.isOpen = false;
            }
        }
    }
}
