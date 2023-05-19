//using Microsoft.EntityFrameworkCore;
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
        #region Readonlys

        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;


        #endregion

        #region Constructor

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        #endregion

        #region Implementation

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }


        public async Task<IEnumerable<T>> GetManyAsync(
                    Expression<Func<T, bool>> filter = null,
                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                    int? top = null,
                    int? skip = null,
                    params string[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties.Length > 0)
            {
                query = includeProperties.Aggregate(query, (theQuery, theInclude) => theQuery.Include(theInclude));
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (top.HasValue)
            {
                query = query.Take(top.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await _dbSet.FindAsync(id);

            if (result != null)
                return result;

            throw new NullReferenceException();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var deleted = await _dbSet.FindAsync(id);            
            if (deleted != null)
                _dbSet.Remove(deleted);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteManyAsync(Expression<Func<T, bool>> filter)
        {
            var filtered = _dbSet.Where(filter);
            if (filtered != null)
                _dbSet.RemoveRange(filtered);

            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}
