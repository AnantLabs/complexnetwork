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
    /// 
    /// </summary>
    class ERNetworkGenerator : INetworkGenerator
    {
        // Организация работы с лог файлом.
        //protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(ERGenerator));

        // Контейнер, в котором содержится граф конкретной модели (ER).
        private NonHierarchicContainer container;
     
        // Конструктор по умолчанию, в котором создается пустой контейнер графа.
        public ERNetworkGenerator()
        {
            container = new NonHierarchicContainer();
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
            Single probability = (Single)genParam[GenerationParameter.Probability];
            
            container.Size = numberOfVertices;
           
            FillValuesByProbability(probability);
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
