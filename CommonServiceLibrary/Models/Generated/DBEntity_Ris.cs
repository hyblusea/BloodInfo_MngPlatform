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
        public string ITEMCODE
        {
            get
            {
                return _ITEMCODE;
            }
            set
            {
                _ITEMCODE = value;
                MarkColumnModified("ITEMCODE");
            }
        }
        string _ITEMCODE;


        public string ITEM_RESULT
        {
            get
            {
                return _ITEM_RESULT;
            }
            set
            {
                _ITEM_RESULT = value;
                MarkColumnModified("ITEM_RESULT");
            }
        }
        string _ITEM_RESULT;


    }
   
}

