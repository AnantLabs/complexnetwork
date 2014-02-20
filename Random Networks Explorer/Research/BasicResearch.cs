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
    [AvailableModelType(ModelType.BA)]
    [AvailableModelType(ModelType.WS)]
    [AvailableModelType(ModelType.RegularHierarchic)]
    [AvailableModelType(ModelType.NonRegularHierarchic)]
    [AvailableAnalyzeOption(
        AnalyzeOption.AvgClusteringCoefficient |
        AnalyzeOption.AvgDegree |
        AnalyzeOption.AvgPathLength |
        AnalyzeOption.ClusteringCoefficientDistribution |
        AnalyzeOption.CompleteComponentDistribution |
        AnalyzeOption.ConnectedComponentDistribution |
        AnalyzeOption.CycleDistribution |
        AnalyzeOption.Cycles3 |
        AnalyzeOption.Cycles3Eigen |
        AnalyzeOption.Cycles4 |
        AnalyzeOption.Cycles4Eigen |
        AnalyzeOption.DegreeDistribution |
        AnalyzeOption.Diameter |
        AnalyzeOption.DistanceDistribution |
        AnalyzeOption.EigenDistanceDistribution |
        AnalyzeOption.EigenValues |
        AnalyzeOption.TriangleByVertexDistribution |
        AnalyzeOption.TriangleDistribution )]    
    public class BasicResearch : AbstractResearch
    {
        public override void StartResearch()
        {
 	        throw new NotImplementedException();
        }

        public override void StopResearch()
        {
            throw new NotImplementedException();
        }

        protected override void InitializeGenerationParameters(AbstractEnsembleManager m)
        {
            throw new NotImplementedException();
        }
    }
}
