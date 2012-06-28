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
using StatisticAnalyzer;

namespace StatisticAnalyzerUI
{
    public partial class Information : Form
    {
        public string m_parameterLine;
        public string[] m_optionNames;
        public string[] m_averageValues;

        public Information(SortedDictionary<AnalyseOptions, double> results)
        {
            InitializeComponent();

            m_optionNames = new string[results.Count];
            m_averageValues = new string[results.Count];

            SortedDictionary<AnalyseOptions, double>.KeyCollection keys = results.Keys;
            int index = 0;
            foreach (AnalyseOptions key in keys)
            {
                AnalyzeOptionInfo info = (AnalyzeOptionInfo)(key.GetType().
                    GetField(Enum.GetName(typeof(AnalyseOptions), key)).
                    GetCustomAttributes(typeof(AnalyzeOptionInfo), false)[0]);

                m_optionNames[index] = info.Name;
                m_averageValues[index] = " = " + results[key];

                ++index;
            }
        }

        // Event Handlers

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

        // Member Functions //

        public void RefreshInformation()
        {
            string str = this.m_parameterLine + "\n";
            for (int i = 0; i < m_optionNames.Count(); ++i)
            {
                str += m_optionNames[i] + m_averageValues[i] + ";\n";
            }
            this.InformationTxt.Text = str;
        }
    }
}
