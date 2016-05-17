namespace ptCodeTool
{
    partial class FrmRoadCode
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbRoad = new System.Windows.Forms.ComboBox();
            this.rdoSelectRoadPolygon = new System.Windows.Forms.RadioButton();
            this.rdoSelectRoadLine = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbRegion = new System.Windows.Forms.ComboBox();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbRoad);
            this.groupBox1.Controls.Add(this.rdoSelectRoadPolygon);
            this.groupBox1.Controls.Add(this.rdoSelectRoadLine);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(367, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "道路图层";
            // 
            // cbRoad
            // 
            this.cbRoad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRoad.FormattingEnabled = true;
            this.cbRoad.Location = new System.Drawing.Point(7, 43);
            this.cbRoad.Name = "cbRoad";
            this.cbRoad.Size = new System.Drawing.Size(348, 20);
            this.cbRoad.TabIndex = 2;
            // 
            // rdoSelectRoadPolygon
            // 
            this.rdoSelectRoadPolygon.AutoSize = true;
            this.rdoSelectRoadPolygon.Location = new System.Drawing.Point(121, 20);
            this.rdoSelectRoadPolygon.Name = "rdoSelectRoadPolygon";
            this.rdoSelectRoadPolygon.Size = new System.Drawing.Size(59, 16);
            this.rdoSelectRoadPolygon.TabIndex = 1;
            this.rdoSelectRoadPolygon.Text = "道路面";
            this.rdoSelectRoadPolygon.UseVisualStyleBackColor = true;
            this.rdoSelectRoadPolygon.CheckedChanged += new System.EventHandler(this.rdoSelectRoadPolygon_CheckedChanged);
            // 
            // rdoSelectRoadLine
            // 
            this.rdoSelectRoadLine.AutoSize = true;
            this.rdoSelectRoadLine.Checked = true;
            this.rdoSelectRoadLine.Location = new System.Drawing.Point(6, 20);
            this.rdoSelectRoadLine.Name = "rdoSelectRoadLine";
            this.rdoSelectRoadLine.Size = new System.Drawing.Size(59, 16);
            this.rdoSelectRoadLine.TabIndex = 0;
            this.rdoSelectRoadLine.TabStop = true;
            this.rdoSelectRoadLine.Text = "道路线";
            this.rdoSelectRoadLine.UseVisualStyleBackColor = true;
            this.rdoSelectRoadLine.CheckedChanged += new System.EventHandler(this.rdoSelectRoadLine_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbRegion);
            this.groupBox2.Location = new System.Drawing.Point(12, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(367, 59);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "行政区图层";
            // 
            // cbRegion
            // 
            this.cbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRegion.FormattingEnabled = true;
            this.cbRegion.Location = new System.Drawing.Point(7, 20);
            this.cbRegion.Name = "cbRegion";
            this.cbRegion.Size = new System.Drawing.Size(348, 20);
            this.cbRegion.TabIndex = 3;
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(304, 169);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 2;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(223, 169);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // FrmRoadCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 204);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmRoadCode";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "道路编码";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbRoad;
        private System.Windows.Forms.RadioButton rdoSelectRoadPolygon;
        private System.Windows.Forms.RadioButton rdoSelectRoadLine;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbRegion;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnOk;
    }
}