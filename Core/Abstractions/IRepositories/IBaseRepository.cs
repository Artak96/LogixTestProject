using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveChangeAsync();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(int id);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
    }
}
