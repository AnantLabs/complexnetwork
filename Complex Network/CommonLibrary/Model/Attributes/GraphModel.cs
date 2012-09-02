using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandomGraph.Common.Model.Generation;

namespace CommonLibrary.Model.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class GraphModel : System.Attribute
    {
        public GraphModel(string name, string description)
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
    }
}
