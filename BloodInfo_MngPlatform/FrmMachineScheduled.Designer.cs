namespace BloodInfo_MngPlatform
{
    partial class FrmMachineScheduled
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMachineScheduled));
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::BloodInfo_MngPlatform.WaitForm1), true, true);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.vALUECODEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.barEditItem2 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemLookUpEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.vALUECODEBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnSearch = new DevExpress.XtraBars.BarButtonItem();
            this.btnCopy = new DevExpress.XtraBars.BarButtonItem();
            this.btnReloadBaseInfo = new DevExpress.XtraBars.BarButtonItem();
            this.sharedImageCollection1 = new DevExpress.Utils.SharedImageCollection(this.components);
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemGridLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vALUECODEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vALUECODEBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharedImageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharedImageCollection1.ImageSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Collapsed = true;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl3);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(966, 438);
            this.splitContainerControl1.SplitterPosition = 235;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.radioGroup1);
            this.groupControl1.Controls.Add(this.monthCalendar1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(235, 438);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "选择预定日期";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(9, 223);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "上午"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "下午"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "晚间")});
            this.radioGroup1.Size = new System.Drawing.Size(220, 28);
            this.radioGroup1.TabIndex = 3;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(9, 31);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 2;
            // 
            // groupControl3
            // 
            this.groupControl3.Appearance.BackColor = System.Drawing.Color.White;
            this.groupControl3.Appearance.Options.UseBackColor = true;
            this.groupControl3.Controls.Add(this.barDockControlLeft);
            this.groupControl3.Controls.Add(this.barDockControlRight);
            this.groupControl3.Controls.Add(this.barDockControlBottom);
            this.groupControl3.Controls.Add(this.barDockControlTop);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(0, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(726, 438);
            this.groupControl3.TabIndex = 0;
            this.groupControl3.Text = "透析机布局示意图";
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(2, 53);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 383);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(724, 53);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 383);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(2, 436);
            this.barDockControlBottom.Size = new System.Drawing.Size(722, 0);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(2, 22);
            this.barDockControlTop.Size = new System.Drawing.Size(722, 31);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this.groupControl3;
            this.barManager1.Images = this.sharedImageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStaticItem1,
            this.barEditItem1,
            this.barStaticItem2,
            this.barEditItem2,
            this.btnSearch,
            this.btnCopy,
            this.btnReloadBaseInfo});
            this.barManager1.MaxItemId = 10;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1,
            this.repositoryItemGridLookUpEdit1,
            this.repositoryItemLookUpEdit2,
            this.repositoryItemTextEdit1,
            this.repositoryItemLookUpEdit3});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEditItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEditItem2),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnSearch, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnCopy, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnReloadBaseInfo, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.Text = "Tools";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "楼层:";
            this.barStaticItem1.Id = 0;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "barEditItem1";
            this.barEditItem1.Edit = this.repositoryItemLookUpEdit2;
            this.barEditItem1.Id = 3;
            this.barEditItem1.Name = "barEditItem1";
            this.barEditItem1.Width = 75;
            this.barEditItem1.EditValueChanged += new System.EventHandler(this.barEditItem1_EditValueChanged);
            // 
            // repositoryItemLookUpEdit2
            // 
            this.repositoryItemLookUpEdit2.AutoHeight = false;
            this.repositoryItemLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit2.DataSource = this.vALUECODEBindingSource;
            this.repositoryItemLookUpEdit2.DisplayMember = "DSP_MEMBER";
            this.repositoryItemLookUpEdit2.Name = "repositoryItemLookUpEdit2";
            this.repositoryItemLookUpEdit2.ValueMember = "VALUE_MEMBER";
            // 
            // vALUECODEBindingSource
            // 
            this.vALUECODEBindingSource.DataSource = typeof(BloodInfo_MngPlatform.Models.VALUE_CODE);
            // 
            // barStaticItem2
            // 
            this.barStaticItem2.Caption = "区域:";
            this.barStaticItem2.Id = 5;
            this.barStaticItem2.Name = "barStaticItem2";
            this.barStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barEditItem2
            // 
            this.barEditItem2.Caption = "barEditItem2";
            this.barEditItem2.Edit = this.repositoryItemLookUpEdit3;
            this.barEditItem2.Id = 6;
            this.barEditItem2.Name = "barEditItem2";
            this.barEditItem2.Width = 75;
            // 
            // repositoryItemLookUpEdit3
            // 
            this.repositoryItemLookUpEdit3.AutoHeight = false;
            this.repositoryItemLookUpEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit3.DataSource = this.vALUECODEBindingSource1;
            this.repositoryItemLookUpEdit3.DisplayMember = "DSP_MEMBER";
            this.repositoryItemLookUpEdit3.Name = "repositoryItemLookUpEdit3";
            this.repositoryItemLookUpEdit3.ValueMember = "VALUE_MEMBER";
            // 
            // vALUECODEBindingSource1
            // 
            this.vALUECODEBindingSource1.DataSource = typeof(BloodInfo_MngPlatform.Models.VALUE_CODE);
            // 
            // btnSearch
            // 
            this.btnSearch.Caption = "查询";
            this.btnSearch.Id = 7;
            this.btnSearch.ImageIndex = 35;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSearch_ItemClick);
            // 
            // btnCopy
            // 
            this.btnCopy.Caption = "预约信息复制...";
            this.btnCopy.Id = 8;
            this.btnCopy.ImageIndex = 57;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCopy_ItemClick);
            // 
            // btnReloadBaseInfo
            // 
            this.btnReloadBaseInfo.Caption = "重载患者基本信息";
            this.btnReloadBaseInfo.Id = 9;
            this.btnReloadBaseInfo.ImageIndex = 70;
            this.btnReloadBaseInfo.Name = "btnReloadBaseInfo";
            this.btnReloadBaseInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnReloadBaseInfo_ItemClick);
            // 
            // sharedImageCollection1
            // 
            // 
            // 
            // 
            this.sharedImageCollection1.ImageSource.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("sharedImageCollection1.ImageSource.ImageStream")));
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._1, "_1", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 0);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(0, "_1");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._10, "_10", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 1);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(1, "_10");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._100, "_100", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 2);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(2, "_100");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._101, "_101", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 3);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(3, "_101");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._102, "_102", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 4);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(4, "_102");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._103, "_103", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 5);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(5, "_103");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._104, "_104", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 6);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(6, "_104");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._105, "_105", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 7);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(7, "_105");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._106, "_106", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 8);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(8, "_106");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._107, "_107", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 9);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(9, "_107");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._108, "_108", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 10);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(10, "_108");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._11, "_11", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 11);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(11, "_11");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._12, "_12", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 12);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(12, "_12");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._13, "_13", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 13);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(13, "_13");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._14, "_14", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 14);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(14, "_14");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._15, "_15", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 15);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(15, "_15");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._16, "_16", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 16);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(16, "_16");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._17, "_17", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 17);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(17, "_17");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._18, "_18", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 18);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(18, "_18");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._19, "_19", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 19);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(19, "_19");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._2, "_2", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 20);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(20, "_2");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._20, "_20", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 21);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(21, "_20");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._21, "_21", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 22);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(22, "_21");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._22, "_22", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 23);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(23, "_22");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._23, "_23", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 24);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(24, "_23");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._24, "_24", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 25);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(25, "_24");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._25, "_25", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 26);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(26, "_25");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._26, "_26", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 27);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(27, "_26");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._27, "_27", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 28);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(28, "_27");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._28, "_28", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 29);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(29, "_28");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._29, "_29", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 30);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(30, "_29");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._3, "_3", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 31);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(31, "_3");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._30, "_30", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 32);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(32, "_30");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._31, "_31", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 33);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(33, "_31");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._32, "_32", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 34);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(34, "_32");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._33, "_33", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 35);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(35, "_33");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._34, "_34", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 36);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(36, "_34");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._35, "_35", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 37);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(37, "_35");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._36, "_36", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 38);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(38, "_36");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._37, "_37", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 39);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(39, "_37");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._38, "_38", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 40);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(40, "_38");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._39, "_39", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 41);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(41, "_39");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._4, "_4", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 42);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(42, "_4");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._40, "_40", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 43);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(43, "_40");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._41, "_41", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 44);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(44, "_41");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._42, "_42", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 45);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(45, "_42");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._43, "_43", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 46);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(46, "_43");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._44, "_44", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 47);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(47, "_44");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._45, "_45", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 48);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(48, "_45");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._46, "_46", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 49);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(49, "_46");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._47, "_47", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 50);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(50, "_47");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._48, "_48", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 51);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(51, "_48");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._49, "_49", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 52);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(52, "_49");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._5, "_5", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 53);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(53, "_5");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._50, "_50", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 54);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(54, "_50");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._51, "_51", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 55);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(55, "_51");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._52, "_52", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 56);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(56, "_52");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._53, "_53", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 57);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(57, "_53");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._54, "_54", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 58);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(58, "_54");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._55, "_55", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 59);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(59, "_55");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._56, "_56", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 60);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(60, "_56");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._57, "_57", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 61);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(61, "_57");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._58, "_58", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 62);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(62, "_58");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._59, "_59", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 63);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(63, "_59");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._6, "_6", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 64);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(64, "_6");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._60, "_60", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 65);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(65, "_60");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._61, "_61", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 66);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(66, "_61");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._62, "_62", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 67);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(67, "_62");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._63, "_63", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 68);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(68, "_63");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._64, "_64", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 69);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(69, "_64");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._65, "_65", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 70);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(70, "_65");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._66, "_66", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 71);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(71, "_66");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._67, "_67", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 72);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(72, "_67");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._68, "_68", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 73);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(73, "_68");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._69, "_69", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 74);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(74, "_69");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._7, "_7", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 75);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(75, "_7");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._70, "_70", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 76);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(76, "_70");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._71, "_71", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 77);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(77, "_71");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._72, "_72", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 78);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(78, "_72");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._73, "_73", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 79);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(79, "_73");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._74, "_74", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 80);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(80, "_74");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._75, "_75", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 81);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(81, "_75");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._76, "_76", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 82);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(82, "_76");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._77, "_77", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 83);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(83, "_77");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._78, "_78", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 84);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(84, "_78");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._79, "_79", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 85);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(85, "_79");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._8, "_8", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 86);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(86, "_8");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._80, "_80", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 87);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(87, "_80");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._81, "_81", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 88);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(88, "_81");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._82, "_82", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 89);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(89, "_82");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._83, "_83", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 90);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(90, "_83");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._84, "_84", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 91);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(91, "_84");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._85, "_85", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 92);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(92, "_85");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._86, "_86", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 93);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(93, "_86");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._87, "_87", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 94);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(94, "_87");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._88, "_88", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 95);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(95, "_88");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._89, "_89", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 96);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(96, "_89");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._9, "_9", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 97);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(97, "_9");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._90, "_90", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 98);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(98, "_90");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._91, "_91", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 99);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(99, "_91");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._92, "_92", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 100);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(100, "_92");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._93, "_93", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 101);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(101, "_93");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._94, "_94", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 102);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(102, "_94");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._95, "_95", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 103);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(103, "_95");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._96, "_96", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 104);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(104, "_96");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._97, "_97", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 105);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(105, "_97");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._98, "_98", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 106);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(106, "_98");
            this.sharedImageCollection1.ImageSource.InsertImage(global::BloodInfo_MngPlatform.Properties.Resources._99, "_99", typeof(global::BloodInfo_MngPlatform.Properties.Resources), 107);
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(107, "_99");
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(108, "Add.png");
            this.sharedImageCollection1.ParentControl = this;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            // 
            // repositoryItemGridLookUpEdit1
            // 
            this.repositoryItemGridLookUpEdit1.AutoHeight = false;
            this.repositoryItemGridLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemGridLookUpEdit1.Name = "repositoryItemGridLookUpEdit1";
            this.repositoryItemGridLookUpEdit1.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // FrmMachineScheduled
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 438);
            this.Controls.Add(this.splitContainerControl1);
            this.DoubleBuffered = true;
            this.Name = "FrmMachineScheduled";
            this.Text = "透析机预约";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMachineScheduled_FormClosed);
            this.Load += new System.EventHandler(this.FrmMachineScheduled_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vALUECODEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vALUECODEBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharedImageCollection1.ImageSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharedImageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
        private System.Windows.Forms.BindingSource vALUECODEBindingSource;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.XtraBars.BarEditItem barEditItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit3;
        private System.Windows.Forms.BindingSource vALUECODEBindingSource1;
        private DevExpress.Utils.SharedImageCollection sharedImageCollection1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraBars.BarButtonItem btnSearch;
        private DevExpress.XtraBars.BarButtonItem btnCopy;
        private DevExpress.XtraBars.BarButtonItem btnReloadBaseInfo;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;

    }
}