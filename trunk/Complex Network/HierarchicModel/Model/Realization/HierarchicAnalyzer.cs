using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using CommonLibrary.Model;

namespace Model.HierarchicModel.Realization
{
    public class HierarchicAnalyzer : AbstarctGraphAnalyzer
    {
        private Engine engine;
        private EngineForConnectedComp engForConnectedComponent;
        private EngineForCycles engForCycles;
        private HierarchicGraph mTree;

        public HierarchicAnalyzer(HierarchicGraph tree)
        {
            this.engine = new Engine();
            this.engForConnectedComponent = new EngineForConnectedComp();
            this.engForCycles = new EngineForCycles();
            this.mTree = tree;
        }

        public HierarchicGraph Tree
        {
            get { return mTree; }
            set { mTree = value; }
        }

        //Calculate degree distribution of graph.
        public override SortedDictionary<int, int> GetDegreeDistribution()
        {
            return ArrayCntAdjacentCntVertexes(0, 0);
        }

        //Calculate average path of graph.
        public override double GetAveragePath()
        {
            long[] pathsInfo = GetSubgraphsPathInfo(0, 0);
            return 2 * (pathsInfo[0] + pathsInfo[2]) / (Math.Pow(mTree.prime, mTree.degree) *
                (Math.Pow(mTree.prime, mTree.degree) - 1));
        }

        //Calculate clustering coefficient of graph.
        public override SortedDictionary<double, int> GetClusteringCoefficient()
        {
            SortedDictionary<double, int> result = new SortedDictionary<double, int>();

            for (int i = 0; i < Math.Pow(mTree.prime, mTree.degree); i++)
            {
                double dresult = ClusteringCoefficientOfVertex(i);
                dresult = dresult * 10000;
                int iResult = Convert.ToInt32(dresult);
                double r = (double)iResult / 10000;
                if (result.Keys.Contains(r))
                    result[r] += 1;
                else
                    result.Add(r, 1);
            }

            return result;
        }

        //Calculate Eigen values of graph.
        public override ArrayList GetEigenValue()
        {
            return new ArrayList();
        }

        //Calculate count of cycles in 3 lenght of graph.
        public override int GetCycles3()
        {
            return (int)Count3Cycle(0, 0)[1];
        }

        //Calculate diameter of graph.
        public override int GetDiameter()
        {
            int result = 0;
            return result;
        }

        //Calculate distribution of connected subgraph of graph.
        public override SortedDictionary<int, int> GetConnSubGraph()
        {
            return AmountConnectedSubGraphs(0, 0);
        }

        //Calculate count of cycles in 3 lenght based in eigen valu of graph.
        public override int GetCycleEigen3()
        {
            int result = 0;
            return result;
        }

        //Calculate count of cycles in 4 lenght of graph.
        public override int GetCycles4()
        {
            return (int)Count4Cycle(0, 0)[0];
        }

        //Calculate count of cycles in 4 lenght based in eigen valu of graph.
        public override int GetCycleEigen4()
        {
            int result = 0;
            return result;
        }

        //Calculate distribution of minimum paths of graph.
        public override SortedDictionary<int, int> GetMinPathDist()
        {
            SortedDictionary<int, int> result = new SortedDictionary<int, int>();
            return result;
        }

        //Calculate distribution of eigen value of graph.
        public override SortedDictionary<double, int> GetDistEigenPath()
        {
            SortedDictionary<double, int> result = new SortedDictionary<double, int>();
            return result;
        }

        //Calculate distribution of connected subgraph of graph.
        public override SortedDictionary<int, int> GetFullSubGraph()
        {
            SortedDictionary<int, int> result = new SortedDictionary<int, int>();
            return result;
        }

        public override SortedDictionary<int, int> GetCycles(int lowBound, int hightBound)
        {
            SortedDictionary<int, int> result = new SortedDictionary<int, int>();
            for (int i = lowBound; i <= hightBound; ++i)
                result[i] = (int)engForCycles.GetCycleCount(this.mTree, i);

            return result;
        }

