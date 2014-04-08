using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

using Core.Model;
using NetworkModel;
using NetworkModel.Engine.Eigenvalues;
using NetworkModel.HierarchicEngine;

namespace RegularHierarchicModel
{
    /// <summary>
    /// Implementation of regularly branching block-hierarchic network's analyzer.
    /// </summary>
    class RegularHierarchicNetworkAnalyzer : AbstractNetworkAnalyzer
    {
        /// <summary>
        /// Container with network of specified model (regular block-hierarchic).
        /// </summary>
        private RegularHierarchicNetworkContainer container;

        public override INetworkContainer Container
        {
            get { return container; }
            set { container = (RegularHierarchicNetworkContainer)value; }
        }

        public RegularHierarchicNetworkAnalyzer() { }

        protected override double CalculateAveragePath()
        {
            if (-1 == avgPath)
            {
                CountPathDistribution();
            }

            return Math.Round(avgPath, 14);

            // TODO
            //long[] pathsInfo = GetSubgraphsPathInfo(0, 0);
            // !petq e bajanel chanaparhneri qanaki vra!
            //return 2 * (pathsInfo[0] + pathsInfo[2]) / ((double)container.Size *
            //    ((double)container.Size - 1));
        }

        protected override uint CalculateDiameter()
        {
            if (-1 == diameter)
            {
                CountPathDistribution();
            }

            // TODO return diameter;
            return 0;
        }

        protected override double CalculateAverageDegree()
        {
            // TODO
            return 0;
        }

        protected override double CalculateAverageClusteringCoefficient()
        {
            // TODO
            return 0;
        }

        protected override BigInteger CalculateCycles3()
        {
            // TODO get BigInteger result
            return (long)Count3Cycle(0, 0)[0];
        }

        protected override BigInteger CalculateCycles4()
        {
            // TODO get BigInteger result
            return (long)Count4Cycle(0, 0)[0];
        }

        protected override List<Double> CalculateEigenValues()
        {
            bool[,] m = container.GetMatrix();

            EigenValueUtils eg = new EigenValueUtils();
            try
            {
                return eg.CalculateEigenValue(m);
            }
            catch (SystemException ex)
            {
                return new List<double>();
            }
        }

        protected override SortedDictionary<Double, Int32> CalculateEigenDistanceDistribution()
        {
            bool[,] m = container.GetMatrix();

            EigenValueUtils eg = new EigenValueUtils();
            try
            {
                eg.CalculateEigenValue(m);
                return eg.CalcEigenValuesDist();
            }
            catch (SystemException ex)
            {
                return new SortedDictionary<double, int>();
            }
        }

        protected override SortedDictionary<UInt32, UInt32> CalculateDegreeDistribution()
        {
            // TODO return ArrayCntAdjacentCntVertexes(0, 0);
            return new SortedDictionary<uint, uint>();
        }

        protected override SortedDictionary<Double, UInt32> CalculateClusteringCoefficientDistribution()
        {
            SortedDictionary<double, uint> result = new SortedDictionary<double, uint>();

            for (int i = 0; i < container.Size; ++i)
            {
                double dresult = Math.Round(ClusterringCoefficientOfVertex(i), 4);
                if (result.Keys.Contains(dresult))
                    ++result[dresult];
                else
                    result.Add(dresult, 1);
            }

            return result;
        }

        protected override SortedDictionary<UInt32, UInt32> CalculateConnectedComponentDistribution()
        {
            // TODO return AmountConnectedSubGraphs(0, 0);
            return new SortedDictionary<uint, uint>();
        }

        protected override SortedDictionary<UInt32, UInt32> CalculateDistanceDistribution()
        {
            if (-1 == avgPath)
            {
                CountPathDistribution();
            }

            // TODO return pathDistribution;
            return new SortedDictionary<uint, uint>();
        }

        protected override SortedDictionary<UInt32, UInt32> CalculateTriangleByVertexDistribution()
        {
            // TODO
            /*SortedDictionary<int, int> result = new SortedDictionary<int, int>();
            for (int i = 0; i < container.Size; ++i)
            {
                int triangleCountOfVertex = (int)Count3CycleOfVertex(i, 0)[0];
                if (result.Keys.Contains(triangleCountOfVertex))
                    ++result[triangleCountOfVertex];
                else
                    result.Add(triangleCountOfVertex, 1);
            }

            return result;*/
            return new SortedDictionary<uint, uint>();
        }

