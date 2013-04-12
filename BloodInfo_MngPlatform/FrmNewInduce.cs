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
    public partial class FrmNewInduce : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        Database db;

        public INDUCEMENT_HISTORY induceMent = new INDUCEMENT_HISTORY();

        Int64 _reg_id = 0;
        Int64 _baseID;

        public FrmNewInduce(Int64  base_id, Int64 reg_id)
        {
            InitializeComponent();

            
            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            _reg_id = reg_id;
            _baseID = base_id;

            //ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            //ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            //ruleNoEmpty.ErrorText = "该项不能为空。";
            //dxValidationProvider1.SetValidationRule(MEMOMemoEdit, ruleNoEmpty);
            //dxValidationProvider1.SetValidationRule(DOSE_MEMOMemoEdit, ruleNoEmpty);

            induceMent.REG_ID = _reg_id;
            induceMent.BASE_INFO_ID = _baseID;
            induceMent.OPERATOR = ClsFrmMng.WorkerID;
            iNDUCEMENTHISTORYBindingSource.DataSource = induceMent;

            //INDUCEMENTTextEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            INDUCEMENTTextEdit.Properties.DataSource = ClsFrmMng.lstIndecumentCode;
            INDUCEMENTTextEdit.Properties.DisplayMember = "INDUCEMENT_MEMO";
            INDUCEMENTTextEdit.Properties.ValueMember = "INDUCEMENT_MEMO";
        }


        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该患者诱因信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                iNDUCEMENTHISTORYBindingSource.EndEdit();
                iNDUCEMENTHISTORYBindingSource.CurrencyManager.EndCurrentEdit();
                try
                {
                    //db.OpenSharedConnection();
                    induceMent.LOG_TIME = DateTime.Now;
                    db.Insert(induceMent);
                    //db.CloseSharedConnection();

                    induceMent = new INDUCEMENT_HISTORY();
                    induceMent.REG_ID = _reg_id;
                    induceMent.BASE_INFO_ID = _baseID;
                    induceMent.OPERATOR = ClsFrmMng.WorkerID;
                    iNDUCEMENTHISTORYBindingSource.DataSource = induceMent;

                    if (NewRegistEvt != null)
                        NewRegistEvt();
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
                }
            }
        }

        private void btnSaveAndExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnSave_ItemClick(null, null);
            this.Close();
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

    }

}