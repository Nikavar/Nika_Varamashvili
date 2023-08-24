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
    public class BookAuthorRepository : BaseRepository<BookAuthor>, IBookAuthorRepository
	{
        public BookAuthorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
                
        }
    }

    public interface IBookAuthorRepository : IBaseRepository<BookAuthor>
    {

    }
}
