using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Model;
using RandomNumberGeneration;

namespace NonRegularHierarchicModel
{
    /// <summary>
    /// Implementation of non regularly branching block-hierarchic network's generator.
    /// </summary>
    class NonRegularHierarchicNetworkGenerator : INetworkGenerator
    {
        /// <summary>
        /// Container with network of specified model (non regular block-hierarchic).
        /// </summary>
        private NonRegularHierarchicNetworkContainer container;

        public NonRegularHierarchicNetworkGenerator()
        {
            container = new NonRegularHierarchicNetworkContainer();
        }

        public INetworkContainer Container
        {
            get { return container; }
            set { container = (NonRegularHierarchicNetworkContainer)value; }
        }

        public void RandomGeneration(Dictionary<GenerationParameter, object> genParam)
        {
            // TODO change without parse
            UInt32 vertices = UInt32.Parse(genParam[GenerationParameter.Vertices].ToString());
            UInt16 branchingIndex = UInt16.Parse(genParam[GenerationParameter.BranchingIndex].ToString());
            Single mu = Single.Parse(genParam[GenerationParameter.Mu].ToString());

            container.Vertices = vertices;
            container.BranchIndex = branchingIndex;            
            container.HierarchicTree = GenerateByVertices(mu);
        }

        public void StaticGeneration(ArrayList matrix)
        {
            container.SetMatrix(matrix);
        }

        private RNGCrypto rand = new RNGCrypto();
        private const int ARRAY_MAX_SIZE = 2000000000;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mu"></param>
        /// <returns></returns>
        private BitArray[][] GenerateByVertices(Single mu)
        {
            List<List<int>> branchList = GenerateBranchList();
            container.Level = (UInt16)branchList.Count;
            container.Branches = new UInt16[container.Level][];
            BitArray[][] treeMatrix = new BitArray[container.Level][];

            for (int i = 0; i < container.Level; ++i)
            {
                int levelVertexCount = branchList[container.Level - 1 - i].Count;
                container.Branches[i] = new UInt16[levelVertexCount];
                long dataLength = 0;
                for (int j = 0; j < levelVertexCount; ++j)
                {
                    int nodeDataLength = branchList[container.Level - 1 - i][j];
                    container.Branches[i][j] = (UInt16)nodeDataLength;
                    dataLength += nodeDataLength * (nodeDataLength - 1) / 2;
                }

                GenerateTreeMatrixForLevel(treeMatrix, i, dataLength);
            }

            GenerateData(treeMatrix, mu);
            return treeMatrix;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<List<int>> GenerateBranchList()
        {
            List<List<int>> branchList = new List<List<int>>();
            int left = (int)container.Size;

            while (left != 1)
            {
                List<int> list = new List<int>();
                while (left != 0)
                {
                    int randomInt = rand.Next(1, container.BranchIndex + 1);
                    if (randomInt > left)
                    {
                        randomInt = left;
                    }
                    left -= randomInt;
                    list.Add(randomInt);
                }
                branchList.Add(list);
                left = branchList[branchList.Count - 1].Count;
            }
            return branchList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="treeMatrix"></param>
        /// <param name="level"></param>
        /// <param name="dataLength"></param>
        private void GenerateTreeMatrixForLevel(BitArray[][] treeMatrix, 
            int level, 
            long dataLength)
        {
            int arrCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dataLength) / ARRAY_MAX_SIZE));
            treeMatrix[level] = new BitArray[arrCount];
            if (arrCount > 0)
            {
                int t;
                for (t = 0; t < arrCount - 1; ++t)
                {
                    treeMatrix[level][t] = new BitArray(ARRAY_MAX_SIZE);
                }
                treeMatrix[level][t] = new BitArray(Convert.ToInt32(dataLength - (arrCount - 1) * ARRAY_MAX_SIZE));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="treeMatrix"></param>
        /// <param name="mu"></param>
        private void GenerateData(BitArray[][] treeMatrix, Single mu)
        {
            for (int currentLevel = 0; currentLevel < container.Level; ++currentLevel)
            {
                if (treeMatrix[currentLevel].Length > 0)
                {
                    int branchSize = container.Branches[currentLevel][0];
                    int counter = 0, nodeNumber = 0;
                    for (int i = 0; i < treeMatrix[currentLevel].Length; i++)
                    {
                        for (int j = 0; j < treeMatrix[currentLevel][i].Length; j++)
                        {
                            if (counter == (branchSize * (branchSize - 1) / 2))
                            {
                                ++nodeNumber;
                                counter = 0;
                                branchSize = container.Branches[currentLevel][nodeNumber];
                            }
                            double k = rand.NextDouble();
                            if (k <= (1 / Math.Pow(container.CountLeaves(currentLevel, nodeNumber), mu)))
                            {
                                treeMatrix[currentLevel][i][j] = true;
                            }
                            else
                            {
                                treeMatrix[currentLevel][i][j] = false;
                            }
                            ++counter;
                        }
                    }
                }
            }
        }
    }
}
