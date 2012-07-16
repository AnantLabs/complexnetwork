﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonLibrary.Model.Result;

using RandomGraph.Common.Model;

namespace StatisticAnalyzer
{
    public class StAnalyzer
    {
        private ResultAssembly assemblyToAnalyze;
        private Dictionary<AnalyseOptions, StAnalyzeOptions> analyzeOptions;
        private StAnalyzeResult result;

        public AnalyseOptions globalOptions = AnalyseOptions.None;
        public AnalyseOptions localOptions = AnalyseOptions.None;

        public StAnalyzer(ResultAssembly assembly)//, StAnalyzeOptions options)
        {
            assemblyToAnalyze = assembly;
            analyzeOptions = new Dictionary<AnalyseOptions,StAnalyzeOptions>();
            result = new StAnalyzeResult();
        }

        public Dictionary<AnalyseOptions, StAnalyzeOptions> AnalyzeOptions
        {
            set { analyzeOptions = value; }
        }

        public void GlobalAnalyze()
        {
            if ((globalOptions & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath)
                GlobalAnalyzeByOption(AnalyseOptions.AveragePath);
            if ((globalOptions & AnalyseOptions.Diameter) == AnalyseOptions.Diameter)
                GlobalAnalyzeByOption(AnalyseOptions.Diameter);
            if ((globalOptions & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient)
                GlobalAnalyzeByOption(AnalyseOptions.ClusteringCoefficient);
            if ((globalOptions & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution)
                GlobalAnalyzeByOption(AnalyseOptions.DegreeDistribution);
            if ((globalOptions & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3)
                GlobalAnalyzeByOption(AnalyseOptions.Cycles3);
            if ((globalOptions & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4)
                GlobalAnalyzeByOption(AnalyseOptions.Cycles4);
            if ((globalOptions & AnalyseOptions.MaxFullSubgraph) == AnalyseOptions.MaxFullSubgraph)
                GlobalAnalyzeByOption(AnalyseOptions.MaxFullSubgraph);
            if ((globalOptions & AnalyseOptions.LargestConnectedComponent) == AnalyseOptions.LargestConnectedComponent)
                GlobalAnalyzeByOption(AnalyseOptions.LargestConnectedComponent);
            if ((globalOptions & AnalyseOptions.MinEigenValue) == AnalyseOptions.MinEigenValue)
                GlobalAnalyzeByOption(AnalyseOptions.MinEigenValue);
            if ((globalOptions & AnalyseOptions.MaxEigenValue) == AnalyseOptions.MaxEigenValue)
                GlobalAnalyzeByOption(AnalyseOptions.MaxEigenValue);
        }

        public void LocalAnalyze()
        {
            if ((localOptions & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient)
                LocalAnalyzeByOption(AnalyseOptions.ClusteringCoefficient);
            if ((localOptions & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution)
                LocalAnalyzeByOption(AnalyseOptions.DegreeDistribution);
            if ((localOptions & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient)
                LocalAnalyzeByOption(AnalyseOptions.ClusteringCoefficient);
            if ((localOptions & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution)
                LocalAnalyzeByOption(AnalyseOptions.DegreeDistribution);
            if ((localOptions & AnalyseOptions.ConnSubGraph) == AnalyseOptions.ConnSubGraph)
                LocalAnalyzeByOption(AnalyseOptions.ConnSubGraph);
            if ((localOptions & AnalyseOptions.MinPathDist) == AnalyseOptions.MinPathDist)
                LocalAnalyzeByOption(AnalyseOptions.MinPathDist);
            if ((localOptions & AnalyseOptions.EigenValue) == AnalyseOptions.EigenValue)
                LocalAnalyzeByOption(AnalyseOptions.EigenValue);
            if ((localOptions & AnalyseOptions.DistEigenPath) == AnalyseOptions.DistEigenPath)
                LocalAnalyzeByOption(AnalyseOptions.DistEigenPath);
            if ((localOptions & AnalyseOptions.Cycles) == AnalyseOptions.Cycles)
                LocalAnalyzeByOption(AnalyseOptions.Cycles);
        }

        // Global Analyze //

        private void GlobalAnalyzeByOption(AnalyseOptions option)
        {
            KeyValuePair<SortedDictionary<double, double>, double> resultPair;

            SortedDictionary<double, double> resultDictionary = new SortedDictionary<double, double>();
            int instanceCount = assemblyToAnalyze.Results.Count, deltaI = 10, I = deltaI;
            resultDictionary = GlobalCases(option, I, deltaI, instanceCount, 0);

            resultPair = new KeyValuePair<SortedDictionary<double, double>, double>(resultDictionary,
                GetGlobalAverage(instanceCount, resultDictionary));
        }

        // Local Analyze //

        private void LocalAnalyzeByOption(AnalyseOptions option)
        {
            SortedDictionary<double, double> resultDictionary = new SortedDictionary<double, double>();
            SortedDictionary<double, double> tempResult = new SortedDictionary<double, double>();
            int instanceCount = assemblyToAnalyze.Results.Count;
            for (int i = 0; i < instanceCount; ++i)
            {
                tempResult = LocalCases(option, i, 0);
            }

            GetLocalResult(option, tempResult, instanceCount);
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

        // Utilities //

        private SortedDictionary<double, double> GlobalCases(AnalyseOptions option, int I,
            int deltaI, int instanceCount, int previousInstanceCount)
        {
            switch (option)
            {
                case AnalyseOptions.ClusteringCoefficient:
                    {
                        return FillGlobalResultCC(I, deltaI, instanceCount, previousInstanceCount);
                    }
                case AnalyseOptions.DegreeDistribution:
                    {
                        return FillGlobalResultDD(I, deltaI, instanceCount, previousInstanceCount);
                    }
                default:
                    {
                        return FillGlobalResult(option, I, deltaI, previousInstanceCount);
                    }
            }
        }

        private SortedDictionary<double, double> LocalCases(AnalyseOptions option, int i,
            int initialInstance)
        {
            switch (option)
            {
                case AnalyseOptions.ClusteringCoefficient:
                    {
                        return FillLocalResultCC(i, initialInstance);
                    }
                case AnalyseOptions.EigenValue:
                    {
                        return FillLocalResultEigen(i);
                    }
                case AnalyseOptions.DistEigenPath:
                    {
                        return FillLocalResultEigenDistance(i, initialInstance);
                    }
                default:
                    {
                        return FillLocalResult(option, i, initialInstance);
                    }
            }
        }

        private SortedDictionary<double, double> FillGlobalResultCC(int I, int deltaI, int instanceCount, int previousInstanceCount)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            if (instanceCount <= 10)// && !m_parameters.m_byAllAssemblies)
            {
                for (int i = 0; i < instanceCount; ++i)
                {
                    SortedDictionary<double, int>.KeyCollection keyColl = assemblyToAnalyze.Results[i].Coefficient.Keys;
                    double sumOfCoeffs = 0;
                    foreach (double key in keyColl)
                    {
                        sumOfCoeffs += key * assemblyToAnalyze.Results[i].Coefficient[key];
                    }
                    sumOfCoeffs /= assemblyToAnalyze.Size;
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
                        SortedDictionary<double, int>.KeyCollection keyColl = assemblyToAnalyze.Results[i].Coefficient.Keys;
                        double sumOfCoeffs = 0;
                        foreach (double key in keyColl)
                        {
                            sumOfCoeffs += key * assemblyToAnalyze.Results[i].Coefficient[key];
                        }
                        sumOfCoeffs /= assemblyToAnalyze.Size;

                        sum += sumOfCoeffs;
                    }
                    r.Add(I, sum / I);
                    I += deltaI;
                }
            }

            return r;
        }

        private SortedDictionary<double, double> FillGlobalResultDD(int I, int deltaI, int instanceCount, int previousInstanceCount)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            if (instanceCount <= 10)// && !m_parameters.m_byAllAssemblies)
            {
                for (int i = 0; i < instanceCount; ++i)
                {
                    SortedDictionary<int, int>.KeyCollection keyColl = assemblyToAnalyze.Results[i].VertexDegree.Keys;
                    double sumOfDegrees = 0;
                    foreach (int key in keyColl)
                    {
                        sumOfDegrees += key * assemblyToAnalyze.Results[i].VertexDegree[key];
                    }
                    sumOfDegrees /= assemblyToAnalyze.Size;
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
                        SortedDictionary<int, int>.KeyCollection keyColl = assemblyToAnalyze.Results[i].VertexDegree.Keys;
                        double sumOfDegrees = 0;
                        foreach (int key in keyColl)
                        {
                            sumOfDegrees += key * assemblyToAnalyze.Results[i].VertexDegree[key];
                        }
                        sumOfDegrees /= assemblyToAnalyze.Size;

                        sum += sumOfDegrees;
                    }
                    r.Add(I, sum / I);
                    I += deltaI;
                }
            }

            return r;
        }

        private SortedDictionary<double, double> FillGlobalResult(AnalyseOptions option, int I,
            int deltaI, int previousInstanceCount)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            int iCount = assemblyToAnalyze.Results.Count;
            if (iCount <= 10)// && !m_parameters.m_byAllAssemblies)
            {
                for (int i = 0; i < iCount; ++i)
                {
                    r.Add(i + 1, assemblyToAnalyze.Results[i].Result[option]);
                }
            }
            else
            {
                int index = previousInstanceCount == -1 ? I : I - previousInstanceCount;
                double sum = 0;
                while (I <= iCount)
                {
                    sum = 0;
                    for (int i = 0; i < index; ++i)
                        sum += assemblyToAnalyze.Results[i].Result[option];
                    r.Add(I, sum / I);
                    I += deltaI;
                }
            }
            return r;
        }

        protected double GetGlobalAverage(int instanceCount, SortedDictionary<double, double> resultDictionary)
        {
            if (instanceCount <= 10)// && !m_parameters.m_byAllAssemblies)
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

        private SortedDictionary<double, double> FillLocalResultCC(int i, int initialInstance)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            SortedDictionary<double, int> CCDictionary = assemblyToAnalyze.Results[i - initialInstance].Coefficient;
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

        private SortedDictionary<double, double> FillLocalResultEigen(int i)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            ArrayList l = assemblyToAnalyze.Results[i].EigenVector;
            l.Sort();
            for (int k = 0; k < l.Count; ++k)
            {
                r.Add((double)l[k], 1);
            }

            return r;
        }

        private SortedDictionary<double, double> FillLocalResultEigenDistance(int i, int initialInstance)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            SortedDictionary<double, int> EDDictionary = assemblyToAnalyze.Results[i - initialInstance].DistancesBetweenEigenValues;
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

        protected SortedDictionary<double, double> FillLocalResult(AnalyseOptions option, int i, int initialInstance)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            SortedDictionary<int, int> tempDictionary = new SortedDictionary<int, int>();

            switch (option)
            {
                case AnalyseOptions.DegreeDistribution:
                    {
                        tempDictionary = assemblyToAnalyze.Results[i - initialInstance].VertexDegree;
                        break;
                    }
                case AnalyseOptions.ConnSubGraph:
                    {
                        tempDictionary = assemblyToAnalyze.Results[i - initialInstance].Subgraphs;
                        break;
                    }
                case AnalyseOptions.MinPathDist:
                    {
                        tempDictionary = assemblyToAnalyze.Results[i - initialInstance].DistanceBetweenVertices;
                        break;
                    }
                case AnalyseOptions.Cycles:
                    {
                        //tempDictionary = assemblyToAnalyze.Results[i - initialInstance].Cycles;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            SortedDictionary<int, int>.KeyCollection keyColl = tempDictionary.Keys;
            int size = assemblyToAnalyze.Size;
            int div = (option == AnalyseOptions.MinPathDist) ? (size * (size - 1) / 2) : size;

            foreach (int key in keyColl)
            {
                if (r.Keys.Contains(key))
                    r[key] += (double)tempDictionary[key] / div;
                else
                    r.Add(key, (double)tempDictionary[key] / div);
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
            if (analyzeOptions[option].useDelta)
                thickening = (int)analyzeOptions[option].optionValue;
            else
                thickening = (int)Math.Ceiling(((analyzeOptions[option].optionValue * r.Count()) / 100));

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

            //m_localMathWaitings[option] = mathWaiting;
            //m_localDispersions[option] = mathWaitingSquare - Math.Pow(mathWaiting, 2);
        }
    }
}