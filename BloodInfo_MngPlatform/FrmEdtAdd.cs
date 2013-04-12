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
    public partial class FrmEdtAdd : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        Database db;

        public ADDTION_CHECK_HISTORY addHis = new  ADDTION_CHECK_HISTORY();
        Int64 _id;

        public FrmEdtAdd(Int64 id)
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
            _id = id;

            addHis = db.Single<ADDTION_CHECK_HISTORY>("where ID = @0", _id);
            aDDTIONCHECKHISTORYBindingSource.DataSource = addHis;
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!dxValidationProvider1.Validate())
            //    return;
            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                aDDTIONCHECKHISTORYBindingSource.EndEdit();
                aDDTIONCHECKHISTORYBindingSource.CurrencyManager.EndCurrentEdit();

                try
                {
                    //db.OpenSharedConnection();
                    addHis.Update();
                    //db.CloseSharedConnection();
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