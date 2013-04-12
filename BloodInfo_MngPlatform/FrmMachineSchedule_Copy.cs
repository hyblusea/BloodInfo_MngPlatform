using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;
using System.Collections;
using PetaPoco;
using BloodInfo_MngPlatform.Models;

namespace BloodInfo_MngPlatform
{
    public partial class FrmMachineSchedule_Copy : DevExpress.XtraEditors.XtraForm
    {
        Database db;
        List<MACHINE_SCHEDULE> lstMachineScdu = new List<MACHINE_SCHEDULE>();

        public FrmMachineSchedule_Copy()
        {
            InitializeComponent();
            dateNavigator1.DateTime = DateTime.Now;

            wizardControl1.FinishClick += wizardControl1_FinishClick;
            dateNavigator2.EditDateModified += dateNavigator2_EditDateModified;

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");
        }

        void dateNavigator2_EditDateModified(object sender, EventArgs e)
        {
            lblToDays.Text = dateNavigator2.DateTime.ToShortDateString() + "  -  " + dateNavigator2.DateTime.AddDays(Convert.ToInt32(lblDays.Text) - 1).ToShortDateString();
            TimeSpan sp1 =  dateNavigator2.DateTime - dateNavigator1.Selection[dateNavigator1.Selection.Count - 1];
            lblSplitDays.Text = (sp1.Days - 1).ToString();
        }

        void wizardControl1_FinishClick(object sender, CancelEventArgs e)
        {
            if (dateNavigator2.DateTime <= dateNavigator1.Selection[dateNavigator1.Selection.Count - 1])
            {
                XtraMessageBox.Show("信息待填充的日期应大于 [" + dateNavigator1.Selection[dateNavigator1.Selection.Count - 1].ToShortDateString() + "].", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            int tempCnt = db.ExecuteScalar<int>("select count(*) from MACHINE_SCHEDULE where SCHEDULE_TIME between @0 and @1", new object[]{
                dateNavigator2.DateTime, 
                dateNavigator2.DateTime.AddDays(Convert.ToInt32(lblDays.Text) - 1)
            });

            if (tempCnt > 0)
            {
                if (XtraMessageBox.Show("在 " + dateNavigator2.DateTime.ToShortDateString() + "  -  " + dateNavigator2.DateTime.AddDays(Convert.ToInt32(lblDays.Text) - 1).ToShortDateString() + "已存在数据, 是否删除后继续? ", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            if (XtraMessageBox.Show("您确实要将 [" + lblStart.Text + "] 的数据复制到 [" + dateNavigator2.DateTime.ToShortDateString() + "  -  " + dateNavigator2.DateTime.AddDays(Convert.ToInt32(lblDays.Text) - 1).ToShortDateString() + "] 吗?",
                "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                lstMachineScdu = db.Fetch<MACHINE_SCHEDULE>(
                    string.Format("select FLOOR_ID, AREA_ID, SCHEDULE_TIME + {0} as SCHEDULE_TIME, SCHEEDULE_PERIOD, PT_ID, 76 as MACHINE_STATUS, BED_NO, MACHINE_INFO_ID, sysdate as RESERVATION from MACHINE_SCHEDULE where SCHEDULE_TIME between @0 and @1",
                    Convert.ToInt32(lblDays.Text) + Convert.ToInt32(lblSplitDays.Text)),
                    new object[] { dateNavigator1.SelectionStart, dateNavigator1.SelectionEnd});

                try
                {
                    using (var scope = db.GetTransaction())
                    {
                        db.Execute("delete  MACHINE_SCHEDULE where SCHEDULE_TIME between @0 and @1", new object[]{
                            dateNavigator2.DateTime, 
                            dateNavigator2.DateTime.AddDays(Convert.ToInt32(lblDays.Text) - 1)
                        });

                        for (int i = 0; i < lstMachineScdu.Count; i++)
                        {
                            db.Insert(lstMachineScdu[i]);
                        }
                        scope.Complete();
                    }
                    XtraMessageBox.Show("已复制完成.", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception err)
                {
                    XtraMessageBox.Show("操作遇到了错误, 错误原因: " + err.Message);
                    e.Cancel = true;
                }
            }
            else
                e.Cancel = true;
        }

        private void welcomeWizardPage1_PageCommit(object sender, EventArgs e)
        {
            Console.WriteLine(dateNavigator1.Selection.ToString());
            lblStart.Text = dateNavigator1.Selection[0].ToShortDateString() + "  -  " + dateNavigator1.Selection[dateNavigator1.Selection.Count - 1].ToShortDateString();
            TimeSpan ts =  dateNavigator1.Selection[dateNavigator1.Selection.Count-1] - dateNavigator1.Selection[0];
            lblDays.Text = (ts.Days + 1).ToString();

            DateTime dt = dateNavigator1.Selection[dateNavigator1.Selection.Count - 1].AddDays( 1);
            if (dt.DayOfWeek == DayOfWeek.Sunday)
                dt = dt.AddDays(1);
            dateNavigator2.DateTime = dt;
        }
    }
}