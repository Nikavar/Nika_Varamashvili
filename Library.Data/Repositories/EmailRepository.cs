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

		public Email GetTemplateByEmailTypeAsync(string emailType)
		{
            var email = dbSet.Where(x => x.TemplateType == emailType).FirstOrDefault();

            return email;
        }
	}

    public interface IEmailRepository : IBaseRepository<Email>
    {
        Email GetTemplateByEmailTypeAsync(string emailType);
    }
}
