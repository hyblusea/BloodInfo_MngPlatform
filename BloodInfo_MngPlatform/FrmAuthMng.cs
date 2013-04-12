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

namespace BloodInfo_MngPlatform
{
    public partial class FrmAuthMng : DevExpress.XtraEditors.XtraForm
    {
        List<ATH_ROLE> lstAthRole = new List<ATH_ROLE>();
        List<ATH_ROLE> lstAthRoleNoAdmin = new List<ATH_ROLE>();
        List<ACCOUNT> lstAct = new List<ACCOUNT>();
        List<ATH_CONTROL_ENABLE> lstCtlEable = new List<ATH_CONTROL_ENABLE>();
        Database db;

        public FrmAuthMng()
        {
            InitializeComponent();

            ////string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReLoadDate();
        }

        private void FrmAuthMng_Load(object sender, EventArgs e)
        {
            ReLoadDate();
        }

        private void ReLoadDate()
        {
            lstAthRole = db.Fetch<ATH_ROLE>("order by group_id desc");
            lstAthRoleNoAdmin = db.Fetch<ATH_ROLE>("where group_id <> 1");

            rOLEGROUPDataGridViewTextBoxColumn.DataSource = lstAthRole;
            rOLEGROUPDataGridViewTextBoxColumn.DisplayMember = "MEMO";
            rOLEGROUPDataGridViewTextBoxColumn.ValueMember = "GROUP_ID";

            cbxRole1.ComboBox.DataSource = lstAthRoleNoAdmin;
            cbxRole1.ComboBox.DisplayMember = "MEMO";
            cbxRole1.ComboBox.ValueMember = "GROUP_ID";

            cbxRole2.ComboBox.DataSource = lstAthRoleNoAdmin;
            cbxRole2.ComboBox.DisplayMember = "MEMO";
            cbxRole1.ComboBox.ValueMember = "GROUP_ID";

            btnRefreshAct_Click(null, null);
            btnRefreshRole_Click(null, null);
        }

        private void btnNewRole_Click(object sender, EventArgs e)
        {
            dgvRole.EndEdit();
            aTHROLEBindingSource.EndEdit();
            aTHROLEBindingSource.CurrencyManager.EndCurrentEdit();

            aTHROLEBindingSource.AddNew();
            //((ATH_ROLE)aTHROLEBindingSource.Current).isNew = true;
        }

        private void btnSaveRole_Click(object sender, EventArgs e)
        {
            dgvRole.EndEdit();
            aTHROLEBindingSource.EndEdit();
            aTHROLEBindingSource.CurrencyManager.EndCurrentEdit();

            for (int i = 0; i < lstAthRole.Count; i++)
            {
                if (lstAthRole[i].IsNew())
                    lstAthRole[i].Insert();
                else
                    lstAthRole[i].Update();
            }
        }

        private void btnRefreshRole_Click(object sender, EventArgs e)
        {
            lstAthRole = db.Fetch<ATH_ROLE>("order by group_id desc");
            aTHROLEBindingSource.DataSource = lstAthRole;
        }

