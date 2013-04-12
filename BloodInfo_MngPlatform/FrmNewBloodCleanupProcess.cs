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
    public partial class FrmNewBloodCleanupProcess : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _cleanupID;
        BLOODCLEANUP_PROCESS process = new  BLOODCLEANUP_PROCESS();

        public FrmNewBloodCleanupProcess(Int64 cleanup_id)
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            _cleanupID = cleanup_id;

            process.BLOODCLEANUP_ID = _cleanupID;
            process.OPERATOR = ClsFrmMng.WorkerID;
            bLOODCLEANUPPROCESSBindingSource.DataSource = process;
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                bLOODCLEANUPPROCESSBindingSource.EndEdit();
                bLOODCLEANUPPROCESSBindingSource.CurrencyManager.EndCurrentEdit();
                try
                {
                    process.LOG_TIME = DateTime.Now;
                    db.Insert(process);

                    process = new  BLOODCLEANUP_PROCESS();
                    process.BLOODCLEANUP_ID = _cleanupID;
                    process.OPERATOR = ClsFrmMng.WorkerID;
                    bLOODCLEANUPPROCESSBindingSource.DataSource = process;

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
    }
}