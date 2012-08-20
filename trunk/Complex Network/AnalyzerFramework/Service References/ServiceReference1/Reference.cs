﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnalyzerFramework.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://ComplexNetwork.Ysu", ConfigurationName="ServiceReference1.IComplexNetworkWorkerService", CallbackContract=typeof(AnalyzerFramework.ServiceReference1.IComplexNetworkWorkerServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IComplexNetworkWorkerService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://ComplexNetwork.Ysu/IComplexNetworkWorkerService/StopAll")]
        void StopAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ComplexNetwork.Ysu/IComplexNetworkWorkerService/StopInstance", ReplyAction="http://ComplexNetwork.Ysu/IComplexNetworkWorkerService/StopInstanceResponse")]
        void StopInstance(int index);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ComplexNetwork.Ysu/IComplexNetworkWorkerService/PauseAll", ReplyAction="http://ComplexNetwork.Ysu/IComplexNetworkWorkerService/PauseAllResponse")]
        void PauseAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ComplexNetwork.Ysu/IComplexNetworkWorkerService/PauseInstance", ReplyAction="http://ComplexNetwork.Ysu/IComplexNetworkWorkerService/PauseInstanceResponse")]
        void PauseInstance(int index);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ComplexNetwork.Ysu/IComplexNetworkWorkerService/ContinueAll", ReplyAction="http://ComplexNetwork.Ysu/IComplexNetworkWorkerService/ContinueAllResponse")]
        void ContinueAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ComplexNetwork.Ysu/IComplexNetworkWorkerService/ContinueInstance", ReplyAction="http://ComplexNetwork.Ysu/IComplexNetworkWorkerService/ContinueInstanceResponse")]
        void ContinueInstance(int index);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ComplexNetwork.Ysu/IComplexNetworkWorkerService/Start", ReplyAction="http://ComplexNetwork.Ysu/IComplexNetworkWorkerService/StartResponse")]
        void Start(RandomGraph.Common.Model.AbstractGraphModel model, int startIndex, int endIndex);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IComplexNetworkWorkerServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://ComplexNetwork.Ysu/IComplexNetworkWorkerService/ProgressReport")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Model.BAModel.BAModel))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Model.ERModel.ERModel))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Model.WSModel.WSModel))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Model.ParisiHierarchicModel.ParisiHierarchicModel))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Model.NonRegularHierarchicModel.NonRegularHierarchicModel))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Model.HierarchicModel.HierarchicModel))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RandomGraph.Common.Model.AnalyseOptions))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<RandomGraph.Common.Model.Generation.GenerationParam, object>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<double, int>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<RandomGraph.Common.Model.AnalyseOptions, double>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<int, int>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(int[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RandomGraph.Common.Model.Generation.GenerationParam))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RandomGraph.Common.Model.Generation.GenerationRule))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RandomGraph.Common.Model.Generation.GenerationParam[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RandomGraph.Common.Model.Status.GraphProgressStatus))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RandomGraph.Common.Model.Status.GraphProgress))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RandomGraph.Common.Model.Result.AnalizeResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.BitArray))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RandomGraph.Core.Events.GraphProgressEventArgs))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.EventArgs))]
        void ProgressReport(RandomGraph.Common.Model.AbstractGraphModel model, RandomGraph.Core.Events.GraphProgressEventArgs args);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IComplexNetworkWorkerServiceChannel : AnalyzerFramework.ServiceReference1.IComplexNetworkWorkerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ComplexNetworkWorkerServiceClient : System.ServiceModel.DuplexClientBase<AnalyzerFramework.ServiceReference1.IComplexNetworkWorkerService>, AnalyzerFramework.ServiceReference1.IComplexNetworkWorkerService {
        
        public ComplexNetworkWorkerServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ComplexNetworkWorkerServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ComplexNetworkWorkerServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ComplexNetworkWorkerServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ComplexNetworkWorkerServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void StopAll() {
            base.Channel.StopAll();
        }
        
        public void StopInstance(int index) {
            base.Channel.StopInstance(index);
        }
        
        public void PauseAll() {
            base.Channel.PauseAll();
        }
        
        public void PauseInstance(int index) {
            base.Channel.PauseInstance(index);
        }
        
        public void ContinueAll() {
            base.Channel.ContinueAll();
        }
        
        public void ContinueInstance(int index) {
            base.Channel.ContinueInstance(index);
        }
        
        public void Start(RandomGraph.Common.Model.AbstractGraphModel model, int startIndex, int endIndex) {
            base.Channel.Start(model, startIndex, endIndex);
        }
    }
}
