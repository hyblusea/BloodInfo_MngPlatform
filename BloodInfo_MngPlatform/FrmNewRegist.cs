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
    public partial class FrmNewRegist : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        Database db;
        
        List<OUTPATIENT_CATEGORY> lstOutpatientCategory;
        PATIENT_REGIST patientReg = new PATIENT_REGIST();

        string _sWorkID = string.Empty;
        Int64 _baseID;
        public FrmNewRegist()
        {
            InitializeComponent();
            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            //db.OpenSharedConnection();
            lstOutpatientCategory = db.Fetch<OUTPATIENT_CATEGORY>("");
            OUTPATIENT_CATEGORYTextEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            OUTPATIENT_CATEGORYTextEdit.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CATEGORY_ID"));
            OUTPATIENT_CATEGORYTextEdit.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CATEGORY_NAME"));
            OUTPATIENT_CATEGORYTextEdit.Properties.DataSource = lstOutpatientCategory;

            OUTPATIENT_CATEGORYTextEdit.Properties.ValueMember = "CATEGORY_ID";         // 对应ID  
            OUTPATIENT_CATEGORYTextEdit.Properties.DisplayMember = "CATEGORY_NAME";     // 显示内容

            patientReg.STATUS = 0;
            patientReg.OPERATOR = ClsFrmMng.WorkerID;

            

            pATIENTREGISTBindingSource.DataSource = patientReg;
            //db.CloseSharedConnection();

            ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            ruleNoEmpty.ErrorText = "该项不能为空。";
            dxValidationProvider1.SetValidationRule(NAMETextEdit, ruleNoEmpty);
            dxValidationProvider1.SetValidationRule(OUTPATIENT_CATEGORYTextEdit, ruleNoEmpty);

            pATIENTBASEINFOBindingSource.DataSource = db.Fetch<PATIENT_BASEINFO>("");
            NAMETextEdit.Properties.EditValueChanged += Properties_EditValueChanged;
            
        }

        void Properties_EditValueChanged(object sender, EventArgs e)
        {
            if (NAMETextEdit.EditValue == null)
            {
                AGESpinEdit.EditValue = null;
                SEXTextEdit.EditValue = null;
                NAMETextEdit.EditValue = null;
                DIALYSIS_IDTextEdit.EditValue = null;
            }
            else if (searchLookUpEdit1View.GetFocusedRow() != null)
            {
                object o1 = searchLookUpEdit1View.GetFocusedRow();

                AGESpinEdit.EditValue = (DateTime.Now.Year - ((PATIENT_BASEINFO)o1).DATEOFBTH.Year);
                SEXTextEdit.EditValue = ((PATIENT_BASEINFO)o1).SEX;
                
                _baseID = (Int64)((PATIENT_BASEINFO)o1).ID;

                // 查询该患者所签到的透析机机位
                try
                {
                    MACHINE_SCHEDULE ms = db.SingleOrDefault<MACHINE_SCHEDULE>(string.Format("where to_char( SCHEDULE_TIME , 'yyyy-MM-dd') = '{0}' and PT_ID = @0", DateTime.Now.ToString("yyyy-MM-dd")), _baseID);
                    if (ms != null)
                    {
                        string sFloor = db.ExecuteScalar<string>("select DSP_MEMBER from VALUE_CODE where VALUE_MEMBER = @0", ms.FLOOR_ID);
                        string sArea = db.ExecuteScalar<string>("select DSP_MEMBER from VALUE_CODE where VALUE_MEMBER = @0", ms.AREA_ID);
                        //patientReg.DIALYSIS_ID = sFloor + " " +  sArea + " " + ms.MACHINE_NO + "#";
                        DIALYSIS_IDTextEdit.EditValue = sFloor + " " + sArea + " " + ms.BED_NO + "#";
                    }
                }
                catch { }               

                pATIENTREGISTBindingSource.EndEdit();
            }
        }

        private void btnSave_Print_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!dxValidationProvider1.Validate())
                return;

            if (XtraMessageBox.Show("确定保存该挂号单?", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    pATIENTREGISTBindingSource.EndEdit();
                    pATIENTREGISTBindingSource.CurrencyManager.EndCurrentEdit();

                    patientReg.BASE_INFO_ID = _baseID;
                    patientReg.CREATEDATE = DateTime.Now;
                    
                    //db.OpenSharedConnection();
                    db.Insert(patientReg);
                    //db.CloseSharedConnection();

                    patientReg = new PATIENT_REGIST();
                    patientReg.OPERATOR = ClsFrmMng.WorkerID;
                    patientReg.STATUS = 0;
                    patientReg.BASE_INFO_ID = _baseID;
                    pATIENTREGISTBindingSource.DataSource = patientReg;

                    if (NewRegistEvt != null)
                    {
                        NewRegistEvt();
                    }
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
                }
            }
        }

        private void btnSave_Print_Close_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnSave_Print_ItemClick(null, null);
            this.Close();
        }

        private void btnPrintView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!dxValidationProvider1.Validate())
            {
                XtraMessageBox.Show("数据完整性校验失败. ", "错误提示", MessageBoxButtons.OK);
                return;
            }
            rptPatientRegist rpt = new rptPatientRegist(patientReg);
            DevExpress.XtraReports.UI.ReportPrintTool rptPrint = new DevExpress.XtraReports.UI.ReportPrintTool(rpt);
            rptPrint.ShowPreviewDialog();
        }
    }
}