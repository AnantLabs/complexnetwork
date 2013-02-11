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

        private void fromFileXml_Click(object sender, EventArgs e)
        {
            ResultAssembly result = new ResultAssembly();

            result.Name = result.ID.ToString();
            result.ModelType = typeof(ERModel);
            result.ModelName = result.ModelType.Name;

            result.GenerationParams.Add(GenerationParam.Vertices, GetN());
            result.GenerationParams.Add(GenerationParam.P, GetP());

            AnalizeResult r = new AnalizeResult();
            r.Size = GetN();
            r.TriangleTrajectory = GetDictionary();
            r.trajectoryMu = GetMu();
            r.trajectoryStepCount = (BigInteger)GetDictionary().Count();

            result.Results.Add(r);

            IResultStorage xmlStorage = new XMLResultStorage(this.xmlLocationTxt.Text);
            xmlStorage.Save(result);
        }

        private void fromFileSql_Click(object sender, EventArgs e)
        {

        }

        // Утилиты.

        private int GetN()
        {
            return 0;
        }

        private double GetP()
        {
            return 0.0;
        }

        private BigInteger GetMu()
        {
            return 0;
        }

        private SortedDictionary<int, double> GetDictionary()
        {
            return new SortedDictionary<int, double>();
        }
    }
}
