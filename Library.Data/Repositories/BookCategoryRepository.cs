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
    public class BookCategoryRepository : BaseRepository<BookCategory>, IBookCategoryRepository
	{
        public BookCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
                
        }
    }

    public interface IBookCategoryRepository : IBaseRepository<BookCategory>
    {

    }
}
