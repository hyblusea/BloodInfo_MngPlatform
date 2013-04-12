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
using System.IO;
using System.Drawing.Drawing2D;

namespace BloodInfo_MngPlatform
{
    public partial class UcDialysis : UserControl
    {
        public delegate void ChangeEventHandler(DateTime dt, int peroid, decimal floorId, decimal areaId, object ptId, decimal? status);
        public event ChangeEventHandler ChangeEvt;

        Database db;
        int _frmType;

        //List<PATIENT_REGIST> _lstReg;
        List<PATIENT_BASEINFO> _lstBaseInfo;

        DateTime _dt = new DateTime();
        decimal _floorId;
        int _peroid;
        decimal _areaId;

        decimal _scheduleID;
        public decimal ScheduleID
        {
            get { return _scheduleID; }
            set { _scheduleID = value;}
        }

        // 床位号
        string _no;
        public string No
        {
            get { return _no; }
            set { _no = value; lblNo.Text = value.ToString(); }
        }

        // 机器型号
        decimal? _model;
        public decimal? Model
        {
            get { return _model; }
            set
            {
                _model = value;
                lblModel.Text = db.ExecuteScalar<string>("select DSP_MEMBER from VALUE_CODE where VALUE_MEMBER = @0", value);
            }
        }

        // 设备类型
        decimal? _m_Type;
        public decimal? M_Type
        {
            get { return _m_Type; }
            set
            {
                _m_Type = value;
                lblMachType.Text = db.ExecuteScalar<string>("select GROUPNAME from VALUE_GROUP where ID = @0", value);
            }
        }

        // 机器编码
        string _m_Sn;
        public string SN
        {
            get { return _m_Sn; }
            set
            {
                _m_Sn = value;
                lblSn.Text = value;
            }
        }

        // 图片
        public byte[] Pict
        {
            set {
                MemoryStream ms = new MemoryStream(value);
                pictureBox1.Image = Image.FromStream(ms);
            }
        }

        decimal _info_ID;
        public decimal InfoID
        {
            get { return _info_ID; }
            set
            {
                _info_ID = value;
            }
        }

        decimal? _status; 
        public decimal? Status
        {
            get { return _status; }
            set
            {
                _status = value;
                if (value != null)
                {
                    // 使用中, 清洗, 维护
                    if (value != 73 && value != 75 && value != 77 && _frmType == 0)
                        searchLookUpEdit1.Enabled = true;
                    else 
                        searchLookUpEdit1.Enabled = false;

                    // 已预定
                    if (value == 76 && _frmType == 1)
                        btnCheckIn.Enabled = true;
                    else
                        btnCheckIn.Enabled = false;

                    // 已签到
                    if (value == 81)
                    {
                        btnCheckIn.Enabled = false;
                        SearchPt = false;
                    }

                    //cbxStatus.SelectedValue = value;
                    cbxStatus1.EditValue = value;
                }
            }
        }

        decimal? _ptNo;
        public decimal? PtNo
        {
            get { return _ptNo; }
            set
            {
                _ptNo = value;
                if (value != null && value != 0)
                {
                    lblName.Text = _lstBaseInfo.Where(p => p.ID == value).FirstOrDefault<PATIENT_BASEINFO>().NAME;
                    searchLookUpEdit1.EditValueChanged -= searchLookUpEdit1_EditValueChanged;
                    searchLookUpEdit1.EditValue = value;
                    searchLookUpEdit1.EditValueChanged += searchLookUpEdit1_EditValueChanged;                    
                }
                else
                    lblName.Text = "";
            }
        }

        DateTime _checkInTm;
        public DateTime CheckInTime
        {
            set { _checkInTm = value; lblChckInTm.Text = value.ToString("HH:mm"); }
        }

        bool _statusOp;
        public bool StatusOP
        {
            set { _statusOp = value; cbxStatus1.Enabled = value; }
        }

        bool _checkIn;
        public bool CheckIn
        {
            set { _checkIn = value; btnCheckIn.Enabled = value; }
        }

        bool _searchPt;
        public bool SearchPt
        {
            set { _searchPt = value; searchLookUpEdit1.Enabled = value; }
        }

        bool _maintence;
        public bool Maintence
        {
            set { _maintence = value; btnMaintence.Enabled = value; }
        }

        bool _idel;
        public bool Idel
        {
            set { _idel = value; btnIdle.Enabled = value; }
        }

        DateTime _reservTm;
        public DateTime ReservTm
        {
           set { _reservTm = value; if( value != null) lblResevTm.Text = value.ToString("MM-dd HH:mm"); }
        }

