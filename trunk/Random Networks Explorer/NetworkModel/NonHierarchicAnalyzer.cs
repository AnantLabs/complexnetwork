using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

using Core.Model;
using NetworkModel.Engine.Eigenvalues;
using NetworkModel.Engine.Cycles;

namespace NetworkModel
{
    public class NonHierarchicAnalyzer : INetworkAnalyzer
    {
        // Контейнер, в котором содержится граф конкретной модели (BA).
        private NonHierarchicContainer container;

        public NonHierarchicAnalyzer() { }

        // Конструктор, получающий контейнер графа.
        public NonHierarchicAnalyzer(NonHierarchicContainer c)
        {
            container = c;
            Initialization();
        }

        // Контейнер, в котором содержится сгенерированный граф (полученный от генератора).
        public INetworkContainer Container
        {
            get { return container; }
            set { container = (NonHierarchicContainer)value; }
        }

        // Возвращается средняя длина пути в графе. Реализовано.
        public Double CalculateAveragePath()
        {
            //log.Info("Getting average path length.");

            if (-1 == avgPath)
            {
                CountEssentialOptions();
            }

            return Math.Round(avgPath, 14);
        }

        // Возвращается диаметр графа. Реализовано.
        public UInt32 CalculateDiameter()
        {
            //log.Info("Getting diameter.");

            if (-1 == diameter)
            {
                CountEssentialOptions();
            }

            return (UInt32)diameter;
        }

        public Double CalculateAverageDegree()
        {
            throw new NotImplementedException();
        }

        public Double CalculateAverageClusteringCoefficient()
        {
            throw new NotImplementedException();
        }

        // Возвращается число циклов длиной 3 в графе. Реализовано.
        public BigInteger CalculateCycles3()
        {
            //log.Info("Getting count of cycles - order 3.");

            if (-1 == avgPath)
            {
                CountEssentialOptions();
            }

            double cycles3 = 0;
            for (int i = 0; i < container.Size; ++i)
            {
                if (edgesBetweenNeighbours[i] != -1)
                    cycles3 += (int)edgesBetweenNeighbours[i];
            }

            if (cycles3 > 0 && cycles3 < 3)
                cycles3 = 1;
            else
                cycles3 /= 3;

            return (BigInteger)cycles3;
        }

        // Возвращается число циклов длиной 4 в графе. Реализовано.
        public BigInteger CalculateCycles4()
        {
            //log.Info("Getting count of cycles - order 4.");

            long count = 0;
            for (int i = 0; i < container.Size; i++)
                count += Get4OrderCyclesOfNode(i);

            return (BigInteger)count / 4;
        }

        // Возвращается массив собственных значений матрицы смежности. Реализовано.
        public List<double> CalculateEigenValues()
        {
            //log.Info("Getting eigen values array.");
            bool[,] m = container.GetMatrix();

            EigenValueUtils eg = new EigenValueUtils();
            
            try
            {
                return eg.CalculateEigenValue(m);
            }
            catch (Exception)
            {
                //log.Error(ex);
                return new List<double>();
            }
        }

        public BigInteger CalculateCycles3Eigen()
        {
            throw new NotImplementedException();
        }

        public BigInteger CalculateCycles4Eigen()
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<Double, Int32> CalculateEigenDistanceDistribution()
        {
            //log.Info("Getting distances between eigen values.");

            bool[,] m = container.GetMatrix();

            EigenValueUtils eg = new EigenValueUtils();

            try
            {
                eg.CalculateEigenValue(m);
                return eg.CalcEigenValuesDist();

            }
            catch (Exception)
            {
                //log.Error(ex);
                return new SortedDictionary<double, int>();
            }
        }

        public SortedDictionary<UInt32, UInt32> CalculateDegreeDistribution()
        {
            return DegreeDistribution();
        }

