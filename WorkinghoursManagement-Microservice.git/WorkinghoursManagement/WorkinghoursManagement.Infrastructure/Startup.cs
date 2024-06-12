using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using WorkinghoursManagement.Domain.Interfaces;
using WorkinghoursManagement.Domain.Repositories.Interfaces;
using WorkinghoursManagement.Infrastructure.Context;
using WorkinghoursManagement.Infrastructure.MessageBroker.Listener;
using WorkinghoursManagement.Infrastructure.MessageBroker.Publisher;
using WorkinghoursManagement.Infrastructure.Repositories;

namespace WorkinghoursManagement.Infrastructure
{
    public static class Startup
    {
        private const string SQLCNXSTRINGNAME = "SqlServerConnectionString";
        private const string LOCALHOST = "localhost";

        public static IServiceCollection InitInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WorkinghoursContext>(opt => opt.UseSqlServer(configuration.GetConnectionString(SQLCNXSTRINGNAME)));
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
            services.AddScoped<INotificationsMessageBrokerPublisher, NotificationsMessageBrokerPublisher>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IWorkingHoursByUserRepository, WorkingHoursByUserRepository>();

            services.AddSingleton<UserOperationsListener>(sp =>
            {
                var connection = sp.GetRequiredService<IConnection>();
                var channel = sp.GetRequiredService<IModel>();
                var usersRepository = sp.GetRequiredService<IUsersRepository>();
                var workingHoursByUserRepository = sp.GetRequiredService<IWorkingHoursByUserRepository>();
                var mapper = sp.GetRequiredService<IMapper>();
                var listener = new UserOperationsListener(connection, channel, usersRepository, workingHoursByUserRepository, mapper);
                return listener;
            });


            return services;
        }
    }
}