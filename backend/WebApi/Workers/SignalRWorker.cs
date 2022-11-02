using System.Text;
using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Workers;

public class SignalRWorker : BackgroundService
{
    private readonly ILogger _logger;
    private readonly INotificationService _notification;

    private IConnection _connection;
    private IModel _channel;

    public SignalRWorker(
        ILogger<SignalRWorker> logger,
        INotificationService notification)
    {
        _logger = logger;
        _notification = notification;

        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            DispatchConsumersAsync = true
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: eQueue.SignalR,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        _channel.BasicQos(0, 1, false); //this limits delivery to this library to 1 message at a time

    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }

    private async Task HandleMessage(string content)
    {
        _logger.LogInformation($"SignalR received: {content}");
        //content is json
        try
        {
            var signalr = JsonConvert.DeserializeObject<SignalRMessageDto>(content);

            if (signalr != null)
                await _notification.SendAllAsync(signalr);
        }
        catch (Exception err)
        {
            _logger.LogError(err.Message);
        }

        _logger.LogInformation($"SignalR processed: {content}");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            await HandleMessage(message);
            _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };

        _channel.BasicConsume(
            queue: eQueue.SignalR,
            autoAck: false,
            consumer: consumer);

        return Task.CompletedTask;
    }
}
