using NotificationsManagement.Application.Results;
using NotificationsManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationsManagement.Application.Interfaces
{
    public interface INotificationsTypeApplication
    {
        public Task<Result<IEnumerable<NotificationTypeModel>>> GetAsync();

        public Task<Result<NotificationTypeModel>> GetByIdAsync(Guid id);

        public Task<Result> PostAsync(NotificationTypeInputModel notificationTypeInputModel);

        public Task<Result> PutAsync(NotificationTypeModel notificationTypeModel);

        public Task<Result> DeleteAsync(Guid id);
    }
}