using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StatisticAnalyzerUI
{
    public partial class ExtendedValueTable : Form
    {
        private string header;
        private string genParams;
        private SortedDictionary<double, double> valuesToFill;

        public ExtendedValueTable(string headerText, string generationParams,
            SortedDictionary<double, double> val)
        {
            this.header = headerText;
            this.genParams = generationParams;
            valuesToFill = val;

            InitializeComponent();
        }

        // Event Handlers

        private void ExtendedValueTable_Load(object sender, EventArgs e)
        {
            this.Text = this.header;
            this.generationParametersTxt.Text = this.genParams;
            this.valuesGrd.Columns[0].HeaderText = "Mu";
            this.valuesGrd.Columns[1].HeaderText = this.header;

            SetValues();
        }

        private void excelButton_Click(object sender, EventArgs e)
        {

        }

        private void Print_Click(object sender, EventArgs e)
        {

        }

        private void SetValues()
        {
            this.valuesGrd.Rows.Clear();
            int dataIndex;
            foreach (KeyValuePair<double, double> val in this.valuesToFill)
            {
                dataIndex = this.valuesGrd.Rows.Add();
                this.valuesGrd.Rows[dataIndex].Cells[0].Value = val.Key;
                this.valuesGrd.Rows[dataIndex].Cells[1].Value = val.Value;
            }
        }
    }
}
