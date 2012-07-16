using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandomGraph.Common.Model;
using CommonLibrary.Model;

namespace Model.NonRegularHierarchicModel.Realization
{
    public class NonRegularHierarchicAnalyzer : AbstarctGraphAnalyzer
    {
        // !Организовать логирование!

        IGraphContainer container;  // пересмотреть!

        public NonRegularHierarchicAnalyzer(NonRegularHierarchicGraph g)
        {
            graph = g;
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
            SortedDictionary<int, int> dist = GetMinPathDist();

            double result = 0.0;
            double count = 0.0;

            foreach (KeyValuePair<int, int> k in dist)
            {
                count += k.Value;
                result += k.Key * k.Value;
            }

            result /= count;

            return result;
        }

        // Возвращается диаметр графа. Реализовано.
        public override int GetDiameter()
        {
            SortedDictionary<int, int> dist = GetMinPathDist();

            int result = 0;

            foreach (KeyValuePair<int, int> k in dist)
            {
                if (k.Key > result)
                    result = k.Key;
            }

            return result;
        }

        // Возвращается число циклов длиной 3 в графе. Реализовано.
        public override int GetCycles3()
        {
            return (int)(graph.Get3CirclesCount());
        }

        // Возвращается число циклов длиной 4 в графе. Реализовано.
        public override int GetCycles4()
        {
            return (int)graph.Get4CirclesCount();
        }

        // Возвращается степенное распределение графа. Реализовано.
        public override SortedDictionary<int, int> GetDegreeDistribution()
        {
            SortedDictionary<int, int> result = new SortedDictionary<int, int>();

            /// Iterate over all the vertexes and count degrees.
            uint v;
            int degree;
            for (v = 0; v < graph.node.VertexCount; ++v)
            {
                degree = (int)(graph.GetDegree(v));
                if (!result.ContainsKey(degree))
                {
                    result.Add(degree, 1);
                }
                else
                {
                    ++result[degree];
                }
            }
            return result;
        }

        // Возвращается распределение коэффициентов кластеризации графа. Реализовано.
        public override SortedDictionary<double, int> GetClusteringCoefficient()
        {
            return graph.GetClusteringCoefficient();
        }

        // Возвращается распределение длин минимальных путей в графе. Реализовано.
        public override SortedDictionary<int, int> GetMinPathDist()
        {
            return graph.GetMinPathDistribution();
        }


        // Закрытая часть класса (не из общего интерфейса). //
        private NonRegularHierarchicGraph graph;
    }
}
