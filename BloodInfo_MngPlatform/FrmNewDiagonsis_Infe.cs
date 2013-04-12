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
    public partial class FrmNewDiagonsis_Infe : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _baseID;

        DIAGNOSIS_INFECTIOUS_DISEASE diag = new    DIAGNOSIS_INFECTIOUS_DISEASE();

        public FrmNewDiagonsis_Infe(Int64 base_id)
        {
            InitializeComponent();
            db = new Database("XE");

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 137);

            dIAGNOSISINFECTIOUSDISEASEBindingSource.DataSource = diag;

            //ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            //ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            //ruleNoEmpty.ErrorText = "该项不能为空。";
            //dxValidationProvider1.SetValidationRule(INFECTIOUS_DISEASETextEdit, ruleNoEmpty);

            _baseID = base_id;
        }

        private void SaveData()
        {
            dIAGNOSISINFECTIOUSDISEASEBindingSource.EndEdit();
            dIAGNOSISINFECTIOUSDISEASEBindingSource.CurrencyManager.EndCurrentEdit();

            diag.PT_ID = _baseID;
            diag.LOG_TIME = DateTime.Now;
            diag.OPERATOR = ClsFrmMng.WorkerID;

            db.Insert(diag);
            if (NewRegistEvt != null)
                NewRegistEvt();

            diag = new DIAGNOSIS_INFECTIOUS_DISEASE();
            dIAGNOSISINFECTIOUSDISEASEBindingSource.DataSource = diag;
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if (dxValidationProvider1.Validate())
                        SaveData();
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
                }
            }
        }

        private void btnSaveAndExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该信息,并关闭该窗口？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if (dxValidationProvider1.Validate())
                    {
                        SaveData();
                        this.Close();
                    }
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
                }
            }            
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}