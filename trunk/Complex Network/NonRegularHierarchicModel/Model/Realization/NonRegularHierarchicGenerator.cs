using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using NumberGeneration;

namespace Model.NonRegularHierarchicModel.Realization
{
    public class NonRegularHierarchicGenerator
    {
        private NonRegularHierarchicGraph graph = new NonRegularHierarchicGraph();

        /// K.Martun TODO change to RGNCRYPTO.
        //RNGCrypto rnd2 = new RNGCrypto();
        Random rnd2 = new Random();

        /// <summary>
        /// Generates graph with given parameters.
        /// </summary>
        /// <param name="p"> Maximal number of blocks in a single level of graph.</param>
        /// <param name="max_level"> Number of levels in graph.</param>
        /// <param name="Mu"> Double value which determines the connectivity of graph. 1 will create a full connected graph.</param>
        public NonRegularHierarchicGenerator(Int16 p, Int16 max_level, Double Mu)
        {
            /// If this is to be a leave, just return.
            if (0 == max_level)
            {
                graph.Post_generate();
                return;
            }

            graph.node.children = new NonRegularHierarchicGraph[rnd2.Next(2, p + 1)];
            int i;
            for (i = 0; i < graph.node.children.Length; ++i)
            {
                graph.node.children[i] = new NonRegularHierarchicGraph();

                /// generate further tree of graph.
                NonRegularHierarchicGenerator sub_generator = new NonRegularHierarchicGenerator(p, (Int16)(max_level - 1), Mu);
                graph.node.children[i] = sub_generator.Graph;
            }

            int length = (graph.node.children.Length - 1) * graph.node.children.Length / 2;
            graph.node.data = new BitArray(length, false);
            for (i = 0; i < length; ++i)
            {
                double k = rnd2.NextDouble();
                if (k <= (Math.Pow(Mu, Math.Pow(p, max_level - 1))))
                {
                    graph.node.data[i] = true;
                }
                else
                {
                    graph.node.data[i] = false;
                }
            }

            graph.Post_generate();
        }

        public NonRegularHierarchicGraph Graph
        {
            get { return graph; }
        }
    }
}
