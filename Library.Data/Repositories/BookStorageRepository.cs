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
    public class BookStorageRepository : BaseRepository<BookStorage>, IBookStorageRepository
	{
        public BookStorageRepository(IDbFactory dbFactory) : base(dbFactory)
        {
                
        }
    }

    public interface IBookStorageRepository : IBaseRepository<BookStorage>
    {

    }
}
