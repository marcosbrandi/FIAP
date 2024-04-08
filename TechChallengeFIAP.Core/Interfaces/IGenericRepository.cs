using TechChallengeFIAP.Core.Entities;
//using TechChallengeFIAP.Core.Specifications;

namespace TechChallengeFIAP.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> CountAsync();

        void DeleteAsync(T entity);
        void UpdateAsync(T entity);
        void AddAsync(T entity);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
