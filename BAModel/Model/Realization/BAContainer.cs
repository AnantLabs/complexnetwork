using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.BAModel.Realization
{
    public class BAContainer
    {
        // Implementation members //
        private int m_size;                                     // number of vertices
        private SortedDictionary<int, List<int>> m_neighbourship;     // list of neighbours     

        private List<int> m_degrees;                            // list of degrees (from 0 to m_size - 1)

        public BAContainer(int size)
        {
            m_size = size;
            m_neighbourship = new SortedDictionary<int, List<int>>();
            for (int i = 0; i < m_size; ++i)
                m_neighbourship[i] = new List<int>();

            m_degrees = new List<int>();
            m_degrees.Add(m_size);
            for (int i = 1; i < m_size; ++i)
                m_degrees.Add(0);
        }
        public BAContainer(ArrayList matrix)
        {
            m_size = matrix.Count;
            m_neighbourship = new SortedDictionary<int, List<int>>();
            ArrayList neighbourshipOfIVertex = new ArrayList();
            for (int i = 0; i < matrix.Count; i++)
            {
                neighbourshipOfIVertex = (ArrayList)matrix[i];
                setDataToDictionary(i, neighbourshipOfIVertex);
            }
        }

        public void setDataToDictionary(int index, ArrayList neighbourshipOfIVertex)
        {
            m_neighbourship[index] = new List<int>();
            for (int j = 0; j < m_size; j++)
                if ((bool)neighbourshipOfIVertex[j] == true && index != j)
                    m_neighbourship[index].Add(j);
        }
        public void AddVertex()
        {
            m_neighbourship.Add(m_size, new List<int>());
            ++m_size;
            m_degrees.Add(0);
        }

        public int CountVertexDegree(int i)
        {
            return m_neighbourship[i].Count;
        }

        public double[] CountProbabilities()
        {
            double[] result = new double[this.m_size];

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

        public void RefreshNeighbourships(bool[] generatedVector)
        {
            int newVertexDegree = 0, iVertexDegree = 0;

            for (int i = 0; i < generatedVector.Length; ++i)
            {
                if (generatedVector[i])
                {
                    ++newVertexDegree;
                    AddEdge(i, m_size - 1);
                    iVertexDegree = CountVertexDegree(i);
                    --m_degrees[iVertexDegree];
                    ++m_degrees[iVertexDegree + 1];
                }
            }

            ++m_degrees[newVertexDegree];
        }

        public int GetDegree(int i)
        {
            return m_degrees[i];
        }

        // Get functions //
        public int Size
        {
            get { return m_size; }
        }

        public SortedDictionary<int, List<int>> Neighbourship
        {
            get { return m_neighbourship; }
        }

        // Utilities //
        private void AddEdge(int i, int j)
        {
            m_neighbourship[i].Add(j);
            m_neighbourship[j].Add(i);
        }

        public bool AreNeighbours(int i, int j)
        {
            return m_neighbourship[i].Contains(j);
        }

        private int CountGraphDegree()
        {
            int sum = 0;
            for (int i = 0; i < m_size; ++i)
                sum += CountVertexDegree(i);

            return sum;
        }
        public bool[,] GetMatrix()
        {
            bool[,] matrix = new bool[m_neighbourship.Count, m_neighbourship.Count];

            for (int i = 0; i < m_neighbourship.Count; i++)
                for (int j = 0; j < m_neighbourship.Count; j++)
                    matrix[i,j] = false;

            List<int> list = new List<int>();

            for (int i = 0; i < m_neighbourship.Count; i++)
            {
                list = m_neighbourship[i];
                for (int j = 0; j < list.Count; j++)
                    matrix[i, list[j]] = true;
            }

            return matrix;

        }
    }
}
