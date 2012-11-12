using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RandomGraph.Common.Model.Generation;
using CommonLibrary.Model;
using NumberGeneration;
using log4net;

namespace Model.BAModel.Realization
{
    // Реализация генератор (BA).
    public class BAGenerator : IGraphGenerator
    {
        // Организация работы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(BAGenerator));
        
        // Контейнер, в котором содержится граф конкретной модели (BA).
        private BAContainer container;
        private BAContainer initialcontainer;
        private int edges;
        private string initialGragh;
        private bool initialGeneration=true;
        // Конструктор по умолчанию, в котором создается пустой контейнер графа.
        public BAGenerator()
        {
            container = new BAContainer();
            initialcontainer = new BAContainer();
        }

        // Контейнер, в котором содержится сгенерированный граф.
        public IGraphContainer Container
        {
            get { return container; }
            set { container = (BAContainer)value; }
        }

        // Случайным образом генерируется граф, на основе параметров генерации.
        public void RandomGeneration(Dictionary<GenerationParam, object> genParam)
        {
            log.Info("Random generation step started.");
            int numberOfVertices = (Int32)genParam[GenerationParam.Vertices];
            edges = (Int16)genParam[GenerationParam.MaxEdges];
            int stepCount = (Int32)genParam[GenerationParam.StepCount];
            double probability = (double)genParam[GenerationParam.InitialProbability];
            initialGragh = (string)genParam[GenerationParam.InitialStep];
            container.Size = numberOfVertices;
            Generate(stepCount,probability);
            log.Info("Random generation step finished.");
        }

        // Строится граф, на основе матрицы смежности.
        public void StaticGeneration(ArrayList matrix)
        {
            log.Info("Static generation started.");
            container.SetMatrix(matrix);
            log.Info("Static generation finished.");
        }

        // Закрытая часть класса (не из общего интерфейса).

        // Генератор случайного числа.
        private RNGCrypto rand = new RNGCrypto();

        private void Generate(long stepCount,double probability)
        {
            if (initialGeneration)
            {
                GenereateInitialGraph(probability);
                if (initialGragh.Equals("permanent"))
                    initialGeneration = false;
                container = initialcontainer;
            }
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
                        initialcontainer.ConnectVertex(i, j);

                }
        }

        private bool[] MakeGenerationStep(double[] probabilityArray)
        {
            Dictionary<int,double> resultDic = new Dictionary<int,double>();
            int count = 0;
            for (int i = 0; i < probabilityArray.Length; ++i)
                resultDic.Add(i, probabilityArray[i] - rand.NextDouble());

           
             var  items = from pair in resultDic
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
