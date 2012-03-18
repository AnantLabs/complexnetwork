using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Discovery;
using AnalyzerFramework.ServiceReference1;

namespace AnalyzerFramework.Manager.WcfClient
{
    public class ServiceDiscoveryManager
    {
        private static IList<EndpointDiscoveryMetadata> endpoints = null;

        public static IList<EndpointDiscoveryMetadata> SearchServices()
        {
            DiscoveryClient discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());
            try
            {
                FindResponse discoveryResponse = discoveryClient.Find(new FindCriteria(typeof(IComplexNetworkWorkerService)));
                endpoints = discoveryResponse.Endpoints;    
            }
            catch (Exception)
            {
                endpoints = new List<EndpointDiscoveryMetadata>();

            }

            return endpoints;
        }

        public static IList<EndpointDiscoveryMetadata> GetServices()
        {
            if (endpoints == null)
            {
                return SearchServices();
            }
            return endpoints;
        }

        public static IList<EndpointDiscoveryMetadata> SelectedServices { get; set; }

    }
}
