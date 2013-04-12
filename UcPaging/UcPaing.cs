using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UcPaging
{
    public partial class UcPaing : UserControl
    {
        public delegate void PageChangedHandler(long curPage, int dspLen);
        public event PageChangedHandler PageChanged;

        long _curPage;
        public long curPage
        {
            get { return _curPage; }
            set { _curPage = value; lblPage.Text = string.Format("{0}/{1}", _curPage, _totalPage); }
        }

        long _totalPage;
        public long totalPage
        {
            get { return _totalPage; }
            set { _totalPage = value; lblPage.Text = string.Format("{0}/{1}", _curPage, _totalPage); }
        }

        public int dspLenght { get; set; }

        public long _recoredCnt;
        public long recordCnt
            {
                get { return _recoredCnt; }
                set { _recoredCnt = value; lblRecoredCount.Text = value.ToString(); } 
        }

        public UcPaing()
        {
            InitializeComponent();
            
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (curPage == 1)
                return;
            else
            {
                curPage = 1;
                if (PageChanged != null)
                    PageChanged(curPage, dspLenght);
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (curPage == 1)
                return;
            else
            {
                curPage -= 1;
                if (PageChanged != null)
                    PageChanged(curPage, dspLenght);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (curPage == totalPage)
                return;
            else
            {
                curPage += 1;
                if (PageChanged != null)
                    PageChanged(curPage, dspLenght);
            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (curPage == totalPage)
                return;
            else
            {
                curPage = totalPage;
                if (PageChanged != null)
                    PageChanged(curPage, dspLenght);
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(txtPage.Text) <= totalPage)
                {
                    curPage = Convert.ToInt32(txtPage.Text);
                    if (PageChanged != null)
                        PageChanged(curPage, dspLenght);
                }
            }
            catch { };
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            curPage = 1;
            if (PageChanged != null)
                PageChanged(curPage, dspLenght);
        }

        private void txtPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                btnGo_Click(null, null);
        }
    }
}
