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
    public class UsersRepository : IUsersRepository
    {
        public WorkinghoursContext _context { get; }

        public UsersRepository(WorkinghoursContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(User user)
        {
            _context.Add(user);

            return await _context.SaveChangesAsync();
        }

        public IQueryable<User> GetByCondition(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> SelectAllAsync()
        {
            IEnumerable<User> wfList = null;

            await Task.Run(() =>
            {
                wfList = _context.Users;
            });

            return wfList;
        }

        public async Task<User> SelectByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<int> UpdateAsync(User user)
        {
            var oldData = await SelectByIdAsync(user.Id);

            _context.Entry(oldData).CurrentValues.SetValues(user);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveAsync(Guid id)
        {
            var user = await SelectByIdAsync(id);

            if (user != null)
            {
                _context.Remove(user);

                return await _context.SaveChangesAsync();
            }
            else
                return 0;
        }
    }
}