        // Возвращается распределение коэффициентов кластеризации графа. Реализовано.
        public SortedDictionary<Double, UInt32> GetClusteringCoefficientDistribution()
        {
            //log.Info("Getting clustering coefficients.");

            if (-1 == avgPath)
            {
                CountEssentialOptions();
            }

            double clusteringCoefficient = 0;
            int iEdgeCountForFullness = 0, iNeighbourCount = 0;
            double iclusteringCoefficient = 0;
            SortedDictionary<Double, UInt32> m_iclusteringCoefficient =
                new SortedDictionary<Double, UInt32>();

            SortedDictionary<int, double> iclusteringCoefficientList = new SortedDictionary<int, double>();
            for (int i = 0; i < container.Size; ++i)
            {
                iNeighbourCount = container.CountVertexDegree(i);
                if (iNeighbourCount != 0)
                {
                    iEdgeCountForFullness = (iNeighbourCount == 1) ? 1 : iNeighbourCount * (iNeighbourCount - 1) / 2;
                    iclusteringCoefficient = (edgesBetweenNeighbours[i]) / iEdgeCountForFullness;
                    iclusteringCoefficientList[i] = Math.Round(iclusteringCoefficient, 4);
                    clusteringCoefficient += iclusteringCoefficient;
                }
                else
                    iclusteringCoefficientList[i] = 0;
            }

            clusteringCoefficient /= container.Size;

            for (int i = 0; i < container.Size; ++i)
            {
                double result = iclusteringCoefficientList[i];
                if (m_iclusteringCoefficient.Keys.Contains(result))
                    m_iclusteringCoefficient[iclusteringCoefficientList[i]]++;
                else
                    m_iclusteringCoefficient[iclusteringCoefficientList[i]] = 1;

                
            }
            return m_iclusteringCoefficient;
        }

        // Возвращается распределение чисел  связанных подграфов в графе.
        public SortedDictionary<UInt32, UInt32> CalculateConnectedComponentDistribution()
        {
            var connectedSubGraphDic = new SortedDictionary<UInt32, UInt32>();
            Queue<int> q = new Queue<int>();
            var nodes = new Node[container.Size];
            for (int i = 0; i < nodes.Length; i++)
                nodes[i] = new Node();
            var list = new List<int>();

            for (int i = 0; i < container.Size; i++)
            {
                UInt32 order = 0;
                q.Enqueue(i);
                while (q.Count != 0)
                {
                    var item = q.Dequeue();
                    if (nodes[item].lenght != 2)
                    {
                        if (nodes[item].lenght == -1)
                        {
                            order++;
                        }
                        list = container.Neighbourship[item];
                        nodes[item].lenght = 2;

                        for (int j = 0; j < list.Count; j++)
                        {
                            if (nodes[list[j]].lenght == -1)
                            {
                                nodes[list[j]].lenght = 1;
                                order++;
                                q.Enqueue(list[j]);
                            }

                        }
                    }
                }

                if (order != 0)
                {
                    if (connectedSubGraphDic.ContainsKey(order))
                        connectedSubGraphDic[order]++;
                    else
                        connectedSubGraphDic.Add(order, 1);
                }
            }
            return connectedSubGraphDic;
        }

        public SortedDictionary<UInt32, UInt32> CalculateCompleteComponentDistribution()
        {
            throw new NotImplementedException();
        }

        // Возвращается распределение длин минимальных путей в графе. Реализовано.
        public SortedDictionary<UInt32, UInt32> CalculateDistanceDistribution()
        {
            //log.Info("Getting minimal distances between vertices.");

            if (-1 == avgPath)
            {
                CountEssentialOptions();
            }

            return pathDistribution;
        }

        // Возвращает распределение триугольников, прикрепленных к вершине.
        public SortedDictionary<UInt32, UInt32> CalculateTriangleByVertexDistribution()
        {
            //log.Info("Getting triangles distribution.");

            if (-1 == avgPath)
            {
                CountEssentialOptions();
            }

            var trianglesDistribution = new SortedDictionary<UInt32, UInt32>();
            for (int i = 0; i < container.Size; ++i)
            {
                var countTringle = (UInt32)edgesBetweenNeighbours[i];
                if (trianglesDistribution.ContainsKey(countTringle))
                {
                    trianglesDistribution[countTringle]++;
                }
                else
                {
                    trianglesDistribution.Add(countTringle, 1);
                }
            }

            return trianglesDistribution;
        }

