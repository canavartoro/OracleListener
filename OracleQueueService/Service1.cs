using Newtonsoft.Json;
using OracleQueueService.Data;
using OracleQueueService.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace OracleQueueService
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;
        private static RabbitMQManager queueConsumer;
        //System.Diagnostics.EventLog eventLog1;
        private int eventId = 1011;
        public Service1()
        {
            InitializeComponent();
            //eventLog1 = new System.Diagnostics.EventLog();
            //if (!System.Diagnostics.EventLog.SourceExists("QueueService"))
            //{
            //    System.Diagnostics.EventLog.CreateEventSource(
            //        "QueueService", "QueueServiceLog");
            //}
            //eventLog1.Source = "QueueService";
            //eventLog1.Log = "QueueServiceLog";
        }

        protected override void OnStart(string[] args)
        {
            Logger.I("Servis başladı");
            timer = new Timer();
            timer.Interval = 20000; // 20 seconds
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();

            queueConsumer = new RabbitMQManager(1);
            queueConsumer.Received += QueueConsumer_Received; ;
            queueConsumer.Consume();
        }

        private void QueueConsumer_Received(object sender, RabbitMQ.Client.Events.BasicDeliverEventArgs e)
        {
            if (e != null)
            {
                //Monitor.Enter(activityLock);
                var message = Encoding.UTF8.GetString(e.Body);
                Logger.V($"Activity received:{message}");
                if (!string.IsNullOrEmpty(message))
                    try
                    {
                        using (var db = new Data.DataSynchronization())
                        {
                            if (!db.Connect()) Logger.E("Veritabanına bağlanılamadı!");

                            var synchronizationObj = JsonConvert.DeserializeObject<DataSynchronizationModel>(message);
                            if (synchronizationObj != null)
                            {
                                if (db.ExecuteScalar("SELECT  \"sp_prdt_automation_ac_time\"(@automationdevicedid, @wstationid, @wstation_code, @cnt, @cntdiff, @ac_time, @ac_time_diff)") != null)
                                {
                                    queueConsumer.BasicAck(e.DeliveryTag);
                                    Logger.I($"ors.opcclient.activity-->{synchronizationObj.Argument},{synchronizationObj.Name}");
                                }
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        Logger.E(exc);
                    }
                //Monitor.Exit(activityLock);
            }
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            Logger.I("Servis activities");
            // TODO: Insert monitoring activities here.
            //eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }

        protected override void OnStop()
        {
            queueConsumer?.Dispose();
            queueConsumer = null;
            Logger.I("Servis durduruldu");
            timer.Stop();
            //eventLog1.WriteEntry("In OnStop.");
        }

        protected override void OnContinue()
        {
            Logger.I("Servis devam");
            timer.Start();
            //eventLog1.WriteEntry("In OnContinue.");
        }
    }
}
