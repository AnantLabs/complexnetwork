﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Model.ERModel.Realization;
using CommonLibrary.Model;
using Algorithms;
using log4net;

namespace Model.ERModel.Realization
{
    // Реализация анализатора (ER).
    public class ERAnalyzer : AbstarctGraphAnalyzer
    {
        // Организация работы с лог файлом.
        protected static readonly new ILog log = log4net.LogManager.GetLogger(typeof(ERAnalyzer));

        // Контейнер, в котором содержится граф конкретной модели (ER).
        private ERContainer container;
        static Random random1 = new Random();

      
        // Конструктор, получающий контейнер графа.
        public ERAnalyzer(ERContainer c)
        {
            log.Info("Creating ERAnalizer object.");
            container = c;
        }

        // Контейнер, в котором содержится сгенерированный граф (полученный от генератора).
        public override IGraphContainer Container
        {
            get { return container; }
            set { container = (ERContainer)value; }
        }

        // Возвращается средняя длина пути в графе. Реализовано.
        public override double GetAveragePath()
        {
            log.Info("Getting average path length.");

            if (-1 == avgPathLenght)
            {
                CountEssentialOptions();
            }
            return avgPathLenght;
        }

        // Возвращается диаметр графа. Реализовано.
        public override int GetDiameter()
        {
            log.Info("Getting diameter.");

            if (-1 == diameter)
            {
                CountEssentialOptions();
            }
            return diameter;
        }

        public override SortedDictionary<int, long> GetCycles(int lowBound, int hightBound)
        {
            log.Info("Getting cycles.");
            var cyclesCounter = new CyclesCounter(container);
            SortedDictionary<int, long> cyclesCount = new SortedDictionary<int, long>();
            long count = 0;
            for (int i = lowBound; i <= hightBound; i++)
            {
                count = cyclesCounter.getCyclesCount(i);
                cyclesCount.Add(i, count);
            }

            return cyclesCount;
        }

        // Возвращается число циклов длиной 3 в графе. Реализовано.
        public override int GetCycles3()
        {
            log.Info("Getting count of cycles - order 3.");
            int count = 0;
            for (int i = 0; i < container.Size; ++i)
            {
                List<int> nbs = container.Neighbourship[i];
                for (int j = 0; j < nbs.Count; ++j)
                {
                    List<int> tmp = container.Neighbourship[nbs[j]];
                    count += nbs.Intersect(tmp).Count();
                }
            }

            return count / 6;
        }
        private  int GetCyclesForTringle(ERContainer container)
        {
           // log.Info("Getting count of cycles - order 3.");

            int count = 0;
            for (int i = 0; i < container.Size; ++i)
            {
                count += CountTringlei(i, container.Size, container);
                //List<int> nbs = container.Neighbourship[i];
                //for (int j = 0; j < nbs.Count; ++j)
                //{
                //    List<int> tmp = container.Neighbourship[nbs[j]];
                //    count += nbs.Intersect(tmp).Count();
                //}
            }

            return count / 3;
        }

        private class NodeTrigle
        {
            public int ancestor = -1;
            public int lenght = -1;
            public int m_4Cycles = 0;
            public NodeTrigle() { }
        }

