using System.Threading.Tasks;
using UsersManagement.Domain.Core.Interfaces;
using UsersManagement.Domain.Entities;

namespace UsersManagement.Domain.Repositories.Interfaces
{
    public interface IUsersRepository : IBaseRepository<User>
    {
        Task<User> GetByEmailAndPassword(string email, string password);
    }
}