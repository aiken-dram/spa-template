using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Infrastructure.Service
{
    public class MessageQueryService : IMessageQueryService
    {
        private ConnectionFactory _factory;
        private ILogger _logger;

        public MessageQueryService(ILogger<MessageQueryService> logger)
        {
            this._factory = new ConnectionFactory() { HostName = "localhost" };
            _logger = logger;
        }

        public int QueueLength(string queue)
        {
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                return Convert.ToInt32(channel.MessageCount(queue));
            }
        }

        public void Send(string queue, string message, string routingKey)
        {
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queue,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: queue,
                                     basicProperties: null,
                                     body: body);
                _logger.LogDebug("Message sent: {0}", message);
            }
        }
    }
}