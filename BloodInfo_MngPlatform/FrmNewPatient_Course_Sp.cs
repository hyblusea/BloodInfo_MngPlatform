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
    public partial class FrmNewPatient_Course_Sp : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _baseID;

        PATIENT_COURSE_SPECIAL v = new PATIENT_COURSE_SPECIAL();

        public FrmNewPatient_Course_Sp(Int64 base_id)
        {
            InitializeComponent();
            db = new Database("XE");

            //vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 137);

            pATIENTCOURSESPECIALBindingSource.DataSource = v;

            //ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            //ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            //ruleNoEmpty.ErrorText = "该项不能为空。";
            //dxValidationProvider1.SetValidationRule(INFECTIOUS_DISEASETextEdit, ruleNoEmpty);

            _baseID = base_id;
        }

        private void SaveData()
        {
            pATIENTCOURSESPECIALBindingSource.EndEdit();
            pATIENTCOURSESPECIALBindingSource.CurrencyManager.EndCurrentEdit();

            v.PT_ID = _baseID;
            v.LOG_TIME = DateTime.Now;
            v.OPERATOR = ClsFrmMng.WorkerID;

            db.Insert(v);
            if (NewRegistEvt != null)
                NewRegistEvt();

            v = new PATIENT_COURSE_SPECIAL();
            pATIENTCOURSESPECIALBindingSource.DataSource = v;
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