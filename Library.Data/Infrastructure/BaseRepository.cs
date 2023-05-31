using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Infrastructure
{
    /// <summary>
    /// Generic Repository class for performing Database Entity Operations.
    /// </summary>
    /// <typeparam name="T">The Type of Entity to operate on</typeparam>

    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Properties
        protected LibraryContext? dataContext;
        protected readonly DbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected LibraryContext DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }

        #endregion  

        #region Constructor

        public BaseRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        #endregion

        #region Implementation

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }


        public virtual async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> filter)                   
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);

            //var result = await _dbSet.FindAsync(id);

            //if (result != null)
            //    return result;

            //throw new NullReferenceException();
        }

        public virtual async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            //await _dbContext.SaveChangesAsync();
        }
        public virtual async Task UpdateAsync(T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;

            //_dbContext.Entry(entity).State = EntityState.Modified;
            //await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);

            //var deleted = await _dbSet.FindAsync(id);            
            //if (deleted != null)
            //    _dbSet.Remove(deleted);

            //await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteManyAsync(Expression<Func<T, bool>> filter)
        {
            IEnumerable<T> objects = dbSet.Where<T>(filter).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);


            //var filtered = _dbSet.Where(filter);
            //if (filtered != null)
            //    _dbSet.RemoveRange(filtered);

            //await _dbContext.SaveChangesAsync();
        }

        public virtual async Task SaveAsync()
        {
           await dataContext.SaveChangesAsync();
        }

        #endregion
    }
}
