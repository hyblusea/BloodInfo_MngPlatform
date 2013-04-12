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
    public partial class FrmMachineScheduled : DevExpress.XtraEditors.XtraForm
    {
        Database db;
        List<PATIENT_REGIST> lstPatientReg;
        List<PATIENT_BASEINFO> lstPatientBaseInfo;

        // 查询条件
        DateTime dt = new DateTime();
        int iPeroid = 0;
        decimal iFloorId;
        decimal iAreaId;

        int iFrmType = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frmType">0:患者预约; 1:患者签到; 2:状态维护</param>
        public FrmMachineScheduled(int frmType = 0)
        {
            InitializeComponent();
            iFrmType = frmType;

            if (iFrmType == 0)
                btnCopy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        public FrmMachineScheduled()
        {
            InitializeComponent();
            iFrmType = 0;
        }

        private void FrmMachineScheduled_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (iFrmType == 0)
                ClsFrmMng.frmMachineScdu = null;
            else if (iFrmType == 1)
                ClsFrmMng.frmMachineCheckIn = null;
            else
                ClsFrmMng.frmMachineMaintence = null;
        }

        private void FrmMachineScheduled_Load(object sender, EventArgs e)
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

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupname = @0", new object[] { 21 });

            lstPatientReg = db.Fetch<PATIENT_REGIST>("where status < 3");
            lstPatientBaseInfo = db.Fetch<PATIENT_BASEINFO>("");
            
            if (iFrmType == 0 || iFrmType == 2)
                monthCalendar1.MinDate = monthCalendar1.TodayDate;
            else if (iFrmType == 1)
            {
                monthCalendar1.MinDate = monthCalendar1.TodayDate;
                monthCalendar1.MaxDate = monthCalendar1.TodayDate;
            }

            splitContainerControl1.Collapsed = true;
            splitContainerControl1.CollapsePanel = SplitCollapsePanel.Panel1;

            if (iFrmType == 0)
                this.Text = "患者预约";
            else if (iFrmType == 1)
                
                this.Text = "患者签到";
            else if (iFrmType == 2)
                this.Text = "透析机状态维护";
        }

        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            if (barEditItem1.EditValue != null)
            {
                List<MACHINE_LAYOUT> area = db.Fetch<MACHINE_LAYOUT>("where FloorId = @0", barEditItem1.EditValue);
                if (area == null || area.Count == 0)
                    vALUECODEBindingSource1.DataSource = null;
                else
                {
                    List<VALUE_CODE> lst = new List<VALUE_CODE>();
                    for (int i = 0; i < area.Count; i++)
                    {
                        lst.Add(db.SingleOrDefault<VALUE_CODE>(area[i].AREAID));
                    }
                    vALUECODEBindingSource1.DataSource = lst;
                }
            }
            else
                vALUECODEBindingSource1.DataSource = null;
            vALUECODEBindingSource1.ResetBindings(false);
        }

        private void btnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            if (barEditItem1.EditValue == null || barEditItem2.EditValue == null)
            {
                XtraMessageBox.Show("请指定明确的楼层与区域.", "错误提示");
                return;
            }

            splashScreenManager1.ShowWaitForm();

            if (tableLayoutPanel1 != null)
            {
                groupControl3.Controls.Remove(tableLayoutPanel1);
                tableLayoutPanel1.Dispose();
            }

            try
            {
                tableLayoutPanel1 = new TableLayoutPanel();
                tableLayoutPanel1.BackColor = Color.White;
                tableLayoutPanel1.Dock = DockStyle.Fill;
                tableLayoutPanel1.AutoScroll = true;
                tableLayoutPanel1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1, true, null);
                this.groupControl3.Controls.Add(tableLayoutPanel1);

                dt = monthCalendar1.SelectionStart;
                iPeroid = radioGroup1.SelectedIndex;
                iFloorId = Convert.ToDecimal(barEditItem1.EditValue);
                iAreaId = Convert.ToDecimal(barEditItem2.EditValue);

                List<MACHINE_SCHEDULE> lstMc1 = db.Fetch<MACHINE_SCHEDULE>("where SCHEDULE_TIME = @0 and SCHEEDULE_PERIOD = @1 and FLOOR_ID = @2 and AREA_ID = @3",
                    new object[] { monthCalendar1.SelectionStart, radioGroup1.SelectedIndex, barEditItem1.EditValue, barEditItem2.EditValue });

                List<MACHINE_INFO> lstInfo = db.Fetch<MACHINE_INFO>("where FLOOR_ID = @0 and AREA_ID = @1 order by BED_NO", new object[] { barEditItem1.EditValue, barEditItem2.EditValue });
                MACHINE_LAYOUT mL = db.SingleOrDefault<MACHINE_LAYOUT>("where floorid = @0 and areaid = @1", new object[] { barEditItem1.EditValue, barEditItem2.EditValue });

                int iRow = 0;                                                                                   // 行数
                int iCol = 0;                                                                                   // 列数
                int iCount = 0;                                                                                 // 透析机总数
                if (lstInfo != null && lstInfo.Count > 0 && mL != null)
                {
                    iRow = Convert.ToInt32(mL.ROOMROWS);
                    iCol = Convert.ToInt32(Math.Ceiling((decimal)lstInfo.Count / (decimal)mL.ROOMROWS));        // 向上取整
                    iCount = lstInfo.Count;

                    tableLayoutPanel1.Controls.Clear();
                    tableLayoutPanel1.Size = new System.Drawing.Size(0, 0);
                    tableLayoutPanel1.AutoSize = true;
                    tableLayoutPanel1.RowStyles.Clear();

                    tableLayoutPanel1.Controls.Clear();
                    tableLayoutPanel1.RowCount = iRow;
                    tableLayoutPanel1.ColumnCount = iCol;

                    tableLayoutPanel1.ColumnStyles.Clear();
                    for (int i = 0; i < iRow; i++)
                    {
                        RowStyle rs = new RowStyle();
                        rs.SizeType = SizeType.Percent;
                        tableLayoutPanel1.RowStyles.Add(rs);
                    }

                    for (int i = 0; i < iCol; i++)
                    {
                        ColumnStyle cs = new ColumnStyle();
                        cs.SizeType = SizeType.AutoSize;
                        tableLayoutPanel1.ColumnStyles.Add(cs);
                    }

                    int iCnt = 0;
                    for (int r = 0; r < iRow; r++)
                    {
                        for (int c = 0; c < iCol; c++)
                        {
                            if (iCnt < iCount)
                            {
                                // 生成控件
                                UcDialysis uc = new UcDialysis(lstPatientReg, lstPatientBaseInfo, dt, iFloorId, iAreaId, iPeroid, iFrmType);
                                uc.No = lstInfo[iCnt].BED_NO;
                                uc.Model = lstInfo[iCnt].MODEL;
                                uc.M_Type = lstInfo[iCnt].MACHINETYPE;
                                uc.SN = lstInfo[iCnt].SN;
                                uc.InfoID = lstInfo[iCnt].ID;

                                // 获取该设备的图片
                                MACHINE_TYPE mType = db.SingleOrDefault<MACHINE_TYPE>("where MODEL = @0 AND M_TYPE = @1", new object[] { lstInfo[iCnt].MODEL, lstInfo[iCnt].MACHINETYPE });
                                if (mType != null && mType.M_PICTURE != null && mType.M_PICTURE.Length > 0)
                                    uc.Pict = mType.M_PICTURE;

                                // 根据不同功能,控件按钮全能
                                switch (iFrmType)
                                {
                                    case 0:
                                        {
                                            uc.StatusOP = false;
                                            uc.CheckIn = false;
                                            uc.Maintence = false;
                                            uc.Idel = false;
                                            break;
                                        }
                                    case 1:
                                        {
                                            uc.StatusOP = false;
                                            uc.Maintence = false;
                                            uc.SearchPt = false;
                                            uc.Idel = false;
                                            break;
                                        }
                                    case 2:
                                        {
                                            uc.StatusOP = false;
                                            uc.SearchPt = false;
                                            uc.CheckIn = false;
                                            break;
                                        }
                                }

                                // 加载预约/签到/维护等信息
                                var v = lstMc1.Where(s => s.BED_NO == lstInfo[iCnt].BED_NO).FirstOrDefault<MACHINE_SCHEDULE>();
                                if (v != null)
                                {
                                    if (v.MACHINE_STATUS != null)
                                        uc.Status = (decimal?)v.MACHINE_STATUS;
                                    else
                                        uc.Status = 74;                                     // 空闲
                                    uc.PtNo = Convert.ToInt32(v.PT_ID);
                                    uc.ScheduleID = v.ID;
                                    uc.CheckInTime = v.CHCKINTM;
                                    uc.ReservTm = v.RESERVATION;
                                }
                                else
                                {
                                    uc.Status = 74;
                                    uc.PtNo = null;
                                }
                                try
                                {
                                    tableLayoutPanel1.Controls.Add(uc);
                                }
                                catch (Exception err)
                                {
                                    Console.WriteLine(err.Message);
                                }

                            }
                            iCnt++;
                        }
                    }
                }
                else
                    tableLayoutPanel1.Controls.Clear();
            }
            catch(Exception err)
            {
                splashScreenManager1.CloseWaitForm();
                MessageBox.Show("未知错误. 错误原因: " + err.Message, "错误提示",  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            splashScreenManager1.CloseWaitForm();           
        }

        private void btnCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmMachineSchedule_Copy copy = new FrmMachineSchedule_Copy();
            copy.ShowDialog();
        }

        private void btnReloadBaseInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lstPatientBaseInfo = db.Fetch<PATIENT_BASEINFO>("");
        }

        


    }
}