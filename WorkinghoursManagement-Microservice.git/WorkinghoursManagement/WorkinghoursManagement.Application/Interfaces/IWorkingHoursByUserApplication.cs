using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkinghoursManagement.Application.Results;
using WorkinghoursManagement.Domain.Models;

namespace WorkinghoursManagement.Application.Interfaces
{
    public interface IWorkingHoursByUserApplication
    {
        public Task<Result<IEnumerable<WorkingHoursByUserModel>>> GetAsync();

        public Task<Result<WorkingHoursByUserModel>> GetByIdAsync(Guid id);

        public Task<Result> PostAsync(WorkingHoursByUserInputModel workingHoursByUserInputModel);

        public Task<Result> PutAsync(WorkingHoursByUserModel workingHoursByUserModel);

        public Task<Result> DeleteAsync(Guid id);
    }
}