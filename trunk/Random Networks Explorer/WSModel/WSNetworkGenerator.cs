﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Model;
using NetworkModel;
using RandomNumberGeneration;

namespace WSModel
{
    /// <summary>
    /// Implementation of generator of random network of Watts-Strogatz's model.
    /// </summary>
    class WSNetworkGenerator : INetworkGenerator
    {
        private NonHierarchicContainer container;

        public WSNetworkGenerator()
        {
            container = new NonHierarchicContainer();
        }

        public INetworkContainer Container
        {
            get { return container; }
            set { container = (NonHierarchicContainer)value; }
        }

        public void RandomGeneration(Dictionary<GenerationParameter, object> genParam)
        {
            UInt16 numberOfVertices = Convert.ToUInt16(genParam[GenerationParameter.Vertices]);
            UInt32 numberOfEdges = Convert.ToUInt32(genParam[GenerationParameter.Edges]);
            Single probability = Convert.ToSingle(genParam[GenerationParameter.Probability]);
            UInt16 stepCount = Convert.ToUInt16(genParam[GenerationParameter.StepCount]);

            //container.SetParameters(numberOfVertices, numberOfEdges / 2);
            Randomize();
            FillValuesByProbability(probability, stepCount);
        }

        public void StaticGeneration(MatrixInfoToRead matrixInfo)
        {
            container.SetMatrix(matrixInfo.Matrix);
        }

        private int currentId = 0;
        private List<int> collectRandoms = new List<int>();

        private void Randomize()
        {
            Random rand = new Random();
            collectRandoms.Clear();

            for (int i = 0; i < container.Size; ++i)
            {
                double rand_number = 0;// rand.Next(0, container.Size);
                collectRandoms.Add((int)rand_number);
            }
        }

        private void FillValuesByProbability(double probability, int stepCount)
        {
            while (stepCount > 0)
            {
                for (int i = 1; i < container.Size; ++i)
                {
                    List<int> neighbours = new List<int>();
                    List<int> nonNeighbours = new List<int>();
                    for (int k = 0; k < container.Size && k < i; ++k)
                    {
                        if (container.AreConnected(i, k))
                            neighbours.Add(k);
                        else
                            nonNeighbours.Add(k);
                    }

                    if (nonNeighbours.Count > 0)
                    {
                        int size_neighbours = neighbours.Count;
                        for (int j = 0; j < size_neighbours; ++j)
                        {
                            int r = WSStep(probability, nonNeighbours, neighbours[j]);
                            if (r != neighbours[j])
                            {
                                //container.Disconnect(i, neighbours[j]);
                                container.AreConnected(i, r);
                            }
                        }
                    }
                }
                --stepCount;
            }
        }

        public int WSStep(double probability, List<int> indexes, int index)
        {
            // select a number from indices with m_prob probability 
            // or return index with 1 - m_prob probability

            if (probability * container.Size > collectRandoms[currentId])
            {
                int cycleCount = 0;
                while (collectRandoms[currentId] > indexes.Count - 1)
                {
                    cycleCount++;
                    if (currentId == collectRandoms.Count - 1)
                        currentId = 0;
                    else
                        ++currentId;
                    if (cycleCount > container.Size)
                        return index;
                }

                int id = indexes[collectRandoms[currentId]];
                if (currentId == collectRandoms.Count - 1)
                    currentId = 0;
                else
                    ++currentId;
                return id;
            }
            if (currentId == collectRandoms.Count - 1)
                currentId = 0;
            else
                ++currentId;
            return index;
        }
    }
}
