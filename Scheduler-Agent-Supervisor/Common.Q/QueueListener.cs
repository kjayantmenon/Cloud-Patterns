

namespace Common.Q
{
    using Microsoft.Extensions.Hosting;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class QueueListener:BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private string _qName;
        protected QueueListener(string hostName, string qName)
        {
            InitQ(hostName, qName);
        }
        private void InitQ(string hostName, string qName)
        {
            this._qName = qName;
            var factory = new ConnectionFactory { HostName = hostName };
            // create connection  
            _connection = factory.CreateConnection();
            // create channel  
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(qName, false, false, false, null);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                // received message  
                var content = System.Text.Encoding.UTF8.GetString(ea.Body);

                // handle the received message  
                HandleMessageAsync(content);
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(_qName, false, consumer);

        }

        protected abstract Task HandleMessageAsync(string content);

    }
}
