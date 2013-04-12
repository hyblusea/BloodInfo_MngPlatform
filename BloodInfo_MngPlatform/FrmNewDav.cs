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
    public partial class FrmNewDav : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _baseID;
        Int64 _regID;
        int _isTemp;

        DOC_ADVICE_DRUG docAdv = new DOC_ADVICE_DRUG();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="base_id"></param>
        /// <param name="reg_id"></param>
        /// <param name="isTemp">0:长期, 1:临时</param>
        public FrmNewDav(Int64 base_id, Int64 reg_id, int isTemp)
        {
            InitializeComponent();
            if (isTemp == 0)
                this.Text = "新建长期医嘱";
            else
                this.Text = "新建临时医嘱";

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            _regID = reg_id;
            _baseID = base_id;
            _isTemp = isTemp;

            docAdv.REG_ID = _regID;
            docAdv.BASE_INFO_ID = _baseID;
            docAdv.OPERATOR = ClsFrmMng.WorkerID;
            docAdv.ADVICE_TYPE = _isTemp;
            docAdv.IS_DEL = 0;
            dOCADVICEBindingSource.DataSource = docAdv;

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("");

            M_UNITLookUpEdit.Properties.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 14 });
            M_UNITLookUpEdit.Properties.DisplayMember = "DSP_MEMBER";
            M_UNITLookUpEdit.Properties.ValueMember = "VALUE_MEMBER";

            M_ACTIONLookUpEdit.Properties.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] { 13 });
            M_ACTIONLookUpEdit.Properties.DisplayMember = "DSP_MEMBER";
            M_ACTIONLookUpEdit.Properties.ValueMember = "VALUE_MEMBER";
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该患者基本信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                dOCADVICEBindingSource.EndEdit();
                dOCADVICEBindingSource.CurrencyManager.EndCurrentEdit();
                try
                {
                    docAdv.LOG_TIME = DateTime.Now;

                    if (_isTemp != 9)
                        db.Insert(docAdv);
                    else
                    {
                        DOC_ADVICE_DFT det = new DOC_ADVICE_DFT();
                        det.ADVICE_TYPE = 9;
                        det.LOG_TIME = DateTime.Now;
                        det.M_ACTION = docAdv.M_ACTION;
                        det.M_AMOUNT = docAdv.M_AMOUNT;
                        det.M_NAME = docAdv.M_NAME;
                        det.M_UNIT = docAdv.M_UNIT;
                        det.MEMO = docAdv.MEMO;
                        det.RATEOFDAY = docAdv.RATEOFDAY;
                        det.RATEOFTIME = docAdv.RATEOFTIME;
                        det.OPERATOR = ClsFrmMng.WorkerID;
                        det.IS_DEL = 0;
                        db.Insert("DOC_ADVICE_DFT", "ID", det);
                    }

                    docAdv = new DOC_ADVICE_DRUG();
                    docAdv.REG_ID = _regID;
                    docAdv.BASE_INFO_ID = _baseID;
                    docAdv.OPERATOR = ClsFrmMng.WorkerID;
                    docAdv.ADVICE_TYPE = _isTemp;
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