using Infrastructure;
using WorkService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddInfrastructure(hostContext.Configuration);
        services.AddHostedService<QueryWorker>();
    })
    .Build();

await host.RunAsync();
