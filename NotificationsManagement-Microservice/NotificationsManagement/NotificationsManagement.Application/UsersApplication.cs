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
    public class UsersApplication : Notifiable<Notification>, IUsersApplication
    {
        private readonly IMapper _mapper;

        private readonly IUsersRepository _usersRepository;

        public UsersApplication(IMapper mapper, IUsersRepository usersRepository)
        {
            _mapper = mapper;

            _usersRepository = usersRepository;
        }

        public async Task<Result<IEnumerable<UserModel>>> GetAsync()
        {
            var users = await _usersRepository.SelectAllAsync();

            return users != null ? Result<IEnumerable<UserModel>>.Ok(_mapper.Map<IEnumerable<UserModel>>(users)) : null;
        }

        public async Task<Result<UserModel>> GetByIdAsync(Guid id)
        {
            var user = await _usersRepository.SelectByIdAsync(id);

            return user != null ? Result<UserModel>.Ok(_mapper.Map<UserModel>(user)) : null;
        }

        public async Task<Result> PostAsync(UserInputModel userInputModel)
        {
            AreValidInformations(userInputModel);

            if (!IsValid)
                return Result.Error(Notifications);

            try
            {
                await _usersRepository.AddAsync(_mapper.Map<User>(userInputModel));

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

        private void AreValidInformations(UserInputModel userInputModel)
        {
            if (userInputModel == null)
                AddNotification(nameof(User), Constants.ReturnMessages.NullOrEmptyData);

            if (userInputModel?.Id == Guid.Empty)
                AddNotification(nameof(userInputModel.Id), Constants.ReturnMessages.NullOrEmptyData);
        }

        public async Task<Result> PutAsync(UserModel userModel)
        {
            try
            {
                await _usersRepository.UpdateAsync(_mapper.Map<User>(userModel));

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Error(new ReadOnlyCollection<Notification>(new List<Notification>
                {
                    new Notification(nameof(userModel.Id), ex.InnerException.Message ?? ex.Message)
                }));
            }
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            try
            {
                await _usersRepository.RemoveAsync(id);

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