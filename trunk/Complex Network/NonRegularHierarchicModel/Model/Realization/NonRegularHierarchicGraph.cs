using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
//using NumberGeneration;

namespace NonRegularHierarchicModel.Model.Realization
{
    public class NonRegularHierarchicGraph
    {
        // Some magic number to set infinite distance between blocks.
        private const uint INFINITE_DISTANCE = 1000000000;

        // Distances between blocks.
        private uint[,] blockDists;

        // Number of vertices in current graph.
        private uint vertexCount;

        // Children blocks stored here.
        private NonRegularHierarchicGraph[] children;

        // Tree connectivity data. Information about connectivity of subblocks of this graph.
        public BitArray data;

        /// KM TODO change to RGNCRYPTO.
        //RNGCrypto rnd2 = new RNGCrypto();
        Random rnd2 = new Random();

        // Minimum paths distribution here. Stored to return without counting again if called many times.
        SortedDictionary<int, int> minPathDistribution;

        // Number of chains of length 2
        uint chainsLength2;

        // Magic value for not yet counted values.
        uint NOT_COUNTED_YET_VALUE = uint.MaxValue;

        /// <summary>
        /// Constructor.
        /// </summary>
        public NonRegularHierarchicGraph()
        {
            vertexCount = 0;
            chainsLength2 = NOT_COUNTED_YET_VALUE;
        }

        /// <summary>
        /// Generates graph with given parameters.
        /// </summary>
        /// <param name="p"> Maximal number of blocks in a single level of graph.</param>
        /// <param name="max_level"> Number of levels in graph.</param>
        /// <param name="Mu"> Double value which determines the connectivity of graph. 1 will create a full connected graph.</param>
        public void generate_with(Int16 p, Int16 max_level, Double Mu)
        {
            // Delete this old value if any.
            minPathDistribution = null;

            /// If this is to be a leave, just return.
            if (0 == max_level)
            {
                vertexCount = 1;
                return;
            }

            children = new NonRegularHierarchicGraph[rnd2.Next(2, p + 1)];
            int i;
            for (i = 0; i < children.Length; ++i)
            {
                children[i] = new NonRegularHierarchicGraph();

                /// generate further tree of graph.
                children[i].generate_with(p, (Int16)(max_level - 1), Mu);
                vertexCount += children[i].VertexCount;
            }

            int length = (children.Length - 1) * children.Length / 2;
            data = new BitArray(length, false);
            for (i = 0; i < length; ++i)
            {
                double k = rnd2.NextDouble();
                if (k <= (1 / Math.Pow(p, max_level * Mu)))
                {
                    data[i] = true;
                }
                else
                {
                    data[i] = false;
                }
            }

            CountBlocksDists();
        }

        public uint VertexCount
        {
            get { return vertexCount; }
        }

        /// <summary>
        /// Counts degree of given vertex.
        /// </summary>
        /// <param name="vertex"> Number of vertex</param>
        /// <returns> Degree of given vertex</returns>
        public uint GetDegree(uint vertex)
        {
            /// If this tree is a leave, return 0.
            if (1 == vertexCount)
            {
                return 0;
            }

            uint degree = 0;
            if (vertex >= vertexCount)
                throw new Exception("Vertex number more than maximal number of vertexes in get_degree");
            uint block_num = GetBlockOfVertex(vertex);
            uint i;
            for (i = 0; i < children.Length; ++i)
            {
                if (IsConnectedBlocks(block_num, i))
                {
                    degree += children[i].VertexCount;
                }
            }
            degree += children[block_num].GetDegree(GetIndexInSubtree(vertex));
            return degree;
        }

        /// <summary>
        /// Counts number of edges in the graph.
        /// </summary>
        /// <returns>Number of edges in the graph</returns>
        public uint GetEdgesCount()
        {
            if (1 == vertexCount)
                return 0;
            uint i, j;
            uint edges_count = 0;

            // Take into account connections between blocks.
            for (i = 0; i < children.Length; ++i)
            {
                for (j = i + 1; j < children.Length; ++j)
                {
                    if (IsConnectedBlocks(i, j))
                    {
                        edges_count += children[i].VertexCount * children[j].VertexCount;
                    }
                }
                edges_count += children[i].GetEdgesCount();
            }
            return edges_count;
        }

        /// <summary>
        /// Counts number of vertexes adjusent to the given in the graph.
        /// </summary>
        /// <param name="vertex"> Number of vertex to which adjusents to be found</param>
        /// <returns>Number of edges in the graph</returns>
        public uint GetAdjacentVertexCount(uint vertex)
        {
            if (1 == vertexCount)
                return 0;
            uint i;
            uint edges_count = 0;

            /// Number of block which contains given vertex.
            uint my_block = GetBlockOfVertex(vertex);

            // Take into account connections between blocks.
            for (i = 0; i < children.Length; ++i)
            {
                if (IsConnectedBlocks(i, my_block))
                {
                    edges_count += children[i].VertexCount;
                }
            }

            // Get this number from my subblock containing this vertex.
            edges_count += children[my_block].GetAdjacentVertexCount(GetIndexInSubtree(vertex));
            return edges_count;
        }

