using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net.Config;
using log4net.Appender;
using log4net;

namespace RandomGraphLauncher
{
    public partial class LoggerSetingsForm : Form
    {
        public LoggerSetingsForm()
        {
            InitializeComponent();
            radioButton2.Select();
            textBox1.Text = "c:\\MyLogFile.log";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath + "\\MyLogFile.log";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.label1.Text = folderBrowserDialog1.SelectedPath;
            XmlConfigurator.Configure();
            log4net.Repository.Hierarchy.Hierarchy h =
            (log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository();
            foreach (IAppender a in h.Root.Appenders)
            {
                if (a is FileAppender)
                {
                    FileAppender fa = (FileAppender)a;
                    // Programmatically set this to the desired location here
                    string logFileLocation = folderBrowserDialog1.SelectedPath + "\\MyLogFile.log";

                    // Uncomment the lines below if you want to retain the base file name
                    // and change the folder name...
                    //FileInfo fileInfo = new FileInfo(fa.File);
                    //logFileLocation = string.Format(@"C:\MySpecialFolder\{0}", fileInfo.Name);

                    fa.File = logFileLocation;
                    fa.ActivateOptions();
                    break;
                }
            }
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SetLogingLevel("ERROR");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SetLogingLevel("INFO");
        }

        /// <summary>
        /// Activates debug level 
        /// </summary>
        /// <sourceurl>http://geekswithblogs.net/rakker/archive/2007/08/22/114900.aspx</sourceurl>
        private void SetLogingLevel(string strLogLevel)
        {
            string strChecker = "WARN_INFO_DEBUG_ERROR_FATAL";

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
            log4net.Repository.Hierarchy.Hierarchy h = (log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository();
            log4net.Repository.Hierarchy.Logger rootLogger = h.Root;
            rootLogger.Level = h.LevelMap[strLogLevel];
        }
    }
}
