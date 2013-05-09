using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace CommonServiceLibrary.LISDBModels
{
    public partial class LIS_LIST : LISDB.Record<LIS_LIST>
    {
        public bool? IsChecked { get; set; }
    }
}

