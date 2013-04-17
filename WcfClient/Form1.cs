using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;

namespace WcfClient
{
    public partial class Form1 : Form
    {
        //DataCallbackImp dcb = new DataCallbackImp();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CommonService.CommonServiceClient cs = new CommonService.CommonServiceClient();
            //Console.WriteLine(cs.GetData(9));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MsgSubscribe.IMsgSubscribeCallback callback = new DataCallbackImp();
            InstanceContext instanceContext = new InstanceContext(callback);
            MsgSubscribe.MsgSubscribeClient cs = new MsgSubscribe.MsgSubscribeClient(instanceContext);

            //var service = DuplexChannelFactory<MsgSubscribe.IMsgSubscribe>.CreateChannel(instanceContext, new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8739/CommonServiceLibrary/MsgSubscribe/"));
            cs.Subscribe();

            cs.DoWork();
        }

        class DataCallbackImp : MsgSubscribe.IMsgSubscribeCallback
        {
            /// <summary> 
            /// 实现SleepCallback方法 
            /// </summary> 
            public void DataCallback(string text)
            {
                Console.WriteLine("收到回调了：" + text);
            }
        } 
    }
}
