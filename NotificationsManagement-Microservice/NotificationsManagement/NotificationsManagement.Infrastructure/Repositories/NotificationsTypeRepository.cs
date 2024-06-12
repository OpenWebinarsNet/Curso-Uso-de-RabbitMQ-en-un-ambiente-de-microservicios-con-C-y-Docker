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
    public class NotificationsTypeRepository : INotificationsTypeRepository
    {
        public NotificationsContext _context { get; }

        public NotificationsTypeRepository(NotificationsContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(NotificationType notificationType)
        {
            if (notificationType is null)
            {
                throw new ArgumentNullException(nameof(notificationType));
            }

            _context.Add(notificationType);

            return await _context.SaveChangesAsync();
        }

        public IQueryable<NotificationType> GetByCondition(Expression<Func<NotificationType, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<NotificationType>> SelectAllAsync()
        {
            IEnumerable<NotificationType> wfList = null;

            await Task.Run(() =>
            {
                wfList = _context.NotificationTypes;
            });

            return wfList;
        }

        public async Task<NotificationType> SelectByIdAsync(Guid id)
        {
            return await _context.NotificationTypes.FindAsync(id);
        }

        public async Task<int> UpdateAsync(NotificationType newNotificationType)
        {
            var oldData = await SelectByIdAsync(newNotificationType.Id);

            _context.Entry(oldData).CurrentValues.SetValues(newNotificationType);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveAsync(Guid id)
        {
            var notificationType = await SelectByIdAsync(id);

            if (notificationType != null)
            {
                _context.Remove(notificationType);

                return await _context.SaveChangesAsync();
            }
            else
                return 0;
        }
    }
}
