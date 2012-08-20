using System;
using System.Collections.Generic;
using System.Text;
using RandomGraph.Core.Manager.Interface;
using RandomGraph.Common.Model;
using RandomGraph.Common.Storage;
using RandomGraph.Core.Events;
using RandomGraph.Core.Manager.Status;
using RandomGraph.Common.Model.Status;
using CommonLibrary.Model.Result;
//using RandomGraph.Common.Model.StatAnalyzer;
using CommonLibrary.Model.Util;
using AnalyzerFramework.Manager.Impl;
using CommonLibrary.Model.Events;

namespace RandomGraph.Core.Manager.Impl
{
    /// <summary>
    /// This class is a base for all Graph manager implementations.
    /// </summary>
    public abstract class AbstractGraphManager : IGraphManager
    {
        /// <summary>
        /// Event for notification of graph manager status change.
        /// e.g it can be change execution status from Stopped to Running
        /// </summary>
        public event StatusChangedEventHandler ExecutionStatusChange;

        public event GraphsAreGenerated GraphsGenerated;

        /// <summary>
        /// Event is generated any time when one of instances changes its state.
        /// Event arguments of this event contain statuses of all instances of graph models,
        /// that are currently being executed
        /// </summary>
        public event GraphProgressEventHandler OverallProgress;

        /// <summary>
        /// The only constructor of abstract class that requires that all
        /// child classes should define storage.
        /// </summary>
        /// <param name="storage">Instance of IResultStorage implementation class. 
        /// defines a storage that will be used</param>
        public AbstractGraphManager(IResultStorage storage)
        {
            DataStorage = storage;
            Assembly = new ResultAssembly();
            GraphTables = new List<GraphTable>();
        }

        #region Properties

        public ResultAssembly Assembly { get; protected set; }

        /// <summary>
        /// List for storing all created instances of graph models
        /// </summary>
        protected List<AbstractGraphModel> Models { get; set; }


        protected List<GraphTable> GraphTables { get; set; }

        /// <summary>
        /// Reference to result storage object
        /// </summary>
        public IResultStorage DataStorage { get; protected set; }

        /// <summary>
        /// Current status of execution, e.g. Stopped, Running, Paused.
        /// </summary>
        public ExecutionStatus CurrentExecutionStatus { get; protected set; }

        #endregion

        public abstract void Stop();

        public abstract void Pause();

        public abstract void Continue();

        public abstract void Stop(int instanceID);

        public abstract void Pause(int instanceID);

        public abstract void Continue(int instanceID);

        public abstract void Start(AbstractGraphModel model, int iterations, string name);

        protected void OnExecutionStatusChange(ExecutionStatusEventArgs args)
        {
            CurrentExecutionStatus = args.ExecutionStatus;
            if (ExecutionStatusChange != null)
            {
                ExecutionStatusChange(this, args);
            }
        }

        protected void OnOverallProgress(GraphProgressEventArgs args)
        {
            if (OverallProgress != null)
            {
                OverallProgress(this, args);
            }
        }

        protected void invokeOverallProgress(AbstractGraphModel graphModel)
        {
            OnOverallProgress(new GraphProgressEventArgs(graphModel.CurrentStatus));
        }

        protected void OnGraphsGenerated()
        {
            if (GraphsGenerated != null)
            {
                GraphsGenerated(this, GraphTables);
            }
        }
    }

}
