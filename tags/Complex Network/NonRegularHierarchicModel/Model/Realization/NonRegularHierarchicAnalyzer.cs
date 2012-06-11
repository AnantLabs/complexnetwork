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
            return graph.GetClusteringCoefficient();
        }

        //Calculate Eigen values of graph.
        // Not implemented
        public override ArrayList GetEigenValue()
        {
            ArrayList result = new ArrayList();
            return result;
        }

        //Calculate count of cycles in 3 lenght of graph.
        public override int GetCycles3()
        {
            return (int)(graph.Get3CirclesCount());
        }

        //Calculate diameter of graph.
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

        //Calculate distribution of connected subgraph of graph.
        // Not implemented
        public override SortedDictionary<int, int> GetConnSubGraph()
        {
            SortedDictionary<int, int> result = new SortedDictionary<int, int>();
            return result;
        }

        /// <summary>
        /// Calculate count of cycles in 3 lenght based in eigen valu of graph. WILL NEVER BE IMPLEMENTED.
        /// </summary>
        /// <returns></returns>
        public override int GetCycleEigen3()
        {
            return -1;
        }

        /// <summary>
        /// Calculate count of cycles in 4 lenght of graph.
        /// </summary>
        /// <returns></returns>
        public override int GetCycles4()
        {
            return (int)graph.Get4CirclesCount();
        }

        /// <summary>
        /// Calculate count of cycles in 4 lenght based in eigen valu of graph. WILL NEVER BE IMPLEMENTED.
        /// </summary>
        /// <returns></returns>
        public override int GetCycleEigen4()
        {
            return 0;
        }


        //Calculate distribution of minimum paths of graph.
        public override SortedDictionary<int, int> GetMinPathDist()
        {
            return graph.GetMinPathDistribution();
        }

        /// <summary>
        /// Calculate distribution of eigen value of graph. WILL NEVER BE IMPLEMENTED.
        /// </summary>
        /// <returns></returns>
        // Not implemented
        public override SortedDictionary<double, int> GetDistEigenPath()
        {
            SortedDictionary<double, int> result = new SortedDictionary<double, int>();
            return result;
        }

        /// <summary>
        /// Calculate distribution of connected subgraph of graph.  WILL NEVER BE IMPLEMENTED.
        /// </summary>
        /// <returns></returns>
        // Not implemented
        public override SortedDictionary<int, int> GetFullSubGraph()
        {
            SortedDictionary<int, int> result = new SortedDictionary<int, int>();
            return result;
        }
    }
}
