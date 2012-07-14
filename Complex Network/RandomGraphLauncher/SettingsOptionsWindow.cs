// <Mikayel Samvelyan>
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
using CommonLibrary.Model.Settings;
using System.ServiceModel.Discovery;
using AnalyzerFramework.Manager.WcfClient;
using log4net.Config;
using log4net.Appender;
using log4net;

namespace RandomGraphLauncher
{
    public partial class SettingsOptionsWindow : Form
    {
        // private members used by Distributed Mode.
        private DataConnectionDialog dcd = new DataConnectionDialog();
        private Dictionary<string, EndpointDiscoveryMetadata> services = new Dictionary<string, EndpointDiscoveryMetadata>();

        public SettingsOptionsWindow()
        {
            InitializeComponent();
        }

        // Member Functions

        private void TracingModeOn()
        {
            tracingModeCheckBox.Checked = true;
            tracingPathTxtBox.Enabled = true;
            tracingBrowseButton.Enabled = true;
            tracingLabel.Enabled = true;
        }

        private void TracingModeOff()
        {
            tracingModeCheckBox.Checked = false;
            tracingPathTxtBox.Enabled = false;
            tracingBrowseButton.Enabled = false;
            tracingLabel.Enabled = false;
        }

        private void XMLChecked()
        {
            this.Location.Enabled = true;
            this.LocationTxt.Enabled = true;
            this.Browse.Enabled = true;

            this.LabelConnStr.Enabled = false;
            this.textBoxConnStr.Enabled = false;
            this.AddConnection.Enabled = false;
        }

        private void SQLChecked()
        {
            this.LabelConnStr.Enabled = true;
            this.textBoxConnStr.Enabled = true;
            this.AddConnection.Enabled = true;

            this.Location.Enabled = false;
            this.LocationTxt.Enabled = false;
            this.Browse.Enabled = false;
        }

        private void DiscoverServices()
        {
            DiscoveredServices.Items.Clear();
            IList<EndpointDiscoveryMetadata> endpoints = ServiceDiscoveryManager.SearchServices();
            services.Clear();
            if (endpoints.Count == 0)
                if (DiscoveredServices.CheckedItems.Count == 0)
                {
                    MessageBox.Show("There is no any computer in local area network");
                    return;
                }
            foreach (EndpointDiscoveryMetadata item in endpoints)
            {
                services.Add(item.Address.Uri.Host, item);
                DiscoveredServices.Items.Add(item.Address.Uri.Host);
            }
        }

        // Event Handlers

        private void SettingsOptionsWindow_Load(object sender, EventArgs e)
        {
            if (Options.Storage == Options.StorageProvider.XMLProvider)
            {
                this.XMLRadioButton.Checked = true;
                XMLChecked();
            }
            else
            {
                this.SQLRadioButton.Checked = true;
                SQLChecked();
            }
            
            this.LocationTxt.Text = Options.StorageDirectory;
            this.textBoxConnStr.Text = Options.ConnectionString;
            
            this.trainingModeCheckBox.Checked = (Options.TrainingMode == true) ? true : false;
            
            tracingPathTxtBox.Text = Options.TracingDirectory;
            if (Options.TracingMode == true)
            {
                TracingModeOn();
            }
            else
            {
                TracingModeOff();
            }
            
            if (Options.Generation == Options.GenerationMode.randomGeneration)
            {
                randomGenerationRadioButton.Checked = true;
                staticGenerationRadioButton.Checked = false;
            }
            else
            {
                randomGenerationRadioButton.Checked = false;
                staticGenerationRadioButton.Checked = true;
            }

            distributedCheckBox.Checked = Options.DistributedMode ? true : false;  
        
            debugCheckBox.Checked = (Options.Logger == Options.LoggerMode.debug) ? true : false; 
            loggerPathTextBox.Text = Options.LoggerDirectory;
        } 


        private void XMLRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            XMLChecked();
        }

        private void SQLRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SQLChecked();
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            if (BrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.LocationTxt.Text = BrowserDialog.SelectedPath;
            }
        }

        private void AddConnection_Click(object sender, EventArgs e)
        {
            dcd = new DataConnectionDialog();
            DataConnectionConfiguration dcs = new DataConnectionConfiguration(null);
            dcs.LoadConfiguration(dcd);

            if (dcd.SelectedDataProvider != null && dcd.SelectedDataSource != null)
            {
                dcd.ConnectionString = this.textBoxConnStr.Text;
            }
            if (DataConnectionDialog.Show(dcd) == DialogResult.OK)
            {
                textBoxConnStr.Text = dcd.ConnectionString;
            }
            dcs.SaveConfiguration(dcd);
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            if (distributedCheckBox.Checked == true)
            {
                if (DiscoveredServices.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Please select at least one computer.");
                    return;
                }
                IList<EndpointDiscoveryMetadata> selectedEndpoints = new List<EndpointDiscoveryMetadata>();
                foreach (var item in DiscoveredServices.CheckedItems)
                {
                    selectedEndpoints.Add(services[(string)item]);
                }
                ServiceDiscoveryManager.SelectedServices = selectedEndpoints;
            }
            
            Options.Storage = (XMLRadioButton.Checked == true) ? Options.StorageProvider.XMLProvider :
                Options.StorageProvider.SQLProvider;
            Options.StorageDirectory = LocationTxt.Text;
            Options.ConnectionString = textBoxConnStr.Text;
            Options.TrainingMode = trainingModeCheckBox.Checked;
            Options.TracingMode = tracingModeCheckBox.Checked;
            Options.TracingDirectory = tracingPathTxtBox.Text;
            Options.Generation = (randomGenerationRadioButton.Checked) ? Options.GenerationMode.randomGeneration :
                Options.GenerationMode.staticGeneration;
            Options.DistributedMode = distributedCheckBox.Checked;
            Options.Logger = debugCheckBox.Checked ? Options.LoggerMode.debug : Options.LoggerMode.info;
            Options.LoggerDirectory = loggerPathTextBox.Text;
            Options.Refresh();
            this.Close();

        }
        
        private void tracingModeCheckBox_CheckedChanged(object sender, EventArgs e)
        { 
            if (tracingModeCheckBox.Checked == true)
            {
                TracingModeOn();
            }
            else
            {
                TracingModeOff();
            }
        }
        
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            DiscoverServices();
        }
        
        private void savingButton_Click(object sender, EventArgs e)
        {
            if (LoggerBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                loggerPathTextBox.Text = LoggerBrowserDialog.SelectedPath + "\\randomGraphLog.txt";
            }
        }

        private void tracingBrowseButton_Click(object sender, EventArgs e)
        {
            if (tracingBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.tracingPathTxtBox.Text = tracingBrowserDialog.SelectedPath;
            }
        }
    }
}
// </Mikayel Samvelyan>
