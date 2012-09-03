using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace RandomGraph.Common.Model.Settings
{
    public static class Options
    {
        // public members that indicate current settings.
        static private StorageProvider storage;
        static private string storageDirectory;
        static private string connectionString;

        static private bool trainingMode;

        static private bool tracingMode;
        static private string tracingDirectory;

        static private GenerationMode generation;

        static private bool distributedMode;

        static private LoggerMode logger;
        static private string loggerDirectory;

        // private member
        static private Configuration config;

        // Properties
        static public StorageProvider Storage 
        {
            get
            {
                return storage;
            } 
            set
            {
                storage = value;
                config.AppSettings.Settings["Storage"].Value = (storage == StorageProvider.XMLProvider) ?
                    "XmlProvider" : "SQLProvider";

            }
        }

        static public string StorageDirectory
        {
            get
            {
                return storageDirectory;
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
                config.AppSettings.Settings["XmlProvider"].Value = storageDirectory;
            }
        }

        static public string ConnectionString 
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
        }

        static public bool TrainingMode 
        {
            get
            {
                return trainingMode;
            }
            set
            {
                trainingMode = value;
                config.AppSettings.Settings["Training"].Value = trainingMode ? "yes" : "no";
            }
        }

        static public bool TracingMode
        {
            get
            {
                return tracingMode;
            }
            set
            {
                tracingMode = value;
                config.AppSettings.Settings["Tracing"].Value = tracingMode ? "yes" : "no";
            }
        }

        static public string TracingDirectory
        {
            get
            {
                return tracingDirectory;
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

        static public GenerationMode Generation 
        { 
            get
            {
                return generation;
            }
            set 
            {
                generation = value;
                config.AppSettings.Settings["Generation"].Value =
                    (generation == GenerationMode.randomGeneration) ? "random" : "static";
            }
        }

        static public bool DistributedMode 
        {
            get
            {
                return distributedMode;
            }
            set
            {
                distributedMode = value;
                config.AppSettings.Settings["Distributed"].Value = distributedMode ? "yes" : "no";
            }
        }

        static public LoggerMode Logger 
        {
            get
            {
                return logger;
            }
            set
            {
                logger = value;
                config.AppSettings.Settings["LoggerMode"].Value = (logger == LoggerMode.info) ? "info": "debug";
            }
        }
        static public string LoggerDirectory
        {
            get
            {
                return loggerDirectory;
            }
            set
            {
                loggerDirectory = value;
                config.AppSettings.Settings["LoggerDirectory"].Value = loggerDirectory;
            }
        }

        static Options()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings["Storage"].Value == "XmlProvider")
                storage = StorageProvider.XMLProvider;
            else if (config.AppSettings.Settings["Storage"].Value == "SQLProvider")
                storage = StorageProvider.SQLProvider;
            else throw new Exception("StorageProvider is set improperly.");

            storageDirectory = config.AppSettings.Settings["XmlProvider"].Value;

            connectionString =
                    config.ConnectionStrings.ConnectionStrings[config.AppSettings.Settings["SQLProvider"].Value].ConnectionString;

            if (config.AppSettings.Settings["Training"].Value == "yes")
                trainingMode = true;
            else if (config.AppSettings.Settings["Training"].Value == "no")
                trainingMode = false;
            else throw new Exception("Training is set improperly.");

            if (config.AppSettings.Settings["Tracing"].Value == "yes")
                tracingMode = true;
            else if (config.AppSettings.Settings["Tracing"].Value == "no")
                tracingMode = false;
            else throw new Exception("Tracing is set improperly.");

            tracingDirectory = config.AppSettings.Settings["TracingDirectory"].Value;

            if (config.AppSettings.Settings["Generation"].Value == "random")
                generation = GenerationMode.randomGeneration;
            else if (config.AppSettings.Settings["Generation"].Value == "static")
                generation = GenerationMode.staticGeneration;
            else throw new Exception("GenerationMode is set improperly.");

            if (config.AppSettings.Settings["Distributed"].Value == "yes")
                distributedMode = true;
            else if (config.AppSettings.Settings["Distributed"].Value == "no")
                distributedMode = false;
            else throw new Exception("Distributed  is set improperly.");

            if (config.AppSettings.Settings["LoggerMode"].Value == "info")
                logger = LoggerMode.info;
            else if (config.AppSettings.Settings["LoggerMode"].Value == "debug")
                logger = LoggerMode.debug;
            else throw new Exception("LoggerMode is set improperly.");

            loggerDirectory = config.AppSettings.Settings["LoggerDirectory"].Value;
        }

        // Other function
        static public void Refresh()
        {
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            ConfigurationManager.RefreshSection("connectionStrings");
        }
        
        public enum StorageProvider
        {
            XMLProvider,
            SQLProvider
        }

        public enum GenerationMode
        {
            randomGeneration,
            staticGeneration
        }

        public enum LoggerMode
        {
            info,
            debug
        }
    }
}
