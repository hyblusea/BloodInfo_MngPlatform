using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;
using PetaPoco;
using BloodInfo_MngPlatform.Models;
using System.Linq;

namespace BloodInfo_MngPlatform
{
    public partial class FrmPatientBaseInfo : DevExpress.XtraEditors.XtraForm
    {
        Database db;
        List<PATIENT_BASEINFO> lstPatientBaseInfo = new List<PATIENT_BASEINFO>();

        // 查询条件
        string sName = "%";

        public FrmPatientBaseInfo()
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("");
            //vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where groupname = 41");
        }

        private void FrmPatientBaseInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClsFrmMng.frmPatientBaseInfo = null;            
        }

        private void barbtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (txtName.EditValue != null && txtName.EditValue.ToString() != string.Empty)
                    sName = "%" + txtName.EditValue.ToString() + "%";
                else
                    sName = "%";

                ucPaing1_PageChanged(1, 30);
            }
            catch (Exception err)
            {
                XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
            }
        }

        private void FrmPatientBaseInfo_Load(object sender, EventArgs e)
        {
            barbtnSearch_ItemClick(null, null);

            // Form按钮使用设置
            List<ATH_CONTROL_ENABLE> lstFrmCtrl = ClsFrmMng.lstCtrlEnable.Where(c => c.FATHERITEM == this.Name).ToList<ATH_CONTROL_ENABLE>();
            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            for (int i = 0; i < lstFrmCtrl.Count(); i++)
            {
                dic.Add(lstFrmCtrl[i].CONTROL_NAME, Convert.ToBoolean(lstFrmCtrl[i].ENABLE));
            }
            AuthrzForDevDx.ScanFormControl ctrlEnable = new AuthrzForDevDx.ScanFormControl();
            ctrlEnable.SetBtn(this.components.Components, dic);

            ucPaing1.dspLenght = 30;
            ucPaing1.PageChanged += ucPaing1_PageChanged;

            //repositoryItemLookUpEdit1.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 10 });
            //repositoryItemLookUpEdit1.DisplayMember = "DSP_MEMBER";
            //repositoryItemLookUpEdit1.ValueMember = "VALUE_MEMBER";

            //repositoryItemLookUpEdit2.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 8 });
            //repositoryItemLookUpEdit2.DisplayMember = "DSP_MEMBER";
            //repositoryItemLookUpEdit2.ValueMember = "VALUE_MEMBER";

            //repositoryItemLookUpEdit3.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 9 });
            //repositoryItemLookUpEdit3.DisplayMember = "DSP_MEMBER";
            //repositoryItemLookUpEdit3.ValueMember = "VALUE_MEMBER";
        }

        private void barbtnPrintOrExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dgvPatientBaseInfo.ShowRibbonPrintPreview();
        }

        private void barbtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmNewPatientBaseInfo frmNewPatientBaseInfo = new FrmNewPatientBaseInfo();
            frmNewPatientBaseInfo.NewRegistEvt += frmNewPatientBaseInfo_NewRegistEvt;
            frmNewPatientBaseInfo.ShowDialog();
        }

        void frmNewPatientBaseInfo_NewRegistEvt()
        {
            barbtnSearch_ItemClick(null, null);
        }

        private void barbtnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的行。", "错误提示", MessageBoxButtons.OK);
                return;
            }
            decimal id = ((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID;
            FrmEdtPatientBaseInfo frm = new FrmEdtPatientBaseInfo((Int64)id);
            frm.NewRegistEvt += frm_NewRegistEvt;
            frm.ShowDialog();            
        }

        void frm_NewRegistEvt()
        {
            barbtnSearch_ItemClick(null, null);
        }

        private void barbtnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要删除的行。", "错误提示", MessageBoxButtons.OK);
                return;
            }
            string sName = ((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).NAME;
            if (XtraMessageBox.Show(string.Format("您确实要删除姓名为【{0}】的记录吗？", sName), "操作确认", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {                    
                    //db.OpenSharedConnection();
                    db.Execute("delete PATIENT_BASEINFO where ID = @0", ((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID);
                    //db.CloseSharedConnection();
                    barbtnSearch_ItemClick(null, null);
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
                }
            }
        }

        private void ucPaing1_PageChanged(long curPage, int dspLen)
        {
            var page = db.Page<PATIENT_BASEINFO>(curPage, dspLen, "where NAME like @0 ORDER BY CREATE_TIME DESC", new object[] { sName });
            lstPatientBaseInfo = page.Items;
            ucPaing1.totalPage = page.TotalPages;
            ucPaing1.curPage = curPage;
            ucPaing1.recordCnt = page.TotalItems;

            pATIENTBASEINFOBindingSource.DataSource = lstPatientBaseInfo;
        }

    }
}