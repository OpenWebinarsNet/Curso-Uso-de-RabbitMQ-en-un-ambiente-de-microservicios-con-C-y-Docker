using AutoMapper;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UsersManagement.Application.Interfaces;
using UsersManagement.Application.Results;
using UsersManagement.Domain.Entities;
using UsersManagement.Domain.Interfaces;
using UsersManagement.Domain.Models;
using UsersManagement.Domain.Repositories.Interfaces;
using UsersManagement.Util;

namespace UsersManagement.Application
{
    public class UsersApplication : Notifiable<Notification>, IUsersApplication
    {
        private readonly IMapper _mapper;

        private readonly IUsersRepository _usersRepository;
        private readonly IMessageBrokerPublisher _messageBrokerPublisher;

        public UsersApplication(IMapper mapper, IUsersRepository usersRepository, IMessageBrokerPublisher messageBrokerPublisher)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
            _messageBrokerPublisher = messageBrokerPublisher;
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
                _messageBrokerPublisher.PublishCreateUser(userInputModel);

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
            if (userInputModel is null)
                AddNotification(nameof(userInputModel), Constants.ReturnMessages.NullOrEmptyData);

            if (string.IsNullOrWhiteSpace(userInputModel?.Name))
                AddNotification(nameof(userInputModel.Name), Constants.ReturnMessages.NullOrEmptyData);

            if (string.IsNullOrWhiteSpace(userInputModel?.Email))
                AddNotification(nameof(userInputModel.Email), Constants.ReturnMessages.NullOrEmptyData);

            if (string.IsNullOrWhiteSpace(userInputModel?.Password))
                AddNotification(nameof(userInputModel.Password), Constants.ReturnMessages.NullOrEmptyData);
        }

        private void AreValidLoginData(UserLoginInputModel userLoginInputModel)
        {
            if (userLoginInputModel is null)
                AddNotification(nameof(userLoginInputModel), Constants.ReturnMessages.NullOrEmptyData);

            if (string.IsNullOrWhiteSpace(userLoginInputModel?.Email))
                AddNotification(nameof(userLoginInputModel.Email), Constants.ReturnMessages.NullOrEmptyData);

            if (string.IsNullOrWhiteSpace(userLoginInputModel?.Password))
                AddNotification(nameof(userLoginInputModel.Password), Constants.ReturnMessages.NullOrEmptyData);
        }

        public async Task<Result> PutAsync(UserModel userModel)
        {
            try
            {
                await _usersRepository.UpdateAsync(_mapper.Map<User>(userModel));
                await _messageBrokerPublisher.PublishUpdateUser(userModel);
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

        public async Task<Result> DeleteAsync(Guid id)
        {
            try
            {
                await _usersRepository.RemoveAsync(id);
                await _messageBrokerPublisher.PublishDeleteUser(id);
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

        public async Task<Result> LoginAsync(UserLoginInputModel userLoginInputModel)
        {
            AreValidLoginData(userLoginInputModel);

            if (!IsValid)
                return Result.Error(Notifications);

            var user = await _usersRepository.GetByEmailAndPassword(userLoginInputModel.Email, userLoginInputModel.Password);
            var userLogInOutModel = _mapper.Map<User, UserLogInOutModel>(user);
            userLogInOutModel.RegisteredDateTime = DateTime.Now;
            userLogInOutModel.RegisterType = Constants.RegisterConstants.RegisterType_In;
            await _messageBrokerPublisher.PublishUserLogInOut(userLogInOutModel);

            return user != null ? Result.Ok() : Result.Error(new ReadOnlyCollection<Notification>(new List<Notification>
            {
                new Notification(nameof(UserLoginInputModel), Constants.ReturnMessages.InvalidLogin)
            }));
        }

        public async Task<Result> LogoutAsync(Guid id)
        {
            var user = await _usersRepository.SelectByIdAsync(id);
            var userLogInOutModel = _mapper.Map<User, UserLogInOutModel>(user);
            userLogInOutModel.RegisteredDateTime = DateTime.Now;
            userLogInOutModel.RegisterType = Constants.RegisterConstants.RegisterType_Out;

            await _messageBrokerPublisher.PublishUserLogInOut(userLogInOutModel);

            return Result.Ok();
        }
    }
}