        /// <summary>
        /// Counts number of circles of length 3 in this graph.
        /// </summary>
        /// <returns>number of circles of length 3</returns>
        public uint Get3CirclesCount()
        {
            if (1 == vertexCount)
                return 0;
            uint res = 0;
            uint i, j, k;

            /// Count one edge from one block + a vertex in another block connected to that one.
            for (i = 0; i < children.Length; ++i)
            {
                for (j = 0; j < children.Length; ++j)
                {
                    if (IsConnectedBlocks(i, j))
                        res += children[i].GetEdgesCount() * children[j].VertexCount;
                }
            }

            /// One vertex from 3 connected blocks.
            for (i = 0; i < children.Length; ++i)
            {
                for (j = i + 1; j < children.Length; ++j)
                {
                    if (!IsConnectedBlocks(i, j))
                        continue;
                    for (k = j + 1; k < children.Length; ++k)
                    {
                        if (IsConnectedBlocks(j, k) && (IsConnectedBlocks(k, i)))
                        {
                            res += children[i].VertexCount * children[j].VertexCount * children[k].VertexCount;
                        }
                    }
                }
            }

            /// Count circles in subblocks.
            for (i = 0; i < children.Length; ++i)
            {
                res += children[i].Get3CirclesCount();
            }

            return res;
        }


        /// <summary>
        /// Counts number of circles of length 3 in this graph which contain given vertex.
        /// </summary>
        /// <param name="vertex"> Number of vertex.</param>
        /// <returns>number of circles of length 3 in this graph which contain given vertex</returns>
        public uint Get3CirclesCountWithVertex(uint vertex)
        {
            if (1 == vertexCount)
            {
                return 0;
            }

            /// Number of block which contains given vertex.
            uint my_block = GetBlockOfVertex(vertex);
            uint res = 0;
            uint i, j;

            /// Count one edge from one block + a vertex in another block connected to that one.
            for (i = 0; i < children.Length; ++i)
            {
                if (IsConnectedBlocks(i, my_block))
                {
                    res += children[i].GetEdgesCount();
                }
            }

            /// One vertex from 3 connected blocks.
            for (i = 0; i < children.Length; ++i)
            {
                for (j = i + 1; j < children.Length; ++j)
                {
                    if (!IsConnectedBlocks(i, j))
                        continue;
                    if (IsConnectedBlocks(j, my_block) && (IsConnectedBlocks(my_block, i)))
                    {
                        res += children[i].VertexCount * children[j].VertexCount;
                    }
                }
            }

            /// Count circles in subblock of this vertex.
            res += children[my_block].Get3CirclesCountWithVertex(GetIndexInSubtree(vertex));

            return res;
        }

        /// <summary>
        /// Counts number of chains of length 2 in graph. NOTE: For a triangle there are 3 chains of length 2.
        /// </summary>
        /// <returns>Number of chains of length 2</returns>
        public uint Get2LengthChainsCount()
        {
            if (1 == vertexCount)
                return 0;
            // Check answer memorization.
            if (chainsLength2 != NOT_COUNTED_YET_VALUE)
                return chainsLength2;

            chainsLength2 = 0;
            uint i, j, k;

            /// One vertex from 3 connected blocks.
            for (i = 0; i < children.Length; ++i)
            {
                for (j = i + 1; j < children.Length; ++j)
                {
                    if (!IsConnectedBlocks(i, j))
                        continue;
                    for (k = j + 1; k < children.Length; ++k)
                    {
                        if (IsConnectedBlocks(j, k) && (IsConnectedBlocks(k, i)))
                        {
                            chainsLength2 += children[i].VertexCount * children[j].VertexCount * children[k].VertexCount * 3;
                        }
                    }
                }
            }

            /// Count one edge from one block + a vertex in another block connected to that one.
            for (i = 0; i < children.Length; ++i)
            {
                for (j = 0; j < children.Length; ++j)
                {
                    if (IsConnectedBlocks(i, j))
                    {
                        // Chains with an edge in one side.
                        chainsLength2 += children[i].GetEdgesCount() * children[j].VertexCount * 2;

                        // Chains with 2 vertexes in one side, and one in another side.
                        chainsLength2 += ((children[i].GetEdgesCount() - 1) * children[i].GetEdgesCount() / 2) * children[j].VertexCount;
                    }
                }
            }

            /// Count circles in subblocks.
            for (i = 0; i < children.Length; ++i)
            {
                chainsLength2 += children[i].Get2LengthChainsCount();
            }
            return chainsLength2;
        }

