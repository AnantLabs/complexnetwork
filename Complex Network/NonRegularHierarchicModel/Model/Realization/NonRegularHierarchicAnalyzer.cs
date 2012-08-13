﻿using System;
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
        public override IGraphContainer Container
        {
            get { return container; }
            set { container = (NonRegularHierarchicContainer)value; }
        }

        // Возвращается средняя длина пути в графе. Реализовано.
        public override double GetAveragePath()
        {
            log.Info("Getting average path length.");

            SortedDictionary<int, int> dist = GetMinPathDist();
            double result = 0.0, count = 0.0;

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
            log.Info("Getting diameter.");

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
            log.Info("Getting count of cycles - order 3.");
            return (int)(container.Get3CirclesCount());
        }

        // Возвращается число циклов длиной 4 в графе. Реализовано.
        public override int GetCycles4()
        {
            log.Info("Getting count of cycles - order 4.");
            return (int)container.Get4CirclesCount();
        }

        // Возвращается степенное распределение графа. Реализовано.
        public override SortedDictionary<int, int> GetDegreeDistribution()
        {
            log.Info("Getting degree distribution.");

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
            return result;
        }

        // Возвращается распределение коэффициентов кластеризации графа. Реализовано.
        public override SortedDictionary<double, int> GetClusteringCoefficient()
        {
            log.Info("Getting clustering coefficients.");
            return container.GetClusteringCoefficient();
        }

        // Возвращается распределение длин минимальных путей в графе. Реализовано.
        public override SortedDictionary<int, int> GetMinPathDist()
        {
            log.Info("Getting minimal distances between vertices.");
            return container.GetMinPathDistribution();
        }
    }
}
