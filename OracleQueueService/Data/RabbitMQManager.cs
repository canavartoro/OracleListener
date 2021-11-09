using Newtonsoft.Json;
using OracleQueueService.Log;
using OracleQueueService.Utilities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OracleQueueService.Data
{
    public class RabbitMQManager : IDisposable
    {
        public readonly object lockQueue = new object();
        private IConnection connection = null;
        private IModel channel = null;
        public event EventHandler<BasicDeliverEventArgs> Received;
        string QueueName = "ora.datasynchronization";
        ushort PrefetchCount = 10;


        public RabbitMQManager()
        {
        }

        public RabbitMQManager(ushort prefetchCount)
        {
            PrefetchCount = prefetchCount;
            CreateConnection();
        }

        public bool IsConnected
        {
            get
            {
                return ((connection != null && connection.IsOpen) && (channel != null && channel.IsOpen));
            }
        }

        public void CreateConnection()
        {
            Monitor.Enter(lockQueue);
            try
            {
                if (channel != null)
                {
                    if (channel.IsOpen) channel.Close();
                    channel.Dispose();
                }
                if (connection != null)
                {
                    if (connection.IsOpen) connection.Close();
                    connection.Dispose();
                }

                Logger.I($"Rabbit bağlantısı kuruluyor. {QueueName}");
                var factory = new ConnectionFactory()
                {
                    HostName = AppConfig.Default.MQHostName,
                    UserName = AppConfig.Default.MQUserName,
                    Password = AppConfig.Default.MQPassword,
                    AutomaticRecoveryEnabled = true,
                    TopologyRecoveryEnabled = false
                };
                connection = factory.CreateConnection();
                connection.AutoClose = false;
                factory.AutomaticRecoveryEnabled = true;
                factory.TopologyRecoveryEnabled = false;
                factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(10);

                #region Connection
                connection.CallbackException += (object sender, CallbackExceptionEventArgs e) =>
                {
                    Logger.E($"Rabbit bağlantısı koptu! {QueueName}");
                    if (e.Exception != null)
                        Logger.E(e.Exception);

                    //MailHelper.SendMail(MailHelper.Adresler, MailHelper.MailBaslik, "Rabbit bağlantısı koptu! (Activity) ");
                    if (connection != null)
                    {
                        if (connection.IsOpen)
                            connection.Close();
                        connection.Dispose();
                    }
                    connection = null;
                };
                connection.ConnectionBlocked += (object sender, ConnectionBlockedEventArgs e) =>
                {
                    Logger.E($"Rabbit bağlantısı engellendi! {QueueName}");
                    Logger.E(e.Reason);

                    //MailHelper.SendMail(MailHelper.Adresler, MailHelper.MailBaslik, "Rabbit bağlantısı engellendi! (Activity)");
                    if (connection != null)
                    {
                        if (connection.IsOpen)
                            connection.Close();
                        connection.Dispose();
                    }
                    connection = null;
                };
                /*connection.ConnectionShutdown += (object sender, ShutdownEventArgs e) =>
                {
                    Logger.E("Rabbit bağlantısı kapandi! (Activity)");

                    //MailHelper.SendMail(MailHelper.Adresler, MailHelper.MailBaslik, "Rabbit bağlantısı kapandi! (Activity)");
                    if (connection != null)
                    {
                        if (connection.IsOpen)
                            connection.Close();
                        connection.Dispose();
                    }
                    connection = null;
                };*/
                connection.ConnectionUnblocked += (object sender, EventArgs e) =>
                {
                    Logger.E($"Rabbit bağlantısı engel kalkti! {QueueName}");
                    //MailHelper.SendMail(MailHelper.Adresler, MailHelper.MailBaslik, "Rabbit bağlantısı engel kalkti!");
                };
                #endregion
                #region Channel
                channel = connection.CreateModel();
                channel.CallbackException += (object sender, CallbackExceptionEventArgs e) =>
                {
                    Logger.E($"Rabbit callback hatası! {QueueName}");
                    if (e.Exception != null)
                        Logger.E(e.Exception);

                    //MailHelper.SendMail(MailHelper.Adresler, MailHelper.MailBaslik, "Rabbit callback hatası! (Activity)");
                };
                /*channel.ModelShutdown += (object sender, ShutdownEventArgs e) =>
                {
                    Logger.E("Rabbit model hatası!");
                    //MailHelper.SendMail(MailHelper.Adresler, MailHelper.MailBaslik, "Rabbit break model hatası! (Activity)");

                    //if (e.Initiator != ShutdownInitiator.Application)
                    //{
                    //    Task.Run(() => ((AutorecoveringModel)channelActivity).AutomaticallyRecover((AutorecoveringConnection)connectionActivity, null));
                    //}
                };*/
                channel.QueueDeclare(queue: QueueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                #endregion

                if (Received != null)
                {
                    Consume();
                }

                Logger.I("Rabbit bağlandı.");
            }
            catch (Exception exception)
            {
                Logger.E(exception);
            }

            Monitor.Exit(lockQueue);
        }

        public void Publish(object publishObject)
        {
            try
            {
                if (!IsConnected) CreateConnection();
                string message = JsonConvert.SerializeObject(publishObject);
                Logger.I($"Rabbit Publish({QueueName}). {message}");
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                     routingKey: QueueName,
                                     basicProperties: null,
                                     body: body);
            }
            catch (Exception exception)
            {
                Logger.E(exception);
            }
        }

        public void Consume(bool _noAck = false)
        {
            try
            {
                Logger.I($"Rabbit Consume{QueueName}");

                if (!IsConnected) CreateConnection();
                var consumer = new EventingBasicConsumer(channel);
                if (Received != null)
                    consumer.Received += Received;
                channel.BasicQos(0, PrefetchCount, false);
                channel.BasicConsume(queue: QueueName,
                                     noAck: _noAck, // true olursa mesaj okunduğunda silinir.
                                     consumer: consumer);
            }
            catch (Exception exception)
            {
                Logger.E(exception);
            }
        }

        public void BasicAck(ulong deliveryTag, bool multiple = false)
        {
            try
            {
                Logger.I($"Rabbit BasicAck({QueueName}). {deliveryTag}");

                if (!IsConnected) CreateConnection();
                channel.BasicAck(deliveryTag, multiple);
            }
            catch (Exception exception)
            {
                Logger.E(exception);
            }
        }

        public void BasicNack(ulong deliveryTag, bool multiple = false, bool requeue = true)
        {
            try
            {
                Logger.I($"Rabbit BasicNack({QueueName}). {deliveryTag}");

                if (!IsConnected) CreateConnection();
                channel.BasicNack(deliveryTag, multiple, requeue);
            }
            catch (Exception exception)
            {
                Logger.E(exception);
            }
        }

        public void BasicReject(ulong deliveryTag, bool multiple = false)
        {
            try
            {
                Logger.I($"Rabbit BasicReject({QueueName}). {deliveryTag}");

                if (!IsConnected) CreateConnection();
                channel.BasicReject(deliveryTag, multiple);
            }
            catch (Exception exception)
            {
                Logger.E(exception);
            }
        }

        #region IDisposable
        ~RabbitMQManager()
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
                if (channel != null)
                {
                    channel.Close();
                    channel.Dispose();
                }
                if (connection != null)
                {
                    if (connection.IsOpen)
                        connection.Close();
                    connection.Dispose();
                }
                channel = null;
                connection = null;
            }

            disposed = true;
        }
        #endregion
    }
}
