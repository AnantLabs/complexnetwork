using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
            Algorithms.EigenValue ev = new EigenValue();
            bool[,] m = container.GetMatrix();
            return ev.EV(m);
        }

        // Возвращается распределение длин между собственными значениями. Реализовано.
        public override SortedDictionary<double, int> GetDistEigenPath()
        {
            log.Info("Getting distances between eigen values.");
            Algorithms.EigenValue ev = new EigenValue();
            bool[,] m = container.GetMatrix();
            ev.EV(m);
            return ev.CalcEigenValuesDist();
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


        // Закрытая часть класса (не из общего интерфейса). //

        private int[] minimalPathList;
        private double avgPathLenght = -1;
        private int diameter = -1;
        private SortedDictionary<int, int> pathDistribution = new SortedDictionary<int,int>();

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
                    if (n > 0) {
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
            SortedDictionary<int, int> degreeDistribution = new SortedDictionary<int,int>();
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
    }
}
