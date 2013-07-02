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
using System.Configuration;

using RandomGraph.Common.Storage;
using ResultStorage.Storage;
using CommonLibrary.Model.Result;
using ResultStorage.StorageConverter;

namespace RandomGraphLauncher
{
    // Реализация формы для перенесения информации из одного хранилища данных в другое.
    // 1-ый таб - из .xml файлов в БД и обратно (все job-ы).
    // 2-ой таб - из входных файлов (Analyze Results File) в БД (все job-ы).
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
                switch (this.mainTab.SelectedIndex)
                {
                    case 0:
                        {
                            this.xmlStorageLocationTxt.Text = BrowseDlg.SelectedPath;
                            break;
                        }
                    case 1:
                        {
                            this.externalStoreLocationTxt.Text = BrowseDlg.SelectedPath;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }

        private void sqlConnection_Click(object sender, EventArgs e)
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
                            this.sqlStorageConnTxt.Text = dcd.ConnectionString;
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

        private void xmlToSql_Click(object sender, EventArgs e)
        {
            IResultStorage sqlStorage = new SQLResultStorage(new ConnectionStringSettings("a",
                this.sqlStorageConnTxt.Text, "System.Data.SqlClient"));
            IResultStorage xmlStorage = new XMLResultStorage(this.xmlStorageLocationTxt.Text);

            FreezeButtons(true);
            TransferData(xmlStorage, sqlStorage);
            FreezeButtons(false);
        }

        private void sqlToXml_Click(object sender, EventArgs e)
        {
            IResultStorage sqlStorage = new SQLResultStorage(new ConnectionStringSettings("a", 
                this.sqlStorageConnTxt.Text, "System.Data.SqlClient"));
            IResultStorage xmlStorage = new XMLResultStorage(this.xmlStorageLocationTxt.Text);

            FreezeButtons(true);
            TransferData(sqlStorage, xmlStorage);
            FreezeButtons(false);
        }        

        private void fromFileSql_Click(object sender, EventArgs e)
        {
            try
            {
                SQLResultStorage sqlStorage = new SQLResultStorage(new ConnectionStringSettings("a",
                    this.connectionStringTxt.Text, "System.Data.SqlClient"));

                FreezeButtons(true);
                ResultsFileConverter converter = 
                    new ResultsFileConverter(this.externalStoreLocationTxt.Text);
                converter.ReadRootDirectory();
                converter.Save(sqlStorage);
                FreezeButtons(false);

                MessageBox.Show("Data transfer succeed.", "Success");
            }
            catch (SystemException)
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
                        this.xmlBrowse.Enabled = !freeze;
                        this.sqlConnection.Enabled = !freeze;
                        this.xmlToSql.Enabled = !freeze;
                        this.sqlToXml.Enabled = !freeze;

                        break;
                    }
                case 1:
                    {
                        this.externalBrowse.Enabled = !freeze;
                        this.xmlBrowse.Enabled = !freeze;
                        this.connections.Enabled = !freeze;
                        this.fromFileSql.Enabled = !freeze;

                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
