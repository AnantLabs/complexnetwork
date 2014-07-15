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
            UInt16 numberOfVertices = Convert.ToUInt16(genParam[GenerationParameter.Vertices]);
            Single probability = Convert.ToSingle(genParam[GenerationParameter.Probability]);
            
            container.Size = numberOfVertices;           
            FillValuesByProbability(probability);
        }

        public void StaticGeneration(MatrixInfoToRead matrixInfo)
        {
            container.SetMatrix(matrixInfo.Matrix);
        }

        private RNGCrypto rand = new RNGCrypto();

        private void FillValuesByProbability(double p)
        {            
            for (int i = 0; i < container.Size; ++i)
            {
                for (int j = i + 1; j < container.Size; ++j)
                {
                    double a = rand.NextDouble();
                    if (a < p)
                    {
                        container.AddConnection(i, j);
                    }
                }
            }
        }
    }
}
