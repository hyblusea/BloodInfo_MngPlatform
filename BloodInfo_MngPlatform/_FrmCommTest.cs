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
using System.Threading;

using PetaPoco;
using BloodInfo_MngPlatform.Models;
using System.Configuration;

namespace BloodInfo_MngPlatform
{
    public partial class _FrmCommTest : DevExpress.XtraEditors.XtraForm
    {
        SerialPortManager _spManager;
        Dictionary<string, SerialPortManager> dicSerialPort = new Dictionary<string, SerialPortManager>();
        bool isRun = true;

        static bool isView = true;


        Dictionary<string, PetaPoco.Database> dicPetaPoco = new Dictionary<string, Database>();
        object o = new object();

        public _FrmCommTest()
        {
            InitializeComponent();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            
            
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
         
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            
        }

        void sl_ClientConnected(string msgContent, string remoteIP, int remotePort)
        {
            
        }

        void sl_MsgReceived(string msgContent, string remoteIP, int remotePort)
        {
           
        }







        
        TcpClient clt = new TcpClient();
        private void btnConnToSvr_Click(object sender, EventArgs e)
        {
           
        }

        private void btnSendToSvr_Click(object sender, EventArgs e)
        {
   
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            sl.Stop();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
          
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
            
        }

        private void _FrmCommTest_Load(object sender, EventArgs e)
        {
            _spManager = new SerialPortManager();
            SerialSettings mySerialSettings = _spManager.CurrentSerialSettings;

            for (int i = 0; i < mySerialSettings.PortNameCollection.Length; i++)
            {
                TreeNode tn = new TreeNode();
                tn.Text = mySerialSettings.PortNameCollection[i];
                treeView1.Nodes.Add(tn);

                dicSerialPort.Add(tn.Text, new SerialPortManager());
            }
        }

        private void Table1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                isRun = true;
                backgroundWorker1.RunWorkerAsync();
                toolStripButton1.Enabled = false;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] by = new byte[] { 0x4b, 0x0d, 0x0a };

            // 打开所有串口
            foreach (KeyValuePair<string, SerialPortManager> item in dicSerialPort)
            {
                try
                {
                    item.Value.StartListening(item.Key, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                }
                catch (Exception err)
                {
                    tbData.SafeCall(delegate()
                    {
                        tbData.AppendText("初始化串口" + item.Key + "发生异常: " + err.Message + Environment.NewLine);
                    });
                }
            }

            while (isRun)
            {
                foreach (KeyValuePair<string, SerialPortManager> item in dicSerialPort)
                {
                    try
                    {
                        item.Value.SendMsg(by);
                    }
                    catch (Exception err)
                    {
                        tbData.SafeCall(delegate()
                        {
                            tbData.AppendText("向串口" + item.Key + "发送数据失败: " + err.Message);
                        });
                    }
                }

                tbData.SafeCall(delegate()
                {
                    tbData.Clear();
                });

                foreach (KeyValuePair<string, SerialPortManager> item in dicSerialPort)
                {
                    try
                    {
                        string sData = item.Value.ReadData();
                        tbData.SafeCall(delegate()
                        {
                            tbData.AppendText( DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + item.Key + ": "  + sData + Environment.NewLine);
                        });

                        if (sData.Length > 20)
                        {
                            try
                            {
                                // 写入数据库
                                PetaPoco.Database db = new Database("XE");

                                DEVICECOMMUNICATION_LOG log = new DEVICECOMMUNICATION_LOG();
                                log.MSG = sData.Length > 3999 ? sData.Substring(0, 3999) : sData;
                                log.REMOTE_IP = "com3";
                                //log.REMOTE_PORT = remotePort;
                                log.RECEIVE_TIME = DateTime.Now;

                                db.Insert(log);

                                // 写入到台湾人要的表
                                var log1 = GetLog1Entity(sData, item.Key);
                                db.Insert(log1);
                            }
                            catch (Exception err)
                            {
                                tbData.SafeCall(delegate()
                                {
                                    tbData.AppendText("写数据失败: " + err.Message + Environment.NewLine);
                                });
                            }
                            
                        }
                    }
                    catch (Exception err)
                    {
                        tbData.SafeCall(delegate()
                        {
                            tbData.AppendText("读取串口" + item.Key + "数据失败: " + err.Message + Environment.NewLine);
                        });
                    }
                }
                Thread.Sleep(5000);                
                
            }

        }

