using Microsoft.AspNetCore.Builder;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkinghoursManagement.Infrastructure.MessageBroker.Publisher;

namespace WorkinghoursManagement.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRabbitMQ(this IApplicationBuilder app)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                new NotificationsMessageBrokerPublisher(connection, channel).CreateExchange();
            }

            return app;
        }
    }
}
