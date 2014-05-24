using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonLibrary.Model.Result;
using RandomGraph.Common.Model.Generation;
using CommonLibrary.Model.Attributes;
using RandomGraph.Common.Model;

namespace StatisticAnalyzer.Analyzer
{
    public abstract class AbstractStAnalyzer
    {
        // Список сборок для анализа. Число сборок больше 1, если анализ по параметрам и byAllJobs.
        protected List<ResultAssembly> assemblyToAnalyze;
        // Дополнительные параметры анализа для каждого свойства (только для локального анализа).
        protected Dictionary<AnalyseOptions, StAnalyzeOptions> analyzeOptions;
        // Свойства, которые нужно анализировать.
        public AnalyseOptions options = AnalyseOptions.None;

        // Результат статистического анализа.
        protected StAnalyzeResult result;

        // Конструктор, который получает список сборок для анализа.
        public AbstractStAnalyzer(List<ResultAssembly> assembly)
        {
            if (assembly.Count != 0)
            {
                assemblyToAnalyze = assembly;
                analyzeOptions = new Dictionary<AnalyseOptions, StAnalyzeOptions>();
                result = new StAnalyzeResult();

                result.modelName = assemblyToAnalyze[0].ModelType.Name;
                result.networkSize = assemblyToAnalyze[0].Results[0].Size;
                result.parameterLine = GetParameterLine();
                result.realizationsCount = GetRealizationsCount();
            }
            else
                throw new SystemException("There are no assemblies.");
        }

        // Свойства.

        public Dictionary<AnalyseOptions, StAnalyzeOptions> AnalyzeOptions
        {
            set
            {
                analyzeOptions = value;
                result.options = value;
            }
        }

        public StAnalyzeResult Result
        {
            get { return result; }
        }

        // Открытая часть статистического анализатора.

        // Глобальный анализ.
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
            if ((options & AnalyseOptions.Cycles5) == AnalyseOptions.Cycles5)
                GlobalAnalyzeByOption(AnalyseOptions.Cycles5);
            if ((options & AnalyseOptions.MaxFullSubgraph) == AnalyseOptions.MaxFullSubgraph)
                GlobalAnalyzeByOption(AnalyseOptions.MaxFullSubgraph);
            if ((options & AnalyseOptions.LargestConnectedComponent) == AnalyseOptions.LargestConnectedComponent)
                GlobalAnalyzeByOption(AnalyseOptions.LargestConnectedComponent);
            if ((options & AnalyseOptions.MinEigenValue) == AnalyseOptions.MinEigenValue)
                GlobalAnalyzeByOption(AnalyseOptions.MinEigenValue);
            if ((options & AnalyseOptions.MaxEigenValue) == AnalyseOptions.MaxEigenValue)
                GlobalAnalyzeByOption(AnalyseOptions.MaxEigenValue);
        }

        // Локальный анализ.
        public void LocalAnalyze()
        {
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
            if ((options & AnalyseOptions.TriangleCountByVertex) == AnalyseOptions.TriangleCountByVertex)
                LocalAnalyzeByOption(AnalyseOptions.TriangleCountByVertex);
            if ((options & AnalyseOptions.TriangleTrajectory) == AnalyseOptions.TriangleTrajectory)
                LocalAnalyzeByOption(AnalyseOptions.TriangleTrajectory);
        }

        public void ExtendedAnalyze(UInt32 stepsToRemove)
        {
            if ((options & AnalyseOptions.TriangleTrajectory) == AnalyseOptions.TriangleTrajectory)
                ExtendedAnalyzeByOption(AnalyseOptions.TriangleTrajectory, stepsToRemove);
        }

        // Utilities //

        private string GetParameterLine()
        {
            string line = "";

            Dictionary<GenerationParam, object> genParams = assemblyToAnalyze[0].GenerationParams;
            Dictionary<GenerationParam, object>.KeyCollection keys = genParams.Keys;
            foreach (GenerationParam g in keys)
            {
                GenerationParamInfo paramInfo =
                    (GenerationParamInfo)(g.GetType().GetField(g.ToString()).
                    GetCustomAttributes(typeof(GenerationParamInfo), false)[0]);
                line += paramInfo.Name += " = ";
                line += genParams[g].ToString() + "; ";
            }

            return line;
        }

        private int GetRealizationsCount()
        {
            int sum = 0;
            for (int i = 0; i < assemblyToAnalyze.Count; ++i)
            {
                sum += assemblyToAnalyze[i].Results.Count;
            }

            return sum;
        }

        private void GlobalAnalyzeByOption(AnalyseOptions option)
        {
            if (ContainsOption(option))
            {
                switch (option)
                {
                    case AnalyseOptions.ClusteringCoefficient:
                        {
                            FillGlobalResultCC();
                            break;
                        }
                    case AnalyseOptions.DegreeDistribution:
                        {
                            FillGlobalResultDD();
                            break;
                        }
                    default:
                        {
                            FillGlobalResult(option);
                            break;
                        }
                }

                result.resultAvgValues.Add(option, GetGlobalAverage(result.result[option]));
            }
        }

        private void LocalAnalyzeByOption(AnalyseOptions option)
        {
            if (ContainsOption(option))
            {
                SortedDictionary<double, double> tempResult;
                switch (option)
                {
                    case AnalyseOptions.ClusteringCoefficient:
                        {
                            tempResult = FillLocalResultCC();
                            break;
                        }
                    case AnalyseOptions.EigenValue:
                        {
                            tempResult = FillLocalResultEigen();
                            break;
                        }
                    case AnalyseOptions.DistEigenPath:
                        {
                            tempResult = FillLocalResultEigenDistance();
                            break;
                        }
                    case AnalyseOptions.Cycles:
                        {
                            tempResult = new SortedDictionary<double, double>(); //FillLocalResultCycles();
                            break;
                        }
                    case AnalyseOptions.TriangleTrajectory:
                        {
                            tempResult = FillLocalResultTrajectory();
                            break;
                        }
                    default:
                        {
                            tempResult = FillLocalResult(option);
                            break;
                        }
                }

                result.result.Add(option, GetLocalResult(option, tempResult));
            }
        }

