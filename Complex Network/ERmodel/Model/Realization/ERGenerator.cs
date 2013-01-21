using System;
using System.Collections;
using System.Collections.Generic;

using RandomGraph.Common.Model.Generation;
using CommonLibrary.Model;
using NumberGeneration;
using log4net;

namespace Model.ERModel.Realization
{
    // Реализация генератор (ER).
    public class ERGenerator : IGraphGenerator
    {
        // Организация работы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(ERGenerator));

        // Контейнер, в котором содержится граф конкретной модели (ER).
        private ERContainer container;

        private ERAnalyzer analyzer;

        // Конструктор по умолчанию, в котором создается пустой контейнер графа.
        public ERGenerator()
        {
            container = new ERContainer();
        }

        // Контейнер, в котором содержится сгенерированный граф.
        public IGraphContainer Container
        {
            get { return container; }
            set { container = (ERContainer)value; }
        }

        // Случайным образом генерируется граф, на основе параметров генерации.
        public void RandomGeneration(Dictionary<GenerationParam, object> genParam)
        {
            log.Info("Random generation step started.");
            ERAnalyzer.ansmble = new SortedDictionary<int, SortedDictionary<int, long>>();
            int numberOfVertices = (Int32)genParam[GenerationParam.Vertices];
            double probability = (Double)genParam[GenerationParam.P];
            int constant = (int)genParam[GenerationParam.Constant];
            int stepCount = (int)genParam[GenerationParam.StepCount];
            container.Size = numberOfVertices;
            int ansambleCount = 0;
            FillValuesByProbability(probability);
            var currentContainer = container;
            ERAnalyzer.GetTrianglesTraectory(container, constant, ansambleCount);
            ansambleCount++;
            if (stepCount != 0)
            {
                while (stepCount != ansambleCount)
                {
                    if ((string)genParam[GenerationParam.InitialStep] != "permament")
                        FillValuesByProbability(probability);

                    ERAnalyzer.GetTrianglesTraectory(container, constant, ansambleCount);
                    ansambleCount++;
                }
            }
            container = currentContainer;
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
        private RNGCrypto r = new RNGCrypto();

        // Добовляет ребра в граф (контейнер) по данной вероятности.
        private void FillValuesByProbability(double p)
        {
            for (int i = 0; i < container.Size; ++i)
            {
                for (int j = i + 1; j < container.Size; ++j)
                {
                    double a = r.NextDouble();
                    if (a < p)
                    {
                        container.AddEdge(i, j);
                    }
                }
            }
        }
    }
}
