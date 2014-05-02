using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using CommonLibrary.Model;
using GenericAlgorithms;
using log4net;
using Model.HierarchicModel.Realization;

namespace Model.NonRegularHierarchicModel.Realization
{
    // Реализация контейнера (Block-Hierarchic Non Regular).
    public class NonRegularHierarchicContainer : AbstractGraphContainer
    {
        // Организация pаботы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(NonRegularHierarchicContainer));

        // Иерархияеская основа (простое число).
        private int branchIndex = 0;
        // Иерархический уровень (максимальный).
        private int level = 0;
        private int[][] branches;
        private int vertices = 0;
        private const int ARRAY_MAX_SIZE = 2000000000;
        // Иерархическое дерево (специфическое).
        private BitArray[][] treeMatrix;

        // Конструктор по умолчанию для контейнера.
        public NonRegularHierarchicContainer()
        {
            log.Info("Creating NonRegularHierarchicContainer default object.");
            treeMatrix = new BitArray[0][];
            branches = new int[0][];
        }

        // Размер контейнера (число вершин в графе).
        public override int Size
        {
            get 
            {
                if (vertices == 0)
                {
                    int vertexCount = 0;
                    for (int i = 0; i < Branches[level - 1].Length; ++i)
                    {
                        for (int j = 0; j < Branches[level - 1][i]; ++j)
                        {
                            ++vertexCount;
                        }
                    }
                    vertices = vertexCount;
                }
                return vertices;
            }
            set { } 
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

        public int[][] Branches
        {
            get { return branches; }
            set { branches = value; }
        }

        public int Vertices
        {
            set { vertices = value; }
        }

        // Строится граф на основе матрицы смежности.
        public override void SetMatrix(string fileName)
        {
            ArrayList branch = MatrixFileReader.BranchesReader(fileName.Insert(fileName.Length - 4, "_branches"));
            ArrayList matrix = MatrixFileReader.MatrixReader(fileName);

            log.Info("Creating NonRegularHierarchicContainer object from given matrix.");

            // Fill this.branches from branch
            this.branches = new int[branch.Count][];
            ArrayList arr;
            for (int i = 0; i < branch.Count; ++i)
            {
                arr = (ArrayList)branch[i];
                this.branches[i] = new int[arr.Count];
                for (int j = 0; j < arr.Count; ++j)
                    this.branches[i][j] = (int)arr[j];
            }

            this.level = branch.Count;

            // Move the matrix to List

            List<List<bool>> matrixInList = new List<List<bool>>();
            for (int i = 0; i < matrix.Count; ++i)
            {
                arr = (ArrayList)matrix[i];
                matrixInList.Add(new List<bool>());
                for (int j = 0; j < arr.Count - 1; ++j)
                    matrixInList[i].Add((bool)arr[j]);
            }

            // Create empty TreeMatrix

            this.treeMatrix = new BitArray[Level][];
            for (int i = 0; i < level; ++i)
            {
                long dataLength = 0;
                for (int j = 0; j < branches[i].Length; ++j)
                {
                    int nodeDataLength = branches[i][j];
                    dataLength += nodeDataLength * (nodeDataLength - 1) / 2;
                }
                int arrCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dataLength) / ARRAY_MAX_SIZE));

                treeMatrix[i] = new BitArray[arrCount];
                if (arrCount > 0)
                {
                    int t;
                    for (t = 0; t < arrCount - 1; ++t)
                    {
                        treeMatrix[i][t] = new BitArray(ARRAY_MAX_SIZE);
                    }
                    treeMatrix[i][t] = new BitArray(Convert.ToInt32(dataLength - (arrCount - 1) * ARRAY_MAX_SIZE));
                }
            }

            // Fill the TreeMatrix from matrixInList

