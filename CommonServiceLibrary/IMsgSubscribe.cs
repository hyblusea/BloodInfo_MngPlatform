using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CommonServiceLibrary
{
    //[ServiceContract(CallbackContract = typeof(IDataCallback), SessionMode = SessionMode.Required)]
    [ServiceContract]
    public interface IMsgSubscribe
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        void Subscribe();
    }


}
