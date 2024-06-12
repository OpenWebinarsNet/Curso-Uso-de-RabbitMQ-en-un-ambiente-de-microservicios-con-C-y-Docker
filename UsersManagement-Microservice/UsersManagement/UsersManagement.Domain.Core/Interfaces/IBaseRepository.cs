using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<int> AddAsync(T obj);

        Task<int> UpdateAsync(T obj);

        Task<int> RemoveAsync(Guid id);

        Task<T> SelectByIdAsync(Guid id);

        Task<IEnumerable<T>> SelectAllAsync();

        Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression);
    }
}