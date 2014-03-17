using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;
using Core.Enumerations;
using Core.Attributes;

namespace NonRegularHierarchicModel
{
    [RequiredGenerationParameter(GenerationParameter.BranchingIndex)]
    [RequiredGenerationParameter(GenerationParameter.Level)]
    [RequiredGenerationParameter(GenerationParameter.Mu)]
    [AvailableAnalyzeOption(AnalyzeOption.AvgClusteringCoefficient |
        AnalyzeOption.AvgDegree |
        AnalyzeOption.AvgPathLength |
        AnalyzeOption.ClusteringCoefficientDistribution |
        AnalyzeOption.Cycles3 |
        AnalyzeOption.Cycles4 |
        AnalyzeOption.DegreeDistribution |
        AnalyzeOption.Diameter)]
    public class NonRegularHierarchicNetwork : AbstractNetwork
    {
    }
}
