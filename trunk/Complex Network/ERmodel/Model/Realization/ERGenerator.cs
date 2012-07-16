using System;
using System.Collections;
using System.Collections.Generic;
using log4net;

using RandomGraph.Common.Model.Generation;
using CommonLibrary.Model;

namespace Model.ERModel.Realization
{
    public class ERGenerator : IGraphGenerator
    {
        // Контейнер, в котором содержится граф конкретной модели (ER).
        private ERContainer container;

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
            int numberOfVertices = (Int32)genParam[GenerationParam.Vertices];
            double probability = (Double)genParam[GenerationParam.P];

            container.Size = numberOfVertices;
            //container.Fill(probability);
        }

        // Строится граф, на основе матрицы смежности.
        public void StaticGeneration(ArrayList matrix)
        {
            container.SetMatrix(matrix);
        }
    }
}
