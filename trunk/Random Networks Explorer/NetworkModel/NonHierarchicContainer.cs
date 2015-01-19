﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

using Core;
using Core.Utility;
using Core.Model;
using Core.Settings;
using RandomNumberGeneration;

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

        // Data for randomization. //
        private List<KeyValuePair<int, int>> existingEdges;
        private List<KeyValuePair<int, int>> nonExistingEdges;

        public NonHierarchicContainer()
        {
            neighbourship = new SortedDictionary<int, List<int>>();
            degrees = new List<int>();

            existingEdges = new List<KeyValuePair<int, int>>();
            nonExistingEdges = new List<KeyValuePair<int, int>>();
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

                existingEdges.Clear();

                nonExistingEdges.Clear();
                for (int i = 0; i < size; ++i)
                {
                    for (int j = i + 1; j < size; ++j)
                        nonExistingEdges.Add(new KeyValuePair<int, int>(i, j));
                }
            }
        }

        public SortedDictionary<int, List<int>> Neighbourship
        {
            get { return neighbourship; }
        }

        public void SetMatrix(ArrayList matrix)
        {
            Size = (uint)matrix.Count;
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

        // TODO add to interface
        static int traceCount = 1;

        public void Trace(String extendedName)
        {
            String tracingDirectory = ExplorerSettings.TracingDirectory;
            if (tracingDirectory != "")
            {
                string newFolderName = tracingDirectory + "\\" + extendedName;
                Directory.CreateDirectory(newFolderName);
                string filePath = newFolderName + "\\" + traceCount.ToString();
                Interlocked.Increment(ref traceCount);

                MatrixInfoToWrite matrixInfo = new MatrixInfoToWrite();
                matrixInfo.Matrix = GetMatrix();
                matrixInfo.Branches = null;
                FileManager.Write(matrixInfo, filePath);
            }
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

                KeyValuePair<int, int> newEdje = new KeyValuePair<int, int>(i, j);
                existingEdges.Add(newEdje);
                nonExistingEdges.Remove(newEdje);
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
                int iVertexDegree = GetVertexDegree(i);
                int jVertexDegree = GetVertexDegree(j);

                neighbourship[i].Remove(j);
                neighbourship[j].Remove(i);

                --degrees[iVertexDegree];
                --degrees[jVertexDegree];
                ++degrees[iVertexDegree - 1];
                ++degrees[jVertexDegree - 1];

                KeyValuePair<int, int> oldEdje = new KeyValuePair<int, int>(i, j);
                existingEdges.Remove(oldEdje);
                nonExistingEdges.Add(oldEdje);
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
            return existingEdges.Count();
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

        /// <summary>
        /// Clones the container.
        /// </summary>
        /// <returns>Cloned container.</returns>
        public NonHierarchicContainer Clone()
        {
            NonHierarchicContainer clone = (NonHierarchicContainer)this.MemberwiseClone();

            clone.neighbourship = new SortedDictionary<int, List<int>>(this.neighbourship);
            foreach (var item in this.neighbourship)
            {
                clone.neighbourship[item.Key] = new List<int>(item.Value);
            }
            clone.degrees = new List<int>(this.degrees);

            clone.existingEdges = new List<KeyValuePair<int, int>>(existingEdges);
            clone.nonExistingEdges = new List<KeyValuePair<int, int>>(nonExistingEdges);

            return clone;
        }

        public int NonPermanentRandomization()
        {
            RNGCrypto rand = new RNGCrypto();

            int edgeToRemove = rand.Next(0, existingEdges.Count - 1);
            int rvertex1 = existingEdges[edgeToRemove].Key;
            int rvertex2 = existingEdges[edgeToRemove].Value;

            int edgeToAdd = rand.Next(0, nonExistingEdges.Count - 1);
            int avertex1 = nonExistingEdges[edgeToAdd].Key;
            int avertex2 = nonExistingEdges[edgeToAdd].Value;

            RemoveConnection(rvertex1, rvertex2);
            // Calculate removed cycles count
            int removedCyclesCount = Cycles3ByVertices(rvertex1, rvertex2);

            AddConnection(avertex1, avertex2);
            // Calculate removed cycles count
            int addedCyclesCount = Cycles3ByVertices(avertex1, avertex2);

            return addedCyclesCount - removedCyclesCount;
        }

        public int PermanentRandomization()
        {
            RNGCrypto rand = new RNGCrypto();

            int e1 = rand.Next(0, existingEdges.Count - 1);
            int e2 = rand.Next(0, existingEdges.Count - 1);

            while (existingEdges[e1].Key == existingEdges[e2].Key ||
                existingEdges[e1].Value == existingEdges[e2].Value ||
                existingEdges[e1].Key == existingEdges[e2].Value ||
                existingEdges[e1].Value == existingEdges[e2].Key ||
                AreConnected(existingEdges[e1].Key, existingEdges[e2].Key) ||
                AreConnected(existingEdges[e1].Value, existingEdges[e2].Value) ||
                AreConnected(existingEdges[e1].Key, existingEdges[e2].Value) ||
                AreConnected(existingEdges[e1].Value, existingEdges[e2].Key))
            {
                e1 = rand.Next(0, existingEdges.Count - 1);
                e2 = rand.Next(0, existingEdges.Count - 1);
            }

            int vertex1 = existingEdges[e1].Key, vertex2 = existingEdges[e1].Value;
            int vertex3 = existingEdges[e2].Key, vertex4 = existingEdges[e2].Value;
            RemoveConnection(vertex1, vertex2);
            RemoveConnection(vertex3, vertex4);
            // Calculate removed cycles count
            int removedCyclesCount = Cycles3ByVertices(vertex1, vertex2) +
                Cycles3ByVertices(vertex3, vertex4);

            AddConnection(vertex1, vertex3);
            AddConnection(vertex2, vertex4);
            // Calculate removed cycles count
            int addedCyclesCount = Cycles3ByVertices(vertex1, vertex3) +
                Cycles3ByVertices(vertex2, vertex4);

            return addedCyclesCount - removedCyclesCount;
        }

        private void SetDataToDictionary(int index, ArrayList neighbourshipOfIVertex)
        {
            for (int j = 0; j < size; j++)
                if ((bool)neighbourshipOfIVertex[j] == true && index != j)
                {
                    AddConnection(index, j);
                }
        }

        private int CalculateSumOfDegrees()
        {
            int sum = 0;
            for (int i = 0; i < size; ++i)
                sum += GetVertexDegree(i);

            return sum;
        }

        private int Cycles3ByVertices(int i, int j)
        {
            List<int> n = neighbourship[j];

            int count = 0;
            for (int t = 0; t < n.Count; t++)
            {
                if (n[t] != i)
                {
                    if (neighbourship[n[t]].Contains(i))
                        ++count;
                }
            }

            return count;
        }
    }
}
