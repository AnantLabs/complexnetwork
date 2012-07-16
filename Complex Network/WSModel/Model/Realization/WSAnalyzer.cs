using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandomGraph.Common.Model;
using Model.WSModel.Realization;
using CommonLibrary.Model;
using Algorithms;

namespace Model.WSModel.Realization
{
    public class WSAnalyzer : AbstarctGraphAnalyzer
    {
        // !Организовать лофгирование!
        
        IGraphContainer container;  // пересмотреть!

        public WSAnalyzer(WSContainer container)
        {
            m_container = container;
            CountNeighbourships();
        }

        // Контейнер, в котором содержится сгенерированный граф (полученный от генератора).
        public override IGraphContainer Container
        {
            get { return container; }
            set { container = value; }
        }

        // Возвращается средняя длина пути в графе. Реализовано.
        public override double GetAveragePath()
        {
            CountAvgPathAndDiametr();
            return m_avgPathLenght;
        }

        // Возвращается диаметр графа. Реализовано.
        public override int GetDiameter()
        {
            return m_diametr;
        }

        // Возвращается число циклов длиной 3 в графе. Реализовано.
        public override int GetCycles3()
        {
            return m_cyclesOfOrder3;
        }

        // Возвращается число циклов длиной 4 в графе. Реализовано.
        public override int GetCycles4()
        {
            return m_cyclesOfOrder4;
        }

        // Возвращается массив собственных значений матрицы смежности. Реализовано.
        public override ArrayList GetEigenValues()
        {
            Algorithms.EigenValue ev = new EigenValue();
            bool[,] m = m_container.GetMatrix();
            return ev.EV(m);
        }

        // Возвращается степенное распределение графа. Реализовано.
        public override SortedDictionary<int, int> GetDegreeDistribution()
        {
            return m_degreeDistribution;
        }

        // Возвращается распределение коэффициентов кластеризации графа. Реализовано.
        public override SortedDictionary<double, int> GetClusteringCoefficient()
        {
            return m_coefficient;
        }

        // Возвращается распределение чисел связанных подграфов в графе. Реализовано.
        public override SortedDictionary<int, int> GetConnSubGraph()
        {
            return m_fullSubgraphs;
        }

        // Возвращается распределение длин минимальных путей в графе. Реализовано.
        public override SortedDictionary<int, int> GetMinPathDist()
        {
            return m_vertexDistances;
        }


        // Закрытая часть класса (не из общего интерфейса). //

        // Type definitions //
        private class Node
        {
            public int m_ancestor;
            public int m_lenght;

            public Node()
            {
                m_ancestor = -1;
                m_lenght = -1;
            }
        }

        // Implementation members //
        private WSContainer m_container;
        private List<int> m_neighbourship;

        private double m_avgPathLenght = 0;
        private int m_diametr = 0;
        private int m_cyclesOfOrder3 = 0;
        private int m_cyclesOfOrder4 = 0;
        private SortedDictionary<int, int> m_degreeDistribution = new SortedDictionary<int,int>();
        private SortedDictionary<double, int> m_coefficient = new SortedDictionary<double,int>();
        private SortedDictionary<int, int> m_fullSubgraphs = new SortedDictionary<int,int>();
        private SortedDictionary<int, int> m_vertexDistances = new SortedDictionary<int,int>();
        private double m_clusteringCoefficient = 0;
        
        private void CountNeighbourships()
        {
            int size = m_container.Size;
            m_neighbourship = new List<int>(size);

            for (int i = 0; i < size; ++i)
            {
                m_neighbourship.Insert(i, 0);
                for (int j = 0; j < size; j++)
                    if (m_container.AreNeighbours(i, j))
                        m_neighbourship[i]++;
            }
        }

        private void CountAvgPathAndDiametr()
        {
            m_vertexDistances = new SortedDictionary<int, int>();
            double avg = 0;
            int diametr = 0;
            int k = 0;
            int size = m_container.Size;
            for (int i = 0; i < size; ++i)
            {
                List<Node> nodes = new List<Node>();
                for (int p = 0; p < size; p++)
                    nodes.Insert(p, new Node());
                BFS(i, nodes);

                for (int j = i; j < size; ++j)
                {
                    Node nd = nodes[j];
                    int way = nd.m_lenght; ;
                    if (way == -1)
                        continue;
                    if (way > diametr)
                        diametr = way;

                    if (way > 0)
                    {
                        avg += way;
                        ++k;

                        if (m_vertexDistances.ContainsKey(way))
                            m_vertexDistances[way]++;
                        else
                            m_vertexDistances.Add(way, 1);
                    }
                }
            }

            avg /= k;
            double avgD = avg * 10000;
            int avgI = Convert.ToInt32(avgD);
            m_avgPathLenght = (double)avgI / 10000;
            m_diametr = diametr;
            m_cyclesOfOrder4 /= 4;
        }

