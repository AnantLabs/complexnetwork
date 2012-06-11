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
    public class SequentialMethodForBA : AbstractMethod
    {
        private Int32 m_initialCount;
        private Int16 m_maxConnections;

        public SequentialMethodForBA() { }

        override public void SetParameters(List<object> parameters)
        {
            m_initialCount = Convert.ToInt32(parameters[0]);
            m_maxConnections = Convert.ToInt16(parameters[1]);
        }

        override protected bool ContainsOption(ResultAssembly assembly, AnalyseOptions option)
        {
            return assembly.Results[0].Result.Keys.Contains(option);
        }

        protected override bool IsCorrectAssembly(ResultAssembly assembly, AnalyseOptions option)
        {
            return (ContainsOption(assembly, option) &&
                (Int32)assembly.GenerationParams[GenerationParam.Verticles] == m_initialCount &&
                (Int16)assembly.GenerationParams[GenerationParam.MaxConnections] == m_maxConnections);
        }

        // Global Analyze //

        override public KeyValuePair<SortedDictionary<double, double>, double> GlobalAnalyzeByOption(AnalyseOptions option)
        {
            KeyValuePair<SortedDictionary<double, double>, double> resultPair;

            SortedDictionary<double, double> resultDictionary = new SortedDictionary<double, double>();
            int instanceCount = 0, deltaI = 10, I = deltaI;
            foreach (string resultName in m_assemblesID)
            {
                ResultAssembly result = m_storage.Load(m_resultAssemblies.Find(i => i.Name == resultName).ID);
                if (IsCorrectAssembly(result, option))
                {
                    instanceCount = result.Results.Count;
                    resultDictionary = FillGlobalResult(option, result, I, deltaI, result.Results.Count);
                }
            }

            resultPair = new KeyValuePair<SortedDictionary<double, double>, double>(resultDictionary,
                GetGlobalAverage(instanceCount, resultDictionary));

            return resultPair;
        }

        override public KeyValuePair<SortedDictionary<double, double>, double> GlobalAnalyzeByOption(string jobName, AnalyseOptions option)
        {
            KeyValuePair<SortedDictionary<double, double>, double> resultPair = new KeyValuePair<SortedDictionary<double, double>, double>();

            SortedDictionary<double, double> resultDictionary = new SortedDictionary<double, double>();
            ResultAssembly result = m_storage.Load(m_resultAssemblies.Find(i => i.Name == jobName).ID);
            int instanceCount = 0, deltaI = 10, I = deltaI;
            if (ContainsOption(result, option))
            {
                instanceCount = result.Results.Count;
                resultDictionary = FillGlobalResult(option, result, I, deltaI, result.Results.Count);
            }

            resultPair = new KeyValuePair<SortedDictionary<double, double>, double>(resultDictionary,
                GetGlobalAverage(instanceCount, resultDictionary));

            return resultPair;
        }

        // Local Analyze //

        override public SortedDictionary<double, double> LocalAnalyzeByOption(AnalyseOptions option)
        {
            SortedDictionary<double, double> resultDictionary = new SortedDictionary<double, double>();
            return resultDictionary;
            /*SortedDictionary<double, double> tempResult = new SortedDictionary<double, double>();
            int instanceCount = 0;
            foreach (string resultName in m_assemblesID)
            {
                ResultAssembly result = m_storage.Load(m_resultAssemblies.Find(i => i.Name == resultName).ID);
                if (IsCorrectAssembly(result, option))
                {
                    instanceCount = result.Results.Count;
                    for (int i = 0; i < instanceCount; ++i)
                    {
                        switch (option)
                        {
                            case AnalyseOptions.ClusteringCoefficient:
                                {
                                    tempResult = FillLocalResultForDD(result, i, 0);
                                    break;
                                }
                            default:
                                {
                                    // add
                                    break;
                                }
                        }
                    }
                }
            }

            SortedDictionary<double, double>.KeyCollection keys = tempResult.Keys;
            return GetLocalResult(option, keys, tempResult, instanceCount);*/
        }

        override public SortedDictionary<double, double> LocalAnalyzeByOption(string jobName, AnalyseOptions option)
        {
            SortedDictionary<double, double> resultDictionary = new SortedDictionary<double, double>();
            return resultDictionary;
        }

        override public string GetParameterLine()
        {
            return "Initial Count = " + m_initialCount.ToString() + "; Maximal Connections = " + m_maxConnections.ToString() + ";";
        }

        override public double CountM(StatAnalyzeLocalParameters p)
        {
            double m = 0;
            return m;
        }

        override public double CountD(StatAnalyzeLocalParameters p)
        {
            double d = 0;
            return d;
        }

        // Utilities //

        private SortedDictionary<double, double> FillGlobalResult(AnalyseOptions option, ResultAssembly result, int I, int deltaI,
            int instanceCount)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            if (instanceCount <= 10 && !m_parameters.m_byAllAssemblies)
            {
                for (int i = 0; i < instanceCount; ++i)
                {
                    r.Add(i + 1, result.Results[i].Result[option]);
                }
            }
            else
            {
                double sum = 0;
                while (I <= instanceCount)
                {
                    sum = 0;
                    for (int i = 0; i < I; ++i)
                        sum += result.Results[i].Result[option];
                    r.Add(I, sum / I);
                    /*if (r.Count > 1)
                    {
                        if (Math.Abs(r[I] - r[I - deltaI]) < 1)
                            deltaI /= 2;
                    }*/

                    I += deltaI;
                }
            }

            return r;
        }

        private double GetGlobalAverage(int instanceCount, SortedDictionary<double, double> resultDictionary)
        {
            if (instanceCount <= 10 && !m_parameters.m_byAllAssemblies)
            {
                double result = 0;
                SortedDictionary<double, double>.KeyCollection keys = resultDictionary.Keys;
                foreach (double key in keys)
                {
                    result += resultDictionary[key];
                }

                return result / instanceCount;
            }
            else
            {
                if (resultDictionary.Count != 0)
                    return resultDictionary.Last().Value;
                else
                    return 0;
            }
        }
    }
}
