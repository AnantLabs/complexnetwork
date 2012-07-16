using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NumberGeneration;
using RandomGraph.Common.Model.Generation;
using CommonLibrary.Model;

namespace Model.WSModel.Realization
{
    public class WSGenerator : IGraphGenerator
    {
        // Контейнер, в котором содержится граф конкретной модели (ER).
        private WSContainer container;

        // Конструктор по умолчанию, в котором создается пустой контейнер графа.
        public WSGenerator()
        {
            container = new WSContainer();
        }

        // Контейнер, в котором содержится сгенерированный граф.
        public IGraphContainer Container
        {
            get { return container; }
            set { container = (WSContainer)value; }
        }

        // Случайным образом генерируется граф, на основе параметров генерации.
        public void RandomGeneration(Dictionary<GenerationParam, object> genParam)
        {
            /*m_container = new WSContainer(size, param / 2);
            m_generator = new WSGenerator(prob, size);*/

            int size = container.Size;
            Randomize();

            for (int i = 1; i < size; ++i)
            {
                List<int> neighbours = new List<int>();
                List<int> nonNeighbours = new List<int>();
                for (int k = 0; k < size && k < i; ++k)
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
                        int r = WSStep(nonNeighbours, neighbours[j]);
                        if (r != neighbours[j])
                        {
                            container.Disconnect(i, neighbours[j]);
                            container.Connect(i, r);
                        }
                    }
                }
            }
        }

        // Строится граф, на основе матрицы смежности.
        public void StaticGeneration(ArrayList matrix)
        {
            container.SetMatrix(matrix);
        }


        // Утилиты

        private int currentId = 0;
        private int size;
        private double probability;
        private List<int> collectRandoms;

        /*public WSGenerator(double prob, int size)
        {
            m_probability = prob;
            m_size = size;
            m_collectRandoms = new List<int>(m_size);
        }*/

        public void Randomize()
        {
            Random rand = new Random();
            collectRandoms.Clear();

            for (int i = 0; i < size; ++i)
            {
                double rand_number = rand.Next(0, size);
                collectRandoms.Add((int)rand_number);
            }
        }

        public int WSStep(List<int> indexes, int index)
        {
            // select a number from indices with m_prob probability 
            // or return index with 1 - m_prob probability

            if (probability * size > collectRandoms[currentId])
            {
                int cycleCount = 0;
                while (collectRandoms[currentId] > indexes.Count - 1)
                {
                    cycleCount++;
                    if (currentId == collectRandoms.Count - 1)
                        currentId = 0;
                    else
                        ++currentId;
                    if (cycleCount > size)
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
