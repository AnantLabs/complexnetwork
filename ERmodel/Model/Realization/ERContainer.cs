using System;
using System.Collections;
using System.Collections.Generic;
using log4net;
using NumberGeneration;

namespace Model.ERModel.Realization
{
    public class ERContainer
    {
        /// <summary>
        /// The logger static object for monitoring.
        /// </summary>
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(ERContainer));

        private RNGCrypto r = new RNGCrypto();
        private int m_size;                                            // number of vertices
        private SortedDictionary<int, List<int>> m_neighbourship;      // list of neighbours     

        // this will be eliminated later
        private List<int> m_degrees;   

        public ERContainer(int size)
        {
            log.Info("Creating ERContainer object with given size");
            m_size = size;
            m_neighbourship = new SortedDictionary<int, List<int>>();
            for (int i = 0; i < m_size; ++i)
            {
                m_neighbourship[i] = new List<int>();
            }

            m_degrees = new List<int>();
            for (int i = 0; i < m_size; ++i)
            {
                m_degrees.Add(0);
            }
        }

        public ERContainer(ArrayList matrix)
        {
            log.Info("Creating ERContainer object from given matrix");
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
            {
                if ((bool)neighbourshipOfIVertex[j] == true && index != j)
                {
                    m_neighbourship[index].Add(j);
                }
            }
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

        public void FillContainerByProbability(double p)
        {
            for (int i = 0; i < m_size; ++i)
            {
                for (int j = i + 1; j < m_size; ++j)
                {
                    double a = r.NextDouble();
                    if (a < p)
                    {
                        AddEdge(i, j);
                        ++m_degrees[i];
                        ++m_degrees[j];
                    }
                }
            }
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
            {
                for (int j = 0; j < m_neighbourship.Count; j++)
                {
                    matrix[i, j] = false;
                }
            }

            List<int> list = new List<int>();

            for (int i = 0; i < m_neighbourship.Count; i++)
            {
                list = m_neighbourship[i];
                for (int j = 0; j < list.Count; j++)
                {
                    matrix[i, list[j]] = true;
                }
            }

            return matrix;
        }
    }
}