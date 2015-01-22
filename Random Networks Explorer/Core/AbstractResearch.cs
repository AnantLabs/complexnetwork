using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Attributes;
using Core.Exceptions;
using Core.Result;
using Core.Events;
using Core.Settings;

namespace Core
{
    /// <summary>
    /// Abstract class presenting single research. 
    /// </summary>
    public abstract class AbstractResearch
    {
        protected ModelType modelType = ModelType.ER;
        protected int realizationCount = 1;

        private ManagerType managerType;
        protected AbstractEnsembleManager currentManager;
        protected delegate void ManagerRunner();

        private ResearchStatus status;
        protected ResearchResult result = new ResearchResult();

        public event ResearchStatusUpdateHandler OnUpdateResearchStatus;
        public event ResearchEnsembleStatusUpdateHandler OnUpdateResearchEnsembleStatus;

        public AbstractResearch()
        {
            ResearchID = Guid.NewGuid();
            Status = ResearchStatus.NotStarted;
            GenerationType = GenerationType.Random;

            ResearchParameterValues = new Dictionary<ResearchParameter, object>();
            GenerationParameterValues = new Dictionary<GenerationParameter, object>();
            AnalyzeOption = AnalyzeOption.None;

            InitializeResearchParameters();
            InitializeGenerationParameters();

            managerType = ExplorerSettings.WorkingMode;
        }

        public Guid ResearchID { get; private set; }

        public string ResearchName { get; set; }

        public ModelType ModelType
        {
            get { return modelType; }
            set
            {
                List<AvailableModelType> l = new List<AvailableModelType>((AvailableModelType[])this.GetType().GetCustomAttributes(typeof(AvailableModelType), true));
                if (l.Exists(x => x.ModelType == value))
                {
                    modelType = value;
                    InitializeGenerationParameters();
                }
                else
                    throw new CoreException("Research does not support specified model type.");
            }
        }

        public AbstractResultStorage Storage { get; set; }

        public GenerationType GenerationType { get; set; }

        public string TracingPath { get; set; }

        public ResearchStatus Status 
        {
            get { return status; }
            protected set
            {
                status = value;

                // Make sure someone is listening to event
                if (OnUpdateResearchStatus == null)
                    return;

                // Invoke event for GUI
                OnUpdateResearchStatus(this, new ResearchEventArgs(ResearchID, Status, Status.ToString()));
            }
        }

        public int RealizationCount
        {
            get { return realizationCount; }
            set 
            {
                if (value > 0)
                    realizationCount = value;
                else
                    throw new CoreException("Realization count cannot be less then 1.");
            }
        }

        public Dictionary<ResearchParameter, object> ResearchParameterValues { get; set; }

        public Dictionary<GenerationParameter, object> GenerationParameterValues { get; set; }

        public AnalyzeOption AnalyzeOption { get; set; }

        /// <summary>
        /// Starts research generation, analyze and save.
        /// </summary>
        public abstract void StartResearch();

        /// <summary>
        /// Force stops the research.
        /// </summary>
        public abstract void StopResearch();

        /// <summary>
        /// Returns research type.
        /// </summary>
        /// <returns>Research type.</returns>
        public abstract ResearchType GetResearchType();

        public NetworkEventArgs[] GetEnsembleStatus()
        {
            if (currentManager != null)
                return currentManager.NetworkStatuses;
            else
                return null;
        }

        /// <summary>
        /// Creates ensemble manager of corresponding type and 
        /// initializes from current research.
        /// </summary>
        protected void CreateEnsembleManager()
        {
            ManagerTypeInfo[] info = (ManagerTypeInfo[])managerType.GetType().GetField(managerType.ToString()).GetCustomAttributes(typeof(ManagerTypeInfo), false);
            Type t = Type.GetType(info[0].Implementation);
            currentManager = (AbstractEnsembleManager)t.GetConstructor(Type.EmptyTypes).Invoke(new object[0]);

            currentManager.ModelType = modelType;
            currentManager.TracingPath = (TracingPath == "" ? "" : TracingPath + "\\" + ResearchName);
            currentManager.RealizationCount = realizationCount;
            currentManager.ResearchName = ResearchName;
            currentManager.AnalyzeOptions = AnalyzeOption;
            FillParameters(currentManager);

            currentManager.OnUpdateStatus += new EnsembleStatusUpdateHandler(CurrentManager_OnUpdateStatus);
        }

