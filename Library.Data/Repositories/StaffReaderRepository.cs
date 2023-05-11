using Library.Data.Infrastructure;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories
{
    public class StaffReaderRepository : RepositoryBase<StaffReader>, IStaffReaderRepository
    {
        public StaffReaderRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public StaffReader GetById(int id)
        {
            var staffReader = this.DbContext.StaffReaders.Where(sr => sr.StaffReaderID == id).FirstOrDefault();
            return staffReader ?? throw new NotImplementedException();
        }

        public override void Update(StaffReader entity)
        {
            base.Update(entity);
        }
    }


    public interface IStaffReaderRepository : IRepository<StaffReader>
    {
        
    }
}
