using AutoMapper;
using Flunt.Notifications;
using NotificationsManagement.Application.Interfaces;
using NotificationsManagement.Application.Results;
using NotificationsManagement.Domain.Entities;
using NotificationsManagement.Domain.Models;
using NotificationsManagement.Domain.Repositories.Interfaces;
using NotificationsManagement.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace NotificationsManagement.Application
{
    public class NotificationsTypeApplication : Notifiable<Notification>, INotificationsTypeApplication
    {
        private readonly IMapper _mapper;

        private readonly INotificationsTypeRepository _notificationsTypeRepository;

        public NotificationsTypeApplication(IMapper mapper, INotificationsTypeRepository notificationsTypeRepository)
        {
            _mapper = mapper;

            _notificationsTypeRepository = notificationsTypeRepository;
        }

        public async Task<Result<IEnumerable<NotificationTypeModel>>> GetAsync()
        {
            var users = await _notificationsTypeRepository.SelectAllAsync();

            return users != null ? Result<IEnumerable<NotificationTypeModel>>.Ok(_mapper.Map<IEnumerable<NotificationTypeModel>>(users)) : null;
        }

        public async Task<Result<NotificationTypeModel>> GetByIdAsync(Guid id)
        {
            var user = await _notificationsTypeRepository.SelectByIdAsync(id);

            return user != null ? Result<NotificationTypeModel>.Ok(_mapper.Map<NotificationTypeModel>(user)) : null;
        }

        public async Task<Result> PostAsync(NotificationTypeInputModel notificationTypeInputModel)
        {
            AreValidInformations(notificationTypeInputModel);

            if (!IsValid)
                return Result.Error(Notifications);

            try
            {
                await _notificationsTypeRepository.AddAsync(_mapper.Map<NotificationType>(notificationTypeInputModel));

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Error(new ReadOnlyCollection<Notification>(new List<Notification>
                {
                    new Notification(nameof(NotificationTypeInputModel.Id), ex.InnerException.Message ?? ex.Message)
                }));
            }
        }

        private void AreValidInformations(NotificationTypeInputModel notificationTypeInputModel)
        {
            if (notificationTypeInputModel == null)
                AddNotification(nameof(User), Constants.ReturnMessages.NullOrEmptyData);

            if (notificationTypeInputModel?.Id == Guid.Empty)
                AddNotification(nameof(notificationTypeInputModel.Id), Constants.ReturnMessages.NullOrEmptyData);
        }

        public async Task<Result> PutAsync(NotificationTypeModel notificationTypeModel)
        {
            try
            {
                await _notificationsTypeRepository.UpdateAsync(_mapper.Map<NotificationType>(notificationTypeModel));

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Error(new ReadOnlyCollection<Notification>(new List<Notification>
                {
                    new Notification(nameof(notificationTypeModel.Id), ex.InnerException.Message ?? ex.Message)
                }));
            }
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            try
            {
                await _notificationsTypeRepository.RemoveAsync(id);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Error(new ReadOnlyCollection<Notification>(new List<Notification>
                {
                    new Notification(nameof(User.Id), ex.InnerException.Message ?? ex.Message)
                }));
            }
        }
    }
}