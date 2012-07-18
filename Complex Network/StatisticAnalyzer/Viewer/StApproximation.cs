using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatisticAnalyzer.Viewer
{
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class ApproximationTypeInfo : Attribute
    {
        public int ID
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string XAxis
        {
            get;
            set;
        }
        public string YAxis
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Enumeration of supported approximation types that
    /// could be use in analyze process.
    /// <summary>
    public enum ApproximationTypes
    {
        [ApproximationTypeInfo(ID = 1, Name = "None Approximation", XAxis = "X", YAxis = "Y")]
        None = 0x0,

        [ApproximationTypeInfo(ID = 2, Name = "Degree Approximation", XAxis = "lnX", YAxis = "lnY")]
        Degree = 0x01,

        [ApproximationTypeInfo(ID = 3, Name = "Exponential Approximation", XAxis = "X", YAxis = "lnY")]
        Exponential = 0x02,

        [ApproximationTypeInfo(ID = 4, Name = "Gaus Approximation", XAxis = "X ^ 2", YAxis = "lnY")]
        Gaus = 0x04
    }
}