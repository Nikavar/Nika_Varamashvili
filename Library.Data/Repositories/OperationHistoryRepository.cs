using Library.Data.Infrastructure;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories
{
    public class OperationHistoryRepository : RepositoryBase<OperationHistory>, IOperationHistoryInterface
    {
        public OperationHistoryRepository(DbFactory dbFactory) : base(dbFactory)
        {
            
        }

        public OperationHistory GetById(int id)
        {
            var history = this.DbContext.Histories.Where(h => h.HistoryID == id).FirstOrDefault();
            return history ?? throw new NotImplementedException();
        }
    }

    public interface IOperationHistoryInterface : IRepository<OperationHistory>
    {

    }
}
