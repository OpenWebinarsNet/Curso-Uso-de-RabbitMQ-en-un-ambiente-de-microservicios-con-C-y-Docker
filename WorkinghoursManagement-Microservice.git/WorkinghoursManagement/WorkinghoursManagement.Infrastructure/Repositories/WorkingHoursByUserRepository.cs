using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WorkinghoursManagement.Domain.Entities;
using WorkinghoursManagement.Domain.Repositories.Interfaces;
using WorkinghoursManagement.Infrastructure.Context;

namespace WorkinghoursManagement.Infrastructure.Repositories
{
    public class WorkingHoursByUserRepository : IWorkingHoursByUserRepository
    {
        public WorkinghoursContext _context { get; }

        public WorkingHoursByUserRepository(WorkinghoursContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(WorkingHoursByUser workingHoursByUser)
        {
            _context.Add(workingHoursByUser);

            return await _context.SaveChangesAsync();
        }

        public IQueryable<WorkingHoursByUser> GetByCondition(Expression<Func<WorkingHoursByUser, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WorkingHoursByUser>> SelectAllAsync()
        {
            IEnumerable<WorkingHoursByUser> wfList = null;

            await Task.Run(() =>
            {
                wfList = _context.WorkingHoursByUsers;
            });

            return wfList;
        }

        public async Task<WorkingHoursByUser> SelectByIdAsync(Guid id)
        {
            return await _context.WorkingHoursByUsers.FindAsync(id);
        }

        public async Task<int> UpdateAsync(WorkingHoursByUser workingHoursByUser)
        {
            var oldData = await SelectByIdAsync(workingHoursByUser.Id);

            _context.Entry(oldData).CurrentValues.SetValues(workingHoursByUser);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveAsync(Guid id)
        {
            var workingHoursByUser = await SelectByIdAsync(id);

            if (workingHoursByUser != null)
            {
                _context.Remove(workingHoursByUser);

                return await _context.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<WorkingHoursByUser> GetLastRegisterByUserAsync(Guid id)
        {
            WorkingHoursByUser data = await _context.WorkingHoursByUsers.Where(d => d.UserId == id).OrderByDescending(d => d.RegisteredDateTime).FirstOrDefaultAsync();
            return data;
        }
    }
}