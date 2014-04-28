using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel.Discovery;

using Core;

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
            if (SessionManager.ExistsAnyRunningResearch())
                workingModePage.Enabled = false;

            /*directoryPathTxt.Text = Settings.LoggingPath;
            textStorageTxt.Text = Settings.OutputPath;
            databaseTxt.Text = Settings.ConnectionString;
            tracingDirectoryTxt.Text = Settings.TracingPath;

            distributedCheckBox.Checked = Settings.DistributedMode ? true : false;*/
        }

        private void BrowseLogDirButton_Click(object sender, EventArgs e)
        {

        }

        private void BrowseFileStorageButton_Click(object sender, EventArgs e)
        {

        }

        private void BrowseDatabaseButton_Click(object sender, EventArgs e)
        {

        }

        private void browseTracingBtn_Click(object sender, EventArgs e)
        {

        }

        private void distributedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (distributedCheckBox.Checked == true)
                DistributedModeOn();
            else
                DistributedModeOff();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            DiscoverServices();
        }

        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {

        }

        private void CancelSettingsButton_Click(object sender, EventArgs e)
        {

        }

        #endregion 

        #region Utilities

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
