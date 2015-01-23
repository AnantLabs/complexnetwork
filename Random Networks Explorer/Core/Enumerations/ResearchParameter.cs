using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Attributes;

namespace Core.Enumerations
{
    /// <summary>
    /// Enumeration of research parameters that should be used for Research run.
    /// Uses Attribute ResearchParameterInfo for storing metadata about every research parameter.
    /// This metadata is used mainly for validating and getting user-friendly information.
    /// </summary>
    public enum ResearchParameter
    {
        [ResearchParameterInfo("", "", typeof(UInt32), "1")]
        EvolutionStepCount,

        [ResearchParameterInfo("", "", typeof(Single), "0.1")]
        Nu,

        [ResearchParameterInfo("", "", typeof(UInt16), "0")]
        TracingStepIncrement,

        [ResearchParameterInfo("", "", typeof(Boolean), "false")]
        PermanentDistribution,

        [ResearchParameterInfo("", "", typeof(Single), "1.0")]
        ProbabilityMax,

        [ResearchParameterInfo("", "", typeof(Single), "0.01")]
        ProbabilityDelta
    }
}
