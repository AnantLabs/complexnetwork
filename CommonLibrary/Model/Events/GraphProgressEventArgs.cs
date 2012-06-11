using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandomGraph.Common.Model.Status;
using System.Runtime.Serialization;

namespace RandomGraph.Core.Events
{
    public class GraphProgressEventArgs : EventArgs
    {
        public GraphProgressEventArgs() { }

        public GraphProgressEventArgs(GraphProgressStatus progressStatus)
        {
            this.Progress = progressStatus;
        }

        public GraphProgressStatus Progress { get; set;}
    }
}
