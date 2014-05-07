using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;
using Core.Attributes;
using Core.Enumerations;
using NetworkModel;

namespace BAModel
{
    /// <summary>
    /// Implementation of random network of Baraba´si-Albert's model.
    /// </summary>
    [RequiredGenerationParameter(GenerationParameter.AdjacencyMatrixFile)]
    [RequiredGenerationParameter(GenerationParameter.Vertices)]
    [RequiredGenerationParameter(GenerationParameter.Edges)]
    [RequiredGenerationParameter(GenerationParameter.Probability)]
    [RequiredGenerationParameter(GenerationParameter.StepCount)]
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
    public class BANetwork : AbstractNetwork
    {
        public BANetwork(Dictionary<GenerationParameter, object> genParams,
            AnalyzeOption analyzeOpts) : base(genParams, analyzeOpts)
        {
            networkGenerator = new BANetworkGenerator();
            networkAnalyzer = new NonHierarchicAnalyzer();
        }

        public static UInt32 CalculateSize(Dictionary<GenerationParameter, object> p)
        {
            if (p.ContainsKey(GenerationParameter.Vertices) &&
                p.ContainsKey(GenerationParameter.StepCount) &&
                p[GenerationParameter.Vertices] != null &&
                p[GenerationParameter.StepCount] != null)
            {
                // TODO change without parse
                return UInt32.Parse(p[GenerationParameter.Vertices].ToString()) +
                    UInt32.Parse(p[GenerationParameter.StepCount].ToString());
            }
            else
            {
                throw new SystemException("Wrong generation parameters for current model.");
            }
        }
    }
}
