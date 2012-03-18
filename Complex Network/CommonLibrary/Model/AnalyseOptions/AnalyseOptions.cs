using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary.Model.Attributes;

namespace RandomGraph.Common.Model
{
    /// <summary>
    /// Flags enumaration, used for indicating which statistical properties
    /// should be calculated during the assembly run.
    /// Uses Attribute AnalyzeOptionInfo for storing metadata about every field.
    /// This metadata is used mainly for displayin user friendly information
    /// </summary>
    [Flags]
    public enum AnalyseOptions
    {
        [AnalyzeOptionInfo("None", "Used for indicating empty selection")]
        None = 0x0,

        [AnalyzeOptionInfo("Degree Distribution", "Degree distribution", GXAxis = "Instances", GYAxis = "Average Degree",
            LXAxis = "Degree", LYAxis = "Distribution")]
        DegreeDistribution = 0x01,

        [AnalyzeOptionInfo("Average Path Length", "Average path length", GXAxis = "Instances", GYAxis = "Avg")]
        AveragePath = 0x02,

        [AnalyzeOptionInfo("Clustering Coefficient", "Clustering coefficient", GXAxis = "Instances",
            GYAxis = "Clustering Coefficient", LXAxis = "Clustering Coefficient", LYAxis = "Distribution")]
        ClusteringCoefficient = 0x04,

        [AnalyzeOptionInfo("Eigen Values", "Eigen values", LXAxis = "Values", LYAxis = "Distribution")]
        EigenValue = 0x08,

        [AnalyzeOptionInfo("Cycles of Order 3", "Cycles with 3 length", GXAxis = "Instances", GYAxis = "Count")]
        Cycles3 = 0x10,

        [AnalyzeOptionInfo("Diameter", "Diameter of graph", GXAxis = "Instances", GYAxis = "Diameter")]
        Diameter = 0x20,

        [AnalyzeOptionInfo("Connected Subgraphs by Order", "Propability of k-degree connected sub graph existance", LXAxis = "Order",
            LYAxis = "Distribution")]
        ConnSubGraph = 0x40,

        [AnalyzeOptionInfo("Cycles 3 by eigen values", "Cycles 3 by eigen values")]
        CycleEigen3 = 0x80,

        [AnalyzeOptionInfo("Cycles of Order 4", "Cycles with 4 length", GXAxis = "Instances", GYAxis = "Count")]
        Cycles4 = 0x100,

        [AnalyzeOptionInfo("Cycles 4 by eigen values", "Cycles 4 by eigen values")]
        CycleEigen4 = 0x200,

        [AnalyzeOptionInfo("Motifs", "Calculate the count of motifs ")]
        Motif = 0x400,

        [AnalyzeOptionInfo("Distance between Vertices", "Minpath distribution", LXAxis = "Distance", LYAxis = "Distribution")]
        MinPathDist = 0x800,

        [AnalyzeOptionInfo("Distance between Eigen Values", "Eigen distribution", LXAxis = "Distance", LYAxis = "Distribution")]
        DistEigenPath = 0x1000,

        [AnalyzeOptionInfo("Full Subgraphs by Order", "Propability of k-degree connected sub graph existance", LXAxis = "Order",
            LYAxis = "Distribution")]
        FullSubGraph = 0x2000,

        [AnalyzeOptionInfo("Order of Maximal Full Subgraph", "Order of maximal full subgraph", GXAxis = "Instances", GYAxis = "Count")]
        MaxFullSubgraph = 0x4000,

        [AnalyzeOptionInfo("Largest Connected Component", "Maximal size of connected subgraph", GXAxis = "Instances",
            GYAxis = "Count")]
        LargestConnectedComponent = 0x8000,

        [AnalyzeOptionInfo("Minimal Eigen value", "Minimal eigen value", GXAxis = "Instances", GYAxis = "Count")]
        MinEigenValue = 0x10000,

        [AnalyzeOptionInfo("Maximal Eigen value", "Maximal eigen value", GXAxis = "Instances", GYAxis = "Count")]
        MaxEigenValue = 0x20000,

        [AnalyzeOptionInfo("Cycles", "Propability of k-degree cycle existance", LXAxis = "Order", LYAxis = "Distribution")]
        Cycles = 0x40000
    }
}
