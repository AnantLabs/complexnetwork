using System;
using System.Collections;
using System.Collections.Generic;

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

        // Конструктор по умолчанию, в котором создается пустой контейнер графа.
        public BAGenerator()
        {
            container = new BAContainer();
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
            int edges = (Int16)genParam[GenerationParam.MaxEdges];
            int stepCount = (Int32)genParam[GenerationParam.StepCount];

            container.Size = numberOfVertices;
            Generate(stepCount);
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

        private void Generate(long stepCount)
        {
            while (stepCount > 0)
            {
                double[] probabilyArray = container.CountProbabilities();
                container.AddVertex();
                container.RefreshNeighbourships(MakeGenerationStep(probabilyArray));
                --stepCount;
            }
        }

        private bool[] MakeGenerationStep(double[] probabilityArray)
        {
            bool[] result = new bool[probabilityArray.Length];

            for (int i = 0; i < probabilityArray.Length; ++i)
                result[i] = rand.NextDouble() <= probabilityArray[i];

            return result;
        }
    }
}
