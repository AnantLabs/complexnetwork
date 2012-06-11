using System;
using System.Collections.Generic;
using System.Text;

namespace RandomGraph.Core.Manager.Status
{
    /// <summary>
    /// Used for Graph Manager statuses marking
    /// </summary>
    public enum ExecutionStatus
    {
        Stopped,
        Running,
        Paused,
        Starting,
        Failed,
        Stopping,
        Success
    }
}
