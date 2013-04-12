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
    public partial class FrmEdtPatientBaseInfo : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        Database db;
        
        public PATIENT_BASEINFO patientBase = new PATIENT_BASEINFO();

        Int64 _ID;
        public FrmEdtPatientBaseInfo(Int64 id)
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
            _ID = id;

            //db.OpenSharedConnection();
            patientBase = db.Single<PATIENT_BASEINFO>("select * from PATIENT_BASEINFO where ID = @0", _ID);
            //db.CloseSharedConnection();

            pATIENTBASEINFOBindingSource.DataSource = patientBase;

            ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            ruleNoEmpty.ErrorText = "该项不能为空。";
            dxValidationProvider1.SetValidationRule(NAMETextEdit, ruleNoEmpty);

            MEDICARE_TYPETextEdit.Properties.DataSource = db.Fetch<VALUE_CODE>("select DSP_MEMBER, VALUE_MEMBER from VALUE_CODE where GROUPNAME = @0", new object[] { 10 });
            MEDICARE_TYPETextEdit.Properties.DisplayMember = "DSP_MEMBER";
            MEDICARE_TYPETextEdit.Properties.ValueMember = "VALUE_MEMBER";

            EDUCATIONAL_LEVELTextEdit.Properties.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 8 });
            EDUCATIONAL_LEVELTextEdit.Properties.DisplayMember = "DSP_MEMBER";
            EDUCATIONAL_LEVELTextEdit.Properties.ValueMember = "VALUE_MEMBER";

            MARITAL_STATUSTextEdit.Properties.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 9 });
            MARITAL_STATUSTextEdit.Properties.DisplayMember = "DSP_MEMBER";
            MARITAL_STATUSTextEdit.Properties.ValueMember = "VALUE_MEMBER";

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupname = 42");
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where groupname = 41");
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where groupname = 62");
            vALUECODEBindingSource3.DataSource = db.Fetch<VALUE_CODE>("where groupname = 63");
        }

        private void btnSaveAndExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!dxValidationProvider1.Validate())
                return;
            if (XtraMessageBox.Show("确定保存该患者基本信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                pATIENTBASEINFOBindingSource.EndEdit();
                pATIENTBASEINFOBindingSource.CurrencyManager.EndCurrentEdit();

                try
                {
                    //db.OpenSharedConnection();
                    patientBase.Update();
                    //db.CloseSharedConnection();
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

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}