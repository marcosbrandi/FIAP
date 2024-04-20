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

        public async Task DeleteAsync(T entity)
        {
            try
            {
                _fiapContext.Remove<T>(entity);
                await _fiapContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                return await _fiapContext.Set<T>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            try
            {
                return await _fiapContext.Set<T>().FindAsync(id);
            }
            catch
            {
                throw;
            }
        }
        public async Task UpdateAsync(T entity)
        {
            try
            {
                _fiapContext.Attach<T>(entity);
                _fiapContext.Entry(entity).State = EntityState.Modified;
                _fiapContext.Update(entity);
                await _fiapContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public void Add(T entity)
        {
        }

        public void Update(T entity)
        {
            try
            {
                _fiapContext.Attach<T>(entity);
                _fiapContext.Entry(entity).State = EntityState.Modified;
                _fiapContext.Update(entity);
                _fiapContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                _fiapContext.Remove<T>(entity);
                _fiapContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            return await _fiapContext.Set<T>().CountAsync();
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                await _fiapContext.AddAsync<T>(entity);
                await _fiapContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
