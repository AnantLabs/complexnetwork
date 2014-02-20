using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        private Single minProbability;
        private Single currentProbability;
        private Single maxProbability;
        private Single delta;

        /// <summary>
        /// 
        /// </summary>
        public override void StartResearch()
        {
            minProbability = Convert.ToSingle(base.GenerationParameterValues[GenerationParameter.Probability]);
            currentProbability = minProbability;
            maxProbability = Convert.ToSingle(base.ResearchParameterValues[ResearchParameter.ProbabilityMax]);
            delta = Convert.ToSingle(base.ResearchParameterValues[ResearchParameter.ProbabilityDelta]);

            StartCurrentEnsemble();
        }

        public override void StopResearch()
        {
            throw new NotImplementedException();
        }

        private void StartCurrentEnsemble()
        {
            if (currentProbability < maxProbability)
            {
                AbstractEnsembleManager manager = base.CreateEnsembleManager();
                ManagerRunner r = new ManagerRunner(manager.Run);
                r.BeginInvoke(null, null);

                currentProbability += delta;
            }
            else
            {
            }
        }

        protected override void InitializeGenerationParameters(AbstractEnsembleManager m)
        {
            Dictionary<GenerationParameter, object> g = new Dictionary<GenerationParameter, object>();
            foreach (GenerationParameter p in base.GenerationParameterValues.Keys)
            {
                if (p == GenerationParameter.Probability)
                    g.Add(p, currentProbability);
                else
                    g.Add(p, base.GenerationParameterValues[p]);
            }

            m.GenerationParameterValues = g;
        }
    }
}
