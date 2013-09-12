using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Result;

namespace CommonLibrary.Model.Result
{
    public class ResultAssembly
    {
        public ResultAssembly()
        {
            ID = Guid.NewGuid();
            Results = new List<AnalizeResult>();
            GenerationParams = new Dictionary<GenerationParam, object>();

            AnalyzeOptionParams = new Dictionary<AnalyzeOptionParam, object>();
            AnalyzeOptionParams.Add(AnalyzeOptionParam.CyclesLow, (Int16)0);
            AnalyzeOptionParams.Add(AnalyzeOptionParam.CyclesHigh, (Int16)0);
            AnalyzeOptionParams.Add(AnalyzeOptionParam.MotifsLow, (Int16)0);
            AnalyzeOptionParams.Add(AnalyzeOptionParam.MotifsHigh, (Int16)0);
            AnalyzeOptionParams.Add(AnalyzeOptionParam.TrajectoryMu, (Double)0);
            AnalyzeOptionParams.Add(AnalyzeOptionParam.TrajectoryStepCount, (BigInteger)0);

            CoefficientsLocal = new SortedDictionary<double, double>();
            VertexDegreeLocal = new SortedDictionary<double, double>();
            SubgraphsLocal = new SortedDictionary<double, double>();
            DistanceBetweenVerticesLocal = new SortedDictionary<double, double>();
            DistancesBetweenEigenValuesLocal = new SortedDictionary<double, double>();
            TriangleTrajectoryLocal = new SortedDictionary<double, double>();
        }

        public Guid ID { get; set; }
        public string Name { get; set; }
        public List<AnalizeResult> Results { get; set; }
        public Type ModelType { get; set; }
        public string ModelName { get; set; }
        public Dictionary<GenerationParam, object> GenerationParams { get; set; }
        public AnalyseOptions AnalizeOptions { get; set; }
        public Dictionary<AnalyzeOptionParam, object> AnalyzeOptionParams;

        // !исправить!
        // Часть для БД (результаты хранимых процедур)
        public SortedDictionary<double, double> CoefficientsLocal { get; set; }
        public SortedDictionary<double, double> VertexDegreeLocal { get; set; }
        public SortedDictionary<double, double> SubgraphsLocal { get; set; }
        public SortedDictionary<double, double> DistanceBetweenVerticesLocal { get; set; }
        public SortedDictionary<double, double> DistancesBetweenEigenValuesLocal { get; set; }
        public SortedDictionary<double, double> TriangleTrajectoryLocal { get; set; }
    }
}
