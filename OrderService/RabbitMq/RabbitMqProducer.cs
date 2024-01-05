using RabbitMQ.Client;
using System.Text;

namespace OrderService.RabbitMq
{
    public class RabbitMqProducer
    {
        private IConnection _connection;
        private IModel _channel;

        public RabbitMqProducer()
        {
            InitRabbitMQ();
        }

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory
            {
                HostName = Environment.GetEnvironmentVariable("RabbitMQ__Hostname"),
                UserName = Environment.GetEnvironmentVariable("RabbitMQ__Username"),
                Password = Environment.GetEnvironmentVariable("RabbitMQ__Password")
            };

            // create connection  
            _connection = factory.CreateConnection();
        }

        private void CreationDuChannel(string nomDuChannel)
        {
            // create channel
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(nomDuChannel, ExchangeType.Topic);
            _channel.QueueDeclare(nomDuChannel, false, false, false, null);
        }

        public void sendMessage(string message, string nomDuChannel)
        {
            byte[] encodeMessage = Encoding.UTF8.GetBytes(message);

            //Permet la création du channel si non existant
            CreationDuChannel(nomDuChannel);

            _channel.BasicPublish("", nomDuChannel, null, encodeMessage);
        }
    }
}
