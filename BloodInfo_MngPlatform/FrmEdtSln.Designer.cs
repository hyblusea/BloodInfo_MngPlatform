﻿namespace BloodInfo_MngPlatform
{
    partial class FrmEdtSln
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEdtSln));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barExit = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.sharedImageCollection1 = new DevExpress.Utils.SharedImageCollection(this.components);
            this.rENALREPLACEMENTTHERAPYHIBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.FRIST_ANADateEdit = new DevExpress.XtraEditors.DateEdit();
            this.SCD_ANADateEdit = new DevExpress.XtraEditors.DateEdit();
            this.CCRTDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.SLNMemoEdit = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForFRIST_ANA = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForSCD_ANA = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForCCRT = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForSLN = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharedImageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharedImageCollection1.ImageSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rENALREPLACEMENTTHERAPYHIBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FRIST_ANADateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FRIST_ANADateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SCD_ANADateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SCD_ANADateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CCRTDateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CCRTDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SLNMemoEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForFRIST_ANA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSCD_ANA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForCCRT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSLN)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControl1);
            this.barManager1.Form = this;
            this.barManager1.Images = this.sharedImageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSave,
            this.barExit});
            this.barManager1.MaxItemId = 2;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barExit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.Text = "Tools";
            // 
            // barSave
            // 
            this.barSave.Caption = "保存并关闭";
            this.barSave.Id = 0;
            this.barSave.ImageIndex = 59;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barExit
            // 
            this.barExit.Caption = "取消";
            this.barExit.Id = 1;
            this.barExit.ImageIndex = 53;
            this.barExit.Name = "barExit";
            this.barExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barExit_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(709, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 315);
            this.barDockControlBottom.Size = new System.Drawing.Size(709, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 284);
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl1.Location = new System.Drawing.Point(709, 31);
            this.barDockControl1.Size = new System.Drawing.Size(0, 284);
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
            // rENALREPLACEMENTTHERAPYHIBindingSource
            // 
            this.rENALREPLACEMENTTHERAPYHIBindingSource.DataSource = typeof(BloodInfo_MngPlatform.Models.RENAL_REPLACEMENT_THERAPY_HI);
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.FRIST_ANADateEdit);
            this.dataLayoutControl1.Controls.Add(this.SCD_ANADateEdit);
            this.dataLayoutControl1.Controls.Add(this.CCRTDateEdit);
            this.dataLayoutControl1.Controls.Add(this.SLNMemoEdit);
            this.dataLayoutControl1.DataSource = this.rENALREPLACEMENTTHERAPYHIBindingSource;
            this.dataLayoutControl1.Location = new System.Drawing.Point(62, 39);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(584, 262);
            this.dataLayoutControl1.TabIndex = 5;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // FRIST_ANADateEdit
            // 
            this.FRIST_ANADateEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.rENALREPLACEMENTTHERAPYHIBindingSource, "FRIST_ANA", true));
            this.FRIST_ANADateEdit.EditValue = null;
            this.FRIST_ANADateEdit.Location = new System.Drawing.Point(91, 12);
            this.FRIST_ANADateEdit.MenuManager = this.barManager1;
            this.FRIST_ANADateEdit.Name = "FRIST_ANADateEdit";
            this.FRIST_ANADateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.FRIST_ANADateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.FRIST_ANADateEdit.Size = new System.Drawing.Size(481, 20);
            this.FRIST_ANADateEdit.StyleController = this.dataLayoutControl1;
            this.FRIST_ANADateEdit.TabIndex = 4;
            // 
            // SCD_ANADateEdit
            // 
            this.SCD_ANADateEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.rENALREPLACEMENTTHERAPYHIBindingSource, "SCD_ANA", true));
            this.SCD_ANADateEdit.EditValue = null;
            this.SCD_ANADateEdit.Location = new System.Drawing.Point(91, 36);
            this.SCD_ANADateEdit.MenuManager = this.barManager1;
            this.SCD_ANADateEdit.Name = "SCD_ANADateEdit";
            this.SCD_ANADateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SCD_ANADateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.SCD_ANADateEdit.Size = new System.Drawing.Size(481, 20);
            this.SCD_ANADateEdit.StyleController = this.dataLayoutControl1;
            this.SCD_ANADateEdit.TabIndex = 5;
            // 
            // CCRTDateEdit
            // 
            this.CCRTDateEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.rENALREPLACEMENTTHERAPYHIBindingSource, "CCRT", true));
            this.CCRTDateEdit.EditValue = null;
            this.CCRTDateEdit.Location = new System.Drawing.Point(91, 60);
            this.CCRTDateEdit.MenuManager = this.barManager1;
            this.CCRTDateEdit.Name = "CCRTDateEdit";
            this.CCRTDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CCRTDateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.CCRTDateEdit.Size = new System.Drawing.Size(481, 20);
            this.CCRTDateEdit.StyleController = this.dataLayoutControl1;
            this.CCRTDateEdit.TabIndex = 6;
            // 
            // SLNMemoEdit
            // 
            this.SLNMemoEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.rENALREPLACEMENTTHERAPYHIBindingSource, "SLN", true));
            this.SLNMemoEdit.Location = new System.Drawing.Point(91, 84);
            this.SLNMemoEdit.MenuManager = this.barManager1;
            this.SLNMemoEdit.Name = "SLNMemoEdit";
            this.SLNMemoEdit.Size = new System.Drawing.Size(481, 166);
            this.SLNMemoEdit.StyleController = this.dataLayoutControl1;
            this.SLNMemoEdit.TabIndex = 7;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(584, 262);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AllowDrawBackground = false;
            this.layoutControlGroup2.CustomizationFormText = "autoGeneratedGroup0";
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForFRIST_ANA,
            this.ItemForSCD_ANA,
            this.ItemForCCRT,
            this.ItemForSLN});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "autoGeneratedGroup0";
            this.layoutControlGroup2.Size = new System.Drawing.Size(564, 242);
            this.layoutControlGroup2.Text = "autoGeneratedGroup0";
            // 
            // ItemForFRIST_ANA
            // 
            this.ItemForFRIST_ANA.Control = this.FRIST_ANADateEdit;
            this.ItemForFRIST_ANA.CustomizationFormText = "首次血透时间:";
            this.ItemForFRIST_ANA.Location = new System.Drawing.Point(0, 0);
            this.ItemForFRIST_ANA.Name = "ItemForFRIST_ANA";
            this.ItemForFRIST_ANA.Size = new System.Drawing.Size(564, 24);
            this.ItemForFRIST_ANA.Text = "首次血透时间:";
            this.ItemForFRIST_ANA.TextSize = new System.Drawing.Size(76, 14);
            // 
            // ItemForSCD_ANA
            // 
            this.ItemForSCD_ANA.Control = this.SCD_ANADateEdit;
            this.ItemForSCD_ANA.CustomizationFormText = "腹透时间:";
            this.ItemForSCD_ANA.Location = new System.Drawing.Point(0, 24);
            this.ItemForSCD_ANA.Name = "ItemForSCD_ANA";
            this.ItemForSCD_ANA.Size = new System.Drawing.Size(564, 24);
            this.ItemForSCD_ANA.Text = "腹透时间:";
            this.ItemForSCD_ANA.TextSize = new System.Drawing.Size(76, 14);
            // 
            // ItemForCCRT
            // 
            this.ItemForCCRT.Control = this.CCRTDateEdit;
            this.ItemForCCRT.CustomizationFormText = "CRRT时间:";
            this.ItemForCCRT.Location = new System.Drawing.Point(0, 48);
            this.ItemForCCRT.Name = "ItemForCCRT";
            this.ItemForCCRT.Size = new System.Drawing.Size(564, 24);
            this.ItemForCCRT.Text = "CCRT时间:";
            this.ItemForCCRT.TextSize = new System.Drawing.Size(76, 14);
            // 
            // ItemForSLN
            // 
            this.ItemForSLN.Control = this.SLNMemoEdit;
            this.ItemForSLN.CustomizationFormText = "治疗方案:";
            this.ItemForSLN.Location = new System.Drawing.Point(0, 72);
            this.ItemForSLN.Name = "ItemForSLN";
            this.ItemForSLN.Size = new System.Drawing.Size(564, 170);
            this.ItemForSLN.Text = "治疗方案:";
            this.ItemForSLN.TextSize = new System.Drawing.Size(76, 14);
            // 
            // FrmEdtSln
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 315);
            this.Controls.Add(this.dataLayoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControl1);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmEdtSln";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑既往肾代替治疗信息";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharedImageCollection1.ImageSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharedImageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rENALREPLACEMENTTHERAPYHIBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FRIST_ANADateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FRIST_ANADateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SCD_ANADateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SCD_ANADateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CCRTDateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CCRTDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SLNMemoEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForFRIST_ANA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSCD_ANA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForCCRT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSLN)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barExit;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.Utils.SharedImageCollection sharedImageCollection1;
        private System.Windows.Forms.BindingSource rENALREPLACEMENTTHERAPYHIBindingSource;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraEditors.DateEdit FRIST_ANADateEdit;
        private DevExpress.XtraEditors.DateEdit SCD_ANADateEdit;
        private DevExpress.XtraEditors.DateEdit CCRTDateEdit;
        private DevExpress.XtraEditors.MemoEdit SLNMemoEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem ItemForFRIST_ANA;
        private DevExpress.XtraLayout.LayoutControlItem ItemForSCD_ANA;
        private DevExpress.XtraLayout.LayoutControlItem ItemForCCRT;
        private DevExpress.XtraLayout.LayoutControlItem ItemForSLN;
    }
}