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
    public partial class FrmEdtUremic : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        Database db;

        public UREMIC_SYMPTOMS_HI uremicHis = new  UREMIC_SYMPTOMS_HI();
        Int64 _id;

        public FrmEdtUremic(Int64 id)
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
            _id = id;

            uremicHis = db.Single<UREMIC_SYMPTOMS_HI>("select * from UREMIC_SYMPTOMS_HIS where ID = @0", _id);
            uREMICSYMPTOMSHIBindingSource.DataSource = uremicHis;

            DIGESTIVETextEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            DIGESTIVETextEdit.Properties.DataSource = ClsFrmMng.lstIndecumentCode;
            DIGESTIVETextEdit.Properties.DisplayMember = "INDUCEMENT_MEMO";
            DIGESTIVETextEdit.Properties.ValueMember = "INDUCEMENT_MEMO";

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupName = 192");
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where groupName = 189");
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where groupName = 190");
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!dxValidationProvider1.Validate())
            //    return;
            if (XtraMessageBox.Show("确定保存该患者基本信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                uREMICSYMPTOMSHIBindingSource.EndEdit();
                uREMICSYMPTOMSHIBindingSource.CurrencyManager.EndCurrentEdit();

                try
                {
                    uremicHis.Update();
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
                }
                if (NewRegistEvt != null)
                    NewRegistEvt();
                this.Close();
            }
        }

        private void barExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}