        // TODO TODO TODO
        // Закрытая часть класса (не из общего интерфейса). //

        private double avgPath = -1;
        private int diameter = -1;
        private SortedDictionary<int, int> pathDistribution = new SortedDictionary<int, int>();

        private void CountPathDistribution()
        {
            double avgPath = 0;
            int diameter = 0, countOfWays = 0;

            for (int i = 0; i < container.Size; ++i)
            {
                for (int j = i + 1; j < container.Size; ++j)
                {
                    int way = container.CalculateMinimalPathLength(i, j);
                    if (way == -1)
                        continue;
                    if (pathDistribution.ContainsKey(way))
                        pathDistribution[way]++;
                    else
                        pathDistribution.Add(way, 1);

                    if (way > diameter)
                        diameter = way;

                    avgPath += way;
                    ++countOfWays;
                }
            }

            this.avgPath = avgPath / countOfWays;
            this.diameter = diameter;
        }

        // Возвращает степень данного узла на данном уровне (в соответствующем кластере).
        private double VertexDegree(int vertexNumber, int level)
        {
            double result = 0;
            int vertexIndex = 0, nodeNumber = 0;
            for (int i = container.Level - 1; i >= level; --i)
            {
                vertexIndex = container.TreeIndex(vertexNumber, i + 1) % container.BranchingIndex;
                nodeNumber = container.TreeIndex(vertexNumber, i);
                BitArray node = container.TreeNode(i, nodeNumber);
                result += container.Links(vertexIndex, i, nodeNumber) *
                    Math.Pow(container.BranchingIndex, container.Level - i - 1);
            }

            return result;
        }

        // Возвращает распределение степеней.
        // Распределение степеней вычисляется в данном узле данного уровня.
        private SortedDictionary<int, int> ArrayCntAdjacentCntVertexes(int numberNode, int level)
        {
            if (level == container.Level)
            {
                SortedDictionary<int, int> returned = new SortedDictionary<int, int>();
                returned[0] = 1;
                return returned;
            }
            else
            {
                BitArray node = container.TreeNode(level, numberNode);

                SortedDictionary<int, int> arraysReturned = new SortedDictionary<int, int>();
                SortedDictionary<int, int> array = new SortedDictionary<int, int>();
                int powPK = Convert.ToInt32(Math.Pow(container.BranchingIndex, container.Level - level - 1));

                for (int i = numberNode * container.BranchingIndex; i < container.BranchingIndex * (numberNode + 1); i++)
                {
                    int nodeNumberi = i - numberNode * container.BranchingIndex;
                    array = ArrayCntAdjacentCntVertexes(i, level + 1);
                    int countAjacentsThisnode = container.CountConnectedBlocks(node, nodeNumberi);
                    foreach (KeyValuePair<int, int> kvt in array)
                    {
                        int key = kvt.Key + countAjacentsThisnode * powPK;
                        if (arraysReturned.ContainsKey(key))
                            arraysReturned[key] += kvt.Value;
                        else
                            arraysReturned.Add(key, kvt.Value);
                    }

                }
                return arraysReturned;
            }
        }

