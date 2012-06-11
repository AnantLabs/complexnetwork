using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandomGraph.Common.Model;

namespace CommonLibrary.Model.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class AvailableAnalyzeOptions : System.Attribute
    {
        public AvailableAnalyzeOptions(AnalyseOptions options)
        {
            Options = options;
        }

        public AnalyseOptions Options { get; private set; }

    }
}
