using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CommonLibrary.Model.Attributes;
using CommonLibrary.Model.Util;
using Flash.External;
using Newtonsoft.Json;
using RandomGraph.Common.Model.Settings;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using RandomGraph.Core.Events;
using RandomGraph.Core.Manager.Impl;
using RandomGraph.Core.Manager.Status;
using RandomGraphLauncher.controls;
using RandomGraphLauncher.models;
using RandomGraphLauncher.Properties;
using log4net;
using GenericAlgorithms;

namespace RandomGraphLauncher.src
{
    class ViewController
    {
        /// <summary>
        /// The logger static object for monitoring.
        /// </summary>
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(ViewController));
        public bool isDistributed { get; set; }
        public bool isTrainingMode { get; set; }
        public bool isTraceingMode { get; set; }
        public int instances { get; set; }
        public string errorMessage { get; set; }
        public AnalyseOptions availableOptions { get; set; }
        public List<RequiredGenerationParam> reqGenParams { get; set; }
        public Dictionary<GenerationParam, object> genParams { get; set; }
        public Dictionary<String, Object> AnalizeOptionsValues = new Dictionary<string, object>();
        private ExternalInterfaceProxy proxy { get; set; }
        private AbstractGraphManager manager { get; set; }
        private Type modelType { get; set; }
        private string jobName { get; set; }

        public void Init(Type arg_modelType, 
            string jobName, 
            AbstractGraphManager manager)
        {
            this.manager = manager;            
            this.jobName = jobName;
            this.modelType = arg_modelType;

            this.isTrainingMode = Options.TracingMode;
            this.isTraceingMode = Options.TracingMode;
            this.isDistributed = Options.DistributedMode;
            reqGenParams = new List<RequiredGenerationParam>((RequiredGenerationParam[])this.modelType.GetCustomAttributes(typeof(RequiredGenerationParam), false));
            AvailableAnalyzeOptions[] optionsAttributes = (AvailableAnalyzeOptions[])this.modelType.GetCustomAttributes(typeof(AvailableAnalyzeOptions), false);
            this.availableOptions = optionsAttributes[0].Options;
            this.genParams = new Dictionary<GenerationParam, object>();
        }

        public void SetStatusChangedEventHandler(StatusChangedEventHandler manager_ExecutionStatusChange)
        {
            this.manager.ExecutionStatusChange += new StatusChangedEventHandler(manager_ExecutionStatusChange);
        }

        public void SetGraphProgressEventHandler(GraphProgressEventHandler manager_GraphProgressEventHandler)
        {
            this.manager.OverallProgress += new GraphProgressEventHandler(manager_GraphProgressEventHandler);
        }

        public void SetGraphsGeneratedEventHandler(CommonLibrary.Model.Events.GraphsAreGenerated manager_GraphsGenerated)
        {
            this.manager.GraphsGenerated += new CommonLibrary.Model.Events.GraphsAreGenerated(manager_GraphsGenerated);
        }

        public void InitFlashApi(AxShockwaveFlashObjects.AxShockwaveFlash proxy, ExternalInterfaceCallEventHandler proxy_ExternalInterfaceCall)
        {
            this.proxy = new ExternalInterfaceProxy(proxy);
            this.proxy.ExternalInterfaceCall += new ExternalInterfaceCallEventHandler(proxy_ExternalInterfaceCall);
        }

        public void CallFlash(String jsonString)
        {
            this.proxy.Call("sendToActionScript", jsonString);
        }

        public GraphModel GetGraphModel()
        {
            return (GraphModel)(this.modelType.GetCustomAttributes(typeof(GraphModel), false)[0]);
        }

        public void Save()
        {
            this.manager.DataStorage.Save(this.manager.Assembly);
        }

        public void StartGraphModel(object[] invokeParams)
        {
            if (Options.GenerationMode.randomGeneration == Options.Generation)
            {
                Type[] constructTypes = new Type[] { typeof(Dictionary<GenerationParam, object>), typeof(AnalyseOptions), typeof(Dictionary<String, Object>) };
                AbstractGraphModel graphModel = (AbstractGraphModel)this.modelType.GetConstructor(constructTypes).Invoke(invokeParams);
                this.manager.Start(graphModel, this.instances, this.jobName);
            }
            else if (Options.GenerationMode.staticGeneration == Options.Generation)
            {
                Type[] constructTypes = new Type[] { typeof(ArrayList), typeof(AnalyseOptions), typeof(Dictionary<String, Object>) };
                String path = (String)((Dictionary<GenerationParam, object>)invokeParams[0])[GenerationParam.Vertices];
                invokeParams[0] = MatrixFileReader.MatrixReader(path);
                AbstractGraphModel graphModel = (AbstractGraphModel)this.modelType.GetConstructor(constructTypes).Invoke(invokeParams);
                this.manager.Start(graphModel, this.instances, this.jobName);
            }
        }

        public bool CheckGenerationParams(AnalyseOptions selectedOptions)
        {
            if (Options.GenerationMode.randomGeneration == Options.Generation)
            {
                Type[] constructTypes = new Type[] { typeof(Dictionary<GenerationParam, object>), typeof(AnalyseOptions), typeof(Dictionary<String, Object>) };
                object[] invokeParams = new object[] { genParams, selectedOptions, null };
                AbstractGraphModel graphModel = (AbstractGraphModel)this.modelType.GetConstructor(constructTypes).Invoke(invokeParams);
                errorMessage = graphModel.GetParamsInfo();
                return graphModel.CheckGenerationParams(this.instances);
            }
            else
                return true;    // !Исправить!
        }

        public int ResultCount()
        {
            return manager.Assembly.Results.Count;
        }

        public void Continue()
        {
            this.manager.Continue();
        }

        public void Pause()
        {
            this.manager.Pause();
        }

        public void Stop()
        {
            this.manager.Stop();
        }
        public void Continue(int index)
        {
            this.manager.Continue(index);
        }

        public void Pause(int index)
        {
            this.manager.Pause(index);
        }

        public void Stop(int index)
        {
            this.manager.Stop(index);
        }
    }
}
