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
    public partial class FrmNewPatientBaseInfo : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        Database db;
        
        public PATIENT_BASEINFO patientBase = new PATIENT_BASEINFO();
        public FrmNewPatientBaseInfo()
        {
            InitializeComponent();

            ////string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
            ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            ruleNoEmpty.ErrorText = "该项不能为空。";
            dxValidationProvider1.SetValidationRule(NAMETextEdit, ruleNoEmpty);
            dxValidationProvider1.SetValidationRule(ID_CODETextEdit, ruleNoEmpty);

            pATIENTBASEINFOBindingSource.DataSource = patientBase;

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
            pATIENTBASEINFOBindingSource.EndEdit();
            pATIENTBASEINFOBindingSource.CurrencyManager.EndCurrentEdit();

            if (!dxValidationProvider1.Validate())
                return;
            barButtonItem1_ItemClick(null, null);
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    patientBase.CREATE_TIME = DateTime.Now;
                    var id = db.Insert(patientBase);
                    Console.WriteLine(id.ToString());
                    //db.CloseSharedConnection();

                    patientBase = new PATIENT_BASEINFO();
                    pATIENTBASEINFOBindingSource.DataSource = patientBase;
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
                }

                if (NewRegistEvt != null)
                    NewRegistEvt();
            }
        }
    }
}