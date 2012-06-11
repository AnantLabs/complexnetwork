using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Model.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class AnalyzeOptionInfo : System.Attribute
    {
        public AnalyzeOptionInfo(string name, string description)
        {
            Name = name;
            Description = description;
        }
        
        public string Name
        {
            get;
            private set;
        }

        public string Description
        {
            get;
            private set;
        }

        public string GXAxis
        {
            get;
            set;
        }

        public string GYAxis
        {
            get;
            set;
        }

        public string LXAxis
        {
            get;
            set;
        }

        public string LYAxis
        {
            get;
            set;
        }
    }
}
