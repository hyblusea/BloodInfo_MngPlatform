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
    public partial class FrmNewConsum : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        CONSUMABLES_WAREHOUSE consumWh = new  CONSUMABLES_WAREHOUSE();

        public FrmNewConsum()
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            cONSUMABLESWAREHOUSEBindingSource.DataSource = consumWh;

            vALUEGROUPBindingSource.DataSource = db.Fetch<VALUE_GROUP>("where FATHERID = @0", new object[] { 16 });     // 耗材分类
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", new object[] { 17 });     // 耗材供应商

            ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            ruleNoEmpty.ErrorText = "该项不能为空。";
            dxValidationProvider1.SetValidationRule(CONSUMABLES_CODETextEdit, ruleNoEmpty);
            dxValidationProvider1.SetValidationRule(NAMETextEdit, ruleNoEmpty);
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!dxValidationProvider1.Validate())
                return;

            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                cONSUMABLESWAREHOUSEBindingSource.EndEdit();
                cONSUMABLESWAREHOUSEBindingSource.CurrencyManager.EndCurrentEdit();
                try
                {
                    var o = db.Fetch<CONSUMABLES_WAREHOUSE>("where CONSUMABLES_CODE = @0 and NAME = @1 and SIZE1 = @2 and FIRM = @3 and MODEL = @4",
                        new object[] { consumWh.CONSUMABLES_CODE, consumWh.NAME, consumWh.SIZE1, consumWh.FIRM, consumWh.MODEL });
                    if (o.Count > 0)
                    {
                        XtraMessageBox.Show("已存在相同规格该耗材, 新增失败.", "错误提示", MessageBoxButtons.OK);
                        return;
                    }

                    consumWh.COUNT = 0;
                    consumWh.LOG_TIME = DateTime.Now;
                    consumWh.OPERATOR = ClsFrmMng.WorkerID;
                    db.Insert(consumWh);

                    consumWh = new  CONSUMABLES_WAREHOUSE();
                    cONSUMABLESWAREHOUSEBindingSource.DataSource = consumWh;

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
            if (!dxValidationProvider1.Validate())
                return;

            btnSave_ItemClick(null, null);
            this.Close();
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void CONSUMABLES_CODETextEdit_EditValueChanged(object sender, EventArgs e)
        {
            NAMETextEdit.EditValue = null;

            if (CONSUMABLES_CODETextEdit.EditValue == null || CONSUMABLES_CODETextEdit.EditValue.ToString() == "")
            {
                NAMETextEdit.EditValue = null;
                vALUECODEBindingSource1.DataSource = null;
            }
            else
                vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", Convert.ToInt64(CONSUMABLES_CODETextEdit.EditValue));     // 耗材名称
        }


    }
}