        /// <summary>
        /// Counts number of circles of length 4 in this graph.
        /// </summary>
        /// <returns>number of circles of length 4</returns>
        public uint Get4CirclesCount()
        {
            if (1 == vertexCount)
                return 0;
            uint res = 0;
            uint i1, i2, i3, i4;

            /// One vertex from 4 connected blocks.
            for (i1 = 0; i1 < children.Length; ++i1)
            {
                for (i2 = i1 + 1; i2 < children.Length; ++i2)
                {
                    if (!IsConnectedBlocks(i1, i2))
                        continue;
                    for (i3 = i2 + 1; i3 < children.Length; ++i3)
                    {
                        if (!IsConnectedBlocks(i2, i3))
                            continue;
                        for (i4 = i3 + 1; i4 < children.Length; ++i4)
                        {
                            if (IsConnectedBlocks(i3, i4) && (IsConnectedBlocks(i4, i1)))
                            {
                                res += children[i1].VertexCount * children[i2].VertexCount * children[i3].VertexCount * children[i4].VertexCount * 3;
                            }
                        }
                    }
                }
            }

            /// One vertex from 2 connected blocks, and an edge from another block, connected to first 2.
            for (i1 = 0; i1 < children.Length; ++i1)
            {
                for (i2 = i1 + 1; i2 < children.Length; ++i2)
                {
                    if (!IsConnectedBlocks(i1, i2))
                        continue;
                    for (i3 = i2 + 1; i3 < children.Length; ++i3)
                    {
                        if (!IsConnectedBlocks(i2, i3) || !IsConnectedBlocks(i3, i1))
                            continue;
                        res += (children[i1].VertexCount * children[i2].VertexCount * children[i3].GetEdgesCount() +
                            children[i3].VertexCount * children[i2].VertexCount * children[i1].GetEdgesCount() +
                            children[i1].VertexCount * children[i3].VertexCount * children[i2].GetEdgesCount()) * 2;
                    }
                }
            }

            /// 2 edges from 2 blocks, or 2 connected edges from one block and a vertex from another.
            for (i1 = 0; i1 < children.Length; ++i1)
            {
                for (i2 = 0; i2 < children.Length; ++i2)
                {
                    if (IsConnectedBlocks(i1, i2))
                        res += children[i1].GetEdgesCount() * children[i2].GetEdgesCount() + 
                            children[i1].Get2LengthChainsCount() * children[i2].VertexCount;
                }
            }

            /// Count circles in subblocks.
            for (i1 = 0; i1 < children.Length; ++i1)
            {
                res += children[i1].Get4CirclesCount();
            }

            return res;
        }

        /// <summary>
        /// Counts average path.
        /// </summary>
        /// <returns> A pair of numbers. Key is the average path length. Value is the number of paths, on which average has been counted.</returns>
        public SortedDictionary<int, int> GetMinPathDistribution()
        {
            /// Check if we have this value allready.
            if (null != minPathDistribution)
            {
                return minPathDistribution;
            }

            minPathDistribution = new SortedDictionary<int, int>();

            /// If this tree is a leave, return 0.
            if (1 == vertexCount)
            {
                return new SortedDictionary<int, int>();
            }

            uint i, j;
            bool has_connection;

            // Take into account connections between blocks.
            for (i = 0; i < children.Length; ++i)
            {
                for (j = i + 1; j < children.Length; ++j)
                {
                    if (INFINITE_DISTANCE != blockDists[i, j])
                    {
                        int new_average = (int)(blockDists[i, j]);
                        int new_power_average = (int)(children[i].VertexCount * children[j].VertexCount);

                        AddValues(minPathDistribution, new_average, new_power_average);
                    }
                }

                has_connection = false;
                for (j = 1; j < children.Length; ++j)
                {
                    if (IsConnectedBlocks(i, j))
                    {
                        has_connection = true;
                        break;
                    }
                }

                /// If there is a connection between [i]-th and one another block.
                if (has_connection)
                {
                    /// Counted immediate edges in [i]-th subgraph.
                    int new_average = 1;
                    int new_power_average = (int)children[i].GetEdgesCount();

                    AddValues(minPathDistribution, new_average, new_power_average);

                    /// Count connections passing through another block connection.
                    new_average = 2;
                    new_power_average = (int)((children[i].VertexCount * (children[i].VertexCount - 1) / 2) - children[i].GetEdgesCount());

                    AddValues(minPathDistribution, new_average, new_power_average);
                }
                else
                {
                    /// Get distribution from child and add it to result.
                    SortedDictionary<int, int> sub_distribution = children[i].GetMinPathDistribution();
                    foreach (KeyValuePair<int, int> k in sub_distribution)
                    {
                        AddValues(minPathDistribution, k.Key, k.Value);
                    }
                }
            }

            return minPathDistribution;
        }

