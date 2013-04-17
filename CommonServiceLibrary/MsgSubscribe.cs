using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CommonServiceLibrary
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class MsgSubscribe : IMsgSubscribe
    {
        

        public void DoWork()
        {
            
        }

        //This event handler runs when a PriceChange event is raised.
        //The client's PriceChange service operation is invoked to provide notification about the price change.
        public void Subscribe()
        {
            
        }
    }


    
}
