using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NumberGeneration;
using RandomGraph.Common.Model.Generation;
using CommonLibrary.Model;

namespace Model.BAModel.Realization
{
    public class BAGenerator : IGraphGenerator
    {
        private RNGCrypto rand = new RNGCrypto();

        // Контейнер, в котором содержится граф конкретной модели (ER).
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
            int numberOfVertices = (Int32)genParam[GenerationParam.Vertices];
            int edges = (Int16)genParam[GenerationParam.MaxEdges];
            int addVertices = (Int32)genParam[GenerationParam.AddVertices];

            Generate(addVertices);                
        }

        // Строится граф, на основе матрицы смежности.
        public void StaticGeneration(ArrayList matrix)
        {
            container.SetMatrix(matrix);
        }

        // Утилиты.
        private void Generate(long countAssamble)
        {
            while (countAssamble > 0)
            {
                double[] probabilyArray = container.CountProbabilities();
                container.AddVertex();
                container.RefreshNeighbourships(MakeGenerationStep(probabilyArray));
                countAssamble--;
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
