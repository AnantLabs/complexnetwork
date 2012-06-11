using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using WcfService;

namespace WindowsService
{
    public partial class WindowsServiceForCN : ServiceBase
    {
        private static ServiceHost host;

        public WindowsServiceForCN()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Just to be really safe.
            if (host != null)
            {
                host.Close();
                host = null;
            }
            // Create the host.
            host = new ServiceHost(typeof(ComplexNetworkWorkerService));
            // The ABCs in code!
            /*
            Uri address = new Uri("net.tcp://localhost:1012/myservice");
            NetTcpBinding binding = new NetTcpBinding();
            Type contract = typeof(IComplexNetworkWorkerService);
            // Add this endpoint.
            host.AddServiceEndpoint(contract, binding, address);
            */
            // discovery
            ServiceDiscoveryBehavior discoveryBehavior = new ServiceDiscoveryBehavior();
            host.Description.Behaviors.Add(discoveryBehavior);
            host.AddServiceEndpoint(new UdpDiscoveryEndpoint());
            discoveryBehavior.AnnouncementEndpoints.Add(new UdpAnnouncementEndpoint());

            // Open the host.
            host.Open();

        }

        protected override void OnStop()
        {
            // Shut down the host.
            if (host != null)
                host.Close();
        }
    }
}
