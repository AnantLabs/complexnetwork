using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

using Core.Model;
using Core.Enumerations;

namespace NetworkModel
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractNetworkAnalyzer : INetworkAnalyzer
    {
        public abstract INetworkContainer Container { get; set; }

        public Object CalculateOption(AnalyzeOption option)
        {
            switch (option)
            {
                case AnalyzeOption.AvgClusteringCoefficient:
                    return CalculateAverageClusteringCoefficient();
                case AnalyzeOption.AvgDegree:
                    return CalculateAverageDegree();
                case AnalyzeOption.AvgPathLength:
                    return CalculateAveragePath();
                case AnalyzeOption.ClusteringCoefficientDistribution:
                    return CalculateClusteringCoefficientDistribution();
                case AnalyzeOption.CompleteComponentDistribution:
                    return CalculateCompleteComponentDistribution();
                case AnalyzeOption.ConnectedComponentDistribution:
                    return CalculateConnectedComponentDistribution();
                case AnalyzeOption.CycleDistribution:
                    // TODO
                    return CalculateCycleDistribution(1, 1);
                case AnalyzeOption.Cycles3:
                    return CalculateCycles3();
                case AnalyzeOption.Cycles3Eigen:
                    return CalculateCycles3Eigen();
                case AnalyzeOption.Cycles4:
                    return CalculateCycles4();
                case AnalyzeOption.Cycles4Eigen:
                    return CalculateCycles4Eigen();
                case AnalyzeOption.DegreeDistribution:
                    return CalculateDegreeDistribution();
                case AnalyzeOption.Diameter:
                    return CalculateDiameter();
                case AnalyzeOption.DistanceDistribution:
                    return CalculateDistanceDistribution();
                case AnalyzeOption.EigenDistanceDistribution:
                    return CalculateEigenDistanceDistribution();
                case AnalyzeOption.EigenValues:
                    return CalculateEigenValues();
                case AnalyzeOption.TriangleByVertexDistribution:
                    return CalculateTriangleByVertexDistribution();
                default:
                    return null;
            }
        }

        /// <summary>
        /// Calculates the average path length of the network.
        /// </summary>
        /// <returns>Average path length.</returns>
        protected virtual Double CalculateAveragePath()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the diameter of the network.
        /// </summary>
        /// <returns>Diameter.</returns>
        protected virtual UInt32 CalculateDiameter()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the average value of vertex degrees in the network.
        /// </summary>
        /// <returns>Average degree.</returns>
        protected virtual Double CalculateAverageDegree()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the average value of vertex clustering coefficients in the network.  
        /// </summary>
        /// <returns>Average clustering coefficient.</returns>
        protected virtual Double CalculateAverageClusteringCoefficient()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the number of cycles of length 3 in the network.
        /// </summary>
        /// <returns>Number of cycles 3.</returns>
        protected virtual BigInteger CalculateCycles3()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the number of cycles of length 4 in the network.
        /// </summary>
        /// <returns>Number of cycles 4.</returns>
        protected virtual BigInteger CalculateCycles4()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the eigenvalues of adjacency matrix of the network.
        /// </summary>
        /// <returns>List of eigenvalues.</returns>
        protected virtual List<Double> CalculateEigenValues()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the number of cycles of length 3 in the network using eigenvalues.
        /// </summary>
        /// <returns>Number of cycles 3.</returns>
        protected virtual BigInteger CalculateCycles3Eigen()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the number of cycles of length 4 in the network using eigenvalues.
        /// </summary>
        /// <returns>Number of cycles 4.</returns>
        protected virtual BigInteger CalculateCycles4Eigen()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates distances between eigenvalues.
        /// </summary>
        /// <returns>Distribution of distances between eigenvalues.</returns>
        protected virtual SortedDictionary<Double, Int32> CalculateEigenDistanceDistribution()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates degrees of vertices in the network.
        /// </summary>
        /// <returns>Degree distribution.</returns>
        protected virtual SortedDictionary<UInt32, UInt32> CalculateDegreeDistribution()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates clustering coefficients of vertices in the network.
        /// </summary>
        /// <returns>Distribution of clustering coefficinets.</returns>
        protected virtual SortedDictionary<Double, UInt32> CalculateClusteringCoefficientDistribution()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates counts of connected components in the network.
        /// </summary>
        /// <returns>Distribution of connected components.</returns>
        protected virtual SortedDictionary<UInt32, UInt32> CalculateConnectedComponentDistribution()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates counts of complete components in the network.
        /// </summary>
        /// <returns>Distribution of complete components.</returns>
        protected virtual SortedDictionary<UInt32, UInt32> CalculateCompleteComponentDistribution()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates minimal path lengths in the network.
        /// </summary>
        /// <returns>Distribution of minimal path lengths.</returns>
        protected virtual SortedDictionary<UInt32, UInt32> CalculateDistanceDistribution()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates counts of triangles by vertices in the network.
        /// </summary>
        /// <returns>Distribution of triangles by vertex.</returns>
        protected virtual SortedDictionary<UInt32, UInt32> CalculateTriangleByVertexDistribution()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the counts of cycles in the network.
        /// </summary>
        /// <param name="lowBound">Minimal length of cycle.</param>
        /// <param name="hightBound">Maximal length of cycle.</param>
        /// <returns>Distribution of cycles lengths.</returns>
        protected virtual SortedDictionary<UInt16, BigInteger> CalculateCycleDistribution(UInt16 lowBound, UInt16 hightBound)
        {
            throw new NotImplementedException();
        }
    }
}
