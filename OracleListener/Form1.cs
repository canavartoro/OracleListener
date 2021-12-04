using OracleListener.Data;
using OracleListener.Log;
using OracleListener.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
                oraserv.Receved += Oraserv_Receved;
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

        private void GetDepoList()
        {
            Task.Run(() =>
            {
                List<DepoModel> depots = null;
                string depotlist = FileHelper.ReadFile("depots.xml");
                if (!string.IsNullOrEmpty(depotlist))
                {
                    depots = FileHelper.FromXml(depotlist, typeof(List<DepoModel>)) as List<DepoModel>;
                }
                else
                {
                    using (OracleProvider db = new OracleProvider())
                    {
                        depots = db.Select<DepoModel>($@"SELECT WH.WHOUSE_ID,WH.WHOUSE_CODE,WH.DESCRIPTION WHOUSE_DESC,WH.ISPASSIVE,WH.ISNEGATIVE,WH.ENTITY_ID,WH.CREATE_DATE,WH.UPDATE_DATE 
FROM INVD_WHOUSE WH INNER JOIN INVD_BRANCH_WHOUSE BW ON WH.WHOUSE_ID = BW.WHOUSE_ID
WHERE BW.BRANCH_ID = {AppConfig.Default.BranchId}
ORDER BY WH.WHOUSE_CODE");
                        FileHelper.SaveFile("depots.xml", FileHelper.ToXml(depots));
                    }
                }
                if (depots != null)
                {
                    listView1.Invoke(new Action(() =>
                    {
                        listView1.BeginUpdate();
                        listView1.Items.Clear();
                        for (int i = 0; i < depots.Count; i++)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = $@"{depots[i].WHOUSE_CODE} {depots[i].WHOUSE_DESC}";
                            item.Tag = depots[i];
                            item.Checked = depots[i].SELECTED;
                            listView1.Items.Add(item);
                        }
                        listView1.EndUpdate();
                        Application.DoEvents();
                    }));
                }

            });
        }

        private void CheckItemMField()
        {
            Task.Run(() =>
            {
                if (!File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Templates)}\\CheckItemMField.log"))
                {
                    using (OracleProvider db = new OracleProvider())
                    {
                        var count = db.ExecuteScalar($"SELECT COUNT(*) ROW_COUNT FROM ALL_TAB_COLUMNS WHERE TABLE_NAME = 'INVT_ITEM_M' AND COLUMN_NAME = 'ZZ_DOCENTETE_ID'");
                        if (count == null || Convert.ToInt32(count) == 0)
                        {
                            db.Execute(@"ALTER TABLE ""UYUMSOFT"".""INVT_ITEM_M"" ADD (""ZZ_DOCENTETE_ID"" NUMBER)");
                        }
                    }
                    File.WriteAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.Templates)}\\CheckItemMField.log", DateTime.Now.ToString());
                }
            });
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
            numCoId.Value = AppConfig.Default.CoId;
            numBranchId.Value = AppConfig.Default.BranchId;
            checkOtostart.Checked = AppConfig.Default.Otostart;
            textCreateUserId.Text = AppConfig.Default.CreateUserId;
            textCreateUser.Text = AppConfig.Default.CreateUser;
            this.Text = string.Concat(Text, " V:", Program.Versiyon, " B:", Program.BuildNumber());
            lblStatu.Text = DateTime.Now.ToString();
            if (!string.IsNullOrWhiteSpace(AppConfig.Default.OracleHost) &&
                !string.IsNullOrWhiteSpace(AppConfig.Default.SqlHost))
            {
                if (AppConfig.Default.Otostart)
                    btnbaslat_Click(btnbaslat, EventArgs.Empty);
                else
                    btnbaslat.Enabled = true;
            }
            else
            {
                btnbaslat.Enabled = false;
            }
            GetDepoList();
            CheckItemMField();

            //using (var sync = new DataSynchronization())
            //{
            //    sync.IrsaliyeSynchronization("39341");
            //}

        }

        private void btnlogac_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", $"/select,\"{Application.StartupPath}\\app_trace.log\"");
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

                if (string.IsNullOrWhiteSpace(textCreateUserId.Text))
                {
                    errorProvider1.SetError(textCreateUserId, "Oluşturan Kullanıcı bilgisi boş bırakılamaz!");
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
                AppConfig.Default.CoId = Convert.ToInt32(numCoId.Value);
                AppConfig.Default.BranchId = Convert.ToInt32(numBranchId.Value);
                AppConfig.Default.Otostart = checkOtostart.Checked;
                AppConfig.Default.CreateUserId = textCreateUserId.Text;
                AppConfig.Default.CreateUser = textCreateUser.Text;
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

        private void btnlogtemizle_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void cariKartlarıAktarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var syncQueue = new RabbitMQManager(1))
            {
                DataSynchronizationModel synchronizationobj = new DataSynchronizationModel();
                synchronizationobj.Argument = "";
                synchronizationobj.Name = "CARI";
                syncQueue.Publish(synchronizationobj);
            }
        }

        private void stokKartlarıAktarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (var data = new DataSynchronization())
                {
                    data.StokSynchronization();
                }
            }
            catch (Exception ex)
            {
                Logger.E(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            //using (var syncQueue = new RabbitMQManager(1))
            //{
            //    DataSynchronizationModel synchronizationobj = new DataSynchronizationModel();
            //    synchronizationobj.Argument = "";
            //    synchronizationobj.Name = "STOK";
            //    syncQueue.Publish(synchronizationobj);
            //}
        }

        private void depoKartlarıAktarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (var data = new DataSynchronization())
                {
                    data.DepoSynchronization();
                }
            }
            catch (Exception ex)
            {
                Logger.E(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            //using (var syncQueue = new RabbitMQManager(1))
            //{
            //    DataSynchronizationModel synchronizationobj = new DataSynchronizationModel();
            //    synchronizationobj.Argument = "";
            //    synchronizationobj.Name = "DEPO";
            //    syncQueue.Publish(synchronizationobj);
            //}
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                FileHelper.DeleteFile("depots.xml");
                List<DepoModel> depots = new List<DepoModel>();
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    DepoModel depo = listView1.Items[i].Tag as DepoModel;
                    depo.SELECTED = listView1.Items[i].Checked;
                    depots.Add(depo);
                }
                FileHelper.SaveFile("depots.xml", FileHelper.ToXml(depots));

            }
            catch (Exception exc)
            {
                FileHelper.DeleteFile("depots.xml");
                Logger.E(exc);
            }
        }

        private void btndepo_Click(object sender, EventArgs e)
        {
            FileHelper.DeleteFile("depots.xml");
            GetDepoList();
        }
    }
}
