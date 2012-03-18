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
    class MethodForER : AbstractMethod
    {
        public MethodForER() { }

        override protected bool IsCorrectAssembly(ResultAssembly assembly, AnalyseOptions option)
        {
            return ContainsOption(assembly, option) &&
                (Int32)assembly.GenerationParams[GenerationParam.Vertices] == m_parameters.m_numberOfVerticesER &&
                (Double)assembly.GenerationParams[GenerationParam.P] == m_parameters.m_probabilityER;
        }

        public override void SetSize()
        {
            m_size = m_parameters.m_numberOfVerticesER;
        }

        override public Dictionary<GenerationParam, string> GetParameterLine()
        {
            Dictionary<GenerationParam, string> parameters = new Dictionary<GenerationParam, string>();
            parameters.Add(GenerationParam.Vertices, m_parameters.m_numberOfVerticesER.ToString());
            parameters.Add(GenerationParam.P, m_parameters.m_probabilityER.ToString());
            return parameters;
        }
    }
}
