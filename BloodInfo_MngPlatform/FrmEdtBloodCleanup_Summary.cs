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
    public partial class FrmEdtBloodCleanup_Summary : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        Database db;

        public BLOODCLEANUP cleanup = new  BLOODCLEANUP();
        Int64 _id;

        public FrmEdtBloodCleanup_Summary(Int64 id)
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
            _id = id;

            cleanup = db.Single<BLOODCLEANUP>("where ID = @0", _id);
            bLOODCLEANUPBindingSource.DataSource = cleanup;

            CLEANUP_TYPETextEdit.Properties.DataSource = db.Fetch<VALUE_CODE>("select DSP_MEMBER, VALUE_MEMBER from VALUE_CODE where GROUPNAME = @0", new object[] { 1 });
            CLEANUP_TYPETextEdit.Properties.DisplayMember = "DSP_MEMBER";
            CLEANUP_TYPETextEdit.Properties.ValueMember = "VALUE_MEMBER";

            SYMPTOMTextEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            SYMPTOMTextEdit.Properties.DataSource = ClsFrmMng.lstHaveOrNull;
            SYMPTOMTextEdit.Properties.DisplayMember = "MEMO";
            SYMPTOMTextEdit.Properties.ValueMember = "ID";

            BLOOD_PASSTextEdit.Properties.DataSource = db.Fetch<VALUE_CODE>("select DSP_MEMBER, VALUE_MEMBER from VALUE_CODE where GROUPNAME = @0", new object[] { 2 });
            BLOOD_PASSTextEdit.Properties.DisplayMember = "DSP_MEMBER";
            BLOOD_PASSTextEdit.Properties.ValueMember = "VALUE_MEMBER";
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                bLOODCLEANUPBindingSource.EndEdit();
                bLOODCLEANUPBindingSource.CurrencyManager.EndCurrentEdit();

                try
                {
                    cleanup.LOG_TIME2 = DateTime.Now;
                    cleanup.OPERATOR2 = ClsFrmMng.WorkerID;
                    cleanup.Update();
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
