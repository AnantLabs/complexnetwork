using System;
using System.Collections;
using System.Collections.Generic;

using CommonLibrary.Model;
using GenericAlgorithms;
using ModelCheck;
using log4net;

namespace Model.HierarchicModel.Realization
{
    // Реализация контейнера (Block-Hierarchic).
    public class HierarchicContainer : AbstractGraphContainer
    {
        // Организация pаботы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(HierarchicContainer));

        // Иерархияеская основа (простое число).
        private int branchIndex = 0;
        // Иерархический уровень (максимальный).
        private int level = 0;
        private const int ARRAY_MAX_SIZE = 2000000000;
        // Иерархическое дерево (специфическое).
        private BitArray[][] treeMatrix;

        // Конструктор по умолчанию для контейнера.
        public HierarchicContainer()
        {
            log.Info("Creating HierarchicContainer default object.");
            treeMatrix = new BitArray[0][];
        }

        // Размер контейнера (число вершин в графе).
        public override int Size 
        {
            get { return (int)Math.Pow(branchIndex, level); }
            set { } // ??
        }

        public int BranchIndex
        {
            get { return branchIndex; }
            set { branchIndex = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public BitArray[][] TreeMatrix
        {
            set { treeMatrix = value; }
        }

        // Строится граф на основе матрицы смежности.
        public override void SetMatrix(string fileName)
        {
            ArrayList matrix = MatrixFileReader.MatrixReader(fileName);

            log.Info("Checking if given matrix is block-hierarchic.");

            // проверка на правильность входной матрицы (она должна быть иерархической)
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
                log.Info("Given matrix is not block-hierarchic.");
                throw new SystemException("Not correct matrix.");
            }
            else
            {
                log.Info("Given matrix is block-hierarchic.");
                branchIndex = checker.BranchIndex;
                level = checker.Level;

                log.Info("Creating HierarchicContainer object from given matrix.");
                treeMatrix = new BitArray[level][];

                // начиная снизу, для каждого уровня создаются и заполняются данные
                int nodeDataLength = branchIndex * (branchIndex - 1) / 2;
                int[] nIndexes = new int[nodeDataLength];
                int[] mIndexes = new int[nodeDataLength];
                for (int gamma = level; gamma > 0; --gamma)
                {
                    // get current level data length and bitArrays count
                    long dataLength = Convert.ToInt64(Math.Pow(branchIndex, gamma - 1) * nodeDataLength);
                    int arrCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dataLength) / ARRAY_MAX_SIZE));

                    treeMatrix[gamma - 1] = new BitArray[arrCount];
                    int j;
                    for (j = 0; j < arrCount - 1; j++)
                    {
                        treeMatrix[gamma - 1][j] = new BitArray(ARRAY_MAX_SIZE);
                    }
                    treeMatrix[gamma - 1][j] = new BitArray(Convert.ToInt32(dataLength - (arrCount - 1) * ARRAY_MAX_SIZE));

                    // fills data for current level nodes
                    // loop over all elements of given level and fill him values
                    int lim1 = branchIndex - 1, lim2 = 0, nt = 0;
                    nIndexes[0] = 0;
                    for (int nIndex = 1; nIndex < branchIndex; ++nIndex)
                    {
                        while (nt < lim1)
                        {
                            if (nIndex == 1)
                                nIndexes[nt] = nIndexes[lim2];
                            else
                                nIndexes[nt] = nIndexes[lim2] +
                                    Convert.ToInt32(Math.Pow(branchIndex, level - gamma));
                            ++nt;
                        }
                        lim2 = lim1 - 1;
                        lim1 = lim1 + branchIndex - 1 - nIndex;
                    }

                    mIndexes[0] = Convert.ToInt32(Math.Pow(branchIndex, level - gamma));
                    int mt = 0;
                    int fIndex = 1;
                    while (mt < nodeDataLength)
                    {
                        for (int i = fIndex; i <= branchIndex - 1; ++i)
                        {
                            mIndexes[mt] = i * mIndexes[0];
                            ++mt;
                        }
                        ++fIndex;
                    }

                    for (int f = 0; f < treeMatrix[gamma - 1].Length; f++)
                    {
                        for (int g = 0; g < treeMatrix[gamma - 1][f].Length; g++)
                        {
                            int currentIndex = g % nodeDataLength;
                            int add = Convert.ToInt32((g / nodeDataLength)) *
                                Convert.ToInt32(Math.Pow(branchIndex, level - gamma + 1));
                            treeMatrix[gamma - 1][f][g] =
                                matrixInList[nIndexes[currentIndex] + add][mIndexes[currentIndex] + add];
                        }
                    }
                }
            }
        }

        // Возвращается матрица смежности, соответсвующая графу.
        public override bool[,] GetMatrix()
        {
            log.Info("Getting matrix from HierarchicContainer object.");

            int vertexCount = (int)Math.Pow(branchIndex, level);
            bool[,] matrix = new bool[vertexCount, vertexCount];

            for (int i = 0; i < vertexCount; ++i)
            {
                for (int j = 0; j < i; ++j)
                {
                    if (i == j)
                        matrix[i, i] = false;
                    else
                        matrix[i, j] = matrix[j, i] = (this[i, j] == 1) ? true : false;
                }
            }

            return matrix;
        }

        public override int[][] GetBranches()
        {
            int[][] branches = new int[Level - 1][];
            for (int i = 0; i < Level - 1; ++i)
            { 
                int levelVertexCount = Convert.ToInt32(Math.Pow(branchIndex, i));
                branches[i] = new int[levelVertexCount];
                for (int j = 0; j < levelVertexCount; ++j)
                {
                    branches[i][j] = branchIndex;
                }
            }

            return branches;
        }

        // Методы не из общего интерфейса.    

        // Возвращает последовательность 0/1 длиной p*(p-1)/2, которой отмечен данный узел дерева.
        // Узел определяется номером уровня l (из диапазоне [0, k-1])
        // и номером узла на данном уровне (из диапазона [0, pow(p,l) - 1]).
        public BitArray TreeNode(int l, long n)
        {
            // проверка параметров на правильность
            if (l < 0 || l >= level)
                throw new SystemException("Wrong parameter (number of level).");
            if (n < 0 || n >= Math.Pow(branchIndex, l))
                throw new SystemException("Wrong parameter (number of node).");

            int resultSize = branchIndex * (branchIndex - 1) / 2;
            BitArray result = new BitArray(resultSize);

            long i = n * resultSize;
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
                result[counter] = treeMatrix[l][ind][j];
                counter++;
            }

            for (int j = 0; j < secArray; j++)
            {
                result[counter] = treeMatrix[l][ind + 1][j];
                counter++;
            }

            return result;
        }

        // Возвращает матрицу 0/1 размером p Х p, которая определяет связность данного узла дерева.
        // Узел определяется номером уровня l (из диапазоне [0, k-1])
        // и номером узла на данном уровне (из диапазона [0, pow(p,l) - 1]).
        public int[,] NodeMatrix(int l, long n)
        {
            // проверка параметров на правильность
            if (l < 0 || l >= level)
                throw new SystemException("Wrong parameter (number of level).");
            if (n < 0 || n >= Math.Pow(branchIndex, l))
                throw new SystemException("Wrong parameter (number of node).");

            int[,] result = new int[branchIndex, branchIndex];
            long i = n * branchIndex * (branchIndex - 1) / 2;
            int ind = Convert.ToInt32(Math.Floor(Convert.ToDouble(i / ARRAY_MAX_SIZE)));
            int rangeSt = Convert.ToInt32(i - ind * ARRAY_MAX_SIZE);
            int rangeEnd = rangeSt + branchIndex * (branchIndex - 1) / 2;
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
                result[counterX, counterY] = (treeMatrix[l][ind][j] ? 1 : 0);
                result[counterY, counterX] = (treeMatrix[l][ind][j] ? 1 : 0);
                counterX++;
                if (counterX == branchIndex)
                {
                    counterX = counterY + 2;
                    counterY++;
                }
            }

            for (int j = 0; j < secArray; j++)
            {
                result[counterX, counterY] = (treeMatrix[l][ind + 1][j] ? 1 : 0);
                result[counterY, counterX] = (treeMatrix[l][ind + 1][j] ? 1 : 0);
                counterX++;
                if (counterX == branchIndex)
                {
                    counterX = 0;
                    counterY++;
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
            for (int i = 0; i < branchIndex; i++)
                matrixList.Add(i, new ArrayList());
            for (int i = 0; i < branchIndex - 1; i++)
            {
                int s = i + 1;
                for (int j = i * (branchIndex - (i - 1) - 1) + i * (i - 1) / 2; 
                    j < (i + 1) * (branchIndex - i - 1) + i * (i + 1) / 2; 
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
        /// Возвраэает число смежных дочерных узлов данного узла.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="nodeNumber"></param>
        /// <returns></returns>
        public int NodeChildAdjacentsCount(int level, long nodeNumber, int childNumber)
        {
            BitArray tempNode = TreeNode(level, nodeNumber);

            int tempCount = 0, j = 0;

            //adds values until current child part 
            for (int i = 1; i <= childNumber; i++)
            {
                tempCount += (tempNode[j + childNumber - i] ? 1 : 0);
                j += branchIndex - i;
            }

            //adds child part values
            int curChildEnd = j + branchIndex - childNumber - 1;
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
                j += branchIndex - i;
            }
            //adds child part values
            int curChildSt = j;
            int curChildEnd = j + branchIndex - childNumber - 1;
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
                ind += branchIndex - i - 1;
                i++;
            }
            //ind += branchIndex - vert1 - 1;
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

                for (int i = 0; i < (branchIndex * (branchIndex - 1) / 2); i++)
                {
                    res += (node[i]) ? 1 : 0;
                }
                double t = Math.Pow(branchIndex, level - l - 1);
                result = res * t * t;

                for (long i = n * branchIndex; i < branchIndex * (n + 1); ++i)
                {
                    result += CountEdges(i, l + 1); // - n * branchIndex
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
                double powPK = Math.Pow(branchIndex, level - l - 1);

                for (int i = nodeNumber * branchIndex; i < (nodeNumber + 1) * branchIndex; ++i)
                {
                    result += CountEdges2(i, l + 1);

                    for (int j = i + 1; j < (nodeNumber + 1) * branchIndex; ++j)
                    {
                        if (IsConnectedTwoBlocks(node, i - nodeNumber * branchIndex, j - nodeNumber * branchIndex))
                        {
                            result += (CountEdges(i, l + 1) + CountEdges(j, l + 1)) * powPK * powPK * powPK;

                            for (int k = j + 1; k < (nodeNumber + 1) * branchIndex; ++k)
                            {
                                if (IsConnectedTwoBlocks(node, j - nodeNumber * branchIndex, k - nodeNumber * branchIndex))
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
            for (int i = 0; i < treeMatrix.Length; i++)
            {
                for (int j = 0; j < treeMatrix[i].Length; j++)
                    for (int h = 0; h < treeMatrix[i][j].Length; h++)
                    {
                        countOne += (treeMatrix[i][j][h]) ? 1 : 0;
                    }
                double t = Math.Pow(branchIndex, level - i - 1);
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
                index += branchIndex - k;
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
            for (int i = branchIndex * nodeNumber; i < branchIndex * (nodeNumber + 1) - 1; ++i)
            {
                for (int j = i + 1; j < branchIndex * (nodeNumber + 1); ++j)
                {
                    if(IsConnectedTwoBlocks(node, i - branchIndex * nodeNumber, j - branchIndex * nodeNumber))
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
            for (int j = branchIndex * nodeNumber; j < branchIndex * (nodeNumber + 1); ++j)
            {
                if (IsConnectedTwoBlocks(node, i, j - branchIndex * nodeNumber))
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
                findex += branchIndex - s;
                s++;
            }

            int endindex = findex + (branchIndex - s);
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

            for (int i = 0; i < treeMatrix.Length; i++)
            {
                vector[i] = treeMatrix[i][0][0];
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
                return TreeIndex(vertexNumber, levelNumber + 1) / branchIndex;
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
            int numberOfGroup1 = Convert.ToInt32(v1 / branchIndex);
            int numberOfGroup2 = Convert.ToInt32(v2 / branchIndex);
            while (numberOfGroup1 != numberOfGroup2)
            {
                v1 = numberOfGroup1;
                v2 = numberOfGroup2;
                numberOfGroup1 = Convert.ToInt32(numberOfGroup1 / branchIndex);
                numberOfGroup2 = Convert.ToInt32(numberOfGroup2 / branchIndex);
                --currentLevel;
            }

            BitArray currentNode = TreeNode(currentLevel, numberOfGroup1);
            if (currentNode[AdjacentIndex(v1 % branchIndex, v2 % branchIndex)] == true)
                return 1;

            int tempCurrentLevel = currentLevel, vertexIndex, vI;
            while (0 != tempCurrentLevel)
            {
                vertexIndex = TreeIndex(vertex1, tempCurrentLevel - 1);
                vI = TreeIndex(vertex1, tempCurrentLevel) % branchIndex;
                if (Links(vI, vertexIndex, tempCurrentLevel - 1) >= 1)
                    return 2;

                --tempCurrentLevel;
            }

            int[,] nodeMatrix = NodeMatrix(currentLevel, numberOfGroup1);
            int[,] distances = Engine.MinPath(nodeMatrix);
            if (distances[v1 % branchIndex, v2 % branchIndex] != int.MaxValue)
                return distances[v1 % branchIndex, v2 % branchIndex];

            return -1;
        }

        // Закрытая часть класса (не из общего интерфейса).

        // Возвращает 1, если данные вершины соединены, и 0 - в обратном случае.
        // Номера вершин задаются из диапазона [0, pow(p,k) - 1].
        private int this[int v1, int v2]
        {
            get
            {
                int currentLevel = level - 1;
                // проверка на принадлежение к одному поддереву (для данных вершин)
                // поднимаемся по уровням до того уровна, где они будут принадлежать одному поддереву
                int numberOfGroup1 = Convert.ToInt32(v1 / branchIndex);
                int numberOfGroup2 = Convert.ToInt32(v2 / branchIndex);
                while (numberOfGroup1 != numberOfGroup2)
                {
                    v1 = numberOfGroup1;
                    v2 = numberOfGroup2;
                    numberOfGroup1 = Convert.ToInt32(numberOfGroup1 / branchIndex);
                    numberOfGroup2 = Convert.ToInt32(numberOfGroup2 / branchIndex);
                    --currentLevel;
                }

                BitArray currentNode = TreeNode(currentLevel, numberOfGroup1);
                int index = AdjacentIndex(v1 % branchIndex, v2 % branchIndex);
                return Convert.ToInt32(currentNode[index]);
            }
        }

        // Возвращает индекс того бита (в соответствующей битовой последовательности), 
        // который определяет связность данных узлов.
        // Номера узлов задаются из диапазона [0, p-1]. 
        private int AdjacentIndex(int v1, int v2)
        {
            if (v1 == v2)
            {
                return 0;
            }

            int result = 0;
            for (int i = 1; i <= v1; i++)
            {
                result += (branchIndex - i);
            }
            --result;
            result += v2 - v1;

            return result;
        }

        // Вывод иерархического дерева (рекурсивно). Не используется.
        private void printTree()
        {
            for (int i = 0; i < level; i++)
            {
                for (int k = 0; k < treeMatrix[i].Length; k++)
                {
                    for (int j = 0; j < treeMatrix[i][k].Length; j++)
                    {
                        Console.Write((treeMatrix[i][k][j] ? 1 : 0));
                    }
                }
                Console.WriteLine("");
            }
        }
    }
}
