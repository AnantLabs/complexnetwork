using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Model.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class StatAnalyzeParametersInfo : Attribute
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
}
