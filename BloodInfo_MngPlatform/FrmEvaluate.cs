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
    public partial class FrmEvaluate : XtraForm
    {
        Database db;
        List<PATIENT_REGIST> reg = new List<PATIENT_REGIST>();
        EVALUATE evaluate = new EVALUATE();

        List<EVALUATE> lstEval = new List<EVALUATE>();
        public FrmEvaluate()
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
        }

        private void FrmEvaluate_Load(object sender, EventArgs e)
        {
            pATIENTREGISTBindingSource.CurrentItemChanged += pATIENTREGISTBindingSource_CurrentItemChanged;
            btnRefresh_ItemClick(null, null);

            // Form按钮使用设置
            List<ATH_CONTROL_ENABLE> lstFrmCtrl = ClsFrmMng.lstCtrlEnable.Where(c => c.FATHERITEM == this.Name).ToList<ATH_CONTROL_ENABLE>();
            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            for (int i = 0; i< lstFrmCtrl.Count(); i++)
            {
                dic.Add(lstFrmCtrl[i].CONTROL_NAME, Convert.ToBoolean(lstFrmCtrl[i].ENABLE));
            }
            AuthrzForDevDx.ScanFormControl ctrlEnable = new AuthrzForDevDx.ScanFormControl();
            ctrlEnable.SetBtn(this.components.Components, dic);
        }

        void pATIENTREGISTBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (pATIENTREGISTBindingSource.Current != null)
            {
                PATIENT_REGIST p = ((PATIENT_REGIST)pATIENTREGISTBindingSource.Current);

                // 记录
                var o = db.Fetch<EVALUATE>("where reg_id = @0 order by log_date desc", new object[] { p.ID });
                evaluate.REG_ID = p.ID;
                if (o.Count == 0)
                    db.Insert(evaluate);
                else
                    evaluate = o[0];
                txtEvaluate.Text = evaluate.MEMO;
            }
        }

        private void FrmEvaluate_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClsFrmMng.frmEvaluate = null;
        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string sId = "%";
            if (lblCaseHisID.EditValue != null && lblCaseHisID.EditValue.ToString() != string.Empty)
                sId = lblCaseHisID.EditValue.ToString();
            else
                sId = "%";

            reg = db.Fetch<PATIENT_REGIST>("where status = 2 and NAME like @0 ORDER BY INNSER_SORT ASC", new object[] { sId });

            //reg = db.Fetch<PATIENT_REGIST>("where status = 2 order by INNSER_SORT ASC");
            pATIENTREGISTBindingSource.DataSource = reg;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTREGISTBindingSource.Current != null)
            {
                PATIENT_REGIST p = ((PATIENT_REGIST)pATIENTREGISTBindingSource.Current);
                evaluate.MEMO = txtEvaluate.Text;
                db.Execute("update EVALUATE set memo = @0 where reg_id = @1", new object[] { evaluate.MEMO, evaluate.REG_ID });     
            }
            else
                XtraMessageBox.Show("请选择需要作评估的患者记录。", "错误提示",   MessageBoxButtons.OK);
        }

        private void btnEndEvaluate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTREGISTBindingSource.Current != null)
            {
                if (txtEvaluate.Text != null)
                {
                    if (XtraMessageBox.Show("您确实要保存评估内容并完成评估吗？", "操作确认", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        PATIENT_REGIST p = ((PATIENT_REGIST)pATIENTREGISTBindingSource.Current);
                        evaluate.MEMO = txtEvaluate.Text;
                        db.Execute("update EVALUATE set memo = @0 , log_date = sysDate where reg_id = @1", new object[] { evaluate.MEMO, evaluate.REG_ID });

                        p.STATUS = 3;
                        db.Update(p);
                        btnRefresh_ItemClick(null, null);
                    }
                }
                else
                    XtraMessageBox.Show("请输入评估内容。", "错误提示", MessageBoxButtons.OK);
            }
            else
                XtraMessageBox.Show("请选择需要作评估的患者记录。", "错误提示", MessageBoxButtons.OK);
        }

        private void btnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string sql = "select * from EVALUATE  where log_date between @0 and @1 and CASE_HISTROY_ID like @2";
            string casehisId = "%";
            if (txtCaseHisID.EditValue != null && txtCaseHisID.EditValue.ToString() != string.Empty)
                casehisId = txtCaseHisID.EditValue.ToString();
            lstEval = db.Fetch<EVALUATE>(sql, new object[] { (DateTime)dtStart.EditValue, (DateTime)dtEnd.EditValue, casehisId });
            eVALUATEBindingSource.DataSource = lstEval;

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl2.ShowRibbonPrintPreview();
        }

        private void chartControl1_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Frm t = new Frm();
            t.Show();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            dtStart.EditValue = DateTime.Now.Date.AddDays(-1);
            dtEnd.EditValue = DateTime.Now;
        }
    }
}