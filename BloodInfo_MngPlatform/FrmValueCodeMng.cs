using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;
using PetaPoco;
using BloodInfo_MngPlatform.Models;
using System.Linq;
using DevExpress.XtraEditors.DXErrorProvider;

namespace BloodInfo_MngPlatform
{
    public partial class FrmValueCodeMng : DevExpress.XtraEditors.XtraForm
    {
        Database db;

        public FrmValueCodeMng()
        {
            InitializeComponent();
            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            vALUEGROUPBindingSource.CurrentItemChanged += vALUEGROUPBindingSource_CurrentItemChanged;
        }

        void vALUEGROUPBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (vALUEGROUPBindingSource.Current != null)
            {
                lblGroupName.Caption = ((VALUE_GROUP)vALUEGROUPBindingSource.Current).GROUPNAME;
                vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where GROUPNAME = @0 order by VALUE_MEMBER asc", new object[] { ((VALUE_GROUP)vALUEGROUPBindingSource.Current).ID });
            }
            else
            {
                lblGroupName.Caption = "";
                vALUECODEBindingSource.DataSource = null;
            }
        }

        private void FrmValueCodeMng_Load(object sender, EventArgs e)
        {
            // Form按钮使用设置
            List<ATH_CONTROL_ENABLE> lstFrmCtrl = ClsFrmMng.lstCtrlEnable.Where(c => c.FATHERITEM == this.Name).ToList<ATH_CONTROL_ENABLE>();
            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            for (int i = 0; i < lstFrmCtrl.Count(); i++)
            {
                dic.Add(lstFrmCtrl[i].CONTROL_NAME, Convert.ToBoolean(lstFrmCtrl[i].ENABLE));
            }
            AuthrzForDevDx.ScanFormControl ctrlEnable = new AuthrzForDevDx.ScanFormControl();
            ctrlEnable.SetBtn(this.components.Components, dic);

            vALUEGROUPBindingSource.DataSource = db.Fetch<VALUE_GROUP>("order by ID desc");
            treeList1.ExpandAll();
        }

        private void FrmValueCodeMng_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClsFrmMng.frmValCode = null;
        }

        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtGroupName.EditValue == null)
            {
                XtraMessageBox.Show("请输入[分组名称].", "错误提示", MessageBoxButtons.OK,  MessageBoxIcon.Error);
                return;
            }
            VALUE_GROUP vg = new VALUE_GROUP();
            vg.ISENABLE = 1;
            vg.FATHERID = 0;
            vg.GROUPNAME = txtGroupName.EditValue.ToString();
            vg.Insert();
            XtraMessageBox.Show("新增成功, 请联系技术支持人员对新建值集分组进行支持.", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            vALUEGROUPBindingSource.DataSource = db.Fetch<VALUE_GROUP>("order by ID desc");
            treeList1.ExpandAll();
            txtGroupName.EditValue = "";
            treeList1.Refresh();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (vALUEGROUPBindingSource.Current == null)
            {
                XtraMessageBox.Show("请输需要删除的分组项目.", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (XtraMessageBox.Show("删除分组数据有可能会导致程序无法正常运行.确实要删除该分组吗?", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                var vg = ((VALUE_GROUP)vALUEGROUPBindingSource.Current);
                DelAllNodes(vg.ID);
                //vg.Delete();
                vALUEGROUPBindingSource.DataSource = db.Fetch<VALUE_GROUP>("order by ID desc");       
            } 
        }

        private void DelAllNodes(decimal fatherId)
        {
            var v = db.Fetch<VALUE_GROUP>("where FATHERID = @0", fatherId);
            if (v != null && v.Count() > 0)
            {
                for (int i = 0; i < v.Count(); i++)
                {
                    // 删除该分组
                    db.Delete(v[i]);

                    // 删除该分组下的所有值集
                    db.Execute("delete VALUE_CODE where GROUPNAME = @0", v[i].ID);

                    // 遍历该分组的子类分组
                    DelAllNodes(v[i].ID);
                }
            }
            else
            {
                db.Delete("VALUE_GROUP", "ID", null, fatherId);
                db.Execute("delete VALUE_CODE where GROUPNAME = @0", fatherId);
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (vALUEGROUPBindingSource.Current == null)
            {
                XtraMessageBox.Show("请输需要删除的分组项目.", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            gridView1.CloseEditor();
            dgvValCode.EndUpdate();
            vALUECODEBindingSource.EndEdit();
            vALUECODEBindingSource.CurrencyManager.EndCurrentEdit();

            vALUECODEBindingSource.AddNew();
            ((VALUE_CODE)vALUECODEBindingSource.Current).GROUPNAME = ((VALUE_GROUP)vALUEGROUPBindingSource.Current).ID;
            bool a = ((VALUE_CODE)vALUECODEBindingSource.Current).IsNew();             
            //((VALUE_CODE)vALUECODEBindingSource.Current).isNew = true;
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (vALUECODEBindingSource.Current != null)
            {
                if (XtraMessageBox.Show("确实要删除信息吗? ", "操作确认", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    db.Delete((VALUE_CODE)vALUECODEBindingSource.Current);
                    btnRefresh_ItemClick(null, null);
                }
            }
        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vALUEGROUPBindingSource_CurrentItemChanged(null, null);
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            treeList1.CloseEditor();
            vALUEGROUPBindingSource.EndEdit();
            vALUEGROUPBindingSource.CurrencyManager.EndCurrentEdit();

            gridView1.CloseEditor();
            vALUECODEBindingSource.EndEdit();
            vALUECODEBindingSource.CurrencyManager.EndCurrentEdit();
            dgvValCode.EndUpdate();

            List<VALUE_CODE> lst = (List<VALUE_CODE>)vALUECODEBindingSource.DataSource;
            for (int i = 0; i < lst.Count; i++)
            {
                try
                {
                    lst[i].Save();
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
                }
            }

            List<VALUE_GROUP> lstGroup = (List<VALUE_GROUP>)vALUEGROUPBindingSource.DataSource ;
            for (int i = 0; i < lstGroup.Count(); i++ )
            {
                try
                {
                    lstGroup[i].Update();
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
                }
            }
            btnRefresh_ItemClick(null, null);
        }

        private void btnNewSub_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtGroupName.EditValue == null)
            {
                XtraMessageBox.Show("请输入[分组名称].", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (vALUEGROUPBindingSource.Current == null)
            {
                XtraMessageBox.Show("请选择父亲分组.", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                VALUE_GROUP v = (VALUE_GROUP)vALUEGROUPBindingSource.Current;

                VALUE_GROUP vg = new VALUE_GROUP();
                vg.GROUPNAME = txtGroupName.EditValue.ToString();
                vg.FATHERID = v.ID;
                vg.ISENABLE = 1;
                vg.Insert();

                XtraMessageBox.Show("新增成功, 请联系技术支持人员对新建值集分组进行支持.", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                vALUEGROUPBindingSource.DataSource = db.Fetch<VALUE_GROUP>("order by ID desc");
                treeList1.ExpandAll();
                txtGroupName.EditValue = "";
            }
            treeList1.Refresh();
        }

    
    }
}