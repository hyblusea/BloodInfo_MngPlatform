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
    public partial class FrmNewPath : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _baseID;
        Int64 _regID;
        VASCULARPATH_HISTORY path = new VASCULARPATH_HISTORY();

        public FrmNewPath(Int64 base_id, Int64 reg_id)
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            _regID = reg_id;
            _baseID = base_id;

            path.REG_ID = _regID;
            path.BASE_INFO_ID = _baseID;
            path.OPERATOR = ClsFrmMng.WorkerID;
            vASCULARPATHHISTORYBindingSource.DataSource = path;

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupName = 193");
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where groupName = 194");
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where groupName = 246");
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                vASCULARPATHHISTORYBindingSource.EndEdit();
                vASCULARPATHHISTORYBindingSource.CurrencyManager.EndCurrentEdit();
                try
                {
                    path.LOG_TIME = DateTime.Now;
                    db.Insert(path);

                    path = new  VASCULARPATH_HISTORY();
                    path.REG_ID = _regID;
                    path.BASE_INFO_ID = _baseID;
                    path.OPERATOR = ClsFrmMng.WorkerID;
                    vASCULARPATHHISTORYBindingSource.DataSource = path;

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