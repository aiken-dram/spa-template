using Application;
using Application.Common.Interfaces;
using Infrastructure;
using WorkService;
using WorkService.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddServiceApplication();
        services.AddConfiguration(hostContext.Configuration);
        services.AddInfrastructure(hostContext.Configuration);

        services.AddHostedService<QueryWorker>();
        services.AddHostedService<RScriptWorker>();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IDomainEventService, DomainEventService>();
        services.AddSingleton<IConfiguration>(hostContext.Configuration);
    })
    .Build();

await host.RunAsync();
