using System;
using System.Collections;
using System.Collections.Generic;

using CommonLibrary.Model;
using log4net;

namespace Model.WSModel.Realization
{
    // Реализация контейнера (WS).
    public class WSContainer : IGraphContainer
    {
        // Организация Работы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(WSContainer));

        // Число вершин графа.
        private int size = 0;
        // Число ребер графа.
        private int edges = 0;
        // Список индексов соседей (специализированный для данной модели).
        private Dictionary<int, ArrayList> indexes;

        // Конструктор по умолчанию для контейнера.
        public WSContainer() { }

        // Размер контейнера (число вершин в графе).
        public int Size
        {
            get { return size; }
            set { } // ??
        }

        public int Edges
        {
            get { return edges; }
        }

        public Dictionary<int, ArrayList> NeighbourshipMap
        {
            get { return indexes; }
        }

        // Инициализируются поля контейнера, соответствующим для данной модели образом.
        public void SetParameters(int s, int e)
        {
            size = s;
            edges = e;
            indexes = new Dictionary<int, ArrayList>(size);
            indexes.Add(0, new ArrayList(2));
            indexes[0].Add(true);
            indexes[0].Add(new List<int>());

            for (int i = 1; i <= edges; ++i)
            {
                indexes.Add(i, new ArrayList(2));
                indexes[i].Add(true);
                indexes[i].Add(new List<int>());
                List<int> ls = (List<int>)indexes[i][1];
                ls.Add(0);
            }

            for (int i = edges + 1; i < size - edges; ++i)
            {
                indexes.Add(i, new ArrayList(2));
                indexes[i].Add(false);
                indexes[i].Add(new List<int>());
                List<int> ls = (List<int>)indexes[i][1];
                ls.Add(0);
                ls.Add(i - edges);
            }

            for (int i = size - edges, k = 1; i < size; ++i, ++k)
            {
                indexes.Add(i, new ArrayList(2));
                indexes[i].Add(true);
                indexes[i].Add(new List<int>());
                List<int> ls = (List<int>)indexes[i][1];
                ls.Add(0);
                ls.Add(k);
                ls.Add(i - edges);
            }
        }

        // Строится граф на основе матрицы смежности.
        public void SetMatrix(ArrayList matrix)
        {
            size = matrix.Count;
            indexes = new Dictionary<int, ArrayList>(size);

            indexes.Add(0, new ArrayList(2));
            indexes[0].Add(true);
            indexes[0].Add(new List<int>());

            for (int i = 1; i < matrix.Count; ++i)
            {
                ArrayList data = (ArrayList)matrix[i];
                indexes.Add(i, new ArrayList(2));
                indexes[i].Add((bool)data[0]);
                List<int> lst = new List<int>();
                lst.Add(0);
                bool var = (bool)data[0];
                for (int k = 1; k < i; ++k)
                {
                    if ((bool)data[k] != var)
                    {
                        lst.Add(k);
                        var = !var;
                    }
                }
                indexes[i].Add(lst);
            }
        }

        // Возвращается матрица смежности, соответсвующая графу.
        public bool[,] GetMatrix()
        {
            Dictionary<int, List<int>> matrixDict = GetMatrixDict();
            bool[,] matrix = new bool[size, size];
            for (int i = 0; i < size; ++i)
            {
                List<int> lst = matrixDict[i];
                matrix[i, i] = false;
                for (int j = 0; j < i; ++j)
                {
                    if (i == j)
                    {
                        matrix[i, j] = false;
                        continue;
                    }
                    matrix[i, j] = (lst[j] == 0) ? false : true;
                    matrix[j, i] = matrix[i, j];
                }
            }

            return matrix;
        }

        // Методы не из общего интерфейса.

        // Возвращается степень данной вершины.
        public int CountDegree(int i)
        {
            int nCount = 0;
            for (int j = 0; j < size; ++j)
                if (AreNeighbours(i, j))
                    nCount++;

            return nCount;
        }

        // Возвращает список соседей данной вершины.
        public List<int> Neighbours(int i)
        {
            List<int> neighbours = new List<int>();
            for (int j = 0; j < size; ++j)
                if (AreNeighbours(i, j))
                    neighbours.Add(j);
            return neighbours;
        }

        // Проверяет являются ли данные вершины соседями (true - если да).
        public bool AreNeighbours(int i, int j)
        {
            if (i < j)
                return AreNeighbours(j, i);
            if (i == j)
                return false;

            List<int> data = new List<int>(i);
            RestoreData(i, data);

            return Convert.ToBoolean(data[j]);
        }

        // Добавление ребра между данными вершинами.
        public void Connect(int i, int j)
        {
            if (i < j)
            {
                Connect(j, i);
                return;
            }
            if (i == j)
                return;

            List<int> data = new List<int>(i);
            RestoreData(i, data);
            data[j] = 1;
            PressData(i, data);
        }

        // Удаление ребра между данными вершинами.
        public void Disconnect(int i, int j)
        {
            if (i < j)
            {
                Disconnect(j, i);
                return;
            }
            if (i == j)
                return;

            List<int> data = new List<int>(i);
            RestoreData(i, data);
            data[j] = 0;
            PressData(i, data);
        }

        // Превращение списка соседей в специализированный список индексов.
        public void PressData(int i, List<int> data)
        {
            indexes[i][0] = Convert.ToBoolean(data[0]);
            List<int> lst = new List<int>();
            lst.Add(0);
            int var = data[0] > 0 ? 1 : 0;
            for (int k = 1; k < data.Count; ++k)
            {
                if (data[k] != var)
                {
                    lst.Add(k);
                    var = var > 0 ? 0 : 1;
                }
            }
            indexes[i][1] = lst;
        }

        // Превращение специализированного списка индексов в список соседей.
        public void RestoreData(int i, List<int> data, bool fromOldMap = false)
        {
            Dictionary<int, ArrayList> t_indexes = indexes;
            List<int> ind = (List<int>)t_indexes[i][1];
            int var = (bool)t_indexes[i][0] ? 1 : 0;
            for (int k = 0; k < (int)ind.Count; ++k)
            {
                int endIndex = (k + 1 >= (int)ind.Count) ? i - 1 : ind[k + 1] - 1;
                for (int j = ind[k]; j <= endIndex; ++j)
                    data.Insert(j, var);
                var = var == 1 ? 0 : 1;
            }
        }

        public Dictionary<int, List<int>> GetMatrixDict()
        {
            Dictionary<int, List<int>> matrix = new Dictionary<int, List<int>>();
            int size = Size;
            for (int i = 0; i < size; i++)
            {
                matrix.Add(i, new List<int>());
                RestoreData(i, matrix[i]);
            }
            return matrix;
        }
    }
}
