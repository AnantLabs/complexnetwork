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
        public GraphModel(string name, string description,bool checkModel=false)
        {
            Name = name;
            Description = description;
            CheckModel = checkModel;
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

        public bool CheckModel
        {
            get;
            private set;
        }
    }
}
