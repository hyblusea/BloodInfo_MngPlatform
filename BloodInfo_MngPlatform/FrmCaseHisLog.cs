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
    public partial class FrmCaseHisLog : DevExpress.XtraEditors.XtraForm
    {
        Database db;
        //List<PATIENT_BASEINFO> reg = new List<PATIENT_BASEINFO>();
        List<PATIENT_BASEINFO> lstPatientBaseInfo = new List<PATIENT_BASEINFO>();
        List<CASE_HISTORY> caseHis = new List<CASE_HISTORY>();
        List<DRUG_ALLERGY_HISTORY> lstAllergy = new List<DRUG_ALLERGY_HISTORY>();
        List<INDUCEMENT_HISTORY> lstInduce = new List<INDUCEMENT_HISTORY>();
        List<UREMIC_SYMPTOMS_HI> lstUremic = new List<UREMIC_SYMPTOMS_HI>();
        List<DOSE_HISTORY> lstDose = new List<DOSE_HISTORY>();
        List<RENAL_REPLACEMENT_THERAPY_HI> lstSln = new List<RENAL_REPLACEMENT_THERAPY_HI>();
        List<VASCULARPATH_HISTORY> lstPath = new List<VASCULARPATH_HISTORY>();
        List<BODY_CHECK_HISTORY> lstBody = new List<BODY_CHECK_HISTORY>();
        List<ADDTION_CHECK_HISTORY> lstAdd = new List<ADDTION_CHECK_HISTORY>();

        string sName = string.Empty;

        public FrmCaseHisLog()
        {
            InitializeComponent();

            ////string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
        }

        private void FrmCaseHisLog_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClsFrmMng.frmCaseHisLog = null;
        }

        private void FrmCaseHisLog_Load(object sender, EventArgs e)
        {
            pATIENTBASEINFOBindingSource.CurrentItemChanged += pATIENTBASEINFOBindingSource_CurrentItemChanged;
            btnSearch_ItemClick(null, null);

            // Form按钮使用设置
            List<ATH_CONTROL_ENABLE> lstFrmCtrl = ClsFrmMng.lstCtrlEnable.Where(c => c.FATHERITEM == this.Name).ToList<ATH_CONTROL_ENABLE>();
            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            for (int i = 0; i < lstFrmCtrl.Count(); i++)
            {
                dic.Add(lstFrmCtrl[i].CONTROL_NAME, Convert.ToBoolean(lstFrmCtrl[i].ENABLE));
            }
            AuthrzForDevDx.ScanFormControl ctrlEnable = new AuthrzForDevDx.ScanFormControl();
            ctrlEnable.SetBtn(this.components.Components, dic);

            // 设置值集
            repositoryItemGridLookUpEdit3.DataSource = ClsFrmMng.lstHaveOrNull;
            repositoryItemGridLookUpEdit3.DisplayMember = "MEMO";
            repositoryItemGridLookUpEdit3.ValueMember = "ID";

            repositoryItemGridLookUpEdit4.DataSource = ClsFrmMng.lstHaveOrNull;
            repositoryItemGridLookUpEdit4.DisplayMember = "MEMO";
            repositoryItemGridLookUpEdit4.ValueMember = "ID";

            repositoryItemLookUpEdit1.DataSource = db.Fetch<VALUE_CODE>("where groupname = @0", new object[] { 11 });
            repositoryItemLookUpEdit1.DisplayMember = "DSP_MEMBER";
            repositoryItemLookUpEdit1.ValueMember = "VALUE_MEMBER";

            bsdValueCode.DataSource = db.Fetch<VALUE_CODE>("");

            ucPaing1.dspLenght = 10;
            ucPaing1.PageChanged += ucPaing1_PageChanged;
        }

        void ucPaing1_PageChanged(long curPage, int dspLen)
        {
            var page = db.Page<PATIENT_BASEINFO>(curPage, dspLen, "where NAME like @0 ORDER BY CREATE_TIME DESC", new object[] { sName });
            lstPatientBaseInfo = page.Items;
            ucPaing1.totalPage = page.TotalPages;
            ucPaing1.curPage = curPage;
            ucPaing1.recordCnt = page.TotalItems;

            pATIENTBASEINFOBindingSource.DataSource = lstPatientBaseInfo;
        }

        void pATIENTBASEINFOBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current != null)
            {
                PATIENT_BASEINFO p = ((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current);
                string sPatientName = p.NAME;
                lblName1.Caption = sPatientName;
                lblName2.Caption = sPatientName;
                lblName3.Caption = sPatientName;
                lblName4.Caption = sPatientName;
                lblName5.Caption = sPatientName;
                lblName6.Caption = sPatientName;
                lblName7.Caption = sPatientName;
                lblName8.Caption = sPatientName;
                lblName9.Caption = sPatientName;

                // 病历记录
                caseHis = db.Fetch<CASE_HISTORY>(string.Format("where BASE_INFO_ID ={0} order by log_date desc", p.ID));
                cASEHISTORYBindingSource.DataSource = caseHis;

                // 药物过敏记录
                lstAllergy = db.Fetch<DRUG_ALLERGY_HISTORY>(string.Format("where BASE_INFO_ID ={0} order by log_Time desc", p.ID));
                dRUGALLERGYHISTORYBindingSource.DataSource = lstAllergy;

                // 诱因记录
                lstInduce = db.Fetch<INDUCEMENT_HISTORY>("where BASE_INFO_ID = @0 order by log_time desc", new object[] { p.ID });
                iNDUCEMENTHISTORYBindingSource.DataSource = lstInduce;

                // 尿素症
                lstUremic = db.Fetch<UREMIC_SYMPTOMS_HI>("where BASE_INFO_ID = @0 order by log_time desc", new object[] { p.ID });
                uREMICSYMPTOMSHIBindingSource.DataSource = lstUremic;

                // 用药
                lstDose = db.Fetch<DOSE_HISTORY>("where BASE_INFO_ID = @0 order by log_time desc", new object[] { p.ID });
                dOSEHISTORYBindingSource.DataSource = lstDose;

                // 肾脏代替治疗
                lstSln = db.Fetch<RENAL_REPLACEMENT_THERAPY_HI>("where BASE_INFO_ID = @0 order by log_time desc", new object[] { p.ID });
                rENALREPLACEMENTTHERAPYHIBindingSource.DataSource = lstSln;

                // 血管通路
                lstPath = db.Fetch<VASCULARPATH_HISTORY>("where BASE_INFO_ID = @0 order by log_time desc", new object[] { p.ID });
                vASCULARPATHHISTORYBindingSource.DataSource = lstPath;

                // 体格检查
                lstBody = db.Fetch<BODY_CHECK_HISTORY>("where BASE_INFO_ID = @0 order by log_time desc", new object[] { p.ID });
                bODYCHECKHISTORYBindingSource.DataSource = lstBody;

                // 辅助检查
                lstAdd = db.Fetch<ADDTION_CHECK_HISTORY>("where BASE_INFO_ID = @0 order by log_time desc", new object[] { p.ID });
                aDDTIONCHECKHISTORYBindingSource.DataSource = lstAdd;

            }
        }


        private void btnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (txtName.EditValue != null && txtName.EditValue.ToString() != string.Empty)
                    sName = "%" + txtName.EditValue.ToString() + "%";
                else
                    sName = "%";

                ucPaing1_PageChanged(1, 10);
            }
            catch (Exception err)
            {
                XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
            }
        }

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current == null)
                return;

            FrmNewCaseHis frmNewCaseHis = new FrmNewCaseHis(false, (Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID, 0);
            frmNewCaseHis.NewRegistEvt += frmNewCaseHis_NewRegistEvt;
            frmNewCaseHis.ShowDialog();
        }

        void frmNewCaseHis_NewRegistEvt()
        {
            //caseHis = db.Fetch<CASE_HISTORY>(string.Format("where CASE_HIST_ID ={0} order by LOG_DATE	desc", ((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).CASE_HISTORY_ID));
            //cASEHISTORYBindingSource.DataSource = caseHis;
            pATIENTBASEINFOBindingSource_CurrentItemChanged(null, null);
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cASEHISTORYBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的行。", "错误提示", MessageBoxButtons.OK);
                return;
            }
            Int64 id = (Int64)((CASE_HISTORY)cASEHISTORYBindingSource.Current).ID;
            FrmEdtCaseHis frm = new FrmEdtCaseHis(id);
            frm.NewRegistEvt += frm_NewRegistEvt;
            frm.ShowDialog();          
        }

        void frm_NewRegistEvt()
        {
            pATIENTBASEINFOBindingSource_CurrentItemChanged(null, null);
        }

        private void btnGrugsNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current == null)
                return;

            FrmNewDrugsAllergy frmNewDrugsAllergy = new FrmNewDrugsAllergy((Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID, 0);
            frmNewDrugsAllergy.NewRegistEvt += frmNewDrugsAllergy_NewRegistEvt;
            frmNewDrugsAllergy.ShowDialog();
        }

        void frmNewDrugsAllergy_NewRegistEvt()
        {
            //lstAllergy = db.Fetch<DRUG_ALLERGY_HISTORY>(string.Format("where CASE_HIS_ID ={0} order by LOG_TIME	desc", ((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).CASE_HISTORY_ID));
            //dRUGALLERGYHISTORYBindingSource.DataSource = lstAllergy;
            pATIENTBASEINFOBindingSource_CurrentItemChanged(null, null);
        }

        private void btnEditGrugs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dRUGALLERGYHISTORYBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的行。", "错误提示", MessageBoxButtons.OK);
                return;
            }
            Int64 id = (Int64)((DRUG_ALLERGY_HISTORY)dRUGALLERGYHISTORYBindingSource.Current).ID;
            FrmEdtDrugsAllergy frmEdtDrugsAllergy = new FrmEdtDrugsAllergy(id);
            frmEdtDrugsAllergy.NewRegistEvt += frmEdtDrugsAllergy_NewRegistEvt;
            frmEdtDrugsAllergy.ShowDialog();          
        }

        void frmEdtDrugsAllergy_NewRegistEvt()
        {
            pATIENTBASEINFOBindingSource_CurrentItemChanged(null, null);
        }

        private void btnNewInduce_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current == null)
                return;

            FrmNewInduce frmNewInduce = new FrmNewInduce((Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID, 0);
            frmNewInduce.NewRegistEvt += frmNewInduce_NewRegistEvt;
            frmNewInduce.ShowDialog();
        }

        void frmNewInduce_NewRegistEvt()
        {
            pATIENTBASEINFOBindingSource_CurrentItemChanged(null, null);
        }

        private void btnEditDuce_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (iNDUCEMENTHISTORYBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的行。", "错误提示", MessageBoxButtons.OK);
                return;
            }
            Int64 id = (Int64)((INDUCEMENT_HISTORY)iNDUCEMENTHISTORYBindingSource.Current).ID;
            FrmEdtInduce frmEdtInduce = new  FrmEdtInduce(id);
            frmEdtInduce.NewRegistEvt += frmEdtInduce_NewRegistEvt;
            frmEdtInduce.ShowDialog();          
        }

        void frmEdtInduce_NewRegistEvt()
        {
            pATIENTBASEINFOBindingSource_CurrentItemChanged(null, null);
        }

        private void btnNewUrmic_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current == null)
                return;

            FrmNewUremic frmNewUremic = new  FrmNewUremic((Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID, 0);
            frmNewUremic.NewRegistEvt += frmNewUremic_NewRegistEvt;
            frmNewUremic.ShowDialog();
        }

        void frmNewUremic_NewRegistEvt()
        {
            pATIENTBASEINFOBindingSource_CurrentItemChanged(null, null);
        }

        private void btnEdtUremic_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (uREMICSYMPTOMSHIBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的行。", "错误提示", MessageBoxButtons.OK);
                return;
            }
            Int64 id = (Int64)((UREMIC_SYMPTOMS_HI)uREMICSYMPTOMSHIBindingSource.Current).ID;
            FrmEdtUremic frmEdtUremic = new  FrmEdtUremic(id);
            frmEdtUremic.NewRegistEvt += frmEdtUremic_NewRegistEvt;
            frmEdtUremic.ShowDialog();          
        }

        void frmEdtUremic_NewRegistEvt()
        {
            pATIENTBASEINFOBindingSource_CurrentItemChanged(null, null);
        }

        private void btnNewDose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current == null)
                return;

            FrmNewDose frmNewDose = new FrmNewDose((Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID, 0);
            frmNewDose.NewRegistEvt += frmNewDose_NewRegistEvt;
            frmNewDose.ShowDialog();
        }

        void frmNewDose_NewRegistEvt()
        {
            pATIENTBASEINFOBindingSource_CurrentItemChanged(null, null);
        }

        private void btnEdtDose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dOSEHISTORYBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的行。", "错误提示", MessageBoxButtons.OK);
                return;
            }
            Int64 id = (Int64)((DOSE_HISTORY)dOSEHISTORYBindingSource.Current).ID;
            FrmEdtDose frmEdtDose = new  FrmEdtDose(id);
            frmEdtDose.NewRegistEvt += frmEdtDose_NewRegistEvt;
            frmEdtDose.ShowDialog();      
        }

        void frmEdtDose_NewRegistEvt()
        {
            pATIENTBASEINFOBindingSource_CurrentItemChanged(null, null);
        }

        private void btnNewSln_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current == null)
                return;

            FrmNewSln frmNewSln = new FrmNewSln((Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID, 0);
            frmNewSln.NewRegistEvt += frmNewSln_NewRegistEvt;
            frmNewSln.ShowDialog();
        }

        void frmNewSln_NewRegistEvt()
        {
            pATIENTBASEINFOBindingSource_CurrentItemChanged(null, null);
        }

        private void btnEdtSln_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (rENALREPLACEMENTTHERAPYHIBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的行。", "错误提示", MessageBoxButtons.OK);
                return;
            }
            Int64 id = (Int64)((RENAL_REPLACEMENT_THERAPY_HI)rENALREPLACEMENTTHERAPYHIBindingSource.Current).ID;
            FrmEdtSln frmEdtSln = new  FrmEdtSln(id);
            frmEdtSln.NewRegistEvt += frmEdtSln_NewRegistEvt;
            frmEdtSln.ShowDialog();     
        }

        void frmEdtSln_NewRegistEvt()
        {
            pATIENTBASEINFOBindingSource_CurrentItemChanged(null, null);
        }

        private void btnNewPath_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current == null)
                return;

            FrmNewPath frmNewPath = new FrmNewPath((Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID, 0);
            frmNewPath.NewRegistEvt += frmNewPath_NewRegistEvt;
            frmNewPath.ShowDialog();
        }

        void frmNewPath_NewRegistEvt()
        {
            pATIENTBASEINFOBindingSource_CurrentItemChanged(null, null);
        }

        private void btnEdtPath_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (vASCULARPATHHISTORYBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的行。", "错误提示", MessageBoxButtons.OK);
                return;
            }
            Int64 id = (Int64)((VASCULARPATH_HISTORY)vASCULARPATHHISTORYBindingSource.Current).ID;
            FrmEdtPath frmEdtPath = new  FrmEdtPath(id);
            frmEdtPath.NewRegistEvt += frmEdtPath_NewRegistEvt;
            frmEdtPath.ShowDialog();     
        }

        void frmEdtPath_NewRegistEvt()
        {
            pATIENTBASEINFOBindingSource_CurrentItemChanged(null, null);
        }

        private void btnNewBodyCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current == null)
                return;

            FrmNewBodyCheck frmNewbody = new FrmNewBodyCheck((Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID, 0);
            frmNewbody.NewRegistEvt += frmNewbody_NewRegistEvt;
            frmNewbody.ShowDialog();
        }

        void frmNewbody_NewRegistEvt()
        {
            pATIENTBASEINFOBindingSource_CurrentItemChanged(null, null);
        }

        private void btnEdtBodyCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bODYCHECKHISTORYBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的行。", "错误提示", MessageBoxButtons.OK);
                return;
            }
            Int64 id = (Int64)((BODY_CHECK_HISTORY)bODYCHECKHISTORYBindingSource.Current).ID;
            FrmEdtBody frmEdtBody = new  FrmEdtBody(id);
            frmEdtBody.NewRegistEvt += frmEdtBody_NewRegistEvt;
            frmEdtBody.ShowDialog(); 
        }

        void frmEdtBody_NewRegistEvt()
        {
            pATIENTBASEINFOBindingSource_CurrentItemChanged(null, null);
        }

        private void btnNewAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current == null)
                return;

            FrmNewAdd frmNewAdd = new FrmNewAdd((Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID, 0);
            frmNewAdd.NewRegistEvt += frmNewAdd_NewRegistEvt;
            frmNewAdd.ShowDialog();
        }

        void frmNewAdd_NewRegistEvt()
        {
            pATIENTBASEINFOBindingSource_CurrentItemChanged(null, null);
        }

        private void btnEdtAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (aDDTIONCHECKHISTORYBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的行。", "错误提示", MessageBoxButtons.OK);
                return;
            }
            Int64 id = (Int64)((ADDTION_CHECK_HISTORY)aDDTIONCHECKHISTORYBindingSource.Current).ID;
            FrmEdtAdd frmEdtAdd = new  FrmEdtAdd(id);
            frmEdtAdd.NewRegistEvt += frmEdtAdd_NewRegistEvt;
            frmEdtAdd.ShowDialog(); 
        }

        void frmEdtAdd_NewRegistEvt()
        {
            pATIENTBASEINFOBindingSource_CurrentItemChanged(null, null);
        }

        private void dgvPatientBaseInfo_Click(object sender, EventArgs e)
        {

        }
    }
}