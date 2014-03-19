using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;
using Core.Attributes;
using Core.Enumerations;

namespace WSModel
{
    [RequiredGenerationParameter(GenerationParameter.Vertices)]
    [RequiredGenerationParameter(GenerationParameter.Edges)]
    [RequiredGenerationParameter(GenerationParameter.Probability)]
    [RequiredGenerationParameter(GenerationParameter.StepCount)]
    [AvailableAnalyzeOption(AnalyzeOption.AvgClusteringCoefficient |
        AnalyzeOption.AvgDegree |
        AnalyzeOption.AvgPathLength |
        AnalyzeOption.ClusteringCoefficientDistribution |
        AnalyzeOption.CompleteComponentDistribution |
        AnalyzeOption.ConnectedComponentDistribution |
        AnalyzeOption.CycleDistribution | 
        AnalyzeOption.Cycles3 |
        AnalyzeOption.Cycles4 |
        AnalyzeOption.DegreeDistribution |
        AnalyzeOption.Diameter |
        AnalyzeOption.DistanceDistribution |
        AnalyzeOption.EigenDistanceDistribution |
        AnalyzeOption.EigenValues)]
    public class WSNetwork : AbstractNetwork
    {
    }
}
