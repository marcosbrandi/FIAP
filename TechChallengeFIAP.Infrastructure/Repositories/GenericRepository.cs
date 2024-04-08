using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;
//using TechChallengeFIAP.Core.Specifications;
using TechChallengeFIAP.Infrastracture.Data;

namespace TechChallengeFIAP.Infrastracture.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly FiapDbContext _fiapContext;

        public GenericRepository(FiapDbContext fiapContext)
        {
            _fiapContext = fiapContext;
        }
        public void DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                return await _fiapContext.Set<T>().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await _fiapContext.Set<T>().FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void Add(T entity)
        {
            _fiapContext.Add<T>(entity);
        }

        public void Update(T entity)
        {
            _fiapContext.Attach<T>(entity);
            _fiapContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _fiapContext.Set<T>().Remove(entity);
        }

        public async Task<int> CountAsync()
        {
            return await _fiapContext.Set<T>().CountAsync();
        }

        public async void AddAsync(T entity)
        {
            await _fiapContext.AddAsync<T>(entity);
        }
    }
}
