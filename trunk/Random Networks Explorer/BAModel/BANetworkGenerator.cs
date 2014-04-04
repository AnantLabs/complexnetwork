using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Model;
using NetworkModel;
using RandomNumberGeneration;

namespace BAModel
{
    /// <summary>
    /// Implementation of generator of random network of Baraba´si-Albert's model.
    /// </summary>
    class BANetworkGenerator : INetworkGenerator
    {
        private NonHierarchicContainer container;
        private NonHierarchicContainer initialcontainer;
        private int edges;

        public BANetworkGenerator()
        {
            container = new NonHierarchicContainer();
            initialcontainer = new NonHierarchicContainer();
        }

        public INetworkContainer Container
        {
            get { return container; }
            set { container = (NonHierarchicContainer)value; }
        }

        public void RandomGeneration(Dictionary<GenerationParameter, object> genParam)
        {
            UInt16 numberOfVertices = (UInt16)genParam[GenerationParameter.Vertices];
            edges = (Int32)genParam[GenerationParameter.Edges];
            Single probability = (Single)genParam[GenerationParameter.Probability];
            UInt16 stepCount = (UInt16)genParam[GenerationParameter.StepCount];

            container.Size = numberOfVertices;
            initialcontainer.Size = numberOfVertices;
            Generate(stepCount, probability);
        }

        public void StaticGeneration(ArrayList matrix)
        {
            container.SetMatrix(matrix);
        }

        private RNGCrypto rand = new RNGCrypto();

        private void Generate(long stepCount, double probability)
        {
            GenereateInitialGraph(probability);
            container = initialcontainer;

            while (stepCount > 0)
            {
                double[] probabilyArray = container.CountProbabilities();
                container.AddVertex();
                container.RefreshNeighbourships(MakeGenerationStep(probabilyArray));
                --stepCount;
            }
        }

        private void GenereateInitialGraph(double probability)
        {
            for(int i = 0; i < container.Size; ++i)
                for(int j = i + 1; j < container.Size; ++j)
                {
                    if (rand.NextDouble() < probability)
                        initialcontainer.AddConnection(i, j);
                }
        }

        private bool[] MakeGenerationStep(double[] probabilityArray)
        {
            Dictionary<int, double> resultDic = new Dictionary<int, double>();
            int count = 0;
            for (int i = 0; i < probabilityArray.Length; ++i)
                resultDic.Add(i, probabilityArray[i] - rand.NextDouble());
          
            var items = from pair in resultDic
                        orderby pair.Value descending
                        select pair;
            
            bool[] result = new bool[container.Neighbourship.Count];
            foreach (var item in items)
            {
                if (count < edges)
                {
                    result[item.Key] = true;
                    count++;
                }
            }

            return result;
        }
    }
}
