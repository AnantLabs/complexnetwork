using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using CommonLibrary.Model;
using Algorithms;
using Motifs;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using log4net;

namespace Model.BAModel.Realization
{
    public class BAAnalyzer : AbstarctGraphAnalyzer
    {
        // Организация работы с лог файлом.
        protected static readonly new ILog log = log4net.LogManager.GetLogger(typeof(BAAnalyzer));

        // Контейнер, в котором содержится граф конкретной модели (BA).
        private BAContainer container;

        // Конструктор, получающий контейнер графа.
        public BAAnalyzer(BAContainer c)
        {
            log.Info("Creating ERAnalizer object.");
            container = c;
            Initialization();
        }

        // Контейнер, в котором содержится сгенерированный граф (полученный от генератора).
        public override IGraphContainer Container 
        {
            get { return container; }
            set { container = (BAContainer)value; } 
        }

        // Возвращается средняя длина пути в графе. Реализовано.
        public override double GetAveragePath()
        {
            log.Info("Getting average path length.");

            if (1 != startCountAnalyzeOptions)
                CountAnalyzeOptions();
            return Math.Round(m_avgPath, 14);
        }

        // Возвращается диаметр графа. Реализовано.
        public override int GetDiameter()
        {
            log.Info("Getting diameter.");

            if (startCountAnalyzeOptions != 1)
                CountAnalyzeOptions();
            return m_diametr;
        }

        // Возвращается число циклов длиной 3 в графе. Реализовано.
        public override int GetCycles3()
        {
            log.Info("Getting count of cycles - order 3.");

            if (startCountAnalyzeOptions != 1)
                CountAnalyzeOptions();
            double m_cyclesOfOrder3 = 0;
            for (int i = 0; i < m_container.Size; ++i)
                if (m_edgesBetweenNeighbours[i] != -1)
                    m_cyclesOfOrder3 += (int)m_edgesBetweenNeighbours[i];
            if (m_cyclesOfOrder3 > 0 && m_cyclesOfOrder3 < 3)
                m_cyclesOfOrder3 = 1;
            else
                m_cyclesOfOrder3 /= 3;
            return (int)m_cyclesOfOrder3;
        }

        // Возвращается число циклов длиной 4 в графе. Реализовано.
        public override int GetCycles4()
        {
            log.Info("Getting count of cycles - order 4.");

            int count = 0;
            for (int i = 0; i < m_container.Size; i++)
                count += CalculatCycles4(i);
            int nmn = count;
            return count / 4;
        }

        // Возвращается массив собственных значений матрицы смежности. Реализовано.
        public override ArrayList GetEigenValues()
        {
            log.Info("Getting eigen values array.");

            Algorithms.EigenValue ev = new EigenValue();
            bool[,] m = m_container.GetMatrix();
            return ev.EV(m);
        }

        // Возвращается степенное распределение графа. Реализовано.
        public override SortedDictionary<int, int> GetDegreeDistribution()
        {
            log.Info("Getting degree distribution.");

            SortedDictionary<int, int> m_degreeDistribution = new SortedDictionary<int, int>();
            for (int i = 0; i < m_container.Size; ++i)
                m_degreeDistribution[i] = new int();
            for (int i = 0; i < m_container.Size; ++i)
            {
                int degreeOfVertexI = m_container.Neighbourship[i].Count;
                m_degreeDistribution[degreeOfVertexI]++;
            }
            for (int i = 0; i < m_container.Size; i++)
                if (m_degreeDistribution[i] == 0)
                    m_degreeDistribution.Remove(i);
            log.Info("End calculat DegreeDistribution");
            return m_degreeDistribution;
        }

