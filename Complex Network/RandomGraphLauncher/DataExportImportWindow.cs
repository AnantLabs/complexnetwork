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
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Result;
using ResultStorage.Storage;

using RandomGraph.Common.Model;
using CommonLibrary.Model.Result;
using RandomGraph.Settings;
using Model.ERModel;

using ResultStorage.StorageConverter;

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
                    case 2:
                        {
                            this.textBox3.Text = dcd.ConnectionString;
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
                TrajectoryFileConverter converter = 
                    new TrajectoryFileConverter(this.externalLocationTxt.Text);
                converter.ReadRootDirectory();
                converter.Save(xmlStorage, this.avgCheck.Checked);
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
                TrajectoryFileConverter converter =
                    new TrajectoryFileConverter(this.externalLocationTxt.Text);
                converter.ReadRootDirectory();
                converter.Save(sqlStorage, this.avgCheck.Checked);
                FreezeButtons(false);

                MessageBox.Show("Data transfer succeed.", "Success");
            }
            catch (SystemException ex)
            {
               MessageBox.Show("Data transfer failed.", "Failed");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (BrowseDlg.ShowDialog() == DialogResult.OK)
            {
                this.textBox2.Text = BrowseDlg.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (BrowseDlg.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = BrowseDlg.SelectedPath;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == true)
            {
                MessageBox.Show("Not supported.", "Failed");
                return;
            }

            try
            {
                IResultStorage xmlStorage = new XMLResultStorage(this.textBox1.Text);

                FreezeButtons(true);
                ResultsFileConverter converter =
                    new ResultsFileConverter(this.textBox2.Text);
                converter.ReadRootDirectory();
                converter.Save(xmlStorage, false);
                FreezeButtons(false);

                MessageBox.Show("Data transfer succeed.", "Success");
            }
            catch (SystemException)
            {
                MessageBox.Show("Data transfer failed.", "Failed");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == true)
            {
                MessageBox.Show("Not supported.", "Failed");
                return;
            }

            try
            {
                IResultStorage sqlStorage = new SQLResultStorage(new ConnectionStringSettings("a",
                    this.textBox3.Text, "System.Data.SqlClient"));

                FreezeButtons(true);
                ResultsFileConverter converter =
                    new ResultsFileConverter(this.textBox2.Text);
                converter.ReadRootDirectory();
                converter.Save(sqlStorage, false);
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
    }
}
