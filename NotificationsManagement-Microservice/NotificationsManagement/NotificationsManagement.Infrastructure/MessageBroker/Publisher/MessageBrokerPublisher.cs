using NotificationsManagement.Domain.Interfaces;
using RabbitMQ.Client;
using System;
using IModel = RabbitMQ.Client.IModel;

namespace NotificationsManagement.Infrastructure.MessageBroker.Publisher
{
    internal class MessageBrokerPublisher : IMessageBrokerPublisher
    {
        private readonly IConnection _connection;

        private readonly IModel _channel;
        private bool disposedValue;

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

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~MessageBrokerPublisher()
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
