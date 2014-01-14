namespace Intems.Illuminate.HardwareTester
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
            this.cmbPortNames = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbRates = new System.Windows.Forms.ComboBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelColor = new System.Windows.Forms.Panel();
            this.txtBlueHex = new System.Windows.Forms.TextBox();
            this.txtGreenHex = new System.Windows.Forms.TextBox();
            this.txtRedHex = new System.Windows.Forms.TextBox();
            this.txtBlue = new System.Windows.Forms.TextBox();
            this.txtGreen = new System.Windows.Forms.TextBox();
            this.txtRed = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelColor1 = new System.Windows.Forms.Panel();
            this.txtBlue1Hex = new System.Windows.Forms.TextBox();
            this.txtGreen1Hex = new System.Windows.Forms.TextBox();
            this.txtRed1Hex = new System.Windows.Forms.TextBox();
            this.txtBlue1 = new System.Windows.Forms.TextBox();
            this.txtGreen1 = new System.Windows.Forms.TextBox();
            this.txtRed1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoTurnOff = new System.Windows.Forms.RadioButton();
            this.rdoTurnOn = new System.Windows.Forms.RadioButton();
            this.txtFreq = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rdoBlink = new System.Windows.Forms.RadioButton();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rdoFade = new System.Windows.Forms.RadioButton();
            this.rdoSetColor = new System.Windows.Forms.RadioButton();
            this.btnSend = new System.Windows.Forms.Button();
            this.dlgColor = new System.Windows.Forms.ColorDialog();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAddrAndPort = new System.Windows.Forms.TextBox();
            this.chkUseUdp = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtChannel = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbPortNames
            // 
            this.cmbPortNames.FormattingEnabled = true;
            this.cmbPortNames.Location = new System.Drawing.Point(12, 99);
            this.cmbPortNames.Name = "cmbPortNames";
            this.cmbPortNames.Size = new System.Drawing.Size(219, 21);
            this.cmbPortNames.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "COM Port";
            // 
            // cmbRates
            // 
            this.cmbRates.FormattingEnabled = true;
            this.cmbRates.Location = new System.Drawing.Point(246, 99);
            this.cmbRates.Name = "cmbRates";
            this.cmbRates.Size = new System.Drawing.Size(115, 21);
            this.cmbRates.TabIndex = 2;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(401, 97);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 3;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.OnBtnOpen_Click);
            // 
            // btnClose
            // 
            this.btnClose.Enabled = false;
            this.btnClose.Location = new System.Drawing.Point(513, 97);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.OnBtnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelColor);
            this.groupBox1.Controls.Add(this.txtBlueHex);
            this.groupBox1.Controls.Add(this.txtGreenHex);
            this.groupBox1.Controls.Add(this.txtRedHex);
            this.groupBox1.Controls.Add(this.txtBlue);
            this.groupBox1.Controls.Add(this.txtGreen);
            this.groupBox1.Controls.Add(this.txtRed);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label);
            this.groupBox1.Location = new System.Drawing.Point(12, 142);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 169);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "COLOR 1";
            // 
            // panelColor
            // 
            this.panelColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelColor.Location = new System.Drawing.Point(19, 33);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new System.Drawing.Size(190, 96);
            this.panelColor.TabIndex = 0;
            this.panelColor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnPanelColor_MouseDoubleClick);
            // 
            // txtBlueHex
            // 
            this.txtBlueHex.Enabled = false;
            this.txtBlueHex.Location = new System.Drawing.Point(362, 109);
            this.txtBlueHex.Name = "txtBlueHex";
            this.txtBlueHex.Size = new System.Drawing.Size(58, 20);
            this.txtBlueHex.TabIndex = 8;
            // 
            // txtGreenHex
            // 
            this.txtGreenHex.Enabled = false;
            this.txtGreenHex.Location = new System.Drawing.Point(362, 72);
            this.txtGreenHex.Name = "txtGreenHex";
            this.txtGreenHex.Size = new System.Drawing.Size(58, 20);
            this.txtGreenHex.TabIndex = 7;
            // 
            // txtRedHex
            // 
            this.txtRedHex.Enabled = false;
            this.txtRedHex.Location = new System.Drawing.Point(362, 35);
            this.txtRedHex.Name = "txtRedHex";
            this.txtRedHex.Size = new System.Drawing.Size(58, 20);
            this.txtRedHex.TabIndex = 6;
            // 
            // txtBlue
            // 
            this.txtBlue.Location = new System.Drawing.Point(267, 107);
            this.txtBlue.Name = "txtBlue";
            this.txtBlue.Size = new System.Drawing.Size(58, 20);
            this.txtBlue.TabIndex = 5;
            // 
            // txtGreen
            // 
            this.txtGreen.Location = new System.Drawing.Point(267, 70);
            this.txtGreen.Name = "txtGreen";
            this.txtGreen.Size = new System.Drawing.Size(58, 20);
            this.txtGreen.TabIndex = 4;
            // 
            // txtRed
            // 
            this.txtRed.Location = new System.Drawing.Point(267, 35);
            this.txtRed.Name = "txtRed";
            this.txtRed.Size = new System.Drawing.Size(58, 20);
            this.txtRed.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(240, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 20);
            this.label6.TabIndex = 2;
            this.label6.Text = "B";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(240, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "G";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label.Location = new System.Drawing.Point(240, 35);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(21, 20);
            this.label.TabIndex = 0;
            this.label.Text = "R";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panelColor1);
            this.groupBox2.Controls.Add(this.txtBlue1Hex);
            this.groupBox2.Controls.Add(this.txtGreen1Hex);
            this.groupBox2.Controls.Add(this.txtRed1Hex);
            this.groupBox2.Controls.Add(this.txtBlue1);
            this.groupBox2.Controls.Add(this.txtGreen1);
            this.groupBox2.Controls.Add(this.txtRed1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(12, 325);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(426, 169);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "COLOR 2";
            // 
            // panelColor1
            // 
            this.panelColor1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelColor1.Location = new System.Drawing.Point(13, 36);
            this.panelColor1.Name = "panelColor1";
            this.panelColor1.Size = new System.Drawing.Size(190, 96);
            this.panelColor1.TabIndex = 10;
            this.panelColor1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnPanelColor1_MouseDoubleClick);
            // 
            // txtBlue1Hex
            // 
            this.txtBlue1Hex.Enabled = false;
            this.txtBlue1Hex.Location = new System.Drawing.Point(356, 112);
            this.txtBlue1Hex.Name = "txtBlue1Hex";
            this.txtBlue1Hex.Size = new System.Drawing.Size(58, 20);
            this.txtBlue1Hex.TabIndex = 18;
            // 
            // txtGreen1Hex
            // 
            this.txtGreen1Hex.Enabled = false;
            this.txtGreen1Hex.Location = new System.Drawing.Point(356, 75);
            this.txtGreen1Hex.Name = "txtGreen1Hex";
            this.txtGreen1Hex.Size = new System.Drawing.Size(58, 20);
            this.txtGreen1Hex.TabIndex = 17;
            // 
            // txtRed1Hex
            // 
            this.txtRed1Hex.Enabled = false;
            this.txtRed1Hex.Location = new System.Drawing.Point(356, 38);
            this.txtRed1Hex.Name = "txtRed1Hex";
            this.txtRed1Hex.Size = new System.Drawing.Size(58, 20);
            this.txtRed1Hex.TabIndex = 16;
            // 
            // txtBlue1
            // 
            this.txtBlue1.Location = new System.Drawing.Point(261, 110);
            this.txtBlue1.Name = "txtBlue1";
            this.txtBlue1.Size = new System.Drawing.Size(58, 20);
            this.txtBlue1.TabIndex = 15;
            // 
            // txtGreen1
            // 
            this.txtGreen1.Location = new System.Drawing.Point(261, 73);
            this.txtGreen1.Name = "txtGreen1";
            this.txtGreen1.Size = new System.Drawing.Size(58, 20);
            this.txtGreen1.TabIndex = 14;
            // 
            // txtRed1
            // 
            this.txtRed1.Location = new System.Drawing.Point(261, 38);
            this.txtRed1.Name = "txtRed1";
            this.txtRed1.Size = new System.Drawing.Size(58, 20);
            this.txtRed1.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(234, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "B";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(234, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 20);
            this.label7.TabIndex = 11;
            this.label7.Text = "G";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(234, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 20);
            this.label8.TabIndex = 9;
            this.label8.Text = "R";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoTurnOff);
            this.groupBox3.Controls.Add(this.rdoTurnOn);
            this.groupBox3.Controls.Add(this.txtFreq);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.rdoBlink);
            this.groupBox3.Controls.Add(this.txtTime);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.rdoFade);
            this.groupBox3.Controls.Add(this.rdoSetColor);
            this.groupBox3.Location = new System.Drawing.Point(455, 142);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(195, 274);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Function";
            // 
            // rdoTurnOff
            // 
            this.rdoTurnOff.AutoSize = true;
            this.rdoTurnOff.Location = new System.Drawing.Point(17, 246);
            this.rdoTurnOff.Name = "rdoTurnOff";
            this.rdoTurnOff.Size = new System.Drawing.Size(79, 17);
            this.rdoTurnOff.TabIndex = 8;
            this.rdoTurnOff.TabStop = true;
            this.rdoTurnOff.Tag = "5";
            this.rdoTurnOff.Text = "TURN OFF";
            this.rdoTurnOff.UseVisualStyleBackColor = true;
            this.rdoTurnOff.CheckedChanged += new System.EventHandler(this.OnRadio_CheckedChanged);
            // 
            // rdoTurnOn
            // 
            this.rdoTurnOn.AutoSize = true;
            this.rdoTurnOn.Location = new System.Drawing.Point(17, 209);
            this.rdoTurnOn.Name = "rdoTurnOn";
            this.rdoTurnOn.Size = new System.Drawing.Size(75, 17);
            this.rdoTurnOn.TabIndex = 7;
            this.rdoTurnOn.TabStop = true;
            this.rdoTurnOn.Tag = "4";
            this.rdoTurnOn.Text = "TURN ON";
            this.rdoTurnOn.UseVisualStyleBackColor = true;
            this.rdoTurnOn.CheckedChanged += new System.EventHandler(this.OnRadio_CheckedChanged);
            // 
            // txtFreq
            // 
            this.txtFreq.Location = new System.Drawing.Point(77, 168);
            this.txtFreq.Name = "txtFreq";
            this.txtFreq.Size = new System.Drawing.Size(56, 20);
            this.txtFreq.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "F, Hz";
            // 
            // rdoBlink
            // 
            this.rdoBlink.AutoSize = true;
            this.rdoBlink.Location = new System.Drawing.Point(17, 138);
            this.rdoBlink.Name = "rdoBlink";
            this.rdoBlink.Size = new System.Drawing.Size(56, 17);
            this.rdoBlink.TabIndex = 4;
            this.rdoBlink.TabStop = true;
            this.rdoBlink.Tag = "3";
            this.rdoBlink.Text = "BLINK";
            this.rdoBlink.UseVisualStyleBackColor = true;
            this.rdoBlink.CheckedChanged += new System.EventHandler(this.OnRadio_CheckedChanged);
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(77, 100);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(56, 20);
            this.txtTime.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Time, sec";
            // 
            // rdoFade
            // 
            this.rdoFade.AutoSize = true;
            this.rdoFade.Location = new System.Drawing.Point(17, 70);
            this.rdoFade.Name = "rdoFade";
            this.rdoFade.Size = new System.Drawing.Size(53, 17);
            this.rdoFade.TabIndex = 1;
            this.rdoFade.TabStop = true;
            this.rdoFade.Tag = "2";
            this.rdoFade.Text = "FADE";
            this.rdoFade.UseVisualStyleBackColor = true;
            this.rdoFade.CheckedChanged += new System.EventHandler(this.OnRadio_CheckedChanged);
            // 
            // rdoSetColor
            // 
            this.rdoSetColor.AutoSize = true;
            this.rdoSetColor.Checked = true;
            this.rdoSetColor.Location = new System.Drawing.Point(17, 33);
            this.rdoSetColor.Name = "rdoSetColor";
            this.rdoSetColor.Size = new System.Drawing.Size(89, 17);
            this.rdoSetColor.TabIndex = 0;
            this.rdoSetColor.TabStop = true;
            this.rdoSetColor.Tag = "1";
            this.rdoSetColor.Text = "SET COLOR ";
            this.rdoSetColor.UseVisualStyleBackColor = true;
            this.rdoSetColor.CheckedChanged += new System.EventHandler(this.OnRadio_CheckedChanged);
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSend.Location = new System.Drawing.Point(455, 455);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(195, 39);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.OnBtnSend_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(12, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(123, 20);
            this.label9.TabIndex = 9;
            this.label9.Text = "UDP ipaddr:port";
            // 
            // txtAddrAndPort
            // 
            this.txtAddrAndPort.Location = new System.Drawing.Point(12, 32);
            this.txtAddrAndPort.Name = "txtAddrAndPort";
            this.txtAddrAndPort.Size = new System.Drawing.Size(219, 20);
            this.txtAddrAndPort.TabIndex = 10;
            // 
            // chkUseUdp
            // 
            this.chkUseUdp.AutoSize = true;
            this.chkUseUdp.Location = new System.Drawing.Point(250, 32);
            this.chkUseUdp.Name = "chkUseUdp";
            this.chkUseUdp.Size = new System.Drawing.Size(115, 17);
            this.chkUseUdp.TabIndex = 11;
            this.chkUseUdp.Text = "Use UDP transport";
            this.chkUseUdp.UseVisualStyleBackColor = true;
            this.chkUseUdp.CheckedChanged += new System.EventHandler(this.OnUdpCheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(460, 429);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Channel N";
            // 
            // txtChannel
            // 
            this.txtChannel.Location = new System.Drawing.Point(523, 426);
            this.txtChannel.Name = "txtChannel";
            this.txtChannel.Size = new System.Drawing.Size(127, 20);
            this.txtChannel.TabIndex = 13;
            this.txtChannel.Text = "1";
            this.txtChannel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 522);
            this.Controls.Add(this.txtChannel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.chkUseUdp);
            this.Controls.Add(this.txtAddrAndPort);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.cmbRates);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPortNames);
            this.Name = "Form1";
            this.Text = "FADE PROCESSOR";
            this.Load += new System.EventHandler(this.Form1_Load);
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

        private System.Windows.Forms.ComboBox cmbPortNames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbRates;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoSetColor;
        private System.Windows.Forms.RadioButton rdoFade;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdoBlink;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.TextBox txtFreq;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdoTurnOff;
        private System.Windows.Forms.RadioButton rdoTurnOn;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.ColorDialog dlgColor;
        private System.Windows.Forms.TextBox txtBlue;
        private System.Windows.Forms.TextBox txtGreen;
        private System.Windows.Forms.TextBox txtRed;
        private System.Windows.Forms.TextBox txtBlueHex;
        private System.Windows.Forms.TextBox txtGreenHex;
        private System.Windows.Forms.TextBox txtRedHex;
        private System.Windows.Forms.Panel panelColor;
        private System.Windows.Forms.Panel panelColor1;
        private System.Windows.Forms.TextBox txtBlue1Hex;
        private System.Windows.Forms.TextBox txtGreen1Hex;
        private System.Windows.Forms.TextBox txtRed1Hex;
        private System.Windows.Forms.TextBox txtBlue1;
        private System.Windows.Forms.TextBox txtGreen1;
        private System.Windows.Forms.TextBox txtRed1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAddrAndPort;
        private System.Windows.Forms.CheckBox chkUseUdp;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtChannel;
    }
}

