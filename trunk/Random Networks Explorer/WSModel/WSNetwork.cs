using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;
using Core.Attributes;
using Core.Enumerations;
using NetworkModel;

namespace WSModel
{
    [RequiredGenerationParameter(GenerationParameter.AdjacencyMatrixFile)]
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
        public WSNetwork(Dictionary<GenerationParameter, object> genParams,
            AnalyzeOption analyzeOpts) : base(genParams, analyzeOpts)
        {
            networkGenerator = new WSNetworkGenerator();
            networkAnalyzer = new NonHierarchicAnalyzer();
        }

        public static UInt32 CalculateSize(Dictionary<GenerationParameter, object> p)
        {
            if (p.ContainsKey(GenerationParameter.Vertices) &&
                p[GenerationParameter.Vertices] != null)
            {
                return Convert.ToUInt32(p[GenerationParameter.Vertices]);
            }
            else
            {
                throw new SystemException("Wrong generation parameters for current model.");
            }
        }
    }
}
