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
        [ResearchTypeInfo("Basic research", 
            "The basic analysis for random networks (single ensemble).", 
            "BasicResearch")]
        Basic = 0,

        [ResearchTypeInfo("Trajectory research", 
            "Analysis of trajectories for random networks (several ensembles).", 
            "TrajectoryResearch")]
        Trajectory,

        [ResearchTypeInfo("Percolation research", 
            "Analysis of percolation probability (critical probability) for random network (several ensembles).", 
            "PercolationResearch")]
        Percolation
    }
}