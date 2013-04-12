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
    public partial class FrmPatient_Course : DevExpress.XtraEditors.XtraForm
    {
        Database db;
        string sName = "%";

        public FrmPatient_Course()
        {
            InitializeComponent();
            db = new Database("XE");
        }

        private void FrmDocAdvice_Drugs_Load(object sender, EventArgs e)
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

            pATIENTBASEINFOBindingSource.CurrentItemChanged += pATIENTBASEINFOBindingSource_CurrentItemChanged;
            btnSearch_ItemClick(null, null);

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("");

            //ucPaing1.PageChanged += ucPaing1_PageChanged;
            //ucPaing2.PageChanged += ucPaing2_PageChanged;
            //ucPaing3.PageChanged += ucPaing3_PageChanged;
        }

        void ucPaing1_PageChanged(long curPage, int dspLen)
        {
            var page = db.Page<PATIENT_BASEINFO>(curPage, dspLen, "where NAME like @0 ORDER BY CREATE_TIME DESC", new object[] { sName });
            pATIENTBASEINFOBindingSource.DataSource = page.Items;
            ucPaing1.totalPage = page.TotalPages;
            ucPaing1.curPage = curPage;
            ucPaing1.recordCnt = page.TotalItems;
        }

        void pATIENTBASEINFOBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current != null)
            {
                refresh();
                refresh1();
            }
            else
            {
                pATIENTCOURSEBindingSource.DataSource = null;
                //dOCADVICEDRUGBindingSource1.DataSource = null;
            }
        }


        private void btnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (txtName.EditValue != null && txtName.EditValue.ToString() != string.Empty)
                    sName = "%" + txtName.EditValue.ToString() + "%";
                else
                    sName = "%";

                ucPaing1_PageChanged(1, ucPaing1.dspLenght);
            }
            catch (Exception err)
            {
                XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
            }
        }

        private void refresh()
        {
            ucPaing2_PageChanged(1, ucPaing2.dspLenght);
        }

        private void refresh1()
        {
            ucPaing3_PageChanged(1, ucPaing3.dspLenght);
        }

        private void ucPaing3_PageChanged(long curPage, int dspLen)
        {
            Page<DOC_ADVICE_DRUG> page = null;

            // 0:长期, 1:临时
            //if (barCheckItem2.Checked)
            //    page = db.Page<DOC_ADVICE_DRUG>(curPage, dspLen, "where ADVICE_TYPE = 1 and BASE_INFO_ID = @0 ORDER BY ID DESC", new object[] { ((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID });
            //else
            //    page = db.Page<DOC_ADVICE_DRUG>(curPage, dspLen, "where IS_DEL <> 1 and ADVICE_TYPE = 1 AND BASE_INFO_ID = @0  ORDER BY ID DESC", new object[] { ((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID });
            //dOCADVICEDRUGBindingSource1.DataSource = page.Items;
            //ucPaing3.totalPage = page.TotalPages;
            //ucPaing3.curPage = curPage;
            //ucPaing3.recordCnt = page.TotalItems;
        }

        private void ucPaing2_PageChanged(long curPage, int dspLen)
        {
            Page<PATIENT_COURSE> page = null;
            page = db.Page<PATIENT_COURSE>(curPage, dspLen, "where ADVICE_TYPE = 0 and BASE_INFO_ID = @0 ORDER BY ID DESC", new object[] { ((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID });
            pATIENTCOURSEBindingSource.DataSource = page.Items;
            ucPaing2.totalPage = page.TotalPages;
            ucPaing2.curPage = curPage;
            ucPaing2.recordCnt = page.TotalItems;
        }

        private void FrmDocAdvice_Drugs_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClsFrmMng.frmCourse = null;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current != null)
            {
                FrmNewDav frm1 = new FrmNewDav((Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID, 0, 0);
                frm1.NewRegistEvt += frm1_NewRegistEvt;
                frm1.ShowDialog();
            }
            else
                XtraMessageBox.Show("请选择需要新增医嘱的患者.");
        }

        void frm1_NewRegistEvt()
        {
            refresh();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (dOCADVICEDRUGBindingSource.Current != null)
            //{
            //    if ((Int64)((DOC_ADVICE_DRUG)dOCADVICEDRUGBindingSource.Current).IS_DEL != 0)
            //    {
            //        XtraMessageBox.Show("被禁用的医嘱不允许编辑.");
            //        return;
            //    }

            //    FrmEdtDav frm1 = new FrmEdtDav((Int64)((DOC_ADVICE_DRUG)dOCADVICEDRUGBindingSource.Current).ID);
            //    frm1.NewRegistEvt += frm1_NewRegistEvt;
            //    frm1.ShowDialog();
            //}
            //else
            //    XtraMessageBox.Show("请选择需要编辑的记录.");
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (dOCADVICEDRUGBindingSource.Current == null)
            //{
            //    XtraMessageBox.Show("请选择需要禁用的记录.", "错误提示", MessageBoxButtons.OK);
            //    return;
            //}
            //if (XtraMessageBox.Show("确实要禁用该医嘱信息吗?", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            //{
            //    Int64 id = (Int64)((DOC_ADVICE_DRUG)dOCADVICEDRUGBindingSource.Current).ID;
            //    db.Execute("update DOC_ADVICE_DRUGS set IS_DEL = 1, del_time = @0, del_oper = @1 where ID = @2", new object[] { DateTime.Now, ClsFrmMng.WorkerID, id });
            //    refresh();
            //}
        }

        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //refresh();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pATIENTBASEINFOBindingSource.Current != null)
            {
                FrmNewDav frm2 = new FrmNewDav((Int64)((PATIENT_BASEINFO)pATIENTBASEINFOBindingSource.Current).ID, 0, 1);
                frm2.NewRegistEvt += frm2_NewRegistEvt;
                frm2.ShowDialog();
            }
            else
                XtraMessageBox.Show("请选择需要新增医嘱的患者.");
        }

        void frm2_NewRegistEvt()
        {
            refresh1();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (dOCADVICEDRUGBindingSource1.Current == null)
            //{
            //    XtraMessageBox.Show("请选择需要禁用的记录.", "错误提示", MessageBoxButtons.OK);
            //    return;
            //}
            //if (XtraMessageBox.Show("确实要禁用该医嘱信息吗?", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            //{
            //    Int64 id = (Int64)((DOC_ADVICE_DRUG)dOCADVICEDRUGBindingSource1.Current).ID;
            //    db.Execute("update DOC_ADVICE_DRUGS set IS_DEL = 1, del_time = @0, del_oper = @1 where ID = @2", new object[] { DateTime.Now, ClsFrmMng.WorkerID, id });
            //    refresh1();
            //}
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (dOCADVICEDRUGBindingSource1.Current != null)
            //{
            //    if ((Int64)((DOC_ADVICE_DRUG)dOCADVICEDRUGBindingSource1.Current).IS_DEL != 0)
            //    {
            //        XtraMessageBox.Show("被禁用的医嘱不允许编辑.");
            //        return;
            //    }

            //    FrmEdtDav frm2 = new FrmEdtDav((Int64)((DOC_ADVICE_DRUG)dOCADVICEDRUGBindingSource1.Current).ID);
            //    frm2.NewRegistEvt += frm2_NewRegistEvt;
            //    frm2.ShowDialog();
            //}
            //else
            //    XtraMessageBox.Show("请选择需要编辑的记录.");
        }

        private void barCheckItem2_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            refresh1();
        }
    }
}
