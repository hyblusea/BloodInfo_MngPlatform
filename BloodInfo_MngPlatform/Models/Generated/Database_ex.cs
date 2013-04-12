using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace BloodInfo_MngPlatform.Models
{    
    public partial class PATIENT_BASEINFO : XEDB.Record<PATIENT_BASEINFO>
    {
        [ResultColumn]
        public List<PATIENT_REGIST> PATIENT_REG {get; set;}

    }

    public partial class ATH_CONTROL_ENABLE : XEDB.Record<ATH_CONTROL_ENABLE>
    {
        public bool NeedDelete = false;
    }

    public partial class CONSUMABLES_WAREHOUSE : XEDB.Record<CONSUMABLES_WAREHOUSE>
    {
        public decimal? OutCount { get; set; }
        public decimal? InCount { get; set; }
        public decimal? Surplus { get; set; }
    }

    public partial class CONSUMABLES_LOG : XEDB.Record<CONSUMABLES_LOG>
    {
        [ResultColumn]
        public List<CONSUMABLES_LOG1> CONSUMABLES_LOG1 { get; set; }
    }

    public partial class CONSUMABLES_REQUEST_LST : XEDB.Record<CONSUMABLES_REQUEST_LST>
    {
        [Comments("使用量")] 
        public decimal? OutCount { get; set; }
    }

    public partial class CONSUMABLES_REQUEST_LST_USELOG : XEDB.Record<CONSUMABLES_REQUEST_LST_USELOG>
    {
        public decimal? SURPLUS { get; set; }
    }
}

