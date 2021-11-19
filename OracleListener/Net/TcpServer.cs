using OracleListener.Data;
using OracleListener.Log;
using OracleListener.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OracleListener.Net
{
    //https://stackoverflow.com/questions/3609280/sending-and-receiving-data-over-a-network-using-tcpclient
    public class TcpServer : IDisposable
    {
        bool abortListener = false;
        TcpListener server = null;
        Thread threadlisten = null;

        public event DataReceved Receved;

        public void Start()
        {
            try
            {
                abortListener = true;
                server = new TcpListener(IPAddress.Any, AppConfig.Default.TcpPort);
                server.Start();

                threadlisten = new Thread(new ThreadStart(Listening));
                threadlisten.Start();

                Logger.I(" >> Server Started");

            }
            catch (Exception exc)
            {
                Logger.E(exc);
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]
        public void Stop()
        {
            try
            {
                abortListener = false;

                try
                {
                    Thread.Sleep(1000);
                    if (server != null)
                    {
                        server.Stop();
                    }
                }
                catch (Exception excstop) { Logger.E(excstop); }

                try
                {
                    Thread.Sleep(1000);
                    if (threadlisten != null && threadlisten.IsAlive) threadlisten.Abort();
                }
                catch (ThreadAbortException exctabort) { Logger.E(exctabort); }
                catch (Exception excabort) { Logger.E(excabort); }

                //Logger.I("Server durduruldu");
            }
            catch (Exception exc)
            {
                Logger.E(exc);
            }
        }

        private void Listening()
        {
            byte[] bytesFrom = new byte[2048];

            while (abortListener)
            {
                Thread.Sleep(1000);
                try
                {
                    using (System.Net.Sockets.TcpClient client = server.AcceptTcpClient())
                    {
                        using (NetworkStream stream = client.GetStream())
                        {
                            int i;
                            while ((i = stream.Read(bytesFrom, 0, bytesFrom.Length)) != 0)
                            {
                                try
                                {
                                    TcpClient receiveData = TcpClient.ParsData(bytesFrom);
                                    if (receiveData != null)
                                    {
                                        Task.Run(() => Run(receiveData));
                                        if (Receved != null) Receved(this, receiveData);
                                    }
                                }
                                catch (Exception e1)
                                {
                                    Logger.E(e1);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.E(ex);
                }
            }

            Logger.E(" >> exit");
        }

        private void Run(TcpClient receivedata)
        {
            try
            {
                Logger.I(receivedata.ToString());
                using (DataSynchronization data = new DataSynchronization())
                {
                    if (receivedata.Command.StartsWith(TcpClient.COMMAND_STOK))
                    {
                        data.StokSynchronization(receivedata.Name);
                    }
                    else if (receivedata.Command.StartsWith(TcpClient.COMMAND_DEPO))
                    {
                        data.DepoSynchronization(receivedata.Name);
                    }
                }
            }
            catch (Exception exc)
            {
                Logger.E(exc);
            }
        }

        #region IDisposable
        ~TcpServer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if (server != null)
                {
                    server.Stop();
                }
            }

            disposed = true;
        }

        #endregion
    }
}
