﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
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
            Result = new SortedDictionary<AnalyseOptions, double>();
            VertexDegree = new SortedDictionary<int, int>();
            Coefficient = new SortedDictionary<double, int>();
            DistanceBetweenVertices = new SortedDictionary<int, int>();
            Subgraphs = new SortedDictionary<int, int>();
            FullSubgraphs = new SortedDictionary<int, int>();
            EigenVector = new ArrayList();
            DistancesBetweenEigenValues = new SortedDictionary<double, int>();
        }

        public int InstanceID { get; set; }

        public SortedDictionary<AnalyseOptions, double> Result { get; set; }

        public SortedDictionary<int, int> VertexDegree { get; set; }

        public SortedDictionary<double, int> Coefficient { get; set; }

        public SortedDictionary<int, int> Subgraphs { get; set; }

        public SortedDictionary<int, int> FullSubgraphs { get; set; }

        public SortedDictionary<int, int> DistanceBetweenVertices { get; set; }

        public SortedDictionary<double, int> DistancesBetweenEigenValues { get; set; }

        public SortedDictionary<int, int> Cycles { get; set; }

        public ArrayList EigenVector { get; set; }

        public BitArray TreeVector { get; set; }

    }
}