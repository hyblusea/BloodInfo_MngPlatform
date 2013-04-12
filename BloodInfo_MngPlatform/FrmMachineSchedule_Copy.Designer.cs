namespace BloodInfo_MngPlatform
{
    partial class FrmMachineSchedule_Copy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMachineSchedule_Copy));
            this.wizardControl1 = new DevExpress.XtraWizard.WizardControl();
            this.welcomeWizardPage1 = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.dateNavigator1 = new DevExpress.XtraScheduler.DateNavigator();
            this.wizardPage1 = new DevExpress.XtraWizard.WizardPage();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblToDays = new DevExpress.XtraEditors.LabelControl();
            this.lblDays = new DevExpress.XtraEditors.LabelControl();
            this.lblStart = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateNavigator2 = new DevExpress.XtraScheduler.DateNavigator();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lblSplitDays = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardControl1.SuspendLayout();
            this.welcomeWizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigator1)).BeginInit();
            this.wizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigator2)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.Controls.Add(this.welcomeWizardPage1);
            this.wizardControl1.Controls.Add(this.wizardPage1);
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.welcomeWizardPage1,
            this.wizardPage1});
            this.wizardControl1.Size = new System.Drawing.Size(695, 360);
            this.wizardControl1.Text = "透析预约信息复制向导";
            this.wizardControl1.WizardStyle = DevExpress.XtraWizard.WizardStyle.WizardAero;
            // 
            // welcomeWizardPage1
            // 
            this.welcomeWizardPage1.Controls.Add(this.dateNavigator1);
            this.welcomeWizardPage1.Name = "welcomeWizardPage1";
            this.welcomeWizardPage1.Size = new System.Drawing.Size(635, 198);
            this.welcomeWizardPage1.Text = "1. 请选择待复制信息的时间范围";
            this.welcomeWizardPage1.PageCommit += new System.EventHandler(this.welcomeWizardPage1_PageCommit);
            // 
            // dateNavigator1
            // 
            this.dateNavigator1.DateTime = new System.DateTime(2013, 2, 3, 0, 0, 0, 0);
            this.dateNavigator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateNavigator1.HotDate = null;
            this.dateNavigator1.Location = new System.Drawing.Point(0, 0);
            this.dateNavigator1.Name = "dateNavigator1";
            this.dateNavigator1.Size = new System.Drawing.Size(635, 198);
            this.dateNavigator1.TabIndex = 3;
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.labelControl5);
            this.wizardPage1.Controls.Add(this.lblSplitDays);
            this.wizardPage1.Controls.Add(this.labelControl2);
            this.wizardPage1.Controls.Add(this.lblToDays);
            this.wizardPage1.Controls.Add(this.lblDays);
            this.wizardPage1.Controls.Add(this.lblStart);
            this.wizardPage1.Controls.Add(this.labelControl3);
            this.wizardPage1.Controls.Add(this.labelControl1);
            this.wizardPage1.Controls.Add(this.dateNavigator2);
            this.wizardPage1.Controls.Add(this.shapeContainer1);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(635, 198);
            this.wizardPage1.Text = "2. 请选择待填充信息的起始日期";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(356, 86);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "总天数:";
            // 
            // lblToDays
            // 
            this.lblToDays.Location = new System.Drawing.Point(417, 128);
            this.lblToDays.Name = "lblToDays";
            this.lblToDays.Size = new System.Drawing.Size(51, 14);
            this.lblToDays.TabIndex = 1;
            this.lblToDays.Text = "lblToDays";
            // 
            // lblDays
            // 
            this.lblDays.Location = new System.Drawing.Point(415, 86);
            this.lblDays.Name = "lblDays";
            this.lblDays.Size = new System.Drawing.Size(36, 14);
            this.lblDays.TabIndex = 1;
            this.lblDays.Text = "lblDays";
            // 
            // lblStart
            // 
            this.lblStart.Location = new System.Drawing.Point(415, 59);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(38, 14);
            this.lblStart.TabIndex = 1;
            this.lblStart.Text = "lblStart";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(284, 128);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(112, 14);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "待填充信息时间范围:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(284, 59);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(112, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "被复制信息时间范围:";
            // 
            // dateNavigator2
            // 
            this.dateNavigator2.DateTime = new System.DateTime(2013, 2, 2, 0, 0, 0, 0);
            this.dateNavigator2.HotDate = null;
            this.dateNavigator2.Location = new System.Drawing.Point(45, 0);
            this.dateNavigator2.Multiselect = false;
            this.dateNavigator2.Name = "dateNavigator2";
            this.dateNavigator2.Size = new System.Drawing.Size(197, 183);
            this.dateNavigator2.TabIndex = 0;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(635, 198);
            this.shapeContainer1.TabIndex = 2;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.BorderColor = System.Drawing.Color.Silver;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 273;
            this.lineShape1.X2 = 638;
            this.lineShape1.Y1 = 111;
            this.lineShape1.Y2 = 111;
            // 
            // lblSplitDays
            // 
            this.lblSplitDays.Location = new System.Drawing.Point(417, 169);
            this.lblSplitDays.Name = "lblSplitDays";
            this.lblSplitDays.Size = new System.Drawing.Size(59, 14);
            this.lblSplitDays.TabIndex = 1;
            this.lblSplitDays.Text = "lblSplitDays";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(320, 169);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(76, 14);
            this.labelControl5.TabIndex = 1;
            this.labelControl5.Text = "时间区域间隔:";
            // 
            // lineShape2
            // 
            this.lineShape2.BorderColor = System.Drawing.Color.Silver;
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 273;
            this.lineShape2.X2 = 638;
            this.lineShape2.Y1 = 158;
            this.lineShape2.Y2 = 158;
            // 
            // FrmMachineSchedule_Copy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 360);
            this.Controls.Add(this.wizardControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMachineSchedule_Copy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "预约信息复制";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardControl1.ResumeLayout(false);
            this.welcomeWizardPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigator1)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigator2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl wizardControl1;
        private DevExpress.XtraWizard.WelcomeWizardPage welcomeWizardPage1;
        private DevExpress.XtraWizard.WizardPage wizardPage1;
        private DevExpress.XtraScheduler.DateNavigator dateNavigator1;
        private DevExpress.XtraScheduler.DateNavigator dateNavigator2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblDays;
        private DevExpress.XtraEditors.LabelControl lblStart;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblToDays;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl lblSplitDays;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
    }
}