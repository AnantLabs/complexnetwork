﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

using RandomGraph.Common.Model;
using CommonLibrary.Model;
using Motifs;
using log4net;
using Algorithms;

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
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(BAAnalyzer));
        // Implementation members //
        private BAContainer m_container;
        private CyclesCounter cyclesCounter;
        private double m_avgPath;
        private bool Circleorder4;
        private List<double> m_edgesBetweenNeighbours;
        private int m_diametr;
        private SortedDictionary<int, int> m_pathDistribution;
        private double m_clusteringCoefficient;
        private int m_cyclesOfOrder4;
        private int startCountAnalyzeOptions; 
        public BAAnalyzer(BAContainer c)
        {
            m_container = c;
            
            m_edgesBetweenNeighbours = new List<double>();
            for (int i = 0; i < m_container.Size; ++i)
                m_edgesBetweenNeighbours.Add(-1);
            cyclesCounter = new CyclesCounter(m_container);



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
                                    m_cyclesOfOrder4++;
                            }
                            
                    }


            }
            if (b)
                m_edgesBetweenNeighbours[i] /= 2;
        }

        public void CountAnalyzeOptions()
        {
            log.Info("Start count Diametr");
            startCountAnalyzeOptions = 1;
            m_pathDistribution = new SortedDictionary<int, int>();
            m_cyclesOfOrder4 = 0;
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
            Circleorder4 = true;

            BFS(m_container.Size - 1, nodes);
            avg /= k;

            m_avgPath = avg;
            m_diametr = diametr;

            if (m_cyclesOfOrder4 >= 4)
                m_cyclesOfOrder4 /= 4;


        }

        private int CalculatCycles4(int i, Node[] nodes)
        {
            int cyclesOfOrderi4 = 0;
            nodes[i].m_lenght = 0;
            nodes[i].m_ancestor = 0;
            bool b = true;
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
                            cyclesOfOrderi4++;
                            

                    }


            }
            return cyclesOfOrderi4;
    
        }
        public override double GetAveragePath()
        {
            if (startCountAnalyzeOptions != 1)
                CountAnalyzeOptions();
               return Math.Round(m_avgPath,14);
        }
        public override int GetDiameter()
        {
            log.Info("End count Diametr");
            if (startCountAnalyzeOptions != 1)
                CountAnalyzeOptions();
            return m_diametr;
        }
        public override SortedDictionary<int, int> GetMinPathDist()
        {
            if (startCountAnalyzeOptions != 1)
                CountAnalyzeOptions();
            return m_pathDistribution;

        }
        public override SortedDictionary<double, int> GetClusteringCoefficient()
        {
            if (startCountAnalyzeOptions != 1)
                CountAnalyzeOptions();
            log.Info("Start calculate ClusteringCoefficient ");
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
                    iclusteringCoefficientList[i] = Math.Round(iclusteringCoefficient,14);
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
            log.Info("End calculate ClusteringCoefficient ");
            return m_iclusteringCoefficient;
        }
        public override SortedDictionary<int, int> GetDegreeDistribution()
        {
            log.Info("Start calculat DegreeDistribution");
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


        public override int GetCycles3()
        {
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

        public SortedDictionary<int, long> getNCyclesCount(int mincycleLenght, int maxcycleLenght)
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
        public int GetMaxFullSubgraph()
        {
            log.Info("Start calculate fullSubGgraph");
            int k = 0;
            for (int i = 0; i < m_container.Size; i++)
                if (this.fullSubGgraph(i) > k)
                    k = this.fullSubGgraph(i);
            log.Info("End calculate fullSubGgraph");
            return k;
        }
        //Calculate distribution of connected subgraph of graph.
        public override SortedDictionary<int, int> GetConnSubGraph()
        {
            return new SortedDictionary<int, int>();
        }
        //Calculate count of cycles in 3 lenght based in eigen valu of graph.
        public override int GetCycleEigen3()
        {
            return 0;
        }

        //Calculate count of cycles in 4 lenght of graph.
        public override int GetCycles4()
        {
            int count = 0;
            //Node[] nodes = new Node[m_container.Size];
            //for (int k = 0; k < m_container.Size; ++k)
            //    nodes[k] = new Node();
            //for(int i = 0 ;i<m_container.Size;i++)
            //    count+=CalculatCycles4(i, nodes);
            //int nmn =count;
            return count/4;
        }

        //Calculate count of cycles in 4 lenght based in eigen valu of graph.
        public override int GetCycleEigen4()
        {
            return 0;
        }
        //Calculate motive of graph.
        public override SortedDictionary<int, float> GetMotif(int minMotiv,int maxMotiv)
        {
            Graph graph = Graph.reformatToOurGraghFromBAContainer(m_container);
            MotifFinder.SearchMotifs(graph, minMotiv);
            return MotifFinder.dictionaryIdsValues();
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
        //Calculate Eigen values of graph.
        public override ArrayList GetEigenValue()
        {
            Algorithms.EigenValue ev = new EigenValue();
            bool[,] m = m_container.GetMatrix();
            return ev.EV(m);
        }
    }
}
        //private Matrix GetMatrixForEngineValues()
        //{
        //    bool[,] matrix = m_container.GetMatrix();
        //    Vector vectorRow;
        //    IList<Vector> ListOfVector = new List<Vector>();
        //    Matrix AdjMatrix;
        //    double[] vectorEl = new double[m_container.Size];
        //    for (int i = 0; i < m_container.Size; i++)
        //    {
        //        for (int j = 0; j < m_container.Size; j++)
        //            vectorEl[j] = Convert.ToDouble(matrix[i, j]);
        //        vectorRow = new Vector(vectorEl);
        //        ListOfVector.Add(vectorRow);
        //    }

        //    AdjMatrix = Matrix.CreateFromRows(ListOfVector);
        //    return AdjMatrix;
        //}

        //private int isInArray(double[] array, double element)
        //{
        //    for (int i = 0; i < array.Length; ++i)
        //    {
        //        if (array[i] == element)
        //            return i;
        //    }
        //    return -1;
        //}
        //public void CalculateEngineValues()
        //{
        //    Matrix AdjMatrix = GetMatrixForEngineValues();
        //    EigenvalueDecomposition values = new EigenvalueDecomposition(AdjMatrix);
        //    ComplexVector EigVal = values.EigenValues;

        //    double[] ArrayOfEigVal = new double[EigVal.Length];

        //    for (int i = 0; i < ArrayOfEigVal.Length; ++i)
        //    {
        //        ArrayOfEigVal[i] = EigVal[i].Real;
        //    }

        //    Array.Sort(ArrayOfEigVal);
            //m_result.ArrayOfEigVal = new ArrayList();
            //for (int i = 0; i < ArrayOfEigVal.Length; i++)
            //    m_result.ArrayOfEigVal.Add ((Object)ArrayOfEigVal[i]);
       
            //double[] dist = new double[ArrayOfEigVal.Length - 1];

            //for (int i = 0; i < dist.Length; ++i)
            //{
            //    dist[i] = ArrayOfEigVal[i + 1] - ArrayOfEigVal[i];
            //    dist[i] = Math.Round(dist[i], 4);
            //}

            //double[] array1 = new double[dist.Length];
            //int[] count = new int[dist.Length];

            //for (int i = 0, j = 0; i < dist.Length; ++i, ++j)
            //{
            //    if (isInArray(array1, dist[i]) == -1)
            //    {
            //        array1[j] = dist[i];
            //        count[j]++;
            //    }
            //    else
            //    {
            //        count[isInArray(array1, dist[i])]++;
            //        j--;
            //    }
            //}

           

//        }
         



//    }
//}

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