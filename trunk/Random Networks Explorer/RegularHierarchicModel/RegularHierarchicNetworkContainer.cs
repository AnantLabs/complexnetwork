using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NetworkModel;
using NetworkModel.HierarchicEngine;
using ModelChecking;

namespace RegularHierarchicModel
{
    /// <summary>
    /// Implementation of regularly branching block-hierarchic network's container.
    /// </summary>
    class RegularHierarchicNetworkContainer : AbstractHierarchicContainer
    {
        private UInt32 size = 0;
        private UInt16 branchingIndex = 0;
        private UInt16 level = 0;
        private const int ARRAY_MAX_SIZE = 2000000000;
        private BitArray[][] hierarchicTree;

        public RegularHierarchicNetworkContainer()
        {
            hierarchicTree = new BitArray[0][];
        }

        public override UInt32 Size 
        {
            get { return (UInt32)Math.Pow(branchingIndex, level); }
            set
            {
                throw new NotImplementedException();
            }
        }

        public UInt16 BranchingIndex
        {
            get { return branchingIndex; }
            set { branchingIndex = value; }
        }

        public UInt16 Level
        {
            get { return level; }
            set { level = value; }
        }

        public BitArray[][] HierarchicMatrix
        {
            set { hierarchicTree = value; }
        }

