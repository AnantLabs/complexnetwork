using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandomGraph.Common.Model;
using Model.WSModel.Realization;
using Model.WSModel.Result;
using CommonLibrary.Model;

namespace Model.WSModel.Realization
{
    public class WSAnalyzer : AbstarctGraphAnalyzer
    {
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
        private AnalyzeResult m_result;
        protected List<int> m_neighbourship;

        public AnalyzeResult Result
        {

            get { return m_result; }
        }

        public WSAnalyzer(WSContainer container)
        {
            m_container = container;
            m_result = new AnalyzeResult();
            m_result.m_cyclesOfOrder3 = 0;
            m_result.m_cyclesOfOrder4 = 0;
            CountNeighbourships();
        }
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

        public void CountAvgPathAndDiametr()
        {
            m_result.m_vertexDistances = new SortedDictionary<int, int>();
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

                        if (m_result.m_vertexDistances.ContainsKey(way))
                            m_result.m_vertexDistances[way]++;
                        else
                            m_result.m_vertexDistances.Add(way, 1);
                    }
                }
            }

            avg /= k;
            double avgD = avg * 10000;
            int avgI = Convert.ToInt32(avgD);
            m_result.m_avgPathLenght = (double)avgI / 10000;
            m_result.m_diametr = diametr;
            m_result.m_cyclesOfOrder4 /= 4;
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
                            ++m_result.m_cyclesOfOrder4;
                    }
                }

            }
        }

        public void DegreeDistribution()
        {
            int size = m_container.Size;
            m_result.m_degreeDistribution = new SortedDictionary<int, int>();
            int degreeCount = 0;

            for (int i = 0; i < size; ++i)
            {
                degreeCount = ReturnNeighboursCount(i);
                if (degreeCount != 0)
                {
                    if (m_result.m_degreeDistribution.ContainsKey(degreeCount))
                        m_result.m_degreeDistribution[degreeCount]++;
                    else
                        m_result.m_degreeDistribution.Add(degreeCount, 1);
                }
            }
        }

        int ReturnNeighboursCount(int i)
        {
            return m_container.CountDegree(i);
        }


        public void ClusteringCoefficient()
        {
            // no multythreading in current realization
            int size = m_container.Size;
            m_result.m_fullSubgraphs = new SortedDictionary<int, int>();
            m_result.m_coefficient = new SortedDictionary<double, int>();

            for (int i = 0; i < size; ++i)
                ClusteringCoeffForVertex(i);

            m_result.m_clusteringCoefficient /= size;
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

            m_result.m_cyclesOfOrder3 += E;

            //System::Threading::Monitor::Enter(this);
            //{
            double clusteringCoefI = (double)E / K;

            if (m_result.m_coefficient.ContainsKey(clusteringCoefI))
                m_result.m_coefficient[clusteringCoefI]++;
            else
                m_result.m_coefficient.Add(clusteringCoefI, 1);

            m_result.m_clusteringCoefficient += clusteringCoefI;	// sincronization
            if (E / K == 1)
            {
                if (m_result.m_fullSubgraphs.ContainsKey(neighbours + 1))
                    m_result.m_fullSubgraphs[neighbours + 1]++;
                else
                    m_result.m_fullSubgraphs.Add(neighbours + 1, 1);
            }
            //}
            //System::Threading::Monitor::Exit(this);
        }

        public int CyclesOfOrder3()
        {
            m_result.m_cyclesOfOrder3 /= 3;
            return m_result.m_cyclesOfOrder3;
        }

        public int MaxFullSubGraph()
        {
            int size = m_result.m_fullSubgraphs.Count;
            int max = 0;
            for (int i = 0; i < size; ++i)
            {
                int element = m_result.m_fullSubgraphs.ElementAt(i).Key;
                if (max < element)
                    max = element;
            }

            return max;
        }

        public override SortedDictionary<int, int> GetDegreeDistribution()
        {
            return m_result.m_degreeDistribution;
        }
        //Calculate average parth of graph.
        public override double GetAveragePath()
        {
            CountAvgPathAndDiametr();
            return m_result.m_avgPathLenght;
        }
        //Calculate clustering coefficient of graph.
        public override SortedDictionary<double, int> GetClusteringCoefficient()
        {
            return m_result.m_coefficient;
        }
        //Calculate Eigen values of graph.
        public override ArrayList GetEigenValue()
        {
            return new ArrayList();
        }
        //Calculate count of cycles in 3 lenght of graph.
        public override int GetCycles3()
        {
            return m_result.m_cyclesOfOrder3;
        }

        //Calculate diameter of graph.
        public override int GetDiameter()
        {
            return m_result.m_diametr;
        }

        //Calculate distribution of connected subgraph of graph.
        public override SortedDictionary<int, int> GetConnSubGraph()
        {
            return m_result.m_fullSubgraphs;
        }
        //Calculate count of cycles in 3 lenght based in eigen valu of graph.
        public override int GetCycleEigen3()
        {
            return 0;
        }

        //Calculate count of cycles in 4 lenght of graph.
        public override int GetCycles4()
        {
            return m_result.m_cyclesOfOrder4;
        }

        //Calculate count of cycles in 4 lenght based in eigen valu of graph.
        public override int GetCycleEigen4()
        {
            return 0;
        }

        //Calculate distribution of minimum paths of graph.
        public override SortedDictionary<int, int> GetMinPathDist()
        {
            return m_result.m_vertexDistances;
        }
        //Calculate distribution of eigen value of graph.
        public override SortedDictionary<double, int> GetDistEigenPath()
        {
            return new SortedDictionary<double, int>();
        }
        //Calculate distribution of connected subgraph of graph.
        public override SortedDictionary<int, int> GetFullSubGraph()
        {
            return new SortedDictionary<int, int>();
        }

    }

}
