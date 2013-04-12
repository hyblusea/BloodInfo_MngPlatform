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
    public partial class FrmEdtDrugsAllergy : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        Database db;

        public DRUG_ALLERGY_HISTORY drugAllergyHis = new DRUG_ALLERGY_HISTORY();

        Int64 _id;

        public FrmEdtDrugsAllergy(Int64 id)
        {
            InitializeComponent();
            _id = id;

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
            _id = id;

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = 185");
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = 186");
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = 187");
            vALUECODEBindingSource3.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = 183");
            vALUECODEBindingSource4.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = 184");

            drugAllergyHis = db.Single<DRUG_ALLERGY_HISTORY>("select * from DRUG_ALLERGY_HISTORY where ID = @0", _id);
            dRUGALLERGYHISTORYBindingSource.DataSource = drugAllergyHis;
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该患者药物过敏信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                dRUGALLERGYHISTORYBindingSource.EndEdit();
                dRUGALLERGYHISTORYBindingSource.CurrencyManager.EndCurrentEdit();

                try
                {
                    //db.OpenSharedConnection();
                    drugAllergyHis.Update();
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