using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;
using PetaPoco;
using BloodInfo_MngPlatform.Models;
using System.Linq;


namespace BloodInfo_MngPlatform
{
    public partial class _FrmModifyPwd : XtraForm
    {
        Database db;
        public _FrmModifyPwd()
        {
            InitializeComponent();
            this.txtOldPwd.Properties.PasswordChar = '●';
            this.txtNewPwd.Properties.PasswordChar = '●';
            this.txtNewPwdComfirm.Properties.PasswordChar = '●';

            ////string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
        }

        private void btnEntry_Click(object sender, EventArgs e)
        {
            try
            {
                //db.OpenSharedConnection();
                //var i = db.Fetch<ACCOUNT>(string.Format("select * from ACCOUNT where WORKID = '{0}' and Pwd = '{1}'", ClsFrmMng.WorkerID, txtOldPwd.Text));
                var i = db.Fetch<ACCOUNT>("select * from ACCOUNT where WORKID = @0 and Pwd = @1", new object[] { ClsFrmMng.WorkerID, txtOldPwd.Text });
                //db.CloseSharedConnection();
                if (i != null && i.Count == 1)
                {
                    if (txtNewPwd.Text != txtNewPwdComfirm.Text)
                        XtraMessageBox.Show("两次输入的新密码不一致。", "错误提示", MessageBoxButtons.OK);
                    else
                    {
                        db.Execute("update ACCOUNT set PWD = @0 where WORKID = @1", txtNewPwdComfirm.Text, ClsFrmMng.WorkerID);
                        XtraMessageBox.Show("密码修改成功。", "信息提示", MessageBoxButtons.OK);
                        this.Close();
                    }
                }
                else
                {
                    XtraMessageBox.Show("旧密码密码错误，请重新输入。", "错误提示", MessageBoxButtons.OK);
                }
            }
            catch (Exception err)
            {
                XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
            }
        }
    }
}
