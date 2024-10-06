using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Services.RabbitMQ
{
    public class RabbitMQPublisher
    {
        private readonly IRabbitMQClientService _rabbitMQClientService;

        public RabbitMQPublisher(IRabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        public void Publish(object message)
        {
            var channel = _rabbitMQClientService.ConnectSender();

            var messageBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(RabbitMQClientService.ExchangeName, RabbitMQClientService.RoutingExcel, properties, messageBody);
        }
    }
}