        // Возвращает информацию о пути подграфа (реализована рекурсивным образом).
        // Используется алгоритм Флойда для вычисления минимальных путей между вершинами графа.
        private long[] GetSubgraphsPathInfo(int level, long nodeNumber)
        {
            //resultArr's and tempinfo's 
            //1 element is current paths, that can't minimized, lengths sum
            //2 temp paths count, that have chance to be minimized
            //3 >2 paths' lengths sum
            long[] resultArr = { 0, 0, 0 };
            long[] tempInfo = { 0, 0, 0 };

            // Если это не лист дерева, то проход по всем дочерным узлам (рекурсивный вызов).
            if (level < container.Level - 1)
            {
                for (int i = 0; i < container.BranchingIndex; i++)
                {
                    tempInfo = GetSubgraphsPathInfo(level + 1, nodeNumber * container.BranchingIndex + i);

                    resultArr[0] += tempInfo[0];
                    if (container.NodeChildAdjacentsCount(level, nodeNumber, i) > 0)
                    {
                        resultArr[0] += tempInfo[1] * 2;
                    }
                    else
                    {
                        resultArr[1] += tempInfo[1];
                        resultArr[2] += tempInfo[2];
                    }
                }
            }

            // Получение суммы длин минимальных путей (и дополнительной информации) для данного узла.
            tempInfo = Engine.FloydMinPath(container.NodeMatrix(level, nodeNumber));

            double tempPow = Math.Pow(container.BranchingIndex, container.Level - level - 1);
            resultArr[0] += tempInfo[0] * Convert.ToInt64(Math.Pow(tempPow, 2));
            resultArr[1] += tempInfo[1] * Convert.ToInt64(Math.Pow(tempPow, 2));
            resultArr[2] += tempInfo[2] * Convert.ToInt64(Math.Pow(tempPow, 2));

            return resultArr;
        }

        private SortedDictionary<int, int> AmountConnectedSubGraphs(int numberNode, int level)
        {
            SortedDictionary<int, int> retArray = new SortedDictionary<int, int>();

            if (level == container.Level)
            {
                retArray[1] = 1;
                return retArray;
            }
            BitArray node = container.TreeNode(level, numberNode);

            bool haveOne = false;
            for (int i = 0; i < container.BranchingIndex; i++)
            {
                if (container.CountConnectedBlocks(node, i) == 0)
                {
                    SortedDictionary<int, int> array = AmountConnectedSubGraphs(numberNode * container.BranchingIndex + i,
                        level + 1);

                    foreach (KeyValuePair<int, int> kvt in array)
                    {
                        if (retArray.Keys.Contains(kvt.Key))
                            retArray[kvt.Key] += kvt.Value;
                        else
                            retArray.Add(kvt.Key, kvt.Value);
                    }
                }
                else
                    haveOne = true;
            }

            if (haveOne)
            {
                int powPK = Convert.ToInt32(Math.Pow(container.BranchingIndex, container.Level - level - 1));
                EngineForConnectedComp engForConnectedComponent = new EngineForConnectedComp();
                ArrayList arrConnComp = engForConnectedComponent.GetCountConnSGruph(container.NodeAdjacencyLists(node),
                    container.BranchingIndex);
                for (int i = 0; i < arrConnComp.Count; i++)
                {
                    int countConnCompi = (int)arrConnComp[i];
                    if (retArray.Keys.Contains(countConnCompi * powPK))
                        retArray[countConnCompi * powPK] += 1;
                    else
                        retArray.Add(countConnCompi * powPK, 1);
                }
            }

            return retArray;
        }

        // ???????????????
        private SortedDictionary<int, int> AmountConnectedSubGraphsPerLevel(int numberNode,
            int level)
        {
            SortedDictionary<int, int> retArray = new SortedDictionary<int, int>();

            if (level == this.container.Level)
            {
                retArray[1] = 1;
                return retArray;
            }
            BitArray node = container.TreeNode(level, numberNode);

            int powPK = Convert.ToInt32(Math.Pow(container.BranchingIndex,
                container.Level - level - 1));
            EngineForConnectedComp engForConnectedComponent = new EngineForConnectedComp();
            ArrayList arrConnComp =
                engForConnectedComponent.GetCountConnSGruph(container.NodeAdjacencyLists(node),
                container.BranchingIndex);
            for (int i = 0; i < arrConnComp.Count; i++)
            {
                int countConnCompi = (int)arrConnComp[i];
                if (retArray.Keys.Contains(countConnCompi * powPK))
                    retArray[countConnCompi * powPK] += 1;
                else
                    retArray.Add(countConnCompi * powPK, 1);
            }

            return retArray;
        }