            for (int i = 0; i < matrixInList.Count; ++i)
            {
                for (int j = i + 1; j < matrixInList[i].Count; ++j)
                {
                    int currentLevel = level;
                    int numberOfGroup1 = 0, numberOfGroup2 = 0;
                    int v1 = i, v2 = j; 
                    do
                    {
                        --currentLevel;
                        numberOfGroup1 = TreeIndexStep(v1, currentLevel);
                        numberOfGroup2 = TreeIndexStep(v2, currentLevel);

                        if (numberOfGroup1 != numberOfGroup2)
                        {
                            v1 = numberOfGroup1;
                            v2 = numberOfGroup2;
                        }

                    } while (numberOfGroup1 != numberOfGroup2);

                    int branchSize = branches[currentLevel][numberOfGroup1];
                    int index = AdjacentIndex(branchSize, v1 % branchSize, v2 % branchSize);
                    int resultSize = branchSize * (branchSize - 1) / 2;

                    long count = 0;
                    for (int t = 0; t < numberOfGroup1; ++t)
                    {
                        count += branches[currentLevel][t] * (branches[currentLevel][t] - 1) / 2;
                    }

                    int ind = Convert.ToInt32(Math.Floor(Convert.ToDouble(count / ARRAY_MAX_SIZE)));
                    int rangeSt = Convert.ToInt32(count - ind * ARRAY_MAX_SIZE);
                    int rangeEnd = rangeSt + resultSize;
                    int secArray = 0;

                    if (rangeEnd > ARRAY_MAX_SIZE)
                    {
                        secArray = rangeEnd - ARRAY_MAX_SIZE;
                        rangeEnd = ARRAY_MAX_SIZE - 1;
                    }

                    int counter1 = 0;
                    bool result = false;
                    for (int t = rangeSt; t < rangeEnd; t++)
                    {
                        if (counter1 == index)
                        {
                            treeMatrix[currentLevel][ind][t] = matrixInList[i][j];
                            result = true;
                        }
                        counter1++;
                    }

                    if (result == false)
                    {
                        for (int t = 0; t < secArray; t++)
                        {
                            treeMatrix[currentLevel][ind + 1][t] = matrixInList[i][j];
                            counter1++;
                        }
                    }
                }
            }
        }

        // Возвращается матрица смежности, соответсвующая графу.
        public override bool[,] GetMatrix()
        {
            log.Info("Getting matrix from NonRegularHierarchicContainer object.");

            int vertexCount = Size;
            bool[,] matrix = new bool[vertexCount, vertexCount];

            for (int i = 0; i < vertexCount; ++i)
            {
                matrix[i, i] = false;
                for (int j = 0; j < i; ++j)
                {
                    matrix[i, j] = matrix[j, i] = ((this[j, i] == 1) ? true : false);
                }
            }

            return matrix;
        }

        public override int[][] GetBranches()
        {
            return Branches;
        }

        // Закрытая часть класса (не из общего интерфейса).

        // Возвращает 1, если данные вершины соединены, и 0 - в обратном случае.
        // Номера вершин задаются из диапазона [0, Size - 1].
        private int this[int v1, int v2]
        {
            get
            {
                // проверка на принадлежение к одному поддереву (для данных вершин)
                // поднимаемся по уровням до того уровна, где они будут принадлежать одному поддереву
                int numberOfGroup1 = 0, numberOfGroup2 = 0;
                int currentLevel = level;
                do
                {
                    --currentLevel;
                    numberOfGroup1 = TreeIndexStep(v1, currentLevel);
                    numberOfGroup2 = TreeIndexStep(v2, currentLevel);
                 
                    if (numberOfGroup1 != numberOfGroup2)
                    {
                        v1 = numberOfGroup1;
                        v2 = numberOfGroup2;
                    }

                } while (numberOfGroup1 != numberOfGroup2);

                int branchSize = branches[currentLevel][numberOfGroup1];
                BitArray currentNode = TreeNode(currentLevel, numberOfGroup1);
                int index = AdjacentIndex(branchSize, v1 % branchSize, v2 % branchSize);
                return Convert.ToInt32(currentNode[index]);
            }
        }

        public BitArray TreeNode(int l, long n)
        {
            // проверка параметров на правильность
            if (l < 0 || l > level - 1)
                throw new SystemException("Wrong parameter (number of level).");

            int branchesCount = branches[l][n];
            int resultSize = branchesCount * (branchesCount - 1) / 2;
            BitArray result = new BitArray(resultSize);

            long count = 0;
            for (int i = 0; i < n; ++i)
            {
                count += branches[l][i] * (branches[l][i] - 1) / 2;
            }
            
            int ind = Convert.ToInt32(Math.Floor(Convert.ToDouble(count / ARRAY_MAX_SIZE)));
            int rangeSt = Convert.ToInt32(count - ind * ARRAY_MAX_SIZE);
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

        // Возвращает индекс того бита (в соответствующей битовой последовательности), 
        // который определяет связность данных узлов.
        private int AdjacentIndex(int branchSize, int v1, int v2)
        {
            if (branchSize == 2)
            {
                return 0;
            }

            if (v1 == v2)
            {
                return 0;
            }

            int result = 0;
            for (int i = 1; i <= v1; i++)
            {
                result += (branchSize - i);
            }
            --result;
            result += v2 - v1;

            return result;
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
            
            // проверка на принадлежение к одному поддереву (для данных вершин)
            // поднимаемся по уровням до того уровна, где они будут принадлежать одному поддереву
            int numberOfGroup1 = 0, numberOfGroup2 = 0;
            int currentLevel = level;
            do
            {
                --currentLevel;
                numberOfGroup1 = TreeIndexStep(v1, currentLevel);
                numberOfGroup2 = TreeIndexStep(v2, currentLevel);

                if (numberOfGroup1 != numberOfGroup2)
                {
                    v1 = numberOfGroup1;
                    v2 = numberOfGroup2;
                }

            } while (numberOfGroup1 != numberOfGroup2);

            int branchSize = branches[currentLevel][numberOfGroup1];
            BitArray currentNode = TreeNode(currentLevel, numberOfGroup1);
            if (currentNode[AdjacentIndex(branchSize, v1 % branchSize, v2 % branchSize)] == true)
                return 1;

            int tempCurrentLevel = currentLevel, vertexIndex, vI;
            while (0 < tempCurrentLevel)
            {
                vertexIndex = TreeIndex(vertex1, tempCurrentLevel - 1);
                vI = TreeIndex(vertex1, tempCurrentLevel);
                if (Links(vI, vertexIndex, tempCurrentLevel - 1) > 0)
                    return 2;

                --tempCurrentLevel;
            }
        
            int[,] nodeMatrix = NodeMatrix(currentLevel, numberOfGroup1);
            int[,] distances = Engine.MinPath(nodeMatrix);
            if (distances[v1 % branchSize, v2 % branchSize] != int.MaxValue)
                return distances[v1 % branchSize, v2 % branchSize];

            return -1;
        }


        // Возвращает номер дерева данного уровня (levelNumber), 
        // листом которого является данная вершина (vertexNumber).
        public int TreeIndex(int vertexNumber, int levelNumber)
        {
            if (levelNumber == level)
            {
                return vertexNumber;
            }
            else
            {
                int currentLevel = level;
                do
                {
                    --currentLevel;
                    vertexNumber = TreeIndexStep(vertexNumber, currentLevel);
                }
                while (currentLevel != levelNumber);
                return vertexNumber;
            }
        }

        // Возвращает номер дерева данного уровня (levelNumber), 
        // от которого выходит дерево под номером (vertexNumber) уровня (levelNumber + 1).
        public int TreeIndexStep(int vertexNumber, int levelNumber)
        {
            if (levelNumber > level - 1)
                throw new SystemException("Wrong parameter (number of level).");
            int counter = 0;
            bool found = false;
            for (int i = 0; i < branches[levelNumber].Length; ++i)
            {
                for (int j = 0; j < branches[levelNumber][i]; ++j)
                {
                    if (counter == vertexNumber)
                    {
                        vertexNumber = i;
                        found = true;
                        break;
                    }
                    ++counter;
                }
                if (found == true)
                {
                    break;
                }
            }
            return vertexNumber;
        }

        // Возвращает число непосредственных соединений между child и остальными поддеревьями 
        // данного номера на данном уровне родителя.
        public int Links(int child, int parent, int parentLevel)
        {
            int result = 0;
            int branchSize = branches[parentLevel][parent];
            if (branchSize == 1)
            {
                return 0;
            }
            BitArray node = TreeNode(parentLevel, parent);
            int branchStartPnt = FindBranches(parentLevel, parent);
            int childIndex = (child - branchStartPnt) % branchSize;
            for (int i = 0; i < branchSize; ++i)
            {
                if (AreConnectedTwoBlocks(node, branchSize, childIndex, i))
                {
                    ++result;
                }
            }

            return result;
        }

        // Возвращает true, если поддеревья под номерами i и j непосредственно соединены.
        public bool AreConnectedTwoBlocks(BitArray node, int branchSize, int number1, int number2)
        {
            if (number1 == number2)
            {
                return false;
            }
            // number1 must be the lower number
            if (number1 > number2)
            {
                int k = number2;
                number2 = number1;
                number1 = k;
            }
            // Get the index of two numbers adjacent value
            int index = 0;
            for (int k = 1; k <= number1; k++)
            {
                index += branchSize - k;
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

        // Возвращает число листьев, дерева данного уровня и номера.
        public int CountLeaves(int currentLevel, int numberNode)
        {
            // проверка параметров на правильность
            if (currentLevel < 0 || currentLevel > level)
                throw new SystemException("Wrong parameter (number of level).");

            if (currentLevel == level)
            {
                return 1;
            }

            else
            {
                int branchStartPnt = FindBranches(currentLevel, numberNode);
                int countLeaves = 0;
                int branchSize = branches[currentLevel][numberNode];
                for (int i = 0; i < branchSize; ++i)
                {
                    countLeaves += CountLeaves(currentLevel + 1, branchStartPnt + i);
                }
                return countLeaves;
            }
        }

        // Возвращает начальный номер поддеревьев дерево данного уровня и номера,
        // количиство можно посчетать container.branches[currentLevel][numberNode]
        public int FindBranches(int currentLevel, int numberNode)
        {
            // проверка параметров на правильность
            if (currentLevel < 0 || currentLevel > level - 1)
                throw new SystemException("Wrong parameter (number of level).");
            int counter = 0;
            for (int i = 0; i < numberNode; ++i)
            {
                counter += branches[currentLevel][i];
            }
            return counter;
        }

        /// <summary>
        /// Возвращает число блоков, которые соединены с i блоками.
        /// </summary>
        /// <param name="node">nodes data</param>
        /// <param name="branchSize">size of the branch</param>
        /// <param name="i">number of the  block</param>
        /// <returns></returns>
        public int CountConnectedBlocks(BitArray node, int branchSize, int i)
        {
            i++;
            int s = 1, sum = 0;
            int findex = 0;
            while (s < i)
            {
                sum += Convert.ToInt32(node[findex + i - s - 1]);
                findex += branchSize - s;
                s++;
            }

            int endindex = findex + (branchSize - s);
            s = findex;

            while (s < endindex)
            {
                sum += Convert.ToInt32(node[s]);
                s++;
            }

            return sum;
        }

        // Возвращает матрицу 0/1 размером p Х p, которая определяет связность данного узла дерева.
        // Узел определяется номером уровня l (из диапазоне [0, k-1])
        // и номером узла на данном уровне (из диапазона [0, pow(p,l) - 1]).
        public int[,] NodeMatrix(int l, long n)
        {
            // проверка параметров на правильность
            if (l < 0 || l >= level)
                throw new SystemException("Wrong parameter (number of level).");

            int branchSize = branches[l][n];
            int[,] result = new int[branchSize, branchSize];
            long indexCounter = 0;
            for (int j = 0; j < n; ++j)
            {
                indexCounter += (branches[l][j] * (branches[l][j] - 1) / 2);
            }
            int ind = Convert.ToInt32(Math.Floor(Convert.ToDouble(indexCounter / ARRAY_MAX_SIZE)));
            int rangeSt = Convert.ToInt32(indexCounter - ind * ARRAY_MAX_SIZE);
            int rangeEnd = rangeSt + branchSize * (branchSize - 1) / 2;
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
                if (counterX == branchSize)
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
                if (counterX == branchSize)
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
        public Dictionary<int, ArrayList> nodeMatrixList(BitArray node, int branchSize)
        {
            Dictionary<int, ArrayList> matrixList = new Dictionary<int, ArrayList>();
            for (int i = 0; i < branchSize; ++i)
            {
                matrixList.Add(i, new ArrayList());
            }

            for (int i = 0; i < branchSize - 1; ++i)
            {
                int s = i + 1;
                for (int j = i * (branchSize - (i - 1) - 1) + i * (i - 1) / 2;
                    j < (i + 1) * (branchSize - i - 1) + i * (i + 1) / 2;
                    ++j)
                {
                    if (node[j] == true)
                    {
                        matrixList[i].Add(s);
                        matrixList[s].Add(i);
                    }
                    ++s;
                }
            }

            return matrixList;
        }

        // Возвращает число ребер  дерево данного уровня и номера.
        public int CountEdges(int currentLevel, int numberNode)
        {
            if (currentLevel < 0 || currentLevel > level)
                throw new SystemException("Wrong parameter (number of level).");

            if (currentLevel == level)
            {
                return 0;
            }

            int count = 0;
            int branchSize = branches[currentLevel][numberNode];
            BitArray node = TreeNode(currentLevel, numberNode);
            int StartPoint = FindBranches(currentLevel, numberNode);
            for (int i = 0; i < branchSize; ++i)
            {
                count += CountEdges(currentLevel + 1, i + StartPoint);
                for (int j = i + 1; j < branchSize; ++j)
                {
                    if (AreConnectedTwoBlocks(node, branchSize, i, j))
                    {
                        count += CountLeaves(currentLevel + 1, i + StartPoint) * 
                            CountLeaves(currentLevel + 1, j + StartPoint);
                    }
                }
            }
            return count;
        }

    }
}
