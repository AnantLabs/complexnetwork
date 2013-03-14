using System;
using System.Collections.Generic;
using System.Text;

namespace RandomGraph.Common.Model.Status
{
    /// <summary>
    /// Statuses of Graph model
    /// </summary>
    public enum GraphProgress
    {
        Initializing,
        Ready,
        StartingGeneration,
        Generating,
        GenerationDone,
        GenerationFailed,

        StartingAnalyzing,
        Analyzing,
        AnalyzingDone,
        AnalyzingFailed,
        Done,
        Paused,
        Stopped, 
        Failed,
        Calculating
    }
}
