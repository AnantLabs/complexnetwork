using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.ConnectionUI;
using System.Numerics;
using System.IO;

using RandomGraph.Common.Storage;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Result;
using ResultStorage.Storage;
using System.Configuration;

using RandomGraph.Common.Model;
using CommonLibrary.Model.Result;
using RandomGraph.Settings;
using Model.ERModel;

namespace RandomGraphLauncher
{
    // Реализация формы для перенесения информации из одного хранилища данных в другое.
    // 1-ый таб - из .xml файла в БД и обратно.
    // 2-ой таб - из входного файла (без header) в .xml файл или БД, только для траектории (в дальнейшем убрать).
    // 3-ий таб - из входного файла (Analyze Results File) в .xml файл или БД.
    public partial class DataExportImportWindow : Form
    {
        private DataConnectionDialog dcd = new DataConnectionDialog();

        public DataExportImportWindow()
        {
            InitializeComponent();
        }

        // Обработчики сообщений.

        private void Browse_Click(object sender, EventArgs e)
        {
            if (BrowseDlg.ShowDialog() == DialogResult.OK)
            {
                this.LocationTxt.Text = BrowseDlg.SelectedPath;
            }
        }

        private void AddConnection_Click(object sender, EventArgs e)
        {
            dcd = new DataConnectionDialog();
            DataConnectionConfiguration dcs = new DataConnectionConfiguration(null);
            dcs.LoadConfiguration(dcd);

            if (DataConnectionDialog.Show(dcd) == DialogResult.OK)
            {
                switch (this.mainTab.SelectedIndex)
                {
                    case 0:
                        {
                            this.textBoxConnStr.Text = dcd.ConnectionString;
                            break;
                        }
                    case 1:
                        {
                            this.connectionStringTxt.Text = dcd.ConnectionString;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            dcs.SaveConfiguration(dcd);
        }

        private void XML_into_SQL_Button_Click(object sender, EventArgs e)
        {
            IResultStorage sqlStorage = new SQLResultStorage(new ConnectionStringSettings("a", 
                textBoxConnStr.Text, "System.Data.SqlClient"));
            IResultStorage xmlStorage = new XMLResultStorage(LocationTxt.Text);

            FreezeButtons(true);
            TransferData(xmlStorage, sqlStorage);
            FreezeButtons(false);
        }

        private void SQL_into_XML_Button_Click(object sender, EventArgs e)
        {
            IResultStorage sqlStorage = new SQLResultStorage(new ConnectionStringSettings("a", textBoxConnStr.Text, "System.Data.SqlClient"));
            IResultStorage xmlStorage = new XMLResultStorage(LocationTxt.Text);

            FreezeButtons(true);
            TransferData(sqlStorage, xmlStorage);
            FreezeButtons(false);
        }        

        private void externalBrowse_Click(object sender, EventArgs e)
        {
            if (BrowseDlg.ShowDialog() == DialogResult.OK)
            {
                this.externalLocationTxt.Text = BrowseDlg.SelectedPath;
            }
        }

        private void xmlBrowse_Click(object sender, EventArgs e)
        {
            if (BrowseDlg.ShowDialog() == DialogResult.OK)
            {
                this.xmlLocationTxt.Text = BrowseDlg.SelectedPath;
            }
        }

        private void fromFileXml_Click(object sender, EventArgs e)
        {
            if (this.avgCheck.Checked == true)
            {
                MessageBox.Show("Cannot transfer average trajectory into xml.", "Failed");
                return;
            }

            try
            {
                IResultStorage xmlStorage = new XMLResultStorage(this.xmlLocationTxt.Text);

                FreezeButtons(true);
                DirectoryInfo parentDir = new DirectoryInfo(this.externalLocationTxt.Text);
                foreach (DirectoryInfo dir in parentDir.GetDirectories())
                {
                    ReadDirectory(dir.FullName);
                    xmlStorage.Save(CreateAssembly());
                }
                FreezeButtons(false);

                MessageBox.Show("Data transfer succeed.", "Success");
            }
            catch (SystemException)
            {
                MessageBox.Show("Data transfer failed.", "Failed");
            }
        }

        private void fromFileSql_Click(object sender, EventArgs e)
        {
            try
            {
                IResultStorage sqlStorage = new SQLResultStorage(new ConnectionStringSettings("a",
                    this.connectionStringTxt.Text, "System.Data.SqlClient"));

                FreezeButtons(true);
                DirectoryInfo parentDir = new DirectoryInfo(this.externalLocationTxt.Text);
                foreach (DirectoryInfo dir in parentDir.GetDirectories())
                {
                    ReadDirectory(dir.FullName);
                    if (this.avgCheck.Checked == true)
                    {
                        SQLResultStorage st = (SQLResultStorage)sqlStorage;
                        st.SaveTT(CreateAssembly());
                    }
                    else
                    {
                        sqlStorage.Save(CreateAssembly());
                    }
                }
                FreezeButtons(false);

                MessageBox.Show("Data transfer succeed.", "Success");
            }
            catch (SystemException ex)
            {
               MessageBox.Show("Data transfer failed.", "Failed");
            }
        }

        // Утилиты.

        // Перевод информации с одного хранилища данных на другое.
        private void TransferData(IResultStorage from, IResultStorage into)
        {
            try
            {
                List<Guid> intoResultsGUIDs = new List<Guid>();
                foreach (ResultAssembly item in into.LoadAllAssemblies())
                {
                    intoResultsGUIDs.Add(item.ID);
                }
                foreach (ResultAssembly resultID in from.LoadAllAssemblies())
                {
                    if (!intoResultsGUIDs.Contains(resultID.ID))
                    {
                        into.Save(from.Load(resultID.ID));
                    }
                }
                MessageBox.Show("Data transfer succeed.", "Success");
            }
            catch (Exception)
            {
                MessageBox.Show("Data transfer failed.", "Failed");
            }
        }

        private void FreezeButtons(bool freeze)
        {
            switch (this.mainTab.SelectedIndex)
            {
                case 0:
                    {
                        this.Browse.Enabled = !freeze;
                        this.AddConnection.Enabled = !freeze;
                        this.XML_into_SQL_Button.Enabled = !freeze;
                        this.SQL_into_XML_Button.Enabled = !freeze;

                        break;
                    }
                case 1:
                    {
                        this.externalBrowse.Enabled = !freeze;
                        this.xmlBrowse.Enabled = !freeze;
                        this.Connections.Enabled = !freeze;
                        this.fromFileXml.Enabled = !freeze;
                        this.fromFileSql.Enabled = !freeze;

                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        // Значения параметров генерации и результатов анализа из внешнего файла (директории).
        private string fileName = "";
        private int N = 0;
        private double p = 0.0;
        private double mu = 0.0;
        private bool perm = false;
        private List<SortedDictionary<int, double>> dictionaries = 
            new List<SortedDictionary<int, double>>();

        private void ReadDirectory(string fullName)
        {
            DirectoryInfo d = new DirectoryInfo(fullName);
            string dictionaryName = d.Name;

            this.fileName = dictionaryName;

            // Получение значение параметра N из имени каталога.
            int i = 1;
            string paramN = "";
            while (dictionaryName[i] != '_')
            {
                paramN += dictionaryName[i];
                ++i;
            }
            this.N = Convert.ToInt32(paramN);

            // Получение значение параметра p из имени каталога.
            i += 2;
            string paramP = "";
            while (dictionaryName[i] != '_')
            {
                paramP += dictionaryName[i];
                ++i;
            }
            this.p = Convert.ToDouble(paramP);

            // Получение значение параметра mu из имени каталога.
            i += 2;
            string paramMu = "";
            while (dictionaryName[i] != '_')
            {
                paramMu += dictionaryName[i];
                ++i;
            }
            this.mu = Convert.ToDouble(paramMu);

            // Получение значение параметра Permanent из имени каталога.
            ++i;
            string paramPerm = dictionaryName.Substring(i);
            this.perm = (paramPerm == "F") ? false : true;

            // Получение пар значений из файлов данного каталога.
            this.dictionaries.Clear();
            FileInfo[] f = d.GetFiles();
            foreach (FileInfo fInfo in f)
            {
                SortedDictionary<int, double> dict= new SortedDictionary<int, double>();
                StreamReader streamReader;
                using (streamReader = new StreamReader(fInfo.FullName, System.Text.Encoding.Default))
                {
                    string contents;
                    while ((contents = streamReader.ReadLine()) != null)
                    {
                        string first = "", second = "";
                        int j = 0;
                        while (contents[j] != ' ')
                        {
                            first += contents[j];
                            ++j;
                        }

                        second = contents.Substring(j);

                        dict.Add(Convert.ToInt32(first), Convert.ToDouble(second));
                    }
                }

                this.dictionaries.Add(dict);
            }
        }

        // Создание ансамбля из внешних данных.
        private ResultAssembly CreateAssembly()
        {
            ResultAssembly result = new ResultAssembly();

            result.Name = fileName;
            result.ModelType = typeof(ERModel);
            result.ModelName = result.ModelType.Name;
            result.Size = this.N;

            result.GenerationParams.Add(GenerationParam.Vertices, this.N);
            result.GenerationParams.Add(GenerationParam.P, this.p);
            result.GenerationParams.Add(GenerationParam.Permanent, this.perm);

            result.AnalyzeOptionParams[AnalyzeOptionParam.TrajectoryMu] = (double)this.mu;

            foreach (SortedDictionary<int, double> t in this.dictionaries)
            {
                AnalizeResult r = new AnalizeResult();

                r.TriangleTrajectory = t;
                result.AnalyzeOptionParams[AnalyzeOptionParam.TrajectoryStepCount] = (BigInteger)t.Count;

                result.Results.Add(r);
            }

            return result;
        }
    }
}
