using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PostsRestApiService.Data;
using PostsRestApiService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Runtime.CompilerServices;
using System.Text;

namespace PostsRestApiService
{
    public static class RabbitMQListener
    {
        public static void ListenForIntegrationEvents(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.Port = 6672;
            var connection = factory.CreateConnection();            
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var contextOptions = new DbContextOptionsBuilder<PostsdbContext>()
                    .UseSqlServer(connectionString)
                    .Options;
                var dbContext = new PostsdbContext(contextOptions);

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);

                var data = JObject.Parse(message);
                var type = ea.RoutingKey;
                if (type == "user.add")
                {
                    dbContext.Users.Add(new User()
                    {
                        UserId = Guid.Parse(data["id"].Value<string>()),
                        UserNames = data["name"].Value<string>()
                    });
                    dbContext.SaveChanges();
                }
                else if (type == "user.update")
                {
                    var user = dbContext.Users.First(a => a.UserId == Guid.Parse(data["id"].Value<string>()));
                    user.UserNames = data["newname"].Value<string>();
                    dbContext.SaveChanges();
                }
            };
            channel.BasicConsume(queue: "user.postservice",
                                     autoAck: true,
                                     consumer: consumer);
        }
    }
}
