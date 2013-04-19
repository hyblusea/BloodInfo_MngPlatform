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

        BroadcastSub.IBroadcastingSubscribeCallback callback = new PublishCallbackImp();
        BroadcastSub.BroadcastingSubscribeClient cs;

        //var service = DuplexChannelFactory<MsgSubscribe.IMsgSubscribe>.CreateChannel(instanceContext, new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8739/CommonServiceLibrary/MsgSubscribe/"));
        //cs.Subscribe();


        public Form1()
        {
            InitializeComponent();

            try
            {
                ((PublishCallbackImp)callback).OnRecvSubMsgEvent += Form1_OnRecvSubMsgEvent;
                InstanceContext instanceContext = new InstanceContext(callback);
                cs = new BroadcastSub.BroadcastingSubscribeClient(instanceContext);
                cs.Subscribe();
            }
            catch (Exception err)
            {
                cs.Abort();
                textBox2.Text = "通道出现异常, " + DateTime.Now.ToString();
                MessageBox.Show(err.Message);
            }
                        
            
        }

        void Form1_OnRecvSubMsgEvent(string sMsg)
        {
            textBox1.Text = sMsg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CommService.CommonServiceClient cs = new CommService.CommonServiceClient();
            //Console.WriteLine(cs.GetData(9));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cs.PublishMsg(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffffff"));
            }
            catch (Exception)
            {
            }
            
            //Console.WriteLine(cs.InnerChannel.SessionId);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (cs.State == CommunicationState.Faulted || cs.State == CommunicationState.Closed)
            {
                textBox2.Text = "通道出现异常, " + DateTime.Now.ToString();
                cs.Abort();

                try
                {
                    InstanceContext instanceContext = new InstanceContext(callback);
                    cs = new BroadcastSub.BroadcastingSubscribeClient(instanceContext);
                    cs.Subscribe();
                }
                catch (Exception )
                {
                    cs.Abort();                    
                }

                
            }
        }

        

    }

    public class PublishCallbackImp : BroadcastSub.IBroadcastingSubscribeCallback
    {
        public delegate void OnRecvSubMsg(string sMsg);
        public event OnRecvSubMsg OnRecvSubMsgEvent;

        public void MsgPublished(string Msg)
        {
            if (OnRecvSubMsgEvent != null)
                OnRecvSubMsgEvent(Msg);
        }
    }

    
}
