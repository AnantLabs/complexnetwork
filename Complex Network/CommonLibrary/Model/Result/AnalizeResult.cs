using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Numerics;

using CommonLibrary.Model.Result;

namespace RandomGraph.Common.Model.Result
{
    /// <summary>
    /// Result bean for one graph model analyze result saving
    /// </summary>
    public class AnalizeResult
    {
        public AnalizeResult()
        {
            //FIX-ME: NEED TO REMOVE THIS, CREATE SEPERATE OBJECTS;
            Result = new SortedDictionary<AnalyseOptions, double>();
            VertexDegree = new SortedDictionary<int, int>();
            Coefficient = new SortedDictionary<double, int>();
            DistanceBetweenVertices = new SortedDictionary<int, int>();
            Subgraphs = new SortedDictionary<int, int>();
            FullSubgraphs = new SortedDictionary<int, int>();
            EigenVector = new ArrayList();
            DistancesBetweenEigenValues = new SortedDictionary<double, int>();
            Cycles = new SortedDictionary<int, long>();
            TriangleCount = new SortedDictionary<int, int>();
            MotivesCount = new SortedDictionary<int, float>();
            TriangleTrajectory = new SortedDictionary<int, double>();
        }

        public int InstanceID { get; set; }

        public SortedDictionary<AnalyseOptions, double> Result { get; set; }

        public SortedDictionary<int, int> VertexDegree { get; set; }

        public SortedDictionary<double, int> Coefficient { get; set; }

        public SortedDictionary<int, int> Subgraphs { get; set; }

        public SortedDictionary<int, int> FullSubgraphs { get; set; }

        public SortedDictionary<int, int> DistanceBetweenVertices { get; set; }

        public SortedDictionary<double, int> DistancesBetweenEigenValues { get; set; }

        public SortedDictionary<int, float> MotivesCount { get; set; }

        public SortedDictionary<int, long> Cycles { get; set; }

        public SortedDictionary<int, int> TriangleCount { get; set; }

        public ArrayList EigenVector { get; set; }

        public BitArray TreeVector { get; set; }

        public int Cycles3 { get; set; }

        public int Cycles4 { get; set; }

        public SortedDictionary<int, double> TriangleTrajectory { get; set; }
        // !исправить!
        public BigInteger trajectoryMu { get; set; }
        public BigInteger trajectoryStepCount { get; set; }
    }
}
