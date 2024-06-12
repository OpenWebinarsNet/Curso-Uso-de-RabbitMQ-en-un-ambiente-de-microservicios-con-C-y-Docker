using AutoMapper;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WorkinghoursManagement.Application.Interfaces;
using WorkinghoursManagement.Application.Results;
using WorkinghoursManagement.Domain.Entities;
using WorkinghoursManagement.Domain.Models;
using WorkinghoursManagement.Domain.Repositories.Interfaces;
using WorkinghoursManagement.Util;

namespace WorkinghoursManagement.Application
{
    public class WorkingHoursByUserApplication : Notifiable<Notification>, IWorkingHoursByUserApplication
    {
        private readonly IMapper _mapper;

        private readonly IWorkingHoursByUserRepository _workingHoursByUserRepository;

        public WorkingHoursByUserApplication(IMapper mapper, IWorkingHoursByUserRepository workingHoursByUserRepository)
        {
            _mapper = mapper;

            _workingHoursByUserRepository = workingHoursByUserRepository;
        }

        public async Task<Result<IEnumerable<WorkingHoursByUserModel>>> GetAsync()
        {
            var workingHoursByUsers = await _workingHoursByUserRepository.SelectAllAsync();

            return workingHoursByUsers != null ? Result<IEnumerable<WorkingHoursByUserModel>>.Ok(_mapper.Map<IEnumerable<WorkingHoursByUserModel>>(workingHoursByUsers)) : null;
        }

        public async Task<Result<WorkingHoursByUserModel>> GetByIdAsync(Guid id)
        {
            var workingHoursByUser = await _workingHoursByUserRepository.SelectByIdAsync(id);

            return workingHoursByUser != null ? Result<WorkingHoursByUserModel>.Ok(_mapper.Map<WorkingHoursByUserModel>(workingHoursByUser)) : null;
        }

        public async Task<Result> PostAsync(WorkingHoursByUserInputModel workingHoursByUserInputModel)
        {
            AreValidInformations(workingHoursByUserInputModel);

            if (!IsValid)
                return Result.Error(Notifications);

            try
            {
                await _workingHoursByUserRepository.AddAsync(_mapper.Map<WorkingHoursByUser>(workingHoursByUserInputModel));

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Error(new ReadOnlyCollection<Notification>(new List<Notification>
                {
                    new Notification(nameof(WorkingHoursByUser.Id), ex.InnerException.Message ?? ex.Message)
                }));
            }
        }

        private void AreValidInformations(WorkingHoursByUserInputModel workingHoursByUser)
        {
            if (workingHoursByUser == null)
                AddNotification(nameof(WorkingHoursByUser.Id), Constants.ReturnMessages.NullOrEmptyData);

            if (workingHoursByUser?.UserId == Guid.Empty)
                AddNotification(nameof(WorkingHoursByUser.UserId), Constants.ReturnMessages.NullOrEmptyData);

            if (workingHoursByUser.RegisterType != Constants.RegisterConstants.RegisterType_In && workingHoursByUser.RegisterType != Constants.RegisterConstants.RegisterType_Out)
                AddNotification(nameof(WorkingHoursByUser.RegisterType), Constants.ReturnMessages.InvalidDataInOut);
        }

        public async Task<Result> PutAsync(WorkingHoursByUserModel workingHoursByUserModel)
        {
            try
            {
                await _workingHoursByUserRepository.UpdateAsync(_mapper.Map<WorkingHoursByUser>(workingHoursByUserModel));

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Error(new ReadOnlyCollection<Notification>(new List<Notification>
                {
                    new Notification(nameof(WorkingHoursByUser.Id), ex.InnerException.Message ?? ex.Message)
                }));
            }
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            try
            {
                await _workingHoursByUserRepository.RemoveAsync(id);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Error(new ReadOnlyCollection<Notification>(new List<Notification>
                {
                    new Notification(nameof(WorkingHoursByUser.Id), ex.InnerException.Message ?? ex.Message)
                }));
            }
        }
    }
}