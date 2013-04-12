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
    public partial class FrmEdtBloodCeanupBase : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;
        Database db;

        BLOODCLEANUP bloodCleanup = new  BLOODCLEANUP();
        Int64 _id;

        public FrmEdtBloodCeanupBase(Int64 id)
        {
            InitializeComponent();

            ////string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
            _id = id;

            bloodCleanup = db.Single<BLOODCLEANUP>("where ID = @0", _id);
            bLOODCLEANUPBindingSource.DataSource = bloodCleanup;

            //EPOTextEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            EPOTextEdit.Properties.DataSource = db.Fetch<VALUE_CODE>("");
            EPOTextEdit.Properties.DisplayMember = "DSP_MEMBER";
            EPOTextEdit.Properties.ValueMember = "VALUE_MEMBER";

            MACH_TYPTextEdit.Properties.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", new object[] {3});
            MACH_TYPTextEdit.Properties.DisplayMember = "DSP_MEMBER";
            MACH_TYPTextEdit.Properties.ValueMember = "VALUE_MEMBER";

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 101);
            vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 44);
            vALUECODEBindingSource2.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 102);
            vALUECODEBindingSource3.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 161);
            aCCOUNTBindingSource.DataSource = db.Fetch<ACCOUNT>("");

            bindingSource1.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 193);              // 通路类型
            bindingSource2.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 47);               // 管路
            bindingSource3.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 43);               // 穿刺针
            bindingSource4.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 171);              // 敷贴
            bindingSource5.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 166);              // 护理包
            bindingSource6.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0", 169);              // 肝素帽

            // 血管通路类型
            if (bloodCleanup.FISTULA_TYPE == 519 || bloodCleanup.FISTULA_TYPE == 520)
            {
                ItemForAPPLICATOR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ItemForAPPLICATOR_NUM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ItemForHEPARIN_CAP.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ItemForHEPARIN_CAP_NUM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else if (bloodCleanup.FISTULA_TYPE == 704 || bloodCleanup.FISTULA_TYPE == 705)
            {
                ItemForFISTULA_NEEDLE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ItemForFISTULA_NEEDLE_NUM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ItemForFISTULA_CARE_PACKAGES.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ItemForFISTULA_CARE_PACKAGES_NUM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!dxValidationProvider1.Validate())
            //    return;
            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                bLOODCLEANUPBindingSource.EndEdit();
                bLOODCLEANUPBindingSource.CurrencyManager.EndCurrentEdit();

                try
                {
                    bloodCleanup.Update();
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

        private void ANA_WEIGHTTextEdit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            getFixWeight();
        }

        private void WEIGHTSpinEdit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            getFixWeight();
        }

        private void getFixWeight()
        {
            if (WEIGHTSpinEdit.EditValue != null && ANA_WEIGHTTextEdit.EditValue != null)
            {
                double w1 = 0;
                double w2 = 0;
                try
                {
                    w1 = Convert.ToDouble(ANA_WEIGHTTextEdit.EditValue);
                    w2 = Convert.ToDouble(WEIGHTSpinEdit.EditValue);
                    FIX_CAPACITYSpinEdit.EditValue = w1 - w2;
                    ((BLOODCLEANUP)bLOODCLEANUPBindingSource.Current).FIX_CAPACITY = (decimal)(w1 - w2);
                    //bLOODCLEANUPBindingSource.ResetCurrentItem();
                }
                catch { }
            }
        }

        private void ANA_WEIGHTTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            getFixWeight();
        }

        private void WEIGHTSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            getFixWeight();
        }

        private void ANA_STAR_TIMEDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            ANA_TIMETextEdit_Enter(null, null);
        }

        private void ANA_END_TIMEDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            ANA_TIMETextEdit_Enter(null, null);
        }

        private void EPOTextEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                _FrmDrugSelect frm = new _FrmDrugSelect(true);
                frm.SetSelectedValues(bloodCleanup.EPO);

                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (frm.LstDrugID != null && frm.LstDrugID.Count > 0)
                    {
                        string sTemp = "";
                        for (int i = 0; i < frm.LstDrugID.Count; i++)
                        {
                            sTemp += frm.LstDrugID[i].ToString() + ",";
                        }
                        sTemp.TrimEnd(',');
                        ((BLOODCLEANUP)bLOODCLEANUPBindingSource.Current).EPO = sTemp;
                        bLOODCLEANUPBindingSource.ResetCurrentItem();
                    }
                }
            }
        }

        private void EPOTextEdit_DoubleClick(object sender, EventArgs e)
        {
            EPOTextEdit_ButtonClick(null, new DevExpress.XtraEditors.Controls.ButtonPressedEventArgs(new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)));
        }

        private void ANA_TIMETextEdit_Enter(object sender, EventArgs e)
        {
            try
            {
                double h = ((DateTime)ANA_END_TIMEDateEdit.EditValue - (DateTime)ANA_STAR_TIMEDateEdit.EditValue).TotalHours;
                ANA_TIMETextEdit.EditValue = Math.Round(h, 2);
                ((BLOODCLEANUP)bLOODCLEANUPBindingSource.Current).ANA_TIME = (decimal)Math.Round(h, 2);
            }
            catch (Exception)
            {
            }     
        }
    }
}