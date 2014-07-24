using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

//using log4net.Config;
//using log4net.Appender;
//using log4net;

using Core.Exceptions;
using Core.Enumerations;

namespace Core.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExplorerSettings
    {
        static private String defaultDirectory = 
            Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\RNE";

        static private Configuration config;

        //static private LoggerMode logger;
        static private string loggingDirectory;        
        static private string storageDirectory;
        //static private StorageProvider storage;
        //static private string connectionString;
        static private string tracingDirectory;
        static private ManagerType workingMode;
        static private string staticGenerationDirectory;

        static ExplorerSettings()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            try
            {
                /*if (config.AppSettings.Settings["LoggerMode"].Value == "info")
                    logger = LoggerMode.info;
                else if (config.AppSettings.Settings["LoggerMode"].Value == "debug")
                    logger = LoggerMode.debug;
                else throw new Exception("LoggerMode is set improperly.");*/

                loggingDirectory = config.AppSettings.Settings["LoggingDirectory"].Value;
                storageDirectory = config.AppSettings.Settings["StorageDirectory"].Value;
                //connectionString = config.ConnectionStrings.ConnectionStrings[config.AppSettings.Settings["SQLProvider"].Value].ConnectionString;
                tracingDirectory = config.AppSettings.Settings["TracingDirectory"].Value;
                workingMode = (ManagerType)Enum.Parse(typeof(ManagerType), 
                    config.AppSettings.Settings["WorkingMode"].Value);
                staticGenerationDirectory = config.AppSettings.Settings["StaticGenerationDirectory"].Value;
            }
            catch
            {
                throw new CoreException("The structure of Configuration file is not correct.");
            }
        }

        /*static public LoggerMode Logger
        {
            get
            {
                return logger;
            }
            set
            {
                logger = value;
                config.AppSettings.Settings["LoggerMode"].Value = (logger == LoggerMode.info) ? "info" : "debug";
            }
        }*/

        static public string LoggingDirectory
        {
            get
            {
                return (loggingDirectory == "") ? defaultDirectory + "\\Logging" : loggingDirectory;
            }
            set
            {
                if (value.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    loggingDirectory = value;
                }
                else
                {
                    loggingDirectory = value + Path.DirectorySeparatorChar;
                }

                if (Directory.Exists(storageDirectory) == false)
                {
                    Directory.CreateDirectory(loggingDirectory);
                }
                
                config.AppSettings.Settings["LoggingDirectory"].Value = loggingDirectory;
            }
        }

        static public string StorageDirectory
        {
            get
            {
                return (storageDirectory == "") ? defaultDirectory + "\\Results" : storageDirectory;
            }
            set
            {
                if (value.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    storageDirectory = value;
                }
                else
                {
                    storageDirectory = value + Path.DirectorySeparatorChar;
                }

                if (Directory.Exists(storageDirectory) == false)
                {
                    Directory.CreateDirectory(storageDirectory);
                }

                config.AppSettings.Settings["StorageDirectory"].Value = storageDirectory;
            }
        }

        /*static public string ConnectionString 
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
                config.ConnectionStrings.ConnectionStrings[config.AppSettings.Settings["SQLProvider"].Value].ConnectionString
                    = connectionString;
            }
        }*/

        static public string TracingDirectory
        {
            get
            {
                return (tracingDirectory == "") ? defaultDirectory + "\\Tracing" : tracingDirectory;
            }
            set
            {
                if (value.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    tracingDirectory = value;
                }
                else
                {
                    tracingDirectory = value + Path.DirectorySeparatorChar;
                }

                if (Directory.Exists(tracingDirectory) == false)
                {
                    Directory.CreateDirectory(tracingDirectory);
                }

                config.AppSettings.Settings["TracingDirectory"].Value = tracingDirectory;
            }
        }

        static public ManagerType WorkingMode 
        {
            get
            {
                return workingMode;
            }
            set
            {
                workingMode = value;
                config.AppSettings.Settings["WorkingMode"].Value = workingMode.ToString();
            }
        }

        static public string StaticGenerationDirectory
        {
            get
            {
                return (staticGenerationDirectory == "") ? defaultDirectory + "\\Results" : staticGenerationDirectory;
            }
            set
            {
                if (value.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    staticGenerationDirectory = value;
                }
                else
                {
                    staticGenerationDirectory = value + Path.DirectorySeparatorChar;
                }

                if (Directory.Exists(staticGenerationDirectory) == false)
                {
                    Directory.CreateDirectory(staticGenerationDirectory);
                }

                config.AppSettings.Settings["StaticGenerationDirectory"].Value = staticGenerationDirectory;
            }
        }

        /// <summary>
        /// Refreshes app.config file content.
        /// </summary>
        static public void Refresh()
        {
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        static public void InitializeLogManager()
        {
            /*XmlConfigurator.Configure();
            log4net.Repository.Hierarchy.Hierarchy h = (log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository();
            foreach (IAppender a in h.Root.Appenders)
            {
                if (a is FileAppender)
                {
                    FileAppender fa = (FileAppender)a;
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

            if (Settings.Logger == Settings.LoggerMode.debug)
            {
                strLogLevel = "DEBUG";
            }

            else if (Settings.Logger == Settings.LoggerMode.info)
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
            rootLogger.Level = h.LevelMap[strLogLevel];*/
        }
        
        /*public enum StorageProvider
        {
            XMLProvider,
            SQLProvider
        }

        public enum LoggerMode
        {
            info,
            debug
        }*/
    }
}
