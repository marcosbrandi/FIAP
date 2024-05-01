using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;
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
                T? ret = await _fiapContext.Set<T>().FindAsync(id);
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Add(T entity)
        {
            _fiapContext.Add<T>(entity);
        }
        public async void AddAsync(T entity)
        {
            await _fiapContext.AddAsync<T>(entity);
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

    }
}
