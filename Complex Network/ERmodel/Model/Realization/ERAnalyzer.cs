using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Model.ERModel.Realization;
using CommonLibrary.Model;
using log4net;
using Algorithms;

namespace Model.ERModel.Realization
{
    public class ERAnalyzer : AbstarctGraphAnalyzer
    {
        // Организация Работы с лог файлом. !Использовать ключевое слово new!
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(ERAnalyzer));

        IGraphContainer container;  // пересмотреть!

        public ERAnalyzer(ERContainer c)
        {
            log.Info("Creating ERAnalizer object");
            m_container = c;
            m_minimal_path_list = new int[m_container.Size];

            m_avgPathLenght = -1;
            m_avgDegree = -1;
            m_diameter = -1;
            m_clusteringCoefficient = -1;
            m_cyclesOfOrder3 = -1;
            m_cyclesOfOrder4 = -1;
            m_maxfullsubgraph = -1;
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
            log.Info("GetAveragePath");

            if (-1 == m_avgPathLenght)
            {
                CountEssentialOptions();
            }
            return m_avgPathLenght;
        }

        // Возвращается диаметр графа. Реализовано.
        public override int GetDiameter()
        {
            log.Info("GetDiameter");
            if (-1 == m_diameter)
            {
                CountEssentialOptions();
            }
            return m_diameter;
        }

        // Возвращается число циклов длиной 3 в графе. Реализовано.
        public override int GetCycles3()
        {
            log.Info("GetCycles3");
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
            m_cyclesOfOrder3 = count / 6;

            return m_cyclesOfOrder3;
        }

        // Возвращается число циклов длиной 4 в графе. Реализовано.
        public override int GetCycles4()
        {
            log.Info("GetCycles4");
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
            m_cyclesOfOrder4 = count / 8;

            return m_cyclesOfOrder4;
        }

        // Возвращается массив собственных значений матрицы смежности. Реализовано.
        public override ArrayList GetEigenValues()
        {
            Algorithms.EigenValue ev = new EigenValue();
            bool[,] m = m_container.GetMatrix();
            return ev.EV(m);
        }

        // Возвращается распределение длин между собственными значениями. Реализовано.
        public override SortedDictionary<double, int> GetDistEigenPath()
        {
            Algorithms.EigenValue ev = new EigenValue();
            bool[,] m = m_container.GetMatrix();
            ev.EV(m);
            return ev.CalcEigenValuesDist();
        }
        
        // Возвращается степенное распределение графа. Реализовано.
        public override SortedDictionary<int, int> GetDegreeDistribution()
        {
            log.Info("GetDegreeDistribution");

            if (null == m_degreeDistribution)
            {
                CountDegreeDestribution();
            }
            return m_degreeDistribution;
        }

        // Возвращается распределение коэффициентов кластеризации графа. Реализовано.
        public override SortedDictionary<double, int> GetClusteringCoefficient()
        {
            log.Info("GetClusteringCoefficient");

            if (null == m_vertexClusteringCoefficient)
            {
                CountGraphClusteringCoefficient();
            }
            return m_vertexClusteringCoefficient;
        }

        // Возвращается распределение длин минимальных путей в графе. Реализовано.
        public override SortedDictionary<int, int> GetMinPathDist()
        {
            log.Info("GetMinPathDist");

            if (null == m_pathDistribution)
            {
                CountEssentialOptions();
            }
            return m_pathDistribution;
        }


        // Закрытая часть класса (не из общего интерфейса). //

        private ERContainer m_container;
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

        private int[] m_minimal_path_list;

        //members for storage of options value
        private double m_avgPathLenght;
        private double m_avgDegree;
        private int m_diameter;
        private double m_clusteringCoefficient;
        private SortedDictionary<double, int> m_vertexClusteringCoefficient;
        private SortedDictionary<int, int> m_degreeDistribution;//count the number of vertex that have i degrees
        private SortedDictionary<int, int> m_pathDistribution;
        private int m_cyclesOfOrder3;
        private int m_cyclesOfOrder4;
        private int m_maxfullsubgraph;
        private ArrayList ArrayOfEigVal;

        private void bfs(int s)
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

        private double GetAverageDegree()
        {
            log.Info("GetAverageDegree");

            if (-1 == m_avgDegree)
            {
                CountDegreeDestribution();
            }
            return m_avgDegree;
        }

        private void CountEssentialOptions()
        {
            log.Info("CountEssentialOptions");
            int size = m_container.Size;
            int d = 0;
            int count = 0, sum = 0;
            m_pathDistribution = new SortedDictionary<int, int>();

            for (int i = 0; i < size; ++i)
            {
                bfs(i);
                d = Math.Max(d, m_minimal_path_list.Max());

                for (int j = 0; j < size; ++j)
                {
                    int n = m_minimal_path_list[j];
                    if (n > 0) {
                        sum += n;
                        if (m_pathDistribution.ContainsKey(n))
                        {
                            m_pathDistribution[n]++;
                        }
                        else
                        {
                            m_pathDistribution.Add(n, 1);
                        }
                        count++;
                    }
                }
            }

            for (int i = 0; i < size; ++i)
            {
                if (m_pathDistribution.ContainsKey(i))
                {
                    m_pathDistribution[i] /= 2;
                }
            }

            m_diameter = d;
            m_avgPathLenght = Math.Round((double)sum / count, 4);
        }

        private void CountDegreeDestribution()
        {
            log.Info("CountDegreeDestribution");
            int avg = 0;
            m_degreeDistribution = new SortedDictionary<int, int>();

            for (int i = 0; i < m_container.Size; ++i)
            {
                int n = m_container.Neighbourship[i].Count;
                avg += n;
                if (m_degreeDistribution.ContainsKey(n))
                {
                    m_degreeDistribution[n]++;
                }
                else
                {
                    m_degreeDistribution.Add(n, 1);
                }
            }
            m_avgDegree = (double) avg / m_container.Size;
        }

        private double GetAvgClusteringCoefficient()
        {
            log.Info("GetAvgClusteringCoefficient");

            if (-1 == m_clusteringCoefficient)
            {
                CountGraphClusteringCoefficient();
            }
            return m_clusteringCoefficient;
        }

        private void CountGraphClusteringCoefficient()
        {
            log.Info("CountGraphClusteringCoefficient");
            double r = 0.0;
            double count = 0.0;
            int size = m_container.Size;
            m_vertexClusteringCoefficient = new SortedDictionary<double, int>();

            for (int i = 0; i < size; ++i)
            {
                r = Math.Round(GetVertexClusteringCoefficient(i), 4);
                if (m_vertexClusteringCoefficient.ContainsKey(r))
                {
                    m_vertexClusteringCoefficient[r]++;
                }
                else
                {
                    m_vertexClusteringCoefficient.Add(r, 1);
                }
                count += r;
            }

            m_clusteringCoefficient = Math.Round(count / size, 4);
        }

        private double GetVertexClusteringCoefficient(int i)
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
