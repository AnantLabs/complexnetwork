using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RandomGraph.Common.Model;

namespace StatisticAnalyzer
{
    /// <summary>
    /// Struct for supported statistic analyze parameters that
    /// should be use any analyze process.
    /// <summary>
    public struct StatAnalyzeParameters
    {
        public string m_param1;
        public string m_param2;
        public string m_param3;

        // Generation process parameters //
        // Hierarchic model //
        public Int16 m_hierarchicBase;
        public Int16 m_blockDegree;
        public Double m_lambdaParam;

        // BA model //
        public Int32 m_initialCount;
        public Int16 m_maximalConnections;

        // WS model //
        public Int32 m_numberOfVerticesWS;
        public Int32 m_numberOfEdges;
        public Double m_probabilityWS;

        // ER model //
        public Int32 m_numberOfVerticesER;
        public Double m_probabilityER;

        // Analyze process parameters //
        public bool m_byAllAssemblies;
        public Dictionary<AnalyseOptions, StAnalyzeOptions> m_localAnalyzeOptions;
    }

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