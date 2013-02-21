using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandomGraph.Core.Manager.Impl;
using RandomGraph.Common.Model;
using System.Threading;
using CommonLibrary.Model.Attributes;
using RandomGraph.Settings;
using RandomGraph.Core.Exceptions;
using RandomGraph.Core.Manager.Status;
using RandomGraph.Core.Events;
using RandomGraph.Common.Model.Status;
using RandomGraph.Common.Storage;
using CommonLibrary.Model.Util;
using log4net;

namespace AnalyzerFramework.Manager.Impl
{
    public class MultiTreadGraphManager : AbstractGraphManager 
    {
        /// <summary>
        /// The logger static object for monitoring.
        /// </summary>
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(MultiTreadGraphManager));
        private int iterations;
        private Thread[] threads;
        private AutoResetEvent[] waitHandles;

        public MultiTreadGraphManager(IResultStorage storage)
            : base(storage)
        {
            log.Info("Called constructor of MultiTreadGraphManager");
        }

        public override void Start(AbstractGraphModel origineModel, int iterations, string name)
        {
            log.Info("Started multi threaded calculation");
            if (CurrentExecutionStatus != ExecutionStatus.Stopped)
            {
                log.Info("Wrong Execution Status, calculation ended");
                log.Fatal("WrongExecutionStatusException throwed");
                throw new WrongExecutionStatusException("should be stopped before new start");
            }
            this.iterations = iterations;

            //Set permanent status
            origineModel.PermanentStatus = true;

            Assembly.AnalizeOptions = origineModel.AnalyzeOptions;
            Assembly.GenerationParams = origineModel.GenerationParamValues;
            Assembly.ModelType = origineModel.GetType();
            Assembly.Name = name;
            Assembly.Size = origineModel.GetNetworkSize();

            OnExecutionStatusChange(new ExecutionStatusEventArgs(ExecutionStatus.Starting));

            waitHandles = new AutoResetEvent[iterations];
            Models = new List<AbstractGraphModel>(iterations);
            threads = new Thread[iterations];
            int processorcount = Environment.ProcessorCount;
            log.Info("Started creating thread for each instance");
            for (int i = 0; i < iterations; i++)
            {
                AbstractGraphModel model;
                if (Options.GenerationMode.staticGeneration == GenerationMode) 
                {
                    model = origineModel.CloneStatic();
                }
                else 
                {
                    model = origineModel.CloneRandom();
                }
                model.SetID(i);
                model.Progress += new GraphProgressEventHandler(OnSeparateModelProgress);
                model.GraphGenerated += new CommonLibrary.Model.Events.GraphGeneratedDelegate(model_GraphGenerated);
                Models.Add(model);

                waitHandles[i] = new AutoResetEvent(false);
                threads[i] = new Thread(new ParameterizedThreadStart(StartAnalyze)) { Priority = ThreadPriority.Lowest };
            }


            log.Info("Ended creating thread for each instance");
            for (int i = 0; i < iterations; i++)
            {
                if (Models[i].CurrentStatus.GraphProgress != GraphProgress.Stopped)
                {
                    log.Info("Threads are starting");
                    threads[i].Start(i);
                    if ((i + 1) % processorcount == 0)
                    {
                        log.Info("Threads are joined");
                        for (int j = 0; j < processorcount; ++j)
                            threads[i - j].Join();
                    }
                }
            }
            Thread waitingThread = new Thread(new ThreadStart(Waiting));
            waitingThread.Start();
        }


        private void StartAnalyze(object obj)
        {
            log.Info("Start Analyze");
            int index = (int)obj;
            try
            {
                Models[index].StartGenerate();
                if (TracingMode)
                {
                    Models[index].StartTrace(index, Models[index].ModelName, Assembly.Name);
                }
                if (Options.TrainingMode)
                {
                    Models[index].UpdateGeneratedMatrix();
                }
                Models[index].StartAnalize();
            }
            catch (SystemException ex)
            {
                log.Error(ex);
                Models[index].InvokeFailureProgressEvent(GraphProgress.Stopped, "User stopped calculation");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Models[index].InvokeFailureProgressEvent(GraphProgress.Failed, "");
            }
            finally
            {
                log.Info("Disposing objects");
                Models[index].Dispose();
                waitHandles[index].Set();
            }
        }

