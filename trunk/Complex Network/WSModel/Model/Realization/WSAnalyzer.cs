using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Model.WSModel.Realization;
using CommonLibrary.Model;
using Algorithms;
using log4net;

namespace Model.WSModel.Realization
{
    // Реализация анализатора (WS).
    public class WSAnalyzer : AbstarctGraphAnalyzer
    {
        // Организация работы с лог файлом.
        protected static readonly new ILog log = log4net.LogManager.GetLogger(typeof(WSAnalyzer));

        // Контейнер, в котором содержится граф конкретной модели (WS).
        private WSContainer container;

        // Конструктор, получающий контейнер графа.
        public WSAnalyzer(WSContainer c)
        {
            log.Info("Creating WSAnalizer object.");
            container = c;
            CountNeighbourships();
        }

        // Контейнер, в котором содержится сгенерированный граф (полученный от генератора).
        public override IGraphContainer Container
        {
            get { return container; }
            set
            {
                container = (WSContainer)value;
                CountNeighbourships();
            }
        }

        // Возвращается средняя длина пути в графе. Реализовано.
        public override double GetAveragePath()
        {
            log.Info("Getting average path length.");

            if (-1 == avgPathLenght)
            {
                CountEssentialOptions();
            }

            return avgPathLenght;
        }

        // Возвращается диаметр графа. Реализовано.
        public override int GetDiameter()
        {
            log.Info("Getting diameter.");

            if (-1 == diameter)
            {
                CountEssentialOptions();
            }

            return diameter;
        }

        // Возвращается число циклов длиной 3 в графе. Реализовано.
        public override int GetCycles3()
        {
            log.Info("Getting count of cycles - order 3.");

            if (-1 == cyclesOfOrder3)
            {
                ClusteringCoefficient();
                CyclesOfOrder3();
            }

            return cyclesOfOrder3;
        }

        // Возвращается число циклов длиной 4 в графе. Реализовано.
        public override int GetCycles4()
        {
            log.Info("Getting count of cycles - order 4.");

            if (-1 == cyclesOfOrder4)
            {
                CountCycles4();
            }

            return cyclesOfOrder4;
        }

        // Возвращается массив собственных значений матрицы смежности. Реализовано.
        public override ArrayList GetEigenValues()
        {
            log.Info("Getting eigen values array.");
            bool[,] m = container.GetMatrix();

            EigenValueUtils eg = new EigenValueUtils();

            try
            {
                return eg.CalculateEigenValue(m);

            }
            catch (Exception ex)
            {
                log.Error(ex);
                return new ArrayList();
            }
        }

         // Возвращается распределение длин между собственными значениями. Реализовано
        public override SortedDictionary<double, int> GetDistEigenPath()
        {
            log.Info("Getting distances between eigen values.");

            bool[,] m = container.GetMatrix();

            EigenValueUtils eg = new EigenValueUtils();


            try
            {

                eg.CalculateEigenValue(m);
                return eg.CalcEigenValuesDist();

            }
            catch (Exception ex)
            {
                log.Error(ex);
                return new SortedDictionary<double, int>();
            }
        }


        // Возвращается степенное распределение графа. Реализовано.
        public override SortedDictionary<int, int> GetDegreeDistribution()
        {
            log.Info("Getting degree distribution.");
            return DegreeDistribution();
        }

        // Возвращается распределение коэффициентов кластеризации графа. Реализовано.
        public override SortedDictionary<double, int> GetClusteringCoefficient()
        {
            log.Info("Getting clustering coefficients.");

            if (-1 == cyclesOfOrder3)
            {
                ClusteringCoefficient();
            }
            return coefficients;
        }

        // Возвращается распределение чисел связанных полных подграфов в графе. Реализовано.
        public override SortedDictionary<int, int> GetFullSubGraph()
        {
            log.Info("Getting clustering coefficients.");

            if (-1 == cyclesOfOrder3)
            {
                ClusteringCoefficient();
            }
            return fullSubgraphs;
        }

        // Возвращается распределение чисел связанных полных подграфов в графе. Реализовано.
        public override SortedDictionary<int, int> GetConnSubGraph()
        {
            log.Info("Getting clustering coefficients.");

            if (-1 == connSubgraphsOrder)
            {
                CountConnSubGraphs();
            }
            return connSubgraphs;
        }

