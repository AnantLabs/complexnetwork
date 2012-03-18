using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumberGeneration;

namespace CommonLibrary.Model.Random
{
    class VectorGenerator : IVectorGenerator
    {
        private AbstractNumberGenerator numberGenerator;

        public VectorGenerator(AbstractNumberGenerator numberGenerator)
        {
            this.numberGenerator = numberGenerator;
        }

        public bool GenerateNumber(ulong probability)
        {
            return numberGenerator.Rand1DivP(probability);
        }

        public bool[] GenerateVector(ulong probability, int vectorLength)
        {
            bool[] vector = new bool[vectorLength];
            for (int i = 0; i < vector.Length; ++i)
            {
                vector[i] = numberGenerator.Rand1DivP(probability);
            }
            return vector;
        }

        public bool[] GenerateVector(ulong[] probabilityArray, int vectorLength)
        {
            bool[] vector = new bool[vectorLength];
            for (int i = 0; i < vector.Length; ++i)
            {
                vector[i] = numberGenerator.Rand1DivP(probabilityArray[i]);
            }
            return vector;
        }
    }
}
