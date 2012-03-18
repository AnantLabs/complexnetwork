using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.WSModel.Realization
{
    public class WSContainer
    {
        // Implementation members //
        private int m_size;                                     // number of vertices
        private int m_param;                                    // number of edges 

        private Dictionary<int, ArrayList> m_indexes;

        public int Size
        {
            get { return m_size; }
        }

        public Dictionary<int, ArrayList> GetMap
        {
            get { return m_indexes; }
        }


        public int Param
        {
            get { return m_param; }
        }

        public WSContainer(int size, int param)
        {
            m_size = size;
            m_param = param;
            m_indexes = new Dictionary<int, ArrayList>(size);
            m_indexes.Add(0, new ArrayList(2));
            m_indexes[0].Add(true);
            m_indexes[0].Add(new List<int>());

            for (int i = 1; i <= param; ++i)
            {
                m_indexes.Add(i, new ArrayList(2));
                m_indexes[i].Add(true);
                m_indexes[i].Add(new List<int>());
                List<int> ls = (List<int>)m_indexes[i][1];
                ls.Add(0);
            }

            for (int i = param + 1; i < m_size - param; ++i)
            {
                m_indexes.Add(i, new ArrayList(2));
                m_indexes[i].Add(false);
                m_indexes[i].Add(new List<int>());
                List<int> ls = (List<int>)m_indexes[i][1];
                ls.Add(0);
                ls.Add(i - param);
            }

            for (int i = m_size - param, k = 1; i < m_size; ++i, ++k)
            {
                m_indexes.Add(i, new ArrayList(2));
                m_indexes[i].Add(true);
                m_indexes[i].Add(new List<int>());
                List<int> ls = (List<int>)m_indexes[i][1];
                ls.Add(0);
                ls.Add(k);
                ls.Add(i - param);
            }
        }

        public WSContainer(ArrayList matrix)
        {
            m_size = matrix.Count;
            m_indexes = new Dictionary<int, ArrayList>(m_size);
            for (int i = 1; i < matrix.Count; ++i)
            {
                ArrayList data = (ArrayList)matrix[i];
                m_indexes.Add(i , new ArrayList(2));
                m_indexes[i].Add((bool)data[0]);
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
                m_indexes[i].Add(lst);
            }
        }

        public int CountDegree(int i)
        {
            int nCount = 0;
            for (int j = 0; j < m_size; ++j)
                if (AreNeighbours(i, j))
                    nCount++;

            return nCount;
        }
        public void Neighbours(int i, List<int> neighbours)
        {
            for (int j = 0; j < m_size; ++j)
                if (AreNeighbours(i, j))
                    neighbours.Add(j);
        }

        public bool AreNeighbours(int i, int j)
        {
            if (i < j)
                return AreNeighbours(j, i);
            if (i == j)
                return false;

            List<int> data = new List<int>(i);
            RestoreData(i, data);

            return convertIntToBool(data[j]);
        }
        public void RestoreData(int i, List<int> data, bool fromOldMap = false)
        {
            Dictionary<int, ArrayList> t_indexes = m_indexes;
            List<int> indexes = (List<int>)t_indexes[i][1];
            int var = (bool)t_indexes[i][0] ? 1 : 0;
            for (int k = 0; k < (int)indexes.Count; ++k)
            {
                int endIndex = (k + 1 >= (int)indexes.Count) ? i - 1 : indexes[k + 1] - 1;
                for (int j = indexes[k]; j <= endIndex; ++j)
                    data.Insert(j, var);
                var = var == 1 ? 0 : 1;
            }
        }

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
        public void PressData(int i, List<int> data)
        {
            m_indexes[i][0] = convertIntToBool(data[0]);
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
            m_indexes[i][1] = lst;
        }
        private bool convertIntToBool(int number)
        {
            return (number == 0) ? false : true;
        }
        public Dictionary<int, List<int>> getMatrixDict()
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

        public bool[,] GetMatrix()
        {
            Dictionary<int, List<int>> matrixDict = getMatrixDict();
            bool[,] matrix = new bool[m_size, m_size];
            for (int i = 0; i < m_size; ++i)
            {
                List<int> lst = matrixDict[i];
                matrix[i, i] = true;
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


    }
}
