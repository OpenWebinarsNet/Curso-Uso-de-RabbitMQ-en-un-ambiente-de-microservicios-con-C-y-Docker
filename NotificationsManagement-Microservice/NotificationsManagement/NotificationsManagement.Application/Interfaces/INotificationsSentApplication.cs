using NotificationsManagement.Application.Results;
using NotificationsManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationsManagement.Application.Interfaces
{
    public interface INotificationsSentApplication
    {
        public Task<Result<IEnumerable<NotificationSentModel>>> GetAsync();

        public Task<Result<NotificationSentModel>> GetByIdAsync(Guid id);

        public Task<Result> PostAsync(NotificationSentInputModel notificationSentInputModel);

        public Task<Result> PutAsync(NotificationSentModel notificationSentModel);

        public Task<Result> DeleteAsync(Guid id);
    }
}