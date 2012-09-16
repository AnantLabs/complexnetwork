using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RandomGraph.Common.Model;
using CommonLibrary.Model.Attributes;
using StatisticAnalyzer.Analyzer;
using StatisticAnalyzer.Viewer;
using CarlosAg.ExcelXmlWriter;

namespace StatisticAnalyzerUI
{
    public partial class ValueTable : Form
    {
        private ValueTableCaller valueTableCaller;
        private Graphic parent;
        private AnalyseOptions currentOption;
        private StAnalyzeResult currentResult;
        private Dictionary<string, AnalyseOptions> optionNames;
        private bool isFirst = true;

        public ValueTable(AnalyseOptions option, Graphic caller)
        {
            this.parent = caller;
            this.currentOption = option;
            this.valueTableCaller = ValueTableCaller.Graphic;
            InitializeComponent();
        }

        public ValueTable(StAnalyzeResult stAnalyzeResult)
        {
            this.valueTableCaller = ValueTableCaller.StatisticAnalyzer;
            this.currentResult = stAnalyzeResult;
            this.currentOption = this.currentResult.result.Keys.First<AnalyseOptions>();
            InitializeComponent();
        }

        private void ValueTable_Load(object sender, EventArgs e)
        {
            this.optionNames = new Dictionary<string, AnalyseOptions>();

            if (this.valueTableCaller == ValueTableCaller.Graphic)
            {
                foreach (StAnalyzeResult res in parent.resultsList)
                {
                    generationCmbBox.Items.Add(res.parameterLine);
                    if (res.result.ContainsKey(this.currentOption))
                    {
                        this.currentResult = res;
                        this.generationCmbBox.Text = res.parameterLine;
                        // The last command calls generationCmbBox_SelectedIndexChanged event handler,
                        // wich calls SetValues, AddOptions. etc.. 
                    }
                }
                                                                  
            }

            else
            {
                this.generationCmbBox.Items.Add(this.currentResult.parameterLine);
                this.generationCmbBox.Text = generationCmbBox.Items[0].ToString();
            }

            if (this.currentResult.type != StAnalyzeType.Local)
            {
                DisableApproximation();
            }
            else
            {
                EnableApproximation();
                this.approximationTxt.Text = currentResult.approximationType.ToString();
            }

            this.realizationCountTxt.Text = currentResult.realizationsCount.ToString();

        }

        private void AddOptions()
        {
            this.optionCmbBox.Items.Clear();
            this.optionNames.Clear();
            AnalyzeOptionInfo info;

            Dictionary<AnalyseOptions, SortedDictionary<double, double>>.KeyCollection keyOptions = currentResult.result.Keys;
            foreach (AnalyseOptions option in keyOptions)
            {
                info = (AnalyzeOptionInfo)(option.GetType().GetField(option.ToString()).
                    GetCustomAttributes(typeof(AnalyzeOptionInfo), false)[0]);

                this.optionNames.Add(info.Name, option);
                this.optionCmbBox.Items.Add(info.Name);
                if (option == this.currentOption)
                {
                    this.optionCmbBox.Text = info.Name;
                }
            }
        }

        private void EnableApproximation()
        {
            this.apprLabel.Enabled = true;
            this.approximationTxt.Enabled = true;
        }

        private void DisableApproximation()
        {
            this.apprLabel.Enabled = false;
            this.approximationTxt.Enabled = false;
        }
    
        private void SetValues()
        {
            this.ValuesGrd.Rows.Clear();

            int dataIndex;
            SortedDictionary<double, double> pointsDictionary;
            double x, y;

            this.currentResult.result.TryGetValue(this.currentOption, out pointsDictionary);
            foreach (KeyValuePair<double, double> point in pointsDictionary)
            {
                x = point.Key;
                y = point.Value;
                if (this.currentResult.type == StAnalyzeType.Local && currentResult.approximationType != ApproximationTypes.None)
                {
                    Graphic.HandleApproximation(currentResult.approximationType, ref x, ref y);
                }
                dataIndex = this.ValuesGrd.Rows.Add();
                this.ValuesGrd.Rows[dataIndex].Cells[0].Value = x;
                this.ValuesGrd.Rows[dataIndex].Cells[1].Value = y;
            }
        }

