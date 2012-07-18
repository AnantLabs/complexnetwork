using System;
using System.Collections.Generic;

using RandomGraph.Common.Model;

using StatisticAnalyzer.Viewer;

namespace StatisticAnalyzer.Analyzer
{
    public class StAnalyzeResult
    {
        public string modelName;
        public string parameterLine;
        public int networkSize;
        public Viewer.ApproximationTypes approximationType;
        public StAnalyzeOptions options;
        public SortedDictionary<AnalyseOptions, SortedDictionary<double, double>> result;
        public SortedDictionary<AnalyseOptions, double> resultAvgValues;
        public SortedDictionary<AnalyseOptions, double> resultMathWaitings;
        public SortedDictionary<AnalyseOptions, double> resultDispersions;

        public StAnalyzeResult()
        {
            modelName = "";
            parameterLine = "";
            networkSize = 0;
            approximationType = ApproximationTypes.None;
            options = new StAnalyzeOptions();

            result = new SortedDictionary<AnalyseOptions, SortedDictionary<double, double>>();
            resultAvgValues = new SortedDictionary<AnalyseOptions, double>();
            resultMathWaitings = new SortedDictionary<AnalyseOptions, double>();
            resultDispersions = new SortedDictionary<AnalyseOptions, double>();
        }
    }
}