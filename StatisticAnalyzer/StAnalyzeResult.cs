using System;
using System.Collections.Generic;

using RandomGraph.Common.Model;

namespace StatisticAnalyzer
{
    public struct StAnalyzeResult
    {
        public string modelName;
        public string parameterLine;
        public int networkSize;
        public ApproximationTypes approximationType;
        public SortedDictionary<AnalyseOptions, SortedDictionary<double, double>> result;
        public SortedDictionary<AnalyseOptions, double> resultAvgValues;
        public SortedDictionary<AnalyseOptions, double> resultMathWaitings;
        public SortedDictionary<AnalyseOptions, double> resultDispersions;
    }
}