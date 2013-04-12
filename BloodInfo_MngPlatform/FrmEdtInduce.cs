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
    public partial class FrmEdtInduce : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        Database db;

        public INDUCEMENT_HISTORY induceHis = new  INDUCEMENT_HISTORY();
        Int64 _id;

        public FrmEdtInduce(Int64 id)
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
            _id = id;

            induceHis = db.Single<INDUCEMENT_HISTORY>("select * from INDUCEMENT_HISTORY where ID = @0", _id);
            iNDUCEMENTHISTORYBindingSource.DataSource = induceHis;

            //INDUCEMENTTextEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            INDUCEMENTTextEdit.Properties.DataSource = ClsFrmMng.lstIndecumentCode;
            INDUCEMENTTextEdit.Properties.DisplayMember = "INDUCEMENT_MEMO";
            INDUCEMENTTextEdit.Properties.ValueMember = "INDUCEMENT_MEMO";
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!dxValidationProvider1.Validate())
            //    return;
            if (XtraMessageBox.Show("确定保存该患者基本信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                iNDUCEMENTHISTORYBindingSource.EndEdit();
                iNDUCEMENTHISTORYBindingSource.CurrencyManager.EndCurrentEdit();

                try
                {
                    //db.OpenSharedConnection();
                    induceHis.Update();
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