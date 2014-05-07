using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Model;
using Core.Enumerations;
using NetworkModel;
using RandomNumberGeneration;

namespace ERModel
{
    /// <summary>
    /// Implementation of generator of random network of Erdős-Rényi's model.
    /// </summary>
    class ERNetworkGenerator : INetworkGenerator
    {
        private NonHierarchicContainer container;
     
        public ERNetworkGenerator()
        {
            container = new NonHierarchicContainer();
        }

        public INetworkContainer Container
        {
            get { return container; }
            set { container = (NonHierarchicContainer)value; }
        }

        public void RandomGeneration(Dictionary<GenerationParameter, object> genParam)
        {
            // TODO change without parse
            //UInt16 numberOfVertices = (UInt16)genParam[GenerationParameter.Vertices];
            //Single probability = (Single)genParam[GenerationParameter.Probability];
            UInt16 numberOfVertices = UInt16.Parse(genParam[GenerationParameter.Vertices].ToString());
            Single probability = Single.Parse(genParam[GenerationParameter.Probability].ToString());
            
            container.Size = numberOfVertices;           
            FillValuesByProbability(probability);
        }

        public void StaticGeneration(ArrayList matrix)
        {
            container.SetMatrix(matrix);
        }

        private RNGCrypto r = new RNGCrypto();

        private void FillValuesByProbability(double p)
        {            
            for (int i = 0; i < container.Size; ++i)
            {
                for (int j = i + 1; j < container.Size; ++j)
                {
                    double a = r.NextDouble();
                    if (a < p)
                    {
                        container.AddConnection(i, j);
                    }
                }
            }
        }
    }
}
