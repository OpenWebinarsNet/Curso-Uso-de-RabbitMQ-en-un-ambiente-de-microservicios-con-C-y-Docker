using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersManagement.Application.Results;
using UsersManagement.Domain.Models;

namespace UsersManagement.Application.Interfaces
{
    public interface IUsersApplication
    {
        public Task<Result<IEnumerable<UserModel>>> GetAsync();

        public Task<Result<UserModel>> GetByIdAsync(Guid id);

        public Task<Result> PostAsync(UserInputModel userInputModel);

        public Task<Result> PutAsync(UserModel userModel);

        public Task<Result> DeleteAsync(Guid id);

        public Task<Result> LoginAsync(UserLoginInputModel userLoginInputModel);

        public Task<Result> LogoutAsync(Guid id);
    }
}