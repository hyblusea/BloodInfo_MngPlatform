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
    public partial class FrmNewDose : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _baseID;
        Int64 _regID;

        DOSE_HISTORY doseHis = new  DOSE_HISTORY();

        public FrmNewDose(Int64 base_id, Int64 reg_id)
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            _regID = reg_id;
            _baseID = base_id;

            doseHis.REG_ID = _regID;
            doseHis.BASE_INFO_ID = _baseID;
            doseHis.OPERATOR = ClsFrmMng.WorkerID;
            dOSEHISTORYBindingSource.DataSource = doseHis;
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该患者基本信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                dOSEHISTORYBindingSource.EndEdit();
                dOSEHISTORYBindingSource.CurrencyManager.EndCurrentEdit();
                try
                {
                    doseHis.LOG_TIME = DateTime.Now;
                    db.Insert(doseHis);

                    doseHis = new  DOSE_HISTORY();
                    doseHis.REG_ID = _regID;
                    doseHis.BASE_INFO_ID = _baseID;
                    doseHis.OPERATOR = ClsFrmMng.WorkerID;
                    dOSEHISTORYBindingSource.DataSource = doseHis;

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