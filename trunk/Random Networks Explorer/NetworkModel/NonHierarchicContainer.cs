using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Model;

namespace NetworkModel
{
    /// <summary>
    /// Implementation of non hierarchic network's container.
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

        public UInt32 Size
        {
            get { return (UInt32)size; }
            set
            {
                size = (int)value;

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

        /// <summary>
        /// Adds a new vertex with no connections.
        /// </summary>
        public void AddVertex()
        {
            neighbourship.Add((int)size, new List<int>());
            ++size;
            degrees.Add(0);
        }

        /// <summary>
        /// Adds a connection between specified vertices.
        /// </summary>
        /// <param name="i">First vertex number.</param>
        /// <param name="j">Second vertex number.</param>
        public void AddConnection(int i, int j)
        {
            if (!AreConnected(i, j))
            {
                int ivertexdegree = GetVertexDegree(i);
                int jvertexdegree = GetVertexDegree(j);

                neighbourship[i].Add(j);
                neighbourship[j].Add(i);

                --degrees[ivertexdegree];
                --degrees[jvertexdegree];
                ++degrees[ivertexdegree + 1];
                ++degrees[jvertexdegree + 1];
            }
        }

        /// <summary>
        /// Removes the connection between specified vertices.
        /// </summary>
        /// <param name="i">First vertex number.</param>
        /// <param name="j">Second vertex number.</param>
        public void RemoveConnection(int i, int j)
        {
            if (AreConnected(i, j))
            {
                neighbourship[i].Remove(j);
                neighbourship[j].Remove(i);

                int iVertexDegree = GetVertexDegree(i);
                int jVertexDegree = GetVertexDegree(j);
                --degrees[iVertexDegree];
                --degrees[jVertexDegree];
                ++degrees[iVertexDegree - 1];
                ++degrees[jVertexDegree - 1];
            }
        }

        public bool AreConnected(int i, int j)
        {
            return neighbourship[i].Contains(j);
        }

        public int GetVertexDegree(int i)
        {
            return neighbourship[i].Count;
        }

        public int CalculateNumberOfEdges()
        {
            // TODO add implementation
            return 0;
        }

        /// <summary>
        /// Refreshes the neighbourship information.
        /// </summary>
        /// <param name="generatedVector">New neighourhip information.</param>
        public void RefreshNeighbourships(bool[] generatedVector)
        {
            int newVertexDegree = 0;

            for (int i = 0; i < generatedVector.Length; ++i)
            {
                if (generatedVector[i])
                {
                    ++newVertexDegree;
                    AddConnection(i, size - 1);
                    // TODO check if code is removed correclty
                }
            }

            ++degrees[newVertexDegree];
        }

        /// <summary>
        /// Retrieves probabilities for current state of network.
        /// </summary>
        /// <returns>Array of probabilities.</returns>
        /// <note>For BAModel generation step.</note>
        public double[] CountProbabilities()
        {
            double[] result = new double[this.size];

            double graphDegree = (double)CalculateSumOfDegrees();
            if (graphDegree != 0)
            {
                for (int i = 0; i < result.Length; ++i)
                    result[i] = (double)GetVertexDegree(i) / graphDegree;
            }
            else
            {
                for (int i = 0; i < result.Length; ++i)
                    result[i] = 1.0 / result.Length;
            }

            return result;
        }

        private void SetDataToDictionary(int index, ArrayList neighbourshipOfIVertex)
        {
            neighbourship[index] = new List<int>();
            for (int j = 0; j < size; j++)
                if ((bool)neighbourshipOfIVertex[j] == true && index != j)
                    neighbourship[index].Add(j);
        }

        private int CalculateSumOfDegrees()
        {
            int sum = 0;
            for (int i = 0; i < size; ++i)
                sum += GetVertexDegree(i);

            return sum;
        }
    }
}
