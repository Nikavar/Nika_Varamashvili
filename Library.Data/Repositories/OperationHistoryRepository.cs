using Library.Data.Infrastructure;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories
{
    public class OperationHistoryRepository : RepositoryBase<LogInfo>, IOperationHistoryInterface
    {
        public OperationHistoryRepository(DbFactory dbFactory) : base(dbFactory)
        {
            
        }

        public LogInfo GetById(int id)
        {
            var history = this.DbContext.Logs.Where(h => h.LogID == id).FirstOrDefault();
            return history ?? throw new NotImplementedException();
        }
    }

    public interface IOperationHistoryInterface : IRepository<LogInfo>
    {

    }
}
