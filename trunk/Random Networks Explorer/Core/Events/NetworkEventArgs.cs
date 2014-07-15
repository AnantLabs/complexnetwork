using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;

namespace Core.Events
{
    public class NetworkEventArgs : EventArgs
    {
        public RealizationStatus Status { get; private set; }
        public string ExtendedInfo { get; private set; }

        public NetworkEventArgs(RealizationStatus status, string extendedInfo)
        {
            Status = status;
            ExtendedInfo = extendedInfo;
        }
    }

    public delegate void NetworkStatusUpdateHandler(object sender, NetworkEventArgs e);
}
