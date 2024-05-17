using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;
using TechChallengeFIAP.Infrastracture.Data;

namespace TechChallengeFIAP.Infrastracture.Repositories
{
    public class Repository<T>(FiapDbContext fiapContext) : IRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> db = fiapContext.Set<T>();
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                return await db.ToListAsync();

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
                children.ToList().ForEach(x => fiapContext.Set<T>().Include(x).Load());
                return await fiapContext.Set<T>().Where(filter).ToListAsync();
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
                //T? ret = await _fiapContext.Set<T>().FindAsync(id);
                T? ret = await db.FindAsync(id);
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
                children.ToList().ForEach(x => fiapContext.Set<T>().Include(x).Load());
                return await fiapContext.Set<T>().Where(z => z.Id == id).FirstOrDefaultAsync();
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
                fiapContext.Add<T>(entity);
                int ret = fiapContext.SaveChanges();
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
                fiapContext.Add<T>(entity);
                await fiapContext.SaveChangesAsync();
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
                fiapContext.Attach<T>(entity);
                fiapContext.Entry(entity).State = EntityState.Modified;
                fiapContext.Update(entity);
                int ret = fiapContext.SaveChanges();
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
                int ret = 0;
                /*
                T? found = await GetByIdAsync(entity.Id);
                if (found != null)
                {
                    fiapContext.Entry(found).State = EntityState.Detached;
                }
                fiapContext.Attach(entity);
                fiapContext.Entry(entity).State = EntityState.Modified;
                ret = await fiapContext.SaveChangesAsync();
                */

                fiapContext.Attach<T>(entity);
                fiapContext.Entry(entity).State = EntityState.Modified;
                fiapContext.Update(entity);
                ret = await fiapContext.SaveChangesAsync();

                return ret;
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                return await db.CountAsync();
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
                return db.Count();
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
                fiapContext.Remove<T>(entity);
                int ret = fiapContext.SaveChanges();
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
                fiapContext.Remove<T>(entity);
                int ret = await fiapContext.SaveChangesAsync();
                return ret;
            }
            catch
            {
                throw;
            }
        }

    }
}
