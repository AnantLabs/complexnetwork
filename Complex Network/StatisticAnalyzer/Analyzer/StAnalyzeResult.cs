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
        public SortedDictionary<AnalyseOptions, StAnalyzeOptions> options;
        public SortedDictionary<AnalyseOptions, SortedDictionary<double, double>> result;
        public SortedDictionary<AnalyseOptions, double> resultAvgValues;
        public SortedDictionary<AnalyseOptions, double> resultMathWaitings;
        public SortedDictionary<AnalyseOptions, double> resultDispersions;

        public StAnalyzeResult()
        {
            type = StAnalyzeType.Global;
            modelName = "";
            parameterLine = "";
            networkSize = 0;
            realizationsCount = 0;
            approximationType = ApproximationTypes.None;
            options = new SortedDictionary<AnalyseOptions,StAnalyzeOptions>();

            result = new SortedDictionary<AnalyseOptions, SortedDictionary<double, double>>();
            resultAvgValues = new SortedDictionary<AnalyseOptions, double>();
            resultMathWaitings = new SortedDictionary<AnalyseOptions, double>();
            resultDispersions = new SortedDictionary<AnalyseOptions, double>();
        }
    }
}