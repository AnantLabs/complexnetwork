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
using RandomGraph.Common.Model.StatAnalyzer;

namespace StatisticAnalyzerUI
{
    public partial class Graphic : Form
    {
        private Color m_curveColor;
        private bool m_points;
        private Dictionary<GenerationParam, string> m_parameters;
        private GraphicalInformation m_graphicalInform;
        private PointPairList m_pointPair;
        private ApproximationTypes m_approximationType;

        public StatisticAnalyzer m_parent;
        public String modelName;

        public Graphic(Color c, bool p, Dictionary<GenerationParam, string> par, GraphicalInformation g,
            SortedDictionary<double, double> d, ApproximationTypes t)
        {
            InitializeComponent();
            Initialization(c, p, par, g, d, t);
        }

        // Event Handlers

        private void Graphic_Load(object sender, EventArgs e)
        {
            if (m_pointPair.Count == 0)
            {
                AnalyzeOptionInfo info = (AnalyzeOptionInfo)(m_graphicalInform.m_option.GetType().
                    GetField(Enum.GetName(typeof(AnalyseOptions), m_graphicalInform.m_option)).
                    GetCustomAttributes(typeof(AnalyzeOptionInfo), false)[0]);
                string notEvaluatedMessage = info.Name + " option value is not evaluated.";
                MessageBox.Show(notEvaluatedMessage);
                this.Close();
            }
        }

        private void ValueTable_Click(object sender, EventArgs e)
        {
            SortedDictionary<double, double> values = new SortedDictionary<double, double>();
            for (int i = 0; i < m_pointPair.Count; ++i)
            {
                values.Add(m_pointPair[i].X, m_pointPair[i].Y);
            }

            this.ValueTable.Enabled = false;
            int lastCurveIndex = ZedGraph.GraphPane.CurveList.Count - 1;
            string lastCurveLabel = ZedGraph.GraphPane.CurveList[lastCurveIndex].Label.Text;
            ValueTable table = new ValueTable(lastCurveLabel, m_graphicalInform, values);
            table.m_parent = this;
            table.Show();
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
            m_parent.DestroyGraphic(m_graphicalInform);
        }

        // Member Functions //

        public void SetAll(Color c, bool p, Dictionary<GenerationParam, string> par, GraphicalInformation g,
            SortedDictionary<double, double> d, ApproximationTypes t)
        {
            Initialization(c, p, par, g, d, t);
        }

        public void RefreshGraphic()
        {
            GraphPane graphPane = this.ZedGraph.GraphPane;

            AnalyzeOptionInfo info = (AnalyzeOptionInfo)(m_graphicalInform.m_option.GetType().
                    GetField(Enum.GetName(typeof(AnalyseOptions), m_graphicalInform.m_option)).
                    GetCustomAttributes(typeof(AnalyzeOptionInfo), false)[0]);
            ApproximationTypeInfo infoApproximation = (ApproximationTypeInfo)(m_approximationType.GetType().
                GetField(Enum.GetName(typeof(ApproximationTypes), m_approximationType)).
                GetCustomAttributes(typeof(ApproximationTypeInfo), false)[0]);

            graphPane.Title.Text = info.Name;

            switch (m_graphicalInform.m_mode)
            {
                case StatAnalyzeMode.GlobalMode:
                    {
                        graphPane.XAxis.Title.Text = info.GXAxis;
                        graphPane.YAxis.Title.Text = info.GYAxis;
                        break;
                    }
                case StatAnalyzeMode.LocalMode:
                case StatAnalyzeMode.MotifMode:
                    {
                        graphPane.XAxis.Title.Text = info.LXAxis;
                        graphPane.YAxis.Title.Text = info.LYAxis;
                        break;
                    }
                default:
                    break;
            }

            string label = "";
            Dictionary<GenerationParam, string>.KeyCollection keys = m_parameters.Keys;
            foreach (GenerationParam p in keys)
            {
                GenerationParamInfo infoGenParam = (GenerationParamInfo)(p.GetType().
                    GetField(Enum.GetName(typeof(GenerationParam), p)).
                    GetCustomAttributes(typeof(GenerationParamInfo), false)[0]);
                label += infoGenParam.Name + " = " + m_parameters[p] + "; ";
            }
            LineItem line = graphPane.AddCurve(label, this.m_pointPair, this.m_curveColor, SymbolType.Circle);

            this.ModelNameTxt.Text = modelName;

            if (m_points)
                line.Line.IsVisible = false;

            this.ZedGraph.AxisChange();
        }

        public void TableClosed()
        {
            this.ValueTable.Enabled = true;
        }

        // Utilities //

        private void Initialization(Color c, bool p, Dictionary<GenerationParam, string> par, GraphicalInformation g,
            SortedDictionary<double, double> d, ApproximationTypes t)
        {
            m_curveColor = c;
            m_points = p;
            m_parameters = par;
            m_graphicalInform = g;
            SetPointPairList(d);
            m_approximationType = t;
        }

        private void SetPointPairList(SortedDictionary<double, double> d)
        {
            m_pointPair = new PointPairList();
            SortedDictionary<double, double>.KeyCollection keys = d.Keys;
            foreach (double key in keys)
                m_pointPair.Add(key, d[key]);
        }

        private void SetM(double m)
        {
            this.MathWaiting.Visible = true;
            this.MathWaitingTxt.Visible = true;

            this.MathWaitingTxt.Text = m.ToString();
        }

        private void SetD(double d)
        {
            this.Dispersion.Visible = true;
            this.DTxt.Visible = true;

            this.DTxt.Text = d.ToString();
        }
    }
}