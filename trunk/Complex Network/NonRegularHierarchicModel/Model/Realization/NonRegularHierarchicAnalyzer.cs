using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandomGraph.Common.Model;
using CommonLibrary.Model;
using Model.NonRegularHierarchicModel.Realization;

namespace Model.NonRegularHierarchicModel.Realization
{
    public class NonRegularHierarchicAnalyzer : AbstarctGraphAnalyzer
    {
        private NonRegularHierarchicGraph graph;

        public NonRegularHierarchicAnalyzer(NonRegularHierarchicGraph g)
        {
            graph = g;
        }

        /// <summary>
        /// Counts degree distribution of graph.
        /// </summary>
        /// <returns></returns>
        public override SortedDictionary<int, int> GetDegreeDistribution()
        {
            SortedDictionary<int, int> result = new SortedDictionary<int, int>();

            /// Iterate over all the vertexes and count degrees.
            uint v;
            int degree;
            for (v = 0; v < graph.get_vertexes_count(); ++v)
            {
                degree = (int)(graph.get_degree(v));
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

        /// <summary>
        /// Calculates average path of graph.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Calculates clustering coefficient of graph.
        /// </summary>
        /// <returns></returns>
        public override SortedDictionary<double, int> GetClusteringCoefficient()
        {
            double result = (double)(graph.get_3_circles_count() * 3) / graph.get_2_length_chains_count();

            // TODO return upper result when fixed.
            SortedDictionary<double, int> result_bad = new SortedDictionary<double, int>();
            return result_bad;
        }

        //Calculate Eigen values of graph.
        public override ArrayList GetEigenValue()
        {
            ArrayList result = new ArrayList();
            return result;
        }

        //Calculate count of cycles in 3 lenght of graph.
        public override double GetCycles3()
        {
            double result = (double)(graph.get_3_circles_count());
            return result;
        }

        //Calculate diameter of graph.
        public override double GetDiameter()
        {
            SortedDictionary<int, int> dist = GetMinPathDist();

            double result = 0.0;

            foreach (KeyValuePair<int, int> k in dist)
            {
                if (k.Key > result)
                    result = k.Key;
            }

            return result;
        }

        //Calculate distribution of connected subgraph of graph.
        public override SortedDictionary<int, int> GetConnSubGraph()
        {
            SortedDictionary<int, int> result = new SortedDictionary<int, int>();
            return result;
        }

        /// <summary>
        /// Calculate count of cycles in 3 lenght based in eigen valu of graph. WILL NEVER BE IMPLEMENTED.
        /// </summary>
        /// <returns></returns>
        public override double GetCycleEigen3()
        {
            double result = -1.0;
            return result;
        }

        /// <summary>
        /// Calculate count of cycles in 4 lenght of graph.
        /// </summary>
        /// <returns></returns>
        public override double GetCycles4()
        {
            double result = graph.get_4_circles_count();
            return result;
        }

        /// <summary>
        /// Calculate count of cycles in 4 lenght based in eigen valu of graph. WILL NEVER BE IMPLEMENTED.
        /// </summary>
        /// <returns></returns>
        public override double GetCycleEigen4()
        {
            double result = 0;
            return result;
        }

        /// <summary>
        /// Calculate motive of graph.
        /// </summary>
        public override void GetMotif()
        {
        }

        //Calculate distribution of minimum paths of graph.
        public override SortedDictionary<int, int> GetMinPathDist()
        {
            SortedDictionary<int, int> result = graph.get_min_path_distribution();
            return result;
        }

        /// <summary>
        /// Calculate distribution of eigen value of graph. WILL NEVER BE IMPLEMENTED.
        /// </summary>
        /// <returns></returns>
        public override SortedDictionary<double, int> GetDistEigenPath()
        {
            SortedDictionary<double, int> result = new SortedDictionary<double, int>();
            return result;
        }

        /// <summary>
        /// Calculate distribution of connected subgraph of graph.  WILL NEVER BE IMPLEMENTED.
        /// </summary>
        /// <returns></returns>
        public override SortedDictionary<int, int> GetFullSubGraph()
        {
            SortedDictionary<int, int> result = new SortedDictionary<int, int>();
            return result;
        }
    }
}
