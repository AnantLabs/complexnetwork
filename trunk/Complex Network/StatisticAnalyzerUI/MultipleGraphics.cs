using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

//using Origin;

namespace Percolations
{
    // !исправить!
    public partial class MultipleGraphics : Form
    {
        private List<Chart> graphics = new List<Chart>();
        SortedDictionary<double, SortedDictionary<double, double>> info;

        public MultipleGraphics(int size, SortedDictionary<double, SortedDictionary<double, double>> information)
        {
            InitializeComponent();

            info = information;

            SortedDictionary<double, SortedDictionary<double, double>>.KeyCollection keys =
                information.Keys;
            foreach (double k in keys)
            {
                Chart graphic = new Chart();
                graphic.Titles.Add("Network Size = " + size.ToString());

                ChartArea chArea = new ChartArea("Current Level = " + k.ToString());
                chArea.AxisX.Title = "Mu";
                chArea.AxisY.Title = "Order";
                graphic.ChartAreas.Add(chArea);

                Series s = new Series("Current Level = " + k.ToString());
                s.ChartType = SeriesChartType.Line;
                s.Color = Color.Red;
                foreach (KeyValuePair<double, double> v in information[k])
                {
                    s.Points.Add(new DataPoint(v.Key, v.Value));
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

        private void saveExcel_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
