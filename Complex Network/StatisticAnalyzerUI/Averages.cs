using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using RandomGraph.Common.Model;
using CommonLibrary.Model.Attributes;
using StatisticAnalyzer.Analyzer;

namespace StatisticAnalyzerUI
{
    public partial class Averages : Form
    {
        StAnalyzeResult result;

        public Averages(StAnalyzeResult r)
        {
            result = r;
            InitializeComponent();
        }

        // Event Handlers

        private void Averages_Load(object sender, EventArgs e)
        {
            string str = result.parameterLine + "\n";

            Dictionary<AnalyseOptions, double>.KeyCollection keys = result.resultAvgValues.Keys;
            foreach (AnalyseOptions opt in keys)
            {
                AnalyzeOptionInfo optInfo = (AnalyzeOptionInfo)(opt.GetType().GetField(opt.ToString()).
                    GetCustomAttributes(typeof(AnalyzeOptionInfo), false)[0]);
                str += optInfo.Name + " = " + result.resultAvgValues[opt].ToString() + ";\n";
            }
            this.InformationTxt.Text = str;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (this.SaveInformationDlg.ShowDialog() == DialogResult.OK)
            {
                FileStream fStream = new FileStream(this.SaveInformationDlg.FileName, FileMode.Create);
                byte[] arr = new UTF8Encoding().GetBytes(this.InformationTxt.Text);
                fStream.Write(arr, 0, arr.Length);
                fStream.Close();
            }
        }
    }
}
