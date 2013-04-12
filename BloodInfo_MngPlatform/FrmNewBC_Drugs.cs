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
    public partial class FrmNewBC_Drugs : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _baseID;
        Int64 _regID;

        DOC_ADVICE docAdv = new DOC_ADVICE();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="base_id"></param>
        /// <param name="reg_id"></param>
        public FrmNewBC_Drugs(Int64 base_id, Int64 reg_id)
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            _regID = reg_id;
            _baseID = base_id;

            docAdv.REG_ID = _regID;
            docAdv.BASE_INFO_ID = _baseID;
            docAdv.OPERATOR = ClsFrmMng.WorkerID;
            docAdv.IS_DEL = 0;
            dOCADVICEBindingSource.DataSource = docAdv;

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 18 });

            M_UNITLookUpEdit.Properties.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 14 });
            M_UNITLookUpEdit.Properties.DisplayMember = "DSP_MEMBER";
            M_UNITLookUpEdit.Properties.ValueMember = "VALUE_MEMBER";

            M_ACTIONLookUpEdit.Properties.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 13 });
            M_ACTIONLookUpEdit.Properties.DisplayMember = "DSP_MEMBER";
            M_ACTIONLookUpEdit.Properties.ValueMember = "VALUE_MEMBER";
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                dOCADVICEBindingSource.EndEdit();
                dOCADVICEBindingSource.CurrencyManager.EndCurrentEdit();
                try
                {
                    docAdv.LOG_TIME = DateTime.Now;

                        db.Insert(docAdv);

                        docAdv = new DOC_ADVICE();
                    docAdv.REG_ID = _regID;
                    docAdv.BASE_INFO_ID = _baseID;
                    docAdv.OPERATOR = ClsFrmMng.WorkerID;
                    docAdv.IS_DEL = 0;
                    dOCADVICEBindingSource.DataSource = docAdv;

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