        private static int CountTringlei(int i, int size,ERContainer container)
        {
            var  nodes = new NodeTrigle[size];
            for (int j = 0; j < size; j++)
                nodes[j] = new NodeTrigle();

            int count = 0;
            nodes[i].lenght = 0;
            nodes[i].ancestor = 0;
            var  q = new Queue<int>();
            q.Enqueue(i);
            int u;

            while (q.Count != 0)
            {
                u = q.Dequeue();
                var  l = container.Neighbourship[u];
                for (int j = 0; j < l.Count; ++j)
                    if (nodes[l[j]].lenght == -1)
                    {
                        nodes[l[j]].lenght = nodes[u].lenght + 1;
                        nodes[l[j]].ancestor = u;
                        q.Enqueue(l[j]);
                    }
                    else
                    {
                        if (nodes[u].lenght == 1 && nodes[l[j]].lenght == 1)
                        {
                            ++count;
                        }
                    }
            }

            return count /= 2;
        }
        // Возвращается число циклов длиной 4 в графе. Реализовано.
        public override int GetCycles4()
        {
            log.Info("Getting count of cycles - order 4.");
            int count = 0;
            for (int i = 0; i < container.Size; ++i)
            {
                List<int> nbs = container.Neighbourship[i];

                for (int j = 0; j < nbs.Count; ++j)
                {
                    List<int> lj = container.Neighbourship[nbs[j]];

                    for (int k = 0; k < nbs.Count; ++k)
                    {
                        if (k != j)
                        {
                            List<int> lk = container.Neighbourship[nbs[k]];
                            count += (lj.Intersect(lk).Count() - 1);
                        }
                    }
                }
            }

            return count / 8;
        }

        // Возвращается массив собственных значений матрицы смежности. Реализовано.
        public override ArrayList GetEigenValues()
        {
            log.Info("Getting eigen values array.");

            bool[,] m = container.GetMatrix();

            EigenValueUtils eg = new EigenValueUtils();

            try
            {
                return eg.CalculateEigenValue(m);

            }
            catch (Exception ex)
            {
                log.Error(ex);
                return new ArrayList();
            }
        }

        // Возвращается распределение длин между собственными значениями. Реализовано.
        public override SortedDictionary<double, int> GetDistEigenPath()
        {
            log.Info("Getting distances between eigen values.");

            bool[,] m = container.GetMatrix();

            EigenValueUtils eg = new EigenValueUtils();

            try
            {
                eg.CalculateEigenValue(m);
                return eg.CalcEigenValuesDist();

            }
            catch (Exception ex)
            {
                log.Error(ex);
                return new SortedDictionary<double, int>();
            }

        }

        // Возвращается степенное распределение графа. Реализовано.
        public override SortedDictionary<int, int> GetDegreeDistribution()
        {
            log.Info("Getting degree distribution.");

            return CountDegreeDestribution();
        }

        // Возвращается распределение коэффициентов кластеризации графа. Реализовано.
        public override SortedDictionary<double, int> GetClusteringCoefficient()
        {
            log.Info("Getting clustering coefficients.");

            return CountGraphClusteringCoefficient();
        }

        // Возвращается распределение длин минимальных путей в графе. Реализовано.
        public override SortedDictionary<int, int> GetMinPathDist()
        {
            log.Info("Getting minimal distances between vertices.");

            if (-1 == avgPathLenght)
            {
                CountEssentialOptions();
            }

            return pathDistribution;
        }