        private void btnDelRole_Click(object sender, EventArgs e)
        {
            if (aTHROLEBindingSource.Current != null)
            {
                int roleId = (int)((ATH_ROLE)aTHROLEBindingSource.Current).GROUP_ID;
                if (roleId == 1)
                {
                    MessageBox.Show("管理员组不允许删除.");
                    return;
                }

                int iCount = db.ExecuteScalar<int>("select count(*) from ACCOUNT where role_group = @0", roleId);

                if (iCount > 0)
                {
                    MessageBox.Show("该组中存在用户, 不允许删除操作, 否则将造成该组中的用户无法登陆.");
                    return;
                }
                else
                {
                    if (MessageBox.Show("确实要删除该组吗? ", "操作确认", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        db.Delete((ATH_ROLE)aTHROLEBindingSource.Current);
                        btnRefreshRole_Click(null, null);
                    }
                }
            }
        }

        private void btnNewAct_Click(object sender, EventArgs e)
        {
            dgvAct.EndEdit();
            aCCOUNTBindingSource.EndEdit();
            aCCOUNTBindingSource.CurrencyManager.EndCurrentEdit();

            aCCOUNTBindingSource.AddNew();
            ((ACCOUNT)aCCOUNTBindingSource.Current).ROLE_GROUP = 1;
            ((ACCOUNT)aCCOUNTBindingSource.Current).PWD = "1";
            ((ACCOUNT)aCCOUNTBindingSource.Current).isNew = true;
        }

        private void btnDelAct_Click(object sender, EventArgs e)
        {
            if (aCCOUNTBindingSource.Current != null)
            {
                if (MessageBox.Show("确实要删除该用户吗? ", "操作确认", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    db.Delete((ACCOUNT)aCCOUNTBindingSource.Current);
                    btnRefreshAct_Click(null, null);
                }
            }
        }

        private void btnRefreshAct_Click(object sender, EventArgs e)
        {
            lstAct = db.Fetch<ACCOUNT>("");
            aCCOUNTBindingSource.DataSource = lstAct;
        }

        private void btnSaveACt_Click(object sender, EventArgs e)
        {
            dgvAct.EndEdit();
            aCCOUNTBindingSource.EndEdit();
            aCCOUNTBindingSource.CurrencyManager.EndCurrentEdit();

            for (int i = 0; i < lstAct.Count; i++)
            {
                try
                {
                    if (lstAct[i].isNew)
                        lstAct[i].Insert();
                    else
                        lstAct[i].Update();
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
                }
            }
            btnRefreshAct_Click(null, null);
        }

        private void btnRefreshBtn_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();

            List<ATH_FORMBUTTON> lstFrom = db.Fetch<ATH_FORMBUTTON>("where FATHERITEM = '0'");
            TreeNode root = new TreeNode("窗体集");

            for (int i = 0; i < lstFrom.Count; i++)
            {
                TreeNode td = new TreeNode(lstFrom[i].ITEMCAPTION);
                td.Tag = lstFrom[i].ITEMNAME;
                root.Nodes.Add(td);
            }
            treeView1.Nodes.Add(root);
            treeView1.ExpandAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                aTHCONTROLENABLEBindingSource.DataSource = null;
                return;
            }

            int groupID = (int)((ATH_ROLE)cbxRole1.SelectedItem).GROUP_ID;
            List<ATH_FORMBUTTON> lstBtn = db.Fetch<ATH_FORMBUTTON>(" where FATHERITEM = @0", new object[]{e.Node.Tag.ToString()});
            List<ATH_CONTROL_ENABLE> lstCtrlEnable = db.Fetch<ATH_CONTROL_ENABLE>("where group_id = @0 and FATHERITEM = @1", new object[] { groupID, e.Node.Tag.ToString() });

            for (int i = 0; i < lstBtn.Count; i++)
            {
                var o = lstCtrlEnable.Where(d => d.CONTROL_NAME == lstBtn[i].ITEMNAME);
                if (o.Count() == 0)
                {
                    ATH_CONTROL_ENABLE ctrlEnable = new ATH_CONTROL_ENABLE();
                    ctrlEnable.GROUP_ID = groupID;
                    ctrlEnable.CONTROL_NAME = lstBtn[i].ITEMNAME;
                    ctrlEnable.CONTROL_CAPTION = lstBtn[i].ITEMCAPTION;
                    ctrlEnable.FATHERITEM = lstBtn[i].FATHERITEM;
                    ctrlEnable.CONTROL_TYPE = 0;
                    //ctrlEnable.isNew = true;
                    ctrlEnable.ENABLE = Convert.ToInt32(false);

                    lstCtrlEnable.Add(ctrlEnable);
                }                
            }

            // 预删除已不存在的控件授权
            for (int i = 0; i < lstCtrlEnable.Count; i++)
            {
                var o = lstBtn.Where(d => d.ITEMNAME == lstCtrlEnable[i].CONTROL_NAME);
                if (o.Count() == 0)
                {
                    lstCtrlEnable[i].NeedDelete = true;
                }
            }

            aTHCONTROLENABLEBindingSource.DataSource = lstCtrlEnable;
        }

