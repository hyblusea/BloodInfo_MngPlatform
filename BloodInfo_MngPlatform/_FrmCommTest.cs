using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SerialPortComm;

using System.Net.Sockets;
using System.Net;
using SocketAsyncServer;

using PetaPoco;
using BloodInfo_MngPlatform.Models;
using System.Configuration;

namespace BloodInfo_MngPlatform
{
    public partial class _FrmCommTest : DevExpress.XtraEditors.XtraForm
    {
        SerialPortManager _spManager;

        static bool isView = true;


        Dictionary<string, PetaPoco.Database> dicPetaPoco = new Dictionary<string, Database>();
        object o = new object();

        public _FrmCommTest()
        {
            InitializeComponent();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            _spManager = new SerialPortManager();

            SerialSettings mySerialSettings = _spManager.CurrentSerialSettings;
            portNameComboBox.DataSource = mySerialSettings.PortNameCollection;
            baudRateComboBox.DataSource = mySerialSettings.BaudRateCollection;
            dataBitsComboBox.DataSource = mySerialSettings.DataBitsCollection;
            parityComboBox.DataSource = Enum.GetValues(typeof(System.IO.Ports.Parity));
            stopBitsComboBox.DataSource = Enum.GetValues(typeof(System.IO.Ports.StopBits));

            _spManager.NewSerialDataRecieved += _spManager_NewSerialDataRecieved;
            this.FormClosing += _FrmCommTest_FormClosing;
        }

        void _FrmCommTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            _spManager.Dispose();   
        }

        void _spManager_NewSerialDataRecieved(object sender, SerialDataEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<SerialDataEventArgs>(_spManager_NewSerialDataRecieved), new object[] { sender, e });
                return;
            }

            int maxTextLength = 3000;
            if (tbData.TextLength > maxTextLength)
                tbData.Text = tbData.Text.Remove(0, tbData.TextLength - maxTextLength);

            string str = Encoding.ASCII.GetString(e.Data);
            tbData.AppendText(str + Environment.NewLine);
            tbData.ScrollToCaret();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _spManager.StartListening(portNameComboBox.Text, Convert.ToInt32(baudRateComboBox.SelectedItem),
                (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), parityComboBox.SelectedItem.ToString(), false),
                Convert.ToInt32( dataBitsComboBox.Text),
                (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), stopBitsComboBox.SelectedItem.ToString(), false)
                );

            timer1.Enabled = true;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            _spManager.StopListening();

            timer1.Enabled = false;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            _spManager.SendDataRequest();
        }




        // Socket Server

        private const int DEFAULT_PORT = 9900, DEFAULT_NUM_CONNECTIONS = 3000, DEFAULT_BUFFER_SIZE = Int16.MaxValue;
        SocketAsyncServer.SocketListener sl;

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            System.Net.IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            cbxIP.DataSource = addressList;
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
             sl = new SocketListener(DEFAULT_NUM_CONNECTIONS, DEFAULT_BUFFER_SIZE);

             try
             {
                 sl.Start(cbxIP.Text, Convert.ToInt32(txtPort.Text));
             }
             catch { sl.Start(cbxIP.Text, DEFAULT_PORT); }

             sl.MsgReceived += sl_MsgReceived;
             sl.ClientConnected +=sl_ClientConnected;
        }

        void sl_ClientConnected(string msgContent, string remoteIP, int remotePort)
        {
            txtSocketSvr.SafeCall(delegate()
            {
                txtSocketSvr.AppendText(msgContent + " ," +  remoteIP + ": " + remotePort);
            });
        }

        void sl_MsgReceived(string msgContent, string remoteIP, int remotePort)
        {
            if (isView)
            {
                txtSocketSvr.SafeCall(delegate()
                {
                    int maxTextLength = 3000;
                    if (txtSocketSvr.TextLength > maxTextLength)
                        txtSocketSvr.Text = txtSocketSvr.Text.Remove(0, txtSocketSvr.TextLength - maxTextLength);

                    txtSocketSvr.AppendText("Received from :" + remoteIP + ":" + remotePort.ToString() + ";  [" + msgContent + "]" + Environment.NewLine);
                    txtSocketSvr.ScrollToCaret();
                });
            }

            PetaPoco.Database db;
            lock (o)
            {
                dicPetaPoco.TryGetValue(remoteIP, out db);
            }

            if (db == null)
            {
                db = new Database("XE");
                //db.KeepConnectionAlive = true;        // 性能无提升

                lock (o)
                {
                    dicPetaPoco.Add(remoteIP, db);
                }
            }

            // 写入数据库
            DEVICECOMMUNICATION_LOG log = new DEVICECOMMUNICATION_LOG();
            log.MSG =  msgContent.Length > 3999 ? msgContent.Substring(0, 3999): msgContent;
            log.REMOTE_IP = remoteIP;
            log.REMOTE_PORT = remotePort;
            log.RECEIVE_TIME = DateTime.Now;
            //log.Insert();                             // 性能太低
            //db.Execute("insert into DEVICECOMMUNICATION_LOG (MSG,REMOTE_IP, REMOTE_PORT,RECEIVE_TIME  ) values (@0, @1, @2, @3)", new object[] { msgContent, remoteIP, remotePort, DateTime.Now});

            lock (db)
            {
                try
                {
                    db.Insert(log);
                }
                catch { }
            }
        }







        
        TcpClient clt = new TcpClient();
        private void btnConnToSvr_Click(object sender, EventArgs e)
        {
            clt.Connect(txtSvrIP.Text, Convert.ToInt32(txtSrvPort.Text));
        }

        private void btnSendToSvr_Click(object sender, EventArgs e)
        {
            byte[] by = Encoding .ASCII.GetBytes(txtSendToSvr.Text);
            clt.Client.Send(by);
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            sl.Stop();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            isView = checkEdit1.Checked;
        }

        private void triStateTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            _spManager.SendDataRequest2();
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 发送数据
            byte[] by = new byte[] { 0x4b, 0x0d, 0x0a };
            _spManager.SendMsg(by);

            //  获取串口返回
            string sData = _spManager.ReadData();
            tbData.Text =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + sData;

            if (sData.Length > 20)
            {
                // 写入数据库
                PetaPoco.Database db = new Database("XE");

                DEVICECOMMUNICATION_LOG log = new DEVICECOMMUNICATION_LOG();
                log.MSG = sData.Length > 3999 ? sData.Substring(0, 3999) : sData;
                log.REMOTE_IP = "com3";
                //log.REMOTE_PORT = remotePort;
                log.RECEIVE_TIME = DateTime.Now;

                db.Insert(log);
            }
        }
    }

    public static class Extensions
    {
        public static void SafeCall(this Control ctrl, Action callback)
        {
            try
            {
                if (ctrl.InvokeRequired)
                    ctrl.Invoke(callback);
                else
                    callback();
            }
            catch { }
        }
    }
}