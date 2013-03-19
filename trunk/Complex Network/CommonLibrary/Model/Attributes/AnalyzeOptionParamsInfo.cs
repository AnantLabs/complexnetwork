using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Model.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class AnalyzeOptionParamInfo : Attribute
    {
        public Type Type { get; set; }
        public String Name { get; set; }
    }
}