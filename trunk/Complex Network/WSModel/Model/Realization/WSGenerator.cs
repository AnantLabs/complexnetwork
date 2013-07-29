using System;
using System.Collections;
using System.Collections.Generic;

using RandomGraph.Common.Model.Generation;
using CommonLibrary.Model;
using NumberGeneration;
using log4net;

namespace Model.WSModel.Realization
{
    // Реализация генератор (WS).
    public class WSGenerator : AbstractGraphGenerator
    {
        // Организация работы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(WSGenerator));

        // Контейнер, в котором содержится граф конкретной модели (WS).
        private WSContainer container;

        // Конструктор по умолчанию, в котором создается пустой контейнер графа.
        public WSGenerator()
        {
            container = new WSContainer();
        }

        // Контейнер, в котором содержится сгенерированный граф.
        public override AbstractGraphContainer Container
        {
            get { return container; }
            set { container = (WSContainer)value; }
        }

        // Permanet Generation
        public override void PermanentGeneration(Dictionary<GenerationParam, object> genParam)
        {
            throw new NotImplementedException();
        }

        // Случайным образом генерируется граф, на основе параметров генерации.
        protected override void RandomGeneration(Dictionary<GenerationParam, object> genParam)
        {
            log.Info("Random generation step started.");
            int numberOfVertices = (Int32)genParam[GenerationParam.Vertices];
            int numberOfEdges = (Int32)genParam[GenerationParam.Edges];
            double probability = (Double)genParam[GenerationParam.P];
            int stepCount = (Int32)genParam[GenerationParam.StepCount];

            container.SetParameters(numberOfVertices, numberOfEdges / 2);
            Randomize();
            FillValuesByProbability(probability, stepCount);
            log.Info("Random generation step finished.");
        }

        // Строится граф, на основе матрицы смежности.
        protected override void StaticGeneration(string fileName)
        {
            log.Info("Static generation started.");
            container.SetMatrix(fileName);
            log.Info("Static generation finished.");
        }

        // Закрытая часть класса (не из общего интерфейса).

        private int currentId = 0;
        private List<int> collectRandoms = new List<int>();

        private void Randomize()
        {
            Random rand = new Random();
            collectRandoms.Clear();

            for (int i = 0; i < container.Size; ++i)
            {
                double rand_number = rand.Next(0, container.Size);
                collectRandoms.Add((int)rand_number);
            }
        }

        private void FillValuesByProbability(double probability, int stepCount)
        {
            while (stepCount > 0)
            {
                for (int i = 1; i < container.Size; ++i)
                {
                    List<int> neighbours = new List<int>();
                    List<int> nonNeighbours = new List<int>();
                    for (int k = 0; k < container.Size && k < i; ++k)
                    {
                        if (container.AreNeighbours(i, k))
                            neighbours.Add(k);
                        else
                            nonNeighbours.Add(k);
                    }

                    if (nonNeighbours.Count > 0)
                    {
                        int size_neighbours = neighbours.Count;
                        for (int j = 0; j < size_neighbours; ++j)
                        {
                            int r = WSStep(probability, nonNeighbours, neighbours[j]);
                            if (r != neighbours[j])
                            {
                                container.Disconnect(i, neighbours[j]);
                                container.Connect(i, r);
                            }
                        }
                    }
                }
                --stepCount;
            }
        }

        public int WSStep(double probability, List<int> indexes, int index)
        {
            // select a number from indices with m_prob probability 
            // or return index with 1 - m_prob probability

            if (probability * container.Size > collectRandoms[currentId])
            {
                int cycleCount = 0;
                while (collectRandoms[currentId] > indexes.Count - 1)
                {
                    cycleCount++;
                    if (currentId == collectRandoms.Count - 1)
                        currentId = 0;
                    else
                        ++currentId;
                    if (cycleCount > container.Size)
                        return index;
                }

                int id = indexes[collectRandoms[currentId]];
                if (currentId == collectRandoms.Count - 1)
                    currentId = 0;
                else
                    ++currentId;
                return id;
            }
            if (currentId == collectRandoms.Count - 1)
                currentId = 0;
            else
                ++currentId;
            return index;
        }
    }
}
