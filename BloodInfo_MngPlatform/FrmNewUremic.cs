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
    public partial class FrmNewUremic : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _baseId;
        Int64 _regID;

        UREMIC_SYMPTOMS_HI uremicHis = new  UREMIC_SYMPTOMS_HI();

        public FrmNewUremic(Int64 base_id, Int64 reg_id)
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            _regID = reg_id;
            _baseId = base_id;

            uremicHis.REG_ID = _regID;
            uremicHis.BASE_INFO_ID = base_id;
            uremicHis.OPERATOR = ClsFrmMng.WorkerID;
            uREMICSYMPTOMSHIBindingSource.DataSource = uremicHis;

            DIGESTIVETextEdit.Properties.DataSource = ClsFrmMng.lstDigesCode;
            DIGESTIVETextEdit.Properties.DisplayMember = "DIGESTIVESYS_MEMO";
            DIGESTIVETextEdit.Properties.ValueMember = "DIGESTIVESYS_MEMO";;

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupName = 192");
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where groupName = 189");
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where groupName = 190");
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该患者基本信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                uREMICSYMPTOMSHIBindingSource.EndEdit();
                uREMICSYMPTOMSHIBindingSource.CurrencyManager.EndCurrentEdit();
                try
                {
                    uremicHis.LOG_TIME = DateTime.Now;
                    db.Insert(uremicHis);

                    uremicHis = new  UREMIC_SYMPTOMS_HI();
                    uremicHis.REG_ID = _regID;
                    uremicHis.BASE_INFO_ID = _baseId;
                    uremicHis.OPERATOR = ClsFrmMng.WorkerID;
                    uREMICSYMPTOMSHIBindingSource.DataSource = uremicHis;

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

        private void CARDIOVASCULAR_PAINTextEdit_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}