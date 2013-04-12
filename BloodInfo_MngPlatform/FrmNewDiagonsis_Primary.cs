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
    public partial class FrmNewDiagonsis_Primary : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _baseID;

        DIAGNOSIS_PRIMARY_DISEASE diagPrimary = new DIAGNOSIS_PRIMARY_DISEASE();

        public FrmNewDiagonsis_Primary(Int64 base_id)
        {
            InitializeComponent();
            db = new Database("XE");

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where GroupName = @0", 122);
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where GroupName = @0", 123);
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where GroupName = @0", 124);
            vALUECODEBindingSource3.DataSource = db.Fetch<VALUE_CODE>("where GroupName = @0", 125);
            vALUECODEBindingSource4.DataSource = db.Fetch<VALUE_CODE>("where GroupName = @0", 126);
            vALUECODEBindingSource5.DataSource = db.Fetch<VALUE_CODE>("where GroupName = @0", 127);
            vALUECODEBindingSource6.DataSource = db.Fetch<VALUE_CODE>("where GroupName = @0", 128);            

            dIAGNOSISPRIMARYDISEASEBindingSource.DataSource = diagPrimary;

            ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            ruleNoEmpty.ErrorText = "该项不能为空。";
            dxValidationProvider1.SetValidationRule(PRIMARY_DISEAE_TYPETextEdit, ruleNoEmpty);

            _baseID = base_id;


        }

        private void HideItem()
        {
            for (int i = 0; i < dataLayoutControl1.Items.Count; i++)
            {

                if (dataLayoutControl1.Items[i].GetType().FullName == "DevExpress.XtraLayout.LayoutControlItem")
                {
                    if (dataLayoutControl1.Items[i].Text != "原发病诊断分类:")
                    {
                        dataLayoutControl1.Items[i].Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    }

                }

            }
        }

        private void SaveData()
        {
            dIAGNOSISPRIMARYDISEASEBindingSource.EndEdit();
            dIAGNOSISPRIMARYDISEASEBindingSource.CurrencyManager.EndCurrentEdit();

            diagPrimary.PT_ID = _baseID;
            diagPrimary.LOG_TIME = DateTime.Now;
            diagPrimary.OPERATOR = ClsFrmMng.WorkerID;

            db.Insert(diagPrimary);
            if (NewRegistEvt != null)
            NewRegistEvt();

            diagPrimary = new DIAGNOSIS_PRIMARY_DISEASE();
            dIAGNOSISPRIMARYDISEASEBindingSource.DataSource = diagPrimary;
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

        private void PRIMARY_DISEAE_TYPETextEdit_EditValueChanged(object sender, EventArgs e)
        {
            HideItem();

            if(PRIMARY_DISEAE_TYPETextEdit.EditValue != null &&　!string.IsNullOrEmpty( PRIMARY_DISEAE_TYPETextEdit.EditValue.ToString())  )
            {
                switch (Convert.ToInt64(PRIMARY_DISEAE_TYPETextEdit.EditValue))
                {
                    case 261:
                        ItemForPRIMARY_GLOMERULAR_DISEASE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        ItemForPRIMARY_GLOMERULAR_DISEASE_OTH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        break;
                    case 262:
                        ItemForSECONDARY_GLOMERULAR_DISEASES.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        ItemForSECONDARY_GLOMERULAR_OTH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        break;
                    case 263:
                        ItemForHEREDITARY_CONGENITAL_NEPH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        ItemForHEREDITARY_CONGENITAL_NEPH_OTH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        break;
                    case 264:
                        ItemForTUBULOINTERSTITIAL_DISEASE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        ItemForTUBULOINTERSTITIAL_DISEASE_OTH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        break;
                    case 265:
                        //ItemForURINARY_TRACT_INFECTIONS_STON.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        //ItemForINFECTIONS_STON_OTH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        break;
                    case 266:
                        ItemForURINARY_TRACT_INFECTIONS_STON.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        ItemForINFECTIONS_STON_OTH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        break;
                    case 267:
                        ItemForNEPHRECTOMY_REASON.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        ItemForNEPHRECTOMY_REASON_OTH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        break;
                }
            }
        }
    }
}