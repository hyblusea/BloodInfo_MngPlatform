using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CommonServiceLibrary
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface ICommonService
    {
        [OperationContract]
        List<Lis_List> GetDataLisList(string sql, params object[] args);

        [OperationContract]
        List<Ris_List> GetDataRisList(string sql, params object[] args);

        [OperationContract]
        List<Lis_Result> GetDataLisResult(string sql, params object[] args);

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: 在此添加您的服务操作
    }

    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    [DataContract]
    public class Lis_List
    {
        [DataMember]
        public bool IsChecked
        {
            get
            {
                return _IsChecked;
            }
            set
            {
                _IsChecked = value;
            }
        }
        bool _IsChecked;

        [DataMember]
        public int ApplyNo
        {
            get
            {
                return _ApplyNo;
            }
            set
            {
                _ApplyNo = value;
            }
        }
        int _ApplyNo;

        [DataMember]
        public DateTime ExecTime
        {
            get
            {
                return _ExecTime;
            }
            set
            {
                _ExecTime = value;
            }
        }
        DateTime _ExecTime;

        [DataMember]
        public int InstID
        {
            get
            {
                return _InstID;
            }
            set
            {
                _InstID = value;
            }
        }
        int _InstID;

        [DataMember]
        public string SampleType
        {
            get
            {
                return _SampleType;
            }
            set
            {
                _SampleType = value;
            }
        }
        string _SampleType;

        [DataMember]
        public int TechNo
        {
            get
            {
                return _TechNo;
            }
            set
            {
                _TechNo = value;
            }
        }
        int _TechNo;

        [DataMember]
        public string CureNo
        {
            get
            {
                return _CureNo;
            }
            set
            {
                _CureNo = value;
            }
        }
        string _CureNo;

        [DataMember]
        public string CardNo
        {
            get
            {
                return _CardNo;
            }
            set
            {
                _CardNo = value;
            }
        }
        string _CardNo;

        [DataMember]
        public string HospNo
        {
            get
            {
                return _HospNo;
            }
            set
            {
                _HospNo = value;
            }
        }
        string _HospNo;

       [DataMember]
        public string PatName
        {
            get
            {
                return _PatName;
            }
            set
            {
                _PatName = value;
            }
        }
        string _PatName;

        [DataMember]
        public string ImeCode
        {
            get
            {
                return _ImeCode;
            }
            set
            {
                _ImeCode = value;
            }
        }
        string _ImeCode;

       [DataMember]
        public string Sex
        {
            get
            {
                return _Sex;
            }
            set
            {
                _Sex = value;
            }
        }
        string _Sex;

        [DataMember]
        public DateTime Birthday
        {
            get
            {
                return _Birthday;
            }
            set
            {
                _Birthday = value;
            }
        }
        DateTime _Birthday;

        [DataMember]
        public int? Age
        {
            get
            {
                return _Age;
            }
            set
            {
                _Age = value;
            }
        }
        int? _Age;

        [DataMember]
        public string AgeUnit
        {
            get
            {
                return _AgeUnit;
            }
            set
            {
                _AgeUnit = value;
            }
        }
        string _AgeUnit;

        [DataMember]
        public string ChargeType
        {
            get
            {
                return _ChargeType;
            }
            set
            {
                _ChargeType = value;
            }
        }
        string _ChargeType;

       [DataMember]
        public string WardOrReg
        {
            get
            {
                return _WardOrReg;
            }
            set
            {
                _WardOrReg = value;
            }
        }
        string _WardOrReg;

       [DataMember]
        public string Ward
        {
            get
            {
                return _Ward;
            }
            set
            {
                _Ward = value;
            }
        }
        string _Ward;

       [DataMember]
        public string BedNo
        {
            get
            {
                return _BedNo;
            }
            set
            {
                _BedNo = value;
            }
        }
        string _BedNo;

        [DataMember]
        public string ApplyDept
        {
            get
            {
                return _ApplyDept;
            }
            set
            {
                _ApplyDept = value;
            }
        }
        string _ApplyDept;

       [DataMember]
        public string ApplyDeptName
        {
            get
            {
                return _ApplyDeptName;
            }
            set
            {
                _ApplyDeptName = value;
            }
        }
        string _ApplyDeptName;

        [DataMember]
        public DateTime ApplyTime
        {
            get
            {
                return _ApplyTime;
            }
            set
            {
                _ApplyTime = value;
            }
        }
        DateTime _ApplyTime;

        [DataMember]
        public string ApplyRequset
        {
            get
            {
                return _ApplyRequset;
            }
            set
            {
                _ApplyRequset = value;
            }
        }
        string _ApplyRequset;

        [DataMember]
        public string ClinicDesc
        {
            get
            {
                return _ClinicDesc;
            }
            set
            {
                _ClinicDesc = value;
            }
        }
        string _ClinicDesc;

       [DataMember]
        public string Sample
        {
            get
            {
                return _Sample;
            }
            set
            {
                _Sample = value;
            }
        }
        string _Sample;

        [DataMember]
        public DateTime SampleTime
        {
            get
            {
                return _SampleTime;
            }
            set
            {
                _SampleTime = value;
            }
        }
        DateTime _SampleTime;

        [DataMember]
        public DateTime ReceiveTime
        {
            get
            {
                return _ReceiveTime;
            }
            set
            {
                _ReceiveTime = value;
            }
        }
        DateTime _ReceiveTime;

        [DataMember]
        public string ExecDept
        {
            get
            {
                return _ExecDept;
            }
            set
            {
                _ExecDept = value;
            }
        }
        string _ExecDept;

        [DataMember]
        public DateTime ReportTime
        {
            get
            {
                return _ReportTime;
            }
            set
            {
                _ReportTime = value;
            }
        }
        DateTime _ReportTime;

        [DataMember]
        public string OrgApplyNo
        {
            get
            {
                return _OrgApplyNo;
            }
            set
            {
                _OrgApplyNo = value;
            }
        }
        string _OrgApplyNo;

        [DataMember]
        public string PatPropNo
        {
            get
            {
                return _PatPropNo;
            }
            set
            {
                _PatPropNo = value;
            }
        }
        string _PatPropNo;

        [DataMember]
        public string ToDocCode
        {
            get
            {
                return _ToDocCode;
            }
            set
            {
                _ToDocCode = value;
            }
        }
        string _ToDocCode;

        [DataMember]
        public string ToDocName
        {
            get
            {
                return _ToDocName;
            }
            set
            {
                _ToDocName = value;
            }
        }
        string _ToDocName;

        [DataMember]
        public string RegisterCode
        {
            get
            {
                return _RegisterCode;
            }
            set
            {
                _RegisterCode = value;
            }
        }
        string _RegisterCode;

        [DataMember]
        public string RegisterName
        {
            get
            {
                return _RegisterName;
            }
            set
            {
                _RegisterName = value;
            }
        }
        string _RegisterName;

        [DataMember]
        public string ExecDocCode
        {
            get
            {
                return _ExecDocCode;
            }
            set
            {
                _ExecDocCode = value;
            }
        }
        string _ExecDocCode;

        [DataMember]
        public string ExecDocName
        {
            get
            {
                return _ExecDocName;
            }
            set
            {
                _ExecDocName = value;
            }
        }
        string _ExecDocName;

        [DataMember]
        public string VerifierCode
        {
            get
            {
                return _VerifierCode;
            }
            set
            {
                _VerifierCode = value;
            }
        }
        string _VerifierCode;

        [DataMember]
        public string VerifierName
        {
            get
            {
                return _VerifierName;
            }
            set
            {
                _VerifierName = value;
            }
        }
        string _VerifierName;

        [DataMember]
        public string SampleDesc
        {
            get
            {
                return _SampleDesc;
            }
            set
            {
                _SampleDesc = value;
            }
        }
        string _SampleDesc;

       [DataMember]
        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }
        string _Status;

       [DataMember]
        public string PrintStatus
        {
            get
            {
                return _PrintStatus;
            }
            set
            {
                _PrintStatus = value;
            }
        }
        string _PrintStatus;

       [DataMember]
        public string ExamResult
        {
            get
            {
                return _ExamResult;
            }
            set
            {
                _ExamResult = value;
            }
        }
        string _ExamResult;

        [DataMember]
        public string NoPassReason
        {
            get
            {
                return _NoPassReason;
            }
            set
            {
                _NoPassReason = value;
            }
        }
        string _NoPassReason;

       [DataMember]
        public string Comment
        {
            get
            {
                return _Comment;
            }
            set
            {
                _Comment = value;
            }
        }
        string _Comment;

        [DataMember]
        public string ChargeStatus
        {
            get
            {
                return _ChargeStatus;
            }
            set
            {
                _ChargeStatus = value;
            }
        }
        string _ChargeStatus;

        [DataMember]
        public int? Redo
        {
            get
            {
                return _Redo;
            }
            set
            {
                _Redo = value;
            }
        }
        int? _Redo;

       [DataMember]
        public string M_Phone
        {
            get
            {
                return _M_Phone;
            }
            set
            {
                _M_Phone = value;
            }
        }
        string _M_Phone;

        [DataMember]
        public short? MobileStatus
        {
            get
            {
                return _MobileStatus;
            }
            set
            {
                _MobileStatus = value;
            }
        }
        short? _MobileStatus;

        [DataMember]
        public DateTime PubDateTime
        {
            get
            {
                return _PubDateTime;
            }
            set
            {
                _PubDateTime = value;
            }
        }
        DateTime _PubDateTime;

        [DataMember]
        public int PrintCount
        {
            get
            {
                return _PrintCount;
            }
            set
            {
                _PrintCount = value;
            }
        }
        int _PrintCount;

        [DataMember]
        public string HospID
        {
            get
            {
                return _HospID;
            }
            set
            {
                _HospID = value;
            }
        }
        string _HospID;

        [DataMember]
        public string PubCode
        {
            get
            {
                return _PubCode;
            }
            set
            {
                _PubCode = value;
            }
        }
        string _PubCode;

        [DataMember]
        public string PubName
        {
            get
            {
                return _PubName;
            }
            set
            {
                _PubName = value;
            }
        }
        string _PubName;

        [DataMember]
        public string ResFlag
        {
            get
            {
                return _ResFlag;
            }
            set
            {
                _ResFlag = value;
            }
        }
        string _ResFlag;

        [DataMember]
        public string PatientID
        {
            get
            {
                return _PatientID;
            }
            set
            {
                _PatientID = value;
            }
        }
        string _PatientID;

        [DataMember]
        public string UserFingerMark
        {
            get
            {
                return _UserFingerMark;
            }
            set
            {
                _UserFingerMark = value;
            }
        }
        string _UserFingerMark;

        [DataMember]
        public string Invoice
        {
            get
            {
                return _Invoice;
            }
            set
            {
                _Invoice = value;
            }
        }
        string _Invoice;

        [DataMember]
        public int? DocMobileStatus
        {
            get
            {
                return _DocMobileStatus;
            }
            set
            {
                _DocMobileStatus = value;
            }
        }
        int? _DocMobileStatus;

    }

    [DataContract]
    public class Ris_List
    {
        [DataMember]
        public bool IsChecked
        {
            get
            {
                return _IsChecked;
            }
            set
            {
                _IsChecked = value;
            }
        }
        bool _IsChecked;
        
        [DataMember]
        public int ApplyNo
        {
            get
            {
                return _ApplyNo;
            }
            set
            {
                _ApplyNo = value;
            }
        }
        int _ApplyNo;

        [DataMember]
        public string TechNo
        {
            get
            {
                return _TechNo;
            }
            set
            {
                _TechNo = value;
            }
        }
        string _TechNo;

        [DataMember]
        public DateTime ExecTime
        {
            get
            {
                return _ExecTime;
            }
            set
            {
                _ExecTime = value;
            }
        }
        DateTime _ExecTime;

       [DataMember]
        public string LabelID
        {
            get
            {
                return _LabelID;
            }
            set
            {
                _LabelID = value;
            }
        }
        string _LabelID;

       [DataMember]
        public int? TechPatID
        {
            get
            {
                return _TechPatID;
            }
            set
            {
                _TechPatID = value;
            }
        }
        int? _TechPatID;

       [DataMember]
        public string PatName
        {
            get
            {
                return _PatName;
            }
            set
            {
                _PatName = value;
            }
        }
        string _PatName;

       [DataMember]
        public string PatNameSpell
        {
            get
            {
                return _PatNameSpell;
            }
            set
            {
                _PatNameSpell = value;
            }
        }
        string _PatNameSpell;

      [DataMember]
        public int? PatientID
        {
            get
            {
                return _PatientID;
            }
            set
            {
                _PatientID = value;
            }
        }
        int? _PatientID;

      [DataMember]
        public int? CureNo
        {
            get
            {
                return _CureNo;
            }
            set
            {
                _CureNo = value;
            }
        }
        int? _CureNo;

      [DataMember]
        public string CardNo
        {
            get
            {
                return _CardNo;
            }
            set
            {
                _CardNo = value;
            }
        }
        string _CardNo;

       [DataMember]
        public string HospNo
        {
            get
            {
                return _HospNo;
            }
            set
            {
                _HospNo = value;
            }
        }
        string _HospNo;

      [DataMember]
        public string Sex
        {
            get
            {
                return _Sex;
            }
            set
            {
                _Sex = value;
            }
        }
        string _Sex;

      [DataMember]
        public decimal? Age
        {
            get
            {
                return _Age;
            }
            set
            {
                _Age = value;
            }
        }
        decimal? _Age;

    [DataMember]
        public string AgeUnit
        {
            get
            {
                return _AgeUnit;
            }
            set
            {
                _AgeUnit = value;
            }
        }
        string _AgeUnit;

      [DataMember]
        public DateTime Birthday
        {
            get
            {
                return _Birthday;
            }
            set
            {
                _Birthday = value;
            }
        }
        DateTime _Birthday;

        [DataMember]
        public string Career
        {
            get
            {
                return _Career;
            }
            set
            {
                _Career = value;
            }
        }
        string _Career;

        [DataMember]
        public string Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                _Phone = value;
            }
        }
        string _Phone;

        [DataMember]
        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
            }
        }
        string _Address;

        [DataMember]
        public string Zip
        {
            get
            {
                return _Zip;
            }
            set
            {
                _Zip = value;
            }
        }
        string _Zip;

        [DataMember]
        public string Nation
        {
            get
            {
                return _Nation;
            }
            set
            {
                _Nation = value;
            }
        }
        string _Nation;

        [DataMember]
        public string IDNum
        {
            get
            {
                return _IDNum;
            }
            set
            {
                _IDNum = value;
            }
        }
        string _IDNum;

        [DataMember]
        public string ChargeType
        {
            get
            {
                return _ChargeType;
            }
            set
            {
                _ChargeType = value;
            }
        }
        string _ChargeType;

        [DataMember]
        public string ChargeTypeName
        {
            get
            {
                return _ChargeTypeName;
            }
            set
            {
                _ChargeTypeName = value;
            }
        }
        string _ChargeTypeName;

       [DataMember]
        public string ApplyDept
        {
            get
            {
                return _ApplyDept;
            }
            set
            {
                _ApplyDept = value;
            }
        }
        string _ApplyDept;

        [DataMember]
        public string ApplyDeptName
        {
            get
            {
                return _ApplyDeptName;
            }
            set
            {
                _ApplyDeptName = value;
            }
        }
        string _ApplyDeptName;

        [DataMember]
        public string Ward
        {
            get
            {
                return _Ward;
            }
            set
            {
                _Ward = value;
            }
        }
        string _Ward;

        [DataMember]
        public string WardName
        {
            get
            {
                return _WardName;
            }
            set
            {
                _WardName = value;
            }
        }
        string _WardName;

       [DataMember]
        public string BedNo
        {
            get
            {
                return _BedNo;
            }
            set
            {
                _BedNo = value;
            }
        }
        string _BedNo;

       [DataMember]
        public string WardOrReg
        {
            get
            {
                return _WardOrReg;
            }
            set
            {
                _WardOrReg = value;
            }
        }
        string _WardOrReg;

        [DataMember]
        public DateTime ApplyTime
        {
            get
            {
                return _ApplyTime;
            }
            set
            {
                _ApplyTime = value;
            }
        }
        DateTime _ApplyTime;

       [DataMember]
        public string BriefCase
        {
            get
            {
                return _BriefCase;
            }
            set
            {
                _BriefCase = value;
            }
        }
        string _BriefCase;

        [DataMember]
        public string ClinicDesc
        {
            get
            {
                return _ClinicDesc;
            }
            set
            {
                _ClinicDesc = value;
            }
        }
        string _ClinicDesc;

        [DataMember]
        public DateTime AcceptTime
        {
            get
            {
                return _AcceptTime;
            }
            set
            {
                _AcceptTime = value;
            }
        }
        DateTime _AcceptTime;

        [DataMember]
        public string ApplyDoctor
        {
            get
            {
                return _ApplyDoctor;
            }
            set
            {
                _ApplyDoctor = value;
            }
        }
        string _ApplyDoctor;

        [DataMember]
        public string ApplyDoctorName
        {
            get
            {
                return _ApplyDoctorName;
            }
            set
            {
                _ApplyDoctorName = value;
            }
        }
        string _ApplyDoctorName;

       [DataMember]
        public string Accepter
        {
            get
            {
                return _Accepter;
            }
            set
            {
                _Accepter = value;
            }
        }
        string _Accepter;

        [DataMember]
        public string AccepterName
        {
            get
            {
                return _AccepterName;
            }
            set
            {
                _AccepterName = value;
            }
        }
        string _AccepterName;

        [DataMember]
        public string ExecDoctor1
        {
            get
            {
                return _ExecDoctor1;
            }
            set
            {
                _ExecDoctor1 = value;
            }
        }
        string _ExecDoctor1;

        [DataMember]
        public string ExecDoctor1Name
        {
            get
            {
                return _ExecDoctor1Name;
            }
            set
            {
                _ExecDoctor1Name = value;
            }
        }
        string _ExecDoctor1Name;

        [DataMember]
        public string ExecDoctor2
        {
            get
            {
                return _ExecDoctor2;
            }
            set
            {
                _ExecDoctor2 = value;
            }
        }
        string _ExecDoctor2;

        [DataMember]
        public string ExecDoctor2Name
        {
            get
            {
                return _ExecDoctor2Name;
            }
            set
            {
                _ExecDoctor2Name = value;
            }
        }
        string _ExecDoctor2Name;

        [DataMember]
        public string QualityDoctor
        {
            get
            {
                return _QualityDoctor;
            }
            set
            {
                _QualityDoctor = value;
            }
        }
        string _QualityDoctor;

        [DataMember]
        public string QualityDoctorName
        {
            get
            {
                return _QualityDoctorName;
            }
            set
            {
                _QualityDoctorName = value;
            }
        }
        string _QualityDoctorName;

        [DataMember]
        public string VerifyDoctor
        {
            get
            {
                return _VerifyDoctor;
            }
            set
            {
                _VerifyDoctor = value;
            }
        }
        string _VerifyDoctor;

        [DataMember]
        public string VerifyDoctorName
        {
            get
            {
                return _VerifyDoctorName;
            }
            set
            {
                _VerifyDoctorName = value;
            }
        }
        string _VerifyDoctorName;

        [DataMember]
        public string ReportDoctor
        {
            get
            {
                return _ReportDoctor;
            }
            set
            {
                _ReportDoctor = value;
            }
        }
        string _ReportDoctor;

        [DataMember]
        public string ReportDoctorName
        {
            get
            {
                return _ReportDoctorName;
            }
            set
            {
                _ReportDoctorName = value;
            }
        }
        string _ReportDoctorName;

        [DataMember]
        public string ReportWriter
        {
            get
            {
                return _ReportWriter;
            }
            set
            {
                _ReportWriter = value;
            }
        }
        string _ReportWriter;

       [DataMember]
        public string ReportWriterName
        {
            get
            {
                return _ReportWriterName;
            }
            set
            {
                _ReportWriterName = value;
            }
        }
        string _ReportWriterName;

        [DataMember]
        public string FinallyEditDoctor
        {
            get
            {
                return _FinallyEditDoctor;
            }
            set
            {
                _FinallyEditDoctor = value;
            }
        }
        string _FinallyEditDoctor;

        [DataMember]
        public string FinallyEditDoctorName
        {
            get
            {
                return _FinallyEditDoctorName;
            }
            set
            {
                _FinallyEditDoctorName = value;
            }
        }
        string _FinallyEditDoctorName;

        [DataMember]
        public DateTime FinallyEditTime
        {
            get
            {
                return _FinallyEditTime;
            }
            set
            {
                _FinallyEditTime = value;
            }
        }
        DateTime _FinallyEditTime;

        [DataMember]
        public DateTime AuditingTime
        {
            get
            {
                return _AuditingTime;
            }
            set
            {
                _AuditingTime = value;
            }
        }
        DateTime _AuditingTime;

        [DataMember]
        public DateTime PublicTime
        {
            get
            {
                return _PublicTime;
            }
            set
            {
                _PublicTime = value;
            }
        }
        DateTime _PublicTime;

        [DataMember]
        public int? ReportID
        {
            get
            {
                return _ReportID;
            }
            set
            {
                _ReportID = value;
            }
        }
        int? _ReportID;

        [DataMember]
        public DateTime ReportTime
        {
            get
            {
                return _ReportTime;
            }
            set
            {
                _ReportTime = value;
            }
        }
        DateTime _ReportTime;

        [DataMember]
        public string Instrument
        {
            get
            {
                return _Instrument;
            }
            set
            {
                _Instrument = value;
            }
        }
        string _Instrument;

        [DataMember]
        public string OrgApplyNo
        {
            get
            {
                return _OrgApplyNo;
            }
            set
            {
                _OrgApplyNo = value;
            }
        }
        string _OrgApplyNo;

        [DataMember]
        public int Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }
        int _Status;

        [DataMember]
        public int? LockFlag
        {
            get
            {
                return _LockFlag;
            }
            set
            {
                _LockFlag = value;
            }
        }
        int? _LockFlag;

        [DataMember]
        public string Locker
        {
            get
            {
                return _Locker;
            }
            set
            {
                _Locker = value;
            }
        }
        string _Locker;

        [DataMember]
        public int? PrintFlag
        {
            get
            {
                return _PrintFlag;
            }
            set
            {
                _PrintFlag = value;
            }
        }
        int? _PrintFlag;

        [DataMember]
        public int? DeleteFlag
        {
            get
            {
                return _DeleteFlag;
            }
            set
            {
                _DeleteFlag = value;
            }
        }
        int? _DeleteFlag;

        [DataMember]
        public int? ChargeFlag
        {
            get
            {
                return _ChargeFlag;
            }
            set
            {
                _ChargeFlag = value;
            }
        }
        int? _ChargeFlag;

        [DataMember]
        public short? HavingImages
        {
            get
            {
                return _HavingImages;
            }
            set
            {
                _HavingImages = value;
            }
        }
        short? _HavingImages;

        [DataMember]
        public string EngName
        {
            get
            {
                return _EngName;
            }
            set
            {
                _EngName = value;
            }
        }
        string _EngName;

        [DataMember]
        public string EquipmentID
        {
            get
            {
                return _EquipmentID;
            }
            set
            {
                _EquipmentID = value;
            }
        }
        string _EquipmentID;

        [DataMember]
        public string Invoice
        {
            get
            {
                return _Invoice;
            }
            set
            {
                _Invoice = value;
            }
        }
        string _Invoice;

       [DataMember]
        public DateTime ImageTime
        {
            get
            {
                return _ImageTime;
            }
            set
            {
                _ImageTime = value;
            }
        }
        DateTime _ImageTime;

        [DataMember]
        public int? DpSign
        {
            get
            {
                return _DpSign;
            }
            set
            {
                _DpSign = value;;
            }
        }
        int? _DpSign;

        [DataMember]
        public string dpsuggest
        {
            get
            {
                return _dpsuggest;
            }
            set
            {
                _dpsuggest = value;
            }
        }
        string _dpsuggest;

        [DataMember]
        public DateTime ExamTime
        {
            get
            {
                return _ExamTime;
            }
            set
            {
                _ExamTime = value;
            }
        }
        DateTime _ExamTime;

        [DataMember]
        public int? BookCenterSID
        {
            get
            {
                return _BookCenterSID;
            }
            set
            {
                _BookCenterSID = value;
            }
        }
        int? _BookCenterSID;

        [DataMember]
        public int? CrisisFlag
        {
            get
            {
                return _CrisisFlag;
            }
            set
            {
                _CrisisFlag = value;
            }
        }
        int? _CrisisFlag;

        [DataMember]
        public int? SurveySign
        {
            get
            {
                return _SurveySign;
            }
            set
            {
                _SurveySign = value;
            }
        }
        int? _SurveySign;

        [DataMember]
        public string SurveySuggest
        {
            get
            {
                return _SurveySuggest;
            }
            set
            {
                _SurveySuggest = value;
            }
        }
        string _SurveySuggest;

       [DataMember]
        public int? JCBZ
        {
            get
            {
                return _JCBZ;
            }
            set
            {
                _JCBZ = value;
            }
        }
        int? _JCBZ;
        [DataMember]
        public string ItemName
        {
            get
            {
                return _ItemName;
            }
            set
            {
                _ItemName = value;
            }
        }
        string _ItemName;
        [DataMember]
        public string ItemResult
        {
            get
            {
                return _ItemResult;
            }
            set
            {
                _ItemResult = value;
            }
        }
        string _ItemResult;

    }

    [DataContract]
    public class Lis_Result
    {
        [DataMember]
        public int SerialNo
        {
            get
            {
                return _SerialNo;
            }
            set
            {
                _SerialNo = value;
            }
        }
        int _SerialNo;

        [DataMember]
        public int InstID
        {
            get
            {
                return _InstID;
            }
            set
            {
                _InstID = value;
            }
        }
        int _InstID;

        [DataMember]
        public int ApplyNo
        {
            get
            {
                return _ApplyNo;
            }
            set
            {
                _ApplyNo = value;
            }
        }
        int _ApplyNo;

        [DataMember]
        public DateTime ResultTime
        {
            get
            {
                return _ResultTime;
            }
            set
            {
                _ResultTime = value;
            }
        }
        DateTime _ResultTime;

        [DataMember]
        public string ItemCode
        {
            get
            {
                return _ItemCode;
            }
            set
            {
                _ItemCode = value;
            }
        }
        string _ItemCode;

        [DataMember]
        public decimal? ResultValue
        {
            get
            {
                return _ResultValue;
            }
            set
            {
                _ResultValue = value;
            }
        }
        decimal? _ResultValue;

        [DataMember]
        public string ResultChar
        {
            get
            {
                return _ResultChar;
            }
            set
            {
                _ResultChar = value;
            }
        }
        string _ResultChar;

       [DataMember]
        public string HintInfo
        {
            get
            {
                return _HintInfo;
            }
            set
            {
                _HintInfo = value;
            }
        }
        string _HintInfo;

        [DataMember]
        public string DisplayFlag
        {
            get
            {
                return _DisplayFlag;
            }
            set
            {
                _DisplayFlag = value;
            }
        }
        string _DisplayFlag;

        [DataMember]
        public decimal? ODResult
        {
            get
            {
                return _ODResult;
            }
            set
            {
                _ODResult = value;
            }
        }
        decimal? _ODResult;

        [DataMember]
        public decimal? CutOffValue
        {
            get
            {
                return _CutOffValue;
            }
            set
            {
                _CutOffValue = value;
            }
        }
        decimal? _CutOffValue;

        [DataMember]
        public decimal? SCOValue
        {
            get
            {
                return _SCOValue;
            }
            set
            {
                _SCOValue = value;
            }
        }
        decimal? _SCOValue;

        [DataMember]
        public int? TestInstId
        {
            get
            {
                return _TestInstId;
            }
            set
            {
                _TestInstId = value;
            }
        }
        int? _TestInstId;

        [DataMember]
        public string RESULT
        {
            get
            {
                return _RESULT;
            }
            set
            {
                _RESULT = value;
            }
        }
        string _RESULT;

        [DataMember]
        public string HIGHLOWFLAG
        {
            get
            {
                return _HIGHLOWFLAG;
            }
            set
            {
                _HIGHLOWFLAG = value;
            }
        }
        string _HIGHLOWFLAG;

        [DataMember]
        public string REFERENCERANGE
        {
            get
            {
                return _REFERENCERANGE;
            }
            set
            {
                _REFERENCERANGE = value;
            }
        }
        string _REFERENCERANGE;

        [DataMember]
        public string UNIT
        {
            get
            {
                return _UNIT;
            }
            set
            {
                _UNIT = value;
            }
        }
        string _UNIT;

        [DataMember]
        public string RESULTTYPE
        {
            get
            {
                return _RESULTTYPE;
            }
            set
            {
                _RESULTTYPE = value;
            }
        }
        string _RESULTTYPE;

        [DataMember]
        public int? Redo
        {
            get
            {
                return _Redo;
            }
            set
            {
                _Redo = value;
            }
        }
        int? _Redo;

    }


}
