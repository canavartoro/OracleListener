using OracleListener.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OracleListener.Net
{
    [Serializable]
    public class TcpClient
    {
        public const string COMMAND_STOK = "STOK";
        public const string COMMAND_CARI = "CARI";
        public const string COMMAND_DEPO = "DEPO";
        public TcpClient() { }

        [System.Xml.Serialization.XmlIgnore]
        System.Net.Sockets.TcpClient tcpClient = null;

        [System.Xml.Serialization.XmlIgnore]
        NetworkStream networkStream = null;

        public string Name { get; set; } = "";
        public string Message { get; set; } = "";
        public string Data { get; set; } = "";
        public string Command { get; set; } = "";
        public string Outher { get; set; } = "";

        [System.Xml.Serialization.XmlIgnore]
        public System.Net.Sockets.TcpClient Client
        {
            get
            {
                return tcpClient;
            }
            set
            {
                tcpClient = value;
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public NetworkStream Stream
        {
            get
            {
                return networkStream;
            }
            set
            {
                networkStream = value;
            }
        }

        public void SendMessage()
        {
            try
            {
                Thread.Sleep(100);

                if (networkStream != null && networkStream.CanWrite)
                {
                    string message = string.Concat(this.Name, "|", this.Command, "|", this.Message, "|", this.Data);
                    Byte[] sendBytes = Encoding.GetEncoding("Windows-1254").GetBytes(message);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();

                    Logger.I(string.Concat(Name, " >> ", message));
                }
            }
            catch (Exception exc)
            {
                Logger.E(exc);
            }

        }

        public override string ToString()
        {
            return $"Command:{Command},ARGS:{Name},OUTHER:{Outher}";
        }

        public static TcpClient ParsData(byte[] bytesdata)
        {
            if (bytesdata == null) return null;

            String strdata = Encoding.GetEncoding("Windows-1254").GetString(bytesdata);
            Logger.I(strdata);
            TcpClient client = null;
            if (!string.IsNullOrWhiteSpace(strdata) && strdata.IndexOf("|") != -1)
            {
                client = new TcpClient();
                string[] arrdata = strdata.Split('|');
                if (arrdata != null)
                {
                    //123344|STOK
                    if (arrdata.Length > 0)
                        client.Name = arrdata[0];
                    if (arrdata.Length > 1)
                        client.Command = arrdata[1];
                    if (arrdata.Length > 2)
                        client.Message = arrdata[2];
                    if (arrdata.Length > 3)
                        client.Data = arrdata[3];
                    if (arrdata.Length > 4)
                        client.Outher = arrdata[4];
                }
            }

            return client;

        }
    }
}
