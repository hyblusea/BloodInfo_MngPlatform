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
    public partial class FrmNewDiagonsis_Outcome : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _baseID;

        DIAGNOSIS_OUTCOME diag = new DIAGNOSIS_OUTCOME();

        public FrmNewDiagonsis_Outcome(Int64 base_id)
        {
            InitializeComponent();
            db = new Database("XE");

            // 死亡原因
            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 242);
            // 心血管事件
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 243);
            // 脑管理事件
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 244);
            // 感染
            vALUECODEBindingSource3.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 245);

            dIAGNOSISOUTCOMEBindingSource.DataSource = diag;

            //ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            //ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            //ruleNoEmpty.ErrorText = "该项不能为空。";
            //dxValidationProvider1.SetValidationRule(INFECTIOUS_DISEASETextEdit, ruleNoEmpty);

            _baseID = base_id;
            HideItem();
        }

        private void SaveData()
        {
            dIAGNOSISOUTCOMEBindingSource.EndEdit();
            dIAGNOSISOUTCOMEBindingSource.CurrencyManager.EndCurrentEdit();

            diag.PT_ID = _baseID;
            diag.LOG_TIME = DateTime.Now;
            diag.OPERATOR = ClsFrmMng.WorkerID;

            db.Insert(diag);
            if (NewRegistEvt != null)
                NewRegistEvt();

            diag = new DIAGNOSIS_OUTCOME();
            dIAGNOSISOUTCOMEBindingSource.DataSource = diag;
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

        private void OUTCOM_TYPETextEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            HideItem();

            if (OUTCOM_TYPETextEdit.EditValue != null && !string.IsNullOrEmpty(OUTCOM_TYPETextEdit.EditValue.ToString()))
            {
                string[] sValue = OUTCOM_TYPETextEdit.EditValue.ToString().Split(',');
                for (int i = 0; i < sValue.Length; i++)
                {
                    switch (sValue[i].Trim())
                    {
                        case "退出":
                            layoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case "转出":
                            layoutControlGroup4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case "死亡":
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
            //layoutControlGroup6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void CAUSE_OF_DEATHTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            HideItem2();

            if (CAUSE_OF_DEATHTextEdit.EditValue != null && !string.IsNullOrEmpty(CAUSE_OF_DEATHTextEdit.EditValue.ToString()))
            {
                string[] sValue = CAUSE_OF_DEATHTextEdit.EditValue.ToString().Split(',');
                for (int i = 0; i < sValue.Length; i++)
                {
                    switch (Convert.ToInt64(sValue[i].Trim()))
                    {
                        case 688:
                            layoutControlGroup7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 689:
                            layoutControlGroup8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 690:
                            layoutControlGroup9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                    }
                }
            }
        }
        private void HideItem2()
        {
            layoutControlGroup7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
    }
}