using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Infrastructure
{
    public interface IBaseRepository<T> where T : class
    {
        #region General Methods for All Classes

        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> filter);
        Task<T> GetByIdAsync(params object[] key);
        Task<T> AddAsync(T entity);
        //Task<T> AddManyAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteManyAsync(Expression<Func<T, bool>> filter);
        Task SaveAsync();

        #endregion
    }
}
