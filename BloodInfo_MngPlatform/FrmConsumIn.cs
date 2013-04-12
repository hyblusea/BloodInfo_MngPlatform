using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Configuration;
using PetaPoco;
using BloodInfo_MngPlatform.Models;

namespace BloodInfo_MngPlatform
{
    public partial class FrmConsumIn : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Int64 id;
        Database db;
        //CONSUMABLES_WAREHOUSE consumWh = new CONSUMABLES_WAREHOUSE();
        List<CONSUMABLES_LOG> lstConsumIn = new List<CONSUMABLES_LOG>();

        public FrmConsumIn(Int64 consumLstID)
        {
            InitializeComponent();
            gridView2.InvalidValueException += gridView2_InvalidValueException;

            id = consumLstID;
            ////string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            cONSUMABLESWAREHOUSEBindingSource.DataSource = db.Fetch<CONSUMABLES_WAREHOUSE>("where ID = @0", new object[]{consumLstID});
            cONSUMABLESLOGBindingSource.DataSource = lstConsumIn;

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_GROUP>("where FATHERID = @0", new object[] { 16 });      // 耗材分类
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", new object[] { 17 });     // 耗材供应商
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("");

            vALUECODEBindingSource3.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", new object[] { 20 });     // 操作类型
        }

        void gridView2_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            gridView2.CloseEditor();
            cONSUMABLESLOGBindingSource.EndEdit();
            cONSUMABLESLOGBindingSource.CurrencyManager.EndCurrentEdit();
            cONSUMABLESLOGBindingSource.AddNew();

            ((CONSUMABLES_LOG)cONSUMABLESLOGBindingSource.Current).OPERATOR = ClsFrmMng.WorkerID;
            ((CONSUMABLES_LOG)cONSUMABLESLOGBindingSource.Current).LOG_TIME = DateTime.Now;
            ((CONSUMABLES_LOG)cONSUMABLESLOGBindingSource.Current).CONSUMABLES_ID = id;
            ((CONSUMABLES_LOG)cONSUMABLESLOGBindingSource.Current).OPERATOR_TYPE = 56;
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            gridView2.CloseEditor();
            cONSUMABLESLOGBindingSource.EndEdit();
            cONSUMABLESLOGBindingSource.CurrencyManager.EndCurrentEdit();

            if (XtraMessageBox.Show("确认保存入库记录? 库存记录不允许删除及修改.", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    for (int i = 0; i < lstConsumIn.Count; i++)
                    {
                        if (lstConsumIn[i].OPERATOR_NUM == null || lstConsumIn[i].OPERATOR_NUM < 1)
                        {
                            //gridView2.rows
                            XtraMessageBox.Show("输入的入库存存在非法数值, 请更正后重试", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    using (var scope = db.GetTransaction())
                    {
                        for (int i = 0; i < lstConsumIn.Count; i++)
                        {
                            lstConsumIn[i].SURPLUS = lstConsumIn[i].OPERATOR_NUM;

                            // 写入入库记录表
                            lstConsumIn[i].Insert();

                            // 写入入库日志表
                            db.Insert("CONSUMABLES_LOG1", "ID", lstConsumIn[i]);
                        }
                        scope.Complete();
                    }
                    XtraMessageBox.Show("保存成功.", "操作提示", MessageBoxButtons.OK);

                    if (NewRegistEvt != null)
                        NewRegistEvt();
                    this.Close();
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show("提交过程中出现异常, 原因: " + err.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                
            }
        }

        private void gridView2_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
        }

        private void gridView2_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
        }
    }
}