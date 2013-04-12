using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BloodInfo_MngPlatform
{
    public partial class FrmBackReason : DevExpress.XtraEditors.XtraForm
    {
        public string Reason { get; set; }

        public FrmBackReason()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Reason = textEdit1.Text.ToString();
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();

            if (iData.GetDataPresent(DataFormats.Text))
            {
                textEdit1.Text = (String)iData.GetData(DataFormats.Text);
            }
        }
    }
}