        private void btnSaveBtn_Click(object sender, EventArgs e)
        {
            if (aTHCONTROLENABLEBindingSource.DataSource == null)
                return;

            dgvCtrlEnable.EndEdit();
            aTHCONTROLENABLEBindingSource.EndEdit();
            aTHCONTROLENABLEBindingSource.CurrencyManager.EndCurrentEdit();

            var o = ((List<ATH_CONTROL_ENABLE>)aTHCONTROLENABLEBindingSource.DataSource);
            for (int i = 0; i < o.Count; i++)
            {
                if (o[i].IsNew())
                    o[i].Insert();
                else if (o[i].NeedDelete)
                    o[i].Delete();
                else
                    o[i].Update();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (aTHCONTROLENABLEBindingSource.DataSource == null)
                return;

            var o = ((List<ATH_CONTROL_ENABLE>)aTHCONTROLENABLEBindingSource.DataSource);
            for (int i = 0; i < o.Count; i++)
            {
                o[i].ENABLE = 1;
            }
            dgvCtrlEnable.Refresh();
        }

        private void cbxRole1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRefreshBtn_Click(null, null);
            treeView1.SelectedNode = treeView1.Nodes[0];
        }

        private void btnRefreshMenu_Click(object sender, EventArgs e)
        {
            tvMenu.Nodes.Clear();
            int groupID = (int)((ATH_ROLE)cbxRole2.SelectedItem).GROUP_ID;

            List<ATH_MAINMENU> lstMenu = db.Fetch<ATH_MAINMENU>("where FATHERITEM = '0'");
            TreeNode root = new TreeNode("菜单项");
            root.Name = "root";

            for (int i = 0; i < lstMenu.Count; i++)
            {
                TreeNode td = new TreeNode(lstMenu[i].ITEMCAPTION);
                td.Name = lstMenu[i].ITEMNAME;
                td.Tag = lstMenu[i].ITEMNAME;
                root.Nodes.Add(td);
                GetAllMenu(td, lstMenu[i].ITEMNAME);
            }
            tvMenu.Nodes.Add(root);
            tvMenu.ExpandAll();

            // 选中所有节点
            btnSelectAllMenu_Click(null, null);

            // 获取数据库中的权限记录, 取消选中Enable=0的节点
            lstMenu = db.Fetch<ATH_MAINMENU>("");
            for (int i = 0; i < lstMenu.Count; i++)
            {
                if (lstMenu[i].ITEMNAME == "barButtonItem14")
                { 
                }
                TreeNode td = tvMenu.Nodes.Find(lstMenu[i].ITEMNAME, true)[0];
                List<ATH_CONTROL_ENABLE> ctrlEnable = db.Fetch<ATH_CONTROL_ENABLE>("where GROUP_ID = @0 and CONTROL_NAME = @1 and CONTROL_TYPE = 1 and ENABLE = 0",
                    new object[] { groupID, lstMenu[i].ITEMNAME });
                if (ctrlEnable.Count == 1 && ctrlEnable[0].ENABLE == 0)
                    td.Checked = false;
            }

            // 导航栏元素全能设置
            List<ATH_NAVGATIONITEM> lstBtn = db.Fetch<ATH_NAVGATIONITEM>("");
            List<ATH_CONTROL_ENABLE> lstCtrlEnable = db.Fetch<ATH_CONTROL_ENABLE>("where group_id = @0 and control_type = 2", groupID);

            for (int i = 0; i < lstBtn.Count; i++)
            {
                var o = lstCtrlEnable.Where(d => d.CONTROL_NAME == lstBtn[i].ITEMNAME);
                if (o.Count() == 0)
                {
                    ATH_CONTROL_ENABLE ctrlEnable = new ATH_CONTROL_ENABLE();
                    ctrlEnable.GROUP_ID = groupID;
                    ctrlEnable.CONTROL_NAME = lstBtn[i].ITEMNAME;
                    ctrlEnable.CONTROL_CAPTION = lstBtn[i].ITEMCAPTION;
                    ctrlEnable.CONTROL_TYPE = 2;
                    //ctrlEnable.isNew = true;
                    ctrlEnable.ENABLE = Convert.ToInt32(false);

                    lstCtrlEnable.Add(ctrlEnable);
                }
            }

            // 预删除已不存在的控件授权
            for (int i = 0; i < lstCtrlEnable.Count; i++)
            {
                var o = lstBtn.Where(d => d.ITEMNAME == lstCtrlEnable[i].CONTROL_NAME);
                if (o.Count() == 0)
                {
                    lstCtrlEnable[i].NeedDelete = true;
                }
            }

            bindingSource1.DataSource = lstCtrlEnable;
        }

