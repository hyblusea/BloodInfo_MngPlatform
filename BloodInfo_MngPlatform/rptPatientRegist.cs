using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BloodInfo_MngPlatform.Models;

namespace BloodInfo_MngPlatform
{
    public partial class rptPatientRegist : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPatientRegist(PATIENT_REGIST reg )
        {
            InitializeComponent();

            bindingSource1.DataSource = reg;
        }

    }
}
