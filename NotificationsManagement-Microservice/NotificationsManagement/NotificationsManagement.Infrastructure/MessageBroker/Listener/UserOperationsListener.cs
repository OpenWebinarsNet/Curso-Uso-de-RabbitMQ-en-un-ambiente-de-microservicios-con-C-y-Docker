using AutoMapper;
using NotificationsManagement.Domain.Repositories.Interfaces;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotificationsManagement.Domain.Interfaces;
using NotificationsManagement.Domain.Models;
using Newtonsoft.Json;
using NotificationsManagement.Domain.Entities;

namespace NotificationsManagement.Infrastructure.MessageBroker.Listener
{
    public class UserOperationsListener : IUserOperationsListener
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private bool disposedValue;

        public UserOperationsListener(IConnection connection,
                                      IModel channel,
                                      IUsersRepository usersRepository,
                                      IMapper mapper)
        {
            _connection = connection;
            _channel = channel;
            _usersRepository = usersRepository;

            _mapper = mapper;
            CreateQueues();
            CreateUserOperationBindings();

            StartListeningCreatingNewUsersAsync();
            StartListeningUpdatingUsers();
            StartListeningDeletingUsers();
            Console.WriteLine($" START LISTENING USER OPERATIONS!!");
        }

        private void CreateQueues()
        {
            _channel.QueueDeclare(
                            queue: NotificationsManagement.Util.Constants.RabbitMQConstants.UserOperationsCreateQueue,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

            _channel.QueueDeclare(
                            queue: NotificationsManagement.Util.Constants.RabbitMQConstants.UserOperationsUpdateQueue,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

            _channel.QueueDeclare(
                            queue: NotificationsManagement.Util.Constants.RabbitMQConstants.UserOperationsDeleteQueue,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

        }

        private void CreateUserOperationBindings()
        {
            _channel.QueueBind(
                            queue: NotificationsManagement.Util.Constants.RabbitMQConstants.UserOperationsCreateQueue,
                            exchange: NotificationsManagement.Util.Constants.RabbitMQConstants.UsersExchange,
                            routingKey: NotificationsManagement.Util.Constants.RabbitMQConstants.RoutingKey_CreateUser
                            );

            _channel.QueueBind(
                            queue: NotificationsManagement.Util.Constants.RabbitMQConstants.UserOperationsUpdateQueue,
                            exchange: NotificationsManagement.Util.Constants.RabbitMQConstants.UsersExchange,
                            routingKey: NotificationsManagement.Util.Constants.RabbitMQConstants.RoutingKey_UpdateUser
                            );

            _channel.QueueBind(
                            queue: NotificationsManagement.Util.Constants.RabbitMQConstants.UserOperationsDeleteQueue,
                            exchange: NotificationsManagement.Util.Constants.RabbitMQConstants.UsersExchange,
                            routingKey: NotificationsManagement.Util.Constants.RabbitMQConstants.RoutingKey_DeleteUser
                            );
        }

        private void StartListeningCreatingNewUsersAsync()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received FOR ADD {0}", message);

                UserInputModel userInputModel = JsonConvert.DeserializeObject<UserInputModel>(message);

                Task.Run(async () =>
                {
                    userInputModel.CreationDatetime = DateTime.Now;
                    await _usersRepository.AddAsync(_mapper.Map<User>(userInputModel));
                    Console.WriteLine(" [x] USER ADDED");
                });
            };

            _channel.BasicConsume(
                queue: NotificationsManagement.Util.Constants.RabbitMQConstants.UserOperationsCreateQueue,
                autoAck: true,
                consumer: consumer);
        }

        private void StartListeningUpdatingUsers()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received FOR UPDATE {0}", message);

                UserModel userModel = JsonConvert.DeserializeObject<UserModel>(message);
                Task.Run(async () =>
                {
                    await _usersRepository.UpdateAsync(_mapper.Map<User>(userModel));
                    Console.WriteLine(" [x] USER UPDATED");
                });
            };

            _channel.BasicConsume(
                    queue: NotificationsManagement.Util.Constants.RabbitMQConstants.UserOperationsUpdateQueue,
                    autoAck: true,
                    consumer: consumer);
        }

        private void StartListeningDeletingUsers()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received FOR DELETE {0}", message);

                Guid id = Guid.Parse(message);
                Task.Run(async () =>
                {
                    await _usersRepository.RemoveAsync(id);
                    Console.WriteLine(" [x] USER DELETED");
                });
            };

            _channel.BasicConsume(
                    queue: NotificationsManagement.Util.Constants.RabbitMQConstants.UserOperationsDeleteQueue,
                    autoAck: true,
                    consumer: consumer);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                disposedValue = true;
            }
        }

        ~UserOperationsListener()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
