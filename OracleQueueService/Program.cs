using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OracleQueueService
{
    //%windir%\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe QueueService.exe
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            System.Diagnostics.Trace.Listeners.Add(new OracleQueueService.Log.TextWriterTraceListener());
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
        }
        
        public static string AppPath
        {
            get
            {
                try
                {
                    string _Path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\";
                    if (_Path.StartsWith("file"))
                    {
                        var xDevice = new Regex("[A-Z]");
                        string xPath = _Path.Replace("file:\\", "").Substring(0, 1);
                        if (xDevice.IsMatch(xPath))
                            _Path = _Path.Replace("file:\\", "");
                    }
                    return _Path;
                }
                catch
                {
                    return "";
                }
            }
        }
    }
}
