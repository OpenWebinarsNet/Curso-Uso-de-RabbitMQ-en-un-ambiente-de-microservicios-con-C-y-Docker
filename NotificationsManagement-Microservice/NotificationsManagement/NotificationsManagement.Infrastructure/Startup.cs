using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationsManagement.Domain.Repositories.Interfaces;
using NotificationsManagement.Infrastructure.Context;
using NotificationsManagement.Infrastructure.MessageBroker.Listener;
using NotificationsManagement.Infrastructure.Repositories;
using RabbitMQ.Client;

namespace NotificationsManagement.Infrastructure
{
    public static class Startup
    {
        private const string SQLCNXSTRINGNAME = "SqlServerConnectionString";
        private const string LOCALHOST = "localhost";

        public static IServiceCollection InitInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NotificationsContext>(opt => opt.UseSqlServer(configuration.GetConnectionString(SQLCNXSTRINGNAME)));
            services.AddSingleton<IConnection>(sp =>
            {
                var factory = new ConnectionFactory() { HostName = LOCALHOST };
                return factory.CreateConnection();
            });

            services.AddSingleton<IModel>(sp =>
            {
                var connection = sp.GetRequiredService<IConnection>();
                return connection.CreateModel();
            });

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<INotificationsTypeRepository, NotificationsTypeRepository>();
            services.AddScoped<INotificationsSentRepository, NotificationsSentRepository>();

            services.AddSingleton<UserOperationsListener>(sp =>
            {
                var connection = sp.GetRequiredService<IConnection>();
                var channel = sp.GetRequiredService<IModel>();
                var usersRepository = sp.GetRequiredService<IUsersRepository>();
                var mapper = sp.GetRequiredService<IMapper>();
                var listener = new UserOperationsListener(connection, channel, usersRepository, mapper);
                return listener;
            });

            services.AddSingleton<NotificationsReceivedListener>(sp =>
            {
                var connection = sp.GetRequiredService<IConnection>();
                var channel = sp.GetRequiredService<IModel>();
                var notificationsSentRepository = sp.GetRequiredService<INotificationsSentRepository>();
                var mapper = sp.GetRequiredService<IMapper>();
                var listener = new NotificationsReceivedListener(connection, channel, notificationsSentRepository, mapper);
                return listener;
            });

            return services;
        }
    }
}
