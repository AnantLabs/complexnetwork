using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core.Enumerations;

namespace Core.Events
{
    public class ResearchEventArgs : EventArgs
    {
        public Guid ResearchID { get; set; }
        public ResearchStatus Status { get; set; }
        public string ExtendedInfo { get; set; }

        public ResearchEventArgs()
        {
            Status = ResearchStatus.NotStarted;
            ExtendedInfo = "Not Started";
        }

        public ResearchEventArgs(Guid id, ResearchStatus status, string extendedInfo)
        {
            ResearchID = id;
            Status = status;
            ExtendedInfo = extendedInfo;
        }
    }

    public class ResearchEnsembleEventArgs : EventArgs
    {
        public Guid ResearchID { get; set; }
        public int UpdatedNetworkID { get; set; }
        public NetworkStatus UpdatedStatus { get; set; }
        public string UpdatedExtendedInfo { get; set; }

        public ResearchEnsembleEventArgs(Guid id, EnsembleEventArgs ensembleArgs)
        {
            ResearchID = id;
            UpdatedNetworkID = ensembleArgs.UpdatedNetworkID;
            UpdatedStatus = ensembleArgs.UpdatedStatus;
            UpdatedExtendedInfo = ensembleArgs.UpdatedExtendedInfo;
        }
    }

    public delegate void ResearchStatusUpdateHandler(object sender, ResearchEventArgs e);

    public delegate void ResearchEnsembleStatusUpdateHandler(object sender, ResearchEnsembleEventArgs e);
}
