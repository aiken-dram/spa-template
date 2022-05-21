using Infrastructure;
using WorkService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddInfrastructure(hostContext.Configuration);
        services.AddHostedService<QueryWorker>();
        services.AddHostedService<RScriptWorker>();
    })
    .Build();

await host.RunAsync();
