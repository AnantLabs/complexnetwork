using System;
using System.Collections.Generic;
using CommonLibrary.Model.Attributes;

namespace RandomGraph.Common.Model.StatAnalyzer
{
    public enum StatAnalyzeMode
    {
        GlobalMode,
        LocalMode,
        MotifMode
    }

    public struct GraphicalInformation
    {
        public GraphicalInformation(AnalyseOptions o, StatAnalyzeMode m)
        {
            m_option = o;
            m_mode = m;
        }

        public AnalyseOptions m_option;
        public StatAnalyzeMode m_mode;
    }

    public struct StatAnalyzeOptions
    {
        public bool m_useDelta;
        public double m_optionValue;

        public StatAnalyzeOptions(bool useDelta, double optionValue)
        {
            m_useDelta = useDelta;
            m_optionValue = optionValue;
        }
    }

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
        public Dictionary<AnalyseOptions, StatAnalyzeOptions> m_localAnalyzeOptions;
    }

    /// <summary>
    /// Enumeration of supported approximation types that
    /// could be use in analyze process.
    /// <summary>
    public enum ApproximationTypes
    {
        [ApproximationTypeInfo(ID = 1, Name = "None Approximation", XAxis = "X", YAxis = "Y")]
        None = 0x0,

        [ApproximationTypeInfo(ID = 2, Name = "Degree Approximation", XAxis = "lnX", YAxis = "lnY")]
        Degree = 0x01,

        [ApproximationTypeInfo(ID = 3, Name = "Exponential Approximation", XAxis = "X", YAxis = "lnY")]
        Exponential = 0x02,

        [ApproximationTypeInfo(ID = 4, Name = "Gaus Approximation", XAxis = "X ^ 2", YAxis = "lnY")]
        Gaus = 0x04
    }
}