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
    public partial class FrmNewPatient_Course : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _baseID;

        DIAGNOSIS_ALLERGY diag = new DIAGNOSIS_ALLERGY();

        public FrmNewPatient_Course(Int64 base_id)
        {
            InitializeComponent();
            db = new Database("XE");

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 147);
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 148);
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 44);
            vALUECODEBindingSource3.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 149);
            vALUECODEBindingSource4.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 150);
            vALUEGROUPBindingSource.DataSource = db.Fetch<VALUE_GROUP>("where FATHERID = @0", 18);
            vALUECODEBindingSource6.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 152);
            vALUECODEBindingSource7.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 153);

            dIAGNOSISALLERGYBindingSource.DataSource = diag;

            //ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            //ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            //ruleNoEmpty.ErrorText = "该项不能为空。";
            //dxValidationProvider1.SetValidationRule(INFECTIOUS_DISEASETextEdit, ruleNoEmpty);

            _baseID = base_id;
        }

        private void SaveData()
        {
            dIAGNOSISALLERGYBindingSource.EndEdit();
            dIAGNOSISALLERGYBindingSource.CurrencyManager.EndCurrentEdit();

            diag.PT_ID = _baseID;
            diag.LOG_TIME = DateTime.Now;
            diag.OPERATOR = ClsFrmMng.WorkerID;

            db.Insert(diag);
            if (NewRegistEvt != null)
                NewRegistEvt();

            diag = new DIAGNOSIS_ALLERGY();
            dIAGNOSISALLERGYBindingSource.DataSource = diag;
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

        private void FrmNewDiagonsis_Infe_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 器材过敏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ALLERGIC_REACTIONSTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            hideItem1();

            if(ALLERGIC_REACTIONSTextEdit.EditValue !=null && !string.IsNullOrEmpty(ALLERGIC_REACTIONSTextEdit.EditValue.ToString()))
            {
                string[] sValue = ALLERGIC_REACTIONSTextEdit.EditValue.ToString().Split(',');
                for (int i = 0; i < sValue.Length; i++)
                {
                    switch (Convert.ToInt64(sValue[i].Trim()))
                    {
                        case 349:
                            layoutControlGroup4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 350:
                            layoutControlGroup7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                    }
                }
            }
        }

        private void hideItem1()
        {
            // 透析器
            layoutControlGroup4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 透析器/ 透析膜,消毒剂
            layoutControlGroup5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never; 

            // 药物过敏
            layoutControlGroup7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never; 
        }

        /// <summary>
        /// 过敏器材
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DIALYSIS_EQUIPMENT_ALLERGIESTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            hideItem2();

            if (DIALYSIS_EQUIPMENT_ALLERGIESTextEdit.EditValue != null && !string.IsNullOrEmpty(DIALYSIS_EQUIPMENT_ALLERGIESTextEdit.EditValue.ToString()))
            {
                string[] sValue = DIALYSIS_EQUIPMENT_ALLERGIESTextEdit.EditValue.ToString().Split(',');
                for (int i = 0; i < sValue.Length; i++)
                {
                    switch (Convert.ToInt64(sValue[i].Trim()))
                    {
                        case 352:
                            layoutControlGroup5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 353:
                            layoutControlGroup6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                    }
                }
            }
        }

        private void hideItem2()
        {
            layoutControlGroup5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never; 
        }

        // 药物过敏
        private void DRUG_ALLERGYTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            hideItem3();

            if (DRUG_ALLERGYTextEdit.EditValue != null && !string.IsNullOrEmpty(DRUG_ALLERGYTextEdit.EditValue.ToString()))
            {
                string[] sValue = DRUG_ALLERGYTextEdit.EditValue.ToString().Split(',');
                for (int i = 0; i < sValue.Length; i++)
                {
                    switch (Convert.ToInt64(sValue[i].Trim()))
                    {
                        case 143:
                            layoutControlGroup9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 144:
                            layoutControlGroup8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 145:
                            layoutControlGroup10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                    }
                }
            }
        }

        private void hideItem3()
        {
            layoutControlGroup9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        /// <summary>
        /// 静脉铁剂
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void INTRAVENOUS_IRONTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            ItemForIRON_SUCROSE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            ItemForIRON_DEXTRAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (INTRAVENOUS_IRONTextEdit.EditValue != null && !string.IsNullOrEmpty(INTRAVENOUS_IRONTextEdit.EditValue.ToString()))
            {
                string[] sValue = INTRAVENOUS_IRONTextEdit.EditValue.ToString().Split(',');
                for (int i = 0; i < sValue.Length; i++)
                {
                    switch (Convert.ToInt64(sValue[i].Trim()))
                    {
                        case 363:
                            ItemForIRON_SUCROSE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                        case 364:
                            ItemForIRON_DEXTRAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            break;
                    }
                }
            }
        }
    }
}