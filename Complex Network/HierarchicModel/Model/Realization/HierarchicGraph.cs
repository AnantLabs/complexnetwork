using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Model.HierarchicModel.Realization
{
    public class HierarchicGraph
    {
        private const int ARRAY_MAX_SIZE = 2000000000;
        private int primeNumber;
        private int maxlevel;
        private BitArray[][] treeMatrix;

        /// <summary>
        /// Constructor of hierarchic graph model tree
        /// </summary>
        /// <param name="primeNumber"></param>
        /// <param name="numberDegree"></param>
        public HierarchicGraph(int primeNumber, int degree, BitArray[][] treeMatrix)
        {

            this.primeNumber = primeNumber;
            this.maxlevel = degree;
            this.treeMatrix = treeMatrix;
        }


        /// <summary>
        /// Print a tree with recursion
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public void printTree()
        {
            //for every level create datas, started with root
            for (int i = 0; i < this.maxlevel; i++)
            {
                for (int k = 0; k < this.treeMatrix[i].Length; k++)
                {
                    for (int j = 0; j < this.treeMatrix[i][k].Length; j++)
                    {
                        Console.Write((this.treeMatrix[i][k][j] ? 1 : 0));
                    }
                }
                Console.WriteLine("");
            }
        }

        /// <summary>
        /// Returns current vertexes are given or not
        /// </summary>
        /// <param name="vertex1"></param>
        /// <param name="vertex2"></param>
        /// <returns></returns>
        public int this[int vertex1, int vertex2]
        {
            get
            {
                if (vertex1 == vertex2)
                {
                    return 0;
                }
                else
                {
                    int rm1, rm2;
                    int prevNum = 0;

                    for (int i = 1; i <= this.maxlevel; i++)
                    {
                        rm1 = Convert.ToInt32(Math.Floor(vertex1 / Math.Pow(this.primeNumber, this.maxlevel - i)));
                        rm2 = Convert.ToInt32(Math.Floor(vertex2 / Math.Pow(this.primeNumber, this.maxlevel - i)));

                        if (rm1 != rm2)
                        {
                            var tempInd = this.adjacentIndex(rm1, rm2);
                            if (tempInd < 0)    // CORRECT
                                return 0;   // CORRECT
                            return Convert.ToInt32(this.treeNode(i - 1, prevNum)[tempInd]);
                        }
                        prevNum = rm1;
                    }
                }
                return 0;
            }
        }

        /// <summary>
        /// Return node connectivity matrix as one row array
        /// </summary>
        /// <param name="level"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public BitArray treeNode(int level, long number)
        {
            BitArray tempNode = new BitArray(this.primeNumber * (this.primeNumber - 1) / 2);
            long i = number * this.primeNumber * (this.primeNumber - 1) / 2;
            int ind = Convert.ToInt32(Math.Floor(Convert.ToDouble(i / ARRAY_MAX_SIZE)));
            int rangeSt = Convert.ToInt32(i - ind * ARRAY_MAX_SIZE);
            int rangeEnd = rangeSt + this.primeNumber * (this.primeNumber - 1) / 2;
            int secArray = 0;

            if (rangeEnd > ARRAY_MAX_SIZE)
            {
                secArray = rangeEnd - ARRAY_MAX_SIZE;
                rangeEnd = ARRAY_MAX_SIZE - 1;
            }
            int counter = 0;
            for (int j = rangeSt; j < rangeEnd; j++)
            {
                tempNode[counter] = this.treeMatrix[level][ind][j];
                counter++;
            }

            for (int j = 0; j < secArray; j++)
            {
                tempNode[counter] = this.treeMatrix[level][ind + 1][j];
                counter++;
            }

            return tempNode;
        }

        /// <summary>
        /// Returns tree node matrix
        /// </summary>
        /// <param name="level"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public int[,] nodeMatrix(int level, long number)
        {
            int[,] tempNode = new int[this.prime, this.prime];
            long i = number * this.primeNumber * (this.primeNumber - 1) / 2;
            int ind = Convert.ToInt32(Math.Floor(Convert.ToDouble(i / ARRAY_MAX_SIZE)));
            int rangeSt = Convert.ToInt32(i - ind * ARRAY_MAX_SIZE);
            int rangeEnd = rangeSt + this.primeNumber * (this.primeNumber - 1) / 2;
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
                tempNode[counterX, counterY] = (this.treeMatrix[level][ind][j] ? 1 : 0);
                tempNode[counterY, counterX] = (this.treeMatrix[level][ind][j] ? 1 : 0);
                counterX++;
                if (counterX == this.prime)
                {
                    counterX = counterY + 2;
                    counterY++;
                }
            }

            for (int j = 0; j < secArray; j++)
            {
                tempNode[counterX, counterY] = (this.treeMatrix[level][ind + 1][j] ? 1 : 0);
                tempNode[counterY, counterX] = (this.treeMatrix[level][ind + 1][j] ? 1 : 0);
                counterX++;
                if (counterX == this.prime)
                {
                    counterX = 0;
                    counterY++;
                }
            }

            return tempNode;
        }
        /// <summary>
        /// Returns tree node matrixList
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Dictionary<int, ArrayList> nodeMatrixList(BitArray node)
        {
            Dictionary<int, ArrayList> matrixList = new Dictionary<int, ArrayList>();
            for (int i = 0; i < prime; i++)
                matrixList.Add(i, new ArrayList());
            for (int i = 0; i < prime - 1; i++)
            {
                int s = i + 1;
                for (int j = i * (prime - (i - 1) - 1) + i * (i - 1) / 2; j < (i + 1) * (prime - i - 1) + i * (i + 1) / 2; j++)
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
        /// Returns that vertex where this vertexes become connected
        /// </summary>
        /// <param name="vert1"></param>
        /// <param name="vert2"></param>
        /// <returns></returns>
        private int adjacentIndex(int vert1, int vert2)
        {
            if (vert1 == vert2)
            {
                return 0;
            }
            else if (vert2 > vert1)
            {
                int tempVert = vert2;
                vert2 = vert1;
                vert1 = tempVert;
            }
            vert1++;
            vert2++;
            int tempInd = 0;
            for (int i = 1; i < vert1; i++)
            {
                tempInd += (this.primeNumber - vert1);
            }
            tempInd += vert2 - vert1;

            return tempInd;
        }

        /// <summary>
        /// Returns node child adjacents count
        /// </summary>
        /// <param name="level"></param>
        /// <param name="nodeNumber"></param>
        /// <returns></returns>
        public int nodeChildAdjacentsCount(int level, long nodeNumber, int childNumber)
        {
            BitArray tempNode = this.treeNode(level, nodeNumber);

            int tempCount = 0;
            int j = 0;
            //adds values until current child part 
            for (int i = 1; i <= childNumber; i++)
            {
                tempCount += (tempNode[j + childNumber - i] ? 1 : 0);
                j += this.prime - i;
            }
            //adds child part values
            int curChildEnd = j + this.prime - childNumber - 1;
            while (j < curChildEnd)
            {
                tempCount += (tempNode[j] ? 1 : 0);
                j++;
            }

            return tempCount;
        }

        /// <summary>
        /// Returns node child adjacents array
        /// </summary>
        /// <param name="level"></param>
        /// <param name="nodeNumber"></param>
        /// <returns></returns>
        public List<int> nodeChildAdjacentsArray(int level, long nodeNumber, int childNumber)
        {
            BitArray tempNode = this.treeNode(level, nodeNumber);

            List<int> tempIndexes = new List<int>();
            int j = 0;
            //adds values until current child part 
            for (int i = 1; i <= childNumber; i++)
            {
                if (tempNode[j + childNumber - i])
                {
                    tempIndexes.Add(i - 1);
                }
                j += this.prime - i;
            }
            //adds child part values
            int curChildSt = j;
            int curChildEnd = j + this.prime - childNumber - 1;
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
        /// Returns 1 when given vertexes are adjacent
        /// </summary>
        /// <param name="level"></param>
        /// <param name="nodeNumber"></param>
        /// <param name="vert1"></param>
        /// <param name="vert2"></param>
        /// <returns></returns>
        public int areAdjacent(int level, long nodeNumber, int vert1, int vert2)
        {
            BitArray tempNode = this.treeNode(level, nodeNumber);

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
                ind += this.prime - i - 1;
                i++;
            }
            //ind += this.prime - vert1 - 1;
            if (tempNode[ind + vert2 - vert1 - 1])
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Returns count of edges of graph
        /// </summary>
        /// <param name="numberNode"></param>
        /// <param name="level"></param>
        /// <returns>count edges</returns>
        public double countEdges(long numberNode, int level)
        {
            if (level < 0)
            {
                return 0;
            }
            if (level == this.maxlevel)
            {
                return 0;
            }
            else
            {
                int countOne = 0;
                BitArray node = treeNode(level, numberNode);

                for (int i = 0; i < (this.prime * (this.prime - 1) / 2); i++)
                {
                    countOne += (node[i]) ? 1 : 0;
                }
                double t = Math.Pow(this.prime, this.maxlevel - level - 1);
                double count = countOne * t * t;

                for (long i = numberNode * this.prime; i < this.prime * (numberNode + 1); i++)
                {
                    count += countEdges(i, level + 1);
                }
                return count;
            }

        }

        /// <summary>
        /// Returns graphs all edges count
        /// </summary>
        /// <returns></returns>
        public double countEdgesAllGraph()
        {
            int countOne = 0;
            double count = 0;
            for (int i = 0; i < this.treeMatrix.Length; i++)
            {
                for (int j = 0; j < this.treeMatrix[i].Length; j++)
                    for (int h = 0; h < this.treeMatrix[i][j].Length; h++)
                    {
                        countOne += (this.treeMatrix[i][j][h]) ? 1 : 0;
                    }
                double t = Math.Pow(this.prime, this.maxlevel - i - 1);
                count += countOne * t * t;
                countOne = 0;
            }
            return count;

        }

        /// <summary>
        /// Returns true if number1 block connected to number2
        /// </summary>
        /// <param name="node">nodes data</param>
        /// <param name="number1">number of the first block</param>
        /// <param name="number2">number of the second block</param>
        /// <returns></returns>
        public bool isConnectedTwoBlocks(BitArray node, int number1, int number2)
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
                index += prime - k;
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
        /// return count bloks which connected to i blok 
        /// </summary>
        /// <param name="node">nodes data</param>
        /// <param name="i">number of the  block</param>
        /// <returns></returns>
        public int countConnectedBlocks(BitArray node, int i)
        {
            i++;
            int s = 1, sum = 0;
            int findex = 0;
            while (s < i)
            {
                sum += Convert.ToInt32(node[findex + i - s - 1]);
                findex += prime - s;
                s++;
            }

            int endindex = findex + (prime - s);
            s = findex;

            while (s < endindex)
            {
                sum += Convert.ToInt32(node[s]);
                s++;
            }

            return sum;
        }
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
        /// Returns per level tree column vector
        /// </summary>
        /// <returns></returns>
        public BitArray treeVector()
        {
            BitArray vector = new BitArray(this.maxlevel);

            for (int i = 0; i < this.treeMatrix.Length; i++)
            {
                vector[i] = this.treeMatrix[i][0][0];
            }

            return vector;
        }

        ///getter - setter
        ///
        public int prime
        {
            get
            {
                return this.primeNumber;
            }
        }

        public int degree
        {
            get
            {
                return this.maxlevel;
            }
        }

        public int getGraphSize()
        {
            return (int)Math.Pow(primeNumber,maxlevel);
        }
    }
}
