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
using DevExpress.XtraBars.Helpers;
using PetaPoco;
using BloodInfo_MngPlatform.Models;
using System.Configuration;
using System.Reflection;

namespace BloodInfo_MngPlatform
{
    public partial class FrmMain : XtraForm
    {
        XtraForm _frmUserLogin = null;
        string _userID = string.Empty;
        string _userName = string.Empty;
        public FrmMain(XtraForm frmUserLogin, string userID, string userName, int iGroupID)
        {
            InitializeComponent();
            //navBarControl1.AllowSelectedLink = true;
            //xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InActiveTabPageHeaderAndOnMouseHover;

            _frmUserLogin = frmUserLogin;
            _userID = userID;
            _userName = userName;
            ClsFrmMng.GroupID = iGroupID;
            ClsFrmMng.WorkerID = _userID;
            ClsFrmMng.WorkerName = _userName;

            SkinHelper.InitSkinPopupMenu(menuStyle);  
          
            // 获取值集代码
            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            PetaPoco.Database db = new Database("XE");
            ClsFrmMng.lstRegStatus = db.Fetch<REG_STATUS>("");
            ClsFrmMng.lstIndecumentCode = db.Fetch<INDUCEMENT_CODE>("");
            ClsFrmMng.lstDigesCode = db.Fetch<DIGESTIVESYS_CODE>("");
            ClsFrmMng.lstHaveOrNull = new List<HaveOrNull>();
            ClsFrmMng.lstHaveOrNull.Add(new HaveOrNull(0, "无"));
            ClsFrmMng.lstHaveOrNull.Add(new HaveOrNull(1, "有"));

            ClsFrmMng.lstDocDavType = new List<DocAdvType>();
            ClsFrmMng.lstDocDavType.Add(new DocAdvType(0, "长期医嘱")); 
            ClsFrmMng.lstDocDavType.Add(new DocAdvType(1, "临时医嘱")); 
            ClsFrmMng.lstDocDavType.Add(new DocAdvType(9, "默认医嘱")); 

            if (iGroupID != 1)
            {
                mitAuthrz.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.Shown += FrmMain_Shown;
            }

            ClientFormStateClass formStat = new ClientFormStateClass();
            formStat.RestorePanelsState(dockManager1);
        }

        void FrmMain_Shown(object sender, EventArgs e)
        {
            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            PetaPoco.Database db = new Database("XE");

            // 权限相关
            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            List<ATH_CONTROL_ENABLE> lstEnable = db.Fetch<ATH_CONTROL_ENABLE>("where GROUP_ID = @0 and CONTROL_TYPE = 1 and ENABLE = 0", new object[]{ClsFrmMng.GroupID});
            List<ATH_CONTROL_ENABLE> lstEnableNav = db.Fetch<ATH_CONTROL_ENABLE>("where GROUP_ID = @0 and CONTROL_TYPE = 2 and ENABLE = 0", new object[] { ClsFrmMng.GroupID });
            ClsFrmMng.lstCtrlEnable = db.Fetch<ATH_CONTROL_ENABLE>("where GROUP_ID = @0 and CONTROL_TYPE = 0 and ENABLE = 0", new object[] { ClsFrmMng.GroupID });

            // 设置菜单使能
            for (int i = 0; i < lstEnable.Count; i++)
            {
                dic.Add(lstEnable[i].CONTROL_NAME, Convert.ToBoolean(lstEnable[i].ENABLE));
            }
            AuthrzForDevDx.SetMainMenuEnable setMenu = new AuthrzForDevDx.SetMainMenuEnable();
            setMenu.SetMenuItem(mainMenu, dic);

            // 设置导航栏使能
            dic.Clear();
            for (int i = 0; i < lstEnableNav.Count; i++)
            {
                dic.Add(lstEnableNav[i].CONTROL_NAME, Convert.ToBoolean(lstEnableNav[i].ENABLE));
            }            
            setMenu.SetNav(navBarControl1, dic);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (XtraMessageBox.Show("您确实要退出吗？", "操作确认", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                if (e != null)
                    e.Cancel = true;
                else
                    return;
            }
            else
            {
                this.FormClosing -= FrmMain_FormClosing;

                Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                cfg.AppSettings.Settings["Skin"].Value = this.LookAndFeel.ActiveSkinName;
                cfg.AppSettings.Settings["UserID"].Value = ClsFrmMng.WorkerID;
                cfg.AppSettings.SectionInformation.ForceSave = true;
                cfg.Save(ConfigurationSaveMode.Modified);

                ClientFormStateClass formStat = new ClientFormStateClass();
                formStat.SavePanelsState(dockManager1);

                Application.Exit();
            }
        }

        private void barbtnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmMain_FormClosing(null, null);
        }


