using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;
using Core.Enumerations;
using Core.Attributes;

namespace Research
{
    /// <summary>
    /// Evolution research implementation.
    /// </summary>
    [AvailableModelType(ModelType.ER)]
    [RequiredResearchParameter(ResearchParameter.EvolutionStepCount)]
    [RequiredResearchParameter(ResearchParameter.Nu)]
    [RequiredResearchParameter(ResearchParameter.PermanentDistribution)]
    [AvailableAnalyzeOption(AnalyzeOption.Cycles3Trajectory)]
    public class EvolutionResearch : AbstractResearch
    {
        /// <summary>
        /// Creates a single EnsembleManager, runs in background thread.
        /// </summary>
        public override void StartResearch()
        {
            CreateEnsembleManager();
            Status = ResearchStatus.Running;
            ManagerRunner r = new ManagerRunner(currentManager.Run);
            r.BeginInvoke(new AsyncCallback(RunCompleted), null);
        }

        public override void StopResearch()
        {
            currentManager.Cancel();
            Status = ResearchStatus.Stopped;
        }

        public override ResearchType GetResearchType()
        {
            return ResearchType.Evolution;
        }

        private void RunCompleted(IAsyncResult res)
        {
            realizationCount = currentManager.RealizationsDone;
            result.EnsembleResults.Add(currentManager.Result);
            SaveResearch();
            Status = ResearchStatus.Succeed;
        }

        protected override void FillParameters(AbstractEnsembleManager m)
        {
            m.ResearchParamaterValues = ResearchParameterValues;
            m.GenerationParameterValues = GenerationParameterValues;
        }
    }
}