        // Возвращается распределение длин минимальных путей в графе. Реализовано.
        public override SortedDictionary<int, int> GetMinPathDist()
        {
            log.Info("Getting minimal distances between vertices.");

            if (-1 == avgPathLenght)
            {
                CountEssentialOptions();
            }

            return vertexDistances;
        }


        // Закрытая часть класса (не из общего интерфейса). //

        private List<int> neighbourship;

        private double avgPathLenght = -1;
        private int diameter = -1;
        private int cyclesOfOrder3 = -1;
        private int cyclesOfOrder4 = -1;
        private int connSubgraphsOrder = -1;
        private SortedDictionary<double, int> coefficients = new SortedDictionary<double, int>();
        private SortedDictionary<int, int> fullSubgraphs = new SortedDictionary<int, int>();
        private SortedDictionary<int, int> connSubgraphs = new SortedDictionary<int, int>();
        private SortedDictionary<int, int> vertexDistances = new SortedDictionary<int, int>();

        // Внутренный тип для работы BFS алгоритма.
        private class Node
        {
            public int ancestor = -1;
            public int lenght = -1;

            public Node() { }
        }

        // Реализация BFS алгоритма.
        private void BFS(int i, List<Node> nodes)
        {
            nodes[i].lenght = 0;
            nodes[i].ancestor = 0;
            Queue<int> q = new Queue<int>();
            q.Enqueue(i);
            int u;
            while (q.Count != 0)
            {
                u = q.Dequeue();
                List<int> l = container.Neighbours(u);
                for (int j = 0; j < l.Count; ++j)
                {
                    if (nodes[l[j]].lenght == -1)
                    {
                        nodes[l[j]].lenght = nodes[u].lenght + 1;
                        nodes[l[j]].ancestor = u;
                        q.Enqueue(l[j]);
                    }
                }
            }
        }

        // Реализация DFS алгоритма.
        private void DFS(int v, List<bool> used) 
        {
	        used[v] = true;
            ++connSubgraphsOrder;
            List<int> l = container.Neighbours(v);
	        for (int i = 0; i < l.Count; ++i) 
            {
                int neighbour = l[i];
		        if (!used[neighbour])
			        DFS(neighbour, used);
            }
    	}

        private void CountCycles4()
        {
            cyclesOfOrder4 = 0;
            for (int i = 0; i < container.Size; ++i)
                DFSforCycles(i);

            cyclesOfOrder4 /= 8;
        }

        // Реализация DFS алгоритма.
        private void DFSforCycles(int v)
        {
            List<int> firstLevel = container.Neighbours(v);
            for (int i = 0; i < firstLevel.Count; ++i)
            {
                List<int> secondLevel = container.Neighbours(firstLevel[i]);
                for (int j = 0; j < secondLevel.Count; ++j)
                {
                    if (secondLevel[j] == v)
                        continue;

                    List<int> thirdLevel = container.Neighbours(secondLevel[j]);
                    for (int k = 0; k < thirdLevel.Count; ++k)
                    {
                        if (thirdLevel[k] == v || thirdLevel[k] == firstLevel[i])
                            continue;

                        List<int> neighbours = container.Neighbours(thirdLevel[k]);
                        if(neighbours.Contains(v))
                            ++cyclesOfOrder4;
                    }
                }
            }
        }

        // Считает распределение подграфов в графе.
        private void CountConnSubGraphs()
        {
            List<bool> used = new List<bool>(container.Size);
            for (int i = 0; i < container.Size; ++i)
		        used.Insert(i, false);

	        for (int i = 0; i < container.Size; ++i)
                if (!used[i])
                {
                    connSubgraphsOrder = 0;
                    DFS(i, used);

                    if (connSubgraphsOrder != 0)
                    {
                        if (connSubgraphs.ContainsKey(connSubgraphsOrder))
                            connSubgraphs[connSubgraphsOrder]++;
                        else
                            connSubgraphs.Add(connSubgraphsOrder, 1);
                    }
                }
        }

        private void CountNeighbourships()
        {
            neighbourship = new List<int>(container.Size);

            for (int i = 0; i < container.Size; ++i)
            {
                neighbourship.Insert(i, 0);
                for (int j = 0; j < container.Size; j++)
                    if (container.AreNeighbours(i, j))
                        neighbourship[i]++;
            }
        }

