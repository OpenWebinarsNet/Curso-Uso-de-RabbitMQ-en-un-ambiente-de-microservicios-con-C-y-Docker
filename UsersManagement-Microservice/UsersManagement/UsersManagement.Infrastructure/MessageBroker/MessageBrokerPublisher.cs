using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Domain.Entities;
using UsersManagement.Domain.Interfaces;
using UsersManagement.Domain.Models;

namespace UsersManagement.Infrastructure.MessageBroker
{
    internal class MessageBrokerPublisher : IMessageBrokerPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBrokerPublisher(IConnection connection, IModel channel)
        {
            _connection = connection;
            _channel = channel;
        }

        public void CreateExchange()
        {
            _channel.ExchangeDeclare(
                            exchange: Util.Constants.RabbitMQConstants.UsersExchange,
                            type: ExchangeType.Direct,
                            autoDelete: false,
                            arguments: null);
            Console.WriteLine($"### EXCHANGE CREATED --> {Util.Constants.RabbitMQConstants.UsersExchange} ###");
        }


        public void PublishCreateUser(UserInputModel userInputModel)
        {
            string serializedMessage = JsonConvert.SerializeObject(userInputModel);

            var body = Encoding.UTF8.GetBytes(serializedMessage);

            _channel.BasicPublish(
                    exchange: Util.Constants.RabbitMQConstants.UsersExchange,
                    routingKey: Util.Constants.RabbitMQConstants.RoutingKey_CreateUser,
                    basicProperties: null,
                    body: body);
            Console.WriteLine("### USER ADDED, MESSAGE SENT ###");
        }

        public async Task PublishDeleteUser(Guid id)
        {
            string serializedMessage = id.ToString();

            var body = Encoding.UTF8.GetBytes(serializedMessage);

            await Task.Run(() =>
            {
                _channel.BasicPublish(
                    exchange: Util.Constants.RabbitMQConstants.UsersExchange,
                    routingKey: Util.Constants.RabbitMQConstants.RoutingKey_DeleteUser,
                    basicProperties: null,
                    body: body);
            });
        }

        public async Task PublishUpdateUser(UserModel userModel)
        {
            string serializedMessage = JsonConvert.SerializeObject(userModel);

            var body = Encoding.UTF8.GetBytes(serializedMessage);

            await Task.Run(() =>
            {
                _channel.BasicPublish(
                    exchange: Util.Constants.RabbitMQConstants.UsersExchange,
                    routingKey: Util.Constants.RabbitMQConstants.RoutingKey_UpdateUser,
                    basicProperties: null,
                    body: body);
            });
        }

        public async Task PublishUserLogInOut(UserLogInOutModel userLogInOutModel)
        {
            string serializedMessage = JsonConvert.SerializeObject(userLogInOutModel);

            var body = Encoding.UTF8.GetBytes(serializedMessage);

            await Task.Run(() =>
            {
                _channel.BasicPublish(
                    exchange: Util.Constants.RabbitMQConstants.UsersExchange,
                    routingKey: Util.Constants.RabbitMQConstants.RoutingKey_UserLogInOut,
                    basicProperties: null,
                    body: body);
            });
        }
    }
}
