using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CarlosAg.ExcelXmlWriter;

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
            Workbook book = new Workbook();
            // Some optional properties of the Document
            book.Properties.Author = "Yerevan State University, Faculty of Informatics and Applied Mathematics," +
                "Chair of Programming and Information Technologies";
            book.Properties.Title = "Extended Value Table";
            book.Properties.Created = DateTime.Now;

            // Add some styles to the Workbook
            WorksheetStyle style = book.Styles.Add("HeaderStyle");
            style.Font.FontName = "TimesNewRoman";
            style.Font.Size = 14;
            style.Font.Bold = true;
            style.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            style.Font.Color = "White";
            style.Interior.Color = "Gray";
            style.Interior.Pattern = StyleInteriorPattern.Solid;

            style = book.Styles.Add("ColumnNames");
            style.Font.FontName = "TimesNewRoman";
            style.Font.Size = 12;
            style.Font.Bold = true;
            style.Alignment.Horizontal = StyleHorizontalAlignment.Center;
            style.Font.Color = "White";
            style.Interior.Color = "Gray";
            style.Interior.Pattern = StyleInteriorPattern.Solid;

            style = book.Styles.Add("Default");
            style.Font.FontName = "TimesNewRoman";
            style.Font.Size = 11;
            style.Alignment.Horizontal = StyleHorizontalAlignment.Center;

            Worksheet sheet = book.Worksheets.Add("Value Table");
            sheet.Table.Columns.Add(new WorksheetColumn(300));
            sheet.Table.Columns.Add(new WorksheetColumn(300));

            WorksheetRow row = sheet.Table.Rows.Add();

            WorksheetCell generationCell = new WorksheetCell(this.generationParametersTxt.Text, "HeaderStyle");
            generationCell.Comment.Data.Text = this.generationParameters.Text;
            row.Cells.Add(generationCell);
            generationCell.MergeAcross = 1;

            row = sheet.Table.Rows.Add();
            row.Cells.Add(this.valuesGrd.Columns[0].HeaderText, DataType.String, "ColumnNames");
            row.Cells.Add(this.valuesGrd.Columns[1].HeaderText, DataType.String, "ColumnNames");

            for (int i = 0; i < this.valuesGrd.Rows.Count; i++)
            {
                row = sheet.Table.Rows.Add();
                row.Cells.Add(new WorksheetCell(this.valuesGrd.Rows[i].Cells[0].Value.ToString(),
                    DataType.String, "Default"));
                row.Cells.Add(new WorksheetCell(this.valuesGrd.Rows[i].Cells[1].Value.ToString(),
                    DataType.String, "Default"));
            }

            saveFileDialog.FileName = "Extended ValueTable.xls";
            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                book.Save(this.saveFileDialog.FileName);
            }
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
