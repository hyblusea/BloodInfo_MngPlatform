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
    public partial class FrmEdtDiagosis_Primary : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        Database db;

        public DIAGNOSIS_PRIMARY_DISEASE diag = new  DIAGNOSIS_PRIMARY_DISEASE();
        Int64 _id;

        public FrmEdtDiagosis_Primary(Int64 id)
        {
            InitializeComponent();

            db = new Database("XE");
            _id = id;

            ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            ruleNoEmpty.ErrorText = "该项不能为空。";
            dxValidationProvider1.SetValidationRule(PRIMARY_DISEAE_TYPETextEdit, ruleNoEmpty);

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where GroupName = @0", 122);
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where GroupName = @0", 123);
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where GroupName = @0", 124);
            vALUECODEBindingSource3.DataSource = db.Fetch<VALUE_CODE>("where GroupName = @0", 125);
            vALUECODEBindingSource4.DataSource = db.Fetch<VALUE_CODE>("where GroupName = @0", 126);
            vALUECODEBindingSource5.DataSource = db.Fetch<VALUE_CODE>("where GroupName = @0", 127);
            vALUECODEBindingSource6.DataSource = db.Fetch<VALUE_CODE>("where GroupName = @0", 128);  

            diag = db.Single<DIAGNOSIS_PRIMARY_DISEASE>("where ID = @0", _id);
            dIAGNOSISPRIMARYDISEASEBindingSource.DataSource = diag;
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (dxValidationProvider1.Validate())
                {
                    dIAGNOSISPRIMARYDISEASEBindingSource.EndEdit();
                    dIAGNOSISPRIMARYDISEASEBindingSource.CurrencyManager.EndCurrentEdit();

                    try
                    {
                        diag.Update();

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

        private void PRIMARY_DISEAE_TYPETextEdit_EditValueChanged(object sender, EventArgs e)
        {
            HideItem();

            if (PRIMARY_DISEAE_TYPETextEdit.EditValue != null && !string.IsNullOrEmpty(PRIMARY_DISEAE_TYPETextEdit.EditValue.ToString()))
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
    }
}