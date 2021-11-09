using OracleListener.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OracleListener
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string Project = System.IO.Path.GetFileNameWithoutExtension(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            System.Diagnostics.Process[] Prc = System.Diagnostics.Process.GetProcessesByName(Project);

            if (Prc.Length > 1)
            {
                MessageBox.Show("Oracle TCP servisi, hafızada ya da diğer kullanıcılarda çalışmaktadır.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                Application.ExitThread();
                Process.GetCurrentProcess().Kill();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            CultureInfo enUsCulture = new CultureInfo("en-US", false);
            enUsCulture.NumberFormat.CurrencyDecimalDigits = 2;
            System.Threading.Thread.CurrentThread.CurrentCulture = enUsCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = enUsCulture;

            System.Diagnostics.Trace.Listeners.Add(new Log.TextWriterTraceListener());

            Application.Run(new Form1());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.E(e.ExceptionObject);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {

            string error = "Bir sistem hatası oluştu!\nBu hatayı sistem yöneticinize bildirin!\n" + e.Exception.ToString();

            Logger.E(error);
            //System.Diagnostics.EventLog ev = new System.Diagnostics.EventLog("Application", System.Environment.MachineName, dom);

            //ev.WriteEntry(error, System.Diagnostics.EventLogEntryType.Error, 0);

            //ev.Close();

            Application.ExitThread();
        }

        static string versiyon = null;

        public static string Versiyon
        {
            get
            {
                if (versiyon == null)
                {
                    Assembly entryPoint = Assembly.GetEntryAssembly();
                    AssemblyName entryPointName = entryPoint.GetName();
                    Version entryPointVersion = entryPointName.Version;
                    versiyon = string.Format("{0}", entryPointVersion.ToString());
                }
                return versiyon;
            }
        }

        public static string BuildNumber()
        {
            var assembly = Assembly.GetEntryAssembly();
            var stream = assembly.GetManifestResourceStream("OracleListener.build.txt");
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd().Replace("\r", "").Replace("\n","");
            }
        }

        public static object GetParamValue(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return DBNull.Value;
            else return value;
        }
    }
}
