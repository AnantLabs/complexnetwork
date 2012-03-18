using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandomGraph.Common.Model;

using CommonLibrary.Model.Result;

namespace RandomGraph.Common.Model.StatAnalyzer
{
    /// <summary>
    /// Defines bean class for storage final statistical results.
    /// </summary>
    public class StatisticResult
    {
        public StatisticResult()
        {
            Results = new Dictionary<AnalyseOptions, object>();
            ChartResult = new Dictionary<AnalyseOptions, List<double>>();
        }
        public Dictionary<AnalyseOptions, object> Results
        {
            get;
            set;
        }
        public Dictionary<AnalyseOptions, List<double>> ChartResult
        {
            get;
            set;
        }

    }
}
