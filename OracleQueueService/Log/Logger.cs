using OracleQueueService.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleQueueService.Log
{
    public class Logger
    {
        #region Write
        [DebuggerStepThrough()]
        public static void Write(Exception exception)
        {
            Trace.WriteLine(string.Concat("Trace :: ", DateTime.Now.ToString(), ", Exception: ", exception.Message, ", StackTrace:", exception.StackTrace));
        }
        [DebuggerStepThrough()]
        public static void Write(string str)
        {
            Trace.WriteLine(string.Concat("Trace :: ", DateTime.Now.ToString(), ", Mesaj: ", str));
        }
        [DebuggerStepThrough()]
        public static void Write(object obj)
        {
            Trace.WriteLine(string.Concat("Trace :: ", DateTime.Now.ToString(), ", Object: ", obj != null ? obj.ToString() : "null"));
        }
        #endregion

        #region Verbose
        [DebuggerStepThrough()]
        public static void V(Exception exception)
        {
            if (AppConfig.Default.TraceLevel >= 4)
            {
                Debug.WriteLine(string.Concat("Verbose :: ", DateTime.Now.ToString(), ", Exception: ", exception.Message, ", StackTrace:", exception.StackTrace));
            }
        }
        [DebuggerStepThrough()]
        public static void V(string str)
        {
            if (AppConfig.Default.TraceLevel >= 4)
            {
                Trace.WriteLine(string.Concat("Verbose :: ", DateTime.Now.ToString(), ", Mesaj: ", str));
            }
        }
        [DebuggerStepThrough()]
        public static void V(object obj)
        {
            if (AppConfig.Default.TraceLevel >= 4)
            {
                Trace.WriteLine(string.Concat("Verbose :: ", DateTime.Now.ToString(), ", Object: ", obj != null ? obj.ToString() : "null"));
            }
        }
        #endregion

        #region Info
        [DebuggerStepThrough()]
        public static void I(Exception exception)
        {
            if (AppConfig.Default.TraceLevel >= 3)
            {
                Trace.WriteLine(string.Concat("Info :: ", DateTime.Now.ToString(), ", Exception: ", exception.Message, ", StackTrace:", exception.StackTrace));
            }
        }
        [DebuggerStepThrough()]
        public static void I(string str)
        {
            if (AppConfig.Default.TraceLevel >= 3)
            {
                Trace.WriteLine(string.Concat("Info :: ", DateTime.Now.ToString(), ", Mesaj: ", str));
            }
        }
        [DebuggerStepThrough()]
        public static void I(object obj)
        {
            if (AppConfig.Default.TraceLevel >= 3)
            {
                Trace.WriteLine(string.Concat("Info :: ", DateTime.Now.ToString(), ", Object: ", obj != null ? obj.ToString() : "null"));
            }
        }
        #endregion

        #region Warning
        [DebuggerStepThrough()]
        public static void W(Exception exception)
        {
            if (AppConfig.Default.TraceLevel >= 2)
            {
                Trace.WriteLine(string.Concat("Warning :: ", DateTime.Now.ToString(), ", Exception: ", exception.Message, ", StackTrace:", exception.StackTrace));
            }
        }
        [DebuggerStepThrough()]
        public static void W(string str)
        {
            if (AppConfig.Default.TraceLevel >= 2)
            {
                Trace.WriteLine(string.Concat("Warning :: ", DateTime.Now.ToString(), ", Mesaj: ", str));
            }
        }
        [DebuggerStepThrough()]
        public static void W(object obj)
        {
            if (AppConfig.Default.TraceLevel >= 2)
            {
                Trace.WriteLine(string.Concat("Warning :: ", DateTime.Now.ToString(), ", Object: ", obj != null ? obj.ToString() : "null"));
            }
        }
        #endregion

        #region Error
        [DebuggerStepThrough()]
        public static void E(Exception exception)
        {
            if (AppConfig.Default.TraceLevel >= 1)
            {
                Debug.WriteLine(string.Concat("Error :: ", DateTime.Now.ToString(), ", Exception: ", exception.Message, ", StackTrace:", exception.StackTrace));
            }

        }
        [DebuggerStepThrough()]
        public static void E(string str)
        {
            if (AppConfig.Default.TraceLevel >= 1)
            {
                Trace.WriteLine(string.Concat("Error :: ", DateTime.Now.ToString(), ", Mesaj: ", str));
            }
        }
        [DebuggerStepThrough()]
        public static void E(object obj)
        {
            if (AppConfig.Default.TraceLevel >= 1)
            {
                Trace.WriteLine(string.Concat("Error :: ", DateTime.Now.ToString(), ", Object: ", obj != null ? obj.ToString() : "null"));
            }
        }
        #endregion
    }
}
