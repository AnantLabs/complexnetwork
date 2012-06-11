using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumberGeneration;

namespace Model.StaticModel.Realization
{
    public class StaticGenerator
    {
        private  RNGCrypto rand = new RNGCrypto();

        public StaticGenerator() { }

        public bool[] MakeGenerationStep(double[] probabilityArray)
        {
            
            bool[] result = new bool[probabilityArray.Length];

            for (int i = 0; i < probabilityArray.Length; ++i)
                result[i] = rand.NextDouble() <= probabilityArray[i];

            return result;
        }
    }
}
