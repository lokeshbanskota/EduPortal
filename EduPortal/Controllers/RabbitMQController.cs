using EduPortal.RabbitMQ;
using Microsoft.AspNetCore.Mvc;

namespace EduPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RabbitMQController : ControllerBase
    {
        private readonly RabbitMQProducer _producer;

        public RabbitMQController(RabbitMQProducer producer)
        {
            _producer = producer;
        }

        [HttpPost("send")]
        public IActionResult SendMessage([FromQuery] string queue, [FromBody] string message)
        {
            _producer.PublishMessage(queue, message);
            return Ok("Message sent successfully!");
        }
    }
}
