using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Model;

namespace Core
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractNetwork
    {
        protected INetworkGenerator networkGenerator;
        protected INetworkAnalyzer networkAnalyzer;

        protected Dictionary<GenerationParameter, object> generationParameterValues;
        protected AnalyzeOption analyzeOptions;
        protected string tracingPath;

        public AbstractNetwork(Dictionary<GenerationParameter, object> genParams,
            AnalyzeOption analyzeOpts, string trPath)
        {
            generationParameterValues = genParams;
            analyzeOptions = analyzeOpts;
            tracingPath = trPath;
        }

        public void Generate()
        {
            try
            {
                if (generationParameterValues.ContainsKey(GenerationParameter.AdjacencyMatrixFile))
                {
                    // TODO read matrix from file.
                    ArrayList matrix = new ArrayList();
                    networkGenerator.StaticGeneration(matrix);
                }
                else
                {
                    networkGenerator.RandomGeneration(generationParameterValues);
                }
            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Analyze()
        {
            networkAnalyzer.Container = networkGenerator.Container;
            
            // TODO get analyze results.
            try
            {
                if ((analyzeOptions & AnalyzeOption.AvgClusteringCoefficient) == AnalyzeOption.AvgClusteringCoefficient)
                {
                    networkAnalyzer.GetAverageClusteringCoefficient();
                }

                if ((analyzeOptions & AnalyzeOption.AvgDegree) == AnalyzeOption.AvgDegree)
                {
                    networkAnalyzer.GetAverageDegree();
                }

                if ((analyzeOptions & AnalyzeOption.AvgPathLength) == AnalyzeOption.AvgPathLength)
                {
                    networkAnalyzer.GetAveragePath();
                }

                if ((analyzeOptions & AnalyzeOption.ClusteringCoefficientDistribution) == AnalyzeOption.ClusteringCoefficientDistribution)
                {
                    networkAnalyzer.GetClusteringCoefficientDistribution();
                }

                if ((analyzeOptions & AnalyzeOption.CompleteComponentDistribution) == AnalyzeOption.CompleteComponentDistribution)
                {
                    networkAnalyzer.GetCompleteComponentDistribution();
                }

                if ((analyzeOptions & AnalyzeOption.ConnectedComponentDistribution) == AnalyzeOption.ConnectedComponentDistribution)
                {
                    networkAnalyzer.GetConnectedComponentDistribution();
                }

                if ((analyzeOptions & AnalyzeOption.CycleDistribution) == AnalyzeOption.CycleDistribution)
                {
                    // TODO get analyze options parameter values
                    networkAnalyzer.GetCycleDistribution(1, 1);
                }

                if ((analyzeOptions & AnalyzeOption.Cycles3) == AnalyzeOption.Cycles3)
                {
                    networkAnalyzer.GetCycles3();
                }

                if ((analyzeOptions & AnalyzeOption.Cycles3Eigen) == AnalyzeOption.Cycles3Eigen)
                {
                    networkAnalyzer.GetCycles3Eigen();
                }

                if ((analyzeOptions & AnalyzeOption.Cycles4) == AnalyzeOption.Cycles4)
                {
                    networkAnalyzer.GetCycles4();
                }

                if ((analyzeOptions & AnalyzeOption.Cycles4Eigen) == AnalyzeOption.Cycles4Eigen)
                {
                    networkAnalyzer.GetCycles4Eigen();
                }

                if ((analyzeOptions & AnalyzeOption.DegreeDistribution) == AnalyzeOption.DegreeDistribution)
                {
                    networkAnalyzer.GetDegreeDistribution();
                }

                if ((analyzeOptions & AnalyzeOption.Diameter) == AnalyzeOption.Diameter)
                {
                    networkAnalyzer.GetDiameter();
                }

                if ((analyzeOptions & AnalyzeOption.DistanceDistribution) == AnalyzeOption.DistanceDistribution)
                {
                    networkAnalyzer.GetDistanceDistribution();
                }

                if ((analyzeOptions & AnalyzeOption.EigenDistanceDistribution) == AnalyzeOption.EigenDistanceDistribution)
                {
                    networkAnalyzer.GetEigenDistanceDistribution();
                }

                if ((analyzeOptions & AnalyzeOption.EigenValues) == AnalyzeOption.EigenValues)
                {
                    networkAnalyzer.GetEigenValues();
                }

                if ((analyzeOptions & AnalyzeOption.TriangleByVertexDistribution) == AnalyzeOption.TriangleByVertexDistribution)
                {
                    networkAnalyzer.GetAverageClusteringCoefficient();
                }

            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Trace()
        {
        }
    }
}
