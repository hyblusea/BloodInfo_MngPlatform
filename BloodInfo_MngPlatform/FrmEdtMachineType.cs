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
    public partial class FrmEdtMachineType : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;
        Int64 _id;
        MACHINE_TYPE diag;

        public FrmEdtMachineType(Int64 id)
        {
            InitializeComponent();
            _id = id;
            db = new Database("XE");

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_GROUP>("where FATHERID = @0", 15);
            //vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", 3);

            ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            ruleNoEmpty.ErrorText = "该项不能为空。";
            dxValidationProvider1.SetValidationRule(MODELTextEdit, ruleNoEmpty);
            dxValidationProvider1.SetValidationRule(M_TYPELookUpEdit, ruleNoEmpty);

            diag = db.Single<MACHINE_TYPE>(_id);
            mACHINETYPEBindingSource.DataSource = diag;
        }

        private void SaveData()
        {
            mACHINETYPEBindingSource.EndEdit();
            mACHINETYPEBindingSource.CurrencyManager.EndCurrentEdit();

            db.Update(diag);
            if (NewRegistEvt != null)
                NewRegistEvt();
        }

        private void M_PICTUREPictureEdit_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "图片(*.bmp;*.gif;*.jpg;*.png)|*.bmp;*.gif;*.jpg;*.png";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    ImageConverter converter = new ImageConverter();
                    diag.M_PICTURE = (byte[])converter.ConvertTo(Bitmap.FromFile(ofd.FileName), typeof(byte[]));
                    mACHINETYPEBindingSource.ResetCurrentItem();
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show("读取图片文件时发生错误. 原因: " + err.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void M_PICTUREPictureEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Delete)
            {
                diag.M_PICTURE = null;
                mACHINETYPEBindingSource.ResetCurrentItem();
            }
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该信息,并关闭该窗口？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if (dxValidationProvider1.Validate())
                    {
                        SaveData();
                        this.Close();
                    }
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
                }
            }
        }

        private void barExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void M_TYPELookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (M_TYPELookUpEdit.EditValue == null || M_TYPELookUpEdit.EditValue.ToString() == "")
            {
                MODELTextEdit.EditValue = null;
                vALUECODEBindingSource1.DataSource = null;
            }
            else
                vALUECODEBindingSource1.DataSource = db.Fetch<VALUE_CODE>("where groupName = @0", M_TYPELookUpEdit.EditValue);
        }
    }
}