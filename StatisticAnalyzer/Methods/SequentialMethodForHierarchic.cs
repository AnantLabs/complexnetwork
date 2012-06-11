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
    public class SequentialMethodForHierarchic : AbstractMethod
    {
        private Int16 m_hierarchicBase;
        private Int16 m_blockDegree;
        private Double m_lambdaParam;
        private int m_size;

        private SortedDictionary<StatAnalyzeLocalParameters, double> m_mathWaitings;
        private SortedDictionary<StatAnalyzeLocalParameters, double> m_dispersions;

        public SequentialMethodForHierarchic() { }

        override public void SetParameters(List<object> parameters)
        {
            m_hierarchicBase = Convert.ToInt16(parameters[0]);
            m_blockDegree = Convert.ToInt16(parameters[1]);
            m_lambdaParam = Convert.ToDouble(parameters[2]);
            m_size = (int)Math.Pow(m_hierarchicBase, m_blockDegree);

            m_mathWaitings = new SortedDictionary<StatAnalyzeLocalParameters, double>();
            m_dispersions = new SortedDictionary<StatAnalyzeLocalParameters, double>();
            m_mathWaitings[StatAnalyzeLocalParameters.DegreeDistribution] = 0;
            m_mathWaitings[StatAnalyzeLocalParameters.ConnectedSubgraphsByOrders] = 0;
            m_dispersions[StatAnalyzeLocalParameters.DegreeDistribution] = 0;
            m_dispersions[StatAnalyzeLocalParameters.ConnectedSubgraphsByOrders] = 0;
        }

        override protected bool ContainsOption(ResultAssembly assembly, AnalyseOptions option)
        {
            return (option == AnalyseOptions.ClusteringCoefficient ?
                assembly.Results[0].Coefficient.Keys.Count != 0 :
                assembly.Results[0].Result.Keys.Contains(option));
        }

        override protected bool IsCorrectAssembly(ResultAssembly assembly, AnalyseOptions option)
        {
            return ContainsOption(assembly, option) &&
                (Int16)assembly.GenerationParams[GenerationParam.BranchIndex] == m_hierarchicBase &&
                (Int16)assembly.GenerationParams[GenerationParam.Level] == m_blockDegree &&
                (Double)assembly.GenerationParams[GenerationParam.Mu] == m_lambdaParam;
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
                    switch (option)
                    {
                        case AnalyseOptions.ClusteringCoefficient:
                            {
                                resultDictionary = FillGlobalResultForCC(result, I, deltaI, instanceCount, previousInstanceCount);
                                break;
                            }
                        case AnalyseOptions.DegreeDistribution:
                            {
                                resultDictionary = FillGlobalResultForDD(result, I, deltaI, instanceCount, previousInstanceCount);
                                break;
                            }
                        default:
                            {
                                resultDictionary = FillGlobalResult(option, result, I, deltaI, result.Results.Count, previousInstanceCount);
                                break;
                            }
                    }

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
                switch (option)
                {
                    case AnalyseOptions.ClusteringCoefficient:
                        {
                            resultDictionary = FillGlobalResultForCC(result, I, deltaI, result.Results.Count, -1);
                            break;
                        }
                    case AnalyseOptions.DegreeDistribution:
                        {
                            resultDictionary = FillGlobalResultForDD(result, I, deltaI, result.Results.Count, -1);
                            break;
                        }
                    default:
                        {
                            resultDictionary = FillGlobalResult(option, result, I, deltaI, result.Results.Count, -1);
                            break;
                        }
                }
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
                        switch (option)
                        {
                            case AnalyseOptions.ClusteringCoefficient:
                                {
                                    tempResult = FillLocalResultForCC(result, i, initialInstance);
                                    break;
                                }
                            default:
                                {
                                    tempResult = FillLocalResult(option, result, i, initialInstance);
                                    break;
                                }
                        }
                    }

                    if (!m_parameters.m_byAllAssemblies)
                        break;
                }
            }

            SortedDictionary<double, double>.KeyCollection keys = tempResult.Keys;
            return GetLocalResult(option, keys, tempResult, instanceCount);
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
                    switch (option)
                    {
                        case AnalyseOptions.ClusteringCoefficient:
                            {
                                tempResult = FillLocalResultForCC(result, i, 0);
                                break;
                            }
                        default:
                            {
                                tempResult = FillLocalResult(option, result, i, 0);
                                break;
                            }
                    }
                }
            }

            SortedDictionary<double, double>.KeyCollection keys = tempResult.Keys;
            return GetLocalResult(option, keys, tempResult, instanceCount);
        }

        override public string GetParameterLine()
        {
            string result = "Hierarchic Base = " + m_hierarchicBase.ToString() + "; Block Degree = " + m_blockDegree.ToString()
                + "; Lambda Parameter = " + m_lambdaParam + ";";

            return result;
        }

        override public double CountM(StatAnalyzeLocalParameters p)
        {
            return m_mathWaitings[p];
        }

        override public double CountD(StatAnalyzeLocalParameters p)
        {
            return m_dispersions[p];
        }

        // Utilities //

        private SortedDictionary<double, double> FillGlobalResultForCC(ResultAssembly result, int I, int deltaI, int instanceCount, int previousInstanceCount)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            if (instanceCount <= 10 && !m_parameters.m_byAllAssemblies)
            {
                for (int i = 0; i < instanceCount; ++i)
                {
                    Dictionary<double, int>.KeyCollection keyColl = result.Results[i].Coefficient.Keys;
                    double sumOfCoeffs = 0;
                    foreach (double key in keyColl)
                    {
                        sumOfCoeffs += key * result.Results[i].Coefficient[key];
                    }
                    sumOfCoeffs /= m_size;
                    r.Add(i + 1, sumOfCoeffs);
                }
            }
            else
            {
                int index = previousInstanceCount == -1 ? I : I - previousInstanceCount;
                double sum = 0;
                while (I <= instanceCount)
                {
                    sum = 0;
                    for (int i = 0; i < I - index; ++i)
                    {
                        Dictionary<double, int>.KeyCollection keyColl = result.Results[i].Coefficient.Keys;
                        double sumOfCoeffs = 0;
                        foreach (double key in keyColl)
                        {
                            sumOfCoeffs += key * result.Results[i].Coefficient[key];
                        }
                        sumOfCoeffs /= m_size;

                        sum += sumOfCoeffs;
                    }
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

        private SortedDictionary<double, double> FillGlobalResultForDD(ResultAssembly result, int I, int deltaI, int instanceCount, int previousInstanceCount)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            if (instanceCount <= 10 && !m_parameters.m_byAllAssemblies)
            {
                for (int i = 0; i < instanceCount; ++i)
                {
                    Dictionary<int, int>.KeyCollection keyColl = result.Results[i].VertexDegree.Keys;
                    double sumOfDegrees = 0;
                    foreach (int key in keyColl)
                    {
                        sumOfDegrees += key * result.Results[i].VertexDegree[key];
                    }
                    sumOfDegrees /= m_size;
                    r.Add(i + 1, sumOfDegrees);
                }
            }
            else
            {
                int index = previousInstanceCount == -1 ? I : I - previousInstanceCount;
                double sum = 0;
                while (I <= instanceCount)
                {
                    sum = 0;
                    for (int i = 0; i < index; ++i)
                    {
                        Dictionary<int, int>.KeyCollection keyColl = result.Results[i].VertexDegree.Keys;
                        double sumOfDegrees = 0;
                        foreach (int key in keyColl)
                        {
                            sumOfDegrees += key * result.Results[i].VertexDegree[key];
                        }
                        sumOfDegrees /= m_size;

                        sum += sumOfDegrees;
                    }
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

        private SortedDictionary<double, double> FillGlobalResult(AnalyseOptions option, ResultAssembly result, int I, int deltaI,
            int instanceCount, int previousInstanceCount)
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
                int index = previousInstanceCount == -1 ? I : I - previousInstanceCount;
                double sum = 0;
                while (I <= instanceCount)
                {
                    sum = 0;
                    for (int i = 0; i < index; ++i)
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

        private SortedDictionary<double, double> FillLocalResultForCC(ResultAssembly result, int i, int initialInstance)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            Dictionary<double, int> CCDictionary = result.Results[i - initialInstance].Coefficient;
            Dictionary<double, int>.KeyCollection keyColl = CCDictionary.Keys;
            foreach (double key in keyColl)
            {
                if (r.Keys.Contains(key))
                    r[key] += CCDictionary[key];
                else
                    r.Add(key, CCDictionary[key]);
            }
            return r;
        }

        private SortedDictionary<double, double> FillLocalResult(AnalyseOptions option, ResultAssembly result, int i, int initialInstance)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            Dictionary<int, int> tempDictionary = (option == AnalyseOptions.DegreeDistribution) ?
                result.Results[i - initialInstance].VertexDegree : result.Results[i - initialInstance].Subgraphs;
            Dictionary<int, int>.KeyCollection keyColl = tempDictionary.Keys;

            foreach (int key in keyColl)
            {
                if (r.Keys.Contains(key))
                    r[key] += (double)tempDictionary[key] / m_size;
                else
                    r.Add(key, (double)tempDictionary[key] / m_size);
            }
            return r;
        }

        private SortedDictionary<double, double> GetLocalResult(AnalyseOptions option,
            SortedDictionary<double, double>.KeyCollection keys, SortedDictionary<double, double> t, int instanceCount)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            foreach (double key in keys)
            {
                r.Add(key, t[key] / instanceCount);
            }
            FillMathWaitingsAndDispersions(r, option);

            int param = 1;
            if (option == AnalyseOptions.DegreeDistribution)
            {
                if (m_parameters.m_thickeningDD > 0)
                    param = (int)Math.Ceiling(((m_parameters.m_thickeningDD * r.Count()) / 100));
                return UseThickening(option, r, param);
            }
            else if (option == AnalyseOptions.FullSubGraph)
            {
                if (m_parameters.m_thickeningSubgraph > 0)
                    param = (int)Math.Ceiling(((m_parameters.m_thickeningSubgraph * r.Count()) / 100));
                return UseThickening(option, r, param);
            }
            else
                return r;
        }

        private void FillMathWaitingsAndDispersions(SortedDictionary<double, double> r, AnalyseOptions option)
        {
            StatAnalyzeLocalParameters sP = (option == AnalyseOptions.DegreeDistribution) ?
                StatAnalyzeLocalParameters.DegreeDistribution :
                StatAnalyzeLocalParameters.ConnectedSubgraphsByOrders;

            SortedDictionary<double, double>.KeyCollection keys = r.Keys;
            double mathWaiting = 0, mathWaitingSquare = 0;
            foreach (double key in keys)
            {
                mathWaiting += key * r[key];
                mathWaitingSquare += Math.Pow(key, 2) * r[key];
            }

            m_mathWaitings[sP] = mathWaiting;
            m_dispersions[sP] = mathWaitingSquare - Math.Pow(mathWaiting, 2);
        }

        private SortedDictionary<double, double> UseThickening(AnalyseOptions option, SortedDictionary<double, double> r, int p)
        {
            SortedDictionary<double, double> res = new SortedDictionary<double, double>();
            double[] array = r.Values.ToArray();

            int k = 1, step = p;
            double sum = 0;
            for (int i = 0; i < array.Count(); ++i)
            {
                if (k <= p)
                {
                    sum += array[i];
                    ++k;
                }
                else
                {
                    res.Add(step, sum / p);
                    sum = array[i];
                    k = 2;
                    step += p;
                }
            }

            res.Add(array.Count(), sum / ((array.Count() % p == 0) ? p : array.Count() % p));
            return res;
        }
    }
}