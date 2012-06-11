using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RandomGraph.Common.Model;
using RandomGraph.Core.Events;
using RandomGraph.Common.Model.Result;


namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(Namespace = "http://ComplexNetwork.Ysu", SessionMode = SessionMode.Required,
                 CallbackContract = typeof(IDuplexCallback))]
    public interface IComplexNetworkWorkerService
    {
        [OperationContract(IsOneWay = true)]
        void StopAll();

        [OperationContract]
        void StopInstance(int index);

        [OperationContract]
        void PauseAll();

        [OperationContract]
        void PauseInstance(int index);

        [OperationContract]
        void ContinueAll();

        [OperationContract]
        void ContinueInstance(int index);

        [OperationContract]
        void Start(AbstractGraphFactory modelFactory, int startIndex, int endIndex);
    }

    public interface IDuplexCallback
    {
        [OperationContract(IsOneWay = true)]
        void ProgressReport(AbstractGraphModel model, GraphProgressEventArgs args);
    }
}
