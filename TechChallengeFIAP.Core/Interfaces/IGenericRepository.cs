using TechChallengeFIAP.Core.Entities;

namespace TechChallengeFIAP.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> CountAsync();

        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Task AddAsync(T entity);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
