using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using CommonLibrary.Model;
using log4net;

namespace Model.NonRegularHierarchicModel.Realization
{
    // Реализация анализатора (Block-Hierarchic Non Regular).
    public class NonRegularHierarchicAnalyzer : AbstarctGraphAnalyzer
    {
        // Организация работы с лог файлом.
        protected static readonly new ILog log = log4net.LogManager.GetLogger(typeof(NonRegularHierarchicAnalyzer));

        // Контейнер, в котором содержится граф конкретной модели (Block-Hierarchic Non Regular).
        private NonRegularHierarchicContainer container;

        public NonRegularHierarchicAnalyzer(NonRegularHierarchicContainer c)
        {
            log.Info("Creating NonRegularHierarchicAnalyzer object.");
            container = c;
        }

        // Контейнер, в котором содержится сгенерированный граф (полученный от генератора).
        public override AbstractGraphContainer Container
        {
            get { return container; }
            set { container = (NonRegularHierarchicContainer)value; }
        }

        // Возвращается средняя длина пути в графе. Реализовано.
        public override double GetAveragePath()
        {
            log.Info("Getting average path length.");

            if (-1 == avgPath)
            {
                CountPathDistribution();
            }

            return Math.Round(avgPath, 14);
        }

        // Возвращается диаметр графа. Реализовано.
        public override int GetDiameter()
        {
            log.Info("Getting diameter.");

            if (-1 == diameter)
            {
                CountPathDistribution();
            }

            return diameter;
        }

        // Возвращается число циклов длиной 3 в графе. Реализовано.
        public override long GetCycles3()
        {
            throw new NotImplementedException();
            /*log.Info("Getting count of cycles - order 3.");
            return (long)(container.Get3CirclesCount());*/
        }

        // Возвращается число циклов длиной 4 в графе. Реализовано.
        public override long GetCycles4()
        {
            throw new NotImplementedException();
            /*log.Info("Getting count of cycles - order 4.");
            return (long)container.Get4CirclesCount();*/
        }

        // Возвращается степенное распределение графа. Реализовано.
        public override SortedDictionary<int, int> GetDegreeDistribution()
        {
            throw new NotImplementedException();
            /*log.Info("Getting degree distribution.");

            SortedDictionary<int, int> result = new SortedDictionary<int, int>();
            /// Iterate over all the vertexes and count degrees.
            uint v;
            int degree;
            for (v = 0; v < container.node.VertexCount; ++v)
            {
                degree = (int)(container.GetDegree(v));
                if (!result.ContainsKey(degree))
                {
                    result.Add(degree, 1);
                }
                else
                {
                    ++result[degree];
                }
            }
            return result;*/
        }

        // Возвращает распределение триугольников, прикрепленных к вершине.
        public override SortedDictionary<int, int> GetTrianglesDistribution()
        {
            throw new NotImplementedException();
            /*log.Info("Getting triangles distribution.");
            SortedDictionary<int, int> result = new SortedDictionary<int, int>();

            for (uint i = 0; i < container.Size; ++i)
            {
                int triangleCountOfVertex = (int)container.Get3CirclesCountWithVertex(i);
                if (result.Keys.Contains(triangleCountOfVertex))
                    ++result[triangleCountOfVertex];
                else
                    result.Add(triangleCountOfVertex, 1);
            }

            return result;*/
        }

        // Возвращается распределение коэффициентов кластеризации графа. Реализовано.
        public override SortedDictionary<double, int> GetClusteringCoefficient()
        {
            throw new NotImplementedException();
            /*log.Info("Getting clustering coefficients.");
            return container.GetClusteringCoefficient();*/
        }

        // Возвращается распределение длин минимальных путей в графе. Реализовано.
        public override SortedDictionary<int, int> GetMinPathDist()
        {
            throw new NotImplementedException();
            /*log.Info("Getting minimal distances between vertices.");
            return container.GetMinPathDistribution();*/
        }

        // Закрытая часть класса (не из общего интерфейса). //

        private double avgPath = -1;
        private int diameter = -1;
        private SortedDictionary<int, int> pathDistribution = new SortedDictionary<int, int>();

        private void CountPathDistribution()
        {
            double avgPath = 0;
            int diameter = 0, countOfWays = 0;

            for (int i = 0; i < container.Size; ++i)
            {
                for (int j = i + 1; j < container.Size; ++j)
                {
                    int way = container.MinimumWay(i, j);
                    if (way == -1)
                        continue;
                    if (pathDistribution.ContainsKey(way))
                        pathDistribution[way]++;
                    else
                        pathDistribution.Add(way, 1);

                    if (way > diameter)
                        diameter = way;

                    avgPath += way;
                    ++countOfWays;
                }
            }

            this.avgPath = avgPath / countOfWays;
            this.diameter = diameter;
        }
    }
}
