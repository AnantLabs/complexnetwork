using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Model.Random
{
    class NativeVectorGenerator : IVectorGenerator
    {
        public bool GenerateNumber(ulong probability)
        {
            double prob = 1 / probability;
            System.Random rand = new System.Random();
            return (rand.NextDouble() <= prob);
        }

        public bool[] GenerateVector(ulong probability, int vectorLength)
        {
            double prob = 1 / probability;
            bool[] vector = new bool[vectorLength];
            System.Random rand = new System.Random();
            for (int i = 0; i < vector.Length; ++i)
            {
                double k = rand.NextDouble();
                vector[i] = k <= prob;
            }
            return vector;
        }

        public bool[] GenerateVector(ulong[] probabilityArray, int vectorLength)
        {
            bool[] vector = new bool[vectorLength];
            System.Random rand = new System.Random();
            for (int i = 0; i < vector.Length; ++i)
            {
                double prob = 1 / probabilityArray[i];
                double k = rand.NextDouble();
                vector[i] = k <= prob;
            }
            return vector;
        }
    }
}
