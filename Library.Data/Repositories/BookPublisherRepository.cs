using Library.Data.Infrastructure;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories
{
    public class BookPublisherRepository : BaseRepository<BookPublisher>, IBookPublisherRepository
	{
        public BookPublisherRepository(IDbFactory dbFactory) : base(dbFactory)
        {
                
        }
    }

    public interface IBookPublisherRepository : IBaseRepository<BookPublisher>
    {

    }
}
