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
        protected ModelType modelType;
        protected int realizationCount;

        protected ResearchResult result;
        protected AbstractEnsembleManager currentManager;
        protected delegate void ManagerRunner();
        private ManagerType managerType;

        public AbstractResearch()
        {
            ResearchID = Guid.NewGuid();

            // TODO read from config manager type.
            managerType = ManagerType.Local;

            Status = Status.NotStarted;

            ResearchParameterValues = new Dictionary<ResearchParameter, object>();
            GenerationParameterValues = new Dictionary<GenerationParameter, object>();
            AnalyzeOption = AnalyzeOption.None;

            InitializeParameters();
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

        public Guid ResearchID { get; private set; }

        public string ResearchName { get; set; }

        public AbstractResultStorage Storage { get; set; }

        public string TracingPath { get; set; }

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

        public Status Status { get; private set; }

        public Dictionary<ResearchParameter, object> ResearchParameterValues { get; private set; }

        public Dictionary<GenerationParameter, object> GenerationParameterValues { get; private set; }

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
            // TODO correct tracing path
            currentManager.TracingPath = TracingPath + "\\" + ResearchName;
            currentManager.RealizationCount = realizationCount;
            currentManager.AnalyzeOptions = AnalyzeOption;
            FillGenerationParameters(currentManager);
        }

        /// <summary>
        /// Initializes generation parameters for single ensemble manager.
        /// </summary>
        /// <param name="m">Ensemble manager to initialize.</param>
        protected abstract void FillGenerationParameters(AbstractEnsembleManager m);

        /// <summary>
        /// Saves the results of research analyze.
        /// </summary>
        protected void SaveResearch()
        {
            result.ResearchID = ResearchID;
            result.ResearchName = ResearchName;
            result.ResearchType = GetResearchType();
            result.ModelType = modelType;
            result.RealizationCount = realizationCount;

            Storage.Save(result);
        }

        private void InitializeParameters()
        {
            RequiredResearchParameter[] rp = (RequiredResearchParameter[])this.GetType().GetCustomAttributes(typeof(RequiredResearchParameter), true);
            for (int i = 0; i < rp.Length; ++i)
                ResearchParameterValues.Add(rp[i].Parameter, null);

            ModelTypeInfo info = ((ModelTypeInfo[])modelType.GetType().GetCustomAttributes(typeof(ModelTypeInfo), false))[0];
            Type t = Type.GetType(info.Implementation, true);
            RequiredGenerationParameter[] gp = (RequiredGenerationParameter[])t.GetCustomAttributes(typeof(RequiredGenerationParameter), false);
            for (int i = 0; i < rp.Length; ++i)
                GenerationParameterValues.Add(gp[i].Parameter, null);
        }
    }
}
