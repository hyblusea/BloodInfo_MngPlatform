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
    public partial class FrmNewBloodCleanBase : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _baseID;
        Int64 _regID;
        BLOODCLEANUP blookCaeanup = new  BLOODCLEANUP();

        public FrmNewBloodCleanBase(Int64 base_id, Int64 reg_id, Int64 machineCheckID, decimal pt_path_type)
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            _regID = reg_id;
            _baseID = base_id;

            blookCaeanup.REG_ID = _regID;
            blookCaeanup.BASE_INFO_ID = _baseID;
            blookCaeanup.OPERATOR = ClsFrmMng.WorkerID;
            blookCaeanup.ANA_DATE = DateTime.Now;

            // 查询该患者所签到的透析机机位, 透析机型号
            MACHINE_SCHEDULE ms = db.SingleOrDefault<MACHINE_SCHEDULE>(machineCheckID);
            string sFloor = db.ExecuteScalar<string>("select DSP_MEMBER from VALUE_CODE where VALUE_MEMBER = @0", ms.FLOOR_ID);
            string sArea = db.ExecuteScalar<string>("select DSP_MEMBER from VALUE_CODE where VALUE_MEMBER = @0", ms.AREA_ID);
            blookCaeanup.MACH_POS = sFloor + " " +  sArea + " " + ms.BED_NO + "#";

            MACHINE_INFO info = db.SingleOrDefault<MACHINE_INFO>(ms.MACHINE_INFO_ID);
            if (info != null && info.MODEL != null)
                blookCaeanup.MACH_TYP = info.MODEL.ToString();

            // 血管通路类型
            blookCaeanup.FISTULA_TYPE = pt_path_type;
            if (pt_path_type == 519 || pt_path_type == 520)
            {
                ItemForAPPLICATOR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ItemForAPPLICATOR_NUM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ItemForHEPARIN_CAP.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ItemForHEPARIN_CAP_NUM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                // 获取该患者最近一次, 当通路类型与目前匹配时使用的耗材型号与数据
                BLOODCLEANUP bc1 = db.SingleOrDefault<BLOODCLEANUP>("where FISTULA_TYPE = @0 and BASE_INFO_ID = @1 AND  rownum = 1 order by ID DESC", new object[] { pt_path_type, base_id });
                if (bc1 != null)
                {
                    blookCaeanup.FISTULA_NEEDLE = bc1.FISTULA_NEEDLE;
                    blookCaeanup.FISTULA_NEEDLE_NUM = bc1.FISTULA_NEEDLE_NUM;
                    blookCaeanup.FISTULA_CARE_PACKAGES = bc1.FISTULA_CARE_PACKAGES;
                    blookCaeanup.FISTULA_CARE_PACKAGES_NUM = bc1.FISTULA_CARE_PACKAGES_NUM; 
                }
            }
            else if (pt_path_type == 704 || pt_path_type == 705)
            {
                ItemForFISTULA_NEEDLE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ItemForFISTULA_NEEDLE_NUM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ItemForFISTULA_CARE_PACKAGES.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ItemForFISTULA_CARE_PACKAGES_NUM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                // 获取该患者最近一次, 当通路类型与目前匹配时使用的耗材型号与数据
                BLOODCLEANUP bc1 = db.SingleOrDefault<BLOODCLEANUP>("where FISTULA_TYPE = @0 and BASE_INFO_ID = @1 AND  rownum = 1 order by ID DESC", new object[] { pt_path_type, base_id });
                if (bc1 != null)
                {
                    blookCaeanup.APPLICATOR = bc1.APPLICATOR;
                    blookCaeanup.APPLICATOR_NUM = bc1.APPLICATOR_NUM;
                    blookCaeanup.HEPARIN_CAP = bc1.HEPARIN_CAP;
                    blookCaeanup.HEPARIN_CAP_NUM = bc1.HEPARIN_CAP_NUM;  
                }
            }

            // 查询该患者上一次血液净信息
            BLOODCLEANUP bc = db.SingleOrDefault<BLOODCLEANUP>("where BASE_INFO_ID = @0 AND  rownum = 1 order by ID DESC", base_id);
            if (bc != null)
            {
                blookCaeanup.WEIGHT = bc.WEIGHT;
                blookCaeanup.MACH = bc.MACH;                        // 透析器
                blookCaeanup.PIPELINE = bc.PIPELINE;                // 管路
            }

            // 查询该患者医嘱(长期及临时医嘱)中的药品
            List<DOC_ADVICE> lstAdv = db.Fetch<DOC_ADVICE>("where BASE_INFO_ID = @0 and (ADVICE_TYPE = 0 or ADVICE_TYPE = 9)  AND IS_DEL = 0 order by log_time desc", base_id);
            string sDose = string.Empty;
            for (int i = 0; i < lstAdv.Count; i++)
            {
                sDose += db.ExecuteScalar<string>("select DSP_MEMBER from VALUE_CODE where VALUE_MEMBER = @0", lstAdv[i].M_NAME) +", ";
            }

            lstAdv = db.Fetch<DOC_ADVICE>("where reg_id = @0 and ADVICE_TYPE = 1 AND IS_DEL = 0 order by log_time desc", reg_id);
            for (int i = 0; i < lstAdv.Count; i++)
            {
                sDose += db.ExecuteScalar<string>("select DSP_MEMBER from VALUE_CODE where VALUE_MEMBER = @0", lstAdv[i].M_NAME) + ", ";
            }
            sDose = sDose.TrimEnd(new char[] { ',', ' ' });
            blookCaeanup.EPO = sDose;

            bLOODCLEANUPBindingSource.DataSource = blookCaeanup;

            //EPOTextEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            EPOTextEdit.Properties.DataSource = db.Fetch<VALUE_CODE>("");
            EPOTextEdit.Properties.DisplayMember = "DSP_MEMBER";
            EPOTextEdit.Properties.ValueMember = "VALUE_MEMBER";

            MACH_TYPTextEdit.Properties.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0 or GROUPNAME = @1", new object[] { 3, 162 });
            MACH_TYPTextEdit.Properties.DisplayMember = "DSP_MEMBER";
            MACH_TYPTextEdit.Properties.ValueMember = "VALUE_MEMBER";

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 101);
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 44);
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 102);
            vALUECODEBindingSource3.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 161);
            aCCOUNTBindingSource.DataSource = db.Fetch<ACCOUNT>("");

            bindingSource1.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 193);              // 通路类型
            bindingSource2.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 47);               // 管路
            bindingSource3.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 43);               // 穿刺针
            bindingSource4.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 171);              // 敷贴
            bindingSource5.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 166);              // 护理包
            bindingSource6.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 169);              // 肝素帽
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                bLOODCLEANUPBindingSource.EndEdit();
                bLOODCLEANUPBindingSource.CurrencyManager.EndCurrentEdit();
                try
                {
                    blookCaeanup.LOG_TIME = DateTime.Now;
                    db.Insert(blookCaeanup);

                    blookCaeanup = new  BLOODCLEANUP();
                    blookCaeanup.REG_ID = _regID;
                    blookCaeanup.BASE_INFO_ID = _baseID;
                    blookCaeanup.OPERATOR = ClsFrmMng.WorkerID;
                    bLOODCLEANUPBindingSource.DataSource = blookCaeanup;

                    if (NewRegistEvt != null)
                        NewRegistEvt();
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
                }
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

        private void ANA_END_TIMEDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            ANA_TIMETextEdit_Enter(null, null);
        }

        private void ANA_TIMETextEdit_Enter(object sender, EventArgs e)
        {
            try
            {
                double h = ((DateTime)ANA_END_TIMEDateEdit.EditValue - (DateTime)ANA_STAR_TIMEDateEdit.EditValue).TotalHours;
                ANA_TIMETextEdit.EditValue = Math.Round(h, 2);
                ((BLOODCLEANUP)bLOODCLEANUPBindingSource.Current).ANA_TIME = (decimal) Math.Round(h, 2); 
            }
            catch (Exception)
            {
            }            
        }

        private void ANA_WEIGHTTextEdit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            getFixWeight();
        }

        private void WEIGHTSpinEdit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            getFixWeight();
        }

        private void getFixWeight()
        {
            if (WEIGHTSpinEdit.EditValue != null && ANA_WEIGHTTextEdit.EditValue != null)
            {
                double w1 = 0;
                double w2 = 0;
                try
                {
                    w1 = Convert.ToDouble(ANA_WEIGHTTextEdit.EditValue);
                    w2 = Convert.ToDouble(WEIGHTSpinEdit.EditValue);
                    FIX_CAPACITYSpinEdit.EditValue = w1 - w2;
                    ((BLOODCLEANUP)bLOODCLEANUPBindingSource.Current).FIX_CAPACITY = (decimal)(w1 - w2);
                    //bLOODCLEANUPBindingSource.ResetCurrentItem();
                }
                catch { }
            }
        }

        private void WEIGHTSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
           getFixWeight();
        }

        private void ANA_WEIGHTTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            getFixWeight();
        }

        private void ANA_STAR_TIMEDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            ANA_TIMETextEdit_Enter(null, null);
        }

        private void EPOTextEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                _FrmDrugSelect frm = new _FrmDrugSelect(true);
                frm.SetSelectedValues(blookCaeanup.EPO);

                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (frm.LstDrugID != null && frm.LstDrugID.Count > 0)
                    {
                        string sTemp = "";
                        for (int i = 0; i < frm.LstDrugID.Count; i++)
                        {
                            sTemp += frm.LstDrugID[i].ToString() + ",";
                        }
                        sTemp.TrimEnd(',');
                        ((BLOODCLEANUP)bLOODCLEANUPBindingSource.Current).EPO = sTemp;
                        bLOODCLEANUPBindingSource.ResetCurrentItem();
                    }
                }
            }
        }

        private void EPOTextEdit_DoubleClick(object sender, EventArgs e)
        {
            EPOTextEdit_ButtonClick(null, new DevExpress.XtraEditors.Controls.ButtonPressedEventArgs(new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)));
        }

        
    }
}