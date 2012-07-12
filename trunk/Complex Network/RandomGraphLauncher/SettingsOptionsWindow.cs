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

        // public members that indicate current settings.
        public StorageProvider Storage { get; set; }
        public string StorageDirectory { get; set; }
        public string ConnectionString { get; set; }
        public bool TrainingMode { get; set; }
        public bool TracingMode { get; set; }
        public string TracingDirectory { get; set; }
        public GenerationMode generationMode { get; set; }
        public bool DistributedMode { get; set; }
        public bool LoggerMode { get; set; } // 1 if Info. 0 if Debug.
        public string LoggerDirectory { get; set; }

        // private members used by Distributed Mode.
        private DataConnectionDialog dcd = new DataConnectionDialog();
        private Dictionary<string, EndpointDiscoveryMetadata> services = new Dictionary<string, EndpointDiscoveryMetadata>();

        public SettingsOptionsWindow(StorageProvider storage,
                             string storageDirectory,
                             string connectionString,
                             bool trainingMode,
                             bool tracingMode,
                             string tracingDirectory,
                             GenerationMode generation,
                             bool distributedMode,
                             bool loggerSettingsMode,
                             string loggerDirectory)
        {
            this.Storage = storage;
            this.StorageDirectory = storageDirectory;
            this.ConnectionString = connectionString;        
            this.TrainingMode = trainingMode;
            this.TracingMode = tracingMode;
            this.TracingDirectory = tracingDirectory;
            this.generationMode = generation;
            this.DistributedMode = distributedMode;
            this.LoggerMode = loggerSettingsMode;
            this.LoggerDirectory = loggerDirectory;
            
            InitializeComponent();
        }

        // Member Functions

        private void TracingModeOn()
        {
            this.TracingMode = true;
            tracingModeCheckBox.Checked = true;
            tracingPathTxtBox.Enabled = true;
            tracingBrowseButton.Enabled = true;
            tracingLabel.Enabled = true;
        }

        private void TracingModeOff()
        {
            this.TracingMode = false;
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
            Storage = StorageProvider.XMLProvider;
        }

        private void SQLChecked()
        {
            this.LabelConnStr.Enabled = true;
            this.textBoxConnStr.Enabled = true;
            this.AddConnection.Enabled = true;

            this.Location.Enabled = false;
            this.LocationTxt.Enabled = false;
            this.Browse.Enabled = false;
            Storage = StorageProvider.SQLProvider;
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
            if (Storage == StorageProvider.XMLProvider)
            {
                this.XMLRadioButton.Checked = true;
                XMLChecked();
            }
            else
            {
                this.SQLRadioButton.Checked = true;
                SQLChecked();
            }
            this.LocationTxt.Text = StorageDirectory;
            this.textBoxConnStr.Text = ConnectionString;


            this.trainingModeCheckBox.Checked = (TrainingMode == true) ? true : false;
           
            tracingPathTxtBox.Text = this.TracingDirectory;
            if (this.TracingMode == true)
            {
                TracingModeOn();
            }
            else
            {
                TracingModeOff();
            }

            if (generationMode == GenerationMode.randomGeneration)
            {
                randomGenerationRadioButton.Checked = true;
                staticGenerationRadioButton.Checked = false;
            }
            else
            {
                randomGenerationRadioButton.Checked = false;
                staticGenerationRadioButton.Checked = true;
            }

            distributedCheckBox.Checked = (DistributedMode == true) ? true : false;

            debugCheckBox.Checked = (LoggerMode == true) ? false : true;
            loggerPathTextBox.Text = LoggerDirectory;
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
            this.TracingDirectory = tracingPathTxtBox.Text;
            this.LoggerDirectory = loggerPathTextBox.Text;

            if (this.DistributedMode == true)
            {
                if (DiscoveredServices.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Please select at list one computer.");
                    return;
                }
                IList<EndpointDiscoveryMetadata> selectedEndpoints = new List<EndpointDiscoveryMetadata>();
                foreach (var item in DiscoveredServices.CheckedItems)
                {
                    selectedEndpoints.Add(services[(string)item]);
                }
                ServiceDiscoveryManager.SelectedServices = selectedEndpoints;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void trainingModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            TrainingMode = (trainingModeCheckBox.Checked == true) ? true : false;
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

        private void randomGenerationRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (randomGenerationRadioButton.Checked == true)
            {
                generationMode = GenerationMode.randomGeneration;
            }
        }

        private void staticGenerationRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (staticGenerationRadioButton.Checked == true)
            {
                generationMode = GenerationMode.staticGeneration;
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            DiscoverServices();
        }

        private void distributedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.DistributedMode = (distributedCheckBox.Checked == true) ? true : false;
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

        private void debugCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.LoggerMode = (debugCheckBox.Checked == false) ? true : false;  
        }  
    }
    public enum StorageProvider
    {
        XMLProvider,
        SQLProvider
    }
}
// </Mikayel Samvelyan>
