using System;
using System.Collections;
using System.Collections.Generic;

using CommonLibrary.Model;
using ModelCheck;
using log4net;

namespace Model.HierarchicModel.Realization
{
    // Реализация контейнера (Block-Hierarchic).
    public class HierarchicContainer : IGraphContainer
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
        public int Size 
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
        public void SetMatrix(ArrayList matrix)
        {
            log.Info("Checking if given matrix is block-hierarchic.");
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
                log.Info("Creating HierarchicContainer object from given matrix.");
            }
        }

        // Возвращается матрица смежности, соответсвующая графу.
        public bool[,] GetMatrix()
        {
            log.Info("Getting matrix from HierarchicContainer object.");

            int vertexCount = (int)Math.Pow(branchIndex, level);
            bool[,] matrix = new bool[vertexCount, vertexCount];

            for (int i = 0; i < vertexCount; ++i)
            {
                for (int j = 0; j < vertexCount; ++j)
                    matrix[i, j] = (this[i, j] == 1) ? true : false;
            }

            return matrix;
        }

        // Методы не из общего интерфейса.    

        /// <summary>
        /// Возвращает матрицу смежности для узла, как одну строку.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public BitArray TreeNode(int level, long number)
        {
            BitArray tempNode = new BitArray(branchIndex * (branchIndex - 1) / 2);
            long i = number * branchIndex * (branchIndex - 1) / 2;
            int ind = Convert.ToInt32(Math.Floor(Convert.ToDouble(i / ARRAY_MAX_SIZE)));
            int rangeSt = Convert.ToInt32(i - ind * ARRAY_MAX_SIZE);
            int rangeEnd = rangeSt + branchIndex * (branchIndex - 1) / 2;
            int secArray = 0;

            if (rangeEnd > ARRAY_MAX_SIZE)
            {
                secArray = rangeEnd - ARRAY_MAX_SIZE;
                rangeEnd = ARRAY_MAX_SIZE - 1;
            }
            int counter = 0;
            for (int j = rangeSt; j < rangeEnd; j++)
            {
                tempNode[counter] = treeMatrix[level][ind][j];
                counter++;
            }

            for (int j = 0; j < secArray; j++)
            {
                tempNode[counter] = treeMatrix[level][ind + 1][j];
                counter++;
            }

            return tempNode;
        }

        /// <summary>
        /// Возвращает матрицу смежности для узла.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public int[,] NodeMatrix(int level, long number)
        {
            int[,] tempNode = new int[branchIndex, branchIndex];
            long i = number * branchIndex * (branchIndex - 1) / 2;
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
                tempNode[counterX, counterY] = (treeMatrix[level][ind][j] ? 1 : 0);
                tempNode[counterY, counterX] = (treeMatrix[level][ind][j] ? 1 : 0);
                counterX++;
                if (counterX == branchIndex)
                {
                    counterX = counterY + 2;
                    counterY++;
                }
            }

            for (int j = 0; j < secArray; j++)
            {
                tempNode[counterX, counterY] = (treeMatrix[level][ind + 1][j] ? 1 : 0);
                tempNode[counterY, counterX] = (treeMatrix[level][ind + 1][j] ? 1 : 0);
                counterX++;
                if (counterX == branchIndex)
                {
                    counterX = 0;
                    counterY++;
                }
            }

            return tempNode;
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

        /// <summary>
        /// Возвращает число ребер в графе.
        /// </summary>
        /// <param name="numberNode"></param>
        /// <param name="level"></param>
        /// <returns>count edges</returns>
        public double CountEdges(long numberNode, int level)
        {
            if (level < 0)
            {
                return 0;
            }
            if (level == this.level)
            {
                return 0;
            }
            else
            {
                int countOne = 0;
                BitArray node = TreeNode(level, numberNode);

                for (int i = 0; i < (branchIndex * (branchIndex - 1) / 2); i++)
                {
                    countOne += (node[i]) ? 1 : 0;
                }
                double t = Math.Pow(branchIndex, level - level - 1);
                double count = countOne * t * t;

                for (long i = numberNode * branchIndex; i < branchIndex * (numberNode + 1); i++)
                {
                    count += CountEdges(i, level + 1);
                }
                return count;
            }
        }

        /// <summary>
        /// Возвращает число ребер для всего графа.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Возвращает истину, если первый данный блок соединен со вторым данным блоком.
        /// </summary>
        /// <param name="node">nodes data</param>
        /// <param name="number1">number of the first block</param>
        /// <param name="number2">number of the second block</param>
        /// <returns></returns>
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

        // Закрытая часть класса (не из общего интерфейса).

        /// <summary>
        /// Возвращает 1, если данные вершины соединены, и 0 - в обратном случае.
        /// </summary>
        /// <param name="vertex1"></param>
        /// <param name="vertex2"></param>
        /// <returns></returns>
        private int this[int v1, int v2]
        {
            get
            {
                if (v1 == v2)
                    return 0;
                if (v1 > v2)
                {
                    int temp = v1;
                    v1 = v2;
                    v2 = temp;
                }

                int currentLevel = level - 1;
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

        /// <summary>
        /// Возвращает вершину, где данные вершины связываются.
        /// </summary>
        /// <param name="vert1"></param>
        /// <param name="vert2"></param>
        /// <returns></returns>
        private int AdjacentIndex(int v1, int v2)
        {
            if (v1 == v2)
            {
                return 0;
            }
            int tempInd = 0;
            for (int i = 1; i <= v1; i++)
            {
                tempInd += (branchIndex - i);
            }
            --tempInd;
            tempInd += v2 - v1;

            return tempInd;
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