        // Возвращается распределение чисел циклов. Реализовано.
        public SortedDictionary<Int32, BigInteger> CalculateCycleDistribution(Int16 lowBound, Int16 hightBound)
        {
            //log.Info("Getting cycles.");
            CyclesCounter cyclesCounter = new CyclesCounter(container);
            SortedDictionary<int, BigInteger> cyclesCount = new SortedDictionary<int, BigInteger>();
            long count = 0;
            for (int i = lowBound; i <= hightBound; i++)
            {
                count = cyclesCounter.getCyclesCount(i);
                cyclesCount.Add(i, (BigInteger)count);
            }

            return cyclesCount;
        }

        public SortedDictionary<UInt16, BigInteger> CalculateCycleDistribution(UInt16 lowBound, UInt16 highBound)
        {
            throw new NotImplementedException();
        }

        // Закрытая часть класса (не из общего интерфейса). //

        private List<double> edgesBetweenNeighbours;

        private double avgPath = -1;
        private int diameter = -1;
        private SortedDictionary<UInt32, UInt32> pathDistribution = new SortedDictionary<UInt32, UInt32>();
        private List<SortedList<int, int>> cycles4 = new List<SortedList<int, int>>();

        private void Initialization()
        {
            edgesBetweenNeighbours = new List<double>();
            for (int i = 0; i < container.Size; ++i)
                edgesBetweenNeighbours.Add(-1);
        }

        // Внутренный тип для работы BFS алгоритма.
        private class Node
        {
            public int ancestor = -1;
            public int lenght = -1;
            public int m_4Cycles = 0;
            public Node() { }
        }

        // Реализация BFS алгоритма.
        private void BFS(int i, Node[] nodes)
        {
            nodes[i].lenght = 0;
            nodes[i].ancestor = 0;
            bool b = true;
            Queue<int> q = new Queue<int>();
            q.Enqueue(i);
            int u;
            if (edgesBetweenNeighbours[i] == -1)
                edgesBetweenNeighbours[i] = 0;
            else
                b = false;

            while (q.Count != 0)
            {
                u = q.Dequeue();
                List<int> l = container.Neighbourship[u];
                for (int j = 0; j < l.Count; ++j)
                    if (nodes[l[j]].lenght == -1)
                    {
                        nodes[l[j]].lenght = nodes[u].lenght + 1;
                        nodes[l[j]].ancestor = u;
                        q.Enqueue(l[j]);
                    }
                    else
                    {
                        if (nodes[u].lenght == 1 && nodes[l[j]].lenght == 1 && b)
                        {
                            ++edgesBetweenNeighbours[i];
                        }
                    }
            }
            if (b)
                edgesBetweenNeighbours[i] /= 2;
        }

        // Выполняет подсчет сразу 3 свойств - средняя длина пути, диаметр и пути между вершинами.
        // Нужно вызвать перед получением этих свойств не изнутри.
        private void CountEssentialOptions()
        {
            if (edgesBetweenNeighbours.Count == 0)
            {
                for (int i = 0; i < container.Size; ++i)
                    edgesBetweenNeighbours.Add(-1);
            }
            double avg = 0;
            int diametr = 0, k = 0;

            for (int i = 0; i < container.Size; ++i)
            {
                for (int j = i + 1; j < container.Size; ++j)
                {
                    int way = MinimumWay(i, j);
                    if (way == -1)
                        continue;
                    if (pathDistribution.ContainsKey((UInt32)way))
                        pathDistribution[(UInt32)way]++;
                    else
                        pathDistribution.Add((UInt32)way, 1);

                    if (way > diametr)
                        diametr = way;

                    avg += way;
                    ++k;
                }
            }
            Node[] nodes = new Node[container.Size];
            for (int t = 0; t < container.Size; ++t)
                nodes[t] = new Node();

            BFS(container.Size - 1, nodes);
            avg /= k;

            avgPath = avg;
            diameter = diametr;
        }