        /// <summary>
        /// Initializes generation parameters for single ensemble manager.
        /// </summary>
        /// <param name="m">Ensemble manager to initialize.</param>
        protected abstract void FillParameters(AbstractEnsembleManager m);

        /// <summary>
        /// Saves the results of research analyze.
        /// </summary>
        protected void SaveResearch()
        {
            if (result.EnsembleResults.Count() != 0 && result.EnsembleResults[0] == null)
                return;
            result.ResearchID = ResearchID;
            result.ResearchName = ResearchName;
            result.ResearchType = GetResearchType();
            result.ModelType = modelType;
            result.RealizationCount = realizationCount;
            result.Size = result.EnsembleResults[0].NetworkSize;
            result.Date = DateTime.Now;

            result.ResearchParameterValues = ResearchParameterValues;
            result.GenerationParameterValues = GenerationParameterValues;

            Storage.Save(result);
        }

        private void InitializeParameters()
        {
            ResearchParameterValues.Clear();
            GenerationParameterValues.Clear();

            RequiredResearchParameter[] rp = (RequiredResearchParameter[])this.GetType().GetCustomAttributes(typeof(RequiredResearchParameter), true);
            for (int i = 0; i < rp.Length; ++i)
                ResearchParameterValues.Add(rp[i].Parameter, null);

            ModelTypeInfo info = ((ModelTypeInfo[])modelType.GetType().GetField(modelType.ToString()).GetCustomAttributes(typeof(ModelTypeInfo), false))[0];
            Type t = Type.GetType(info.Implementation, true);
            RequiredGenerationParameter[] gp = (RequiredGenerationParameter[])t.GetCustomAttributes(typeof(RequiredGenerationParameter), false);
            for (int i = 0; i < gp.Length; ++i)
                GenerationParameterValues.Add(gp[i].Parameter, null);
        }

        private void InitializeResearchParameters()
        {
            ResearchParameterValues.Clear();

            RequiredResearchParameter[] rp = (RequiredResearchParameter[])this.GetType().GetCustomAttributes(typeof(RequiredResearchParameter), true);
            for (int i = 0; i < rp.Length; ++i)
                ResearchParameterValues.Add(rp[i].Parameter, null);
        }

        private void InitializeGenerationParameters()
        {
            GenerationParameterValues.Clear();

            ModelTypeInfo info = ((ModelTypeInfo[])modelType.GetType().GetField(modelType.ToString()).GetCustomAttributes(typeof(ModelTypeInfo), false))[0];
            Type t = Type.GetType(info.Implementation, true);
            RequiredGenerationParameter[] gp = (RequiredGenerationParameter[])t.GetCustomAttributes(typeof(RequiredGenerationParameter), false);
            for (int i = 0; i < gp.Length; ++i)
                GenerationParameterValues.Add(gp[i].Parameter, null);
        }

        private void CurrentManager_OnUpdateStatus(object sender, EnsembleEventArgs e)
        {
            InvokeUpdateResearchStatus(e);
            InvokeUpdateResearchEnsembleStatus(e);
        }

        private void InvokeUpdateResearchStatus(EnsembleEventArgs e)
        {
            if (e.UpdatedStatus == NetworkStatus.Failed)
                Status = ResearchStatus.Failed;
        }

        void InvokeUpdateResearchEnsembleStatus(EnsembleEventArgs e)
        {
            // Make sure someone is listening to event
            if (OnUpdateResearchEnsembleStatus == null)
                return;

            // Invoke event for GUI
            OnUpdateResearchEnsembleStatus(this, new ResearchEnsembleEventArgs(ResearchID, e));
        }
    }
}
