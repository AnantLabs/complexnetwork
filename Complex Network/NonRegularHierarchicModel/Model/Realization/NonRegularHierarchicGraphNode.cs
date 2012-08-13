using System;
using System.Collections.Generic;
using System.Collections;

namespace Model.NonRegularHierarchicModel.Realization
{
    public class NonRegularHierarchicGraphNode
    {
        // Some magic number to set infinite distance between blocks.
        public const uint INFINITE_DISTANCE = 1000000000;

        // Distances between blocks.
        public uint[,] blockDists;

        // Number of vertices in current graph.
        public uint vertexCount;

        // Children blocks stored here.
        public NonRegularHierarchicContainer[] children;

        // Tree connectivity data. Information about connectivity of subblocks of this graph.
        public BitArray data;

        // Minimum paths distribution here. Stored to return without counting again if called many times.
        public SortedDictionary<int, int> minPathDistribution;

        // Number of chains of length 2
        public uint chainsLength2;

        // Magic value for not yet counted values.
        public uint NOT_COUNTED_YET_VALUE = uint.MaxValue;

        public uint VertexCount
        {
            get { return vertexCount; }
        }
    }
}
