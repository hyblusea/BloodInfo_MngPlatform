﻿using System;
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
    public partial class FrmNewDiagonsis_Complication : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _baseID;

        DIAGNOSIS_COMPLICATION diag = new   DIAGNOSIS_COMPLICATION();

        public FrmNewDiagonsis_Complication(Int64 base_id)
        {
            InitializeComponent();
            db = new Database("XE");

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 131);
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 132);
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 133);
            vALUECODEBindingSource3.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 134);
            vALUECODEBindingSource4.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 135);
            vALUECODEBindingSource5.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 136);
            vALUECODEBindingSource6.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 221);

            dIAGNOSISCOMPLICATIONBindingSource.DataSource = diag;

            ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            ruleNoEmpty.ErrorText = "该项不能为空。";
            dxValidationProvider1.SetValidationRule(COMPLICATIONS_TYPETextEdit, ruleNoEmpty);

            _baseID = base_id;
        }

        private void SaveData()
        {
            dIAGNOSISCOMPLICATIONBindingSource.EndEdit();
            dIAGNOSISCOMPLICATIONBindingSource.CurrencyManager.EndCurrentEdit();

            diag.PT_ID = _baseID;
            diag.LOG_TIME = DateTime.Now;
            diag.OPERATOR = ClsFrmMng.WorkerID;

            db.Insert(diag);
            if (NewRegistEvt != null)
                NewRegistEvt();

            diag = new DIAGNOSIS_COMPLICATION();
            dIAGNOSISCOMPLICATIONBindingSource.DataSource = diag;
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

        private void COMPLICATIONS_TYPETextEdit_EditValueChanged(object sender, EventArgs e)
        {
            HideItem();

            if (COMPLICATIONS_TYPETextEdit.EditValue != null && !string.IsNullOrEmpty(COMPLICATIONS_TYPETextEdit.EditValue.ToString()))
            {
                string[] sValue = COMPLICATIONS_TYPETextEdit.EditValue.ToString().Split(',');
                for (int i = 0; i < sValue.Length; i++)
                {
                    switch (Convert.ToInt64(sValue[i].Trim()))
                    {
                        case 302:
                            layoutControlGroup4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 305:
                            layoutControlGroup5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 306:
                            layoutControlGroup6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 307:
                            layoutControlGroup7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 308:
                            layoutControlGroup8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 309:
                            layoutControlGroup9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                    }
                }
            }
        }

        private void HideItem()
        {
            layoutControlGroup4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
    }
}