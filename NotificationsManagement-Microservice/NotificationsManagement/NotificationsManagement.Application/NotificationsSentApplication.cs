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
    public class NotificationsSentApplication : Notifiable<Notification>, INotificationsSentApplication
    {
        private readonly IMapper _mapper;

        private readonly INotificationsSentRepository _notificationsSentRepository;

        public NotificationsSentApplication(IMapper mapper, INotificationsSentRepository notificationsSentRepository)
        {
            _mapper = mapper;

            _notificationsSentRepository = notificationsSentRepository;
        }

        public async Task<Result<IEnumerable<NotificationSentModel>>> GetAsync()
        {
            var notificationsSent = await _notificationsSentRepository.SelectAllAsync();

            return notificationsSent != null ? Result<IEnumerable<NotificationSentModel>>.Ok(_mapper.Map<IEnumerable<NotificationSentModel>>(notificationsSent)) : null;
        }

        public async Task<Result<NotificationSentModel>> GetByIdAsync(Guid id)
        {
            var notificationSent = await _notificationsSentRepository.SelectByIdAsync(id);

            return notificationSent != null ? Result<NotificationSentModel>.Ok(_mapper.Map<NotificationSentModel>(notificationSent)) : null;
        }

        public async Task<Result> PostAsync(NotificationSentInputModel notificationSentInputModel)
        {
            AreValidInformations(notificationSentInputModel);

            if (!IsValid)
                return Result.Error(Notifications);

            try
            {
                await _notificationsSentRepository.AddAsync(_mapper.Map<NotificationSent>(notificationSentInputModel));

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

        private void AreValidInformations(NotificationSentInputModel notificationSentInputModel)
        {
            if (notificationSentInputModel == null)
                AddNotification(nameof(NotificationSent), Constants.ReturnMessages.NullOrEmptyData);

            if (notificationSentInputModel?.Id == Guid.Empty)
                AddNotification(nameof(notificationSentInputModel.Id), Constants.ReturnMessages.NullOrEmptyData);
        }

        public async Task<Result> PutAsync(NotificationSentModel notificationSentModel)
        {
            try
            {
                await _notificationsSentRepository.UpdateAsync(_mapper.Map<NotificationSent>(notificationSentModel));

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Error(new ReadOnlyCollection<Notification>(new List<Notification>
                {
                    new Notification(nameof(notificationSentModel.Id), ex.InnerException.Message ?? ex.Message)
                }));
            }
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            try
            {
                await _notificationsSentRepository.RemoveAsync(id);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Error(new ReadOnlyCollection<Notification>(new List<Notification>
                {
                    new Notification(nameof(NotificationSent.Id), ex.InnerException.Message ?? ex.Message)
                }));
            }
        }
    }
}