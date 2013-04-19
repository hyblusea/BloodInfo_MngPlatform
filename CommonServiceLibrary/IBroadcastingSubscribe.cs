using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CommonServiceLibrary
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IBroadcastingSubscribe”。
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IPublishMsgCallBack))]
    public interface IBroadcastingSubscribe
    {
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        void Subscribe();

        [OperationContract(IsOneWay = false, IsTerminating = true)]
        void Unsubscribe();

        [OperationContract(IsOneWay = true)]
        void PublishMsg(string MsgContent);
    }

    public interface IPublishMsgCallBack
    {
        [OperationContract(IsOneWay = true)]
        void MsgPublished(string MsgContent);
    }
}
