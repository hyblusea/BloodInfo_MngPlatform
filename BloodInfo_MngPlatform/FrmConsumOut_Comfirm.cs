using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Configuration;
using PetaPoco;
using BloodInfo_MngPlatform.Models;

namespace BloodInfo_MngPlatform
{
    public partial class FrmConsumOut_Comfirm : DevExpress.XtraEditors.XtraForm
    {
        Database db;
        List<CONSUMABLES_LOG1> _lst;        // 日志表
        List<CONSUMABLES_LOG> _consumLst;   // 入库表

        public FrmConsumOut_Comfirm(List<CONSUMABLES_LOG1> lst, List<CONSUMABLES_LOG>  lst1)
        {
            InitializeComponent();

            ////string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");


            _lst = lst;
            _consumLst = lst1;
            cONSUMABLESLOG1BindingSource.DataSource = lst;
            aCCOUNTBindingSource.DataSource = db.Fetch<ACCOUNT>("");
            vALUECODEBindingSource3.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", new object[] { 20 });     // 操作类型
        }

        private void btnComfirm_Click(object sender, EventArgs e)
        {
            try
            {
                var k = db.Fetch<ACCOUNT>("select * from ACCOUNT where WORKID = @0 and Pwd = @1", new object[] { txtUserID.EditValue.ToString(), txtPwd.Text });
                if (k != null && k.Count == 1)
                {
                    try
                    {
                        using (var scope = db.GetTransaction())
                        {
                            for (int j = 0; j < _consumLst.Count; j++)
                            {
                                // 修改剩量
                                _consumLst[j].Update();
                            }
                            for (int i = 0; i < _lst.Count; i++)
                            {
                                // 写入入库日志表
                                _lst[i].CONFIRMOR = txtUserID.EditValue.ToString();
                                db.Insert("CONSUMABLES_LOG1", "ID", _lst[i]);
                            }
                            scope.Complete();

                            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                            this.Close();
                        }
                    }
                    catch (Exception err)
                    {
                        XtraMessageBox.Show("提交过程中出现异常, 原因: " + err.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }                
                }
                else
                {
                    XtraMessageBox.Show("用户名或密码错误。", "错误提示", MessageBoxButtons.OK);
                }
            }
            catch (Exception err)
            {
                XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
            }

            

            
        }
    }
}