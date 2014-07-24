using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Core.Enumerations;
using Core.Settings;

namespace Random_Networks_Statistic_Analyzer
{
    public partial class LoadFrom : Form
    {
        public LoadFrom()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void LoadFrom_Load(object sender, EventArgs e)
        {
            InitializeDataStorage();

            xmlStorageDirectoryTxt.Text = StatisticAnalyzerSettings.XMLStorageDirectory;
            txtStorageDirectoryTxt.Text = StatisticAnalyzerSettings.TXTStorageDirectory;
            excelStorageDirectoryTxt.Text = StatisticAnalyzerSettings.ExcelStorageDirectory;
            //databaseTxt.Text = StatisticAnalyzerSettings.ConnectionString;
        }

        private void storageRadio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton checkedRadio = (RadioButton)sender;
            switch (checkedRadio.Name)
            {
                case "xmlStorageRadio":
                    XmlChecked(true);
                    TxtChecked(false);
                    ExcelChecked(false);
                    SqlChecked(false);
                    break;
                case "txtStorageRadio":
                    XmlChecked(false);
                    TxtChecked(true);
                    ExcelChecked(false);
                    SqlChecked(false);
                    break;
                case "excelStorageRadio":
                    XmlChecked(false);
                    TxtChecked(false);
                    ExcelChecked(true);
                    SqlChecked(false);
                    break;
                case "sqlStorageRadio":
                    XmlChecked(false);
                    TxtChecked(false);
                    ExcelChecked(false);
                    SqlChecked(true);
                    break;
                default:
                    break;
            }
        }

        private void xmlStorageBrowseButton_Click(object sender, EventArgs e)
        {
            if (browserDlg.ShowDialog() == DialogResult.OK)
            {
                xmlStorageDirectoryTxt.Text = browserDlg.SelectedPath;
            }
        }

        private void databaseBrowseButton_Click(object sender, EventArgs e)
        {

        }

        private void txtStorageBrowseButton_Click(object sender, EventArgs e)
        {
            if (browserDlg.ShowDialog() == DialogResult.OK)
            {
                txtStorageDirectoryTxt.Text = browserDlg.SelectedPath;
            }
        }

        private void excelStorageBrowseButton_Click(object sender, EventArgs e)
        {
            if (browserDlg.ShowDialog() == DialogResult.OK)
            {
                excelStorageDirectoryTxt.Text = browserDlg.SelectedPath;
            }
        }

        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            StatisticAnalyzerSettings.StorageType = GetDataStorage();

            StatisticAnalyzerSettings.XMLStorageDirectory = xmlStorageDirectoryTxt.Text;
            StatisticAnalyzerSettings.TXTStorageDirectory = txtStorageDirectoryTxt.Text;
            StatisticAnalyzerSettings.ExcelStorageDirectory = excelStorageDirectoryTxt.Text;
            //StatisticAnalyzerSettings.ConnectionString = textBoxConnStr.Text;

            StatisticAnalyzerSettings.Refresh();
            Close();
        }

        #endregion

        #region Utilities

        private void InitializeDataStorage()
        {
            StorageType stType = StatisticAnalyzerSettings.StorageType;
            switch (stType)
            {
                case StorageType.XMLStorage:
                    xmlStorageRadio.Checked = true;
                    break;
                case StorageType.TXTStorage:
                    txtStorageRadio.Checked = true;
                    break;
                case StorageType.ExcelStorage:
                    excelStorageRadio.Checked = true;
                    break;
                case StorageType.SQLStorage:
                    sqlStorageRadio.Checked = true;
                    break;
                default:
                    break;
            }
        }

        private StorageType GetDataStorage()
        {
            if (xmlStorageRadio.Checked == true)
                return StorageType.XMLStorage;
            else if (txtStorageRadio.Checked == true)
                return StorageType.TXTStorage;
            else if (excelStorageRadio.Checked == true)
                return StorageType.ExcelStorage;
            else 
                return StorageType.SQLStorage;
        }

        private void XmlChecked(bool c)
        {
            xmlStorageDirectory.Enabled = c;
            xmlStorageDirectoryTxt.Enabled = c;
            xmlStorageBrowseButton.Enabled = c;
        }

        private void TxtChecked(bool c)
        {
            txtStorageDirectory.Enabled = c;
            txtStorageDirectoryTxt.Enabled = c;
            txtStorageBrowseButton.Enabled = c;
        }

        private void ExcelChecked(bool c)
        {
            excelStorageDirectory.Enabled = c;
            excelStorageDirectoryTxt.Enabled = c;
            excelStorageBrowseButton.Enabled = c;
        }

        private void SqlChecked(bool c)
        {
            database.Enabled = c;
            databaseTxt.Enabled = c;
            databaseBrowseButton.Enabled = c;
        }

        #endregion
    }
}
