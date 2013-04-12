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
    public partial class FrmNewMachineInfo : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _baseID;

        MACHINE_INFO mach = new MACHINE_INFO();

        public FrmNewMachineInfo(Int64 base_id)
        {
            InitializeComponent();
            db = new Database("XE");

            vALUEGROUPBindingSource.DataSource = db.Fetch<VALUE_GROUP>("where FATHERID = @0", 15);
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 17);      // 耗材供应商 

            mACHINEINFOBindingSource.DataSource = mach;

            ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            ruleNoEmpty.ErrorText = "该项不能为空。";
            dxValidationProvider1.SetValidationRule(MACHINETYPETextEdit, ruleNoEmpty);                  // 设备类型
            dxValidationProvider1.SetValidationRule(MODELTextEdit, ruleNoEmpty);                        // 设备型号
            dxValidationProvider1.SetValidationRule(SNTextEdit, ruleNoEmpty);                           // 设备序列号
            dxValidationProvider1.SetValidationRule(BED_NOTextEdit, ruleNoEmpty);                       // 所在床位号

            _baseID = base_id;
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

        private void SaveData()
        {
            mACHINEINFOBindingSource.EndEdit();
            mACHINEINFOBindingSource.CurrencyManager.EndCurrentEdit();
            var layout = db.Single<MACHINE_LAYOUT>(_baseID);

            var info = db.Fetch<MACHINE_INFO>("where FLOOR_ID = @0 and AREA_ID = @1 and BED_NO = @2", new object[] { layout .FLOORID, layout.AREAID, mach.BED_NO});
            if (info != null && info.Count > 0)
                throw new Exception("该床位号已存在于该区域, 请确认.");

            mach.LAYOUT_ID = _baseID;
            mach.LOG_TIME = DateTime.Now;
            mach.OPERATOR = ClsFrmMng.WorkerID;
            mach.FLOOR_ID = layout.FLOORID;
            mach.AREA_ID = layout.AREAID;           

            db.Insert(mach);
            if (NewRegistEvt != null)
                NewRegistEvt();

            mach = new MACHINE_INFO();
            mACHINEINFOBindingSource.DataSource = mach;
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