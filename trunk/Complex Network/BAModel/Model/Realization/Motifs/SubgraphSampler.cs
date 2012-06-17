using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motifs
{
    /// <summary>
    /// in this class are sampling methods implementation
    /// </summary>
    public  class SubgraphSampler
    {
        /// <summary>
        /// returns graph sample from super graph whith k size
        /// </summary>
        /// <param name="super"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public Graph GetRandomSubgraphESA(Graph super, int k)
        {
            if (k <= 2)
                throw new ArgumentOutOfRangeException();

            Graph sub = new Graph();
            Random rand = new Random();

            sub.AddEdge(super.Edges[rand.Next(super.Edges.Count)]);
            int nonNeighborCount = 0;
            while (sub.Vertices.Count < k)
            {
                System.Threading.Thread.Sleep(2);
                List<Edge> nhood = Graph.GetEdgesNeighborhood(super, sub);
                if (nhood.Count != 0)
                {
                    nonNeighborCount = 0;
                    sub.AddEdge(nhood[rand.Next(nhood.Count)]);
                }
                else
                {
                    nonNeighborCount++;
                    if (nonNeighborCount == 5)
                        return null;
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

        /// <summary>
        /// returns graph sample from super graph whith k size
        /// </summary>
        /// <param name="super"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public Graph GetRandomSubgraphESU(Graph super, int k)
        {
            if (k <= 2)
                throw new ArgumentOutOfRangeException();

            Graph sub = new Graph();
            Random rand = new Random();
            int nonNeighborCount = 0;
            sub.AddVertice(super.Vertices[rand.Next(super.Vertices.Count)]);
            while (sub.Vertices.Count < k)
            {
                Vertice vert = sub.Vertices[rand.Next(sub.Vertices.Count)];
                System.Threading.Thread.Sleep(2);
                List<Vertice> nhood = Graph.GetVerticesNeighborhood(super, sub, vert);
                if (nhood.Count != 0)
                {
                    Vertice vertice = nhood[rand.Next(nhood.Count)];
           //         if (vert.index < vertice.index)
           //         {
                        nonNeighborCount = 0;
                        Edge edge = new Edge(vertice.index, vert.index);
                        sub.AddVertice(vertice);
                        sub.AddEdge(edge);
          //          }
          //          else
          //          {
          //              nonNeighborCount++;
          //              if (nonNeighborCount == 5)
         //                   return null;
         //           }
                    
                }
                else
                {
                    nonNeighborCount++;
                    if (nonNeighborCount == 5)
                        return null;
                }


            }
            int verticeIndex = 0;
            Dictionary<int, int> edges = new Dictionary<int, int>();
            foreach (Vertice vertice in sub.Vertices)
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
