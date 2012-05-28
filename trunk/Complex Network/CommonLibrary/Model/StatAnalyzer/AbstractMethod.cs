using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonLibrary.Model.Result;
using RandomGraph.Common.Storage;
using RandomGraph.Common.Model.Generation;

namespace RandomGraph.Common.Model.StatAnalyzer
{
    /// <summary>
    /// Base class for statistical analyze method implementation
    /// </summary>
    public abstract class AbstractMethod
    {
        public StatAnalyzeParameters m_parameters;

        // Analyzer Data //
        protected int m_size;
        protected SortedDictionary<AnalyseOptions, double> m_localMathWaitings;
        protected SortedDictionary<AnalyseOptions, double> m_localDispersions;

        // Common Data //
        protected IResultStorage m_storage;
        protected List<ResultAssembly> m_resultAssemblies;
        protected List<string> m_assemblesID;

        public AbstractMethod()
        {
            m_localMathWaitings = new SortedDictionary<AnalyseOptions, double>();
            m_localDispersions = new SortedDictionary<AnalyseOptions, double>();

            m_localMathWaitings[AnalyseOptions.DegreeDistribution] = 0;
            m_localMathWaitings[AnalyseOptions.ConnSubGraph] = 0;
            m_localMathWaitings[AnalyseOptions.MinPathDist] = 0;
            m_localMathWaitings[AnalyseOptions.EigenValue] = 0;
            m_localMathWaitings[AnalyseOptions.DistEigenPath] = 0;
            m_localMathWaitings[AnalyseOptions.Cycles] = 0;

            m_localDispersions[AnalyseOptions.DegreeDistribution] = 0;
            m_localDispersions[AnalyseOptions.ConnSubGraph] = 0;
            m_localDispersions[AnalyseOptions.MinPathDist] = 0;
            m_localDispersions[AnalyseOptions.EigenValue] = 0;
            m_localDispersions[AnalyseOptions.DistEigenPath] = 0;
            m_localDispersions[AnalyseOptions.Cycles] = 0;
        }

        abstract public void SetSize();
        abstract public Dictionary<GenerationParam, string> GetParameterLine();
        abstract protected bool IsCorrectAssembly(ResultAssembly assembly, AnalyseOptions option);

        // Global Analyze //

        virtual public KeyValuePair<SortedDictionary<double, double>, double> GlobalAnalyzeByOption(AnalyseOptions option)
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
                    resultDictionary = GlobalCases(option, result, I, deltaI, instanceCount, 0);

