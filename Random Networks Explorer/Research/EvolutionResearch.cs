﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;
using Core.Enumerations;
using Core.Attributes;

namespace Research
{
    [AvailableModelType(ModelType.ER)]
    [RequiredResearchParameter(ResearchParameter.StepCount)]
    [RequiredResearchParameter(ResearchParameter.Nu)]
    [RequiredResearchParameter(ResearchParameter.PermanentDistribution)]
    [AvailableAnalyzeOption(AnalyzeOption.Cycles3)]
    public class EvolutionResearch : AbstractResearch
    {
        public override void StartResearch()
        {
            throw new NotImplementedException();
        }

        public override ResearchType GetResearchType()
        {
            return ResearchType.Evolution;
        }

        protected override void InitializeGenerationParameters(AbstractEnsembleManager m)
        {
            throw new NotImplementedException();
        }
    }
}
