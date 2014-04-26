using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using CommonLibrary.Model;
using log4net;

namespace Model.NonRegularHierarchicModel.Realization
{
    // Реализация анализатора (Block-Hierarchic Non Regular).
    public class NonRegularHierarchicAnalyzer : AbstarctGraphAnalyzer
    {
        // Организация работы с лог файлом.
        protected static readonly new ILog log = log4net.LogManager.GetLogger(typeof(NonRegularHierarchicAnalyzer));

        // Контейнер, в котором содержится граф конкретной модели (Block-Hierarchic Non Regular).
        private NonRegularHierarchicContainer container;

        public NonRegularHierarchicAnalyzer(NonRegularHierarchicContainer c)
        {
            log.Info("Creating NonRegularHierarchicAnalyzer object.");
            container = c;
        }

        // Контейнер, в котором содержится сгенерированный граф (полученный от генератора).
        public override AbstractGraphContainer Container
        {
            get { return container; }
            set { container = (NonRegularHierarchicContainer)value; }
        }

        // Возвращается средняя длина пути в графе. Реализовано.
        public override double GetAveragePath()
        {
            log.Info("Getting average path length.");

            if (-1 == avgPath)
            {
                CountPathDistribution();
            }

            return Math.Round(avgPath, 14);
        }

        // Возвращается диаметр графа. Реализовано.
        public override int GetDiameter()
        {
            log.Info("Getting diameter.");

            if (-1 == diameter)
            {
                CountPathDistribution();
            }

            return diameter;
        }

        // Возвращается число циклов длиной 3 в графе. Реализовано.
        public override long GetCycles3()
        {
            log.Info("Getting count of cycles - order 3.");
            return (long)Count3Cycle(0, 0)[0];
        }

        // Возвращается число циклов длиной 4 в графе. Реализовано.
        public override long GetCycles4()
        {
            log.Info("Getting count of cycles - order 4.");
            return (long)Count4Cycle(0, 0)[0];
        }

        // Возвращает среднее степеней. Не используется.
        public double GetAverageDegree()
        {
            return (double)container.CountEdges(0, 0) * 2 / container.Size;
        }

        // Возвращается степенное распределение графа. Реализовано.
        public override SortedDictionary<int, int> GetDegreeDistribution()
        {
            log.Info("Getting degree distribution.");
            return ArrayCntAdjacentCntVertexes(0, 0);
        }

        // Возвращает распределение степеней.
        // Распределение степеней вычисляется в данном узле данного уровня.
        private SortedDictionary<int, int> ArrayCntAdjacentCntVertexes(int numberNode, int currentLevel)
        {
            if (currentLevel == container.Level)
            {
                SortedDictionary<int, int> returned = new SortedDictionary<int, int>();
                returned[0] = 1;
                return returned;
            }
            else
            {
                BitArray node = container.TreeNode(currentLevel, numberNode);

                SortedDictionary<int, int> arraysReturned = new SortedDictionary<int, int>();
                SortedDictionary<int, int> array = new SortedDictionary<int, int>();
                int branchSize = container.Branches[currentLevel][numberNode];
                int branchStartPnt = container.FindBranches(currentLevel, numberNode);

                for (int i = 0; i < branchSize; ++i)
                {
                    array = ArrayCntAdjacentCntVertexes(branchStartPnt + i, currentLevel + 1);
                    int countAjacentsNodes = container.CountConnectedBlocks(node, branchSize, i);
                    foreach (KeyValuePair<int, int> kvt in array)
                    {
                        int key = kvt.Key;
                        for (int j = 0; j < branchSize; ++j)
                        {
                            int counter = 0;
                            if(container.AreConnectedTwoBlocks(node, branchSize, i, j))
                            {
                                key += container.CountLeaves(currentLevel + 1, branchStartPnt + j);
                                ++counter;
                            }
                            if (counter == countAjacentsNodes)
                            {
                                break;
                            }
                        }

                        if (arraysReturned.ContainsKey(key))
                        {
                            arraysReturned[key] += kvt.Value;
                        }
                        else
                        {
                            arraysReturned.Add(key, kvt.Value);
                        }
                    }
                }
                
                return arraysReturned;
            }
        }

