using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLibrary.Model.Attributes;
using RandomGraph.Common.Storage;
using log4net;

namespace RandomGraphLauncher
{
    public partial class ModelChooserWindow : Form
    {
        /// <summary>
        /// The logger static object for monitoring.
        /// </summary>
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(ModelChooserWindow));
        private IResultStorage iResultStorage;
        private List<string> runningJobs;

        public ModelChooserWindow(ICollection<string> modelNames, IResultStorage iResultStorage, List<string> runningJobs)
        {
            this.runningJobs = runningJobs;
            this.iResultStorage = iResultStorage;
            InitializeComponent();
            InitCompoBox(modelNames);
            this.ActiveControl = textBox_JobName;
            textBox_JobName.Focus();
        }

        private void InitCompoBox(ICollection<string> modelNames)
        {
            List<string> list = new List<string>();
            foreach (string s in modelNames)
            {
                list.Add(s);
            }
            list.Sort((x, y) => string.Compare(x, y));
            comboBox_ModelType.Items.AddRange(list.ToArray<string>());
            comboBox_ModelType.SelectedIndex = 0;
        }
        
        private void OK_ButtonClick(object sender, EventArgs e)
        {
            if (isJobWithTheSameNameExists())
            {
                MessageBox.Show("There are Job with the same name.\nPlease choose another name for job.");
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool isJobWithTheSameNameExists()
        {
            string currentJob = textBox_JobName.Text;
            foreach (var result in iResultStorage.LoadAllAssemblies())
            {
                if(result.Name == currentJob) 
                {
                    return true;
                }
            }
            foreach (var runningJob in runningJobs)
            {
                if (runningJob == currentJob)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
