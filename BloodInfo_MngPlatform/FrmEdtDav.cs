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
    public partial class FrmEdtDav : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _id;
        DOC_ADVICE_DRUG docAdv = new DOC_ADVICE_DRUG();

        public FrmEdtDav(Int64 id)
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
            _id = id;

            docAdv = db.Single<DOC_ADVICE_DRUG>("where ID = @0", _id);
            dOCADVICEBindingSource.DataSource = docAdv;

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("");

            M_UNITLookUpEdit.Properties.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 14 });
            M_UNITLookUpEdit.Properties.DisplayMember = "DSP_MEMBER";
            M_UNITLookUpEdit.Properties.ValueMember = "VALUE_MEMBER";

            M_ACTIONLookUpEdit.Properties.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 13 });
            M_ACTIONLookUpEdit.Properties.DisplayMember = "DSP_MEMBER";
            M_ACTIONLookUpEdit.Properties.ValueMember = "VALUE_MEMBER";
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!dxValidationProvider1.Validate())
            //    return;
            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                dOCADVICEBindingSource.EndEdit();
                dOCADVICEBindingSource.CurrencyManager.EndCurrentEdit();

                try
                {
                    //db.OpenSharedConnection();
                    docAdv.Update();
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

        private void M_NAMELookUpEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                _FrmDrugSelect frm = new _FrmDrugSelect();
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (frm.LstDrugID != null && frm.LstDrugID.Count > 0)
                    {
                        ((DOC_ADVICE_DRUG)dOCADVICEBindingSource.Current).M_NAME = frm.LstDrugID[0];
                        dOCADVICEBindingSource.ResetCurrentItem();
                    }
                }
            }
        }

        private void M_NAMELookUpEdit_DoubleClick(object sender, EventArgs e)
        {
            M_NAMELookUpEdit_ButtonClick(null, new DevExpress.XtraEditors.Controls.ButtonPressedEventArgs(new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)));
        }

    }
}