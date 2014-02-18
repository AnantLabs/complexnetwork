using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Attributes;
using Core.Exceptions;

namespace Core
{
    public abstract class AbstractResearch
    {
        protected ModelType modelType;
        protected string researchName;
        protected AbstractResultStorage storage;
        protected string tracingPath;
        protected GenerationType generationType;
        protected int realizationCount;
        protected Status status;

        protected Dictionary<ResearchParameter, object> researchParameterValues;
        protected Dictionary<GenerationParameter, object> generationParameterValues;
        protected AnalyzeOption analyzeOption;

        protected AbstractResult result;

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

        public GenerationType GenerationType
        {
            get { return generationType; }
            set { generationType = value; }
        }

        public int RealizationCount
        {
            get { return realizationCount; }
            set { realizationCount = value; }
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

        public AbstractResearch()
        {
            status = Status.NotStarted;

            researchParameterValues = new Dictionary<ResearchParameter, object>();
            generationParameterValues = new Dictionary<GenerationParameter, object>();
            AnalyzeOption = AnalyzeOption.None;

            RequiredResearchParameter[] n = (RequiredResearchParameter[])this.GetType().GetCustomAttributes(typeof(RequiredResearchParameter), true);
            for (int i = 0; i < n.Length; ++i)
                researchParameterValues.Add(n[i].Parameter, null);
        }

        public abstract void StartResearch();
        public abstract void StopResearch();
    }
}
