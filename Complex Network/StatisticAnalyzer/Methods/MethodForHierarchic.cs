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
    public class MethodForHierarchic : AbstractMethod
    {
        public MethodForHierarchic() { }

        override protected bool IsCorrectAssembly(ResultAssembly assembly, AnalyseOptions option)
        {
            return ContainsOption(assembly, option) &&
                (Int16)assembly.GenerationParams[GenerationParam.BranchIndex] == m_parameters.m_hierarchicBase &&
                (Int16)assembly.GenerationParams[GenerationParam.Level] == m_parameters.m_blockDegree &&
                (Double)assembly.GenerationParams[GenerationParam.Mu] == m_parameters.m_lambdaParam;
        }

        public override void SetSize()
        {
            m_size = (int)Math.Pow(m_parameters.m_hierarchicBase, m_parameters.m_blockDegree);
        }

        // Global Analyze //

        override public KeyValuePair<SortedDictionary<double, double>, double> GlobalAnalyzeByOption(AnalyseOptions option)
        {
            KeyValuePair<SortedDictionary<double, double>, double> resultPair;

            SortedDictionary<double, double> resultDictionary = new SortedDictionary<double, double>();
            int instanceCount = 0, previousInstanceCount = 0, deltaI = 10, I = deltaI;
            foreach (string resultName in m_assemblesID)
            {
                ResultAssembly result = m_storage.Load(m_resultAssemblies.Find(i => i.Name == resultName).ID);
                if (IsCorrectAssembly(result, option))
                {
                    previousInstanceCount = instanceCount;
                    instanceCount += result.Results.Count;
                    resultDictionary = GlobalCases(option, result, I, deltaI, instanceCount, previousInstanceCount);

                    if (!m_parameters.m_byAllAssemblies)
                        break;
                }
            }

            resultPair = new KeyValuePair<SortedDictionary<double, double>, double>(resultDictionary,
                GetGlobalAverage(instanceCount, resultDictionary));

            return resultPair;
        }

        override public KeyValuePair<SortedDictionary<double, double>, double> GlobalAnalyzeByOption(string jobName, AnalyseOptions option)
        {
            KeyValuePair<SortedDictionary<double, double>, double> resultPair;

            SortedDictionary<double, double> resultDictionary = new SortedDictionary<double, double>();
            ResultAssembly result = m_storage.Load(m_resultAssemblies.Find(i => i.Name == jobName).ID);
            if (ContainsOption(result, option))
            {
                int deltaI = result.Results.Count / 10, I = deltaI;
                resultDictionary = GlobalCases(option, result, I, deltaI, result.Results.Count, -1);
            }

            resultPair = new KeyValuePair<SortedDictionary<double, double>, double>(resultDictionary,
                GetGlobalAverage(result.Results.Count, resultDictionary));

            return resultPair;
        }

        // Local Analyze //

        override public SortedDictionary<double, double> LocalAnalyzeByOption(AnalyseOptions option)
        {
            SortedDictionary<double, double> resultDictionary = new SortedDictionary<double, double>();
            SortedDictionary<double, double> tempResult = new SortedDictionary<double, double>();
            int initialInstance = 0, instanceCount = 0;
            foreach (string resultName in m_assemblesID)
            {
                ResultAssembly result = m_storage.Load(m_resultAssemblies.Find(i => i.Name == resultName).ID);
                if (IsCorrectAssembly(result, option))
                {
                    initialInstance = instanceCount;
                    instanceCount += result.Results.Count;
                    for (int i = initialInstance; i < instanceCount; ++i)
                    {
                        tempResult = LocalCases(option, result, i, initialInstance);
                    }

                    if (!m_parameters.m_byAllAssemblies)
                        break;
                }
            }

            return GetLocalResult(option, tempResult, instanceCount);
        }

        override public SortedDictionary<double, double> LocalAnalyzeByOption(string jobName, AnalyseOptions option)
        {
            SortedDictionary<double, double> resultDictionary = new SortedDictionary<double, double>();
            SortedDictionary<double, double> tempResult = new SortedDictionary<double, double>();
            ResultAssembly result = m_storage.Load(m_resultAssemblies.Find(i => i.Name == jobName).ID);

            int instanceCount = 1;
            if (ContainsOption(result, option))
            {
                instanceCount = result.Results.Count;

                for (int i = 0; i < instanceCount; ++i)
                {
                    tempResult = LocalCases(option, result, i, 0);
                }
            }

            return GetLocalResult(option, tempResult, instanceCount);
        }

        override public Dictionary<GenerationParam, string> GetParameterLine()
        {
            Dictionary<GenerationParam, string> parameters = new Dictionary<GenerationParam, string>();
            parameters.Add(GenerationParam.BranchIndex, m_parameters.m_hierarchicBase.ToString());
            parameters.Add(GenerationParam.Level, m_parameters.m_blockDegree.ToString());
            parameters.Add(GenerationParam.Mu, m_parameters.m_lambdaParam.ToString());
            return parameters;
        }
    }
}