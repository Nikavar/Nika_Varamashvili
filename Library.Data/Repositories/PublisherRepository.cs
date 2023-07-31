using Library.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using Publisher = Library.Model.Models.Publisher;

namespace Library.Data.Repositories
{
    public class PublisherRepository : BaseRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            
        }

        public override Task<IEnumerable<Publisher>> GetAllAsync()
        {
            return base.GetAllAsync();
        }
    }

    public interface IPublisherRepository : IBaseRepository<Publisher>
    {

    }
}
