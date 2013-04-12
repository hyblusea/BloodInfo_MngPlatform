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
    public partial class FrmConsumWH : XtraForm
    {
        Database db;
        Int64? consumTypeID;
        string consumName;
        string consumModel;
        string consumSize;

        Int64 consumListID;

        public FrmConsumWH()
        {
            InitializeComponent();
            ////string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
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
            vALUEGROUPBindingSource.CurrentItemChanged += vALUEGROUPBindingSource_CurrentItemChanged;
            txtType.EditValueChanged += txtType_EditValueChanged; 

            ucPaing1.dspLenght = 30;
            ucPaing2.dspLenght = 10;
            btnSearch_ItemClick(null, null);

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

        void vALUEGROUPBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (vALUEGROUPBindingSource.Current != null)
                vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", ((VALUE_GROUP)vALUEGROUPBindingSource.Current).ID);     // 耗材名称
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
                //cONSUMABLESLOG1BindingSource.DataSource = null;
            
        }

        private void FrmConsumWH_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClsFrmMng.frmConsumWh = null;
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
            var page = db.Page<CONSUMABLES_LOG>(curPage, dspLen, "where CONSUMABLES_ID = @0 order by ID DESC", new object[] { consumListID });

            for (int i = 0; i < page.Items.Count(); i++)
            {
                page.Items[i].CONSUMABLES_LOG1 = db.Fetch<CONSUMABLES_LOG1>(curPage, dspLen, "where CONSUMABLES_IN_LOG_ID = @0 order by ID DESC", new object[] { page.Items[i].ID });
            }
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
            //FrmConsumOut frmOut = new   FrmConsumOut((Int64)((CONSUMABLES_WAREHOUSE)cONSUMABLESWAREHOUSEBindingSource.Current).ID);
            //frmOut.NewRegistEvt += frmOut_NewRegistEvt;
            //frmOut.ShowDialog();

            FrmConsumnOut_New outNew = new FrmConsumnOut_New();
            outNew.NewRegistEvt += outNew_NewRegistEvt;
            outNew.ShowDialog();
        }

        void outNew_NewRegistEvt()
        {
            ucPaing2_PageChanged(1, ucPaing2.dspLenght);
        }

       
    }
}
