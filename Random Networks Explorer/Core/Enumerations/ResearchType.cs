using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Attributes;

namespace Core.Enumerations
{
    /// <summary>
    /// Enumaration, used for indicating Research types.
    /// Uses Attribute ResearchTypeInfo for storing metadata about every Research.
    /// </summary>
    public enum ResearchType
    {
        [ResearchTypeInfo("", "", "BasicResearch")]
        Basic = 1,

        [ResearchTypeInfo("", "", "TrajectoryResearch")]
        Trajectory,

        [ResearchTypeInfo("", "", "PercolationResearch")]
        Percolation
    }
}