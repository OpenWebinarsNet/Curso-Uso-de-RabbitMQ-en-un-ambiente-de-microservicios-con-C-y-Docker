using System;
using System.Threading.Tasks;
using WorkinghoursManagement.Domain.Core.Interfaces;
using WorkinghoursManagement.Domain.Entities;

namespace WorkinghoursManagement.Domain.Repositories.Interfaces
{
    public interface IWorkingHoursByUserRepository : IBaseRepository<WorkingHoursByUser>
    {
        Task<WorkingHoursByUser> GetLastRegisterByUserAsync(Guid id);
    }
}