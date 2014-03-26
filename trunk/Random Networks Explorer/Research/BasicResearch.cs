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
            base.CreateEnsembleManager();
            ManagerRunner r = new ManagerRunner(currentManager.Run);
            r.BeginInvoke(new AsyncCallback(RunCompleted), null);
        }

        public override ResearchType GetResearchType()
        {
            return ResearchType.Basic;
        }

        protected override void InitializeGenerationParameters(AbstractEnsembleManager m)
        {
            m.GenerationParameterValues = base.GenerationParameterValues;
        }        

        private void RunCompleted(IAsyncResult res)
        {
            // TODO getting result from currentManager and add to base.result
            SaveResearch();
        }
    }
}
