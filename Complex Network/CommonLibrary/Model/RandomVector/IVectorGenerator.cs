using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Model.Random
{
    public interface IVectorGenerator
    {
        // returns 'true' with probability (1 / 'probability')
        bool GenerateNumber(ulong probability);

        // returns bool[] with length 'vectorLength', in which each element is 'true' with probability (1 / 'probability')
        bool[] GenerateVector(ulong probability, int vectorLength);

        // returns bool[] with length 'vectorLength', in which element 'i' is 'true' with probability (1 / 'probabilityArray[i]')
        bool[] GenerateVector(ulong[] probabilityArray, int vectorLength);
    }
}
