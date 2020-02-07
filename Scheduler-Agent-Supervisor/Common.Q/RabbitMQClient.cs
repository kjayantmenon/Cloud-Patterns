using System;



namespace Common.Q
{
    using RabbitMQ.Client;
    using System.Text;
    using System.Threading.Tasks;

    public class RabbitMQClient : IQClient
    {
        private string _hostName;
        private string _qName;
        private string _routingKey = "default-route";
        private string _port = "5672"; 
        public RabbitMQClient()
        {
            
        }
        public RabbitMQClient(string hostName, string qName, string port=null)
        {
            _hostName = hostName;
            _qName = qName;
            _port = port??_port;
                    }
        public async Task PublishAsync(string message)
        {
            this.PublishAsync(_hostName, _qName, _port, message);
        }

        public async Task PublishAsync(string hostName, string qName, string port, string message)
        {
            var factory = new ConnectionFactory() { HostName = hostName, Port = int.Parse(port?? _port) };
            //create a connection using the connection factory and create a channel using the connection
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: qName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                _routingKey = qName;
                
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                    routingKey: _routingKey,
                    basicProperties: null,
                    body: body);
            }
        }
    }
}
