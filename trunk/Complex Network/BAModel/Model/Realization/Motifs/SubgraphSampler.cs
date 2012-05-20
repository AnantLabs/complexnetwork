using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motifs
{
    /// <summary>
    /// in this class are sampling methods implementation
    /// </summary>
    public static class SubgraphSampler
    {
        /// <summary>
        /// returns graph sample from super graph whith k size
        /// </summary>
        /// <param name="super"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static Graph GetRandomSubgraphESA(Graph super, int k)
        {
            if (k <= 2)
                throw new ArgumentOutOfRangeException();

            Graph sub = new Graph();
            Random rand = new Random();

            sub.AddEdge(super.Edges[rand.Next(super.Edges.Count)]);

            while (sub.Vertices.Count < k)
            {
                System.Threading.Thread.Sleep(2);
                List<Edge> nhood = Graph.GetNeighborhood(super, sub);
                if (nhood.Count != 0) 
                {
                    sub.AddEdge(nhood[rand.Next(nhood.Count)]);
                }

                
            }
            int verticeIndex = 0;
            Dictionary<int, int> edges = new Dictionary<int, int>();
            foreach(Vertice vertice in sub.Vertices) 
            {
                edges[vertice.index] = verticeIndex;
                vertice.index = verticeIndex;
                verticeIndex++;
            }
            sub.changeEdeVerticeIndex(edges);
            if (sub.Edges.Count == 1)
            {
                int edg = sub.Edges.Count;
            }
            return sub;
        }
    }
}
