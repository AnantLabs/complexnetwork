using System;
using System.Collections.Generic;

using RandomGraph.Common.Model;

using StatisticAnalyzer.Viewer;

namespace StatisticAnalyzer.Analyzer
{
    public enum StAnalyzeType
    {
        Global,
        Local,
        Motif
    }

    public class StAnalyzeResult
    {
        public StAnalyzeType type;
        public string modelName;
        public string parameterLine;
        public int networkSize;
        public int realizationsCount;
        public Viewer.ApproximationTypes approximationType;
        public Dictionary<AnalyseOptions, StAnalyzeOptions> options;
        public Dictionary<AnalyseOptions, SortedDictionary<double, double>> result;
        public Dictionary<AnalyseOptions, SortedDictionary<double, double>> resultValues;
        public Dictionary<AnalyseOptions, double> resultAvgValues;
        public Dictionary<AnalyseOptions, double> resultMathWaitings;
        public Dictionary<AnalyseOptions, double> resultDispersions;
        public SortedDictionary<double, double> trajectoryAvgs;
        public SortedDictionary<double, double> trajectorySigmas;

        public StAnalyzeResult()
        {
            type = StAnalyzeType.Global;
            modelName = "";
            parameterLine = "";
            networkSize = 0;
            realizationsCount = 0;
            approximationType = ApproximationTypes.None;
            options = new Dictionary<AnalyseOptions, StAnalyzeOptions>();

            result = new Dictionary<AnalyseOptions, SortedDictionary<double, double>>();
            resultValues = new Dictionary<AnalyseOptions, SortedDictionary<double, double>>();
            resultAvgValues = new Dictionary<AnalyseOptions, double>();
            resultMathWaitings = new Dictionary<AnalyseOptions, double>();
            resultDispersions = new Dictionary<AnalyseOptions, double>();
            trajectoryAvgs = new SortedDictionary<double, double>();
            trajectorySigmas = new SortedDictionary<double, double>();
        }
    }
}