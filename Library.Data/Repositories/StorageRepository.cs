using Library.Data.Infrastructure;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories
{
    public class StorageRepository : BaseRepository<Storage>, IStorageRepository
    {
        public StorageRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }

    public interface IStorageRepository : IBaseRepository<Storage>
    {

    }
}
