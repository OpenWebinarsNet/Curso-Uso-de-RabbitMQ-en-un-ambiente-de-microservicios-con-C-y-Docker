using NotificationsManagement.Domain.Entities;
using NotificationsManagement.Domain.Repositories.Interfaces;
using NotificationsManagement.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NotificationsManagement.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public NotificationsContext _context { get; }

        public UsersRepository(NotificationsContext context)
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

        public async Task<int> UpdateAsync(User newUser)
        {
            var oldData = await SelectByIdAsync(newUser.Id);

            _context.Entry(oldData).CurrentValues.SetValues(newUser);

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
