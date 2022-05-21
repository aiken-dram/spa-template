using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Identity;
using Infrastructure.Files;
using Infrastructure.Persistence;
using IBM.EntityFrameworkCore;
using Shared.Infrastructure.Interceptors;
using Infrastructure.Common.Interfaces;
using Infrastructure.Service;
using Infrastructure.MessageQuery;
using Infrastructure.RScript;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Infrastructure services
        /// </summary>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Database (switching to DB2 in docker, cuz easier)
            services.AddDbContext<SPADbContext>(options =>
                options.UseDb2(configuration.GetConnectionString("SPADatabase"), p =>
                {
                    p.SetServerInfo(IBMDBServerType.LUW);
                    p.UseRowNumberForPaging();
                    p.CommandTimeout(600);
                })
                .EnableSensitiveDataLogging(true));
            services.AddScoped<ISPADbContext>(provider => provider.GetRequiredService<SPADbContext>());

            //Domain events
            services.AddScoped<IDomainEventService, DomainEventService>();

            //Message query
            services.AddTransient<IMessageQueryService, MessageQueryService>();

            //Query builder
            services.AddTransient<IQueryResponseBuilder, QueryResponseBuilder>();

            //R script builder
            services.AddTransient<IRScriptBuilder, RScriptBuilder>();

            //File service
            services.AddTransient<IFileService, FileService>();

            return services;
        }

        /// <summary>
        /// WebApi infrastructure services
        /// </summary>
        public static IServiceCollection AddWebApiInfrastructure(this IServiceCollection services)
        {
            //Authentication
            services.AddTransient<IAuthService, AuthService>();

            //SignalR
            services.AddTransient<INotificationService, NotificationService>();

            return services;
        }
    }
}
