using OracleListener.Data;
using OracleListener.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace OracleListener.Utilities
{
    [XmlRoot("SETTINGS")]
    //[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public class AppConfig
    {
        public AppConfig() { }

        private static AppConfig _default = null;
        public static AppConfig Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new AppConfig();
                    _default.Load();
                }
                return _default;
            }
        }

        [XmlElement("TCP_PORT")]
        public int TcpPort { get; set; } = 2001;

        [XmlElement("ORA_HOST")]
        public string OracleHost { get; set; }

        [XmlElement("ORA_PORT")]
        public int OraclePort { get; set; } = 1521;

        [XmlElement("ORA_SERVICE")]
        public string OracleServiceName { get; set; }

        [XmlElement("ORA_DB")]
        public string OracleDbName { get; set; }

        [XmlElement("SQL_HOST")]
        public string SqlHost { get; set; }

        [XmlElement("SQL_DB")]
        public string SqlDbName { get; set; }

        [XmlElement("SQL_USER")]
        public string SqlUserId { get; set; }

        [XmlElement("SQL_PASSW")]
        public string SqlPassword { get; set; }

        [XmlElement("APP_LOG")]
        public int TraceLevel { get; set; } = 3;

        public string GetOracleConnectionString()
        {
            //return $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={_default.OracleHost})(PORT={_default.OraclePort})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={_default.OracleServiceName})));User Id=uyumsoft;Password=uyumsoft";
            return string.Format(OracleProvider.CONNECTION_STRING, _default.OracleHost, _default.OraclePort, _default.OracleServiceName);
        }

        public string GetSqlConnectionString()
        {
            return string.Format(SqlClient.CONNECTION_STRING, _default.SqlHost, _default.SqlDbName, _default.SqlUserId, _default.GetSqlPassword());
        }

        public void SetSqlPassword(string passw)
        {
            SqlPassword = Encrypt(passw);
        }

        public string GetSqlPassword()
        {
            return Decrypt(SqlPassword);
        }

        public void Load()
        {
            try
            {
                string configFile = $"{Application.StartupPath}\\config.xml";
                if (File.Exists(configFile))
                {
                    using (Stream stream = new FileStream(configFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        XmlSerializer xmlserializer = new XmlSerializer(typeof(AppConfig));
                        _default = (AppConfig)xmlserializer.Deserialize(stream);
                        stream.Close();
                    }
                }
                else
                {
                    Save();
                }
            }
            catch (Exception exc)
            {
                Logger.E(exc);
            }
        }

        public void Save()
        {
            try
            {
                using (Stream stream = new FileStream($"{Application.StartupPath}\\config.xml", FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    XmlSerializer xmlserializer = new XmlSerializer(typeof(AppConfig));
                    xmlserializer.Serialize(stream, AppConfig.Default);
                    stream.Close();
                }
            }
            catch (Exception exc)
            {
                Logger.E(exc);
            }
        }

        public static string Encrypt(string clearText)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(clearText))
                {
                    string EncryptionKey = "Smh!Grbz*.";
                    byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                    using (Aes encryptor = Aes.Create())
                    {
                        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                        encryptor.Key = pdb.GetBytes(32);
                        encryptor.IV = pdb.GetBytes(16);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(clearBytes, 0, clearBytes.Length);
                            }
                            clearText = Convert.ToBase64String(ms.ToArray());
                        }
                    }
                }
                else
                {
                    clearText = string.Empty;
                }
                return clearText;
            }
            catch
            {
            }
            return "";
        }

        public static string Decrypt(string cipherText)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(cipherText))
                {
                    string EncryptionKey = "Smh!Grbz*.";
                    cipherText = cipherText.Replace(" ", "+");
                    byte[] cipherBytes = Convert.FromBase64String(cipherText);
                    using (Aes encryptor = Aes.Create())
                    {
                        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                        encryptor.Key = pdb.GetBytes(32);
                        encryptor.IV = pdb.GetBytes(16);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(cipherBytes, 0, cipherBytes.Length);
                            }
                            cipherText = Encoding.Unicode.GetString(ms.ToArray());
                        }
                    }
                }
                else
                {
                    cipherText = string.Empty;
                }
                return cipherText;
            }
            catch
            {
            }
            return "";
        }
    }
}
