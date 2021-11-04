using OracleListener.Log;
using OracleListener.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OracleListener
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ~Form1()
        {
            oraserv?.Stop();
            oraserv?.Dispose();
            oraserv = null;
        }

        private Net.TcpServer oraserv;

        private void StartServer()
        {
            AppCache.WriteBoolean("START", true);
            btnbaslat.Enabled = false;
            btndurdur.Enabled = true;
            try
            {
                oraserv = new Net.TcpServer();
                oraserv.Receved += Oraserv_Receved; ;
                oraserv.Start();
            }
            catch (Exception ex)
            {
                Logger.E(ex);
            }


        }

        private void StopServer()
        {
            AppCache.WriteBoolean("START", false);
            btnbaslat.Enabled = true;
            btndurdur.Enabled = false;

            try
            {
                if (oraserv != null)
                {
                    oraserv.Stop();
                    oraserv.Dispose();
                    oraserv = null;
                }
            }
            catch
            {
            }

            //Process.GetCurrentProcess().Kill();
        }

        private void Oraserv_Receved(object sender, Net.TcpClient e)
        {
            //Task.Run(() => LoadData());
            //Logger.I(string.Concat("istek geldi:", e.Message));
        }

        private void timerStart_Tick(object sender, EventArgs e)
        {
            timerStart.Enabled = false;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            //this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, Screen.PrimaryScreen.Bounds.Height - this.Height);
            Trace.Listeners.Add(new Log.TextTraceListener(richTextBox1));
            Logger.I($"V:{Program.Versiyon} B:{Program.BuildNumber()} Log:{AppConfig.Default.TraceLevel}");
            Application.DoEvents();
            if (AppCache.ReadBoolean("START", false))
            {
                StartServer();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            numTcpPort.Value = AppConfig.Default.TcpPort;
            textOraHost.Text = AppConfig.Default.OracleHost;
            textOraDb.Text = AppConfig.Default.OracleDbName;
            textOraService.Text = AppConfig.Default.OracleServiceName;
            numOraPort.Value = AppConfig.Default.OraclePort;
            textSqlHost.Text = AppConfig.Default.SqlHost;
            textSqlDb.Text = AppConfig.Default.SqlDbName;
            textSqlUser.Text = AppConfig.Default.SqlUserId;
            textSqlPassword.Text = AppConfig.Default.GetSqlPassword();
            comboTrace.SelectedIndex = AppConfig.Default.TraceLevel;
            this.Text = string.Concat(Text, " V:", Program.Versiyon, " B:", Program.BuildNumber());
            lblStatu.Text = DateTime.Now.ToString();
            if (!string.IsNullOrWhiteSpace(AppConfig.Default.OracleHost) &&
                !string.IsNullOrWhiteSpace(AppConfig.Default.SqlHost))
            {

            }
        }

        private void btnlogac_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", $"/select,\"{Application.StartupPath}\\app_trace.txt\"");
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true;
            this.Show();
            notifyIcon1.Visible = false;
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                IntPtr hWnd = Process.GetCurrentProcess().MainWindowHandle;
                if (hWnd != User32.InvalidHandleValue)
                {
                    User32.SetForegroundWindow(hWnd);
                    User32.ShowWindow(hWnd, User32.SW_RESTORE);
                }
                System.Media.SystemSounds.Beep.Play();
            });
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                this.Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(2000, "Oracle Entegrasyon Servisi", "Oracle servisi aktif", ToolTipIcon.Info);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();

                bool error = false;

                if (string.IsNullOrWhiteSpace(textOraHost.Text))
                {
                    errorProvider1.SetError(textOraHost, "Oracle Host bilgisi boş bırakılamaz!");
                    error = true;
                }

                if (string.IsNullOrWhiteSpace(textOraDb.Text))
                {
                    errorProvider1.SetError(textOraDb, "Oracle Db bilgisi boş bırakılamaz!");
                    error = true;
                }

                if (string.IsNullOrWhiteSpace(textOraService.Text))
                {
                    errorProvider1.SetError(textOraService, "Oracle Servis bilgisi boş bırakılamaz!");
                    error = true;
                }

                if (numOraPort.Value == 0)
                {
                    errorProvider1.SetError(numOraPort, "Oracle Port bilgisi boş bırakılamaz!");
                    error = true;
                }

                if (numTcpPort.Value == 0)
                {
                    errorProvider1.SetError(numTcpPort, "Tcp Port bilgisi boş bırakılamaz!");
                    error = true;
                }

                if (string.IsNullOrWhiteSpace(textSqlHost.Text))
                {
                    errorProvider1.SetError(textSqlHost, "Sql Host bilgisi boş bırakılamaz!");
                    error = true;
                }

                if (string.IsNullOrWhiteSpace(textSqlDb.Text))
                {
                    errorProvider1.SetError(textSqlDb, "Sql Db bilgisi boş bırakılamaz!");
                    error = true;
                }

                if (string.IsNullOrWhiteSpace(textSqlUser.Text))
                {
                    errorProvider1.SetError(textSqlUser, "Sql Kullanıcı bilgisi boş bırakılamaz!");
                    error = true;
                }

                if (error) return;

                AppConfig.Default.TcpPort = Convert.ToInt32(numTcpPort.Value);
                AppConfig.Default.OracleHost = textOraHost.Text;
                AppConfig.Default.OracleDbName = textOraDb.Text;
                AppConfig.Default.OracleServiceName = textOraService.Text;
                AppConfig.Default.OraclePort = Convert.ToInt32(numOraPort.Value);
                AppConfig.Default.SqlHost = textSqlHost.Text;
                AppConfig.Default.SqlDbName = textSqlDb.Text;
                AppConfig.Default.SqlUserId = textSqlUser.Text;
                AppConfig.Default.SetSqlPassword(textSqlPassword.Text);
                AppConfig.Default.TraceLevel = comboTrace.SelectedIndex;
                AppConfig.Default.Save();

            }
            catch (Exception exc)
            {
                Logger.E(exc);
            }
        }

        private void btnbaslat_Click(object sender, EventArgs e)
        {
            StartServer();
        }

        private void btndurdur_Click(object sender, EventArgs e)
        {
            StopServer();
        }
    }
}