        // Возвращает распределение триугольников, прикрепленных к вершине.
        public override SortedDictionary<int, int> GetTrianglesDistribution()
        {
            log.Info("Getting triangles distribution.");

            SortedDictionary<int, int> result = new SortedDictionary<int, int>();
            for (int i = 0; i < container.Size; ++i)
            {
                int triangleCountOfVertex = (int)Count3CycleOfVertex(i, 0)[0];
                if (result.Keys.Contains(triangleCountOfVertex))
                    ++result[triangleCountOfVertex];
                else
                    result.Add(triangleCountOfVertex, 1);
            }

            return result;
        }

        // Возвращается распределение коэффициентов кластеризации графа. Реализовано.
        public override SortedDictionary<double, int> GetClusteringCoefficient()
        {
            log.Info("Getting clustering coefficients.");
            SortedDictionary<double, int> result = new SortedDictionary<double, int>();

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

        // Возвращается распределение длин минимальных путей в графе. Реализовано.
        public override SortedDictionary<int, int> GetMinPathDist()
        {
            log.Info("Getting minimal distances between vertices.");

            if (-1 == avgPath)
            {
                CountPathDistribution();
            }

            return pathDistribution;
        }

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
                    int way = container.MinimumWay(i, j);
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

        // Возвращает число циклов порядка 3 в нулевом элементе SortedDictionary<int, double>.
        // Число циклов вычисляется в данном узле данного уровня.
        private SortedDictionary<int, double> Count3Cycle(int level, int numberNode)
        {
            SortedDictionary<int, double> retArray = new SortedDictionary<int, double>();
            retArray[0] = 0; // count cycles
            retArray[1] = 0; // count edges
            retArray[2] = 0; // count leaves

            if (level == container.Level)
            {
                retArray[2] = 1;
                return retArray;
            }
            else
            {
                int branchSize = container.Branches[level][numberNode];
                int branchStart = container.FindBranches(level, numberNode);
                BitArray node = container.TreeNode(level, numberNode);
                double[] countEdge = new double[branchSize];
                double[] countLeaves = new double[branchSize];

                for (int i = 0; i < branchSize; ++i)
                {
                    SortedDictionary<int, double> arr = new SortedDictionary<int, double>();
                    arr = Count3Cycle(level + 1, i + branchStart);
                    countEdge[i] = arr[1];
                    countLeaves[i] = arr[2];
                    retArray[0] += arr[0];
                    retArray[1] += arr[1];
                    retArray[2] += arr[2];
                }

                for (int i = 0; i < branchSize; ++i)
                {
                    for (int j = i + 1; j < branchSize; ++j)
                    {
                        if (container.AreConnectedTwoBlocks(node, branchSize, i, j))
                        {
                            retArray[0] += countLeaves[i] * countEdge[j] +
                                countLeaves[j] * countEdge[i];

                            retArray[1] += countLeaves[i] * countLeaves[j];

                            for (int k = j + 1; k < branchSize; ++k)
                            {
                                if (container.AreConnectedTwoBlocks(node, branchSize, i, k)
                                    && container.AreConnectedTwoBlocks(node, branchSize, j, k))
                                {
                                    retArray[0] += countLeaves[i] * countLeaves[j] * countLeaves[k];
                                }
                            }
                        }
                    }
                }
                return retArray;
            }
        }

        // Возвращает число циклов порядка 4 в нулевом элементе SortedDictionary<int, double>.
        // Число циклов вычисляется в данном узле данного уровня.
        private SortedDictionary<int, double> Count4Cycle(int level, int nodeNumber)
        {
            SortedDictionary<int, double> retArray = new SortedDictionary<int, double>();
            retArray[0] = 0; // число циклов порядка 4
            retArray[1] = 0; // число путей длиной 1 (ребер)
            retArray[2] = 0; // число путей длиной 2
            retArray[3] = 0; // count leaves

            if (level == container.Level)
            {
                retArray[3] = 1;
                return retArray;
            }
            else
            {
                SortedDictionary<int, double> array = new SortedDictionary<int, double>();

                int branchSize = container.Branches[level][nodeNumber];
                int branchStart = container.FindBranches(level, nodeNumber);
                BitArray node = container.TreeNode(level, nodeNumber);

                double[] countEdge = new double[branchSize];
                double[] countLeaves = new double[branchSize];
                double[] countDoubleEdge = new double[branchSize];

                for (int i = 0; i < branchSize; ++i)
                {
                    array = Count4Cycle(level + 1, i + branchStart);
                    retArray[0] += array[0];
                    retArray[1] += array[1];
                    retArray[2] += array[2];
                    retArray[3] += array[3];
                    countEdge[i] = array[1];
                    countDoubleEdge[i] = array[2];
                    countLeaves[i] = array[3];
                }

                for (int i = 0; i < branchSize; ++i)
                {
                    for (int j = i + 1; j < branchSize; ++j)
                    {
                        if (container.AreConnectedTwoBlocks(node, branchSize, i, j))
                        {
                            retArray[0] += countDoubleEdge[i] * countLeaves[j] 
                                + countDoubleEdge[j] * countLeaves[i];
                            retArray[0] += 2 * countEdge[i] * countEdge[j];

                            retArray[0] += countLeaves[i] * (countLeaves[i] - 1) *
                                countLeaves[j] * (countLeaves[j] - 1) / 4;

                            retArray[1] += countLeaves[i] * countLeaves[j];

                            retArray[2] += 2 * (countEdge[i] * countLeaves[j] +
                                countEdge[j] * countLeaves[i]);
                            retArray[2] += (countLeaves[i] * (countLeaves[i] - 1) * countLeaves[j] +
                                countLeaves[j] * (countLeaves[j] - 1) * countLeaves[i]) / 2;
                        }

                        for (int k = j + 1; k < branchSize; ++k)
                        {
                            int countDouble = 0;
                            if (container.AreConnectedTwoBlocks(node, branchSize, i, j) &&
                                container.AreConnectedTwoBlocks(node, branchSize, j, k) &&
                                container.AreConnectedTwoBlocks(node, branchSize, i, k))
                            {
                                retArray[0] += 2 * (countEdge[i] * countLeaves[j] * countLeaves[k] +
                                    countEdge[j] * countLeaves[i] * countLeaves[k] +
                                    countEdge[k] * countLeaves[i] * countLeaves[j]);
                            }

                            if (container.AreConnectedTwoBlocks(node, branchSize, i, j) &&
                                container.AreConnectedTwoBlocks(node, branchSize, j, k))
                            {
                                retArray[0] += countLeaves[i] * countLeaves[k] * 
                                    (countLeaves[j] * (countLeaves[j] - 1) / 2);

                                ++countDouble;
                            }

                            if (container.AreConnectedTwoBlocks(node, branchSize, i, k) &&
                                container.AreConnectedTwoBlocks(node, branchSize, j, k))
                            {
                                retArray[0] += countLeaves[i] * countLeaves[j] *
                                    (countLeaves[k] * (countLeaves[k] - 1) / 2);

                                ++countDouble;
                            }

                            if (container.AreConnectedTwoBlocks(node, branchSize, i, j) &&
                                container.AreConnectedTwoBlocks(node, branchSize, i, k))
                            {
                                retArray[0] += countLeaves[j] * countLeaves[k] *
                                    (countLeaves[i] * (countLeaves[i] - 1) / 2);

                                ++countDouble;                                
                            }

                            retArray[2] += countDouble * countLeaves[i] * countLeaves[j] * countLeaves[k];

                            for (int l = k + 1; l < branchSize; ++l)
                            {
                                int count4clusters = 0;
                                if (container.AreConnectedTwoBlocks(node, branchSize, i, j) &&
                                    container.AreConnectedTwoBlocks(node, branchSize, j, k) &&
                                    container.AreConnectedTwoBlocks(node, branchSize, k, l) &&
                                    container.AreConnectedTwoBlocks(node, branchSize, i, l))
                                {
                                    ++count4clusters;
                                }
                                if (container.AreConnectedTwoBlocks(node, branchSize, i, j) &&
                                    container.AreConnectedTwoBlocks(node, branchSize, j, l) &&
                                    container.AreConnectedTwoBlocks(node, branchSize, k, l) &&
                                    container.AreConnectedTwoBlocks(node, branchSize, i, k))
                                {
                                    ++count4clusters;
                                }
                                if (container.AreConnectedTwoBlocks(node, branchSize, i, l) &&
                                    container.AreConnectedTwoBlocks(node, branchSize, j, l) &&
                                    container.AreConnectedTwoBlocks(node, branchSize, j, k) &&
                                    container.AreConnectedTwoBlocks(node, branchSize, i, k))
                                {
                                    ++count4clusters;
                                }
                                retArray[0] += count4clusters * countLeaves[i] * countLeaves[j] * countLeaves[k] *
                                        countLeaves[l];
                            }
                        }
                    }
                }

                return retArray;
            }
        }

        // Возвращает коэффициент класстеризации для данной вершины (vertexNumber).
        // Вычисляется с помощью числа циклов порядка 3, прикрепленных к данной вершине.
        private double ClusterringCoefficientOfVertex(int vertexNumber)
        {
            SortedDictionary<int, double> result = Count3CycleOfVertex(vertexNumber, 0);
            double count3CyclesOfVertex = result[0];
            double degree = result[1];
            if (degree == 0 || degree == 1)
                return 0;
            else
                return (2 * count3CyclesOfVertex) / (degree * (degree - 1));
        }

        // Возвращает число циклов порядка 3 прикрепленных к данному узлу 
        // в нулевом элементе SortedDictionary<int, double>.
        // Число циклов вычисляется в данном узле данного уровня.
        private SortedDictionary<int, double> Count3CycleOfVertex(int vertexNumber, int level)
        {
            SortedDictionary<int, double> result = new SortedDictionary<int, double>();
            result[0] = 0;  // число циклов 3 прикрепленных к данному узлу
            result[1] = 0;  // степень узла в данном подграфе
            result[2] = 0;  // индекс узла в данном подграфе

            if (level == container.Level)
            {
                result[2] = vertexNumber;
                return result;
            }
            else
            {
                SortedDictionary<int, double> previousResult = Count3CycleOfVertex(vertexNumber, level + 1);
                int vertexIndex = (int)previousResult[2];
                int numberNode = container.TreeIndexStep(vertexIndex, level);
                int branchStart = container.FindBranches(level, numberNode);
                int branchSize = container.Branches[level][numberNode];
                int currentVertexIndex = vertexIndex - branchStart;
                BitArray node = container.TreeNode(level, numberNode);

                result[0] += previousResult[0];
                result[1] += previousResult[1];
                result[2] = numberNode;
                double degree = previousResult[1];
                for (int i = 0; i < branchSize; ++i)
                {
                    if (container.AreConnectedTwoBlocks(node, branchSize, currentVertexIndex, i))
                    {
                        result[1] += container.CountLeaves(level + 1, i + branchStart);
                        result[0] += container.CountEdges(level + 1, i + branchStart);
                        result[0] += container.CountLeaves(level + 1, i + branchStart) * degree;
                        for (int j = i + 1; j < branchSize; ++j)
                        {
                            if (container.AreConnectedTwoBlocks(node, branchSize, i, j) &&
                                container.AreConnectedTwoBlocks(node, branchSize, j, currentVertexIndex))
                            {
                                result[0] += container.CountLeaves(level + 1, i + branchStart) *
                                    container.CountLeaves(level + 1, j + branchStart);
                            }
                        }
                    }
                } 

                return result;
            }
        }

        // Возвращает степень данного узла на данном уровне (в соответствующем кластере).
        private double VertexDegree(int vertexNumber, int level)
        {
            if (level == container.Level)
            {
                return 0;
            }
            else
            {
                double result = 0;
                int nodeNumber = container.TreeIndex(vertexNumber, level);
                int branchStart = container.FindBranches(level, nodeNumber);
                int branchSize = container.Branches[level][nodeNumber];
                int vertexIndex = container.TreeIndex(vertexNumber, level + 1) - branchStart;
                BitArray node = container.TreeNode(level, nodeNumber);
                for (int i = 0; i < branchSize; ++i)
                {
                    if (i != vertexIndex)
                    {
                        if (container.AreConnectedTwoBlocks(node, branchSize, i, vertexIndex))
                        {
                            result += container.CountLeaves(level + 1, i + branchStart);
                        }
                    }
                }

                return result + VertexDegree(vertexNumber, level + 1);
            }
        }

        public int bIndex { get; set; }
    }
}
