using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel.Discovery;

using Session;
using Core;
using Core.Enumerations;

namespace RandomNetworksExplorer
{
    public partial class SettingsWindow : Form
    {
        private Dictionary<string, EndpointDiscoveryMetadata> services;

        public SettingsWindow()
        {
            InitializeComponent();

            services = new Dictionary<string, EndpointDiscoveryMetadata>();
        }

        #region Event Handlers

        private void Settings_Load(object sender, EventArgs e)
        {
            InitializeWorkingModes();

            if (SessionManager.ExistsAnyRunningResearch())
                workingModePage.Enabled = false;

            loggingDirectoryTxt.Text = Settings.LoggingDirectory;
            storageDirectoryTxt.Text = Settings.StorageDirectory;
            //databaseTxt.Text = Settings.ConnectionString;
            tracingDirectoryTxt.Text = Settings.TracingDirectory;
            managerTypeCmb.SelectedIndex = (int)Settings.WorkingMode - 1;
        }

        private void loggingBrowseButton_Click(object sender, EventArgs e)
        {
            if (browserDlg.ShowDialog() == DialogResult.OK)
            {
                loggingDirectoryTxt.Text = browserDlg.SelectedPath;
            }
        }

        private void storageBrowseButton_Click(object sender, EventArgs e)
        {
            if (browserDlg.ShowDialog() == DialogResult.OK)
            {
                storageDirectoryTxt.Text = browserDlg.SelectedPath;
            }
        }

        private void tracingBrowseBtn_Click(object sender, EventArgs e)
        {
            if (browserDlg.ShowDialog() == DialogResult.OK)
            {
                tracingDirectoryTxt.Text = browserDlg.SelectedPath;
            }
        }

        private void managerTypeCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (managerTypeCmb.SelectedItem.ToString())
            {
                case "Local":
                    DistributedModeOff();
                    break;
                case "WCFDistributed":
                    DistributedModeOn();
                    break;
                default:
                    break;
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            DiscoverServices();
        }

        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            /*Settings.Logger = debugCheckBox.Checked ?
                Settings.LoggerMode.debug :
                Settings.LoggerMode.info;*/
            Settings.LoggingDirectory = loggingDirectoryTxt.Text;
            Settings.StorageDirectory = storageDirectoryTxt.Text;
            //Settings.ConnectionString = textBoxConnStr.Text;
            Settings.TracingDirectory = tracingDirectoryTxt.Text;

            Settings.WorkingMode = (ManagerType)Enum.Parse(typeof(ManagerType),
                managerTypeCmb.SelectedItem.ToString());

            if (Settings.WorkingMode == ManagerType.WCFDistributed)
            {
                if (discoveredServices.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Warning", "Please, select at least one computer.");
                    return;
                }
                IList<EndpointDiscoveryMetadata> selectedEndpoints = new List<EndpointDiscoveryMetadata>();
                foreach (var item in discoveredServices.CheckedItems)
                {
                    selectedEndpoints.Add(services[(string)item]);
                }
                //ServiceDiscoveryManager.SelectedServices = selectedEndpoints;
            }

            Settings.Refresh();
            Close();
        }

        #endregion 

        #region Utilities

        private void InitializeWorkingModes()
        {
            managerTypeCmb.Items.Clear();

            Array availableManagerTypes = Enum.GetValues(typeof(ManagerType));
            foreach (ManagerType t in availableManagerTypes)
            {
                managerTypeCmb.Items.Add(t.ToString());
            }

            if(managerTypeCmb.Items.Count != 0)
                managerTypeCmb.SelectedIndex = 0;
        }

        private void DistributedModeOn()
        {
            RefreshButton.Enabled = true;
            distributedLabel.Enabled = true;
            discoveredServices.Enabled = true;
        }

        private void DistributedModeOff()
        {
            RefreshButton.Enabled = false;
            distributedLabel.Enabled = false;
            discoveredServices.Enabled = false;
        }

        private void DiscoverServices()
        {
            /*discoveredServices.Items.Clear();
            IList<EndpointDiscoveryMetadata> endpoints = ServiceDiscoveryManager.SearchServices();
            services.Clear();
            if (endpoints.Count == 0)
            {
                if (discoveredServices.CheckedItems.Count == 0)
                {
                    MessageBox.Show("There is no any computer in local area network.");
                    return;
                }
            }

            foreach (EndpointDiscoveryMetadata item in endpoints)
            {
                services.Add(item.Address.Uri.Host, item);
                discoveredServices.Items.Add(item.Address.Uri.Host);
            }*/
        }

        #endregion
    }
}
