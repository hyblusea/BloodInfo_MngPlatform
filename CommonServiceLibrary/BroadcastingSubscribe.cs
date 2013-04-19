using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CommonServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class BroadcastingSubscribe : IBroadcastingSubscribe
    {
        public static event PublishMsgHandler PublishMsgHandlerEvent;
        public delegate void PublishMsgHandler(string MsgContent);

        IPublishMsgCallBack callback = null;

        PublishMsgHandler publishHandler = null;

        //Clients call this service operation to subscribe.
        //A price change event handler is registered for this client instance.
        public void Subscribe()
        {
            callback = OperationContext.Current.GetCallbackChannel<IPublishMsgCallBack>();
            publishHandler = new PublishMsgHandler(CallBackMsgToClient);
            PublishMsgHandlerEvent += publishHandler;
        }

        //Clients call this service operation to unsubscribe.
        //The previous price change event handler is deregistered.
        public void Unsubscribe()
        {
            PublishMsgHandlerEvent -= publishHandler;
        }

        //Information source clients call this service operation to report a price change.
        //A price change event is raised. The price change event handlers for each subscriber will execute.
        public void PublishMsg(string msg)
        {
            if (PublishMsgHandlerEvent != null)
                PublishMsgHandlerEvent(msg);
        }

        //This event handler runs when a PriceChange event is raised.
        //The client's PriceChange service operation is invoked to provide notification about the price change.
        public void CallBackMsgToClient(string MsgContent)
        {
            try
            {
                callback.MsgPublished(MsgContent);
            }
            catch (Exception err)
            {
                Unsubscribe();
                Console.WriteLine(err.Message);
            }
            
        }

    }
}