        // Возвращает число циклов порядка 3 в нулевом элементе SortedDictionary<int, double>.
        // Число циклов вычисляется в данном узле данного уровня.
        private SortedDictionary<int, double> Count3Cycle(int numberNode, int level)
        {
            SortedDictionary<int, double> retArray = new SortedDictionary<int, double>();
            retArray[0] = 0; // count cycles
            retArray[1] = 0; // count edges

            if (level == container.Level)
            {
                return retArray;
            }
            else
            {
                double countCycle = 0;
                double[] countEdge = new double[container.BranchingIndex];
                int countOne = 0;
                double powPK = Math.Pow(container.BranchingIndex, container.Level - level - 1);
                BitArray node = container.TreeNode(level, numberNode);

                for (int i = numberNode * container.BranchingIndex; i < container.BranchingIndex * (numberNode + 1); i++)
                {
                    SortedDictionary<int, double> arr = new SortedDictionary<int, double>();
                    arr = Count3Cycle(i, level + 1);
                    countEdge[i - numberNode * container.BranchingIndex] = arr[1];
                    retArray[0] += arr[0];
                    retArray[1] += arr[1];
                }
                for (int i = 0; i < (container.BranchingIndex * (container.BranchingIndex - 1) / 2); i++)
                {
                    countOne += (node[i]) ? 1 : 0;
                }
                retArray[1] += countOne * powPK * powPK;


                for (int i = numberNode * container.BranchingIndex; i < container.BranchingIndex * (numberNode + 1); i++)
                {
                    for (int j = (i + 1); j < container.BranchingIndex * (numberNode + 1); j++)
                    {
                        if (container.IsConnectedTwoBlocks(node, i - numberNode * container.BranchingIndex,
                            j - numberNode * container.BranchingIndex))
                        {
                            countCycle += (countEdge[i - numberNode * container.BranchingIndex] +
                                countEdge[j - numberNode * container.BranchingIndex]) * powPK;

                            for (int k = (j + 1); k < container.BranchingIndex * (numberNode + 1); k++)
                            {
                                if (container.IsConnectedTwoBlocks(node, j - numberNode * container.BranchingIndex,
                                    k - numberNode * container.BranchingIndex)
                                    && container.IsConnectedTwoBlocks(node, i - numberNode * container.BranchingIndex,
                                    k - numberNode * container.BranchingIndex))
                                    countCycle += powPK * powPK * powPK;
                            }
                        }
                    }
                }
                retArray[0] += countCycle;

                return retArray;
            }
        }

