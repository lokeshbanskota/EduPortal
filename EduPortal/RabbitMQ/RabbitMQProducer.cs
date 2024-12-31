using RabbitMQ.Client; // Ensure this is the correct namespace
using System.Text;
namespace EduPortal.RabbitMQ
{
    public class RabbitMQProducer
    {
        private readonly ConnectionFactory _connectionFactory;

        public RabbitMQProducer()
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
        public void PublishMessage(string queueName, string message)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declare a queue
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                // Publish the message
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);

                Console.WriteLine($"Message sent to queue '{queueName}': {message}");
            }
        }
    }

}
