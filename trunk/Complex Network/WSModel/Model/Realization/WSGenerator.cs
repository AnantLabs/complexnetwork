using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumberGeneration;

namespace Model.WSModel.Realization
{
    public class WSGenerator
    {
        // Initialization members //
        public int m_currentId = 0;
        private int m_size;             // number of initial vertices
        private double m_probability;
        private List<int> m_collectRandoms;

        public WSGenerator(double prob, int size)
        {
            m_probability = prob;
            m_size = size;
            m_collectRandoms = new List<int>(m_size);
        }
        public void Randomize()
        {
            Random rand = new Random();
            m_collectRandoms.Clear();

            for (int i = 0; i < m_size; ++i)
            {
                double rand_number = rand.Next(0, m_size);
                m_collectRandoms.Add((int)rand_number);
            }
        }

        public int WSStep(List<int> indexes, int index)
        {
            // select a number from indices with m_prob probability 
            // or return index with 1 - m_prob probability

            if (m_probability * m_size > m_collectRandoms[m_currentId])
            {
                int cycleCount = 0;
                while (m_collectRandoms[m_currentId] > indexes.Count - 1)
                {
                    cycleCount++;
                    if (m_currentId == m_collectRandoms.Count - 1)
                        m_currentId = 0;
                    else
                        ++m_currentId;
                    if (cycleCount > m_size)
                        return index;
                }

                int id = indexes[m_collectRandoms[m_currentId]];
                if (m_currentId == m_collectRandoms.Count - 1)
                    m_currentId = 0;
                else
                    ++m_currentId;
                return id;
            }
            if (m_currentId == m_collectRandoms.Count - 1)
                m_currentId = 0;
            else
                ++m_currentId;
            return index;
        }
    }
}
