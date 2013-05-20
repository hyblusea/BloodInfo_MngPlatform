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
    public partial class FrmNewBloodCleanupProcess : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _cleanupID;
        BLOODCLEANUP_PROCESS process = new  BLOODCLEANUP_PROCESS();

        public FrmNewBloodCleanupProcess(Int64 cleanup_id)
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            _cleanupID = cleanup_id;

            process.BLOODCLEANUP_ID = _cleanupID;
            process.OPERATOR = ClsFrmMng.WorkerID;
            bLOODCLEANUPPROCESSBindingSource.DataSource = process;

            BLOODCLEANUP_TEMP bt = db.SingleOrDefault<BLOODCLEANUP_TEMP>("where BLOOD_CLEANUP_ID = @0", _cleanupID);
            DEVICECOMMUNICATION_LOG log = db.SingleOrDefault<DEVICECOMMUNICATION_LOG>("where REMOTE_IP=@0 and rownum = 1 order by ID DESC", bt.SERIAL_PORT_NUM);

            if (log != null)
            {
                string sDataTmp = log.MSG.Substring(0, log.MSG.Length - 4);
                string sTmp = sDataTmp.Substring(log.MSG.LastIndexOf('F') + 1, 5);
                string sVp = sDataTmp.Substring(log.MSG.LastIndexOf('H') + 1, 5);
                string sBf = sDataTmp.Substring(log.MSG.LastIndexOf('D') + 1, 5);
                string sMaxBp = sDataTmp.Substring(log.MSG.LastIndexOf('N') + 1, 5);
                string sMinBp = sDataTmp.Substring(log.MSG.LastIndexOf('O') + 1, 5);
                string sPulse = sDataTmp.Substring(log.MSG.LastIndexOf('P') + 1, 5);
                string sTotalUFAmount = sDataTmp.Substring(log.MSG.LastIndexOf('B') + 1, 5);
                string sDC = sDataTmp.Substring(log.MSG.LastIndexOf('G') + 1, 5);
                string sTMP = sDataTmp.Substring(log.MSG.LastIndexOf('J') + 1, 5);

                process.ANA_TIME = log.RECEIVE_TIME;
                process.TEMP = decimal.Parse(sTmp);
                process.VENOUS_PRESSURE = decimal.Parse(sVp);
                process.BLOOD_FLOW = decimal.Parse(sBf);
                process.BP = decimal.Parse(sMaxBp).ToString() + "~" + decimal.Parse(sMinBp);
                process.P = decimal.Parse(sPulse);
                process.ULTRAFILTRATION = decimal.Parse(sTotalUFAmount);
                process.CONDUCTIVITY = sDC;
                process.ARTERIAL_PRESSURE = decimal.Parse(sTMP);
            }
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                bLOODCLEANUPPROCESSBindingSource.EndEdit();
                bLOODCLEANUPPROCESSBindingSource.CurrencyManager.EndCurrentEdit();
                try
                {
                    process.LOG_TIME = DateTime.Now;
                    db.Insert(process);

                    process = new  BLOODCLEANUP_PROCESS();
                    process.BLOODCLEANUP_ID = _cleanupID;
                    process.OPERATOR = ClsFrmMng.WorkerID;
                    bLOODCLEANUPPROCESSBindingSource.DataSource = process;

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