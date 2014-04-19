﻿using System;
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
            throw new NotImplementedException();
            /*log.Info("Getting count of cycles - order 4.");
            return (long)container.Get4CirclesCount();*/
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
            throw new NotImplementedException();
            /*log.Info("Getting triangles distribution.");
            SortedDictionary<int, int> result = new SortedDictionary<int, int>();

            for (uint i = 0; i < container.Size; ++i)
            {
                int triangleCountOfVertex = (int)container.Get3CirclesCountWithVertex(i);
                if (result.Keys.Contains(triangleCountOfVertex))
                    ++result[triangleCountOfVertex];
                else
                    result.Add(triangleCountOfVertex, 1);
            }

            return result;*/
        }

        // Возвращается распределение коэффициентов кластеризации графа. Реализовано.
        public override SortedDictionary<double, int> GetClusteringCoefficient()
        {
            throw new NotImplementedException();
            /*log.Info("Getting clustering coefficients.");
            return container.GetClusteringCoefficient();*/
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
                            retArray[1] += countLeaves[i] * countLeaves[j];
                        }
                    }
                }

                for (int i = 0; i < branchSize; ++i)
                {
                    for (int j = i + 1; j < branchSize; ++j)
                    {
                        if (container.AreConnectedTwoBlocks(node, branchSize, i, j))
                        {
                            retArray[0] += countLeaves[i] * countEdge[j] +
                                countLeaves[j] * countEdge[i];

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
        
    }
}
