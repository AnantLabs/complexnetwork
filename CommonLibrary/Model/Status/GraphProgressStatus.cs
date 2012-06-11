using System;
using System.Collections.Generic;
using System.Text;
using RandomGraph.Common.Model.Status;

namespace RandomGraph.Common.Model.Status
{
    public class GraphProgressStatus
    {
        public GraphProgress GraphProgress { get; set; }

        public int ID { get; set; }

        public int? Percent { get; set; }

        public string TargetName { get; set; }

        public string FailReason { get; set; }

        public string HostName { get; set; }
    }
}