        public override SortedDictionary<int, double> GetTrianglesTrajectory(double constant, BigInteger stepcount)
        {
          
            log.Info("Getting triangle trajectory.");

            log.Error("Getting triangle trajectory.");

            var stepscount = stepcount;
            var tarctory = new SortedDictionary<int, double>();
            int time = 0;
            int currentcounttriangle = GetCyclesForTringle(container);

            tarctory.Add(time, currentcounttriangle);

            var currentContainer = container.Copy();

            var tempContainer = new ERContainer();
            var randeom = new Random();
            while (time != stepcount)
            {
                try
                {
                    time++;
                    var count = 0;
                    tempContainer = Transformations(currentContainer, randeom,out count);
                
                   
                    var counttriangle = currentcounttriangle + count;

                    var delta = counttriangle - currentcounttriangle;

                    if (delta > 0)
                    {
                        tarctory.Add(time, counttriangle);
                        currentContainer = tempContainer.Copy();
                        currentcounttriangle = counttriangle;

                    }
                    else
                    {
                       

                        if (new Random().NextDouble() < CalculatePropability(delta, constant))
                        {
                            tarctory.Add(time, counttriangle);
                            currentContainer = tempContainer.Copy();
                            currentcounttriangle = counttriangle;

                        }
                        else
                        {
                            tarctory.Add(time, currentcounttriangle);
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error(String.Format("Error occurred in step {0} ,Error message {1} ", stepcount, ex.InnerException));
                }

            }

            return tarctory;
        }




        // Закрытая часть класса (не из общего интерфейса). //

        private int[] minimalPathList;
        private double avgPathLenght = -1;
        private int diameter = -1;
        private SortedDictionary<int, int> pathDistribution = new SortedDictionary<int, int>();

        // Внутренный тип для работы BFS алгоритма.
        private class Node
        {
            public int length = -1;
            public bool visited = false;

            public Node() { }
        }

        // Реализация BFS алгоритма.
        private void bfs(int s)
        {
            minimalPathList = new int[container.Size];
            Queue<int> queue = new Queue<int>();
            Node[] nodes = new Node[container.Size];
            for (int i = 0; i < container.Size; ++i)
            {
                minimalPathList[i] = -1;
                nodes[i] = new Node();
            }

            nodes[s].length = 0;
            nodes[s].visited = true;

            minimalPathList[s] = 0;

            queue.Enqueue(s);
            while (queue.Count != 0)
            {
                int t = queue.Dequeue();
                List<int> tmp = container.Neighbourship[t];
                for (int i = 0; i < tmp.Count; ++i)
                {
                    int e = tmp[i];
                    if (nodes[e].visited == false)
                    {
                        nodes[e].visited = true;
                        nodes[e].length = nodes[t].length + 1;
                        queue.Enqueue(e);
                        minimalPathList[e] = nodes[e].length;
                    }
                }
            }
        }

        // Выполняет подсчет сразу 3 свойств - средняя длина пути, диаметр и пути между вершинами.
        // Нужно вызвать перед получением этих свойств не изнутри.
        private void CountEssentialOptions()
        {
            log.Info("Counting essential options.");
            int size = container.Size;
            int d = 0;
            int count = 0, sum = 0;

            for (int i = 0; i < size; ++i)
            {
                bfs(i);
                d = Math.Max(d, minimalPathList.Max());

                for (int j = 0; j < size; ++j)
                {
                    int n = minimalPathList[j];
                    if (n > 0)
                    {
                        sum += n;
                        if (pathDistribution.ContainsKey(n))
                        {
                            pathDistribution[n]++;
                        }
                        else
                        {
                            pathDistribution.Add(n, 1);
                        }
                        count++;
                    }
                }
            }

            for (int i = 0; i < size; ++i)
            {
                if (pathDistribution.ContainsKey(i))
                {
                    pathDistribution[i] /= 2;
                }
            }

            diameter = d;
            avgPathLenght = Math.Round((double)sum / count, 4);
        }

        // Возвращает распределение степеней.
        private SortedDictionary<int, int> CountDegreeDestribution()
        {
            SortedDictionary<int, int> degreeDistribution = new SortedDictionary<int, int>();
            int avg = 0;

            for (int i = 0; i < container.Size; ++i)
            {
                int n = container.Neighbourship[i].Count;
                avg += n;
                if (degreeDistribution.ContainsKey(n))
                {
                    degreeDistribution[n]++;
                }
                else
                {
                    degreeDistribution.Add(n, 1);
                }
            }

            return degreeDistribution;
        }

        // Возвращает коэффициент кластеризации.
        private SortedDictionary<double, int> CountGraphClusteringCoefficient()
        {
            log.Info("Counting graph clustering coefficient.");
            SortedDictionary<double, int> vertexClusteringCoefficient = new SortedDictionary<double, int>();
            double r = 0.0;
            double count = 0.0;
            int size = container.Size;

            for (int i = 0; i < size; ++i)
            {
                r = Math.Round(GetVertexClusteringCoefficient(i), 4);
                if (vertexClusteringCoefficient.ContainsKey(r))
                {
                    vertexClusteringCoefficient[r]++;
                }
                else
                {
                    vertexClusteringCoefficient.Add(r, 1);
                }
                count += r;
            }

            return vertexClusteringCoefficient;
        }

        // Возвращает коэффициент кластеризации данной вершины.
        private double GetVertexClusteringCoefficient(int i)
        {
            int count = 0;
            List<int> neighbors = container.Neighbourship[i];
            int neighbor_count = neighbors.Count;
            if (neighbor_count < 2)
            {
                return 0;
            }

            for (int j = 0; j < neighbor_count; ++j)
            {
                List<int> tmp = container.Neighbourship[neighbors[j]];
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

        // Возвращается распределение чисел мотивов. Реализовано.
        public override SortedDictionary<int, float> GetMotivs(int lowBound, int hightBound)
        {
            log.Info("Getting motifs.");

            var motivfinder = new MotifFinder();
            var motifisCount = new SortedDictionary<int, float>();
            var motifisCountResult = new SortedDictionary<int, float>();
            Graph graph = Graph.reformatToOurGraghFromBAContainer(container.Neighbourship);
            for (int motifDegree = lowBound; motifDegree <= hightBound; motifDegree++)
            {
                motivfinder.SearchMotifs(graph, motifDegree);
                motifisCount = motivfinder.dictionaryIdsValues();
                foreach (var key in motifisCount.Keys)
                    motifisCountResult.Add(key, motifisCount[key]);
            }

            return motifisCountResult;
        }


        private static double CalculatePropability(long delta, double constant)
        {
            return Math.Exp((-constant * Math.Abs(delta)));
        }

        private  ERContainer Transformations(ERContainer trcontainer,Random random, out int  triangle)
        {
            var tranformation = trcontainer.Copy();

            triangle = 0;

            try
            {

                var removeedje = random.Next(0, tranformation.Edjes.Count - 1);
                var addEdje = random.Next(0, tranformation.NoEdjes.Count - 1);

                var removeedjeVertix1 = tranformation.Edjes[removeedje].Key;
                var removeedjeVertix2 = tranformation.Edjes[removeedje].Value;

                var addEdjeVertix1 = tranformation.NoEdjes[addEdje].Key;
                var addEdjeVertix2 = tranformation.NoEdjes[addEdje].Value;
               

                //Remove edje 
                tranformation.Neighbourship[removeedjeVertix1].Remove(removeedjeVertix2);
                tranformation.Neighbourship[removeedjeVertix2].Remove(removeedjeVertix1);


                tranformation.NoEdjes.Add(tranformation.Edjes[removeedje]);
                tranformation.Edjes.RemoveAt(removeedje);

                //Count remove triangle
                int removetriangle = CountTriangle(removeedjeVertix1,
                   removeedjeVertix2, tranformation);


                //add edje
                tranformation.Neighbourship[addEdjeVertix1].Add(addEdjeVertix2);
                tranformation.Neighbourship[addEdjeVertix2].Add(addEdjeVertix1);

                tranformation.Edjes.Add(tranformation.NoEdjes[addEdje]);
                tranformation.NoEdjes.RemoveAt(addEdje);

                // Count Add triangle

                int addtriangle = CountTriangle(addEdjeVertix1,
                   addEdjeVertix2, tranformation);

                triangle = addtriangle - removetriangle;
            }

            catch (Exception ex)
            {
                log.Error("Error according when transforms edges");
            }

            return tranformation;

        }

        private  int CountTriangle(int i, int j,ERContainer container)
        {
            var list = container.Neighbourship[j];

            int count = 0;
            for (int t = 0; t < list.Count; t++)
            {
                if (list[t] != i)
                {
                    if (container.Neighbourship[list[t]].Contains(i))
                        count++;
                }
            }

            return count;
        }


    }
}
