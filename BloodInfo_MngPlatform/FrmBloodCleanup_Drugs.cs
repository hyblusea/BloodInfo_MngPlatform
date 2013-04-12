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
    public partial class FrmBloodCleanup_Drugs : DevExpress.XtraEditors.XtraForm
    {
        Database db;

        Int64 Reg_ID = 0;

        public FrmBloodCleanup_Drugs()
        {
            InitializeComponent();

            ////string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
        }

        private void FrmBloodCleanup_Load(object sender, EventArgs e)
        {
            
            pATIENT_MachineScheduleBindingSource.CurrentItemChanged += pATIENT_MachineScheduleBindingSource_CurrentItemChanged;

            // Form按钮使用设置
            List<ATH_CONTROL_ENABLE> lstFrmCtrl = ClsFrmMng.lstCtrlEnable.Where(c => c.FATHERITEM == this.Name).ToList<ATH_CONTROL_ENABLE>();
            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            for (int i = 0; i < lstFrmCtrl.Count(); i++)
            {
                dic.Add(lstFrmCtrl[i].CONTROL_NAME, Convert.ToBoolean(lstFrmCtrl[i].ENABLE));
            }
            AuthrzForDevDx.ScanFormControl ctrlEnable = new AuthrzForDevDx.ScanFormControl();
            ctrlEnable.SetBtn(this.components.Components, dic);

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("");
            vALUEGROUPBindingSource.DataSource = db.Fetch<VALUE_GROUP>("");
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

        void pATIENT_MachineScheduleBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            refresh1();
            refresh2();
        }

        void refresh1()
        {
            if (pATIENT_MachineScheduleBindingSource.Current != null)
            {
                MACHINE_SCHEDULE p1 = ((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current);
                PATIENT_REGIST p = db.FirstOrDefault<PATIENT_REGIST>("where ID = @0", new object[] { p1.REG_ID });
                if (p != null)
                {
                    Reg_ID = (Int64)p.ID;

                    // 此次透气所用的药品
                    dOCADVICEBindingSource.DataSource = db.Fetch<DOC_ADVICE>(string.Format("where REG_ID ={0} order by ID desc", p.ID));
                }
                else
                {
                    dOCADVICEBindingSource.DataSource = null;
                }
            } 
            else
                dOCADVICEBindingSource.DataSource = null;
        }

        void refresh2()
        {
            if (pATIENT_MachineScheduleBindingSource.Current != null)
            {
                MACHINE_SCHEDULE p1 = ((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current);
                PATIENT_REGIST p = db.FirstOrDefault<PATIENT_REGIST>("where ID = @0", new object[] { p1.REG_ID });
                if (p != null)
                {
                    Reg_ID = (Int64)p.ID;

                    // 此次透析所用的耗材
                    cONSUMABLESREQUESTLSTUSELOGBindingSource.DataSource = db.Fetch<CONSUMABLES_REQUEST_LST_USELOG>("where REG_ID =@0 order by id desc", p.ID);
                }
                else
                {
                    cONSUMABLESREQUESTLSTUSELOGBindingSource.DataSource = null;
                }
            }
            else
                cONSUMABLESREQUESTLSTUSELOGBindingSource.DataSource = null;
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

        private void FrmBloodCleanup_Drugs_FormClosed(object sender, FormClosedEventArgs e)
        {
            //ClsFrmMng.frmBC_D = null;
        }

        // 新建药品
        private void btnNewDevTemp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENT_MachineScheduleBindingSource.CurrencyManager != null)
            {
                if (((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current).REG_ID == null || ((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current).REG_ID == 0)
                {
                    XtraMessageBox.Show("该患者还未签到, 请确认.");
                    return;
                }
                else
                {
                    FrmNewBC_Drugs frm = new FrmNewBC_Drugs((Int64)((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current).PT_ID, (Int64)((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current).REG_ID);
                    frm.NewRegistEvt += frm_NewRegistEvt;
                    frm.ShowDialog();
                }
            }
            else
                XtraMessageBox.Show("请需要需要录入药品信息的患者, 请确认.");
        }

        void frm_NewRegistEvt()
        {
            refresh1();
        }
        

        // 编辑药品 
        private void btnEdtAdvTemp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dOCADVICEBindingSource.Current != null)
            {
                FrmEdtBC_Drugs frm = new FrmEdtBC_Drugs((Int64)((DOC_ADVICE)dOCADVICEBindingSource.Current).ID);
                frm.NewRegistEvt += frm_NewRegistEvt;
                frm.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("请需要需要编辑的药品记录, 请确认.");
            }
        }

        private void btnDelTemp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dOCADVICEBindingSource.Current != null)
            {
                if (XtraMessageBox.Show("您确实要删除该信息吗?", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    db.Delete("DOC_ADVICE", "ID", null, ((DOC_ADVICE)dOCADVICEBindingSource.Current).ID);
                    refresh1();
                }
            }
        }

        // 新增耗材使用
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENT_MachineScheduleBindingSource.Current != null)
            {
                if (((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current).REG_ID == null || ((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current).REG_ID == 0)
                {
                    XtraMessageBox.Show("该患者还未签到, 请确认.");
                    return;
                }
                else
                {
                    FrmNewBC_Consumable frm1 = new FrmNewBC_Consumable((Int64)((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current).PT_ID, (Int64)((MACHINE_SCHEDULE)pATIENT_MachineScheduleBindingSource.Current).REG_ID);
                    frm1.NewRegistEvt += frm1_NewRegistEvt;
                    frm1.ShowDialog();
                }
            }
            else
                XtraMessageBox.Show("请需要需要录入耗材使用信息的患者, 请确认.");
        }

        void frm1_NewRegistEvt()
        {
            refresh2();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cONSUMABLESREQUESTLSTUSELOGBindingSource.Current != null)
            {
                if (((CONSUMABLES_REQUEST_LST_USELOG)cONSUMABLESREQUESTLSTUSELOGBindingSource.Current).OPERATOR != ClsFrmMng.WorkerID)
                {
                    XtraMessageBox.Show("对不起, 您无权对 [" + ClsFrmMng.WorkerID + "] 创建耗材进行删除, 请确认. ");
                    return;
                }

                if (XtraMessageBox.Show("您确实要删除该耗材吗? 删除之后该耗材使用量将被退回原申请单.", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.Yes)
                {
                    decimal? ID = ((CONSUMABLES_REQUEST_LST_USELOG)cONSUMABLESREQUESTLSTUSELOGBindingSource.Current).REQUEST_LIST_ID;
                    decimal? useCount = ((CONSUMABLES_REQUEST_LST_USELOG)cONSUMABLESREQUESTLSTUSELOGBindingSource.Current).USECOUNT;

                    try
                    {
                        using (var scop = db.GetTransaction())
                        {
                            db.Delete("CONSUMABLES_REQUEST_LST_USELOG", "ID", null, ((CONSUMABLES_REQUEST_LST_USELOG)cONSUMABLESREQUESTLSTUSELOGBindingSource.Current).ID);
                            db.Execute("update CONSUMABLES_REQUEST_LST set SURPLUS = SURPLUS + @0 where ID = @1", new object[] { useCount, ID });
                            
                            scop.Complete();
                        }
                        refresh2();
                    }
                    catch (Exception err)
                    {
                        XtraMessageBox.Show("耗材使用数回退过程中出现异常, 原因: " + err.Message);
                    }
                }
            }
            else
                XtraMessageBox.Show("请选择需要删除的耗材记录.");
        }

  
    }
}