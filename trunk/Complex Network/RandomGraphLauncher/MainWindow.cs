using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyControlLibrary;
using RandomGraph.Core.Manager.Impl;
using RandomGraph.Common.Storage;
using RandomGraphLauncher.models;
using CommonLibrary.Model.Attributes;
using RandomGraph.Common.Model;
using ResultStorage.Storage;
using System.Configuration;
using SettingsConfiguration;
using AnalyzerFramework.Manager.ModelRepo;
using AnalyzerFramework.Manager.Impl;
using System.Diagnostics;
using log4net;

using RandomGraph.Common.Model.Settings;
using log4net.Config;
using log4net.Appender;

namespace RandomGraphLauncher
{
    public partial class MainWindow : Form
    {
        // Организация работы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(MainWindow));

        // model name, model factory, model
        private IDictionary<string, Type> models = new Dictionary<string, Type>();
        private AbstractGraphManager manager;
        private List<string> runningJobs = new List<string>();

        private IResultStorage storageManager;

        public MainWindow()
        {
            InitializeComponent();
            InitStorageManager();

            List<Type> availableModelTypes = ModelRepository.GetInstance().GetAvailableModelTypes();
            foreach (Type modelType in availableModelTypes)
            {
                string modelName = modelType.Name;
                models.Add(modelName, modelType);
            }
        }

        private void InitLogManager()
        {
            XmlConfigurator.Configure();
            log4net.Repository.Hierarchy.Hierarchy h =
            (log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository();
            foreach (IAppender a in h.Root.Appenders)
            {
                if (a is FileAppender)
                {
                    FileAppender fa = (FileAppender)a;
                    // Uncomment the lines below if you want to retain the base file name
                    // and change the folder name...
                    //FileInfo fileInfo = new FileInfo(fa.File);
                    //logFileLocation = string.Format(@"C:\MySpecialFolder\{0}", fileInfo.Name);
                    fa.File = Options.LoggerDirectory;
                    fa.ActivateOptions();
                    break;
                }
            }

            //Set Logger level
            string strChecker = "WARN_INFO_DEBUG_ERROR_FATAL";
            string strLogLevel = null;

            if (Options.Logger == Options.LoggerMode.debug)
            {
                strLogLevel = "DEBUG";
            }

            else if (Options.Logger == Options.LoggerMode.info)
            {
                strLogLevel = "INFO";
            }

            if (String.IsNullOrEmpty(strLogLevel) == true || strChecker.Contains(strLogLevel) == false)
                throw new Exception(" The strLogLevel should be set to WARN , INFO , DEBUG ,");



            log4net.Repository.ILoggerRepository[] repositories = log4net.LogManager.GetAllRepositories();

            //Configure all loggers to be at the debug level.
            foreach (log4net.Repository.ILoggerRepository repository in repositories)
            {
                repository.Threshold = repository.LevelMap[strLogLevel];
                log4net.Repository.Hierarchy.Hierarchy hier = (log4net.Repository.Hierarchy.Hierarchy)repository;
                log4net.Core.ILogger[] loggers = hier.GetCurrentLoggers();
                foreach (log4net.Core.ILogger logger in loggers)
                {
                    ((log4net.Repository.Hierarchy.Logger)logger).Level = hier.LevelMap[strLogLevel];
                }
            }

            //Configure the root logger.
            log4net.Repository.Hierarchy.Logger rootLogger = h.Root;
            rootLogger.Level = h.LevelMap[strLogLevel];
        }

        private void InitStorageManager()
        {
            string provider = ConfigurationManager.AppSettings["Storage"];
            if (provider == "XmlProvider")
            {
                storageManager = new XMLResultStorage(ConfigurationManager.AppSettings[provider]);
            }
            else if (provider == "SQLProvider")
            {
                storageManager = new SQLResultStorage(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[provider]]);
            }
        }

        private void userControl11_OnClose(object sender, CloseEventArgs e)
        {
            TabPage tab = this.userControl11.TabPages[e.TabIndex];
            ((CalculationControl)tab.Controls.Find("calculationControl", true)[0]).closeCalculation();
            if (this.userControl11.TabPages.Count > 1)
            {
                this.userControl11.Controls.Remove(tab);
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void NewJob()
        {
            ModelChooserWindow modelChooser = new ModelChooserWindow(models.Keys, storageManager, runningJobs);

            modelChooser.ShowInTaskbar = false;
            if (modelChooser.ShowDialog() == DialogResult.OK)
            {
                string modelName = modelChooser.comboBox_ModelType.SelectedItem.ToString();
                string jobName = modelChooser.textBox_JobName.Text;
                if (modelChooser.textBox_JobName.Text != null)
                {
                    jobName = modelChooser.textBox_JobName.Text;
                }
                modelChoosed(models[modelName], jobName);
            }
        }

        private void modelChoosed(Type modelType, string jobName)
        {
            InitLogManager();
            if (Options.DistributedMode)
            {
                manager = new DistributedGraphManager(storageManager);
            }
            else
            {
                manager = new MultiTreadGraphManager(storageManager);
            }
            runningJobs.Add(jobName);
            TabPageEx tabPage = new MyControlLibrary.TabPageEx(new System.ComponentModel.Container());

            tabPage.SuspendLayout();
            this.userControl11.Controls.Add(tabPage);
            this.userControl11.SelectedTab = tabPage;
            // 
            // calculationControl
            // 
            CalculationControl calculationControl = new CalculationControl(modelType, 
                jobName, manager);
            calculationControl.Dock = System.Windows.Forms.DockStyle.Fill;
            calculationControl.Location = new System.Drawing.Point(0, 0);
            calculationControl.Name = "calculationControl";
            calculationControl.Size = new System.Drawing.Size(1005, 457);
            calculationControl.TabIndex = 0;

            // 
            // tabPage
            // 
            tabPage.Controls.Add(calculationControl);
            tabPage.Location = new System.Drawing.Point(4, 28);
            tabPage.Menu = null;
            tabPage.Name = "tabPage";
            tabPage.Size = new System.Drawing.Size(1005, 457);
            tabPage.TabIndex = 1;
            tabPage.Text = jobName;

            tabPage.ResumeLayout(false);

            //calculationControl.ExecutionStatusChange += new CalculationControl.AnalyzeEventHandler(graphWindow_ExecutionStatusChange);
            Width++;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void statisticAnalyzerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("StatisticAnalyzerUI.exe");
        }

        private void dataExportIMportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataExportImport window = new DataExportImport(storageManager);
            window.ShowDialog();
        }

        private void FormClosing_Event(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void testerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TesterForm tester = new TesterForm();
            tester.ShowDialog();
        }

        private void modelCheckingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelCheckWindow wnd = new ModelCheckWindow();
            wnd.ShowDialog();
        }

        private void newJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewJob(); 
        }
        
        // <Mikayel Samvelyan>
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsOptionsWindow window = new SettingsOptionsWindow();
            window.ShowDialog();    
        }     
        // </Mikayel Samvelyan>
    }
}
