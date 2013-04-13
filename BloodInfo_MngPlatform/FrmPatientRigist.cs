using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using System.Configuration;
using PetaPoco;
using BloodInfo_MngPlatform.Models;
using System.Linq;

namespace BloodInfo_MngPlatform
{
    public partial class FrmPatientRigist : DevExpress.XtraEditors.XtraForm
    {
        Database db;
        string sId = "%";

        public FrmPatientRigist()
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
        }

        public FrmPatientRigist(bool isAuthScan)
        {
            InitializeComponent();
            //MessageBox.Show("aaa");
        }

        private void FrmPatientRigist_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClsFrmMng.frmPatientReg = null;
        }

        private void barbtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //db.OpenSharedConnection();
            if (lblName.EditValue != null && lblName.EditValue.ToString() != string.Empty)
                sId = "%" + lblName.EditValue.ToString() + "%";
            else
                sId = "%";

            ucPaing1_PageChanged(1, 30);
            //db.CloseSharedConnection();
        }

        private void barbtnAddRegist_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmNewRegist frmNewReg = new FrmNewRegist();
            frmNewReg.NewRegistEvt += frmNewReg_NewRegistEvt;
            frmNewReg.ShowDialog();
            frmNewReg.Dispose();
            frmNewReg = null;
        }

        void frmNewReg_NewRegistEvt()
        {
            barbtnSearch_ItemClick(null, null);
        }

        private void FrmPatientRigist_Load(object sender, EventArgs e)
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

            rEGSTATUSBindingSource.DataSource = db.Fetch<REG_STATUS>("");

            ucPaing1.dspLenght = 30;
            ucPaing1.PageChanged += ucPaing1_PageChanged;
        }

        void ucPaing1_PageChanged(long curPage, int dspLen)
        {
            var page = db.Page<PATIENT_REGIST>(curPage, dspLen, "select t.* from PATIENT_REGIST t where t.NAME like @0 ORDER BY t.CREATEDATE desc", new object[] { sId });
            ucPaing1.totalPage = page.TotalPages;
            ucPaing1.curPage = curPage;
            ucPaing1.recordCnt = page.TotalItems;

            dgvPatientReg.DataSource = page.Items;
        }

        private void barbtnPrintOrExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dgvPatientReg.ShowRibbonPrintPreview();
        }
    }
}