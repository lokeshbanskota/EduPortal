namespace EduPortal.RabbitMQ
{
    public class RabbitMQBackgroundService : BackgroundService
    {
        private readonly RabbitMQConsumer _consumer;

        public RabbitMQBackgroundService()
        {
            _consumer = new RabbitMQConsumer();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.StartConsuming("my-queue"); // Replace with your queue name
            return Task.CompletedTask;
        }
    }

}
