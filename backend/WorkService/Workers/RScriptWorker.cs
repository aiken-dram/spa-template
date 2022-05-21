using System.Text;
using Application.Common.Enums;
using Application.Common.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace WorkService;

public class RScriptWorker : BackgroundService
{
    private readonly ILogger _logger;
    private IConnection _connection;
    private IModel _channel;
    private readonly IServiceProvider _serviceProvider;

    public RScriptWorker(
        ILogger<RScriptWorker> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;

        var factory = new ConnectionFactory()
        {
            HostName = "localhost", //maybe switch to config?
            DispatchConsumersAsync = true
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: eQueue.RQueryService,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        _channel.BasicQos(0, 1, false); //this limits delivery to this library to 1 message at a time
        _serviceProvider = serviceProvider;
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }

    private async Task HandleMessage(string content)
    {
        _logger.LogInformation($"R script request received: {content}");
        long id;
        if (Int64.TryParse(content, out id))
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var _builder = scope.ServiceProvider.GetRequiredService<IRScriptBuilder>();
                await _builder.ProcessRScriptRequestAsync(id, CancellationToken.None);
            }
        }
        else
            _logger.LogError("Could not read request id from queue!");
        _logger.LogInformation($"R script processed: {content}");
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
            queue: eQueue.QueryService,
            autoAck: false,
            consumer: consumer);

        return Task.CompletedTask;
    }
}
