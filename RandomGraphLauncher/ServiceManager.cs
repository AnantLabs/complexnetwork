using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel.Discovery;
using AnalyzerFramework.Manager.WcfClient;

namespace RandomGraphLauncher
{
    public partial class ServiceManager : Form
    {
        public bool IsDistributed { get; set; }
        private Dictionary<string, EndpointDiscoveryMetadata> services = new Dictionary<string,EndpointDiscoveryMetadata>();
        public ServiceManager(bool isDistributed)
        {
            InitializeComponent();
            checkBox1.Checked = isDistributed;
        }

        private void DiscoverServices()
        {
            checkedListBox1.Items.Clear();
            IList<EndpointDiscoveryMetadata> endpoints = ServiceDiscoveryManager.SearchServices();
            services.Clear();
            if(endpoints.Count == 0)
                if (checkedListBox1.CheckedItems.Count == 0)
                {
                    MessageBox.Show("There is no any computer in local area network");
                    return;
                }
            foreach (EndpointDiscoveryMetadata item in endpoints)
            {
                services.Add(item.Address.Uri.Host, item);
                checkedListBox1.Items.Add(item.Address.Uri.Host);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DiscoverServices();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (checkedListBox1.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Please select at list one computer.");
                    return;
                }
                IList<EndpointDiscoveryMetadata> selectedEndpoints = new List<EndpointDiscoveryMetadata>();
                foreach (var item in checkedListBox1.CheckedItems)
                {
                    selectedEndpoints.Add(services[(string)item]);
                }
                ServiceDiscoveryManager.SelectedServices = selectedEndpoints;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
