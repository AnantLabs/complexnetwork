using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using CommonLibrary.Model.Result;
using RandomGraph.Common.Model.Generation;

//using Origin;

namespace Percolations
{
    // !исправить!
    public partial class MultipleGraphics : Form
    {
        private List<Chart> graphics = new List<Chart>();
        ResultResearch research;

        public MultipleGraphics(ResultResearch r)
        {
            InitializeComponent();

            this.research = r;

            SortedDictionary<double, SortedDictionary<double, SubGraphsInfo>>.KeyCollection keys =
                this.research.Result.Keys;
            foreach (double k in keys)
            {
                Chart graphic = new Chart();
                graphic.Titles.Add("Network Size = " + this.research.Size.ToString());

                ChartArea chArea = new ChartArea("Current Level = " + k.ToString());
                chArea.AxisX.Title = "Mu";
                chArea.AxisY.Title = "Order";
                graphic.ChartAreas.Add(chArea);

                Series s = new Series("Current Level = " + k.ToString());
                s.ChartType = SeriesChartType.Line;
                s.Color = Color.Red;
                foreach (KeyValuePair<double, SubGraphsInfo> v in this.research.Result[k])
                {
                    s.Points.Add(new DataPoint(v.Key, v.Value.avgOrder));
                }
                graphic.Series.Add(s);

                graphic.Dock = DockStyle.Fill;
                TabPage page = new TabPage("Current Level = " + k.ToString());
                page.Controls.Add(graphic);
                this.graphicsTab.TabPages.Add(page);

                this.graphics.Add(graphic);
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < this.graphics.Count; ++i)
                {
                    this.graphics[i].SaveImage(sfd.FileName + "_" + (i + 1).ToString() + ".jpg", 
                        ChartImageFormat.Jpeg);
                }
            }
        }

        private void saveOpj_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < this.graphics.Count; ++i)
                {
                    //SaveOrigin(i + 1, sfd.FileName);
                }
            }
        }

        private void saveTxt_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(sfd.FileName +".txt"))
                {
                    int levelCount = this.research.Result.Count();
                    int muCount = this.research.Result[1].Count();

                    writer.Write("Mu ");
                    for (int i = 1; i <= levelCount; ++i)
                    {
                        writer.Write("AvgOrder" + i.ToString());
                        writer.Write(" ");
                        writer.Write("AvgOrderCount ");
                        writer.Write("SecondMax ");
                        writer.Write("SecondMaxCount ");
                        writer.Write("AvgOrderRest ");
                    }
                    writer.Write("\n");

                    foreach (double mu in this.research.Result[1].Keys)
                    {
                        writer.Write(mu.ToString() + " ");
                        foreach (double level in this.research.Result.Keys)
                        {
                            writer.Write(this.research.Result[level][mu].avgOrder);
                            writer.Write(" ");
                            writer.Write(this.research.Result[level][mu].avgOrderCount);
                            writer.Write(" ");
                            writer.Write(this.research.Result[level][mu].secondMax);
                            writer.Write(" ");
                            writer.Write(this.research.Result[level][mu].secondMaxCount);
                            writer.Write(" ");
                            writer.Write(this.research.Result[level][mu].avgOrderRest);
                            writer.Write(" ");
                        }
                        writer.Write("\n");
                    }

                    foreach (GenerationParam p in this.research.GenerationParams.Keys)
                    {
                        writer.WriteLine(p.ToString() + "=" + 
                            this.research.GenerationParams[p].ToString());
                    }

                    writer.Write("RealizationCount=" + this.research.RealizationCount.ToString());
                }
            }
        }

        private void openTable_Click(object sender, EventArgs e)
        {
        }

        /*private void SaveOrigin(int currentLevel, string fileName)
        {
            // Создание Origin COM обьекта.
            Origin.Application org = new Origin.Application();

            if (org == null)
            {
                MessageBox.Show("Error", "The Origin application coud not be created.");
                return;
            }

            // Инициализация нового пройекта.
            org.NewProject();
            // Добавление workbook-а и получение worksheet-а (таблицы).
            Origin.WorksheetPage orgWkBk = org.WorksheetPages.Add(System.Type.Missing, System.Type.Missing);
            Origin.Worksheet orgWks = (Origin.Worksheet)orgWkBk.Layers[0];

            // Опции таблицы.
            orgWks.Name = "Current Level = " + currentLevel.ToString();
            orgWks.Columns.Add(System.Type.Missing);
            orgWks.Columns.Add(System.Type.Missing);
            orgWks.Columns[0].LongName = "Mu";
            //orgWks.Columns[0].Units = @"(\+(o)C)";
            orgWks.Columns[1].LongName = "Order";
            //orgWks.Columns[1].Units = @"(lb/in\+(2))";
            orgWks.Columns[0].Type = Origin.COLTYPES.COLTYPE_X;
            orgWks.Columns[1].Type = Origin.COLTYPES.COLTYPE_Y;
            orgWks.set_LabelVisible(Origin.LABELTYPEVALS.LT_LONG_NAME, true);
            //orgWks.set_LabelVisible(Origin.LABELTYPEVALS.LT_UNIT, true);

            // Добавление данных в соответсвтующие колонны.
            orgWks.Columns[0].SetData(info[currentLevel].Keys.ToArray(), System.Type.Missing);
            orgWks.Columns[1].SetData(info[currentLevel].Values.ToArray(), System.Type.Missing);

            // Сохранение.
            string path = fileName + "_" + currentLevel.ToString() + ".opj"; 
            if (org.Save(path) == false)
            {
                MessageBox.Show("Error", "The file with name " + path + " could not be saved.");
                return;
            }
        }*/
    }
}
