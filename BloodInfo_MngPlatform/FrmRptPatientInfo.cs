using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Configuration;
using PetaPoco;
using BloodInfo_MngPlatform.Models;
using System.Linq;

namespace BloodInfo_MngPlatform
{
    public partial class FrmRptPatientInfo : DevExpress.XtraEditors.XtraForm
    {
        Dictionary<string, PATIENT_REGIST> reger = new Dictionary<string, PATIENT_REGIST>();

        Database db;
        List<PATIENT_BASEINFO> lstBaseInfo = new List<PATIENT_BASEINFO>();
        List<PATIENT_REGIST> lstReg = new List<PATIENT_REGIST>();

        public FrmRptPatientInfo()
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
        }

        private void FrmRptPatientInfo_Load(object sender, EventArgs e)
        {
            gridView2.FocusedRowChanged += gridView2_FocusedRowChanged;
            pATIENTBASEINFOBindingSource.CurrentItemChanged += pATIENTBASEINFOBindingSource_CurrentItemChanged;

            // Form按钮使用设置
            List<ATH_CONTROL_ENABLE> lstFrmCtrl = ClsFrmMng.lstCtrlEnable.Where(c => c.FATHERITEM == this.Name).ToList<ATH_CONTROL_ENABLE>();
            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            for (int i = 0; i < lstFrmCtrl.Count(); i++)
            {
                dic.Add(lstFrmCtrl[i].CONTROL_NAME, Convert.ToBoolean(lstFrmCtrl[i].ENABLE));
            }
            AuthrzForDevDx.ScanFormControl ctrlEnable = new AuthrzForDevDx.ScanFormControl();
            ctrlEnable.SetBtn(this.components.Components, dic);
        }

        void pATIENTBASEINFOBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            
        }

        // 明细表行改变事件
        void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var oID = gridView2.GetRowCellValue(e.FocusedRowHandle,  "ID");
            var oName = gridView2.GetRowCellValue(e.FocusedRowHandle,  gridView2.Columns["NAME"]);
            //groupControl2.Text = string.Format("医疗过程记录 -- 患者姓名: {0}", oName.ToString());
        }

        private void FrmRptPatientInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClsFrmMng.frmRptPatientInfo = null;
        }

        private void btnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lstBaseInfo = db.Fetch<PATIENT_BASEINFO, PATIENT_REGIST>("select t.*, t1.*  from PATIENT_BASEINFO t join PATIENT_REGIST t1 on T.ID = T1.BASE_INFO_ID  order by T.ID");
            //lstBaseInfo = db.Fetch<PATIENT_BASEINFO, PATIENT_REGIST, PATIENT_BASEINFO>( new BaseInfoPostRelator ().MapIt, "select t.*, t1.*  from PATIENT_BASEINFO t join PATIENT_REGIST t1 on T.CASE_HISTORY_ID = T1.CASE_HISTORY_ID");

            pATIENTBASEINFOBindingSource.DataSource = lstBaseInfo;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            richEditControl1.ShowPrintPreview();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dgvNurseLog.ShowRibbonPrintPreview();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dgvDocHmodLog.ShowRibbonPrintPreview();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dgvEvaluate.ShowRibbonPrintPreview();
        }

        public class BaseInfoPostRelator
        {
            public PATIENT_BASEINFO current;
            public PATIENT_BASEINFO MapIt(PATIENT_BASEINFO a, PATIENT_REGIST p)
            {
                // Terminating call.  Since we can return null from this function
                // we need to be ready for PetaPoco to callback later with null
                // parameters
                if (a == null)
                    return current;

                // Is this the same author as the current one we're processing
                if (current != null && current.ID == a.ID)
                {
                    // Yes, just add this post to the current author's collection of posts
                    current.PATIENT_REG.Add(p);

                    // Return null to indicate we're not done with this author yet
                    return null;
                }

                // This is a different author to the current one, or this is the 
                // first time through and we don't have an author yet

                // Save the current author
                var prev = current;

                // Setup the new current author
                current = a;
                current.PATIENT_REG = new List<PATIENT_REGIST>();
                current.PATIENT_REG.Add(p);

                //Return the now populated previous author (or null if first time through)
                return prev;
            }
        }
    }



   

}