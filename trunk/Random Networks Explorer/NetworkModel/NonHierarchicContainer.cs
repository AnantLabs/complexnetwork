using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Model;

namespace NetworkModel
{
    /// <summary>
    /// 
    /// </summary>
    public class NonHierarchicContainer : INetworkContainer
    {
        private int size = 0;
        private SortedDictionary<int, List<int>> neighbourship;
        private List<int> degrees;

        public NonHierarchicContainer()
        {
            neighbourship = new SortedDictionary<int, List<int>>();
            degrees = new List<int>();
        }

        public int Size
        {
            get { return size; }
            set
            {
                neighbourship.Clear();
                for (int i = 0; i < size; ++i)
                {
                    neighbourship[i] = new List<int>();
                }

                degrees.Clear();
                for (int i = 0; i < size; ++i)
                {
                    degrees.Add(0);
                }

                size = value;
            }
        }

        public SortedDictionary<int, List<int>> Neighbourship
        {
            get { return neighbourship; }
        }

        public void SetMatrix(ArrayList matrix)
        {
            size = matrix.Count;
            neighbourship = new SortedDictionary<int, List<int>>();
            ArrayList neighbourshipOfVertex = new ArrayList();
            for (int i = 0; i < matrix.Count; i++)
            {
                neighbourshipOfVertex = (ArrayList)matrix[i];
                SetDataToDictionary(i, neighbourshipOfVertex);
            }
        }

        public bool[,] GetMatrix()
        {
            bool[,] matrix = new bool[neighbourship.Count, neighbourship.Count];

            for (int i = 0; i < neighbourship.Count; i++)
                for (int j = 0; j < neighbourship.Count; j++)
                    matrix[i, j] = false;

            List<int> list = new List<int>();

            for (int i = 0; i < neighbourship.Count; i++)
            {
                list = neighbourship[i];
                for (int j = 0; j < list.Count; j++)
                    matrix[i, list[j]] = true;
            }

            return matrix;
        }

        // Методы не из общего интерфейса.

        // Добавление вершины (не имеющий соседей).
        public void AddVertex()
        {
            neighbourship.Add(size, new List<int>());
            ++size;
            degrees.Add(0);
        }

        // Возвращает число соседей данной вершины.
        public int CountVertexDegree(int i)
        {
            return neighbourship[i].Count;
        }

        // Проверяет являются ли данные вершины соседями (true - если да).
        public bool AreNeighbours(int i, int j)
        {
            return neighbourship[i].Contains(j);
        }

        // Возвращает массив вероятностей для данного состояния графа.
        public double[] CountProbabilities()
        {
            double[] result = new double[this.size];

            double graphDegree = (double)CountGraphDegree();
            if (graphDegree != 0)
            {
                for (int i = 0; i < result.Length; ++i)
                    result[i] = (double)CountVertexDegree(i) / graphDegree;
            }
            else
            {
                for (int i = 0; i < result.Length; ++i)
                    result[i] = (double)1 / result.Length;
            }

            return result;
        }

        // Обновляет сявзи в графе по сгенерированному вектору.
        public void RefreshNeighbourships(bool[] generatedVector)
        {
            int newVertexDegree = 0, iVertexDegree = 0;

            for (int i = 0; i < generatedVector.Length; ++i)
            {
                if (generatedVector[i])
                {
                    ++newVertexDegree;
                    AddEdge(i, size - 1);
                    iVertexDegree = CountVertexDegree(i);
                    --degrees[iVertexDegree];
                    ++degrees[iVertexDegree + 1];
                }
            }

            ++degrees[newVertexDegree];
        }

        /// <summary>
        /// Adds an edge connecting given vertices.
        /// </summary>
        /// <param name="i">First vertex number.</param>
        /// <param name="j">Second vertex number.</param>
        public void AddEdge(int i, int j)
        {
            int ivertexdegree = CountVertexDegree(i);
            int jvertexdegree = CountVertexDegree(j);

            neighbourship[i].Add(j);
            neighbourship[j].Add(i);

            --degrees[ivertexdegree];
            --degrees[jvertexdegree];
            ++degrees[ivertexdegree + 1];
            ++degrees[jvertexdegree + 1];
        }

        /// <summary>
        /// Removes the edge, which connects given vertices.
        /// </summary>
        /// <param name="i">First vertex number.</param>
        /// <param name="j">Second vertex number.</param>
        public void RemoveEdge(int i, int j)
        {
            neighbourship[i].Remove(j);
            neighbourship[j].Remove(i);

            int iVertexDegree = CountVertexDegree(i);
            int jVertexDegree = CountVertexDegree(j);
            --degrees[iVertexDegree];
            --degrees[jVertexDegree];
            ++degrees[iVertexDegree - 1];
            ++degrees[jVertexDegree - 1];
        }


        // Utilities

        private void SetDataToDictionary(int index, ArrayList neighbourshipOfIVertex)
        {
            neighbourship[index] = new List<int>();
            for (int j = 0; j < size; j++)
                if ((bool)neighbourshipOfIVertex[j] == true && index != j)
                    neighbourship[index].Add(j);
        }

        // Возвращает суммарное число степеней в графе.
        private int CountGraphDegree()
        {
            int sum = 0;
            for (int i = 0; i < size; ++i)
                sum += CountVertexDegree(i);

            return sum;
        }
    }
}