        // Not used
        public double AverageDegree(HierarchicGraph tree)
        {
            return tree.countEdgesAllGraph() * 2 / Math.Pow(tree.prime, tree.degree);
        }

        // Not used
        public double MinPathsSum(HierarchicGraph tree)
        {
            long[] pathsInfo = GetSubgraphsPathInfo(0, 0);
            return pathsInfo[0] + pathsInfo[2];
        }

        // Not used
        public double CalcCyclesCount(HierarchicGraph tree, int cycleLength)
        {
            List<int> eigValue = new List<int>();
            CalcEigenValue(tree.treeVector(), tree.prime, eigValue);

            double total = 0;
            foreach (int i in eigValue)
            {
                total += Math.Pow(i, cycleLength);
            }
            return total / (2 * cycleLength);
        }

        // Not used
        public String EigenValues(HierarchicGraph tree)
        {
            List<int> eigVals = new List<int>();
            CalcEigenValue(tree.treeVector(), tree.prime, eigVals);

            return eigVals.ToString();
        }

        // Utilities

        /// <summary>
        /// Returns  <countAdjacent,countVertexes> 
        /// </summary>
        /// <param name="HierarchicGraph"></param>
        /// <returns></returns>
        private SortedDictionary<int, int> ArrayCntAdjacentCntVertexes(int numberNode, int level)
        {
            if (level == mTree.degree)
            {
                SortedDictionary<int, int> returned = new SortedDictionary<int, int>();
                returned[0] = 1;
                return returned;
            }
            else
            {
                BitArray node = mTree.treeNode(level, numberNode);

                SortedDictionary<int, int> arraysReturned = new SortedDictionary<int, int>();
                SortedDictionary<int, int> array = new SortedDictionary<int, int>();
                int powPK = Convert.ToInt32(Math.Pow(mTree.prime, mTree.degree - level - 1));

                for (int i = numberNode * mTree.prime; i < mTree.prime * (numberNode + 1); i++)
                {
                    int nodeNumberi = i - numberNode * mTree.prime;
                    array = ArrayCntAdjacentCntVertexes(i, level + 1);
                    int countAjacentsThisnode = mTree.countConnectedBlocks(node, nodeNumberi);
                    foreach (KeyValuePair<int, int> kvt in array)
                    {
                        int key = kvt.Key + countAjacentsThisnode * powPK;
                        if (arraysReturned.ContainsKey(key))
                            arraysReturned[key] += kvt.Value;
                        else
                            arraysReturned.Add(key, kvt.Value);
                    }

                }
                return arraysReturned;
            }
        }

        /// <summary>
        /// Returns subgraphs path information
        /// </summary>
        /// <returns></returns>
        private long[] GetSubgraphsPathInfo(int level, long nodeNumber)
        {
            //tempArr's and tempinfo's 
            //1 element is current paths, that can't minimized, lengths sum
            //2 temp paths count, that have chance to be minimized
            //3 >2 paths' lengths sum
            long[] tempArr = { 0, 0, 0 };
            long[] tempInfo = { 0, 0, 0 };
            // if it is not a leaf of tree
            if (level < mTree.degree - 1)
            {
                //loop over all child nodes
                for (int i = 0; i < mTree.prime; i++)
                {
                    tempInfo = GetSubgraphsPathInfo(level + 1, nodeNumber * mTree.prime + i);

                    tempArr[0] += tempInfo[0];
                    if (mTree.nodeChildAdjacentsCount(level, nodeNumber, i) > 0)
                    {
                        tempArr[0] += tempInfo[1] * 2;
                    }
                    else
                    {
                        tempArr[1] += tempInfo[1];
                        tempArr[2] += tempInfo[2];
                    }
                }
            }
            //get node min paths sum and other info
            tempInfo = this.engine.FloydMinPath(mTree.nodeMatrix(level, nodeNumber));

            tempArr[0] += tempInfo[0] * Convert.ToInt64(Math.Pow(Math.Pow(mTree.prime, mTree.degree - level - 1), 2));
            tempArr[1] += tempInfo[1] * Convert.ToInt64(Math.Pow(Math.Pow(mTree.prime, mTree.degree - level - 1), 2));
            tempArr[2] += tempInfo[2] * Convert.ToInt64(Math.Pow(Math.Pow(mTree.prime, mTree.degree - level - 1), 2));

            return tempArr;
        }

