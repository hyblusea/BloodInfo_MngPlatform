namespace BloodInfo_MngPlatform
{
    partial class UcDialysis
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcDialysis));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.vALUECODEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.searchLookUpEdit1 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.pATIENTBASEINFOBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSEX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colID_CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pATIENTREGISTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblName = new System.Windows.Forms.Label();
            this.btnCheckIn = new DevExpress.XtraEditors.SimpleButton();
            this.btnMaintence = new DevExpress.XtraEditors.SimpleButton();
            this.btnIdle = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            this.lblChckInTm = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblResevTm = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSn = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblMachType = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblModel = new System.Windows.Forms.Label();
            this.cbxStatus1 = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vALUECODEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pATIENTBASEINFOBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pATIENTREGISTBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxStatus1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(87, 178);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "床位:";
            // 
            // lblNo
            // 
            this.lblNo.AutoSize = true;
            this.lblNo.Location = new System.Drawing.Point(130, 12);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(29, 12);
            this.lblNo.TabIndex = 1;
            this.lblNo.Text = "床号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(93, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "状态:";
            // 
            // vALUECODEBindingSource
            // 
            this.vALUECODEBindingSource.DataSource = typeof(BloodInfo_MngPlatform.Models.VALUE_CODE);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "患者:";
            // 
            // searchLookUpEdit1
            // 
            this.searchLookUpEdit1.Location = new System.Drawing.Point(185, 103);
            this.searchLookUpEdit1.Name = "searchLookUpEdit1";
            this.searchLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEdit1.Properties.DataSource = this.pATIENTBASEINFOBindingSource;
            this.searchLookUpEdit1.Properties.DisplayMember = "NAME";
            this.searchLookUpEdit1.Properties.ValueMember = "ID";
            this.searchLookUpEdit1.Properties.View = this.searchLookUpEdit1View;
            this.searchLookUpEdit1.Size = new System.Drawing.Size(15, 20);
            this.searchLookUpEdit1.TabIndex = 6;
            // 
            // pATIENTBASEINFOBindingSource
            // 
            this.pATIENTBASEINFOBindingSource.DataSource = typeof(BloodInfo_MngPlatform.Models.PATIENT_BASEINFO);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNAME,
            this.colSEX,
            this.colID_CODE});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colNAME
            // 
            this.colNAME.Caption = "姓名";
            this.colNAME.FieldName = "NAME";
            this.colNAME.MinWidth = 50;
            this.colNAME.Name = "colNAME";
            this.colNAME.Visible = true;
            this.colNAME.VisibleIndex = 0;
            // 
            // colSEX
            // 
            this.colSEX.Caption = "性别";
            this.colSEX.FieldName = "SEX";
            this.colSEX.Name = "colSEX";
            this.colSEX.Visible = true;
            this.colSEX.VisibleIndex = 1;
            // 
            // colID_CODE
            // 
            this.colID_CODE.Caption = "身份证号";
            this.colID_CODE.FieldName = "ID_CODE";
            this.colID_CODE.MinWidth = 120;
            this.colID_CODE.Name = "colID_CODE";
            this.colID_CODE.Visible = true;
            this.colID_CODE.VisibleIndex = 2;
            this.colID_CODE.Width = 120;
            // 
            // pATIENTREGISTBindingSource
            // 
            this.pATIENTREGISTBindingSource.DataSource = typeof(BloodInfo_MngPlatform.Models.PATIENT_REGIST);
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(132, 107);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(50, 12);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "预定人";
            // 
            // btnCheckIn
            // 
            this.btnCheckIn.Location = new System.Drawing.Point(97, 162);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new System.Drawing.Size(31, 18);
            this.btnCheckIn.TabIndex = 7;
            this.btnCheckIn.Text = "签到";
            this.btnCheckIn.Click += new System.EventHandler(this.btnCheckIn_Click);
            // 
            // btnMaintence
            // 
            this.btnMaintence.Location = new System.Drawing.Point(167, 162);
            this.btnMaintence.Name = "btnMaintence";
            this.btnMaintence.Size = new System.Drawing.Size(33, 18);
            this.btnMaintence.TabIndex = 7;
            this.btnMaintence.Text = "维护";
            this.btnMaintence.Click += new System.EventHandler(this.btnMaintence_Click);
            // 
            // btnIdle
            // 
            this.btnIdle.Location = new System.Drawing.Point(132, 162);
            this.btnIdle.Name = "btnIdle";
            this.btnIdle.Size = new System.Drawing.Size(31, 18);
            this.btnIdle.TabIndex = 7;
            this.btnIdle.Text = "空闲";
            this.btnIdle.Click += new System.EventHandler(this.btnIdle_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "签到时间:";
            // 
            // lblChckInTm
            // 
            this.lblChckInTm.AutoSize = true;
            this.lblChckInTm.Location = new System.Drawing.Point(130, 143);
            this.lblChckInTm.Name = "lblChckInTm";
            this.lblChckInTm.Size = new System.Drawing.Size(41, 12);
            this.lblChckInTm.TabIndex = 1;
            this.lblChckInTm.Text = "      ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(72, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "预约时间:";
            // 
            // lblResevTm
            // 
            this.lblResevTm.AutoSize = true;
            this.lblResevTm.Location = new System.Drawing.Point(130, 125);
            this.lblResevTm.Name = "lblResevTm";
            this.lblResevTm.Size = new System.Drawing.Size(41, 12);
            this.lblResevTm.TabIndex = 1;
            this.lblResevTm.Text = "      ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(81, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "序列号:";
            // 
            // lblSn
            // 
            this.lblSn.AutoSize = true;
            this.lblSn.Location = new System.Drawing.Point(130, 48);
            this.lblSn.Name = "lblSn";
            this.lblSn.Size = new System.Drawing.Size(29, 12);
            this.lblSn.TabIndex = 1;
            this.lblSn.Text = "    ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(71, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "设备类型:";
            // 
            // lblMachType
            // 
            this.lblMachType.AutoSize = true;
            this.lblMachType.Location = new System.Drawing.Point(130, 30);
            this.lblMachType.Name = "lblMachType";
            this.lblMachType.Size = new System.Drawing.Size(29, 12);
            this.lblMachType.TabIndex = 1;
            this.lblMachType.Text = "    ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(93, 67);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 12);
            this.label10.TabIndex = 1;
            this.label10.Text = "型号:";
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(130, 66);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(29, 12);
            this.lblModel.TabIndex = 1;
            this.lblModel.Text = "    ";
            // 
            // cbxStatus1
            // 
            this.cbxStatus1.Location = new System.Drawing.Point(132, 84);
            this.cbxStatus1.Name = "cbxStatus1";
            this.cbxStatus1.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cbxStatus1.Properties.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbxStatus1.Properties.Appearance.Options.UseBackColor = true;
            this.cbxStatus1.Properties.Appearance.Options.UseForeColor = true;
            this.cbxStatus1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.cbxStatus1.Properties.DataSource = this.vALUECODEBindingSource;
            this.cbxStatus1.Properties.DisplayMember = "DSP_MEMBER";
            this.cbxStatus1.Properties.ValueMember = "VALUE_MEMBER";
            this.cbxStatus1.Size = new System.Drawing.Size(70, 18);
            this.cbxStatus1.TabIndex = 8;
            // 
            // UcDialysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.cbxStatus1);
            this.Controls.Add(this.btnMaintence);
            this.Controls.Add(this.btnIdle);
            this.Controls.Add(this.btnCheckIn);
            this.Controls.Add(this.searchLookUpEdit1);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblMachType);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.lblSn);
            this.Controls.Add(this.lblNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblResevTm);
            this.Controls.Add(this.lblChckInTm);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Name = "UcDialysis";
            this.Size = new System.Drawing.Size(205, 180);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vALUECODEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pATIENTBASEINFOBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pATIENTREGISTBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxStatus1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.BindingSource vALUECODEBindingSource;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpEdit1;
        private System.Windows.Forms.BindingSource pATIENTREGISTBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private System.Windows.Forms.Label lblName;
        private DevExpress.XtraGrid.Columns.GridColumn colNAME;
        private DevExpress.XtraEditors.SimpleButton btnCheckIn;
        private DevExpress.XtraEditors.SimpleButton btnMaintence;
        private DevExpress.XtraEditors.SimpleButton btnIdle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblChckInTm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblResevTm;
        private System.Windows.Forms.BindingSource pATIENTBASEINFOBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colSEX;
        private DevExpress.XtraGrid.Columns.GridColumn colID_CODE;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblMachType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblModel;
        private DevExpress.XtraEditors.LookUpEdit cbxStatus1;

    }
}
