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
    /// Percolation research implementation.
    /// </summary>
    [AvailableModelType(ModelType.ER)]
    [AvailableModelType(ModelType.RegularHierarchic)]
    [RequiredResearchParameter(ResearchParameter.ProbabilityMax)]
    [RequiredResearchParameter(ResearchParameter.ProbabilityDelta)]
    [AvailableAnalyzeOption(AnalyzeOption.ConnectedComponentDistribution)]
    public class PercolationResearch : AbstractResearch
    {
        private bool isCanceled = false;

        private GenerationParameter probabilityParameter;
        private Single minProbability;
        private Single currentProbability;
        private Single maxProbability;
        private Single delta;

        /// <summary>
        /// Creates multiple EnsembleManagers, running sequentially.
        /// </summary>
        public override void StartResearch()
        {
            if (GenerationParameterValues.ContainsKey(GenerationParameter.Probability))
                probabilityParameter = GenerationParameter.Probability;
            else if (GenerationParameterValues.ContainsKey(GenerationParameter.Mu))
                probabilityParameter = GenerationParameter.Mu;
            else
                throw new SystemException("Unexpected generation parameter set.");

            minProbability = (Single)GenerationParameterValues[probabilityParameter];
            currentProbability = minProbability;
            maxProbability = (Single)ResearchParameterValues[ResearchParameter.ProbabilityMax];
            delta = (Single)ResearchParameterValues[ResearchParameter.ProbabilityDelta];

            StartCurrentEnsemble();
        }

        public override void StopResearch()
        {
            isCanceled = true;
            currentManager.Cancel();
        }

        public override ResearchType GetResearchType()
        {
            return ResearchType.Percolation;
        }

        private void RunCompleted(IAsyncResult res)
        {
            if (isCanceled)
            {
                // validating result
                ResearchParameterValues[ResearchParameter.ProbabilityMax] = currentProbability;
            }
            else
            {
                result.EnsembleResults.Add(currentManager.Result);
            }

            currentProbability += delta;
            StartCurrentEnsemble();
        }

        private void StartCurrentEnsemble()
        {
            if (currentProbability < maxProbability && !isCanceled)
            {
                base.CreateEnsembleManager();
                ManagerRunner r = new ManagerRunner(currentManager.Run);
                r.BeginInvoke(new AsyncCallback(RunCompleted), null);
            }
            else
            {
                base.SaveResearch();
            }
        }

        protected override void FillGenerationParameters(AbstractEnsembleManager m)
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
