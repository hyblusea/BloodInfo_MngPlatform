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
    public partial class FrmEdtDiagonsis_Tumor : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        Database db;

        public DIAGNOSIS_TUMOR diag = new DIAGNOSIS_TUMOR();
        Int64 _id;

        public FrmEdtDiagonsis_Tumor(Int64 id)
        {
            InitializeComponent();
            db = new Database("XE");
            _id = id;

            //ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            //ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            //ruleNoEmpty.ErrorText = "该项不能为空。";
            //dxValidationProvider1.SetValidationRule(INFECTIOUS_DISEASETextEdit, ruleNoEmpty);

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 139);

            diag = db.Single<DIAGNOSIS_TUMOR>("where ID = @0", _id);
            dIAGNOSISTUMORBindingSource.DataSource = diag;
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (dxValidationProvider1.Validate())
                {
                    dIAGNOSISTUMORBindingSource.EndEdit();
                    dIAGNOSISTUMORBindingSource.CurrencyManager.EndCurrentEdit();

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

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void TUMOR_POSTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            HideItem();

            if (TUMOR_POSTextEdit.EditValue != null && !string.IsNullOrEmpty(TUMOR_POSTextEdit.EditValue.ToString()))
            {
                string[] sValue = TUMOR_POSTextEdit.EditValue.ToString().Split(',');
                for (int i = 0; i < sValue.Length; i++)
                {
                    switch (Convert.ToInt64(sValue[i].Trim()))
                    {
                        case 339:
                            layoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 340:
                            layoutControlGroup4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 341:
                            layoutControlGroup5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 342:
                            layoutControlGroup6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 343:
                            layoutControlGroup7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 344:
                            layoutControlGroup8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
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
            layoutControlGroup7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

    }
}