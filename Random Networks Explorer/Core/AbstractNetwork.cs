using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Model;

namespace Core
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractNetwork
    {
        protected INetworkGenerator networkGenerator;
        protected INetworkAnalyzer networkAnalyzer;

        protected Dictionary<GenerationParameter, object> generationParameter;
        protected AnalyzeOption analyzeOptions;
        protected string tracingPath;

        public AbstractNetwork(Dictionary<GenerationParameter, object> genParams,
            AnalyzeOption analyzeOpts, string trPath)
        {
            generationParameter = genParams;
            analyzeOptions = analyzeOpts;
            tracingPath = trPath;
        }

        public abstract void Generate();
        public abstract void Analyze();

        private void Trace()
        {
        }
    }
}
