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
    public partial class FrmNewBC_Consumable : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _baseID;
        Int64 _regID;

        List<CONSUMABLES_REQUEST_LST> lst;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="base_id"></param>
        /// <param name="reg_id"></param>
        public FrmNewBC_Consumable(Int64 base_id, Int64 reg_id)
        {
            InitializeComponent();

            db = new Database("XE");

            _regID = reg_id;
            _baseID = base_id;

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("");
            vALUEGROUPBindingSource.DataSource = db.Fetch<VALUE_GROUP>("");
            LoadData();
        }

        void LoadData()
        {
            lst = db.Fetch<CONSUMABLES_REQUEST_LST>(
                //"select t.* from CONSUMABLES_REQUEST_LST t join  CONSUMABLES_REQUEST a on  T.REQUEST_ID = A.REQUEST_ID where a.operator = @0 and a.status = '完成' and SURPLUS > 0",
                //ClsFrmMng.WorkerID);
                "select t.* from CONSUMABLES_REQUEST_LST t join  CONSUMABLES_REQUEST a on  T.REQUEST_ID = A.REQUEST_ID where a.status = '完成' and SURPLUS > 0");
            cONSUMABLESREQUESTLSTBindingSource.DataSource = lst;
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.CloseEditor();
            cONSUMABLESREQUESTLSTBindingSource.EndEdit();
            cONSUMABLESREQUESTLSTBindingSource.CurrencyManager.EndCurrentEdit();

            if (lst == null)
            {
                XtraMessageBox.Show("没有耗材记录,请确认.");
                return;
            }

            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                // 使用量校验
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i].USECOUNT < 0 || lst[i].USECOUNT > db.ExecuteScalar<decimal>("select SURPLUS from CONSUMABLES_REQUEST_LST where ID = @0", lst[i].ID))
                    {
                        XtraMessageBox.Show("使用量存在负数或大于剩余量, 请核对.", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                List<CONSUMABLES_REQUEST_LST_USELOG> lstUseLog = new List<CONSUMABLES_REQUEST_LST_USELOG>();
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i].USECOUNT > 0)
                    {
                        CONSUMABLES_REQUEST_LST_USELOG v = new CONSUMABLES_REQUEST_LST_USELOG();
                        v.CONSUMABLE_ID = lst[i].CONSUMABLE_ID;
                        v.CONSUMABLE_TYPE_CODE = lst[i].CONSUMABLE_TYPE_CODE;
                        v.CONSUMABLES_IN_LOG_ID = lst[i].CONSUMABLES_IN_LOG_ID;
                        v.FIRM = lst[i].FIRM;
                        v.MODEL = lst[i].MODEL;
                        v.NAME = lst[i].NAME;
                        v.OPERATOR = ClsFrmMng.WorkerID;
                        v.PRODUCTION_DATE = v.PRODUCTION_DATE;
                        v.PT_ID = _baseID;
                        v.REG_ID = _regID;
                        v.REQUEST_ID = lst[i].REQUEST_ID;
                        v.REQUEST_NUM = lst[i].REQUEST_NUM;
                        v.SIZE1 = lst[i].SIZE1;
                        v.SN = lst[i].SN;
                        v.USECOUNT = lst[i].USECOUNT;
                        v.USETIME = DateTime.Now;
                        v.VALID = lst[i].VALID;

                        v.REQUEST_LIST_ID = lst[i].ID;                                              // 保存请求单的记录ID, 请修改使用剩余量
                        v.SURPLUS = lst[i].SURPLUS - v.USECOUNT;                                    // 为修改剩余量
                        lstUseLog.Add(v);
                    }
                }

                // 生成使用日志
                try
                {
                    using (var scop = db.GetTransaction())
                    {
                        for (int i = 0; i < lstUseLog.Count; i++)
                        {
                            // 查询申请单中的耗材记录, 并为更新剩余量加锁
                            var v = db.Single<CONSUMABLES_REQUEST_LST>("select * from CONSUMABLES_REQUEST_LST where ID = @0 FOR UPDATE", lstUseLog[i].REQUEST_LIST_ID);
                            if (v.SURPLUS < lstUseLog[i].USECOUNT)
                                throw new Exception(string.Format("剩余量不足. 申请单号: {0}, 耗材ID: {1}, 记录ID: {2}.", v.REQUEST_ID, v.NAME, v.ID ));

                            db.Insert(lstUseLog[i]);
                            db.Execute("update CONSUMABLES_REQUEST_LST set SURPLUS = @0 where ID = @1", new object[] { lstUseLog[i].SURPLUS, lstUseLog[i].REQUEST_LIST_ID });
                        }
                        scop.Complete();
                    }
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show("生成耗材使用日志出现异常, 原因: " + err.Message);
                    return;
                }
                if (NewRegistEvt != null)
                    NewRegistEvt();

                this.Close();
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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}