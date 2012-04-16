using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

using RandomGraph.Common.Model;
using CommonLibrary.Model;

namespace Model.BAModel.Realization
{
    public class BAAnalyzer : AbstarctGraphAnalyzer
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
        private BAContainer m_container;
        private double m_avgPath;
        private bool[] Circleorder4;
        private List<double> m_edgesBetweenNeighbours;
        private int m_diametr;
        private SortedDictionary<int, int> m_pathDistribution;
        private double m_clusteringCoefficient;
        private int m_cyclesOfOrder4;
        public BAAnalyzer(BAContainer c)
        {
            m_container = c;
            Circleorder4 = new bool[m_container.Size];
            m_edgesBetweenNeighbours = new List<double>();
            for (int i = 0; i < m_container.Size; ++i)
                m_edgesBetweenNeighbours.Add(-1);
            for (int i = 0; i < m_container.Size; ++i)
                Circleorder4[i] = false;



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
                            if (Circleorder4[i] == false)
                            {
                                if (nodes[u].m_lenght == 2 && nodes[l[j]].m_lenght == 1 && nodes[u].m_ancestor != l[j])
                                    m_cyclesOfOrder4++;
                            }
                    }


            }
            if (b)
                m_edgesBetweenNeighbours[i] /= 2;
        }

        public void CountAnalyzeOptions()
        {
            m_pathDistribution = new SortedDictionary<int, int>();
            m_cyclesOfOrder4 = 0;
            double avg = 0;
            int diametr = 0, k = 0;

            for (int i = 0; i < m_container.Size; ++i)
            {

                for (int j = i + 1; j < m_container.Size; ++j)
                {
                    if (j == i + 2)
                        Circleorder4[i] = true;
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
            Circleorder4[m_container.Size - 1] = true;

            BFS(m_container.Size - 1, nodes);
            avg /= k;

            m_avgPath = avg;
            m_diametr = diametr;
           
            if (m_cyclesOfOrder4 >= 4)
                m_cyclesOfOrder4 /= 4;
        //    CountClusteringCoefficient();
          //  CountCyclesOfOrder3();
        

        }
        public double GetAveragePath()
        {
            return m_avgPath;
        }
        public double GetDiameter()
        {
            return m_diametr;
        }
        public SortedDictionary<int, int> GetMinPathDist()
        {
            return m_pathDistribution;
               
        }
        public  SortedDictionary<double, int> GetClusteringCoefficient()
        {
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
                    iclusteringCoefficientList[i] = iclusteringCoefficient;
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
       public SortedDictionary<int, int> GetDegreeDistribution()
        {
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
            return m_degreeDistribution;


        }


       public double GetCycles3()
        {
           double m_cyclesOfOrder3 = 0;
            for (int i = 0; i < m_container.Size; ++i)
                if (m_edgesBetweenNeighbours[i] != -1)
                      m_cyclesOfOrder3 += (int)m_edgesBetweenNeighbours[i];
            if (m_cyclesOfOrder3 > 0 && m_cyclesOfOrder3 < 3)
                 m_cyclesOfOrder3 = 1;
            else
                m_cyclesOfOrder3 /= 3;
            return m_cyclesOfOrder3;
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
        public  int GetMaxFullSubgraph()
        {
            int k = 0;
            for (int i = 0; i < m_container.Size; i++)
                if (this.fullSubGgraph(i) > k)
                    k = this.fullSubGgraph(i);

            return k;
        }
        //Calculate distribution of connected subgraph of graph.
        public  SortedDictionary<int, int> GetConnSubGraph()
        {
            return new SortedDictionary<int, int>();
        }
        //Calculate count of cycles in 3 lenght based in eigen valu of graph.
        public  double GetCycleEigen3()
        {
            return 0;
        }

        //Calculate count of cycles in 4 lenght of graph.
        public  double GetCycles4()
        {
            return 0;
        }

        //Calculate count of cycles in 4 lenght based in eigen valu of graph.
        public  double GetCycleEigen4()
        {
            return 0;
        }
        //Calculate motive of graph.
        public void GetMotif()
        {
        }
        //Calculate distribution of eigen value of graph.
        public  SortedDictionary<double, int> GetDistEigenPath()
        {
            return new SortedDictionary<double, int>();
        }
        //Calculate distribution of connected subgraph of graph.
        public  SortedDictionary<int, int> GetFullSubGraph()
        {
            return new SortedDictionary<int, int>();
        }
        //Calculate Eigen values of graph.
        public  ArrayList GetEigenValue()
        {
            return new ArrayList();
        }
        /*
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

           

        }
         **/


      
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