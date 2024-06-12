using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationsManagement.Application.Interfaces;
using NotificationsManagement.CrossCutting.Assemblies;
using NotificationsManagement.Infrastructure;

namespace NotificationsManagement.Application
{
    public static class Startup
    {
        public static IServiceCollection InitApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AssemblyUtil.GetCurrentAssemblies());

            services.InitInfrastructureLayer(configuration);

            services.AddScoped<IUsersApplication, UsersApplication>();
            services.AddScoped<INotificationsTypeApplication, NotificationsTypeApplication>();
            services.AddScoped<INotificationsSentApplication, NotificationsSentApplication>();
            return services;
        }
    }
}