        private void GetAllMenu(TreeNode td, string sFatherName)
        {
            List<ATH_MAINMENU> lstMenuSub = db.Fetch<ATH_MAINMENU>("where FATHERITEM = @0", sFatherName);
            for (int i = 0; i < lstMenuSub.Count; i++)
            {
                TreeNode tdSub = new TreeNode(lstMenuSub[i].ITEMCAPTION);
                tdSub.Name = lstMenuSub[i].ITEMNAME;
                tdSub.Tag = lstMenuSub[i].ITEMNAME;
                td.Nodes.Add(tdSub);
                GetAllMenu(tdSub, lstMenuSub[i].ITEMNAME);
            }
        }

        private TreeNode SearchNode(TreeNode td, string selectParentNum)
        {
            if (td.ToolTipText == selectParentNum)
            {
                return td;
            }
            TreeNode targetNode = null;
            foreach (TreeNode childNodes in td.Nodes)
            {
                targetNode = SearchNode(childNodes, selectParentNum);
                if (targetNode != null)
                    break;
            }
            return targetNode;
        }

        private void btnSelectAllMenu_Click(object sender, EventArgs e)
        {
            tvMenu.Nodes[0].Checked = true;
        }

        private void btnSaveMenu_Click(object sender, EventArgs e)
        {
            int groupID = (int)((ATH_ROLE)cbxRole2.SelectedItem).GROUP_ID;
            db.Execute("delete ATH_CONTROL_ENABLE where group_id = @0 and control_type =1" , new object[] { groupID });
            List<ATH_MAINMENU> lstMenu = db.Fetch<ATH_MAINMENU>("");
            for (int i = 0; i < lstMenu.Count; i++)
            {
                var o = tvMenu.Nodes.Find(lstMenu[i].ITEMNAME, true)[0];
                if (!o.Checked)
                {
                    ATH_CONTROL_ENABLE ctlEnable = new ATH_CONTROL_ENABLE();
                    ctlEnable.GROUP_ID = groupID;
                    ctlEnable.CONTROL_NAME = lstMenu[i].ITEMNAME;
                    ctlEnable.CONTROL_CAPTION = lstMenu[i].ITEMCAPTION;
                    ctlEnable.ENABLE = 0;
                    ctlEnable.CONTROL_TYPE = 1;

                    ctlEnable.Insert();
                }
            }

            // 导航设置
            if (bindingSource1.DataSource == null)
                return;

            dataGridView1.EndEdit();
            aTHCONTROLENABLEBindingSource.EndEdit();
            aTHCONTROLENABLEBindingSource.CurrencyManager.EndCurrentEdit();

            var oNav = ((List<ATH_CONTROL_ENABLE>)bindingSource1.DataSource);
            for (int i = 0; i < oNav.Count; i++)
            {
                if (oNav[i].IsNew())
                    oNav[i].Insert();
                else if (oNav[i].NeedDelete)
                    oNav[i].Delete();
                else
                    oNav[i].Update();
            }
        }

        private void cbxRole2_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRefreshMenu_Click(null, null);
        }
    }
}