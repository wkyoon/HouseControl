namespace HouseControl
{
    partial class FormSetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btndown1 = new System.Windows.Forms.Button();
            this.btnup1 = new System.Windows.Forms.Button();
            this.tbTemp1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btndown2 = new System.Windows.Forms.Button();
            this.btnup2 = new System.Windows.Forms.Button();
            this.tbTemp2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "자주쓰는 난방온도를 입력해 주십시오.";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(23, 191);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(323, 52);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "설정하기";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel3.Controls.Add(this.btndown1);
            this.panel3.Controls.Add(this.btnup1);
            this.panel3.Controls.Add(this.tbTemp1);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(23, 49);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(144, 113);
            this.panel3.TabIndex = 11;
            // 
            // btndown1
            // 
            this.btndown1.Font = new System.Drawing.Font("Gulim", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btndown1.Location = new System.Drawing.Point(73, 70);
            this.btndown1.Name = "btndown1";
            this.btndown1.Size = new System.Drawing.Size(67, 31);
            this.btndown1.TabIndex = 6;
            this.btndown1.Text = "DOWN";
            this.btndown1.UseVisualStyleBackColor = true;
            this.btndown1.Click += new System.EventHandler(this.btndown1_Click);
            // 
            // btnup1
            // 
            this.btnup1.Font = new System.Drawing.Font("Gulim", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnup1.Location = new System.Drawing.Point(73, 36);
            this.btnup1.Name = "btnup1";
            this.btnup1.Size = new System.Drawing.Size(67, 28);
            this.btnup1.TabIndex = 5;
            this.btnup1.Text = "UP";
            this.btnup1.UseVisualStyleBackColor = true;
            this.btnup1.Click += new System.EventHandler(this.btnup1_Click);
            // 
            // tbTemp1
            // 
            this.tbTemp1.Font = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbTemp1.Location = new System.Drawing.Point(5, 38);
            this.tbTemp1.MaxLength = 3;
            this.tbTemp1.Name = "tbTemp1";
            this.tbTemp1.Size = new System.Drawing.Size(62, 26);
            this.tbTemp1.TabIndex = 4;
            this.tbTemp1.Text = "15";
            this.tbTemp1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "난방설정온도 1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this.btndown2);
            this.panel1.Controls.Add(this.btnup2);
            this.panel1.Controls.Add(this.tbTemp2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(202, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 113);
            this.panel1.TabIndex = 12;
            // 
            // btndown2
            // 
            this.btndown2.Font = new System.Drawing.Font("Gulim", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btndown2.Location = new System.Drawing.Point(73, 70);
            this.btndown2.Name = "btndown2";
            this.btndown2.Size = new System.Drawing.Size(67, 31);
            this.btndown2.TabIndex = 6;
            this.btndown2.Text = "DOWN";
            this.btndown2.UseVisualStyleBackColor = true;
            this.btndown2.Click += new System.EventHandler(this.btndown2_Click);
            // 
            // btnup2
            // 
            this.btnup2.Font = new System.Drawing.Font("Gulim", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnup2.Location = new System.Drawing.Point(73, 36);
            this.btnup2.Name = "btnup2";
            this.btnup2.Size = new System.Drawing.Size(67, 28);
            this.btnup2.TabIndex = 5;
            this.btnup2.Text = "UP";
            this.btnup2.UseVisualStyleBackColor = true;
            this.btnup2.Click += new System.EventHandler(this.btnup2_Click);
            // 
            // tbTemp2
            // 
            this.tbTemp2.Font = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbTemp2.Location = new System.Drawing.Point(5, 38);
            this.tbTemp2.MaxLength = 3;
            this.tbTemp2.Name = "tbTemp2";
            this.tbTemp2.Size = new System.Drawing.Size(62, 26);
            this.tbTemp2.TabIndex = 4;
            this.tbTemp2.Text = "25";
            this.tbTemp2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "난방설정온도 2";
            // 
            // FormSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 267);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSetting";
            this.Text = "난방온도 설정";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSetting_FormClosing);
            this.Load += new System.EventHandler(this.FormSetting_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btndown1;
        private System.Windows.Forms.Button btnup1;
        private System.Windows.Forms.TextBox tbTemp1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btndown2;
        private System.Windows.Forms.Button btnup2;
        private System.Windows.Forms.TextBox tbTemp2;
        private System.Windows.Forms.Label label5;
    }
}