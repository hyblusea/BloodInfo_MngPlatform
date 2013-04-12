using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using BloodInfo_MngPlatform.Models;
using PetaPoco;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace BloodInfo_MngPlatform
{
    public partial class Rpt_bloodCleanup : DevExpress.XtraReports.UI.XtraReport
    {
        Database db;
        List<VALUE_CODE> _lstCode;

        public Rpt_bloodCleanup(string Name, int Age, string Sex,List<BLOODCLEANUP_PROCESS> lstProcess, BLOODCLEANUP Blood, List<VALUE_CODE> lstCode)
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            lblName.Text = Name;
            lblAge.Text = Age.ToString();
            lblSex.Text = Sex;
            _lstCode = lstCode;

            bindingSource1.DataSource = Blood;
            bindingSource2.DataSource = lstProcess;
        }

        public void SetDspMember()
        {
            //int i = 0;
            //i = string.IsNullOrEmpty(cleanupType.Text) ? -1 : Convert.ToInt32(cleanupType.Text);
            //cleanupType.Text = db.ExecuteScalar<string>("select dsp_member from VALUE_CODE where groupname = 1 and value_member = @0", new object[] { });
        }

        private void cleanupType_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            object id = GetCurrentColumnValue("CLEANUP_TYPE");

            try
            {
                if (id != null)
                    cleanupType.Text = Convert.ToString(_lstCode.Where(p => p.VALUE_MEMBER == Convert.ToDecimal(id)).FirstOrDefault().DSP_MEMBER);
            }
            catch {MessageBox.Show( " cleanupType.Text:" + cleanupType.Text + ".转换失败."); }
        }

        private void xrLabel56_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            object id = GetCurrentColumnValue("BLOOD_PASS");

            try
            {
                if (id != null)
                    xrLabel56.Text = Convert.ToString(_lstCode.Where(p => p.VALUE_MEMBER == Convert.ToDecimal(id)).FirstOrDefault().DSP_MEMBER);
            }
            catch {MessageBox.Show( "xrLabel56.Text:" + xrLabel56.Text + ".转换失败."); }
        }

        private void xrLabel59_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            object id = GetCurrentColumnValue("SYMPTOM");

            if (id != null)
            {
                try
                {
                    if (Convert.ToInt32(id) == 1)
                        xrLabel59.Text = "有";
                    else
                        xrLabel59.Text = "无";
                }
                catch { MessageBox.Show("id: " + id.ToString() + ".转换失败.");  }
            }
        }

    }
}
