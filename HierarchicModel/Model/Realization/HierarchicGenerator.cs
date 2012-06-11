using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Model.HierarchicModel.Realization
{
    public class HierarchicGenerator : HierarchicGraphGenerator
    {
        public HierarchicGenerator(int primeNumber, int degree, double lambda)
            : base(primeNumber, degree, lambda)
        {
        }


        private void generateData(int level)
        {
            /*switch (level)
            {
                case 3: this.treeMatrix[this.maxlevel - level][0].Set(0, false);
                        break;
                case 2: this.treeMatrix[this.maxlevel - level][0].Set(0, false);
                        break;
                case 1: this.treeMatrix[this.maxlevel - level][0].Set(0, true);
                        //this.treeMatrix[this.maxlevel - level][0].Set(1, true);
                        this.treeMatrix[this.maxlevel - level][0].Set(2, true);
                        break;
            }*/
            //loop over all elements of given level and generate him values
            for (int i = 0; i < this.treeMatrix[this.maxlevel - level].Length; i++)
            {
                for (int j = 0; j < this.treeMatrix[this.maxlevel - level][i].Length; j++)
                {
                    double k = rand.NextDouble();
                    if (k <= (1 / Math.Pow(this.primeNumber, level * this.lambda)))
                    {
                        this.treeMatrix[this.maxlevel - level][i][j] = true;
                    }
                    else
                    {
                        this.treeMatrix[this.maxlevel - level][i][j] = false;
                    }
                }
            }
        }


        /// <summary>
        /// Creates a tree with recursion
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        protected override void createTree()
        {
            //for every level create datas, started with root
            for (int i = this.maxlevel; i > 0; i--)
            {
                //get current level data length and bitArrays count
                int nodeDataLength = (this.primeNumber - 1) * this.primeNumber / 2;
                long dataLength = Convert.ToInt64(Math.Pow(this.primeNumber, this.maxlevel - i) * nodeDataLength);
                int arrCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dataLength) / ARRAY_MAX_SIZE));

                this.treeMatrix[this.maxlevel - i] = new BitArray[arrCount];
                int j;
                for (j = 0; j < arrCount - 1; j++)
                {
                    this.treeMatrix[this.maxlevel - i][j] = new BitArray(ARRAY_MAX_SIZE);
                }
                this.treeMatrix[this.maxlevel - i][j] = new BitArray(Convert.ToInt32(dataLength - (arrCount - 1) * ARRAY_MAX_SIZE));
                //genereates data for current level nodes

                this.generateData(i);

            }
        }
    }
}
