using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Identity;
using Infrastructure.Files;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Infrastructure.MessageQuery;
using Infrastructure.RScript;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ConfigurationService>(p => new ConfigurationService(configuration));

            services.AddSingleton<Infrastructure.Common.Interfaces.IConfigurationService, ConfigurationService>();
            services.AddSingleton<Application.Common.Interfaces.IConfigurationService, ConfigurationService>();

            return services;
        }

        /// <summary>
        /// Infrastructure services
        /// </summary>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            /*services.AddDbContext<SPADbContext>(options =>
                options.UseDb2(configuration.GetConnectionString("SPADatabase"), p =>
                {
                    p.SetServerInfo(IBMDBServerType.LUW);
                    p.UseRowNumberForPaging();
                    p.CommandTimeout(600);
                })
                .EnableSensitiveDataLogging(true)
                .AddInterceptors(new DB9QueryInterceptor())); //this is for old DB2 version 9.8*/
            services.AddDbContext<SPADbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SPADatabase"), p =>
                {
                    p.CommandTimeout(600);
                })
                .EnableSensitiveDataLogging(true));
            services.AddScoped<ISPADbContext>(provider => provider.GetRequiredService<SPADbContext>());

            //Message query
            services.AddTransient<IMessageQueryService, MessageQueryService>();

            //Query builder
            services.AddScoped<IQueryResponseBuilder, QueryResponseBuilder>();

            //R script builder
            services.AddScoped<IRScriptService, RScriptService>();

            //File builder
            services.AddTransient<IFileService, FileService>();

            return services;
        }

        /// <summary>
        /// WebApi infrastructure services
        /// </summary>
        public static IServiceCollection AddWebApiInfrastructure(this IServiceCollection services)
        {
            //Domain events
            services.AddScoped<IDomainEventService, DomainEventService>();

            //Authentication
            services.AddScoped<IAuthService, AuthService>();

            //SignalR
            services.AddTransient<INotificationService, NotificationService>();

            return services;
        }
    }
}