                    break;
                }
            }

            resultPair = new KeyValuePair<SortedDictionary<double, double>, double>(resultDictionary,
                GetGlobalAverage(instanceCount, resultDictionary));

            return resultPair;
        }

        virtual public KeyValuePair<SortedDictionary<double, double>, double> GlobalAnalyzeByOption(string jobName, AnalyseOptions option)
        {
            KeyValuePair<SortedDictionary<double, double>, double> resultPair = new KeyValuePair<SortedDictionary<double, double>, double>();

            SortedDictionary<double, double> resultDictionary = new SortedDictionary<double, double>();
            ResultAssembly result = m_storage.Load(m_resultAssemblies.Find(i => i.Name == jobName).ID);
            int instanceCount = 0, deltaI = 10, I = deltaI;
            if (ContainsOption(result, option))
            {
                instanceCount = result.Results.Count;
                resultDictionary = GlobalCases(option, result, I, deltaI, instanceCount, 0);
            }

            resultPair = new KeyValuePair<SortedDictionary<double, double>, double>(resultDictionary,
                GetGlobalAverage(instanceCount, resultDictionary));

            return resultPair;
        }

        // Local Analyze //

        virtual public SortedDictionary<double, double> LocalAnalyzeByOption(AnalyseOptions option)
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
                        tempResult = LocalCases(option, result, i, 0);
                    }

                    break;
                }
            }

            return GetLocalResult(option, tempResult, instanceCount);
        }

        virtual public SortedDictionary<double, double> LocalAnalyzeByOption(string jobName, AnalyseOptions option)
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

        // Motif Analyze

        virtual public SortedDictionary<double, double> MotifAnalyzeByOption(AnalyseOptions option)
        {
            SortedDictionary<double, double> resultDictionary = new SortedDictionary<double, double>();
            foreach (string resultName in m_assemblesID)
            {
                ResultAssembly result = m_storage.Load(m_resultAssemblies.Find(i => i.Name == resultName).ID);
                if (IsCorrectAssembly(result, option))
                {
                    break;
                }
            }

            return resultDictionary;
        }

        virtual public SortedDictionary<double, double> MotifAnalyzeByOption(string jobName, AnalyseOptions option)
        {
            SortedDictionary<double, double> resultDictionary = new SortedDictionary<double, double>();
            ResultAssembly result = m_storage.Load(m_resultAssemblies.Find(i => i.Name == jobName).ID);
            if (ContainsOption(result, option))
            {
            }

            return resultDictionary;
        }

        // Common methods //

        protected bool ContainsOption(ResultAssembly assembly, AnalyseOptions option)
        {
            switch (option)
            {
                case AnalyseOptions.ClusteringCoefficient:
                    return assembly.Results[0].Coefficient.Keys.Count != 0;
                case AnalyseOptions.DegreeDistribution:
                    return assembly.Results[0].VertexDegree.Keys.Count != 0;
                case AnalyseOptions.ConnSubGraph:
                    return assembly.Results[0].Subgraphs.Keys.Count != 0;
                case AnalyseOptions.MinPathDist:
                    return assembly.Results[0].DistanceBetweenVertices.Count != 0;
                case AnalyseOptions.EigenValue:
                    return assembly.Results[0].EigenVector.Count != 0;
                case AnalyseOptions.DistEigenPath:
                    return assembly.Results[0].DistancesBetweenEigenValues.Count != 0;
                case AnalyseOptions.Cycles:
                    return assembly.Results[0].Cycles.Count != 0;
                default:
                    return assembly.Results[0].Result.Keys.Contains(option);
            }
        }

        public void RefreshParameters(IResultStorage storage, List<ResultAssembly> l)
        {
            m_storage = storage;
            m_resultAssemblies = l;
            GetAllIDs();
        }

        public List<string> GetValues(GenerationParam parameter)
        {
            List<string> result = new List<string>();
            foreach (string resultName in m_assemblesID)
            {
                ResultAssembly r = m_storage.Load(m_resultAssemblies.Find(i => i.Name == resultName).ID);
                result.Add(r.GenerationParams[parameter].ToString());
            }
            result.Sort();
            result = result.Distinct().ToList();
            return result;
        }

        public List<string> GetValues(Dictionary<GenerationParam, string> values,
            GenerationParam parameter)
        {
            List<string> result = new List<string>();
            foreach (string resultName in m_assemblesID)
            {
                ResultAssembly r = m_storage.Load(m_resultAssemblies.Find(i => i.Name == resultName).ID);
                Dictionary<GenerationParam, string>.KeyCollection keys = values.Keys;
                bool b = true;
                foreach (GenerationParam key in keys)
                {
                    b = b && (r.GenerationParams[key].ToString() == values[key]);
                }
                if (b)
                    result.Add(r.GenerationParams[parameter].ToString());
            }
            result.Sort();
            result = result.Distinct().ToList();
            return result;
        }

        public double GetLocalM(AnalyseOptions p)
        {
            return m_localMathWaitings[p];
        }

        public double GetLocalD(AnalyseOptions p)
        {
            return m_localDispersions[p];
        }

        public StatAnalyzeParameters Parameters
        {
            set { m_parameters = value; }
        }

        // Utilities //

        private void GetAllIDs()
        {
            m_assemblesID = new List<string>();
            foreach (ResultAssembly result in m_resultAssemblies)
                m_assemblesID.Add(result.Name);
        }

        protected SortedDictionary<double, double> GlobalCases(AnalyseOptions option, ResultAssembly result, int I,
            int deltaI, int instanceCount, int previousInstanceCount)
        {
            switch (option)
            {
                case AnalyseOptions.ClusteringCoefficient:
                    {
                        return FillGlobalResultCC(result, I, deltaI, instanceCount, previousInstanceCount);
                    }
                case AnalyseOptions.DegreeDistribution:
                    {
                        return FillGlobalResultDD(result, I, deltaI, instanceCount, previousInstanceCount);
                    }
                default:
                    {
                        return FillGlobalResult(option, result, I, deltaI, result.Results.Count, previousInstanceCount);
                    }
            }
        }

        protected SortedDictionary<double, double> LocalCases(AnalyseOptions option, ResultAssembly result, int i,
            int initialInstance)
        {
            switch (option)
            {
                case AnalyseOptions.ClusteringCoefficient:
                    {
                        return FillLocalResultCC(result, i, initialInstance);
                    }
                case AnalyseOptions.EigenValue:
                    {
                        return FillLocalResultEigen(result, i);
                    }
                case AnalyseOptions.DistEigenPath:
                    {
                        return FillLocalResultEigenDistance(result, i, initialInstance);
                    }
                default:
                    {
                        return FillLocalResult(option, result, i, initialInstance);
                    }
            }
        }

        private SortedDictionary<double, double> FillGlobalResultCC(ResultAssembly result, int I, int deltaI, int instanceCount, int previousInstanceCount)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            if (instanceCount <= 10 && !m_parameters.m_byAllAssemblies)
            {
                for (int i = 0; i < instanceCount; ++i)
                {
                    SortedDictionary<double, int>.KeyCollection keyColl = result.Results[i].Coefficient.Keys;
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
                        SortedDictionary<double, int>.KeyCollection keyColl = result.Results[i].Coefficient.Keys;
                        double sumOfCoeffs = 0;
                        foreach (double key in keyColl)
                        {
                            sumOfCoeffs += key * result.Results[i].Coefficient[key];
                        }
                        sumOfCoeffs /= m_size;

                        sum += sumOfCoeffs;
                    }
                    r.Add(I, sum / I);
                    I += deltaI;
                }
            }

            return r;
        }

        private SortedDictionary<double, double> FillGlobalResultDD(ResultAssembly result, int I, int deltaI, int instanceCount, int previousInstanceCount)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            if (instanceCount <= 10 && !m_parameters.m_byAllAssemblies)
            {
                for (int i = 0; i < instanceCount; ++i)
                {
                    SortedDictionary<int, int>.KeyCollection keyColl = result.Results[i].VertexDegree.Keys;
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
                        SortedDictionary<int, int>.KeyCollection keyColl = result.Results[i].VertexDegree.Keys;
                        double sumOfDegrees = 0;
                        foreach (int key in keyColl)
                        {
                            sumOfDegrees += key * result.Results[i].VertexDegree[key];
                        }
                        sumOfDegrees /= m_size;

                        sum += sumOfDegrees;
                    }
                    r.Add(I, sum / I);
                    I += deltaI;
                }
            }

            return r;
        }

        private SortedDictionary<double, double> FillGlobalResult(AnalyseOptions option, ResultAssembly result, int I,
            int deltaI, int instanceCount, int previousInstanceCount)
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
                    I += deltaI;
                }
            }
            return r;
        }

        protected double GetGlobalAverage(int instanceCount, SortedDictionary<double, double> resultDictionary)
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

        private SortedDictionary<double, double> FillLocalResultCC(ResultAssembly result, int i, int initialInstance)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            SortedDictionary<double, int> CCDictionary = result.Results[i - initialInstance].Coefficient;
            SortedDictionary<double, int>.KeyCollection keyColl = CCDictionary.Keys;
            foreach (double key in keyColl)
            {
                if (r.Keys.Contains(key))
                    r[key] += CCDictionary[key];
                else
                    r.Add(key, CCDictionary[key]);
            }

            return r;
        }

        private SortedDictionary<double, double> FillLocalResultEigen(ResultAssembly result, int i)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            ArrayList l = result.Results[i].EigenVector;
            l.Sort();
            for (int k = 0; k < l.Count; ++k)
            {
                if (!r.ContainsKey((double)l[k]))
                    r.Add((double)l[k], 1);
            }

            return r;
        }

        private SortedDictionary<double, double> FillLocalResultEigenDistance(ResultAssembly result, int i, int initialInstance)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            SortedDictionary<double, int> EDDictionary = result.Results[i - initialInstance].DistancesBetweenEigenValues;
            SortedDictionary<double, int>.KeyCollection keyCol = EDDictionary.Keys;
            foreach (double key in keyCol)
                if (r.Keys.Contains(key))
                    r[key] += (double)EDDictionary[key] / EDDictionary.Count;
                else
                    r.Add(key, (double)EDDictionary[key] / EDDictionary.Count);

            return r;

            /* // keteri mijev heravorutyun@ < difference
            double[] keys = eigenDistanceDictionary.Keys.ToArray();
            double difference = 
             * m_parameters.m_localAnalyzeOptions[StatAnalyzeLocalParameters.DistancesBetweenEigenValues].m_optionValue;

            int d = 0, c = 0, tempD = 0;
            while (d < keys.Count() - 1)
            {
                tempD = d;
                c = eigenDistanceDictionary[keys[d]];
                ++d;
                while (d < keys.Count() && (keys[d] - keys[tempD] < difference))
                {
                    c += eigenDistanceDictionary[keys[d]];
                    ++d;
                }
                r.Add(keys[tempD], c);
            }

            return r;*/
        }

        protected SortedDictionary<double, double> FillLocalResult(AnalyseOptions option, ResultAssembly result, int i, int initialInstance)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            SortedDictionary<int, int> tempDictionary = new SortedDictionary<int, int>();
            SortedDictionary<int, long> tempDictionaryCycles = new SortedDictionary<int, long>();

            switch (option)
            {
                case AnalyseOptions.DegreeDistribution:
                    {
                        tempDictionary = result.Results[i - initialInstance].VertexDegree;
                        break;
                    }
                case AnalyseOptions.ConnSubGraph:
                    {
                        tempDictionary = result.Results[i - initialInstance].Subgraphs;
                        break;
                    }
                case AnalyseOptions.MinPathDist:
                    {
                        tempDictionary = result.Results[i - initialInstance].DistanceBetweenVertices;
                        break;
                    }
                case AnalyseOptions.Cycles:
                    {   
                        tempDictionaryCycles = result.Results[i - initialInstance].Cycles;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            if (option == AnalyseOptions.Cycles)
            {
                SortedDictionary<int, long>.KeyCollection keyColl = tempDictionaryCycles.Keys;
                int div = (option == AnalyseOptions.MinPathDist) ? (m_size * (m_size - 1) / 2) : m_size;

                foreach (int key in keyColl)
                {
                    if (r.Keys.Contains(key))
                        r[key] += (double)tempDictionaryCycles[key] / div;
                    else
                        r.Add(key, (double)tempDictionaryCycles[key] / div);
                }
            }
            else
            {
                SortedDictionary<int, int>.KeyCollection keyColl = tempDictionary.Keys;
                int div = (option == AnalyseOptions.MinPathDist) ? (m_size * (m_size - 1) / 2) : m_size;

                foreach (int key in keyColl)
                {
                    if (r.Keys.Contains(key))
                        r[key] += (double)tempDictionary[key] / div;
                    else
                        r.Add(key, (double)tempDictionary[key] / div);
                }
            }

            return r;
        }

        protected SortedDictionary<double, double> GetLocalResult(AnalyseOptions option,
            SortedDictionary<double, double> t, int instanceCount)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            SortedDictionary<double, double>.KeyCollection keys = t.Keys;
            foreach (double key in keys)
            {
                r.Add(key, t[key] / instanceCount);
            }

            FillMathWaitingsAndDispersions(r, option);

            int thickening = 0;
            if (m_parameters.m_localAnalyzeOptions[option].m_useDelta)
                thickening = (int)m_parameters.m_localAnalyzeOptions[option].m_optionValue;
            else
                thickening = (int)Math.Ceiling(((m_parameters.m_localAnalyzeOptions[option].m_optionValue * r.Count()) / 100)); ;

            return UseThickening(r, thickening);
        }

        private SortedDictionary<double, double> UseDelta(SortedDictionary<double, double> r, double delta)
        {
            if (delta == 0)
                return r;
            else
            {
                SortedDictionary<double, double> res = new SortedDictionary<double, double>();
                double[] l = r.Keys.ToArray();
                double initial = l[0] + delta;

                int k = 0;
                double count = 0;
                while (k < l.Count())
                {
                    while (k < l.Count() && l[k] < initial)
                    {
                        count += (double)r[l[k]];
                        ++k;
                    }
                    res.Add(initial, (double)count / l.Count());
                    count = 0;
                    initial += delta;
                }

                return res;
            }
        }

        private SortedDictionary<double, double> UseThickening(SortedDictionary<double, double> r, int t)
        {
            if (t == 0)
                return r;
            else
            {
                SortedDictionary<double, double> res = new SortedDictionary<double, double>();
                double[] array = r.Values.ToArray();

                int k = 1, step = t;
                double sum = 0;
                for (int i = 0; i < array.Count(); ++i)
                {
                    if (k <= t)
                    {
                        sum += array[i];
                        ++k;
                    }
                    else
                    {
                        res.Add(step, sum / t);
                        sum = array[i];
                        k = 2;
                        step += t;
                    }
                }

                res.Add(array.Count(), sum / ((array.Count() % t == 0) ? t : array.Count() % t));
                return res;
            }
        }

        private void FillMathWaitingsAndDispersions(SortedDictionary<double, double> r, AnalyseOptions option)
        {
            SortedDictionary<double, double>.KeyCollection keys = r.Keys;
            double mathWaiting = 0, mathWaitingSquare = 0;
            foreach (double key in keys)
            {
                mathWaiting += key * r[key];
                mathWaitingSquare += Math.Pow(key, 2) * r[key];
            }

            m_localMathWaitings[option] = mathWaiting;
            m_localDispersions[option] = mathWaitingSquare - Math.Pow(mathWaiting, 2);
        }
    }
}