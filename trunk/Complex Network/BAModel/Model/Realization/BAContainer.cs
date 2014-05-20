using System;
using System.Collections;
using System.Collections.Generic;

using CommonLibrary.Model;
using GenericAlgorithms;
using log4net;

namespace Model.BAModel.Realization
{
    // Реализация контейнера (BA).
    public class BAContainer : AbstractGraphContainer
    {
        // Организация pаботы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(BAContainer));        

        // Число вершин графа.
        private int size = 0;
        // Списки соседей для вершин графа.
        private SortedDictionary<int, List<int>> neighbourship;
        // Список степеней вершин графа.
        private List<int> degrees;

        // Конструктор по умолчанию для контейнера.
        public BAContainer()
        {
            log.Info("Creating BAContainer default object.");
            neighbourship = new SortedDictionary<int, List<int>>();
            degrees = new List<int>();
        }

        // Размер контейнера (число вершин в графе).
        public override int Size
        {
            get { return size; }
            set
            {
                log.Info("Creating BAContainer object with given size.");
                size = value;
                for (int i = 0; i < size; ++i)
                {
                    neighbourship[i] = new List<int>();
                }

                degrees.Add(size);
                for (int i = 1; i < size; ++i)
                {
                    degrees.Add(0);
                }
            }
        }

        // Списки соседей для вершин графа.
        public SortedDictionary<int, List<int>> Neighbourship
        {
            get { return neighbourship; }
        }

        // Строится граф на основе матрицы смежности.
        public override void SetMatrix(string fileName)
        {
            ArrayList matrix = MatrixFileReader.MatrixReader(fileName);

            log.Info("Creating BAContainer object from given matrix.");
            size = matrix.Count;
            neighbourship = new SortedDictionary<int, List<int>>();
            ArrayList neighbourshipOfIVertex = new ArrayList();
            for (int i = 0; i < matrix.Count; i++)
            {
                neighbourshipOfIVertex = (ArrayList)matrix[i];
                SetDataToDictionary(i, neighbourshipOfIVertex);
            }
        }

        // Возвращается матрица смежности, соответсвующая графу.
        public override bool[,] GetMatrix()
        {
            log.Info("Getting matrix from BAContainer object.");
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

        public override int[][] GetBranches()
        {
            throw new NotImplementedException();
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

  
        public void ConnectVertex(int i,int j)
        {
            var ivertexdegree = CountVertexDegree(i);
            var jvertexdegree = CountVertexDegree(j);
            AddEdge(i, j);
            --degrees[ivertexdegree];
            --degrees[jvertexdegree];
            ++degrees[ivertexdegree + 1];
            ++degrees[jvertexdegree + 1];

        }


        // Закрытая часть класса (не из общего интерфейса). //

        private void SetDataToDictionary(int index, ArrayList neighbourshipOfIVertex)
        {
            neighbourship[index] = new List<int>();
            for (int j = 0; j < size; j++)
                if ((bool)neighbourshipOfIVertex[j] == true && index != j)
                    neighbourship[index].Add(j);
        }

        // Добавление ребра между данными вершинами.
        private void AddEdge(int i, int j)
        {
            neighbourship[i].Add(j);
            neighbourship[j].Add(i);
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
