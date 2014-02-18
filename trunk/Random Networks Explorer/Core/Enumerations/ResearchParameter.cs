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
        [ResearchParameterInfo("", "", typeof(UInt16))]
        StepCount = 1,

        [ResearchParameterInfo("", "", typeof(Single))]
        Nu,

        [ResearchParameterInfo("", "", typeof(Boolean))]
        PermanentDistribution,

        [ResearchParameterInfo("", "", typeof(Single))]
        ProbabilityMax
    }
}
