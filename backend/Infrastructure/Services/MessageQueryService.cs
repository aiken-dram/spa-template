using System.Text;
using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Infrastructure.Services;

public class MessageQueryService : IMessageQueryService
{
    private ConnectionFactory _factory;
    private ILogger _logger;

    public MessageQueryService(
        Infrastructure.Common.Interfaces.IConfigurationService configuration,
        ILogger<MessageQueryService> logger)
    {
        _logger = logger;

        if (configuration.MQ == "localhost")
        {
            _logger.Log(LogLevel.Information, "Connecting to local Rabbit MQ");
            this._factory = new ConnectionFactory() { HostName = "localhost" };
        }
        else if (!string.IsNullOrEmpty(configuration.MQUri))
        {
            _logger.Log(LogLevel.Information, "Connecting to Rabbit MQ uri");
            this._factory = new ConnectionFactory()
            {
                Uri = new Uri(configuration.MQUri)
            };
        }
        else
        {
            logger.Log(LogLevel.Information, "Connecting to Rabbit MQ host {0}", configuration.MQ);
            this._factory = new ConnectionFactory()
            {
                HostName = configuration.MQ,
                Port = configuration.MQPort,
                UserName = configuration.MQUser,
                Password = configuration.MQPass
            };
        }

    }

    public int QueueLength(string queue)
    {
        using (var connection = _factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            return Convert.ToInt32(channel.MessageCount(queue));
        }
    }

    public void SendRequest(Request request)
    => Send(request.IdType switch
    {
        eRequestType.RScript => eQueue.RQueryService,
        _ => eQueue.QueryService
    }, request.IdRequest.ToString(), "");

    private SignalRMessageDto RequestSignalR(Domain.Entities.Request request)
    => new SignalRMessageDto
    {
        From = "server",
        To = "",
        Subject = eSignalRSubject.MessageQuery,
        Id = request.IdRequest,
        IdUser = request.IdUser,
        Body = JsonConvert.SerializeObject(new
        {
            idState = request.IdState,
            state = request.IdState.ToString().ToUpper(), //so i dont have to include navigation property
            created = request.Created,
            processed = request.Processed,
        })
    };

    public void SendRequestSignalR(Domain.Entities.Request request)
    => Send(eQueue.SignalR, JsonConvert.SerializeObject(RequestSignalR(request)), "");

    /// <summary>
    /// Send message into queue service for processing by worker service
    /// </summary>
    /// <param name="queue">Name of queue</param>
    /// <param name="message">Message</param>
    /// <param name="routingKey">Routing key</param>
    private void Send(string queue, string message, string routingKey)
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