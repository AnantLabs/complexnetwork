using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;
using Core.Enumerations;
using Core.Attributes;

namespace NonRegularHierarchicModel
{
    /// <summary>
    /// Implementation of non regularly branching block-hierarchic network.
    /// </summary>
    [RequiredGenerationParameter(GenerationParameter.AdjacencyMatrixFile)]
    [RequiredGenerationParameter(GenerationParameter.Vertices)]
    [RequiredGenerationParameter(GenerationParameter.BranchingIndex)]
    [RequiredGenerationParameter(GenerationParameter.Mu)]
    [AvailableAnalyzeOption(AnalyzeOption.AvgClusteringCoefficient |
        AnalyzeOption.AvgDegree |
        AnalyzeOption.AvgPathLength |
        AnalyzeOption.ClusteringCoefficientDistribution |
        AnalyzeOption.ConnectedComponentDistribution |
        AnalyzeOption.Cycles3 |
        AnalyzeOption.Cycles4 |
        AnalyzeOption.DegreeDistribution |
        AnalyzeOption.Diameter | 
        AnalyzeOption.DistanceDistribution | 
        AnalyzeOption.EigenDistanceDistribution | 
        AnalyzeOption.EigenValues |
        AnalyzeOption.TriangleByVertexDistribution)]
    public class NonRegularHierarchicNetwork : AbstractNetwork
    {
        public NonRegularHierarchicNetwork(Dictionary<ResearchParameter, object> rParams,
            Dictionary<GenerationParameter, object> genParams,
            AnalyzeOption analyzeOpts) : base(rParams, genParams, analyzeOpts)
        {
            networkGenerator = new NonRegularHierarchicNetworkGenerator();
            networkAnalyzer = new NonRegularHierarchicNetworkAnalyzer(this);
        }

        public static UInt32 CalculateSize(Dictionary<GenerationParameter, object> p)
        {
            if (p.ContainsKey(GenerationParameter.Vertices) &&
                p[GenerationParameter.Vertices] != null)
            {
                // TODO change without parse
                return UInt32.Parse(p[GenerationParameter.Vertices].ToString());
            }
            else
            {
                throw new SystemException("Wrong generation parameters for current model.");
            }
        }
    }
}
