using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Identity;
using Infrastructure.Files;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Infrastructure services
        /// </summary>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SPADbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("SPADatabase"), p => { p.CommandTimeout(600); })
                    .EnableSensitiveDataLogging(true)); //for workload, though dont think there's a db2advisor in postgre
            /*
            //This was IBM DB2 9.8 configuration:
            services.AddDbContext<SPADbContext>(options =>
                options.UseDb2(configuration.GetConnectionString("SPADatabase"), p => { p.SetServerInfo(IBMDBServerType.LUW); p.UseRowNumberForPaging(); p.CommandTimeout(600); })
                       .EnableSensitiveDataLogging(true)
                       .AddInterceptors(new DB9QueryInterceptor())); //this is for old DB2 version 9.8*/
            services.AddScoped<ISPADbContext>(provider => provider.GetService<SPADbContext>());

            //Message query
            services.AddTransient<IMessageService, MessageService>();

            //File builder
            services.AddTransient<IFileBuilder, FileBuilder>();

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
