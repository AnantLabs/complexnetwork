using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

using Core.Model;

namespace NetworkModel
{
    public abstract class AbstractHierarchicAnalyzer : INetworkAnalyzer
    {
        public INetworkContainer Container { get; set; }

        public Double CalculateAveragePath()
        {
            throw new NotImplementedException();
        }

        public UInt32 CalculateDiameter()
        {
            throw new NotImplementedException();
        }

        public Double CalculateAverageDegree()
        {
            throw new NotImplementedException();
        }

        public Double CalculateAverageClusteringCoefficient()
        {
            throw new NotImplementedException();
        }

        public BigInteger CalculateCycles3()
        {
            throw new NotImplementedException();
        }

        public BigInteger CalculateCycles4()
        {
            throw new NotImplementedException();
        }

        public List<Double> CalculateEigenValues()
        {
            throw new NotImplementedException();
        }

        public BigInteger CalculateCycles3Eigen()
        {
            throw new NotImplementedException();
        }

        public BigInteger CalculateCycles4Eigen()
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<Double, Int32> CalculateEigenDistanceDistribution()
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<UInt32, UInt32> CalculateDegreeDistribution()
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<Double, UInt32> GetClusteringCoefficientDistribution()
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<UInt32, UInt32> CalculateConnectedComponentDistribution()
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<UInt32, UInt32> CalculateCompleteComponentDistribution()
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<UInt32, UInt32> CalculateDistanceDistribution()
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<UInt32, UInt32> CalculateTriangleByVertexDistribution()
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<UInt16, BigInteger> CalculateCycleDistribution(UInt16 lowBound, UInt16 hightBound)
        {
            throw new NotImplementedException();
        }
    }
}
