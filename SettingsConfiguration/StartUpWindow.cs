using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Data.ConnectionUI;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;


namespace SettingsConfiguration
{
    public partial class StartUpWindow : Form
    {
        private DataConnectionDialog dcd = new DataConnectionDialog();

        public StorgeProvider Storge { get; set; }
        public string StorageDirectory { get; set; }
        public string ConnectionString { get; set; }

        public StartUpWindow(StorgeProvider storge,
                             string storageDirectory,
                             string connectionString)
        {
            this.Storge = storge;
            this.StorageDirectory = storageDirectory;
            this.ConnectionString = connectionString;

            InitializeComponent();

            if (Storge == StorgeProvider.XMLProvider)
            {
                this.XMLRadio.Checked = true;
                XMLChecked();
            }
            else
            {
                this.SQLRadio.Checked = true;
                SQLChecked();
            }
            this.LocationTxt.Text = StorageDirectory;
            this.textBoxConnStr.Text = ConnectionString;
        }

        private void XMLChecked()
        {
            this.Location.Enabled = true;
            this.LocationTxt.Enabled = true;
            this.Browse.Enabled = true;

            this.LabelConnStr.Enabled = false;
            this.textBoxConnStr.Enabled = false;
            this.AddConnection.Enabled = false;
            Storge = StorgeProvider.XMLProvider;
        }

        private void SQLChecked()
        {
            this.LabelConnStr.Enabled = true;
            this.textBoxConnStr.Enabled = true;
            this.AddConnection.Enabled = true;

            this.Location.Enabled = false;
            this.LocationTxt.Enabled = false;
            this.Browse.Enabled = false;
            Storge = StorgeProvider.SQLProvider;
        }

        private void XMLRadio_CheckedChanged(object sender, EventArgs e)
        {
            XMLChecked();
        }

        private void SQLRadio_CheckedChanged(object sender, EventArgs e)
        {
            SQLChecked();
        }

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

        private void Ok_Click(object sender, EventArgs e)
        {
            this.StorageDirectory = LocationTxt.Text;
            this.ConnectionString = textBoxConnStr.Text;
        }
    }

    public enum StorgeProvider
    {
        XMLProvider,
        SQLProvider
    }
}