        public UcDialysis(List<PATIENT_REGIST> lst, List<PATIENT_BASEINFO> lstBaseInfo, DateTime dt, decimal floorId, decimal areaId, int peroid, int iFrmType)
        {
            InitializeComponent();

            //string sPwd = Des.Decrypt(ClsFrmMng.KEY, ConfigurationManager.AppSettings["DbPwd"]);
            db = new Database("XE");

            vALUECODEBindingSource.DataSource = db.Fetch<VALUE_CODE>("where groupname = @0", new object[] { 23 });
            //_lstReg = lst;
            _lstBaseInfo = lstBaseInfo;

            //pATIENTREGISTBindingSource.DataSource = _lstReg;
            pATIENTBASEINFOBindingSource.DataSource = lstBaseInfo;

            _dt = dt;
            _floorId = floorId;
            _peroid = peroid;
            _areaId = areaId;

            _frmType = iFrmType;

            searchLookUpEdit1.EditValueChanged += searchLookUpEdit1_EditValueChanged;
        }

        // 患者预约
        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("是否保存新的预约信息.", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DateTime dt = DateTime.Now;
                if (searchLookUpEdit1.EditValue != null)
                {
                    var o = db.FirstOrDefault<MACHINE_SCHEDULE>("where SCHEDULE_TIME = @0 and pt_id = @1", new object[] { _dt, Convert.ToDecimal(searchLookUpEdit1.EditValue) });
                    if (o != null)
                    {
                        XtraMessageBox.Show("该患者在 " + dt.ToString("yyyy-MM-dd") + string.Format(" 已预定了床位 . 楼层ID: {0}, 区域ID: {1}, 床位号: {2}, 患者ID: {3}", o.FLOOR_ID, o.AREA_ID, o.BED_NO, o.PT_ID), "错误提示");
                        searchLookUpEdit1.EditValueChanged -= searchLookUpEdit1_EditValueChanged;
                        searchLookUpEdit1.EditValue = null;
                        searchLookUpEdit1.EditValueChanged += searchLookUpEdit1_EditValueChanged;
                        return;
                    }
                }

                //Int64 regId = -1;
                MACHINE_SCHEDULE ms = new MACHINE_SCHEDULE();

                if (searchLookUpEdit1View.GetFocusedRow() != null)
                {
                    object o1 = searchLookUpEdit1View.GetFocusedRow();
                    //ms.REG_ID = (Int64)((PATIENT_REGIST)o1).ID;
                    //ms.PT_ID = (Int64)((PATIENT_BASEINFO)o1).ID;
                }
                
                ms.SCHEDULE_TIME = _dt;
                ms.FLOOR_ID = _floorId;
                ms.AREA_ID = _areaId;
                ms.SCHEEDULE_PERIOD = _peroid;
                ms.RESERVATION = dt;
                ms.MACHINE_INFO_ID = _info_ID;
                ms.BED_NO = No;
                Status = 76;
                ms.MACHINE_STATUS = 76;

                if (searchLookUpEdit1.EditValue != null)
                    ms.PT_ID = Convert.ToDecimal(searchLookUpEdit1.EditValue);
                else
                {
                    ms.PT_ID = null;
                    Status = 74;
                    ms.MACHINE_STATUS = 74;
                    ms.RESERVATION = new DateTime();
                }

                if (ScheduleID == 0)
                {

                    ScheduleID = Convert.ToDecimal(ms.Insert());
                    //ScheduleID = db.ExecuteScalar<decimal>("select ID from MACHINE_SCHEDULE where floor_id = @0 and area_id = @1 and SCHEDULE_TIME = @2 and SCHEEDULE_PERIOD = @3 and MACHINE_NO = @4 ",
                    //    new object[] { _floorId, _areaId, _dt, _peroid, No });
                }
                else
                {
                    ms.ID = ScheduleID;
                    ms.Update();
                }

                if (searchLookUpEdit1.EditValue != null && searchLookUpEdit1.EditValue.ToString() != "")
                    PtNo = Convert.ToDecimal(searchLookUpEdit1.EditValue);
                else
                    PtNo = null;
                lblResevTm.Text = dt.ToString("MM-dd HH:mm");

                if (ChangeEvt != null)
                {
                    ChangeEvt(_dt, _peroid, _floorId, _areaId, searchLookUpEdit1.EditValue, Status);
                }
                
                
            }
            else
            {
                searchLookUpEdit1.EditValueChanged -= searchLookUpEdit1_EditValueChanged;
                searchLookUpEdit1.EditValue = null;
                searchLookUpEdit1.EditValueChanged += searchLookUpEdit1_EditValueChanged;
            }
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("是否将状态修改为[已签到] ?", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (ScheduleID == 0)
                {
                    XtraMessageBox.Show("获取 ScheduleID 失败, 请联系开发人员.");
                    return;
                }
                else
                {
                    try
                    {
                        // 判断用户是否已经注册挂号
                        var regInfo = db.Fetch<PATIENT_REGIST>("where status = 0 and base_info_id = @0 order by ID asc", _ptNo);
                        if (regInfo.Count() == 0)
                        {
                            XtraMessageBox.Show("该患者尚未完成挂号登记, 请确认.");
                            return;
                        }

                        DateTime dt = DateTime.Now;
                        db.Execute("update MACHINE_SCHEDULE set  MACHINE_STATUS = 81, REG_ID = @0, CHCKINTM = @1 where ID = @2", new object[] {regInfo[0].ID, dt,  ScheduleID });
                        Status = 81;
                        CheckInTime = dt;
                    }
                    catch (Exception err)
                    {
                        XtraMessageBox.Show("未知异常: " + err.Message);
                    }
                }
            }
        }

