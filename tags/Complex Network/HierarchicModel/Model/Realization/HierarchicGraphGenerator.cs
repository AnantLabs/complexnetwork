using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using NumberGeneration;

namespace Model.HierarchicModel.Realization
{
    public abstract class HierarchicGraphGenerator
    {
        public BitArray[][] treeMatrix;
        protected const int ARRAY_MAX_SIZE = 2000000000;
        protected int primeNumber;
        protected int maxlevel;
        protected RNGCrypto rand;
        protected double lambda;

        public HierarchicGraphGenerator(int primeNumber, int degree, double lambda)
        {
            this.primeNumber = primeNumber;
            this.maxlevel = degree;
            this.lambda = lambda;
            rand = new RNGCrypto();
            this.treeMatrix = new BitArray[degree][];
            this.createTree();
        }

        protected abstract void createTree();

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
    }
}
