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
        private GraphicCondition parent;


        private Dictionary<AnalyseOptions, ZedGraphControl> graphs; // Contains all ZedGraphsControls 
        private Dictionary<AnalyseOptions, TabPage> pages; // Contains all tabs

        // Constructor and Load event handler

        public Graphic(StAnalyzeResult stAnalyzeResult, Color color, bool pointView, GraphicCondition graphicCondition)
        {
            this.resultsList = new List<StAnalyzeResult>();
            this.resultsList.Add(stAnalyzeResult);
            this.currentColor = color;
            this.currentPointView = pointView;
            this.parent = graphicCondition;

            this.graphs = new Dictionary<AnalyseOptions, ZedGraphControl>();
            this.pages = new Dictionary<AnalyseOptions, TabPage>();

            InitializeComponent();
        }

        private void Graphic_Load(object sender, EventArgs e)
        {
            if (this.resultsList[0].type != StAnalyzeType.Local)
            {
                ApproximationTxt.Enabled = false;
                Approximation.Enabled = false;
                PropertyTxt.Enabled = false;
                Property.Enabled = false;
            }

            // The only way to properly display the window in maximized state having disabled MaximizeBox.
            this.MaximizeBox = true;
            this.WindowState = FormWindowState.Maximized;
            this.MaximizeBox = false; 

            Initialization();
            foreach (KeyValuePair<AnalyseOptions, SortedDictionary<double, double>> option
                in this.resultsList[0].result)
            {
                ShowInfo(option.Key);
                break; // Bad style. I'll work on this later.
            }
        }

        // Member Functions

        public void Add(StAnalyzeResult stAnalyzeResult, Color color, bool pointView) 
        {
            this.resultsList.Add(stAnalyzeResult);
            this.currentColor = color;
            this.currentPointView = pointView;

            this.MaximizeBox = true;
            this.WindowState = FormWindowState.Maximized;
            this.MaximizeBox = false;

            ShowInfo((AnalyseOptions)Enum.Parse(typeof(AnalyseOptions), optionTabs.SelectedTab.Text));
                
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

            if (zedGraph.GraphPane.CurveList.Count == 0)
            {
                SetNames(zedGraph.GraphPane, option);
            }

            PointPairList points = new PointPairList();
            SortedDictionary<double, double> pointsDictionary;

            double x, y;
            this.resultsList[this.resultsList.Count - 1].result.TryGetValue(option, out pointsDictionary);
            foreach (KeyValuePair<double, double> point in pointsDictionary)
            {
                x = point.Key; 
                y = point.Value;
                if (this.resultsList[0].type == StAnalyzeType.Local && 
                    this.resultsList[0].approximationType != ApproximationTypes.None)
                {
                    HandleApproximation(this.resultsList[0].approximationType, ref x, ref y);
                }
                points.Add(x, y);
            }

            zedGraph.GraphPane.AddCurve(resultsList[resultsList.Count -1].parameterLine, points, currentColor, SymbolType.Circle);
            zedGraph.GraphPane.CurveList[zedGraph.GraphPane.CurveList.Count - 1].IsVisible = this.currentPointView;

            zedGraph.AxisChange();
            zedGraph.Invalidate();
            zedGraph.Refresh();
        }

        private void SetNames(GraphPane pane, AnalyseOptions option)
        {
            pane.Title.Text = option.ToString();

            AnalyzeOptionInfo info = (AnalyzeOptionInfo)(option.GetType().GetField(option.ToString()).
                GetCustomAttributes(typeof(AnalyzeOptionInfo), false)[0]);

            if (this.resultsList[0].type == StAnalyzeType.Global)
            {
                pane.XAxis.Title.Text = info.GXAxis;
                pane.YAxis.Title.Text = info.GYAxis;
            }

            else if (this.resultsList[0].type == StAnalyzeType.Local)
            {
                string x = info.LXAxis, y = info.LYAxis;
                if (this.resultsList[0].approximationType != ApproximationTypes.None)
                {
                    GetApproximationAxisNames(this.resultsList[0].approximationType, ref x, ref y);
                } 
                pane.XAxis.Title.Text = x;
                pane.YAxis.Title.Text = y;
            }
            
        }

        private void GetApproximationAxisNames(ApproximationTypes type, ref string x, ref string y)
        {
            switch (type)
            {
                case ApproximationTypes.Degree:
                    {
                        x = "Ln (" + x + ")";
                        y = "Ln (" + y + ")";
                        break;
                    }
                case ApproximationTypes.Exponential:
                    {
                        y = "Ln (" + y + ")";
                        break;
                    }
                case ApproximationTypes.Gaus:
                    {
                        x = x  + " ^ 2";
                        y = "Ln (" + y + ")";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public ApproximationTypes GetApproximation()
        {
            return this.resultsList[0].approximationType;
        }

        private void ShowInfo(AnalyseOptions option)
        {
            int index = 0;
            for (int i = resultsList.Count - 1; i >= 0; --i)
            {
                if (resultsList[i].result.ContainsKey(option))
                {
                    index = i;
                    break;
                }
            }

            this.ModelNameTxt.Text = this.resultsList[index].modelName;
            this.NetworkSizeTxt.Text = this.resultsList[index].networkSize.ToString();

            if (this.resultsList[0].type == StAnalyzeType.Local)
            {
                this.ApproximationTxt.Text = this.resultsList[0].approximationType.ToString();
                StAnalyzeOptions stAnalyzeOptions;
                this.resultsList[index].options.TryGetValue(option, out stAnalyzeOptions);
                this.PropertyTxt.Text = stAnalyzeOptions.optionValue.ToString();
            }

            double temp;
            this.resultsList[index].resultAvgValues.TryGetValue(option, out temp);
            this.AverageTxt.Text = temp.ToString();
            this.resultsList[index].resultMathWaitings.TryGetValue(option, out temp);
            this.MathWaitingTxt.Text = temp.ToString();
            this.resultsList[index].resultDispersions.TryGetValue(option, out temp);
            this.DispertionTxt.Text = temp.ToString();
            
        }

        private void HandleApproximation(ApproximationTypes type, ref double x, ref double y)
        {
            switch (type)
            {
                case ApproximationTypes.Degree:
                    {
                        x = Math.Log(x);
                        y = Math.Log(y);
                        break;
                    }
                case ApproximationTypes.Exponential:
                    {
                        y = Math.Log(y);
                        break;
                    }
                case ApproximationTypes.Gaus:
                    {
                        x *= x;
                        y = Math.Log(y);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        // Event Handlers

        public void TableClosed()
        {
            //this.ValueTable.Enabled = true;
        }

        private void optionTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            AnalyseOptions currentOption;
            currentOption = (AnalyseOptions)Enum.Parse(typeof(AnalyseOptions), optionTabs.SelectedTab.Text);

            ShowInfo(currentOption);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            AnalyseOptions currentOption;
            currentOption = (AnalyseOptions)Enum.Parse(typeof(AnalyseOptions), optionTabs.SelectedTab.Text);
            ZedGraphControl zedGraph;
            this.graphs.TryGetValue(currentOption, out zedGraph);
            zedGraph.SaveAs();
        }

        // Other Event Handlers - not implemented

        private void ValueTable_Click(object sender, EventArgs e)
        {
        }

        private void Graphic_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.parent.isOpen = false;
        }        
     }
}