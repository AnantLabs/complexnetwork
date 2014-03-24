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

        public Double GetAveragePath()
        {
            throw new NotImplementedException();
        }

        public UInt32 GetDiameter()
        {
            throw new NotImplementedException();
        }

        public Double GetAverageDegree()
        {
            throw new NotImplementedException();
        }

        public Double GetAverageClusteringCoefficient()
        {
            throw new NotImplementedException();
        }

        public BigInteger GetCycles3()
        {
            throw new NotImplementedException();
        }

        public BigInteger GetCycles4()
        {
            throw new NotImplementedException();
        }

        public List<Double> GetEigenValues()
        {
            throw new NotImplementedException();
        }

        public BigInteger GetCycles3Eigen()
        {
            throw new NotImplementedException();
        }

        public BigInteger GetCycles4Eigen()
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<Double, Int32> GetEigenDistanceDistribution()
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<UInt32, UInt32> GetDegreeDistribution()
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<Double, UInt32> GetClusteringCoefficientDistribution()
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<UInt32, UInt32> GetConnectedComponentDistribution()
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<UInt32, UInt32> GetCompleteComponentDistribution()
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<UInt32, UInt32> GetDistanceDistribution()
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<UInt32, UInt32> GetTriangleByVertexDistribution()
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<UInt16, BigInteger> GetCycleDistribution(UInt16 lowBound, UInt16 hightBound)
        {
            throw new NotImplementedException();
        }
    }
}