        public override void SetMatrix(ArrayList matrix)
        {
            List<List<bool>> matrixInList = new List<List<bool>>();
            ArrayList arr;
            for (int i = 0; i < matrix.Count; ++i)
            {
                arr = (ArrayList)matrix[i];
                matrixInList.Add(new List<bool>());
                for(int j = 0; j < arr.Count - 1; ++j)
                    matrixInList[i].Add((bool)arr[j]);
            }

            HierarchicExactChecker checker = new HierarchicExactChecker();
            if (!checker.IsHierarchic(matrixInList))
            {
                throw new SystemException("Not correct matrix.");
            }
            else
            {
                size = (UInt32)matrix.Count;
                branchingIndex = (UInt16)checker.BranchIndex;
                level = (UInt16)checker.Level;
                hierarchicTree = new BitArray[level][];

                // Initializing and filling data for each level, beginning from root.
                int nodeDataLength = branchingIndex * (branchingIndex - 1) / 2;
                int[] nIndexes = new int[nodeDataLength];
                int[] mIndexes = new int[nodeDataLength];
                for (int gamma = level; gamma > 0; --gamma)
                {
                    // get current level data length and bitArrays count
                    long levelDataLength = Convert.ToInt64(Math.Pow(branchingIndex, gamma - 1) * nodeDataLength);
                    int arrayCountForLevel = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(levelDataLength) / ARRAY_MAX_SIZE));

                    hierarchicTree[gamma - 1] = new BitArray[arrayCountForLevel];
                    int j;
                    for (j = 0; j < arrayCountForLevel - 1; j++)
                    {
                        hierarchicTree[gamma - 1][j] = new BitArray(ARRAY_MAX_SIZE);
                    }
                    hierarchicTree[gamma - 1][j] = new BitArray(Convert.ToInt32(levelDataLength - (arrayCountForLevel - 1) * ARRAY_MAX_SIZE));

                    // fills data for current level nodes
                    // loop over all elements of given level and fill him values
                    int lim1 = branchingIndex - 1, lim2 = 0, nt = 0;
                    nIndexes[0] = 0;
                    for (int nIndex = 1; nIndex < branchingIndex; ++nIndex)
                    {
                        while (nt < lim1)
                        {
                            if (nIndex == 1)
                                nIndexes[nt] = nIndexes[lim2];
                            else
                                nIndexes[nt] = nIndexes[lim2] +
                                    Convert.ToInt32(Math.Pow(branchingIndex, level - gamma));
                            ++nt;
                        }
                        lim2 = lim1 - 1;
                        lim1 = lim1 + branchingIndex - 1 - nIndex;
                    }

                    mIndexes[0] = Convert.ToInt32(Math.Pow(branchingIndex, level - gamma));
                    int mt = 0;
                    int fIndex = 1;
                    while (mt < nodeDataLength)
                    {
                        for (int i = fIndex; i <= branchingIndex - 1; ++i)
                        {
                            mIndexes[mt] = i * mIndexes[0];
                            ++mt;
                        }
                        ++fIndex;
                    }

                    for (int f = 0; f < hierarchicTree[gamma - 1].Length; f++)
                    {
                        for (int g = 0; g < hierarchicTree[gamma - 1][f].Length; g++)
                        {
                            int currentIndex = g % nodeDataLength;
                            int add = Convert.ToInt32((g / nodeDataLength)) *
                                Convert.ToInt32(Math.Pow(branchingIndex, level - gamma + 1));
                            hierarchicTree[gamma - 1][f][g] =
                                matrixInList[nIndexes[currentIndex] + add][mIndexes[currentIndex] + add];
                        }
                    }
                }
            }
        }

        public override bool[,] GetMatrix()
        {
            bool[,] matrix = new bool[Size, Size];

            for (int i = 0; i < Size; ++i)
            {
                for (int j = 0; j < Size; ++j)
                    matrix[i, j] = (this[i, j] == 1) ? true : false;
            }

            return matrix;
        }

        public override int[][] GetBranches()
        {
            int[][] branches = new int[Level][];
            for (int i = 0; i < Level; ++i)
            { 
                int levelVertexCount = Convert.ToInt32(Math.Pow(branchingIndex, i));
                branches[i] = new int[levelVertexCount];
                for (int j = 0; j < levelVertexCount; ++j)
                {
                    branches[i][j] = branchingIndex;
                }
            }

            return branches;
        }

        /// <summary>
        /// Retrieves mark of specified node of block-hierarchical tree.
        /// </summary>
        /// <param name="currentLevel">Index of level of specified node.</param>
        /// <note>currentLevel must be in [0, level - 1] range.</note>
        /// <param name="currentNodeNumber">Index of specified node in currentLevel.</param>
        /// <note>currentNodeNumber must be in [0, pow(branchingIndex, currentLevel) - 1] range.</note>
        /// <returns>Sequence of {0, 1} values. Length is branchingIndex*(branchingIndex-1)/2 </returns>
        public BitArray TreeNode(int currentLevel, long currentNodeNumber)
        {
            if (currentLevel < 0 || currentLevel >= level)
                throw new SystemException("Wrong parameter - currentLevel.");
            if (currentNodeNumber < 0 || currentNodeNumber >= Math.Pow(branchingIndex, currentLevel))
                throw new SystemException("Wrong parameter - currentNodeNumber.");

            int resultSize = branchingIndex * (branchingIndex - 1) / 2;
            BitArray result = new BitArray(resultSize);

            long i = currentNodeNumber * resultSize;
            int ind = Convert.ToInt32(Math.Floor(Convert.ToDouble(i / ARRAY_MAX_SIZE)));
            int rangeSt = Convert.ToInt32(i - ind * ARRAY_MAX_SIZE);
            int rangeEnd = rangeSt + resultSize;
            int secArray = 0;

            if (rangeEnd > ARRAY_MAX_SIZE)
            {
                secArray = rangeEnd - ARRAY_MAX_SIZE;
                rangeEnd = ARRAY_MAX_SIZE - 1;
            }

            int counter = 0;
            for (int j = rangeSt; j < rangeEnd; j++)
            {
                result[counter] = hierarchicTree[currentLevel][ind][j];
                counter++;
            }

            for (int j = 0; j < secArray; j++)
            {
                result[counter] = hierarchicTree[currentLevel][ind + 1][j];
                counter++;
            }

            return result;
        }

        /// <summary>
        /// Retrieves the adjacency matrix for specified node of block-hierarchical tree.
        /// </summary>
        /// <param name="currentLevel">Index of level of specified node.</param>
        /// <note>currentLevel must be in [0, level - 1] range.</note>
        /// <param name="currentNodeNumber">Index of specified node in currentLevel.</param>
        /// <note>currentNodeNumber must be in [0, pow(branchingIndex, currentLevel) - 1] range.</note>
        /// <returns>Matrix of {0, 1} values. Size is branchingIndex X branchingIndex. </returns>
        public int[,] NodeMatrix(int currentLevel, long currentNodeNumber)
        {
            if (currentLevel < 0 || currentLevel >= level)
                throw new SystemException("Wrong parameter - currentLevel.");
            if (currentNodeNumber < 0 || currentNodeNumber >= Math.Pow(branchingIndex, currentLevel))
                throw new SystemException("Wrong parameter - currentNodeNumber.");

            int[,] result = new int[branchingIndex, branchingIndex];
            long i = currentNodeNumber * branchingIndex * (branchingIndex - 1) / 2;
            int ind = Convert.ToInt32(Math.Floor(Convert.ToDouble(i / ARRAY_MAX_SIZE)));
            int rangeSt = Convert.ToInt32(i - ind * ARRAY_MAX_SIZE);
            int rangeEnd = rangeSt + branchingIndex * (branchingIndex - 1) / 2;
            int secArray = 0;

            if (rangeEnd > ARRAY_MAX_SIZE)
            {
                secArray = rangeEnd - ARRAY_MAX_SIZE;
                rangeEnd = ARRAY_MAX_SIZE - 1;
            }
            int counterX = 1;
            int counterY = 0;
            for (int j = rangeSt; j < rangeEnd; j++)
            {
                result[counterX, counterY] = (hierarchicTree[currentLevel][ind][j] ? 1 : 0);
                result[counterY, counterX] = (hierarchicTree[currentLevel][ind][j] ? 1 : 0);
                ++counterX;
                if (counterX == branchingIndex)
                {
                    counterX = counterY + 2;
                    ++counterY;
                }
            }

            for (int j = 0; j < secArray; j++)
            {
                result[counterX, counterY] = (hierarchicTree[currentLevel][ind + 1][j] ? 1 : 0);
                result[counterY, counterX] = (hierarchicTree[currentLevel][ind + 1][j] ? 1 : 0);
                ++counterX;
                if (counterX == branchingIndex)
                {
                    counterX = 0;
                    ++counterY;
                }
            }

            return result;
        }

        /// <summary>
        /// Возвращает список матриц смежности узлов.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Dictionary<int, ArrayList> nodeMatrixList(BitArray node)
        {
            Dictionary<int, ArrayList> matrixList = new Dictionary<int, ArrayList>();
            for (int i = 0; i < branchingIndex; i++)
                matrixList.Add(i, new ArrayList());
            for (int i = 0; i < branchingIndex - 1; i++)
            {
                int s = i + 1;
                for (int j = i * (branchingIndex - (i - 1) - 1) + i * (i - 1) / 2; 
                    j < (i + 1) * (branchingIndex - i - 1) + i * (i + 1) / 2; 
                    j++)
                {
                    if (node[j] == true)
                    {
                        matrixList[i].Add(s);
                        matrixList[s].Add(i);
                    }
                    s++;
                }
            }

            return matrixList;
        }

        /// <summary>
        /// Возвращает число смежных дочерных узлов данного узла.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="nodeNumber"></param>
        /// <returns></returns>
        public int NodeChildAdjacentsCount(int currentLevel, 
            long currentNodeNumber, 
            int childNumber)
        {
            BitArray tempNode = TreeNode(currentLevel, currentNodeNumber);

            int tempCount = 0, j = 0;

            //adds values until current child part 
            for (int i = 1; i <= childNumber; i++)
            {
                tempCount += (tempNode[j + childNumber - i] ? 1 : 0);
                j += branchingIndex - i;
            }

            //adds child part values
            int curChildEnd = j + branchingIndex - childNumber - 1;
            while (j < curChildEnd)
            {
                tempCount += (tempNode[j] ? 1 : 0);
                j++;
            }

            return tempCount;
        }

        /// <summary>
        /// Возвращает список смежных дочерных узлов данного узла.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="nodeNumber"></param>
        /// <returns></returns>
        public List<int> NodeChildAdjacentsArray(int level, long nodeNumber, int childNumber)
        {
            BitArray tempNode = TreeNode(level, nodeNumber);

            List<int> tempIndexes = new List<int>();
            int j = 0;
            //adds values until current child part 
            for (int i = 1; i <= childNumber; i++)
            {
                if (tempNode[j + childNumber - i])
                {
                    tempIndexes.Add(i - 1);
                }
                j += branchingIndex - i;
            }
            //adds child part values
            int curChildSt = j;
            int curChildEnd = j + branchingIndex - childNumber - 1;
            while (j < curChildEnd)
            {
                if (tempNode[j])
                {
                    tempIndexes.Add(j - curChildSt + childNumber + 1);
                }
                j++;
            }

            return tempIndexes;
        }

        /// <summary>
        /// Возвращает 1, если данные вершины смежны.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="nodeNumber"></param>
        /// <param name="vert1"></param>
        /// <param name="vert2"></param>
        /// <returns></returns>
        public int AreAdjacent(int level, long nodeNumber, int vert1, int vert2)
        {
            BitArray tempNode = TreeNode(level, nodeNumber);

            if (vert1 > vert2)
            {
                int temp = vert2;
                vert2 = vert1;
                vert1 = temp;
            }
            var i = 0;
            var ind = 0;
            while (i < vert1)
            {
                ind += branchingIndex - i - 1;
                i++;
            }
            //ind += branchingIndex - vert1 - 1;
            if (tempNode[ind + vert2 - vert1 - 1])
            {
                return 1;
            }

            return 0;
        }

        // Возвращает число ребер в данном кластере (определяется по l и nodeNumber).
        // Кластер определяется номером уровня l (из диапазоне [0, k-1])
        // и номером узла на данном уровне n (из диапазона [0, pow(p,l) - 1]).
        public double CountEdges(long n, int l)
        {
            if (l == level)
            {
                return 0;
            }
            else
            {
                double result = 0;
                double res = 0;
                BitArray node = TreeNode(l, n);

                for (int i = 0; i < (branchingIndex * (branchingIndex - 1) / 2); i++)
                {
                    res += (node[i]) ? 1 : 0;
                }
                double t = Math.Pow(branchingIndex, level - l - 1);
                result = res * t * t;

                for (long i = n * branchingIndex; i < branchingIndex * (n + 1); ++i)
                {
                    result += CountEdges(i, l + 1); // - n * branchingIndex
                }

                return result;
            }
        }

        // Возвращает число связей длиной 2 в данном кластере (определяется по l и nodeNumber). ??
        public double CountEdges2(int nodeNumber, int l)
        {
            double result = 0;

            if (l < 0 || l == level)
                return result;
            else
            {
                BitArray node = TreeNode(l, nodeNumber);
                double powPK = Math.Pow(branchingIndex, level - l - 1);

                for (int i = nodeNumber * branchingIndex; i < (nodeNumber + 1) * branchingIndex; ++i)
                {
                    result += CountEdges2(i, l + 1);

                    for (int j = i + 1; j < (nodeNumber + 1) * branchingIndex; ++j)
                    {
                        if (IsConnectedTwoBlocks(node, i - nodeNumber * branchingIndex, j - nodeNumber * branchingIndex))
                        {
                            result += (CountEdges(i, l + 1) + CountEdges(j, l + 1)) * powPK * powPK * powPK;

                            for (int k = j + 1; k < (nodeNumber + 1) * branchingIndex; ++k)
                            {
                                if (IsConnectedTwoBlocks(node, j - nodeNumber * branchingIndex, k - nodeNumber * branchingIndex))
                                {
                                    result += powPK * powPK * powPK;
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        // Возвращает число ребер графа.
        public double CountEdgesAllGraph()
        {
            int countOne = 0;
            double count = 0;
            for (int i = 0; i < hierarchicTree.Length; i++)
            {
                for (int j = 0; j < hierarchicTree[i].Length; j++)
                    for (int h = 0; h < hierarchicTree[i][j].Length; h++)
                    {
                        countOne += (hierarchicTree[i][j][h]) ? 1 : 0;
                    }
                double t = Math.Pow(branchingIndex, level - i - 1);
                count += countOne * t * t;
                countOne = 0;
            }

            return count;
        }

        // Возвращает true, если поддеревья под номерами i и j непосредственно соединены.
        public bool IsConnectedTwoBlocks(BitArray node, int number1, int number2)
        {
            if (number1 == number2)
            {
                return false;
            }
            // number1 must have min number
            if (number1 > number2)
            {
                int k = number2;
                number2 = number1;
                number1 = k;
            }
            // Get the index of two numberes adjacent value
            int index = 0;
            for (int k = 1; k <= number1; k++)
            {
                index += branchingIndex - k;
            }
            index += number2 - number1 - 1;
            if (node[index])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Возвращает число непосредственных соединений между всеми поддеревьями данного номера на данном уровне.
        public int Links(int nodeNumber, int levelNumber)
        {
            int result = 0;
            BitArray node = TreeNode(levelNumber, nodeNumber);
            for (int i = branchingIndex * nodeNumber; i < branchingIndex * (nodeNumber + 1) - 1; ++i)
            {
                for (int j = i + 1; j < branchingIndex * (nodeNumber + 1); ++j)
                {
                    if(IsConnectedTwoBlocks(node, i - branchingIndex * nodeNumber, j - branchingIndex * nodeNumber))
                        ++result;
                }
            }

            return result;
        }

        // Возвращает число непосредственных соединений между i и остальными поддеревьями 
        // данного номера на данном уровне.
        public int Links(int i, int nodeNumber, int levelNumber)
        {
            int result = 0;
            BitArray node = TreeNode(levelNumber, nodeNumber);
            for (int j = branchingIndex * nodeNumber; j < branchingIndex * (nodeNumber + 1); ++j)
            {
                if (IsConnectedTwoBlocks(node, i, j - branchingIndex * nodeNumber))
                    ++result;
            }

            return result;
        }

        /// <summary>
        /// Возвращает число блоков, которые соединены с i блоками.
        /// </summary>
        /// <param name="node">nodes data</param>
        /// <param name="i">number of the  block</param>
        /// <returns></returns>
        public int CountConnectedBlocks(BitArray node, int i)
        {
            i++;
            int s = 1, sum = 0;
            int findex = 0;
            while (s < i)
            {
                sum += Convert.ToInt32(node[findex + i - s - 1]);
                findex += branchingIndex - s;
                s++;
            }

            int endindex = findex + (branchingIndex - s);
            s = findex;

            while (s < endindex)
            {
                sum += Convert.ToInt32(node[s]);
                s++;
            }

            return sum;
        }

        // Вычисление факториала данного числа.
        public double Factorial(double n)
        {
            if (n <= 0) return 0;
            double tempResult = 1;
            for (int i = 1; i <= n; i++)
            {
                tempResult *= i;
            }
            return tempResult;
        }

        /// <summary>
        /// Возвращает столбец дерева по уровням.
        /// </summary>
        /// <returns></returns>
        public BitArray TreeVector()
        {
            BitArray vector = new BitArray(level);

            for (int i = 0; i < hierarchicTree.Length; i++)
            {
                vector[i] = hierarchicTree[i][0][0];
            }

            return vector;
        }

        // Возвращает номер дерева данного уровня (levelNumber), 
        // листом которого является данная вершина (vertexNumber).
        public int TreeIndex(int vertexNumber, int levelNumber)
        {
            if (levelNumber == level)
                return vertexNumber;
            else
                return TreeIndex(vertexNumber, levelNumber + 1) / branchingIndex;
        }

        public int MinimumWay(int v1, int v2)
        {
            int vertex1 = v1, vertex2 = v2;

            if (v1 == v2)
                return 0;
            if (v1 > v2)
            {
                return MinimumWay(v2, v1);
            }
            
            int currentLevel = level - 1;
            // проверка на принадлежение к одному поддереву (для данных вершин)
            // поднимаемся по уровням до того уровна, где они будут принадлежать одному поддереву
            int numberOfGroup1 = Convert.ToInt32(v1 / branchingIndex);
            int numberOfGroup2 = Convert.ToInt32(v2 / branchingIndex);
            while (numberOfGroup1 != numberOfGroup2)
            {
                v1 = numberOfGroup1;
                v2 = numberOfGroup2;
                numberOfGroup1 = Convert.ToInt32(numberOfGroup1 / branchingIndex);
                numberOfGroup2 = Convert.ToInt32(numberOfGroup2 / branchingIndex);
                --currentLevel;
            }

            BitArray currentNode = TreeNode(currentLevel, numberOfGroup1);
            if (currentNode[AdjacentIndex(v1 % branchingIndex, v2 % branchingIndex)] == true)
                return 1;

            int tempCurrentLevel = currentLevel, vertexIndex, vI;
            while (0 != tempCurrentLevel)
            {
                vertexIndex = TreeIndex(vertex1, tempCurrentLevel - 1);
                vI = TreeIndex(vertex1, tempCurrentLevel) % branchingIndex;
                if (Links(vI, vertexIndex, tempCurrentLevel - 1) >= 1)
                    return 2;

                --tempCurrentLevel;
            }

            int[,] nodeMatrix = NodeMatrix(currentLevel, numberOfGroup1);
            int[,] distances = Engine.MinPath(nodeMatrix);
            if (distances[v1 % branchingIndex, v2 % branchingIndex] != int.MaxValue)
                return distances[v1 % branchingIndex, v2 % branchingIndex];

            return -1;
        }
        
        // Возвращает 1, если данные вершины соединены, и 0 - в обратном случае.
        // Номера вершин задаются из диапазона [0, pow(p,k) - 1].
        public int this[int v1, int v2]
        {
            get
            {
                if (v1 == v2)
                {
                    return 0;
                }

                if (v1 > v2)
                {
                    return this[v2, v1];
                }

                int currentLevel = level - 1;
                // проверка на принадлежение к одному поддереву (для данных вершин)
                // поднимаемся по уровням до того уровна, где они будут принадлежать одному поддереву
                int numberOfGroup1 = Convert.ToInt32(v1 / branchingIndex);
                int numberOfGroup2 = Convert.ToInt32(v2 / branchingIndex);
                while (numberOfGroup1 != numberOfGroup2)
                {
                    v1 = numberOfGroup1;
                    v2 = numberOfGroup2;
                    numberOfGroup1 = Convert.ToInt32(numberOfGroup1 / branchingIndex);
                    numberOfGroup2 = Convert.ToInt32(numberOfGroup2 / branchingIndex);
                    --currentLevel;
                }

                BitArray currentNode = TreeNode(currentLevel, numberOfGroup1);
                int index = AdjacentIndex(v1 % branchingIndex, v2 % branchingIndex);
                return Convert.ToInt32(currentNode[index]);
            }
        }

        // Возвращает индекс того бита (в соответствующей битовой последовательности), 
        // который определяет связность данных узлов.
        // Номера узлов задаются из диапазона [0, p-1]. 
        public int AdjacentIndex(int v1, int v2)
        {
            if (v1 == v2)
            {
                return 0;
            }

            int result = 0;
            for (int i = 1; i <= v1; i++)
            {
                result += (branchingIndex - i);
            }
            --result;
            result += v2 - v1;

            return result;
        }

    }
}
