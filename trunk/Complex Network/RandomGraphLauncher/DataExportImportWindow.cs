﻿using System;
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
using CommonLibrary.Model.Result;
using RandomGraph.Settings;
using Model.ERModel;

namespace RandomGraphLauncher
{
    // Реализация формы для проверки соответствия модели.
    public partial class DataExportImportWindow : Form
    {
        private DataConnectionDialog dcd = new DataConnectionDialog();
        private IResultStorage storageManager;

        public string StorageDirectory { get; set; }
        public string ConnectionString { get; set; }

        public DataExportImportWindow()
        {
            this.storageManager = Options.StorageManager;
            this.StorageDirectory = Options.StorageDirectory;
            this.ConnectionString = Options.ConnectionString;

            InitializeComponent();

            this.LocationTxt.Text = StorageDirectory;
            this.textBoxConnStr.Text = ConnectionString;
            this.xmlLocationTxt.Text = StorageDirectory;
            this.connectionStringTxt.Text = ConnectionString;
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

            if (dcd.SelectedDataProvider != null && dcd.SelectedDataSource != null)
            {
                dcd.ConnectionString = ConnectionString;
            }
            if (DataConnectionDialog.Show(dcd) == DialogResult.OK)
            {
                textBoxConnStr.Text = dcd.ConnectionString;
            }
            dcs.SaveConfiguration(dcd);
        }

        private void XML_into_SQL_Button_Click(object sender, EventArgs e)
        {
            IResultStorage sqlStorage = new SQLResultStorage(new ConnectionStringSettings("a", textBoxConnStr.Text, "System.Data.SqlClient"));
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
                MessageBox.Show("Data transfer succeed", "Success");
            }
            catch (Exception)
            {
                MessageBox.Show("Data transfer failed", "Failed");
            }
        }

        private void FreezeButtons(bool freeze)
        {
            Browse.Enabled = !freeze;
            AddConnection.Enabled = !freeze;
            XML_into_SQL_Button.Enabled = !freeze;
            SQL_into_XML_Button.Enabled = !freeze;
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

        private void Connections_Click(object sender, EventArgs e)
        {
            dcd = new DataConnectionDialog();
            DataConnectionConfiguration dcs = new DataConnectionConfiguration(null);
            dcs.LoadConfiguration(dcd);

            if (dcd.SelectedDataProvider != null && dcd.SelectedDataSource != null)
            {
                dcd.ConnectionString = ConnectionString;
            }
            if (DataConnectionDialog.Show(dcd) == DialogResult.OK)
            {
                textBoxConnStr.Text = dcd.ConnectionString;
            }
            dcs.SaveConfiguration(dcd);
        }

        private int N = 0;
        private double p = 0.0;
        private double mu = 0.0;
        private bool perm = false;
        private List<SortedDictionary<int, double>> dictionaries = new List<SortedDictionary<int, double>>();

        private void fromFileXml_Click(object sender, EventArgs e)
        {
            ReadDirectory();            

            IResultStorage xmlStorage = new XMLResultStorage(this.xmlLocationTxt.Text);
            xmlStorage.Save(CreateAssembly());
        }

        private void fromFileSql_Click(object sender, EventArgs e)
        {
            ReadDirectory();

            IResultStorage sqlStorage = new SQLResultStorage(new ConnectionStringSettings("a", 
                this.connectionStringTxt.Text, "System.Data.SqlClient"));
            sqlStorage.Save(CreateAssembly());
        }

        // Утилиты.

        private void ReadDirectory()
        {
            DirectoryInfo d = new DirectoryInfo(this.externalLocationTxt.Text);
            string dictionaryName = d.Name;

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

        private ResultAssembly CreateAssembly()
        {
            ResultAssembly result = new ResultAssembly();

            result.Name = result.ID.ToString();
            result.ModelType = typeof(ERModel);
            result.ModelName = result.ModelType.Name;

            result.GenerationParams.Add(GenerationParam.Vertices, this.N);
            result.GenerationParams.Add(GenerationParam.P, this.p);
            result.GenerationParams.Add(GenerationParam.Permanent, this.perm);

            foreach (SortedDictionary<int, double> t in this.dictionaries)
            {
                AnalizeResult r = new AnalizeResult();

                r.Size = this.N;
                r.TriangleTrajectory = t;
                r.trajectoryMu = (BigInteger)this.mu;
                r.trajectoryStepCount = (BigInteger)this.dictionaries.Count;

                result.Results.Add(r);
            }

            return result;
        }
    }
}