        private SortedDictionary<int, int> AmountConnectedSubGraphs(int numberNode, int level)
        {
            SortedDictionary<int, int> retArray = new SortedDictionary<int, int>();

            if (level == mTree.degree)
            {
                retArray[1] = 1;
                return retArray;
            }
            BitArray node = mTree.treeNode(level, numberNode);

            bool haveOne = false;
            for (int i = 0; i < mTree.prime; i++)
            {
                if (mTree.countConnectedBlocks(node, i) == 0)
                {
                    SortedDictionary<int, int> array = AmountConnectedSubGraphs(numberNode * mTree.prime + i, level + 1);

                    foreach (KeyValuePair<int, int> kvt in array)
                    {
                        if (retArray.Keys.Contains(kvt.Key))
                            retArray[kvt.Key] += kvt.Value;
                        else
                            retArray.Add(kvt.Key, kvt.Value);
                    }
                }
                else
                    haveOne = true;
            }
            if (haveOne)
            {
                int powPK = Convert.ToInt32(Math.Pow(mTree.prime, mTree.degree - level - 1));
                ArrayList arrConnComp = engForConnectedComponent.getCountConnSGruph(mTree.nodeMatrixList(node), mTree.prime);
                for (int i = 0; i < arrConnComp.Count; i++)
                {
                    int countConnCompi = (int)arrConnComp[i];
                    if (retArray.Keys.Contains(countConnCompi * powPK))
                        retArray[countConnCompi * powPK] += 1;
                    else
                        retArray.Add(countConnCompi * powPK, 1);
                }

            }

            return retArray;
        }

        /// <summary>
        /// Calculats count of cycles with 3 length+
        /// </summary>
        /// <param name="numberNode"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private SortedDictionary<int, double> Count3Cycle(int numberNode, int level)
        {
            SortedDictionary<int, double> retArray = new SortedDictionary<int, double>();
            retArray[0] = 0; // count cycles
            retArray[1] = 0; // count edges

            if (level == mTree.degree)
            {
                return retArray;
            }
            else
            {
                double countCycle = 0;
                double[] countEdge = new double[mTree.prime];
                int countOne = 0;
                double powPK = Math.Pow(mTree.prime, mTree.degree - level - 1);
                BitArray node = mTree.treeNode(level, numberNode);

                for (int i = numberNode * mTree.prime; i < mTree.prime * (numberNode + 1); i++)
                {
                    SortedDictionary<int, double> arr = new SortedDictionary<int, double>();
                    arr = Count3Cycle(i, level + 1);
                    countEdge[i - numberNode * mTree.prime] = arr[1];
                    retArray[0] += arr[0];
                    retArray[1] += arr[1];
                }
                for (int i = 0; i < (mTree.prime * (mTree.prime - 1) / 2); i++)
                {
                    countOne += (node[i]) ? 1 : 0;
                }
                retArray[1] += countOne * powPK * powPK;


                for (int i = numberNode * mTree.prime; i < mTree.prime * (numberNode + 1); i++)
                {
                    for (int j = (i + 1); j < mTree.prime * (numberNode + 1); j++)
                    {
                        if (mTree.isConnectedTwoBlocks(node, i - numberNode * mTree.prime, j - numberNode * mTree.prime))
                        {
                            countCycle += (countEdge[i - numberNode * mTree.prime] + countEdge[j - numberNode * mTree.prime]) * powPK;

                            for (int k = (j + 1); k < mTree.prime * (numberNode + 1); k++)
                            {
                                if (mTree.isConnectedTwoBlocks(node, j - numberNode * mTree.prime, k - numberNode * mTree.prime) 
                                    && mTree.isConnectedTwoBlocks(node, i - numberNode * mTree.prime, k - numberNode * mTree.prime))
                                    countCycle += powPK * powPK * powPK;
                            }
                        }
                    }
                }
                retArray[0] += countCycle;
                return retArray;
            }
        }