        // Возвращает число циклов порядка 4 в нулевом элементе SortedDictionary<int, double>.
        // Число циклов вычисляется в данном узле данного уровня.
        private SortedDictionary<int, double> Count4Cycle(int nodeNumber, int level)
        {
            SortedDictionary<int, double> arrayReturned = new SortedDictionary<int, double>();
            arrayReturned[0] = 0; // число циклов порядка 4
            arrayReturned[1] = 0; // число путей длиной 1 (ребер)
            arrayReturned[2] = 0; // число путей длиной 2

            if (level == container.Level)
            {
                return arrayReturned;
            }
            else
            {
                SortedDictionary<int, SortedDictionary<int, double>> array =
                    new SortedDictionary<int, SortedDictionary<int, double>>();
                int bIndex = container.BranchingIndex;

                for (int i = nodeNumber * bIndex; i < (nodeNumber + 1) * bIndex; ++i)
                {
                    array[i] = Count4Cycle(i, level + 1);
                    arrayReturned[0] += array[i][0];
                    arrayReturned[1] += array[i][1];
                    arrayReturned[2] += array[i][2];
                }

                BitArray node = container.TreeNode(level, nodeNumber);
                double powPK = Math.Pow(container.BranchingIndex, container.Level - level - 1);

                for (int i = nodeNumber * bIndex; i < (nodeNumber + 1) * bIndex; ++i)
                {
                    for (int j = i + 1; j < (nodeNumber + 1) * bIndex; ++j)
                    {
                        if (container.IsConnectedTwoBlocks(node, i - nodeNumber * bIndex, j - nodeNumber * bIndex))
                        {
                            arrayReturned[0] += (array[i][2] + array[j][2]) * powPK;
                            arrayReturned[0] += 2 * array[i][1] * array[j][1];

                            arrayReturned[0] += Math.Pow(powPK * (powPK - 1) / 2, 2);

                            arrayReturned[1] += powPK * powPK;

                            arrayReturned[2] += 2 * powPK * (array[i][1] + array[j][1]);
                            arrayReturned[2] += powPK * powPK * (powPK - 1);
                        }

                        for (int k = j + 1; k < (nodeNumber + 1) * bIndex; ++k)
                        {
                            if (container.IsConnectedTwoBlocks(node, i - nodeNumber * bIndex, j - nodeNumber * bIndex) &&
                                container.IsConnectedTwoBlocks(node, j - nodeNumber * bIndex, k - nodeNumber * bIndex) &&
                                container.IsConnectedTwoBlocks(node, i - nodeNumber * bIndex, k - nodeNumber * bIndex))
                            {
                                arrayReturned[0] += 2 * (array[i][1] + array[j][1] + array[k][1]) * powPK * powPK;
                            }

                            if (container.IsConnectedTwoBlocks(node, i - nodeNumber * bIndex, j - nodeNumber * bIndex) &&
                                container.IsConnectedTwoBlocks(node, j - nodeNumber * bIndex, k - nodeNumber * bIndex))
                            {
                                arrayReturned[0] += powPK * powPK * powPK * (powPK - 1) / 2;

                                arrayReturned[2] += powPK * powPK * powPK;
                            }

                            if (container.IsConnectedTwoBlocks(node, i - nodeNumber * bIndex, k - nodeNumber * bIndex) &&
                                container.IsConnectedTwoBlocks(node, k - nodeNumber * bIndex, j - nodeNumber * bIndex))
                            {
                                arrayReturned[0] += powPK * powPK * powPK * (powPK - 1) / 2;

                                arrayReturned[2] += powPK * powPK * powPK;
                            }

                            if (container.IsConnectedTwoBlocks(node, i - nodeNumber * bIndex, j - nodeNumber * bIndex) &&
                                container.IsConnectedTwoBlocks(node, i - nodeNumber * bIndex, k - nodeNumber * bIndex))
                            {
                                arrayReturned[0] += powPK * powPK * powPK * (powPK - 1) / 2;

                                arrayReturned[2] += powPK * powPK * powPK;
                            }

                            for (int l = k + 1; l < (nodeNumber + 1) * bIndex; ++l)
                            {
                                bool b1 = container.IsConnectedTwoBlocks(node, i - nodeNumber * bIndex, j - nodeNumber * bIndex) &&
                                    container.IsConnectedTwoBlocks(node, j - nodeNumber * bIndex, k - nodeNumber * bIndex) &&
                                    container.IsConnectedTwoBlocks(node, k - nodeNumber * bIndex, l - nodeNumber * bIndex) &&
                                    container.IsConnectedTwoBlocks(node, i - nodeNumber * bIndex, l - nodeNumber * bIndex);
                                bool b2 = container.IsConnectedTwoBlocks(node, i - nodeNumber * bIndex, j - nodeNumber * bIndex) &&
                                    container.IsConnectedTwoBlocks(node, j - nodeNumber * bIndex, l - nodeNumber * bIndex) &&
                                    container.IsConnectedTwoBlocks(node, l - nodeNumber * bIndex, k - nodeNumber * bIndex) &&
                                    container.IsConnectedTwoBlocks(node, i - nodeNumber * bIndex, k - nodeNumber * bIndex);
                                bool b3 = container.IsConnectedTwoBlocks(node, i - nodeNumber * bIndex, l - nodeNumber * bIndex) &&
                                    container.IsConnectedTwoBlocks(node, l - nodeNumber * bIndex, j - nodeNumber * bIndex) &&
                                    container.IsConnectedTwoBlocks(node, j - nodeNumber * bIndex, k - nodeNumber * bIndex) &&
                                    container.IsConnectedTwoBlocks(node, i - nodeNumber * bIndex, k - nodeNumber * bIndex);
                                if (b1)
                                {
                                    arrayReturned[0] += powPK * powPK * powPK * powPK;
                                }

                                if (b2)
                                {
                                    arrayReturned[0] += powPK * powPK * powPK * powPK;
                                }

                                if (b3)
                                {
                                    arrayReturned[0] += powPK * powPK * powPK * powPK;
                                }
                            }
                        }
                    }
                }

                return arrayReturned;
            }
        }

