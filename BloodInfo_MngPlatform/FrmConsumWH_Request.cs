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
    public partial class FrmConsumWH_Request : XtraForm
    {
        Database db;
        Int64? consumTypeID;
        string consumName;
        string consumModel;
        string consumSize;

        Int64 consumListID;

        List< CONSUMABLES_REQUEST_LST> lstConsumablesRequestLst = new List<CONSUMABLES_REQUEST_LST>();
        

        public FrmConsumWH_Request()
        {
            InitializeComponent();
            ////string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            SetBtnVisible();
        }

        private void FrmConsumWH_Load(object sender, EventArgs e)
        {
            // Form按钮使用设置
            List<ATH_CONTROL_ENABLE> lstFrmCtrl = ClsFrmMng.lstCtrlEnable.Where(c => c.FATHERITEM == this.Name).ToList<ATH_CONTROL_ENABLE>();
            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            for (int i = 0; i < lstFrmCtrl.Count(); i++)
            {
                dic.Add(lstFrmCtrl[i].CONTROL_NAME, Convert.ToBoolean(lstFrmCtrl[i].ENABLE));
            }
            AuthrzForDevDx.ScanFormControl ctrlEnable = new AuthrzForDevDx.ScanFormControl();
            ctrlEnable.SetBtn(this.components.Components, dic);

            // 绑定值集
            vALUEGROUPBindingSource.DataSource = db.Fetch<VALUE_GROUP>("where FATHERID = @0", new object[] { 16 });      // 耗材分类
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", new object[] { 17 });     // 耗材供应商
            //vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", new object[] { 19 });     // 耗材名称
            vALUECODEBindingSource3.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", new object[] { 20 });     // 库存操作类型
            bindingSource1.DataSource = db.Fetch<VALUE_CODE>("");

            ucPaing1.dspLenght = 30;
            ucPaing2.dspLenght = 10;
            ucPaing3.dspLenght = 10;
            btnSearch_ItemClick(null, null);
           
            cONSUMABLESWAREHOUSEBindingSource.CurrentItemChanged += cONSUMABLESWAREHOUSEBindingSource_CurrentItemChanged;
            cONSUMABLESREQUESTBindingSource.CurrentItemChanged += cONSUMABLESREQUESTBindingSource_CurrentItemChanged;

            cONSUMABLESREQUESTLSTBindingSource.DataSource = lstConsumablesRequestLst;
            ucPaing3_PageChanged(1, ucPaing3.dspLenght);

            txtType.EditValueChanged += txtType_EditValueChanged; 
            cONSUMABLESWAREHOUSEBindingSource.CurrentItemChanged += cONSUMABLESWAREHOUSEBindingSource_CurrentItemChanged;
        }

        void txtType_EditValueChanged(object sender, EventArgs e)
        {
            txtName.EditValue = null;

            if (txtType.EditValue == null || txtType.EditValue.ToString() == "")
            {
                vALUECODEBindingSource2.DataSource = null;
            }
            else
            {
                vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", Convert.ToInt64(txtType.EditValue));     // 耗材名称
            }
        }

        void SetBtnVisible()
        {
            btnCancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnConfirmA.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnRecv.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        void cONSUMABLESREQUESTBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            SetBtnVisible();

            if( cONSUMABLESREQUESTBindingSource.Current != null )
            {
                cONSUMABLESREQUESTLSTBindingSource1.DataSource = db.Fetch<CONSUMABLES_REQUEST_LST>("where REQUEST_ID = @0", ((CONSUMABLES_REQUEST)cONSUMABLESREQUESTBindingSource.Current).REQUEST_ID); 

                if (((CONSUMABLES_REQUEST)cONSUMABLESREQUESTBindingSource.Current).STATUS == ClsFrmMng.StatusComBack)
                {
                    btnConfirmA.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    btnCancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }                    
                else if (((CONSUMABLES_REQUEST)cONSUMABLESREQUESTBindingSource.Current).STATUS == ClsFrmMng.StatusComOut)
                    btnRecv.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else if (((CONSUMABLES_REQUEST)cONSUMABLESREQUESTBindingSource.Current).STATUS == ClsFrmMng.StatusComfirm)
                    btnCancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
        }

        void cONSUMABLESWAREHOUSEBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (cONSUMABLESWAREHOUSEBindingSource.Current != null)
            {
                consumListID = (Int64)((CONSUMABLES_WAREHOUSE)cONSUMABLESWAREHOUSEBindingSource.Current).ID;
                ucPaing2_PageChanged(1, ucPaing2.dspLenght);
            }
            else
            {
                cONSUMABLESLOGBindingSource.DataSource = null;
            }            
        }

        private void FrmConsumWH_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClsFrmMng.frmConsumWhReqst = null;
        }

        private void btnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtType.EditValue != null)
                consumTypeID = Convert.ToInt64(txtType.EditValue);
            else
                consumTypeID = null;
            if (txtName.EditValue != null)
                consumName = txtName.EditValue.ToString();
            else
                consumName = "%";

            if (txtModel.EditValue != null && txtModel.EditValue.ToString() != "")
                consumModel = txtModel.EditValue.ToString();
            else
                consumModel = "%";

            if (txtSize.EditValue != null && txtSize.EditValue.ToString() != "")
                consumSize = "%" + txtSize.EditValue.ToString();
            else
                consumSize = "%";

            ucPaing1_PageChanged(1, ucPaing1.dspLenght);
        }

        private void ucPaing1_PageChanged(long curPage, int dspLen)
        {
            var page = new Page<CONSUMABLES_WAREHOUSE>();
            try
            {
                if (consumTypeID != null)
                {
                    page = db.Page<CONSUMABLES_WAREHOUSE>(curPage, dspLen, "where CONSUMABLES_CODE = @0 and NAME like @1 and (MODEL like @2  or MODEL is null) and (SIZE1 like @3 or size1 is null) ORDER BY ID DESC",
                        new object[] { consumTypeID, consumName, consumModel, consumSize });
                }
                else
                {
                    page = db.Page<CONSUMABLES_WAREHOUSE>(curPage, dspLen, "where NAME like @0 and (MODEL like @1 or MODEL is null ) and (SIZE1 like @2 or size1 is null) ORDER BY ID DESC",
                                       new object[] { consumName, consumModel, consumSize });
                }
            }
            catch (Exception err)
            { Console.WriteLine(err.Message); }

            // 计算各类元素的入库, 出库总量, 以及库存量
            for (int i = 0; i < page.Items.Count(); i++)
            {
                // 入库量
                page.Items[i].InCount = db.FirstOrDefault<decimal?>("select sum(t.operator_num) from CONSUMABLES_LOG t where t.CONSUMABLES_ID = @0", new object[] { page.Items[i].ID });

                // 出库量
                page.Items[i].OutCount = db.FirstOrDefault<decimal?>("select sum(t.out_num) from CONSUMABLES_LOG1 t where t.CONSUMABLES_ID = @0 and operator_type = 57", new object[] { page.Items[i].ID });

                // 剩余库存
                page.Items[i].Surplus = db.FirstOrDefault<decimal?>("select sum(t.surplus) from CONSUMABLES_LOG t where t.CONSUMABLES_ID = @0", new object[] { page.Items[i].ID });
            }

            cONSUMABLESWAREHOUSEBindingSource.DataSource = page.Items;
            ucPaing1.totalPage = page.TotalPages;
            ucPaing1.recordCnt = page.TotalItems;
            ucPaing1.curPage = curPage;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmNewConsum frmNewConsum = new FrmNewConsum();
            frmNewConsum.NewRegistEvt += frmNewConsum_NewRegistEvt;
            frmNewConsum.ShowDialog();
        }

        void frmNewConsum_NewRegistEvt()
        {
            ucPaing1_PageChanged(1, ucPaing1.dspLenght);
        }

        private void ucPaing2_PageChanged(long curPage, int dspLen)
        {
            var page = db.Page<CONSUMABLES_LOG>(curPage, dspLen, "where CONSUMABLES_ID = @0  and SURPLUS > 0  order by VALID asc, PRODUCTION_DATE asc", new object[] { consumListID });

            //for (int i = 0; i < page.Items.Count(); i++)
            //{
            //    page.Items[i].CONSUMABLES_LOG1 = db.Fetch<CONSUMABLES_LOG1>(curPage, dspLen, "where CONSUMABLES_IN_LOG_ID = @0 order by ID DESC", new object[] { page.Items[i].ID });
            //}
            cONSUMABLESLOGBindingSource.DataSource = page.Items;
                
            ucPaing2.totalPage = page.TotalPages;
            ucPaing2.recordCnt = page.TotalItems;
            ucPaing2.curPage = curPage;
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cONSUMABLESWAREHOUSEBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要入库耗材的大类.", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FrmConsumIn frmIn = new FrmConsumIn((Int64)((CONSUMABLES_WAREHOUSE)cONSUMABLESWAREHOUSEBindingSource.Current).ID);
            frmIn.NewRegistEvt += frmIn_NewRegistEvt;
            frmIn.ShowDialog();
        }

        void frmIn_NewRegistEvt()
        {
            ucPaing2_PageChanged(1, ucPaing2.dspLenght);
        }

        private void btnOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cONSUMABLESWAREHOUSEBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要入库耗材的大类.", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FrmConsumOut frmOut = new   FrmConsumOut((Int64)((CONSUMABLES_WAREHOUSE)cONSUMABLESWAREHOUSEBindingSource.Current).ID);
            frmOut.NewRegistEvt += frmOut_NewRegistEvt;
            frmOut.ShowDialog();
        }

        void frmOut_NewRegistEvt()
        {
            ucPaing2_PageChanged(1, ucPaing2.dspLenght);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            gridView4.CloseEditor();
            cONSUMABLESREQUESTLSTBindingSource.EndEdit();
            cONSUMABLESREQUESTLSTBindingSource.CurrencyManager.EndCurrentEdit();

            if (cONSUMABLESLOGBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择所需要材料的入库记录.", "错误提示");
                return;
            }
            if (cONSUMABLESWAREHOUSEBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择所需要材料的入库记录.", "错误提示");
                return;
            }

            CONSUMABLES_REQUEST_LST requestLst = new CONSUMABLES_REQUEST_LST();

            requestLst.CONSUMABLE_ID = ((CONSUMABLES_WAREHOUSE)cONSUMABLESWAREHOUSEBindingSource.Current).ID;
            requestLst.NAME = ((CONSUMABLES_WAREHOUSE)cONSUMABLESWAREHOUSEBindingSource.Current).NAME;
            requestLst.FIRM = ((CONSUMABLES_WAREHOUSE)cONSUMABLESWAREHOUSEBindingSource.Current).FIRM;
            requestLst.SIZE1 = ((CONSUMABLES_WAREHOUSE)cONSUMABLESWAREHOUSEBindingSource.Current).SIZE1;
            requestLst.MODEL = ((CONSUMABLES_WAREHOUSE)cONSUMABLESWAREHOUSEBindingSource.Current).MODEL;
            requestLst.CONSUMABLE_TYPE_CODE = ((CONSUMABLES_WAREHOUSE)cONSUMABLESWAREHOUSEBindingSource.Current).CONSUMABLES_CODE;

            requestLst.CONSUMABLES_IN_LOG_ID = ((CONSUMABLES_LOG)cONSUMABLESLOGBindingSource.Current).ID;
            requestLst.PRODUCTION_DATE = ((CONSUMABLES_LOG)cONSUMABLESLOGBindingSource.Current).PRODUCTION_DATE;
            requestLst.VALID = ((CONSUMABLES_LOG)cONSUMABLESLOGBindingSource.Current).VALID;
            requestLst.SN = ((CONSUMABLES_LOG)cONSUMABLESLOGBindingSource.Current).SN;

            if (lstConsumablesRequestLst.Where(p => p.CONSUMABLES_IN_LOG_ID == requestLst.CONSUMABLES_IN_LOG_ID).Count() == 0)
            {
                lstConsumablesRequestLst.Add(requestLst);
                cONSUMABLESREQUESTLSTBindingSource.ResetBindings(false);
            }
            else
                XtraMessageBox.Show("该材料已被选中.");
            
        }

        private void btnComfirm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView4.CloseEditor();
            cONSUMABLESREQUESTLSTBindingSource.EndEdit();
            cONSUMABLESREQUESTLSTBindingSource.CurrencyManager.EndCurrentEdit();

            if (XtraMessageBox.Show("您确实要提交该材料申请单吗?", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                string sRequestID = DateTime.Now.ToString("yyyyMMddHH24mmssffffff");
                for (int i = 0; i < lstConsumablesRequestLst.Count(); i++)
                {

                    if (lstConsumablesRequestLst[i].REQUEST_NUM == null || lstConsumablesRequestLst[i].REQUEST_NUM < 1)
                    {
                        XtraMessageBox.Show("需求数不允许小于1,请核对.", "错误提示");
                        return;
                    }
                    else
                    {
                        lstConsumablesRequestLst[i].REQUEST_ID = sRequestID;
                        lstConsumablesRequestLst[i].SURPLUS = lstConsumablesRequestLst[i].REQUEST_NUM;
                    }

                }

                if (lstConsumablesRequestLst == null || lstConsumablesRequestLst.Count() == 0)
                {
                    XtraMessageBox.Show("请选择需要申请的材料.", "错误提示");
                    return;
                }
                using (var scope = db.GetTransaction())
                {
                    for (int i = 0; i < lstConsumablesRequestLst.Count(); i++)
                    {
                        db.Insert(lstConsumablesRequestLst[i]);
                    }
                    CONSUMABLES_REQUEST request = new CONSUMABLES_REQUEST();
                    request.REQUEST_ID = sRequestID;
                    request.LOG_TIME = DateTime.Now;
                    request.OPERATOR = ClsFrmMng.WorkerID;
                    request.STATUS = ClsFrmMng.StatusComfirm;

                    request.Insert();

                    scope.Complete();

                    XtraMessageBox.Show("申请单编号: " + sRequestID + ". 已提交成功, 请等待库管确认.");
                    lstConsumablesRequestLst.Clear();
                    cONSUMABLESREQUESTLSTBindingSource.ResetBindings(false);
                }
            }

        }

        private void ucPaing3_PageChanged(long curPage, int dspLen)
        {
            var page = new Page<CONSUMABLES_REQUEST>();
            try
            {
                page = db.Page<CONSUMABLES_REQUEST>(curPage, dspLen, "WHERE OPERATOR  = @0 AND STATUS <> '删除' ORDER BY LOG_TIME DESC", ClsFrmMng.WorkerID);
            }
            catch (Exception err)
            { XtraMessageBox.Show(err.Message); }

            cONSUMABLESREQUESTBindingSource.DataSource = page.Items;
            ucPaing3.totalPage = page.TotalPages;
            ucPaing3.recordCnt = page.TotalItems;
            ucPaing3.curPage = curPage;

            //cONSUMABLESREQUESTBindingSource_CurrentItemChanged(null, null);
        }

        private void btnRefreshRequest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ucPaing3_PageChanged(1, ucPaing3.dspLenght);
        }

        private void btnConfirmA_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cONSUMABLESREQUESTBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要提交的申请单.", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (((CONSUMABLES_REQUEST)cONSUMABLESREQUESTBindingSource.Current).STATUS != ClsFrmMng.StatusComBack)
            {
                XtraMessageBox.Show("请申请单状态并非'" + ClsFrmMng.StatusComBack + "', 请确认.", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (XtraMessageBox.Show("您确实要提交该材料申请单吗?", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    var v = ((CONSUMABLES_REQUEST)cONSUMABLESREQUESTBindingSource.Current);
                    v.STATUS = ClsFrmMng.StatusComfirm;
                    v.Update();

                    ucPaing3_PageChanged(1, ucPaing3.dspLenght);
                }
            }
        }

        private void btnRecv_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cONSUMABLESREQUESTBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要确认的申请单.", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (((CONSUMABLES_REQUEST)cONSUMABLESREQUESTBindingSource.Current).STATUS != ClsFrmMng.StatusComOut)
            {
                XtraMessageBox.Show("请申请单状态并非'" + ClsFrmMng.StatusComOut + "', 请确认.", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (XtraMessageBox.Show("您确认该申请单中所有材料均已收到?", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    var v = ((CONSUMABLES_REQUEST)cONSUMABLESREQUESTBindingSource.Current);
                    v.STATUS = ClsFrmMng.StatusComOk;
                    v.Update();

                    ucPaing3_PageChanged(1, ucPaing3.dspLenght);
                }
            }
        }

        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cONSUMABLESREQUESTBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要删除的申请单.", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (XtraMessageBox.Show("您确实要提删除该材料申请单吗?", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                string s = db.ExecuteScalar<string>("select status from CONSUMABLES_REQUEST where REQUEST_ID = @0", ((CONSUMABLES_REQUEST)cONSUMABLESREQUESTBindingSource.Current).REQUEST_ID);
                if (s != ClsFrmMng.StatusComfirm)
                {
                    XtraMessageBox.Show("请申请单状态并非'" + ClsFrmMng.StatusComfirm + "', 请刷新后确认.", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var v = ((CONSUMABLES_REQUEST)cONSUMABLESREQUESTBindingSource.Current);
                    v.STATUS = ClsFrmMng.StatusComDel;
                    v.Update();

                    ucPaing3_PageChanged(1, ucPaing3.dspLenght);
                }
            }            
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (cONSUMABLESREQUESTLSTBindingSource.Current != null)
            {
                cONSUMABLESREQUESTLSTBindingSource.RemoveCurrent();
            }
        }
    }
}
