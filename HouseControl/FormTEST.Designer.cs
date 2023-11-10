namespace HouseControl
{
    partial class FormTEST
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
            this.btnMasterInfo = new System.Windows.Forms.Button();
            this.tbNID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbMID = new System.Windows.Forms.TextBox();
            this.btnRStatus = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbID = new System.Windows.Forms.TextBox();
            this.btnPowerOn = new System.Windows.Forms.Button();
            this.btnPowerOff = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbTemp = new System.Windows.Forms.TextBox();
            this.btnSetTemp = new System.Windows.Forms.Button();
            this.btnOutOn = new System.Windows.Forms.Button();
            this.btnOutOff = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMasterInfo
            // 
            this.btnMasterInfo.Location = new System.Drawing.Point(510, 26);
            this.btnMasterInfo.Name = "btnMasterInfo";
            this.btnMasterInfo.Size = new System.Drawing.Size(140, 23);
            this.btnMasterInfo.TabIndex = 0;
            this.btnMasterInfo.Text = "마스터특성요구";
            this.btnMasterInfo.UseVisualStyleBackColor = true;
            this.btnMasterInfo.Click += new System.EventHandler(this.btnMasterInfo_Click);
            // 
            // tbNID
            // 
            this.tbNID.Location = new System.Drawing.Point(60, 29);
            this.tbNID.Name = "tbNID";
            this.tbNID.Size = new System.Drawing.Size(100, 21);
            this.tbNID.TabIndex = 1;
            this.tbNID.Text = "5211";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "NID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "MasterID";
            // 
            // tbMID
            // 
            this.tbMID.Location = new System.Drawing.Point(240, 28);
            this.tbMID.Name = "tbMID";
            this.tbMID.Size = new System.Drawing.Size(36, 21);
            this.tbMID.TabIndex = 4;
            this.tbMID.Text = "1";
            // 
            // btnRStatus
            // 
            this.btnRStatus.Location = new System.Drawing.Point(510, 66);
            this.btnRStatus.Name = "btnRStatus";
            this.btnRStatus.Size = new System.Drawing.Size(140, 23);
            this.btnRStatus.TabIndex = 5;
            this.btnRStatus.Text = "온도조절기 상태요구";
            this.btnRStatus.UseVisualStyleBackColor = true;
            this.btnRStatus.Click += new System.EventHandler(this.btnRStatus_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(303, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "온도조절기ID";
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(411, 68);
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(51, 21);
            this.tbID.TabIndex = 7;
            this.tbID.Text = "1";
            // 
            // btnPowerOn
            // 
            this.btnPowerOn.Location = new System.Drawing.Point(510, 106);
            this.btnPowerOn.Name = "btnPowerOn";
            this.btnPowerOn.Size = new System.Drawing.Size(140, 23);
            this.btnPowerOn.TabIndex = 8;
            this.btnPowerOn.Text = "온도조절기 난방 ON";
            this.btnPowerOn.UseVisualStyleBackColor = true;
            this.btnPowerOn.Click += new System.EventHandler(this.btnPowerOn_Click);
            // 
            // btnPowerOff
            // 
            this.btnPowerOff.Location = new System.Drawing.Point(510, 146);
            this.btnPowerOff.Name = "btnPowerOff";
            this.btnPowerOff.Size = new System.Drawing.Size(140, 23);
            this.btnPowerOff.TabIndex = 9;
            this.btnPowerOff.Text = "온도조절기 난방 OFF";
            this.btnPowerOff.UseVisualStyleBackColor = true;
            this.btnPowerOff.Click += new System.EventHandler(this.btnPowerOff_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(152, 287);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(227, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "난방온도(0000 형태로 표시 30도는 3000 )";
            // 
            // tbTemp
            // 
            this.tbTemp.Location = new System.Drawing.Point(405, 282);
            this.tbTemp.Name = "tbTemp";
            this.tbTemp.Size = new System.Drawing.Size(57, 21);
            this.tbTemp.TabIndex = 11;
            this.tbTemp.Text = "3000";
            // 
            // btnSetTemp
            // 
            this.btnSetTemp.Location = new System.Drawing.Point(510, 282);
            this.btnSetTemp.Name = "btnSetTemp";
            this.btnSetTemp.Size = new System.Drawing.Size(179, 23);
            this.btnSetTemp.TabIndex = 12;
            this.btnSetTemp.Text = "온도조절기 설정온도 변경";
            this.btnSetTemp.UseVisualStyleBackColor = true;
            this.btnSetTemp.Click += new System.EventHandler(this.btnSetTemp_Click);
            // 
            // btnOutOn
            // 
            this.btnOutOn.Location = new System.Drawing.Point(510, 186);
            this.btnOutOn.Name = "btnOutOn";
            this.btnOutOn.Size = new System.Drawing.Size(140, 23);
            this.btnOutOn.TabIndex = 13;
            this.btnOutOn.Text = "온도조절기 외출 ON";
            this.btnOutOn.UseVisualStyleBackColor = true;
            this.btnOutOn.Click += new System.EventHandler(this.btnOutOn_Click);
            // 
            // btnOutOff
            // 
            this.btnOutOff.Location = new System.Drawing.Point(510, 226);
            this.btnOutOff.Name = "btnOutOff";
            this.btnOutOff.Size = new System.Drawing.Size(140, 23);
            this.btnOutOff.TabIndex = 14;
            this.btnOutOff.Text = "온도조절기 외출 OFF";
            this.btnOutOff.UseVisualStyleBackColor = true;
            this.btnOutOff.Click += new System.EventHandler(this.btnOutOff_Click);
            // 
            // FormTEST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 424);
            this.Controls.Add(this.btnOutOff);
            this.Controls.Add(this.btnOutOn);
            this.Controls.Add(this.btnSetTemp);
            this.Controls.Add(this.tbTemp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnPowerOff);
            this.Controls.Add(this.btnPowerOn);
            this.Controls.Add(this.tbID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRStatus);
            this.Controls.Add(this.tbMID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbNID);
            this.Controls.Add(this.btnMasterInfo);
            this.Name = "FormTEST";
            this.Text = "FormTEST";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTEST_FormClosing);
            this.Load += new System.EventHandler(this.FormTEST_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMasterInfo;
        private System.Windows.Forms.TextBox tbNID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbMID;
        private System.Windows.Forms.Button btnRStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.Button btnPowerOn;
        private System.Windows.Forms.Button btnPowerOff;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbTemp;
        private System.Windows.Forms.Button btnSetTemp;
        private System.Windows.Forms.Button btnOutOn;
        private System.Windows.Forms.Button btnOutOff;
    }
}