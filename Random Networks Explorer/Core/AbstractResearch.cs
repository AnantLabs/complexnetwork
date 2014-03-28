using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Attributes;
using Core.Exceptions;
using Core.Result;

namespace Core
{
    /// <summary>
    /// Abstract class presenting single research. 
    /// </summary>
    public abstract class AbstractResearch
    {
        protected Guid researchID;
        protected ModelType modelType;
        protected string researchName;
        protected AbstractResultStorage storage;
        protected string tracingPath;
        protected int realizationCount;
        protected Status status;

        protected Dictionary<ResearchParameter, object> researchParameterValues;
        protected Dictionary<GenerationParameter, object> generationParameterValues;
        protected AnalyzeOption analyzeOption;

        protected ResearchResult result;

        protected AbstractEnsembleManager currentManager;
        protected delegate void ManagerRunner();

        private ManagerType managerType;

        public AbstractResearch()
        {
            // TODO read from config manager type.
            managerType = ManagerType.Local;

            status = Status.NotStarted;

            researchParameterValues = new Dictionary<ResearchParameter, object>();
            generationParameterValues = new Dictionary<GenerationParameter, object>();
            AnalyzeOption = AnalyzeOption.None;

            RequiredResearchParameter[] n = (RequiredResearchParameter[])this.GetType().GetCustomAttributes(typeof(RequiredResearchParameter), true);
            for (int i = 0; i < n.Length; ++i)
                researchParameterValues.Add(n[i].Parameter, null);
        }

        public ModelType ModelType
        {
            get { return modelType; }
            set
            {
                List<AvailableModelType> l = new List<AvailableModelType>((AvailableModelType[])this.GetType().GetCustomAttributes(typeof(AvailableModelType), true));
                if (l.Exists(x => x.ModelType == value))
                    modelType = value;
                else
                    throw new CoreException("Research does not support specified model type.");
            }
        }

        public string ResearchName
        {
            get { return researchName; }
            set { researchName = value; }
        }

        public AbstractResultStorage Storage
        {
            get { return storage; }
            set { storage = value; }
        }

        public string TracingPath
        {
            get { return tracingPath; }
            set { tracingPath = value; }
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

        public Status Status
        {
            get { return status; }
        }

        public Dictionary<ResearchParameter, object> ResearchParameterValues
        {
            get { return researchParameterValues; }
        }

        public Dictionary<GenerationParameter, object> GenerationParameterValues
        {
            get { return generationParameterValues; }
        }

        public AnalyzeOption AnalyzeOption
        {
            get { return analyzeOption; }
            set { analyzeOption = value; }
        }

        /// <summary>
        /// Starts research generation and analyze.
        /// </summary>
        public abstract void StartResearch();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract ResearchType GetResearchType();

        /// <summary>
        /// Force stops the research.
        /// </summary>
        public void StopResearch()
        {
            // BUG for evolution and percolation researches canceling currentManager
            // will not prevent cycle interruption.
            currentManager.Cancel();
            // TODO make existing result valid.
            SaveResearch();
        }

        /// <summary>
        /// Creates ensemble manager of corresponding type and 
        /// initializes from current research.
        /// </summary>
        protected void CreateEnsembleManager()
        {
            ManagerTypeInfo[] info = (ManagerTypeInfo[])managerType.GetType().GetCustomAttributes(typeof(ManagerTypeInfo), false);
            Type t = Type.GetType(info[0].Implementation);
            currentManager = (AbstractEnsembleManager)t.GetConstructor(null).Invoke(null);

            currentManager.ModelType = modelType;
            currentManager.TracingPath = tracingPath;
            currentManager.RealizationCount = realizationCount;
            currentManager.AnalyzeOptions = analyzeOption;
            InitializeGenerationParameters(currentManager);
        }

        /// <summary>
        /// Initializes generation parameters for single ensemble manager.
        /// </summary>
        /// <param name="m">Ensemble manager to initialize.</param>
        protected abstract void InitializeGenerationParameters(AbstractEnsembleManager m);

        /// <summary>
        /// Saves the results of research analyze.
        /// </summary>
        protected void SaveResearch()
        {
            result.ResearchID = researchID;
            result.ResearchName = researchName;
            result.ResearchType = GetResearchType();
            result.ModelType = modelType;
            result.RealizationCount = realizationCount;

            storage.Save(result);
        }
    }
}
