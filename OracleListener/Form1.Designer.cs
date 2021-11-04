﻿namespace OracleListener
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnbaslat = new System.Windows.Forms.ToolStripButton();
            this.btndurdur = new System.Windows.Forms.ToolStripButton();
            this.btnlogtemizle = new System.Windows.Forms.ToolStripButton();
            this.btnlogac = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.comboTrace = new System.Windows.Forms.ComboBox();
            this.textSqlPassword = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textSqlUser = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textSqlDb = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textSqlHost = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textOraDb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numOraPort = new System.Windows.Forms.NumericUpDown();
            this.textOraService = new System.Windows.Forms.TextBox();
            this.textOraHost = new System.Windows.Forms.TextBox();
            this.numTcpPort = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timerStart = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblStatu = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOraPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTcpPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "Oracle data entegrasyon servisi";
            this.notifyIcon1.BalloonTipTitle = "Oracle Tcp Server";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnbaslat,
            this.btndurdur,
            this.btnlogtemizle,
            this.btnlogac,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnbaslat
            // 
            this.btnbaslat.CheckOnClick = true;
            this.btnbaslat.Image = ((System.Drawing.Image)(resources.GetObject("btnbaslat.Image")));
            this.btnbaslat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnbaslat.Name = "btnbaslat";
            this.btnbaslat.Size = new System.Drawing.Size(73, 24);
            this.btnbaslat.Text = "Başlat";
            this.btnbaslat.Click += new System.EventHandler(this.btnbaslat_Click);
            // 
            // btndurdur
            // 
            this.btndurdur.CheckOnClick = true;
            this.btndurdur.Image = ((System.Drawing.Image)(resources.GetObject("btndurdur.Image")));
            this.btndurdur.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btndurdur.Name = "btndurdur";
            this.btndurdur.Size = new System.Drawing.Size(79, 24);
            this.btndurdur.Text = "Durdur";
            this.btndurdur.Click += new System.EventHandler(this.btndurdur_Click);
            // 
            // btnlogtemizle
            // 
            this.btnlogtemizle.Image = ((System.Drawing.Image)(resources.GetObject("btnlogtemizle.Image")));
            this.btnlogtemizle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnlogtemizle.Name = "btnlogtemizle";
            this.btnlogtemizle.Size = new System.Drawing.Size(134, 24);
            this.btnlogtemizle.Text = "Logları Temizle";
            // 
            // btnlogac
            // 
            this.btnlogac.Image = ((System.Drawing.Image)(resources.GetObject("btnlogac.Image")));
            this.btnlogac.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnlogac.Name = "btnlogac";
            this.btnlogac.Size = new System.Drawing.Size(79, 24);
            this.btnlogac.Text = "Log Aç";
            this.btnlogac.Click += new System.EventHandler(this.btnlogac_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(134, 24);
            this.toolStripButton1.Text = "Ayarları Kaydet";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatu});
            this.statusStrip1.Location = new System.Drawing.Point(0, 425);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 25);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 296);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 129);
            this.panel1.TabIndex = 2;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(800, 129);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 293);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(800, 3);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.comboTrace);
            this.panel2.Controls.Add(this.textSqlPassword);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.textSqlUser);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.textSqlDb);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.textSqlHost);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.textOraDb);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.numOraPort);
            this.panel2.Controls.Add(this.textOraService);
            this.panel2.Controls.Add(this.textOraHost);
            this.panel2.Controls.Add(this.numTcpPort);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 266);
            this.panel2.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 150);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 17);
            this.label10.TabIndex = 19;
            this.label10.Text = "Log Seviyesi:";
            // 
            // comboTrace
            // 
            this.comboTrace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTrace.FormattingEnabled = true;
            this.comboTrace.Items.AddRange(new object[] {
            "Kapalı",
            "Hata Mesajları",
            "Uyarı Mesajları",
            "Bilgi Mesajları",
            "Tüm Loglar"});
            this.comboTrace.Location = new System.Drawing.Point(112, 147);
            this.comboTrace.Name = "comboTrace";
            this.comboTrace.Size = new System.Drawing.Size(137, 24);
            this.comboTrace.TabIndex = 18;
            // 
            // textSqlPassword
            // 
            this.textSqlPassword.Location = new System.Drawing.Point(417, 89);
            this.textSqlPassword.Name = "textSqlPassword";
            this.textSqlPassword.PasswordChar = '#';
            this.textSqlPassword.Size = new System.Drawing.Size(137, 22);
            this.textSqlPassword.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(317, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 17);
            this.label9.TabIndex = 16;
            this.label9.Text = "Sql Parola:";
            // 
            // textSqlUser
            // 
            this.textSqlUser.Location = new System.Drawing.Point(417, 61);
            this.textSqlUser.Name = "textSqlUser";
            this.textSqlUser.Size = new System.Drawing.Size(137, 22);
            this.textSqlUser.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(317, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 17);
            this.label8.TabIndex = 14;
            this.label8.Text = "Sql Kullanici:";
            // 
            // textSqlDb
            // 
            this.textSqlDb.Location = new System.Drawing.Point(417, 33);
            this.textSqlDb.Name = "textSqlDb";
            this.textSqlDb.Size = new System.Drawing.Size(137, 22);
            this.textSqlDb.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(317, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "Sql Db:";
            // 
            // textSqlHost
            // 
            this.textSqlHost.Location = new System.Drawing.Point(417, 7);
            this.textSqlHost.Name = "textSqlHost";
            this.textSqlHost.Size = new System.Drawing.Size(137, 22);
            this.textSqlHost.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(317, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Sql Host:";
            // 
            // textOraDb
            // 
            this.textOraDb.Location = new System.Drawing.Point(112, 119);
            this.textOraDb.Name = "textOraDb";
            this.textOraDb.Size = new System.Drawing.Size(137, 22);
            this.textOraDb.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Oracle Db:";
            // 
            // numOraPort
            // 
            this.numOraPort.Location = new System.Drawing.Point(112, 63);
            this.numOraPort.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numOraPort.Name = "numOraPort";
            this.numOraPort.Size = new System.Drawing.Size(93, 22);
            this.numOraPort.TabIndex = 7;
            this.numOraPort.Value = new decimal(new int[] {
            1421,
            0,
            0,
            0});
            // 
            // textOraService
            // 
            this.textOraService.Location = new System.Drawing.Point(112, 91);
            this.textOraService.Name = "textOraService";
            this.textOraService.Size = new System.Drawing.Size(137, 22);
            this.textOraService.TabIndex = 5;
            // 
            // textOraHost
            // 
            this.textOraHost.Location = new System.Drawing.Point(112, 35);
            this.textOraHost.Name = "textOraHost";
            this.textOraHost.Size = new System.Drawing.Size(137, 22);
            this.textOraHost.TabIndex = 3;
            // 
            // numTcpPort
            // 
            this.numTcpPort.Location = new System.Drawing.Point(112, 7);
            this.numTcpPort.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numTcpPort.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numTcpPort.Name = "numTcpPort";
            this.numTcpPort.Size = new System.Drawing.Size(93, 22);
            this.numTcpPort.TabIndex = 1;
            this.numTcpPort.Value = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Oracle Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Oracle Servis:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Oracle Host:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tcp Portu:";
            // 
            // timerStart
            // 
            this.timerStart.Enabled = true;
            this.timerStart.Interval = 10;
            this.timerStart.Tick += new System.EventHandler(this.timerStart_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // lblStatu
            // 
            this.lblStatu.Name = "lblStatu";
            this.lblStatu.Size = new System.Drawing.Size(18, 20);
            this.lblStatu.Text = "...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Oracle Listener";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOraPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTcpPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer timerStart;
        private System.Windows.Forms.ToolStripButton btnbaslat;
        private System.Windows.Forms.ToolStripButton btndurdur;
        private System.Windows.Forms.ToolStripButton btnlogtemizle;
        private System.Windows.Forms.ToolStripButton btnlogac;
        private System.Windows.Forms.NumericUpDown numTcpPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textOraHost;
        private System.Windows.Forms.NumericUpDown numOraPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textOraService;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textOraDb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textSqlHost;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textSqlDb;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textSqlUser;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textSqlPassword;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboTrace;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatu;
    }
}

