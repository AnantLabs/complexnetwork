using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Model.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class TargetGraphModel : System.Attribute
    {
        public TargetGraphModel(Type graphModel)
        {
            GraphModelType = graphModel;
        }

        public Type GraphModelType
        {
            get;
            private set;
        }
    }
}
