using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using RandomGraph.Common.Model;
using AnalyzerFramework.ServiceReference1;
using RandomGraph.Common.Model.Result;


namespace WcfClient
{
    public class HelpService
    {
        IComplexNetworkWorkerService service;

        public HelpService(InstanceContext instanceContext, EndpointDiscoveryMetadata metadata) {
            //service = new ComplexNetworkWorkerServiceClient(instanceContext, "NetTcpBinding_IComplexNetworkWorkerService", metadata.Address);
            service = new ComplexNetworkWorkerServiceClient(instanceContext);
        }

        public void StopAll() 
        {
            try
            {
                service.StopAll();
            }
            catch (TimeoutException) { }
        }

        public void StopInstance(int index)
        {
            try
            {
                service.StopInstance(index);
            }
            catch (TimeoutException) { }
        }

        public void PauseAll() 
        {
            try
            {
                service.PauseAll();
            }
            catch (TimeoutException) { }
        }

        public void PauseInstance(int index)
        {
            try
            {
                service.PauseInstance(index);
            }
            catch (TimeoutException) { }
        }

        public void ContinueAll() 
        {
            try
            {
                service.ContinueAll();
            }
            catch (TimeoutException) { }
        }

        public void ContinueInstance(int index) 
        {
            try
            {
                service.ContinueInstance(index);
            }
            catch (TimeoutException) { }
        }

        public void Start(AbstractGraphFactory modelFactory, int startIndex, int endIndex)
        {
           service.Start(modelFactory, startIndex, endIndex);
        }
    }
}
