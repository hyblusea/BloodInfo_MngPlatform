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
    public partial class FrmEdtPath : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        Database db;

        public VASCULARPATH_HISTORY pathHis = new  VASCULARPATH_HISTORY();
        Int64 _id;

        public FrmEdtPath(Int64 id)
        {
            InitializeComponent();
            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
            _id = id;

            pathHis = db.Single<VASCULARPATH_HISTORY>("where ID = @0", _id);
            vASCULARPATHHISTORYBindingSource.DataSource = pathHis;

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupName = 193");
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where groupName = 194");
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!dxValidationProvider1.Validate())
            //    return;
            if (XtraMessageBox.Show("确定保存用药信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                vASCULARPATHHISTORYBindingSource.EndEdit();
                vASCULARPATHHISTORYBindingSource.CurrencyManager.EndCurrentEdit();

                try
                {
                    pathHis.Update();
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

        private void FISTULATextEdit_EditValueChanged(object sender, EventArgs e)
        {
            HideItem();
            if (FISTULATextEdit.EditValue != null && !string.IsNullOrEmpty(FISTULATextEdit.EditValue.ToString()))
            {
                switch (Convert.ToInt64(FISTULATextEdit.EditValue))
                {
                    case 519:
                    case 520:
                        layoutControlGroup5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        break;
                    case 704:
                    case 705:
                        layoutControlGroup7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        break;
                }
            }
        }

        private void HideItem()
        {
            layoutControlGroup5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

        }
    }
}