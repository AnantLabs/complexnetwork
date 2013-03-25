using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Numerics;

using CommonLibrary.Model.Result;
using RandomGraph.Common.Model.Generation;
using CommonLibrary.Model.Attributes;
using RandomGraph.Common.Model;
using StatisticAnalyzer;
using StatisticAnalyzer.Loader;
using StatisticAnalyzer.Analyzer;

namespace StatisticAnalyzerUI
{
    public partial class ExtendedAnalyze : Form
    {
        public string ModelName = "";
        private List<ResultAssembly> assembliesToAnalyze;

        public ExtendedAnalyze(List<ResultAssembly> assemblies)
        {
            InitializeComponent();

            assembliesToAnalyze = assemblies;
        }

        private void ExtendedAnalyze_Load(object sender, EventArgs e)
        {
            this.modelNameTxt.Text = ModelName;
        }

        private void trajectoryAnalyze_Click(object sender, EventArgs e)
        {
            StAnalyzer analyzer = new StAnalyzer(assembliesToAnalyze);
            analyzer.options |= AnalyseOptions.TriangleTrajectory;
            analyzer.ExtendedAnalyze(Convert.ToUInt32(this.stepsToRemoveTxt.Text));
            if (analyzer.Result.trajectoryAvgs.Keys.Count == 0)
            {
                MessageBox.Show("There are no results!");
                return;
            }

            ExtendedGraphic graphic = new ExtendedGraphic(analyzer.Result.trajectoryAvgs,
                analyzer.Result.trajectorySigmas,
                analyzer.Result.parameterLine);
            graphic.Show();         
        }

        private void stepsToRemoveTxt_Leave(object sender, EventArgs e)
        {
            try
            {
                Convert.ToUInt32(this.stepsToRemoveTxt.Text);
            }
            catch (SystemException)
            {
                MessageBox.Show("Count of steps to remove must be a none negative integer!", "Error");
                this.stepsToRemoveTxt.SelectAll();
                this.stepsToRemoveTxt.Focus();
                return;
            }
        }
    }
}
