using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Events
{
    public class NetworkEventArgs : EventArgs
    {
        public string Status { get; private set; }

        public NetworkEventArgs(string status)
        {
            Status = status;
        }
    }

    public delegate void NetworkStatusUpdateHandler(object sender, NetworkEventArgs e);
}