        // Возвращает число циклов 4, которые содержат данную вершину.
        private int CalculatCycles4(int i)
        {
            Node[] nodes = new Node[container.Size];
            for (int k = 0; k < container.Size; ++k)
                nodes[k] = new Node();
            int cyclesOfOrderi4 = 0;
            nodes[i].lenght = 0;
            nodes[i].ancestor = 0;
            Queue<int> q = new Queue<int>();
            q.Enqueue(i);
            int u;

            while (q.Count != 0)
            {
                u = q.Dequeue();
                List<int> l = container.Neighbourship[u];
                for (int j = 0; j < l.Count; ++j)
                    if (nodes[l[j]].lenght == -1)
                    {
                        nodes[l[j]].lenght = nodes[u].lenght + 1;
                        nodes[l[j]].ancestor = u;
                        q.Enqueue(l[j]);
                    }
                    else
                    {
                        if (nodes[u].lenght == 2 && nodes[l[j]].lenght == 1 && nodes[u].ancestor != l[j])
                        {
                            SortedList<int, int> cycles4I = new SortedList<int, int>();
                            cyclesOfOrderi4++;
                        }
                    }
            }

            return cyclesOfOrderi4;
        }

        // Возвращает распределение степеней.
        private SortedDictionary<UInt32, UInt32> DegreeDistribution()
        {
            SortedDictionary<UInt32, UInt32> degreeDistribution =
                new SortedDictionary<UInt32, UInt32>();
            for (UInt32 i = 0; i < container.Size; ++i)
                degreeDistribution[i] = new UInt32();
            for (int i = 0; i < container.Size; ++i)
            {
                int degreeOfVertexI = container.Neighbourship[i].Count;
                degreeDistribution[(UInt32)degreeOfVertexI]++;
            }
            for (UInt32 i = 0; i < container.Size; i++)
                if (degreeDistribution[i] == 0)
                    degreeDistribution.Remove(i);

            return degreeDistribution;
        }

        private int fullSubGgraph(int u)    // Разобраться, почему не реализована соответсвующая функция интерфейса.
        {
            List<int> n1;
            n1 = container.Neighbourship[u];
            List<int> n2 = new List<int>();
            int l = 0;
            bool t;
            int k = 0;
            while (l != n1.Count)
            {
                n2.Clear();
                n2.Add(u);
                if (l != 0)
                    n2.Add(n1[l]);
                for (int i = 0; i < n1.Count && i != l; i++)
                {
                    t = true;
                    for (int j = 0; j < n2.Count; j++)
                        if (container.AreNeighbours(n1[i], n2[j]) == false)
                        {
                            t = false;
                            break;

                        }
                    if (t == true)
                        n2.Add(n1[i]);
                }
                int p = n2.Count;
                if (p > k)
                    k = p;
                l++;
            }
            return k;
        }

        // Возвращает длину минимальной пути между данными вершинами.
        private int MinimumWay(int i, int j)
        {
            if (i == j)
                return 0;

            Node[] nodes = new Node[container.Size];
            for (int k = 0; k < container.Size; ++k)
                nodes[k] = new Node();

            BFS(i, nodes);

            return nodes[j].lenght;
        }

        // Возвращает степень максимального соединенного подграфа. Не используется.
        private int GetMaxFullSubgraph()
        {
            int k = 0;
            for (int i = 0; i < container.Size; i++)
                if (this.fullSubGgraph(i) > k)
                    k = this.fullSubGgraph(i);

            return k;
        }

        private long Get4OrderCyclesOfNode(int j)
        {
            List<int> neigboursList = container.Neighbourship[j];
            List<int> neigboursList1 = new List<int>();
            List<int> neigboursList2 = new List<int>();
            long count = 0;
            for (int i = 0; i < neigboursList.Count; i++)
            {
                neigboursList1 = container.Neighbourship[neigboursList[i]];
                for (int t = 0; t < neigboursList1.Count; t++)
                {
                    if (j != neigboursList1[t])
                    {
                        neigboursList2 = container.Neighbourship[neigboursList1[t]];
                        for (int k = 0; k < neigboursList2.Count; k++)
                            if (container.AreNeighbours(neigboursList2[k], j) && neigboursList2[k] != neigboursList1[t] && neigboursList2[k] != neigboursList[i])
                                count++;
                    }
                }


            }

            return count / 2;

        }
    }
}
