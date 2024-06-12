using AutoMapper;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using WorkinghoursManagement.Domain.Entities;
using WorkinghoursManagement.Domain.Interfaces;
using WorkinghoursManagement.Domain.Models;
using WorkinghoursManagement.Domain.Repositories.Interfaces;
using WorkinghoursManagement.Infrastructure.Repositories;

namespace WorkinghoursManagement.Infrastructure.MessageBroker.Listener
{
    public class UserOperationsListener : IUserOperationsListener
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IUsersRepository _usersRepository;
        private readonly IWorkingHoursByUserRepository _workingHoursByUserRepository;
        private readonly IMapper _mapper;
        private bool disposedValue;

        public UserOperationsListener(IConnection connection,
                                      IModel channel,
                                      IUsersRepository usersRepository,
                                      IWorkingHoursByUserRepository workingHoursByUserRepository,
                                      IMapper mapper)
        {
            _connection = connection;
            _channel = channel;
            _usersRepository = usersRepository;
            _workingHoursByUserRepository = workingHoursByUserRepository;
            _mapper = mapper;

            CreateQueues();
            CreateUserOperationsBindings();

            StartListeningCreatingNewUsers();
            StartListeningUpdatingUsers();
            StartListeningDeletingUsers();
            StartListeningUsersLogin();
            StartListeningUsersLogout();
            Console.WriteLine($" START LISTENING!!");
        }        

        private void CreateQueues()
        {
            _channel.QueueDeclare(
                            queue: WorkinghoursManagement.Util.Constants.RabbitMQConstants.UserOperationsCreateQueue,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

            _channel.QueueDeclare(
                            queue: WorkinghoursManagement.Util.Constants.RabbitMQConstants.UserOperationsUpdateQueue,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

            _channel.QueueDeclare(
                            queue: WorkinghoursManagement.Util.Constants.RabbitMQConstants.UserOperationsDeleteQueue,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

            _channel.QueueDeclare(
                            queue: WorkinghoursManagement.Util.Constants.RabbitMQConstants.UserOperationsLogInOutQueue,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
        }

        private void CreateUserOperationsBindings()
        {
            _channel.QueueBind(
                            queue: WorkinghoursManagement.Util.Constants.RabbitMQConstants.UserOperationsCreateQueue,
                            exchange: WorkinghoursManagement.Util.Constants.RabbitMQConstants.UsersExchange,
                            routingKey: WorkinghoursManagement.Util.Constants.RabbitMQConstants.RoutingKey_CreateUser
                            );

            _channel.QueueBind(
                            queue: WorkinghoursManagement.Util.Constants.RabbitMQConstants.UserOperationsUpdateQueue,
                            exchange: WorkinghoursManagement.Util.Constants.RabbitMQConstants.UsersExchange,
                            routingKey: WorkinghoursManagement.Util.Constants.RabbitMQConstants.RoutingKey_UpdateUser
                            );

            _channel.QueueBind(
                            queue: WorkinghoursManagement.Util.Constants.RabbitMQConstants.UserOperationsDeleteQueue,
                            exchange: WorkinghoursManagement.Util.Constants.RabbitMQConstants.UsersExchange,
                            routingKey: WorkinghoursManagement.Util.Constants.RabbitMQConstants.RoutingKey_DeleteUser
                            );

            _channel.QueueBind(
                            queue: WorkinghoursManagement.Util.Constants.RabbitMQConstants.UserOperationsLogInOutQueue,
                            exchange: WorkinghoursManagement.Util.Constants.RabbitMQConstants.UsersExchange,
                            routingKey: WorkinghoursManagement.Util.Constants.RabbitMQConstants.RoutingKey_UserLogInOut
                            );
        }

        private void StartListeningCreatingNewUsers()
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
                queue: WorkinghoursManagement.Util.Constants.RabbitMQConstants.UserOperationsCreateQueue,
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
                    queue: WorkinghoursManagement.Util.Constants.RabbitMQConstants.UserOperationsUpdateQueue,
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
                    queue: WorkinghoursManagement.Util.Constants.RabbitMQConstants.UserOperationsDeleteQueue,
                    autoAck: true,
                    consumer: consumer);
        }

        private void StartListeningUsersLogin()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received FOR LOGIN {0}", message);

                UserLogInOutModel userModel = JsonConvert.DeserializeObject<UserLogInOutModel>(message);
                Task.Run(async () =>
                {
                    WorkingHoursByUser workingHoursByUser = new WorkingHoursByUser();
                    workingHoursByUser.Id = Guid.NewGuid();
                    workingHoursByUser.UserId = userModel.Id;
                    workingHoursByUser.RegisteredDateTime = userModel.RegisteredDateTime;
                    workingHoursByUser.RegisterType = userModel.RegisterType;

                    //check for last register type, if it is login, then we add a logout register
                    await Task.Run(async () =>
                    {
                        WorkingHoursByUser lastRegister = await _workingHoursByUserRepository.GetLastRegisterByUserAsync(userModel.Id);
                        if (lastRegister != null && lastRegister.RegisterType == WorkinghoursManagement.Util.Constants.RegisterConstants.RegisterType_In)
                        {
                            WorkingHoursByUser workingHoursByUserLogout = new WorkingHoursByUser();
                            workingHoursByUserLogout.Id = Guid.NewGuid();
                            workingHoursByUserLogout.UserId = userModel.Id;
                            workingHoursByUserLogout.RegisteredDateTime = userModel.RegisteredDateTime.AddHours(8);
                            workingHoursByUserLogout.RegisterType = WorkinghoursManagement.Util.Constants.RegisterConstants.RegisterType_Out;
                            workingHoursByUserLogout.Comment = "Automatically filled due to not have registerd logout by the user";
                            await _workingHoursByUserRepository.AddAsync(workingHoursByUser);
                        }
                    });
                    await _workingHoursByUserRepository.AddAsync(workingHoursByUser);
                    Console.WriteLine(" [x] USER LOGIN REGISTERED");
                });
            };

            _channel.BasicConsume(
                    queue: WorkinghoursManagement.Util.Constants.RabbitMQConstants.UserOperationsLogInOutQueue,
                    autoAck: true,
                    consumer: consumer);
        }

        private void StartListeningUsersLogout()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received FOR LOGOUT {0}", message);

                UserLogInOutModel userModel = JsonConvert.DeserializeObject<UserLogInOutModel>(message);
                Task.Run(async () =>
                {
                    WorkingHoursByUser workingHoursByUser = new WorkingHoursByUser();
                    workingHoursByUser.Id = Guid.NewGuid();
                    workingHoursByUser.UserId = userModel.Id;
                    workingHoursByUser.RegisteredDateTime = userModel.RegisteredDateTime;
                    workingHoursByUser.RegisterType = userModel.RegisterType;

                    await _workingHoursByUserRepository.AddAsync(workingHoursByUser);

                    Console.WriteLine(" [x] USER LOGOUT REGISTERED");
                });
            };

            _channel.BasicConsume(
                    queue: WorkinghoursManagement.Util.Constants.RabbitMQConstants.UserOperationsLogInOutQueue,
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

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
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
