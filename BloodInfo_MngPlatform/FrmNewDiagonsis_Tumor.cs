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
    public partial class FrmNewDiagonsis_Tumor : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _baseID;

        DIAGNOSIS_TUMOR diag = new DIAGNOSIS_TUMOR();

        public FrmNewDiagonsis_Tumor(Int64 base_id)
        {
            InitializeComponent();
            db = new Database("XE");

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 139);

            dIAGNOSISTUMORBindingSource.DataSource = diag;

            //ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            //ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            //ruleNoEmpty.ErrorText = "该项不能为空。";
            //dxValidationProvider1.SetValidationRule(INFECTIOUS_DISEASETextEdit, ruleNoEmpty);

            _baseID = base_id;
        }

        private void SaveData()
        {
            dIAGNOSISTUMORBindingSource.EndEdit();
            dIAGNOSISTUMORBindingSource.CurrencyManager.EndCurrentEdit();

            diag.PT_ID = _baseID;
            diag.LOG_TIME = DateTime.Now;
            diag.OPERATOR = ClsFrmMng.WorkerID;

            db.Insert(diag);
            if (NewRegistEvt != null)
                NewRegistEvt();

            diag = new DIAGNOSIS_TUMOR();
            dIAGNOSISTUMORBindingSource.DataSource = diag;
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

        private void OTHERTextEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void DIGESTIVE_SYSTEMTextEdit_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}