using Library.Data.Infrastructure;
using Library.Model.Models;
using Microsoft.AspNet.Identity;
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

		public async Task<Email> GetTemplateByEmailTypeAsync(Expression<Func<Email, bool>> filter)
		{
            return await dbSet.Where(filter).FirstOrDefaultAsync();
		}
	}

    public interface IEmailRepository : IBaseRepository<Email>
    {
        Task<Email> GetTemplateByEmailTypeAsync(Expression<Func<Email,bool>> filter);
    }
}
