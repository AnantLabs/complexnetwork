using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonLibrary.Model.Result;
using RandomGraph.Common.Model;

using StatisticAnalyzer.Loader;

namespace StatisticAnalyzer.Analyzer
{
    public class StAnalyzerDB : AbstractStAnalyzer
    {
        public StAnalyzerDB(List<ResultAssembly> assembly)
            : base(assembly) { }

        protected override bool ContainsOption(RandomGraph.Common.Model.AnalyseOptions option)
        {
            switch (option)
            {
                case AnalyseOptions.ClusteringCoefficient:
                    return assemblyToAnalyze[0].CoefficientsLocal.Keys.Count != 0;
                case AnalyseOptions.DegreeDistribution:
                    return assemblyToAnalyze[0].VertexDegreeLocal.Keys.Count != 0;
                case AnalyseOptions.ConnSubGraph:
                    return assemblyToAnalyze[0].SubgraphsLocal.Keys.Count != 0;
                case AnalyseOptions.MinPathDist:
                    return assemblyToAnalyze[0].DistanceBetweenVerticesLocal.Count != 0;
                case AnalyseOptions.EigenValue:
                    return assemblyToAnalyze[0].Results[0].EigenVector.Count != 0;
                case AnalyseOptions.DistEigenPath:
                    return assemblyToAnalyze[0].DistancesBetweenEigenValuesLocal.Count != 0;
                case AnalyseOptions.Cycles:
                    return assemblyToAnalyze[0].Results[0].Cycles.Count != 0;
                case AnalyseOptions.TriangleCountByVertex:
                    return assemblyToAnalyze[0].Results[0].TriangleCount.Count != 0;
                case AnalyseOptions.TriangleTrajectory:
                    return assemblyToAnalyze[0].TriangleTrajectoryLocal.Count != 0;
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
                    double index = (rValues.Count != 0) ? rValues.Last().Key : 0;
                    rValues.Add(index + 1, assemblyToAnalyze[i].Results[j].CoefficientGlobal);
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
                    double index = (rValues.Count != 0) ? rValues.Last().Key : 0;
                    rValues.Add(index + 1, assemblyToAnalyze[i].Results[j].DegreeGlobal);
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
                int instanceCount = assemblyToAnalyze[i].Results.Count;
                SortedDictionary<double, double> CCDictionary = assemblyToAnalyze[i].CoefficientsLocal;
                SortedDictionary<double, double>.KeyCollection keyColl = CCDictionary.Keys;
                foreach (double key in keyColl)
                {
                    if (r.Keys.Contains(key))
                        r[key] += CCDictionary[key] * instanceCount;
                    else
                        r.Add(key, CCDictionary[key] * instanceCount);
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
                SortedDictionary<double, double> EDDictionary = assemblyToAnalyze[i].DistancesBetweenEigenValuesLocal;
                SortedDictionary<double, double>.KeyCollection keyColl = EDDictionary.Keys;
                foreach (double key in keyColl)
                {
                    if (r.Keys.Contains(key))
                        r[key] += EDDictionary[key] * instanceCount;
                    else
                        r.Add(key, EDDictionary[key] * instanceCount);
                }
            }

            return r;
        }

        protected override SortedDictionary<double, double> FillLocalResultTrajectory()
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            for (int i = 0; i < assemblyToAnalyze.Count(); ++i)
            {
                int instanceCount = assemblyToAnalyze[i].Results.Count;
                SortedDictionary<double, double> TTDictionary = assemblyToAnalyze[i].TriangleTrajectoryLocal;
                SortedDictionary<double, double>.KeyCollection keyColl = TTDictionary.Keys;
                foreach (double key in keyColl)
                {
                    if (r.Keys.Contains(key))
                        r[key] += TTDictionary[key] * instanceCount;
                    else
                        r.Add(key, TTDictionary[key] * instanceCount);
                }
            }

            return r;
        }

        protected override void FillExtendedResultTrajectory(uint stepsToRemove)
        {
            result.parameterLine += " StepCount = " +
                assemblyToAnalyze[0].AnalyzeOptionParams[AnalyzeOptionParam.TrajectoryStepCount].ToString() +
                "; k = " + stepsToRemove.ToString() + ";";

            StLoaderDB loader = new StLoaderDB();
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            double avg = 0, sigma = 0;
            for (int i = 0; i < assemblyToAnalyze.Count(); ++i)
            {
                loader.GetAvgsSigmas(assemblyToAnalyze[i].ID, (int)stepsToRemove, out avg, out sigma);
                sigma = Math.Sqrt(sigma);

                double mu = Convert.ToDouble(assemblyToAnalyze[i].AnalyzeOptionParams[AnalyzeOptionParam.TrajectoryMu]);
                result.trajectoryAvgs.Add(mu, avg);
                result.trajectorySigmas.Add(mu, sigma);
            }
        }

        protected override SortedDictionary<double, double> FillLocalResult(AnalyseOptions option)
        {
            SortedDictionary<double, double> r = new SortedDictionary<double, double>();
            SortedDictionary<double, double> tempDictionary;
            for (int i = 0; i < assemblyToAnalyze.Count(); ++i)
            {
                int instanceCount = assemblyToAnalyze[i].Results.Count();
                switch (option)
                {
                    case AnalyseOptions.DegreeDistribution:
                        {
                            tempDictionary = assemblyToAnalyze[i].VertexDegreeLocal;
                            break;
                        }
                    case AnalyseOptions.ConnSubGraph:
                        {
                            tempDictionary = assemblyToAnalyze[i].SubgraphsLocal;
                            break;
                        }
                    case AnalyseOptions.MinPathDist:
                        {
                            tempDictionary = assemblyToAnalyze[i].DistanceBetweenVerticesLocal;
                            break;
                        }
                    case AnalyseOptions.TriangleCountByVertex:
                        {
                            tempDictionary = new SortedDictionary<double, double>();
                            break;
                        }
                    default:
                        {
                            throw (new SystemException("Unknown analyze option."));
                        }
                }

                SortedDictionary<double, double>.KeyCollection keyColl = tempDictionary.Keys;
                foreach (int key in keyColl)
                {
                    if (r.Keys.Contains(key))
                        r[key] += tempDictionary[key] * instanceCount;
                    else
                        r.Add(key, tempDictionary[key] * instanceCount);
                }
            }

            return r;
        }
    }
}
