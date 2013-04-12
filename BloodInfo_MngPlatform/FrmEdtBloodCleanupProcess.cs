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
    public partial class FrmEdtBloodCleanupProcess : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        Database db;

        BLOODCLEANUP_PROCESS process = new  BLOODCLEANUP_PROCESS();
        Int64 _id;


        public FrmEdtBloodCleanupProcess(Int64 id)
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
            _id = id;

            process = db.Single<BLOODCLEANUP_PROCESS>("where ID = @0", _id);
            bLOODCLEANUPPROCESSBindingSource.DataSource = process;
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!dxValidationProvider1.Validate())
            //    return;
            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                bLOODCLEANUPPROCESSBindingSource.EndEdit();
                bLOODCLEANUPPROCESSBindingSource.CurrencyManager.EndCurrentEdit();

                try
                {
                    process.Update();
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
                }
                if (NewRegistEvt != null)
                    NewRegistEvt();
                this.Close();
            }
        }

        private void barExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}