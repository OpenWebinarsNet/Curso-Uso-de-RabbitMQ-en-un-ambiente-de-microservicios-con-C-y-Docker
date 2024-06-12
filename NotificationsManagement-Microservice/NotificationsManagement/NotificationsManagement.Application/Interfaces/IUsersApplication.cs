using NotificationsManagement.Application.Results;
using NotificationsManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationsManagement.Application.Interfaces
{
    public interface IUsersApplication
    {
        public Task<Result<IEnumerable<UserModel>>> GetAsync();

        public Task<Result<UserModel>> GetByIdAsync(Guid id);

        public Task<Result> PostAsync(UserInputModel userInputModel);

        public Task<Result> PutAsync(UserModel userModel);

        public Task<Result> DeleteAsync(Guid id);
    }
}