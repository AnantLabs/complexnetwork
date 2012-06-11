using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonLibrary.Model.Result;
using RandomGraph.Common.Storage;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.StatAnalyzer;

namespace StatisticAnalyzer.Methods
{
    public class MethodForBA : AbstractMethod
    {
        public MethodForBA() { }

        protected override bool IsCorrectAssembly(ResultAssembly assembly, AnalyseOptions option)
        {
            return (ContainsOption(assembly, option) &&
                (Int32)assembly.GenerationParams[GenerationParam.Vertices] == m_parameters.m_initialCount &&
                (Int16)assembly.GenerationParams[GenerationParam.MaxEdges] == m_parameters.m_maximalConnections);
        }

        public override void SetSize()
        {
            m_size = m_parameters.m_initialCount;
        }

        override public Dictionary<GenerationParam, string> GetParameterLine()
        {
            Dictionary<GenerationParam, string> parameters = new Dictionary<GenerationParam, string>();
            parameters.Add(GenerationParam.Vertices, m_parameters.m_initialCount.ToString());
            parameters.Add(GenerationParam.MaxEdges, m_parameters.m_maximalConnections.ToString());
            return parameters;
        }
    }
}