        private void BFS(int i, List<Node> nodes)
        {
            nodes[i].m_lenght = 0;
            nodes[i].m_ancestor = 0;
            Queue<int> q = new Queue<int>();
            q.Enqueue(i);
            int u;
            while (q.Count != 0)
            {
                u = q.Dequeue();
                List<int> l = new List<int>();
                m_container.Neighbours(u, l);
                for (int j = 0; j < l.Count; ++j)
                {
                    if (nodes[l[j]].m_lenght == -1)
                    {
                        nodes[l[j]].m_lenght = nodes[u].m_lenght + 1;
                        nodes[l[j]].m_ancestor = u;
                        q.Enqueue(l[j]);
                    }
                    else
                    {
                        if (nodes[u].m_lenght == 2 && nodes[l[j]].m_lenght == 1 && nodes[u].m_ancestor != l[j])
                            ++m_cyclesOfOrder4;
                    }
                }

            }
        }

        private void DegreeDistribution()
        {
            int size = m_container.Size;
            m_degreeDistribution = new SortedDictionary<int, int>();
            int degreeCount = 0;

            for (int i = 0; i < size; ++i)
            {
                degreeCount = ReturnNeighboursCount(i);
                if (degreeCount != 0)
                {
                    if (m_degreeDistribution.ContainsKey(degreeCount))
                        m_degreeDistribution[degreeCount]++;
                    else
                        m_degreeDistribution.Add(degreeCount, 1);
                }
            }
        }

        private int ReturnNeighboursCount(int i)
        {
            return m_container.CountDegree(i);
        }

        private void ClusteringCoefficient()
        {
            // no multythreading in current realization
            int size = m_container.Size;
            m_fullSubgraphs = new SortedDictionary<int, int>();
            m_coefficient = new SortedDictionary<double, int>();

            for (int i = 0; i < size; ++i)
                ClusteringCoeffForVertex(i);

            m_clusteringCoefficient /= size;
        }

        private void ClusteringCoeffForVertex(int index)
        {
            int i = index;
            int neighbours = m_neighbourship[i];
            if (!(neighbours > 0))
                return;

            int E = 0;
            int K = (neighbours == 1) ? 1 : neighbours * (neighbours - 1) / 2;

            List<int> nVec = new List<int>();
            m_container.Neighbours(i, nVec);

            int size = nVec.Count;
            for (int k = 0; k < size; ++k)
            {
                int counter = 0;
                for (int j = k + 1; j < size; ++j)
                    if (m_container.AreNeighbours(nVec[k], nVec[j]))
                        ++counter;

                E += counter;
            }

            m_cyclesOfOrder3 += E;

            //System::Threading::Monitor::Enter(this);
            //{
            double clusteringCoefI = (double)E / K;

            if (m_coefficient.ContainsKey(clusteringCoefI))
                m_coefficient[clusteringCoefI]++;
            else
                m_coefficient.Add(clusteringCoefI, 1);

            m_clusteringCoefficient += clusteringCoefI;	// sincronization
            if (E / K == 1)
            {
                if (m_fullSubgraphs.ContainsKey(neighbours + 1))
                    m_fullSubgraphs[neighbours + 1]++;
                else
                    m_fullSubgraphs.Add(neighbours + 1, 1);
            }
            //}
            //System::Threading::Monitor::Exit(this);
        }

        private int CyclesOfOrder3()
        {
            m_cyclesOfOrder3 /= 3;
            return m_cyclesOfOrder3;
        }

        private int MaxFullSubGraph()
        {
            int size = m_fullSubgraphs.Count;
            int max = 0;
            for (int i = 0; i < size; ++i)
            {
                int element = m_fullSubgraphs.ElementAt(i).Key;
                if (max < element)
                    max = element;
            }

            return max;
        } 
    }
}
