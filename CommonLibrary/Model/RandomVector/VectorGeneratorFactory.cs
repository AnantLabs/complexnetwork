using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumberGeneration;

namespace CommonLibrary.Model.Random
{
    public class VectorGeneratorFactory
    {
        private VectorGeneratorFactory() { }

        public static IVectorGenerator GetDefaultGenerator()
        {
            return GetGenerator(RandomGenerator.Native);
        }

        public static IVectorGenerator GetGenerator(RandomGenerator randomGenerator)
        {
            if (randomGenerator == RandomGenerator.Native)
            {
                return new NativeVectorGenerator();
            }
            AbstractNumberGenerator numberGenerator = null;
            switch (randomGenerator)
            {
                case RandomGenerator.Bauke:
                    numberGenerator = new BaukeNumberGenerator();
                    break;
                case RandomGenerator.LEcuyer:
                    numberGenerator = new LEcuyerNumberGenerator();
                    break;
                case RandomGenerator.Tausworthe:
                    numberGenerator = new TauswortheNumberGenerator();
                    break;
            }
            return new VectorGenerator(numberGenerator);
        }
    }

    public enum RandomGenerator
    {
        Native,
        Bauke,
        LEcuyer,
        Tausworthe
    }
}
