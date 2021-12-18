namespace OracleListener
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnbaslat = new System.Windows.Forms.ToolStripButton();
            this.btndurdur = new System.Windows.Forms.ToolStripButton();
            this.btnlogtemizle = new System.Windows.Forms.ToolStripButton();
            this.btnlogac = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.stokKartlarıAktarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.depoKartlarıAktarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnalis = new System.Windows.Forms.ToolStripMenuItem();
            this.btnsatis = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatu = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.txtIrsaliyeNo = new System.Windows.Forms.TextBox();
            this.textCreateUser = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textCreateUserId = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkOtostart = new System.Windows.Forms.CheckBox();
            this.btndepo = new System.Windows.Forms.Button();
            this.numBranchId = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.numCoId = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
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
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.hesapPlanlarıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBranchId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCoId)).BeginInit();
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
            this.notifyIcon1.Text = "Oracle Tcp Server";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnbaslat,
            this.btndurdur,
            this.btnlogtemizle,
            this.btnlogac,
            this.toolStripButton1,
            this.toolStripSplitButton1});
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
            this.btnlogtemizle.Click += new System.EventHandler(this.btnlogtemizle_Click);
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
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stokKartlarıAktarToolStripMenuItem,
            this.depoKartlarıAktarToolStripMenuItem,
            this.btnalis,
            this.btnsatis,
            this.hesapPlanlarıToolStripMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(170, 24);
            this.toolStripSplitButton1.Text = "Manuel Aktarımlar";
            // 
            // stokKartlarıAktarToolStripMenuItem
            // 
            this.stokKartlarıAktarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("stokKartlarıAktarToolStripMenuItem.Image")));
            this.stokKartlarıAktarToolStripMenuItem.Name = "stokKartlarıAktarToolStripMenuItem";
            this.stokKartlarıAktarToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.stokKartlarıAktarToolStripMenuItem.Text = "Stok Kartları Aktar";
            this.stokKartlarıAktarToolStripMenuItem.Click += new System.EventHandler(this.stokKartlarıAktarToolStripMenuItem_Click);
            // 
            // depoKartlarıAktarToolStripMenuItem
            // 
            this.depoKartlarıAktarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("depoKartlarıAktarToolStripMenuItem.Image")));
            this.depoKartlarıAktarToolStripMenuItem.Name = "depoKartlarıAktarToolStripMenuItem";
            this.depoKartlarıAktarToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.depoKartlarıAktarToolStripMenuItem.Text = "Depo Kartları Aktar";
            this.depoKartlarıAktarToolStripMenuItem.Click += new System.EventHandler(this.depoKartlarıAktarToolStripMenuItem_Click);
            // 
            // btnalis
            // 
            this.btnalis.Image = ((System.Drawing.Image)(resources.GetObject("btnalis.Image")));
            this.btnalis.Name = "btnalis";
            this.btnalis.Size = new System.Drawing.Size(216, 26);
            this.btnalis.Text = "Alış Irsaliye Aktar";
            this.btnalis.Click += new System.EventHandler(this.btnalis_Click);
            // 
            // btnsatis
            // 
            this.btnsatis.Image = ((System.Drawing.Image)(resources.GetObject("btnsatis.Image")));
            this.btnsatis.Name = "btnsatis";
            this.btnsatis.Size = new System.Drawing.Size(216, 26);
            this.btnsatis.Text = "Satış Irsaliye Aktar";
            this.btnsatis.Click += new System.EventHandler(this.btnsatis_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatu});
            this.statusStrip1.Location = new System.Drawing.Point(0, 425);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStrip1.Size = new System.Drawing.Size(800, 25);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatu
            // 
            this.lblStatu.Name = "lblStatu";
            this.lblStatu.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblStatu.Size = new System.Drawing.Size(18, 20);
            this.lblStatu.Text = "...";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 296);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 129);
            this.panel1.TabIndex = 2;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(800, 129);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 294);
            this.splitter1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(800, 2);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.txtIrsaliyeNo);
            this.panel2.Controls.Add(this.textCreateUser);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.textCreateUserId);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.listView1);
            this.panel2.Controls.Add(this.checkOtostart);
            this.panel2.Controls.Add(this.btndepo);
            this.panel2.Controls.Add(this.numBranchId);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.numCoId);
            this.panel2.Controls.Add(this.label12);
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
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 267);
            this.panel2.TabIndex = 4;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(324, 209);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 17);
            this.label15.TabIndex = 34;
            this.label15.Text = "Irsaliye No :";
            // 
            // txtIrsaliyeNo
            // 
            this.txtIrsaliyeNo.Location = new System.Drawing.Point(417, 201);
            this.txtIrsaliyeNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIrsaliyeNo.Name = "txtIrsaliyeNo";
            this.txtIrsaliyeNo.Size = new System.Drawing.Size(137, 22);
            this.txtIrsaliyeNo.TabIndex = 33;
            this.txtIrsaliyeNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIrsaliyeNo_KeyDown);
            // 
            // textCreateUser
            // 
            this.textCreateUser.Location = new System.Drawing.Point(112, 202);
            this.textCreateUser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textCreateUser.Name = "textCreateUser";
            this.textCreateUser.Size = new System.Drawing.Size(137, 22);
            this.textCreateUser.TabIndex = 32;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 206);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(84, 17);
            this.label14.TabIndex = 31;
            this.label14.Text = "Kullanıcı Adı";
            // 
            // textCreateUserId
            // 
            this.textCreateUserId.Location = new System.Drawing.Point(112, 174);
            this.textCreateUserId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textCreateUserId.Name = "textCreateUserId";
            this.textCreateUserId.Size = new System.Drawing.Size(137, 22);
            this.textCreateUserId.TabIndex = 30;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 177);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 17);
            this.label11.TabIndex = 29;
            this.label11.Text = "Kullanıcı Kodu";
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(563, 27);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(233, 233);
            this.listView1.TabIndex = 28;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView1_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Depo Listesi";
            this.columnHeader1.Width = 220;
            // 
            // checkOtostart
            // 
            this.checkOtostart.AutoSize = true;
            this.checkOtostart.Location = new System.Drawing.Point(417, 176);
            this.checkOtostart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkOtostart.Name = "checkOtostart";
            this.checkOtostart.Size = new System.Drawing.Size(128, 21);
            this.checkOtostart.TabIndex = 27;
            this.checkOtostart.Text = "Otomatik başlat";
            this.checkOtostart.UseVisualStyleBackColor = true;
            // 
            // btndepo
            // 
            this.btndepo.Image = ((System.Drawing.Image)(resources.GetObject("btndepo.Image")));
            this.btndepo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btndepo.Location = new System.Drawing.Point(563, 1);
            this.btndepo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btndepo.Name = "btndepo";
            this.btndepo.Size = new System.Drawing.Size(235, 25);
            this.btndepo.TabIndex = 26;
            this.btndepo.Text = "Listeyi Güncelle";
            this.btndepo.UseVisualStyleBackColor = true;
            this.btndepo.Click += new System.EventHandler(this.btndepo_Click);
            // 
            // numBranchId
            // 
            this.numBranchId.Location = new System.Drawing.Point(417, 148);
            this.numBranchId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numBranchId.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numBranchId.Name = "numBranchId";
            this.numBranchId.Size = new System.Drawing.Size(93, 22);
            this.numBranchId.TabIndex = 25;
            this.numBranchId.Value = new decimal(new int[] {
            1421,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(299, 150);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(123, 17);
            this.label13.TabIndex = 24;
            this.label13.Text = "İş yeri Id (Branch):";
            // 
            // numCoId
            // 
            this.numCoId.Location = new System.Drawing.Point(417, 121);
            this.numCoId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numCoId.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numCoId.Name = "numCoId";
            this.numCoId.Size = new System.Drawing.Size(93, 22);
            this.numCoId.TabIndex = 23;
            this.numCoId.Value = new decimal(new int[] {
            1421,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(299, 122);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 17);
            this.label12.TabIndex = 22;
            this.label12.Text = "Firma Id (Co):";
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
            this.comboTrace.Location = new System.Drawing.Point(112, 146);
            this.comboTrace.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboTrace.Name = "comboTrace";
            this.comboTrace.Size = new System.Drawing.Size(137, 24);
            this.comboTrace.TabIndex = 18;
            // 
            // textSqlPassword
            // 
            this.textSqlPassword.Location = new System.Drawing.Point(417, 89);
            this.textSqlPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.textSqlUser.Location = new System.Drawing.Point(417, 62);
            this.textSqlUser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.textSqlDb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.textSqlHost.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.textOraDb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.numOraPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.textOraService.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textOraService.Name = "textOraService";
            this.textOraService.Size = new System.Drawing.Size(137, 22);
            this.textOraService.TabIndex = 5;
            // 
            // textOraHost
            // 
            this.textOraHost.Location = new System.Drawing.Point(112, 34);
            this.textOraHost.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textOraHost.Name = "textOraHost";
            this.textOraHost.Size = new System.Drawing.Size(137, 22);
            this.textOraHost.TabIndex = 3;
            // 
            // numTcpPort
            // 
            this.numTcpPort.Location = new System.Drawing.Point(112, 7);
            this.numTcpPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // hesapPlanlarıToolStripMenuItem
            // 
            this.hesapPlanlarıToolStripMenuItem.Name = "hesapPlanlarıToolStripMenuItem";
            this.hesapPlanlarıToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.hesapPlanlarıToolStripMenuItem.Text = "Hesap Planları";
            this.hesapPlanlarıToolStripMenuItem.Click += new System.EventHandler(this.hesapPlanlarıToolStripMenuItem_Click);
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            ((System.ComponentModel.ISupportInitialize)(this.numBranchId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCoId)).EndInit();
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
        private System.Windows.Forms.NumericUpDown numBranchId;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numCoId;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem stokKartlarıAktarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem depoKartlarıAktarToolStripMenuItem;
        private System.Windows.Forms.Button btndepo;
        private System.Windows.Forms.CheckBox checkOtostart;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TextBox textCreateUserId;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textCreateUser;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ToolStripMenuItem btnalis;
        private System.Windows.Forms.ToolStripMenuItem btnsatis;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtIrsaliyeNo;
        private System.Windows.Forms.ToolStripMenuItem hesapPlanlarıToolStripMenuItem;
    }
}

