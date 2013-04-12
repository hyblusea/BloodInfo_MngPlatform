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
    public partial class FrmEdtDiagonsis_Pathological : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        Database db;

        public DIAGNOSIS_PATHOLOGICAL diag = new DIAGNOSIS_PATHOLOGICAL();
        Int64 _id;

        public FrmEdtDiagonsis_Pathological(Int64 id)
        {
            InitializeComponent();
            db = new Database("XE");
            _id = id;

            ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            ruleNoEmpty.ErrorText = "该项不能为空。";
            dxValidationProvider1.SetValidationRule(PATHOLOGICAL_DIAGNOSIS_TYPELookUpEdit, ruleNoEmpty);

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 129);
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 123);
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 124);
            vALUECODEBindingSource3.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 126);
            vALUECODEBindingSource4.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 125);

            diag = db.Single<DIAGNOSIS_PATHOLOGICAL>("where ID = @0", _id);
            dIAGNOSISPATHOLOGICALBindingSource.DataSource = diag;
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) ==

System.Windows.Forms.DialogResult.OK)
            {
                if (dxValidationProvider1.Validate())
                {
                    dIAGNOSISPATHOLOGICALBindingSource.EndEdit();
                    dIAGNOSISPATHOLOGICALBindingSource.CurrencyManager.EndCurrentEdit();

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

        private void PATHOLOGICAL_DIAGNOSIS_TYPELookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            HideItem();

            if (PATHOLOGICAL_DIAGNOSIS_TYPELookUpEdit.EditValue != null && !string.IsNullOrEmpty(PATHOLOGICAL_DIAGNOSIS_TYPELookUpEdit.EditValue.ToString()))
            {
                string[] sValue = PATHOLOGICAL_DIAGNOSIS_TYPELookUpEdit.EditValue.ToString().Split(',');
                for (int i = 0; i < sValue.Length; i++)
                {
                    switch (Convert.ToInt64(sValue[i].Trim()))
                    {
                        case 297:
                            layoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 298:
                            layoutControlGroup4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 299:
                            layoutControlGroup6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 300:
                            layoutControlGroup5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                    }
                }
            }
        }

        private void HideItem()
        {
            layoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
    }
}