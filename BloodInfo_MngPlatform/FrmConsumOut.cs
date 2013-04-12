using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Configuration;
using PetaPoco;
using BloodInfo_MngPlatform.Models;

namespace BloodInfo_MngPlatform
{
    public partial class FrmConsumOut : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Int64 id;
        Database db;
        //CONSUMABLES_WAREHOUSE consumWh = new CONSUMABLES_WAREHOUSE();
        List<CONSUMABLES_LOG> lstConsumLog = new List<CONSUMABLES_LOG>();
        List<CONSUMABLES_LOG1> lstConsumOut = new List<CONSUMABLES_LOG1>();

        public FrmConsumOut(Int64 consumLstID)
        {
            InitializeComponent();

            id = consumLstID;
            ////string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            cONSUMABLESLOGBindingSource.CurrentItemChanged += cONSUMABLESLOGBindingSource_CurrentItemChanged;
            cONSUMABLESWAREHOUSEBindingSource.DataSource = db.Fetch<CONSUMABLES_WAREHOUSE>("where ID = @0", new object[]{consumLstID});
            lstConsumLog = db.Fetch<CONSUMABLES_LOG>("where CONSUMABLES_ID = @0 and SURPLUS > 0 order by VALID asc, PRODUCTION_DATE asc", consumLstID);
            cONSUMABLESLOGBindingSource.DataSource = lstConsumLog;
            cONSUMABLESLOG1BindingSource.DataSource = lstConsumOut;

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", new object[] { 16 });      // 耗材分类
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", new object[] { 17 });     // 耗材供应商
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", new object[] { 19 });     // 耗材名称

            vALUECODEBindingSource3.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", new object[] { 20 });     // 操作类型
        }

        void cONSUMABLESLOGBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (cONSUMABLESLOGBindingSource.Current != null)
                lblSurplus.Caption = ((CONSUMABLES_LOG)cONSUMABLESLOGBindingSource.Current).SURPLUS.ToString();
            else
                lblSurplus.Caption = "0";
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            cONSUMABLESLOGBindingSource.EndEdit();
            cONSUMABLESLOGBindingSource.CurrencyManager.EndCurrentEdit();

            if (XtraMessageBox.Show("确认保存入库记录? 库存记录不允许删除及修改.", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                //try
                //{
                //    using (var scope = db.GetTransaction())
                //    {
                //        for (int i = 0; i < lstConsumIn.Count; i++)
                //        {
                //            lstConsumIn[i].SURPLUS = lstConsumIn[i].OPERATOR_NUM;

                //            // 写入入库记录表
                //            lstConsumIn[i].Insert();

                //            // 写入入库日志表
                //           db.Insert("CONSUMABLES_LOG1", "ID", lstConsumIn[i]);
                //        }
                //        scope.Complete();
                //    }
                //    XtraMessageBox.Show("保存成功.", "操作提示", MessageBoxButtons.OK);

                //    if (NewRegistEvt != null)
                //        NewRegistEvt();
                //    this.Close();
                //}
                //catch (Exception err)
                //{
                //    XtraMessageBox.Show("提交过程中出现异常, 原因: " + err.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}                
            }
        }

        private void btnOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cONSUMABLESLOGBindingSource.Current == null) {
                XtraMessageBox.Show("请选择需要出库的耗材.", "错误提示", MessageBoxButtons.OK);
                return;
            }
            if (barEditItem1.EditValue == null || barEditItem1.EditValue.ToString() == "" || Convert.ToInt64(barEditItem1.EditValue) < 1)
            {
                XtraMessageBox.Show("输入的出库量非法,请核对.", "错误提示", MessageBoxButtons.OK);
                return;
            }

            if (((CONSUMABLES_LOG)cONSUMABLESLOGBindingSource.Current).SURPLUS < Convert.ToInt64(barEditItem1.EditValue))
            {
                XtraMessageBox.Show("出库量大于库存量, 请核对.", "错误提示", MessageBoxButtons.OK);
                return;
            }

            cONSUMABLESLOG1BindingSource.EndEdit();
            cONSUMABLESLOG1BindingSource.CurrencyManager.EndCurrentEdit();
            cONSUMABLESLOG1BindingSource.AddNew();

            ((CONSUMABLES_LOG)cONSUMABLESLOGBindingSource.Current).SURPLUS = ((CONSUMABLES_LOG)cONSUMABLESLOGBindingSource.Current).SURPLUS - Convert.ToInt64(barEditItem1.EditValue);
            cONSUMABLESLOGBindingSource_CurrentItemChanged(null, null);
            gridControl2.RefreshDataSource();

            ((CONSUMABLES_LOG1)cONSUMABLESLOG1BindingSource.Current).OPERATOR = ClsFrmMng.WorkerID;
            ((CONSUMABLES_LOG1)cONSUMABLESLOG1BindingSource.Current).LOG_TIME = DateTime.Now;
            ((CONSUMABLES_LOG1)cONSUMABLESLOG1BindingSource.Current).CONSUMABLES_ID = id;
            ((CONSUMABLES_LOG1)cONSUMABLESLOG1BindingSource.Current).OPERATOR_TYPE = 57;
            ((CONSUMABLES_LOG1)cONSUMABLESLOG1BindingSource.Current).CONSUMABLES_IN_LOG_ID = ((CONSUMABLES_LOG)cONSUMABLESLOGBindingSource.Current).ID;
            ((CONSUMABLES_LOG1)cONSUMABLESLOG1BindingSource.Current).PRODUCTION_DATE = ((CONSUMABLES_LOG)cONSUMABLESLOGBindingSource.Current).PRODUCTION_DATE;
            ((CONSUMABLES_LOG1)cONSUMABLESLOG1BindingSource.Current).VALID = ((CONSUMABLES_LOG)cONSUMABLESLOGBindingSource.Current).VALID;
            ((CONSUMABLES_LOG1)cONSUMABLESLOG1BindingSource.Current).SN = ((CONSUMABLES_LOG)cONSUMABLESLOGBindingSource.Current).SN;
            ((CONSUMABLES_LOG1)cONSUMABLESLOG1BindingSource.Current).OPERATOR_NUM = ((CONSUMABLES_LOG)cONSUMABLESLOGBindingSource.Current).OPERATOR_NUM;
            ((CONSUMABLES_LOG1)cONSUMABLESLOG1BindingSource.Current).SURPLUS = ((CONSUMABLES_LOG)cONSUMABLESLOGBindingSource.Current).SURPLUS;
            ((CONSUMABLES_LOG1)cONSUMABLESLOG1BindingSource.Current).OUT_NUM = Convert.ToInt64(barEditItem1.EditValue);
            barEditItem1.EditValue = null;
            gridControl3.RefreshDataSource();            
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmConsumOut_Comfirm frmComfirm = new FrmConsumOut_Comfirm(lstConsumOut, lstConsumLog);
            if (frmComfirm.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                XtraMessageBox.Show("出库成功.");
                this.Close();
                if (NewRegistEvt != null)
                    NewRegistEvt();
            }
     

        }

      
    }
}