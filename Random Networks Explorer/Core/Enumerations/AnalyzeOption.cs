using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        [AnalyzeOptionInfo("None", "Indication of empty selection.")]
        None = 0x0,

        // Global properties. //

        [AnalyzeOptionInfo("", "")]
        AvgPathLength = 0x01,

        [AnalyzeOptionInfo("", "")]
        Diameter = 0x02,

        [AnalyzeOptionInfo("", "")]
        AvgDegree = 0x04,

        [AnalyzeOptionInfo("", "")]
        AvgClusteringCoefficient = 0x08,

        [AnalyzeOptionInfo("", "")]
        Cycles3 = 0x10,

        [AnalyzeOptionInfo("", "")]
        Cycles4 = 0x20,

        // Eigenvalues spectra properties. //

        [AnalyzeOptionInfo("", "")]
        EigenValues = 0x40,

        [AnalyzeOptionInfo("", "")]
        Cycles3Eigen = 0x80,

        [AnalyzeOptionInfo("", "")]
        Cycles4Eigen = 0x100,

        [AnalyzeOptionInfo("", "")]
        EigenDistanceDistribution = 0x200,

        // Distributions. //

        [AnalyzeOptionInfo("", "")]
        DegreeDistribution = 0x400,

        [AnalyzeOptionInfo("", "")]
        ClusteringCoefficientDistribution = 0x800,

        [AnalyzeOptionInfo("", "")]
        ConnectedComponentDistribution = 0x1000,

        [AnalyzeOptionInfo("", "")]
        CompleteComponentDistribution = 0x2000,

        [AnalyzeOptionInfo("", "")]
        DistanceDistribution = 0x4000,

        [AnalyzeOptionInfo("", "")]
        TriangleDistribution = 0x8000,

        [AnalyzeOptionInfo("", "")]
        TriangleByVertexDistribution = 0x10000,

        [AnalyzeOptionInfo("", "")]
        CycleDistribution = 0x20000
    }
}
