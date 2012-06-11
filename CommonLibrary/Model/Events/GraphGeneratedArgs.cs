using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Model.Util;

namespace CommonLibrary.Model.Events
{
    public class GraphGeneratedArgs
    {
        public GraphGeneratedArgs() { }

        public GraphGeneratedArgs(GraphTable generatedArgs)
        {
            this.GeneratedArgs = generatedArgs;
        }

        public GraphTable GeneratedArgs { get; set; }
    }
}
