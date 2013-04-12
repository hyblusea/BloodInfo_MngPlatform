using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BloodInfo_MngPlatform.Models;
using PetaPoco;

namespace BloodInfo_MngPlatform
{
    public partial class FrmMachineTypeMng : DevExpress.XtraEditors.XtraForm
    {
        Database db;
        string sModel = "%";

        public FrmMachineTypeMng()
        {
            InitializeComponent();
        }

        private void FrmMachineTypeMng_Load(object sender, EventArgs e)
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

            db = new Database("XE");
            vALUEGROUPBindingSource.DataSource = db.Fetch<VALUE_GROUP>("");
            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("");
            barButtonItem4_ItemClick(null, null);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            if(txtSearch.EditValue != null)
                sModel = txtSearch.EditValue.ToString();

            mACHINETYPEBindingSource.DataSource = db.Fetch<MACHINE_TYPE>("where MODEL LIKE @0 ORDER BY ID DESC", sModel);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmNewMachineType frm = new FrmNewMachineType();
            frm.NewRegistEvt += frm_NewRegistEvt;
            frm.ShowDialog();
        }

        void frm_NewRegistEvt()
        {
            barButtonItem4_ItemClick(null, null);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (mACHINETYPEBindingSource.Current != null)
            {
                FrmEdtMachineType frm = new FrmEdtMachineType((Int64)((MACHINE_TYPE)mACHINETYPEBindingSource.Current).ID);
                frm.NewRegistEvt += frm_NewRegistEvt;
                frm.ShowDialog();
            }
            else
                XtraMessageBox.Show("请选择需要编辑的记录.");
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (mACHINETYPEBindingSource.Current != null)
            {
                if (XtraMessageBox.Show("您确实要删除该记录吗? ", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.Yes)
                {
                    barButtonItem4_ItemClick(null, null);
                    db.Delete((MACHINE_TYPE)mACHINETYPEBindingSource.Current);
                }
            }
            else
                XtraMessageBox.Show("请选择需要编辑的记录.");
        }
    }
}