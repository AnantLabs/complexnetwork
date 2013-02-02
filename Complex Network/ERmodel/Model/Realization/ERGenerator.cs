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

        //For Thread seaf
        private static object syncLock = new object();

        // Контейнер, в котором содержится граф конкретной модели (ER).
        private ERContainer container;

        //static container for permanent geretation
        private static ERContainer permanentContainer;

     
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

        //Permanet Generation
        public void PermanentGeneration(Dictionary<GenerationParam, object> genParam)
        {
            Console.WriteLine(ERModel.permanentStatus);
            if (ERModel.permanentStatus)
            {
                lock (syncLock)
                {
                    double probability = (Double)genParam[GenerationParam.P];
                    int numberOfVertices = (Int32)genParam[GenerationParam.Vertices];
                    container.Size = numberOfVertices;
                    if (ERModel.permanentStatus)
                    {
                        FillValuesByProbability(probability);
                        permanentContainer = container;
                        ERModel.permanentStatus = false;
                    }
                    else
                    {
                        container = permanentContainer;
                    }
                    permanentContainer = container;
                }
            }
            else
            {
                container = permanentContainer;
            }


        }

        // Случайным образом генерируется граф, на основе параметров генерации.
        public void RandomGeneration(Dictionary<GenerationParam, object> genParam)
        {
            log.Info("Random generation step started.");
  
            int numberOfVertices = (Int32)genParam[GenerationParam.Vertices];
            double probability = (Double)genParam[GenerationParam.P];
            
            container.Size = numberOfVertices;
           
            FillValuesByProbability(probability);
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
