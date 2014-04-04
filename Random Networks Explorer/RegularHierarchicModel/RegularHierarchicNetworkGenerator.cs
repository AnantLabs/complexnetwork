using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;
using Core.Model;
using RandomNumberGeneration;

namespace RegularHierarchicModel
{
    /// <summary>
    /// Implementation of regularly branching block-hierarchic network's generator.
    /// </summary>
    class RegularHierarchicNetworkGenerator : INetworkGenerator
    {
        /// <summary>
        /// Container with network of specified model (regular block-hierarchic).
        /// </summary>
        private RegularHierarchicNetworkContainer container;

        public RegularHierarchicNetworkGenerator()
        {
            container = new RegularHierarchicNetworkContainer();
        }

        public INetworkContainer Container
        {
            get { return container; }
            set { container = (RegularHierarchicNetworkContainer)value; }
        }

        public void RandomGeneration(Dictionary<GenerationParameter, object> genParam)
        {
            UInt16 branchingIndex = (UInt16)genParam[GenerationParameter.BranchingIndex];
            UInt16 level = (UInt16)genParam[GenerationParameter.Level];
            Single mu = (Single)genParam[GenerationParameter.Mu];

            container.BranchingIndex = branchingIndex;
            container.Level = level;
            container.HierarchicMatrix = GenerateTree(branchingIndex, level, mu);
        }

        public void StaticGeneration(ArrayList matrix)
        {
            container.SetMatrix(matrix);
        }

        private RNGCrypto rand = new RNGCrypto();
        private const int ARRAY_MAX_SIZE = 2000000000;        

        /// <summary>
        /// Recursively creates a block-hierarchic tree.
        /// </summary>
        /// <note>Data is initializing and generating started from root.</note>
        private BitArray[][] GenerateTree(UInt16 branchingIndex, 
            UInt16 level, 
            Single mu)
        {
            BitArray[][] treeMatrix = new BitArray[level][];

            int nodeDataLength = (branchingIndex - 1) * branchingIndex / 2;
            long levelDataLength;
            int arrayCountForLevel;
            for (UInt16 i = level; i > 0; --i)
            {                
                levelDataLength = Convert.ToInt64(Math.Pow(branchingIndex, level - i) * nodeDataLength);
                arrayCountForLevel = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(levelDataLength) / ARRAY_MAX_SIZE));

                treeMatrix[level - i] = new BitArray[arrayCountForLevel];
                int j;
                for (j = 0; j < arrayCountForLevel - 1; j++)
                {
                    treeMatrix[level - i][j] = new BitArray(ARRAY_MAX_SIZE);
                }
                treeMatrix[level - i][j] = new BitArray(Convert.ToInt32(levelDataLength -
                    (arrayCountForLevel - 1) * ARRAY_MAX_SIZE));

                GenerateData(treeMatrix, i, branchingIndex, level, mu);
            }

            return treeMatrix;
        }
        
        /// <summary>
        /// Generates random data for current level of block-hierarchic tree.
        /// </summary>
        /// <node>Current level must be initialized.</node>
        private void GenerateData(BitArray[][] treeMatrix, 
            UInt16 currentLevel, 
            UInt16 branchingIndex, 
            UInt16 maxLevel, 
            Single mu)
        {
            for (int i = 0; i < treeMatrix[maxLevel - currentLevel].Length; i++)
            {
                for (int j = 0; j < treeMatrix[maxLevel - currentLevel][i].Length; j++)
                {
                    double k = rand.NextDouble();
                    if (k <= (1 / Math.Pow(branchingIndex, currentLevel * mu)))
                    {
                        treeMatrix[maxLevel - currentLevel][i][j] = true;
                    }
                    else
                    {
                        treeMatrix[maxLevel - currentLevel][i][j] = false;
                    }
                }
            }
        }
    }
}
