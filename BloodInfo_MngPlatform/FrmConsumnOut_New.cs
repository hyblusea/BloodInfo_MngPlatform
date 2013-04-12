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
    public partial class FrmConsumnOut_New : DevExpress.XtraEditors.XtraForm
    {
        Database db;

        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        
        List<CONSUMABLES_LOG> lstConsumLog = new List<CONSUMABLES_LOG>();
        List<CONSUMABLES_LOG1> lstConsumOut = new List<CONSUMABLES_LOG1>();

        public FrmConsumnOut_New()
        {
            InitializeComponent();

            ////string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
        }

        private void FrmConsumnOut_New_Load(object sender, EventArgs e)
        {
            ucPaing3.dspLenght = 15;

            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("");                                              // 耗材名称
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", new object[] { 17 });     // 耗材供应商

            cONSUMABLESREQUESTBindingSource.CurrentItemChanged += cONSUMABLESREQUESTBindingSource_CurrentItemChanged;
            ucPaing3_PageChanged(1, ucPaing3.dspLenght);
        }

        void cONSUMABLESREQUESTBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (cONSUMABLESREQUESTBindingSource.Current != null)
            {
                cONSUMABLESREQUESTLSTBindingSource1.DataSource = db.Fetch<CONSUMABLES_REQUEST_LST>("where REQUEST_ID = @0", ((CONSUMABLES_REQUEST)cONSUMABLESREQUESTBindingSource.Current).REQUEST_ID);
            }
            else
                cONSUMABLESREQUESTLSTBindingSource1.DataSource = null;
        }

        private void ucPaing3_PageChanged(long curPage, int dspLen)
        {
            var page = new Page<CONSUMABLES_REQUEST>();
            try
            {
                page = db.Page<CONSUMABLES_REQUEST>(curPage, dspLen, "WHERE STATUS = '申请中' ORDER BY LOG_TIME DESC");
            }
            catch (Exception err)
            { XtraMessageBox.Show(err.Message); }

            cONSUMABLESREQUESTBindingSource.DataSource = page.Items;
            ucPaing3.totalPage = page.TotalPages;
            ucPaing3.recordCnt = page.TotalItems;
            ucPaing3.curPage = curPage;
        }

        private void btnOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cONSUMABLESREQUESTBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要出库的申请单.", "错误提示");
                return;
            }

            try
            {
                 List<CONSUMABLES_REQUEST_LST> lst = new List<CONSUMABLES_REQUEST_LST>();


                if (XtraMessageBox.Show("该申请单确定出库? ", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    lst = ((List<CONSUMABLES_REQUEST_LST>)cONSUMABLESREQUESTLSTBindingSource1.DataSource);
                    if (cONSUMABLESREQUESTLSTBindingSource1.DataSource == null || lst.Count() == 0)
                    {
                        XtraMessageBox.Show("该申请单中无材料明细信息. 请 '撤回' 到原申请人.");
                    }
                    else
                    {
                        // 数据效验
                        for (int i = 0; i < lst.Count(); i++)
                        {
                            string errtxt = "";

                            CONSUMABLES_LOG consumablesInLog = db.FirstOrDefault<CONSUMABLES_LOG>("where ID = @0", lst[i].CONSUMABLES_IN_LOG_ID);
                            if (consumablesInLog == null)
                            {
                                XtraMessageBox.Show("未找到ID为 [" + lst[i].CONSUMABLES_IN_LOG_ID + "] 的入库记录, 请联系维护人员.", "致命错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            if (lst[i].REQUEST_NUM < 1)
                            {
                                errtxt = "输入的出库量非法,请核对.";
                                Clipboard.SetDataObject(errtxt);
                                XtraMessageBox.Show(errtxt, "错误提示", MessageBoxButtons.OK);
                                return;
                            }

                            if (consumablesInLog.SURPLUS < lst[i].REQUEST_NUM)
                            {
                                errtxt = "申请数大于库存量, 请 '撤回' 到原申请人.";
                                Clipboard.SetDataObject(errtxt);
                                XtraMessageBox.Show(errtxt, "错误提示", MessageBoxButtons.OK);
                                return;
                            }
                        }

                        // 出库操作
                        using (var scope = db.GetTransaction())
                        {
                            for (int i = 0; i < lst.Count(); i++)
                            {
                                CONSUMABLES_LOG consumablesInLog = db.FirstOrDefault<CONSUMABLES_LOG>("where ID = @0", lst[i].CONSUMABLES_IN_LOG_ID);

                                // 修改入库记录中的库存量
                                consumablesInLog.SURPLUS = consumablesInLog.SURPLUS - lst[i].REQUEST_NUM;

                                // 新增出库日志
                                CONSUMABLES_LOG1 OutLog = new CONSUMABLES_LOG1();
                                OutLog.OPERATOR = ClsFrmMng.WorkerID;
                                OutLog.LOG_TIME = DateTime.Now;
                                OutLog.CONSUMABLES_ID = lst[i].CONSUMABLE_ID;
                                OutLog.OPERATOR_TYPE = 57;
                                OutLog.CONSUMABLES_IN_LOG_ID = lst[i].CONSUMABLES_IN_LOG_ID;
                                OutLog.PRODUCTION_DATE = lst[i].PRODUCTION_DATE;
                                OutLog.VALID = lst[i].VALID;
                                OutLog.SN = lst[i].SN;
                                OutLog.OPERATOR_NUM = consumablesInLog.OPERATOR_NUM;            // 入库量
                                OutLog.SURPLUS = consumablesInLog.SURPLUS;
                                OutLog.OUT_NUM = lst[i].REQUEST_NUM;

                                consumablesInLog.Update();
                                OutLog.Insert();
                            }

                            db.Execute("update CONSUMABLES_REQUEST set status = '出库' where REQUEST_ID	 = @0", ((CONSUMABLES_REQUEST)cONSUMABLESREQUESTBindingSource.Current).REQUEST_ID);
                            scope.Complete();

                            XtraMessageBox.Show("申请单[ " + ((CONSUMABLES_REQUEST)cONSUMABLESREQUESTBindingSource.Current).REQUEST_ID + " ],出库操作成功.");

                            ucPaing3_PageChanged(1, ucPaing3.dspLenght);
                            if (NewRegistEvt != null)
                            {
                                NewRegistEvt();
                            }
                        }
                    }
                }
            }
            catch (Exception err) { XtraMessageBox.Show(err.Message); }
        }

        private void btnBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cONSUMABLESREQUESTBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要出库的申请单.", "错误提示");
                return;
            }

            FrmBackReason frmReason = new FrmBackReason();
            if (frmReason.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                var v = (CONSUMABLES_REQUEST)cONSUMABLESREQUESTBindingSource.Current;
                v.STATUS = ClsFrmMng.StatusComBack;

                if (!string.IsNullOrEmpty( frmReason.Reason) )
                    v.MEMO = v.MEMO + "|" + frmReason.Reason + "|";

                v.Update();

                XtraMessageBox.Show("撤回操作成功.");
                ucPaing3_PageChanged(1, ucPaing3.dspLenght);
            }
        }
    }
}

