using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

using Core.Attributes;

namespace Core.Enumerations
{
    /// <summary>
    /// Flags enumaration, used for indicating which statistical properties
    /// should be analyzed during the Research run.
    /// Uses Attribute AnalyzeOptionInfo for storing metadata about every option.
    /// This metadata is used mainly for getting user-friendly information.
    /// </summary>
    [Flags]
    public enum AnalyzeOption
    {
        [AnalyzeOptionInfo("None", 
            "Indication of empty selection.",
            OptionType.Global,
            typeof(void),
            typeof(void))]
        None = 0x0,

        // Global properties. //

        [AnalyzeOptionInfo("Average path length", 
            "The average length of the shortest paths for all possible pairs in the network.",
            OptionType.Global,
            typeof(Double),
            typeof(Double))]
        AvgPathLength = 0x01,

        [AnalyzeOptionInfo("Diameter", 
            "The longest shortest path between any two nodes in the network.",
            OptionType.Global,
            typeof(UInt32),
            typeof(Double))]
        Diameter = 0x02,

        [AnalyzeOptionInfo("Average degree", 
            "The average value of the degrees of nodes in the network.",
            OptionType.Global,
            typeof(Double),
            typeof(Double))]
        AvgDegree = 0x04,

        [AnalyzeOptionInfo("Average clustering coefficient", 
            "The average value of the clustering coefficients of nodes in the network.",
            OptionType.Global,
            typeof(Double),
            typeof(Double))]
        AvgClusteringCoefficient = 0x08,

        [AnalyzeOptionInfo("3-length cycles", 
            "Number of cycles of length 3 in the network.",
            OptionType.Global,
            typeof(Double),   
            typeof(Double))]
        Cycles3 = 0x10,

        [AnalyzeOptionInfo("4-length cycles", 
            "Number of cycles of length 4 in the network.",
            OptionType.Global,
            typeof(Double),  
            typeof(Double))]
        Cycles4 = 0x20,

        // Eigenvalues spectra properties. //

        [AnalyzeOptionInfo("Eigenvalues",
            "The spectrum of network's adjacency matrix’s eigenvalues.",
            OptionType.ValueList,
            typeof(List<Double>),
            typeof(List<Double>))]
        EigenValues = 0x40,

        [AnalyzeOptionInfo("3-length cycles (eigenvalues)", 
            "Number of cycles of length 3 in the network calculated from the spectrum of eigenvalues.",
            OptionType.Global,
            typeof(BigInteger),
            typeof(Double))]
        Cycles3Eigen = 0x80,

        [AnalyzeOptionInfo("4-length cycles (eigenvalues)",
            "Number of cycles of length 4 in the network calculated from the spectrum of eigenvalues.",
            OptionType.Global,
            typeof(BigInteger),
            typeof(Double))]
        Cycles4Eigen = 0x100,

        [AnalyzeOptionInfo("Eigenvalues distances distribution",
            "The distribution of intervals between network's adjacency matrix’s eigenvalues.",
            OptionType.Distribution,
            typeof(SortedDictionary<Double, UInt32>),
            typeof(SortedDictionary<Double, Double>),
            "Distance", 
            "AvgCount")]
        EigenDistanceDistribution = 0x200,

        // Distributions. //

        [AnalyzeOptionInfo("Degree distribution", 
            "Network's node degree distribution.",
            OptionType.Distribution,
            typeof(SortedDictionary<UInt32, UInt32>),
            typeof(SortedDictionary<UInt32, Double>),
            "Degree",
            "AvgCount")]
        DegreeDistribution = 0x400,

        [AnalyzeOptionInfo("Clustering coefficients distribution", 
            "Network's node clustering coefficient distribution.",
            OptionType.Distribution,
            typeof(SortedDictionary<Double, UInt32>),
            typeof(SortedDictionary<Double, Double>),
            "Coefficient",
            "AvgCount")]
        ClusteringCoefficientDistribution = 0x800,

        [AnalyzeOptionInfo("Connected component distribution",
            "Length distribution of the connected subnetworks in the network.",
            OptionType.Distribution,
            typeof(SortedDictionary<UInt32, UInt32>),
            typeof(SortedDictionary<UInt32, Double>),
            "Order",
            "AvgCount")]
        ConnectedComponentDistribution = 0x1000,

        [AnalyzeOptionInfo("Complete component distribution", 
            "Length distribution of the complete subnetworks in the network.",
            OptionType.Distribution,
            typeof(SortedDictionary<UInt32, UInt32>),
            typeof(SortedDictionary<UInt32, Double>),
            "Order",
            "AvgCount")]
        CompleteComponentDistribution = 0x2000,

        [AnalyzeOptionInfo("Distance distribution", 
            "Node-node distance distribution in the network.",
            OptionType.Distribution,
            typeof(SortedDictionary<UInt32, UInt32>),
            typeof(SortedDictionary<UInt32, Double>),
            "Distance",
            "AvgCount")]
        DistanceDistribution = 0x4000,

        [AnalyzeOptionInfo("Triangle distribution", 
            "The distribution of cycles of length 3 (triangles), which contain the node x.",
            OptionType.Distribution,
            typeof(SortedDictionary<UInt32, UInt32>),
            typeof(SortedDictionary<UInt32, Double>),
            "TriangleCount",
            "AvgCount")]
        TriangleByVertexDistribution = 0x8000,

        [AnalyzeOptionInfo("Cycle distribution", 
            "Cycle length distribution in the network.",
            OptionType.Distribution,
            typeof(SortedDictionary<UInt16, BigInteger>),
            typeof(SortedDictionary<UInt16, Double>),
            "Length",
            "AvgCount")]
        CycleDistribution = 0x10000,

        // Trajectories. //

        [AnalyzeOptionInfo("3-length cycles trajectory",
            "Count of 3-length cycles in evolution process.",
            OptionType.Trajectory,
            typeof(SortedDictionary<UInt32, long>),
            typeof(SortedDictionary<UInt32, Double>),
            "StepNumber",
            "AvgCount")]
        Cycles3Trajectory = 0x20000
    }
}
