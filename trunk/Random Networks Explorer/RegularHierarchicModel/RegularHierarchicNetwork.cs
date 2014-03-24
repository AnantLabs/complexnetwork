using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;
using Core.Attributes;
using Core.Enumerations;

namespace RegularHierarchicModel
{
    [RequiredGenerationParameter(GenerationParameter.BranchingIndex)]
    [RequiredGenerationParameter(GenerationParameter.Level)]
    [RequiredGenerationParameter(GenerationParameter.Mu)]
    [AvailableAnalyzeOption(AnalyzeOption.AvgClusteringCoefficient |
        AnalyzeOption.AvgDegree |
        AnalyzeOption.AvgPathLength |
        AnalyzeOption.ClusteringCoefficientDistribution |
        AnalyzeOption.ConnectedComponentDistribution |
        AnalyzeOption.CycleDistribution |
        AnalyzeOption.Cycles3 |
        AnalyzeOption.Cycles4 |
        AnalyzeOption.DegreeDistribution |
        AnalyzeOption.Diameter |
        AnalyzeOption.DistanceDistribution |
        AnalyzeOption.EigenDistanceDistribution |
        AnalyzeOption.EigenValues |
        AnalyzeOption.TriangleByVertexDistribution)]
    public class RegularHierarchicNetwork : AbstractNetwork
    {
        public RegularHierarchicNetwork(Dictionary<GenerationParameter, object> genParams,
            AnalyzeOption analyzeOpts, string trPath) :
            base(genParams, analyzeOpts, trPath)
        {
            networkGenerator = new RegularHierarchicNetworkGenerator();
            networkAnalyzer = new RegularHierarchicNetworkAnalyzer();
        }

        public override void Generate()
        {
            throw new NotImplementedException();
        }

        public override void Analyze()
        {
            throw new NotImplementedException();
        }   
    }
}