        private DEVICECOMMUNICATION_LOG1 GetLog1Entity(string sData, string sSerialPortNum)
        {
            DEVICECOMMUNICATION_LOG1 log1 = new DEVICECOMMUNICATION_LOG1();
            log1.REMOTE_IP = sSerialPortNum;
            log1.RECEIVE_TIME = DateTime.Now;
            log1.MSG = sData;

            log1.A = sData.Substring(sData.LastIndexOf('A') + 1, 5);
            log1.B = sData.Substring(sData.LastIndexOf('B') + 1, 5);
            log1.C = sData.Substring(sData.LastIndexOf('C') + 1, 5);
            log1.D = sData.Substring(sData.LastIndexOf('D') + 1, 5);
            log1.E = sData.Substring(sData.LastIndexOf('E') + 1, 5);
            log1.F = sData.Substring(sData.LastIndexOf('F') + 1, 5);
            log1.G = sData.Substring(sData.LastIndexOf('G') + 1, 5);
            log1.H = sData.Substring(sData.LastIndexOf('H') + 1, 5);
            log1.I = sData.Substring(sData.LastIndexOf('I') + 1, 5);
            log1.J = sData.Substring(sData.LastIndexOf('J') + 1, 5);
            log1.K = sData.Substring(sData.LastIndexOf('K') + 1, 5);
            log1.L = sData.Substring(sData.LastIndexOf('L') + 1, 5);
            log1.M = sData.Substring(sData.LastIndexOf('M') + 1, 5);
            log1.N = sData.Substring(sData.LastIndexOf('N') + 1, 5);
            log1.O = sData.Substring(sData.LastIndexOf('O') + 1, 5);
            log1.P = sData.Substring(sData.LastIndexOf('P') + 1, 5);
            log1.Q = sData.Substring(sData.LastIndexOf('Q') + 1, 5);
            log1.R = sData.Substring(sData.LastIndexOf('R') + 1, 5);
            log1.S = sData.Substring(sData.LastIndexOf('S') + 1, 5);
            log1.T = sData.Substring(sData.LastIndexOf('T') + 1, 5);
            log1.U = sData.Substring(sData.LastIndexOf('U') + 1, 5);
            log1.V = sData.Substring(sData.LastIndexOf('V') + 1, 5);
            log1.W = sData.Substring(sData.LastIndexOf('W') + 1, 5);
            log1.X = sData.Substring(sData.LastIndexOf('X') + 1, 5);
            log1.Y = sData.Substring(sData.LastIndexOf('Y') + 1, 5);
            log1.Z = sData.Substring(sData.LastIndexOf('Z') + 1, 5);
            log1.A_A = sData.Substring(sData.LastIndexOf("a") + 1, 1);
            log1.A_B = sData.Substring(sData.LastIndexOf("b") + 1, 1);
            log1.A_C = sData.Substring(sData.LastIndexOf("c") + 1, 1);
            log1.A_D = sData.Substring(sData.LastIndexOf("d") + 1, 1);
            log1.A_E = sData.Substring(sData.LastIndexOf("e") + 1, 1);
            log1.A_F = sData.Substring(sData.LastIndexOf("f") + 1, 1);
            log1.A_G = sData.Substring(sData.LastIndexOf("g") + 1, 1);
            log1.A_H = sData.Substring(sData.LastIndexOf("h") + 1, 1);
            log1.A_I = sData.Substring(sData.LastIndexOf("i") + 1, 5);
            log1.A_J = sData.Substring(sData.LastIndexOf("j") + 1, 1);
            log1.A_K = sData.Substring(sData.LastIndexOf("k") + 1, 1);
            log1.A_L = sData.Substring(sData.LastIndexOf("l") + 1, 1);
            log1.A_M = sData.Substring(sData.LastIndexOf("m") + 1, 1);
            log1.A_N = sData.Substring(sData.LastIndexOf("n") + 1, 8);
            log1.A_O = sData.Substring(sData.LastIndexOf("o") + 1, 5);
            log1.A_P = sData.Substring(sData.LastIndexOf("p") + 1, 1);
            log1.A_Q = sData.Substring(sData.LastIndexOf("q") + 1, 5);
            log1.A_R = sData.Substring(sData.LastIndexOf("r") + 1, 5);
            log1.A_S = sData.Substring(sData.LastIndexOf("s") + 1, 5);
            log1.A_T = sData.Substring(sData.LastIndexOf("t") + 1, 5);
            log1.A_U = sData.Substring(sData.LastIndexOf("u") + 1, 5);
            log1.A_V = sData.Substring(sData.LastIndexOf("v") + 1, 5);
            log1.A_W = sData.Substring(sData.LastIndexOf("w") + 1, 5);
            log1.A_X = sData.Substring(sData.LastIndexOf("x") + 1, 1);
            log1.A_Y = sData.Substring(sData.LastIndexOf("y") + 1, 1);
            log1.A_Z = sData.Substring(sData.LastIndexOf("z") + 1, 1);
            log1.DIALYSATE_PROFILE = sData.Substring(sData.LastIndexOf("!") + 1, 1);
            log1.DIALYSATE_TEMPERATURE_SV = sData.Substring(sData.LastIndexOf("#") + 1, 5);
            log1.WATER_SHORT_2_ALARM = sData.Substring(sData.LastIndexOf("$") + 1, 1);
            return log1;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            isRun = false;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripButton1.Enabled = true;
            foreach (KeyValuePair<string, SerialPortManager> item in dicSerialPort)
            {
                try
                {
                    item.Value.StopListening();
                }
                catch (Exception err)
                {
                    tbData.SafeCall(delegate()
                    {
                        tbData.AppendText("关闭串口" + item.Key + "发生异常: " + err.Message + Environment.NewLine);
                    });
                }
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