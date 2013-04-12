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
    public partial class FrmEdtPatient_Course_Sp : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        Database db;

        public PATIENT_COURSE_SPECIAL v = new PATIENT_COURSE_SPECIAL();
        Int64 _id;

        public FrmEdtPatient_Course_Sp(Int64 id)
        {
            InitializeComponent();
            db = new Database("XE");
            _id = id;

            //ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            //ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            //ruleNoEmpty.ErrorText = "该项不能为空。";
            //dxValidationProvider1.SetValidationRule(INFECTIOUS_DISEASETextEdit, ruleNoEmpty);

            //vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 137);

            v = db.Single<PATIENT_COURSE_SPECIAL>("where ID = @0", _id);
            pATIENTCOURSESPECIALBindingSource.DataSource = v;
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (dxValidationProvider1.Validate())
                {
                    pATIENTCOURSESPECIALBindingSource.EndEdit();
                    pATIENTCOURSESPECIALBindingSource.CurrencyManager.EndCurrentEdit();

                    try
                    {
                        v.Update();

                        if (NewRegistEvt != null)
                            NewRegistEvt();

                        this.Close();
                    }
                    catch (Exception err)
                    {
                        XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

    }
}