using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary.Model.Attributes;

namespace RandomGraph.Common.Model.Generation
{
    /// <summary>
    /// Enumeration of supported generation parameters that
    /// should be use by graph models for generation process.
    /// Also uses attrinute GenerationParamInfo for validation and displaying on
    /// user interface 
    /// </summary>
    public enum GenerationParam
    {
        [GenerationParamInfo(Name = "Number of Vertices", Type = typeof(Int32))]
        Vertices = 1,

        [GenerationParamInfo(Name = "Number of edges", Type = typeof(Int32))]
        Edges = 2,

        [GenerationParamInfo(Name = "Branch index", Type = typeof(Int16))]
        BranchIndex = 3,

        [GenerationParamInfo(Name = "Level", Type = typeof(Int16))]
        Level = 4,

        [GenerationParamInfo(Name = "Maximum connections", Type = typeof(Int16))]
        MaxEdges = 5,

        [GenerationParamInfo(Name = "Mu", Type = typeof(double))]
        Mu = 6,

        [GenerationParamInfo(Name = "Probability", Type = typeof(double))]
        P = 7,

        [GenerationParamInfo(Name = "Step count", Type = typeof(Int32))]
        StepCount = 8,

        [GenerationParamInfo(Name = "Add Vertices", Type = typeof(Int32))]
        AddVertices = 9,
    }
}
