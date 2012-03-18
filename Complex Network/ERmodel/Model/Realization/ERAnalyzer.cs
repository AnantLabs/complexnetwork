using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RandomGraph.Common.Model;
using Model.ERModel.Result;
using Model.ERModel.Realization;

namespace model.ERModel.Realization
{
    class ERAnalyzer
    {
        private ERContainer m_container;
        private AnalyzeResult m_result;
        private int[] m_minimal_path_list;
        private class Node
        {
            public int n_length;
            public bool n_visited;

            public Node()
            {
                n_length = -1;
                n_visited = false;
            }
        }
        
        public ERAnalyzer(ERContainer c)
        {
            m_container = c;
            m_result = new AnalyzeResult();
            m_minimal_path_list = new int[m_container.Size];
        }

        public AnalyzeResult Analyze(AnalyseOptions options)
        {
            if (((options & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath) ||
                ((options & AnalyseOptions.Diameter) == AnalyseOptions.Diameter))
            {
                count_essential_options();
            }
            if ((options & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient)
            {
                count_graph_clustering_coefficiant();
            }
            if ((options & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution)
            {
                count_degree_destribution();
            }
            if ((options & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3)
            {
                count_cycles_of_order3();
            }

            if ((options & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4)
            {
                count_cycles_of_order4();
            }
            //if ((options & AnalyseOptions.FullSubGraph) == AnalyseOptions.FullSubGraph)
            //{
            //    count_max_full_graphs();
            //}

            return m_result;
        }

        public void count_essential_options()
        {
            int size = m_container.Size;
            int d = 0;
            int count = 0, sum = 0;
            m_result.m_pathDistribution = new SortedDictionary<int, int>();

            for (int i = 0; i < size; ++i)
            {
                bfs(i);
                d = Math.Max(d, m_minimal_path_list.Max());

                for (int j = 0; j < size; ++j)
                {
                    int n = m_minimal_path_list[j];
                    if (n > 0) {
                        sum += n;
                        if (m_result.m_pathDistribution.ContainsKey(n))
                        {
                            m_result.m_pathDistribution[n]++;
                        }
                        else
                        {
                            m_result.m_pathDistribution.Add(n, 1);
                        }
                        count++;
                    }
                }
            }

            m_result.m_diameter = d;
            m_result.m_avgPathLenght = Math.Round((double)sum / count, 4);
        }

        public void bfs(int s)
        {
            Queue<int> queue = new Queue<int>();
            Node[] nodes = new Node[m_container.Size];
            for (int i = 0; i < m_container.Size; ++i)
            {
                m_minimal_path_list[i] = -1;
                nodes[i] = new Node();
            }

            nodes[s].n_length = 0;
            nodes[s].n_visited = true;

            m_minimal_path_list[s] = 0;
          
            queue.Enqueue(s);
            while (queue.Count != 0)
            {
                int t = queue.Dequeue();
                List<int> tmp = m_container.Neighbourship[t];
                for (int i = 0; i < tmp.Count; ++i)
                {
                    int e = tmp[i];
                    if (nodes[e].n_visited == false)
                    {
                        nodes[e].n_visited = true;
                        nodes[e].n_length = nodes[t].n_length + 1;
                        queue.Enqueue(e);
                        m_minimal_path_list[e] = nodes[e].n_length;
                    }
                }
            }
        }

        public void count_degree_destribution()
        {
            int avg = 0;
            m_result.m_degreeDistribution = new SortedDictionary<int, int>();

            for (int i = 0; i < m_container.Size; ++i)
            {
                int n = m_container.Neighbourship[i].Count;
                avg += n;
                if (m_result.m_degreeDistribution.ContainsKey(n))
                {
                    m_result.m_degreeDistribution[n]++;
                }
                else
                {
                    m_result.m_degreeDistribution.Add(n, 1);
                }
            }
            m_result.m_avgDegree = (double) avg / m_container.Size;
        }

        public void count_cycles_of_order3()
        {
            int count = 0;
            for (int i = 0; i < m_container.Size; ++i)
            {
                List<int> nbs = m_container.Neighbourship[i];
                for (int j = 0; j < nbs.Count; ++j)
                {
                    List<int> tmp = m_container.Neighbourship[nbs[j]];
                    count += nbs.Intersect(tmp).Count();
                }
            }

            m_result.m_cyclesOfOrder3 = count / 6;
        }

        public void count_cycles_of_order4()
        {
            int count = 0;
            for (int i = 0; i < m_container.Size; ++i)
            {
                List<int> nbs = m_container.Neighbourship[i];

                for (int j = 0; j < nbs.Count; ++j)
                {
                    List<int> lj = m_container.Neighbourship[nbs[j]];

                    for (int k = 0; k < nbs.Count; ++k)
                    {
                        if (k != j)
                        {
                            List<int> lk = m_container.Neighbourship[nbs[k]];
                            count += (lj.Intersect(lk).Count() - 1);
                        }
                    }
                }
            }

            m_result.m_cyclesOfOrder4 = count / 8;
        }

        public void count_graph_clustering_coefficiant()
        {
            double r = 0.0;
            double count = 0.0;
            int size = m_container.Size;
            m_result.m_vertexClusteringCoefficient = new SortedDictionary<double, int>();

            for (int i = 0; i < size; ++i)
            {
                r = Math.Round(get_vertex_clustering_coefficient(i), 4);
                if (m_result.m_vertexClusteringCoefficient.ContainsKey(r))
                {
                    m_result.m_vertexClusteringCoefficient[r]++;
                }
                else
                {
                    m_result.m_vertexClusteringCoefficient.Add(r, 1);
                }
                count += r;
            }

            m_result.m_clusteringCoefficient = Math.Round(count / size, 4);
        }

        public double get_vertex_clustering_coefficient(int i)
        {
            int count = 0;
            List<int> neighbors = m_container.Neighbourship[i];
            int neighbor_count = neighbors.Count;
            if (neighbor_count < 2)
            {
                return 0;
            }

            for (int j = 0; j < neighbor_count; ++j)
            {
                List<int> tmp = m_container.Neighbourship[neighbors[j]];
                for (int k = 0; k < neighbor_count; ++k)
                {
                    if (tmp.Contains(neighbors[k]))
                    {
                        count++;
                    }
                }
            }

            return (double)(2 * (count / 2 + neighbor_count)) / (neighbor_count * (neighbor_count + 1));
        }

    }
}
