using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PetaPoco;
using DevExpress.XtraEditors;
using BloodInfo_MngPlatform.Models;
using CommonServiceLibrary.RISDBModels;
using System.Configuration;
using System.Collections.Specialized;
using System.Reflection;
using CommonServiceLibrary;

namespace BloodInfo_MngPlatform
{
    public partial class FrmQueryCheckExtInfo : DevExpress.XtraEditors.XtraForm
    {
        string _strPatName;
        decimal _id;
        Database db;
        List<Ris_List> lstRisList = new List<Ris_List>();
        List<ADDTION_CHECK_HISTORY_EXT> lstAddCheckHisExt = new List<ADDTION_CHECK_HISTORY_EXT>();
        CommServiceReference.CommonServiceClient cs = new CommServiceReference.CommonServiceClient();
 
        public FrmQueryCheckExtInfo(string strPatName, decimal id)
        {
            InitializeComponent();
            _strPatName = strPatName;
            _id = id;
            db = new Database("XE");
        }

        private void FrmQueryCheckExtInfo_Load(object sender, EventArgs e)
        {
            //获取已存在的数据
            lstAddCheckHisExt = db.Fetch<ADDTION_CHECK_HISTORY_EXT>("where base_info_id = @0", _id);
            string sql = "select a.applyno,a.hospno,b.itemname,c.itemresult,a.exectime,a.patname,a.sex,a.birthday,a.age,"+
                "a.ageunit,a.bedno,a.career,a.phone,a.address from ris_list a ,ris_acceptitems b, " +
                "(select applyno,'检查所见：'+ max(case  when itemcode='jcsj' then itemresult else null end)+  "+
                "'检查结论:' + max(case  when itemcode='jcjl' then itemresult else null end) itemresult from ris_result group by applyno) c "+
                "where a.applyno=b.applyno and b.applyno=c.applyno  and b.itemname in ('常规心电图检查（十二通道)','心脏彩超声检查(东院)','双肾血管彩超检查','胸片','心胸比') " +
                "and a.exectime=(select max(exectime) from ris_list where patname=a.patname group by patname ) "+
                "and a.patname=@0";
            //绑定数据源
            lstRisList = cs.GetDataRisList(sql, new object[] { _strPatName }).ToList();
            rISLISTBindingSource1.DataSource = lstRisList;

            //绑定性别的数据源
            repositoryItemLookUpEdit1.DataSource = ClsFrmMng.lstSexType;
            repositoryItemLookUpEdit1.DisplayMember = "SexName";
            repositoryItemLookUpEdit1.ValueMember = "SexCode";
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("确定保存该患者基本信息？", "操作确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                gridView1.CloseEditor();
                List<Ris_List> dsRisList = (List<Ris_List>)rISLISTBindingSource1.DataSource;
                ADDTION_CHECK_HISTORY_EXT checkEntity = new ADDTION_CHECK_HISTORY_EXT();

                bool flag = true;
                for (int i = 0; i < dsRisList.Count; i++)
                {
                    if ((bool)dsRisList[i].IsChecked)
                    {
                        string itemName = ConfigurationManager.AppSettings[dsRisList[i].ItemName];
                        Type entityType = checkEntity.GetType();
                        PropertyInfo propertyInfo = entityType.GetProperty(NullConvertString(itemName).ToUpper());
                        if (propertyInfo == null) continue;
                        propertyInfo.SetValue(checkEntity, dsRisList[i].ItemResult, null);
                        checkEntity.BASE_INFO_ID = _id;
                        checkEntity.LOG_TIME = DateTime.Now;
                        checkEntity.OPERATOR = ClsFrmMng.WorkerID;
                        checkEntity.APPLYNO = dsRisList[i].ApplyNo;
                        db.Insert(checkEntity);
                        //checkEntity.Insert(); 
                        flag = false;
                    }
                }
                if (flag)
                {
                    XtraMessageBox.Show("请至少选择一条信息", "提示", MessageBoxButtons.OK);
                    return;
                }
                
                this.Close();
            }
        }

        private string NullConvertString(object obj)
        {
            return (obj == null ? "" : obj.ToString());
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (gridView1.GetRow(e.RowHandle) == null)
            {
                return;
            }
            else
            {
                //获取所在行指定列的值
                string applyno = gridView1.GetRowCellValue(e.RowHandle, "ApplyNo").ToString();
                if (lstAddCheckHisExt.Count <= 0) return;
                for (int i = 0; i < lstAddCheckHisExt.Count; i++)
                {
                    //比较指定列的状态
                    if (applyno == lstAddCheckHisExt[i].APPLYNO.ToString())
                    {
                        e.Appearance.BackColor = Color.Yellow;//设置此行的背景颜色
                        break;
                    }
                }
            }
        }
    }
}
