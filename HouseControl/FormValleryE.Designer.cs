namespace HouseControl
{
    partial class FormValleryE
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSetTemp1 = new System.Windows.Forms.Button();
            this.btnOutOn1 = new System.Windows.Forms.Button();
            this.btnOutOff1 = new System.Windows.Forms.Button();
            this.tbTemp1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSetTemp2 = new System.Windows.Forms.Button();
            this.tbTemp2 = new System.Windows.Forms.TextBox();
            this.btnPowerOff1 = new System.Windows.Forms.Button();
            this.btnPowerOn1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 11;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 57);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.05263F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.31579F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.31579F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.31579F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(952, 192);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "밸리하우스 E";
            // 
            // btnSetTemp1
            // 
            this.btnSetTemp1.Location = new System.Drawing.Point(6, 32);
            this.btnSetTemp1.Name = "btnSetTemp1";
            this.btnSetTemp1.Size = new System.Drawing.Size(75, 23);
            this.btnSetTemp1.TabIndex = 2;
            this.btnSetTemp1.Text = "온도설정";
            this.btnSetTemp1.UseVisualStyleBackColor = true;
            this.btnSetTemp1.Click += new System.EventHandler(this.btnSetTemp1_Click);
            // 
            // btnOutOn1
            // 
            this.btnOutOn1.Location = new System.Drawing.Point(93, 266);
            this.btnOutOn1.Name = "btnOutOn1";
            this.btnOutOn1.Size = new System.Drawing.Size(75, 23);
            this.btnOutOn1.TabIndex = 3;
            this.btnOutOn1.Text = "외출ON";
            this.btnOutOn1.UseVisualStyleBackColor = true;
            this.btnOutOn1.Click += new System.EventHandler(this.btnOutOn1_Click);
            // 
            // btnOutOff1
            // 
            this.btnOutOff1.Location = new System.Drawing.Point(93, 295);
            this.btnOutOff1.Name = "btnOutOff1";
            this.btnOutOff1.Size = new System.Drawing.Size(75, 23);
            this.btnOutOff1.TabIndex = 4;
            this.btnOutOff1.Text = "외출OFF";
            this.btnOutOff1.UseVisualStyleBackColor = true;
            this.btnOutOff1.Click += new System.EventHandler(this.btnOutOff1_Click);
            // 
            // tbTemp1
            // 
            this.tbTemp1.Location = new System.Drawing.Point(6, 5);
            this.tbTemp1.Name = "tbTemp1";
            this.tbTemp1.Size = new System.Drawing.Size(75, 21);
            this.tbTemp1.TabIndex = 5;
            this.tbTemp1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbTemp1_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.btnSetTemp1);
            this.panel1.Controls.Add(this.tbTemp1);
            this.panel1.Location = new System.Drawing.Point(174, 263);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(88, 66);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.btnSetTemp2);
            this.panel2.Controls.Add(this.tbTemp2);
            this.panel2.Location = new System.Drawing.Point(268, 263);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(88, 66);
            this.panel2.TabIndex = 9;
            // 
            // btnSetTemp2
            // 
            this.btnSetTemp2.Location = new System.Drawing.Point(6, 32);
            this.btnSetTemp2.Name = "btnSetTemp2";
            this.btnSetTemp2.Size = new System.Drawing.Size(75, 23);
            this.btnSetTemp2.TabIndex = 2;
            this.btnSetTemp2.Text = "온도설정";
            this.btnSetTemp2.UseVisualStyleBackColor = true;
            this.btnSetTemp2.Click += new System.EventHandler(this.btnSetTemp2_Click);
            // 
            // tbTemp2
            // 
            this.tbTemp2.Location = new System.Drawing.Point(6, 5);
            this.tbTemp2.Name = "tbTemp2";
            this.tbTemp2.Size = new System.Drawing.Size(75, 21);
            this.tbTemp2.TabIndex = 5;
            this.tbTemp2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbTemp2_KeyPress);
            // 
            // btnPowerOff1
            // 
            this.btnPowerOff1.Location = new System.Drawing.Point(12, 295);
            this.btnPowerOff1.Name = "btnPowerOff1";
            this.btnPowerOff1.Size = new System.Drawing.Size(75, 23);
            this.btnPowerOff1.TabIndex = 11;
            this.btnPowerOff1.Text = "난방OFF";
            this.btnPowerOff1.UseVisualStyleBackColor = true;
            this.btnPowerOff1.Click += new System.EventHandler(this.btnPowerOff1_Click);
            // 
            // btnPowerOn1
            // 
            this.btnPowerOn1.Location = new System.Drawing.Point(12, 266);
            this.btnPowerOn1.Name = "btnPowerOn1";
            this.btnPowerOn1.Size = new System.Drawing.Size(75, 23);
            this.btnPowerOn1.TabIndex = 10;
            this.btnPowerOn1.Text = "난방ON";
            this.btnPowerOn1.UseVisualStyleBackColor = true;
            this.btnPowerOn1.Click += new System.EventHandler(this.btnPowerOn1_Click);
            // 
            // FormValleryE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 624);
            this.Controls.Add(this.btnPowerOff1);
            this.Controls.Add(this.btnPowerOn1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnOutOff1);
            this.Controls.Add(this.btnOutOn1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormValleryE";
            this.Text = "FormValleryE";
            this.Load += new System.EventHandler(this.FormValleryE_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSetTemp1;
        private System.Windows.Forms.Button btnOutOn1;
        private System.Windows.Forms.Button btnOutOff1;
        private System.Windows.Forms.TextBox tbTemp1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSetTemp2;
        private System.Windows.Forms.TextBox tbTemp2;
        private System.Windows.Forms.Button btnPowerOff1;
        private System.Windows.Forms.Button btnPowerOn1;
    }
}