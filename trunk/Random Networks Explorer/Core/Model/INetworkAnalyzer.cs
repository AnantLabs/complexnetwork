using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Core.Model
{
    /// <summary>
    /// Interface for analyzer of a random network.
    /// </summary>
    public interface INetworkAnalyzer
    {
        /// <summary>
        /// Container of the generated graph.
        /// </summary>
        INetworkContainer Container { get; set; }

        /// <summary>
        /// Calculates the average path length of the network.
        /// </summary>
        /// <returns>Average path length.</returns>
        Double GetAveragePath();

        /// <summary>
        /// Calculates the diameter of the network.
        /// </summary>
        /// <returns>Diameter.</returns>
        Int32 GetDiameter();

        /// <summary>
        /// Calculates the average value of vertex degrees in the network.
        /// </summary>
        /// <returns>Average degree.</returns>
        Double GetAverageDegree();

        /// <summary>
        /// Calculates the average value of vertex clustering coefficients in the network.  
        /// </summary>
        /// <returns>Average clustering coefficient.</returns>
        Double GetAverageClusteringCoefficient();

        /// <summary>
        /// Calculates the number of cycles of length 3 in the network.
        /// </summary>
        /// <returns>Number of cycles 3.</returns>
        BigInteger GetCycles3();

        /// <summary>
        /// Calculates the number of cycles of length 4 in the network.
        /// </summary>
        /// <returns>Number of cycles 4.</returns>
        BigInteger GetCycles4();

        /// <summary>
        /// Calculates the eigenvalues of adjacency matrix of the network.
        /// </summary>
        /// <returns>List of eigenvalues.</returns>
        List<Double> GetEigenValues();

        /// <summary>
        /// Calculates the number of cycles of length 3 in the network using eigenvalues.
        /// </summary>
        /// <returns>Number of cycles 3.</returns>
        BigInteger GetCycles3Eigen();

        /// <summary>
        /// Calculates the number of cycles of length 4 in the network using eigenvalues.
        /// </summary>
        /// <returns>Number of cycles 4.</returns>
        BigInteger GetCycles4Eigen();

        /// <summary>
        /// Calculates distances between eigenvalues.
        /// </summary>
        /// <returns>Distribution of distances between eigenvalues.</returns>
        SortedDictionary<Double, Int32> GetEigenDistanceDistribution();

        /// <summary>
        /// Calculates degrees of vertices in the network.
        /// </summary>
        /// <returns>Degree distribution.</returns>
        SortedDictionary<Int32, Int32> GetDegreeDistribution();

        /// <summary>
        /// Calculates clustering coefficients of vertices in the network.
        /// </summary>
        /// <returns>Distribution of clustering coefficinets.</returns>
        SortedDictionary<Double, Int32> GetClusteringCoefficientDistribution();

        /// <summary>
        /// Calculates counts of connected components in the network.
        /// </summary>
        /// <returns>Distribution of connected components.</returns>
        SortedDictionary<Int32, Int32> GetConnectedComponentDistribution();

        /// <summary>
        /// Calculates counts of complete components in the network.
        /// </summary>
        /// <returns>Distribution of complete components.</returns>
        SortedDictionary<Int32, Int32> GetCompleteComponentDistribution();

        /// <summary>
        /// Calculates minimal path lengths in the network.
        /// </summary>
        /// <returns>Distribution of minimal path lengths.</returns>
        SortedDictionary<Int32, Int32> GetDistanceDistribution();

        /// <summary>
        /// Calculates counts of triangles by vertices in the network.
        /// </summary>
        /// <returns>Distribution of triangles by vertex.</returns>
        SortedDictionary<Int32, Int32> GetTriangleByVertexDistribution();

        /// <summary>
        /// Calculates the counts of cycles in the network.
        /// </summary>
        /// <param name="lowBound">Minimal length of cycle.</param>
        /// <param name="hightBound">Maximal length of cycle.</param>
        /// <returns>Distribution of cycles lengths.</returns>
        SortedDictionary<Int32, BigInteger> GetCycleDistribution(Int16 lowBound, Int16 hightBound);
    }
}
