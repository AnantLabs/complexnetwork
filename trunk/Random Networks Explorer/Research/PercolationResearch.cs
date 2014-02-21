using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;

using Core;
using Core.Attributes;
using Core.Enumerations;

namespace Research
{
    /// <summary>
    /// 
    /// </summary>
    [AvailableModelType(ModelType.ER)]
    [AvailableModelType(ModelType.RegularHierarchic)]
    [RequiredResearchParameter(ResearchParameter.ProbabilityMax)]
    [RequiredResearchParameter(ResearchParameter.ProbabilityDelta)]
    [AvailableAnalyzeOption(AnalyzeOption.ConnectedComponentDistribution)]
    public class PercolationResearch : AbstractResearch
    {
        private GenerationParameter probabilityParameter;
        private Single minProbability;
        private Single currentProbability;
        private Single maxProbability;
        private Single delta;

        private AbstractEnsembleManager currentManager;

        /// <summary>
        /// 
        /// </summary>
        public override void StartResearch()
        {
            if (base.generationParameterValues.ContainsKey(GenerationParameter.Probability))
                probabilityParameter = GenerationParameter.Probability;
            else if (base.generationParameterValues.ContainsKey(GenerationParameter.Mu))
                probabilityParameter = GenerationParameter.Mu;
            else
                throw new SystemException("Unexpected generation parameter set.");

            minProbability = Convert.ToSingle(base.GenerationParameterValues[probabilityParameter]);
            currentProbability = minProbability;
            maxProbability = Convert.ToSingle(base.ResearchParameterValues[ResearchParameter.ProbabilityMax]);
            delta = Convert.ToSingle(base.ResearchParameterValues[ResearchParameter.ProbabilityDelta]);

            StartCurrentEnsemble();
        }

        public override void StopResearch()
        {
            currentManager.Cancel();
        }

        private void RunCompleted(IAsyncResult res)
        {
            currentProbability += delta;
            // getting result from currentManager and add to base.result
            StartCurrentEnsemble();
        }

        private void StartCurrentEnsemble()
        {
            if (currentProbability < maxProbability)
            {
                currentManager = base.CreateEnsembleManager();
                ManagerRunner r = new ManagerRunner(currentManager.Run);
                r.BeginInvoke(new AsyncCallback(RunCompleted), null);
            }
            else
            {
                base.SaveResearch();
            }
        }

        protected override void InitializeGenerationParameters(AbstractEnsembleManager m)
        {
            Dictionary<GenerationParameter, object> g = new Dictionary<GenerationParameter, object>();
            foreach (GenerationParameter p in base.GenerationParameterValues.Keys)
            {
                if (p == probabilityParameter)
                    g.Add(p, currentProbability);
                else
                    g.Add(p, base.GenerationParameterValues[p]);
            }

            m.GenerationParameterValues = g;
        }
    }
}