        // Выполняет подсчет сразу 4 свойств - средняя длина пути, диаметр, циклов 4 и пути между вершинами.
        // Нужно вызвать перед получением этих свойств не изнутри.
        private void CountEssentialOptions()
        {
            double avg = 0;
            int diametr = 0, k = 0;
            for (int i = 0; i < container.Size; ++i)
            {
                List<Node> nodes = new List<Node>();
                for (int p = 0; p < container.Size; p++)
                    nodes.Insert(p, new Node());
                BFS(i, nodes);

                for (int j = i; j < container.Size; ++j)
                {
                    Node nd = nodes[j];
                    int way = nd.lenght; ;
                    if (way == -1)
                        continue;
                    if (way > diametr)
                        diametr = way;

                    if (way > 0)
                    {
                        avg += way;
                        ++k;

                        if (vertexDistances.ContainsKey(way))
                            vertexDistances[way]++;
                        else
                            vertexDistances.Add(way, 1);
                    }
                }
            }

            avg /= k;
            double avgD = avg * 10000;
            int avgI = Convert.ToInt32(avgD);
            avgPathLenght = (double)avgI / 10000;
            diameter = diametr;
        }

        // Возвращает распределение степеней
        private SortedDictionary<int, int> DegreeDistribution()
        {
            SortedDictionary<int, int> degreeDistribution = new SortedDictionary<int, int>();
            int degreeCount = 0;

            for (int i = 0; i < container.Size; ++i)
            {
                degreeCount = ReturnNeighboursCount(i);
                if (degreeCount != 0)
                {
                    if (degreeDistribution.ContainsKey(degreeCount))
                        degreeDistribution[degreeCount]++;
                    else
                        degreeDistribution.Add(degreeCount, 1);
                }
            }

            return degreeDistribution;
        }

        // Возвращает число соседей для данной вершины.
        private int ReturnNeighboursCount(int i)
        {
            return container.CountDegree(i);
        }

        // Вычисление коэффициента кластеризации для графа.
        private void ClusteringCoefficient()
        {
            cyclesOfOrder3 = 0;

            for (int i = 0; i < container.Size; ++i)
                ClusteringCoeffForVertex(i);
        }

        // Вычисление коэффициента кластеризации для данной вршины.
        // Паралелльно вычисляются циклы 3, распределение чисел соединенных подграфов.
        private void ClusteringCoeffForVertex(int index)
        {
            int i = index;
            int neighbours = neighbourship[i];
            if (!(neighbours > 0))
                return;

            int E = 0;
            int K = (neighbours == 1) ? 1 : neighbours * (neighbours - 1) / 2;

            List<int> nVec = container.Neighbours(i);

            int size = nVec.Count;
            for (int k = 0; k < size; ++k)
            {
                int counter = 0;
                for (int j = k + 1; j < size; ++j)
                    if (container.AreNeighbours(nVec[k], nVec[j]))
                        ++counter;

                E += counter;
            }

            cyclesOfOrder3 += E;

            double clusteringCoef = (double)E / K;

            double coefD = clusteringCoef * 10000;
            int coefI = Convert.ToInt32(coefD);
            double clusteringCoefI = (double)coefI / 10000;

            if (coefficients.ContainsKey(clusteringCoefI))
                coefficients[clusteringCoefI]++;
            else
                coefficients.Add(clusteringCoefI, 1);

            if (E / K == 1)
            {
                if (fullSubgraphs.ContainsKey(neighbours + 1))
                    fullSubgraphs[neighbours + 1]++;
                else
                    fullSubgraphs.Add(neighbours + 1, 1);
            }
        }

        // Возвращает число циклов 3.
        private void CyclesOfOrder3()
        {
            cyclesOfOrder3 /= 3;
        }

        // Возвращает степень максимального соединенного подграфа.
        private int MaxConSubGraph()
        {
            int size = fullSubgraphs.Count;
            int max = 0;
            for (int i = 0; i < size; ++i)
            {
                int element = fullSubgraphs.ElementAt(i).Key;
                if (max < element)
                    max = element;
            }

            return max;
        }
    }
}
