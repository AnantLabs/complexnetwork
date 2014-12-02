using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;
using Core.Attributes;
using Core.Enumerations;

namespace Research
{
    /// <summary>
    /// Basic research implementation.
    /// </summary>
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
        AnalyzeOption.TriangleByVertexDistribution )]    
    public class BasicResearch : AbstractResearch
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
            return ResearchType.Basic;
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
            m.GenerationParameterValues = GenerationParameterValues;
        }        
    }
}