        // Возвращается распределение коэффициентов кластеризации графа. Реализовано.
        public override SortedDictionary<double, int> GetClusteringCoefficient()
        {
            log.Info("Getting clustering coefficients.");

            if (startCountAnalyzeOptions != 1)
                CountAnalyzeOptions();
            int iEdgeCountForFullness = 0, iNeighbourCount = 0;
            double iclusteringCoefficient = 0;
            SortedDictionary<double, int> m_iclusteringCoefficient = new SortedDictionary<double, int>();

            SortedDictionary<int, double> iclusteringCoefficientList = new SortedDictionary<int, double>();
            for (int i = 0; i < m_container.Size; ++i)
            {
                iNeighbourCount = m_container.CountVertexDegree(i);
                if (iNeighbourCount != 0)
                {
                    iEdgeCountForFullness = (iNeighbourCount == 1) ? 1 : iNeighbourCount * (iNeighbourCount - 1) / 2;
                    iclusteringCoefficient = (m_edgesBetweenNeighbours[i]) / iEdgeCountForFullness;
                    iclusteringCoefficientList[i] = Math.Round(iclusteringCoefficient, 14);
                    m_clusteringCoefficient += iclusteringCoefficient;
                }
                else
                    iclusteringCoefficientList[i] = 0;
            }

            m_clusteringCoefficient /= m_container.Size;

            for (int i = 0; i < m_container.Size; ++i)
            {
                double result = iclusteringCoefficientList[i];
                if (m_iclusteringCoefficient.Keys.Contains(result))
                    m_iclusteringCoefficient[iclusteringCoefficientList[i]]++;
                else
                    m_iclusteringCoefficient[iclusteringCoefficientList[i]] = 1;
            }
            return m_iclusteringCoefficient;
        }

        // Возвращается распределение длин минимальных путей в графе. Реализовано.
        public override SortedDictionary<int, int> GetMinPathDist()
        {
            log.Info("Getting minimal distances between vertices.");

            if (startCountAnalyzeOptions != 1)
                CountAnalyzeOptions();
            return m_pathDistribution;
        }

        // Возвращается распределение чисел мотивов. Реализовано.
        public override SortedDictionary<int, float> GetMotivs(int lowBound, int hightBound)
        {
            log.Info("Getting motifs.");

            var motivfinder = new MotifFinder();
            var motifisCount = new SortedDictionary<int, float>();
            var motifisCountResult = new SortedDictionary<int, float>();
            Graph graph = Graph.reformatToOurGraghFromBAContainer(m_container);
            for (int motifDegree = lowBound; motifDegree <= hightBound; motifDegree++)
            {
                motivfinder.SearchMotifs(graph, motifDegree);
                motifisCount = motivfinder.dictionaryIdsValues();
                foreach (var key in motifisCount.Keys)
                    motifisCountResult.Add(key, motifisCount[key]);
            }
            return motifisCountResult;
        }


        // Закрытая часть класса (не из общего интерфейса). //

        private void Initialization()
        {
            m_edgesBetweenNeighbours = new List<double>();
            for (int i = 0; i < m_container.Size; ++i)
                m_edgesBetweenNeighbours.Add(-1);
            cyclesCounter = new CyclesCounter(m_container);
        }

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

        private List<SortedList<int,int>> cycles4 = new List<SortedList<int,int>>();

        // Implementation members //
        private BAContainer m_container;
        private CyclesCounter cyclesCounter;
        private double m_avgPath;
        private List<double> m_edgesBetweenNeighbours;
        private int m_diametr;
        private SortedDictionary<int, int> m_pathDistribution;
        private double m_clusteringCoefficient;
        private int startCountAnalyzeOptions;        

        private int MinimumWay(int i, int j)
        {
            
            if (i == j)
                return 0;

            Node[] nodes = new Node[m_container.Size];
            for (int k = 0; k < m_container.Size; ++k)
                nodes[k] = new Node();

            BFS(i, nodes);
         
            return nodes[j].m_lenght;
        }

