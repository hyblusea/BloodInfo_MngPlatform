using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace CommonServiceLibrary.RISDBModels
{
    public partial class RIS_LIST : RISDB.Record<RIS_LIST>
    {
        public bool? IsChecked { get; set; }
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
}

