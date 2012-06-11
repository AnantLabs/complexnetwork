using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RandomGraph.Common.Model;
//using Model.BAModel.Result;

namespace Model.WSModel.Realization
{
    public class WSGraph
    {

        // Initialization members //
       // private int m_size;             // number of initial vertices
      //  private int m_numberOfEdges;    // number of edges, number given by user
      //  private double m_probability;

        // Implementation memebers //
        private WSContainer m_container;
        private WSGenerator m_generator;

        public WSContainer Container
        {
            get { return m_container; }
        }

        public WSGraph(int size, int param, double prob)
        {
            m_container = new WSContainer(size, param / 2);
            m_generator = new WSGenerator(prob, size);
        }

        public WSGraph(ArrayList martix)
        {
            m_container = new WSContainer(martix);
        }

        public void Generate()
        {
            int size = m_container.Size;
            m_generator.Randomize();

            for (int i = 1; i < size; ++i)
            {
                List<int> neighbours = new List<int>();
                List<int> nonNeighbours = new List<int>();
                for (int k = 0; k < size && k < i; ++k)
                {
                    if (m_container.AreNeighbours(i, k))
                        neighbours.Add(k);
                    else
                        nonNeighbours.Add(k);
                }

                if (nonNeighbours.Count > 0)
                {
                    int size_neighbours = neighbours.Count;
                    for (int j = 0; j < size_neighbours; ++j)
                    {
                        int r = m_generator.WSStep(nonNeighbours, neighbours[j]);
                        if (r != neighbours[j])
                        {
                            m_container.Disconnect(i, neighbours[j]);
                            m_container.Connect(i, r);
                        }
                    }
                }
            }
        }

    }
}