using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Model;
using NetworkModel;
using RandomNumberGeneration;

namespace WSModel
{
    /// <summary>
    /// 
    /// </summary>
    class WSNetworkGenerator : INetworkGenerator
    {
        // Организация работы с лог файлом.
        //protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(WSGenerator));

        // Контейнер, в котором содержится граф конкретной модели (WS).
        private NonHierarchicContainer container;

        // Конструктор по умолчанию, в котором создается пустой контейнер графа.
        public WSNetworkGenerator()
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
            Int32 numberOfEdges = (Int32)genParam[GenerationParameter.Edges];
            Single probability = (Single)genParam[GenerationParameter.Probability];
            UInt16 stepCount = (UInt16)genParam[GenerationParameter.StepCount];

            //container.SetParameters(numberOfVertices, numberOfEdges / 2);
            Randomize();
            FillValuesByProbability(probability, stepCount);
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
                                //container.Disconnect(i, neighbours[j]);
                                container.AddEdge(i, r);
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
