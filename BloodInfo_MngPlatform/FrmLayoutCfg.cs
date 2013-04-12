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
    public partial class FrmLayoutCfg : DevExpress.XtraEditors.XtraForm
    {
        Database db;

        public FrmLayoutCfg()
        {
            InitializeComponent();

            db = new Database("XE");
        }

        private void FrmLayoutCfg_Load(object sender, EventArgs e)
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


            vALUEGROUPBindingSource.DataSource = db.Fetch<VALUE_GROUP>("");
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("");
            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupname = @0", new object[] { 21 });
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where groupname = @0", new object[] { 22 });

            mACHINELAYOUTBindingSource.CurrentItemChanged += mACHINELAYOUTBindingSource_CurrentItemChanged;
            mACHINELAYOUTBindingSource.DataSource = db.Fetch<MACHINE_LAYOUT>("order by ID desc");            
        }

        void mACHINELAYOUTBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (mACHINELAYOUTBindingSource.Current != null)
                mACHINEINFOBindingSource.DataSource = db.Fetch<MACHINE_INFO>("where FLOOR_ID = @0 and AREA_ID = @1", new object[] { ((MACHINE_LAYOUT)mACHINELAYOUTBindingSource.Current).FLOORID, ((MACHINE_LAYOUT)mACHINELAYOUTBindingSource.Current).AREAID });
            else
                mACHINEINFOBindingSource.DataSource = null;
        }

        private void FrmLayoutCfg_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClsFrmMng.frmLayoutCfg = null;
        }

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //dgv.EndUpdate();
            //mACHINELAYOUTBindingSource.EndEdit();
            //mACHINELAYOUTBindingSource.CurrencyManager.EndCurrentEdit();

            //mACHINELAYOUTBindingSource.AddNew();
            //((MACHINE_LAYOUT)mACHINELAYOUTBindingSource.Current).isNew = true;

            FrmNewLayout frmLayout = new FrmNewLayout();
            frmLayout.NewRegistEvt += frmLayout_NewRegistEvt;
            frmLayout.ShowDialog();
        }

        void frmLayout_NewRegistEvt()
        {
            mACHINELAYOUTBindingSource.DataSource = db.Fetch<MACHINE_LAYOUT>("order by ID desc");
        }

        private void btnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (mACHINELAYOUTBindingSource.Current != null)
            {
                if (MessageBox.Show("确实要删除该区域?, 以及该区域所附带的透析机信息吗? ", "操作确认", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        using (var scope = db.GetTransaction())
                        {
                            db.Delete((MACHINE_LAYOUT)mACHINELAYOUTBindingSource.Current);
                            db.Execute("DELETE MACHINE_INFO WHERE LAYOUT_ID = @0", ((MACHINE_LAYOUT)mACHINELAYOUTBindingSource.Current).ID);
                            scope.Complete();
                            btnRefresh_ItemClick(null, null);
                        }
                    }
                    catch (Exception err)
                    {
                        XtraMessageBox.Show(err.Message);
                    }
                }
            }
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //gridView1.CloseEditor();
            //mACHINELAYOUTBindingSource.EndEdit();
            //mACHINELAYOUTBindingSource.CurrencyManager.EndCurrentEdit();
            //dgv.EndUpdate();

            //List<MACHINE_LAYOUT> lst = (List<MACHINE_LAYOUT>)mACHINELAYOUTBindingSource.DataSource;
            //for (int i = 0; i < lst.Count; i++)
            //{
            //    try
            //    {
            //        if (lst[i].IsNew())
            //            lst[i].Insert();
            //        else
            //            lst[i].Update();
            //    }
            //    catch (Exception err)
            //    {
            //        XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
            //    }
            //}
            //btnRefresh_ItemClick(null, null);
        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mACHINELAYOUTBindingSource.DataSource = db.Fetch<MACHINE_LAYOUT>("order by ID desc");
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (mACHINELAYOUTBindingSource.Current != null)
            {
                FrmNewMachineInfo frm = new FrmNewMachineInfo((Int64)((MACHINE_LAYOUT)mACHINELAYOUTBindingSource.Current).ID);
                frm.NewRegistEvt += frm_NewRegistEvt;
                frm.ShowDialog();
            }
        }

        void frm_NewRegistEvt()
        {
            mACHINELAYOUTBindingSource_CurrentItemChanged(null, null);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (mACHINEINFOBindingSource.Current != null)
            {
                FrmEdtMachineInfo frm = new FrmEdtMachineInfo((Int64)((MACHINE_INFO)mACHINEINFOBindingSource.Current).ID);
                frm.NewRegistEvt += frm_NewRegistEvt;
                frm.ShowDialog();
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (mACHINEINFOBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要删除的记录.", "错误提示");
                return;
            }
            if (XtraMessageBox.Show("您确实要删除该记录吗?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.Yes)
            {
                db.Delete("MACHINE_INFO", "ID", null, (Int64)((MACHINE_INFO)mACHINEINFOBindingSource.Current).ID);
                frm_NewRegistEvt();
            }
        }
    }
}