        private void btnMaintence_Click(object sender, EventArgs e)
        {
            if (Status == 77)
            {
                XtraMessageBox.Show("目前状态已经是维护状态.");
                return;
            }

            if (XtraMessageBox.Show("是否将状态修改为[维护] ?", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                if ( Status != 76 || ( Status == 76 && XtraMessageBox.Show("该设备在该时段已被预定, 确实要将状态置为 [维护] 吗 ?", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                {
                    try
                    {
                        if (ScheduleID == 0)
                        {
                            MACHINE_SCHEDULE ms = new MACHINE_SCHEDULE();
                            ms.SCHEDULE_TIME = _dt;
                            ms.FLOOR_ID = _floorId;
                            ms.AREA_ID = _areaId;
                            ms.SCHEEDULE_PERIOD = _peroid;
                            ms.BED_NO = No;
                            ms.MACHINE_INFO_ID = _info_ID;
                            ms.MACHINE_STATUS = 77;
                            ms.Insert();

                            ScheduleID = db.ExecuteScalar<decimal>("select ID from MACHINE_SCHEDULE where floor_id = @0 and area_id = @1 and SCHEDULE_TIME = @2 and SCHEEDULE_PERIOD = @3 and BED_NO = @4 ",
                                new object[] { _floorId, _areaId, _dt, _peroid, No });
                        }
                        else
                            db.Execute("update MACHINE_SCHEDULE set  MACHINE_STATUS = 77 where ID = @0", new object[] { ScheduleID });
                        Status = 77;
                    }
                    catch (Exception err)
                    {
                        XtraMessageBox.Show("未知异常: " + err.Message);
                    }
                }
            }
        }

        private void btnIdle_Click(object sender, EventArgs e)
        {
            if (Status == 74)
            {
                XtraMessageBox.Show("目前状态已经是空闲状态.");
                return;
            }

            if (XtraMessageBox.Show("是否将状态修改为[空闲] ?", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (Status != 76 || (Status == 76 && XtraMessageBox.Show("该设备在该时段已被预定, 确实要将状态置为 [维护] 吗 ?", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                {
                    try
                    {
                        if (ScheduleID == 0)
                        {
                            MACHINE_SCHEDULE ms = new MACHINE_SCHEDULE();
                            ms.SCHEDULE_TIME = _dt;
                            ms.FLOOR_ID = _floorId;
                            ms.AREA_ID = _areaId;
                            ms.SCHEEDULE_PERIOD = _peroid;
                            ms.BED_NO = No;
                            ms.MACHINE_INFO_ID = _info_ID;
                            ms.MACHINE_STATUS = 74;
                            ms.Insert();

                            ScheduleID = db.ExecuteScalar<decimal>("select ID from MACHINE_SCHEDULE where floor_id = @0 and area_id = @1 and SCHEDULE_TIME = @2 and SCHEEDULE_PERIOD = @3 and MACHINE_NO = @4 ",
                                new object[] { _floorId, _areaId, _dt, _peroid, No });
                        }
                        else
                            db.Execute("update MACHINE_SCHEDULE set  MACHINE_STATUS = 74 where ID = @0", new object[] { ScheduleID });
                        Status = 74;
                    }
                    catch (Exception err)
                    {
                        XtraMessageBox.Show("未知异常: " + err.Message);
                    }
                }
            }
        }


    }
}
