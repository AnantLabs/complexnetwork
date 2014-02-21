using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;

namespace Core
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractEnsembleManager
    {
        protected ModelType modelType;
        protected string tracingPath;
        protected int realizationCount;

        protected Dictionary<GenerationParameter, object> generationParameterValues;
        protected AnalyzeOption analyzeOptions;

        public ModelType ModelType
        {
            set { modelType = value; }
        }

        public string TracingPath
        {
            set { tracingPath = value; }
        }

        public int RealizationCount
        {
            set { realizationCount = value; }
        }

        public Dictionary<GenerationParameter, object> GenerationParameterValues
        {
            set { generationParameterValues = value; }
        }

        public AnalyzeOption AnalyzeOptions
        {
            set { analyzeOptions = value; }
        }

        public abstract void Run();
        public abstract void Cancel();
    }
}
