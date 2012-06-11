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
    class SequentialMethodForWS : AbstractMethod
    {
        private Int32 m_numberOfVertices;
        private Int32 m_numberOfEdges;
        private double m_probability;

        private SortedDictionary<StatAnalyzeLocalParameters, double> m_mathWaitings;
        private SortedDictionary<StatAnalyzeLocalParameters, double> m_dispersions;

        public SequentialMethodForWS() { }

        override public void SetParameters(List<object> parameters)
        {
            m_numberOfVertices = Convert.ToInt32(parameters[0]);
            m_numberOfEdges = Convert.ToInt32(parameters[1]);
            m_probability = Convert.ToDouble(parameters[2]);

            m_mathWaitings = new SortedDictionary<StatAnalyzeLocalParameters, double>();
            m_dispersions = new SortedDictionary<StatAnalyzeLocalParameters, double>();
            m_mathWaitings[StatAnalyzeLocalParameters.DegreeDistribution] = 0;
            m_mathWaitings[StatAnalyzeLocalParameters.ConnectedSubgraphsByOrders] = 0;
            m_dispersions[StatAnalyzeLocalParameters.DegreeDistribution] = 0;
            m_dispersions[StatAnalyzeLocalParameters.ConnectedSubgraphsByOrders] = 0;
        }

        override protected bool ContainsOption(ResultAssembly assembly, AnalyseOptions option)
        {
            return assembly.Results[0].Result.Keys.Contains(option);
        }

        override protected bool IsCorrectAssembly(ResultAssembly assembly, AnalyseOptions option)
        {
            return ContainsOption(assembly, option) &&
                (Int32)assembly.GenerationParams[GenerationParam.Verticles] == m_numberOfVertices &&
                (Int32)assembly.GenerationParams[GenerationParam.Edges] == m_numberOfEdges &&
                (Double)assembly.GenerationParams[GenerationParam.P] == m_probability;
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
                    switch(option)
                    {
                        case AnalyseOptions.DegreeDistribution:
                            {
                                resultDictionary = FillGlobalResultDD(option, result, I, deltaI, result.Results.Count);
                                break;
                            }
                        default:
                            {
                                resultDictionary = FillGlobalResult(option, result, I, deltaI, result.Results.Count);
                                break;
                            }
                    }
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
                switch (option)
                {
                    case AnalyseOptions.DegreeDistribution:
                        {
                            resultDictionary = FillGlobalResultDD(option, result, I, deltaI, result.Results.Count);
                            break;
                        }
                    default:
                        {
                            resultDictionary = FillGlobalResult(option, result, I, deltaI, result.Results.Count);
                            break;
                        }
                }                
            }

            resultPair = new KeyValuePair<SortedDictionary<double, double>, double>(resultDictionary,
                GetGlobalAverage(instanceCount, resultDictionary));

            return resultPair;
        }

        // Local Analyze //

        override public SortedDictionary<double, double> LocalAnalyzeByOption(AnalyseOptions option)
        {
            SortedDictionary<double, double> resultDictionary = new SortedDictionary<double, double>();
            SortedDictionary<double, double> tempResult = new SortedDictionary<double, double>();
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
                                    tempResult = FillLocalResultForCC(result, i);
                                    break;
                                }
                            default:
                                {
                                    tempResult = FillLocalResult(option, result, i);
                                    break;
                                }
                        }
                    }
                    
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
                                tempResult = FillLocalResultForCC(result, i);
                                break;
                            }
                        default:
                            {
                                tempResult = FillLocalResult(option, result, i);
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
            return "Number Of Vertices = " + m_numberOfVertices.ToString() + "; Number Of Edges = " + 
                m_numberOfEdges.ToString() + "; Probability = " + m_probability.ToString() + ";";
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

        private SortedDictionary<double, double> FillGlobalResultDD(AnalyseOptions option, ResultAssembly result, int I, int deltaI,
            int instanceCount)
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
                    sumOfDegrees /= m_numberOfVertices;
                    r.Add(i + 1, sumOfDegrees);
                }
            }
            else
            {
                double sum = 0;
                while (I <= instanceCount)
                {
                    sum = 0;
                    for (int i = 0; i < I; ++i)
                    {
                        Dictionary<int, int>.KeyCollection keyColl = result.Results[i].VertexDegree.Keys;
                        double sumOfDegrees = 0;
                        foreach (int key in keyColl)
                        {
                            sumOfDegrees += key * result.Results[i].VertexDegree[key];
                        }
                        sumOfDegrees /= m_numberOfVertices;

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


        private SortedDictionary<double, double> FillLocalResultForCC(ResultAssembly result, int i)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            Dictionary<double, int> CCDictionary = result.Results[i].Coefficient;
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

        private SortedDictionary<double, double> FillLocalResult(AnalyseOptions option, ResultAssembly result, int i)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            Dictionary<int, int> tempDictionary = (option == AnalyseOptions.DegreeDistribution) ?
                result.Results[i].VertexDegree : result.Results[i].Subgraphs;
            Dictionary<int, int>.KeyCollection keyColl = tempDictionary.Keys;

            foreach (int key in keyColl)
            {
                if (r.Keys.Contains(key))
                    r[key] += (double)tempDictionary[key] / m_numberOfVertices;
                else
                    r.Add(key, (double)tempDictionary[key] / m_numberOfVertices);
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
