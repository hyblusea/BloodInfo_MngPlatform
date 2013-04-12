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
    public partial class FrmNewMachineType : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NewRegistEventHandler();
        public event NewRegistEventHandler NewRegistEvt;

        Database db;

        MACHINE_TYPE diag = new MACHINE_TYPE();

        public FrmNewMachineType()
        {
            InitializeComponent();
            db = new Database("XE");

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_GROUP>("where FATHERID = @0", 15);

            ImageConverter converter = new ImageConverter();
            diag.M_PICTURE = (byte[])converter.ConvertTo(global::BloodInfo_MngPlatform.Properties.Resources._20130319022303686_easyicon_net_256, typeof(byte[]));

            mACHINETYPEBindingSource.DataSource = diag;

            ConditionValidationRule ruleNoEmpty = new ConditionValidationRule();
            ruleNoEmpty.ConditionOperator = ConditionOperator.IsNotBlank;
            ruleNoEmpty.ErrorText = "该项不能为空。";
            dxValidationProvider1.SetValidationRule(MODELTextEdit, ruleNoEmpty);
            dxValidationProvider1.SetValidationRule(M_TYPELookUpEdit, ruleNoEmpty);

            M_TYPELookUpEdit.EditValueChanged += M_TYPELookUpEdit_EditValueChanged;

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

        private void SaveData()
        {
            mACHINETYPEBindingSource.EndEdit();
            mACHINETYPEBindingSource.CurrencyManager.EndCurrentEdit();

            List<MACHINE_TYPE> lst = db.Fetch<MACHINE_TYPE>("where M_TYPE = @0 and  MODEL = @1", new object[] { diag.M_TYPE, diag.MODEL });
            if(lst !=null && lst.Count > 0)
                throw new Exception ("该类型以及该型号的设备已经配置有图片, 请确认.");

            db.Insert(diag);
            if (NewRegistEvt != null)
                NewRegistEvt();

            diag = new MACHINE_TYPE();
            mACHINETYPEBindingSource.DataSource = diag;
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if (dxValidationProvider1.Validate())
                        SaveData();
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
                }
            }
        }

        private void btnSaveAndExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
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

        
    }
}