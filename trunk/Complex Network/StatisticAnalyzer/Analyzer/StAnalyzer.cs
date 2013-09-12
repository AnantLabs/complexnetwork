using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

using CommonLibrary.Model.Result;
using RandomGraph.Common.Model.Generation;
using CommonLibrary.Model.Attributes;
using RandomGraph.Common.Model;

namespace StatisticAnalyzer.Analyzer
{
    // Статистический анализатор.
    public class StAnalyzer : AbstractStAnalyzer
    {
        // Конструктор, который получает список сборок для анализа.
        public StAnalyzer(List<ResultAssembly> assembly)
            : base(assembly) { }

        protected override bool ContainsOption(AnalyseOptions option)
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
                case AnalyseOptions.TriangleCountByVertex:
                    return assemblyToAnalyze[0].Results[0].TriangleCount.Count != 0;
                case AnalyseOptions.TriangleTrajectory:
                    return assemblyToAnalyze[0].Results[0].TriangleTrajectory.Count != 0;
                default:
                    return assemblyToAnalyze[0].Results[0].Result.Keys.Contains(option);
            }
        }

        protected override void FillGlobalResultCC()
        {
            SortedDictionary<double, double> rValues = new SortedDictionary<double, double>();
            for (int i = 0; i < assemblyToAnalyze.Count; ++i)
            {
                int instanceCount = assemblyToAnalyze[i].Results.Count;
                for (int j = 0; j < instanceCount; ++j)
                {
                    SortedDictionary<double, int>.KeyCollection keyColl =
                                assemblyToAnalyze[i].Results[j].Coefficient.Keys;
                    double sumOfCoeffs = 0;
                    foreach (double key in keyColl)
                    {
                        sumOfCoeffs += key * assemblyToAnalyze[i].Results[j].Coefficient[key];
                    }
                    sumOfCoeffs /= assemblyToAnalyze[i].Results[j].Size;
                    double index = (rValues.Count != 0) ? rValues.Last().Key : 0;
                    rValues.Add(index + 1, sumOfCoeffs);
                }
            }

            result.result.Add(AnalyseOptions.ClusteringCoefficient, GetAverageValuesByDelta(rValues));
            result.resultValues.Add(AnalyseOptions.ClusteringCoefficient, rValues);
        }

        protected override void FillGlobalResultDD()
        {
            SortedDictionary<double, double> rValues = new SortedDictionary<double, double>();
            for (int i = 0; i < assemblyToAnalyze.Count; ++i)
            {
                int instanceCount = assemblyToAnalyze[i].Results.Count;
                for (int j = 0; j < instanceCount; ++j)
                {
                    SortedDictionary<int, int>.KeyCollection keyColl =
                        assemblyToAnalyze[i].Results[j].VertexDegree.Keys;
                    double sumOfDegrees = 0;
                    foreach (int key in keyColl)
                    {
                        sumOfDegrees += key * assemblyToAnalyze[i].Results[j].VertexDegree[key];
                    }
                    sumOfDegrees /= assemblyToAnalyze[i].Results[j].Size;
                    double index = (rValues.Count != 0) ? rValues.Last().Key : 0;
                    rValues.Add(index + 1, sumOfDegrees);
                }
            }

            result.result.Add(AnalyseOptions.DegreeDistribution, GetAverageValuesByDelta(rValues));
            result.resultValues.Add(AnalyseOptions.DegreeDistribution, rValues);
        }

        protected override SortedDictionary<double, double> FillLocalResultCC()
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            for (int i = 0; i < assemblyToAnalyze.Count(); ++i)
            {
                int size, instanceCount = assemblyToAnalyze[i].Results.Count;
                for (int j = 0; j < instanceCount; ++j)
                {
                    size = assemblyToAnalyze[i].Results[j].Size;
                    SortedDictionary<double, int> CCDictionary =
                        assemblyToAnalyze[i].Results[j].Coefficient;
                    SortedDictionary<double, int>.KeyCollection keyColl = CCDictionary.Keys;
                    foreach (double key in keyColl)
                    {
                        if (r.Keys.Contains(key))
                            r[key] += (double)CCDictionary[key] / size;
                        else
                            r.Add(key, (double)CCDictionary[key] / size);
                    }
                }
            }

            return r;
        }

        protected override SortedDictionary<double, double> FillLocalResultEigenDistance()
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            for (int i = 0; i < assemblyToAnalyze.Count(); ++i)
            {
                int instanceCount = assemblyToAnalyze[i].Results.Count;
                for (int j = 0; j < instanceCount; ++j)
                {
                    SortedDictionary<double, int> EDDictionary =
                        assemblyToAnalyze[i].Results[j].DistancesBetweenEigenValues;
                    SortedDictionary<double, int>.KeyCollection keyCol = EDDictionary.Keys;
                    foreach (double key in keyCol)
                        if (r.Keys.Contains(key))
                            r[key] += (double)EDDictionary[key] / EDDictionary.Count;
                        else
                            r.Add(key, (double)EDDictionary[key] / EDDictionary.Count);
                }
            }

            return r;
        }

        protected override void FillExtendedResultTrajectory(UInt32 stepsToRemove)
        {
            result.parameterLine += " StepCount = " +
                assemblyToAnalyze[0].AnalyzeOptionParams[AnalyzeOptionParam.TrajectoryStepCount].ToString() +
                "; k = " + stepsToRemove.ToString() + ";";

            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            for (int i = 0; i < assemblyToAnalyze.Count(); ++i)
            {
                int instanceCount = assemblyToAnalyze[i].Results.Count();
                for (int j = 0; j < instanceCount; ++j)
                {
                    SortedDictionary<int, double> tempDictionary = assemblyToAnalyze[i].Results[j].TriangleTrajectory;
                    SortedDictionary<int, double>.KeyCollection keyColl = tempDictionary.Keys;
                    int limit = 1;
                    int previousKey = -1;
                    foreach (int key in keyColl)
                    {
                        if (limit >= stepsToRemove)
                        {
                            if (key - 1 == previousKey)
                            {
                                if (r.Keys.Contains(key))
                                    r[key] += tempDictionary[key];
                                else
                                    r.Add(key, tempDictionary[key]);
                            }
                            else
                            {
                                for (int tempIndex = previousKey + 1; tempIndex <= key - previousKey; ++tempIndex)
                                {
                                    if (r.Keys.Contains(tempIndex))
                                        r[tempIndex] += tempDictionary[key];
                                    else
                                        r.Add(tempIndex, tempDictionary[key]);
                                }
                            }

                            previousKey = key;
                        }
                        else
                        {
                            ++limit;
                        }
                    }
                }

                SortedDictionary<double, double> res = new SortedDictionary<double, double>();
                SortedDictionary<double, double>.KeyCollection keys = r.Keys;
                foreach (double key in keys)
                {
                    res.Add(key, r[key] / instanceCount);
                }

                SortedDictionary<double, double>.KeyCollection resultKeys = res.Keys;
                double avg = 0, sigma = 0;

                foreach (double key in resultKeys)
                {
                    avg += res[key];
                }
                avg /= resultKeys.Count() - stepsToRemove;

                foreach (double key in resultKeys)
                {
                    sigma += Math.Pow((avg - res[key]), 2);
                }
                sigma /= resultKeys.Count() - stepsToRemove;
                sigma = Math.Sqrt(sigma);

                double mu = Convert.ToDouble(assemblyToAnalyze[i].AnalyzeOptionParams[AnalyzeOptionParam.TrajectoryMu]);
                result.trajectoryAvgs.Add(mu, avg);
                result.trajectorySigmas.Add(mu, sigma);
            }
        }

        protected override SortedDictionary<double, double> FillLocalResultTrajectory()
        {
            result.parameterLine += "Mu = " +
                assemblyToAnalyze[0].AnalyzeOptionParams[AnalyzeOptionParam.TrajectoryMu].ToString() +
                "; StepCount = " +
                assemblyToAnalyze[0].AnalyzeOptionParams[AnalyzeOptionParam.TrajectoryStepCount].ToString() + ";";

            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            for (int i = 0; i < assemblyToAnalyze.Count(); ++i)
            {
                int instanceCount = assemblyToAnalyze[i].Results.Count();
                for (int j = 0; j < instanceCount; ++j)
                {
                    SortedDictionary<int, double> tempDictionary = assemblyToAnalyze[i].Results[j].TriangleTrajectory;
                    SortedDictionary<int, double>.KeyCollection keyColl = tempDictionary.Keys;
                    int previousKey = -1;
                    foreach (int key in keyColl)
                    {
                        if (key - 1 == previousKey)
                        {
                            if (r.Keys.Contains(key))
                                r[key] += tempDictionary[key];
                            else
                                r.Add(key, tempDictionary[key]);
                        }
                        else
                        {
                            for (int tempIndex = previousKey + 1; tempIndex <= key - previousKey; ++tempIndex)
                            {
                                if (r.Keys.Contains(tempIndex))
                                    r[tempIndex] += tempDictionary[key];
                                else
                                    r.Add(tempIndex, tempDictionary[key]);
                            }
                        }

                        previousKey = key;
                    }
                }
            }

            return r;
        }

        protected override SortedDictionary<double, double> FillLocalResult(AnalyseOptions option)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            SortedDictionary<int, int> tempDictionary;
            for (int i = 0; i < assemblyToAnalyze.Count(); ++i)
            {
                int size, instanceCount = assemblyToAnalyze[i].Results.Count();
                for (int j = 0; j < instanceCount; ++j)
                {
                    size = assemblyToAnalyze[i].Results[j].Size;
                    switch (option)
                    {
                        case AnalyseOptions.DegreeDistribution:
                            {
                                tempDictionary = assemblyToAnalyze[i].Results[j].VertexDegree;
                                break;
                            }
                        case AnalyseOptions.ConnSubGraph:
                            {
                                tempDictionary = assemblyToAnalyze[i].Results[j].Subgraphs;
                                break;
                            }
                        case AnalyseOptions.MinPathDist:
                            {
                                tempDictionary = assemblyToAnalyze[i].Results[j].DistanceBetweenVertices;
                                break;
                            }
                        case AnalyseOptions.TriangleCountByVertex:
                            {
                                tempDictionary = new SortedDictionary<int, int>(); //assemblyToAnalyze[i].Results[j].TriangleCount;
                                break;
                            }
                        default:
                            {
                                throw (new SystemException("Unknown analyze option."));
                            }
                    }

                    SortedDictionary<int, int>.KeyCollection keyColl = tempDictionary.Keys;
                    int div = (option == AnalyseOptions.MinPathDist) ? (size * (size - 1) / 2) : size;
                    foreach (int key in keyColl)
                    {
                        if (r.Keys.Contains(key))
                            r[key] += (double)tempDictionary[key] / div;
                        else
                            r.Add(key, (double)tempDictionary[key] / div);
                    }
                }
            }

            return r;
        }
    }
}
