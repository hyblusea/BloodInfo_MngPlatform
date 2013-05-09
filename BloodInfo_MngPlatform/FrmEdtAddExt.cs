using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Configuration;
using PetaPoco;
using BloodInfo_MngPlatform.Models;

namespace BloodInfo_MngPlatform
{
    public partial class FrmEdtAddExt : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        Database db;

        public ADDTION_CHECK_HISTORY_EXT addHis = new ADDTION_CHECK_HISTORY_EXT();
        Int64 _id;

        public FrmEdtAddExt(Int64 id)
        {
            InitializeComponent();

            db = new Database("XE");
            _id = id;

            addHis = db.Single<ADDTION_CHECK_HISTORY_EXT>("where ID = @0", _id);
            aDDTIONCHECKHISTORYEXTBindingSource.DataSource = addHis;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                aDDTIONCHECKHISTORYEXTBindingSource.EndEdit();
                aDDTIONCHECKHISTORYEXTBindingSource.CurrencyManager.EndCurrentEdit();

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

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}
