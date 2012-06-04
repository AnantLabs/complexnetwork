using System;
using System.Collections.Generic;

using CommonLibrary.Model.Attributes;
using RandomGraph.Common.Model;

namespace StatisticAnalyzer
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
}