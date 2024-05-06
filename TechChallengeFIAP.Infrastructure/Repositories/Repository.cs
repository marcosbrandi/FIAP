using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;
using TechChallengeFIAP.Infrastracture.Data;

namespace TechChallengeFIAP.Infrastracture.Repositories
{
    public class Repository<T>(FiapDbContext _fiapContext) : IRepository<T> where T : BaseEntity
    {
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
        public async Task<IReadOnlyList<T>> GetAllWithTrackingAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children)
        {
            try
            {
                children.ToList().ForEach(x => _fiapContext.Set<T>().Include(x).Load());
                return await _fiapContext.Set<T>().Where(filter).ToListAsync();
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
                T? ret = await _fiapContext.Set<T>().FindAsync(id);
                return (ret is null ? default : ret);
            }
            catch
            {
                throw;
            }
        }

        public async Task<T?> GetByIdWithTrackingAsync(int id, params Expression<Func<T, object>>[] children)
        {
            try
            {
                children.ToList().ForEach(x => _fiapContext.Set<T>().Include(x).Load());
                return await _fiapContext.Set<T>().Where(z => z.Id == id).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public int Add(T entity)
        {
            try
            {
                _fiapContext.Add<T>(entity);
                int ret = _fiapContext.SaveChanges();
                return ret;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AddAsync(T entity)
        {
            try
            {
                _fiapContext.Add<T>(entity);
                await _fiapContext.SaveChangesAsync();
                int ret = entity.Id;
                return ret;
            }
            catch 
            {
                throw;
            }
        }

        public int Update(T entity)
        {
            try
            {
                _fiapContext.Update<T>(entity);
                int ret = _fiapContext.SaveChanges();
                return ret;
            }
            catch 
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(T entity)
        {
            try
            {
                _fiapContext.Update<T>(entity);
                int ret = await _fiapContext.SaveChangesAsync();
                return ret;
            }
            catch 
            {
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                return await _fiapContext.Set<T>().CountAsync();
            }
            catch
            {
                throw;
            }
        }
        public int Count()
        {
            try
            {
                return _fiapContext.Set<T>().Count();
            }
            catch
            {
                throw;
            }
        }

        public int Delete(T entity)
        {
            try
            {
                _fiapContext.Remove<T>(entity);
                int ret = _fiapContext.SaveChanges();
                return ret;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeleteAsync(T entity)
        {
            try
            {
                _fiapContext.Remove<T>(entity);
                int ret = await _fiapContext.SaveChangesAsync();
                return ret;
            }
            catch
            {
                throw;
            }
        }

    }
}
