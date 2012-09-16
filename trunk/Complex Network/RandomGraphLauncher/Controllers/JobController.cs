using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RandomGraph.Core.Manager.Impl;
using RandomGraph.Core.Events;
using RandomGraph.Core.Manager.Status;
using AnalyzerFramework.Manager.Impl;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Settings;
using CommonLibrary.Model.Events;
using GenericAlgorithms;
using log4net;

namespace RandomGraphLauncher.Controllers
{
    // Реализация работы одного job-а в сессии.
    class JobController
    {
        // Организация работы с лог файлом.
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(JobController));

        // Имя job-а.
        private string jobName;
        // ??
        private bool finished = false;
        // Тип модели job-а.
        private Type modelType;
        // Значения параметров генерации.
        private Dictionary<GenerationParam, object> genParamValues;
        // Имя файла для статической генерации.
        private string filePath;
        // Выбранные свойства для анализа.
        private AnalyseOptions selectedOptions = AnalyseOptions.None;
        // Значения для некоторых свойств анализа.
        private Dictionary<string, object> analyzeOptionValues = new Dictionary<string, object>();
        // Число реализаций.
        private int instanceCount;
        // Manager графа.
        private AbstractGraphManager manager;
        // Текст ошибки.
        private string errorMessage;

        // Конструктор, который инициализирует manager графа.
        public JobController(Type modelType, string jobName)
        {
            this.modelType = modelType;
            this.jobName = jobName;
            InitializeGraphManager();
        }

        // ??
        public bool CheckParameters()
        {
            if (Options.GenerationMode.randomGeneration == Options.Generation)
            {
                Type[] constructTypes = new Type[] { typeof(Dictionary<GenerationParam, object>), 
                    typeof(AnalyseOptions), 
                    typeof(Dictionary<String, Object>) };
                object[] invokeParams = new object[] { genParamValues, selectedOptions, null };

                AbstractGraphModel graphModel = (AbstractGraphModel)this.modelType.GetConstructor(constructTypes).
                    Invoke(invokeParams);
                errorMessage = graphModel.GetParamsInfo();
                return graphModel.CheckGenerationParams(instanceCount);
            }
            else
                return true;
        }

        public void Start(object[] invokeParams)
        {
            // !убедиться!
            if (Options.GenerationMode.randomGeneration == Options.Generation)
            {
                Type[] constructTypes = new Type[] { typeof(Dictionary<GenerationParam, object>), 
                    typeof(AnalyseOptions), 
                    typeof(Dictionary<String, Object>) };
                manager.Start((AbstractGraphModel)modelType.GetConstructor(constructTypes).Invoke(invokeParams), 
                    instanceCount, jobName);
            }
            else if (Options.GenerationMode.staticGeneration == Options.Generation)
            {
                Type[] constructTypes = new Type[] { typeof(ArrayList), 
                    typeof(AnalyseOptions), 
                    typeof(Dictionary<String, Object>) };
                invokeParams[0] = MatrixFileReader.MatrixReader(filePath);
                AbstractGraphModel graphModel = (AbstractGraphModel)modelType.GetConstructor(constructTypes).Invoke(invokeParams);
                manager.Start((AbstractGraphModel)modelType.GetConstructor(constructTypes).Invoke(invokeParams),
                    instanceCount, jobName);
            }
        }

        public void SetStatusChangedEventHandler(StatusChangedEventHandler manager_ExecutionStatusChange)
        {
            manager.ExecutionStatusChange += new StatusChangedEventHandler(manager_ExecutionStatusChange);
        }

        public void SetGraphProgressEventHandler(GraphProgressEventHandler manager_GraphProgressEventHandler)
        {
            manager.OverallProgress += new GraphProgressEventHandler(manager_GraphProgressEventHandler);
        }

        public void SetGraphsGeneratedEventHandler(GraphsAreGenerated manager_GraphsGenerated)
        {
            manager.GraphsGenerated += new GraphsAreGenerated(manager_GraphsGenerated);
        }

        public void Save()
        {
            manager.DataStorage.Save(manager.Assembly);
        }

        // Свойства
        public bool Finished
        {
            get { return finished; }
            set { finished = value; }
        }

        public Type ModelType
        {
            get { return modelType; }
        }

        public AbstractGraphManager Manager
        {
            get { return manager; }
        }

        public Dictionary<GenerationParam, object> GenParamValues
        {
            get { return genParamValues; }
            set { genParamValues = value; }
        }

        public string FilePath
        {
            set { filePath = value; }
        }

        public AnalyseOptions SelectedOptions
        {
            get { return selectedOptions; }
            set { selectedOptions = value; }
        }

        public Dictionary<string, object> AnalyzeOptionValues
        {
            get { return analyzeOptionValues; }
        }

        public int InstanceCount
        {
            set { instanceCount = value; }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
        }

        public int ResultsCount
        {
            get { return manager.Assembly.Results.Count; }
        }

        // Утилиты

        private void InitializeGraphManager()
        {
            if (Options.DistributedMode)
            {
                manager = new DistributedGraphManager(Options.StorageManager, Options.Generation, Options.TracingMode);
            }
            else
            {
                manager = new MultiTreadGraphManager(Options.StorageManager, Options.Generation, Options.TracingMode);
            }
        }
    }
}
