using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using NumberGeneration;

namespace Model.NonRegularHierarchicModel.Realization
{
    public class NonRegularHierarchicGraph
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NonRegularHierarchicGraph()
        {
            vertexes_count = 0;
            m_chains_length_2 = NOT_COUNTED_YET_VALUE;
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
            m_min_path_distribution = null;

            /// If this is to be a leave, just return.
            if (0 == max_level)
            {
                vertexes_count = 1;
                return;
            }

            children = new NonRegularHierarchicGraph[rnd2.Next(2, p + 1)];
            int i;
            for (i = 0; i < children.Length; ++i)
            {
                children[i] = new NonRegularHierarchicGraph();

                /// generate further tree of graph.
                children[i].generate_with(p, (Int16)(max_level - 1), Mu);
                vertexes_count += children[i].get_vertexes_count();
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

            count_blocks_dists();
        }

        /// <summary>
        /// Counts distances between subblocks of this graph.
        /// </summary>
        private void count_blocks_dists()
        {
            block_dists = new uint[children.Length, children.Length];
            uint i, j, k;
            for (i = 0; i < children.Length; ++i)
                for (j = 0; j < children.Length; ++j)
                {
                    if (is_connected_blocks(i, j))
                        block_dists[i, j] = 1;
                    else
                        block_dists[i, j] = INFINITE_DISTANCE;
                }

            for (i = 0; i < children.Length; ++i)
                block_dists[i, i] = 0;

            for (k = 0; k < children.Length; ++k)
                for (i = 0; i < children.Length; ++i)
                    for (j = 0; j < children.Length; ++j)
                    {
                        if (is_connected_blocks(i, k) && is_connected_blocks(k, j) && (block_dists[i, j] > block_dists[i, k] + block_dists[k, j]))
                            block_dists[i, j] = block_dists[i, k] + block_dists[k, j];
                    }
        }

        public uint get_vertexes_count()
        {
            return vertexes_count;
        }

        /// <summary>
        /// Counts degree of given vertex.
        /// </summary>
        /// <param name="vertex"> Number of vertex</param>
        /// <returns> Degree of given vertex</returns>
        public uint get_degree(uint vertex)
        {
            /// If this tree is a leave, return 0.
            if (1 == vertexes_count)
            {
                return 0;
            }

            uint degree = 0;
            if (vertex >= vertexes_count)
                throw new Exception("Vertex number more than maximal number of vertexes in get_degree");
            uint block_num = get_block_of_vertex(vertex);
            uint i;
            for (i = 0; i < children.Length; ++i)
            {
                if (is_connected_blocks(block_num, i))
                {
                    degree += children[i].get_vertexes_count();
                }
            }
            degree += children[block_num].get_degree(get_index_in_subtree(vertex));
            return degree;
        }

        /// <summary>
        /// Counts number of edges in the graph.
        /// </summary>
        /// <returns>Number of edges in the graph</returns>
        public uint get_edges_count()
        {
            if (1 == vertexes_count)
                return 0;
            uint i, j;
            uint edges_count = 0;

            // Take into account connections between blocks.
            for (i = 0; i < children.Length; ++i)
            {
                for (j = i + 1; j < children.Length; ++j)
                {
                    if (is_connected_blocks(i, j))
                    {
                        edges_count += children[i].get_vertexes_count() * children[j].get_vertexes_count();
                    }
                }
                edges_count += children[i].get_edges_count();
            }
            return edges_count;
        }

        /// <summary>
        /// Counts number of circles of length 3 in this graph.
        /// </summary>
        /// <returns>number of circles of length 3</returns>
        public uint get_3_circles_count()
        {
            if (1 == vertexes_count)
                return 0;
            uint res = 0;
            uint i, j, k;

            /// Count one edge from one block + a vertex in another block connected to that one.
            for (i = 0; i < children.Length; ++i)
            {
                for (j = 0; j < children.Length; ++j)
                {
                    if (is_connected_blocks(i, j))
                        res += children[i].get_edges_count() * children[j].get_vertexes_count();
                }
            }

            /// One vertex from 3 connected blocks.
            for (i = 0; i < children.Length; ++i)
            {
                for (j = i + 1; j < children.Length; ++j)
                {
                    if (!is_connected_blocks(i, j))
                        continue;
                    for (k = j + 1; k < children.Length; ++k)
                    {
                        if (is_connected_blocks(j, k) && (is_connected_blocks(k, i)))
                        {
                            res += children[i].get_vertexes_count() * children[j].get_vertexes_count() * children[k].get_vertexes_count();
                        }
                    }
                }
            }

            /// Count circles in subblocks.
            for (i = 0; i < children.Length; ++i)
            {
                res += children[i].get_3_circles_count();
            }

            return res;
        }

        /// <summary>
        /// Counts number of chains of length 2 in graph. NOTE: For a triangle there are 3 chains of length 2.
        /// </summary>
        /// <returns>Number of chains of length 2</returns>
        public uint get_2_length_chains_count()
        {
            if (1 == vertexes_count)
                return 0;
            // Check answer memorization.
            if (m_chains_length_2 != NOT_COUNTED_YET_VALUE)
                return m_chains_length_2;

            m_chains_length_2 = 0;
            uint i, j, k;

            /// One vertex from 3 connected blocks.
            for (i = 0; i < children.Length; ++i)
            {
                for (j = i + 1; j < children.Length; ++j)
                {
                    if (!is_connected_blocks(i, j))
                        continue;
                    for (k = j + 1; k < children.Length; ++k)
                    {
                        if (is_connected_blocks(j, k) && (is_connected_blocks(k, i)))
                        {
                            m_chains_length_2 += children[i].get_vertexes_count() * children[j].get_vertexes_count() * children[k].get_vertexes_count() * 3;
                        }
                    }
                }
            }

            /// Count one edge from one block + a vertex in another block connected to that one.
            for (i = 0; i < children.Length; ++i)
            {
                for (j = 0; j < children.Length; ++j)
                {
                    if (is_connected_blocks(i, j))
                    {
                        // Chains with an edge in one side.
                        m_chains_length_2 += children[i].get_edges_count() * children[j].get_vertexes_count() * 2;

                        // Chains with 2 vertexes in one side, and one in another side.
                        m_chains_length_2 += ((children[i].get_edges_count() - 1) * children[i].get_edges_count() / 2) * children[j].get_vertexes_count();
                    }
                }
            }

            /// Count circles in subblocks.
            for (i = 0; i < children.Length; ++i)
            {
                m_chains_length_2 += children[i].get_2_length_chains_count();
            }
            return m_chains_length_2;
        }

        /// <summary>
        /// Counts number of circles of length 4 in this graph.
        /// </summary>
        /// <returns>number of circles of length 4</returns>
        public uint get_4_circles_count()
        {
            if (1 == vertexes_count)
                return 0;
            uint res = 0;
            uint i1, i2, i3, i4;

            /// One vertex from 4 connected blocks.
            for (i1 = 0; i1 < children.Length; ++i1)
            {
                for (i2 = i1 + 1; i2 < children.Length; ++i2)
                {
                    if (!is_connected_blocks(i1, i2))
                        continue;
                    for (i3 = i2 + 1; i3 < children.Length; ++i3)
                    {
                        if (!is_connected_blocks(i2, i3))
                            continue;
                        for (i4 = i3 + 1; i4 < children.Length; ++i4)
                        {
                            if (is_connected_blocks(i3, i4) && (is_connected_blocks(i4, i1)))
                            {
                                res += children[i1].get_vertexes_count() * children[i2].get_vertexes_count() * children[i3].get_vertexes_count() * children[i4].get_vertexes_count() * 3;
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
                    if (!is_connected_blocks(i1, i2))
                        continue;
                    for (i3 = i2 + 1; i3 < children.Length; ++i3)
                    {
                        if (!is_connected_blocks(i2, i3) || !is_connected_blocks(i3, i1))
                            continue;
                        res += (children[i1].get_vertexes_count() * children[i2].get_vertexes_count() * children[i3].get_edges_count() +
                            children[i3].get_vertexes_count() * children[i2].get_vertexes_count() * children[i1].get_edges_count() +
                            children[i1].get_vertexes_count() * children[i3].get_vertexes_count() * children[i2].get_edges_count()) * 2;
                    }
                }
            }

            /// 2 edges from 2 blocks, or 2 connected edges from one block and a vertex from another.
            for (i1 = 0; i1 < children.Length; ++i1)
            {
                for (i2 = 0; i2 < children.Length; ++i2)
                {
                    if (is_connected_blocks(i1, i2))
                        res += children[i1].get_edges_count() * children[i2].get_edges_count() + children[i1].get_2_length_chains_count() * children[i2].get_vertexes_count();
                }
            }

            /// Count circles in subblocks.
            for (i1 = 0; i1 < children.Length; ++i1)
            {
                res += children[i1].get_4_circles_count();
            }

            return res;
        }

        /// <summary>
        /// Counts average path.
        /// </summary>
        /// <returns> A pair of numbers. Key is the average path length. Value is the number of paths, on which average has been counted.</returns>
        public SortedDictionary<int, int> get_min_path_distribution()
        {
            /// Check if we have this value allready.
            if (null != m_min_path_distribution)
            {
                return m_min_path_distribution;
            }

            m_min_path_distribution = new SortedDictionary<int, int>();

            /// If this tree is a leave, return 0.
            if (1 == vertexes_count)
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
                    if (INFINITE_DISTANCE != block_dists[i, j])
                    {
                        int new_average = (int)(block_dists[i, j]);
                        int new_power_average = (int)(children[i].get_vertexes_count() * children[j].get_vertexes_count());

                        add_values(m_min_path_distribution, new_average, new_power_average);
                    }
                }

                has_connection = false;
                for (j = 1; j < children.Length; ++j)
                {
                    if (is_connected_blocks(i, j))
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
                    int new_power_average = (int)children[i].get_edges_count();

                    add_values(m_min_path_distribution, new_average, new_power_average);

                    /// Count connections passing through another block connection.
                    new_average = 2;
                    new_power_average = (int)((children[i].get_vertexes_count() * (children[i].get_vertexes_count() - 1) / 2) - children[i].get_edges_count());

                    add_values(m_min_path_distribution, new_average, new_power_average);
                }
                else
                {
                    /// Get distribution from child and add it to result.
                    SortedDictionary<int, int> sub_distribution = children[i].get_min_path_distribution();
                    foreach (KeyValuePair<int, int> k in sub_distribution)
                    {
                        add_values(m_min_path_distribution, k.Key, k.Value);
                    }
                }
            }

            return m_min_path_distribution;
        }

        /// <summary>
        /// Adds given value to the value in dictionary.
        /// </summary>
        /// <param name="ret">Dictionary in which to add</param>
        /// <param name="key">Key</param>
        /// <param name="value_to_add">Value to be added</param>
        private void add_values(SortedDictionary<int, int> ret, int key, int value_to_add)
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
        private bool is_connected_blocks(uint block1_num, uint block2_num)
        {
            if (block1_num == block2_num)
            {
                return false;
            }
            // vertex1 must have min number
            if (block1_num > block2_num)
            {
                return is_connected_blocks(block2_num, block1_num);
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
        private uint get_index_in_subtree(uint vertex)
        {
            uint i;
            for (i = 0; i < children.Length; ++i)
            {
                if (vertex >= children[i].get_vertexes_count())
                    vertex -= children[i].get_vertexes_count();
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
        private uint get_block_of_vertex(uint vertex)
        {
            uint i;
            for (i = 0; i < children.Length; ++i)
            {
                if (vertex >= children[i].get_vertexes_count())
                    vertex -= children[i].get_vertexes_count();
                else
                    break;
            }
            return i;
        }

        // Some magic number to set infinite distance between blocks.
        private const uint INFINITE_DISTANCE = 1000000000;

        // Distances between blocks.
        private uint[,] block_dists;

        // Number of vertexes in current graph.
        private uint vertexes_count;

        // Children blocks stored here.
        private NonRegularHierarchicGraph[] children;

        // Tree connectivity data. Information about connectivity of subblocks of this graph.
        public BitArray data;

        /// KM TODO change to RGNCRYPTO.
        //RNGCrypto rnd2 = new RNGCrypto();
        Random rnd2 = new Random();

        // Minimum paths distribution here. Stored to return without counting again if called many times.
        SortedDictionary<int, int> m_min_path_distribution;

        // Number of chains of length 2
        uint m_chains_length_2;

        // Magic value for not yet counted values.
        uint NOT_COUNTED_YET_VALUE = uint.MaxValue;
    }
}