        // Возвращает коэффициент класстеризации для данной вершины (vertexNumber).
        // Вычисляется с помощью числа циклов порядка 3, прикрепленных к данной вершине.
        private double ClusterringCoefficientOfVertex(int vertexNumber)
        {
            double degree = VertexDegree(vertexNumber, 0);
            if (degree == 0 || degree == 1)
                return 0;
            else
                return (2 * Count3CycleOfVertex(vertexNumber, 0)[0]) / (degree * (degree - 1));
        }

        // Возвращает число циклов порядка 3 прикрепленных к данному узлу 
        // в нулевом элементе SortedDictionary<int, double>.
        // Число циклов вычисляется в данном узле данного уровня.
        private SortedDictionary<int, double> Count3CycleOfVertex(int vertexNumber, int level)
        {
            SortedDictionary<int, double> result = new SortedDictionary<int, double>();
            result[0] = 0;  // число циклов 3 прикрепленных к данному узлу
            result[1] = 0;  // число ребер в данном подграфе (такое вычисление повышает эффективность)

            if (level == container.Level)
            {
                return result;
            }
            else
            {
                int numberNode = container.TreeIndex(vertexNumber, level);
                int vertexIndex = container.TreeIndex(vertexNumber, level + 1) % container.BranchingIndex;
                BitArray node = container.TreeNode(level, numberNode);
                double powPK = Math.Pow(container.BranchingIndex, container.Level - level - 1);

                SortedDictionary<int, double> previousResult = Count3CycleOfVertex(vertexNumber, level + 1);
                result[0] += previousResult[0];
                result[1] += previousResult[1];

                double degree = VertexDegree(vertexNumber, level + 1);
                for (int j = numberNode * container.BranchingIndex; j < container.BranchingIndex * (numberNode + 1); ++j)
                {
                    if (container.IsConnectedTwoBlocks(node, vertexIndex, j - numberNode * container.BranchingIndex))
                    {
                        result[0] += container.CalculateNumberOfEdges(level + 1, j);//- numberNode * container.BranchingIndex
                        result[0] += powPK * degree;

                        for (int k = j + 1; k < container.BranchingIndex * (numberNode + 1); ++k)
                        {
                            if (container.IsConnectedTwoBlocks(node, j - numberNode * container.BranchingIndex,
                                k - numberNode * container.BranchingIndex) &&
                                container.IsConnectedTwoBlocks(node, k - numberNode * container.BranchingIndex,
                                vertexIndex))
                            {
                                result[0] += powPK * powPK;
                            }
                        }
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Возвращает коэффициент кластеризации графа.
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        private double ClusteringCoefficientOfVertex(long vert)
        {
            double sum = 0;
            long adjCount = 0;
            //loop over all levels
            for (int level = container.Level - 1; level >= 0; level--)
            {
                //get vertex position in current level
                long vertNodeNum = Convert.ToInt64(Math.Floor(Convert.ToDouble(vert / container.BranchingIndex)));
                int vertNodeInd = Convert.ToInt32(vert % container.BranchingIndex);

                //get vertex adjacent vertexes in current node
                List<int> adjIndexes = container.NodeChildAdjacentsArray(level, vertNodeNum, vertNodeInd);

                long levelVertexCount = Convert.ToInt64(Math.Pow(container.BranchingIndex, container.Level - level - 1));
                //vertex subtree vertexes with adjacent subtrees vertexes
                long vertexSubTreeWithAdjSubTrees = adjCount * levelVertexCount * adjIndexes.Count;
                sum += vertexSubTreeWithAdjSubTrees;
                //add adjacent vertexes count
                adjCount += levelVertexCount * adjIndexes.Count;
                //adjacent subtrees weights
                for (int i = 0; i < container.BranchingIndex; i++)
                {
                    if (adjIndexes.IndexOf(i) != -1)
                    {
                        sum += container.CalculateNumberOfEdges(level + 1, 
                            vertNodeNum * container.BranchingIndex + i);
                    }
                }
                //connectivity of adjacent subtrees
                for (int i = 0; i < container.BranchingIndex; i++)
                {
                    if (adjIndexes.IndexOf(i) != -1)
                    {
                        for (int j = i; j < container.BranchingIndex; j++)
                        {
                            if (i != j && i != vertNodeInd && j != vertNodeInd)
                            {
                                sum += container.AreAdjacent(level, vertNodeNum, i, j) * Math.Pow(levelVertexCount, 2);
                            }
                        }
                    }
                }

                vert = vertNodeNum;
            }
            double vertClustCoef = 0;
            if (adjCount > 1)
            {
                vertClustCoef = 2 * sum / (adjCount * (adjCount - 1));
            }
            else if (adjCount == 1)
            {
                vertClustCoef = sum;
            }

            return vertClustCoef;
        }

        /// <summary>
        /// Возвращает собственные значения.
        /// </summary>
        /// <param name="bitArr"></param>
        /// <param name="mBase"></param>
        /// <param name="EigValue"></param>
        private List<double> CalcEigenValue(BitArray bitArr, int mBase)
        {
            List<double> EigValue = new List<double>();

            List<double> basicEigValue = new List<double>(mBase);
            List<double> eigValueE = new List<double>(mBase);
            for (int i = 1; i < mBase; ++i)
            {
                eigValueE.Add(0);
            }
            eigValueE.Add(mBase);
            int bitArrSize = bitArr.Count;
            if (bitArr[0] == false)
            {
                if (bitArr[1] == false)
                {
                    for (int i = 0; i < mBase; ++i)
                    {
                        basicEigValue.Add(0);
                    }
                }
                else
                {
                    for (int i = 0; i < mBase; ++i)
                    {
                        basicEigValue.Add(1);
                    }
                }
            }
            else
            {
                if (bitArr[1] == false)
                {
                    for (int i = 1; i < mBase; ++i)
                    {
                        basicEigValue.Add(-1);
                    }
                    basicEigValue.Add(mBase - 1);
                }
                else
                {
                    for (int i = 1; i < mBase; ++i)
                    {
                        basicEigValue.Add(0);
                    }
                    basicEigValue.Add(mBase);
                }
            }
            int size = mBase;
            BitArray BA = new BitArray(bitArrSize + 1);
            for (int i = 0; i < bitArrSize; ++i)
                BA[i] = bitArr[i];
            BA.Set(bitArrSize, false);
            int x = 1;
            while (x != bitArrSize)
            {
                foreach (int elemE in eigValueE)
                {
                    int t1, t2;
                    if (BA[x] == true)
                        t1 = 1;
                    else
                        t1 = 0;
                    if (BA[x + 1] == true)
                        t2 = 1;
                    else
                        t2 = 0;
                    foreach (int elem in basicEigValue)
                        EigValue.Add(elem * elemE - t1 + t2);
                }
                ++x;
                basicEigValue.Clear();
                basicEigValue.InsertRange(0, EigValue);
                EigValue.Clear();

            }

            EigValue.InsertRange(0, basicEigValue);
            return EigValue;
        }

        // Возвращает число циклов данного порядка, с помощью собственных значений.
        public double CalcCyclesCount(int cycleLength)
        {
            BitArray vector = new BitArray(container.Level);

            for (int i = 0; i < container.HierarchicTree.Length; i++)
            {
                vector[i] = container.HierarchicTree[i][0][0];
            }

            List<double> eigValue = CalcEigenValue(vector, container.BranchingIndex);

            double total = 0;
            foreach (int i in eigValue)
            {
                total += Math.Pow(i, cycleLength);
            }
            return total / (2 * cycleLength);
        }

        // Возвращает среднее степеней. Не используется.
        public double AverageDegree()
        {
            return container.CalculateNumberOfEdges() * 2 / container.Size;
        }

        // Возвращает сумму минимальных путей. Не используется.
        public double MinPathsSum()
        {
            long[] pathsInfo = GetSubgraphsPathInfo(0, 0);
            return pathsInfo[0] + pathsInfo[2];
        }
    }
}
