using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Configuration;
using PetaPoco;
using BloodInfo_MngPlatform.Models;

namespace BloodInfo_MngPlatform
{
    public partial class FrmNewBodyCheck : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _baseID;
        Int64 _regID;

        BODY_CHECK_HISTORY bodyHis = new    BODY_CHECK_HISTORY();

        public FrmNewBodyCheck(Int64 base_id, Int64 reg_id)
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            _regID = reg_id;
            _baseID = base_id;

            bodyHis.REG_ID = _regID;
            bodyHis.BASE_INFO_ID = base_id;
            bodyHis.OPERATOR = ClsFrmMng.WorkerID;
            bODYCHECKHISTORYBindingSource.DataSource = bodyHis;

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupName = 208");
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该患者基本信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                bODYCHECKHISTORYBindingSource.EndEdit();
                bODYCHECKHISTORYBindingSource.CurrencyManager.EndCurrentEdit();
                try
                {
                    bodyHis.LOG_TIME = DateTime.Now;
                    db.Insert(bodyHis);

                    bodyHis = new BODY_CHECK_HISTORY();
                    bodyHis.REG_ID = _regID;
                    bodyHis.BASE_INFO_ID = _baseID;
                    bodyHis.OPERATOR = ClsFrmMng.WorkerID;
                    bODYCHECKHISTORYBindingSource.DataSource = bodyHis;

                    if (NewRegistEvt != null)
                        NewRegistEvt();
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
                }
            }
        }

        private void btnSaveAndExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnSave_ItemClick(null, null);
            this.Close();
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}