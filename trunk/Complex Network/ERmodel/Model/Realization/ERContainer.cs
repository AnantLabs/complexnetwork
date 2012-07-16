using System;
using System.Collections;
using System.Collections.Generic;
using log4net;
using NumberGeneration;

using CommonLibrary.Model;

namespace Model.ERModel.Realization
{
    public class ERContainer : IGraphContainer
    {
        // Организация Работы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(ERContainer));

        // Число вершин графа.
        private int size = 0;
        // Списки соседей для вершин графа.
        private SortedDictionary<int, List<int>> neighbourship;
        // Список степеней вершин графа.
        private List<int> degrees;

        // !Исправить!
        private RNGCrypto r = new RNGCrypto();

        // Конструктор по умолчанию для контейнера.
        public ERContainer()
        {
            log.Info("Creating ERContainer default object.");
            neighbourship = new SortedDictionary<int, List<int>>();
            degrees = new List<int>();
        }

        // Размер контейнера (число вершин в графе).
        public int Size 
        {
            get { return size; }
            set 
            {
                log.Info("Creating ERContainer object with given size");
                size = value;                
                for (int i = 0; i < size; ++i)
                {
                    neighbourship[i] = new List<int>();
                }

                for (int i = 0; i < size; ++i)
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
        public void SetMatrix(ArrayList matrix)
        {
            log.Info("Creating ERContainer object from given matrix");
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
        public bool[,] GetMatrix()
        {
            bool[,] matrix = new bool[neighbourship.Count, neighbourship.Count];

            for (int i = 0; i < neighbourship.Count; i++)
            {
                for (int j = 0; j < neighbourship.Count; j++)
                {
                    matrix[i, j] = false;
                }
            }

            List<int> list = new List<int>();

            for (int i = 0; i < neighbourship.Count; i++)
            {
                list = neighbourship[i];
                for (int j = 0; j < list.Count; j++)
                {
                    matrix[i, list[j]] = true;
                }
            }

            return matrix;
        }


        // Закрытая часть класса (не из общего интерфейса). // 

        private void SetDataToDictionary(int index, ArrayList neighbourshipOfIVertex)
        {
            neighbourship[index] = new List<int>();
            for (int j = 0; j < size; j++)
            {
                if ((bool)neighbourshipOfIVertex[j] == true && index != j)
                {
                    neighbourship[index].Add(j);
                }
            }
        }

        private void AddVertex()
        {
            neighbourship.Add(size, new List<int>());
            ++size;
            degrees.Add(0);
        }

        private int CountVertexDegree(int i)
        {
            return neighbourship[i].Count;
        }

        private void FillContainerByProbability(double p)
        {
            for (int i = 0; i < size; ++i)
            {
                for (int j = i + 1; j < size; ++j)
                {
                    double a = r.NextDouble();
                    if (a < p)
                    {
                        AddEdge(i, j);
                        ++degrees[i];
                        ++degrees[j];
                    }
                }
            }
        }

        private int GetDegree(int i)
        {
            return degrees[i];
        }        

        private void AddEdge(int i, int j)
        {
            neighbourship[i].Add(j);
            neighbourship[j].Add(i);
        }

        private bool AreNeighbours(int i, int j)
        {
            return neighbourship[i].Contains(j);
        }

        private int CountGraphDegree()
        {
            int sum = 0;
            for (int i = 0; i < size; ++i)
                sum += CountVertexDegree(i);

            return sum;
        }
    }
}