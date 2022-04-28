namespace IEC104_dotnet
{
    partial class Form1
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
            this.txtReceive = new System.Windows.Forms.TextBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCA = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTRIP = new System.Windows.Forms.Button();
            this.txtIOAAddr = new System.Windows.Forms.TextBox();
            this.btnCLOSE = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.DoubleCMD = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSingleIOA = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSingleClose = new System.Windows.Forms.Button();
            this.btnSingleTRIP = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDoubleTestValue = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtIOATestDouble = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnDoubleTestSelect = new System.Windows.Forms.Button();
            this.btnDoubleTestExec = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtSingleTestValue = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtIOATestSingle = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSingleTestSel = new System.Windows.Forms.Button();
            this.btnSingleTestExec = new System.Windows.Forms.Button();
            this.DoubleCMD.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtReceive
            // 
            this.txtReceive.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceive.Location = new System.Drawing.Point(12, 120);
            this.txtReceive.Multiline = true;
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtReceive.Size = new System.Drawing.Size(519, 487);
            this.txtReceive.TabIndex = 5;
            // 
            // txtResult
            // 
            this.txtResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Location = new System.Drawing.Point(537, 120);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(484, 487);
            this.txtResult.TabIndex = 6;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(105, 34);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(50, 20);
            this.txtPort.TabIndex = 7;
            this.txtPort.Text = "2404";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(63, 8);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(92, 20);
            this.txtIP.TabIndex = 8;
            this.txtIP.Text = "10.175.88.211";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(19, 78);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(137, 23);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Communication Log";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(540, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Parse Result";
            // 
            // txtCA
            // 
            this.txtCA.Location = new System.Drawing.Point(105, 57);
            this.txtCA.Name = "txtCA";
            this.txtCA.Size = new System.Drawing.Size(50, 20);
            this.txtCA.TabIndex = 14;
            this.txtCA.Tag = "175.88.211";
            this.txtCA.Text = "8211";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "ADSU Addr";
            // 
            // btnTRIP
            // 
            this.btnTRIP.Location = new System.Drawing.Point(5, 16);
            this.btnTRIP.Name = "btnTRIP";
            this.btnTRIP.Size = new System.Drawing.Size(64, 23);
            this.btnTRIP.TabIndex = 16;
            this.btnTRIP.Text = "OPEN";
            this.btnTRIP.UseVisualStyleBackColor = true;
            this.btnTRIP.Click += new System.EventHandler(this.btnTRIP_Click);
            // 
            // txtIOAAddr
            // 
            this.txtIOAAddr.Location = new System.Drawing.Point(180, 17);
            this.txtIOAAddr.Name = "txtIOAAddr";
            this.txtIOAAddr.Size = new System.Drawing.Size(53, 20);
            this.txtIOAAddr.TabIndex = 17;
            this.txtIOAAddr.Text = "2200";
            // 
            // btnCLOSE
            // 
            this.btnCLOSE.Location = new System.Drawing.Point(75, 16);
            this.btnCLOSE.Name = "btnCLOSE";
            this.btnCLOSE.Size = new System.Drawing.Size(68, 23);
            this.btnCLOSE.TabIndex = 18;
            this.btnCLOSE.Text = "CLOSE";
            this.btnCLOSE.UseVisualStyleBackColor = true;
            this.btnCLOSE.Click += new System.EventHandler(this.btnCLOSE_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(149, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "IOA";
            // 
            // DoubleCMD
            // 
            this.DoubleCMD.Controls.Add(this.btnTRIP);
            this.DoubleCMD.Controls.Add(this.txtIOAAddr);
            this.DoubleCMD.Controls.Add(this.label6);
            this.DoubleCMD.Controls.Add(this.btnCLOSE);
            this.DoubleCMD.Location = new System.Drawing.Point(161, 8);
            this.DoubleCMD.Name = "DoubleCMD";
            this.DoubleCMD.Size = new System.Drawing.Size(246, 45);
            this.DoubleCMD.TabIndex = 20;
            this.DoubleCMD.TabStop = false;
            this.DoubleCMD.Text = "S/E DoubleCMD";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSingleIOA);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnSingleClose);
            this.groupBox1.Controls.Add(this.btnSingleTRIP);
            this.groupBox1.Location = new System.Drawing.Point(162, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(245, 42);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "S/E SingleCMD";
            // 
            // txtSingleIOA
            // 
            this.txtSingleIOA.Location = new System.Drawing.Point(179, 15);
            this.txtSingleIOA.Name = "txtSingleIOA";
            this.txtSingleIOA.Size = new System.Drawing.Size(55, 20);
            this.txtSingleIOA.TabIndex = 24;
            this.txtSingleIOA.Text = "2100";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(148, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "IOA";
            // 
            // btnSingleClose
            // 
            this.btnSingleClose.Location = new System.Drawing.Point(74, 13);
            this.btnSingleClose.Name = "btnSingleClose";
            this.btnSingleClose.Size = new System.Drawing.Size(68, 23);
            this.btnSingleClose.TabIndex = 23;
            this.btnSingleClose.Text = "CLOSE";
            this.btnSingleClose.UseVisualStyleBackColor = true;
            this.btnSingleClose.Click += new System.EventHandler(this.btnSingleClose_Click);
            // 
            // btnSingleTRIP
            // 
            this.btnSingleTRIP.Location = new System.Drawing.Point(6, 14);
            this.btnSingleTRIP.Name = "btnSingleTRIP";
            this.btnSingleTRIP.Size = new System.Drawing.Size(61, 23);
            this.btnSingleTRIP.TabIndex = 22;
            this.btnSingleTRIP.Text = "OPEN";
            this.btnSingleTRIP.UseVisualStyleBackColor = true;
            this.btnSingleTRIP.Click += new System.EventHandler(this.btnSingleTRIP_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDoubleTestValue);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtIOATestDouble);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.btnDoubleTestSelect);
            this.groupBox2.Controls.Add(this.btnDoubleTestExec);
            this.groupBox2.Location = new System.Drawing.Point(430, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 41);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DoubleCMD";
            // 
            // txtDoubleTestValue
            // 
            this.txtDoubleTestValue.Location = new System.Drawing.Point(150, 14);
            this.txtDoubleTestValue.Name = "txtDoubleTestValue";
            this.txtDoubleTestValue.Size = new System.Drawing.Size(53, 20);
            this.txtDoubleTestValue.TabIndex = 22;
            this.txtDoubleTestValue.Text = "1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(110, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Value";
            // 
            // txtIOATestDouble
            // 
            this.txtIOATestDouble.Location = new System.Drawing.Point(38, 14);
            this.txtIOATestDouble.Name = "txtIOATestDouble";
            this.txtIOATestDouble.Size = new System.Drawing.Size(53, 20);
            this.txtIOATestDouble.TabIndex = 20;
            this.txtIOATestDouble.Text = "2200";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "IOA";
            // 
            // btnDoubleTestSelect
            // 
            this.btnDoubleTestSelect.Location = new System.Drawing.Point(210, 12);
            this.btnDoubleTestSelect.Name = "btnDoubleTestSelect";
            this.btnDoubleTestSelect.Size = new System.Drawing.Size(45, 23);
            this.btnDoubleTestSelect.TabIndex = 1;
            this.btnDoubleTestSelect.Text = "Select";
            this.btnDoubleTestSelect.UseVisualStyleBackColor = true;
            this.btnDoubleTestSelect.Click += new System.EventHandler(this.btnDoubleTestSelect_Click);
            // 
            // btnDoubleTestExec
            // 
            this.btnDoubleTestExec.Location = new System.Drawing.Point(261, 12);
            this.btnDoubleTestExec.Name = "btnDoubleTestExec";
            this.btnDoubleTestExec.Size = new System.Drawing.Size(48, 23);
            this.btnDoubleTestExec.TabIndex = 0;
            this.btnDoubleTestExec.Text = "Exec";
            this.btnDoubleTestExec.UseVisualStyleBackColor = true;
            this.btnDoubleTestExec.Click += new System.EventHandler(this.btnDoubleTestExec_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtSingleTestValue);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtIOATestSingle);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.btnSingleTestSel);
            this.groupBox3.Controls.Add(this.btnSingleTestExec);
            this.groupBox3.Location = new System.Drawing.Point(430, 60);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(315, 41);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "SingleCMD";
            // 
            // txtSingleTestValue
            // 
            this.txtSingleTestValue.Location = new System.Drawing.Point(150, 14);
            this.txtSingleTestValue.Name = "txtSingleTestValue";
            this.txtSingleTestValue.Size = new System.Drawing.Size(53, 20);
            this.txtSingleTestValue.TabIndex = 22;
            this.txtSingleTestValue.Text = "1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(110, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Value";
            // 
            // txtIOATestSingle
            // 
            this.txtIOATestSingle.Location = new System.Drawing.Point(38, 14);
            this.txtIOATestSingle.Name = "txtIOATestSingle";
            this.txtIOATestSingle.Size = new System.Drawing.Size(53, 20);
            this.txtIOATestSingle.TabIndex = 20;
            this.txtIOATestSingle.Text = "2100";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "IOA";
            // 
            // btnSingleTestSel
            // 
            this.btnSingleTestSel.Location = new System.Drawing.Point(210, 12);
            this.btnSingleTestSel.Name = "btnSingleTestSel";
            this.btnSingleTestSel.Size = new System.Drawing.Size(45, 23);
            this.btnSingleTestSel.TabIndex = 1;
            this.btnSingleTestSel.Text = "Select";
            this.btnSingleTestSel.UseVisualStyleBackColor = true;
            this.btnSingleTestSel.Click += new System.EventHandler(this.btnSingleTestSel_Click);
            // 
            // btnSingleTestExec
            // 
            this.btnSingleTestExec.Location = new System.Drawing.Point(261, 12);
            this.btnSingleTestExec.Name = "btnSingleTestExec";
            this.btnSingleTestExec.Size = new System.Drawing.Size(48, 23);
            this.btnSingleTestExec.TabIndex = 0;
            this.btnSingleTestExec.Text = "Exec";
            this.btnSingleTestExec.UseVisualStyleBackColor = true;
            this.btnSingleTestExec.Click += new System.EventHandler(this.btnSingleTestExec_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 619);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DoubleCMD);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCA);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtReceive);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DoubleCMD.ResumeLayout(false);
            this.DoubleCMD.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtReceive;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCA;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnTRIP;
        private System.Windows.Forms.TextBox txtIOAAddr;
        private System.Windows.Forms.Button btnCLOSE;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox DoubleCMD;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSingleIOA;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSingleClose;
        private System.Windows.Forms.Button btnSingleTRIP;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtDoubleTestValue;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtIOATestDouble;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnDoubleTestSelect;
        private System.Windows.Forms.Button btnDoubleTestExec;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtSingleTestValue;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtIOATestSingle;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnSingleTestSel;
        private System.Windows.Forms.Button btnSingleTestExec;
    }
}

