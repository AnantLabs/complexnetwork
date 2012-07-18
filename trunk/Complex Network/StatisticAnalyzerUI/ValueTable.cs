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

namespace StatisticAnalyzerUI
{
    public partial class ValueTable : Form
    {
        private SortedDictionary<double, double> m_values;
        public Graphic m_parent;

        public ValueTable(SortedDictionary<double, double> values)
        {
            InitializeComponent();

            m_values = values;
        }

        private void ValueTable_Load(object sender, EventArgs e)
        {
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
