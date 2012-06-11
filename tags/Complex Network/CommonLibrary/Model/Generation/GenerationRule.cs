using System;
using System.Collections.Generic;
using System.Text;

namespace RandomGraph.Common.Model.Generation
{
    /// <summary>
    /// Defines generation rules available in the system.
    /// Currently only two type are supported.
    /// Sequential for non Markovian processes and
    /// Separate for Markovian processes
    /// </summary>
    public enum GenerationRule
    {
        Sequential,
        Separate
    }
}
