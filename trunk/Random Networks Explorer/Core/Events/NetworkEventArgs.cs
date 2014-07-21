using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;

namespace Core.Events
{
    public class NetworkEventArgs : EventArgs
    {
        public int ID { get; set; }
        public NetworkStatus Status { get; set; }
        public string ExtendedInfo { get; set; }

        public NetworkEventArgs()
        {
            Status = NetworkStatus.NotStarted;
            ExtendedInfo = "Not Started";
        }

        public NetworkEventArgs(int id, NetworkStatus status, string extendedInfo)
        {
            ID = id;
            Status = status;
            ExtendedInfo = extendedInfo;
        }
    }

    public delegate void NetworkStatusUpdateHandler(object sender, NetworkEventArgs e);
}