        private void barbtnLogout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("您确实要注销吗？", "操作确认", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                this.FormClosing -= FrmMain_FormClosing;
                Application.Restart();
            }

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            lblWorkID.Caption = "员工号： " + _userID;
            lblUserName.Caption = "欢迎您： " + _userName;
        }

        private void tmSysTime_Tick(object sender, EventArgs e)
        {
            lblTime.Caption = "系统时间： " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void btnPatientRegist_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            if (ClsFrmMng.frmPatientReg == null)
            {
                splashScreenManager1.ShowWaitForm();
                ClsFrmMng.frmPatientReg = new FrmPatientRigist();
                ClsFrmMng.frmPatientReg.MdiParent = this;
                ClsFrmMng.frmPatientReg.Show();
                xtraTabbedMdiManager1.Pages[ClsFrmMng.frmPatientReg].ImageIndex = 14;
                splashScreenManager1.CloseWaitForm();
            }
            else
                ClsFrmMng.frmPatientReg.Activate();
        }

        private void barbtnPatientBaseInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ClsFrmMng.frmPatientBaseInfo == null)
            {
                splashScreenManager1.ShowWaitForm();
                ClsFrmMng.frmPatientBaseInfo = new FrmPatientBaseInfo();
                ClsFrmMng.frmPatientBaseInfo.MdiParent = this;
                ClsFrmMng.frmPatientBaseInfo.Show();
                xtraTabbedMdiManager1.Pages[ClsFrmMng.frmPatientBaseInfo].ImageIndex = 17;
                splashScreenManager1.CloseWaitForm();
            }
            else
                ClsFrmMng.frmPatientBaseInfo.Activate();
        }

        private void barbutton1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void btnHelp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        // 护士护理记录
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        // 医生透晰记录
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ClsFrmMng.frmCaseHisLog == null)
            {
                splashScreenManager1.ShowWaitForm();
                ClsFrmMng.frmCaseHisLog = new FrmCaseHisLog();
                ClsFrmMng.frmCaseHisLog.MdiParent = this;
                ClsFrmMng.frmCaseHisLog.Show();
                xtraTabbedMdiManager1.Pages[ClsFrmMng.frmCaseHisLog].ImageIndex = 35;
                splashScreenManager1.CloseWaitForm();
            }
            else
                ClsFrmMng.frmCaseHisLog.Activate();
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        // 血液净化记录
        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ClsFrmMng.frmBloodCleanup == null)
            {
                splashScreenManager1.ShowWaitForm();
                ClsFrmMng.frmBloodCleanup = new  FrmBloodCleanup();
                ClsFrmMng.frmBloodCleanup.MdiParent = this;
                ClsFrmMng.frmBloodCleanup.Show();
                xtraTabbedMdiManager1.Pages[ClsFrmMng.frmBloodCleanup].ImageIndex = 50;
                splashScreenManager1.CloseWaitForm();
            }
            else
                ClsFrmMng.frmBloodCleanup.Activate();
        }

        // 评估
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ClsFrmMng.frmEvaluate == null)
            {
                splashScreenManager1.ShowWaitForm();
                ClsFrmMng.frmEvaluate = new FrmEvaluate();
                ClsFrmMng.frmEvaluate.MdiParent = this;
                ClsFrmMng.frmEvaluate.Show();
                xtraTabbedMdiManager1.Pages[ClsFrmMng.frmEvaluate].ImageIndex = 17;
                splashScreenManager1.CloseWaitForm();
            }
            else
                ClsFrmMng.frmEvaluate.Activate();
        }

        // 查询/报表 : 透析记录
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ClsFrmMng.frmRptPatientInfo == null)
            {
                splashScreenManager1.ShowWaitForm();
                ClsFrmMng.frmRptPatientInfo = new FrmRptPatientInfo();
                ClsFrmMng.frmRptPatientInfo.MdiParent = this;
                ClsFrmMng.frmRptPatientInfo.Show();
                xtraTabbedMdiManager1.Pages[ClsFrmMng.frmRptPatientInfo].ImageIndex = 1;
                splashScreenManager1.CloseWaitForm();
            }
            else
                ClsFrmMng.frmRptPatientInfo.Activate();
        }

        // 技术支持
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        // 扫描菜单项
        private void mbtnScanMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            PetaPoco.Database db = new Database("XE");

            AuthrzForDevDx.ScanMainMenu authScan = new AuthrzForDevDx.ScanMainMenu();

            // 扫描主菜单
            var o = authScan.getMenuItem(mainMenu);

            ATH_MAINMENU.Delete("");
            foreach (KeyValuePair<string, AuthrzForDevDx.ItemTree> item in o)
            {
                ATH_MAINMENU athMainMenu = new ATH_MAINMENU();
                athMainMenu.ITEMNAME = item.Key;
                athMainMenu.ITEMCAPTION = item.Value.ItemCaption;
                athMainMenu.FATHERITEM = item.Value.ItemFather;

                athMainMenu.Insert();
            }

            // 扫描NavBar
            var navItem = authScan.getNavItem(navBarControl1);
            ATH_NAVGATIONITEM.Delete("");
            foreach (KeyValuePair<string, AuthrzForDevDx.ItemTree> item in navItem)
            {
                ATH_NAVGATIONITEM athNavItem = new ATH_NAVGATIONITEM();
                athNavItem.ITEMNAME = item.Key;
                athNavItem.ITEMCAPTION = item.Value.ItemCaption;

                athNavItem.Insert();
            }

            splashScreenManager1.CloseWaitForm();
            XtraMessageBox.Show("所有菜单项已经扫描完毕.", "信息提示", MessageBoxButtons.OK);
        }

        // 扫描所有窗体按钮
        private void mbtnScanWindows_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();

            Type t = typeof(ClsFrmMng);
            FieldInfo[] fields = t.GetFields();

            ATH_FORMBUTTON.Delete("");
            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i].FieldType.BaseType.Name == "XtraForm")
                {
                    XtraForm o = AuthrzForDevDx.ReflectionHelper.CreateInstance<XtraForm>(fields[i].FieldType, null);
                    if (o != null)
                    {
                        AuthrzForDevDx.ScanFormControl authScan = new AuthrzForDevDx.ScanFormControl();
                        var dic = authScan.GetButton((Control)o);

                        foreach (KeyValuePair<string, AuthrzForDevDx.ItemTree> item in dic)
                        {
                            ATH_FORMBUTTON athMainMenu = new ATH_FORMBUTTON();
                            athMainMenu.ITEMNAME = item.Key;
                            athMainMenu.ITEMCAPTION = item.Value.ItemCaption;
                            athMainMenu.FATHERITEM = item.Value.ItemFather;

                            athMainMenu.Insert();
                        }
                        o.Dispose();
                    }
                }
            }
            splashScreenManager1.CloseWaitForm();
            XtraMessageBox.Show("所有窗体控件已经扫描完毕.", "信息提示", MessageBoxButtons.OK);
        }

        private void mbtnAuthMng_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmAuthMng frmAuthMng = new  FrmAuthMng();

            splashScreenManager1.ShowWaitForm();
            frmAuthMng.MdiParent = this;
            frmAuthMng.Show();
            xtraTabbedMdiManager1.Pages[frmAuthMng].ImageIndex = 105;
            splashScreenManager1.CloseWaitForm();
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _FrmAes frmDes = new _FrmAes();
            frmDes.MdiParent = this;
            frmDes.Show();
        }

        // 修改密码
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _FrmModifyPwd frmModifyPwd = new _FrmModifyPwd();
            frmModifyPwd.ShowDialog();
        }

        private void barbtnShowNavbar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dockPanel1.Show();
        }

        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            barbtnPatientBaseInfo_ItemClick(null, null);
        }

        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            btnPatientRegist_ItemClick(null, null); 
        }

        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            barButtonItem8_ItemClick(null, null);
        }

        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            barButtonItem17_ItemClick(null, null);
        }

        // 值集代码维护
        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ClsFrmMng.frmValCode == null)
            {
                splashScreenManager1.ShowWaitForm();
                ClsFrmMng.frmValCode = new  FrmValueCodeMng();
                ClsFrmMng.frmValCode.MdiParent = this;
                ClsFrmMng.frmValCode.Show();
                xtraTabbedMdiManager1.Pages[ClsFrmMng.frmValCode].ImageIndex = 105;
                splashScreenManager1.CloseWaitForm();
            }
            else
                ClsFrmMng.frmValCode.Activate();
        }

        // 耗材库存管理
        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ClsFrmMng.frmConsumWh == null)
            {
                splashScreenManager1.ShowWaitForm();
                ClsFrmMng.frmConsumWh = new  FrmConsumWH();
                ClsFrmMng.frmConsumWh.MdiParent = this;
                ClsFrmMng.frmConsumWh.Show();
                xtraTabbedMdiManager1.Pages[ClsFrmMng.frmConsumWh].ImageIndex = 94;
                splashScreenManager1.CloseWaitForm();
            }
            else
                ClsFrmMng.frmConsumWh.Activate();
        }

        // 透析机部局配置
        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ClsFrmMng.frmLayoutCfg == null)
            {
                splashScreenManager1.ShowWaitForm();
                ClsFrmMng.frmLayoutCfg = new  FrmLayoutCfg();
                ClsFrmMng.frmLayoutCfg.MdiParent = this;
                ClsFrmMng.frmLayoutCfg.Show();
                xtraTabbedMdiManager1.Pages[ClsFrmMng.frmLayoutCfg].ImageIndex = 23;
                splashScreenManager1.CloseWaitForm();
            }
            else
                ClsFrmMng.frmLayoutCfg.Activate();
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ClsFrmMng.frmMachineScdu == null)
            {
                splashScreenManager1.ShowWaitForm();
                ClsFrmMng.frmMachineScdu = new  FrmMachineScheduled();
                ClsFrmMng.frmMachineScdu.MdiParent = this;
                ClsFrmMng.frmMachineScdu.Show();
                xtraTabbedMdiManager1.Pages[ClsFrmMng.frmMachineScdu].ImageIndex = 77;
                splashScreenManager1.CloseWaitForm();
            }
            else
                ClsFrmMng.frmMachineScdu.Activate();
        }

        private void barButtonItem7_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ClsFrmMng.frmMachineCheckIn == null)
            {
                splashScreenManager1.ShowWaitForm();
                ClsFrmMng.frmMachineCheckIn = new  FrmMachineScheduled(1);
                ClsFrmMng.frmMachineCheckIn.MdiParent = this;
                ClsFrmMng.frmMachineCheckIn.Show();
                xtraTabbedMdiManager1.Pages[ClsFrmMng.frmMachineCheckIn].ImageIndex = 78;
                splashScreenManager1.CloseWaitForm();
            }
            else
                ClsFrmMng.frmMachineCheckIn.Activate();
        }

        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ClsFrmMng.frmMachineMaintence == null)
            {
                splashScreenManager1.ShowWaitForm();
                ClsFrmMng.frmMachineMaintence = new FrmMachineScheduled(2);
                ClsFrmMng.frmMachineMaintence.MdiParent = this;
                ClsFrmMng.frmMachineMaintence.Show();
                xtraTabbedMdiManager1.Pages[ClsFrmMng.frmMachineMaintence].ImageIndex = 56;
                splashScreenManager1.CloseWaitForm();
            }
            else
                ClsFrmMng.frmMachineMaintence.Activate();
        }

        private void barButtonItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Models.MANGED_ODP_TEST m = new MANGED_ODP_TEST();
            for (int i = 1; i < 100; i++)
            {
                m.ABC = Guid.NewGuid().ToString();
                Console.WriteLine(m.Insert().ToString());
            }
        }

        private void barButtonItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ClsFrmMng.frmDftDocAdv == null)
            {
                splashScreenManager1.ShowWaitForm();
                ClsFrmMng.frmDftDocAdv = new  FrmDefaultDocAdvice_Cfg();
                ClsFrmMng.frmDftDocAdv.MdiParent = this;
                ClsFrmMng.frmDftDocAdv.Show();
                xtraTabbedMdiManager1.Pages[ClsFrmMng.frmDftDocAdv].ImageIndex = 20;
                splashScreenManager1.CloseWaitForm();
            }
            else
                ClsFrmMng.frmDftDocAdv.Activate();
        }

        // 需求申请
        private void barButtonItem25_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ClsFrmMng.frmConsumWhReqst == null)
            {
                splashScreenManager1.ShowWaitForm();
                ClsFrmMng.frmConsumWhReqst = new FrmConsumWH_Request();
                ClsFrmMng.frmConsumWhReqst.MdiParent = this;
                ClsFrmMng.frmConsumWhReqst.Show();
                xtraTabbedMdiManager1.Pages[ClsFrmMng.frmConsumWhReqst].ImageIndex = 90;
                splashScreenManager1.CloseWaitForm();
            }
            else
                ClsFrmMng.frmConsumWhReqst.Activate();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _FrmCommTest frm = new  _FrmCommTest();
            frm.MdiParent = this;
            frm.Show();
        }

        // 患者诊断
        private void barButtonItem2_ItemClick_2(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ClsFrmMng.frmDiagnosis == null)
            {
                splashScreenManager1.ShowWaitForm();
                ClsFrmMng.frmDiagnosis = new  FrmDiagnosis();
                ClsFrmMng.frmDiagnosis.MdiParent = this;
                ClsFrmMng.frmDiagnosis.Show();
                xtraTabbedMdiManager1.Pages[ClsFrmMng.frmDiagnosis].ImageIndex = 108;
                splashScreenManager1.CloseWaitForm();
            }
            else
                ClsFrmMng.frmDiagnosis.Activate();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ClsFrmMng.frmMachineType == null)
            {
                splashScreenManager1.ShowWaitForm();
                ClsFrmMng.frmMachineType = new FrmMachineTypeMng();
                ClsFrmMng.frmMachineType.MdiParent = this;
                ClsFrmMng.frmMachineType.Show();
                xtraTabbedMdiManager1.Pages[ClsFrmMng.frmMachineType].ImageIndex = 109;
                splashScreenManager1.CloseWaitForm();
            }
            else
                ClsFrmMng.frmMachineType.Activate();
        }

        private void barButtonItem9_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ClsFrmMng.frmMachineType == null)
            {
                splashScreenManager1.ShowWaitForm();
                ClsFrmMng.frmDocAdvice = new FrmDocAdvice_Drugs();
                ClsFrmMng.frmDocAdvice.MdiParent = this;
                ClsFrmMng.frmDocAdvice.Show();
                xtraTabbedMdiManager1.Pages[ClsFrmMng.frmDocAdvice].ImageIndex = 111;
                splashScreenManager1.CloseWaitForm();
            }
            else
                ClsFrmMng.frmDocAdvice.Activate();
        }

        private void barButtonItem10_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ClsFrmMng.frmBC_D == null)
            {
                splashScreenManager1.ShowWaitForm();
                ClsFrmMng.frmBC_D = new FrmBloodCleanup_Drugs();
                ClsFrmMng.frmBC_D.MdiParent = this;
                ClsFrmMng.frmBC_D.Show();
                xtraTabbedMdiManager1.Pages[ClsFrmMng.frmBC_D].ImageIndex = 112;
                splashScreenManager1.CloseWaitForm();
            }
            else
                ClsFrmMng.frmBC_D.Activate();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            splashScreenManager1.ShowWaitForm();
            Frm frm = new Frm();
            frm.MdiParent = this;
            frm.Show();
            splashScreenManager1.CloseWaitForm();

        }

        private void barButtonItem12_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ClsFrmMng.frmCourse == null)
            {
                splashScreenManager1.ShowWaitForm();
                ClsFrmMng.frmCourse = new FrmPatient_Course();
                ClsFrmMng.frmCourse.MdiParent = this;
                ClsFrmMng.frmCourse.Show();
                xtraTabbedMdiManager1.Pages[ClsFrmMng.frmCourse].ImageIndex = 110;
                splashScreenManager1.CloseWaitForm();
            }
            else
                ClsFrmMng.frmCourse.Activate();
        }


        
    }
}