        private void BFS(int i, Node[] nodes)
        {
            nodes[i].m_lenght = 0;
            nodes[i].m_ancestor = 0;
            bool b = true;
            Queue<int> q = new Queue<int>();
            q.Enqueue(i);
            int u;
            if (m_edgesBetweenNeighbours[i] == -1)
                m_edgesBetweenNeighbours[i] = 0;
            else
                b = false;
            
            while (q.Count != 0)
            {
                u = q.Dequeue();
                List<int> l = m_container.Neighbourship[u];
                for (int j = 0; j < l.Count; ++j)
                    if (nodes[l[j]].m_lenght == -1)
                    {
                        nodes[l[j]].m_lenght = nodes[u].m_lenght + 1;
                        nodes[l[j]].m_ancestor = u;
                        q.Enqueue(l[j]);
                    }
                    else
                    {
                        if (nodes[u].m_lenght == 1 && nodes[l[j]].m_lenght == 1 && b)
                        {

                            ++m_edgesBetweenNeighbours[i];
                        }   
                    }
            }
            if (b)
                m_edgesBetweenNeighbours[i] /= 2;
        }

        private void CountAnalyzeOptions()
        {
            log.Info("Start count Diametr");
            startCountAnalyzeOptions = 1;
            m_pathDistribution = new SortedDictionary<int, int>();
            double avg = 0;
            int diametr = 0, k = 0;

            for (int i = 0; i < m_container.Size; ++i)
            {
                for (int j = i + 1; j < m_container.Size; ++j)
                {
                    int way = MinimumWay(i, j);
                    if (way == -1)
                        continue;
                    if (m_pathDistribution.ContainsKey(way))
                        m_pathDistribution[way]++;
                    else
                        m_pathDistribution.Add(way, 1);

                    if (way > diametr)
                        diametr = way;

                    avg += way;
                    ++k;
                }
            }
            Node[] nodes = new Node[m_container.Size];
            for (int t = 0; t < m_container.Size; ++t)
                nodes[t] = new Node();

            BFS(m_container.Size - 1, nodes);
            avg /= k;

            m_avgPath = avg;
            m_diametr = diametr;
        }

        private int CalculatCycles4(int i)
        {
            Node[] nodes = new Node[m_container.Size];
            for (int k = 0; k < m_container.Size; ++k)
                nodes[k] = new Node();
            int cyclesOfOrderi4 = 0;
            nodes[i].m_lenght = 0;
            nodes[i].m_ancestor = 0;
            Queue<int> q = new Queue<int>();
            q.Enqueue(i);
            int u;

            while (q.Count != 0)
            {
                u = q.Dequeue();
                List<int> l = m_container.Neighbourship[u];
                for (int j = 0; j < l.Count; ++j)
                    if (nodes[l[j]].m_lenght == -1)
                    {
                        nodes[l[j]].m_lenght = nodes[u].m_lenght + 1;
                        nodes[l[j]].m_ancestor = u;
                        q.Enqueue(l[j]);
                    }
                    else
                    {
                        if (nodes[u].m_lenght == 2 && nodes[l[j]].m_lenght == 1 && nodes[u].m_ancestor != l[j])
                        {
                            SortedList<int,int> cycles4I = new SortedList<int,int>();
                            cyclesOfOrderi4++;
                        }
                    }
            }
 
            return cyclesOfOrderi4;    
        }

        private int fullSubGgraph(int u)    // Разобраться, почему не реализована соответсвующая функция интерфейса.
        {
            List<int> n1;
            n1 = m_container.Neighbourship[u];
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
                        if (m_container.AreNeighbours(n1[i], n2[j]) == false)
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

        private SortedDictionary<int, long> getNCyclesCount(int mincycleLenght, int maxcycleLenght)
        {
            int min = mincycleLenght;
            long count = 0;
            SortedDictionary<int, long> cyclesCount = new SortedDictionary<int, long>();
            for (int i = mincycleLenght; i <= maxcycleLenght; i++)
            {
                count = cyclesCounter.getCyclesCount(i);
                cyclesCount.Add(i, count);

            }
            return cyclesCount;
        }

        private int GetMaxFullSubgraph()
        {
            log.Info("Start calculate fullSubGgraph");
            int k = 0;
            for (int i = 0; i < m_container.Size; i++)
                if (this.fullSubGgraph(i) > k)
                    k = this.fullSubGgraph(i);
            log.Info("End calculate fullSubGgraph");
            return k;
        }
    }
}