        private void ExtendedAnalyzeByOption(AnalyseOptions option, UInt32 stepsToRemove)
        {
            if (ContainsOption(option))
            {
                switch (option)
                {
                    case AnalyseOptions.TriangleTrajectory:
                        {
                            FillExtendedResultTrajectory(stepsToRemove);
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private void FillGlobalResult(AnalyseOptions option)
        {
            SortedDictionary<double, double> rValues = new SortedDictionary<double, double>();
            for (int i = 0; i < assemblyToAnalyze.Count; ++i)
            {
                int instanceCount = assemblyToAnalyze[i].Results.Count;
                for (int j = 0; j < instanceCount; ++j)
                {
                    double index = (rValues.Count != 0) ? rValues.Last().Key : 0;
                    rValues.Add(index + 1, assemblyToAnalyze[i].Results[j].Result[option]);
                }
            }

            result.result.Add(option, GetAverageValuesByDelta(rValues));
            result.resultValues.Add(option, rValues);
        }

        protected SortedDictionary<double, double> GetAverageValuesByDelta(SortedDictionary<double, double> d)
        {
            SortedDictionary<double, double> res = new SortedDictionary<double, double>();
            int delta = (int)Math.Ceiling((double)d.Count / 10), step = delta;
            while (step <= d.Count)
            {
                double sum = 0;
                int index = 0;
                SortedDictionary<double, double>.KeyCollection keys = d.Keys;
                foreach (double k in keys)
                {
                    if (index < step)
                    {
                        sum += d[k];
                        ++index;
                    }
                    else
                        break;
                }

                res.Add(step, sum / step);
                step += delta;
            }

            if (d.Count % delta != 0)
            {
                double sum = 0;
                SortedDictionary<double, double>.KeyCollection keys = d.Keys;
                foreach (double k in keys)
                {
                    sum += d[k];
                }
                res.Add(d.Count, sum / d.Count);
            }

            return res;
        }

        private double GetGlobalAverage(SortedDictionary<double, double> resultDictionary)
        {
            if (resultDictionary.Count != 0)
                return resultDictionary.Last().Value;
            else
                return 0;
        }

        // ??
        private SortedDictionary<double, double> FillLocalResultEigen()
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            ArrayList l = assemblyToAnalyze[0].Results[0].EigenVector;
            l.Sort();
            for (int k = 0; k < l.Count; ++k)
            {
                r.Add(k, (double)l[k]);
            }

            return r;
        }

        /*private SortedDictionary<double, double> FillLocalResultCycles()
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            for (int i = 0; i < assemblyToAnalyze.Count(); ++i)
            {
                int size = assemblyToAnalyze[i].Size;
                int instanceCount = assemblyToAnalyze[i].Results.Count();
                for (int j = 0; j < instanceCount; ++j)
                {
                    SortedDictionary<int, long> tempDictionary = assemblyToAnalyze[i].Results[j].Cycles;
                    SortedDictionary<int, long>.KeyCollection keyColl = tempDictionary.Keys;
                    foreach (int key in keyColl)
                    {
                        if (r.Keys.Contains(key))
                            r[key] += (double)tempDictionary[key] / size;
                        else
                            r.Add(key, (double)tempDictionary[key] / size);
                    }
                }
            }

            return r;
        }*/

        private SortedDictionary<double, double> GetLocalResult(AnalyseOptions option,
            SortedDictionary<double, double> t)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            SortedDictionary<double, double>.KeyCollection keys = t.Keys;
            foreach (double key in keys)
            {
                r.Add(key, t[key] / GetRealizationsCount());
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
            switch (option)
            {
                case AnalyseOptions.TriangleTrajectory:
                    {
                        double avg = 0, sigma = 0;

                        foreach (double key in keys)
                        {
                            avg += r[key];
                        }
                        avg /= keys.Count();

                        foreach (double key in keys)
                        {
                            sigma += Math.Pow((avg - r[key]), 2);
                        }
                        sigma /= keys.Count();
                        sigma = Math.Sqrt(sigma);

                        result.resultMathWaitings.Add(option, avg);
                        result.resultDispersions.Add(option, sigma);

                        break;
                    }
                case AnalyseOptions.EigenValue:
                case AnalyseOptions.Cycles:
                case AnalyseOptions.TriangleCountByVertex:
                    {
                        break;
                    }
                default:
                    {
                        double mathWaiting = 0, mathWaitingSquare = 0;

                        foreach (double key in keys)
                        {
                            mathWaiting += key * r[key];
                            mathWaitingSquare += Math.Pow(key, 2) * r[key];
                        }

                        result.resultMathWaitings.Add(option, mathWaiting);
                        result.resultDispersions.Add(option, (mathWaitingSquare - Math.Pow(mathWaiting, 2)));

                        break;
                    }
            }
        }

        protected abstract bool ContainsOption(AnalyseOptions option);
        protected abstract void FillGlobalResultCC();
        protected abstract void FillGlobalResultDD();
        protected abstract SortedDictionary<double, double> FillLocalResultCC();
        protected abstract SortedDictionary<double, double> FillLocalResultEigenDistance();
        protected abstract SortedDictionary<double, double> FillLocalResultTrajectory();
        protected abstract SortedDictionary<double, double> FillLocalResult(AnalyseOptions option);
        protected abstract void FillExtendedResultTrajectory(UInt32 stepsToRemove);

    }
}