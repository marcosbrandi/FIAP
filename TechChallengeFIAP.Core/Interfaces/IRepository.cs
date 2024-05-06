using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using TechChallengeFIAP.Core.Entities;

namespace TechChallengeFIAP.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllWithTrackingAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children);
        Task<T?> GetByIdWithTrackingAsync(int id, params Expression<Func<T, object>>[] children);

        int Count();
        Task<int> CountAsync();

        Task<int> AddAsync(T entity);
        int Add(T entity);

        int Update(T entity);
        Task<int> UpdateAsync(T entity);

        Task<int> DeleteAsync(T entity);
        int Delete(T entity);
    }
}
