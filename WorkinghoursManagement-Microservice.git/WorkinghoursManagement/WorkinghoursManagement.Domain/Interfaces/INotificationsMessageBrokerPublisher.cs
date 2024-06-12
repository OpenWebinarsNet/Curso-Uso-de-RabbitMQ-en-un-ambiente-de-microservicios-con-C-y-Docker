using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkinghoursManagement.Domain.Models;

namespace WorkinghoursManagement.Domain.Interfaces
{
    public interface INotificationsMessageBrokerPublisher : IDisposable
    {
        Task PublishNotificationAsync(NotificationSentInputModel notificationSentInputModel);
    }
}
