using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

using Core;
using Core.Exceptions;
using Core.Enumerations;
using Core.Model;
using NetworkModel.Engine.Eigenvalues;
using NetworkModel.Engine.Cycles;
using RandomNumberGeneration;

namespace NetworkModel
{
    /// <summary>
    /// Implementation of non hierarchic network's analyzer.
    /// </summary>
    public class NonHierarchicAnalyzer : AbstractNetworkAnalyzer
    {
        private NonHierarchicContainer container;

        public NonHierarchicAnalyzer(AbstractNetwork n) : base(n) { }

        public override INetworkContainer Container
        {
            get { return container; }
            set { container = (NonHierarchicContainer)value; }
        }

        protected override uint CalculateEdgesCountOfNetwork()
        {
            return (UInt32)container.ExistingEdgesCount();
        }

        protected override Double CalculateAveragePath()
        {
            if(!calledPaths)
                CountEssentialOptions();

            return averagePathLength;
        }

        protected override UInt32 CalculateDiameter()
        {
            if (!calledPaths)
                CountEssentialOptions();

            return diameter;
        }

        protected override Double CalculateAverageDegree()
        {
            return AverageDegree();
        }

        protected override Double CalculateAverageClusteringCoefficient()
        {
            double cycles3 = (double)CalculateCycles3(), sum = 0, degree = 0;
            for (int i = 0; i < container.Size; ++i)
            {
                degree = container.GetVertexDegree(i);
                sum += degree * (degree - 1);
            }

            return 6 * cycles3 / sum;
        }

        protected override double CalculateCycles3()
        {
            if (!calledPaths)
                CountEssentialOptions();

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

            return cycles3;
        }

        protected override double CalculateCycles4()
        {
            long count = 0;
            for (int i = 0; i < container.Size; i++)
                count += Get4OrderCyclesOfNode(i);

            return count / 4;
        }

        protected override List<double> CalculateEigenValues()
        {
            bool[,] m = container.GetMatrix();

            EigenValueUtils eg = new EigenValueUtils();            
            try
            {
                eigenValues = eg.CalculateEigenValue(m);
                calledEigens = true;
                return eigenValues;
            }
            catch (SystemException)
            {
                return new List<double>();
            }
        }

        protected override SortedDictionary<Double, UInt32> CalculateEigenDistanceDistribution()
        {
            bool[,] m = container.GetMatrix();

            EigenValueUtils eg = new EigenValueUtils();
            try
            {
                if (!calledEigens)
                    eg.CalculateEigenValue(m);

                return eg.CalcEigenValuesDist(eigenValues);
            }
            catch (SystemException)
            {
                return new SortedDictionary<Double, UInt32>();
            }
        }

        protected override SortedDictionary<UInt32, UInt32> CalculateDegreeDistribution()
        {
            return DegreeDistribution();
        }

