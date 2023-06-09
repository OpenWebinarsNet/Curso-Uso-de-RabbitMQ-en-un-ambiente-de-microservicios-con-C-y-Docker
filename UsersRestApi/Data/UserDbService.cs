using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using UsersRestApiService.Models;

namespace UsersRestApiService.Data
{
    public class UserDbService : IUserDbService
    {
        private readonly UsersdbContext _userDbContext;
        
        public UserDbService(UsersdbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userDbContext.Users.ToListAsync();
        }

        public async Task<IResult> UpdateUser(User user)
        {
            _userDbContext.Entry(user).State = EntityState.Modified;
            await _userDbContext.SaveChangesAsync();

            var integrationEventData = JsonConvert.SerializeObject(new
            {
                id = user.UserId,
                newname = user.UserNames
            });
            PublishToMessageQueue("user.update", integrationEventData);

            return Results.Ok(user);
        }

        public async Task<IResult> CreateUser(User user)
        {
            _userDbContext.Users.Add(user);
            await _userDbContext.SaveChangesAsync();

            var integrationEventData = JsonConvert.SerializeObject(new
            {
                id = user.UserId,
                name = user.UserNames
            });
            PublishToMessageQueue("user.add", integrationEventData);

            return Results.Ok(user);
        }

        private void PublishToMessageQueue(string integrationEvent, string eventData)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.Port = 6672;            
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var body = Encoding.UTF8.GetBytes(eventData);
            channel.BasicPublish(exchange: "user",
                                             routingKey: integrationEvent,
                                             basicProperties: null,
                                             body: body);
        }
    }
}
