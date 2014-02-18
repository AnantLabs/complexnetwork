using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Attributes;

namespace Core.Enumerations
{
    /// <summary>
    /// Enumeration of generation parameters that should be used for generating of a random network.
    /// Uses Attribute GenerationParameterInfo for storing metadata about every generation parameter.
    /// This metadata is used mainly for validating and getting user-friendly information.
    /// </summary>
    public enum GenerationParameter
    {
        [GenerationParameterInfo("", "", typeof(UInt16))]
        Vertices = 1,

        [GenerationParameterInfo("", "", typeof(Single))]
        Probability,

        [GenerationParameterInfo("", "", typeof(Boolean))]
        PermanentNetwork,

        [GenerationParameterInfo("", "", typeof(UInt32))]
        Edges,

        [GenerationParameterInfo("", "", typeof(UInt16))]
        StepCount,

        [GenerationParameterInfo("", "", typeof(UInt16))]
        BranchingIndex,

        [GenerationParameterInfo("", "", typeof(UInt16))]
        Level,

        [GenerationParameterInfo("", "", typeof(Single))]
        Mu
    }
}
