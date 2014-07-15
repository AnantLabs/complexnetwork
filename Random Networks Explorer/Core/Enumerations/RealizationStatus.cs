using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Enumerations
{
    public enum RealizationStatus
    {
        Generating,
        GenerationCompleted,
        Tracing,
        TracingCompleted,
        Analyzing,
        AnalyzingCompleted,
        Failed,
    }
}