        /// <summary>
        /// Calculates clustering coefficient of graph.
        /// </summary>
        /// <returns></returns>
        public SortedDictionary<double, int> GetClusteringCoefficient()
        {
            SortedDictionary<double, int> retArray = new SortedDictionary<double, int>();

            for (uint i = 0; i < vertexCount; i++)
            {
                double dresult = ClusteringCoefficientOfVertex(i);
                dresult = dresult * 10000;
                int iResult = Convert.ToInt32(dresult);
                double result = (double)iResult / 10000;
                if (retArray.Keys.Contains(result))
                    retArray[result] += 1;
                else
                    retArray.Add(result, 1);
            }

            return retArray;
        }

        // Utilities

        /// <summary>
        /// Counts distances between subblocks of this graph.
        /// </summary>
        private void CountBlocksDists()
        {
            blockDists = new uint[children.Length, children.Length];
            uint i, j, k;
            for (i = 0; i < children.Length; ++i)
                for (j = 0; j < children.Length; ++j)
                {
                    if (IsConnectedBlocks(i, j))
                        blockDists[i, j] = 1;
                    else
                        blockDists[i, j] = INFINITE_DISTANCE;
                }

            for (i = 0; i < children.Length; ++i)
                blockDists[i, i] = 0;

            for (k = 0; k < children.Length; ++k)
                for (i = 0; i < children.Length; ++i)
                    for (j = 0; j < children.Length; ++j)
                    {
                        if (IsConnectedBlocks(i, k) && IsConnectedBlocks(k, j) && (blockDists[i, j] > blockDists[i, k] + blockDists[k, j]))
                            blockDists[i, j] = blockDists[i, k] + blockDists[k, j];
                    }
        }

        /// <summary>
        /// Adds given value to the value in dictionary.
        /// </summary>
        /// <param name="ret">Dictionary in which to add</param>
        /// <param name="key">Key</param>
        /// <param name="value_to_add">Value to be added</param>
        private void AddValues(SortedDictionary<int, int> ret, int key, int value_to_add)
        {
            // Check not to create empty, not needed values in the list.
            if (0 == value_to_add)
                return;

            if (ret.ContainsKey(key))
                ret[key] += value_to_add;
            else
                ret.Add(key, value_to_add);
        }

        /// <summary>
        /// Checks if given 2 blocks are connected.
        /// </summary>
        /// <param name="block1_num">Number of 1st block</param>
        /// <param name="block2_num">Number of 2nd block</param>
        /// <returns>True, if given 2 blocks are connected.</returns>
        private bool IsConnectedBlocks(uint block1_num, uint block2_num)
        {
            if (block1_num == block2_num)
            {
                return false;
            }
            // vertex1 must have min number
            if (block1_num > block2_num)
            {
                return IsConnectedBlocks(block2_num, block1_num);
            }

            // Get the index of two vertexes adjacent value
            int index = 0;
            for (int k = 1; k <= block1_num; k++)
            {
                index += this.children.Length - k;
            }
            index += (int)(block2_num - block1_num - 1);
            return data[index];
        }

        /// <summary>
        /// Returns what is the number of this vertex in its subtree.
        /// </summary>
        /// <param name="vertex">Number of vertex in this tree.</param>
        /// <returns>Number of vertex in subtree.</returns>
        private uint GetIndexInSubtree(uint vertex)
        {
            uint i;
            for (i = 0; i < children.Length; ++i)
            {
                if (vertex >= children[i].VertexCount)
                    vertex -= children[i].VertexCount;
                else
                    break;
            }
            return vertex;
        }

        /// <summary>
        /// Counts number of subtree in which is the given vertex.
        /// </summary>
        /// <param name="vertex">Number of vertex</param>
        /// <returns>Number of subtree/block in which it is</returns>
        private uint GetBlockOfVertex(uint vertex)
        {
            uint i;
            for (i = 0; i < children.Length; ++i)
            {
                if (vertex >= children[i].VertexCount)
                    vertex -= children[i].VertexCount;
                else
                    break;
            }
            return i;
        }

        private double ClusteringCoefficientOfVertex(uint v)
        {
            uint adj = GetAdjacentVertexCount(v);

            /// Check if there are at least 2 vertexes. If not, return 0.
            if (adj < 2)
            {
                return 0;
            }
            uint cyrcles = Get3CirclesCountWithVertex(v);
            return (cyrcles + 0.0) / (adj * (adj - 1.0) / 2);
        }
    }
}
