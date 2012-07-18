using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RandomGraph.Common.Model;

namespace StatisticAnalyzer.Analyzer
{
    public struct StAnalyzeOptions
    {
        public bool useDelta;
        public double optionValue;

        public StAnalyzeOptions(bool delta, double value)
        {
            useDelta = delta;
            optionValue = value;
        }
    }
}