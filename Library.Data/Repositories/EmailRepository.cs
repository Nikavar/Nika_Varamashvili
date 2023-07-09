using Library.Data.Infrastructure;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories
{
    public class EmailRepository : BaseRepository<Email>, IEmailRepository
    {
        public EmailRepository(IDbFactory dbFactory) : base(dbFactory) 
        {
                
        }

        public IEnumerable<Email> GetEmailEntityByTemplateTypeAsync(Expression<Func<Email,bool>> filter)
        {
            //IQueryable<Email> query = dbSet;

            //if (filter != null)
            //{
            //    query = query.Where(filter);
            //}

            //return await query.FirstOrDefaultAsync();

            return dbSet.Where(x=>x.Equals(filter));
        }
    }

    public interface IEmailRepository : IBaseRepository<Email>
    {
        IEnumerable<Email> GetEmailEntityByTemplateTypeAsync(Expression<Func<Email,bool>> filter);
    }
}
