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
    public partial class FrmBloodCleanup : DevExpress.XtraEditors.XtraForm
    {
        Database db;
        List<PATIENT_REGIST> reg = new List<PATIENT_REGIST>();
        List<BLOODCLEANUP> lstBloodCleanup = new List<BLOODCLEANUP>();
        List<BLOODCLEANUP_PROCESS> lstProcess = new List<BLOODCLEANUP_PROCESS>();

        Int64 Reg_ID = 0;
        Int64 cleanup_ID = 0;

        public FrmBloodCleanup()
        {
            InitializeComponent();

            ////string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
        }

        private void FrmBloodCleanup_Load(object sender, EventArgs e)
        {

            bLOODCLEANUPBindingSource.CurrentItemChanged += bLOODCLEANUPBindingSource_CurrentItemChanged;
            pATIENT_MachineScheduleBindingSource.CurrentItemChanged += pATIENT_MachineScheduleBindingSource_CurrentItemChanged;
            //btnSearch_ItemClick(null, null);

            // Form按钮使用设置
            List<ATH_CONTROL_ENABLE> lstFrmCtrl = ClsFrmMng.lstCtrlEnable.Where(c => c.FATHERITEM == this.Name).ToList<ATH_CONTROL_ENABLE>();
            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            for (int i = 0; i < lstFrmCtrl.Count(); i++)
            {
                dic.Add(lstFrmCtrl[i].CONTROL_NAME, Convert.ToBoolean(lstFrmCtrl[i].ENABLE));
            }
            AuthrzForDevDx.ScanFormControl ctrlEnable = new AuthrzForDevDx.ScanFormControl();
            ctrlEnable.SetBtn(this.components.Components, dic);

            repositoryItemGridLookUpEdit2.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 1 });
            repositoryItemGridLookUpEdit2.DisplayMember = "DSP_MEMBER";
            repositoryItemGridLookUpEdit2.ValueMember = "VALUE_MEMBER";

            repositoryItemGridLookUpEdit3.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 2 });
            repositoryItemGridLookUpEdit3.DisplayMember = "DSP_MEMBER";
            repositoryItemGridLookUpEdit3.ValueMember = "VALUE_MEMBER";

            repositoryItemGridLookUpEdit4.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 4 });
            repositoryItemGridLookUpEdit4.DisplayMember = "DSP_MEMBER";
            repositoryItemGridLookUpEdit4.ValueMember = "VALUE_MEMBER";

            //vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 18 });        

            repositoryItemLookUpEdit7.DataSource = ClsFrmMng.lstHaveOrNull;
            repositoryItemLookUpEdit7.DisplayMember = "MEMO";
            repositoryItemLookUpEdit7.ValueMember = "ID";

            docAdvTypeBindingSource.DataSource = ClsFrmMng.lstDocDavType;

            vALUECODEBindingSource3.DataSource = db.Fetch<VALUE_CODE>("");
            aCCOUNTBindingSource.DataSource = db.Fetch<ACCOUNT>("");

            pATIENTBASEINFOBindingSource.DataSource = db.Fetch<PATIENT_BASEINFO>("");

            // 楼层值集
            bdsValueCode.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 21);

            rdoPer.EditValue = 0;
            btnTime.EditValue = DateTime.Now;
        }

        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            if (barEditItem1.EditValue != null)
            {
                List<MACHINE_LAYOUT> area = db.Fetch<MACHINE_LAYOUT>("where FloorId = @0", barEditItem1.EditValue);
                if (area == null || area.Count == 0)
                    bdsValueCode_Area.DataSource = null;
                else
                {
                    List<VALUE_CODE> lst = new List<VALUE_CODE>();
                    for (int i = 0; i < area.Count; i++)
                    {
                        lst.Add(db.SingleOrDefault<VALUE_CODE>(area[i].AREAID));
                    }
                    bdsValueCode_Area.DataSource = lst;
                }
            }
            else
                bdsValueCode_Area.DataSource = null;
            bdsValueCode_Area.ResetBindings(false);
        }

        void bLOODCLEANUPBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (bLOODCLEANUPBindingSource.Current != null)
            {
                cleanup_ID = (Int64)((BLOODCLEANUP)bLOODCLEANUPBindingSource.Current).ID;

                // 血液净化过程明细
                lstProcess = db.Fetch<BLOODCLEANUP_PROCESS>(string.Format("where BLOODCLEANUP_ID ={0} order by log_time desc", cleanup_ID));
                bLOODCLEANUPPROCESSBindingSource.DataSource = lstProcess;
            }
            else
            {
                lstProcess = null;
                bLOODCLEANUPPROCESSBindingSource.DataSource = null;
            }
        }

        void pATIENT_MachineScheduleBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (pATIENT_MachineScheduleBindingSource.Current != null)
            {
                MACHINE_SCHEDULE p1 = ((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current);
                PATIENT_REGIST p = db.FirstOrDefault<PATIENT_REGIST>("where ID = @0", new object[] { p1.REG_ID });
                if (p != null)
                {
                    string sPatientName = p.NAME;
                    lblName1.Caption = sPatientName;
                    lblName2.Caption = sPatientName;
                    lblName3.Caption = sPatientName;

                    Reg_ID = (Int64)p.ID;

                    // 血液净化记录
                    lstBloodCleanup = db.Fetch<BLOODCLEANUP>(string.Format("where reg_id ={0} order by log_time desc", p.ID));
                    bLOODCLEANUPBindingSource.DataSource = lstBloodCleanup;
                }
                else
                {
                    bLOODCLEANUPBindingSource.DataSource = null;
                }
            }
            else
                bLOODCLEANUPBindingSource.DataSource = null;
        }

        private void btnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barEditItem1.EditValue == null || barEditItem2.EditValue == null)
            {
                XtraMessageBox.Show("请指定明确的楼层与区域.", "错误提示");
                return;
            }

            List<MACHINE_SCHEDULE> lstMc1 = db.Fetch<MACHINE_SCHEDULE>("where SCHEDULE_TIME = @0 and SCHEEDULE_PERIOD = @1 and FLOOR_ID = @2 and AREA_ID = @3",
                new object[] { ((DateTime)btnTime.EditValue).Date, rdoPer.EditValue, barEditItem1.EditValue, barEditItem2.EditValue });
            pATIENT_MachineScheduleBindingSource.DataSource = lstMc1;
        }

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENT_MachineScheduleBindingSource.Current == null)
                return;

            var v = db.Fetch<VASCULARPATH_HISTORY>("where BASE_INFO_ID = @0 AND  rownum = 1 order by ID desc", ((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current).PT_ID);
            if (v == null || v.Count() == 0 || v[0].FISTULA == null || v[0].FISTULA == 0)
            {
                XtraMessageBox.Show("未找到患者基本病史中的血管通路类型, 请确认.", "错误提示", MessageBoxButtons.OK);
                return;
            }

            pATIENT_MachineScheduleBindingSource_CurrentItemChanged(null, null);
            if (lstBloodCleanup.Count() > 0)
                XtraMessageBox.Show("该患者的本次挂号,已经存在血液净化记录,不允许重复新增.", "错误提示", MessageBoxButtons.OK);
            else
            {
                if (((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current).REG_ID == null || ((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current).REG_ID == 0)
                {
                    XtraMessageBox.Show("患者还没有签到.");
                    return;
                }
                FrmNewBloodCleanBase frmNewbloodBase = new FrmNewBloodCleanBase((Int64)((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current).PT_ID, (Int64)((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current).REG_ID, (Int64)((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current).ID, Convert.ToDecimal(v[0].FISTULA));
                frmNewbloodBase.NewRegistEvt += frmNewbloodBase_NewRegistEvt;
                frmNewbloodBase.ShowDialog();
            }
        }

        void frmNewbloodBase_NewRegistEvt()
        {
            lstBloodCleanup = db.Fetch<BLOODCLEANUP>(string.Format("where reg_id ={0} order by log_time desc", Reg_ID));
            bLOODCLEANUPBindingSource.DataSource = lstBloodCleanup;
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bLOODCLEANUPBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的行。", "错误提示", MessageBoxButtons.OK);
                return;
            }
            Int64 id = (Int64)((BLOODCLEANUP)bLOODCLEANUPBindingSource.Current).ID;
            FrmEdtBloodCeanupBase frmEdtBloodBase = new FrmEdtBloodCeanupBase(id);
            frmEdtBloodBase.NewRegistEvt += frmEdtBloodBase_NewRegistEvt;
            frmEdtBloodBase.ShowDialog();
        }

        void frmEdtBloodBase_NewRegistEvt()
        {
            lstBloodCleanup = db.Fetch<BLOODCLEANUP>(string.Format("where reg_id ={0} order by log_time desc", Reg_ID));
            bLOODCLEANUPBindingSource.DataSource = lstBloodCleanup;
        }

        private void btnEnd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENT_MachineScheduleBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要结束治疗的患者记住.");
                return;
            }

            if (XtraMessageBox.Show("确定结束该患者的本次医疗？", "操作确认", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    using (var scop = db.GetTransaction())
                    {
                        // 修改患者挂号登记状态为2
                        db.Execute("update PATIENT_REGIST set STATUS = 2 where ID = @0", ((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current).REG_ID);

                        // 备份该患者的透析机预约信息
                        Int64 scd = (Int64)((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current).ID;                     // 机器预约信息
                        var v = db.Single<MACHINE_SCHEDULE>(scd);
                        MACHINE_SCHEDULE_HI his = new MACHINE_SCHEDULE_HI();
                        MACHINE_INFO m_info = db.Single<MACHINE_INFO>(v.MACHINE_INFO_ID);                                           // 机器信息

                        his.AREA_ID = db.ExecuteScalar<string>("select DSP_MEMBER from VALUE_CODE where VALUE_MEMBER = @0", v.AREA_ID);
                        his.BED_NO = v.BED_NO;
                        his.CHCKINTM = v.CHCKINTM;
                        his.FLOOR_ID = db.ExecuteScalar<string>("select DSP_MEMBER from VALUE_CODE where VALUE_MEMBER = @0", v.FLOOR_ID);
                        his.M_TYPE = db.ExecuteScalar<string>("select groupName from VALUE_GROUP where ID = @0", m_info.MACHINETYPE);
                        his.MACHINE_MODEL = db.ExecuteScalar<string>("select DSP_MEMBER from VALUE_CODE where VALUE_MEMBER = @0", m_info.MODEL);
                        his.PT_ID = v.PT_ID;
                        his.REG_ID = v.REG_ID;
                        his.RESERVATION = v.RESERVATION;
                        his.SCHEDULE_TIME = v.SCHEDULE_TIME;
                        his.SCHEEDULE_PERIOD = his.SCHEEDULE_PERIOD;

                        // 写入到透析机备份表
                        db.Insert(his);

                        // 清空机器预约信息
                        db.Delete(v);

                        scop.Complete();
                    }
                    btnSearch_ItemClick(null, null);
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
                }
            }
        }

        private void btnNewDetail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bLOODCLEANUPBindingSource.Current == null)
            {
                XtraMessageBox.Show("请先创建血液净化基本信息.", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FrmNewBloodCleanupProcess frmNewprocess = new FrmNewBloodCleanupProcess((Int64)((BLOODCLEANUP)bLOODCLEANUPBindingSource.Current).ID);
            frmNewprocess.NewRegistEvt += frmNewprocess_NewRegistEvt;
            frmNewprocess.ShowDialog();
        }

        void frmNewprocess_NewRegistEvt()
        {
            lstProcess = db.Fetch<BLOODCLEANUP_PROCESS>(string.Format("where BLOODCLEANUP_ID ={0} order by log_time desc", cleanup_ID));
            bLOODCLEANUPPROCESSBindingSource.DataSource = lstProcess;
        }

        private void btnEdtDetail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bLOODCLEANUPPROCESSBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的行。", "错误提示", MessageBoxButtons.OK);
                return;
            }
            Int64 id = (Int64)((BLOODCLEANUP_PROCESS)bLOODCLEANUPPROCESSBindingSource.Current).ID;
            FrmEdtBloodCleanupProcess frmEdtProcess = new FrmEdtBloodCleanupProcess(id);
            frmEdtProcess.NewRegistEvt += frmEdtProcess_NewRegistEvt;
            frmEdtProcess.ShowDialog();
        }

        void frmEdtProcess_NewRegistEvt()
        {
            lstProcess = db.Fetch<BLOODCLEANUP_PROCESS>(string.Format("where BLOODCLEANUP_ID ={0} order by log_time desc", cleanup_ID));
            bLOODCLEANUPPROCESSBindingSource.DataSource = lstProcess;
        }

        private void FrmBloodCleanup_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClsFrmMng.frmBloodCleanup = null;
        }

        private void btnEdtSummary_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bLOODCLEANUPBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要编辑的行。", "错误提示", MessageBoxButtons.OK);
                return;
            }
            Int64 id = (Int64)((BLOODCLEANUP)bLOODCLEANUPBindingSource.Current).ID;
            FrmEdtBloodCleanup_Summary frmEdtSummary = new FrmEdtBloodCleanup_Summary(id);
            frmEdtSummary.NewRegistEvt += frmEdtSummary_NewRegistEvt;
            frmEdtSummary.ShowDialog();
        }

        void frmEdtSummary_NewRegistEvt()
        {
            lstBloodCleanup = db.Fetch<BLOODCLEANUP>(string.Format("where reg_id ={0} order by log_time desc", Reg_ID));
            bLOODCLEANUPBindingSource.DataSource = lstBloodCleanup;
        }

        private void btnRpt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENT_MachineScheduleBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要生成报表的患者基本记录.");
                return;
            }
            if (lstBloodCleanup.Count() < 1)
            {
                XtraMessageBox.Show("未找到血液净化相关记录.");
                return;
            }

            PATIENT_REGIST p = db.FirstOrDefault<PATIENT_REGIST>("where id = @0", new object[] { ((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current).REG_ID });

            if (p != null)
            {
                Rpt_bloodCleanup rpt = new Rpt_bloodCleanup(p.NAME,
                    Convert.ToInt32(p.AGE),
                    p.SEX,
                    lstProcess, lstBloodCleanup[0],
                    db.Fetch<VALUE_CODE>(""));

                DevExpress.XtraReports.UI.ReportPrintTool rptPrint = new DevExpress.XtraReports.UI.ReportPrintTool(rpt);

                rptPrint.ShowPreview();
                rpt.SetDspMember();
            }
            else
            {
                XtraMessageBox.Show("未找到该患者的挂号登记信息.", "错误提示");
            }

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bLOODCLEANUPBindingSource.Current != null)
            {
                BLOODCLEANUP bc = (BLOODCLEANUP)bLOODCLEANUPBindingSource.Current;

                DEVICECOMMUNICATION_LOG log = db.Single<DEVICECOMMUNICATION_LOG>("where remote_ip=@0 and rownum=1 order by id desc ", "com3");
                if (log != null)
                {
                    string sTmp = log.MSG.Substring(log.MSG.LastIndexOf('F') + 1, 5);
                    string sVp = log.MSG.Substring(log.MSG.LastIndexOf('H') + 1, 5);
                    string sBf = log.MSG.Substring(log.MSG.LastIndexOf('D') + 1, 5);
                    string sMaxBp = log.MSG.Substring(log.MSG.LastIndexOf('N') + 1, 5);
                    string sMinBp = log.MSG.Substring(log.MSG.LastIndexOf('O') + 1, 5);
                    string sPulse = log.MSG.Substring(log.MSG.LastIndexOf('P') + 1, 5);

                    BLOODCLEANUP_PROCESS proc = new BLOODCLEANUP_PROCESS();
                    proc.ANA_TIME = log.RECEIVE_TIME;
                    proc.TEMP = decimal.Parse(sTmp);
                    proc.VENOUS_PRESSURE = decimal.Parse(sVp);
                    proc.BLOOD_FLOW = decimal.Parse(sBf);
                    proc.BP = decimal.Parse(sMaxBp).ToString() + "~" + decimal.Parse(sMinBp);
                    proc.P = decimal.Parse(sPulse);

                    proc.LOG_TIME = DateTime.Now;
                    proc.BLOODCLEANUP_ID = bc.ID;
                    proc.OPERATOR = ClsFrmMng.WorkerID;
                    db.Insert(proc);
                    frmNewprocess_NewRegistEvt();
                }
            }
        }






    }
}