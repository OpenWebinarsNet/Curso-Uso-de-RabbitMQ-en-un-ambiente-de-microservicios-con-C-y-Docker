using Microsoft.AspNetCore.Builder;
using RabbitMQ.Client;
using UsersManagement.Infrastructure.MessageBroker;

namespace UsersManagement.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRabbitMQ(this IApplicationBuilder app)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                new MessageBrokerPublisher(connection, channel).CreateExchange();
            }

            return app;
        }
    }
}
