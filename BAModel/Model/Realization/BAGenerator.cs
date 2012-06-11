using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumberGeneration;

namespace Model.BAModel.Realization
{
    public class BAGenerator
    {
        private  RNGCrypto rand = new RNGCrypto();
        private BAGraph m_graph;
        public BAGenerator(BAGraph graph) 
        {
            m_graph = graph;
        }

        public bool[] MakeGenerationStep(double[] probabilityArray)
        {
            
            bool[] result = new bool[probabilityArray.Length];

            for (int i = 0; i < probabilityArray.Length; ++i)
                result[i] = rand.NextDouble() <= probabilityArray[i];

            return result;
        }
        public void Generate(long countAssamble)
        {
            while (countAssamble > 0)
            {
                double[] probabilyArray = m_graph.Container.CountProbabilities();
                m_graph.Container.AddVertex();
                m_graph.Container.RefreshNeighbourships(MakeGenerationStep(probabilyArray));
                countAssamble--;
            }
        }
        public BAGraph Graph
        {
            get { return m_graph; }
        }
    }
}
