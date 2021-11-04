using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OracleListener.Log
{
    public class TextWriterTraceListener : TraceListener
    {
        private readonly StreamWriter Writer;

        public TextWriterTraceListener()
        {
            try
            {
                string trace = $"{Application.StartupPath}\\app_trace.txt";
                FileInfo fi = new FileInfo(trace);
                bool append = fi.Exists && fi.Length < 1000000;
                Writer = new StreamWriter(trace, append, Encoding.GetEncoding("windows-1254"));
                Writer.AutoFlush = true;
            }
            catch (Exception exc)
            {
                Logger.E(exc);
            }
        }

        ~TextWriterTraceListener()
        {
            try
            {
                if (Writer != null)
                    Writer.Close();
            }
            catch
            {
                ;
            }
        }

        public override void Write(string message)
        {
            try
            {
                Writer.Write("-> " + DateTime.Now.ToString() + "\t" + message);
            }
            catch (Exception exc)
            {
                Logger.E("Hata dosyasİna yazİlamAd:" + exc.Message);
            }
        }

        public override void WriteLine(string message)
        {
            try
            {
                Writer.WriteLine("-> " + DateTime.Now.ToString() + "\t" + message);
            }
            catch (Exception exc)
            {
                Logger.E("Hata kaydedilemedi:" + exc.Message);
            }
        }
    }
}
