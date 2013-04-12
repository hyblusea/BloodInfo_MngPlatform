using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Configuration;
using PetaPoco;
using BloodInfo_MngPlatform.Models;

namespace BloodInfo_MngPlatform
{
    public partial class _FrmDrugSelect : DevExpress.XtraEditors.XtraForm
    {
        Database db;
        List<VALUE_GROUP> lstDrguGrouyp = new List<VALUE_GROUP>();
        bool _isMultipleSelect = false;

        public List<decimal> LstDrugID;

        Dictionary<decimal, SelectedDrug> lstSelectedDrug = new Dictionary<decimal, SelectedDrug>();

        public class Drug
        {
            public delegate void ChangeEventHandler(decimal id, bool isCheck, string sName);
            public event ChangeEventHandler ChangeEvt;

            public Drug(bool b, decimal i, string n)
            {
                _isCheck = b;
                ID = i;
                DrugName = n;
            }

            public bool _isCheck;
            public bool IsCheck
            {
                get { return _isCheck; }
                set
                {
                    if (ChangeEvt != null)
                    {
                        ChangeEvt(this.ID, value, this.DrugName);
                    }
                    _isCheck = value;
                }
            }
            public decimal ID { get; set; }
            public string DrugName { get; set; }
        }

        public class SelectedDrug
        {
            public SelectedDrug(decimal i, string n)
            {
                ID = i;
                DrugName = n;
            }
            public decimal ID { get; set; }
            public string DrugName { get; set; }
        }

        public _FrmDrugSelect(bool isMultipleSelect = false)
        {
            InitializeComponent();
            db = new Database("XE");

            var v = db.Single<VALUE_GROUP>("where ID = @0 order by ID desc", 18);
            lstDrguGrouyp.Add(v);
            GetAllDrugGroup(18);
            vALUEGROUPBindingSource.DataSource = lstDrguGrouyp;

            treeList1.ExpandAll();
            _isMultipleSelect = isMultipleSelect;

            if (!isMultipleSelect)
            {
                repositoryItemCheckEdit1.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
                gridColumn1.Visible = false;
                xtraTabControl2.Visible = false;
                this.Width = 592;
            }
        }

        public void SetSelectedValues(string sValues)
        {
            if (string.IsNullOrEmpty(sValues))
                return;

            string[] s = sValues.Split(',');
            List<SelectedDrug> lst = new List<SelectedDrug>();
            for (int i = 0; i < s.Length; i++)
            {
                if (!string.IsNullOrEmpty(s[i]))
                {
                    try
                    {
                        Convert.ToDecimal(s[i]);
                        string s1 = db.ExecuteScalar<string>("select DSP_MEMBER from VALUE_CODE where VALUE_MEMBER = @0", Convert.ToDecimal(s[i]));
                        SelectedDrug sd = new SelectedDrug(Convert.ToDecimal(s[i]), s1);
                        lstSelectedDrug.Add(Convert.ToDecimal(s[i]), sd);
                        lst.Add(sd);
                    }
                    catch { }                    
                }
            }
            bdsSelectedDrug.DataSource = lst;
            treeList1_FocusedNodeChanged(null, null);
        }

        private void GetAllDrugGroup(decimal fatherId)
        {
            var v = db.Fetch<VALUE_GROUP>("where fatherID = @0 order by ID desc", fatherId);
            if (v != null && v.Count() > 0)
            {
                for (int i = 0; i < v.Count(); i++)
                {
                    lstDrguGrouyp.Add(v[i]);
                    GetAllDrugGroup(v[i].ID);
                }
            }
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (vALUEGROUPBindingSource.Current != null)
            {
                decimal id = ((VALUE_GROUP)vALUEGROUPBindingSource.Current).ID;
                List<VALUE_CODE> lstDrug = db.Fetch<VALUE_CODE>("where GroupName = @0", id);
                if (lstDrug != null)
                {
                    List<Drug> lstTemp = new List<Drug>();
                    for (int i = 0; i < lstDrug.Count(); i++)
                    {
                        Drug d = new Drug(false, lstDrug[i].VALUE_MEMBER, lstDrug[i].DSP_MEMBER);
                        d.ChangeEvt += d_ChangeEvt;

                        lstTemp.Add(d);
                    }
                    bdsDrugSelect.DataSource = lstTemp;

                    if (_isMultipleSelect)
                    {
                        // 选中已被选择的药品
                        for (int i = 0; i < lstTemp.Count; i++)
                        {
                            SelectedDrug o = null;
                            lstSelectedDrug.TryGetValue(lstTemp[i].ID, out o);
                            if (o != null)
                            {
                                lstTemp[i]._isCheck = true;
                            }
                        }
                    }
                }
                else
                    bdsDrugSelect.DataSource = null;
            }
            else
                bdsDrugSelect.DataSource = null;

            
        }

        void d_ChangeEvt(decimal id, bool isCheck, string sName)
        {
            if (isCheck)
            {
                try
                {
                    SelectedDrug sd = new SelectedDrug(id, sName);
                    lstSelectedDrug.Add(id, sd);
                }
                catch { }
            }
            else
                lstSelectedDrug.Remove(id);

            List<SelectedDrug> lst = new List<SelectedDrug>();
            foreach ( KeyValuePair<decimal, SelectedDrug> v in lstSelectedDrug)
            {
                lst.Add(v.Value);
            }
            bdsSelectedDrug.DataSource = lst;
        }

        // 确定
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LstDrugID = new List<decimal>();
            if (!_isMultipleSelect)
            {
                if (bdsDrugSelect.Current != null)
                {
                    LstDrugID.Add(((Drug)bdsDrugSelect.Current).ID);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else
                    XtraMessageBox.Show("请选择药品.");
            }
            else
            {
                if (lstSelectedDrug.Count > 0)
                {
                    LstDrugID.AddRange(lstSelectedDrug.Keys);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else
                    XtraMessageBox.Show("请选择药品.");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gridView1.CloseEditor();
        }

        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
