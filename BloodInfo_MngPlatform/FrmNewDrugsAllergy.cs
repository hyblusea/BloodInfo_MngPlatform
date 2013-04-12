﻿using System;
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
    public partial class FrmNewDrugsAllergy : Form
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _baseID;
        Int64 _regID;

        DRUG_ALLERGY_HISTORY drugAllergyHis = new DRUG_ALLERGY_HISTORY();

        public FrmNewDrugsAllergy(Int64 base_id, Int64 reg_id)
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            _regID = reg_id;
            _baseID = base_id;

            drugAllergyHis.REG_ID = _regID;
            drugAllergyHis.BASE_INFO_ID = _baseID;
            drugAllergyHis.OPERATOR = ClsFrmMng.WorkerID;
            dRUGALLERGYHISTORYBindingSource.DataSource = drugAllergyHis;

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = 185");
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = 186");
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = 187");
            vALUECODEBindingSource3.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = 183");
            vALUECODEBindingSource4.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = 184");
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该患者基本信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                dRUGALLERGYHISTORYBindingSource.EndEdit();
                dRUGALLERGYHISTORYBindingSource.CurrencyManager.EndCurrentEdit();
                try
                {
                    drugAllergyHis.LOG_TIME = DateTime.Now;
                    db.Insert(drugAllergyHis);

                    drugAllergyHis = new DRUG_ALLERGY_HISTORY();
                    drugAllergyHis.REG_ID = _regID;
                    drugAllergyHis.BASE_INFO_ID = _baseID;
                    drugAllergyHis.OPERATOR = ClsFrmMng.WorkerID;
                    dRUGALLERGYHISTORYBindingSource.DataSource = drugAllergyHis;

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

        private void POSITIONTextEdit_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}