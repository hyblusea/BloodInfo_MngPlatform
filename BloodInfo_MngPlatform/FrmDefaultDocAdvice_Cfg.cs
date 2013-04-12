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
    public partial class FrmDefaultDocAdvice_Cfg : DevExpress.XtraEditors.XtraForm
    {
        Database db;
        public FrmDefaultDocAdvice_Cfg()
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
        }

        private void FrmDefaultDocAdvice_Cfg_Load(object sender, EventArgs e)
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

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 18 });
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 14 });
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 13 });
            docAdvTypeBindingSource.DataSource = ClsFrmMng.lstDocDavType;

            dOCADVICEBindingSource.DataSource = db.Fetch<DOC_ADVICE_DFT>("where IS_DEL = 0");
        }

        private void FrmDefaultDocAdvice_Cfg_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClsFrmMng.frmDftDocAdv = null;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmNewDav frmNewDav = new FrmNewDav(-1, -1, 9);
            frmNewDav.NewRegistEvt += frmNewDav_NewRegistEvt;
            frmNewDav.ShowDialog();
        }

        void frmNewDav_NewRegistEvt()
        {
            dOCADVICEBindingSource.DataSource = db.Fetch<DOC_ADVICE_DFT>("where IS_DEL = 0");
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dOCADVICEBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择需要删除的行.", "错误提示", MessageBoxButtons.OK);
                return;
            }
            if (XtraMessageBox.Show("确实要删除该医嘱信息吗?", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Int64 id = (Int64)((DOC_ADVICE_DFT)dOCADVICEBindingSource.Current).ID;
                db.Execute("update DOC_ADVICE_DFT set IS_DEL = 1, del_time = @0, del_oper = @1 where ID = @2", new object[] { DateTime.Now, ClsFrmMng.WorkerID, id });
                //db.Execute("update DOC_ADVICE set IS_DEL = 1 where ID = @0", new object[] { id });
                frmNewDav_NewRegistEvt();
            }
        }
    }
}