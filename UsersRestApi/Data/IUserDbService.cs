using UsersRestApiService.Models;

namespace UsersRestApiService.Data
{
    public interface IUserDbService
    {
        Task<IResult> CreateUser(User user);
        Task<IEnumerable<User>> GetUsers();
        Task<IResult> UpdateUser(User user);
    }
}