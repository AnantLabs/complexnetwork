using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomGraph.Core.Manager.Status
{
    public class ExecutionStatusEventArgs : EventArgs
    {
        private ExecutionStatus status;

        public ExecutionStatusEventArgs(ExecutionStatus status)
        {
            this.status = status;
        }

        public ExecutionStatus ExecutionStatus 
        {
            get
            {
                return status;
            }
        }
    }
}
