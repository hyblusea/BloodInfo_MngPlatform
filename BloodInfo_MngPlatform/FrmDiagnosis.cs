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
    public partial class FrmDiagnosis : DevExpress.XtraEditors.XtraForm
    {
        Database db;
        string sName = string.Empty;

        public FrmDiagnosis()
        {
            InitializeComponent();
            db = new Database("XE");
        }

        private void FrmDiagnosis_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClsFrmMng.frmDiagnosis = null;
        }

        private void FrmDiagnosis_Load(object sender, EventArgs e)
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

            ucPaing1.dspLenght = 10;
            ucPaing1.PageChanged += ucPaing1_PageChanged;

            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("");
            vALUEGROUPBindingSource.DataSource = db.Fetch<VALUE_GROUP>("where FATHERID = @0", 18);
        }

        void ucPaing1_PageChanged(long curPage, int dspLen)
        {
            var page = db.Page<PATIENT_BASEINFO>(curPage, dspLen, "where NAME like @0 ORDER BY CREATE_TIME DESC", new object[] { sName });
            pATIENTBASEINFOBindingSource.DataSource = page.Items;
            ucPaing1.totalPage = page.TotalPages;
            ucPaing1.curPage = curPage;
            ucPaing1.recordCnt = page.TotalItems;
        }

        void pATIENTBASEINFOBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            Refresh1();
            Refresh2();
            Refresh3();
            Refresh4();
            Refresh5();
            Refresh6();
            Refresh7();
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

        // 刷新原发病诊断
        private void Refresh1()
        {
            if (pATIENTBASEINFOBindingSource.Current != null)
                dIAGNOSISPRIMARYDISEASEBindingSource.DataSource = db.Fetch<DIAGNOSIS_PRIMARY_DISEASE>("where PT_ID = @0", (Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID);
            else
                dIAGNOSISPRIMARYDISEASEBindingSource.DataSource = null;
            
        }

        // 刷新病理诊断
        private void Refresh2()
        {
            if (pATIENTBASEINFOBindingSource.Current != null)
                dIAGNOSISPATHOLOGICALBindingSource.DataSource = db.Fetch<DIAGNOSIS_PATHOLOGICAL>("where PT_ID = @0", (Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID);
            else
                dIAGNOSISPATHOLOGICALBindingSource.DataSource = null;
        }

        // 刷新并发症诊断
        private void Refresh3()
        {
            if (pATIENTBASEINFOBindingSource.Current != null)
                dIAGNOSISCOMPLICATIONBindingSource.DataSource = db.Fetch<DIAGNOSIS_COMPLICATION>("where PT_ID = @0", (Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID);
            else
                dIAGNOSISCOMPLICATIONBindingSource.DataSource = null;
        }

        // 刷新传染病诊断
        private void Refresh4()
        {
            if (pATIENTBASEINFOBindingSource.Current != null)
                dIAGNOSISINFECTIOUSDISEASEBindingSource.DataSource = db.Fetch<DIAGNOSIS_INFECTIOUS_DISEASE>("where PT_ID = @0", (Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID);
            else
                dIAGNOSISINFECTIOUSDISEASEBindingSource.DataSource = null;
        }

        // 刷新传染病诊断
        private void Refresh5()
        {
            if (pATIENTBASEINFOBindingSource.Current != null)
                dIAGNOSISTUMORBindingSource.DataSource = db.Fetch<DIAGNOSIS_TUMOR>("where PT_ID = @0", (Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID);
            else
                dIAGNOSISTUMORBindingSource.DataSource = null;
        }

        // 刷新过敏诊断
        private void Refresh6()
        {
            if (pATIENTBASEINFOBindingSource.Current != null)
                dIAGNOSISALLERGYBindingSource.DataSource = db.Fetch<DIAGNOSIS_ALLERGY>("where PT_ID = @0", (Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID);
            else
                dIAGNOSISALLERGYBindingSource.DataSource = null;
        }

        // 刷新转归情况
        private void Refresh7()
        {
            if (pATIENTBASEINFOBindingSource.Current != null)
                dIAGNOSISOUTCOMEBindingSource.DataSource = db.Fetch<DIAGNOSIS_OUTCOME>("where PT_ID = @0", (Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID);
            else
                dIAGNOSISOUTCOMEBindingSource.DataSource = null;
        }


        // 原发病诊断 //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if( pATIENTBASEINFOBindingSource.Current == null )
            {
                XtraMessageBox.Show("请选择需要新增信息的患者记录.", "错误提示");
                return;
            }

            FrmNewDiagonsis_Primary frm1 = new FrmNewDiagonsis_Primary((Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID);
            frm1.NewRegistEvt += frm1_NewRegistEvt;
            frm1.ShowDialog();
        }

        
        void frm1_NewRegistEvt()
        {
            Refresh1();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dIAGNOSISPRIMARYDISEASEBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的记录.", "错误提示");
                return;
            }

            FrmEdtDiagosis_Primary frm1 = new  FrmEdtDiagosis_Primary((Int64)((DIAGNOSIS_PRIMARY_DISEASE)dIAGNOSISPRIMARYDISEASEBindingSource.Current).ID);
            frm1.NewRegistEvt += frm1_NewRegistEvt;
            frm1.ShowDialog();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dIAGNOSISPRIMARYDISEASEBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要删除的记录.", "错误提示");
                return;
            }
            if (XtraMessageBox.Show("您确实要删除该记录吗?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.Yes)
            {
                db.Delete("DIAGNOSIS_PRIMARY_DISEASE", "ID", null, (Int64)((DIAGNOSIS_PRIMARY_DISEASE)dIAGNOSISPRIMARYDISEASEBindingSource.Current).ID);
                Refresh1();
            }
        }

        // 病理诊断 ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要新增信息的患者记录.", "错误提示");
                return;
            }

            FrmNewDiagonsis_Pathological frm2 = new  FrmNewDiagonsis_Pathological((Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID);
            frm2.NewRegistEvt += frm2_NewRegistEvt;
            frm2.ShowDialog();
        }

        void frm2_NewRegistEvt()
        {
            Refresh2();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dIAGNOSISPATHOLOGICALBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的记录.", "错误提示");
                return;
            }

            FrmEdtDiagonsis_Pathological frm2 = new  FrmEdtDiagonsis_Pathological((Int64)((DIAGNOSIS_PATHOLOGICAL)dIAGNOSISPATHOLOGICALBindingSource.Current).ID);
            frm2.NewRegistEvt += frm2_NewRegistEvt;
            frm2.ShowDialog();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dIAGNOSISPATHOLOGICALBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要删除的记录.", "错误提示");
                return;
            }
            if (XtraMessageBox.Show("您确实要删除该记录吗?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.Yes)
            {
                db.Delete("DIAGNOSIS_PATHOLOGICAL", "ID", null, (Int64)((DIAGNOSIS_PATHOLOGICAL)dIAGNOSISPATHOLOGICALBindingSource.Current).ID);
                Refresh2();
            }
        }

        // 并发症诊断 ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要新增信息的患者记录.", "错误提示");
                return;
            }

            FrmNewDiagonsis_Complication frm3 = new  FrmNewDiagonsis_Complication((Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID);
            frm3.NewRegistEvt += frm3_NewRegistEvt;
            frm3.ShowDialog();
        }

        void frm3_NewRegistEvt()
        {
            Refresh3();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dIAGNOSISCOMPLICATIONBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的记录.", "错误提示");
                return;
            }

            FrmEdtDiagonsis_Complication frm3 = new  FrmEdtDiagonsis_Complication((Int64)((DIAGNOSIS_COMPLICATION)dIAGNOSISCOMPLICATIONBindingSource.Current).ID);
            frm3.NewRegistEvt += frm3_NewRegistEvt;
            frm3.ShowDialog();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dIAGNOSISCOMPLICATIONBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要删除的记录.", "错误提示");
                return;
            }
            if (XtraMessageBox.Show("您确实要删除该记录吗?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.Yes)
            {
                db.Delete("DIAGNOSIS_COMPLICATION", "ID", null, (Int64)((DIAGNOSIS_COMPLICATION)dIAGNOSISCOMPLICATIONBindingSource.Current).ID);
                Refresh3();
            }
        }

        // 传染病诊断 /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要新增信息的患者记录.", "错误提示");
                return;
            }

            FrmNewDiagonsis_Infe frm4 = new  FrmNewDiagonsis_Infe((Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID);
            frm4.NewRegistEvt += frm4_NewRegistEvt;
            frm4.ShowDialog();
        }

        void frm4_NewRegistEvt()
        {
            Refresh4();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dIAGNOSISINFECTIOUSDISEASEBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的记录.", "错误提示");
                return;
            }

            FrmEdtDiagonsis_Infe frm4 = new  FrmEdtDiagonsis_Infe((Int64)((DIAGNOSIS_INFECTIOUS_DISEASE)dIAGNOSISINFECTIOUSDISEASEBindingSource.Current).ID);
            frm4.NewRegistEvt += frm4_NewRegistEvt;
            frm4.ShowDialog();
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dIAGNOSISINFECTIOUSDISEASEBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要删除的记录.", "错误提示");
                return;
            }
            if (XtraMessageBox.Show("您确实要删除该记录吗?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.Yes)
            {
                db.Delete("DIAGNOSIS_INFECTIOUS_DISEASE", "ID", null, (Int64)((DIAGNOSIS_INFECTIOUS_DISEASE)dIAGNOSISINFECTIOUSDISEASEBindingSource.Current).ID);
                Refresh4();
            }
        }

        // 肿瘤诊断 ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要新增信息的患者记录.", "错误提示");
                return;
            }

            FrmNewDiagonsis_Tumor frm5 = new  FrmNewDiagonsis_Tumor((Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID);
            frm5.NewRegistEvt += frm5_NewRegistEvt;
            frm5.ShowDialog();
        }

        void frm5_NewRegistEvt()
        {
            Refresh5();
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dIAGNOSISTUMORBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的记录.", "错误提示");
                return;
            }

            FrmEdtDiagonsis_Tumor frm5 = new FrmEdtDiagonsis_Tumor((Int64)((DIAGNOSIS_INFECTIOUS_DISEASE)dIAGNOSISTUMORBindingSource.Current).ID);
            frm5.NewRegistEvt += frm5_NewRegistEvt;
            frm5.ShowDialog();
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dIAGNOSISTUMORBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要删除的记录.", "错误提示");
                return;
            }
            if (XtraMessageBox.Show("您确实要删除该记录吗?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.Yes)
            {
                db.Delete("DIAGNOSIS_TUMOR", "ID", null, (Int64)((DIAGNOSIS_TUMOR)dIAGNOSISTUMORBindingSource.Current).ID);
                Refresh5();
            }
        }


        // 过敏诊断 ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要新增信息的患者记录.", "错误提示");
                return;
            }

            FrmNewDiagonsis_Allergy frm6 = new FrmNewDiagonsis_Allergy((Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID);
            frm6.NewRegistEvt += frm6_NewRegistEvt;
            frm6.ShowDialog();
        }

        void frm6_NewRegistEvt()
        {
            Refresh6();
        }

        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dIAGNOSISALLERGYBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的记录.", "错误提示");
                return;
            }

            FrmEdtDiagosis_Allergy frm6 = new  FrmEdtDiagosis_Allergy((Int64)((DIAGNOSIS_ALLERGY)dIAGNOSISALLERGYBindingSource.Current).ID);
            frm6.NewRegistEvt += frm6_NewRegistEvt;
            frm6.ShowDialog();
        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dIAGNOSISALLERGYBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要删除的记录.", "错误提示");
                return;
            }
            if (XtraMessageBox.Show("您确实要删除该记录吗?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.Yes)
            {
                db.Delete("DIAGNOSIS_ALLERGY", "ID", null, (Int64)((DIAGNOSIS_ALLERGY)dIAGNOSISALLERGYBindingSource.Current).ID);
                Refresh6();
            }
        }

        // 转归情况
        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要新增信息的患者记录.", "错误提示");
                return;
            }

            FrmNewDiagonsis_Outcome frm7 = new FrmNewDiagonsis_Outcome((Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID);
            frm7.NewRegistEvt += frm7_NewRegistEvt;
            frm7.ShowDialog();
        }

        void frm7_NewRegistEvt()
        {
            Refresh7();
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dIAGNOSISOUTCOMEBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的记录.", "错误提示");
                return;
            }

            FrmEdtDiagosis_Outcome frm7 = new FrmEdtDiagosis_Outcome((Int64)((DIAGNOSIS_OUTCOME)dIAGNOSISOUTCOMEBindingSource.Current).ID);
            frm7.NewRegistEvt += frm7_NewRegistEvt;
            frm7.ShowDialog();
        }

        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dIAGNOSISOUTCOMEBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要删除的记录.", "错误提示");
                return;
            }
            if (XtraMessageBox.Show("您确实要删除该记录吗?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.Yes)
            {
                db.Delete("DIAGNOSIS_OUTCOME", "ID", null, (Int64)((DIAGNOSIS_OUTCOME)dIAGNOSISOUTCOMEBindingSource.Current).ID);
                Refresh7();
            }
        }

        

        


        

    }
}
