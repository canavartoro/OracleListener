using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OracleQueueService.Utilities
{
    public class AppCache
    {
        private static string path = $"{AppDomain.CurrentDomain.BaseDirectory}\\PCConfig.ini";
        private static string section = "CONFIG";

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static void Write(string Key, string Value)
        {
            WritePrivateProfileString(section, Key, Value, path);
        }

        public static void WriteBoolean(string Key, bool Value)
        {
            Write(Key, Value ? "1" : "0");
        }

        public static string Read(string Key, string dvalue)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section, Key, "", temp, 255, path);
            if (i == 0)
            {
                Write(Key, dvalue);
                return dvalue;
            }
            return temp.ToString();
        }

        public static bool ReadBoolean(string Key, bool dvalue)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section, Key, "", temp, 255, path);
            if (i == 0)
            {
                Write(Key, dvalue ? "1" : "0");
                return dvalue;
            }
            return temp.ToString() == "1";
        }

        public static int ReadInteger(string Key, int dvalue)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section, Key, "", temp, 255, path);
            if (i == 0)
            {
                Write(Key, dvalue.ToString());
                return dvalue;
            }
            int iout = dvalue;
            int.TryParse(temp.ToString(), out iout);
            return iout;
        }

    }
}