        /// <summary>
        /// Calculats count of cycles with 4 length+
        /// </summary>
        /// <param name="numberNode"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private SortedDictionary<int, double> Count4Cycle(int numberNode, int level)
        {
            if (level == mTree.degree)
            {
                SortedDictionary<int, double> arrayRetured = new SortedDictionary<int, double>();
                arrayRetured[0] = 0;//count cycles with 4 length
                arrayRetured[1] = 0;//count way with 1 length
                arrayRetured[2] = 0;//count way with 2 length
                return arrayRetured;
            }
            else
            {

                SortedDictionary<int, SortedDictionary<int, double>> array = new SortedDictionary<int, SortedDictionary<int, double>>();
                SortedDictionary<int, double> arrayRetured = new SortedDictionary<int, double>();
                arrayRetured[0] = 0;//count cycles with 4 length
                arrayRetured[1] = 0;//count way with 1 length
                arrayRetured[2] = 0;//count way with 2 length
                double powPK = Math.Pow(mTree.prime, mTree.degree - level - 1);

                for (int i = numberNode * mTree.prime; i < mTree.prime * (numberNode + 1); i++)
                {
                    array[i] = Count4Cycle(i, level + 1);
                    arrayRetured[0] += array[i][0];
                    arrayRetured[1] += array[i][1];
                    arrayRetured[2] += array[i][2];

                }

                BitArray node = mTree.treeNode(level, numberNode);

                string str = "";
                for (int b = 0; b < node.Length; b++)
                    str += node[b];

                for (int i = numberNode * mTree.prime; i < mTree.prime * (numberNode + 1); i++)
                {

                    arrayRetured[2] += mTree.Factorial(mTree.countConnectedBlocks(node, i - numberNode * mTree.prime) - 1) * powPK * powPK * powPK;

                    for (int j = (i + 1); j < mTree.prime * (numberNode + 1); j++)
                    {

                        if (mTree.isConnectedTwoBlocks(node, i - numberNode * mTree.prime, j - numberNode * mTree.prime))
                        {
                            if (level < mTree.degree)
                            {
                                arrayRetured[0] += array[i][1] * array[j][1];
                                arrayRetured[0] += (array[i][2] + array[j][2]) * powPK;

                                arrayRetured[1] += powPK * powPK;

                                arrayRetured[2] += 2 * powPK * (array[i][1] + array[j][1]);

                            }

                            for (int k = (j + 1); k < mTree.prime * (numberNode + 1); k++)
                            {
                                if (mTree.isConnectedTwoBlocks(node, j - numberNode * mTree.prime, k - numberNode * mTree.prime))
                                {
                                    if (mTree.isConnectedTwoBlocks(node, i - numberNode * mTree.prime, k - numberNode * mTree.prime))
                                    {
                                        arrayRetured[0] += (array[i][1] + array[j][1] + array[k][1]) * powPK * powPK;
                                    }


                                    for (int l = (k + 1); l < mTree.prime * (numberNode + 1); l++)
                                        if (mTree.isConnectedTwoBlocks(node, k - numberNode * mTree.prime, l - numberNode * mTree.prime)
                                            && mTree.isConnectedTwoBlocks(node, i - numberNode * mTree.prime, l - numberNode * mTree.prime))
                                            arrayRetured[0] += powPK * powPK * powPK * powPK;
                                }
                            }
                        }
                    }
                }
                return arrayRetured;
            }
        }

