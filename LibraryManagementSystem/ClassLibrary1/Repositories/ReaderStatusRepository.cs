using Library.Data.Infrastructure;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories
{
    public class ReaderStatusRepository : RepositoryBase<ReaderStatus>, IReaderStatusRepository
    {
        public ReaderStatusRepository(DbFactory dbFactory) : base(dbFactory) {}

        public ReaderStatus GetById(int id)
        {
            var readerStatus = this.DbContext.ReaderStatuses.Where(rs => rs.ReaderStatusID == id).FirstOrDefault();
            return readerStatus ?? throw new NotImplementedException();
        }
    }

    public interface IReaderStatusRepository : IRepository<ReaderStatus>
    {

    }
}
