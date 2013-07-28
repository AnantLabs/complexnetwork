using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using CommonLibrary.Model;
using log4net;

namespace Model.NonRegularHierarchicModel.Realization
{
    // Реализация контейнера (Block-Hierarchic Non Regular).
    public class NonRegularHierarchicContainer : IGraphContainer
    {
        // Организация pаботы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(NonRegularHierarchicContainer));

        // Иерархияеская основа (простое число).
        private int branchIndex = 0;
        // Иерархический уровень (максимальный).
        private int level = 0;
        private int[][] branches;
        private const int ARRAY_MAX_SIZE = 2000000000;
        // Иерархическое дерево (специфическое).
        private BitArray[][] treeMatrix;

        // Конструктор по умолчанию для контейнера.
        public NonRegularHierarchicContainer()
        {
            log.Info("Creating NonRegularHierarchicContainer default object.");
            treeMatrix = new BitArray[0][];
            branches = new int[0][];
        }

        // Размер контейнера (число вершин в графе).
        public int Size
        {
            get { return Branches[Branches.Length - 1].Length; }   // ??
            set { } // ??
        }

        public int BranchIndex
        {
            get { return branchIndex; }
            set { branchIndex = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public BitArray[][] TreeMatrix
        {
            set { treeMatrix = value; }
        }

        public int[][] Branches
        {
            get { return branches; }
            set { branches = value; }
        }

        // Строится граф на основе матрицы смежности.
        public void SetMatrix(ArrayList matrix)
        {
            log.Info("Creating NonRegularHierarchicContainer object from given matrix.");

            throw new NotImplementedException();
        }

        // Возвращается матрица смежности, соответсвующая графу.
        public bool[,] GetMatrix()
        {
            log.Info("Getting matrix from NonRegularHierarchicContainer object.");

            int vertexCount = Size;
            bool[,] matrix = new bool[vertexCount, vertexCount];

            for (int i = 0; i < vertexCount; ++i)
            {
                for (int j = 0; j < vertexCount; ++j)
                    matrix[i, j] = (this[i, j] == 1) ? true : false;
            }

            return matrix;
        }

        // Закрытая часть класса (не из общего интерфейса).

        // Возвращает 1, если данные вершины соединены, и 0 - в обратном случае.
        // Номера вершин задаются из диапазона [0, Size - 1].
        private int this[int v1, int v2]
        {
            get
            {
                if (v1 == v2)
                    return 0;
                if (v1 > v2)
                {
                    return this[v2, v1];
                }

                // проверка на принадлежение к одному поддереву (для данных вершин)
                // поднимаемся по уровням до того уровна, где они будут принадлежать одному поддереву
                int numberOfGroup1 = 0, numberOfGroup2 = 0;
                int currentLevel = level;
                do
                {
                    --currentLevel;
                    bool groupsFound = false;
                    int counter1 = 0, counter2 = 0;
                    v1 = numberOfGroup1;
                    v2 = numberOfGroup2;
                    for (int i = 0; i < branches[currentLevel].Length - 1; ++i)
                    {
                        for (int j = 0; i < branches[currentLevel][i]; ++j)
                        {
                            if (counter1 == v1)
                            {
                                numberOfGroup1 = i;
                            }
                            if (counter2 == v2)
                            {
                                numberOfGroup2 = i;
                                groupsFound = true;
                                break;
                            }
                        }
                        if (groupsFound == true)
                        {
                            break;
                        }
                    }
                } while (numberOfGroup1 != numberOfGroup2);

                int branchSize = branches[currentLevel][numberOfGroup1];
                BitArray currentNode = TreeNode(currentLevel, numberOfGroup1);
                int index = AdjacentIndex(v1 % branchSize, v2 % branchSize);
                return Convert.ToInt32(currentNode[index]);
            }
        }

        public BitArray TreeNode(int l, long n)
        {
            // проверка параметров на правильность
            if (l < 0 || l >= level)
                throw new SystemException("Wrong parameter (number of level).");
            if (n < 0 || n >= Math.Pow(branchIndex, l))
                throw new SystemException("Wrong parameter (number of node).");

            int branchesCount = branches[l][n];
            int resultSize = branchesCount * (branchesCount - 1) / 2;
            BitArray result = new BitArray(resultSize);

            long i = n * resultSize;
            int ind = Convert.ToInt32(Math.Floor(Convert.ToDouble(i / ARRAY_MAX_SIZE)));
            int rangeSt = Convert.ToInt32(i - ind * ARRAY_MAX_SIZE);
            int rangeEnd = rangeSt + resultSize;
            int secArray = 0;

            if (rangeEnd > ARRAY_MAX_SIZE)
            {
                secArray = rangeEnd - ARRAY_MAX_SIZE;
                rangeEnd = ARRAY_MAX_SIZE - 1;
            }

            int counter = 0;
            for (int j = rangeSt; j < rangeEnd; j++)
            {
                result[counter] = treeMatrix[l][ind][j];
                counter++;
            }

            for (int j = 0; j < secArray; j++)
            {
                result[counter] = treeMatrix[l][ind + 1][j];
                counter++;
            }

            return result;
        }

        // Возвращает индекс того бита (в соответствующей битовой последовательности), 
        // который определяет связность данных узлов.
        private int AdjacentIndex(int v1, int v2)
        {
            if (v1 == v2)
            {
                return 0;
            }

            int result = 0;
            for (int i = 1; i <= v1; i++)
            {
                result += (branchIndex - i);
            }
            --result;
            result += v2 - v1;

            return result;
        }

        /*
        // Методы не из общего интерфейса.   

        /// <summary>
        /// Must be called immediately after the graph is generated.
        /// </summary>
        public void Post_generate()
        {
            /// If this is to be a leave, just return.
            if ((null == node.children) || (0 == node.children.Length))
            {
                node.vertexCount = 1;
                return;
            }

            // Delete this old value if any.
            node.minPathDistribution = null;
            uint i;

            for (i = 0; i < node.children.Length; ++i)
            {
                node.vertexCount += node.children[i].node.vertexCount;
            }

            CountBlocksDists();
        }

        /// <summary>
        /// Counts degree of given vertex.
        /// </summary>
        /// <param name="vertex"> Number of vertex</param>
        /// <returns> Degree of given vertex</returns>
        public uint GetDegree(uint vertex)
        {
            /// If this tree is a leave, return 0.
            if (1 == node.vertexCount)
            {
                return 0;
            }

            uint degree = 0;
            if (vertex >= node.vertexCount)
                throw new Exception("Vertex number more than maximal number of vertexes in get_degree");
            uint block_num = GetBlockOfVertex(vertex);
            uint i;
            for (i = 0; i < node.children.Length; ++i)
            {
                if (IsConnectedBlocks(block_num, i))
                {
                    degree += node.children[i].node.vertexCount;
                }
            }
            degree += node.children[block_num].GetDegree(GetIndexInSubtree(vertex));
            return degree;
        }

        /// <summary>
        /// Counts number of edges in the graph.
        /// </summary>
        /// <returns>Number of edges in the graph</returns>
        public uint GetEdgesCount()
        {
            if (1 == node.vertexCount)
                return 0;
            uint i, j;
            uint edges_count = 0;

            // Take into account connections between blocks.
            for (i = 0; i < node.children.Length; ++i)
            {
                for (j = i + 1; j < node.children.Length; ++j)
                {
                    if (IsConnectedBlocks(i, j))
                    {
                        edges_count += node.children[i].node.vertexCount * node.children[j].node.vertexCount;
                    }
                }
                edges_count += node.children[i].GetEdgesCount();
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
            if (1 == node.vertexCount)
                return 0;
            uint i;
            uint edges_count = 0;

            /// Number of block which contains given vertex.
            uint my_block = GetBlockOfVertex(vertex);

            // Take into account connections between blocks.
            for (i = 0; i < node.children.Length; ++i)
            {
                if (IsConnectedBlocks(i, my_block))
                {
                    edges_count += node.children[i].node.vertexCount;
                }
            }

            // Get this number from my subblock containing this vertex.
            edges_count += node.children[my_block].GetAdjacentVertexCount(GetIndexInSubtree(vertex));
            return edges_count;
        }

        /// <summary>
        /// Counts number of circles of length 3 in this graph.
        /// </summary>
        /// <returns>number of circles of length 3</returns>
        public ulong Get3CirclesCount()
        {
            if (1 == node.vertexCount)
                return 0;
            ulong res = 0;
            uint i, j, k;

            /// Count one edge from one block + a vertex in another block connected to that one.
            for (i = 0; i < node.children.Length; ++i)
            {
                for (j = 0; j < node.children.Length; ++j)
                {
                    if (IsConnectedBlocks(i, j))
                        res += node.children[i].GetEdgesCount() * node.children[j].node.vertexCount;

                }
            }

            /// One vertex from 3 connected blocks.
            for (i = 0; i < node.children.Length; ++i)
            {
                for (j = i + 1; j < node.children.Length; ++j)
                {
                    if (!IsConnectedBlocks(i, j))
                        continue;
                    for (k = j + 1; k < node.children.Length; ++k)
                    {
                        if (IsConnectedBlocks(j, k) && (IsConnectedBlocks(k, i)))
                        {
                            res += node.children[i].node.vertexCount * node.children[j].node.vertexCount * node.children[k].node.vertexCount;
                        }
                    }
                }
            }

            /// Count circles in subblocks.
            for (i = 0; i < node.children.Length; ++i)
            {
                res += node.children[i].Get3CirclesCount();
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
            if (1 == node.vertexCount)
            {
                return 0;
            }

            /// Number of block which contains given vertex.
            uint my_block = GetBlockOfVertex(vertex);
            uint res = 0;
            uint i, j;

            /// Count one edge from one block + a vertex in another block connected to that one.
            for (i = 0; i < node.children.Length; ++i)
            {
                if (IsConnectedBlocks(i, my_block))
                {
                    res += node.children[i].GetEdgesCount();
                    res += node.children[i].node.vertexCount * node.children[my_block].GetDegree(GetIndexInSubtree(vertex));
                }
            }

            /// One vertex from 3 connected blocks.
            for (i = 0; i < node.children.Length; ++i)
            {
                for (j = i + 1; j < node.children.Length; ++j)
                {
                    if (!IsConnectedBlocks(i, j))
                        continue;
                    if (IsConnectedBlocks(j, my_block) && (IsConnectedBlocks(my_block, i)))
                    {
                        res += node.children[i].node.vertexCount * node.children[j].node.vertexCount;
                    }
                }
            }

            /// Count circles in subblock of this vertex.
            res += node.children[my_block].Get3CirclesCountWithVertex(GetIndexInSubtree(vertex));

            return res;
        }

        /// <summary>
        /// Counts number of chains of length 2 in graph. NOTE: For a triangle there are 3 chains of length 2.
        /// </summary>
        /// <returns>Number of chains of length 2</returns>
        public uint Get2LengthChainsCount()
        {
            if (1 == node.vertexCount)
                return 0;
            // Check answer memorization.
            if (node.chainsLength2 != node.NOT_COUNTED_YET_VALUE)
                return node.chainsLength2;

            node.chainsLength2 = 0;
            uint i, j, k;

            /// One vertex from 3 connected blocks.
            for (i = 0; i < node.children.Length; ++i)
            {
                for (j = i + 1; j < node.children.Length; ++j)
                {
                    if (!IsConnectedBlocks(i, j))
                        continue;
                    for (k = j + 1; k < node.children.Length; ++k)
                    {
                        if (IsConnectedBlocks(j, k) && (IsConnectedBlocks(k, i)))
                        {
                            node.chainsLength2 += node.children[i].node.vertexCount * node.children[j].node.vertexCount * node.children[k].node.vertexCount * 3;
                        }
                    }
                }
            }

            /// Count one edge from one block + a vertex in another block connected to that one.
            for (i = 0; i < node.children.Length; ++i)
            {
                for (j = 0; j < node.children.Length; ++j)
                {
                    if (IsConnectedBlocks(i, j))
                    {
                        // Chains with an edge in one side.
                        node.chainsLength2 += node.children[i].GetEdgesCount() * node.children[j].node.vertexCount * 2;

                        // Chains with 2 vertexes in one side, and one in another side.
                        node.chainsLength2 += ((node.children[i].GetEdgesCount() - 1) * node.children[i].GetEdgesCount() / 2) * node.children[j].node.vertexCount;
                    }
                }
            }

            /// Count circles in subblocks.
            for (i = 0; i < node.children.Length; ++i)
            {
                node.chainsLength2 += node.children[i].Get2LengthChainsCount();
            }
            return node.chainsLength2;
        }

        /// <summary>
        /// Counts number of circles of length 4 in this graph.
        /// </summary>
        /// <returns>number of circles of length 4</returns>
        public ulong Get4CirclesCount()
        {
            if (1 == node.vertexCount)
                return 0;
            ulong res = 0;
            uint i1, i2, i3, i4;

            /// One vertex from 4 connected blocks.
            for (i1 = 0; i1 < node.children.Length; ++i1)
            {
                for (i2 = i1 + 1; i2 < node.children.Length; ++i2)
                {
                    if (!IsConnectedBlocks(i1, i2))
                        continue;
                    for (i3 = i2 + 1; i3 < node.children.Length; ++i3)
                    {
                        if (!IsConnectedBlocks(i2, i3))
                            continue;
                        for (i4 = i3 + 1; i4 < node.children.Length; ++i4)
                        {
                            if (IsConnectedBlocks(i3, i4) && (IsConnectedBlocks(i4, i1)))
                            {
                                res += node.children[i1].node.vertexCount * node.children[i2].node.vertexCount * node.children[i3].node.vertexCount * node.children[i4].node.vertexCount * 3;
                            }
                        }
                    }
                }
            }

            /// One vertex from 2 connected blocks, and an edge from another block, connected to first 2.
            for (i1 = 0; i1 < node.children.Length; ++i1)
            {
                for (i2 = i1 + 1; i2 < node.children.Length; ++i2)
                {
                    if (!IsConnectedBlocks(i1, i2))
                        continue;
                    for (i3 = i2 + 1; i3 < node.children.Length; ++i3)
                    {
                        if (!IsConnectedBlocks(i2, i3) || !IsConnectedBlocks(i3, i1))
                            continue;
                        res += (node.children[i1].node.vertexCount * node.children[i2].node.vertexCount * node.children[i3].GetEdgesCount() +
                            node.children[i3].node.vertexCount * node.children[i2].node.vertexCount * node.children[i1].GetEdgesCount() +
                            node.children[i1].node.vertexCount * node.children[i3].node.vertexCount * node.children[i2].GetEdgesCount()) * 2;
                    }
                }
            }

            /// 2 edges from 2 blocks, or 2 connected edges from one block and a vertex from another.
            for (i1 = 0; i1 < node.children.Length; ++i1)
            {
                for (i2 = 0; i2 < node.children.Length; ++i2)
                {
                    if (IsConnectedBlocks(i1, i2))
                        res += node.children[i1].GetEdgesCount() * node.children[i2].GetEdgesCount() +
                            node.children[i1].Get2LengthChainsCount() * node.children[i2].node.vertexCount;
                }
            }

            /// Count circles in subblocks.
            for (i1 = 0; i1 < node.children.Length; ++i1)
            {
                res += node.children[i1].Get4CirclesCount();
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
            if (null != node.minPathDistribution)
            {
                return node.minPathDistribution;
            }

            node.minPathDistribution = new SortedDictionary<int, int>();

            /// If this tree is a leave, return 0.
            if (1 == node.vertexCount)
            {
                return new SortedDictionary<int, int>();
            }

            uint i, j;
            bool has_connection;

            // Take into account connections between blocks.
            for (i = 0; i < node.children.Length; ++i)
            {
                for (j = i + 1; j < node.children.Length; ++j)
                {
                    if (NonRegularHierarchicGraphNode.INFINITE_DISTANCE != node.blockDists[i, j])
                    {
                        int new_average = (int)(node.blockDists[i, j]);
                        int new_power_average = (int)(node.children[i].node.vertexCount * node.children[j].node.vertexCount);

                        AddValues(node.minPathDistribution, new_average, new_power_average);
                    }
                }

                has_connection = false;
                for (j = 1; j < node.children.Length; ++j)
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
                    int new_power_average = (int)node.children[i].GetEdgesCount();

                    AddValues(node.minPathDistribution, new_average, new_power_average);

                    /// Count connections passing through another block connection.
                    new_average = 2;
                    new_power_average = (int)((node.children[i].node.vertexCount * (node.children[i].node.vertexCount - 1) / 2) - node.children[i].GetEdgesCount());

                    AddValues(node.minPathDistribution, new_average, new_power_average);
                }
                else
                {
                    /// Get distribution from child and add it to result.
                    SortedDictionary<int, int> sub_distribution = node.children[i].GetMinPathDistribution();
                    foreach (KeyValuePair<int, int> k in sub_distribution)
                    {
                        AddValues(node.minPathDistribution, k.Key, k.Value);
                    }
                }
            }

            return node.minPathDistribution;
        }

        /// <summary>
        /// Calculates clustering coefficient of graph.
        /// </summary>
        /// <returns></returns>
        public SortedDictionary<double, int> GetClusteringCoefficient()
        {
            SortedDictionary<double, int> retArray = new SortedDictionary<double, int>();

            for (uint i = 0; i < node.vertexCount; i++)
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
            node.blockDists = new uint[node.children.Length, node.children.Length];
            uint i, j, k;
            for (i = 0; i < node.children.Length; ++i)
                for (j = 0; j < node.children.Length; ++j)
                {
                    if (IsConnectedBlocks(i, j))
                        node.blockDists[i, j] = 1;
                    else
                        node.blockDists[i, j] = NonRegularHierarchicGraphNode.INFINITE_DISTANCE;
                }

            for (i = 0; i < node.children.Length; ++i)
                node.blockDists[i, i] = 0;

            for (k = 0; k < node.children.Length; ++k)
                for (i = 0; i < node.children.Length; ++i)
                    for (j = 0; j < node.children.Length; ++j)
                    {
                        if (IsConnectedBlocks(i, k) && IsConnectedBlocks(k, j) && (node.blockDists[i, j] > node.blockDists[i, k] + node.blockDists[k, j]))
                            node.blockDists[i, j] = node.blockDists[i, k] + node.blockDists[k, j];
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
                log.Info("IN IsConnectedBlocks " + block1_num + " " + block2_num + "and result is false");
                return false;
            }
            // vertex1 must have min number
            if (block1_num > block2_num)
            {
                log.Info("IN IsConnectedBlocks " + block1_num + " " + block2_num + "and result is " + IsConnectedBlocks(block2_num, block1_num));
                return IsConnectedBlocks(block2_num, block1_num);
            }

            // Get the index of two vertexes adjacent value
            int index = 0;
            for (int k = 1; k <= block1_num; k++)
            {
                index += node.children.Length - k;
            }
            index += (int)(block2_num - block1_num - 1);
            log.Info("IN IsConnectedBlocks " + block1_num + " " + block2_num + "and result is " + node.data[index]);
            return node.data[index];
        }

        /// <summary>
        /// Returns what is the number of this vertex in its subtree.
        /// </summary>
        /// <param name="vertex">Number of vertex in this tree.</param>
        /// <returns>Number of vertex in subtree.</returns>
        private uint GetIndexInSubtree(uint vertex)
        {
            uint i;
            for (i = 0; i < node.children.Length; ++i)
            {
                if (vertex >= node.children[i].node.vertexCount)
                    vertex -= node.children[i].node.vertexCount;
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
            for (i = 0; i < node.children.Length; ++i)
            {
                if (vertex >= node.children[i].node.vertexCount)
                    vertex -= node.children[i].node.vertexCount;
                else
                    break;
            }
            return i;
        }

        public double ClusteringCoefficientOfVertex(uint v)
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
         * */
    }
}