        /// <summary>
        /// Returns clustering coefficient of given graph
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        private double ClusteringCoefficientOfVertex(long vert)
        {
            double sum = 0;
            long adjCount = 0;
            //loop over all levels
            for (int level = mTree.degree - 1; level >= 0; level--)
            {
                //get vertex position in current level
                long vertNodeNum = Convert.ToInt64(Math.Floor(Convert.ToDouble(vert / mTree.prime)));
                int vertNodeInd = Convert.ToInt32(vert % mTree.prime);

                //get vertex adjacent vertexes in current node
                List<int> adjIndexes = mTree.nodeChildAdjacentsArray(level, vertNodeNum, vertNodeInd);

                long levelVertexCount = Convert.ToInt64(Math.Pow(mTree.prime, mTree.degree - level - 1));
                //vertex subtree vertexes with adjacent subtrees vertexes
                long vertexSubTreeWithAdjSubTrees = adjCount * levelVertexCount * adjIndexes.Count;
                sum += vertexSubTreeWithAdjSubTrees;
                //add adjacent vertexes count
                adjCount += levelVertexCount * adjIndexes.Count;
                //adjacent subtrees weights
                for (int i = 0; i < mTree.prime; i++)
                {
                    if (adjIndexes.IndexOf(i) != -1)
                    {
                        sum += mTree.countEdges(vertNodeNum * mTree.prime + i, level + 1);
                    }
                }
                //connectivity of adjacent subtrees
                for (int i = 0; i < mTree.prime; i++)
                {
                    if (adjIndexes.IndexOf(i) != -1)
                    {
                        for (int j = i; j < mTree.prime; j++)
                        {
                            if (i != j && i != vertNodeInd && j != vertNodeInd)
                            {
                                sum += mTree.areAdjacent(level, vertNodeNum, i, j) * Math.Pow(levelVertexCount, 2);
                            }
                        }
                    }
                }

                vert = vertNodeNum;
            }
            double vertClustCoef = 0;
            if (adjCount > 1)
            {
                vertClustCoef = 2 * sum / (adjCount * (adjCount - 1));
            }
            else if (adjCount == 1)
            {
                vertClustCoef = sum;
            }

            return vertClustCoef;
        }

        /// <summary>
        /// Calculates eigen values
        /// </summary>
        /// <param name="bitArr"></param>
        /// <param name="mBase"></param>
        /// <param name="EigValue"></param>
        private void CalcEigenValue(BitArray bitArr, int mBase, List<int> EigValue)
        {
            List<int> basicEigValue = new List<int>(mBase);
            List<int> eigValueE = new List<int>(mBase);
            for (int i = 1; i < mBase; ++i)
            {
                eigValueE.Add(0);
            }
            eigValueE.Add(mBase);
            int bitArrSize = bitArr.Count;
            if (bitArr[0] == false)
            {
                if (bitArr[1] == false)
                {
                    for (int i = 0; i < mBase; ++i)
                    {
                        basicEigValue.Add(0);
                    }
                }
                else
                {
                    for (int i = 0; i < mBase; ++i)
                    {
                        basicEigValue.Add(1);
                    }
                }
            }
            else
            {
                if (bitArr[1] == false)
                {
                    for (int i = 1; i < mBase; ++i)
                    {
                        basicEigValue.Add(-1);
                    }
                    basicEigValue.Add(mBase - 1);
                }
                else
                {
                    for (int i = 1; i < mBase; ++i)
                    {
                        basicEigValue.Add(0);
                    }
                    basicEigValue.Add(mBase);
                }
            }
            int size = mBase;
            BitArray BA = new BitArray(bitArrSize + 1);
            for (int i = 0; i < bitArrSize; ++i)
                BA[i] = bitArr[i];
            BA.Set(bitArrSize, false);
            int x = 1;
            while (x != bitArrSize)
            {
                foreach (int elemE in eigValueE)
                {
                    int t1, t2;
                    if (BA[x] == true)
                        t1 = 1;
                    else
                        t1 = 0;
                    if (BA[x + 1] == true)
                        t2 = 1;
                    else
                        t2 = 0;
                    foreach (int elem in basicEigValue)
                        EigValue.Add(elem * elemE - t1 + t2);
                }
                ++x;
                basicEigValue.Clear();
                basicEigValue.InsertRange(0, EigValue);
                EigValue.Clear();

            }
            EigValue.InsertRange(0, basicEigValue);
        }
    }
}
