using System;
using System.Collections;
using System.Collections.Generic;

using CommonLibrary.Model;
using log4net;

namespace Model.ERModel.Realization
{
    // Реализация контейнера (ER).
    public class ERContainer : IGraphContainer
    {
        // Организация pаботы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(ERContainer));

        // Число вершин графа.
        private int size = 0;
        // Списки соседей для вершин графа.
        private SortedDictionary<int, List<int>> neighbourship;
        // Список степеней вершин графа.
        private List<int> degrees;
        public List<KeyValuePair<int, int>> Edjes = new List<KeyValuePair<int, int>>();
        public List<KeyValuePair<int, int>> NoEdjes = new List<KeyValuePair<int, int>>();

        // Конструктор по умолчанию для контейнера.
        public ERContainer()
        {
            log.Info("Creating ERContainer default object.");
            neighbourship = new SortedDictionary<int, List<int>>();
            degrees = new List<int>();
        }

        public ERContainer(ERContainer cotainer)
        {
            this.size = cotainer.Size;
            this.neighbourship = cotainer.neighbourship;
            this.Edjes = new List<KeyValuePair<int,int>>(cotainer.Edjes);
            this.NoEdjes = new List<KeyValuePair<int,int>>(cotainer.NoEdjes);
            this.degrees = new List<int>( cotainer.degrees);


            
        }

        public ERContainer Copy()
        {
            ERContainer other = (ERContainer)this.MemberwiseClone();
            other.neighbourship = new SortedDictionary<int, List<int>>(this.neighbourship);
            other.Edjes = new List<KeyValuePair<int, int>>(this.Edjes);
            other.NoEdjes = new List<KeyValuePair<int, int>>(this.NoEdjes);
            other.degrees = new List<int>(this.degrees);
            return other;
        }

        // Размер контейнера (число вершин в графе).
        public int Size 
        {
            get { return size; }
            set 
            {
                log.Info("Creating ERContainer object with given size.");
                size = value;                
                for (int i = 0; i < size; ++i)
                {
                    neighbourship[i] = new List<int>();
                }

                for (int i = 0; i < size; ++i)
                {
                    degrees.Add(0);
                }

                for (int i = 0; i < size; i++)
                    for (int j = i + 1; j < size; j++)
                        NoEdjes.Add(new KeyValuePair<int, int>(i, j));
            }
        }

        // Списки соседей для вершин графа.
        public  SortedDictionary<int, List<int>> Neighbourship
        {
            get { return neighbourship; }
        }

        // Строится граф на основе матрицы смежности.
        public void SetMatrix(ArrayList matrix)
        {
            log.Info("Creating ERContainer object from given matrix.");
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
            log.Info("Getting matrix from ERContainer object.");
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

        // Методы не из общего интерфейса.

        // Добавление ребра между данными вершинами.
        public void AddEdge(int i, int j)
        {
            neighbourship[i].Add(j);
            neighbourship[j].Add(i);
            ++degrees[i];
            ++degrees[j];
            var newEdjes = new KeyValuePair<int, int>(i, j);
            Edjes.Add(newEdjes);
            NoEdjes.Remove(newEdjes);
        }

        // Закрытая часть класса (не из общего интерфейса).

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

        // Добавление вершины (не имеющий соседей). Не используется.
        private void AddVertex()
        {
            neighbourship.Add(size, new List<int>());
            ++size;
            degrees.Add(0);
        }

        // Проверяет являются ли данные вершины соседями (true - если да). Не используется.
        private bool AreNeighbours(int i, int j)
        {
            return neighbourship[i].Contains(j);
        }

        // Возвращает степень графа (сумма всеь степеней вершин). Не используется.
        private int CountGraphDegree()
        {
            int sum = 0;
            for (int i = 0; i < size; ++i)
                sum += CountVertexDegree(i);

            return sum;
        }

        // Возвращает число соседей данной вершины.
        private int CountVertexDegree(int i)
        {
            return neighbourship[i].Count;
        }
    }
}