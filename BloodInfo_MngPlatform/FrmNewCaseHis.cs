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
    public partial class FrmNewCaseHis : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        Database db;
        
        public CASE_HISTORY caseHis = new CASE_HISTORY();

        Int64 _reg_id = 0;
        bool _isAdd = false;
        Int64 _baseID = 0;
        public FrmNewCaseHis(bool isAdd, Int64 base_id, Int64 reg_id)
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            _reg_id = reg_id;
            _isAdd = isAdd;
            _baseID = base_id;

            //ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            //ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            //ruleNoEmpty.ErrorText = "该项不能为空。";
            //dxValidationProvider1.SetValidationRule(MEMOMemoEdit, ruleNoEmpty);
            //dxValidationProvider1.SetValidationRule(DOSE_MEMOMemoEdit, ruleNoEmpty);

            caseHis.REG_ID = _reg_id;
            caseHis.BASE_INFO_ID = _baseID;
            caseHis.OPERATOR = ClsFrmMng.WorkerID;
            cASEHISTORYBindingSource.DataSource = caseHis;
            
            lookUpEdit1.Properties.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 11 });
            lookUpEdit1.Properties.DisplayMember = "DSP_MEMBER";
            lookUpEdit1.Properties.ValueMember = "VALUE_MEMBER";

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 181 });
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!dxValidationProvider1.Validate())
                return;
            if (XtraMessageBox.Show("确定保存该患者基本信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                cASEHISTORYBindingSource.EndEdit();
                cASEHISTORYBindingSource.CurrencyManager.EndCurrentEdit();
                try
                {
                    //db.OpenSharedConnection();
                    caseHis.LOG_DATE = DateTime.Now;
                    db.Insert(caseHis);
                    //db.CloseSharedConnection();

                    caseHis = new CASE_HISTORY();
                    caseHis.REG_ID = _reg_id;
                    caseHis.BASE_INFO_ID = _baseID;
                    caseHis.OPERATOR = ClsFrmMng.WorkerID;
                    cASEHISTORYBindingSource.DataSource = caseHis;

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
            if (!dxValidationProvider1.Validate())
                return;
            else
            {
                btnSave_ItemClick(null, null);
                this.Close();
            }
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void textEdit2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FrmNewCaseHis_Load(object sender, EventArgs e)
        {

        }
    }
}