        protected override SortedDictionary<Double, UInt32> CalculateClusteringCoefficientDistribution()
        {
            if (!calledPaths)
                CountEssentialOptions();

            double clusteringCoefficient = 0;
            int iEdgeCountForFullness = 0, iNeighbourCount = 0;
            double iclusteringCoefficient = 0;
            SortedDictionary<Double, UInt32> m_iclusteringCoefficient =
                new SortedDictionary<Double, UInt32>();

            SortedDictionary<int, double> iclusteringCoefficientList = new SortedDictionary<int, double>();
            for (int i = 0; i < container.Size; ++i)
            {
                iNeighbourCount = container.GetVertexDegree(i);
                if (iNeighbourCount != 0)
                {
                    iEdgeCountForFullness = (iNeighbourCount == 1) ? 1 : iNeighbourCount * (iNeighbourCount - 1) / 2;
                    iclusteringCoefficient = (edgesBetweenNeighbours[i]) / iEdgeCountForFullness;
                    iclusteringCoefficientList[i] = Math.Round(iclusteringCoefficient, 3);
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

        protected override SortedDictionary<UInt32, UInt32> CalculateConnectedComponentDistribution()
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

        protected override SortedDictionary<UInt32, UInt32> CalculateDistanceDistribution()
        {
            if (!calledPaths)
                CountEssentialOptions();

            return distanceDistribution;
        }

        protected override SortedDictionary<UInt32, UInt32> CalculateTriangleByVertexDistribution()
        {
            if (!calledPaths)
                CountEssentialOptions();

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

        protected override SortedDictionary<UInt16, Double> CalculateCycleDistribution(UInt16 lowBound, UInt16 hightBound)
        {
            CyclesCounter cyclesCounter = new CyclesCounter(container);
            SortedDictionary<UInt16, Double> cyclesCount =
                new SortedDictionary<UInt16, Double>();
            double count = 0;
            for (int i = lowBound; i <= hightBound; i++)
            {
                count = cyclesCounter.getCyclesCount(i);
                cyclesCount.Add((UInt16)i, count);
            }

            return cyclesCount;
        }

        protected override SortedDictionary<UInt32, Double> CalculateCycles3Trajectory()
        {
            // Retrieving research parameters from network. //
            // TODO without parce
            if(network.ResearchParameterValues == null)
                throw new CoreException("Research parameters are not set.");
            UInt32 stepCount = UInt32.Parse(network.ResearchParameterValues[ResearchParameter.EvolutionStepCount].ToString());
            Single nu = Single.Parse(network.ResearchParameterValues[ResearchParameter.Nu].ToString());
            bool permanentDistribution = Boolean.Parse(network.ResearchParameterValues[ResearchParameter.PermanentDistribution].ToString());
            object v = network.ResearchParameterValues[ResearchParameter.TracingStepIncrement];
            UInt16 tracingStepIncrement = ((v != null) ? UInt16.Parse(v.ToString()) : (ushort)0);

            // keep initial container
            NonHierarchicContainer initialContainer = container.Clone();

            SortedDictionary<UInt32, double> trajectory = new SortedDictionary<UInt32, double>();
            uint currentStep = 0;
            uint currentTracingStep = tracingStepIncrement;
            double currentCycle3Count = CalculateCycles3();
            trajectory.Add(currentStep, currentCycle3Count);

            NonHierarchicContainer previousContainer = new NonHierarchicContainer();
            RNGCrypto rand = new RNGCrypto();
            while (currentStep != stepCount)
            {
                previousContainer = container.Clone();
                try
                {
                    ++currentStep;

                    long deltaCount = permanentDistribution ?
                        container.PermanentRandomization() : 
                        container.NonPermanentRandomization();
                    double newCycle3Count = currentCycle3Count + deltaCount;

                    int delta = (int)(newCycle3Count - currentCycle3Count);
                    if (delta > 0)
                    {
                        // accept
                        trajectory.Add(currentStep, newCycle3Count);
                        currentCycle3Count = newCycle3Count;
                    }
                    else
                    {
                        double probability = Math.Exp((-nu * Math.Abs(delta)));
                        if (rand.NextDouble() < probability)
                        {
                            // accept
                            trajectory.Add(currentStep, newCycle3Count);
                            currentCycle3Count = newCycle3Count;
                        }
                        else
                        {
                            // reject
                            trajectory.Add(currentStep, currentCycle3Count);
                            container = previousContainer;
                        }
                    }

                    if (currentTracingStep == currentStep - 1)
                    {
                        container.Trace(network.ResearchName, 
                            "Realization_" + network.NetworkID.ToString(), 
                            "Matrix_" + currentTracingStep.ToString());
                        currentTracingStep += tracingStepIncrement;
                    }
                }
                catch (Exception ex)
                {
                    container = initialContainer;
                    //log.Error(String.Format("Error occurred in step {0} ,Error message {1} ", currentStep, ex.InnerException));
                }
            }

            container = initialContainer;
            return trajectory;
        }

        #region Utilities

        bool calledPaths = false;
        private double averagePathLength;
        private uint diameter;
        private SortedDictionary<uint, uint> distanceDistribution =
            new SortedDictionary<uint, uint>();
        private List<double> edgesBetweenNeighbours = new List<double>();

        bool calledEigens = false;
        private List<double> eigenValues = new List<double>();

        private class Node
        {
            public int ancestor = -1;
            public int lenght = -1;
            public int m_4Cycles = 0;
            public Node() { }
        }

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
            uint d = 0, uWay = 0;
            int k = 0;

            for (int i = 0; i < container.Size; ++i)
            {
                for (int j = i + 1; j < container.Size; ++j)
                {
                    int way = MinimumWay(i, j);
                    if (way == -1)
                        continue;
                    else
                        uWay = (uint)way;

                    if (distanceDistribution.ContainsKey(uWay))
                        ++distanceDistribution[uWay];
                    else
                        distanceDistribution.Add(uWay, 1);

                    if (uWay > d)
                        d = uWay;

                    avg += uWay;
                    ++k;
                }
            }

            Node[] nodes = new Node[container.Size];
            for (int t = 0; t < container.Size; ++t)
                nodes[t] = new Node();

            BFS((int)container.Size - 1, nodes);
            avg /= k;

            averagePathLength = avg;
            diameter = d;
            calledPaths = true;
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
        private SortedDictionary<uint, uint> DegreeDistribution()
        {
            SortedDictionary<uint, uint> degreeDistribution = new SortedDictionary<uint, uint>();
            for (uint i = 0; i < container.Size; ++i)
                degreeDistribution[i] = new uint();

            for (uint i = 0; i < container.Size; ++i)
            {
                uint degreeOfVertexI = (uint)container.Neighbourship[(int)i].Count;
                ++degreeDistribution[degreeOfVertexI];
            }

            for (uint i = 0; i < container.Size; ++i)
                if (degreeDistribution[i] == 0)
                    degreeDistribution.Remove(i);

            return degreeDistribution;
        }

        // Разобраться, почему не реализована соответсвующая функция интерфейса.
        private int fullSubGgraph(int u)
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
                        if (container.AreConnected(n1[i], n2[j]) == false)
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
                            if (container.AreConnected(neigboursList2[k], j) && neigboursList2[k] != neigboursList1[t] && neigboursList2[k] != neigboursList[i])
                                count++;
                    }
                }
            }

            return count / 2;
        }

        /// <summary>
        /// Calculates average degree of the network.
        /// </summary>
        /// <returns>Average degree.</returns>
        public double AverageDegree()
        {
            return container.CalculateNumberOfEdges() * 2 / (double)container.Size;
        }

        #endregion
    }
}
