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
    public class BANetworkGenerator : INetworkGenerator
    {
        // Организация работы с лог файлом.
        //protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(BAGenerator));
        
        // Контейнер, в котором содержится граф конкретной модели (BA).
        private NonHierarchicContainer container;
        private NonHierarchicContainer initialcontainer;
        private int edges;

        // Конструктор по умолчанию, в котором создается пустой контейнер графа.
        public BANetworkGenerator()
        {
            container = new NonHierarchicContainer();
            initialcontainer = new NonHierarchicContainer();
        }

        // Контейнер, в котором содержится сгенерированный граф.
        public INetworkContainer Container
        {
            get { return container; }
            set { container = (NonHierarchicContainer)value; }
        }

        // Случайным образом генерируется граф, на основе параметров генерации.
        public void RandomGeneration(Dictionary<GenerationParameter, object> genParam)
        {
            //log.Info("Random generation step started.");
            Int16 numberOfVertices = (Int16)genParam[GenerationParameter.Vertices];
            edges = (Int32)genParam[GenerationParameter.Edges];
            Single probability = (Single)genParam[GenerationParameter.Probability];
            Int16 stepCount = (Int16)genParam[GenerationParameter.StepCount];

            container.Size = numberOfVertices;
            initialcontainer.Size = numberOfVertices;
            Generate(stepCount, probability);
            //log.Info("Random generation step finished.");
        }

        // Строится граф, на основе матрицы смежности.
        public void StaticGeneration(ArrayList matrix)
        {
            //log.Info("Static generation started.");
            container.SetMatrix(matrix);
            //log.Info("Static generation finished.");
        }

        // Закрытая часть класса (не из общего интерфейса).

        // Генератор случайного числа.
        private RNGCrypto rand = new RNGCrypto();

        private void Generate(long stepCount,double probability)
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
            for(int i = 0;i<container.Size;i++)
                for(int j = i+1;j<container.Size;j++)
                {
                    if (rand.NextDouble() < probability)
                        initialcontainer.AddEdge(i, j);

                }
        }

        private bool[] MakeGenerationStep(double[] probabilityArray)
        {
            Dictionary<int,double> resultDic = new Dictionary<int,double>();
            int count = 0;
            for (int i = 0; i < probabilityArray.Length; ++i)
                resultDic.Add(i, probabilityArray[i] - rand.NextDouble());
          
            var items = from pair in resultDic
                        orderby pair.Value descending
                        select pair;
            
            bool[] result = new bool[container.Neighbourship.Count];
            foreach (var item in items)
                if (count < edges)
                {
                    result[item.Key] = true;
                    count++;
                }

            return result;
        }
    }
}
