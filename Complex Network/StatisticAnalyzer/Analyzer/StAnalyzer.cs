using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonLibrary.Model.Result;

using RandomGraph.Common.Model;

namespace StatisticAnalyzer.Analyzer
{
    public class StAnalyzer
    {
        private List<ResultAssembly> assemblyToAnalyze;
        private Dictionary<AnalyseOptions, StAnalyzeOptions> analyzeOptions;
        private StAnalyzeResult result;

        public AnalyseOptions options = AnalyseOptions.None;

        public StAnalyzer(List<ResultAssembly> assembly)
        {
            if (assembly.Count != 0)
            {
                assemblyToAnalyze = assembly;
                analyzeOptions = new Dictionary<AnalyseOptions, StAnalyzeOptions>();
                result = new StAnalyzeResult();
            }
            else
                throw new SystemException("There are no assemblies.");
        }

        public Dictionary<AnalyseOptions, StAnalyzeOptions> AnalyzeOptions
        {
            set { analyzeOptions = value; }
        }

        public StAnalyzeResult Result
        {
            get { return result; }
        }

        public void GlobalAnalyze()
        {
            if ((options & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath)
                GlobalAnalyzeByOption(AnalyseOptions.AveragePath);
            if ((options & AnalyseOptions.Diameter) == AnalyseOptions.Diameter)
                GlobalAnalyzeByOption(AnalyseOptions.Diameter);
            if ((options & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient)
                GlobalAnalyzeByOption(AnalyseOptions.ClusteringCoefficient);
            if ((options & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution)
                GlobalAnalyzeByOption(AnalyseOptions.DegreeDistribution);
            if ((options & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3)
                GlobalAnalyzeByOption(AnalyseOptions.Cycles3);
            if ((options & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4)
                GlobalAnalyzeByOption(AnalyseOptions.Cycles4);
            if ((options & AnalyseOptions.MaxFullSubgraph) == AnalyseOptions.MaxFullSubgraph)
                GlobalAnalyzeByOption(AnalyseOptions.MaxFullSubgraph);
            if ((options & AnalyseOptions.LargestConnectedComponent) == AnalyseOptions.LargestConnectedComponent)
                GlobalAnalyzeByOption(AnalyseOptions.LargestConnectedComponent);
            if ((options & AnalyseOptions.MinEigenValue) == AnalyseOptions.MinEigenValue)
                GlobalAnalyzeByOption(AnalyseOptions.MinEigenValue);
            if ((options & AnalyseOptions.MaxEigenValue) == AnalyseOptions.MaxEigenValue)
                GlobalAnalyzeByOption(AnalyseOptions.MaxEigenValue);
        }

        public void LocalAnalyze()
        {
            if ((options & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient)
                LocalAnalyzeByOption(AnalyseOptions.ClusteringCoefficient);
            if ((options & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution)
                LocalAnalyzeByOption(AnalyseOptions.DegreeDistribution);
            if ((options & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient)
                LocalAnalyzeByOption(AnalyseOptions.ClusteringCoefficient);
            if ((options & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution)
                LocalAnalyzeByOption(AnalyseOptions.DegreeDistribution);
            if ((options & AnalyseOptions.ConnSubGraph) == AnalyseOptions.ConnSubGraph)
                LocalAnalyzeByOption(AnalyseOptions.ConnSubGraph);
            if ((options & AnalyseOptions.MinPathDist) == AnalyseOptions.MinPathDist)
                LocalAnalyzeByOption(AnalyseOptions.MinPathDist);
            if ((options & AnalyseOptions.EigenValue) == AnalyseOptions.EigenValue)
                LocalAnalyzeByOption(AnalyseOptions.EigenValue);
            if ((options & AnalyseOptions.DistEigenPath) == AnalyseOptions.DistEigenPath)
                LocalAnalyzeByOption(AnalyseOptions.DistEigenPath);
            if ((options & AnalyseOptions.Cycles) == AnalyseOptions.Cycles)
                LocalAnalyzeByOption(AnalyseOptions.Cycles);
        }

        // Utilities //

        private void GlobalAnalyzeByOption(AnalyseOptions option)
        {
            if (ContainsOption(option))
            {
                SortedDictionary<double, double> resultDictionary = new SortedDictionary<double, double>();
                int instanceCount = assemblyToAnalyze[0].Results.Count, deltaI = 10, I = deltaI;
                resultDictionary = GlobalCases(option, I, deltaI, instanceCount, 0);
                result.result.Add(option, resultDictionary);
                result.resultAvgValues.Add(option, GetGlobalAverage(instanceCount, resultDictionary));
            }
        }
        
        private void LocalAnalyzeByOption(AnalyseOptions option)
        {
            if (ContainsOption(option))
            {
                SortedDictionary<double, double> resultDictionary = new SortedDictionary<double, double>();
                SortedDictionary<double, double> tempResult = new SortedDictionary<double, double>();
                int instanceCount = assemblyToAnalyze[0].Results.Count;
                for (int i = 0; i < instanceCount; ++i)
                {
                    tempResult = LocalCases(option, i, 0);
                }

                result.result.Add(option, GetLocalResult(option, tempResult, instanceCount));
            }
        }
        
        private bool ContainsOption(AnalyseOptions option)
        {
            switch (option)
            {
                case AnalyseOptions.ClusteringCoefficient:
                    return assemblyToAnalyze[0].Results[0].Coefficient.Keys.Count != 0;
                case AnalyseOptions.DegreeDistribution:
                    return assemblyToAnalyze[0].Results[0].VertexDegree.Keys.Count != 0;
                case AnalyseOptions.ConnSubGraph:
                    return assemblyToAnalyze[0].Results[0].Subgraphs.Keys.Count != 0;
                case AnalyseOptions.MinPathDist:
                    return assemblyToAnalyze[0].Results[0].DistanceBetweenVertices.Count != 0;
                case AnalyseOptions.EigenValue:
                    return assemblyToAnalyze[0].Results[0].EigenVector.Count != 0;
                case AnalyseOptions.DistEigenPath:
                    return assemblyToAnalyze[0].Results[0].DistancesBetweenEigenValues.Count != 0;
                case AnalyseOptions.Cycles:
                    return assemblyToAnalyze[0].Results[0].Cycles.Count != 0;
                default:
                    return assemblyToAnalyze[0].Results[0].Result.Keys.Contains(option);
            }
        }

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
            if (instanceCount <= 10 && assemblyToAnalyze.Count ==1)
            {
                for (int i = 0; i < instanceCount; ++i)
                {
                    SortedDictionary<double, int>.KeyCollection keyColl = assemblyToAnalyze[0].Results[i].Coefficient.Keys;
                    double sumOfCoeffs = 0;
                    foreach (double key in keyColl)
                    {
                        sumOfCoeffs += key * assemblyToAnalyze[0].Results[i].Coefficient[key];
                    }
                    sumOfCoeffs /= assemblyToAnalyze[0].Size;
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
                        SortedDictionary<double, int>.KeyCollection keyColl = assemblyToAnalyze[0].Results[i].Coefficient.Keys;
                        double sumOfCoeffs = 0;
                        foreach (double key in keyColl)
                        {
                            sumOfCoeffs += key * assemblyToAnalyze[0].Results[i].Coefficient[key];
                        }
                        sumOfCoeffs /= assemblyToAnalyze[0].Size;

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
            if (instanceCount <= 10 && assemblyToAnalyze.Count == 1)
            {
                for (int i = 0; i < instanceCount; ++i)
                {
                    SortedDictionary<int, int>.KeyCollection keyColl = assemblyToAnalyze[0].Results[i].VertexDegree.Keys;
                    double sumOfDegrees = 0;
                    foreach (int key in keyColl)
                    {
                        sumOfDegrees += key * assemblyToAnalyze[0].Results[i].VertexDegree[key];
                    }
                    sumOfDegrees /= assemblyToAnalyze[0].Size;
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
                        SortedDictionary<int, int>.KeyCollection keyColl = assemblyToAnalyze[0].Results[i].VertexDegree.Keys;
                        double sumOfDegrees = 0;
                        foreach (int key in keyColl)
                        {
                            sumOfDegrees += key * assemblyToAnalyze[0].Results[i].VertexDegree[key];
                        }
                        sumOfDegrees /= assemblyToAnalyze[0].Size;

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
            int iCount = assemblyToAnalyze[0].Results.Count;
            if (iCount <= 10 && assemblyToAnalyze.Count == 1)
            {
                for (int i = 0; i < iCount; ++i)
                {
                    r.Add(i + 1, assemblyToAnalyze[0].Results[i].Result[option]);
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
                        sum += assemblyToAnalyze[0].Results[i].Result[option];
                    r.Add(I, sum / I);
                    I += deltaI;
                }
            }
            return r;
        }

        protected double GetGlobalAverage(int instanceCount, SortedDictionary<double, double> resultDictionary)
        {
            if (instanceCount <= 10 && assemblyToAnalyze.Count == 1)
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
            SortedDictionary<double, int> CCDictionary = assemblyToAnalyze[0].Results[i - initialInstance].Coefficient;
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
            ArrayList l = assemblyToAnalyze[0].Results[i].EigenVector;
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
            SortedDictionary<double, int> EDDictionary = assemblyToAnalyze[0].Results[i - initialInstance].DistancesBetweenEigenValues;
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
                        tempDictionary = assemblyToAnalyze[0].Results[i - initialInstance].VertexDegree;
                        break;
                    }
                case AnalyseOptions.ConnSubGraph:
                    {
                        tempDictionary = assemblyToAnalyze[0].Results[i - initialInstance].Subgraphs;
                        break;
                    }
                case AnalyseOptions.MinPathDist:
                    {
                        tempDictionary = assemblyToAnalyze[0].Results[i - initialInstance].DistanceBetweenVertices;
                        break;
                    }
                case AnalyseOptions.Cycles:
                    {
                        //tempDictionary = assemblyToAnalyze[0].Results[i - initialInstance].Cycles;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            SortedDictionary<int, int>.KeyCollection keyColl = tempDictionary.Keys;
            int size = assemblyToAnalyze[0].Size;
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

            //result.resultMathWaitings.Add(option, mathWaiting);
            //result.resultDispersions.Add(option, (mathWaitingSquare - Math.Pow(mathWaiting, 2)));
        }
    }
}
