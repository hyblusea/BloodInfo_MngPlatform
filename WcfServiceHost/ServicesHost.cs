using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.Configuration;

namespace WcfServiceHost
{
    public partial class ServicesHost : Form
    {
        //ServiceHost server = null;
        Uri uri1 = new Uri(ConfigurationManager.AppSettings["HostUri1"]);
        Uri uri2 = new Uri(ConfigurationManager.AppSettings["HostUri2"]);
        DataTable dt = new DataTable("WcfServices");

        public ServicesHost()
        {
            InitializeComponent();
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("MetUrl", typeof(string));
            dt.Columns.Add("SrvHost", typeof(ServiceHost));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dt.Rows.Add(new object[] { "CommonServiceLibrary.CommonService", "Stop", uri1.ToString(), DBNull.Value });
            dt.Rows.Add(new object[] { "CommonServiceLibrary.BroadcastingSubscribe", "Stop", uri2.ToString(), DBNull.Value });
            bindingSource1.DataSource = dt;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current != null)
            {
                DataRowView dr = (DataRowView)bindingSource1.Current;
                var type = Type.GetType(string.Format("{0}, CommonServiceLibrary", dr["Name"].ToString()));
                var server = new ServiceHost(type);

                try
                {
                    if (dr["SrvHost"] == DBNull.Value)
                    {
                        dr["SrvHost"] = server;
                        ((ServiceHost)dr["SrvHost"]).Open();
                        dr["Status"] = "Run";
                        bindingSource1.ResetCurrentItem();
                    }
                    else if (((ServiceHost)dr["SrvHost"]).State != CommunicationState.Opened && ((ServiceHost)dr["SrvHost"]).State != CommunicationState.Opening)
                    {
                        dr["SrvHost"] = server;
                        ((ServiceHost)dr["SrvHost"]).Open();
                        dr["Status"] = "Run";
                        bindingSource1.ResetCurrentItem();
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }                
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current != null)
            {
                DataRowView dr = (DataRowView)bindingSource1.Current;

                try
                {
                    ((ServiceHost)dr["SrvHost"]).Close(new TimeSpan(0, 0, 15));
                    dr["Status"] = "Stop";
                    bindingSource1.ResetCurrentItem();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }
    }
}
