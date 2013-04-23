using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PetaPoco;

namespace BloodInfo_MngPlatform
{
    public partial class _FrmAes : DevExpress.XtraEditors.XtraForm
    {
        public _FrmAes()
        {
            InitializeComponent();  
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            memoEdit2.Text = AES.Encrypt(AES.KEY, memoEdit1.Text);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            memoEdit2.Text = AES.Decrypt(AES.KEY, memoEdit1.Text);
        }
    }
}