        private void SetColumnNames()
        {
            AnalyzeOptionInfo info = (AnalyzeOptionInfo)(this.currentOption.GetType().GetField(this.currentOption.ToString()).
                GetCustomAttributes(typeof(AnalyzeOptionInfo), false)[0]);

            if (this.currentResult.type == StAnalyzeType.Global)
            {
                this.ValuesGrd.Columns[0].HeaderText = info.GXAxis;
                this.ValuesGrd.Columns[1].HeaderText = info.GYAxis;
            }

            else if (this.currentResult.type == StAnalyzeType.Local)
            {
                string x = info.LXAxis, y = info.LYAxis;
                if (currentResult.approximationType != ApproximationTypes.None)
                {
                    Graphic.GetApproximationAxisNames(this.currentResult.approximationType, ref x, ref y);
                }

                this.ValuesGrd.Columns[0].HeaderText = x;
                this.ValuesGrd.Columns[1].HeaderText = y;
            }

        }
        
        private void Print_Click(object sender, EventArgs e)
        {
            //TODO
        }


        private void ValueTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.valueTableCaller == ValueTableCaller.Graphic)
            {
                parent.TableClosed();
            }
        }
         

        private void generationCmbBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.valueTableCaller == ValueTableCaller.Graphic)
            {
                this.currentResult = this.parent.resultsList[this.generationCmbBox.SelectedIndex];
                if (this.isFirst == false)
                {
                    this.currentOption = this.currentResult.result.Keys.First<AnalyseOptions>();
                }
                isFirst = false;
            }

            AddOptions();
        }

        private void optionCmbBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AnalyseOptions option;
            this.optionNames.TryGetValue(optionCmbBox.Text, out option);
            this.currentOption = option;
            SetColumnNames();
            SetValues();
        }

        private void excelButton_Click(object sender, EventArgs e)
        {
            Workbook book = new Workbook();
            // Some optional properties of the Document
            book.Properties.Author = "Yerevan State University, Faculty of Informatics and Applied Mathematics," +
                "Chair of Programming and Information Technologies";
            book.Properties.Title = "Value Table";
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
            sheet.Table.Columns.Add(new WorksheetColumn(220));
            sheet.Table.Columns.Add(new WorksheetColumn(220));

            WorksheetRow row = sheet.Table.Rows.Add();

            WorksheetCell generationCell = new WorksheetCell(this.generationCmbBox.Text, "HeaderStyle");
            generationCell.Comment.Data.Text = GenerationParameters.Text;
            row.Cells.Add(generationCell);
            generationCell.MergeAcross = 1;

            row = sheet.Table.Rows.Add();
            WorksheetCell optionCell = new WorksheetCell(this.optionCmbBox.Text, "HeaderStyle");
            row.Cells.Add(optionCell);
            optionCell.Comment.Data.Text = OptionNameLabel.Text;
            optionCell.MergeAcross = 1;

            if (this.currentResult.type == StAnalyzeType.Local)
            {
                row = sheet.Table.Rows.Add();
                WorksheetCell approximationCell = new WorksheetCell(this.apprLabel.Text + " - " + 
                        this.currentResult.approximationType.ToString() , "HeaderStyle");
                row.Cells.Add(approximationCell);
                approximationCell.MergeAcross = 1;
            }

            row = sheet.Table.Rows.Add();
            WorksheetCell realizationCountCell = new WorksheetCell(this.realizationCountLabel.Text
                + " = " + this.realizationCountTxt.Text, "HeaderStyle");
            row.Cells.Add(realizationCountCell);
            realizationCountCell.MergeAcross = 1;

            row = sheet.Table.Rows.Add();
            row.Cells.Add(this.ValuesGrd.Columns[0].HeaderText, DataType.String, "ColumnNames");
            row.Cells.Add(this.ValuesGrd.Columns[1].HeaderText, DataType.String, "ColumnNames");
            
            // Generate values 
            for (int i = 0; i < this.ValuesGrd.Rows.Count; i++)
            {
                row = sheet.Table.Rows.Add();
                row.Cells.Add(new WorksheetCell(this.ValuesGrd.Rows[i].Cells[0].Value.ToString(), 
                    DataType.String, "Default"));
                row.Cells.Add(new WorksheetCell(this.ValuesGrd.Rows[i].Cells[1].Value.ToString(),
                    DataType.String, "Default"));
            }

            saveFileDialog.FileName = "ValueTable.xls";
            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                book.Save(this.saveFileDialog.FileName);
            }
        }
    }

    enum ValueTableCaller
    {
        StatisticAnalyzer,
        Graphic
    }
}
