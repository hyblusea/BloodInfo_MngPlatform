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
using CommonServiceLibrary.LISDBModels;
using System.Configuration;
using System.Collections.Specialized;
using System.Reflection;

namespace BloodInfo_MngPlatform
{
    public partial class FrmQueryCheckInfo : DevExpress.XtraEditors.XtraForm
    {
        string _strPatName;
        decimal _id;
        Database exdb;
        List<LIS_LIST> lstLisList = new List<LIS_LIST>();
        List<ADDTION_CHECK_HISTORY> lstAddCheckHis = new List<ADDTION_CHECK_HISTORY>();
        CommonServiceLibrary.ICommonService cs = new CommonServiceLibrary.CommonService();

        public FrmQueryCheckInfo(string strPatName, decimal id)
        {
            InitializeComponent();
            _strPatName = strPatName;
            _id = id;
            exdb = new Database("XE");
        }
       
        private void FrmQueryCheckInfo_Load(object sender, EventArgs e)
        {
            //获取已存在的数据
            lstAddCheckHis = exdb.Fetch<ADDTION_CHECK_HISTORY>("where base_info_id = @0", _id);
            //绑定数据源
            lstLisList = cs.GetDataForSQLServer<LIS_LIST>("LIS", "where patName = @0", _strPatName);
            lISLISTBindingSource.DataSource = lstLisList;
          
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
                List<LIS_LIST> dsLisList = (List<LIS_LIST>)lISLISTBindingSource.DataSource;              
                List<int> lstApplyNo = new List<int>();
                for (int i = 0; i < dsLisList.Count;i++ )
                {
                    if (dsLisList[i].IsChecked !=null &&(bool)dsLisList[i].IsChecked)
                    {
                        lstApplyNo.Add(dsLisList[i].ApplyNo);
                    }
                }
                if (lstApplyNo == null || lstApplyNo.Count == 0)
                {
                    XtraMessageBox.Show("请至少选择一条信息", "提示", MessageBoxButtons.OK);
                    return;
                }
                addCheckHistory(lstApplyNo);
                this.Close();
            }
        }

        private void addCheckHistory(List<int> lstApplyNo)
        {
            List<LIS_RESULT> lstresult = new List<LIS_RESULT>();
            ADDTION_CHECK_HISTORY checkEntity = new ADDTION_CHECK_HISTORY();
            
             try
                {
                    for(int i=0;i<lstApplyNo.Count;i++)
                    {
                        lstresult = cs.GetDataForSQLServer<LIS_RESULT>("LIS", "where applyno=@0", lstApplyNo[i]);
                        if (lstresult.Count <= 0) continue;
                        for(int j=0;j<lstresult.Count;j++)
                        {
                            string itemCode = ConfigurationManager.AppSettings[lstresult[j].ItemCode.ToLower().Trim()];
                            Type entityType  = checkEntity.GetType();
                            PropertyInfo propertyInfo = entityType.GetProperty(NullConvertString(itemCode).ToUpper());
                            if (propertyInfo == null) continue;
                            propertyInfo.SetValue(checkEntity, lstresult[j].ResultValue, null);
                        }
                        checkEntity.BASE_INFO_ID = _id;
                        checkEntity.LOG_TIME = DateTime.Now;
                        checkEntity.OPERATOR = ClsFrmMng.WorkerID;
                        checkEntity.BLOOD = lstresult[0].ResultTime;
                        checkEntity.APPLYNO = lstApplyNo[i];
                        checkEntity.Insert();
                    }
                }
             catch (Exception err)
             {
                 XtraMessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK);
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
                if (lstAddCheckHis.Count <= 0) return;
                for (int i = 0; i < lstAddCheckHis.Count; i++)
                {
                    //比较指定列的状态
                    if (applyno == lstAddCheckHis[i].APPLYNO.ToString())
                    {
                        e.Appearance.BackColor = Color.Yellow;//设置此行的背景颜色
                        break;
                    }
                }   
            }
        }

    }
}
