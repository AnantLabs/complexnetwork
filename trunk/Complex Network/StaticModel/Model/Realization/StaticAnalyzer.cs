using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

using RandomGraph.Common.Model;
using Model.StaticModel.Result;

namespace Model.StaticModel.Realization
{
    public class StaticAnalyzer
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
        private StaticContainer m_container;
        private AnalyzeResult m_result;
        private bool Circleorder4;
        private List<double> m_edgesBetweenNeighbours;
        public StaticAnalyzer(StaticContainer c)
        {
            m_container = c;
            m_result = new AnalyzeResult();
          
            m_edgesBetweenNeighbours = new List<double>();
            for (int i = 0; i < m_container.Size; ++i)
                m_edgesBetweenNeighbours.Add(-1);
           


        }
        public AnalyzeResult Analyze(AnalyseOptions options)
        {
            if (((options & AnalyseOptions.AveragePath) == AnalyseOptions.AveragePath) ||
                ((options & AnalyseOptions.Diameter) == AnalyseOptions.Diameter) ||
                ((options & AnalyseOptions.Cycles4) == AnalyseOptions.Cycles4) || ((options & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient) || ((options & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3))
            {

                CountAnalyzeOptions();

            }
            if ((options & AnalyseOptions.ClusteringCoefficient) == AnalyseOptions.ClusteringCoefficient)
            {
                CountClusteringCoefficient();
            }
            if ((options & AnalyseOptions.DegreeDistribution) == AnalyseOptions.DegreeDistribution)
            {
                CountDegreeDistribution();
            }
            if ((options & AnalyseOptions.Cycles3) == AnalyseOptions.Cycles3)
            {
                CountCyclesOfOrder3();
            }
            if ((options & AnalyseOptions.FullSubGraph) == AnalyseOptions.FullSubGraph)
            {
                MaxFullSubgraph();
            }
            if ((options & AnalyseOptions.EigenValue) == AnalyseOptions.EigenValue)
            {
                CalculateEngineValues();
            }
            return m_result;
        }

        // Utilities //
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
                        else
                            if (Circleorder4)
                            {
                                if (nodes[u].m_lenght == 2 && nodes[l[j]].m_lenght == 1 && nodes[u].m_ancestor != l[j])
                                    ++m_result.m_cyclesOfOrder4;
                            }
                    }


            }
            if (b)
                m_edgesBetweenNeighbours[i] /= 2;
        }

        public void CountAnalyzeOptions()
        {
          //  log.Info("Start count Diametr");
           
         //   m_pathDistribution = new SortedDictionary<int, int>();
           
            double avg = 0;
            int diametr = 0, k = 0;

            for (int i = 0; i < m_container.Size; ++i)
            {
                Circleorder4 = true;
                for (int j = i + 1; j < m_container.Size; ++j)
                {
                    int way = MinimumWay(i, j);
                    Circleorder4 = false;
                    if (way == -1)
                        continue;
                  //  if (m_pathDistribution.ContainsKey(way))
                  //      m_pathDistribution[way]++;
                 //   else
                 //       m_pathDistribution.Add(way, 1);

                    if (way > diametr)
                        diametr = way;

                    avg += way;
                    ++k;
                }
            }
            Node[] nodes = new Node[m_container.Size];
            for (int t = 0; t < m_container.Size; ++t)
                nodes[t] = new Node();
            Circleorder4 = true;

            BFS(m_container.Size - 1, nodes);
            avg /= k;

            

           


        }
        private void CountClusteringCoefficient()
        {
            int iEdgeCountForFullness = 0, iNeighbourCount = 0;
            double iclusteringCoefficient = 0;
            m_result.m_iclusteringCoefficient = new SortedDictionary<double, int>();

            SortedDictionary<int, double> iclusteringCoefficientList = new SortedDictionary<int, double>();
            for (int i = 0; i < m_container.Size; ++i)
            {
                iNeighbourCount = m_container.CountVertexDegree(i);
                if (iNeighbourCount != 0)
                {
                    iEdgeCountForFullness = (iNeighbourCount == 1) ? 1 : iNeighbourCount * (iNeighbourCount - 1) / 2;
                    iclusteringCoefficient = (m_edgesBetweenNeighbours[i]) / iEdgeCountForFullness;
                    iclusteringCoefficientList[i] = iclusteringCoefficient;


                    m_result.m_clusteringCoefficient += iclusteringCoefficient;
                }
                else
                    iclusteringCoefficientList[i] = 0;

            }

            m_result.m_clusteringCoefficient /= m_container.Size;

            for (int i = 0; i < m_container.Size; ++i)
            {
                double result = iclusteringCoefficientList[i];
                if (m_result.m_iclusteringCoefficient.Keys.Contains(result))
                    m_result.m_iclusteringCoefficient[iclusteringCoefficientList[i]]++;
                else
                    m_result.m_iclusteringCoefficient[iclusteringCoefficientList[i]] = 1;
            }
        }
        private void CountDegreeDistribution()
        {
            m_result.m_degreeDistribution = new SortedDictionary<int, int>();
            for (int i = 0; i < m_container.Size; ++i)
                m_result.m_degreeDistribution[i] = new int();
            for (int i = 0; i < m_container.Size; ++i)
            {
                int degreeOfVertexI = m_container.Neighbourship[i].Count;
                m_result.m_degreeDistribution[degreeOfVertexI]++;
            }
            for (int i = 0; i < m_container.Size; i++)
                if (m_result.m_degreeDistribution[i] == 0)
                    m_result.m_degreeDistribution.Remove(i);


        }


        private void CountCyclesOfOrder3()
        {
            m_result.m_cyclesOfOrder3 = 0;
            for (int i = 0; i < m_container.Size; ++i)
                if (m_edgesBetweenNeighbours[i] != -1)
                    m_result.m_cyclesOfOrder3 += (int)m_edgesBetweenNeighbours[i];
            if (m_result.m_cyclesOfOrder3 > 0 && m_result.m_cyclesOfOrder3 < 3)

                m_result.m_cyclesOfOrder3 = 1;
            else
                m_result.m_cyclesOfOrder3 /= 3;
        }

        private int fullSubGgraph(int u)
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
        private void MaxFullSubgraph()
        {
            int k = 0;
            for (int i = 0; i < m_container.Size; i++)
                if (this.fullSubGgraph(i) > k)
                    k = this.fullSubGgraph(i);
            m_result.m_maxfullsubgraph = k;
        }
        private Matrix GetMatrixForEngineValues()
        {
            bool[,] matrix = m_container.GetMatrix();
            Vector vectorRow;
            IList<Vector> ListOfVector = new List<Vector>();
            Matrix AdjMatrix;
            double[] vectorEl = new double[m_container.Size];
            for (int i = 0; i < m_container.Size; i++)
            {
                for (int j = 0; j < m_container.Size; j++)
                    vectorEl[j] = Convert.ToDouble(matrix[i, j]);
                vectorRow = new Vector(vectorEl);
                ListOfVector.Add(vectorRow);
            }

            AdjMatrix = Matrix.CreateFromRows(ListOfVector);
            return AdjMatrix;
        }

        private int isInArray(double[] array, double element)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] == element)
                    return i;
            }
            return -1;
        }
        private void CalculateEngineValues()
        {
            Matrix AdjMatrix = GetMatrixForEngineValues();
            EigenvalueDecomposition values = new EigenvalueDecomposition(AdjMatrix);
            ComplexVector EigVal = values.EigenValues;

            double[] ArrayOfEigVal = new double[EigVal.Length];

            for (int i = 0; i < ArrayOfEigVal.Length; ++i)
            {
                ArrayOfEigVal[i] = EigVal[i].Real;
            }

            Array.Sort(ArrayOfEigVal);
            m_result.ArrayOfEigVal = new ArrayList();
            for (int i = 0; i < ArrayOfEigVal.Length; i++)
                m_result.ArrayOfEigVal.Add ((Object)ArrayOfEigVal[i]);
            /*
            double[] dist = new double[ArrayOfEigVal.Length - 1];

            for (int i = 0; i < dist.Length; ++i)
            {
                dist[i] = ArrayOfEigVal[i + 1] - ArrayOfEigVal[i];
                dist[i] = Math.Round(dist[i], 4);
            }

            double[] array1 = new double[dist.Length];
            int[] count = new int[dist.Length];

            for (int i = 0, j = 0; i < dist.Length; ++i, ++j)
            {
                if (isInArray(array1, dist[i]) == -1)
                {
                    array1[j] = dist[i];
                    count[j]++;
                }
                else
                {
                    count[isInArray(array1, dist[i])]++;
                    j--;
                }
            }

            */
        }
    }
}

/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model.BAModel.Result;

namespace Model.BAModel.Realization
{
    public class BAAnalyzer
    {
        // Type definitions 
        private enum Colors { WHITE, GREY, BLACK };
        private class Node
        {
            public Colors m_color;
            public int m_ancestor;
            public int m_lenght;

            public Node()
            {
                m_color = Colors.WHITE;
                m_ancestor = -1;
                m_lenght = -1;
            }
        }

        private BAContainer m_container;
        private int[] m_classteringCoeff;   // ??
        private AnalyzeResult m_result;

      
       

        public void Start()
        {
            CountAvgPathAndDiametr();
            DegreeDistribution();
            Cycle3();
        }

        public BAAnalyzer(BAContainer c) //constructer
        {
            m_container = c;
            m_result = new AnalyzeResult();
            m_classteringCoeff = new int[m_container.Size()];
            for (int i = 0; i < m_classteringCoeff.Length; i++)
                m_classteringCoeff[i] = -1;
        }
    }
}
*/