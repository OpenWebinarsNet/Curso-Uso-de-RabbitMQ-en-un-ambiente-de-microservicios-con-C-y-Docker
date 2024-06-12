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
    public class NotificationsReceivedListener : INotificationsReceivedListener
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly INotificationsSentRepository _notificationsSentRepository;
        private readonly IMapper _mapper;
        private bool disposedValue;

        public NotificationsReceivedListener(IConnection connection,
                                      IModel channel,
                                      INotificationsSentRepository notificationsSentRepository,
                                      IMapper mapper)
        {
            _connection = connection;
            _channel = channel;
            _notificationsSentRepository = notificationsSentRepository;

            _mapper = mapper;
            CreateQueues();
            CreateUserOperationBindings();

            StartListeningNotificationsReceivedAsync();
            Console.WriteLine($" START LISTENING NOTIFICATIONS!!");
        }

        private void CreateQueues()
        {
            _channel.QueueDeclare(
                            queue: NotificationsManagement.Util.Constants.RabbitMQConstants.NotificationsReceivedQueue,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
        }

        private void CreateUserOperationBindings()
        {
            _channel.QueueBind(
                            queue: NotificationsManagement.Util.Constants.RabbitMQConstants.NotificationsReceivedQueue,
                            exchange: NotificationsManagement.Util.Constants.RabbitMQConstants.UsersExchange,
                            routingKey: NotificationsManagement.Util.Constants.RabbitMQConstants.RoutingKey_UserNotification
                            );
        }

        private void StartListeningNotificationsReceivedAsync()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received FOR NOTIFICATION{0}", message);

                NotificationSentInputModel notificationSentInputModel = JsonConvert.DeserializeObject<NotificationSentInputModel>(message);

                Task.Run(async () =>
                {
                    await _notificationsSentRepository.AddAsync(_mapper.Map<NotificationSent>(notificationSentInputModel));
                    Console.WriteLine(" [x] NOTIFICATION RECEIVED AND SAVED");
                });
            };

            _channel.BasicConsume(
                queue: NotificationsManagement.Util.Constants.RabbitMQConstants.NotificationsReceivedQueue,
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

        ~NotificationsReceivedListener()
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
