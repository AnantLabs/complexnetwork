using System;
using System.Collections.Generic;
using System.Text;

using CommonLibrary.Model.Attributes;

namespace RandomGraph.Common.Model.Generation
{
    /// <summary>
    /// Enumeration of supported generation parameters that
    /// should be used by graph models for generation process.
    /// Also uses attrinute GenerationParamInfo for validation and displaying on
    /// user interface 
    /// </summary>
    public enum GenerationParam
    {
        [GenerationParamInfo(Name = "Number", Type = typeof(Int32))]
        Vertices = 1,

        [GenerationParamInfo(Name = "Number of Edges", Type = typeof(Int32))]
        Edges = 2,

        [GenerationParamInfo(Name = "Branch Index", Type = typeof(Int16))]
        BranchIndex = 3,

        [GenerationParamInfo(Name = "Level", Type = typeof(Int16))]
        Level = 4,

        [GenerationParamInfo(Name = "Connections Count", Type = typeof(Int16))]
        MaxEdges = 5,

        [GenerationParamInfo(Name = "Mu", Type = typeof(double))]
        Mu = 6,

        [GenerationParamInfo(Name = "Probability", Type = typeof(double))]
        P = 7,

        [GenerationParamInfo(Name = "Step Count", Type = typeof(Int32))]
        StepCount = 8,

        [GenerationParamInfo(Name = "Initial Graph", Type = typeof(string))]
        InitialStep = 9,

        [GenerationParamInfo(Name = "Initial Probability", Type = typeof(double))]
        InitialProbability = 10,

        [GenerationParamInfo(Name = "Permanent", Type = typeof(bool))]
        Permanent = 11,

        [GenerationParamInfo(Name = "FileName", Type = typeof(string))]
        FileName = 12
    }
}
