using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RandomGraph.Common.Model;
using CommonLibrary.Model.Attributes;
using StatisticAnalyzer;

namespace StatisticAnalyzerUI
{
    public partial class ValueTable : Form
    {
        private string m_generationParameters;
        private AnalyzeOptionInfo m_optionInform;
        private StatAnalyzeMode m_mode;
        private SortedDictionary<double, double> m_values;

        public Graphic m_parent;

        public ValueTable(string genParams, GraphicalInformation inform, SortedDictionary<double, double> values)
        {
            InitializeComponent();

            m_generationParameters = genParams;
            m_optionInform = (AnalyzeOptionInfo)(inform.m_option.GetType().
                    GetField(Enum.GetName(typeof(AnalyseOptions), inform.m_option)).
                    GetCustomAttributes(typeof(AnalyzeOptionInfo), false)[0]);
            m_mode = inform.m_mode;
            m_values = values;
        }

        private void ValueTable_Load(object sender, EventArgs e)
        {
            this.GenerationParametersTxt.Text = m_generationParameters;
            this.OptionNameTxt.Text = m_optionInform.Name;
            if (m_mode == StatAnalyzeMode.GlobalMode)
            {
                this.ValuesGrd.Columns[0].HeaderText = m_optionInform.GXAxis;
                this.ValuesGrd.Columns[1].HeaderText = m_optionInform.GYAxis;
            }
            else
            {
                this.ValuesGrd.Columns[0].HeaderText = m_optionInform.LXAxis;
                this.ValuesGrd.Columns[1].HeaderText = m_optionInform.LYAxis;
            }

            int index = 0;
            SortedDictionary<double, double>.KeyCollection keys = m_values.Keys;
            foreach (double key in keys)
            {
                index = this.ValuesGrd.Rows.Add();
                this.ValuesGrd.Rows[index].Cells[0].Value = key;
                this.ValuesGrd.Rows[index].Cells[1].Value = m_values[key];
            }
        }

        private void Print_Click(object sender, EventArgs e)
        {
        }

        private void Save_Click(object sender, EventArgs e)
        {
        }

        private void ValueTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_parent.TableClosed();
        }
    }
}
