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
    public partial class FrmNewAddExt : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _baseID;
        Int64 _regID;

        ADDTION_CHECK_HISTORY_EXT addHis = new ADDTION_CHECK_HISTORY_EXT();

        public FrmNewAddExt(Int64 base_id, Int64 reg_id)
        {
            InitializeComponent();

            db = new Database("XE");

            _regID = reg_id;
            _baseID = base_id;

            addHis.REG_ID = _regID;
            addHis.BASE_INFO_ID = _baseID;
            addHis.OPERATOR = ClsFrmMng.WorkerID;
            aDDTIONCHECKHISTORYEXTBindingSource.DataSource = addHis;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该患者基本信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                aDDTIONCHECKHISTORYEXTBindingSource.EndEdit();
                aDDTIONCHECKHISTORYEXTBindingSource.CurrencyManager.EndCurrentEdit();
                try
                {
                    addHis.LOG_TIME = DateTime.Now;
                    db.Insert(addHis);

                    addHis = new ADDTION_CHECK_HISTORY_EXT();
                    addHis.REG_ID = _regID;
                    addHis.BASE_INFO_ID = _baseID;
                    addHis.OPERATOR = ClsFrmMng.WorkerID;
                    aDDTIONCHECKHISTORYEXTBindingSource.DataSource = addHis;

                    if (NewRegistEvt != null)
                        NewRegistEvt();
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
                }
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barButtonItem1_ItemClick(null, null);
            this.Close();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}
