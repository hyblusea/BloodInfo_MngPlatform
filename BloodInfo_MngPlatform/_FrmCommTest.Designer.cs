namespace BloodInfo_MngPlatform
{
    partial class _FrmCommTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_FrmCommTest));
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.Table1 = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tbData = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.group2 = new DevExpress.XtraEditors.GroupControl();
            this.txtSocketSvr = new System.Windows.Forms.TextBox();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.simpleButton7 = new DevExpress.XtraEditors.SimpleButton();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.simpleButton6 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.cbxIP = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl6 = new DevExpress.XtraEditors.GroupControl();
            this.txtSendToSvr = new System.Windows.Forms.TextBox();
            this.btnSendToSvr = new DevExpress.XtraEditors.SimpleButton();
            this.btnConnToSvr = new DevExpress.XtraEditors.SimpleButton();
            this.txtSvrIP = new System.Windows.Forms.TextBox();
            this.txtSrvPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.shapeContainer3 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape3 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.Table1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.group2)).BeginInit();
            this.group2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).BeginInit();
            this.groupControl6.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.Table1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1028, 491);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.Table1,
            this.xtraTabPage2,
            this.xtraTabPage1});
            // 
            // Table1
            // 
            this.Table1.Controls.Add(this.splitContainer1);
            this.Table1.Controls.Add(this.toolStrip1);
            this.Table1.Controls.Add(this.groupControl1);
            this.Table1.Image = global::BloodInfo_MngPlatform.Properties.Resources._20130307020756735_easyicon_cn_16;
            this.Table1.Name = "Table1";
            this.Table1.Size = new System.Drawing.Size(1022, 460);
            this.Table1.Text = "串口调试器";
            this.Table1.Paint += new System.Windows.Forms.PaintEventHandler(this.Table1_Paint);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(147, 52);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbData);
            this.splitContainer1.Size = new System.Drawing.Size(274, 185);
            this.splitContainer1.SplitterDistance = 145;
            this.splitContainer1.TabIndex = 16;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(145, 185);
            this.treeView1.TabIndex = 0;
            // 
            // tbData
            // 
            this.tbData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbData.Location = new System.Drawing.Point(0, 0);
            this.tbData.Multiline = true;
            this.tbData.Name = "tbData";
            this.tbData.ReadOnly = true;
            this.tbData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbData.Size = new System.Drawing.Size(125, 185);
            this.tbData.TabIndex = 14;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1022, 25);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::BloodInfo_MngPlatform.Properties.Resources.arrow;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(49, 22);
            this.toolStripButton1.Text = "开始";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(33, 22);
            this.toolStripButton2.Text = "停止";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.simpleButton4);
            this.groupControl1.Controls.Add(this.simpleButton2);
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.shapeContainer1);
            this.groupControl1.Location = new System.Drawing.Point(47, 256);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(776, 113);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "串口设置";
            // 
            // simpleButton4
            // 
            this.simpleButton4.Location = new System.Drawing.Point(26, 78);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(75, 23);
            this.simpleButton4.TabIndex = 20;
            this.simpleButton4.Text = "获取串口";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(194, 78);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 21;
            this.simpleButton2.Text = "关闭";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(110, 78);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 20;
            this.simpleButton1.Text = "打开/开始";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(2, 22);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(772, 89);
            this.shapeContainer1.TabIndex = 22;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineShape1.BorderColor = System.Drawing.SystemColors.AppWorkspace;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 12;
            this.lineShape1.X2 = 765;
            this.lineShape1.Y1 = 45;
            this.lineShape1.Y2 = 45;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.group2);
            this.xtraTabPage2.Controls.Add(this.groupControl4);
            this.xtraTabPage2.Image = global::BloodInfo_MngPlatform.Properties.Resources._20130307021031884_easyicon_cn_16;
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1022, 460);
            this.xtraTabPage2.Text = "Tcp Server";
            // 
            // group2
            // 
            this.group2.Controls.Add(this.txtSocketSvr);
            this.group2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.group2.Location = new System.Drawing.Point(0, 111);
            this.group2.Name = "group2";
            this.group2.Size = new System.Drawing.Size(1022, 349);
            this.group2.TabIndex = 2;
            this.group2.Text = "客户端消息";
            // 
            // txtSocketSvr
            // 
            this.txtSocketSvr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSocketSvr.Location = new System.Drawing.Point(2, 22);
            this.txtSocketSvr.Multiline = true;
            this.txtSocketSvr.Name = "txtSocketSvr";
            this.txtSocketSvr.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSocketSvr.Size = new System.Drawing.Size(1018, 325);
            this.txtSocketSvr.TabIndex = 1;
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.checkEdit1);
            this.groupControl4.Controls.Add(this.simpleButton7);
            this.groupControl4.Controls.Add(this.txtPort);
            this.groupControl4.Controls.Add(this.label2);
            this.groupControl4.Controls.Add(this.simpleButton6);
            this.groupControl4.Controls.Add(this.simpleButton5);
            this.groupControl4.Controls.Add(this.cbxIP);
            this.groupControl4.Controls.Add(this.label1);
            this.groupControl4.Controls.Add(this.shapeContainer2);
            this.groupControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl4.Location = new System.Drawing.Point(0, 0);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(1022, 111);
            this.groupControl4.TabIndex = 0;
            this.groupControl4.Text = "网络信息";
            // 
            // checkEdit1
            // 
            this.checkEdit1.EditValue = true;
            this.checkEdit1.Location = new System.Drawing.Point(293, 82);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "显示日志";
            this.checkEdit1.Size = new System.Drawing.Size(75, 19);
            this.checkEdit1.TabIndex = 24;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // simpleButton7
            // 
            this.simpleButton7.Location = new System.Drawing.Point(192, 78);
            this.simpleButton7.Name = "simpleButton7";
            this.simpleButton7.Size = new System.Drawing.Size(75, 23);
            this.simpleButton7.TabIndex = 6;
            this.simpleButton7.Text = "停止";
            this.simpleButton7.Click += new System.EventHandler(this.simpleButton7_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(366, 33);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 22);
            this.txtPort.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(325, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port:";
            // 
            // simpleButton6
            // 
            this.simpleButton6.Location = new System.Drawing.Point(108, 78);
            this.simpleButton6.Name = "simpleButton6";
            this.simpleButton6.Size = new System.Drawing.Size(75, 23);
            this.simpleButton6.TabIndex = 2;
            this.simpleButton6.Text = "监听";
            this.simpleButton6.Click += new System.EventHandler(this.simpleButton6_Click);
            // 
            // simpleButton5
            // 
            this.simpleButton5.Location = new System.Drawing.Point(24, 78);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(75, 23);
            this.simpleButton5.TabIndex = 2;
            this.simpleButton5.Text = "获取网卡";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // cbxIP
            // 
            this.cbxIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxIP.FormattingEnabled = true;
            this.cbxIP.Location = new System.Drawing.Point(49, 33);
            this.cbxIP.Name = "cbxIP";
            this.cbxIP.Size = new System.Drawing.Size(252, 22);
            this.cbxIP.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // shapeContainer2
            // 
            this.shapeContainer2.Location = new System.Drawing.Point(2, 22);
            this.shapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer2.Name = "shapeContainer2";
            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2});
            this.shapeContainer2.Size = new System.Drawing.Size(1018, 87);
            this.shapeContainer2.TabIndex = 3;
            this.shapeContainer2.TabStop = false;
            // 
            // lineShape2
            // 
            this.lineShape2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineShape2.BorderColor = System.Drawing.SystemColors.AppWorkspace;
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 14;
            this.lineShape2.X2 = 1013;
            this.lineShape2.Y1 = 45;
            this.lineShape2.Y2 = 45;
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.groupControl6);
            this.xtraTabPage1.Image = global::BloodInfo_MngPlatform.Properties.Resources._20130307021059678_easyicon_cn_16;
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1022, 460);
            this.xtraTabPage1.Text = "Tcp Client";
            // 
            // groupControl6
            // 
            this.groupControl6.Controls.Add(this.txtSendToSvr);
            this.groupControl6.Controls.Add(this.btnSendToSvr);
            this.groupControl6.Controls.Add(this.btnConnToSvr);
            this.groupControl6.Controls.Add(this.txtSvrIP);
            this.groupControl6.Controls.Add(this.txtSrvPort);
            this.groupControl6.Controls.Add(this.label3);
            this.groupControl6.Controls.Add(this.label4);
            this.groupControl6.Controls.Add(this.shapeContainer3);
            this.groupControl6.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl6.Location = new System.Drawing.Point(0, 0);
            this.groupControl6.Name = "groupControl6";
            this.groupControl6.Size = new System.Drawing.Size(1022, 111);
            this.groupControl6.TabIndex = 3;
            this.groupControl6.Text = "网络信息";
            // 
            // txtSendToSvr
            // 
            this.txtSendToSvr.Location = new System.Drawing.Point(186, 80);
            this.txtSendToSvr.Name = "txtSendToSvr";
            this.txtSendToSvr.Size = new System.Drawing.Size(842, 22);
            this.txtSendToSvr.TabIndex = 5;
            // 
            // btnSendToSvr
            // 
            this.btnSendToSvr.Location = new System.Drawing.Point(105, 80);
            this.btnSendToSvr.Name = "btnSendToSvr";
            this.btnSendToSvr.Size = new System.Drawing.Size(75, 23);
            this.btnSendToSvr.TabIndex = 6;
            this.btnSendToSvr.Text = "发送";
            this.btnSendToSvr.Click += new System.EventHandler(this.btnSendToSvr_Click);
            // 
            // btnConnToSvr
            // 
            this.btnConnToSvr.Location = new System.Drawing.Point(24, 80);
            this.btnConnToSvr.Name = "btnConnToSvr";
            this.btnConnToSvr.Size = new System.Drawing.Size(75, 23);
            this.btnConnToSvr.TabIndex = 6;
            this.btnConnToSvr.Text = "连接";
            this.btnConnToSvr.Click += new System.EventHandler(this.btnConnToSvr_Click);
            // 
            // txtSvrIP
            // 
            this.txtSvrIP.Location = new System.Drawing.Point(49, 33);
            this.txtSvrIP.Name = "txtSvrIP";
            this.txtSvrIP.Size = new System.Drawing.Size(158, 22);
            this.txtSvrIP.TabIndex = 5;
            // 
            // txtSrvPort
            // 
            this.txtSrvPort.Location = new System.Drawing.Point(274, 33);
            this.txtSrvPort.Name = "txtSrvPort";
            this.txtSrvPort.Size = new System.Drawing.Size(100, 22);
            this.txtSrvPort.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(233, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "Port:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "IP:";
            // 
            // shapeContainer3
            // 
            this.shapeContainer3.Location = new System.Drawing.Point(2, 22);
            this.shapeContainer3.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer3.Name = "shapeContainer3";
            this.shapeContainer3.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape3});
            this.shapeContainer3.Size = new System.Drawing.Size(1018, 87);
            this.shapeContainer3.TabIndex = 3;
            this.shapeContainer3.TabStop = false;
            // 
            // lineShape3
            // 
            this.lineShape3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineShape3.BorderColor = System.Drawing.SystemColors.AppWorkspace;
            this.lineShape3.Name = "lineShape2";
            this.lineShape3.X1 = 14;
            this.lineShape3.X2 = 1013;
            this.lineShape3.Y1 = 45;
            this.lineShape3.Y2 = 45;
            // 
            // serialPort1
            // 
            this.serialPort1.RtsEnable = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "20130307020633664_easyicon_cn_32.png");
            // 
            // _FrmCommTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 491);
            this.Controls.Add(this.xtraTabControl1);
            this.DoubleBuffered = true;
            this.Name = "_FrmCommTest";
            this.Text = "通讯调试工具";
            this.Load += new System.EventHandler(this._FrmCommTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.Table1.ResumeLayout(false);
            this.Table1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.group2)).EndInit();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            this.groupControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).EndInit();
            this.groupControl6.ResumeLayout(false);
            this.groupControl6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage Table1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.TextBox tbData;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private System.Windows.Forms.ComboBox cbxIP;
        private System.Windows.Forms.Label label1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private DevExpress.XtraEditors.SimpleButton simpleButton7;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton simpleButton6;
        private DevExpress.XtraEditors.GroupControl groupControl6;
        private System.Windows.Forms.TextBox txtSendToSvr;
        private DevExpress.XtraEditors.SimpleButton btnSendToSvr;
        private DevExpress.XtraEditors.SimpleButton btnConnToSvr;
        private System.Windows.Forms.TextBox txtSvrIP;
        private System.Windows.Forms.TextBox txtSrvPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer3;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape3;
        private DevExpress.XtraEditors.GroupControl group2;
        private System.Windows.Forms.TextBox txtSocketSvr;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ImageList imageList1;
    }
}