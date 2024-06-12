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
    public class NotificationsSentRepository : INotificationsSentRepository
    {
        public NotificationsContext _context { get; }

        public NotificationsSentRepository(NotificationsContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(NotificationSent notificationSent)
        {
            _context.Add(notificationSent);

            return await _context.SaveChangesAsync();
        }

        public IQueryable<NotificationSent> GetByCondition(Expression<Func<NotificationSent, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<NotificationSent>> SelectAllAsync()
        {
            IEnumerable<NotificationSent> wfList = null;

            await Task.Run(() =>
            {
                wfList = _context.NotificationsSent;
            });

            return wfList;
        }

        public async Task<NotificationSent> SelectByIdAsync(Guid id)
        {
            return await _context.NotificationsSent.FindAsync(id);
        }

        public async Task<int> UpdateAsync(NotificationSent newUser)
        {
            var oldData = await SelectByIdAsync(newUser.Id);

            _context.Entry(oldData).CurrentValues.SetValues(newUser);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveAsync(Guid id)
        {
            var notificationSent = await SelectByIdAsync(id);

            if (notificationSent != null)
            {
                _context.Remove(notificationSent);

                return await _context.SaveChangesAsync();
            }
            else
                return 0;
        }
    }
}
