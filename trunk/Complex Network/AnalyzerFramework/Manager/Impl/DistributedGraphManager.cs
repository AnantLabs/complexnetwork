using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RandomGraph.Settings;
using RandomGraph.Core.Manager.Impl;
using RandomGraph.Common.Model;
using RandomGraph.Common.Storage;
using System.ServiceModel.Discovery;
using System.ServiceModel;
using RandomGraph.Core.Manager.Status;
using RandomGraph.Core.Exceptions;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Attributes;
using WcfClient;
using AnalyzerFramework.ServiceReference1;
using RandomGraph.Core.Events;
using System.Threading;
using log4net;

namespace AnalyzerFramework.Manager.Impl
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext=false)]
    public class CallbackHandler : IComplexNetworkWorkerServiceCallback
    {
        /// <summary>
        /// The logger static object for monitoring.
        /// </summary>
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(CallbackHandler));

        private DistributedGraphManager manager;

        public CallbackHandler(DistributedGraphManager distributedGraphManager)
        {
            manager = distributedGraphManager;
        }

        public void ProgressReport(AbstractGraphModel model, GraphProgressEventArgs args)
        {
            manager.OnSeparateModelProgress(model, args);   
        }

        public IAsyncResult BeginProgressReport(AbstractGraphModel model, GraphProgressEventArgs args, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndProgressReport(IAsyncResult result)
        {
            throw new NotImplementedException();
        }
    }
    
    public class DistributedGraphManager : AbstractGraphManager
    {
        /// <summary>
        /// The logger static object for monitoring.
        /// </summary>
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(DistributedGraphManager));

        private int modelsCountInEachService;
        private int iterations;
        List<HelpService> services = new List<HelpService>();

        public DistributedGraphManager(IResultStorage storage)
            : base(storage) 
        {
            // Construct InstanceContext to handle messages on callback interface
            InstanceContext instanceContext = new InstanceContext(new CallbackHandler(this));
            /*
            IList<EndpointDiscoveryMetadata> serviceInfos = ServiceDiscoveryManager.SelectedServices;
            if (serviceInfos.Count == 0)
            {
                serviceInfos = ServiceDiscoveryManager.GetServices();
            }
            foreach (var item in serviceInfos)
            {
                services.Add(new HelpService(instanceContext, item));
            }
            */
            services.Add(new HelpService(instanceContext, null));
        }

        public override void Stop()
        {
            if (CurrentExecutionStatus == ExecutionStatus.Stopped ||
                CurrentExecutionStatus == ExecutionStatus.Stopping ||
                CurrentExecutionStatus == ExecutionStatus.Failed ||
                CurrentExecutionStatus == ExecutionStatus.Success)
            {
                throw new WrongExecutionStatusException("execution stopped already");
            }
            foreach (HelpService service in services)
            {
                service.StopAll();
            }
            OnExecutionStatusChange(new ExecutionStatusEventArgs(ExecutionStatus.Stopped));
        }

        public override void Pause()
        {
            if (CurrentExecutionStatus == ExecutionStatus.Stopped ||
                CurrentExecutionStatus == ExecutionStatus.Stopping ||
                CurrentExecutionStatus == ExecutionStatus.Failed ||
                CurrentExecutionStatus == ExecutionStatus.Paused ||
                CurrentExecutionStatus == ExecutionStatus.Success)
            {
                throw new WrongExecutionStatusException("execution can not pause");
            }
            foreach (HelpService service in services)
            {
                service.PauseAll();
            }
            OnExecutionStatusChange(new ExecutionStatusEventArgs(ExecutionStatus.Paused));
        }

        public override void Continue()
        {
            if (CurrentExecutionStatus != ExecutionStatus.Paused)
            {
                throw new WrongExecutionStatusException("execution can not be continued");
            }
            foreach (HelpService service in services)
            {
                service.ContinueAll();
            }
            OnExecutionStatusChange(new ExecutionStatusEventArgs(ExecutionStatus.Running));
        }

        public override void Stop(int instanceID)
        {
            HelpService service = services[instanceID / modelsCountInEachService];
            service.StopInstance(instanceID);
        }

        public override void Pause(int instanceID)
        {
            HelpService service = services[instanceID / modelsCountInEachService];
            service.PauseInstance(instanceID);
        }

        public override void Continue(int instanceID)
        {
            HelpService service = services[instanceID / modelsCountInEachService];
            service.ContinueInstance(instanceID);
        }

        public override void Start(AbstractGraphModel model, int iterations, string name)
        {
            if (CurrentExecutionStatus != ExecutionStatus.Stopped)
            {
                throw new WrongExecutionStatusException("should be stopped before new start");
            }
            this.iterations = iterations;

            Assembly.AnalizeOptions = model.AnalyzeOptions;
            Assembly.GenerationParams = model.GenerationParamValues;
            Assembly.AnalyzeOptionParams = model.AnalyzeOptionsValues;
            Assembly.ModelType = model.GetType();
            Assembly.ModelName = Assembly.ModelType.Name;
            Assembly.Name = name;
            Assembly.Size = model.GetNetworkSize();

            OnExecutionStatusChange(new ExecutionStatusEventArgs(ExecutionStatus.Starting));

            modelsCountInEachService = (iterations / services.Count) ;
            if (iterations % services.Count != 0)
            {
                modelsCountInEachService ++;
            }

            for (int i = 0; i < services.Count; i++)
			{
                int startIndex = i * modelsCountInEachService;
                int endIndex = (i + 1) * modelsCountInEachService;
                services[i].Start(model, startIndex, endIndex > iterations ? iterations : endIndex);
			}
        }

        public void OnSeparateModelProgress(AbstractGraphModel model, GraphProgressEventArgs args)
        {
            invokeOverallProgress(model);
            if (args.Progress.GraphProgress == GraphProgress.Done)
            {
                Assembly.Results.Add(model.Result);
                if (Assembly.Results.Count == iterations)
                {
                    OnExecutionStatusChange(new ExecutionStatusEventArgs(ExecutionStatus.Success));
                }
            }
            else if (args.Progress.GraphProgress == GraphProgress.Failed)
            {
                OnExecutionStatusChange(new ExecutionStatusEventArgs(ExecutionStatus.Failed));
            }
        }
    }
}
