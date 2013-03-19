using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

using CommonLibrary.Model.Attributes;

namespace RandomGraph.Common.Model
{
    public enum AnalyzeOptionParam
    {
        [AnalyzeOptionParamInfo(Name = "Cycles Low Bound", Type = typeof(Int16))]
        CyclesLow = 1,

        [AnalyzeOptionParamInfo(Name = "Cycles High Bound", Type = typeof(Int16))]
        CyclesHigh = 2,

        [AnalyzeOptionParamInfo(Name = "Motifs Low Bound", Type = typeof(Int16))]
        MotifsLow = 3,

        [AnalyzeOptionParamInfo(Name = "Motifs High Bound", Type = typeof(Int16))]
        MotifsHigh = 4,

        [AnalyzeOptionParamInfo(Name = "Mu Parameter for Trajectory", Type = typeof(Double))]
        TrajectoryMu = 5,

        [AnalyzeOptionParamInfo(Name = "Step Count Parameter for Trajectory", Type = typeof(BigInteger))]
        TrajectoryStepCount = 6
    }
}