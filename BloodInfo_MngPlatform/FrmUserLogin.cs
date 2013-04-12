using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using System.Configuration;
using PetaPoco;
using BloodInfo_MngPlatform.Models;

namespace BloodInfo_MngPlatform
{
    public partial class FrmUserLogin : XtraForm
    {
        Database db;
        FrmMain frmMain = null;
        public FrmUserLogin()
        {
            InitializeComponent();
            try
            {
                txtUserID.Text = ConfigurationManager.AppSettings["UserID"];
            }
            catch { txtUserID.Text = ""; }

            try
            {
                //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
                db = new Database("XE");
            }
            catch (Exception err)
            {
                XtraMessageBox.Show(err.Message, "错误提示");
            }
        }

        private void btnEntry_Click(object sender, EventArgs e)
        {
            try
            {
                splashScreenManager1.ShowWaitForm();
                //db.OpenSharedConnection();
                var i = db.Fetch<ACCOUNT>("where WORKID = @0 and Pwd = @1", new object[] { txtUserID.Text, txtPwd.Text });
                //db.CloseSharedConnection();
                if (i != null && i.Count == 1)
                {
                    frmMain = new FrmMain(this, txtUserID.Text, i[0].USERNAME, (int)i[0].ROLE_GROUP);
                    frmMain.Show();
                    splashScreenManager1.CloseWaitForm();
                    this.Visible = false;
                }
                else
                {
                    splashScreenManager1.CloseWaitForm();
                    XtraMessageBox.Show("用户名或密码错误。", "错误提示", MessageBoxButtons.OK);
                }
            }
            catch (Exception err)
            {
                splashScreenManager1.CloseWaitForm();
                XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
