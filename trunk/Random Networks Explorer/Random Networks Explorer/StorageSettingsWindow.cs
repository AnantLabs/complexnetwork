using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Core;
using Core.Enumerations;

namespace RandomNetworksExplorer
{
    public partial class StorageSettingsWindow : Form
    {
        public StorageSettingsWindow()
        {
            InitializeComponent();
        }

        public StorageType StorageType
        {
            get
            {
                if (xmlRadioButton.Checked)
                    return StorageType.XMLStorage;
                else if (txtRadioButton.Checked)
                    return StorageType.TXTStorage;
                else if (sqlRadioButton.Checked)
                    return StorageType.SQLStorage;
                else
                    throw new SystemException("Storage type is not set");
            }
            private set { }
        }

        public string XmlOutputDirectory 
        {
            get { return xmlOutputDirectoryTxt.Text; }
        }

        public string TxtOutputDirectory
        {
            get { return txtOutputDirectoryTxt.Text; }
        }

        public string SqlConnectionString
        {
            get { return this.connectionStrTxt.Text; }
        }

        #region Event Handlers

        private void StorageSettingsWindow_Load(object sender, EventArgs e)
        {
            xmlRadioButton.Checked = true;
            xmlOutputDirectoryTxt.Text = Settings.StorageDirectory;
            txtOutputDirectoryTxt.Text = Settings.StorageDirectory;
            //connectionStrTxt.Text = Settings.ConnectionString;
        }

        private void store_checkedChanged(object sender, EventArgs e)
        {
            RadioButton btn = (RadioButton)sender;
            switch (btn.Name)
            {
                case "xmlRadioButton":
                    XmlChecked(true);
                    TxtChecked(false);
                    SqlChecked(false);
                    StorageType = StorageType.XMLStorage;
                    break;
                case "txtRadioButton":
                    XmlChecked(false);
                    TxtChecked(true);
                    SqlChecked(false);
                    StorageType = StorageType.TXTStorage;
                    break;
                case "sqlRadioButton":
                    XmlChecked(false);
                    TxtChecked(false);
                    SqlChecked(true);
                    StorageType = StorageType.SQLStorage;
                    break;
                default:
                    break;
            }            
        }

        private void xmlBrowseButton_Click(object sender, EventArgs e)
        {
            if (browserDlg.ShowDialog() == DialogResult.OK)
            {
                xmlOutputDirectoryTxt.Text = browserDlg.SelectedPath;
            }
        }

        private void txtBrowseButton_Click(object sender, EventArgs e)
        {
            if (browserDlg.ShowDialog() == DialogResult.OK)
            {
                txtOutputDirectoryTxt.Text = browserDlg.SelectedPath;
            }
        }

        private void connectionsButton_Click(object sender, EventArgs e)
        {

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region Utilities

        private void XmlChecked(bool c)
        {
            xmlOutputDirectory.Enabled = c;
            xmlOutputDirectoryTxt.Enabled = c;
            xmlBrowseButton.Enabled = c;
        }

        private void TxtChecked(bool c)
        {
            txtOutputDirectory.Enabled = c;
            txtOutputDirectoryTxt.Enabled = c;
            txtBrowseButton.Enabled = c;
        }

        private void SqlChecked(bool c)
        {
            connectionStr.Enabled = c;
            connectionStrTxt.Enabled = c;
            connectionsButton.Enabled = c;
        }

        #endregion
    }
}
