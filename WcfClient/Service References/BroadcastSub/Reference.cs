﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfClient.BroadcastSub {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BroadcastSub.IBroadcastingSubscribe", CallbackContract=typeof(WcfClient.BroadcastSub.IBroadcastingSubscribeCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IBroadcastingSubscribe {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastingSubscribe/Subscribe", ReplyAction="http://tempuri.org/IBroadcastingSubscribe/SubscribeResponse")]
        void Subscribe();
        
        [System.ServiceModel.OperationContractAttribute(IsTerminating=true, Action="http://tempuri.org/IBroadcastingSubscribe/Unsubscribe", ReplyAction="http://tempuri.org/IBroadcastingSubscribe/UnsubscribeResponse")]
        void Unsubscribe();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IBroadcastingSubscribe/PublishMsg")]
        void PublishMsg(string MsgContent);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBroadcastingSubscribeCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IBroadcastingSubscribe/MsgPublished")]
        void MsgPublished(string MsgContent);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBroadcastingSubscribeChannel : WcfClient.BroadcastSub.IBroadcastingSubscribe, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BroadcastingSubscribeClient : System.ServiceModel.DuplexClientBase<WcfClient.BroadcastSub.IBroadcastingSubscribe>, WcfClient.BroadcastSub.IBroadcastingSubscribe {
        
        public BroadcastingSubscribeClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public BroadcastingSubscribeClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public BroadcastingSubscribeClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public BroadcastingSubscribeClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public BroadcastingSubscribeClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Subscribe() {
            base.Channel.Subscribe();
        }
        
        public void Unsubscribe() {
            base.Channel.Unsubscribe();
        }
        
        public void PublishMsg(string MsgContent) {
            base.Channel.PublishMsg(MsgContent);
        }
    }
}
