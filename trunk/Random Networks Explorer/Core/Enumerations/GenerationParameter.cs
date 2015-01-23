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
        [GenerationParameterInfo("Number of vertices", 
            "The initial number of vertices in the network.", 
            typeof(UInt16), "10")]
        Vertices,

        [GenerationParameterInfo("Connectivity probability", 
            "The probability of existance of a connection between the nodes in the network.", 
            typeof(Single), "0.1")]
        Probability,

        [GenerationParameterInfo("Permanent network", 
            "Defines if the initial network is permanent for each generation step.", 
            typeof(Boolean), "false")]
        PermanentNetwork,

        [GenerationParameterInfo("Number of edges", 
            "The initial number of edges in the network.", 
            typeof(UInt32), "1")]
        Edges,

        [GenerationParameterInfo("Step count", 
            "The number of generation steps to get each network in the ensemble.", 
            typeof(UInt16), "1")]
        StepCount,

        [GenerationParameterInfo("Branching index", 
            "The branching index of the block-hierarchical network.", 
            typeof(UInt16), "2")]
        BranchingIndex,

        [GenerationParameterInfo("Γ - Level", 
            "The level of the block-hierarchical network.", 
            typeof(UInt16), "1")]
        Level,

        [GenerationParameterInfo("μ", 
            "The density parameter of the block-hierarchical network.", 
            typeof(Single), "0.1")]
        Mu,

        [GenerationParameterInfo("Adjacency matrix's file name",
            "The name of file, which contains adjacency matrix of network.",
            typeof(String), "")]
        AdjacencyMatrixFile
    }
}
