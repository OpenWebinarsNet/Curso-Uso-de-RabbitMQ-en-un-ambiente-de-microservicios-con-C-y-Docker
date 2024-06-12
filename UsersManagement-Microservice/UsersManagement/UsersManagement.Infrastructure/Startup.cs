using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using UsersManagement.Domain.Interfaces;
using UsersManagement.Domain.Repositories.Interfaces;
using UsersManagement.Infrastructure.Context;
using UsersManagement.Infrastructure.MessageBroker;
using UsersManagement.Infrastructure.Repositories;

namespace UsersManagement.Infrastructure
{
    public static class Startup
    {
        private const string SQLCNXSTRINGNAME = "SqlServerConnectionString";
        private const string LOCALHOST = "localhost";

        /// <summary>
        /// Initializes the infrastructure layer.
        /// </summary>
        /// <param name="services">Services</param>
        /// <param name="configuration">Configuration</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection InitInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UsersContext>(opt => opt.UseSqlServer(configuration.GetConnectionString(SQLCNXSTRINGNAME)));
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
            services.AddScoped<IMessageBrokerPublisher, MessageBrokerPublisher>();
            services.AddScoped<IUsersRepository, UsersRepository>();

            return services;
        }
    }
}