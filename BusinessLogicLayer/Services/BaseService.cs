using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>> predicate);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task DeleteAsync(int entityId);

        Task<T> FindByIdAsync(int entityId);
    }

    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly ICompanyDbContext context;
        public BaseService(ICompanyDbContext context)
        {
            this.context = context;
        }

        public virtual async Task CreateAsync(T entity)
        {
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int entityId)
        {
            var entity = await context.Set<T>().FindAsync(entityId);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public virtual async Task<T> FindByIdAsync(int entityId)
        {
            return await context.Set<T>().FindAsync(entityId);
        }
    }
}