namespace ptCodeTool
{
    partial class FrmMain
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
            this.gbCodeType = new System.Windows.Forms.GroupBox();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnDoCoding = new System.Windows.Forms.Button();
            this.rbSzRoadCode = new System.Windows.Forms.RadioButton();
            this.rbFsRoadCode = new System.Windows.Forms.RadioButton();
            this.rbFsFacilityCode = new System.Windows.Forms.RadioButton();
            this.btnSaveLog = new System.Windows.Forms.Button();
            this.gbCodeType.SuspendLayout();
            this.gbLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbCodeType
            // 
            this.gbCodeType.Controls.Add(this.rbFsFacilityCode);
            this.gbCodeType.Controls.Add(this.rbFsRoadCode);
            this.gbCodeType.Controls.Add(this.rbSzRoadCode);
            this.gbCodeType.Location = new System.Drawing.Point(12, 12);
            this.gbCodeType.Name = "gbCodeType";
            this.gbCodeType.Size = new System.Drawing.Size(762, 56);
            this.gbCodeType.TabIndex = 0;
            this.gbCodeType.TabStop = false;
            this.gbCodeType.Text = "编码类型";
            // 
            // gbLog
            // 
            this.gbLog.Controls.Add(this.txtLog);
            this.gbLog.Location = new System.Drawing.Point(12, 74);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(762, 194);
            this.gbLog.TabIndex = 1;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "日志";
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(3, 17);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(756, 174);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "";
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(699, 274);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 2;
            this.btnCancle.Text = "关闭";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnDoCoding
            // 
            this.btnDoCoding.Location = new System.Drawing.Point(618, 274);
            this.btnDoCoding.Name = "btnDoCoding";
            this.btnDoCoding.Size = new System.Drawing.Size(75, 23);
            this.btnDoCoding.TabIndex = 3;
            this.btnDoCoding.Text = "开始编码";
            this.btnDoCoding.UseVisualStyleBackColor = true;
            this.btnDoCoding.Click += new System.EventHandler(this.btnDoCoding_Click);
            // 
            // rbSzRoadCode
            // 
            this.rbSzRoadCode.AutoSize = true;
            this.rbSzRoadCode.Checked = true;
            this.rbSzRoadCode.Location = new System.Drawing.Point(37, 20);
            this.rbSzRoadCode.Name = "rbSzRoadCode";
            this.rbSzRoadCode.Size = new System.Drawing.Size(95, 16);
            this.rbSzRoadCode.TabIndex = 0;
            this.rbSzRoadCode.TabStop = true;
            this.rbSzRoadCode.Text = "深圳道路编码";
            this.rbSzRoadCode.UseVisualStyleBackColor = true;
            this.rbSzRoadCode.CheckedChanged += new System.EventHandler(this.rbSzRoadCode_CheckedChanged);
            // 
            // rbFsRoadCode
            // 
            this.rbFsRoadCode.AutoSize = true;
            this.rbFsRoadCode.Location = new System.Drawing.Point(154, 20);
            this.rbFsRoadCode.Name = "rbFsRoadCode";
            this.rbFsRoadCode.Size = new System.Drawing.Size(95, 16);
            this.rbFsRoadCode.TabIndex = 1;
            this.rbFsRoadCode.Text = "佛山道路编码";
            this.rbFsRoadCode.UseVisualStyleBackColor = true;
            this.rbFsRoadCode.CheckedChanged += new System.EventHandler(this.rbFsRoadCode_CheckedChanged);
            // 
            // rbFsFacilityCode
            // 
            this.rbFsFacilityCode.AutoSize = true;
            this.rbFsFacilityCode.Location = new System.Drawing.Point(265, 20);
            this.rbFsFacilityCode.Name = "rbFsFacilityCode";
            this.rbFsFacilityCode.Size = new System.Drawing.Size(95, 16);
            this.rbFsFacilityCode.TabIndex = 2;
            this.rbFsFacilityCode.Text = "佛山设施编码";
            this.rbFsFacilityCode.UseVisualStyleBackColor = true;
            this.rbFsFacilityCode.CheckedChanged += new System.EventHandler(this.rbFsFacilityCode_CheckedChanged);
            // 
            // btnSaveLog
            // 
            this.btnSaveLog.Location = new System.Drawing.Point(15, 274);
            this.btnSaveLog.Name = "btnSaveLog";
            this.btnSaveLog.Size = new System.Drawing.Size(75, 23);
            this.btnSaveLog.TabIndex = 4;
            this.btnSaveLog.Text = "保存日志";
            this.btnSaveLog.UseVisualStyleBackColor = true;
            this.btnSaveLog.Click += new System.EventHandler(this.btnSaveLog_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 308);
            this.Controls.Add(this.btnSaveLog);
            this.Controls.Add(this.btnDoCoding);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.gbLog);
            this.Controls.Add(this.gbCodeType);
            this.Name = "FrmMain";
            this.ShowIcon = false;
            this.Text = "道路编码                                                           ";
            this.gbCodeType.ResumeLayout(false);
            this.gbCodeType.PerformLayout();
            this.gbLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbCodeType;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnDoCoding;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.RadioButton rbSzRoadCode;
        private System.Windows.Forms.RadioButton rbFsFacilityCode;
        private System.Windows.Forms.RadioButton rbFsRoadCode;
        private System.Windows.Forms.Button btnSaveLog;
    }
}