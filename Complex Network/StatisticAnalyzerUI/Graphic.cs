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
using StatisticAnalyzer.Viewer;
using StatisticAnalyzer.Analyzer;

namespace StatisticAnalyzerUI
{
    public partial class Graphic : Form
    {
        private List<StAnalyzeResult> resultsList; // Contains all StAnalyzeResults

        private Color currentColor; // Color of last graph line
        private bool currentPointView; // When true, points of the line are linked

 //     public StatisticAnalyzer m_parent; // Don't have an idea what is this about

        private SortedDictionary<AnalyseOptions, ZedGraphControl> graphs; // Contains all ZedGraphsControls 
        private SortedDictionary<AnalyseOptions, TabPage> pages; // Contains all tabs

        // Constructor and Load event handler

        public Graphic(StAnalyzeResult stAnalyzeResult, Color color, bool pointView)
        {
            resultsList = new List<StAnalyzeResult>();
            resultsList.Add(stAnalyzeResult);
            currentColor = color; 
            currentPointView = pointView;

            graphs = new SortedDictionary<AnalyseOptions, ZedGraphControl>();
            pages = new SortedDictionary<AnalyseOptions, TabPage>();

            InitializeComponent();
        }

        private void Graphic_Load(object sender, EventArgs e)
        {
            Initialization();
        }

        // Member Functions

        public void Add(StAnalyzeResult stAnalyzeResult, Color color, bool pointView) 
        {
            resultsList.Add(stAnalyzeResult);
            currentColor = color;
            currentPointView = pointView;

            Initialization();
        }

        private void Initialization() // Creates tabs if neccesary. Each tab contains a ZedGraphController.
        {
            foreach (KeyValuePair<AnalyseOptions, SortedDictionary<double, double>> option
                in this.resultsList[resultsList.Count - 1].result)
            {
                if (!pages.ContainsKey(option.Key))
                {
                    TabPage tabPage = new TabPage();
                    tabPage.Text = option.Key.ToString();

                    ZedGraphControl ZedGraph = new ZedGraphControl();
                    ZedGraph.Dock = DockStyle.Fill;
                    tabPage.Controls.Add(ZedGraph);

                    this.optionTabs.Controls.Add(tabPage);
                    this.pages.Add(option.Key, tabPage);
                    this.graphs.Add(option.Key, ZedGraph);
                }

                HandleZedGraph(option.Key);
            }
        }
                    
        private void HandleZedGraph(AnalyseOptions option) //Draws Graph on each tab
        {
            ZedGraphControl zedGraph;
            this.graphs.TryGetValue(option, out zedGraph);

            if (this.resultsList.Count == 1)
            {
                zedGraph.GraphPane.Title.Text = option.ToString();
            }

            PointPairList points = new PointPairList();
            SortedDictionary<double, double> pointsDictionary;

            this.resultsList[this.resultsList.Count - 1].result.TryGetValue(option, out pointsDictionary);
            foreach (KeyValuePair<double, double> point in pointsDictionary)
            {
                points.Add(point.Key, point.Value);
            }

            zedGraph.GraphPane.AddCurve("Mika"/*Name of job or whatever*/, points, this.currentColor, SymbolType.Circle);
            zedGraph.GraphPane.CurveList[zedGraph.GraphPane.CurveList.Count - 1].IsVisible = this.currentPointView;

            zedGraph.AxisChange();
            zedGraph.Invalidate();
            zedGraph.Refresh();
        }

        // Event Handlers

        private void optionTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            AnalyseOptions currentOption;
            currentOption = (AnalyseOptions)Enum.Parse(typeof(AnalyseOptions), optionTabs.SelectedTab.Text);

            int index = 0;
            for (int i = resultsList.Count - 1; i >= 0; --i)
            {
                if (resultsList[i].result.ContainsKey(currentOption))
                {
                    index = i;
                }
            }

            this.ModelName.Text = this.resultsList[index].modelName;
            this.NetworkSizeTxt.Text = this.resultsList[index].networkSize.ToString();
            this.ApproximationTxt.Text = this.resultsList[index].approximationType.ToString();
            this.PropertyTxt.Text = this.resultsList[index].parameterLine;

            double temp;
            this.resultsList[index].resultAvgValues.TryGetValue(currentOption, out temp);
            this.AverageTxt.Text = temp.ToString();
            this.resultsList[index].resultMathWaitings.TryGetValue(currentOption, out temp);
            this.MathWaitingTxt.Text = temp.ToString();
            this.resultsList[index].resultDispersions.TryGetValue(currentOption, out temp);
            this.DispertionTxt.Text = temp.ToString();
        }

        // Other Event Handlers - not implemented

        private void ValueTable_Click(object sender, EventArgs e)
        {
        }

        private void Save_Click(object sender, EventArgs e)
        {
        }

        private void Clear_Click(object sender, EventArgs e)
        {
        }

        private void OnClose(object sender, FormClosingEventArgs e)
        {
        }

        public void TableClosed()
        {
//            this.ValueTable.Enabled = true;
        }
        
    }
}