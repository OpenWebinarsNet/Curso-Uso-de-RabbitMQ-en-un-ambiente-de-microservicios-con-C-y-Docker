using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UsersManagement.Domain.Entities;
using UsersManagement.Domain.Repositories.Interfaces;
using UsersManagement.Infrastructure.Context;

namespace UsersManagement.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public UsersContext _context { get; }

        public UsersRepository(UsersContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(User user)
        {
            _context.Add(user);

            return await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmailAndPassword(string email, string password)
        {
            return await Task.FromResult(_context.Users.FirstOrDefault(x => x.Email == email && x.Password == password));
        }

        public async Task<IQueryable<User>> GetByConditionAsync(Expression<Func<User, bool>> expression)
        {
            IQueryable<User> data = null;
            await Task.Run(() => data = _context.Users.Where(expression));
            return data;
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