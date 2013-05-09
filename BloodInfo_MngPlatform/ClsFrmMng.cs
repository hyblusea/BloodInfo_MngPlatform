using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BloodInfo_MngPlatform.Models;

namespace BloodInfo_MngPlatform
{
    public class ClsFrmMng
    {
        public static string StatusComfirm = "申请中";
        public static string StatusComBack = "撤回";
        public static string StatusComDel = "删除";
        public static string StatusComOut = "出库";
        public static string StatusComOk = "完成";

        // 全局变量
        public static string WorkerID;
        public static string WorkerName;
        public static int GroupID;

        // 按钮使能设置
        public static List<ATH_CONTROL_ENABLE> lstCtrlEnable = new List<ATH_CONTROL_ENABLE>();

        // 值集代码表
        public static List<REG_STATUS> lstRegStatus;
        public static List<INDUCEMENT_CODE> lstIndecumentCode;
        public static List<DIGESTIVESYS_CODE> lstDigesCode;
        public static List<HaveOrNull> lstHaveOrNull;
        public static List<DocAdvType> lstDocDavType;
        public static List<SexType> lstSexType;

        // 全局窗体
        public static FrmPatientRigist frmPatientReg;
        public static FrmPatientBaseInfo frmPatientBaseInfo;
        public static FrmBloodCleanup frmBloodCleanup;
        public static FrmValueCodeMng frmValCode;
        public static FrmConsumWH frmConsumWh;
        public static FrmConsumWH_Request frmConsumWhReqst;
        public static FrmMachineScheduled frmMachineScdu;
        public static FrmMachineScheduled frmMachineCheckIn;
        public static FrmMachineScheduled frmMachineMaintence;
        public static FrmLayoutCfg frmLayoutCfg;
        public static FrmDefaultDocAdvice_Cfg frmDftDocAdv;

        public static FrmCaseHisLog frmCaseHisLog;
        public static FrmEvaluate frmEvaluate;
        public static FrmDiagnosis frmDiagnosis;
        public static FrmMachineTypeMng frmMachineType;
        public static FrmDocAdvice_Drugs frmDocAdvice;
        //public static FrmBloodCleanup_Drugs frmBC_D;
        public static FrmPatient_Course frmCourse;

        public static FrmRptPatientInfo frmRptPatientInfo;
    }

    public class HaveOrNull
    {
        public string MEMO { get; set; }
        public int ID { get; set; }

        public HaveOrNull(int id, string memo)
        {
            ID = id;
            MEMO = memo;
        }
    }

    public class DocAdvType
    {
        public string TypeMEMO { get; set; }
        public int TypeID { get; set; }

        public DocAdvType(int id, string memo)
        {
            TypeID = id;
            TypeMEMO = memo;
        }
    }

    public class SexType
    {
        public string SexCode { get; set; }
        public string SexName { get; set; }

        public SexType(string sexcode,string sexname)
        {
            SexCode = sexcode;
            SexName = sexname;
        }
    }
}