        private void Waiting()
        {
            foreach (WaitHandle wh in waitHandles)
            {
                wh.WaitOne();
            }
            if (CurrentExecutionStatus != ExecutionStatus.Stopped)
            {
                bool isAllStopped = false;
                foreach (var model in Models)
                {
                    if (model.CurrentStatus.GraphProgress == GraphProgress.Stopped)
                    {
                        isAllStopped = true;
                    }
                }
                if (!isAllStopped)
                {
                    bool isFailed = false;
                    foreach (var model in Models)
                    {
                        if (model.CurrentStatus.GraphProgress == GraphProgress.Failed)
                        {
                            isFailed = true;
                        }
                    }
                    OnExecutionStatusChange(new ExecutionStatusEventArgs(isFailed ? ExecutionStatus.Failed : ExecutionStatus.Success));
                }
            }
        }

        public override void Stop()
        {
            log.Info("Calculation stoped");
            if (CurrentExecutionStatus == ExecutionStatus.Stopped ||
                CurrentExecutionStatus == ExecutionStatus.Stopping ||
                CurrentExecutionStatus == ExecutionStatus.Failed ||
                CurrentExecutionStatus == ExecutionStatus.Success)
            {
                log.Error("Execution stopped already");
                throw new WrongExecutionStatusException("execution stopped already");
            }
            for (int i = 0; i < iterations; i++)
            {
                Stop(i);
            }
            OnExecutionStatusChange(new ExecutionStatusEventArgs(ExecutionStatus.Stopped));
        }

        public override void Pause()
        {
            log.Info("Calculation paused");
            if (CurrentExecutionStatus == ExecutionStatus.Stopped ||
                CurrentExecutionStatus == ExecutionStatus.Stopping ||
                CurrentExecutionStatus == ExecutionStatus.Failed ||
                CurrentExecutionStatus == ExecutionStatus.Paused ||
                CurrentExecutionStatus == ExecutionStatus.Success)
            {
                log.Error("execution can not pause");
                throw new WrongExecutionStatusException("execution can not pause");
            }
            for (int i = 0; i < iterations; i++)
            {
                Pause(i);
            }
            OnExecutionStatusChange(new ExecutionStatusEventArgs(ExecutionStatus.Paused));
        }

        public override void Continue()
        {
            log.Info("Continue");
            if (CurrentExecutionStatus != ExecutionStatus.Paused)
            {
                log.Error("execution can not be continued");
                throw new WrongExecutionStatusException("execution can not be continued");
            }
            for (int i = 0; i < iterations; i++)
            {
                Continue(i);
            }
            OnExecutionStatusChange(new ExecutionStatusEventArgs(ExecutionStatus.Running));
        }

        public override void Stop(int instanceID)
        {
            log.Info("Stoped instance with ID" + instanceID.ToString());
            Thread thread = threads[instanceID];
            if (thread.ThreadState != ThreadState.Suspended)
            {
                try
                {
                    thread.Abort();
                }
                catch (ThreadStateException ex) 
                {
                    log.Error(ex);
                }
                Models[instanceID].CurrentStatus.GraphProgress = GraphProgress.Stopped;
            }
        }

        public override void Pause(int instanceID)
        {
            if (threads[instanceID].IsAlive)
            {
                log.Info("Paused instance with ID" + instanceID.ToString());
                threads[instanceID].Suspend();
                Models[instanceID].InvokeFailureProgressEvent(GraphProgress.Paused, "User paused calculation");
            }
        }

        public override void Continue(int instanceID)
        {
            Thread thread = threads[instanceID];
            if (thread.ThreadState == ThreadState.Suspended || thread.ThreadState == ThreadState.Unstarted)
            {
                log.Info("Resumed instance with ID" + instanceID.ToString());
                thread.Resume();
            }
            Models[instanceID].InvokeFailureProgressEvent(GraphProgress.Calculating, "");
        }

        /// <summary>
        /// Event handler for monitoring processes for separate generation.
        /// Calls analyze method of model when its generation is finished.
        /// Also saves results of fully processed model.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSeparateModelProgress(object sender, GraphProgressEventArgs e)
        {
            AbstractGraphModel senderModel = (AbstractGraphModel)sender;
            invokeOverallProgress(senderModel);
            if (e.Progress.GraphProgress == GraphProgress.Done)
            {
                Assembly.Results.Add(senderModel.Result);
            }
        }

        void model_GraphGenerated(object sender, CommonLibrary.Model.Events.GraphGeneratedArgs e)
        {
            GraphTables.Add(e.GeneratedArgs);
            if (GraphTables.Count == iterations)
            {
                OnGraphsGenerated();
            }
        }

    }
}
