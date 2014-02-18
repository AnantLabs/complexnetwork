using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;
using Core.Attributes;
using Core.Enumerations;

namespace Research
{
    [AvailableModelType(ModelType.ER)]
    [AvailableModelType(ModelType.RegularHierarchic)]
    [RequiredResearchParameter(ResearchParameter.ProbabilityMax)]
    [AvailableAnalyzeOption(AnalyzeOption.ConnectedComponentDistribution)]
    public class PercolationResearch : AbstractResearch
    {
        public override void StartResearch()
        {
            throw new NotImplementedException();
        }

        public override void StopResearch()
        {
            throw new NotImplementedException();
        }
    }
}
