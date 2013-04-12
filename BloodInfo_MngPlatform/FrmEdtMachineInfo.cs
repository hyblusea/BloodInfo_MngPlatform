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
    public partial class FrmEdtMachineInfo : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        Database db;

        public MACHINE_INFO mach = new MACHINE_INFO();
        Int64 _id;

        public FrmEdtMachineInfo(Int64 id)
        {
            InitializeComponent();
            db = new Database("XE");
            _id = id;

            vALUEGROUPBindingSource.DataSource = db.Fetch<VALUE_GROUP>("where FATHERID = @0", 15);
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 17);      // 耗材供应商 

            ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            ruleNoEmpty.ErrorText = "该项不能为空。";
            dxValidationProvider1.SetValidationRule(MACHINETYPETextEdit, ruleNoEmpty);                  // 设备类型
            dxValidationProvider1.SetValidationRule(MODELTextEdit, ruleNoEmpty);                        // 设备型号
            dxValidationProvider1.SetValidationRule(SNTextEdit, ruleNoEmpty);                           // 设备序列号
            dxValidationProvider1.SetValidationRule(BED_NOTextEdit, ruleNoEmpty);                       // 所在床位号

            mach = db.Single<MACHINE_INFO>("where ID = @0", _id);
            mACHINEINFOBindingSource.DataSource = mach;
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (dxValidationProvider1.Validate())
                {
                    mACHINEINFOBindingSource.EndEdit();
                    mACHINEINFOBindingSource.CurrencyManager.EndCurrentEdit();

                    try
                    {
                        mach.Update();

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

        private void barExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void MACHINETYPETextEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (MACHINETYPETextEdit.EditValue == null || MACHINETYPETextEdit.EditValue.ToString() == "")
            {
                MODELTextEdit.EditValue = null;
                vALUECODEBindingSource.DataSource = null;
            }
            else
                vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", MACHINETYPETextEdit.EditValue);
        }
    }
}