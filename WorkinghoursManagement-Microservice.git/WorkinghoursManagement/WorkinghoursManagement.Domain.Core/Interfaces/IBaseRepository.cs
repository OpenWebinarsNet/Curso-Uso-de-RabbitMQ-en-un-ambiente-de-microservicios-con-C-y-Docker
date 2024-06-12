using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WorkinghoursManagement.Domain.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<int> AddAsync(T obj);

        Task<int> UpdateAsync(T obj);

        Task<int> RemoveAsync(Guid id);

        Task<T> SelectByIdAsync(Guid id);

        Task<IEnumerable<T>> SelectAllAsync();

        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
    }
}