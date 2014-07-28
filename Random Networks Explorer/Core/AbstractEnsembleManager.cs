using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Result;
using Core.Events;

namespace Core
{
    /// <summary>
    /// Abstract class presenting ensemble manager.
    /// </summary>
    public abstract class AbstractEnsembleManager
    {
        protected AbstractNetwork[] networks;
        protected int realizationsDone;

        public ModelType ModelType { protected get; set; }

        public string TracingPath { protected get; set; }

        public int RealizationCount { protected get; set; }

        public Dictionary<ResearchParameter, object> ResearchParamaterValues { get; set; }

        public Dictionary<GenerationParameter, object> GenerationParameterValues { get; set; }

        public AnalyzeOption AnalyzeOptions { get; set; }

        public int RealizationsDone 
        {
            get { return realizationsDone; }
            protected set { realizationsDone = value; }
        }

        public EnsembleResult Result { get; protected set; }

        public NetworkEventArgs[] NetworkStatuses { get; protected set; }

        public event EnsembleStatusUpdateHandler OnUpdateStatus;

        /// <summary>
        /// Runs generation, analyze and save for each realization in single ensemble.
        /// Blocks current thread until whole work completes.
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// Terminates running operations.
        /// </summary>
        public abstract void Cancel();

        protected void AbstractEnsembleManager_OnUpdateNetworkStatus(object sender, NetworkEventArgs e)
        {
            NetworkStatuses[e.ID].Status = e.Status;
            NetworkStatuses[e.ID].ExtendedInfo = e.ExtendedInfo;

            // Make sure someone is listening to event
            if (OnUpdateStatus == null)
                return;

            // Invoke event for AbstractResearch
            OnUpdateStatus(this, new EnsembleEventArgs(e));
        }
    }
}
