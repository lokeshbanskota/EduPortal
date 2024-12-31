using global::RabbitMQ.Client.Events;
using global::RabbitMQ.Client;
using System.Text;
namespace EduPortal.RabbitMQ
{
    public class RabbitMQConsumer
    {
        private readonly ConnectionFactory _connectionFactory;

        public RabbitMQConsumer()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = "localhost", // If using Docker, "localhost" is fine. If running natively, use "localhost" as well.
                Port = 5672, // Default RabbitMQ port
                UserName = "guest", // Default username
                Password = "guest", // Default password
                VirtualHost = "/", // Default virtual host
            };
        }

        public void StartConsuming(string queueName)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declare a queue
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Message received from queue '{queueName}': {message}");
                };

                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            }
        }
    }

}
