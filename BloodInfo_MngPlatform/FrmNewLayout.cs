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
    public partial class FrmNewLayout : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;

        MACHINE_LAYOUT mach = new MACHINE_LAYOUT();

        public FrmNewLayout()
        {
            InitializeComponent();
            db = new Database("XE");

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 21);
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 22);      // 区域

            mACHINELAYOUTBindingSource.DataSource = mach;

            ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            ruleNoEmpty.ErrorText = "该项不能为空。";
            dxValidationProvider1.SetValidationRule(FLOORIDTextEdit, ruleNoEmpty);                  // 楼层
            dxValidationProvider1.SetValidationRule(AREAIDTextEdit, ruleNoEmpty);                   // 区域
        }

        private void SaveData()
        {
            mACHINELAYOUTBindingSource.EndEdit();
            mACHINELAYOUTBindingSource.CurrencyManager.EndCurrentEdit();

            List<MACHINE_LAYOUT> lst = db.Fetch<MACHINE_LAYOUT>("where FLOORID = @0 and AREAID = @1", new object[] { FLOORIDTextEdit.EditValue, AREAIDTextEdit.EditValue });
            if (lst != null && lst.Count > 0)
                throw new Exception("该区域已存在, 请确认.");

            db.Insert(mach);
            if (NewRegistEvt != null)
                NewRegistEvt();

            mach = new MACHINE_LAYOUT();
            mACHINELAYOUTBindingSource.DataSource = mach;
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